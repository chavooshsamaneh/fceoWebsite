<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddInstitueTeacher.aspx.cs" Inherits="Institue_Amoozesh_AddInstitueTeacher"
    Title="مشخصات استاد مؤسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'TeId;Name;Family;Father;FileNo;FileDate;IdNo;SSN;TiId;MjName;LiId;MjId;Tel;Address;MobileNo;Email;BirthDate', SetValue);
        }
        function SetValue(values) {
            txtName1.SetText(values[1] + ' ' + values[2]);
            txtFileNo1.SetText(values[4]);
            document.getElementById('<%=txtFileDate.ClientID%>').value = values[5];
            HiddenFieldInsTeacher.Set('TeId', values[0]);
            cmbLicence1.SetValue(values[10]);
            cmbMajor1.SetValue(values[9]);

        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" Width="25px" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/new.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/edit.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/save.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" runat="server" Text=" "
                                            CausesValidation="False" Width="25px" ID="btnDisActive" AutoPostBack="False"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/disactive.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/Back.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsTeacher" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام و نام خانوادگی *" Width="113px" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ClientInstanceName="txtName1"
                                            ID="txtName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="استاد را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxButton  runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnMe"
                                            EnableClientSideAPI="True" CausesValidation="False"
                                            EnableTheming="False" ToolTip="جستجو" ID="btnSearch1" EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>
                                            <image url="~/Images/icons/Search.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td valign="top" align="right">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی " Width="119px" ID="lblicence">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBLicence"
                                            TextField="LiName" ValueField="LiId" HorizontalAlign="Right"
                                            Width="170px" Height="21px" ClientInstanceName="cmbLicence1"
                                            ClientEnabled="False" ID="cmbLicence">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="لطفاً یک گزینه را انتخاب نمائید."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="رشته " Width="48px" ID="lblMajor">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBMajor"
                                            TextField="MjName" ValueField="MjId" HorizontalAlign="Right"
                                            Width="170px" Height="21px" ClientInstanceName="cmbMajor1"
                                            EnableClientSideAPI="True" ClientEnabled="False" ID="cmbMajor">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="لطفاً یک گزینه را انتخاب نمائید."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره مجوز تدریس " Width="120px" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="170px"
                                            ClientInstanceName="txtFileNo1" ClientEnabled="False" ID="txtFileNo" Style="direction: ltr">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ مجوز " Width="65px" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Enabled="False" Width="185px"
                                            ID="txtFileDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری *" Width="112px" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="185px" ID="txtStartDate"
                                            Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator"
                                            ValidateEmptyText="True" ControlToValidate="txtStartDate" ErrorMessage="تاریخ شروع را وارد نمایید"
                                            Width="128px" ID="PersianDateValidator1"></pdc:PersianDateValidator>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری " Width="108px" ID="ASPxLabel3"
                                            Visible="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="185px" ID="txtEndDate"
                                            Visible="False" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="500px"
                                            ID="txtDescription">
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
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" Width="25px" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/new.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/edit.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/save.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" runat="server" Text=" "
                                            CausesValidation="False" Width="25px" ID="btnDisActive2" AutoPostBack="False"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/disactive.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </hoverstyle>
                                            <image url="~/Images/icons/Back.png">
                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldInsTeacher" ClientInstanceName="HiddenFieldInsTeacher">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"
                HeaderText="جستجو" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
                PopupElementID="btnSearch1" CloseAction="CloseButton" ClientInstanceName="pop"
                Height="269px">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <div dir="rtl">
                            <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                                Width="544px" ID="GridTeacher" DataSourceID="ObjdsTeachers" KeyFieldName="MeId"
                                AutoGenerateColumns="False" ClientInstanceName="grid">
                                <ClientSideEvents RowDblClick="function(s, e) {
pop.Hide();
//loadPanel1.Show();
	SetControlValues();
	
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت"
                                        Name="MeId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام" Name="FirstName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Family" Caption="نام خانوادگی"
                                        Name="LastName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Father" Caption="نام پدر"
                                        Name="FatherName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="BirthDate"
                                        Name="BirthDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="SSN" Caption="کد ملی" Name="SSN">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="IdNo" Name="IdNo">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="FileNo"
                                        Name="FileNo">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Tel" Name="HomeTel">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Address"
                                        Name="HomeAdr">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Email"
                                        Name="Email">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="MobileNo"
                                        Name="MobileNo">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="LiId" Name="LiId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="MjId" Name="MjId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="MjName"
                                        Name="LastMjName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="TiId" Name="TiId">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                            <asp:ObjectDataSource runat="server" SelectMethod="SelectConfirmedTeacher" ID="ObjdsTeachers"
                                TypeName="TSP.DataManager.TeacherManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </TSPControls:CustomASPxPopupControl>
            <asp:ObjectDataSource ID="ODBMajor" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBLicence" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"
                UpdateMethod="Update"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
