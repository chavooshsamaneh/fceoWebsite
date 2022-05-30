<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberCapacityUserControl.ascx.cs"
    Inherits="UserControl_MemberCapacityUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<fieldset id="RoundPanelCapacity" runat="server">
    <legend class="HelpUL" id="RoundPanelCapacityHeader" runat="server">مشخصات عضو</legend>
    <table width="100%">
        <tbody>

            <tr>
                <td>
                    <dxe:ASPxLabel runat="server" Text="مقدار افزایش/کاهش ظرفیت طراحی:" ID="lblConditionalCapacityDesign">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtConditionalCapacityDesign" Width="100%"
                        Font-Bold="true" RightToLeft="True">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="مقدار افزایش/کاهش ظرفیت نظارت:" ID="lblConditionalCapacityObservation">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtConditionalCapacityObservation" Width="100%"
                        Font-Bold="true" RightToLeft="True">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>

                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="مجموع نظارت و طراحی قابل ثبت در سیستم :" ID="lblRemainCapacity">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtRemainCapacity" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="ظرفیت باقیمانده نظارت واقعی:" ID="ASPxLabel6">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtRemainCapacityObsReal" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
            </tr>      

            <tr>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="حداکثر تعداد کار مجاز:" ID="ASPxLabel1" Visible="false">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtMaxJobCount" Width="100%" Font-Bold="true"  Visible="false">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="تعداد کار باقیمانده:" ID="ASPxLabeProjectCount"  Visible="false">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtCountRemainWorkCount" Width="100%" Font-Bold="true"  Visible="false">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="تعداد پروژه طراحی زیر 400 متر:" ID="ASPxLabel2">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtCountUnder400MeterWorkDesign" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="تعداد پروژه نظارت زیر 400 متر:" ID="ASPxLabel4">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel runat="server" Text="---" ID="txtCountUnder400MeterWork" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
            </tr>     
        </tbody>
    </table>
    <TSPControls:CustomAspxDevGridView2 ID="GridViewProject" runat="server" Width="100%"
        ClientInstanceName="GridViewProject" DataSourceID="ObjectDataSourceReportMemberWageByCity"
        KeyFieldName="CitId" AutoGenerateColumns="False">
        <SettingsCookies Enabled="true" />
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="SumWage" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="SumCapacityDecrement" SummaryType="Sum" />
        </TotalSummary>
        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="200px" FieldName="CitName"
                Caption="شهر" Name="CitName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="220px" FieldName="ProjectIngridientResName"
                Caption="مسئولیت" Name="ProjectIngridientResName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="220px" FieldName="ProjectIngridientDecreaseTypeName"
                Caption="زمینه کسر ظرفیت" Name="ProjectIngridientDecreaseTypeName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SumWage" Caption="مجموع کسر دستمزد"
                Name="CapacityDecrement" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="False" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SumCapacityDecrement" Caption="مجموع کسر ظرفیت"
                Name="SumCapacityDecrement" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="False" />
            </dxwgv:GridViewDataTextColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView2>

    <asp:ObjectDataSource ID="ObjectDataSourceReportMemberWageByCity" runat="server" SelectMethod="ReportMemberWageByCity" TypeName="TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="-2" Name="MeId" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="ProjectIngridientTypeId" />
        </SelectParameters>

    </asp:ObjectDataSource>
</fieldset>

