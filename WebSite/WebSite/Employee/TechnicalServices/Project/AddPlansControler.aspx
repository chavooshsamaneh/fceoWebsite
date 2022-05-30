<%@ Page Title="بازبین نقشه" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddPlansControler.aspx.cs" Inherits="Employee_TechnicalServices_Project_AddPlansControler" %>

<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID='UpdatePanel1' runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
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
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره و ارسال اتوماتیک به بازبین"
                                            ID="btnSaveAndSend" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSaveAndSend_Click" ValidationGroup="Acc">
                                            <ClientSideEvents Click="function(s,e){ e.processOnServer= confirm('پس از ذخیره و ارسال اتوماتیک گردش کار به بررسی بازبین امکان تغییرات بازبین وجود ندارد.آیا مطمئن به ذخیره و ارسال اتوماتیک گردش کار به بررسی بازبین می باشید؟');}" />

                                            <Image Url="~/Images/icons/SaveControlerAndSendWF.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                        <td>
                                                <TSPControls:CustomAspxButton Visible="false" IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار نقشه"
                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/icons/Back.png">
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
            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPlans" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <asp:Panel ID="RoundPanelControler" runat="server" Width="100%">
                            <fieldset>
                                <legend class="HelpUL">بازبین نقشه</legend>
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr runat="server" id="tblTRControlerInfo">
                                            <td>
                                                <table dir="rtl" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td id="Td5" runat="server" valign="top" align="right" style="width: 15%">
                                                                <dxe:ASPxLabel runat="server" Text="بازبین* " Width="100%" ID="ASPxLabel18">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td id="Td6" runat="server" valign="top" align="right" style="width: 35%">
                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="FullName" ID="cmbPlanControler" DataSourceID="ObjdsControler" ValueType="System.String"
                                                                    ValueField="ControlerId" TextFormatString="{1}"
                                                                    EnableIncrementalFiltering="True" RightToLeft="True" OnSelectedIndexChanged="cmbPlanControler_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ValidationSettings Display="Dynamic" ValidationGroup="ValidControler" ErrorTextPosition="Bottom">

                                                                        <RequiredField IsRequired="True" ErrorText="بازبین را انتخاب نمایید"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn FieldName="MeId" Caption="کد عضویت"></dxe:ListBoxColumn>
                                                                        <dxe:ListBoxColumn FieldName="FullName" Caption="نام و نام خانوادگی"></dxe:ListBoxColumn>
                                                                        <dxe:ListBoxColumn FieldName="FileMjName" Caption="رشته"></dxe:ListBoxColumn>                                                                        
                                                                        <dxe:ListBoxColumn FieldName="AgentName" Caption="نمایندگی"></dxe:ListBoxColumn>
                                                                        
                                                                    </Columns>
                                                                </TSPControls:CustomAspxComboBox>
                                                                <asp:ObjectDataSource ID="ObjdsControler" runat="server" SelectMethod="SelectTSControlerByAgent"
                                                                    TypeName="TSP.DataManager.TechnicalServices.ControlerManager">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="-1" Name="ControlerId" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="-1" Name="Type" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="1" Name="AgentIdShiraz" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="0" Name="MajorIdList" Type="String" />
                                                                        <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td id="Td7" runat="server" valign="top" align="right" style="width: 15%"></td>
                                                            <td id="Td8" runat="server" valign="top" align="right" style="width: 35%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" width="15%" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel9">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" width="35%" align="right">
                                                                <TSPControls:CustomTextBox runat="server" ID="txtObsGrade" Width="100%"
                                                                    Enabled="false">
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel1221">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <TSPControls:CustomTextBox runat="server" ID="txtImpGrade" Width="100%"
                                                                    Enabled="false">
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel12">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <TSPControls:CustomTextBox runat="server" ID="txtDesGrade" Width="100%"
                                                                    Enabled="false">
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="ASPxLabel4">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <TSPControls:CustomTextBox runat="server" ID="txtMappingGrade" Width="100%"
                                                                    Enabled="false">
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="ASPxLabel6">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <TSPControls:CustomTextBox runat="server" ID="txtUrbanismGrade" Width="100%"
                                                                    Enabled="false">
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="پایه ترافیک" ID="ASPxLabel8">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <TSPControls:CustomTextBox runat="server" ID="txtTrafficGrade" Width="100%"
                                                                    Enabled="false">
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <fieldset>
                            <legend class="HelpUL">نظرات بازبینی نقشه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" width="100%" align="center" colspan="4">
                                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                ID="GridViewViewPoint" DataSourceID="ObjectDataSourcePlansControlerViewPoint"
                                                KeyFieldName="ViewPointId" AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner">
                                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RowNo" Width="30px" Caption="ردیف">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Subject" Caption="موضوع">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SheetNo" Caption="شماره برگ نقشه">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                        FieldName="FileUrl" Caption="فایل پیوست" Name="ControlerFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                                    </dxwgv:GridViewDataHyperLinkColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ViewPoint" Width="300px"
                                                        Caption="توضیحات بازبینی">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ControllerName" Caption="بازبین"
                                                        Name="ControllerName" Width="150px">
                                                        <CellStyle HorizontalAlign="Right" Wrap="False">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id" Caption="Id">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">اطلاعات پایه نقشه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع نقشه:" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <dxe:ASPxLabel runat="server" ID="txtPlanType" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره نقشه:" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="35%">
                                            <dxe:ASPxLabel runat="server" ID="txtPlanNo" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات:" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" ID="txtPlanDes" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                ID="GridViewAttachment" DataSourceID="ObjectDataSourceAttachments" KeyFieldName="AttachmentId"
                                                AutoGenerateColumns="False" ClientInstanceName="GridViewAttachment">
                                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Title" Caption="نوع فایل">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FileName" Caption="نام فایل">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FileSize" Caption="حجم فایل (KB)">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                        FieldName="FilePath" Caption="لینک" Name="PlanFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                                    </dxwgv:GridViewDataHyperLinkColumn>

                                                </Columns>
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <fieldset>
                            <legend class="HelpUL">طراحان نقشه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="center" width="100%" colspan="4">
                                             <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                        ID="GridViewDesigner" DataSourceID="ObjectDataSourceDesignerPlans" KeyFieldName="DesignerPlansId"
                                                        AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner">
                                                        <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                                        <Columns>
                                                           <%-- <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeType" Caption="نوع طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>--%>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="OfficeEngOId" Caption="کد دفتر/شرکت طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DesignerMeId" Caption="کد عضویت طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="DesignerName" Caption="نام و نام خانوادگی"
                                                                Width="300px">
                                                                <CellStyle Wrap="False">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Wage" Caption="متراژ دستمزد">
                                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="YearName" Caption="تعرفه">
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Year" Caption="سال کاری">
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="DesignerCreateDate" Caption="تاریخ ثبت طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Date" Caption="تاریخ ثبت نقشه">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </TSPControls:CustomAspxDevGridView2>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceDesignerPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                                                        SelectMethod="SelectTSDesignerPlansForByPlanId" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:SessionParameter SessionField="PlansId" Type="Int32" DefaultValue="-1" Name="PlansId"></asp:SessionParameter>
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

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
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <%--<td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>--%>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره و ارسال اتوماتیک به بازبین"
                                            ID="btnSaveAndSend2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSaveAndSend_Click" ValidationGroup="Acc">
                                            <ClientSideEvents Click="function(s,e){ e.processOnServer= confirm('پس از ذخیره و ارسال اتوماتیک گردش کار به بررسی بازبین امکان تغییرات بازبین وجود ندارد.آیا مطمئن به ذخیره و ارسال اتوماتیک گردش کار به بررسی بازبین می باشید؟');}" />
                                            <Image Url="~/Images/icons/SaveControlerAndSendWF.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                          <td>
                                            <TSPControls:CustomAspxButton  Visible="false" IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار نقشه"
                                                ID="btnSendToNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
              <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewPlans" SessionName="SendBackDataTable_EmpAddPln"
                    OnCallback="CallbackPanelWorkFlow_Callback" />
            <dxhf:ASPxHiddenField ID="HiddenFieldPrjDes" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceAttachments" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindByTableTypeId" TypeName="TSP.DataManager.TechnicalServices.AttachmentsManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TableTypeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TableType" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="AttachTypeId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourcePlansControlerViewPoint" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="FindByPlansId" TypeName="TSP.DataManager.TechnicalServices.PlansControlerViewPointManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PlansId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            </div>
                </div>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
