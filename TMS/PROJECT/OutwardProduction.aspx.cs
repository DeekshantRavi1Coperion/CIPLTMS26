using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;

public partial class PROJECT_OutwardProduction : System.Web.UI.Page
{

    DataSet dsJobNo = new DataSet();
    DataSet dsVendorData = new DataSet();

    int companyID = 0;
    string custCode = string.Empty;
    string jobNo = string.Empty;
    string poNo = string.Empty;
    string VendorName = string.Empty;
    string VendorCode = string.Empty;
    string VendorEmail = string.Empty;
    string VendorContact = string.Empty;

    string VendorLocation = string.Empty;
    string deliveryLocation = string.Empty;
    string incoTerms = string.Empty;
    string deliveryTerm = string.Empty;
    string txtVendorNamePickup = string.Empty;
    string txtVendAddressPickup = string.Empty;
    string txtVendContactPickup  = string.Empty;

    string noOfTruck = string.Empty;
    string datePickup = string.Empty;
    string typeOfConsignment = string.Empty;
    string packingListName = string.Empty;

    int isConStuffingPossible = 0;
    string noOfContainer = string.Empty;

    string createdRemarksDomestic = string.Empty;
    string createdremarksInt = string.Empty;

    string otherPickupLocation = string.Empty;
    int isClientDeliveryAddressOtherThanAbove = 0;
    string clientOtherDelLocDiff = string.Empty;

    string packingListName2 = string.Empty;
    Byte[] packingListNameBytes = null;
    Byte[] packingListName2Bytes = null;


    DataSet dsUnit = new DataSet();
    DataSet dsVendor = new DataSet();
    DataSet dsIncoterms = new DataSet();

    BAL.Project objProject = new BAL.Project();
    BAL.LessonLearnt objLessonLearnt = new BAL.LessonLearnt();
    BAL.InwardOutward InwardOutward = new BAL.InwardOutward();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (IsPostBack)
            {
                txtIsNoOfContainersRequired.Text = hdnTotalContainers.Value;
                txtIsNoOfTrucksReq.Text = hdnNoOfTruck.Value;

            }

