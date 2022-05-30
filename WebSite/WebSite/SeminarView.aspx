<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="SeminarView.aspx.cs" Inherits="SeminarView" Title="مشخصات" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                 <table>
                <tbody>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnRegister" CssClass="ButtonMenue" OnClick="btnRegister_Click" runat="server">ثبت نام</asp:LinkButton>
                        </td>
                        <td>

                            <asp:LinkButton ID="btnBack" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">بازگشت</asp:LinkButton>
                        </td>

                    </tr>
                </tbody>
            </table>
                <ul class="HelpUL">
                    <li><b>توجه!</b>جهت ثبت نام بخش توضیحات به طور دقیق مطالعه شود</li>
                </ul>
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="سمینار/دوره غیر مصوب"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <fieldset>
                                <legend class="HelpUL">اطلاعات پایه</legend>

                                <table style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td width="15%" style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="موضوع" ID="ASPxLabel10">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td width="85%" style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox runat="server" ID="txtSubject" Width="100%" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="موضوع را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%" style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel18">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td width="35%" style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtDate"
                                                    Width="100%" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="زمان را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td width="15%" style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پایان" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td width="35%" style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtEndDate"
                                                    Width="100%" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="زمان را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="هزینه ریال" Width="127px" ID="ASPxLabel19">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtSeminarCost" Width="100%"
                                                    ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="هزینه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right"></td>
                                            <td style="vertical-align: top" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="زمان برگزاری(ساعت)" Width="64px" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtTime" Width="100%" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="زمان را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="مدت زمان برگزاری(ساعت)" ID="ASPxLabel29">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtDuration" Width="100%"
                                                    MaxLength="5" RightToLeft="True">
                                                    <%--     <MaskSettings Mask="HH:mm"></MaskSettings>--%>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="زمان را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="مطالب و سرفصل ها" Width="106px" ID="ASPxLabel20">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="68px" ID="txtTopic" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="محل برگزاری" ID="ASPxLabel7">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtPlace" Width="100%"
                                                    ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100px" ID="ASPxLabel15">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="63px" ID="txtDesc" Width="100%"
                                                    ReadOnly="True">
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                                    <fieldset><legend class="HelpUL">فایل های پیوست</legend>
                          
                                        <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                            Width="100%" ID="AspxGridFlp" KeyFieldName="AttachId" AutoGenerateColumns="False">
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Width="300px" VisibleIndex="0" FieldName="FilePath" Caption="فایل"
                                                    Name="FilePath">
                                                    <DataItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" NavigateUrl='<%# Bind("FilePath") %>'
                                                            Target="_blank"></asp:HyperLink>
                                                    </DataItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                                    </EditItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Width="250px" VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                                                    Name="Description">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView2>
                                    </fieldset>
                          <fieldset><legend class="HelpUL">زمان بندی برنامه</legend>
                            
                                        <TSPControls:CustomAspxDevGridView2 runat="server" ID="AspxGridSchedule"
                                             KeyFieldName="SchId" AutoGenerateColumns="False"
                                            OnHtmlDataCellPrepared="AspxGridSchedule_HtmlDataCellPrepared"
                                            Width="100%">
                                            <SettingsCookies Enabled="false" />
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                            <Columns>

                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Date" Caption="تاریخ" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="StartTime" Caption="از ساعت"
                                                    Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="EndTime" Caption="تا ساعت"
                                                    Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Subject" Caption="موضوع فعالیت">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="5" Caption=" ">
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView2>
                                    </fieldset>
                            <fieldset><legend class="HelpUL">مشخصات سخنران</legend>
                           
                                        <TSPControls:CustomAspxDevGridView2 Width="100%" runat="server"
                                            ID="Grdv_Teacher" KeyFieldName="TrTeId" AutoGenerateColumns="False" ClientInstanceName="gridTe">
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Id" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Family" Caption="نام خانوادگی" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="Type" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="Description" Caption="توضیحات" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="TeId" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="LiName" Caption="مدرک تحصیلی" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="MjName" Caption="رشته" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="ارائه دهنده" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView2>
                                 </fieldset>
                    
                            <fieldset><legend class="HelpUL">امتیاز بندی برای رشته های مختلف</legend>
                          
                                        <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                            Width="100%" ID="CustomAspxDevGridViewGrade" DataSourceID="OdbGrades" KeyFieldName="TrGrId"
                                            AutoGenerateColumns="False">
                                            <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Description" Caption="توضیحات" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView2>
                                   </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
               <table>
                <tbody>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1" CssClass="ButtonMenue" OnClick="btnRegister_Click" runat="server">ثبت نام</asp:LinkButton>
                        </td>
                        <td>

                            <asp:LinkButton ID="LinkButton3" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">بازگشت</asp:LinkButton>
                        </td>

                    </tr>
                </tbody>
            </table>
                <asp:HiddenField ID="SeminarId" runat="server" Visible="False"></asp:HiddenField>
               
                <asp:ObjectDataSource ID="OdbGrades" runat="server" SelectMethod="FindByPKCode" TypeName="TSP.DataManager.TrainingAcceptedGradeManager">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="PkId"></asp:Parameter>
                        <asp:Parameter Type="Byte" DefaultValue="10" Name="Type"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
