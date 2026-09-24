using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TandTAllStatus
{
    public enum EnumTripType : int
    {
        Reimbursable = 1,
        NonReimbursable = 2
    }

    public enum EnumTravelApprovalTypes : int
    {
        All = 1,
        HodApproval = 2,
        HrChecked = 3,
        Passed = 4,
        Settled = 5,
        Amendment = 6,
        AmendmentApproved = 7,
        AmendmentChecked = 8,
        AmendmentPassed = 9,
        InvoiceBooked = 10,
        AccChecked = 11,
        AmendmentAccChecked = 12

    }

    public enum EnumTourAct
    {
        New = 1,
        HODApprove = 2,
        Delete = 3,
        Cancel = 4,
        ApprovedAndUpdateAdviceGenerated = 5,
        FinalApprove = 6,
        MgmtApprove = 7,
        UKVApproveByParzer = 8,
        //UKVhodFinalApprove = 8,
        //UKVhodNonFinalApprove = 9,

    }

    public enum EnumTourStatus
    {
        New = 1,
        HODApproved = 2,
        Deleted = 3,
        Cancelled = 4,
        FinalApproved = 5,
        MgmtHODApproved = 6,
        UKVApproveByParzer = 7,
    }

    public enum EnumTourAdditionalAdvance
    {
        New = 1,
        HODApproved = 2,
        
    }

    public enum EnumTourBasedOn
    {
        Customer = 1,
        NonCustomer = 2,

    }


    public enum EnumTourExpenseActions : int
    {
        Update = 1,
        Book = 2,
        Close = 3
    }

    public enum EnumTourExpenseFiles : int
    {
        File1 = 1,
        File2 = 2
    }

    public enum EnumTourExpenseStatus : int
    {
        Open = 1,
        Booked = 2,
        Closed = 3
    }

    public enum EnumTourExpenseMailType : int
    {
        OpenMail = 1,
        BookedMail = 2
    }


    public enum EnumTravelStatus
    {
        New = 1,
        Approved = 2,
        Checked = 3,
        Passed = 4,
        Settled = 5,
        Cancelled = 6,
        Amendment = 7,
        Amended = 8,
        AmendedApproved = 9,
        AmendedChecked = 10,
        AmendedPassed = 11,
        InvoiceBooked = 12,
        AccChecked = 13,
        AmendedAccChecked = 14
    }


    public enum EnumTravelMode
    {
        Air = 1,
        Rail = 2,
        Bus = 3,
        Other = 4,
        AirBusiness = 5,
        AirEconomy = 6
    }




    public enum EnumOthers : int
    {
        CurrencyINR = 68,
        AccountPerson = 3,
        AdminPerson = 117,
        FinalApproverID = 411,
        UKVEmpRecordID = 8

    }
}