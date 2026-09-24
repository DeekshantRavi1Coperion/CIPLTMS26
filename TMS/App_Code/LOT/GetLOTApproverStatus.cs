using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetLOTApproverStatus
{
    public string GetApproverStatusValue(int createdByID, int PEID, int PMID, int prodMngrID, int approvedByID)
    {
        try
        {
            string approverStatus = string.Empty;

            string ids = string.Empty;

            //Created by any of Project Engineer or Project Manager or Production Manager
            if (createdByID == PEID || createdByID == PMID || createdByID == prodMngrID)
            {
                if (createdByID == PEID)
                {
                    if (PEID == PMID && PMID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_ALL);

                    else if (PEID == PMID && PMID != prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PM);

                    else if (PEID != PMID && PMID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PM_PRODM);

                    else if (PEID != PMID && PEID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPE_PE_PRODM);

                    else if (PEID != PMID && PMID != prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_NONE);

                }

                else if (createdByID == PMID)
                {
                    if (PEID == PMID && PMID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_ALL);

                    else if (PEID == PMID && PMID != prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PM);

                    else if (PEID != PMID && PMID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PM_PRODM);

                    else if (PEID != PMID && PEID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_PE_PRODM);

                    else if (PEID != PMID && PMID != prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPM_NONE);

                }

                else if (createdByID == prodMngrID)
                {
                    if (PEID == PMID && PMID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_ALL);

                    else if (PEID == PMID && PMID != prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PM);

                    else if (PEID != PMID && PMID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PM_PRODM);

                    else if (PEID != PMID && PEID == prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_PE_PRODM);

                    else if (PEID != PMID && PMID != prodMngrID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREPRODM_NONE);
                }
            }

            //Created by an individual none of Project Engineer or Project Manager or Production Manager
            else
            {

                if (PEID == PMID && PMID == prodMngrID)
                    approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_ALL);

                else if (PEID == PMID && PMID != prodMngrID)
                    approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PM);

                else if (PEID != PMID && PMID == prodMngrID)
                {
                    approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM);

                    if (approvedByID == PEID)
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM_APPPE);

                    else
                        approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PM_PRODM_APPPM);
                }

                else if (PEID != PMID && PEID == prodMngrID)
                    approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_PE_PRODM);

                else if (PEID != PMID && PMID != prodMngrID)
                    approverStatus = Convert.ToString(LOTAllStatusAndTypes.ApproverStatus.CREI_NONE);
            }

            return approverStatus;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}