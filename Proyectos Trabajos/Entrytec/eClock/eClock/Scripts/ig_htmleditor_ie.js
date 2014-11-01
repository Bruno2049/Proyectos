/*
* ig_htmleditor_ie.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


//vs 010312
function iged_init(ids,p1,p2,p3,p4,p5)
{
	ids=ids.split("|");
	var id=ids[0];
	var elem=iged_el(ids[1]),ta=iged_el(id+"_t_a"),tb=iged_el(ids[3]);
	if(!ta)return;
	var o=new iged_new(id,ta,tb,p1,p2,p3,p4,p5);
	o._ids=[ids[4]+"_d",ids[4]+"_h"];
	o._elem=elem;
	o._ie=function()
	{
		if(this._wSet||!this._elem0)return;
		var e=this._elem.parentNode;
		var w=this._elem0.style.width;
		if(!w)w='100%';
		if(w.indexOf('px')>0)
		{
			w=e?e.offsetWidth:0;
			if(w<10)return;
			e.style.width=(w+='px');
		}
		this._wSet=w;
	}
	o._doc=function(){return this._elem.document;}
	o._sel=function(){return this._doc().selection;}
	o._range=function(){return this._sel().createRange();}
	o.getText=function(){return this._html?this._elem.innerText:this._elem.innerHTML;}
	o._onLink=function()
	{
		var img=this._getSelImg();
		if(img)img.border=0;
		this._format("CreateLink");
	}
	o.print=function()
	{
		var s=this._elem.outerHTML;
		var i=s.indexOf(" style=");
		if(i>3)
		{
			var s0=s.substring(i+7,i+8);
			s=s.substring(i+8);
			s=s.substring(0,s.indexOf(s0));
		}
		var win=window.open("","","width=10,height=10");
		win.document.write("<html><body style='"+s+"'>"+this._elem.innerHTML+"</body></html>");
		win.document.close();
		win.print();
		win.close();
	}
	o._inTbl=function()
	{
		var cr=iged_all._curRange=this._range();
		if(!cr||this._sel().type=="Control"||!cr.parentElement)return false;
		return this._getTag(cr.parentElement(),'TD','TH');
	}
	o._insBook=function(n){iged_insText("<a name='"+n+"' />",false,true,true);}
	o._insRule=function(align,w,clr,size,noShad)
	{
		var t="<hr";
		if(align&&align!="default")t+=" align='"+align+"'";
		if(w&&w!="")t+=" width='"+w+"'";
		if(clr&&clr!="")t+=" color='"+clr+"'";
		if(size&&size!="")t+=" size='"+size+"'";
		if(noShad)t+=" NOSHADE";
		t+=" />";
		iged_insText(t,false,true,true);
	}
	o._onApplyStyle=function(e)
	{
		var str=e.id,cr=iged_all._curRange;
		if(!cr||!cr.select)return;
		var txt=cr.htmlText;
		if(!txt||txt.indexOf("hidden")>0)txt="";
		if(txt=="")
		{
			cr.expand("word");
			txt=cr.htmlText;
		}
		if(txt.indexOf("hidden")>-1)txt="";
		if(str.indexOf(":")>1)str="<font style='"+str+"'>"+txt+"</font>";
		else if(str.length>1)str="<font class='"+str+"'>"+txt+"</font>"
		cr.select();
		this._doc().execCommand("removeFormat");
		if(str.length>1)iged_insText(str,false,true);
		this._afterSel(e);
	}
	o._popWin=function(cap,x,img,evt)
	{
		x=this._fixDlgS(x);
		var id=x[0],h=x[14],w=x[15],flag=x[16];
		if(id.length<1)return;
		if(flag=="t")if((iged_all._curTable=this._getCurTable())==null)return;
		if(flag=="c")if((iged_all._curCell=this._getCurCell())==null)return;
		if(this._isKnown(id))iged_all._curRange=this._range();
		iged_closePop();
		var elem=iged_el(id,1),pan=this._pan();
		if(!elem||!pan)return;
		this._choiceAct=this._itemStyle=null;
		if(!elem._igf){elem._igf=true;this._fixPop(elem);}
		if(x[1])pan.className=x[1];
		var s=pan.style;
		if(x[2])s.backgroundColor=x[2];if(x[3])s.borderColor=x[3];
		if(x[4])s.borderStyle=x[4];if(x[5])s.borderWidth=x[5];
		if(x[6])s.fontFamily=x[6];if(x[7])s.fontSize=x[7];if(x[8])s.color=x[8];
		if(h)s.height=h;if(w)s.width=w;
		var tbl=document.createElement("TABLE");
		tbl.border=tbl.cellPadding=tbl.cellSpacing=0;
		tbl.insertRow();tbl.insertRow();
		if(w)tbl.style.width=w;
		var r0=tbl.rows[0];
		r0.insertCell();
		tbl.rows[1].insertCell();
		var c0=r0.cells[0];var s0=c0.style;
		s0.width="100%";s0.cursor="pointer";
		c0.id="titleBar";
		if(x[10])s0.backgroundColor=x[10];
		var txt="<table border='0' cellpadding='2' cellspacing='0' width='100%'><tr><td id='titleBar' width='100%'>";
		if(img!="")txt+="<img id='titleBar' align='absmiddle' src='"+img+"'></img>";
		txt+="&nbsp;<b id='titleBar' style='";
		if(x[11])txt+="font-family:"+x[11]+";";
		if(x[12])txt+="font-size:"+x[12]+";";
		if(x[13])txt+="color:"+x[13]+";";
		txt+="'>"+cap+"</b></td><td>";
		if(x[9])txt+="<img onclick='iged_closePop(3)' align='absmiddle' src='"+x[9]+"'></img>";
		txt+="</td></tr></table>";
		c0.innerHTML=txt;
		tbl.rows[1].cells[0].innerHTML=elem.innerHTML;
		pan.innerHTML=tbl.outerHTML;
		iged_all._pop=pan;
		iged_all._popID=this.ID;
		this._pos(evt,pan,9);
		this._delay();
		if(flag=="c")iged_loadCell();
		if(flag=="t")iged_loadTable();
		if(!pan._mde){this._addLsnr(pan,"mousedown",iged_dragEvt);pan._mde=true;}
	}
	o._decode=function(t){return t.replace(/<!--###@@@/gi, "<").replace(/@@@###-->/gi, ">");}
	o._encode=function(txt)
	{
		var exp=/<form(.*?)>/i;
		do{txt=txt.replace(exp,"@@@&&&###").replace(/@@@&&&###/,"<!--###@@@form"+RegExp.$1+"@@@###-->");}
		while(exp.test(txt));
		txt=txt.replace(/<\/form>/gi,"<!--###@@@/form@@@###-->");
		if(txt.indexOf("<!--###@@@")==0)txt="&nbsp;"+txt;
		return txt;
	}
	o._showHtml=function(html)
	{
		var o=this,e=this._elem;
		if(e._html!=null)o._html=e._html;
		if(html!==true&&html!==false)html=o._html!=true;
		if(html==(o._html==true))return;
		e._html=html;
		var i,tb=o._tb;
		o._swapS(iged_el(o._ids[html?0:1]),iged_el(o._ids[html?1:0]));
		if(html)
		{
			iged_all._noUndo=true;
			if(tb)for(i=0;i<tb.all.length;i++)tb.all[i].style.visibility="hidden";
			o._html=true;
			o.setText(o._amp(o._elem.innerHTML),1);
			o.focus();
			return;
		}
		if(tb)for(i=0;i<tb.all.length;i++)tb.all[i].style.visibility="visible";
		o._html=false;
		o.setText(o._elem.innerText,1);
		o.focus();
		o._syncBullets();
	}
	
	o._find0=function(x)
	{
		var txt=iged_el('iged_f_fw').value,txt2=iged_el('iged_f_rw').value||'';
		if(!txt)return;
		var flag=(iged_el('iged_f_mc').checked?4:0)^(iged_el('iged_f_mw').checked?2:0);
		if(x==1)return this._find(txt,flag);
		this._findOld=null;
		this._findNum=0;
		while(this._find(txt,flag,txt2))if(x==2)return;
	}
	o._find=function(txt,flag,txt2)
	{
		this._delay();
		var s,old=this._findOld==txt,fr=this._findRange;
		if(txt2!=null&&fr)fr.text=txt2;
		this._findOld=txt;
		if(!fr)
		{
			fr=this._findRange=this._body().createTextRange();
			fr.moveToElementText(this._elem);
			this._findLen=fr.text.length;
		}
		else fr.collapse(false);
		if(fr.findText(txt,this._findLen,flag))try
		{
			s=fr.parentElement();
			while(s)
			{
				if(s==this._elem){fr.select();this._findNum++;return true;}
				s=s.parentNode;
			}
		}catch(s){}
		txt="\""+txt+"\"";s=15;
		if(!old)this._findOld=null;
		else if(txt2==null)s=16;
		else{s=17;txt=this._findNum;}
		alert(this._cs(s).replace('{0}',txt));
		fr.collapse(false);
		this._findRange=null;
		return false;
	}
	o._txt=function(txt,paste)
	{
		if(!paste)txt=this._encode(txt);
		var i=0,css=new Array();
		while(true)
		{
			var i0=txt.indexOf('<style ',i),i1=txt.indexOf('<style>',i),i2=txt.indexOf('<STYLE ',i),i3=txt.indexOf('<STYLE>',i);
			if(i>i0||(i1>=i&&i0>i1))i0=i1;if(i>i0||(i2>=i&&i0>i2))i0=i2;if(i>i0||(i3>=i&&i0>i3))i0=i3;
			i1=txt.indexOf('>',i0);
			if(i>i0||i0>i1)break;
			i2=txt.indexOf('</style>',i0);i3=txt.indexOf('</STYLE>',i0);
			if(i1>i2||(i3>i1&&i2>i3))i2=i3;
			if(i1>i2)break;
			css[css.length]=txt.substring(i1+1,i2);
			txt=txt.substring(0,i0)+txt.substring(i2+8);
			i=i0;
		}
		var x,ii,e=this._elem,vv=new Array(),n="<a ",nx=" href",q="_$$_",t0=txt,t=txt.toLowerCase();
		while(true)
		{
			i=t.length-1;
			while(i>2)
			{
				if((ii=t.lastIndexOf(n,i))>=0)if((x=this._attrVal(t,ii+1,nx,t0))!=null)
				{
					txt=txt.substring(0,ii+n.length)+q+"='"+vv.length+"' "+txt.substring(ii+n.length);
					vv[vv.length]=x;
				}
				i=ii-1;
			}
			if(n.length>3)break;
			n="<img ";nx=" src";t0=txt;t=txt.toLowerCase();
		}
		if(paste)try{this._range().pasteHTML(txt);}catch(x){return;}
		else e.innerHTML=txt;
		x="A";
		while(true)
		{
			n=e.getElementsByTagName(x);
			for(i=0;i<n.length;i++)
			{
				if((nx=n[i].getAttribute(q))==null)continue;
				try{if((nx=vv[parseInt(nx)])!=null)if(x.length==1){if(n[i].href!=nx)n[i].href=nx;}else if(n[i].src!=nx)n[i].src=nx;}
				catch(ii){}
				n[i].removeAttribute(q);
			}
			if(x.length>1)break;
			x="IMG";
		}
		i=css.length;
		while(i-->0)try
		{
			var s=document.createElement('STYLE');
			e.insertBefore(s,e.firstChild);
			s.type='text/css';
			s.styleSheet.cssText=css[i];
		}catch(ii){}
	}
	o._attrVal=function(t,i0,a,t0)
	{
		var c,s=0,x=a.length,i=i0,i1=t.length;
		while(++i<i1)if(t.charCodeAt(i)==62)break;//62'>'
		if(i==i1)return null;
		t=t.substring(i0,i);t0=t0.substring(i0,i);
		i1=t.length;i=-1;
		while(++i<i1)
		{
			i0=t.indexOf(a,i);
			if(i0<0||i0+x+2>i1)return null;
			i=i0+x;
			while((c=t.charCodeAt(i))==32)if(++i>=i1)return null;
			if(c==61){i++;break;}//61'='
		}
		t=t0.substring(i);
		i1=t.length;i=0;
		while(i<i1&&(c=t.charCodeAt(i))==32)i++;
		if(c==39||c==34)i++;else c=32;//39''',34'"'
		i0=i;
		while(i<i1)
		{
			x=t.charCodeAt(i++);
			if(x==c)i1=--i;
			else if(s==0&&x==32)i0++;else s++;
		}
		if(i1!=i)return null;
		while(i>i0)if(t.charCodeAt(i-1)==32)i--;else break;
		return t.substring(i0,i);
	}	
	o._getSelElem=function()
	{
		if(this._sel().type=="Control")
			return this._range().item(0);
		iged_all._curRange=this._range();
		if(iged_all._curRange.boundingWidth>0)
		{
			var txt=iged_all._curRange.text;
			var id="pwCurrentlySelectedText";
			txt="<span id='"+id+"'>"+txt+"</span>";
			iged_all._curRange.pasteHTML(txt);
			var obj=this._doc().getElementById(id);
			obj.id="";
			return obj;
		}
		return null;
	}
	o._getCurCell=function()
	{
		return iged_all._curMenuCell?iged_all._curMenuCell:this._getTag(iged_all._curRange.parentElement(),'TD','TH');
	}
	o._getCurTable=function()
	{
		return iged_all._curMenuTable?iged_all._curMenuTable:this._getTag(iged_all._curRange.parentElement(),'TABLE');
	}
	o._getCurRow=function()
	{
		return iged_all._curMenuRow?iged_all._curMenuRow:this._getTag(iged_all._curRange.parentElement(),'TR');
	}
	o._insTbl=function()
	{
		var o=this;
		var iRows=iged_el("iged_tp_rr").value,iCols=iged_el("iged_tp_cc").value;
		var align=iged_el("iged_tp_al").value,cellPd=iged_el("iged_tp_cp").value;
		var cellSp=iged_el("iged_tp_cs").value,bdSize=iged_el("iged_tp_bds").value;
		var clrBg=iged_el("iged_tp_bk1").value,clrBd=iged_el("iged_tp_bd1").value,w=iged_el("iged_tp_w").value;
		var i,t,t0=iged_all._curTable;
		if(t0)t=t0;
		else
		{
			t=o._doc().createElement("TABLE");
			for(i=0;i<iRows;i++)
			{
				var r=t.insertRow();
				for(j=0;j<iCols;j++)r.insertCell();
			}
		}
		if(align!="default")t.align=align;
		t.border=bdSize;
		t.cellPadding=cellPd;
		t.cellSpacing=cellSp;
		if(clrBg&&clrBg!="")t.bgColor=clrBg;
		if(clrBd&&clrBd!="")t.borderColor=clrBd;
		t.width=w;
		if(t0)t0.outerHTML=t.outerHTML;
		else iged_insText(t.outerHTML,false,true,true);
		iged_all._curTable=null;
		iged_closePop();
	}
	o._delCol=function()
	{
		var c=this._getCurCell(),t=this._getCurTable();
		var i,ii=c.cellIndex;
		if(t&&c)for(i=0;i<t.rows.length;i++)if(t.rows[i].cells.length>ii)
			t.rows[i].deleteCell(ii);
		iged_closePop();
	}
	o._delRow=function()
	{
		var r=this._getCurRow(),t=this._getCurTable();
		if(t&&r)t.deleteRow(r.rowIndex);
		iged_closePop();
	}
	o._insRow=function(act)
	{
		var r=this._getCurRow(),t=this._getCurTable();
		iged_closePop();
		if(!t||!r)return;
		var i,r2=t.insertRow(r.rowIndex+((act=="below")?1:0));
		for(i=0;i<r.cells.length;i++)r2.insertCell(i);
	}
	o._insCol=function(act)
	{
		var c=this._getCurCell(),t=this._getCurTable();
		iged_closePop();
		if(!t||!c)return;
		var i,t2=t.cloneNode(true);
		for(i=0;i<t2.rows.length;i++)
			t2.rows[i].insertCell(c.cellIndex+((act=="right")?1:0));
		t.outerHTML=t2.outerHTML;
	}
	o._colSpan=function(act)
	{
		var c=this._getCurCell(),r=this._getCurRow();
		iged_closePop();
		if(!c||!r)return;
		if(act=="increase")
		{
			if(r.cells[c.cellIndex+1])
			{
				c.colSpan+=1;
				r.deleteCell(c.cellIndex+1);
			}
		}
		else if(c.colSpan>1) 
		{
			c.colSpan-=1;
			r.insertCell(c.cellIndex);
		}
	}
	o._rowSpan=function(act)
	{
		var c=this._getCurCell(),r=this._getCurRow(),t=this._getCurTable();
		iged_closePop();
		if(!c||!r)return;
		var nextRow=null;
		if(t.rows.length>r.rowIndex)nextRow=t.rows[r.rowIndex+c.rowSpan];
		if(act=="increase")
		{
			if(!nextRow)return;
			c.rowSpan+=1;
			nextRow.deleteCell(c.cellIndex);
		}
		else
		{
			if(c.rowSpan==1)return;
			c.rowSpan-=1;
			nextRow=t.rows[r.rowIndex+c.rowSpan];
			nextRow.insertCell(c.cellIndex);
		}
	}
	o._onKey=function(e)
	{
		var k=e.keyCode,p=(this._prop[12]&2)!=0;
		if(p&&e.shiftKey&&k==13)
		{
			iged_insText("<P />", false, false);
			iged_cancelEvt(e);
			return;
		}
		if(k==9){iged_insText("&nbsp;&nbsp; ", false, false);iged_cancelEvt(e);}
		if(k==13&&iged_all._needSync)window.setTimeout("iged_all._cur._syncBullets()", 100);
		if(k>=37&&k<=40)iged_all._curRange=this._body().createTextRange();
		if(!p)return;
		if(k==13)
		{
			this._syncBullets();
			var range=this._range();
			if(range.queryCommandState("insertorderedlist")||range.queryCommandState("insertunorderedlist"))
			{
				if(!iged_all._terminateList)iged_all._terminateList=true;
				else
				{
					e.keyCode=8;
					iged_all._terminateList=false;
				}
				return;
			}
			iged_insText("<BR />",false,false);
			iged_cancelEvt(e);
			range.select();
			range.collapse(false);
			range.select();
		}
		else iged_all._terminateList=false;
	}
	o.insertAtCaret=function(o)
	{if(o)iged_insText(o.outerHTML?o.outerHTML:''+o,null,null,null,this);}
	ta=o._fixStr(ta.value);
	var s=elem.style;
	var w=(ta.length>20)?elem.offsetWidth:0,sw=s.width;
	if(w>10)s.width=w+"px";
	o.setText(ta,1);
	if(w>10)s.width=sw;
	var edit=o._prop[12];
	if(!elem._oldE)
	{
		elem._oldE=true;
		o._addLsnr(iged_el(id),"mousedown",iged_mainEvt);
		if(edit>0)
		{
		o._ed0=true;
		o._addLsnr(elem,"focus",iged_mainEvt);
		o._addLsnr(elem,"blur",iged_mainEvt);
		elem.contentEditable=true;
		s.layout="layout-grid: both fixed 12px 12px";
		o._addLsnr(iged_el(id),"keydown",iged_mainEvt);
		o._addLsnr(elem,"keypress",iged_mainEvt);
		o._addMenu();
		}
	}
	o._ie();
	elem.content=false;
	if(o._ie7)
	{
		s.position='';
		var timer=setInterval(function()
		{
			sw=elem.parentNode;
			if(!sw||elem.offsetWidth>10)
			{
				clearInterval(timer);
				if(sw)s.position='relative';
			}
		}, 200);
	}
	if(o._prop[10]==1)o._showHtml(true);
	else if((edit&4)!=0)o.focus();
	o._fire(0);
}
function iged_fixValids()
{
	for(var i=0;i<document.all.length;i++)
	{
		var e=document.all[i];
		if(e.href&&e.href.indexOf("Page_ClientValidate()")>-1)
			e.href="javascript:iged_update(1);"+e.href;
		if(e.onclick&&e.onclick.toString().indexOf("Page_ClientValidate()")>-1)
		{
			var v=e.onclick.toString().replace("function anonymous()","").replace("{","").replace("}","");
			v=iged_replaceS(v,"\r","");
			iged_all._validFunc=iged_replaceS(v,"\n","");
			e.onclick=iged_doValidsSubmit;
		}
	}
}
function iged_doValidsSubmit(){iged_update(1);try{eval(iged_all._validFunc);}catch(i){}}
function iged_insText(txt,strip,restore,sel,a4)
{
	var o=a4?a4:iged_all._cur,cr=iged_all._curRange;
	if(!txt||txt==""||!o||!cr)return;
	var t=o.hasFocus()?o._elem.parentNode.scrollTop:0,h=o._elem.scrollHeight;
	if(!(cr.offsetLeft==12&&cr.offsetTop==17))if(restore)
	{
		if(cr.boundingWidth > 0||sel)cr.select();
		if(cr.boundingWidth==null)cr.select();
	}
	if(o._sel().type=="Control")o._sel().clear();
	iged_closePop(a4?null:"InsertText");
	o._elem.setActive();
	if(strip)txt=iged_stripTags(txt);
	o._txt(txt,true);
	o._mod=true;
	if(t>0)setTimeout('var o=iged_all._cur;if(o)o._elem.parentNode.scrollTop=Math.max('+t+'+o._elem.scrollHeight-'+h+',0)',0);
}
function iged_doImgUpdate(txt)
{
	var o=iged_all._cur;
	if(o&&iged_all._curRange)try
	{
		iged_all._curRange.select();
		o._sel().clear();
		iged_insText(txt,false,false);
		iged_all._curImg=null;
	}catch(o){}
}
function iged_modal(url,h,w,evt)
{
	var str="";
	url=iged_replaceS(url,"&amp;","&");
	if(h)str+="dialogHeight:"+h+";";
	if(w&&w!="500px")str+="dialogWidth:"+w+";";
	str+="dialogLeft:200;dialogTop:150;scroll:no;status:no;help:no;center:no;";
	
	url+=((url.indexOf("?")>-1)?"&num=":"?num=")+Math.round(Math.random()*100000000);
	url+="&parentId="+iged_all._cur._elem.id;
	return window.showModalDialog(url,window,str);
}
