using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class TourAndTravels
    {
        public DataSet GetPrimaryDetails(string ProcedureName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = ProcedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetEmployeeForTravel(int employeeRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_for_travels";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@employeeid", employeeRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetEmployeeForServiceDeptTravelReport()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_for_service_travel_summary_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //cmd.Parameters.AddWithValue("@employeeid", employeeRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetEmployeeList(int employeeRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_emplyoee_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@employeeid", employeeRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertTourInformation(int tourID, int empRecordID, string startDate, string endDate,
                                        string custVendName, string placeOfVisit,
                                        int purposeOfVisitID, string jobNo,
                                        int busSegmentID, string busSegmentOther,
                                        int modeOfTavelID, string modeOfTavelOther,
                                        int tripTypeID, int localTravellingID, string localTravellingOther,
                                        double expectedExpenditure, int expectedExpenditureCurrencyID,
                                        double advanceRequired, int advanceRequiredCurrencyID, string remarks,
                                        string fileUploadPassportCopyFileName, byte[] fileUploadPassportCopyBytes,
                                        int createdBy, int countryOfVisit, int tourBasedOnId,
                                        int tourInitiativeID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_tour_information01";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tourid", tourID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@custvendname", custVendName);
            cmd.Parameters.AddWithValue("@placeofvisit", placeOfVisit);
            ////
            cmd.Parameters.AddWithValue("@countryofvisit", countryOfVisit);
            cmd.Parameters.AddWithValue("@tourbasedon", tourBasedOnId);
            /////
            cmd.Parameters.AddWithValue("@purposeofvisitid", purposeOfVisitID);
            cmd.Parameters.AddWithValue("@jobno", jobNo);
            cmd.Parameters.AddWithValue("@bussegmentid", busSegmentID);
            cmd.Parameters.AddWithValue("@bussegmentother", busSegmentOther);
            cmd.Parameters.AddWithValue("@modeoftavelid", modeOfTavelID);
            cmd.Parameters.AddWithValue("@modeoftavelother", modeOfTavelOther);
            cmd.Parameters.AddWithValue("@triptypeid", tripTypeID);
            cmd.Parameters.AddWithValue("@localtravellingid", localTravellingID);
            cmd.Parameters.AddWithValue("@localtravellingother", localTravellingOther);
            cmd.Parameters.AddWithValue("@expectedexpenditure", expectedExpenditure);
            cmd.Parameters.AddWithValue("@expectedexpenditurecurrencyid", expectedExpenditureCurrencyID);
            cmd.Parameters.AddWithValue("@advancerequired", advanceRequired);
            cmd.Parameters.AddWithValue("@advancecurrencyid", advanceRequiredCurrencyID);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@tourinitiativeid", tourInitiativeID);

            cmd.Parameters.AddWithValue("@passportcopyfilename", fileUploadPassportCopyFileName);
            if (fileUploadPassportCopyBytes != null)
                cmd.Parameters.AddWithValue("@passportcopybytes", fileUploadPassportCopyBytes);
            else
                cmd.Parameters.AddWithValue("@passportcopybytes", System.Data.SqlTypes.SqlBinary.Null);

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

        public DataSet GetTourNoInfo(int tourID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_info01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@tourid", tourID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        
        public DataSet GetAddAdvanceRequestStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_additional_adv_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetTourList(string startDate, string endDate, string tourNoSearch, string sanctionNoSearch, int tourStatusID, int empRecordID, int teamMemberID, string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_list04";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@tourno", tourNoSearch);
            cmd.Parameters.AddWithValue("@sanctionno", sanctionNoSearch);
            cmd.Parameters.AddWithValue("@tourstatusid", tourStatusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@teammemberid", teamMemberID);
            cmd.Parameters.AddWithValue("@teammembers", teamMembers);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetAdditionalAdvList(string startDate, string endDate, string tourNoSearch, string sanctionNoSearch, int tourStatusID, int empRecordID, int teamMemberID, string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_additional_adv_request_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@tourno", tourNoSearch);
            cmd.Parameters.AddWithValue("@sanctionno", sanctionNoSearch);
            cmd.Parameters.AddWithValue("@tourstatusid", tourStatusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@teammemberid", teamMemberID);
            cmd.Parameters.AddWithValue("@teammembers", teamMembers);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }









        public int UpdateTourStatus(int tourID, int tourStatusID, int actID, string remarks, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_tour_status_02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tourid", tourID);
            cmd.Parameters.AddWithValue("@tourstatusid", tourStatusID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);

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


        public string UpdateTourStatusString(int tourID, int tourStatusID, int actID, string remarks, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_tour_status_03"; //this is responsible for tour_sanction_number
            //cmd.CommandText = "sp_update_tour_status_03_NEW_02"; //this is responsible for tour_sanction_number(working)OLD
            //cmd.CommandText = "sp_update_tour_status_03_NEW_01"; //this is responsible for tour_sanction_number
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tourid", tourID);
            cmd.Parameters.AddWithValue("@tourstatusid", tourStatusID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);

            cmd.Parameters.Add(rcode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                string value = Convert.ToString(rcode.Value);
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




        public string UpdateAddAdvStatusString(int requestID, int requestStatusID, int actID, string remarks, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_add_adv_status_03"; //this is responsible for tour_sanction_number
            //cmd.CommandText = "sp_update_tour_status_03_NEW_02"; //this is responsible for tour_sanction_number(working)OLD
            //cmd.CommandText = "sp_update_tour_status_03_NEW_01"; //this is responsible for tour_sanction_number
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@requestid", requestID);
            cmd.Parameters.AddWithValue("@requeststatusid", requestStatusID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);

            cmd.Parameters.Add(rcode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                string value = Convert.ToString(rcode.Value);
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


        public DataSet GetTourInformationReport(string startDate, string endDate, string tourNo, int tourStatusID, int empRecordID, int teamMemberID, string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_information_report03";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@tourno", tourNo);
            cmd.Parameters.AddWithValue("@tourstatusid", tourStatusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@teammemberid", teamMemberID);
            cmd.Parameters.AddWithValue("@teammembers", teamMembers);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourInfoByTourID(int tourID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_info_by_tour_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@tourid", tourID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSanctionNo(int employeeRecordID, int chkSetteled)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_sanction_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@employeeid", employeeRecordID);
            cmd.Parameters.AddWithValue("@chksetteled", chkSetteled);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetSanctionNo(string tourSanctionNo, string employeeName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_info_for_tour_expense";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@sanctionno", tourSanctionNo);
            cmd.Parameters.AddWithValue("@employeename", employeeName);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourInformaionBySanctionNo(string tourSanctionNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_info_by_sanction_no";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@sanctionno", tourSanctionNo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertUpdateTravelStatement(int travelStatementID, int tourID,
                                                string visitRptSummaryOneFileName, byte[] visitRptSummaryOneBytes,
                                                string visitRptSummaryTwoFileName, byte[] visitRptSummaryTwoBytes,
                                                string visitRptSummaryThreeFileName, byte[] visitRptSummaryThreeBytes,
                                                double airfareAmt, double telephoneMobAmt, double lodgingAmt,
                                                double tipsAmt, double mealsAmt, double visaFeeAmt, double groundTransAmt,
                                                double dailyAllowanceAmt, double entertainmentAmt, double otherAmt, double giftsAmt,
                                                double totalAmt, int totalAmtCurrencyID,
                                                string adjustedAmt, int adjustedAmtCurrencyID,
                                                int isTourCostRec, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_travel_statement01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@travelstatementid", travelStatementID);
            cmd.Parameters.AddWithValue("@tourid", tourID);

            cmd.Parameters.AddWithValue("@visitrptsummaryonefilename", visitRptSummaryOneFileName);
            if (visitRptSummaryOneBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummaryonebytes", visitRptSummaryOneBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummaryonebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@visitrptsummarytwofilename", visitRptSummaryTwoFileName);
            if (visitRptSummaryTwoBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummarytwobytes", visitRptSummaryTwoBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummarytwobytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@visitrptsummarythreefilename", visitRptSummaryThreeFileName);
            if (visitRptSummaryThreeBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummarythreebytes", visitRptSummaryThreeBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummarythreebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@airfareamt", airfareAmt);
            cmd.Parameters.AddWithValue("@telephonemobamt", telephoneMobAmt);
            cmd.Parameters.AddWithValue("@lodgingamt", lodgingAmt);
            cmd.Parameters.AddWithValue("@tipsamt", tipsAmt);
            cmd.Parameters.AddWithValue("@mealsamt", mealsAmt);
            cmd.Parameters.AddWithValue("@visafeemt", visaFeeAmt);
            cmd.Parameters.AddWithValue("@groundtransamt", groundTransAmt);
            cmd.Parameters.AddWithValue("@dailyallowanceamt", dailyAllowanceAmt);
            cmd.Parameters.AddWithValue("@entertainmentamt", entertainmentAmt);
            cmd.Parameters.AddWithValue("@otheramt", otherAmt);
            cmd.Parameters.AddWithValue("@giftsamt", giftsAmt);
            cmd.Parameters.AddWithValue("@totalamt", totalAmt);
            cmd.Parameters.AddWithValue("@totalamtcurrencyid", totalAmtCurrencyID);

            cmd.Parameters.AddWithValue("@adjustmentamt", adjustedAmt);
            cmd.Parameters.AddWithValue("@adjustmentamtcurrencyid", adjustedAmtCurrencyID);

            cmd.Parameters.AddWithValue("@istourcostrec", isTourCostRec);
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

        public int UpdateTravelStatementAmendment(int travelStatementID, int statusID,
                                                string visitRptSummaryOneFileName, byte[] visitRptSummaryOneBytes,
                                                string visitRptSummaryTwoFileName, byte[] visitRptSummaryTwoBytes,
                                                string visitRptSummaryThreeFileName, byte[] visitRptSummaryThreeBytes,
                                                double airfareAmt, double telephoneMobAmt, double lodgingAmt,
                                                double tipsAmt, double mealsAmt, double visaFeeAmt, double groundTransAmt,
                                                double dailyAllowanceAmt, double entertainmentAmt, double otherAmt, double giftsAmt,
                                                double totalAmt, int totalAmtCurrencyID,
                                                string adjustedAmt, int adjustedAmtCurrencyID,
                                                int isTourCostRec, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_travel_statement_amendment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@travelstatementid", travelStatementID);
            cmd.Parameters.AddWithValue("@statusid", statusID);

            cmd.Parameters.AddWithValue("@visitrptsummaryonefilename", visitRptSummaryOneFileName);
            if (visitRptSummaryOneBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummaryonebytes", visitRptSummaryOneBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummaryonebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@visitrptsummarytwofilename", visitRptSummaryTwoFileName);
            if (visitRptSummaryTwoBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummarytwobytes", visitRptSummaryTwoBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummarytwobytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@visitrptsummarythreefilename", visitRptSummaryThreeFileName);
            if (visitRptSummaryThreeBytes != null)
                cmd.Parameters.AddWithValue("@visitrptsummarythreebytes", visitRptSummaryThreeBytes);
            else
                cmd.Parameters.AddWithValue("@visitrptsummarythreebytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@airfareamt", airfareAmt);
            cmd.Parameters.AddWithValue("@telephonemobamt", telephoneMobAmt);
            cmd.Parameters.AddWithValue("@lodgingamt", lodgingAmt);
            cmd.Parameters.AddWithValue("@tipsamt", tipsAmt);
            cmd.Parameters.AddWithValue("@mealsamt", mealsAmt);
            cmd.Parameters.AddWithValue("@visafeemt", visaFeeAmt);
            cmd.Parameters.AddWithValue("@groundtransamt", groundTransAmt);
            cmd.Parameters.AddWithValue("@dailyallowanceamt", dailyAllowanceAmt);
            cmd.Parameters.AddWithValue("@entertainmentamt", entertainmentAmt);
            cmd.Parameters.AddWithValue("@otheramt", otherAmt);
            cmd.Parameters.AddWithValue("@giftsamt", giftsAmt);
            cmd.Parameters.AddWithValue("@totalamt", totalAmt);
            cmd.Parameters.AddWithValue("@totalamtcurrencyid", totalAmtCurrencyID);

            cmd.Parameters.AddWithValue("@adjustmentamt", adjustedAmt);
            cmd.Parameters.AddWithValue("@adjustmentamtcurrencyid", adjustedAmtCurrencyID);

            cmd.Parameters.AddWithValue("@istourcostrec", isTourCostRec);
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


        public DataSet GetTravelStatementNoInfo(int travelstatementID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_statement_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@statementid", travelstatementID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetTravelStatementStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_statement_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTravelStatementList(string startDate, string endDate, string sanctionNo, int statusID, int empRecordID, int teamMemberID, string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "sp_get_travel_statement_list01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@sanctionno", sanctionNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@teammemberid", teamMemberID);
            cmd.Parameters.AddWithValue("@teammembers", teamMembers);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

             public DataSet GetServiceTravelSummaryReport(string startDate, string endDate)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "sp_get_travel_summary_report_service_department";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@inputStartDate", startDate);
            cmd.Parameters.AddWithValue("@inputEndDate", endDate);
           
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAttachedFiles(int travelStatementID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_statement_docs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@travelstatementid", travelStatementID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetPassportFile(int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_emp_passport_file";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTravelStatementStatus(int travelStatementID, int statusID,
                                               double disallowedAmount, int disallowedAmountCurrencyID, string reasonForDisallowing,
                                               string dnNo, string amountDated, double amount, int currencyID, string remarks,
                                               int amendmentCount, string settlementType,
                                               int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_travel_statement_status01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@travelstatementid", travelStatementID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            //cmd.Parameters.AddWithValue("@actid", actID);

            cmd.Parameters.AddWithValue("@disallowedamount", disallowedAmount);
            cmd.Parameters.AddWithValue("@disallowedamountcurrencyid", disallowedAmountCurrencyID);
            cmd.Parameters.AddWithValue("@reasonfordisallowing", reasonForDisallowing);

            cmd.Parameters.AddWithValue("@dnno", dnNo);
            cmd.Parameters.AddWithValue("@amountdated", amountDated);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@amountcurrencyid", currencyID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
            cmd.Parameters.AddWithValue("@settlementtype", settlementType);

            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);
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

        public int UpdateTravelStatementStatus(int travelStatementID, int statusID,
                                               double disallowedAmount, int disallowedAmountCurrencyID, string reasonForDisallowing,
                                               string dnNo, string amountDated, double amount, int currencyID, string remarks,
                                               int amendmentCount, string settlementType,
                                               string invoiceNumber,string invoiceDate,double invoiceAmount,
                                               int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_travel_statement_status01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@travelstatementid", travelStatementID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            //cmd.Parameters.AddWithValue("@actid", actID);

            cmd.Parameters.AddWithValue("@disallowedamount", disallowedAmount);
            cmd.Parameters.AddWithValue("@disallowedamountcurrencyid", disallowedAmountCurrencyID);
            cmd.Parameters.AddWithValue("@reasonfordisallowing", reasonForDisallowing);

            cmd.Parameters.AddWithValue("@dnno", dnNo);
            cmd.Parameters.AddWithValue("@amountdated", amountDated);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@amountcurrencyid", currencyID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@amendmentcount", amendmentCount);
            cmd.Parameters.AddWithValue("@settlementtype", settlementType);

            cmd.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
            cmd.Parameters.AddWithValue("@invoiceDate", invoiceDate);
            cmd.Parameters.AddWithValue("@invoiceAmount", invoiceAmount);

            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);
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


        

        public DataSet GetRequesterInfo(int tourID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_req_details_for_mail01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@tourid", tourID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetTravellerInfo(int requestID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_req_details_for_additional_advance_mail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@requestId", requestID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetTravelRequesterInfo(int statementID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_req_details_for_mail01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@statementid", statementID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTravelStatementReport(string startDate, string endDate, string sanctionNo, int statusID, int empRecordID, int teamMemberID, string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_statement_report02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@sanctionno", sanctionNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            cmd.Parameters.AddWithValue("@teammemberid", teamMemberID);
            cmd.Parameters.AddWithValue("@teammembers", teamMembers);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourSanctionDetails(string teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_sanction_details_for_date_change";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@teammember", teamMembers);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourSanctionDetailsForAddAdv(int teamMembers)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_sanction_details_for_additional_advance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@teammember", teamMembers);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int UpdateTravelDates(int tourID, string tourSanctionNo, string startDate, string endDate, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_travel_dates";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tourid", tourID);
            cmd.Parameters.AddWithValue("@toursanctionno", tourSanctionNo);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
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

        public DataSet GetTravelStatementDetailsByStatementID(int statementID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_statement_details_by_statement_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@statementid", statementID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourSanctionNoStatusID(string tourNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_toursantionno_by_tourno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@tourno", tourNo);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourSanctionNoStatusIDByTourID(int tourID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_toursantionno_by_tourid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@tourid", tourID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAddAdvSanctionNoStatusIDByRequestID(int addAdvRequestID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_additional_advance_sanctionno_by_requestid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@requestid", addAdvRequestID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public int UpdateTourInfoMailStatus(int actID, int tourID, int chkAccVal)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            //cmd.CommandText = "sp_update_tour_info_send_mail_value_NEW";
            cmd.CommandText = "sp_update_tour_info_send_mail_value";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@tourid", tourID);
            cmd.Parameters.AddWithValue("@chkaccval", chkAccVal);
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


        public int UpdateAddAdvInfoMailStatus(int actID, int requestID, int chkAccVal)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            //cmd.CommandText = "sp_update_tour_info_send_mail_value_NEW";
            cmd.CommandText = "sp_update_add_advance_info_send_mail_value";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@requestid", requestID);
            cmd.Parameters.AddWithValue("@chkaccval", chkAccVal);
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




        public int UpdateTravelStatementMailStatus(int travelStatementID, int actID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_travel_statement_send_mail_value";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@statementid", travelStatementID);
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

        public DataSet GetTravelStatementForPDF(int statementID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_travel_statement_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@travelstatementid", statementID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourInformationForPDF(int tourID)
        {

            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_info_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@tourid", tourID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetAdditionalAdvanceInformationForPDF(int requestID)
        {

            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_request_info_for_pdf_add_advance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@requestid", requestID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTravelStatementDAReport(string startDate, string endDate, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_traval_statment_da_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet CheckForAdviceGenerated(int tourID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_check_for_advice_generated";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@tourid", tourID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //2019-12-18----

        public DataSet GetEmployeeForTSServiceReport()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_for_ts_service_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetServiceReport(string startDate, string endDate, int empRecordID, int type)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ts_service_report01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@empid", empRecordID);
            cmd.Parameters.AddWithValue("@type", type);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetServiceReportDetails(string startDate, string endDate, int empRecordID, int type)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ts_service_report_in_detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@empid", empRecordID);
            cmd.Parameters.AddWithValue("@type", type);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //2019-12-20----
        public DataSet GetServiceReportDetailsOne(string startDate, string endDate, int empRecordID, int type, string bu)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ts_service_report_in_detail01";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@empid", empRecordID);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@bu", bu);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        //23-Dec-2019--------------------------

        public DataSet GetVoucherType()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_voucher_type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        //24-Oct-2022--------------------------

        public string InsertUpdateTravelExpense(int recordID, int tourID, double airTicketAmt, double hotelAmt, double taxiAmt
                                           , double othersAmt, double totalAmt, int totalAmtCurrencyID, string remarks
                                           , string attachment1FileName, byte[] attachment1Bytes
                                           , string attachment2FileName, byte[] attachment2Bytes
                                           , int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_tour_expense";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@recordId", recordID);
            cmd.Parameters.AddWithValue("@tourId", tourID);
            cmd.Parameters.AddWithValue("@airTicketAmount", airTicketAmt);
            cmd.Parameters.AddWithValue("@hotelAmount", hotelAmt);
            cmd.Parameters.AddWithValue("@taxiAmount", taxiAmt);
            cmd.Parameters.AddWithValue("@othersAmount", othersAmt);
            cmd.Parameters.AddWithValue("@totalAmount", totalAmt);
            cmd.Parameters.AddWithValue("@currencyId", totalAmtCurrencyID);
            cmd.Parameters.AddWithValue("@remarks", remarks);

            cmd.Parameters.AddWithValue("@attachment1FileName", attachment1FileName);
            if (attachment1Bytes != null)
                cmd.Parameters.AddWithValue("@attachment1Doc", attachment1Bytes);
            else
                cmd.Parameters.AddWithValue("@attachment1Doc", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@attachment2FileName", attachment2FileName);
            if (attachment2Bytes != null)
                cmd.Parameters.AddWithValue("@attachment2Doc", attachment2Bytes);
            else
                cmd.Parameters.AddWithValue("@attachment2Doc", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@createdby", createdBy);
            cmd.Parameters.Add(rcode);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                string value = Convert.ToString(rcode.Value);
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


        public int UpdateTourExpenseMailStatus(int recordID, int actID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_tour_expense_send_mail_value";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@recordid", recordID);
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

        public DataSet GetTourExpenseMailInfo(int recordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_expense_mail_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@recordId", recordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourExpenseStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_expense_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetTourExpenseList(string startDate, string endDate, string tourExpenseNoSearch
                                        , string sanctionNoSearch, int statusID, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_expense_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@tourexpenseno", tourExpenseNoSearch);
            cmd.Parameters.AddWithValue("@sanctionno", sanctionNoSearch);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int UpdateTourExpenseStatus(int recordID, int actID, int statusID
                                         , string voucherNo, string voucherDate
                                         , string invoiceNo, string invoiceDate, string remarks, int modifiedBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_update_tour_expense_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@voucherno", voucherNo);
            cmd.Parameters.AddWithValue("@voucherdate", voucherDate);
            cmd.Parameters.AddWithValue("@invoiceno", invoiceNo);
            cmd.Parameters.AddWithValue("@invoicedate", invoiceDate);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);

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


        public DataSet GetExpenseFile(int recordID, int fileTypeID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_expense_file";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@recordid", recordID);
            cmd.Parameters.AddWithValue("@filetypeid", fileTypeID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetTourExpenseReport(string startDate, string endDate, string tourExpenseNoSearch
                                            , string sanctionNoSearch, int statusID, int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_expense_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@tourexpenseno", tourExpenseNoSearch);
            cmd.Parameters.AddWithValue("@sanctionno", sanctionNoSearch);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            cmd.Parameters.AddWithValue("@emprecordid", empRecordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int SaveAdditionalAttachments(int statementID, 
                                            string additionalAttachmentFileName, 
                                            byte[] additionalAttachmentFileBytes, 
                                            int seqNo,
                                            string additionalAttachmentRemarks, 
                                            int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_add_travel_additional_attachment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@TravelStatementId", statementID);
            cmd.Parameters.AddWithValue("@FileName", additionalAttachmentFileName);
            if (additionalAttachmentFileBytes != null)
                cmd.Parameters.AddWithValue("@FileBytes", additionalAttachmentFileBytes);
            else
                cmd.Parameters.AddWithValue("@FileBytes", System.Data.SqlTypes.SqlBinary.Null);

            cmd.Parameters.AddWithValue("@FileSeqNo", seqNo);
            cmd.Parameters.AddWithValue("@Remarks", additionalAttachmentRemarks);
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

        public DataSet GetOpenToursByCreator(int empRecordID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_tour_pending_tours_by_emp_record_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOpenAdvanceRequestByCreator(string sanctionNo)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_open_additional_adv_requests_by_emp_record_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@sanctionNo", sanctionNo);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int InsertAddAdvanceInformation(int actId, string sanctionNo , string tourNo , string custVendName, string placeOfVisit ,
           string startDate , string endDate, double advanceTaken, int advanceTakenCurrencyID,
           double additionalAdvanceRequired, int additionalAdvReqCurrencyID, 
            string remarks, int empRecordID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_additional_advance_information";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@actId", actId);
            cmd.Parameters.AddWithValue("@sanctionNo", sanctionNo);
            cmd.Parameters.AddWithValue("@tourNo", tourNo);
            cmd.Parameters.AddWithValue("@custvendname", custVendName);
            cmd.Parameters.AddWithValue("@placeofvisit", placeOfVisit);
            
            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@advancetaken", advanceTaken);
            cmd.Parameters.AddWithValue("@advanceTakenCurrencyID", advanceTakenCurrencyID);
            
            cmd.Parameters.AddWithValue("@additionalAdvanceRequired", additionalAdvanceRequired);
            cmd.Parameters.AddWithValue("@additionalAdvanceRequiredCurrencyID", additionalAdvReqCurrencyID);

            cmd.Parameters.AddWithValue("@remarks", remarks);
           cmd.Parameters.AddWithValue("@empRecordID", empRecordID);


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


        public DataSet GetTSNDetails(int employeeRecordId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_ts_no_02";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@employeeid", employeeRecordId);
            da.Fill(ds);
            if (ds != null)
            {
                return ds;
            }
            return null;

        }

        public int InsertCompleteWeeklySiteReportMaster(
          string tsNo, string startDate, string endDate, string custName, string site, string empName, string poNo, string jobNo, string remarks,
          string focType, int createdBy, string detailsXml, string approvalsXml) // <-- Parameter list updated
        {
            string conn = ConnectionString.GetCiplTMSCommonConn();
            int reportID = 0;

            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SaveCompleteWeeklySiteReportMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TOUR_SANCTION_NO", tsNo);
                    cmd.Parameters.AddWithValue("@TOUR_START_DATE", Convert.ToDateTime(startDate).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@TOUR_END_DATE", Convert.ToDateTime(endDate).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CUSTOMER_NAME", custName);
                    cmd.Parameters.AddWithValue("@END_USER_SITE", site);
                    cmd.Parameters.AddWithValue("@EMPLOYEE_NAME", empName);
                    cmd.Parameters.AddWithValue("@PO_NO", poNo);
                    cmd.Parameters.AddWithValue("@JOB_NO", jobNo);
                    cmd.Parameters.AddWithValue("@CREATED_REMARKS", remarks);
                    cmd.Parameters.AddWithValue("@MASTER_FOC_TYPE", focType); // <-- Naya parameter map kiya
                    cmd.Parameters.AddWithValue("@CREATED_BY", createdBy);
                    cmd.Parameters.AddWithValue("@DetailsXml", detailsXml);
                    cmd.Parameters.AddWithValue("@ApprovalsXml", approvalsXml);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        reportID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return reportID;
        }



        public DataSet GetMonthlySiteReport(int selectedYear, bool isMonthEnabled, int selectedMonth)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "sp_GetMonthlySiteReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@SelectedYear", selectedYear);
            cmd.Parameters.AddWithValue("@IsMonthEnabled", isMonthEnabled ? 1 : 0);
            cmd.Parameters.AddWithValue("@SelectedMonth", selectedMonth);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetServiceDeptEmployeeList()
        {
            DataSet ds = new DataSet();
            string ciplconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(ciplconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_service_dept_employees";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;

        }

        public DataTable GetSiteReportDataEngineerWise(string year, int empId)
        {
            DataTable ds = new DataTable();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();

            using (SqlConnection con = new SqlConnection(cipltmsconnectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("Get_ServiceHoursReportEngineerWise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@selectedYear", year);
                    cmd.Parameters.AddWithValue("@selectedEngineerId", empId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            if (ds != null && ds.Rows.Count > 0)
            {
                return ds;
            }

            return null;
        }


        public DataTable GetServiceHourReport(string empId, string year, string month)
        {
            DataTable ds = new DataTable();

            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Get_ServiceHoursReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@EmpId", string.IsNullOrEmpty(empId) ? (object)DBNull.Value : empId);
            cmd.Parameters.AddWithValue("@Year", string.IsNullOrEmpty(year) ? (object)DBNull.Value : year);
            cmd.Parameters.AddWithValue("@Month", string.IsNullOrEmpty(month) ? (object)DBNull.Value : month);

            da.Fill(ds);

            if (ds != null)
                return ds;

            return null;
        }


    }
}
