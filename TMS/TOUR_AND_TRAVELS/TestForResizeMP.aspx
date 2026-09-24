<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestForResizeMP.aspx.cs" Inherits="TOUR_AND_TRAVELS_TestForResizeMP" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Resizable Control</title>
<style type="text/css">
.handle
{
width:10px;
height:10px;
background-color:#aaccee;
}
.resizing
{
padding:0px;
border-style:solid;
border-width:1px;
border-color:#aaccee;
cursor:se-resize;
}
</style>
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager" />
        <div>
            <asp:Panel ID="Panel1" runat="server" Style="width: 300px; height: 200px;">
                <asp:Label ID="image1" runat="server" Text="ASP.NET AJAX offers you Resizable Extender Control. ResizableControl is an extender that attaches to any element on a web page and allows the user to resize that control with a handle that attaches to lower-right corner of the control. The resize handle lets the user resize the element as if it were a window.">
                </asp:Label>
        </asp:Panel>
        <ajaxToolkit:ResizableControlExtender ID="ResizableControlExtender1" runat="server" TargetControlID="Panel1" HandleCssClass="handle" ResizableCssClass="resizing" MaximumHeight="400" MaximumWidth="500" MinimumHeight="100" MinimumWidth="100" HandleOffsetX="5" HandleOffsetY="5" />
    </div>
</form>
</body>
</html>
