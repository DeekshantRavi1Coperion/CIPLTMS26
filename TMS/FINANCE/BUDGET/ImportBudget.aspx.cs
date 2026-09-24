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

public partial class FINANCE_BUDGET_ImportBudget : System.Web.UI.Page
{

    #region VARIABLES[================]
    BAL.Finance _objFinance = new BAL.Finance();


    BAL.Reports _objReports = new BAL.Reports();
    BAL.Common _objCommon = new BAL.Common();

    DataTable _dtCSVBudget = new DataTable();
    DataSet _dsGLCodesAndBudget = new DataSet();

    DataSet _dsGLCodeList = new DataSet();
    DataSet _dsPeriodTypList = new DataSet();
    DataSet _dsU7TList = new DataSet();

    int _existedRecrdsCount = 0;
    int _notImportingCount = 0;

    int _unitId = 0;
    string _unitName = string.Empty;


    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["dtCSVBudget"] = null;
                Session["dtBudget"] = null;
                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnGetFormat_Click(object sender, EventArgs e)
    {
        DownloadFormat();
    }

    protected void btnGetBudgetDetails_Click(object sender, EventArgs e)
    {
        GetGLCodeList();
        GetScenarioList();
        GetBudgetDetails();
    }

    protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtGL = new DataTable();
                DataTable dtPT = new DataTable();
                DataTable dtU7T = new DataTable();

                if (Session["dtGLCodeList"] != null)
                    dtGL = (DataTable)Session["dtGLCodeList"];
                else
                {
                    GetGLCodeList();
                    dtGL = (DataTable)Session["dtGLCodeList"];
                }

                if (Session["dtPeriodTypdList"] != null)
                    dtPT = (DataTable)Session["dtPeriodTypdList"];
                else
                {
                    GetScenarioList();
                    dtPT = (DataTable)Session["dtPeriodTypdList"];
                }

                //if (Session["dtU7TList"] != null)
                //    dtU7T = (DataTable)Session["dtU7TList"];
                //else
                //{
                //    GetU7TList();
                //    dtU7T = (DataTable)Session["dtU7TList"];
                //}

                //Label lblMonth = (Label)e.Row.FindControl("lblMonth");
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                Label lblGLPID = (Label)e.Row.FindControl("lblGLPID");
                Label lblScenarioID = (Label)e.Row.FindControl("lblScenarioID");

                DropDownList ddlGLCode = (DropDownList)e.Row.FindControl("ddlGLCode");
                DropDownList ddlScenario = (DropDownList)e.Row.FindControl("ddlScenario");


                ddlGLCode.Items.Clear();
                ddlGLCode.Items.Insert(0, "Select");
                ddlGLCode.SelectedIndex = 0;

                if (dtGL != null && dtGL.Rows.Count > 0)
                {
                    ddlGLCode.DataSource = dtGL;
                    ddlGLCode.DataTextField = "GL_CODE";
                    ddlGLCode.DataValueField = "PID";
                    ddlGLCode.DataBind();

                    ddlGLCode.Items.Insert(0, "Select");

                    if (!string.IsNullOrEmpty(lblGLPID.Text) && Convert.ToInt32(lblGLPID.Text) > 0)
                    {
                        ddlGLCode.SelectedValue = lblGLPID.Text;
                        ddlGLCode.BackColor = System.Drawing.Color.LightGray;
                    }
                    else
                    {
                        ddlGLCode.SelectedIndex = 0;
                        ddlGLCode.BackColor = System.Drawing.Color.Pink;
                    }
                }

                ddlScenario.Items.Clear();
                ddlScenario.Items.Insert(0, "Select");
                ddlScenario.SelectedIndex = 0;