            if (!IsPostBack)
            {
                BindPartLoadDetails();
                GetContainersDetails();
                //BinContainerData();
                BindCompany();
                BindIncoterms();
                BindModeOfTransport();
                BindContainerStuffingPossible();
                BindDomesticDeliveryTerm();
                BindTypeOfConsignment();
                pnlForm1.Visible = rblForms.SelectedValue == "1";
                pnlForm2.Visible = rblForms.SelectedValue == "2";
                //upnlPopup.Show();
                BindTruckData();

                //hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            }
        };

         

    }

    protected void ddlMaterialPickUpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(txtVendorNameInt.Text != "")
        {
            txtVendorNameInt.Text = "";
        }
        if (txtVendorAddressInt.Text != "")
        {
            txtVendorAddressInt.Text = "";
        }
        if (txtContactDetailsVenPickup.Text != "")
        {
            txtContactDetailsVenPickup.Text = "";
        }
        if (txtVendornameVenPickup.Text != "")
        {
            txtVendornameVenPickup.Text = "";
        }
        if (txtVendAddressVenPickup.Text != "")
        {
            txtVendAddressVenPickup.Text = "";
        }
        if (txtVenContactDetailsPickup.Text != "")
        {
            txtVenContactDetailsPickup.Text = "";
        }


        if (txtOthersInt.Text != "")
        {
            txtOthersInt.Text = "";
        }

        if (txtothersPickupLocD.Text != "")
        {
            txtothersPickupLocD.Text = "";
        }


        if (ddlDeliveryLoc.SelectedValue == "Vendor location")
        {
            mpeVendorDetail.Show();
            pnlPopupVendorDetail.Visible = true;
            GetVendorDetail();
        }
        else if (ddlDeliveryLoc.SelectedValue == "Others")
        {
            txtothersPickupLocD.Enabled = true;
        }

        if (ddlVenLoc.SelectedValue == "Vendor location")
        {
            mpeVendorDetail.Show();
            pnlPopupVendorDetail.Visible = true;
            GetVendorDetail();
        }
        else if (ddlVenLoc.SelectedValue == "Others")
        {
            txtOthersInt.Enabled = true;
        }

    }

    private void BindPartLoadDetails()
    {
        try
        {
            ddlPartLoadDetails.Items.Clear();
            ddlPartLoadDetails.Items.Add(new ListItem("Select", ""));
            ddlPartLoadDetails.Items.Add(new ListItem("XPS", "XPS"));
            ddlPartLoadDetails.Items.Add(new ListItem("GATI", "GATI"));
            ddlPartLoadDetails.Items.Add(new ListItem("Safex", "Safex"));
            ddlPartLoadDetails.Items.Add(new ListItem("TCI", "TCI"));
            ddlPartLoadDetails.Items.Add(new ListItem("Others", "Others"));

        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindContainerStuffingPossible()
    {
        try
        {// Clear existing items to prevent duplicates
            ddlIsStuffing.Items.Clear();
            ddlIsStuffing.Items.Add(new ListItem("Select", ""));
            ddlIsStuffing.Items.Add(new ListItem("Yes", "Yes"));
            ddlIsStuffing.Items.Add(new ListItem("No", "No"));


        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }




    private void BindModeOfTransport()
    {
        try
        {// Clear existing items to prevent duplicates
            ddlModeOfTransport.Items.Clear();

            // Add new items
            //ddlInco2.Items.Add(new ListItem("Select", ""));
            //ddlInco2.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            //ddlInco2.Items.Add(new ListItem("FOT", "FOT"));
            //ddlInco2.Items.Add(new ListItem("Door Delivery", "Door_Delivery"));

            ddlModeOfTransport.Items.Add(new ListItem("Select", ""));
            ddlModeOfTransport.Items.Add(new ListItem("Air", "Air"));
            ddlModeOfTransport.Items.Add(new ListItem("Sea", "Sea"));


        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindIncoterms()
    {
        try
        {// Clear existing items to prevent duplicates
            ddlInco2.Items.Clear();

            // Add new items
            //ddlInco2.Items.Add(new ListItem("Select", ""));
            //ddlInco2.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            //ddlInco2.Items.Add(new ListItem("FOT", "FOT"));
            //ddlInco2.Items.Add(new ListItem("Door Delivery", "Door_Delivery"));

            ddlInco2.Items.Add(new ListItem("Select", ""));
            ddlInco2.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            ddlInco2.Items.Add(new ListItem("FOB", "FOB"));
            ddlInco2.Items.Add(new ListItem("CIF", "CIF"));
            ddlInco2.Items.Add(new ListItem("C&F", "C&F"));


            //ddlIncoterms2.Items.Clear();

            //// Add new items
            //ddlIncoterms2.Items.Add(new ListItem("Select", ""));
            //ddlIncoterms2.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            //ddlIncoterms2.Items.Add(new ListItem("FOT", "FOT"));
            //ddlIncoterms2.Items.Add(new ListItem("Door Delivery", "Door_Delivery"));


        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindDomesticDeliveryTerm()
    {
        //ddlDeliveryTerm2.Items.Clear();
        //ddlDeliveryTerm2.Items.Add(new ListItem("Select", ""));
        //ddlDeliveryTerm2.Items.Add(new ListItem("Door delivery,Freight paid and reimbursable", "Door delivery,Freight paid and reimbursable"));
        //ddlDeliveryTerm2.Items.Add(new ListItem("Freight To Pay", "Freight To Pay"));

        ddlDeliveryTerm2.Items.Clear();
        ddlDeliveryTerm2.Items.Add(new ListItem("Select", ""));
        ddlDeliveryTerm2.Items.Add(new ListItem("Freight Paid", "Freight Paid"));
        ddlDeliveryTerm2.Items.Add(new ListItem("Freight To Pay", "Freight To Pay"));
        ddlDeliveryTerm2.Items.Add(new ListItem("Freight Paid & Reimbursable", "Freight Paid & Reimbursable"));

    }

    private void BindTypeOfConsignment()
    {

        ddlTypeOfConsignment2.Items.Clear();
        ddlTypeOfConsignment2.Items.Add(new ListItem("Select", ""));
        ddlTypeOfConsignment2.Items.Add(new ListItem("Full Consignment", "Full Consignment"));
        ddlTypeOfConsignment2.Items.Add(new ListItem("Part Load", "Part Load"));

        ddlTypeConsignment.Items.Clear();
        ddlTypeConsignment.Items.Add(new ListItem("Select", ""));
        ddlTypeConsignment.Items.Add(new ListItem("Full Consignment - FCL", "Full Consignment - FCL"));
        ddlTypeConsignment.Items.Add(new ListItem("Part Load - LCL", "Part Load - LCL"));
    }

    private void BindCompany()
    {
        try
        {
            dsUnit = InwardOutward.getLocation();

            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                DataTable filteredTable = dsUnit.Tables[0].Clone();
                foreach (DataRow row in dsUnit.Tables[0].Rows)
                {
                    if (!row["UNIT_NAME"].ToString().Contains("CID2527") &&
                         !row["UNIT_NAME"].ToString().Contains("CID_INDIANGAAP2426"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                //ddlVenLoc

                ddlVenLoc.DataSource = filteredTable;
                ddlVenLoc.DataTextField = "UNIT_NAME";
                ddlVenLoc.DataValueField = "UNIT_ID";
                ddlVenLoc.DataBind();
                ddlVenLoc.Items.Insert(2, "Vendor location");
                ddlVenLoc.Items.Insert(3, "Others");
                ddlVenLoc.SelectedIndex = 0;

                ddlDeliveryLoc.DataSource = filteredTable;
                ddlDeliveryLoc.DataTextField = "UNIT_NAME";
                ddlDeliveryLoc.DataValueField = "UNIT_ID";
                ddlDeliveryLoc.DataBind();
                //ddlCompany.Items.Insert(0, "Select");
                ddlDeliveryLoc.Items.Insert(2, "Vendor location");
                ddlDeliveryLoc.Items.Insert(3, "Others");
                ddlDeliveryLoc.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }
    protected void rblForms_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (rblForms.SelectedValue != null)
        //{
        pnlForm1.Visible = rblForms.SelectedValue == "1";
        pnlForm2.Visible = rblForms.SelectedValue == "2";
        //}

    }

    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        upnlPopup.Show();
        //pnlPopupJOBDetail.Visible = true;
        GetJOBDetail();
    }

    protected void btnGetDomestic_Click(object sender, EventArgs e)
    {

        //mpeVendorDetail.Show();
        //pnlPopupVendorDetail.Visible = true;
        //GetVendorDetail();
        mpeVendorDetail.Show();
        GetVendorDetail();
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        upnlPopup.Show();
        GetJOBDetail();
    }

    protected void btnSearchVendor_Click(object sender, EventArgs e)
    {
        mpeVendorDetail.Show();
        GetVendorDetail();
    }

    //protected void btnGetJOBNo_Click(object sender, EventArgs e)
    //{
    //    pnlPopupJOBDetail.Visible = true;
    //    upnlPopup.Show();
    //    ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "setTimeout(function() { $find('upnlPopup').show(); }, 500);", true);
    //}



    protected void btnAddApprover_Click(object sender, EventArgs e)
    {
        //mpeAddApprovers.Show();
        //iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0&unitid=" + Convert.ToString(ddlCompany.SelectedValue) + "");
    }

   
    private void GetVendorDetail()
    {
        try
        {
            dsVendorData = GetVendorData();
            if (dsVendorData.Tables.Count > 0 && dsVendorData.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                GridView1.DataSource = dsVendorData.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            lblVendorDetail.Text = "Records[" + GridView1.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
        }
    }


    private DataSet GetVendorData()
    {
        try
        {
            companyID = 0;
            VendorCode = string.Empty;
            VendorName = string.Empty;


            companyID = 1;

            if (!string.IsNullOrEmpty(txtVenCode.Text))
                VendorCode = txtVenCode.Text;
            else
                VendorCode = string.Empty;

            if (!string.IsNullOrEmpty(txtVenName.Text))
                VendorName = txtVenName.Text;
            else
                VendorName = string.Empty;


            //dsVendor = InwardOutward.GetVendorDetails(companyID, VendorCode, VendorName);
            dsVendor = InwardOutward.GetVendorDetails(VendorCode, VendorName);

            if (dsVendor.Tables.Count > 0)
            {
                return dsVendor;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void GetJOBDetail()
    {
        try
        {
            dsJobNo = GetJOBData();
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsJobNo.Tables[0];
                gvJOBDetail.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
        }
    }

    private DataSet GetJOBData()
    {
        try
        {
            companyID = 0;
            custCode = string.Empty;
            jobNo = string.Empty;
            poNo = string.Empty;

            companyID = 1;

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                custCode = txtCustomerCodeSearch.Text;
            else
                custCode = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;
            else
                poNo = string.Empty;

            dsJobNo = objProject.GetJOBDetailsForShipping(companyID, custCode, jobNo, poNo);

            if (dsJobNo.Tables.Count > 0)
            {
                return dsJobNo;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return null;
        }
    }


    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                //int rowindex = 0;
                //if (Convert.ToString(e.CommandArgument) == "PROPERTIES" || Convert.ToString(e.CommandArgument) == "REMOVE")
                //{
                //    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //    rowindex = rowSelect.RowIndex;
                //}


                int rowindex = 0;
                btnAddApprover.Visible = false;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblAppJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblAppJOBNo") as Label;
                Label lblPONo = gvJOBDetail.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblCustomerName = gvJOBDetail.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblCustCode = gvJOBDetail.Rows[rowindex].FindControl("lblCustCode") as Label;

                Label lblAddress12 = gvJOBDetail.Rows[rowindex].FindControl("lblAddress12") as Label;
                Label lblAddress22 = gvJOBDetail.Rows[rowindex].FindControl("lblAddress22") as Label;
                Label lblAddress2 = gvJOBDetail.Rows[rowindex].FindControl("lblAddress2") as Label;
                Label lblState2 = gvJOBDetail.Rows[rowindex].FindControl("lblState2") as Label;
                Label lblCountry2 = gvJOBDetail.Rows[rowindex].FindControl("lblCountry2") as Label;
                Label lblPinCode2 = gvJOBDetail.Rows[rowindex].FindControl("lblPinCode2") as Label;
                Label lblEmail2 = gvJOBDetail.Rows[rowindex].FindControl("lblEmail2") as Label;
                Label lblPhone2 = gvJOBDetail.Rows[rowindex].FindControl("lblPhone2") as Label;

                StringBuilder sb = new StringBuilder();

                if (lblAddress12 != null && lblAddress12.Text != null && lblAddress12.Text.Trim() != "")
                    sb.Append(lblAddress12.Text.Trim() + " ");

                if (lblAddress22 != null && lblAddress22.Text != null && lblAddress22.Text.Trim() != "")
                    sb.Append(lblAddress22.Text.Trim() + " ");

                if (lblAddress2 != null && lblAddress2.Text != null && lblAddress2.Text.Trim() != "")
                    sb.Append(lblAddress2.Text.Trim() + " ");

                
                String ClientName = string.Empty;
                String ClientEmail = string.Empty;
                String ClientAddress = string.Empty;
                String ClientContact = string.Empty;
                DataSet jobdet = new DataSet();

                if (rblForms.SelectedValue == "1")  // Domestic Outward
                {
                    if(lblJOBNo.Text != "")
                    {

                        String jobNumForClientDetais = Convert.ToString(lblJOBNo.Text).Trim();
                        String custNameForClientDetais = Convert.ToString(lblCustomerName.Text).Trim();
                        jobdet = InwardOutward.GetClientDetails(jobNumForClientDetais, custNameForClientDetais);
                        if (jobdet.Tables.Count > 0 && jobdet.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr0 = jobdet.Tables[0].Rows[0];
                            ClientName = Convert.ToString(dr0["CLIENT_NAME"]);
                            ClientEmail = Convert.ToString(dr0["CLIENT_EMAIL"]);
                            ClientAddress = Convert.ToString(dr0["CLIENT_ADDRESS"]);
                            ClientContact = Convert.ToString(dr0["CLIENT_CONTACT_DETAILS"]);

                            txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
                            txtVenName2.Text = Convert.ToString(lblCustomerName.Text).Trim();
                            txtVenLoc2.Text = ClientAddress;
                            txtVenEmail2.Text = ClientEmail;
                            txtVenContact.Text = ClientContact;
                        }
                        else
                        {
                            txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
                            txtVenName2.Text = Convert.ToString(lblCustomerName.Text).Trim();
                            //txtVenLoc2.Text = sb.ToString().Trim();
                            //txtVenEmail2.Text = Convert.ToString(lblEmail2.Text).Trim();
                            //txtVenContact.Text = Convert.ToString(lblPhone2.Text).Trim();

                        }
                    }



                   
                }
                else if (rblForms.SelectedValue == "2")  // International Outward
                {

                    if (lblJOBNo.Text != "")
                    {

                        String jobNumForClientDetais = Convert.ToString(lblJOBNo.Text).Trim();
                        String custNameForClientDetais = Convert.ToString(lblCustomerName.Text).Trim();
                        jobdet = InwardOutward.GetClientDetails(jobNumForClientDetais, custNameForClientDetais);
                        if (jobdet.Tables.Count > 0 && jobdet.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr0 = jobdet.Tables[0].Rows[0];
                            ClientName = Convert.ToString(dr0["CLIENT_NAME"]);
                            ClientEmail = Convert.ToString(dr0["CLIENT_EMAIL"]);
                            ClientAddress = Convert.ToString(dr0["CLIENT_ADDRESS"]);
                            ClientContact = Convert.ToString(dr0["CLIENT_CONTACT_DETAILS"]);

                            txtJobNoInt.Text = Convert.ToString(lblJOBNo.Text).Trim();
                            txtVendorName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                            txtVendorLocation.Text = ClientAddress;
                            txtVendorEmail.Text = ClientEmail;
                            txtVendorContactInt.Text = ClientContact;
                        }
                        else
                        {
                            txtJobNoInt.Text = Convert.ToString(lblJOBNo.Text).Trim();
                            txtVendorName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                            //txtVendorLocation.Text = sb.ToString().Trim();
                            //txtVendorEmail.Text = Convert.ToString(lblEmail2.Text).Trim();
                            //txtVendorContactInt.Text = Convert.ToString(lblPhone2.Text).Trim();

                        }
                    }


                }


                //txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                //txtCustomerCode.Text = Convert.ToString(lblCustCode.Text).Trim();



            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }


    public class Truck
    {
        public string TruckName { get; set; }
    }


    private void BindTruckData()
    {

        DataTable dt = new DataTable();
        dt = InwardOutward.GetTruckDetails();


        rptTrucks.DataSource = dt;
        rptTrucks.DataBind();
    }

    public class Container
    {
        public string ContainerName { get; set; }
    }

    private void GetContainersDetails()
    {

        DataTable dt = new DataTable();
        dt = InwardOutward.GetContainersDetails();


        rptContainer.DataSource = dt;
        rptContainer.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (rblForms.SelectedValue == "1")//Domestic I
            {
                if (!string.IsNullOrEmpty(txtJOBNo.Text))
                    jobNo = txtJOBNo.Text;
                else
                    jobNo = string.Empty;

                if (!string.IsNullOrEmpty(txtVenName2.Text))
                    VendorName = txtVenName2.Text;
                else
                    VendorName = string.Empty;

                if (!string.IsNullOrEmpty(txtVenLoc2.Text))
                    VendorLocation = txtVenLoc2.Text;
                else
                    VendorLocation = string.Empty;

                if (!string.IsNullOrEmpty(txtVenEmail2.Text))
                    VendorEmail = txtVenEmail2.Text;
                else
                    VendorEmail = string.Empty;

                if (!string.IsNullOrEmpty(txtVenContact.Text))
                    VendorContact = txtVenContact.Text;
                else
                    VendorContact = string.Empty;


                //if (ddlDeliveryLoc.SelectedIndex > 0)
                //    deliveryLocation = ddlDeliveryLoc.SelectedValue;
                //else
                //    deliveryLocation = "";

                if(txtothersPickupLocD.Text != "")
                {
                    otherPickupLocation = txtothersPickupLocD.Text;
                }
                else
                {
                    otherPickupLocation = "";
                }

                if(chkMatPickupLocSameAsAbove.Checked == true && txtMatPickupLoc.Text != "")
                {
                    isClientDeliveryAddressOtherThanAbove = 1; 
                }
                else
                {
                    isClientDeliveryAddressOtherThanAbove = 0;
                }

                if(txtMatPickupLoc.Text != "") {

                    clientOtherDelLocDiff = txtMatPickupLoc.Text;
                }
                else
                {
                    clientOtherDelLocDiff = "";
                }

                if (ddlDeliveryLoc.SelectedIndex >= 0)
                    deliveryLocation = ddlDeliveryLoc.SelectedItem.Text;
                else
                    deliveryLocation = "";


                if (txtVendornameVenPickup.Text != "")
                {
                    txtVendorNamePickup = txtVendornameVenPickup.Text;
                }

                if (txtVendAddressVenPickup.Text != "")
                {
                    txtVendAddressPickup = txtVendAddressVenPickup.Text;
                }

                if (txtVenContactDetailsPickup.Text != "")
                {
                    txtVendContactPickup = txtVenContactDetailsPickup.Text;
                }


                //if (ddlIncoterms2.SelectedIndex > 0)
                //    incoTerms = ddlIncoterms2.SelectedValue;
                //else
                //    incoTerms = "";

                if (ddlDeliveryTerm2.SelectedIndex > 0)
                    deliveryTerm = ddlDeliveryTerm2.SelectedValue;
                else
                    deliveryTerm = "";

                //if (!string.IsNullOrEmpty(txtIsNoOfTrucksReq.Text))
                //    noOfTruck = txtIsNoOfTrucksReq.Text;
                //else
                //    noOfTruck = string.Empty;

                int noOfTruckDom = 0;

                if (!string.IsNullOrEmpty(txtIsNoOfTrucksReq.Text))
                {
                    int parsedValueTruck = Convert.ToInt32(txtIsNoOfTrucksReq.Text);
                    noOfTruckDom = parsedValueTruck; // Assign the parsed integer value
                }

                if (!string.IsNullOrEmpty(txtDate.Text))
                    datePickup = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
                else
                    datePickup = string.Empty;

                if (ddlTypeOfConsignment2.SelectedIndex > 0)
                    typeOfConsignment = ddlTypeOfConsignment2.SelectedValue;
                else
                    typeOfConsignment = "";


                Byte[] packingListNameBytes = null;
                Byte[] attachment2Bytes = null;

                if (fileUploadPackingList.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUploadPackingList.PostedFile.FileName))
                    {
                        packingListName = fileUploadPackingList.PostedFile.FileName;
                        packingListNameBytes = GetFileBytes(fileUploadPackingList.PostedFile.FileName, fileUploadPackingList.PostedFile.InputStream);
                    }
                    else
                    {
                        packingListName = string.Empty;
                        packingListNameBytes = null;
                    }
                }
                else
                {
                    packingListName = string.Empty;
                    packingListNameBytes = null;
                }

                if (fileUploadInvoiceInstruction.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUploadInvoiceInstruction.PostedFile.FileName))
                    {
                        packingListName2 = fileUploadInvoiceInstruction.PostedFile.FileName;
                        attachment2Bytes = GetFileBytes(fileUploadInvoiceInstruction.PostedFile.FileName, fileUploadInvoiceInstruction.PostedFile.InputStream);
                    }
                    else
                    {
                        packingListName2 = string.Empty;
                        attachment2Bytes = null;
                    }
                }
                else
                {
                    packingListName2 = string.Empty;
                    attachment2Bytes = null;
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Define truck quantity variables
                int qtyTruck14 = 0, qtyTruck17 = 0, qtyTruck19 = 0, qtyTruck22 = 0, qtyDContainer40 = 0;
                int qtyTruck24 = 0, qtyTruck32 = 0, qtyTruck40 = 0, qtyLowBed = 0, qtyODC = 0, qtyDContainer20 = 0;
                int quantity = 0;

                // Iterate through each item in the Repeater
                foreach (RepeaterItem item in rptTrucks.Items)
                {
                    HiddenField hfTruckType = (HiddenField)item.FindControl("hfTruckType");
                    TextBox txtQty = (TextBox)item.FindControl("txtQty");

                    if (hfTruckType != null && txtQty != null)
                    {
                        string truckType = hfTruckType.Value.Trim();
                        //int quantity = 0;
                        int.TryParse(txtQty.Text, out quantity); // Convert input to integer safely

                        // Assign quantity to the correct truck type variable
                        switch (truckType)
                        {
                            case "TRUCK 14'": qtyTruck14 = quantity; break;
                            case "TRUCK 17'": qtyTruck17 = quantity; break;
                            case "TRUCK 19'": qtyTruck19 = quantity; break;
                            case "TRUCK 22'": qtyTruck22 = quantity; break;
                            case "TRUCK 24'": qtyTruck24 = quantity; break;
                            case "TRUCK 32'": qtyTruck32 = quantity; break;
                            case "TRUCK 40'": qtyTruck40 = quantity; break;
                            case "LOW BED": qtyLowBed = quantity; break;
                            case "ODC": qtyODC = quantity; break;
                            case "CONT 20'": qtyDContainer20 = quantity; break;
                            case "CONT 40'": qtyDContainer40 = quantity; break;

                        }
                    }
                }

                if (txtRemarksDomestic.Text != "")
                {
                    createdRemarksDomestic = txtRemarksDomestic.Text;
                }
                else
                {
                    createdRemarksDomestic = "";
                }

                string modeOfTransport = string.Empty;

                byte[] testBytes1 = new byte[0];
                byte[] testBytes2 = new byte[0];


                //int created_by = (Session["EMP_RECORD_ID"]);
          

                int value = InwardOutward.InsertOutwardInformation(0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]), jobNo, VendorName,
                                     VendorLocation, VendorEmail, VendorContact,
                                     deliveryLocation,
                                     txtVendorNamePickup, txtVendAddressPickup,
                                     txtVendContactPickup,
                                     incoTerms, deliveryTerm, noOfTruckDom,
                                     datePickup, typeOfConsignment,
                                     packingListName,packingListName2,
                                     packingListNameBytes, attachment2Bytes,
                                     "", 0 , 0 , qtyTruck14, qtyTruck17, qtyTruck19, qtyTruck22,
                                     qtyTruck24, qtyTruck32, qtyTruck40, qtyLowBed, qtyODC, qtyDContainer20, qtyDContainer40,
                                     0, 0, 0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", "", "", testBytes1 , "", "", "", testBytes2,createdRemarksDomestic ,
                                     otherPickupLocation,isClientDeliveryAddressOtherThanAbove,
                                     clientOtherDelLocDiff
                                     );
                                  

                SuccessMessageDomestic("Domestic Outward Request No. 'OUT" + value + "' added successfully and mail sent.");
                Reset1();


                if (value > 0)
                {
                    InwardOutwardSendMail inout = new InwardOutwardSendMail();
                    //int mailSentValue = tsm.SendMail(value);
                    string mailSentValue = inout.SendOutwardMail(value);



                }
                else
                {
                    ExceptionMessageDomestic("Please try again!");
                }

            }
            else if (rblForms.SelectedValue == "2")  // International Inward
            {
                if (!string.IsNullOrEmpty(txtJobNoInt.Text))
                    jobNo = txtJobNoInt.Text;
                else
                    jobNo = string.Empty;

                if (!string.IsNullOrEmpty(txtVendorName.Text))
                    VendorName = txtVendorName.Text;
                else
                    VendorName = string.Empty;

                if (!string.IsNullOrEmpty(txtVendorLocation.Text))
                    VendorLocation = txtVendorLocation.Text;
                else
                    VendorLocation = string.Empty;

                if (!string.IsNullOrEmpty(txtVendorEmail.Text))
                    VendorEmail = txtVendorEmail.Text;
                else
                    VendorEmail = string.Empty;

                if (!string.IsNullOrEmpty(txtVendorContactInt.Text))
                    VendorContact = txtVendorContactInt.Text;
                else
                    VendorContact = string.Empty;

                if (ddlVenLoc.SelectedIndex >= 0)
                    deliveryLocation = ddlVenLoc.SelectedItem.Text;
                else
                    deliveryLocation = "";


                if (txtVendorNameInt.Text != "")
                {
                    txtVendorNamePickup = txtVendorNameInt.Text;
                }

                if (txtVendorAddressInt.Text != "")
                {
                    txtVendAddressPickup = txtVendorAddressInt.Text;
                }

                if (txtContactDetailsVenPickup.Text != "")
                {
                    txtVendContactPickup = txtContactDetailsVenPickup.Text;
                }


                if (ddlInco2.SelectedIndex > 0)
                    incoTerms = ddlInco2.SelectedValue;
                else
                    incoTerms = "";

                if (ddlDeliveryTerm2.SelectedIndex > 0)
                    deliveryTerm = ddlDeliveryTerm2.SelectedValue;
                else
                    deliveryTerm = "";

                string modeOfTransport = string.Empty;

                if (ddlModeOfTransport.SelectedIndex > 0)
                    modeOfTransport = ddlModeOfTransport.SelectedValue;
                else
                    modeOfTransport = "";


                if (ddlIsStuffing.SelectedIndex > 0)
                    //isConStuffingPossible = Convert.ToInt32(ddlIsStuffing.SelectedValue);
                    isConStuffingPossible = Convert.ToInt32(ddlIsStuffing.SelectedIndex);
                else
                    isConStuffingPossible = 0;


                //if (!string.IsNullOrEmpty(txtIsNoOfContainersRequired.Text))
                //    noOfContainer = txtIsNoOfContainersRequired.Text;
                //else
                //    noOfContainer = string.Empty;



                int noOfContainer = 0; // Ensure it remains an integer

                if (!string.IsNullOrEmpty(txtIsNoOfContainersRequired.Text))
                {
                    int parsedValue = Convert.ToInt32(txtIsNoOfContainersRequired.Text);
                    noOfContainer = parsedValue; // Assign the parsed integer value
                }


                if (!string.IsNullOrEmpty(txtNoOfTrucks.Text))
                    noOfTruck = txtNoOfTrucks.Text;
                else
                    noOfTruck = string.Empty;



                if (!string.IsNullOrEmpty(txtStartDate.Text))
                    datePickup = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
                else
                    datePickup = string.Empty;


//                hdStartDate


                if (ddlTypeConsignment.SelectedIndex > 0)
                    typeOfConsignment = ddlTypeConsignment.SelectedValue;
                else
                    typeOfConsignment = "";


                Byte[] packingListNameBytes = null;
                Byte[] attachment2Bytes = null;

                if (fileUpload1.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUpload1.PostedFile.FileName))
                    {
                        packingListName = fileUpload1.PostedFile.FileName;
                        packingListNameBytes = GetFileBytes(fileUpload1.PostedFile.FileName, fileUpload1.PostedFile.InputStream);
                    }
                    else
                    {
                        packingListName = string.Empty;
                        packingListNameBytes = null;
                    }
                }
                else
                {
                    packingListName = string.Empty;
                    packingListNameBytes = null;
                }

                if (fileUpload2.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUpload2.PostedFile.FileName))
                    {
                        packingListName2 = fileUpload2.PostedFile.FileName;
                        attachment2Bytes = GetFileBytes(fileUpload2.PostedFile.FileName, fileUpload2.PostedFile.InputStream);
                    }
                    else
                    {
                        packingListName2 = string.Empty;
                        attachment2Bytes = null;
                    }
                }
                else
                {
                    packingListName2 = string.Empty;
                    attachment2Bytes = null;
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Define truck quantity variables
                int qtyContainer20 = 0, qtyContainer40 = 0, qtyContainer40HC = 0, qtyContainerFR = 0,
                qtyINODC = 0;

                // Iterate through each item in the Repeater
                foreach (RepeaterItem item in rptContainer.Items)
                {
                    HiddenField hfContainerType = (HiddenField)item.FindControl("hfContainerType");
                    TextBox txtQtyContainer = (TextBox)item.FindControl("txtQtyContainer");

                    if (hfContainerType != null && txtQtyContainer != null)
                    {
                        string containerType = hfContainerType.Value.Trim();
                        int quantity = 0;
                        int.TryParse(txtQtyContainer.Text, out quantity); // Convert input to integer safely

                        // Assign quantity to the correct truck type variable
                        switch (containerType)
                        {
                            case "20'": qtyContainer20 = quantity; break;
                            case "40'": qtyContainer40 = quantity; break;
                            case "40'HC": qtyContainer40HC = quantity; break;
                            case "FR": qtyINODC = quantity; break;
                            case "FR ODC": qtyContainerFR = quantity; break;


                        }
                    }
                }

                if (txtRemarksInternational.Text != "")
                {
                    createdremarksInt = txtRemarksInternational.Text;
                }
                else
                {
                    createdremarksInt = "";
                }

                byte[] testBytes1 = new byte[0];
                byte[] testBytes2 = new byte[0];

                if (txtOthersInt.Text != "")
                {
                    otherPickupLocation = txtOthersInt.Text;
                }
                else
                {
                    otherPickupLocation = "";
                }

                if (chkClientDelAddress.Checked == true && txtClientAddress.Text != "")
                {
                    isClientDeliveryAddressOtherThanAbove = 1;
                }
                else
                {
                    isClientDeliveryAddressOtherThanAbove = 0;
                }

                if (txtClientAddress.Text != "")
                {

                    clientOtherDelLocDiff = txtClientAddress.Text;
                }
                else
                {
                    clientOtherDelLocDiff = "";
                }

                int value = InwardOutward.InsertOutwardInformation(0, 1, Convert.ToInt32(Session["EMP_RECORD_ID"]), jobNo, VendorName,
                                    VendorLocation, VendorEmail, VendorContact,
                                    deliveryLocation,
                                    txtVendorNamePickup, txtVendAddressPickup,
                                    txtVendContactPickup,
                                    incoTerms, deliveryTerm, 0,
                                    datePickup, typeOfConsignment,
                                    packingListName,packingListName2,
                                    packingListNameBytes, attachment2Bytes,
                                    modeOfTransport, isConStuffingPossible, noOfContainer, 0, 0, 0, 0,
                                    0, 0, 0, 0, qtyINODC,0,0,
                                    qtyContainer20, qtyContainer40, qtyContainer40HC, qtyContainerFR,
                                    Convert.ToInt32(Session["EMP_RECORD_ID"]), "", "", "", testBytes1, "", "", "", testBytes2, createdremarksInt,
                                    otherPickupLocation, isClientDeliveryAddressOtherThanAbove, clientOtherDelLocDiff);

                SuccessMessageInternational("International Outward Request No. 'OUT" + value + "' added successfully and mail sent.");
                Reset2();

                if (value > 0)
                {
                    InwardOutwardSendMail tsm = new InwardOutwardSendMail();
                    string mailSentValue = tsm.SendOutwardMail(value);

                    //if (!string.IsNullOrEmpty(mailSentValue))
                    //{
                    //    int val = objTourAndTravels.UpdateTourInfoMailStatus(1, value, 0);
                    //    SuccessMessage("Tour No. '" + mailSentValue + "' added successfully and mail sent.");
                    //}
                    //else
                    //{
                    //    SuccessMessage("Tour No. '" + mailSentValue + "' added successfully, and resend an approval email from Tour Informatin List.");
                    //}

                    SuccessMessageInternational("International Inward Request No. 'OUT" + value + "' added successfully and mail sent.");
                    //Reset2();

                }
                else
                {
                    ExceptionMessageInternational("Please try again!");
                }

            }



        }
        catch (Exception ex)
        {

        }
    }

    private void Reset2()
    {
        txtJobNoInt.Text = string.Empty;
        //TextBox1.Text = string.Empty;
        txtVendorName.Text = string.Empty;
        txtVendorLocation.Text = string.Empty;
        txtVendorEmail.Text = string.Empty;
        txtVendorContactInt.Text = string.Empty;
        ddlVenLoc.SelectedIndex = 0;
        txtVendorNameInt.Text = string.Empty;
        txtVendorAddressInt.Text = string.Empty;
        txtContactDetailsVenPickup.Text = string.Empty;
        ddlInco2.SelectedIndex = 0;
        ddlModeOfTransport.SelectedIndex = 0;
        ddlIsStuffing.SelectedIndex = 0;
        txtIsNoOfContainersRequired.Text = string.Empty;
        hdnTotalContainers.Value = "";
        txtNoOfTrucks.Text = string.Empty;
        hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtStartDate.Text = hdStartDate.Value;
        ddlTypeConsignment.SelectedIndex = 0;
        txtRemarksInternational.Text = string.Empty;
        txtClientAddress.Text = string.Empty;
        chkClientDelAddress.Checked = false;

        foreach (RepeaterItem item in rptContainer.Items)
        {
            TextBox txtQtyContainer = item.FindControl("txtQtyContainer") as TextBox;
            if (txtQtyContainer != null)
            {
                txtQtyContainer.Text = string.Empty;
            }

            HiddenField hfTruckType = item.FindControl("hfContainerType") as HiddenField;
            if (hfTruckType != null)
            {
                hfTruckType.Value = string.Empty;
            }
        }
    }
    private void Reset1()
    {
        txtJOBNo.Text = string.Empty;
        txtVenName2.Text = string.Empty;
        txtVenEmail2.Text = string.Empty;
        txtVenLoc2.Text = string.Empty;
        txtVenEmail2.Text = string.Empty;
        txtVenContact.Text = string.Empty;
        ddlDeliveryLoc.SelectedIndex = 0;

        txtVendornameVenPickup.Text = string.Empty;
        txtVendAddressVenPickup.Text = string.Empty;
        txtVenContactDetailsPickup.Text = string.Empty;

        //ddlIncoterms2.SelectedIndex = 0;
        ddlDeliveryTerm2.SelectedIndex = 0;
        txtIsNoOfTrucksReq.Text = string.Empty;
        hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtDate.Text = hdDate.Value;
        ddlTypeOfConsignment2.SelectedIndex = 0;

        ddlPartLoadDetails.SelectedIndex = 0;
        txtRemarksDomestic.Text = string.Empty;
        chkMatPickupLocSameAsAbove.Checked = false;
        txtMatPickupLoc.Text = string.Empty;

        foreach (RepeaterItem item in rptTrucks.Items)
        {
            TextBox txtQty = item.FindControl("txtQty") as TextBox;
            if (txtQty != null)
            {
                txtQty.Text = string.Empty;
            }

            HiddenField hfTruckType = item.FindControl("hfTruckType") as HiddenField;
            if (hfTruckType != null)
            {
                hfTruckType.Value = string.Empty;
            }

        }
    }

   
    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                //ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }
   

    protected void gvVendDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {

                int rowindex0 = 0;
                //btnAddApprover.Visible = false;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex0 = rowSelect.RowIndex;

                Label lblUnit = GridView1.Rows[rowindex0].FindControl("lblUnit") as Label;
                Label lblVendCode = GridView1.Rows[rowindex0].FindControl("lblVendCode") as Label;
                Label lblName = GridView1.Rows[rowindex0].FindControl("lblName") as Label;
                Label lblCity = GridView1.Rows[rowindex0].FindControl("lblCity") as Label;
                Label lblState = GridView1.Rows[rowindex0].FindControl("lblState") as Label;
                Label lblCountry = GridView1.Rows[rowindex0].FindControl("lblCountry") as Label;
                Label lblPin = GridView1.Rows[rowindex0].FindControl("lblPin") as Label;
                Label lblEmail = GridView1.Rows[rowindex0].FindControl("lblEmail") as Label;
                Label lblMob = GridView1.Rows[rowindex0].FindControl("lblMob") as Label;
                Label lblAdd1 = GridView1.Rows[rowindex0].FindControl("lblAddress1") as Label;
                Label lblAdd2 = GridView1.Rows[rowindex0].FindControl("lblAddress2") as Label;
                Label lblAdd = GridView1.Rows[rowindex0].FindControl("lblAddress0") as Label;


                //txtVenName2.Text = Convert.ToString(lblName.Text).Trim();
                ////txtVenLoc2.Text = Convert.ToString(lblCity.Text).Trim();
                //txtVenEmail2.Text = Convert.ToString(lblEmail.Text).Trim();
                //txtVenContact.Text = Convert.ToString(lblMob.Text).Trim();
                ////txtVenLoc2.Text = $"{(lblAdd1?.Text ?? "").Trim()} {(lblAdd2?.Text ?? "").Trim()} {(lblAdd?.Text ?? "").Trim()}".Trim();


                StringBuilder sb = new StringBuilder();

                if (lblAdd1 != null && lblAdd1.Text != null && lblAdd1.Text.Trim() != "")
                    sb.Append(lblAdd1.Text.Trim() + " ");

                if (lblAdd2 != null && lblAdd2.Text != null && lblAdd2.Text.Trim() != "")
                    sb.Append(lblAdd2.Text.Trim() + " ");

                if (lblAdd != null && lblAdd.Text != null && lblAdd.Text.Trim() != "")
                    sb.Append(lblAdd.Text.Trim() + " ");

                if (ddlDeliveryLoc.SelectedValue == "Vendor location" && rblForms.SelectedValue == "1")
                {
                    txtVendornameVenPickup.Text = Convert.ToString(lblName.Text).Trim();
                    txtVendAddressVenPickup.Text = Convert.ToString(lblAdd1.Text).Trim();
                    //txtContactDetailsVenPickup.Text = Convert.ToString(lblEmail.Text).Trim();
                    txtVenContactDetailsPickup.Text = Convert.ToString(lblEmail.Text).Trim();
                    //pnlPopupVendorDetail.Visible = false;
                    //pnlPopupJOBDetail.Visible = false;
                }
                else if(ddlVenLoc.SelectedValue == "Vendor location" && rblForms.SelectedValue == "2")
                {
                    txtVendorNameInt.Text = Convert.ToString(lblName.Text).Trim();
                    txtVendorAddressInt.Text = Convert.ToString(lblAdd1.Text).Trim();
                    txtContactDetailsVenPickup.Text = Convert.ToString(lblEmail.Text).Trim();
                    //pnlPopupVendorDetail.Visible = false;

                }


                if (rblForms.SelectedValue == "1" && ddlDeliveryLoc.SelectedValue != "Vendor location") // Domestic Outward
                {
                    // Set vendor values
                    // Set vendor values
                    txtVenName2.Text = lblName.Text.Trim();
                    txtVenEmail2.Text = lblEmail.Text.Trim();
                    txtVenContact.Text = lblMob.Text.Trim();
                    txtVenLoc2.Text = sb.ToString().Trim();
                    //pnlPopupVendorDetail.Visible = false;
                    //pnlPopupJOBDetail.Visible = false;

                    // JavaScript to hide vendor modal
                    //                string script = string.Format(@"
                    //setTimeout(function() {{
                    //    document.getElementById('{0}').value = '{1}';
                    //    document.getElementById('{2}').value = '{3}';
                    //    document.getElementById('{4}').value = '{5}';
                    //    document.getElementById('{6}').value = '{7}';

                    //    var vendorModal = $find('{8}'); if (vendorModal) vendorModal.hide();
                    //}}, 200);",
                    //        txtVenName2.ClientID, lblName.Text.Replace("'", "\\'"),
                    //        txtVenEmail2.ClientID, lblEmail.Text.Replace("'", "\\'"),
                    //        txtVenContact.ClientID, lblMob.Text.Replace("'", "\\'"),
                    //        txtVenLoc2.ClientID, sb.ToString().Replace("'", "\\'"),
                    //        "mpeVendorDetailID"
                    //    );

                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetVendorFieldsAndHideModal", script, true);

                }

                else if (rblForms.SelectedValue == "2" && ddlVenLoc.SelectedValue != "Vendor location")  // International Inward
                {
                    txtVendorName.Text = lblName.Text.Trim();
                    txtVendorEmail.Text = lblEmail.Text.Trim();
                    txtVendorContactInt.Text = lblMob.Text.Trim();
                    txtVendorLocation.Text = sb.ToString().Trim();
                    //pnlPopupVendorDetail.Visible = false;
                }


                ///////////////////////////////////////////////////////////////////////////////////////

                //txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                //txtCustomerCode.Text = Convert.ToString(lblCustCode.Text).Trim();



            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }

   

    private void HidePanel()
    {
        //upnlPopup.Visible = false;
        pnlDomestic.Visible = false;
        lblDomestic.Text = string.Empty;
        pnlInternational.Visible = false;
        lbllInternational.Text = string.Empty;
    }

   


    private void SuccessMessageDomestic(string message)
    {
        pnlDomestic.Visible = true;
        lblDomestic.Text = message;
        lblDomestic.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessageDomestic(string message)
    {
        pnlDomestic.Visible = true;
        lblDomestic.Text = message;
        lblDomestic.ForeColor = System.Drawing.Color.Red;
    }

    private void SuccessMessageInternational(string message)
    {
        pnlInternational.Visible = true;
        lbllInternational.Text = message;
        lbllInternational.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessageInternational(string message)
    {
        pnlInternational.Visible = true;
        lbllInternational.Text = message;
        lbllInternational.ForeColor = System.Drawing.Color.Red;
    }



}