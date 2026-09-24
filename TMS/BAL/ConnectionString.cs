using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
//using System.Windows.Forms;
//using System.Windows.Forms;

namespace BAL
{
    public static class ConnectionString
    {
        public static string GetDBName(int unitID)
        {
            BAL.Common objCommon = new BAL.Common();
            DataSet dsCon = new DataSet();
            string name = string.Empty;
            dsCon = objCommon.GetDBDetails();
            if (dsCon.Tables.Count > 0 && dsCon.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCon.Tables[0].Select("UNIT_ID='" + unitID + "'"))
                {
                    if (dr["DATABASE_NAME"] != DBNull.Value)
                    {
                        name = Convert.ToString(dr["DATABASE_NAME"]);
                    }
                }
            }
            return name;
        }
        
        public static string cipltmsconnectionstring = string.Empty;
        public static string timesheetconnectionstring = string.Empty;

        //public static string GetCommonConn()
        //{
        //    connectionstring = ConfigurationManager.ConnectionStrings["Db_Tc_Attachment"].ConnectionString;
        //    return connectionstring;
        //}

        public static string GetCiplTMSCommonConn()
        {
            cipltmsconnectionstring = ConfigurationManager.ConnectionStrings["CIPLTMS"].ConnectionString;
            return cipltmsconnectionstring;
        }

        public static string GetTimesheetCommonConn()
        {
            timesheetconnectionstring = ConfigurationManager.ConnectionStrings["TIMESHEET"].ConnectionString;
            return timesheetconnectionstring;
        }

        public static string GetWindowsExeTestingCommonConn()
        {
            timesheetconnectionstring = ConfigurationManager.ConnectionStrings["CIPLTMS_WINDOW_APPLICATIONS"].ConnectionString;
            return timesheetconnectionstring;
        }
    }
}
