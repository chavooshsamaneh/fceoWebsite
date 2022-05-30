<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="Users_ResetPassword" Title="بازیابی رمز عبور" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divcontent" style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangePassword" runat="server"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="بازیابی رمز عبور"
                                                UseSubmitBehavior="False" AutoPostBack="true" OnClick="btnChangePassword_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/ChangePassword.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" EnableTheming="False"
                                                EnableViewState="False" Text=" " ToolTip="چاپ اطلاعات کاربری" UseSubmitBehavior="False"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){
		   grid.PerformCallback('Print;'+HiddenUserData.Get('UId')+';'+HiddenUserData.Get('P'));
                                                                 }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" EnableTheming="False"
                                                EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False"
                                                Width="25px">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                    <ul class="HelpUL">
                        <li>با بازیابی رمز عبور در صورت ثبت  بودن شماره همراه معتبر در سیستم نام کاربری و رمزعبور برای کاربر پیامک می شود</li>
                    </ul>
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelResetPassword" HeaderText="" runat="server"
                    ClientInstanceName="RoundPanelRequest" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="grid"  
                                DataSourceID="ObjectDataSourceResetPass" EnableViewState="False" KeyFieldName="RpId"
                                OnCustomCallback="CustomAspxDevGridView1_CustomCallback">
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="RequestDate" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="RequestType" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>

                                    <dxwgv:GridViewDataTextColumn Caption="ثبت کننده درخواست" FieldName="ChangerName" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>

                                </Columns>
                                <ClientSideEvents EndCallback="function(s,e)
                                {
                                if(s.cpPrint == 1)
{
    s.cpPrint = 0;
    window.open(s.cpPrintPath);
}
                                }" />
                            </TSPControls:CustomAspxDevGridView>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangePassword2" runat="server"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="بازیابی رمز عبور"
                                                UseSubmitBehavior="False" AutoPostBack="true" OnClick="btnChangePassword_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/ChangePassword.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" EnableTheming="False"
                                                EnableViewState="False" Text=" " ToolTip="چاپ اطلاعات کاربری" UseSubmitBehavior="False"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){
		   grid.PerformCallback('Print;'+HiddenUserData.Get('UId')+';'+HiddenUserData.Get('P'));
                                                                 }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" EnableTheming="False"
                                                EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False"
                                                Width="25px">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjectDataSourceResetPass" runat="server" SelectMethod="SelectByType"
                    TypeName="TSP.DataManager.ResetPasswordManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="Type" Type="Byte" />
                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="HDType" runat="server" Visible="False" />
                <asp:HiddenField ID="HDId" runat="server" Visible="False" />
                <dx:ASPxHiddenField ID="HiddenUserData" runat="server" ClientInstanceName="HiddenUserData">
                </dx:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
