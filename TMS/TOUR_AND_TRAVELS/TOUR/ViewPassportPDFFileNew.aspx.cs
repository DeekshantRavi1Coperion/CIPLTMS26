using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewPassportPDFFileNew : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["emprecordid"] != null)
                {
                    byte[] bytes = null;
                    string fileName = string.Empty;
                    dsFiles = objTourAndTravels.GetPassportFile(Convert.ToInt32(Request.QueryString["emprecordid"]));
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        bytes = (byte[])dsFiles.Tables[0].Rows[0]["PASSPORT_COPY_DOC"];
                        fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["PASSPORT_COPY_NAME"]);

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
