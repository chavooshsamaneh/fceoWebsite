<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SMSRecieveresReport.aspx.cs" Inherits="Employee_SMS_SMSRecieveresReport"
    Title="گیرندگان پیام کوتاه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        //function MemberSearch(s,e)
        //{
        //if(ASPxClientEdit.ValidateGroup('Member')==false)
        //   return;
        //grdSMSReciever.PerformCallback('');
        //}

        /************** Grid Selection *********************/
        var _selectNumber = 0;
        var _handle = true;

        function cbSelectAllCheckedChanged(s, e) {
            if (s.GetChecked())
                grdSMSReciever.SelectRows();
            else
                grdSMSReciever.UnselectRows();
        }

        function OnGridSelectionChanged(s, e) {
            cbSelectAll.SetChecked(s.GetSelectedRowCount() == s.cpVisibleRowCount);

            if (e.isChangedOnServer == false) {
                if (e.isAllRecordsOnPage && e.isSelected)
                    _selectNumber = s.GetVisibleRowsOnPage();
                else if (e.isAllRecordsOnPage && !e.isSelected)
                    _selectNumber = 0;
                else if (!e.isAllRecordsOnPage && e.isSelected)
                    _selectNumber++;
                else if (!e.isAllRecordsOnPage && !e.isSelected)
                    _selectNumber--;

                _handle = true;
            }
            if (chkMultiSelect.GetChecked() == true) {
                if (grdSMSReciever.GetSelectedRowCount() > 0) {
                    txtSelectedMeId.SetText('در حال بارگذاری...');
                    grdSMSReciever.GetSelectedFieldValues('MeId', OnGetSelectedFieldValues);
                }
                else
                    txtSelectedMeId.SetText('');
            }
        }
        function OnGridEndCallback(s, e) {
            _selectNumber = s.cpSelectedRowsOnPage;
        }
        function OnGetSelectedFieldValues(selectedValues) {
            if (selectedValues.length == 0) return;
            var RecieverId = '';
            for (i = 0; i < selectedValues.length; i++) {
                if (RecieverId != '') RecieverId += ';';
                RecieverId += selectedValues[i];
            }
            txtSelectedMeId.SetText(RecieverId);
        }

        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        /*******************************************************/
    </script>
    <TSPControls:CustomAspxCallbackPanel ID="CallbackSMS" HideContentOnCallback="false" ClientInstanceName="CallbackSMS"
        OnCallback="CallbackSMS_Callback" runat="server"
        Width="100%">
        <ClientSideEvents EndCallback="function(s,e){  
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
           }" />
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table>
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بارگذاری مجدد وضعیت ارسال"
                                                ID="btnRefresh" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(e,s){                                            
                                                CallbackSMS.PerformCallback('Timer');
                                            }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/SMSReportRefresh.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="false" Text=" "
                                                ToolTip="خروجی Excel" ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s,e){
                                                         popupChooseType.Show();
                                                         cmbListType.SetSelectedIndex(-1); }"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/ExportExcel.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                ID="btnBack1" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                              
                                                <Image Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <ul class="HelpUL">
                    <li>توجه: تنها تا 24 ساعت پس از ارسال پیامک، امکان دریافت وضعیت آن وجود دارد
                    </li>
                </ul>
                <br />

                <fieldset id="RoundPanelSMSInfo" runat="server">
                    <legend>مشخصات پیام کوتاه</legend>
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                    Text="عنوان">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" colspan="3" dir="rtl" valign="top">
                                <TSPControls:CustomASPXMemo ID="txtSMSSubject" runat="server" AutoResizeWithContainer="True"
                                    Height="37px" ReadOnly="True" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText="عنوان پیام کوتاه را وارد نمایید" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ClientSideEvents TextChanged="function(s, e) {

}" />
                                </TSPControls:CustomASPXMemo>
                            </td>

                        </tr>
                        <tr>

                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                    Text="نوع پیام">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top" >
                                <TSPControls:CustomTextBox ID="txtSMSType" runat="server"
                                    ReadOnly="True" Width="100%">
                                    <ValidationSettings>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                    Text="ارسال کننده">
                                </dxe:ASPxLabel>
                                <td align="right"  valign="top">
                                    <TSPControls:CustomTextBox ID="txtSenderName" runat="server"
                                        ReadOnly="True" Width="100%">
                                        <ValidationSettings>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                    Text="زمان ارسال">
                                </dxe:ASPxLabel>
                                <td align="right" dir="ltr" valign="top">
                                    <TSPControls:CustomTextBox ID="txtSMSTime" runat="server"
                                        ReadOnly="True" Width="100%">
                                        <ValidationSettings>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>

                            </td>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                    Text="تاریخ ارسال">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" dir="ltr" valign="top">
                                <TSPControls:CustomTextBox ID="txtSMSDate" runat="server"
                                    ReadOnly="True" Width="100%">
                                    <ValidationSettings>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <br />

                <div align="right">
                    <TSPControls:CustomASPxCheckBox ID="chkMultiSelect" runat="server" Text="انتخاب گروهی" ClientInstanceName="chkMultiSelect">
                        <ClientSideEvents CheckedChanged="function(s,e){
          grdSMSReciever.PerformCallback('MultiSelect;'+s.GetChecked());
          PanelSelectedMeId.SetVisible(s.GetChecked());
          //if(s.GetChecked()==false) grdSMSReciever.UnselectRows();
   }" />
                    </TSPControls:CustomASPxCheckBox>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView runat="server" Font-Size="7.5pt" Width="100%"
                    ID="grdSMSReciever" DataSourceID="objdSMSReMe" ClientInstanceName="grdSMSReciever"
                    KeyFieldName="SmsReId" AutoGenerateColumns="False" OnCustomJSProperties="grdSMSReciever_CustomJSProperties"
                    OnCustomCallback="grdSMSReciever_CustomCallback" RightToLeft="True" OnHtmlDataCellPrepared="grdSMSReciever_HtmlDataCellPrepared">
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                    <ClientSideEvents SelectionChanged="OnGridSelectionChanged" EndCallback="OnGridEndCallback"
                        RowDblClick="function(s,e){if(chkMultiSelect.GetChecked())s.SelectRowOnPage(e.visibleIndex);}" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" Visible="False" VisibleIndex="0">
                            <HeaderStyle HorizontalAlign="Center">
                                <Paddings PaddingTop="1px" PaddingBottom="1px"></Paddings>
                            </HeaderStyle>
                            <HeaderTemplate>
                                <TSPControls:CustomASPxCheckBox ID="cbSelectAll" runat="server" ClientInstanceName="cbSelectAll"
                                    OnInit="cbSelectAll_Init">
                                    <ClientSideEvents CheckedChanged="cbSelectAllCheckedChanged" />
                                </TSPControls:CustomASPxCheckBox>
                            </HeaderTemplate>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="RecieverId" VisibleIndex="0"
                            Width="20%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1"
                            Width="20%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2"
                            Width="30%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Width="20%" Caption="شماره همراه" FieldName="RecieverCellPhone"
                            VisibleIndex="3">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <%--  <dxwgv:GridViewDataTextColumn Caption="وضعیت ارسال" FieldName="DeliveryReport" VisibleIndex="4">
                                <headerstyle wrap="True" />
                            </dxwgv:GridViewDataTextColumn>--%>
                        <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت" FieldName="SMSDeliveryReId" Name="SMSDeliveryReId"
                            VisibleIndex="5" Width="10%">
                            <PropertiesComboBox>
                                <Items>
                                    <dxe:ListEditItem Text="رسیده به گوشی" Value="24" />
                                    <dxe:ListEditItem Text="نرسیده به گوشی" Value="25" />
                                    <dxe:ListEditItem Text="رسیده به مخابرات" Value="26" />
                                    <dxe:ListEditItem Text="نرسیده به مخابرات" Value="27" />
                                    <dxe:ListEditItem Text="رسیده به مخابرات-نامشخص بودن وضعیت" Value="28" />
                                    <dxe:ListEditItem Text="عدم ارسال" Value="1" />
                                    <dxe:ListEditItem Text="فاقد شماره همراه" Value="3" />
                                    <dxe:ListEditItem Text="نامشخص" Value="29" />
                                </Items>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnReport" runat="server" Width="20px" Height="20px" ImageUrl="~/Images/SMSNotSent.png">
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="50px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <dxp:ASPxPanel ID="PanelSelectedMeId" runat="server" ClientInstanceName="PanelSelectedMeId"
                    ClientVisible="False">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">کدهای عضویت انتخاب شده :
                                        </td>
                                        <td align="left">
                                            <table style="border-collapse: collapse; background-color: transparent" 
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                                EnableTheming="False" ToolTip="کپی" ID="btnCopy"
                                                                EnableViewState="False">
                                                                <ClientSideEvents Click="function(s,e){copyToClipboard(txtSelectedMeId.GetText());}"></ClientSideEvents>
                                                                <Image Url="~/Images/icons/Copy2.png">
                                                                </Image>

                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                                EnableTheming="False" ToolTip="حذف انتخاب ها"
                                                                ID="btnClear" EnableViewState="False">
                                                                <ClientSideEvents Click="function(s,e){grdSMSReciever.UnselectRows();}"></ClientSideEvents>
                                                                <Image Url="~/Images/icons/Clear-Form.png">
                                                                </Image>

                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <TSPControls:CustomASPXMemo runat="server" Height="80px" Width="100%" ReadOnly="True"
                                ClientInstanceName="txtSelectedMeId"
                                ID="txtSelectedMeId">
                                <ValidationSettings>
                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomASPXMemo>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxp:ASPxPanel>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView runat="server" Font-Size="7.5pt" Width="100%"
                    ID="GridViewSMSReEmp" DataSourceID="objdsSMSReEmp" KeyFieldName="SmsReId" AutoGenerateColumns="False"
                    OnCustomJSProperties="grdSMSReciever_CustomJSProperties" OnCustomCallback="grdSMSReciever_CustomCallback"
                    RightToLeft="True" OnHtmlDataCellPrepared="GridViewSMSReEmp_HtmlDataCellPrepared">
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                    <ClientSideEvents SelectionChanged="OnGridSelectionChanged" EndCallback="OnGridEndCallback"
                        RowDblClick="function(s,e){if(chkMultiSelect.GetChecked())s.SelectRowOnPage(e.visibleIndex);}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="شماره پرسنلی" FieldName="RecieverId" VisibleIndex="0"
                            Width="20%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1"
                            Width="20%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2"
                            Width="30%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="RecieverCellPhone"
                            VisibleIndex="3" Width="20%">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت" FieldName="SMSDeliveryReId" Name="SMSDeliveryReId"
                            VisibleIndex="5" Width="10%">
                            <PropertiesComboBox>
                                <Items>
                                    <dxe:ListEditItem Text="رسیده به گوشی" Value="24" />
                                    <dxe:ListEditItem Text="نرسیده به گوشی" Value="25" />
                                    <dxe:ListEditItem Text="رسیده به مخابرات" Value="26" />
                                    <dxe:ListEditItem Text="نرسیده به مخابرات" Value="27" />
                                    <dxe:ListEditItem Text="رسیده به مخابرات-نامشخص بودن وضعیت" Value="28" />
                                    <dxe:ListEditItem Text="عدم ارسال" Value="1" />
                                    <dxe:ListEditItem Text="فاقد شماره همراه" Value="3" />
                                    <dxe:ListEditItem Text="نامشخص" Value="29" />
                                </Items>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnReportEmp" runat="server" Width="20px" Height="20px" ImageUrl="~/Images/SMSNotSent.png">
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="50px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <asp:ObjectDataSource ID="objdsSMSReEmp" runat="server" SelectMethod="FindBySMSId"
                    TypeName="TSP.DataManager.SmsRecieverManager" CacheDuration="600" CacheExpirationPolicy="Sliding"
                    EnableCaching="True" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="SMSId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="recievertype" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <TSPControls:CustomAspxDevGridView runat="server" Font-Size="7.5pt" Width="100%"
                    ID="GridViewSMSReMaual" DataSourceID="objdSMSReManual" KeyFieldName="SmsReId"
                    AutoGenerateColumns="False" OnCustomJSProperties="grdSMSReciever_CustomJSProperties"
                    OnCustomCallback="grdSMSReciever_CustomCallback" RightToLeft="True" OnHtmlDataCellPrepared="GridViewSMSReMaual_HtmlDataCellPrepared">
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                    <ClientSideEvents SelectionChanged="OnGridSelectionChanged" EndCallback="OnGridEndCallback"
                        RowDblClick="function(s,e){if(chkMultiSelect.GetChecked())s.SelectRowOnPage(e.visibleIndex);}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="RecieverCellPhone"
                            VisibleIndex="1" Width="90%">
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت" FieldName="SMSDeliveryReId" Name="SMSDeliveryReId"
                            VisibleIndex="2" Width="10%">
                            <PropertiesComboBox>
                                <Items>
                                    <dxe:ListEditItem Text="رسیده به گوشی" Value="24" />
                                    <dxe:ListEditItem Text="نرسیده به گوشی" Value="25" />
                                    <dxe:ListEditItem Text="رسیده به مخابرات" Value="26" />
                                    <dxe:ListEditItem Text="نرسیده به مخابرات" Value="27" />
                                    <dxe:ListEditItem Text="رسیده به مخابرات-نامشخص بودن وضعیت" Value="28" />
                                    <dxe:ListEditItem Text="عدم ارسال" Value="1" />
                                    <dxe:ListEditItem Text="فاقد شماره همراه" Value="3" />
                                    <dxe:ListEditItem Text="نامشخص" Value="29" />
                                </Items>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnReportManu" runat="server" Width="20px" Height="20px" ImageUrl="~/Images/SMSNotSent.png">
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="50px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <asp:ObjectDataSource ID="objdSMSReManual" runat="server" SelectMethod="FindBySMSId"
                    TypeName="TSP.DataManager.SmsRecieverManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="SMSId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="recievertype" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsSMSDeliveryReType" runat="server" SelectMethod="FindBySMSId"
                    TypeName="TSP.DataManager.SmsRecieverManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="SMSId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="recievertype" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>


                            <table>
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بارگذاری مجدد وضعیت ارسال"
                                                ID="btnRefresh2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(e,s){                                            
                                                CallbackSMS.PerformCallback('Timer');
                                            }" />

                                                <Image Url="~/Images/icons/SMSReportRefresh.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                                ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){
                                                        popupChooseType.Show();
                                                        cmbListType.SetSelectedIndex(-1); }"></ClientSideEvents>

                                                <Image Url="~/Images/icons/ExportExcel.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">

                                                <Image Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField ID="HiddenFieldSMS" ClientInstanceName="HiddenFieldSMS" runat="server">
                </dxhf:ASPxHiddenField>

                <asp:ObjectDataSource ID="objdSMSReMe" runat="server" SelectMethod="FindBySMSId"
                    TypeName="TSP.DataManager.SmsRecieverManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="SMSId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="recievertype" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <%-- <dx:ASPxTimer ID="TimerGridBind" runat="server" Interval="300000">
                    <ClientSideEvents Tick="function(e,s){processOnServer = false;if(HiddenFieldSMS.Get('Timer')==1){ CallbackSMS.PerformCallback('Timer');}}" />
                </dx:ASPxTimer>--%>
                <TSPControls:CustomASPxPopupControl ID="popupChooseType" runat="server" Width="300px"
                    AutoUpdatePosition="true" ClientInstanceName="popupChooseType"
                    HeaderText="تنظیمات">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">نوع خروجی اکسل را مشخص نمائید
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>نوع لیست
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server" ID="cmbListType"
                                            ClientInstanceName="cmbListType" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <Items>
                                                <dxe:ListEditItem Text="اعضای حقیقی" Value="0" />
                                                <dxe:ListEditItem Text="کارمندان" Value="1" />
                                                <dxe:ListEditItem Text="شماره های دستی" Value="2" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ListType">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" ErrorText="نوع لیست را انتخاب کنید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <TSPControls:CustomAspxButton  runat="server" Text="&nbsp;&nbsp;ذخیره"
                                            ID="btnSetList" ValidationGroup="ListType"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e){
                                            if (ASPxClientEdit.ValidateGroup('ListType') == false)
                                                return;
                                            btnTempExport.DoClick();
                                            popupChooseType.Hide(); }"></ClientSideEvents>
                                            <Image Width="16px" Height="16px" Url="~/Images/ok.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>

                </TSPControls:CustomASPxPopupControl>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporterMember" runat="server" GridViewID="grdSMSReciever">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporterEmployee" runat="server" GridViewID="GridViewSMSReEmp">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporterNumber" runat="server" GridViewID="GridViewSMSReMaual">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomTextBox ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
        runat="server" OnClick="btntemp_Click">
    </TSPControls:CustomTextBox>
</asp:Content>
