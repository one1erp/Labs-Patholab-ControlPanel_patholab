using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LSExtensionWindowLib;
using LSSERVICEPROVIDERLib;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Oracle.ManagedDataAccess.Client;
using Application = System.Windows.Forms.Application;

using Patholab_Common;



namespace ControlPanel_patholab
{

    [ComVisible(true)]
    [ProgId("ControlPanel_patholab.ControlPanel_patholabcls")]
    public partial class ControlPanel_patholab : UserControl, IExtensionWindow
    {
        #region Ctor
        public ControlPanel_patholab()
        {
            InitializeComponent();
            this.BackColor = Color.FromName("Control");

        }
        #endregion


        #region private members

        private IExtensionWindowSite _ntlsSite;



        private INautilusServiceProvider _sp;

        private OracleConnection _connection;

        private OracleCommand cmd;

        private INautilusDBConnection _ntlsCon;

        private INautilusProcessXML _processXml;

        #endregion


        #region Implementing IExtensionWindow



        public void PreDisplay()
        {
            INautilusDBConnection dbConnection;
            if (_sp != null)
            {
                dbConnection = _sp.QueryServiceProvider("DBConnection") as NautilusDBConnection;
            }
            else
            {
                dbConnection = null;
            }

            ////test
            //System.Diagnostics.Debugger.Launch();
            //var p = new Program();
            //var q=p.Main(null);
            ////end test
            if (DEBUG)
            {
                var cs = "Data Source=PATHOLAB;User ID=lims_sys;Password=lims_sys;";
                _connection = new OracleConnection(cs);

                _connection.Open();

                


            }
            else
                _connection = GetConnection(dbConnection);

            Init();

        }
        public OracleConnection GetConnection(INautilusDBConnection ntlsCon)
        {

            OracleConnection connection = null;

            if (ntlsCon != null)
            {


                // Initialize variables
                String roleCommand;
                // Try/Catch block
                try
                {
                    _connectionString = ntlsCon.GetADOConnectionString();

                    var splited = _connectionString.Split(';');

                    var cs = "";

                    for (int i = 1; i < splited.Count(); i++)
                    {
                        cs += splited[i] + ';';
                    }


                    //Create the connection
                    connection = new OracleConnection(cs);

                    // Open the connection
                    connection.Open();

                    // Get lims user password
                    string limsUserPassword = ntlsCon.GetLimsUserPwd();

                    // Set role lims user
                    if (limsUserPassword == "")
                    {
                        // LIMS_USER is not password protected
                        roleCommand = "set role lims_user";
                    }
                    else
                    {
                        // LIMS_USER is password protected.
                        roleCommand = "set role lims_user identified by " + limsUserPassword;
                    }

           
                    // set the Oracle user for this connecition
                    OracleCommand command = new OracleCommand(roleCommand, connection);

                    // Try/Catch block
                    try
                    {
                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                    catch (Exception f)
                    {
                        // Throw the exception
                        throw new Exception("Inconsistent role Security : " + f.Message);
                    }

                    // Get the session id
                    var sessionId = ntlsCon.GetSessionId();

                    // Connect to the same session
                    string sSql = string.Format("call lims.lims_env.connect_same_session({0})", sessionId);

                    // Build the command
                    command = new OracleCommand(sSql, connection);

                    // Execute the command
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    // Throw the exception
                    throw e;
                }

                // Return the connection
            }

            return connection;

        }
        public void SetParameters(string parameters)
        {


        }

        public bool CloseQuery()
        {
            try
            {
                Logger1.Close();
                if (cmd != null)



                    cmd.Dispose();
                if (_connection != null)

                    _connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                return true;
            }
        }

        public void Internationalise()
        {
        }

        public void SetSite(object site)
        {
            _ntlsSite = (IExtensionWindowSite)site;
            _ntlsSite.SetWindowInternalName("Control Panel");
            _ntlsSite.SetWindowRegistryName("Control Panel");
            _ntlsSite.SetWindowTitle("Control Panel");

        }

        public WindowButtonsType GetButtons()
        {
            return LSExtensionWindowLib.WindowButtonsType.windowButtonsNone;
        }

        public bool SaveData()
        {
            return false;
        }

        public void SaveSettings(int hKey)
        {
        }

        public void Setup()
        {
        }

        public void refresh()
        {

        }

        public WindowRefreshType DataChange()
        {
            return LSExtensionWindowLib.WindowRefreshType.windowRefreshNone;
        }

        public WindowRefreshType ViewRefresh()
        {
            return LSExtensionWindowLib.WindowRefreshType.windowRefreshNone;
        }

        public void SetServiceProvider(object serviceProvider)
        {
            _sp = serviceProvider as NautilusServiceProvider;

            if (_sp != null)
            {
                _processXml = _sp.QueryServiceProvider("ProcessXML") as NautilusProcessXML;
            }
            else
            {
                _processXml = null;
            }


        }

        public void RestoreSettings(int hKey)
        {

        }

        #endregion


        private List<Thread> listThread = new List<Thread>();
        private Queries queries;
        private int NumberOfEndedThreds = 0;
        private List<string> EndedThreds = new List<string>();
        private DateTime runTimeBegin = new DateTime();
        private DateTime runTimeEnd = new DateTime();
        private bool needToRefresh = false;
        private string _connectionString;
        public bool DEBUG;


        private void Init()
        {

            buttonPrint.Enabled = false;
            ButtonCreateExcelFile.Enabled = false;
            bottonCopyToCilipboard.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            queries = new Queries(_connection);
            RunQueries();
        }

        private void RunQueries()
        {
        
            InitDataGridHeaders();

            runTimeBegin = DateTime.Now;
            foreach (Query query in queries.ListQueries)
            {
                ThreadStart runQueryJob = new ThreadStart(query.RunQuery);
                Thread runQueryThread = new Thread(runQueryJob);
                listThread.Add(runQueryThread);
                query.FinishdQueryEvent += new FinishdQueryEventHandler(UpdateUiOnFinishedQuery);

            }
            foreach (Thread runQueryThread in listThread)
            {

                runQueryThread.Start();
            }

            while (NumberOfEndedThreds != queries.ListQueries.Count)
            {
                Thread.Sleep(1500);
                statusLabel.Text = statusLabel.Text + ".";
                if (needToRefresh)
                {
                    EndedQueriesLabel.Text = "שאילתות שהסתיימו:" + string.Join(",", EndedThreds.ToArray());
             
                    while (dataGridView2.Rows.Count > 0)
                    {
                        dataGridView2.Rows.RemoveAt(0);
                    }

         
                    needToRefresh = false;

                }
                Application.DoEvents();
            }
            while (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.Rows.RemoveAt(0);
            }
            runTimeEnd = DateTime.Now;
            buttonPrint.Enabled = true;
            ButtonCreateExcelFile.Enabled = true;
            bottonCopyToCilipboard.Enabled = true;
            statusLabel.Text = "הריצה הסתיימה ב- " + runTimeEnd.ToString("hh:mm:ss");
            this.Cursor = Cursors.Default;
 
            InsertToGrid(queries, dataGridView2, false);
       
        }

        private void UpdateUiOnFinishedQuery(object o, EventArgs e)
        {
            Query query = (Query)o;
            EndedThreds.Add(query.Number.ToString());

            NumberOfEndedThreds++;
            needToRefresh = true;




        }

        private void InitDataGridHeaders()
        {
           

            dataGridView2.Columns.Add("1", @"מס""ד");
            dataGridView2.Columns.Add("2", @"כותרת");
            dataGridView2.Columns.Add("3", @"סה""כ");
            dataGridView2.Columns.Add("4", "מיון לפי");
            dataGridView2.Columns.Add("5", "כמות");
            dataGridView2.Columns.Add("1", @"מס""ד");
            dataGridView2.Columns.Add("2", @"כותרת");
            dataGridView2.Columns.Add("3", @"סה""כ");
            dataGridView2.Columns.Add("4", "מיון לפי");
            dataGridView2.Columns.Add("5", "כמות");
        }


        private void InsertToGrid(Queries queries, DataGridView dgv, bool PlotInTwoCols)
        {
            
            //insert the data in queries to grid
            int initailCol = 0;
            int firstInsertCol = 3;
            int colOfTotal = 2;
            int lastColInFirstRow = 4;
            int rowsPerQuery = 4;
            int NumberOfQueriesInFirstColumn = 6;
            int currntRowNumber;

            Color alternatingColor;


            dgv.RowsDefaultCellStyle.BackColor = Color.LightGray;

            foreach (Query query in queries.ListQueries)
            {


                DataGridViewRow topRow;
                DataGridViewRow row;
                bool inSecondCol = false;


                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                if (query.Number % 2 == 0)
                {
                    alternatingColor = Color.WhiteSmoke;
                }
                else
                {
                    alternatingColor = Color.Gainsboro;
                }

                if (PlotInTwoCols && query.Number > NumberOfQueriesInFirstColumn)
                {
                    inSecondCol = true;
                    initailCol = lastColInFirstRow + 1;
                    currntRowNumber = (query.Number - NumberOfQueriesInFirstColumn - 1) * rowsPerQuery;
                }
                else
                {
                    currntRowNumber = dgv.Rows.Add();
                }
                int numOfOpenedRows = 1;
                if (query.Number > NumberOfQueriesInFirstColumn + 1)
                {
                    currntRowNumber++;
                }
                topRow = dgv.Rows[currntRowNumber];



                dgv.Rows[currntRowNumber].DefaultCellStyle.BackColor = alternatingColor;


                topRow.Cells[initailCol + 0].Value = query.Number;
                topRow.Cells[initailCol + 1].Value = query.Header;
                topRow.DefaultCellStyle.BackColor = Color.Khaki;


                if (query.ResultDic != null)
                    foreach (var item in query.ResultDic)
                    {


                        var colName = item.Key;
                        var colValue = item.Value;

                        if (colName == "TOTAL" ||
                            colName == @"סה""כ")
                        {
                            topRow.Cells[initailCol + colOfTotal].Value = colValue;
                        }
                        else
                        {
                            if (inSecondCol)
                            {
                                currntRowNumber++;
                            }
                            else
                            {
                                currntRowNumber = dgv.Rows.Add();
                            }

                            numOfOpenedRows++;
                            row = dgv.Rows[currntRowNumber];
                            dgv.Rows[currntRowNumber].DefaultCellStyle.BackColor = alternatingColor;
                            var newRes = colName.Replace("_", " ")
                                .Replace("PRIORITY", "עדיפות")
                                .Replace("TOTAL", @"סה""כ");
                            row.Cells[initailCol + firstInsertCol].Value = newRes;

                            row.Cells[initailCol + firstInsertCol + 1].Value =
                                colValue;
                        }




                    }
            }
            if (PlotInTwoCols)
            {
                //                dgv.GridColor  = Color.LightSalmon;
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (column.HeaderText == @"כמות")
                    {

                        column.DividerWidth = 10;
                    }
                }


            }

        }

