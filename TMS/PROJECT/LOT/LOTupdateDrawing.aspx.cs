using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PROJECT_LOT_LOTupdateDrawing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    BAL.Project objProject = new BAL.Project();

    int recordID = 0;


    string SiDrawingFile1 = string.Empty;
    Byte[] SiDrawingFileBytes1 = null;

    string SiDrawingFile2 = string.Empty;
    Byte[] SiDrawingFileBytes2 = null;

    string SiDrawingFile3 = string.Empty;
    Byte[] SiDrawingFileBytes3 = null;

    string SiDrawingFile4 = string.Empty;
    Byte[] SiDrawingFileBytes4 = null;

    string clientApprovedDrawingFile = string.Empty;
    Byte[] clientApprovedDrawingFileBytes = null;


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SiDrawingFile1 = string.Empty;
        SiDrawingFileBytes1 = null;

        SiDrawingFile2 = string.Empty;
        SiDrawingFileBytes2 = null;

        SiDrawingFile3 = string.Empty;
        SiDrawingFileBytes3 = null;

        SiDrawingFile4 = string.Empty;
        SiDrawingFileBytes4 = null;

        if (uploadFileSiDrawing1.HasFile)
        {
            if (!string.IsNullOrEmpty(uploadFileSiDrawing1.PostedFile.FileName))
            {
                string[] str = uploadFileSiDrawing1.PostedFile.FileName.Split('\\');
                int length = str.Length;
                SiDrawingFile1 = str[str.Length - 1];
                SiDrawingFileBytes1 = GetFileBytes(uploadFileSiDrawing1.PostedFile.FileName, uploadFileSiDrawing1.PostedFile.InputStream);
            }
        }

        if (uploadFileSiDrawing2.HasFile)
        {
            if (!string.IsNullOrEmpty(uploadFileSiDrawing2.PostedFile.FileName))
            {
                string[] str = uploadFileSiDrawing2.PostedFile.FileName.Split('\\');
                int length = str.Length;
                SiDrawingFile2 = str[str.Length - 1];
                SiDrawingFileBytes2 = GetFileBytes(uploadFileSiDrawing2.PostedFile.FileName, uploadFileSiDrawing2.PostedFile.InputStream);
            }
        }

        if (uploadFileSiDrawing3.HasFile)
        {
            if (!string.IsNullOrEmpty(uploadFileSiDrawing3.PostedFile.FileName))
            {
                string[] str = uploadFileSiDrawing3.PostedFile.FileName.Split('\\');
                int length = str.Length;
                SiDrawingFile3 = str[str.Length - 1];
                SiDrawingFileBytes3 = GetFileBytes(uploadFileSiDrawing3.PostedFile.FileName, uploadFileSiDrawing3.PostedFile.InputStream);
            }
        }

        if (uploadFileSiDrawing4.HasFile)
        {
            if (!string.IsNullOrEmpty(uploadFileSiDrawing4.PostedFile.FileName))
            {
                string[] str = uploadFileSiDrawing4.PostedFile.FileName.Split('\\');
                int length = str.Length;
                SiDrawingFile4 = str[str.Length - 1];
                SiDrawingFileBytes4 = GetFileBytes(uploadFileSiDrawing4.PostedFile.FileName, uploadFileSiDrawing4.PostedFile.InputStream);
            }
        }

        recordID = Convert.ToInt32(txtRecordID.Text);

        int value = objProject.UpdateLOTDrawings(recordID, SiDrawingFile1, SiDrawingFileBytes1,
                                                            SiDrawingFile2, SiDrawingFileBytes2,
                                                            SiDrawingFile3, SiDrawingFileBytes3,
                                                            SiDrawingFile4, SiDrawingFileBytes4);
        if (value > 0)
        {
            SuccessMessage("Updation successfull...!!!");
            txtRecordID.Text = String.Empty;
            return;
        }
        else
        {
            ExceptionMessage("Please try again...!!!");
            return;
        }
    }


    protected void btnUpdateClientApprovedDrawing_Click(object sender, EventArgs e)
    {
        string clientApprovedDrawingFile = string.Empty;
        Byte[] clientApprovedDrawingFileBytes = null;

        if (uploadFileClientApprovedDrawing.HasFile)
        {
            if (!string.IsNullOrEmpty(uploadFileClientApprovedDrawing.PostedFile.FileName))
            {
                string[] str = uploadFileClientApprovedDrawing.PostedFile.FileName.Split('\\');
                int length = str.Length;
                clientApprovedDrawingFile = str[str.Length - 1];
                clientApprovedDrawingFileBytes = GetFileBytes(uploadFileClientApprovedDrawing.PostedFile.FileName, uploadFileClientApprovedDrawing.PostedFile.InputStream);
            }
        }

        int lotTfId = Convert.ToInt32(txtLotTfId.Text);

        int value = objProject.UpdateLOTClientApprovedDrawing(lotTfId
                                                            , clientApprovedDrawingFile
                                                            , clientApprovedDrawingFileBytes);
        if (value > 0)
        {
            SuccessMessage("Updation successfull...!!!");
            txtLotTfId.Text = String.Empty;
            return;
        }
        else
        {
            ExceptionMessage("Please try again...!!!");
            return;
        }
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
                //ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
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