<%@ Page Title="مدیریت فیش های پرداخت الکترونیکی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EpaymentFishes.aspx.cs" Inherits="EPayment_EpaymentFishes" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                       
                                    <table >
                                        <tr>                                          
                                        
                                            <td >
                                                <TSPControls:CustomAspxButton   ID="btnPayment" Width="140px" runat="server" runat="server" EnableViewState="False"
                                                    OnClick="btnPayment_Click" Text="پرداخت الکترونیکی" Wrap="False" 
                                                    >
                                                    <Image Url="~/Images/icons/Empayment.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                              
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPrj" HeaderText="مدیریت فیش ها" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div align="right" width="100%">
                            <ul class="HelpUL">
                                <li>فیش هایی که وضعیت پرداخت آنها <u>ثبت سیستم</u> می باشد پرداخت نشده محسوب می شوند
                                </li>
                                <li>جهت پرداخت فیش های پرداخت نشده ابتدا فیش را از لیست انتخاب نموده سپس برروی دکمه
                                    پرداخت الکترونیکی کلیک نمایید</li>
                                <li>مجموع فیش های پرداختی به سازمان به منزلیه مجموع بستانکاری شما نمی باشد و برای مشخص
                                    شدن ریز حساب خود بایستی به امور مالی سازمان مراجعه نمایید. </li>
                            </ul>
                        </div>
                        <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                            ID="GridViewAccounting" KeyFieldName="AccountingId" AutoGenerateColumns="False"
                             ClientInstanceName="grid" DataSourceID="ObjdsEpayment">
                            <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsCookies Enabled="false" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت پرداخت"
                                    Width="100px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Amount" Caption="مبلغ (ريال)"
                                    Width="200px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                    <PropertiesTextEdit DisplayFormatString="#,#">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت"
                                    Width="200px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ" Width="80px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Time" Caption="ساعت" Width="80px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FollowNumber" Caption="کد رهگیری در سامانه"
                                    Width="100px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ReferenceId" Caption="کدرهگیری بانکی"
                                    Width="100px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TypeName" Caption="نحوه پرداخت"
                                    Width="100px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActiveName" Caption="وضعیت ثبت"
                                    Width="100px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ProjectId" Caption="کد پروژه ساختمانی"
                                    Width="100px">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelMenu2" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                       
                                    <table>
                                        <tr>                                                                                                                                  
                                            <td >
                                                <TSPControls:CustomAspxButton   ID="btnPayment1" Width="140px" runat="server" runat="server" EnableViewState="False"
                                                    OnClick="btnPayment_Click" Text="پرداخت الکترونیکی" ToolTip="پرداخت الکترونیکی" Wrap="False" 
                                                    >
                                                    <Image  Url="~/Images/icons/Empayment.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsEpayment" runat="server" SelectMethod="SelectAccountingForEpayemntForMemberPortal"
                TypeName="TSP.DataManager.TechnicalServices.AccountingManager">
                <SelectParameters>
                    <asp:Parameter DbType="Int32" Name="TableTypeId" DefaultValue="-1" />
                    <asp:Parameter DbType="Int32" Name="AccType" DefaultValue="-1" />
                    <asp:Parameter DbType="Int32" Name="Status" DefaultValue="-1" />
                    <asp:Parameter DbType="Int32" Name="FishPayerId" DefaultValue="-1" />
                    <asp:Parameter DbType="Int32" Name="TMeId" DefaultValue="-1" />
                    <asp:Parameter DbType="Boolean" Name="IsPayerTempMe" DefaultValue="false" />
                    <asp:Parameter DbType="Int32" Name="Type" DefaultValue="4" />
                    
                </SelectParameters>
            </asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
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
