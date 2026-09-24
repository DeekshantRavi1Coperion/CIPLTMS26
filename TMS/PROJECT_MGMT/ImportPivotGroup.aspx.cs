using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.OleDb;

public partial class PROJECT_MGMT_ImportPivotGroup : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsGrossMarginLine = new DataSet();

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HidePanel();
                Session["dt"] = null;
                Session["dsGrossMarginLine"] = null;

                dsGrossMarginLine = objProject.GetDetailsBySP("sp_get_gross_margin_line");
                if (dsGrossMarginLine.Tables.Count > 0 && dsGrossMarginLine.Tables[0].Rows.Count > 0)
                    Session["dsGrossMarginLine"] = dsGrossMarginLine;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnGetProjectPivotGroup_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetProjectPivotFile();
    }

    protected void btnImportProjectPivotGroup_Click(object sender, EventArgs e)
    {
        HidePanel();
        if (gvProjectPivotGroup.Rows.Count > 0)
        {
            ImportProjectPivotGroup();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }

    protected void gvProjectPivotGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlGrossMarginLine = (DropDownList)e.Row.FindControl("ddlGrossMarginLine");

                DataSet dsGrossMarginLineNew = (DataSet)Session["dsGrossMarginLine"];
                if (dsGrossMarginLineNew.Tables.Count > 0 && dsGrossMarginLineNew.Tables[0].Rows.Count > 0)
                {
                    ddlGrossMarginLine.DataSource = dsGrossMarginLineNew.Tables[0];
                    ddlGrossMarginLine.DataTextField = "GROSS_MARGIN_LINE";
                    ddlGrossMarginLine.DataValueField = "GROSS_MARGIN_LINE_ID";
                    ddlGrossMarginLine.DataBind();
                    ddlGrossMarginLine.Items.Insert(0, "Select");
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[==================]

    private void GetProjectPivotFile()
    {
        try
        {
            int serialNo = -1;
            string fileName = string.Empty;

            if (fileUploadProjectPivotGroup.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadProjectPivotGroup.PostedFile.FileName))
                {                   
                    DataTable dt = new DataTable();

                    dt.Columns.Add("SERIAL_NO", typeof(string));
                    dt.Columns.Add("JOB_NO", typeof(string));
                    dt.Columns.Add("PIVOT_GROUP", typeof(string));
                    dt.Columns.Add("0", typeof(string));
                    dt.Columns.Add("C", typeof(string));
                    dt.Columns.Add("E", typeof(string));
                    dt.Columns.Add("F", typeof(string));
                    dt.Columns.Add("I", typeof(string));
                    dt.Columns.Add("N", typeof(string));
                    dt.Columns.Add("OI", typeof(string));
                    dt.Columns.Add("P", typeof(string));
                    dt.Columns.Add("TOTAL", typeof(string));
                    dt.Columns.Add("GROSS_MARGIN_LINE_ID", typeof(string));



                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadProjectPivotGroup.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();
                    //Response.Write(output);  
                    
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();

                            int i = 0;
                            foreach (string cell in row.Split(','))
                            {
                                string dd = cell;

                                if (i <= (dt.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dd))
                                        dt.Rows[dt.Rows.Count - 1][i + 1] = cell;
                                    else
                                        dt.Rows[dt.Rows.Count - 1][i + 1] = string.Empty;

                                    i++;
                                }
                            }
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["SERIAL_NO"] = Convert.ToString(i);
                        }
                        dt.Rows.RemoveAt(0);
                        Session["dt"] = dt;
                        gvProjectPivotGroup.DataSource = dt;
                        gvProjectPivotGroup.DataBind();

                    }
                    else
                    {
                        Session["dt"] = null;
                        gvProjectPivotGroup.DataSource = null;
                        gvProjectPivotGroup.DataBind();
                    }


                    lblRecords.Text = "Records[" + dt.Rows.Count + "]";
                }
                else
                {
                    fileName = string.Empty;
                }
            }
            else
            {
                fileName = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ImportProjectPivotGroup()
    {
        try
        {
            bool chk = false;
            string serialNo = string.Empty;
            string serialNos = string.Empty;
            string jobNo = string.Empty;
            string pivotGroup = string.Empty;
            double col0 = 0;
            double colC = 0;
            double colE = 0;
            double colF = 0;
            double colI = 0;
            double colN = 0;
            double colOI = 0;
            double colP = 0;
            double total = 0;
            int grossMarginLineID = 0;
            string udf1 = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;
            int value = 0;
            int count = 0;

            foreach (GridViewRow gr in gvProjectPivotGroup.Rows)
            {

                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblPivotGroup = (Label)gr.FindControl("lblPivotGroup");
                Label lbl0 = (Label)gr.FindControl("lbl0");
                Label lblC = (Label)gr.FindControl("lblC");
                Label lblE = (Label)gr.FindControl("lblE");
                Label lblF = (Label)gr.FindControl("lblF");
                Label lblI = (Label)gr.FindControl("lblI");
                Label lblN = (Label)gr.FindControl("lblN");
                Label lblOI = (Label)gr.FindControl("lblOI");
                Label lblP = (Label)gr.FindControl("lblP");
                Label lblTotal = (Label)gr.FindControl("lblTotal");
                DropDownList ddlGrossMarginLine = (DropDownList)gr.FindControl("ddlGrossMarginLine");
                TextBox txtUDF1 = (TextBox)gr.FindControl("txtUDF1");
                TextBox txtUDF2 = (TextBox)gr.FindControl("txtUDF2");
                TextBox txtUDF3 = (TextBox)gr.FindControl("txtUDF3");
                TextBox txtUDF4 = (TextBox)gr.FindControl("txtUDF4");
                TextBox txtUDF5 = (TextBox)gr.FindControl("txtUDF5");

                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    serialNo = Convert.ToString(lblSerialNo.Text);
                else
                    serialNo = "0";

                if (!string.IsNullOrEmpty(lblJobNo.Text))
                    jobNo = Convert.ToString(lblJobNo.Text);
                else
                    jobNo = string.Empty;

                if (!string.IsNullOrEmpty(lblPivotGroup.Text))
                    pivotGroup = Convert.ToString(lblPivotGroup.Text);
                else
                    pivotGroup = string.Empty;

                if (!string.IsNullOrEmpty(lbl0.Text))
                    col0 = Convert.ToDouble(lbl0.Text);
                else
                    col0 = 0;

                if (!string.IsNullOrEmpty(lblC.Text))
                    colC = Convert.ToDouble(lblC.Text);
                else
                    colC = 0;

                if (!string.IsNullOrEmpty(lblE.Text))
                    colE = Convert.ToDouble(lblE.Text);
                else
                    colE = 0;

                if (!string.IsNullOrEmpty(lblF.Text))
                    colF = Convert.ToDouble(lblF.Text);
                else
                    colF = 0;

                if (!string.IsNullOrEmpty(lblI.Text))
                    colI = Convert.ToDouble(lblI.Text);
                else
                    colI = 0;

                if (!string.IsNullOrEmpty(lblN.Text))
                    colN = Convert.ToDouble(lblN.Text);
                else
                    colN = 0;

                if (!string.IsNullOrEmpty(lblOI.Text))
                    colOI = Convert.ToDouble(lblOI.Text);
                else
                    colOI = 0;

                if (!string.IsNullOrEmpty(lblP.Text))
                    colP = Convert.ToDouble(lblP.Text);
                else
                    colP = 0;

                if (!string.IsNullOrEmpty(lblTotal.Text))
                    total = Convert.ToDouble(lblTotal.Text);
                else
                    total = 0;

                if (ddlGrossMarginLine.SelectedIndex > 0)
                {
                    grossMarginLineID = Convert.ToInt32(ddlGrossMarginLine.SelectedValue);
                    chk = true;
                }
                else
                {
                    grossMarginLineID = 0;
                    chk = false;
                }


                if (!string.IsNullOrEmpty(txtUDF1.Text))
                    udf1 = Convert.ToString(txtUDF1.Text);
                else
                    udf1 = string.Empty;

                if (!string.IsNullOrEmpty(txtUDF2.Text))
                    udf2 = Convert.ToString(txtUDF2.Text);
                else
                    udf2 = string.Empty;

                if (!string.IsNullOrEmpty(txtUDF3.Text))
                    udf3 = Convert.ToString(txtUDF3.Text);
                else
                    udf3 = string.Empty;

                if (!string.IsNullOrEmpty(txtUDF4.Text))
                    udf4 = Convert.ToString(txtUDF4.Text);
                else
                    udf4 = string.Empty;

                if (!string.IsNullOrEmpty(txtUDF5.Text))
                    udf5 = Convert.ToString(txtUDF5.Text);
                else
                    udf5 = string.Empty;

                if (chk)
                {
                    value = objProject.ImportProjectPivotGroup(jobNo, pivotGroup, col0, colC, colE, colF, colI, colN, colOI, colP, total,
                                                            grossMarginLineID, udf1, udf2, udf3, udf4, udf5, Convert.ToInt32(Session["EMP_RECORD_ID"]));


                    if (value > 0)
                    {
                        count++;
                        serialNos += serialNo + ",";
                    }
                }
            }

            if (count > 0)
            {
                serialNos = serialNos.TrimEnd(',');
                RemoveRecords(serialNos);
                SuccessMessage(count + " Records Imported successfully.");                
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecords(string serialNos)
    {
        string[] strSerialNo = serialNos.Split(',');
        DataTable dtNew = (DataTable)Session["dt"];

        if (dtNew.Rows.Count > 0)
        {
            foreach (string sr in strSerialNo)
            {
                if (!string.IsNullOrEmpty(sr))
                {
                    foreach (DataRow drremove in dtNew.Select("SERIAL_NO='" + sr + "'"))
                    {
                        dtNew.Rows.Remove(drremove);
                    }
                }
            }

            if (dtNew.Rows.Count > 0)
            {
                Session["dt"] = dtNew;
                gvProjectPivotGroup.DataSource = dtNew;
                gvProjectPivotGroup.DataBind();
            }
            else
            {
                Session["dt"] = null;
                gvProjectPivotGroup.DataSource = null;
                gvProjectPivotGroup.DataBind();
            }
        }
        else
        {
            Session["dt"] = null;
        }
        lblRecords.Text = "Records[" + gvProjectPivotGroup.Rows.Count + "]";
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
