using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class LessonLearnt
    {

        public DataSet GetDepartment()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_department_LL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetInitiatedBy()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_LL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCustomer()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_customer_LL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetEmpListForLLReport()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_InitiatedBy_for_ll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetLessonLearntReportNew(string fromDate, string toDate, int empID, int departmentID, int unitID,
            string customerName, string jobNum, int equipmentID, int createdByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lesson_learnt_log_report01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@empid", empID);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@jobno", jobNum);
            cmd.Parameters.AddWithValue("@equipmentid", equipmentID);
            cmd.Parameters.AddWithValue("@createdbyid", createdByID);



            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetCreatedBy()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_CreatedBy_for_ll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetDeptListForLLReport()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_Department_for_ll_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        

        public DataSet GetEquipmentList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_equipment_LL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJOBDetailsForLOT(int companyID, string custCode, string jobNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lesson_learnt_customer_list_NEW_02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@companyid", companyID);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet Unit()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_unit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        //    public int InsertUpdateLessonLearnt(string lessonLearntDate, int employeeID, int departmentId, int unitId,
        //                                       string jobNo, string customerName, string customerCode,
        //                                       int equipmentId, string problemFacedRemark, string lessonLearntRemark )

        //        string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //        SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //        SqlCommand cmd = new SqlCommand();
        //        //SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
        //        cmd.CommandText = "InsertLessonLearnt";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = con;
        //        cmd.Parameters.AddWithValue("@Date", lessonLearntDate);
        //        cmd.Parameters.AddWithValue("@InitiatedByEmpRecordId", employeeID);
        //        cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
        //        cmd.Parameters.AddWithValue("@UnitId", unitId);
        //        cmd.Parameters.AddWithValue("@JobNo", jobNo);
        //        cmd.Parameters.AddWithValue("@CustomerName", customerName);
        //        cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
        //        cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
        //        cmd.Parameters.AddWithValue("@ProblemFacedRemark", problemFacedRemark);
        //        cmd.Parameters.AddWithValue("@LessonLearntRemark", lessonLearntRemark);



        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            cmd.ExecuteNonQuery();
        //            //int value = Convert.ToInt32(rcode.Value);
        //            con.Close();
        //            con.Dispose();
        //            return value;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }

        //}

        public int InsertUpdateLessonLearnt(string lessonLearntDate, int employeeID, int createdById, int departmentId, int unitId,
                                     string jobNo, string customerName, string customerCode,
                                     int equipmentId, string problemFacedRemark, string lessonLearntRemark)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            using (SqlConnection con = new SqlConnection(cipltmsconnectionstring))
            {
                SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
                using (SqlCommand cmd = new SqlCommand("InsertLessonLearnt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(lessonLearntDate));
                    cmd.Parameters.AddWithValue("@InitiatedByEmpRecordId", employeeID);
                    cmd.Parameters.AddWithValue("@CreatedByEmpRecordId", createdById);
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                    cmd.Parameters.AddWithValue("@UnitId", unitId);
                    cmd.Parameters.AddWithValue("@JobNo", jobNo);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    cmd.Parameters.AddWithValue("@ProblemFacedRemark", problemFacedRemark);
                    cmd.Parameters.AddWithValue("@LessonLearntRemark", lessonLearntRemark);
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

    }
}
