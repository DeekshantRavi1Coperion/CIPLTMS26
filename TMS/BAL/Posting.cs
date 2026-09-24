using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class Posting
    {
        public DataSet GetEndMarket()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_end_market";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetEndMarketForUnpostedGL()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_end_market_for_unposted_gl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCountryForUnpostedGL()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_country_for_unposted_gl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostingList(int unitId, string unitName, string fromDate, string toDate)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_bill_for_posting02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostingListAllUnits(string fromDate, string toDate, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_bill_for_posting_all_units02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
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

        public int InesrtPosting(string insertQueryTxt)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@insertquery", insertQueryTxt);

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

        public int InesrtPostingNew(int recordID, string month, string jobNo, string custoerCode, string customerName, string billDate,
                                    string classTxt, string currencyDesc, double rate, double fCurrency, double amountInr,
                                    string billNo, string location, string businessSegment, string businessSegmentDesc,
                                    string glCode, string glCodeDesc, int glCodeType, int endMarketID, int countryID, string postingMonth,
                                    int postingYear, double postingValue, int postingCurrency, double unpostedValue, string status,
                                    string udf1, string udf2, string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_posting01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@custoercode", custoerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@billdate", billDate);
            cmd.Parameters.AddWithValue("@classtxt", classTxt);
            cmd.Parameters.AddWithValue("@currencydesc", currencyDesc);
            cmd.Parameters.AddWithValue("@rate", rate);
            cmd.Parameters.AddWithValue("@fcurrency", fCurrency);
            cmd.Parameters.AddWithValue("@amountinr", amountInr);
            cmd.Parameters.AddWithValue("@billno", billNo);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@businesssegment", businessSegment);
            cmd.Parameters.AddWithValue("@businesssegmentdesc", businessSegmentDesc);
            cmd.Parameters.AddWithValue("@glcode", glCode);
            cmd.Parameters.AddWithValue("@glcodedesc", glCodeDesc);
            cmd.Parameters.AddWithValue("@glcodetype", glCodeType);
            cmd.Parameters.AddWithValue("@endmarketid", endMarketID);
            cmd.Parameters.AddWithValue("@countryid", countryID);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrency", postingCurrency);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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

        public DataSet GetPostedList(string fromDate, string toDate, string billNo, string unitName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_posted_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@billno", billNo);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //------------------------------------------date 2018-05-14--------------------------

        public DataSet GetUnpostedList(int unitId, string unitName, string fromDate, string toDate, string invoiceNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_bill_for_posting03";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedListAllUnits(string fromDate, string toDate, string invoiceNo, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_bill_for_posting_all_units03";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InesrtUpdatePosting(int recordID, string invoiceNo, string invoiceDate,
                                        string customerCode, string customerName,
                                        string jobNo, string businessUnit, string productCode, string revenueAccount,
                                        string revenueAccountDesc, int revenueAccountType, double quantity,
                                        double productRate, double invoiceAmount, string location,
                                        int endMarketID, int countryID, string postingMonth,
                                        int postingYear, string financialYear, double postingValue,
                                        int postingCurrencyId, double unpostedValue, string postingDate,
                                        int typeID, int revenueTypeID,
                                        string udf1, string udf2, string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_posting01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@invoicedate", invoiceDate);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@businessunit", businessUnit);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@revenueaccountdesc", revenueAccountDesc);
            cmd.Parameters.AddWithValue("@revenueaccounttype", revenueAccountType);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@productrate", productRate);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@endmarketid", endMarketID);
            cmd.Parameters.AddWithValue("@countryid", countryID);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postringyear", postingYear);
            cmd.Parameters.AddWithValue("@financialyear", financialYear);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrencyid", postingCurrencyId);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@postingdate", postingDate);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);

            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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

        public DataSet GetPostedListNew(string fromDate, string toDate, string billNo, string unitName,
                                        string customerName, string revenueAccount, int isNewInserted)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_posted_list02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@billno", billNo);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@isnewinserted", isNewInserted);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostedListInDetails(string fromDate, string toDate, string billNo, string unitName, string customerName, string revenueAccount)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_posted_list_in_details02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@billno", billNo);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdatePostedBill(int recordID, string invoiceNo, string customerCode, string jobNo, string productCode, string postingValue, int postingCurrencyID,
                                    double unpostedValue, int endMarketID, int countryID, string postingMonth, int postingYear, string postingDate,
                                    int typeID, int revenueTypeID,
                                    string udf1, string udf2, string udf3, string udf4, string udf5, int createdBy,
                                    double postedValue, double editedValue, double newPostingValue)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_posted_bills02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);

            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);

            // cmd.Parameters.AddWithValue("@procuctcode", productCode);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrencyid", postingCurrencyID);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@endmarketid", endMarketID);
            cmd.Parameters.AddWithValue("@countryid", countryID);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);
            cmd.Parameters.AddWithValue("@postingdate", postingDate);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);

            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
            cmd.Parameters.AddWithValue("@createdby", createdBy);

            cmd.Parameters.AddWithValue("@postedvalue", postedValue);
            cmd.Parameters.AddWithValue("@editedvalue", editedValue);
            cmd.Parameters.AddWithValue("@newpostingvalue", newPostingValue);

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

        public int DeletePostedBill(int recordID, int deletedBy, string deletedReason)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_delete_posted_bills";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@deletedby", deletedBy);
            cmd.Parameters.AddWithValue("@deletedreason", deletedReason);

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

        public DataSet GetCustomerDetails(string customerName, string customerCode, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_customer_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductDetail(string productCode, string productDesc, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@productdesc", productDesc);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetRevenueAccountDetail(string revenueAccount, string revenueAccountDesc, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_revenue_account_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@revenueaccountdesc", revenueAccountDesc);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddNewPosting(string invoiceNo, string unitName, string invoiceDate, string jobNo, string customerName, string customerCode,
                                     string businessUnit, string productCode, string revenueAccount, string revenueAccountDesc, int revenueAccountType,
                                     double quantity, double productRate, double invoiceAmount, string postedValue,
                                     double postedValueCurrencyID, string postedDate, double unpostedValue, int endMarketID, int countryID,
                                     string postingMonth, int postingYear,
                                     int typeID, int revenueTypeID, string udf1, string udf2, string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_add_new_posting_01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@invoicedate", invoiceDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@businessunit", businessUnit);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@revenueaccountdesc", revenueAccountDesc);
            cmd.Parameters.AddWithValue("@revenueaccounttype", revenueAccountType);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@productrate", productRate);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@postedvalue", postedValue);
            cmd.Parameters.AddWithValue("@postedvaluecurrencyid", postedValueCurrencyID);
            cmd.Parameters.AddWithValue("@posteddate", postedDate);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@endmarketid", endMarketID);
            cmd.Parameters.AddWithValue("@countryid", countryID);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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


        public DataSet GenerateHFMReport(string fromDate, string toDate, int createBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_generate_hfm_report01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@createdBy", createBy);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GenerateHFMReportRevenueTypeWise(string fromDate, string toDate, int typeID, int revenueTypeID, string revenueAccount, int createBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_generate_hfm_report_revenue_type_wise01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@createdBy", createBy);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //---------------------------------------------------------glwise
        public DataSet GetUnpostedListGLWise(int unitId, string unitName, string fromDate, string toDate, string invoiceNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_bill_for_posting_glwise01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUnpostedListAllUnitsGLWise(string fromDate, string toDate, string invoiceNo, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_bill_for_posting_all_units_glwise02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetRevenueType(int typeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_revenue_and_not_revenue_type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InesrtUpdatePostingNew(int recordID, string invoiceNo, string invoiceDate,
                                        string customerCode, string customerName,
                                        string jobNo, string businessUnit, string productCode, string revenueAccount,
                                        string revenueAccountDesc, int revenueAccountType, double quantity,
                                        double productRate, double invoiceAmount, string location,
                                        string endMarketCode, string countryCode, string postingMonth,
                                        int postingYear, string financialYear, double postingValue,
                                        int postingCurrencyId, double unpostedValue, string postingDate,
                                        int typeID, int revenueTypeID,
                                        string revRecReduction, string udf2, string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_posting02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@invoicedate", invoiceDate);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@businessunit", businessUnit);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@revenueaccountdesc", revenueAccountDesc);
            cmd.Parameters.AddWithValue("@revenueaccounttype", revenueAccountType);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@productrate", productRate);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@endmarketcode", endMarketCode);
            cmd.Parameters.AddWithValue("@countrycode", countryCode);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postringyear", postingYear);
            cmd.Parameters.AddWithValue("@financialyear", financialYear);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrencyid", postingCurrencyId);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@postingdate", postingDate);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);

            cmd.Parameters.AddWithValue("@udf1", revRecReduction);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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



        public int AddNewPostingNew(string invoiceNo, string unitName, string invoiceDate, string jobNo, string customerName, string customerCode,
                                     string businessUnit, string productCode, string revenueAccount, string revenueAccountDesc, int revenueAccountType,
                                     double quantity, double productRate, double invoiceAmount, string postedValue,
                                     double postedValueCurrencyID, string postedDate, double unpostedValue, string endMarketCode, string countryCode,
                                     string postingMonth, int postingYear,
                                     int typeID, int revenueTypeID, string revRecReduction, string udf2, string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_add_new_posting_02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@invoicedate", invoiceDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@businessunit", businessUnit);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@revenueaccountdesc", revenueAccountDesc);
            cmd.Parameters.AddWithValue("@revenueaccounttype", revenueAccountType);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@productrate", productRate);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@postedvalue", postedValue);
            cmd.Parameters.AddWithValue("@postedvaluecurrencyid", postedValueCurrencyID);
            cmd.Parameters.AddWithValue("@posteddate", postedDate);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@endmarketcode", endMarketCode);
            cmd.Parameters.AddWithValue("@countrycode", countryCode);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@udf1", revRecReduction);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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


        public int AddNewPostingOne(string invoiceNo, string unitName, string invoiceDate, string jobNo, string customerName, string customerCode,
                                     string businessUnit, string productCode, string revenueAccount, string revenueAccountDesc, int revenueAccountType,
                                     double quantity, double productRate, string invoiceAmount, string postedValue,
                                     double postedValueCurrencyID, string postedDate, double unpostedValue, string endMarketCode, string countryCode,
                                     string postingMonth, int postingYear,
                                     int typeID, int revenueTypeID, string revRecReduction, string udf2, string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_add_new_posting_02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@invoicedate", invoiceDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@businessunit", businessUnit);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@revenueaccountdesc", revenueAccountDesc);
            cmd.Parameters.AddWithValue("@revenueaccounttype", revenueAccountType);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@productrate", productRate);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@postedvalue", postedValue);
            cmd.Parameters.AddWithValue("@postedvaluecurrencyid", postedValueCurrencyID);
            cmd.Parameters.AddWithValue("@posteddate", postedDate);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@endmarketcode", endMarketCode);
            cmd.Parameters.AddWithValue("@countrycode", countryCode);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);

            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@udf1", revRecReduction);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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



        public int UpdatePostedBillNew(int recordID, string revenueAccount, string invoiceNo, string customerCode, string jobNo, string productCode, string postingValue, int postingCurrencyID,
                                    double unpostedValue, string endMarketCode, string countryCode, string postingMonth, int postingYear, string postingDate,
                                    int typeID, int revenueTypeID,
                                    string udf1, string udf2, string udf3, string udf4, string udf5, int createdBy,
                                    double postedValue, double editedValue, double newPostingValue)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_posted_bills03";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrencyid", postingCurrencyID);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);

            cmd.Parameters.AddWithValue("@endmarketcode", endMarketCode);
            cmd.Parameters.AddWithValue("@countrycode", countryCode);

            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);
            cmd.Parameters.AddWithValue("@postingdate", postingDate);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
            cmd.Parameters.AddWithValue("@createdby", createdBy);
            cmd.Parameters.AddWithValue("@postedvalue", postedValue);
            cmd.Parameters.AddWithValue("@editedvalue", editedValue);
            cmd.Parameters.AddWithValue("@newpostingvalue", newPostingValue);

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


        public int UpdatePostedBillNewOne(int recordID, string revenueAccount, string invoiceNo,
                    double invoiceAmount, string customerCode, string jobNo, string productCode,
                    double postingValue, int postingCurrencyID, double unpostedValue, string endMarketCode,
                    string countryCode, string postingMonth, int postingYear, string postingDate, int typeID,
                    int revenueTypeID, string revRecReduction, string udf2, string udf3, string udf4, string udf5, double editedValue,
                    int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_posted_bills05";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrencyid", postingCurrencyID);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@endmarketcode", endMarketCode);
            cmd.Parameters.AddWithValue("@countrycode", countryCode);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);
            cmd.Parameters.AddWithValue("@postingdate", postingDate);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@udf1", revRecReduction);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
            cmd.Parameters.AddWithValue("@editedvalue", editedValue);
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


        public int UpdatePostedBillNewTwo(int recordID, string revenueAccount, string invoiceNo,
                   string invoiceAmount, string customerCode, string jobNo, string productCode,
                   string postingValue, int postingCurrencyID, double unpostedValue, string endMarketCode,
                   string countryCode, string postingMonth, int postingYear, string postingDate, int typeID,
                   int revenueTypeID, string revRecReduction, string udf2, string udf3, string udf4, string udf5, double editedValue,
                   int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_posted_bills05";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@revenueaccount", revenueAccount);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@invoiceamount", invoiceAmount);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@postingvalue", postingValue);
            cmd.Parameters.AddWithValue("@postingcurrencyid", postingCurrencyID);
            cmd.Parameters.AddWithValue("@unpostedvalue", unpostedValue);
            cmd.Parameters.AddWithValue("@endmarketcode", endMarketCode);
            cmd.Parameters.AddWithValue("@countrycode", countryCode);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);
            cmd.Parameters.AddWithValue("@postingdate", postingDate);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@revenuetypeid", revenueTypeID);
            cmd.Parameters.AddWithValue("@udf1", revRecReduction);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
            cmd.Parameters.AddWithValue("@editedvalue", editedValue);
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



        public DataSet GetGLDataDumpReport(string PLBS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_GL_data_dump_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@plbs", PLBS);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
            {
                return ds;
            }

            return null;

        }

        public int ImportGLDataDump(DataTable dtpostingData)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();

            using (SqlConnection con = new SqlConnection(cipltmsconnectionstring))
            using (SqlCommand cmd = new SqlCommand("sp_import_GL_data_dump", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // TVP Parameter
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@tblGLDataDump", dtpostingData);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "GL_DATA_DUMP_TYPE_03";

                // Output parameter
                SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int);
                rcode.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(rcode);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(rcode.Value);
                }
                catch (Exception ex)
                {
                    // THIS will show SQL error
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public int CheckCustomerCode(string customercd)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_check_customer_code_importing_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customercd", customercd);

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

        public int CheckJobNo(string jobno)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_check_job_number";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobno);

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

        public int CheckProductCode(string productCode)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_check_product_code_importing_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@prodcode", productCode);

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

        public int CheckCustomerName(string customerName)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_check_customer_name_importing_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customerName", customerName);

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


        public int GetCountryCodeByName(string geographyName)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_get_country_code";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@geographyName", geographyName);

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

        public int GetTypeByName(string type)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_get_type_code";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@type", type);

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


        public int ImportPosting(DataTable dtpostingData)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_import_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblPosting", dtpostingData);

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


        public int GetEndMarketCodeByName(string endMarketName)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_get_endmarket_code";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@endMarketName", endMarketName);

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

        public int GetRevenueTypeByName(string Type, string RevNonRevSubtype)
        {
            String cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_get_type_subtype_code";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@RevNonRevSubtype", RevNonRevSubtype);
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
