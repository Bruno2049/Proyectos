/*
* ig_webdropdown.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


//vs 100510
function igdrp_getUniqueId(name)
{
	var o=igdrp_getComboById(name);
	return o?o.UniqueId:o;
}
function igdrp_getElementById(id){return document.getElementById(id);}
function igdrp_getComboById(id,e)
{
	var o=null,i=0;
	while(e!=null&&ig_csom.isEmpty(id))
	{
		try{if(e.getAttribute!=null)id=e.getAttribute("ig_drp");}catch(ex){}
		if(++i>7)return null;
		e=e.parentNode;
	}
	if(!ig_csom.isEmpty(id))if((e=igdrp_all)!=null)if((o=e[id])==null)
		for(i in e){if((o=e[i])!=null)if(o.Id==id||o.UniqueId==id)break;else o=null;}
	return (o&&o.ID)?o:null;
}
function igdrp_getComboByItem(item){return igdrp_all[igdrp_comboIdById(item.id)];}
function igdrp_comboIdById(itemId){return itemId.split("_")[0];}
if(typeof igdrp_all!="object")
	var igdrp_all=new Object();
function igdrp_evt(e)
{
	if(!e)if((e=window.event)==null)return;
	var o,t=e.type,src=e.target,all=igdrp_all;
	if(e==1){if(all._droped)all._droped._keepDrop();return;}
	if(t=="submit"){if(all._foc)all._foc.updateValue(true);return;}
	if(t=="unload"){for(o in all)igdc_new(o);return;}
	if(t=="load"){for(o in all)if(all[o]&&all[o]._load)all[o]._load();return;}
	if(!src)if((src=e.srcElement)==null)src=this;
	if((o=igdrp_getComboById(null,src))!=null)if(o.doEvt)o.doEvt(e,t,src);
}
function igdrp_combo(elem,props,ce)
{
	this.ID=this.Id=elem.id;
	this.Element=elem;
	elem.setAttribute("ig_drp",this.Id);
	this._addLsnr=function(e,n,m)
	{
		if(!e||e._old)return;
		e._old=true;
		for(var i=0;i<n.length;i++)if(n[i])
			ig_csom.addEventListener(e,n[i],m?igmask_event:igdrp_evt);
	}
	this._addLsnr(elem,["mousedown","mouseup","mouseout","mouseover",document.all?"resize":null]);
	this.Element.Object=this;
	this.UniqueId=props[0];
	this._fire=function(id,arg,evt)
	{
		var fn=this.Events.ce[id];
		if(ig_csom.isEmpty(fn)||!this.Loaded)return false;
		if(!evt)evt=new ig_EventObject();
		ig_fireEvent(this,fn,arg,evt);
		return evt.cancel;
	}
	var h=0,o=props[1],im=igdrp_getElementById(this.Id+"_img");
	var css=im.className,hov=o[4];
	if(ig_csom.isEmpty(hov))o[4]=null;else{o[4]=css;o[5]=css+' '+hov;}
	while(++h<4)if(ig_csom.isEmpty(o[h]))o[h]=o[0];
	this.DropButton={a:o,ImageUrl1:o[0],ImageUrl2:o[2],Image:im};
	this.elemCal=this.container=igdrp_getElementById(this.Id+"_container");
	this.ExpandEffects=new igdrp_expandEffects(this,props[2]);
	this.HideDropDowns=props[5];
	this.editable=props[6];
	this.readOnly=props[7]
	this._align=props[8];
	this.getDropDownAlignment=function(){return this._align;}
	this.setDropDownAlignment=function(v){this.update("DropDownAlignment",this._align=v);}
	this.autoCloseUp=props[9];
	this.Dropped=props[10];
	this.getAutoCloseUp=function(){return this.autoCloseUp;}
	this.setAutoCloseUp=function(v){this.update("AutoCloseUp",this.autoCloseUp=v);}
	this.isEnabled=function(){return !this.inputBox.disabled;}
	this.setEnabled=function(v)
	{
		this._state(v?0:3);
		this.inputBox.disabled=!v;
		if(this.Calendar)this.Calendar.setEnabled(v);
		this.update("Enabled",v);
	}
	this.ClientUniqueId=this.Id;
	var box=this.inputBox=igdrp_getElementById(this.Id+"_input");
	if(!ig_csom._dc_load)
	{
		ig_csom.addEventListener(window,"load",igdrp_evt);
		ig_csom.addEventListener(window,"unload",igdrp_evt);
		ig_csom.addEventListener(box.form,"submit",igdrp_evt);
		ig_csom.addEventListener(document,"mousedown",igdrp_mouseDown);
		if(document.captureEvent)document.captureEvent(Event.MOUSEDOWN);
		ig_csom._dc_load=true;
	}
	h=box.offsetHeight;
	if(h<8)if((h=box.parentNode.offsetHeight)>7)box.style.height=h+"px";
	this.postField=igdrp_getElementById(this.UniqueId+"_hidden");
	this.ForeColor=box.style.color;
	this.BackColor=box.style.backgroundColor;
	for(h=0;h<ce.length;h++)ce[h]=ig_csom.replace(ce[h],'&quot;','\"');
	this.Events={InitializeDateChooser:[ce[0]],BeforeDropDown:[ce[1]],AfterDropDown:[ce[2]],
		BeforeCloseUp:[ce[3]],AfterCloseUp:[ce[4]],EditKeyDown:[ce[5]],EditKeyUp:[ce[6]],
		ValueChanged:[ce[7]],TextChanged:[ce[8]],OnBlur:[ce[9]],InvalidDateEntered:[ce[10]],ce:ce};
	this._title=ce[11];
	this.Loaded=false;
	this.focus=function(){try{this.inputBox.focus();}catch(e){}}
	this.hasFocus=function(){return this.editor.foc;}
	this.isEditable=function(){return this.editable;}
	this.setEditable=function(v)
	{
		this.editable=(v=(v==true));
		this.inputBox.readOnly=v?this.isReadOnly():v;
		this.update("Editable",v);
	}
	this.isReadOnly=function(){return this.readOnly;}
	this.setReadOnly=function(v)
	{
		this.readOnly=(v=(v==true));
		this.inputBox.readOnly=v?v:!this.isEditable();
		this.update("ReadOnly",v);
	}
	this.getText=function(){return this.inputBox.value;}
	this.update=function(p,v,ee)
	{
		if(!this.postField)return;
		if(!this.viewState)this.viewState=new ig_xmlNode();
		var n=this.viewState.addNode("DateChooser",true);
		if(ee)n=n.addNode("ExpandEffects",true);
		if(v==null)v="";
		else if(v.getFullYear)v=v.getFullYear()+'x'+(v.getMonth()+1)+'x'+v.getDate();
		n.setPropertyValue(p,v);
		this.postField.value=this.viewState.getText();
	}
	this.isDropDownVisible=function(){return this.Dropped&&igdrp_all._droped==this;}
	this.move=function(e,par){try{ig_csom._skipNew=true;e.parentNode.removeChild(e);par.appendChild(e);ig_csom._skipNew=false;e._moved=true;return true;}catch(ex){}return false;}
	this._keepDrop=function()
	{
		var e=this.Element,p={x:0,y:0},pos=this._pos;
		var hide=e.offsetWidth<2;
		while(e&&e.nodeName!='BODY'&&e.nodeName!='FORM'){p.x+=e.offsetLeft;p.y+=e.offsetTop;e=e.parentNode;}
		if(''+p.x=='NaN')p.x=p.y=0;
		if(!pos)pos=this._pos=p;
		if(hide||Math.abs(pos.x-p.x)>2||Math.abs(pos.y-p.y)>2)this.setDropDownVisible(false);
	}
	this.setDropDownVisible=function(bDrop,foc)
	{
		if(ig_csom.IsIEWin&&!(igdrp_all._loaded||document.readyState=='complete'))return;
		var h,evt,iePan=this._ieBug,pan=this.container,edit=this.Element,body=window.document.body;
		/*if IE6 has body/html width/height set in %, but it is not 100%, then IFRAME or filter triggers lost focus, which destroyes grid-edit-mode*/
		if(ig_csom.IsIE6&&this._grid)this._ieBug2=true;
		if(pan==null||this.isReadOnly()||this.Dropped==bDrop)return;
		if(!bDrop)
		{
			if((evt=this.editor.elem.Validators)!=null)
				for(var i=0;i<evt.length;i++)try{ValidatorValidate(evt[i]);}catch(ex){}
			if(this._fire(3,pan))return;/* 3:BeforeCloseUp */
			pan.style.visibility="hidden";
			pan.style.display="none";
			if(iePan!=null)iePan.hide();
			this.Dropped=false;
			igdrp_all._droped=null;
			this._fire(4,pan);/* 4:AfterCloseUp */
			if(this.changed)this.updateValue(null,true);
			if(igdrp_all._dropTimer){window.clearInterval(igdrp_all._dropTimer);delete igdrp_all._dropTimer;}
			return;
		}
		if(this._fire(1,pan))return;/* 1: BeforeDropDown */
		/* Bugs 19608+25942 used by grid editor */
		this._dropTime=(new Date()).getTime();
		if(igdrp_all._droped)igdrp_all._droped.setDropDownVisible(false);
		this.focus();
		var cal=this.Calendar,editH=edit.offsetHeight,editW=edit.offsetWidth,e=edit,x=pan.parentNode;
		if(cal)cal.setSelectedDate(this.getValue());
		if(editH==null)editH=20;
		var f=x.tagName,bp=body.parentNode,par=this.inputBox.form;
		if(!par)par=document.forms[0];
		if(f=="FORM"){par=null;if(x.style)if((f=x.style.position)!=null)if(f.toLowerCase()=="absolute")par=body;}
		else if(f=="BODY"||f=="HTML")par=null;
		if(par)if(!this.move(pan,par))if(par!=body)this.move(pan,body);
		if(!this._ieBug2)this.ExpandEffects.applyFilter();
		pan.style.visibility="visible";pan.style.display="";
		if(pan.offsetHeight<5&&par&&par!=body)this.move(pan,body);
		if(!iePan&&this.HideDropDowns&&ig_csom.IsIEWin&&!this._ieBug2)
			this._ieBug=iePan=ig_csom.createTransparentPanel();
		var panH=pan.offsetHeight,panW=pan.offsetWidth,z=this.elemCal;
		x=z.offsetHeight;if(panH<x||panH>x+8)pan.style.height=x+"px";
		x=z.offsetWidth;if(panW<x||panW>x+8)pan.style.width=x+"px";
		ig_csom.absPosition(e,pan,64+this._align,iePan,this.inputBox);
		if(!this._ieBug2)this.ExpandEffects.applyFilter(true);
		this.Dropped=true;
		igdrp_all._droped=this;
		this._fire(2,pan);/* 2:AfterDropDown */
		if(cal&&foc!=1)cal._focus();
		this._pos=null;
		igdrp_all._dropTimer=window.setInterval('if(typeof igdrp_evt=="function")igdrp_evt(1)',300);
	}
	this.getVisible=function(){return (this.Element.style.display!="none"&&this.Element.style.visibility!="hidden");}
	this.setVisible=function(show,left,top,width,height)
	{
		var w=width,h=height,bdr=-1,e=this.Element,im=this.DropButton.Image,edit=this.inputBox;
		var s=e.style,t=edit.value,si=im.style,se=edit.style;
		if(show)edit.value="";else this.setDropDownVisible(false);
		s.display=show?"":"none";
		s.visibility=show?"visible":"hidden";
		if(show)
		{
			/*flag for editor in UltraWebGrid*/
			this._grid=width;
			if(top){s.position="absolute";s.top=top+"px";s.left=left+"px";}
			if(e.clientWidth)bdr=e.offsetWidth-e.clientWidth;
			if(!(bdr>=0&&bdr<7))bdr=s.borderWidth*2;if(!(bdr>=0&&bdr<7))bdr=0;
			var iw=im.offsetWidth;if(ig_csom.isEmpty(si.width))this.imW=si.width=iw+"px";
			if(h){if((h-=bdr)<1)h=1;if(ig_csom.isEmpty(si.height))this.imH=si.height=h+"px";se.height=h+"px";s.height=height+"px";}
			if(w)
			{
				if((w-=iw+bdr)<1)w=1;
				s.width=width+"px";
				if((si=edit.runtimeStyle)==null)si=se;
				for(h=0;h<4;h++){si.width=se.width=w+"px";if(e.offsetWidth>width+1)w--;else break;}
			}
			this.focus();
			edit.value=t;
			this.setFocusTop();
		}
		else if(this.imH)si.height="";
	}
	this.setFocusTop=function(){this.inputBox.select();this.focus();}
	this._same=function(d,v){return d==v||(v&&d&&v.getFullYear()==d.getFullYear()&&v.getMonth()==d.getMonth()&&v.getDate()==d.getDate());}
	this.updateValue=function(noFire,u)
	{
		var e=this.editor;
		var v=e.getDate(),o=this.old;
		if(!u&&this._same(o,v))return;
		this._0M=true;/*value used to fix reset*/
		this.update("Value",this.old=v);
		this._dt();
		if(noFire)return;
		this.changed=e.changed=false;
		var evt=new ig_EventObject();
		if(this._fire(7,this.getValue(),evt))return;/* 7:ValueChanged */
		if(evt.cancelPostBack||!(this._autoPost||evt.needPostBack))return;
		if(this.getAutoCloseUp())this.setDropDownVisible(false);
		if(this.post&&(new Date()).getTime()<this.post.getTime()+300)return;
		try{this.post=new Date();__doPostBack(this.UniqueId,"ValueChanged:"+e.text);}catch(e){}
	}
	this.onImgKey=function(evt)
	{
		var k=evt.keyCode;
		if(k==40||(!this.Dropped&&(k==32||k==13)))
		{
			this.setDropDownVisible(true);
			return;
		}
		if(k==27||(this.Dropped&&(k==32||k==13||k==9)))
		{
			this.setDropDownVisible(false);
			this.updateValue();
		}
	}
	if(this.Dropped)try{window.setTimeout("try{igdrp_all[\""+this.Id+"\"].setDropDownVisible(true,1);}catch(ex){}",100);}catch(ex){}
	this.Dropped=false;
	this.handlers=new Array();
	this.removeEventListener=function(name,fref)
	{
		name=name.toLowerCase();
		if(this.handlers==null)return;
		if(this.handlers[name]==null||!(this.handlers[name].length))return;
		for(i=0;i<this.handlers[name].length;i++)
		{
			var listener=this.handlers[name][i];
			if(listener!=null)if(listener.handler==fref)this.handlers[name][i]=null;
		}
	}
	this.addEventListener=function(name,fref,obj)
	{
		var a=this.handlers[name=name.toLowerCase()];
		if(a==null)this.handlers[name]=a=new Array();
		var eh=a[a.length]=new Object();eh.handler=fref;eh.obj=obj;
	}
	this.fireMulticastEvent=function(name,evt)
	{
		var list=this.handlers[name.toLowerCase()];
		if(ig_csom.notEmpty(list))for(var i=0;i<list.length;i++)
			if(list[i]!=null)list[i].handler(this,evt,list[i].obj);
	}
	this._state=function(i)
	{
		var db=this.DropButton;
		var im=db.Image,src=db.a[i];
		if(im.src!=src)im.src=src;
		if(i==3)i=0;
		var v=db.a[4+i];
		if(i<2&&db.a[5]&&im.className!=v)im.className=v;
	}
	this.doMousedown=function(evt,src)
	{
		var hide=igdrp_all._droped!=null,foc=igdrp_all._droped===this;
		if(hide)igdrp_all._droped.setDropDownVisible(false);
		if(foc){try{igdrp_all._old=this;window.setTimeout("try{igdrp_all._old.setFocusTop();}catch(e){}",10);}catch(e){}}
		else if(src.id==this.Id+"_img"||!this.isEditable()){this._state(2);this.setDropDownVisible(true);}
		if(src!=this.inputBox)ig_cancelEvent(evt);
	}
	this.doEvt=function(evt,type,src)
	{
		if(type=="resize")this.inputBox.style.width="100%";
		else if(this.isEnabled()&&this.Loaded&&!this.isReadOnly())switch(type)
		{
			case "mousedown":this.doMousedown(evt,src);return;
			case "mouseup":case "mouseout":this._state(0);return;
			case "mouseover":this._state(1);return;
			case "keydown":this.onImgKey(evt);return;
		}
	}
}
function igdrp_expandEffects(owner,props)
{
	this.owner=owner;
	this.Element=owner.container;
	this.duration=props[0];
	this.opacity=props[1];
	this.type=props[2];
	this.color=props[3];
	this.shadow=props[4];
	this.delay=props[5];
	this.getDuration=function(){return this.duration;}
	this.getOpacity=function(){return this.opacity;}
	this.getType=function(){return this.type;}
	this.getShadowColor=function(){return this.color;}
	this.getShadowWidth=function(){return this.shadow;}
	this.getDelay=function(){return this.delay;}
	this.setDuration=function(v){this.owner.update("Duration",this.duration=v,true);}
	this.setOpacity=function(v){this.owner.update("Opacity",this.opacity=v,true);}
	this.setType=function(v){this.owner.update("Type",this.type=v,true);}
	this.setShadowColor=function(v){this.owner.update("ShadowColor",this.color=v,true);}
	this.setShadowWidth=function(v){this.owner.update("ShadowWidth",this.shadow=v,true);}
	this.setDelay=function(v){this.owner.update("Delay",this.delay=v,true);}
	this.applyFilter=function(p)
	{
		var e=this.Element;
		if(!e||!ig_csom.IsIEWin||ig_csom.AgentName.indexOf("win98")>0||ig_csom.AgentName.indexOf("windows 98")>0)return;
		var s=e.style,ms="progid:DXImageTransform.Microsoft.";
		if(!p&&this.type!="NotSet")s.filter=ms+this.type+"(duration="+(this.duration/1000)+")";
		if(!p&&this.shadow>0)s.filter+=" "+ms+"Shadow(Direction=135,Strength="+this.shadow+",color='"+this.color+"')";
		if(!p)
		{
			var pe=e,o=this.opacity;if(o>100)o=100;
			while(o<0&&(pe=pe.parentNode)!=null)if(pe.filters)o=100;
			if(o>=0)s.filter+=" "+"Alpha(Opacity="+o+")";
		}
		if(e.filters[0])try{if(p)e.filters[0].play();else e.filters[0].apply();}catch(ex){}
	}
}
function igdrp_mouseDown(evnt)
{
	if(!evnt)evnt=window.event;
	var me=igdrp_all._droped;
	if(!evnt||!igdrp_all._droped)return;
	var e=evnt.target,container=me.Id+"_container";
	if(!e)if((e=evnt.srcElement)==null)e=this;
	while(e!=null){if(e.id==container)return;e=e.parentNode;}
	me.setDropDownVisible(false);
}
function igdc_new(id,prop,df,ce,calID,a)
{
	var foc=false,pan=null,drop=false,me=igdrp_all[id],elem=(typeof document=='unknown')?null:igdrp_getElementById(id);
	if(me&&me.doEvt&&elem)
	{
		foc=me.hasFocus();
		drop=me.Dropped;
		pan=me._ieBug;
		if(igdrp_all._droped==me)igdrp_all._droped=null;
		if(igdrp_all._foc==me)igdrp_all._foc=null;
		if(igdrp_all._old==me)igdrp_all._old=null;
		var m=me.inputBox;
		if(m)m=igdrp_all[m.id];
		if(m){m.tr=m.elem=m.parent=m.Event=null;if(m._onTimer)delete m._onTimer;}
		me.editor=me._ieBug=me.postField=me.elemCal=me.container=me.Element=me.inputBox=me.Calendar=me.webGrid=null;
		ig_dispose(me);
	}
	if(!prop||!elem)return null;
	igdrp_all[id]=me=new igdrp_combo(elem,prop,ce);
	var info=new Object();
	info.DayNames=df[0];info.AbbreviatedDayNames=df[1];info.MonthNames=df[2];info.AbbreviatedMonthNames=df[3];
	info.ShortDatePattern=df[4];info.LongDatePattern=df[5];info.DateSeparator=df[6];info.MonthDayPattern=df[7];info.YearMonthPattern=df[8];
	df=prop[11];
	var box=me.inputBox,date=ig_csom.isEmpty(df)?null:new Date(df[0],df[1]-1,df[2]);
	var editor=new igmask_date(me,box,info,date,prop[15]!=0,prop[14],prop[16]);
	if(!ig_csom.isEmpty(df=prop[12]))editor.min=new Date(df[0],df[1]-1,df[2]);
	if(!ig_csom.isEmpty(df=prop[13]))editor.max=new Date(df[0],df[1]-1,df[2]);
	me._autoPost=prop[17];
	var tab=prop[18];
	if(tab&&tab>-1)editor.elem.tabIndex=me.DropButton.Image.tabIndex=tab;
	me._addLsnr(me.DropButton.Image,["keydown"]);
	me.getAllowNull=function(){return this.editor.allowNull;}
	me.setAllowNull=function(v){this.editor.allowNull=v;}
	me.editor=editor;
	me.setMaxDate=function(d)
	{
		if(this.Calendar)this.Calendar.MaxDate=d;
		this.update("MaxDate",this.editor.max=d);
	}
	me.getMaxDate=function(){return this.editor.max;}
	me.setMinDate=function(d)
	{
		if(this.Calendar)this.Calendar.MinDate=d;
		this.update("MinDate",this.editor.min=d);
	}
	me.getMinDate=function(){return this.editor.min;}
	me.getNullDateLabel=function(){return this.editor.nullText;}
	me.setNullDateLabel=function(v){this.update("NullDateLabel",this.editor.nullText=v);}
	me.showCalendar=function(){this.setDropDownVisible(true);}
	me.hideCalendar=function(){this.setDropDownVisible(false);}
	me._msV=function(d,fire)
	{
		var v=this.Element.value=d?(d.getFullYear()+"-"+(d.getMonth()+1)+"-"+d.getDate()):"";
		if(this._msV_==null)this._msV_=v;
		if(fire)fire=!this._noFire;
		if(!this._same(this.old,d))this.update("Value",this.old=d);
		if(fire&&this.inputBox.onchange&&window.event&&this._msV_!=v){this.inputBox.onchange();this._msV_=v;}
	}
	me.onDateSelected=function(cal,date)
	{
		var me=cal.ownerDC;
		if(!me.Dropped)return;
		me.setValue(date,true);
		me._msV(me.getValue());
		if(me.getAutoCloseUp())me.setDropDownVisible(false,true);
		if(!me.isEditable()||me.getAutoCloseUp())me.setFocusTop();
	}
	me.setValue=function(d,fire){this.old=null;this._noFire=true;this.editor.setDate(d);this.updateValue(!fire,true);this._noFire=false;}
	me.setText=function(v,fire){this.old=null;this._noFire=true;this.editor.setText(v);this.updateValue(!fire,true);this._noFire=false;}
	me.getValue=function(){return this.editor.getDate();}
	me.getRenderedValue=function(v){return this.editor.staticText(v,true);}
	me._dt=function(){var e=this.inputBox,v=this._title;if(v&&v.length>5)e.title=e.alt=v.replace('[value]',e.value);}
	var cal=igcal_getCalendarById(calID);
	if(cal){me.Calendar=cal;cal.ownerDC=me;cal.onValueChanged=me.onDateSelected;me.elemCal=cal.element;editor.yearFix=cal.yearFix;}
	me._msV(date);
	me.old=me._0D=date;
	editor.init0();
	me._oldV=me._0T=box.value;
	me._dt();
	
	me._load=function()
	{
		igdrp_all._loaded=true;
		if(this._oldV==this.inputBox.value)return;
		var v=unescape(this.postField.value);
		var i=v.indexOf('Value=');
		if(i<1)return;
		v=v.substring(i+7);
		i=v.indexOf('\"');
		if(i<0)return;
		v=v.substring(0,i).split('x');
		this.setValue((v.length>2)?(new Date(v[0],v[1]-1,v[2])):null);
	}
	me.Loaded=true;
	me._ieBug=pan;
	if(a)me._load();
	if(drop)me.setDropDownVisible(true);
	me._fire(0);
	if(prop[3]||foc)me.focus();
	return me;
}
function igdc_initDateChooser(id,p,df,ce,calID,a){return igdc_new(id,p,df,ce,calID,a);}

