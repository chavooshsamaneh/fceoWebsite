<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master"  AutoEventWireup="true"
    CodeFile="SearchIsNotAccessible.aspx.cs" Inherits="MembersInfo_SearchIsNotAccessible"
    Title="عدم دسترسی به صفحه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div dir="ltr" style="width: 100%">
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
             GroupBoxCaptionOffsetY="-24px" ShowHeader="False" 
            Width="100%">
            <ContentPaddings PaddingLeft="4px" PaddingTop="10px" PaddingBottom="10px"></ContentPaddings>
            <HeaderStyle Height="23px">
                <Paddings PaddingLeft="2px" PaddingTop="0px" PaddingBottom="0px"></Paddings>
            </HeaderStyle>
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <table align="center" width="100%">
                        <tr>
                            <td align="center">
                                <p align="center" style="font-family: Tahoma; font-size: 10pt; color: darkblue; line-height: 15pt">
                                    دسترسی به لیست اعضای سازمان در حال بررسی می باشد
                                    <br />
                                    <br />
                                    این صفحه تا اطلاع ثانوی در دسترس نمی باشد
                                </p>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
</asp:Content>
