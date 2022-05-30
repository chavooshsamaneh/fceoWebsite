<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeFinancialStausInsert.aspx.cs" Inherits="Employee_OfficeRegister_OfficeFinancialStausInsert"
    Title="مشخصات وضعیت مالی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="BtnNew_Click">

                                <Image Url="~/Images/icons/new.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                CausesValidation="False"  ID="btnEdit" UseSubmitBehavior="False"
                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                <Image Url="~/Images/icons/edit.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnSave_Click">

                                <Image Url="~/Images/icons/save.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td  align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="btnBack_Click">

                                <Image Url="~/Images/icons/Back.png">
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
            <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table style="width: 100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع وضعیت مالی * " ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Name" ID="CmbName" EnableIncrementalFiltering="True" DataSourceID="ObjectDataSource1"
                                            ValueType="System.String" ValueField="OfdId"
                                            ClientInstanceName="CmbName" IncrementalFilteringMode="StartsWith">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                
                                                <RequiredField IsRequired="True" ErrorText="نوع وضعیت مالی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="Name" Caption="نام" ></dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="Value" Caption="ضریب"></dxe:ListBoxColumn>
                                            </Columns>
                                           
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	txtCmbName.SetText(CmbName.GetSelectedIndex());
	//alert(txtCmbName.GetText());
}" />
                                        </TSPControls:CustomAspxComboBox>
                                        <TSPControls:CustomTextBox ID="txtCmbName" runat="server" ClientInstanceName="txtCmbName" ClientVisible="False">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مبلغ(ریال) *" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server" ID="txtValue">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        <dxe:ASPxLabel runat="server" Text="حداقل مبلغ وارد شده نباید کمتر از 10000000 ریال باشد"
                                            ID="ASPxLabel5" ForeColor="Red">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" ID="txtDesc">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server"
                            Visible="False">
                            <PanelCollection>
                                <dxp:PanelContent>

                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="4">
                                                    <dxe:ASPxRadioButtonList runat="server" ID="rdbtnIsConfirm">
                                                        <Border BorderWidth="0px"></Border>
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="مورد تایید نمی باشد"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                        </Items>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="وضعیت را مشخص نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </dxe:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtMeetingId" >
                                                        <ValidationSettings>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="شماره جلسه"   ID="ASPxLabel6">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                                        ID="txtMeetingDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                        Style="text-align: right; direction: ltr;"></pdc:PersianDateTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ جلسه" ID="ASPxLabel7">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtGrade">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="مبلغ مورد قبول کارشناس را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="مبلغ(ریال)" ID="ASPxLabel9">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtViewPoint" W >
                                                        <ValidationSettings>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                                <td style="vertical-align: top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نظر کارشناسی"  ID="ASPxLabel8">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4" HeaderText="مدارک پیوست" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table runat="server" id="TblFile" dir="rtl" width="100%">
                            <tr runat="server" id="Tr1">
                                <td runat="server" id="Td1" style="vertical-align: top; text-align: right">
                                    <asp:Label runat="server" Text="فایل" ID="lblimg"></asp:Label>
                                </td>
                                <td runat="server" id="Td2" style="text-align: right">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                        ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                        <ClientSideEvents TextChanged="function (s,e) {
	var InputFile=s.GetText();
var extension = new Array();

extension[0] = &quot;.txt&quot;;
extension[1] = &quot;.html&quot;;
extension[1] = &quot;.htm&quot;;
extension[2] = &quot;.xml&quot;;
extension[3] = &quot;.rtf&quot;;
extension[4] = &quot;.wav&quot;;
extension[5] = &quot;.mid&quot;;
extension[6] = &quot;.gif&quot;;
extension[7] = &quot;.jpg&quot;;
extension[8] = &quot;.jpeg&quot;;
extension[9] = &quot;.png&quot;;
extension[10] = &quot;.bmp&quot;;
extension[11] = &quot;.avi&quot;;
extension[12] = &quot;.mpeg&quot;;
extension[13] = &quot;.mpg&quot;;
extension[14] = &quot;.zip&quot;;
extension[15] = &quot;.doc&quot;;
extension[16] = &quot;.docx&quot;;
extension[17] = &quot;.xls&quot;;
extension[18] = &quot;.xlsx&quot;;
extension[19] = &quot;.pdf&quot;;


var thisext = InputFile.substr(InputFile.lastIndexOf('.')).toLowerCase();
for(var i = 0; i &lt; extension.length; i++) 
   {
	   if(thisext == extension[i]) {flpc.Upload(); return; }
	}
alert(&quot;شما مجاز به آپلود این فایل نیستید&quot;);
s.ClearText();
}"
                                                            FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
}"></ClientSideEvents>
                                                        <ValidationSettings AllowedContentTypes="text/plain, text/html, text/xml, text/richtext, audio/wav, audio/mid, image/gif, image/pjpeg, image/png, image/bmp, video/avi, video/mpeg, application/pdf, application/x-zip-compressed, application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/xml, image/jpeg, image/x-png, image/x-xbitmap, application/zip"
                                                            FileDoesNotExistErrorText="فایل انتخابی وجود ندارد" NotAllowedContentTypeErrorText="شما مجاز به آپلود این نوع فایل نیستید"
                                                            GeneralErrorText="خطایی در بارگزاری فایل در سرور انجام گرفته است" MaxFileSizeErrorText="سایز فایل انتخابی از حداکثر مجاز (0 KB) بیشتر است">
                                                        </ValidationSettings>
                                                        <CancelButton Text="انصراف">
                                                        </CancelButton>
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
                                </td>
                            </tr>
                            <tr runat="server" id="Tr3">
                                <td runat="server" id="Td4" style="vertical-align: top; text-align: right">
                                    <asp:Label runat="server" Text="توضیحات" ID="Label10"></asp:Label>
                                </td>
                                <td runat="server" id="Td5" style="text-align: right">
                                    <TSPControls:CustomASPXMemo runat="server" ID="txtDescImg">
                                        <ValidationSettings>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr runat="server" id="Tr2">
                                <td runat="server" id="Td3" align="center" colspan="2">
                           
                                    <TSPControls:CustomAspxButton runat="server" Text="اضافه" CausesValidation="False"
                                        ID="btnAddFlp" UseSubmitBehavior="False"
                                        OnClick="btnAddFlp_Click">
                                    </TSPControls:CustomAspxButton>
                              
                                </td>
                            </tr>
                        </table>      <br />
                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                            ID="AspxGridFlp" KeyFieldName="Id" AutoGenerateColumns="False"
                            OnRowDeleting="AspxGridFlp_RowDeleting">

                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل"
                                    Name="FilePath">
                                    <DataItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                                    Name="Description">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="2" Caption=" " ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>  <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False"  ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="FinancialId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDJudgeId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDComboValue" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" FilterExpression="Type={0}"
                TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager" SelectMethod="GetData">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
