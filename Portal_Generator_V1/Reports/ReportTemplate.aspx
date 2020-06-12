<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTemplate.aspx.cs" Inherits="Portal_Generator_V1.Reports.ReportTemplate" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;">
    <h2> <asp:Label runat="server" ID="nombreReporte"/> </h2>
    <form id="form1" runat="server">
        <div class="table-panel-body">
        <table class="table table-striped table-hover">
            <tr>
                <th><span >Seleccionar Reporte Deseado: </span></th>
                <td>
                <asp:DropDownList runat="server" ID="ddlReportes" OnSelectedIndexChanged="ddlReportes_Cambio" AutoPostBack="true">
                    <asp:ListItem value="Informe Anual Fecha" Text="INFORME ANUAL" />
                    <%--<asp:ListItem value="Informe Anual" Text="INFORME ANUAL" />--%>
                </asp:DropDownList>
                </td>
            </tr>
            <tr></tr>
        </table>
        </div>
        <br />
        <div>
            <asp:ScriptManager ID="ScriptManagerReport" runat="server">

            </asp:ScriptManager>
            <rsweb:ReportViewer id="rvSiteMapping" runat ="server" ShowPrintButton="false"  Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false" ></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
