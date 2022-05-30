<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddTeachingCertificateGrade.aspx.cs" Inherits="Employee_Amoozesh_AddTeachingCertificateGrade"
    Title="مشخصات امتیازات استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function SetNewMode() {
            txtMeeting.SetText('');
            txtMinGrade.SetText('');
            txtMinGradeRelateJob.SetText('');
            txtSumTeachJob.SetText('');
            txtDescription.SetText('');
            txtMinGrade.SetText('');
            txtMinGradeTeaching.SetText('');

            txtMeeting.SetEnabled(true);
            txtMinGrade.SetEnabled(true);
            txtMinGradeRelateJob.SetEnabled(true);
            txtSumTeachJob.SetEnabled(true);
            txtDescription.SetEnabled(true);
            txtMinGrade.SetEnabled(true);
            txtMinGradeTeaching.SetEnabled(true);
            HiddenFieldTeachingGrade.Set("PageMode", HiddenFieldTeachingGrade.Get("NewMode"));
            HiddenFieldTeachingGrade.Set("TGradeId", "");

            RoundPanelGrade.SetHeaderText("جدید");
            document.getElementById('<%=txtDate.ClientID%>').value = "";

	document.getElementById('<%=txtDate.ClientID%>').disabled = false;
    document.getElementById('<%=txtDate.ClientID%>').setAttribute("usedatepicker", true);

    btnNew.SetEnabled(false);
    btnEdit.SetEnabled(false);
    btnNew2.SetEnabled(false);
    btnEdit2.SetEnabled(false);
    btnSave2.SetEnabled(true);
    btnDisActive.SetEnabled(false);
    btnDisActive2.SetEnabled(true);



}
    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             EnableTheming="False" ToolTip="جدید" ID="btnNew"
                                                            EnableViewState="False" ClientInstanceName="btnNew" OnClick="btnNew_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	//SetNewMode();

}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                                            ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click" ClientInstanceName="btnEdit"
                                                            EnableClientSideAPI="True">
                                                            <ClientSideEvents Click="function(s, e) {

	
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                            Text=" "  EnableTheming="False" ToolTip="ذخیره"
                                                            ID="btnSave" EnableViewState="False" OnClick="btnSave_Click" ClientInstanceName="btnSave">
                                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                            CausesValidation="False">
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                 </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    	<TSPControls:CustomASPxRoundPanel ID="RoundPanelGrade" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                   
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top;Width:15%" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره جلسه" Width="82px" ID="ASPxLabel6">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;Width:35%" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"   ID="txtMeeting"
                                                            ClientInstanceName="txtMeeting" >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top;Width:15%" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;Width:35%" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                                            ID="txtDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" Width="300px"
                                                            ID="RequiredFieldValidator1">تاریخ را وارد نمایید.</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="حداقل امتیاز لازم" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"   ID="txtMinGrade"
                                                            ClientInstanceName="txtMinGrade" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="حداقل امتیاز سابقه تدریس"  ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"   ID="txtMinGradeTeaching"
                                                            ClientInstanceName="txtMinGradeTeaching" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;" dir="rtl" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="حداقل امتیاز سابقه کار حرفه ای" 
                                                            ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"   ID="txtMinGradeRelateJob"
                                                            ClientInstanceName="txtMinGradeRelateJob" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مجموع امتیاز سابقه تدریس و سوابق پژوهشی" 
                                                            ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"   ID="txtSumTeachJob"
                                                            ClientInstanceName="txtSumTeachJob" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top;" colspan="3" align="right">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px"   ID="txtDescription"
                                                            ClientInstanceName="txtDescription" >
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
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             EnableTheming="False" ToolTip="جدید" ID="btnNew2"
                                                            EnableViewState="False" ClientInstanceName="btnNew2" OnClick="btnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                            <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                                            ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click" ClientInstanceName="btnEdit2">
                                                            <ClientSideEvents Click="function(s, e) {

	
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                            Text=" "  EnableTheming="False" ToolTip="ذخیره"
                                                            ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click" ClientInstanceName="btnSave2">
                                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False"
                                                            OnClick="btnBack_Click" CausesValidation="False">
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeachingGrade" ClientInstanceName="HiddenFieldTeachingGrade">
                                        </dxhf:ASPxHiddenField>
                                   
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

</asp:Content>
