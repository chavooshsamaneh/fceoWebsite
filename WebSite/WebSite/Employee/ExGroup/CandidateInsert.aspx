<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="CandidateInsert.aspx.cs" Inherits="Employee_ExGroup_CandidateInsert"
    Title="مشخصات نامزد" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
            <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelCandidate" runat="server" Width="100%" ClientInstanceName="CallbackPanelCandidate"
                HideContentOnCallback="False" OnCallback="CallbackPanelCandidate_Callback">   <clientsideevents endcallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}
"></clientsideevents>
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                            [<a class="closeLink" href="#">بستن</a>]
                        </div>
                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>



                                    <table cellpadding="0">
                                        <tr>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true"   runat="server" Text=" " ToolTip="جدید"
                                                    CausesValidation="False" ID="btnnew" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnnew_Click">
                                                   
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave" Text=" " runat="server"
                                                    EnableTheming="False" EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/save.png">
                                                    </Image>
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server" EnableTheming="False"
                                                    EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" OnClick="btnback_Click"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/Back.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanelMenu>
                        <br />
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelExGroupPeriod" HeaderText="مشخصات تشکل" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>


                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="نوع تشکل" Width="100%">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" width="35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtExGroupName" Width="100%"
                                                        Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td align="right" valign="top" width="15%"></td>
                                                <td align="left" valign="top" width="35%"></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ شروع" Width="100%">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="right" dir="ltr" valign="top" width="35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDate" Width="100%"
                                                        Enabled="false" RightToLeft="False">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td align="right" valign="top" width="15%">
                                                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ پایان" Width="100%">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="left" dir="ltr" valign="top" width="35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDate" Width="100%" Enabled="false"
                                                        RightToLeft="False">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                        <br />
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" ClientInstanceName="RoundPanelRequest" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>

                                    <table style="width: 100%;">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" width="15%"></td>
                                                <td valign="top" align="right" width="35%"></td>
                                                <td valign="top" align="right" width="15%"></td>
                                                <td valign="top" align="right" width="35%">
                                                    <dx:ASPxImage ID="ImgMember" runat="server" ImageUrl="~/Images/Person.png" Width="80px"
                                                        Height="80px">
                                                    </dx:ASPxImage>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="labelMeId">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeId" Width="100%" ClientInstanceName="txtMeId"
                                                        AutoPostBack="false">
                                                        <validationsettings display="Dynamic" errortextposition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد کنید" ValidationExpression="^\d*$">
                                                            </RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </validationsettings>
                                                        <ClientSideEvents TextChanged="function(s, e) {
 if(txtMeId.GetText()!='')
    {
	  CallbackPanelCandidate.PerformCallback(txtMeId.GetText()+';'+'FindMe');
    }
}"></ClientSideEvents>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="middle" align="center"></td>
                                                <td valign="middle" align="center"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نام" ID="labelFirstName" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFirstName" Width="100%"
                                                        Enabled="false" ClientInstanceName="txtFirstName">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="middle" align="center"></td>
                                                <td valign="middle" align="center"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="labelLastName">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="left">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtLastName" Width="100%"
                                                        Enabled="false" ClientInstanceName="txtLastName">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="middle" align="center"></td>
                                                <td valign="middle" align="center"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="رشته تحصیلی" ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String" DataSourceID="ObjdsMemberLicence"
                                                        TextField="MeLicenceNamertl" ValueField="MlId" AutoPostBack="true" OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged"
                                                        ID="cmbMajor"
                                                        ClientInstanceName="cmbMajor">
                                                     <%--   <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                          CallbackPanelCandidate.PerformCallback(cmbMajor.GetSelectedIndex()+';'+'FindLicence');
                                                        }"></ClientSideEvents>--%>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="True" ErrorText="رشته تحصیلی را انتخاب نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
                                                        SelectMethod="SelectByMemberId">
                                                        <SelectParameters>
                                                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MemberId"></asp:Parameter>
                                                            <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="دانشگاه" ID="ASPxLabel2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtUniversity" Width="100%"
                                                        Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFileno" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel4">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtImpName" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel5">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtObsName" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel6">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesName" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="ASPxLabel7">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtUrbonism" Width="100%"
                                                        Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="ASPxLabel8">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMapping" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="پایه ترافیک" ID="ASPxLabel9">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTraffic" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="کد انتخاباتی" ID="ASPxLabel13" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtElectionCode" Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right"></td>
                                                <td valign="top" align="right"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="وضعیت" ID="labelStatus" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        ID="cmbStatus" ValueType="System.Int32" ClientInstanceName="comboStatus"
                                                        EnableIncrementalFiltering="True">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
