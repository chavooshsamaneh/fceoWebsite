<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodEPayment.aspx.cs" Inherits="Members_Amoozesh_PeriodEPayment"
    Title="پرداخت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
//function contentPageLoad(sender, args)
//{
//    //if(PanelVisible.Get('Visible')==true)
//    if(ComboLoan.GetSelectedIndex()<0)
//        PanelLoanPayment.SetVisible(false);
//    //else
//        //PanelLoanPayment.SetVisible(true);
//}
function SetControlValues()
{
    grid.GetRowValues(grid.GetFocusedRowIndex(),'MeId;FirstName;LastName',SetValue);
}

function SetValue(values)
{
    ID.SetText(values[0]);
    mFirstName.SetText(values[1]);
    mLastName.SetText(values[2]);    
}   
    </script>
    
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="false">
                <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
                [<a class="closeLink" href="#">بستن</a>]</div>
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                    cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 5px">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پرداخت"
                                                                    CausesValidation="False" ID="btnFinish" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image Height="30px" Width="30px" Url="~/Images/icons/Payment.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
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
          	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2"  HeaderText="مشخصات پرداخت" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
     
                                <table>
                                    <tbody>
                                        <tr>
                                            
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نام دوره" ID="ASPxLabel3" ClientInstanceName="lblMe">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="399px" AutoPostBack="True"
                                                    ClientEnabled="False" ID="ASPxTextBoxPeriodTitle" ClientInstanceName="ID" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ برگزاری" Width="65px" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                               <td dir="rtl" align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" Enabled="False"
                                                    AutoPostBack="True" ShowPickerOnTop="True" ID="StartDate" PickerDirection="ToRight"
                                                    IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            </td>
                                            <td dir="ltr" align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeId" ClientInstanceName="lblMe">
                                                </dxe:ASPxLabel>
                                            </td>
                                             <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="145px" AutoPostBack="True"
                                                    ClientEnabled="False" ID="txtMeNo" ClientInstanceName="ID" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                                                                    
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نام" ID="lblMeFirstName" ClientInstanceName="lblMname">
                                                </dxe:ASPxLabel>
                                            </td>
                                             <td dir="rtl" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="145px" ClientEnabled="False"
                                                    ReadOnly="True" ID="txtMeFirstName" ClientInstanceName="mFirstName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td dir="ltr" align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblMeLastName" ClientInstanceName="lblMfamily">
                                                </dxe:ASPxLabel>
                                            </td>
                                             <td dir="rtl" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="145px" ClientEnabled="False"
                                                    ReadOnly="True" ID="txtMeLastName" ClientInstanceName="mLastName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>                                           
                                           
                                        </tr>
                                        <tr>
                                             <td dir="rtl" align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="مبلغ" Width="20px" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                               <td dir="rtl" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  EnableClientSideAPI="True" Width="145px"
                                                    ClientEnabled="False" TabIndex="1" ID="txtTotalAmount" >
                                                    <ValidationSettings Display="Dynamic">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پرداخت" Width="65px" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" Enabled="False"
                                                    AutoPostBack="True" ShowPickerOnTop="True" ID="PaymentDate" PickerDirection="ToRight"
                                                    IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            </td>                                                                                    
                                        </tr>
                                    </tbody>
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
                                                <td align="right">
                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldCourseRegister">
                                                    </dxhf:ASPxHiddenField>
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پرداخت"
                                                                        CausesValidation="False" ID="btnFinish2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image Height="30px" Width="30px" Url="~/Images/icons/Payment.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
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
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </ContentTemplate> </asp:UpdatePanel>            
            <asp:modalupdateprogress id="ModalUpdateProgress2" runat="server" displayafter="0"
                associatedupdatepanelid="UpdatePanel1" backgroundcssclass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <img id="IMG1" src="../../Image/indicator.gif" align="middle" />
                        لطفا صبر نمایید ...</div>
                </ProgressTemplate>
            </asp:modalupdateprogress>
            <asp:objectdatasource id="ObjectDataSourceProject" runat="server"
               selectmethod="GetData" typename="TSP.DataManager.AccountingProjectManager"
             >

            </asp:objectdatasource>
            <asp:objectdatasource id="ObjectDataSourceCC" runat="server" selectmethod="GetData"
                typename="TSP.DataManager.AccountingCostCenterManager"></asp:objectdatasource>
            <asp:hiddenfield id="HFAmount" runat="server" visible="False" />
            <asp:hiddenfield id="HFRegisterType" runat="server" visible="False" />
            <input id="merchantId" name="merchantId" type="hidden" value="<%= TSP.Utility.OnlinePayment.GetNezamMerchantId() %>" />
            <input id="paymentId" name="paymentId" type="hidden" value="<%=  Session["MePaymentId"]%>" />
            <input id="amount" name="amount" type="hidden" value='<%= (Session["MePrice"]!=null)?Convert.ToInt64(Decimal.Parse(Session["MePrice"].ToString())):-1%>' />
            <input id="revertURL" name="revertURL" type="hidden" value="<%= this.Request.Url.AbsoluteUri %>" />
            <input id="customerId" name="customerId" type="hidden" />
</asp:Content>
