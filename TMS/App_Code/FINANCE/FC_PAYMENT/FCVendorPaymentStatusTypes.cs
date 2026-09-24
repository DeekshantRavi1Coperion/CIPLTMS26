using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FCVendorPaymentStatusTypes
/// </summary>
public class FCVendorPaymentStatusTypes
{
    public enum EnumStatus : int
    {
        OPEN = 1,
        AMENDMENT = 2,
        AMENDED = 3,
        APPROVED = 4,
        AMENDED_APPROVED = 5,
        RELEASED = 6,
        CANCELLED = 7
    }

    public enum EnumEmailType : int
    {
        APPROVAL_MAIL = 1,
        AMENDMENT_MAIL = 2,
        AMENDED_APPROVAL_MAIL = 3,
        APPROVED_MAIL = 4,
        AMENDED_APPROVED_MAIL = 5,
        RELEASED_MAIL = 6,
        CANCELLED_MAIL = 7
    }

    
}