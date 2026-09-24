using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetLOTMailTypeAndStatus
{

    LOTMailTypeAndStatusProperties objLOTMailTypeAndStatusProperties = new LOTMailTypeAndStatusProperties();

    LOTMailTypeAndStatusPropertiesByCurrentStatusID objLOTMailTypeAndStatusPropertiesByCurrentStatusID = new LOTMailTypeAndStatusPropertiesByCurrentStatusID();
    int currentStatusID = 0;
    int approvedByID = 0;
    int acceptedByID = 0;

    int amendedApprovedByID = 0;
    int amendedAcceptedByID = 0;

    int mailTypeID = 0;
    int nextStatusID = 0;

    public LOTMailTypeAndStatusProperties GetLOTMailTypeAndStatusValues(int createdByID, string approverStatus, int amendmentCount, int suppliedCurrentStatusID,
                                                                        int PEID, int PMID, int prodMngrID)
    {
        try
        {
            //objLOTMailTypeAndStatusProperties = GetValues(createdByID, approverStatus, suppliedCurrentStatusID);

            if (amendmentCount > 0)
                objLOTMailTypeAndStatusProperties = GetAmendedValues(createdByID, approverStatus, suppliedCurrentStatusID, PEID, PMID, prodMngrID);
            else
                objLOTMailTypeAndStatusProperties = GetValues(createdByID, approverStatus, suppliedCurrentStatusID, PEID, PMID, prodMngrID);


            return objLOTMailTypeAndStatusProperties;
        }
        catch (Exception)
        {
            return null;
        }
    }



    private LOTMailTypeAndStatusProperties GetValues(int createdByID, string approverStatus, int suppliedCurrentStatusID, int PEID, int PMID, int prodMngrID)
    {
        try
        {
            currentStatusID = 0;
            approvedByID = 0;
            acceptedByID = 0;
            amendedApprovedByID = 0;
            amendedAcceptedByID = 0;
            mailTypeID = 0;
            nextStatusID = 0;


            #region CREATED BY PE[=============================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_ALL) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PM_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    approvedByID = createdByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);                        
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.PlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QAIntlInspAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }

            #endregion



            #region CREATED BY PM[=============================]


            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_ALL) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PM_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    approvedByID = createdByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);                        
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.PlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QAIntlInspAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }

            #endregion



            #region CREATED BY PRODM[==========================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM) ||
                approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL) ||
                        approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM) ||
                        approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                        approvedByID = createdByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                    }


                    else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM) ||
                             approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    }
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                    {                       
                        if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL) ||
                            approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM) ||
                            approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM))
                        {
                            currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);                            
                            mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                            nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                        }


                        else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM) ||
                                 approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE))
                        {
                            currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                            mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                            nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                        }

                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);                        
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.PlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QAIntlInspAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }

            #endregion



            #region CREATED BY INDIVIDUAL[=====================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_ALL) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PM) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PRODM) ||
                    approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.PlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.ForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.QAIntlInspAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }
           
            #endregion


            objLOTMailTypeAndStatusProperties.CurrentStatusID = currentStatusID;
            objLOTMailTypeAndStatusProperties.ApprovedByID = approvedByID;
            objLOTMailTypeAndStatusProperties.AcceptedByID = acceptedByID;

            objLOTMailTypeAndStatusProperties.AmendedApprovedByID = amendedApprovedByID;
            objLOTMailTypeAndStatusProperties.AmendedAcceptedByID = amendedAcceptedByID;
            objLOTMailTypeAndStatusProperties.MailTypeID = mailTypeID;
            objLOTMailTypeAndStatusProperties.NextStatusID = nextStatusID;

            return objLOTMailTypeAndStatusProperties;

        }
        catch (Exception)
        {
            return null;
        }
    }
   
    private LOTMailTypeAndStatusProperties GetAmendedValues(int amendedByID, string approverStatus, int suppliedCurrentStatusID, int PEID, int PMID, int prodMngrID)
    {
        try
        {
            currentStatusID = 0;
            approvedByID = 0;
            acceptedByID = 0;
            amendedApprovedByID = 0;
            amendedAcceptedByID = 0;
            mailTypeID = 0;
            nextStatusID = 0;


            #region CREATED BY PE[=============================]

            if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_ALL))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }

                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    amendedApprovedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PM_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }


                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }


                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    amendedApprovedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }


                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            #endregion



            #region CREATED BY PM[=============================]


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_ALL))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    amendedApprovedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }

            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PM_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }

            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    amendedApprovedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            #endregion



            #region CREATED BY PRODM[==========================]


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }


                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            #endregion



            #region CREATED BY INDIVIDUAL[=====================]

            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_ALL))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }


                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    amendedApprovedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }

            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    amendedApprovedByID = amendedByID;
                    amendedAcceptedByID = amendedByID;
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedApprovedByID = amendedByID;
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }


                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PRODM))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }


            else if (approverStatus == Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_NONE))
            {
                if (suppliedCurrentStatusID == 0)
                {
                    currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                }
                else
                {
                    if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovalMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                        amendedApprovedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedApprovedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedPlanningAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedForwardedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                        amendedAcceptedByID = amendedByID;
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendedQualityAcceptedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.AmendmentMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    else if (suppliedCurrentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                    {
                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                        mailTypeID = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CompletedMail);
                        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Completed);
                    }
                }
            }

            #endregion


            objLOTMailTypeAndStatusProperties.CurrentStatusID = currentStatusID;

            objLOTMailTypeAndStatusProperties.ApprovedByID = approvedByID;
            objLOTMailTypeAndStatusProperties.AcceptedByID = acceptedByID;

            objLOTMailTypeAndStatusProperties.AmendedApprovedByID = amendedApprovedByID;
            objLOTMailTypeAndStatusProperties.AmendedAcceptedByID = amendedAcceptedByID;

            objLOTMailTypeAndStatusProperties.MailTypeID = mailTypeID;
            objLOTMailTypeAndStatusProperties.NextStatusID = nextStatusID;

            return objLOTMailTypeAndStatusProperties;

        }
        catch (Exception)
        {
            return null;
        }
    }



    #region Changes On 09-Mar-2021
    /*
     0001   :   Changes the value of acceptedBy from acceptedBy to prodMngrID
     0002   :   Changes the value of approvedBy from createdBy to PMID
     0003   :   Changes the value of approvedBy from createdBy to PEID
     0004   :   Changes the current status as New and next status as approved
     */
    #endregion

}