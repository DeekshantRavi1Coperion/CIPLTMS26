using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class BOMHeader
{
    public int Pid { get; set; }
    public int UnitFid { get; set; }
    public int TypeFid { get; set; }
    public string MrNo { get; set; }
    public int StatusFid { get; set; }
    public string BomNo { get; set; }
    public DateTime BomDate { get; set; }
    public string JobNo { get; set; }
    public DateTime DeliveryRequiredBy { get; set; }
    public string AcceptableVendor1 { get; set; }
    public string AcceptableVendor2 { get; set; }
    public string AcceptableVendor3 { get; set; }
    public string AcceptableVendor4 { get; set; }
    public string AcceptableVendor5 { get; set; }
    public int RevisionNumber { get; set; }
    public double BudgetedCost { get; set; }
    public double EstimatedCost { get; set; }
    public string CostRelatedRemarks { get; set; }
    public int PivotGroupFid { get; set; }
    public int ResponsibleForBomFid { get; set; }
    public int IsTcRequired { get; set; }
    public int CreatedByFid { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedRemarks { get; set; }
    public int ApprovedByFid { get; set; }
    public DateTime ApprovedOn { get; set; }
    public string ApprovedRemarks { get; set; }
    public int AmendmentCount { get; set; }
    public int AmendmentByFid { get; set; }
    public DateTime AmendmentOn { get; set; }
    public string AmendmentRemarks { get; set; }
    public int AmendedByFid { get; set; }
    public DateTime AmendedOn { get; set; }
    public string AmendedRemarks { get; set; }
    public int AmendedApprovedByFid { get; set; }
    public DateTime AmendedApprovedOn { get; set; }
    public string AmendedApprovedRemrks { get; set; }
    public int IsSentForApproval { get; set; }
    public int IsApprovalMailSent { get; set; }
    public int IsApprovedMailSent { get; set; }
    public int IsAmendmentMailSent { get; set; }
    public int IsAmendedMailSent { get; set; }
    public int IsAmendedApprovedMailSent { get; set; }

    public BOMHeader
        (
          int unitFid
        , int typeFid
        , string mrNo
        , int statusFid
        , string bomNo
        , DateTime bomDate
        , string jobNo
        , DateTime deliveryRequiredBy
        , string acceptableVendor1
        , string acceptableVendor2
        , string acceptableVendor3
        , string acceptableVendor4
        , string acceptableVendor5
        , int revisionNumber
        , double budgetedCost
        , double estimatedCost
        , string costRelatedRemarks
        , int pivotGroupFid
        , int responsibleForBomFid
        , int isTcRequired
        , int createdByFid
        , string createdRemarks

        )
    {
        UnitFid = unitFid;
        TypeFid = typeFid;
        MrNo = mrNo;
        StatusFid = statusFid;
        BomNo = bomNo;
        BomDate = bomDate;
        JobNo = jobNo;
        DeliveryRequiredBy = deliveryRequiredBy;
        AcceptableVendor1 = acceptableVendor1;
        AcceptableVendor2 = acceptableVendor2;
        AcceptableVendor3 = acceptableVendor3;
        AcceptableVendor4 = acceptableVendor4;
        AcceptableVendor5 = acceptableVendor5;
        RevisionNumber = revisionNumber;
        BudgetedCost = budgetedCost;
        EstimatedCost = estimatedCost;
        CostRelatedRemarks = costRelatedRemarks;
        PivotGroupFid = pivotGroupFid;
        ResponsibleForBomFid = responsibleForBomFid;
        IsTcRequired = isTcRequired;
        CreatedByFid = createdByFid;
        CreatedRemarks = createdRemarks;
    }
}