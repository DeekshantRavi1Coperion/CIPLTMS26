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


public class HRTHtmlForPDF
{
    private int _ticketTypeId;

    private string _ticketNumber;
    private string _status;
    private string _ticketType;
    private string _ticketSubtype;
    private string _description;
    private string _allocatedTo;

    private int _createdById;
    private string _createdBy;
    private string _createdOn;
    private string _createdRemarks;

    private int _allocatedById;
    private string _allocatedBy;
    private string _allocatedOn;
    private string _allocatedRemarks;

    private int _closedById;
    private string _closedBy;
    private string _closedOn;
    private string _closedRemarks;

    private int _cancelledById;
    private string _cancelledBy;
    private string _cancelledOn;
    private string _cancelledRemarks;

    public string TicketNumber
    {
        get
        {
            return _ticketNumber;
        }

        set
        {
            _ticketNumber = value;
        }
    }

    public string Status
    {
        get
        {
            return _status;
        }

        set
        {
            _status = value;
        }
    }

    public string TicketType
    {
        get
        {
            return _ticketType;
        }

        set
        {
            _ticketType = value;
        }
    }

    public string TicketSubtype
    {
        get
        {
            return _ticketSubtype;
        }

        set
        {
            _ticketSubtype = value;
        }
    }

    public int CreatedById
    {
        get
        {
            return _createdById;
        }

        set
        {
            _createdById = value;
        }
    }

    public string CreatedBy
    {
        get
        {
            return _createdBy;
        }

        set
        {
            _createdBy = value;
        }
    }

    public string CreatedOn
    {
        get
        {
            return _createdOn;
        }

        set
        {
            _createdOn = value;
        }
    }

    public string CreatedRemarks
    {
        get
        {
            return _createdRemarks;
        }

        set
        {
            _createdRemarks = value;
        }
    }

    public int AllocatedById
    {
        get
        {
            return _allocatedById;
        }

        set
        {
            _allocatedById = value;
        }
    }

    public string AllocatedBy
    {
        get
        {
            return _allocatedBy;
        }

        set
        {
            _allocatedBy = value;
        }
    }

    public string AllocatedOn
    {
        get
        {
            return _allocatedOn;
        }

        set
        {
            _allocatedOn = value;
        }
    }

    public string AllocatedRemarks
    {
        get
        {
            return _allocatedRemarks;
        }

        set
        {
            _allocatedRemarks = value;
        }
    }

    public int ClosedById
    {
        get
        {
            return _closedById;
        }

        set
        {
            _closedById = value;
        }
    }

    public string ClosedBy
    {
        get
        {
            return _closedBy;
        }

        set
        {
            _closedBy = value;
        }
    }

    public string ClosedOn
    {
        get
        {
            return _closedOn;
        }

        set
        {
            _closedOn = value;
        }
    }

    public string ClosedRemarks
    {
        get
        {
            return _closedRemarks;
        }

        set
        {
            _closedRemarks = value;
        }
    }

    public int CancelledById
    {
        get
        {
            return _cancelledById;
        }

        set
        {
            _cancelledById = value;
        }
    }

    public string CancelledBy
    {
        get
        {
            return _cancelledBy;
        }

        set
        {
            _cancelledBy = value;
        }
    }

    public string CancelledOn
    {
        get
        {
            return _cancelledOn;
        }

        set
        {
            _cancelledOn = value;
        }
    }

    public string CancelledRemarks
    {
        get
        {
            return _cancelledRemarks;
        }

        set
        {
            _cancelledRemarks = value;
        }
    }

    public int TicketTypeId
    {
        get
        {
            return _ticketTypeId;
        }

        set
        {
            _ticketTypeId = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }

        set
        {
            _description = value;
        }
    }

    public string AllocatedTo
    {
        get
        {
            return _allocatedTo;
        }

        set
        {
            _allocatedTo = value;
        }
    }

