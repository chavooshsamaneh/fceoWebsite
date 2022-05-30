<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="DocResponsibility.aspx.cs" Inherits="Members_Documents_DocResponsibility"
    Title="مدیریت صلاحیت ها" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
                document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }
    </script>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table  >
                                            <tbody>
                                                <tr>
                                                  
                                                    <td>
                                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                            ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnBack_Click">
                                                    
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="مدیریت پروانه اشتغال به کار"  ToolTip="مدیریت پروانه اشتغال به کار"
                                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                     
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu> 
                        <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server" 
                           
                            OnItemClick="MenuMemberFile_ItemClick">
                            <Items>
                                <dxm:MenuItem Text="مشخصات پروانه" Name="Major" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="سابقه کار" Name="JobHistory"  Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="آزمون ها" Name="Exam" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Periods" Text="دوره آموزشی" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="MeDetail" Text="پایه-صلاحیت" Selected="True" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مدارک پیوست" Name="Attachment" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="ظرفیت اشتغال" Name="Capacity" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>
                           
                        </TSPControls:CustomAspxMenuHorizontal>
                  
                    <table>
                        <tr>
                            <td style="width: 100%" align="center">
                                <br />
                                <dxe:ASPxLabel runat="server" Font-Bold="true" Text="دستورالعمل درخواست" ID="txtRequestComment"
                                    ForeColor="DarkRed" Visible="false">
                                </dxe:ASPxLabel>
                                <br />
                            </td>
                            </tr>
                    </table>
                    <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                    <br />
                    <TSPControls:CustomAspxDevGridView2 runat="server"  EnableViewState="False"
                        ID="GridViewMeFiledetail" DataSourceID="ObjdsMemberFileDetail" KeyFieldName="MfdId"
                        AutoGenerateColumns="False" ClientInstanceName="GridViewMeFiledetail" 
                        OnHtmlRowPrepared="GridViewMeFiledetail_HtmlRowPrepared" Width="100%" OnAutoFilterCellEditorInitialize="GridViewMeFiledetail_AutoFilterCellEditorInitialize"
                        OnHtmlDataCellPrepared="GridViewMeFiledetail_HtmlDataCellPrepared">
                   
                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjName" Width="250px" Caption="رشته">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GMRName" Width="100px"
                                Caption="پایه - صلاحیت">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ActTypeName" Width="200px"
                                Caption="شیوه اخذ">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Date" Name="Date" Width="120px"
                                Caption="تاریخ اخذ">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ResRangeName" Name="ResRangeName"
                                Width="150px" Caption="محدوده صلاحیت">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="InActives" Width="80px"
                                Caption="وضعیت">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption=" " Width="30px" ShowClearFilterButton="true">
                             
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents EndCallback="function(s, e) {
	//if(GridViewMeFiledetail.cpError==1)
	//{
	//	SetError(GridViewMeFiledetail.cpErrorMsg);
	//}

}" />
                    </TSPControls:CustomAspxDevGridView2>
                    <br />
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table  >
                                            <tbody>
                                                <tr>
                                                 
                                                    <td>
                                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                            ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnBack_Click">
                                                        
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="مدیریت پروانه اشتغال به کار" ToolTip="مدیریت پروانه اشتغال به کار"
                                                            CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                     </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
       
                     <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldMeFileDetail">
                                        </dxhf:ASPxHiddenField>

                    <asp:ObjectDataSource ID="ObjdsMemberFileDetail" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="SelectById" TypeName="TSP.DataManager.DocMemberFileDetailManager">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MFId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="InActive"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
             

</asp:Content>
