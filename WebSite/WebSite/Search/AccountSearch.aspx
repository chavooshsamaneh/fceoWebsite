<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AccountSearch.aspx.cs" Inherits="Search_AccountSearch" Title="جستجوی حسابهای مالی" %>
<%--
<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
function MemberSearch(s,e)
{
if(ASPxClientEdit.ValidateGroup('Member')==false)
   return;
grdMembers.PerformCallback('');
}

/************** Grid Selection *********************/
var _selectNumber = 0;
        var _handle = true;

        function cbSelectAllCheckedChanged(s, e) {
            if (s.GetChecked())
                grdMembers.SelectRows();
            else
                grdMembers.UnselectRows();
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
            if(chkMultiSelect.GetChecked()==true){
             if(grdMembers.GetSelectedRowCount()>0){
              txtSelectedMeId.SetText('در حال بارگذاری...');          
              grdMembers.GetSelectedFieldValues('MeId', OnGetSelectedFieldValues);
             }
             else
              txtSelectedMeId.SetText('');
            }
        }
        function OnGridEndCallback(s, e) {
            _selectNumber = s.cpSelectedRowsOnPage;
       }
function OnGetSelectedFieldValues(selectedValues)
{
    if(selectedValues.length == 0) return;
    var MeId='';
for(i = 0; i < selectedValues.length; i++) {
if(MeId!='') MeId+=';';
MeId+=selectedValues[i];
}
txtSelectedMeId.SetText(MeId);
}
/*******************************************************/
    </script>

    <div dir="ltr">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" BackColor="#EBF2F4" 
             Width="100%" ShowHeader="False">
            <LeftEdge>
                <BackgroundImage ImageUrl="~/App_Themes/Glass/Web/rpLeftRightEdge.gif" Repeat="RepeatX"
                    VerticalPosition="bottom" />
            </LeftEdge>
            <BottomRightCorner Height="5px" Url="~/App_Themes/Glass/Web/rpBottomRightCorner.png"
                Width="5px" />
            <ContentPaddings PaddingBottom="2px" PaddingLeft="4px" PaddingTop="2px" />
            <NoHeaderTopRightCorner Height="5px" Url="~/App_Themes/Glass/Web/rpNoHeaderTopRightCorner.png"
                Width="5px" />
            <RightEdge>
                <BackgroundImage ImageUrl="~/App_Themes/Glass/Web/rpLeftRightEdge.gif" Repeat="RepeatX"
                    VerticalPosition="bottom" />
            </RightEdge>
            <HeaderRightEdge>
                <BackgroundImage ImageUrl="~/App_Themes/Glass/Web/rpHeaderRightEdge.gif" VerticalPosition="bottom" />
            </HeaderRightEdge>
            <Border BorderColor="#7EACB1" BorderStyle="Solid" BorderWidth="1px" />
            <Content>
                <BackgroundImage ImageUrl="~/App_Themes/Glass/Web/rpContentBack.gif" Repeat="RepeatX"
                    VerticalPosition="bottom" />
            </Content>
            <HeaderLeftEdge>
                <BackgroundImage ImageUrl="~/App_Themes/Glass/Web/rpHeaderLeftEdge.gif" Repeat="RepeatX"
                    VerticalPosition="bottom" />
            </HeaderLeftEdge>
            <HeaderStyle Height="23px" BackColor="White">
                <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <BottomEdge BackColor="#D7E9F1">
            </BottomEdge>
            <TopRightCorner Height="5px" Url="~/App_Themes/Glass/Web/rpTopRightCorner.png" Width="5px" />
            <HeaderContent>
                <BackgroundImage ImageUrl="~/App_Themes/Glass/Web/rpHeaderBack.gif" Repeat="RepeatX"
                    VerticalPosition="bottom" />
            </HeaderContent>
            <NoHeaderTopEdge BackColor="#EBF2F4">
            </NoHeaderTopEdge>
            <NoHeaderTopLeftCorner Height="5px" Url="~/App_Themes/Glass/Web/rpNoHeaderTopLeftCorner.png"
                Width="5px" />
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <div dir="rtl" align="center">
                        <table>
                            <tr>
                                <td align="right">
                                    کد عضویت </td>
                                <td align="right">
                                    <TSPControls:CustomTextBox ID="txtMeId" runat="server" 
                                         Width="170px">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right"
                                            ValidationGroup="Member">
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت صحیح وارد شده است" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td align="right">
                                    نام </td>
                                <td align="right">
                                    <TSPControls:CustomTextBox ID="txtFName" runat="server" 
                                         Width="170px">
                                        <ValidationSettings>
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    رشته </td>
                                <td dir="ltr">
                                    <TSPControls:CustomAspxComboBox ID="cmbMajor" runat="server" 
                                          ValueType="System.String"
                                        Width="170px" HorizontalAlign="Right" TextField="MjName" ValueField="MjId" EnableIncrementalFiltering="True"
                                        DataSourceID="ObjectDataSource_MajorParents">
                                        <ValidationSettings>
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    نام خانوادگی </td>
                                <td align="right">
                                    <TSPControls:CustomTextBox ID="txtLName" runat="server" 
                                         Width="170px">
                                        <ValidationSettings>
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center">
                                    <TSPControls:CustomTextBox runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                        Text="&nbsp;جستجو"  
                                        Width="100px" ID="ASPxButton10">
                                        <Image Width="20px" Height="20px" Url="~/Images/icons/Search.png" />
                                        <ClientSideEvents Click="MemberSearch" />
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </dxp:PanelContent>
            </PanelCollection>
            <TopLeftCorner Height="5px" Url="~/App_Themes/Glass/Web/rpTopLeftCorner.png" Width="5px" />
            <BottomLeftCorner Height="5px" Url="~/App_Themes/Glass/Web/rpBottomLeftCorner.png"
                Width="5px" />
        </dxrp:ASPxRoundPanel>
    </div>
    <br />
    <div align="right">
        <TSPControls:CustomASPxCheckBox ID="chkMultiSelect" runat="server" Text="انتخاب گروهی" ClientInstanceName="chkMultiSelect">
            <ClientSideEvents CheckedChanged="function(s,e){
          grdMembers.PerformCallback('MultiSelect;'+s.GetChecked());
          PanelSelectedMeId.SetVisible(s.GetChecked());
          //if(s.GetChecked()==false) grdMembers.UnselectRows();
   }" />
        </TSPControls:CustomASPxCheckBox>
    </div>
    <br />
    <TSPControls:CustomAspxDevGridView runat="server"  Font-Size="7.5pt"
        Width="100%" ID="grdMembers" DataSourceID="ObjectDataSource1" ClientInstanceName="grdMembers"
        KeyFieldName="MeId" AutoGenerateColumns="False" 
        OnCustomJSProperties="grdMembers_CustomJSProperties" OnCustomCallback="grdMembers_CustomCallback">
        <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟"
            EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="برای گروه بندی از این قسمت استفاده کنید"
            CommandEdit="ویرایش" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید"
            CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>
        <Styles  >
            <GroupPanel BackColor="CornflowerBlue" ForeColor="Black">
            </GroupPanel>
            <Header HorizontalAlign="Center">
            </Header>
            <SelectedRow BackColor="Wheat">
            </SelectedRow>
            <AlternatingRow BackColor="WhiteSmoke">
            </AlternatingRow>
        </Styles>
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
        <SettingsPager PageSize="20" CurrentPageNumberFormat="{0}">
            <AllButton Text="همه رکوردها">
            </AllButton>
            <FirstPageButton Text="اولین صفحه">
            </FirstPageButton>
            <LastPageButton Text="آخرین صفحه">
            </LastPageButton>
            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>
            <NextPageButton Text="صفحه بعد">
            </NextPageButton>
            <PrevPageButton Text="صفحه قبل">
            </PrevPageButton>
        </SettingsPager>
        <ClientSideEvents SelectionChanged="OnGridSelectionChanged" EndCallback="OnGridEndCallback"
            RowDblClick="function(s,e){if(chkMultiSelect.GetChecked())s.SelectRowOnPage(e.visibleIndex);}" />
        <Columns>
            <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" visible="False">
                <headerstyle horizontalalign="Center" >
