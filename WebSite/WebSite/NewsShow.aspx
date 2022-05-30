<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageWebsite.master"
    CodeFile="NewsShow.aspx.cs" Inherits="NewsShow" Title="مشاهده خبر" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <script language="javascript">
        function ShowImg(id) {
            //window.alert(id.src);
            window.open(id.src, '_blank');
        }

        function OnMouseOver(e) {
            e.style.cursor = 'hand';
        }
        function Clear() {
            txtName.SetText('');
            txtLastName.SetText('');
            txtEmail_PopupIdea.SetText('');
            txtBody_PopupIdea.SetText('');
            Rating3.SetValue(0);
        }
        function CheckValidating() {
            if (ASPxClientEdit.ValidateGroup('Comment') == false)
                return false;
            //if(txtEmail_PopupIdea.GetIsValid()==false)
            //return false;
            return true;
        }

        function ShowPrintWindow() {
            window.open(HiddenNewsData.Get("PrintAddress"), null, "height=500px, width=800px,resizable=yes, status=no, toolbar=no, menubar=no, location=no, scrollbars=1");
        }
        function OpenTelegram() {
            window.open('https://t.me/share/url?url=' + encodeURIComponent(window.location.href), '_blank');
        }
    </script>

    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="اشتراک گذاری در تلگرام"
                                    ID="btnshareTelegram" EnableViewState="False" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){ OpenTelegram(); }" />
                                    <Image Url="~/Images/icons/telegramlogo.png">
                                    </Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="چاپ"
                                    ID="btnPrint" EnableViewState="False" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){ ShowPrintWindow(); }" />
                                    <Image Url="~/Images/icons/printers.png">
                                    </Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="بازگشت"
                                    ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
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
    <div class="DataViewOneColumn">
        <div class="row topTitleNews">

            <div class="col-md-6 col-sm-6 col-xs-6" style="text-align: right">
                <span id="lblAgent" runat="server"><strong></strong></span>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6" style="text-align: left">
                <span id="lblDate" runat="server"><strong></strong></span>
                <span id="lblTime" runat="server"><strong></strong></span>
            </div>
        </div>
        <div class="row topTitleNews">
            <div class="col-md-6 col-sm-6 col-xs-6" style="text-align: right">
                <span runat="server" id="lblSub"></span>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6" style="text-align: left">
                <span runat="server" id="lblCountOfRead"></span>
            </div>
        </div>
        <div class="row" style="min-height: 50px; text-align: right">
            <span class="TitleOragne titleNews" runat="server" id="lblTitle">
                <%-- <h5><%# Eval("Title") %></h5>--%>
            </span>
        </div>

        <div class="col-md-12">


            <div class="col-md-12 NewsShowBody">
                <span id="lblBody" runat="server"></span>

            </div>
            <div class="col-md-12" style="text-align: right">
                <dxe:ASPxHyperLink ID="HyperLinkAttachment" runat="server" ClientInstanceName="HyperLinkAttachment"
                    Target="_blank" Text="دانلود" Visible="False">
                </dxe:ASPxHyperLink>
            </div>
        </div>
        <div class="row">
            <%--    <div style="width:155px;margin-left:auto;margin-right:auto">   </div>--%>
            <TSPControls:CustomASPXRatingControl ID="Rating1" runat="server" Value="0" ReadOnly="true" FillPrecision="Full">
            </TSPControls:CustomASPXRatingControl>
            <dxe:ASPxHyperLink ID="btnIdea" runat="server" Text="ثبت نظرات" NavigateUrl="#">
                <ClientSideEvents Click="function(s,e){
