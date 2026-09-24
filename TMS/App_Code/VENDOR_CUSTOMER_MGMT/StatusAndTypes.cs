using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class StatusAndTypes
{

    public enum EnumDOCTypes : int
    {
        VRF = 1,
        PAN = 2,
        GSTIn = 3,
        CancelledCheque = 4,
        Other1 = 5,
        Other2 = 6,
        Other3 = 7,
        Other4 = 8
    }

    public enum EnumStatus
    {
        //Requested_1 = 1,
        //Amendment_2 = 2,
        //Amended_3 = 3,
        //HODApproved_4 = 4,
        //FinanceLevel1Approved_5 = 5,
        //FinanceLevel2Approved_6 = 6,
        //Registered_7 = 7,
        //Revised_8 = 8

        Created_1 = 1,
        Amendment_2 = 2,
        Amended_3 = 3,
        Checked_4 = 4,
        PROCHODApproved_5 = 5,
        AccountsChecked_6 = 6,
        FinalApproved_7 = 7,
        Registered_8 = 8,
        Revised_9 = 9,


    }

    public enum EnumSavingType
    {
        Save = 0,
        SaveAndSendForApproval = 1
    }

    public enum EnumEntityType
    {
        Vendor = 1,
        Customer = 2
    }

    public enum EnumApproverType
    {
        Checker_1 = 1,
        ProcurementHead_2 = 2,
        AccountsChecker_3 = 3,
        FinalApprover_4 = 4,
        ITTeam_5 = 5,

    }

    public enum EnumEmailType
    {
        Requested = 1,
        Amendment = 2,
        Amended = 3,
        HODApproved = 4,
        FinanceLevel1Approved = 5,
        FinanceLevel2Approved = 6,
        Registered = 7
    }

    public enum EnumRevisionType
    {
        BasicDetails_1 = 1,
        BillingAddress_2 = 2,
        ContactPerson_3 = 3,
        BankDetail_4 = 4,
        OtherDetail_5 = 5
    }

    public enum MasterTableType
    {
        tblItemCategorys_1 = 1,
        tblOrganizationTypes_2 = 2,
        tblRelationTypes_3 = 3,
        tblRevisionTypes_4 = 4,
        tblVendorCategorys_5 = 5,
        tblItemSubcategorys_6 = 6
    }
}