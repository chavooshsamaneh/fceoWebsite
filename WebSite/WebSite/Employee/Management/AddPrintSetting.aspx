<%@ Page Title="مشخصات امضاکنندگان گواهینامه ها" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddPrintSetting.aspx.cs" Inherits="Employee_HomePage_AddPrintSetting" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#"><span style="color: #000000">بستن</span></a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table cellpadding="0">
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px">
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
                                        <ClientSideEvents Click="function(s, e) {
 if((cmbFirstAssigner.GetValue() != null)&&(cmbSecontAssigner.GetValue() != null))
 {
  if(cmbFirstAssigner.GetValue() == cmbSecontAssigner.GetValue())
    {
        alert('امضاکنندگان یکسان می باشند');
        e.processOnServer=false;
        return;
    } 
}
}" />
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                    </TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server" EnableTheming="False"
                                        EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" PostBackUrl="PrintSetting.aspx">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxCallbackPanel runat="server" Width="100%"
                ID="CallbackPanelPrintSetting" ClientInstanceName="CallbackPanelPrintSetting"
                OnCallback="CallbackPanelPrintSetting_Callback">

                <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}" />
                <PanelCollection>

                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="جدید" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>



                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <dxe:ASPxLabel runat="server" Text="نکته : ترتیب امضا کنندگان در چاپ به ترتیب از راست به چپ و یا از بالا به پایین می باشد"
                                                        ID="ASPxLabel6" Font-Bold="true" ForeColor="DarkRed">
                                                    </dxe:ASPxLabel>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="نوع گواهینامه*">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" width="85%" colspan="3">
                                                    <cc1:ComboBox ID="cmbPrintType" runat="server"
                                                        DataSourceID="ObjectDataSourcePrintType" TextField="PrtTypeName"
                                                        ValueField="PrtTypeId" EnableIncrementalFiltering="True" HorizontalAlign="Right"
                                                        IncrementalFilteringMode="StartsWith" Width="100%"
                                                        RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" ErrorText="نوع گواهینامه را انتخاب نمایید" />
                                                        </ValidationSettings>
                                                    </cc1:ComboBox>
                                                </td>
                                                <%--   <td align="right" valign="top" width="15%">
                                                                </td>
                                                                <td align="left" valign="top" width="35%">
                                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="امضاکننده اول*">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" width="35%">
                                                    <cc1:ComboBox ID="cmbFirstAssigner" ClientInstanceName="cmbFirstAssigner" runat="server"
                                                        DataSourceID="ObjectDataSourceGovTitle"
                                                        TextField="GmtNameDetail" ValueField="GmtId" EnableIncrementalFiltering="True" HorizontalAlign="Right"
                                                        IncrementalFilteringMode="StartsWith" Width="100%"
                                                        RightToLeft="True">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {

	CallbackPanelPrintSetting.PerformCallback('first'+';'+ cmbFirstAssigner.GetValue());
}"></ClientSideEvents>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" ErrorText="امضاکننده اول را انتخاب نمایید" />
                                                        </ValidationSettings>
                                                    </cc1:ComboBox>
                                                </td>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="نام مدیر">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="left" valign="top" width="35%">
                                                    <dx:ASPxTextBox ID="txtFirstAssigner" ReadOnly="true" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="امضاکننده دوم">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" width="35%">
                                                    <cc1:ComboBox ID="cmbSecontAssigner" ClientInstanceName="cmbSecontAssigner" runat="server"
                                                        DataSourceID="ObjectDataSourceGovTitle"
                                                        TextField="GmtNameDetail" ValueField="GmtId" EnableIncrementalFiltering="True" HorizontalAlign="Right"
                                                        IncrementalFilteringMode="StartsWith" Width="100%"
                                                        RightToLeft="True">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {

if(cmbSecontAssigner.GetValue()==null)
txtSecontAssigner.SetText('');
if(cmbSecontAssigner.GetValue()!=null)
	CallbackPanelPrintSetting.PerformCallback('second'+';'+ cmbSecontAssigner.GetValue());
}"></ClientSideEvents>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <%--  <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                                <errortextpaddings paddingleft="4px" />
                                                                            </ErrorFrameStyle>
                                                                            <RequiredField IsRequired="true" ErrorText="امضاکننده دوم را انتخاب نمایید" />
                                                                        </ValidationSettings>--%>
                                                    </cc1:ComboBox>
                                                </td>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="نام مدیر">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="left" valign="top" width="35%">
                                                    <dx:ASPxTextBox ID="txtSecontAssigner" ReadOnly="true" ClientInstanceName="txtSecontAssigner"
                                                        runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="توضیحات">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="3">
                                                    <TSPControls:CustomASPXMemo ID="MemoDescription" runat="server"
                                                        Height="37px"
                                                        Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="تاریخ شروع">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="right" dir="ltr" valign="top" width="35%">
                                              <%--      <dx:ASPxTextBox ID="txtCreateDate" RightToLeft="False" ReadOnly="true" runat="server"
                                                        Width="100%">
                                                    </dx:ASPxTextBox>--%>
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" 
                                                        ShowPickerOnTop="True" ID="txtCreateDate" PickerDirection="ToRight" RightToLeft="False"
                                                        IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreateDate" ID="RequiredFieldValidatorRegDate">تاریخ شروع  را وارد نمایید</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="تاریخ پایان">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="left" valign="top" width="35%">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" 
                                                        ShowPickerOnTop="True" ID="txtExpireDate" PickerDirection="ToRight" RightToLeft="False"
                                                        IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtExpireDate" ID="RequiredFieldValidator1">تاریخ پایان  را وارد نمایید</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>

                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table cellpadding="0">
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
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
                                        <ClientSideEvents Click="function(s, e) {
 if((cmbFirstAssigner.GetValue() != null)&&(cmbSecontAssigner.GetValue() != null))
 {
  if(cmbFirstAssigner.GetValue() == cmbSecontAssigner.GetValue())
    {
        alert('امضاکنندگان یکسان می باشند');
        e.processOnServer=false;
        return;
    } 
}
}" />
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                    </TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server" EnableTheming="False"
                                        EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" PostBackUrl="PrintSetting.aspx">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
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
                        لطفا صبر نمایید ...
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>

            <asp:ObjectDataSource ID="ObjectDataSourceGovTitle" runat="server" OldValuesParameterFormatString="original_{0}"
                TypeName="TSP.DataManager.GovManagerTitleManager" SelectMethod="SelectActivManagerTitle"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourcePrintType" runat="server" OldValuesParameterFormatString="original_{0}"
                TypeName="TSP.DataManager.PrintTypeManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldModeID" runat="server">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
