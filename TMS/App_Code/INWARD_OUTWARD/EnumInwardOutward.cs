using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnumInwardOutward
/// </summary>
public class EnumInwardOutward
{
    public EnumInwardOutward()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum EnumINOUTStatus
    {
        New = 1,
        HODApproved = 2,
        Deleted = 3,
        Cancelled = 4,
        LogisticsConfirmed = 5,
        Acknowledged = 6,
        Closed = 7
        
    }

    public enum EnumINOUTStatusForUpdate
    {
        New = 1,
        HODApprove = 2,
        Delete = 3,
        Cancel = 4,
        LogisticsConfirm = 5,
        Acknowledge = 6,
        Close = 7

    }

    public enum LogisticsPersons
    {
        Senthil =  10 ,
        Davkinandan  = 99,
        RajivKr =  7,
        Jayant = 508
        //ChandanKr = 481,
        //ChandanKr = 476

    }



}