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

public partial class PROJECT_InwardProduction: System.Web.UI.Page
{

    DataSet dsJobNo = new DataSet();
    DataSet dsVendorData = new DataSet();
    
    int companyID = 0;
    string modeOfTransport = string.Empty;
    int isConStuffingPossible = 0;
    string custCode = string.Empty;
    string jobNo = string.Empty;
    string  poNo = string.Empty;
    string VendorName = string.Empty;
    string VendorLocation = string.Empty;
    string typeOfConsignment = string.Empty;
    string datePickup = string.Empty;
    string deliveryLocation = string.Empty;
    string deliveryTerm = string.Empty;
    string incoTerms = string.Empty;
    string noOfTruck  = string.Empty;
    string noOfContainer = string.Empty;
    string VendorCode = string.Empty;
    string VendorEmail = string.Empty;
    string VendorContact = string.Empty;
    string packingListName = string.Empty;
    string subVendorInvoiceName = string.Empty;
    string createdRemarksDomestic = string.Empty;
    string createdremarksInt = string.Empty;
    string materialPickLocContactPersonDetails = string.Empty;

    DataSet dsUnit = new DataSet();
    DataSet dsVendor = new DataSet();
    DataSet dsIncoterms = new DataSet();

    BAL.Project objProject = new BAL.Project();
    BAL.LessonLearnt objLessonLearnt = new BAL.LessonLearnt();
    BAL.InwardOutward InwardOutward = new BAL.InwardOutward();
    InwardOutwardSendMail InOutSendMail = new InwardOutwardSendMail();

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
                BinContainerData();
                BindCompany();
                BindIncoterms();
                BindModeOfTransport();
                BindContainerStuffingPossible();
                BindDomesticDeliveryTerm();
                BindTypeOfConsignment();
                BindNoOfTrucks();
                //BindNoOfContainers();
                pnlForm1.Visible = rblForms.SelectedValue == "1";
                pnlForm2.Visible = rblForms.SelectedValue == "2";
                //mpeJOBDetail.Show();
                //upnlPopup.Show();
                BindTruckData();
                hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                txtIsNoOfContainersRequired.Text = hdnTotalContainers.Value;
                txtIsNoOfTrucksReq.Text = hdnNoOfTruck.Value;

                int containerTotal = 0;
                if (int.TryParse(hdnTotalContainers.Value, out containerTotal))
                {
                    txtIsNoOfContainersRequired.Text = containerTotal.ToString();
                }


                int truckTotal01 = 0;

                if (int.TryParse(hdnNoOfTruck.Value, out truckTotal01))
                {
                    txtIsNoOfTrucksReq.Text = truckTotal01.ToString();
                }
                //GetInwardList();

                //pnlPopupJOBDetail.Visible = false;

