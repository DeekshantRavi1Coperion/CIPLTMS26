using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class TC
    {


        public int AddUpdateDepartment(int departmentID, int unitID, string department, int checkerID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_po_tc_department";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@emprecordid", checkerID);
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

        public int AddUpdateChecker(int checkerID, int departmentID, int empRecordID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_po_tc_checker";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@tccheckerid", checkerID);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
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

        public DataSet GetTCDepartmentList(int unitID, string department)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_potc_department_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@department", department);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCheckerList(string checker, int departmentID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_potc_checker_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@employeename", checker);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAssingedTo(int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_assigned_to_list1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@unitid", unitID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOListToAddTC(string fromDate, string toDate, string dbNameA35, string dbNameDLH, string dbNameGNU,
                                   string dbNameSEZ, string poNo, string vendorName, int unitID, string JOBNo, string itemCode, string itemName,
                                   int excluedCIDF, int excludeEngineeringService)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_list_to_add_tc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedelhi", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);
            cmd.Parameters.AddWithValue("@excludeengineeringservice", excludeEngineeringService);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetPODetailsForPDF(string pONo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_details_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pono", pONo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UploadTC(int recordID, string poNo, string poDate, string vendorCode, string vendorName, string jOBNo, int unitID,
                            string itemCode, string itemName, string remarks,
                            string tC1File, byte[] tC1FileBytes, string tC2File, byte[] tC2FileBytes, string tC3File, byte[] tC3FileBytes,
                            int employeeRecordID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_po_tc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@remarks", remarks);

            cmd.Parameters.AddWithValue("@tc1filename", tC1File);
            if (tC1FileBytes != null)
                cmd.Parameters.AddWithValue("@tc1filebytes", tC1FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tc2filename", tC2File);
            if (tC2FileBytes != null)
                cmd.Parameters.AddWithValue("@tc2filebytes", tC2FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tc3filename", tC3File);
            if (tC3FileBytes != null)
                cmd.Parameters.AddWithValue("@tc3filebytes", tC3FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc3filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@employeerecordid", employeeRecordID);
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

        public int UploadTC(int poRecordID, string poNo, string poDate, string vendorCode, string vendorName, string jOBNo, int unitID, string itemCode, string itemName,
                            DataTable dtDocs,
                            int poTC1RecordID, string tC1File, byte[] tC1FileBytes, string tC1remarks, int tC1employeeRecordID, int tC1SeqNo, int removedTC1ID,
                            int poTC2RecordID, string tC2File, byte[] tC2FileBytes, string tC2remarks, int tC2employeeRecordID, int tC2SeqNo, int removedTC2ID,
                            int poTC3RecordID, string tC3File, byte[] tC3FileBytes, string tC3remarks, int tC3employeeRecordID, int tC3SeqNo, int removedTC3ID,
                            int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_po_tc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@porecordid", poRecordID);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@dtdocs", dtDocs);

            cmd.Parameters.AddWithValue("@potc1recordid", poTC1RecordID);

            cmd.Parameters.AddWithValue("@tc1file", tC1File);
            if (tC1FileBytes != null && !string.IsNullOrEmpty(tC1File))
                cmd.Parameters.AddWithValue("@tc1filebytes", tC1FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc1filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@tc1remarks", tC1remarks);
            cmd.Parameters.AddWithValue("@tc1employeerecordid", tC1employeeRecordID);
            cmd.Parameters.AddWithValue("@tc1seqno", tC1SeqNo);
            cmd.Parameters.AddWithValue("@removedtc1id", removedTC1ID);





            cmd.Parameters.AddWithValue("@potc2recordid", poTC2RecordID);

            cmd.Parameters.AddWithValue("@tc2file", tC2File);
            if (tC2FileBytes != null && !string.IsNullOrEmpty(tC2File))
                cmd.Parameters.AddWithValue("@tc2filebytes", tC2FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc2filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@tc2remarks", tC2remarks);
            cmd.Parameters.AddWithValue("@tc2employeerecordid", tC2employeeRecordID);
            cmd.Parameters.AddWithValue("@tc2seqno", tC2SeqNo);
            cmd.Parameters.AddWithValue("@removedtc2id", removedTC2ID);



            cmd.Parameters.AddWithValue("@potc3recordid", poTC3RecordID);

            cmd.Parameters.AddWithValue("@tc3file", tC3File);
            if (tC3FileBytes != null && !string.IsNullOrEmpty(tC3File))
                cmd.Parameters.AddWithValue("@tc3filebytes", tC3FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc3filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@tc3remarks", tC3remarks);
            cmd.Parameters.AddWithValue("@tc3employeerecordid", tC3employeeRecordID);
            cmd.Parameters.AddWithValue("@tc3seqno", tC3SeqNo);
            cmd.Parameters.AddWithValue("@removedtc3id", removedTC3ID);



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




        public int UploadNewTCToExistingPO(int poRecordID,
                           int poTC1RecordID, string tC1File, byte[] tC1FileBytes, string tC1remarks, int tC1employeeRecordID, int tC1SeqNo, int removedTC1ID, string TC1IdentificationReferenceNo,
                           int poTC2RecordID, string tC2File, byte[] tC2FileBytes, string tC2remarks, int tC2employeeRecordID, int tC2SeqNo, int removedTC2ID, string TC2IdentificationReferenceNo,
                           int poTC3RecordID, string tC3File, byte[] tC3FileBytes, string tC3remarks, int tC3employeeRecordID, int tC3SeqNo, int removedTC3ID, string TC3IdentificationReferenceNo,
                           int isCompleted, string isCompletedRemarks,
                           int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_upload_new_tc_to_existing_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@porecordid", poRecordID);
            cmd.Parameters.AddWithValue("@potc1recordid", poTC1RecordID);

            cmd.Parameters.AddWithValue("@tc1file", tC1File);
            if (tC1FileBytes != null && !string.IsNullOrEmpty(tC1File))
                cmd.Parameters.AddWithValue("@tc1filebytes", tC1FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc1filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@tc1remarks", tC1remarks);
            cmd.Parameters.AddWithValue("@tc1employeerecordid", tC1employeeRecordID);
            cmd.Parameters.AddWithValue("@tc1seqno", tC1SeqNo);
            cmd.Parameters.AddWithValue("@removedtc1id", removedTC1ID);
            cmd.Parameters.AddWithValue("@tc1identificationreferenceno", TC1IdentificationReferenceNo);





            cmd.Parameters.AddWithValue("@potc2recordid", poTC2RecordID);

            cmd.Parameters.AddWithValue("@tc2file", tC2File);
            if (tC2FileBytes != null && !string.IsNullOrEmpty(tC2File))
                cmd.Parameters.AddWithValue("@tc2filebytes", tC2FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc2filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@tc2remarks", tC2remarks);
            cmd.Parameters.AddWithValue("@tc2employeerecordid", tC2employeeRecordID);
            cmd.Parameters.AddWithValue("@tc2seqno", tC2SeqNo);
            cmd.Parameters.AddWithValue("@removedtc2id", removedTC2ID);
            cmd.Parameters.AddWithValue("@tc2identificationreferenceno", TC2IdentificationReferenceNo);



            cmd.Parameters.AddWithValue("@potc3recordid", poTC3RecordID);

            cmd.Parameters.AddWithValue("@tc3file", tC3File);
            if (tC3FileBytes != null && !string.IsNullOrEmpty(tC3File))
                cmd.Parameters.AddWithValue("@tc3filebytes", tC3FileBytes);
            else
                cmd.Parameters.AddWithValue("@tc3filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@tc3remarks", tC3remarks);
            cmd.Parameters.AddWithValue("@tc3employeerecordid", tC3employeeRecordID);
            cmd.Parameters.AddWithValue("@tc3seqno", tC3SeqNo);
            cmd.Parameters.AddWithValue("@removedtc3id", removedTC3ID);
            cmd.Parameters.AddWithValue("@tc3identificationreferenceno", TC3IdentificationReferenceNo);

            cmd.Parameters.AddWithValue("@iscompleted", isCompleted);
            cmd.Parameters.AddWithValue("@iscompletedremarks", isCompletedRemarks);



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



        public int UploadNewTCToNewPO(string poNo, string poDate, string vendorCode, string vendorName, string jOBNo, int unitID, string itemCode, string itemName,
                                      DataTable dtDocs, int isCompleted, string isCompletedRemarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_upload_new_tc_to_new_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@dtdocs", dtDocs);

            cmd.Parameters.AddWithValue("@iscompleted", isCompleted);
            cmd.Parameters.AddWithValue("@iscompletedremarks", isCompletedRemarks);

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



        public DataSet GetLOTDrawingFiles(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_tc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPOTCList(string fromDate, string toDate, int statusID, string poNo, string vendorName,
                                    int unitID, string JOBNo, string itemCode, string itemName, int teamID, string identificationReferenceNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_tc_list1";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@teamid", teamID);
            cmd.Parameters.AddWithValue("@identificationreferenceno", identificationReferenceNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetTCStatusList(int notOpenFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tc_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@notopenflag", notOpenFlag);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTCStatus(int recordID, int statusID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_po_tc_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
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


        public DataSet GetPOTCMailInfo(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tc_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOTCMailInfo(int poRecordID, string poTcRecordIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tc_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@porecordid", poRecordID);
            cmd.Parameters.AddWithValue("@potcrecordid", poTcRecordIDs);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTCMailStatus(int recordID, int statusID, int createdBy)
        {
            {
                string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
                SqlConnection con = new SqlConnection(cipltmsconnectionstring);
                SqlCommand cmd = new SqlCommand();
                SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
                cmd.CommandText = "sp_update_po_tc_mail_status";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;


                cmd.Parameters.AddWithValue("@recordid", recordID);
                cmd.Parameters.AddWithValue("@statusid", statusID);
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

        public int UpdateTCStatus(int poRecordID,
                                  int poTC1RecordID, int poTC1StatusID, string tC1remarks,
                                  int poTC2RecordID, int poTC2StatusID, string tC2remarks,
                                  int poTC3RecordID, int poTC3StatusID, string tC3remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_po_tc_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@porecordid", poRecordID);
            cmd.Parameters.AddWithValue("@potc1recordid", poTC1RecordID);
            cmd.Parameters.AddWithValue("@potc1statusid", poTC1StatusID);
            cmd.Parameters.AddWithValue("@tc1remarks", tC1remarks);

            cmd.Parameters.AddWithValue("@potc2recordid", poTC2RecordID);
            cmd.Parameters.AddWithValue("@potc2statusid", poTC2StatusID);
            cmd.Parameters.AddWithValue("@tc2remarks", tC2remarks);

            cmd.Parameters.AddWithValue("@potc3recordid", poTC3RecordID);
            cmd.Parameters.AddWithValue("@potc3statusid", poTC3StatusID);
            cmd.Parameters.AddWithValue("@tc3remarks", tC3remarks);

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


        public DataSet GetTCDetailsForPDF(int PORecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_tc_detail_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@porecordid", PORecordID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetCheckedByList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Created_By_List]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPOListToAddTC(string fromDate, string toDate, string dbNameA35, string dbNameDLH, string dbNameGNU,
                                   string dbNameSEZ, string poNo, string vendorName, int unitID, string JOBNo, string itemCode, string itemName,
                                   int excluedCIDF, int excludeEngineeringService, string CheckedBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_list_to_add_tc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedelhi", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);
            cmd.Parameters.AddWithValue("@excludeengineeringservice", excludeEngineeringService);
            cmd.Parameters.AddWithValue("@CheckedBy", CheckedBy);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



    }
}
