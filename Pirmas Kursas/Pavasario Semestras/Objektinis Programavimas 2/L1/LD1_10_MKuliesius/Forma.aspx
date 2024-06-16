<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma.aspx.cs" Inherits="LD1_10_MKuliesius.Forma" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="LD_10"></asp:Label>
            <br/>
            <br/>
            <asp:Label ID="Label2" runat="server" Text="Nuskaityti Duomenys:"></asp:Label>
            <br/>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="125px"></asp:TextBox>
            <br/>
            <asp:Button ID="Button1" runat="server" Text="Atlikti užduotį" OnClick="Button1_Click" />
            <br/>
            <br/>
            <asp:Label ID="Label3" runat="server" Text="Gauti rezultatai:"></asp:Label>
            <br/>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="125px"></asp:TextBox>
            <br/>
            <br/>
            <br/>
            <br/>
            <asp:Label ID="Label4" runat="server" Text="Martynas Kuliešius IFF-1/9"></asp:Label>
            <br/>
        </div>
    </form>
</body>
</html>
