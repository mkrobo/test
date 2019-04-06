<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee030.aspx.cs" Inherits="Empy030CSV.TestCSVOutput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>引き合い検索CSV</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>CSV出力テスト</h1>
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="DataGridView1" runat="server" AllowCustomPaging="True" BorderStyle="None" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="True" Enabled="False">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <asp:Button ID="CSVOutput" runat="server" OnClick="CSVOutput_Click" Text="CSV保存" />
            <br />
        </div>
    </form>
</body>
</html>
