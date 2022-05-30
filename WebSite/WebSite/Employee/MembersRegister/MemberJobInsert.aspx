<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberJobInsert.aspx.cs" Inherits="Employee_MembersRegister_MemberJobInsert"
    Title="مشخصات سابقه کاری" %>

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

    <%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function CheckDate() {
            var StartDate = document.getElementById('<%=txtjCoStartDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtjCoEndDate.ClientID%>').value;
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }
    </script> 
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" class="DivErrors" align="right" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="BtnNew_Click">
                                                      
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                      
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                        ValidationGroup="j" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnSave_Click">
                                                       
                                                        <Image  Url="~/Images/icons/save.png">
                                                        </Image>
                                                        <ClientSideEvents Click="function(s, e) {
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		lblDateError.SetVisible(true);
	}
	else
		lblDateError.SetVisible(false);
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                       
                                                        <Image  Url="~/Images/icons/Back.png">
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
                <div style="width: 100%; text-align: right;display:none">
                    <dxe:ASPxLabel ID="lblSex" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblT" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblOfName" runat="server">
                    </dxe:ASPxLabel>
                </div>
                     <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

    
           
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام پروژه *" ID="ASPxLabel9" >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjPrName"  
                                                    ClientInstanceName="TextPrName" >
                                                    <ValidationSettings Display="Dynamic" ErrorText="" ValidationGroup="j" ErrorTextPosition="Bottom">                                               
                                                        <RequiredField IsRequired="True" ErrorText="نام پروژه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام کارفرما *" ID="ASPxLabel11" >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjEmployer"  
                                                    ClientInstanceName="TextEmployer" >
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">                                                  
                                                        <RequiredField IsRequired="True" ErrorText="نام کارفرما را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع پروژه *" ID="ASPxLabel8" >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server"
                                                    IncrementalFilteringMode="StartsWith" TextField="Name" ID="CombojPrType" 
                                                    DataSourceID="OdbPrType" ValueType="System.String" ValueField="PrtId" ClientInstanceName="CmbPrType"
                                                EnableIncrementalFiltering="True">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
   if(CmbPrType.GetValue() == '1')
	{
	TextArea.SetVisible(true);
	TextFloor.SetVisible(true);
	lbl1.SetVisible(true);
	lbl2.SetVisible(true);
	CmbSazeType.SetVisible(true);
	lbl3.SetVisible(true);
	}
	else
	{
	TextArea.SetVisible(false);
	TextFloor.SetVisible(false);
	lbl1.SetVisible(false);
	lbl2.SetVisible(false);
	CmbSazeType.SetVisible(false);
	lbl3.SetVisible(false);
	}
}"></ClientSideEvents>
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">
                                        
                                                        <RequiredField IsRequired="True" ErrorText="نوع پروژه را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع سازه *" ClientVisible="False" ID="ASPxLabel10"
                                                    ClientInstanceName="lbl3" >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" ClientVisible="False"  
                                                     IncrementalFilteringMode="StartsWith" TextField="Name"
                                                    ID="CombojSazeType" DataSourceID="OdbSazeType"
                                                    ValueType="System.String" ValueField="SztId" ClientInstanceName="CmbSazeType"
                                              EnableIncrementalFiltering="True">
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">
                                                       
                                                        <RequiredField IsRequired="True" ErrorText="نوع سازه را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="سمت *" ID="ASPxLabel14">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" 
                                                    IncrementalFilteringMode="StartsWith" TextField="PName" ID="ComboPosition" 
                                                    DataSourceID="OdbJobPosition" ValueType="System.String" ValueField="PJPId" ClientInstanceName="CmbPosition"
                                                   EnableIncrementalFiltering="True">
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">
                                                     
                                                        <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نحوه مشارکت *" ID="ASPxLabel24" >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server"  
                                                    IncrementalFilteringMode="StartsWith" TextField="CorName" ID="CombojIsCorporate"
                                                  DataSourceID="OdbCorType" ValueType="System.String"
                                                    ValueField="CortId" ClientInstanceName="CmbCorporate" 
                                                    EnableIncrementalFiltering="True">
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">
                                                 
                                                        <RequiredField IsRequired="True" ErrorText="نحوه مشارکت را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کشور" ID="ASPxLabel12">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" 
                                                    IncrementalFilteringMode="StartsWith" TextField="CounName" ID="CombojCountry"
                                                    DataSourceID="ODBJobCountry" ValueType="System.String"
                                                    ValueField="CounId" ClientInstanceName="CmbCountry" 
                                                    EnableIncrementalFiltering="True">
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">
                                             
                                                        <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شهر *" ID="ASPxLabel13">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjCity"  ClientInstanceName="TextCity"
                                                    >
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                        ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع پروژه *" ID="ASPxLabel16">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                     ShowPickerOnTop="True" ID="txtjStartDate" PickerDirection="ToRight"
                                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtjStartDate" ValidationGroup="j"
                                                    ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="حجم پروژه" ID="ASPxLabel21">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjPrVolume"  
                                                    ClientInstanceName="TextVolume" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری *" ID="ASPxLabel17">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                     ShowPickerOnTop="True" ID="txtjCoStartDate" PickerDirection="ToRight"
                                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtjCoStartDate" ValidationGroup="j"
                                                    ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری *" ID="ASPxLabel19">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                    ShowPickerOnTop="True" ID="txtjCoEndDate" PickerDirection="ToRight"
                                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtjCoEndDate" ValidationGroup="j"
                                                    ID="PersianDateValidator3">تاریخ نامعتبر</pdc:PersianDateValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                            </td>
                                            <td valign="top" align="right" colspan="2">
                                                <dxe:ASPxLabel runat="server" Text="محدوده تاریخ وارد شده صحیح نمی باشد" ClientVisible="False"
                                                    ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblDateError">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان شروع همکاری" 
                                                    ID="ASPxLabel18">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjStartStatus" 
                                                    ClientInstanceName="TextSStatus" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان پایان همکاری" 
                                                    ID="ASPxLabel20">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjEndStatus"  
                                                    ClientInstanceName="TextEStatus" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="زیربنا *" ClientVisible="False" ID="ASPxLabel22"
                                                    ClientInstanceName="lbl1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjArea"  ClientVisible="False"
                                                    ClientInstanceName="TextArea" >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="زبر بنا را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="مقدار زیربنا را صحیح وارد نمایید" ValidationExpression="\d*">
                                                        </RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تعداد طبقات *" ClientVisible="False" ID="ASPxLabel23"
                                                    ClientInstanceName="lbl2" RightToLeft="True">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtjFloor"  ClientVisible="False"
                                                   ClientInstanceName="TextFloor" >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="تعداد طبقات را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="تعداد طبقات را صحیح وارد نمایید" ValidationExpression="\d*">
                                                        </RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="33px" ID="txtjDesc"                                                      ClientInstanceName="TextDesc" >
                                                </TSPControls:CustomASPXMemo>
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
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="BtnNew_Click">
                                                         
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                        
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ValidationGroup="j" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnSave_Click">
                                                        
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) {
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		lblDateError.SetVisible(true);
	}
	else
		lblDateError.SetVisible(false);
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                      
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
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
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:HiddenField ID="MemberId" runat="server" Visible="False" />
        <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
        <asp:HiddenField ID="JobId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="ODBJobCountry" runat="server" DeleteMethod="Delete" EnableCaching="True"
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SqlCacheDependency="NezamFars:tblCountry"
            SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager" UpdateMethod="Update"
            CacheExpirationPolicy="Sliding">
            
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbPrType" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.ProjectTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbSazeType" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.SazeTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbCorType" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.CorporationTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbJobPosition" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.ProjectJobPositionManager">
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
 
</asp:Content>
