<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="SeminarView.aspx.cs" Inherits="Members_Amoozesh_SeminarView"
    Title="مشخصات سمینار/دوره های غیر مصوب" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
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
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="سمینار/دوره های غیر مصوب آموزشی" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">اطلاعات پایه</legend>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="موضوع سمینار" ID="ASPxLabel10"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" ID="txtSubject" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField ErrorText="موضوع را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع برگزاری" ID="ASPxLabel18"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtDate" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField ErrorText="زمان را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان برگزاری" Width="100px" ID="lblEndDate"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtEndDate" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField IsRequired="True" ErrorText="هزینه را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="زمان برگزاری(ساعت)" Width="64px" ID="ASPxLabel2"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtTime" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField ErrorText="زمان را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان برگزاری(ساعت)" ID="ASPxLabel29"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtDuration" MaxLength="5" ReadOnly="True">
                                                <MaskSettings Mask="HH:mm"></MaskSettings>

                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField IsRequired="True" ErrorText="زمان را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ظرفیت" ID="ASPxLabel8"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtCapacity" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField IsRequired="True" ErrorText="ظرفیت را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="هزینه" ID="ASPxLabel1"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtSeminarCost" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField ErrorText="هزینه را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="امتیاز" ID="Label1"></asp:Label>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPoint" ReadOnly="True">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td valign="top" align="right">&nbsp;</td>
                                        <td valign="top" align="right">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مطالب و سرفصل ها" Width="106px" ID="ASPxLabel20"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="68px" ID="txtTopic">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل برگزاری" ID="ASPxLabel7"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtPlace" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel15"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="63px" ID="txtDesc" ReadOnly="True"></TSPControls:CustomASPXMemo>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </fieldset>
                        <br />
                        <fieldset>
                            <legend class="HelpUL">فایل های پیوست</legend>

                            <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" ID="AspxGridFlp" KeyFieldName="AttachId" AutoGenerateColumns="False">

                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل" Name="FilePath">
                                        <DataItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Text="آدرس فایل" NavigateUrl='<%# Bind("FilePath") %>' Target="_blank"></asp:HyperLink>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server">LinkButton</asp:LinkButton>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات" Name="Description"></dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                        <br />

                        <fieldset>
                            <legend class="HelpUL">زمان بندی برنامه ها </legend>

                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="AspxGridSchedule" DataSourceID="OdbSchedule" KeyFieldName="SchId" AutoGenerateColumns="False">

                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="SchId"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StartTime" Caption="از ساعت"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="EndTime" Caption="تا ساعت"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Subject" Caption="موضوع فعالیت"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" "></dxwgv:GridViewCommandColumn>
                                </Columns>


                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">مشخصات سخنران</legend>

                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="Grdv_Teacher" KeyFieldName="TrTeId" AutoGenerateColumns="False" ClientInstanceName="gridTe">
                                <ClientSideEvents RowDblClick="function(s, e) {
	//SetControlValuesTe();
	//btnTe.SetEnabled(false);
	//if(combo.GetValue()==2)
	//{
		//gridfile.SetVisible(false);
		//hp.SetVisible(false);
	//}
	//else
	//{
	//	gridfile.SetVisible(true);
		//hp.SetVisible(true);
	//}
}"></ClientSideEvents>

                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Id"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Family" Caption="نام خانوادگی"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Father" Caption="نام پدر"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="PracticalSalary" Caption="دستمزد"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="Type"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="TeId"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="LiName" Caption="مدرک تحصیلی"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="MjName" Caption="رشته"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="ارائه دهنده"></dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>

                        </fieldset>
                        <br />


                        <fieldset>
                            <legend class="HelpUL">امتیاز بندی برای رشته های مختلف</legend>
                            <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" ID="CustomAspxDevGridViewGrade" DataSourceID="OdbGrades" KeyFieldName="TrGrId" AutoGenerateColumns="False">
                                <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm"></SettingsEditing>

                                <ClientSideEvents RowDblClick="function(s, e) {
	Gradepop.Hide();

	SetControlValuesGrade();
}"></ClientSideEvents>


                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>

                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <table>
                <tbody>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnRegister1" CssClass="ButtonMenue" OnClick="btnRegister_Click" runat="server">ثبت نام</asp:LinkButton>
                        </td>
                        <td>

                            <asp:LinkButton ID="btnBack1" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">بازگشت</asp:LinkButton>
                        </td>

                    </tr>
                </tbody>
            </table>
            <asp:HiddenField ID="SeminarId" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="OdbGrades" runat="server" TypeName="TSP.DataManager.TrainingAcceptedGradeManager" SelectMethod="FindByPKCode" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PkId"></asp:Parameter>
                    <asp:Parameter Type="Byte" DefaultValue="1" Name="Type"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbSchedule" runat="server" TypeName="TSP.DataManager.ScheduleManager" SelectMethod="GetData" FilterExpression="TtId={0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
