<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="CourseDetails.aspx.cs" Inherits="Members_Amoozesh_CourseDetails" Title="ارتباط با پروانه" %>

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

        function SetControlValuesGrade() {
            Gradegrid.GetRowValues(Gradegrid.GetFocusedRowIndex(), 'GMRId;GrdName;ResName;MjName', SetValueGrade);
        }
        function SetValueGrade(values) {
            TextMajorID.SetText(values[0]);
            TextGrd.SetText(values[1]);
            TextRes.SetText(values[2]);
            TextMj.SetText(values[3]);
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS" WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS" FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS" CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
            </pdc:PersianDateScriptManager>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table >
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                            <image url="~/Images/icons/Back.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetails" runat="server" OnItemClick="MenuCourseDetails_ItemClick">
                <Items>

                    <dxm:MenuItem Name="Course" Text="مشخصات درس">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseRefrence" Text="منابع درس">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه بندی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <div dir="rtl">
                <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" ID="CustomAspxDevGridViewGrade" DataSourceID="ObjectDataSourceGrid" KeyFieldName="TrGrId" AutoGenerateColumns="False" ClientInstanceName="grid">
                    <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm"></SettingsEditing>

                    <ClientSideEvents RowDblClick="function(s, e) {
}"></ClientSideEvents>


                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="TrGrId"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActiveName" Caption="وضعیت"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>

                </TSPControls:CustomAspxDevGridView>
                <br />
                <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                <image url="~/Images/icons/Back.png"></image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" TypeName="TSP.DataManager.TrainingAcceptedGradeManager" SelectMethod="FindByPKCode">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="PkId" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="Type" Type="Byte" />
                    </SelectParameters>
                </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>

