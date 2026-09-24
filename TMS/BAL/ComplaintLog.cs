using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BAL
{
    public class ComplaintLog
    {
        public DataSet GetCustomer()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_customer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendor()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vendor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetComplaintType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBusinessUnit()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_business_unit_for_cl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int InsertUpdateComplaintLog(int complaintLogID, string customerCode, string customerName,
                                            string jobNo, string poNo, string itemName,
                                            string modelNo, int warranty, int natureOfComplaintID, int complaintLoggedByID,
                                            string complaintLoggedDate, string vendorCode, string vendorName, string remarks,
                                            int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_complaint_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@modelno", modelNo);
            cmd.Parameters.AddWithValue("@warranty", warranty);
            cmd.Parameters.AddWithValue("@natureofcomplaintid", natureOfComplaintID);
            cmd.Parameters.AddWithValue("@complaintloggedbyid", complaintLoggedByID);
            cmd.Parameters.AddWithValue("@complaintloggeddate", complaintLoggedDate);
            cmd.Parameters.AddWithValue("@vendorcode", vendorCode);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
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

        public DataSet GetComplaintLogList(string fromDate, string toDate, string complaintLogNo, string status, int employeeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_list01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@complaintlogno", complaintLogNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", employeeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetComplaintLogReport(string fromDate, string toDate, string complaintLogNo, string status, int employeeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@complaintlogno", complaintLogNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", employeeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet ComplaintLogInfo(int complaintLogID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_by_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateComplaintLogStatus(int complaintLogID, string solution, int closedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_complaint_log_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@solution", solution);
            cmd.Parameters.AddWithValue("@closedby", closedBy);
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



        //------------------------------------------------------------------------------


        public DataSet GetCustomerJobNoDetails(string customerCode, string jobNo, string poNo,
                                            string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_customer_job_list_for_complaint_log01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertUpdateComplaintLogNew(int complaintLogID, string customerName, string customerCode, string address, string location, string plant,
                                            string complaintDescription, string jobNo, string poNo, string itemName, string modelNo,
                                            int warranty, int natureOfComplaintID, string manufacturerName, int responsibleDepartmentID,
                                            int responsiblePersonID, string proposedActions, string targetCompletionDate,
                                            string actualCompletionDate, string lessonLearned, string attachmentOneFileName, byte[] attachmentOneBytes,
                                            string attachmentTwoFileName, byte[] attachmentTwoBytes, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_complaint_log01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@plant", plant);
            cmd.Parameters.AddWithValue("@complaintdescription", complaintDescription);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@modelno", modelNo);
            cmd.Parameters.AddWithValue("@warranty", warranty);
            cmd.Parameters.AddWithValue("@natureofcomplaintid", natureOfComplaintID);
            cmd.Parameters.AddWithValue("@manufacturername", manufacturerName);
            cmd.Parameters.AddWithValue("@responsibledepartmentid", responsibleDepartmentID);
            cmd.Parameters.AddWithValue("@responsiblepersonid", responsiblePersonID);
            cmd.Parameters.AddWithValue("@proposedactions", proposedActions);
            cmd.Parameters.AddWithValue("@targetcompletiondate", targetCompletionDate);
            cmd.Parameters.AddWithValue("@actualcompletiondate", actualCompletionDate);
            cmd.Parameters.AddWithValue("@lessonlearned", lessonLearned);

            cmd.Parameters.AddWithValue("@attachmentonefilename", attachmentOneFileName);
            if (attachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@attachmentonebytes", attachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@attachmentonebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@attachmenttwofilename", attachmentTwoFileName);
            if (attachmentTwoBytes != null)
                cmd.Parameters.AddWithValue("@attachmenttwobytes", attachmentTwoBytes);
            else
                cmd.Parameters.AddWithValue("@attachmenttwobytes", System.Data.SqlTypes.SqlBinary.Null);

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



        public DataSet GetCustomerAddressList(string customerName, string customerCode, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_customer_address_list_for_complaint_log01";
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

        public DataSet GetComplaintLogDetails(int complaintLogID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        //-------------------------------------------------------------------


        public DataSet GetTypeofService()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_type_of_service";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetComplaintLogInfo(int complaintID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_details01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@complaintlogid", complaintID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetComplaintLogInfoNew(int complaintID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_details02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@complaintlogid", complaintID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertUpdateComplaintLogNewOne(int complaintLogID, string complaintReveivedDate, string customerName, string customerCode,
                                           string address, string location, string plant,
                                           string jobNo, string poNo, string itemName, string modelNo, int serviceTypeID, string complaintDescription,
                                           string attachmentOneFileName, byte[] attachmentOneBytes,
                                           string attachmentTwoFileName, byte[] attachmentTwoBytes, int createdBy)//string lessonLearned,
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_complaint_log02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@complaintrcvddate", complaintReveivedDate);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@plant", plant);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@modelno", modelNo);
            cmd.Parameters.AddWithValue("@servicetypeid", serviceTypeID);
            cmd.Parameters.AddWithValue("@complaintdesc", complaintDescription);
            // cmd.Parameters.AddWithValue("@lessonlearned", lessonLearned);

            cmd.Parameters.AddWithValue("@attachmentonefilename", attachmentOneFileName);
            if (attachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@attachmentonebytes", attachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@attachmentonebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachmenttwofilename", attachmentTwoFileName);
            if (attachmentTwoBytes != null)
                cmd.Parameters.AddWithValue("@attachmenttwobytes", attachmentTwoBytes);
            else
                cmd.Parameters.AddWithValue("@attachmenttwobytes", System.Data.SqlTypes.SqlBinary.Null);


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

        public int AssignComplaintLog(int complaintLogID, int responsibleDepartment, int responsiblePerson, string lessonLearned, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_assign_complaint_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@responsibledepartment", responsibleDepartment);
            cmd.Parameters.AddWithValue("@responsibleperson", responsiblePerson);
            cmd.Parameters.AddWithValue("@lessonlearned", lessonLearned);
            cmd.Parameters.AddWithValue("@assignedby", createdBy);
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

        public int AssignComplaintLogNew(int complaintLogID, int actID, int responsibleDepartment, int responsiblePerson, string targetCompletionDate, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_assign_complaint_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@responsibledepartment", responsibleDepartment);
            cmd.Parameters.AddWithValue("@responsibleperson", responsiblePerson);
            cmd.Parameters.AddWithValue("@targetcompletiondate", targetCompletionDate);
            cmd.Parameters.AddWithValue("@assignedby", createdBy);
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

        public int ProcessComplaintLog(int complaintLogId, int projectCategoryID, string complaintDescription, string manufacturerName,
                                        string proposedActions, string rootCause, string targetCompletionDate,
                                        string actualCompletionDate, string lessonLearned, string correctiveAction,
                                        string attachmentOneFileName, byte[] attachmentOneBytes,
                                        string attachmentTwoFileName, byte[] attachmentTwoBytes, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_process_complaint_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogId);
            cmd.Parameters.AddWithValue("@projectcategoryid", projectCategoryID);
            cmd.Parameters.AddWithValue("@complaintdescription", complaintDescription);
            cmd.Parameters.AddWithValue("@manufacturername", manufacturerName);
            cmd.Parameters.AddWithValue("@proposedactions", proposedActions);
            cmd.Parameters.AddWithValue("@rootcause", rootCause);
            cmd.Parameters.AddWithValue("@targetcompletiondate", targetCompletionDate);
            cmd.Parameters.AddWithValue("@actualcompletiondate", actualCompletionDate);
            cmd.Parameters.AddWithValue("@lessonlearned", lessonLearned);
            cmd.Parameters.AddWithValue("@correctiveaction", correctiveAction);


            cmd.Parameters.AddWithValue("@attachmentonefilename", attachmentOneFileName);
            if (attachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@attachmentonebytes", attachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@attachmentonebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachmenttwofilename", attachmentTwoFileName);
            if (attachmentTwoBytes != null)
                cmd.Parameters.AddWithValue("@attachmenttwobytes", attachmentTwoBytes);
            else
                cmd.Parameters.AddWithValue("@attachmenttwobytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@processedby", createdBy);
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

        public int ResolveComplaintLog(int complaintLogID, int businessUnitID, string complaintDescription, string manufacturerName,
                                       string proposedActions, string rootCause,
                                       string actualCompletionDate, string respPersonLessonLearnt, string correctiveAction,
                                       int statementID,
                                       string fileUploadVisitReportOneFileName, byte[] fileUploadVisitReportOneBytes,
                                       string fileUploadVisitReportTwoFileName, byte[] fileUploadVisitReportTwoBytes,
                                       string fileUploadVisitReportThreeFileName, byte[] fileUploadVisitReportThreeBytes,
                                       string attachmentOneFileName, byte[] attachmentOneBytes,
                                       string attachmentTwoFileName, byte[] attachmentTwoBytes, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_resolve_complaint_log_01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@businessunitid", businessUnitID);
            cmd.Parameters.AddWithValue("@complaintdescription", complaintDescription);
            cmd.Parameters.AddWithValue("@manufacturername", manufacturerName);
            cmd.Parameters.AddWithValue("@proposedactions", proposedActions);
            cmd.Parameters.AddWithValue("@rootcause", rootCause);
            cmd.Parameters.AddWithValue("@actualcompletiondate", actualCompletionDate);
            cmd.Parameters.AddWithValue("@resppersonlessonlearnt", respPersonLessonLearnt);
            cmd.Parameters.AddWithValue("@correctiveaction", correctiveAction);


            cmd.Parameters.AddWithValue("@travelstatementid", statementID);

            cmd.Parameters.AddWithValue("@visitrptsummaryonefilename", fileUploadVisitReportOneFileName);
            if (fileUploadVisitReportOneBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummaryonebytes", fileUploadVisitReportOneBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummaryonebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@visitrptsummarytwofilename", fileUploadVisitReportTwoFileName);
            if (fileUploadVisitReportTwoBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummarytwobytes", fileUploadVisitReportTwoBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummarytwobytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@visitrptsummarythreefilename", fileUploadVisitReportThreeFileName);
            if (fileUploadVisitReportThreeBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummarythreebytes", fileUploadVisitReportThreeBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummarythreebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachmentonefilename", attachmentOneFileName);
            if (attachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@attachmentonebytes", attachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@attachmentonebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachmenttwofilename", attachmentTwoFileName);
            if (attachmentTwoBytes != null)
                cmd.Parameters.AddWithValue("@attachmenttwobytes", attachmentTwoBytes);
            else
                cmd.Parameters.AddWithValue("@attachmenttwobytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@processedby", createdBy);
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

        public int ApproveAndCloseComplaintLog(int complaintLogID, int actID, string respDeptHODLessonLearnt,string serviceDeptHODLessonLearnt, 
                                                int createdBy, int responsibleDepartment)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_approve_and_close_complaint_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@respdepthodlessonlearnt", respDeptHODLessonLearnt);
            cmd.Parameters.AddWithValue("@servicedepthodlessonlearnt", serviceDeptHODLessonLearnt);

            cmd.Parameters.AddWithValue("@closedby", createdBy);
            cmd.Parameters.AddWithValue("@responsibledepartment", responsibleDepartment);
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

        public int CloseComplaintLog(int complaintLogID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_close_complaint_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            cmd.Parameters.AddWithValue("@closedby", createdBy);
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

        public DataSet GetComplaingLogStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetComplaintLogListNew(string fromDate, string toDate, string complaintLogNo, string customerName, int statusID, int employeeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_list02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@complaintlogno", complaintLogNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@employeeid", employeeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetComplaintLogReportNew(string fromDate, string toDate, string complaintLogNo, string customerName, int statusID, int employeeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_report01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@complaintlogno", complaintLogNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@employeeid", employeeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAttachedFiles(int complaintLogID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complait_log_docs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@complaitlogid", complaintLogID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int UpdateComplaintLogMailStatus(int complaintLogId, int actID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_complaint_log_send_mail_value";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogId);
            cmd.Parameters.AddWithValue("@actid", actID);
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

        public DataSet GetComplaintLogForPDF(int complaintLogID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_complaint_log_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@complaintlogid", complaintLogID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //----------------------------------------------------------------


        public DataSet GetDepartmentForComplaintLog()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_department_for_cl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetEmployeeListForComplaintLog()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_responsibleperson_for_cl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSanctionNo(int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sanction_no_by_emprecordid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
    }
}


