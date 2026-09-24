using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class FCVendorPayment
    {
        public DataSet GetCurrencys()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_currency_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPaymentTypes()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_fc_vendor_payment_types]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPaymentStatusList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_payment_status_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBDetails(string custCode, string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_job_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@customerCode", custCode);
            cmd.Parameters.AddWithValue("@jobNo", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorDetails(string vendorCode, string vendorName, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);
            cmd.Parameters.AddWithValue("@PONo", poNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public string AddUpdateVendorPaymentRequest
            (
                int FCVendorPaymentPID,
                string JobNo,
                string VendorCode,
                string VendorName,
                string VendorAddress,
                string PoNo,
                string InvoiceFileName,
                byte[] InvoiceFileBytes,
                decimal AmountToBeReleased,
                int PaymentTypeID,
                string InvoiceNo,
                string InvoiceDate,
                string BankName,
                string BankBranch,
                string SwiftCode,
                string BankAccountNo,
                string CutOffDate,
                string Remarks,
                int CreatedBy
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbFin].[sp_add_update_fc_vendor_payment]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@FCVendorPaymentPID", FCVendorPaymentPID);
            cmd.Parameters.AddWithValue("@JobNo", JobNo);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@VendorAddress", VendorAddress);
            cmd.Parameters.AddWithValue("@PoNo", PoNo);


            if (InvoiceFileBytes != null)
            {
                cmd.Parameters.AddWithValue("@InvoiceFileName", InvoiceFileName);
                cmd.Parameters.AddWithValue("@InvoiceFileBytes", InvoiceFileBytes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@InvoiceFileBytes", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@InvoiceFileName", "");
            }

            cmd.Parameters.AddWithValue("@AmountToBeReleased", AmountToBeReleased);
            cmd.Parameters.AddWithValue("@PaymentTypeID", PaymentTypeID);
            cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            cmd.Parameters.AddWithValue("@InvoiceDate", InvoiceDate);
            cmd.Parameters.AddWithValue("@BankName", BankName);
            cmd.Parameters.AddWithValue("@BankBranch", BankBranch);
            cmd.Parameters.AddWithValue("@SwiftCode", SwiftCode);
            cmd.Parameters.AddWithValue("@BankAccountNo", BankAccountNo);
            cmd.Parameters.AddWithValue("@CutOffDate", CutOffDate);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);


            cmd.Parameters.Add(rCode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                string value = Convert.ToString(rCode.Value);
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


        public string CancelVendorPaymentRequest
            (
                int FCVendorPaymentPID,
                string Remarks,
                int CreatedBy
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbFin].[sp_cancel_fc_vendor_payment]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@FCVendorPaymentPID", FCVendorPaymentPID);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);


            cmd.Parameters.Add(rCode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                string value = Convert.ToString(rCode.Value);
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


        public DataSet GetSignatories()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_payment_signatories]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorPaymentList(
                    string fromDate
                , string toDate
                , string poNo
                , string vendorName
                , string vendorCode
                , int statusID
                , string requestNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_payment_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.Parameters.AddWithValue("@PONo", poNo);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@StatusID", statusID);
            cmd.Parameters.AddWithValue("@RequestNo", requestNo);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorPaymentDetailsForMail(int PID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_payment_details_for_mail]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", PID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorPaymentDetailsForPDF(int PID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_payment_details_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", PID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorPaymentDetailsByPID(int PID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_payment_by_id]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", PID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateVendorPaymentMailStatus(int PID, int stausID)
        {
            {
                string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
                SqlConnection con = new SqlConnection(cipltmsconnectionstring);
                SqlCommand cmd = new SqlCommand();
                SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.CommandText = "[dbFin].[sp_udpate_mail_status]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@PID", PID);
                cmd.Parameters.AddWithValue("@StatusID", stausID);
                
                cmd.Parameters.Add(rCode);
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    int value = Convert.ToInt32(rCode.Value);
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

        public int UpdatePaymentRequestSatus
            (
                    int PID
                ,   int stausID
                ,   string remarks
                ,   string BankAdvice
                ,   string DateOfPaymentReleased
                ,   string PaymentFileName
                ,   byte[] PaymentFileBytes
                ,   int createdBy)
        {
            //[dbFin].[sp_udpate_mail_status]
            {
                string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
                SqlConnection con = new SqlConnection(cipltmsconnectionstring);
                SqlCommand cmd = new SqlCommand();
                SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.CommandText = "[dbFin].[sp_update_vendor_payment_status]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@PID", PID);
                cmd.Parameters.AddWithValue("@StatusID", stausID);
                cmd.Parameters.AddWithValue("@Remarks", remarks);
                cmd.Parameters.AddWithValue("@BankAdvice", BankAdvice);
                cmd.Parameters.AddWithValue("@DateOfPaymentReleased", DateOfPaymentReleased);

                if (PaymentFileBytes != null)
                {
                    cmd.Parameters.AddWithValue("@PaymentFileName", PaymentFileName);
                    cmd.Parameters.AddWithValue("@PaymentFileBytes", PaymentFileBytes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PaymentFileBytes", System.Data.SqlTypes.SqlBinary.Null);
                    cmd.Parameters.AddWithValue("@PaymentFileName", "");
                }


                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                cmd.Parameters.Add(rCode);
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    int value = Convert.ToInt32(rCode.Value);
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

        public DataSet GetFiles(int PID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbFin].[sp_get_vendor_payment_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", PID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

    }
}
