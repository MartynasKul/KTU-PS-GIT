<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="SesijaWeb.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" Text="Naujas įrašas" ID="Label1"></asp:Label>
            <br/>
            <asp:TextBox runat="server" Width="300px" ID="Textbox1"></asp:TextBox>
            <br/>
            <br/>
            <asp:Button runat="server" Text="Įvesti" ID="Button1" OnClick="Button1_Click"></asp:Button>
            <br/>
            <br/>
            <br/>
            <asp:Label runat="server" Text="Egzistuojantys įrašai:" ID="Label2"></asp:Label>
            <br/>
            <asp:Table runat="server" ID="Table1" BorderColor="Black" BorderWidth="1px" Width="300px"></asp:Table>
            <br/>
        </div>
    </form>
</body>
</html>