                if (dtPT != null && dtPT.Rows.Count > 0)
                {
                    ddlScenario.DataSource = dtPT;
                    ddlScenario.DataTextField = "SCENARIO";
                    ddlScenario.DataValueField = "SCENARIO_PID";
                    ddlScenario.DataBind();

                    ddlScenario.Items.Insert(0, "Select");

                    if (!string.IsNullOrEmpty(lblScenarioID.Text) && Convert.ToInt32(lblScenarioID.Text) > 0)
                    {
                        ddlScenario.SelectedValue = lblScenarioID.Text;
                        ddlScenario.BackColor = System.Drawing.Color.LightGray;
                    }
                    else
                    {
                        ddlScenario.SelectedIndex = 0;
                        ddlScenario.BackColor = System.Drawing.Color.Pink;
                    }
                }




                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    _existedRecrdsCount++;
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
                    }
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

    protected void ddlGLCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlGLCode = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlGLCode.Parent.Parent;

            Label lblGLPID = (Label)gr.FindControl("lblGLPID");
            TextBox txtGLDescription = (TextBox)gr.FindControl("txtGLDescription");
            TextBox txtGLType = (TextBox)gr.FindControl("txtGLType");
            TextBox txtGLSubType = (TextBox)gr.FindControl("txtGLSubType");

            if (ddlGLCode.SelectedIndex > 0)
            {
                DataTable dtGL = new DataTable();
                if (Session["dtGLCodeList"] != null)
                    dtGL = (DataTable)Session["dtGLCodeList"];
                else
                {
                    GetGLCodeList();
                    dtGL = (DataTable)Session["dtGLCodeList"];
                }

                if (dtGL.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGL.Select("PID=" + Convert.ToInt32(ddlGLCode.SelectedValue)))
                    {
                        lblGLPID.Text = Convert.ToString(dr["PID"]);

                        if (dr["GL_DESCRIPTION"] != DBNull.Value)
                            txtGLDescription.Text = Convert.ToString(dr["GL_DESCRIPTION"]);
                        else txtGLDescription.Text = string.Empty;

                        if (dr["GL_TYPE"] != DBNull.Value)
                            txtGLType.Text = Convert.ToString(dr["GL_TYPE"]);
                        else txtGLType.Text = string.Empty;

                        if (dr["GL_SUBTYPE"] != DBNull.Value)
                            txtGLSubType.Text = Convert.ToString(dr["GL_SUBTYPE"]);
                        else txtGLSubType.Text = string.Empty;
                    }
                }
            }
            else
            {
                lblGLPID.Text = "0";
                txtGLDescription.Text = string.Empty;
                txtGLType.Text = string.Empty;
                txtGLSubType.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvBudget.Rows.Count > 0)
        {
            ImportBudget();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }


    #endregion


    #region METHODS[==================]

    private void GetGLCodeList()
    {
        try
        {
            Session["dtGLCodeList"] = null;
            _dsGLCodeList = _objFinance.GetGLCodeList(0, "", 0, 0);
            if (_dsGLCodeList.Tables.Count > 0 && _dsGLCodeList.Tables[0].Rows.Count > 0)
                Session["dtGLCodeList"] = _dsGLCodeList.Tables[0];
        }
        catch (Exception ex)
        {
            Session["dtGLCodeList"] = null;
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetScenarioList()
    {
        try
        {
            Session["dtPeriodTypdList"] = null;
            _dsPeriodTypList = _objFinance.GetScenarioList(0, "");
            if (_dsPeriodTypList.Tables.Count > 0 && _dsPeriodTypList.Tables[0].Rows.Count > 0)
                Session["dtPeriodTypdList"] = _dsPeriodTypList.Tables[0];
        }
        catch (Exception ex)
        {
            Session["dtPeriodTypdList"] = null;
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void GetU7TList()
    //{
    //    try
    //    {
    //        Session["dtU7TList"] = null;
    //        _dsU7TList = _objFinance.GetU7TList(0, "");
    //        if (_dsU7TList.Tables.Count > 0 && _dsU7TList.Tables[0].Rows.Count > 0)
    //            Session["dtU7TList"] = _dsU7TList.Tables[0];
    //    }
    //    catch (Exception ex)
    //    {
    //        Session["dtU7TList"] = null;
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}


    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            //csv  = "YEAR" + ',';
            //csv += "PERIOD" + ',';
            //csv += "QUARTER" + ',';
            //csv += "OS_TMT" + ',';
            csv += "GL_CODE" + ',';

            //csv += "GL_TYPE" + ',';
            //csv += "GL_SUBTYPE" + ',';
            //csv += "U7T" + ',';
            csv += "SCENARIO" + ',';
            csv += "AMOUNT" + ',';
            csv += "\r\n";

            string fileName = "Budget_Format";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetBudgetDetails()
    {
        try
        {
            #region CREATE_TABLE

            _dtCSVBudget.Columns.Add("GL_CODE", typeof(string));
            _dtCSVBudget.Columns.Add("SCENARIO", typeof(string));
            _dtCSVBudget.Columns.Add("AMOUNT", typeof(double));

            _dtCSVBudget.Columns.Add("GL_DESCRIPTION", typeof(string));
            _dtCSVBudget.Columns.Add("GL_TYPE", typeof(string));
            _dtCSVBudget.Columns.Add("GL_SUBTYPE", typeof(string));
            _dtCSVBudget.Columns.Add("U7T", typeof(string));
            _dtCSVBudget.Columns.Add("GL_FID", typeof(int));
            _dtCSVBudget.Columns.Add("GL_TYPE_FID", typeof(int));
            _dtCSVBudget.Columns.Add("U7T_FID", typeof(int));
            _dtCSVBudget.Columns.Add("GL_SUBTYPE_FID", typeof(int));
            _dtCSVBudget.Columns.Add("SCENARIO_FID", typeof(string));
            _dtCSVBudget.Columns.Add("YEAR", typeof(int));
            _dtCSVBudget.Columns.Add("PERIOD", typeof(int));
            _dtCSVBudget.Columns.Add("QUARTER", typeof(int));
            _dtCSVBudget.Columns.Add("OS_TMT", typeof(string));
            _dtCSVBudget.Columns.Add("SR_NO", typeof(int));
            _dtCSVBudget.Columns.Add("RECORD_ID", typeof(int));
            _dtCSVBudget.Columns.Add("COMPOSITE_COL", typeof(string));
            _dtCSVBudget.Columns.Add("DELETE_FLAG", typeof(int));

            #endregion

            string glCodeText = string.Empty;
            string periodDate = string.Empty;
            periodDate = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

            if (fileUploadBudget.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadBudget.PostedFile.FileName))
                {
                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadBudget.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row) && !Convert.ToString(row).Contains("GL_CODE"))
                        {
                            string glCodeNew = "";
                            string[] str = row.Split(',');
                            string glCode = str[0].ToString();
                            glCodeNew = GetGLCodeText(glCode);

                            if (!string.IsNullOrEmpty(glCodeNew))
                                glCodeText += "'" + glCodeNew + "',";

                            _dtCSVBudget.Rows.Add();

                            int i = 0;
                            string dtColunValue = string.Empty;
                            foreach (string cell in row.Split(','))
                            {
                                dtColunValue = cell.Trim();

                                if (i <= (_dtCSVBudget.Columns.Count - 1))
                                {
                                    string colValue = string.Empty;
                                    string colValueNew = string.Empty;


                                    if (!string.IsNullOrEmpty(dtColunValue))
                                    {
                                        colValue = dtColunValue;

                                        if (_dtCSVBudget.Columns[0].ColumnName == "GL_CODE") colValueNew = GetGLCodeText(colValue);
                                        else colValueNew = colValue;
                                    }
                                    _dtCSVBudget.Rows[_dtCSVBudget.Rows.Count - 1][i] = colValueNew;

                                    //if (!string.IsNullOrEmpty(dtColunValue))
                                    //    _dtCSVBudget.Rows[_dtCSVBudget.Rows.Count - 1][i] = dtColunValue;
                                    //else
                                    //    _dtCSVBudget.Rows[_dtCSVBudget.Rows.Count - 1][i] = string.Empty;

                                    i++;
                                }
                            }
                        }
                    }
                    glCodeText = glCodeText.TrimEnd(',');
                }
            }

            if (!string.IsNullOrEmpty(glCodeText)) _dsGLCodesAndBudget = _objFinance.GetGLAndScenarioMasterListAndBudget(glCodeText, periodDate, 0);
            else _dsGLCodesAndBudget = null;

            if (_dsGLCodesAndBudget != null && _dsGLCodesAndBudget.Tables.Count > 0)
            {
                if (_dtCSVBudget.Rows.Count > 0)
                {
                    if (_dsGLCodesAndBudget.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in _dsGLCodesAndBudget.Tables[0].Rows)
                        {
                            foreach (DataRow drcsv in _dtCSVBudget.Select("GL_CODE='" + Convert.ToString(dr1["GL_CODE"]) + "'"))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["GL_CODE"])))
                                    drcsv["GL_CODE"] = Convert.ToString(dr1["GL_CODE"]);
                                else drcsv["GL_CODE"] = "";

                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["GL_DESCRIPTION"])))
                                    drcsv["GL_DESCRIPTION"] = Convert.ToString(dr1["GL_DESCRIPTION"]);
                                else drcsv["GL_DESCRIPTION"] = "";

                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["PID"])) && Convert.ToInt32(dr1["PID"]) > 0)
                                    drcsv["GL_FID"] = Convert.ToInt32(dr1["PID"]);
                                else drcsv["GL_FID"] = "0";



                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["GL_TYPE"])))
                                    drcsv["GL_TYPE"] = Convert.ToString(dr1["GL_TYPE"]);
                                else drcsv["GL_TYPE"] = "";

                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["GL_TYPE_FID"])) && Convert.ToInt32(dr1["GL_TYPE_FID"]) > 0)
                                    drcsv["GL_TYPE_FID"] = Convert.ToInt32(dr1["GL_TYPE_FID"]);
                                else drcsv["GL_TYPE_FID"] = "0";



                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["GL_SUBTYPE"])))
                                    drcsv["GL_SUBTYPE"] = Convert.ToString(dr1["GL_SUBTYPE"]);
                                else drcsv["GL_SUBTYPE"] = "";

                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["GL_SUBTYPE_FID"])) && Convert.ToInt32(dr1["GL_SUBTYPE_FID"]) > 0)
                                    drcsv["GL_SUBTYPE_FID"] = Convert.ToInt32(dr1["GL_SUBTYPE_FID"]);
                                else drcsv["GL_SUBTYPE_FID"] = "0";


                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["U7T"])))
                                    drcsv["U7T"] = Convert.ToString(dr1["U7T"]);
                                else drcsv["U7T"] = "";

                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["U7T_FID"])) && Convert.ToInt32(dr1["U7T_FID"]) > 0)
                                    drcsv["U7T_FID"] = Convert.ToInt32(dr1["U7T_FID"]);
                                else drcsv["U7T_FID"] = "0";
                            }
                        }
                    }

                    if (_dsGLCodesAndBudget.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in _dsGLCodesAndBudget.Tables[1].Rows)
                        {
                            foreach (DataRow drcsv in _dtCSVBudget.Select("SCENARIO='" + Convert.ToString(dr1["SCENARIO"]) + "'"))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["SCENARIO"])))
                                    drcsv["SCENARIO"] = Convert.ToString(dr1["SCENARIO"]);
                                else drcsv["SCENARIO"] = "";

                                if (!string.IsNullOrEmpty(Convert.ToString(dr1["SCENARIO_PID"])) && Convert.ToInt32(dr1["SCENARIO_PID"]) > 0)
                                    drcsv["SCENARIO_FID"] = Convert.ToInt32(dr1["SCENARIO_PID"]);
                                else drcsv["SCENARIO_FID"] = "0";
                            }
                        }
                    }
                }

                if (_dtCSVBudget.Rows.Count > 0)
                {
                    string date = "";
                    int year = 0;
                    int period = 0;
                    int quarter = 0;
                    string oSTMT = "";

                    date = hdPostingMonth.Value;
                    year = Convert.ToDateTime(date).Year;
                    period = Convert.ToDateTime(date).Month;

                    if (period >= 1 && period <= 4)
                        quarter = 1;
                    else if (period >= 5 && period <= 8)
                        quarter = 2;
                    else quarter = 3;

                    oSTMT = year + "M" + period;
                    int count = 0;
                    foreach (DataRow dr in _dtCSVBudget.Rows)
                    {
                        count++;
                        dr["SR_NO"] = count;
                        dr["YEAR"] = year;
                        dr["PERIOD"] = period;
                        dr["QUARTER"] = quarter;
                        dr["OS_TMT"] = oSTMT;
                        dr["RECORD_ID"] = 0;
                        dr["DELETE_FLAG"] = 0;

                        if (string.IsNullOrEmpty(Convert.ToString(dr["SCENARIO_FID"])))
                            dr["SCENARIO_FID"] = 0;
                    }


                    if (_dsGLCodesAndBudget.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dsGLCodesAndBudget.Tables[2].Rows)
                        {
                            foreach (DataRow drc in _dtCSVBudget.Select("GL_FID=" + dr["GL_FID"] +
                                                                  " AND YEAR=" + dr["YEAR"] +
                                                                  " AND PERIOD=" + dr["PERIOD"] +
                                                                  " AND SCENARIO_FID=" + dr["SCENARIO_FID"]))
                            {
                                drc["RECORD_ID"] = dr["PID"];
                            }
                        }
                    }

                    foreach (DataRow dr in _dtCSVBudget.Rows)
                    {
                        dr["COMPOSITE_COL"] = Convert.ToString(dr["GL_FID"])
                                            + Convert.ToString(dr["YEAR"])
                                            + Convert.ToString(dr["PERIOD"])
                                            + Convert.ToString(dr["SCENARIO_FID"]);
                    }


                    _dtCSVBudget = RemoveDuplicatesRecords(_dtCSVBudget);
                }

                Session["dtCSVBudget"] = _dtCSVBudget;
                gvBudget.DataSource = _dtCSVBudget;
                gvBudget.DataBind();
                lblRecords.Text = "Records[" + _dtCSVBudget.Rows.Count + "], Already Exists[" + _existedRecrdsCount + "]";
                hdExistedRecords.Value = _existedRecrdsCount.ToString();
            }
            else
            {
                Session["dtCSVBudget"] = null;
                gvBudget.DataSource = null;
                gvBudget.DataBind();
                hdExistedRecords.Value = "0";
                lblRecords.Text = "Records[0], Already Exists[0]";
                ExceptionMessage("No data found..!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private string GetGLCodeText(string glCode)
    {
        try
        {
            string glCodeNew = "";
            int n;
            bool isNumeric = int.TryParse(glCode, out n);
            if (isNumeric)
            {
                if (glCode == "600" || glCode == "910" || glCode == "915") glCodeNew = glCode;
                else
                {
                    int length = 4 - glCode.Length;
                    string zeros = "";
                    if (length > 0)
                    {
                        for (int l = 0; l < length; l++)
                        {
                            zeros += "0";
                        }
                        glCodeNew = zeros + glCode;
                    }
                    else glCodeNew = glCode;
                }
            }
            else glCodeNew = glCode;

            return glCodeNew;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private DataTable RemoveDuplicatesRecords(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        dtNew = dt.Copy();

        int count = 0;
        string drTxt = "";

        foreach (DataRow dr in dt.Rows)
        {
            count = 0;
            drTxt = Convert.ToString(dr["GL_FID"]) + Convert.ToString(dr["YEAR"]) + Convert.ToString(dr["PERIOD"]) + Convert.ToString(dr["SCENARIO_FID"]);

            var query = dtNew.AsEnumerable().Where(r => r.Field<string>("COMPOSITE_COL") == drTxt);

            foreach (var row in query.ToList())
            {
                count++;
                if (count > 1)
                {
                    row.Delete();
                    count = 0;
                }
            }
        }

        return dtNew;
    }


    private void ImportBudget()
    {
        try
        {
            string updateQuery = string.Empty;
            string srNoForRemoval = string.Empty;
            int srNo = 0;
            int updateCount = 0;
            int insertCount = 0;
            int recordID = 0;
            int year = 0;
            int period = 0;
            int quarter = 0;
            string osTMT = string.Empty;
            int gLCodeId = 0;
            string u7t = string.Empty;
            int scenarioId = 0;
            decimal amount = 0;

            DataTable dtBudget = new DataTable();

            dtBudget.Columns.Add("YEAR", typeof(int));
            dtBudget.Columns.Add("PERIOD", typeof(int));
            dtBudget.Columns.Add("QUARTER", typeof(int));
            dtBudget.Columns.Add("OS_TMT", typeof(string));
            dtBudget.Columns.Add("GL_FID", typeof(int));
            dtBudget.Columns.Add("SCENARIO_FID", typeof(int));
            dtBudget.Columns.Add("AMOUNT", typeof(decimal));

            int value = 0;
            foreach (GridViewRow gr in gvBudget.Rows)
            {
                recordID = 0;
                srNo = 0;
                year = 0;
                period = 0;
                quarter = 0;
                osTMT = string.Empty;
                gLCodeId = 0;
                u7t = string.Empty;
                scenarioId = 0;
                amount = 0;

                DataRow dr = dtBudget.NewRow();

                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                TextBox txtSrNo = (TextBox)gr.FindControl("txtSrNo");
                TextBox txtYear = (TextBox)gr.FindControl("txtYear");
                TextBox txtPeriod = (TextBox)gr.FindControl("txtPeriod");
                TextBox txtQuarter = (TextBox)gr.FindControl("txtQuarter");
                TextBox txtOSTMT = (TextBox)gr.FindControl("txtOSTMT");
                DropDownList ddlGLCode = (DropDownList)gr.FindControl("ddlGLCode");
                TextBox txtU7T = (TextBox)gr.FindControl("txtU7T");
                DropDownList ddlScenario = (DropDownList)gr.FindControl("ddlScenario");
                TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");

                srNo = Convert.ToInt32(txtSrNo.Text);

                if (!string.IsNullOrEmpty(lblRecordID.Text) && Convert.ToInt32(lblRecordID.Text) > 0) recordID = Convert.ToInt32(lblRecordID.Text);
                if (!string.IsNullOrEmpty(txtYear.Text)) year = Convert.ToInt32(txtYear.Text);
                if (!string.IsNullOrEmpty(txtPeriod.Text)) period = Convert.ToInt32(txtPeriod.Text);
                if (!string.IsNullOrEmpty(txtQuarter.Text)) quarter = Convert.ToInt32(txtQuarter.Text);
                if (!string.IsNullOrEmpty(txtOSTMT.Text)) osTMT = Convert.ToString(txtOSTMT.Text);
                if (ddlGLCode.SelectedIndex > 0) gLCodeId = Convert.ToInt32(ddlGLCode.SelectedValue);
                if (!string.IsNullOrEmpty(txtU7T.Text)) u7t = Convert.ToString(txtU7T.Text);
                if (ddlScenario.SelectedIndex > 0) scenarioId = Convert.ToInt32(ddlScenario.SelectedValue);
                if (!string.IsNullOrEmpty(txtAmount.Text)) amount = Convert.ToDecimal(txtAmount.Text);

                if (gLCodeId > 0 && scenarioId > 0)
                {

                    if (recordID > 0)
                    {
                        updateCount++;
                        srNoForRemoval += srNo + ",";

                        updateQuery += "UPDATE tblBUDBudget" +
                                        " SET" +
                                        "  [YEAR] = year" +
                                        ", [QUARTER]          =" + quarter +
                                        ", [PERIOD]           =" + period +
                                        ", [OS_TMT]           ='" + osTMT +
                                        "',[GL_FID]           =" + gLCodeId +
                                        "',[SCENARIO_FID]  =" + scenarioId +
                                        ", [AMOUNT]           ='" + amount +
                                        "',[MODIFIED_BY]      =" + Convert.ToInt32(Session["EMP_RECORD_ID"]) +
                                        ", [MODIFIED_ON]      =   GETDATE() " +
                                        "  WHERE PID =" + recordID + Environment.NewLine;
                    }
                    else
                    {
                        insertCount++;
                        srNoForRemoval += srNo + ",";

                        dr["YEAR"] = year;
                        dr["PERIOD"] = period;
                        dr["QUARTER"] = quarter;
                        dr["OS_TMT"] = osTMT;
                        dr["GL_FID"] = gLCodeId;
                        dr["SCENARIO_FID"] = scenarioId;
                        dr["AMOUNT"] = amount;

                        dtBudget.Rows.Add(dr);
                    }
                }
                else
                {
                    _notImportingCount++;
                }
            }

            if (Convert.ToInt32(hdReplacementFlag.Value) > 0)
            {
                if (!string.IsNullOrEmpty(updateQuery))
                    updateQuery = updateQuery.TrimEnd(';').Trim();
            }
            else updateQuery = string.Empty;


            if (dtBudget.Rows.Count > 0 || !string.IsNullOrEmpty(updateQuery))
            {
                value = _objFinance.InsertUpdateBudget(0, dtBudget, updateQuery, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }

            if (value > 0)
            {
                if (dtBudget.Rows.Count > 0 && !string.IsNullOrEmpty(updateQuery))
                    SuccessMessage(insertCount + " record(s) imported and " + updateCount + " record(s) updated successfully.");

                else if (dtBudget.Rows.Count > 0 && string.IsNullOrEmpty(updateQuery))
                    SuccessMessage(insertCount + " record(s) imported successfully.");

                else if (dtBudget.Rows.Count == 0 && !string.IsNullOrEmpty(updateQuery))
                    SuccessMessage(updateCount + " record(s) updated successfully.");


                if (_notImportingCount > 0)
                    ExceptionMessageNotImported("Please enter GL Code or Period type...");


                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvBudget.DataSource = null;
                    gvBudget.DataBind();
                    lblRecords.Text = "Records[" + gvBudget.Rows.Count + "]";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveAndBind(string srNoForRemoval)
    {
        try
        {
            _dtCSVBudget = (DataTable)Session["dtCSVBudget"];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in _dtCSVBudget.Select("SR_NO='" + item + "'"))
                {
                    _dtCSVBudget.Rows.Remove(dr);
                }
            }
            if (_dtCSVBudget.Rows.Count > 0)
            {
                gvBudget.DataSource = _dtCSVBudget;
                gvBudget.DataBind();
            }
            else
            {
                gvBudget.DataSource = null;
                gvBudget.DataBind();
            }
            lblRecords.Text = "Records[" + gvBudget.Rows.Count + "]";
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
    private void ExceptionMessageNotImported(string message)
    {
        pnlMsg1.Visible = true;
        lblMsg1.Text = message;
        lblMsg1.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;

        pnlMsg1.Visible = false;
        lblMsg1.Text = string.Empty;
    }

    #endregion    
}

