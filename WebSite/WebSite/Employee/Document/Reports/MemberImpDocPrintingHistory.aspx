<%@ Page Title="گزارش چاپ کارت های مجوز فعالیت مجری حقیقی" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MemberImpDocPrintingHistory.aspx.cs" Inherits="Employee_Document_Reports_MemberImpDocPrintingHistory" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function ShowWFDesc(s, e) {
            GridViewMemberFile.GetRowValues(GridViewMemberFile.GetFocusedRowIndex(), 'wfDescription', OnGetSelectedFieldValues);

        }
        function OnGetSelectedFieldValues(selectedValues) {

            txtWFDesc.SetText(selectedValues);
            PopUpWFDesc.Show();
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS"
        WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS"
        FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS"
        CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
    </pdc:PersianDateScriptManager>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>



                <table>
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){GridViewPrintingHistory.PerformCallback('Print'); }"></ClientSideEvents>

                                <Image Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                <Image Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tr>
                        <td align="right" style="width: 15%">کد عضویت
                            
                        </td>
                        <td align="right" style="width: 35%">
                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMeId" runat="server" ClientInstanceName="txtMeId"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" style="width: 15%">بالاترین پایه</td>
                        <td align="right" style="width: 35%">
                            <TSPControls:CustomAspxComboBox ID="cmbMaxGrade" runat="server" DataSourceID="ObjectDataSourceDocGrade" TextField="GrdName" ValueField="GrdId"
                                ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="cmbMaxGrade"  HorizontalAlign="Right"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />

                            </TSPControls:CustomAspxComboBox>

                            <asp:ObjectDataSource ID="ObjectDataSourceDocGrade" runat="server" SelectMethod="GetData"
                                TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" style="width: 15%">تاریخ چاپ از
                          
                        </td>
                        <td align="right" style="width: 35%">
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" onkeypress="SearchKeyPress(event,2,btnSearch);" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" style="width: 15%">
                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ چاپ تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" style="width: 35%">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" onkeypress="SearchKeyPress(event,2,btnSearch);" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>وضعیت اعتبار
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="CmbIsValid" runat="server"
                                ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="CmbIsValid"  HorizontalAlign="Right"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <Items>
                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                    <dxe:ListEditItem Text="معتبر" Value="0" />
                                    <dxe:ListEditItem Text="نامعتبر" Value="1" />
                                    <dxe:ListEditItem Text="باطل شده" Value="2" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td>شماره سریال کارت
                        </td>
                        <td>
                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtPrtSerialNo" runat="server" ClientInstanceName="txtPrtSerialNo"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />

                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="4" dir="ltr" valign="top">

                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton I ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                            Text="پاک کردن فرم" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
	 ClearSearch();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                            Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid')){
 
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
  }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewPrintingHistory" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="GridViewPrintingHistory" DataSourceID="objdsPrintingHistory"
        KeyFieldName="PrtHId" OnCustomCallback="GridViewPrintingHistory_CustomCallback"
        Width="100%">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="نام عضو" FieldName="MemberFullName" VisibleIndex="2"
                Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام کاربر" FieldName="UserFullName" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="3"
                Width="80px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت اعتبار" FieldName="IsValid" Name="IsValid"
                VisibleIndex="4" Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="معتبر" Value="0" />
                        <dx:ListEditItem Text="نامعتبر" Value="1" />
                        <dx:ListEditItem Text="باطل شده" Value="2" />
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" VisibleIndex="4"
                Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ چاپ" FieldName="CreateDate" VisibleIndex="7"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ساعت چاپ" FieldName="CreateTime" VisibleIndex="8"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>            
            <dxwgv:GridViewDataTextColumn Caption="شماره مجوز" FieldName="MfNoImp" VisibleIndex="5"
                Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="MFNo" VisibleIndex="5"
                Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره سریال کارت" FieldName="PrtSerialNo"
                VisibleIndex="1" Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="بالاترین پایه" FieldName="MaxGradeName" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نظارت" FieldName="ObsGrade" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="طراحی" FieldName="DesginGrade" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="اجرا" FieldName="ImpGrade" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="نقشه برداری" FieldName="MappingGrade" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="ترافیک" FieldName="TrafficGrade" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="شهرسازی" FieldName="URbenismGrade" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>

        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../../Print.aspx');
           s.cpPrint=0;
        }
}" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>



                <table>
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){GridViewPrintingHistory.PerformCallback('Print'); }"></ClientSideEvents>

                                <Image Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                <Image Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="objdsPrintingHistory" runat="server" TypeName="TSP.DataManager.PrintingHistoryManager"
        SelectMethod="SelectPrintHistoryForMemberImpDoc">
        <SelectParameters>
            <asp:Parameter DbType="String" Name="CreateDateFrom" DefaultValue="9999/99/99" />
            <asp:Parameter DbType="String" Name="CreateDateTo" DefaultValue="9999/99/99" />
            <asp:Parameter DbType="String" Name="PrtSerialNo" DefaultValue="%" />
            <asp:Parameter DbType="String" Name="MeId" DefaultValue="-1" />
            <asp:Parameter DbType="String" Name="IsValid" DefaultValue="-1" />
            <asp:Parameter DbType="String" Name="MaxGradeId" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPrintingHistory">
    </dx:ASPxGridViewExporter>
</asp:Content>
