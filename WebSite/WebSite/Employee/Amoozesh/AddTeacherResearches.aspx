<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddMemberResearchActivity.aspx.cs" Inherits="Employee_Amoozesh_AddMemberResearchActivity"
    Title="مشخصات تالیفات و تحقیقات عضو" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%" align="center" dir="ltr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS" WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS" FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS" CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
                </pdc:PersianDateScriptManager>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
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
                    <br />
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelResearch" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                   
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top" dir="rtl" align="center" colspan="4">
                                                            <dxe:ASPxLabel runat="server" Text="وضعیت جریان کار" ID="lblWorkFlowState" ForeColor="Red"></dxe:ASPxLabel>











                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td dir="rtl" valign="top" align="right" colspan="3">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeNo"  AutoPostBack="True" Width="148px" ClientInstanceName="TextName"  OnTextChanged="txtMeNo_TextChanged">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>

                                                                    <RegularExpression ErrorText="کد عضویت عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>











                                                        </td>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <asp:Label runat="server" Text="کد عضویت *" Width="83px" ID="Label2"></asp:Label>











                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFamily"  ReadOnly="True" Width="148px" ClientInstanceName="TextName" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <RequiredField ErrorText="نام را وارد نمایید"></RequiredField>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                             </TSPControls:CustomTextBox>











                                                        </td>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="نام خانوادگی" Width="81px" ID="Label5"></asp:Label>











                                                        </td>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName"  ReadOnly="True" Width="148px" ClientInstanceName="TextName" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>











                                                        </td>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="نام" ID="Label4"></asp:Label>











                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                      
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                                <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4" HeaderText="تالیف/فعالیت آموزشی" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

  
                                
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtResearch"  Width="242px" ClientInstanceName="TextName" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                             </TSPControls:CustomTextBox>











                                                        </td>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <asp:Label runat="server" Text="نام مقاله/ فعالیت آموزشی *" Width="147px" ID="Label50"></asp:Label>











                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox runat="server"  Width="242px" TextField="RaName" ID="cmbResearch"  DataSourceID="ODBResearch" ValueType="System.String" ValueField="RaId" ClientInstanceName="ComboType" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Width="14px" Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <RequiredField IsRequired="True" ErrorText="نوع را انتخاب نمایید"></RequiredField>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>

                                                                <ButtonStyle Width="13px"></ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>











                                                        </td>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="نوع مقاله/ فعالیت آموزشی" Width="137px" ID="Label48"></asp:Label>











                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtResearchDesc"  Width="500px" ClientInstanceName="TextDesc" ></TSPControls:CustomASPXMemo>











                                                        </td>
                                                        <td dir="ltr" valign="top" align="right">
                                                            <asp:Label runat="server" Text="توضیحات" ID="Label53"></asp:Label>











                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                             </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                                <br />

                                <dx:ASPxCallbackPanel runat="server"  Width="100%" ID="CallbackPanelReq" ClientInstanceName="CallbackPanelReq" OnCallback="CallbackPanelReq_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            	<TSPControls:CustomASPxRoundPanel ID="RoundPanelJudgment" HeaderText="نظر کارشناسی" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>


                                           
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr id="Tr1" runat="server" visible="False">
                                                                        <td id="TD1" runat="server" style="width: 86px" valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="شماره نامه" ID="ASPxLabel11" ClientInstanceName="lblPr"></dxe:ASPxLabel>


                                                                        </td>
                                                                        <td id="TD2" runat="server" valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMailNo"  Width="170px" ClientInstanceName="txtMailNo"  Style="direction: ltr">
                                                                                <ValidationSettings Display="Dynamic" ValidationGroup="ValidateDuplicate" ErrorTextPosition="Bottom">
                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                                    <RequiredField IsRequired="True" ErrorText="شماره نامه را وارد نمایید"></RequiredField>

                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>

                                                                                <ClientSideEvents TextChanged="function(s, e) {
	CallbackPanelReq.PerformCallback('CheckLetter'+';'+txtMailNo.GetText());
}"></ClientSideEvents>
                                                                            </TSPControls:CustomTextBox>


                                                                            <dxe:ASPxLabel runat="server" Text="شماره نامه معتبر نمی باشد" ClientVisible="False" ID="lblErrorMail" ForeColor="Red" BackColor="White"></dxe:ASPxLabel>


                                                                        </td>
                                                                        <td id="TD3" runat="server" style="width: 86px" dir="ltr" valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="تاریخ نامه" ID="ASPxLabel13" ClientInstanceName="lblDate"></dxe:ASPxLabel>


                                                                        </td>
                                                                        <td id="TD4" runat="server" dir="rtl" valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMailDate"  ReadOnly="True" Width="170px" >
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomTextBox>


                                                                        </td>
                                                                    </tr>


                                                                    <tr id="Tr2" runat="server" visible="False">
                                                                        <td id="TD5" runat="server" style="width: 86px" valign="top" align="right">موضوع نامه</td>
                                                                        <td id="TD6" runat="server" dir="rtl" valign="top" align="right" colspan="3">
                                                                            <TSPControls:CustomASPXMemo runat="server" Height="34px" ID="txtMailTitle"  ReadOnly="True" Width="520px" ClientInstanceName="txtMailTitle" >
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomASPXMemo>


                                                                        </td>
                                                                    </tr>


                                                                    <tr>
                                                                        <td style="width: 86px" valign="top" align="right">
                                                                            <asp:Label runat="server" Text="امتیاز *" Width="37px" ID="Label1"></asp:Label>


                                                                        </td>
                                                                        <td dir="rtl" valign="top" align="right" colspan="3">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtJudgGrad"  Width="148px" ClientInstanceName="TextName" >
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                                    <RequiredField IsRequired="True" ErrorText="امتیاز را وارد نمایید"></RequiredField>

                                                                                    <RegularExpression ErrorText="امتیاز را با فرمت صحیح وارد نمایید" ValidationExpression="\d*.\d*"></RegularExpression>

                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomTextBox>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 86px" valign="top" align="right">
                                                                            <asp:Label runat="server" Text="توضیحات" ID="Label3"></asp:Label>


                                                                        </td>
                                                                        <td dir="rtl" valign="top" align="right" colspan="3">
                                                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtJudgViewPoint"  Width="525px" ClientInstanceName="TextDesc" ></TSPControls:CustomASPXMemo>


                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                      
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                         
                    <br /> <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldResearch">
                                                    </dxhf:ASPxHiddenField>
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                   
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td  dir="ltr">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table> </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                          
                <asp:ObjectDataSource ID="ODBResearch" runat="server" CacheDuration="30" DeleteMethod="Delete" EnableCaching="True" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.ResearchActivityManager" UpdateMethod="Update">
                   
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
