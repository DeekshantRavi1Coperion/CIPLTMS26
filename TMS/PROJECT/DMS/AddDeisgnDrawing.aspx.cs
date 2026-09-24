using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_DMS_AddDeisgnDrawing : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUnit = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsDrawingNo = new DataSet();

    string jobNo = string.Empty;
    int jobUnitID = 0;
    string drawingNo = string.Empty;

    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;

    string jobNoNew = string.Empty;
    string drawingNoNew = string.Empty;

    string description = string.Empty;
    int quantity = 0;
    string UOM = string.Empty;
    string rqdDateByProjectTeam = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                hdDateByProjectTeam.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDateByProjectTeam.Text = hdDateByProjectTeam.Value;

                BindCompany();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }

    // JOB DETAILS
    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetJOBDetail();
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetJOBDetail();
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;

                txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    protected void btnAddDrawing_Click(object sender, EventArgs e)
    {
        SaveDesignDrawing();
    }

    protected void btnDrawingList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/DesignDrawingList.aspx");
    }


    protected void btnImportDrawings_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/ImportDrawings.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataSet GetJOBData()
    {
        try
        {
            int unitID = 0;
            string jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;

            unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            dsJobNo = objProject.GetJOBDetailsForDesignDrawings(unitID, jobNo);

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
            ExceptionMessage(ex.ToString());
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
            ExceptionMessage(ex.ToString());
        }
    }



    private void SaveDesignDrawing()
    {
        try
        {
            string searchQuery = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("JOB_NO", typeof(string));
            //dt.Columns.Add("JOB_UNIT_ID", typeof(int));
            dt.Columns.Add("DRAWING_NO", typeof(string));
            dt.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
            dt.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("QUANTITY", typeof(int));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dt.Columns.Add("CREATED_BY", typeof(int));

            jobNo = string.Empty;
            //jobUnitID = 0;
            drawingNo = string.Empty;

            jobNoNew = string.Empty;
            drawingNoNew = string.Empty;
            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;

            description = string.Empty;
            quantity = 0;
            UOM = string.Empty;
            rqdDateByProjectTeam = string.Empty;


            if (!string.IsNullOrEmpty(txtJOBNo.Text))
            {
                jobNo = txtJOBNo.Text.ToUpper().Trim();
            }

            //jobUnitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtDrawingNumber.Text))
            {
                drawingNo = txtDrawingNumber.Text.ToUpper().Trim();
            }

            if (!string.IsNullOrEmpty(txtClientDrawingNumber.Text))
            {
                clientDrawingNo = txtClientDrawingNumber.Text.ToUpper().Trim();
            }

            if (!string.IsNullOrEmpty(txtContractorDrawingNumber.Text))
            {
                contractorDrawingNo = txtContractorDrawingNumber.Text.ToUpper().Trim();
            }

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;

            if (Convert.ToInt32(txtQuantity.Text) > 0)
                quantity = Convert.ToInt32(txtQuantity.Text);

            if (!string.IsNullOrEmpty(txtUOM.Text))
                UOM = txtUOM.Text;

            rqdDateByProjectTeam = Convert.ToDateTime(hdDateByProjectTeam.Value).ToString("yyyy-MM-dd");

            DataRow dr = dt.NewRow();
            dr["JOB_NO"] = jobNo;
            //dr["JOB_UNIT_ID"] = jobUnitID;
            dr["DRAWING_NO"] = drawingNo;

            dr["CLIENT_DRAWING_NO"] = clientDrawingNo;
            dr["CONTRACTOR_DRAWING_NO"] = contractorDrawingNo;

            dr["DESCRIPTION"] = description;
            dr["QUANTITY"] = quantity;
            dr["UOM"] = UOM;
            dr["REQD_DATE_BY_PROJECT_TEAM"] = rqdDateByProjectTeam;
            dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            //DataSet dsDuplicacy = new DataSet();
            //dsDuplicacy = objProject.CheckDrawingForDuplicacy(drawingNo, 0);
            //if (dsDuplicacy.Tables[0].Rows.Count > 0)
            //{
            //    if (Convert.ToInt32(dsDuplicacy.Tables[0].Rows[0]["DRAWINGS_COUNT"]) == 0)
            //        dt.Rows.Add(dr);
            //    else
            //    {
            //        ExceptionMessage("Drawing No. already exists...!!!");
            //        return;
            //    }
            //}



            int value = 0;

            //searchQuery = "SELECT DRAWING_ID,JOB_NO,DRAWING_NO FROM tblDesignDrawings where JOB_NO  ='" + jobNo + "' AND DRAWING_NO ='" + drawingNo + "'";
            //dsDrawingNo = objProject.GetJOBDraiwngsForDMS(searchQuery);

            int duplicacyValue = objProject.CheckDuplicacyOfSingleDrawing(drawingNo);

            if (duplicacyValue == 0)
            {
                dt.Rows.Add(dr);
                value = objProject.AddDesignDrawing(dt);
            }
            else if (duplicacyValue < 0)
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
            else
            {
                ExceptionMessage("Drawing already exists...!!!");
                return;
            }

            if (value > 0)
            {
                Reset();
                SuccessMessage("Drawing saved successfully...!!!");
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Drawing already exists...!!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void Reset()
    {
        try
        {
            txtJOBNo.Text = string.Empty;
            txtDrawingNumber.Text = string.Empty;
            txtClientDrawingNumber.Text = string.Empty;
            txtContractorDrawingNumber.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtUOM.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}
