<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OtherPersonInsert.aspx.cs" Inherits="Employee_TechniciansManagement_OtherPersonInsert"
    Title="مشخصات شخص" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnNew1_Click">
                                                           
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                CausesValidation="False" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False"  OnClick="btnEdit2_Click">
                                                            
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnInsert" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                ClientInstanceName="save" OnClick="btnInsert_Click">
                                                      
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                       
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

                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

        
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFirstName"  EnableClientSideAPI="True"
                                                     MaxLength="30" ClientInstanceName="TextFirstName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                      
                                                        <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="text-align: right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtLastName"  EnableClientSideAPI="True"
                                                     MaxLength="50" ClientInstanceName="TextLastName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                 
                                                        <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام پدر" Width="56px" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFatherName"  EnableClientSideAPI="True"
                                                     MaxLength="30" ClientInstanceName="TextFatherName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                      
                                                        <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="95px" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtIdNo"  EnableClientSideAPI="True"
                                                     MaxLength="10" ClientInstanceName="TextIdNo" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                               
                                                        <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}">
                                                        </RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel6">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="text-align: right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtSSN"  EnableClientSideAPI="True"
                                                     MaxLength="10" ClientInstanceName="TextSSN" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                  
                                                        <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ تولد" ID="ASPxLabel7">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate=""  Width="230px" ShowPickerOnTop="True"
                                                    ID="txtBirthDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="محل تولد" ID="ASPxLabel8">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="text-align: right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtBirthPlace"  EnableClientSideAPI="True"
                                                     ClientInstanceName="TextBirthPlace" >
                                                    <ValidationSettings>
                                        
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تلفن" ID="ASPxLabel9">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtTel"  EnableClientSideAPI="True"
                                                     ClientInstanceName="TextTel" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                
                                                        <RegularExpression ErrorText="تلفن را با پیش شماره وارد نمایید" ValidationExpression="0\d{8,12}">
                                                        </RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="شماره همراه" Width="81px" ID="ASPxLabel10">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="text-align: right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtMobileNo"  EnableClientSideAPI="True"
                                                     MaxLength="11" ClientInstanceName="TextMobileNo" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                  
                                                        <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="0\d{1,10}">
                                                        </RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel11">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="29px" ID="txtAddress"  EnableClientSideAPI="True"
                                                   ClientInstanceName="TextAddress" >
                                                   
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="29px" ID="txtDesc"  EnableClientSideAPI="True"
                                                    ClientInstanceName="TextDesc" >
                                                 
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text=" تصویر" ID="ASPxLabel13">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                                    ID="flpImg" InputType="Images" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                                    <ClientSideEvents  FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
	Img.SetVisible(true);

	Img.SetImageUrl('../../Image/Temp/'+e.callbackData);
}"></ClientSideEvents>
                                                                    
                                                                </TSPControls:CustomAspxUploadControl>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxImage runat="server" Height="75px" EnableClientSideAPI="True" Width="75px"
                                                    ID="Img1" ClientInstanceName="Img" __designer:wfdid="w1">
                                                    <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                                    <EmptyImage Url="~/Images/Person.png">
                                                    </EmptyImage>
                                                </dxe:ASPxImage>
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
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                CausesValidation="False" ID="btnNew1" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnNew1_Click">
                                                                <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                           
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                CausesValidation="False" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False"  OnClick="btnEdit2_Click">
                                                             
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnInsert1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                ClientInstanceName="save1" OnClick="btnInsert_Click">
                                                          
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="ASPxButton5" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                          
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <asp:HiddenField ID="OtherPersonId" runat="server" Visible="False" />
    </div>
</asp:Content>
