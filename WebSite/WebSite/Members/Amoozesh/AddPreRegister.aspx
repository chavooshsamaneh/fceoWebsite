<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddPreRegister.aspx.cs" Inherits="Members_Amoozesh_AddPreRegister"
    Title="مشخصات پیش ثبت نام دوره" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function SetStateMorning(Node,ch)
        {
            //alert(Node);
            if   (Node<0)
                Node=-Node;
            //window.alert(Node);
            if(ch==true)

                hMorning.Set('m'+Node,ch);
            else if(hMorning.Contains('m'+Node))
                hMorning.Remove('m'+Node);   
            //window.alert(hMorning.Get('m'+Node));
        }

        function SetStateNoon(Node,ch)
        {

            if   (Node<0)
                Node=-Node;
            if(ch==true)
                hNoon.Set('n'+Node,ch);
            else if(hNoon.Contains('n'+Node))
                hNoon.Remove('n'+Node);

        }
        function SetStateAfterNoon(Node,ch)
        {

            if   (Node<0)
                Node=-Node;
            if(ch==true)
                hAfterNoon.Set('a'+Node,ch);
            else if(hAfterNoon.Contains('a'+Node))
                hAfterNoon.Remove('a'+Node);
        }

        function SetCityControlValues()
        {
            gridCity.GetRowValues(gridCity.GetFocusedRowIndex(),'CitName;CitId;AgentName;AgentCode;AgentAddress',SetCityValue);
        }

        function SetCityValue(values)
        {
            txtCity.SetText(values[0]);
            cmbinstitue.PerformCallback(values[1]);
            //HiddenFieldPreRegister.Set('CitId',)
            //txtName.SetText(values[1]);
            //txtFamily.SetText(values[2]);
        }
    </script>

    <div style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%" dir="ltr" align="center">
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" CausesValidation="false" Text=" "
                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" CausesValidation="false" Text=" "
                                                    ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnSave_Click" Text=" "
                                                    ToolTip="ذخیره" Width="25px" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {

}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelPreRegister" HeaderText="ویرایش" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right" valign="top" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="واحد درسی" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top"  width="35%">
                                                <TSPControls:CustomAspxComboBox  width="100%" runat="server"  TextField="CrsName"
                                                    ID="cmbCourse"  DataSourceID="ObjdsCourse" ValueType="System.String"
                                                    ValueField="CrsId"  RightToLeft="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="واحد درسی را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td align="right" valign="top"  width="15%">
                                                <dxe:ASPxLabel runat="server" Text="استاد" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  align="right" valign="top"  width="35%">
                                                <TSPControls:CustomAspxComboBox  width="100%" runat="server"  TextField="TeFullName"
                                                    ID="cmbTeacher"  DataSourceID="ObjdsTeacher" ValueType="System.String"
                                                    ValueField="TeId" TextFormatString="Name Family"  RightToLeft="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="استاد را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="مؤسسه" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td  align="right" valign="top">
                                                <TSPControls:CustomAspxComboBox  width="100%" runat="server"  TextField="InsName"
                                                    ID="cmbinstitue"  DataSourceID="ObjdsInstitue" ValueType="System.String"
                                                    ValueField="InsId" ClientInstanceName="cmbinstitue"  RightToLeft="true">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="موسسه را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: center" colspan="4">
                                                <dxe:ASPxLabel runat="server" Text="روزها وساعت های پیشنهادی  خود را انتخاب نمایید"
                                                    Width="257px" ID="ASPxLabel4" ForeColor="red">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top"></td>
                                            <td align="right" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td dir="rtl" colspan="4" align="center">
                                                  <TSPControls:CustomAspxDevGridView2 runat="server"  EnableViewState="False"
                                                    ID="GridViewCourseHours" AutoGenerateColumns="False" 
                                                    OnHtmlRowPrepared="GridViewCourseHours_HtmlRowPrepared" Width="100%">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DayName" Width="100px"
                                                            Caption="روز">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataCheckColumn VisibleIndex="1" FieldName="Morning" Caption="صبح">
                                                            <DataItemTemplate>
                                                                <input type="checkbox" id="n<%# Container.KeyValue %>" <%# HDMorning.Contains("m"+Container.KeyValue)?"checked=\"checked\"":"" %> onclick="SetStateMorning('<%# Container.KeyValue %>    ',checked)" />
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataCheckColumn>
                                                   <%--     <dxwgv:GridViewDataCheckColumn VisibleIndex="2" FieldName="Noon" Caption="ساعت(12-18)">
                                                            <DataItemTemplate>
                                                                <input type="checkbox" id="e<%# Container.KeyValue %>" <%# HDNoon.Contains("n"+Container.KeyValue)?"checked=\"checked\"":"" %> onclick="SetStateNoon('<%# Container.KeyValue %>    ',checked)" />
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataCheckColumn>--%>
                                                        <dxwgv:GridViewDataCheckColumn VisibleIndex="4" FieldName="Afternoon" Caption="بعد از ظهر">
                                                            <DataItemTemplate>
                                                                <input type="checkbox" id="d<%# Container.KeyValue %>" <%# HDAfterNoon.Contains("a"+Container.KeyValue)?"checked=\"checked\"":"" %> onclick="SetStateAfterNoon('<%# Container.KeyValue %>    ',checked)" />
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataCheckColumn>
                                                    </Columns>
                                                </TSPControls:CustomAspxDevGridView2>
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
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" CausesValidation="false" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False" OnClick="btnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" OnClick="btnEdit_Click" CausesValidation="false" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" Width="25px"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {

}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnSave_Click" Text=" "
                                                    ToolTip="ذخیره" Width="25px" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
		
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <asp:ObjectDataSource ID="ObjdsTeacher" runat="server" TypeName="TSP.DataManager.TeacherManager"
                        SelectMethod="SelectActivTeachers"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsCourse" runat="server" TypeName="TSP.DataManager.CourseManager"
                        SelectMethod="SelectActiveCourceName"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsInstitue" runat="server" TypeName="TSP.DataManager.InstitueManager"
                        SelectMethod="SelectInstitueByCity" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="CitId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="0" Name="InActive"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <dxhf:ASPxHiddenField ID="HDMorning" runat="server" ClientInstanceName="hMorning">
                    </dxhf:ASPxHiddenField>
                  <%--  <dxhf:ASPxHiddenField ID="HDNoon" runat="server" ClientInstanceName="hNoon">
                    </dxhf:ASPxHiddenField>--%>
                    <dxhf:ASPxHiddenField ID="HDAfterNoon" runat="server" ClientInstanceName="hAfterNoon">
                    </dxhf:ASPxHiddenField>
                    <dxhf:ASPxHiddenField ID="HiddenFieldPreRegister" runat="server">
                    </dxhf:ASPxHiddenField>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
