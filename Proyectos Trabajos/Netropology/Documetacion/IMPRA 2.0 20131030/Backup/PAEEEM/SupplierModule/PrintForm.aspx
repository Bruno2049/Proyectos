<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintForm.aspx.cs" Inherits="PAEEEM.PrintForm" Title="SIP"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Imprimir Informe</title>
    <style type="text/css">
        #report
        {
            margin-left:1%;
            margin-right:1%;
            margin-bottom:2%;
            vertical-align:middle;            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <rsweb:ReportViewer ID="report" runat="server" ShowPrintButton="true"
            ShowRefreshButton="false" SizeToReportContent="true" AsyncRendering="false">
        </rsweb:ReportViewer>
     </div>
    </form>

<script language = "Javascript">
//javascript: get parameter from URL
// function getParameter(paraStr, url)
// {
// 	var result = ""; 
// 	//get all parameters from the URL
// 	var str = "&" + url.split("?")[1];
// 	var paraName = paraStr + "=";
// 	//check if the required parameter exist
// 	if(str.indexOf("&"+paraName)!=-1)
// 	{
// 		//if "&" is at the end of the required parameter
// 		if(str.substring(str.indexOf(paraName),str.length).indexOf("&")!=-1)
// 		{
// 			//get the end string
// 			var TmpStr=str.substring(str.indexOf(paraName),str.length);
// 			//get the value.
// 			result=unescape(TmpStr.substr(TmpStr.indexOf(paraName) + paraName.length,TmpStr.indexOf("&")-TmpStr.indexOf(paraName) - 
// 						paraName.length));   
// 		} 
// 		else
// 		{   
// 			result=unescape(str.substring(str.indexOf(paraName) + paraName.length,str.length)); 
// 		}
// 	}   
// 	else
// 	{   
// 		result="Null";   
// 	}   
// 	return (result.replace("&",""));   
// }
// 
var timer2;
var dueTime2=0
// function RemoveCTLExportFormats(format)
// {              
// 	dueTime2 += 50;
// 	if(dueTime2 > 30000)
// 	{
// 		clearTimeout(timer2);
// 		return;
// 	}
// 
// 	var obj=document.getElementsByTagName("Select");
// 	for(var i=0;i<obj.length;i++)
// 	{
// 		if (obj[i].title == "Export Formats")
// 		{
// 			var k = -1;
// 			for(var j = 0; j < obj[i].length; j ++)
// 			{
// 				if(obj[i].options[j].value.toLowerCase() == format.toLowerCase())
// 				{
// 					k = j;      
// 					obj[i].options.remove(k);
// 					clearTimeout(timer2);   
// 					return;                                                                  
// 				}
// 			}
// 
// 
// 		}
// 	}
// 	timer2=setTimeout("RemoveCTLExportFormats('" + format + "')",50);
// 
// }
// 
// function RemoveOption(report, format)
// {
// 	if(getParameter("ItemPath", location.href).toLowerCase() == report.toLowerCase())
// 	{
// 		timer2=setTimeout("RemoveCTLExportFormats('" + format + "')",50);
// 	}
// 	else
// 	{
// 	return;
// 	}
// }
// 
// RemoveOption("/PAEEEM_F2PROD/Solicitud de Credito", "Excel");
// RemoveOption("/PAEEEM_F2PROD/Solicitud de Credito", "XML");
// RemoveOption("/PAEEEM_F2PROD/Solicitud de Credito", "CSV");
// RemoveOption("/PAEEEM_F2PROD/Solicitud de Credito", "MHTML");
// RemoveOption("/PAEEEM_F2PROD/Solicitud de Credito", "IMAGE");
// RemoveOption("/PAEEEM_F2PROD/Solicitud de Credito", "WORD");
function RemoveCTLExportFormatsAllBut(format)
{              
	dueTime2 += 50;
	if(dueTime2 > 30000)
	{
		return;
	}

	var obj=document.getElementsByTagName("Select");
	for(var i=0;i<obj.length;i++)
	{
		if (obj[i].title == "Export Formats")
		{
			for(var j = obj[i].length - 1; j > 0; j--)
			{
				if(obj[i].options[j].value.toLowerCase() != format.toLowerCase())
				{
					obj[i].options.remove(j);
				}
            }
            if (obj[i].length > 1) {
                obj[i].selectedIndex = 1;
                obj[i].onchange();
            }
			return;
		}
	}
	// try again if not found yet
	timer2=setTimeout("RemoveCTLExportFormatsAllBut('" + format + "')",50);
}
timer2=setTimeout("RemoveCTLExportFormatsAllBut('PDF')",50);
</script>
</body>
</html>
