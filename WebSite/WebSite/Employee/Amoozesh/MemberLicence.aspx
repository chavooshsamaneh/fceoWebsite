<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLicence.aspx.cs" Inherits="Employee_Amoozesh_MemberLicence"
    Title="مدیریت گواهینامه های آموزشی" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
    <script language="javascript">
        function ShowMessage(Message) {
            if (Message != null) {
                alert(Message);
            }
        }

        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();

                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }

        function CheckSearch() {
            if (
                cmbCourse.GetSelectedIndex() == 0 &&
                txtMeId.GetText() == '' &&
                txtFirstName.GetText() == '' &&
                txtLastName.GetText() == '' &&
                ComboType.GetSelectedIndex() == 0 &&
                cmbObjectionType.GetSelectedIndex() == 0)
                return 0;
            return 1;
        }
    </script>

    <div style="text-align: right; display: block; visibility: hidden" id="DivReport"
        class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="BtnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
	else
	{
		grid.PerformCallback('btnEdit');
	}	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست صدور گواهی خارج از نوبت"
                                                    ID="btnNewPeriodReg" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnNewPeriodReg_Click" ClientInstanceName="NewPeriodReg">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/NewPeriodRegOutTime.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableTheming="False"
                                                    EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="حذف درخواست" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
  if(confirm('آیا مطمئن به حذف این درخواست هستید؟'))
  {
        grid.PerformCallback('btnDelete');
  }
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
grid.PerformCallback('WF');
   
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	grid.PerformCallback('Tracing');
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                    ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/ExportExcel.png">
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
    <div width="100%" align="right">
        <ul class="HelpUL">
            <li>جهت ثبت گواهینامه های آموزشی اعضای انتقالی از ''دکمه جدید'' استفاده نمایید </li>
            <li>جهت ثبت گواهینامه اعضایی که به هر دلیل اطلاعات ثبت نام آنها درهنگام ثبت نام دوره
                        در سیستم ثبت نشده است ، از دکمه ''درخواست صدور گواهی خارج از نوبت'' استفاده نمایید
            </li>
        </ul>
    </div>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="جستجو"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">
                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel1">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtMeId"
                                    ClientInstanceName="txtMeId">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="کد عضویت عدد صحیح می باشد" ValidationExpression="\d*" />
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td style="vertical-align: top;" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="عنوان درس" ID="ASPxLabel6">
                                </dxe:ASPxLabel>
                            </td>
                            <td dir="ltr" valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    TextField="CrsName" ID="cmbCourse"
                                    DataSourceID="odbCourseName" ValueType="System.String" ValueField="CrsId"
                                    EnableIncrementalFiltering="True" ClientInstanceName="cmbCourse" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">
                                <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel3">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtFirstName"
                                    ClientInstanceName="txtFirstName">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel5">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtLastName"
                                    ClientInstanceName="txtLastName">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">
                                <dxe:ASPxLabel runat="server" Text="نوع" ID="ASPxLabel2">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    ID="ComboType" ValueType="System.String"
                                    EnableIncrementalFiltering="True"
                                    ClientInstanceName="ComboType" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <Items>
                                        <dxe:ListEditItem Value="0" Text="مدرک دوره"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="1" Text="مدرک سمینار"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="ثبت نام دوره" Value="2" />
                                        <dxe:ListEditItem Text="ثبت نام سمینار" Value="3" />
                                        <dxe:ListEditItem Text="گواهی دوره خارج از نوبت" Value="4" />
                                    </Items>
                                </TSPControls:CustomAspxComboBox>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <dxe:ASPxLabel runat="server" Text="وضعیت اعتراض" ID="ASPxLabel4">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbObjectionType"
                                    ValueType="System.String" EnableIncrementalFiltering="True"
                                    ClientInstanceName="cmbObjectionType" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <Items>
                                        <dxe:ListEditItem Text="بدون اعتراض" Value="0" />
                                        <dxe:ListEditItem Text="دارای اعتراض" Value="1" />
                                    </Items>
                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="4">
                                <br />
                                <table>
                                    <tr>
                                        <td align="left" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text="جستجو" ID="btnSearch" ClientInstanceName="btnSearch" OnClick="btnSearch_OnClick"
                                                AutoPostBack="False" UseSubmitBehavior="False"
                                                Height="25px" Width="92px">
                                                <ClientSideEvents Click="function(s, e) {
          if(CheckSearch()==0)
                       {  
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
                       }
          else
          e.processOnServer=true;

}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" ID="btnClear" OnClick="btnSearch_OnClick"
                                                AutoPostBack="False" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
