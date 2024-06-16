<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma.aspx.cs" Inherits="LD5_10_MKuliesius.Forma" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="Style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label runat="server" Text="LD5_10 Martynas Kuliešius"></asp:Label>  <br /><br />

            <asp:Label runat="server" Text="Pasirinkite Užsakovų failus"></asp:Label> <br />
            <asp:FileUpload runat="server" ID="FileUpload1" AllowMultiple="True" BackColor="#CCFF99" BorderColor="Black" BorderStyle="Groove" Height="20px" Width="210px"></asp:FileUpload>  <br />
            <asp:Label runat="server" Text="Pasirinkite leidėjų failą"></asp:Label>  <br />
            <asp:FileUpload runat="server" ID="FileUpload2" BackColor="cyan" BorderColor="Black" BorderStyle="Groove" Height="20px " Width="210px"></asp:FileUpload> <br />
            
            <asp:Button runat="server" Text="Skaityti failus" OnClick="Button1_Click" ID="Button1" Font-Size="X-Large" Height="50px" Width="160px"></asp:Button> <br />
            <asp:Label runat="server" Text="Label" ID="Label1" Visible="false"></asp:Label>
            <br />
            <br />
            <asp:Button runat="server" Text="Rodyti mėnesio pajamas" ID="Button2" Width="170px" Visible="false" OnClick="Button2_Click"></asp:Button>          <asp:Button runat="server" Text="Surikiuoti leidėjų pajamas" ID="Button3" Visible="false" OnClick="Button3_Click"></asp:Button>          <asp:Button runat="server" Text="Nurodyto mėnesio prenumeratorių sąrašas" ID="Button4" Visible="false" OnClick="Button4_Click"></asp:Button>

            <br />
            <asp:DropDownList ID="DropDown1" runat="server" Visible="false">
                <asp:ListItem Text="Sausis" Value="1"></asp:ListItem>
                <asp:ListItem Text="Vasaris" Value="2"></asp:ListItem>
                <asp:ListItem Text="Kovas" Value="3"></asp:ListItem>
                <asp:ListItem Text="Balandis" Value="4"></asp:ListItem>
                <asp:ListItem Text="Gegužė" Value="5"></asp:ListItem>
                <asp:ListItem Text="Birželis" Value="6"></asp:ListItem>
                <asp:ListItem Text="Liepa" Value="7"></asp:ListItem>
                <asp:ListItem Text="Rugpjūtis" Value="8"></asp:ListItem>
                <asp:ListItem Text="Rugsėjis" Value="9"></asp:ListItem>
                <asp:ListItem Text="Spalis" Value="10"></asp:ListItem>
                <asp:ListItem Text="Lapkritis" Value="11"></asp:ListItem>
                <asp:ListItem Text="Gruodis" Value="12"></asp:ListItem>
            </asp:DropDownList><br />
            <br />
            <br />
            <asp:Table runat="server" ID="Table1" Visible="false"></asp:Table>



        </div>            
    </form>
</body>
</html>
