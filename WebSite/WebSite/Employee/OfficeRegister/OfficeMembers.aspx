<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeMembers.aspx.cs" Inherits="Employee_OfficeRegister_OfficeMembers"
    Title="اعضای شرکت" %>

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
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">

                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click">

                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">

                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>

                                            <Image  Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                            ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>

                                            <Image  Url="~/Images/icons/active.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            ID="ASPxButton2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Printdt.aspx&quot;);	
}"></ClientSideEvents>

                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <Image  Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">

                                            <Image  Url="~/Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal  ID="ASPxMenu1" runat="server"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضا" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="شعبه ها" Name="Agent">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوابق کاری" Name="Job">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مستندات" Name="Attach">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه ها">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
            <div style="width: 100%" align="right">
                <ul class="HelpUL">
                    <%--          <li>تنها نام اعضایی از شرکت که عضو سازمان بوده و دارای پروانه اشتغال هستند پشت پروانه
                            اشتغال درج می گردد. </li>--%>
                    <li><b>در صورتی که شرکت دارای پروانه باشد ،از آنجایی که تغییرات اعضای شرکت (بجز سهامدارن) نیازمند چاپ
                            گواهینامه اشتغال شرکت می باشد لذا جهت اعمال تغییرات در اعضای شرکت (بجز سهامدارن) تنها از طریق درخواست
                            تغییرات در واحد پروانه می توان اقدام نمود.</b></li>
                    <li><b>درصورتی که تنها قصد تغییر سهامداران شرکت را دارید از درخواست تغییرات اطلاعات پایه و سهامداران استفاده نمایید.</b></li>
                    <li>درصورتی که درخواست ثبت شده شما "درخواست تغییر اطلاعات پایه" باشد با توجه به این
                            که در این نوع درخواست تاییدیه مسکن وجود ندارد، و همچنین با توجه به این که اطلاعات
                            اعضای شرکت درگواهینامه چاپ شده و تغییر آن نیاز به چاپ مجدد و در نتیجه تایید مسکن
                            دارد، امکان تغییر اطلاعات اعضای شرکت وجود ندارد. </li>
                    <li>درصورتی که قصد تغییر اطلاعات اعضای شرکت را دارید از درخواست تغییر اطلاعات پایه استفاده
                            ننمایید. </li>
                    <li>بر اساس ماده 6 بند 6-1-1 و ماده 9 بند 9-1-1 کتاب مبحث دوم و طبق قانون تجارت ذکر
                            شده توسط اداره ثبت شرکت ها و مالکیت صنعتی ، مجموع تعداد سهامداران و اعضای هیئت مدیره
                            شرکت های سهامی خاص بایستی حداقل سه نفر باشد.</li>
                    <li>بر اساس ماده 6 بند 6-1-1 و ماده 9 بند 9-1-1 کتاب مبحث دوم و طبق قانون تجارت ذکر
                            شده توسط اداره ثبت شرکت ها و مالکیت صنعتی ، مجموع تعداد سهامداران و اعضای هیئت مدیره
                            شرکت های مسئولیت محدود بایستی حداقل دو نفر باشد. </li>
                    <li>طبق ماده 6 بند 6-1-4 کتاب مبحث دوم حداقل دو نفر از اعضای شرکت طراح و ناظر باید دارای
                            پروانه اشتغال به کار باشند </li>
                    <li>براساس ماده 6 بند 6-5-1 بایستی مدیرعامل شرکت طراح و ناظر دارای پروانه اشتغال به
                            کار طراحی باشد و بنابراین بایستی عضو نظام مهندسی باشد. </li>
                    <li>طبق ماده 9 بند 9-1-4 کتاب مبحث دوم حداقل دو نفر از اعضای شرکت اجرایی باید دارای
                            پروانه اشتغال به کار باشند </li>
                    <li><b>در صورت تمایل به اطلاع از پایه و صلاحیت اعضای کاردان لطفا بر روی دکمه مشاهده کلیک نمایید</b></li>
                </ul>
            </div>
            <TSPControls:CustomAspxDevGridView ID="DevGridViewOfficeMember" ClientInstanceName="grid"
                runat="server" Width="100%"
                AutoGenerateColumns="False" KeyFieldName="OfmId" OnHtmlRowPrepared="DevGridViewOfficeMember_HtmlRowPrepared"
                OnHtmlDataCellPrepared="DevGridViewOfficeMember_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="DevGridViewOfficeMember_AutoFilterCellEditorInitialize"
                OnCustomCallback="DevGridViewOfficeMember_CustomCallback">
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" FieldName="OfmId" Name="OfmId">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Visible="False" FieldName="OfmId" Name="OfpId">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" FieldName="OfmType" Name="OfmType">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" FieldName="OfReId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="نوع">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OffMeId" Caption="کد عضویت"
                        Name="PersonId">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfpName" Caption="سمت">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Active" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FirstName" Caption="نام">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="LastName" Caption="نام خانوادگی">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="FileNo" Caption="شماره پروانه"
                        Name="FileNo">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="FileDate" Caption="تاریخ اعتبار پروانه"
                        Name="FileNo">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="HasEfficientGradeName" Caption="وضعیت امتیاز"
                        Name="HasEfficientGradeName">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="DesGrdName" Caption="پایه طراحی در نظام مهندسی">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="ObsGrdName" Caption="پایه نظارت در نظام مهندسی">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="ImpGrdName" Caption="پایه اجرادر نظام مهندسی">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="16" Width="30px" ShowClearFilterButton="true">
            
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True"></Settings>
                <ClientSideEvents EndCallback="function(s, e) {
if(grid.cpDoPrint==1)
{
    grid.cpDoPrint = 0;
    window.open('../../Printdt.aspx');
}  
}" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            ID="btnInActive1" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
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
                                            <Image  Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                            ID="btnActive1" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/active.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            ID="ASPxButton3" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Printdt.aspx&quot;);	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                            CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
            <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
