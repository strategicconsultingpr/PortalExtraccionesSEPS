<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportesTEDS_SEPS.aspx.cs" Inherits="Portal_Generator_V1.Reports.ReportesTEDS_SEPS" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;">
    <div style="margin-left:auto; margin-right: auto; text-align:center">

        <h2> <asp:Label runat="server" ID="nombreReporte"/> </h2>

        <h4> <asp:Label runat="server" ID="descripcionReporte" /> </h4>

    </div>


    <form id="form1" runat="server">
        <div class="table-panel-body">
        <table class="table table-striped table-hover">
            <tr>
                <th><span >Seleccionar Reporte Deseado: </span></th>
                <td>
                <asp:DropDownList runat="server" ID="ddlReportes" OnSelectedIndexChanged="ddlReportes_Cambio" AutoPostBack="true">
                    <asp:ListItem value="AD_SM_TEDS-SEPS MATCH" Text="AD | SALUD MENTAL" />
                    <asp:ListItem value="AD_AS_TEDS-SEPS MATCH" Text="AD | ABUSO SUSTANCIA" />
                    <asp:ListItem value="DIS_SM_TEDS-SEPS MATCH" Text="AL | SALUD MENTAL" />
                    <asp:ListItem value="DIS_AS_TEDS-SEPS MATCH" Text="AL | ABUSO SUSTANCIA" />
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
