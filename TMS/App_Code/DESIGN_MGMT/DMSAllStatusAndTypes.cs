using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class DMSAllStatusAndTypes
{
    public enum EnumStatus
    {
        //Generated = 1,
        //Open = 2,
        //Checking = 3,
        //Checked = 4,
        //Amendment = 5,
        //Closed = 6,
        //AmendedOpen = 7,
        //AmendedChecking = 8,
        //AmendedChecked = 9

        Generated = 1,
        Open = 2,
        Checking = 3,
        Checked = 4
        //Closed

    }


    public enum EnumMailType
    {
        GeneratedMail = 1,

        OpenToHimselfMail = 2,
        OpenToOtherMail = 3,
        AssignmentMail = 4,
        
        CheckingMail = 5,
        
        CheckedMail = 6,
        
        ClosedMail = 7,

        EditedGeneratedMail = 8,
        
        EditedOpenToHimselfMail = 9,        
        EditedOpenToOtherMail = 10,

        AmendmentMail = 11,
        CorrectionMail = 12,

        AmendededMail = 13,
        
        AmendedOpenToHimselfMail = 14,
        AmendedOpenToOtherMail = 15,

        AmendedAssignmentMail = 16,
        AmendedCheckingMail = 17,
        AmendedCheckedMail = 18

    }


    public enum AmendmentType
    {
        Amendment = 1,
        Correction = 2
    }

    public enum EnumRespEnggType
    {
        Design = 1,
        Project = 2
    }

    public enum EnumManager
    {
        SSKarasi = 29,
        Sunil = 41,
        Jaswinder = 261
    }

    public enum Action
    {
        Select = 0,
        Assign = 1,
        Send_To_Checking = 2,
        Check = 3,        
        Send_To_Amendment = 4,
        Send_To_Correction = 5,
        Re_Assign = 6
    }
}