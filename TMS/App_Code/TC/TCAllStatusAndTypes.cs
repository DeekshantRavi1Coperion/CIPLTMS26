using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TCAllStatusAndTypes
/// </summary>
public class TCAllStatusAndTypes
{
    public TCAllStatusAndTypes()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum EnumStatus
    {
        Open = 1,
        Accepted = 2,
        Rejected = 3
    }
    public enum EnumTCType
    {
        TC1 = 1,
        TC2 = 2,
        TC3 = 3
    }

    public enum EnumMails
    {
        AcceptedMail = 1,
        NotAcceptedMail = 2
    }
}