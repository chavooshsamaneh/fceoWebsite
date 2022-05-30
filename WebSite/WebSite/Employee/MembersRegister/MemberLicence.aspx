<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    EnableEventValidation="true" AutoEventWireup="true" CodeFile="MemberLicence.aspx.cs"
    Inherits="Employee_MembersRegister_MemberLicence" Title="مدارک تحصیلی" %>

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
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table >
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">
                                                
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                 ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                            
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>  <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش و غیرفعال"
                                                 ID="btnEditInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEditInActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                               
                                                <Image  Url="~/Images/icons/EditAndInActive.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                              
                                                <Image  Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال/حذف"
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="استعلام و تأیید مدرک"
                                                ID="btnConfirm" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnConfirm_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                               
                                                <Image  Url="~/Images/icons/User comment.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {                                                          
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 ShowWf();
 }
}" />
                                               
                                                <Image  Url="~/Images/icons/reload.png"  />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                ID="ASPxButton4" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
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
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCardRequest" runat="server" CausesValidation="False"
                                                 EnableTheming="False" EnableViewState="False"
                                                Text=" " ToolTip="چاپ درخواست کارت عضویت" UseSubmitBehavior="False" Visible="true"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    grid.PerformCallback('PrintRequestCard;'+grid.GetFocusedRowIndex());
 	
}" />
                                              
                                                <Image  Url="~/Images/icons/printCardRequest.png"  />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ درخواست استعلام مدرک تحصیلی"
                                                ID="btnInqueryPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
	    grid.PerformCallback('PrintInquery;'+grid.GetFocusedRowIndex());
}"></ClientSideEvents>
                                               
                                                <Image  Url="~/Images/icons/PrintInquery.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                               
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                                CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                             
                                                <Image  Url="../../Images/icons/BakToManagment.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  
                        AutoSeparators="RootOnly"  ItemSpacing="0px"
                        OnItemClick="ASPxMenu1_ItemClick" SeparatorColor="#A5A6A8" SeparatorHeight="100%"
                        SeparatorWidth="1px" ItemImagePosition="right" Font-Size="11px">
                        <Items>
                            <dxm:MenuItem Name="Request" Text="مشخصات عضو">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Language" Text="زبان ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attach" Text="مستندات">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مالی" Name="AccFish">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Message" Text="پیام ها" Visible="false">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer">
                            </dxm:MenuItem>
                        </Items>
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-2" LastItemX="-1" LastItemY="-2"
                            X="-1" Y="-2" />
                        <Border BorderColor="#A5A6A8" BorderStyle="Solid" BorderWidth="1px" />
                        <VerticalPopOutImage Height="8px" Width="4px" />
                        <ItemStyle ImageSpacing="5px" PopOutImageSpacing="7px" VerticalAlign="Middle" />
                        <SubMenuItemStyle ImageSpacing="7px">
                        </SubMenuItemStyle>
                        <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1" />
                        <HorizontalPopOutImage Height="7px" Width="7px" />
                    </TSPControls:CustomAspxMenuHorizontal>
            
                <br />
                <uc2:MemberInfoUserControl ID="MemberInfoUserControl" runat="server" />
                <div align="right" style="width: 100%">
                    <ul class="HelpUL">
                        <li>درصوتی که قصد تغییر اطلاعات و یا تغییر وضعیت استعلام و یا تغییر وضعیت پیش فرض مدارکی
                            که در درخواست های پیشین ثبت و یا استعلام شده اند را دارید ، جهت حفظ سوابق در سیستم
                            بایستی ابتدا مدرک را غیرفعال نموده و سپس مدرک را مجددا به صورت صحیح وارد نمایید.
                        </li>
                    </ul>
                </div>
                <TSPControls:CustomAspxDevGridView ID="GridViewMemberLicence" runat="server" Width="100%"
                    OnHtmlRowPrepared="GridViewMemberLicence_HtmlRowPrepared" ClientInstanceName="grid"
                    OnAutoFilterCellEditorInitialize="GridViewMemberLicence_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="GridViewMemberLicence_HtmlDataCellPrepared" OnCustomCallback="GridViewMemberLicence_CustomCallback">
                    <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                    <Columns>
                           <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InActiveName" Width="60px"
                            Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn FieldName="ImageURL" Caption="تصویر" VisibleIndex="0"
                            Width="150px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn FieldName="InquiryImageURL" Caption="تصویر استعلام"
                            VisibleIndex="0" Width="150px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataTextColumn Width="50px" VisibleIndex="0" Caption="ریزنمرات">
                                <DataItemTemplate>
                                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server"  Text='<%# Bind("HasScoreImage") %>' Target="_blank"
                                        NavigateUrl='<%# Bind("ScoresImageURL") %>' >
                                    </dxe:ASPxHyperLink>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn FieldName="EntranceExamConfImageURL" Caption="جذب ازطریق آزمون سراری"
                            VisibleIndex="0" Width="100px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn FieldName="EquivalentImageURL" Caption="نامه معادلسازی"
                            VisibleIndex="0" Width="100px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="DefaultValue" Width="60px"
                            Caption="پیش فرض">
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlId" Name="MlId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LiName" Width="150px" Caption="مقطع">
                            <CellStyle Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Width="150px" Caption="رشته">
                            <CellStyle Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjCode" Width="80px" Caption="کد پروانه">
                            <CellStyle Wrap="true" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="UnName" Width="200px" Caption="دانشگاه">
                            <CellStyle Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="CitName"
                            Caption="شهر">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Avg" Width="60px" Caption="معدل">
                              <PropertiesTextEdit DisplayFormatString="f2">
                                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="EndDate" Caption="تاریخ فارغ التحصیلی"
                            Name="EndDate" Width="110px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Inquiry" Caption="استعلام">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="confirm" Caption="نوع تأیید">
                        </dxwgv:GridViewDataTextColumn>
                     
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="MReId"
                            Name="MReId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="9" Width="30px" Caption=" " ShowClearFilterButton="true">
                    
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="MeId">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="True"></Settings>
                    <ClientSideEvents EndCallback="function(s, e) {
   if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
