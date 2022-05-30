<%@ Page Title="گزارش مدارک تحصیلی اعضا" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MemberLicenceReport.aspx.cs" Inherits="Employee_MembersRegister_MemberLicenceReport" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

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
        function ClearSearch() {
            txtMeIdFrom.SetText('');
            txtMeIdTo.SetText('');
        }
    </script>
    <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%" dir="rtl">
                    <tbody>
                        <tr>
                            <td width="1%">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                     
                                
                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                          
                            <td width="99%">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب ستون ها"
                                    ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                 
                                    <Image  Url="~/Images/icons/cursor-hand.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="جستجو" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" style="width: 15%">
                                <dxe:ASPxLabel runat="server" Text="کد عضویت از" ID="ASPxLabel1" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="width: 35%">
                                <TSPControls:CustomTextBox  runat="server"  Width="100%" ID="txtMeIdFrom" ClientInstanceName="txtMeIdFrom"
                                    >
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                        <RequiredField IsRequired="true" ErrorText="کد عضویت وارد نمایید" />
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td valign="top" align="right" style="width: 15%">
                                <dxe:ASPxLabel runat="server" Text="کد عضویت تا" Width="100%" ID="ASPxLabel2">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" style="width: 35%">
                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtMeIdTo" ClientInstanceName="txtMeIdTo"
                                    >
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                        <RequiredField IsRequired="true" ErrorText="کد عضویت وارد نمایید" />
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="4">
                                <table>
                                    <tr>
                                        <td align="left" valign="top">
                                            <TSPControls:CustomAspxButton  runat="server" Text="جستجو"  ID="btnSearch" AutoPostBack="true"
                                                UseSubmitBehavior="False"  Width="98px"
                                                ClientInstanceName="btnSearch" OnClick="btnSearch_OnClick">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton   runat="server" Text="پاک کردن فرم"  ID="btnClearSearch"
                                                AutoPostBack="true" UseSubmitBehavior="False" 
                                                Width="98px" ClientInstanceName="btnClearSearch" OnClick="btnSearch_OnClick">
                                                <ClientSideEvents Click="function(s, e) {

	 ClearSearch();
         e.processOnServer=true;
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
    <br />
    <TSPControls:CustomAspxDevGridView SettingsDetail-ExportMode="Expanded" ID="GridViewMember"
        runat="server" DataSourceID="ObjdsMembers" Width="100%"
        ClientInstanceName="grid">
        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
        <SettingsCustomizationWindow Enabled="True" />
        <SettingsCookies Enabled="true" StoreColumnsVisiblePosition="true" StoreColumnsWidth="true" />
        <Columns>

            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Name="MeId"
                VisibleIndex="0" Width="70px" Visible="true">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" Name="MeNo"
                VisibleIndex="0" Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" Visible="False" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="0"
                Width="15%" Visible="false">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" VisibleIndex="0"
                Width="10%" Visible="false">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>



            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" Name="FileNo"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="ObsGrade" Name="ObsGrade"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه طراحی" FieldName="DesGrade" Name="DesGrade"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه اجرا" FieldName="ImpGrade" Name="ImpGrade"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه شهرسازی" FieldName="UrbanismGrade" Name="UrbanismGrade"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه ترافیک" FieldName="TrafficGrade" Name="TrafficGrade"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه نقشه برادی" FieldName="MappingGrade" Name="MappingGrade"
                VisibleIndex="4" Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="رشته تحصیلی" FieldName="MjName" 
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد رشته" FieldName="MjCode" 
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="مقطع تحصیلی" FieldName="LiName" 
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="رشته پیش فرض عضویت" FieldName="IsDefualtMembership" 
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته پیش فرض پروانه" FieldName="IsDefualtDocLicence"
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تصویر مدرک" FieldName="ImageURL"
                VisibleIndex="0">
                <CellStyle Wrap="False"  >
                </CellStyle>                
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="تصویر قبولی آزمون سراسری" FieldName="EntranceExamConfImageURL"
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تصویر نامه معادلسازی" FieldName="EquivalentImageURL" 
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تصویر استعلام" FieldName="InquiryImageURL"
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تصویر ریز نمرات" FieldName="ScoresImageURL"
                VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="40px" ShowClearFilterButton="true">
            
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True"></Settings>



    </TSPControls:CustomAspxDevGridView>

    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMember"
        ExportedRowType="All">
    </dxwgv:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="ObjdsMembers" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
        SelectMethod="SelectMemberLicenceImageReport" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="MeIdFrom" Type="Int32" DefaultValue="-1" />
            <asp:Parameter Name="MeIdTo" Type="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%" dir="rtl">
                    <tbody>
                        <tr>

                            <td align="right" width="1%">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                            
                                   
                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td  align="right" width="99%">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب ستون ها"
                                    ID="btnChooseColumn2" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" Visible="true" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                   
                                    <Image  Url="~/Images/icons/cursor-hand.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>

