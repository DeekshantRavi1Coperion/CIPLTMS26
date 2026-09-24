using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Net.Mime;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using System.Xml;
using iTextSharp.tool.xml.css;
using System.Data;


public class BOMHtmlForPDF
{
    private string _unitName = "";
    private string _type = "";
    private string _mrNo = "";
    private string _mrDate = "";
    private string _status = "";
    private string _bomNo = "";
    private string _bomDate = "";
    private string _jobNo = "";
    private string _deliveryRequiredBy = "";
    private string _acceptableVendor1 = "";
    private string _acceptableVendor2 = "";
    private string _acceptableVendor3 = "";
    private string _acceptableVendor4 = "";
    private string _acceptableVendor5 = "";
    private string _revisionNumber = "";
    private string _budgetedCost = "";
    private string _estimatedCost = "";
    private string _costRelatedRemarks = "";
    private string _pivotGroup = "";
    private string _responsibleForBom = "";
    private int _isTcRequired = 0;
    
    private int _createdByFid = 0;
    private string _createdBy = "";
    private string _createdOn = "";
    private string _createdRemarks = "";
    
    private int _approvedByFid = 0;
    private string _approvedBy = "";
    private string _approvedOn = "";
    private string _approvedRemarks = "";
    private int _amendmentCount = 0;
    private int _amendmentByFid = 0;
    private string _amendmentBy = "";
    private string _amendmentOn = "";
    private string _amendmentRemarks = "";
    private int _amendedByFid = 0;
    private string _amendedBy = "";
    private string _amendedOn = "";
    private string _amendedRemarks = "";
    private int _amendedApprovedByFid = 0;
    private string _amendedApprovedBy = "";
    private string _amendedApprovedOn = "";
    private string _amendedApprovedRemarks = "";

    private int    _cancelledByFid = 0;
    private string _cancelledBy = "";
    private string _cancelledOn = "";
    private string _cancelledRemarks = "";

    private string _srNo = "";
    private string _productCode = "";
    private string _productDescription = "";
    private string _additionalDescription = "";
    private string _UOM = "";
    private string _quantity = "0.00";




