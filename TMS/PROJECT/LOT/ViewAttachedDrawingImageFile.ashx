<%@ WebHandler Language="C#" Class="ViewAttachedDrawingImageFile" %>

using System;
using System.Web;
using System.IO;

public class ViewAttachedDrawingImageFile : IHttpHandler
{    
    System.Data.DataTable dtSubitems = new System.Data.DataTable();

    int srNo = 0;
    string fileName = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        srNo = 0;
        byte[] bytes = null;
        fileName = string.Empty;
        string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        srNo = Convert.ToInt32(context.Request.QueryString["srNo"]);

        if (context.Request.QueryString["srNo"] != null && srNo > 0)
        {
            dtSubitems = SubitemTable.dtSubitems;

            if (dtSubitems != null && dtSubitems.Rows.Count > 0)
            {
                if (fileType == "DRAWING1")
                {
                    bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT1_BTYTES"];
                    fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT1_NAME"]);
                }
                else if (fileType == "DRAWING2")
                {
                    bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT2_BTYTES"];
                    fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT2_NAME"]);
                }
                else if (fileType == "DRAWING3")
                {
                    bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT3_BTYTES"];
                    fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT3_NAME"]);
                }
                else if (fileType == "DRAWING4")
                {
                    bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT4_BTYTES"];
                    fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT4_NAME"]);
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