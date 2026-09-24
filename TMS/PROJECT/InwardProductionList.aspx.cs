using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.Xml.Linq;
using System.Threading;

public partial class PROJECT_InwardProductionList : System.Web.UI.Page
{

    BAL.InwardOutward InwardOutward = new BAL.InwardOutward();
    BAL.LessonLearnt objLessonLearnt = new BAL.LessonLearnt();
    DataSet dsEmployee = new DataSet();
    DataSet dsRequestList = new DataSet();
    DataSet dsUnit = new DataSet();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string reqNo = string.Empty;
    string jobNoSearch = string.Empty;
    int inwardCategory = 0;
    int createdByID = 0;
    int empRecordID = 0;

    string lrNo = string.Empty;
    string LRDate = string.Empty;
    string truckNo = string.Empty;
    string podattachment1FileName = string.Empty;
    string FCRBLDate = string.Empty;
    string fcrbrNo = string.Empty;
    string containerNo = string.Empty;
    string podattachment2FileName = string.Empty;

    string jobNoEdit = string.Empty;
    string vendorNameEdit = string.Empty;
    string vendorLocationEdit = string.Empty;
    string vendorEmailEdit = string.Empty;
    string vendorContactEdit = string.Empty;
    string vendorLocation = string.Empty;
    string incotermsEdit = string.Empty;
    string deliveryTermEdit = string.Empty;
    string NoOfTrucksEdit = string.Empty;
    string dateEditDomInward = string.Empty;
    string TypeOfConsignmentEdit = string.Empty;

protected void Page_Load(object sender, EventArgs e)
    {
        //BindTruckData();
        //////////////////
        //BinContainerData();
        //BindIncoterms();
        //BindIncoterms2();
        //BindCompany();
        /////////////////////////
        //BindDomesticDeliveryTermE();
        //BindTypeOfConsignment();
        //////////////////////////////////////
        //BindModeOfTransport();
        //BindContainerStuffingPossible();
        //////////////////////////////////////

        //ModalPopupExtender7.Show();

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindDomesticOrInternational();
                BindDomesticDeliveryTermE();
                BindTypeOfConsignment();
                BindEmployee();
                BindIncoterms();
                BinContainerData();
                BindMaterialDelLoc();
                GetInwardList();
                BindTruckData();
                BindCompany();
                BindModeOfTransport();
                BindContainerStuffingPossible();

                if(txtApproveRemark.Text != "")
                {
                    txtApproveRemark.Text = "";
                }
                else
                {
                    txtApproveRemark.Text = "";
                }

                txtDateFCRBL.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtLRDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtApproveRemark.Text = "";

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");


                //row1domesticC.Visible = false;
                //row2domesticC.Visible = false;
                //row1InternationalC.Visible = false;
                //row2InternationalC.Visible = false;

            }

        }
    }

    private void ClearForm()
    {
        txtApproveRemark.Text = "";
        txtLRNo.Text = "";
        txtLRNoC.Text = "";
        txtTruckNo.Text = "";
        txtTruckNoC.Text = "";
        txtFCRBLC.Text = "";
        txtFCRBL.Text = "";
        txtContainerNoC.Text = "";
        txtContainerNo.Text = "";


        txtDateFCRBL.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtLRDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

    }


    private void BindContainerStuffingPossible()
    {
        try
        {
            ddlIsStuffingEditInt.Items.Clear();
            ddlIsStuffingEditInt.Items.Add(new ListItem("Select", ""));
            ddlIsStuffingEditInt.Items.Add(new ListItem("Yes", "Yes"));
            ddlIsStuffingEditInt.Items.Add(new ListItem("No", "No"));

        }
        catch (Exception ex)
        {
            return;
        }
    }


    private void BindModeOfTransport()
    {
        try
        {
            ddlModeOfTransportIntEdit.Items.Clear();
            ddlModeOfTransportIntEdit.Items.Add(new ListItem("Select", ""));
            ddlModeOfTransportIntEdit.Items.Add(new ListItem("Air", "Air"));
            ddlModeOfTransportIntEdit.Items.Add(new ListItem("Sea", "Sea"));


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindTypeOfConsignment()
    {
        //ddlTypeOfConsignmentEdit.Items.Clear();
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Select", ""));
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Full Truck", "Full Truck"));
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Part Load", "Part Load"));

        ddlTypeConsignmentEditInt.Items.Clear();
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Select", ""));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Full Consignment - FCL", "Full Consignment - FCL"));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Part Load - LCL", "Part Load - LCL"));
    }

    private void BindTypeOfConsignmentIntEditRejectDomestic()
    {

        ddlTypeConsignmentEditInt.Items.Clear();
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Select", ""));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Full Truck", "Full Truck"));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Part Load", "Part Load"));

        ddlTypeOfConsignmentEdit.Items.Clear();
        ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Select", ""));
        ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Full Truck", "Full Truck"));
        ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Part Load", "Part Load"));

        
    }


    private void BindIncoterms()
    {
        try
        {
            ddlIncotermsIntEdit.Items.Clear();

            ddlIncotermsIntEdit.Items.Clear();
            ddlIncotermsIntEdit.Items.Add(new ListItem("Select", ""));
            ddlIncotermsIntEdit.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("FOB", "FOB"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CIF", "CIF"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("C&F", "C&F"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("FCA", "FCA"));

            ddlIncotermsIntEdit.Items.Add(new ListItem("FAS", "FAS"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CFR", "CFR"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CIP", "CIP"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CPT", "CPT"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("DAP", "DAP"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("DPU", "DPU"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("DDP", "DDP"));

            DropDownList12.Items.Clear();


            DropDownList12.Items.Add(new ListItem("Select", ""));
            DropDownList12.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            DropDownList12.Items.Add(new ListItem("FOB", "FOT"));
            DropDownList12.Items.Add(new ListItem("CIF", "CIF"));
            DropDownList12.Items.Add(new ListItem("C&F", "C&F"));
            DropDownList12.Items.Add(new ListItem("FCA", "FCA"));

            DropDownList12.Items.Add(new ListItem("FAS", "FAS"));
            DropDownList12.Items.Add(new ListItem("CFR", "CFR"));
            DropDownList12.Items.Add(new ListItem("CIP", "CIP"));
            DropDownList12.Items.Add(new ListItem("CPT", "CPT"));
            DropDownList12.Items.Add(new ListItem("DAP", "DAP"));
            DropDownList12.Items.Add(new ListItem("DPU", "DPU"));
            DropDownList12.Items.Add(new ListItem("DDP", "DDP"));


            DropDownList12.Items.Clear();

            // Add new items
            DropDownList12.Items.Add(new ListItem("Select", ""));
            DropDownList12.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            DropDownList12.Items.Add(new ListItem("FOT", "FOT"));
            DropDownList12.Items.Add(new ListItem("Door Delivery", "Door_Delivery"));

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindIncoterms2()
    {
        try
        {
            ddlIncotermsIntEdit.Items.Clear();

            ddlIncotermsIntEdit.Items.Clear();
            ddlIncotermsIntEdit.Items.Add(new ListItem("Select", ""));
            ddlIncotermsIntEdit.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("FOB", "FOT"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CIF", "CIF"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("C&F", "C&F"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("FCA", "FCA"));

            ddlIncotermsIntEdit.Items.Add(new ListItem("FAS", "FAS"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CFR", "CFR"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CIP", "CIP"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CPT", "CPT"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("DAP", "DAP"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("DPU", "DPU"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("DDP", "DDP"));
        }
        catch (Exception ex)
        {
           ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEmployee()
    {
        try
        {
            dsEmployee = InwardOutward.GetEmployeeForInwardList();

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                //ddlEmployee.DataSource = dsEmployee.Tables[0];
                //ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                //ddlEmployee.DataValueField = "EMP_RECORD_ID";
                //ddlEmployee.DataBind();
                //ddlEmployee.Items.Insert(0, "Select");
                //ddlEmployee.SelectedIndex = 0;
                //ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);


                ddlEmployeeName.DataSource = dsEmployee.Tables[0];
                ddlEmployeeName.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeName.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, "Select");
                ddlEmployeeName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindMaterialDelLoc()
    {
        try
        {

            dsUnit = objLessonLearnt.Unit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlMaterialDelLocation.DataSource = dsUnit.Tables[0];
                ddlMaterialDelLocation.DataTextField = "UNIT_NAME";
                ddlMaterialDelLocation.DataValueField = "UNIT_NAME";
                ddlMaterialDelLocation.DataBind();
                //ddlCompany.Items.Insert(0, "Select");
                ddlMaterialDelLocation.SelectedIndex = 0;

                //ddlDeliveryLocIO.DataSource = dsUnit.Tables[0];
                //ddlDeliveryLocIO.DataTextField = "UNIT_NAME";
                //ddlDeliveryLocIO.DataValueField = "UNIT_ID";
                //ddlDeliveryLocIO.DataBind();
                ////ddlCompany.Items.Insert(0, "Select");
                //ddlDeliveryLocIO.SelectedIndex = 0;



                ddlDeliveryLocIO.DataSource = dsUnit.Tables[0];
                ddlDeliveryLocIO.DataTextField = "UNIT_NAME";
                ddlDeliveryLocIO.DataValueField = "UNIT_NAME";
                ddlDeliveryLocIO.DataBind();
                // Set default selection (optional)
                if (ddlDeliveryLocIO.Items.Count > 0)
                {
                    //ddlDeliveryLocIO.SelectedIndex = 0; 
                    ddlDeliveryLocIO.SelectedValue = "A35";
                }





                ddlMatDelLocIntEdit.DataSource = dsUnit.Tables[0];
                ddlMatDelLocIntEdit.DataTextField = "UNIT_NAME";
                ddlMatDelLocIntEdit.DataValueField = "UNIT_NAME";
                ddlMatDelLocIntEdit.DataBind();
                if (ddlMatDelLocIntEdit.Items.Count > 0)
                {
                    //ddlMatDelLocIntEdit.SelectedIndex = 0; 
                    ddlMatDelLocIntEdit.SelectedValue = "A35";
                }


                ddlVenLoc.DataSource = dsUnit.Tables[0];
                ddlVenLoc.DataTextField = "UNIT_NAME";
                ddlVenLoc.DataValueField = "UNIT_NAME";
                ddlVenLoc.DataBind();
                // Set default selection (optional)
                if (ddlVenLoc.Items.Count > 0)
                {
                    ddlVenLoc.SelectedIndex = 0; // OR use SelectedValue, not both
                                                 // ddlVenLoc.SelectedValue = "someValue"; // Use this if you want to select a specific item
                }
                //ddlVenLoc.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDomesticOrInternational()
    {
        try
        {

            ddlDomInt.Items.Clear(); // Clear existing items
            ddlDomInt.Items.Add(new ListItem("Both", "0")); // Default option
            ddlDomInt.Items.Add(new ListItem("Domestic", "1"));
            ddlDomInt.Items.Add(new ListItem("International", "2"));

            ddlDomInt.SelectedValue = "0"; // Ensure "Both" is selected

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        pnlMsg.Visible = false;
        GetInwardList();

    }


    
    private void GetInwardList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtReqNo.Text))
                reqNo = txtReqNo.Text.Trim();
            else
                reqNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNoSearch = txtJobNo.Text.Trim();
            else
                jobNoSearch = string.Empty;

            if (!string.IsNullOrEmpty(ddlDomInt.SelectedValue))
            {
                inwardCategory = Convert.ToInt32(ddlDomInt.SelectedValue);
            }
            else
            {
                inwardCategory = 0;
            }



            if (ddlEmployeeName.SelectedIndex > 0)
            {
                createdByID = Convert.ToInt32(ddlEmployeeName.SelectedValue);
            }
            else
            {
                createdByID = 0;

            }

            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsRequestList = InwardOutward.GetInwardList(startDate, endDate, reqNo, jobNoSearch, inwardCategory,
                                                        empRecordID, createdByID);
            if (dsRequestList.Tables.Count > 0 && dsRequestList.Tables[0].Rows.Count > 0)
            {
                Session["INWARD_LIST"] = dsRequestList.Tables[0];
                gvInwardList.DataSource = dsRequestList.Tables[0];
                gvInwardList.DataBind();
            }
            else
            {
                Session["INWARD_LIST"] = null;
                gvInwardList.DataSource = null;
                gvInwardList.DataBind();
            }
            lblRecords.Text = "Records[" + dsRequestList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)EnumInwardOutward.EnumINOUTStatus.New)
        {
           
            UpdateInwardInformation();
            ClearForm();
        }
        else if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
        {
            ConfirmInward();
            ClearForm();
        }
        else
        {
            UpdateInwardStatus();
            ClearForm();
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
                ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }


    private void UpdateInwardInformation()
    {
        try
        {
            int reqID = Convert.ToInt32(ViewState["INWARD_ID"]);
            //int reqID = Convert.ToInt32(Session["REQ_ID"]);
            string reqNo = Convert.ToString(ViewState["INWARD_NO"]);
            int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            string typeOfInward = Convert.ToString(ViewState["TYPE_OF_INWARD"]);
            int value = 0;

            if (typeOfInward == "INTERNATIONAL")
            {
                lblDateMsg.Visible = false;
                lblDateMsg.Text = string.Empty;

                if (txtJobIntEdit.Text != "")
                {
                    jobNoEdit = Convert.ToString(txtJobIntEdit.Text);
                }
                if (txtVendNameIntEdit.Text != "")
                {
                    vendorNameEdit = Convert.ToString(txtVendNameIntEdit.Text);
                }
                if (txtVendLocIntEdit.Text != "")
                {
                    vendorLocationEdit = Convert.ToString(txtVendLocIntEdit.Text);
                }
                if (txtVendEmailIntEdit.Text != "")
                {
                    vendorEmailEdit = Convert.ToString(txtVendEmailIntEdit.Text);
                }
                if (txtVendContactIntEdit.Text != "")
                {
                    vendorContactEdit = Convert.ToString(txtVendContactIntEdit.Text);
                }

                if (ddlMatDelLocIntEdit.SelectedIndex > 0)
                    vendorLocation = Convert.ToString(ddlMatDelLocIntEdit.SelectedValue);
                else
                    vendorLocation = "";

                if (ddlIncotermsIntEdit.SelectedIndex > 0)
                    incotermsEdit = Convert.ToString(ddlIncotermsIntEdit.SelectedValue);
                else
                    incotermsEdit = "";

                string modeOfTransportIntEdit = string.Empty;
                string isIntInwardStuffingPossible = string.Empty;
                string NoOfContainersEdit = string.Empty;
                string NoOfTrucksIntEdit = string.Empty;
                string dateEditIntInward = string.Empty;
                string typeOfConsignmentEditInt = string.Empty;

                if (ddlModeOfTransportIntEdit.SelectedIndex > 0)
                    modeOfTransportIntEdit = Convert.ToString(ddlModeOfTransportIntEdit.SelectedValue);
                else
                    modeOfTransportIntEdit = "";


                if (ddlIsStuffingEditInt.SelectedIndex > 0)
                    isIntInwardStuffingPossible = Convert.ToString(ddlIsStuffingEditInt.SelectedValue);
                else
                    isIntInwardStuffingPossible = "";


                NoOfContainersEdit = Convert.ToString(txtNoContainersEditInt.Text);
                NoOfTrucksIntEdit = Convert.ToString(txtNoOfTruckIntEdit.Text);

                //dateEditIntInward = Convert.ToString(TextBox15.Text);
                if (!string.IsNullOrEmpty(HiddenField3.Value))
                    dateEditIntInward = Convert.ToDateTime(HiddenField3.Value).ToString("yyyy-MM-dd");
                else
                    dateEditIntInward = string.Empty;


                if (ddlTypeConsignmentEditInt.SelectedIndex > 0)
                    typeOfConsignmentEditInt = Convert.ToString(ddlTypeConsignmentEditInt.SelectedValue);
                else
                    typeOfConsignmentEditInt = "";


                int qtyContainer20 = 0, qtyContainer40 = 0, qtyContainer40HC = 0;
                int qtyContainerFr = 0, qtyContainerODC = 0;

                // Iterate through each item in the Repeater
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    HiddenField hfContainerType = (HiddenField)item.FindControl("hfContainerType");
                    TextBox txtQty = (TextBox)item.FindControl("txtQtyContainer");

                    if (hfContainerType != null && txtQty != null)
                    {
                        string containerType = hfContainerType.Value.Trim();
                        int quantity = 0;
                        int.TryParse(txtQty.Text, out quantity); // Convert input to integer safely

                        // Assign quantity to the correct truck type variable
                        switch (containerType)
                        {
                            case "20'": qtyContainer20 = quantity; break;
                            case "40'": qtyContainer40 = quantity; break;
                            case "40' HC": qtyContainer40HC = quantity; break;
                            //case "ODC": qtyContainerFr = quantity; break;
                            //case "FR": qtyContainerODC = quantity; break;

                            case "FR": qtyContainerFr = quantity; break;
                            case "FR ODC": qtyContainerODC = quantity; break;

                        }
                    }
                }


                Byte[] packingListNameBytes = null;
                Byte[] attachment2Bytes = null;
                string packingListName = string.Empty;
                string packingListName2 = string.Empty;

                if (fileUpload4.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUpload4.PostedFile.FileName))
                    {
                        packingListName = fileUpload4.PostedFile.FileName;
                        packingListNameBytes = GetFileBytes(fileUpload4.PostedFile.FileName, fileUpload4.PostedFile.InputStream);
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

                if (fileUpload5.HasFile)
                {
                    if (!string.IsNullOrEmpty(fileUpload5.PostedFile.FileName))
                    {
                        packingListName2 = fileUpload5.PostedFile.FileName;
                        attachment2Bytes = GetFileBytes(fileUpload5.PostedFile.FileName, fileUpload5.PostedFile.InputStream);
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


                value = InwardOutward.EditInwardInternational(reqID, empRecordID, jobNoEdit, vendorNameEdit,
                                                               vendorLocationEdit, vendorEmailEdit,
                                                               vendorContactEdit, vendorLocation,
                                                               incotermsEdit, deliveryTermEdit,
                                                               NoOfTrucksIntEdit, dateEditIntInward,
                                                               typeOfConsignmentEditInt,
                                                               qtyContainer20, qtyContainer40, qtyContainer40HC, qtyContainerFr,
                                                               qtyContainerODC,
                                                              packingListName, packingListNameBytes, packingListName2, attachment2Bytes
                                                               );



            }
            else
            {
                lblDateMsg.Visible = false;
                lblDateMsg.Text = string.Empty;

                if (txtJobNoEdit.Text != "")
                {
                    jobNoEdit = Convert.ToString(txtJobNoEdit.Text);
                }
                if (txtVendorName.Text != "")
                {
                    vendorNameEdit = Convert.ToString(txtVendorName.Text);
                }
                if (txtVendorLocation.Text != "")
                {
                    vendorLocationEdit = Convert.ToString(txtVendorLocation.Text);
                }
                if (txtVendorEmail.Text != "")
                {
                    vendorEmailEdit = Convert.ToString(txtVendorEmail.Text);
                }
                if (txtVendorContact.Text != "")
                {
                    vendorContactEdit = Convert.ToString(txtVendorContact.Text);
                }

                if (ddlVenLoc.SelectedIndex > 0 || ddlVenLoc.SelectedIndex == 0)
                    vendorLocation = Convert.ToString(ddlVenLoc.SelectedValue);
                else
                    vendorLocation = "";

                if (DropDownList12.SelectedIndex > 0)
                    incotermsEdit = Convert.ToString(DropDownList12.SelectedValue);
                else
                    incotermsEdit = "";

                if (ddlDeliveryTermEdit.SelectedIndex > 0)
                    deliveryTermEdit = Convert.ToString(ddlDeliveryTermEdit.SelectedValue);
                else
                    deliveryTermEdit = "";

                NoOfTrucksEdit = Convert.ToString(txtNoOfTrucksEdit.Text);

                //dateEditDomInward = Convert.ToString(txtStartDate.Text);
                //dateEditDomInward = Convert.ToString(hdStartDate.Text);

                if (!string.IsNullOrEmpty(hdStartDate.Value))
                    dateEditDomInward = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
                else
                    dateEditDomInward = string.Empty;


                if (ddlTypeOfConsignmentEdit.SelectedIndex > 0)
                    TypeOfConsignmentEdit = Convert.ToString(ddlTypeOfConsignmentEdit.SelectedValue);
                else
                    TypeOfConsignmentEdit = "";

                // Define truck quantity variables
                int qtyTruck14 = 0, qtyTruck17 = 0, qtyTruck19 = 0, qtyTruck22 = 0;
                int qtyTruck24 = 0, qtyTruck32 = 0, qtyTruck40 = 0, qtyLowBed = 0, qtyODC = 0;

                // Iterate through each item in the Repeater
                foreach (RepeaterItem item in rptTruck2.Items)
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
                            case "Truck 14": qtyTruck14 = quantity; break;
                            case "Truck 17": qtyTruck17 = quantity; break;
                            case "Truck 19": qtyTruck19 = quantity; break;
                            case "Truck 22": qtyTruck22 = quantity; break;
                            case "Truck 24": qtyTruck24 = quantity; break;
                            case "Truck 32": qtyTruck32 = quantity; break;
                            case "Truck 40": qtyTruck40 = quantity; break;
                            case "Low Bed": qtyLowBed = quantity; break;
                            case "ODC": qtyODC = quantity; break;


                        }
                    }
                }



                Byte[] packingListNameBytes = null;
                Byte[] attachment2Bytes = null;
                string packingListName = string.Empty;
                string packingListName2 = string.Empty;

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



                value = InwardOutward.EditInwardDomestic(reqID, empRecordID, jobNoEdit, vendorNameEdit,
                                                               vendorLocationEdit, vendorEmailEdit,
                                                               vendorContactEdit, vendorLocation,
                                                               incotermsEdit, deliveryTermEdit,
                                                               NoOfTrucksEdit, dateEditDomInward,
                                                               TypeOfConsignmentEdit,
                                                               qtyTruck14, qtyTruck17, qtyTruck19, qtyTruck22,
                                                               qtyTruck24, qtyTruck32, qtyTruck40, qtyLowBed, qtyODC,
                                                              packingListName, packingListNameBytes, packingListName2, attachment2Bytes
                                                               );

            }



            if (value > 0)
            {
                //TourSendMail tsm = new TourSendMail();
                InwardOutwardSendMail insm = new InwardOutwardSendMail();
                //int sendMailValue = insm.SendInwardMail(reqID);
                string sendMailValue = insm.SendInwardMail(reqID);

                if (!string.IsNullOrEmpty(sendMailValue))
                {
                    SuccessMessage("Inward No. IN" + reqID + " updated and mail sent successfully!");
                    GetInwardList();
                }
                else
                {
                    SuccessMessage("Inward No. IN" + reqID + " updated and mail sent successfully!");
                    GetInwardList();
                }
            }
            else
                ExceptionMessage("Please try again!");

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }

    }


    private void ConfirmInward()
    {
        int inwardID = Convert.ToInt32(ViewState["INWARD_ID"]);
        string inwardNo = Convert.ToString(ViewState["INWARD_NO"]);
        int empRecordID = 0;


        if (!string.IsNullOrEmpty(txtLRNo.Text))
            lrNo = txtLRNo.Text;
        else
            lrNo = string.Empty;

        if (!string.IsNullOrEmpty(hdLRDate.Value))
            LRDate = Convert.ToDateTime(hdLRDate.Value).ToString("yyyy-MM-dd");
        else
            LRDate = string.Empty;


        if (!string.IsNullOrEmpty(txtTruckNo.Text))
            truckNo = txtTruckNo.Text;
        else
            truckNo = string.Empty;


        Byte[] podAttachment1Bytes = null;
        if (podattachment1.HasFile)
        {
            if (!string.IsNullOrEmpty(podattachment1.PostedFile.FileName))
            {
                podattachment1FileName = podattachment1.PostedFile.FileName;
                podAttachment1Bytes = GetFileBytes(podattachment1.PostedFile.FileName, podattachment1.PostedFile.InputStream);
            }
            else
            {
                podattachment1FileName = string.Empty;
                podattachment1FileName = null;
            }
        }


        if (!string.IsNullOrEmpty(txtFCRBL.Text))
            fcrbrNo = txtLRNo.Text;
        else
            fcrbrNo = string.Empty;

        if (!string.IsNullOrEmpty(hdDateFCRBL.Value))
            FCRBLDate = Convert.ToDateTime(hdDateFCRBL.Value).ToString("yyyy-MM-dd");
        else
            FCRBLDate = string.Empty;

        if (!string.IsNullOrEmpty(txtContainerNo.Text))
            containerNo = txtContainerNo.Text;
        else
            containerNo = string.Empty;
        

        Byte[] podAttachment2Bytes = null;
        if (txtPODAttachment2.HasFile)
        {
            if (!string.IsNullOrEmpty(txtPODAttachment2.PostedFile.FileName))
            {
                podattachment2FileName = txtPODAttachment2.PostedFile.FileName;
                podAttachment2Bytes = GetFileBytes(txtPODAttachment2.PostedFile.FileName, podattachment1.PostedFile.InputStream);
            }
            else
            {
                podattachment2FileName = string.Empty;
                podAttachment2Bytes = null;
            }
        }


        byte[] testBytes1 = new byte[0];
        byte[] testBytes2 = new byte[0];

        int value = InwardOutward.InsertInwardOutwardInformation(0, 0, 439, "te",
                                                                 "te", "te",
                                                                 "te", "te",
                                                                 "te", "te",
                                                                 "te", "2", 0, "te", "te",
                                                                 "","",
                                                                 testBytes1, testBytes2, "te",
                                                                 0, 0, 0, 0, 0, 0,
                                                                 0, 0, 0, 0, 0, 0,
                                                                 0, 0, 0, 439, lrNo, LRDate,
                                                                 truckNo, podAttachment1Bytes,
                                                                 fcrbrNo, FCRBLDate, containerNo,
                                                                 podAttachment2Bytes,"",""
                                                                 );


    }


    protected void btnAddNewRequestInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/InwardProduction.aspx");

    }

    public void btnSubmitCONFIRM_Click(object sender, EventArgs e)
    {
       if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.New)
        {
            UpdateInwardInformation();
            ClearForm();
        }
         else
        {
            UpdateInwardStatus();
            ClearForm();
        }

    }

    private void UpdateInwardStatus()
    {
        try
        {
            int actID = Convert.ToInt32(ViewState["ACT_ID"]);
            int inwardID = Convert.ToInt32(ViewState["INWARD_ID"]);
            string inwardNO = Convert.ToString(ViewState["INWARD_NO"]);
            int empRecordID = Convert.ToInt32(ViewState["EMP_RECORD_ID"]);
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int reqStatusId = Convert.ToInt32(ViewState["REQ_STATUS_ID"]);
            string isIntInward = Convert.ToString(ViewState["IS_INT_INWARD"]);
            byte[] podAttachment1 = new byte[0];
            byte[] txtPODAttachment2byte = new byte[0];

            string lrNoD = Convert.ToString(txtLRNoC.Text);
            string lrDateD = Convert.ToString(txtLRDateC.Text);
            //string lrDateD = Convert.ToString(hdLRDateC.Value);

            //string lrDateD =  Convert.ToDateTime(txtLRDateC.Text).ToString("yyyy-MM-dd");
            string lrTruckNoD = Convert.ToString(txtTruckNoC.Text);


            //if (txtPOD1.HasFile)
            //{
            //     podAttachment1 = txtPOD1.FileBytes;
            //};

            string fcrBrNoI = Convert.ToString(txtFCRBLC.Text);
            string fcrBrDateI = Convert.ToString(txtFCRBLDateC.Text);
            //string fcrBrDateI = Convert.ToDateTime(txtFCRBLDateC.Text).ToString("yyyy-MM-dd");
            string fcrBrContainerNoI = Convert.ToString(txtContainerNoC.Text);
            //if (pod2C.HasFile)
            //{
            //    txtPODAttachment2byte = pod2C.FileBytes;
            //};

            int teamLeaderID = Convert.ToInt32(ViewState["TEAM_LEADER_ID"]);   //Session["TEAMLEADER_ID"]

            string remarks = string.Empty;
            if (txtApproveRemark.Text != "")
            {
                remarks = txtApproveRemark.Text;
            }
            else
            {
                remarks = "";
            }

            string stringConfirmRemarks = string.Empty;
            string transporterName = string.Empty;
            string transporterEmail = string.Empty;
            string transporterContactNum = string.Empty;
            string vehiclePlacementDate = string.Empty;

            if (actID == 5)
            {
                if (txtApproveRemark.Text != "")
                {
                    stringConfirmRemarks = txtApproveRemark.Text;
                }
                else
                {
                    stringConfirmRemarks = "";
                }

                if (txtTransporterName.Text != "")
                {
                    transporterName = txtTransporterName.Text;
                }
                else
                {
                    transporterName = "";
                }
                if (txtTransporterEmail.Text != "")
                {
                    transporterEmail = txtTransporterEmail.Text;
                }
                else
                {
                    transporterEmail = "";
                }
                if (txtTransporterPhn.Text != "")
                {
                    transporterContactNum = txtTransporterPhn.Text;
                }
                else
                {
                    transporterContactNum = "";
                }
                if (txtVehiclePDate.Text != "")
                {
                    vehiclePlacementDate = txtVehiclePDate.Text;
                }
                else
                {
                    vehiclePlacementDate = "";
                }
            }
            else
            {
                stringConfirmRemarks = "";
            }

            if (actID == 3)
            {

                if (isIntInward == "INTERNATIONAL") //international
                {
                    remarks = txtCancelRemarksInt.Text;

                }
                else//domesic
                {
                    remarks = txtCancelRemarks.Text;
                }
            }
            if (actID == 4)
            {
                
                if (isIntInward == "INTERNATIONAL") //international
                {
                    remarks = txtCancelRemarksInt.Text;
                   
                }
                else//domesic
                {
                    remarks = txtCancelRemarks.Text;
                }
            }
            //if (!string.IsNullOrEmpty(txtRemarks.Text)) remarks = txtRemarks.Text;

            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;
            string inwardConfirmationNumber = "";

            InwardOutwardStatusUpdate objUT = new InwardOutwardStatusUpdate();
            string returnVal = objUT.UpdateInwardOutwardStatus(actID,
                                                      inwardID,
                                                      empRecordID,
                                                      reqStatusId,
                                                      remarks,
                                                      teamLeaderID,
                                                      currentUserID,
                                                      lrNoD,
                                                      lrDateD,
                                                      lrTruckNoD,
                                                      podAttachment1,
                                                      fcrBrNoI,
                                                      fcrBrDateI,
                                                      fcrBrContainerNoI,
                                                      txtPODAttachment2byte,
                                                      stringConfirmRemarks,
                                                      transporterName,
                                                      transporterEmail,
                                                      transporterContactNum,
                                                      vehiclePlacementDate
                                                      );

            insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
            sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);

            if (returnVal.Length > 50)
            {
                ExceptionMessage(returnVal);
                return;
            }
            else
            {
                insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
                sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);
                retStatusID = Convert.ToInt32(returnVal.Split(':')[2]);
                inwardConfirmationNumber = Convert.ToString(returnVal.Split(':')[3]);
            }

            string mailMessage = "";
            if (sentMailVal > 0)
            {
                mailMessage = "and mail sent successfully!";
            }
            else
            {
                mailMessage = "successfully, please resend e - mail from Tour Information List!";
            }

            string approvedMsg = string.Empty;
            if (!string.IsNullOrEmpty(inwardConfirmationNumber))
                approvedMsg = "Inward No.: '" + inwardNO + "' approved and Confirmation No. : '" + inwardConfirmationNumber + "' generated " + mailMessage;
            else
                approvedMsg = "Inward No.: '" + inwardNO + "' approved " + mailMessage;

            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved ||
                        retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed )
                    {
                        SuccessMessage(approvedMsg);
                    }
                    else if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                    {
                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Inward No.: '" + inwardNO + "' rejected " + mailMessage);
                        }
                        else
                        {
                            SuccessMessage("Inward No.: '" + inwardNO + "' deleted " + mailMessage);
                        }

                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                    {
                        SuccessMessage("Inward No.: '" + inwardNO + "' cancelled " + mailMessage);
                    }
                }
                else
                {
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved ||
                        retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed )
                    {
                        SuccessMessage(approvedMsg + "successfully, please resend e-mail from Inward Information List!");
                    }
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                    {
                        SuccessMessage("Inward Request No.: '" + inwardNO + "' deleted successfully.");
                    }
                    else if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Cancelled)
                    {
                        SuccessMessage("Inward Request No.: '" + inwardNO + "' cancelled successfully.");
                    }
                }
                GetInwardList();
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }

    }

    protected void gvInwardList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int rowindex = e.Row.RowIndex;
                    Label lblReqID = (Label)e.Row.FindControl("lblReqID");
                    Label lblInwardNO = (Label)e.Row.FindControl("lblInwardNO");
                    Label lblEmpRecordID = (Label)e.Row.FindControl("lblEmpRecordID");
                    Label lblTeamLeaderID = (Label)e.Row.FindControl("lblTeamLeaderID");
                    ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                    Label lblInwardStatusID = (Label)e.Row.FindControl("lblStatusId");
                    Label lblInwardStatus = (Label)e.Row.FindControl("lblInwardStatus");
                    Label lblIsInternationalInward = (Label)e.Row.FindControl("lblIsInternationalInward");
                    Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");
                    Label lblVendorName = (Label)e.Row.FindControl("lblVendorName");
                    Label lblVendorLocation = (Label)e.Row.FindControl("lblVendorLocation");
                    Label lblVendorEmail = (Label)e.Row.FindControl("lblVendorEmail");
                    Label lblVendorContact = (Label)e.Row.FindControl("lblVendorContact");
                    Label lblDelPickLoc = (Label)e.Row.FindControl("lblDelPickLoc");
                    Label lblNodeOfTransport = (Label)e.Row.FindControl("lblNodeOfTransport");
                    Label lblIsContainerStuffingPossible = (Label)e.Row.FindControl("lblIsContainerStuffingPossible");
                    Label lblIncoterms = (Label)e.Row.FindControl("lblIncoterms");
                    Label lblDeliveryTerm = (Label)e.Row.FindControl("lblDeliveryTerm");
                    Label lblNoOfTrucks = (Label)e.Row.FindControl("lblNoOfTrucks");
                    Label lblNoOfContainers = (Label)e.Row.FindControl("lblNoOfContainers");
                    Label lblDatePickup = (Label)e.Row.FindControl("lblDatePickup");
                    Label lblTypeConsignment = (Label)e.Row.FindControl("lblTypeConsignment");
                    Label lblPackingListDoc = (Label)e.Row.FindControl("lblPackingListDoc");
                    Label lblSubVendorInvoice = (Label)e.Row.FindControl("lblSubVendorInvoice");
                    Label lblQtyTruck14 = (Label)e.Row.FindControl("lblQtyTruck14");
                    Label lblQtyTruck17 = (Label)e.Row.FindControl("lblQtyTruck17");
                    Label lblQtyTruck19 = (Label)e.Row.FindControl("lblQtyTruck19");
                    Label lblQtyTruck22 = (Label)e.Row.FindControl("lblQtyTruck22");
                    Label lblQtyTruck24 = (Label)e.Row.FindControl("lblQtyTruck24");
                    Label lblQtyTruck32 = (Label)e.Row.FindControl("lblQtyTruck32");
                    Label lblQtyTruck40 = (Label)e.Row.FindControl("lblQtyTruck40");
                    Label lblQtyLowBed = (Label)e.Row.FindControl("lblQtyLowBed");
                    //Label lblQtyODC = (Label)e.Row.FindControl("lblQtyODC");

                    Label lblQtyODCTruck = (Label)e.Row.FindControl("lblQtyODCTruck");
                    Label lblQtyODCContainer = (Label)e.Row.FindControl("lblQtyODCContainer");

                    Label lblQtyContainers20 = (Label)e.Row.FindControl("lblQtyContainers20");
                    Label lblQtyContainers40 = (Label)e.Row.FindControl("lblQtyContainers40");
                    Label lblQtyContainers40HC = (Label)e.Row.FindControl("lblQtyContainers40HC");
                    Label lblQtyFR = (Label)e.Row.FindControl("lblQtyFR");
                    Label lblIsDeleted = (Label)e.Row.FindControl("lblIsDeleted");
                    Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");
                    Label lblCreatedOn = (Label)e.Row.FindControl("lblCreatedOn");
                    Label lblModifiedBy = (Label)e.Row.FindControl("lblModifiedBy");
                    Label lblModifiedOn = (Label)e.Row.FindControl("lblModifiedOn");
                    Label lblIsApprovalMailSent = (Label)e.Row.FindControl("lblIsApprovalMailSent");
                    Label lblIsApprovedMailSent = (Label)e.Row.FindControl("lblIsApprovedMailSent");
                    Label lblIsDeletedMailSent = (Label)e.Row.FindControl("lblIsDeletedMailSent");
                    Label lblIsCancelledMailSent = (Label)e.Row.FindControl("lblIsCancelledMailSent");
                    Label lblFinalApprovedBy = (Label)e.Row.FindControl("lblFinalApprovedBy");
                    Label lblFinalApprovedOn = (Label)e.Row.FindControl("lblFinalApprovedOn");
                    Label lblFinalApprovedRemarks = (Label)e.Row.FindControl("lblFinalApprovedRemarks");
                    Label lblFinalApprovedMailSent = (Label)e.Row.FindControl("lblFinalApprovedMailSent");

                    Label lblTransporterName = (Label)e.Row.FindControl("lblTransporterName");
                    Label lblTransporterNumber = (Label)e.Row.FindControl("lblTransporterNumber");
                    Label lblTransporterEmail = (Label)e.Row.FindControl("lblTransporterEmail");


                    Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                    Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                    ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");

                    btnCancel.Visible = true;
                    btnApprove.Visible = true;
                   

                    //departmentID = Convert.ToInt32(Session["DEPARTMENT_ID"]);
                    int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    ViewState["REQ_ID"] = Convert.ToInt32(lblReqID.Text);
                    ViewState["TYPE_OF_INWARD"] = Convert.ToString(lblIsInternationalInward.Text);
                 

                    string status = lblInwardStatus.Text;
                    int statusID = Convert.ToInt32(lblInwardStatusID.Text);
                    
                    btnCancel.Visible = false;
                    btnApprove.Visible = false;
                    int employeeRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                    if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.New)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/New02.png";
                        imgStatus.ToolTip = "New";

                        if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                            Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                            empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            btnCancel.Visible = true;

                            if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                            {
                                btnCancel.Text = "Reject";
                                btnCancel.ToolTip = "Reject Inward Request : " + lblInwardNO.Text;
                            }
                            else if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                            {
                                btnCancel.Text = "Delete";
                                btnCancel.ToolTip = "Delete Inward Request : " + lblInwardNO.Text;
                            }
                            else
                            {
                                btnCancel.Text = "Delete";
                                btnCancel.ToolTip = "Delete Inward Request: " + lblInwardNO.Text;
                            }
                            if (Convert.ToInt32(empRecordID) == Convert.ToInt32(lblTeamLeaderID.Text) &&
                            Convert.ToInt32(lblIsApprovalMailSent.Text) == 0)
                            {
                                imgProperties.Visible = false;
                                imgStatus.Enabled = false;
                                btnApprove.Visible = true;
                                btnCancel.Visible = true;

                            }

                        }

                        if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Jayant ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            imgStatus.Enabled = false;
                            btnApprove.Visible = false;
                            btnApprove.Text = "Confirm";
                            btnApprove.ToolTip = "Confirm Inward Request: " + lblInwardNO.Text;
                            btnCancel.Visible = false;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Inward Request: " + lblInwardNO.Text;
                            imgProperties.Visible = false;
                        }


                        if (Convert.ToInt32(empRecordID) == Convert.ToInt32(lblTeamLeaderID.Text) &&
                            Convert.ToInt32(lblIsApprovalMailSent.Text) == 0)
                        {
                            imgProperties.Visible = false;
                            imgStatus.Enabled = false;
                            btnApprove.Visible = true;
                            btnCancel.Visible = true;

                        }
                        else if(Convert.ToInt32(empRecordID) != Convert.ToInt32(lblTeamLeaderID.Text)
                           && Convert.ToInt32(empRecordID) != Convert.ToInt32(lblEmpRecordID.Text)
                          )
                        {
                            imgProperties.Visible = false;
                            //imgStatus.Enabled = false;
                            btnApprove.Visible = false;
                            btnCancel.Visible = false;
                        }

                    }
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                        imgStatus.ToolTip = "HOD Approved";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        btnApprove.Visible = false;
                        btnCancel.Visible = false;

                        if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                            empRecordID == (int)EnumInwardOutward.LogisticsPersons.Jayant ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson
                          //||empRecordID == Convert.ToInt32(lblTeamLeaderID.Text
                          )
                        {
                            imgStatus.Enabled = true;
                            btnApprove.Visible = true;
                            btnApprove.Text = "Acknowledge";
                            btnApprove.ToolTip = "Confirm Inward Request: " + lblInwardNO.Text;
                            btnCancel.Visible = true;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Inward Request: " + lblInwardNO.Text;

                        }

                        if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                           //empRecordID ==  Convert.ToInt32(lblTeamLeaderID.Text)  ||
                            empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson
                            )
                        {
                            btnCancel.Visible = false;
                            btnApprove.Visible = false;

                           
                        }
                        if (empRecordID == Convert.ToInt32(lblTeamLeaderID.Text))
                        {
                            btnCancel.Visible = true;
                            //btnApprove.Visible = true;

                        }




                    }
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/ADVICE03.png";
                        imgStatus.ToolTip = "Acknowledged";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        btnApprove.Visible = false;
                        btnCancel.Visible = false;

                        if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                          empRecordID == (int)EnumInwardOutward.LogisticsPersons.Jayant ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson ||
                          empRecordID == Convert.ToInt32(lblTeamLeaderID.Text)
                          )
                        {
                            imgStatus.Enabled = true;
                            btnApprove.Visible = true;
                            btnApprove.Text = "Confirm";
                            btnApprove.ToolTip = "Confirm Inward Request: " + lblInwardNO.Text;
                            //btnCancel.Visible = true;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Inward Request: " + lblInwardNO.Text;

                        }

                        if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                            //empRecordID ==  Convert.ToInt32(lblTeamLeaderID.Text)  ||
                            empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson
                            )
                        {
                            btnCancel.Visible = false;
                            btnApprove.Visible = false;


                        }
                        if (empRecordID == Convert.ToInt32(lblTeamLeaderID.Text))
                        {
                            btnCancel.Visible = true;
                            btnApprove.Visible = false;

                        }


                    }
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/accepted23.png";
                        imgStatus.ToolTip = "Logistics Confirmed";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        //if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                        //    empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                          empRecordID == (int)EnumInwardOutward.LogisticsPersons.Jayant ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            imgStatus.Enabled = false;
                            btnApprove.Visible = true;
                            btnApprove.Text = "Close";
                            btnApprove.ToolTip = "Close Inward Request: " + lblInwardNO.Text;
                            btnCancel.Visible = false;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Inward Request: " + lblInwardNO.Text;
                            txtTransporterName.Text = lblTransporterName.Text;
                            txtTransporterEmail.Text = lblTransporterEmail.Text;
                            txtTransporterPhn.Text = lblTransporterNumber.Text;


                        }

                        if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                          Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            btnCancel.Visible = false;
                            btnApprove.Visible = false;

                        }

                        //btnCancel.Visible = false;
                        //btnApprove.Visible = false;

                    }
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Closed)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Closed01.png";
                        imgStatus.ToolTip = "Closed";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        btnApprove.Visible = false;
                        btnCancel.Visible = false;

                    }
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Deleted02.png";
                        imgStatus.ToolTip = "Deleted";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        btnApprove.Visible = false;
                        btnCancel.Visible = false;
                       
                    }
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Cancelled)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Cancelled01.png";
                        imgStatus.ToolTip = "Cancelled";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        btnApprove.Visible = false;
                        btnCancel.Visible = false;
                    }

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                    }

                  

                }

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }


    }

    public class Truck
    {
        public string TruckName { get; set; }
        public string Qty { get; set; }
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
        rptTruck2.DataSource = truckList;
        rptTruck2.DataBind();
        rptConfirmDomestic.DataSource = truckList;
        rptConfirmDomestic.DataBind();
    }

    public class Container
    {
        public string ContainerName { get; set; }

        public string QtyCon { get; set; }
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

        Repeater1.DataSource = containerList;
        Repeater1.DataBind();

        rptConfirmInternational.DataSource = containerList;
        rptConfirmInternational.DataBind();
    }

    private void BindCompany()
    {
        try
        {
            dsUnit = InwardOutward.getLocation();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                //ddlVenLoc.DataSource = dsUnit.Tables[0];
                //ddlVenLoc.DataTextField = "UNIT_NAME";
                //ddlVenLoc.DataValueField = "UNIT_NAME";
                //ddlVenLoc.DataBind();
                ////ddlCompany.Items.Insert(0, "Select");
                //ddlVenLoc.SelectedIndex = 0;
              
                ddlMatDelLocIntEdit.DataSource = dsUnit.Tables[0];
                ddlMatDelLocIntEdit.DataTextField = "UNIT_NAME";
                ddlMatDelLocIntEdit.DataValueField = "UNIT_NAME";
                ddlMatDelLocIntEdit.DataBind();
                ddlMatDelLocIntEdit.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDomesticDeliveryTermE()
    {
        //    ddlDeliveryTermEdit.Items.Clear();
        //    ddlDeliveryTermEdit.Items.Add(new ListItem("Select", ""));
        //    ddlDeliveryTermEdit.Items.Add(new ListItem("Door delivery,Freight paid and reimbursable", "Door delivery,Freight paid and reimbursable"));
        //    ddlDeliveryTermEdit.Items.Add(new ListItem("Freight To Pay", "Freight To Pay"));


        ddlDeliveryTermEdit.Items.Clear();
        ddlDeliveryTermEdit.Items.Add(new ListItem("Select", ""));
        ddlDeliveryTermEdit.Items.Add(new ListItem("EXW", "EXW"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("FCA", "FCA"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("FAS", "FAS"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("FOB", "FOB"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("CFR", "CFR"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("CIF", "CIF"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("CIP", "CIP"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("CPT", "CPT"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("DAP", "DAP"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("DPU", "DPU"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("DDP", "DDP"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("FOR", "FOR"));

    }

    protected void rptTrucks_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Truck truck = (Truck)e.Item.DataItem;

            Label lblTruckName = (Label)e.Item.FindControl("lblTruckName");
            TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");

            if (lblTruckName != null)
                lblTruckName.Text = truck.TruckName;

            if (txtQty != null)
                txtQty.Text = truck.Qty;
        }
    }

    protected void gvInwardList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if(Convert.ToString(e.CommandArgument) == "STATUS" ||
                        Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                        Convert.ToString(e.CommandArgument) == "VIEWPASSPORT" ||
                        Convert.ToString(e.CommandArgument) == "VIEW_PACKING_LIST_FILE"||
                        Convert.ToString(e.CommandArgument) == "VIEW_SUB_VENDOR_INVOICE_FILE_NAME" ||
                        Convert.ToString(e.CommandArgument) == "SEND_MAIL" ||
                        Convert.ToString(e.CommandArgument) == "ADVICE" ||
                        Convert.ToString(e.CommandArgument) == "ADVICE" ||
                        Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL" ||
                         Convert.ToString(e.CommandArgument) == "APPROVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                Label lblTeamLeaderId = gvInwardList.Rows[rowindex].FindControl("lblTeamLeaderID") as Label;
                Label lblReqID = gvInwardList.Rows[rowindex].FindControl("lblReqID") as Label;
                int reqId = Convert.ToInt32(lblReqID.Text);
                //Label lblReqID = (Label)e.Rows.FindControl("lblReqID");
                Label lblInwardNO = gvInwardList.Rows[rowindex].FindControl("lblInwardNO") as Label;
                Label lblInwardStatus = gvInwardList.Rows[rowindex].FindControl("lblInwardStatus") as Label;
                Label lblInwardStatusId = gvInwardList.Rows[rowindex].FindControl("lblStatusId") as Label;
                Label lblIsInternationalInward = gvInwardList.Rows[rowindex].FindControl("lblIsInternationalInward") as Label;
                string isInternationalInwardValue = lblIsInternationalInward.Text;
                Label lblJobNo = gvInwardList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblVendorName = gvInwardList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblVendorLocation = gvInwardList.Rows[rowindex].FindControl("lblVendorLocation") as Label;
                Label lblVendorEmail = gvInwardList.Rows[rowindex].FindControl("lblVendorEmail") as Label;
                Label lblVendorContact = gvInwardList.Rows[rowindex].FindControl("lblVendorContact") as Label;
                Label lblDelPickLoc = gvInwardList.Rows[rowindex].FindControl("lblDelPickLoc") as Label;
                Label lblNodeOfTransport = gvInwardList.Rows[rowindex].FindControl("lblNodeOfTransport") as Label;
                Label lblIsContainerStuffingPossible = gvInwardList.Rows[rowindex].FindControl("lblIsContainerStuffingPossible") as Label;
                Label lblIncoterms = gvInwardList.Rows[rowindex].FindControl("lblIncoterms") as Label;
                Label lblDeliveryTerm = gvInwardList.Rows[rowindex].FindControl("lblDeliveryTerm") as Label;
                Label lblNoOfTrucks = gvInwardList.Rows[rowindex].FindControl("lblNoOfTrucks") as Label;
                Label lblNoOfContainers = gvInwardList.Rows[rowindex].FindControl("lblNoOfContainers") as Label;
                Label lblDatePickup = gvInwardList.Rows[rowindex].FindControl("lblDatePickup") as Label;
                Label lblTypeConsignment = gvInwardList.Rows[rowindex].FindControl("lblTypeConsignment") as Label;
                Label lblPackingListDoc = gvInwardList.Rows[rowindex].FindControl("lblPackingListDoc") as Label;
                Label lblSubVendorInvoice = gvInwardList.Rows[rowindex].FindControl("lblSubVendorInvoice") as Label;
                Label lblQtyTruck14 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck14") as Label;
                Label lblQtyTruck17 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck17") as Label;
                Label lblQtyTruck19 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck19") as Label;
                Label lblQtyTruck22 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck22") as Label;
                Label lblQtyTruck24 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck24") as Label;
                Label lblQtyTruck32 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck32") as Label;
                Label lblQtyTruck40 = gvInwardList.Rows[rowindex].FindControl("lblQtyTruck40") as Label;
                Label lblQtyLowBed = gvInwardList.Rows[rowindex].FindControl("lblQtyLowBed") as Label;

                //Label lblQtyODC = gvInwardList.Rows[rowindex].FindControl("lblQtyODC") as Label;

                Label lblQtyODCTruck = gvInwardList.Rows[rowindex].FindControl("lblQtyODCTruck") as Label;
                Label lblQtyODCContainer = gvInwardList.Rows[rowindex].FindControl("lblQtyODCContainer") as Label;

                Label lblQtyContainers20 = gvInwardList.Rows[rowindex].FindControl("lblQtyContainers20") as Label;
                Label lblQtyContainers40 = gvInwardList.Rows[rowindex].FindControl("lblQtyContainers40") as Label;
                Label lblQtyContainers40HC = gvInwardList.Rows[rowindex].FindControl("lblQtyContainers40HC") as Label;
                Label lblQtyFR = gvInwardList.Rows[rowindex].FindControl("lblQtyFR") as Label;
                Label lblIsDeleted = gvInwardList.Rows[rowindex].FindControl("lblIsDeleted") as Label;
                Label lblCreatedBy = gvInwardList.Rows[rowindex].FindControl("lblCreatedBy") as Label;
                Label lblCreatedOn = gvInwardList.Rows[rowindex].FindControl("lblCreatedOn") as Label;
                Label lblModifiedBy = gvInwardList.Rows[rowindex].FindControl("lblModifiedBy") as Label;
                Label lblModifiedOn = gvInwardList.Rows[rowindex].FindControl("lblModifiedOn") as Label;
                Label lblIsApprovalMailSent = gvInwardList.Rows[rowindex].FindControl("lblIsApprovalMailSent") as Label;
                Label lblIsApprovedMailSent = gvInwardList.Rows[rowindex].FindControl("lblIsApprovedMailSent") as Label;
                Label lblIsDeletedMailSent = gvInwardList.Rows[rowindex].FindControl("lblIsDeletedMailSent") as Label;
                Label lblIsCancelledMailSent = gvInwardList.Rows[rowindex].FindControl("lblIsCancelledMailSent") as Label;
                Label lblFinalApprovedBy = gvInwardList.Rows[rowindex].FindControl("lblFinalApprovedBy") as Label;
                Label lblFinalApprovedOn = gvInwardList.Rows[rowindex].FindControl("lblFinalApprovedOn") as Label;
                Label lblFinalApprovedRemarks = gvInwardList.Rows[rowindex].FindControl("lblFinalApprovedRemarks") as Label;
                Label lblFinalApprovedMailSent = gvInwardList.Rows[rowindex].FindControl("lblFinalApprovedMailSent") as Label;



                Label lblPackingListName = gvInwardList.Rows[rowindex].FindControl("lblPackingListName") as Label;
                Label lblSubVendorInvoiceName = gvInwardList.Rows[rowindex].FindControl("lblSubVendorInvoiceName") as Label;

                lblLegend.Text = "Inward Information [" + lblInwardNO.Text + "]";

                ViewState["INWARD_ID"] = Convert.ToInt32(lblReqID.Text);
                ViewState["INWARD_NO"] = Convert.ToString(lblInwardNO.Text);
                ViewState["EMP_RECORD_ID"] = Convert.ToInt32(lblCreatedBy.Text);
                ViewState["REQ_STATUS_ID"] = Convert.ToInt32(lblInwardStatusId.Text);
                ViewState["IS_INT_INWARD"] = Convert.ToString(lblIsInternationalInward.Text);
                lblLegend.Text = "Inward Information [" + lblInwardNO.Text + "]";

                BindMaterialDelLoc();
                BindContainerStuffingPossible();

                txtInwardNo.Text = Convert.ToString(lblInwardNO.Text);
                txtJobNo1.Text = Convert.ToString(lblJobNo.Text);
                txtVenName.Text = Convert.ToString(lblVendorName.Text);
                txtVenLoc.Text = Convert.ToString(lblVendorLocation.Text);
                txtVenEmail.Text = Convert.ToString(lblVendorEmail.Text);
                txtVenConDetForUnloading.Text = Convert.ToString(lblVendorContact.Text);

                txtJobNoIO.Text = Convert.ToString(lblJobNo.Text);
                txtVenNameIO.Text = Convert.ToString(lblVendorName.Text);
                txtVenLocIO.Text = Convert.ToString(lblVendorLocation.Text);
                txtVenEmailIO.Text = Convert.ToString(lblVendorEmail.Text);
                txtVenContactIO.Text = Convert.ToString(lblVendorContact.Text);

                if(Convert.ToString(lblDelPickLoc.Text) != "")
                {
                    try
                    {
                        ddlDeliveryLocIO.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                    }
                    catch
                    {
                        ddlDeliveryLocIO.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlDeliveryLocIO.SelectedIndex = 0;
                }

                txtIncoterms.Text = Convert.ToString(lblIncoterms.Text);
                txtDelTerm.Text = Convert.ToString(lblDeliveryTerm.Text);
                txtIsNoOfTrucksReqIO.Text = Convert.ToString(lblNoOfTrucks.Text);
                txtDate.Text = Convert.ToString(lblDatePickup.Text);
                txtTypeOfConsignmentC.Text = Convert.ToString(lblTypeConsignment.Text);

                txtTypeInward.Text = Convert.ToString(isInternationalInwardValue);

                string qty14c = (lblQtyTruck14 != null) ? lblQtyTruck14.Text.Trim() : "0";
                string qty17c = (lblQtyTruck17 != null) ? lblQtyTruck17.Text.Trim() : "0";
                string qty19c = (lblQtyTruck19 != null) ? lblQtyTruck19.Text.Trim() : "0";
                string qty22c = (lblQtyTruck22 != null) ? lblQtyTruck22.Text.Trim() : "0";
                string qty24c = (lblQtyTruck24 != null) ? lblQtyTruck24.Text.Trim() : "0";
                string qty32c = (lblQtyTruck32 != null) ? lblQtyTruck32.Text.Trim() : "0";
                string qty40c = (lblQtyTruck40 != null) ? lblQtyTruck40.Text.Trim() : "0";
                string qtyLowBedc = (lblQtyLowBed != null) ? lblQtyLowBed.Text.Trim() : "0";
                string qtyODCc = (lblQtyODCTruck != null) ? lblQtyODCTruck.Text.Trim() : "0";



                List<Truck> truckListc = new List<Truck>
                        {
                            new Truck { TruckName = "Truck 14", Qty = qty14c },
                            new Truck { TruckName = "Truck 17", Qty = qty17c },
                            new Truck { TruckName = "Truck 19", Qty = qty19c },
                            new Truck { TruckName = "Truck 22", Qty = qty22c },
                            new Truck { TruckName = "Truck 24", Qty = qty24c },
                            new Truck { TruckName = "Truck 32", Qty = qty32c },
                            new Truck { TruckName = "Truck 40", Qty = qty40c },
                            new Truck { TruckName = "Low Bed", Qty = qtyLowBedc },
                            new Truck { TruckName = "ODC", Qty = qtyODCc }
                        };


                rptConfirmDomestic.DataSource = truckListc;
                rptConfirmDomestic.DataBind();

                txtJobIntEdit.Text = Convert.ToString(lblJobNo.Text);
                txtVendNameIntEdit.Text = Convert.ToString(lblVendorName.Text);
                txtVendLocIntEdit.Text = Convert.ToString(lblVendorLocation.Text);
                txtVendEmailIntEdit.Text = Convert.ToString(lblVendorEmail.Text);
                txtVendContactIntEdit.Text = Convert.ToString(lblVendorContact.Text);
                
                TextBox15.Text = Convert.ToString(lblDatePickup.Text);
                txtNoOfTruckIntEdit.Text = Convert.ToString(lblDeliveryTerm.Text);
                txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);
                if (lblDelPickLoc.Text != "")
                {
                    ddlMatDelLocIntEdit.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                }
                else
                {
                    ddlMatDelLocIntEdit.SelectedIndex = 0;
                }
                if (lblIncoterms.Text != "")
                {
                    ddlIncotermsIntEdit.SelectedValue = Convert.ToString(lblIncoterms.Text);
                }
                else
                {
                    ddlIncotermsIntEdit.SelectedIndex = 0;
                }
                if(lblNodeOfTransport.Text != "")
                {
                    ddlModeOfTransportIntEdit.SelectedValue = Convert.ToString(lblNodeOfTransport.Text); ;
                }
                else
                {
                    ddlIncotermsIntEdit.SelectedIndex = 0;
                }

                if (lblIsContainerStuffingPossible.Text != "0")
                {
                    ddlIsStuffingEditInt.SelectedIndex = 1 ;
                }
                else
                {
                    ddlIsStuffingEditInt.SelectedIndex = 2;
                }

               txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);

                txtNoOfTruckIntEdit.Text = Convert.ToString(lblNoOfTrucks.Text);

                if(lblIsInternationalInward.Text != "INTERNATIONAL")
                {
                    BindTypeOfConsignmentIntEditRejectDomestic();
                }
                else
                {
                    ddlTypeConsignmentEditInt.Items.Clear();
                    ddlTypeConsignmentEditInt.Items.Add(new ListItem("Select", ""));
                    ddlTypeConsignmentEditInt.Items.Add(new ListItem("Full Consignment - FCL", "Full Consignment - FCL"));
                    ddlTypeConsignmentEditInt.Items.Add(new ListItem("Part Load - LCL", "Part Load - LCL"));
                }


                if (lblTypeConsignment.Text != "")
                {
                    try
                    {
                        ddlTypeConsignmentEditInt.SelectedValue = Convert.ToString(lblTypeConsignment.Text); 
                    }
                    catch
                    {
                        ddlTypeConsignmentEditInt.SelectedIndex = 0;
                    }

                    
                }
                else
                {
                    ddlTypeConsignmentEditInt.SelectedIndex = 0;
                }


                string qtyCon20C = (lblQtyContainers20 != null) ? lblQtyContainers20.Text.Trim() : "0";
                string qtyCon40C = (lblQtyContainers40 != null) ? lblQtyContainers40.Text.Trim() : "0";
                string qtyCon40HCC = (lblQtyContainers40HC != null) ? lblQtyContainers40HC.Text.Trim() : "0";
                string qtyFRC = (lblQtyFR != null) ? lblQtyFR.Text.Trim() : "0";
                string qtyODCC = (lblQtyODCContainer != null) ? lblQtyODCContainer.Text.Trim() : "0";

                List<Container> containerListC = new List<Container>
                            {
                                new Container { ContainerName = "20'",QtyCon = qtyCon20C },
                                new Container { ContainerName = "40'" ,QtyCon = qtyCon40C },
                                new Container { ContainerName = "40' HC" ,QtyCon = qtyCon40HCC },
                                //new Container { ContainerName = "ODC" ,QtyCon = qtyODCC},
                                //new Container { ContainerName = "FR" ,QtyCon = qtyFRC },
                                 new Container { ContainerName = "FR" ,QtyCon = qtyODCC},
                                new Container { ContainerName = "FR ODC" ,QtyCon = qtyFRC },

                            };

                Repeater1.DataSource = containerListC;
                Repeater1.DataBind();

                txtJobIntEdit.Enabled = true;
                txtVendNameIntEdit.Enabled = true;
                txtVendLocIntEdit.Enabled = true;
                txtVendEmailIntEdit.Enabled = true;
                txtVendContactIntEdit.Enabled = true;
                ddlMatDelLocIntEdit.Enabled = true;
                ddlIncotermsIntEdit.Enabled = true;
                ddlModeOfTransportIntEdit.Enabled = true;
                ddlIsStuffingEditInt.Enabled = true;
                txtNoContainersEditInt.Enabled = true;
                txtNoOfTruckIntEdit.Enabled = true;
                TextBox15.Enabled = true;
                ddlTypeConsignmentEditInt.Enabled = true;
                txtNoOfTruckIntEdit.Enabled = true;
                TextBox15.Enabled = true;
                fileUpload4.Enabled = true;
                fileUpload5.Enabled = true;
                //txtQtyContainer.Enabled = false;

                cancelInt0.Visible = false;
                cancelInt2.Visible = false;
                submitbtncanceleditint.Style["margin-top"] = "5px";
                //this.ModalPopupExtender7.Show();
                Button4.Text = "Update Inward Information";


                rptConfirmInternational.DataSource = containerListC;
                rptConfirmInternational.DataBind();

                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) != Convert.ToInt32(lblTeamLeaderId.Text))
                {
                    if (isInternationalInwardValue == "INTERNATIONAL")
                    {
                        internationalrow1.Visible = true;
                        internationalrow2.Visible = true;
                        internationalrow3.Visible = true;
                        internationalrow4.Visible = true;

                        domesticrow1.Visible = false;
                        domesticrow2.Visible = false;
                        domesticrow3.Visible = false;
                        domesticrow4.Visible = false;

                        txtFCRBLC.Visible = true;
                        txtFCRBLDateC.Visible = true;
                        imgFCRBLDateC.Visible = true;
                        txtContainerNoC.Visible = true;
                        //pod2C.Visible = true;


                        intContainerspacing1.Visible = true;
                        intContainerspacing2.Visible = true;
                        intContainerspacing3.Visible = true;
                        intContainerspacing4.Visible = true;
                        intContainerspacing5.Visible = true;
                        intContainerspacing6.Visible = true;
                        intContainerspacing7.Visible = true;


                        //row0InternationalC.Visible = true;
                        //row1InternationalC.Visible = true;
                        //row2InternationalC.Visible = true;

                        //intContainerspacing1.Visible = false;
                        //intContainerspacing2.Visible = false;
                        //intContainerspacing3.Visible = false;
                        //intContainerspacing4.Visible = false;
                        //intContainerspacing5.Visible = false;
                        //intContainerspacing6.Visible = false;
                        //intContainerspacing7.Visible = false;


                        row0InternationalC.Visible = true;
                        row1InternationalC.Visible = false;
                        row2InternationalC.Visible = false;


                        txtLRNoC.Visible = false;
                        txtLRDateC.Visible = false;
                        imgLRDate.Visible = false;
                        txtTruckNoC.Visible = false;
                        //txtPOD1.Visible = false;


                        DomesticTruckSpacing1.Visible = false;
                        DomesticTruckSpacing2.Visible = false;
                        DomesticTruckSpacing3.Visible = false;
                        DomesticTruckSpacing4.Visible = false;
                        DomesticTruckSpacing5.Visible = false;
                        DomesticTruckSpacing6.Visible = false;
                        DomesticTruckSpacing7.Visible = false;
                        DomesticTruckSpacing8.Visible = false;
                        DomesticTruckSpacing9.Visible = false;
                        DomesticTruckSpacing10.Visible = false;
                        DomesticTruckSpacing11.Visible = false;
                        //DomesticTruckSpacing12.Visible = false;
                        //DomesticTruckSpacing13.Visible = false;
                        //DomesticTruckSpacing14.Visible = false;

                        row0domesticC.Visible = false;
                        row1domesticC.Visible = false;
                        row2domesticC.Visible = false;

                        transporter0.Visible = false;
                        transporter0a.Visible = false;
                        rowa3.Visible = false;
                        transporter1.Visible = false;
                        transporter2.Visible = false;

                        if(Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                        {
                            transporter1.Visible = true;
                            transporter2.Visible = true;
                            transporter0a.Visible = true;
                            rowa3.Visible = true;
                            rowb3.Visible = true;
                        }
                        if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                        {
                            row1InternationalC.Visible = true;
                            rowa1.Visible = true;
                            row2InternationalC.Visible = true;
                            rowa2.Visible = true;
                            transporter0.Visible = true;

                            transporter1.Visible = true;
                            transporter2.Visible = true;
                            transporter0a.Visible = true;
                            rowa3.Visible = true;
                            rowb3.Visible = true;

                            txtTransporterName.Enabled = false;
                            txtTransporterPhn.Enabled = false;
                            txtTransporterEmail.Enabled = false;
                        }

                    }
                    else
                    {

                        internationalrow1.Visible = false;
                        internationalrow2.Visible = false;
                        internationalrow3.Visible = false;
                        internationalrow4.Visible = false;

                        domesticrow1.Visible = true;
                        domesticrow2.Visible = true;
                        domesticrow3.Visible = true;
                        domesticrow4.Visible = true;


                        txtFCRBLC.Visible = false;
                        txtFCRBLDateC.Visible = false;
                        imgFCRBLDateC.Visible = false;
                        txtContainerNoC.Visible = false;
                        //pod2C.Visible = false;


                        intContainerspacing1.Visible = false;
                        intContainerspacing2.Visible = false;
                        intContainerspacing3.Visible = false;
                        intContainerspacing4.Visible = false;
                        intContainerspacing5.Visible = false;
                        intContainerspacing6.Visible = false;
                        intContainerspacing7.Visible = false;


                        row0InternationalC.Visible = false;

                        row1InternationalC.Visible = false;
                        row2InternationalC.Visible = false;


                        txtLRNoC.Visible = true;
                        txtLRDateC.Visible = true;
                        imgLRDate.Visible = true;
                        txtTruckNoC.Visible = true;
                        //txtPOD1.Visible = true;



                        DomesticTruckSpacing1.Visible = true;
                        DomesticTruckSpacing2.Visible = true;
                        DomesticTruckSpacing3.Visible = true;
                        DomesticTruckSpacing4.Visible = true;
                        DomesticTruckSpacing5.Visible = true;
                        DomesticTruckSpacing6.Visible = true;
                        DomesticTruckSpacing7.Visible = true;
                        DomesticTruckSpacing8.Visible = true;
                        DomesticTruckSpacing9.Visible = true;
                        DomesticTruckSpacing10.Visible = true;
                        DomesticTruckSpacing11.Visible = true;
                        //DomesticTruckSpacing1.Visible = false;
                        //DomesticTruckSpacing2.Visible = false;
                        //DomesticTruckSpacing3.Visible = false;
                        //DomesticTruckSpacing4.Visible = false;
                        //DomesticTruckSpacing5.Visible = false;
                        //DomesticTruckSpacing6.Visible = false;
                        //DomesticTruckSpacing7.Visible = false;
                        //DomesticTruckSpacing8.Visible = false;
                        //DomesticTruckSpacing9.Visible = false;
                        //DomesticTruckSpacing10.Visible = false;
                        //DomesticTruckSpacing11.Visible = false;
                        //DomesticTruckSpacing12.Visible = true;
                        //DomesticTruckSpacing13.Visible = true;
                        //DomesticTruckSpacing14.Visible = true;

                        //row0domesticC.Visible = true;
                        //row1domesticC.Visible = true;
                        //row2domesticC.Visible = true;
                        row0domesticC.Visible = true;
                        row1domesticC.Visible = false;
                        row2domesticC.Visible = false;

                        transporter0.Visible = false;
                        transporter0a.Visible = false;
                        rowa3.Visible = false;
                        transporter1.Visible = false;
                        transporter2.Visible = false;
                        if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                        {

                            transporter1.Visible = true;
                            transporter2.Visible = true;
                            transporter0a.Visible = true;
                            rowa3.Visible = true;
                            rowb3.Visible = true;
                        }
                        if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                        {
                            row1domesticC.Visible = true;
                            row2domesticC.Visible = true;
                            //row2InternationalC.Visible = true;
                            rowa2.Visible = true;
                            transporter0.Visible = true;

                            transporter1.Visible = true;
                            transporter2.Visible = true;
                            transporter0a.Visible = true;
                            rowa3.Visible = true;
                            rowb3.Visible = true;

                            txtTransporterName.Enabled = false;
                            txtTransporterPhn.Enabled = false;
                            txtTransporterEmail.Enabled = false;
                        }

                    }

                }
                else
                {
                    DomesticTruckSpacing1.Visible = false;
                    DomesticTruckSpacing2.Visible = false;
                    DomesticTruckSpacing3.Visible = false;
                    DomesticTruckSpacing4.Visible = false;
                    DomesticTruckSpacing5.Visible = false;
                    DomesticTruckSpacing6.Visible = false;
                    DomesticTruckSpacing7.Visible = false;
                    DomesticTruckSpacing8.Visible = false;
                    DomesticTruckSpacing9.Visible = false;
                    DomesticTruckSpacing10.Visible = false;
                    DomesticTruckSpacing11.Visible = false;
                    //DomesticTruckSpacing12.Visible = false;
                    //DomesticTruckSpacing13.Visible = false;
                    //DomesticTruckSpacing14.Visible = false;
                    transporter0.Visible = false;
                    transporter0a.Visible = false;
                    transporter1.Visible = false;
                    transporter2.Visible = false;
                    row0domesticC.Visible = false;
                    row1domesticC.Visible = false;
                    row2domesticC.Visible = false;

                    intContainerspacing1.Visible = false;
                    intContainerspacing2.Visible = false;
                    intContainerspacing3.Visible = false;
                    intContainerspacing4.Visible = false;
                    intContainerspacing5.Visible = false;
                    intContainerspacing6.Visible = false;
                    intContainerspacing7.Visible = false;
                    row0InternationalC.Visible = false;
                    row1InternationalC.Visible = false;
                    row2InternationalC.Visible = false;
                    

                    if (isInternationalInwardValue == "INTERNATIONAL")
                    {
                        row0InternationalC.Visible = true;
                        intContainerspacing1.Visible = true;
                        intContainerspacing2.Visible = true;
                        intContainerspacing3.Visible = true;
                        intContainerspacing4.Visible = true;
                        intContainerspacing5.Visible = true;
                        intContainerspacing6.Visible = true;
                        intContainerspacing7.Visible = true;
                        
                        rptConfirmInternational.DataSource = containerListC;
                        rptConfirmInternational.DataBind();

                    }
                    else
                    {
                        row0domesticC.Visible = true;
                        DomesticTruckSpacing1.Visible = true;
                        DomesticTruckSpacing2.Visible = true;
                        DomesticTruckSpacing3.Visible = true;
                        DomesticTruckSpacing4.Visible = true;
                        DomesticTruckSpacing5.Visible = true;
                        DomesticTruckSpacing6.Visible = true;
                        DomesticTruckSpacing7.Visible = true;
                        DomesticTruckSpacing8.Visible = true;
                        DomesticTruckSpacing9.Visible = true;
                        DomesticTruckSpacing10.Visible = true;
                        DomesticTruckSpacing11.Visible = true;


                        string qty14 = (lblQtyTruck14 != null) ? lblQtyTruck14.Text.Trim() : "0";
                        string qty17 = (lblQtyTruck17 != null) ? lblQtyTruck17.Text.Trim() : "0";
                        string qty19 = (lblQtyTruck19 != null) ? lblQtyTruck19.Text.Trim() : "0";
                        string qty22 = (lblQtyTruck22 != null) ? lblQtyTruck22.Text.Trim() : "0";
                        string qty24 = (lblQtyTruck24 != null) ? lblQtyTruck24.Text.Trim() : "0";
                        string qty32 = (lblQtyTruck32 != null) ? lblQtyTruck32.Text.Trim() : "0";
                        string qty40 = (lblQtyTruck40 != null) ? lblQtyTruck40.Text.Trim() : "0";
                        string qtyLowBed = (lblQtyLowBed != null) ? lblQtyLowBed.Text.Trim() : "0";
                        string qtyODC = (lblQtyODCTruck != null) ? lblQtyODCTruck.Text.Trim() : "0";

                        List<Truck> truckList = new List<Truck>
                        {
                            new Truck { TruckName = "Truck 14", Qty = qty14 },
                            new Truck { TruckName = "Truck 17", Qty = qty17 },
                            new Truck { TruckName = "Truck 19", Qty = qty19 },
                            new Truck { TruckName = "Truck 22", Qty = qty22 },
                            new Truck { TruckName = "Truck 24", Qty = qty24 },
                            new Truck { TruckName = "Truck 32", Qty = qty32 },
                            new Truck { TruckName = "Truck 40", Qty = qty40 },
                            new Truck { TruckName = "Low Bed", Qty = qtyLowBed },
                            new Truck { TruckName = "ODC", Qty = qtyODC }
                        };


                        rptConfirmDomestic.DataSource = truckList;
                        rptConfirmDomestic.DataBind();

                        //txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);
                        //rptConfirmDomestic.DataSource = ;
                    }

                }

                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) != Convert.ToInt32(lblTeamLeaderId.Text))
                {
                    if (isInternationalInwardValue == "INTERNATIONAL")
                    {
                        ddlIncotermsIntEdit.SelectedValue = Convert.ToString(lblIncoterms.Text);
                        ddlModeOfTransportIntEdit.SelectedValue = Convert.ToString(lblNodeOfTransport.Text);
                        
                        
                        txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);
                        txtNoOfTruckIntEdit.Text = Convert.ToString(lblNoOfTrucks.Text);

                        try
                        {
                            ddlTypeConsignmentEditInt.SelectedValue = Convert.ToString(lblTypeConsignment.Text);
                        }
                        catch
                        {
                            ddlTypeConsignmentEditInt.SelectedIndex = 0;
                        }

                        string qtyCon20 = (lblQtyContainers20 != null) ? lblQtyContainers20.Text.Trim() : "0";
                        string qtyCon40 = (lblQtyContainers40 != null) ? lblQtyContainers40.Text.Trim() : "0";
                        string qtyCon40HC = (lblQtyContainers40HC != null) ? lblQtyContainers40HC.Text.Trim() : "0";
                        string qtyFR = (lblQtyFR != null) ? lblQtyFR.Text.Trim() : "0";
                        string qtyODC = (lblQtyODCContainer != null) ? lblQtyODCContainer.Text.Trim() : "0";

                        List<Container> containerList = new List<Container>
                            {
                                new Container { ContainerName = "20'",QtyCon = qtyCon20 },
                                new Container { ContainerName = "40'" ,QtyCon = qtyCon40 },
                                new Container { ContainerName = "40' HC" ,QtyCon = qtyCon40HC },
                                //new Container { ContainerName = "ODC" ,QtyCon = qtyODC},
                                //new Container { ContainerName = "FR" ,QtyCon = qtyFR },
                                  new Container { ContainerName = "FR" ,QtyCon = qtyODC},
                                new Container { ContainerName = "FR ODC" ,QtyCon = qtyFR },

                            };

                        Repeater1.DataSource = containerList;
                        Repeater1.DataBind();

                        //cancelInt0.Visible = false;
                        //cancelInt2.Visible = false;
                        //submitbtncanceleditint.Style["margin-top"] = "30px";

                        //txtNoContainersEditInt.Text = Convert.ToString(lblNoOfTrucks.Text);

                    }
                    else
                    {
                        txtJobNoEdit.Text = Convert.ToString(lblJobNo.Text);
                        txtVendorName.Text = Convert.ToString(lblVendorName.Text);
                        txtVendorLocation.Text = Convert.ToString(lblVendorLocation.Text);
                        txtVendorEmail.Text = Convert.ToString(lblVendorEmail.Text);
                        txtVendorContact.Text = Convert.ToString(lblVendorContact.Text);


                        DropDownList12.SelectedValue = Convert.ToString(lblIncoterms.Text);

                        BindMaterialDelLoc();

                        if (lblDelPickLoc.Text != "")
                        {
                            ddlVenLoc.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                        }
                        else
                        {
                            ddlVenLoc.SelectedIndex = 0;
                        }


                        if (Convert.ToString(lblDeliveryTerm) != "")
                        {
                            try
                            {

                                ddlDeliveryTermEdit.SelectedValue = Convert.ToString(lblDeliveryTerm.Text);
                            }
                            catch
                            {

                                ddlDeliveryTermEdit.SelectedValue = "";
                            }


                        }

                        txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);
                        if (lblIsInternationalInward.Text != "INTERNATIONAL")
                        {
                            BindTypeOfConsignmentIntEditRejectDomestic();
                        }
                        else
                        {
                            ddlTypeConsignmentEditInt.Items.Clear();
                            ddlTypeConsignmentEditInt.Items.Add(new ListItem("Select", ""));
                            ddlTypeConsignmentEditInt.Items.Add(new ListItem("Full Consignment - FCL", "Full Consignment - FCL"));
                            ddlTypeConsignmentEditInt.Items.Add(new ListItem("Part Load - LCL", "Part Load - LCL"));
                        }


                        ////txtNoOfTrucksEdit
                        txtStartDate.Text = Convert.ToString(lblDatePickup.Text);
                        if (Convert.ToString(lblTypeConsignment) != "")
                        {
                            try
                            {

                                ddlTypeOfConsignmentEdit.SelectedValue = Convert.ToString(lblTypeConsignment.Text);
                            }
                            catch
                            {
                                ddlTypeOfConsignmentEdit.SelectedValue = "";
                            }


                        }

                        string qty14 = (lblQtyTruck14 != null) ? lblQtyTruck14.Text.Trim() : "0";
                        string qty17 = (lblQtyTruck17 != null) ? lblQtyTruck17.Text.Trim() : "0";
                        string qty19 = (lblQtyTruck19 != null) ? lblQtyTruck19.Text.Trim() : "0";
                        string qty22 = (lblQtyTruck22 != null) ? lblQtyTruck22.Text.Trim() : "0";
                        string qty24 = (lblQtyTruck24 != null) ? lblQtyTruck24.Text.Trim() : "0";
                        string qty32 = (lblQtyTruck32 != null) ? lblQtyTruck32.Text.Trim() : "0";
                        string qty40 = (lblQtyTruck40 != null) ? lblQtyTruck40.Text.Trim() : "0";
                        string qtyLowBed = (lblQtyLowBed != null) ? lblQtyLowBed.Text.Trim() : "0";
                        string qtyODC = (lblQtyODCTruck != null) ? lblQtyODCTruck.Text.Trim() : "0";

                        List<Truck> truckList = new List<Truck>
                        {
                            new Truck { TruckName = "Truck 14", Qty = qty14 },
                            new Truck { TruckName = "Truck 17", Qty = qty17 },
                            new Truck { TruckName = "Truck 19", Qty = qty19 },
                            new Truck { TruckName = "Truck 22", Qty = qty22 },
                            new Truck { TruckName = "Truck 24", Qty = qty24 },
                            new Truck { TruckName = "Truck 32", Qty = qty32 },
                            new Truck { TruckName = "Truck 40", Qty = qty40 },
                            new Truck { TruckName = "Low Bed", Qty = qtyLowBed },
                            new Truck { TruckName = "ODC", Qty = qtyODC }
                        };


                        rptTruck2.DataSource = truckList;
                        rptTruck2.DataBind();

                        txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);


                        canceldomesticid0.Visible = false;
                        canceldomesticid1.Visible = false;
                        canceldomesticid2.Visible = false;
                        canceldomesticid3.Visible = false;
                        canceldomesticid4.Visible = false;
                        canceldomesticid5.Visible = false;
                        canceldomesticid6.Visible = false;
                        canceldomesticid7.Visible = false;
                        canceldomesticid8.Visible = false;
                        canceldomesticid9.Visible = false;
                        //submitbtncanceleditdom
                        submitbtncanceleditdom.Style["margin-top"] = "160px";

                    }
                }
                else if(Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblTeamLeaderId.Text) )
                {
                    txtJobNoEdit.Text = Convert.ToString(lblJobNo.Text);
                    txtVendorName.Text = Convert.ToString(lblVendorName.Text);
                    txtVendorLocation.Text = Convert.ToString(lblVendorLocation.Text);
                    txtVendorEmail.Text = Convert.ToString(lblVendorEmail.Text);
                    txtVendorContact.Text = Convert.ToString(lblVendorContact.Text);

                    if (lblIsInternationalInward.Text != "INTERNATIONAL")
                    {
                        //BindTypeOfConsignmentIntEditRejectDomestic();

                    }
                    else
                    {
                        DropDownList12.Items.Clear();
                        DropDownList12.Items.Add(new ListItem("Select", ""));
                        DropDownList12.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
                        DropDownList12.Items.Add(new ListItem("FOB", "FOT"));
                        DropDownList12.Items.Add(new ListItem("CIF", "CIF"));
                        DropDownList12.Items.Add(new ListItem("C&F", "C&F"));
                        DropDownList12.Items.Add(new ListItem("FCA", "FCA"));

                        DropDownList12.Items.Add(new ListItem("FAS", "FAS"));
                        DropDownList12.Items.Add(new ListItem("CFR", "CFR"));
                        DropDownList12.Items.Add(new ListItem("CIP", "CIP"));
                        DropDownList12.Items.Add(new ListItem("CPT", "CPT"));
                        DropDownList12.Items.Add(new ListItem("DAP", "DAP"));
                        DropDownList12.Items.Add(new ListItem("DPU", "DPU"));
                        DropDownList12.Items.Add(new ListItem("DDP", "DDP"));
                    }
                    

                    DropDownList12.SelectedValue = Convert.ToString(lblIncoterms.Text);
                    //ddlVenLoc.SelectedValue = Convert.ToString(lblVendorLocation.Text); 
                     if (Convert.ToString(lblDelPickLoc) != "")
                    {
                        try
                        {
                            ddlVenLoc.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                        }
                        catch
                        {
                            ddlVenLoc.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ddlVenLoc.SelectedIndex = 0;
                    }
                   

                    if (Convert.ToString(lblDeliveryTerm) != "")
                    {
                        try
                        {

                            ddlDeliveryTermEdit.SelectedValue = Convert.ToString(lblDeliveryTerm.Text);
                        }
                        catch
                        {

                            ddlDeliveryTermEdit.SelectedValue = "";
                        }


                    }

                    txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);

                    ////txtNoOfTrucksEdit
                    txtStartDate.Text = Convert.ToString(lblDatePickup.Text);
                    if (Convert.ToString(lblTypeConsignment) != "")
                    {
                        try
                        {

                            ddlTypeOfConsignmentEdit.SelectedValue = Convert.ToString(lblTypeConsignment.Text);
                        }
                        catch
                        {
                            ddlTypeOfConsignmentEdit.SelectedValue = "";
                        }


                    }

                    string qty14 = (lblQtyTruck14 != null) ? lblQtyTruck14.Text.Trim() : "0";
                    string qty17 = (lblQtyTruck17 != null) ? lblQtyTruck17.Text.Trim() : "0";
                    string qty19 = (lblQtyTruck19 != null) ? lblQtyTruck19.Text.Trim() : "0";
                    string qty22 = (lblQtyTruck22 != null) ? lblQtyTruck22.Text.Trim() : "0";
                    string qty24 = (lblQtyTruck24 != null) ? lblQtyTruck24.Text.Trim() : "0";
                    string qty32 = (lblQtyTruck32 != null) ? lblQtyTruck32.Text.Trim() : "0";
                    string qty40 = (lblQtyTruck40 != null) ? lblQtyTruck40.Text.Trim() : "0";
                    string qtyLowBed = (lblQtyLowBed != null) ? lblQtyLowBed.Text.Trim() : "0";
                    string qtyODC = (lblQtyODCTruck != null) ? lblQtyODCTruck.Text.Trim() : "0";



                    List<Truck> truckList = new List<Truck>
                        {
                            new Truck { TruckName = "Truck 14", Qty = qty14 },
                            new Truck { TruckName = "Truck 17", Qty = qty17 },
                            new Truck { TruckName = "Truck 19", Qty = qty19 },
                            new Truck { TruckName = "Truck 22", Qty = qty22 },
                            new Truck { TruckName = "Truck 24", Qty = qty24 },
                            new Truck { TruckName = "Truck 32", Qty = qty32 },
                            new Truck { TruckName = "Truck 40", Qty = qty40 },
                            new Truck { TruckName = "Low Bed", Qty = qtyLowBed },
                            new Truck { TruckName = "ODC", Qty = qtyODC }
                        };


                    rptTruck2.DataSource = truckList;
                    rptTruck2.DataBind();

                    txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);




                }

                //ddlMaterialDelLocation.SelectedValue = Convert.ToString(lblDelPickLoc.Text);

                ddlIncoTerms.SelectedValue = Convert.ToString(lblIncoterms.Text);
                ddlDeliveryTerms.SelectedValue = Convert.ToString(lblDeliveryTerm.Text);
                txtNoOfTruckReq.Text = Convert.ToString(lblNoOfTrucks.Text);
                TextBox14.Text = Convert.ToString(lblDatePickup.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.New;//1
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.New;

                        EnableControls();
                        btnSubmit.Text = "Update Inward Information";
                        

                        if (isInternationalInwardValue == "INTERNATIONAL")
                        {
                            this.ModalPopupExtender7.Show();
                        }
                        else
                        {
                            this.ModalPopupExtender1.Show();
                        }
                    }
                }
                else if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                        Convert.ToString(e.CommandArgument) == "APPROVE")
                {

                    if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.HODApprove; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.HODApproved;
                        btnSubmitCONFIRM.Text = "Approve Inward Information";

                    }
                    else if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
                    {
                        //ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm; ;
                        //ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Acknowledge; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged;
                        ViewState["CURRENT_USER_ID"] = Session["EMP_RECORD_ID"];
                        btnSubmitCONFIRM.Text = "Acknowledge Inward";
                    }
                    else if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                    {
                        //ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm; ;
                        //ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;
                        ViewState["CURRENT_USER_ID"] = Session["EMP_RECORD_ID"];
                        btnSubmitCONFIRM.Text = "Confirm Inward";
                        //btnApprove.Text = "Acknowledge";
                    }
                    else if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Close; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.Closed;
                        ViewState["CURRENT_USER_ID"] = Session["EMP_RECORD_ID"];
                        btnSubmitCONFIRM.Text = "Close Inward";
                    }
                    
                    this.ApproveConfirmPopUp.Show();
                  
                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.New)
                        {
                            ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Delete; //3;
                            ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.Deleted;


                            btnSubmit.Text = "Cancel Inward Information";
                        }
                        else
                        {
                            ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.Cancel; //4;
                            ViewState["INWARD_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.Cancelled;

                            btnSubmit.Text = "Cancel Inward Information";
                        }

                    if (isInternationalInwardValue == "INTERNATIONAL")
                    {

                        txtJobIntEdit.Enabled = false;
                        txtVendNameIntEdit.Enabled = false;
                        txtVendLocIntEdit.Enabled = false;
                        txtVendEmailIntEdit.Enabled = false;
                        txtVendContactIntEdit.Enabled = false;
                        ddlMatDelLocIntEdit.Enabled = false;
                        ddlIncotermsIntEdit.Enabled = false;
                        ddlModeOfTransportIntEdit.Enabled = false;
                        ddlIsStuffingEditInt.Enabled = false;
                        txtNoContainersEditInt.Enabled = false;
                        txtNoOfTruckIntEdit.Enabled = false;
                        TextBox15.Enabled = false;
                        ddlTypeConsignmentEditInt.Enabled = false;
                        txtNoOfTruckIntEdit.Enabled = false;
                        TextBox15.Enabled = false;
                        fileUpload4.Enabled = false;
                        fileUpload5.Enabled = false;
                        //txtQtyContainer.Enabled = false;
                        cancelInt0.Visible = true;
                        cancelInt2.Visible = true;
                        submitbtncanceleditint.Style["margin-top"] = "10px";

                        this.ModalPopupExtender7.Show();
                        Button4.Text = "Cancel Inward Information";
                    }
                    else
                    {
                        txtJobNoEdit.Enabled = false;
                        txtVendorName.Enabled = false;
                        txtVendorLocation.Enabled = false;
                        txtVendorEmail.Enabled = false;
                        txtVendorContact.Enabled = false;
                        ddlVenLoc.Enabled = false;
                        DropDownList12.Enabled = false;
                        ddlDeliveryTermEdit.Enabled = false;
                        txtNoOfTrucksEdit.Enabled = false;
                        txtStartDate.Enabled = false;
                        ddlTypeOfConsignmentEdit.Enabled = false;
                        fileUpload1.Enabled = false;
                        fileUpload2.Enabled = false;
                        canceldomesticid0.Visible = true;
                        canceldomesticid2.Visible = true;
                        canceldomesticid3.Visible = true;
                        canceldomesticid4.Visible = true;
                        canceldomesticid5.Visible = true;
                        canceldomesticid6.Visible = true;
                        canceldomesticid7.Visible = true;
                        canceldomesticid8.Visible = true;
                        canceldomesticid9.Visible = true;
                        canceldomesticid10.Visible = true;
                        canceldomesticid11.Visible = true;
                       
                       
                        submitbtncanceleditdom.Style["margin-top"] = "10px";

                        this.ModalPopupExtender1.Show();
                    }

                    //DisableControls();
                    //this.ModalPopupExtender1.Show();
                    

                    }

                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewTourInformationInPDF.Attributes.Add("src", "InwardOutwardPDF.aspx?reqid=" +
                     Convert.ToString(lblReqID.Text) + "&reqno=" + Convert.ToString(lblInwardNO.Text));
                }
                else if (Convert.ToString(e.CommandArgument) == "VIEW_PACKING_LIST_FILE")
                    ViewAttachmentFiles(reqId, "VIEW_PACKING_LIST_FILE", Convert.ToString(lblPackingListName.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "VIEW_SUB_VENDOR_INVOICE_FILE_NAME")
                    ViewAttachmentFiles(reqId, "VIEW_SUB_VENDOR_INVOICE_FILE_NAME", Convert.ToString(lblSubVendorInvoiceName.Text).Trim());

            }
            }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }

    }

    private void ViewAttachmentFiles(int PID, string fileType, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {

                ExportPDFFFiles(PID, fileType);
            }
            else
            {
                ExceptionMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ExportPDFFFiles(int PID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = InwardOutward.GetInwardFiles(PID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "VIEW_PACKING_LIST_FILE")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["PACKING_LIST"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["PACKING_LIST_NAME"]);
                }
                else if (fileType == "VIEW_SUB_VENDOR_INVOICE_FILE_NAME")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["SUB_VENDOR_INVOICE"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["SUB_VENDOR_NAME"]);
                }

                if (bytes != null)
                {
                    string[] stringParts = fileName.Split(new char[] { '.' });
                    string strType = stringParts[1];
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = strType;
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }

        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Timeout expired"))
            {
                //ExceptionSubitemsMessage("The process of downloading is too longer, please try again...!!!");
                return;
            }
            else
            {
                throw;
            }
        }
    }
    private void EnableControls()
    {
        
    }

    private void SuccessMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }


}