/*
* ig_webcombo3_1.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/







var igcmbo_displaying;
var igcmbo_currentDropped;

if(typeof igcmbo_comboState!="object")

var igcmbo_comboState=new Object();







var wccounter=0;
function igcmbo_onmousedown(evnt,id)
{
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded || oCombo.Element.getAttribute("disabled")=="disabled") 
		return;
	var src = igcmbo_srcElement(evnt);
	





	
	ig_inCombo = true;
	if(oCombo.Editable && src.id == id + "_input")
		return;
	
	




    if(src.id==id + "_img")
    {
    	if (!((ig_csom.IsNetscape6 || ig_csom.IsIE9Plus) && evnt.button == 0 || ig_csom.IsIE && evnt.button == 1))
        return;
    }
	
	oCombo.Element.setAttribute("noOnBlur",true);
	if(igcmbo_currentDropped != null && igcmbo_currentDropped != oCombo)
		igcmbo_currentDropped.setDropDown(false);
	if(oCombo.getDropDown() == true)
	{
		oCombo.setDropDown(false);
		igcmboObject = oCombo;
		if(document.all)
			setTimeout('igcmbo_focusEdit()', 10);
	}
	else
	{
		igcmbo_swapImage(oCombo, 2);
		oCombo.setDropDown(true);
	}
	window.setTimeout("igcmbo_cancelNoOnBlur('"+id+"')",100);
}

var igcmboObject = null;
function igcmbo_focusEdit() 
{
	igcmboObject.setFocusTop();
}

function igcmbo_onmouseup(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	if(oCombo.Dropped == true) {
		igcmbo_swapImage(oCombo, 1);
		
		oCombo.setFocusTop();
	}
	else {
	}
}

function igcmbo_onmouseout(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	if(oCombo.Dropped == true) {
		igcmbo_swapImage(oCombo, 1);
	}
	else {
	}
}

function igcmbo_swapImage(combo, imageNo) {
	var img = igcmbo_getElementById(combo.ClientUniqueId + "_img");
	if(imageNo == 1) img.src = combo.DropImage1;
	else img.src = combo.DropImage2;
}

function igcmbo_ondblclick(evnt,id) {
	var oCombo = igcmbo_getComboById(id);
	if(!oCombo || !oCombo.Loaded) 
		return;
	if(oCombo.getDropDown() == true) {
		oCombo.setDropDown(false);
	}
}


function igcmbo_onKeyDown(evnt) {
	if(evnt.keyCode == 40)
	{ // down arrow
	}
}

// public - Retrieves the server-side unique id of the combo
function igcmbo_getUniqueId(comboName) {
	var combo = igcmbo_comboState[comboName];
	if(combo != null)
		return combo.UniqueId;
	return null;
}
function igcmbo_getElementById(id) {
		





		if(document.getElementById)
			return document.getElementById(id);
        else if(document.all)
			return document.all[id];
        return null;			
}

// public - returns the combo object for the Item Id
function igcmbo_getComboById(itemId) {
	


	
	





	if(typeof(igcmbo_comboState)=="undefined")
		return null;
	var cmbo=igcmbo_comboState[itemId];
	if(!cmbo)
	{	
		var tcmbo = null;
		for(var gId in igcmbo_comboState)
		{
			tcmbo = igcmbo_comboState[gId];
			if(tcmbo.UniqueID==itemId || tcmbo.ClientID==itemId)
			{
				cmbo=tcmbo;
				break;
			}
		}	
	}			
	return cmbo;		
}

// public - returns the combo object from an Item element
function igcmbo_getComboByItem(item) {
	var id = igcmbo_comboIdById(item.id);  
	return igcmbo_comboState[id];
}

// public - returns the combo Name from an itemId
function igcmbo_comboIdById(itemId) {
   var comboName = itemId;
   var strArray = comboName.split("_");
   return strArray[0];
}

function igcmbo_getLeftPos(e) {
	x = e.offsetLeft;
	if(e.style.position=="absolute")
		return x;
	tmpE = e.offsetParent;
	while (tmpE!=null && tmpE.tagName!="BODY")
	{
		if(tmpE.style.overflowX && tmpE.style.overflowX!="visible" || tmpE.style.overflow && tmpE.style.overflow!="visible")
			break;
		if((tmpE.style.position!="relative") && (tmpE.style.position!="absolute"))
			x += tmpE.offsetLeft;
		tmpE = tmpE.offsetParent;
	}
	return x;
}

function igcmbo_getTopPos(e) {
	y = e.offsetTop;
	if(e.style.position=="absolute")
		return y;
	tmpE = e.offsetParent;
	while(tmpE!=null && tmpE.tagName!="BODY")
	{
		if(tmpE.style.overflowY && tmpE.style.overflowY!="visible" || tmpE.style.overflow && tmpE.style.overflow!="visible")
			break;
		if((tmpE.style.position!="relative") && (tmpE.style.position!="absolute"))
			y += tmpE.offsetTop;
		tmpE = tmpE.offsetParent;
	}
	return y;
}

// Warning: Private functions for internal component usage only
// The functions in this section are not intended for general use and are not supported
// or documented.

// private - Fires an event to client-side script and then to the server is necessary
function igcmbo_fireEvent(id,eventObj,eventString){
	var oCombo=igcmbo_comboState[id];
	var result=false;
	if(eventObj[0]!="")
		result=eval(eventObj[0]+eventString);
	if(oCombo.Loaded && result!=true && eventObj[1]==1 && !oCombo.CancelPostBack)
		oCombo.NeedPostBack = true;
	oCombo.CancelPostBack=false;
	return result;
}

// private - Performed on page initialization
function igcmbo_initialize() 
{
	if(typeof(window.igcmbo_initialized)=="undefined")
	{
		if(typeof(ig_csom)=="undefined" || ig_csom==null)
			return;
		ig_csom.addEventListener(document, "mousedown", igcmbo_mouseDown, false);
		ig_csom.addEventListener(document, "mouseup", igcmbo_mouseUp, false);

		window.igcmbo_initialized=true;
		ig_currentCombo = null;
	}
}

// private - initializes the combo object on the client
function igcmbo_initCombo(comboId,comboProps,clientEvents)
{
	var comboElement = igcmbo_getElementById(comboId+"_Main");
	var oCombo=null;
	var metCur=false;
	for(var ci in igcmbo_comboState)
	{
		var c=igtbl_getGridById(ci);
		if(c && metCur)
			c.Loaded=false;
		if(comboId==ci)
			metCur=true;
	}
	oCombo=igcmbo_getComboById(comboId);
	if(oCombo)
	{
		if(!igtbl_isAtlas)
		{
			if(oCombo.Element==comboElement)
			{
				this.Loaded=true;
				igtbl_pbInited=false;
				return;
			}
			else if(comboElement 
				&& typeof(comboElement.length)!="undefined" 
				&& comboElement.length==2
				&& (comboElement[0]==oCombo.Element || comboElement[1]==oCombo.Element))
			{
				comboElement=oCombo.Element;
				if(comboElement)
				{
					comboElement.parentNode.removeChild(comboElement.nextSibling);
					comboElement.parentNode.removeChild(comboElement);
				}
			}
			

			if (oCombo.container && oCombo.container.parentNode)			
				oCombo.container.parentNode.removeChild(oCombo.container);			
		}
		if(oCombo.grid && oCombo.grid.MainGrid && oCombo.grid.MainGrid.parentNode && oCombo.grid.MainGrid.parentNode.parentNode)
			oCombo.grid.MainGrid.parentNode.parentNode.removeChild(oCombo.grid.MainGrid.parentNode);
		oCombo.grid=null;
		oCombo.webGrid=null;
		

		if(oCombo._unloadCombo)
			oCombo._unloadCombo();
	}

   oCombo = new igcmbo_combo(comboId,comboElement,comboProps,clientEvents);
   

   igcmbo_fireEvent(comboId,oCombo.Events.InitializeCombo,"(\""+comboId+"\");");
   
   oCombo.Loaded = true;
   igtbl_pbInited=false;
   return oCombo;
}

// private - constructor for the combo object
function igcmbo_combo(comboId,comboElement,comboProps,clientEvents) 
{

   
	igcmbo_initialize();

	this.Id=comboId;
	this.Element=comboElement;
	this.Type="WebCombo";
	this.UniqueId=comboProps[0];
	this.ClientUniqueId=comboProps[1];



	this.ClientID=comboProps[23];
	this.DropDownId=this.ClientUniqueId+"_main";
	this.DropImage1=comboProps[2];
	this.DropImage2=comboProps[3];
	this.ForeColor=comboProps[9];
	this.BackColor=comboProps[10];
	this.SelForeColor=comboProps[11];
	this.SelBackColor=comboProps[12];
	this.SelCssClass=" "+comboProps[27];
	







	this.DataTextField= comboProps[13]&&comboProps[13].length>0?unescape(comboProps[13].replace(/&nbsp;/gi," ")):unescape(comboProps[13]);
	this.DataValueField=comboProps[15]&&comboProps[15].length>0?unescape(comboProps[15].replace(/&nbsp;/gi," ")):unescape(comboProps[15]);
	this.HideDropDowns=comboProps[17];
	this.Editable=comboProps[18];
	this.ClassName=comboProps[19];
	this.Prompt=comboProps[20];
	this.ComboTypeAhead=comboProps[22];
	this.EnableProgressIndicator=comboProps[25];
	this.setEnableProgressIndicator=function(b)
	{
		this.EnableProgressIndicator=b;
		this.grid.EnableProgressIndicator=b;
	}
    
	this.Events= new igcmbo_events(clientEvents);

	this.Loaded=false;
	this.Dropped = false;
	this.NeedPostBack=false;
	this.CancelPostBack=false;
	this.TopHoverStarted=false;
	
	this.getDropDown = igcmbo_getDropDown;
	this.setDropDown = igcmbo_setDropDown;
	this.getDisplayValue = igcmbo_getDisplayValue;
	this.setDisplayValue = igcmbo_setDisplayValue;
	this._set_displayValue=function(value)
	{
		this.displayValue=value;
	};
	this.getDataValue = igcmbo_getDataValue;
	this.setDataValue = igcmbo_setDataValue;
	this._set_dataValue=function(value)
	{
		this.dataValue=value;
	};
	this.setWidth = igcmbo_setWidth;
	this.getWidth = igcmbo_getWidth;
	this._setInputWidth= _igcmbo_setInputWidth;
	this.getSelectedIndex = igcmbo_getSelectedIndex;
	this.setSelectedIndex = igcmbo_setSelectedIndex;
	this._setSelectedIndex = function(newIndex)
	{
		this._oldSelectedIndex = this.selectedIndex;
		this.selectedIndex = newIndex;
	}	
	this.selectedIndex = comboProps[21];
	this._oldSelectedIndex = comboProps[21];
	
	this.setFocusTop = igcmbo_setFocusTop;
	this.updateValue = igcmbo_updateValue;
	this.updatePostField = igcmbo_updatePostField;
	this.setSelectedRow = igcmbo_setSelectedRow;
	this._addOnGrid="__Grid";
	var grid = igtbl_getElementById(this.ClientID + this._addOnGrid);
	if(!grid)
	{
		this._addOnGrid="xxGrid";
		grid = igtbl_getElementById(this.ClientUniqueId + this._addOnGrid);
	}	
	if(!grid)
	{
		this._addOnGrid="$xGrid";
		grid = igtbl_getElementById(this.ClientUniqueId + this._addOnGrid);
	}
	if(grid!=null)
		grid.setAttribute("igComboId", this.ClientUniqueId);
	this._addOnGrid="xxGrid";
	this.grid = igtbl_getGridById(this.ClientUniqueId + this._addOnGrid);
	this.grid._isComboGrid = true;
	this.grid.Bands[0]._sqlWhere=comboProps[24];

	






	if(this.grid.DivElement.style.position=="")
		this.grid.DivElement.style.position="relative";
	
	





	if(ig_csom.IsFireFox && this.grid.MainGrid)
	{
	    this.grid.DivElement.style.height = this.grid.MainGrid.style.height;
	    
	}
	
	this.getGrid = igcmbo_getGrid;
	
	







	
    







    this.getInputBox=function()
    {
        if (this.inputBox==null || this.inputBox.parentElement==null)
        {
            this.inputBox = igcmbo_getElementById(this.ClientUniqueId + "_input");            
        }
        return this.inputBox;
    }
    this._set_displayValue(this.getInputBox().value);
	
	this._setInputWidth();

	
	
	if(this.ComboTypeAhead!=3)
		this.setDisplayValue(this.displayValue,false,true);

	
	else if(this.DataValueField)
	{
	    
	    var dataValueAsString = decodeURI ? decodeURI(comboProps[26]) : unescape(comboProps[26]);
		this._set_dataValue(this.getGrid().Bands[0].getColumnFromKey(this.DataValueField).getValueFromString(dataValueAsString));
	}

	
	
	// begin - editor control support

	igcmbo_getElementById(this.ClientID).Object=this;

	this.getVisible = igcmbo_getVisible;
	this.setVisible = igcmbo_setVisible;
	this.getValue = igcmbo_getValue;
	this.setValue = igcmbo_setValue;
	this.eventHandlers=new Object();
	this.addEventListener=igcmbo_addEventListener;
	this.removeEventListener=igcmbo_removeEventListener;
	// end - editor control support
	
	this.keyCount=0;
	this.typeAheadTimeout=null;
	this.highlightText=igcmbo_highlightText;
	
	







    this._getContainer=function()
    {
        if (this.container==null || this.container.parentElement==null)
        {
            this.container = document.getElementById(this.ClientUniqueId + "_container");
            
            if (this.container)
				this._orignalContainerParent = this.container.parentNode;
        }
        return this.container;
    }
    
	
	



	




	this.elemCal=this._getContainer();
	this.ExpandEffects = new igcmbo_expandEffects(comboProps[4], comboProps[5], comboProps[6], comboProps[7], comboProps[8], comboProps[9],this);
	this._relocate=function(npe1,npe2)
	{
		var pe=this.Element.parentNode,e=this.Element,ne=e.nextSibling,npe=npe1;
		pe.removeChild(e);
		pe.removeChild(ne);
		if(npe)
			try
			{
				npe.appendChild(e);
				npe.appendChild(ne);
			}
			catch(ex)
			{
				npe=null;
			}
		if(!npe)
		{
			npe2.appendChild(e);
			npe2.appendChild(ne);
		}
		e.style.zIndex=9999;
		ne.style.zIndex=9999;
	}
	this.focus=function()
	{	


   
		
		try
		{
			this.inputBox.focus();
		}
		catch(e){}
	}
	

	this._returnContainer = function()
	{		
		if (this._orignalContainerParent && this.container && this._orignalContainerParent != this.container.parentNode)
			this._move(this.container, this._orignalContainerParent);
	}
	this._unloadCombo = function()
	{
		var gn = this.Id
		if (this.Dropped)
			this.setDropDown(false);
		igcmbo_currentDropped = null;

		




		
		if (this.Element.parentNode)
			this.Element.parentNode.removeChild(this.Element)
		if (gn && ig_csom.IsIE)
		{
			igcmbo_comboState[this.Id].disposing = true;
			igtbl_dispose(igcmbo_comboState[gn]);
			delete igcmbo_comboState[gn];
		}
	}	
	this._move=function(e,par)
	{
		try
		{
			ig_csom._skipNew=true;
			e.parentNode.removeChild(e);
			par.appendChild(e);
			ig_csom._skipNew=false;
			
			e._moved=true;
			return true;
		}catch(ex){}
		return false;
	}
	





	this._reparentDropDown=_igcmbo_reparent;

	this.selectWhere = function(where)
	{
		var g = this.grid;
		if (g.LoadOnDemand != 3)
			return;
		g.Bands[0]._sqlWhere = where;
		this.updatePostField(this.getDisplayValue()); 
		var sortOrder = g._buildSortOrder();
		this.Loaded = false; 
		g.invokeXmlHttpRequest(g.eReqType.Custom, null, "combofilter\x02" + where + "\x04sort\x02" + sortOrder + "\x04displayValue\x02" + this.displayValue);
	}

	if(igtbl_isAtlas)
	{
		this.dispose = function()
		{
			if (this.grid && this.grid.MainGrid && this.grid.MainGrid.parentNode && this.grid.MainGrid.parentNode.parentNode)
				this.grid.MainGrid.parentNode.parentNode.removeChild(this.grid.MainGrid.parentNode);
			this.webGrid = null;
			if (this.grid && this.grid.Id)
			{
				this.grid.dispose();
				this.grid = null;
			}
			this._unloadCombo();
		}
		$get(this.ClientID).control=this;
	}

	igcmbo_comboState[this.Id]=this;
}

function igcmbo_onresize(evt,comboId)
{
	var c = igcmbo_getComboById(comboId);
	if (c && !c.inComboResize)
	{
		c.inComboResize=true;
		c._setInputWidth();	
		c.inComboResize=false;
	}
}

function igcmbo_imitateResize(comboId)
{
	var combo = igcmbo_getComboById(comboId);
	
	if(!combo || !combo.Element || !combo.Element.parentNode)
	{
		var timer = '_comboTimer_' + this.Id;
		var id = ig_shared[timer];
		if(id != null)
			try{window.clearInterval(id);}catch(id){}
		ig_shared[timer] = null;
		return;
	}
	if (combo.Element.parentNode && combo._imitationWidth != combo.Element.parentNode.offsetWidth)
	{
		var innerctl = igcmbo_getElementById(combo.ClientUniqueId + "_input");
		innerctl.style.width = "100%";
		igcmbo_onresize(null, comboId);
		combo._imitationWidth=combo.Element.parentNode.offsetWidth;
	}
	
}

// private
function _igcmbo_setInputWidth(width)
{
	
	


	if (!this.Element.offsetWidth && !this.Element.clientWidth) return;
	
	
	var innerctl = igcmbo_getElementById(this.ClientUniqueId + "_input");
	



	var cStyle=igtbl_getComputedStyle(this.Element);
	if(!cStyle)
		cStyle=this.Element.style;
	var peWidth = cStyle.width;

	if(!peWidth)
		peWidth = this.Element.width;
	if(!this.Loaded && (peWidth && peWidth.substr(peWidth.length-1,1)=="%"))
	{
		this.inComboResize=true;
		window.setTimeout("igcmbo_getComboById('"+this.Id+"').inComboResize=false",50);
		innerctl.style.width = "100%";
	}
	if(ig_shared.IsNetscape6)

	{
		
		var timer = '_comboTimer_' + this.Id;
		
		if(ig_shared[timer] != null)
			try{window.clearInterval(ig_shared[timer]);}catch(id){}
		
		ig_shared[timer] = window.setInterval("igcmbo_imitateResize('" + this.Id + "')", 500);
	}

	if(igtbl_isXHTML)
	{
	    




		if(typeof(width)=='undefined')
			width = this.Element.clientWidth;
		var bordersHeight = igtbl_dom.dimensions.bordersHeight(this.Element, true) + igtbl_dom.dimensions.bordersHeight(innerctl, true);
		var innerCtrlHeight=this.Element.offsetHeight-bordersHeight;
		innerCtrlHeight=innerCtrlHeight<0?0:innerCtrlHeight;
		if(!this.inComboResize || !ig_shared.IsIE6)
			innerctl.style.height=innerCtrlHeight+"px";
	}
	else
	{
		if(typeof(width)=='undefined')
			width=this.Element.offsetWidth;	
		if (width==0 && this.Element.clientWidth) width = this.Element.clientWidth;
	}
	if (width==0) return;
	
	









	var borders = igtbl_dom.dimensions.bordersWidth(this.Element, true) + igtbl_dom.dimensions.bordersWidth(innerctl, true);
	var image = igcmbo_getElementById(this.ClientUniqueId + "_img");
	var innerWidth = width - image.offsetWidth - borders - (ig_csom.IsIE ? 1 : 0);

	innerctl.style.width = (innerWidth > 0 ? innerWidth : width - image.offsetWidth) + "px";

	




	if (ig_csom.IsIE && innerctl.getBoundingClientRect)
	{
		var setWidth = parseInt(innerctl.style.width);
		while (setWidth > 0 && igtbl_getAbsBounds(image).y > igtbl_getAbsBounds(innerctl).y)
		{
			setWidth--;
			innerctl.style.width = setWidth + "px";
		}
	}
}

// public - sets the width of the WebCombo to the passed in value
function igcmbo_setWidth(width) {
	if(width==0)
		return;
	this._setInputWidth(width);	
	this.Element.style.width = width+"px";
}

// public - returns the CSS width of the combo element.
function igcmbo_getWidth() {return this.Element.style.width;}

// private - event initialization for combo object
function igcmbo_getDropDown(){return this.Dropped;}







function _igcmbo_reparent()
{	
	var tPan=this.transPanel,
		pan=this.container,
		edit=this.inputBox;

		var x=pan.parentNode,
			body=window.document.body;

		var f=x.tagName,
			bp=body.parentNode,
			par=this.inputBox.form;
		
		if(!par)
			par = document.forms[0];
		if(f=="FORM")
		{
			par=null;
			if(x.style)
				if((f=x.style.position)!=null)
					if(f.toLowerCase()=="absolute")
						par=body;
		}
		else if(f=="BODY"||f=="HTML")
			par=null;
		if(par)
			if(!this._move(pan,par))
				if(par!=body)
					this._move(pan,body);
}

// private - event initialization for combo object
function igcmbo_setDropDown(bDrop)
{
	if(this.Element.style.display=="none" || this.Element.getAttribute("disabled")=="disabled") return;	
	var tPan=this.transPanel,
		pan=this.container,
		edit=this.inputBox;
	
	edit =this.getInputBox();
	pan = this._getContainer();
	
	if(bDrop == true)
	{
		if(this.Dropped == true)
			return;
 		if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.BeforeDropDown,"(\""+this.ClientUniqueId+"\");"))
	 		return;






		if(this.grid && this.grid.Element)
			this.grid.Element.setAttribute("mouseDown",null);

		this.focus();		
		var editH=edit.offsetHeight,
			editW=edit.offsetWidth,
			e=edit,
			x=pan.parentNode,
			body=window.document.body;
		if(editH==null)
			editH=20;
		var f=x.tagName,
			bp=body.parentNode,
			par=this.inputBox.form;
		
		if(!par)
			par = document.forms[0];
		if(f=="FORM")
		{
			par=null;
			if(x.style)
				if((f=x.style.position)!=null)
					if(f.toLowerCase()=="absolute")
						par=body;
		}
		else if(f=="BODY"||f=="HTML")
			par=null;
		if(par)
			if(!this._move(pan,par))
				if(par!=body)
					this._move(pan,body);
		this.ExpandEffects.applyFilter();

		
		if(this.grid && this.grid.oActiveRow)
			this.grid.oActiveRow.renderActive(false);

		pan.style.visibility="visible";
		pan.style.display="";		

		
		if(this.grid && this.grid.oActiveRow)
			this.grid.oActiveRow.renderActive();

		if(pan.offsetHeight<5&&par&&par!=body)
			this._move(pan,body);
		var panH=pan.offsetHeight,
			panW=pan.offsetWidth,
			z=0;
		if((x=this.elemCal.offsetHeight)!=panH)
			pan.style.height=(panH=x)+"px";
		if((x=this.elemCal.offsetWidth)!=panW)
			pan.style.width=(panW=x)+"px";
		
		
		
		var zIndex = 10002;
		var elem = this.Element;
		while(elem)
		{
			if(elem.nodeName == 'BODY' || elem.nodeName == 'FORM')
				break;
			var z = ig_shared.getStyleValue(null, 'zIndex', elem);
			if(z && z.substring) z = (z.length > 4 && z.charCodeAt(0) < 58) ? parseInt(z) : 0;
			if(z && z >= zIndex) zIndex = z + 1;
			elem = elem.parentNode;
		}
		if(tPan==null&&ig_csom.IsIEWin)
		{	
			this.transPanel=tPan=ig_csom.createTransparentPanel();
			if(tPan){tPan.Element.style.zIndex=zIndex;}			
		}		
		pan.style.zIndex=zIndex+1;
		var ok = 0,
			pe = e,
			y = 0,
			x = 0,
			passedMain = false,
			fixedPositioning = false;
		while(e!=null)
		{
			
			if(window.__smartNav && e==body)
				break;
			if(ok<1||e==body)
			{
				if((z=e.offsetLeft)!=null)
					x+=z;
				if((z=e.offsetTop)!=null)
					y+=z;
				if(e==this.Element)
					passedMain=true;
				if(passedMain && e.style.borderLeftWidth)
				{
					var blw=parseInt(e.style.borderLeftWidth,10);
					if(!isNaN(blw))
						x+=blw;
				}
				if(e.style.borderTopWidth)
				{
					var btw=parseInt(e.style.borderTopWidth,10);
					if(!isNaN(btw))
						y+=btw;
				}
			}
			if(e.nodeName=="HTML")
				body=e;
			if(e==body)
			{
				if(igtbl_isXHTML && body.tagName=="BODY")
					body=body.parentNode;
				break;
			}
			





			if (e.id != this.ClientUniqueId + "_input")
			{
			    z=e.scrollLeft;
			    if(z==null||z==0)
				    z=pe.scrollLeft;
			    if(z!=null&&z>0)
				    x-=z;
			    z=e.scrollTop;
			    if(z==null||z==0)
				    z=pe.scrollTop;
			    if(z!=null&&z>0)
				    y-=z;
			}	
			pe=e.parentNode;
			e=e.offsetParent;
			if(pe.tagName=="TR")
				pe=e;
			if(e==body&&pe.tagName=="DIV")
			{
				e=pe;
				ok++;
			}
			if (e && e.style && e.style.position == "fixed")
				fixedPositioning = true;
		}
		
		




















































		y+=editH;		
		if((z=this.dropDownAlignment)==1)
			x-=(panW-editW)/2;
		else if(z==2)
			x-=panW-editW;
		z=body.clientHeight;
		if(z==null||z<20)
		{
			z=pe.offsetHeight;
			f=body.offsetHeight;
			if(f>z)
				z=f;
		}
		else if(!ig_csom.IsNetscape6)
		{
			if(bp&&(f=bp.offsetHeight)!=null)
				if(f>panH&&f<z)
					z=f-10;
		}
		if((f=body.scrollTop)==null)
			f=0;
		if(f==0&&bp)
			if((f=bp.scrollTop)==null)
				f = 0;
		if (fixedPositioning && f > 0)
			y += f;
		if(y-f-3>panH+editH)
			if(z<y-f+panH)
				y-=panH+editH;
		z=body.clientWidth;
		if(z==null||z<20)
		{
			z=pe.offsetWidth;
			f=body.offsetWidth;
			if(f>z)
				z=f;
		}
		else
		{
			if(bp&&(f=bp.offsetWidth)!=null)
				if(f>panW&&f<z)
					z=f-20;
		}
		if((f=body.scrollLeft)==null)
			f=0;
		if(f==0&&bp)
			if((f=bp.scrollLeft)==null)
				f=0;
		if(x+panW>z+f)
			x=z+f-panW;
		if(x<f)
			x=f;
		if(x<0)
			x=0;
		if(y<0)
			y=0;
		if(ig_csom.IsMac&&(ig_csom.IsIE||ig_csom.IsSafari))
		{
			x+=ig_csom.IsIE?5:-5;
			y+=ig_csom.IsIE?11:-7;
		}
		pan.style.left=x+"px";
		pan.style.top=y+"px";
		this.ExpandEffects.applyFilter(true);
		if(tPan!=null)
		{
			

			tPan.setPosition((y - 1) + "px", (x - 1) + "px", (panW + 2) + "px", (panH + 2) + "px");
			tPan.show();
		}
		var dropdowngrid=igcmbo_getElementById(this.ClientUniqueId+this._addOnGrid+"_main");
		if(document.all && dropdowngrid != null) {
			if(this.webGrid)
				this.webGrid.Element.setAttribute("noOnResize",true);
			igtbl_activate(this.ClientUniqueId + this._addOnGrid);
			if(this.webGrid)
				this.webGrid.Element.removeAttribute("noOnResize");
		}
		




		
		this.Dropped = true;
		if(this.grid && this.grid.getActiveRow())
			igtbl_scrollToView(this.grid.Id,this.grid.getActiveRow().Element);
		





		igcmbo_displaying=igcmbo_currentDropped = this;
 		igcmbo_fireEvent(this.ClientUniqueId,this.Events.AfterDropDown,"(\""+this.ClientUniqueId+"\");");
 		this._internalDrop = true;
 		setTimeout(igcmbo_clearInternalDrop, 100);
	}
	else
	{
		if(this.Dropped == false)
			return;
		var grid = igcmbo_getElementById(this.ClientUniqueId + "_container");
 		if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.BeforeCloseUp,"(\""+this.ClientUniqueId+"\");")) {
 			return;
		}
		if(this.webGrid)
			this.webGrid.Element.setAttribute("noOnResize",true);

		if (pan != null)
		{
			pan.style.visibility = "hidden";
			pan.style.display = "none";
		}

		if(tPan!=null)
		{
			tPan.hide();
		}
		this.Dropped = false;		
		
		var inputbox = igcmbo_getElementById(this.ClientUniqueId + "_input");
		igcmbo_currentDropped = null;
 		igcmbo_fireEvent(this.ClientUniqueId,this.Events.AfterCloseUp,"(\""+this.ClientUniqueId+"\");");
		








		if (this.grid && this.grid.event && typeof(this.grid.event.srcElement)!="undefined" && typeof(this.grid.event.srcElement)!="unknown")
			if(igtbl_isAChildOfB(this.grid.event.srcElement,this.Element) || igtbl_isAChildOfB(this.grid.event.srcElement,this.grid.Element))
				igtbl_cancelEvent(this.grid.event);
 		
		if(this.webGrid)
		{
			igcmbo_wgNoResize=this.webGrid;
	 		setTimeout(igcmbo_clearnoOnResize, 100);
		}
	}
}

function igcmbo_clearInternalDrop()
{
	if(igcmbo_currentDropped)
		igcmbo_currentDropped._internalDrop = null;
}

var igcmbo_wgNoResize=null;
function igcmbo_clearnoOnResize() {
	if(igcmbo_wgNoResize){
		
		if(igcmbo_wgNoResize.Element)
			igcmbo_wgNoResize.Element.removeAttribute("noOnResize");
		igcmbo_wgNoResize=null;
	}
}

function igcmbo_editkeydown(evnt,comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
	if(oCombo && oCombo.Loaded)
	{
		var keyCode = (evnt.keyCode);
		var newValue = igcmbo_srcElement(evnt).value;
    	if(igcmbo_fireEvent(oCombo.ClientUniqueId,oCombo.Events.EditKeyDown,"('"+oCombo.ClientUniqueId+"','"+escape(newValue)+"',"+keyCode+");"))
    		return igtbl_cancelEvent(evnt);
		if(oCombo && oCombo.eventHandlers && oCombo.eventHandlers["keydown"] && oCombo.eventHandlers["keydown"].length>0)
		{
			var ig_event=new ig_EventObject();
			ig_event.event=evnt;
			for(var i=0;i<oCombo.eventHandlers["keydown"].length;i++)
				if(oCombo.eventHandlers["keydown"][i].fListener)
				{
					if(keyCode==9 || keyCode==13 || keyCode==27)
						oCombo.setDisplayValue(newValue,false);
					oCombo.eventHandlers["keydown"][i].fListener(oCombo,ig_event,oCombo.eventHandlers["keydown"][i].oThis);
					
					if(ig_event.cancel || keyCode==13)
						return igtbl_cancelEvent(evnt);
				}
		}				
		if (oCombo.ComboTypeAhead!=0 && igcmbo_isCountableKey(keyCode))
			oCombo.keyCount++;
		if(!oCombo.Editable && (oCombo.ComboTypeAhead==1 || oCombo.ComboTypeAhead==0))
		{
			if(oCombo.DataTextField.length>0)
				column=oCombo.getGrid().Bands[0].getColumnFromKey(oCombo.DataTextField);
			else column=oCombo.getGrid().Bands[0].Columns[0];
			var s=String.fromCharCode(evnt.keyCode);			
			if (igcmbo_isCountableKey(evnt.keyCode))
			{
				var cell=null;
				var row;
				cell = igcmbo_typeaheadFindCell(oCombo,s, column, oCombo.lastKey);
				if(cell)
				{
					oCombo.lastKey = s;
					text = igcmbo_processTypeAhead(oCombo,oCombo.getGrid(),cell);
					newValue=text;
				}
			}
			else
			{
				var oText=igcmbo_ProcessNavigationKey(oCombo,column,evnt.keyCode,evnt);
				if(oText)
					newValue=oText;
			}
		}
		if(oCombo.displayValue!=newValue)
		{
			if(oCombo.Editable)
				oCombo._set_dataValue(newValue);
			oCombo.updatePostField(newValue);
			oCombo._set_displayValue(newValue);
		}
		
		if(keyCode==38 || keyCode==40)
		{
			if(evnt.altKey)
			{
				oCombo.setDropDown(keyCode==40);
				oCombo.setFocusTop();
			}
			return igtbl_cancelEvent(evnt);
		}
		

        else if((keyCode==27 || keyCode==9) && oCombo.Dropped)
        {
            oCombo.setDropDown(false);
            oCombo.setFocusTop();
        }
    }
}






function igcmbo_dropdownkeydown(evnt,comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
	var keyCode = (evnt.keyCode);
	if(keyCode==27 && oCombo.Dropped)
	{
		oCombo.setDropDown(false);
		oCombo.setFocusTop();
	}
}

// private function
// used to determine what keys will trigger type ahead counter increment/decrements
function igcmbo_isCountableKey(keyCode)
{		
	if (keyCode<32)
		return false;
	switch(keyCode)
	{
		//end//right//home//left
		case 35: case 39: case 36: case 37:
		//back//del
		case 8: case 46:
		//up//down
		case 38: case 40:
			return false;
			break;
	}	
	return true;
}
// private function
function igcmbo_arrowKeyNavigation(oCombo, oGrid, oRow, column)
{
	var text = null;
	if (oRow != null) 
	{
		






	
		oCombo._inArrowKeyNavigation = true;
		oGrid.setActiveRow(oRow);
		

		if (ig_csom.IsFireFox && oGrid.getActiveRow() != oRow)
			oGrid.setActiveRow(oRow);
		
		oGrid.clearSelectionAll();
		oRow.setSelected(true);
		



		oRow.scrollToView();
		oCombo._setSelectedIndex(oRow.getIndex());		
		var cell = oRow.getCell(column.Index);
		text = cell.getValue(true);
		oCombo.updateValue(text, true);		
		if(oCombo.DataValueField)
			oCombo._set_dataValue(oRow.getCellFromKey(oCombo.DataValueField).getValue());
		igtbl_updatePostField(oGrid.Id);
		oCombo._inArrowKeyNavigation = false;
	}
	return text;
}
// private function
function igcmbo_highlightText(high)
{
	if(typeof(high)=="undefined")
		high=true;
	var oInput = document.getElementById(this.ClientUniqueId + "_input");
	if (null==oInput)return;
	var oInTextRange= oInput.createTextRange?oInput.createTextRange():null;
	if (this.Editable)
	{






		var chrPos = 0;
		if (this.typeAheadTimeout && this.ComboTypeAhead == 3)
			chrPos = oInput.value.length;
		else if ((this.ComboTypeAhead == 2 || this.ComboTypeAhead == 3) && this.lastKey)
			chrPos = this.lastKey.length;
		if (oInTextRange)
		{
			oInTextRange.moveStart("character", chrPos);
			oInTextRange.moveEnd("textedit");
			oInTextRange.select();
		}
		else 
		{
			




			try
			{
				if(typeof(oInput.selectionStart) != "undefined")
				{
					oInput.selectionStart =  chrPos;
					oInput.selectionEnd = oInput.value.length;
				}
			} catch(e) {}
		}
	}
	else
	{
		if(high)
		{
            



			if(oInput.className.indexOf(this.SelCssClass)==-1 && this.SelCssClass != " ")
				oInput.className+=this.SelCssClass;

			if (oInTextRange)
			{
				oInTextRange.moveStart("character",0);
				oInTextRange.moveEnd("character",-oInput.value.length);
				oInTextRange.select();
				


			}
			




			else if (oInput.offsetHeight > 0 && oInput.selectionStart)
			{
				oInput.selectionStart = 0;
				oInput.selectionEnd = 0;
			}
		}
		else
		{
			var sccIndex=oInput.className.indexOf(this.SelCssClass);
			



			if(sccIndex!=-1 && this.SelCssClass != " ")
				oInput.className=oInput.className.substr(0,sccIndex);
		}
	}
}
// private function
function igcmbo_typeAheadReset(comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
	if (oCombo)
	{


		oCombo._keyCode=null;

		oCombo.keyCount=0;
		oCombo.typeAheadTimeout=null;
		if(oCombo.ComboTypeAhead==2	|| oCombo.ComboTypeAhead==3)
			oCombo.lastKey="";

		
		if(oCombo.ComboTypeAhead==3)
		{
			oCombo.selectedIndex=-1;
			oCombo._set_dataValue(null);
			oCombo.updatePostField(oCombo.displayValue);
		}
	}
}
// private
function igcmbo_typeaheadFindCell(oCombo,charFromCode, column, lastKey)
{
		var cell=null;
		var re=new RegExp("^"+igtbl_getRegExpSafe(charFromCode),"gi");
		if(lastKey!=charFromCode) cell=column.find(re);
		else if(cell==null){
			cell=column.findNext();
			if(cell==null) cell=column.find(re);
		}
		return cell;
}
//private
function igcmbo_processTypeAhead(oCombo,oGrid,oCell)
{
	var text=null;
	text=oCell.getValue(true);
	var oRow=oGrid.getActiveRow();
	oGrid.clearSelectionAll();
	if(oRow) oRow.setSelected(false);
	oRow=oCell.getRow();
	oGrid.setActiveRow(oRow);
	oRow.setSelected(true);
	
	oCombo._setSelectedIndex(oRow.getIndex());
	
	
	
	oCombo.updateValue(text, true);
	if(oCombo.DataValueField) oCombo._set_dataValue(oRow.getCellFromKey(oCombo.DataValueField).getValue());
	igtbl_updatePostField(oGrid.Id);
	oCombo.highlightText();								
	return text;
}
	
//private
function igcmbo_ProcessNavigationKey(oCombo,column,keyCode,evnt)
{
	var oRow=null;
	var oGrid=oCombo.getGrid();
	var oText=null;
	switch(keyCode)
	{
		case 8:
		case 46:
			if (oCombo.Editable){
				document.selection.createRange().text="";
				oCombo.lastKey=igcmbo_srcElement(evnt).value;
			}
			break;
		case 40:
			






            
			if(evnt.altKey)break;
			oRow=oGrid.getActiveRow();
			if(oRow!=null)
			{
				oRow.setSelected(false);
				oRow=oRow.getNextRow();
				if(oRow)
					oText=igcmbo_arrowKeyNavigation(oCombo,oGrid,oRow,column);
			}
			else if(oGrid.Rows.length>0)
				oText=igcmbo_arrowKeyNavigation(oCombo,oGrid,oGrid.Rows.getRow(0),column);
			break;
		case 38:
            
            if(evnt.altKey)break;
			oRow = oGrid.getActiveRow();
			if(oRow != null)
			{
				oRow.setSelected(false);
				oRow = oRow.getPrevRow();
				if(oRow)
					oText = igcmbo_arrowKeyNavigation(oCombo,oGrid,oRow,column);
			}
			else if(oGrid.Rows.length > 0)
				oText=igcmbo_arrowKeyNavigation(oCombo,oGrid,oGrid.Rows.getRow(oGrid.Rows.length-1),column);				
			break;
	}
	return oText;
}		

function _igcmbo_isDeleteKey(keyCode)
{
    return keyCode == 8 || keyCode ==46;
}

function igcmbo_editkeyup(evnt,comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
	if(oCombo&&oCombo.Loaded)
	{
		var keyCode=evnt.keyCode;
		





		
		









		if (keyCode==9 || keyCode==16 )
		{	
			return;
		}










		if(oCombo._keyCode && 
		    keyCode!=8 && keyCode!=46 
		    && keyCode != 40 && keyCode != 38
		    )
			keyCode=oCombo._keyCode;
		else
			oCombo._keyCode=null;







		var charFromCode= igcmbo_isCountableKey(keyCode) ?  String.fromCharCode(keyCode) : String.fromCharCode(null);
		var newValue = oCombo.Editable ? igcmbo_srcElement(evnt).value:charFromCode;
    	if(igcmbo_fireEvent(oCombo.ClientUniqueId,oCombo.Events.EditKeyUp,"(\""+oCombo.ClientUniqueId+"\",\""+escape(newValue) +"\","+keyCode+");"))
    		return igtbl_cancelEvent(evnt);		
		if (0==oCombo.ComboTypeAhead)
		{
		    







    		if(oCombo.displayValue!=newValue)
	    	{
				if(oCombo.Editable)
					oCombo._set_dataValue(newValue);
		    	oCombo.updatePostField(newValue);
			    oCombo._set_displayValue(newValue);
		    }
			return;
		}	
		var bCountableKey=(oCombo._keyCode!=null || igcmbo_isCountableKey(keyCode));
		if (bCountableKey)
			--oCombo.keyCount;
		var lastKey=oCombo.lastKey;
		if(oCombo.ComboTypeAhead==2	|| oCombo.ComboTypeAhead==3)
			if (oCombo.Editable)
				charFromCode=newValue;
			else
			{
				charFromCode=(bCountableKey?(lastKey?lastKey:"")+newValue:null);
				oCombo.lastKey=charFromCode;
			}
		else
			oCombo.lastKey = charFromCode;
		if (oCombo.keyCount<=0)
		{
			var oGrid=oCombo.getGrid();
			if(oGrid==null || oGrid.Bands==null)
				return;
			var column=null;
			if(oCombo.DataTextField.length>0)
				column=oGrid.Bands[0].getColumnFromKey(oCombo.DataTextField);
			else
			{
				var colNo=0;
				column=oGrid.Bands[0].Columns[colNo];
			}
			if(column==null)
				return;			
			var text;
			var cell;
			var oCurrentRow=null;				
			if(charFromCode && bCountableKey || (keyCode==8 || keyCode==46) && oCombo.ComboTypeAhead==3)
			{
				if(oCombo.Editable && oCombo.ComboTypeAhead==3)
				{
					oCombo.lastKey=charFromCode;
					if(oCombo.typeAheadTimeout!=null)
						clearTimeout(oCombo.typeAheadTimeout);
					
					
					oCombo.typeAheadTimeout = setTimeout("igtbl_processSuggestTypeAhead('"+oCombo.ClientUniqueId+"','"+newValue.replace(/\\/g,"\\\\").replace(/\'/g,"\\'")+"')",500);
				}
				else
				{
					cell=igcmbo_typeaheadFindCell(oCombo,charFromCode,column,lastKey);
					if(cell!=null)
					{
						oCombo.lastKey=charFromCode;
						oCombo._set_displayValue(newValue);
						text=igcmbo_processTypeAhead(oCombo,oGrid,cell);
						if(oCombo.typeAheadTimeout!=null)
							clearTimeout(oCombo.typeAheadTimeout);
						oCombo.typeAheadTimeout = setTimeout("igcmbo_typeAheadReset('"+oCombo.ClientUniqueId+"')",1000);
						if (!oCombo.Editable)
							newValue=text;
					}
					else
					{
						var oEditor=document.getElementById(oCombo.ClientUniqueId + "_input");
						if (!oCombo.Editable)
						{						
							var oActRow=oGrid.getActiveRow();												
							if (oActRow)
							{
								
								var curRowValue = null;
								if(oCombo.DataTextField)
									curRowValue = oActRow.getCellFromKey(oCombo.DataTextField).getValue();
								else
									curRowValue = oActRow.getCell(0).getValue();
								if(curRowValue != null)
									oEditor.value = curRowValue;
							}
							newValue=oEditor.value;										
							oCombo.highlightText();						
						}
						else  // if editable and no row is found we should move off all rows since this may be a new value
						{
							oGrid.clearSelectionAll();
							oGrid.setActiveRow(null);
							oCombo._setSelectedIndex(-1);
							newValue=charFromCode;
						}
						if(oCombo.typeAheadTimeout!=null)
							clearTimeout(oCombo.typeAheadTimeout);
						oCombo.typeAheadTimeout=setTimeout("igcmbo_typeAheadReset('"+oCombo.ClientUniqueId+"')",1000);
					}
				}
			}
			else
			{				
				var oText=igcmbo_ProcessNavigationKey(oCombo,column,evnt.keyCode,evnt);
				if (null!=oText)
					newValue=oText;
				else if(!newValue)
					oCombo._set_dataValue(null);
			}
		}
		else
		{
			if(oCombo.typeAheadTimeout!=null)
				clearTimeout(oCombo.typeAheadTimeout);
			oCombo.typeAheadTimeout=setTimeout("igcmbo_typeAheadReset('"+oCombo.ClientUniqueId+"')",500);
		}
		




		if(oCombo.displayValue!=newValue && oCombo.Editable)
		{
			if(oCombo.Editable)
				oCombo._set_dataValue(newValue);
			oCombo.updatePostField(newValue);
			oCombo._set_displayValue(newValue);
		}
	}
}

function igcmbo_keypress(evnt, comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
	if(!oCombo) return;
	oCombo._keyCode=evnt.keyCode;
}

function igtbl_processSuggestTypeAhead(comboId,newValue)
{
	var oCombo=igcmbo_getComboById(comboId);
	
	




	oCombo.selectWhere("["+oCombo.DataTextField+"] LIKE '"+newValue.replace(/\'/g,"''")+"%'");
	oCombo.setDropDown(true);
	
	if(oCombo.EnableProgressIndicator)
	{
		
		oCombo.grid._progressIndicator.display();
	}
	
	
	if(oCombo.lastKey!=undefined && oCombo.lastKey.length===0)
		oCombo.lastKey = oCombo.inputBox.value;
	oCombo.setFocusTop();
}

function igcmbo_onfocus(evnt,comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
	if(!oCombo)
		return;
	oCombo.setFocusTop();
}

function igcmbo_oneditfocus(evnt,comboId)
{
	var oCombo=igcmbo_getComboById(comboId);
	if(!oCombo)
		return;
	if(oCombo.typeAheadTimeout!=null)
		oCombo.highlightText();
	else
		oCombo.setFocusTop();
}

function igcmbo_onblur(evnt,comboId)
{
	var oCombo = igcmbo_getComboById(comboId);
		
	if(!oCombo || !oCombo.Loaded) 
		return;
		
	if(oCombo.Element.getAttribute("noOnBlur"))
		return;

	if (oCombo.NeedPostBack && !igtbl_pbInited)
	{
		igcmbo_doPostBack(comboId);
		return;
	}
		
	// moved this outside the loop 
	var inputbox = igcmbo_getElementById(oCombo.ClientUniqueId + "_input");		
	if (inputbox)
	{
		if (!oCombo.Editable)
		{
			

			oCombo.highlightText(false);
		}
		else
		{
			var oGrid = oCombo.getGrid();
			var oEditor=document.getElementById(oCombo.ClientUniqueId + "_input");
			var oActRow=oGrid.getActiveRow();
			if (oActRow)
			{			
				var oCellValue = oCombo.DataTextField!=null && oCombo.DataTextField!="" ? oActRow.getCellFromKey(oCombo.DataTextField).getValue():oActRow.getCell(0).getValue(); 
				if (oEditor.value!=oCellValue)				
				{
					oGrid.clearSelectionAll();
					oGrid.setActiveRow(null);	
					oCombo._setSelectedIndex(-1);
					//oCombo.selectedIndex = -1;
					oCombo.updateValue(oEditor.value, true);
					igtbl_updatePostField(oGrid.Id);
							
				}
			}
			if(oCombo.getDisplayValue()!=oEditor.value)
			{
				oCombo._set_displayValue(oEditor.value);
				if(oCombo.Editable)
					oCombo._set_dataValue(oEditor.value);
				oCombo.updatePostField(oEditor.value);	
			}
		}
	}

	if(oCombo!=igcmbo_displaying) 
	{
        



	    if(oCombo.Dropped)
	        oCombo.setDropDown(false);
		


	}

	if (document.all && oCombo.Element.contains(evnt.toElement))
	{
    }
    else
    {	
		if(oCombo.webGrid != null) {
			var container = igcmbo_getElementById(oCombo.ClientUniqueId + "_container");
			if(oCombo._internalDrop || oCombo.Element.getAttribute("noOnBlur") || evnt.explicitOriginalTarget && igtbl_isAChildOfB(evnt.explicitOriginalTarget, container))
				return;
			if(oCombo.eventHandlers && oCombo.eventHandlers["blur"] && oCombo.eventHandlers["blur"].length>0)
			{
				var ig_event=new ig_EventObject();
				ig_event.event=evnt;
				for(var i=0;i<oCombo.eventHandlers["blur"].length;i++)
					if(oCombo.eventHandlers["blur"][i].fListener)
					{
						oCombo.eventHandlers["blur"][i].fListener(oCombo,ig_event,oCombo.eventHandlers["blur"][i].oThis);
						if(ig_event.cancel)
							return igtbl_cancelEvent(evnt);
					}
			}
		}
    }
}
function igcmbo_setFocusTop() {
	
	
	this.Element.setAttribute("noOnBlur",true);
	this.focus();
	if(!this.typeAheadTimeout)
		this.highlightText();
	else if(this.ComboTypeAhead==3)
	{
		if(typeof(this.inputBox.createTextRange)!="undefined")
		{
			var tr=this.inputBox.createTextRange();
			tr.moveStart("character",this.inputBox.value.length);
			tr.moveEnd("character",this.inputBox.value.length);
		}
		else
		{
			
            if(this.inputBox.value != null)
            {				
				
				var oInput = document.getElementById(this.ClientUniqueId + "_input");
				if (oInput)
				{
					

					try
					{
						oInput.selectionStart=this.inputBox.value.length;
						oInput.selectionEnd=this.inputBox.value.length;
					}
					catch(e){}
				}					
			}
		}
	}
	window.setTimeout("igcmbo_cancelNoOnBlur('"+this.Id+"')",100);
	




	
}

function igcmbo_cancelNoOnBlur(comboId)
{
	var combo=igcmbo_getComboById(comboId);
	if(combo)
		combo.Element.removeAttribute("noOnBlur");
}

// private - event initialization for combo object
function igcmbo_events(events){
	this.InitializeCombo=events[0];
	this.EditKeyDown=events[1];
	this.EditKeyUp=events[2];
	this.BeforeDropDown=events[3];
	this.AfterDropDown=events[4];
	this.BeforeCloseUp=events[5];
	this.AfterCloseUp=events[6];
	this.BeforeSelectChange=events[7];
	this.AfterSelectChange=events[8];
}

function igcmbo_gridmouseover(gridName, itemId) {
	var grid = igtbl_getGridById(gridName);
	var cell = igtbl_getCellById(itemId);
	if(cell == null)
		return;
	igtbl_clearSelectionAll(gridName);
	igtbl_selectRow(gridName,cell.Row.Element.id);
}

function igcmbo_gridkeydown(gridName, itemId, keyCode) {
	var oCombo = igcmbo_currentDropped;
	igtbl_clearSelectionAll(gridName);
	if(keyCode == 27 || keyCode == 10)
	{
		oCombo.setDropDown(false);
		oCombo.setFocusTop();
	}
}

function igcmbo_gridrowactivate(gridName, itemId) {	
	var oCombo = igcmbo_getComboByGridName(gridName);
	//var oCombo = igcmbo_currentDropped;
	var row = igtbl_getRowById(itemId);
	if(oCombo == null || row == null)
		return;
	if(oCombo.DataTextField.length > 0) {
		cell = row.getCellFromKey(oCombo.DataTextField);
	}
	else
		cell = row.getCell(0);		
	

	
	var valueCell = oCombo.DataValueField.length>0 ? row.getCellFromKey(oCombo.DataValueField) : row.getCell(0);
	if(cell != null) {
		if(valueCell)oCombo._set_dataValue(valueCell.getValue());
		
		var v = cell.getValue(true);
		
		oCombo._setSelectedIndex(row.getIndex());
		
		//oCombo.selectedIndex = row.getIndex();
		oCombo.updateValue(v, true);
		



		




		
		

		oCombo.setFocusTop();
	}
}

function igcmbo_setSelectedRow(row) {
	var cell = null;
	if(this.DataValueField.length > 0) 
	{
		cell = row.getCellFromKey(this.DataValueField);
		this.setDataValue(cell, false);
		if(this.Element.style.display!="none")
			this.setFocusTop();
	}
}

function igcmbo_gridmouseup(gridName, itemId) {
	var grid = igtbl_getGridById(gridName);
	var row = igtbl_getRowById(itemId);
	if(row == null)
		return;
	




	var oCombo = igcmbo_currentDropped;
	if(oCombo != null)
	{
		oCombo.setSelectedRow(row);
		oCombo.setDropDown(false);
		if (ig_csom.IsFireFox && grid.event && grid.event.target && grid.event.target.type == "checkbox")
		{
		    


		    oCombo._getContainer().style.display = "";
	        window.setTimeout("igcmbo_getComboById('" + oCombo.Id + "')._getContainer().style.display = 'none'", 100);
	    }
		
		





		if(!igtbl_dom.isTag(igtbl_srcElement(grid.event), "A"))
			oCombo.setFocusTop();
			
		




		if(grid.event)
			ig_cancelEvent(grid.event);
	}
}

function igcmbo_getSelectedIndex() {
	return this.selectedIndex;
}

function igcmbo_setSelectedIndex(index)
{
	if(index>=0 && index<this.grid.Rows.length)
		this.setSelectedRow(this.grid.Rows.getRow(index));
}

function igcmbo_getVisible() {
	if(this.Element.style.display == "none" || this.Element.style.visibility == "hidden")
		return false;
	else
		return true;
}

function igcmbo_setVisible(bVisible,left,top,width,height){
	if(bVisible){
		this.Element.style.display = "";
		this.Element.style.visibility = "visible";
		igcmbo_displaying=this;
		if(top)
		{
			this.Element.style.position="absolute";
			this.Element.style.top=top+"px";
			this.Element.style.left=left+"px";
		}
		if(height)
		{
			this.Element.style.height=height;
			this.setWidth(width);
		}
		if(bVisible)
		{
			this.focus();
			this.highlightText();
		}
	}
	else
	{
		if(this.Dropped)
			this.setDropDown(false);
		

		if(this.getInputBox()!=null && this.getDisplayValue()!=this.getInputBox().value)
		{
			this._set_displayValue(this.getInputBox().value);
			if(this.Editable)
				this._set_dataValue(this.getInputBox().value);
			this.updatePostField(this.getInputBox().value);	
		}
		this.Element.style.display = "none";
		this.Element.style.visibility = "hidden";
		igcmbo_displaying=null;
	}
}

function igcmbo_getDisplayValue()
{
	return this.displayValue;
}

function igcmbo_getDataValue()
{
	return this.dataValue;
}

function igcmbo_setDisplayValue(newValue, bFireEvent, bNoPostUpdate)
{
	var cell=newValue;
	if(cell==null || typeof(cell)!="object")
	{
		this.updateValue(newValue, bFireEvent);
		var re = new RegExp("^"+igtbl_getRegExpSafe(newValue)+"$", "g");
		var column = null;
		if(this.DataTextField.length > 0) {
			column = this.grid.Bands[0].getColumnFromKey(this.DataTextField)
		}
		else {
			//var colNo = 0;
			column = this.grid.Bands[0].Columns[0];
		}
		if(column == null)
			return;
		if (this.selectedIndex>-1 && this.grid.Rows.length>0 && this.selectedIndex < this.grid.Rows.length)		
			cell = this.grid.Rows.getRow(this.selectedIndex).getCellByColumn(column);
		else
			cell = column.find(re);
	}
	else
		this.updateValue(cell.getValue(), bFireEvent);
	if(cell != null) 
	{
		




		
		var dataValueCell;
		if (this.DataValueField&&this.DataValueField.length>0)
			dataValueCell=cell.Row.getCellFromKey(this.DataValueField);
		if (!dataValueCell)
			dataValueCell=cell.Row.getCell(0);
		if(!dataValueCell) return;
		var nValue=dataValueCell.getValue();
		if(this.getDataValue()!=nValue)
		{
			if(this.DataValueField)
				this._set_dataValue(nValue);
			igtbl_clearSelectionAll(this.grid.Id);
			this.grid.setActiveRow(cell.Row);
			cell.Row.setSelected(true);
			
			//this.selectedIndex = cell.Row.getIndex();
			this._setSelectedIndex(cell.Row.getIndex());
			
			if(!bNoPostUpdate)
			{
				igtbl_updatePostField(this.grid.Id);
				this.updatePostField(newValue,false);
			}
		}
	}
	else
	{
		if(this.getDataValue()!=null)
		{
			if(!this.Editable)
				this._set_dataValue(null);
			igtbl_clearSelectionAll(this.grid.Id);
			this.grid.setActiveRow(null);
			
			//this.selectedIndex = -1;
			this._setSelectedIndex(-1);
			if(!bNoPostUpdate)
			{
				igtbl_updatePostField(this.grid.Id);
				this.updatePostField(newValue,false);
			}
		}
	}
	return this.selectedIndex;
}

function igcmbo_setDataValue(newValue, bFireEvent)
{
	var cell=newValue;
	var oldDataValue=this.dataValue;
	if(cell==null || typeof(cell)!="object")
	{
		this._set_dataValue(newValue);
		var re = new RegExp("^"+igtbl_getRegExpSafe(newValue)+"$", "g");
		var column = null;
		if(this.DataTextField.length > 0)
			column = this.grid.Bands[0].getColumnFromKey(this.DataValueField)
		else
			column = this.grid.Bands[0].Columns[0];
		if(column == null)
			return;
		cell = column.find(re);

		// A. T. Sep 26 2008: commenting the stuff below, as it doesn't make any sense to me and also causes 
		// the regression reported by 8064
		//if(cell && this.DataTextField)
		//{
		//    newValue=cell.Row.getCellFromKey(this.DataTextField).getValue(true);
		//	this.updateValue(newValue,bFireEvent);
	    //}
	}
	else
	{
		this._set_dataValue(cell.getValue());
	}
		
	
















	
	if(cell != null)
	{
//		if(this.DataTextField)
//			this.updateValue(cell.Row.getCellFromKey(this.DataTextField).getValue(true),bFireEvent);
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(cell.Row);
		
		cell.Row.setSelected(true);
		
		this._setSelectedIndex(cell.Row.getIndex());
		
		igtbl_updatePostField(this.grid.Id);
		if(oldDataValue!=newValue && !this.DataTextField)
			this.updatePostField(newValue,false);
	}
	else
	{
		if(oldDataValue!=null)
		{
			this._set_dataValue(null);
			igtbl_clearSelectionAll(this.grid.Id);
			if(this.Prompt)	{
				var row=this.grid.Rows.getRow(0);
				row.activate();
				row.setSelected();					
			}
			else {
				this.grid.setActiveRow(null);
				this._setSelectedIndex(-1);
				igtbl_updatePostField(this.grid.Id);
				this.updatePostField(newValue,false);
			}
		}
	}
	return this.selectedIndex;
}

function igcmbo_getValue()
{
	if(!this.Prompt || this.getSelectedIndex()>0 || this.Editable)
		return this.dataValue;
}

function igcmbo_setValue(newValue, bFireEvent)
{
	if(this.dataValue==newValue)
		return;
	var cell=newValue;
	if(cell==null || typeof(cell)!="object" || newValue.getMonth)
	{
		var oRegEx = newValue?igtbl_dateToString(newValue):newValue;
		var re = new RegExp("^"+igtbl_getRegExpSafe(oRegEx)+"$", "g");
		var column = null;
		if(this.DataValueField.length > 0)
			column = this.grid.Bands[0].getColumnFromKey(this.DataValueField)
		else
			column = this.grid.Bands[0].Columns[0];
		if(column == null)
			return;
		cell = column.find(re);
	}
	var dispValue=this.Prompt;
	if(cell != null)
	{
		this._set_dataValue(newValue);
		if(this.DataValueField)
		{
			cellValue=cell.Row.getCellFromKey(this.DataValueField).getValue();
			if(cellValue!=newValue)
				this._set_dataValue(cellValue);
		}
		if(this.DataTextField)
		{
			dispValue=cell.Row.getCellFromKey(this.DataTextField).getValue(true);
			this.updateValue(dispValue, (typeof(bFireEvent)=="undefined" || bFireEvent));
		}
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(cell.Row);
		cell.Row.setSelected(true);
		this._setSelectedIndex(cell.Row.getIndex());
		//this.selectedIndex = cell.Row.getIndex();
	}
	else
	{
		if(this.Editable)
		{
			this._set_dataValue(newValue);
			dispValue = newValue;
		}
		else
			this._set_dataValue(null);
		this._set_displayValue(dispValue);
		var ib=igcmbo_getElementById(this.ClientUniqueId+"_input");
		if(ib)
			ib.value=dispValue;
		igtbl_clearSelectionAll(this.grid.Id);
		this.grid.setActiveRow(null);
		//this.selectedIndex = -1;
		this._setSelectedIndex(-1);
	}
	igtbl_updatePostField(this.grid.Id);
	this.updatePostField(dispValue,false);
	if(this.Prompt && this.selectedIndex==-1)
	{
		this._setSelectedIndex(0);
		this.setSelectedIndex(0);
		return -1;
	}
	return this.selectedIndex;
}

var igtbl_pbInited=false;
function igcmbo_updateValue(newValue, bFireEvent)
{
	



	if(this.getDisplayValue()==newValue && this._oldSelectedIndex==this.selectedIndex || 
		(ig_csom.IsFireFox &&  this.selectedIndex != -1 && this.getGrid().getActiveRow() == null))
		return;
	if(bFireEvent == true) {
    	if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.BeforeSelectChange,"(\""+this.ClientUniqueId+"\");")) {
	    	return false;
	    }
	}
	var inputbox = igcmbo_getElementById(this.ClientUniqueId + "_input");
	inputbox.value = newValue;
	this.updatePostField(newValue);
	this._set_displayValue(newValue);
	
 	if(this.Editable)
		this._set_dataValue(newValue);
	if(bFireEvent == true) {
		if(igcmbo_fireEvent(this.ClientUniqueId,this.Events.AfterSelectChange,"(\""+this.ClientUniqueId+"\");"))
			return;
	}
	







	if (this.NeedPostBack && bFireEvent == true && !igtbl_pbInited && !this._inArrowKeyNavigation)
		igcmbo_doPostBack(this.Id);
	else if (!bFireEvent)
		this.NeedPostBack = false;
}

function igcmbo_doPostBack(comboName)
{
	var oCombo = igcmbo_getComboById(comboName);
	igtbl_pbInited = true;
	var dataVal = oCombo.getDataValue();
	var dispVal = oCombo.getDisplayValue();
	if (typeof (dataVal) == "undefined" || dataVal == null)
		dataVal = "";
	else
		dataVal = igtbl_dateToString(dataVal);
	if (dataVal)
		dataVal = dataVal.replace(/\'/gi, "\\'");
	if (dispVal)
		dispVal = dispVal.replace(/\'/gi, "\\'");

	

	oCombo.setDropDown(false);

	window.setTimeout("__doPostBack('" + oCombo.UniqueId + "','AfterSelChange\x02" + oCombo.selectedIndex
			+ "\x02" + dataVal + "\x02" + dispVal
			+ "')");
	
	window.setTimeout("igtbl_pbInited=false;", 1000);
}

var ig_inCombo=false;
// private - Handles the mouse down event
function igcmbo_mouseDown(evnt) {
	if(igcmbo_currentDropped != null)
	{			
		var grid = igcmbo_getElementById(igcmbo_currentDropped.ClientUniqueId + "_container");
		var elem = igtbl_srcElement(evnt);
		var parent = elem;
		while(parent != null) {
			








			if(parent.id == grid.id)
			{
				if(igcmbo_currentDropped.webGrid)
				{
					igtbl_lastActiveGrid=igcmbo_currentDropped.webGrid.Id;
					igcmbo_currentDropped.Element.setAttribute("noOnBlur",true);
					window.setTimeout("igcmbo_cancelNoOnBlur('"+igcmbo_currentDropped.Id+"')",500);
				}
				return;
			}
			parent = parent.parentNode;
		}				
		if(ig_inCombo == true) {
			ig_inCombo = false;
			return;		
		}

		if(igcmbo_currentDropped)
			igcmbo_currentDropped.setDropDown(false);

		ig_inCombo = false;			
	}
	var combo=igcmbo_currentDropped;
	if(!combo)
		combo=igcmbo_displaying;
	
	if(combo && combo.eventHandlers && combo.eventHandlers["blur"] && combo.eventHandlers["blur"].length>0 && !igtbl_isAChildOfB(igtbl_srcElement(evnt),combo.Element))
	{
			





		if(combo.Element.getAttribute("noOnBlur"))
		{
			combo.Element.removeAttribute("noOnBlur")
		}
		var ig_event=new ig_EventObject();
		ig_event.event=evnt;
		for(var i=0;i<combo.eventHandlers["blur"].length;i++)
			if(combo.eventHandlers["blur"][i].fListener)
			{
				combo.eventHandlers["blur"][i].fListener(combo,ig_event,combo.eventHandlers["blur"][i].oThis);
				if(ig_event.cancel)
					return igtbl_cancelEvent(evnt);
			}
	}

	try 
	{
		
    	if (ig_inCombo) 
    	{
    		ig_inCombo = false;
    		evnt.srcElement.focus();
    	}
	}
	catch (Error) 
	{
		
	}
}

// private - Handles the mouse up event
function igcmbo_mouseUp(evnt) {
	return;
}

// private - Obtains the proper source element in relation to an event
function igcmbo_srcElement(evnt)
{
	var se
	if(evnt.target)
		se=evnt.target;
	else if(evnt.srcElement)
		se=evnt.srcElement;
	return se;
}

// private - Updates the PostBackData field
function igcmbo_updatePostField(value)
{
	
	var formControl = igcmbo_getElementById(this.ClientID);

	if(!formControl)
		return;
	var index = this.selectedIndex;
	formControl.value = "Select\x02" + index + "\x02Value\x02" + value;

	var dataVal=this.getDataValue();
	if(typeof(dataVal)!="undefined" && dataVal!==null)
		formControl.value+="\x02DataValue\x02"+igtbl_dateToString(dataVal);
	if(this.grid.Bands[0]._sqlWhere)
		formControl.value+="\x02SelectWhere\x02"+this.grid.Bands[0]._sqlWhere;
}

// private
function igcmbo_getGrid()
{
	if(!this.grid || !this.grid.Element)
		this.grid=igtbl_getGridById(this.ClientUniqueId + this._addOnGrid);
	return this.grid;
}

function igcmbo_addEventListener(eventName,fListener,oThis)
{
	eventName=eventName.toLowerCase();
	if(!this.eventHandlers[eventName])
		this.eventHandlers[eventName]=new Array();
	var index=this.eventHandlers[eventName].length;
	if(index>=15)
		return false;
	for(var i=0;i<this.eventHandlers[eventName].length;i++)
		if(this.eventHandlers[eventName][i]["fListener"]==fListener)
			return false;
	this.eventHandlers[eventName][index]=new Object();
	this.eventHandlers[eventName][index]["fListener"]=fListener;
	this.eventHandlers[eventName][index]["oThis"]=oThis;
	return true;
}

function igcmbo_removeEventListener(eventName,fListener)
{
	if(!this.eventHandlers)
		return false;
	var eventName=eventName.toLowerCase();
	if(!this.eventHandlers[eventName] || this.eventHandlers[eventName].length==0)
		return false;
	for(var i=0;i<this.eventHandlers[eventName].length;i++)
		if(this.eventHandlers[eventName][i]["fListener"]==fListener)
		{
			delete this.eventHandlers[eventName][i]["fListener"];
			delete this.eventHandlers[eventName][i]["oThis"];
			delete this.eventHandlers[eventName][i];
			if(this.eventHandlers[eventName].pop)
				this.eventHandlers[eventName].pop();
			else
				this.eventHandlers[eventName]=this.eventHandlers[eventName].slice(0,-1);
			return true;
		}
	return false;
}

function igcmbo_getComboByGridName(gridName)
{
	var oC = null;
	if (!igcmbo_comboState) return oC;
	for (var c in igcmbo_comboState)
		if (igcmbo_comboState[c].grid != null && igcmbo_comboState[c].grid.Id == gridName)
		oC = igcmbo_comboState[c];
	return oC;
}

function igcmbo_getComboByElement(element)
{
	




	while(element)
	{
		if(element.id && element.id.length > 5)
		{
			var gridName = element.id.substring(0,element.id.length-5);
			var combo = igcmbo_getComboByGridName(gridName);
			if(combo) return combo;
		}
		element = element.parentNode;
	}
	return null;
}

function igcmbo_expandEffects(duration, opacity, type, shadowColor, shadowWidth, delay,owner){
	
	this.owner=owner;	
	this.Element=owner.container;
	
	this.Duration=duration;
	this.Opacity=opacity;
	this.Type=type;
	this.ShadowColor=shadowColor;
	this.ShadowWidth=shadowWidth;
	this.Delay=delay;
	this.getDuration=function(){return this.Duration;}
	this.getOpacity=function(){return this.Opacity;}
	this.getType=function(){return this.Type;}
	this.getShadowColor=function(){return this.ShadowColor;}
	this.getShadowWidth=function(){return this.ShadowWidth;}
	this.getDelay=function(){return this.Delay;}
	this.applyFilter=function(p)
	{
		var e=this.Element;
		if(!e || !ig_csom.IsIEWin || ig_csom.AgentName.indexOf("win98")>0 || ig_csom.AgentName.indexOf("windows 98")>0)
			return;
		var s=e.style,ms="progid:DXImageTransform.Microsoft.";
		if(!p && this.Type!="NotSet")
			s.filter=ms+this.Type+"(duration="+(this.Duration/1000)+")";
		if(!p && this.ShadowWidth>0)
			s.filter+=" "+ms+"Shadow(Direction=135,Strength="+this.ShadowWidth+",color='"+this.ShadowColor+"')";
		if(!p)
			s.filter+=" "+"Alpha(Opacity="+this.Opacity+")";
		try
		{
			if(e.filters[0])
				if(p)
					e.filters[0].play();
				else 
					e.filters[0].apply();
		}
		catch(ex){;}
	}	
	
}

var igcmbo_oldOnUnload;
var igcmbo_bInsideOldOnUnload=false;
function _igcmbo_unload()
{
		if(igcmbo_oldOnUnload && !igcmbo_bInsideOldOnUnload)
		{
			igcmbo_bInsideOldOnUnload=true;
			igcmbo_oldOnUnload();
			igcmbo_bInsideOldOnUnload=false;
		}
		for(var comboId in igcmbo_comboState)
		{
			try
			{
				if(typeof(document)!=='unknown')
				{
					var p=igtbl_getElementById(comboId);
					p.value=ig_ClientState.getText(igcmbo_comboState[comboId].ViewState);
				}
			}
			catch(e)	
			{		
			}
			if(igcmbo_comboState[comboId]._unloadCombo)
				igcmbo_comboState[comboId]._unloadCombo();
			else
				delete igcmbo_comboState[comboId];
		}
		igcmbo_currentDropped = igcmbo_displaying = null;
}
if(window.addEventListener)
	window.addEventListener('unload',_igcmbo_unload,false);
else if(window.onunload!=_igcmbo_unload)
{
	igcmbo_oldOnUnload=window.onunload;
	window.onunload=_igcmbo_unload;
}

function igcmbo_XmlHTTPResponse(gn,rowId,respObj)
{
	var grid=igtbl_getGridById(gn);
	if(respObj.ReqType!=grid.eReqType.Custom)
		return;
	igtbl_refillXmlGrid(gn);
	grid._progressIndicator.hide();
	var comboId=igtbl_getElementById(grid.ClientID).getAttribute("igComboId");
	igcmbo_typeAheadReset(comboId);
	igcmbo_getComboById(comboId).Loaded = true;
}

function igcmbo_globalMouseWheel(evnt)
{
	for(var cn in igcmbo_comboState)
	{
		var combo=igcmbo_getComboById(cn);
		if(combo && combo.Dropped)
		{
		    





		    var src = igcmbo_srcElement(evnt);
		    if( !igtbl_isAChildOfB(src,combo.grid.Element.parentNode))
			    combo.setDropDown(false);
		}	
	}
}

igcmbo_initialize();
if(typeof(ig_csom) != "undefined" && ig_csom != null)
	ig_csom.addEventListener(document.documentElement,"mousewheel",igcmbo_globalMouseWheel);
