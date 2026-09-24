using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LOTMailTypeAndStatusProperties
{

    public int CurrentStatusID { get; set; }

    public int ApprovedByID { get; set; }

    public int AcceptedByID { get; set; }

    public int AmendedApprovedByID { get; set; }
    
    public int AmendedAcceptedByID { get; set; }
    
    public int MailTypeID { get; set; }
    
    public int NextStatusID { get; set; }
           
}