<Paddings PaddingTop="1px" PaddingBottom="1px"></Paddings>
</headerstyle>
                <headertemplate>
                          <TSPControls:CustomASPxCheckBox ID="cbSelectAll" runat="server" ClientInstanceName="cbSelectAll"
                             OnInit="cbSelectAll_Init">
                             <ClientSideEvents CheckedChanged="cbSelectAllCheckedChanged" />
                          </TSPControls:CustomASPxCheckBox>
                     
</headertemplate>
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccCode" Caption="کد حساب"
                width="30%" Name="AccCode">
                <cellstyle wrap="False"></cellstyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="AccName" Caption="نام حساب"
                width="60%" Name="AccName">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="AccTypeName" Caption="سطح حساب"
                width="10%" Name="AccTypeName">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="1px">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <dxp:ASPxPanel ID="PanelSelectedMeId" runat="server" ClientInstanceName="PanelSelectedMeId"
        ClientVisible="false">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table width="100%">
                    <tr>
                        <td align="right">
                            کدهای عضویت انتخاب شده :
                        </td>
                        <td align="left">
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tr>
                                    <td >
                                        <TSPControls:CustomTextBox runat="server" Text=" "  ToolTip="کپی"
                                            ID="btnCopy" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                            UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Copy2.png"></Image>
                                            <ClientSideEvents Click="function(s,e){copyToClipboard(txtSelectedMeId.GetText());}" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td >
                                        <TSPControls:CustomTextBox runat="server" Text=" "  ToolTip="حذف انتخاب ها"
                                            ID="btnClear" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                            UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Clear-Form.png"></Image>
                                            <ClientSideEvents Click="function(s,e){grdMembers.UnselectRows();}" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <TSPControls:CustomASPXMemo ID="txtSelectedMeId" runat="server" 
                     Height="80px" Width="100%" ReadOnly="true" ClientInstanceName="txtSelectedMeId">
                    <ValidationSettings>
                        
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </TSPControls:CustomASPXMemo>
            </dxp:PanelContent>
        </PanelCollection>
    </dxp:ASPxPanel>
    <asp:ObjectDataSource ID="ObjectDataSource_MajorParents" runat="server" SelectMethod="FindMjParents"
        TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectMemberForSearch"
        TypeName="TSP.DataManager.MemberManager" CacheDuration="600" CacheExpirationPolicy="Sliding"
        EnableCaching="True">
        <SelectParameters>
            <asp:Parameter Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="" Name="LastName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
</asp:Content>