using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HRTicketEnums
/// </summary>
public class HRTicketEnums
{
    public enum EnumType : int
    {
        HR = 1,
        Admin = 2
    }

    public enum EnumStatus : int
    {
        Open = 1,
        Allocated = 2,
        Closed = 3,
        Cancelled = 4
    }

    public enum EnumMailType : int
    {
        HROpen = 1,
        HRAllocated = 2,
        HRClosed = 3,
        HRCancelled = 4,
        AdminOpen = 5,
        AdminClosed = 6,
        AdminCancelled = 7
    }

    public enum EnumActions : int
    {
        Update = 1,
        Allocate = 2,
        Close = 3,
        Cancel = 4
    }
}