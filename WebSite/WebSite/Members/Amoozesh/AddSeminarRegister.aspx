<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddSeminarRegister.aspx.cs" Inherits="Members_Amoozesh_SeminarRegister" Title="ثبت نام در سمینار" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton runat="server" Text=" " ToolTip="ذخیره" Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click" Visible="False">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>

                                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                            <image url="~/Images/icons/save.png"></image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton ID="btnFinish" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnFinish_Click" Text=" "
                                                            ToolTip="پرداخت" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
            </hoverstyle>
                                                            <image height="30px" url="~/Images/icons/Payment.png" width="30px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                            <image url="~/Images/icons/Back.png"></image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
              
                    <br />
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelSeminar" HeaderText="سمینار آموزشی" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; width: 107px;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="موضوع سمینار:" ID="ASPxLabel10"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtSubject" Width="192px">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <br />
                                            </td>
                                            <td align="right" colspan="3" style="vertical-align: top"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع:" ID="ASPxLabel18">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top;">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtStartDate">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پایان:" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top;">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtEndDate">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <br />
                                            </td>
                                            <td align="right" style="vertical-align: top;"></td>
                                            <td align="right" style="vertical-align: top; width: 107px;"></td>
                                            <td align="right" style="vertical-align: top;"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 107px;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="زمان برگزاری(ساعت):" Width="111px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtTime">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; width: 107px;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="مدت زمان برگزاری(ساعت):" ID="ASPxLabel29" Width="143px"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtDuration">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px">
                                                <br />
                                            </td>
                                            <td align="right" style="vertical-align: top"></td>
                                            <td align="right" style="vertical-align: top; width: 107px"></td>
                                            <td align="right" style="vertical-align: top"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px">
                                                <dxe:ASPxLabel runat="server" Text="ظرفیت سمینار:" Width="111px" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="lblCapacity">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top; width: 107px">
                                                <dxe:ASPxLabel runat="server" Text="ظرفیت باقیمانده:" ID="ASPxLabel4" Width="143px">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="lblRemainCapacity">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <br />
                                            </td>
                                            <td align="right" style="vertical-align: top;"></td>
                                            <td align="right" style="vertical-align: top; width: 107px;"></td>
                                            <td align="right" style="vertical-align: top;"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <dxe:ASPxLabel runat="server" Text="هزینه شرکت در سمینار:" Width="125px" ID="ASPxLabel19"></dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="vertical-align: top;">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtSeminarCost">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; width: 107px;" align="right"></td>
                                            <td style="vertical-align: top;" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px">
                                                <br />
                                            </td>
                                            <td align="right" style="vertical-align: top"></td>
                                            <td align="right" style="vertical-align: top; width: 107px"></td>
                                            <td align="right" style="vertical-align: top"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 107px;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="محل برگزاری:" ID="ASPxLabel7"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtPlace" Height="18px">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <br />
                                            </td>
                                            <td align="right" style="vertical-align: top;"></td>
                                            <td align="right" style="vertical-align: top; width: 107px;"></td>
                                            <td align="right" style="vertical-align: top;"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 107px;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="مطالب و سرفصل ها:" Width="106px" ID="ASPxLabel20"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtTopic">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px;">
                                                <br />
                                            </td>
                                            <td align="right" colspan="3" style="vertical-align: top"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 107px;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات:" Width="100px" ID="ASPxLabel15"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtDesc" Height="18px" Width="145px">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; width: 107px"></td>
                                            <td align="right" colspan="3" style="vertical-align: top"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                	<fieldset><legend class="HelpUL">مشخصات ثبت نام</legend>
                 
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; width: 102px; text-align: right">
                                                    <dxe:ASPxLabel runat="server" Text="نحوه پرداخت:" Width="80px" ID="ASPxLabel13"></dxe:ASPxLabel>
                                                </td>
                                                <td style="vertical-align: top; text-align: right" dir="ltr" colspan="3">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="145px" ID="cmbPaymentType" ValueType="System.String">
                                                        <Items>
                                                            <dxe:ListEditItem Value="نقدی" Text="نقدی"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="فیش بانکی" Text="فیش بانکی"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="کارت" Text="کارت"></dxe:ListEditItem>
                                                        </Items>

                                                        <ValidationSettings>
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                              </fieldset>
                    <br />
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton runat="server" Text=" " ToolTip="ذخیره" Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click" Visible="False">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>

                                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                            <image url="~/Images/icons/save.png"></image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton ID="btnFinish2" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnFinish_Click" Text=" "
                                                            ToolTip="پرداخت" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
            </hoverstyle>
                                                            <image height="30px" url="~/Images/icons/Payment.png" width="30px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                            <image url="~/Images/icons/Back.png"></image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                            </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldCourseRegister"></dxhf:ASPxHiddenField>
                                   
                <asp:ObjectDataSource ID="ObjdsPeriods" runat="server" FilterExpression="InsId={0}" SelectMethod="GetData" TypeName="TSP.DataManager.PeriodPresentManager">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
</asp:Content>
