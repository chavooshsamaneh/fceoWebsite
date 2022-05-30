<%@ Page Title="لیست ارجاع کار ناظران" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverSelected.aspx.cs" Inherits="Members_TechnicalServices_Project_ObserverSelected" %>


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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                    <td>

                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="رد کار" ToolTip="رد کار"
                                            ID="CustomAspxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnReject_Click">

                                            <ClientSideEvents Click="function(s,e){
                                                     if (grid.GetFocusedRowIndex()&lt;0)
 	                                                {
   		                                                e.processOnServer=false;
   		                                                alert('ابتدا یک ردیف را از جدول انتخاب نمایید');
	                                                }
                                                if(HDpage.Get('CountRejectByObs')=='0')
                                                 {
                                                     e.processOnServer= confirm('مهندس ناظر محترم؛ به استناد نظام نامه شورای مرکزی سازمان، شما مجاز به یک بار رد کار در مدت نظارت خود هستید و در دفعات بعد به ازای هر بار رد کار ارجاع شده، یک کار مجازی به مدت شش ماه در مجموع تعداد کارهای شما منظور خواهد شد.آیا از رد کار مطمئن می باشید؟');
                                                 }
                                                else{ if(HDpage.Get('CountRejectByObs')!='0')
                                                 {
                                                      e.processOnServer= confirm('مهندس ناظر محترم؛ به استناد نظام نامه شورای مرکزی سازمان در خصوص جریمه رد کار ارجاع شده نظارت، از امروز به مدت شش ماه یک کار مجازی در مجموع تعداد کارهای شما منظور خواهد شد.بدیهی است مدیریت تعداد کارهای باقی مانده به عهده جنابعالی است.آیا از رد کار مطمئن هستید؟');
                                                 }else
                                                    e.processOnServer=false;
                                                }
                                                    }" />


                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="قبول کار" ToolTip="قبول کار"
                                            ID="CustomAspxButton2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnConfirm_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <ul runat="server" id="ULAlarm" class="HelpUL">
                <li><b>مهندس ناظر محترم؛ به استناد نظام نامه شورای مرکزی سازمان، شما مجاز به یک بار رد کار در مدت نظارت خود هستید و در دفعات بعد به ازای هر بار رد کار ارجاع شده، یک کار مجازی به مدت شش ماه در مجموع تعداد کارهای شما منظور خواهد شد.بدیهی است مدیریت تعداد کارهای باقی مانده به عهده جنابعالی است.
                </b></li>

            </ul>
            <%--       <div>
                <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" Font-Size="15pt" ClientInstanceName="lblWarning" ID="lblWarning" Text="">
                </dxcp:ASPxLabel>
            </div>--%>

            <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewObserverSelected" runat="server"
                DataSourceID="ObjectDataSourceSelectObs"
                ClientInstanceName="grid" EnableViewState="False" KeyFieldName="ProjectObserverSelectedId" AutoGenerateColumns="False">
                <SettingsText Title="لیست ارجاع کار ناظران پروژه" />
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
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ارجاع" FieldName="CreateDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ رد کار" FieldName="RejectDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="متراژ کل پروژه" FieldName="Foundation" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="گروه ساختمانی" FieldName="GroupName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
  <dxwgv:GridViewDataTextColumn Caption="ناظر هماهنگ کننده" FieldName="CoordinatorObserver" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="متراژ کسر ظرفیت" FieldName="CapacityDecrement" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهر" FieldName="CitName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نشانی" Width="300px" FieldName="Address" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام مالک" FieldName="OwnerFullName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره همراه مالک" FieldName="OwnerMobileNo" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>


                    <dxcp:GridViewDataTextColumn FieldName="IsObserverConfirmedName" Caption="وضعیت پذیرش کار توسط ناظر"
                        Name="IsConfirmName" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataTextColumn>



                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <br />


            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>

                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="رد کار" ToolTip="رد کار"
                                            ID="btnReject2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnReject_Click">
                                            <ClientSideEvents Click="function(s,e){
                                                     if (grid.GetFocusedRowIndex()&lt;0)
 	                                                {
   		                                                e.processOnServer=false;
   		                                                alert('ابتدا یک ردیف را از جدول انتخاب نمایید');
	                                                }
                                                if(HDpage.Get('CountRejectByObs')=='0')
                                                 {
                                                     e.processOnServer= confirm('مهندس ناظر محترم؛ به استناد نظام نامه شورای مرکزی سازمان، شما مجاز به یک بار رد کار در مدت نظارت خود هستید و در دفعات بعد به ازای هر بار رد کار ارجاع شده، یک کار مجازی به مدت شش ماه در مجموع تعداد کارهای شما منظور خواهد شد.آیا از رد کار مطمئن می باشید؟');
                                                 }
                                                else{ if(HDpage.Get('CountRejectByObs')!='0')
                                                 {
                                                      e.processOnServer= confirm('مهندس ناظر محترم؛ به استناد نظام نامه شورای مرکزی سازمان در خصوص جریمه رد کار ارجاع شده نظارت، از امروز به مدت شش ماه یک کار مجازی در مجموع تعداد کارهای شما منظور خواهد شد.بدیهی است مدیریت تعداد کارهای باقی مانده به عهده جنابعالی است.آیا از رد کار مطمئن هستید؟');
                                                 }else
                                                    e.processOnServer=false;
                                                }
                                                    }" />

                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="قبول کار" ToolTip="قبول کار"
                                            ID="btnConfirm" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnConfirm_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <dxhf:ASPxHiddenField ID="HDpage" runat="server" ClientInstanceName="HDpage">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceSelectObs" runat="server" TypeName="TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager"
                SelectMethod="SearchForManagmentPage" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>

                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


