<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddFinancialStatus.aspx.cs" Inherits="Members_ImplementDoc_AddFinancialStatus"
    Title="مشخصات توان مالی" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                    visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 27px; height: 27px">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image Height="25px" Width="25px" Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td style="width: 27px; height: 27px">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image Height="25px" Width="25px" Url="~/Images/icons/edit.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td style="width: 27px; height: 27px">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image Height="25px" Width="25px" Url="~/Images/icons/save.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td style="width: 27px; height: 27px">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image Height="25px" Width="25px" Url="~/Images/icons/Back.png">
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
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع وضعیت مالی" Width="86px" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" colspan="3">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="300px" ImageFolder="~/App_Themes/Glass/{0}/"
                                                TextField="Name" ID="CmbName" CssPostfix="Glass" DataSourceID="ObjectDataSource1"
                                                ValueType="System.String" ValueField="OfdId" CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نوع وضعیت مالی را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Columns>
                                                    <dxe:ListBoxColumn FieldName="Name" Caption="نام" Width="290px"></dxe:ListBoxColumn>
                                                    <dxe:ListBoxColumn FieldName="Value" Caption="ضریب"></dxe:ListBoxColumn>
                                                </Columns>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مبلغ(ریال)" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <dxe:ASPxTextBox runat="server" CssPostfix="Glass" Width="300px" ID="txtValue" CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                            <dxe:ASPxLabel runat="server" Text="حداقل مبلغ اعلام شده  نباید کمتر از 10,000,000ریال باشد"
                                                Font-Size="8pt" ID="lblWarning" ForeColor="Red">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="15px" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <dxe:ASPxMemo runat="server" Height="35px" CssPostfix="Glass" Width="550px" ID="txtDesc"
                                                CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dxe:ASPxMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4" HeaderText="مدارک پیوست" runat="server"
                                Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table runat="server" id="TblFile" dir="rtl" width="100%">
                                            <tr runat="server" id="Tr1">
                                                <td runat="server" id="Td1" style="width: 77px" valign="top" align="right">
                                                    <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>
                                                </td>
                                                <td runat="server" id="Td6" valign="top" align="right">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl runat="server" ID="flp" InputType="Files" ClientInstanceName="flpc"
                                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flp_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
	 if(e.isValid){
        imgEndUploadImgClient.SetVisible(true);
	    flpme.Set('name',1);
	    lblf.SetVisible(false);
	    hpFilePath.SetVisible(true);
	    hpFilePath.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
    }
     else{
	imgEndUploadImgClient.SetVisible(false);
	 flpme.Set('name',0);
	lblf.SetVisible(true);
    hpFilePath.SetVisible(true);
    }
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
                                                                    </dxhf:ASPxHiddenField>
                                                                    <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientVisible="False"
                                                                        ID="ASPxLabel48" ForeColor="Red" ClientInstanceName="lblf">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                        ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" Target="_blank" ID="hpFilePath"
                                                        ClientVisible="False" ClientInstanceName="hpFilePath">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="Tr3">
                                                <td runat="server" id="Td4" style="width: 77px" valign="top" align="right">
                                                    <asp:Label runat="server" Text="توضیحات" Width="52px" ID="Label10" __designer:wfdid="w11"></asp:Label>
                                                </td>
                                                <td runat="server" id="Td5" valign="top">
                                                    <dxe:ASPxMemo runat="server" Height="28px" ID="txtDescImg" CssPostfix="Glass" Width="550px"
                                                        CssFilePath="~/App_Themes/Glass/{0}/styles.css" __designer:wfdid="w12">
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="Tr2">
                                                <td runat="server" id="Td3" align="center" colspan="2">
                                                    <br />
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="اضافه به لیست" CssPostfix="Glass" CausesValidation="False"
                                                         ID="btnAddFlp" UseSubmitBehavior="False" CssFilePath="~/App_Themes/Glass/{0}/styles.css"
                                                       OnClick="btnAddFlp_Click">
                                                         <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <TSPControls:CustomAspxDevGridView runat="server" CssPostfix="Glass" EnableViewState="False"
                                            Width="100%" ID="AspxGridFlp" KeyFieldName="Id" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Glass/{0}/styles.css"
                                             OnRowDeleting="AspxGridFlp_RowDeleting">
                                            <Columns>

                                             <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="حذف" ShowDeleteButton="true" Width="35px" Name="clnDelete">
                                                                   
                                                                    </dxwgv:GridViewCommandColumn>
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
                                               
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldFinancial">
                                                    </dxhf:ASPxHiddenField>
                                         
                </div>
                <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="FinancialId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="MemberFile" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                    TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager" FilterExpression="Type={0}">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
  
</asp:Content>
