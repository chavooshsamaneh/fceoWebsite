<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddGovManagerName.aspx.cs" Inherits="Employee_Document_AddGovManagerName"
    Title="مشخصات فرد" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click"
                                            UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                            ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                            UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                            <ClientSideEvents Click="
function(s, e) {
if(flpmesign.Get('name')!=1)
{
lble.SetVisible(true);
e.processOnServer=false;
}}" />

                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                            UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="عنوان" ID="ASPxLabel1" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="80%">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.Int32" DataSourceID="ObjectDataSource1"
                                            TextField="GmtNameDetail"  ValueField="GmtId" ID="ComboName"
                                            RightToLeft="True" Width="100%">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="عنوان را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" valign="top" colspan="2">
                                        <TSPControls:CustomASPxCheckBox runat="server" Width="100%" ID="checkboxHaveNmcId" ClientInstanceName="checkboxHaveNmcId" Text="دارای سمت در چارت سازمانی نظام مهندسی ساختمان استان فارس می باشند و از کاربران این سیستم می باشند" >
                                            <ClientSideEvents CheckedChanged="function(s,e){
                                                if(checkboxHaveNmcId.GetChecked())
                                                {cmbNezamChart.SetVisible(true);
                                                txtGmnName.SetVisible(false);
                                                flpSign.SetVisible(false);}
                                                else
                                                {cmbNezamChart.SetVisible(false);
                                                  txtGmnName.SetVisible(true);
                                                flpSign.SetVisible(true);}
                                                }" />
                                           <%-- <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>--%>
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>


                                 <tr>
                                    <td align="right" valign="top" colspan="2">
                                         <TSPControls:CustomAspxComboBox runat="server" Caption="سمت" Width="100%" IncrementalFilteringMode="Contains"
                                                TextField="FullName" ID="cmbNezamChart" ClientInstanceName="cmbNezamChart" DataSourceID="ObjdsNezamChart"
                                                ValueType="System.String" ValueField="NmcId"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                           <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" OldValuesParameterFormatString="original_{0}"
                    TypeName="TSP.DataManager.NezamChartManager" SelectMethod="SelectNezamChartActive"></asp:ObjectDataSource>
                                    </td>
                                </tr>



                                <tr>
                                   <%-- <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نام و نام خانوادگی" ID="Label10" Width="100%"></asp:Label>
                                    </td>--%>
                                    <td align="right" valign="top" colspan="2">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtGmnName" Caption="نام و نام خانوادگی"  ClientInstanceName="txtGmnName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label3" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تصویر امضا" ID="Label42" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSign" InputType="Images" ClientInstanceName="flps" OnFileUploadComplete="flpSign_FileUploadComplete" Width="100%">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	
	imgEndUploadImgClientSign.SetVisible(true);
	 flpmesign.Set('name',1);
	lble.SetVisible(false);
	signImg.SetVisible(true);
	signImg.SetImageUrl('../../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSign.SetVisible(false);
	lble.SetVisible(true);
	signImg.SetVisible(false);
	signImg.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" ClientInstanceName="lble" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر امضا را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRemoveFile" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnRemoveFile_Click" Text=" "
                                                            ToolTip="حذف فایل" UseSubmitBehavior="False">

                                                            <Image Height="16px" Url="~/Images/icons/DeleteFile.png" Width="16px" />
                                                            <ClientSideEvents Click="function(s,e){	 e.processOnServer= confirm('آیا مطمئن به حذف فایل هستید؟');}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage4" ClientInstanceName="imgEndUploadImgClientSign">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="لطفا تصویری با ابعاد حداکثر 113*113 پیکسل انتخاب نمائید"
                                                            ID="lblImg" ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxImage runat="server" Height="110px" Width="100px" ID="ImgSign" ClientInstanceName="signImg"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                            <EmptyImage Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <fieldset id="RoundPanelPrintingAssigner"
                            runat="server">
                            <legend class="HelpUL">اسناد قابل امضا توسط این سمت</legend>
                            <TSPControls:CustomAspxDevGridView ID="GridViewPrintSetting" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="grid"
                                DataSourceID="ObjectDataSourcePrintAssigner" EnableViewState="False" Width="100%"
                                RightToLeft="True" KeyFieldName="PrtsId">
                                <Settings ShowHorizontalScrollBar="true" />
                                <SettingsCookies Enabled="false" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="عنوان سند" Width="350px" FieldName="PrtTypeName"
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" Width="130px" FieldName="CreateDate"
                                        VisibleIndex="1">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <%--      <dxwgv:GridViewDataTextColumn Caption="امضاکننده دوم" Width="130px" FieldName="SecondAssigner"
                                                                    VisibleIndex="2">
                                                                </dxwgv:GridViewDataTextColumn>--%>
                                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" Width="200px" FieldName="Description"
                                        VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="50px" ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                            <asp:ObjectDataSource ID="ObjectDataSourcePrintAssigner" runat="server" SelectMethod="FindGovermentManager"
                                TypeName="TSP.DataManager.PrintAssignerSettingManager" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="-1" Name="GmnId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <asp:HiddenField ID="ManagerId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click"
                                            UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>

                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                            ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="
function(s, e) {
if(flpmesign.Get('name')!=1)
{
lble.SetVisible(true);
e.processOnServer=false;
}}" />

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                            OnClick="btnBack_Click" UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.GovManagerTitleManager"></asp:ObjectDataSource>
    <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpmesign">
    </dxhf:ASPxHiddenField>
</asp:Content>
