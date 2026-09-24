using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BAL
{
    public class MachineScheduling
    {

        public int AddUpdateActivity(int recordID, string activityName, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_insert_update_activity";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@activityname", activityName);
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

        public int AddUpdateMachine(int recordID, string machineName, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_inert_update_machine";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@machinename", machineName);
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



        public DataSet GetMachines(string machineName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_machine_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@machinename", machineName);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetScheduleType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetWorkers()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_workers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetActivities(string activityName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_activity_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@activityname", activityName);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMachineActivity(int activityID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_machine_activities";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AddMachineActivity(int recordID, int activityID, int machineID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_add_update_machine_activity";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            cmd.Parameters.AddWithValue("@machineid", machineID);
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

        public DataSet GetMachineSchedulingReport(string fromDate, string toDate, int dateTypeID, string dateFilterSign,
            int unitID, string jobNo, string tagNo, string itemInLOT, string allocatedDrawingCode, int statusID,
            string lOTNo, string drawingNo, string equipment, int machineID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_machine_scheduling_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@datetypeid", dateTypeID);
            cmd.Parameters.AddWithValue("@datefiltersign", dateFilterSign);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@tagno", tagNo);
            cmd.Parameters.AddWithValue("@iteminlot", itemInLOT);
            cmd.Parameters.AddWithValue("@allocateddrawingcode", allocatedDrawingCode);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@lotno", lOTNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            cmd.Parameters.AddWithValue("@machineid", machineID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMachineActivityList(int activityID, int machineID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_machine_activity_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBDetailsForLMS(int companyID, string jobNo, string lOTNo, int typeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_lot_job_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@tfno", lOTNo);
            cmd.Parameters.AddWithValue("@typeid", typeId);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBDetailsForLMS(int companyID, string jobNo, string lOTNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_lot_job_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@tfno", lOTNo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetDrawingDetailsForLMS(string jobNo, string lOTNo, string drawingNo, string equipment)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_lot_drawing_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@tfno", lOTNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertAssignedItemsForMachineSch(int recordID, DataTable dtSubitems, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_insert_assign_items_for_ms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@tblassigneditems", dtSubitems);
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

        public DataSet GetAssignedItemsList(int companyID, string LOTNo, string jobNo
                                          , string drawingNo, string equipment,int statusId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_items_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            cmd.Parameters.AddWithValue("@statusId", statusId);
            
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDrawingFiles(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_drawing_files";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@recordid", recordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductionManagers(int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_production_managers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertMachineSchedulingDetails(int recordID, DataTable dtMachinesDetails, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_insert_machine_scheduling_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@tblmachinesdetails", dtMachinesDetails);
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


        public int AddCompletedQuantity(int recordID, int activityID, int machineID, int quantity, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_insert_completed_quantity_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@msrecordid", recordID);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@completedremarks", remarks);
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

        public DataSet GetAssignedActivityList(int asisgnedRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_activity_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@assignedrecordid", asisgnedRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        

        public DataSet GetAssignedMachineList(int asisgnedRecordID, int activityID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_machine_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@assignedrecordid", asisgnedRecordID);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAssignedItemsListForQuantityUpdation(int companyID, string LOTNo, string jobNo, string drawingNo, string equipment)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_items_list_for_quantity_updation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetScheduledMachineList(int companyID, string LOTNo, string jobNo, string drawingNo, string equipment)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_mahines_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMachineScheduledDatesList(int machineID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_machine_scheculed_dates_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAssignedItemsListForachineSch(int companyID, string LOTNo, string jobNo, string drawingNo, string equipment)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_items_list_for_ms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSIDrawingFiles(int lotTFSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_lot_drawing_files";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@lottfsubitemid", lotTFSubitemID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataTable GetMachineShopInchargeList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_machine_shop_incharge";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;
        }

        public int UpdateMailStatus(DataTable dtUpdateMailStatus, int mailTypeID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_update_machine_sch_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@tblassigneditems", dtUpdateMailStatus);
            cmd.Parameters.AddWithValue("@mailtypeid", mailTypeID);
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

        public int GetTableCounts()
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_drawing_counts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int value = Convert.ToInt32(cmd.ExecuteScalar());
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

        public int UpdateStatus(int recordID, int statusID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_update_status";
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

        public DataSet GetMailInfo(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@recordid", recordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMachineScheduledDatesList(DataTable dtTemp, int activityID, string machineID, string fromDate, string toDate)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_dates_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@dttable", dtTemp);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int SaveScheduleData(int recordID, string commitedDateByShopIncharge, string dateOfReceiptOfMaterial, DataTable dt, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_insert_machine_scheduling_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@commiteddatebyShopincharge", commitedDateByShopIncharge);
            cmd.Parameters.AddWithValue("@daterfreceiptofmaterial", dateOfReceiptOfMaterial);
            cmd.Parameters.AddWithValue("@tblassigneditems", dt);
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

        public DataSet GetScheduledListForCompletion(int recordID, string fromDate, string toDate, int activityID, int machineID, int typeOfWorkID, int workerID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_machine_list_for_completion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            cmd.Parameters.AddWithValue("@typeofworkid", typeOfWorkID);
            cmd.Parameters.AddWithValue("@workerid", workerID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetScheduledListForViewDetails(int recordID, string fromDate, string toDate, int activityID, int machineID, int typeOfWorkID, int workerID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_machine_list_for_view_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@activityid", activityID);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            cmd.Parameters.AddWithValue("@typeofworkid", typeOfWorkID);
            cmd.Parameters.AddWithValue("@workerid", workerID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int CompleteScheduledDetails(int recordID, string completionDate, DataTable dt, int rowCounts, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_complete_machine_scheduling";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@completiondate", completionDate);
            cmd.Parameters.AddWithValue("@tblscheduledmachines", dt);
            cmd.Parameters.AddWithValue("@counts", rowCounts);
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

        public int ModifyWorkerDetails(int recordID, DataTable dt, int rowCounts, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_modify_worker_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@tblscheduledmachines", dt);
            cmd.Parameters.AddWithValue("@counts", rowCounts);
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

        public int UpdateAcceptanceMailStatus(int recordID, int statusID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_update_machine_sch_acceptance_mail_status";
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

        public int QualityInspection(int recordID, int acceptedQuantity, int rejectedQuantity, string acceptedRemarks, string rejectedRemarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_quality_inspection";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@acceptedquantity", acceptedQuantity);
            cmd.Parameters.AddWithValue("@rejectedquantity", rejectedQuantity);
            cmd.Parameters.AddWithValue("@acceptedremarks", acceptedRemarks);
            cmd.Parameters.AddWithValue("@rejectedremarks", rejectedRemarks);
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




        public DataSet GetMachineAllocatedDrawingReport(string fromDate, string toDate, int dateTypeID, string dateFilterSign, int unitID
                                                        , int statusID
                                                        , string jobNo, string tagNo, string itemInLOT, string allocatedCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_allocated_drawings_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@datetypeid", dateTypeID);
            cmd.Parameters.AddWithValue("@datefiltersign", dateFilterSign);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@tagno", tagNo);
            cmd.Parameters.AddWithValue("@iteminlot", itemInLOT);
            cmd.Parameters.AddWithValue("@allocateddrawingcode", allocatedCode);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetStatusList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_status_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //2022-11-23

        public DataSet GetCategoryList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_category_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAssignedMachineList(int companyID, string LOTNo, string jobNo, string drawingNo, string equipment)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_machine_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@equipment", equipment);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetAssignedMachineList(int machineID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_machine_list01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@machineId", machineID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetScheduledMachine(int machineID,string fromDate)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_scheduled_machine";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@machineid", machineID);
            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int SaveRescheduleData(string commitedDateByShopIncharge
                                     , string dateOfReceiptOfMaterial
                                     , DataTable dt
                                     ,string recoedIdsToRemove 
                                     , int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_ms_insert_machine_rescheduling_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            
            cmd.Parameters.AddWithValue("@commiteddatebyShopincharge", commitedDateByShopIncharge);
            cmd.Parameters.AddWithValue("@daterfreceiptofmaterial", dateOfReceiptOfMaterial);
            cmd.Parameters.AddWithValue("@tblassigneditems", dt);
            cmd.Parameters.AddWithValue("@recoedIdsToRemove", recoedIdsToRemove);
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

        public DataSet GetAssignedDrawingsList(string jobNo, string drawingNo, string itemName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ms_get_assigned_items_for_rescheduling";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@jobNo", jobNo);
            cmd.Parameters.AddWithValue("@drawingNo", drawingNo);
            cmd.Parameters.AddWithValue("@itemName", itemName);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
    }
}
