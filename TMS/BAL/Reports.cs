using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class Reports
    {
        public DataSet GetSaleBillDetails(int unitId, string unitName, string fromDate, string toDate)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_salesbill_report";//sp_get_salesbill_details_new
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

        public DataSet GetSaleBillDetailsAllUnit(string fromDate, string toDate, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_salesbill_report_all_unit";//sp_get_salesbill_details_all_unit_new
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



        public DataSet GetSalesorderDetails(int unitId, string unitName, string fromDate, string toDate)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_salesorder_report";//sp_get_salesorder_details
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



        public DataSet GetSalesorderDetailsAllUnit(string fromDate, string toDate, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_salesorder_report_all_unit";//sp_get_salesorder_details_all_unit
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


        public DataSet GetEnggHoursReport(string startDate, string endDate, string jobNo, int subordinateID, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "sp_get_engg_hours_report";
            cmd.CommandText = "sp_get_engg_hours_report_TEST_to_be_deleted_later_01";//testing purpose on 3 july 24 by minni
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@subordinateid", subordinateID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetUserDetailsForEnggHours(string startDate, string endDate, int deptID, string employeeType)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_user_engg_hours01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", startDate);
            cmd.Parameters.AddWithValue("@todate", endDate);
            cmd.Parameters.AddWithValue("@departmentid", deptID);
            cmd.Parameters.AddWithValue("@employeetype", employeeType);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int CreateTempTable(string createQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = createQuery;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int value = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                return 1;
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

        public DataSet GetPoDocumentsList(int unitId,
                                          string dateType,
                                          string startDate,
                                          string endDate,
                                          string vendorCode,
                                          string vendorName,
                                          string poNumber,
                                          string jobNumber,
                                          string mrNumber,
                                          int attachedById)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Po_Documents_List]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@DateType", dateType);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);

            cmd.Parameters.AddWithValue("@poNumber", poNumber);
            cmd.Parameters.AddWithValue("@jobNumber", jobNumber);
            cmd.Parameters.AddWithValue("@attachedById", attachedById);
            cmd.Parameters.AddWithValue("@MRNumber", mrNumber);



            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAttachedPoDocumentsList(int pId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Attached_Po_Documents_List]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PId", pId);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAttachedPoDocumentFiles(int pId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Attached_Po_Documents_Files]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PId", pId);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAttachedByList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Attached_By_List]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTempDetails()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM tbltempengghours";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTempTable(string updateQuery)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = updateQuery;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int value = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                return 1;
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

        public DataSet GetPOReport(string fromDate, string toDate, string unitName, string PONo, string vendorName, string vendorCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);


            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOReport(string PONo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_in_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pono", PONo);


            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetSaleOrderLeadTimeReport(int unitId, string unitName, string fromDate, string toDate)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_lead_time_report";
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

        public int SavePoDocuments(int pidToI
                                 , int unitFidToI
                                 , string poNoToI
                                 , string poDateToI
                                 , string vendorCodeToI
                                 , string jobNoToI
                                 , string attachment1NameToI
                                 , byte[] attachment1DocToI
                                 , string attachment1RemarksToI
                                 , int createdByToI
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@Rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbPO].[Sp_Insert_Po_Documents]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@RecordId", pidToI);
            cmd.Parameters.AddWithValue("@UnitId", unitFidToI);
            cmd.Parameters.AddWithValue("@PoNo", poNoToI);
            cmd.Parameters.AddWithValue("@PoDate", poDateToI);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCodeToI);
            cmd.Parameters.AddWithValue("@JobNo", jobNoToI);


            cmd.Parameters.AddWithValue("@Attachment1Name", attachment1NameToI);
            if (attachment1DocToI != null)
                cmd.Parameters.AddWithValue("@Attachment1Doc", attachment1DocToI);
            else
                cmd.Parameters.AddWithValue("@Attachment1Doc", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@Attachment1Remarks", attachment1RemarksToI);


            cmd.Parameters.AddWithValue("@CreatedBy", createdByToI);

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



        public int RemovePoDocuments(int pidToR
                                    , int docIdToR
                                    , int createdByToR
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@Rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbPO].[Sp_Remove_Po_Documents]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Po_Id", pidToR);
            cmd.Parameters.AddWithValue("@Doc_Id", docIdToR);
            cmd.Parameters.AddWithValue("@CreatedBy", createdByToR);

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



        public DataSet GetSaleOrderLeadTimeReportAllUnits(string fromDate, string toDate, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_lead_time_report_all_unit";
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


        public DataSet GetPOReportSummery(string fromDate, string toDate, string dbNameA35, string dbNameDLH, string dbNameGNU, string dbNameSEZ,
                                        string poNo, string vendorName, string vendorCode, string unitName, string status, string JOBNo,
                                        double amount1, double amount2, string sign, int excluedCIDF)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedelhi", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount1", amount1);
            cmd.Parameters.AddWithValue("@amount2", amount2);
            cmd.Parameters.AddWithValue("@sign", sign);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOReportSummeryPmtTerm(string fromDate, string toDate, string poNo, string vendorName, string unitName, string status, string JOBNo, double amount, int excluedCIDF)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary02";
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



        public DataSet GetPOReportSummeryV2(string fromDate, string toDate, string poNo, string vendorName, string unitName, string status, string JOBNo, double amount, int excluedCIDF)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary_new";
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

        public DataSet GetAttachedPoDocumentFile(int pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Attached_Po_Document_File]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PId", pid);


            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPOReportSummaryUnitWise(int unitId, string unitName, string fromDate, string toDate, string poNo, string vendorName,
                                            string JOBNo, double amount, string status)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary01";//sp_get_po_report_summery
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOReportSummaryAllUnit(string fromDate, string toDate, string dbNameA35, string unitNameA35,
                                                 string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ,
                                                 string dbNameGNU, string unitNameGNU,
                                                 string poNo, string vendorName, string JOBNo, double amount, string status)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary_all_units";
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
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPODetail(string unit, string poNo, string docClass)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@unit", unit);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@docclass", docClass);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSalesorderOutstandingDetails(int unitId, string unitName, string fromDate, string toDate, string jobNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_outstanding_detail03";//sp_get_sale_order_outstanding_detail02
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSalesorderOutstandingDetailsAllUnit(string fromDate, string toDate, string jobNo,
                                                             string dbNameA35, string unitNameA35,
                                                             string dbNameDLH, string unitNameDLH,
                                                             string dbNameSEZ, string unitNameSEZ,
                                                             string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_outstanding_detail_all_units02";//sp_get_sale_order_outstanding_detail_all_units01
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
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

        public DataSet CreateAndInsertIntoOutstandingTempTable(string colOutstandingMonth, string insertQueryText, string insertQueryValue)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_create_outstanding_table_and_insert_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@columns", colOutstandingMonth);
            cmd.Parameters.AddWithValue("@insertquerytext", insertQueryText);
            cmd.Parameters.AddWithValue("@insertqueryvalue", insertQueryValue);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOutStandingReport(string fromDate, string toDate, int unitId, string unitName, string updateQuery, int actID)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_update_temptable_and_get_outstanding_report";//sp_update_temptable_and_get_outstanding_report01
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOutStandingReportAllUnit(string fromDate, string toDate,
                                                    string dbNameA35, string dbNameDLH,
                                                    string dbNameSEZ, string dbNameGNU,
                                                    string updateQuery, int actID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_update_temptable_and_get_outstanding_report_all_units02";//sp_update_temptable_and_get_outstanding_report_all_units01
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet BindUserForLogReport(string fromDate, string toDate, string reportType, string unitName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_users_for_log_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@reportType", reportType);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetLogReport(string fromDate, string toDate, string unitName, string reportType, string userName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_log_report_of_all";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@reportType", reportType);
            cmd.Parameters.AddWithValue("@username", userName);

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetFactLogReport(string fromDate, string toDate, string department,
                                        string dbNameA35, string dbNameDLH, string dbNameGNU, string dbNameSEZ)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fact_log_report06";//sp_get_fact_log_report03
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@dbnameA35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnameDELHI", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnameGNU", dbNameGNU);
            cmd.Parameters.AddWithValue("@dbnameSEZ", dbNameSEZ);
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet CreateTempTableAndGetTemptableDataPreviousMonths(string colOutstandingMonth, string insertQueryText, string insertQueryValue)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_create_outstanding_table_and_insert_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@columns", colOutstandingMonth);
            cmd.Parameters.AddWithValue("@insertquerytext", insertQueryText);
            cmd.Parameters.AddWithValue("@insertqueryvalue", insertQueryValue);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int DropExistingTable()
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_tour_information";
            cmd.CommandType = CommandType.StoredProcedure;
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

        public DataSet GetSalesorderOutstandingDetailsOne(int unitId, string unitName, string month1, string month2, string month3, string month4,
                                                         string month5, string month6, string month7, string month8, string month9, string month10,
                                                         string month11, string month12, string jobNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_outstanding_detail06";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);

            cmd.Parameters.AddWithValue("@month1", month1);
            cmd.Parameters.AddWithValue("@month2", month2);
            cmd.Parameters.AddWithValue("@month3", month3);
            cmd.Parameters.AddWithValue("@month4", month4);
            cmd.Parameters.AddWithValue("@month5", month5);
            cmd.Parameters.AddWithValue("@month6", month6);
            cmd.Parameters.AddWithValue("@month7", month7);
            cmd.Parameters.AddWithValue("@month8", month8);
            cmd.Parameters.AddWithValue("@month9", month9);
            cmd.Parameters.AddWithValue("@month10", month10);
            cmd.Parameters.AddWithValue("@month11", month11);
            cmd.Parameters.AddWithValue("@month12", month12);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSalesorderOutstandingDetailsAllUnitOne(string month1, string month2, string month3, string month4, string month5, string month6,
                                                                 string month7, string month8, string month9, string month10, string month11, string month12,
                                                  string jobNo, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH,
                                                  string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_outstanding_detail_all_units05";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);

            cmd.Parameters.AddWithValue("@month1", month1);
            cmd.Parameters.AddWithValue("@month2", month2);
            cmd.Parameters.AddWithValue("@month3", month3);
            cmd.Parameters.AddWithValue("@month4", month4);
            cmd.Parameters.AddWithValue("@month5", month5);
            cmd.Parameters.AddWithValue("@month6", month6);
            cmd.Parameters.AddWithValue("@month7", month7);
            cmd.Parameters.AddWithValue("@month8", month8);
            cmd.Parameters.AddWithValue("@month9", month9);
            cmd.Parameters.AddWithValue("@month10", month10);
            cmd.Parameters.AddWithValue("@month11", month11);
            cmd.Parameters.AddWithValue("@month12", month12);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCurrentCurrencyList(string month1, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fact_current_fx_rate_list01";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@month", month1);
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

        public int SaveCurrnetFXRate(string currencyCode, string dated, string currentMonth, double fxRate, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_current_fx_rate";
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@currencycode", currencyCode);
            cmd.Parameters.AddWithValue("@dated", dated);
            cmd.Parameters.AddWithValue("@currentmonth", currentMonth);
            cmd.Parameters.AddWithValue("@fxrate", fxRate);
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

        public DataSet GetSOBacklogPOCPostingList(int unitId, string unitName, string month1, string jobNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_backlog_for_poc_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month1);
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSOBacklogPOCPostingListAllUnit(string month1, string jobNo, string dbNameA35, string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ, string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_backlog_for_poc_posting_all_units";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@month", month1);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
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

        public int PostPOCBilling(string jobNo, string orderDate, string customerCode, string customerName, string BU,
                                    double orderAmount, double pocBilling, double taxBilling, double netBilling, string unit, string month, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_poc_backlog_billing_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@orderdate", orderDate);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@bu", BU);
            cmd.Parameters.AddWithValue("@orderamount", orderAmount);
            cmd.Parameters.AddWithValue("@pocbillingamount", pocBilling);
            cmd.Parameters.AddWithValue("@taxbillingamount", taxBilling);
            cmd.Parameters.AddWithValue("@netbillingamount", netBilling);
            cmd.Parameters.AddWithValue("@unit", unit);
            cmd.Parameters.AddWithValue("@postingmonth", month);
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

        public DataSet GetSOBacklogPOCPostingListAllUnitOne(string month1, string jobNo, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_backlog_for_poc_posting_all_units03";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@month", month1);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
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

        public int PostPOCBillingOne(string jobNo, string orderDate, string customerCode, string customerName,
                                string bu, double orderAmount, double pocBilling, double taxBilling, double netPOCBacklog,
                                string month, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_poc_backlog_billing_posting01";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@orderdate", orderDate);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@bu", bu);
            cmd.Parameters.AddWithValue("@orderamount", orderAmount);
            cmd.Parameters.AddWithValue("@pocbillingamount", pocBilling);
            cmd.Parameters.AddWithValue("@taxbillingamount", taxBilling);
            cmd.Parameters.AddWithValue("@netpocbacklog", netPOCBacklog);
            cmd.Parameters.AddWithValue("@postingmonth", month);
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

        public DataSet GetSalesorderOutstandingDetailsAllUnitTwo(string month1, string month2, string month3, string month4,
                                            string month5, string month6, string month7, string month8, string month9,
                                            string month10, string month11, string month12,
                                            string jobNo, string customerName, string customerType, string bu, string currency,
                                            string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sale_order_outstanding_detail_all_units09";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customertype", customerType);
            cmd.Parameters.AddWithValue("@bu", bu);
            cmd.Parameters.AddWithValue("@currency", currency);

            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);

            cmd.Parameters.AddWithValue("@month1", month1);
            cmd.Parameters.AddWithValue("@month2", month2);
            cmd.Parameters.AddWithValue("@month3", month3);
            cmd.Parameters.AddWithValue("@month4", month4);
            cmd.Parameters.AddWithValue("@month5", month5);
            cmd.Parameters.AddWithValue("@month6", month6);
            cmd.Parameters.AddWithValue("@month7", month7);
            cmd.Parameters.AddWithValue("@month8", month8);
            cmd.Parameters.AddWithValue("@month9", month9);
            cmd.Parameters.AddWithValue("@month10", month10);
            cmd.Parameters.AddWithValue("@month11", month11);
            cmd.Parameters.AddWithValue("@month12", month12);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJobNoInvoiceList(string month, string jobNo, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_no_invoice_list";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
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

        public DataSet GetFactCurrencyList(string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fact_currency_list";
            cmd.CommandType = CommandType.StoredProcedure;

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

        public int TagInvoiceToInvoice(string finalValQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_job_no_tagged_invoices";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@valuequery", finalValQuery);
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

        public DataSet GetVendoerMasterReport(string fromDate, string toDate, string vendorName, string vendorCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vendor_report";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOReportJOBWise(string startDate, string endDate, string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_summary_job_wise";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOCPostedBillingReport(string fromDate, string toDate, string JOBNo, string customerCode, string customerName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_poc_billing_posted_report";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOProcurementProcessReport(int unitId, string unitName, string fromDate, string toDate, string poNo, string vendorName, string status, string MRNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_procurement_process_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@mrnno", MRNo);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOProcurementProcessReportAllUnit(string fromDate, string toDate, string dbNameA35,
                                        string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ,
                                        string unitNameSEZ, string dbNameGNU, string unitNameGNU, string poNo,
                                        string vendorName, string status, string MRNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_procurement_process_report_all_units";
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

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@mrnno", MRNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVPOCListForPosting(int unitId, string unitName, string fromDate, string toDate, string poNo,
                                        string vendorName, string status)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vpoc_list_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVPOCListForPostingAllUnit(string fromDate, string toDate, string dbNameA35,
                                        string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ,
                                        string unitNameSEZ, string dbNameGNU, string unitNameGNU, string poNo,
                                        string vendorName, string status)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vpoc_list_for_posting_all_units";
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

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertUpdateVPOCPosting(int recordID, string PONo, string PODate, string DOCClass, string vendorCode, string vendorName,
                                string POValueINR, string POStatus, string location, string itemCategory, string DOD,
                                string likelyDOD, int months, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_vpoc_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@podate", PODate);
            cmd.Parameters.AddWithValue("@docclass", DOCClass);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@povalueinr", POValueINR);
            cmd.Parameters.AddWithValue("@postatus", POStatus);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@itemcategory", itemCategory);
            cmd.Parameters.AddWithValue("@dod", DOD);
            cmd.Parameters.AddWithValue("@likelydod", likelyDOD);
            cmd.Parameters.AddWithValue("@months", months);
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


        public DataSet GetPOCList(string month1, string month2, string month3, string month4,
                                            string month5, string month6, string month7, string month8, string month9,
                                            string month10, string month11, string month12,
                                            string jobNo, string customerName, string customerType, string bu, string currency,
                                            string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_poc_list_for_phasing";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customertype", customerType);
            cmd.Parameters.AddWithValue("@bu", bu);
            cmd.Parameters.AddWithValue("@currency", currency);

            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);

            cmd.Parameters.AddWithValue("@month1", month1);
            cmd.Parameters.AddWithValue("@month2", month2);
            cmd.Parameters.AddWithValue("@month3", month3);
            cmd.Parameters.AddWithValue("@month4", month4);
            cmd.Parameters.AddWithValue("@month5", month5);
            cmd.Parameters.AddWithValue("@month6", month6);
            cmd.Parameters.AddWithValue("@month7", month7);
            cmd.Parameters.AddWithValue("@month8", month8);
            cmd.Parameters.AddWithValue("@month9", month9);
            cmd.Parameters.AddWithValue("@month10", month10);
            cmd.Parameters.AddWithValue("@month11", month11);
            cmd.Parameters.AddWithValue("@month12", month12);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int InsertUpdateNewPOCPhasing(int recordID, string JOBNo, string customerCode, string customerName, string customerType, string DOCClass,
                                       string BU, string orderCurrency, double orderCurrRate, double orderAmtFC, double INVAmtFC,
                                       double deltaFC, double orderAmtINR, double INVAmtINR, double deltaINR, double currentFCRate,
                                       double orderAmtINROnCurrentRate, double INVAmtINROnCurrentRate, double deltaOnCurrRate,
                                       double POCBillingAmt, double TAXBillingAmt, double finalBacklog, double adjustment, string expectedDate,
                                       double monthFirst, double monthSecond, double monthThird, double monthFourth,
                                       double monthFifth, double monthSixth, double monthSeventh, double monthEighth, double monthNinth,
                                       double monthTenth, double monthEleventh, double monthTwelfth, double nextYears, string postingMonth,
                                       int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_poc_phasing";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customertype", customerType);
            cmd.Parameters.AddWithValue("@docclass", DOCClass);
            cmd.Parameters.AddWithValue("@bu", BU);
            cmd.Parameters.AddWithValue("@ordercurrency", orderCurrency);
            cmd.Parameters.AddWithValue("@ordercurrrate", orderCurrRate);
            cmd.Parameters.AddWithValue("@orderamtfc", orderAmtFC);
            cmd.Parameters.AddWithValue("@invamtfc", INVAmtFC);
            cmd.Parameters.AddWithValue("@deltafc", deltaFC);
            cmd.Parameters.AddWithValue("@orderamtinr", orderAmtINR);
            cmd.Parameters.AddWithValue("@invamtinr", INVAmtINR);
            cmd.Parameters.AddWithValue("@deltainr", deltaINR);
            cmd.Parameters.AddWithValue("@currentfcrate", currentFCRate);
            cmd.Parameters.AddWithValue("@orderamtinroncurrentrate", orderAmtINROnCurrentRate);
            cmd.Parameters.AddWithValue("@invamtinroncurrentrate", INVAmtINROnCurrentRate);
            cmd.Parameters.AddWithValue("@deltaoncurrrate", deltaOnCurrRate);
            cmd.Parameters.AddWithValue("@pocbillingamt", POCBillingAmt);
            cmd.Parameters.AddWithValue("@taxbillingamt", TAXBillingAmt);
            cmd.Parameters.AddWithValue("@finalbacklog", finalBacklog);
            cmd.Parameters.AddWithValue("@adjustment", adjustment);
            cmd.Parameters.AddWithValue("@expecteddate", expectedDate);
            cmd.Parameters.AddWithValue("@monthfirst", monthFirst);
            cmd.Parameters.AddWithValue("@monthsecond", monthSecond);
            cmd.Parameters.AddWithValue("@monththird", monthThird);
            cmd.Parameters.AddWithValue("@monthfourth", monthFourth);
            cmd.Parameters.AddWithValue("@monthfifth", monthFifth);
            cmd.Parameters.AddWithValue("@monthsixth", monthSixth);
            cmd.Parameters.AddWithValue("@monthseventh", monthSeventh);
            cmd.Parameters.AddWithValue("@montheighth", monthEighth);
            cmd.Parameters.AddWithValue("@monthninth", monthNinth);
            cmd.Parameters.AddWithValue("@monthtenth", monthTenth);
            cmd.Parameters.AddWithValue("@montheleventh", monthEleventh);
            cmd.Parameters.AddWithValue("@monthtwelfth", monthTwelfth);
            cmd.Parameters.AddWithValue("@nextyears", nextYears);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
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


        public DataSet GetMRNListForPFDPriting(int unitId, string unitName, string fromDate, string toDate, string poNo, string vendorName, string MRNo)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mrn_list_for_pdf_printing";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@mrnno", MRNo);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMRNListForPFDPritingAllUnit(string fromDate, string toDate, string dbNameA35,
                                        string unitNameA35, string dbNameDLH, string unitNameDLH, string dbNameSEZ,
                                        string unitNameSEZ, string dbNameGNU, string unitNameGNU, string poNo,
                                        string vendorName, string MRNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mrn_list_for_pdf_printing_all_units";
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

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@mrnno", MRNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetMRNDetailForPDF(string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU, string MRNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mrn_detail_for_pdf_printing_all_units";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@mrnno", MRNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetDetailforEnc()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_prod_add_desc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVPOCPostedList(string unitName, string fromDate, string toDate, string poNo,
                                        string vendorName, string status)
        {

            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vpoc_posted_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVPOCPostedReport(string unitName, string fromDate, string toDate, string poNo,
                                        string vendorName, string status)
        {

            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vpoc_posted_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPOReportSummeryForcast(string POmonth, string month1, string month2, string month3, string month4,
                                                string month5, string month6, string month7, string month8, string month9,
                                                string month10, string month11, string month12, string poNo,
                                                string vendorName, string unitName, string status, string jOBNo,
                                                double amount1, double amount2, string sign, int excludeCIDF, int isPOCJobs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_purchase_order_forcast_report_all_units";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            cmd.Parameters.AddWithValue("@amount1", amount1);
            cmd.Parameters.AddWithValue("@amount2", amount2);
            cmd.Parameters.AddWithValue("@sign", sign);
            cmd.Parameters.AddWithValue("@excludecidf", excludeCIDF);
            cmd.Parameters.AddWithValue("@ispocjobs", isPOCJobs);

            cmd.Parameters.AddWithValue("@pomonth", POmonth);
            cmd.Parameters.AddWithValue("@month1", month1);
            cmd.Parameters.AddWithValue("@month2", month2);
            cmd.Parameters.AddWithValue("@month3", month3);
            cmd.Parameters.AddWithValue("@month4", month4);
            cmd.Parameters.AddWithValue("@month5", month5);
            cmd.Parameters.AddWithValue("@month6", month6);
            cmd.Parameters.AddWithValue("@month7", month7);
            cmd.Parameters.AddWithValue("@month8", month8);
            cmd.Parameters.AddWithValue("@month9", month9);
            cmd.Parameters.AddWithValue("@month10", month10);
            cmd.Parameters.AddWithValue("@month11", month11);
            cmd.Parameters.AddWithValue("@month12", month12);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetProductLogReport(string fromDate, string toDate, string dbNameA35, string unitNameA35,
                                                    string dbNameDLH, string unitNameDLH, string dbNameSEZ,
                                                    string unitNameSEZ, string dbNameGNU, string unitNameGNU,
                                                    string productCode,
                                                    int unitID, string unitName, int selectionFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_log_report";
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
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@selectionflag", selectionFlag);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMRDetailReport(string fromDate, string toDate, string dbNameA35, string unitNameA35,
                                                   string dbNameDLH, string unitNameDLH, string dbNameSEZ,
                                                   string unitNameSEZ, string dbNameGNU, string unitNameGNU,
                                                   string MRNo, string PONo, string MRNNo, string productCode, string jobNo,
                                                   int unitID, string unitName, int selectionFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mr_detail_report";
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
            cmd.Parameters.AddWithValue("@mrno", MRNo);
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@mrnno", MRNNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@selectionflag", selectionFlag);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetSaleGrossMarginDetails(int unitId, string unitName, string fromDate, string toDate)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sales_gross_margin_report";
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

        public DataSet GetSaleGrossMarginDetailsAllUnit(string month, string dbNameA35, string unitNameA35,
                                                        string dbNameDLH, string unitNameDLH, string dbNameSEZ, string unitNameSEZ,
                                                        string dbNameGNU, string unitNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sales_gross_margin_report_all_unit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month);
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

        public DataSet GetUnpostedSaleGrossMargin(string month, int unitID, string unitName,
                                                string customerName, string OANo, string billNo, string companyType, string pocNPoc,
                                                        string dbNameA35, string unitNameA35, int unitIDA35,
                                                        string dbNameDLH, string unitNameDLH, int unitIDDLH,
                                                        string dbNameSEZ, string unitNameSEZ, int unitIDSEZ,
                                                        string dbNameGNU, string unitNameGNU, int unitIDGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unposted_sales_gross_margin02";//sp_get_unposted_sales_gross_margin01
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month);

            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@unitname", unitName);

            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@oano", OANo);
            cmd.Parameters.AddWithValue("@billno", billNo);
            cmd.Parameters.AddWithValue("@companytype", companyType);
            cmd.Parameters.AddWithValue("@pocnpoc", pocNPoc);

            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@unitida35", unitIDA35);

            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@unitiddlh", unitIDDLH);

            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@unitidsez", unitIDSEZ);

            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);
            cmd.Parameters.AddWithValue("@unitidgnu", unitIDGNU);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetImportedCostCenter(string costCenter, string month,
                                             string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cost_center";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@costcenter", costCenter);
            cmd.Parameters.AddWithValue("@month", month);
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

        public DataSet GetCostCenterDetails(string costCenterCode, string month, int unitId, string unitName)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cost_center04";//sp_get_cost_center03
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitid", unitId);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@costcentercode", costCenterCode);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCostCenterStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cost_center_status_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int ImportCostCenterFile(DataTable dtCostCenter, string updateQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_cost_center";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblcostcenter", dtCostCenter);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
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

        public int ImportCostCenterFile(string month, DataTable dtCostCenter)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_cost_center";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@tblcostcenter", dtCostCenter);
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


        public DataSet GetPostedListInDetails(string month, string orderNo, string costCenterCode
                                            , string costCenterName, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cost_center_list_one";//sp_get_cost_center_list
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@orderno", orderNo);
            cmd.Parameters.AddWithValue("@costcentercode", costCenterCode);
            cmd.Parameters.AddWithValue("@costcentername", costCenterName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostedListInDetails(string month, string orderNo, string costCenterCode
                                            , string costCenterName, int unitID, int statusID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cost_center_list_one";//sp_get_cost_center_list
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@orderno", orderNo);
            cmd.Parameters.AddWithValue("@costcentercode", costCenterCode);
            cmd.Parameters.AddWithValue("@costcentername", costCenterName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateCostCenter(int costCenterID, double costCenterAmount, double directBilling, double partialBilling, double netAmount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_cost_center_by_cost_center_id";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@costcenterid", costCenterID);
            cmd.Parameters.AddWithValue("@costcenteramount", costCenterAmount);
            cmd.Parameters.AddWithValue("@directbilling", directBilling);
            cmd.Parameters.AddWithValue("@partialbilling", partialBilling);
            cmd.Parameters.AddWithValue("@netamount", netAmount);
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


        public int PostSaleGrossMargin(DataTable dtSalesGMToPost, string updateQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_sales_gross_margin";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblsalesgrossmargin", dtSalesGMToPost);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
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

        public DataSet GetSaleGrossMarginReport(string month, string unitName, string customerName, string OANo,
                                                string billNo, string companyType, string pocNPoc)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sales_gross_margin_report";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@oano", OANo);
            cmd.Parameters.AddWithValue("@billno", billNo);
            cmd.Parameters.AddWithValue("@companytype", companyType);
            cmd.Parameters.AddWithValue("@pocnpoc", pocNPoc);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateSaleGrossMargin(int recordID, double projectCosts, double actualGrossMargin, double actual, double highDelta, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_sales_gross_margin";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@projectcosts", projectCosts);
            cmd.Parameters.AddWithValue("@actualgrossmargin", actualGrossMargin);
            cmd.Parameters.AddWithValue("@actual", actual);
            cmd.Parameters.AddWithValue("@highdelta", highDelta);
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


        public DataSet GetMRWithOpenPOReport(string fromDate, string toDate, string dbNameA35, string unitNameA35,
                                                  string dbNameDLH, string unitNameDLH, string dbNameSEZ,
                                                  string unitNameSEZ, string dbNameGNU, string unitNameGNU,
                                                  string MRNo, string PONo, string fUser,
                                                  int unitID, string unitName, int selectionFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mr_with_open_po_report";
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
            cmd.Parameters.AddWithValue("@mrno", MRNo);
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@fuser", fUser);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@selectionflag", selectionFlag);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetFDMEE()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fdmee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetFDMEE(string year, string period, string quarter)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fdmee_by_year_period_quarter";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@period", period);
            cmd.Parameters.AddWithValue("@quarter", quarter);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int PostFDMEE(DataTable dtTempFDMEE, string updateQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_FDMEE";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblFDMEE", dtTempFDMEE);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
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

        public DataSet getFDMEEList(int year, int period, int quarter, string glAccount, string bSPLType,
                                    string hFMAccount, string type)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fdmee_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@period", period);
            cmd.Parameters.AddWithValue("@quarter", quarter);
            cmd.Parameters.AddWithValue("@glaccount", glAccount);
            cmd.Parameters.AddWithValue("@bspltype", bSPLType);
            cmd.Parameters.AddWithValue("@hfmaccount", hFMAccount);
            cmd.Parameters.AddWithValue("@type", type);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateFDMEE(int recordID, double amount, double sourceAmount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_FDMEE";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@sourceamount", sourceAmount);
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

        public DataSet GetGLCodeList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_glcode_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetGLCodeList(string glCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_glcode_list_by_glcode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@glcode", glCode);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetTimehsheetOfAllDepts(string month, int year, int deptID, string JOBNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_report_all_depts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@departmentid", deptID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int CreateTimehsheetAllDeptsTempTable(string createQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = createQuery;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int value = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                return 1;
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

        public DataSet GetTimehsheetAllDeptsTempTableDetails()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_all_depts_created_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCombinedProcProdDesignReport(string jobNo, int typeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_combined_drod_proc_design_view_reports_for_auto_mailer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCombinedProcProdDesignReportStatusWise(string unit, string jobNo, int typeID, string status)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_combined_drod_proc_design_view_report_status_wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@unit", unit);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSubordinates(int employeeRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_for_subordinate_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@employeeid", employeeRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPGReportForPosting(string fromDate, string toDate, string jOBNo, string postedMonth)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_pivot_group_list_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            cmd.Parameters.AddWithValue("@postedmonth", postedMonth);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPostedPOPivotGroupReport(string pivotGroup, string pivotGroupDesc, string postedMonth, string jOBNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_posted_po_pivot_group_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@pivotgroupdesc", pivotGroupDesc);
            cmd.Parameters.AddWithValue("@postedmonth", postedMonth);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOReportPivotGroupWise(string startDate, string endDate, string postedMonth, string jOBNo, string pivotGroup)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_pivot_group_wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@postedmonth", postedMonth);
            cmd.Parameters.AddWithValue("@jobno", jOBNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPOItemReportPivotGroupWise(string unitName, string pONo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_item_report_pivot_group_wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@pono", pONo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int ImportSaleEstimateBudget(DataTable dtSaleEstimateBudget, string updateQuery, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_import_sale_estimate_budget";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblsaleestimatebudget", dtSaleEstimateBudget);
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

        public DataSet GetPivotGroupList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_pivot_group_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetExistedPivotGroupList(string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_existed_pivot_group_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@jobno", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int PostPOPivotGroup(DataTable dtPOPivotGroup, string updateQuery, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_po_pivot_group";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblpopivotgroup", dtPOPivotGroup);
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


        public int ApprovePOPivotGroup(string recordIDs, int statusID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_approve_posted_po_pivot_group";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordids", recordIDs);
            cmd.Parameters.AddWithValue("@statusid", statusID);
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


        public int InsertPOPivotGroupLog(int unitID, string jobNo, string PONo, string newPivotGroup, string newPivotGroupDesc, string oldPivotGroup, string oldPivotGroupDesc, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_po_pivot_group_log";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@newpivotgroup", newPivotGroup);
            cmd.Parameters.AddWithValue("@newpivotgroupdesc", newPivotGroupDesc);
            cmd.Parameters.AddWithValue("@oldpivotgroup", oldPivotGroup);
            cmd.Parameters.AddWithValue("@oldpivotgroupdesc", oldPivotGroupDesc);
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



        public DataSet GetPODetailForPivotGroup(int companyID, string JOBNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_old_pivot_group_for_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@companyid", companyID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@pono", poNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetNewPivotGroupForPO(int unitID, string jobNo, string pivotGroup, string pivotGroupDesc)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_new_pivot_group_for_po";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@pivotgroupdesc", pivotGroupDesc);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetPivotGroupDetails(string jobNo, string pivotGroup)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_pivot_group_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int InsertPOPivotGroupLogOne(int unitID, string jobNo, string PONo, string newPivotGroup, string newPivotGroupDesc,
                                            string oldPivotGroup, string oldPivotGroupDesc,
                                            int isPostingFlag,
                                            int isPivotGroupLogFlag, string postingMonth,
                                            int postingYear, double poBudget, double saleEstimateBudget, double poValueINR,
                                            double deltaAsPerPOBudget, double deltaAsPerSaleEstimateBudget, double pendingCost,
                                            double savingCost, double savingOfOriginalEstimatePercentage,
                                            string likelyDeliveryDate, string plannedDateOfProcurement, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_po_pivot_group_log";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", PONo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@newpivotgroup", newPivotGroup);
            cmd.Parameters.AddWithValue("@newpivotgroupdesc", newPivotGroupDesc);
            cmd.Parameters.AddWithValue("@oldpivotgroup", oldPivotGroup);
            cmd.Parameters.AddWithValue("@oldpivotgroupdesc", oldPivotGroupDesc);

            cmd.Parameters.AddWithValue("@ispostingflag", isPostingFlag);
            cmd.Parameters.AddWithValue("@ispivotgrouplogflag", isPivotGroupLogFlag);

            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@postingyear", postingYear);
            cmd.Parameters.AddWithValue("@pobudget", poBudget);
            cmd.Parameters.AddWithValue("@saleestimatebudget", saleEstimateBudget);
            cmd.Parameters.AddWithValue("@povalueinr", poValueINR);
            cmd.Parameters.AddWithValue("@deltaasperpobudget", deltaAsPerPOBudget);
            cmd.Parameters.AddWithValue("@deltaaspersaleestimatebudget", deltaAsPerSaleEstimateBudget);
            cmd.Parameters.AddWithValue("@pendingcost", pendingCost);
            cmd.Parameters.AddWithValue("@savingcost", savingCost);
            cmd.Parameters.AddWithValue("@savingoforiginalestimatepercentage", savingOfOriginalEstimatePercentage);

            cmd.Parameters.AddWithValue("@likelydeliverydate", likelyDeliveryDate);
            cmd.Parameters.AddWithValue("@plandateofprocurement", plannedDateOfProcurement);

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

        public DataSet GetNewPivotGroupSaleEstimateBudgetAndDates(int unitID, string jobNo, string pivotGroupDesc)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_new_pivot_group_sale_estimate_budget_and_dates";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroupdesc", pivotGroupDesc);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int PostOneStream(DataTable dtTempOneStream, string updateQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_one_stream";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblOneStream", dtTempOneStream);
            cmd.Parameters.AddWithValue("@updatequery", updateQuery);
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


        public DataSet GetOneStream(string year, string period, string quarter, string glCodes)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_one_stream_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@period", period);
            cmd.Parameters.AddWithValue("@quarter", quarter);
            cmd.Parameters.AddWithValue("@glcodes", glCodes);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetOneStreamList(int year, int period, int quarter, string glAccount)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_one_stream_posted_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@period", period);
            cmd.Parameters.AddWithValue("@quarter", quarter);
            cmd.Parameters.AddWithValue("@glaccount", glAccount);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateOneStream(int recordID, double rawAmount, double convertedAmount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_one_stream";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@rawamount", rawAmount);
            cmd.Parameters.AddWithValue("@convertedamount", convertedAmount);
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


        public DataSet GetPOReportCostControl(string fromDate, string toDate,
                                            string poNo, string vendorName, string vendorCode, string unitName, string status, string JOBNo,
                                            double amount1, double amount2, string sign, int excluedCIDF)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_report_cost_control";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@amount1", amount1);
            cmd.Parameters.AddWithValue("@amount2", amount2);
            cmd.Parameters.AddWithValue("@sign", sign);
            cmd.Parameters.AddWithValue("@excludecidf", excluedCIDF);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        //RPB2  2022-11-17

        public DataSet GetSalesorderIntakeReportForRPB2(int unitId, string unitName, string fromDate, string toDate)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_salesorder_intake_report_for_rpb2";
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

        public DataSet GetRPB2SalesorderIntakeReport
             (
               string yearMonthSearch
             , int unitId
             , int revenueTypeId
             , int companyTypeId
             , string bu
             , string orderNumber
             , string yearMonthCompare
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_rpb2_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@yearMonthSearch", yearMonthSearch);
            cmd.Parameters.AddWithValue("@unitId", unitId);
            cmd.Parameters.AddWithValue("@revenueTypeId", revenueTypeId);
            cmd.Parameters.AddWithValue("@companyTypeId", companyTypeId);
            cmd.Parameters.AddWithValue("@bu", bu);
            cmd.Parameters.AddWithValue("@orderNumber", orderNumber);
            cmd.Parameters.AddWithValue("@yearMonthCompare", yearMonthCompare);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int PostRpb2Report(string yearMonthSearch, string yearMonthCompare, DataTable dtRpb2, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_rpb";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@yearMonth", yearMonthSearch);
            cmd.Parameters.AddWithValue("@comparedYearMonth", yearMonthCompare);
            cmd.Parameters.AddWithValue("@tblRpb", dtRpb2);
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


        public DataSet GetVendorRatingReport(int unitId, string dateType, string startDate, string endDate
                                           , string vendorCode, string vendorName

                                           , string signNoOfCountsForDelivery, double noOfCountsForDelivery1, double noOfCountsForDelivery2
                                           , string signNoOfCountsForQuality, double noOfCountsForQuality1, double noOfCountsForQuality2

                                           , string signNoOfRejectedDeliveries, double noOfRejectedDeliveries1, double noOfRejectedDeliveries2
                                           , string signQuality, double quality1, double quality2
                                           , string signDelivery, double delivery1, double delivery2
                                           , string signNoOfDelays, double noOfDelays1, double noOfDelays2
                                           , string signCommulative, double commulative1, double commulative2)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vendor_rating_report";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UnitId", unitId);


            cmd.Parameters.AddWithValue("@DateType", dateType);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);

            cmd.Parameters.AddWithValue("@SignNoOfCountsForDelivery", signNoOfCountsForDelivery);
            cmd.Parameters.AddWithValue("@NoOfCountsForDelivery1", noOfCountsForDelivery1);
            cmd.Parameters.AddWithValue("@NoOfCountsForDelivery2", noOfCountsForDelivery2);

            cmd.Parameters.AddWithValue("@SignNoOfCountsForQuality", signNoOfCountsForQuality);
            cmd.Parameters.AddWithValue("@NoOfCountsForQuality1", noOfCountsForQuality1);
            cmd.Parameters.AddWithValue("@NoOfCountsForQuality2", noOfCountsForQuality2);

            cmd.Parameters.AddWithValue("@SignNoOfRejectedDeliveries", signNoOfRejectedDeliveries);
            cmd.Parameters.AddWithValue("@NoOfRejectedDeliveries1", noOfRejectedDeliveries1);
            cmd.Parameters.AddWithValue("@NoOfRejectedDeliveries2", noOfRejectedDeliveries2);

            cmd.Parameters.AddWithValue("@SignQuality", signQuality);
            cmd.Parameters.AddWithValue("@Quality1", quality1);
            cmd.Parameters.AddWithValue("@Quality2", quality2);

            cmd.Parameters.AddWithValue("@SignDelivery", signDelivery);
            cmd.Parameters.AddWithValue("@Delivery1", delivery1);
            cmd.Parameters.AddWithValue("@Delivery2", delivery2);

            cmd.Parameters.AddWithValue("@SignNoOfDelays", signNoOfDelays);
            cmd.Parameters.AddWithValue("@NoOfDelays1", noOfDelays1);
            cmd.Parameters.AddWithValue("@NoOfDelays2", noOfDelays2);

            cmd.Parameters.AddWithValue("@SignCommulative", signCommulative);
            cmd.Parameters.AddWithValue("@Commulative1", commulative1);
            cmd.Parameters.AddWithValue("@Commulative2", commulative2);


            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetPurcaseOrderForcastReport(string finalDeliveryDate, string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_po_job_final_delivery_date_wise_forecast_report";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FinalDeliverDate", finalDeliveryDate);
            cmd.Parameters.AddWithValue("@JobNo", jobNo);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRNDetailReport(string fromDate
                                        , string toDate
                                        , string vendorCode
                                        , string unitName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mrn_detail_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@vendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@unitname", unitName);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public DataSet GetCreatedByList()
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



        public DataSet GetPoDocumentsList(int unitId,
                                              string dateType,
                                              string startDate,
                                              string endDate,
                                              string vendorCode,
                                              string vendorName,
                                              string poNumber,
                                              string jobNumber,
                                              string mrNumber,
                                              int attachedById,
                                              string CheckedBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbPO].[Sp_Get_Po_Documents_List]";
            //cmd.CommandText = "[dbPO].[Sp_Get_Po_Documents_List_02]";
            //cmd.CommandText = "[dbPO].[Sp_Get_Po_Documents_List_FOR_BUG_FIXING]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UnitId", unitId);
            cmd.Parameters.AddWithValue("@DateType", dateType);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@poNumber", poNumber);
            cmd.Parameters.AddWithValue("@jobNumber", jobNumber);
            cmd.Parameters.AddWithValue("@attachedById", attachedById);
            cmd.Parameters.AddWithValue("@MRNumber", mrNumber);
            cmd.Parameters.AddWithValue("@CheckedBy", CheckedBy);



            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLateOTDReport(string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "get_project_delivery_report_02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

    }
}
