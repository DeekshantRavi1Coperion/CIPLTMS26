using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class YearEnd
    {

        public DataSet GetYearEndDetails(string catalog, string DBType, string searchText)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_objects_for_year_end";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@catalog", catalog);
            cmd.Parameters.AddWithValue("@type", DBType);
            cmd.Parameters.AddWithValue("@searchText", searchText);
           

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

       
    }
}
