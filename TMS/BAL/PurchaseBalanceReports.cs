using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class PurchaseBalanceReports
    {

        public int ImportBooks(DataTable dt, string year, string month)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbPur].[sp_import_books]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblRecords", dt);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
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

        public int ImportBooksB2B(DataTable dt, string year, string month)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbPur].[sp_import_books_b2b]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblRecords", dt);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
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

        public DataTable GetImportedBooksList(string year, string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_imported_clean_books]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            da.Fill(ds);
            if (ds != null)
                return ds.Tables[0];

            return null;
        }

        public DataTable GetImportedBooksB2BList(string year, string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_imported_clean_books_b2b]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            da.Fill(ds);
            if (ds != null)
                return ds.Tables[0];

            return null;
        }


        public DataTable GetExcludedBooksList(string year, string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_excluded_books_b2b]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            da.Fill(ds);
            if (ds != null)
                return ds.Tables[0];

            return null;
        }

        public int ImportGSTR2B(DataTable dt, string year, string month)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbPur].[sp_import_gstr2b]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblRecords", dt);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
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


        public DataTable GetImportedGSTR2BList(string year, string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_imported_clean_GSTR2B]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            da.Fill(ds);
            if (ds != null)
                return ds.Tables[0];

            return null;
        }

       

        public DataSet GetReportByBooks(string dateSignS
                                        , string dateTypeS
                                        , string startDateS
                                        , string endDateS
                                        , string gSTINS
                                        , string vendorCodeS
                                        , string vendorNameS
                                        , string invoiceNoS
                                        , string isGSTInvoiceNoAmountMatchedS
                                        , string isGSTInvoiceDateAmountMatchedS
                                        , string isGSTAmountMatchedS
                                        , string isFullyUnmatchedS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_purchase_balance_report_by_books]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@GSTIN", gSTINS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@PartyInvoiceNo", invoiceNoS);
            cmd.Parameters.AddWithValue("@IsGSTInvoiceNoAmountMatched", isGSTInvoiceNoAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsGSTInvoiceDateAmountMatched", isGSTInvoiceDateAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsGSTAmountMatched", isGSTAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsFullyUnmatched", isFullyUnmatchedS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetReportByBooksB2B(string dateSignS
                                        , string dateTypeS
                                        , string startDateS
                                        , string endDateS
                                        , string gSTINS
                                        , string vendorCodeS
                                        , string vendorNameS
                                        , string invoiceNoS
                                        , string isGSTInvoiceNoAmountMatchedS
                                        , string isGSTInvoiceDateAmountMatchedS
                                        , string isGSTAmountMatchedS
                                        , string isFullyUnmatchedS
                                        , string ischkIsGSTNotInS
                                        , string postedYearS
                                        , string postedMonthS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_purchase_balance_report_by_books_b2b]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@GSTIN", gSTINS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@PartyInvoiceNo", invoiceNoS);
            cmd.Parameters.AddWithValue("@IsGSTInvoiceNoAmountMatched", isGSTInvoiceNoAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsGSTInvoiceDateAmountMatched", isGSTInvoiceDateAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsGSTAmountMatched", isGSTAmountMatchedS);
            cmd.Parameters.AddWithValue("@IschkIsGSTNotIn", ischkIsGSTNotInS);
            cmd.Parameters.AddWithValue("@IsFullyUnmatched", isFullyUnmatchedS);

            cmd.Parameters.AddWithValue("@Year", postedYearS);
            cmd.Parameters.AddWithValue("@Month", postedMonthS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetReportByBooksGSTR2B(string dateSignS
                                        , string dateTypeS
                                        , string startDateS
                                        , string endDateS
                                        , string gSTINS
                                        , string vendorCodeS
                                        , string vendorNameS
                                        , string invoiceNoS
                                        , string isGSTInvoiceNoAmountMatchedS
                                        , string isGSTInvoiceDateAmountMatchedS
                                        , string isGSTAmountMatchedS
                                        , string isFullyUnmatchedS
                                        , string ischkIsGSTNotInS
                                        , string postedYearS
                                        , string postedMonthS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_purchase_balance_report_by_gstr2b_b2b]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@GSTIN", gSTINS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@PartyInvoiceNo", invoiceNoS);
            cmd.Parameters.AddWithValue("@IsGSTInvoiceNoAmountMatched", isGSTInvoiceNoAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsGSTInvoiceDateAmountMatched", isGSTInvoiceDateAmountMatchedS);
            cmd.Parameters.AddWithValue("@IsGSTAmountMatched", isGSTAmountMatchedS);
            cmd.Parameters.AddWithValue("@IschkIsGSTNotIn", ischkIsGSTNotInS);
            cmd.Parameters.AddWithValue("@IsFullyUnmatched", isFullyUnmatchedS);

            cmd.Parameters.AddWithValue("@Year", postedYearS);
            cmd.Parameters.AddWithValue("@Month", postedMonthS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataTable GetVendorByGSTIN(DataTable dt)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_vendor_by_gstin]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@tblRecords", dt);

            da.Fill(ds);
            if (ds != null)
                return ds.Tables[0];

            return null;
        }


        public DataTable GetExcludedBooksB2BList(string year, string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_excluded_books_b2b]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            da.Fill(ds);
            if (ds != null)
                return ds.Tables[0];

            return null;
        }



        public DataSet GetGSTReconSummaryReport(
                  string dateSignS
                , string dateTypeS
                , string startDateS
                , string endDateS
                , string gSTINS
                , string vendorCodeS
                , string vendorNameS
                , string invoiceNoS
                , string postedYearS
                , string postedMonthS
                
                , int IsBooksS
                , int IsGSTR2BS

                , int GSTINNotInFACTS
                , int OldInvoiceS
                , int RCMS
                , int PartiallyMatchS
                , int MismatchS

                //, int MismatchTaxableValueS
                //, int MatchInvoiceDateTaxableValueS

                //, int MismatchOldInvoiceS
                //, int MismatchRCMS

            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_purchase_balance_summary_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@GSTIN", gSTINS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@PartyInvoiceNo", invoiceNoS);
            
            cmd.Parameters.AddWithValue("@Year", postedYearS);
            cmd.Parameters.AddWithValue("@Month", postedMonthS);

            cmd.Parameters.AddWithValue("@IsBooks", IsBooksS);
            cmd.Parameters.AddWithValue("@IsGSTR2B", IsGSTR2BS);


            cmd.Parameters.AddWithValue("@GSTINNotInFACT", GSTINNotInFACTS);
            cmd.Parameters.AddWithValue("@OldInvoice", OldInvoiceS);
            cmd.Parameters.AddWithValue("@RCM", RCMS);
            cmd.Parameters.AddWithValue("@PartiallyMatch", PartiallyMatchS);
            cmd.Parameters.AddWithValue("@Mismatch", MismatchS);

            //cmd.Parameters.AddWithValue("@MismatchInvoiceNoAndTaxableValue", MismatchInvoiceNoAndTaxableValueS);
            //cmd.Parameters.AddWithValue("@MismatchInvoiceNo", MismatchInvoiceNoS);
            //cmd.Parameters.AddWithValue("@MatchInvoiceNoTaxableValue", MatchInvoiceNoTaxableValueS);

            //cmd.Parameters.AddWithValue("@MismatchInvoiceDateTaxableValue", MismatchInvoiceDateTaxableValueS);
            //cmd.Parameters.AddWithValue("@MismatchInvoiceDate", MismatchInvoiceDateS);
            //cmd.Parameters.AddWithValue("@MismatchTaxableValue", MismatchTaxableValueS);
            //cmd.Parameters.AddWithValue("@MatchInvoiceDateTaxableValue", MatchInvoiceDateTaxableValueS);

            //cmd.Parameters.AddWithValue("@MismatchOldInvoice", MismatchOldInvoiceS);
            //cmd.Parameters.AddWithValue("@MismatchRCM", MismatchRCMS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetGSTR2BToFACTGSTINLookupReport(
                  string gSTINS
                , string PANS
                , string vendorCodeS
                , string vendorNameS         
                , string statusS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbPur].[sp_get_gstr2b_fact_gstin_lookup_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@GSTIN", gSTINS);
            cmd.Parameters.AddWithValue("@PAN", PANS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@Status", statusS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


    }
}