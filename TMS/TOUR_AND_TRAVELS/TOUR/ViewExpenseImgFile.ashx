<%@ WebHandler Language="C#" Class="ViewExpenseImgFile" %>

using System;
using System.Web;

public class ViewExpenseImgFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        int recordID = 0;
        int fileTypeID = Convert.ToInt32(context.Request.QueryString["filetypeid"]);

        if (context.Request.QueryString["recordid"] != null)
        {
            recordID = Convert.ToInt32(context.Request.QueryString["recordid"]);

            dsFiles = objTourAndTravels.GetExpenseFile(recordID, fileTypeID);
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileTypeID == (int)TandTAllStatus.EnumTourExpenseFiles.File1)
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_FILE_NAME"]);
                }
                else if (fileTypeID == (int)TandTAllStatus.EnumTourExpenseFiles.File2)
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT2_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT2_FILE_NAME"]);
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

}