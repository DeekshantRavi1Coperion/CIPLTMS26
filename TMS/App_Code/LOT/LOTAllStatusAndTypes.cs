using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class LOTAllStatusAndTypes
{
    //public enum EnumUnit : int
    //{
    //    A35 = 1,
    //    Gnu = 4,
    //}

    public enum EnumProductionStatusType : int
    {
        ProductionView = 1,
        ProjectView = 2,
    }

    public enum EnumStatus
    {
        //New = 1,
        //Approved = 2,
        //ProdAccepted = 3,
        //IntlInspection = 4,
        //QAAccepted = 5,
        //QANotAccepted = 6,
        //Complete = 7,
        //Amendment = 8,
        //Amended = 9,
        //AmndApproved = 10,
        //AmndProdAccepted = 11,

        //Completed = 12// DECLARE FOR OUR CONVENICE , IT IS NOT ANY STATUS


        //New = 1,
        //Approved = 2,
        //ProdAccepted = 3,
        //QALOTAccepted = 4,
        //IntlInspection = 5,
        //QAItemAccepted = 6,
        //QAItemNotAccepted = 7,
        //Complete = 8,
        //Amendment = 9,
        //Amended = 10,
        //AmndApproved = 11,
        //AmndProdAccepted = 12,
        //AmndQALOTAccepted = 13,
        //Completed = 14// DECLARE FOR OUR CONVENICE , IT IS NOT ANY STATUS


        New = 1,
        Approved = 2,
        PlanningAccepted = 3,
        Forwarded = 4,
        ProdAccepted = 5,
        QALOTAccepted = 6,
        FitupInspection = 7,
        QAItemAccepted = 8,
        QAItemNotAccepted = 9,
        Complete = 10,
        Amendment = 11,
        Amended = 12,
        AmndApproved = 13,
        AmndPlanningAccepted = 14,
        AmndForwarded = 15,
        AmndProdAccepted = 16,
        AmndQALOTAccepted = 17,
        Completed = 18,// DECLARE FOR OUR CONVENICE , IT IS NOT ANY STATUS

        FinalInspection = 23,
        Rework = 24,
        //Transferred = 27


    }

    public enum EnumPartialStatus
    {
        //Partial = 14,
        //PartialIntlInspection = 15,
        //PartialQAItemAccepted = 16,
        //PartialQAItemNotAccepted = 17,
        //PartialComplete = 18

        Partial = 18,
        PartialFitupInspection = 19,
        PartialQAItemAccepted = 20,
        PartialQAItemNotAccepted = 21,
        PartialComplete = 22,
        PartialFinalInspection = 25,
        PartialRework = 26

    }




    public enum EnumEmailType
    {
        //NotApplicable = 0,
        //ApprovalMail = 1,
        //AcceptanceMail = 2,
        //AcceptedMail = 3,
        //SendToIntlInspMail = 4,
        //QAIntlInspAcceptedMail = 5,
        //QAIntlInspNotAcceptedMail = 6,
        //CompletedMail = 7,
        //AmendmentMail = 8,
        //AmendedApprovalMail = 9,
        //AmendedAcceptanceMail = 10,
        //AmendedAcceptedMail = 11,

        //QualityAcceptedMail = 12,
        //PlanningAcceptedMail = 13,
        //AmendedQualityAcceptedMail = 14,
        //AmendedPlanningAcceptedMail = 15

        NotApplicable = 0,
        ApprovalMail = 1,
        ApprovedMail = 2,
        PlanningAcceptedMail = 3,
        ForwardedMail = 4,
        AcceptedMail = 5,
        QualityAcceptedMail = 6,

        SendToFitupInspMail = 7,
        QAIntlInspAcceptedMail = 8,
        QAIntlInspNotAcceptedMail = 9,
        CompletedMail = 10,
        AmendmentMail = 11,

        AmendedApprovalMail = 12,
        AmendedApprovedMail = 13,
        AmendedPlanningAcceptedMail = 14,
        AmendedForwardedMail = 15,
        AmendedAcceptedMail = 16,
        AmendedQualityAcceptedMail = 17,
        AmendmentByProdMail = 18,

        SendToFinalInspMail = 19,
        SendToReworkMail = 20,
        CancellationMail = 21,
        LinkProductionOrderByStoreEmail = 22
    }


    public enum ApproverStatus
    {
        //Created By Project Engineer
        CREPE_ALL,          //All are same
        CREPE_PE_PM,        //Only PE and PM are same
        CREPE_PM_PRODM,     //Only PM and PRODM are same
        CREPE_PE_PRODM,     //Only PE and PRODM are same
        CREPE_NONE,         //None of them are same

        //Created By Project Manager
        CREPM_ALL,          //All are same
        CREPM_PE_PM,        //Only PE and PM are same
        CREPM_PM_PRODM,     //Only PM and PRODM are same
        CREPM_PE_PRODM,     //Only PE and PRODM are same
        CREPM_NONE,         //None of them are same

        //Created By Production Manager
        CREPRODM_ALL,       //All are same
        CREPRODM_PE_PM,     //Only PE and PM are same
        CREPRODM_PM_PRODM,  //Only PM and PRODM are same
        CREPRODM_PE_PRODM,  //Only PE and PRODM are same
        CREPRODM_NONE,      //None of them are same

        //Created By an individual (No PR, No PM and No Production Manager)
        CREI_ALL,           //All are same
        CREI_PE_PM,         //Only PE and PM are same
        CREI_PM_PRODM,      //Only PM and PRODM are same
        CREI_PM_PRODM_APPPE,//Only PE (Approved By) and PRODM are same
        CREI_PM_PRODM_APPPM,//Only PM (Approved By) and PRODM are same
        CREI_PE_PRODM,      //Only PE and PRODM are same
        CREI_NONE           //None of them are same
    }


    public enum EnumActID
    {
        ApproveOrAccept = 1,
        SendToAmendment = 2,
        SendToAmendmentByProduction = 3
    }


    public enum EnumQualityAndPlanningStatus
    {
        //Quality = 1,
        //Planning = 2,
        //AmendedQuality = 3,
        //AmendedPlanning = 4

        PlanningAccepted = 1,
        PlanningForward = 2,
        QualityAccepted = 3,

        AmndPlanningAccepted = 4,
        AmndPlanningForward = 5,
        AmndQualityAccepted = 6
    }


    public enum EnumLOTDepartments
    {
        Production = 1,
        Quality = 2,
        Planning = 3
    }


    public enum EnumLOTListButtons
    {
        Approve = 1,
        SendToFitupInsp = 2,
        AcceptItems = 3,
        NotAcceptItems = 4,
        SendToFinalInsp = 5,
        CompleteItems = 6,
        Rework = 7
    }



    public enum EnumPDFType
    {
        Mail = 1,
        List = 2,
        Create_Preview = 3,
        Edit_Preview = 4
    }

    public enum EnumLOTUpdationType
    {
        Edit = 1,
        Amend = 2,
        //EditAmended = 3
    }

    public enum ButtonClicked
    {
        AcceptLOT = 1,
        AcceptItems = 2
    }

    public enum Category
    {
        Fabrication = 1,
        Inspection = 2,
        Information = 3
    }


    public enum SavingType
    {
        Save = 0,
        SaveAndSendForApproval = 1
    }

    public enum EnumTransferType : int
    {
        Transfer = 0,
        Transferred = 1,
    }

}