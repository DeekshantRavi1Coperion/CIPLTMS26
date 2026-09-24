using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class MR_WORKFLOW_PivotGroups : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.PROJECT_MANAGEMENT.ProjectManagement objProjectManagement = new BAL.PROJECT_MANAGEMENT.ProjectManagement();
    DataSet dsPivotGroup = new DataSet();


    private string _codeS;
    private string _nameS;
    private string _descriptionS;

    private int _PIdU;
    private string _codeU;
    private string _nameU;
    private string _descriptionU;
    private int _currentUserIdU;


    public int PIdU
    {
        get
        {
            if (Convert.ToInt32(ViewState["PID"]) != 0)
                _PIdU = Convert.ToInt32(ViewState["PID"]);
            else _PIdU = 0;

            return _PIdU;
        }

        set
        {
            _PIdU = value;
        }
    }

    public string CodeU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtCodeToU.Text))
                _codeU = txtCodeToU.Text;
            else _codeU = "";

            return _codeU;
        }

        set
        {
            _codeU = value;
        }
    }

    public string NameU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtNameToU.Text))
                _nameU = txtNameToU.Text;
            else _nameU = "";

            return _nameU;
        }

        set
        {
            _nameU = value;
        }
    }

    public string DescriptionU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtDescriptionToU.Text))
                _descriptionU = txtDescriptionToU.Text;
            else _descriptionU = "";

            return _descriptionU;
        }

        set
        {
            _descriptionU = value;
        }
    }




    public string CodeS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
                _codeS = txtCode.Text;
            else _codeS = "";

            return _codeS;
        }

        set
        {
            _codeS = value;
        }
    }

    public string NameS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtName.Text))
                _nameS = txtName.Text;
            else _nameS = "";
            return _nameS;
        }

        set
        {
            _nameS = value;
        }
    }

    public string DescriptionS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtDescription.Text))
                _descriptionS = txtDescription.Text;
            else _descriptionS = "";

            return _descriptionS;
        }

        set
        {
            _descriptionS = value;
        }
    }

    public int CurrentUserIdU
    {
        get
        {
            _currentUserIdU = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _currentUserIdU;
        }

        set
        {
            _currentUserIdU = value;
        }
    }

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {

            if (!IsPostBack)
            {
                GetPivotGroupList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetPivotGroupList();
    }

    protected void gvPivotGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPid = gvPivotGroup.Rows[rowindex].FindControl("lblPid") as Label;
                Label lblCode = gvPivotGroup.Rows[rowindex].FindControl("lblCode") as Label;
                Label lblName = gvPivotGroup.Rows[rowindex].FindControl("lblName") as Label;
                Label lblDescription = gvPivotGroup.Rows[rowindex].FindControl("lblDescription") as Label;

                ViewState["PID"] = Convert.ToInt32(lblPid.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    txtCodeToU.Text = Convert.ToString(lblCode.Text);
                    txtNameToU.Text = Convert.ToString(lblName.Text);
                    txtDescriptionToU.Text = Convert.ToString(lblDescription.Text);

                    MpeInsertUpdatePivotGroup.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    protected void gvPivotGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        Reset();
        MpeInsertUpdatePivotGroup.Show();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdatePivotGroup();

        GetPivotGroupList();
    }

    #endregion


    #region METHODS[=======================]

    private void GetPivotGroupList()
    {
        try
        {
            dsPivotGroup = objProjectManagement.GetPivotGroupList(CodeS, NameS, DescriptionS);

            if (dsPivotGroup.Tables.Count > 0 && dsPivotGroup.Tables[0].Rows.Count > 0)
            {
                gvPivotGroup.DataSource = dsPivotGroup.Tables[0];
                gvPivotGroup.DataBind();
                lblRecords.Text = "Records[" + dsPivotGroup.Tables[0].Rows.Count + "]";
            }
            else
            {
                gvPivotGroup.DataSource = null;
                gvPivotGroup.DataBind();
                lblRecords.Text = "Records[0]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InsertUpdatePivotGroup()
    {
        try
        {
            if (Convert.ToInt32(ViewState["PID"]) != 0)
                PIdU = Convert.ToInt32(ViewState["PID"]);
            else PIdU = 0;


            int insertedId = 0;
            string pivotGroupName = string.Empty;

            string retValue = objProjectManagement.InsertUpdatePivotGroup(PIdU, NameU, DescriptionU, CurrentUserIdU);

            if (!string.IsNullOrEmpty(retValue))
            {
                insertedId = Convert.ToInt32(retValue.Split(':')[0]);
                pivotGroupName = Convert.ToString(retValue.Split(':')[1]);

                if (insertedId == 0) return;

                if (PIdU > 0) SuccessMessage("Pivot Group: '" + pivotGroupName + "' updated successfully.");
                else SuccessMessage("Pivot Group: '" + pivotGroupName + "' added successfully.");

                Reset();

                return;
            }
            else
            {
                ExceptionMessage("Please try again");
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
        ViewState["PID"] = "0";
        txtCodeToU.Text = string.Empty;
        txtNameToU.Text = string.Empty;
        txtDescriptionToU.Text = string.Empty;
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

    #endregion

}