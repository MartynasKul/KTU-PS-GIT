<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma.aspx.cs" Inherits="LD2_10_MKuliesius.Forma" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Label ID="Label1" runat="server" CssClass="MainLabelTop" Font-Bold="True" Font-Size="XX-Large" Text="LD2 10"></asp:Label>
            </div>
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Darbuotojai: "></asp:Label>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Detales: "></asp:Label>
            <asp:FileUpload ID="FileUpload2" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Skaityti" CausesValidation="False" BackColor="#99FF99" />
            <br />
            <asp:Label ID="Label4" runat="server"></asp:Label>
            <br />
            <br />


            <asp:Label ID="Label5" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="200px" Width="800px"></asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="800px" Height="150px"></asp:TextBox>
            <br />
            <asp:Label ID="Label9" runat="server"></asp:Label>
            <br />

            <br />
            <asp:Label ID="Label7" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Height="150px" Width="800px"></asp:TextBox>
            <br />
                       



            <br />
            
            <asp:Label ID="Label8" runat="server"></asp:Label>
            <asp:TextBox ID="TextBox5" runat="server" Visible="True"></asp:TextBox>

            <br />
            <asp:Button ID="Button2" runat="server" Text="Atrinkti" Visible="True" OnClick="Button2_Click" BackColor="#99FF99" ForeColor="Black" />
            <br />
            <br />
            <br />
            
            <asp:Label ID="Label10" runat="server"> Iveskite kieki</asp:Label>
            <br />
            <asp:TextBox ID="TextBox4" runat="server" Visible="True"></asp:TextBox>
            <br />
            <asp:Label ID="Label11" runat="server">Iveskite ikaini</asp:Label>
            <br />
            <asp:TextBox ID="TextBox6" runat="server" Visible="True"></asp:TextBox>

            <br />
            <asp:Button ID="Button3" runat="server" Text="Atrinkti" Visible="True" OnClick="Button3_Click" BackColor="#99FF99" ForeColor="Black" />
            <br />
            <br />
            <br />
         </div>
        </form>
    </body>
