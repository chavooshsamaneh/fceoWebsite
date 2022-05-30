<%@ Page Title="گزارش تغییرات تنظیمات کاربری" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ReportMemberPollAnswers.aspx.cs" Inherits="Employee_MembersRegister_ReportMemberPollAnswers" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
                <table >
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grid.PerformCallback('Print'); }"></ClientSideEvents>
                               
                                <Image  Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                               
                                <Image  Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="btnBack_Click">
                              
                                <Image  Url="~/Images/icons/Back.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                               
                                <Image  Url="../../Images/icons/BakToManagment.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
          </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" OnItemClick="ASPxMenu1_ItemClick" >
        <Items>
            <dxm:MenuItem Name="Request" Text="مشخصات درخواست">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Job" Text="سوابق کاری">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Language" Text="زبان ها">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Activity" Text="فعالیت ها">
            </dxm:MenuItem>
            <dxm:MenuItem Text="مستندات" Name="Attach">
            </dxm:MenuItem>
            <dxm:MenuItem Text="گروه ها" Name="Group">
            </dxm:MenuItem>
            <dxm:MenuItem Text="مالی" Name="AccFish">
            </dxm:MenuItem>
            <dxm:MenuItem Text="پیام ها" Name="Message" Visible="false">
            </dxm:MenuItem>
            <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer" Selected="true">
            </dxm:MenuItem>
        </Items>
      
    </TSPControls:CustomAspxMenuHorizontal>
    <br />
    <div style="width: 100%; text-align: right; display: none">
        <dx:ASPxLabel ID="lblSex" runat="server">
        </dx:ASPxLabel>
        <dx:ASPxLabel ID="lblT" runat="server">
        </dx:ASPxLabel>
        <dx:ASPxLabel ID="lblOfName" runat="server">
        </dx:ASPxLabel>
    </div>
    <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewAnswer" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grid" DataSourceID="objAnswer" EnableViewState="False" Width="100%"
        RightToLeft="True" KeyFieldName="AnswerId" OnCustomCallback="GridViewAnswer_CustomCallback"
        OnAutoFilterCellEditorInitialize="GridViewAnswer_AutoFilterCellEditorInitialize"
        OnHtmlDataCellPrepared="GridViewAnswer_HtmlDataCellPrepared">
        <ClientSideEvents EndCallback="function(s, e) {
       if(s.cpDoPrint==1)
            {
               s.cpDoPrint = 0;
	           window.open('../../Print.aspx');
            }
}" />
        
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="عنوان سوال" Width="150px" FieldName="Title"
                VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پاسخ انتخاب شده" Width="350px" FieldName="ChoiseName"
                VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" Width="80px" FieldName="AnswerDate"
                Name="Date" VisibleIndex="2">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ساعت پاسخ" Width="80px" FieldName="AnswerTime"
                VisibleIndex="3">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" ShowClearFilterButton="true">
              
            </dxwgv:GridViewCommandColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


                <table >
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grid.PerformCallback('Print'); }"></ClientSideEvents>
                               
                                <Image  Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                               
                                <Image  Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="btnBack_Click">
                               
                                <Image  Url="~/Images/icons/Back.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                               
                                <Image  Url="../../Images/icons/BakToManagment.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
         
  </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="objAnswer" runat="server" TypeName="TSP.DataManager.PollAnswerManager"
        SelectMethod="FindByMeId">
        <SelectParameters>
            <asp:Parameter Name="MeId" DbType="Int32" DefaultValue="-1" />
            <asp:Parameter Name="UltId" DbType="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenFieldAnswer" ClientInstanceName="HiddenFieldAnswer"
        runat="server">
    </dx:ASPxHiddenField>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewAnswer">
    </dx:ASPxGridViewExporter>
</asp:Content>
