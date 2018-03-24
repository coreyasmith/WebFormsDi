<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsDi.Default" %>
<%@ Register Src="~/Controls/DiUserControl.ascx" TagPrefix="uc1" TagName="DiUserControl" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Forms Dependency Injection</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Dependency Injection Page</h1>
        <ul>
            <li>Init: <asp:Label runat="server" id="InitLabel"></asp:Label></li>
            <li>Load: <asp:Label runat="server" id="LoadLabel"></asp:Label></li>
        </ul>
        <uc1:DiUserControl runat="server" id="DiUserControl" />
    </form>
</body>
</html>
