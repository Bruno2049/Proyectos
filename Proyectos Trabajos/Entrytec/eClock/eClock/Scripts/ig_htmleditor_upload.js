/*
* ig_htmleditor_upload.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


//vs 071210
var iged_obj=null;
var iged_update=false;
var iged_loadTime=0;
var iged_src=null;
var iged_width=null,iged_height=null,iged_ratio=null;
function iged_el(id){return document.getElementById(id);}
function iged_loadImg(src)
{
	if(!src)
	{
		if((iged_obj=opener.iged_all._curImg)==null)return;
		iged_src=iged_obj.src;
		iged_update=true;
	}
	else
	{
		iged_obj=document.createElement("IMG");
		src=opener.iged_replaceS(src,"//","/");
		iged_src=iged_obj.src=src;
	}
	setTimeout("iged_showImg()",100);
}
function iged_showImg()
{
	if(!iged_obj)return;
	iged_loadTime+=100;
	if(iged_obj.complete||iged_loadTime>1000)
	{
		iged_writeStr(iged_imgStr());
		iged_imgFields();
	}
	else setTimeout("iged_showImg()",100);
}
function iged_imgStr()
{
	var img=iged_obj;
	if(!img)return null;
	var s='<IMG src="'+(iged_src?iged_src:img.src)+'"';
	var v=img.alt;if(v!=''&&v)s+=' alt="'+v+'"';
	v=img.height;if(v!=''&&v)s+=' height="'+v+'"';
	v=img.width;if(v!=''&&v)s+=' width="'+v+'"';
	v=img.border;if(v!=''&&v)s+=' border="'+v+'"';
	v=img.align;if(v!=''&&v)s+=' align="'+v+'"';
	return s+" />";
}
function iged_updateImg(noFrame)
{
	var v=iged_el("imageUrl").value;
	if(v=="")return null;
	var img=document.createElement("IMG");
	img.src=iged_src=v;
	img.alt=iged_el("imageAltText").value;
	var h=iged_el("imageHeight").value;
	v=iged_el("imageWidth").value;
	var p=iged_el("proportion").checked;
	if(p&&!iged_ratio)
	{
		iged_height=parseInt(iged_height);
		iged_width=parseInt(iged_width);
		iged_ratio=iged_width/iged_height;
	}
	if(noFrame==2&&p)
	{
		if(iged_width)
		{
			h=iged_el("imageHeight").value=iged_height;
			v=iged_el("imageWidth").value=iged_width;
		}
		else v=h='';
	}
	else if(p)
	{
		h=h?parseInt(h):null;
		v=v?parseInt(v):null;
		if(iged_width!=v)
			iged_el('imageHeight').value=h=v?Math.floor(v/iged_ratio):'';
		else if(iged_height!=h)
			iged_el('imageWidth').value=v=h?Math.floor(h*iged_ratio):'';
		iged_width=v?v:null;
		iged_height=h?h:null;
	}
	img.height=h?h:'';
	img.width=v?v:'';
	v=iged_el("imageBorder").value;if(v!="none")img.border=v;
	v=iged_el("imageAlign").value;if(v!="none")img.align=v;
	iged_obj=img;
	img=iged_imgStr();
	if(noFrame!==true)iged_writeStr(img);
	return img;
}
function iged_imgFields()
{
	var i,img=iged_obj;
	if(!img)return;
	iged_ratio=null;
	iged_el("imageUrl").value=img.src;
	iged_el("imageHeight").value=iged_height=img.height;
	iged_el("imageWidth").value=iged_width=img.width;
	iged_el("imageAltText").value=img.alt;
	var o=iged_el("imageBorder").options;
	i=o.length;
	while(--i>0)if(o[i].value==img.border)break;
	o[i].selected=true;
	o=iged_el("imageAlign").options;
	i=o.length;
	while(--i>0)if(o[i].value==img.align)break;
	o[i].selected=true;
}
function iged_insImg()
{
	var txt=iged_updateImg(true);
	if(!txt||txt==""){window.close();return;}
	var img=iged_obj,o=opener.iged_all._cur;
	if(iged_update)
	{
		if(document.all)opener.iged_doImgUpdate(txt);
		else opener.iged_doImgUpdate(img);
	}
	else
	{
		if(document.all)opener.iged_insText(txt,false,false);
		else
		{
			if(opener.iged_replaceS(opener.iged_getEditTxt(),"\r\n","")=="<br>"&&o)o.setText("<p>");
			opener.iged_insNodeAtSel(img);
		}
	}
	window.close();
}
function iged_postUpload(arg)
{
	var e=iged_el("iged_upload_dir_field");
	if(!e)return;
	e.value=arg;
	e.form.submit();
}
function iged_loadFlash(src)
{
	var v=iged_el("flashSource"),fl=document.createElement("EMBED");
	if(!src)src=v.value;
	fl.src=iged_src=src;
	fl.type="application/x-shockwave-flash";
	v.value=src;
	v=iged_el("htmlAlign").value;if(v!=""&&v!="none")fl.align=v;
	v=iged_el("flashWidth").value;if(v!="")fl.width=v;
	v=iged_el("flashHeight").value;if(v!="")fl.height=v;
	v=iged_el("backgroundColor").value;if(v!="none")fl.bgColor=v;
	v=iged_el("flashAlign").value;if(v!=""&&v!="none")fl.salign=v;
	v=iged_el("flashQuality").value;if(v!="")fl.quality=v;
	v=document.forms[0].radLoop;
	if(v&&v[0])v=v[0].checked;else v=false;
	fl.loop=v;
	v=document.forms[0].radMenu;
	if(v&&v[0])v=v[0].checked;else v=false;
	fl.menu=v;
	fl.setAttribute("pluginspage", "http://www.macromedia.com/go/getflashplayer");
	iged_obj=fl;
	iged_writeStr(iged_flashStr());
}
function iged_insFlash()
{
	var v=iged_el("backgroundColor");
	if(v&&v.value)iged_loadFlash();
	if(iged_obj)
	{
		if(document.all)opener.iged_insText(iged_obj.outerHTML,false,false);
		else opener.iged_insNodeAtSel(iged_obj);
	}
	window.close();
}
function iged_flashStr()
{
	var fl=iged_obj;
	if(!fl)return "";
	var s='<EMBED pluginspage="http://www.macromedia.com/go/getflashplayer"';
	var v=fl.align;if(v!=''&&v)s+=' align="'+v+'"';
	v=iged_src;if(!v)v=fl.src;if(v!=''&&v)s+=' src="'+v+'"';
	v=fl.width;if(v!=''&&v)s+=' width="'+v+'"';
	v=fl.height;if(v!=''&&v)s+=' height="'+v+'"';
	s+=' type="application/x-shockwave-flash"';
	v=fl.menu;if(v!=''&&v)s+=' menu="'+v+'"';
	v=fl.loop;if(v!=''&&v)s+=' loop="'+v+'"';
	v=fl.quality;if(v!=''&&v)s+=' quality="'+v+'"';
	v=fl.salign;if(v!=''&&v)s+=' salign="'+v+'"';
	v=fl.bgcolor;if(v!=''&&v)s+=' bgcolor="'+v+'"';
	s+="/>";
	return s;
}
function iged_loadMedia(src)
{
	var v=iged_el("mediaSource"),md=document.createElement("EMBED");
	if(!src)src=v.value;
	md.src=iged_src=src;
	md.type="application/x-mplayer2";
	v.value=src;
	v=iged_el("mediaAlign").value;if(v!=""&&v!="none")md.align=v;
	v=iged_el("mediaWidth").value;if(v!="")md.width=v;
	v=iged_el("mediaHeight").value;if(v!="")md.height=v;
	v=iged_el("mediaStart").checked;md.autoStart=v?"true":"false";
	iged_obj=md;
	iged_writeStr(iged_mediaStr());
}
function iged_insMedia()
{
	iged_loadMedia();
	if(document.all)opener.iged_insText(iged_obj.outerHTML,false,false);
	else opener.iged_insNodeAtSel(iged_obj);
	window.close();
}
function iged_mediaStr()
{
	var md=iged_obj;
	if(!md)return "";
	var s='<EMBED type="application/x-mplayer2"';
	var v=md.align;if(v!=''&&v)s+=' align="'+v+'"';
	v=iged_src;if(!v)v=md.src;if(v!=''&&v)s+=' src="'+v+'"';
	v=md.width;if(v!=''&&v)s+=' width="'+v+'"';
	v=md.height;if(v!=''&&v)s+=' height="'+v+'"';
	v=md.autoStart;if(v!=''&&v)s+=' AUTOSTART="'+v+'"';
	return s+" />";
}
function iged_writeStr(s)
{
	var d=null,e=iged_el("iged_preview");
	if(e)if((d=e.contentDocument)==null)if((e=e.contentWindow)!=null)d=e.document;
	if(!d)return;
	d.clear();d.open();d.write(s);d.close();
}
function iged_loadFile(src){var e=iged_el("iged_preview");if(e)e.src=src;}
try{opener.iged_closePop();}catch(i){}
