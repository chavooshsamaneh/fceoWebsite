<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMemberActivity.aspx.cs" Inherits="NezamRegister_WizardMemberActivity"
    Title="عضویت حقیقی - فعالیت ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function SetEmpty() {
            TextDesc.SetText("");
            Per.SetText("");
            ComboName.SetSelectedIndex(-1);

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="مشخصات فردی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Activity" Text="فعالیت ها" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Language" Text="زبان ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <ul class="HelpUL" style="text-align: center;">این فرم شامل اطلاعات تکمیلی است و پر کردن آن اجباری نمی باشد!</ul>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="فعالیت" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">کمیسیون های همکاری</legend>
                            <TSPControls:CustomASPxCheckBoxList runat="server" RepeatColumns="3" TextField="ComName" ValueField="ComId"
                                DataSourceID="ODBCom"  ID="chbComId" Width="100%">
                            </TSPControls:CustomASPxCheckBoxList>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">زمینه های فعالیت</legend>

                            <div class="row">
                                <div class="col-md-3">زمینه فعالیت فعلی</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                        ValueType="System.String" DataSourceID="ODbAtType" TextField="AtName" ValueField="AtId"
                                        RightToLeft="True" Width="100%"
                                        ClientInstanceName="ComboName"
                                        ID="ChbAtType">
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField ErrorText="زمینه فعالیت را انتخاب نمایید"></RequiredField>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                            </div>

                            <div class="row">
                                فعالیت شما در کدامیک از زمینه های زیر است؟
                          
                            </div>

                            <div class="row">
                                <div class="col-md-3">زمینه فعالیت *</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                        ValueType="System.String" DataSourceID="ODBAtSubj" TextField="AsName" ValueField="AsId"
                                        RightToLeft="True" Width="100%"
                                        ClientInstanceName="ComboName"
                                        ID="drdAtSubj" >
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" ErrorText="زمینه فعالیت را انتخاب نمایید"></RequiredField>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                </div>
                                <div class="col-md-3">درصد مشارکت</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="3"
                                        ClientInstanceName="Per" ID="txtPercent">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="درصد صحیح نیست" ValidationExpression="^([0-9]|[1-9][0-9]|100)$"></RegularExpression>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">توضیحات (حداکثر 255 کاراکتر)</div>
                                <div class="col-md-9">
                                    <TSPControls:CustomASPXMemo runat="server" Height="45px" Width="100%"
                                        ClientInstanceName="TextDesc" ID="txtAtDesc">
                                        <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                    </TSPControls:CustomASPXMemo>
                                </div>

                            </div>
                            <div class="Item-center">
                                <br />
                                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" UseSubmitBehavior="False" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                    ID="btnSave"
                                    OnClick="btnSave_Click">
                                </TSPControls:CustomAspxButton>

                                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                                    CausesValidation="False" AutoPostBack="False" ID="btnRefresh" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
 {
	SetEmpty();	
  }
}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                               
                            </div>
 <br />
                            <TSPControls:CustomAspxDevGridView2 runat="server"
                                KeyFieldName="MasId" AutoGenerateColumns="False" RightToLeft="True"
                                Width="100%" ID="CustomAspxDevGridView1" EnableViewState="False" Font-Size="8pt"
                                OnRowDeleting="CustomAspxDevGridView1_RowDeleting">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="35px">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="MasId" Name="MasId" Caption="کد" Visible="False"
                                        VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="AsName" Name="AsName" Caption="نام فعالیت"
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="AsPercent" Name="AsPercent" Caption="درصد مشارکت"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Width="260px"
                                        Caption="توضیحات" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                        <
                    </dxp:PanelContent>
                </PanelCollection>
                <HeaderTemplate>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td style="width: 20%; height: 28px" valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="فعالیت ها" Font-Bold="true">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 80%; height: 30px" valign="middle" align="left">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" Text=" " CausesValidation="False" UseSubmitBehavior="False"
                                        EnableTheming="False" EnableViewState="False" ToolTip="راهنما"
                                        AutoPostBack="false">
                                        <Image Height="25px" Url="~/Images/Help.png" Width="25px">
                                        </Image>
                                        <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </HeaderTemplate>
            </TSPControls:CustomASPxRoundPanel>
            <div class="Item-center">

                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" CausesValidation="False"
                    UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="بازگشت">
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" CausesValidation="False"
                    UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه">
                </TSPControls:CustomAspxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ODBAtSubj" runat="server" CacheDuration="30" EnableCaching="True"
        SelectMethod="GetData" TypeName="TSP.DataManager.ActivitySubjectManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBCom" runat="server" CacheDuration="30" EnableCaching="True"
        SelectMethod="GetData" TypeName="TSP.DataManager.CommissionManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODbAtType" runat="server" CacheDuration="30" EnableCaching="True"
        SelectMethod="GetData" TypeName="TSP.DataManager.ActivityTypeManager"></asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
</asp:Content>
