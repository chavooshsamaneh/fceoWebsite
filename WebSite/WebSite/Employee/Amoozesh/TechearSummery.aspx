<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TechearSummery.aspx.cs" Inherits="Employee_Amoozesh_TechearSummery" Title="خلاصه اطلاعات استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server" OnItemClick="ASPxMenu1_ItemClick" Enabled="False">
                <Items>
                    <dxm:MenuItem Name="BasicInfo" Text="مشخصات فردی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق آموزشی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Research" Text="تالیفات و تحقیقات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Atachment" Text="مستندات">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherSummery" HeaderText="خلاصه مشخصات" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table style="vertical-align: top; text-align: right" dir="rtl" cellpadding="1" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; width: 80px; text-align: right">&nbsp;<dxe:ASPxLabel runat="server" Text="شماره مجوز تدریس" Width="100px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 142px; text-align: right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" ClientEnabled="False" ID="txtFileNo" ClientInstanceName="txtFileNoClient">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        <br />
                                    </td>
                                    <td style="vertical-align: top; width: 37px; text-align: right" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ مجوز" ID="ASPxLabel1" Width="100px"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 6px; text-align: right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="145px" Enabled="False" ShowPickerOnTop="True" ID="txtFileDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 80px; height: 89px; text-align: right">&nbsp;<dxe:ASPxLabel runat="server" Text="توضیحات" Width="80px" ID="ASPxLabel15"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; height: 89px; text-align: right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" EnableClientSideAPI="True" Width="95%" ClientEnabled="False" ID="txtDesc" ClientInstanceName="txtDescClient">
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table style="vertical-align: top;" dir="rtl" id="TABLE1" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت عضویت در نظام" Width="118px" ID="ASPxLabel17"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 178px;" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="150px" Enabled="False" ID="cmbMemberType" AutoPostBack="True" ValueType="System.String" Height="21px" OnSelectedIndexChanged="cmbMemberType_SelectedIndexChanged">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {	
          
}"></ClientSideEvents>
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="عضو نظام"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="شخص جدید"></dxe:ListEditItem>
                                            </Items>

                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <br />
                                    </td>
                                    <td style="vertical-align: top; width: 103px;">&nbsp;&nbsp; </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">&nbsp;<dxe:ASPxLabel runat="server" Text="نام و نام خانوادگی" ID="lbNameFamily" ClientInstanceName="lbNameFamilyClient" Visible="False"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 178px; height: 51px;" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" Visible="False" DropDownStyle="DropDown" EnableClientSideAPI="True" Width="150px" Enabled="False" TextField="LastName;FirstName;SSN" ID="cmbName" AutoPostBack="True" DataSourceID="ObjectDataSourceMember" ValueType="System.String" ValueField="MeId" Height="21px" ClientInstanceName="cmbNameClient">
                                            <ClientSideEvents EndCallback="function(s, e) {
	if (cmbNameClient.GetItemCount() &gt; 0)
              cmbNameClient.EnshureShowDropDownArea();
            else
               cmbNameClient.HideDropDown();
}"
                                                KeyUp="function(s, e) {
	 var cbText = cmbNameClient.GetInputElement().value;
            if (!cmbNameClient.InCallback()){
                cmbNameClient.PerformCallback(cbText);
                cmbNameClient.SetText(cbText)
             }
}"
                                                SelectedIndexChanged="function(s, e) {
	//if (!cmbNameClient.InCallback()){
    //      MeIdClient.SetText(cmbNameClient.GetValue());
    // }
}"></ClientSideEvents>

                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="LastName" Caption="نام خانوادگی" Name="LastName"></dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="FirstName" Caption="نام" Name="Name"></dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="SSN" Caption="کد ملی" Name="SSN"></dxe:ListBoxColumn>
                                            </Columns>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        &nbsp;</td>
                                    <td style="vertical-align: top; width: 103px;">&nbsp;<dxe:ASPxLabel runat="server" Text="کد عضویت در سازمان" Width="111px" ID="lbIsMember" ClientInstanceName="lbIsMemberClient" Visible="False"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" AutoPostBack="True" ID="txtMeID" ClientInstanceName="MeIdClient" Visible="False"></TSPControls:CustomAspxButton>
                                        &nbsp; &nbsp; &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="lblName" ClientInstanceName="lblNameClient"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; height: 37px; text-align: right; width: 178px;" dir="ltr">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" ID="txtName" ClientInstanceName="txtNameClient">
                                            <ValidationSettings ErrorText="" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        &nbsp;&nbsp; </td>
                                    <td style="vertical-align: top; width: 103px;">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblFamily" ClientInstanceName="lblFamilyClient"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" ID="txtFamily" ClientInstanceName="txtFamilyClient">
                                            <ValidationSettings ErrorText="" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        &nbsp; </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">&nbsp;<dxe:ASPxLabel runat="server" Text="نام پدر" Width="48px" ID="ASPxLabel7"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 178px;" dir="ltr">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" ID="txtFatherName" ClientInstanceName="txtFatherNameClient"></TSPControls:CustomTextBox>
                                        &nbsp;<br />
                                    </td>
                                    <td style="vertical-align: top; width: 103px;">&nbsp;<dxe:ASPxLabel runat="server" Text="تاریخ تولد" ID="ASPxLabel11"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="145px" Enabled="False" ShowPickerOnTop="True" ID="txtBrithDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">&nbsp;<dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="98px" ID="ASPxLabel8"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; height: 40px; text-align: right; width: 178px;">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" ID="txtIdNo" ClientInstanceName="txtIdNoClient">
                                            <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{0,10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        <br />
                                    </td>
                                    <td style="vertical-align: top; width: 103px;">&nbsp;<dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel12"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" MaxLength="10" ID="txtSSN" ClientInstanceName="txtSSNClient">
                                            <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>

                                            <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">&nbsp;<dxe:ASPxLabel runat="server" Text="عنوان" ID="lblTiId"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 178px; height: 51px;" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" TextField="TiName" ID="cmbTiId" DataSourceID="ODBTitle" ValueType="System.String" ValueField="TiId" ClientInstanceName="cmbTiIdClient">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <br />
                                    </td>
                                    <td style="vertical-align: top; width: 103px;">&nbsp;<dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی" Width="119px" ID="lblLastMajor"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" ID="txtbLastMajor" ClientInstanceName="txtFatherNameClient"></TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی" Width="119px" ID="lblicence"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; height: 51px; text-align: right; width: 178px;" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="150px" Enabled="False" TextField="LiName" ID="cmbLicence" DataSourceID="ODBLicence" ValueType="System.String" ValueField="LiId" Height="21px" ClientInstanceName="cmbLicenceClient" HorizontalAlign="Right">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="لطفاً یک گزینه را انتخاب نمائید."></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <br />
                                    </td>
                                    <td style="vertical-align: top; width: 103px;">
                                        <dxe:ASPxLabel runat="server" Text="رشته" ID="lblMajor"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" لطفا TextField="MjName" ID="cmbMajor" DataSourceID="ODBMajor" ValueType="System.String" ValueField="MjId" Height="21px" ClientInstanceName="cmbMajorClient" HorizontalAlign="Right">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="لطفاً یک گزینه را انتخاب نمائید."></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن" Width="80px" ID="ASPxLabel9"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 178px;">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" MaxLength="12" ID="txtTel" ClientInstanceName="txtTelClient">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)" ValidationExpression="0\d{8,11}"></RegularExpression>

                                                <ErrorFrameStyle Wrap="True"></ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        <br />
                                    </td>
                                    <td style="vertical-align: top; width: 103px;">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100px" ID="ASPxLabel13"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; text-align: right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="150px" Enabled="False" MaxLength="11" ID="txtMobileNo" ClientInstanceName="txtMobileNoClient">
                                            <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 103px;" colspan="3">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="453px" Enabled="False" ID="txtAddress" ClientInstanceName="txtAddressClient"></TSPControls:CustomTextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="120px" ID="ASPxLabel16"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right; width: 103px;" colspan="3">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" EnableClientSideAPI="True" Width="453px" Enabled="False" ID="txtEmail" ClientInstanceName="txtEmailClient">
                                            <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 123px; height: 51px;" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت" ID="ASPxLabel6"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; height: 51px; text-align: right; width: 178px;">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="غیر فعال" EnableClientSideAPI="True" Width="97px" Enabled="False" ID="chbInActive" ClientInstanceName="chbInActiveClient"></TSPControls:CustomASPxCheckBox>
                                    </td>
                                    <td style="vertical-align: top; width: 103px; height: 51px; text-align: right"></td>
                                    <td style="vertical-align: top; width: 180px; height: 51px; text-align: right">&nbsp;</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="100%" EnableCallBacks="False" ID="GridViewTeacherLicence" DataSourceID="ObjdsTeacherLicence" KeyFieldName="MlId" AutoGenerateColumns="False" ClientInstanceName="GridViewTeacherLicence">
                            <ClientSideEvents RowClick="function(s, e) {
	//btn.SetEnabled(false);
	//SetControlValues();
}"></ClientSideEvents>


                            <Columns>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlId" Name="MlId"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LiName" Caption="مدرک" Name="LiName"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MjName" Caption="رشته" Name="MjName"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UnName" Caption="دانشگاه" Name="UnName"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CitName" Caption="شهر" Name="CitName"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Avg" Caption="معدل" Name="Avg"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="StartDate" Caption="تاریخ شروع" Name="StartDate"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="EndDate" Caption="تاریخ پایان" Name="EndDate"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="Description" Caption="توضیحات" Name="Description"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="9" Width="1px"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Grade" VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="MjId" Name="MjId"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="10" FieldName="LiId" Name="LiId" Visible="False"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="UnId" Name="UnId"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="NumUnit" Name="NumUnit"></dxwgv:GridViewDataTextColumn>
                            </Columns>


                        </TSPControls:CustomAspxDevGridView>
                        <br />
                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="100%" ID="GridViewTeacherJob" DataSourceID="ObjdsTeacherJobHistory" AutoGenerateColumns="False" ClientInstanceName="GridViewTeacherJob">

                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TJobPlace" Caption="نام موسسه"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="CitId" Caption="شهر" VisibleIndex="2">
                                    <PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TJobName" Caption="نوع فعالیت آموزشی"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StartDate" Caption="تاریخ شروع"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="1px"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="EndDate" Caption="تاریخ پایان"></dxwgv:GridViewDataTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView>
                        <br />
                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="100%" ID="GridViewTeacherResearch" KeyFieldName="TResearchId" AutoGenerateColumns="False" ClientInstanceName="GridViewTeacherResearch">

                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام مقاله/فعالیت آموزشی">
                                    <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="RaId" Caption="نوع مقاله/فعالیت آموزشی" Name="ResearchType" VisibleIndex="2">
                                    <PropertiesComboBox Width="100px" ValueType="System.String" TextField="RaName" ValueField="RaId" DataSourceID="ObjdsResearchActivity"></PropertiesComboBox>
                                    <EditItemTemplate>
                                        <div style="width: 100px; height: 23px" dir="ltr">
                                            <TSPControls:CustomAspxComboBox ID="cmbResearchActivity" runat="server" Text='<%# Bind("RaName") %>' DataSourceID="ObjdsResearchActivity" TextField="RaName" ValueField="RaId" ValueType="System.String" __designer:wfdid="w43">
                                                <ButtonStyle Width="13px"></ButtonStyle>

                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </div>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ResearchDate" Caption="تاریخ مقاله/فعالیت آموزشی" Name="ResearchDate">
                                    <EditItemTemplate>
                                        <pdc:PersianDateTextBox ID="txtResearchDate" runat="server" Width="145px" Text='<%#Bind("ResearchDate") %>' IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" DefaultDate="" __designer:wfdid="w44"></pdc:PersianDateTextBox>
                                    </EditItemTemplate>

                                    <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataMemoColumn VisibleIndex="4" FieldName="Description" Width="150px" Caption="توضیحات">
                                    <PropertiesMemoEdit Width="150px"></PropertiesMemoEdit>
                                </dxwgv:GridViewDataMemoColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="1px">
                                    <EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Grade" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView>
                        <br />
                        </div>
                <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="100%" ID="GridViewAttachment" KeyFieldName="AttachId" AutoGenerateColumns="False" ClientInstanceName="GridViewAttachment">

                    <Columns>
                        <dxwgv:GridViewDataImageColumn Visible="False" VisibleIndex="0" FieldName="FilePath" Caption="مستندات">
                            <PropertiesImage ImageWidth="24px" ImageHeight="24px"></PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل" Name="FilePath">
                            <DataItemTemplate>
                                <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" __designer:wfdid="w102" OnDataBinding="ASPxHyperLink1_DataBinding" NavigateUrl='<%# Bind("FilePath") %>' Target="_blank"></dxe:ASPxHyperLink>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="1px"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>


                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />

            <asp:ObjectDataSource ID="ObjdsTeacherLicence" runat="server" TypeName="TSP.DataManager.TeacherLicenceManger" SelectMethod="SelectByTeacherId">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TeacherId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsTeacherJobHistory" runat="server" TypeName="TSP.DataManager.TeacherJobHistoryManager" SelectMethod="SelectByTeacherId">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TeacherId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsResearchActivity" runat="server" TypeName="TSP.DataManager.ResearchActivityManager" SelectMethod="GetData" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsTeacherResearch" runat="server" TypeName="TSP.DataManager.TeacherResearchActivityManager" SelectMethod="SelectByTeacher">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TecherId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberResearchActivity" runat="server" TypeName="TSP.DataManager.MemberResearchActivityManager" SelectMethod="SelectByMember" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete">

                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                </SelectParameters>

            </asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HiddenFieldTeacher" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ODBMajor" runat="server" TypeName="TSP.DataManager.MajorManager" SelectMethod="GetData" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBLicence" runat="server" TypeName="TSP.DataManager.LicenceManager" SelectMethod="GetData" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBTitle" runat="server" TypeName="TSP.DataManager.TitleManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbFamily" runat="server" TypeName="TSP.DataManager.MemberManager" SelectMethod="SelectMemberByName" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" CacheDuration="30">

                <SelectParameters>
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                    <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
                </SelectParameters>

            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbLastName" runat="server" TypeName="TSP.DataManager.MemberManager" SelectMethod="SelectMemberByName" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" CacheDuration="30" EnableCaching="True">

                <SelectParameters>
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                    <asp:Parameter DefaultValue="" Name="LastName" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceMember" runat="server" TypeName="TSP.DataManager.MemberManager" SelectMethod="SelectMemberByNameCode" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" CacheDuration="30" EnableCaching="True">

                <SelectParameters>
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                    <asp:Parameter DefaultValue="" Name="LastName" Type="String" />
                    <asp:Parameter Name="Code" Type="Int32" />
                </SelectParameters>

            </asp:ObjectDataSource>
            <asp:HiddenField ID="LastMjId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="LastLiId" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" TypeName="TSP.DataManager.MemberLicenceManager" SelectMethod="SelectByMemberId" UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}">

                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MemberId" Type="Int32" />
                </SelectParameters>

            </asp:ObjectDataSource>
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

