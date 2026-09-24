using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class VendorCustomerManagement
    {
        public DataSet GetMasterTablesList(int tableTypeId
                                          , int itemCategoryId
                                          , int itemSubcategoryId
                                          , string name
                                          , string description)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_master_tables_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@tableTypeId", tableTypeId);
            cmd.Parameters.AddWithValue("@itemCategoryId", itemCategoryId);
            cmd.Parameters.AddWithValue("@itemSubcategoryId", itemSubcategoryId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetItemSubCategorys(int itemSubcategoryId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_item_subcategorys]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@ItemCategoryId", itemSubcategoryId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetItemCategorys()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_item_categorys]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetItemSubcategorys()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_item_categorys]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetRelationTypes()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_relation_types]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetEntityTypes()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_entity_types]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetResponsibles()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_responsible]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMSMEStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_msme_status]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GettApproverTypes()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_approver_types]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCategorys()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendor_categorys]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddUpdateEmailTemplates(
                                            int pid
                                        ,   int entityTypeId
                                        ,   int statusId
                                        ,   string subject
                                        ,   string bodyline
                                        ,   string salutation
                                        ,   string closingLine
                                        ,   string link
                                        ,   string cc
                                        ,   string bcc
                                        ,   int createdBy
                                        )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_save_email_template]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);
            cmd.Parameters.AddWithValue("@statusId", statusId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@bodyLine", bodyline);
            cmd.Parameters.AddWithValue("@salutation", salutation);
            cmd.Parameters.AddWithValue("@closingLine", closingLine);
            cmd.Parameters.AddWithValue("@link", link);
            cmd.Parameters.AddWithValue("@cc", cc);
            cmd.Parameters.AddWithValue("@bcc", bcc);
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

        public int RegisterVendorOLD(
              string VendorName
            , int CategoryId
            , string Gstin
            , string Pan
            , int CreditDays
            , double CreditLimit
            , int IsMsmed
            , int IsIsoCertified
            , int IsTechnicalDetailsReceived
            , int IsVisitByQa
            , int IsGovernment
            , int ResponsibleId
            , int VendorTypeId
            , int ItemCategoryFid
            , int ItemSubcategoryFid
            , int EntityTypeId
            , DataTable AddressTable
            , DataTable BankTable
            , DataTable ContactPersonTable
            , DataTable DOCsTable
            , int SavingType
            , string Remarks
            , int CreatedBy
        )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_save_vendor]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@CategoryFid", CategoryId);
            cmd.Parameters.AddWithValue("@Gstin", Gstin);
            cmd.Parameters.AddWithValue("@Pan", Pan);
            cmd.Parameters.AddWithValue("@CreditDays", CreditDays);
            cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
            cmd.Parameters.AddWithValue("@IsMsmed", IsMsmed);
            cmd.Parameters.AddWithValue("@IsIsoCertified", IsIsoCertified);
            cmd.Parameters.AddWithValue("@IsTechnicalDetailsReceived", IsTechnicalDetailsReceived);
            cmd.Parameters.AddWithValue("@IsVisitByQa", IsVisitByQa);
            cmd.Parameters.AddWithValue("@IsGovernment", IsGovernment);
            cmd.Parameters.AddWithValue("@ResponsibleFid", ResponsibleId);
            cmd.Parameters.AddWithValue("@VendorTypeFid", VendorTypeId);
            cmd.Parameters.AddWithValue("@ItemCategoryFid", ItemCategoryFid);
            cmd.Parameters.AddWithValue("@ItemSubcategoryFid", ItemSubcategoryFid);

            cmd.Parameters.AddWithValue("@EntityTypeFid", EntityTypeId);
            cmd.Parameters.AddWithValue("@AddressTable", AddressTable);
            cmd.Parameters.AddWithValue("@BankTable", BankTable);
            cmd.Parameters.AddWithValue("@ContactPersonTable", ContactPersonTable);
            cmd.Parameters.AddWithValue("@DOCsTable", DOCsTable);
            cmd.Parameters.AddWithValue("@SavingType", SavingType);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            ;

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



        public int RegisterVendor(
              string VendorName
            , int CategoryId
            , int MSMEStatusId
            //, string Gstin
            , string Pan
            , int CreditDays
            , double CreditLimit
            , int IsAssociated
            , int IsIsoCertified
            , int IsTechnicalDetailsReceived
            , int IsVisitByQa
            , int IsGovernment
            , int IsOneTime
            , int ResponsibleId
            , int VendorTypeId
            , int ItemCategoryFid
            , int ItemSubcategoryFid
            , int EntityTypeId
            , DataTable AddressTable
            , DataTable BankTable
            , DataTable ContactPersonTable
            , DataTable DOCsTable
            , int SavingType
            , string Remarks
            , int CreatedBy
            , int CheckerId
        )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_save_vendor]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@CategoryFid", CategoryId);
            cmd.Parameters.AddWithValue("@MSMEStatusFid", MSMEStatusId);
            //cmd.Parameters.AddWithValue("@Gstin", Gstin);
            cmd.Parameters.AddWithValue("@Pan", Pan);
            cmd.Parameters.AddWithValue("@CreditDays", CreditDays);
            cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
            cmd.Parameters.AddWithValue("@IsAssociated", IsAssociated);
            cmd.Parameters.AddWithValue("@IsIsoCertified", IsIsoCertified);
            cmd.Parameters.AddWithValue("@IsTechnicalDetailsReceived", IsTechnicalDetailsReceived);
            cmd.Parameters.AddWithValue("@IsVisitByQa", IsVisitByQa);
            cmd.Parameters.AddWithValue("@IsGovernment", IsGovernment);
            cmd.Parameters.AddWithValue("@IsOneTime", IsOneTime);
            cmd.Parameters.AddWithValue("@ResponsibleFid", ResponsibleId);
            cmd.Parameters.AddWithValue("@VendorTypeFid", VendorTypeId);
            cmd.Parameters.AddWithValue("@ItemCategoryFid", ItemCategoryFid);
            cmd.Parameters.AddWithValue("@ItemSubcategoryFid", ItemSubcategoryFid);

            cmd.Parameters.AddWithValue("@EntityTypeFid", EntityTypeId);
            cmd.Parameters.AddWithValue("@AddressTable", AddressTable);
            cmd.Parameters.AddWithValue("@BankTable", BankTable);
            cmd.Parameters.AddWithValue("@ContactPersonTable", ContactPersonTable);
            cmd.Parameters.AddWithValue("@DOCsTable", DOCsTable);
            cmd.Parameters.AddWithValue("@SavingType", SavingType);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CheckerId", CheckerId);
            ;

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

        public int AddUpdateApprover(int pid, int entityTypeId, int approverTypeId, int approverId,int isDeleted, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_save_approver]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);
            cmd.Parameters.AddWithValue("@approverTypeId", approverTypeId);
            cmd.Parameters.AddWithValue("@approverId", approverId);            
            cmd.Parameters.AddWithValue("@isDeleted", isDeleted);            
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

        public DataSet GetApproversList(int entityTypeId, int approverTypeId, int approverId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_approvers_list]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);
            cmd.Parameters.AddWithValue("@approverTypeId", approverTypeId);
            cmd.Parameters.AddWithValue("@approverId", approverId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

       // public int UpdateVendorOLD(
       //      int Pid
       //    , int StatusId
       //    , string VendorName
       //    , int CategoryId
       //    , string Gstin
       //    , string Pan
       //    , int CreditDays
       //    , double CreditLimit
       //    , int IsMsmed
       //    , int IsIsoCertified
       //    , int IsTechnicalDetailsReceived
       //    , int IsVisitByQa
       //    , int IsGovernment
       //    , int ResponsibleId
       //    , int VendorTypeId
       //    , int ItemCategoryFid
       //    , int ItemSubcategoryFid
       //    , int EntityTypeId
       //    , DataTable AddressTable
       //    , DataTable BankTable
       //    , DataTable ContactPersonTable
       //    , DataTable DOCsTable
       //    , int SavingType
       //    , string Remarks
       //    , bool IsEditFlag
       //    , bool IsRevisionFlag
       //    , DataTable RevisionTypeIds
       //    , int CreatedBy
       //)
       // {
       //     string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
       //     SqlConnection con = new SqlConnection(cipltmsconnectionstring);
       //     SqlCommand cmd = new SqlCommand();
       //     SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };

       //     if (IsRevisionFlag)
       //     {
       //         cmd.CommandText = "[DbVCM].[sp_revise_vendor]";
                
       //     }
       //     else
       //     {
       //         cmd.CommandText = "[DbVCM].[sp_update_vendor]";
       //     }

            
       //     cmd.CommandType = CommandType.StoredProcedure;
       //     cmd.Connection = con;

       //     cmd.Parameters.AddWithValue("@PID", Pid);
       //     cmd.Parameters.AddWithValue("@StatusFid", StatusId);
            
       //     cmd.Parameters.AddWithValue("@VendorName", VendorName);
       //     cmd.Parameters.AddWithValue("@CategoryFid", CategoryId);
       //     cmd.Parameters.AddWithValue("@Gstin", Gstin);
       //     cmd.Parameters.AddWithValue("@Pan", Pan);
       //     cmd.Parameters.AddWithValue("@CreditDays", CreditDays);
       //     cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
       //     cmd.Parameters.AddWithValue("@IsMsmed", IsMsmed);
       //     cmd.Parameters.AddWithValue("@IsIsoCertified", IsIsoCertified);
       //     cmd.Parameters.AddWithValue("@IsTechnicalDetailsReceived", IsTechnicalDetailsReceived);
       //     cmd.Parameters.AddWithValue("@IsVisitByQa", IsVisitByQa);
       //     cmd.Parameters.AddWithValue("@IsGovernment", IsGovernment);
       //     cmd.Parameters.AddWithValue("@ResponsibleFid", ResponsibleId);
       //     cmd.Parameters.AddWithValue("@VendorTypeFid", VendorTypeId);
       //     cmd.Parameters.AddWithValue("@ItemCategoryFid", ItemCategoryFid);
       //     cmd.Parameters.AddWithValue("@ItemSubcategoryFid", ItemSubcategoryFid);

       //     cmd.Parameters.AddWithValue("@EntityTypeFid", EntityTypeId);
       //     cmd.Parameters.AddWithValue("@AddressTable", AddressTable);
       //     cmd.Parameters.AddWithValue("@BankTable", BankTable);
       //     cmd.Parameters.AddWithValue("@ContactPersonTable", ContactPersonTable);
       //     cmd.Parameters.AddWithValue("@DOCsTable", DOCsTable);
       //     cmd.Parameters.AddWithValue("@SavingType", SavingType);
       //     cmd.Parameters.AddWithValue("@Remarks", Remarks);
           
       //     if (IsRevisionFlag)
       //     {
       //         cmd.Parameters.AddWithValue("@RevisionTypeIds", RevisionTypeIds);
       //         cmd.Parameters.AddWithValue("@IsRevision", Convert.ToInt32(IsRevisionFlag));
       //     }
       //     else
       //     {
       //         cmd.Parameters.AddWithValue("@IsEdit", Convert.ToInt32(IsEditFlag));
       //     }


       //     cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);


       //     cmd.Parameters.Add(rcode);
       //     try
       //     {
       //         if (con.State == ConnectionState.Closed)
       //         {
       //             con.Open();
       //         }
       //         cmd.ExecuteNonQuery();
       //         int value = Convert.ToInt32(rcode.Value);
       //         con.Close();
       //         con.Dispose();
       //         return value;
       //     }
       //     catch (Exception ex)
       //     {
       //         throw ex;
       //     }
       //     finally
       //     {
       //         con.Close();
       //         con.Dispose();
       //     }
       // }




        public int UpdateVendor(
             int Pid
           , int StatusId
           , string VendorName
           , int CategoryId
           , int MSMEStatusId
           //, string Gstin
           , string Pan
           , int CreditDays
           , double CreditLimit
           , int IsAssociated
           , int IsIsoCertified
           , int IsTechnicalDetailsReceived
           , int IsVisitByQa
           , int IsGovernment
           , int IsOneTime
           , int ResponsibleId
           , int VendorTypeId
           , int ItemCategoryFid
           , int ItemSubcategoryFid
           , int EntityTypeId
           , DataTable AddressTable
           , DataTable BankTable
           , DataTable ContactPersonTable
           , DataTable DOCsTable
           , int SavingType
           , string Remarks    
           //, bool IsRevisionFlag
           , DataTable RevisionTypeIds
           , int CreatedBy
           , int CheckerId
       )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };

            //if (IsRevisionFlag)
            //{
            //    cmd.CommandText = "[DbVCM].[sp_revise_vendor]";
            //}
            //else
            //{
            //    cmd.CommandText = "[DbVCM].[sp_update_vendor]";
            //}

            cmd.CommandText = "[DbVCM].[sp_update_vendor]";


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@PID", Pid);
            cmd.Parameters.AddWithValue("@StatusFid", StatusId);

            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@CategoryFid", CategoryId);
            cmd.Parameters.AddWithValue("@MSMEStatusFid", MSMEStatusId);
            //cmd.Parameters.AddWithValue("@Gstin", Gstin);
            cmd.Parameters.AddWithValue("@Pan", Pan);
            cmd.Parameters.AddWithValue("@CreditDays", CreditDays);
            cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
            cmd.Parameters.AddWithValue("@IsAssociated", IsAssociated);
            cmd.Parameters.AddWithValue("@IsIsoCertified", IsIsoCertified);
            cmd.Parameters.AddWithValue("@IsTechnicalDetailsReceived", IsTechnicalDetailsReceived);
            cmd.Parameters.AddWithValue("@IsVisitByQa", IsVisitByQa);

            cmd.Parameters.AddWithValue("@IsGovernment", IsGovernment);
            cmd.Parameters.AddWithValue("@IsOneTime", IsOneTime);
            
            cmd.Parameters.AddWithValue("@ResponsibleFid", ResponsibleId);
            cmd.Parameters.AddWithValue("@VendorTypeFid", VendorTypeId);
            cmd.Parameters.AddWithValue("@ItemCategoryFid", ItemCategoryFid);
            cmd.Parameters.AddWithValue("@ItemSubcategoryFid", ItemSubcategoryFid);

            cmd.Parameters.AddWithValue("@EntityTypeFid", EntityTypeId);
            cmd.Parameters.AddWithValue("@AddressTable", AddressTable);
            cmd.Parameters.AddWithValue("@BankTable", BankTable);
            cmd.Parameters.AddWithValue("@ContactPersonTable", ContactPersonTable);
            cmd.Parameters.AddWithValue("@DOCsTable", DOCsTable);

            //if (IsRevisionFlag)
            //{
            //    cmd.Parameters.AddWithValue("@RevisionTypeIds", RevisionTypeIds);
            //}            

            cmd.Parameters.AddWithValue("@SavingType", SavingType);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CheckerId", CheckerId);


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


        public DataSet GetApproversById(int approverTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_approvers_by_id]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@approverTypeId", approverTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        // public int ReviseVendorOld(
        //      int Pid
        //    , int StatusId
        //    , string VendorName
        //    , int CategoryId
        //    , string Gstin
        //    , string Pan
        //    , int CreditDays
        //    , double CreditLimit
        //    , int IsMsmed
        //    , int IsIsoCertified
        //    , int IsTechnicalDetailsReceived
        //    , int IsVisitByQa
        //    , int IsGovernment
        //    , int ResponsibleId
        //    , int VendorTypeId
        //    , int ItemCategoryFid
        //    , int ItemSubcategoryFid
        //    , int EntityTypeId
        //    , DataTable AddressTable
        //    , DataTable BankTable
        //    , DataTable ContactPersonTable
        //    , DataTable DOCsTable
        //    , int SavingType
        //    , string Remarks
        //    , DataTable RevisionTypeIds
        //    , int CreatedBy
        //)
        // {
        //     string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //     SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //     SqlCommand cmd = new SqlCommand();
        //     SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };

        //     cmd.CommandText = "[DbVCM].[sp_revise_vendor]";


        //     cmd.CommandType = CommandType.StoredProcedure;
        //     cmd.Connection = con;

        //     cmd.Parameters.AddWithValue("@PID", Pid);
        //     cmd.Parameters.AddWithValue("@StatusFid", StatusId);

        //     cmd.Parameters.AddWithValue("@VendorName", VendorName);
        //     cmd.Parameters.AddWithValue("@CategoryFid", CategoryId);
        //     cmd.Parameters.AddWithValue("@Gstin", Gstin);
        //     cmd.Parameters.AddWithValue("@Pan", Pan);
        //     cmd.Parameters.AddWithValue("@CreditDays", CreditDays);
        //     cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
        //     cmd.Parameters.AddWithValue("@IsMsmed", IsMsmed);
        //     cmd.Parameters.AddWithValue("@IsIsoCertified", IsIsoCertified);
        //     cmd.Parameters.AddWithValue("@IsTechnicalDetailsReceived", IsTechnicalDetailsReceived);
        //     cmd.Parameters.AddWithValue("@IsVisitByQa", IsVisitByQa);
        //     cmd.Parameters.AddWithValue("@IsGovernment", IsGovernment);
        //     cmd.Parameters.AddWithValue("@ResponsibleFid", ResponsibleId);
        //     cmd.Parameters.AddWithValue("@VendorTypeFid", VendorTypeId);
        //     cmd.Parameters.AddWithValue("@ItemCategoryFid", ItemCategoryFid);
        //     cmd.Parameters.AddWithValue("@ItemSubcategoryFid", ItemSubcategoryFid);

        //     cmd.Parameters.AddWithValue("@EntityTypeFid", EntityTypeId);
        //     cmd.Parameters.AddWithValue("@AddressTable", AddressTable);
        //     cmd.Parameters.AddWithValue("@BankTable", BankTable);
        //     cmd.Parameters.AddWithValue("@ContactPersonTable", ContactPersonTable);
        //     cmd.Parameters.AddWithValue("@DOCsTable", DOCsTable);
        //     cmd.Parameters.AddWithValue("@SavingType", SavingType);
        //     cmd.Parameters.AddWithValue("@Remarks", Remarks);

        //     cmd.Parameters.AddWithValue("@RevisionTypeIds", RevisionTypeIds);

        //     cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);


        //     cmd.Parameters.Add(rcode);
        //     try
        //     {
        //         if (con.State == ConnectionState.Closed)
        //         {
        //             con.Open();
        //         }
        //         cmd.ExecuteNonQuery();
        //         int value = Convert.ToInt32(rcode.Value);
        //         con.Close();
        //         con.Dispose();
        //         return value;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        //     finally
        //     {
        //         con.Close();
        //         con.Dispose();
        //     }
        // }



        public int ReviseVendor(
                      int Pid
                    , string VendorName
                    , string VendorCode
                    , int StatusId
                    , int CategoryId
                    , int MSMEStatusId
                    //, string Gstin
                    , string Pan
                    , int CreditDays
                    , double CreditLimit
                    , int IsAssociated
                    , int IsIsoCertified
                    , int IsTechnicalDetailsReceived
                    , int IsVisitByQa
                    , int IsGovernment
                    , int IsOneTime
                    , int ResponsibleId
                    , int VendorTypeId
                    , int ItemCategoryFid
                    , int ItemSubcategoryFid
                    , int EntityTypeId
                    , DataTable AddressTable
                    , DataTable BankTable
                    , DataTable ContactPersonTable
                    , DataTable DOCsTable
                    , int SavingType
                    , string Remarks
                    , DataTable RevisionTypeIds

                    , string Declaration
                    , string ContactPerson
                    , string ContactNo
                    , string ContactDate
                    , string ContactedBy

                    , int CreatedBy
                    , int CheckerId
                )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };

            if (Pid==0)
            {
                cmd.CommandText = "[DbVCM].[sp_revise_vendor_fact]";
            }
            else
            {
                cmd.CommandText = "[DbVCM].[sp_revise_vendor]";
            }

            //cmd.CommandText = "[DbVCM].[sp_revise_vendor_fact]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@PID", Pid);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@StatusFid", StatusId);
            cmd.Parameters.AddWithValue("@CategoryFid", CategoryId);
            cmd.Parameters.AddWithValue("@MSMEStatusFid", MSMEStatusId);
            //cmd.Parameters.AddWithValue("@Gstin", Gstin);
            cmd.Parameters.AddWithValue("@Pan", Pan);
            cmd.Parameters.AddWithValue("@CreditDays", CreditDays);
            cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
            cmd.Parameters.AddWithValue("@IsAssociated", IsAssociated);
            cmd.Parameters.AddWithValue("@IsIsoCertified", IsIsoCertified);
            cmd.Parameters.AddWithValue("@IsTechnicalDetailsReceived", IsTechnicalDetailsReceived);
            cmd.Parameters.AddWithValue("@IsVisitByQa", IsVisitByQa);
            cmd.Parameters.AddWithValue("@IsGovernment", IsGovernment);
            cmd.Parameters.AddWithValue("@IsOneTime", IsOneTime);

            cmd.Parameters.AddWithValue("@ResponsibleFid", ResponsibleId);
            cmd.Parameters.AddWithValue("@VendorTypeFid", VendorTypeId);
            cmd.Parameters.AddWithValue("@ItemCategoryFid", ItemCategoryFid);
            cmd.Parameters.AddWithValue("@ItemSubcategoryFid", ItemSubcategoryFid);

            cmd.Parameters.AddWithValue("@EntityTypeFid", EntityTypeId);
            cmd.Parameters.AddWithValue("@AddressTable", AddressTable);
            cmd.Parameters.AddWithValue("@BankTable", BankTable);
            cmd.Parameters.AddWithValue("@ContactPersonTable", ContactPersonTable);
            cmd.Parameters.AddWithValue("@DOCsTable", DOCsTable);
            cmd.Parameters.AddWithValue("@SavingType", SavingType);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@RevisionTypeIds", RevisionTypeIds);

            cmd.Parameters.AddWithValue("@Declaration", Declaration);
            cmd.Parameters.AddWithValue("@ContactPerson", ContactPerson);
            cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
            cmd.Parameters.AddWithValue("@ContactDate", ContactDate);
            cmd.Parameters.AddWithValue("@ContactedBy", ContactedBy);
            
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CheckerId", CheckerId);
            

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


        public DataSet GetVendorForPDF(int vendorId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_vendor_for_pdf]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@vendorId", vendorId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetEmailTemplates(int entityTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_email_templates]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetMailInfo(int pid,int statusId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_vendor_mail_info]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@vendorId", pid);
            cmd.Parameters.AddWithValue("@statusId", statusId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateMailStatus(int pId, int statusId, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_update_vendor_mail_status]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@pid", pId);
            cmd.Parameters.AddWithValue("@statusid", statusId);
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

        public int UpdateStatus(int vendorId, int statusId, string remarks, string vendorCode, int createdBy
            , string Declaration, string ContactPerson, string ContactNo, string ContactDate, string ContactedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_update_vendor_status]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@vendorId", vendorId);
            cmd.Parameters.AddWithValue("@statusId", statusId);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@vendorCode", vendorCode);

            cmd.Parameters.AddWithValue("@declaration", Declaration);
            cmd.Parameters.AddWithValue("@contactPerson", ContactPerson);
            cmd.Parameters.AddWithValue("@contactNo", ContactNo);
            cmd.Parameters.AddWithValue("@contactDate", ContactDate);
            cmd.Parameters.AddWithValue("@contactedBy", ContactedBy);

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



        public DataSet GetVendorsList(string startDate
                                    , string endDate
                                    , string vendorName
                                    , string vendorCode
                                    , int statusId
                                    , int responsibleId
                                    , int relationTypeId
                                    , string pAN
                                    , string gSTIN
                                    , int categoryId
                                    , int organizationTypeId
        )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_vendors_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@ResponsibleId", responsibleId);
            cmd.Parameters.AddWithValue("@RelationTypeId", relationTypeId);
            cmd.Parameters.AddWithValue("@PAN", pAN);
            cmd.Parameters.AddWithValue("@GSTIN", gSTIN);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            cmd.Parameters.AddWithValue("@OrganizationTypeId", organizationTypeId);



            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_vendor_status]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetApprovers(int entityTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_approvers]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@entityTypeFid", entityTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorDetails(int pID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_vendor_details]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@vendorId", pID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetVendorDetailsByCode(string VendorCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_vendor_details_fact]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@vendorCode", VendorCode);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDOC(int pid)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_doc]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@Pid", pid);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOrganizationType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_organization_types]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDOCs(int vendorId, int vendorTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_docs]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@vendorId", vendorId);
            cmd.Parameters.AddWithValue("@vendorTypeId", vendorTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public DataSet GetRevisionType(int entityTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_revision_ypes]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int AddUpdateMasterTables(int pid ,int tableTypeId, string name, string description, int itemCategoryFid, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_save_master_tables]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@tableTypeId", tableTypeId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@itemCategoryFid", itemCategoryFid);
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



        public int AddUpdateRespnsible(int pid, int employeeId, string name, string description,int isDeleted, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@RCode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[DbVCM].[sp_save_responsible]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@isDeleted", isDeleted);
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


        public DataSet GetResponsiblesList(int employeeId
                                          , string name
                                          , string description)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_responsibles_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@employeeId",employeeId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVendorsListForRevision(string startDate
                                        , string endDate
                                        , string vendorName
                                        , string vendorCode
                                        , int responsibleId
                                        , int relationTypeId
                                        , string pAN
                                        , string gSTIN
                                        , int categoryId
                                        , int organizationTypeId
                                        , int currentUserId
                                        , int onlyFACTVendors
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[DbVCM].[sp_get_registered_vendors_list]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@VendorName", vendorName);
            cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
            cmd.Parameters.AddWithValue("@ResponsibleId", responsibleId);
            cmd.Parameters.AddWithValue("@RelationTypeId", relationTypeId);
            cmd.Parameters.AddWithValue("@PAN", pAN);
            cmd.Parameters.AddWithValue("@GSTIN", gSTIN);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            cmd.Parameters.AddWithValue("@OrganizationTypeId", organizationTypeId);
            cmd.Parameters.AddWithValue("@EmployeeId", currentUserId);
            cmd.Parameters.AddWithValue("@OnlyFactVendors", onlyFACTVendors);



            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }





        public DataSet GetEmailTemplatesList(int entityTypeId,int statusId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_email_templates_list]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);
            cmd.Parameters.AddWithValue("@statusId", statusId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDODTypes(int entityTypeId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[DbVCM].[sp_get_doc_types]";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
    }

}
