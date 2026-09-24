using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class Order
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


        public DataSet GetCustomerListForOrder(string customerName, string customerCode, string dbNameA35, string dbNameDLH, string dbNameSEZ, string dbNameGNU)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_get_customer_list_for_order01";
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

        public DataSet GetEmployeesForOrderReg()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_orm_get_employees_for_order";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertOrderRegistration(int orderID, string orderNo, string GSSNo, string customerName, string customerCode, string endUserName,
                                string poNo, string poDate, string hsnCode, string description, int divisionID, int businessUnitID,
                                int targetGroupID, string industryCode, int projectTypeID, int customerCharacterID,
                                int orderBookingLocationID, int projectManagerID, int projectEngineerID, int salesManagerID,
                                int salesEngineerID, string quotationNo, int estimateAttached, string paymentTerms, int creditDays,
                                int deliveryTermsID, int ld, string deliveryDate, string orderRegistrationDate, double basicValue, double basicValueINR,
                                int basicValueCurrencyId, double exchangeRate, string exchangeRateDate, double materialCost,
                                double grossMarginValue, double grossMarginPercentage, double cidEngghours,
                                double cidEngghoursRate, double cidEngghoursValue, double cwgEngghours, double cwgEngghoursRate,
                                double cwgEngghoursValue, double estimatedTravelCost, double supervision, double purchaseRatePercentage,
                                double purchaseRateValue, double warrantyCostPercentage, double warrantyCostValue, double royaltyPercentage,
                                double royaltyValue, double insurancePercentage, double insuranceValue, double fraight,
                                double commision1Percentage, double commision1Value, double commision2Percentage, double commision2Value,
                                double totalValue, string remarks, int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_orm_insert_order_registration";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@orderid", orderID);
            cmd.Parameters.AddWithValue("@orderno", orderNo);
            cmd.Parameters.AddWithValue("@gssno", GSSNo);
            cmd.Parameters.AddWithValue("@customername", customerName);
            cmd.Parameters.AddWithValue("@customercode", customerCode);
            cmd.Parameters.AddWithValue("@endusername", endUserName);
            cmd.Parameters.AddWithValue("@pono", poNo);
            cmd.Parameters.AddWithValue("@podate", poDate);
            cmd.Parameters.AddWithValue("@hsncode", hsnCode);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@divisionid", divisionID);
            cmd.Parameters.AddWithValue("@businessunitid", businessUnitID);
            cmd.Parameters.AddWithValue("@targetgroupid", targetGroupID);
            cmd.Parameters.AddWithValue("@industrycode", industryCode);
            cmd.Parameters.AddWithValue("@projecttypeid", projectTypeID);
            cmd.Parameters.AddWithValue("@customercharacterid", customerCharacterID);
            cmd.Parameters.AddWithValue("@orderbookinglocationid", orderBookingLocationID);
            cmd.Parameters.AddWithValue("@projectmanagerid", projectManagerID);
            cmd.Parameters.AddWithValue("@projectengineerid", projectEngineerID);
            cmd.Parameters.AddWithValue("@salesmanagerid", salesManagerID);
            cmd.Parameters.AddWithValue("@salesengineerid", salesEngineerID);
            cmd.Parameters.AddWithValue("@quotationno", quotationNo);
            cmd.Parameters.AddWithValue("@estimateattached", estimateAttached);
            cmd.Parameters.AddWithValue("@paymentterms", paymentTerms);
            cmd.Parameters.AddWithValue("@creditdays", creditDays);
            cmd.Parameters.AddWithValue("@deliverytermsid", deliveryTermsID);
            cmd.Parameters.AddWithValue("@ld", ld);
            cmd.Parameters.AddWithValue("@deliverydate", deliveryDate);
            cmd.Parameters.AddWithValue("@orderregistrationdate", orderRegistrationDate);
            cmd.Parameters.AddWithValue("@basicvalue", basicValue);
            cmd.Parameters.AddWithValue("@basicvaluecurrencyid", basicValueCurrencyId);
            cmd.Parameters.AddWithValue("@exchangerate", exchangeRate);
            cmd.Parameters.AddWithValue("@basicvalueinr", basicValueINR);
            cmd.Parameters.AddWithValue("@exchangeratedate", exchangeRateDate);
            cmd.Parameters.AddWithValue("@materialcost", materialCost);
            cmd.Parameters.AddWithValue("@grossmarginvalue", grossMarginValue);
            cmd.Parameters.AddWithValue("@grossmarginpercentage", grossMarginPercentage);
            cmd.Parameters.AddWithValue("@cidengghours", cidEngghours);
            cmd.Parameters.AddWithValue("@cidengghoursrate", cidEngghoursRate);
            cmd.Parameters.AddWithValue("@cidengghoursvalue", cidEngghoursValue);
            cmd.Parameters.AddWithValue("@cwgengghours", cwgEngghours);
            cmd.Parameters.AddWithValue("@cwgengghoursrate", cwgEngghoursRate);
            cmd.Parameters.AddWithValue("@cwgengghoursvalue", cwgEngghoursValue);
            cmd.Parameters.AddWithValue("@estimatedtravelcost", estimatedTravelCost);
            cmd.Parameters.AddWithValue("@supervision", supervision);
            cmd.Parameters.AddWithValue("@purchaseratepercentage", purchaseRatePercentage);
            cmd.Parameters.AddWithValue("@purchaseratevalue", purchaseRateValue);
            cmd.Parameters.AddWithValue("@warrantycostpercentage", warrantyCostPercentage);
            cmd.Parameters.AddWithValue("@warrantycostvalue", warrantyCostValue);
            cmd.Parameters.AddWithValue("@royaltypercentage", royaltyPercentage);
            cmd.Parameters.AddWithValue("@royaltyvalue", royaltyValue);
            cmd.Parameters.AddWithValue("@insurancepercentage", insurancePercentage);
            cmd.Parameters.AddWithValue("@insurancevalue", insuranceValue);
            cmd.Parameters.AddWithValue("@fraight", fraight);
            cmd.Parameters.AddWithValue("@commision1percentage", commision1Percentage);
            cmd.Parameters.AddWithValue("@commision1value", commision1Value);
            cmd.Parameters.AddWithValue("@commision2percentage", commision2Percentage);
            cmd.Parameters.AddWithValue("@commision2value", commision2Value);

            cmd.Parameters.AddWithValue("@totalvalue", totalValue);

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



        public DataSet GetOrderList(string startDate, string endDate, string orderNo, int statusID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_orm_get_order_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            cmd.Parameters.AddWithValue("@startdate", startDate);
            cmd.Parameters.AddWithValue("@enddate", endDate);
            cmd.Parameters.AddWithValue("@orderno", orderNo);
            cmd.Parameters.AddWithValue("@statusid", statusID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public DataSet GetOrderReviseList(int orderID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_orm_get_revised_order_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            cmd.Parameters.AddWithValue("@orderid", orderID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }
        
        public DataSet GetOrderStatus()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_orm_get_order_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

        public int InsertNewRevision(int orderID, double basicValue, double basicValueINR,
                                int basicValueCurrencyId, double exchangeRate, string exchangeRateDate, double materialCost,
                                double grossMarginValue, double grossMarginPercentage, double cidEngghours,
                                double cidEngghoursRate, double cidEngghoursValue, double cwgEngghours, double cwgEngghoursRate,
                                double cwgEngghoursValue, double estimatedTravelCost, double supervision, double purchaseRatePercentage,
                                double purchaseRateValue, double warrantyCostPercentage, double warrantyCostValue, double royaltyPercentage,
                                double royaltyValue, double insurancePercentage, double insuranceValue, double fraight,
                                double commision1Percentage, double commision1Value, double commision2Percentage, double commision2Value,
                                double totalValue,
                                double revisionValue,
                                int revisionValueCurrencyId,
                                double revisionExchangeRate,
                                double revisionValueINR,            
                                int createdBy)
        {
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            SqlParameter rcode = new SqlParameter("@rcode", SqlDbType.Int, 11) { Direction = ParameterDirection.Output };
            cmd.CommandText = "sp_orm_insert_order_revision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@orderid", orderID);

            cmd.Parameters.AddWithValue("@basicvalue", basicValue);
            cmd.Parameters.AddWithValue("@basicvaluecurrencyid", basicValueCurrencyId);
            cmd.Parameters.AddWithValue("@exchangerate", exchangeRate);
            cmd.Parameters.AddWithValue("@basicvalueinr", basicValueINR);
            cmd.Parameters.AddWithValue("@exchangeratedate", exchangeRateDate);
            cmd.Parameters.AddWithValue("@materialcost", materialCost);
            cmd.Parameters.AddWithValue("@grossmarginvalue", grossMarginValue);
            cmd.Parameters.AddWithValue("@grossmarginpercentage", grossMarginPercentage);
            cmd.Parameters.AddWithValue("@cidengghours", cidEngghours);
            cmd.Parameters.AddWithValue("@cidengghoursrate", cidEngghoursRate);
            cmd.Parameters.AddWithValue("@cidengghoursvalue", cidEngghoursValue);
            cmd.Parameters.AddWithValue("@cwgengghours", cwgEngghours);
            cmd.Parameters.AddWithValue("@cwgengghoursrate", cwgEngghoursRate);
            cmd.Parameters.AddWithValue("@cwgengghoursvalue", cwgEngghoursValue);
            cmd.Parameters.AddWithValue("@estimatedtravelcost", estimatedTravelCost);
            cmd.Parameters.AddWithValue("@supervision", supervision);
            cmd.Parameters.AddWithValue("@purchaseratepercentage", purchaseRatePercentage);
            cmd.Parameters.AddWithValue("@purchaseratevalue", purchaseRateValue);
            cmd.Parameters.AddWithValue("@warrantycostpercentage", warrantyCostPercentage);
            cmd.Parameters.AddWithValue("@warrantycostvalue", warrantyCostValue);
            cmd.Parameters.AddWithValue("@royaltypercentage", royaltyPercentage);
            cmd.Parameters.AddWithValue("@royaltyvalue", royaltyValue);
            cmd.Parameters.AddWithValue("@insurancepercentage", insurancePercentage);
            cmd.Parameters.AddWithValue("@insurancevalue", insuranceValue);
            cmd.Parameters.AddWithValue("@fraight", fraight);
            cmd.Parameters.AddWithValue("@commision1percentage", commision1Percentage);
            cmd.Parameters.AddWithValue("@commision1value", commision1Value);
            cmd.Parameters.AddWithValue("@commision2percentage", commision2Percentage);
            cmd.Parameters.AddWithValue("@commision2value", commision2Value);

            cmd.Parameters.AddWithValue("@totalvalue", totalValue);

            cmd.Parameters.AddWithValue("@revisionvalue", revisionValue);
            cmd.Parameters.AddWithValue("@revisionvaluecurrencyid", revisionValueCurrencyId);
            cmd.Parameters.AddWithValue("@revisionexchangerate", revisionExchangeRate);
            cmd.Parameters.AddWithValue("@revisionvalueinr", revisionValueINR);

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




        public DataSet GetOrderDetailForPDF(int orderID)
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_orm_get_order_detail_for_pdf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            cmd.Parameters.AddWithValue("@orderid", orderID);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }

    }
}
