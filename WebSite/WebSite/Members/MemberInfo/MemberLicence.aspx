<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLicence.aspx.cs" Inherits="Members_MemberInfo_MemberLicence"
    Title="مدارک تحصیلی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function __doPostBack(eventTarget, eventArgument) {
            document.forms['form1'].__EVENTTARGET.value=eventTarget;
            document.forms['form1'].__EVENTARGUMENT.value = eventArgument;
            document.forms['form1'].submit();
            
        }
        function SetMajorControlValues(s, e) {
            e.processOnServer = false;
            if (grid.GetFocusedRowIndex() < 0) {
                e.processOnServer = false;
                alert("ردیفی انتخاب نشده است");
            }
            else {
                HiddenFieldPage.Set('Messg', '');
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'LiName;MjName;MjCode', values => {
                    HiddenFieldPage.Set('Messg', "آیا مطمئن به غیر فعال کردن مدرک تحصیلی " + values[0] + " " + values[1] + " با کد پروانه" + values[2] + " می باشید");
                    if (confirm(HiddenFieldPage.Get('Messg'))) {
                        console.log("1");
                        __doPostBack('btnInActive', '');
                        console.log("2");

                    }
                }
                );

            }

        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnInActive_Click" Text=" " ToolTip="غیر فعال"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="SetMajorControlValues" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left" style="width: 100%">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" CssClass="ProjectMainMenuHorizontal"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Member" Text="مشخصات عضو" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Language" Text="زبان ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Activity" Text="فعالیت ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                </Items>
           
            </TSPControls:CustomAspxMenuHorizontal>
            <br>
            <span class="HelpUL">
                <p>
                    <b>توجه: امكان ويرايش اطلاعات مربوط به درخواستهاي تایید شده قبلي وجود ندارد به منظور تغييرات مدرك تحصيلي تاييد شده به صورت زیر عمل کنید
                    </b>
                </p>
                <ul>
                    <li>پس از انتخاب مدرک تحصیلی مورد نظر از طریق آیکون غیر فعال(   
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icons/disactive.png" />
                        ) مدرک تحصیلی قدیمی را حذف کنید </li>
                    <li>سپس از طریق آیکون جدید (   
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/icons/New.png" />) نسبت به ثبت اطلاعات مدرک تحصیلی اقدام نمایید.</li>
                    <li>ورود اطلاعات ستاره دار الزامي مي باشد</li>
                </ul>

            </span>
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%"
                ClientInstanceName="grid"
                AutoGenerateColumns="False" OnPageIndexChanged="CustomAspxDevGridView1_PageIndexChanged"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared">
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <Settings ShowHorizontalScrollBar="true" />

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="0"
                        Width="60px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataImageColumn FieldName="ImageURL" Caption="تصویر" VisibleIndex="0"
                        Width="150px">
                        <EditCellStyle Wrap="False">
                        </EditCellStyle>
                        <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                        </PropertiesImage>
                    </dxwgv:GridViewDataImageColumn>
                    <dxwgv:GridViewDataCheckColumn Caption="پیش فرض" FieldName="DefaultValue" VisibleIndex="0"
                        Width="60px">
                    </dxwgv:GridViewDataCheckColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مقطع" FieldName="LiName" VisibleIndex="1"
                        Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="MjName" VisibleIndex="2"
                        Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjCode" Width="80px" Caption="کد پروانه">
                        <CellStyle Wrap="true" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="دانشگاه" FieldName="UnName" VisibleIndex="3"
                        Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهر" FieldName="CitName" Visible="False" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ فارغ التحصیلی" FieldName="EndDate" VisibleIndex="4">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="معدل" FieldName="Avg" VisibleIndex="5" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="استعلام" FieldName="Inquiry" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="confirm" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                            ToolTip="غیر فعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="SetMajorControlValues" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left" style="width: 100%">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <asp:HiddenField ID="MemberId" runat="server" />

            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ODBMadrak" runat="server" DeleteMethod="Delete" FilterExpression="MeId={0}"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                TypeName="TSP.DataManager.MemberLicenceManager" UpdateMethod="Update">

                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>

            </asp:ObjectDataSource>
            <asp:HiddenField ID="PgMode" runat="server" />
            <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
            <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>


</asp:Content>