if(grid.cpDoPrint == 1)
{
	grid.cpDoPrint = 0;
	window.open(&quot;../../Printdt.aspx&quot;);
}
if(grid.cpDoPrintRequestCard == 1)
{
    grid.cpDoPrintRequestCard = 0;
    window.open(grid.cpPrintRequestCardPath);
    grid.cpPrintRequestCardPath='';
}

if(grid.cpDoPrintInquery == 1)
{
    grid.cpDoPrintInquery = 0;
    window.open(grid.cpPrintInqueryPath);
    grid.cpPrintInqueryPath='';
}
}" />
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table >
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">
                                               
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                 ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                              
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش و غیرفعال"
                                                 ID="btnEditInActive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEditInActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                               
                                                <Image  Url="~/Images/icons/EditAndInActive.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                             
                                                <Image  Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال/حذف"
                                                ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
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
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="استعلام و تأیید مدرک"
                                                ID="btnConfirm2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnConfirm_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                              
                                                <Image  Url="~/Images/icons/User comment.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendToNexStep" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {                                                             
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 ShowWf();
 }
}" />
                                               
                                                <Image  Url="~/Images/icons/reload.png"  />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator9"></TSPControls:MenuSeprator>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                ID="ASPxButton3" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
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
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCardRequest2" runat="server" CausesValidation="False"
                                                 EnableTheming="False" EnableViewState="False"
                                                Text=" " ToolTip="چاپ درخواست کارت عضویت" UseSubmitBehavior="False" Visible="true"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    grid.PerformCallback('PrintRequestCard;'+grid.GetFocusedRowIndex());
 	
}" />
                                                
                                                <Image  Url="~/Images/icons/printCardRequest.png"  />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ درخواست استعلام مدرک تحصیلی"
                                                ID="btnInqueryPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
	   grid.PerformCallback('PrintInquery;'+grid.GetFocusedRowIndex());
}"></ClientSideEvents>
                                                <Image  Url="~/Images/icons/PrintInquery.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                                CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                <Image  Url="../../Images/icons/BakToManagment.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    
     
        <asp:HiddenField ID="MemberId" runat="server" />
        <asp:HiddenField ID="PgMode" runat="server" />
        <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
        <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
        <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_MeConf"
            OnCallback="WFUserControl_Callback" />
 
</asp:Content>
