<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddLink.aspx.cs" Inherits="Employee_HomePage_AddLink"
    Title="مشخصات لینک" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                    
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                    ToolTip="ویرایش"  UseSubmitBehavior="False">
                                                   
                                                    <Image  Url="~/Images/icons/edit.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                   
                                                    <Image  Url="~/Images/icons/save.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table width="100%">
                            <tr>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="نام لینک *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 45%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtLiName" runat="server" 
                                        >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RequiredField ErrorText="نام لینک را وارد نمایید" IsRequired="True" />
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="width: 10%"></td>
                                <td style="width: 30%"></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="نوع لینک *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxComboBox ID="ComboType" runat="server" 
                                         DataSourceID="ObjectDataSource1" DropDownStyle="DropDown"
                                         TextField="Name" ValueField="LiTId" ValueType="System.String"
                                        Width="100%" RightToLeft="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            
                                            <RequiredField ErrorText="نوع لینک را انتخاب نمایید" IsRequired="True" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td align="right" valign="top"></td>
                                <td align="right" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="آدرس لینک *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" dir="ltr" valign="top" colspan="3">
                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtLinkAddress" RightToLeft="False" runat="server" Width="100%"
                                         >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RequiredField ErrorText="آدرس لینک را وارد نمایید" IsRequired="True" />
                                            <RegularExpression ErrorText="آدرس لینک را با فرمت صحیح وارد نمایید- http://www.sample.com"
                                                ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?" />                                                                                        
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" colspan="3">
                                    <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" Height="37px" Width="100%" 
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
                                <td align="right" valign="top"></td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomASPxCheckBox ID="chkShow" runat="server" ClientInstanceName="chkShow" 
                                        Text="نمایش در صفحه اول" >
                                    </TSPControls:CustomASPxCheckBox>
                                </td>
                                <td align="right" valign="top"></td>
                                <td align="right" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="تصویر*" ID="lblFileUploadDocument">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td valign="top" align="right">
                                                <table width="100%">
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxUploadControl ID="FileUploadimg" runat="server" ClientInstanceName="flp"
                                                                InputType="Images" UploadWhenFileChoosed="True" OnFileUploadComplete="FileUploadimg_FileUploadComplete"
                                                                Width="300px">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
    HiddenFieldUploadControl.Set('HasFile','1');
    lblerrorupload.SetVisible(false);    
    ImgLink.SetImageUrl('../../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
    HiddenFieldUploadControl.Set('HasFile','0');
    lblerrorupload.SetVisible(false); 
    ImgLink.SetImageUrl('');
	}
}" />
                                                                <BrowseButton Text=" انتخاب تصویر">
                                                                    <Image Height="16px" Url="~/Images/Icons/image-upload.png" Width="16px">
                                                                    </Image>
                                                                </BrowseButton>
                                                                <CancelButton Text="انصراف">
                                                                </CancelButton>
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxImage ID="imgEndUploadImgClient" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                                ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel ID="lblerrorupload" runat="server" ClientInstanceName="lblerrorupload"
                                                    ClientVisible="False" ForeColor="Red" Text="تصویر را انتخاب نمایید">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top"></td>
                                <td align="right" valign="top" colspan="3">
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImgLink" ClientInstanceName="ImgLink"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                        <EmptyImage Url="~/Images/noimage.gif">
                                        </EmptyImage>
                                        <Border BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top"></td>
                                <td align="right" valign="top" colspan="3">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" ForeColor="Red" Text="ابعاد تصویر را حداکثر 70*255 انتخاب کنید">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                    ToolTip="ویرایش"  UseSubmitBehavior="False">
                                                    <Image  Url="~/Images/icons/edit.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                    <Image  Url="~/Images/icons/save.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>                                
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="LinkId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.LinkTypeManager"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldUploadControl" runat="server" ClientInstanceName="HiddenFieldUploadControl">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
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
