using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

using System.Text;
//using System.Net.Mime;
//using iTextSharp.tool.xml.pipeline.css;
//using iTextSharp.tool.xml;
//using iTextSharp.tool.xml.pipeline.html;
//using iTextSharp.tool.xml.pipeline.end;
//using iTextSharp.tool.xml.parser;
//using System.Xml;
//using iTextSharp.tool.xml.css;
using System.Data;

  public class InwardOutwardHTML
    {
        string inwardNo = string.Empty;
        string outwardNo = string.Empty;
        string reqNumber = string.Empty;
        string finalConfirmationNo = string.Empty;
        string emplyoeeName = string.Empty;
        string employeeID = string.Empty;
        string nameOfCustomerVendor = string.Empty;
        string inwardStatusName = string.Empty;
        string inwardType = string.Empty;
        string jobNo = string.Empty;
        string vendorLocation = string.Empty;
        string vendorEmailAddress = string.Empty;
        string delPickUpLocation = string.Empty;
        string modeOfTransport = string.Empty;
        string incoterms = string.Empty;
        string deliveryTerm = string.Empty;
        string noOfTrucks = string.Empty;
        string noOfContainers = string.Empty;
        string readinessDate = string.Empty;
        string typeOfConsignment = string.Empty;
        int createdByID = 0;
        string createdRemarks = string.Empty;
        string createdBy = string.Empty;
        string createdOn = string.Empty;
        int hodApprovedByID = 0;
        string hodApprovedRemarks = string.Empty;
        string hodApprovedBy = string.Empty;
        string hodApprovedOn = string.Empty;
        int finalApprovedByID = 0;
        string finalApprovedRemarks = string.Empty;
        string finalApprovedBy = string.Empty;
        string finalApprovedOn = string.Empty;
        int deletedByID = 0;
        string deletedRemarks = string.Empty;
        string deletedBy = string.Empty;
        string deletedOn = string.Empty;
        int cancelledByID = 0;
        string cancelledRemarks = string.Empty;
        string cancelledBy = string.Empty;
        string cancelledOn = string.Empty;
        int acknowledgedByID = 0;
        string acknowledgedBy = string.Empty;
        string acknowledgedOn = string.Empty;
        string acknowledgedByRemarks = string.Empty;
        int closedByID = 0;
        string closedBy = string.Empty;
        string closedByRemarks = string.Empty;
        string closedOn = string.Empty;
        string requestId = string.Empty;
        int inwardStatusID = 0;
        int outwardStatusID = 0;
        int teamleaderID = 0;

        string transporterName = string.Empty;
        string transporterContactNumber = string.Empty;
        string transporterEmail = string.Empty;

        string fcrBl = string.Empty;
        string fcrBlDate = string.Empty;
        string containerNo = string.Empty;
        string lrNo = string.Empty; //domestic
        string lrDate = string.Empty;
        string truckNo = string.Empty;

        string concPersonCDAndPickupLoc = string.Empty;

        int truck14 = 0;
        int truck17 = 0;
        int truck19 = 0;
        int truck22 = 0;
        int truck24 = 0;
        int truck32 = 0;
        int truck40 = 0;
        int truckLowBed = 0;
        int truckOdc = 0;
        int Dcont20 = 0;
        int Dcont40 = 0;

        int cont20 = 0;
        int cont40 = 0;
        int cont40HC = 0;
        int contFR = 0;
        int contFRODC = 0;

    public string GetHtmlForPDF(DataTable dtInwardDetail)
        {
            try
            {
                inwardNo = string.Empty;
                finalConfirmationNo = string.Empty;
                emplyoeeName = string.Empty;
                employeeID = string.Empty;
                nameOfCustomerVendor = string.Empty;
                createdByID = 0;
                createdRemarks = string.Empty;
                createdBy = string.Empty;
                createdOn = string.Empty;
                hodApprovedByID = 0;
                hodApprovedRemarks = string.Empty;
                hodApprovedBy = string.Empty;
                hodApprovedOn = string.Empty;
                finalApprovedByID = 0;
                finalApprovedRemarks = string.Empty;
                finalApprovedBy = string.Empty;
                finalApprovedOn = string.Empty;
                deletedByID = 0;
                deletedRemarks = string.Empty;
                deletedBy = string.Empty;
                deletedOn = string.Empty;
                cancelledByID = 0;
                cancelledRemarks = string.Empty;
                cancelledBy = string.Empty;
                cancelledOn = string.Empty;
                acknowledgedByID = 0;
                acknowledgedBy = string.Empty;
                acknowledgedOn = string.Empty;
                acknowledgedByRemarks = string.Empty;
                closedByID = 0;
                closedBy = string.Empty;
                closedByRemarks = string.Empty;
                closedOn = string.Empty;
                int teamleaderID = 0;
                int inwardStatusID = 0;
                inwardStatusName = string.Empty;
                inwardType = string.Empty;
                jobNo = string.Empty;
                vendorLocation = string.Empty;
                vendorEmailAddress = string.Empty;
                delPickUpLocation = string.Empty;
                modeOfTransport = string.Empty;
                incoterms = string.Empty;
                deliveryTerm = string.Empty;
                noOfTrucks = string.Empty;
                noOfContainers = string.Empty;
                readinessDate = string.Empty;
                typeOfConsignment = string.Empty;
                concPersonCDAndPickupLoc = string.Empty;

                truck14 = 0;
                truck17 = 0;
                truck19 = 0;
                truck22 = 0;
                truck24 = 0;
                truck32 = 0;
                truck40 = 0;
                truckLowBed = 0;
                truckOdc = 0;

                cont20 = 0;
                cont40 = 0;
                cont40HC = 0;
                contFR = 0;
                contFRODC = 0;

            string htmlText = string.Empty;
                htmlText = string.Empty;
                StringBuilder sb = new StringBuilder();

                if (dtInwardDetail.Rows.Count > 0)
                {
                    DataRow dr = dtInwardDetail.Rows[0];

                    if (dr["FINAL_CONFIRMATION_NUMBER"] != DBNull.Value)
                        finalConfirmationNo = Convert.ToString(dr["FINAL_CONFIRMATION_NUMBER"]);

                    if (dr["INWARD_NO"] != DBNull.Value)
                        inwardNo = Convert.ToString(dr["INWARD_NO"]);


                if (dr["CREATED_BY_NAME"] != DBNull.Value)
                        emplyoeeName = Convert.ToString(dr["CREATED_BY_NAME"]);

                    if (dr["CREATED_BY"] != DBNull.Value)
                        employeeID = Convert.ToString(dr["CREATED_BY"]);


                    if (dr["VENDOR_NAME"] != DBNull.Value)
                    nameOfCustomerVendor = Convert.ToString(dr["VENDOR_NAME"]);

                if (dr["STATUS_ID"] != DBNull.Value)
                    inwardStatusID = Convert.ToInt32(dr["STATUS_ID"]);

                if (dr["USER_INPUT_PICKLOC"] != DBNull.Value)
                    concPersonCDAndPickupLoc = Convert.ToString(dr["USER_INPUT_PICKLOC"]);
                

                if (dr["CREATED_BY"] != DBNull.Value)
                        createdByID = Convert.ToInt32(dr["CREATED_BY"]);

                    if (dr["CREATED_REMARKS"] != DBNull.Value)
                        createdRemarks = Convert.ToString(dr["CREATED_REMARKS"]);

                    if (dr["CREATED_BY_NAME"] != DBNull.Value)
                        createdBy = Convert.ToString(dr["CREATED_BY_NAME"]);

                    if (dr["CREATED_ON"] != DBNull.Value)
                        createdOn = Convert.ToString(dr["CREATED_ON"]);


                    if (dr["APPROVED_BY"] != DBNull.Value)
                        hodApprovedByID = Convert.ToInt32(dr["APPROVED_BY"]);

                    if (dr["APPROVED_REMARKS"] != DBNull.Value)
                        hodApprovedRemarks = Convert.ToString(dr["APPROVED_REMARKS"]);

                    if (dr["APPROVED_BY_NAME"] != DBNull.Value)
                        hodApprovedBy = Convert.ToString(dr["APPROVED_BY_NAME"]);

                    if (dr["APPROVED_ON"] != DBNull.Value)
                        hodApprovedOn = Convert.ToString(dr["APPROVED_ON"]);

                    if (dr["FINAL_APPROVED_BY"] != DBNull.Value)
                        finalApprovedByID = Convert.ToInt32(dr["FINAL_APPROVED_BY"]);

                    if (dr["FINAL_APPROVED_REMARKS"] != DBNull.Value)
                        finalApprovedRemarks = Convert.ToString(dr["FINAL_APPROVED_REMARKS"]);

                    if (dr["FINAL_APPROVED_BY_NAME"] != DBNull.Value)
                        finalApprovedBy = Convert.ToString(dr["FINAL_APPROVED_BY_NAME"]);

                    if (dr["FINAL_APPROVED_ON"] != DBNull.Value)
                        finalApprovedOn = Convert.ToString(dr["FINAL_APPROVED_ON"]);
                    //////////////////


                    if (dr["DELETED_BY"] != DBNull.Value)
                        deletedByID = Convert.ToInt32(dr["DELETED_BY"]);

                    if (dr["DELETED_REMARKS"] != DBNull.Value)
                        deletedRemarks = Convert.ToString(dr["DELETED_REMARKS"]);

                    if (dr["DELETED_BY_NAME"] != DBNull.Value)
                        deletedBy = Convert.ToString(dr["DELETED_BY_NAME"]);

                    if (dr["DELETED_ON"] != DBNull.Value)
                        deletedOn = Convert.ToString(dr["DELETED_ON"]);


                    if (dr["CANCELLED_BY"] != DBNull.Value)
                        cancelledByID = Convert.ToInt32(dr["CANCELLED_BY"]);

                    if (dr["CANCELLED_REMARKS"] != DBNull.Value)
                        cancelledRemarks = Convert.ToString(dr["CANCELLED_REMARKS"]);

                    if (dr["CANCELLED_BY_NAME"] != DBNull.Value)
                        cancelledBy = Convert.ToString(dr["CANCELLED_BY_NAME"]);

                    if (dr["CANCELLED_ON"] != DBNull.Value)
                        cancelledOn = Convert.ToString(dr["CANCELLED_ON"]);


                if (dr["ACKNOWLEDGED_BY"] != DBNull.Value)
                    acknowledgedByID = Convert.ToInt32(dr["ACKNOWLEDGED_BY"]);

                if (dr["ACKNOWLEDGED_BY_NAME"] != DBNull.Value)
                    acknowledgedBy = Convert.ToString(dr["ACKNOWLEDGED_BY_NAME"]);

                if (dr["ACKNOWLEDGED_ON"] != DBNull.Value)
                    acknowledgedOn = Convert.ToString(dr["ACKNOWLEDGED_ON"]);

                if (dr["ACKNOWLEDGED_REMARKS"] != DBNull.Value)
                    acknowledgedByRemarks = Convert.ToString(dr["ACKNOWLEDGED_REMARKS"]);


                if (dr["CLOSED_BY"] != DBNull.Value)
                    closedByID = Convert.ToInt32(dr["CLOSED_BY"]);

                if (dr["CLOSED_BY_NAME"] != DBNull.Value)
                    closedBy = Convert.ToString(dr["CLOSED_BY_NAME"]);

                if (dr["CLOSED_ON"] != DBNull.Value)
                    closedOn = Convert.ToString(dr["CLOSED_ON"]);

                if (dr["CLOSED_REMARKS"] != DBNull.Value)
                    closedByRemarks = Convert.ToString(dr["CLOSED_REMARKS"]);

                if (dr["TEAMLEADER_ID"] != DBNull.Value)
                        teamleaderID = Convert.ToInt32(dr["TEAMLEADER_ID"]);

                    if (dr["STATUS_NAME"] != DBNull.Value)
                    inwardStatusName = Convert.ToString(dr["STATUS_NAME"]);

                     if (dr["TYPE_OF_INWARD"] != DBNull.Value)
                    inwardType = Convert.ToString(dr["TYPE_OF_INWARD"]);

                    if (dr["JOB_NO"] != DBNull.Value)
                    jobNo = Convert.ToString(dr["JOB_NO"]);

                    if (dr["VENDOR_LOCATION"] != DBNull.Value)
                    vendorLocation = Convert.ToString(dr["VENDOR_LOCATION"]);

                    if (dr["VENDOR_EMAIL"] != DBNull.Value)
                    vendorEmailAddress = Convert.ToString(dr["VENDOR_EMAIL"]);

                    if (dr["DELIVERY_PICKUP_LOCATION"] != DBNull.Value)
                    delPickUpLocation = Convert.ToString(dr["DELIVERY_PICKUP_LOCATION"]);

                    if (dr["INTERNATIONAL_MODE_OF_TRANSPORT"] != DBNull.Value)
                    modeOfTransport = Convert.ToString(dr["INTERNATIONAL_MODE_OF_TRANSPORT"]);

                    if (dr["INCOTERMS"] != DBNull.Value)
                    incoterms = Convert.ToString(dr["INCOTERMS"]);

                    if (dr["DELIVERY_TERM"] != DBNull.Value)
                    deliveryTerm = Convert.ToString(dr["DELIVERY_TERM"]);

                    if (dr["NO_OF_TRUCKS"] != DBNull.Value)
                    noOfTrucks = Convert.ToString(dr["NO_OF_TRUCKS"]);

                    if (dr["NO_OF_CONTAINERS"] != DBNull.Value)
                    noOfContainers = Convert.ToString(dr["NO_OF_CONTAINERS"]);

                    if (dr["DATE_OF_PICKUP"] != DBNull.Value)
                    readinessDate = Convert.ToString(dr["DATE_OF_PICKUP"]);

                    if (dr["TYPE_OF_CONSIGNMENT"] != DBNull.Value)
                    typeOfConsignment = Convert.ToString(dr["TYPE_OF_CONSIGNMENT"]);


                if (dr["LR_NO"] != DBNull.Value)
                    lrNo = Convert.ToString(dr["LR_NO"]);

                if (dr["LR_DATE"] != DBNull.Value)
                    lrDate = Convert.ToString(dr["LR_DATE"]);

                if (dr["TRUCK_NO"] != DBNull.Value)
                    truckNo = Convert.ToString(dr["TRUCK_NO"]);

                if (dr["FCR_BL"] != DBNull.Value)
                    fcrBl = Convert.ToString(dr["FCR_BL"]);

                if (dr["FCR_BL_DATE"] != DBNull.Value)
                    fcrBlDate = Convert.ToString(dr["FCR_BL_DATE"]);

                if (dr["CONTAINER_NO"] != DBNull.Value)
                    containerNo = Convert.ToString(dr["CONTAINER_NO"]);


                if (dr["TRANSPORTER_NAME"] != DBNull.Value)
                    transporterName = Convert.ToString(dr["TRANSPORTER_NAME"]);

                if (dr["TRANSPORTER_CONTACT_NUMBER"] != DBNull.Value)
                    transporterContactNumber = Convert.ToString(dr["TRANSPORTER_CONTACT_NUMBER"]);

                if (dr["TRANSPORTER_EMAIL"] != DBNull.Value)
                    transporterEmail = Convert.ToString(dr["TRANSPORTER_EMAIL"]);

                //truck14 = 0;
                //truck17 = 0;
                //truck19 = 0;
                //truck22 = 0;
                //truck24 = 0;
                //truck32 = 0;
                //truck40 = 0;
                //truckLowBed = 0;
                //truckOdc = 0;

                //cont20 = 0;
                //cont40 = 0;
                //cont40HC = 0;
                //contFR = 0;
                //contFRODC = 0;

                if (dr["QTY_OF_TRUCK_14"] != DBNull.Value)
                    truck14 = Convert.ToInt32(dr["QTY_OF_TRUCK_14"]);

                if (dr["QTY_OF_TRUCK_17"] != DBNull.Value)
                    truck17 = Convert.ToInt32(dr["QTY_OF_TRUCK_17"]);

                if (dr["QTY_OF_TRUCK_19"] != DBNull.Value)
                    truck19 = Convert.ToInt32(dr["QTY_OF_TRUCK_19"]);

                if (dr["QTY_OF_TRUCK_22"] != DBNull.Value)
                    truck22 = Convert.ToInt32(dr["QTY_OF_TRUCK_22"]);

                if (dr["QTY_OF_TRUCK_24"] != DBNull.Value)
                    truck24 = Convert.ToInt32(dr["QTY_OF_TRUCK_24"]);

                if (dr["QTY_OF_TRUCK_32"] != DBNull.Value)
                    truck32 = Convert.ToInt32(dr["QTY_OF_TRUCK_32"]);


                if (dr["QTY_OF_TRUCK_40"] != DBNull.Value)
                    truck40 = Convert.ToInt32(dr["QTY_OF_TRUCK_40"]);

                if (dr["QTY_OF_LOW_BED"] != DBNull.Value)
                    truckLowBed = Convert.ToInt32(dr["QTY_OF_LOW_BED"]);


                if (dr["QTY_OF_ODC_TRUCK"] != DBNull.Value)
                    truckOdc = Convert.ToInt32(dr["QTY_OF_ODC_TRUCK"]);


                if (dr["QTY_OF_CONTAINERS_20"] != DBNull.Value)
                    cont20 = Convert.ToInt32(dr["QTY_OF_CONTAINERS_20"]);

                if (dr["QTY_OF_CONTAINERS_40"] != DBNull.Value)
                    cont40 = Convert.ToInt32(dr["QTY_OF_CONTAINERS_40"]);


                if (dr["QTY_OF_CONTAINERS_40HC"] != DBNull.Value)
                    cont40HC = Convert.ToInt32(dr["QTY_OF_CONTAINERS_40HC"]);

                if (dr["QTY_OF_ODC_CONTAINER"] != DBNull.Value)
                    contFR = Convert.ToInt32(dr["QTY_OF_ODC_CONTAINER"]);


                if (dr["QTY_OF_FR"] != DBNull.Value)
                    contFRODC = Convert.ToInt32(dr["QTY_OF_FR"]);



                    sb.Append("<h2 class='headerStyle'>Inward Information</h2>\n");
                    sb.Append("<hr />\n");
                    sb.Append("<table class='tblheader'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Inward No.:</td>\n");
                    sb.Append("<td class='td2header'>{#inwardNo#}</td>\n");
                    sb.Append("<td class='td1header'>Inward Type.:</td>\n");
                    sb.Append("<td class='td2header'>{#inwardType#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Job No.:</td>\n");
                    sb.Append("<td class='td2header'>{#jobNo#}</td>\n");
                    sb.Append("<td class='td1header'>Final Confirmation No.:</td>\n");
                    sb.Append("<td class='td2header'>{#finalConfirmationNo#}</td>\n");
                    
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Requester Name:</td>\n");
                    sb.Append("<td class='td2header'>{#emplyoeeName#}</td>\n");
                    sb.Append("<td class='td1header'>Type Of Consignment:</td>\n");
                    sb.Append("<td class='td2header'>{#typeOfConsignment#}</td>\n");

                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Name Of Customer/Vendor:</td>\n");
                    sb.Append("<td class='td2header'>{#nameOfCustomerVendor#}</td>\n");
                    sb.Append("<td class='td1header'>Vendor Location:</td>\n");
                    sb.Append("<td class='td2header'>{#vendorLocation#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Pickup Location & Concerned Person's contact details:</td>\n");
                    sb.Append("<td class='td2header'>{#concPersonCDAndPickupLoc#}</td>\n");
                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Vendor Email:</td>\n");
                    sb.Append("<td class='td2header'>{#vendorEmailAddress#}</td>\n");
                    sb.Append("<td class='td1header'>Delivery Location:</td>\n");
                    sb.Append("<td class='td2header'>{#delPickUpLocation#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Mode Of Transport:</td>\n");
                    sb.Append("<td class='td2header'>{#modeOfTransport#}</td>\n");
                    sb.Append("<td class='td1header'>Incoterms:</td>\n");
                    sb.Append("<td class='td2header'>{#incoterms#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Delivery Term:</td>\n");
                    sb.Append("<td class='td2header'>{#deliveryTerm#}</td>\n");
                    sb.Append("<td class='td1header'>No Of Trucks:</td>\n");
                    sb.Append("<td class='td2header'>{#noOfTrucks#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>No Of Containers:</td>\n");
                    sb.Append("<td class='td2header'>{#noOfContainers#}</td>\n");
                    sb.Append("<td class='td1header'>Readiness Date:</td>\n");
                    sb.Append("<td class='td2header'>{#readinessDate#}</td>\n");
                    sb.Append("</tr>\n");

                    if (truck14 > 0 || truck17 > 0  || truck19 > 0)
                    {
                        sb.Append("<tr>\n");
                        if (truck14 > 0)
                        {
                            sb.Append("<td class='td1header'>truck14:</td>\n");
                            sb.Append("<td class='td2header'>{#truck14#}</td>\n");
                        }
                        if (truck17 > 0)
                        {
                            sb.Append("<td class='td1header'>truck17:</td>\n");
                            sb.Append("<td class='td2header'>{#truck17#}</td>\n");
                        }
                        if (truck19 > 0)
                        {
                            sb.Append("<td class='td1header'>truck19:</td>\n");
                            sb.Append("<td class='td2header'>{#truck19#}</td>\n");
                        }
                        sb.Append("</tr>\n");
                    }

                if (truck22 > 0 || truck24 > 0 || truck32 > 0)
                {
                    sb.Append("<tr>\n");
                    if (truck22 > 0)
                    {
                        sb.Append("<td class='td1header'>truck22:</td>\n");
                        sb.Append("<td class='td2header'>{#truck22#}</td>\n");
                    }
                    if (truck24 > 0)
                    {
                        sb.Append("<td class='td1header'>truck24:</td>\n");
                        sb.Append("<td class='td2header'>{#truck24#}</td>\n");
                    }
                    if (truck32 > 0)
                    {
                        sb.Append("<td class='td1header'>truck32:</td>\n");
                        sb.Append("<td class='td2header'>{#truck32#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }


                if (truck40 > 0 || truckLowBed > 0 || truckOdc > 0)
                {
                    sb.Append("<tr>\n");
                    if (truck40 > 0)
                    {
                        sb.Append("<td class='td1header'>truck40:</td>\n");
                        sb.Append("<td class='td2header'>{#truck40#}</td>\n");
                    }
                    if (truckLowBed > 0)
                    {
                        sb.Append("<td class='td1header'>truckLowBed:</td>\n");
                        sb.Append("<td class='td2header'>{#truckLowBed#}</td>\n");
                    }
                    if (truckOdc > 0)
                    {
                        sb.Append("<td class='td1header'>truckOdc:</td>\n");
                        sb.Append("<td class='td2header'>{#truckOdc#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }

                if (cont20 > 0 || cont40 > 0 || cont40HC > 0)
                {
                    sb.Append("<tr>\n");
                    if (cont20 > 0)
                    {
                        sb.Append("<td class='td1header'>cont20:</td>\n");
                        sb.Append("<td class='td2header'>{#cont20#}</td>\n");
                    }
                    if (cont40 > 0)
                    {
                        sb.Append("<td class='td1header'>cont40:</td>\n");
                        sb.Append("<td class='td2header'>{#cont40#}</td>\n");
                    }
                    if (cont40HC > 0)
                    {
                        sb.Append("<td class='td1header'>cont40HC:</td>\n");
                        sb.Append("<td class='td2header'>{#cont40HC#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }

                if (contFR > 0 || contFRODC > 0)
                {
                    sb.Append("<tr>\n");
                    if (contFR > 0)
                    {
                        sb.Append("<td class='td1header'>contFR:</td>\n");
                        sb.Append("<td class='td2header'>{#contFR#}</td>\n");
                    }
                    if (contFRODC > 0)
                    {
                        sb.Append("<td class='td1header'>contFRODC:</td>\n");
                        sb.Append("<td class='td2header'>{#contFRODC#}</td>\n");
                    }
                    
                    sb.Append("</tr>\n");
                }



                if (inwardStatusID == 5 || inwardStatusID == 7)
                   {
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Transporter Name:</td>\n");
                    sb.Append("<td class='td2header'>{#transporterName#}</td>\n");
                    sb.Append("<td class='td1header'>Transporter Contact Details:</td>\n");
                    sb.Append("<td class='td2header'>{#transporterContactNumber#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Transaporter Email:</td>\n");
                    sb.Append("<td class='td2header'>{#transporterEmail#}</td>\n");
                    sb.Append("</tr>\n");

                    }
                   if(inwardStatusID == 7)
                    if (inwardType == "INTERNATIONAL")
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>FCR/BL:</td>\n");
                        sb.Append("<td class='td2header'>{#fcrBl#}</td>\n");
                        sb.Append("<td class='td1header'>FCR/BL Date:</td>\n");
                        sb.Append("<td class='td2header'>{#fcrBlDate#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>Container No:</td>\n");
                        sb.Append("<td class='td2header'>{#containerNo#}</td>\n");
                        //sb.Append("<td class='td1header'>Vehicle PLacement Date:</td>\n");
                        //sb.Append("<td class='td2header'>{#transporterEmail#}</td>\n");
                        sb.Append("</tr>\n");

                    }
                    else
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>LR No.:</td>\n");
                        sb.Append("<td class='td2header'>{#lrNo#}</td>\n");
                        sb.Append("<td class='td1header'>LR Date:</td>\n");
                        sb.Append("<td class='td2header'>{#lrDate#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>Truck No:</td>\n");
                        sb.Append("<td class='td2header'>{#truckNo#}</td>\n");
                        //sb.Append("<td class='td1header'>Vehicle PLacement Date:</td>\n");
                        //sb.Append("<td class='td2header'>{#transporterEmail#}</td>\n");
                        sb.Append("</tr>\n");
                    }

            }


            //sb.Append("<tr>\n");

            //sb.Append("</tr>\n");

            sb.Append("</table>\n");
                    sb.Append("<hr />\n");

                    sb.Replace("{#inwardNo#}", inwardNo);
                    sb.Replace("{#inwardType#}", inwardType);
                    sb.Replace("{#jobNo#}", jobNo);
                    sb.Replace("{#finalConfirmationNo#}", finalConfirmationNo);
                    sb.Replace("{#vendorLocation#}", vendorLocation);
                    sb.Replace("{#emplyoeeName#}", emplyoeeName);
                    //sb.Replace("{#employeeID#}", employeeID);
                    sb.Replace("{#nameOfCustomerVendor#}", nameOfCustomerVendor);
                    sb.Replace("{#vendorEmailAddress#}", vendorEmailAddress);
                    sb.Replace("{#delPickUpLocation#}", delPickUpLocation);
                    sb.Replace("{#modeOfTransport#}", modeOfTransport);
                    sb.Replace("{#incoterms#}", incoterms);
                sb.Replace("{#deliveryTerm#}", deliveryTerm);
                sb.Replace("{#noOfTrucks#}", noOfTrucks);
                sb.Replace("{#noOfContainers#}", noOfContainers);
                sb.Replace("{#readinessDate#}", readinessDate);
                sb.Replace("{#typeOfConsignment#}", typeOfConsignment);

                sb.Replace("{#transporterName#}", transporterName);
                sb.Replace("{#transporterEmail#}", transporterEmail);
                sb.Replace("{#transporterContactNumber#}", transporterContactNumber);
                sb.Replace("{#concPersonCDAndPickupLoc#}", concPersonCDAndPickupLoc);
                sb.Replace("{#lrNo#}", lrNo);
                sb.Replace("{#lrDate#}", lrDate);
                sb.Replace("{#truckNo#}", truckNo);
                sb.Replace("{#fcrBl#}", fcrBl);
                sb.Replace("{#fcrBlDate#}", fcrBlDate);
                sb.Replace("{#containerNo#}", containerNo);

                sb.Replace("{#truck14#}", Convert.ToString(truck14));
                sb.Replace("{#truck17#}", Convert.ToString(truck17));
                sb.Replace("{#truck19#}", Convert.ToString(truck19));
                sb.Replace("{#truck22#}", Convert.ToString(truck22));
                sb.Replace("{#truck24#}", Convert.ToString(truck24));
                sb.Replace("{#truck32#}", Convert.ToString(truck32));
                sb.Replace("{#truck40#}", Convert.ToString(truck40));
                sb.Replace("{#truckLowBed#}", Convert.ToString(truckLowBed)); 
                sb.Replace("{#truckOdc#}", Convert.ToString(truckOdc));


                sb.Replace("{#cont20#}", Convert.ToString(cont20));
                sb.Replace("{#cont40#}", Convert.ToString(cont40));
                sb.Replace("{#cont40HC#}", Convert.ToString(cont40HC));
                sb.Replace("{#contFR#}", Convert.ToString(contFR));
                sb.Replace("{#contFRODC#}", Convert.ToString(contFRODC));


            //truck14 = 0;
            //truck17 = 0;
            //truck19 = 0;
            //truck22 = 0;
            //truck24 = 0;
            //truck32 = 0;
            //truck40 = 0;
            //truckLowBed = 0;
            //truckOdc = 0;

            //cont20 = 0;
            //cont40 = 0;
            //cont40HC = 0;
            //contFR = 0;
            //contFRODC = 0;


            if (createdByID > 0 )
                    {
                        sb.Append("<h3 class='header2'>Signatories</h3>\n");
                        sb.Append("<hr />\n");
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Created</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#createdRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Created By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#createdBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Created On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#createdOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#createdRemarks#}", createdRemarks);
                        sb.Replace("{#createdBy#}", createdBy);
                        sb.Replace("{#createdOn#}", createdOn);
 
                    if (hodApprovedByID > 0 && (inwardStatusID == 2 || inwardStatusID == 5
                        || inwardStatusID == 6 || inwardStatusID == 7))
                    {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>HOD Approved</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>HOD Approved Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#approvedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>HOD Approved By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#approvedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>HOD Approved On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#approvedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#approvedRemarks#}", hodApprovedRemarks);
                            sb.Replace("{#approvedBy#}", hodApprovedBy);
                            sb.Replace("{#approvedOn#}", hodApprovedOn);
                        }


                    if (acknowledgedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Acknowledged</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'> Acknowledged Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#acknowledgedByRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Acknowledged By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#acknowledgedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Acknowledged On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#acknowledgedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#acknowledgedByRemarks#}", acknowledgedByRemarks);
                        sb.Replace("{#acknowledgedBy#}", acknowledgedBy);
                        sb.Replace("{#acknowledgedOn#}", acknowledgedOn);
                    }


                    if (finalApprovedByID > 0)
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Final Confirmed</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'> Final Confirmed Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#finalApprovedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Final Confirmed By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#finalApprovedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Final Confirmed On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#finalApprovedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#finalApprovedRemarks#}", finalApprovedRemarks);
                            sb.Replace("{#finalApprovedBy#}", finalApprovedBy);
                            sb.Replace("{#finalApprovedOn#}", finalApprovedOn);
                        }

                    if (closedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Closed</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'> Closed Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#closedByRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Closed By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#closedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Closed On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#closedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#closedByRemarks#}", closedByRemarks);
                        sb.Replace("{#closedBy#}", closedBy);
                        sb.Replace("{#closedOn#}", closedOn);
                    }


                    if (deletedByID > 0 && inwardStatusID == 3)
                        {

                            if (deletedByID == teamleaderID)
                            {
                                sb.Append("<fieldset class='pdffieldset'>\n");
                                sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                                sb.Append("<table class='tblsignatories'>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                                sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("</table>\n");
                                sb.Append("</fieldset>\n");

                                sb.Append("<hr class='hrsignatories' />\n");

                                sb.Replace("{#deletedRemarks#}", deletedRemarks);
                                sb.Replace("{#deletedBy#}", deletedBy);
                                sb.Replace("{#deletedOn#}", deletedOn);
                            }
                            else
                            {
                                sb.Append("<fieldset class='pdffieldset'>\n");
                                sb.Append("<legend class='pdflegend'>Deleted</legend>\n");
                                sb.Append("<table class='tblsignatories'>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='3'>Deleted Remarks:</td>\n");
                                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td class='tdsignatories1'>Deleted By:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                                sb.Append("<td class='tdsignatories1'>Deleted On:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("</table>\n");
                                sb.Append("</fieldset>\n");

                                sb.Append("<hr class='hrsignatories' />\n");

                                sb.Replace("{#deletedRemarks#}", deletedRemarks);
                                sb.Replace("{#deletedBy#}", deletedBy);
                                sb.Replace("{#deletedOn#}", deletedOn);
                            }

                        }

                        if (cancelledByID > 0 && inwardStatusID == 4)
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Cancelled</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Cancelled Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#cancelledRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Cancelled By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#cancelledBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Cancelled On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#cancelledOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#cancelledRemarks#}", cancelledRemarks);
                            sb.Replace("{#cancelledBy#}", cancelledBy);
                            sb.Replace("{#cancelledOn#}", cancelledOn);
                        }
                    
                }


                htmlText = sb.ToString();
                return htmlText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    public string GetHtmlForPDFOutward(DataTable dtOutwardDetail)
    {
        try
        {
            outwardNo = string.Empty;
            finalConfirmationNo = string.Empty;
            emplyoeeName = string.Empty;
            employeeID = string.Empty;
            nameOfCustomerVendor = string.Empty;
            createdByID = 0;
            createdRemarks = string.Empty;
            createdBy = string.Empty;
            createdOn = string.Empty;

            hodApprovedByID = 0;
            hodApprovedRemarks = string.Empty;
            hodApprovedBy = string.Empty;
            hodApprovedOn = string.Empty;

            finalApprovedByID = 0;
            finalApprovedRemarks = string.Empty;
            finalApprovedBy = string.Empty;
            finalApprovedOn = string.Empty;

            deletedByID = 0;
            deletedRemarks = string.Empty;
            deletedBy = string.Empty;
            deletedOn = string.Empty;

            cancelledByID = 0;
            cancelledRemarks = string.Empty;
            cancelledBy = string.Empty;
            cancelledOn = string.Empty;
            int teamleaderID = 0;
            int outwardStatusID = 0;


            truck14 = 0;
            truck17 = 0;
            truck19 = 0;
            truck22 = 0;
            truck24 = 0;
            truck32 = 0;
            truck40 = 0;
            truckLowBed = 0;
            truckOdc = 0;
            Dcont20 = 0;
            Dcont40 = 0;
            cont20 = 0;
            cont40 = 0;
            Dcont20 = 0;
            Dcont40 = 0;
            cont40HC = 0;
            contFR = 0;
            contFRODC = 0;


            string inwardStatusName = string.Empty;
            string inwardType = string.Empty;
            string jobNo = string.Empty;
            string vendorLocation = string.Empty;
            string vendorEmailAddress = string.Empty;
            string delPickUpLocation = string.Empty;
            string modeOfTransport = string.Empty;
            string incoterms = string.Empty;
            string deliveryTerm = string.Empty;
            string noOfTrucks = string.Empty;
            string noOfContainers = string.Empty;
            string readinessDate = string.Empty;
            string typeOfConsignment = string.Empty;



            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();


            if (dtOutwardDetail.Rows.Count > 0)
            {
                DataRow dr = dtOutwardDetail.Rows[0];

                if (dr["FINAL_CONFIRMATION_NUMBER"] != DBNull.Value)
                    finalConfirmationNo = Convert.ToString(dr["FINAL_CONFIRMATION_NUMBER"]);

                if (dr["OUTWARD_NO"] != DBNull.Value)
                    outwardNo = Convert.ToString(dr["OUTWARD_NO"]);

                if (dr["CREATED_BY_NAME"] != DBNull.Value)
                    emplyoeeName = Convert.ToString(dr["CREATED_BY_NAME"]);

                if (dr["CREATED_BY"] != DBNull.Value)
                    employeeID = Convert.ToString(dr["CREATED_BY"]);


                if (dr["CLIENT_NAME"] != DBNull.Value)
                    nameOfCustomerVendor = Convert.ToString(dr["CLIENT_NAME"]);

                if (dr["STATUS_ID"] != DBNull.Value)
                    outwardStatusID = Convert.ToInt32(dr["STATUS_ID"]);


                if (dr["CREATED_BY"] != DBNull.Value)
                    createdByID = Convert.ToInt32(dr["CREATED_BY"]);

                if (dr["CREATED_REMARKS"] != DBNull.Value)
                    createdRemarks = Convert.ToString(dr["CREATED_REMARKS"]);

                if (dr["CREATED_BY_NAME"] != DBNull.Value)
                    createdBy = Convert.ToString(dr["CREATED_BY_NAME"]);

                if (dr["CREATED_ON"] != DBNull.Value)
                    createdOn = Convert.ToString(dr["CREATED_ON"]);


                if (dr["APPROVED_BY"] != DBNull.Value)
                    hodApprovedByID = Convert.ToInt32(dr["APPROVED_BY"]);

                if (dr["APPROVED_REMARKS"] != DBNull.Value)
                    hodApprovedRemarks = Convert.ToString(dr["APPROVED_REMARKS"]);

                if (dr["APPROVED_BY_NAME"] != DBNull.Value)
                    hodApprovedBy = Convert.ToString(dr["APPROVED_BY_NAME"]);

                if (dr["APPROVED_ON"] != DBNull.Value)
                    hodApprovedOn = Convert.ToString(dr["APPROVED_ON"]);



                if (dr["FINAL_APPROVED_BY"] != DBNull.Value)
                    finalApprovedByID = Convert.ToInt32(dr["FINAL_APPROVED_BY"]);

                if (dr["FINAL_APPROVED_REMARKS"] != DBNull.Value)
                    finalApprovedRemarks = Convert.ToString(dr["FINAL_APPROVED_REMARKS"]);

                if (dr["FINAL_APPROVED_BY_NAME"] != DBNull.Value)
                    finalApprovedBy = Convert.ToString(dr["FINAL_APPROVED_BY_NAME"]);

                if (dr["FINAL_APPROVED_ON"] != DBNull.Value)
                    finalApprovedOn = Convert.ToString(dr["FINAL_APPROVED_ON"]);
                //////////////////


                if (dr["DELETED_BY"] != DBNull.Value)
                    deletedByID = Convert.ToInt32(dr["DELETED_BY"]);

                if (dr["DELETED_REMARKS"] != DBNull.Value)
                    deletedRemarks = Convert.ToString(dr["DELETED_REMARKS"]);

                if (dr["DELETED_BY_NAME"] != DBNull.Value)
                    deletedBy = Convert.ToString(dr["DELETED_BY_NAME"]);

                if (dr["DELETED_ON"] != DBNull.Value)
                    deletedOn = Convert.ToString(dr["DELETED_ON"]);


                if (dr["CANCELLED_BY"] != DBNull.Value)
                    cancelledByID = Convert.ToInt32(dr["CANCELLED_BY"]);

                if (dr["CANCELLED_REMARKS"] != DBNull.Value)
                    cancelledRemarks = Convert.ToString(dr["CANCELLED_REMARKS"]);

                if (dr["CANCELLED_BY_NAME"] != DBNull.Value)
                    cancelledBy = Convert.ToString(dr["CANCELLED_BY_NAME"]);

                if (dr["CANCELLED_ON"] != DBNull.Value)
                    cancelledOn = Convert.ToString(dr["CANCELLED_ON"]);


                if (dr["ACKNOWLEDGED_BY"] != DBNull.Value)
                    acknowledgedByID = Convert.ToInt32(dr["ACKNOWLEDGED_BY"]);

                if (dr["ACKNOWLEDGED_BY_NAME"] != DBNull.Value)
                    acknowledgedBy = Convert.ToString(dr["ACKNOWLEDGED_BY_NAME"]);

                if (dr["ACKNOWLEDGED_ON"] != DBNull.Value)
                    acknowledgedOn = Convert.ToString(dr["ACKNOWLEDGED_ON"]);

                if (dr["ACKNOWLEDGED_REMARKS"] != DBNull.Value)
                    acknowledgedByRemarks = Convert.ToString(dr["ACKNOWLEDGED_REMARKS"]);


                if (dr["CLOSED_BY"] != DBNull.Value)
                    closedByID = Convert.ToInt32(dr["CLOSED_BY"]);

                if (dr["CLOSED_BY_NAME"] != DBNull.Value)
                    closedBy = Convert.ToString(dr["CLOSED_BY_NAME"]);

                if (dr["CLOSED_ON"] != DBNull.Value)
                    closedOn = Convert.ToString(dr["CLOSED_ON"]);

                if (dr["CLOSED_REMARKS"] != DBNull.Value)
                    closedByRemarks = Convert.ToString(dr["CLOSED_REMARKS"]);


                if (dr["TEAMLEADER_ID"] != DBNull.Value)
                    teamleaderID = Convert.ToInt32(dr["TEAMLEADER_ID"]);

                if (dr["STATUS_NAME"] != DBNull.Value)
                    inwardStatusName = Convert.ToString(dr["STATUS_NAME"]);

                if (dr["TYPE_OF_OUTWARD"] != DBNull.Value)
                    inwardType = Convert.ToString(dr["TYPE_OF_OUTWARD"]);

                if (dr["JOB_NO"] != DBNull.Value)
                    jobNo = Convert.ToString(dr["JOB_NO"]);

                if (dr["CLIENT_LOCATION"] != DBNull.Value)
                    vendorLocation = Convert.ToString(dr["CLIENT_LOCATION"]);

                if (dr["CLIENT_EMAIL"] != DBNull.Value)
                    vendorEmailAddress = Convert.ToString(dr["CLIENT_EMAIL"]);

                if (dr["DELIVERY_PICKUP_LOCATION"] != DBNull.Value)
                    delPickUpLocation = Convert.ToString(dr["DELIVERY_PICKUP_LOCATION"]);

                if (dr["INTERNATIONAL_MODE_OF_TRANSPORT"] != DBNull.Value)
                    modeOfTransport = Convert.ToString(dr["INTERNATIONAL_MODE_OF_TRANSPORT"]);

                if (dr["INCOTERMS"] != DBNull.Value)
                    incoterms = Convert.ToString(dr["INCOTERMS"]);

                if (dr["DELIVERY_TERM"] != DBNull.Value)
                    deliveryTerm = Convert.ToString(dr["DELIVERY_TERM"]);

                if (dr["NO_OF_TRUCKS"] != DBNull.Value)
                    noOfTrucks = Convert.ToString(dr["NO_OF_TRUCKS"]);

                if (dr["NO_OF_CONTAINERS"] != DBNull.Value)
                    noOfContainers = Convert.ToString(dr["NO_OF_CONTAINERS"]);

                if (dr["DATE_OF_PICKUP"] != DBNull.Value)
                    readinessDate = Convert.ToString(dr["DATE_OF_PICKUP"]);

                if (dr["TYPE_OF_CONSIGNMENT"] != DBNull.Value)
                    typeOfConsignment = Convert.ToString(dr["TYPE_OF_CONSIGNMENT"]);


                if (dr["TRANSPORTER_NAME"] != DBNull.Value)
                    transporterName = Convert.ToString(dr["TRANSPORTER_NAME"]);

                if (dr["TRANSPORTER_CONTACT_NUMBER"] != DBNull.Value)
                    transporterContactNumber = Convert.ToString(dr["TRANSPORTER_CONTACT_NUMBER"]);

                if (dr["TRANSPORTER_EMAIL"] != DBNull.Value)
                    transporterEmail = Convert.ToString(dr["TRANSPORTER_EMAIL"]);

                if (dr["FCR_BL"] != DBNull.Value)
                    fcrBl = Convert.ToString(dr["FCR_BL"]);

                if (dr["FCR_BL_DATE"] != DBNull.Value)
                    fcrBlDate = Convert.ToString(dr["FCR_BL_DATE"]);

                if (dr["CONTAINER_NO"] != DBNull.Value)
                    containerNo = Convert.ToString(dr["CONTAINER_NO"]);

                if (dr["LR_NO"] != DBNull.Value)
                    lrNo = Convert.ToString(dr["LR_NO"]);

                if (dr["LR_DATE"] != DBNull.Value)
                    lrDate = Convert.ToString(dr["LR_DATE"]);

                if (dr["TRUCK_NO"] != DBNull.Value)
                    truckNo = Convert.ToString(dr["TRUCK_NO"]);


                if (dr["TRANSPORTER_NAME"] != DBNull.Value)
                    transporterName = Convert.ToString(dr["TRANSPORTER_NAME"]);

                if (dr["TRANSPORTER_CONTACT_NUMBER"] != DBNull.Value)
                    transporterContactNumber = Convert.ToString(dr["TRANSPORTER_CONTACT_NUMBER"]);

                if (dr["TRANSPORTER_EMAIL"] != DBNull.Value)
                    transporterEmail = Convert.ToString(dr["TRANSPORTER_EMAIL"]);


                if (dr["QTY_OF_TRUCK_14"] != DBNull.Value)
                    truck14 = Convert.ToInt32(dr["QTY_OF_TRUCK_14"]);

                if (dr["QTY_OF_TRUCK_17"] != DBNull.Value)
                    truck17 = Convert.ToInt32(dr["QTY_OF_TRUCK_17"]);

                if (dr["QTY_OF_TRUCK_19"] != DBNull.Value)
                    truck19 = Convert.ToInt32(dr["QTY_OF_TRUCK_19"]);

                if (dr["QTY_OF_TRUCK_22"] != DBNull.Value)
                    truck22 = Convert.ToInt32(dr["QTY_OF_TRUCK_22"]);

                if (dr["QTY_OF_TRUCK_24"] != DBNull.Value)
                    truck24 = Convert.ToInt32(dr["QTY_OF_TRUCK_24"]);

                if (dr["QTY_OF_TRUCK_32"] != DBNull.Value)
                    truck32 = Convert.ToInt32(dr["QTY_OF_TRUCK_32"]);


                if (dr["QTY_OF_TRUCK_40"] != DBNull.Value)
                    truck40 = Convert.ToInt32(dr["QTY_OF_TRUCK_40"]);

                if (dr["QTY_OF_LOW_BED"] != DBNull.Value)
                    truckLowBed = Convert.ToInt32(dr["QTY_OF_LOW_BED"]);


                if (dr["TRUCK_QTY_OF_ODC"] != DBNull.Value)
                    truckOdc = Convert.ToInt32(dr["TRUCK_QTY_OF_ODC"]);

                if (dr["QTY_OF_D_CONT_20"] != DBNull.Value)
                    Dcont20 = Convert.ToInt32(dr["QTY_OF_D_CONT_20"]);


                if (dr["QTY_OF_D_CONT_40"] != DBNull.Value)
                    Dcont40 = Convert.ToInt32(dr["QTY_OF_D_CONT_40"]);


                if (dr["QTY_OF_CONTAINERS_20"] != DBNull.Value)
                    cont20 = Convert.ToInt32(dr["QTY_OF_CONTAINERS_20"]);

                if (dr["QTY_OF_CONTAINERS_40"] != DBNull.Value)
                    cont40 = Convert.ToInt32(dr["QTY_OF_CONTAINERS_40"]);


                if (dr["QTY_OF_CONTAINERS_40HC"] != DBNull.Value)
                    cont40HC = Convert.ToInt32(dr["QTY_OF_CONTAINERS_40HC"]);

                if (dr["QTY_OF_FR"] != DBNull.Value)
                    contFR = Convert.ToInt32(dr["QTY_OF_FR"]);


                if (dr["CONTAINER_QTY_OF_ODC"] != DBNull.Value)
                    contFRODC = Convert.ToInt32(dr["CONTAINER_QTY_OF_ODC"]);

                sb.Append("<h2 class='headerStyle'>Outward Information</h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Outward No.:</td>\n");
                sb.Append("<td class='td2header'>{#outwardNo#}</td>\n");
                sb.Append("<td class='td1header'>Final Confirmation No.:</td>\n");
                sb.Append("<td class='td2header'>{#finalConfirmationNo#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Requester Name:</td>\n");
                sb.Append("<td class='td2header'>{#emplyoeeName#}</td>\n");
                //sb.Append("<td class='td1header'>Employee ID:</td>\n");
                //sb.Append("<td class='td2header'>{#employeeID#}</td>\n");
                sb.Append("<td class='td1header'>Type Of Consignment:</td>\n");
                sb.Append("<td class='td2header'>{#typeOfConsignment#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Name Of Customer:</td>\n");
                sb.Append("<td class='td2header'>{#nameOfCustomerVendor#}</td>\n");
                sb.Append("<td class='td1header'>Customer Location:</td>\n");
                sb.Append("<td class='td2header'>{#vendorLocation#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Customer Email:</td>\n");
                sb.Append("<td class='td2header'>{#vendorEmailAddress#}</td>\n");
                sb.Append("<td class='td1header'>Pickup Location:</td>\n");
                sb.Append("<td class='td2header'>{#delPickUpLocation#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Mode Of Transport:</td>\n");
                sb.Append("<td class='td2header'>{#modeOfTransport#}</td>\n");
                sb.Append("<td class='td1header'>Incoterms:</td>\n");
                sb.Append("<td class='td2header'>{#incoterms#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Delivery Term:</td>\n");
                sb.Append("<td class='td2header'>{#deliveryTerm#}</td>\n");
                sb.Append("<td class='td1header'>No Of Trucks:</td>\n");
                sb.Append("<td class='td2header'>{#noOfTrucks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>No Of Containers:</td>\n");
                sb.Append("<td class='td2header'>{#noOfContainers#}</td>\n");
                sb.Append("<td class='td1header'>Readiness Date:</td>\n");
                sb.Append("<td class='td2header'>{#readinessDate#}</td>\n");
                sb.Append("</tr>\n");
                //sb.Append("<tr>\n");


                if (truck14 > 0 || truck17 > 0 || truck19 > 0)
                {
                    sb.Append("<tr>\n");
                    if (truck14 > 0)
                    {
                        sb.Append("<td class='td1header'>truck14:</td>\n");
                        sb.Append("<td class='td2header'>{#truck14#}</td>\n");
                    }
                    if (truck17 > 0)
                    {
                        sb.Append("<td class='td1header'>truck17:</td>\n");
                        sb.Append("<td class='td2header'>{#truck17#}</td>\n");
                    }
                    if (truck19 > 0)
                    {
                        sb.Append("<td class='td1header'>truck19:</td>\n");
                        sb.Append("<td class='td2header'>{#truck19#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }

                if (truck22 > 0 || truck24 > 0 || truck32 > 0)
                {
                    sb.Append("<tr>\n");
                    if (truck22 > 0)
                    {
                        sb.Append("<td class='td1header'>truck22:</td>\n");
                        sb.Append("<td class='td2header'>{#truck22#}</td>\n");
                    }
                    if (truck24 > 0)
                    {
                        sb.Append("<td class='td1header'>truck24:</td>\n");
                        sb.Append("<td class='td2header'>{#truck24#}</td>\n");
                    }
                    if (truck32 > 0)
                    {
                        sb.Append("<td class='td1header'>truck32:</td>\n");
                        sb.Append("<td class='td2header'>{#truck32#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }


                if (truck40 > 0 || truckLowBed > 0 || truckOdc > 0)
                {
                    sb.Append("<tr>\n");
                    if (truck40 > 0)
                    {
                        sb.Append("<td class='td1header'>truck40:</td>\n");
                        sb.Append("<td class='td2header'>{#truck40#}</td>\n");
                    }
                    if (truckLowBed > 0)
                    {
                        sb.Append("<td class='td1header'>truckLowBed:</td>\n");
                        sb.Append("<td class='td2header'>{#truckLowBed#}</td>\n");
                    }
                    if (truckOdc > 0)
                    {
                        sb.Append("<td class='td1header'>truckOdc:</td>\n");
                        sb.Append("<td class='td2header'>{#truckOdc#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }

                if (Dcont20 > 0 || Dcont40 > 0 )
                {
                    sb.Append("<tr>\n");
                    if (Dcont20 > 0)
                    {
                        sb.Append("<td class='td1header'>Conatiner20:</td>\n");
                        sb.Append("<td class='td2header'>{#Dcont20#}</td>\n");
                    }
                    if (Dcont40 > 0)
                    {
                        sb.Append("<td class='td1header'>Conatiner40:</td>\n");
                        sb.Append("<td class='td2header'>{#Dcont40#}</td>\n");
                    }
                  
                    sb.Append("</tr>\n");
                }


                if (cont20 > 0 || cont40 > 0 || cont40HC > 0)
                {
                    sb.Append("<tr>\n");
                    if (cont20 > 0)
                    {
                        sb.Append("<td class='td1header'>cont20:</td>\n");
                        sb.Append("<td class='td2header'>{#cont20#}</td>\n");
                    }
                    if (cont40 > 0)
                    {
                        sb.Append("<td class='td1header'>cont40:</td>\n");
                        sb.Append("<td class='td2header'>{#cont40#}</td>\n");
                    }
                    if (cont40HC > 0)
                    {
                        sb.Append("<td class='td1header'>cont40HC:</td>\n");
                        sb.Append("<td class='td2header'>{#cont40HC#}</td>\n");
                    }
                    sb.Append("</tr>\n");
                }

                if (contFR > 0 || contFRODC > 0)
                {
                    sb.Append("<tr>\n");
                    if (contFR > 0)
                    {
                        sb.Append("<td class='td1header'>contFR:</td>\n");
                        sb.Append("<td class='td2header'>{#contFR#}</td>\n");
                    }
                    if (contFRODC > 0)
                    {
                        sb.Append("<td class='td1header'>contFRODC:</td>\n");
                        sb.Append("<td class='td2header'>{#contFRODC#}</td>\n");
                    }

                    sb.Append("</tr>\n");
                }


                //sb.Append("</tr>\n");
                if (outwardStatusID == 5 || outwardStatusID == 7)
                {
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Transporter Name:</td>\n");
                    sb.Append("<td class='td2header'>{#transporterName#}</td>\n");
                    sb.Append("<td class='td1header'>Transporter Contact Details:</td>\n");
                    sb.Append("<td class='td2header'>{#transporterContactNumber#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Transaporter Email:</td>\n");
                    sb.Append("<td class='td2header'>{#transporterEmail#}</td>\n");
                    sb.Append("</tr>\n");

                }

                if (outwardStatusID == 7)
                    if (inwardType == "INTERNATIONAL")
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>FCR/BL:</td>\n");
                        sb.Append("<td class='td2header'>{#fcrBl#}</td>\n");
                        sb.Append("<td class='td1header'>FCR/BL Date:</td>\n");
                        sb.Append("<td class='td2header'>{#fcrBlDate#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>Container No:</td>\n");
                        sb.Append("<td class='td2header'>{#containerNo#}</td>\n");
                        //sb.Append("<td class='td1header'>Vehicle PLacement Date:</td>\n");
                        //sb.Append("<td class='td2header'>{#transporterEmail#}</td>\n");
                        sb.Append("</tr>\n");

                    }
                    else
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>LR No.:</td>\n");
                        sb.Append("<td class='td2header'>{#lrNo#}</td>\n");
                        sb.Append("<td class='td1header'>LR Date:</td>\n");
                        sb.Append("<td class='td2header'>{#lrDate#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1header'>Truck No:</td>\n");
                        sb.Append("<td class='td2header'>{#truckNo#}</td>\n");
                        //sb.Append("<td class='td1header'>Vehicle PLacement Date:</td>\n");
                        //sb.Append("<td class='td2header'>{#transporterEmail#}</td>\n");
                        sb.Append("</tr>\n");
                    }


                sb.Append("</table>\n");
                sb.Append("<hr />\n");

                sb.Replace("{#outwardNo#}", outwardNo);
                sb.Replace("{#finalConfirmationNo#}", finalConfirmationNo);
                sb.Replace("{#emplyoeeName#}", emplyoeeName);
                sb.Replace("{#employeeID#}", employeeID);
                sb.Replace("{#vendorLocation#}", vendorLocation);
                sb.Replace("{#vendorEmailAddress#}", vendorEmailAddress);
                sb.Replace("{#delPickUpLocation#}", delPickUpLocation);

                sb.Replace("{#nameOfCustomerVendor#}", nameOfCustomerVendor);
                sb.Replace("{#typeOfConsignment#}", typeOfConsignment);

                sb.Replace("{#readinessDate#}", readinessDate);
                sb.Replace("{#noOfContainers#}", noOfContainers);
                sb.Replace("{#noOfTrucks#}", noOfTrucks);
                sb.Replace("{#deliveryTerm#}", deliveryTerm);
                sb.Replace("{#incoterms#}", incoterms);
                sb.Replace("{#modeOfTransport#}", modeOfTransport);

                sb.Replace("{#transporterName#}", transporterName);
                sb.Replace("{#transporterEmail#}", transporterEmail);
                sb.Replace("{#transporterContactNumber#}", transporterContactNumber);

                sb.Replace("{#lrNo#}", lrNo);
                sb.Replace("{#lrDate#}", lrDate);
                sb.Replace("{#truckNo#}", truckNo);
                sb.Replace("{#fcrBl#}", fcrBl);
                sb.Replace("{#fcrBlDate#}", fcrBlDate);
                sb.Replace("{#containerNo#}", containerNo);
                sb.Replace("{#truck14#}", Convert.ToString(truck14));
                sb.Replace("{#truck17#}", Convert.ToString(truck17));
                sb.Replace("{#truck19#}", Convert.ToString(truck19));
                sb.Replace("{#truck22#}", Convert.ToString(truck22));
                sb.Replace("{#truck24#}", Convert.ToString(truck24));
                sb.Replace("{#truck32#}", Convert.ToString(truck32));
                sb.Replace("{#truck40#}", Convert.ToString(truck40));
                sb.Replace("{#truckLowBed#}", Convert.ToString(truckLowBed));
                sb.Replace("{#truckOdc#}", Convert.ToString(truckOdc));
                sb.Replace("{#Dcont20#}", Convert.ToString(Dcont20));
                sb.Replace("{#Dcont40#}", Convert.ToString(Dcont40));


                sb.Replace("{#cont20#}", Convert.ToString(cont20));
                sb.Replace("{#cont40#}", Convert.ToString(cont40));
                sb.Replace("{#cont40HC#}", Convert.ToString(cont40HC));
                sb.Replace("{#contFR#}", Convert.ToString(contFR));
                sb.Replace("{#contFRODC#}", Convert.ToString(contFRODC));

                if (createdByID > 0)
                {
                    sb.Append("<h3 class='header2'>Signatories</h3>\n");
                    sb.Append("<hr />\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Created</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#createdRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Created By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#createdBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Created On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#createdOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#createdRemarks#}", createdRemarks);
                    sb.Replace("{#createdBy#}", createdBy);
                    sb.Replace("{#createdOn#}", createdOn);


                    if (acknowledgedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Acknowledged</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'> Acknowledged Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#acknowledgedByRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Acknowledged By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#acknowledgedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Acknowledged On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#acknowledgedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#acknowledgedByRemarks#}", acknowledgedByRemarks);
                        sb.Replace("{#acknowledgedBy#}", acknowledgedBy);
                        sb.Replace("{#acknowledgedOn#}", acknowledgedOn);
                    }


                    if (finalApprovedByID > 0 )
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Final Confirmed</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'> Final Confirmed Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#finalApprovedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Final Confirmed By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#finalApprovedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Final Confirmed On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#finalApprovedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#finalApprovedRemarks#}", finalApprovedRemarks);
                        sb.Replace("{#finalApprovedBy#}", finalApprovedBy);
                        sb.Replace("{#finalApprovedOn#}", finalApprovedOn);
                    }

                    if (closedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Closed</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'> Closed Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#closedByRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Closed By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#closedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Closed On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#closedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#closedByRemarks#}", closedByRemarks);
                        sb.Replace("{#closedBy#}", closedBy);
                        sb.Replace("{#closedOn#}", closedOn);
                    }

                    if (deletedByID > 0 || outwardStatusID == 3)
                    {

                        if (deletedByID == teamleaderID)
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#deletedRemarks#}", deletedRemarks);
                            sb.Replace("{#deletedBy#}", deletedBy);
                            sb.Replace("{#deletedOn#}", deletedOn);
                        }
                        else
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Deleted</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Deleted Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Deleted By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Deleted On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#deletedRemarks#}", deletedRemarks);
                            sb.Replace("{#deletedBy#}", deletedBy);
                            sb.Replace("{#deletedOn#}", deletedOn);
                        }

                    }

                    if (cancelledByID > 0 || outwardStatusID == 4)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Cancelled</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Cancelled Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#cancelledRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Cancelled By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#cancelledBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Cancelled On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#cancelledOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#cancelledRemarks#}", cancelledRemarks);
                        sb.Replace("{#cancelledBy#}", cancelledBy);
                        sb.Replace("{#cancelledOn#}", cancelledOn);
                    }

                }

            }

            htmlText = sb.ToString();
            return htmlText;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


}






