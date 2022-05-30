<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddPeriodAttenders.aspx.cs" Inherits="Employee_Amoozesh_AddPeriodAttenders"
    Title="مشخصات شرکت کننده در دوره" %>

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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: left">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Wrap="False" CausesValidation="False" Text="ثبت شخص جدید"
                                                      ID="BtnOtherPerson"
                                                    OnClick="BtnOtherPerson_Click" UseSubmitBehavior="False">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                    cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                    EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                    Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                                    OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                                    ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/save.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                     EnableTheming="False" EnableViewState="False"
                                                                    OnClick="btnDisActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	// e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                    EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/Back.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
 
                                <table style="vertical-align: top; text-align: right" dir="rtl" cellpadding="1">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="نوع شرکت کننده" Width="93px" ID="ASPxLabel17">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="150px" 
                                                    ID="cmbMemberType"  AutoPostBack="True" ValueType="System.String"
                                                     OnSelectedIndexChanged="cmbMemberType_SelectedIndexChanged">
                                                    <Items>
                                                        <dxe:ListEditItem Value="NULL" Text="---------"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="0" Text="افراد متفرقه"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="عضو حقیقی"></dxe:ListEditItem>
                                                    </Items>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نوع را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">&nbsp;<dxe:ASPxLabel runat="server" Text="نوع ثبت نام" Width="66px" ID="lbTypeOfReg">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; width: 153px; text-align: right" dir="ltr">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="150px" 
                                                    ID="cmbTypeOfReg"  ValueType="System.String" 
                                                    OnSelectedIndexChanged="cmbMemberType_SelectedIndexChanged">
                                                    <Items>
                                                        <dxe:ListEditItem Value="NULL" Text="---------"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="0" Text="دوره و آزمون"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="آزمون"></dxe:ListEditItem>
                                                    </Items>
                                                    <ValidationSettings Display="Dynamic">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نوع ثبت نام را انتخاب کنید"></RequiredField>
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
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="رشته" ID="lbMjId" Visible="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr" colspan="3">
                                                <TSPControls:CustomAspxComboBox runat="server" Visible="False" EnableClientSideAPI="True" Width="250px"
                                                     TextField="MjName" ID="cmbMjId" 
                                                    DataSourceID="ODBMajor" ValueType="System.String" ValueField="MjId" AutoResizeWithContainer="True"
                                                    ClientInstanceName="combobox" 
                                                    EnableIncrementalFiltering="True">
                                                    <ValidationSettings Display="Dynamic">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText=" دوره مرتبط را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                                                    SelectMethod="GetData" ID="ODBMajor" UpdateMethod="Update" TypeName="TSP.DataManager.MajorManager">
                                                    <InsertParameters>
                                                        <asp:Parameter Type="String" Name="MjCode"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="MjName"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="ParentId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Description"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="UserId"></asp:Parameter>
                                                        <asp:Parameter Type="DateTime" Name="ModifiedDate"></asp:Parameter>
                                                    </InsertParameters>
                                                    <DeleteParameters>
                                                        <asp:Parameter Type="Int32" Name="Original_MjId"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Original_LastTimeStamp"></asp:Parameter>
                                                    </DeleteParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Type="String" Name="MjCode"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="ParentId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Description"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="UserId"></asp:Parameter>
                                                        <asp:Parameter Type="DateTime" Name="ModifiedDate"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="Original_MjId"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Original_LastTimeStamp"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="MjId"></asp:Parameter>
                                                    </UpdateParameters>
                                                </asp:ObjectDataSource>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="اعضای نظام" ID="lbMeID" Visible="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr" colspan="3">
                                                <TSPControls:CustomAspxComboBox runat="server" Visible="False" EnableSynchronization="False" EnableClientSideAPI="True"
                                                    Width="250px"  TextField="MeName;FatherName;SSN"
                                                    ID="cmbMeId"  ImageUrlField="ImgUrl" DataSourceID="ODBMembers"
                                                    ValueType="System.String" MaxLength="50" EnableCallbackMode="True" ValueField="MeId"
                                                    AutoResizeWithContainer="True" CallbackPageSize="20" ClientInstanceName="combobox"
                                                     EnableIncrementalFiltering="True">
                                                    <ValidationSettings Display="Dynamic">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText=" دوره مرتبط را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Columns>
                                                        <dxe:ListBoxColumn FieldName="MeName" Caption="نام و نام خانوادگی"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="FatherName" Caption="نام پدر"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="SSN" Caption="کد ملی"></dxe:ListBoxColumn>
                                                    </Columns>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource runat="server" DeleteMethod="Delete" EnableCaching="True" InsertMethod="Insert"
                                                    SelectMethod="FindMembersByMjId" ID="ODBMembers" CacheDuration="3600" UpdateMethod="Update"
                                                    TypeName="TSP.DataManager.MemberManager" OldValuesParameterFormatString="original_{0}">
                                                    <InsertParameters>
                                                        <asp:Parameter Type="Int32" Name="MeId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FirstName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="LastName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FirstNameEn"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="LastNameEn"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="TiId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FatherName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirhtDate"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirthPlace"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="IdNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="IssuePlace"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="SSN"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="MobileNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="HomeAdr"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="HomeTel"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="HomePO"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="WorkAdr"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="WorkTel"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="WorkPO"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FaxNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BankAccNo"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="SoId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="MsId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="MrsId"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="SexId"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="MarId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="MeNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FileNo"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="RelId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="ComId"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="AtId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Nationality"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Website"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Email"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="CreateDate"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Description"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Image"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="ImageUrl"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="UserId"></asp:Parameter>
                                                        <asp:Parameter Type="DateTime" Name="ModifiedDate"></asp:Parameter>
                                                    </InsertParameters>
                                                    <SelectParameters>
                                                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                                                    </SelectParameters>
                                                    <DeleteParameters>
                                                        <asp:Parameter Type="Int32" Name="Original_MeId"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Original_LastTimeStamp"></asp:Parameter>
                                                    </DeleteParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Type="Int32" Name="MeId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FirstName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="LastName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FirstNameEn"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="LastNameEn"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="TiId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FatherName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirhtDate"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirthPlace"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="IdNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="IssuePlace"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="SSN"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="MobileNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="HomeAdr"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="HomeTel"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="HomePO"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="WorkAdr"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="WorkTel"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="WorkPO"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FaxNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BankAccNo"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="SoId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="MsId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="MrsId"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="SexId"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="MarId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="MeNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FileNo"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="RelId"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="ComId"></asp:Parameter>
                                                        <asp:Parameter Type="Int16" Name="AtId"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Nationality"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Website"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Email"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="CreateDate"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Description"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Image"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="ImageUrl"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="UserId"></asp:Parameter>
                                                        <asp:Parameter Type="DateTime" Name="ModifiedDate"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="Original_MeId"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Original_LastTimeStamp"></asp:Parameter>
                                                    </UpdateParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="دیگر اشخاص" ID="lbOtpId" Visible="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr" colspan="3">
                                                <TSPControls:CustomAspxComboBox runat="server" Visible="False" EnableSynchronization="False" EnableClientSideAPI="True"
                                                    Width="250px"  TextField="OtpName;FatherName;SSN"
                                                    ID="cmbOtherPerson"  ImageUrlField="ImgUrl" DataSourceID="ODBOterPerson"
                                                    ValueType="System.String" MaxLength="50" EnableCallbackMode="True" ValueField="OtpId"
                                                    AutoResizeWithContainer="True" CallbackPageSize="20" ClientInstanceName="combobox"
                                                     EnableIncrementalFiltering="True">
                                                    <ValidationSettings Display="Dynamic">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="دوره مرتبط را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Columns>
                                                        <dxe:ListBoxColumn FieldName="OtpName" Caption="نام و نام خانوادگی"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="FatherName" Caption="نام پدر"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="SSN" Caption="کد ملی"></dxe:ListBoxColumn>
                                                    </Columns>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource runat="server" FilterExpression="OtpType={0}" DeleteMethod="Delete"
                                                    EnableCaching="True" InsertMethod="Insert" SelectMethod="FindOtherPersonByOtpType"
                                                    ID="ODBOterPerson" CacheDuration="3600" UpdateMethod="Update" TypeName="TSP.DataManager.OtherPersonManager"
                                                    OldValuesParameterFormatString="original_{0}">
                                                    <InsertParameters>
                                                        <asp:Parameter Type="String" Name="OtpCode"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FirstName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="LastName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="IdNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="SSN"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirthPlace"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirthDate"></asp:Parameter>
                                                        <asp:Parameter Type="Byte" Name="OtpType"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Description"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="UserId"></asp:Parameter>
                                                        <asp:Parameter Type="DateTime" Name="ModifiedDate"></asp:Parameter>
                                                    </InsertParameters>
                                                    <SelectParameters>
                                                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="OtpId"></asp:Parameter>
                                                        <asp:Parameter Type="Byte" DefaultValue="2" Name="OtpType"></asp:Parameter>
                                                    </SelectParameters>
                                                    <FilterParameters>
                                                        <asp:Parameter DefaultValue="2" Name="OtpType"></asp:Parameter>
                                                    </FilterParameters>
                                                    <DeleteParameters>
                                                        <asp:Parameter Type="Int32" Name="Original_OtpId"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Original_LastTimeStamp"></asp:Parameter>
                                                    </DeleteParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Type="String" Name="OtpCode"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="FirstName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="LastName"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="IdNo"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="SSN"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirthPlace"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="BirthDate"></asp:Parameter>
                                                        <asp:Parameter Type="Byte" Name="OtpType"></asp:Parameter>
                                                        <asp:Parameter Type="String" Name="Description"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="UserId"></asp:Parameter>
                                                        <asp:Parameter Type="DateTime" Name="ModifiedDate"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="Original_OtpId"></asp:Parameter>
                                                        <asp:Parameter Type="Object" Name="Original_LastTimeStamp"></asp:Parameter>
                                                        <asp:Parameter Type="Int32" Name="OtpId"></asp:Parameter>
                                                    </UpdateParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="71px" Width="380px" ID="txtDesc" 
                                                    >
                                                    <ValidationSettings>
                                                        
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                  </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <asp:HiddenField ID="PeriodAtId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
 
                                <table >
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: left">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Wrap="False" CausesValidation="False" Text="ثبت شخص جدید"
                                                      ID="BtnOtherPerson2"
                                                    OnClick="BtnOtherPerson_Click" UseSubmitBehavior="False">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                    cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                    EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                    Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                                    OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                                    ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/save.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                     EnableTheming="False" EnableViewState="False"
                                                                    OnClick="btnDisActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	// e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                    EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                                                    OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/Back.png">
                                                                    </Image>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
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
