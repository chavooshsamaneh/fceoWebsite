<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="Office.aspx.cs" Inherits="Office" Title="اعضای حقوقی سازمان" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript">
        function SearchKeyPress(e, Type) {
            if (Type == 1) {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2) {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>


    <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanelMenu2" HeaderText="جستجو" runat="server" ShowCollapseButton="true"
        Width="100%">

        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tr>
                        <td align="right" valign="top" width="15%">کد شرکت
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomTextBox ID="txtOfId" runat="server"
                                ClientInstanceName="TextOfId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="bottom"
                                    ValidationGroup="Member">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت نا معتبر است" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top">شماره عضویت
                             
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtMeNo" runat="server"
                                ClientInstanceName="txtMeNo">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                    ValidationGroup="Member">


                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>

                    </tr>
                    <tr>
                        <td align="right" valign="top" width="15%">نام شرکت
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomTextBox ID="TextOfName" runat="server"
                                ClientInstanceName="TextOfName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">نام و نام خانوادگی مدیر عامل
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomTextBox ID="txtManagerName" runat="server"
                                ClientInstanceName="txtManagerName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">نوع پروانه
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox ID="cmbMFType" runat="server"
                                ValueType="System.String"
                                RightToLeft="True" HorizontalAlign="Right" EnableIncrementalFiltering="True"
                                ClientInstanceName="cmbMFType">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="طراح و ناظر" Value="1" />
                                    <dxe:ListEditItem Text="مجری" Value="2" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right" valign="top">پایه شرکت
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="comboGrade" runat="server"
                                ValueType="System.String"
                                TextField="GrdName" ValueField="GrdId" RightToLeft="True" ClientInstanceName="comboGrade"
                                DataSourceID="ObjectDataSourceDocGrade" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomAspxComboBox>

                            <asp:ObjectDataSource ID="ObjectDataSourceDocGrade" runat="server" SelectMethod="GetData"
                                TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="center">
                            <br />
                            <table>
                                <tr>
                                    <td width="50%" align="left">
                                        <TSPControls:CustomAspxButton runat="server" AutoPostBack="true" OnClick="btnSearch_Click" UseSubmitBehavior="False" CausesValidation="False"
                                            Text="&nbsp;جستجو"
                                            Width="126px" ID="btnSearch" ClientInstanceName="btnSearch">
                                            <Image Width="20px" Height="20px" Url="~/Images/icons/Search.png" />
                                            <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
  
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="50%" align="right">
                                        <TSPControls:CustomAspxButton runat="server" Text="&nbsp;پاک کردن فرم" CausesValidation="False"
                                            ID="btnMeRefresh" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="False"
                                            Width="126px">
                                            <Image Height="20px" Width="20px" Url="~/Images/icons/Clear-Form.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
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
    <TSPControls:CustomAspxDevDataView ID="DataViewOffice" runat="server" DataSourceID="ObjectDataSourceOffice" ColumnCount="1" RowPerPage="10"
        AlwaysShowPager="True" Width="100%" PagerSettings-EndlessPagingMode="OnClick">

        <ItemTemplate>
            <table width="100%" class="TableBorder">
                <tbody>
                    <tr>
                        <td align="right" valign="top" colspan="2">
                            <span class="TitleOragne"><strong><%# Eval("OfName") %></strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" width="20%"></td>
                        <td align="right" valign="top" width="80%">
                            <table width="100%">

                                <tbody>

                                    <tr>
                                        <td width="15%">
                                            <asp:Label ID="Label171" runat="server" Text="كد عضويت"></asp:Label>

                                        </td>
                                        <td width="35%">
                                            <asp:Label ID="Label181" runat="server" Text='<%# Bind("OfId") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label10" runat="server" Text="شماره عضويت"></asp:Label>

                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("MeNo") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="مدیر عامل"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("ManagerFullName") %>' ForeColor="DarkBlue"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>

                                        </td>

                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="شماره همراه مدیر عامل"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("ManagerMobileNo") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="تلفن"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Tel1") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="شماره پروانه"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("FileNo") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Width="36px" Text="نوع پروانه"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("MFTypeName") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="تاریخ اعتبار پروانه"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("FileDate") %>' ForeColor="DarkBlue"></asp:Label>
                                        </td>
                                    </tr>

                                </tbody>


                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>

        </ItemTemplate>
    </TSPControls:CustomAspxDevDataView>



    <asp:ObjectDataSource ID="ObjectDataSourceOffice" runat="server" SelectMethod="SelectOfficeForSearchPortal" TypeName="TSP.DataManager.OfficeManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="MeNo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="OfName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="ManagerFullName" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="MFType" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="GrdId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

