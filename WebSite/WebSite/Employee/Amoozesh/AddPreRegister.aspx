<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddPreRegister.aspx.cs" Inherits="Employee_Amoozesh_AddPreRegister"
    Title="مشخصات پیش ثبت نام دروس" %>

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

    <script language="javascript" type="text/javascript">
        function SetControlValues()
        {
            grid.GetRowValues(grid.GetFocusedRowIndex(),'MeId;FirstName;LastName',SetValue);
        }
        function SetValue(values)
        {
            txtMeNo.SetText(values[0]);
            txtName.SetText(values[1]);
            txtFamily.SetText(values[2]);
            //GridViewPreRegister.PerformCallback(values[0]);
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

        function SetStateMorning(Node,ch)
        {
            if   (Node<0)
                Node=-Node;
            if(ch==true)
                hMorning.Set('m'+Node,ch);
            else if(hMorning.Contains('m'+Node))
                hMorning.Remove('m'+Node);   
            //window.alert(tree.GetVisibleNodeKeys().length);
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
    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPreRegister" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeNo">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeNo"  AutoPostBack="True"
                                            ClientInstanceName="txtMeNo" OnTextChanged="txtMeNo_TextChanged">
                                            <validationsettings display="Dynamic" errortextposition="Bottom">                                                   
                                                        <RequiredField IsRequired="True" ErrorText="عضو مورد نظر را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </validationsettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">&nbsp;</td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName"  AutoPostBack="True"
                                            ClientInstanceName="txtName">
                                            <validationsettings display="Dynamic" errortextposition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="نام خانوادگی را وارد نمایید" IsRequired="True"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </validationsettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFamily"  AutoPostBack="True"
                                            ClientInstanceName="txtFamily">
                                            <validationsettings display="Dynamic" errortextposition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="نام خانوادگی را وارد نمایید" IsRequired="True"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </validationsettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="واحد درسی" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="CrsName" ID="cmbCourse" DataSourceID="ObjdsCourse" ValueType="System.String"
                                            ValueField="CrsId">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="استاد" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="TeName" ID="cmbTeacher" DataSourceID="ObjdsTeacher" ValueType="System.String"
                                            ValueField="TeId" TextFormatString="Name Family">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCity"  Enabled="False"
                                                            AutoPostBack="True" ClientInstanceName="txtCity">
                                                            <validationsettings display="Dynamic" errortextposition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="شهر را انتخاب نمایید"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </validationsettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ToolTip="جستجو" CausesValidation="False"
                                                            ID="btnSearchCity" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	popUpCity.Show();
}"></ClientSideEvents>
                                                            <Image Url="~/Images/icons/Search.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="مؤسسه" ID="ASPxLabel2">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="InsName" ID="cmbinstitue" DataSourceID="ObjdsInstitue" ValueType="System.String"
                                            ValueField="InsId" ClientInstanceName="cmbinstitue"
                                            OnCallback="cmbinstitue_Callback">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">&nbsp;</td>
                                    <td dir="ltr" valign="top" align="right">&nbsp;</td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <dxe:ASPxLabel runat="server" Text="روزها وساعت های پیشنهادی  خود را انتخاب نمایید"
                                            ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" colspan="4">
                                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                                         ID="GridViewCourseHours" AutoGenerateColumns="False"
                                            OnHtmlRowPrepared="GridViewCourseHours_HtmlRowPrepared">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DayName" Width="100px"
                                                    Caption="روز">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataCheckColumn VisibleIndex="1" FieldName="Morning" Caption="ساعت(8-12)">
                                                    <DataItemTemplate>
                                                        <input type="checkbox" id="m<%# Container.KeyValue %>" <%# HDMorning.Contains("m"+Container.KeyValue)?"checked=\"checked\"":"" %> onclick="SetStateMorning('<%# Container.KeyValue %>    ',checked)" />
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn VisibleIndex="2" FieldName="Noon" Caption="ساعت(12-18)">
                                                    <DataItemTemplate>
                                                        <input type="checkbox" id="n<%# Container.KeyValue %>" <%# HDNoon.Contains("n"+Container.KeyValue)?"checked=\"checked\"":"" %> onclick="SetStateNoon('<%# Container.KeyValue %>    ',checked)" />
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn VisibleIndex="4" FieldName="Afternoon" Caption="ساعت(بعد از 18)">
                                                    <DataItemTemplate>
                                                        <input type="checkbox" id="a<%# Container.KeyValue %>" <%# HDAfterNoon.Contains("a"+Container.KeyValue)?"checked=\"checked\"":"" %> onclick="SetStateAfterNoon('<%# Container.KeyValue %>    ',checked)" />
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataCheckColumn>
                                            </Columns>
                                            <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                        </TSPControls:CustomAspxDevGridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server"
                HeaderText="جستجو" ClientInstanceName="popUpCity" Height="269px">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
               
                            <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                               ID="CustomAspxDevGridView1" DataSourceID="ObjdsCity" KeyFieldName="CitId"
                                AutoGenerateColumns="False" ClientInstanceName="gridCity"
                                OnCustomCallback="CustomAspxDevGridView1_CustomCallback">
                                <ClientSideEvents RowDblClick="function(s, e) {
	SetCityControlValues();
	popUpCity.Hide();
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CitName" Caption="شهر"
                                        Name="CitName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="AgentName" Caption="نام نمایندگی"
                                        Name="AgentName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="AgentCode"
                                        Caption="کد نمایندگی" Name="AgentCode">
                                    </dxwgv:GridViewDataTextColumn>
                                <%--    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="AgentAddress" Caption="آدرس"
                                        Name="AgentAddress">
                                    </dxwgv:GridViewDataTextColumn>--%>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="1px" >
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
              
                        <asp:ObjectDataSource runat="server" 
                            SelectMethod="SelectByAgent" ID="ObjdsCity" UpdateMethod="Update" TypeName="TSP.DataManager.CityManager"
                            OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="AgentId"></asp:Parameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
               
            </TSPControls:CustomASPxPopupControl>
            <br />
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPreRegister" ClientInstanceName="HiddenFieldPreRegister">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
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
            <asp:ObjectDataSource ID="ObjdsTeacher" runat="server" TypeName="TSP.DataManager.TeacherManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsCourse" runat="server" TypeName="TSP.DataManager.CourseManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsInstitue" runat="server" TypeName="TSP.DataManager.InstitueManager"
                SelectMethod="SelectInstitueByCity" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="CitId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <dxhf:ASPxHiddenField ID="HDMorning" runat="server" ClientInstanceName="hMorning">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDNoon" runat="server" ClientInstanceName="hNoon">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDAfterNoon" runat="server" ClientInstanceName="hAfterNoon">
            </dxhf:ASPxHiddenField>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
