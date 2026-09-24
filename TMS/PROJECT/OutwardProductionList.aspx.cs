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
public partial class PROJECT_OutwardProductionList : System.Web.UI.Page
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
    string podFileName1 = string.Empty;
    string podFileName2 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    
    {
        //BindTruckData();
        //BinContainerData();

        //BindIncoterms();
        //BindIncoterms2();
        //BindCompany();
        //BindDomesticDeliveryTermE();
        //BindTypeOfConsignment();
        //BindModeOfTransport();
        //BindContainerStuffingPossible();


        //ModalPopupExtender7.Show();

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindDomesticOrInternational();
                BindEmployee();
                BindIncoterms();
                BinContainerData();
                BindMaterialDelLoc();
                GetInwardList();
                BindTruckData();


                BindIncoterms();
                BindIncoterms2();
                BindCompany();
                BindDomesticDeliveryTermE();
                BindTypeOfConsignment();
                BindModeOfTransport();
                BindContainerStuffingPossible();


                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

            }
        }
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
            //ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ClearForm()
    {
        txtApproveRemark.Text = "";
        TextBox13.Text = "";
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

    private void BindTypeOfConsignment()
    {

        //ddlTypeOfConsignmentEdit.Items.Clear();
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Select", ""));
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Full Truck", "Full Truck"));
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Part Load", "Part Load"));

        //ddlTypeOfConsignmentEdit.Items.Clear();
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Select", ""));
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Full Consignment - FCL", "Full Consignment - FCL"));
        //ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Part Load - LCL", "Part Load - LCL"));


        ddlTypeOfConsignmentEdit.Items.Clear();
        ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Select", ""));
        ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Full Consignment", "Full Consignment"));
        ddlTypeOfConsignmentEdit.Items.Add(new ListItem("Part Load", "Part Load"));



        ddlTypeConsignmentEditInt.Items.Clear();
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Select", ""));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Full Consignment - FCL", "Full Consignment - FCL"));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Part Load - LCL", "Part Load - LCL"));
    }


    private void BindIncoterms()
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


            DropDownList12.Items.Clear();


            DropDownList12.Items.Add(new ListItem("Select", ""));
            DropDownList12.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            DropDownList12.Items.Add(new ListItem("FOB", "FOT"));
            DropDownList12.Items.Add(new ListItem("CIF", "CIF"));
            DropDownList12.Items.Add(new ListItem("C&F", "C&F"));

            DropDownList12.Items.Clear();

            // Add new items
            DropDownList12.Items.Add(new ListItem("Select", ""));
            DropDownList12.Items.Add(new ListItem("EXW, Packed", "EXW_Packed"));
            DropDownList12.Items.Add(new ListItem("FOT", "FOT"));
            DropDownList12.Items.Add(new ListItem("Door Delivery", "Door_Delivery"));

        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
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
            ddlIncotermsIntEdit.Items.Add(new ListItem("FOB", "FOB"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("CIF", "CIF"));
            ddlIncotermsIntEdit.Items.Add(new ListItem("C&F", "C&F"));


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
                ddlMaterialDelLocation.Items.Insert(ddlMaterialDelLocation.Items.Count, "Vendor location");
                ddlMaterialDelLocation.Items.Insert(ddlMaterialDelLocation.Items.Count, "Others");

                //ddlCompany.Items.Insert(0, "Select");
                ddlMaterialDelLocation.SelectedIndex = 0;

                ddlDeliveryLocIO.DataSource = dsUnit.Tables[0];
                ddlDeliveryLocIO.DataTextField = "UNIT_NAME";
                ddlDeliveryLocIO.DataValueField = "UNIT_NAME";
                ddlDeliveryLocIO.DataBind();
                ddlDeliveryLocIO.Items.Insert(ddlDeliveryLocIO.Items.Count, "Vendor location");
                ddlDeliveryLocIO.Items.Insert(ddlDeliveryLocIO.Items.Count, "Others");

                //ddlCompany.Items.Insert(0, "Select");
                ddlDeliveryLocIO.SelectedIndex = 0;

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

            dsRequestList = InwardOutward.GetOutwardList(startDate, endDate, reqNo, jobNoSearch, inwardCategory,
                                                        empRecordID, createdByID);
            if (dsRequestList.Tables.Count > 0 && dsRequestList.Tables[0].Rows.Count > 0)
            {
                Session["OUTWARD_LIST"] = dsRequestList.Tables[0];
                gvOutwardList.DataSource = dsRequestList.Tables[0];
                gvOutwardList.DataBind();
            }
            else
            {
                Session["OUTWARD_LIST"] = null;
                gvOutwardList.DataSource = null;
                gvOutwardList.DataBind();
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
            UpdateOutwardInformation();
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

    private void UpdateOutwardInformation()
    {
        try
        {
            int reqID = Convert.ToInt32(ViewState["OUTWARD_ID"]);
            //int reqID =  Convert.ToInt32(lblReqID.Text);
            //ViewState["TYPE_OF_INWARD"] = Convert.ToString(lblIsInte
            string reqNo = Convert.ToString(ViewState["OUTWARD_NO"]);
            int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            //string typeOfOutward = Convert.ToString(ViewState["TYPE_OF_OUTWARD"]);
            string typeOfOutward = Convert.ToString(ViewState["OUTWARD_TYPE"]);
            int value = 0;

            if (typeOfOutward == "INTERNATIONAL")
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


                value = InwardOutward.EditOutwardInternational(reqID, empRecordID, jobNoEdit, vendorNameEdit,
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

                if (ddlVenLoc.SelectedIndex > 0)
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

               // dateEditDomInward = Convert.ToString(hdStartDate.Text);

                if (!string.IsNullOrEmpty(hdStartDate.Value))
                    dateEditDomInward = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
                else
                    dateEditDomInward = string.Empty;

                if (ddlTypeOfConsignmentEdit.SelectedIndex > 0)
                    TypeOfConsignmentEdit = Convert.ToString(ddlTypeOfConsignmentEdit.SelectedValue);
                else
                    TypeOfConsignmentEdit = "";

                // Define truck quantity variables
                int qtyTruck14 = 0, qtyTruck17 = 0, qtyTruck19 = 0, qtyTruck22 = 0, qtyDContainer20 = 0; 
                int qtyTruck24 = 0, qtyTruck32 = 0, qtyTruck40 = 0, qtyLowBed = 0, qtyODC = 0 , qtyDContainer40 = 0; 

                // Iterate through each item in the Repeater
                foreach (RepeaterItem item in rptTruck2.Items)
                {
                    HiddenField hfTruckType = (HiddenField)item.FindControl("hfTruckType");
                    TextBox txtQty = (TextBox)item.FindControl("txtQty");
                    int quantity = 0;
                    if (hfTruckType != null && txtQty != null)
                    {
                        string truckType = hfTruckType.Value.Trim();
                        
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
                            case "Cont 20'": qtyDContainer20 = quantity; break;
                            case "Cont 40'": qtyDContainer40 = quantity; break;


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



                value = InwardOutward.EditOutwardDomestic(reqID, empRecordID, jobNoEdit, vendorNameEdit,
                                                               vendorLocationEdit, vendorEmailEdit,
                                                               vendorContactEdit, vendorLocation,
                                                               incotermsEdit, deliveryTermEdit,
                                                               NoOfTrucksEdit, dateEditDomInward,
                                                               TypeOfConsignmentEdit,
                                                               qtyTruck14, qtyTruck17, qtyTruck19, qtyTruck22,
                                                               qtyTruck24, qtyTruck32, qtyTruck40, qtyLowBed, qtyODC, qtyDContainer20,
                                                               qtyDContainer40,
                                                              packingListName, packingListNameBytes, packingListName2, attachment2Bytes
                                                               );

            }



            if (value > 0)
            {
                //TourSendMail tsm = new TourSendMail();
                InwardOutwardSendMail insm = new InwardOutwardSendMail();
                //int sendMailValue = insm.SendInwardMail(reqID);
                string sendMailValue = insm.SendOutwardMail(reqID);

                if (!string.IsNullOrEmpty(sendMailValue))
                {
                    SuccessMessage("Outward No. OUT" + reqID + " updated and mail sent successfully!");
                    GetInwardList();
                }
                else
                {
                    SuccessMessage("Outward No. " + reqID + " updated and mail sent successfully!");
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
        Response.Redirect("~/PROJECT/OutwardProduction.aspx");

    }

    public void btnSubmitCONFIRM_Click(object sender, EventArgs e)
    {
        
        if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.New)
        {
            UpdateOutwardInformation();
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
            int outwardID = Convert.ToInt32(ViewState["OUTWARD_ID"]);
            string outwardNO = Convert.ToString(ViewState["OUTWARD_NO"]);
            int empRecordID = Convert.ToInt32(ViewState["EMP_RECORD_ID"]);
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int reqStatusId = Convert.ToInt32(ViewState["REQ_STATUS_ID"]);
            string isIntOutward = Convert.ToString(ViewState["IS_INT_OUTWARD"]);

            byte[] podAttachment1 = new byte[0];
            byte[] podAttachment2 = new byte[0];
            string lrNoD = Convert.ToString(txtLRNoC.Text);
            //string lrDateD = Convert.ToString(txtLRDateC.Text);
            string lrDateD = Convert.ToString(txtLRDateC.Text);

            string lrTruckNoD = Convert.ToString(txtTruckNoC.Text);
            string lrInvoiceNo2C = Convert.ToString(txtInvoiceNo2C.Text);
            string lrInvoiceDate2C = Convert.ToString(txtInvoiceDate2C.Text);

            if (txtPOD1.HasFile)
            {
                //podAttachment1 = txtPOD1.FileBytes;
                if (!string.IsNullOrEmpty(txtPOD1.PostedFile.FileName))
                {
                    podFileName1 = txtPOD1.PostedFile.FileName;
                    podAttachment1 = GetFileBytes(txtPOD1.PostedFile.FileName, txtPOD1.PostedFile.InputStream);
                }
                else
                {
                    podFileName1 = string.Empty;
                    podAttachment1 = null;
                }
            }
            else
            {
                podFileName1 = string.Empty;
                podAttachment1 = null;
            }

            string fcrBrNoI = Convert.ToString(txtFCRBLC.Text);
            string fcrBrDateI = Convert.ToString(txtFCRBLDateC.Text);
            string fcrBrContainerNoI = Convert.ToString(txtContainerNoC.Text);
            string fcrBrInvoiceNo = Convert.ToString(txtInvoiceNoC.Text);
            string fcrBrInvoiceDate = Convert.ToString(txtInvoiceDateC.Text);

            if (pod2C.HasFile)
            {
                //podAttachment1 = txtPOD1.FileBytes;
                if (!string.IsNullOrEmpty(pod2C.PostedFile.FileName))
                {
                    podFileName2 = pod2C.PostedFile.FileName;
                    podAttachment2 = GetFileBytes(pod2C.PostedFile.FileName, pod2C.PostedFile.InputStream);
                }
                else
                {
                    podFileName2 = string.Empty;
                    podAttachment2 = null;
                }
            }
            else
            {
                podFileName2 = string.Empty;
                podAttachment2 = null;
            }

          
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
                
                
            }
            else
            {
                stringConfirmRemarks = "";
            }
          
            if (actID == 3)
            {

                if (isIntOutward == "INTERNATIONAL") //international
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

                if (isIntOutward == "INTERNATIONAL") //international
                {
                    remarks = txtCancelRemarksInt.Text;

                }
                else//domesic
                {
                    remarks = txtCancelRemarks.Text;
                }
            }

            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;
            string inwardConfirmationNumber = "";

            if(actID == 2)
            {
                actID = 5;
            }

            InwardOutwardStatusUpdate objUT = new InwardOutwardStatusUpdate();
            string returnVal = objUT.UpdateOutwardStatus(actID,
                                                      outwardID,
                                                      empRecordID,
                                                      reqStatusId,
                                                      remarks,
                                                      teamLeaderID,
                                                      currentUserID,
                                                      lrNoD,
                                                      lrDateD,

                                                      lrInvoiceNo2C,
                                                      lrInvoiceDate2C,
                                                      lrTruckNoD,
                                                      podAttachment1,
                                                      podFileName1,
                                                      fcrBrNoI,
                                                      fcrBrDateI,
                                                      fcrBrInvoiceNo,
                                                      fcrBrInvoiceDate,
                                                      fcrBrContainerNoI,
                                                      podAttachment2,
                                                      podFileName2,
                                                      stringConfirmRemarks,
                                                      transporterName,
                                                      transporterEmail,
                                                      transporterContactNum
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
                approvedMsg = "Outward No.: '" + outwardNO + "' approved and Confirmation No. : '" + inwardConfirmationNumber + "' generated " + mailMessage;
            else
                approvedMsg = "Outward No.: '" + outwardNO + "' approved " + mailMessage;

            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved ||
                        retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                    {
                        SuccessMessage(approvedMsg);
                    }
                    else if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                    {
                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Inward No.: '" + outwardNO + "' rejected " + mailMessage);
                        }
                        else
                        {
                            SuccessMessage("Inward No.: '" + outwardNO + "' deleted " + mailMessage);
                        }

                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                    {
                        SuccessMessage("Inward No.: '" + outwardNO + "' cancelled " + mailMessage);
                    }
                }
                else
                {
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved ||
                        retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                    {
                        SuccessMessage(approvedMsg + "successfully, please resend e-mail from Outward Information List!");
                    }
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                    {
                        SuccessMessage("Inward Request No.: '" + outwardNO + "' deleted successfully.");
                    }
                    else if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Cancelled)
                    {
                        SuccessMessage("Inward Request No.: '" + outwardNO + "' cancelled successfully.");
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


    protected void gvOutwardList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int rowindex = e.Row.RowIndex;
                    Label lblReqID = (Label)e.Row.FindControl("lblReqID");
                    Label lblOutwardNO = (Label)e.Row.FindControl("lblOutwardNO");
                    Label lblEmpRecordID = (Label)e.Row.FindControl("lblEmpRecordID");
                    Label lblTeamLeaderID = (Label)e.Row.FindControl("lblTeamLeaderID");
                    ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                    Label lblInwardStatusID = (Label)e.Row.FindControl("lblStatusId");
                    Label lblOutwardStatus = (Label)e.Row.FindControl("lblOutwardStatus");
                    Label lblIsInternationalOutward = (Label)e.Row.FindControl("lblIsInternationalOutward");
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
                    //ViewState["TYPE_OF_INWARD"] = Convert.ToString(lblIsInternationalInward.Text);
                    ViewState["TYPE_OF_OUTWARD"] = Convert.ToString(lblIsInternationalOutward.Text);

                    string status = lblOutwardStatus.Text;
                    int statusID = Convert.ToInt32(lblInwardStatusID.Text);

                    btnCancel.Visible = false;
                    btnApprove.Visible = false;
                    imgProperties.Visible = false;
                    int employeeRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                    if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.New)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/New02.png";
                        imgStatus.ToolTip = "New";

                        if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                      empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            imgProperties.Visible = true;
                            imgProperties.ToolTip = "Edit Inward Request-: " + lblOutwardNO.Text;
                            imgStatus.Enabled = false;
                            btnApprove.Visible = false;
                            btnCancel.Visible = false;

                        }

                        //if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                        //    Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                        //    empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)

                        if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                            empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            btnCancel.Visible = true;

                            //if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                            //{
                            //    btnCancel.Text = "Reject";
                            //    btnCancel.ToolTip = "Reject Inward Request : " + lblOutwardNO.Text;
                            //}
                            if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                            empRecordID == (int)EnumInwardOutward.LogisticsPersons.Jayant ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                            {
                                btnCancel.Text = "Reject";
                                btnCancel.ToolTip = "Reject Inward Request : " + lblOutwardNO.Text;
                            }
                            else if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                            {
                                btnCancel.Text = "Delete";
                                btnCancel.ToolTip = "Delete Inward Request : " + lblOutwardNO.Text;
                            }
                            else
                            {
                                btnCancel.Text = "Delete";
                                btnCancel.ToolTip = "Delete Inward Request: " + lblOutwardNO.Text;
                            }


                        }

                        if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                           empRecordID == (int)EnumInwardOutward.LogisticsPersons.Jayant ||
                          empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            btnCancel.Visible = true;
                            btnApprove.Visible = true;
                            //imgStatus.Enabled = false;
                            //btnApprove.Visible = false;
                            btnApprove.Text = "Acknowledge";
                            btnApprove.ToolTip = "Acknowledge Outward Request: " + lblOutwardNO.Text;
                            //btnCancel.Visible = false;
                            ////btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Outward Request: " + lblOutwardNO.Text;
                            imgProperties.Visible = false;
                        }


                        if (Convert.ToInt32(empRecordID) == Convert.ToInt32(lblTeamLeaderID.Text) &&
                            Convert.ToInt32(lblIsApprovalMailSent.Text) == 0)
                        {
                            imgProperties.Visible = false; 

                             imgStatus.Enabled = false;
                            btnApprove.Visible = false;
                            btnCancel.Visible = false;

                        }

                    }
                    //else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
                    //{
                    //    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                    //    imgStatus.ToolTip = "HOD Approved";
                    //    imgProperties.Visible = false;
                    //    imgStatus.Enabled = false;

                    //    if (empRecordID == (int)EnumInwardOutward.LogisticsPersons.Senthil ||
                    //       empRecordID == (int)EnumInwardOutward.LogisticsPersons.RajivKr ||
                    //       empRecordID == (int)EnumInwardOutward.LogisticsPersons.Davkinandan ||
                    //       empRecordID == (int)EnumInwardOutward.LogisticsPersons.ChandanKr ||
                    //      empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson ||
                    //      empRecordID == Convert.ToInt32(lblTeamLeaderID.Text)
                    //      )
                    //    {
                    //        imgStatus.Enabled = true;
                    //        btnApprove.Visible = true;
                    //        btnApprove.Text = "Confirm";
                    //        btnApprove.ToolTip = "Confirm Inward Request: " + lblOutwardNO.Text;
                    //        btnCancel.Visible = true;
                    //        //btnCancel.Text = "Delete";
                    //        btnCancel.Text = "Reject";
                    //        btnCancel.ToolTip = "Reject Inward Request: " + lblOutwardNO.Text;



                    //    }

                    //    if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                    //        //empRecordID ==  Convert.ToInt32(lblTeamLeaderID.Text)  ||
                    //        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson
                    //        )
                    //    {
                    //        btnCancel.Visible = false;
                    //        btnApprove.Visible = false;


                    //    }
                    //    if (empRecordID == Convert.ToInt32(lblTeamLeaderID.Text))
                    //    {
                    //        btnCancel.Visible = true;
                    //        btnApprove.Visible = false;


                    //    }


                    //}
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
                            btnApprove.ToolTip = "Confirm Outward Request: " + lblOutwardNO.Text;
                            //btnCancel.Visible = true;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Outward Request: " + lblOutwardNO.Text;

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
                    else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Closed)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Closed01.png";
                        imgStatus.ToolTip = "Closed";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                        btnApprove.Visible = false;
                        btnCancel.Visible = false;

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
                            btnApprove.ToolTip = "Close Outward Request: " + lblOutwardNO.Text;
                            btnCancel.Visible = false;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Outward Request: " + lblOutwardNO.Text;
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
        new Truck { TruckName = "ODC" },
        new Truck { TruckName = "Cont20'" },
        new Truck { TruckName = "Cont40'" }
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
    private void BindTypeOfConsignmentIntEditRejectDomestic()
    {

        ddlTypeConsignmentEditInt.Items.Clear();
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Select", ""));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Full Consignment", "Full Consignment"));
        ddlTypeConsignmentEditInt.Items.Add(new ListItem("Part Load", "Part Load"));
    }



    private void BindCompany()
    {
        try
        {
            dsUnit = InwardOutward.getLocation();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlVenLoc.DataSource = dsUnit.Tables[0];
                ddlVenLoc.DataTextField = "UNIT_NAME";
                ddlVenLoc.DataValueField = "UNIT_NAME";
                ddlVenLoc.DataBind();
                //ddlCompany.Items.Insert(0, "Select");
                ddlVenLoc.SelectedIndex = 0;

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
        //ddlDeliveryTermEdit.Items.Clear();
        //ddlDeliveryTermEdit.Items.Add(new ListItem("Select", ""));
        //ddlDeliveryTermEdit.Items.Add(new ListItem("Door delivery,Freight paid and reimbursable", "Door delivery,Freight paid and reimbursable"));
        //ddlDeliveryTermEdit.Items.Add(new ListItem("Freight To Pay", "Freight To Pay"));


        ddlDeliveryTermEdit.Items.Clear();
        ddlDeliveryTermEdit.Items.Add(new ListItem("Select", ""));
        ddlDeliveryTermEdit.Items.Add(new ListItem("Freight Paid", "Freight Paid"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("Freight To Pay", "Freight To Pay"));
        ddlDeliveryTermEdit.Items.Add(new ListItem("Freight Paid & Reimbursable", "Freight Paid & Reimbursable"));



    }

    protected void rptTrucks_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Truck truck = (Truck)e.Item.DataItem;

            Label lblTruckName = (Label)e.Item.FindControl("lblTruck");
            TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");

            if (lblTruckName != null)
                lblTruckName.Text = truck.TruckName;

            if (txtQty != null)
                txtQty.Text = truck.Qty;
        }
    }

    protected void gvOutwardList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
               
                if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                        Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                        Convert.ToString(e.CommandArgument) == "VIEWPASSPORT" ||
                        Convert.ToString(e.CommandArgument) == "VIEW_PACKING_LIST_FILE" ||
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
                Label lblTeamLeaderId = gvOutwardList.Rows[rowindex].FindControl("lblTeamLeaderID") as Label;
                Label lblReqID = gvOutwardList.Rows[rowindex].FindControl("lblReqID") as Label;
                int reqId = Convert.ToInt32(lblReqID.Text);
                //Label lblReqID = (Label)e.Rows.FindControl("lblReqID");
                Label lblOutwardNO = gvOutwardList.Rows[rowindex].FindControl("lblOutwardNO") as Label;
                Label lblInwardStatus = gvOutwardList.Rows[rowindex].FindControl("lblInwardStatus") as Label;
                Label lblInwardStatusId = gvOutwardList.Rows[rowindex].FindControl("lblStatusId") as Label;
                Label lblIsInternationalOutward = gvOutwardList.Rows[rowindex].FindControl("lblIsInternationalOutward") as Label;
                string isInternationalOutwardValue = lblIsInternationalOutward.Text;
                Label lblJobNo = gvOutwardList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblVendorName = gvOutwardList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblVendorLocation = gvOutwardList.Rows[rowindex].FindControl("lblVendorLocation") as Label;
                Label lblVendorEmail = gvOutwardList.Rows[rowindex].FindControl("lblVendorEmail") as Label;
                Label lblVendorContact = gvOutwardList.Rows[rowindex].FindControl("lblVendorContact") as Label;
                Label lblDelPickLoc = gvOutwardList.Rows[rowindex].FindControl("lblDelPickLoc") as Label;
                Label lblNodeOfTransport = gvOutwardList.Rows[rowindex].FindControl("lblNodeOfTransport") as Label;
                Label lblIsContainerStuffingPossible = gvOutwardList.Rows[rowindex].FindControl("lblIsContainerStuffingPossible") as Label;
                Label lblIncoterms = gvOutwardList.Rows[rowindex].FindControl("lblIncoterms") as Label;
                Label lblDeliveryTerm = gvOutwardList.Rows[rowindex].FindControl("lblDeliveryTerm") as Label;
                Label lblNoOfTrucks = gvOutwardList.Rows[rowindex].FindControl("lblNoOfTrucks") as Label;
                Label lblNoOfContainers = gvOutwardList.Rows[rowindex].FindControl("lblNoOfContainers") as Label;
                Label lblDatePickup = gvOutwardList.Rows[rowindex].FindControl("lblDatePickup") as Label;
                Label lblTypeConsignment = gvOutwardList.Rows[rowindex].FindControl("lblTypeConsignment") as Label;
                Label lblPackingListDoc = gvOutwardList.Rows[rowindex].FindControl("lblPackingListDoc") as Label;
                Label lblSubVendorInvoice = gvOutwardList.Rows[rowindex].FindControl("lblSubVendorInvoice") as Label;
                Label lblQtyTruck14 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck14") as Label;
                Label lblQtyTruck17 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck17") as Label;
                Label lblQtyTruck19 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck19") as Label;
                Label lblQtyTruck22 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck22") as Label;
                Label lblQtyTruck24 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck24") as Label;
                Label lblQtyTruck32 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck32") as Label;
                Label lblQtyTruck40 = gvOutwardList.Rows[rowindex].FindControl("lblQtyTruck40") as Label;
                Label lblQtyDCont20 = gvOutwardList.Rows[rowindex].FindControl("lblQtyDCont20") as Label;
                Label lblQtyDCont40 = gvOutwardList.Rows[rowindex].FindControl("lblQtyDCont40") as Label;
                Label lblQtyLowBed = gvOutwardList.Rows[rowindex].FindControl("lblQtyLowBed") as Label;
                //Label lblQtyODC = gvOutwardList.Rows[rowindex].FindControl("lblQtyODC") as Label;
                Label lblQtyODCTruck = gvOutwardList.Rows[rowindex].FindControl("lblQtyODCTruck") as Label;
                Label lblQtyODCContainer = gvOutwardList.Rows[rowindex].FindControl("lblQtyODCContainer") as Label;
                Label lblQtyContainers20 = gvOutwardList.Rows[rowindex].FindControl("lblQtyContainers20") as Label;
                Label lblQtyContainers40 = gvOutwardList.Rows[rowindex].FindControl("lblQtyContainers40") as Label;
                Label lblQtyContainers40HC = gvOutwardList.Rows[rowindex].FindControl("lblQtyContainers40HC") as Label;
                Label lblQtyFR = gvOutwardList.Rows[rowindex].FindControl("lblQtyFR") as Label;
                Label lblIsDeleted = gvOutwardList.Rows[rowindex].FindControl("lblIsDeleted") as Label;
                Label lblCreatedBy = gvOutwardList.Rows[rowindex].FindControl("lblCreatedBy") as Label;
                Label lblCreatedOn = gvOutwardList.Rows[rowindex].FindControl("lblCreatedOn") as Label;
                Label lblModifiedBy = gvOutwardList.Rows[rowindex].FindControl("lblModifiedBy") as Label;
                Label lblModifiedOn = gvOutwardList.Rows[rowindex].FindControl("lblModifiedOn") as Label;
                Label lblIsApprovalMailSent = gvOutwardList.Rows[rowindex].FindControl("lblIsApprovalMailSent") as Label;
                Label lblIsApprovedMailSent = gvOutwardList.Rows[rowindex].FindControl("lblIsApprovedMailSent") as Label;
                Label lblIsDeletedMailSent = gvOutwardList.Rows[rowindex].FindControl("lblIsDeletedMailSent") as Label;
                Label lblIsCancelledMailSent = gvOutwardList.Rows[rowindex].FindControl("lblIsCancelledMailSent") as Label;
                Label lblFinalApprovedBy = gvOutwardList.Rows[rowindex].FindControl("lblFinalApprovedBy") as Label;
                Label lblFinalApprovedOn = gvOutwardList.Rows[rowindex].FindControl("lblFinalApprovedOn") as Label;
                Label lblFinalApprovedRemarks = gvOutwardList.Rows[rowindex].FindControl("lblFinalApprovedRemarks") as Label;
                Label lblFinalApprovedMailSent = gvOutwardList.Rows[rowindex].FindControl("lblFinalApprovedMailSent") as Label;
                Label lblPackingListName = gvOutwardList.Rows[rowindex].FindControl("lblPackingListName") as Label;
                Label lblSubVendorInvoiceName = gvOutwardList.Rows[rowindex].FindControl("lblSubVendorInvoiceName") as Label;

                lblLegend.Text = "Outward Information [" + lblOutwardNO.Text + "]";

                ViewState["OUTWARD_ID"] = Convert.ToInt32(lblReqID.Text);
                ViewState["OUTWARD_NO"] = Convert.ToString(lblOutwardNO.Text);
                ViewState["EMP_RECORD_ID"] = Convert.ToInt32(lblCreatedBy.Text);
                ViewState["REQ_STATUS_ID"] = Convert.ToInt32(lblInwardStatusId.Text);
                ViewState["IS_INT_OUTWARD"] = Convert.ToString(lblIsInternationalOutward.Text);
                ViewState["OUTWARD_TYPE"] = isInternationalOutwardValue;


                lblLegend.Text = "Outward Information [" + lblOutwardNO.Text + "]";
                BindMaterialDelLoc();


                //txtInwardNo.Text = Convert.ToString(lblOutwardNO.Text);
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
                txtIncoterms.Text = Convert.ToString(lblIncoterms.Text);
                txtDelTerm.Text = Convert.ToString(lblDeliveryTerm.Text);
                txtIsNoOfTrucksReqIO.Text = Convert.ToString(lblNoOfTrucks.Text);
                txtDate.Text = Convert.ToString(lblDatePickup.Text);
                txtTypeOfConsignmentC.Text = Convert.ToString(lblTypeConsignment.Text);
                txtTypeOutward.Text = Convert.ToString(isInternationalOutwardValue);

                //txtTypeInward.Text = Convert.ToString(isInternationalInwardValue);

                txtJobIntEdit.Text = Convert.ToString(lblJobNo.Text);
                txtVendNameIntEdit.Text = Convert.ToString(lblVendorName.Text);
                txtVendLocIntEdit.Text = Convert.ToString(lblVendorLocation.Text);
                txtVendEmailIntEdit.Text = Convert.ToString(lblVendorEmail.Text);
                txtVendContactIntEdit.Text = Convert.ToString(lblVendorContact.Text);
                TextBox15.Text = Convert.ToString(lblDatePickup.Text);
                txtNoOfTruckIntEdit.Text = Convert.ToString(lblDeliveryTerm.Text);
                txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);

                cancelInt0.Visible = false;
                cancelInt2.Visible = false;
                //submitbtncanceleditint.Style["margin-top"] = "20px";
                //this.ModalPopupExtender7.Show();
                Button4.Text = "Update Inward Information";


                if (lblDelPickLoc.Text != "")
                {
                    try
                    {
                        ddlMatDelLocIntEdit.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                    }
                    catch
                    {
                        ddlMatDelLocIntEdit.SelectedIndex = 0;
                    }
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
                if (lblNodeOfTransport.Text != "")
                {
                    ddlModeOfTransportIntEdit.SelectedValue = Convert.ToString(lblNodeOfTransport.Text); ;
                }
                else
                {
                    ddlIncotermsIntEdit.SelectedIndex = 0;
                }

                if (lblIsContainerStuffingPossible.Text != "0")
                {
                    ddlIsStuffingEditInt.SelectedIndex = 0;
                }
                else
                {
                    ddlIsStuffingEditInt.SelectedIndex = 1;
                }

                txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);

                txtNoOfTruckIntEdit.Text = Convert.ToString(lblNoOfTrucks.Text);

                if (lblIsInternationalOutward.Text != "INTERNATIONAL")
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
                    ddlTypeConsignmentEditInt.SelectedValue = Convert.ToString(lblTypeConsignment.Text); ;
                }
                else
                {
                    ddlTypeConsignmentEditInt.SelectedIndex = 0;
                }


                // txtTypeInward.Text = Convert.ToString(lblTypeConsignment.Text);

                try
                {
                    ddlDeliveryLocIO.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                }
                catch
                {
                    ddlDeliveryLocIO.SelectedIndex = 1;
                }
               
                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) != Convert.ToInt32(lblTeamLeaderId.Text))
                {

                    if (isInternationalOutwardValue == "INTERNATIONAL")
                    {

                        //internationalrow1.Visible = true;
                        //internationalrow2.Visible = true;
                        //internationalrow3.Visible = true;
                        //internationalrow4.Visible = true;

                        internationalrow1.Visible = false;
                        internationalrow2.Visible = false;
                        internationalrow3.Visible = false;
                        internationalrow4.Visible = false;


                        domesticrow1.Visible = false;
                        domesticrow2.Visible = false;
                        domesticrow3.Visible = false;
                        domesticrow4.Visible = false;

                        txtFCRBLC.Visible = true;
                        txtFCRBLDateC.Visible = true;
                        imgFCRBLDateC.Visible = true;
                        txtInvoiceNoC.Visible = true;
                        txtInvoiceDateC.Visible = true;
                        imgInvoiceDateC.Visible = true;
                        txtContainerNoC.Visible = true;
                        pod2C.Visible = true;
                        row0InternationalC.Visible = true;

                        intContainerspacing1.Visible = true;
                        intContainerspacing2.Visible = true;
                        intContainerspacing3.Visible = true;
                        intContainerspacing4.Visible = true;
                        intContainerspacing5.Visible = true;
                        intContainerspacing6.Visible = true;
                        intContainerspacing7.Visible = true;

                        //row1InternationalC.Visible = true;
                        //row2InternationalC.Visible = true;
                        //row3InternationalC.Visible = true;

                        row1InternationalC.Visible = false;
                        row2InternationalC.Visible = false;
                        row3InternationalC.Visible = false;


                        txtLRNoC.Visible = false;
                        txtLRDateC.Visible = false;
                        imgLRDate.Visible = false;

                        txtInvoiceNo2C.Visible = false;
                        txtInvoiceDate2C.Visible = false;
                        imgInvoiceDate2C.Visible = false;

                        txtTruckNoC.Visible = false;
                        txtPOD1.Visible = false;


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
                                new Container { ContainerName = "FR" ,QtyCon = qtyODC},
                                new Container { ContainerName = "FR ODC" ,QtyCon = qtyFR },

                            };

                        rptConfirmInternational.DataSource = containerList;
                        rptConfirmInternational.DataBind();

                        //txtNoContainersEditInt.Text = Convert.ToString(lblNoOfTrucks.Text);

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
                        DomesticTruckSpacing12.Visible = false;
                        DomesticTruckSpacing13.Visible = false;
                        DomesticTruckSpacing14.Visible = false;

                        row0domesticC.Visible = false;
                        row1domesticC.Visible = false;
                        row2domesticC.Visible = false;
                        row3domesticC.Visible = false;

                        transporter0a.Visible = false;
                        rowa3.Visible = false;
                        rowb3.Visible = false;
                        transporter1.Visible = false;
                        transporter2.Visible = false;

                        if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                        {
                            transporter1.Visible = true;
                            transporter2.Visible = true;
                            transporter0a.Visible = true;
                            rowa3.Visible = true;
                            rowb3.Visible = true;
                            txtTransporterName.Enabled = true;
                            txtTransporterPhn.Enabled = true;
                            txtTransporterEmail.Enabled = true;
                        }
                        if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                        {
                            row1InternationalC.Visible = true;
                            //rowa1.Visible = true;
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
//////////////////////////////////////////////////////////////////////////////////////////////////////////

                        string qty14 = (lblQtyTruck14 != null) ? lblQtyTruck14.Text.Trim() : "0";
                        string qty17 = (lblQtyTruck17 != null) ? lblQtyTruck17.Text.Trim() : "0";
                        string qty19 = (lblQtyTruck19 != null) ? lblQtyTruck19.Text.Trim() : "0";
                        string qty22 = (lblQtyTruck22 != null) ? lblQtyTruck22.Text.Trim() : "0";
                        string qty24 = (lblQtyTruck24 != null) ? lblQtyTruck24.Text.Trim() : "0";
                        string qty32 = (lblQtyTruck32 != null) ? lblQtyTruck32.Text.Trim() : "0";
                        string qty40 = (lblQtyTruck40 != null) ? lblQtyTruck40.Text.Trim() : "0";
                        string qtyLowBed = (lblQtyLowBed != null) ? lblQtyLowBed.Text.Trim() : "0";
                        string qtyODC = (lblQtyODCTruck != null) ? lblQtyODCTruck.Text.Trim() : "0";
                        string qtyDCont20 = (lblQtyDCont20 != null) ? lblQtyDCont20.Text.Trim() : "0";
                        string qtyDCont40 = (lblQtyDCont40 != null) ? lblQtyDCont40.Text.Trim() : "0";



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
                            new Truck { TruckName = "ODC", Qty = qtyODC },
                            new Truck { TruckName = "Cont 20'", Qty = qtyDCont20 },
                            new Truck { TruckName = "Cont 40'", Qty = qtyDCont40 }
                        };


                        rptConfirmDomestic.DataSource = truckList;
                        rptConfirmDomestic.DataBind();

                        //txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //domesticrow1.Visible = true;
                        //domesticrow2.Visible = true;
                        //domesticrow3.Visible = true;
                        //domesticrow4.Visible = true;

                        domesticrow1.Visible = false;
                        domesticrow2.Visible = false;
                        domesticrow3.Visible = false;
                        domesticrow4.Visible = false;


                        txtFCRBLC.Visible = false;
                        txtFCRBLDateC.Visible = false;
                        txtInvoiceNoC.Visible = false;
                        txtInvoiceDateC.Visible = false;
                        imgInvoiceDateC.Visible = false;
                        imgFCRBLDateC.Visible = false;
                        txtContainerNoC.Visible = false;
                        pod2C.Visible = false;
                        row0InternationalC.Visible = false;

                        intContainerspacing1.Visible = false;
                        intContainerspacing2.Visible = false;
                        intContainerspacing3.Visible = false;
                        intContainerspacing4.Visible = false;
                        intContainerspacing5.Visible = false;
                        intContainerspacing6.Visible = false;
                        intContainerspacing7.Visible = false;

                        row1InternationalC.Visible = false;
                        row2InternationalC.Visible = false;
                        row3InternationalC.Visible = false;


                        txtLRNoC.Visible = true;
                        txtLRDateC.Visible = true;
                        imgLRDate.Visible = true;
                        txtInvoiceNo2C.Visible = true; 
                        txtInvoiceDate2C.Visible = true; 
                        imgInvoiceDate2C.Visible = true; 
                        txtTruckNoC.Visible = true;
                        txtPOD1.Visible = true;
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
                        DomesticTruckSpacing12.Visible = true;
                        DomesticTruckSpacing13.Visible = true;
                        DomesticTruckSpacing14.Visible = true;

                        //row0domesticC.Visible = true;
                        //row1domesticC.Visible = true;
                        //row2domesticC.Visible = true;
                        //row3domesticC.Visible = true;

                        row0domesticC.Visible = true;
                        row1domesticC.Visible = false;
                        row2domesticC.Visible = false;
                        row3domesticC.Visible = false;

                        transporter0a.Visible = false;
                        rowa3.Visible = false;
                        rowb3.Visible = false;
                        transporter1.Visible = false;
                        transporter2.Visible = false;

                        if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                        {

                            transporter1.Visible = true;
                            transporter2.Visible = true;
                            transporter0a.Visible = true;
                            rowa3.Visible = true;
                            rowb3.Visible = true;
                            txtTransporterName.Enabled = true;
                            txtTransporterPhn.Enabled = true;
                            txtTransporterEmail.Enabled = true;
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
                    DomesticTruckSpacing12.Visible = false;
                    DomesticTruckSpacing13.Visible = false;
                    DomesticTruckSpacing14.Visible = false;


                    row0domesticC.Visible = false;
                    row1domesticC.Visible = false;
                    row2domesticC.Visible = false;
                    row3domesticC.Visible = false;
                    row0InternationalC.Visible = false;
                    intContainerspacing1.Visible = false;
                    intContainerspacing2.Visible = false;
                    intContainerspacing3.Visible = false;
                    intContainerspacing4.Visible = false;
                    intContainerspacing5.Visible = false;
                    intContainerspacing6.Visible = false;
                    intContainerspacing7.Visible = false;

                    row1InternationalC.Visible = false;
                    row2InternationalC.Visible = false;
                    row3InternationalC.Visible = false;
                }

                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) != Convert.ToInt32(lblTeamLeaderId.Text))
                {
                    if (isInternationalOutwardValue == "INTERNATIONAL")
                    {
                        ddlIncotermsIntEdit.SelectedValue = Convert.ToString(lblIncoterms.Text);
                        ddlModeOfTransportIntEdit.SelectedValue = Convert.ToString(lblNodeOfTransport.Text);

                        if (Convert.ToString(lblIsContainerStuffingPossible) != "")
                        {
                            try
                            {
                                if(Convert.ToString(lblIsContainerStuffingPossible.Text) == "1")
                                {
                                    ddlIsStuffingEditInt.SelectedValue = "Yes";
                                }
                                else
                                {
                                    ddlIsStuffingEditInt.SelectedValue = "No";
                                }

                                //ddlIsStuffingEditInt.SelectedValue = Convert.ToString(lblIsContainerStuffingPossible.Text);
                            }
                            catch
                            {

                                ddlIsStuffingEditInt.SelectedValue = "";
                            }


                        }

                        

                        txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);
                        txtNoOfTruckIntEdit.Text = Convert.ToString(lblNoOfTrucks.Text);
                      
                        if (Convert.ToString(lblTypeConsignment) != "")
                        {
                            try
                            {

                                ddlTypeConsignmentEditInt.SelectedValue = Convert.ToString(lblTypeConsignment.Text);
                            }
                            catch
                            {

                                ddlTypeConsignmentEditInt.SelectedValue = "";
                            }


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
                                new Container { ContainerName = "FR" ,QtyCon = qtyODC},
                                new Container { ContainerName = "FR ODC" ,QtyCon = qtyFR },

                            };

                        Repeater1.DataSource = containerList;
                        Repeater1.DataBind();

                        txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);

                    }
                    else
                    {
                        txtJobNoEdit.Text = Convert.ToString(lblJobNo.Text);
                        txtVendorName.Text = Convert.ToString(lblVendorName.Text);
                        txtVendorLocation.Text = Convert.ToString(lblVendorLocation.Text);
                        txtVendorEmail.Text = Convert.ToString(lblVendorEmail.Text);
                        txtVendorContact.Text = Convert.ToString(lblVendorContact.Text);


                        DropDownList12.SelectedValue = Convert.ToString(lblIncoterms.Text);

                        try
                        {
                            ddlVenLoc.SelectedValue = Convert.ToString(lblDelPickLoc.Text);
                        }
                        catch
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
                        string qtyDCont20 = (lblQtyDCont20!= null) ? lblQtyDCont20.Text.Trim() : "0";
                        string qtyDCont40 = (lblQtyDCont40 != null) ? lblQtyDCont40.Text.Trim() : "0";



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
                            new Truck { TruckName = "ODC", Qty = qtyODC },
                            new Truck { TruckName = "Cont 20'", Qty = qtyDCont20 },
                            new Truck { TruckName = "Cont 40'", Qty = qtyDCont40 }
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
                        submitbtncanceleditdom.Style["margin-top"] = "160px";

                    }
                }
                else if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblTeamLeaderId.Text))
                {

                    if (isInternationalOutwardValue == "INTERNATIONAL")
                    {

                        try
                        {
                            ddlIncotermsIntEdit.SelectedValue = Convert.ToString(lblIncoterms.Text);
                            ddlModeOfTransportIntEdit.SelectedValue = Convert.ToString(lblNodeOfTransport.Text);
                            if(Convert.ToInt32(lblIsContainerStuffingPossible.Text) == 1)
                            {
                                ddlIsStuffingEditInt.SelectedValue = "Yes";
                            }
                            else
                            {
                                ddlIsStuffingEditInt.SelectedValue = "No";
                            }
                            //ddlIsStuffingEditInt.SelectedValue = Convert.ToString(lblIsContainerStuffingPossible.Text);
                            txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);
                            txtNoOfTruckIntEdit.Text = Convert.ToString(lblNoOfTrucks.Text);
                            ddlTypeConsignmentEditInt.SelectedValue = Convert.ToString(lblTypeConsignment.Text);

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
                                new Container { ContainerName = "FR" ,QtyCon = qtyODC},
                                new Container { ContainerName = "FR ODC" ,QtyCon = qtyFR },

                            };

                            Repeater1.DataSource = containerList;
                            Repeater1.DataBind();

                            txtNoContainersEditInt.Text = Convert.ToString(lblNoOfContainers.Text);
                        }
                        catch
                        {

                        }
                        
                    }
                    else
                    {

                        txtJobNoEdit.Text = Convert.ToString(lblJobNo.Text);
                        txtVendorName.Text = Convert.ToString(lblVendorName.Text);
                        txtVendorLocation.Text = Convert.ToString(lblVendorLocation.Text);
                        txtVendorEmail.Text = Convert.ToString(lblVendorEmail.Text);
                        txtVendorContact.Text = Convert.ToString(lblVendorContact.Text);


                        DropDownList12.SelectedValue = Convert.ToString(lblIncoterms.Text);
                        ddlVenLoc.SelectedValue = Convert.ToString(lblDelPickLoc.Text); 
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
                        string qtyDCont20 = (lblQtyDCont20 != null) ? lblQtyDCont20.Text.Trim() : "0";
                        string qtyDCont40 = (lblQtyDCont40 != null) ? lblQtyDCont40.Text.Trim() : "0";



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
                            new Truck { TruckName = "ODC", Qty = qtyODC },
                            new Truck { TruckName = "Cont 20'", Qty = qtyDCont20 },
                            new Truck { TruckName = "Cont 40'", Qty = qtyDCont40 }
                        };


                        rptTruck2.DataSource = truckList;
                        rptTruck2.DataBind();

                        txtNoOfTrucksEdit.Text = Convert.ToString(lblNoOfTrucks.Text);




                    }



                }


                ddlIncoTerms.SelectedValue = Convert.ToString(lblIncoterms.Text);
                ddlDeliveryTerms.SelectedValue = Convert.ToString(lblDeliveryTerm.Text);
                txtNoOfTruckReq.Text = Convert.ToString(lblNoOfTrucks.Text);
                TextBox14.Text = Convert.ToString(lblDatePickup.Text);

                ddlTypeOfConsignment.SelectedValue = "A35";
             
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.New;//1
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.New;

                        EnableControls();
                        btnSubmit.Text = "Update Outward Information";


                        if (isInternationalOutwardValue == "INTERNATIONAL")
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
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Acknowledge; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged;
                        btnSubmitCONFIRM.Text = "Acknowledge Outward Request";

                    }
                    else if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;
                        ViewState["CURRENT_USER_ID"] = Session["EMP_RECORD_ID"];
                        btnSubmitCONFIRM.Text = "Confirm Outward Request";
                    }
                    else if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Close; ;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.Closed;
                        ViewState["CURRENT_USER_ID"] = Session["EMP_RECORD_ID"];
                        btnSubmitCONFIRM.Text = "Close Outward Request";
                    }

                    //DisableControls();
                    this.ApproveConfirmPopUp.Show();


                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    if (Convert.ToInt32(lblInwardStatusId.Text) == (int)EnumInwardOutward.EnumINOUTStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Delete; //3;
                        ViewState["INWARD_STATUS_ID"] = (int)EnumInwardOutward.EnumINOUTStatus.Deleted;


                        btnSubmit.Text = "Cancel Outward Information";
                    }
                    else
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.Cancel; //4;
                        ViewState["INWARD_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.Cancelled;

                        btnSubmit.Text = "Cancel Outward Information";
                    }

                    if (isInternationalOutwardValue == "INTERNATIONAL")
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
                        submitbtncanceleditint.Style["margin-top"] = "5px";
                        
                        this.ModalPopupExtender7.Show();
                        Button4.Text = "Cancel Outward Information";
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
                        canceldomesticid1.Visible = true;
                        canceldomesticid2.Visible = true;
                        canceldomesticid3.Visible = true;
                        canceldomesticid4.Visible = true;
                        canceldomesticid5.Visible = true;
                        canceldomesticid6.Visible = true;
                        canceldomesticid7.Visible = true;
                        canceldomesticid8.Visible = true;
                        canceldomesticid9.Visible = true;
                        //submitbtncanceleditdom
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
                     Convert.ToString(lblReqID.Text) + "&inwardOrOutward=outward"+ "&reqno=" + Convert.ToString(lblOutwardNO.Text));
                }
                else if (Convert.ToString(e.CommandArgument) == "VIEW_PACKING_LIST_FILE")
                    ViewAttachmentFiles(reqId, "VIEW_PACKING_LIST_FILE", Convert.ToString(lblPackingListName.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "VIEW_SUB_VENDOR_INVOICE_FILE_NAME")
                    ViewAttachmentFiles(reqId, "VIEW_SUB_VENDOR_INVOICE_FILE_NAME", Convert.ToString(lblSubVendorInvoiceName.Text).Trim());

            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
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
            dsFiles = InwardOutward.GetOutwardFiles(PID);

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