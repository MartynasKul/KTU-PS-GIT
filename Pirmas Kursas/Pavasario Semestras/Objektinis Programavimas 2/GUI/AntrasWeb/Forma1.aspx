<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="AntrasWeb.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="Label1" Text="Kūgio aukštinė, H"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="Textbox1">0</asp:TextBox>
            <br/>
            <asp:Label runat="server" ID="Label2" Text="Kūgio spindulys, R"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="Textbox2">0</asp:TextBox>
            <br/>
            <asp:Label runat="server" ID="Label3" Text="Kūgio spindulys, R"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="Textbox3">0</asp:TextBox>
            <br/>
            <asp:Label runat="server" ID="Label4" Text="Kūgio tūris"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="Textbox4">0</asp:TextBox>
            <br/>
            <br/>
            <asp:Button ID="Button1" runat="server" Text="Skaičiuoti!" OnClick="Button1_Click"></asp:Button>

        </div>
    </form>
</body>
</html>
