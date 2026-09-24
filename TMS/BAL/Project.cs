using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class Project
    {

        public DataSet GetDetailsBySP(string spName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTimesheetReport(string startDate, string endDate, string jobNo, string status, int employeeID, string teamMemberID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "sp_get_timesheet_report";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", employeeID);
            cmd.Parameters.AddWithValue("@teammemberid", teamMemberID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetTimesheetList(string startDate, string endDate, string jobNo, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "sp_get_timesheet_list01";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertUpdateTimesheet(int recordID, int empRecordID,
                                        string projectCategory, string projectNumber, string jobNumber,
                                        string drawingCategory, string serialNumber, string size,
                                        string rev, int sheets, string drawingID, int drawingTypeID,
                                        string startTime, string endTime, string timeSpent, string title, string timesheetDate,
                                        string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_timesheet01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@projectcategory", projectCategory);
            cmd.Parameters.AddWithValue("@projectnumber", projectNumber);
            cmd.Parameters.AddWithValue("@jobnumber", jobNumber);
            cmd.Parameters.AddWithValue("@drawingcategory", drawingCategory);
            cmd.Parameters.AddWithValue("@serialnumber", serialNumber);
            cmd.Parameters.AddWithValue("@size", size);
            cmd.Parameters.AddWithValue("@rev", rev);
            cmd.Parameters.AddWithValue("@sheets", sheets);
            cmd.Parameters.AddWithValue("@drawingid", drawingID);
            cmd.Parameters.AddWithValue("@drawingtypeid", drawingTypeID);
            cmd.Parameters.AddWithValue("@starttime", startTime);
            cmd.Parameters.AddWithValue("@endtime", endTime);
            cmd.Parameters.AddWithValue("@timespent", timeSpent);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@timesheetdate", timesheetDate);
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


        public DataSet GetOldTimesheetDetails(string startDate, string endDate, string jobNo, string status, string userName, string teamUserNames)
        {
            DataSet ds = new DataSet();
            string timesheetconnectionstring = ConnectionString.GetTimesheetCommonConn();
            SqlConnection con = new SqlConnection(timesheetconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_new_entries01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Parameters.AddWithValue("@teamusernames", teamUserNames);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetNewTimesheetDetails()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_old_timesheet_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetNewTimesheetIDs()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_new_timesheet_list_ids";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int ImportTimesheet(int ID, int EMP_RECORD_ID, string ENTRY_DATE, string PROJECT_CATETORY, string PROJECT_NUMBER,
                                   string JOB_NUMBER, string DRAWING_CATEGORY, string SERIAL_NUMBER, string SIZE, string REV, int SHEETS,
                                   string DRAWING_ID, int DRAWING_TYPE_ID, string START_TIME, string END_TIME, string TIME_SPENT, string TITLE,
                                   string REMARKS, int CREATED_BY)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_import_timesheet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@emprecordid", EMP_RECORD_ID);
            cmd.Parameters.AddWithValue("@entrydate", ENTRY_DATE);
            cmd.Parameters.AddWithValue("@projectcategory", PROJECT_CATETORY);
            cmd.Parameters.AddWithValue("@projectnumber", PROJECT_NUMBER);
            cmd.Parameters.AddWithValue("@jobnumber", JOB_NUMBER);
            cmd.Parameters.AddWithValue("@drawingcategory", DRAWING_CATEGORY);
            cmd.Parameters.AddWithValue("@serialnumber", SERIAL_NUMBER);
            cmd.Parameters.AddWithValue("@size", SIZE);
            cmd.Parameters.AddWithValue("@rev", REV);
            cmd.Parameters.AddWithValue("@sheets", SHEETS);
            cmd.Parameters.AddWithValue("@drawingid", DRAWING_ID);
            cmd.Parameters.AddWithValue("@drawingtypeid", DRAWING_TYPE_ID);
            cmd.Parameters.AddWithValue("@starttime", START_TIME);
            cmd.Parameters.AddWithValue("@endtime", END_TIME);
            cmd.Parameters.AddWithValue("@timespent", TIME_SPENT);
            cmd.Parameters.AddWithValue("@title", TITLE);
            cmd.Parameters.AddWithValue("@remarks", REMARKS);
            cmd.Parameters.AddWithValue("@createdby", CREATED_BY);

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



        public int UpdateOldTimesheetDetails(int ID, int IS_IMPORTED)
        {
            string timesheetconnectionstring = ConnectionString.GetTimesheetCommonConn();
            SqlConnection con = new SqlConnection(timesheetconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_old_timesheet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@isimported", IS_IMPORTED);

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



        public DataSet GetUserLastEndTime(int empRecordID, string entryDate)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_last_end_time01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@entrydate", entryDate);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetTimesheetDeptList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_dept_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public int ImportEstimatedProjectFile(string projectNo, string description, string scope, double quantity, double unitRateINR,
                                                double priceINR, double pAndF, double pfd, double ed, double totalED, double st,
                                                double stAmt, double totalCostInclEdSt, double importedExWRateEuro,
                                                double importedExWPriceEuro, double fobPriceEuro, double equivRupeePrice, double fullDuty,
                                                double totalImportCost, double totalCostINR, double totalCostEURO,
                                                string category, string codes, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_import_estimated_project";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@scope", scope);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@unitrateinr", unitRateINR);
            cmd.Parameters.AddWithValue("@priceinr", priceINR);
            cmd.Parameters.AddWithValue("@pandf", pAndF);
            cmd.Parameters.AddWithValue("@pfd", pfd);
            cmd.Parameters.AddWithValue("@ed", ed);
            cmd.Parameters.AddWithValue("@totaled", totalED);
            cmd.Parameters.AddWithValue("@st", st);
            cmd.Parameters.AddWithValue("@stamt", stAmt);
            cmd.Parameters.AddWithValue("@totalcostincledst", totalCostInclEdSt);
            cmd.Parameters.AddWithValue("@importedexwrateeuro", importedExWRateEuro);
            cmd.Parameters.AddWithValue("@importedexwpriceeuro", importedExWPriceEuro);
            cmd.Parameters.AddWithValue("@fobpriceeuro", fobPriceEuro);
            cmd.Parameters.AddWithValue("@equivrupeeprice", equivRupeePrice);
            cmd.Parameters.AddWithValue("@fullduty", fullDuty);
            cmd.Parameters.AddWithValue("@totalimportcost", totalImportCost);
            cmd.Parameters.AddWithValue("@totalcostinr", totalCostINR);
            cmd.Parameters.AddWithValue("@totalcosteuro", totalCostEURO);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@codes", codes);
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


        public DataSet GetEstimatedProjctList(string startDate, string endDate, string projectNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_estimated_project_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int UpdateEstimatedProject(int projectID, string projectNo, string description, string scope, double quantity, double unitRateINR, double priceINR, double PandF,
                           double PFD, double ED, double totalED, double ST, double STAmt, double totalCostInclEDST, double importedEXWRateEuro, double importedEXWPriceEURO, double fOBPriceEuro,
                           double equivRupeePrice, double fullDuty, double totalImportCost, double totalCostINR, double totalCostEURO, string category, string codes, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_estimated_project";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@projectid", projectID);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@scope", scope);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@unitrateinr", unitRateINR);
            cmd.Parameters.AddWithValue("@priceinr", priceINR);
            cmd.Parameters.AddWithValue("@pandf", PandF);
            cmd.Parameters.AddWithValue("@pfd", PFD);
            cmd.Parameters.AddWithValue("@ed", ED);
            cmd.Parameters.AddWithValue("@totaled", totalED);
            cmd.Parameters.AddWithValue("@st", ST);
            cmd.Parameters.AddWithValue("@stamt", STAmt);
            cmd.Parameters.AddWithValue("@totalcostincledst", totalCostInclEDST);
            cmd.Parameters.AddWithValue("@importedexwrateeuro", importedEXWRateEuro);
            cmd.Parameters.AddWithValue("@importedexwpriceeuro", importedEXWPriceEURO);
            cmd.Parameters.AddWithValue("@fobpriceeuro", fOBPriceEuro);
            cmd.Parameters.AddWithValue("@equivrupeeprice", equivRupeePrice);
            cmd.Parameters.AddWithValue("@fullduty", fullDuty);
            cmd.Parameters.AddWithValue("@totalimportcost", totalImportCost);
            cmd.Parameters.AddWithValue("@totalcostinr", totalCostINR);
            cmd.Parameters.AddWithValue("@totalcosteuro", totalCostEURO);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@codes", codes);
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



        public DataSet GetEstimatedProduct(string projectNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "get_estimated_item_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@projectno", projectNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        /*int value = objProject.AddNewProduct(0, projectNo, typeID, estimatedProjectItemID, categoryID, itemCode, description,
                                                additionalDescription, hsn, rate, Convert.ToInt32(Session["EMP_RECORD_ID"]));*/


        public int AddUpdateNewProduct(int productID, string projectNo, int typeID, int estimatedProjectItemID, int categoryID, string itemCode,
                                string description, string additionalDescription, string hsn, double rate, int purchaseUnitID, int stockUnitID,
                                int saleUnitID, string tagNo, string modelNo, string additionalInformation, string technicalSpecification, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_product";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@productid", productID);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@estimatedprojectitemid", estimatedProjectItemID);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@itemcode", itemCode);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@additionaldescription", additionalDescription);
            cmd.Parameters.AddWithValue("@hsn", hsn);
            cmd.Parameters.AddWithValue("@rate", rate);
            cmd.Parameters.AddWithValue("@purchaseunitid", purchaseUnitID);
            cmd.Parameters.AddWithValue("@stockunitid", stockUnitID);
            cmd.Parameters.AddWithValue("@saleunitid", saleUnitID);
            cmd.Parameters.AddWithValue("@tagno", tagNo);
            cmd.Parameters.AddWithValue("@modelno", modelNo);
            cmd.Parameters.AddWithValue("@additionalinformation", additionalInformation);
            cmd.Parameters.AddWithValue("@technicalspecification", technicalSpecification);
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


        public DataSet GetItemCode(int productId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_item_code";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@productid", productId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductList(string startDate, string endDate, string projectNo, string productCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_list01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductListNew(string startDate, string endDate, string projectNo, string productCode, int unitId)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_list02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitid", unitId);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetProductListAllUnit(string startDate, string endDate, string projectNo, string productCode,
                                             string dbNameA35, string unitNameA35,
                                             string dbNameDLH, string unitNameDLH,
                                             string dbNameSEZ, string unitNameSEZ,
                                             string dbNameGNU, string unitNameGNU,
                                             int unitIDA35, int unitIDDLH, int unitIDGNU, int unitIDSEZ)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_list_all_units";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);

            cmd.Parameters.AddWithValue("@unitida35", unitIDA35);
            cmd.Parameters.AddWithValue("@unitiddlh", unitIDDLH);
            cmd.Parameters.AddWithValue("@unitidgnu", unitIDGNU);
            cmd.Parameters.AddWithValue("@unitidsez", unitIDSEZ);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddUpdateNewProductNew(int productID, string projectNo, int estimatedProjectItemID, int typeID, int categoryID,
                                            string productCode, string description, string additionalDescription, string gSTHSN,
                                            double gSTRate, int purchaseUnitID, int stockUnitID, int saleUnitID, string tagNo,
                                            string modelNo, string additionalInformation, string technicalSpecification,
                                            int unitID, string groupID, string subGroupID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_product01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@productid", productID);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@estimatedprojectitemid", estimatedProjectItemID);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@additionaldescription", additionalDescription);
            cmd.Parameters.AddWithValue("@gsthsn", gSTHSN);
            cmd.Parameters.AddWithValue("@gstrate", gSTRate);
            cmd.Parameters.AddWithValue("@purchaseunitid", purchaseUnitID);
            cmd.Parameters.AddWithValue("@stockunitid", stockUnitID);
            cmd.Parameters.AddWithValue("@saleunitid", saleUnitID);
            cmd.Parameters.AddWithValue("@tagno", tagNo);
            cmd.Parameters.AddWithValue("@modelno", modelNo);
            cmd.Parameters.AddWithValue("@additionalinformation", additionalInformation);
            cmd.Parameters.AddWithValue("@technicalspecification", technicalSpecification);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@groupid", groupID);
            cmd.Parameters.AddWithValue("@subgroupid", subGroupID);
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


        public DataSet GetProductDetail(int productID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_detail_for_updation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@productid", productID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet BindSubgroup(int groupID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_subgroup_by_group_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@groupid", groupID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductReport(string startDate, string endDate, string projectNo, string productCode)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_report01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetProductReportNew(string startDate, string endDate, string projectNo, string productCode, int unitId)
        {
            string dbname = ConnectionString.GetDBName(unitId);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_report02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@unitid", unitId);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductReportAllUnit(string startDate, string endDate, string projectNo, string productCode,
                                             string dbNameA35, string unitNameA35,
                                             string dbNameDLH, string unitNameDLH,
                                             string dbNameSEZ, string unitNameSEZ,
                                             string dbNameGNU, string unitNameGNU,
                                             int unitIDA35, int unitIDDLH, int unitIDGNU, int unitIDSEZ)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_report_all_units";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@dbnamea35", dbNameA35);
            cmd.Parameters.AddWithValue("@unitnamea35", unitNameA35);
            cmd.Parameters.AddWithValue("@dbnamedlh", dbNameDLH);
            cmd.Parameters.AddWithValue("@unitnamedlh", unitNameDLH);
            cmd.Parameters.AddWithValue("@dbnamesez", dbNameSEZ);
            cmd.Parameters.AddWithValue("@unitnamesez", unitNameSEZ);
            cmd.Parameters.AddWithValue("@dbnamegnu", dbNameGNU);
            cmd.Parameters.AddWithValue("@unitnamegnu", unitNameGNU);

            cmd.Parameters.AddWithValue("@unitida35", unitIDA35);
            cmd.Parameters.AddWithValue("@unitiddlh", unitIDDLH);
            cmd.Parameters.AddWithValue("@unitidgnu", unitIDGNU);
            cmd.Parameters.AddWithValue("@unitidsez", unitIDSEZ);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProductCode(string projectNo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_product_code";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddUpdateNewMR(int mRCodeID, int productID, int typeID, int unitID, string mRNo, string documentClass, string description, int uom, double quantity,
                                    string additionalDescription, string av1, string av2, string av3, string av4, string av5,
                                    double budget, string remarks, string expectedPODate, string deliveryRequiredBy, string financialYear, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_mr";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@mrcodeid", mRCodeID);
            cmd.Parameters.AddWithValue("@productid", productID);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@mrno", mRNo);
            cmd.Parameters.AddWithValue("@documentclass", documentClass);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@uom", uom);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@additionaldescription", additionalDescription);
            cmd.Parameters.AddWithValue("@av1", av1);
            cmd.Parameters.AddWithValue("@av2", av2);
            cmd.Parameters.AddWithValue("@av3", av3);
            cmd.Parameters.AddWithValue("@av4", av4);
            cmd.Parameters.AddWithValue("@av5", av5);
            cmd.Parameters.AddWithValue("@budget", budget);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@expectedpodate", expectedPODate);
            cmd.Parameters.AddWithValue("@deliveryrequiredby", deliveryRequiredBy);
            cmd.Parameters.AddWithValue("@financialyear", financialYear);
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

        public DataSet GetMRList(string startDate, string endDate, string projectNo, string productCode, string mRNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mr_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@mrno", mRNo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRDetail(int mrID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mr_detail_for_updation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@mrid", mrID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProjectGroup(string projectN0)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@projectno", projectN0);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetProjectGroupNew(int unitID)
        {
            string dbname = ConnectionString.GetDBName(unitID);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_group01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProjectSubgroup(int groupID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_subgroup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@groupid", groupID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetProjectSubgroupNew(int unitID, string groupID)
        {
            string dbname = ConnectionString.GetDBName(unitID);
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_subgroup01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dbname", dbname);
            cmd.Parameters.AddWithValue("@groupid", groupID);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetMRReport(string startDate, string endDate, string projectNo, string productCode, string mRNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mr_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@projectno", projectNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@mrno", mRNo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetRunningNo(string financialYearC, string unitAndType)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_running_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@financialyear", financialYearC);
            cmd.Parameters.AddWithValue("@unitandtype", unitAndType);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddGroupSubgroup(string projectNo, int groupID, string groupName, string subgroupName, int p)
        {
            throw new NotImplementedException();
        }



        public int ImportProjectPivotGroup(string jobNo, string pivotGroup, double col0, double colC, double colE, double colF, double colI,
                                            double colN, double colOI, double colP, double total, int grossMarginLineID, string udf1, string udf2,
                                            string udf3, string udf4, string udf5, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_project_pivot_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@col0", col0);
            cmd.Parameters.AddWithValue("@colc", colC);
            cmd.Parameters.AddWithValue("@cole", colE);
            cmd.Parameters.AddWithValue("@colf", colF);
            cmd.Parameters.AddWithValue("@coli", colI);
            cmd.Parameters.AddWithValue("@coln", colN);
            cmd.Parameters.AddWithValue("@coloi", colOI);
            cmd.Parameters.AddWithValue("@colp", colP);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@grossMarginlineid", grossMarginLineID);
            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
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

        public DataSet GetProjectPivotGroupReport(string startDate, string endDate, string jobNo, int revisionNo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_pivot_group_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@revisionno", revisionNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetProjectPivotGroupForPosting(string startDate, string endDate, string jobNo, int revisionNo, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_pivot_group_list_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@revisionno", revisionNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int PostProjectPivotGroup(int revisionNo, string jobNo, string pivotGroup, string grossMarginLine,
                                            double budgtedAmount, double actualAmount, double differenceAmount, string status,
                                            string postingMonth, string remarks, string udf1, string udf2, string udf3,
                                            string udf4, string udf5, string unit, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_post_project_pivot_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@revisionno", revisionNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pivotgroup", pivotGroup);
            cmd.Parameters.AddWithValue("@grossmarginline", grossMarginLine);
            cmd.Parameters.AddWithValue("@budgtedamount", budgtedAmount);
            cmd.Parameters.AddWithValue("@actualamount", actualAmount);
            cmd.Parameters.AddWithValue("@differenceamount", differenceAmount);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@udf1", udf1);
            cmd.Parameters.AddWithValue("@udf2", udf2);
            cmd.Parameters.AddWithValue("@udf3", udf3);
            cmd.Parameters.AddWithValue("@udf4", udf4);
            cmd.Parameters.AddWithValue("@udf5", udf5);
            cmd.Parameters.AddWithValue("@unit", unit);
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



        public DataSet GetProjectPivotGroupPostedReport(string jobNo, int revisionNo, string unit, string postingMonth, string status)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_pivot_group_posted_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@revisionno", revisionNo);
            cmd.Parameters.AddWithValue("@unit", unit);
            cmd.Parameters.AddWithValue("@postingmonth", postingMonth);
            cmd.Parameters.AddWithValue("@status", status);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertProjectMonthlyInput(int recordID, string jobNo, string month, double vpoc, double icpoc,
                                            double material, double engineering, double travelling, double otherCost, double warranty,
                                            double commission, double royalty, double lateDelivery, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_project_input";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@vpoc", vpoc);
            cmd.Parameters.AddWithValue("@icpoc", icpoc);
            cmd.Parameters.AddWithValue("@material", material);
            cmd.Parameters.AddWithValue("@engineering", engineering);
            cmd.Parameters.AddWithValue("@travelling", travelling);
            cmd.Parameters.AddWithValue("@othercost", otherCost);
            cmd.Parameters.AddWithValue("@warranty", warranty);
            cmd.Parameters.AddWithValue("@commission", commission);
            cmd.Parameters.AddWithValue("@royalty", royalty);
            cmd.Parameters.AddWithValue("@latedelivery", lateDelivery);
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

        public DataSet GetProjectmonthlyReport(string jobNo, string month)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_project_monthly_input_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@month", month);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }






        // 22-JAN-2020
        public DataSet GetJOBDetails(string custCode, string jobNo, string dbName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_detail_for_project";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@dbname", dbName);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //23-JAN-2020
        //public DataSet GetFTRunningNo(string jobNo, int companyID)
        //{
        //    DataSet ds = new DataSet();
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "sp_get_lot_td_running_no1";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    cmd.Parameters.AddWithValue("@jobno", jobNo);
        //    cmd.Parameters.AddWithValue("@companyid", companyID);


        //    da.Fill(ds);
        //    if (ds != null)
        //        return ds;

        //    return null;
        //}

        public int GetFTRunningNo(string jobNo, int companyID)
        {

            int runningNo = 0;
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_td_running_no1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@companyid", companyID);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    runningNo = Convert.ToInt32(rdr["RUNNING_NO"]);
                }
                con.Close();
            }
            catch (Exception)
            {
                runningNo = 0;
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return runningNo;
        }

        public DataSet GetFactoryCategory()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_category";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLotMainItems()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_items";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //24-JAN-2020
        public int InsertLOTTransmittalToFactory(int companyID, string jobNo, string productionNo, string custCode, string tFNo, string poNo,
                                                        string LOTDate, string itemName, int LOTMainItemID, string impNotes,
                                                        string attachmentFile1, string attachmentFile2, string attachmentFile3, string attachmentFile4,
                                                        DataTable dtSubitem, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_lot_transmittal_to_factory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachmentfile1", attachmentFile1);
            cmd.Parameters.AddWithValue("@attachmentfile2", attachmentFile2);
            cmd.Parameters.AddWithValue("@attachmentfile3", attachmentFile3);
            cmd.Parameters.AddWithValue("@attachmentfile4", attachmentFile4);

            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
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

        public int UpdateLOTMailStatus(int LOTTFID, int actID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_tf_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@actid", actID);
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


        //27-JAN-2020
        public DataSet GetJOBDetailsForLOT(int companyID, string custCode, string jobNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_detail_for_project01";
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

        //28-JAN-2020
        public DataSet GetJOBMailInfo(int LOTTFID, int PEApprovedByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@emprecordid", PEApprovedByID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //29-JAN-2020

        public DataSet GetLOTTFStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTTFList(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                    string jobNo, string customerName, int lOTForID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_transmittal_to_factory_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", lOTForID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTJobPEList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_job_pe_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //30-JAN-2020

        public DataSet GetLOTMainItemProductionMngr()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_item_production_mngr_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTSubitems(int LOTTFID, string description, string dRGNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_subitem_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@drgno", dRGNo);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateSubitem(int LOTTFSubitemID, int LOTTFID, string description, string DRGNo, string categoryID, int revisionNo, int noOfCopies, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_tf_subitem";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfsubitemid", LOTTFSubitemID);
            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@subitemdesc", description);
            cmd.Parameters.AddWithValue("@drawingno", DRGNo);
            cmd.Parameters.AddWithValue("@category", categoryID);
            cmd.Parameters.AddWithValue("@revisionno", revisionNo);
            cmd.Parameters.AddWithValue("@copies", noOfCopies);
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



        //31-JAN-2020

        //public int UpdateLOTTransmittalToFactory(int LOTTFID, int companyID, string jobNo, string productionNo, string custCode, 
        //                                         string tFNo, string poNo, string LOTDate, string itemName, int LOTMainItemID, string impNotes, 
        //                                         string attachmentFile1, string attachmentFile2, string attachmentFile3, string attachmentFile4, int createdBy)
        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "sp_update_lot_transmittal_to_factory";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;

        //    cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
        //    cmd.Parameters.AddWithValue("@unitid", companyID);
        //    cmd.Parameters.AddWithValue("@jobno", jobNo);
        //    cmd.Parameters.AddWithValue("@productionno", productionNo);
        //    cmd.Parameters.AddWithValue("@customercode", custCode);
        //    cmd.Parameters.AddWithValue("@tfno", tFNo);
        //    cmd.Parameters.AddWithValue("@pono", poNo);
        //    cmd.Parameters.AddWithValue("@date", LOTDate);
        //    cmd.Parameters.AddWithValue("@itemname", itemName);
        //    cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
        //    cmd.Parameters.AddWithValue("@impnotes", impNotes);

        //    cmd.Parameters.AddWithValue("@attachmentfile1", attachmentFile1);
        //    cmd.Parameters.AddWithValue("@attachmentfile2", attachmentFile2);
        //    cmd.Parameters.AddWithValue("@attachmentfile3", attachmentFile3);
        //    cmd.Parameters.AddWithValue("@attachmentfile4", attachmentFile4);            
        //    cmd.Parameters.AddWithValue("@createdby", createdBy);

        //    cmd.Parameters.Add(rcode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rcode.Value);
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



        public int UpdateLOTTransmittalToFactory(int LOTTFID, int companyID, string jobNo, string productionNo, string custCode, string tFNo, string poNo,
                                                        string LOTDate, string itemName, int LOTMainItemID, string impNotes,
                                                        string attachmentFile1, string attachmentFile2, string attachmentFile3, string attachmentFile4,
                                                        int insertSubitemFlag, DataTable dtSubitem, string updateSubitemQuery, int amendmentFlag, int newStatusID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_transmittal_to_factory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachmentfile1", attachmentFile1);
            cmd.Parameters.AddWithValue("@attachmentfile2", attachmentFile2);
            cmd.Parameters.AddWithValue("@attachmentfile3", attachmentFile3);
            cmd.Parameters.AddWithValue("@attachmentfile4", attachmentFile4);

            cmd.Parameters.AddWithValue("@insertsubitemflag", insertSubitemFlag);
            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
            cmd.Parameters.AddWithValue("@updatesubitemquery", updateSubitemQuery);

            cmd.Parameters.AddWithValue("@amendmentflag", amendmentFlag);
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);

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


        public DataSet GetLOTTFDetails(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //03-FEB-2020
        public int UpdateLOTTFStatus(int LOTTFID, int actID, int newStatusID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_transmittal_to_factory_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);
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

        //05-FEB-2020
        public int AddLOTApprover(DataTable dt, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_lot_approver";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@tbllotapprover", dt);
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

        public int InsertLOTTransmittalToFactoryTwo(string tFNo, int companyID, string LOTDate, string custCode,
                                                    string jobNo, string poNo, string itemName, string impNotes,
                                                    string attachment1File, byte[] attachment1FileBytes,
                                                    string attachment2File, byte[] attachment2FileBytes,
                                                    string attachment3File, byte[] attachment3FileBytes,
                                                    string attachment4File, byte[] attachment4FileBytes,
                                                    DataTable dtSubitemToAdd, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_lot_transmittal_to_factory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitemToAdd);
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

        public int InsertLOTTransmittalToFactoryThree(string tFNo, int companyID, string LOTDate, string custCode,
                                                   string jobNo, string poNo, string itemName, string impNotes,
                                                   string attachment1File, byte[] attachment1FileBytes,
                                                   string attachment2File, byte[] attachment2FileBytes,
                                                   string attachment3File, byte[] attachment3FileBytes,
                                                   string attachment4File, byte[] attachment4FileBytes,
                                                   DataTable dtSubitemToAdd, int savingType, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_lot_transmittal_to_factory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@savingtype", savingType);
            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitemToAdd);
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



        public DataSet GetAppJOBNo(string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_no_in_tblapprover";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobno", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //24-FEB-2020

        public int AddUpdateLOTApprover(int recordID, int jobUnitID, string jobNo, int pmID, int peID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_lot_approver";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lotapproverrecordid", recordID);
            cmd.Parameters.AddWithValue("@jobunitid", jobUnitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pmid", pmID);
            cmd.Parameters.AddWithValue("@peid", peID);
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

        public DataSet GetAppJOBNoOne(string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_no_in_tblapprover1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobno", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJOBApprovers(string jobNo, int unitID, string LOTMainSubItemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_approvers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@companyid", unitID);
            cmd.Parameters.AddWithValue("@lotmainsubitemids", LOTMainSubItemIDs);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBMailInfoOne(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_mail_info1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBMailInfoTwo(int LOTTFID, string LOTTFSubitemIDs, int qaIntlInspFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_mail_info3";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lotsubitemids", LOTTFSubitemIDs);
            cmd.Parameters.AddWithValue("@qaintlinspflag", qaIntlInspFlag);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTApproversList(string jobNo, int PEID, int PMID, int unitID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_approvers_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@peid", PEID);
            cmd.Parameters.AddWithValue("@pmid", PMID);
            cmd.Parameters.AddWithValue("@unitid", unitID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTTFListOne(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                    string jobNo, string customerName, int lOTForID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_transmittal_to_factory_list1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", lOTForID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetLOTTFListTwo(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                       string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_list1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTTFDetailsOne(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTTFDetailsTwo(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTTFDetailsThree(int LOTTFID, string LOTTFSubitemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details3";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubitemIDs);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
        public int UpdateLOTTFStatusOne(int LOTTFID, int actID, int newStatusID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_transmittal_to_factory_status1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);
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


        //public int UpdateLOTTFStatusTwo(int LOTTFID, int LOTMainSubitemID, int newStatusID, string IRNAttachmentFile, byte[] IRNAttachmentFileBytes, string remarks, int createdBy)
        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "sp_update_lot_status";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;

        //    cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
        //    cmd.Parameters.AddWithValue("@lottfsubitemid", LOTMainSubitemID);

        //    cmd.Parameters.AddWithValue("@newstatusid", newStatusID);

        //    cmd.Parameters.AddWithValue("@irnattachmentfile", IRNAttachmentFile);
        //    if (IRNAttachmentFileBytes != null)
        //        cmd.Parameters.AddWithValue("@irnattachmentfilebytes", IRNAttachmentFileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@irnattachmentfilebytes", System.Data.SqlTypes.SqlBinary.Null);

        //    cmd.Parameters.AddWithValue("@remarks", remarks);
        //    cmd.Parameters.AddWithValue("@createdby", createdBy);

        //    cmd.Parameters.Add(rcode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rcode.Value);
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

        public int UpdateLOTTFStatusTwo(int LOTTFID, int LOTMainSubitemID, int quantity, int partialQuantityFlag, int newStatusID,
                                        string IRNAttachmentFile, byte[] IRNAttachmentFileBytes, string remarks, int amendmentFlag,
                                        string standardDrawingFile,
                                        byte[] standardDrawingFileBytes,
                                        string standardDrawingRemarks,
                                        int standardDrawingRemoveID,
                                        int firstQualityPersonID,
                                        int secondQualityPersonID,
                                        int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@partialquantityflag", partialQuantityFlag);

            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);

            cmd.Parameters.AddWithValue("@irnattachmentfile", IRNAttachmentFile);
            if (IRNAttachmentFileBytes != null)
                cmd.Parameters.AddWithValue("@irnattachmentfilebytes", IRNAttachmentFileBytes);
            else
                cmd.Parameters.AddWithValue("@irnattachmentfilebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@standarddrawingfile", standardDrawingFile);
            if (standardDrawingFileBytes != null)
                cmd.Parameters.AddWithValue("@standarddrawingfilebytes", standardDrawingFileBytes);
            else
                cmd.Parameters.AddWithValue("@standarddrawingfilebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@standarddrawingremarks", standardDrawingRemarks);
            cmd.Parameters.AddWithValue("@standarddrawingremoveid", standardDrawingRemoveID);

            cmd.Parameters.AddWithValue("@firstqualitypersonid", firstQualityPersonID);
            cmd.Parameters.AddWithValue("@secondqualitypersonid", secondQualityPersonID);

            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@amendmentflag", amendmentFlag);
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

        public int UpdateLOTMailStatusOne(int LOTTFID, int statusID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_tf_mail_status1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
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


        public int UpdateLOTMailStatusTwo(int LOTTFID, string LOTTFSubItemIDs, int statusID, int amendmentFlag, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_tf_mail_status2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubItemIDs);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@amendmentflag", amendmentFlag);
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





        //24-FEB-2020
        public int InsertLOTTransmittalToFactoryOne(int statusID, int companyID, string jobNo, string productionNo, string custCode, string tFNo, string poNo,
                                                        string LOTDate, string itemName, int LOTMainItemID, string impNotes,
                                                        string attachmentFile1, string attachmentFile2, string attachmentFile3, string attachmentFile4,
                                                        DataTable dtSubitem, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_lot_transmittal_to_factory1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachmentfile1", attachmentFile1);
            cmd.Parameters.AddWithValue("@attachmentfile2", attachmentFile2);
            cmd.Parameters.AddWithValue("@attachmentfile3", attachmentFile3);
            cmd.Parameters.AddWithValue("@attachmentfile4", attachmentFile4);

            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
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



        public int InsertLOTTransmittalToFactoryOne(int statusID, int companyID, string jobNo, string productionNo, string custCode,
                                                    string tFNo, string poNo, string LOTDate, string itemName,
                                                    int LOTMainItemID, int LOTMainSubItemID, string impNotes,
                                                    string drawing1File, byte[] drawing1FileBytes,
                                                    string drawing2File, byte[] drawing2FileBytes,
                                                    string drawing3File, byte[] drawing3FileBytes,
                                                    string drawing4File, byte[] drawing4FileBytes,
                                                    DataTable dtSubitem, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_lot_transmittal_to_factory1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);


            cmd.Parameters.AddWithValue("@drawing1filename", drawing1File);
            if (drawing1FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing1filebytes", drawing1FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing2filename", drawing2File);
            if (drawing2FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing2filebytes", drawing2FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing3filename", drawing3File);
            if (drawing3FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing3filebytes", drawing3FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing4filename", drawing4File);
            if (drawing4FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing4filebytes", drawing4FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
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

        //03-MAR-2020        
        public DataSet GetLOTDrawingFiles(int LOTTFID, int LOTTFSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_drawing_files";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemid", LOTTFSubitemID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int UpdateLOTTransmittalToFactoryOne(int LOTTFID, int companyID, string jobNo, string productionNo, string custCode, string tFNo, string poNo,
                                                       string LOTDate, string itemName, int LOTMainItemID, string impNotes,
                                                       string attachmentFile1, string attachmentFile2, string attachmentFile3, string attachmentFile4,
                                                       int insertSubitemFlag, DataTable dtSubitem, string updateSubitemQuery, int amendmentFlag, int newStatusID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_transmittal_to_factory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachmentfile1", attachmentFile1);
            cmd.Parameters.AddWithValue("@attachmentfile2", attachmentFile2);
            cmd.Parameters.AddWithValue("@attachmentfile3", attachmentFile3);
            cmd.Parameters.AddWithValue("@attachmentfile4", attachmentFile4);

            cmd.Parameters.AddWithValue("@insertsubitemflag", insertSubitemFlag);
            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
            cmd.Parameters.AddWithValue("@updatesubitemquery", updateSubitemQuery);

            cmd.Parameters.AddWithValue("@amendmentflag", amendmentFlag);
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);

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


        public int UpdateLOTTransmittalToFactoryOne(int LOTTFID, int companyID, string jobNo, string productionNo, string custCode,
                                                    string tFNo, string poNo, string LOTDate, string itemName,
                                                    int LOTMainItemID, int LOTMainSubItemID, string impNotes,
                                                    string drawing1File, byte[] drawing1FileBytes,
                                                    string drawing2File, byte[] drawing2FileBytes,
                                                    string drawing3File, byte[] drawing3FileBytes,
                                                    string drawing4File, byte[] drawing4FileBytes,
                                                    int insertSubitemFlag,
                                                    string updateSubitemQuery,
                                                    DataTable dtSubitem,
                                                    int amendmentCount, int newStatusID, string remarks,
                                                    int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_transmittal_to_factory1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);


            cmd.Parameters.AddWithValue("@drawing1filename", drawing1File);
            if (drawing1FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing1filebytes", drawing1FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing2filename", drawing2File);
            if (drawing2FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing2filebytes", drawing2FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing3filename", drawing3File);
            if (drawing3FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing3filebytes", drawing3FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing4filename", drawing4File);
            if (drawing4FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing4filebytes", drawing4FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing4filebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);

            cmd.Parameters.AddWithValue("@remarks", remarks);

            cmd.Parameters.AddWithValue("@updatesubitemquery", updateSubitemQuery);
            cmd.Parameters.AddWithValue("@insertsubitemflag", insertSubitemFlag);
            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
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



        //11-MAR-2020
        public DataSet GetOutstandingMRNList(string fromDate, string toDate, int unitID, string mrnNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_outstanding_mrn_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@mrnno", mrnNo);
            cmd.Parameters.AddWithValue("@pono", poNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //12-MAR-2020
        public DataSet GetQualityChecker()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_quality_checker";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int SendToQualityForCheck(DataTable dtMRN)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_outstanding_mrn";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@tblmrn", dtMRN);

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

        public int UpdateMRNMailSentStatus(string updateMailStatusQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_mrn_mail_sent_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@updatemailstatusquery", updateMailStatusQuery);

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

        //13-MAR-2020
        public DataSet GetMRNStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_mrn_status_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetAssignedMRNList(string fromDate, string toDate, int unitID, string mrnNo, string poNo, int statusID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_assigned_mrn_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@mrnno", mrnNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetMRNSentMailInfo(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_assigned_mrn_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordid", recordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateMRNStatus(int recordID, int actID, int newStatusID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_mrn_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);
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

        public int UpdateMRNMailSentStatus(int recordID, int newStatusID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_mrn_mail_sent_status_single";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@statusid", newStatusID);
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

        public DataSet GetAssignedMRNReport(string fromDate, string toDate, int unitID, string mrnNo, string poNo, int statusID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_assigned_mrn_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@fromdate", fromDate);
            cmd.Parameters.AddWithValue("@todate", toDate);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@mrnno", mrnNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLotMainSubItems(int LOTMainItemID, int companyID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_subitems1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@companyid", companyID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //public int ReviseLOT(int dd, string TFNo, string LOTDate, string itemName, int LOTMainItemID, int LOTMainSubItemID, 
        //    string impNotes, string drawing1File, byte[] drawing1FileBytes, string drawing2File, byte[] drawing2FileBytes, 
        //    string drawing3File, byte[] drawing3FileBytes, string drawing4File, byte[] drawing4FileBytes, int insertSubitemFlag, 
        //    string updateSubitemQuery, DataTable dtSubitem, string remarks, int createdBy)
        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "sp_update_lot_transmittal_to_factory1";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;

        //    cmd.Parameters.AddWithValue("@unitid", companyID);
        //    cmd.Parameters.AddWithValue("@jobno", jobNo);
        //    cmd.Parameters.AddWithValue("@productionno", productionNo);
        //    cmd.Parameters.AddWithValue("@customercode", custCode);
        //    cmd.Parameters.AddWithValue("@tfno", tFNo);
        //    cmd.Parameters.AddWithValue("@pono", poNo);
        //    cmd.Parameters.AddWithValue("@date", LOTDate);
        //    cmd.Parameters.AddWithValue("@itemname", itemName);
        //    cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
        //    cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubItemID);
        //    cmd.Parameters.AddWithValue("@impnotes", impNotes);


        //    cmd.Parameters.AddWithValue("@drawing1filename", drawing1File);
        //    if (drawing1FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing1filebytes", drawing1FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing1filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@drawing2filename", drawing2File);
        //    if (drawing2FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing2filebytes", drawing2FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing2filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@drawing3filename", drawing3File);
        //    if (drawing3FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing3filebytes", drawing3FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing3filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@drawing4filename", drawing4File);
        //    if (drawing4FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing4filebytes", drawing4FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing4filebytes", System.Data.SqlTypes.SqlBinary.Null);

        //    //cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
        //    //cmd.Parameters.AddWithValue("@newstatusid", newStatusID);

        //    cmd.Parameters.AddWithValue("@remarks", remarks);

        //    cmd.Parameters.AddWithValue("@updatesubitemquery", updateSubitemQuery);
        //    cmd.Parameters.AddWithValue("@insertsubitemflag", insertSubitemFlag);
        //    cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
        //    cmd.Parameters.AddWithValue("@createdby", createdBy);

        //    cmd.Parameters.Add(rcode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rcode.Value);
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

        public DataSet GetProductionOrderNoDetails(int companyID, string jobNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_order_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@companyid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionorderno", poNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetEmployeesToAddApprover()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employees_to_add_approvers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int ReviseLOT(int LOTTFID, int isDeactiveOld, int statusID, int companyID, string jobNo, string productionNo, string custCode, string tFNo,
                             string poNo, string LOTDate, string itemName, int LOTMainItemID, int LOTMainSubItemID,
                             string impNotes, string remarks,
                             string drawing1File, byte[] drawing1FileBytes, string drawing2File, byte[] drawing2FileBytes,
                             string drawing3File, byte[] drawing3FileBytes, string drawing4File,
                             byte[] drawing4FileBytes, DataTable dtSubitem, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_revise_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@isdeactiveold", isDeactiveOld);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionno", productionNo);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubItemID);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);
            cmd.Parameters.AddWithValue("@remarks", remarks);


            cmd.Parameters.AddWithValue("@drawing1filename", drawing1File);
            if (drawing1FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing1filebytes", drawing1FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing2filename", drawing2File);
            if (drawing2FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing2filebytes", drawing2FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing3filename", drawing3File);
            if (drawing3FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing3filebytes", drawing3FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@drawing4filename", drawing4File);
            if (drawing4FileBytes != null)
                cmd.Parameters.AddWithValue("@drawing4filebytes", drawing4FileBytes);
            else
                cmd.Parameters.AddWithValue("@drawing4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
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


        public string GetTAGNo(string tagNo)
        {
            string str = string.Empty;
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tag_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@tagNo", tagNo);

            try
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    str = sdr[0].ToString();
                }
                con.Close();
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

            return str;

            //da.Fill(ds);
            //if (ds != null)
            //    return ds;

            //return null;
        }

        public DataSet GetLOTSubitemsByLOTID(int LOTTFID, string LOTMainSubitemIDs, string LOTTFSubitemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_subitem_detail1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lotmainsubitemids", LOTMainSubitemIDs);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubitemIDs);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateLOTTFStatusThree(string updationQuery)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_status1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@updationquery", updationQuery);
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

        public int UpdateQualityPlanningStatus(int LOTTFID, int LOTTFSubitemID, int newStatusID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_tf_quality_and_planning_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemid", LOTTFSubitemID);//lotmainsubitemid,LOTMainSubitemID
            cmd.Parameters.AddWithValue("@newstatusid", newStatusID);
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

        public DataSet GetLOTTFListForRevision(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                  string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID, string tagNumber, string drawingNumber)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_list_for_revision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@tagnumber", tagNumber);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetLOTTFDetailsForRevision(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details_for_revision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int ReviseLOTSubitems(int LOTTFID, string tFNo, int companyID, string LOTDate, string custCode, string jobNo, string poNo, string itemName, string impNotes,
                                        string attachment1File, byte[] attachment1FileBytes,
                                        string attachment2File, byte[] attachment2FileBytes,
                                        string attachment3File, byte[] attachment3FileBytes,
                                        string attachment4File, byte[] attachment4FileBytes,
                                        DataTable dtSubitemToAdd, string LOTTFsubitemIDsToVoid, int createdByID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_revise_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitemToAdd);
            cmd.Parameters.AddWithValue("@lottfsubitemidstovoid", LOTTFsubitemIDsToVoid);


            cmd.Parameters.AddWithValue("@createdby", createdByID);

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


        public DataSet GetLOTReport(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                  string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID, string tagNumber, string drawingNumber, string drawingStatus,
                                  string productionOrderNo, string productCode, string productDesc, string isPartOfProductionID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@tagnumber", tagNumber);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);
            cmd.Parameters.AddWithValue("@drawingstatus", drawingStatus);


            cmd.Parameters.AddWithValue("@productionrrderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@productdesc", productDesc);
            cmd.Parameters.AddWithValue("@ispartofproductionid", isPartOfProductionID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetLOTTFStatusFoRevision()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_status_for_revision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTTFStatusForProduction()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_status_for_production";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTReportForProduction(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                                 string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID,
                                                 string tagNumber, string drawingNumber, int productionManagerID,
                                                 string productionOrderNo, string productCode, string productDesc, string isPartOfProductionID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_report_for_production";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@tagnumber", tagNumber);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);
            cmd.Parameters.AddWithValue("@productionmanagerid", productionManagerID);

            cmd.Parameters.AddWithValue("@productionrrderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@productdesc", productDesc);
            cmd.Parameters.AddWithValue("@ispartofproductionid", isPartOfProductionID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetLOTTFStatusFoQA()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_status_for_qa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetLOTReportForQA(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                  string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID, string tagNumber,
                                  string drawingNumber, string productionOrderNo, string productCode, string productDesc, string isPartOfProductionID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_report_for_qa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@tagnumber", tagNumber);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);

            cmd.Parameters.AddWithValue("@productionrrderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@productdesc", productDesc);
            cmd.Parameters.AddWithValue("@ispartofproductionid", isPartOfProductionID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetLOTDepartments()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_departments";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetLOTTFDetailsForUpdateAndAmendment(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details_for_update_and_amend";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //public int UpdateOrAmendLOTDetails(int LOTTFID, int companyID, string jobNo, string custCode,
        //                                          string tFNo, string poNo, string LOTDate, string itemName,
        //                                          string attachment1File, byte[] attachment1FileBytes,
        //                                          string attachment2File, byte[] attachment2FileBytes,
        //                                          string attachment3File, byte[] attachment3FileBytes,
        //                                          string attachment4File, byte[] attachment4FileBytes,
        //                                          string updateSubitemQuery,
        //                                          DataTable dtSubitem,
        //                                          int createdBy)
        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "sp_update_or_amend_lot_details";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;

        //    cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
        //    cmd.Parameters.AddWithValue("@unitid", companyID);
        //    cmd.Parameters.AddWithValue("@jobno", jobNo);
        //    cmd.Parameters.AddWithValue("@customercode", custCode);
        //    cmd.Parameters.AddWithValue("@tfno", tFNo);
        //    cmd.Parameters.AddWithValue("@pono", poNo);
        //    cmd.Parameters.AddWithValue("@date", LOTDate);
        //    cmd.Parameters.AddWithValue("@itemname", itemName);

        //    cmd.Parameters.AddWithValue("@drawing1filename", attachment1File);
        //    if (attachment1FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing1filebytes", attachment1FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing1filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@drawing2filename", attachment2File);
        //    if (attachment2FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing2filebytes", attachment2FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing2filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@drawing3filename", attachment3File);
        //    if (attachment3FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing3filebytes", attachment3FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing3filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@drawing4filename", attachment4File);
        //    if (attachment4FileBytes != null)
        //        cmd.Parameters.AddWithValue("@drawing4filebytes", attachment4FileBytes);
        //    else
        //        cmd.Parameters.AddWithValue("@drawing4filebytes", System.Data.SqlTypes.SqlBinary.Null);


        //    cmd.Parameters.AddWithValue("@updatesubitemquery", updateSubitemQuery);
        //    cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitem);
        //    cmd.Parameters.AddWithValue("@createdby", createdBy);

        //    cmd.Parameters.Add(rcode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rcode.Value);
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



        //new

        public int UpdateOrAmendLOTDetailsOld1(int LOTTFID, string tFNo, int companyID, string LOTDate, string custCode,
                                                  string jobNo, string poNo, string itemName,
                                                  string impNotes,
                                                  string attachment1File, byte[] attachment1FileBytes,
                                                  string attachment2File, byte[] attachment2FileBytes,
                                                  string attachment3File, byte[] attachment3FileBytes,
                                                  string attachment4File, byte[] attachment4FileBytes,
                                                  string updateSubitemQuery, string removedLOTTFSubItemIDs,
                                                  int insertionFlag, int isAmend,
                                                  DataTable dtSubitem,
                                                  int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_or_amend_lot_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@updatesubitemquery", updateSubitemQuery);
            cmd.Parameters.AddWithValue("@removedlottfsubitemids", removedLOTTFSubItemIDs);

            cmd.Parameters.AddWithValue("@insertionflag", insertionFlag);
            cmd.Parameters.AddWithValue("@amendflag", isAmend);
            cmd.Parameters.AddWithValue("@tblsubitems", dtSubitem);
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



        public int UpdateOrAmendLOTDetails(int LOTTFID, string tFNo, int companyID, string LOTDate, string custCode,
                                                 string jobNo, string poNo, string itemName,
                                                 string impNotes,
                                                 string attachment1File, byte[] attachment1FileBytes,
                                                 string attachment2File, byte[] attachment2FileBytes,
                                                 string attachment3File, byte[] attachment3FileBytes,
                                                 string attachment4File, byte[] attachment4FileBytes,
                                                 string removedLOTTFSubItemIDs,
                                                 int insertionFlag, int isAmend,
                                                 DataTable dtSubitem,
                                                 int subitemsCount, int isUpdateOrAmend,
                                                 DataTable dtSIUpdateOrAmend,
                                                 int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_or_amend_lot_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@removedlottfsubitemids", removedLOTTFSubItemIDs);

            cmd.Parameters.AddWithValue("@insertionflag", insertionFlag);
            cmd.Parameters.AddWithValue("@amendflag", isAmend);
            cmd.Parameters.AddWithValue("@tblsubitems", dtSubitem);

            cmd.Parameters.AddWithValue("@subitemscount", subitemsCount);
            cmd.Parameters.AddWithValue("@updateoramendflag", isUpdateOrAmend);
            cmd.Parameters.AddWithValue("@tblsiupdaterramend", dtSIUpdateOrAmend);
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


        public int UpdateOrAmendLOTDetailsTwo(int LOTTFID, string tFNo, int companyID, string LOTDate, string custCode,
                                                string jobNo, string poNo, string itemName,
                                                string impNotes,
                                                string attachment1File, byte[] attachment1FileBytes,
                                                string attachment2File, byte[] attachment2FileBytes,
                                                string attachment3File, byte[] attachment3FileBytes,
                                                string attachment4File, byte[] attachment4FileBytes,
                                                string removedLOTTFSubItemIDs,
                                                int insertionFlag, int isAmend,
                                                DataTable dtSubitem,
                                                int subitemsCount, int isUpdateOrAmend,
                                                DataTable dtSIUpdateOrAmend, int savingType,
                                                int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_or_amend_lot_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@removedlottfsubitemids", removedLOTTFSubItemIDs);

            cmd.Parameters.AddWithValue("@insertionflag", insertionFlag);
            cmd.Parameters.AddWithValue("@amendflag", isAmend);
            cmd.Parameters.AddWithValue("@tblsubitems", dtSubitem);

            cmd.Parameters.AddWithValue("@subitemscount", subitemsCount);
            cmd.Parameters.AddWithValue("@updateoramendflag", isUpdateOrAmend);
            cmd.Parameters.AddWithValue("@tblsiupdaterramend", dtSIUpdateOrAmend);
            cmd.Parameters.AddWithValue("@savingtype", savingType);
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



        public DataSet GetLOTSubitemsDrawings(int LOTTFSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_subitem_drawing";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@tfsubitemid", LOTTFSubitemID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJOBMailInfoToPrint(int LOTTFID, string LOTTFSubItemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_mail_info_to_print";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubItemIDs);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDrawingsToPrint(int LOTTFID, string LOTTFSubItemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_subitem_drawings_to_print";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubItemIDs);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AddUpdateMainItem(int mainItemID, string mainItem, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_lot_main_item";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@mainitemid", mainItemID);
            cmd.Parameters.AddWithValue("@mainitem", mainItem);
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


        public DataSet GetLOTMainItemList(string maintItem)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_item_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@mainitem", maintItem);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public int AddUpdateMainSubitem(int LOTMainsubitemID, int unitID, int LOTMainItemID, string LOTMainsubitem,
                                        int departmentID, int managerID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_lot_main_subitem";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainsubitemID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitem", LOTMainsubitem);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@managerid", managerID);
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



        public DataSet GetLOTMainSubitemList(int unitID, int LOTMainItemID, string LOTMainsubitem)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_subitem_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitem", LOTMainsubitem);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddNewLOTMainSubitemManager(int managerRecordID, int companyID, int departmentID, int LOTMainSubitemID, int managerID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_lot_main_subitem_manager";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@managerrecordid", managerRecordID);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@managerid", managerID);
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


        public DataSet GetLotMainSubItemManagerList(int companyID, int departmentID, int LOTMainItemID, int LOTMainSubitemID, int managerID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_subitems_manager_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@managerid", managerID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetSubitemDetailsToAddManager(int unitID, int departmentID, string LOTMainSubitemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_main_subitems_to_add_manager";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@lotmainsubitemids", LOTMainSubitemIDs);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AddNewManagersBySubitemIDs(DataTable dtTempSI, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_maangers_by_lot_main_subitemids";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@tblsubitems", dtTempSI);
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

        public int AddNewManagersBySubitemIDs(int unitID, int departmentID, int LOTMainSubitemID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_maangers_by_lot_main_subitemids";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@departmentid", departmentID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
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


        public DataSet GetEquipmentOrType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_equipment_or_type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetFabricationReport(string startDate, string endDate, string internalFabricationNo, int unitID,
                                            int equipmentID, string drawingNumber, int percentageWorkDone, string productionOrderNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fabrication_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@internalfabricationno", internalFabricationNo);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@equipmentid", equipmentID);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);
            cmd.Parameters.AddWithValue("@percentageworkdone", percentageWorkDone);
            cmd.Parameters.AddWithValue("@productionorderno", productionOrderNo);



            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetFabricationListForPosting(int datetTypeID, string dateSign, string fromDate, string toDate, string LOTNo, string jobNo, string productionOrderNo, string drawingNo,
                                                    string productCode, string equipment, string unitName, int unitID, int percentageOfWork,
                                                    string postingStatus, int productionManagerID, int productionStatusTypeFlag
                                                  , int outstandingEDDateFlag
                                                  , int empRecordId, int isTransferred)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fabrication_list_for_posting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productoinorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@eqiuipment", equipment);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@percentageofwork", percentageOfWork);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@productionmanagerid", productionManagerID);
            cmd.Parameters.AddWithValue("@productionstatustypeflag", productionStatusTypeFlag);
            cmd.Parameters.AddWithValue("@outstandingeddateflag", outstandingEDDateFlag);
            cmd.Parameters.AddWithValue("@empRecordId", empRecordId);
            cmd.Parameters.AddWithValue("@isTransferred", isTransferred);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int PostFabricationList(DataTable dtTemp1, DataTable dtTemp2, string updateQuery, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_fabrication_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tblfabricationposting", dtTemp1);
            cmd.Parameters.AddWithValue("@tblfabricationpostinglog", dtTemp2);
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

        public int DeleteFabricationList(DataTable dtTemp, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_delete_fabrication_posting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tbldeletefabrication", dtTemp);
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








        //Design details


        public int AddDesignEngg(int recordID, int employeeRecordID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_design_responsible_engg";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@employeerecordid", employeeRecordID);
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

        public DataSet GetDesignResponsibleEngg()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_responsible_design_engg";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AddDesignDetail(int isDesignenggFlag, DataTable dtDesignDetailsByProject, DataTable dtDesignDetailsByDesign, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_design_details";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@isdesignenggflag", isDesignenggFlag);
            cmd.Parameters.AddWithValue("@tbldesigndetailsbyproject", dtDesignDetailsByProject);
            cmd.Parameters.AddWithValue("@tbldesigndetailsbydesign", dtDesignDetailsByDesign);
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

        public DataSet GetDesignDetailList(int datetTypeID, string dateSign, string fromDate, string toDate, int categoryID, string jobNo,
                                            //string isPlanned,
                                            string drawingNo, int postingStatusID, int designCheckerID, int responsibleEnggID, int createdByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_details_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            //cmd.Parameters.AddWithValue("@isplanned", isPlanned);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@postingstatusid", postingStatusID);
            cmd.Parameters.AddWithValue("@designcheckerid", designCheckerID);
            cmd.Parameters.AddWithValue("@responsibleenggid", responsibleEnggID);
            cmd.Parameters.AddWithValue("@createdbyid", createdByID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetRevisedDesignDetails(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_revised_design_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordid", recordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateDesignDetails(int recordID, string jobNo, int categoryID, string description, string uOM, double quantity,
                                       string reqdDateByProjectTeam, int isPlannedID, string plannedStartDateByDesignTeam, string plannedCompletionDateByDesignTeam,
                                       string drawingNo, int drawingRevNo, string expectedCompletionDate, int responsibleDesignEnggID, string postingStatus,
                                       int isApplicableForProduction, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_design_details";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@uom", uOM);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@reqddatebyprojectteam", reqdDateByProjectTeam);
            cmd.Parameters.AddWithValue("@isplannedid", isPlannedID);
            cmd.Parameters.AddWithValue("@plannedstartdatebydesignteam", plannedStartDateByDesignTeam);
            cmd.Parameters.AddWithValue("@plannedcompletiondatebydesignteam", plannedCompletionDateByDesignTeam);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@drawingrevno", drawingRevNo);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@responsibledesignenggid", responsibleDesignEnggID);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@isapplicableforproduction", isApplicableForProduction);
            cmd.Parameters.AddWithValue("@remarks", remarks);
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


        public DataSet GetDesignDetailListForProduction(int datetTypeID, string dateSign, string fromDate, string toDate,
                                           int categoryID, string jobNo, string isPlanned, string drawingNo, string postingStatus, int responsibleEnggID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_details_report_for_production";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@isplanned", isPlanned);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@responsibleenggid", responsibleEnggID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //public DataSet GetDesignResponsibleEnggList(string enggName, string enggType)
        //{
        //    DataSet ds = new DataSet();
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "sp_get_responsible_design_engg_list";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    cmd.Parameters.AddWithValue("@enggname", enggName);
        //    cmd.Parameters.AddWithValue("@enggtype", enggType);

        //    da.Fill(ds);
        //    if (ds != null)
        //        return ds;

        //    return null;
        //}




        //LOT changes [2020-08-19] onwards

        public DataSet GetProductionOrderProdDetail(int companyID, string pONo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_order_product_deatils";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@companyid", companyID);
            cmd.Parameters.AddWithValue("@pono", pONo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetFabricationPostedReport(int datetTypeID, string dateSign, string fromDate, string toDate, string jobNo, string productionOrderNo, string drawingNo,
                                                    string productCode, string equipment, string unitName, int unitID, int percentageOfWork, string postingStatus)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_fabrication_posted_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productoinorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@eqiuipment", equipment);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@percentageofwork", percentageOfWork);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBDetailsForDesign(string custCode, string jobNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_detail_for_project02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDesignCategoryList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_category";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDesignStatusDesignViewList(int datetTypeID, string dateSign, string fromDate, string toDate, int categoryID, string jobNo, string isPlanned,
                                            string drawingNo, int postingStatusID, int responsibleEnggID, int empRecordID, int typeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_status_design_view_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@isplanned", isPlanned);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@postingstatusid", postingStatusID);
            cmd.Parameters.AddWithValue("@responsibleenggid", responsibleEnggID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@typeid", typeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertDesignTimesheet(int recordID, int empRecordID,
                                        string projectCategory, string projectNumber, string jobNumber,
                                        string drawingCategory, string serialNumber, string size,
                                        string rev, int sheets, string drawingID, int drawingTypeID,
                                        string startTime, string endTime, string timeSpent, string title, string timesheetDate,
                                        string remarks, string status, string expectedCompletionDate, string totalTimeSpent, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_design_timesheet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@projectcategory", projectCategory);
            cmd.Parameters.AddWithValue("@projectnumber", projectNumber);
            cmd.Parameters.AddWithValue("@jobnumber", jobNumber);
            cmd.Parameters.AddWithValue("@drawingcategory", drawingCategory);
            cmd.Parameters.AddWithValue("@serialnumber", serialNumber);
            cmd.Parameters.AddWithValue("@size", size);
            cmd.Parameters.AddWithValue("@rev", rev);
            cmd.Parameters.AddWithValue("@sheets", sheets);
            cmd.Parameters.AddWithValue("@drawingid", drawingID);
            cmd.Parameters.AddWithValue("@drawingtypeid", drawingTypeID);
            cmd.Parameters.AddWithValue("@starttime", startTime);
            cmd.Parameters.AddWithValue("@endtime", endTime);
            cmd.Parameters.AddWithValue("@timespent", timeSpent);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@timesheetdate", timesheetDate);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@totaltimespent", totalTimeSpent);
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


        public int UpdateLOTDrawings(int recordID, string siDrawingFile1, byte[] siDrawingFileBytes1,
                                                    string siDrawingFile2, byte[] siDrawingFileBytes2,
                                                    string siDrawingFile3, byte[] siDrawingFileBytes3,
                                                    string siDrawingFile4, byte[] siDrawingFileBytes4)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_drawings";//sp_revise_lot
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@recordid", recordID);


            cmd.Parameters.AddWithValue("@sidrawingfile1", siDrawingFile1);
            if (siDrawingFileBytes1 != null)
                cmd.Parameters.AddWithValue("@sidrawingfilebytes1", siDrawingFileBytes1);
            else
                cmd.Parameters.AddWithValue("@sidrawingfilebytes1", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@sidrawingfile2", siDrawingFile2);
            if (siDrawingFileBytes2 != null)
                cmd.Parameters.AddWithValue("@sidrawingfilebytes2", siDrawingFileBytes2);
            else
                cmd.Parameters.AddWithValue("@sidrawingfilebytes2", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@sidrawingfile3", siDrawingFile3);
            if (siDrawingFileBytes3 != null)
                cmd.Parameters.AddWithValue("@sidrawingfilebytes3", siDrawingFileBytes3);
            else
                cmd.Parameters.AddWithValue("@sidrawingfilebytes3", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@sidrawingfile4", siDrawingFile4);
            if (siDrawingFileBytes4 != null)
                cmd.Parameters.AddWithValue("@sidrawingfilebytes4", siDrawingFileBytes4);
            else
                cmd.Parameters.AddWithValue("@sidrawingfilebytes4", System.Data.SqlTypes.SqlBinary.Null);

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


        public int UpdateLOTClientApprovedDrawing(int lotTfId, string clientApprovedDrawingFile, byte[] clientApprovedDrawingFileBytes)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_client_approved_drawing";//sp_revise_lot
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lotTfId", lotTfId);

            cmd.Parameters.AddWithValue("@clientApprovedDrawingFile", clientApprovedDrawingFile);
            if (clientApprovedDrawingFileBytes != null)
                cmd.Parameters.AddWithValue("@clientApprovedDrawingFileBytes", clientApprovedDrawingFileBytes);
            else
                cmd.Parameters.AddWithValue("@clientApprovedDrawingFileBytes", System.Data.SqlTypes.SqlBinary.Null);




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



        public DataSet GetUserLastEndTimeForDesign(int empRecordID, string entryDate, string JOBNo, string drawingID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_last_end_time_for_design";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@entrydate", entryDate);
            cmd.Parameters.AddWithValue("@jobnumber", JOBNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTimesheetDeptListForAllDeptsReport()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_timesheet_dept_list_for_all_depts_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetLOTCombinedReport(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                            string jobNo, string customerName, int LOTMainSubitemID, int isPartOfProductionFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_combined_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@ispartofproductionflag", isPartOfProductionFlag);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        //DMS Changes- 2020-10-27

        public DataSet GetDepartmentForDMS()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_department_for_dms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDesignStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_detail_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //public int AddDesignEngg(int recordID, int employeeRecordID, int createdBy)
        //{
        //    string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        //    SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
        //    cmd.CommandText = "sp_insert_update_design_responsible_engg";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@recordid", recordID);
        //    cmd.Parameters.AddWithValue("@employeerecordid", employeeRecordID);
        //    cmd.Parameters.AddWithValue("@createdby", createdBy);
        //    cmd.Connection = con;
        //    cmd.Parameters.Add(rcode);
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        int value = Convert.ToInt32(rcode.Value);
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

        public DataSet GetDesignResponsibleEnggList(string enggName, int deptID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_responsible_design_engg_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@enggname", enggName);
            cmd.Parameters.AddWithValue("@deptid", deptID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJOBDetailsForDesignDrawings(int unitID, string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_detail_for_design_drawings";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDesignDrawingsList(string fromDate, string toDate, string jobNo, string drawingNo, string isActive)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_drawing_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@isactive", isActive);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int RemoveOrActivateDrawwing(int recordID, int activeValue, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_remove_or_active_design_drawing";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@activevalue", activeValue);
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


        public int AddUpdateDesignDetail(int recordID, string JOBNo, int categoryID, string description, string UOM,
                                   int quantity, string reqdDateByProjectTeam, int isPlannedID, string plannedStartDateByDesignTeam,
                                   string plannedCompletionDateByDesignTeam, string drawingNo, string drawingNoLink, int drawingRevNo, string expectedCompletionDate,
                                   int responsibleDesignEnggID, int postingStatusID, int isApplicableForProduction, string remarks,
                                   int isRevised, int revisedRecordID, int amendmentCount,
                                   int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_design_details";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@uom", UOM);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@reqddatebyprojectteam", reqdDateByProjectTeam);
            cmd.Parameters.AddWithValue("@isplannedid", isPlannedID);
            cmd.Parameters.AddWithValue("@plannedstartdatebydesignteam", plannedStartDateByDesignTeam);
            cmd.Parameters.AddWithValue("@plannedcompletiondatebydesignteam", plannedCompletionDateByDesignTeam);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@drawingnolink", drawingNoLink);
            cmd.Parameters.AddWithValue("@drawingrevno", drawingRevNo);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@responsibledesignenggid", responsibleDesignEnggID);
            cmd.Parameters.AddWithValue("@postingstatusid", postingStatusID);
            cmd.Parameters.AddWithValue("@isapplicableforproduction", isApplicableForProduction);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@isrevised", isRevised);
            cmd.Parameters.AddWithValue("@revisedrecordid", revisedRecordID);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
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



        public DataSet GetDMSMailInfo(int drawingID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_dms_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordid", drawingID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateMailSentStatus(int recordID, int typeID, int amendmentCount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_dms_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@typeid", typeID);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
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

        public int UpdateDesignDetailsStatus(int recordID, int statusID, int responsibleDesignEnggID,
                                             string plannedStartDateByDesignTeam, string plannedCompletionDateByDesignTeam,
                                             //string expectedCompletionDate, 
                                             string remarks, int amendmentCount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_dms_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@responsibledesignenggid", responsibleDesignEnggID);
            cmd.Parameters.AddWithValue("@statusid", statusID);

            cmd.Parameters.AddWithValue("@plannedstartdate", plannedStartDateByDesignTeam);
            cmd.Parameters.AddWithValue("@plannedcompletedate", plannedCompletionDateByDesignTeam);
            //cmd.Parameters.AddWithValue("@expectedcompletedate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
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


        public DataSet GetDesignResponsibleEnggDesignAndProjectView(int typeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_responsible_design_engg_for_design_and_project";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@typeid", typeID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetJOBDetailsForProjectDrawings(int unitID, string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_detail_for_project_drawings";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int AddDesignDrawing(DataTable dt)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_design_drawing";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tbldrawings", dt);
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

        public int UpdateDesignDrawing(int recordID, string jobNo, string drawingNo, string clientDrawingNo, string contractorDrawingNo,
                                       string description, double quantity, string UOM, string rqdDateByProjectTeam, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_design_drawing";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);

            cmd.Parameters.AddWithValue("@clientdrawingno", clientDrawingNo);
            cmd.Parameters.AddWithValue("@contractordrawingno", contractorDrawingNo);

            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@uom", UOM);
            cmd.Parameters.AddWithValue("@rqddatebyprojectteam", rqdDateByProjectTeam);
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

        public DataSet GetJOBDraiwngs(string jobNo, string drawingNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_drawings_for_dms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetJOBDraiwngsForDMS(string searchQuery)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_drawings_for_dms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@searchquery", searchQuery);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDMSDrawingDetailForPDF(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_dms_drawing_details_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordid", recordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AddDesignDetail(int statusID, int drawingID, string JOBNo, string description, string UOM, int quantity,
                                    string reqdDateByProjectTeam, int categoryID,
                                    string plannedStartDateByDesignTeam, string plannedCompletionDateByDesignTeam,
                                    string drawingNo, string documentLink, int drawingRevNo, string workingStatus, string expectedCompletionDate,
                                    int responsibleDesignEngineerID, string remarks, int isDesignEnggFlag, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_design_details";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@drawingid", drawingID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@uom", UOM);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@reqddatebyprojectteam", reqdDateByProjectTeam);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@plannedstartdatebydesigneam", plannedStartDateByDesignTeam);
            cmd.Parameters.AddWithValue("@plannedcompletiondatebydesignteam", plannedCompletionDateByDesignTeam);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@documentlink", documentLink);
            cmd.Parameters.AddWithValue("@drawingrevno", drawingRevNo);
            cmd.Parameters.AddWithValue("@workingstatus", workingStatus);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@responsibledesignengineerid", responsibleDesignEngineerID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@isdesignenggflag", isDesignEnggFlag);
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


        public int UpdateDesignMailStatus(int recordID, int statusID, int amendmentCount, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_design_details_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
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

        public int AddDesignChecker(int recordID, int employeeRecordID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_design_checker";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@employeerecordid", employeeRecordID);
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

        public DataSet GetDesignCheckerList(string checkerName, int deptID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_checker_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@checkername", checkerName);
            cmd.Parameters.AddWithValue("@deptid", deptID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDesignChecker()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_checker";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int AssignToDesignEngineer(int recordID, string plannedStartDate, string plannedCompletedDate, int designResponsibleEnggID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_assign_design_to_responsible_design_engg";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@plannedstartdate", plannedStartDate);
            cmd.Parameters.AddWithValue("@plannedcompleteddate", plannedCompletedDate);
            cmd.Parameters.AddWithValue("@designresponsibleenggid", designResponsibleEnggID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
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


        public int SendToChecking(int recordID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_assign_design_to_responsible_design_engg";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
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

        public int UpdateDesignStatus(int recordID, int statusID, string expectedCompletionDate, string plannedStartDate, string plannedCompletedDate, int designResponsibleEnggID,
                                     string drawingLink, int designCheckerID, string remarks, int sentToAmendementFlag, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_design_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@plannedstartdate", plannedStartDate);
            cmd.Parameters.AddWithValue("@plannedcompleteddate", plannedCompletedDate);
            cmd.Parameters.AddWithValue("@designresponsibleenggid", designResponsibleEnggID);
            cmd.Parameters.AddWithValue("@drawinglink", drawingLink);
            cmd.Parameters.AddWithValue("@designcheckerid", designCheckerID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@senttoamendementflag", sentToAmendementFlag);
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


        public int UpdateDesignDetail(int recordID, int statusID, string JOBNo, int jobUnitID, string description, string UOM, int quantity,
                                        string reqdDateByProjectTeam, int categoryID,
                                        string plannedStartDateByDesignTeam, string plannedCompletionDateByDesignTeam,
                                        string drawingNo, string clientDrawingNo, string contractorDrawingNo,
                                        int drawingRecordID, string documentLink, int drawingRevNo, string workingStatus, string expectedCompletionDate,
                                        int responsibleDesignEngineerID, string remarks, int isDesignEnggFlag, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_design_details";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@jobunitid", jobUnitID);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@uom", UOM);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@reqddatebyprojectteam", reqdDateByProjectTeam);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@plannedstartdatebydesigneam", plannedStartDateByDesignTeam);
            cmd.Parameters.AddWithValue("@plannedcompletiondatebydesignteam", plannedCompletionDateByDesignTeam);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);

            cmd.Parameters.AddWithValue("@clientdrawingno", clientDrawingNo);
            cmd.Parameters.AddWithValue("@contractordrawingno", contractorDrawingNo);

            cmd.Parameters.AddWithValue("@drawingrid", drawingRecordID);
            cmd.Parameters.AddWithValue("@documentlink", documentLink);
            cmd.Parameters.AddWithValue("@drawingrevno", drawingRevNo);
            cmd.Parameters.AddWithValue("@workingstatus", workingStatus);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@responsibledesignengineerid", responsibleDesignEngineerID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@isdesignenggflag", isDesignEnggFlag);
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



        public DataSet GetDesignDetailListForProjectView(int datetTypeID, string dateSign, string fromDate, string toDate, int categoryID, string jobNo,
                                          //string isPlanned,
                                          string drawingNo, int postingStatusID, int designCheckerID, int responsibleEnggID, int createdByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_details_project_view_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@postingstatusid", postingStatusID);
            cmd.Parameters.AddWithValue("@designcheckerid", designCheckerID);
            cmd.Parameters.AddWithValue("@responsibleenggid", responsibleEnggID);
            cmd.Parameters.AddWithValue("@createdbyid", createdByID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetDMSMailInfo(string drawingNos)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_dms_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@drawingno", drawingNos);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateDesignMailStatus(string drawingNos, int recordID, int statusID, int amendmentCount, int amendmentFlag, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_design_details_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@drawingnos", drawingNos);
            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
            cmd.Parameters.AddWithValue("@amendmentflag", amendmentFlag);
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

        public DataSet CheckDrawingForDuplicacy(string drawingNo, int drawingID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_duplicate_drawing_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@drawingid", drawingID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetDMSMailInfo(int drawingID, string drawingNos)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_dms_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordid", drawingID);
            cmd.Parameters.AddWithValue("@drawingno", drawingNos);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet CheckDesignDrawingForDuplicacy(string drawingNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_duplicate_design_drawing_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@drawingno", drawingNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int CheckForPrimaryDuplicacy(string drawingNo)
        {

            int drawingCount = 0;
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_duplicate_drawing_counts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@drawing_no", drawingNo);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    drawingCount = Convert.ToInt32(rdr["DRAWING_COUNT"]);
                }
                con.Close();
            }
            catch (Exception)
            {
                drawingCount = -1;
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return drawingCount;
        }

        public int CheckDuplicacyOfSingleDrawing(string drawingNo)
        {

            int drawingCount = 0;
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_duplicacy_of_single_drawing";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@drawing_no", drawingNo);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    drawingCount = Convert.ToInt32(rdr["DRAWING_COUNT"]);
                }
                con.Close();
            }
            catch (Exception)
            {
                drawingCount = -1;
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return drawingCount;
        }



        public DataSet GetDesignDetailListForRevision(int datetTypeID, string dateSign, string fromDate, string toDate, int categoryID, string jobNo,
                                                      string drawingNo, int responsibleEnggID, int createdByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_list_for_revision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@responsibleenggid", responsibleEnggID);
            cmd.Parameters.AddWithValue("@createdbyid", createdByID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int ReviseDesignDetail(int recordID, int drawingID, int statusID, string JOBNo, int jobUnitID, string description, string UOM, int quantity,
                                        string reqdDateByProjectTeam, int categoryID,
                                        string plannedStartDateByDesignTeam, string plannedCompletionDateByDesignTeam,
                                        string drawingNo, string clientDrawingNo, string contractorDrawingNo,
                                        string documentLink, int drawingRevNo, string workingStatus, string expectedCompletionDate,
                                        int responsibleDesignEngineerID, string remarks, int isDesignEnggFlag,
                                        string additionalAttachmentFile, byte[] additionalAttachmentFileBytes,
                                        int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_revise_design_details";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@drawingid", drawingID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@jobno", JOBNo);
            cmd.Parameters.AddWithValue("@jobunitid", jobUnitID);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@uom", UOM);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@reqddatebyprojectteam", reqdDateByProjectTeam);
            cmd.Parameters.AddWithValue("@categoryid", categoryID);
            cmd.Parameters.AddWithValue("@plannedstartdatebydesigneam", plannedStartDateByDesignTeam);
            cmd.Parameters.AddWithValue("@plannedcompletiondatebydesignteam", plannedCompletionDateByDesignTeam);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);

            cmd.Parameters.AddWithValue("@clientdrawingno", clientDrawingNo);
            cmd.Parameters.AddWithValue("@contractordrawingno", contractorDrawingNo);

            cmd.Parameters.AddWithValue("@documentlink", documentLink);
            cmd.Parameters.AddWithValue("@drawingrevno", drawingRevNo);
            cmd.Parameters.AddWithValue("@workingstatus", workingStatus);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@responsibledesignengineerid", responsibleDesignEngineerID);
            cmd.Parameters.AddWithValue("@remarks", remarks);

            cmd.Parameters.AddWithValue("@additionalattachmentfile", additionalAttachmentFile);
            if (additionalAttachmentFileBytes != null)
                cmd.Parameters.AddWithValue("@additionalattachmentfilebytes", additionalAttachmentFileBytes);
            else
                cmd.Parameters.AddWithValue("@additionalattachmentfilebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@isdesignenggflag", isDesignEnggFlag);
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


        public DataSet GetProductionManagers()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_manager_for_production_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int CheckTableUpdate(int srNo, int LOTTfId, DataTable dtCheck)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_check_table_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@srno", srNo);
            cmd.Parameters.AddWithValue("@lottfid", LOTTfId);
            cmd.Parameters.AddWithValue("@tblchk", dtCheck);

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



        public DataSet GetReviseDesignAddAttachmentFiles(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_design_add_attachment_files";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordid", recordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        //LOT Updations -2021-10-07

        public DataSet GetLOTTFListForCancellation(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_list_for_cancellation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int CancelLOT(int LOTTFID, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_cancel_lot";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
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

        public int UpdateCancelledMailStatus(int LOTTFID, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_cancellation_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
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

        public DataSet GetLOTTFDetailsForCancellation(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details_for_cancellation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetCancelledLOTMailInfo(int LOTTFID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cancelled_lot_tf_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetCancelledLOTReport(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                   string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cancelled_lot_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetCancelledLOTSubitemsList(int LOTTFID, string LOTMainSubitemIDs, string LOTTFSubitemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cancelled_lot_subitem_detail_for_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lotmainsubitemids", LOTMainSubitemIDs);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubitemIDs);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCancelledLOTTFDetailsForPDF(int LOTTFID, string LOTTFSubitemIDs)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "sp_get_cancelled_lot_tf_details";
            cmd.CommandText = "sp_get_cancelled_lot_tf_details_test";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemids", LOTTFSubitemIDs);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetCAncelledLOTDrawingFiles(int LOTTFID, int LOTTFSubitemID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_cancelled_lot_drawing_files";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@lottfsubitemid", LOTTFSubitemID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetLOTProductionOrderWiseReport(string startDate, string endDate, string jobNo, string productionOrderNo,
                                                       string productCode, string drawingNo, int unitID, string productDesc, int mainSubitemID,
                                                       int isPartOfProductionID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_production_order_wise_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productionrrderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@mainsubitemid", mainSubitemID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@productdesc", productDesc);
            cmd.Parameters.AddWithValue("@ispartofproductionid", isPartOfProductionID);



            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetProductionStatusReportProdMngrWise(int datetTypeID, string dateSign, string fromDate, string toDate, string jobNo, string productionOrderNo, string drawingNo,
                                                        string productCode, string equipment, string unitName, int unitID, int percentageOfWork, string postingStatus)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_status_report_prod_mngr_wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productoinorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@eqiuipment", equipment);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@percentageofwork", percentageOfWork);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetProductionStatusReportJOBWise(int datetTypeID, string dateSign, string fromDate, string toDate, string jobNo, string productionOrderNo, string drawingNo,
                                                        string productCode, string equipment, string unitName, int unitID, int percentageOfWork, string postingStatus)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_status_report_job_wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productoinorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@eqiuipment", equipment);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@percentageofwork", percentageOfWork);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetProductionStatusReportStatusWise(int datetTypeID, string dateSign, string fromDate, string toDate, string jobNo, string productionOrderNo, string drawingNo,
                                                        string productCode, string equipment, string unitName, int unitID, int percentageOfWork, string postingStatus)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_status_report_status_wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productoinorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@eqiuipment", equipment);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@percentageofwork", percentageOfWork);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int SaveClientApprovedDrawing(int LOTTFID,
                                             string clientApproveDrawingFile,
                                             byte[] clientApproveDrawingFileBytes,
                                             string clientApproveDrawingRemarks,
                                             int seqNo,
                                             int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_add_client_approved_drawing";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@LotTfId", LOTTFID);
            cmd.Parameters.AddWithValue("@ClientApprovedDrawingRemarks", clientApproveDrawingRemarks);

            cmd.Parameters.AddWithValue("@ClientApprovedDrawingFile", clientApproveDrawingFile);
            if (clientApproveDrawingFileBytes != null)
                cmd.Parameters.AddWithValue("@ClientApprovedDrawingFileBytes", clientApproveDrawingFileBytes);
            else
                cmd.Parameters.AddWithValue("@ClientApprovedDrawingFileBytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@SeqNo", seqNo);
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

        public DataSet GetClientApprovedDrawingMailInfo(int LOTTFID, int seqNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_client_approved_drawing_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@seqNo", seqNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateClientApprovedDrawingMailStatus(int LOTTFID, int seqNo, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_client_appoved_drawing_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@LotTfId", LOTTFID);
            cmd.Parameters.AddWithValue("@SeqNo", seqNo);
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


        public DataSet GetDMSDrawingList(int companyID, string jobNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_dms_checked_drawing_list_for_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //Get LOT list for expected completion date updation    Deekshant Ravi:2022-07-26
        public DataSet GetLOTTFListExpectedCompletionDateUpdation(string startDate, string endDate, string lOTTFNo, int statusID, int unitID,
                                string jobNo, string customerName, int LOTMainItemID, int LOTMainSubitemID, string tagNumber, string drawingNumber,
                                string productionOrderNumber)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_list_for_expected_completion_date_updation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@tagnumber", tagNumber);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);
            cmd.Parameters.AddWithValue("@productionordernumber", productionOrderNumber);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateExpectedCompletionDate(string lOTTFSubitemIDs, string productionOrderNo, string expectedCompletionDate
                                              , string expectedCompletionDateRemarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_expected_completion_date";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfsubitemids", lOTTFSubitemIDs);
            cmd.Parameters.AddWithValue("@productionorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@expectedcompletiondate", expectedCompletionDate);
            cmd.Parameters.AddWithValue("@updatedremarks", expectedCompletionDateRemarks);
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


        public DataSet GetUpdatedExpCompDateMailInfo(int LOTTFID, string productionOrderNumber)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_updated_exp_comp_date_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@productionordernumber", productionOrderNumber);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateExpectedCompletionDateMailStatus(string lOTTFSubitemIDs, string productionOrderNo, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_lot_exp_comp_date_mail_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfsubitemids", lOTTFSubitemIDs);
            cmd.Parameters.AddWithValue("@productionorderno", productionOrderNo);
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

        public DataSet GetWorkloadReleasedReport(int datetTypeID, string dateSign, string fromDate, string toDate, string LOTNo, string jobNo, string productionOrderNo, string drawingNo,
                                                    string productCode, string equipment, string unitName, int unitID, int percentageOfWork,
                                                    string postingStatus, int productionManagerID, int productionStatusTypeFlag)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_workload_released_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@datetypeid", datetTypeID);
            cmd.Parameters.AddWithValue("@datesign", dateSign);
            cmd.Parameters.AddWithValue("@startdate", fromDate);
            cmd.Parameters.AddWithValue("@enddate", toDate);
            cmd.Parameters.AddWithValue("@lotno", LOTNo);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@productoinorderno", productionOrderNo);
            cmd.Parameters.AddWithValue("@drawingno", drawingNo);
            cmd.Parameters.AddWithValue("@productcode", productCode);
            cmd.Parameters.AddWithValue("@eqiuipment", equipment);
            cmd.Parameters.AddWithValue("@unit", unitName);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@percentageofwork", percentageOfWork);
            cmd.Parameters.AddWithValue("@postingstatus", postingStatus);
            cmd.Parameters.AddWithValue("@productionmanagerid", productionManagerID);
            cmd.Parameters.AddWithValue("@productionstatustypeflag", productionStatusTypeFlag);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //2022-01-09    Transfer of LOT starts

        public DataSet GetLOTTFListForTransferOfLOT(string startDate, string endDate, string lOTTFNo, int statusID
                                                  , int unitID, string jobNo, string customerName, int LOTMainItemID
                                                  , int LOTMainSubitemID, string tagNumber, string drawingNumber
                                                  , int transferStatusId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_list_for_transfer_of_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@lottfno", lOTTFNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@unitid", unitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@lotmainitemid", LOTMainItemID);
            cmd.Parameters.AddWithValue("@lotmainsubitemid", LOTMainSubitemID);
            cmd.Parameters.AddWithValue("@tagnumber", tagNumber);
            cmd.Parameters.AddWithValue("@drawingnumber", drawingNumber);
            cmd.Parameters.AddWithValue("@transferStatusId", transferStatusId);


            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public int TransferOfLOT(int LOTTFID, string tFNo, int companyID, string LOTDate, string custCode, string jobNo, string poNo, string itemName, string impNotes,
                                        string attachment1File, byte[] attachment1FileBytes,
                                        string attachment2File, byte[] attachment2FileBytes,
                                        string attachment3File, byte[] attachment3FileBytes,
                                        string attachment4File, byte[] attachment4FileBytes,
                                        DataTable dtSubitemToAdd, string LOTTFsubitemIDsToVoid
                                      , string remarks
                                      , int createdByID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_transfer_of_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@tfno", tFNo);
            cmd.Parameters.AddWithValue("@unitid", companyID);
            cmd.Parameters.AddWithValue("@date", LOTDate);
            cmd.Parameters.AddWithValue("@customercode", custCode);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@itemname", itemName);
            cmd.Parameters.AddWithValue("@impnotes", impNotes);

            cmd.Parameters.AddWithValue("@attachment1filename", attachment1File);
            if (attachment1FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment1filebytes", attachment1FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment1filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment2filename", attachment2File);
            if (attachment2FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment2filebytes", attachment2FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment2filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment3filename", attachment3File);
            if (attachment3FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment3filebytes", attachment3FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment3filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@attachment4filename", attachment4File);
            if (attachment4FileBytes != null)
                cmd.Parameters.AddWithValue("@attachment4filebytes", attachment4FileBytes);
            else
                cmd.Parameters.AddWithValue("@attachment4filebytes", System.Data.SqlTypes.SqlBinary.Null);


            cmd.Parameters.AddWithValue("@tbllottranstofactorysubitms", dtSubitemToAdd);
            cmd.Parameters.AddWithValue("@lottfsubitemidstovoid", LOTTFsubitemIDsToVoid);


            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@createdby", createdByID);

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


        public DataSet GetLOTTFDetailsForLOTTransfer(int LOTTFID, DataTable dtLotFor)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_lot_tf_details_for_lot_transfer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@tblSubitems", dtLotFor);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateProductionOrderOnTransferredLOT(int LOTTFID, int statusId
                                                , DataTable dtSubitems
                                                , string remarks
                                                , int createdByID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_status_of_transferred_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@statusId", statusId);
            cmd.Parameters.AddWithValue("@tblSubitems", dtSubitems);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@createdby", createdByID);

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


        public int UpdateProductionOrderOnTransferredLOT(int LOTTFID
                                              , DataTable dtSubitems
                                              , string remarks
                                              , int createdByID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_production_order_on_transferred_lot"; //sp_update_status_of_transferred_lot
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID); ;
            cmd.Parameters.AddWithValue("@tblSubitems", dtSubitems);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@createdby", createdByID);

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

        public DataSet GetProductionAndQualityManagers(int unitId, int departmentId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_production_and_quality_managers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@unitId", unitId);
            cmd.Parameters.AddWithValue("@departmentId", departmentId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetNewSubitemsListForLOTTransfer(int LOTTFID, int createdBy)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_insert_lot_main_subitems_after_transfer_of_lot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@lottfid", LOTTFID);
            cmd.Parameters.AddWithValue("@createdby", createdBy);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetHistoricalPOReport(
                              string year
                            , string orderNo
                            , string jobNo
                            , string startDate
                            , string endDate
                            , string unitName
                            , string vcCode
                            , string vcName
                            , string productCode
                            , string productDesc
                            , string productGroup
                            , string productSubgroup
                            , string gstHsnCode
                            , string pivotGroup
            )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Hist_And_Current_Year_AllUnitsPo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@orderNo", orderNo);
            cmd.Parameters.AddWithValue("@jobNo", jobNo);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@unitName", unitName);
            cmd.Parameters.AddWithValue("@vcCode", vcCode);
            cmd.Parameters.AddWithValue("@vcName", vcName);
            cmd.Parameters.AddWithValue("@productCode", productCode);
            cmd.Parameters.AddWithValue("@productDesc", productDesc);
            cmd.Parameters.AddWithValue("@productGroup", productGroup);
            cmd.Parameters.AddWithValue("@productSubgroup", productSubgroup);
            cmd.Parameters.AddWithValue("@gstHsnCode", gstHsnCode);
            cmd.Parameters.AddWithValue("@pivotGroup", pivotGroup);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetHistoricalSIReport(
                             string year
                           , string billNo
                           , string orderNo
                           , string jobNo
                           , string startDate
                           , string endDate
                           , string unitName
                           , string vcCode
                           , string vcName
                           , string productCode
                           , string productDesc
                           , string productGroup
                           , string productSubgroup
                           , string gstHsnCode
                           , string pocNpoc
                           , string endMarket
                           , string geography
           )
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Hist_And_Current_Year_AllUnitsSi";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@billNo", billNo);
            cmd.Parameters.AddWithValue("@orderNo", orderNo);
            cmd.Parameters.AddWithValue("@jobNo", jobNo);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@unitName", unitName);
            cmd.Parameters.AddWithValue("@vcCode", vcCode);
            cmd.Parameters.AddWithValue("@vcName", vcName);
            cmd.Parameters.AddWithValue("@productCode", productCode);
            cmd.Parameters.AddWithValue("@productDesc", productDesc);
            cmd.Parameters.AddWithValue("@productGroup", productGroup);
            cmd.Parameters.AddWithValue("@productSubgroup", productSubgroup);
            cmd.Parameters.AddWithValue("@gstHsnCode", gstHsnCode);
            cmd.Parameters.AddWithValue("@pocNpoc", pocNpoc);
            cmd.Parameters.AddWithValue("@endMarket", endMarket);
            cmd.Parameters.AddWithValue("@geography", geography);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetJOBDetailsForShipping(int companyID, string custCode, string jobNo, string poNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_job_detail_for_project01";
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


    }
}