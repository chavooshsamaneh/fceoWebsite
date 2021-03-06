<%@ Page Title="ثبت آماده به کاری"  Async="true"  Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverWorkRequestInsert.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_ObserverWorkRequestInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%--<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>
<%@ Register Src="~/UserControl/WorkRequestInsertInfoUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestInsertInfoUserControl" %>--%>
<%@ Register Src="~/UserControl/WorkRequestInsertInfoUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestInsertInfoUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr><td>
                                       <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" CausesValidation="false"
                                            OnClick="BtnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="" ToolTip="ذخیره آماده به کاری"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
                       
                        if(HiddenFieldPage.Get('MustHasAttach')==true && HiddenFieldPage.Get('flpAttachValidation')==false )
                        {
                        alert('باید تاییدیه ی شهرداری  یا بنیاد مسکن بارگذاری شود');
                        e.processOnServer= false; 
                        }
                        
                       else if (ASPxClientEdit.ValidateGroup() == false)
                             {
                               e.processOnServer= false;                                       
                             }
                        else {
	                           e.processOnServer= confirm('آیا از ذخیره آماده بکاری مطمئن می باشید؟');
                             }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>

                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="" ToolTip="لیست اعلام آماده به کاری "
                                            ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" CausesValidation="false"
                                            EnableTheming="False" OnClick="btnBack_Click">
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="legendTitle">شناسه عضو</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" colspan="4" align="center">
                                            <b>
                                                <a runat="server" class="HelpUL" text="" font-bold="False" id="lblWarning"></a></b>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="15%" valign="top" align="right">کد عضویت
                                        </td>
                                        <td width="35%" valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMeId" Width="100%" AutoPostBack="True"
                                                OnTextChanged="txtMeId_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomASPxCheckBox ID="CheckBoxSEndInfoToesup" ClientInstanceName="CheckBoxSEndInfoToesup" runat="server" Checked="true" Text="ارسال اطلاعات به شهرداری">
                                                        </TSPControls:CustomASPxCheckBox></td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <TSP:WorkRequestInsertInfoUserControl runat="server" ID="WorkRequestInsertInfoUserControl" />

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>    
        <%--    <br />
            <dxcp:ASPxPanel ID="RoundPanelNoConfilict" runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>

                        <fieldset>
                            <legend class="legendTitle">تایید اطلاعات فرم آماده بکاری</legend>
                            <div class="row">
                                <TSPControls:CustomASPxRadioButtonList ID="CheckBoxNoConfilict" ClientInstanceName="CheckBoxNoConfilict" runat="server">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired="true" ErrorText="یک گزینه را انتخاب نمایید"></RequiredField>
                                    </ValidationSettings>
                                    <Items>
                                        <dxcp:ListEditItem Text="کلیه مشخصات درج شده را بررسی نموده ام و هیچ گونه مغایرتی در اطلاعات مشاهده نکرده ام" Value="0" />
                                        <dxcp:ListEditItem Text="اطلاعات نمایش داده شده دارای مغایرت می باشد" Value="1" />

                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ 
                                        if(CheckBoxNoConfilict.GetValue()==0)
                                        {
                                        if(HiddenFieldPage.Get('HasError')==true){
                                            btnSave.SetEnabled(false);}
                                        else{
                                            btnSave.SetEnabled(true);}
                                            RoundPanelSaveConflict.SetVisible(false);
                                        btnConflict.SetVisible(false);
                                                   
                                        }
                                        else 
                                        {
                                           btnSave.SetEnabled(false);
                                           RoundPanelSaveConflict.SetVisible(true);
                                        btnConflict.SetVisible(true);
                                        }
                                        }" />
                                </TSPControls:CustomASPxRadioButtonList>
                            </div>
                            <div class="row">
                                <dxcp:ASPxPanel ID="RoundPanelSaveConflict" ClientInstanceName="RoundPanelSaveConflict" ClientVisible="false"
                                    runat="server">
                                    <PanelCollection>
                                        <dxcp:PanelContent>
                                            <fieldset runat="server">
                                                <legend class="legendTitle">اعلام مغایرت اطلاعات</legend>
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td width="30%">
                                                                <dxcp:ASPxLabel runat="server" ID="ASPxLabel4" Text="نوع مغایرت">
                                                                </dxcp:ASPxLabel>
                                                            </td>
                                                            <td>

                                                                <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String"
                                                                    TextField="TypeName" ValueField="ConfTypeId"
                                                                    ID="ComboBoxConflictType"
                                                                    ClientInstanceName="ComboBoxConflictType" DataSourceID="ObjectDataSourceConfilict">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationGroupConflict">
                                                                        <RequiredField IsRequired="True" ErrorText="نوع مغایرت را انتخاب نمایید"></RequiredField>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomAspxComboBox>

                                                                <asp:ObjectDataSource runat="server" SelectMethod="SelectConfilictTypes" ID="ObjectDataSourceConfilict"
                                                                    TypeName="TSP.DataManager.ConflictTypeManager">
                                                                    <SelectParameters>
                                                                        <asp:Parameter Type="Int32" Name="TypeCode" DefaultValue="-1"></asp:Parameter>
                                                                        <asp:Parameter Type="Int32" Name="InActive" DefaultValue="0"></asp:Parameter>
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                                <dxcp:ASPxLabel runat="server" ID="ASPxLabel5" Text="توضیحات">
                                                                </dxcp:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxcp:ASPxMemo runat="server" ID="txtConflictDescription" Width="100%">
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationGroupConflict">
                                                                        <RequiredField IsRequired="True" ErrorText="به طور مختصر مغایرت اطلاعات را شرح دهید"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dxcp:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </fieldset>
                                        </dxcp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxPanel>
                            </div>
                        </fieldset>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>--%>
    
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldForm" ClientInstanceName="HiddenFieldForm">
            </dxhf:ASPxHiddenField>
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
                                            ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" CausesValidation="false"
                                            OnClick="BtnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="" ToolTip="ذخیره آماده به کاری"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
                       
                        if(HiddenFieldPage.Get('MustHasAttach')==true && HiddenFieldPage.Get('flpAttachValidation')==false )
                        {
                        alert('باید تاییدیه ی شهرداری  یا بنیاد مسکن بارگذاری شود');
                        e.processOnServer= false; 
                        }
                        
                       else if (ASPxClientEdit.ValidateGroup() == false)
                             {
                               e.processOnServer= false;                                       
                             }
                        else {
	                           e.processOnServer= confirm('آیا از ذخیره آماده بکاری مطمئن می باشید؟');
                             }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>

                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="" ToolTip="لیست اعلام آماده به کاری "
                                            ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" CausesValidation="false"
                                            EnableTheming="False" OnClick="btnBack_Click">
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

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