    public string GetHtmlForPDF(DataTable dtBOMHeader, DataTable dtBOMLine)
    {
        try
        {
            DataRow dr0 = dtBOMHeader.Rows[0];


            if (dr0["UNIT_NAME"] != DBNull.Value)
                _unitName = Convert.ToString(dr0["UNIT_NAME"]);

            if (dr0["TYPE"] != DBNull.Value)
                _type = Convert.ToString(dr0["TYPE"]);

            if (dr0["MR_NO"] != DBNull.Value)
                _mrNo = Convert.ToString(dr0["MR_NO"]);

            if (dr0["MR_DATE"] != DBNull.Value)
                _mrDate = Convert.ToString(dr0["MR_DATE"]);

            if (dr0["STATUS"] != DBNull.Value)
                _status = Convert.ToString(dr0["STATUS"]);

            if (dr0["BOM_NO"] != DBNull.Value)
                _bomNo = Convert.ToString(dr0["BOM_NO"]);

            if (dr0["BOM_DATE"] != DBNull.Value)
                _bomDate = Convert.ToString(dr0["BOM_DATE"]);

            if (dr0["JOB_NO"] != DBNull.Value)
                _jobNo = Convert.ToString(dr0["JOB_NO"]);

            if (dr0["DELIVERY_REQUIRED_BY"] != DBNull.Value)
                _deliveryRequiredBy = Convert.ToString(dr0["DELIVERY_REQUIRED_BY"]);

            if (dr0["ACCEPTABLE_VENDOR1"] != DBNull.Value)
                _acceptableVendor1 = Convert.ToString(dr0["ACCEPTABLE_VENDOR1"]);

            if (dr0["ACCEPTABLE_VENDOR2"] != DBNull.Value)
                _acceptableVendor2 = Convert.ToString(dr0["ACCEPTABLE_VENDOR2"]);

            if (dr0["ACCEPTABLE_VENDOR3"] != DBNull.Value)
                _acceptableVendor3 = Convert.ToString(dr0["ACCEPTABLE_VENDOR3"]);

            if (dr0["ACCEPTABLE_VENDOR4"] != DBNull.Value)
                _acceptableVendor4 = Convert.ToString(dr0["ACCEPTABLE_VENDOR4"]);

            if (dr0["ACCEPTABLE_VENDOR5"] != DBNull.Value)
                _acceptableVendor5 = Convert.ToString(dr0["ACCEPTABLE_VENDOR5"]);

            if (dr0["REVISION_NUMBER"] != DBNull.Value)
                _revisionNumber = Convert.ToString(dr0["REVISION_NUMBER"]);

            if (dr0["BUDGETED_COST"] != DBNull.Value)
                _budgetedCost = Convert.ToString(dr0["BUDGETED_COST"]);

            if (dr0["ESTIMATED_COST"] != DBNull.Value)
                _estimatedCost = Convert.ToString(dr0["ESTIMATED_COST"]);

            if (dr0["COST_RELATED_REMARKS"] != DBNull.Value)
                _costRelatedRemarks = Convert.ToString(dr0["COST_RELATED_REMARKS"]);

            if (dr0["PIVOT_GROUP"] != DBNull.Value)
                _pivotGroup = Convert.ToString(dr0["PIVOT_GROUP"]);

            if (dr0["RESPONSIBLE_FOR_BOM"] != DBNull.Value)
                _responsibleForBom = Convert.ToString(dr0["RESPONSIBLE_FOR_BOM"]);

            if (dr0["IS_TC_REQUIRED"] != DBNull.Value)
                _isTcRequired = Convert.ToInt32(dr0["IS_TC_REQUIRED"]);

            if (dr0["CREATED_BY_FID"] != DBNull.Value)
                _createdByFid = Convert.ToInt32(dr0["CREATED_BY_FID"]);

            if (dr0["CREATED_BY"] != DBNull.Value)
                _createdBy = Convert.ToString(dr0["CREATED_BY"]);

            if (dr0["CREATED_ON"] != DBNull.Value)
                _createdOn = Convert.ToString(dr0["CREATED_ON"]);

            if (dr0["CREATED_REMARKS"] != DBNull.Value)
                _createdRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);

            if (dr0["APPROVED_BY_FID"] != DBNull.Value)
                _approvedByFid = Convert.ToInt32(dr0["APPROVED_BY_FID"]);

            if (dr0["APPROVED_BY"] != DBNull.Value)
                _approvedBy = Convert.ToString(dr0["APPROVED_BY"]);

            if (dr0["APPROVED_ON"] != DBNull.Value)
                _approvedOn = Convert.ToString(dr0["APPROVED_ON"]);

            if (dr0["APPROVED_REMARKS"] != DBNull.Value)
                _approvedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]);

            if (dr0["AMENDMENT_COUNT"] != DBNull.Value)
                _amendmentCount = Convert.ToInt32(dr0["AMENDMENT_COUNT"]);

            if (dr0["AMENDMENT_BY_FID"] != DBNull.Value)
                _amendmentByFid = Convert.ToInt32(dr0["AMENDMENT_BY_FID"]);

            if (dr0["AMENDMENT_BY"] != DBNull.Value)
                _amendmentBy = Convert.ToString(dr0["AMENDMENT_BY"]);

            if (dr0["AMENDMENT_ON"] != DBNull.Value)
                _amendmentOn = Convert.ToString(dr0["AMENDMENT_ON"]);

            if (dr0["AMENDMENT_REMARKS"] != DBNull.Value)
                _amendmentRemarks = Convert.ToString(dr0["AMENDMENT_REMARKS"]);

            if (dr0["AMENDED_BY_FID"] != DBNull.Value)
                _amendedByFid = Convert.ToInt32(dr0["AMENDED_BY_FID"]);

            if (dr0["AMENDED_BY"] != DBNull.Value)
                _amendedBy = Convert.ToString(dr0["AMENDED_BY"]);

            if (dr0["AMENDED_ON"] != DBNull.Value)
                _amendedOn = Convert.ToString(dr0["AMENDED_ON"]);

            if (dr0["AMENDED_REMARKS"] != DBNull.Value)
                _amendedRemarks = Convert.ToString(dr0["AMENDED_REMARKS"]);

            if (dr0["AMENDED_APPROVED_BY_FID"] != DBNull.Value)
                _amendedApprovedByFid = Convert.ToInt32(dr0["AMENDED_APPROVED_BY_FID"]);

            if (dr0["AMENDED_APPROVED_BY"] != DBNull.Value)
                _amendedApprovedBy = Convert.ToString(dr0["AMENDED_APPROVED_BY"]);

            if (dr0["AMENDED_APPROVED_ON"] != DBNull.Value)
                _amendedApprovedOn = Convert.ToString(dr0["AMENDED_APPROVED_ON"]);

            if (dr0["AMENDED_APPROVED_REMRKS"] != DBNull.Value)
                _amendedApprovedRemarks = Convert.ToString(dr0["AMENDED_APPROVED_REMRKS"]);





            if (dr0["CANCELLED_BY_FID"] != DBNull.Value)
                _cancelledByFid = Convert.ToInt32(dr0["CANCELLED_BY_FID"]);

            if (dr0["CANCELLED_BY"] != DBNull.Value)
                _cancelledBy = Convert.ToString(dr0["CANCELLED_BY"]);

            if (dr0["CANCELLED_ON"] != DBNull.Value)
                _cancelledOn = Convert.ToString(dr0["CANCELLED_ON"]);

            if (dr0["CANCELLED_REMARKS"] != DBNull.Value)
                _cancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();


            sb.Append("<h2 class='headerStyle'>Material Requisition</h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Unit Name:</td>\n");
            sb.Append("<td class='td2header'>{#UnitName#}</td>\n");
            sb.Append("<td class='td1header'>Type:</td>\n");
            sb.Append("<td class='td2header'>{#Type#}</td>\n");
            sb.Append("</tr>\n");
           
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Mr No:</td>\n");
            sb.Append("<td class='td2header'><b>{#MrNo#}</b></td>\n");
            sb.Append("<td class='td1header'>Mr Date:</td>\n");
            sb.Append("<td class='td2header'><b>{#MrDate#}</b></td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Status:</td>\n");
            sb.Append("<td class='td2header'><b>{#Status#}</b></td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Bom No:</td>\n");
            sb.Append("<td class='td2header'>{#BomNo#}</td>\n");
            sb.Append("<td class='td1header'>Bom Date:</td>\n");
            sb.Append("<td class='td2header'>{#BomDate#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Job No:</td>\n");
            sb.Append("<td class='td2header'>{#JobNo#}</td>\n");
            sb.Append("<td class='td1header'>Delivery Required By:</td>\n");
            sb.Append("<td class='td2header'>{#DeliveryRequiredBy#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Acceptable Vendor1:</td>\n");
            sb.Append("<td class='td2header'>{#AcceptableVendor1#}</td>\n");
            sb.Append("<td class='td1header'>Acceptable Vendor2:</td>\n");
            sb.Append("<td class='td2header'>{#AcceptableVendor2#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Acceptable Vendor3:</td>\n");
            sb.Append("<td class='td2header'>{#AcceptableVendor3#}</td>\n");
            sb.Append("<td class='td1header'>Acceptable Vendor4:</td>\n");
            sb.Append("<td class='td2header'>{#AcceptableVendor4#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Acceptable Vendor5:</td>\n");
            sb.Append("<td class='td2header'>{#AcceptableVendor5#}</td>\n");
            sb.Append("<td class='td1header'>Revision Number:</td>\n");
            sb.Append("<td class='td2header'>{#RevisionNumber#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Budgeted Cost:</td>\n");
            sb.Append("<td class='td2header'>{#BudgetedCost#}</td>\n");
            sb.Append("<td class='td1header'>Estimated Cost:</td>\n");
            sb.Append("<td class='td2header'>{#EstimatedCost#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Pivot Group:</td>\n");
            sb.Append("<td class='td2header'>{#PivotGroup#}</td>\n");
            sb.Append("<td class='td1header'>Responsible For PO:</td>\n");
            sb.Append("<td class='td2header'>{#ResponsibleForBom#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Is Tc Required:</td>\n");
            sb.Append("<td class='td2header'>{#IsTcRequired#}</td>\n");
            sb.Append("<td class='td1header'>&nbsp;</td>\n");
            sb.Append("<td class='td2header'>&nbsp;</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Cost Related Remarks:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#CostRelatedRemarks#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");

            sb.Append("<hr />\n");

            if (dtBOMLine.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Products</h3>\n");
                sb.Append("<table class='tblsuitems'>\n");
                sb.Append("<tr class='trsubitems'>\n");
                sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                sb.Append("<th class='tddesc'>Product Code</th>\n");
                sb.Append("<th class='tddesc'>Product Description</th>\n");
                sb.Append("<th class='tddesc'>Additional Description</th>\n");
                sb.Append("<th class='tdtag'>UOM</th>\n");
                sb.Append("<th class='tdquantity'>Quantity</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow drL in dtBOMLine.Rows)
                {
                    _srNo = "";
                    _productCode = "";
                    _productDescription = "";
                    _additionalDescription = "";
                    _UOM = "";
                    _quantity = "0.00";

                    if (drL["SR_NO"] != DBNull.Value) _srNo = Convert.ToString(drL["SR_NO"]);
                    if (drL["PRODUCT_CODE"] != DBNull.Value) _productCode = Convert.ToString(drL["PRODUCT_CODE"]);
                    if (drL["PRODUCT_DESCRIPTION"] != DBNull.Value) _productDescription = Convert.ToString(drL["PRODUCT_DESCRIPTION"]);
                    if (drL["ADDITIONAL_DESCRIPTION"] != DBNull.Value) _additionalDescription = Convert.ToString(drL["ADDITIONAL_DESCRIPTION"]);
                    if (drL["UOM"] != DBNull.Value) _UOM = Convert.ToString(drL["UOM"]);
                    if (drL["POSTED_QUANTITY"] != DBNull.Value) _quantity = Convert.ToString(drL["POSTED_QUANTITY"]);


                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SrNo#}</td>\n");
                    sb.Append("<td class='tddesc'>{#ProductCode#}</td>\n");
                    sb.Append("<td class='tddesc'>{#ProductDescription#}</td>\n");
                    sb.Append("<td class='tddesc'>{#AdditionalDescription#}</td>\n");
                    sb.Append("<td class='tdtag'>{#UOM#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#Quantity#}</td>\n");
                    sb.Append("</tr>\n");


                    sb.Replace("{#SrNo#}", _srNo);
                    sb.Replace("{#ProductCode#}", _productCode);
                    sb.Replace("{#ProductDescription#}", _productDescription);
                    sb.Replace("{#AdditionalDescription#}", _additionalDescription);
                    sb.Replace("{#UOM#}", _UOM);
                    sb.Replace("{#Quantity#}", _quantity);

                }

                sb.Append("</table>\n");
            }


            if (_createdByFid > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<h3 class='header2'>Signatories</h3>\n");
                sb.Append("<hr />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Created</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Created Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#CreatedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Created By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#CreatedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Created On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#CreatedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");
                sb.Append("<hr class='hrsignatories' />\n");


                sb.Replace("{#CreatedRemarks#}", _createdRemarks);
                sb.Replace("{#CreatedBy#}", _createdBy);
                sb.Replace("{#CreatedOn#}", _createdOn);

                if (_amendmentByFid > 0 && _amendmentCount > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Amendment</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Amendment Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#AmendmentRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Amendment By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#AmendmentBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Amendment On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#AmendmentOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");


                    sb.Replace("{#AmendmentRemarks#}", _amendmentRemarks);
                    sb.Replace("{#AmendmentBy#}", _amendmentBy);
                    sb.Replace("{#AmendmentOn#}", _amendmentOn);
                }

                if (_amendedByFid > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Amended</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Amended Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#AmendedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Amended By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#AmendedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Amended On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#AmendedOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#AmendedRemarks#}", _amendedRemarks);
                    sb.Replace("{#AmendedBy#}", _amendedBy);
                    sb.Replace("{#AmendedOn#}", _amendedOn);

                }

                if (_approvedByFid > 0 || _amendedApprovedByFid > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>{#ApprovedOrAmendedApprovedLabel#}</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>{#ApprovedOrAmendedApprovedRemarksLabel#}:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#ApprovedOrAmendedApprovedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>{#ApprovedOrAmendedApprovedByLabel#}:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#ApprovedOrAmendedApprovedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>{#ApprovedOrAmendedApprovedOnLabel#}:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#ApprovedOrAmendedApprovedOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");


                    if (_approvedByFid > 0)
                    {
                        sb.Replace("{#ApprovedOrAmendedApprovedLabel#}", "Approved");

                        sb.Replace("{#ApprovedOrAmendedApprovedRemarksLabel#}", "Approved Remarks");
                        sb.Replace("{#ApprovedOrAmendedApprovedRemarks#}", _approvedRemarks);

                        sb.Replace("{#ApprovedOrAmendedApprovedByLabel#}", "Approved By");
                        sb.Replace("{#ApprovedOrAmendedApprovedBy#}", _approvedBy);

                        sb.Replace("{#ApprovedOrAmendedApprovedOnLabel#}", "Approved On");
                        sb.Replace("{#ApprovedOrAmendedApprovedOn#}", _approvedOn);

                    }
                    else if (_amendedApprovedByFid > 0)
                    {
                        sb.Replace("{#ApprovedOrAmendedApprovedLabel#}", "Amended Approved");

                        sb.Replace("{#ApprovedOrAmendedApprovedRemarksLabel#}", "Amended Approved Remarks");
                        sb.Replace("{#ApprovedOrAmendedApprovedRemarks#}", _amendedApprovedRemarks);

                        sb.Replace("{#ApprovedOrAmendedApprovedByLabel#}", "Amended Approved By");
                        sb.Replace("{#ApprovedOrAmendedApprovedBy#}", _amendedApprovedBy);

                        sb.Replace("{#ApprovedOrAmendedApprovedOnLabel#}", "Amended Approved On");
                        sb.Replace("{#ApprovedOrAmendedApprovedOn#}", _amendedApprovedOn);
                    }

                }


                if (_cancelledByFid > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Cancelled</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Cancelled Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#CancelledRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Cancelled By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#CancelledBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Cancelled On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#CancelledOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#CancelledRemarks#}", _cancelledRemarks);
                    sb.Replace("{#CancelledBy#}", _cancelledBy);
                    sb.Replace("{#CancelledOn#}", _cancelledOn);

                }

            }



            sb.Replace("{#UnitName#}", _unitName);
            sb.Replace("{#Type#}", _type);
            sb.Replace("{#MrNo#}", _mrNo);
            sb.Replace("{#MrDate#}", _mrDate);
            sb.Replace("{#Status#}", _status);
            sb.Replace("{#BomNo#}", _bomNo);
            sb.Replace("{#BomDate#}", _bomDate);
            sb.Replace("{#JobNo#}", _jobNo);
            sb.Replace("{#DeliveryRequiredBy#}", _deliveryRequiredBy);
            sb.Replace("{#AcceptableVendor1#}", _acceptableVendor1);
            sb.Replace("{#AcceptableVendor2#}", _acceptableVendor2);
            sb.Replace("{#AcceptableVendor3#}", _acceptableVendor3);
            sb.Replace("{#AcceptableVendor4#}", _acceptableVendor4);
            sb.Replace("{#AcceptableVendor5#}", _acceptableVendor5);
            sb.Replace("{#RevisionNumber#}", _revisionNumber);
            sb.Replace("{#BudgetedCost#}", _budgetedCost);
            sb.Replace("{#EstimatedCost#}", _estimatedCost);
            sb.Replace("{#PivotGroup#}", _pivotGroup);
            sb.Replace("{#ResponsibleForBom#}", _responsibleForBom);

            if (_isTcRequired > 0)
                sb.Replace("{#IsTcRequired#}", "Yes");
            else
                sb.Replace("{#IsTcRequired#}", "No");

            sb.Replace("{#CostRelatedRemarks#}", _costRelatedRemarks);


            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}