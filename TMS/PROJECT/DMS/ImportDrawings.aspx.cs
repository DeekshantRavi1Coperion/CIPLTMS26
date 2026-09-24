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

public partial class PROJECT_DMS_ImportDrawings : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    //DataSet dsUnit = new DataSet();
    DataSet dsDrawingNo = new DataSet();
    int recordID = 0;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;
    string description = string.Empty;
    int quantity = 0;
    string UOM = string.Empty;
    string rqdDateByProjectTeam = string.Empty;
    string isActive = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //Session["dtUnit"] = null;
                //BindCompany();
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

    protected void btnGetDrawingFile_Click(object sender, EventArgs e)
    {
        GetDrawingDetail();
    }

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = new DataTable();

                Label lblDrawingID = (Label)e.Row.FindControl("lblDrawingID");
                Label lblDuplicateFlag = (Label)e.Row.FindControl("lblDuplicateFlag");
                //Label lblJOBUnitUD = (Label)e.Row.FindControl("lblJOBUnitUD");
                //DropDownList ddlJOBUnit = (DropDownList)e.Row.FindControl("ddlJOBUnit");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                if (Convert.ToInt32(lblDrawingID.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }

                if (Convert.ToInt32(lblDuplicateFlag.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }


                //if (Session["dtUnit"] != null)
                //{
                //    dt = (DataTable)Session["dtUnit"];
                //}
                //else
                //{
                //    BindCompany();
                //    dt = (DataTable)Session["dtUnit"];
                //}

                //if (dt.Rows.Count > 0)
                //{
                //    ddlJOBUnit.DataSource = dt;
                //    ddlJOBUnit.DataTextField = "UNIT_NAME";
                //    ddlJOBUnit.DataValueField= "UNIT_ID";
                //    ddlJOBUnit.DataBind();
                //    if (Convert.ToInt32(lblJOBUnitUD.Text)>0)
                //    {
                //        ddlJOBUnit.SelectedValue = Convert.ToString(lblJOBUnitUD.Text);
                //    }
                //    else
                //    {
                //        ddlJOBUnit.SelectedIndex = 0;
                //    }                    
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (gvDesignDetails.Rows.Count > 0)
        {
            ImportDrawings();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }

    protected void btnDrawingList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/DesignDrawingList.aspx");
    }





    #endregion


    #region METHODS[=========================]

    //private void BindCompany()
    //{
    //    try
    //    {
    //        dsUnit = objCommon.GetUnit();
    //        if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
    //        {
    //            Session["dtUnit"] = dsUnit.Tables[0];
    //        }
    //        else
    //        {
    //            Session["dtUnit"] = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv = "JOB_NO" + ',';
            csv += "DRAWING_NO" + ',';
            csv += "CLIENT_DRAWING_NO" + ',';
            csv += "CONTRACTOR_DRAWING_NO" + ',';
            csv += "DESCRIPTION" + ',';
            csv += "QUANTITY" + ',';
            csv += "UOM" + ',';
            csv += "REQD_DATE_BY_PROJECT_TEAM (yyyy-MM-dd)" + ',';

            csv += "\r\n";

            string fileName = "DRAWINGS";
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

    //private void GetDrawingDetail()
    //{
    //    int cc1 = 0;
    //    try
    //    {
    //        string searchQuery = string.Empty;
    //        jobNo = string.Empty;
    //        drawingNo = string.Empty;

    //        DataTable dtTemp = new DataTable();
    //        #region CREATE_TABLE

    //        dtTemp.Columns.Add("DRAWING_ID", typeof(string));
    //        //dtTemp.Columns.Add("JOB_UNIT_ID", typeof(int));
    //        dtTemp.Columns.Add("JOB_NO", typeof(string));
    //        //dtTemp.Columns.Add("JOB_UNIT", typeof(string));
    //        dtTemp.Columns.Add("DRAWING_NO", typeof(string));
    //        dtTemp.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
    //        dtTemp.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));
    //        dtTemp.Columns.Add("DESCRIPTION", typeof(string));
    //        dtTemp.Columns.Add("QUANTITY", typeof(string));
    //        dtTemp.Columns.Add("UOM", typeof(string));
    //        dtTemp.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
    //        dtTemp.Columns.Add("DUPLICATE_FLAG", typeof(int));

    //        #endregion


    //        if (fileUploadDrawingFile.HasFile)
    //        {
    //            int csvHeaderRowColumnsCount = 0;
    //            int csvDataRowColumnsCount = 0;

    //            if (!string.IsNullOrEmpty(fileUploadDrawingFile.PostedFile.FileName))
    //            {
    //                System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadDrawingFile.PostedFile.InputStream);
    //                string csvData = myReader.ReadToEnd();

    //                foreach (string row in csvData.Split('\n'))
    //                {
    //                    //cc1++;
    //                    if (!string.IsNullOrEmpty(row))
    //                    {
    //                        dtTemp.Rows.Add();

    //                        int i = 0;
    //                        string dtColunValue = string.Empty;

    //                        string[] strRowText = row.Split(',');


    //                        if (cc1 == 1)
    //                            csvHeaderRowColumnsCount = strRowText.Length;
    //                        else if (cc1 > 1)
    //                            csvDataRowColumnsCount = strRowText.Length;

    //                        if (csvDataRowColumnsCount > csvHeaderRowColumnsCount)
    //                        {
    //                            int diff = csvDataRowColumnsCount - csvHeaderRowColumnsCount;
    //                            string txt = string.Empty;

    //                            for (int j = 1; j <= diff; j++)
    //                            {
    //                                txt += Convert.ToString(strRowText[4 + j]) + ", ";
    //                            }

    //                            if (!string.IsNullOrEmpty(txt))
    //                                txt = txt.Trim();

    //                            for (int k = 5; k < csvHeaderRowColumnsCount; k++)
    //                            {
    //                                for (int j = 1; j <= diff; j++)
    //                                {
    //                                    strRowText[k] = Convert.ToString(strRowText[k + j]);
    //                                }
    //                            }
    //                        }

    //                        foreach (string cell in strRowText)
    //                        {
    //                            dtColunValue = cell.Trim();

    //                            if (i <= (dtTemp.Columns.Count - 1))
    //                            {
    //                                if (!string.IsNullOrEmpty(dtColunValue))
    //                                {
    //                                    if ((i + 1) <= 8)
    //                                    {                                           
    //                                        dtColunValue = dtColunValue.Replace("\"\"", "$").Trim();
    //                                        dtColunValue = dtColunValue.Replace("\"", "").Trim();
    //                                        dtColunValue = dtColunValue.Replace("$", "\"").Trim();
    //                                        dtTemp.Rows[dtTemp.Rows.Count - 1][i + 1] = dtColunValue;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    if ((i + 1) <= 8)
    //                                        dtTemp.Rows[dtTemp.Rows.Count - 1][i + 1] = string.Empty;
    //                                }

    //                                i++;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }


    //        if (dtTemp.Rows.Count > 0)
    //        {
    //            dtTemp.Rows.RemoveAt(0);

    //            foreach (DataRow dr in dtTemp.Rows)
    //            {
    //                dr["DRAWING_ID"] = 0;
    //                if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
    //                {
    //                    dr["REQD_DATE_BY_PROJECT_TEAM"] = Convert.ToDateTime(dr["REQD_DATE_BY_PROJECT_TEAM"]).ToString("dd-MMM-yyyy");

    //                }
    //                else
    //                {
    //                    dr["REQD_DATE_BY_PROJECT_TEAM"] = string.Empty;
    //                }

    //                //if (Convert.ToString(dr["JOB_UNIT"]) == "A35")
    //                //{
    //                //    dr["JOB_UNIT_ID"] = 1;
    //                //}
    //                //else if (Convert.ToString(dr["JOB_UNIT"]) == "DELHI" || Convert.ToString(dr["JOB_UNIT"]) == "DLH")
    //                //{
    //                //    dr["JOB_UNIT_ID"] = 2;
    //                //}

    //                //else if (Convert.ToString(dr["JOB_UNIT"]) == "SEZ")
    //                //{
    //                //    dr["JOB_UNIT_ID"] = 3;
    //                //}

    //                //else if (Convert.ToString(dr["JOB_UNIT"]) == "GNU")
    //                //{
    //                //    dr["JOB_UNIT_ID"] = 4;
    //                //}
    //                //else
    //                //{
    //                //    dr["JOB_UNIT"] = string.Empty;
    //                //    dr["JOB_UNIT_ID"] = 0;
    //                //}


    //                jobNo = Convert.ToString(dr["JOB_NO"]);
    //                drawingNo = Convert.ToString(dr["DRAWING_NO"]).ToUpper().Trim();

    //                //searchQuery += "(SELECT DRAWING_ID,JOB_NO,DRAWING_NO FROM tblDesignDrawings where JOB_NO  ='" + jobNo + "' AND DRAWING_NO ='" + drawingNo + "') $" + Environment.NewLine;


    //                int drawingCount = objProject.CheckForPrimaryDuplicacy(drawingNo);
    //                if (drawingCount == 0)
    //                {
    //                    searchQuery += "(select DRAWING_ID,JOB_NO,DRAWING_NO from tblDesignDrawings WHERE IS_DELETED=0 AND DRAWING_NO ='" + drawingNo + "') $" + Environment.NewLine;
    //                }
    //                else if (drawingCount > 0)
    //                {
    //                    searchQuery += "(select RECORD_ID AS DRAWING_ID,JOB_NO,DRAWING_NO from tbldesigndetail WHERE IS_DELETED=0 AND DRAWING_NO ='" + drawingNo + "') $" + Environment.NewLine;
    //                }
    //                else if (drawingCount < 0)
    //                {
    //                    ExceptionMessage("Please try again...!!!");
    //                    return;
    //                }

    //                //searchQuery += "(IF (select COUNT(DRAWING_NO) from tbldesigndetail where DRAWING_NO='OSI2021002.0.408.0003' AND IS_DELETED=0)>0" +
    //                //                 "BEGIN" +
    //                //                        "SELECT RECORD_ID AS DRAWING_ID,JOB_NO,DRAWING_NO from tbldesigndetail where DRAWING_NO = 'OSI2021002.0.408.0003' AND IS_DELETED = 0" +
    //                //                 "END" +
    //                //                 "ELSE" +
    //                //                 "BEGIN" +
    //                //                        "SELECT DRAWING_ID,JOB_NO,DRAWING_NO from tblDesignDrawings where DRAWING_NO = 'OSI2021002.0.408.0003' AND IS_DELETED = 0" +
    //                //                  "END) $" + Environment.NewLine;
    //            }

    //            if (!string.IsNullOrEmpty(searchQuery))
    //            {
    //                searchQuery = searchQuery.Trim();
    //                searchQuery = searchQuery.TrimEnd('\n');
    //                searchQuery = searchQuery.TrimEnd('$');
    //                searchQuery = searchQuery.Replace("$", "UNION");
    //            }


    //            dsDrawingNo = objProject.GetJOBDraiwngsForDMS(searchQuery);

    //            if (dsDrawingNo.Tables.Count > 0 && dsDrawingNo.Tables[0].Rows.Count > 0)
    //            {
    //                foreach (DataRow drtemp in dtTemp.Rows)
    //                {
    //                    //foreach (DataRow drn in dsDrawingNo.Tables[0].Select("JOB_NO='" + Convert.ToString(drtemp["JOB_NO"]) + "' AND DRAWING_NO='" + Convert.ToString(drtemp["DRAWING_NO"]) + "'"))
    //                    //{
    //                    //    drtemp["DRAWING_ID"] = Convert.ToInt32(drn["DRAWING_ID"]);
    //                    //}

    //                    foreach (DataRow drn in dsDrawingNo.Tables[0].Select("DRAWING_NO='" + Convert.ToString(drtemp["DRAWING_NO"]) + "'"))
    //                    {
    //                        drtemp["DRAWING_ID"] = Convert.ToInt32(drn["DRAWING_ID"]);
    //                    }
    //                }
    //            }
    //        }


    //        if (dtTemp.Rows.Count > 0)
    //        {
    //            string drawingNos = string.Empty;

    //            foreach (DataRow dr in dtTemp.Rows)
    //            {
    //                if (!drawingNos.Contains(Convert.ToString(dr["DRAWING_NO"])))
    //                {
    //                    drawingNos += Convert.ToString(dr["DRAWING_NO"]) + ",";
    //                    dr["DUPLICATE_FLAG"] = 0;
    //                }
    //                else
    //                {
    //                    dr["DUPLICATE_FLAG"] = 1;
    //                }
    //            }
    //        }




    //        if (dtTemp.Rows.Count > 0)
    //        {
    //            gvDesignDetails.DataSource = dtTemp;
    //            gvDesignDetails.DataBind();
    //            lblRecords.Text = "Records[" + dtTemp.Rows.Count + "]";
    //        }
    //        else
    //        {
    //            gvDesignDetails.DataSource = null;
    //            gvDesignDetails.DataBind();
    //            lblRecords.Text = "Records[0]";
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

    private void GetDrawingDetail()
    {
        try
        {
            string searchQuery = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;

            DataTable dtTemp = new DataTable();

            #region CREATE_TABLE
            dtTemp.Columns.Add("DRAWING_ID", typeof(string));
            dtTemp.Columns.Add("JOB_NO", typeof(string));
            dtTemp.Columns.Add("DRAWING_NO", typeof(string));
            dtTemp.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
            dtTemp.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));
            dtTemp.Columns.Add("DESCRIPTION", typeof(string));
            dtTemp.Columns.Add("QUANTITY", typeof(string));
            dtTemp.Columns.Add("UOM", typeof(string));
            dtTemp.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dtTemp.Columns.Add("DUPLICATE_FLAG", typeof(int));
            #endregion

            if (fileUploadDrawingFile.HasFile)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(fileUploadDrawingFile.PostedFile.InputStream);

                int rowIndex = 0;

                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();

                    if (row == null || row.Trim() == "")
                        continue;

                    // Skip header
                    if (rowIndex == 0)
                    {
                        rowIndex++;
                        continue;
                    }

                    // CSV parsing
                    System.Collections.Generic.List<string> fieldsList = new System.Collections.Generic.List<string>();
                    bool inQuotes = false;
                    string current = "";

                    foreach (char c in row)
                    {
                        if (c == '"')
                            inQuotes = !inQuotes;
                        else if (c == ',' && !inQuotes)
                        {
                            fieldsList.Add(current);
                            current = "";
                        }
                        else
                            current += c;
                    }
                    fieldsList.Add(current);

                    string[] fields = fieldsList.ToArray();

                    DataRow dr = dtTemp.NewRow();

                    // ✅ FIXED MAPPING (NO SHIFT NOW)
                    dr["JOB_NO"] = fields.Length > 0 ? fields[0].Trim().Replace("\"", "") : "";
                    dr["DRAWING_NO"] = fields.Length > 1 ? fields[1].Trim().Replace("\"", "") : "";
                    dr["CLIENT_DRAWING_NO"] = fields.Length > 2 ? fields[2].Trim().Replace("\"", "") : "";
                    dr["CONTRACTOR_DRAWING_NO"] = fields.Length > 3 ? fields[3].Trim().Replace("\"", "") : "";
                    dr["DESCRIPTION"] = fields.Length > 4 ? fields[4].Trim().Replace("\"", "") : "";
                    dr["QUANTITY"] = fields.Length > 5 ? fields[5].Trim().Replace("\"", "") : "";
                    dr["UOM"] = fields.Length > 6 ? fields[6].Trim().Replace("\"", "") : "";
                    dr["REQD_DATE_BY_PROJECT_TEAM"] = fields.Length > 7 ? fields[7].Trim().Replace("\"", "") : "";

                    dtTemp.Rows.Add(dr);
                    rowIndex++;
                }
            }

            if (dtTemp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    dr["DRAWING_ID"] = 0;

                    string rawDate = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);
                    DateTime parsedDate;

                    if (!string.IsNullOrEmpty(rawDate) && DateTime.TryParse(rawDate, out parsedDate))
                        dr["REQD_DATE_BY_PROJECT_TEAM"] = parsedDate.ToString("dd-MMM-yyyy");
                    else
                        dr["REQD_DATE_BY_PROJECT_TEAM"] = "";

                    jobNo = Convert.ToString(dr["JOB_NO"]).Trim();
                    drawingNo = Convert.ToString(dr["DRAWING_NO"]).Trim().ToUpper();

                    int drawingCount = objProject.CheckForPrimaryDuplicacy(drawingNo);

                    if (drawingCount == 0)
                    {
                        searchQuery += "(select DRAWING_ID,JOB_NO,DRAWING_NO from tblDesignDrawings WHERE IS_DELETED=0 AND DRAWING_NO ='" + drawingNo + "') UNION ";
                    }
                    else if (drawingCount > 0)
                    {
                        searchQuery += "(select RECORD_ID AS DRAWING_ID,JOB_NO,DRAWING_NO from tbldesigndetail WHERE IS_DELETED=0 AND DRAWING_NO ='" + drawingNo + "') UNION ";
                    }
                    else
                    {
                        ExceptionMessage("Please try again...!!!");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(searchQuery) && searchQuery.EndsWith(" UNION "))
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 7);
                }

                dsDrawingNo = objProject.GetJOBDraiwngsForDMS(searchQuery);

                if (dsDrawingNo.Tables.Count > 0 && dsDrawingNo.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drtemp in dtTemp.Rows)
                    {
                        string drwNo = Convert.ToString(drtemp["DRAWING_NO"]);

                        DataRow[] foundRows = dsDrawingNo.Tables[0].Select("DRAWING_NO='" + drwNo + "'");

                        if (foundRows.Length > 0)
                        {
                            drtemp["DRAWING_ID"] = Convert.ToInt32(foundRows[0]["DRAWING_ID"]);
                        }
                    }
                }
            }

            if (dtTemp.Rows.Count > 0)
            {
                System.Collections.Generic.HashSet<string> drawingSet = new System.Collections.Generic.HashSet<string>();

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string drw = Convert.ToString(dr["DRAWING_NO"]);

                    if (drawingSet.Contains(drw))
                        dr["DUPLICATE_FLAG"] = 1;
                    else
                    {
                        drawingSet.Add(drw);
                        dr["DUPLICATE_FLAG"] = 0;
                    }
                }
            }

            if (dtTemp.Rows.Count > 0)
            {
                gvDesignDetails.DataSource = dtTemp;
                gvDesignDetails.DataBind();
                lblRecords.Text = "Records[" + dtTemp.Rows.Count + "]";
            }
            else
            {
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
                lblRecords.Text = "Records[0]";
                ExceptionMessage("No data found..!!!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.Message);
        }
    }


    private void ImportDrawings()
    {
        try
        {
            int value = 0;
            int count1 = 0;
            int count2 = 0;

            jobNo = string.Empty;
            drawingNo = string.Empty;
            description = string.Empty;
            quantity = 0;
            UOM = string.Empty;
            rqdDateByProjectTeam = string.Empty;

            DataTable dtTempDrawing = new DataTable();
            dtTempDrawing.Columns.Add("JOB_NO", typeof(string));
            //dtTempDrawing.Columns.Add("JOB_UNIT_ID", typeof(int));
            dtTempDrawing.Columns.Add("DRAWING_NO", typeof(string));
            dtTempDrawing.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
            dtTempDrawing.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));
            dtTempDrawing.Columns.Add("DESCRIPTION", typeof(string));
            dtTempDrawing.Columns.Add("QUANTITY", typeof(int));
            dtTempDrawing.Columns.Add("UOM", typeof(string));
            dtTempDrawing.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dtTempDrawing.Columns.Add("CREATED_BY", typeof(int));
            bool check = true;

            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                DataRow dr = dtTempDrawing.NewRow();

                Label lblDrawingID = (Label)gr.FindControl("lblDrawingID");

                //Label lblJOBUnitUD = (Label)gr.FindControl("lblJOBUnitUD");
                //DropDownList ddlJOBUnit = (DropDownList)gr.FindControl("ddlJOBUnit");
                //Label lblJOBUnit = (Label)gr.FindControl("lblJOBUnit");

                Label lblDuplicateFlag = (Label)gr.FindControl("lblDuplicateFlag");
                Label lblJOBNo = (Label)gr.FindControl("lblJOBNo");
                Label lblDrawingNo = (Label)gr.FindControl("lblDrawingNo");

                Label lblClientDrawingNo = (Label)gr.FindControl("lblClientDrawingNo");
                Label lblContractorDrawingNo = (Label)gr.FindControl("lblContractorDrawingNo");

                Label lblDescription = (Label)gr.FindControl("lblDescription");
                TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                Label lblUOM = (Label)gr.FindControl("lblUOM");
                TextBox txtRqdDateByProjectTeam = (TextBox)gr.FindControl("txtRqdDateByProjectTeam");

                if (Convert.ToInt32(lblDrawingID.Text) == 0 && Convert.ToInt32(lblDuplicateFlag.Text) == 0)
                {
                    count1++;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
                    {
                        dr["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                        lblJOBNo.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        dr["JOB_NO"] = string.Empty;
                        lblJOBNo.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                        ExceptionMessage("Please enter JOB number in CSV file...!!!");
                        //return;
                    }

                    //if (ddlJOBUnit.Items.Count>0)
                    //{
                    //    dr["JOB_UNIT_ID"] = Convert.ToInt32(ddlJOBUnit.SelectedValue);
                    //}
                    //else
                    //{
                    //    dr["JOB_UNIT_ID"] = 0;
                    //    ddlJOBUnit.BackColor = System.Drawing.Color.LightPink;
                    //    check = false;
                    //    ExceptionMessage("Please refresh the page and try again...!!!");
                    //}


                    if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text)))
                    {
                        dr["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text);
                        lblDrawingNo.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        dr["DRAWING_NO"] = string.Empty;
                        lblDrawingNo.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                        ExceptionMessage("Please enter drawing number in CSV file...!!!");
                        //return;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblClientDrawingNo.Text)))
                    {
                        dr["CLIENT_DRAWING_NO"] = Convert.ToString(lblClientDrawingNo.Text);
                        lblClientDrawingNo.BackColor = System.Drawing.Color.Transparent;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblContractorDrawingNo.Text)))
                    {
                        dr["CONTRACTOR_DRAWING_NO"] = Convert.ToString(lblContractorDrawingNo.Text);
                        lblContractorDrawingNo.BackColor = System.Drawing.Color.Transparent;
                    }


                    if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
                    {
                        dr["DESCRIPTION"] = Convert.ToString(lblDescription.Text);
                        lblDescription.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        dr["DESCRIPTION"] = string.Empty;
                        lblDescription.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                        ExceptionMessage("Please enter description in CSV file...!!!");
                        //return;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text)))
                    {
                        if (Convert.ToInt32(txtQuantity.Text) > 0)
                        {
                            dr["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
                            txtQuantity.BackColor = System.Drawing.Color.Transparent;
                        }
                        else
                        {
                            dr["QUANTITY"] = 0;
                            txtQuantity.BackColor = System.Drawing.Color.LightPink;
                            check = false;
                            ExceptionMessage("Quantity must be greater than 0 in CSV file...!!!");
                            //return;
                        }
                    }
                    else
                    {
                        txtQuantity.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                        ExceptionMessage("Please enter quantity in CSV file...!!!");
                        //return;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text)))
                    {
                        dr["UOM"] = Convert.ToString(lblUOM.Text);
                        lblUOM.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        dr["UOM"] = string.Empty;
                        lblUOM.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                        ExceptionMessage("Please enter UOM in CSV file...!!!");
                        //return;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(txtRqdDateByProjectTeam.Text)))
                        dr["REQD_DATE_BY_PROJECT_TEAM"] = Convert.ToDateTime(txtRqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");
                    else
                        dr["REQD_DATE_BY_PROJECT_TEAM"] = string.Empty;

                    dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                    if (check)
                    {
                        dtTempDrawing.Rows.Add(dr);
                    }
                    else
                    {
                        dtTempDrawing.Rows.Clear();
                        return;
                    }
                }
                else
                {
                    count2++;
                    //ExceptionMessage("Duplicate drawing numbers in list...!!!");
                    //return;
                }
            }

            if (dtTempDrawing.Rows.Count > 0)
            {
                value = objProject.AddDesignDrawing(dtTempDrawing);
            }

            if (value > 0)
            {
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
                SuccessMessage(count1 + " Record(s) imported successfully and, " + count2 + " record(s) already exists...!!!");
                return;
            }
            else
            {
                if (count1 == 0 && count2 > 0)
                {
                    gvDesignDetails.DataSource = null;
                    gvDesignDetails.DataBind();
                    ExceptionMessage(count2 + " record(s) already exists...!!!");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
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
