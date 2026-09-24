using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL.HR
{
    public class Ticket
    {
        public DataSet GetTicketStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Status_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Type_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketSubtype(int ticketTypeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Subtype_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tickettypeid", ticketTypeID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public string CreateAndUpdateTicket
            (int ticketId
            , int ticketTypeID
            , int ticketSubtypeID
            , string attachmentOneFileName
            , byte[] attachmentOneBytes
            , string attachmentTwoFileName
            , byte[] attachmentTwoBytes
            , string attachmentThreeFileName
            , byte[] attachmentThreeBytes
            , string ticketDescription
            , string remarks
            , int empRecordID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcodeVar = new SqlParameter("@rcodeVar", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
            cmd.CommandText = "dbHr.SpTicket_Insert_Update_Ticket";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ticketid", ticketId);
            cmd.Parameters.AddWithValue("@tickettypeid", ticketTypeID);
            cmd.Parameters.AddWithValue("@ticketSubtypeID", ticketSubtypeID);

            cmd.Parameters.AddWithValue("@attachmentonefilename", attachmentOneFileName);
            if (attachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@attachmentonebytes", attachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@attachmentonebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@attachmenttwofilename",attachmentTwoFileName);
            if (attachmentTwoBytes != null)
                cmd.Parameters.AddWithValue("@attachmenttwobytes",attachmentTwoBytes);
            else
            {
                cmd.Parameters.AddWithValue("attachmenttwobytes", System.Data.SqlTypes.SqlBinary.Null);
            }

            cmd.Parameters.AddWithValue("@attachmentthreefilename", attachmentThreeFileName);
            if(attachmentThreeBytes != null)
            {
                cmd.Parameters.AddWithValue("attachmentthreebytes",attachmentThreeBytes);
            }
            else
            {
                cmd.Parameters.AddWithValue("attachmentthreebytes",System.Data.SqlTypes.SqlBinary.Null);
            }


            cmd.Parameters.AddWithValue("@ticketdescription", ticketDescription);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@createdby", empRecordID);
            cmd.Parameters.Add(rcodeVar);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                string value = Convert.ToString(rcodeVar.Value);
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

        public DataSet GetHrTicketMailInfo(int ticketId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Tickets_Mail_Info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@TicketId", ticketId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketByTicketID(int value)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ticket_by_ticketid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ticketid", value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketAndAllocateInfo(int ticketId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Ticket_And_User_Info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketListUser(string fromDate, string toDate, string ticketNo, string ticketStatus, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ticket_list_user";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@ticketno", ticketNo);
            cmd.Parameters.AddWithValue("@ticketstatus", ticketStatus);
            cmd.Parameters.AddWithValue("@createdby", empRecordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTicketStatus(int ticketId, int statusId, string remarks, int allocateToId, int allocateToIdOld,
            string allocatePriority, string closingAttachmentOneFileName , byte[] closingAttachmentOneBytes , string closingAttachmentTwoFileName,
            byte[] closingAttachmentTwoBytes , string closingAttachmentThreeFileName , byte[] closingAttachmentThreeBytes, string statusText, int isAddStatus,
            int isClose, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 20) { Direction = ParameterDirection.Output };
            cmd.CommandText = "dbHr.SpTicket_Update_Ticket_Status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            cmd.Parameters.AddWithValue("@statusId", statusId);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@allocateToId", allocateToId);
            cmd.Parameters.AddWithValue("@allocateToIdOld", allocateToIdOld);
            cmd.Parameters.AddWithValue("@AllocationPriority", allocatePriority);

            cmd.Parameters.AddWithValue("@closingAttachmentOneFileName", closingAttachmentOneFileName);
            if (closingAttachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@closingAttachmentOneBytes", closingAttachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@closingAttachmentOneBytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@closingAttachmentTwoFileName", closingAttachmentTwoFileName);
            if (closingAttachmentTwoBytes != null)
                cmd.Parameters.AddWithValue("@closingAttachmentTwoBytes", closingAttachmentTwoBytes);
            else
                cmd.Parameters.AddWithValue("@closingAttachmentTwoBytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@closingAttachmentThreeFileName", closingAttachmentThreeFileName);
            if (closingAttachmentThreeBytes != null)
                cmd.Parameters.AddWithValue("@closingAttachmentThreeBytes", closingAttachmentThreeBytes);
            else
                cmd.Parameters.AddWithValue("@closingAttachmentThreeBytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@Status", statusText);
            cmd.Parameters.AddWithValue("@IsAddStatus", isAddStatus);
            cmd.Parameters.AddWithValue("@IsClose", isClose);

            cmd.Parameters.AddWithValue("@modifiedBy", modifiedBy);

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



        public DataSet GetTicketListIT(string fromDate, string toDate, string ticketNo, string ticketStatus, string empName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ticket_list_it";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@ticketno", ticketNo);
            cmd.Parameters.AddWithValue("@ticketstatus", ticketStatus);
            cmd.Parameters.AddWithValue("@createdby", empName);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetEmployeeForTicket(int employeeRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_for_ticket";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@employeeid", employeeRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketList(string fromDate
                                   , string toDate
                                   , string ticketNo
                                   , string ticketStatus
                                   , int createdById
                                   , int allocatedToId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ticket_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@ticketno", ticketNo);
            cmd.Parameters.AddWithValue("@ticketstatus", ticketStatus);
            cmd.Parameters.AddWithValue("@emprecordid", createdById);
            cmd.Parameters.AddWithValue("@allocatedToId", allocatedToId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketReport(string fromDate, string toDate, string ticketNo, string ticketStatus, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ticket_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@ticketno", ticketNo);
            cmd.Parameters.AddWithValue("@ticketstatus", ticketStatus);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTicketStatusNew(int TicketID, int actID, string remarks, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_update_ticket_status_01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ticketid", TicketID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int value = cmd.ExecuteNonQuery();
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

        public DataSet GetAttachedFiles(int ticketID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Ticket_Docs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@ticketid", ticketID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTicketList(string DateTypeS
                                    , string fromDateS
                                    , string toDateS
                                    , string ticketNoS
                                    , int ticketStatusIdS
                                    , int ticketTypeIdS
                                    , int ticketSubtypeIdS
                                    , string insuranceNum
                                    , int createdByIdS
                                    , int allocatedToIdS
                                    , int teamMemberID
                                    , string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Tickets_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateType", DateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", fromDateS);
            cmd.Parameters.AddWithValue("@EndDate", toDateS);
            cmd.Parameters.AddWithValue("@ticketNo", ticketNoS);
            cmd.Parameters.AddWithValue("@ticketStatusId", ticketStatusIdS);
            cmd.Parameters.AddWithValue("@ticketTypeId", ticketTypeIdS);
            cmd.Parameters.AddWithValue("@ticketSubtypeId", ticketSubtypeIdS);
            cmd.Parameters.AddWithValue("@insuranceNum", insuranceNum);
            cmd.Parameters.AddWithValue("@empRecordId", createdByIdS);
            cmd.Parameters.AddWithValue("@allocatedToId", allocatedToIdS);
            cmd.Parameters.AddWithValue("@teamMemberID", teamMemberID);
            cmd.Parameters.AddWithValue("@teamMembers", teamMembers);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetTicketReport(string DateTypeS
                                    , string fromDateS
                                    , string toDateS
                                    , string ticketNoS
                                    , int ticketStatusIdS
                                    , int ticketTypeIdS
                                    , int ticketSubtypeIdS
                                    , int createdByIdS
                                    , int allocatedToIdS
                                    , int teamMemberID
                                    , string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Tickets_Report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@DateType", DateTypeS);
            cmd.Parameters.AddWithValue("@StartDate", fromDateS);
            cmd.Parameters.AddWithValue("@EndDate", toDateS);
            cmd.Parameters.AddWithValue("@ticketNo", ticketNoS);
            cmd.Parameters.AddWithValue("@ticketStatusId", ticketStatusIdS);
            cmd.Parameters.AddWithValue("@ticketTypeId", ticketTypeIdS);
            cmd.Parameters.AddWithValue("@ticketSubtypeId", ticketSubtypeIdS);
            cmd.Parameters.AddWithValue("@empRecordId", createdByIdS);
            cmd.Parameters.AddWithValue("@allocatedToId", allocatedToIdS);
            cmd.Parameters.AddWithValue("@teamMemberID", teamMemberID);
            cmd.Parameters.AddWithValue("@teamMembers", teamMembers);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAllocatedToList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbHr.SpTicket_Get_Allocated_To_List";
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
