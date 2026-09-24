using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class Ticket
    {
        public DataSet GetTicketType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tickettype";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int CreateAndUpdateTicket(int ticketId, int ticketTypeID, string attachmentOneFileName, byte[] attachmentOneBytes,
                        string ticketDescription, int empRecordID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_ticket01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ticketid", ticketId);
            cmd.Parameters.AddWithValue("@tickettypeid", ticketTypeID);

            cmd.Parameters.AddWithValue("@attachmentonefilename", attachmentOneFileName);            
            if (attachmentOneBytes != null)
                cmd.Parameters.AddWithValue("@attachmentonebytes", attachmentOneBytes);
            else
                cmd.Parameters.AddWithValue("@attachmentonebytes", System.Data.SqlTypes.SqlBinary.Null);
            
            cmd.Parameters.AddWithValue("@ticketdescription", ticketDescription);
            cmd.Parameters.AddWithValue("@createdby", empRecordID);
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

        public int UpdateTicketStatus(int TicketID, int closedVal, int cancelledVal, string remarks, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_update_ticket_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ticketid", TicketID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@closedval", closedVal);
            cmd.Parameters.AddWithValue("@cancelledval", cancelledVal);
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

        public DataSet GetTicketList(string fromDate, string toDate, string ticketNo, string ticketStatus, int empRecordID)
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
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);

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
            cmd.CommandText = "sp_get_ticket_docs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@ticketid", ticketID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
    }
}