cmbMajor2.SetSelectedIndex(-1);
if(comboStatus.GetValue()==2)
 {
   lblMajor2.SetVisible(true);   
   cmbMajor2.SetVisible(true);
   CallbackPanelCandidate.PerformCallback(cmbMajor2.GetValue()+';'+'FindParentMajor');
 }
else
 {
   lblMajor2.SetVisible(false);
   cmbMajor2.SetVisible(false);
 }
}" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="وضعیت نامزد را انتخاب کنید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="نامزد انتخاباتی" Selected="true"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="اصلی"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="2" Text="علی البدل"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="3" Text="انصراف"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="4" Text="انصراف اولیه"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="5" Text="دیگر"></dxe:ListEditItem>
                                                        </Items>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="رشته علی البدل" Width="100%" ID="lblMajor2" ClientVisible="false"
                                                        ClientInstanceName="lblMajor2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="left">
                                                    <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String" DataSourceID="ObjdsMemberLicence2"
                                                        TextField="MjName" ValueField="MjId" AutoPostBack="false"
                                                        ID="cmbMajor2"
                                                        ClientInstanceName="cmbMajor2" ClientVisible="false">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="True" ErrorText="رشته تحصیلی علی البدل را انتخاب نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ObjdsMemberLicence2" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
                                                        SelectMethod="SelectMajorParents">
                                                        <SelectParameters>
                                                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MemberId"></asp:Parameter>
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="سمت در تشکل" ID="labelManager" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        ID="cmbIsManager" ValueType="System.Int32" ClientInstanceName="comboIsManager"
                                                        EnableIncrementalFiltering="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
txtPosition.SetText('');
if(comboIsManager.GetValue()==2)
 {
   txtPosition.SetVisible(true);   
   labelPosition.SetVisible(true);
 }
else
 {
   txtPosition.SetVisible(false);
   labelPosition.SetVisible(false);
 }
}" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="رییس"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="رابط"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="2" Text="سایر"></dxe:ListEditItem>
                                                        </Items>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="labelPosition" ClientVisible="false"
                                                        Text="عنوان سمت" ID="labelPosition" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="left">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ClientVisible="false" ID="txtPosition" ClientInstanceName="txtPosition"
                                                        Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تعداد آرا" Width="100%" ID="ASPxLabel16">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="left">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtVoteCount" Width="100%">
                                                        <validationsettings display="Dynamic" errortextposition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RegularExpression ErrorText="تعداد آرا را با فرمت صحیح وارد کنید" ValidationExpression="^\d*$">
                                                            </RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </validationsettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="توضیحات" ID="labelContry" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td colspan="3" valign="top" align="right">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDescription"
                                                        Width="100%">
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="فایل" ID="Label9"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td dir="rtl" style="width: 80%">
                                                                    <TSPControls:CustomAspxUploadControl runat="server" ID="flpcAttachment" InputType="Files"
                                                                        ClientInstanceName="flpcAttachment" OnFileUploadComplete="flpcAttachment_FileUploadComplete"
                                                                        MaxSizeForUploadFile="3000000" UploadWhenFileChoosed="true" Width="100%">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
imgEndAttachment.SetVisible(true);
	HyperLinkAttachment.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndAttachment.SetVisible(false); 
	HyperLinkAttachment.SetNavigateUrl('');
 }                                     
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <dxe:ASPxImage runat="server" ID="imgEndAttachment" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                                        ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndAttachment">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxHyperLink ID="HyperLinkAttachment" runat="server" ClientInstanceName="HyperLinkAttachment"
                                                        Target="_blank" Text="آدرس فایل">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                                <td valign="top" align="right"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="خلاصه رزومه" ID="ASPxLabel12" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td colspan="3" valign="top" align="right"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" valign="top" align="right">
                                                    <TSPControls:CustomASPxHtmlEditor ID="txtResume" runat="server" Width="100%">
                                                    </TSPControls:CustomASPxHtmlEditor>
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
                                    <table>
                                        <tr>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true"   runat="server" Text=" " ToolTip="جدید"
                                                    CausesValidation="False" ID="btnnew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnnew_Click">
                                                   
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click" UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/save.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" OnClick="btnback_Click"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/Back.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanelMenu>

                        <asp:ObjectDataSource ID="ObjectDataSourceExGroupPeriod" runat="server" TypeName="TSP.DataManager.ExGroupPeriodManager"
                            SelectMethod="GetData"></asp:ObjectDataSource>
                        <dx:ASPxHiddenField ID="HiddenFieldModeID" runat="server">
                        </dx:ASPxHiddenField>
                   
                     
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
</asp:Content>