                //if (pnlPopupJOBDetail.Visible)
                //{
                //    mpeJOBDetail.Show();
                //}
            }

        };

          
   }


    public void BindNoOfTrucks()
    {
        int noOfTruckDomestic = 0;
        txtIsNoOfTrucksReq.Text = noOfTruckDomestic.ToString();
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
        {
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
            ddlInco2.Items.Add(new ListItem("FCA", "FCA"));

            ddlInco2.Items.Add(new ListItem("FAS", "FAS"));
            ddlInco2.Items.Add(new ListItem("CFR", "CFR"));
            ddlInco2.Items.Add(new ListItem("CIP", "CIP"));
            ddlInco2.Items.Add(new ListItem("CPT", "CPT"));
            ddlInco2.Items.Add(new ListItem("DAP", "DAP"));
            ddlInco2.Items.Add(new ListItem("DPU", "DPU"));
            ddlInco2.Items.Add(new ListItem("DDP", "DDP"));


            ddlIncoterms2.Items.Clear();

            // Add new items
            ddlIncoterms2.Items.Add(new ListItem("Select", ""));
            ddlIncoterms2.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            ddlIncoterms2.Items.Add(new ListItem("FOT", "FOT"));
            ddlIncoterms2.Items.Add(new ListItem("Door Delivery", "Door_Delivery"));


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
        ddlDeliveryTerm2.Items.Add(new ListItem("EXW", "EXW"));
        ddlDeliveryTerm2.Items.Add(new ListItem("FCA", "FCA"));
        ddlDeliveryTerm2.Items.Add(new ListItem("FAS", "FAS"));
        ddlDeliveryTerm2.Items.Add(new ListItem("FOB", "FOB"));
        ddlDeliveryTerm2.Items.Add(new ListItem("CFR", "CFR"));
        ddlDeliveryTerm2.Items.Add(new ListItem("CIF", "CIF"));
        ddlDeliveryTerm2.Items.Add(new ListItem("CIP", "CIP"));
        ddlDeliveryTerm2.Items.Add(new ListItem("CPT", "CPT"));
        ddlDeliveryTerm2.Items.Add(new ListItem("DAP", "DAP"));
        ddlDeliveryTerm2.Items.Add(new ListItem("DPU", "DPU"));
        ddlDeliveryTerm2.Items.Add(new ListItem("DDP", "DDP"));
        ddlDeliveryTerm2.Items.Add(new ListItem("FOR", "FOR"));


    }

    private void BindTypeOfConsignment()
    {

        ddlTypeOfConsignment2.Items.Clear();
        ddlTypeOfConsignment2.Items.Add(new ListItem("Select", ""));
        ddlTypeOfConsignment2.Items.Add(new ListItem("Full Truck", "Full Truck"));
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

                //ddlVenLoc.DataSource = dsUnit.Tables[0];
                ddlVenLoc.DataSource = filteredTable;
                ddlVenLoc.DataTextField = "UNIT_NAME";
                ddlVenLoc.DataValueField = "UNIT_ID";
                ddlVenLoc.DataBind();
                //ddlCompany.Items.Insert(0, "Select");
                ddlVenLoc.SelectedIndex = 0;



                //ddlDeliveryLoc.DataSource = dsUnit.Tables[0];
                ddlDeliveryLoc.DataSource = filteredTable;
                ddlDeliveryLoc.DataTextField = "UNIT_NAME";
                ddlDeliveryLoc.DataValueField = "UNIT_ID";
                ddlDeliveryLoc.DataBind();
                //ddlCompany.Items.Insert(0, "Select");
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
        //mpeJOBDetail.Show();
        /*upnlPopup.Show();
        pnlPopupJOBDetail.Visible = true;
        GetJOBDetail();*/

        upnlPopup.Show();
        GetJOBDetail();
        //mpeVendorDetail.Hide();

        //upnlPopup.Location = new Point(150, 70);

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
        //mpeJOBDetail.Show();
        upnlPopup.Show();
        GetJOBDetail();
    }

    protected void btnSearchVendor_Click(object sender, EventArgs e)
    {
        //mpeJOBDetail.Show();
        //upnlPopup.Show();
        mpeVendorDetail.Show();
        GetVendorDetail();
    }

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

                if(rblForms.SelectedValue == "1")//domestic
                {
                    txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
                }
                else if(rblForms.SelectedValue == "2")//international 
                {
                    TextBox1.Text = Convert.ToString(lblJOBNo.Text).Trim();
                }



                //txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
                //TextBox1.Text = Convert.ToString(lblJOBNo.Text).Trim();

                //if(txtJOBNo.Text == Convert.ToString(lblJOBNo.Text).Trim())
                //{
                //    txtJOBNo.Text = "true";
                //}
                //txtJOBNoSearch.Text = Convert.ToString(lblJOBNo.Text).Trim();
                //txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                //txtCustomerCode.Text = Convert.ToString(lblCustCode.Text).Trim();
                //mpeJOBDetail.Hide();
                //upnlPopup.Hide();

                //pnlPopupJOBDetail.Visible = false;


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
        List<Truck> truckList = new List<Truck>
    {
        new Truck { TruckName = "Truck 14'" },
        new Truck { TruckName = "Truck 17'" },
        new Truck { TruckName = "Truck 19'" },
        new Truck { TruckName = "Truck 22'" },
        new Truck { TruckName = "Truck 24'" },
        new Truck { TruckName = "Truck 32'" },
        new Truck { TruckName = "Truck 40'" },
        new Truck { TruckName = "Low Bed" },
        new Truck { TruckName = "ODC" }
    };

        rptTrucks.DataSource = truckList;
        rptTrucks.DataBind();
    }

    public class Container
    {
        public string ContainerName { get; set; }
    }

    private void BinContainerData()
    {
        List<Container> containerList = new List<Container>
    {
        new Container { ContainerName = "20'" },
        new Container { ContainerName = "40'" },
        new Container { ContainerName = "40' HC" },
        //new Container { ContainerName = "ODC"},
        //new Container { ContainerName = "FR" },
          new Container { ContainerName = "FR"},
        new Container { ContainerName = "FR ODC" },

    };

        rptContainer.DataSource = containerList;
        rptContainer.DataBind();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if(rblForms.SelectedValue == "1")//Domestic Inward
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

                if (ddlDeliveryLoc.SelectedIndex >= 0)
                    deliveryLocation = ddlDeliveryLoc.SelectedItem.Text;
                else
                    deliveryLocation = "";

                if(!string.IsNullOrEmpty(txtMatPickupLoc.Text))
                {
                    materialPickLocContactPersonDetails = txtMatPickupLoc.Text;
                }
                else
                {
                    materialPickLocContactPersonDetails = "";
                }


                if (ddlIncoterms2.SelectedIndex > 0)
                    incoTerms = ddlIncoterms2.SelectedValue;
                else
                    incoTerms = "";

                if (ddlDeliveryTerm2.SelectedIndex > 0)
                    deliveryTerm = ddlDeliveryTerm2.SelectedValue;
                else
                    deliveryTerm = "";

                if (!string.IsNullOrEmpty(txtIsNoOfTrucksReq.Text))
                    noOfTruck = txtIsNoOfTrucksReq.Text;
                else
                    noOfTruck = string.Empty;

                int noOfTruckInt = 0;

                noOfTruckInt = Convert.ToInt32(noOfTruck);

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

                if (fileUploadVisitSummary1.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUploadVisitSummary1.PostedFile.FileName))
                    {
                        packingListName = fileUploadVisitSummary1.PostedFile.FileName;
                        packingListNameBytes = GetFileBytes(fileUploadVisitSummary1.PostedFile.FileName, fileUploadVisitSummary1.PostedFile.InputStream);
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

                if (fileUpload3.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUpload3.PostedFile.FileName))
                    {
                        subVendorInvoiceName = fileUpload3.PostedFile.FileName;
                        attachment2Bytes = GetFileBytes(fileUpload3.PostedFile.FileName, fileUpload3.PostedFile.InputStream);
                    }
                    else
                    {
                        subVendorInvoiceName = string.Empty;
                        attachment2Bytes = null;
                    }
                }
                else
                {
                    subVendorInvoiceName = string.Empty;
                    attachment2Bytes = null;
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Define truck quantity variables
                int qtyTruck14 = 0, qtyTruck17 = 0, qtyTruck19 = 0, qtyTruck22 = 0;
                int qtyTruck24 = 0, qtyTruck32 = 0, qtyTruck40 = 0, qtyLowBed = 0, qtyODC = 0;

                // Iterate through each item in the Repeater
                foreach (RepeaterItem item in rptTrucks.Items)
                {
                    HiddenField hfTruckType = (HiddenField)item.FindControl("hfTruckType");
                    TextBox txtQty = (TextBox)item.FindControl("txtQty");

                    if (hfTruckType != null && txtQty != null)
                    {
                        string truckType = hfTruckType.Value.Trim();
                        int quantity = 0;
                        int.TryParse(txtQty.Text, out quantity); // Convert input to integer safely

                        // Assign quantity to the correct truck type variable
                        switch (truckType)
                        {
                            case "Truck 14'": qtyTruck14 = quantity; break;
                            case "Truck 17'": qtyTruck17 = quantity; break;
                            case "Truck 19'": qtyTruck19 = quantity; break;
                            case "Truck 22'": qtyTruck22 = quantity; break;
                            case "Truck 24'": qtyTruck24 = quantity; break;
                            case "Truck 32'": qtyTruck32 = quantity; break;
                            case "Truck 40'": qtyTruck40 = quantity; break;
                            case "Low Bed": qtyLowBed = quantity; break;
                            case "ODC": qtyODC = quantity; break;


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

                int value = InwardOutward.InsertInwardOutwardInformation(0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]), jobNo,
                                                                    VendorName, VendorLocation,
                                                                    VendorEmail, VendorContact,
                                                                    deliveryLocation, incoTerms,
                                                                    deliveryTerm, "", noOfTruckInt, datePickup, typeOfConsignment,
                                                                    packingListName,subVendorInvoiceName,
                                                                    packingListNameBytes, attachment2Bytes, "",
                                                                    0, 0,qtyTruck14, qtyTruck17, qtyTruck19, qtyTruck22, qtyTruck24,
                                                                    qtyTruck32, qtyTruck40, qtyLowBed, qtyODC, 0, 0,
                                                                    0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]), "","","", testBytes1,"","","",
                                                                    testBytes2, createdRemarksDomestic, materialPickLocContactPersonDetails);

                SuccessMessageDomestic("Domestic Inward Request No. 'IN" + value + "' added successfully and mail sent.");
                Reset1();


                if (value > 0)
                {
                    InwardOutwardSendMail inout = new InwardOutwardSendMail();
                    //int mailSentValue = tsm.SendMail(value);
                    string mailSentValue = inout.SendInwardMail(value);


                    
                }
                else
                {
                    ExceptionMessageDomestic("Please try again!");
                }


                //SuccessMessageDomestic("Domestic Inward Request No. '" + value + "' added successfully and mail sent.");
                //Reset1();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ;

            }
            else if (rblForms.SelectedValue == "2")  // International Inward
            {
                if (!string.IsNullOrEmpty(TextBox1.Text))
                    jobNo = TextBox1.Text;
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

                if (!string.IsNullOrEmpty(txtMatPickupLocINT.Text))
                {
                    materialPickLocContactPersonDetails = txtMatPickupLocINT.Text;
                }
                else
                {
                    materialPickLocContactPersonDetails = "";
                }



                if (ddlVenLoc.SelectedIndex >= 0)
                    deliveryLocation = ddlVenLoc.SelectedItem.Text;
                else
                    deliveryLocation = "";


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

                string a = txtIsNoOfContainersRequired.Text;

                int noOfContainer = 0; // Ensure it remains an integer

                if (!string.IsNullOrEmpty(txtIsNoOfContainersRequired.Text))
                {
                    noOfContainer = Convert.ToInt32(txtIsNoOfContainersRequired.Text);
                }


                if (!string.IsNullOrEmpty(txtNoOfTrucks.Text))
                    noOfTruck = txtNoOfTrucks.Text;
                else
                    noOfTruck = "0";

                int noOfTruck2 = 0;
                noOfTruck2 = Convert.ToInt32(noOfTruck);

                if (!string.IsNullOrEmpty(txtStartDate.Text))
                    datePickup = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
                else
                    datePickup = string.Empty;



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
                        subVendorInvoiceName = fileUpload2.PostedFile.FileName;
                        attachment2Bytes = GetFileBytes(fileUpload2.PostedFile.FileName, fileUpload2.PostedFile.InputStream);
                    }
                    else
                    {
                        subVendorInvoiceName = string.Empty;
                        attachment2Bytes = null;
                    }
                }
                else
                {
                    subVendorInvoiceName = string.Empty;
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
                            case "40' HC": qtyContainer40HC = quantity; break;
                            //case "ODC": qtyINODC = quantity; break;
                            //case "FR": qtyContainerFR = quantity; break;

                            case "FR": qtyINODC = quantity; break;
                            case "FR ODC": qtyContainerFR = quantity; break;

                        }
                    }
                }

                if(txtRemarksInternational.Text != "")
                {
                    createdremarksInt = txtRemarksInternational.Text;
                }
                else
                {
                    createdremarksInt = "";
                }

                

                byte[] testBytes1 = new byte[0];
                byte[] testBytes2 = new byte[0];

                int value = InwardOutward.InsertInwardOutwardInformation(0, 1, Convert.ToInt32(Session["EMP_RECORD_ID"]), jobNo,
                                                                   VendorName, VendorLocation,
                                                                   VendorEmail, VendorContact,
                                                                   deliveryLocation, incoTerms,
                                                                   deliveryTerm, "", noOfTruck2, datePickup, typeOfConsignment,
                                                                   packingListName,subVendorInvoiceName,
                                                                   packingListNameBytes, attachment2Bytes, modeOfTransport,
                                                                   0, noOfContainer, 0, 0, 0, 0,
                                                                   0, 0, 0, 0, qtyINODC, qtyContainer20,
                                                                   qtyContainer40, qtyContainer40HC, qtyContainerFR, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", "", "", testBytes1, "", "", "",
                                                                    testBytes2, createdremarksInt, materialPickLocContactPersonDetails);

                if (value > 0)
                {
                    InwardOutwardSendMail tsm = new InwardOutwardSendMail();
                    string mailSentValue = tsm.SendInwardMail(value);
                    
                    //if (!string.IsNullOrEmpty(mailSentValue))
                    //{
                    //    int val = objTourAndTravels.UpdateTourInfoMailStatus(1, value, 0);
                    //    SuccessMessage("Tour No. '" + mailSentValue + "' added successfully and mail sent.");
                    //}
                    //else
                    //{
                    //    SuccessMessage("Tour No. '" + mailSentValue + "' added successfully, and resend an approval email from Tour Informatin List.");
                    //}

                    SuccessMessageInternational("International Inward Request No. 'IN" + value + "' added successfully and mail sent.");
                    Reset2();

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


    protected void btnViewList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/InwardProductionList.aspx");   
    }

    private void Reset2()
    {
        TextBox1.Text = string.Empty;
        txtVendorName.Text = string.Empty;
        txtVendorLocation.Text = string.Empty;
        txtVendorEmail.Text = string.Empty;
        txtVendorContactInt.Text = string.Empty;
        ddlVenLoc.SelectedIndex = 0;
        ddlInco2.SelectedIndex = 0;
        ddlModeOfTransport.SelectedIndex = 0;
        ddlIsStuffing.SelectedIndex = 0;
        txtIsNoOfContainersRequired.Text = string.Empty;
        txtNoOfTrucks.Text = string.Empty;
        hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtStartDate.Text = hdStartDate.Value;
        ddlTypeConsignment.SelectedIndex = 0;
        txtRemarksInternational.Text = string.Empty;
        txtMatPickupLocINT.Text = string.Empty;
        chkMatPickupLocSameAsAboveINT.Checked = false;
        txtIsNoOfContainersRequired.Text = string.Empty;

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
        ddlIncoterms2.SelectedIndex = 0;
        ddlDeliveryTerm2.SelectedIndex = 0;
        txtIsNoOfTrucksReq.Text = string.Empty;
        hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtDate.Text = hdDate.Value;
        ddlTypeOfConsignment2.SelectedIndex = 0;
        txtRemarksDomestic.Text = string.Empty;
        txtMatPickupLoc.Text = string.Empty;
        chkMatPickupLocSameAsAbove.Checked = false;
      

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

                
                StringBuilder sb = new StringBuilder();

                if (lblAdd1 != null && lblAdd1.Text != null && lblAdd1.Text.Trim() != "")
                    sb.Append(lblAdd1.Text.Trim() + " ");

                if (lblAdd2 != null && lblAdd2.Text != null && lblAdd2.Text.Trim() != "")
                    sb.Append(lblAdd2.Text.Trim() + " ");

                if (lblAdd != null && lblAdd.Text != null && lblAdd.Text.Trim() != "")
                    sb.Append(lblAdd.Text.Trim() + " ");

                
                if (rblForms.SelectedValue == "1")  // Domestic Inward
                {
                    txtVenName2.Text = lblName.Text.Trim();
                    txtVenEmail2.Text = lblEmail.Text.Trim();
                    txtVenContact.Text = lblMob.Text.Trim();
                    txtVenLoc2.Text = sb.ToString().Trim();
                    //mpeVendorDetail.Hide();
                    //pnlPopupVendorDetail.Visible = false;
                    //pnlPopupJOBDetail.Visible = false;
                }
                else if (rblForms.SelectedValue == "2")  // International Inward
                {
                    txtVendorName.Text = lblName.Text.Trim();
                    txtVendorEmail.Text = lblEmail.Text.Trim();
                    txtVendorContactInt.Text = lblMob.Text.Trim();
                    txtVendorLocation.Text = sb.ToString().Trim();
                    //mpeVendorDetail.Hide();
                   // pnlPopupVendorDetail.Visible = false;
                }


                ///////////////////////////////////////////////////////////////////////////////////////

                
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