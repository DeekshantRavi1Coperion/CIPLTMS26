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

public partial class FINANCE_FDMEEPosting : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Reports objReports = new BAL.Reports();
    DataSet dsGLCode = new DataSet();
    DataSet dsFDMEE = new DataSet();
    DataTable dtFDMEE = new DataTable();
    DataTable dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    int existedRecrdsCount = 0;

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["DS_FDMEE"] = null;
                Session["DT_FDMEE"] = null;
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

    protected void btnGetFDMEEFile_Click(object sender, EventArgs e)
    {
        GetFDMEEDetail();
    }

    protected void gvFDMEE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            int year = 0;
            int period = 0;
            int quarter = 0;
            string GLAccount = string.Empty;
            string type = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblYear = (Label)e.Row.FindControl("lblYear");
                Label lblPeriod = (Label)e.Row.FindControl("lblPeriod");
                Label lblQuarter = (Label)e.Row.FindControl("lblQuarter");
                Label lblGLAccount = (Label)e.Row.FindControl("lblGLAccount");
                Label lblType = (Label)e.Row.FindControl("lblType");


                if (Convert.ToInt32(lblYear.Text) > 0)
                    year = Convert.ToInt32(lblYear.Text);
                else
                    year = 0;

                if (Convert.ToInt32(lblPeriod.Text) > 0)
                    period = Convert.ToInt32(lblPeriod.Text);
                else
                    period = 0;

                if (Convert.ToInt32(lblQuarter.Text) > 0)
                    quarter = Convert.ToInt32(lblQuarter.Text);
                else
                    quarter = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblGLAccount.Text)))
                    GLAccount = Convert.ToString(lblGLAccount.Text);
                else
                    GLAccount = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblType.Text)))
                    type = Convert.ToString(lblType.Text);
                else
                    type = string.Empty;


                if (Session["DS_FDMEE"] != null)
                {
                    ds1 = (DataSet)Session["DS_FDMEE"];
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in ds1.Tables[0].Select("YEAR='" + year + "' AND PERIOD='" + period + "' AND QUARTER='" + quarter + "' AND GL_ACCOUNT='" + GLAccount + "' AND TYPE='" + type + "'"))
                        {
                            existedRecrdsCount++;
                            for (int i = 0; i < e.Row.Cells.Count; i++)
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                            }
                        }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvFDMEE.Rows.Count > 0)
        {
            PostFDMEE();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }


    #endregion


    #region METHODS[==================]


    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv = "YEAR" + ',';
            csv += "PERIOD" + ',';
            csv += "QUARTER" + ',';
            csv += "GL_ACCOUNT" + ',';
            csv += "DESCRIPTION" + ',';
            csv += "BSPL_TYPE" + ',';
            csv += "HFM_ACCOUNT" + ',';
            csv += "HFM_ACCOUNT_DESC" + ',';
            csv += "TYPE" + ',';
            csv += "CATEGORY" + ',';
            csv += "ICP" + ',';
            csv += "HFM_CUSTOM1" + ',';
            csv += "HFMNEW_CUSTOM4" + ',';
            csv += "AMOUNT" + ',';
            csv += "SOURCE_AMOUNT" + ',';
            csv += "\r\n";

            string fileName = "FDMEE";
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

    private void GetFDMEEDetail()
    {
        int cc1 = 0;

        try
        {
            hdGVRowCount.Value = "0";
            int count = 0;

            #region CREATE_TABLE

            dtFDMEE.Columns.Add("RECORD_ID", typeof(int));
            dtFDMEE.Columns.Add("YEAR", typeof(string));
            dtFDMEE.Columns.Add("PERIOD", typeof(string));
            dtFDMEE.Columns.Add("QUARTER", typeof(string));
            dtFDMEE.Columns.Add("GL_ACCOUNT", typeof(string));
            dtFDMEE.Columns.Add("DESCRIPTION", typeof(string));
            dtFDMEE.Columns.Add("BSPL_TYPE", typeof(string));
            dtFDMEE.Columns.Add("HFM_ACCOUNT", typeof(string));
            dtFDMEE.Columns.Add("HFM_ACCOUNT_DESC", typeof(string));
            dtFDMEE.Columns.Add("TYPE", typeof(string));
            dtFDMEE.Columns.Add("CATEGORY", typeof(string));
            dtFDMEE.Columns.Add("ICP", typeof(string));
            dtFDMEE.Columns.Add("HFM_CUSTOM1", typeof(string));
            dtFDMEE.Columns.Add("HFMNEW_CUSTOM4", typeof(string));
            dtFDMEE.Columns.Add("AMOUNT", typeof(string));
            dtFDMEE.Columns.Add("SOURCE_AMOUNT", typeof(string));
            dtFDMEE.Columns.Add("SR_NO", typeof(int));

            #endregion



            if (fileUploadCostCenter.HasFile)
            {
                int csvHeaderRowColumnsCount = 0;
                int csvDataRowColumnsCount = 0;

                if (!string.IsNullOrEmpty(fileUploadCostCenter.PostedFile.FileName))
                {
                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadCostCenter.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();


                    #region MyRegion


                    //foreach (string row in csvData.Split('\n'))
                    //{
                    //    //count++;
                    //    if (!string.IsNullOrEmpty(row))
                    //    {
                    //        dtFDMEE.Rows.Add();

                    //        int i = 0;
                    //        string dtColunValue = string.Empty;
                    //        foreach (string cell in row.Split(','))
                    //        {
                    //            dtColunValue = cell.Trim();

                    //            if (i <= (dtFDMEE.Columns.Count - 1))
                    //            {
                    //                if (!string.IsNullOrEmpty(dtColunValue))
                    //                    dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = dtColunValue;
                    //                else
                    //                    dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = string.Empty;

                    //                i++;
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion


                    foreach (string row in csvData.Split('\n'))
                    {
                        cc1++;
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtFDMEE.Rows.Add();

                            int i = 0;
                            string dtColunValue = string.Empty;

                            string[] strRowText = row.Split(',');


                            if (cc1 == 1)
                                csvHeaderRowColumnsCount = strRowText.Length;
                            else if (cc1 > 1)
                                csvDataRowColumnsCount = strRowText.Length;



                            if (csvDataRowColumnsCount > csvHeaderRowColumnsCount)
                            {

                                int diff = csvDataRowColumnsCount - csvHeaderRowColumnsCount;
                                string txt = string.Empty;

                                for (int j = 1; j <= diff; j++)
                                {
                                    txt += Convert.ToString(strRowText[4 + j]) + ", ";                                    
                                }

                                if (!string.IsNullOrEmpty(txt))
                                    txt = txt.Trim();
                                
                                strRowText[4] = Convert.ToString(strRowText[4] + ", " + txt).TrimEnd(',');
                                strRowText[4] = Convert.ToString(strRowText[4]).TrimStart('"').TrimEnd('"');

                                for (int k = 5; k < csvHeaderRowColumnsCount; k++)
                                {
                                    for (int j = 1; j <= diff; j++)
                                    {
                                        strRowText[k] = Convert.ToString(strRowText[k + j]);
                                    }
                                }
                            }

                            foreach (string cell in strRowText)
                            {
                                dtColunValue = cell.Trim();

                                if (i <= (dtFDMEE.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dtColunValue))
                                    {
                                        if ((i + 1) <= 15)
                                            dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = dtColunValue;
                                    }
                                    else
                                    {
                                        if ((i + 1) <= 15)
                                            dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = string.Empty;
                                    }

                                    i++;
                                }
                            }
                        }
                    }
                }
            }


            string year = string.Empty;
            string period = string.Empty;
            string quarter = string.Empty;
            string glCodes = string.Empty;

            //string glCodes1 = string.Empty;
            //string glCodes2 = string.Empty;
            //string glCodes3 = string.Empty;

            if (dtFDMEE.Rows.Count > 0)
                dtFDMEE.Rows.RemoveAt(0);


            if (dtFDMEE.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFDMEE.Rows)
                {
                    count++;
                    dr["SR_NO"] = count;


                    year += Convert.ToString(dr["YEAR"]) + ",";
                    period += Convert.ToString(dr["PERIOD"]) + ",";
                    quarter += Convert.ToString(dr["QUARTER"]) + ",";


                    if (CheckNumeric(Convert.ToString(dr["GL_ACCOUNT"])))
                    {
                        if (Convert.ToInt32(Convert.ToString(dr["GL_ACCOUNT"]).Length) == 1)
                            dr["GL_ACCOUNT"] = "0" + Convert.ToString(dr["GL_ACCOUNT"]);

                        else if (Convert.ToInt32(Convert.ToString(dr["GL_ACCOUNT"]).Length) == 2)
                            dr["GL_ACCOUNT"] = "0" + Convert.ToString(dr["GL_ACCOUNT"]);

                        else if (Convert.ToInt32(Convert.ToString(dr["GL_ACCOUNT"]).Length) == 3)
                            dr["GL_ACCOUNT"] = "0" + Convert.ToString(dr["GL_ACCOUNT"]);
                    }

                    glCodes += "'" + Convert.ToString(dr["GL_ACCOUNT"]) + "',";
                }
            }

            if (!string.IsNullOrEmpty(year))
                year = GetDistinctSringVal(year.TrimEnd(','));

            if (!string.IsNullOrEmpty(period))
                period = GetDistinctSringVal(period.TrimEnd(','));

            if (!string.IsNullOrEmpty(quarter))
                quarter = GetDistinctSringVal(quarter.TrimEnd(','));

            if (!string.IsNullOrEmpty(glCodes))
                glCodes = GetDistinctSringVal(glCodes.TrimEnd(','));


            Session["DS_FDMEE"] = objReports.GetFDMEE(year, period, quarter);
            //dsGLCode = objReports.GetGLCodeList(glCodes);
            dsGLCode = objReports.GetGLCodeList();

            if (Session["DS_FDMEE"] != null)
                dsFDMEE = (DataSet)Session["DS_FDMEE"];



            int yearVal = 0;
            int periodVal = 0;
            int quarterVal = 0;
            string glAccount = string.Empty;
            string type = string.Empty;


            if (dsFDMEE.Tables.Count > 0 && dsFDMEE.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsFDMEE.Tables[0].Rows)
                {
                    yearVal = Convert.ToInt32(dr["YEAR"]);
                    periodVal = Convert.ToInt32(dr["PERIOD"]);
                    quarterVal = Convert.ToInt32(dr["QUARTER"]);
                    glAccount = Convert.ToString(dr["GL_ACCOUNT"]);
                    type = Convert.ToString(dr["TYPE"]);

                    foreach (DataRow dr1 in dtFDMEE.Select("YEAR='" + yearVal + "' AND PERIOD='" + periodVal + "' AND QUARTER='" + quarterVal + "' AND GL_ACCOUNT='" + glAccount + "' AND TYPE='" + type + "'"))
                    {
                        dr1["RECORD_ID"] = dr["RECORD_ID"];
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dtFDMEE.Rows)
                {
                    dr["RECORD_ID"] = 0;
                }
            }


            if (dtFDMEE.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFDMEE.Rows)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dr["RECORD_ID"])))
                        dr["RECORD_ID"] = 0;
                }
            }


            string glCode = string.Empty;
            string glDesc = string.Empty;
            string blplType = string.Empty;

            if (dtFDMEE.Rows.Count > 0)
            {
                if (dsGLCode.Tables.Count > 0 && dsGLCode.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsGLCode.Tables[0].Rows)
                    {


                        glCode = Convert.ToString(dr1["GLCODE"]);
                        glDesc = Convert.ToString(dr1["DESCRIPT"]);
                        blplType = Convert.ToString(dr1["PL_BS"]);

                        foreach (DataRow dr2 in dtFDMEE.Select("GL_ACCOUNT='" + glCode + "'"))
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dr2["DESCRIPTION"])))
                            {
                                if (!string.IsNullOrEmpty(glDesc))
                                    dr2["DESCRIPTION"] = glDesc;
                                else
                                    dr2["DESCRIPTION"] = string.Empty;
                            }

                            if (string.IsNullOrEmpty(Convert.ToString(dr2["BSPL_TYPE"])))
                            {
                                if (!string.IsNullOrEmpty(blplType))
                                    dr2["BSPL_TYPE"] = blplType;
                                else
                                    dr2["BSPL_TYPE"] = string.Empty;

                            }
                        }
                    }
                }
            }


            if (dtFDMEE.Rows.Count > 0)
            {
                Session["DT_FDMEE"] = dtFDMEE;
                gvFDMEE.DataSource = dtFDMEE;
                gvFDMEE.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvFDMEE.Rows.Count);
                lblRecords.Text = "Records[" + dtFDMEE.Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
                hdExistedRecords.Value = existedRecrdsCount.ToString();
            }
            else
            {
                Session["DT_FDMEE"] = null;
                gvFDMEE.DataSource = null;
                gvFDMEE.DataBind();
                hdGVRowCount.Value = "0";
                hdExistedRecords.Value = "0";
                lblRecords.Text = "Records[0], Already Exists[0]";
                ExceptionMessage("No data found..!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void GetFDMEEDetail()
    //{
    //    int cc1 = 0;

    //    try
    //    {
    //        hdGVRowCount.Value = "0";
    //        int count = 0;

    //        #region CREATE_TABLE

    //        dtFDMEE.Columns.Add("RECORD_ID", typeof(int));
    //        dtFDMEE.Columns.Add("YEAR", typeof(string));
    //        dtFDMEE.Columns.Add("PERIOD", typeof(string));
    //        dtFDMEE.Columns.Add("QUARTER", typeof(string));
    //        dtFDMEE.Columns.Add("GL_ACCOUNT", typeof(string));
    //        dtFDMEE.Columns.Add("DESCRIPTION", typeof(string));
    //        dtFDMEE.Columns.Add("BSPL_TYPE", typeof(string));
    //        dtFDMEE.Columns.Add("HFM_ACCOUNT", typeof(string));
    //        dtFDMEE.Columns.Add("HFM_ACCOUNT_DESC", typeof(string));
    //        dtFDMEE.Columns.Add("TYPE", typeof(string));
    //        dtFDMEE.Columns.Add("CATEGORY", typeof(string));
    //        dtFDMEE.Columns.Add("ICP", typeof(string));
    //        dtFDMEE.Columns.Add("HFM_CUSTOM1", typeof(string));
    //        dtFDMEE.Columns.Add("HFMNEW_CUSTOM4", typeof(string));
    //        dtFDMEE.Columns.Add("AMOUNT", typeof(string));
    //        dtFDMEE.Columns.Add("SOURCE_AMOUNT", typeof(string));
    //        dtFDMEE.Columns.Add("SR_NO", typeof(int));

    //        #endregion



    //        if (fileUploadCostCenter.HasFile)
    //        {
    //            int csvRowOneColumnsCount = 0;
    //            int csvAllColumnsCount = 0;

    //            if (!string.IsNullOrEmpty(fileUploadCostCenter.PostedFile.FileName))
    //            {
    //                System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadCostCenter.PostedFile.InputStream);
    //                string csvData = myReader.ReadToEnd();

    //                foreach (string row in csvData.Split('\n'))
    //                {
    //                    count++;
    //                    if (!string.IsNullOrEmpty(row))
    //                    {
    //                        dtFDMEE.Rows.Add();

    //                        int i = 0;
    //                        string dtColunValue = string.Empty;
    //                        foreach (string cell in row.Split(','))
    //                        {
    //                            dtColunValue = cell.Trim();

    //                            if (i <= (dtFDMEE.Columns.Count - 1))
    //                            {
    //                                if (!string.IsNullOrEmpty(dtColunValue))
    //                                    dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = dtColunValue;
    //                                else
    //                                    dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = string.Empty;

    //                                i++;
    //                            }
    //                        }
    //                    }
    //                }


    //                //foreach (string row in csvData.Split('\n'))
    //                //{
    //                //    cc1++;
    //                //    if (!string.IsNullOrEmpty(row))
    //                //    {
    //                //        dtFDMEE.Rows.Add();

    //                //        int i = 0;
    //                //        string dtColunValue = string.Empty;

    //                //        string[] strRowText = row.Split(',');
    //                //        if (cc1 == 1)
    //                //            csvRowOneColumnsCount = strRowText.Length;

    //                //        if (cc1 > 1)
    //                //            csvAllColumnsCount = strRowText.Length;

    //                //        if (csvAllColumnsCount > csvRowOneColumnsCount)
    //                //        {
    //                //            strRowText[4] = Convert.ToString(strRowText[4] + strRowText[5]).Replace('"', ' ');
    //                //        }

    //                //        foreach (string cell in strRowText)
    //                //        {
    //                //            dtColunValue = cell.Trim();

    //                //            if (i <= (dtFDMEE.Columns.Count - 1))
    //                //            {
    //                //                if (!string.IsNullOrEmpty(dtColunValue))
    //                //                {
    //                //                    if ((i + 1) <= 15)
    //                //                        dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = dtColunValue;
    //                //                }
    //                //                else
    //                //                {
    //                //                    if ((i + 1) <= 15)
    //                //                        dtFDMEE.Rows[dtFDMEE.Rows.Count - 1][i + 1] = string.Empty;
    //                //                }

    //                //                i++;
    //                //            }
    //                //        }
    //                //    }
    //                //}


    //            }
    //        }


    //        if (dtFDMEE.Rows.Count > 0)
    //        {
    //            dtFDMEE.Rows.RemoveAt(0);
    //        }

    //        foreach (DataRow dr in dtFDMEE.Rows)
    //        {
    //            count++;
    //            dr["SR_NO"] = count;
    //        }


    //        Session["DS_FDMEE"] = objReports.GetFDMEE();
    //        dsGLCode = objReports.GetGLCodeList();

    //        if (Session["DS_FDMEE"] != null)
    //            dsFDMEE = (DataSet)Session["DS_FDMEE"];

    //        int year = 0;
    //        int period = 0;
    //        int quarter = 0;
    //        string glAccount = string.Empty;
    //        string type = string.Empty;

    //        if (dsFDMEE.Tables.Count > 0 && dsFDMEE.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dsFDMEE.Tables[0].Rows)
    //            {
    //                year = Convert.ToInt32(dr["YEAR"]);
    //                period = Convert.ToInt32(dr["PERIOD"]);
    //                quarter = Convert.ToInt32(dr["QUARTER"]);
    //                glAccount = Convert.ToString(dr["GL_ACCOUNT"]);
    //                type = Convert.ToString(dr["TYPE"]);

    //                foreach (DataRow dr1 in dtFDMEE.Select("YEAR='" + year + "' AND PERIOD='" + period + "' AND QUARTER='" + quarter + "' AND GL_ACCOUNT='" + glAccount + "' AND TYPE='" + type + "'"))
    //                {
    //                    dr1["RECORD_ID"] = dr["RECORD_ID"];
    //                }
    //            }
    //        }
    //        else
    //        {
    //            foreach (DataRow dr in dtFDMEE.Rows)
    //            {
    //                dr["RECORD_ID"] = 0;
    //            }
    //        }

    //        foreach (DataRow dr in dtFDMEE.Rows)
    //        {
    //            if (checkNumeric(Convert.ToString(dr["GL_ACCOUNT"])))
    //            {
    //                if (Convert.ToInt32(Convert.ToString(dr["GL_ACCOUNT"]).Length) == 3)
    //                    dr["GL_ACCOUNT"] = "0" + Convert.ToString(dr["GL_ACCOUNT"]);
    //            }

    //            if (string.IsNullOrEmpty(Convert.ToString(dr["RECORD_ID"])))
    //                dr["RECORD_ID"] = 0;
    //        }

    //        string glCode = string.Empty;
    //        string glDesc = string.Empty;
    //        string blplType = string.Empty;

    //        if (dtFDMEE.Rows.Count > 0)
    //        {
    //            if (dsGLCode.Tables.Count > 0 && dsGLCode.Tables[0].Rows.Count > 0)
    //            {
    //                foreach (DataRow dr1 in dsGLCode.Tables[0].Rows)
    //                {
    //                    glCode = Convert.ToString(dr1["GLCODE"]);
    //                    glDesc = Convert.ToString(dr1["DESCRIPT"]);
    //                    blplType = Convert.ToString(dr1["PL_BS"]);

    //                    foreach (DataRow dr2 in dtFDMEE.Select("GL_ACCOUNT='" + glCode + "'"))
    //                    {
    //                        if (string.IsNullOrEmpty(Convert.ToString(dr2["DESCRIPTION"])))
    //                        {
    //                            if (!string.IsNullOrEmpty(glDesc))
    //                                dr2["DESCRIPTION"] = glDesc;
    //                            else
    //                                dr2["DESCRIPTION"] = string.Empty;
    //                        }

    //                        if (string.IsNullOrEmpty(Convert.ToString(dr2["BSPL_TYPE"])))
    //                        {
    //                            if (!string.IsNullOrEmpty(blplType))
    //                                dr2["BSPL_TYPE"] = blplType;
    //                            else
    //                                dr2["BSPL_TYPE"] = string.Empty;

    //                        }
    //                    }
    //                }
    //            }
    //        }


    //        if (dtFDMEE.Rows.Count > 0)
    //        {
    //            Session["DT_FDMEE"] = dtFDMEE;
    //            gvFDMEE.DataSource = dtFDMEE;
    //            gvFDMEE.DataBind();
    //            hdGVRowCount.Value = Convert.ToString(gvFDMEE.Rows.Count);
    //            lblRecords.Text = "Records[" + dtFDMEE.Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
    //            hdExistedRecords.Value = existedRecrdsCount.ToString();
    //        }
    //        else
    //        {
    //            Session["DT_FDMEE"] = null;
    //            gvFDMEE.DataSource = null;
    //            gvFDMEE.DataBind();
    //            hdGVRowCount.Value = "0";
    //            hdExistedRecords.Value = "0";
    //            lblRecords.Text = "Records[0], Already Exists[0]";
    //            ExceptionMessage("No data found..!!!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private bool CheckNumeric(string value)
    {

        int n;
        bool isNumeric = int.TryParse(value, out n);
        return isNumeric;
    }

    private string GetDistinctSringVal(string text)
    {
        try
        {
            string result = string.Empty;
            string[] str = text.Split(',');
            string resText1 = string.Empty;
            string resText2 = string.Empty;


            foreach (string it in str)
            {
                if (string.IsNullOrEmpty(result))
                {
                    result += it + ",";
                }
                else
                {
                    if (!result.Contains(it))
                    {
                        result += it + ",";
                    }
                }
            }

            if (!string.IsNullOrEmpty(result))
                result = result.TrimEnd(',');

            return result;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void PostFDMEE()
    {
        try
        {
            string updateQuery = string.Empty;

            int recordID = 0;
            int year = 0;
            int period = 0;
            int quarter = 0;
            string GLAccount = string.Empty;
            string description = string.Empty;
            string BSPLType = string.Empty;
            string HFMAccount = string.Empty;
            string HFMAccountDesc = string.Empty;
            string type = string.Empty;
            string category = string.Empty;
            string ICP = string.Empty;
            string HFMCustom1 = string.Empty;
            string HFMNewCustom4 = string.Empty;
            double amount = 0;
            double sourceAmount = 0;
            int srNo = 0;

            string srNoForRemoval = string.Empty;
            int count = 0;

            DataTable dtTempFDMEE = new DataTable();

            dtTempFDMEE.Columns.Add("YEAR", typeof(string));
            dtTempFDMEE.Columns.Add("PERIOD", typeof(string));
            dtTempFDMEE.Columns.Add("QUARTER", typeof(string));
            dtTempFDMEE.Columns.Add("GL_ACCOUNT", typeof(string));
            dtTempFDMEE.Columns.Add("DESCRIPTION", typeof(string));
            dtTempFDMEE.Columns.Add("BSPL_TYPE", typeof(string));
            dtTempFDMEE.Columns.Add("HFM_ACCOUNT", typeof(string));
            dtTempFDMEE.Columns.Add("HFM_ACCOUNT_DESC", typeof(string));
            dtTempFDMEE.Columns.Add("TYPE", typeof(string));
            dtTempFDMEE.Columns.Add("CATEGORY", typeof(string));
            dtTempFDMEE.Columns.Add("ICP", typeof(string));
            dtTempFDMEE.Columns.Add("HFM_CUSTOM1", typeof(string));
            dtTempFDMEE.Columns.Add("HFMNEW_CUSTOM4", typeof(string));
            dtTempFDMEE.Columns.Add("AMOUNT", typeof(string));
            dtTempFDMEE.Columns.Add("SOURCE_AMOUNT", typeof(string));
            dtTempFDMEE.Columns.Add("CREATED_BY", typeof(Int32));

            int value = 0;
            foreach (GridViewRow gr in gvFDMEE.Rows)
            {
                DataRow dr = dtTempFDMEE.NewRow();

                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblYear = (Label)gr.FindControl("lblYear");
                Label lblPeriod = (Label)gr.FindControl("lblPeriod");
                Label lblQuarter = (Label)gr.FindControl("lblQuarter");
                Label lblGLAccount = (Label)gr.FindControl("lblGLAccount");
                Label lblDescription = (Label)gr.FindControl("lblDescription");
                Label lblBSPLType = (Label)gr.FindControl("lblBSPLType");
                Label lblHFMAccount = (Label)gr.FindControl("lblHFMAccount");
                Label lblHFMAccountDesc = (Label)gr.FindControl("lblHFMAccountDesc");
                Label lblType = (Label)gr.FindControl("lblType");
                Label lblCategory = (Label)gr.FindControl("lblCategory");
                Label lblICP = (Label)gr.FindControl("lblICP");
                Label lblHFMCustom1 = (Label)gr.FindControl("lblHFMCustom1");
                Label lblHFMNewCustom4 = (Label)gr.FindControl("lblHFMNewCustom4");
                TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                TextBox txtSourceAmount = (TextBox)gr.FindControl("txtSourceAmount");
                Label lblSRNo = (Label)gr.FindControl("lblSRNo");

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                    recordID = Convert.ToInt32(lblRecordID.Text);
                else
                    recordID = 0;

                if (Convert.ToInt32(lblYear.Text) > 0)
                    year = Convert.ToInt32(lblYear.Text);
                else
                    year = 0;

                if (Convert.ToInt32(lblPeriod.Text) > 0)
                    period = Convert.ToInt32(lblPeriod.Text);
                else
                    period = 0;

                if (Convert.ToInt32(lblQuarter.Text) > 0)
                    quarter = Convert.ToInt32(lblQuarter.Text);
                else
                    quarter = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblGLAccount.Text)))
                    GLAccount = Convert.ToString(lblGLAccount.Text);
                else
                    GLAccount = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
                    description = Convert.ToString(lblDescription.Text);
                else
                    description = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblBSPLType.Text)))
                    BSPLType = Convert.ToString(lblBSPLType.Text);
                else
                    BSPLType = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblHFMAccount.Text)))
                    HFMAccount = Convert.ToString(lblHFMAccount.Text);
                else
                    HFMAccount = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblHFMAccountDesc.Text)))
                    HFMAccountDesc = Convert.ToString(lblHFMAccountDesc.Text);
                else
                    HFMAccountDesc = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblType.Text)))
                    type = Convert.ToString(lblType.Text);
                else
                    type = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCategory.Text)))
                    category = Convert.ToString(lblCategory.Text);
                else
                    category = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblICP.Text)))
                    ICP = Convert.ToString(lblICP.Text);
                else
                    ICP = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblHFMCustom1.Text)))
                    HFMCustom1 = Convert.ToString(lblHFMCustom1.Text);
                else
                    HFMCustom1 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblHFMNewCustom4.Text)))
                    HFMNewCustom4 = Convert.ToString(lblHFMNewCustom4.Text);
                else
                    HFMNewCustom4 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtAmount.Text)))
                    amount = Convert.ToDouble(txtAmount.Text);
                else
                    amount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtSourceAmount.Text)))
                    sourceAmount = Convert.ToDouble(txtSourceAmount.Text);
                else
                    sourceAmount = 0;

                srNo = Convert.ToInt32(lblSRNo.Text);

                dtFDMEE = (DataTable)Session["DT_FDMEE"];

                if (recordID > 0)
                {
                    count++;
                    srNoForRemoval += srNo + ",";
                    updateQuery += "update tblFDMEE set  YEAR=" + year + ", PERIOD=" + period + ", QUARTER='" + quarter + "', GL_ACCOUNT='" + GLAccount + "', DESCRIPTION='" + description + "', BSPL_TYPE='" + BSPLType + "', HFM_ACCOUNT='" + HFMAccount + "', HFM_ACCOUNT_DESC='" + HFMAccountDesc + "', TYPE='" + type + "', CATEGORY='" + category + "', ICP='" + ICP + "', HFM_CUSTOM1='" + HFMCustom1 + "', HFMNEW_CUSTOM4='" + HFMNewCustom4 + "', AMOUNT='" + amount + "', SOURCE_AMOUNT='" + sourceAmount + "', MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", MODIFIED_ON=GETDATE() where RECORD_ID=" + recordID + ";" + Environment.NewLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(GLAccount))
                    {
                        count++;

                        srNoForRemoval += srNo + ",";
                        dr["YEAR"] = year;
                        dr["PERIOD"] = period;
                        dr["QUARTER"] = quarter;
                        dr["GL_ACCOUNT"] = GLAccount;
                        dr["DESCRIPTION"] = description;
                        dr["BSPL_TYPE"] = BSPLType;
                        dr["HFM_ACCOUNT"] = HFMAccount;
                        dr["HFM_ACCOUNT_DESC"] = HFMAccountDesc;
                        dr["TYPE"] = type;
                        dr["CATEGORY"] = category;
                        dr["ICP"] = ICP;
                        dr["HFM_CUSTOM1"] = HFMCustom1;
                        dr["HFMNEW_CUSTOM4"] = HFMNewCustom4;
                        dr["AMOUNT"] = amount;
                        dr["SOURCE_AMOUNT"] = sourceAmount;
                        dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                        dtTempFDMEE.Rows.Add(dr);
                    }
                }
            }

            if (Convert.ToInt32(hdReplacementFlag.Value) > 0)
            {
                if (!string.IsNullOrEmpty(updateQuery))
                    updateQuery = updateQuery.TrimEnd(';').Trim();
            }
            else
            {
                updateQuery = string.Empty;
            }


            value = objReports.PostFDMEE(dtTempFDMEE, updateQuery);

            if (value > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvFDMEE.DataSource = null;
                    gvFDMEE.DataBind();
                    lblRecords.Text = "Records[" + gvFDMEE.Rows.Count + "]";
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
            dt1 = (DataTable)Session["DT_FDMEE"];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dt1.Select("SR_NO='" + item + "'"))
                {
                    dt1.Rows.Remove(dr);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                gvFDMEE.DataSource = dt1;
                gvFDMEE.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvFDMEE.Rows.Count);
            }
            else
            {
                gvFDMEE.DataSource = null;
                gvFDMEE.DataBind();
                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + gvFDMEE.Rows.Count + "]";
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