PopupIdea.Show();
Clear();
}" />
            </dxe:ASPxHyperLink>

        </div>
        <div class="row" style="text-align: center; font-size: xx-small">
            <span runat="server" id="lblCountOfIdea"></span>
        </div>
        <div class="row">
            <div class="col-md-12" align="center">

                <div class="row hidden" id="divImage" style="max-width: 150px; max-height: 150px">
                    <%--  <div onmouseover="OnMouseOver(this);">--%>
                    <asp:Image onclick="ShowImg(this)" Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                        ID="Image1" runat="server"
                        Width="100px" ImageUrl="~/Images/noimage.gif" Height="100px"></asp:Image>
                    <%--</div>--%>
                </div>

            </div>
        </div>
        <div class="row" id="tblAtt" runat="server">
            <fieldset>
                <legend>سایر فایل های پیوست</legend>
                <TSPControls:CustomAspxDevDataView ID="DataViewOtherAttachments" runat="server" ColumnCount="1"
                    RowPerPage="30" Width="100%" RightToLeft="True" ItemSpacing="1px" AlwaysShowPager="True"
                    PagerPanelSpacing="0px">

                    <PagerSettings ShowNumericButtons="True" Visible="True">
                    </PagerSettings>
                    <Border BorderStyle="None" />
                    <ItemStyle Height="1px" Width="1px" Paddings-Padding="0px" HorizontalAlign="Center">
                        <Paddings Padding="0px"></Paddings>
                    </ItemStyle>
                    <ItemTemplate>
                        <table width="100%" cellpadding="5">
                            <tr>
                                <td align="center" width="100%">
                                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" ClientInstanceName="HyperLinkOtherAttachment"
                                        Target="_blank" Text='<%# Bind("Description") %>' NavigateUrl='<%# Bind("ImgUrl") %>'>
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </TSPControls:CustomAspxDevDataView>
            </fieldset>
        </div>
    </div>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="چاپ"
                                    ID="btnPrint2" EnableViewState="False" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){ ShowPrintWindow(); }" />
                                    <Image Url="~/Images/icons/printers.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="بازگشت"
                                    ID="btnBack2" EnableViewState="False" OnClick="btnBack_Click">
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

    <TSPControls:CustomASPxPopupControl ID="PopupIdea" runat="server" ClientInstanceName="PopupIdea"
        HeaderText="نظرات">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table width="100%" class="TableBorder">
                    <tr>
                        <td align="right" style="height: 28px">
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInsert" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnInsert_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False"
                                            ValidationGroup="Comment">

                                            <Image Height="20px" Url="~/Images/icons/save.png" Width="20px" />
                                            <ClientSideEvents Click="function(s,e){
                                                if(CheckValidating())
                                                PopupIdea.Hide();
                                                else
                                                e.ProcessOnServer=false;}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRefresh" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" AutoPostBack="False" Text=" " ToolTip="پاک کردن فرم"
                                            UseSubmitBehavior="False">

                                            <Image Height="20px" Url="~/Images/icons/Clear-Form.png" Width="20px" />
                                            <ClientSideEvents Click="function(s,e){ Clear(); }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table dir="rtl" width="100%">
                    <tr>
                        <td style="vertical-align: top; text-align: right">
                            <asp:Label runat="server" Text="نام" ID="Label7"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: right">
                            <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtName"
                                ClientInstanceName="txtName">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Comment">
                                    <RequiredField IsRequired="True" ErrorText=" نام را وارد نمایید"></RequiredField>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td style="vertical-align: top; text-align: right">
                            <asp:Label runat="server" Text="نام خانوادگی" Width="83px" ID="Label8"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: right">
                            <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtLastName"
                                ClientInstanceName="txtLastName">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Comment">
                                    <RequiredField IsRequired="True" ErrorText=" نام خانوادگی را وارد نمایید"></RequiredField>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right">
                            <asp:Label runat="server" Text="ایمیل" ID="Label9"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: right">
                            <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtEmail"
                                Style="direction: ltr;" ClientInstanceName="txtEmail_PopupIdea">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Comment">
                                    <RequiredField IsRequired="True" ErrorText=" ایمیل را وارد نمایید"></RequiredField>
                                    <RegularExpression ErrorText=" ایمیل را  صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td style="vertical-align: top" align="right">
                            <asp:Label runat="server" Text="امتیاز" ID="Label2"></asp:Label>
                        </td>
                        <td style="vertical-align: top" align="right">
                            <TSPControls:CustomASPXRatingControl ID="Rating3" runat="server" Value="0" ClientInstanceName="Rating3"
                                FillPrecision="Full" RightToLeft="True">
                            </TSPControls:CustomASPXRatingControl>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right">
                            <asp:Label runat="server" Text="متن " ID="Label10"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: right" colspan="3">
                            <TSPControls:CustomASPXMemo runat="server" Height="80px" ID="txtBody"
                                ClientInstanceName="txtBody_PopupIdea">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Comment">
                                    <RequiredField IsRequired="True" ErrorText="متن نظر را وارد نمایید"></RequiredField>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomASPXMemo>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle>
            <Paddings PaddingRight="6px" PaddingLeft="10px" PaddingTop="1px"></Paddings>
        </HeaderStyle>
        <SizeGripImage Height="12px" Width="12px">
        </SizeGripImage>
        <CloseButtonImage Height="17px" Width="17px">
        </CloseButtonImage>
    </TSPControls:CustomASPxPopupControl>
    <%--  <asp:ObjectDataSource ID="OdbOtherAttachments" runat="server" SelectMethod="FindByNewsCodeAndType"
        TypeName="TSP.DataManager.NewsImgManager">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="NewsId" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="Type" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <dx:ASPxHiddenField ID="HiddenNewsData" runat="server" ClientInstanceName="HiddenNewsData">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="HdAgent" runat="server" ClientInstanceName="HdAgent">
    </dx:ASPxHiddenField>
</asp:Content>
<%--<a href="News.aspx">News.aspx</a>--%>
