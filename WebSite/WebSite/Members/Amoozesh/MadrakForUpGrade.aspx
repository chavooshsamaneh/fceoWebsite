<%@ Page Title="مدیریت مدارک دوره/همایش خارج از سازمان" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MadrakForUpGrade.aspx.cs" Inherits="Members_Amoozesh_MadrakForUpGrade" %>

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
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkButton1" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">مدرک جدید</asp:LinkButton>

                            </td>

                            <td>
                                <asp:LinkButton ID="btnView" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده مدرک</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewPeriodRegister" runat="server" DataSourceID="OdbMadrak"
        Width="100%"
        ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="MdId">
        <Columns>
             <%--    <dxwgv:GridViewDataComboBoxColumn FieldName="TaskId" Caption="مرحله" Name="WFState"
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
            </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" Name="CreateDate"
                VisibleIndex="8">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <%--  <dxwgv:GridViewDataTextColumn Caption="نوع مدرک" FieldName="TypeName" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            --%>
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
            <%-- <dxwgv:GridViewDataTextColumn Caption="نمره اولیه" FieldName="FirstMark" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمره نهایی" FieldName="Mark" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تغییرات نمره" FieldName="ObjectionChange"
                Width="80" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" ShowFooter="True" />
    </TSPControls:CustomAspxDevGridView>

    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent2" runat="server">
                <table>
                    <tbody>
                         <tr>
                            <td>
                                <asp:LinkButton ID="BtnNew2" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">مدرک جدید</asp:LinkButton>

                            </td>

                            <td>
                                <asp:LinkButton ID="btnView2" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده مدرک</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="OdbMadrak" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="FindMemberMadraks" TypeName="TSP.DataManager.MadrakManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odbCourseName" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
</asp:Content>

