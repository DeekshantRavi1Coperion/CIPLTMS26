using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class BOMAllTypeEnums
{
    public enum EnumProductScreenType : int
    {
        ViewScreen = 1,
        ApproveScreen = 2
    }

    public enum EnumStatus : int
    {
        New = 1,
        Approved = 2,
        Amendment = 3,
        Amended = 4,
        AmendedApproved = 5,
        Cancelled = 6
    }

    public enum EnumAction : int
    {
        Create = 1,
        Edit = 2
        //Approve = 2,
        //Amendment = 3,
        //Amend = 4,
        //AmendedApprove = 5
    }

    public enum EnumMailType : int
    {
        ApprovalMail = 1,
        ApprovedMail = 2,
        AmendmentMail = 3,
        AmendedMail = 4,
        AmendedApprovedMail = 5
    }

    public enum EnumSavingType : int
    {
        Save = 0,
        SaveAndSendForApproval = 1
    }
}