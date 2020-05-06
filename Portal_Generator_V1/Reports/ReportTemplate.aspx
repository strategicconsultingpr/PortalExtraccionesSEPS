<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTemplate.aspx.cs" Inherits="Portal_Generator_V1.Reports.ReportTemplate" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <div>
            <div class="col-md-2">
                <strong>Seleccionar Reporte Deseado: </strong>
            </div>
            <div class="col-md-10">
            <asp:DropDownList runat="server" ID="ddlReportes" OnSelectedIndexChanged="ddlReportes_Cambio" AutoPostBack="true">
                <asp:ListItem value="/Informes de Portal Extracciones/Informe Anual" Text="Informe Anual" />
                <asp:ListItem value="/Informes Sistema de Perfiles/RPT_TEDS" Text="Reporte2" />
            </asp:DropDownList>
            </div>
        </div>
        <div>
            <asp:ScriptManager ID="ScriptManagerReport" runat="server">
               <%-- <Scripts>
                    <asp:ScriptReference Assembly="ReportViewerForMvc" Name="ReportViewerForMvc.Scripts.PostMessage.js" />
                </Scripts>--%>
            </asp:ScriptManager>
            <rsweb:ReportViewer id="rvSiteMapping" runat ="server" ShowPrintButton="false"  Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false" ></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
