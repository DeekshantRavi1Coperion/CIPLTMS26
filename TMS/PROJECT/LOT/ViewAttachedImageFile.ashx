<%@ WebHandler Language="C#" Class="ViewAttachedImageFile" %>

using System;
using System.Web;
using System.IO;

public class ViewAttachedImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.Project objProject = new BAL.Project();
    int LOTTFSubitemID = 0;
    string fileName = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        LOTTFSubitemID = 0;
        byte[] bytes = null;
        fileName = string.Empty;
        string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        LOTTFSubitemID = Convert.ToInt32(context.Request.QueryString["LOTTFSubitemID"]);
        if (context.Request.QueryString["LOTTFID"] != null)
        {

            dsFiles = objProject.GetLOTDrawingFiles(Convert.ToInt32(context.Request.QueryString["LOTTFID"]), LOTTFSubitemID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "DRAWING1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                }
                else if (fileType == "DRAWING2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT2_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT2_NAME"]);
                }
                else if (fileType == "DRAWING3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT3_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT3_NAME"]);
                }
                else if (fileType == "DRAWING4")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT4_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT4_NAME"]);
                }
                else if (fileType == "IRN")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_NAME"]);
                }

                else if (fileType == "STANDARD_DRAWING")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["STANDARD_DRAWING_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["STANDARD_DRAWING_NAME"]);
                }

                if (bytes != null)
                {
                    context.Response.BinaryWrite(bytes);
                    context.Response.End();
                }
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }






    //System.Data.DataSet dsFiles = new System.Data.DataSet();
    //public void ProcessRequest(HttpContext context)
    //{
    //    byte[] bytes = null;

    //    if (!string.IsNullOrEmpty(Convert.ToString(context.Request.QueryString["fileName"])))
    //    {
    //        string fileName = Convert.ToString(context.Request.QueryString["fileName"]);
    //        bytes = GetFileBytes(fileName);
    //    }


    //    if (bytes != null)
    //    {
    //        context.Response.BinaryWrite(bytes);
    //        context.Response.End();
    //    }
    //}

    //private byte[] GetFileBytes(string fileName)
    //{
    //    Byte[] bytes = null;
    //    string FilePath = fileName;
    //    string FileName = Path.GetFileName(FilePath);
    //    string Text = Path.GetExtension(FileName);
    //    string ContentType = String.Empty;
    //    switch (Text)
    //    {
    //        case ".jpg":
    //            ContentType = "image/jpg";
    //            break;
    //        case ".jpeg":
    //            ContentType = "image/jpeg";
    //            break;
    //        case ".bmp":
    //            ContentType = "image/bmp";
    //            break;
    //        case ".png":
    //            ContentType = "image/png";
    //            break;
    //        case ".gif":
    //            ContentType = "image/gif";
    //            break;
    //        case ".pdf":
    //            ContentType = "application/pdf";
    //            break;
    //        case ".JPG":
    //            ContentType = "image/JPG";
    //            break;
    //        case ".JPEG":
    //            ContentType = "image/JPEG";
    //            break;
    //        case ".BMP":
    //            ContentType = "image/BMP";
    //            break;
    //        case ".PNG":
    //            ContentType = "image/PNG";
    //            break;
    //        case ".GIF":
    //            ContentType = "image/GIF";
    //            break;
    //        case ".PDF":
    //            ContentType = "application/PDF";
    //            break;
    //    }
    //    Stream fs = null;
    //    BinaryReader br = null;
    //    if (ContentType != String.Empty)
    //    {
    //        //fs = GetStream(fileName, stream);
    //        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    //        fs.Position = 0;
    //        br = new BinaryReader(fs);
    //        bytes = br.ReadBytes((Int32)fs.Length);
    //    }
    //    return bytes;
    //}


    //public bool IsReusable
    //{
    //    get
    //    {
    //        return false;
    //    }
    //}

}