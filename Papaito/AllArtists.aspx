<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllArtists.aspx.cs" Inherits="_AllArtists" %>
<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
    <link href="StyleSheet2.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form runat="server">
<div class="mainPublish">
    <div class="spaceArtist">
    </div>
    <div class="publish">
    <h1>Galleries</h1>
    <div id="main" class="menuPublish" runat="server">
<%--        <a id="ps1" class="set" href="#">Artists 1</a>
        <a id="ps2" class="set" href="#">Artists 2</a>
        <a id="ps3" class="set" href="#">Artists 3</a>
        <a id="ps4" class="set" href="#">Artists 4</a> 
        <a id="ps5" class="set" href="#">Artists 5</a> 
        <a id="ps6" class="set" href="#">Artists 6</a>--%>
        </div>
        <ul id="topic" class="topic" runat="server">
        </ul>
    </div>
    </div>
    </form>
</body>
</html>
