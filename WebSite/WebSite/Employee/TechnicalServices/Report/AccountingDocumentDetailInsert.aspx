<%@ Page Title="مشخصات ناظر لیست حق الزحمه ناظرین" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AccountingDocumentDetailInsert.aspx.cs" Inherits="Employee_TechnicalServices_Report_AccountingDocumentDetailInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent2" runat="server">
                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="BtnNew_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/new.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" Enabled="false" runat="server" Text=" " ToolTip="ویرایش"
                                CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/edit.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                ID="btnSave" UseSubmitBehavior="False" OnClick="btnSave_Click" EnableTheming="False">

                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/save.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                EnableTheming="False" OnClick="btnBack_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/Back.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />

    <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <fieldset>
                    <legend class="HelpUL">مشخصات لیست</legend>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td valign="top" align="right" width="15%">شماره لیست                 
                                </td>
                                <td valign="top" align="right" width="35%">
                                    <TSPControls:CustomTextBox runat="server" ID="txtListNo" Enabled="false" Width="100%"
                                        ClientInstanceName="txtListNo">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td width="15%">تاریخ لیست
                                </td>
                                <td width="35%">
                                    <TSPControls:CustomTextBox runat="server" ID="txtListDate" Enabled="false" Width="100%"
                                        ClientInstanceName="txtListDate">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>وضعیت ارسال
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtStatusName" Enabled="false" Width="100%"
                                        ClientInstanceName="txtStatusName">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">نوع لیست                              
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox Enabled="false" runat="server" Width="100%"
                                        ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="CmbType"
                                        RightToLeft="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {

}"></ClientSideEvents>
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                            
                                            <RequiredField IsRequired="True" ErrorText="نوع لیست را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <Items>
                                            <dxe:ListEditItem Value="0" Text="پرداخت حق الزحمه ناظر"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="1" Text="آزاد سازی بر اساس پرداخت اقساط"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="2" Text="اصلاحیه" />
                                        </Items>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>

                            </tr>
                            <tr>
                                <td valign="top" align="right">نام لیست *
                                </td>
                                <td colspan="3" valign="top" align="right">
                                    <TSPControls:CustomTextBox Enabled="false" runat="server" ID="txtListName" Width="100%"
                                        ClientInstanceName="txtListName">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                         
                                            <RequiredField IsRequired="true" ErrorText="نام لیست را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>

                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset>
                    <legend class="HelpUL">مشخصات عضو</legend>
                    <ul class="HelpUL">
                        <li>مبالغ در زمان انتخاب یک ناظر از لیست کشویی بر اساس متراژ کارکرد وی محاسبه و نمایش داده می شود</li>
                        <li>در صورتی که نوع لیست "آزاد سازی بر اساس اقساط" ویا "اصلاحیه" باشد قادر به تغییر مبالغ می باشد</li>
                    </ul>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td valign="top" align="right" width="15%">پلاک ثبتی اصلی پروژه                 
                                </td>
                                <td valign="top" align="right" width="35%">
                                    <TSPControls:CustomTextBox runat="server" ID="txtRegNo" OnTextChanged="txtRegNo_TextChanged" AutoPostBack="true" Width="100%"
                                        ClientInstanceName="txtRegNo">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td  width="15%"></td>
                                <td  width="35%"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="right" width="15%">کد پروژه                 
                                </td>
                                <td valign="top" align="right" width="35%">
                                    <TSPControls:CustomTextBox runat="server" ID="txtProjectId" OnTextChanged="txtProjectId_TextChanged" AutoPostBack="true" Width="100%"
                                        ClientInstanceName="txtProjectId">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td>مالک پروژه
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtProjectOwner" Enabled="false" Width="100%" ReadOnly="true"
                                        ClientInstanceName="txtProjectOwner">
                                    </TSPControls:CustomTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td width="15%">ناظر پروژه
                                </td>
                                <td width="35%">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                        ID="comboProjectObserver" ValueType="System.String" SelectedIndex="0" DataSourceID="ObjectDataSourceObserver" ClientInstanceName="comboProjectObserver"
                                        RightToLeft="True" OnSelectedIndexChanged="comboProjectObserver_SelectedIndexChanged" AutoPostBack="true" TextField="NameAndWage" ValueField="ProjectObserversId">
                                        <ItemStyle HorizontalAlign="Right" />
        
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                    
                                            <RequiredField IsRequired="True" ErrorText="ناظر را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ObjectDataSourceObserver" runat="server" SelectMethod="FindByProjectIdAndRequestId"
                                        TypeName="TSP.DataManager.TechnicalServices.Project_ObserversManager" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:Parameter Type="Int32" DefaultValue="-2" Name="ProjectId"></asp:Parameter>
                                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:Parameter>
                                            <asp:Parameter Type="Int32" DefaultValue="0" Name="InActive"></asp:Parameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                                <td>ماهیت ناظر
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtObsType" Enabled="false" Width="100%"
                                        ClientInstanceName="txtObsType">
                                    </TSPControls:CustomTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>کد عضویت
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtMeId" Enabled="false" Width="100%"
                                        ClientInstanceName="txtMeId">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td>شماره حساب ناظر
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtObserverAccNo" Enabled="false" Width="100%"
                                        ClientInstanceName="txtObserverAccNo">
                                    </TSPControls:CustomTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td valign="top" align="right">شماره پروانه ناظر                              
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtMeFileNo" Enabled="false" Width="100%"
                                        ClientInstanceName="txtMeFileNo">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">زمینه نظارت                              
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtObserversTypeTitle" Enabled="false" Width="100%"
                                        ClientInstanceName="txtObserversTypeTitle">
                                    </TSPControls:CustomTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>متراژ دستمزد
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtWage" Enabled="false" Width="100%"
                                        ClientInstanceName="txtWage">
                                    </TSPControls:CustomTextBox>
                                </td>

                                <td>متراژ کسر ظرفیت
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtCapacityDecrement" Enabled="false" Width="100%"
                                        ClientInstanceName="txtCapacityDecrement">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>تعرفه
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtYearName" Enabled="false" Width="100%"
                                        ClientInstanceName="txtYearName">
                                    </TSPControls:CustomTextBox>

                                </td>

                                <td>هزینه تعرفه
                                </td>
                                <td>
                                    <TSPControls:CustomTextBox runat="server" ID="txtPrice" Enabled="false" Width="100%"
                                        ClientInstanceName="txtPrice">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">درصد پرداخت سهم                              
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtIsPayFivePercent" Width="100%"
                                        ClientInstanceName="txtIsPayFivePercent">
                                        <ClientSideEvents TextChanged="function(s,e){
                                            if(txtIsPayFivePercent.GetText()!='')
                                            { 
                                            var InsuranceShare;
                                           InsuranceShare=( parseInt(HiddenFieldPage.Get('InsuranceShare'))*parseInt(txtIsPayFivePercent.GetText())/100);
                                            txtInsuranceShare.SetText(InsuranceShare);
                                            
                                            var NezamShare;
                                           NezamShare=( parseInt(HiddenFieldPage.Get('NezamShare'))*parseInt(txtIsPayFivePercent.GetText())/100);
                                            txtNezamShare.SetText(NezamShare);
                                            
                                            var Observershare;
                                           Observershare=( parseInt(HiddenFieldPage.Get('ObserverShare'))*parseInt(txtIsPayFivePercent.GetText())/100);                                         
                                            txtObservershare.SetText(Observershare);
                                            
                                                
                                            }
                                            }" />
                                    </TSPControls:CustomTextBox>
                                </td>

                                <td valign="top" align="right">سهم بیمه *
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtInsuranceShare" Width="100%"
                                        ClientInstanceName="txtInsuranceShare">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                                         
                                            <RequiredField IsRequired="true" ErrorText="سهم بیمه را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ClientSideEvents TextChanged="function(s,e){
                                            if(txtIsPayFivePercent.GetText()!='' && txtInsuranceShare.GetText()!='' && parseInt(txtInsuranceShare.GetText())==0)
                                            { 

                                            var InsuranceShare;
                                           InsuranceShare=( parseInt(HiddenFieldPage.Get('InsuranceShare'))*parseInt(txtIsPayFivePercent.GetText())/100);
                                           // txtInsuranceShare.SetText(InsuranceShare);

                                            var Observershare;
                                           Observershare=( parseInt(HiddenFieldPage.Get('ObserverShare'))*parseInt(txtIsPayFivePercent.GetText())/100)+InsuranceShare;                                         
                                            txtObservershare.SetText(Observershare);                                                                                            
                                            }
                                            else
                                            {
                                            
                                            var Observershare;
                                           Observershare=( parseInt(HiddenFieldPage.Get('ObserverShare'))*parseInt(txtIsPayFivePercent.GetText())/100);                                         
                                            txtObservershare.SetText(Observershare);

                                            }
                                            }" />
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">سهم سازمان *
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtNezamShare" Width="100%"
                                        ClientInstanceName="txtNezamShare">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                                 
                                            <RequiredField IsRequired="true" ErrorText="سهم سازمان را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">سهم ناظر *
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtObservershare" Width="100%"
                                        ClientInstanceName="txtObservershare">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                                    
                                            <RequiredField IsRequired="true" ErrorText="سهم ناظر را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">سهم نظام کاردان ها *
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" ID="txtNezamKardanShare" Width="100%"
                                        ClientInstanceName="txtNezamKardanShare">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="true" ErrorText="سهم نظام کاردان ها را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>

                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>توضیحات</td>
                                <td colspan="3">
                                    <TSPControls:CustomASPXMemo runat="server" ID="txtDescription" Width="100%"
                                        ClientInstanceName="txtDescription">
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">

                                    <TSPControls:CustomAspxDevGridView2 ID="GridViewContract" Caption="اطلاعات قرارداد نظارت" Width="100%" runat="server"
                                        DataSourceID="ObjdContract"
                                      KeyFieldName="ContractId" ClientInstanceName="GridViewContract">
                                         <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
                                     
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Title" Caption="نوع قرارداد">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Duration" Caption="مدت زمان(ماه)">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Amount" Caption="مبلغ قرارداد">
                                                <PropertiesTextEdit DisplayFormatString="#,#">
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ContractDate" Caption="تاریخ انعقاد">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ایجاد">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                FieldName="FileUrl" Caption="فایل قرارداد" Name="PlanFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InActiveName" Caption="وضعیت">
                                            </dxwgv:GridViewDataTextColumn>

                                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" ShowClearFilterButton="true">
                                            </dxwgv:GridViewCommandColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>


                                    <asp:ObjectDataSource ID="ObjdContract" runat="server" SelectMethod="SelectTSContractForMembers"
                                        TypeName="TSP.DataManager.TechnicalServices.ContractManager">
                                        <SelectParameters>
                                            <asp:Parameter Type="Int32" DefaultValue="-2" Name="PrjImpObsDsgnId"></asp:Parameter>
                                            <asp:Parameter Type="Int32" DefaultValue="-2" Name="ProjectIngridientTypeId"></asp:Parameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <TSPControls:CustomAspxDevGridView2 ID="GridViewAccountingDocumentDetail" Caption="اطلاعات ناظر در سایر لیست ها" KeyFieldName="AccDocDetailId"
                                        runat="server" Width="100%" ClientInstanceName="GridViewAccountingDocumentDetail">
                                        <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
                                        <SettingsCustomizationWindow Enabled="True" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem FieldName="CapacityDecrement" SummaryType="Sum" />
                                            <dxwgv:ASPxSummaryItem FieldName="Wage" SummaryType="Sum" />
                                            <dxwgv:ASPxSummaryItem FieldName="ObserverInsurancePrice" SummaryType="Sum" />
                                            <dxwgv:ASPxSummaryItem FieldName="InsuranceShare" SummaryType="Sum" />
                                            <dxwgv:ASPxSummaryItem FieldName="NezamShare" SummaryType="Sum" />
                                            <dxwgv:ASPxSummaryItem FieldName="ObserverShare" SummaryType="Sum" />
                                        </TotalSummary>
                                        <Columns>

                                            <dxwgv:GridViewDataTextColumn Caption="شماره لیست" FieldName="ListNo" VisibleIndex="1"
                                                Width="70px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نوع لیست" FieldName="DocumentTypeName" VisibleIndex="1"
                                                Width="70px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت" FieldName="CreateDate" VisibleIndex="1"
                                                Width="70px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="متراژ کسر ظرفیت" FieldName="CapacityDecrement"
                                                VisibleIndex="5" Width="100px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="متراژ دستمزد" FieldName="Wage" VisibleIndex="6"
                                                Width="100px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="سهم بیمه(ریال)" FieldName="InsuranceShare" VisibleIndex="6"
                                                Width="200px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <PropertiesTextEdit DisplayFormatString="#,#">
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="سهم سازمان(ریال)" FieldName="NezamShare" VisibleIndex="6"
                                                Width="200px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <PropertiesTextEdit DisplayFormatString="#,#">
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="سهم ناظر(ریال)" FieldName="ObserverShare"
                                                VisibleIndex="6" Width="200px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <PropertiesTextEdit DisplayFormatString="#,#">
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                                            </dxwgv:GridViewCommandColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>
                                </td>
                            </tr>
                                  <tr>
                                <td colspan="4">

                                    <TSPControls:CustomAspxDevGridView2 ID="GridViewProjFish" Caption="اطلاعات فیش های مربوط به نظارت پروژه" Width="100%" runat="server"
                                    
                                      KeyFieldName="AccountingId" ClientInstanceName="GridViewProjFish">
                                         <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
                                     
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Number" Caption="شماره ">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Amount" Caption="مبلغ">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FishDate" Caption="تاریخ فیش">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="PaymentDate" Caption="تاریخ پرداخت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="StatusName" Caption="وضعیت">
                                            </dxwgv:GridViewDataTextColumn>
                               
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="BtnNew_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/new.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" runat="server" Text=" " ToolTip="ویرایش"
                                CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/edit.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                ID="btnSave2" UseSubmitBehavior="False" OnClick="btnSave_Click" EnableTheming="False">

                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/save.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                EnableTheming="False" OnClick="btnBack_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/Back.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dx:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage" runat="server"></dx:ASPxHiddenField>
</asp:Content>

