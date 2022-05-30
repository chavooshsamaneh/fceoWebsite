<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBAdmin.aspx.cs" Inherits="DBAdmin_DBAdmin"
    Title="مدیریت دیتابیس" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 5px 0px 0px 0px; height: 35px; font-size: 20px; font-family: tahoma;
        color: #FFFFFF; background-color: #00CC66; vertical-align: top;" align="center"
        dir="rtl">
        <asp:Label ID="lblTitle" runat="server" Text="مدیریت DataBase"></asp:Label>
    </div>
    <br />
    <div align="left" dir="ltr" style="vertical-align: top">
        <%--<table width="100%">
                <tr>
                    <td width="50%" align="center">
                        <asp:RadioButton ID="rdbQuery" Checked="True" GroupName="Query" Text="Query ( Select )"
                            CausesValidation="False" runat="server" />
                    </td>
                    <td width="50%" align="center">
                        <asp:RadioButton ID="rdbCommand" runat="server" GroupName="Query" CausesValidation="False"
                            Text="Command ( Insert, Update , Delete, ...)" />
                    </td>
                </tr>
            </table>--%>
        <b>Query:</b>
        <br />
        <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Height="300px" Width="100%"></asp:TextBox>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnExecute" runat="server" Text="Execute" Height="40px" Width="150px"
                OnClick="btnExecute_Click" /></div>
        <%--<br />
                        <br />
                        <asp:TextBox ID="txtCommand" runat="server" TextMode="MultiLine" Height="300px" Width="400px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnCommand" runat="server" Text="Execute" Enabled="False" Height="40px"
                            Width="100px" OnClick="btnCommand_Click" />--%>
        <%--</td>
                </tr>
            </table>--%>
    </div>
    <br />
    <hr />
    <h1 align="center" dir="ltr">
        <asp:Label ID="lblResult" runat="server" Text="Result:" Visible="False"></asp:Label></h1>
    <div align="center" dir="ltr">
        <asp:Label ID="lblError" runat="server" Text="Error!" ForeColor="Red" Visible="False"
            Font-Size="Large"></asp:Label>
    </div>
    <div align="center" dir="ltr">
        <asp:Label ID="lblCommandSuccessfull" runat="server" Font-Size="Large" BorderColor="#003300"
            Width="400px" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006600" Font-Names="Arial"
            Text="Command(s) completed successfully." Visible="False"></asp:Label>
    </div>
    <br />
    <table width="100%">
        <tr>
            <td align="left">
                <asp:GridView ID="GridView1" runat="server" BorderWidth="1px" BorderStyle="Solid"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" Visible="False">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="GridView2" runat="server" BorderWidth="1px" BorderStyle="Solid"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" Visible="False">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="GridView3" runat="server" BorderWidth="1px" BorderStyle="Solid"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" Visible="False">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
