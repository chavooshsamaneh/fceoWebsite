<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowReport.aspx.cs" Inherits="Settlement_MemberDocument_WorkFlowReport"
    Title="مدیریت پیگیری جریان کار" %>

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

            <tspcontrols:customaspxroundpanelmenu id="CustomASPxRoundPanelMenu2" runat="server"
                width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>  <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ID="btnShowDetail" ButtonType="ShowDetail"
                                        ToolTip="پانوشت مرحله"  AutoPostBack="false">
                                        <ClientSideEvents Click="ShowWFDesc" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                        <td>
                                            <TSPControls:CustomAspxButton  IsMenuButton="true"  runat="server" Text=" "  ToolTip="بازگشت"
                                                ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </tspcontrols:customaspxroundpanelmenu>


            <div align="right">
                <ul class="HelpUL">
                    <li>جهت مشاهده <b>پانوشت هر مرحله</b> پس از انتخاب آن در جدول زیر  برروی ''دکمه مشاهده پانوشت مرحله'' (
                            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/icons/ShowDetail.png" />
                        ) واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>

                </ul>
            </div>

            <tspcontrols:customaspxdevgridview id="GridViewWFReport" clientinstancename="GridViewWFReport" runat="server" datasourceid="ObjdsWfReport"
                width="100%"
                autogeneratecolumns="False" keyfieldname="StateId" righttoleft="True">
                    <Settings ShowHorizontalScrollBar="true" />
                    <Columns>
                       
                        
                        
                             <dxwgv:GridViewDataImageColumn VisibleIndex="0" FieldName="SignUrl" Caption="تصویر امضاء">
                             <PropertiesImage ImageHeight="50px"  ImageWidth="50px"></PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                          <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="wfDescriptionSummary" Caption="پانوشت">
                        </dxwgv:GridViewDataTextColumn>
                       <dxwgv:GridViewDataComboBoxColumn FieldName="WorkFlowId" Caption="نام فرایند" VisibleIndex="0" width="200px">
                            <propertiescombobox textfield="WorkFlowName" datasourceid="ObjdsWorkFlow" valuetype="System.String"
                                valuefield="WorkFlowId"></propertiescombobox>
                            <cellstyle wrap="false" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataComboBoxColumn> 
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="WFTaskName" Caption="مرحله" width="200px">
                            <cellstyle wrap="false" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="DocName" Caption="پرونده مربوطه">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="DoerName" Caption="انجام دهنده">
                        </dxwgv:GridViewDataTextColumn>                       
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="ExpireDate" Caption="مهلت">
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Date" Caption="تاریخ">
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StateTime" Caption="ساعت">
                        </dxwgv:GridViewDataTextColumn>
                           <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StateTypeName" Caption="نوع عملیات">
                        </dxwgv:GridViewDataTextColumn>
                    
                     
                        <dxwgv:GridViewCommandColumn VisibleIndex="8" Caption=" " width="50px"  ShowClearFilterButton="true" >
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                 
                </tspcontrols:customaspxdevgridview>
            <br />
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
                                <dxe:aspxlabel id="ASPxLabel33" runat="server" width="31px" text="عادی">
                                        </dxe:aspxlabel>
                            </td>
                            <td style="width: 38px" valign="middle" align="left">
                                <asp:Label ID="Label4" runat="server" Width="16px" BackColor="LemonChiffon" BorderWidth="1px"
                                    BorderStyle="Solid" Height="16px"></asp:Label>
                            </td>
                            <td valign="middle" align="right">
                                <dxe:aspxlabel id="ASPxLabel34" runat="server" width="54px" text="فوری">
                                        </dxe:aspxlabel>
                            </td>
                            <td valign="middle" align="left" style="width: 38px">
                                <asp:Label ID="Label2" runat="server" Width="16px" BackColor="Salmon" BorderWidth="1px"
                                    BorderStyle="Solid" Height="16px"></asp:Label>
                            </td>
                            <td valign="middle" align="right">
                                <dxe:aspxlabel id="ASPxLabel35" runat="server" width="54px" text="آنی">
                                        </dxe:aspxlabel>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </fieldset>

            <br />
            <tspcontrols:customaspxroundpanelmenu id="CustomASPxRoundPanelMenu1" runat="server"
                width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>  <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ID="btnShowDetail2" ButtonType="ShowDetail"
                                        ToolTip="پانوشت مرحله"  AutoPostBack="false">
                                        <ClientSideEvents Click="ShowWFDesc" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                            <td>
                                                <TSPControls:CustomAspxButton  IsMenuButton="true"  runat="server" Text=" "  ToolTip="بازگشت"
                                                    ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Back.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                          </dxp:PanelContent>
                    </PanelCollection>
                </tspcontrols:customaspxroundpanelmenu>

            <asp:ObjectDataSource ID="ObjdsWfReport" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectStateReportsById" TypeName="TSP.DataManager.WorkFlowStateManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableType"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="WfCode" Type="Int32" />
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="NmcId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsWorkFlow" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.WorkFlowManager"></asp:ObjectDataSource>
            <dxhf:aspxhiddenfield id="HiddenFieldWFState" runat="server">
            </dxhf:aspxhiddenfield>
            <tspcontrols:customaspxpopupcontrol id="PopUpWFDesc" runat="server" width="387px" height="500px"
                clientinstancename="PopUpWFDesc"
                allowdragging="True" closeaction="CloseButton"
                modal="True" popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
                headertext="پانوشت مرحله انتخاب شده">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <div width="100%" align="center">
                        <TSPControls:CustomASPXMemo runat="server" Height="500px"   ID="txtWFDesc"  Width="387px"
                            ClientInstanceName="txtWFDesc" ReadOnly="true" >
                            
                        </TSPControls:CustomASPXMemo>
                    </div>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </tspcontrols:customaspxpopupcontrol>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
