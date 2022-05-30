<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddNezamMemberChart.aspx.cs" Inherits="Employee_Nezam_AddNezamMemberChart"
    Title=" مشخصات پست سازمانی عضو " %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetError(Result) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Result;
        }
    </script>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%" align="center">
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]</div>
                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>  
                                                        <table >
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" Visible="false"
                                                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/new.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/edit.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                            Width="25px" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                            OnClick="btnSave_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/save.png">
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
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberChart" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <dxe:ASPxLabel ID="lblNcName" runat="server" Text="پست سازمانی:" Width="224px">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <TSPControls:CustomAspxCallbackPanel HideContentOnCallback="False" ID="CallbackPanelNezamChart"
                                            runat="server" ClientInstanceName="CallbackNezamChart" 
                                            OnCallback="CallbackPanelNezamChart_Callback" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <asp:Label runat="server" Text="کد پرسنلی کارمند" Width="100%" ID="Label2"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="TextBoxEmpCode" Style="direction: ltr" 
                                                                        Width="100%"  ClientInstanceName="TextBoxEmpCode">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="True" ErrorText="کد پرسنلی کارمند را وارد نمایید."></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <ClientSideEvents TextChanged="function(s, e) {
	CallbackNezamChart.PerformCallback('');
}" />
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="top" align="right" width="15%">
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <asp:Label runat="server" Text="نام" ID="Label5"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtName"  Width="100%" ReadOnly="True"
                                                                        >
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <asp:Label runat="server" Text="نام خانوادگی" Width="100%" ID="Label6"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtLastName"  Width="100%"
                                                                        ReadOnly="True" >
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <asp:Label runat="server" Text="نام پدر" ID="Label3" Width="100%"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtFatherName"  Width="100%"
                                                                        ReadOnly="True" >
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <asp:Label runat="server" Text="شماره شناسنامه" Width="100%" ID="Label8"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtIdNo"  Width="100%" ReadOnly="True"
                                                                        >
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                            <ClientSideEvents BeginCallback="function(s, e) {
	//CallbackNezamChart.cpEmpId =TextBoxEmpCode.GetText();
}" EndCallback="function(s, e) {
	HiddenFieldNezamChart.Set('EmpId',CallbackNezamChart.cpEmpId);
	if(CallbackNezamChart.cpError==1)
	{
		CallbackNezamChart.cpError=0;
		SetError(CallbackNezamChart.cpMsgError);
	}

}" />
                                        </TSPControls:CustomAspxCallbackPanel>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="تاریخ شروع" ID="Label4" Width="100%"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                        ID="txtStartDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                        Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                </td>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="تاریخ پایان" Width="100%" ID="Label1"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                        ID="txtEndDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                        Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="توضیحات" ID="Label7"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDesc"  Width="100%"
                                                        >
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="4" valign="top">
                                                    <TSPControls:CustomASPxCheckBox ID="chbIsMasterPosition" runat="server" Checked="True" 
                                                          Text="پست سازمانی اصلی">
                                                    </TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="4" dir="rtl" valign="top">
                                                    <TSPControls:CustomASPxCheckBox ID="chbIsMaster" runat="server" 
                                                         Text="ارشد پست سازمانی">
                                                    </TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                  </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    <br />
                                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldNezamChart" ClientInstanceName="HiddenFieldNezamChart">
                                                        </dxhf:ASPxHiddenField>
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                        <table >
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                            Visible="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/new.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/edit.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                            Width="25px" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False"
                                                                            EnableTheming="False" OnClick="btnSave_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/save.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                            CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
            TypeName="TSP.DataManager.NezamChartManager" UpdateMethod="Update" EnableCaching="True"
            CacheDuration="30">          
        </asp:ObjectDataSource>  
</asp:Content>
