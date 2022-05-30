<%@ Page Title="لیست ارجاع کار ناظران" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ReportObserverSelected.aspx.cs" Inherits="Employee_TechnicalServices_Report_ReportObserverSelected" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <%-- <td>

                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="عدم تایید به دلیل تاخیر در پاسخ"
                                            ID="btnReject" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnReject_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>

                                    </td>--%>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">
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
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="کد عضویت" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" style="width: 35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMeId" runat="server" ClientInstanceName="txtMeId"
                                        Width="100%">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="کد پروژه">
                                    </dxe:ASPxLabel>
                                  
                                </td>
                                <td align="right" style="width: 35%">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtProjectId" runat="server" ClientInstanceName="txtProjectId"
                                        Width="100%">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="کد پروژه را صحیح وارد نمایید" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 104px">
                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ ارجاع از">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right">
                                    <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                        PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                        Width="300px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                </td>
                                <td align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ ارجاع تا">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right">
                                    <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                        PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                        Width="300px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" dir="ltr" valign="top">
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                                    Text="پاک کردن فرم" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                                    Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid')){
 
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
  }
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>


    <br />
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewObserverWorkRequest"
                ExportedRowType="All">
            </dxwgv:ASPxGridViewExporter>
            <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewObserverWorkRequest" runat="server"
                DataSourceID="ObjectDataSourceSelectObs"
                ClientInstanceName="grid" EnableViewState="False" KeyFieldName="ProjectObserverSelectedId" AutoGenerateColumns="False">
                <SettingsText Title="گزارش ارجاع کار ناظران پروژه" />
                <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام عضو" FieldName="MeFullName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تعداد دفعات ارجاع" FieldName="CountObsSelect" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="GrdName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="MjParentName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                       <dxwgv:GridViewDataTextColumn Caption="شهرداری" FieldName="MunName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                       <dxwgv:GridViewDataTextColumn Caption="ناظر هماهنگ کننده" FieldName="CoordinatorObserver" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                       <dxwgv:GridViewDataTextColumn Caption="اسکلت" FieldName="StructureSkeletonTitle" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                       <dxwgv:GridViewDataTextColumn Caption="سال کاری" FieldName="Year" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                       <dxwgv:GridViewDataTextColumn Caption="گروه ساختمانی پروژه" FieldName="GroupName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="متراژ کسر ظرفیت" FieldName="CapacityDecrement" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="IsObserverConfirmedName" Caption="وضعیت تایید"
                        Name="IsConfirmName" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ارجاع" FieldName="CreateDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ساعت ارجاع" FieldName="ModifiedTime" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>



                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>

                                    <%-- <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="عدم تایید به دلیل تاخیر در پاسخ"
                                            ID="btnReject2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnReject_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>--%>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>


                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <dxhf:ASPxHiddenField ID="HDpage" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceSelectObs" runat="server" TypeName="TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager"
                SelectMethod="SearchForManagmentPage" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>

                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                    <asp:Parameter DefaultValue="%" Name="CreateDateTo" Type="String" />
                    <asp:Parameter DefaultValue="%" Name="CreateDateFrom" Type="String" />
                    
                </SelectParameters>
            </asp:ObjectDataSource>
          

</asp:Content>

