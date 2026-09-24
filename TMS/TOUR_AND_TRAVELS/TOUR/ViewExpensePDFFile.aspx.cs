using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TOUR_AND_TRAVELS_TOUR_ViewExpensePDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["recordid"] != null)
                {
                    byte[] bytes = null;
                    string fileName = string.Empty;
                    int fileTypeID = Convert.ToInt32(Request.QueryString["filetypeid"]);
                    int recordID = Convert.ToInt32(Request.QueryString["recordid"]);

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
