<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFUserControl.ascx.cs"
    Inherits="WFUserControl" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register TagPrefix="TSP" Namespace="TSP" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<script type="text/javascript">
    function ShowWf() {
        TextDesc.SetText('');
        //chbIsSendMail.SetChecked(false);
        chbProfSending.SetChecked(false);
        chbIsSendSMS.SetChecked(false);
        txtSMSBody.SetText('');
        cmbSendBackTask.SetSelectedIndex(-1);
        //cmbPriority.SetSelectedIndex(-1);
        CallbackPanelWorkFlow.PerformCallback('FillcmbTask');
    }

    function SetVisible() {
        if (chbProfSending.GetChecked()) {
            PanelProfSending.SetVisible(true);
            PanelConfirmBtns.SetVisible(false);
            PanelSavebtn.SetVisible(true);
        }
        else {
            PanelProfSending.SetVisible(false);
            PanelConfirmBtns.SetVisible(true);
            PanelSavebtn.SetVisible(false);
        }
    }
</script>
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
    CalendarDayHeight="15" CalendarDayWidth="33" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
    FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
    WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
</pdc:PersianDateScriptManager>
<TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" AllowDragging="True" ClientInstanceName="PopupWorkFlow"
    CloseAction="CloseButton"
    HeaderText="گردش کار" RightToLeft="True"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    Width="535px">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <div dir="rtl">
                <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelWorkFlow" runat="server" ClientInstanceName="CallbackPanelWorkFlow"
                    OnCallback="CallbackPanelWorkFlow_Callback" Width="100%">
                    <ClientSideEvents EndCallback="function(s, e) {                    
                        if(s.cpIsPopUpClose==0)
                        {
                            if(CallbackPanelWorkFlow.cpWfName!=null)
                              PopupWorkFlow.SetHeaderText(CallbackPanelWorkFlow.cpWfName);
                            else
                              PopupWorkFlow.SetHeaderText('گردش کار');
	                        lblWfState.SetText(CallbackPanelWorkFlow.cpWfStateName);
	                        PopupWorkFlow.Show();
                            s.cpIsPopUpClose=1;
                        }
                        else
                        {                            
	                      //  popupWaitWorkFlow.Hide(); 
                            s.cpIsPopUpClose=1;
                        }

                        }" />
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldWF" ClientInstanceName="HiddenFieldWF">
                            </dxhf:ASPxHiddenField>
                            <%--                   <asp:ObjectDataSource ID="ObjdsWFTask" runat="server" SelectMethod="SelectSendBackTask"
                                                                                        TypeName="TSP.DataManager.WorkFlowStateManager" OldValuesParameterFormatString="original_{0}">
                                                                                        <SelectParameters>
                                                                                            <asp:Parameter DefaultValue="-2" Name="SendBackTask" Type="Int32" />
                                                                                            <asp:Parameter DefaultValue="-2" Name="WorkFlowId" Type="Int32" />
                                                                                            <asp:Parameter DefaultValue="-2" Name="OppositTaskId" Type="Int32" />
                                                                                        </SelectParameters>
                                                                                    </asp:ObjectDataSource>--%>
                            <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <dxe:ASPxLabel runat="server" Text="ASPxLabel" ID="lblError" ForeColor="Red" Visible="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" ID="lblWfState" ClientInstanceName="lblWfState">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 60px" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="دستورالعمل" Width="54px" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server"
                                                            Height="37px" ID="txtTaskDescription" ReadOnly="True" Width="436px"
                                                            ClientInstanceName="txtTaskDescription">
                                                            <ValidationSettings>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 60px" valign="top" align="right"></td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top" align="right" width="30%">
                                                                    <TSPControls:CustomASPxCheckBox runat="server" Text="ارسال پیشرفته"
                                                                        ID="chbProfSending" Wrap="False" ClientInstanceName="chbProfSending"
                                                                        RightToLeft="True">
                                                                        <ClientSideEvents CheckedChanged="function(s,e){SetVisible();}"></ClientSideEvents>
                                                                    </TSPControls:CustomASPxCheckBox>
                                                                </td>
                                                                <td valign="top" align="right" width="30%">
                                                                    <%--   <TSPControls:CustomASPxCheckBox runat="server" Text="ارسال  همزمان پیام" ID="chbIsSendMail" 
                                                                        ClientInstanceName="chbIsSendMail" >
                                                                    </TSPControls:CustomASPxCheckBox>--%>
                                                                    <TSPControls:CustomASPxCheckBox runat="server" ClientVisible="false" Text="ارسال  همزمان پیام کوتاه"
                                                                        ID="chbIsSendSMS" ClientInstanceName="chbIsSendSMS">
                                                                        <ClientSideEvents CheckedChanged="function(s,e){ 
                                                                        if (chbIsSendSMS.GetChecked())                                                                           
                                                                            PanelSMSBody.SetVisible(true);
                                                                        else       
                                                                            PanelSMSBody.SetVisible(false);   
                                                                               }"></ClientSideEvents>
                                                                    </TSPControls:CustomASPxCheckBox>
                                                                </td>
                                                                <td valign="top" align="right" width="40%"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 60px" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="فوریت" ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="185px" IncrementalFilteringMode="StartsWith"
                                                            RightToLeft="True" TextField="PName" ID="cmbPriority"
                                                            ValueType="System.String"
                                                            SelectedIndex="0" ClientInstanceName="cmbPriority"
                                                            EnableIncrementalFiltering="True" HorizontalAlign="Right">
                                                            <Items>
                                                                <dxcp:ListEditItem Text="عادی" Value="1" Selected="true" />
                                                                <dxcp:ListEditItem Text="فوری" Value="2" />
                                                                <dxcp:ListEditItem Text="آنی" Value="3" />

                                                            </Items>
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField IsRequired="True" ErrorText="فوریت انتخاب نشده است"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>

                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مهلت" ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="198px" ShowPickerOnTop="True" ID="txtExpireDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" colspan="4">
                                                        <dxp:ASPxPanel runat="server" ClientVisible="False" Width="100%" ID="PanelProfSending"
                                                            ClientInstanceName="PanelProfSending">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60px" valign="top" align="right">
                                                                                    <dxe:ASPxLabel runat="server" Text="مرحله" ID="lblSenBack">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td valign="top" align="right">
                                                                                    <TSPControls:CustomAspxComboBox runat="server" Width="436px" ID="cmbSendBackTask"
                                                                                        RightToLeft="True" ValueType="System.String"
                                                                                        ClientInstanceName="cmbSendBackTask">
                                                                                        <ValidationSettings ValidationGroup="ProfSendValid" ErrorTextPosition="Bottom">

                                                                                            <RequiredField IsRequired="True" ErrorText="مرحله را انتخاب نمایید"></RequiredField>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>
                                                                                        <ButtonStyle Width="13px">
                                                                                        </ButtonStyle>
                                                                                    </TSPControls:CustomAspxComboBox>

                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60px" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پانوشت" ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="150px" ID="txtDescription"
                                                            Width="436px" ClientInstanceName="TextDesc">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <TSPControls:CustomASPxCheckBox runat="server" Text="هیچ گونه پیامکی برای این پرونده ارسال نشود"
                                                            ID="CheckBoxDoNotSendSMS" Wrap="False" ClientInstanceName="CheckBoxDoNotSendSMS"
                                                            RightToLeft="True">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <p class="HelpUL">
                                                            نکته مهم:کاربر گرامی پیش از استفاده از دکمه ''تایید/پاس'' و یا ''مرجوع/رد'' نسبت به سطح دسترسی خود به مراحل قابل ارسال در هر مرحله از گردش کار آگاهی کامل داشته باشید تا دچار سردرگمی نشوید.درصورت عدم اطلاع از این مهم با واحد فناوری اطلاعات سازمان نظام مهندسی تماس حاصل نمایید.
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" colspan="4">
                                                        <dxp:ASPxPanel runat="server" ClientVisible="False" Width="100%" ID="ASPxPanel1"
                                                            ClientInstanceName="PanelSMSBody">
                                                            <PanelCollection>
                                                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                                                    <table width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60px" valign="top" align="right">
                                                                                    <dxe:ASPxLabel runat="server" Text="متن پیامک" ID="ASPxLabel1">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td valign="top" align="right" colspan="3">
                                                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtSMSBody" Width="436px"
                                                                                        ClientInstanceName="txtSMSBody">
                                                                                    </TSPControls:CustomASPXMemo>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <dxp:ASPxPanel runat="server" Width="100%" ID="PanelConfirmBtns" ClientInstanceName="PanelConfirmBtns">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <TSPControls:CustomAspxButton UseSubmitBehavior="false" runat="server" Text="&nbsp;&nbsp;تایید/پاس"
                                                                                        ID="btnSentNextStep"
                                                                                        AutoPostBack="False" Width="120px" ClientInstanceName="btnSentNextStep">
                                                                                        <ClientSideEvents Click="function(s,e){
