<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceSettingsChannel.aspx.cs" Inherits="AppControl_Manage_MaintenanceSettingsChannel" EnableEventValidation="false" %>
<%@ Register Src="NavBar.ascx" TagPrefix="wuc" TagName="NavBar" %>
<%@ Register Src="LeftMenu.ascx" TagPrefix="wuc" TagName="LeftMunu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
<title>MaintenanceSettingsChannel</title>
<link rel="stylesheet" media="screen" type="text/css" href="css/styles.css" />
<link rel="stylesheet" media="screen" type="text/css" href="js/jquery-ui/css/ui-lightness/jquery-ui-1.10.4.custom.css" />
<script type="text/javascript" src="js/jquery-ui/js/jquery-1.10.2.js"></script>
<script type="text/javascript" src="js/jquery-ui/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="js/BackendSupport.js"></script>
<script type="text/javascript" src="js/cookie.js"></script>
<script type="text/javascript" src="js/portals.js"></script>
<%--<%=Refresh_State_JSCode%>--%>
<style type="text/css">
.label {width: 200px !important;}
a {cursor: pointer;}
.auto-style1 {height: 1517px;}
.auto-style2 {width: 94%;height: 1512px;}
.auto-style3 {overflow: auto;padding-bottom: 7px;width: 1147px;height: 1420px;margin-right: 0;}
.auto-style4 {left: 0px;top: 0px;height: 1509px;margin-right: 165px;}
.label{background-color: #d3e5ee;}
</style>
</head>
<body onload="UpdateSta()" class="nav">
    <form id="MaintenanceSettingsChannel" runat="server" onkeydown="if(event.keyCode==13){return false;}">
        <div id="main-wrapper">
            <!-- Outermost container -->
            <wuc:NavBar ID="NavBar" runat="server" />
            <!-- BEGIN main content wrapper DIV container -->
            <div id="wrapper" class="wrapper-center with-sidebar-column">
                <div id="left-column" class="left-column" style="left: -6px; top: 0px">
                    <div class="squeeze">
                        <div class="sidebar-box-content sidebar-menu">
                            <div class="content-wrapper">
                                <wuc:LeftMunu ID="LeftMunu" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="right-column">
                    <div class="squeeze">
                        <div class="data-table">
                        <h3><asp:Label ID="label32" runat="Server" Text="<%$ Resources:MyGlobal,MAINTENANCE_SETTINGS_CHANNEL %>"/></h3>
                        <%--<div class="auto-style4" id="SettingsChannel" style="margin-bottom: 0">--%>
                            <%--<div id="fancybox-content" class="auto-style1">--%>
                                <div class="search-bar">
                                    <div class="sub-btn" style="height:23px;margin-top:10px;">
                                        <asp:Label ID="Label1" runat="server" ForeColor="#993300" Font-Size="X-Large"></asp:Label>
                                    </div>
                                </div>
                                <div class="search-bar">
                                    <div class="sub-btn">
                                        <asp:Button ID="Button1" runat="server" Text="顯示全部" CssClass="blue" OnClick="Button1_Click" />
                                        <asp:Button ID="Button2" runat="server" Text="修改儲存" CssClass="yellow" OnClick="Button2_Click"  Enabled="False" />
                                    </div>
                                    <div class="input-group">
                                        <asp:Label ID="label2" runat="server" Text="銀行清單" />
                                        <div class="form-control">
                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" ViewStateMode="Inherit" Enabled="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="input-lg">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="Internal" runat="server" Visible="false">
                                    <div class="auto-style3">
                                        <div class="inf_float">
                                            <asp:Table ID="Table1" runat="server" border="0" CssClass="DataList" >
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell CssClass="title"><%=Item%></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell CssClass="title">維護時間</asp:TableHeaderCell>
                                                    <asp:TableHeaderCell CssClass="title">提示</asp:TableHeaderCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="DeBgTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=DeBgTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="DeBgTimeYear" runat="server" MaxLength="2" Width="70px" OnSelectedIndexChanged="DeBgTimeYear_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="DeBgTimeMonth" runat="server" MaxLength="2" Width="65px" OnSelectedIndexChanged="DeBgTimeMonth_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="DeBgTimeDay" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id=""></span>
                                                        <asp:DropDownList ID="DeBgTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DeBgTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DeBgTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationDeBgTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="DeEnTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=DeEnTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="DeEnTimeYear" runat="server" MaxLength="2" Width="70px" OnSelectedIndexChanged="DeEnTimeYear_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="DeEnTimeMonth" runat="server" MaxLength="2" Width="65px" OnSelectedIndexChanged="DeEnTimeMonth_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="DeEnTimeDay" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id=""></span>
                                                        <asp:DropDownList ID="DeEnTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DeEnTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DeEnTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationDeEnTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="DaBgTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=DaBgTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="DaBgTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DaBgTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DaBgTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationDaBgTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="DaEnTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=DaEnTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="DaEnTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DaEnTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="DaEnTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationDaEnTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="WdBgTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=WdBgTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="WdBgTimeYear" runat="server" MaxLength="2" Width="70px" OnSelectedIndexChanged="WdBgTimeYear_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="WdBgTimeMonth" runat="server" MaxLength="2" Width="65px" OnSelectedIndexChanged="WdBgTimeMonth_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="WdBgTimeDay" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id=""></span>
                                                        <asp:DropDownList ID="WdBgTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WdBgTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WdBgTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationWdBgTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="WdEnTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=WdEnTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="WdEnTimeYear" runat="server" MaxLength="2" Width="70px" OnSelectedIndexChanged="WdEnTimeYear_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="WdEnTimeMonth" runat="server" MaxLength="2" Width="65px" OnSelectedIndexChanged="WdEnTimeMonth_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <span id="">- </span>
                                                        <asp:DropDownList ID="WdEnTimeDay" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id=""></span>
                                                        <asp:DropDownList ID="WdEnTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WdEnTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WdEnTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationWdEnTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="WrmBgTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=WrmBgTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="WrmBgTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WrmBgTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WrmBgTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationWrmBgTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="WrmEnTimeTable" Visible="False">
                                                    <asp:TableCell CssClass="label"><%=WrmEnTime%></asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:DropDownList ID="WrmEnTimeHr" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WrmEnTimeMin" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>
                                                        <span id="">: </span>
                                                        <asp:DropDownList ID="WrmEnTimeSec" runat="server" MaxLength="2" Width="65px"></asp:DropDownList>

                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="label" ForeColor="#CC0000"><%=FailInformationWrmEnTimeTable%></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            <asp:Table ID="Table2" runat="server" border="0" CssClass="DataList" Width="1143px"></asp:Table>
                                        </div>
                                    </div>
                                </asp:Panel>
                            <%--</div>--%>
                        <%--</div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
$(document).ready(function () {
    // table list mousee change
	$(".DataList tr").mouseenter(function () {
	    $(this).addClass('focus_tb');
	    if ($(this).attr('class') == 'odd') {
	        $(this).attr('class', 'focus_odd');
	    }
	        
	}).mouseleave(function () {
	    $(this).removeClass('focus_tb');
	    if ($(this).attr('class') == 'focus_odd') {
	        $(this).attr('class', 'odd');
	    }
	});
});
</script>
</html >