cmbCourse.SetSelectedIndex(0);
txtMeId.SetText('');
txtFirstName.SetText('');
txtLastName.SetText('');
ComboType.SetSelectedIndex(0);
cmbObjectionType.SetSelectedIndex(0);

}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewPeriodRegister"
        runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewPeriodRegister" runat="server" DataSourceID="OdbMadrak"
        Width="100%"
        ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="ID;Type"
        OnHtmlDataCellPrepared="GridViewPeriodRegister_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewPeriodRegister_AutoFilterCellEditorInitialize"
        OnCustomCallback="GridViewPeriodRegister_CustomCallback" RightToLeft="True">
        <%--<SettingsCookies Enabled="false" />--%>
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn FieldName="TaskId" Caption="مرحله" Name="WFState"
                VisibleIndex="0">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox ValueType="System.String" TextField="TaskName" DataSourceID="ObjdsWorkFlowTask"
                    ValueField="TaskId">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <%--   <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" Visible="False"
                        VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="عنوان مدرک" FieldName="CrsTitle" Name="CrsTitle"
                VisibleIndex="1" Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت عضویت" FieldName="MeStatus" Name="MeStatus"
                VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Name="MeId" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" Name="FirstName"
                VisibleIndex="2">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" Name="LastName"
                VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" Name="CreateDate"
                VisibleIndex="8">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع مدرک" FieldName="TypeName" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <%--             <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PPId" Name="PPId" Visible="False" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="CrsTitle" VisibleIndex="4"
                Width="200px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="PPCode" VisibleIndex="4" Width="100px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ آزمون" FieldName="TestDate" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمره اولیه" FieldName="FirstMark" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمره نهایی" FieldName="Mark" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تغییرات نمره" FieldName="ObjectionChange"
                Width="80" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" ShowFooter="True" />
        <Images>
        </Images>
        <ClientSideEvents EndCallback="function(s, e) { 

if(s.cpError==1)
{          
ShowMessage(s.cpMsg);
s.cpError=0;
s.cpMsg='';
}
else
{
 if(s.cpReqType=='WF')
{
	 ShowWf();
	s.cpReqType='';
}
}
s.cpReqType='';
}" />
    </TSPControls:CustomAspxDevGridView>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_PeriodReg"
        OnCallback="WFUserControl_Callback" />
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent2" runat="server">
                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="BtnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
	else
	{
		grid.PerformCallback('btnEdit');
	}	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="صدور گواهی خارج از نوبت"
                                                    ID="btnNewPeriodReg2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnNewPeriodReg_Click" ClientInstanceName="NewPeriodReg">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/NewPeriodRegOutTime.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="حذف درخواست" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
  if(confirm('آیا مطمئن به حذف این درخواست هستید؟'))
  {
        grid.PerformCallback('btnDelete');
  }
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton4" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
grid.PerformCallback('WF');
   
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton5" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                    ToolTip="پیگیری" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	grid.PerformCallback('Tracing');
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                    ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/ExportExcel.png">
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

    <asp:ObjectDataSource ID="OdbMadrak" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SearchMadrakForManagementPage" TypeName="TSP.DataManager.MadrakManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="CrsId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="PeriodHasObjection" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odbCourseName" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
</asp:Content>
