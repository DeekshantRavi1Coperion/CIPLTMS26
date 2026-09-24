using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BAL
{
    public class Purchase
    {

        public DataSet GetPOReportSummeryToPost(string fromDate, string toDate, string poNo, string vendorName, string unitName, string status, string JOBNo, double amount, int excluedCIDF)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary_to_post01";//sp_get_po_report_summary_to_import
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertPurchase(string poNo, string poDate, string vendorCode, string vendorName,
                                   double amount, string docClass, string poStatus, string location, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_purchase";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@docclass", docClass);
            cmd.Parameters.AddWithValue("@postatus", poStatus);
            cmd.Parameters.AddWithValue("@location", location);
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


        public int PostPurchaseOrder(string poNo, string poDate, string vendorCode, string vendorName, double amount,
                                        string docClass, string poStatus, string location, string lastDeliveryDate,
                                        string expectedDeliveryDate, string isOrderSubjectTo, int noOfMonthsToDeliver, int noOfMonthsBasedUpon,
                                        string paymentTerm, string likelyDateOfReceiptOfMaterial, int salesPivotGroup, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_purchase_new01";//sp_insert_purchase_new
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@docclass", docClass);
            cmd.Parameters.AddWithValue("@postatus", poStatus);
            cmd.Parameters.AddWithValue("@location", location);

            cmd.Parameters.AddWithValue("@lastdeliverydate", lastDeliveryDate);
            cmd.Parameters.AddWithValue("@expecteddeliverydate", expectedDeliveryDate);
            cmd.Parameters.AddWithValue("@isordersubjectto", isOrderSubjectTo);
            cmd.Parameters.AddWithValue("@noofmonthstodeliver", noOfMonthsToDeliver);
            cmd.Parameters.AddWithValue("@noofmonthsbasedupon", noOfMonthsBasedUpon);
            cmd.Parameters.AddWithValue("@paymentrerm", paymentTerm);
            cmd.Parameters.AddWithValue("@likelydateofreceiptofmaterial", likelyDateOfReceiptOfMaterial);
            cmd.Parameters.AddWithValue("@salespivotgroup", salesPivotGroup);

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

        public int UpdatePurchase(int purchaseID, double newPoValueINR, string newLastDeliverDate, string PONO, string vendorCode,
                                        string docClass, string POStatus, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_purchase_and_insert_purchase_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@purchaseid", purchaseID);
            cmd.Parameters.AddWithValue("@newpovalueinr", newPoValueINR);
            cmd.Parameters.AddWithValue("@newlastdeliverydate", newLastDeliverDate);
            cmd.Parameters.AddWithValue("@pono", PONO);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@docclass", docClass);
            cmd.Parameters.AddWithValue("@postatus", POStatus);
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


        public int PostPurchaseOrderNew(string poNo, string poDate, string vendorCode, string vendorName,
                                        double poValueINR, double fraightOrCustomLoading, double totalPOValueINR,
                                        string docClass, string poStatus, string location, string lastDeliveryDate,
                                        string expectedDeliveryDate, int noOfMonthsToDeliver, int noOfMonthsBasedUpon,
                                        string paymentTerm, string isOrderSubjectTo, int salesPivotGroup, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_purchase_new02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@povalueinr", poValueINR);
            cmd.Parameters.AddWithValue("@fraightorcustomloading", fraightOrCustomLoading);
            cmd.Parameters.AddWithValue("@totalpovalueinr", totalPOValueINR);
            cmd.Parameters.AddWithValue("@docclass", docClass);
            cmd.Parameters.AddWithValue("@postatus", poStatus);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@lastdeliverydate", lastDeliveryDate);
            cmd.Parameters.AddWithValue("@expecteddeliverydate", expectedDeliveryDate);
            cmd.Parameters.AddWithValue("@noofmonthstodeliver", noOfMonthsToDeliver);
            cmd.Parameters.AddWithValue("@noofmonthsbasedupon", noOfMonthsBasedUpon);
            cmd.Parameters.AddWithValue("@paymentrerm", paymentTerm);
            cmd.Parameters.AddWithValue("@isordersubjectto", isOrderSubjectTo);
            cmd.Parameters.AddWithValue("@salespivotgroup", salesPivotGroup);
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


        public DataSet GetPostedPOList(string fromDate, string toDate, string poNo, string vendorName, string unitName, string status, string JOBNo, double amount, int excluedCIDF)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_posted_purchase_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetFactPivotGroupListForPosting(string unitName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fact_po_pivot_group_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedPOList(string fromDate, string toDate, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int PostUnpostedPO(string poNo, string poDate, string vendorCode, string vendorName,
                                    double poValueINR, double poPostingValueINR, string postingStatus,
                                    string docClass, int salesPivotGroup, string location, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_unposted_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@povalueinr", poValueINR);
            cmd.Parameters.AddWithValue("@poPostingValueINR", poPostingValueINR);
            cmd.Parameters.AddWithValue("@postingStatus", postingStatus);
            cmd.Parameters.AddWithValue("@docclass", docClass);
            cmd.Parameters.AddWithValue("@salespivotgroup", salesPivotGroup);
            cmd.Parameters.AddWithValue("@location", location);
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



        public int PostUnpostedPO(string poNo, string poDate, string vendorCode, string vendorName,
                                    string jobNo, int pivotGroupID, double budgetedAmount, double poAmount,
                                    double poPostingValue, string postingStatus, string location, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_unposted_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@povalueinr", poAmount);
            cmd.Parameters.AddWithValue("@poPostingValueINR", poPostingValue);
            cmd.Parameters.AddWithValue("@postingStatus", postingStatus);
            cmd.Parameters.AddWithValue("@docclass", jobNo);
            cmd.Parameters.AddWithValue("@salespivotgroup", pivotGroupID);
            cmd.Parameters.AddWithValue("@location", location);
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

        public DataSet GetUnpostedPOListOne(string jobNo, string pivotGroup)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet BindPivotGroupByJob(string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_pivot_group_by_job_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedPOListTwo(string fromDate, string toDate, string jobNo, string pivotGroup)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBudgetedAmountByPivotGroup(string pivotGroup, int pivotGroupID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_pivot_group_budgeted_amt_by_pivot_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBudgetedAmountByPivotGroupOne(string jobNo, string pivotGroup, int pivotGroupID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_pg_budgeted_amt_by_pg";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetUnpostedPOListThree(string fromDate, string toDate, int unitId, string unitName, string jobNo, string pivotGroup)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list04";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedPOListFive(string fromDate, string toDate, int unitId, string unitName, string jobNo, string pivotGroup)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list05";//sp_get_unposted_po_list04
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedPOListSix(string fromDate, string toDate, int unitId, string unitName,
                    string jobNo, string pivotGroup, string vendorName)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list09";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedPOListAllUnits(string fromDate, string toDate, int unitId, string unitName, string jobNo, string pivotGroup,
                                                string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH,
                                                string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list_all_units01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetUnpostedPOListAllUnitsOne(string fromDate, string toDate, int unitId, string unitName, string jobNo, string pivotGroup, string vendorName,
                                                string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH,
                                                string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_po_list_all_units06";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int PostUnpostedPO(string jobNo, string poNo, string poDate, string vendorCode, string vendorName, int pivotGroupID,
                            double budgetedAmount, double poAmount, double postingValue, double pendingAmount,
                            string postingStatus, string postingMonth, string location, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_unposted_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@jobNo", jobNo);
            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Parameters.AddWithValue("@budgetedamount", budgetedAmount);
            cmd.Parameters.AddWithValue("@poamount", poAmount);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@pendingamount", pendingAmount);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@location", location);
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

        public DataSet GetBudgetedAmountByPivotGroup(int unitId, string unitName, string jobNo, double poAmount,
                                                    int pivotGroupID, string pivotGroup)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_budgeted_amt_by_pivot_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@poamount", poAmount);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBudgetedAmountByPivotGroupOne(int unitId, string unitName, string jobNo, double poAmount, int pivotGroupID)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_budgeted_amt_by_pivot_group01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@poamount", poAmount);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBudgetedAmountByPivotGroupTwo(int unitId, string unitName, string jobNo, int pivotGroupID)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_budgeted_amt_by_pivot_group03";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int PostUnpostedPOSix(string poNo, string poDate, string vendorCode, string vendorName,
                        string jobNo, int pivotGroupID, double budgetedAmount, double poAmount,
                        double pendingAmount, double poSavingAmount, string status, string postingMonth,
                        string lastDeliveryDate, string expectedDeliveryDate, int monthsToDeliver,
                        int monthsBasedUpon, string paymentTerms, string isOrderSubjectTo, string location,
                        string postedJOBNo, double deltaPOAmount, double deltaSavingAmount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_unposted_po01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@jobNo", jobNo);
            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Parameters.AddWithValue("@pvbudgetedamount", budgetedAmount);
            cmd.Parameters.AddWithValue("@poamount", poAmount);
            cmd.Parameters.AddWithValue("@pobudgetedamount", pendingAmount);
            cmd.Parameters.AddWithValue("@savingvalue", poSavingAmount);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@lastdeliverydate", lastDeliveryDate);
            cmd.Parameters.AddWithValue("@expecteddeliverydate", expectedDeliveryDate);
            cmd.Parameters.AddWithValue("@monthstodeliver", monthsToDeliver);
            cmd.Parameters.AddWithValue("@monthsbasedupon", monthsBasedUpon);
            cmd.Parameters.AddWithValue("@paymentrerms", paymentTerms);
            cmd.Parameters.AddWithValue("@isordersubjectto", isOrderSubjectTo);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@postedjobno", postedJOBNo);
            cmd.Parameters.AddWithValue("@deltapomount", deltaPOAmount);
            cmd.Parameters.AddWithValue("@deltasavingamount", deltaSavingAmount);
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


        public int PostUnpostedPOSeven(string poNo, string poDate, string vendorCode, string vendorName,
                                     string jobNo, int pivotGroupID, double budgetedAmount, double poAmount,
                                     double pendingAmount, double poSavingAmount,
                                     string status, string postingMonth, string lastDeliveryDate,
                                     string expectedDeliveryDate, int monthsToDeliver, int monthsBasedUpon,
                                     string paymentTerms, string isSubjectToVPOC, string isBillable, string remarks,
                                     string udf1, string udf2, string udf3, string udf4, string udf5, string location,
                                     string postedJOBNo, double deltaPOAmount, double deltaSavingAmount,
                                     int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_unposted_po02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@jobNo", jobNo);
            cmd.Parameters.AddWithValue("@poNo", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Parameters.AddWithValue("@pvbudgetedamount", budgetedAmount);
            cmd.Parameters.AddWithValue("@poamount", poAmount);
            cmd.Parameters.AddWithValue("@pobudgetedamount", pendingAmount);
            cmd.Parameters.AddWithValue("@savingvalue", poSavingAmount);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@lastdeliverydate", lastDeliveryDate);
            cmd.Parameters.AddWithValue("@expecteddeliverydate", expectedDeliveryDate);
            cmd.Parameters.AddWithValue("@monthstodeliver", monthsToDeliver);
            cmd.Parameters.AddWithValue("@monthsbasedupon", monthsBasedUpon);
            cmd.Parameters.AddWithValue("@paymentrerms", paymentTerms);
            cmd.Parameters.AddWithValue("@issubjecttovpoc", isSubjectToVPOC);
            cmd.Parameters.AddWithValue("@isBillable", isBillable);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@postedjobno", postedJOBNo);
            cmd.Parameters.AddWithValue("@deltapomount", deltaPOAmount);
            cmd.Parameters.AddWithValue("@deltasavingamount", deltaSavingAmount);
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

        public DataSet GetBudgetedAmountByPivotGroupAllUnits(string jobNo, int pivotGroupID, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_budgeted_amt_by_pivot_group_all_units01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostedPOReport(string fromDate, string toDate, string poNo, string vendorName,
            string JOBNo, int pivotGroupID, string unitName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_posted_purchase_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@pivotgroupid", pivotGroupID);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetPOListForPosting(int dateTypeID, string fromDate, string toDate, string dbNameA35, string dbNameDLH,
                                           string dbNameGNU, string dbNameSEZ, string poNo, string vendorName, string unitName,
                                           string JOBNo, double amount1, double amount2, string sign,
                                           int excluedCIDF, int excludeEngineeringService, string isDoneOrPending, string followupBy,
                                           string followupDate, string postingStatus, string mrNo, string mrCreatedBy, string checkedBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_list_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@datetypeid", dateTypeID);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedelhi", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount1", amount1);
            cmd.Parameters.AddWithValue("@amount2", amount2);
            cmd.Parameters.AddWithValue("@sign", sign);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);
            cmd.Parameters.AddWithValue("@excludeengineeringservice", excludeEngineeringService);
            cmd.Parameters.AddWithValue("@isdoneorpendingflag", isDoneOrPending);
            cmd.Parameters.AddWithValue("@followupby", followupBy);
            cmd.Parameters.AddWithValue("@followupdate", followupDate);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@mrno", mrNo);
            cmd.Parameters.AddWithValue("@mrcreatedby", mrCreatedBy);
            cmd.Parameters.AddWithValue("@checkedBy", checkedBy);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int PostPOList(DataTable dtTemp1, DataTable dtTemp2, string updateQuery, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_po_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblpoposting", dtTemp1);
            cmd.Parameters.AddWithValue("@tblpopostinglog", dtTemp2);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
            cmd.Parameters.AddWithValue("@createdby", createdBy);
            cmd.Connection = con;
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

        public int DeletePO(DataTable dtDeleteTemp, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_delete_procurement_po";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tbldeletepo", dtDeleteTemp);
            cmd.Parameters.AddWithValue("@createdby", createdBy);
            cmd.Connection = con;
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

        public DataSet GetPODetailList(string PONo, string unitName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_detail_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@unitname", unitName);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPoProcurementPostedList(int dateTypeID, string fromDate, string toDate, string dbNameA35, string dbNameDLH, string dbNameGNU,
                                                  string dbNameSEZ, string poNo, string vendorName, string unitName, string JOBNo, double amount1, double amount2, string sign,
                                                  int excluedCIDF, int excludeEngineeringService, string isDoneOrPending, string followupBy, string followupDate,
                                                  string postingStatus, string mrNo, string mrCreatedBy, string checkedBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_list_for_posting";//sp_get_procurement_po_posted_list
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@datetypeid", dateTypeID);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedelhi", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount1", amount1);
            cmd.Parameters.AddWithValue("@amount2", amount2);
            cmd.Parameters.AddWithValue("@sign", sign);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);
            cmd.Parameters.AddWithValue("@excludeengineeringservice", excludeEngineeringService);
            cmd.Parameters.AddWithValue("@isdoneorpendingflag", isDoneOrPending);
            cmd.Parameters.AddWithValue("@followupby", followupBy);
            cmd.Parameters.AddWithValue("@followupdate", followupDate);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@mrno", mrNo);
            cmd.Parameters.AddWithValue("@mrcreatedby", mrCreatedBy);
            cmd.Parameters.AddWithValue("@checkedBy", checkedBy);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
        public DataSet GetMRCreatedBy()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mr_created_by_list";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetImportModeList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_import_mode_list";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCheckedBy()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_checked_by_list";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



    }
}