    public string GetHtmlForPDF(DataTable dtTicketDetails, DataTable dtStatusDetails)
    {
        try
        {
            CreatedRemarks = "";

            AllocatedById = 0;
            AllocatedBy = "";
            AllocatedOn = "";
            AllocatedRemarks = "";

            ClosedById = 0;
            ClosedBy = "";
            ClosedOn = "";
            AllocatedRemarks = "";

            CancelledById = 0;
            CancelledBy = "";
            CancelledOn = "";
            CancelledRemarks = "";

            TicketNumber = Convert.ToString(dtTicketDetails.Rows[0]["TICKET_NUMBER"]);
            Status = Convert.ToString(dtTicketDetails.Rows[0]["STATUS_NAME"]);
            TicketTypeId = Convert.ToInt32(dtTicketDetails.Rows[0]["TICKET_TYPE_FID"]);
            TicketType = Convert.ToString(dtTicketDetails.Rows[0]["TICKET_TYPE"]);
            TicketSubtype = Convert.ToString(dtTicketDetails.Rows[0]["TICKET_SUBTYPE"]);
            Description = Convert.ToString(dtTicketDetails.Rows[0]["DESCRIPTION"]);
            AllocatedTo = Convert.ToString(dtTicketDetails.Rows[0]["ALLOCATED_TO"]);

            CreatedById = Convert.ToInt32(dtTicketDetails.Rows[0]["CREATED_BY_ID"]);
            CreatedBy = Convert.ToString(dtTicketDetails.Rows[0]["CREATED_BY"]);
            CreatedOn = Convert.ToString(dtTicketDetails.Rows[0]["CREATED_ON"]);

            if (dtTicketDetails.Rows[0]["CREATED_REMARKS"] != DBNull.Value)
                CreatedRemarks = Convert.ToString(dtTicketDetails.Rows[0]["CREATED_REMARKS"]);

            if (dtTicketDetails.Rows[0]["ALLOCATED_BY_ID"] != DBNull.Value)
            {
                AllocatedById = Convert.ToInt32(dtTicketDetails.Rows[0]["ALLOCATED_BY_ID"]);
                AllocatedBy = Convert.ToString(dtTicketDetails.Rows[0]["ALLOCATED_BY"]);
                AllocatedOn = Convert.ToString(dtTicketDetails.Rows[0]["ALLOCATED_ON"]);
            }

            if (dtTicketDetails.Rows[0]["ALLOCATED_REMARKS"] != DBNull.Value)
                AllocatedRemarks = Convert.ToString(dtTicketDetails.Rows[0]["ALLOCATED_REMARKS"]);

            if (dtTicketDetails.Rows[0]["CLOSED_BY_ID"] != DBNull.Value)
            {
                ClosedById = Convert.ToInt32(dtTicketDetails.Rows[0]["CLOSED_BY_ID"]);
                ClosedBy = Convert.ToString(dtTicketDetails.Rows[0]["CLOSED_BY"]);
                ClosedOn = Convert.ToString(dtTicketDetails.Rows[0]["CLOSED_ON"]);
            }

            if (dtTicketDetails.Rows[0]["CLOSED_REMARKS"] != DBNull.Value)
                ClosedRemarks = Convert.ToString(dtTicketDetails.Rows[0]["CLOSED_REMARKS"]);


            if (dtTicketDetails.Rows[0]["CANCELLED_BY_ID"] != DBNull.Value)
            {
                CancelledById = Convert.ToInt32(dtTicketDetails.Rows[0]["CANCELLED_BY_ID"]);
                CancelledBy = Convert.ToString(dtTicketDetails.Rows[0]["CANCELLED_BY"]);
                CancelledOn = Convert.ToString(dtTicketDetails.Rows[0]["CANCELLED_ON"]);
            }

            if (dtTicketDetails.Rows[0]["CANCELLED_REMARKS"] != DBNull.Value)
                CancelledRemarks = Convert.ToString(dtTicketDetails.Rows[0]["CANCELLED_REMARKS"]);


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append("<h2 class='headerStyle'>HRD TICKET</h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Ticket Number:</td>\n");
            sb.Append("<td class='td2header'><b>{#TicketNumber#}</b></td>\n");
            sb.Append("<td class='td1header'>Status:</td>\n");
            sb.Append("<td class='td2header'>{#Status#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Ticket Type:</td>\n");
            sb.Append("<td class='td2header'>{#TicketType#}</td>\n");

            if (TicketTypeId == (int)HRTicketEnums.EnumType.HR)
            {
                sb.Append("<td class='td1header'>Ticket Subtype:</td>\n");
                sb.Append("<td class='td2header'>{#TicketSubtype#}</td>\n");
            }
            else
            {
                sb.Append("<td class='td1header'>&nbsp;</td>\n");
                sb.Append("<td class='td2header'>&nbsp;</td>\n");
            }

            sb.Append("</tr>\n");


            if (TicketTypeId == (int)HRTicketEnums.EnumType.HR && !string.IsNullOrEmpty(AllocatedTo))
            {
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Allocated To:</td>\n");
                sb.Append("<td class='td2header'>{#AllocatedTo#}</td>\n");
                sb.Append("<td class='td1header'>&nbsp;</td>\n");
                sb.Append("<td class='td2header'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
            }


            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Description:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#Description#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");
            sb.Append("<hr />\n");

            
            //STATUS DETAILS START[===========================]

            int srNo = 0;

            if (dtStatusDetails.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Status Details</h3>\n");

                sb.Append("<table class='tblsubitems'>\n");

                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdsrno'>Sr.No.</th>\n");

                sb.Append("<th class='tdcate'>Status</th>\n");
                sb.Append("<th class='tddesc'>Created On</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow dr in dtStatusDetails.Rows)
                {
                    srNo++;
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");

                    sb.Append("<td class='tdcate'>{#Status#}</td>\n");
                    sb.Append("<td class='tddesc'>{#CreatedOn#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Replace("{#SRNo#}", Convert.ToString(srNo));

                    sb.Replace("{#Status#}", Convert.ToString(dr["STATUS"]));
                    //sb.Replace("{#CreatedOn#}", Convert.ToDateTime(dr["CREATED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt"));
                    sb.Replace("{#CreatedOn#}", Convert.ToString(dr["CREATED_ON"]));
                    
                }
                sb.Append("</table>\n");
            }



            sb.Append("<h3 class='header2'>Signatories</h3>\n");
            sb.Append("<hr />\n");

            sb.Append("<fieldset class='pdffieldset'>\n");
            sb.Append("<legend class='pdflegend'>Open</legend>\n");
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

            sb.Replace("{#TicketNumber#}", TicketNumber);
            sb.Replace("{#Status#}", Status);
            sb.Replace("{#TicketType#}", TicketType);
            sb.Replace("{#TicketSubtype#}", TicketSubtype);
            sb.Replace("{#AllocatedTo#}", AllocatedTo);
            sb.Replace("{#Description#}", Description);

            sb.Replace("{#CreatedBy#}", CreatedBy);
            sb.Replace("{#CreatedOn#}", CreatedOn);
            sb.Replace("{#CreatedRemarks#}", CreatedRemarks);

            if (TicketTypeId == (int)HRTicketEnums.EnumType.HR && AllocatedById > 0)
            {
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Allocated</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Allocated Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#AllocatedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Allocated By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AllocatedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Allocated On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AllocatedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");
                sb.Append("<hr class='hrsignatories' />\n");

                sb.Replace("{#AllocatedBy#}", AllocatedBy);
                sb.Replace("{#AllocatedOn#}", AllocatedOn);
                sb.Replace("{#AllocatedRemarks#}", AllocatedRemarks);
            }


            if (CancelledById > 0)
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

                sb.Replace("{#CancelledBy#}", CancelledBy);
                sb.Replace("{#CancelledOn#}", CancelledOn);
                sb.Replace("{#CancelledRemarks#}", CancelledRemarks);

            }

            if (ClosedById > 0)
            {
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Closed</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Closed Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#ClosedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Closed By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#ClosedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Closed On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#ClosedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");
                sb.Append("<hr class='hrsignatories' />\n");

                sb.Replace("{#ClosedBy#}", ClosedBy);
                sb.Replace("{#ClosedOn#}", ClosedOn);
                sb.Replace("{#ClosedRemarks#}", ClosedRemarks);

            }

            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}