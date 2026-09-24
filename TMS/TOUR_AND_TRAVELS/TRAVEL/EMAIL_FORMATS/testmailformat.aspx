<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testmailformat.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_EMAIL_FORMATS_testmailformat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            Dear Sir/Mam,</h2>
        <p>
            &nbsp;</p>
        <p>
            Please approve following travel statement in the TMS,</p>
        <br />
        <table style="width: 90%" border="1" cellpadding="10" cellspacing="0">
            <tr>
                <td style="width: 20%;">
                    Tour Sanction No.:
                </td>
                <td colspan="2">
                    {#sanctionno#}
                </td>
            </tr>
            <tr>
                <td>
                    Created By:
                </td>
                <td>
                    {#createdby#}
                </td>
                <td>
                    {#createdon#}
                </td>
            </tr>
            <tr>
                <td>
                    Remark:
                </td>
                <td colspan="2">
                    {#createdremarks#}
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        Please click on below link to approve above travel statement<br />
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Approve" 
            onclick="btnSubmit_Click" />
            <input id="Submit1" runat="server" type="submit" value="submit" />
        <br />
        <br />
        <br />
        <br />
        Regards
        <br />
        {#createdby#}
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
