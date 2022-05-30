<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeLetters.aspx.cs" Inherits="Employee_OfficeRegister_OfficeLetters"
    Title="مدیریت روزنامه های رسمی شرکت" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function SetControlValues() {
            //alert("1");
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'LetterNo;PageNo;Date;Description', SetValue);
        }
        function SetValue(values) {
            TextLeNo.SetText(values[0]);
            TextPageNo.SetText(values[1]);
            document.getElementById('<%=txtLeDate.ClientID%>').value = values[2];
            TextDesc.SetText(values[3]);

        }
        function SetEmpty() {
            TextLeNo.SetText("");
            document.getElementById('<%=txtLeDate.ClientID%>').value = "";
            TextPageNo.SetText("");
            TextDesc.SetText("");

        }
        function Enable() {
            TextLeNo.SetEnabled(true);
            document.getElementById('<%=txtLeDate.ClientID%>').SetEnabled(true);
            TextPageNo.SetEnabled(true);
            TextDesc.SetEnabled(true);

        }
        function Disable() {
            TextLeNo.SetEnabled(false);
            document.getElementById('<%=txtLeDate.ClientID%>').SetEnabled(false);
            TextPageNo.SetEnabled(false);
            TextDesc.SetEnabled(false);

        }
        function HasError() {
            if (TextLeNo.GetIsValid() && TextPageNo.GetIsValid() && TextLeDate.GetIsValid())
                return false;
            return true;
        }


        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
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
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	//pop.Show();
	grid.cpError=0;
grid.AddNewRow();
	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);

 }
 else
{
  //pop.Show();
	
	grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
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
                                            CausesValidation="False" ID="btnView" UseSubmitBehavior="False" Visible="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  pop.Show();
	
	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            ID="ASPxButton2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callmenu"
                OnCallback="ASPxCallbackPanel1_Callback" Width="100%" HideContentOnCallback="False">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                            OnItemClick="ASPxMenu1_ItemClick">
                            <Items>
                                <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="اعضا" Name="Member">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="شعبه ها" Name="Agent">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="سوابق کاری" Name="Job">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters" Selected="true">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مستندات" Name="Attach">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Group" Text="گروه ها">
                                </dxm:MenuItem>
                            </Items>

                        </TSPControls:CustomAspxMenuHorizontal>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>

            <br />
            <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
            <br />
            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="لطفا در صورت وجود اطلاعات روزنامه های مربوط به سال جاری را وارد نمایید."
                ForeColor="Red">
            </dxe:ASPxLabel>
            <br />
            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="OdbOfLetters"
                Width="100%"
                OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnRowUpdating="CustomAspxDevGridView1_RowUpdating"
                OnRowInserting="CustomAspxDevGridView1_RowInserting" OnCustomDataCallback="CustomAspxDevGridView1_CustomDataCallback"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" KeyFieldName="OlId"
                AutoGenerateColumns="False" ClientInstanceName="grid" OnCustomCallback="CustomAspxDevGridView1_CustomCallback"
                RightToLeft="True">
                <ClientSideEvents EndCallback="function(s, e) {
if(grid.cpDoPrint==1)
{
grid.cpDoPrint=0;
window.open(&quot;../../Print.aspx&quot;);
}
else
{
if(grid.cpError==2)
SetTaskOrderError(grid.cpErrorMsg);

if(grid.cpMenu==1)
	callmenu.PerformCallback('');
}	
}"
                    BeginCallback="function(s, e) {
	grid.cpMenu=0;
}"></ClientSideEvents>
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OlId" Name="OlId" VisibleIndex="0"
                        Width="40px">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره روزنامه" FieldName="LetterNo" Name="LetterNo"
                        VisibleIndex="1" Width="60px">
                        <PropertiesTextEdit Width="100px">
                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="شماره روزنامه را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditCellStyle HorizontalAlign="Right">
                        </EditCellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره صفحه" FieldName="PageNo" Name="PageNo"
                        VisibleIndex="2" Width="60px">
                        <PropertiesTextEdit Width="100px">
                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,3}"></RegularExpression>
                                <RequiredField IsRequired="True" ErrorText="شماره صفحه را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditCellStyle HorizontalAlign="Right">
                        </EditCellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" Name="Date" VisibleIndex="3"
                        Width="80px">
                        <PropertiesTextEdit Width="100px">
                            <MaskSettings Mask="1300/00/00" ErrorText="تاریخ را با فرمت--/--/--13 وارد نمایید"></MaskSettings>
                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RegularExpression ErrorText=""></RegularExpression>
                                <RequiredField IsRequired="True" ErrorText="تاریخ را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                            <Style HorizontalAlign="Left" CssClass="LeftToRightDirection"></Style>
                        </PropertiesTextEdit>
                        <EditCellStyle HorizontalAlign="Right">
                        </EditCellStyle>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="OfReId" Visible="False" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="4"
                        Width="320px">
                        <PropertiesTextEdit Width="300px">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="5"
                        Width="50px">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                        
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                </SettingsEditing>

            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
