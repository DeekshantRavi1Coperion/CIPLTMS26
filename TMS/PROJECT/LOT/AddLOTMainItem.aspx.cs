using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using System.Text;
using System.Net.Mime;

public partial class PROJECT_LOT_AddLOTMainItem : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsEmployee = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsJobNo = new DataSet();

    string dbName = string.Empty;


    string mainItem = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewMainItem();
    }

    protected void btnLOTMainItemList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/LOTMainItemList.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void AddNewMainItem()
    {
        try
        {
            mainItem = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(txtMainItem.Text)))
                mainItem = Convert.ToString(txtMainItem.Text);

            int value = 0;
            value = objProject.AddUpdateMainItem(0, mainItem, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("LOT Main item added successfully..!!");
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Main Item already existed...!!!");
                return;
            }
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
