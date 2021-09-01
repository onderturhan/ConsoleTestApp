<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ConfigurationWebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" />
            <asp:Button ID="ButtonGet" runat="server" Text="Get" OnClick="ButtonGet_Click" />

            <asp:Panel ID="pnl" runat="server">

            </asp:Panel>
        </div>
    </form>
</body>
</html>
