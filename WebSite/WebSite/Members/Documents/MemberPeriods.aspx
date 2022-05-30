<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberPeriods.aspx.cs" Inherits="Members_Documents_MemberPeriods"
    Title="دوره ها و سمینار ها" %>
    
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                                
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروانه اشتغال به کار"  ToolTip="مدیریت پروانه اشتغال به کار"
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
                        <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server" OnItemClick="MenuMemberFile_ItemClick">
                            <Items>
                                <dxm:MenuItem Name="Major" Text="مشخصات پروانه" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem> 
                                <dxm:MenuItem Name="JobHistory" Text="سابقه کار"  Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                  <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                                <dxm:MenuItem Name="Exam" Text="آزمون ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="دوره آموزشی" Name="Periods" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="MeDetail" Text="پایه - صلاحیت" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Attachment" Text="مدارک پیوست" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Capacity" Text="ظرفیت اشتغال"  Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>
                           
                        </TSPControls:CustomAspxMenuHorizontal>
                    
                <br />
                <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                <br />
                <TSPControls:CustomAspxDevGridView2 ID="CustomAspxDevGridView1" runat="server" DataSourceID="OdbMadrak"
                      KeyFieldName="ID;Type"
                    AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%">
                    <Settings ShowHorizontalScrollBar="true" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ID" Caption="ID"
                            Name="ID">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="CrsTitle"
                            Caption="عنوان مدرک" Name="CrsTitle">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="MeId" Caption="کد عضویت"
                            Name="MeId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="FirstName"
                            Caption="نام" Name="FirstName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="LastName"
                            Caption="نام خانوادگی" Name="LastName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ایجاد"
                            Name="CreateDate">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="نوع مدرک">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Type">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="PPId" Name="PPId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CrsTitle" Width="250px"
                            Caption="عنوان">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StartDate" Caption="تاریخ شروع">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndDate" Caption="تاریخ پایان">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Mark" Caption="نمره نهایی">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView2>
                <br />    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldMePeriods">
                                                </dxhf:ASPxHiddenField>
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                             
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td  >
                                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                               
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروانه اشتغال به کار"  ToolTip="بازگشت به مدیریت پروانه"
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
                <asp:ObjectDataSource ID="OdbMadrak" runat="server" TypeName="TSP.DataManager.MadrakManager"
                    SelectMethod="SearchMadrakByMeId" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="" Name="MeId" Type="Int32" />
                        <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                        <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
                        <asp:Parameter DefaultValue="-1" Name="CrsId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground">
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