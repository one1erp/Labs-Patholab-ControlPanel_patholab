
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IEnumerator = System.Collections.IEnumerator;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;

namespace ControlPanel_patholab
{

    public delegate void FinishdQueryEventHandler(object sender, EventArgs e);
    public delegate void FinishedExcelExportEventHandler(object sender, EventArgs e);

    internal class Query
    {
        public event FinishdQueryEventHandler FinishdQueryEvent;

        private OracleConnection con;
        private OracleCommand cmd;
        private OracleDataReader ResultReader;
        public Dictionary<string, string> ResultDic { get; private set; }


        private string SelectStatement;
        private string WhereStatment;

        public string Sql
        {
            get
            {
                return SelectStatement + " " + WhereStatment;
            }
        }

        private string header;
        public string Header
        {

            get { return header; }
            private set { header = value; }

        }
        private int number;
        public int Number
        {

            get { return number; }
            private set { number = value; }

        }

        public Query(OracleConnection con, int number, string header, string selectStatement, string whereStatment)
        {
            this.con = con;
            // Logger.Log(MethodInfo.GetCurrentMethod().Name + " " + number.ToString());
            this.Number = number;
            this.Header = header;
            this.SelectStatement = selectStatement;
            this.WhereStatment = whereStatment;

        }

        public void RunQuery()
        {
            // Logger.Log(MethodInfo.GetCurrentMethod().Name + " " + Number.ToString());
            object temp = new object();

            cmd = new OracleCommand(Sql, con);
            ResultReader = cmd.ExecuteReader();

       

            while (ResultReader.Read())
            {

                ResultDic = new Dictionary<string, string>();
                for (int enumerator = 0; enumerator < ResultReader.FieldCount; enumerator++)
                {
                    var colName = ResultReader.GetName(enumerator).ToString();
                    var colValue = ResultReader[enumerator].ToString();
                    ResultDic.Add(colName, colValue);
          
                }
            }
            ResultReader.Close();

            FinishdQueryEvent(this, EventArgs.Empty);

            // returns the result to the form

        }
    }

    internal class Queries : IEnumerable, IEnumerator
    {
        private const string CONTROLPANEL = "לוח בקרה";
        public List<Query> ListQueries = new List<Query>();
        private OracleConnection con;
        private int Position;
        public Queries(OracleConnection conn)
        {
            System.Diagnostics.Debugger.Launch();
            this.con = conn;
           // // Logger.Log(MethodInfo.GetCurrentMethod().Name);
            string SQL;
         
            SQL = " select filter.NAME,";
            SQL = SQL + " filter.DESCRIPTION select_statment,";
            SQL = SQL + " filter.WHERE_STATEMENT";
            SQL = SQL + "  from lims_sys.filter where filter.NAME like '%" + CONTROLPANEL + "%'";
            SQL = SQL + " order by substr(name,1,2)";

            object temp = new object();
            OracleCommand Filterscmd = new OracleCommand(SQL, con);
            Fillqueries(Filterscmd);

         

        }
        public void Fillqueries(OracleCommand Filterscmd)
        {
            // Logger.Log(MethodInfo.GetCurrentMethod().Name);
            int queryNumber = 1;

            var reader = Filterscmd.ExecuteReader();

            while (reader.Read())
            {


                //    // get the filter name
                string header = reader["name"].ToString().Replace(CONTROLPANEL,"");
                header = header.Substring(3);// recordset.Fields["name"].Value.ToString().Split('-')[1];

                string selectStatement = reader["select_statment"].ToString();
                string whereStatement = reader["WHERE_STATEMENT"].ToString();
                ListQueries.Add(new Query(con, queryNumber++, header, selectStatement, whereStatement));

            }
            reader.Close();
            Filterscmd.Dispose();

        }


        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion

        public bool MoveNext()
        {
            if (Position < ListQueries.Count - 1)
            {
                ++Position;
                return true;
            }
            return false;
        }


        public void Reset()
        {
            Position = -1;
        }

        public object Current
        {
            get
            {
                return ListQueries[Position];
            }
        }
    }
}

