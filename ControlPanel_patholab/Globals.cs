using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Text;

using System.Reflection;
using Oracle.ManagedDataAccess.Client;


namespace ControlPanel_patholab
{
    //    internal class Globals
    internal class Globals
    {
        //        internal static string ConnectionString= "Provider=OraOLEDB.Oracle;Data Source=ONEPAT;User ID=lims_sys;Password=lims_sys";
        //         internal static  string ConnectionString = "Provider=OraOLEDB.Oracle;Data Source=NEW-PAT-TEST.MAC.ORG.IL;User ID=lims_sys;Password=limspat";
        internal static string ConnectionString = "Provider=OraOLEDB.Oracle;Data Source=PATHOLOGY.MAC.ORG.IL;User ID=lims_sys;Password=limspat";
        internal static string DBName;
        internal static void InitCon(OracleConnection con)
        {
            Logger.Log(MethodInfo.GetCurrentMethod().Name);
            DBName = ConnectionString.ToUpper()
               .Split(new string[] { "data source".ToUpper() }, StringSplitOptions.None)[1]
               .Split(';')[0]
               .Split('.')[0]
               .Trim('=').Trim();
            //            string userId = ConnectionString.ToLower()
            //                .Split(new string[] { "user id"}, StringSplitOptions.None)[1]
            //                .Split(';')[0]
            //                .Trim('=').Trim();
            //
            //            var password = ConnectionString.ToLower()
            //                .Split(new string[] { "password" }, StringSplitOptions.None)[1]
            //                .Split(';')[0]
            //                .Trim('=').Trim();
            try
            {

                if (con == null)
                {
                    con = new OracleConnection(Globals.ConnectionString);
                }

            //    con.Open(ConnectionString, null, null, 0);
            }
            catch (Exception e)
            {
                throw new ServerException("Unable to connect to server.", e.InnerException);

            }
        }
    }

}
