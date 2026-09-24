using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAttachedPDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["ticketID"] != null)
                {
                    byte[] bytes = null;
                    string fileName = string.Empty;
                    string fileType = Convert.ToString(Request.QueryString["fileType"]);
                    dsFiles = objTicket.GetAttachedFiles(Convert.ToInt32(Request.QueryString["ticketID"]));
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        if (fileType == "ATTACHMENT1")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["DOC_NAME1"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILE_NAME1"]);
                        }
                        else if (fileType == "ATTACHMENT2")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["DOC_NAME2"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILE_NAME2"]);
                        }
                        else if (fileType == "ATTACHMENT3")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["DOC_NAME3"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILE_NAME3"]);
                        }

                        if (fileType == "CLOSINGATTACHMENT1")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["FILEDOC_NAME1"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILECLOSING_NAME1"]);
                        }
                        if (fileType == "CLOSINGATTACHMENT")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["FILEDOC_NAME1"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILECLOSING_NAME1"]);
                        }
                        else if (fileType == "CLOSINGATTACHMENT2")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["FILEDOC_NAME2"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILECLOSING_NAME2"]);
                        }
                        else if (fileType == "CLOSINGATTACHMENT3")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["FILEDOC_NAME3"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILECLOSING_NAME3"]);
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
