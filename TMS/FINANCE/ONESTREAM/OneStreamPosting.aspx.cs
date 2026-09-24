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

public partial class FINANCE_ONESTREAM_OneStreamPosting : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Reports objReports = new BAL.Reports();
    DataSet dsGLCode = new DataSet();
    DataSet dsOneStream = new DataSet();
    DataTable dtOneStream = new DataTable();
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
                Session["DS_OneStream"] = null;
                Session["DT_OneStream"] = null;
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

    protected void btnGetOneStreamFile_Click(object sender, EventArgs e)
    {
        GetOneStreamDetail();
    }

    protected void gvOneStream_RowDataBound(object sender, GridViewRowEventArgs e)
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

                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");


                //if (Convert.ToInt32(lblYear.Text) > 0)
                //    year = Convert.ToInt32(lblYear.Text);
                //else
                //    year = 0;

                //if (Convert.ToInt32(lblPeriod.Text) > 0)
                //    period = Convert.ToInt32(lblPeriod.Text);
                //else
                //    period = 0;

                //if (Convert.ToInt32(lblQuarter.Text) > 0)
                //    quarter = Convert.ToInt32(lblQuarter.Text);
                //else
                //    quarter = 0;

                //if (!string.IsNullOrEmpty(Convert.ToString(lblGLAccount.Text)))
                //    GLAccount = Convert.ToString(lblGLAccount.Text);
                //else
                //    GLAccount = string.Empty;

                //if (!string.IsNullOrEmpty(Convert.ToString(lblType.Text)))
                //    type = Convert.ToString(lblType.Text);
                //else
                //    type = string.Empty;


                //if (Session["DS_OneStream"] != null)
                //{
                //    ds1 = (DataSet)Session["DS_OneStream"];
                //    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                //    {
                //        foreach (DataRow dr1 in ds1.Tables[0].Select("YEAR='" + year + "' AND PERIOD='" + period + "' AND QUARTER='" + quarter + "' AND GL_ACCOUNT='" + GLAccount + "' AND TYPE='" + type + "'"))
                //        {
                //            existedRecrdsCount++;
                //            for (int i = 0; i < e.Row.Cells.Count; i++)
                //            {
                //                e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                //            }
                //        }
                //    }
                //}

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    existedRecrdsCount++;
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
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
        if (gvOneStream.Rows.Count > 0)
        {
            PostOneStream();
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
            csv = "PROFILE_NAME" + ',';
            csv += "TM" + ',';
            csv += "TMT" + ',';
            csv += "ET" + ',';
            csv += "ETT" + ',';
            csv += "AC" + ',';
            csv += "ACT" + ',';
            csv += "FW" + ',';
            csv += "FWT" + ',';
            csv += "IC" + ',';
            csv += "ICT" + ',';
            csv += "U1" + ',';
            csv += "U1T" + ',';
            csv += "U2" + ',';
            csv += "U2T" + ',';
            csv += "U3" + ',';
            csv += "U3T" + ',';
            csv += "U4" + ',';
            csv += "U4T" + ',';
            csv += "U5" + ',';
            csv += "U5T" + ',';
            csv += "U6" + ',';
            csv += "U6T" + ',';
            csv += "U7" + ',';
            csv += "U7T" + ',';
            csv += "U8" + ',';
            csv += "U8T" + ',';
            csv += "RAW_AMOUNT" + ',';
            csv += "CONVERTED_AMOUNT" + ',';
            csv += "\r\n";

            string fileName = "OneStream";
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

    private void GetOneStreamDetail()
    {
        int cc1 = 0;

        try
        {
            hdGVRowCount.Value = "0";
            int count = 0;

            #region CREATE_TABLE

            dtOneStream.Columns.Add("RECORD_ID", typeof(int));

            dtOneStream.Columns.Add("F_YEAR", typeof(string));
            dtOneStream.Columns.Add("F_PERIOD", typeof(string));
            dtOneStream.Columns.Add("F_QUARTER", typeof(string));

            dtOneStream.Columns.Add("PROFILE_NAME", typeof(string));
            dtOneStream.Columns.Add("TM", typeof(string));
            dtOneStream.Columns.Add("TMT", typeof(string));
            dtOneStream.Columns.Add("ET", typeof(string));
            dtOneStream.Columns.Add("ETT", typeof(string));
            dtOneStream.Columns.Add("AC", typeof(string));
            dtOneStream.Columns.Add("ACT", typeof(string));
            dtOneStream.Columns.Add("FW", typeof(string));
            dtOneStream.Columns.Add("FWT", typeof(string));
            dtOneStream.Columns.Add("IC", typeof(string));
            dtOneStream.Columns.Add("ICT", typeof(string));
            dtOneStream.Columns.Add("U1", typeof(string));
            dtOneStream.Columns.Add("U1T", typeof(string));
            dtOneStream.Columns.Add("U2", typeof(string));
            dtOneStream.Columns.Add("U2T", typeof(string));
            dtOneStream.Columns.Add("U3", typeof(string));
            dtOneStream.Columns.Add("U3T", typeof(string));
            dtOneStream.Columns.Add("U4", typeof(string));
            dtOneStream.Columns.Add("U4T", typeof(string));
            dtOneStream.Columns.Add("U5", typeof(string));
            dtOneStream.Columns.Add("U5T", typeof(string));
            dtOneStream.Columns.Add("U6", typeof(string));
            dtOneStream.Columns.Add("U6T", typeof(string));
            dtOneStream.Columns.Add("U7", typeof(string));
            dtOneStream.Columns.Add("U7T", typeof(string));
            dtOneStream.Columns.Add("U8", typeof(string));
            dtOneStream.Columns.Add("U8T", typeof(string));
            dtOneStream.Columns.Add("RAW_AMOUNT", typeof(string));
            dtOneStream.Columns.Add("CONVERTED_AMOUNT", typeof(string));
            dtOneStream.Columns.Add("SR_NO", typeof(int));

            #endregion



            if (fileUploadCostCenter.HasFile)
            {
                int csvHeaderRowColumnsCount = 0;
                int csvDataRowColumnsCount = 0;

                if (!string.IsNullOrEmpty(fileUploadCostCenter.PostedFile.FileName))
                {
                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadCostCenter.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        cc1++;
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtOneStream.Rows.Add();

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

                                if (i <= (dtOneStream.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dtColunValue))
                                    {
                                        if ((i + 4) <= 32)
                                            dtOneStream.Rows[dtOneStream.Rows.Count - 1][i + 4] = dtColunValue;
                                    }
                                    else
                                    {
                                        if ((i + 4) <= 32)
                                            dtOneStream.Rows[dtOneStream.Rows.Count - 1][i + 4] = string.Empty;
                                    }

                                    i++;
                                }
                            }
                        }
                    }
                }
            }

            int month = 0;
            int fMonthi = 0;
            string fMonth = string.Empty;
            string fYear = string.Empty;

            int fQuarteri = 0;
            string fQuarter = string.Empty;
            string glCodes = string.Empty;

            if (dtOneStream.Rows.Count > 0)
                dtOneStream.Rows.RemoveAt(0);


            if (dtOneStream.Rows.Count > 0)
            {
                foreach (DataRow dr in dtOneStream.Rows)
                {
                    count++;
                    dr["SR_NO"] = count;

                    //2022M6,2022M12
                    if (Convert.ToString(dr["TMT"]).Length == 6)
                    {
                        month = Convert.ToInt32(Convert.ToString(dr["TMT"]).Substring(5, 1));
                    }
                    else if (Convert.ToString(dr["TMT"]).Length == 7)
                    {
                        month = Convert.ToInt32(Convert.ToString(dr["TMT"]).Substring(5, 2));
                    }



                    if (month <= 3)
                    {
                        fMonthi = (month + 9);
                    }
                    else
                    {
                        fMonthi = (month - 3);
                    }

                    fMonth += fMonthi + ",";
                    dr["F_PERIOD"] = fMonthi;


                    if (fMonthi >= 10 && fMonthi <= 12)
                    {
                        fQuarteri = 1;
                    }

                    else if (fMonthi >= 1 && fMonthi <= 3)
                    {
                        fQuarteri = 2;
                    }
                    else if (fMonthi >= 4 && fMonthi <= 6)
                    {
                        fQuarteri = 3;
                    }
                    else
                    {
                        fQuarteri = 4;
                    }

                    fQuarter += fQuarteri + ",";
                    dr["F_QUARTER"] = fQuarteri;


                    fYear += Convert.ToString(Convert.ToString(dr["TMT"]).Substring(0, 4)) + ",";
                    dr["F_YEAR"] = Convert.ToInt32(Convert.ToString(dr["TMT"]).Substring(0, 4));

                    glCodes += "'" + Convert.ToString(dr["FW"]) + "',";

                }
            }

            if (!string.IsNullOrEmpty(fYear))
                fYear = GetDistinctSringVal(fYear.TrimEnd(','));

            if (!string.IsNullOrEmpty(fMonth))
                fMonth = GetDistinctSringVal(fMonth.TrimEnd(','));

            if (!string.IsNullOrEmpty(fQuarter))
                fQuarter = GetDistinctSringVal(fQuarter.TrimEnd(','));

            if (!string.IsNullOrEmpty(glCodes))
                glCodes = GetDistinctSringVal(glCodes.TrimEnd(','));


            dsOneStream = objReports.GetOneStream(fYear, fMonth, fQuarter, glCodes);

            if (dsOneStream.Tables.Count > 0)
            {
                Session["DS_OneStream"] = dsOneStream;
            }


            int yearVal = 0;
            int periodVal = 0;
            int quarterVal = 0;
            string glAccount = string.Empty;

            if (dsOneStream.Tables.Count > 0 && dsOneStream.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOneStream.Tables[0].Rows)
                {
                    yearVal = Convert.ToInt32(dr["F_YEAR"]);
                    periodVal = Convert.ToInt32(dr["F_PERIOD"]);
                    quarterVal = Convert.ToInt32(dr["F_QUARTER"]);
                    glAccount = Convert.ToString(dr["FW"]);

                    foreach (DataRow dr1 in dtOneStream.Select("F_YEAR='" + yearVal + "' AND F_PERIOD='" + periodVal + "' AND F_QUARTER='" + quarterVal + "' AND FW='" + glAccount + "'"))
                    {
                        dr1["RECORD_ID"] = dr["RECORD_ID"];
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dtOneStream.Rows)
                {
                    dr["RECORD_ID"] = 0;
                }
            }


            if (dtOneStream.Rows.Count > 0)
            {
                foreach (DataRow dr in dtOneStream.Rows)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dr["RECORD_ID"])))
                        dr["RECORD_ID"] = 0;
                }
            }

            if (dtOneStream.Rows.Count > 0)
            {
                Session["DT_OneStream"] = dtOneStream;
                gvOneStream.DataSource = dtOneStream;
                gvOneStream.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvOneStream.Rows.Count);
                lblRecords.Text = "Records[" + dtOneStream.Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
                hdExistedRecords.Value = existedRecrdsCount.ToString();
            }
            else
            {
                Session["DT_OneStream"] = null;
                gvOneStream.DataSource = null;
                gvOneStream.DataBind();
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

    private void PostOneStream()
    {
        try
        {
            string updateQuery = string.Empty;

            int recordID = 0;
            int FYear = 0;
            int FPeriod = 0;
            int FQuarter = 0;
            string ProfileName = string.Empty;
            string Tm = string.Empty;
            string Tmt = string.Empty;
            string Et = string.Empty;
            string Ett = string.Empty;
            string Ac = string.Empty;
            string Act = string.Empty;
            string Fw = string.Empty;
            string Fwt = string.Empty;
            string Ic = string.Empty;
            string Ict = string.Empty;
            string U1 = string.Empty;
            string U1T = string.Empty;
            string U2 = string.Empty;
            string U2T = string.Empty;
            string U3 = string.Empty;
            string U3T = string.Empty;
            string U4 = string.Empty;
            string U4T = string.Empty;
            string U5 = string.Empty;
            string U5T = string.Empty;
            string U6 = string.Empty;
            string U6T = string.Empty;
            string U7 = string.Empty;
            string U7T = string.Empty;
            string U8 = string.Empty;
            string U8T = string.Empty;
            string RawAmount = string.Empty;
            string ConvertedAmount = string.Empty;
            int srNo = 0;

            string srNoForRemoval = string.Empty;
            int count = 0;

            DataTable dtTempOneStream = new DataTable();

            dtTempOneStream.Columns.Add("F_YEAR", typeof(int));
            dtTempOneStream.Columns.Add("F_PERIOD", typeof(int));
            dtTempOneStream.Columns.Add("F_QUARTER", typeof(int));
            dtTempOneStream.Columns.Add("PROFILE_NAME", typeof(string));
            dtTempOneStream.Columns.Add("TM", typeof(string));
            dtTempOneStream.Columns.Add("TMT", typeof(string));
            dtTempOneStream.Columns.Add("ET", typeof(string));
            dtTempOneStream.Columns.Add("ETT", typeof(string));
            dtTempOneStream.Columns.Add("AC", typeof(string));
            dtTempOneStream.Columns.Add("ACT", typeof(string));
            dtTempOneStream.Columns.Add("FW", typeof(string));
            dtTempOneStream.Columns.Add("FWT", typeof(string));
            dtTempOneStream.Columns.Add("IC", typeof(string));
            dtTempOneStream.Columns.Add("ICT", typeof(string));
            dtTempOneStream.Columns.Add("U1", typeof(string));
            dtTempOneStream.Columns.Add("U1T", typeof(string));
            dtTempOneStream.Columns.Add("U2", typeof(string));
            dtTempOneStream.Columns.Add("U2T", typeof(string));
            dtTempOneStream.Columns.Add("U3", typeof(string));
            dtTempOneStream.Columns.Add("U3T", typeof(string));
            dtTempOneStream.Columns.Add("U4", typeof(string));
            dtTempOneStream.Columns.Add("U4T", typeof(string));
            dtTempOneStream.Columns.Add("U5", typeof(string));
            dtTempOneStream.Columns.Add("U5T", typeof(string));
            dtTempOneStream.Columns.Add("U6", typeof(string));
            dtTempOneStream.Columns.Add("U6T", typeof(string));
            dtTempOneStream.Columns.Add("U7", typeof(string));
            dtTempOneStream.Columns.Add("U7T", typeof(string));
            dtTempOneStream.Columns.Add("U8", typeof(string));
            dtTempOneStream.Columns.Add("U8T", typeof(string));
            dtTempOneStream.Columns.Add("RAW_AMOUNT", typeof(string));
            dtTempOneStream.Columns.Add("CONVERTED_AMOUNT", typeof(string));
            dtTempOneStream.Columns.Add("CREATED_BY", typeof(int));

            int value = 0;
            foreach (GridViewRow gr in gvOneStream.Rows)
            {
                recordID = 0;
                FYear = 0;
                FPeriod = 0;
                FQuarter = 0;
                ProfileName = string.Empty;
                Tm = string.Empty;
                Tmt = string.Empty;
                Et = string.Empty;
                Ett = string.Empty;
                Ac = string.Empty;
                Act = string.Empty;
                Fw = string.Empty;
                Fwt = string.Empty;
                Ic = string.Empty;
                Ict = string.Empty;
                U1 = string.Empty;
                U1T = string.Empty;
                U2 = string.Empty;
                U2T = string.Empty;
                U3 = string.Empty;
                U3T = string.Empty;
                U4 = string.Empty;
                U4T = string.Empty;
                U5 = string.Empty;
                U5T = string.Empty;
                U6 = string.Empty;
                U6T = string.Empty;
                U7 = string.Empty;
                U7T = string.Empty;
                U8 = string.Empty;
                U8T = string.Empty;
                RawAmount = string.Empty;
                ConvertedAmount = string.Empty;
                srNo = 0;


                DataRow dr = dtTempOneStream.NewRow();

                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblFYear = (Label)gr.FindControl("lblFYear");
                Label lblFPeriod = (Label)gr.FindControl("lblFPeriod");
                Label lblFQuarter = (Label)gr.FindControl("lblFQuarter");
                Label lblProfileName = (Label)gr.FindControl("lblProfileName");
                Label lblTm = (Label)gr.FindControl("lblTm");
                Label lblTmt = (Label)gr.FindControl("lblTmt");
                Label lblEt = (Label)gr.FindControl("lblEt");
                Label lblEtt = (Label)gr.FindControl("lblEtt");
                Label lblAc = (Label)gr.FindControl("lblAc");
                Label lblAct = (Label)gr.FindControl("lblAct");
                Label lblFw = (Label)gr.FindControl("lblFw");
                Label lblFwt = (Label)gr.FindControl("lblFwt");
                Label lblIc = (Label)gr.FindControl("lblIc");
                Label lblIct = (Label)gr.FindControl("lblIct");
                Label lblU1 = (Label)gr.FindControl("lblU1");
                Label lblU1T = (Label)gr.FindControl("lblU1T");
                Label lblU2 = (Label)gr.FindControl("lblU2");
                Label lblU2T = (Label)gr.FindControl("lblU2T");
                Label lblU3 = (Label)gr.FindControl("lblU3");
                Label lblU3T = (Label)gr.FindControl("lblU3T");
                Label lblU4 = (Label)gr.FindControl("lblU4");
                Label lblU4T = (Label)gr.FindControl("lblU4T");
                Label lblU5 = (Label)gr.FindControl("lblU5");
                Label lblU5T = (Label)gr.FindControl("lblU5T");
                Label lblU6 = (Label)gr.FindControl("lblU6");
                Label lblU6T = (Label)gr.FindControl("lblU6T");
                Label lblU7 = (Label)gr.FindControl("lblU7");
                Label lblU7T = (Label)gr.FindControl("lblU7T");
                Label lblU8 = (Label)gr.FindControl("lblU8");
                Label lblU8T = (Label)gr.FindControl("lblU8T");
                Label lblRawAmount = (Label)gr.FindControl("lblRawAmount");
                Label lblConvertedAmount = (Label)gr.FindControl("lblConvertedAmount");

                Label lblSRNo = (Label)gr.FindControl("lblSRNo");

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                    recordID = Convert.ToInt32(lblRecordID.Text);


                if (Convert.ToInt32(lblFYear.Text) > 0)
                    FYear = Convert.ToInt32(lblFYear.Text);

                if (Convert.ToInt32(lblFPeriod.Text) > 0)
                {
                    FPeriod = Convert.ToInt32(lblFPeriod.Text);

                    if (FPeriod == 10 || FPeriod == 11 || FPeriod == 12)
                    {
                        FYear = FYear - 1;
                    }
                }
                

                if (Convert.ToInt32(lblFQuarter.Text) > 0)
                    FQuarter = Convert.ToInt32(lblFQuarter.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblProfileName.Text)))
                    ProfileName = Convert.ToString(lblProfileName.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblTm.Text)))
                    Tm = Convert.ToString(lblTm.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblTmt.Text)))
                    Tmt = Convert.ToString(lblTmt.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblEt.Text)))
                    Et = Convert.ToString(lblEt.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblEtt.Text)))
                    Ett = Convert.ToString(lblEtt.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblAc.Text)))
                    Ac = Convert.ToString(lblAc.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblAct.Text)))
                    Act = Convert.ToString(lblAct.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblFw.Text)))
                    Fw = Convert.ToString(lblFw.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblFwt.Text)))
                    Fwt = Convert.ToString(lblFwt.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblIc.Text)))
                    Ic = Convert.ToString(lblIc.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblIct.Text)))
                    Ict = Convert.ToString(lblIct.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU1.Text)))
                    U1 = Convert.ToString(lblU1.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU1T.Text)))
                    U1T = Convert.ToString(lblU1T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU2.Text)))
                    U2 = Convert.ToString(lblU2.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU2T.Text)))
                    U2T = Convert.ToString(lblU2T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU3.Text)))
                    U3 = Convert.ToString(lblU3.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU3T.Text)))
                    U3T = Convert.ToString(lblU3T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU4.Text)))
                    U4 = Convert.ToString(lblU4.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU4T.Text)))
                    U4T = Convert.ToString(lblU4T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU5.Text)))
                    U5 = Convert.ToString(lblU5.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU5T.Text)))
                    U5T = Convert.ToString(lblU5T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU6.Text)))
                    U6 = Convert.ToString(lblU6.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU6T.Text)))
                    U6T = Convert.ToString(lblU6T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU7.Text)))
                    U7 = Convert.ToString(lblU7.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU7T.Text)))
                    U7T = Convert.ToString(lblU7T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU8.Text)))
                    U8 = Convert.ToString(lblU8.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblU8T.Text)))
                    U8T = Convert.ToString(lblU8T.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblRawAmount.Text)))
                    RawAmount = Convert.ToString(lblRawAmount.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblConvertedAmount.Text)))
                    ConvertedAmount = Convert.ToString(lblConvertedAmount.Text);


                srNo = Convert.ToInt32(lblSRNo.Text);

                dtOneStream = (DataTable)Session["DT_OneStream"];

                if (recordID > 0)
                {
                    count++;
                    srNoForRemoval += srNo + ",";
                    updateQuery += "update tblOneStream " +
                                    "set  " +

                                    " F_YEAR =" + FYear + "," +
                                    " F_PERIOD =" + FPeriod + "," +
                                    " F_QUARTER =" + FQuarter + "," +
                                    " PROFILE_NAME ='" + ProfileName + "'," +
                                    " TM ='" + Tm + "'," +
                                    " TMT ='" + Tmt + "'," +
                                    " ET ='" + Et + "'," +
                                    " ETT ='" + Ett + "'," +
                                    " AC ='" + Ac + "'," +
                                    " ACT ='" + Act + "'," +
                                    " FW ='" + Fw + "'," +
                                    " FWT ='" + Fwt + "'," +
                                    " IC ='" + Ic + "'," +
                                    " ICT ='" + Ict + "'," +
                                    " U1 ='" + U1 + "'," +
                                    " U1T ='" + U1T + "'," +
                                    " U2 ='" + U2 + "'," +
                                    " U2T ='" + U2T + "'," +
                                    " U3 ='" + U3 + "'," +
                                    " U3T ='" + U3T + "'," +
                                    " U4 ='" + U4 + "'," +
                                    " U4T ='" + U4T + "'," +
                                    " U5 ='" + U5 + "'," +
                                    " U5T ='" + U5T + "'," +
                                    " U6 ='" + U6 + "'," +
                                    " U6T ='" + U6T + "'," +
                                    " U7 ='" + U7 + "'," +
                                    " U7T ='" + U7T + "'," +
                                    " U8 ='" + U8 + "'," +
                                    " U8T ='" + U8T + "'," +
                                    " RAW_AMOUNT ='" + RawAmount + "'," +
                                    " CONVERTED_AMOUNT ='" + ConvertedAmount + "'," +
                                    " MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "," +
                                    " MODIFIED_ON=GETDATE() " +
                                    " WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Fw))
                    {
                        count++;

                        srNoForRemoval += srNo + ",";

                        dr["F_YEAR"] = FYear;
                        dr["F_PERIOD"] = FPeriod;
                        dr["F_QUARTER"] = FQuarter;
                        dr["PROFILE_NAME"] = ProfileName;
                        dr["TM"] = Tm;
                        dr["TMT"] = Tmt;
                        dr["ET"] = Et;
                        dr["ETT"] = Ett;
                        dr["AC"] = Ac;
                        dr["ACT"] = Act;
                        dr["FW"] = Fw;
                        dr["FWT"] = Fwt;
                        dr["IC"] = Ic;
                        dr["ICT"] = Ict;
                        dr["U1"] = U1;
                        dr["U1T"] = U1T;
                        dr["U2"] = U2;
                        dr["U2T"] = U2T;
                        dr["U3"] = U3;
                        dr["U3T"] = U3T;
                        dr["U4"] = U4;
                        dr["U4T"] = U4T;
                        dr["U5"] = U5;
                        dr["U5T"] = U5T;
                        dr["U6"] = U6;
                        dr["U6T"] = U6T;
                        dr["U7"] = U7;
                        dr["U7T"] = U7T;
                        dr["U8"] = U8;
                        dr["U8T"] = U8T;
                        dr["RAW_AMOUNT"] = RawAmount;
                        dr["CONVERTED_AMOUNT"] = ConvertedAmount;
                        dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                        dtTempOneStream.Rows.Add(dr);
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


            value = objReports.PostOneStream(dtTempOneStream, updateQuery);

            if (value > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvOneStream.DataSource = null;
                    gvOneStream.DataBind();
                    lblRecords.Text = "Records[" + gvOneStream.Rows.Count + "]";
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
            dt1 = (DataTable)Session["DT_OneStream"];
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
                gvOneStream.DataSource = dt1;
                gvOneStream.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvOneStream.Rows.Count);
            }
            else
            {
                gvOneStream.DataSource = null;
                gvOneStream.DataBind();
                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + gvOneStream.Rows.Count + "]";
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