CallbackPanelWorkFlow.PerformCallback('SendNextStep');
 gridName.PerformCallback('wf');
 }"></ClientSideEvents>
                                                                                        <Image Width="20px" Height="20px" Url="~/Images/WorkFlow_Next.png" />
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <TSPControls:CustomAspxButton UseSubmitBehavior="false" runat="server" Text="&nbsp;&nbsp;مرجوع/رد"
                                                                                        ID="btnSentBackStep"
                                                                                        AutoPostBack="False" Width="120px" ClientInstanceName="btnSentBackStep">
                                                                                        <ClientSideEvents Click="function(s,e){CallbackPanelWorkFlow.PerformCallback('SendPreStep');
 gridName.PerformCallback('wf');}"></ClientSideEvents>
                                                                                        <Image Width="20px" Height="20px" Url="~/Images/WorkFlow_Previous.png" />
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <TSPControls:CustomAspxButton UseSubmitBehavior="false" runat="server" Text="&nbsp;&nbsp;تایید کلی"
                                                                                        ID="btnConfirmAndEnd"
                                                                                        AutoPostBack="False" Width="120px" ClientInstanceName="btnConfirmAndEnd">
                                                                                        <ClientSideEvents Click="function(s,e){CallbackPanelWorkFlow.PerformCallback('EndConfirm'); gridName.PerformCallback('wf');}"></ClientSideEvents>
                                                                                        <Image Width="20px" Height="20px" Url="~/Images/WorkFlow_Accept.png" />
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <TSPControls:CustomAspxButton UseSubmitBehavior="false" runat="server" Text="&nbsp;&nbsp;رد کلی"
                                                                                        ID="btnRejectAndEnd"
                                                                                        AutoPostBack="False" Width="120px" ClientInstanceName="btnRejectAndEnd">
                                                                                        <ClientSideEvents Click="function(s,e){CallbackPanelWorkFlow.PerformCallback('EndReject'); gridName.PerformCallback('wf');}"></ClientSideEvents>
                                                                                        <Image Width="20px" Height="20px" Url="~/Images/WorkFlow_Reject.png" />
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                        <dxp:ASPxPanel runat="server" ClientVisible="False" Width="100%" ID="PanelSavebtn"
                                                            ClientInstanceName="PanelSavebtn">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;ذخیره"
                                                                        ID="btnSendNextWorkStep" AutoPostBack="False" UseSubmitBehavior="False"
                                                                        Width="120px" ClientInstanceName="btnSenNextStep">
                                                                        <ClientSideEvents Click="function(s, e) {	
