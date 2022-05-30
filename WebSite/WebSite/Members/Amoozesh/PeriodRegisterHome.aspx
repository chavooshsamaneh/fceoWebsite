<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodRegisterHome.aspx.cs" Inherits="Members_Amoozesh_PeriodRegisterHome"
    Title="مدیریت دوره های آموزشی/سمینارهای سپری شده" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script lang="javascript">
        function HasError() {
            if (txtObjText.GetIsValid())
                return false;
            return true;
        }
    </script>
    <div align="center" style="width: 100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%" dir="rtl" align="center">
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]
                    </div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                    Text=" " EnableTheming="False" ToolTip="حذف"
                                                    ID="btnDelete" EnableViewState="False" Visible="False" OnClick="btnDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/delete.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                    CausesValidation="False" Text=" " EnableTheming="False"
                                                    ToolTip="اعتراض" ID="btnObjection" EnableViewState="False" OnClick="btnObjection_Click">
                                                    <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/objection.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                    CausesValidation="False" Text=" " EnableTheming="False"
                                                    ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                </div>
             <ul class="HelpUL">
                        <li>
                        جهت مشاهده ی نتایج آزمون دوره پس از انتخاب دوره مورد نظر در لیست
                        برروی دکمه 'مشاهده' 
                        <img src="../../Images/icons/view.png" style="display:inline" />
                        واقع در منوی بالا/پایین کلیک نمایید
                 </li>
                   <li>
                        جهت ثبت اعتراض خود نسبت به نمره کسب کرده در دوره پس از انتخاب دوره مورد نظر در لیست
                        برروی دکمه 'اعتراض' 
                        <img src="../../Images/icons/objection.png" style="display:inline" />
                        واقع در منوی بالا/پایین کلیک نمایید
                    </li>
                 </ul>
                
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewCourseRegister" runat="server" DataSourceID="ObjdsPeriodRegister"
                    Width="100%"
                    OnHtmlDataCellPrepared="GridViewCourseRegister_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewCourseRegister_AutoFilterCellEditorInitialize"
                    ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="PRId" RightToLeft="True">
                    <Columns>

                        <dxwgv:GridViewDataTextColumn FieldName="PeriodTitle" Caption="عنوان دوره" VisibleIndex="0" Width="200px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PPCode" Caption="کد دوره" VisibleIndex="0" Width="200px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PPType" Caption="نحوه برگزاری" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="InsName" Caption="موسسه" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RgstType" Caption="نوع ثبت نام" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RegisterDate" Width="100px" Caption="تاریخ ثبت نام"
                            VisibleIndex="4">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="StartDate" Width="100px" Caption="تاریخ شروع دوره"
                            VisibleIndex="5">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="EndDate" Width="100px" Caption="تاریخ پایان دوره" VisibleIndex="6">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PPId" Visible="False" VisibleIndex="11">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت ثبت نام" FieldName="RegInActiveName" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn  Caption="وضعیت دوره" FieldName="PeriodStatus" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت پرداخت" FieldName="StatusAcount" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره فیش" FieldName="FishNumber" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پرداخت" FieldName="PaymentDate" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="مبلغ فیش (ريال)" FieldName="FishAmount" VisibleIndex="12">
                            <PropertiesTextEdit DisplayFormatString="#,#">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تعداد ساعات غیبت" FieldName="TotalTimePresent" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                         <%-- <dxwgv:GridViewDataTextColumn Caption="حضور" FieldName="RegIsPresent" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="statusName" Caption="وضعیت قبولی" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="FirstMark" Caption="نمره آزمون" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="LastMark" Caption="نمره نهایی" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TestTotalMark" Caption="نمره کل آزمون" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" Width="100px" Caption="توضیحات نمره" VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="statusName" Caption="نتیجه نهایی" VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>--%>

                    </Columns>
                    <Settings ShowHorizontalScrollBar="True"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxPopupControl ID="PopupPoll" runat="server"
                    ClientInstanceName="PopupPoll" Modal="True" PopupVerticalAlign="WindowCenter"
                    PopupHorizontalAlign="WindowCenter" PopupElementID="btnView1,btnView"
                    HeaderText="ثبت نظر سنجی آموزشی" CloseAction="CloseButton">
                    <SizeGripImage Height="12px" Width="12px">
                    </SizeGripImage>
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <p class="HelpUL">
                                                <b>جهت مشاهده نتیجه آزمون نخست باید فرم نظرسنجی را تکمیل فرمایید </b>
                                            </p>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" ClientInstanceName="btnPollAnswer"
                                                Text="در نظر سنجی شرکت می کنم"
                                                ID="btnPollAnswer" OnClick="btnPollAnswer_Click">
                                                <ClientSideEvents Click="function(s, e) {

