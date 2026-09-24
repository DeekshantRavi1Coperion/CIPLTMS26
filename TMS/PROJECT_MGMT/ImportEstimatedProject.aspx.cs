using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.OleDb;

public partial class PROJECT_MGMT_ImportEstimatedProject : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Project objProject = new BAL.Project();

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
               //
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnGetEstimatedFile_Click(object sender, EventArgs e)
    {
        GetEstimatedFile();
    }

    protected void ImportEstimatedFile_Click(object sender, EventArgs e)
    {
        if (gvEstimatedProject.Rows.Count > 0)
        {
            ImportEstimatedProjectFile();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }        
    }


    #endregion


    #region METHODS[==================]

    private void GetEstimatedFile()
    {
        try
        {
            string fileName = string.Empty;

            if (fileUploadEstimatedProject.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadEstimatedProject.PostedFile.FileName))
                {
                    
                    #region CREATE_TABLE

                    DataTable dt = new DataTable();
                    dt.Columns.Add("DESCRIPTION", typeof(string));
                    dt.Columns.Add("SCOPE", typeof(string));
                    dt.Columns.Add("QUANTITY", typeof(string));
                    dt.Columns.Add("UNIT_RATE_INR", typeof(string));
                    dt.Columns.Add("PRICE_INR", typeof(string));
                    dt.Columns.Add("P_AND_F", typeof(string));
                    dt.Columns.Add("PFD", typeof(string));
                    dt.Columns.Add("ED", typeof(string));
                    dt.Columns.Add("TOTAL_ED", typeof(string));
                    dt.Columns.Add("ST", typeof(string));
                    dt.Columns.Add("ST_AMT", typeof(string));
                    dt.Columns.Add("TOTAL_COST_INCL_ED_ST", typeof(string));
                    dt.Columns.Add("IMPORTED_EX_W_RATE_EURO", typeof(string));
                    dt.Columns.Add("IMPORTED_EX_W_PRICE_EURO", typeof(string));
                    dt.Columns.Add("FOB_PRICE_EURO", typeof(string));
                    dt.Columns.Add("EQUIV_RUPEE_PRICE", typeof(string));
                    dt.Columns.Add("FULL_DUTY", typeof(string));
                    dt.Columns.Add("TOTAL_IMPORT_COST", typeof(string));
                    dt.Columns.Add("TOTAL_COST_INR", typeof(string));
                    dt.Columns.Add("TOTAL_COST_EURO", typeof(string));
                    dt.Columns.Add("CATEGORY", typeof(string));
                    dt.Columns.Add("CODES", typeof(string));

                    #endregion

                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadEstimatedProject.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();

                            int i = 0;
                            foreach (string cell in row.Split(','))
                            {
                                string dd = cell;

                                if (i <= (dt.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dd))
                                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                                    else
                                        dt.Rows[dt.Rows.Count - 1][i] = string.Empty;

                                    i++;
                                }
                            }
                        }
                    }

                    dt.Rows.RemoveAt(0);
                    gvEstimatedProject.DataSource = dt;
                    gvEstimatedProject.DataBind();



                    //if (!string.IsNullOrEmpty(txtProjectNo.Text))
                    //{
                    //    GetFileByProjectNo(dt);
                    //}
                    //else
                    //{

                    //}

                    lblRecords.Text = "Records[" + dt.Rows.Count + "]";
                }
                else
                {
                    fileName = string.Empty;
                }
            }
            else
            {
                fileName = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private void GetFileByProjectNo(DataTable dt)
    {
        DataTable dtnew = new DataTable();

        dtnew.Columns.Add("PROJECT_NO", typeof(string));
        dtnew.Columns.Add("DESCRIPTION", typeof(string));
        dtnew.Columns.Add("SCOPE", typeof(string));
        dtnew.Columns.Add("QUANTITY", typeof(string));
        dtnew.Columns.Add("UNIT_RATE_INR", typeof(string));
        dtnew.Columns.Add("PRICE_INR", typeof(string));
        dtnew.Columns.Add("P_AND_F", typeof(string));
        dtnew.Columns.Add("PFD", typeof(string));
        dtnew.Columns.Add("ED", typeof(string));
        dtnew.Columns.Add("TOTAL_ED", typeof(string));
        dtnew.Columns.Add("ST", typeof(string));
        dtnew.Columns.Add("ST_AMT", typeof(string));
        dtnew.Columns.Add("TOTAL_COST_INCL_ED_ST", typeof(string));
        dtnew.Columns.Add("IMPORTED_EX_W_RATE_EURO", typeof(string));
        dtnew.Columns.Add("IMPORTED_EX_W_PRICE_EURO", typeof(string));
        dtnew.Columns.Add("FOB_PRICE_EURO", typeof(string));
        dtnew.Columns.Add("EQUIV_RUPEE_PRICE", typeof(string));
        dtnew.Columns.Add("FULL_DUTY", typeof(string));
        dtnew.Columns.Add("TOTAL_IMPORT_COST", typeof(string));
        dtnew.Columns.Add("TOTAL_COST_INR", typeof(string));
        dtnew.Columns.Add("TOTAL_COST_EURO", typeof(string));
        dtnew.Columns.Add("CATEGORY", typeof(string));
        dtnew.Columns.Add("CODES", typeof(string));

        foreach (DataRow dr in dt.Select("PROJECT_NO='" + txtProjectNo.Text + "'"))
        {
            dtnew.Rows.Add();

            dtnew.Rows[0]["PROJECT_NO"] = dr["PROJECT_NO"];
            dtnew.Rows[0]["DESCRIPTION"] = dr["DESCRIPTION"];
            dtnew.Rows[0]["SCOPE"] = dr["SCOPE"];
            dtnew.Rows[0]["QUANTITY"] = dr["QUANTITY"];
            dtnew.Rows[0]["UNIT_RATE_INR"] = dr["UNIT_RATE_INR"];
            dtnew.Rows[0]["PRICE_INR"] = dr["PRICE_INR"];
            dtnew.Rows[0]["P_AND_F"] = dr["P_AND_F"];
            dtnew.Rows[0]["PFD"] = dr["PFD"];
            dtnew.Rows[0]["ED"] = dr["ED"];
            dtnew.Rows[0]["TOTAL_ED"] = dr["TOTAL_ED"];
            dtnew.Rows[0]["ST"] = dr["ST"];
            dtnew.Rows[0]["ST_AMT"] = dr["ST_AMT"];
            dtnew.Rows[0]["TOTAL_COST_INCL_ED_ST"] = dr["TOTAL_COST_INCL_ED_ST"];
            dtnew.Rows[0]["IMPORTED_EX_W_RATE_EURO"] = dr["IMPORTED_EX_W_RATE_EURO"];
            dtnew.Rows[0]["IMPORTED_EX_W_PRICE_EURO"] = dr["IMPORTED_EX_W_PRICE_EURO"];
            dtnew.Rows[0]["FOB_PRICE_EURO"] = dr["FOB_PRICE_EURO"];
            dtnew.Rows[0]["EQUIV_RUPEE_PRICE"] = dr["EQUIV_RUPEE_PRICE"];
            dtnew.Rows[0]["FULL_DUTY"] = dr["FULL_DUTY"];
            dtnew.Rows[0]["TOTAL_IMPORT_COST"] = dr["TOTAL_IMPORT_COST"];
            dtnew.Rows[0]["TOTAL_COST_INR"] = dr["TOTAL_COST_INR"];
            dtnew.Rows[0]["TOTAL_COST_EURO"] = dr["TOTAL_COST_EURO"];
            dtnew.Rows[0]["CATEGORY"] = dr["CATEGORY"];
            dtnew.Rows[0]["CODES"] = dr["CODES"];

        }

        gvEstimatedProject.DataSource = dtnew;
        gvEstimatedProject.DataBind();
    }

    private void ImportEstimatedProjectFile()
    {
        try
        {
            string projectNo = string.Empty;
            string description = string.Empty;
            string scope = string.Empty;
            double quantity = 0;
            double unitRateINR = 0;
            double priceINR = 0;
            double pAndF = 0;
            double pfd = 0;
            double ed = 0;
            double totalED = 0;
            double st = 0;
            double stAmt = 0;
            double totalCostInclEdSt = 0;
            double importedExWRateEuro = 0;
            double importedExWPriceEuro = 0;
            double fobPriceEuro = 0;
            double equivRupeePrice = 0;
            double fullDuty = 0;
            double totalImportCost = 0;
            double totalCostINR = 0;
            double totalCostEURO = 0;
            string category = string.Empty;
            string codes = string.Empty;

            int value = 0;

            if (!string.IsNullOrEmpty(txtProjectNo.Text))
                projectNo = Convert.ToString(txtProjectNo.Text);
            else
                projectNo = string.Empty;

            int count = 0;
            foreach (GridViewRow gr in gvEstimatedProject.Rows)
            {
                Label lblDescription = (Label)gr.FindControl("lblDescription");
                Label lblScope = (Label)gr.FindControl("lblScope");
                Label lblQuantity = (Label)gr.FindControl("lblQuantity");
                Label lblUnitRateINR = (Label)gr.FindControl("lblUnitRateINR");
                Label lblPriceINR = (Label)gr.FindControl("lblPriceINR");
                Label lblPAndF = (Label)gr.FindControl("lblPAndF");
                Label lblPFD = (Label)gr.FindControl("lblPFD");
                Label lblED = (Label)gr.FindControl("lblED");
                Label lblTotalED = (Label)gr.FindControl("lblTotalED");
                Label lblST = (Label)gr.FindControl("lblST");
                Label lblSTAmt = (Label)gr.FindControl("lblSTAmt");
                Label lblTotalCostInclEDST = (Label)gr.FindControl("lblTotalCostInclEDST");
                Label lblImportedEXWRateEURO = (Label)gr.FindControl("lblImportedEXWRateEURO");
                Label lblImportedEXWPriceEURO = (Label)gr.FindControl("lblImportedEXWPriceEURO");
                Label lblFOBPriceEURO = (Label)gr.FindControl("lblFOBPriceEURO");
                Label lblEquivRupeePrice = (Label)gr.FindControl("lblEquivRupeePrice");
                Label lblFullDuty = (Label)gr.FindControl("lblFullDuty");
                Label lblTotalImportCost = (Label)gr.FindControl("lblTotalImportCost");
                Label lblTotalCostINR = (Label)gr.FindControl("lblTotalCostINR");
                Label lblTotalCostEURO = (Label)gr.FindControl("lblTotalCostEURO");
                Label lblCategory = (Label)gr.FindControl("lblCategory");
                Label lblCodes = (Label)gr.FindControl("lblCodes");


                if (!string.IsNullOrEmpty(lblDescription.Text))
                    description = Convert.ToString(lblDescription.Text);
                else
                    description = string.Empty;

                if (!string.IsNullOrEmpty(lblScope.Text))
                    scope = Convert.ToString(lblScope.Text);
                else
                    scope = string.Empty;

                if (!string.IsNullOrEmpty(lblQuantity.Text))
                    quantity = Convert.ToDouble(lblQuantity.Text.TrimEnd('%'));
                else
                    quantity = 0;

                if (!string.IsNullOrEmpty(lblUnitRateINR.Text))
                    unitRateINR = Convert.ToDouble(lblUnitRateINR.Text.TrimEnd('%'));
                else
                    unitRateINR = 0;

                if (!string.IsNullOrEmpty(lblPriceINR.Text))
                    priceINR = Convert.ToDouble(lblPriceINR.Text.TrimEnd('%'));
                else
                    priceINR = 0;

                if (!string.IsNullOrEmpty(lblPAndF.Text))
                    pAndF = Convert.ToDouble(lblPAndF.Text.TrimEnd('%'));
                else
                    pAndF = 0;

                if (!string.IsNullOrEmpty(lblPFD.Text))
                    pfd = Convert.ToDouble(lblPFD.Text.TrimEnd('%'));
                else
                    pfd = 0;

                if (!string.IsNullOrEmpty(lblED.Text))
                    ed = Convert.ToDouble(lblED.Text.TrimEnd('%'));
                else
                    ed = 0;

                if (!string.IsNullOrEmpty(lblTotalED.Text))
                    totalED = Convert.ToDouble(lblTotalED.Text.TrimEnd('%'));
                else
                    totalED = 0;

                if (!string.IsNullOrEmpty(lblST.Text))
                    st = Convert.ToDouble(lblST.Text.TrimEnd('%'));
                else
                    st = 0;

                if (!string.IsNullOrEmpty(lblSTAmt.Text))
                    stAmt = Convert.ToDouble(lblSTAmt.Text.TrimEnd('%'));
                else
                    stAmt = 0;

                if (!string.IsNullOrEmpty(lblTotalCostInclEDST.Text))
                    totalCostInclEdSt = Convert.ToDouble(lblTotalCostInclEDST.Text.TrimEnd('%'));
                else
                    totalCostInclEdSt = 0;

                if (!string.IsNullOrEmpty(lblImportedEXWRateEURO.Text))
                    importedExWRateEuro = Convert.ToDouble(lblImportedEXWRateEURO.Text.TrimEnd('%'));
                else
                    importedExWRateEuro = 0;

                if (!string.IsNullOrEmpty(lblImportedEXWPriceEURO.Text))
                    importedExWPriceEuro = Convert.ToDouble(lblImportedEXWPriceEURO.Text.TrimEnd('%'));
                else
                    importedExWPriceEuro = 0;

                if (!string.IsNullOrEmpty(lblFOBPriceEURO.Text))
                    fobPriceEuro = Convert.ToDouble(lblFOBPriceEURO.Text.TrimEnd('%'));
                else
                    fobPriceEuro = 0;

                if (!string.IsNullOrEmpty(lblEquivRupeePrice.Text))
                    equivRupeePrice = Convert.ToDouble(lblEquivRupeePrice.Text.TrimEnd('%'));
                else
                    equivRupeePrice = 0;

                if (!string.IsNullOrEmpty(lblFullDuty.Text))
                    fullDuty = Convert.ToDouble(lblFullDuty.Text.TrimEnd('%'));
                else
                    fullDuty = 0;

                if (!string.IsNullOrEmpty(lblTotalImportCost.Text))
                    totalImportCost = Convert.ToDouble(lblTotalImportCost.Text.TrimEnd('%'));
                else
                    totalImportCost = 0;

                if (!string.IsNullOrEmpty(lblTotalCostINR.Text))
                    totalCostINR = Convert.ToDouble(lblTotalCostINR.Text.TrimEnd('%'));
                else
                    totalCostINR = 0;

                if (!string.IsNullOrEmpty(lblTotalCostEURO.Text))
                    totalCostEURO = Convert.ToDouble(lblTotalCostEURO.Text.TrimEnd('%'));
                else
                    totalCostEURO = 0;

                if (!string.IsNullOrEmpty(lblCategory.Text))
                    category = Convert.ToString(lblCategory.Text);
                else
                    category = string.Empty;

                if (!string.IsNullOrEmpty(lblCodes.Text))
                    codes = Convert.ToString(lblCodes.Text);
                else
                    codes = string.Empty;


                value = objProject.ImportEstimatedProjectFile(projectNo, description, scope, quantity, unitRateINR,
                                                    priceINR, pAndF, pfd, ed, totalED, st, stAmt, totalCostInclEdSt, importedExWRateEuro,
                                                    importedExWPriceEuro, fobPriceEuro, equivRupeePrice, fullDuty,
                                                    totalImportCost, totalCostINR, totalCostEURO,
                                                    category, codes, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");
                gvEstimatedProject.DataSource = null;
                gvEstimatedProject.DataBind();
                lblRecords.Text = "Records[" + gvEstimatedProject.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SuccessMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
