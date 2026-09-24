<%@ WebHandler Language="C#" Class="ViewImageFile" %>

using System;
using System.Web;
using System.Data;

public class ViewImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        if (context.Request.QueryString["travelStatementID"] != null)
        {
            dsFiles = objTourAndTravels.GetAttachedFiles(Convert.ToInt32(context.Request.QueryString["travelStatementID"]));
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsFiles.Tables[0].Rows[0];

                //if (fileType == "ATTACHMENT1")
                //{
                //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_ONE_DOC"];
                //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_ONE_NAME"]);
                //}
                //else if (fileType == "ATTACHMENT2")
                //{
                //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_TWO_DOC"];
                //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_TWO_NAME"]);
                //}
                //else if (fileType == "ATTACHMENT3")
                //{
                //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_THREE_DOC"];
                //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_THREE_NAME"]);
                //}

                if (fileType == "ATTACHMENT1")
                {
                    bytes = (byte[])dr["VISIT_RPT_SUMMARY_ONE_DOC"];
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_ONE_NAME"]);
                }
                else if (fileType == "ATTACHMENT2")
                {
                    bytes = (byte[])dr["VISIT_RPT_SUMMARY_TWO_DOC"];
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"]);
                }
                else if (fileType == "ATTACHMENT3")
                {
                    bytes = (byte[])dr["VISIT_RPT_SUMMARY_THREE_DOC"];
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"]);
                }

                else if (fileType == "ADD_ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT1_NAME"]);
                    bytes = (byte[])dr["ATTACHMENT1_DOC"];
                }
                else if (fileType == "ADD_ATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT2_NAME"]);
                    bytes = (byte[])dr["ATTACHMENT2_DOC"];
                }
                else if (fileType == "ADD_ATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT3_NAME"]);
                    bytes = (byte[])dr["ATTACHMENT3_DOC"];
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