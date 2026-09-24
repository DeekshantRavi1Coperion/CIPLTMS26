public class MSAllStatusAndTypes
{
    public enum EnumValidationType : int
    {
        Valid = 1,
        InvalidDateRange = 2,
        OverlappingWithPrev = 3,
        OverlappingWithNext = 4
    }

    public enum EnumCatetory : int
    {
        Equipment = 1,
        Piping = 2
    }

    public enum EnumStatus
    {
        Assigned = 1,
        Accepted = 2,
        Rejected = 3,
        Scheduled = 4,
        Completed = 5,
        QA_Inspected = 6,
        Re_Assigned = 7,
        Re_Scheduled = 8
    }


    public enum EnumMailType
    {
        AssignedMail = 1,
        AcceptedMail = 2,
        RejectedMail = 3,
        ScheduledMail = 4,
        CompletedMail = 5,
        QA_AcceptedMail = 6,
        QA_RejectedMail = 7
    }

    public enum EnumPDFType
    {
        NonScheduled = 1,
        Scheduled = 2
    }

    public enum EnumRowCommandFlags
    {
        ModifyWorker = 1,
        ViewSchedulingDetails = 2,
        CompleteScheduling = 3
    }
}