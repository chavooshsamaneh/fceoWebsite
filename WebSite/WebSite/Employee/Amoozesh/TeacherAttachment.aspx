<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherAttachment.aspx.cs" Inherits="Employee_Amoozesh_TeacherAttachment"
    Title="مستندات استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
            [<a class="closeLink" href="#">بستن</a>]</div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tr>
                                        <td style="vertical-align: top; width: 100%;">
                                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" AutoPostBack="False" 
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
flp.IsValid=true;	
PopupAttachment.Show();
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                            ToolTip="حذف" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                         </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu> 
               
                <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server" AutoSeparators="RootOnly" 
                     Enabled="False"  ItemSpacing="0px"
                    OnItemClick="MenuTeacherInfo_ItemClick" SeparatorColor="#A5A6A8" SeparatorHeight="100%"
                    SeparatorWidth="1px" RightToLeft="True">
                    <Items>
                        <dxm:MenuItem Name="BasicInfo" Text="مشخصات فردی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Job" Text="سوابق آموزشی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Research" Text="تالیفات و تحقیقات">
                        </dxm:MenuItem>
                         <dxm:MenuItem Name="Attachment" Text="مستندات" Selected="True">
                        </dxm:MenuItem>
                    </Items>
                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-2" LastItemX="-1" LastItemY="-2"
                        X="-1" Y="-2" />
                    <Border BorderColor="#A5A6A8" BorderStyle="Solid" BorderWidth="1px" />
                    <VerticalPopOutImage Height="8px" Width="4px" />
                    <ItemStyle HorizontalAlign="Right" ImageSpacing="5px" PopOutImageSpacing="7px" VerticalAlign="Middle">
                        <CheckedStyle BackColor="#80FFFF" ForeColor="#400040">
                        </CheckedStyle>
                    </ItemStyle>
                    <SubMenuItemStyle ImageSpacing="7px">
                    </SubMenuItemStyle>
                    <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1" />
                    <HorizontalPopOutImage Height="7px" Width="7px" />
                </TSPControls:CustomAspxMenuHorizontal>
    
            <TSPControls:CustomASPxPopupControl ID="PopupAttachment" runat="server" ClientInstanceName="PopupAttachment"
                  HeaderText="جدید"
                Height="23px"  PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowPageScrollbarWhenModal="True" Width="402px"
                CloseAction="CloseButton" Modal="True">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table style="text-align: right" width="100%">
                            <tr>
                                <td style="vertical-align: top;" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="سند">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top;" align="right">
                                    <TSPControls:CustomAspxUploadControl ID="flp" runat="server" MaxSizeForUploadFile="100000"
                                        ShowProgressPanel="True" Width="328px" ClientInstanceName="flp" InputType="Files">
                                        <ValidationSettings FileDoesNotExistErrorText="فایل انتخابی وجود ندارد" GeneralErrorText="خطایی در بارگزاری فایل در سرور انجام گرفته است"
                                            MaxFileSize="100000" MaxFileSizeErrorText="سایز فایل انتخابی از حداکثر مجاز (100 KB) بیشتر است"
                                            NotAllowedContentTypeErrorText="شما مجاز به آپلود این نوع فایل نیستید">
                                        </ValidationSettings>
                                        <CancelButton Text="انصراف">
                                        </CancelButton>
                                        <ClientSideEvents TextChanged="function (s,e) {
	var InputFile=s.GetText();
var extension = new Array();

extension[0] = &quot;.png&quot;;
extension[1] = &quot;.gif&quot;;
extension[2] = &quot;.jpg&quot;;
extension[3] = &quot;.jpeg&quot;;
extension[4] = &quot;.zip&quot;;
extension[5] = &quot;.rar&quot;;
extension[6] = &quot;.doc&quot;;
extension[7] = &quot;.docx&quot;;
extension[8] = &quot;.xls&quot;;
extension[9] = &quot;.xlsx&quot;;
extension[10] = &quot;.txt&quot;;
extension[11] = &quot;.psd&quot;;
extension[12] = &quot;.ico&quot;;
extension[13] = &quot;.bmp&quot;;
extension[14] = &quot;.pdf&quot;;


var thisext = InputFile.substr(InputFile.lastIndexOf('.')).toLowerCase();
for(var i = 0; i &lt; extension.length; i++) 
   {
	   if(thisext == extension[i]) { return; }
	}
alert(&quot;شما مجاز به آپلود این فایل نیستید&quot;);
s.ClearText();
}" />
                                    </TSPControls:CustomAspxUploadControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="توضیحات">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top;" align="right">
                                    <TSPControls:CustomASPXMemo ID="txtDescription" runat="server" Height="46px" Width="330px" 
                                        >
                                        <ValidationSettings>
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <br />
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" 
                                         OnClick="btnSave_Click" Text="ذخیره" Width="79px" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	PopupAttachment.Hide();
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px" />
                <CloseButtonImage Height="17px" Width="17px" />
            </TSPControls:CustomASPxPopupControl>
            <br />
         <TSPControls:CustomASPxRoundPanel ID="RoundPanelAttachment" HeaderText="مستندات" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

       
                                <TSPControls:CustomAspxDevGridView ID="GridViewAttachment" runat="server" AutoGenerateColumns="False"
                                      Width="100%"
                                    KeyFieldName="AttachId" ClientInstanceName="GridViewAttachment" RightToLeft="True">
                                    
                                    <Columns>
                                        <dxwgv:GridViewDataImageColumn Caption="مستندات" FieldName="FilePath" VisibleIndex="0"
                                            Visible="False">
                                            <propertiesimage imageheight="24px" imagewidth="24px"></propertiesimage>
                                        </dxwgv:GridViewDataImageColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="فایل" FieldName="FilePath" Name="FilePath"
                                            VisibleIndex="0">
                                            <dataitemtemplate>
<dxe:ASPxHyperLink id="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" __designer:wfdid="w102" OnDataBinding="ASPxHyperLink1_DataBinding" NavigateUrl='<%# Bind("FilePath") %>' Target="_blank"></dxe:ASPxHyperLink>
</dataitemtemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" VisibleIndex="1" FieldName="Description">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="3" ShowClearFilterButton="true">
                                            
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                    
                                </TSPControls:CustomAspxDevGridView>
                             </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
          
            <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <dxhf:ASPxHiddenField ID="HiddenFieldTeacherAttach" runat="server">
                                        </dxhf:ASPxHiddenField>
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	PopupAttachment.Show();
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                        ToolTip="حذف" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                          </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <asp:objectdatasource id="ObjdsAttachment" runat="server"></asp:objectdatasource>
       
</asp:Content>
