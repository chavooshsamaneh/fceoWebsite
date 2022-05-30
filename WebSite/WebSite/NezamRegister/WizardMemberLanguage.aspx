<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMemberLanguage.aspx.cs" Inherits="NezamRegister_WizardMemberLanguage"
    Title="عضویت حقیقی - آشنایی با دیگر زبان ها" %>

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
            ComboLq.SetSelectedIndex(-1);
            ComboName.SetSelectedIndex(-1);
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
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
                    <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Language" Text="زبان ها" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <ul class="HelpUL" style="text-align: center;">این فرم شامل اطلاعات تکمیلی است و پر کردن آن اجباری نمی باشد!</ul>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="آشنایی با دیگر زبان ها" runat="server">


                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 50%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="آشنایی با دیگر زبان ها" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 50%; height: 30px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px">
                                    </Image>
                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-3">نام زبان *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox ID="drdLanName" EnableIncrementalFiltering="true" runat="server"
                                    RightToLeft="True" ClientInstanceName="ComboName"
                                    DataSourceID="ODBLanguage"
                                    TextField="LanName" ValueField="LanId" ValueType="System.String">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText="نام زبان را انتخاب نمایید" IsRequired="True" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">حد دانش *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox ID="drdLanQuality" runat="server" ClientInstanceName="ComboLq"
                                    RightToLeft="True"
                                    DataSourceID="ODBLanguageQuality" TextField="LqName"
                                    ValueField="LqId" ValueType="System.String" EnableIncrementalFiltering="true">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText="حد دانش را انتخاب نمایید" IsRequired="True" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">توضیحات(حداکثر255کاراکتر)</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" ClientInstanceName="TextDesc"
                                    Height="45px" Width="100%">
                                    <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                </TSPControls:CustomASPXMemo>
                            </div>

                        </div>
                        <div class="Item-center">
                            <br />
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnSave" runat="server"
                                OnClick="btnSave_Click" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                UseSubmitBehavior="False">
                            </TSPControls:CustomAspxButton>

                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
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
                        <TSPControls:CustomAspxDevGridView2 ID="CustomAspxDevGridView1" runat="server" Width="100%"
                            RightToLeft="True" EnableViewState="False" Font-Size="8pt"
                            EnableCallBacks="False" AutoGenerateColumns="False" KeyFieldName="MlanId" OnRowDeleting="CustomAspxDevGridView1_RowDeleting">
                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="35px" ShowDeleteButton="true">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlanId"
                                    Name="MlanId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="LanId"
                                    Name="LanId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LanName" Caption="نام زبان"
                                    Name="LanName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="LqId" Name="LqId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LqName" Caption="حد دانش"
                                    Name="LqName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView2>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>


            <div class="Item-center">
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPre" OnClick="btnPre_Click" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" EnableViewState="False"
                    UseSubmitBehavior="False" CausesValidation="False" EnableTheming="False" ToolTip="بازگشت">

                    <ClientSideEvents Click="function(s, e) {
	SetEmpty();
}"></ClientSideEvents>

                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" EnableViewState="False"
                    UseSubmitBehavior="False" CausesValidation="False" EnableTheming="False" ToolTip="تایید و ادامه">

                    <ClientSideEvents Click="function(s, e) {
	SetEmpty();
}"></ClientSideEvents>

                </TSPControls:CustomAspxButton>
                <div class="Item-center">
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdbMeLanguage" runat="server" FilterExpression="MeId={0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.MemberLanguageManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBLanguage" runat="server" CacheDuration="30" EnableCaching="True"
        SelectMethod="GetData" TypeName="TSP.DataManager.LanguageManager" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBLanguageQuality" runat="server" CacheDuration="30" DeleteMethod="Delete"
        EnableCaching="True" SelectMethod="GetData" TypeName="TSP.DataManager.LanguageQualityManager"></asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
</asp:Content>
