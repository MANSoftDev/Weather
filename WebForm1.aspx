<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="Weather.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD align="middle" colSpan="2"><STRONG>Local Weather Forecast</STRONG></TD>
				</TR>
				<TR>
					<TD><%=GetWeather()%></TD>
					<TD><%=GetForecast()%></TD>
				</TR>
				<TR>
					<TD align="middle" colSpan="2">information provided by WPXI</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
