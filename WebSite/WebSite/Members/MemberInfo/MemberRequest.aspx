<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberRequest.aspx.cs" Inherits="Members_MemberInfo_MemberRequest"
    Title="مدیریت درخواست ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="BtnNew" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">درخواست تغییرات جدید</asp:LinkButton>


                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnEdit" CssClass="ButtonMenue" OnClick="btnEdit_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">ویرایش درخواست</asp:LinkButton>

                                    </td>
                                    <td>

                                        <asp:LinkButton ID="btnView" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده درخواست</asp:LinkButton>
                                    </td>

                                    <td>

                                        <asp:LinkButton ID="btnDelete" CssClass="ButtonMenue" OnClick="btnDelete_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}
                                            else
	return e.processOnServer= confirm('آیا مطمئن به لغو کردن این درخواست هستید؟');
                                            ">لغو درخواست</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnChangeBaseInfo" CssClass="ButtonMenue" OnClick="btnChangeBaseInfo_Click" runat="server">درخواست تغییرات اطلاعات پایه</asp:LinkButton>

                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnSendNextStep" CssClass="ButtonMenue" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}
                                            else{
                                              TextDesc.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
   return false;   
                                              }
                                            ">گردش کار</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="CustomAspxButton" CssClass="ButtonMenue" OnClick="btnTracing_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">پیگیری گردش کار</asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div align="right">
                <ul class="HelpUL">
                    <li>جهت ثبت درخواست جدید <u>تغییرات اطلاعات پایه</u> برروی ''دکمه درخواست اطلاعات پایه''  واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>
                    <li>جهت <u>ویرایش</u> اطلاعات عضویت خود درصورتی که پیش از این درخواست خود را ثبت نموده
                            اید ، بر روی ''دکمه ویرایش'' 
                     واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>
                    <li>تنها در صورتی قادر به ویرایش اطلاعات یک درخواست می باشید که مرحله نمایش داده شده
                            آن در خواست در لیست زیر''ثبت اطلاعات عضو حقیقی'' (
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/WFStart.png" Height="16px"
                                Width="16px" />
                        ) باشد. </li>
                    <li>پس از تکمیل کلیه اطلاعات عضویت خود جهت بررسی اطلاعات وارد شده توسط مسئول مربوطه
                            و تایید درخواست برروی دکمه ''گردش کار'' '  کلیک نمایید و سپس در پنجره باز شده برروی دکمه ارسال کلیک نمایید.</li>

                </ul>
                <p style="text-align: justify">
                    جهت مشاهده تمامی پانوشت های مربوط به پرونده بعد از انتخاب پرونده از درون جدول، دکمه
                        پیگیری گردش کار را انتخاب کنید. در این صورت می توانید تمامی سابقه گردش کار پرونده خود
                        و تمامی پانوشت های مربوط به پرونده خود را یکجا مشاهده کنید
                </p>
            </div>
            <TSPControls:CustomAspxDevGridView2 Width="100%" ID="GridViewMemberRequest" runat="server"
                DataSourceID="ObjectDataSource1"
                ClientInstanceName="grid" EnableViewState="False" KeyFieldName="MReId" AutoGenerateColumns="False"
                OnAutoFilterCellEditorInitialize="GridViewMemberRequest_AutoFilterCellEditorInitialize"
             >
                <SettingsText Title="لیست درخواست های عضویت" />
                <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="MReId" Name="MReId" Visible="False" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                        VisibleIndex="0">
                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                        <DataItemTemplate>
                            <div align="center">
                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WfTaskFullName") %>'>
                                </dxe:ASPxImage>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="پانوشت" FieldName="WFDes" VisibleIndex="0"
                        Width="300px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="1"
                        Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="2"
                        Visible="False">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="3"
                        Visible="False">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="4"
                        Visible="False">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="2">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="TypeName" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="4">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="5">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="Created" VisibleIndex="1">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع تایید" FieldName="Confirm" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" VisibleIndex="6">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView2>
            <br />

            <fieldset width="98%">
                <legend>راهنما</legend>
                <table width="100%">
                    <tbody>

                        <tr>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image5" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_WFStart.png" />
                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="درخواست ثبت اطلاعات عضو حقیقی"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image6" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/Member-MembershipUnitConfirmingMember.png" />
                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="تایید کارمند واحد عضویت"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">

                                <asp:Image ID="Image11" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFConfirmAndEnd.png" />
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تایید و پایان بررسی درخواست عضو حقیقی"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="Image10" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFREjectAndEnd.png" />
                                <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Text="عدم تایید و پایان بررسی درخواست عضو حقیقی"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image7" Height="16px" Width="16px" runat="server" Visible="false" ImageUrl="~/Images/WF/Member-ExecutiveManagerConfirmingMember.png" />
                                <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Text="تایید مدیر اجرایی" Visible="false" ForeColor="Black"
                                    Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image8" Height="16px" Width="16px" runat="server" Visible="false" ImageUrl="~/Images/WF/Member-AccountingManagerConfirmingMember.png" />
                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="تایید مدیر امور مالی" Visible="false"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>

                        </tr>

                    </tbody>
                </table>
            </fieldset>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnNew2" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">درخواست تغییرات جدید</asp:LinkButton>


                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnEdit2" CssClass="ButtonMenue" OnClick="btnEdit_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">ویرایش درخواست</asp:LinkButton>

                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnView2" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده درخواست</asp:LinkButton>
                                    </td>

                                    <td>
                                        <asp:LinkButton ID="btnDelete2" CssClass="ButtonMenue" OnClick="btnDelete_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}
                                            else
	return e.processOnServer= confirm('آیا مطمئن به لغو کردن این درخواست هستید؟');
                                            ">لغو درخواست</asp:LinkButton>
                                    </td>
                                    <td>

                                        <asp:LinkButton ID="btnChangeBaseInfo2" CssClass="ButtonMenue" OnClick="btnChangeBaseInfo_Click" runat="server">درخواست تغییرات اطلاعات پایه</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnSendNextStep2" CssClass="ButtonMenue" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}
                                            else{
                                              TextDesc.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
   return false;   
                                              }
                                            ">گردش کار</asp:LinkButton>
                                    </td>
                                    <td>

                                        <asp:LinkButton ID="btnTracing2" CssClass="ButtonMenue" OnClick="btnTracing_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">پیگیری گردش کار</asp:LinkButton>
                                    </td>
                                    <%--  <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" runat="server" Text=" "
                                            ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton3" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 100%" align="left" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="راهنما"
                                            CausesValidation="False" ID="ASPxButton1" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/Help.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="MemberId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="MemberRequest" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HDpage" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="TSP.DataManager.MemberRequestManager"
                SelectMethod="FindByMemberId" FilterExpression="MeId={0}" OldValuesParameterFormatString="original_{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="1" Name="IsMeTemp" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
                    <asp:Parameter DefaultValue="-1" Name="IsCreated" Type="Int16" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
                SelectMethod="SelectByWorkId">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" Width="387px"
                ClientInstanceName="PopupWorkFlow"
                AllowDragging="True" CloseAction="CloseButton" HeaderText=""
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <div dir="rtl">
                            <TSPControls:CustomAspxCallbackPanel runat="server" Width="100%"
                                ID="CallbackPanelWorkFlow" ClientInstanceName="CallbackPanelWorkFlow" OnCallback="CallbackPanelWorkFlow_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                                        ForeColor="Red" Visible="False">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 25px" valign="top" align="center" colspan="2">
                                                                    <dxe:ASPxLabel runat="server" Text="وضعیت جاری:" ID="lblWfState" ForeColor="Red"
                                                                        ClientInstanceName="lblWfState">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" colspan="2">
                                                                    <TSPControls:CustomASPxCheckBox runat="server" Text="ارسال  همزمان پیام" ID="chbIsSendMail">
                                                                    </TSPControls:CustomASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="مرحله" Font-Size="X-Small" ID="lblSenBack">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td dir="ltr" valign="top" align="right">
                                                                    <TSPControls:CustomAspxComboBox runat="server" Width="312px"
                                                                        ID="cmbSendBackTask" ValueType="System.String">
                                                                        <ValidationSettings>

                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="توضیحات" Font-Size="X-Small" ID="ASPxLabel7">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td dir="rtl" valign="top" align="right">
                                                                    <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtDescription"
                                                                        ClientInstanceName="TextDesc">
                                                                        <ValidationSettings>

                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomASPXMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 37px" dir="ltr" valign="top" align="center" colspan="2">
                                                                    <TSPControls:CustomAspxButton runat="server" Text="ارسال" Width="93px" ID="btnSendNextWorkStep"
                                                                        AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                                        <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	grid.PerformCallback('');
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel runat="server" Height="100%" Width="100%" ID="PanelSaveSuccessfully"
                                            ClientInstanceName="PanelSaveSuccessfully">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <div align="center">
                                                        <br />
                                                        <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                            ID="lblInstitueWarning" ForeColor="Red" __designer:wfdid="w20">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                        <TSPControls:CustomAspxButton runat="server" Text="خروج" Width="93px" ID="btnClose"
                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                            <ClientSideEvents Click="function(s, e) {	
	//CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Hide();
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </div>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {
	PopupWorkFlow.SetHeaderText(CallbackPanelWorkFlow.cpWfName);
	lblWfState.SetText(CallbackPanelWorkFlow.cpWfStateName);
}" />
                            </TSPControls:CustomAspxCallbackPanel>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
            <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
