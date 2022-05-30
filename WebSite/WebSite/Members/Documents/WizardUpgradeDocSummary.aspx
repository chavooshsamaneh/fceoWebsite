<%@ Page Title="درخواست ارتقاء پایه پروانه اشتغال-خلاصه اطلاعات" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="WizardUpgradeDocSummary.aspx.cs" Inherits="Members_Documents_WizardUpgradeDocSummary" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuSteps" runat="server">
                <Items>
                    <dxm:MenuItem Text="مدارک لازم" Name="Oath">
                        <Image Width="15px" Height="15px" />
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Kardan" Text="استعلام ها">
                    </dxm:MenuItem>

                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <p class=" HelpUL">
                لطفا اطلاعات تکمیل شده خود را با دقت بررسی نمایید و در صورت مشاهده هرگونه اشتباه اقدام
                                به بازگشت در مراحل طی شده و تصحیح آن ها نمایید.در صورت اشتباه در تکمیل اطلاعات سازمان
                                هیچگونه مسئولیتی در قبال آن نخواهد داشت.پس از اطمینان از صحت اطلاعات گزینه ''صحت اطلاعات
                                فوق را تأئید می نمایم'' واقع در پایین صفحه را انتخاب نموده و سپس بر روی دکمه تایید و ادامه
                                کلیک نمایید. 
            </p>
            <br />

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSummary" HeaderText="خلاصه اطلاعات"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td valign="middle" align="center" width="30%" colspan="3">
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="meImg"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/Person.png">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                                <td align="center" valign="top" colspan="3" width="70%">
                                    <table width="100%">
                                        <tr>
                                            <td id="Td48" runat="server" align="right" valign="top" width="50%">تصویر شناسنامه:
                                            </td>
                                            <td align="right" valign="top" width="50%">
                                                <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                                    Text="تصویر شناسنامه">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right" valign="top">تصویر کارت ملی:
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxHyperLink ID="HpSSN" runat="server" ClientInstanceName="hssn" Target="_blank"
                                                    Text="تصویر کارت ملی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblSoldire" ClientVisible="false" runat="server" Text="تصویر کارت پایان خدمت:"
                                                    ClientInstanceName="lblSoldire">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpSoldier" runat="server" ClientInstanceName="hsol" ClientVisible="False"
                                                    Target="_blank" Text="تصویر کارت پایان خدمت">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblTaxOfficeLetter" ClientVisible="false" runat="server" Text="تصویر استعلام اداره امور مالیاتی:"
                                                    ClientInstanceName="lblTaxOfficeLetter">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpTaxOfficeLetter" runat="server" ClientInstanceName="HpTaxOfficeLetter" ClientVisible="false"
                                                    Target="_blank" Text="تصویر استعلام اداره امور مالیاتی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblImgFrontDoc" ClientVisible="false" runat="server" Text="تصویر روی پروانه قبلی:"
                                                    ClientInstanceName="lblImgFrontDoc">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="linkImgFrontDoc" runat="server" ClientInstanceName="linkImgFrontDoc" ClientVisible="false"
                                                    Target="_blank" Text="تصویر روی پروانه قبلی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblImgBackDoc" ClientVisible="false" runat="server" Text="تصویر پشت پروانه قبلی:"
                                                    ClientInstanceName="lblImgBackDoc">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="linkImBackDoc" runat="server" ClientInstanceName="linkImBackDoc" ClientVisible="false"
                                                    Target="_blank" Text="تصویر پشت پروانه قبلی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblPeriodImage" ClientVisible="false" runat="server" Text="تصویر گواهینامه دوره جوش:"
                                                    ClientInstanceName="lblPeriodImage">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpPeriodImage" runat="server" ClientInstanceName="HpPeriodImage" ClientVisible="false"
                                                    Target="_blank" Text="تصویر گواهینامه دوره جوش">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblHseImage" ClientVisible="false" runat="server" Text="تصویر HSE:"
                                                    ClientInstanceName="lblHseImg">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpHseImage" runat="server" ClientInstanceName="HpHseImage" ClientVisible="false"
                                                    Target="_blank" Text="تصویر HSE">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>


                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <br />
                                    <TSPControls:CustomAspxDevGridView2 ID="GrdvJobCon" runat="server" Width="100%" KeyFieldName="JobConfId"
                                        ClientInstanceName="jgrid" Caption="لیست تاییدیه های سوابق کاری" AutoGenerateColumns="False">
                                        <Settings ShowHorizontalScrollBar="true"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JobConfId"
                                                Name="JobConfId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DateFrom" Caption="تاریخ همکاری از"
                                                Name="DateFrom">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DateTo" Caption="تاریخ همکاری تا"
                                                Name="DateTo">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Position" Caption="سمت"
                                                Name="Position">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ConfirmTypeName" Caption="نوع تایید کننده"
                                                Name="ProjectName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت شخص حقیقی"
                                                Name="Employer">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Name" Caption="نام شرکت/نهاد"
                                                Name="PrTypeName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MFNo" Caption="شماره پروانه"
                                                Name="StartCorporateDate">
                                                <HeaderStyle Wrap="False" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="200" FieldName="Description"
                                                Caption="توضیحات" Name="EndCorporateDate">
                                                <HeaderStyle Wrap="True" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر "
                                                FieldName="FileURL" Caption="تصویر" Name="FileURL">
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="5" Width="130" PropertiesHyperLinkEdit-Text="تصویر پروانه/رتبه بندی "
                                                FieldName="GradeURL" Caption="تصویر پروانه/رتبه بندی" Name="GradeURL">
                                                <HeaderStyle Wrap="False" />
                                                <CellStyle Wrap="False" />
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right" valign="top">
                                    <br />
                                    <TSPControls:CustomASPxCheckBox runat="server" Text="صحت اطلاعات فوق را تأئید می نمایم." EnableClientSideAPI="True"
                                        ID="chbConfimInfo">
                                        <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorTextPosition="Bottom"
                                            ErrorDisplayMode="ImageWithText" ErrorText="صحت اطلاعات فوق مورد تأئید قرار نگرفته است.">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است" />
                                        </ValidationSettings>
                                    </TSPControls:CustomASPxCheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top"></td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <div class="Item-center">
                <asp:LinkButton ID="btnPre" CssClass="ButtonMenue" OnClick="btnPre_Click" runat="server">بازگشت</asp:LinkButton>
                <asp:LinkButton ID="btnNext" CssClass="ButtonMenue" OnClick="btnNext_Click" runat="server">تایید و ادامه</asp:LinkButton>
                <%--  <TSPControls:CustomAspxButton ID="btnPre" OnClick="btnPre_Click" runat="server" Text="بازگشت" 
                                       ToolTip="بازگشت" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                                            CausesValidation="False">
                                           
                                         
                                        </TSPControls:CustomAspxButton>
                                        <TSPControls:CustomAspxButton ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" 
                                            ToolTip="تایید و ادامه" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                                            CausesValidation="true">
                                            
                                        </TSPControls:CustomAspxButton>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