grid.cpError=0;
grid.AddNewRow();
	//pop.Show();
	//pop.SetHeaderText('جدید');
	//Enable();
	//HDMode.Set(&quot;Mode&quot;,HDLetterId.Get(&quot;New&quot;));
	
	//SetEmpty();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);

 }
 else
{
  //pop.Show();
	
	grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
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
                                            ID="btnView2" UseSubmitBehavior="False" Visible="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  pop.Show();	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            ID="ASPxButton3" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                            CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"
                ClientInstanceName="pop" HeaderText="جدید"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="True"
                CloseAction="CloseButton" Modal="True">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table style="text-align: right" dir="rtl">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; width: 69px">
                                        <asp:Label runat="server" Text="شماره روزنامه :" Width="79px" ID="Label50"></asp:Label>
                                    </td>
                                    <td style="width: 309px">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="120px" ID="txtLeNo" ClientInstanceName="TextLeNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="شماره روزنامه را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 69px">
                                        <asp:Label runat="server" Text="شماره صفحه :" Width="80px" ID="Label51"></asp:Label>
                                    </td>
                                    <td style="width: 309px">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="120px" ID="txtLePageNo"
                                            ClientInstanceName="TextPageNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شماره صفحه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="شماره صفحه را به طور صحیح وارد نمایید" ValidationExpression="\d{1,3}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 69px">
                                        <asp:Label runat="server" Text="تاریخ :" ID="Label52"></asp:Label>
                                    </td>
                                    <td style="width: 309px">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="120px" ID="txtLeDate" ClientInstanceName="TextLeDate">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="تاریخ را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="تاریخ را با فرمت--/--/--13 وارد نمایید" ValidationExpression="\d{4}/\d{2}/\d{2}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 69px">
                                        <asp:Label runat="server" Text="توضیحات :" ID="Label53"></asp:Label>
                                    </td>
                                    <td style="width: 309px">
                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" Width="308px" ID="txtLeDesc"
                                            ClientInstanceName="TextDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                        <br />
                                        <TSPControls:CustomAspxButton runat="server" Text="ذخیره" ID="btnSave"
                                            OnClick="btnSave_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
if(!HasError())
{
pop.Hide();

	
}
else
  e.processOnServer=false;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
    <dxhf:ASPxHiddenField ID="LetterId" runat="server">
    </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField ID="LetterMode" runat="server">
    </dxhf:ASPxHiddenField>
    <asp:HiddenField ID="OfficeId" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
    <%--</contenttemplate>
    </asp:UpdatePanel>--%>
    <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>--%>
    <%--</contenttemplate>
        </asp:UpdatePanel>--%>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="OdbOfLetters" runat="server" TypeName="TSP.DataManager.OfficialLetterManager"
        SelectMethod="FindByOffRequest" FilterExpression="OfId={0}" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" UpdateMethod="Update">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <DeleteParameters>
            <asp:Parameter Name="Original_OlId" Type="Int32" />
            <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
        </DeleteParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="OfId" Type="Int32" />
            <asp:Parameter Name="LetterNo" Type="String" />
            <asp:Parameter Name="PageNo" Type="Byte" />
            <asp:Parameter Name="Date" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="UserId" Type="Int32" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="OfId" Type="Int32" />
            <asp:Parameter Name="LetterNo" Type="String" />
            <asp:Parameter Name="PageNo" Type="Byte" />
            <asp:Parameter Name="Date" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="UserId" Type="Int32" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
            <asp:Parameter Name="Original_OlId" Type="Int32" />
            <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
            <asp:Parameter Name="OlId" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
    </dxhf:ASPxHiddenField>
    </div>
</asp:Content>
