<%@ Page Title="نتایج نظرسنجی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ResualtTotalGrid.aspx.cs" Inherits="Employee_Poll_ResualtTotalGrid" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table>
                            <tr>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                   
                                    <Image   Url="~/Images/icons/printers.png"   />
                                </TSPControls:CustomAspxButton>
                            </td>

                                 <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "   
                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                   
                                                    <Image   Url="~/Images/icons/ExportExcel.png"   />
                                                </TSPControls:CustomAspxButton>
                                            </td>

                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                    EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                    CausesValidation="false">
                                   
                                    <Image   Url="~/Images/icons/back.png"   />
                                </TSPControls:CustomAspxButton>
                            </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPoll" HeaderText="نتایج نظرسنجی"
                runat="server" >
               
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td align="center" valign="top" width="50%">
                                    <dxe:ASPxLabel runat="server" ID="lblTotalAnswer" Text="تعداد کل پاسخ ها:---" Font-Bold="true"
                                        ForeColor="DarkViolet">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="50%">
                                    <dxe:ASPxLabel runat="server" ID="lblMaxAnswerDate" Text="تاریخ آخرین پاسخ به نظرسنجی:---" Font-Bold="true"
                                        ForeColor="DarkViolet">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                    <TSPControls:CustomAspxDevDataView ID="DataViewQuestion" ClientInstanceName="DataViewPoll"
                                        runat="server" ColumnCount="1" RowPerPage="25" Width="100%" 
                                        RightToLeft="True" ItemSpacing="0px" PagerStyle-ItemSpacing="0px" Border-BorderStyle="None">
                                        <ItemStyle Height="1px" Width="1px" Paddings-Padding="0px" />
                                        <ItemTemplate>
                                            <table class="TableBorder" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="TableTitle" align="right" colspan="2">
                                                            <dx:ASPxLabel ID="lblQuestion" runat="server" Text='<%# Bind("Question") %>' Font-Bold="True"
                                                                Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dx:ASPxLabel ID="lblQustion" runat="server" Width="100%" Text='<%# Bind("QuestionId") %>'
                                                                Wrap="True" Visible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <TSPControls:CustomAspxDevDataView ID="DataViewChoise" ClientInstanceName="DataViewChoise"
                                                                DataSourceID="objdsReport" runat="server" ColumnCount="1" RowPerPage="25" Width="100%"
                                                                 RightToLeft="True" ItemSpacing="0px" PagerStyle-ItemSpacing="0px"
                                                                Border-BorderStyle="None">
                                                                <ItemStyle Height="1px" Width="1px" Paddings-Padding="0px" />
                                                                <ItemTemplate>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td colspan="2" align="right">
                                                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Width="100%" Text='<%# Eval("ChoiseName")+" - تعدادپاسخ:"+Eval("CountAnswer") %>'
                                                                                    Wrap="false" Font-Bold="true" ForeColor="DarkViolet">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="50%" align="right">
                                                                                <dxe:ASPxProgressBar  
                                                                                    RightToLeft="True" ID="ASPxProgressBar1" runat="server" Height="11px" Position="50"
                                                                                    Value='<%# Bind("ChoicePercent") %>' Width="50%">
                                                                                </dxe:ASPxProgressBar>
                                                                            </td>
                                                                            <td align="right" width="50%">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <PagerSettings Visible="false">
                                                                </PagerSettings>
                                                            </TSPControls:CustomAspxDevDataView>
                                                            <asp:ObjectDataSource ID="objdsReport" runat="server" SelectMethod="SelectAnswerReport"
                                                                TypeName="TSP.DataManager.PollAnswerManager" OldValuesParameterFormatString="original_{0}">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="lblQustion" DefaultValue="-1" Name="QuestionId"
                                                                        PropertyName="Text" Type="Int32" />
                                                                    <asp:Parameter Name="PollId" Type="Int32" DefaultValue="-1" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    
                                    </TSPControls:CustomAspxDevDataView>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
               <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="GridViewPollAnswer">
                    </dxwgv:ASPxGridViewExporter>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewPollAnswer" KeyFieldName="AnswerId"
                runat="server" DataSourceID="ObjdsbserverPollQuestion" Width="100%" ClientInstanceName="GridViewPollAnswer">
                <Settings ShowHorizontalScrollBar="true" />
                <SettingsCookies Enabled="false" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="عنوان سوال" FieldName="Question" VisibleIndex="1"
                        Width="600px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="پاسخ انتخابی" FieldName="ChoiseName" VisibleIndex="1">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع کاربر" FieldName="UserType" VisibleIndex="1">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره تماس" FieldName="MobileNo" VisibleIndex="1">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="FullName" VisibleIndex="1">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewDataTextColumn Caption="رشته تحصیلی" FieldName="MeMajor" VisibleIndex="1">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات پاسخ" FieldName="Description" VisibleIndex="1"
                        Width="400px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdsbserverPollQuestion" runat="server" SelectMethod="SearchPollAnswer"
                TypeName="TSP.DataManager.PollAnswerManager">
                <SelectParameters>
                    <asp:Parameter Name="PollId" DefaultValue="-1" Type="Int32" />
                    <asp:Parameter Name="MeId" DefaultValue="-1" Type="Int32" />
                    <asp:Parameter Name="UltId" DefaultValue="-1" Type="Int32" />
                    <asp:Parameter Name="UserId" DefaultValue="-1" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                        
                                        <Image   Url="~/Images/icons/printers.png"   />
                                    </TSPControls:CustomAspxButton>
                                </td>     <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "   
                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel2" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                   
                                                    <Image   Url="~/Images/icons/ExportExcel.png"   />
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                       
                                        <Image   Url="~/Images/icons/back.png"   />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dx:ASPxHiddenField ID="HiddenFieldPage" runat="server">
            </dx:ASPxHiddenField>
         
</asp:Content>
