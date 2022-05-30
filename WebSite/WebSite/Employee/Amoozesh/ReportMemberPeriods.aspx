<%@ Page Title="گزارش دورهای گذرانده شده" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ReportMemberPeriods.aspx.cs" Inherits="Employee_Amoozesh_ReportMemberPeriods" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/ExportExcel.png">
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
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="جستجو"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

            


                <table dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">کد عضویت</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtMeId"
                                     ClientInstanceName="txtMeId">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td style="vertical-align: top;" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="عنوان درس" ID="ASPxLabel6">
                                </dxe:ASPxLabel>
                            </td>
                            <td dir="ltr" valign="top" align="right" style="vertical-align: top">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="CrsName" ID="cmbCourse" 
                                    DataSourceID="odbCourseName" ValueType="System.String" ValueField="CrsId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="cmbCourse" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>

                                <asp:ObjectDataSource ID="odbCourseName" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ شروع دوره از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtStartDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtStartDateFrom" ID="PersianDateValidator2"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ شروع دوره تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtStartDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtStartDateTo" ID="PersianDateValidator3"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ پایان دوره از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtEndDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtEndDateFrom" ID="PersianDateValidator4"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ پایان دوره تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtEndDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtEndDateTo" ID="PersianDateValidator5"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                        </tr>
                        <tr>
                            <td>پایه طراحی</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="GrdName" ID="CmbGrdDes" 
                                    DataSourceID="ObjectDataSourceDocGrade" ValueType="System.String" ValueField="GrdId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbGrdDes" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                                <asp:ObjectDataSource ID="ObjectDataSourceDocGrade" runat="server" SelectMethod="GetData"
                                    TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                            </td>
                            <td>پایه نظارت</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"  TextField="GrdName" ID="CmbGrdObs" 
                                    DataSourceID="ObjectDataSourceDocGrade" ValueType="System.String" ValueField="GrdId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbGrdObs" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>پایه اجرا</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="GrdName" ID="CmbGrdImp" 
                                    DataSourceID="ObjectDataSourceDocGrade" ValueType="System.String" ValueField="GrdId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbGrdImp" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                            </td>
                            <td>پایه ترافیک</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="GrdName" ID="CmbGrdTraffic" 
                                    DataSourceID="ObjectDataSourceDocGrade" ValueType="System.String" ValueField="GrdId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbGrdTraffic" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>پایه شهرسازی</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"  TextField="GrdName" ID="CmbGrdUrbonism" 
                                    DataSourceID="ObjectDataSourceDocGrade" ValueType="System.String" ValueField="GrdId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbGrdUrbonism" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                            </td>
                            <td>پایه نقشه برداری</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"  TextField="GrdName" ID="CmbGrdMapping" 
                                    DataSourceID="ObjectDataSourceDocGrade" ValueType="System.String" ValueField="GrdId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbGrdMapping" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                           <td>رشته عضویت</td>
                            <td>
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"  TextField="MjParentName" ID="CmbMjParents" 
                                    DataSourceID="ObjectDataSourceMjParents" ValueType="System.String" ValueField="MjParentId" 
                                    EnableIncrementalFiltering="True" ClientInstanceName="CmbMjParents" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>
                                <asp:ObjectDataSource ID="ObjectDataSourceMjParents" runat="server" SelectMethod="FindMjParents"
                                    TypeName="TSP.DataManager.MajorParentsManager"></asp:ObjectDataSource>
                            </td>
                             <td></td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="4">
                                <br />
                                <table>
                                    <tr>
                                        <td align="left" valign="top">
                                            <TSPControls:CustomAspxButton  runat="server" Text="جستجو"  ID="btnSearch" ClientInstanceName="btnSearch" OnClick="btnSearch_Click"
                                                AutoPostBack="False" UseSubmitBehavior="False" 
                                                Height="25px" Width="92px">
                                                <ClientSideEvents Click="function(s, e) {
e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid'))
{
	   if(CheckSearch()==0)
        {
          alert('تکمیل یکی از فیلدهای اطلاعاتی جستجو به غیر از فیلدهای مربوط به رشته و پایه الزامی است.'); 
          return; 
        }
        else
         e.processOnServer=true;
 }
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton  runat="server" Text="پاک کردن فرم"  ID="btnClear" OnClick="btnSearch_Click"
                                                AutoPostBack="False" UseSubmitBehavior="False" >
                                                <ClientSideEvents Click="function(s, e) {

        ClearSearch();
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
           <p class="HelpUL">
                                                            لازم است هر کاربر قبل از اقدام به جستجو نکات زیر را مطالعه کرده و درنظر گرفته شود.
                                                        </p>
                                                        <dt>پارامترهای لازم برای اخذ گزارش</dt>
                                                        <dd>از میان پارامترهای کد عضویت، عنوان درس، بازه تاریخ شروع دوره و بازه تاریخ پایان دوره حداقل یک پارامتر باید تکمیل شود.</dd>
                                                   
                                                        <dt>پیچیدگی گزارش</dt>
                                                        <dd>بر اساس نیازمندی مطرح شده کاربران این گزارش برای سرور هزینه بر است و در صورتی که
                                                            بازه های بزرگی برای گزارش در نظر گرفته شود احتمال مواجه شدن با خطای مدت زمان زیاداست.     
                                                        </dd>
                                                      
                <br />
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewReport" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="GridViewReport" DataSourceID="OdbPeriodRegisterReport" KeyFieldName="PPId" 
          Width="100%" RightToLeft="True">
        <SettingsBehavior ColumnResizeMode="Control" />
        <Columns>

            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام عضویت" FieldName="MeName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کدملی" FieldName="SSN" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته عضویت" FieldName="LastMjName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه اجرا" FieldName="ImpName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="ObsName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه طراحی" FieldName="DesName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه ترافیک" FieldName="TrafficName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه شهرسازی" FieldName="UrbanismName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه نقشه برداری" FieldName="MappingName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="تاریخ اجرا" FieldName="ImpGradeDate" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ نظارت" FieldName="ObsGradeDate" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ طراحی" FieldName="DesGradeDate" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ترافیک" FieldName="TrafficGradeDate" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ شهرسازی" FieldName="UrbanismGradeDate" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ نقشه برداری" FieldName="MappingGradeDate" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="نمره" FieldName="Mark" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته عضویت" FieldName="LastMjName" VisibleIndex="0">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="عنوان دوره" FieldName="CrsTitle" VisibleIndex="0"
                Width="160px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد دوره" FieldName="PPCode" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ آزمون" FieldName="TestDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="طول دوره(ساعت)" FieldName="Duration" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewCommandColumn Caption=" " ShowClearFilterButton="true" VisibleIndex="7" Width="30px">
            
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True" />
    </TSPControls:CustomAspxDevGridView>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewReport">
    </dx:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="OdbPeriodRegisterReport" runat="server" SelectMethod="spReportPeriodMembersWithGradeDate"
        TypeName="TSP.DataManager.PeriodRegisterManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="CrsId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="FromStartDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="ToStartDate" Type="String" />
            <asp:Parameter DefaultValue="1" Name="FromEndDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="ToEndDate" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="ImpGrdId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="ObsGrdId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="DesGrdId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="UrbanismGrdId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MappingGrdId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TrafficGrdId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MjParentId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>

