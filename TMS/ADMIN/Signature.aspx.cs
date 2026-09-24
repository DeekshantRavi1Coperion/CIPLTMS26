using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_Signature : System.Web.UI.Page
{

    DataSet dsEmployee = new DataSet();
     BAL.Common objCommon = new BAL.Common();
    string SiDrawingFile1 = string.Empty;
    Byte[] SiDrawingFileBytes1 = null;
    int empRecordID = 0;
    int createdBy = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindEmployee();
            }
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;


            SiDrawingFile1 = string.Empty;
            SiDrawingFileBytes1 = null;

            if (uploadSignFile.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadSignFile.PostedFile.FileName))
                {
                    string[] str = uploadSignFile.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawingFile1 = str[str.Length - 1];
                    SiDrawingFileBytes1 = GetFileBytes(uploadSignFile.PostedFile.FileName, uploadSignFile.PostedFile.InputStream);
                }
            }

            if (Session["EMP_RECORD_ID"] != null)
            {
                createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            }

            // int value = objCommon.UploadSign();
            int value = objCommon.UploadSign(empRecordID, SiDrawingFileBytes1, createdBy);

            if (value > 0)
            {
                SuccessMessage("Signature saved successfully!");
            }
            else
            {
                ExceptionMessage("Please try again!");
            }

            Reset();

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }


    }

    private void Reset()
    {
        ddlEmployee.SelectedIndex = 0;

    }

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionMessage("File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }


    private void BindEmployee()
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeForSign();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
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
}