function igmask_date(me,e,di,v,lf,nullTxt,nullable)
{
	if(e==null)return;
	this.parent=me;
	this.changed=false;
	this.extra=new Object();
	this.nullText=nullTxt;
	this.allowNull=nullable;
	this.repaint0=function(fire)
	{
		if((this.k0==null)||(this.changed&&this.elem.value==this.text))return;
		this.elem.value=this.text;
		if(!fire)return;
		this.changed=true;
		this.fireEvt(10,null);
	}
	var id=e.id;
	me._addLsnr(e,["keydown","keypress","keyup","focus","blur"],true);
	this.id=id;
	e.setAttribute("maskID",id);
	this.elem=e;
	if(e.createTextRange!=null)this.tr=e.createTextRange();
	this.k1=0;
	this.fixKey=this.yearFix=0;
	this.useLastGoodValue=true;
	this.delta=1;
	this.doKey=function(e,a)
	{
		var k=e.keyCode;
		if(k==0||k==null)if((k=e.which)==null)return;
		if(k==9)this.parent.setDropDownVisible(false);
		if((k<32&&k!=8)||e.altKey)return;
		if(e.ctrlKey){if(k!=86)return;this._v=true;if(a!=2)return;}
		if(this._v){if(a==2){this._v=null;this.paste(this.elem.value);}return;}
		if(a==1)this.k1=k;
		var t0=this.text,t1=this.elem.value;
		var i=t1.length,k0=this.k0;
		if(a==2)
		{
			this.k1=0;
			if(t0!=t1)
			{
				this.changed=true;
				if(k==8||k==46||(k0<32&&k0>9))this._msV();
			}
			if(k0<32)return;
			if(this.fixKey>0||i==1)this.afterKey(k,this.fixKey++==1);
			else if(this.fixKey==0)if(i--==0){this.fixKey=2;return;}
			this.k0=-2;
			return;
		}
		switch(k)
		{
			
			case 35:case 39:case 36:case 37:if(this.k1==k)return;break;
			
			case 8:case 46:if(this.k1==k)return;break;
			
			case 38:case 40:
				if(a==1&&this.delta!=0&&!e.shiftKey)this.spin((k==38)?this.delta:-this.delta);
				if(this.k1==k)return;break;
		}
		if(a==1)
		{
			t0=this.getSelectedText();
			if(t0.length>0||this.sel0<i)this.fixKey=0;
			else if(this.fixKey==0&&this.sel0==i)this.fixKey=1;
			return;
		}
		if(k0>0)
		{
			if(t0!=t1)this.changed=true;
			if(this.fixKey>0)this.afterKey(k0,this.fixKey>0);
		}
		var newK=this.filterKey(k,this.fixKey>0);
		if(newK!=k&&this.tr==null)newK=0;
		if(newK==0)ig_cancelEvent(e);
		else if(newK!=k&&this.tr!=null)e.keyCode=newK;
		this.k0=newK;
	}
	this.stoi=function(s)
	{
		switch(s.toLowerCase())
		{
			case "keypress":return 0;
			case "keydown":return 1;
			case "keyup":return 2;
			case "focus":return 8;
			case "blur":return 9;
			case "invalidvalue":return 11;
		}
		return 10;
	}
	this.doEvtM=function(e)
	{
		if(e==null||this.elem.disabled)return;
		var v=!this.elem.readOnly,a=this.stoi(e.type);
		if(a==1&&e.keyCode==13)this.kE=1;
		if(a<8)if(this.fireEvt(a,e))if(a==1)ig_cancelEvent(e);
		if(a<3&&v)this.doKey(e,a);
		if(a>=8)
		{
			if((a==8)==this.foc)return;
			this.isOk();
			this.foc=(a==8);
			if(a==9&&v)
			{
				if(!this.changed)this.changed=this.text!=this.elem.value;
				if(this.changed)this.text=this.elem.value;
				this._same=this.parent._same(this._date0,this.toDate(this.elem.value,1));
			}
			if(a==8&&v)
			{
				this._date0=this.toDate(this.elem.value,1);
				igdrp_all._foc=this.parent;
				if(this.useLastGoodValue)this.setGood();
				if((v=this.elem.value)!=this.text&&v.length<12){this.paste(v);return;}
			}
			this.repaint(a==9&&this.changed);
			this.fireEvt(a,e);
			if(this.foc)
			{
				this.changed=false;
				
				
				
				a=this.parent._gridKey;
				if(a&&a>47&&a<58)
				{
					this.elem.value=String.fromCharCode(a);
					this.select(1,1);
					this.afterKey(this.k0=a,true);
				}
				else this.select();
				this.parent._gridKey=null;
			}
			return;
		}
		this.kE=null;
		if((v=this.elem.value)!=this.text&&(this.k1==0||a<4))
		{
			this.changed=true;
			if(a>3&&this.k1==0)this.paste(v);
			else this.text=v;
			this.fireEvt(10,e);
		}
	}
	this.fireEvt=function(id,e)
	{
		var evt=this.Event,me=this.parent,k=e?e.keyCode:0;
		if(!me||!me.Loaded)return false;
		if(!evt)evt=this.Event=new ig_EventObject();
		evt.reset();evt.event=e;
		if(id==1)
		{
			if(k==40&&e.altKey)me.setDropDownVisible(true);
			if(k==27||k==13)
			{
				me.setDropDownVisible(false);
				if(k==13)
				{
					var d=this.toDate(this.elem.value,1);
					if(!me._same(this._date0,d))me.updateValue(null,true);
					
					this.date=this._date0=d;
				}
			}
			me._fire(5,k,evt);
			me.fireMulticastEvent("keydown",evt);
		}
		if(id==2)me._fire(6,k,evt);
		if(id==9)
		{
			if(this.changed)if(me.Dropped)me.changed=true;else me.updateValue(null,!this._same);
			if(!me.Dropped)
			{
				if(me.endEditCell)me.endEditCell();
				me._fire(9,me,evt);
				me.fireMulticastEvent("blur",evt);
			}
		}
		if(id==10)me._fire(8,this.elem.value,evt);
		if(id==11)me._fire(10,this.extra,evt);
		return evt.cancel;
	}
	this.select=function(s0,s1)
	{
		var i=this.elem.value.length;
		if(s1==null)if((s1=s0)==null){s0=0;s1=i;}
		if(s1>=i)s1=i;
		else if(s1<s0)s1=s0;
		if(s0>s1)s0=s1;
		if(this.elem.selectionStart!=null)
		{
			this.elem.selectionStart=this.sel0=s0;
			this.elem.selectionEnd=this.sel1=s1;
			this.tr=null;
		}
		if(this.tr==null)return;
		this.sel0=s0;this.sel1=s1;
		s1-=s0;
		this.tr.move("textedit",-1);
		this.tr.move("character",s0);
		if(s1>0)this.tr.moveEnd("character",s1);
		this.tr.select();
	}
	this.getSelectedText=function()
	{
		var r="";
		this.sel0=this.sel1=-1;
		if(this.elem.selectionStart!=null)
		{
			if((this.sel0=this.elem.selectionStart)<(this.sel1=this.elem.selectionEnd))
				r=this.elem.value.substring(this.sel0,this.sel1);
			this.tr=null;
		}
		if(this.tr==null)return r;
		var sel=document.selection.createRange();
		r=sel.duplicate();
		r.move("textedit",-1);
		this.sel0=0;
		try{while(r.compareEndPoints("StartToStart",sel)<0)
		{
			if(this.sel0++>1000)break;
			r.moveStart("character",1);
		}}catch(ex){}
		r=sel.text;
		this.sel1=this.sel0+r.length;
		return r;
	}
	
	
	
	this.order=7370;
	this.sepCh="/";
	this.sep=47;
	this.autoCentury=true;
	this.setDateInfo=function(v)
	{
		this.info=v;
		var sep0=null;
		if(v!=null)
		{
			v=this.info.ShortDatePattern;
			if(ig_csom.isEmpty(sep0=this.info.DateSeparator))sep0=null;
		}
		if(v==null||v.length<3)v="MM/dd/yyyy";
		var ii=v.length;
		var y=0,m=0,d=0,sep=0,i=-1,o=0;
		while(++i<ii)
		{
			var ch=v.charAt(i);
			if(ch=='d'){if(d++>0){o|=1024;continue;}o|=1<<(sep++*3);}
			else if(ch=='m'||ch=='M'){if(m++>0){o|=2048;continue;}o|=2<<(sep++*3);}
			else if(ch=='y'){if(y++>0){if(y>2)o|=4096;continue;}o|=3<<(sep++*3);}
			else if(sep==1&&sep0==null)sep0=ch;
		}
		if(sep0!=null){this.sepCh=sep0;this.sep=sep0.charCodeAt(0);}
		if(m==0)o|=1<<(sep++*3);
		if(d==0)o|=2<<(sep++*3);
		if(y==0)o|=3<<(sep++*3);
		this.order=o;
		this.mask=v;
	}
	this.setGood=function()
	{
		var d=this.date;
		if(!d)
		{
			if(this.elem.value.length>0)d=this.toDate();
			if(!d&&!this.allowNull)d=this._mm();
			this.date=d;
		}
		this.good=d;
	}
	this.focusText=function()
	{
		var v,i=-1,t="",d=this.date;
		if(!d){if(this.allowNull)return t;this.date=d=this._mm();}
		while(++i<3)
		{
			if((v=(this.order>>i*3)&3)==0)break;
			if(i>0)t+=this.sepCh;
			switch(v)
			{
				case 1:v=d.getDate();if(v<10&&(this.order&1024)!=0)t+=0;break;
				case 2:v=d.getMonth()+1;if(v<10&&(this.order&2048)!=0)t+=0;break;
				case 3:v=d.getFullYear()+this.yearFix;if((this.order&4096)==0)v%=100;if(v<10)t+=0;break;
			}
			t+=v;
		}
		return t;
	}
	this.setText=function(v){this.setDate(this.toDate(v));}
	this.toDate=function(t,inv)
	{
		if(t==null){if(this.elem.readOnly||this.k0==null)return this.date;t=this.elem.value;}
		var ii=t.length;
		if(ii>12)return this.date;
		if(ii==0&&this.allowNull)return null;
		var y=-1,m=-1,d=-1,sep=0,i=-1,f=0;
		while(++i<=ii)
		{
			var ch=(i<ii)?t.charCodeAt(i):this.sep;
			if(ch==this.sep||(ch>43&&ch<48))
			{
				if(i+1==ii)break;
				switch((this.order>>sep*3)&3)
				{
					case 1:d=f;break;
					case 2:m=f;break;
					case 3:y=f;break;
				}
				sep++;
			}
			ch-=48;
			if(ch>=0&&ch<=9){if(f<1000)f=f*10+ch;}
			else f=0;
		}
		f=null;i=0;
		if(y>99&&y>this.yearFix)y-=this.yearFix;
		if(inv!=1){this.extra.year=y;this.extra.month=m;this.extra.day=d;this.extra.reason=(ii>0)?2:1;}
		if(sep!=3)i++;
		else
		{
			if(d<1||d>31||m<1||m>12||y<0||y>9999)i++;
			else
			{
				if(m==2&&d>29)i=d=29;
				if(this.autoCentury){if(y<37)y+=2000;else if(y<100)y+=1900;}
				f=new Date(y,m-1,d);
				if(y<100&&f.setFullYear!=null)f.setFullYear(y);
				if(f.getDate()!=d)
				{
					f=new Date(i=y,m-1,d-1);
					if(y<100&&f.setFullYear!=null)f.setFullYear(y);
				}
				d=f.getTime();
				if((m=this.max)!=null)if(d>m.getTime()){f=m;if(i++==0)this.extra.reason=0;}
				if((m=this.min)!=null)if(d<m.getTime()){f=m;if(i++==0)this.extra.reason=0;}
			}
		}
		if(inv===1)return f;
		this.extra.date=f;d=this.kE;
		if(d!=2)if((inv||d)&&i>0){this.kE=2;if(this.fireEvt(11,null))f=this.date;}
		return f;
	}
	this.spin=function(v)
	{
		var d=this.toDate();
		if(!d)d=this._mm();
		this.setDate(new Date(d.getFullYear(),d.getMonth(),d.getDate()+v));
	}
	this.repaint=function(fire)
	{
		var t=null;
		if(!this.elem.readOnly&&!this.elem.disabled)
		{
			if(this.foc)t=this.focusText();
			else if(this.changed)
			{
				var d=this.toDate(t=this.elem.value,true);
				if(fire)t=null;
				this._d(d);
				this._msV(this.date,true);
			}
		}
		this.text=(t==null)?this.staticText():t;
		this.repaint0(fire);
	}
	this._d=function(d)
	{
		if(!d)if(this.useLastGoodValue&&this.good!=null&&this.text.length>0)d=this.good;
		else if(!this.allowNull)d=this._mm();
		return this.date=d;
	}
	this.staticText=function(d,x)
	{
		if(!x)if((d=this.date)==null)
		{
			if((d=this._d())==null)return this.nullText;
			this._msV(d,true);
		}
		if(!d)if(this.allowNull)return this.nullText;else d=this._mm();
		var t=this.info;
		if(t!=null)t=this.longFormat?t.LongDatePattern:t.ShortDatePattern;
		else if(this.longFormat&&d.toLocaleDateString!=null)return d.toLocaleDateString();
		if(t==null||t.length<2)t=this.mask;
		else t=ig_csom.replace(t,"'","");
		var f="yyyy",v=d.getFullYear()+this.yearFix;
		if(t.indexOf(f)<0){if(t.indexOf(f="yy")<0)v=-1;else if((v%=100)<10)v="0"+v;}
		if(v!=-1)t=t.replace(f,v);
		f="MMM";
		v=d.getMonth()+1;
		var mm=null,dd=null;
		if(t.indexOf(f)<0)
		{
			if(t.indexOf(f="MM")<0){if(t.indexOf(f="M")<0)v=-1;}
			else if(v<10)v="0"+v;
			if(v!=-1)t=t.replace(f,v);
		}
		else
		{
			if(t.indexOf("MMMM")>=0)f="MMMM";
			if(this.info!=null)mm=(f.length==4)?this.info.MonthNames:this.info.AbbreviatedMonthNames;
			if(mm!=null)mm=(mm.length>=v)?mm[v-1]:null;
			t=t.replace(f,(mm==null)?(""+v):"[]");
		}
		f="ddd";
		v="";
		if(t.indexOf(f)>=0)
		{
			if(t.indexOf("dddd")>=0)f="dddd";
			if(this.info!=null)dd=(f.length==4)?this.info.DayNames:this.info.AbbreviatedDayNames;
			v+=d.getDay();
			if(dd!=null)dd=(dd.length>=v)?dd[v]:null;
			t=t.replace(f,(dd==null)?v:"()");
		}
		f="dd";
		v=d.getDate();
		if(t.indexOf(f)<0){if(t.indexOf(f="d")<0)v=-1;}
		else if(v<10)v="0"+v;
		if(v!=-1)t=t.replace(f,v);
		if(mm!=null)t=t.replace("[]",mm);
		if(dd!=null)t=t.replace("()",dd);
		return t;
	}
	this.isOk=function()
	{
		var p=this.parent;var d=p.old,pv=p.postField.value;
		
		if(!this.foc&&p._0M&&pv==''){this.date=p._0D;this.text=p._0T;p._0M=false;return;}
		if(this._ok||this.foc||d||d===this.date)return;
		d=unescape(pv);
		var i=d.indexOf("Value=\"");if(i<1)return;
		d=d.substring(i+7);i=d.indexOf("\"");if(i<0)return;
		d=d.substring(0,i).split("x");
		this.date=p.old=(d.length<3)?null:new Date(d[0],d[1]-1,d[2]);
		this.text=this.elem.value;this._ok=true;
	}
	this.getDate=function(){this.isOk();return this.foc?this.toDate():this.date;}
	this._mm=function(d){var m=this.max;if(!d)d=new Date();if(m&&d.getTime()>m.getTime())return m;m=this.min;if(m&&d.getTime()<m.getTime())return m;return d;}
	this.setDate=function(v,f)
	{
		if(v&&v.length)v=(v.length<3)?null:this.toDate(v);
		if(!v&&!this.allowNull)if((v=this.date)==null)v=new Date();
		if(v)v=this._mm(v);
		else this.good=null;
		var fire=this.date!=v;
		this._ok=true;
		this.date=v;
		this.text=this.foc?this.focusText():this.staticText();
		this.repaint0(fire);
		if(f!=1)this._msV(v,true);
	}
	
	this.canAdd=function(k,t)
	{
		var ii=t.length-1;
		if(ii<0)return (k==this.sep)?-1:k;
		if(t.charCodeAt(ii)==this.sep)return (k==this.sep)?-1:k;
		var ch,f=0,sep=0,i=-1,n=0;
		while(++i<=ii)
		{
			if((ch=t.charCodeAt(i))==this.sep){if(sep++>1)return -1;n=f=0;continue;}
			n++;f=f*10+ch-48;
		}
		if(sep>1&&k==this.sep)return -1;
		i=(this.order>>sep*3)&3;
		if(i==1){if(n>1||f*10+k-48>31)n=4;}
		if(i==2){if(n>1||f*10+k-48>12)n=4;}
		return (n<4)?k:((sep>1)?-1:this.sep);
	}
	this.afterKey=function(k,fix)
	{
		var f=0,t=this.elem.value;
		if(fix)
		{
			var sep=0,i=-1,i0=0,ii=t.length,tt="";
			while(++i<=ii)
			{
				var ch=(i<ii)?t.charCodeAt(i):this.sep;
				if(ch==this.sep)
				{
					switch((this.order>>sep*3)& 3)
					{
						case 1:if(f>31){while(f>31)f=Math.floor(f/10);}else f=-1;break;
						case 2:if(f>12){while(f>12)f=Math.floor(f/10);}else f=-1;break;
						case 3:if(f<=9999)f=-1;else while(f>9999)f=Math.floor(f/10);break;
					}
					if(f<0)tt+=t.substring(i0,i);else tt+=f;
					if(i<ii)tt+=this.sepCh;
					sep++;i0=i+1;
				}
				ch-=48;
				if(ch>=0&&ch<=9)f=f*10+ch;else f=0;
			}
			t=tt;
		}
		if(this.k0>(f=0))if(this.canAdd(48,t)==this.sep){t+=this.sepCh;f++;}
		this.elem.value=t;
		if(f>0)this.select(100,100);
		this._msV();
	}
	this._msV=function(d,f){this.parent._msV(f?d:this.toDate(this.elem.value,1),f);}
	this.filterKey=function(k,fix)
	{
		if(k!=this.sep&&(k<48||k>57))if(this.tr!=null&&this.isSep(k))k=this.sep;else return 0;
		if(k==this.sep&&this.sel0==0)return 0;
		if(fix&&this.canAdd(k,this.elem.value)!=k)k=0;
		return k;
	}
	this.isSep=function(k){return k==this.sep||k==45||k==92||k==95||k==47||k==32||k==46||k==44||k==58||k==59;}
	this.paste=function(old)
	{
		var ch,sep=true,v="",f=0;
		for(var i=0;i<old.length;i++)
		{
			ch=old.charCodeAt(i);
			if(ch>=48&&ch<=57)sep=false;
			else{if(!this.isSep(ch))continue;if(f>1)break;if(sep)continue;sep=true;f++;}
			v+=sep?this.sepCh:old.charAt(i);
		}
		this.text="";
		this.setText(v);
		this._msV(this.date,true);
	}
	this.longFormat=lf;
	this.setDateInfo(di);
	this.setDate(v,1);
	this.init0=function()
	{
		this.k0=-2;
		var e=this.parent.Element,img=this.parent.DropButton.Image;
		var w=e.offsetWidth,w0=e.style.width;
		if(w0.length>0)if(w0.indexOf("%")>0)return;else try{w0=parseInt(w0);}catch(ex){w0=0;}
		try{if(isNaN(w0))w0=0;}catch(ex){w0=0;}
		if(w==0)
		{
			if(!this._onTimer)try{this._onTimer=function(){return this.init0();};ig_handleTimer(this);}catch(ex){}
			return;
		}
		if(this._onTimer)delete this._onTimer;
		if(w0==w)return true;
		var b=e.clientWidth;b=(b==null)?-1:w-b;if(b<0||b>4)b=2;
		if(w0<=0){w0=(w>50&&w<300)?w:120;e.style.width=w0+"px";}
		w=img.style.width;
		if(w==null||w.length==null||w.length<1)w=img.offsetWidth;
		else try{w=parseInt(w);}catch(ex){w=17;}
		if(w==null||w<2)w=17;
		if((w=w0-w-b)<2)w=2;
		e=this.elem;e.style.width=w+"px";
		if(e.runtimeStyle)e.runtimeStyle.width=w+"px";
		return true;
	}
	igdrp_all[id]=this;
}
function igmask_event(e)
{
	if(!e)if((e=window.event)==null)return;
	var c=null,id=null,i=0,o=e.target;
	if(!o)o=e.srcElement;if(!o)o=this;
	while(true)
	{
		if(!o||i++>2)return;
		try{if(o.getAttribute!=null)id=o.getAttribute("maskID");}catch(ex){}
		if(!ig_csom.isEmpty(id)){c=igdrp_all[id];break;}
		if((c=o.parentNode)!=null)o=c;
		else o=o.parentElement;
	}
	if(c!=null&&c.doEvtM!=null)c.doEvtM(e);
}
