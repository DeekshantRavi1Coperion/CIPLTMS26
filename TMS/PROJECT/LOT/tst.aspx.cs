using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PROJECT_LOT_tst : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void imgBtnSiDrawing1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing1.Value = "0";
        hdUploadSiDrawing1.Value = "1";

        pnlSiDrawing1.Visible = false;
        pnlUploadSiDrawing1.Visible = true;
    }

    protected void imgBtnUploadSiDrawing1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing1.Value = "1";
        hdUploadSiDrawing1.Value = "0";

        pnlSiDrawing1.Visible = true;
        pnlUploadSiDrawing1.Visible = false;
    }

    protected void imgBtnSiDrawing2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing2.Value = "0";
        hdUploadSiDrawing2.Value = "1";

        pnlSiDrawing2.Visible = false;
        pnlUploadSiDrawing2.Visible = true;
    }

    protected void imgBtnUploadSiDrawing2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing2.Value = "1";
        hdUploadSiDrawing2.Value = "0";

        pnlSiDrawing2.Visible = true;
        pnlUploadSiDrawing2.Visible = false;
    }


    protected void imgBtnSiDrawing3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing3.Value = "0";
        hdUploadSiDrawing3.Value = "1";

        pnlSiDrawing3.Visible = false;
        pnlUploadSiDrawing3.Visible = true;
    }

    protected void imgBtnUploadSiDrawing3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing3.Value = "1";
        hdUploadSiDrawing3.Value = "0";

        pnlSiDrawing3.Visible = true;
        pnlUploadSiDrawing3.Visible = false;
    }



    protected void imgBtnSiDrawing4_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing4.Value = "0";
        hdUploadSiDrawing4.Value = "1";

        pnlSiDrawing4.Visible = false;
        pnlUploadSiDrawing4.Visible = true;
    }

    protected void imgBtnUploadSiDrawing4_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing4.Value = "1";
        hdUploadSiDrawing4.Value = "0";

        pnlSiDrawing4.Visible = true;
        pnlUploadSiDrawing4.Visible = false;
    }
}