using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LOTMailInfoProperties
{
    public int statusID { get; set; }

    public int CreatedByID { get; set; }
    public string CreatedByName { get; set; }
    public string CreatedByEmail { get; set; }


    public int ApprovedByID { get; set; }
    public string ApprovedByName { get; set; }
    public string ApprovedByEmail { get; set; }


    public int PlanningAcceptedByID { get; set; }
    public string PlanningAcceptedByName { get; set; }
    public string PlanningAcceptedByEmail { get; set; }


    public int ForwardedByID { get; set; }
    public string ForwardedByName { get; set; }
    public string ForwardedByEmail { get; set; }


    public int AcceptedByID { get; set; }
    public string AcceptedByName { get; set; }
    public string AcceptedByEmail { get; set; }


    public int QualityAcceptedByID { get; set; }
    public string QualityAcceptedByName { get; set; }
    public string QualityAcceptedByEmail { get; set; }




    public int AmendedByID { get; set; }
    public string AmendedByName { get; set; }
    public string AmendedByEmail { get; set; }


    public int AmendedApprovedByID { get; set; }
    public string AmendedApprovedByName { get; set; }
    public string AmendedApprovedByEmail { get; set; }


    public int PlanningAmendedAcceptedByID { get; set; }
    public string PlanningAmendedAcceptedByName { get; set; }
    public string PlanningAmendedAcceptedByEmail { get; set; }

    public int TransferredByID { get; set; }
    public string TransferredByName { get; set; }
    public string TransferredByEmail { get; set; }

    public int TransferredPassedByID { get; set; }
    public string TransferredPassedByName { get; set; }
    public string TransferredPassedByEmail { get; set; }


    public int ForwardedAmendedByID { get; set; }
    public string ForwardedAmendedByName { get; set; }
    public string ForwardedAmendedByEmail { get; set; }


    public int AmendedAcceptedByID { get; set; }
    public string AmendedAcceptedByName { get; set; }
    public string AmendedAcceptedByEmail { get; set; }


    public int QualityAmendedAcceptedByID { get; set; }
    public string QualityAmendedAcceptedByName { get; set; }
    public string QualityAmendedAcceptedByEmail { get; set; }


    public int SendToIntlInspectionByID { get; set; }
    public string SendToIntlInspectionByName { get; set; }
    public string SendToIntlInspectionByEmail { get; set; }


    public int QAIntlInspectionAcceptedByID { get; set; }
    public string QAIntlInspectionAcceptedByName { get; set; }
    public string QAIntlInspectionAcceptedByEmail { get; set; }


    public int QAIntlInspectionNotAcceptedByID { get; set; }
    public string QAIntlInspectionNotAcceptedByName { get; set; }
    public string QAIntlInspectionNotAcceptedByEmail { get; set; }



    public int SentToFinalInspByID { get; set; }
    public string SentToFinalInspByName { get; set; }
    public string SentToFinalInspByEmail { get; set; }


    public int SentToReworkByID { get; set; }
    public string SentToReworkByName { get; set; }
    public string SentToReworkByEmail { get; set; }



    public int AmendmentCount { get; set; }
    public int AmendmentByID { get; set; }
    public string AmendmentByName { get; set; }
    public string AmendmentByEmail { get; set; }

    public string ProdAmendmentByName { get; set; }
    public string ProdAmendmentByEmail { get; set; }
    public int ProdAmendmentByID { get; set; }

    public int CompletedByID { get; set; }
    public string CompletedByName { get; set; }
    public string CompletedByEmail { get; set; }


    public int PEID { get; set; }
    public string PEName { get; set; }
    public string PEEmail { get; set; }
    public string PEUD { get; set; }
    public string PEPD { get; set; }


    public int PMID { get; set; }
    public string PMName { get; set; }
    public string PMEmail { get; set; }
    public string PMUD { get; set; }
    public string PMPD { get; set; }


    public int ProdMngrID { get; set; }
    public string ProdMngrName { get; set; }
    public string ProdMngrEmail { get; set; }
    public string ProdMngrEmailCC { get; set; }



    public string QualityMngrNames { get; set; }

    public string QualityMngrEmails { get; set; }

    public string planningMngrIDs { get; set; }

    public string PlanningMngrNames { get; set; }

    public string PlanningMngrEmails { get; set; }

    public string PlanningMngrEmailsCC { get; set; }

    public int CancelledByID { get; set; }
    public string CancelledByName { get; set; }
    public string CancelledByEmail { get; set; }

    public string StorePersonName { get; set; }
    public string StorePersonEmail { get; set; }

    public string ValvesAdditionalEmailCC { get; set; }
}