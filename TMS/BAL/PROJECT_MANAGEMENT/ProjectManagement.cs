using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.PROJECT_MANAGEMENT
{
    public class ProjectManagement
    {

        public string InsertUpdatePivotGroup
            (int pId
            , string name
            , string description
            , int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcodeVar = new SqlParameter("@rcodeVar", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
            cmd.CommandText = "dbProjM.Sp_Insert_Update_Pivot_Groups";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Pid", pId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
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

        public DataSet GetPivotGroupList(string code, string name, string description)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Pivot_Group_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetBomTypes()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_BOM_Types";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBomStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_BOM_Status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBomMrCreatedBy()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_BOM_Mr_Created_By";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetBomResponsibleFor()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_BOM_Responsible_For";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetPostedBOMList
            (string dateType
            , string startDate
            , string endDate
            , int unitID
            , int typeID
            , string statusID
            , int pivotGroupID
            , int responsibleForID
            , int isTcRequired
            , string mrNo
            , int mrCreatedById
            , string BomNo
            , string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Posted_BOM_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@DateType", dateType);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@UnitID", unitID);
            cmd.Parameters.AddWithValue("@TypeID", typeID);
            cmd.Parameters.AddWithValue("@StatusID", statusID);
            cmd.Parameters.AddWithValue("@PivotGroupID", pivotGroupID);
            cmd.Parameters.AddWithValue("@ResponsibleForID", responsibleForID);
            cmd.Parameters.AddWithValue("@IsTcRequired", isTcRequired);
            cmd.Parameters.AddWithValue("@MrNo", mrNo);
            cmd.Parameters.AddWithValue("@MrCreatedById", mrCreatedById);
            cmd.Parameters.AddWithValue("@BomNo", BomNo);
            cmd.Parameters.AddWithValue("@JobNo", jobNo);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostedBOMListToExport
            (string dateType
            , string startDate
            , string endDate
            , int unitID
            , int typeID
            , string statusID
            , int pivotGroupID
            , int responsibleForID
            , int isTcRequired
            , string mrNo
            , int mrCreatedById
            , string BomNo
            , string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Posted_BOM_List_To_Export";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@DateType", dateType);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@UnitID", unitID);
            cmd.Parameters.AddWithValue("@TypeID", typeID);
            cmd.Parameters.AddWithValue("@StatusID", statusID);
            cmd.Parameters.AddWithValue("@PivotGroupID", pivotGroupID);
            cmd.Parameters.AddWithValue("@ResponsibleForID", responsibleForID);
            cmd.Parameters.AddWithValue("@IsTcRequired", isTcRequired);
            cmd.Parameters.AddWithValue("@MrNo", mrNo);
            cmd.Parameters.AddWithValue("@MrCreatedById", mrCreatedById);
            cmd.Parameters.AddWithValue("@BomNo", BomNo);
            cmd.Parameters.AddWithValue("@JobNo", jobNo);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetBOMList
            (string startDate
            , string endDate
            , string BomNo
            , string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_BOM_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@BomNo", BomNo);
            cmd.Parameters.AddWithValue("@JobNo", jobNo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetProductListByBOMNo(string BomNo, int BomFid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Product_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BomNo", BomNo);
            cmd.Parameters.AddWithValue("@BomFid", BomFid);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public string InsertUpdateBOM(
              int Pid
            , int UnitFid
            , int TypeFid
            , string MrNo
            , string MrDate
            , int StatusFid
            , string BomNo
            , string BomDate
            , string JobNo
            , string DeliveryRequiredBy
            , string AcceptableVendor1
            , string AcceptableVendor2
            , string AcceptableVendor3
            , string AcceptableVendor4
            , string AcceptableVendor5
            , string RevisionNumber
            , double BudgetedCost
            , double EstimatedCost
            , string CostRelatedRemarks
            , int PivotGroupFid
            , int ResponsibleForBomFid
            , int IsTcRequired
            , int CreatedByFid
            , string CreatedRemarks
            , int IsSentForApproval
            , DataTable dtBomLine
        )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcodeVar = new SqlParameter("@rcodeVar", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
            cmd.CommandText = "dbProjM.Sp_Insert_Update_BOM";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", Pid);
            cmd.Parameters.AddWithValue("@UnitFid", UnitFid);
            cmd.Parameters.AddWithValue("@TypeFid", TypeFid);
            cmd.Parameters.AddWithValue("@MrNo", MrNo);
            cmd.Parameters.AddWithValue("@MrDate", MrDate);
            cmd.Parameters.AddWithValue("@StatusFid", StatusFid);
            cmd.Parameters.AddWithValue("@BomNo", BomNo);
            cmd.Parameters.AddWithValue("@BomDate", BomDate);
            cmd.Parameters.AddWithValue("@JobNo", JobNo);
            cmd.Parameters.AddWithValue("@DeliveryRequiredBy", DeliveryRequiredBy);
            cmd.Parameters.AddWithValue("@AcceptableVendor1", AcceptableVendor1);
            cmd.Parameters.AddWithValue("@AcceptableVendor2", AcceptableVendor2);
            cmd.Parameters.AddWithValue("@AcceptableVendor3", AcceptableVendor3);
            cmd.Parameters.AddWithValue("@AcceptableVendor4", AcceptableVendor4);
            cmd.Parameters.AddWithValue("@AcceptableVendor5", AcceptableVendor5);
            cmd.Parameters.AddWithValue("@RevisionNumber", RevisionNumber);
            cmd.Parameters.AddWithValue("@BudgetedCost", BudgetedCost);
            cmd.Parameters.AddWithValue("@EstimatedCost", EstimatedCost);
            cmd.Parameters.AddWithValue("@CostRelatedRemarks", CostRelatedRemarks);
            cmd.Parameters.AddWithValue("@PivotGroupFid", PivotGroupFid);
            cmd.Parameters.AddWithValue("@ResponsibleForBomFid", ResponsibleForBomFid);
            cmd.Parameters.AddWithValue("@IsTcRequired", IsTcRequired);
            cmd.Parameters.AddWithValue("@CreatedByFid", CreatedByFid);
            cmd.Parameters.AddWithValue("@CreatedRemarks", CreatedRemarks);
            cmd.Parameters.AddWithValue("@IsSentForApproval", IsSentForApproval);
            cmd.Parameters.AddWithValue("@tblBomLine", dtBomLine);

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


        public DataSet GetBomMailInfo(int PID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Posted_BOM_Mail_Info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@PID", PID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostedProductListByBOMPid(int BomPid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Posted_BOM_Line_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BomFid", BomPid);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPostedProductListByBOMPid(string BomNo, int BomFid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbProjM.Sp_Get_Posted_BOM_Line_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BomNo", BomNo);
            cmd.Parameters.AddWithValue("@BomFid", BomFid);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateStatus(int PID
                              , int StatusID
                              , string Remarks
                              , int CurrentUserFID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@Rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "dbProjM.Sp_Update_BOM_Status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", PID);
            cmd.Parameters.AddWithValue("@StatusFid", StatusID);
            cmd.Parameters.AddWithValue("@UserFid", CurrentUserFID);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);

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


        public int UpdateMailStatus(int PID
                                  , int StatusID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@Rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "dbProjM.Sp_Update_BOM_Mail_Status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Pid", PID);
            cmd.Parameters.AddWithValue("@StatusFid", StatusID);

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
