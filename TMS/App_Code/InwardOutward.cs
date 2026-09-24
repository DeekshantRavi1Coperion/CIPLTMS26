using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

/// <summary>
/// Summary description for InwardOutward
/// </summary>
/// 

namespace BAL
{

	public class InwardOutward
	{
		public InwardOutward()
		{
			//
			// TODO: Add constructor logic here
			//
		}


        public DataSet GetClientDetails(string jobNumForClientDetais, string custNameForClientDetais)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_client_details_outward";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@jobNumForClientDetais", jobNumForClientDetais);
            cmd.Parameters.AddWithValue("@custNameForClientDetais", custNameForClientDetais);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetInwardFiles(int inwardId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_inward_docs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@inwardid", inwardId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOutwardFiles(int inwardId)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_outward_docs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@outwardid", inwardId);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetVendorDetails(string VendorCode , string VendorName)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_vendor_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@vendorcode", VendorCode);
            cmd.Parameters.AddWithValue("@vendorname", VendorName);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataTable GetTruckDetails()
        {
            DataTable ds = new DataTable();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetTruckTypes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataTable GetContainersDetails()
        {
            DataTable ds = new DataTable();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetContainerTypes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        public DataSet GetFinalConfNoStatusIDByReqID(int reqID,int inorout)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_finalconfirmationno_by_reqid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@reqid", reqID);
            cmd.Parameters.AddWithValue("@inorout", inorout);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetInwardInformationForPDF(int reqID)
        {

            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[sp_get_inward_outward_info_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@reqid", reqID);
            cmd.Parameters.AddWithValue("@typeinwardoroutward", 0);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOutwardInformationForPDF(int reqID)
        {

            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[sp_get_inward_outward_info_for_pdf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@reqid", reqID);
            cmd.Parameters.AddWithValue("@typeinwardoroutward", 1);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }




        public DataSet GetEmployeeForInwardList()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_employee_for_inward_outward";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public DataSet GetInwardList(string startDate, string endDate, string reqNo, string jobNoSearch, int inwardCategory, int empRecordID, int createdByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_inward_data";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@reqNo", reqNo);
            cmd.Parameters.AddWithValue("@jobNoSearch", jobNoSearch);
            cmd.Parameters.AddWithValue("@inwardCategory", inwardCategory);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@createdByID", createdByID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOutwardList(string startDate, string endDate, string reqNo, string jobNoSearch, int inwardCategory, int empRecordID, int createdByID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_outward_data";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@reqNo", reqNo);
            cmd.Parameters.AddWithValue("@jobNoSearch", jobNoSearch);
            cmd.Parameters.AddWithValue("@outwardCategory", inwardCategory);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@createdByID", createdByID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }


        public int EditInwardDomestic(int reqID, int empRecordID, string jobNoEdit, string vendorNameEdit,
                                                            string vendorLocationEdit, string vendorEmailEdit,
                                                            string vendorContactEdit, string vendorLocation,
                                                            string incotermsEdit, string deliveryTermEdit,
                                                            string NoOfTrucksEdit, string dateEditDomInward,
                                                            string TypeOfConsignmentEdit,
                                                            int qtyTruck14, int qtyTruck17, int qtyTruck19, int qtyTruck22,
                                                            int qtyTruck24, int qtyTruck32, int qtyTruck40, int qtyLowBed, int qtyODC,
                                                            string packingListName, byte[] packingListNameBytes, string packingListName2
                                                            , byte[] attachment2Bytes)
        {
            
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_edit_inward_request";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@jobNoEdit", jobNoEdit);
            cmd.Parameters.AddWithValue("@vendorNameEdit", vendorNameEdit);
            cmd.Parameters.AddWithValue("@vendorLocationEdit", vendorLocationEdit);
            cmd.Parameters.AddWithValue("@vendorEmailEdit", vendorEmailEdit);
            cmd.Parameters.AddWithValue("@vendorContactEdit", vendorContactEdit);
            cmd.Parameters.AddWithValue("@vendorLocation", vendorLocation);
            cmd.Parameters.AddWithValue("@incotermsEdit", incotermsEdit);
            cmd.Parameters.AddWithValue("@deliveryTermEdit", deliveryTermEdit);
            cmd.Parameters.AddWithValue("@NoOfTrucksEdit", NoOfTrucksEdit);
            cmd.Parameters.AddWithValue("@dateEditDomInward", dateEditDomInward);
            cmd.Parameters.AddWithValue("@TypeOfConsignmentEdit", TypeOfConsignmentEdit);
            cmd.Parameters.AddWithValue("@qtyTruck14", qtyTruck14);
            cmd.Parameters.AddWithValue("@qtyTruck17", qtyTruck17);
            cmd.Parameters.AddWithValue("@qtyTruck19", qtyTruck19);
            cmd.Parameters.AddWithValue("@qtyTruck22", qtyTruck22);
            cmd.Parameters.AddWithValue("@qtyTruck24", qtyTruck24);
            cmd.Parameters.AddWithValue("@qtyTruck32", qtyTruck32);
            cmd.Parameters.AddWithValue("@qtyTruck40", qtyTruck40);
            cmd.Parameters.AddWithValue("@qtyLowBed", qtyLowBed);
            cmd.Parameters.AddWithValue("@qtyODC", qtyODC);
            cmd.Parameters.AddWithValue("@packingListName", packingListName);
            cmd.Parameters.AddWithValue("@packingListNameBytes", packingListNameBytes);
            cmd.Parameters.AddWithValue("@packingListName2", packingListName2);
            cmd.Parameters.AddWithValue("@attachment2Bytes", attachment2Bytes);
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

        public int EditOutwardDomestic(int reqID, int empRecordID, string jobNoEdit, string vendorNameEdit,
                                                           string vendorLocationEdit, string vendorEmailEdit,
                                                           string vendorContactEdit, string vendorLocation,
                                                           string incotermsEdit, string deliveryTermEdit,
                                                           string NoOfTrucksEdit, string dateEditDomInward,
                                                           string TypeOfConsignmentEdit,
                                                           int qtyTruck14, int qtyTruck17, int qtyTruck19, int qtyTruck22,
                                                           int qtyTruck24, int qtyTruck32, int qtyTruck40, int qtyLowBed, int qtyODC,
                                                           int qtyDContainer20,
                                                           int qtyDContainer40,
                                                           string packingListName, byte[] packingListNameBytes, string packingListName2
                                                           , byte[] attachment2Bytes)
        {

            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_edit_outward_request";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@jobNoEdit", jobNoEdit);
            cmd.Parameters.AddWithValue("@vendorNameEdit", vendorNameEdit);
            cmd.Parameters.AddWithValue("@vendorLocationEdit", vendorLocationEdit);
            cmd.Parameters.AddWithValue("@vendorEmailEdit", vendorEmailEdit);
            cmd.Parameters.AddWithValue("@vendorContactEdit", vendorContactEdit);
            cmd.Parameters.AddWithValue("@vendorLocation", vendorLocation);
            cmd.Parameters.AddWithValue("@incotermsEdit", incotermsEdit);
            cmd.Parameters.AddWithValue("@deliveryTermEdit", deliveryTermEdit);
            cmd.Parameters.AddWithValue("@NoOfTrucksEdit", NoOfTrucksEdit);
            cmd.Parameters.AddWithValue("@dateEditDomInward", dateEditDomInward);
            cmd.Parameters.AddWithValue("@TypeOfConsignmentEdit", TypeOfConsignmentEdit);
            cmd.Parameters.AddWithValue("@qtyTruck14", qtyTruck14);
            cmd.Parameters.AddWithValue("@qtyTruck17", qtyTruck17);
            cmd.Parameters.AddWithValue("@qtyTruck19", qtyTruck19);
            cmd.Parameters.AddWithValue("@qtyTruck22", qtyTruck22);
            cmd.Parameters.AddWithValue("@qtyTruck24", qtyTruck24);
            cmd.Parameters.AddWithValue("@qtyTruck32", qtyTruck32);
            cmd.Parameters.AddWithValue("@qtyTruck40", qtyTruck40);
            cmd.Parameters.AddWithValue("@qtyLowBed", qtyLowBed);
            cmd.Parameters.AddWithValue("@qtyODC", qtyODC);
            cmd.Parameters.AddWithValue("@qtyDContainer20", qtyDContainer20);
            cmd.Parameters.AddWithValue("@qtyDContainer40", qtyDContainer40);
            cmd.Parameters.AddWithValue("@packingListName", packingListName);
            cmd.Parameters.AddWithValue("@packingListNameBytes", packingListNameBytes);
            cmd.Parameters.AddWithValue("@packingListName2", packingListName2);
            cmd.Parameters.AddWithValue("@attachment2Bytes", attachment2Bytes);
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


        public int EditInwardInternational(int reqID, int empRecordID, string jobNoEdit, string vendorNameEdit,
                                   string vendorLocationEdit, string vendorEmailEdit, string vendorContactEdit,
                                   string vendorLocation, string incotermsEdit, string deliveryTermEdit,
                                   string NoOfTrucksIntEdit, string dateEditIntInward, string typeOfConsignmentEditInt,
                                   int qtyContainer20, int qtyContainer40, int qtyContainer40HC, int qtyContainerFr,
                                   int qtyContainerODC, string packingListName, byte[] packingListNameBytes,
                                   string packingListName2, byte[] attachment2Bytes)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };

            cmd.CommandText = "sp_edit_inward_request_international";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            // Add parameters matching function signature 
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@jobNoEdit", jobNoEdit);
            cmd.Parameters.AddWithValue("@vendorNameEdit", vendorNameEdit);
            cmd.Parameters.AddWithValue("@vendorLocationEdit", vendorLocationEdit);
            cmd.Parameters.AddWithValue("@vendorEmailEdit", vendorEmailEdit);
            cmd.Parameters.AddWithValue("@vendorContactEdit", vendorContactEdit);
            cmd.Parameters.AddWithValue("@vendorLocation", vendorLocation);
            cmd.Parameters.AddWithValue("@incotermsEdit", incotermsEdit);
            cmd.Parameters.AddWithValue("@deliveryTermEdit", deliveryTermEdit);
            cmd.Parameters.AddWithValue("@NoOfTrucksIntEdit", NoOfTrucksIntEdit);
            cmd.Parameters.AddWithValue("@dateEditIntInward", dateEditIntInward);
            cmd.Parameters.AddWithValue("@typeOfConsignmentEditInt", typeOfConsignmentEditInt);
            cmd.Parameters.AddWithValue("@qtyContainer20", qtyContainer20);
            cmd.Parameters.AddWithValue("@qtyContainer40", qtyContainer40);
            cmd.Parameters.AddWithValue("@qtyContainer40HC", qtyContainer40HC);
            cmd.Parameters.AddWithValue("@qtyContainerFr", qtyContainerFr);
            cmd.Parameters.AddWithValue("@qtyContainerODC", qtyContainerODC);
            cmd.Parameters.AddWithValue("@packingListName", packingListName);
            cmd.Parameters.AddWithValue("@packingListNameBytes", packingListNameBytes);
            cmd.Parameters.AddWithValue("@packingListName2", packingListName2);
            cmd.Parameters.AddWithValue("@attachment2Bytes", attachment2Bytes);

            cmd.Parameters.Add(rcode);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                int value = Convert.ToInt32(rcode.Value);
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


        public int EditOutwardInternational(int reqID, int empRecordID, string jobNoEdit, string vendorNameEdit,
                                   string vendorLocationEdit, string vendorEmailEdit, string vendorContactEdit,
                                   string vendorLocation, string incotermsEdit, string deliveryTermEdit,
                                   string NoOfTrucksIntEdit, string dateEditIntInward, string typeOfConsignmentEditInt,
                                   int qtyContainer20, int qtyContainer40, int qtyContainer40HC, int qtyContainerFr,
                                   int qtyContainerODC, string packingListName, byte[] packingListNameBytes,
                                   string packingListName2, byte[] attachment2Bytes)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };

            cmd.CommandText = "sp_edit_outward_request_international";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            // Add parameters matching function signature 
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@jobNoEdit", jobNoEdit);
            cmd.Parameters.AddWithValue("@vendorNameEdit", vendorNameEdit);
            cmd.Parameters.AddWithValue("@vendorLocationEdit", vendorLocationEdit);
            cmd.Parameters.AddWithValue("@vendorEmailEdit", vendorEmailEdit);
            cmd.Parameters.AddWithValue("@vendorContactEdit", vendorContactEdit);
            cmd.Parameters.AddWithValue("@vendorLocation", vendorLocation);
            cmd.Parameters.AddWithValue("@incotermsEdit", incotermsEdit);
            cmd.Parameters.AddWithValue("@deliveryTermEdit", deliveryTermEdit);
            cmd.Parameters.AddWithValue("@NoOfTrucksIntEdit", NoOfTrucksIntEdit);
            cmd.Parameters.AddWithValue("@dateEditIntInward", dateEditIntInward);
            cmd.Parameters.AddWithValue("@typeOfConsignmentEditInt", typeOfConsignmentEditInt);
            cmd.Parameters.AddWithValue("@qtyContainer20", qtyContainer20);
            cmd.Parameters.AddWithValue("@qtyContainer40", qtyContainer40);
            cmd.Parameters.AddWithValue("@qtyContainer40HC", qtyContainer40HC);
            cmd.Parameters.AddWithValue("@qtyContainerFr", qtyContainerFr);
            cmd.Parameters.AddWithValue("@qtyContainerODC", qtyContainerODC);
            cmd.Parameters.AddWithValue("@packingListName", packingListName);
            cmd.Parameters.AddWithValue("@packingListNameBytes", packingListNameBytes);
            cmd.Parameters.AddWithValue("@packingListName2", packingListName2);
            cmd.Parameters.AddWithValue("@attachment2Bytes", attachment2Bytes);

            cmd.Parameters.Add(rcode);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                int value = Convert.ToInt32(rcode.Value);
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



        public int InsertInwardOutwardInformation(int requestID, int isInternationalInward, int empRecordID, string jobNumber, string vendorName,
                                      string vendorAddress, string vendorEmail,
                                      string vendorContactDetails, string delPicLoc,
                                      string incoterms, string deliveryTerm,
                                      string noOfTruck1, int noOfTruck,string date, string typeOfConsignment,
                                      string packingListName , string subVendorInvoiceName,
                                      byte[] packingList, byte[] attachment2,
                                      string modeOfTransport, int isContainerStuffingPossible, int numberOfContainersReq,
                                      int qtyTruck14, int qtyTruck17, int qtyTruck19, int qtyTruck22, int qtyTruck24,
                                      int qtyTruck32, int qtyTruck40, int qtyLowBed, int qtyODC,
                                      int container20, int container40, int container40HC, int qtyFR,
                                      int createdBy,string lrNo, string lrDate , string truckNo ,
                                      byte[] podAttachment1Bytes, string fcrbrNo, string fcrbrDate, string containerNo,
                                      byte[] podAttachment2Bytes,string createdRemarks,string materialPickLocContactPersonDetails)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_insert_update_inward_information";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@requestID", requestID);
            cmd.Parameters.AddWithValue("@isInternationalInward", isInternationalInward);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@jobno", jobNumber);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@vendorAddress", vendorAddress);
            cmd.Parameters.AddWithValue("@vendorEmail", vendorEmail);
            cmd.Parameters.AddWithValue("@vendorContactDetails", vendorContactDetails);
            cmd.Parameters.AddWithValue("@delPicLoc", delPicLoc);
            cmd.Parameters.AddWithValue("@incoterms", incoterms);
            cmd.Parameters.AddWithValue("@deliveryTerm", deliveryTerm);
            cmd.Parameters.AddWithValue("@noOfTruck", noOfTruck);
            //cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date)); // Ensuring correct data type conversion
            cmd.Parameters.AddWithValue("@date", date); // Ensuring correct data type conversion
            cmd.Parameters.AddWithValue("@typeOfConsignment", typeOfConsignment);
            cmd.Parameters.AddWithValue("@packingListName", packingListName);
            cmd.Parameters.AddWithValue("@subVendorInvoiceName", subVendorInvoiceName);
            cmd.Parameters.AddWithValue("@packingList", packingList);
            cmd.Parameters.AddWithValue("@attachment2", attachment2);
            cmd.Parameters.AddWithValue("@modeOfTransport", modeOfTransport);
            cmd.Parameters.AddWithValue("@isContainerStuffingPossible", isContainerStuffingPossible);
            cmd.Parameters.AddWithValue("@numberOfContainersReq", numberOfContainersReq);
            cmd.Parameters.AddWithValue("@qtyTruck14", qtyTruck14);
            cmd.Parameters.AddWithValue("@qtyTruck17", qtyTruck17);
            cmd.Parameters.AddWithValue("@qtyTruck19", qtyTruck19);
            cmd.Parameters.AddWithValue("@qtyTruck22", qtyTruck22);
            cmd.Parameters.AddWithValue("@qtyTruck24", qtyTruck24);
            cmd.Parameters.AddWithValue("@qtyTruck32", qtyTruck32);
            cmd.Parameters.AddWithValue("@qtyTruck40", qtyTruck40);
            cmd.Parameters.AddWithValue("@qtyLowBed", qtyLowBed);
            cmd.Parameters.AddWithValue("@qtyODC", qtyODC);
            cmd.Parameters.AddWithValue("@container20", container20);
            cmd.Parameters.AddWithValue("@container40", container40);
            cmd.Parameters.AddWithValue("@container40HC", container40HC);
            cmd.Parameters.AddWithValue("@qtyFR", qtyFR);
            cmd.Parameters.AddWithValue("@createdBy", createdBy);
            cmd.Parameters.AddWithValue("@createdByRemarks", createdRemarks);
            cmd.Parameters.AddWithValue("@materialPickLocContactPersonDetails", materialPickLocContactPersonDetails);


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

        public int InsertOutwardInformation(int requestID, int isInternationalOutward, int empRecordID, string jobNumber, string vendorName,
                                     string vendorAddress, string vendorEmail,
                                     string vendorContactDetails, string delPicLoc,
                                     string vendorNameForPickup, string vendorAddressForPickup,
                                     string txtVendContactPickup,
                                     string incoterms, string deliveryTerm,
                                     int noOfTruck, string date, string typeOfConsignment,
                                     string packingListName , string subvendorinvoiceName,
                                     byte[] packingList, byte[] attachment2,
                                     string modeOfTransport, int isContainerStuffingPossible, int numberOfContainersReq,
                                     int qtyTruck14, int qtyTruck17, int qtyTruck19, int qtyTruck22, int qtyTruck24,
                                     int qtyTruck32, int qtyTruck40, int qtyLowBed, int qtyODC, int qtyDCont20, int qtyDCont40,
                                     int container20, int container40, int container40HC, int qtyFR,
                                     int createdBy, string lrNo, string lrDate, string truckNo,
                                     byte[] podAttachment1Bytes, string fcrbrNo, string fcrbrDate, string containerNo,
                                     byte[] podAttachment2Bytes,string createdRemarks,
                                     string otherPickupLocation, int isClientDeliveryAddressOtherThanAbove,
                                     string clientOtherDelLocDiff)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            //cmd.CommandText = "sp_insert_update_outward_information";
            //cmd.CommandText = "sp_insert_update_outward_information_02";
            cmd.CommandText = "sp_insert_update_outward_information_03";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@requestID", requestID);
            cmd.Parameters.AddWithValue("@isInternationalOutward", isInternationalOutward);
            cmd.Parameters.AddWithValue("@empRecordID", empRecordID);
            cmd.Parameters.AddWithValue("@jobno", jobNumber);
            cmd.Parameters.AddWithValue("@vendorname", vendorName);
            cmd.Parameters.AddWithValue("@vendorAddress", vendorAddress);
            cmd.Parameters.AddWithValue("@vendorEmail", vendorEmail);
            cmd.Parameters.AddWithValue("@vendorContactDetails", vendorContactDetails);
            cmd.Parameters.AddWithValue("@delPicLoc", delPicLoc);
            cmd.Parameters.AddWithValue("@vendorNameForPickup", vendorNameForPickup);
            cmd.Parameters.AddWithValue("@vendorAddressForPickup", vendorAddressForPickup);
            cmd.Parameters.AddWithValue("@VendContactPickup", txtVendContactPickup);
            cmd.Parameters.AddWithValue("@incoterms", incoterms);
            cmd.Parameters.AddWithValue("@deliveryTerm", deliveryTerm);
            cmd.Parameters.AddWithValue("@noOfTruck", noOfTruck);
            //cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date)); // Ensuring correct data type conversion
            cmd.Parameters.AddWithValue("@date", date); // Ensuring correct data type conversion
            cmd.Parameters.AddWithValue("@typeOfConsignment", typeOfConsignment);
            cmd.Parameters.AddWithValue("@packingListName", packingListName); // Ensuring correct data type conversion
            cmd.Parameters.AddWithValue("@subVendorInvoiceName", subvendorinvoiceName);
            cmd.Parameters.AddWithValue("@packingList", packingList);
            cmd.Parameters.Add("@attachment2", SqlDbType.VarBinary).Value = (object)attachment2 ?? DBNull.Value;
            cmd.Parameters.AddWithValue("@modeOfTransport", modeOfTransport);
            cmd.Parameters.AddWithValue("@isContainerStuffingPossible", isContainerStuffingPossible);
            cmd.Parameters.AddWithValue("@numberOfContainersReq", numberOfContainersReq);
            cmd.Parameters.AddWithValue("@qtyTruck14", qtyTruck14);
            cmd.Parameters.AddWithValue("@qtyTruck17", qtyTruck17);
            cmd.Parameters.AddWithValue("@qtyTruck19", qtyTruck19);
            cmd.Parameters.AddWithValue("@qtyTruck22", qtyTruck22);
            cmd.Parameters.AddWithValue("@qtyTruck24", qtyTruck24);
            cmd.Parameters.AddWithValue("@qtyTruck32", qtyTruck32);
            cmd.Parameters.AddWithValue("@qtyTruck40", qtyTruck40);
            cmd.Parameters.AddWithValue("@qtyLowBed", qtyLowBed);
            cmd.Parameters.AddWithValue("@qtyODC", qtyODC);
            cmd.Parameters.AddWithValue("@qtyDCont20", qtyDCont20);
            cmd.Parameters.AddWithValue("@qtyDCont40", qtyDCont40);
            cmd.Parameters.AddWithValue("@container20", container20);
            cmd.Parameters.AddWithValue("@container40", container40);
            cmd.Parameters.AddWithValue("@container40HC", container40HC);
            cmd.Parameters.AddWithValue("@qtyFR", qtyFR);
            cmd.Parameters.AddWithValue("@createdBy", createdBy);
            cmd.Parameters.AddWithValue("@createdByRemarks", createdRemarks);
            cmd.Parameters.AddWithValue("@otherPickupLocation", otherPickupLocation);
            cmd.Parameters.AddWithValue("@isClientDeliveryAddressOtherThanAbove", isClientDeliveryAddressOtherThanAbove);
            cmd.Parameters.AddWithValue("@clientOtherDelLocDiff", clientOtherDelLocDiff);


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



        public int UpdateInwardInfoMailStatus(int actID, int reqID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            //cmd.CommandText = "sp_update_tour_info_send_mail_value_NEW";
            cmd.CommandText = "[sp_update_inward_outward_send_mail_value]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@reqid", reqID);
           
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

        public int UpdateOutwardInfoMailStatus(int actID, int reqID)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            //cmd.CommandText = "sp_update_tour_info_send_mail_value_NEW";
            cmd.CommandText = "[sp_update_outward_send_mail_value]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@reqid", reqID);

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