if(!ASPxClientEdit.ValidateGroup(ProfSendValid))
	return;
CallbackPanelWorkFlow.PerformCallback('Send');
 gridName.PerformCallback('wf');
}"></ClientSideEvents>
                                                                        <Image Width="16px" Height="16px" Url="~/Images/WorkFlow_Save.png" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                            <dxp:ASPxPanel runat="server" Width="100%" ID="PanelSaveSuccessfully" ClientInstanceName="PanelSaveSuccessfully">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <div align="center">
                                            <br />
                                            <dxe:ASPxLabel runat="server" Text="عملیات مورد نظر با موفقیت انجام شد." ID="lblWFWarning"
                                                ForeColor="Red">
                                            </dxe:ASPxLabel>
                                            <br />
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" Text="خروج"
                                                ID="btnClose" AutoPostBack="False" UseSubmitBehavior="False"
                                                Width="93px" ClientInstanceName="btnSenNextStep">
                                                <ClientSideEvents Click="function(s, e) {		                                    
	                                    PopupWorkFlow.Hide();
                                        CallbackPanelWorkFlow.PerformCallback('DoNextTaskOfClosePopUP');
                                    }"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel>
            </div>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
    <HeaderStyle HorizontalAlign="Center" Wrap="False">
        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
    </HeaderStyle>
    <SizeGripImage Height="12px" Width="12px" />
    <CloseButtonImage Height="17px" Width="17px" />
</TSPControls:CustomASPxPopupControl>
<%--<TSPControls:CustomASPxPopupControl ID="popupWaitWorkFlow" runat="server" HeaderText="" EnableViewState="False"
    ClientInstanceName="popupWaitWorkFlow" AutoUpdatePosition="True" EnableAnimation="False"
    AllowDragging="false" AllowResize="false" BackColor="Transparent" PopupVerticalAlign="WindowCenter"
    PopupHorizontalAlign="WindowCenter" Modal="True" CloseAction="CloseButton" ShowHeader="False"
    ShowShadow="False">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl5420542" runat="server">
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <asp:Image ID="IMG1" ImageUrl="~/Image/indicator.gif" align="middle" runat="server" />
                لطفا صبر نمایید ...</div>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
    <ContentStyle>
        <Paddings Padding="0px" />
    </ContentStyle>
    <BackgroundImage ImageUrl="~/Images/UploadBg.png" Repeat="NoRepeat" HorizontalPosition="center"
        VerticalPosition="center" />
    <Border BorderWidth="0px" />
</TSPControls:CustomASPxPopupControl>--%>
