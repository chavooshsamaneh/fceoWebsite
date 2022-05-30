<%@ Page Title="لیست پرداخت حق الزحمه ناظرین و مانده واریزی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ProjectObserverReportList.aspx.cs" Inherits="Employee_TechnicalServices_Report_ProjectObserverReportList" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
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
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف لیست"
                                            ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer=confirm('آیا مطمئن به حذف لیست انتخاب شده می باشید؟');		    
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="تغییر وضعیت به چاپ و ارسال به واحد مالی"
                                            ID="btnChangeStatus" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableTheming="False"
                                            OnClick="btnChangeStatus_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer=confirm('آیا مطمئن به تغییر وضعیت لیست جهت چاپ و ارسال به واحد مالی می باشید؟');		    
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/PrintAndSendToaccUnit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ گزارش حق الزحمه نظارت پرونده های تکمیلی"
                                            ID="btnPrintObservaerWage" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/printorange.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                            	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
                                                 {
                                                   e.processOnServer=false;
                                                   alert('ردیفی انتخاب نشده است');
                                                 }
                                                else
                                                 GridViewAccDocument.PerformCallback('PrintObservaerWage');
                                            }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ گزارش خلاصه حق الزحمه ناظران"
                                            ID="btnPrjRemain" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="../../../Images/icons/printpink.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                             if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
                                                 {
                                                   e.processOnServer=false;
                                                   alert('ردیفی انتخاب نشده است');
                                                 }
                                                else
                                                    GridViewAccDocument.PerformCallback('PrintProjectRemain');
                                            }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ گزارش مانده واریزی پروژه ها"
                                            ID="btnPrjRemainSnap" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="../../../Images/icons/printyellow.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                             if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
                                                 {
                                                   e.processOnServer=false;
                                                   alert('ردیفی انتخاب نشده است');
                                                 }
                                                else
                                                    GridViewAccDocument.PerformCallback('PrintProjectRemainSnap');
                                            }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewAccDocument"
                ExportedRowType="All">
            </dxwgv:ASPxGridViewExporter>
            <div align="right">
                <ul class="HelpUL">
                    <li>درک مانده واریزی بسیار مهم است. از نظر واحد مالی سازمان مانده واریزی در هر لیست همان مانده ای است که قبل از آن لیست در هیچ لیستی اعلام نشده است و پس از آن لیست یا صفر شده و یا هنوز مانده خواهد داشت یا به بیان ساده مانده واریزی مجموع تمام فیش های نظارت قبل از تاریخ صدور لیست می باشد که در هیچ لیستی نیامده است </li>
                    <li>تنها امکان حذف لیست هایی که وضعیت ارسال آن ها ''ذخیره '' می باشد وجود دارد.
                    </li>
                    <li>پس از اطمینان از صحت کلیه اطلاعات لیست ذخیره شده،جهت ارائه به واحد مالی وضعیت ارسال
                        لیست را با استفاده از دکمه "تغییر وضعیت به چاپ و ارسال به واحد مالی" تغییر داده.به
                        این ترتیب تاریخ تهیه و شماره لیست در نسخه چاپی نمایش داده می شود </li>
                    <li>جهت گزارش مانده واریزی پروژه نسبت به تاریخ مورد نظر لیست مربوطه را انتخاب نمایید و سپس دکمه چاپ گزارش مانده پروژه را کلیک نمایید</li>
                </ul>
            </div>
            <div align="right">
                <ul class="HelpUL">
                   <span><b>در صورت مشاهده مغایرت در لیست باید تمامی اطلاعات مربوط به پروژه به صورت کامل بررسی شود. برخی از نکاتی که عدم توجه به آنها توسط کاربر باعث مشاهده مغایرت در لیست می شود در زیر ذکر شده است. در صورت مشاهده مغایرت جهت بررسی بازخورد باید تصاویر مبنی بر صحت شش مورد ذیل به همراه بازخورد ارسال گردد در غیر اینصورت قابل پیگیری نمی باشد</b>. </span>
                    <li>مطابقت مبلغ فیش پرداختی با مبلغ پیشنهادی سیستم برای فیش تمامی ناظران باید بررسی شود </li>
                    <li>وضعیت پرداخت تمام فیش های ناظرینی که در لیست می باشند باید پرداخت شده باشد </li>
                    <li>نام تمام ناظرانی که در لیست است با تمام ناظرانی که برای آنها فیش پرداخت شده است مطابقت داشته باشد </li>
                    <li>تاریخ پرداخت فیش بایستی حداقل یک روز قبل از تاریخ ثبت لیست باشد</li>
                       <li>نام ناظر در صورتی که سهم آن به صورت کامل پرداخت شده است تنها در یک لیست آمده باشد</li>
                       <li>در صورت تغییر متراژ پروژه و متراژ ناظران پس از پرداخت فیش، نیاز به ثبت فیش بابت تفاوت متراژ است</li>
                </ul>
            </div>
            <TSPControls:CustomAspxDevGridView ID="GridViewAccDocument" KeyFieldName="AccDocId"
                runat="server" DataSourceID="ObjdObserveAccDocument" Width="100%" ClientInstanceName="GridViewAccDocument"
                OnCustomCallback="GridViewAccDocument_CustomCallback">
                <Settings ShowHorizontalScrollBar="true" />
                <ClientSideEvents EndCallback="function(s,e){
                if(s.cpPrint==1)
                {
                    if(s.cpUrl!='')
                    {      
                    window.open(s.cpUrl);  
                    s.cpUrl='';
                    s.cpPrint=0;
                    }
                }
                }" />
                <Columns>
                    
                    <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام لیست" FieldName="ListName" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره لیست" FieldName="ListNo" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع لیست" FieldName="TypeName" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ تهیه لیست" FieldName="ListDate" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت ارسال" FieldName="StatusName" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="1"
                        Width="150px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="1"
                        Width="250px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdObserveAccDocument" runat="server" SelectMethod="FindByAgentId"
                TypeName="TSP.DataManager.TechnicalServices.AccountingDocumentManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters><asp:Parameter Name="AgentId" DefaultValue="-1" Type="Int32" /> </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
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
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف لیست"
                                            ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer=confirm('آیا مطمئن به حذف لیست انتخاب شده می باشید؟');		    
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="تغییر وضعیت به چاپ و ارسال به واحد مالی"
                                            ID="btnChangeStatus2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableTheming="False"
                                            OnClick="btnChangeStatus_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer=confirm('آیا مطمئن به تغییر وضعیت لیست جهت چاپ و ارسال به واحد مالی دارید؟');		    
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/PrintAndSendToaccUnit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ گزارش حق الزحمه نظارت پرونده های تکمیلی"
                                            ID="btnPrintObservaerWage1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/printorange.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                         GridViewAccDocument.PerformCallback('PrintObservaerWage');
                                            }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ گزارش خلاصه حق الزحمه ناظران"
                                            ID="btnPrjRemain2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="../../../Images/icons/printpink.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                         GridViewAccDocument.PerformCallback('PrintProjectRemain');
                                            }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ گزارش مانده واریزی پروژه ها"
                                            ID="btnPrjRemainSnap2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="../../../Images/icons/printyellow.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                             if (GridViewAccDocument.GetFocusedRowIndex()&lt;0)
                                                 {
                                                   e.processOnServer=false;
                                                   alert('ردیفی انتخاب نشده است');
                                                 }
                                                else
                                                    GridViewAccDocument.PerformCallback('PrintProjectRemainSnap');
                                            }" />
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
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
