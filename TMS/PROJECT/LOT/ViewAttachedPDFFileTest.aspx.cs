using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAttachedPDFFileTest : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.Project objProject = new BAL.Project();
    int LOTTFSubitemID = 0;
    string fileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["li"] != null)
                {                    
                    byte[] bytes = null;                    
                    List<byte[]> b = new List<byte[]>();
                    dsFiles = objProject.GetDetailsBySP("sp_get_lot_subitems_doc_bytes");
                    
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsFiles.Tables[0].Rows)
                        {
                            bytes = (byte[])dr["DOC_BYTES"];
                            b.Add(bytes);

                            //count++;
                            //if (count == 1)
                            //{
                            //    bytes = (byte[])dr["DOC_BYTES"];
                            //}
                            //else
                            //{
                            //    bytes1 = null;
                            //    bytes1 = (byte[])dr["DOC_BYTES"];
                            //    bytes = CombileBytes.Combine(bytes, bytes1);
                            //}
                        }

                        byte[] allBytes = concatAndAddContent(b);



                        //if (fileType == "DRAWING1")
                        //{
                        //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                        //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                        //}

                        if (allBytes != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", allBytes.Length.ToString());
                            Response.BinaryWrite(allBytes);
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



    public byte[] concatAndAddContent(List<byte[]> pdfByteContent)
    {

        using (var ms = new MemoryStream())
        {
            using (var doc = new Document())
            {
                using (var copy = new PdfSmartCopy(doc, ms))
                {
                    doc.Open();

                    //Loop through each byte array
                    foreach (var p in pdfByteContent)
                    {

                        //Create a PdfReader bound to that byte array
                        using (var reader = new PdfReader(p))
                        {

                            //Add the entire document instead of page-by-page
                            copy.AddDocument(reader);
                        }
                    }

                    doc.Close();
                }
            }

            //Return just before disposing
            //return ms.ToArray();

            byte[] allBytes = ms.GetBuffer();
            ms.Flush();
            ms.Dispose();
            return allBytes;
        }
    }
    
}
