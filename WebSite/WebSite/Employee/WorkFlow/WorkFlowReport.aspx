<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="WorkFlowReport.aspx.cs" Inherits="Employee_WorkFlow_WorkFlowReport"
    Title="مدیریت پیگیری گردش کار" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function ShowWFDesc(s, e) {
            GridViewWFReport.GetRowValues(GridViewWFReport.GetFocusedRowIndex(), 'Description', OnGetSelectedFieldValues);

        }
        function OnGetSelectedFieldValues(selectedValues) {

            txtWFDesc.SetText(selectedValues);
            PopUpWFDesc.Show();
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]<br />
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton  runat="server" ID="btnShowDetail" ButtonType="ShowDetail"
                                            ToolTip="پانوشت مرحله" IsMenuButton="true" AutoPostBack="false">
                                            <ClientSideEvents Click="ShowWFDesc" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            
                                            <image url="~/Images/icons/Back.png">
                                                </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>


            <div align="right">
                <ul class="HelpUL">
                    <li>جهت مشاهده <b>پانوشت هر مرحله</b> پس از انتخاب آن در جدول زیر  برروی ''دکمه مشاهده پانوشت مرحله'' (
                            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/icons/ShowDetail.png" />
                        ) واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>

                </ul>
            </div>

            <TSPControls:CustomAspxDevGridView ID="GridViewWFReport" ClientInstanceName="GridViewWFReport" runat="server" 
                Width="100%" OnAutoFilterCellEditorInitialize="GridViewWFReport_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewWFReport_HtmlDataCellPrepared" OnHtmlRowPrepared="GridViewWFReport_HtmlRowPrepared"
                AutoGenerateColumns="False" KeyFieldName="StateId" RightToLeft="True">
                <Settings ShowHorizontalScrollBar="true" />
                <Columns>


                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DocName" Caption="پرونده مربوطه">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataComboBoxColumn FieldName="WorkFlowId" Caption="نام فرایند" VisibleIndex="0" Width="200px">
                        <PropertiesComboBox TextField="WorkFlowName" DataSourceID="ObjdsWorkFlow" ValueType="System.String"
                            ValueField="WorkFlowId">
                        </PropertiesComboBox>
                        <CellStyle Wrap="false" HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="WFTaskName" Caption="مرحله" Width="200px">
                        <CellStyle Wrap="false" HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DoerName" Caption="انجام دهنده">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DoerNcName" Caption="سمت انجام دهنده">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Date" Caption="تاریخ">
                        <CellStyle Wrap="False" HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StateTime" Caption="ساعت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StateTypeName" Caption="نوع عملیات">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="wfDescriptionSummary" Caption="پانوشت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataImageColumn VisibleIndex="0" FieldName="SignUrl" Caption="تصویر امضاء">
                        <PropertiesImage ImageHeight="50px" ImageWidth="50px"></PropertiesImage>
                    </dxwgv:GridViewDataImageColumn>
                    
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ExpireDate" Caption="مهلت">
                    </dxwgv:GridViewDataTextColumn>


                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <br />
            <div align="right" dir="rtl">
                <fieldset style="width: 98%">
                    <legend>راهنما</legend>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td style="width: 38px" valign="middle" align="left">
                                    <asp:Label ID="Label1" runat="server" Width="16px" BackColor="White" BorderWidth="1px"
                                        BorderStyle="Solid" BorderColor="Black" Height="16px"></asp:Label>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel33" runat="server" Width="31px" Text="عادی">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 38px" valign="middle" align="left">
                                    <asp:Label ID="Label4" runat="server" Width="16px" BackColor="LemonChiffon" BorderWidth="1px"
                                        BorderStyle="Solid" Height="16px"></asp:Label>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel34" runat="server" Width="54px" Text="فوری">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="left" style="width: 38px">
                                    <asp:Label ID="Label2" runat="server" Width="16px" BackColor="Salmon" BorderWidth="1px"
                                        BorderStyle="Solid" Height="16px"></asp:Label>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel35" runat="server" Width="54px" Text="آنی">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </fieldset>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" ID="btnShowDetail2" ButtonType="ShowDetail"
                                            ToolTip="پانوشت مرحله" IsMenuButton="true" AutoPostBack="false">
                                            <ClientSideEvents Click="ShowWFDesc" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <image url="~/Images/icons/Back.png">
                                                    </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
     
            <asp:ObjectDataSource ID="ObjdsWorkFlow" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.WorkFlowManager"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HiddenFieldWFState" runat="server">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxPopupControl ID="PopUpWFDesc" runat="server" Width="387px" Height="500px"
                ClientInstanceName="PopUpWFDesc"
                AllowDragging="True" CloseAction="CloseButton"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                HeaderText="پانوشت مرحله انتخاب شده">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <div width="100%" align="center">
                            <TSPControls:CustomASPXMemo runat="server" Height="500px" ID="txtWFDesc" Width="387px"
                                ClientInstanceName="txtWFDesc" ReadOnly="true">
                            </TSPControls:CustomASPXMemo>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </TSPControls:CustomASPxPopupControl>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
