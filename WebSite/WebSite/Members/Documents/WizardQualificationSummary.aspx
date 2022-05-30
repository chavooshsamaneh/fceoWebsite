<%@ Page Title="درخواست درج صلاحیت جید پروانه اشتغال-خلاصه اطلاعات" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="WizardQualificationSummary.aspx.cs" Inherits="Members_Documents_WizardQualificationSummary" %>

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
                    <dxm:MenuItem Text="سوگند نامه" Name="Oath">
                        <Image Width="15px" Height="15px" />
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Kardan" Text="استعلام ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Exams" Text="آزمون ها">
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
                                            <td align="right" valign="top" colspan="2">
                                                <dxe:ASPxLabel ID="lblTaxOfficeLetterOath" ClientVisible="false" runat="server" Text="توجه نمایید شما بجای بارگذاری تصویر استعلام اداره امور مالیاتی تعهد داده اید آن را حداکثر تا 20 روز از زمان صدور پروانه بارگذاری کنید در غیر اینصورت پروانه به شما تحویل داده نخواهد شد "
                                                    ClientInstanceName="lblTaxOfficeLetterOath">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <br />
                                    <TSPControls:CustomAspxDevGridView2 runat="server" ID="GridViewExam"
                                        KeyFieldName="Id" AutoGenerateColumns="false" ClientInstanceName="GridViewExam"
                                        Width="100%" Caption="لیست آزمون ها">
                                        <Settings ShowHorizontalScrollBar="true"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ExamTitle" Caption="آزمون"
                                                Name="Point">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MajorName" Caption="رشته"
                                                Name="MajorName">
                                            </dxwgv:GridViewDataTextColumn>
                                           
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Point" Caption="نمره آزمون"
                                                Name="Point">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="GrdName" Caption="پایه"
                                                Name="GrdName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TTypeName" Width="150px"
                                                Caption="زمینه آزمون" Name="TTypeName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر کارنامه"
                                                FieldName="FileURL" Caption="تصویر کارنامه" Name="FileURL">
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر گواهینامه دوره آموزشی"
                                                FieldName="PeriodImgURL" Caption="تصویر گواهینامه دوره آموزشی" Name="PeriodImgURL" Width="250px">
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>
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
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPre" OnClick="btnPre_Click" runat="server" Text="بازگشت"
                    ToolTip="بازگشت" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                    CausesValidation="False">
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه"
                    ToolTip="تایید و ادامه" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                    CausesValidation="true">
                </TSPControls:CustomAspxButton>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

