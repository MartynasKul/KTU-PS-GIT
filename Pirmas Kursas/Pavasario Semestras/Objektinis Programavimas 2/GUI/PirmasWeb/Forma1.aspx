<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="PirmasWeb.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pirmas Puslapis</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:Label runat="server" ForeColor="White" ID="Label1" BackColor="Black"></asp:Label>
            <br />

            <asp:TextBox runat="server" ID="Textbox1" ></asp:TextBox>
            <br />

            
            <asp:Button runat="server" Text="Gerai!" OnClick="Button1_Click"></asp:Button>
           
            <br />
        </div>
    </form>
</body>
</html>
