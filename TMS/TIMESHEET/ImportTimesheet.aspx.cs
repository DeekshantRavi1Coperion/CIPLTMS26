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
using System.Collections.Generic;

public partial class TIMESHEET_ImportTimesheet : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    DataSet dsUserAndTypeOfDrawing = new DataSet();
    DataTable dtTimesheet = new DataTable();
    DataTable dt1 = new DataTable();
    DataSet ds1 = new DataSet();

    int empRecordID = 0;
    string entryDate = string.Empty;
    string projectCategory = string.Empty;
    string projectNumber = string.Empty;
    string jobNumber = string.Empty;
    string drawingCategory = string.Empty;
    string serialNumber = string.Empty;
    string size = string.Empty;
    string rev = string.Empty;
    int sheets = 0;
    string drawingID = string.Empty;
    int drawingTypeID = 0;
    string startTime = string.Empty;
    string endTime = string.Empty;
    string timeSpent = string.Empty;
    string title = string.Empty;
    string remarks = string.Empty;
    int srNo = 0;

    int count = 0;
    string srNoForRemoval = string.Empty;

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["dsUserAndTypeOfDrawing"] = objTimesheet.GetUserForTimesheet();
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

    protected void btnGetTimesheetDetails_Click(object sender, EventArgs e)
    {
        GetTimesheeetDetails();
    }

    protected void gvTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string userNameID = "0";
                string drawingTypeID = "0";

                if (Session["dsUserAndTypeOfDrawing"] != null)
                    dsUserAndTypeOfDrawing = (DataSet)Session["dsUserAndTypeOfDrawing"];
                else
                    dsUserAndTypeOfDrawing = objTimesheet.GetUserForTimesheet();

                Label lblEmpRecordID = (Label)e.Row.FindControl("lblEmpRecordID");
                Label lblDrawingTypeID = (Label)e.Row.FindControl("lblDrawingTypeID");

                DropDownList ddlUserName = (DropDownList)e.Row.FindControl("ddlUserName");
                DropDownList ddlDrawingType = (DropDownList)e.Row.FindControl("ddlDrawingType");

                if (!string.IsNullOrEmpty(Convert.ToString(lblEmpRecordID.Text)))
                    userNameID = Convert.ToString(lblEmpRecordID.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingTypeID.Text)))
                    drawingTypeID = Convert.ToString(lblDrawingTypeID.Text);

                if (dsUserAndTypeOfDrawing.Tables.Count > 0)
                {
                    if (dsUserAndTypeOfDrawing.Tables[0].Rows.Count > 0)
                    {
                        ddlUserName.DataSource = dsUserAndTypeOfDrawing.Tables[0];
                        ddlUserName.DataTextField = "USER_NAME";
                        ddlUserName.DataValueField = "EMP_RECORD_ID";
                        ddlUserName.DataBind();
                        ddlUserName.Items.Insert(0, "Select");

                        if (Convert.ToInt32(userNameID) > 0)
                            ddlUserName.SelectedValue = userNameID;
                        else
                            ddlUserName.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlUserName.Items.Insert(0, "Select");
                        ddlUserName.SelectedIndex = 0;
                    }

                    if (dsUserAndTypeOfDrawing.Tables[1].Rows.Count > 0)
                    {
                        ddlDrawingType.DataSource = dsUserAndTypeOfDrawing.Tables[1];
                        ddlDrawingType.DataTextField = "DRAWING_TYPE";
                        ddlDrawingType.DataValueField = "DRAWING_TYPE_ID";
                        ddlDrawingType.DataBind();
                        ddlDrawingType.Items.Insert(0, "Select");

                        if (Convert.ToInt32(drawingTypeID) > 0)
                            ddlDrawingType.SelectedValue = drawingTypeID;
                        else
                            ddlDrawingType.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlUserName.Items.Insert(0, "Select");
                    ddlUserName.SelectedValue = Convert.ToString(lblEmpRecordID.Text);

                    ddlDrawingType.Items.Insert(0, "Select");
                    ddlDrawingType.SelectedValue = Convert.ToString(lblDrawingTypeID.Text);
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
        if (gvTimesheet.Rows.Count > 0)
        {
            ImportTimesheet();
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

            csv = "USER_NAME" + ',';
            csv += "ENTRY_DATE" + ',';
            csv += "PROJECT_CATETORY" + ',';
            csv += "PROJECT_NUMBER" + ',';
            csv += "JOB_NUMBER" + ',';
            csv += "DRAWING_CATEGORY" + ',';
            csv += "SERIAL_NUMBER" + ',';
            csv += "SIZE" + ',';
            csv += "REV" + ',';
            csv += "SHEETS" + ',';
            csv += "DRAWING_ID" + ',';
            csv += "DRAWING_TYPE" + ',';
            csv += "START_TIME" + ',';
            csv += "END_TIME" + ',';
            csv += "TIME_SPENT" + ',';
            csv += "TITLE" + ',';
            csv += "REMARKS" + ',';
            csv += "\r\n";

            string fileName = "Timesheet";
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

    private void GetTimesheeetDetails()
    {
        int cc1 = 0;       
        try
        {
            if (Session["dsUserAndTypeOfDrawing"] != null)
                dsUserAndTypeOfDrawing = (DataSet)Session["dsUserAndTypeOfDrawing"];
            else
                dsUserAndTypeOfDrawing = objTimesheet.GetUserForTimesheet();

            hdGVRowCount.Value = "0";
            int count = 0;

            #region CREATE_TABLE

            dtTimesheet.Columns.Add("EMP_RECORD_ID", typeof(int));
            dtTimesheet.Columns.Add("DRAWING_TYPE_ID", typeof(int));
            dtTimesheet.Columns.Add("USER_NAME", typeof(string));
            dtTimesheet.Columns.Add("ENTRY_DATE", typeof(string));
            dtTimesheet.Columns.Add("PROJECT_CATETORY", typeof(string));
            dtTimesheet.Columns.Add("PROJECT_NUMBER", typeof(string));
            dtTimesheet.Columns.Add("JOB_NUMBER", typeof(string));
            dtTimesheet.Columns.Add("DRAWING_CATEGORY", typeof(string));
            dtTimesheet.Columns.Add("SERIAL_NUMBER", typeof(string));
            dtTimesheet.Columns.Add("SIZE", typeof(string));
            dtTimesheet.Columns.Add("REV", typeof(string));
            dtTimesheet.Columns.Add("SHEETS", typeof(string));
            dtTimesheet.Columns.Add("DRAWING_ID", typeof(string));
            dtTimesheet.Columns.Add("DRAWING_TYPE", typeof(string));
            dtTimesheet.Columns.Add("START_TIME", typeof(string));
            dtTimesheet.Columns.Add("END_TIME", typeof(string));
            dtTimesheet.Columns.Add("TIME_SPENT", typeof(string));
            dtTimesheet.Columns.Add("TITLE", typeof(string));
            dtTimesheet.Columns.Add("REMARKS", typeof(string));
            dtTimesheet.Columns.Add("SR_NO", typeof(int));

            #endregion


            if (fileUploadTimesheet.HasFile)
            {
                int csvRowOneColumnsCount = 0;
                int csvAllColumnsCount = 0;

                string txt = string.Empty;

                if (!string.IsNullOrEmpty(fileUploadTimesheet.PostedFile.FileName))
                {
                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadTimesheet.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        cc1++;
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtTimesheet.Rows.Add();

                            int i = 0;
                            string dtColunValue = string.Empty;

                            string[] strRowText = row.Split(',');
                            if (cc1 == 1)
                                csvRowOneColumnsCount = strRowText.Length;

                            if (cc1 > 1)
                                csvAllColumnsCount = strRowText.Length;

                            if (csvAllColumnsCount > csvRowOneColumnsCount)
                            {
                                strRowText[16] = Convert.ToString(strRowText[16] + strRowText[17]).Replace('"', ' ');
                            }

                            foreach (string cell in strRowText)
                            {
                                dtColunValue = cell.Trim();

                                if (i <= (dtTimesheet.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dtColunValue))
                                    {
                                        if ((i + 2) <= 18)
                                            dtTimesheet.Rows[dtTimesheet.Rows.Count - 1][i + 2] = dtColunValue;
                                    }
                                    else
                                    {
                                        if ((i + 2) <= 18)
                                            dtTimesheet.Rows[dtTimesheet.Rows.Count - 1][i + 2] = string.Empty;
                                    }

                                    i++;
                                }
                            }
                        }
                    }
                }
            }


            if (dtTimesheet.Rows.Count > 0)
            {
                dtTimesheet.Rows.RemoveAt(0);
            }


            
            if (dtTimesheet.Rows.Count > 0)
            {
                foreach (DataRow drt in dtTimesheet.Rows)
                {
                    
                    if (drt["ENTRY_DATE"] != DBNull.Value)
                        drt["ENTRY_DATE"] = GetDate(Convert.ToString(drt["ENTRY_DATE"]));

                    if (drt["START_TIME"] != DBNull.Value)
                        drt["START_TIME"] = GetTime(Convert.ToString(drt["START_TIME"]));

                    if (drt["END_TIME"] != DBNull.Value)
                        drt["END_TIME"] = GetTime(Convert.ToString(drt["END_TIME"]));

                    //if (drt["TIME_SPENT"] != DBNull.Value)
                    //{
                    //    TimeSpan duration = DateTime.Parse(Convert.ToDateTime(drt["END_TIME"]).ToString("HH:mm")).Subtract(DateTime.Parse(Convert.ToDateTime(drt["START_TIME"]).ToString("HH:mm")));
                    //    drt["TIME_SPENT"] = Convert.ToString(duration);

                    //    string[] strTimeSpent = Convert.ToString(duration).Split(':');
                    //    string hh = string.Empty;
                    //    string mm = string.Empty;

                    //    hh = Convert.ToString(strTimeSpent[0]);
                    //    mm = Convert.ToString(strTimeSpent[1]);

                    //    if (Convert.ToString(strTimeSpent[0]).Length < 2)
                    //        hh = "0" + Convert.ToString(strTimeSpent[0]);

                    //    if (Convert.ToString(strTimeSpent[1]).Length < 2)
                    //        mm = "0" + Convert.ToString(strTimeSpent[1]);

                    //    drt["TIME_SPENT"] = Convert.ToString(hh + ":" + mm);
                    //}

                    if (drt["TIME_SPENT"] != DBNull.Value)
                    {
                        if (drt["START_TIME"] != DBNull.Value && drt["END_TIME"] != DBNull.Value)
                        {
                            DateTime startTime = Convert.ToDateTime(drt["START_TIME"]);
                            DateTime endTime = Convert.ToDateTime(drt["END_TIME"]);
                            TimeSpan duration = endTime - startTime;

                            string hh = duration.Hours.ToString("D2");
                            string mm = duration.Minutes.ToString("D2");

                            drt["TIME_SPENT"] = hh + ":" + mm;
                        }
                        else
                        {
                            drt["TIME_SPENT"] = "00:00"; // Default value if start or end time is null
                        }
                    }




                    if (dsUserAndTypeOfDrawing.Tables.Count > 0)
                    {
                        if (dsUserAndTypeOfDrawing.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsUserAndTypeOfDrawing.Tables[0].Select("USER_NAME='" + Convert.ToString(drt["USER_NAME"]) + "'"))
                            {
                                drt["EMP_RECORD_ID"] = dr["EMP_RECORD_ID"];
                            }
                        }

                        if (dsUserAndTypeOfDrawing.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsUserAndTypeOfDrawing.Tables[1].Select("DRAWING_TYPE='" + Convert.ToString(drt["DRAWING_TYPE"]) + "'"))
                            {
                                drt["DRAWING_TYPE_ID"] = dr["DRAWING_TYPE_ID"];
                            }
                        }
                    }
                    else
                    {
                        drt["EMP_RECORD_ID"] = 0;
                        drt["DRAWING_TYPE_ID"] = 0;
                    }
                }
            }



            foreach (DataRow dr in dtTimesheet.Rows)
            {
                count++;
                dr["SR_NO"] = count;

                if (string.IsNullOrEmpty(Convert.ToString(dr["EMP_RECORD_ID"])))
                    dr["EMP_RECORD_ID"] = 0;

                if (string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_TYPE_ID"])))
                    dr["DRAWING_TYPE_ID"] = 0;
            }

            if (dtTimesheet.Rows.Count > 0)
            {
                Session["dtTimesheet"] = dtTimesheet;
                gvTimesheet.DataSource = dtTimesheet;
                gvTimesheet.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvTimesheet.Rows.Count);
                lblRecords.Text = "Records[" + dtTimesheet.Rows.Count + "]";
            }
            else
            {
                Session["dtTimesheet"] = null;
                gvTimesheet.DataSource = null;
                gvTimesheet.DataBind();
                hdGVRowCount.Value = "0";
                lblRecords.Text = "Records[0]";
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

    private string GetDate(string entryDate)
    {
        try
        {
            bool check = false;
            string dateText;

            string year = string.Empty;
            string month = string.Empty;
            string date = string.Empty;

            if (entryDate.Contains("/"))
            {
                string[] strEntryDate = entryDate.Split('/');

                date = Convert.ToString(strEntryDate[0]);
                month = Convert.ToString(strEntryDate[1]);
                year = Convert.ToString(strEntryDate[2]);



                if (year.Length < 4 || Convert.ToInt32(month) > 12 || Convert.ToInt32(date) > 31)
                    check = false;
                else
                    check = true;

                if (check)
                    dateText = Convert.ToDateTime(year + "-" + month + "-" + date).ToString("dd-MMM-yyyy");
                else
                    dateText = string.Empty;
            }
            else
                dateText = Convert.ToDateTime(entryDate).ToString("dd-MMM-yyyy");


            if (!string.IsNullOrEmpty(dateText))
                return dateText;
            else
                return null;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private string GetTime(string time)
    {
        try
        {
            string timeText;

            string hours = string.Empty;
            string mins = string.Empty;

            if (time.Contains("."))
                time = time.Replace(".", ":");


            if (time.Contains("AM") == true || time.Contains("PM") == true)
            {
                timeText = Convert.ToDateTime(time).ToString("HH:mm");
            }
            else
            {
                if (time.Contains(":"))
                {
                    string[] strTime = time.Split(':');

                    hours = Convert.ToString(strTime[0]);
                    mins = Convert.ToString(strTime[1]);

                    if (hours.Length < 2)
                        hours = "0" + hours;

                    if (mins.Length < 2)
                        mins = "0" + mins;


                    timeText = Convert.ToDateTime(hours + ":" + mins).ToString("HH:mm");
                }
                else
                    timeText = string.Empty;
            }

            if (!string.IsNullOrEmpty(timeText))
                return timeText;
            else
                return null;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void ImportTimesheet()
    {
        try
        {
            count = 0;
            srNoForRemoval = string.Empty;

            DataTable dtTempTimesheet = new DataTable();

            dtTempTimesheet.Columns.Add("EMP_RECORD_ID", typeof(int));
            dtTempTimesheet.Columns.Add("ENTRY_DATE", typeof(string));
            dtTempTimesheet.Columns.Add("PROJECT_CATETORY", typeof(string));
            dtTempTimesheet.Columns.Add("PROJECT_NUMBER", typeof(string));
            dtTempTimesheet.Columns.Add("JOB_NUMBER", typeof(string));
            dtTempTimesheet.Columns.Add("DRAWING_CATEGORY", typeof(string));
            dtTempTimesheet.Columns.Add("SERIAL_NUMBER", typeof(string));
            dtTempTimesheet.Columns.Add("SIZE", typeof(string));
            dtTempTimesheet.Columns.Add("REV", typeof(string));
            dtTempTimesheet.Columns.Add("SHEETS", typeof(int));
            dtTempTimesheet.Columns.Add("DRAWING_ID", typeof(string));
            dtTempTimesheet.Columns.Add("DRAWING_TYPE_ID", typeof(int));
            dtTempTimesheet.Columns.Add("START_TIME", typeof(string));
            dtTempTimesheet.Columns.Add("END_TIME", typeof(string));
            dtTempTimesheet.Columns.Add("TIME_SPENT", typeof(string));
            dtTempTimesheet.Columns.Add("TITLE", typeof(string));
            dtTempTimesheet.Columns.Add("REMARKS", typeof(string));
            dtTempTimesheet.Columns.Add("CREATED_BY", typeof(int));

            int value = 0;

            foreach (GridViewRow gr in gvTimesheet.Rows)
            {
                empRecordID = 0;
                entryDate = string.Empty;
                projectCategory = string.Empty;
                projectNumber = string.Empty;
                jobNumber = string.Empty;
                drawingCategory = string.Empty;
                serialNumber = string.Empty;
                size = string.Empty;
                rev = string.Empty;
                sheets = 0;
                drawingID = string.Empty;
                drawingTypeID = 0;
                startTime = string.Empty;
                endTime = string.Empty;
                timeSpent = string.Empty;
                title = string.Empty;
                remarks = string.Empty;
                srNo = 0;

                DataRow dr = dtTempTimesheet.NewRow();

                DropDownList ddlUserName = (DropDownList)gr.FindControl("ddlUserName");
                TextBox txtEntryDate = (TextBox)gr.FindControl("txtEntryDate");
                Label lblProjectCategory = (Label)gr.FindControl("lblProjectCategory");
                Label lblProjectNumber = (Label)gr.FindControl("lblProjectNumber");
                Label lblJobNumber = (Label)gr.FindControl("lblJobNumber");
                Label lblDrwaingCategory = (Label)gr.FindControl("lblDrwaingCategory");
                Label lblSerialNumber = (Label)gr.FindControl("lblSerialNumber");
                Label lblSize = (Label)gr.FindControl("lblSize");
                Label lblRev = (Label)gr.FindControl("lblRev");
                Label lblSheets = (Label)gr.FindControl("lblSheets");
                Label lblDrawingID = (Label)gr.FindControl("lblDrawingID");
                DropDownList ddlDrawingType = (DropDownList)gr.FindControl("ddlDrawingType");
                Label lblStartTime = (Label)gr.FindControl("lblStartTime");
                Label lblEndTime = (Label)gr.FindControl("lblEndTime");
                Label lblTimeSpent = (Label)gr.FindControl("lblTimeSpent");
                Label lblTitle = (Label)gr.FindControl("lblTitle");
                Label lblRemarks = (Label)gr.FindControl("lblRemarks");
                Label lblSRNo = (Label)gr.FindControl("lblSRNo");

                if (Convert.ToInt32(ddlUserName.SelectedIndex) > 0)
                    empRecordID = Convert.ToInt32(ddlUserName.SelectedValue);

                if (!string.IsNullOrEmpty(Convert.ToString(txtEntryDate.Text)))
                    entryDate = Convert.ToDateTime(txtEntryDate.Text).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(Convert.ToString(lblProjectCategory.Text)))
                    projectCategory = Convert.ToString(lblProjectCategory.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblProjectNumber.Text)))
                    projectNumber = Convert.ToString(lblProjectNumber.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblJobNumber.Text)))
                    jobNumber = Convert.ToString(lblJobNumber.Text).Split('.')[0];

                if (!string.IsNullOrEmpty(Convert.ToString(lblDrwaingCategory.Text)))
                    drawingCategory = Convert.ToString(lblDrwaingCategory.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblSerialNumber.Text)))
                    serialNumber = Convert.ToString(lblSerialNumber.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblSize.Text)))
                    size = Convert.ToString(lblSize.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblRev.Text)))
                    rev = Convert.ToString(lblRev.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblSheets.Text)))
                    sheets = Convert.ToInt32(lblSheets.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingID.Text)))
                    drawingID = Convert.ToString(lblDrawingID.Text).Replace("'", " ");

                if (Convert.ToInt32(ddlDrawingType.SelectedIndex) > 0)
                    drawingTypeID = Convert.ToInt32(ddlDrawingType.SelectedValue);

                if (!string.IsNullOrEmpty(Convert.ToString(lblStartTime.Text)))
                    startTime = Convert.ToString(lblStartTime.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblEndTime.Text)))
                    endTime = Convert.ToString(lblEndTime.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblTimeSpent.Text)))
                    timeSpent = Convert.ToString(lblTimeSpent.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblTitle.Text)))
                    title = Convert.ToString(lblTitle.Text).Replace("'", " ");

                if (!string.IsNullOrEmpty(Convert.ToString(lblRemarks.Text)))
                    remarks = Convert.ToString(lblRemarks.Text).Replace("'", " ");

                srNo = Convert.ToInt32(lblSRNo.Text);

                if (empRecordID > 0 && !string.IsNullOrEmpty(entryDate))
                {
                    count++;
                    srNoForRemoval += srNo + ",";

                    dr["EMP_RECORD_ID"] = empRecordID;
                    dr["ENTRY_DATE"] = entryDate;
                    dr["PROJECT_CATETORY"] = projectCategory;
                    dr["PROJECT_NUMBER"] = projectNumber;
                    dr["JOB_NUMBER"] = jobNumber;
                    dr["DRAWING_CATEGORY"] = drawingCategory;
                    dr["SERIAL_NUMBER"] = serialNumber;
                    dr["SIZE"] = size;
                    dr["REV"] = rev;
                    dr["SHEETS"] = sheets;
                    dr["DRAWING_ID"] = drawingID;
                    dr["DRAWING_TYPE_ID"] = drawingTypeID;
                    dr["START_TIME"] = startTime;
                    dr["END_TIME"] = endTime;
                    dr["TIME_SPENT"] = timeSpent;
                    dr["TITLE"] = title;
                    dr["REMARKS"] = remarks;
                    dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                    dtTempTimesheet.Rows.Add(dr);
                }
            }

            value = objTimesheet.ImportTimesheet(dtTempTimesheet);

            if (value > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvTimesheet.DataSource = null;
                    gvTimesheet.DataBind();
                    lblRecords.Text = "Records[" + gvTimesheet.Rows.Count + "]";
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
            dt1 = (DataTable)Session["dtTimesheet"];
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
                gvTimesheet.DataSource = dt1;
                gvTimesheet.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvTimesheet.Rows.Count);
            }
            else
            {
                gvTimesheet.DataSource = null;
                gvTimesheet.DataBind();
                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + gvTimesheet.Rows.Count + "]";
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
