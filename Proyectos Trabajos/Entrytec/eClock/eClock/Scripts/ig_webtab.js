/*
* ig_webtab.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


//vs 062311
if(typeof igtab_all!='object')
	var igtab_all=new Object();
function igtab_getTabById(id)
{
	var o=igtab_all[id];
	if(o)return o;
	for(var i in igtab_all)if((o=igtab_all[i])!=null)
		if(o.ID==id||o.uniqueId==id)return o;
	return null;
}
function igtab_getElementById(id,owner)
{
	if(!owner)owner=window.document;
	var ch,e=null,ids=(id.charAt==null)?id:id.split(" ");
	if(ids.length==1&&owner.forms)e=document.getElementById(id);
	else
	{
		var nodes=owner.childNodes;
		var i=nodes.length;
		while(i-->0)
		{
			e=nodes[i];
			var i0=0,j=ids.length,id=e.id;
			if(!id)id=e.name;
			if(id&&id.length>0)
			{
				while(j-->0)
				{
					if(ids[j].length<1)continue;
					if((i0=id.indexOf(ids[j],i0))<0)break;
					if(i0>0){ch=id.charAt(i0-1);if(ch!='_'&&ch!=':'&&ch!='$')break;}
					i0+=ids[j].length;
					if(i0<id.length){ch=id.charAt(i0);if(ch!='_'&&ch!=':'&&ch!='$')break;}
				}
				if(j<0&&i0==id.length)break;
			}
			if((e=igtab_getElementById(ids,e))!=null)i=0;
		}
	}
	return e;
}
function igtab_init(id,index,prop,evts)
{
	var old=-2,o=igtab_all[id],e0=(typeof document=='unknown')?2:document.getElementById(id);
	
	if(o&&o.element)
	{
		if(e0==o.elemState)old=o.selected;
		o.rows=o._ajax=o.Event=null;
		var t,i=o.Tabs.length;
		if(index!=-7)while(i-->0)if((t=o.Tabs[i])!=null){if(i==old)t.doState(0);t.owner=null;}
		t=o.buttons;
		i=t?t.length:0;
		while(i-->0)if(t[i])t[i].owner=null;
		if(e0)ig_dispose(o);
	}
	if(!prop||e0==2||(o=igtab_getElementById("igtab"+id))==null)return;
	ig_csom._postTab=false;
	igtab_all[id]=o=new igtab_new(o,id,prop,evts,e0);
	if(!ig_csom._tab_load){o._lsnr(window,"load");o._lsnr(window,"unload");o._lsnr(window,"resize");ig_csom._tab_load=true;}
	o.selected=old;
	igtab_selectTab(o,index);
	o.fireEvt(o,o.Events.initializeTabs);
	o._new=true;
}
function igtab_event(e)
{
	if(!e)e=window.event;
	var p=ig_csom._postTab;
	if(p&&p+500<(new Date()).getTime())p=ig_csom._postTab=false;
	if(!e||p)return;
	var i,b,z=igtab_all,o=e.target,type=0;
	if(e.type=="load"){for(o in z)if(z[o])z[o]._load();return;}
	if(e.type=="unload"){for(o in z)igtab_init(o,-7);return;}
	if(!o)if((o=e.srcElement)==null)o=this;
	if(e.type=="focus"){if((o=o.indexOwner)!=null)o.setState(2,e);return;}
	if(e.type=="keydown")
	{
		z=e.keyCode;
		if(!z||z==0)z=e.which;
		if((o=o.indexOwner)==null)return;
		o=o.owner;
		if(o.tabIndex<0||!o.enabled)return;
		i=o.selected;p=o.Tabs.length-1;
		switch(z)
		{
			case 36:i=0;type=p;break;
			case 35:i=p;break;
			case 37:i--;break;
			case 39:i++;type=p;break;
			default:return;
		}
		while(true)
		{
			if(i<0||i>p)return;
			z=o.Tabs[i];
			if(z.visible&&z.enabled)break;
			if(i==type)return;
			if(i<type)i++;else i--;
		}
		z.setState(2,e);
		return;
	}
	o=igtab_getTabFromElement(o);
	if(e.type=="resize")
	{
		if(o){if(o.scrolInit)o.scrolInit(e);}
		else for(o in z)if(z[o]&&z[o].scrolInit)z[o].scrolInit(e);
		return;
	}
	if(!o)return;
	var eX=e.clientX,eY=e.clientY,oo=z=o.owner;
	if(z)if((z=z._fe)==null&&(!ig_csom.IsIE||document.readyState=="complete"))try
	{
		oo.elemState.form.appendChild(z=oo._fe=document.createElement("INPUT"));
		z.title='';z=z.style;z.position="absolute";z.zIndex=-1;z.padding=z.width=z.height=z.border="0px";
	}catch(z){oo._fe=z=2;}
	var ee=(z==2)?null:z;
	if(e.type=="mouseover"||e.type=="mousemove")type=1;
	if(e.type=="mousedown")type=2;
	if(o.text!=null&&o.index!=oo.selected&&ee)try
	{
		if(type==1)
		{
			b=document.body;p=b.parentNode;
			if((i=b.scrollLeft)<(z=p.scrollLeft))i=z;
			ee.style.left=(eX+i+10)+"px";
			if((i=b.scrollTop)<(z=p.scrollTop))i=z;
			ee.style.top=(eY+i+5)+"px";
		}
		if(type==2)ee.focus();
	}catch(z){}
	if(type==2&&o.text!=null)return;
	if(e.type=="click"){type=2;oo.fireEvt(o,oo.Events.click,e);try{if(ee)ee.blur();}catch(z){}}
	if(e.type=="mouseup")type=4;
	if(type==2&&(e.button>1||e.ctrlKey||e.shiftKey||e.altKey))return;
	if(!oo)return;
	if(type==1)if(oo.scrolInit(""))return;
	if(!oo.enabled)return;
	
	if(o.text==null){oo.doBut(type,e,o);return;}
	if(type>0){o.setState(type,e);return;}
	z=o.element;
	var w=o.rect(z),h=o.rect(z,1),x=o.rect(z,2),y=o.rect(z,3);
	if(o.elemLeft)
	{
		if((z=o.rect(o.elemLeft,2))<x){w+=(x-z);x=z;}
		if((z=o.rect(o.elemLeft,3))<y){h+=(y-z);y=z;}
	}
	if(o.elemRight)
	{
		if(o.rect(o.elemRight,2)>x)w+=o.rect(o.elemRight);
		if(o.rect(o.elemRight,3)>y)h+=o.rect(o.elemRight,1);
	}
	z=o.element;
	while((z=z.offsetParent)!=null){x+=o.rect(z,2);y+=o.rect(z,3);}
	if(eX<x+3||eY<y+3||eX+3>x+w||eY+3>y+h)o.setState(0,e);
}
function igtab_new(elem,id,prop,evts,e0)
{
	if(ig_csom.isEmpty(prop))return;
	this.val=function(o,i,t){o=(!o||o.length<=i)?"":o[i];return t?ig_csom.replace(ig_csom.replace(o,"&quot;","\""),"&pipe;","|"):o;}
	this.vis=function(e,v){if(e){e.style.display=(v==null)?"none":v;e.style.visibility=(v==null)?"hidden":"visible";}}
	this._lsnr=function(e,n){ig_csom.addEventListener(e,n,igtab_event);}
	this.addLsnr=function(e,s)
	{
		if(!e)return;
		ig_csom.addEventListener(e,"select",ig_cancelEvent);
		ig_csom.addEventListener(e,"selectstart",ig_cancelEvent);
		if(s)return;
		this._lsnr(e,"mouseout");this._lsnr(e,"mouseover");
		this._lsnr(e,"mousemove");this._lsnr(e,"mousedown");this._lsnr(e,"click");
		if(this.tabIndex<0)e.unselectable="on";
	}
	var prop0=prop[0].split('|'),o=id.charCodeAt(0);
	this.ID=id;
	this.elemState=e0;
	this.element=elem;
	this.elemEmpty=igtab_getElementById(id+"_empty");
	this.selected=-2;
	this._lsnr(elem,"resize");
	elem.setAttribute("tabID",id);
	var css,j=0,i=-1,a=elem.getAttribute("acss"),xID=(o==95||o<65)?"x"+id:id;
	if(a&&a.length>5)
	{
		a=a.split('+');
		while(++j<4){a[j]+=' '+a[0];if(a[5])a[5+j]+=' '+a[5];}
	}
	else a=null;
	this._acss=a;
	while(++i<30)
	{
		if((elem=igtab_getElementById(id+"_r"+i))==null)break;
		if(!this.rows)this.rows=new Array();
		this.rows[i]=elem;
	}
	if(prop0.length<6)return;
	this.uniqueId=prop0[0];
	j=parseInt(prop0[1]);
	this.enabled=(j&1)!=0;
	if(!this.enabled)this.element.disabled=true;
	this.autoPost=(j&2)!=0;
	this.doPost=this.autoPost;
	this.loadAllUrls=(j&4)!=0;
	this.s508=(j&8)!=0;
	this._orient=prop0[3];
	
	var cb=this._cb=parseInt(prop0[2]);
	if(cb>0)this._ajax=ig_shared.getCBManager(this.elemState.form);
	this._pi=i={loc:ig_Location.AboveLeft};
	i.setImageUrl=function(v){this.url=v;}
	i.getImageUrl=function(){return this.url;}
	i.setTemplate=function(v){this.html=v;}
	i.getTemplate=function(){return this.html;}
	i.setLocation=function(v){this.loc=v;}
	i.getLocation=function(){return this.loc;}
	i.setRelativeContainer=function(v){this.rc=v;}
	i.getRelativeContainer=function(){return this.rc;}
	this.getProgressIndicator=function(){return this._pi;}
	this.fixPI=function(pi)
	{
		var p=this._pi;
		pi.setLocation(p.loc);
		if(p.url)pi.setImageUrl(p.url);
		else if(p.html)pi.setTemplate(p.html);
		pi._rc=p.rc;
	}
	this.doResponse=function(vals,man)
	{
		if(this._cbc=this.fireEvt(this,this.Events.beforeCBResponse))return;
		var t,v;
		for(var i=0;i+1<vals.length;i+=2)
		{
			if((v=vals[i])==null)break;
			if(v.length>0&&v.length<3)if((t=this.Tabs[parseInt(v)])!=null)
				man.setHtml(vals[i+1],t.elemDiv);
		}
	}
	this.getCBSubmitElems=function(old)
	{
		var cb=this._cb;
		if(cb)cb=(cb&15)>>2;
		if(!cb||cb<1)return null;
		var t,j=0,tt=new Array(),i=this.Tabs.length-1;
		if(cb==1)if((i=old)==null)i=this.selected;
		while(i>=0)
		{
			t=this.Tabs[i];
			if(t&&t._cb)tt[j++]=t.elemDiv;
			if(cb==1)break;
			i--;
		}
		tt[j]=this.elemState;
		return tt;
	}
	this.beforeCBSubmit=function(id){return this.fireEvt(this,this.Events.beforeCBSubmit,null,id);}
	this.afterCBResponse=function(){if(!this._cbc)return this.fireEvt(this,this.Events.afterCBResponse);}
	this.noHov=ig_csom.notEmpty(prop0[6]);
	j=parseInt(prop0[5]);
	if((j&1)!=0)
	{
		this._cssL=o=new Array();
		for(i=0;i<4;i++)o[i]=xID+'_'+i+'L';
		if((j&4)==0)o[3]=o[0];
		if(this.noHov)o[1]=o[0];
	}
	if((j&2)!=0)
	{
		this._cssR=o=new Array();
		for(i=0;i<4;i++)o[i]=a?(a[i]+' '+a[4]):(xID+'_'+i+'R');
		if((j&4)==0)o[3]=o[0];
		if(this.noHov)o[1]=o[0];
	}
	this.doCss=function(prop)
	{
		var j=-1,cs=new Array();
		if(ig_csom.isEmpty(prop))return cs;
		prop=prop.split("+");
		while(++j<4)if(ig_csom.isEmpty(cs[j]=prop[j]))cs[j]=cs[0];
		return cs;
	}
	this.dummy=this.val(prop0,7);
	this.tabIndex=this.val(prop0,8);
	if(prop0.length<12){this.top=this.val(prop0,9)==0;this.back=this.val(prop0,10);}
	else
	{
		this.butP=-1;this.butL=1;this.buttons=new Array(4);
		for(j=2;j<4;j++)if((e=document.getElementById(id+"more"+j))!=null)
		{
			this._lsnr(e,"mousedown");
			e.unselectable="on";this.addLsnr(e,true);
			e.setAttribute("tabID",id+",100"+j);
			e.owner=this;this.buttons[j]=e;
		}
		j=parseInt(this.val(prop0,9));
		this.delay=parseInt(this.val(prop0,10));
		this.scrolHide=ig_csom.notEmpty(prop0[11]);
		this.scrolToLast=ig_csom.notEmpty(prop0[12]);
		this.css=this.doCss(this.val(prop,13));
		for(i=0;i<4;i++)
		{
			if(a){this.css[i]=a[5+i];continue;}
			if((o=this.css[i])==null)o='';
			css=(j<1)?'':(xID+'_b'+(((j&(1<<i))!=0)?i:0));
			if(css.length>0&&o.length>0)css+=' ';
			if((this.css[i]=css+o)=='')this.css[i]=this.css[0];
		}
		for(j=0;j<2;j++)if((e=document.getElementById(id+"_b"+j))!=null)
		{
			this._lsnr(e,"mousedown");this._lsnr(e,"mouseup");
			this._lsnr(e,"mousemove");this._lsnr(e,"mouseout");
			e.unselectable="on";
			e.setAttribute("tabID",id+",100"+j);
			o=new Object();o.i=j;o.elem=e;o.img=new Array(4);
			css=this.val(prop0,14+j).split(";");
			for(i=0;i<4;i++)if((o.img[i]=this.val(css,i))=="")o.img[i]=o.img[0];
			o.state=0;o.owner=this;
			this.buttons[j]=o;
		}
	}
	css=this.doCss(prop0[4]);
	this.getUniqueId=function(){return this.uniqueId;}
	this.getEnabled=function(){return this.enabled;}
	this.setEnabled=function(v){this.setEnabled0(this,v);}
	this.setEnabled0=function(o,v)
	{
		if(o.enabled==(v==true))return;
		o.enabled=(v==true);
		var x=-1,owner=o.owner;
		if(!owner)owner=o;else x=o.index;
		owner.update(x,"Enabled",v);
		if(++x>0){o.setState(o.enabled?0:3);o.fixSel();return;}
		while(x<o.Tabs.length)o.Tabs[x++].doState(-1);
		o.element.disabled=v!=true;
		o.scrolW_All=null;
		o.scrolInit();
	}
	this.getAutoPostBack=function(){return this.autoPost;}
	this.setAutoPostBack=function(v){this.autoPost=(v==true);}
	this.getSelectedIndex=function(){return this.selected;}
	this.setSelectedIndex=function(v){igtab_selectTab(this,v);}
	this.getSelectedTab=function(){return (this.selected<0)?null:this.Tabs[this.selected];}
	this.setSelectedTab=function(v){igtab_selectTab(this,v?v.index:-1);}
	prop0=ig_csom.notEmpty(evts)?evts.split('|'):null;
	this.Events={afterSelectedTabChange:this.val(prop0,0,1),beforeSelectedTabChange:this.val(prop0,1,1),
		initializeTabs:this.val(prop0,2,1),mouseOut:this.val(prop0,3,1),mouseOver:this.val(prop0,4,1),
		click:this.val(prop0,5,1),beforeCBSubmit:this.val(prop0,6,1),
		beforeCBResponse:this.val(prop0,7,1),afterCBResponse:this.val(prop0,8,1)};
	this.Tabs=new Array();
	this.addLsnr(igtab_getElementById(id+"_tbl"),true);
	i=-1;
	while(++i<prop.length-1)
	{
		if((elem=igtab_getElementById(id+"td"+i))==null)continue;
		elem.setAttribute("tabID",id+","+i);
		var tab=new igtab_newT(id,prop[i+1].split('|'),i,css,this,elem,xID);
		if(tab._cb)
		{
			if((cb&1)!=0)this._ajax.addPanel(this,this.uniqueId,id+"_div"+i,((cb&16)==0)?false:elem);
			tab._cb=elem;
		}
		this.addLsnr(elem,false);
		if(this.tabIndex!=-1)for(j=0;j<6;j++)
		{
			if(elem.tabIndex==-3){this._lsnr(elem,"keydown");j=10;}
			if(ig_csom.notEmpty(elem.accessKey)){this._lsnr(elem,"focus");j=10;}
			if(j==10){elem.indexOwner=tab;tab.elemIndex=elem;}
			if((elem=elem.parentNode)==null)break;
			if(ig_csom.notEmpty(elem.id))break;
		}
		this.Tabs[i]=tab;
		if(ig_csom.notEmpty(tab.Key)&&isNaN(tab.Key))this.Tabs[tab.Key]=tab;
		if(this._cssL)if((elem=igtab_getElementById(id+"td"+i+"L"))!=null)
		{
			elem.setAttribute("tabID",id+","+i);
			tab.elemLeft=elem;
			this.addLsnr(elem,false);
		}
		if(this._cssR)if((elem=igtab_getElementById(id+"td"+i+"R"))!=null)
		{
			elem.setAttribute("tabID",id+","+i);
			tab.elemRight=elem;
			this.addLsnr(elem,false);
		}
	}
	this.tabFromKey=function(key){for(var i=0;i<this.Tabs.length;i++)if(this.Tabs[i].Key==key)return this.Tabs[i];return null;}
	this.findControl=function(id){var c,i=-1;while(this.Tabs[++i])if((c=this.Tabs[i].findControl(id))!=null)return c;}
	this._load=function()
	{
		this.scrolInit(2);
		var v=unescape(this.elemState.value);
		var i=v.indexOf("SelectedTab=\"");if(i<0)return;
		v=v.substring(i+13);
		if((i=v.indexOf("\""))>0)igtab_selectTab(this,parseInt(v.substring(0,i)));
	}
	this.update=function(i,pr,v,post)
	{
		var e=this.elemState;
		if(!e||!this._new)return;
		if(!this.viewState)this.viewState=new ig_xmlNode();
		var n=this.viewState.addNode("x",true);
		if(i>=0)n=n.addNode("Tabs",true).addNode("i"+i,true);
		
		n.setPropertyValue(pr,""+((post==1)?(v+10000):v));
		e.value=this.viewState.getText();
		if(post!=1)return;
		ig_csom._postTab=(new Date()).getTime();
		if((e=this.getSelectedTab())!=null)if((e=e.getTargetUrlDocument())!=null)try
		{
			if((e=e.forms)!=null)for(i=0;i<e.length;i++)
				if(ig_csom.IsIE)e[i].fireEvent("onsubmit");else e[i].submit();
		}catch(n){}
		try{if((e=document.activeElement)!=null)if(e.tagName=="INPUT")e.fireEvent("onblur");}catch(n){}
		try{__doPostBack(this.uniqueId,v);}catch(n){}
	}
	this.fireEvt=function(o,fn,e,p)
	{
		var owner=o.owner;
		if(!owner)owner=o;
		if(ig_csom.isEmpty(fn))return false;
		var evt=owner.Event;
		if(!evt)evt=owner.Event=new ig_EventObject();
		evt.cancelResponse=evt.fullPostBack=false;
		evt.reset();
		evt.event=e;
		ig_fireEvent(owner,fn,o,evt,p);
		owner.doPost=(evt.needPostBack==true)||(owner.autoPost&&evt.cancelPostBack==false);
		if(evt.cancelResponse)return 'cancelResponse';
		if(evt.fullPostBack)return 'fullPostBack';
		return evt.cancel;
	}
	
	this.moveRow=function(i)
	{
		var e1=this.rows[i],e0=this.rows[0];
		var t1=e1.firstChild,t0=e0.firstChild;
		if(!t1||!t0)return;
		e0.removeChild(t0);e1.removeChild(t1);
		this.fixTD(t1,"",true);
		this.fixTD(t0,"0px",i+1<this.rows.length);
		e1.appendChild(t0);e0.appendChild(t1);
	}
	this.fixTD=function(e,v,c)
	{
		if(ig_csom.notEmpty(e.nodeName)&&e.nodeName.toUpperCase()=="TD")
		{
			var s=e.style;
			if(e.id!=null&&s)
			{
				if(this.top)s.borderBottomWidth=v;else s.borderTopWidth=v;
				if(e.id!="edge")s.backgroundColor=c?this.back:"";
			}
		}
		if((e=e.childNodes)!=null)for(var i=0;i<e.length;i++)this.fixTD(e[i],v,c);
	}
	this.butState=function(b,s)
	{
		var e,i=-1,bb=this.buttons;
		if(bb)bb=bb[b];
		if(s<0)if(bb.state<3)return;else s=0;
		if(!bb||bb.state==s)return;
		var timer=(s==2)?2:((bb.state==2)?1:0);
		bb.state=s;
		if(ig_csom.notEmpty(e=this.val(this.css,s)))if(bb.elem.className!=e)bb.elem.className=e;
		if(ig_csom.isEmpty(s=bb.img[s]))s=bb.img[0];
		if(ig_csom.notEmpty(s))for(i=0;i<bb.elem.childNodes.length;i++)
		{
			e=bb.elem.childNodes[i];
			b=e.nodeName.toUpperCase();
			if(b=="IMG")if(e.src!=s)e.src=s;
		}
		if(timer>0)this.timer(this,timer==1);
	}
	
	this.doBut=function(type,e,b)
	{
		ig_cancelEvent(e);
		if(this.hover&&this.hover.state==1)this.hover.setState(0,e);
		var but=4;
		while(but-->0)if(this.buttons[but]==b)break;
		if(but<0)return;
		
		if(but>1){this.scroll(but==2);return;}
		if(type==4)this.butP=-1;
		else if(type!=2&&e.button!=0&&this.butP<0)return;
		if(!this.getEnabled()||b.state==3)return;
		if(type==0&&this.butL==0)this.butP=-1;
		b=this.butP;
		if(type==2)
		{
			if(b>=0)this.butState(b,0);
			this.butP=-2;
			if(e.button<2){this.butP=but;this.butL=e.button;this.butState(but,2);}
			return;
		}
		if(e.button==0&&b<-1)b=this.butP=-1;
		if(b>=0&&e.button!=this.butL&&type==1){b=this.butP=-1;this.butState(but,1);return;}
		if(b<-1||(b>=0&&b!=but))return;
		this.butState(but,(type==0)?0:((b>=0)?2:1));
	}
	this.scrolInit=function(e)
	{
		var w=-1,j=-1,i=this.Tabs.length;
		if(i<1||!this.butP||(e&&this.butP>=0)||(e==2&&this._scrolOk))return false;
		var t=this.Tabs[0];
		if(e==="")if(this.scrolW_Vis==t.rect(this.scrolDiv))return false;
		e=t.element;
		
		var div,tbl,tds=null;
		while(j++<15&&w<=0)
		{
			if(w<0)
			{
				if(e.tagName=="TABLE")tbl=e;
				if(e.tagName=="TR")tds=e.childNodes;
				if(e.tagName=="DIV")
				{
					if(t.rect(e,1)!=(w=t.rect(tbl,1)))e.style.height=w;
					w=t.rect(this.scrolDiv=div=e);
				}
			}
			
			else if((w=t.rect(e))>0)if(ig_csom.IsMac){div.style.width=w;if(t.rect(div,1)!=(j=t.rect(tbl,1)))div.style.height=j;}
			if((e=e.parentNode)==null)return false;
		}
		if(!tds||(this.scrolW_Vis==w&&this.scrolW_All!=null))return false;
		var my,tail,last=true,iTD=tds.length,i0=0,b=this.buttons[3];
		while(iTD-->1){e=tds[iTD];if(e.tagName=="TD"){this.scrolEnd=e;break;}}
		if(b)for(j=-1;j>-3;j--)if(tds[iTD+j]==b){this.vis(b);iTD+=j;break;}
		if(iTD>1&&(b=this.buttons[2])!=null)for(i0=3;i0>0;i0--)if(tds[i0-1]==b){this.vis(b);break;}
		if(iTD-i0<1)return false;
		this.vis(e);
		var old0=this.scrol0;
		if(!old0)old0=this.scrol0=0;
		for(j=i;j>0;)this.show(this.Tabs[--j],true);
		if(this.scrolW_All==null)old0=-1;
		this.scrolW_Vis=w;this.scrolW_All=t.rect(tbl);
		w=0;t=null;
		while(iTD-->i0)
		{
			if(t==null&&i>0)
			{
				while(i-->0){t=this.Tabs[i];t.scrolW=0;t.scrolX=-1;if(t.visible){if(old0<0)this.scrol0=i;break;}}
				if(i<0)break;
				j=0;tail=true;t.scrolTDs=new Array();
			}
			e=tds[iTD];
			if(e.tagName!="TD")continue;
			if(my=t.isMy(e))tail=false;
			
			else if(!tail&&i>0){last=false;t=null;iTD++;continue;}
			if(my||tail||i==0)
			{
				if(t.scrolX<0)t.scrolX=e.offsetLeft;
				
				if((j=e.offsetWidth)>0)t.scrolTDs[t.scrolTDs.length]=e;t.scrolW+=j;w+=j;
				
				t.scrolW_Right=last?0:(this.scrolToLast?9999:w);
			}
		}
		w=true;
		for(i=j=0;j<this.Tabs.length;j++)
		{
			t=this.Tabs[j];
			if(t.scrolW>0)
			{
				if(old0>0&&j<old0)this.show(t,false);
				t.scrolI=i++;
			}
			if(t.visible)w=false;
		}
		this.vis(this.scrolEnd,"");
		this.scrolFix(w);
		if(!this._scrolOk){this._scrolOk=true;this.ensureVisible();}
		return true;
	}
	this.timer=function(tab,end)
	{
		if(tab&&tab.Tabs)
		{
			if(ig_csom.tab_f)window.clearInterval(ig_csom.tab_f);
			ig_csom.tab_f=end?(tab=null):window.setInterval(tab.timer,this.delay);
			ig_csom.tab_o=tab;
		}
		else tab=ig_csom.tab_o;
		if(tab)tab.scroll();
	}
	this.ensureVisible=function()
	{
		var i0=this.selected,i=this.Tabs.length;
		if(this.scrol0==null||i0<0)return;
		while(this.scrol0>i0&&i-->0)this.scroll(true);
		if(this.scrol0>=i0)return;
		var t=this.Tabs[i0];
		var iR=t.scrolX+t.scrolW-this.scrolW_Vis;
		while(this.scrol0<i0&&i-->0)
			if(iR>this.Tabs[this.scrol0].scrolX||t.scrolHidden==true)this.scroll(false);
			else break;
	}
	this.scroll=function(left)
	{
		if(left==null)left=this.butP==0;
		if(this.scrolW_All==null)if(!this.scrolInit())return;
		var ii=this.Tabs.length-1,j=0,i=this.scrol0;
		if(i==null)return;
		var t=this.Tabs[i];
		while(true)
		{
			i+=left?-1:1;
			if(i<0||i>ii)return;
			if(this.Tabs[i].scrolW>0)break;
		}
		this.scrol0=i;
		this.show(left?this.Tabs[i]:t,left);
		this.scrolFix();
	}
	this.show=function(t,vis)
	{
		if(!t.visible||t.scrolW<1||vis==(t.scrolHidden!=true))return;
		for(var i=0;i<t.scrolTDs.length;i++)this.vis(t.scrolTDs[i],vis?"":null);
		t.scrolHidden=!vis;
	}
	
	this.scrolFix=function(non)
	{
		var t=this.scrol0;
		t=this.Tabs[t?t:0];
		if(!t)return;
		var e,w0=0,w=0,i=t.index,more=!non&&t.scrolI>0;
		this.vis(e=this.buttons[2],more?"":null);
		
		if(more)if((w0=t.rect(e))>80)w0=0;
		w+=w0;
		this.butState(0,(more&&this.enabled)?-1:3);
		more=!non&&t.scrolW_Right+w0>this.scrolW_Vis;
		this.butState(1,(more&&this.enabled)?-1:3);
		e=this.buttons[3];
		var vis=more&&this.scrolHide;
		
		if(vis)if((t=t.rect(e))<80)w+=t;
		more=!non;
		if(this.scrolHide)while(i<this.Tabs.length)
		{
			t=this.Tabs[i++];
			if((w0=t.scrolW)<1)continue;
			w+=w0;
			this.show(t,more||w<=this.scrolW_Vis);
			more=false;
		}
		if(vis)for(i=this.Tabs.length;--i>0;)if(this.Tabs[i].scrolW>0)
		{if(this.Tabs[i].scrolHidden!=true)vis=false;break;}
		this.vis(e,vis?"":null);
	}
	
	window.setTimeout('try{igtab_all["'+id+'"].scrolInit(2);}catch(e){}',1);
}
function igtab_newT(id,prop,i,css0,own,elem,xID)
{
	this.owner=own;
	this.element=elem;
	this.tooltip=elem.title;
	this.elemIframe=igtab_getElementById(id+"_frame"+i);
	this.index=i;
	this.visible=ig_csom.notEmpty(own.val(prop,0));
	var v,j=-1,o=own.val(prop,1);
	var st=ig_csom.notEmpty(o)?parseInt(o):0,a=elem.getAttribute("acss");
	this.enabled=(st&1)!=0;
	this._cb=(st&2)!=0;
	this.state=this.enabled?0:3;
	
	
	st=ig_csom.notEmpty(o=own.val(prop,2))?parseInt(o):0;
	if(a&&a.length>3)a=a.split('+');else a=null;
	var cssT=own.doCss(own.val(prop,3));
	this.css=new Array();
	while(++j<4)
	{
		if((o=own._acss)!=null)
		{
			st=o[j];if(a&&ig_csom.notEmpty(v=a[0]))st+=' '+v;
			if(j>0&&a&&ig_csom.notEmpty(v=a[j]))st+=' '+v;
			this.css[j]=st;
			continue;
		}
		o=xID+'_'+j;
		if((st&(1<<j))!=0)o+=i;
		else if(j==1&&own.noHov)o=a;
		a=o;
		if(ig_csom.notEmpty(v=own.val(cssT,j)))o+=' '+v;
		if(ig_csom.notEmpty(v=own.val(css0,j)))o+=' '+v;
		this.css[j]=o;
	}
	this.targetUrl=own.val(prop,4,1);
	o=igtab_getElementById(id+"_div"+i);
	this.elemDiv=o?o:own.elemEmpty;
	o=own.val(prop,5).split(";");
	this._fix=j=0;
	this.img=o=[own.val(o,0),own.val(o,1),own.val(o,2),own.val(o,3)];
	while(++j<4)if(o[j]&&o[j]!=o[0])this._fix=1;
	this.text=own.val(prop,6,1);
	this.selColor=own.val(prop,7);
	this.Key=own.val(prop,8);
	this.imgAlign=own.val(prop,9);
	this.getIndex=function(){return this.index;}
	this.getElement=function(){return this.element;}
	this.getOwner=function(){return this.owner;}
	this.getTargetUrlDocument=function()
	{
		var d,f=this.elemIframe;
		if(f==null)return null;
		try{if((d=f.contentWindow)!=null)if((d=d.document)!=null)return d;}catch(d){}
		try{if((d=f.contentDocument)!=null)return d;}catch(d){}
		return null;
	}
	this.getVisible=function(){return this.visible;}
	this.setVisible=function(v)
	{
		if(this.visible==(v==true))return;
		this.visible=v=(v==true);
		var o=this.owner;
		o.vis(this.element,v?"":null);o.vis(this.elemLeft,v?"":null);o.vis(this.elemRight,v?"":null);
		this.fixSel();
		o.update(this.index,"Visible",v);
		o.scrolW_Vis=null;o.scrolInit();
	}
	this.getEnabled=function(){return this.enabled;}
	this.setEnabled=function(v){this.owner.setEnabled0(this,v);}
	this.getText=function(){return this.text;}
	this.setText=function(v)
	{
		if(this.text==v||!ig_csom.isArray(v))return;
		var t=this.text=v;
		var e=this.element,n=e.childNodes;
		var i=(n==null)?0:n.length;
		while(i-->0)
		{
			if(t==v&&n[i].nodeName=="#text"){n[i].nodeValue=v;t=null;}
			else if(n[i].nodeName!="IMG")e.removeChild(n[i]);
		}
		if(t==v)e.innerHTML=" "+v+" ";
		this.fixImg(-1);
		this.owner.update(this.index,"Text",v);
	}
	this.getTooltip=function(){return this.tooltip;}
	this.setTooltip=function(v)
	{
		if(ig_csom.isArray(v))this.element.title=this.tooltip=v;
		this.owner.update(this.index,"Tooltip",v);
	}
	this.getTargetUrl=function(){return this.targetUrl;}
	this.setTargetUrl=function(v)
	{
		if(!v||v==this.owner.dummy)v="";
		if(this.targetUrl==v||!this.elemIframe)return;
		this.targetUrl=v;
		this.elemIframe.src=(v.length==0)?this.owner.dummy:v;
		if(this.state==2)if((v.length==0)!=(this.elemIframe.style.display=="none"))
		{
			this.owner.vis(this.elemDiv,(v.length==0)?"block":null);
			this.owner.vis(this.elemIframe,(v.length>0)?"block":null);
		}
		this.owner.update(this.index,"TargetUrl",v);
	}
	this.getDefaultImage=function(){return this.img[0];}
	this.setDefaultImage=function(v){this.doImg(v,0,"Default");}
	this.getHoverImage=function(){return this.img[1];}
	this.setHoverImage=function(v){this.doImg(v,1,"Hover");}
	this.getSelectedImage=function(){return this.img[2];}
	this.setSelectedImage=function(v){this.doImg(v,2,"Selected");}
	this.getDisabledImage=function(){return this.img[3];}
	this.setDisabledImage=function(v){this.doImg(v,3,"Disabled");}
	this.doImg=function(v,st,p)
	{
		if(!v)v="";if(this.img[st]==v)return;
		this.img[st]=v;
		this._fix=2;
		if(this.state==st)this.fixImg(-2);
		this.owner.update(this.index,p+"Image",v);
	}
	this.fixImg=function(st,s508)
	{
		if((this._fix<1&&!s508)||this.state==st)return;
		var c=this.element;
		var im,imgC=null,nodes=c.childNodes;
		if(!nodes)return;
		var i=nodes.length;
		while(i-->0)if(nodes[i].tagName=="IMG"){imgC=nodes[i];break;}
		if(imgC)
		{
			if(!this._alt)this._alt=imgC.alt;
			var alt=(s508&&st==2)?'Selected':this._alt;
			if(alt!=imgC.alt)imgC.alt=alt;
			if(this._fix<1)return;
		}
		this._fix=1;
		if(st<0||st>3)st=this.state;
		if(ig_csom.isEmpty(im=this.img[st]))im=this.img[0];
		if(ig_csom.notEmpty(im))
		{
			if(!imgC)
			{
				if((imgC=document.createElement("IMG"))!=null)
				{
					if((i=c.lastChild)!=null)c.removeChild(i);
					imgC=c.appendChild(imgC);
					if(i)c.appendChild(i);
				}
				if(imgC){imgC.border=0;if(ig_csom.notEmpty(this.imgAlign))imgC.align=this.imgAlign;}
			}
			if(imgC)imgC.src=im;
		}
		else if(imgC)c.removeChild(imgC);
	}
	this.fixSel=function()
	{
		if(this.visible&&this.enabled)return;
		if(this.owner.selected!=this.index)return;
		if(!this.visible){this.owner.vis(this.elemDiv);this.owner.vis(this.elemIframe);}
		var o,i=this.index;
		while(i-->0)
		{
			o=this.owner.Tabs[i];
			if(o.visible&&o.enabled){igtab_selectTab(o.owner,o.index,1);return;}
		}
		i=this.index;
		while(++i<this.owner.Tabs.length)
		{
			o=this.owner.Tabs[i];
			if(o.visible&&o.enabled){igtab_selectTab(o.owner,o.index,1);return;}
		}
		igtab_selectTab(this.owner,-1,1);
	}
	this.rect=function(o,s)
	{
		if(!o)s=0;else if(s==1)s=o.offsetHeight;
		else if(s==2)s=o.offsetLeft;else if(s==3)s=o.offsetTop;
		else s=o.offsetWidth;
		return s?s:0;
	}
	this.focus=function(){if(this.elemIndex)try{this.elemIndex.focus();}catch(i){};}
	this.setState=function(st,e)
	{
		if(st<0||st>3||st==this.state){if(st==2)this.focus();return;}
		var o=this.owner;
		if(e)
		{
			if(st==1)
			{
				if(o.hover==this)return;
				if(o.hover&&o.hover.state==1)o.hover.setState(0,e);
			}
			o.hover=(st==1||st==2)?this:null;
			if(this.state==3||!o.enabled)return;
		}
		if(st==2)
		{
			if(e)
			{
				o.doPost=o.autoPost;
				if(o.fireEvt(this,o.Events.beforeSelectedTabChange,e))return;
				if(o.doPost){o.update(-1,"SelectedTab",this.index,1);return;}
			}
			igtab_selectTab(o,this.index);
			if(e){o.fireEvt(this,o.Events.afterSelectedTabChange,e);this.focus();}
			return;
		}
		if(e&&st<2)
		{
			o.fireEvt(this,(st==0)?o.Events.mouseOut:o.Events.mouseOver,e);
			if(o.selected==this.index)return;
		}
		this.doState(st);
		this.state=st;
	}
	this.doState=function(st)
	{
		if(st<0)st=(this.owner.enabled||this.state==2)?this.state:3;
		this.fixImg(st,this.owner.s508);
		var e=this.element;
		if(e.className!=this.css[st])e.className=this.css[st];
		e=this.elemLeft;if(e&&e.className!=this.owner._cssL[st])e.className=this.owner._cssL[st];
		e=this.elemRight;if(e&&e.className!=this.owner._cssR[st])e.className=this.owner._cssR[st];
	}
	this.findControl=function(id)
	{
		var c=ig_csom.findControl(this.elemDiv,id);
		if(c==null)if((c=this.getTargetUrlDocument())!=null)c=ig_csom.findControl(c,id);
		return c;
	}
	this.isMy=function(n)
	{
		var e=this.element;
		if(e==n||this.elemLeft==n||this.elemRight==n)return true;
		for(var i=0;i<6;i++)
		{
			if((e=e.parentNode)==null)break;
			if(e==n)return true;
		}
		return false;
	}
	this._clr=function(e)
	{
		var b=this.owner._orient,c=this.selColor;
		if(e)e.style.backgroundColor=e.bgColor=c;
		if(b==1)b='Top';else if(b==2)b='Right';else if(b==3)b='Left';else b='Bottom';
		b='.style.border'+b+'Color=\"'+(e?c:'')+'\";';
		try{eval('this.element'+b);}catch(e){}
		e=this.elemLeft;if(e)try{eval('e'+b);}catch(e){}
		e=this.elemRight;if(e)try{eval('e'+b);}catch(e){}
	}
}
function igtab_selectTab(owner,index,fix)
{
	if(!owner)return;
	var o,e,i=owner.selected;
	if(i==null)
	{
		if((owner=igtab_getTabById(owner))==null)return;
		if((i=owner.selected)==null)return;
	}
	if(index==i||index<-1||owner.Tabs.length<=index)return;
	/* old */
	var cb=null,obj=owner.elemEmpty,old=i;
	if(i>=0)
	{
		o=owner.Tabs[i];
		cb=o._cb;
		owner.vis(o.elemDiv);
		if((e=o.elemIframe)!=null){owner.vis(e);if(!owner.loadAllUrls)e.src=owner.dummy;}
		o.setState(o.enabled?0:3);
		if((e=o.elemIndex)!=null)e.tabIndex=-1;
		o._clr();
	}
	else owner.vis(obj);
	/* new */
	if(index>=0)if((obj=owner.Tabs[index])==null)index=-1;
	if(fix!=1)owner.previousSelectedTab=owner.Tabs[(owner.selected==-2)?index:owner.selected];
	owner.selected=index;
	owner.update(-1,"SelectedTab",index);
	if(index<0){owner.vis(obj,"block");return;}
	if(!obj.enabled)obj.setEnabled(true);
	if(!obj.visible)obj.setVisible(true);
	obj.fixImg(2,owner.s508);
	obj.element.className=obj.css[2];
	if((e=obj.elemLeft)!=null)e.className=owner._cssL[2];
	if((e=obj.elemRight)!=null)e.className=owner._cssR[2];
	obj.state=2;
	if((e=obj.elemIndex)!=null)e.tabIndex=owner.tabIndex;
	if(owner.rows)
	{
		e=obj.element;
		i=owner.rows.length;
		while((e=e.parentNode)!=null)
			if((o=e.nodeName)!=null)if(o.toUpperCase()=="TD"&&ig_csom.notEmpty(e.id))
			{
				while(--i>0)if(e==owner.rows[i]){owner.moveRow(i);break;}
				break;
			}
	}
	o=obj.targetUrl;
	var len=o.length;
	if(len>2&&(e=obj.elemIframe)!=null)
	{
		var src=e.src;
		var len0=src.length,i=src.indexOf(o);
		if(len0!=len+i||!(i==0||(i>0&&src.charAt(i-1)=='/')))e.src=o;
		owner.vis(e,"block");
	}
	else owner.vis(obj.elemDiv,"block");
	e=igtab_getElementById(owner.ID+"_cp");
	o=obj.element.className;
	if(e&&o)
	{
		if(!owner._acss)e.className=((i=o.indexOf(" "))>2)?o.substring(i+1):"";
		obj._clr(e);
	}
	owner.ensureVisible();
	i=owner._cb;o=obj._cb;
	if(owner._new&&i&&i>1)if((i&2)!=0&&(cb||o))
		owner._ajax.addCallBack(owner,owner.uniqueId,(o&&(i&16)!=0)?o:false,old);
	ig_shared.fireTabChange();
}
function igtab_getTabFromElement(e)
{
	var t,ids=null,i=0;
	while(true)
	{
		if(!e)return null;
		try{if(e.getAttribute)ids=e.getAttribute("tabID");}catch(t){}
		if(ig_csom.notEmpty(ids))break;
		if(++i>1)return null;
		e=e.parentNode;
	}
	ids=ids.split(",");
	t=igtab_getTabById(ids[0]);
	if(t&&ids.length>1)
	{
		if((i=parseInt(ids[1]))<1000)t=ig_csom.notEmpty(t.Tabs)?t.Tabs[i]:null;
		else t=t.buttons[i-1000];
	}
	return t;
}