PopupPoll.Hide();

}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <CloseButtonImage Height="17px" Width="17px">
                    </CloseButtonImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingTop="1px" PaddingRight="6px"></Paddings>
                    </HeaderStyle>
                </TSPControls:CustomASPxPopupControl>


                <TSPControls:CustomASPxPopupControl ID="PopupObjection" runat="server"
                    ClientInstanceName="pop" Modal="True" PopupVerticalAlign="WindowCenter"
                    PopupHorizontalAlign="WindowCenter" PopupElementID="btnObjection1,btnObjection"
                    HeaderText="اعتراض" CloseAction="CloseButton">
                    <SizeGripImage Height="12px" Width="12px">
                    </SizeGripImage>
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">


                            <table>
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ اعتراض" Width="78px" ID="lblMeDate" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right">
                                            <TSPControls:CustomTextBox runat="server" Width="100px" ReadOnly="True" Visible="False"
                                                ClientInstanceName="MeDate" ID="txtMeObjDate">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="متن اعتراض" Width="64px" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right">
                                            <TSPControls:CustomASPXMemo runat="server" Height="69px" Width="328px"
                                                ClientInstanceName="txtObjText" ID="txtObjText">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="متن اعتراض را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="نمره اولیه" Width="78px" ID="lblMark" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right">
                                            <TSPControls:CustomTextBox runat="server" Width="100px" ReadOnly="True" Visible="False"
                                                ID="txtFirstMark">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText=""></RegularExpression>

                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="نمره نهایی" Width="78px" ID="lblLastMark" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right">
                                            <TSPControls:CustomTextBox runat="server" Width="100px" ReadOnly="True" Visible="False"
                                                ClientInstanceName="TotalMark"
                                                ID="txtLastMark">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText=""></RegularExpression>

                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ جواب اعتراض" Width="94px" ID="lblAnsDate"
                                                Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right">
                                            <TSPControls:CustomTextBox runat="server" Width="100px" ReadOnly="True" Visible="False"
                                                ID="txtAnsDate">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText=""></RegularExpression>

                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="جواب اعتراض" ID="lblAns" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right">
                                            <TSPControls:CustomASPXMemo runat="server" Height="69px" Width="328px" ReadOnly="True" Visible="False"
                                                ClientInstanceName="TeAnswer"
                                                ID="txtTeObjText">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="2">
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" ClientInstanceName="btn"
                                                Text="ذخیره"
                                                ID="btnSave" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if(!HasError())
{
pop.Hide();

}
else
  e.processOnServer=false;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>


                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <CloseButtonImage Height="17px" Width="17px">
                    </CloseButtonImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingTop="1px" PaddingRight="6px"></Paddings>
                    </HeaderStyle>
                </TSPControls:CustomASPxPopupControl>


                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                      
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                Text=" " EnableTheming="False" ToolTip="حذف"
                                                ID="btnDelete2" EnableViewState="False" Visible="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                <Image Url="~/Images/icons/delete.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                CausesValidation="False" Text=" " EnableTheming="False"
                                                ToolTip="اعتراض" ID="btnObjection1" EnableViewState="False" OnClick="btnObjection_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                <Image Url="~/Images/icons/objection.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                CausesValidation="False" Text=" " EnableTheming="False"
                                                ToolTip="مشاهده" ID="btnView1" EnableViewState="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsPeriodRegister" runat="server" TypeName="TSP.DataManager.PeriodRegisterManager"
                    SelectMethod="SelectPeriodRegister" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="PRId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="PPId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="InsId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="IsSeminar"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="HDPRId" runat="server" Visible="False"></asp:HiddenField>
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldCourseRegister">
                </dxhf:ASPxHiddenField>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
