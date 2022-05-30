<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddTrainingQuestion.aspx.cs" Inherits="Teachers_Amoozesh_AddTrainingQuestion"
    Title="مشخصات سؤال امتحانی" %>

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
    <div id="Content" runat="server" style="width: 100% display: block;" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>

                               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" align="right">
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                        ToolTip="جدید" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                                        ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
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
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />

                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

        
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right" valign="top" style="width: 15%">
                                                <dxe:ASPxLabel runat="server" Text="نام درس" Width="100%" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="ltr" align="right" valign="top" style="width: 35%">
                                                <TSPControls:CustomAspxComboBox runat="server"  TextField="CrsName"
                                                    ID="cmbCourse"  DataSourceID="ObjectDataSourceCourse" ValueType="System.String"
                                                    ValueField="CrsId"  RightToLeft="True"
                                                    Width="100%">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="نام درس مربوطه را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td style="width: 15%">
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" style="width: 15%">
                                                <dxe:ASPxLabel runat="server" Text="متن سؤال" Width="100%" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td colspan="3" align="right" valign="top" style="width: 85%">
                                                <TSPControls:CustomASPXMemo runat="server" Height="61px"  Width="100%" ID="txtQuText"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="متن سؤال را وارد نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="گزینه 1" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="3">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtAnsw1" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="گزینه 1  را وارد نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="گزینه 2" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="3">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtAnsw2" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="گزینه 2 را وارد نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="گزینه 3" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="3">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtAnsw3" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="گزینه 3  را وارد نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="گزینه 4" ID="ASPxLabel6">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="3">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtAnsw4" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="گزینه 4  را وارد نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="جواب صحیح" ID="ASPxLabel7">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="2">
                                                <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdbAnswer"
                                                    Width="100%"  
                                                     >
                                                    <Items>
                                                        <dxe:ListEditItem Text="گزینه 1" Value="1" />
                                                        <dxe:ListEditItem Text="گزینه  2" Value="2" />
                                                        <dxe:ListEditItem Text="گزینه 3" Value="3" />
                                                        <dxe:ListEditItem Text="گزینه  4" Value="4" />
                                                    </Items>
                                                    <ValidationSettings Display="Dynamic">
                                                        <RequiredField ErrorText="جواب صحیح را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                    <Border BorderStyle="None" />
                                                </dxe:ASPxRadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نوع سؤال" ID="ASPxLabel10">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="2">
                                                <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdbType"
                                                    Width="100%"  
                                                     >
                                                    <Items>
                                                        <dxe:ListEditItem Text="بسیار سخت" Value="1" />
                                                        <dxe:ListEditItem Text="سخت" Value="2" />
                                                        <dxe:ListEditItem Text="متوسط" Value="3" />
                                                        <dxe:ListEditItem Text="آسان" Value="4" />
                                                    </Items>
                                                    <ValidationSettings Display="Dynamic">
                                                        <RequiredField ErrorText="" />
                                                    </ValidationSettings>
                                                    <Border BorderStyle="None" />
                                                </dxe:ASPxRadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="فایل تصویری" Width="65px" ID="lblFileNew">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <table dir="rtl">
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: right;">
                                                            <TSPControls:CustomAspxUploadControl ID="flp" runat="server" InputType="Files" ClientInstanceName="flpc"
                                                                OnFileUploadComplete="flp_FileUploadComplete" UploadWhenFileChoosed="true">
                                                              
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
imgEndUploadImgClient.SetVisible(true);
	HpLinkFile.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndUploadImgClient.SetVisible(false); 
	HpLinkFile.SetNavigateUrl('');
 }           

}" />
                                                               
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right;">
                                                            <dxe:ASPxImage ID="imgEndUploadImg" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                                ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </table>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2" align="right" valign="top">
                                                <dxe:ASPxHyperLink ID="HpLinkFile" runat="server" ClientInstanceName="HpLinkFile"
                                                    Target="_blank" Text="آدرس فایل">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right" valign="top">
                                                <%--            <dxe:ASPxRadioButton runat="server" Text="نمایش عمومی سؤال   " ID="rdbEnableView"
                                                    TextAlign="Left">
                                                </dxe:ASPxRadioButton>--%>
                                                <TSPControls:CustomASPxCheckBox ID="rdbEnableView" Text="نمایش عمومی سؤال" RightToLeft="True" runat="server"
                                                      >
                                                </TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" Width="65px" ID="ASPxLabel9">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtDesc"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
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


  
                                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" align="right">
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                        ToolTip="جدید" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                                        ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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

                <asp:HiddenField ID="QuestionId" runat="server" Visible="False" />
                <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="ObjectDataSourceCourse" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