        private void bottonCopyToCilipboard_Click(object sender, EventArgs e)
        {
            UnFlipGridView(dataGridView2);
            dataGridView2.RightToLeft = RightToLeft.No;
            dataGridView2.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridView2.SelectAll();
            DataObject dataObj = dataGridView2.GetClipboardContent();
            dataGridView2.RightToLeft = RightToLeft.Yes;
           
            string timestamp = DateTime.Now.ToString();
            Clipboard.SetData(DataFormats.Text, timestamp + "\r\n" + dataObj.GetText());
            dataGridView2.ClearSelection();

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintDialog p = new PrintDialog();
            var res = p.ShowDialog();
            if (res.ToString() == "OK")
            {
                CreateExcel(p.PrinterSettings.PrinterName);
            }



        }




        private void UnFlipGridView(DataGridView dataGridView)
        {


            for (int i = 0; i <= dataGridView.Columns.Count - 1; i++)
                dataGridView.Columns[i].DisplayIndex = i;

        }



        private void ButtonCreateExcelFile_Click(object sender, EventArgs e)
        {
            CreateExcel(null);
        }

        private void CreateExcel(string printer)
        {
            ButtonCreateExcelFile.Enabled = false;

            var xlApp = new Microsoft.Office.Interop.Excel.Application();

            Workbook xlWorkBook;
            Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            

            try
            {
                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.DisplayRightToLeft = true;
                //roy
                //System.Diagnostics.Debugger.Launch();
                dataGridView2.SelectAll();
                for (int i = 1; i < dataGridView2.SelectedRows.Count + 1; i++)
                {
                    for (int j = 1; j < dataGridView2.Rows[i - 1].Cells.Count + 1; j++)
                    {
                        var x = dataGridView2.Rows[i - 1].Cells[j - 1].Value;
                        if (x != null)



                            xlWorkSheet.Cells[i, j] = x.ToString();
                    }
                }
                dataGridView2.ClearSelection();
                //TODO:date time into path
                var dt = DateTime.Now;
                var s = String.Format("{0:ddMMyyyyHHmmss}", dt);
                xlWorkBook.SaveAs("c:\\ControlPanel_program\\Control_Panel" + s + ".xls", XlFileFormat.xlWorkbookNormal, misValue,
                    misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue,
                    misValue);
                if (printer != null)
                {
                    xlWorkBook.PrintOutEx();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                ButtonCreateExcelFile.Enabled = true;
                xlWorkSheet = null;
                xlApp = null;
                xlWorkSheet = null;

            }
        }


    }
}