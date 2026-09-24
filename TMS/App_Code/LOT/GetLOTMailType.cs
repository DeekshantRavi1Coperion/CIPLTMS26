using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetLOTMailType
{
    int mailTypeID = 0;

    public int GetLOTMailTypeValue(int PEID, int PMID, int prodMngrID, string approverStatus, int amendmentCount, int suppliedCurrentStatusID)
    {
        try
        {
            mailTypeID = 0;

            if (amendmentCount == 0)
            {
                mailTypeID = GetMailTypeID(PEID, PMID, prodMngrID, suppliedCurrentStatusID);
            }
            else
            {
                mailTypeID = GetAmendedMailTypeID(PEID, PMID, prodMngrID, suppliedCurrentStatusID);
            }


            return mailTypeID;
        }
        catch (Exception)
        {
            return 0;
        }
    }



    private int GetValues(string approverStatus, int suppliedCurrentStatusID)
    {
        try
        {
            mailTypeID = 0;

            #region CREATED BY PE[=============================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_ALL) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PM_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_NONE))
            {
                //mailTypeID = GetMailTypeId(approverStatus, suppliedCurrentStatusID);
            }

            #endregion



            #region CREATED BY PM[=============================]


            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_ALL) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PM_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_NONE))
            {
                //mailTypeID = GetMailTypeId(approverStatus, suppliedCurrentStatusID);
            }

            #endregion



            #region CREATED BY PRODM[==========================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE))
            {
                //mailTypeID = GetMailTypeId(approverStatus, suppliedCurrentStatusID);
            }

            #endregion



            #region CREATED BY INDIVIDUAL[=====================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_ALL) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PM) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PRODM) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_NONE))
            {
                //mailTypeID = GetMailTypeId(approverStatus, suppliedCurrentStatusID);
            }

            #endregion

            return mailTypeID;

        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int GetMailTypeID(int PEID, int PMID, int prodMngrID, int suppliedCurrentStatusID)
    {
        try
        {
            mailTypeID = 0;

            if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);

                if ((PEID == PMID && PMID == prodMngrID) || (PEID != PMID && PMID == prodMngrID) || (PEID != PMID && PEID == prodMngrID))
                {
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                }
                else if ((PEID == PMID && PMID != prodMngrID) || (PEID != PMID && PMID != prodMngrID))
                {
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                }
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.PlanningAcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ForwardedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QualityAcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QAIntlInspAcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
            }

            return mailTypeID;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int GetAmendedMailTypeID(int PEID, int PMID, int prodMngrID, int suppliedCurrentStatusID)
    {
        try
        {
            mailTypeID = 0;

            if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);

                if ((PEID == PMID && PMID == prodMngrID) || (PEID != PMID && PMID == prodMngrID) || (PEID != PMID && PEID == prodMngrID))
                {
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                }
                else if ((PEID == PMID && PMID != prodMngrID) || (PEID != PMID && PMID != prodMngrID))
                {
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                }
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
            }



            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QAIntlInspAcceptedMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
            }

            else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
            {
                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
            }

            return mailTypeID;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    //private int GetAmendedValues(int amendedByID, string approverStatus, int suppliedCurrentStatusID, int PEID, int PMID, int prodMngrID)
    //{
    //    try
    //    {
    //        currentStatusID = 0;
    //        approvedByID = 0;
    //        acceptedByID = 0;
    //        amendedApprovedByID = 0;
    //        amendedAcceptedByID = 0;
    //        mailTypeID = 0;
    //        nextStatusID = 0;


    //        #region CREATED BY PE[=============================]

    //        if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_ALL))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }

    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                amendedApprovedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PM_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }


    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }


    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_NONE))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                amendedApprovedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }


    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        #endregion



    //        #region CREATED BY PM[=============================]


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_ALL))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                amendedApprovedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }

    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PM_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }

    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_NONE))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                amendedApprovedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        #endregion



    //        #region CREATED BY PRODM[==========================]


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }


    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        #endregion



    //        #region CREATED BY INDIVIDUAL[=====================]

    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_ALL))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }


    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                amendedApprovedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }

    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                amendedApprovedByID = amendedByID;
    //                amendedAcceptedByID = amendedByID;
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedApprovedByID = amendedByID;
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }


    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PRODM))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }


    //        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_NONE))
    //        {
    //            if (suppliedCurrentStatusID == 0)
    //            {
    //                currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //            }
    //            else
    //            {
    //                if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
    //                    amendedApprovedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
    //                    amendedAcceptedByID = amendedByID;
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
    //                }

    //                else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //                {
    //                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
    //                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
    //                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
    //                }
    //            }
    //        }

    //        #endregion



    //        return 0;

    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //}

}