<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddDefaultThemes.aspx.cs" Inherits="Employee_Management_AddDefaultThemes"
    Title="مشخصات طرح های پیش فرض" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                function CheckFileUpload() {
                    if (HiddenFieldUploadControl.Get('HasFile') == '0') {
                        lblerrorupload.SetVisible(true);
                        //upload.SetIsValid(false);
                        return false;
                    }
                    lblerrorupload.SetVisible(false);
                    return true;
                }
            </script>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#"><span style="color: #000000">بستن</span></a>]
            </div>
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                                        <Image Height="25px" Url="~/Images/new.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave" runat="server" EnableTheming="False"
                                                        EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click">
                                                        <ClientSideEvents Click="function(s,e)
                                                        {
                                                        if(CheckFileUpload()==false) e.processOnServer=false;
                                                        }" />
                                                        <Image Url="~/Images/icons/save.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server" EnableTheming="False"
                                                        EnableViewState="False" ToolTip="بازگشت" PostBackUrl="~/Employee/Management/DefaultThemes.aspx"
                                                        CausesValidation="False">
                                                        <Image Height="25px" Url="~/Images/icons/back.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                 </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                     	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTheme" HeaderText="مشخصات قالب"  runat="server"
        Width="100%">
        <PanelCollection>
            <dx:PanelContent>

        
                                        <table dir="rtl" align="right" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="center" colspan="4" width="100%">
                                                        <dxe:ASPxLabel runat="server" Font-Bold="true" Text="توجه: پسوند فایل انتخابی بایستی repx باشد."
                                                            ID="txtRequestComment" ForeColor="DarkRed" Width="100%">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top" width="15%">نوع قالب*
                                                    </td>
                                                    <td align="right" dir="ltr" valign="top" width="35%">
                                                        <cc1:ComboBox runat="server"  IncrementalFilteringMode="StartsWith"
                                                            ID="CBDefaultThemeType"  ValueType="System.String"
                                                             EnableIncrementalFiltering="True"
                                                            HorizontalAlign="Right" DataSourceID="ObjectDataSource1" TextField="TypeName"
                                                            ValueField="TypeID" Width="100%" RightToLeft="True">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField ErrorText="نوع قالب را وارد نمایید" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </cc1:ComboBox>
                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="TSP.DataManager.DefaultThemeTypesManager"
                                                            SelectMethod="GetData"></asp:ObjectDataSource>
                                                    </td>
                                                    <td align="right" valign="top" width="15%">نام*
                                                    </td>
                                                    <td align="left" valign="top" width="35%">
                                                        <cc1:TextBox runat="server" ID="txtname"  Width="100%" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField ErrorText="نام قالب را وارد نمایید" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </cc1:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">فایل*
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="True"
                                                                        ID="CustomAspxUploadControl1" InputType="Custom" CustomAllowedFileTypes=".repx"
                                                                        ClientInstanceName="upload" MaxSizeForUploadFile="10000000" OnFileUploadComplete="CustomAspxUploadControl1_FileUploadComplete"
                                                                        Width="100%">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
		imgcheck.SetVisible(true);
		HiddenFieldUploadControl.Set('HasFile','1');
         lblerrorupload.SetVisible(false);  
		}
		else {
		imgcheck.SetVisible(false);		
		HiddenFieldUploadControl.Set('HasFile','0');
         lblerrorupload.SetVisible(false);  
		}
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <dx:ASPxHyperLink ID="lblfilename" ClientInstanceName="lblfilename" Text="مسیر فایل"
                                                                        Target="_blank" runat="server">
                                                                    </dx:ASPxHyperLink>
                                                                </td>
                                                                <td align="right" valign="top">
                                                                    <dx:ASPxImage ID="imgcheck" ClientInstanceName="imgcheck" runat="server" ClientVisible="False"
                                                                        ImageUrl="~/Images/icons/Check.png">
                                                                    </dx:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <dx:ASPxLabel ID="lblerrorupload" runat="server" Font-Size="8pt" ForeColor="Red"
                                                            Text="فایل را وارد نمایید" ClientInstanceName="lblerrorupload" ClientVisible="False">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td align="right" valign="top">تاریخ
                                                    </td>
                                                    <td align="left" dir="ltr" valign="top">
                                                        <dx:ASPxTextBox runat="server" ID="txtdate"  Width="100%" ReadOnly="true"
                                                            RightToLeft="False" >
                                                            <ReadOnlyStyle BackColor="Snow">
                                                            </ReadOnlyStyle>
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField ErrorText="نام قالب را وارد نمایید" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top" width="15%">توضیحات
                                                    </td>
                                                    <td align="right" colspan="3 " dir="rtl" valign="top">
                                                        <TSPControls:CustomASPXMemo ID="memodescr" runat="server" 
                                                             Height="40px" 
                                                            Width="100%">
                                                            <ValidationSettings>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                            <br />
                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/new.png" Width="25px">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave2" runat="server" EnableTheming="False"
                                                        EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click">
                                                        <ClientSideEvents Click="function(s,e)
                                                        {
                                                        if(CheckFileUpload()==false) e.processOnServer=false;
                                                        }" />
                                                        <Image Url="~/Images/icons/save.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton4" runat="server" EnableTheming="False"
                                                        EnableViewState="False" ToolTip="بازگشت" PostBackUrl="~/Employee/Management/DefaultThemes.aspx"
                                                        CausesValidation="False">
                                                        <Image Height="25px" Url="~/Images/icons/back.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                 </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <dx:ASPxHiddenField ID="HiddenFieldPageMode" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="HiddenFieldUploadControl" runat="server" ClientInstanceName="HiddenFieldUploadControl">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
