using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class VouchersAuthorization
    {

        public DataSet GetUnits()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_units]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVouchersType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_types]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVouchersStatusList(int IncludePending)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_status]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@IncludePending", IncludePending);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVouchersCreatedByList(int TypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vouchers_created_by_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@TypeId", TypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



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

        public int AddUpdateDirectoryPath(int Pid, int typeId, int empRecordId, string filePath, int CreatedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_add_update_voucher_directory_path]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeID", typeId);
            cmd.Parameters.AddWithValue("@EmpRecordID", empRecordId);
            cmd.Parameters.AddWithValue("@DirectoryPath", filePath);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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

        public int UpdateDirectoryPath(int pid, string directoryPath, int createdById)
        {
            throw new NotImplementedException();
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


        public DataSet GetVoucherDOCS(int Pid, int TypeID, string ChallanNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);
            cmd.Parameters.AddWithValue("@TypeID", TypeID);
            cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVoucherDOCSByVoucherNo(string VoucherNo, int TypeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_docs_by_voucher_no]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@TypeId", TypeID);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVoucherDirectoryPathList(int typeIDToS, int empRecordIDToS, string directoryPathToS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_directory_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@TypeID", typeIDToS);
            cmd.Parameters.AddWithValue("@EmpRecordID", empRecordIDToS);
            cmd.Parameters.AddWithValue("@Path", directoryPathToS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVoucherDOCFiles(int Pid, int TypeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_doc_files]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);
            cmd.Parameters.AddWithValue("@TypeID", TypeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVoucherDetailsForPDF(int Pid, int TypeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVoucherDetailsForEmail(int Pid, int TypeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vouchers_for_email]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVoucherTypesForDirectoryPath(string fUser, int loadAll)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_types_for_directory_path]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@Fuser", fUser);
            cmd.Parameters.AddWithValue("@LoadAll", loadAll);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetUsersForDirectoryPath(int userID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_voucher_users_for_directory_path]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@userID", userID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAttachedPoDocumentFile(int pid, int typeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_attached_voucher_docs]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PId", pid);
            cmd.Parameters.AddWithValue("@TypeId", typeId);


            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetJournalVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_journal_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJournalVouchersListToAutorize
            (
              string startDate
            , string endDate
            , int UnitId
            , int StatusId
            , string voucherNo
            , int createdBy
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_journal_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@UnitID", UnitId);
            cmd.Parameters.AddWithValue("@StatusID", StatusId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJournalVouchersListToApprove
           (
             string startDate
           , string endDate
           , string voucherNo
           , int statusId
           , int unitId
           , string voucherCreatedBy
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_journal_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJournalVouchersReport
           (
             string startDate
           , string endDate
           , int unitId
           , int statusId
           , string voucherNo
           , string voucherCreatedBy
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_journal_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetJournalVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_journal_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeJournalVoucher
           (
               int Pid,
               string VoucherNo,
               string VoucherDate,
               int unitId,
               string VoucherCreatedBy,
               string AuthorizedRemarks,

               string FileName1,
               byte[] FileBytes1,

               string FileName2,
               byte[] FileBytes2,

               string FileName3,
               byte[] FileBytes3,

               string FileName4,
               byte[] FileBytes4,

               string FileName5,
               byte[] FileBytes5,

               int CreatedBy,

               DataTable dtVoucherDetails
           )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_journal_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            //cmd.Parameters.AddWithValue("@TypeFid", TypeFid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }


            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }


            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }


            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }


            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetPurchaseVouchersReport(string dateSignS, string dateTypeS, string startDateS,
                                                 string endDateS, int unitIdS, int statusIdS, string voucherCreatedByS,
                                                 string searchByS, string searchTextS, string vendorCodeS, string vendorNameS,
                                                 string amountSignS, decimal amountOneS, decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet BindUserDirectoryPath(int createdById, int typeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_user_voucher_directory_path]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@CreatedBy", createdById);
            cmd.Parameters.AddWithValue("@TypeID", typeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public DataSet GetMRNVouchersReport(string startDate, string endDate, int unitId, int statusId, string voucherNumber, string voucherCreatedBy)
        {
            {
                DataSet ds = new DataSet();
                string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
                SqlConnection con = new SqlConnection(cipltmsconnectionstring);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "[dbVoucher].[sp_get_mrn_vouchers_report]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@UnitId", unitId);
                cmd.Parameters.AddWithValue("@StatusId", statusId);
                cmd.Parameters.AddWithValue("@VoucherNo", voucherNumber);
                cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

                da.Fill(ds);
                if (ds != null)
                    return ds;

                return null;
            }
        }

        public int ApproveVoucher
            (
                int Pid,
                int TypeId,
                int StatusId,
                bool AddNewDOCFlag,
                string Remarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_approve_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@StatusId", StatusId);
            cmd.Parameters.AddWithValue("@AddNewDOCFlag", AddNewDOCFlag);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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


        public int SendVoucherToReauthorization
           (
               int Pid,
               int TypeId,
               bool AddNewDOCFlag,
               string Remarks,

               string FileName1,
               byte[] FileBytes1,

               string FileName2,
               byte[] FileBytes2,

               string FileName3,
               byte[] FileBytes3,

               string FileName4,
               byte[] FileBytes4,

               string FileName5,
               byte[] FileBytes5,

               int CreatedBy
           )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_send_voucher_to_reauthorization]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@AddNewDOCFlag", AddNewDOCFlag);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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


        public int ReauthorizeVoucher
           (
               int Pid,
               int TypeId,
               string Remarks,

               string FileName1,
               byte[] FileBytes1,

               string FileName2,
               byte[] FileBytes2,

               string FileName3,
               byte[] FileBytes3,

               string FileName4,
               byte[] FileBytes4,

               string FileName5,
               byte[] FileBytes5,

               int CreatedBy
           )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_reauthorize_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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


        public int RemoveVoucherDocuments(int Pid, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@Rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_remove_voucher_doc]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@PID", Pid);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

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




        //public DataSet GetPurchaseVouchersListToAutorize
        //    (
        //      string startDate
        //    , string endDate
        //    , int UnitId
        //    , string voucherNo
        //    , int createdBy
        //    )
        //{
        //    DataSet ds = new DataSet();
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "[dbVoucher].[sp_get_purchase_vouchers_list_to_authorize]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    cmd.Parameters.AddWithValue("@StartDate", startDate);
        //    cmd.Parameters.AddWithValue("@EndDate", endDate);
        //    cmd.Parameters.AddWithValue("@UnitID", UnitId);
        //    cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
        //    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

        //    da.Fill(ds);
        //    if (ds != null)
        //        return ds;

        //    return null;
        //}


        public DataSet GetPurchaseVouchersListToAutorize(
                string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , int statusToS
            , int CreatedByIdS
            , string searchByS
            , string searchTextS
            , string vendorCodeS
            , string vendorNameS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        //public DataSet GetPurchaseVouchersListToApprove
        //  (
        //    string startDate
        //  , string endDate
        //  , string voucherNo
        //  , string voucherCreatedBy
        //  )
        //{
        //    DataSet ds = new DataSet();
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "[dbVoucher].[sp_get_purchase_vouchers_list_to_approve]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    cmd.Parameters.AddWithValue("@StartDate", startDate);
        //    cmd.Parameters.AddWithValue("@EndDate", endDate);
        //    cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
        //    cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

        //    da.Fill(ds);
        //    if (ds != null)
        //        return ds;

        //    return null;
        //}


        public DataSet GetPurchaseVouchersListToApprove(
                                          string dateSignS
                                        , string dateTypeS
                                        , string startDateS
                                        , string endDateS
                                        , string WarehouseIdS
                                        , int unitIdS
                                        , int statusIdS
                                        , string voucherCreatedByS
                                        , string searchByS
                                        , string searchTextS
                                        , string vendorCodeS
                                        , string vendorNameS
                                        , string amountSignS
                                        , decimal amountOneS
                                        , decimal amountTwoS
                                )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int AuthorizePurchaseVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                int unitId,

                string ChallanNo,
                string ChallanDate,
                string OANo,
                string OADate,
                string DOCClass,
                string VendorCode,
                string VendorName,
                decimal NetAmount,

                string CurrencyDesc,
                decimal CurrencyRate,

                decimal INRBasicAmount,
                decimal INROtherAmount,
                decimal FCBasicAmount,
                 decimal FCOtherAmount,

                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                //string FileName6,
                //byte[] FileBytes6,

                //string FileName7,
                //byte[] FileBytes7,

                //string FileName8,
                //byte[] FileBytes8,

                //string FileName9,
                //byte[] FileBytes9,

                //string FileName10,
                //byte[] FileBytes10,

                int CreatedBy,

                DataTable dtVoucherDetails,
                int ActId
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_purchase_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@UnitId", unitId);

            cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);
            cmd.Parameters.AddWithValue("@ChallanDate", ChallanDate);
            cmd.Parameters.AddWithValue("@OANo", OANo);
            cmd.Parameters.AddWithValue("@OADate", OADate);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@NetAmount", NetAmount);

            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);

            cmd.Parameters.AddWithValue("@INRBasicAmount", INRBasicAmount);
            cmd.Parameters.AddWithValue("@INROtherAmount", INROtherAmount);
            cmd.Parameters.AddWithValue("@FCBasicAmount", FCBasicAmount);
            cmd.Parameters.AddWithValue("@FCOtherAmount", FCOtherAmount);

            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            //cmd.Parameters.AddWithValue("@ActId", ActId);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }




            //if (FileBytes6 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName6", FileName6);
            //    cmd.Parameters.AddWithValue("@FileBytes6", FileBytes6);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes6", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName6", "");
            //}

            //if (FileBytes7 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName7", FileName7);
            //    cmd.Parameters.AddWithValue("@FileBytes7", FileBytes7);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes7", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName7", "");
            //}

            //if (FileBytes8 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName8", FileName8);
            //    cmd.Parameters.AddWithValue("@FileBytes8", FileBytes8);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes8", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName8", "");
            //}

            //if (FileBytes9 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName9", FileName9);
            //    cmd.Parameters.AddWithValue("@FileBytes9", FileBytes9);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes9", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName9", "");
            //}

            //if (FileBytes10 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName10", FileName10);
            //    cmd.Parameters.AddWithValue("@FileBytes10", FileBytes10);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes10", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName10", "");
            //}


            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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

        //public int ApprovePurchaseVoucher
        //    (
        //        int Pid,
        //        int TypeId,
        //        bool AddNewDOCFlag,
        //        string Remarks,

        //        string FileName1,
        //        byte[] FileBytes1,

        //        string FileName2,
        //        byte[] FileBytes2,

        //        string FileName3,
        //        byte[] FileBytes3,

        //        string FileName4,
        //        byte[] FileBytes4,

        //        string FileName5,
        //        byte[] FileBytes5,

        //        //string FileName6,
        //        //byte[] FileBytes6,

        //        //string FileName7,
        //        //byte[] FileBytes7,

        //        //string FileName8,
        //        //byte[] FileBytes8,

        //        //string FileName9,
        //        //byte[] FileBytes9,

        //        //string FileName10,
        //        //byte[] FileBytes10,

        //        int CreatedBy
        //    )

        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "[dbVoucher].[sp_approve_purchase_voucher]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;

        //    cmd.Parameters.AddWithValue("@Pid", Pid);
        //    cmd.Parameters.AddWithValue("@TypeId", TypeId);
        //    cmd.Parameters.AddWithValue("@AddNewDOCFlag", AddNewDOCFlag);
        //    cmd.Parameters.AddWithValue("@Remarks", Remarks);

        //    if (FileBytes1 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName1", FileName1);
        //        cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName1", "");
        //    }

        //    if (FileBytes2 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName2", FileName2);
        //        cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName2", "");
        //    }

        //    if (FileBytes3 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName3", FileName3);
        //        cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName3", "");
        //    }

        //    if (FileBytes4 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName4", FileName4);
        //        cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName4", "");
        //    }

        //    if (FileBytes5 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName5", FileName5);
        //        cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName5", "");
        //    }


        //    //if (FileBytes6 != null)
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileName6", FileName6);
        //    //    cmd.Parameters.AddWithValue("@FileBytes6", FileBytes6);
        //    //}
        //    //else
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileBytes6", System.Data.SqlTypes.SqlBinary.Null);
        //    //    cmd.Parameters.AddWithValue("@FileName6", "");
        //    //}

        //    //if (FileBytes7 != null)
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileName7", FileName7);
        //    //    cmd.Parameters.AddWithValue("@FileBytes7", FileBytes7);
        //    //}
        //    //else
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileBytes7", System.Data.SqlTypes.SqlBinary.Null);
        //    //    cmd.Parameters.AddWithValue("@FileName7", "");
        //    //}

        //    //if (FileBytes8 != null)
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileName8", FileName8);
        //    //    cmd.Parameters.AddWithValue("@FileBytes8", FileBytes8);
        //    //}
        //    //else
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileBytes8", System.Data.SqlTypes.SqlBinary.Null);
        //    //    cmd.Parameters.AddWithValue("@FileName8", "");
        //    //}

        //    //if (FileBytes9 != null)
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileName9", FileName9);
        //    //    cmd.Parameters.AddWithValue("@FileBytes9", FileBytes9);
        //    //}
        //    //else
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileBytes9", System.Data.SqlTypes.SqlBinary.Null);
        //    //    cmd.Parameters.AddWithValue("@FileName9", "");
        //    //}

        //    //if (FileBytes10 != null)
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileName10", FileName10);
        //    //    cmd.Parameters.AddWithValue("@FileBytes10", FileBytes10);
        //    //}
        //    //else
        //    //{
        //    //    cmd.Parameters.AddWithValue("@FileBytes10", System.Data.SqlTypes.SqlBinary.Null);
        //    //    cmd.Parameters.AddWithValue("@FileName10", "");
        //    //}

        //    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

        //    cmd.Parameters.Add(rCode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rCode.Value);
        //        con.Close();
        //        con.Dispose();
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //}


        public DataSet GetPurchaseVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPurchaseVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public DataSet GetMRNVouchersListToApprove(string startDate
                                                , string endDate
                                                , int statusId
                                                , string WarehouseId
                                                , int unitId
                                                , string voucherNumber
                                                , string voucherCreatedBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_mrn_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseId);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNumber);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRNVouchersListToAutorize(string startDate
                        , string endDate
                        , string WarehouseId
                        , int UnitId
                        , int StatusId
                        , string voucherNo
                        , int createdBy
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_mrn_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseId);
            cmd.Parameters.AddWithValue("@UnitID", UnitId);
            cmd.Parameters.AddWithValue("@StatusID", StatusId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRNVouchersListToAttachAdditionalDocs(string startDate
                                                                , string endDate
                                                               , int statusId
                                                               , string WarehouseId
                                                               , int unitId
                                                               , string voucherNumber
                                                               , int createdBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_mrn_vouchers_list_for_additional_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseId);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNumber);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AttachMRNAdditionalDocs
            (
                int Pid,
                int TypeId,
                int IsMRNClosed,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_attach_mrn_additional_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@IsMRNClosed", IsMRNClosed);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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



        public DataSet GetMRNVoucherDetails(string voucherNo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_mrn_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMRNVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_mrn_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AuthorizeMRNVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,

                string PurchaseOrderDate,
                string VendorCode,
                string VendorName,
                string DOCClass,
                decimal NetAmount,

                string CurrencyDesc,
                decimal CurrencyRate,

                decimal INRBasicAmount,
                decimal INROtherAmount,
                decimal FCBasicAmount,
                 decimal FCOtherAmount,

                string GateEntryNo,
                string GateEntryDate,

                int unitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,
                int IsMRNClosedToAS,
                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,

                DataTable dtVoucherDetails
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_mrn_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@OADate", PurchaseOrderDate);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);

            cmd.Parameters.AddWithValue("@NetAmount", NetAmount);

            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);

            cmd.Parameters.AddWithValue("@INRBasicAmount", INRBasicAmount);
            cmd.Parameters.AddWithValue("@INROtherAmount", INROtherAmount);
            cmd.Parameters.AddWithValue("@FCBasicAmount", FCBasicAmount);
            cmd.Parameters.AddWithValue("@FCOtherAmount", FCOtherAmount);

            cmd.Parameters.AddWithValue("@GateEntryNo", GateEntryNo);
            cmd.Parameters.AddWithValue("@GateEntryDate", GateEntryDate);

            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);
            cmd.Parameters.AddWithValue("@IsMRNClosed", IsMRNClosedToAS);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }


            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }


            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }


            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }


            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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



        public DataSet GetServiceVouchersListToAutorize(
               string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           , int unitIdS
           , int statusToS
           , int CreatedByIdS
           , string searchByS
           , string searchTextS
           , string vendorCodeS
           , string vendorNameS
           , string amountSignS
           , decimal amountOneS
           , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_service_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetServiceVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , string WarehouseIdS
                                            , int unitIdS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                            , string vendorCodeS
                                            , string vendorNameS
                                            , string amountSignS
                                            , decimal amountOneS
                                            , decimal amountTwoS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_service_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeIIVoucher(int pid, string voucherNoToAS, string v, string classToAS, string currencyDescToAS, decimal receiptAmountFCToAS, decimal paymentAmountFCToAS, decimal receiptAmountINRToAS, decimal paymentAmountINRToAS, int unitIdToAS, string voucherCreatedByToAS, string remarksToAS, string fileNameToAS1, byte[] fileBytesToAS1, string fileNameToAS2, byte[] fileBytesToAS2, string fileNameToAS3, byte[] fileBytesToAS3, string fileNameToAS4, byte[] fileBytesToAS4, string fileNameToAS5, byte[] fileBytesToAS5, int createdById, DataTable dtVoucherDetails)
        {
            throw new NotImplementedException();
        }

        public int AuthorizeServiceVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                int unitId,

                string ChallanNo,
                string ChallanDate,
                string OANo,
                string OADate,
                string DOCClass,
                string VendorCode,
                string VendorName,
                decimal NetAmount,

                string CurrencyDesc,
                decimal CurrencyRate,

                decimal INRBasicAmount,
                decimal INROtherAmount,
                decimal FCBasicAmount,
                 decimal FCOtherAmount,

                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                //string FileName6,
                //byte[] FileBytes6,

                //string FileName7,
                //byte[] FileBytes7,

                //string FileName8,
                //byte[] FileBytes8,

                //string FileName9,
                //byte[] FileBytes9,

                //string FileName10,
                //byte[] FileBytes10,

                int CreatedBy,

                DataTable dtVoucherDetails,
                int ActId
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_service_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@UnitId", unitId);

            cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);
            cmd.Parameters.AddWithValue("@ChallanDate", ChallanDate);
            cmd.Parameters.AddWithValue("@OANo", OANo);
            cmd.Parameters.AddWithValue("@OADate", OADate);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@NetAmount", NetAmount);

            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);

            cmd.Parameters.AddWithValue("@INRBasicAmount", INRBasicAmount);
            cmd.Parameters.AddWithValue("@INROtherAmount", INROtherAmount);
            cmd.Parameters.AddWithValue("@FCBasicAmount", FCBasicAmount);
            cmd.Parameters.AddWithValue("@FCOtherAmount", FCOtherAmount);

            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            //cmd.Parameters.AddWithValue("@ActId", ActId);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }




            //if (FileBytes6 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName6", FileName6);
            //    cmd.Parameters.AddWithValue("@FileBytes6", FileBytes6);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes6", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName6", "");
            //}

            //if (FileBytes7 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName7", FileName7);
            //    cmd.Parameters.AddWithValue("@FileBytes7", FileBytes7);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes7", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName7", "");
            //}

            //if (FileBytes8 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName8", FileName8);
            //    cmd.Parameters.AddWithValue("@FileBytes8", FileBytes8);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes8", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName8", "");
            //}

            //if (FileBytes9 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName9", FileName9);
            //    cmd.Parameters.AddWithValue("@FileBytes9", FileBytes9);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes9", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName9", "");
            //}

            //if (FileBytes10 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName10", FileName10);
            //    cmd.Parameters.AddWithValue("@FileBytes10", FileBytes10);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes10", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName10", "");
            //}


            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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



        public int ApproveServiceVoucher
            (
                int Pid,
                int TypeId,
                bool AddNewDOCFlag,
                string Remarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                //string FileName6,
                //byte[] FileBytes6,

                //string FileName7,
                //byte[] FileBytes7,

                //string FileName8,
                //byte[] FileBytes8,

                //string FileName9,
                //byte[] FileBytes9,

                //string FileName10,
                //byte[] FileBytes10,

                int CreatedBy
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_approve_service_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@AddNewDOCFlag", AddNewDOCFlag);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }


            //if (FileBytes6 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName6", FileName6);
            //    cmd.Parameters.AddWithValue("@FileBytes6", FileBytes6);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes6", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName6", "");
            //}

            //if (FileBytes7 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName7", FileName7);
            //    cmd.Parameters.AddWithValue("@FileBytes7", FileBytes7);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes7", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName7", "");
            //}

            //if (FileBytes8 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName8", FileName8);
            //    cmd.Parameters.AddWithValue("@FileBytes8", FileBytes8);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes8", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName8", "");
            //}

            //if (FileBytes9 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName9", FileName9);
            //    cmd.Parameters.AddWithValue("@FileBytes9", FileBytes9);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes9", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName9", "");
            //}

            //if (FileBytes10 != null)
            //{
            //    cmd.Parameters.AddWithValue("@FileName10", FileName10);
            //    cmd.Parameters.AddWithValue("@FileBytes10", FileBytes10);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FileBytes10", System.Data.SqlTypes.SqlBinary.Null);
            //    cmd.Parameters.AddWithValue("@FileName10", "");
            //}

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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


        public DataSet GetServiceVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_service_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetServiceVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_service_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetServiceVouchersReport(string dateSignS, string dateTypeS, string startDateS,
                                                string endDateS, int unitIdS, int statusIdS, string voucherCreatedByS,
                                                string searchByS, string searchTextS, string vendorCodeS, string vendorNameS,
                                                string amountSignS, decimal amountOneS, decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_service_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }






        //Sale order vouchers


        public DataSet GetSaleOrderVouchersListToAutorize(
                              string dateSignS
                            , string dateTypeS
                            , string startDateS
                            , string endDateS
                            , string warehouseIdS
                            , int unitIdS
                            , int statusIdS
                            , int createdByIdS
                            , string searchByS
                            , string searchTextS
                            , string customerCodeS
                            , string customerNameS
                            , string amountSignS
                            , decimal amountOneS
                            , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_order_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetSaleOrderVoucherDetails(string voucherNo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_order_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleOrderVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_order_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeSaleOrderVoucher(int pid
            , string voucherNo
            , string voucherDate
            , int unitId
            , string yoNo
            , string yoDate
            , string Docclass
            , string customerCode
            , string customerName
            , decimal currencyBasic
            , decimal currencyVal
            , string currencyDesc
            , decimal currencyRate
            , decimal iNRBasicAmount
            , decimal iNROtherAmount
            , decimal fCBasicAmount
            , decimal fCOtherAmount
            , string voucherCreatedBy
            , string remarksToAS
            , string fileNameToAS1
            , byte[] fileBytesToAS1
            , string fileNameToAS2
            , byte[] fileBytesToAS2
            , string fileNameToAS3
            , byte[] fileBytesToAS3
            , string fileNameToAS4
            , byte[] fileBytesToAS4
            , string fileNameToAS5
            , byte[] fileBytesToAS5
            , int createdById
            , DataTable dtVoucherDetails
            , int actId)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_sale_order_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", pid);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", voucherDate);
            cmd.Parameters.AddWithValue("@UnitId", unitId);

            cmd.Parameters.AddWithValue("@YoNo", yoNo);
            cmd.Parameters.AddWithValue("@YoDate", yoDate);
            cmd.Parameters.AddWithValue("@DOCClass", Docclass);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
            cmd.Parameters.AddWithValue("@CustomerName", customerName);

            cmd.Parameters.AddWithValue("@CurrBasic", currencyBasic);
            cmd.Parameters.AddWithValue("@CurrVal", currencyVal);

            cmd.Parameters.AddWithValue("@CurrencyDesc", currencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", currencyRate);

            cmd.Parameters.AddWithValue("@INRBasicAmount", iNRBasicAmount);
            cmd.Parameters.AddWithValue("@INROtherAmount", iNROtherAmount);
            cmd.Parameters.AddWithValue("@FCBasicAmount", fCBasicAmount);
            cmd.Parameters.AddWithValue("@FCOtherAmount", fCOtherAmount);

            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", remarksToAS);

            //cmd.Parameters.AddWithValue("@ActId", ActId);

            if (fileBytesToAS1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", fileNameToAS1);
                cmd.Parameters.AddWithValue("@FileBytes1", fileBytesToAS1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (fileBytesToAS2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", fileNameToAS2);
                cmd.Parameters.AddWithValue("@FileBytes2", fileBytesToAS2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (fileBytesToAS3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", fileNameToAS3);
                cmd.Parameters.AddWithValue("@FileBytes3", fileBytesToAS3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (fileBytesToAS4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", fileNameToAS4);
                cmd.Parameters.AddWithValue("@FileBytes4", fileBytesToAS4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (fileBytesToAS5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", fileNameToAS5);
                cmd.Parameters.AddWithValue("@FileBytes5", fileBytesToAS5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", createdById);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetSaleOrderVouchersListToApprove(
                                        string dateSignS
                                      , string dateTypeS
                                      , string startDateS
                                      , string endDateS
                                      , string WarehouseIdS
                                      , int unitIdS
                                      , int statusIdS
                                      , string voucherCreatedByS
                                      , string searchByS
                                      , string searchTextS
                                      , string customerCodeS
                                      , string customerNameS
                                      , string amountSignS
                                      , decimal amountOneS
                                      , decimal amountTwoS
                              )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_order_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetSaleOrderVouchersReport(string dateSignS, string dateTypeS, string startDateS, string endDateS, int unitIdS
                                                , int statusIdS, string voucherCreatedByS, string searchByS, string searchTextS, string vendorCodeS
                                                , string vendorNameS, string amountSignS, decimal amountOneS, decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_order_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int BulkAuthorizeJournalVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_journal_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);


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

        public int BulkAuthorizeMRNVoucher(int CreatedBy, int IsMRNClosed, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_mrn_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@IsMRNClosed", IsMRNClosed);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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

        public int BulkAuthorizePurchaseVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_purchase_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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

        public int BulkAuthorizeSaleOrderVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_sale_order_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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

        public int BulkAuthorizeServiceVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_service_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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

        public int BulkApproveVoucher(int voucherTypeId, DataTable dtVoucherPID, int createdById)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_approve_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@TypeId", voucherTypeId);
            cmd.Parameters.AddWithValue("@VoucherPIDTable", dtVoucherPID);
            cmd.Parameters.AddWithValue("@CreatedBy", createdById);

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




        //SIV
        public DataSet GetSaleInvoiceVouchersListToApprove(
                                       string dateSignS
                                     , string dateTypeS
                                     , string startDateS
                                     , string endDateS
                                     , string WarehouseIdS
                                     , int unitIdS
                                     , int statusIdS
                                     , string voucherCreatedByS
                                     , string searchByS
                                     , string searchTextS
                                     , string customerCodeS
                                     , string customerNameS
                                     , string amountSignS
                                     , decimal amountOneS
                                     , decimal amountTwoS
                             )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_invoice_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleInvoiceVoucherDetails(string voucherNo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_invoice_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleInvoiceVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_invoice_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleInvoiceVouchersListToAutorize(
                             string dateSignS
                           , string dateTypeS
                           , string startDateS
                           , string endDateS
                           , string warehouseIdS
                           , int unitIdS
                           , int statusIdS
                           , int createdByIdS
                           , string searchByS
                           , string searchTextS
                           , string customerCodeS
                           , string customerNameS
                           , string amountSignS
                           , decimal amountOneS
                           , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_invoice_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeSaleInvoiceVoucher(
              int pidToAS
            , string voucherNo
            , string voucherDate
            , string dueDate
            , string challanNo
            , string challanDate
            , string oANo
            , string oADate
            , string cNNo
            , string cNDate
            , string yoNo
            , string yoDate
            , string eGPNo
            , string eGPDate
            , string customerCode
            , string customerName
            , string DocClass
            , decimal netAmount
            , decimal basic
            , string currencyDesc
            , decimal currencyRate
            , decimal currencyVal
            , decimal currencyBasic
            , decimal iNRBasicAmount
            , decimal iNROtherAmount
            , decimal fCBasicAmount
            , decimal fCOtherAmount
            , string voucherCreatedBy
            , int unitId
            , string remarksToAS

            , string fileNameToAS1
            , byte[] fileBytesToAS1
            , string fileNameToAS2
            , byte[] fileBytesToAS2
            , string fileNameToAS3
            , byte[] fileBytesToAS3
            , string fileNameToAS4
            , byte[] fileBytesToAS4
            , string fileNameToAS5
            , byte[] fileBytesToAS5
            , int createdById
            , DataTable dtVoucherDetails
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_sale_invoice_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", pidToAS);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", voucherDate);
            cmd.Parameters.AddWithValue("@DueDate", dueDate);
            cmd.Parameters.AddWithValue("@ChallanNo", challanNo);
            cmd.Parameters.AddWithValue("@ChallanDate", challanDate);
            cmd.Parameters.AddWithValue("@OANo", oANo);
            cmd.Parameters.AddWithValue("@OADate", oADate);
            cmd.Parameters.AddWithValue("@CNNo", cNNo);
            cmd.Parameters.AddWithValue("@CNDate", cNDate);
            cmd.Parameters.AddWithValue("@YoNo", yoNo);
            cmd.Parameters.AddWithValue("@YoDate", yoDate);
            cmd.Parameters.AddWithValue("@EGPNo", eGPNo);
            cmd.Parameters.AddWithValue("@EGPDate", eGPDate);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
            cmd.Parameters.AddWithValue("@CustomerName", customerName);
            cmd.Parameters.AddWithValue("@DOCClass", DocClass);
            cmd.Parameters.AddWithValue("@NetAmount", netAmount);
            cmd.Parameters.AddWithValue("@Basic", basic);
            cmd.Parameters.AddWithValue("@CurrencyDesc", currencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", currencyRate);
            cmd.Parameters.AddWithValue("@CurrVal", currencyVal);
            cmd.Parameters.AddWithValue("@CurrBasic", currencyBasic);
            cmd.Parameters.AddWithValue("@INRBasicAmount", iNRBasicAmount);
            cmd.Parameters.AddWithValue("@INROtherAmount", iNROtherAmount);
            cmd.Parameters.AddWithValue("@FCBasicAmount", fCBasicAmount);
            cmd.Parameters.AddWithValue("@FCOtherAmount", fCOtherAmount);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", remarksToAS);



            if (fileBytesToAS1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", fileNameToAS1);
                cmd.Parameters.AddWithValue("@FileBytes1", fileBytesToAS1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (fileBytesToAS2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", fileNameToAS2);
                cmd.Parameters.AddWithValue("@FileBytes2", fileBytesToAS2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (fileBytesToAS3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", fileNameToAS3);
                cmd.Parameters.AddWithValue("@FileBytes3", fileBytesToAS3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (fileBytesToAS4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", fileNameToAS4);
                cmd.Parameters.AddWithValue("@FileBytes4", fileBytesToAS4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (fileBytesToAS5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", fileNameToAS5);
                cmd.Parameters.AddWithValue("@FileBytes5", fileBytesToAS5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", createdById);
            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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

        public int BulkAuthorizeSaleInvoiceVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_sale_invoice_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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



        public DataSet GetSaleInvoiceVouchersReport(string dateSignS, string dateTypeS, string startDateS, string endDateS, int unitIdS
                                                , int statusIdS, string voucherCreatedByS, string searchByS, string searchTextS, string vendorCodeS
                                                , string vendorNameS, string amountSignS, decimal amountOneS, decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_invoice_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }






        //VRPV

        public DataSet GetVRPVouchersListToAutorize(
                       string dateSignS
                     , string dateTypeS
                     , string startDateS
                     , string endDateS
                     , string warehouseIdS
                     , int unitIdS
                     , int statusToS
                     , int createdByIdS
                     , string searchByS
                     , string searchTextS
                     , string vendorCodeS
                     , string vendorNameS
                     , string amountSearchByS
                     , string amountSignS
                     , decimal amountOneS
                     , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_receipt_payment_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", amountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetVRPVouchersListToApprove(
                  string dateSignS
                , string dateTypeS
                , string startDateS
                , string endDateS
                , string WarehouseIdS
                , int unitIdS
                , int statusIdS
                , string voucherCreatedByS
                , string searchByS
                , string searchTextS
                , string vendorCodeS
                , string vendorNameS
                , string amountSearchByS
                , string amountSignS
                , decimal amountOneS
                , decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_receipt_payment_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", amountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AuthorizeVRPVoucher
           (
               int Pid,
               string VoucherNo,
               string VoucherDate,
               string VendorCode,
               string VendorName,
               string DOCClass,
               decimal NetAmount,
               decimal CurrAmount,
               string CurrencyDesc,
               int unitId,
               string VoucherCreatedBy,
               string AuthorizedRemarks,

               string FileName1,
               byte[] FileBytes1,

               string FileName2,
               byte[] FileBytes2,

               string FileName3,
               byte[] FileBytes3,

               string FileName4,
               byte[] FileBytes4,

               string FileName5,
               byte[] FileBytes5,
               int CreatedBy,
               DataTable dtVoucherDetails
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_vendor_receipt_payment_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);


            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@NetAmount", NetAmount);
            cmd.Parameters.AddWithValue("@CurrAmount", CurrAmount);
            cmd.Parameters.AddWithValue("@CurrDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);

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


        public DataSet GetVRPVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_receipt_payment_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetVRPVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_receipt_payment_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVRPVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , int unitIdS
            , int statusIdS
            , string warehouseIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
            , string vendorCodeS
            , string vendorNameS
            , string AmountSearchByS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_receipt_payment_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", AmountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeVRPVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_vendor_receipt_payment_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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




        //CRPV

        public DataSet GetCRPVouchersListToAutorize(
                       string dateSignS
                     , string dateTypeS
                     , string startDateS
                     , string endDateS
                     , string warehouseIdS
                     , int unitIdS
                     , int statusToS
                     , int createdByIdS
                     , string searchByS
                     , string searchTextS
                     , string customerCodeS
                     , string customerNameS
                     , string amountSearchByS
                     , string amountSignS
                     , decimal amountOneS
                     , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_receipt_payment_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", amountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetCRPVouchersListToApprove(
                  string dateSignS
                , string dateTypeS
                , string startDateS
                , string endDateS
                , string WarehouseIdS
                , int unitIdS
                , int statusIdS
                , string voucherCreatedByS
                , string searchByS
                , string searchTextS
                , string customerCodeS
                , string customerNameS
                , string amountSearchByS
                , string amountSignS
                , decimal amountOneS
                , decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_receipt_payment_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", amountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AuthorizeCRPVoucher
           (
               int Pid,
               string VoucherNo,
               string VoucherDate,
               string CustomerCode,
               string CustomerName,
               string DOCClass,
               decimal NetAmount,
               decimal CurrAmount,
               string CurrencyDesc,
               int unitId,
               string VoucherCreatedBy,
               string AuthorizedRemarks,

               string FileName1,
               byte[] FileBytes1,

               string FileName2,
               byte[] FileBytes2,

               string FileName3,
               byte[] FileBytes3,

               string FileName4,
               byte[] FileBytes4,

               string FileName5,
               byte[] FileBytes5,
               int CreatedBy,
               DataTable dtVoucherDetails
            )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_customer_receipt_payment_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);


            cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@NetAmount", NetAmount);
            cmd.Parameters.AddWithValue("@CurrAmount", CurrAmount);
            cmd.Parameters.AddWithValue("@CurrDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);

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


        public DataSet GetCRPVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_receipt_payment_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetCRPVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_receipt_payment_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetCRPVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , int unitIdS
            , int statusIdS
            , string warehouseIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
            , string customerCodeS
            , string customerNameS
            , string AmountSearchByS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_receipt_payment_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", AmountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeCRPVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_customer_receipt_payment_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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



        //CBV

        public DataSet GetCBVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           //, string typeIdS
           , int unitIdS
           , int statusToS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           , string amountSearchByS
           , string amountSignS
           , decimal amountOneS
           , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_cash_bank_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            //cmd.Parameters.AddWithValue("@PTypeId", typeIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            cmd.Parameters.AddWithValue("@AmountSearchBy", amountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCBVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , string WarehouseIdS
                                            //, string TypeIdS
                                            , int unitIdS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                            //, string vendorCodeS
                                            //, string vendorNameS
                                            , string amountSearchByS
                                            , string amountSignS
                                            , decimal amountOneS
                                            , decimal amountTwoS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_cash_bank_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            //cmd.Parameters.AddWithValue("@PTypeId", TypeIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@AmountSearchBy", amountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeCBVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                // string TypeId,
                string DOCClass,
                string CurrencyDesc,
                //decimal Amount,
                decimal ReceiptAmountFC,
                decimal PaymentAmountFC,
                decimal ReceiptAmountINR,
                decimal PaymentAmountINR,
                //string PaiedToReceivedFrom,
                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_cash_bank_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);

            //cmd.Parameters.AddWithValue("@Type", TypeId);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            //cmd.Parameters.AddWithValue("@Amount", Amount);

            cmd.Parameters.AddWithValue("@ReceiptAmountFC", ReceiptAmountFC);
            cmd.Parameters.AddWithValue("@PaymentAmountFC", PaymentAmountFC);
            cmd.Parameters.AddWithValue("@ReceiptAmountINR", ReceiptAmountINR);
            cmd.Parameters.AddWithValue("@PaymentAmountINR", PaymentAmountINR);

            //cmd.Parameters.AddWithValue("@PaiedToReceivedFrom", PaiedToReceivedFrom);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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



        //public int ApproveCBVoucher
        //    (
        //        int Pid,
        //        int TypeId,
        //        bool AddNewDOCFlag,
        //        string Remarks,

        //        string FileName1,
        //        byte[] FileBytes1,

        //        string FileName2,
        //        byte[] FileBytes2,

        //        string FileName3,
        //        byte[] FileBytes3,

        //        string FileName4,
        //        byte[] FileBytes4,

        //        string FileName5,
        //        byte[] FileBytes5,

        //        int CreatedBy
        //    )

        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "[dbVoucher].[sp_approve_cash_bank_voucher]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;

        //    cmd.Parameters.AddWithValue("@Pid", Pid);
        //    cmd.Parameters.AddWithValue("@TypeId", TypeId);
        //    cmd.Parameters.AddWithValue("@AddNewDOCFlag", AddNewDOCFlag);
        //    cmd.Parameters.AddWithValue("@Remarks", Remarks);

        //    if (FileBytes1 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName1", FileName1);
        //        cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName1", "");
        //    }

        //    if (FileBytes2 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName2", FileName2);
        //        cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName2", "");
        //    }

        //    if (FileBytes3 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName3", FileName3);
        //        cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName3", "");
        //    }

        //    if (FileBytes4 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName4", FileName4);
        //        cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName4", "");
        //    }

        //    if (FileBytes5 != null)
        //    {
        //        cmd.Parameters.AddWithValue("@FileName5", FileName5);
        //        cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
        //        cmd.Parameters.AddWithValue("@FileName5", "");
        //    }

        //    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

        //    cmd.Parameters.Add(rCode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rCode.Value);
        //        con.Close();
        //        con.Dispose();
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //}


        public DataSet GetCBVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_cash_bank_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCBVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_cash_bank_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCBVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , int unitIdS
            , int statusIdS
            , string warehouseIdS
            //, string typeIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
            , string AmountSearchByS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_cash_bank_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            //cmd.Parameters.AddWithValue("@PTypeId", typeIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@AmountSearchBy", AmountSearchByS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeCBVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_cash_bank_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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












        //CV

        public DataSet GetCVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           , string typeIdS
           , int unitIdS
           , int statusToS
           , int CreatedByIdS
           , string searchByS
           , string searchTextS
           , string amountSignS
           , decimal amountOneS
           , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_contra_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@PTypeId", typeIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetCVouchersListToApprove(
                                             string dateSignS
                                           , string dateTypeS
                                           , string startDateS
                                           , string endDateS
                                           , string warehouseIdS
                                           , string typeIdS
                                           , int unitIdS
                                           , int statusIdS
                                           , string voucherCreatedByS
                                           , string searchByS
                                           , string searchTextS
                                           , string amountSignS
                                           , decimal amountOneS
                                           , decimal amountTwoS
                                   )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_contra_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@PTypeId", typeIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int AuthorizeCVoucher
            (
                 int Pid
               , string VoucherNo
               , string VoucherDate
               , string TypeId
               , string Class
               , string GLCode
               , string GLDescription
               , string CurrencyDesc
               , decimal Amount
               , decimal ReceiptAmountFC
               , decimal PaymentAmountFC
               , decimal ReceiptAmountINR
               , decimal PaymentAmountINR
               , string PaiedToReceivedFrom
               , string ChequeOrReferenceNo
               , string ChequeDated
               , string BankTransactionRefNoOrUTR
               , string ReconcileDate
               , string Narration
               , int UnitId
               , string VoucherCreatedBy
               , string Remarks

               , string FileName1
               , byte[] FileBytes1

               , string FileName2
               , byte[] FileBytes2

               , string FileName3
               , byte[] FileBytes3

               , string FileName4
               , byte[] FileBytes4

               , string FileName5
               , byte[] FileBytes5
               , int CreatedBy
               , DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_contra_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);


            cmd.Parameters.AddWithValue("@Type", TypeId);
            cmd.Parameters.AddWithValue("@DOCClass", Class);
            cmd.Parameters.AddWithValue("@GLCode", GLCode);
            cmd.Parameters.AddWithValue("@GLDescription", GLDescription);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@Amount", Amount);

            cmd.Parameters.AddWithValue("@ReceiptAmountFC", ReceiptAmountFC);
            cmd.Parameters.AddWithValue("@PaymentAmountFC", PaymentAmountFC);
            cmd.Parameters.AddWithValue("@ReceiptAmountINR", ReceiptAmountINR);
            cmd.Parameters.AddWithValue("@PaymentAmountINR", PaymentAmountINR);

            cmd.Parameters.AddWithValue("@PaiedToReceivedFrom", PaiedToReceivedFrom);
            cmd.Parameters.AddWithValue("@ChequeOrReferenceNo", ChequeOrReferenceNo);
            cmd.Parameters.AddWithValue("@ChequeDated", ChequeDated);
            cmd.Parameters.AddWithValue("@BankTransactionRefNoOrUTR", BankTransactionRefNoOrUTR);

            cmd.Parameters.AddWithValue("@ReconcileDate", ReconcileDate);
            cmd.Parameters.AddWithValue("@Narration", Narration);

            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", Remarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }


            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);

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



        public int BulkAuthorizeCVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_contra_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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

        public DataSet GetCVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_contra_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetCVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_contra_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCVouchersReport(
                string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , int unitIdS
            , int statusIdS
            , string warehouseIdS
            , string typeIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_contra_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@PTypeId", typeIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }






        //MR
        public DataSet GetMRVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_MR_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRVouchersListToAutorize
            (
              string startDate
            , string endDate
            , int UnitId
            , string WarehouseId
            , int StatusId
            , string voucherNo
            , int createdBy
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_MR_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseId);
            cmd.Parameters.AddWithValue("@UnitID", UnitId);
            cmd.Parameters.AddWithValue("@StatusID", StatusId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRVouchersListToApprove
           (
             string startDate
           , string endDate
           , string voucherNo
           , int statusId
           , int unitId
           , string WarehouseId
           , string voucherCreatedBy
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_MR_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseId);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMRVouchersReport
           (
             string startDate
           , string endDate
           , int unitId
           , string WarehouseId
           , int statusId
           , string voucherNo
           , string voucherCreatedBy
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_MR_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseId);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetMRVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_MR_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeMRVoucher
           (
               int Pid,
               string VoucherNo,
               string VoucherDate,
               int unitId,
               string DOCClass,
               string VoucherCreatedBy,
               string AuthorizedRemarks,

               string FileName1,
               byte[] FileBytes1,

               string FileName2,
               byte[] FileBytes2,

               string FileName3,
               byte[] FileBytes3,

               string FileName4,
               byte[] FileBytes4,

               string FileName5,
               byte[] FileBytes5,

               int CreatedBy,

               DataTable dtVoucherDetails
           )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_MR_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }


            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }


            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }


            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }


            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public int BulkAuthorizeMRVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_MR_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);


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
















        //Purchase Order Vouchers

        public DataSet GetPurchaseOrderVouchersReport(string dateSignS, string dateTypeS, string startDateS,
                                                string endDateS, int unitIdS, int statusIdS, string voucherCreatedByS,
                                                string searchByS, string searchTextS, string vendorCodeS, string vendorNameS,
                                                string amountSignS, decimal amountOneS, decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_Order_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public DataSet GetPurchaseOrderVouchersListToAutorize(
                        string dateSignS
                    , string dateTypeS
                    , string startDateS
                    , string endDateS
                    , string warehouseIdS
                    , int unitIdS
                    , int statusToS
                    , int CreatedByIdS
                    , string searchByS
                    , string searchTextS
                    , string vendorCodeS
                    , string vendorNameS
                    , string amountSignS
                    , decimal amountOneS
                    , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_Order_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetPurchaseOrderVouchersListToApprove(
                      string dateSignS
                    , string dateTypeS
                    , string startDateS
                    , string endDateS
                    , string WarehouseIdS
                    , int unitIdS
                    , int statusIdS
                    , string voucherCreatedByS
                    , string searchByS
                    , string searchTextS
                    , string vendorCodeS
                    , string vendorNameS
                    , string amountSignS
                    , decimal amountOneS
                    , decimal amountTwoS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_order_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AuthorizePurchaseOrderVoucher
        (
            int Pid,
            string VoucherNo,
            string VoucherDate,
            string VendorCode,
            string VendorName,
            string Particular,
            string DOCClass,
            string CurrencyDesc,
            decimal CurrencyRate,
            decimal INRAmount,
            decimal FCAmount,
            int UnitId,
            string VoucherCreatedBy,
            string AuthorizedRemarks,

            string FileName1,
            byte[] FileBytes1,

            string FileName2,
            byte[] FileBytes2,

            string FileName3,
            byte[] FileBytes3,

            string FileName4,
            byte[] FileBytes4,

            string FileName5,
            byte[] FileBytes5,

            int CreatedBy,

            DataTable dtVoucherDetails
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_purchase_Order_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@Particular", Particular);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@INRAmount", INRAmount);
            cmd.Parameters.AddWithValue("@FCAmount", FCAmount);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public int ApprovePurchaseOrderVoucher
                    (
                        int Pid,
                        int TypeId,
                        bool AddNewDOCFlag,
                        string Remarks,

                        string FileName1,
                        byte[] FileBytes1,

                        string FileName2,
                        byte[] FileBytes2,

                        string FileName3,
                        byte[] FileBytes3,

                        string FileName4,
                        byte[] FileBytes4,

                        string FileName5,
                        byte[] FileBytes5,

                        int CreatedBy
                    )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_approve_purchase_Order_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@AddNewDOCFlag", AddNewDOCFlag);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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


        public DataSet GetPurchaseOrderVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_Order_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPurchaseOrderVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_Order_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int BulkAuthorizePurchaseOrderVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_purchase_Order_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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















        //Inventory Issue Vouchers

        public DataSet GetIIVouchersReport(
                                  string dateSignS
                                , string dateTypeS
                                , string startDateS
                                , string endDateS
                                , int unitIdS
                                , int statusIdS
                                , string WarehouseIdS
                                , string voucherCreatedByS
                                , string searchByS
                                , string searchTextS
                            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_issue_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetIIVouchersListToAutorize(
                      string dateSignS
                    , string dateTypeS
                    , string startDateS
                    , string endDateS
                    , int unitIdS
                    , int statusIdS
                    , string WarehouseIdS
                    , string searchByS
                    , string searchTextS
                    , int createdByIdS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_issue_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetIIVouchersListToApprove(
                      string dateSignS
                    , string dateTypeS
                    , string startDateS
                    , string endDateS
                    , int unitIdS
                    , int statusIdS
                    , string WarehouseIdS
                    , string searchByS
                    , string searchTextS
                    , string voucherCreatedByS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_issue_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeIIVoucher
        (
            int Pid,
            string VoucherNo,
            string VoucherDate,
            string DOCClass,
            string Factor,
            int UnitId,
            string VoucherCreatedBy,
            string AuthorizedRemarks,

            string FileName1,
            byte[] FileBytes1,

            string FileName2,
            byte[] FileBytes2,

            string FileName3,
            byte[] FileBytes3,

            string FileName4,
            byte[] FileBytes4,

            string FileName5,
            byte[] FileBytes5,

            int CreatedBy,

            DataTable dtVoucherDetails
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_inventory_issue_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@Factor", Factor);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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

        public DataSet GetIIVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_issue_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetIIVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_issue_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int BulkAuthorizeIIVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_inventory_issue_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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


        public int BulkAuthorizeIIVoucherWithoutDOCS(int CreatedBy, DataTable dtVouchers)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_inventory_issue_voucher_without_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            
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














        //Inventory Return Vouchers

        public DataSet GetIRVouchersReport(
                                  string dateSignS
                                , string dateTypeS
                                , string startDateS
                                , string endDateS
                                , int unitIdS
                                , int statusIdS
                                , string WarehouseIdS
                                , string voucherCreatedByS
                                , string searchByS
                                , string searchTextS
                            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_return_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetIRVouchersListToAutorize(
                      string dateSignS
                    , string dateTypeS
                    , string startDateS
                    , string endDateS
                    , int unitIdS
                    , int statusIdS
                    , string WarehouseIdS
                    , string searchByS
                    , string searchTextS
                    , int createdByIdS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_return_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetIRVouchersListToApprove(
                      string dateSignS
                    , string dateTypeS
                    , string startDateS
                    , string endDateS
                    , int unitIdS
                    , int statusIdS
                    , string WarehouseIdS
                    , string searchByS
                    , string searchTextS
                    , string voucherCreatedByS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_return_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeIRVoucher
        (
            int Pid,
            string VoucherNo,
            string VoucherDate,
            string ReferenceVoucherNo,
            string ReferenceVoucherDate,
            string DOCClass,
            int UnitId,
            string VoucherCreatedBy,
            string AuthorizedRemarks,

            string FileName1,
            byte[] FileBytes1,

            string FileName2,
            byte[] FileBytes2,

            string FileName3,
            byte[] FileBytes3,

            string FileName4,
            byte[] FileBytes4,

            string FileName5,
            byte[] FileBytes5,

            int CreatedBy,

            DataTable dtVoucherDetails
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_inventory_return_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@ReferenceVoucherNo", ReferenceVoucherNo);
            cmd.Parameters.AddWithValue("@ReferenceVoucherDate", ReferenceVoucherDate);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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

        public DataSet GetIRVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_return_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetIRVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_inventory_return_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int BulkAuthorizeIRVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_inventory_return_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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

        public int BulkAuthorizeIRVoucherWithoutDOCS(int CreatedBy, DataTable dtVouchers)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_inventory_return_voucher_witout_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            
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




        //VDNV

        public DataSet GetVDNVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           , int unitIdS
           , string vendorCodeS
           , string vendorNameS
           , int statusIdS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_debit_note_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);


            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVDNVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , string warehouseIdS
                                            , int unitIdS
                                            , string vendorCodeS
                                            , string vendorNameS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_debit_note_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeVDNVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string VendorCode,
                string VendorName,
                string DOCClass,
                string CurrencyDesc,
                decimal CurrencyRate,
                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_vendor_debit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetVDNVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_debit_note_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVDNVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_debit_note_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVDNVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , string vendorCodeS
            , string vendorNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_debit_note_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeVDNVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_vendor_debit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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



        public int BulkAuthorizeVDNVoucherWithoutDOCS(int CreatedBy, DataTable dtVouchers)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_vendor_debit_note_voucher_without_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);

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




        //VCNV
        public DataSet GetVCNVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           , int unitIdS
           , string vendorCodeS
           , string vendorNameS
           , int statusIdS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_credit_note_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);


            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVCNVouchersListToApprove(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , string vendorCodeS
            , string vendorNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_credit_note_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeVCNVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string VendorCode,
                string VendorName,
                string DOCClass,
                string CurrencyDesc,
                decimal CurrencyRate,
                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_vendor_credit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetVCNVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_credit_note_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVCNVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_credit_note_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVCNVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , string vendorCodeS
            , string vendorNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_vendor_credit_note_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeVCNVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_vendor_credit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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









        //CDNV

        public DataSet GetCDNVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           , int unitIdS
           , string customerCodeS
           , string customerNameS
           , int statusIdS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_debit_note_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);


            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCDNVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , string warehouseIdS
                                            , int unitIdS
                                            , string customerCodeS
                                            , string customerNameS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_debit_note_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeCDNVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string CustomerCode,
                string CustomerName,
                string DOCClass,
                string CurrencyDesc,
                decimal CurrencyRate,
                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_customer_debit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetCDNVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_debit_note_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCDNVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_debit_note_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCDNVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , string customerCodeS
            , string customerNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_debit_note_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeCDNVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_customer_debit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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











        //CCNV
        public DataSet GetCCNVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string warehouseIdS
           , int unitIdS
           , string customerCodeS
           , string customerNameS
           , int statusIdS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_credit_note_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);


            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCCNVouchersListToApprove(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , string customerCodeS
            , string customerNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_credit_note_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeCCNVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string CustomerCode,
                string CustomerName,
                string DOCClass,
                string CurrencyDesc,
                decimal CurrencyRate,
                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_customer_credit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetCCNVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_credit_note_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCCNVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_credit_note_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCCNVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string warehouseIdS
            , int unitIdS
            , string customerCodeS
            , string customerNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_customer_credit_note_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeCCNVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_customer_credit_note_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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















        //STV

        public DataSet GetSTVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , string fromWarehouseIdS
           , string toWarehouseIdS
           , int unitIdS
           , string customerCodeS
           , string customerNameS
           , int statusIdS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_transfer_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@FromWarehouseId", fromWarehouseIdS);
            cmd.Parameters.AddWithValue("@ToWarehouseId", toWarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);


            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSTVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , string fromWarehouseIdS
                                            , string toWarehouseIdS
                                            , int unitIdS
                                            , string customerCodeS
                                            , string customerNameS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_transfer_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@FromWarehouseId", fromWarehouseIdS);
            cmd.Parameters.AddWithValue("@ToWarehouseId", toWarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeSTVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,


                string StockTransferOrderNo,
                string CustomerCode,
                string CustomerName,
                string FromClass,
                string ToClass,
                string FromWarehouseCode,
                string ToWarehouseCode,
                string RemarksParticular,

                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_stock_transfer_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@StockTransferOrderNo", StockTransferOrderNo);
            cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@FromDOCClass", FromClass);
            cmd.Parameters.AddWithValue("@ToDOCClass", ToClass);
            cmd.Parameters.AddWithValue("@FromWarehouseCode", FromWarehouseCode);
            cmd.Parameters.AddWithValue("@ToWarehouseCode", ToWarehouseCode);
            cmd.Parameters.AddWithValue("@Remarks", RemarksParticular);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetSTVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_transfer_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSTVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_transfer_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSTVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , string fromWarehouseIdS
            , string toWarehouseIdS
            , int unitIdS
            , string customerCodeS
            , string customerNameS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_transfer_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@FromWarehouseId", fromWarehouseIdS);
            cmd.Parameters.AddWithValue("@ToWarehouseId", toWarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeSTVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_stock_transfer_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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


        public int BulkAuthorizeSTVoucherWithoutDOCS(int CreatedBy, DataTable dtVouchers)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_stock_transfer_voucher_without_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);

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







        //SAV

        public DataSet GetSAVouchersListToAutorize(
             string dateSignS
           , string dateTypeS
           , string startDateS
           , string endDateS
           , int unitIdS
           , int statusIdS
           , int createdByIdS
           , string searchByS
           , string searchTextS
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_adjustment_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);

            cmd.Parameters.AddWithValue("@StatusID", statusIdS);

            cmd.Parameters.AddWithValue("@CreatedBy", createdByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSAVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , int unitIdS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_adjustment_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AuthorizeSAVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string Particular,

                int UnitId,
                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,
                DataTable dtVoucherDetails
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_stock_adjustment_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@Particular", Particular);
            cmd.Parameters.AddWithValue("@UnitId", UnitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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


        public DataSet GetSAVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_adjustment_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSAVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_adjustment_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSAVouchersReport(
              string dateSignS
            , string dateTypeS
            , string startDateS
            , string endDateS
            , int unitIdS
            , int statusIdS
            , string voucherCreatedByS
            , string searchByS
            , string searchTextS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_stock_adjustment_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeSAVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_stock_adjustment_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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


        public int BulkAuthorizeSAVoucherWithoutDOCS(int CreatedBy, DataTable dtVouchers)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_stock_adjustment_voucher_without_docs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);

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


        public DataSet GetFinanceAllEntriesReport
        (
              string dateSignS
            , string startDateS
            , string endDateS
            , int monthS
            , string voucherNoS
            , int voucherTypeIdS
            , string partyNameS
            , string GLCodeS
            , string GLDescriptionS
            , int unitIdS
            , int statusIdS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS
            , string voucherCreatedByS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_Finance_All_Entries_Report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@Month", monthS);

            cmd.Parameters.AddWithValue("@DocumentNumber", voucherNoS);
            cmd.Parameters.AddWithValue("@DocumentTypeId", voucherTypeIdS);

            cmd.Parameters.AddWithValue("@PartyName", partyNameS);
            cmd.Parameters.AddWithValue("@GLCode", GLCodeS);
            cmd.Parameters.AddWithValue("@GLDescription", GLDescriptionS);

            cmd.Parameters.AddWithValue("@UnitId", unitIdS);                                  
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@CreatedByS", voucherCreatedByS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetFinanceAllEntriesDetailedReport
        (
              string dateSignS
            , string startDateS
            , string endDateS
            , int monthS
            , string voucherNoS
            , int voucherTypeIdS
            , string partyNameS
            , string GLCodeS
            , string GLDescriptionS
            , string DOCClassS
            , string ProductCodeS
            , string ProductDescriptionS
            , int unitIdS
            , int statusIdS
            , string amountSignS
            , decimal amountOneS
            , decimal amountTwoS
            , string voucherCreatedByS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[dbVoucher].[sp_get_Finance_All_Entries_Detailed_Report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@Month", monthS);

            cmd.Parameters.AddWithValue("@DocumentNumber", voucherNoS);
            cmd.Parameters.AddWithValue("@DocumentTypeId", voucherTypeIdS);

            cmd.Parameters.AddWithValue("@PartyName", partyNameS);
            cmd.Parameters.AddWithValue("@GLCode", GLCodeS);
            cmd.Parameters.AddWithValue("@GLDescription", GLDescriptionS);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClassS);

            cmd.Parameters.AddWithValue("@ProductCode", ProductCodeS);
            cmd.Parameters.AddWithValue("@ProductDescription", ProductDescriptionS);

            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@CreatedByS", voucherCreatedByS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetFinanceAllEntriesReportVoucherDetails
        (
              int VoucherTypeId
            , int VoucherId
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_Finance_All_Entries_Report_Voucher_Details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherTypeId", VoucherTypeId);
            cmd.Parameters.AddWithValue("@VoucherId", VoucherId);
            
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }






        //PRV
        public DataSet GetPurchaseReturnVouchersListToAutorize(
            string dateSignS
          , string dateTypeS
          , string startDateS
          , string endDateS
          , string warehouseIdS
          , int unitIdS
          , int statusToS
          , int CreatedByIdS
          , string searchByS
          , string searchTextS
          , string vendorCodeS
          , string vendorNameS
          , string amountSignS
          , decimal amountOneS
          , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_return_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPurchaseReturnVouchersListToApprove(
                                              string dateSignS
                                            , string dateTypeS
                                            , string startDateS
                                            , string endDateS
                                            , string WarehouseIdS
                                            , int unitIdS
                                            , int statusIdS
                                            , string voucherCreatedByS
                                            , string searchByS
                                            , string searchTextS
                                            , string vendorCodeS
                                            , string vendorNameS
                                            , string amountSignS
                                            , decimal amountOneS
                                            , decimal amountTwoS
                                    )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_return_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

       

        public int AuthorizePurchaseReturnVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string ReferenceInvoiceNo,
                string ReferenceInvoiceDate,
                string VendorCode,
                string VendorName,
                string DOCClass,
                string CurrencyDesc,
                decimal CurrencyRate,
                decimal Amount,
                int unitId,

                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,

                DataTable dtVoucherDetails,
                int ActId
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_purchase_return_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@ReferenceInvoiceNo", ReferenceInvoiceNo);
            cmd.Parameters.AddWithValue("@ReferenceInvoiceDate", ReferenceInvoiceDate);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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




        public DataSet GetPurchaseReturnVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_return_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPurchaseReturnVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_return_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPurchaseReturnVouchersReport(
            string dateSignS, 
            string dateTypeS, 
            string startDateS,
            string endDateS,
            string WarehouseIdS,
            int unitIdS,
            int statusIdS,
            string voucherCreatedByS,
            string searchByS,
            string searchTextS,
            string vendorCodeS,
            string vendorNameS,
            string amountSignS,
            decimal amountOneS,
            decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_purchase_return_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeS);
            cmd.Parameters.AddWithValue("@VendorName", vendorNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizePurchaseReturnVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_purchase_return_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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






        //SRV
        public DataSet GetSaleReturnVouchersListToAutorize(
            string dateSignS
          , string dateTypeS
          , string startDateS
          , string endDateS
          , string warehouseIdS
          , int unitIdS
          , int statusToS
          , int CreatedByIdS
          , string searchByS
          , string searchTextS
          , string customerCodeS
          , string customerNameS
          , string amountSignS
          , decimal amountOneS
          , decimal amountTwoS)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_return_vouchers_list_to_authorize]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600; // 10 minutes
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@WarehouseId", warehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@StatusID", statusToS);

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedByIdS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleReturnVouchersListToApprove(
                            string dateSignS
                        , string dateTypeS
                        , string startDateS
                        , string endDateS
                        , string WarehouseIdS
                        , int unitIdS
                        , int statusIdS
                        , string voucherCreatedByS
                        , string searchByS
                        , string searchTextS
                        , string customerCodeS
                        , string customerNameS
                        , string amountSignS
                        , decimal amountOneS
                        , decimal amountTwoS
                )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_return_vouchers_list_to_approve]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int AuthorizeSaleReturnVoucher
            (
                int Pid,
                string VoucherNo,
                string VoucherDate,
                string ReferenceInvoiceNo,
                string ReferenceInvoiceDate,
                string CustomerCode,
                string CustomerName,
                string DOCClass,
                string CurrencyDesc,
                decimal CurrencyRate,
                decimal Amount,
                string AccountID,
                int unitId,

                string VoucherCreatedBy,
                string AuthorizedRemarks,

                string FileName1,
                byte[] FileBytes1,

                string FileName2,
                byte[] FileBytes2,

                string FileName3,
                byte[] FileBytes3,

                string FileName4,
                byte[] FileBytes4,

                string FileName5,
                byte[] FileBytes5,

                int CreatedBy,

                DataTable dtVoucherDetails,
                int ActId
             )

        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_authorize_sale_return_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherDate", VoucherDate);
            cmd.Parameters.AddWithValue("@ReferenceInvoiceNo", ReferenceInvoiceNo);
            cmd.Parameters.AddWithValue("@ReferenceInvoiceDate", ReferenceInvoiceDate);
            cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@DOCClass", DOCClass);
            cmd.Parameters.AddWithValue("@CurrencyDesc", CurrencyDesc);
            cmd.Parameters.AddWithValue("@CurrencyRate", CurrencyRate);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", VoucherCreatedBy);
            cmd.Parameters.AddWithValue("@AuthorizedRemarks", AuthorizedRemarks);

            if (FileBytes1 != null)
            {
                cmd.Parameters.AddWithValue("@FileName1", FileName1);
                cmd.Parameters.AddWithValue("@FileBytes1", FileBytes1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes1", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName1", "");
            }

            if (FileBytes2 != null)
            {
                cmd.Parameters.AddWithValue("@FileName2", FileName2);
                cmd.Parameters.AddWithValue("@FileBytes2", FileBytes2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes2", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName2", "");
            }

            if (FileBytes3 != null)
            {
                cmd.Parameters.AddWithValue("@FileName3", FileName3);
                cmd.Parameters.AddWithValue("@FileBytes3", FileBytes3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes3", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName3", "");
            }

            if (FileBytes4 != null)
            {
                cmd.Parameters.AddWithValue("@FileName4", FileName4);
                cmd.Parameters.AddWithValue("@FileBytes4", FileBytes4);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes4", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName4", "");
            }

            if (FileBytes5 != null)
            {
                cmd.Parameters.AddWithValue("@FileName5", FileName5);
                cmd.Parameters.AddWithValue("@FileBytes5", FileBytes5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileBytes5", System.Data.SqlTypes.SqlBinary.Null);
                cmd.Parameters.AddWithValue("@FileName5", "");
            }

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            cmd.Parameters.AddWithValue("@tblVoucherDetails", dtVoucherDetails);


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




        public DataSet GetSaleReturnVoucherDetails(string voucherNo, int unitId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_return_voucher_details]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UnitID", unitId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleReturnVoucherDetailsForPDF(int Pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_return_vouchers_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@PID", Pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSaleReturnVouchersReport(
            string dateSignS,
            string dateTypeS,
            string startDateS,
            string endDateS,
            string WarehouseIdS,
            int unitIdS,
            int statusIdS,
            string voucherCreatedByS,
            string searchByS,
            string searchTextS,
            string customerCodeS,
            string customerNameS,
            string amountSignS,
            decimal amountOneS,
            decimal amountTwoS
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_sale_return_vouchers_report]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateSign", dateSignS);
            cmd.Parameters.AddWithValue("@DateType", dateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", startDateS);
            cmd.Parameters.AddWithValue("@EndDate", endDateS);
            cmd.Parameters.AddWithValue("@UnitId", unitIdS);
            cmd.Parameters.AddWithValue("@WarehouseId", WarehouseIdS);
            cmd.Parameters.AddWithValue("@StatusId", statusIdS);
            cmd.Parameters.AddWithValue("@VoucherCreatedBy", voucherCreatedByS);
            cmd.Parameters.AddWithValue("@SearchBy", searchByS);
            cmd.Parameters.AddWithValue("@SearchText", searchTextS);
            cmd.Parameters.AddWithValue("@CustomerCode", customerCodeS);
            cmd.Parameters.AddWithValue("@CustomerName", customerNameS);
            cmd.Parameters.AddWithValue("@AmountSign", amountSignS);
            cmd.Parameters.AddWithValue("@AmountOne", amountOneS);
            cmd.Parameters.AddWithValue("@AmountTwo", amountTwoS);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int BulkAuthorizeSaleReturnVoucher(int CreatedBy, DataTable dtVouchers, DataTable dtVoucherFiles, string filePath)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rCode = new SqlParameter("@RCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbVoucher].[sp_bulk_authorize_sale_return_voucher]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@tblVouchers", dtVouchers);
            cmd.Parameters.AddWithValue("@tblVoucherFiles", dtVoucherFiles);
            cmd.Parameters.AddWithValue("@filePath", filePath);

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
}
