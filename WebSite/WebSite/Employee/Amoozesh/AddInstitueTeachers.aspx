<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddInstitueTeachers.aspx.cs" Inherits="Employee_Amoozesh_AddInstitueTeachers"
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

            document.getElementById('<%=txtFileDate.ClientID%>').value = values[5];
    HiddenFieldInsTeacher.Set('TeId', values[0]);
    cmbLicence1.SetValue(values[10]);
    cmbMajor1.SetValue(values[9]);
    txtName1.SetText(values[1] + ' ' + values[2]);
    txtFileNo1.SetText(values[4]);

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
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                Width="25px" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnNew_Click" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
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
                <div style="vertical-align: top; width: 100%; height: 1px; text-align: right">
                    <dxe:ASPxLabel ID="lblInsName" runat="server" Text="ASPxLabel">
                    </dxe:ASPxLabel>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsTeacher" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام و نام خانوادگی" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName"  ClientEnabled="False"
                                                ReadOnly="True" ClientInstanceName="txtName1" >
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  ToolTip="جستجو" CausesValidation="False"
                                                ID="btnSearch1" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" ClientInstanceName="btnMe">
                                                <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>
                                                <Image  Url="~/Images/icons/Search.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی" Width="119px" ID="lblicence">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td   valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                 TextField="LiName" ID="cmbLicence" ClientEnabled="False"
                                                DataSourceID="ODBLicence" ValueType="System.String" ValueField="LiId" Height="21px"
                                                ClientInstanceName="cmbLicence1" 
                                                HorizontalAlign="Right">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="لطفاً یک گزینه را انتخاب نمائید."></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                            <br />
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته" ID="lblMajor">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td  valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                 TextField="MjName" ID="cmbMajor" ClientEnabled="False"
                                                DataSourceID="ODBMajor" EnableClientSideAPI="True" ValueType="System.String"
                                                ValueField="MjId" Height="21px" ClientInstanceName="cmbMajor1" 
                                                HorizontalAlign="Right">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="لطفاً یک گزینه را انتخاب نمائید."></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره مجوز تدریس" Width="108px" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFileNo"  ClientEnabled="False"
                                                ClientInstanceName="txtFileNo1" >
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
                                            <dxe:ASPxLabel runat="server" Text="تاریخ مجوز" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Enabled="False"
                                                ShowPickerOnTop="True" ID="txtFileDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr" RightToLeft="False" Width="300px"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری" Width="108px" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                RightToLeft="False"  Width="300px"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                ErrorMessage="تاریخ شروع را وارد نمایید" ControlToValidate="txtStartDate" Width="128px"
                                                ID="PersianDateValidator1"></pdc:PersianDateValidator>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری" Width="108px" ID="ASPxLabel3"
                                                Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Visible="False"
                                                ShowPickerOnTop="True" ID="txtEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr" RightToLeft="False"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات"  ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDescription" 
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
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                                ToolTip="جدید" UseSubmitBehavior="False" Width="25px" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btSave_Click" Text=" "
                                                ToolTip="ذخیره" UseSubmitBehavior="False" Width="25px">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td  >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField ID="HiddenFieldInsTeacher" runat="server" ClientInstanceName="HiddenFieldInsTeacher">
                </dxhf:ASPxHiddenField>


                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"  
                    HeaderText="جستجو" Height="269px"  PopupVerticalAlign="WindowCenter"
                    PopupHorizontalAlign="WindowCenter" PopupElementID="btnSearch1" CloseAction="CloseButton"
                    ClientInstanceName="pop">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <div dir="rtl">
                                <TSPControls:CustomAspxDevGridView ID="GridTeacher" runat="server" AutoGenerateColumns="False"
                                    ClientInstanceName="grid"  
                                    DataSourceID="ObjdsTeachers" EnableViewState="False" KeyFieldName="MeId" Width="544px">
                                    <ClientSideEvents RowDblClick="function(s, e) {
pop.Hide();
//loadPanel1.Show();
	SetControlValues();
	
}" />



                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Name="MeId" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" Name="FirstName" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" Name="LastName"
                                            VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="Father" Name="FatherName"
                                            VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="BirthDate" Name="BirthDate" Visible="False"
                                            VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" Name="SSN" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IdNo" Name="IdNo" Visible="False" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="FileNo" Name="FileNo" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Tel" Name="HomeTel" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Address" Name="HomeAdr" Visible="False"
                                            VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Email" Name="Email" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="MobileNo" Name="MobileNo" Visible="False"
                                            VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="LiId" Name="LiId" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="MjId" Name="MjId" Visible="False" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="MjName" Name="LastMjName" Visible="False"
                                            VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="TiId" Name="TiId" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                </TSPControls:CustomAspxDevGridView>
                            </div>
                            <asp:ObjectDataSource ID="ObjdsTeachers" runat="server" SelectMethod="SelectConfirmedTeacher"
                                TypeName="TSP.DataManager.TeacherManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px" />
                    <CloseButtonImage Height="17px" Width="17px" />
                </TSPControls:CustomASPxPopupControl>
                <asp:ObjectDataSource ID="ODBMajor" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBLicence" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>

            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
