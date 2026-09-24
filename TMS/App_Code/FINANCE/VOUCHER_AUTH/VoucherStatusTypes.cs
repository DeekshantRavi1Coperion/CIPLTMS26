using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VoucherStatusTypes
/// </summary>
public class VoucherStatusTypes
{
    public enum EnumStatus : int
    {
        OPEN = 0,
        AUTHORIZED = 1,
        APPROVED = 2,
        REAUTHORIZATION = 3,
        REAUTHORIZED = 4
    }

    public enum EnumVoucherTypes : int
    {
        JV = 1,
        PV = 2,
        MRN = 3,
        SV = 4,
        SOV = 5,
        SIV = 6,
        VRPV = 7,
        CRPV = 8,
        CBV = 9,
        CV = 10,
        MR = 11,
        POV = 12,
        IIV = 13,
        IRV = 14,        
        VDNV = 15,
        VCNV = 16,
        CDNV = 17,
        CCNV = 18,
        STV = 19,
        SAV = 20,
        PRV = 21,
        SRV = 22
    }

    public enum EnumVoucherActions : int
    {
        AddDoc = 1
    }
}