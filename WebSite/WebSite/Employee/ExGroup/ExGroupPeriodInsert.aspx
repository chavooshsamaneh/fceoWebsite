<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ExGroupPeriodInsert.aspx.cs" Inherits="Employee_ExGroup_ExGroupPeriodInsert"
    Title="مشخصات دوره ها" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                function CheckDate() {
                    var StartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
                    var EndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
                    if (EndDate < StartDate && EndDate != "") {
                        lblErrorDate.SetVisible(true);
                        return false;
                    }
                    else {
                        lblErrorDate.SetVisible(false);
                        return true;
                    }

                    var StartDatePropagation = document.getElementById('<%=txtStartPropagation.ClientID%>').value;
                    var EndDatePropagation = document.getElementById('<%=txtEndPropagation.ClientID%>').value;
                    if (EndDatePropagation < StartDatePropagation && EndDatePropagation != "") {
                        lblErrorPropagationDate.SetVisible(true);
                        return false;
                    }
                    else {
                        lblErrorPropagationDate.SetVisible(false);
                        return true;
                    }

                }            
            </script>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>
          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table >
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image  Url="~/Images/icons/new.png" >
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <Image  Url="~/Images/icons/edit.png" >
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click">
                                            <ClientSideEvents Click="function(s, e) {
                                            e.processOnServer=CheckDate();
                                            }" />
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" PostBackUrl="ExGroupPeriod.aspx">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
     
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  
                    SeparatorWidth="1px" SeparatorHeight="100%" SeparatorColor="#A5A6A8" AutoPostBack="true"  OnItemClick="ASPxMenu1_ItemClick"
                    ItemSpacing="0px"  AutoSeparators="RootOnly" RightToLeft="True">
                    
                    <Items>
                        <dxm:MenuItem Name="Period" Selected="true" Text="مشخصات دوره">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Candid" Text="نامزدها">
                        </dxm:MenuItem>
                    </Items>
              
                </TSPControls:CustomAspxMenuHorizontal>    
          
                <br />
            	<TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="جدید" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

         
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="گروه/کمیته*" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <cc1:ComboBox ID="cmbExGroup" runat="server" 
                                                 DataSourceID="ObjectDataSourceExGroup" TextField="ExGroupName"
                                                ValueField="ExGroupId" EnableIncrementalFiltering="True" HorizontalAlign="Right"
                                                 IncrementalFilteringMode="StartsWith" Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="true" ErrorText="گروه تخصصی را وارد نمایید" />
                                                </ValidationSettings>
                                            </cc1:ComboBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="نام دوره" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName"  Width="100%" ClientInstanceName="txtName"
                                                >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ شروع تبلیغات*" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ID="txtStartPropagation"
                                                PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                           <%-- <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                ErrorMessage="تاریخ شروع را وارد نمایید" ControlToValidate="txtStartPropagation"
                                                ID="PersianDateValidator2">تاریخ شروع را وارد نمایید</pdc:PersianDateValidator>--%>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ پایان تبلیغات*">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ID="txtEndPropagation"
                                                PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                          <%--  <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ پایان را وارد نمایید"
                                                ControlToValidate="txtEndPropagation" ForeColor="Red" ID="RequiredFieldValidator4"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <dx:ASPxLabel ID="lblErrorPropagationDate" ClientInstanceName="lblErrorPropagationDate"
                                                ClientVisible="false" runat="server" Font-Size="12px" ForeColor="Red" Text="تاریخ شروع از تاریخ پایان بزرگتر است">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ شروع*" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ID="txtStartDate"
                                                PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ شروع را وارد نمایید"
                                                ControlToValidate="txtStartDate" ForeColor="Red" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ پایان*">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td  valign="top" width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ID="txtEndDate"
                                                PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ پایان را وارد نمایید"
                                                ControlToValidate="txtEndDate" ForeColor="Red" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <dx:ASPxLabel ID="lblErrorDate" ClientInstanceName="lblErrorDate" ClientVisible="false"
                                                runat="server" Font-Size="12px" ForeColor="Red" Text="تاریخ شروع از تاریخ پایان بزرگتر است">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="توضیحات" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" 
                                                Height="37px" ID="MemoDescription"  Width="100%" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر" ID="Label9"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td dir="rtl" style="width: 80%">
                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpcAttachment" InputType="Images"
                                                                ClientInstanceName="flpcAttachment" OnFileUploadComplete="flpcAttachment_FileUploadComplete"
                                                                MaxSizeForUploadFile="3000000" UploadWhenFileChoosed="true" Width="100%">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
imgEndAttachment.SetVisible(true);
	AttachmentImage.SetImageUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndAttachment.SetVisible(false); 
	AttachmentImage.SetImageUrl('');
 }                                     
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <dxe:ASPxImage runat="server" ID="imgEndAttachment" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                                ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndAttachment">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top" colspan="2">
                                                            <asp:Label runat="server" Text="اندازه تصویر انتخابی بایستی تناسبی از 309*1200 باشد" ID="Label1"
                                                                ForeColor="Red" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxImage runat="server" ID="AttachmentImage" ClientInstanceName="AttachmentImage"
                                                ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/noImage.gif" Height="160px" Width="225px">
                                                <EmptyImage Height="160px" Width="225px" Url="~/Images/noImage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                         
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPxCheckBox ID="chkIsGrouping" Text="نامزدهای تشکل در پرتال مهمان بر اساس هفت رشته اصلی دسته بندی شوند" runat="server">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                      </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <br />
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                            <table cellpadding="0">
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image  Url="~/Images/icons/new.png" >
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton   IsMenuButton="true" ID="btnedit2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <Image  Url="~/Images/icons/edit.png" >
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click">
                                            <ClientSideEvents Click="function(s, e) {
                                            e.processOnServer=CheckDate();
                                            }" />
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" PostBackUrl="ExGroupPeriod.aspx">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                            
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <img alt="" id="IMG2" src="../../Image/indicator.gif" align="middle" />
                        لطفا صبر نمایید ...</div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:ObjectDataSource ID="ObjectDataSourceExGroup" runat="server" OldValuesParameterFormatString="original_{0}"
                TypeName="TSP.DataManager.ExGroupManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldModeID" runat="server">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
