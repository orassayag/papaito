<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreLoad.aspx.cs" Inherits="PreLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>

    <script src="jquery.js" type="text/javascript"></script>
    <script src="scripts.js" type="text/javascript"></script>
    <link href="templatemo_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(document).ready(
        function() {
        window.setTimeout(function() {
            $('#Lang').fadeIn();
        },14000);
      
        $('#Lang').hide();
        
    });
    
        
    </script>
    <title></title>
</head>
<body style="background:#000000; width:800px;">
    <div>
    <embed src="PreLoad/Flash/newP.swf" style="margin:100px 0 0 475px"; quality="best" bgcolor="#000000" loop="false" width="300" height="300" name="p" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer" />
    </div>
    
    <div id="Lang" style="margin-left:450px;">
    <tr>
    <td>
    nbsp;
    <a href="Home.aspx?Language=En">
    <img src="PreLoad/usa.png" alt="" /></a>
    </td>
    
    <td>
    nbsp;nbsp;nbsp;
    <a  href="Home.aspx?Language=He">
    <img src="PreLoad/israel.jpg" alt="" />
    </a>
    </td>
    </tr>
    </div>
</body>
</html>
