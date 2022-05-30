<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="JobHistory.aspx.cs" Inherits="Employee_TechnicalServices_Project_JobHistory"
    Title="سوابق کاری" %>

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
    <div id="divcontent" style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuImp" runat="server"  
                        OnItemClick="MenuImp_ItemClick"  AutoSeparators="RootOnly"
                        ItemSpacing="0px" SeparatorColor="#A5A6A8" SeparatorHeight="100%" SeparatorWidth="1px">
                        <Items>
                            <dxm:MenuItem Text="مشخصات مجری" Name="Imp">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="سوابق کاری" Name="Job" Selected="True">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="کیفیت اجرای پروژه ها" Name="Control">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="احکام شورای انتظامی" Name="Entezami">
                            </dxm:MenuItem>
                        </Items>
                        <RootItemSubMenuOffset X="-1" LastItemY="-2" LastItemX="-1" FirstItemY="-2" FirstItemX="-1"
                            Y="-2"></RootItemSubMenuOffset>
                        <Border BorderWidth="1px" BorderStyle="Solid" BorderColor="#A5A6A8"></Border>
                        <VerticalPopOutImage Height="8px" Width="4px">
                        </VerticalPopOutImage>
                        <ItemStyle VerticalAlign="Middle" ImageSpacing="5px" PopOutImageSpacing="7px"></ItemStyle>
                        <SubMenuItemStyle ImageSpacing="7px">
                        </SubMenuItemStyle>
                        <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1"></SubMenuStyle>
                        <HorizontalPopOutImage Height="7px" Width="7px">
                        </HorizontalPopOutImage>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSource1"
                    Width="100%"  
                    ClientInstanceName="jgrid" AutoGenerateColumns="False" KeyFieldName="JhId">
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                    <Styles  >
                        <GroupPanel ForeColor="Black">
                        </GroupPanel>
                        <Header HorizontalAlign="Center">
                        </Header>
                    </Styles>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JhId" Name="JhId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Caption="نام پروژه"
                            Name="ProjectName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Employer" Caption="کارفرما"
                            Name="Employer">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع پروژه"
                            Name="TypeName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StartCorporateDate" Caption="تاریخ شروع همکاری"
                            Name="StartCorporateDate">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="EndCorporateDate"
                            Caption="تاریخ پایان همکاری" Name="EndCorporateDate">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Area" Caption="زیربنا"
                            Name="Area">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CorName" Caption="نحوه مشارکت"
                            Name="CorName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="CreateDate" Caption="تاریخ ایجاد"
                            Name="CreateDate">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveName" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" ShowClearFilterButton="true">                      
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="TtName"
                            Caption="نوع درخواست">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CitName" Caption="شهر">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="TableId">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByMeRequest"
            TypeName="TSP.DataManager.ProjectJobHistoryManager" OldValuesParameterFormatString="original_{0}">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MReId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
                <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
                <asp:Parameter DefaultValue="-1" Name="TableType" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="PgMode" runat="server" />
        <asp:HiddenField ID="HDProjectId" runat="server" Visible="False" />
        <asp:HiddenField ID="HDImpId" runat="server" Visible="False" />
        <asp:HiddenField ID="RequestId" runat="server" Visible="False" />
    </div>
</asp:Content>
