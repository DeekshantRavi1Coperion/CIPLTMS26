<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
    }

    void Application_End(object sender, EventArgs e)
    {

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started        
    }

    void Session_End(object sender, EventArgs e)
    {
        Session["EMP_RECORD_ID"] = null;
        Session["EMPLOYEE_NAME"] = null;
        Session["UNIT_ID"] = null;
        Session["USER_NAME"] = null;
        Session["PASSWORD"] = null;
        Session["EMAIL_ID"] = null;
        Session["USER_TYPE"] = null;
        Session["IS_TEAMLEADER"] = null;
        Session["TEAMLEADER_ID"] = null;
        Session["TEAMMEMBERS"] = null;
        Session["DEPARTMENT_ID"] = null;
        Session["TIMESHEET_DEPT_ID"] = null;
        Session["LOGIN_TIME"] = null;
        Session["TS_APPROVAL_BY"] = null;
        Session["DESIGN_RESPONSIBLE_ENGG_ID"] = null;
        Session["DESIGN_CHECKER_ID"] = null;
        Session["[PO_PIVOT_GROUP_APPROVER_ID]"] = null;
        Session["PUNCH_IN"]= null;
        Session["CHK_ACC"] = null;
    }

</script>

