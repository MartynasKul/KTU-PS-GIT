<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma.aspx.cs" Inherits="LD4_10_MKuliesius.Forma" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #form1 {
            margin-left: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label runat="server" Text="LD4_10 Martynas Kuliešius"></asp:Label></div> <br/>
        <asp:FileUpload runat="server" ID="FileUpload1" AllowMultiple="True" BackColor="#CCFF99" BorderColor="Black" BorderStyle="Groove" Height="69px" Width="420px"></asp:FileUpload> <br/>
        <asp:Button runat="server" Text="Atlikti užduotį" OnClick="Button1_Click" ID="Button1" Font-Size="X-Large" Height="50px" Width="160px"></asp:Button> <br/><br/>
        
        <asp:Label runat="server" ID="Label1" Text="Label" Visible="false"></asp:Label> <br/>
        <asp:Table runat="server" ID="Table1" Visible="false"></asp:Table> <br/>

        <asp:Label runat="server" ID="Label2" Text="Label" Visible="false"></asp:Label> <br/>
        <asp:Table runat="server" ID="Table2" Visible="false"></asp:Table> <br/>

        <asp:Label runat="server" ID="Label3" Text="Label" Visible="false"></asp:Label> <br/>

        <asp:Label runat="server" ID="Label4" Text="Label" Visible="false"></asp:Label> <br/>

        <asp:Label runat="server" ID="Label5" Text="Label" Visible="false"></asp:Label> <br/>

    </form>
</body>
</html>
