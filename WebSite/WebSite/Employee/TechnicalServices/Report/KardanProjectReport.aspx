<%@ Page Title="گزارش کارکرد کاردان ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="KardanProjectReport.aspx.cs" Inherits="Employee_TechnicalServices_Report_KardanProjectReport" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table >
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="btnChoosecolumn" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewProject.IsCustomizationWindowVisible())
		GridViewProject.ShowCustomizationWindow();
	else
		GridViewProject.HideCustomizationWindow();
}" />
                                    <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
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
                        <td valign="top" align="right">
                            <asp:Label runat="server" Text="نمایندگی کاردان" ID="Label2"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                TextField="Name" ID="ComboAgent" ClientInstanceName="ComboAgent" DataSourceID="ObjectdatasourceAgent"
                                ValueType="System.Int32" ValueField="AgentId" RightToLeft="True"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />

                            </TSPControls:CustomAspxComboBox>

                            <asp:ObjectDataSource ID="ObjectdatasourceAgent" runat="server" SelectMethod="FindByCode"
                                TypeName="TSP.DataManager.AccountingAgentManager">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="کد پروژه" ID="Label4"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtProjectId" ClientInstanceName="txtProjectId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RegularExpression ErrorText="کد پروژه را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="نام" ID="Label1"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtFirstName" ClientInstanceName="txtFirstName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="نام خانوادگی" ID="Label3"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtLastName" ClientInstanceName="txtLastName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="کد کانون کاردان ها" ID="Label5"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtOtpCode" ClientInstanceName="txtOtpCode">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <br />
                            <table>
                                <tr>
                                    <td align="left">
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server"
                                            Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="true" OnClick="btnSearch_Onclick">
                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer=false;
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton ID="ASPxButton3" runat="server" AutoPostBack="false"
                                            Text="پاک کردن فرم" Width="100px" UseSubmitBehavior="False" OnClick="btnSearch_Onclick">
                                            <ClientSideEvents Click="function(s, e) {	
	   	 ClearSearch();
}"></ClientSideEvents>
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
    <TSPControls:CustomAspxDevGridView ID="GridViewProject" DataSourceID="ObjdKardanProject" runat="server" Width="100%"
        ClientInstanceName="GridViewProject"
        KeyFieldName="PrjCapacityDecrementId" AutoGenerateColumns="False">
        <SettingsCookies Enabled="true" />
        <SettingsCustomizationWindow Enabled="True" />
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="CapacityDecrement" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="Wage" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="ProjectId" SummaryType="Count" />
        </TotalSummary>
        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="ProjectId"
                Caption="کد پروژه" Name="ProjectId">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="FirstName"
                Caption="نام" Name="FirstName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="LastName"
                Caption="نام خانوادگی" Name="LastName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="OtpCode"
                Caption="کد کانون کاردان ها" Name="OtpCode">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="AgentName"
                Caption="نمایندگی کاردان" Name="AgentName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="PrjAgentName"
                Caption="نمایندگی پروژه" Name="PrjAgentName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="MjName"
                Caption="رشته" Name="MjName">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="MjParentName"
                Caption="گروه رشته" Name="MjParentName">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="OwnerFullName"
                Caption="مالک" Name="OwnerFullName">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="DecreasedDate"
                Caption="تاریخ کسر ظرفیت" Name="DecreasedDate">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ActivityIngridientTypeTitle"
                Caption="مسئولیت" Name="ProjectIngridientResName" Width="80px">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientTypeTitle"
                Caption="زمینه کسر ظرفیت" Name="ProjectIngridientDecreaseTypeName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت"
                Name="DecrementPercent" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="False" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Wage" Caption="متراژ دستمزد"
                Name="WagePercent" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Foundation" Caption="متراژ پروژه"
                Name="Foundation" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="YearWork" Caption="سال کاری"
                Name="YearWork" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="True" />
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="50px" FieldName="GroupName"
                Caption="گروه ساختمان" Name="GroupName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="CitName"
                Caption="شهر" Name="CitName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="ArchiveNo"
                Caption="کد بایگانی پروژه" Name="ArchiveNo">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="سهم سازمان(ریال)" FieldName="NezamShare" VisibleIndex="0"
                Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم کانون کاردان ها(ریال)" FieldName="NezamKardanShare" VisibleIndex="0"
                Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="سهم ناظر(ریال)" FieldName="ObserverShare"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم بیمه(ریال)" FieldName="InsuranceShare"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="CeateDateObsInAccList"
                Caption="ثبت در لیست حق الزحمه ناظرین" Name="CeateDateObsInAccList">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="CoordinatorObserver"
                Caption="ناظر هماهنگ کننده" Name="CoordinatorObserver">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>

    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewProject"
        ExportedRowType="All">
    </dxwgv:ASPxGridViewExporter>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">

                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="btnChoosecolumn2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewProject.IsCustomizationWindowVisible())
		GridViewProject.ShowCustomizationWindow();
	else
		GridViewProject.HideCustomizationWindow();
}" />

                                    <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="ObjdKardanProject" runat="server" SelectMethod="ReportProjectCapacityDecrementForKardan"
        TypeName="TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="AgentId" DefaultValue="-1" Type="Int32" />
            <asp:Parameter Name="ProjectId" DefaultValue="-1" Type="Int32" />
            <asp:Parameter Name="OtpCode" DefaultValue="%" Type="String" />
            <asp:Parameter Name="FirstName" DefaultValue="%" Type="String" />
            <asp:Parameter Name="LastName" DefaultValue="%" Type="String" />

        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

