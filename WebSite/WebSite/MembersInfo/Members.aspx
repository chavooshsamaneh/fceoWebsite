<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageWebsite.master"
    CodeFile="Members.aspx.cs" Inherits="Members" Title="اعضای حقیقی سازمان" %>

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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript" language="javascript">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%" dir="ltr" align="center">
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSerch" HeaderText="جستجو" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%" align="center">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <asp:Label runat="server" Text="کد عضویت" ID="Label1"></asp:Label>
                                        </td>
                                        <td align="right" width="35%">
                                            <TSPControls:CustomTextBox Width="100%" runat="server"
                                                ID="txtMeId" ClientInstanceName="txtMeId">
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
                                        <td valign="top" align="right" width="15%">
                                            <asp:Label runat="server" Text="شماره عضویت" ID="Label41"></asp:Label>
                                        </td>
                                        <td align="right" width="35%">
                                            <TSPControls:CustomTextBox Width="100%" runat="server"
                                                ID="txtMeNo" ClientInstanceName="txtMeNo" Style="direction: ltr">
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
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام" ID="Label2"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox Width="100%" runat="server" MaxLength="50"
                                                ClientInstanceName="txtFirst" ID="txtFirstName">
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
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام خانوادگی" ID="Label3"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox Width="100%" runat="server" MaxLength="50"
                                                ClientInstanceName="txtLastName" ID="txtLastName">
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
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <asp:Label runat="server" Text="رشته تحصیلی" ID="Label5"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <div dir="ltr">
                                                <TSPControls:CustomAspxComboBox Width="100%" runat="server" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                                    IncrementalFilteringMode="StartsWith" ValueType="System.String" DataSourceID="ObjectDataSourceMajor"
                                                    TextField="MjName" ValueField="MjId"
                                                    ID="ComboMajor" ClientInstanceName="ComboMajor"
                                                    RightToLeft="True">
                                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </div>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <asp:Label runat="server" Text="مجوز مجری حقیقی" ID="Label27"></asp:Label>

                                        </td>
                                        <td>
                                            <div dir="ltr">
                                                <TSPControls:CustomAspxComboBox Width="100%" runat="server" DropDownStyle="DropDown" SelectedIndex="0"
                                                    ID="ComboImplement" ClientInstanceName="ComboImplement"
                                                    RightToLeft="True">
                                                    <Items>
                                                        <dxe:ListEditItem Selected="True" Text="----" Value="-1"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="دارد" Value="1"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="ندارد" Value="0"></dxe:ListEditItem>

                                                    </Items>
                                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="4">
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" Text="جستجو"
                                                ID="btnSearch" ClientInstanceName="btnSearch"
                                                OnClick="btnSearch_Click">
                                                <ClientSideEvents Click="function(s,e){
                                                if(txtMeId.GetText()=='' &&
                                                    txtMeNo.GetText()=='' &&
                                                    txtFirst.GetText()=='' &&
                                                    txtLastName.GetText()=='' &&
                                                    ComboMajor.GetSelectedIndex()==0 &&
                                                    ComboImplement.GetSelectedIndex()==0)
                                                    {
                                                    alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.');
                                                    e.processOnServer=false;
                                                    return;
                                                    }
                                                    e.processOnServer=true;
                                                }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
            </div>
            <ul class="HelpUL">
                <li>توجه نمایید در لیست زیر تنها شماره تلفن همراه افرادی نمایش داده می شود که پیش از
                    این از طریق پرتال شخصی خود در همین سایت موافقت خود جهت نمایش آن را اعلام نموده اند.</li>
            </ul>
            <TSPControls:CustomAspxDevDataView ID="DataViewMembers" ClientInstanceName="DataViewMembers"
                DataSourceID="ObjectDataSourceMember" runat="server" ColumnCount="1" RowPerPage="10"
                AlwaysShowPager="True" Width="100%" PagerSettings-EndlessPagingMode="OnClick">

                <ItemTemplate>
                    <table width="100%" class="TableBorder">
                        <tbody>
                            <tr>
                                <td align="right" valign="top" colspan="2">
                                    <span class="TitleOragne"><strong><%# Eval("SexName") %> <%# Eval("TiName") %>  <%# Eval("FirstName") %> <%# Eval("LastName") %></strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" width="20%">
                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" Width="100px" ImageUrl='<%# Bind("ImageUrl") %>'
                                        Height="100px">
                                        <EmptyImage Height="100px" Width="100px" Url="~/Images/person.png">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                                <td align="right" valign="top" width="80%">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td width="15%">
                                                    <asp:Label ID="Label17" runat="server" Text="کد عضویت" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width="35%">
                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("MeId") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:Label ID="Label117" runat="server" Text="پروانه اشتغال به کار" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width="30%">
                                                    <asp:Label ID="Label118" runat="server" Text='<%# Bind("FileNo") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="شماره عضویت" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("MeNo") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text="تلفن همراه" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("MobileNoForShow") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Text="رشته تحصیلی" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("MjNames") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="آخرین مقطع تحصیلی" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label20" runat="server" Text='<%# Bind("LiNames") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label217" runat="server" Text="صلاحیت نظارت" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label218" runat="server" Text='<%# Bind("ObsGrdName") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label317" runat="server" Text="صلاحیت طراحی" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label318" runat="server" Text='<%# Bind("DesGrdName") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label417" runat="server" Text="صلاحیت اجرا" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label418" runat="server" Text='<%# Bind("ImpGrdName") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="صلاحیت شهرسازی" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("UrbanismName") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label21" runat="server" Text="صلاحیت ترافیک" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label22" runat="server" Text='<%# Bind("TrafficName") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label23" runat="server" Text="صلاحیت نقشه برداری" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" Text='<%# Bind("MappingName") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="تاریخ پایان اعتبار پروانه اشتغال" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("FileDate") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text="شماره مجوز اجرا" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" Text='<%# Bind("ImpDocMFNo") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Text="تاریخ پایان اعتبار مجوز اجرا " Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label26" runat="server" Text='<%# Bind("ImpDocExpireDate") %>' Font-Size="8pt"
                                                        ForeColor="DarkBlue"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ItemTemplate>
            </TSPControls:CustomAspxDevDataView>



            <asp:ObjectDataSource ID="ObjectDataSourceMajor" runat="server" SelectMethod="FindMjParents"
                TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceMember" runat="server" SelectMethod="SearchMemberForPublic"
                TypeName="TSP.DataManager.MemberManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String"></asp:Parameter>
                    <asp:Parameter DefaultValue="%" Name="LastName" Type="String"></asp:Parameter>
                    <asp:Parameter DefaultValue="%" Name="MeNo" Type="String"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="HasImpDoc" Type="Int16"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
