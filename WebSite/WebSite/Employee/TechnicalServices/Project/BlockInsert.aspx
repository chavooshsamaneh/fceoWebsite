<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="BlockInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_BlockInsert"
    Title="مشخصات بلوک" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" style="display: block" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                    visible="false">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0" align="right">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSave_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                                ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                            </TSPControls:MenuSeprator>
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
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
                <br />
                <table align="center" width="100%">
                    <tr>
                     
                        <td  align="right"> 
                           <span style="float:right"> <blink id="bkImgWarningMsg">
                              <dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="false" Width="30px" Height="30px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                </dxe:ASPxImage></blink></span>
                            <span style="float:right">
                            <ul class="HelpUL">
                                <span runat="server" visible="false" id="liCountIngrediant">
                                    <li><b>به دلیل ثبت عوامل پروژه در این درخواست قادر به ویرایش زیر بنای کل و تعداد طبقات پروژه نیستید</b></li>
                                </span>
                                <span>
                                    <li>
                                        <dxe:ASPxLabel runat="server" Visible="false" ID="lblWarning" Text="توجه : تعداد طبقات در محاسبه هزینه ها و فیش ها تاثیر دارد"></dxe:ASPxLabel>
                                    </li>
                                </span>
                            </ul>
                                </span>
                        </td>
                    </tr>
                </table>
                <br />
              
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelContent" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="زیر بنای بلوک(متر مربع)" ID="Label24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxFoundation" Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً زیر بنا را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="تعداد طبقات از روی شالوده" ID="Label25">
                                            </dxe:ASPxLabel>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxStageNum" Width="100%">
                                                    <MaskSettings Mask="&lt;0..1000&gt;"></MaskSettings>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="لطفاً تعداد طبقات  را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="سیستم سازه ای" ID="Label29">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Title" ID="ASPxComboBoxStructureSystem" DataSourceID="ObjectdatasourceStructureSystem"
                                                ValueType="System.Int32" ValueField="StructureSystemId"
                                                EnableIncrementalFiltering="True" OnSelectedIndexChanged="ASPxComboBoxStructureSystem_SelectedIndexChanged"
                                                ClientInstanceName="ComboStructureSystem" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً سیستم سازه ای را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if (ComboStructureSystem.GetText()  == &quot;سایر موارد&quot;)
            {
            TextBoxOtherStructureSystem.SetEnabled(true);            
            lblOtherStructureSystem.SetEnabled(true);
            }
        else
        {
            TextBoxOtherStructureSystem.SetEnabled(false);
            TextBoxOtherStructureSystem.SetText(&quot;&quot;);
            lblOtherStructureSystem.SetEnabled(false);
        }
}" />
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="سایر موارد" ID="lblOtherStructureSystem" ClientInstanceName="lblOtherStructureSystem"
                                                ClientEnabled="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxOtherStructureSystem"
                                                Width="100%" ClientEnabled="False"
                                                ClientInstanceName="TextBoxOtherStructureSystem">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%--<RequiredField IsRequired="True" ErrorText="لطفاً نام ببرید"></RequiredField>--%>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع اسکلت" ID="Label31">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Title" ID="ASPxComboBoxStructureSkeleton" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                EnableIncrementalFiltering="True" OnSelectedIndexChanged="ASPxComboBoxStructureSkeleton_SelectedIndexChanged"
                                                ClientInstanceName="ComboBoxStructureSkeleton" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً نوع اسکلت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if (ComboBoxStructureSkeleton.GetText() == &quot;سایر موارد&quot;)
    {
            TextBoxOtherStructureSkeleton.SetEnabled(true);            
            lblOtherStructureSkeleton.SetEnabled(true);
            }
        else
        {
            TextBoxOtherStructureSkeleton.SetEnabled(false);
            TextBoxOtherStructureSkeleton.SetText(&quot;&quot;);
            lblOtherStructureSkeleton.SetEnabled(false);
        }
}" />
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="سایر موارد" ID="lblOtherStructureSkeleton" ClientInstanceName="lblOtherStructureSkeleton"
                                                ClientEnabled="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxOtherStructureSkeleton"
                                                Width="100%" ClientEnabled="False"
                                                ClientInstanceName="TextBoxOtherStructureSkeleton">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%--<RequiredField IsRequired="True" ErrorText="لطفاً نام ببرید"></RequiredField>--%>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نوع سقف" ID="Label33"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Title" ID="ASPxComboBoxRoofType" DataSourceID="ObjectdatasourceRoofType"
                                                ValueType="System.Int32" ValueField="RoofTypeId"
                                                EnableIncrementalFiltering="True" OnSelectedIndexChanged="ASPxComboBoxRoofType_SelectedIndexChanged"
                                                ClientInstanceName="ComboBoxRoofType" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً نوع سقف را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if (ComboBoxRoofType.GetText() == &quot;سایر موارد&quot;)
    {
            TextBoxOtherRoofType.SetEnabled(true);
            lblOtherRoofType.SetEnabled(true);
            }
        else
        {
            TextBoxOtherRoofType.SetEnabled(false);
            TextBoxOtherRoofType.SetText(&quot;&quot;);
            lblOtherRoofType.SetEnabled(false);
        }
}" />
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="سایر موارد" ID="lblOtherRoofType" ClientInstanceName="lblOtherRoofType"
                                                ClientEnabled="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxOtherRoofType"
                                                Width="100%" ClientEnabled="False"
                                                ClientInstanceName="TextBoxOtherRoofType">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%--<RequiredField IsRequired="True" ErrorText="لطفاً نام ببرید"></RequiredField>--%>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" valign="top" width="100%">
                                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelWall" HeaderText="مشخصات دیوارها"
                                                runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent>
                                                        <table dir="rtl" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top" align="right" width="15%">
                                                                        <asp:Label runat="server" Text="جهت" ID="Label14" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td dir="ltr" valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            TextField="Title" ID="ASPxComboBoxWallMainDirections" DataSourceID="ObjectdatasourceMainDirections"
                                                                            ValueType="System.Int32" ValueField="MainDirectionsId" ClientInstanceName="ComboBoxWallMainDirections"
                                                                            RightToLeft="True" EnableIncrementalFiltering="True">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="WG" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً جهت را انتخاب نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                    <td valign="top" align="right" width="15%"></td>
                                                                    <td dir="ltr" valign="top" align="right" width="35%"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="طول (متر)" ID="Label5" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxWallLength" Width="100%"
                                                                            ClientInstanceName="TextBoxWallLength">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="WG" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً طول دیوار را وارد نمایید"></RequiredField>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="ارتفاع (متر)" ID="Label6" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxWallHeight" Width="100%"
                                                                            ClientInstanceName="TextBoxWallHeight">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="WG" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً ارتفاع را وارد نمایید"></RequiredField>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="اضافه به لیست" ValidationGroup="WG"
                                                                            ID="btnAddWall" UseSubmitBehavior="False" TabIndex="8"
                                                                            OnClick="btnAddWall_Click">
                                                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td valign="top" align="right" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" CausesValidation="False"
                                                                            ID="btnCancelWall" AutoPostBack="False" UseSubmitBehavior="False" TabIndex="9">
                                                                            <ClientSideEvents Click="function(s, e) {
	ComboBoxWallMainDirections.SetSelectedIndex(-1);
	TextBoxWallLength.SetText('');
	TextBoxWallHeight.SetText('');	
}"></ClientSideEvents>
                                                                            <Image Height="16px" Width="16px" Url="~/Images/icons/Clear-Form.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" colspan="4">
                                                                        <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                                                                            ID="CustomAspxDevGridViewWall" KeyFieldName="WallsId" AutoGenerateColumns="False"
                                                                            __designer:wfdid="w9" OnRowDeleting="CustomAspxDevGridViewWall_RowDeleting"
                                                                            OnRowUpdating="CustomAspxDevGridViewWall_RowUpdating">
                                                                            <Columns>
                                                                                <%--  <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="EntranceId"
                                                                                    Caption="EntranceId" Name="EntranceId">
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="BlockId"
                                                                                    Caption="BlockId" Name="BlockId">
                                                                                </dxwgv:GridViewDataTextColumn>--%>
                                                                                <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption=" " Name="clnDelete" ShowDeleteButton="true" ShowEditButton="true">
                                                                                </dxwgv:GridViewCommandColumn>
                                                                                <dxwgv:GridViewDataComboBoxColumn FieldName="MainDirectionsId" Caption="جهت" VisibleIndex="0">
                                                                                    <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceMainDirections"
                                                                                        ValueField="MainDirectionsId">
                                                                                    </PropertiesComboBox>
                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Length" Caption="طول (متر)"
                                                                                    Name="Length">
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Height" Caption="ارتفاع (متر)"
                                                                                    Name="Height">
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                            </Columns>
                                                                        </TSPControls:CustomAspxDevGridView>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </TSPControls:CustomASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" valign="top" width="100%">
                                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelEntrance" HeaderText="مشخصات درب ها"
                                                runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent>
                                                        <table dir="rtl" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top" align="right" width="15%">
                                                                        <asp:Label runat="server" Text="نوع درب" ID="Label11" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            TextField="Title" ID="ASPxComboBoxEntranceType" DataSourceID="ObjectdatasourceEntranceType"
                                                                            ValueType="System.Int32" ValueField="EntranceTypeId" ClientInstanceName="ComboBoxEntranceType"
                                                                            EnableIncrementalFiltering="True"
                                                                            RightToLeft="True">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="EG" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً نوع درب را انتخاب نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                    <td valign="top" align="right" width="15%">
                                                                        <asp:Label runat="server" Text="جهت" ID="Label20" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td dir="ltr" valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            TextField="Title" ID="ASPxComboBoxMainDirections" DataSourceID="ObjectdatasourceMainDirections"
                                                                            ValueType="System.Int32" ValueField="MainDirectionsId" ClientInstanceName="ComboBoxMainDirections"
                                                                            RightToLeft="True" EnableIncrementalFiltering="True">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="EG" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً جهت را انتخاب نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="تعداد" ID="Label12" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxNum" Width="100%"
                                                                            ClientInstanceName="TextBoxNum">
                                                                            <MaskSettings Mask="&lt;1..100&gt;"></MaskSettings>
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="EG" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً تعداد را وارد نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right"></td>
                                                                    <td valign="top" align="right"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="اضافه به لیست" ValidationGroup="EG"
                                                                            ID="btnAddEntrance" UseSubmitBehavior="False" TabIndex="8"
                                                                            OnClick="btnAddEntrance_Click">
                                                                            <Image Height="16px" Width="16px" Url="~/Images/AddToList.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td dir="ltr" valign="top" align="right" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" CausesValidation="False"
                                                                            ID="btnCancelEntrance" AutoPostBack="False" UseSubmitBehavior="False" TabIndex="9">
                                                                            <ClientSideEvents Click="function(s, e) {
	ComboBoxEntranceType.SetSelectedIndex(-1);
	ComboBoxMainDirections.SetSelectedIndex(-1);
	TextBoxNum.SetText('');	
}"></ClientSideEvents>
                                                                            <Image Height="16px" Width="16px" Url="~/Images/icons/Clear-Form.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" colspan="4">
                                                                        <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                                                                            ID="CustomAspxDevGridViewEntrance" KeyFieldName="EntranceId" AutoGenerateColumns="False"
                                                                            OnRowDeleting="CustomAspxDevGridViewEntrance_RowDeleting"
                                                                            OnRowUpdating="CustomAspxDevGridViewEntrance_RowUpdating">
                                                                            <SettingsCookies Enabled="false" />
                                                                            <Columns>
                                                                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Name="clnDelete" ShowDeleteButton="true" ShowEditButton="true">
                                                                                </dxwgv:GridViewCommandColumn>
                                                                                <%-- <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="EntranceId"
                                                                                    Caption="EntranceId" Name="EntranceId">
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="BlockId"
                                                                                    Caption="BlockId" Name="BlockId">
                                                                                </dxwgv:GridViewDataTextColumn>--%>
                                                                                <dxwgv:GridViewDataComboBoxColumn FieldName="EntranceTypeId" Caption="نوع" VisibleIndex="0">
                                                                                    <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceEntranceType"
                                                                                        ValueField="EntranceTypeId">
                                                                                    </PropertiesComboBox>
                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                <dxwgv:GridViewDataComboBoxColumn FieldName="MainDirectionsId" Caption="جهت" VisibleIndex="1">
                                                                                    <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceMainDirections"
                                                                                        ValueField="MainDirectionsId">
                                                                                    </PropertiesComboBox>
                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Num" Caption="تعداد" Name="Num">
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                            </Columns>
                                                                        </TSPControls:CustomAspxDevGridView>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </TSPControls:CustomASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" valign="top" width="100%">
                                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelFoundation" HeaderText="مشخصات طبقات"
                                                runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent>
                                                        <table dir="rtl" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="4" align="center" width="100%">
                                                                        <asp:Label runat="server" Text="مجموع مساحت های کلیه طبقات بایستی برابر زیر بنای بلوک باشد"
                                                                            ID="Label2" Font-Bold="true" ForeColor="DarkRed" Width="100%"></asp:Label>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right" width="15%">
                                                                        <asp:Label runat="server" Text="عنوان طبقه" ID="Label55"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxStageTitle" Width="100%"
                                                                            ClientInstanceName="TextBoxStageTitle">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً عنوان طبقه را وارد نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right" width="15%"></td>
                                                                    <td valign="top" align="right" width="35%"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="تعداد واحد" ID="Label60"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxFlat" Width="100%"
                                                                            ClientInstanceName="TextBoxFlat">
                                                                            <MaskSettings Mask="&lt;1..100&gt;"></MaskSettings>
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً تعداد واحد را وارد نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="سطح اشغال (%)" ID="Label56"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxEshghalSurface"
                                                                            Width="100%" ClientInstanceName="TextBoxEshghalSurface">
                                                                            <MaskSettings Mask="&lt;0..100&gt;"></MaskSettings>
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً سطح اشغال را وارد نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="مساحت (متر مربع)" ID="Label57"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxFoundationArea"
                                                                            Width="100%" ClientInstanceName="TextBoxFoundationArea">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً مساحت را وارد نمایید"></RequiredField>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="ارتفاع (متر)" ID="Label58"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxHeight" Width="100%"
                                                                            ClientInstanceName="TextBoxHeight">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً ارتفاع را وارد نمایید"></RequiredField>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="کاربری" ID="Label59"></asp:Label>
                                                                    </td>
                                                                    <td dir="ltr" valign="top" align="right">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            TextField="Title" ID="ASPxComboBoxFoundationUsage" DataSourceID="ObjectdatasourceUsage"
                                                                            ValueType="System.Int32" ValueField="UsageId" ClientInstanceName="ComboBoxFoundationUsage"
                                                                            RightToLeft="True" EnableIncrementalFiltering="True">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً کاربری را انتخاب نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="کاربری فرعی" ID="Label1"></asp:Label>
                                                                    </td>
                                                                    <td dir="ltr" valign="top" align="right">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            TextField="Title" ID="ASPxComboBoxSecondaryUsage" DataSourceID="ObjectdatasourceSecondaryUsage"
                                                                            ValueType="System.Int32" ValueField="SecondaryUsageId" ClientInstanceName="ComboBoxFoundationUsage"
                                                                            RightToLeft="True" EnableIncrementalFiltering="True">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="لطفاً کاربری را انتخاب نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="بالکن روباز" ID="ASPxLabel2" Style="direction: rtl">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxOpenYard" Width="100%"
                                                                            ClientInstanceName="TextBoxOpenYard">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <%-- <RequiredField IsRequired="True" ErrorText="لطفاً فیلد را وارد نمایید"></RequiredField>--%>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                        <dxe:ASPxLabel runat="server" Text="حیاط حداکثر 1.20" ID="ASPxLabel1" Style="direction: rtl"
                                                                            ForeColor="DarkRed">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="بالکن روباز" ID="Label64"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxOpenPathway" Width="100%"
                                                                            ClientInstanceName="TextBoxOpenPathway">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <%-- <RequiredField IsRequired="True" ErrorText="لطفاً فیلد را وارد نمایید"></RequiredField>--%>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                        <asp:Label runat="server" Text="معبر طبق مجوز اداره برق" ID="Label3" ForeColor="DarkRed"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="بالکن روبسته" ID="ASPxLabel4" Style="direction: rtl">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxCloseYard" Width="100%"
                                                                            ClientInstanceName="TextBoxCloseYard">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <%-- <RequiredField IsRequired="True" ErrorText="لطفاً فیلد را وارد نمایید"></RequiredField>--%>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                        <dxe:ASPxLabel runat="server" Text="حیاط حداکثر 1.20" ID="ASPxLabel3" Style="direction: rtl"
                                                                            ForeColor="DarkRed">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <asp:Label runat="server" Text="بالکن روبسته" ID="Label65"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxClosePathway" Width="100%"
                                                                            ClientInstanceName="TextBoxClosePathway">
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="G" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <%-- <RequiredField IsRequired="True" ErrorText="لطفاً فیلد را وارد نمایید"></RequiredField>--%>
                                                                                <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                        <asp:Label runat="server" Text="معبر طبق مجوز اداره برق" ID="Label4" ForeColor="DarkRed"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="اضافه به لیست" ValidationGroup="G"
                                                                            ID="btnAdd" UseSubmitBehavior="False" TabIndex="8"
                                                                            OnClick="btnAdd_Click">
                                                                            <Image Height="16px" Width="16px" Url="~/Images/AddToList.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td valign="top" align="right" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" CausesValidation="False"
                                                                            ID="btnCancel" AutoPostBack="False" UseSubmitBehavior="False" TabIndex="9">
                                                                            <Image Height="16px" Width="16px" Url="~/Images/icons/Clear-Form.png">
                                                                            </Image>
                                                                            <ClientSideEvents Click="function(s, e) {
	TextBoxStageTitle.SetText('');
	TextBoxEshghalSurface.SetText('');
	TextBoxFoundationArea.SetText('');
	TextBoxHeight.SetText('');
	TextBoxFlat.SetText('');
	ComboBoxFoundationUsage.SetSelectedIndex(-1);
	TextBoxOpenYard.SetText('');
	TextBoxOpenPathway.SetText('');
	TextBoxCloseYard.SetText('');
	TextBoxClosePathway.SetText('');
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <tr>
                                                                        <td valign="middle" align="center" colspan="4">
                                                                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                                                                                ID="CustomAspxDevGridView1" KeyFieldName="FoundationId" AutoGenerateColumns="False"
                                                                                OnRowDeleting="CustomAspxDevGridView1_RowDeleting"
                                                                                OnRowUpdating="CustomAspxDevGridView1_RowUpdating">
                                                                                <Settings ShowHorizontalScrollBar="true"></Settings>
                                                                                <SettingsCookies Enabled="false" />
                                                                                <Columns>
                                                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Name="clnEdite" ShowDeleteButton="true" ShowEditButton="true">
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <%--                                                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="FoundationId"
                                                                                        Caption="FoundationId" Name="FoundationId">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ProjectId"
                                                                                        Caption="ProjectId" Name="ProjectId">
                                                                                    </dxwgv:GridViewDataTextColumn>--%>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="StageTitle"
                                                                                        Caption="عنوان طبقه" Name="StageTitle">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="120px" FieldName="EshghalSurface"
                                                                                        Caption="سطح اشغال (%)" Name="EshghalSurface">
                                                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="140px" FieldName="Area" Caption="مساحت (مترمربع)"
                                                                                        Name="Area">
                                                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="100px" FieldName="Height" Caption="ارتفاع (متر)"
                                                                                        Name="Height">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="100px" FieldName="Flat" Caption="تعداد واحد"
                                                                                        Name="Flat">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="UsageId" Width="100px" Caption="کاربری"
                                                                                        VisibleIndex="5">
                                                                                        <PropertiesComboBox ValueType="System.Int32" TextField="Title" DataSourceID="ObjectdatasourceUsage"
                                                                                            ValueField="UsageId">
                                                                                        </PropertiesComboBox>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="SecondaryUsageId" Width="120px" Caption="کاربری فرعی"
                                                                                        VisibleIndex="6">
                                                                                        <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceSecondaryUsage"
                                                                                            ValueField="SecondaryUsageId">
                                                                                        </PropertiesComboBox>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CloseYard" Width="120px"
                                                                                        Caption="بالکن بسته حیاط" Name="Yard">
                                                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="ClosePathway" Width="120px"
                                                                                        Caption="بالکن بسته معبر" Name="Pathway">
                                                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="OpenYard" Width="120px"
                                                                                        Caption="بالکن باز حیاط" Name="Yard">
                                                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="10" FieldName="OpenPathway" Width="120px"
                                                                                        Caption="بالکن باز معبر" Name="Pathway">
                                                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                </Columns>
                                                                            </TSPControls:CustomAspxDevGridView>
                                                                        </td>
                                                                    </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </TSPControls:CustomASPxRoundPanel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0" align="right">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSave_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                                ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
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
                <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="mygrid" SessionName="SendBackDataTable_EmpAccPrj"
                    OnCallback="CallbackPanelWorkFlow_Callback" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                    <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectdatasourceUsage" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.UsageManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectdatasourceSecondaryUsage" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.SecondaryUsageManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectdatasourceStructureSystem" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.StructureSystemManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectdatasourceStructureSkeleton" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.StructureSkeletonManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectdatasourceRoofType" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.RoofTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectdatasourceEntranceType" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.EntranceTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectdatasourceMainDirections" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TechnicalServices.MainDirectionsManager"></asp:ObjectDataSource>
        <asp:HiddenField ID="PkProjectId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <asp:HiddenField ID="PkPrjReId" runat="server" Visible="False" />
        <asp:HiddenField ID="PkBlockId" runat="server" Visible="False" />
        <asp:HiddenField ID="MPgMode" runat="server" Visible="False" />
    </div>
</asp:Content>