        public string UpdateInwardOutwardStatusString(int inwardoutwardID, int reqStatusID,
            int actID, string remarks, int modifiedBy,
            string lrNoD,string lrDateD, string lrTruckNoD, byte[] podAttachment1,string fcrBrNoI,
            string fcrBrDateI,
            string fcrBrContainerNoI,
            byte[] txtPODAttachment2byte,
            string stringConfirmRemarks,
            string transporterName,
            string transporterEmail,
            string transporterContactNum,
            string vehiclePlacementDate
            )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbo].[sp_update_inward_outward_status_string_01]";
            /* cmd.CommandText = "sp_update_inward_outward_status_string"; *///this is responsible for tour_sanction_number
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@inwardoutwardid", inwardoutwardID);
            cmd.Parameters.AddWithValue("@inwardoutwardstatusid", reqStatusID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);

            cmd.Parameters.AddWithValue("@lrno", lrNoD);
            cmd.Parameters.AddWithValue("@lrdate", lrDateD);
            cmd.Parameters.AddWithValue("@lrtruckno", lrTruckNoD);
            cmd.Parameters.AddWithValue("@podAttachment1", podAttachment1);
            cmd.Parameters.AddWithValue("@fcrBrNoI", fcrBrNoI);
            cmd.Parameters.AddWithValue("@fcrBrDateI", fcrBrDateI);
            cmd.Parameters.AddWithValue("@fcrBrContainerNoI", fcrBrContainerNoI);
            cmd.Parameters.AddWithValue("@txtPODAttachment2byte", txtPODAttachment2byte);
            cmd.Parameters.AddWithValue("@stringConfirmRemarks", stringConfirmRemarks);
            cmd.Parameters.AddWithValue("@transporterName", transporterName);
            cmd.Parameters.AddWithValue("@transporterEmail", transporterEmail);
            cmd.Parameters.AddWithValue("@transporterContactNum", transporterContactNum);
            cmd.Parameters.AddWithValue("@vehiclePlacementDate", vehiclePlacementDate);

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
       
        public string UpdateOutwardStatusString(int outwardID, int reqStatusID,
           int actID, string remarks, int modifiedBy,
           string lrNoD, string lrDateD, string lrInvoiceNo2C, string lrlrInvoiceDate2CDateD,
           string lrTruckNoD, byte[] podAttachment1, string podAttachment1Name, string fcrBrNoI,
           string fcrBrDateI,
           string fcrBrInvoiceNo, string fcrBrInvoiceDate,
           string fcrBrContainerNoI,
           byte[] txtPODAttachment2byte,
           string podAttachment2Name,
           string stringConfirmRemarks,
           string transporterName,
           string transporterEmail,
           string transporterContactNum
           )
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            cmd.CommandText = "[dbo].[sp_update_outward_status_string_01]";
            /* cmd.CommandText = "sp_update_inward_outward_status_string"; *///this is responsible for tour_sanction_number
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@outwardid", outwardID);
            cmd.Parameters.AddWithValue("@outwardstatusid", reqStatusID);
            cmd.Parameters.AddWithValue("@actid", actID);
            cmd.Parameters.AddWithValue("@remark", remarks);
            cmd.Parameters.AddWithValue("@modifiedby", modifiedBy);

            cmd.Parameters.AddWithValue("@lrno", lrNoD);
            cmd.Parameters.AddWithValue("@lrdate", lrDateD);

            cmd.Parameters.AddWithValue("@lrInvoiceNo", lrInvoiceNo2C);
            cmd.Parameters.AddWithValue("@lrInvoiceDate", lrlrInvoiceDate2CDateD);

            cmd.Parameters.AddWithValue("@lrtruckno", lrTruckNoD);
            cmd.Parameters.AddWithValue("@podAttachment1", podAttachment1);
            cmd.Parameters.AddWithValue("@podAttachment1Name", podAttachment1Name);
            cmd.Parameters.AddWithValue("@fcrBrNoI", fcrBrNoI);
            cmd.Parameters.AddWithValue("@fcrBrDateI", fcrBrDateI);
            cmd.Parameters.AddWithValue("@fcrBrInvoiceNo", fcrBrInvoiceNo);
            cmd.Parameters.AddWithValue("@fcrBrInvoiceDate", fcrBrInvoiceDate);

            cmd.Parameters.AddWithValue("@fcrBrContainerNoI", fcrBrContainerNoI);
            cmd.Parameters.AddWithValue("@txtPODAttachment2byte", txtPODAttachment2byte);
            cmd.Parameters.AddWithValue("@podAttachment2Name", podAttachment2Name);
            cmd.Parameters.AddWithValue("@stringConfirmRemarks", stringConfirmRemarks);
            cmd.Parameters.AddWithValue("@transporterName", transporterName);
            cmd.Parameters.AddWithValue("@transporterEmail", transporterEmail);
            cmd.Parameters.AddWithValue("@transporterContactNum", transporterContactNum);
        
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

        public DataSet getLocation()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_delivery_location";
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