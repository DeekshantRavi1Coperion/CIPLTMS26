using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewPDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["travelStatementID"] != null)
                {
                    byte[] bytes = null;
                    string fileName = string.Empty;
                    string fileType = Convert.ToString(Request.QueryString["fileType"]);
                    dsFiles = objTourAndTravels.GetAttachedFiles(Convert.ToInt32(Request.QueryString["travelStatementID"]));
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        if (fileType == "ATTACHMENT1")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_ONE_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_ONE_NAME"]);
                        }
                        else if (fileType == "ATTACHMENT2")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_TWO_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_TWO_NAME"]);
                        }
                        else if (fileType == "ATTACHMENT3")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_THREE_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_THREE_NAME"]);
                        }

                        if (bytes != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", bytes.Length.ToString());
                            Response.BinaryWrite(bytes);
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
