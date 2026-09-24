using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

public partial class FINANCE_BUDGET_MASTER_GL_MASTER_AddGLType : System.Web.UI.Page
{

    #region VARIABLES[==========================]
    BAL.Finance _objFinance = new BAL.Finance();
    int _recordId = 0;
    string _glType = string.Empty;
    string _glTypeDesc = string.Empty;

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            InsertGLType();
        }
    }

    protected void btnGLTypeList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINANCE/BUDGET/MASTER/GL_MASTER/GLTypeList.aspx");
    }

    #endregion


    #region METHODS[============================]



    private void InsertGLType()
    {
        try
        {
            _recordId = 0;
            _glType = "";
            _glTypeDesc = "";

            if (!string.IsNullOrEmpty(txtGLType.Text))
                _glType = txtGLType.Text.Trim();

            if (!string.IsNullOrEmpty(txtGLTypeDescription.Text))
                _glTypeDesc = txtGLTypeDescription.Text.Trim();

            int value = _objFinance.InsertUpdateGLType(0, _glType, _glTypeDesc, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("GL Type '" + _glType + "' added successfully.");
                Reset();
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

    private void Reset()
    {
        txtGLType.Text = string.Empty;
        txtGLTypeDescription.Text = string.Empty;
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