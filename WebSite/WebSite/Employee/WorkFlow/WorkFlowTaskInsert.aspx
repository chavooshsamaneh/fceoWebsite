<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowTaskInsert.aspx.cs" Inherits="Employee_WorkFlow_WorkFlowTaskInsert"
    Title="مشخصات عملیات گردش کار" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" style="width: 100%" dir="ltr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%" align="center">
                    <div dir="ltr">
                        <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                            [<a class="closeLink" href="#">بستن</a>]</div>
                                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>


                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click" Height="25px">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
}
"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnSave_Click" >
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click" Height="25px"
                                                            Width="25px">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                  	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTask" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="گردش کار">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <TSPControls:CustomTextBox ID="txtWfName" runat="server" 
                                                         ReadOnly="True" 
                                                        Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="عنوان عملیات">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <TSPControls:CustomTextBox ID="txtTaskName" runat="server" 
                                                          Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField ErrorText="عنوان عملیات را وارد نمایید" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="اولویت">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <TSPControls:CustomTextBox ID="txtTaskOrder" runat="server" ClientInstanceName="txtTaskOrder"
                                                          
                                                        Width="170px">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField ErrorText="اولویت را وارد نمایید" IsRequired="True" />
                                                            <RegularExpression ErrorText="اولویت عدد صحیح می باشد" ValidationExpression="\d*" />
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="دستورالعمل">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <TSPControls:CustomASPXMemo ID="txtDescription" runat="server" 
                                                         Height="37px" 
                                                        Width="100%">
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <TSPControls:CustomASPxCheckBox runat="server" ID="chkSendSMS" ClientInstanceName="chkSendSMS"
                                                        Text="امکان اننخاب گزینه ارسال همزمان پیامک  توسط کاربران ،هنگام خروج از این مرحله از گردش کار به صاحب پرونده وجود داشته باشد"
                                                          >
                                                    </TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <br />
                                                    <ul class="HelpUL"><li>هنگام ورود به این مرحله از گردش کار، پیامکی با متن وارد شده در این قسمت به صاحب پرونده ارسال می گردد</li></ul>                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblSMSBody" ClientInstanceName="lblSMSBody" runat="server" Text="متن پیامک">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" colspan="3" valign="top">
                                                    <TSPControls:CustomASPXMemo ID="txtSMSBody" ClientInstanceName="txtSMSBody" runat="server" 
                                                         Height="37px" 
                                                        Width="100%">
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    <br />
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
}
"></ClientSideEvents>
                                                           
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" Width="25px" OnClick="btnSave_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>
                                                           
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click" Width="25px">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>
                                                          
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTask" ClientInstanceName="HiddenFieldTask">
                                        </dxhf:ASPxHiddenField>
                                  
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
