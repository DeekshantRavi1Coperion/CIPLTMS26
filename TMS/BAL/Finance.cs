using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class Finance
    {
        public DataSet GetPrinters()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_printers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAdvanceEPaymentVoucher(int datetTypeID, string dateSign, string fromDate, string toDate, string unitName,
                                                 string vendorCode, string vendorName, string paymentRunNo, string factVoucherNo, string status,
                                                 string orderNo, string projectNo, string bankName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_advance_e_payment_consolidated";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@paymentrunno", paymentRunNo);
            cmd.Parameters.AddWithValue("@factvoucherno", factVoucherNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@orderno", orderNo);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@bankname", bankName);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetEPaymentVoucher(int datetTypeID, string dateSign, string fromDate, string toDate, string unitName,
                                                 string vendorCode, string vendorName, string factVoucherNo, string status,
                                                 string invoiceNo, string partyBillNo,
                                                 string orderNo, string bankName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_e_payment_consolidated";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@factvoucherno", factVoucherNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@invoiceNo", invoiceNo);
            cmd.Parameters.AddWithValue("@partybillno", partyBillNo);
            cmd.Parameters.AddWithValue("@orderno", orderNo);
            cmd.Parameters.AddWithValue("@bankname", bankName);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //Budget    2022-08-03 Added by Deekshant Ravi

        public DataSet GetGLCodeList(int glCodeId, string glCode, int glCodeTypeId, int glCodeSubTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_bud_get_gl_master_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@glcodeid", glCodeId);
            cmd.Parameters.AddWithValue("@glcode", glCode);
            cmd.Parameters.AddWithValue("@glcodetypeid", glCodeTypeId);
            cmd.Parameters.AddWithValue("@glcodesubtypeid", glCodeSubTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetGLTypeList(int glCodeTypeId, string glType)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_bud_get_gl_type_master_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@glcodetypeid", glCodeTypeId);
            cmd.Parameters.AddWithValue("@gltype", glType);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetGLSubTypeList(int glCodeSubTypeId, int glCodeTypeId, string glSubType)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_bud_get_gl_subtype_master_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@glcodetypeid", glCodeSubTypeId);
            cmd.Parameters.AddWithValue("@glcodetypeid", glCodeTypeId);
            cmd.Parameters.AddWithValue("@gltype", glSubType);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetU7TList(int u7TId, string u7T)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_bud_get_u7t_master_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@u7tid", u7TId);
            cmd.Parameters.AddWithValue("@u7t", u7T);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetScenarioList(int scenarioId, string scenario)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_bud_get_scenario_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@scenarioid", scenarioId);
            cmd.Parameters.AddWithValue("@scenario", scenario);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertUpdateGLType(int recordId, string glType, string glTypeDesc, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_bud_insert_update_gl_type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordId);
            cmd.Parameters.AddWithValue("@gltype", glType);
            cmd.Parameters.AddWithValue("@gltypedesc", glTypeDesc);
            cmd.Parameters.AddWithValue("@createdby", createdBy);

            cmd.Parameters.Add(rcode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                int value = Convert.ToInt32(rcode.Value);
                con.Close();
                con.Dispose();
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

       

        public DataTable GetGLMasterList()
        {
            throw new NotImplementedException();
        }

        public DataSet GetGLAndScenarioMasterListAndBudget(string glCodeText,string period, int typeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_bug_get_gl_codes_and_scenario_and_budget";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@glcodes", glCodeText);
            cmd.Parameters.AddWithValue("@period", period);
            cmd.Parameters.AddWithValue("@type", typeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


       
        public int InsertUpdateBudget(int recordId,DataTable dtBudget, string updateQuery, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_bud_insert_update_budget";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordId);            
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
            cmd.Parameters.AddWithValue("@tblbudget", dtBudget);
            cmd.Parameters.AddWithValue("@createdby", createdBy);

            cmd.Parameters.Add(rcode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                int value = Convert.ToInt32(rcode.Value);
                con.Close();
                con.Dispose();
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}
