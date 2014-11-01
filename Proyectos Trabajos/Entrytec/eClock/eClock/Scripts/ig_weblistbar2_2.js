/*
* ig_weblistbar2_2.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/








var iglbar_AllowMove=0;
var iglbar_SourceGroup;
var iglbar_CloneGroup;
var iglbar_CurrentX=0;
var iglbar_CurrentY=0;
var iglbar_moveLsnr = false;
if(typeof iglbar_all!='object')
	var iglbar_all=new Object();
//Return an UltraWebListbar object
function iglbar_getListbarById(id)
{
	var lbar = iglbar_all[id]; 
	if(lbar == null)
	{
		var parts = id.split("_"); 
		var lbarID = parts[0]; 
		if(id.indexOf("_Group_") != -1)
		{
			for(var i = 1; i < parts.length -3; i++)
				lbarID += "_" + parts[i]; 
			lbar = iglbar_all[lbarID]; 
		}
	}
	return lbar;
}
function iglbar_init(id)
{
    var o = iglbar_all[id];
	if(o)
	{
		for(var i = 0; i < o.Groups.length; i++)
		{
			if(o.Groups[i].ExpandEffect != null)
				clearInterval(o.Groups[i].ExpandEffect.ShrinkProcess);
		}
	}
	o=new UltraWebListbar(id,eval(id+"_groupsArray"));
	o.Element.style.display = "";
	

	iglbar_all[id]=o;
		
	return o;
}
//Return an iglbar_Group object associated with the specified id. 
function iglbar_getGroupById(id){
	var parts=id.split("_");
	var barId = parts[0]; 
	var startIndex = 1; 
	if(barId == "")
	{
		barId = "_" + parts[1]; 
		startIndex = 2; 
	}
	



	for(var i = startIndex; i < parts.length - 2 && parts[i] != "Group"; i++)
		barId += "_" + parts[i]; 
	var bar=iglbar_getListbarById(barId);
	if(bar == null) return null;
	var index = 0; 
	for(var x = 1; x < parts.length -1; x++)
	{
		index = parts[parts.length-x]; 
		if(parseInt(index) > -1)
			break
	}
	return bar.Groups[index];
}
//Return an iglbar_Item object associated with the specified id.
function iglbar_getItemById(id){
	var parts=id.split("_");
	var barId = parts[0]; 
	for(var i = 1; i < parts.length - 3; i++)
		barId += "_" + parts[i]; 
	var bar=iglbar_getListbarById(barId);
	if(bar == null) return null;
	return bar.Groups[parts[parts.length-3]].Items[parts[parts.length-1]];	
}
//Arranges Listbar group buttons so that selected button is the last button shown
//in the Top button area, and shows it's items.
function iglbar_adjust(lbar){
	var i=0;
	var moveToBottom=false;
	var group=new Array(2);
	while(lbar.Groups[i]!=null){
		group[0]=ig_csom.getElementById(lbar.Groups[i].Id+"_top_tr");
		group[1]=ig_csom.getElementById(lbar.Groups[i].Id+"_bottom_tr");
		



		try{
			ig_csom.getElementById(lbar.Id+"_Items_"+i.toString()).style.display="none";
		}
		catch(err)
		{
			return;
		}
		if(!moveToBottom){
			if(lbar.Groups[i]==lbar.SelectedGroup){
				moveToBottom=true;				
				var elem = ig_csom.getElementById(lbar.Id+"_Items_"+i.toString()); 
				elem.style.display="";
			}
			group[0].style.display="";
			group[1].style.display="none";
		}
		else{
			if(ig_csom.getElementById(lbar.Id+"_BotGroup").style.display=="none")ig_csom.getElementById(lbar.Id+"_BotGroup").style.display="";
			group[1].style.display="";
			group[0].style.display="none";
		}
		i++; 
	}
	ig_csom.getElementById(lbar.Id+"_Items").className=lbar.SelectedGroup.GroupStyleClassName;
}
//Internal Use Only
function iglbar_evt(e)
{
	if(e == null) if((e = window.event) == null) return;
	var o, src = e.srcElement, type = e.type;
	if(src == null) if((src = e.target) == null) src = this;
	while((o = src.o) == null) if((src = src.parentNode) == null) return;
	//i: 0-item, 1-explHead, 2-listGroup
	var a = src.accessKey, i = (o.Items == null) ? 0 : 1;
	if(i == 1 && o.getExpanded == null) i = 2;
	//o.ak: 0-focus,1-keydown, 2-keyup	
	
	
	if(!ig_shared.IsIE)
	{
		for(var count = 0; count < src.childNodes.length; count++)
		{
			if(src.childNodes[count].tagName == "A")
			{			
				a = src.childNodes[count].accessKey; 
				break;
			}
		}
	}

	if(type == "keyup" && ig_csom.notEmpty(a) && o.ak != 2)
	{
		var k = e.keyCode;
		if(!e.altKey) k = 0;
		else if(o.ak == 1 && !(a.charCodeAt(0) == k || (k == 186 && a == ";") || (k == 187 && a == "=") || (k == 188 && a == ",") || (k == 189 && a == "-") || (k == 191 && a == "/") || (k == 219 && a == "[") || (k == 221 && a == "]"))) k = 0;
		o.ak = 2;
		if(k == 0) return;
		if(i == 0) type = "mousedown";
		else type = (i == 1 && o.Control.HeaderClickAction != 0) ? "dblclick" : "click";
	}
	switch(type)
	{
		case "focus": o.ak = 0; return;
		case "keydown": o.ak = 1; return;
		case "mouseover":
			if(i == 0) iglbar_itemMouseOver(e, src, o);
			else if(i == 1) iglbar_groupHeaderMouseOver(e, src, o);
			else iglbar_groupButtonMouseOver(e, src, o);
			return;
		case "mouseout":
			if(i == 0) iglbar_itemMouseOut(e, src, o);
			else if(i == 1) iglbar_groupHeaderMouseOut(e, src, o);
			else iglbar_groupButtonMouseOut(e, src, o);
			return;
		case "mousedown":
			if(i == 0) iglbar_itemClicked(e, src, o);
			else iglbar_pickupGroup(e, src, o);
			return;
		case "click":
			if(i == 1) iglbar_headerClick(e, src, o);
			else iglbar_groupButtonClicked(e, src, o);
			return;
		case "dblclick": iglbar_headerDoubleClick(e, src, o); return;
		case "selectstart": if(src.tagName != "INPUT") ig_cancelEvent(e); return;
	}
}
//i: 1-key/focus/out/over, 2-down, 4-click, 8-dbl
function iglbar_addLsnr(e, o, i)
{	
	if(e == null) return;
	e.o = o;
	if((i & 1) != 0)
	{
		ig_csom.addEventListener(e, "mouseout", iglbar_evt);
		ig_csom.addEventListener(e, "mouseover", iglbar_evt);
		ig_csom.addEventListener(e, "keyup", iglbar_evt);
		ig_csom.addEventListener(e, "keydown", iglbar_evt);
		ig_csom.addEventListener(e, "focus", iglbar_evt);
	}
	if((i & 2) != 0) ig_csom.addEventListener(e, "mousedown", iglbar_evt);
	if((i & 4) != 0) ig_csom.addEventListener(e, "click", iglbar_evt);
	if((i & 8) != 0) ig_csom.addEventListener(e, "dblclick", iglbar_evt);
}
function iglbar_groupHeaderMouseOver(e, src, group)
{
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.MouseOver[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(group==null||!group.getExpanded()||!group.getEnabled())return;
	src.className=group.HeaderAppearance.HoverAppearance.ClassName;
	if(group.HeaderAppearance.HoverAppearance.ImageUrl)group.HeaderAppearance.Image.src=group.HeaderAppearance.HoverAppearance.ImageUrl;
	if(group.HeaderAppearance.HoverAppearance.ExpansionIndicatorImageUrl)group.HeaderAppearance.ExpansionIndicator.src=group.HeaderAppearance.HoverAppearance.ExpansionIndicatorImageUrl;
}
//Internal Use Only
function iglbar_groupHeaderMouseOut(e, src, group)
{
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.MouseOut[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(group==null||!group.getEnabled())return;
	
	var currentAppearance=group.getExpanded()?group.HeaderAppearance.ExpandedAppearance:group.HeaderAppearance.CollapsedAppearance;
	src.className=currentAppearance.ClassName;
	if(group.HeaderAppearance.Image!=null){
		group.HeaderAppearance.Image.src=currentAppearance.ImageUrl;
	}
	if(group.HeaderAppearance.ExpansionIndicator!=null){
	    group.HeaderAppearance.ExpansionIndicator.src=currentAppearance.ExpansionIndicatorImageUrl;
	}
}

//Internal Use Only
function iglbar_headerClick(e, src, group)
{
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.HeaderClick[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(group.getEnabled())iglbar_navigate(group.TargetUrl,group.TargetFrame);
	if(group.Control.HeaderClickAction==0&&group.getEnabled())
		iglbar_toggleGroup(e,src);
}
//Internal Use Only
function iglbar_headerDoubleClick(e, src, group)
{
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.HeaderDoubleClick[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(group.Control.HeaderClickAction!=2&&group.getEnabled())
		iglbar_toggleGroup(e,src);
}
//Internal Use Only
function iglbar_groupButtonClicked(e, src, group)
{
	if(group==group.Control.SelectedGroup||!group.getEnabled())return;
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.BeforeGroupSelected[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	group.setSelected(true);	
	iglbar_navigate(group.TargetUrl,group.TargetFrame);
	if(oEvent==null)oEvent=new ig_EventObject();
	oEvent.reset();
	oEvent.event=e;
	iglbar_fireEvent(group.Control,group.Control.Events.AfterGroupSelected[0],group,e,oEvent);
	if(oEvent!=null&&oEvent.needPostBack) group.Control.doPost(group.Id + ":GroupSelected");
}
//Internal Use Only
function iglbar_groupButtonMouseOver(e, src, group)
{
	if(group.getSelected()||!group.getEnabled())return;
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.MouseOver[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	src.className=group.ButtonHoverStyleClassName;
}
//Internal Use Only
function iglbar_groupButtonMouseOut(e, src, group)
{
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.MouseOut[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(group==null||group.getSelected()||!group.getEnabled())return;
	src.className=group.ButtonStyleClassName;
}
//Internal Use Only
function iglbar_itemClicked(e, src, item)
{
	if(!item.getEnabled() || item.getSelected())return;
	var oEvent=iglbar_fireEvent(item.Group.Control,item.Group.Control.Events.BeforeItemSelected[0],item,e);
	if(oEvent!=null&&oEvent.cancel) return;
	item.setSelected(true);
	if(oEvent==null)oEvent=new ig_EventObject();
	oEvent.reset();
	oEvent.event=e;
	iglbar_fireEvent(item.Group.Control,item.Group.Control.Events.AfterItemSelected[0],item,e,oEvent);
	if(oEvent!=null&&oEvent.needPostBack) item.Group.Control.doPost(item.Id + ":ItemSelected");
}
//Internal Use Only
function iglbar_itemMouseOver(e, src, item)
{
	var oEvent=iglbar_fireEvent(item.Group.Control,item.Group.Control.Events.MouseOver[0],item,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(item.getSelected()||!item.getEnabled())return;
	if(item.Group.ItemSelectionStyle==1&&item.getImage()!=null)
		item.getImage().className=item.HoverStyleClassName;
	else src.className=item.HoverStyleClassName;
}
//Internal Use Only
function iglbar_itemMouseOut(e, src, item)
{
	var oEvent=iglbar_fireEvent(item.Group.Control,item.Group.Control.Events.MouseOut[0],item,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(item.getSelected()||!item.getEnabled())return;
	if(item.Group.ItemSelectionStyle==1&&item.getImage()!=null)
		item.getImage().className=item.DefaultStyleClassName;
	else src.className=item.DefaultStyleClassName;
}
function iglbar_navigate(targetUrl,targetFrame){
	ig_csom.navigateUrl(targetUrl,targetFrame);
}




function iglbar_Load(e)
{
	var o; 
	for(o in iglbar_all)
	{
		if (iglbar_all[o])
		{
			iglbar_all[o]._load();
			

			if (iglbar_all[o].Events && iglbar_all[o].Events.InitializeListbar) ig_fireEvent(iglbar_all[o], iglbar_all[o].Events.InitializeListbar[0]);
		}
		return;
	}
}









function UltraWebListbar(id,groups){
	//isInitializing is used so that events do not fire during initialization.
	this.isInitializing=true;
	this.Element=ig_csom.getElementById(id);
	


	ig_shared.addEventListener(window, "load", iglbar_Load );
	ig_csom.addEventListener(this.Element, "selectstart", iglbar_evt);
	var selGr, props=eval(id+"_propsArray");
	this.Id=id;	
	this.uniqueID=eval(id+"UniqueID");
	this.Groups=groups;
	var events=eval(id+"_eventArray");
	if(events==null)return;
	this.Events=new iglbar_Events(events);
	//listbar
	if((this.ViewType = props[3]) == 0)
	{
		var selItem = props[2].split("i");
		selGr = this.Groups[selItem[0]];
		this.SelectedItem = selGr.Items[selItem[1]];
		
		
		if(navigator.userAgent.toLowerCase().indexOf("mozilla")>=0)
		{
			if(navigator.userAgent.toLowerCase().indexOf("firefox")>=0)
			{
				
				if(document.doctype == null || (document.doctype != null &&  document.doctype.publicId.toLowerCase().indexOf("xhtml") == -1))
					ig.getElementById(id+"_Items_0").parentNode.style.height=0;
				else
				{
				    
                    
					//ig.getElementById(id+"_Items_0").parentNode.style.height = this.Element.style.height;
				}
			}
		}
		
		
		
		if((ig_shared.IsIE6 || ig_shared.IsIE7 || ig_shared.IsIE55 || ig_shared.IsIE8) && document.documentElement != null)
		{
			var totalHeight = this.Element.offsetHeight;

			
			var offsetParent = ig.getElementById(id + "_Items").offsetParent.offsetParent;

			if (offsetParent != null)
			{
				var elem = offsetParent.parentNode;
				
				elem.style.height = "0px";
				var actualHeight = this.Element.offsetHeight;
				if (actualHeight < totalHeight)
				{
					
				    var percent = (actualHeight) / (totalHeight) * 100;
				    
					if (elem.offsetHeight == 0 || elem.offsetHeight == 1)
						percent = percent - ((100 - percent) * percent) / 100;
					elem.style.height = percent + "%";
					
					
					while (elem.offsetHeight > actualHeight)
					{
						percent--;

						


						if (ig_csom.IsIE)
						{
							if (percent < 0)
							{
								// FailSafe
								elem.style.height = "100%"
								break;
							}
						}
						elem.style.height = percent + "%";
					}
				}
				else
					elem.style.height = "";
			}
		}
		this._autoNavigateSelectedItem = props[6]; 
	}
	else
		this._autoNavigateSelectedItem = props[8]; 
	this.SelectedGroup = selGr = this.Groups[props[1]];
	if(selGr != null)
	{
		//explorer
		if(this.ViewType != 0) this.SelectedItem = selGr.Items[props[2]];
		selGr.selected = true;
	}
	
	this.findControl=function(id)
	{
		var control;
		var groupIndex=0;
		while(this.Groups[groupIndex]!=null)
		{
			control=this.Groups[groupIndex].findControl(id);
			if(control!=null)return control;
			groupIndex++;
		}
	}
	this.enabled=props[4];
	this.getEnabled=function(){return this.enabled;}
	this.setEnabled=iglbar_setEnabled;
	if(props[3]==1)
	{
		if((this.AllowGroupMoving = props[5]) == 1) if(!iglbar_moveLsnr)
		{
			iglbar_moveLsnr = true;
			ig_csom.addEventListener(window.document, "mousemove", iglbar_MoveGroup);
			ig_csom.addEventListener(window.document, "mouseup", iglbar_dropGroup);
		}
		this.HeaderClickAction=props[6];
		this.GroupExpandEffect=props[7];
	}
	var k, i = groups.length;
	for(var j = 0; j < i; j++)
	{
		var a = groups[j]; a.Control = this; a.i = j;//must be permanent
		if((k = iglbar_k(a.Key, i)) != null) groups[k] = a;
	}
	this.groupCount = i;//can be increased only
	this.update = function(group, item, p, v)
	{
		if(this.isInitializing)return;
		if(this.elemState == null) if((this.elemState = ig_csom.getElementById(this.uniqueID + "_hidden")) == null)
			return;
		if(this.state == null) this.state = new ig_xmlNode();
		var n = this.state.addNode("x", true);
		if(group != null)
		{
			n = n.addNode("Groups", true).addNode("i" + group.i, true);
			if(item != null)
				n = n.addNode("Items", true).addNode("i" + item.i, true);
		}
		n.setPropertyValue(p, "" + v);
		this.elemState.value = this.state.getText();
	}
	this.doPost = function(val)
	{
		if (this.isInitializing) return;
		
		


		
		
			var tempUniqueId = this.uniqueID.replace(/\$/g, "_");
			var hiddenInput = document.getElementById(tempUniqueId);
			if(hiddenInput == null)
			{
				hiddenInput = document.createElement("input");
				hiddenInput.type = "hidden";
				
				hiddenInput.id = tempUniqueId; 
				hiddenInput.name = tempUniqueId; 
				hiddenInput.value = "";
				this.Element.appendChild(hiddenInput);
			}
		try{__doPostBack(this.uniqueID, val);}catch(ex){}
	}
	this.setGrText = function(gr, v)
	{
		var id = gr.Id + "_text";
		if(!gr.HeaderAppearance){this.setText(gr, id + "_bottom", v); id += "_top";}
		this.setText(gr, id, v);
	}
	this.setText = function(item, id, v)
	{
		var e = ig_csom.getElementById(id);
		if(!ig_csom.isArray(v) || e == null) return;
		item.txt = v;
		var gr = item.Group;
		if(gr == null){gr = item; item = null;}
		var t = v, n = e.childNodes;
		var i = (n == null) ? 0 : n.length;
		while(i-- > 0)
		{
			if(t == v && n[i].nodeName == "#text"){n[i].nodeValue = v; t = null;}
			else if(n[i].nodeName != "IMG") e.removeChild(n[i]);
		}
		if(t == v)try{e.innerHTML = " " + v + " ";}catch(ex){}
		this.update(gr, item, "Text", v);
	}
	this.onLoad=function()
	{
		if(this.SelectedItem!=null){
			this.SelectedItem.setSelected(true);
		}	
	}
	


	this._load = function()
	{
		var lbar = this;
		var elemState = ig_csom.getElementById(this.uniqueID + "_hidden")
		if (elemState != null)
		{
			
			this.isInitializing = true;
			var v = unescape(elemState.value);
			if (v != null && v.length > 0)
			{
				var groupStrIndex = v.indexOf("SelectedGroup=\"", 0);
				if (groupStrIndex > -1)
				{
					groupStrIndex += 16;
					var endIndex = v.indexOf("\"", groupStrIndex);
					var groupIndex = v.substring(groupStrIndex - 1, endIndex);
					lbar.Groups[groupIndex].setSelected(true);
				}

				var itemStrIndex = v.indexOf("SelectedIndex=\"", 0);

				if (itemStrIndex > -1)
				{
					itemStrIndex += 16;
					var endIndex = v.indexOf("\"", itemStrIndex);
					var itemIndex = v.substring(itemStrIndex - 1, endIndex).split("i");
					var group = itemIndex[0];
					var item = itemIndex[1];
					lbar.Groups[group].Items[item].setSelected(true);
				}
			}
			this.isInitializing = false;
		}
		
		if (document.documentElement != null && document.documentElement.clientHeight != 0 && this.ViewType == 1)
		{
			for (var i = 0; i < this.Groups.length; i++)
			{
				var lci = this.Groups[i].HeaderAppearance.LeftCornerImage
				if (lci != null)
				{
					var height = lci.offsetHeight;
					if (height > 0)
					{
						if (lci.parentElement.nextSibling != null)
						{
							lci.parentElement.nextSibling.vAlign = "top";
							lci.parentElement.nextSibling.style.height = height + "px";
						}
					}
				}
			}

		}
	}
	if(this.SelectedItem!=null){
		//Use the setSelected method, so that any targetUrls get loaded.
		//We must do this after the load event fires, so that we know we have all of the frames loaded.
		ig.addEventListener(window,"load",function()
											{
												var lbar=iglbar_getListbarById(id); 
												if(lbar==null)return;
												lbar.isInitializing=true;
												lbar.SelectedItem.setSelected(true);
												lbar.isInitializing=false;
											},false);											
	}
	this.isInitializing=false;
	ig.addEventListener(window,"unload",iglbar_onUnload,false);
}
function iglbar_onUnload(){
	ig_dispose(iglbar_all);
}

function iglbar_Events(events){
	this.AfterGroupSelected=events[0];
	this.AfterGroupCollapsed=events[1];
	this.AfterGroupExpanded=events[2];
	this.AfterItemSelected=events[3];
	this.BeforeGroupSelected=events[4];
	this.BeforeGroupCollapsed=events[5];
	this.BeforeGroupExpanded=events[6];
	this.BeforeItemSelected=events[7];
	this.InitializeListbar=events[8];
	this.MouseOver=events[9];
	this.MouseOut=events[10];
	this.BeforeGroupMove=events[11];
	this.AfterGroupMove=events[12];
	this.GroupDrag=events[13];
	this.HeaderClick=events[14];
	this.HeaderDoubleClick=events[15];
}

function iglbar_HeaderStyle(style,xpndImage,image,leftImage,rightImage){
	this.ClassName=style;
	this.ExpansionIndicatorImageUrl=xpndImage;
	this.ImageUrl=image;
	this.LeftCornerImageUrl=leftImage;
	this.RightCornerImageUrl=rightImage;
}
function iglbar_Header(xpnd,clpse,hover,xpandId,imageId,leftId,rightId){
	this.ExpandedAppearance=xpnd;
	this.CollapsedAppearance=clpse;
	this.HoverAppearance=hover;
	this.ExpansionIndicator=ig_csom.getElementById(xpandId);
	this.Image=ig_csom.getElementById(imageId);
	this.LeftCornerImage=ig_csom.getElementById(leftId);
	this.RightCornerImage=ig_csom.getElementById(rightId);
}
function iglbar_ExplorerBarGroup(id,text,key,enabled,groupStyle,headerAppearance,itemIconStyle,itemSelectionStyle,expanded,targetUrl,targetFrame,items){
	this.Id=id;
	this.Element=ig_csom.getElementById(id+"_group");
	this.txt = text;
	this.getText=function(){return this.txt;}
	this.setText=function(v){this.Control.setGrText(this, v);}
	this.Key=key;
	this.enabled=enabled;
	this.getEnabled=function(){return this.enabled && this.Control.getEnabled();}
	this.setEnabled=iglbar_setEnabled;
	this.GroupStyleClassName=groupStyle;
	this.HeaderAppearance=headerAppearance;
	this.ItemIconStyle=itemIconStyle;
	this.ItemSelectionStyle=itemSelectionStyle;
	this.Items=items;
	this.Items.ValueChanged=false;
	this.TargetUrl=targetUrl;
	this.TargetFrame=targetFrame;
	this.findControl=function(id){
		return ig_csom.findControl(this.Element,id);
	}
	this.expanded=expanded;	
	this.getExpanded=function(){return this.expanded;}
	this.setExpanded=iglbar_expandGroup;
	this.getVisibleIndex=iglbar_getVisibleIndex;
	var header=ig_csom.getElementById(id+"_header");
	if(header!=null)
	{
		this.HeaderAppearance.Element=header;
		this.HeaderAppearance.Id=id+"_header";
		//13-key/focus/over/out,click,dblclick
		iglbar_addLsnr(header, this, 13);
	}
	var k, i = (items == null) ? 0 : items.length;
	for(var j = 0; j < i; j++)
	{
		var a = items[j]; a.Group = this; a.i = j;//must be permanent
		if((k = iglbar_k(a.Key, i)) != null) items[k] = a;
	}
	this.itemCount = i;//can be increased only
	//2-mousedown
	iglbar_addLsnr(ig_csom.getElementById(id), this, 2);
	
	




	if(this.expanded)
	{
		var itemsArea = ig_csom.getElementById(id+"_items").firstChild;
		while(itemsArea!=null&&itemsArea.tagName!="TD"){itemsArea=itemsArea.nextSibling;}
		if(itemsArea==null||itemsArea.tagName!="TD")return;
		itemsArea=itemsArea.firstChild;
		while(itemsArea!=null&&itemsArea.tagName!="DIV"){itemsArea=itemsArea.nextSibling;}
		if(ig_csom.IsIE)itemsArea.style.overflowY = "visible";
	}
}
function iglbar_ListbarGroup(id,text,key,enabled,groupStyle,buttonStyle,buttonHovStyle,buttonSelStyle,itemIconStyle,itemSelectionStyle,targetUrl,targetFrame,items){
	this.Id=id;
	this.Element=new Array(2);
	var e = ig_csom.getElementById(id+"_top");
	//5-key/focus/over/out,click
	iglbar_addLsnr(e, this, 5);
	this.Element[0] = e;
	e = ig_csom.getElementById(id+"_bottom");
	iglbar_addLsnr(e, this, 5);
	this.Element[1] = e;
	this.txt = text;
	this.getText=function(){return this.txt;}
	this.setText=function(v){this.Control.setGrText(this, v);}
	this.Key=key;
	this.enabled=enabled;
	this.getEnabled=function(){return this.enabled && this.Control.getEnabled();}
	this.setEnabled=iglbar_setEnabled;
	this.GroupStyleClassName=groupStyle;
	this.ButtonStyleClassName=buttonStyle;
	this.ButtonHoverStyleClassName=buttonHovStyle;
	this.ButtonSelectedStyleClassName=buttonSelStyle;
	this.ItemIconStyle=itemIconStyle;
	this.ItemSelectionStyle=itemSelectionStyle;
	this.Items=items;
	this.Items.ValueChanged=false;
	this.findControl=function(controlId){
		var grpIndex=this.Id.split("_");
		return ig_csom.findControl(ig_csom.getElementById(grpIndex[0]+"_Items_"+grpIndex[2]),controlId);
	}
	this.selected=false;
	this.getSelected=function(){return this.selected;}
	this.TargetUrl=targetUrl;
	this.TargetFrame=targetFrame;
	this.setSelected=iglbar_selectGroup;
	var k, i = (items == null) ? 0 : items.length;
	for(var j = 0; j < i; j++)
	{
		var a = items[j]; a.Group = this; a.i = j;//must be permanent
		if((k = iglbar_k(a.Key, i)) != null) items[k] = a;
	}
	this.itemCount = i;//can be increased only
}

function iglbar_Item(id,text,key,defStyle,hovStyle,selStyle,targetUrl,targetFrame,image,selectedImage,enabled){
	this.Id=id;
	this.Element=ig_csom.getElementById(id);
	this.txt = text;
	this.getText=function(){return this.txt;}
	this.setText=function(text){this.Group.Control.setText(this, this.Id, text);}
	this.Key=key;
	this.DefaultStyleClassName=defStyle;
	this.HoverStyleClassName=hovStyle;
	this.SelectedStyleClassName=selStyle;
	this.TargetUrl=targetUrl;
	this.TargetFrame=targetFrame;
	this.ImageUrl=image;
	this.SelectedImageUrl=selectedImage;
	this.getImage=function(){return ig_csom.getElementById(this.Id+"_img");}
	this.selected=false;
	this.setSelected=iglbar_selectItem;
	this.getSelected=function(){return this.selected;}
	this.enabled=enabled;
	this.getEnabled=function(){return this.enabled && this.Group.getEnabled();}
	this.setEnabled=iglbar_setEnabled;
	this._removeLabels=function(e)
	{
		var ns=e.childNodes, i=(ns==null)?0:ns.length;
		while(--i>=0)
		    if(ns[i].nodeName=="LABEL") 
		        e.removeChild(ns[i]);
		    else if(ns[i].childNodes!=null&&ns[i].childNodes.length>0)
		        this._removeLabels(ns[i]);
	}
	//3-key/focus/over/out,mousedown
	iglbar_addLsnr(this.Element, this, 3);
}
function iglbar_setEnabled(enabled)
{
	this.enabled=enabled;
	if(enabled)
	{
		if(this.Element.length == null)
			this.Element.removeAttribute("disabled");
		else
		{
			if(this.Items.length > 0)
				this.Items[0].Element.parentNode.removeAttribute("disabled");
			this.Element[0].parentNode.removeAttribute("disabled");
			this.Element[1].parentNode.removeAttribute("disabled");
		}
			
	}
	else
	{	
	
		if(this.Element.length == null)
			this.Element.setAttribute("disabled","disabled");
		else
		{
			if(this.Items.length > 0)
				this.Items[0].Element.parentNode.setAttribute("disabled","disabled");
			this.Element[0].parentNode.setAttribute("disabled","disabled");
			this.Element[1].parentNode.setAttribute("disabled","disabled");
		}
	}
}
function iglbar_toggleGroup(e,src){
	e = (e) ? e : ((window.event) ? window.event : "");	
	var group=iglbar_getGroupById(src.id);
	if(group == null) return;
	if(!group.getEnabled())return true;
	var oEvent;
	if(group.getExpanded())
		oEvent=iglbar_fireEvent(group.Control,group.Control.Events.BeforeGroupCollapsed[0],group,e);
	else oEvent=iglbar_fireEvent(group.Control,group.Control.Events.BeforeGroupExpanded[0],group,e);
	if(oEvent!=null&&oEvent.cancel) return;
	if(oEvent==null)oEvent=new ig_EventObject();
	oEvent.reset();
	group.setExpanded(!group.getExpanded(),true);
	if(group.getExpanded())
		iglbar_fireEvent(group.Control,group.Control.Events.AfterGroupExpanded[0],group,e,oEvent);
	else iglbar_fireEvent(group.Control,group.Control.Events.AfterGroupCollapsed[0],group,e,oEvent);
	if(oEvent.needPostBack||(group.getExpanded()&&group.Control.Events.AfterGroupExpanded[1])||((!group.getExpanded())&&group.Control.Events.AfterGroupCollapsed[1])) group.Control.doPost(group.Id + ":" + (group.getExpanded() ? "GroupExpanded" : "GroupCollapsed"));
	if(src.tagName=="IMG"){
		e.cancelBubble=true;
		return false;
	}
}
function iglbar_expandGroup(expand,byMouse){
	if((!this.getEnabled())&&byMouse)return;
	this.expanded=expand;
	if(expand){
		ig_csom.getElementById(this.Id+"_items").style.display="";
		var ei=this.HeaderAppearance.ExpansionIndicator;
		ei.src=this.HeaderAppearance.ExpandedAppearance.ExpansionIndicatorImageUrl;
		if(ei.getAttribute("igAltX")!=null){
		    ei.setAttribute("igAltC",ei.getAttribute("alt"));
		    ei.setAttribute("alt",ei.getAttribute("igAltX"));
		    ei.removeAttribute("igAltX");
		}
		ig_csom.getElementById(this.Id+"_header").className=this.HeaderAppearance.ExpandedAppearance.ClassName;
		if(this.HeaderAppearance.LeftCornerImage!=null)
			this.HeaderAppearance.LeftCornerImage.src=this.HeaderAppearance.ExpandedAppearance.LeftCornerImageUrl;
		if(this.HeaderAppearance.RightCornerImage!=null)
			this.HeaderAppearance.RightCornerImage.src=this.HeaderAppearance.ExpandedAppearance.RightCornerImageUrl;
		if(this.HeaderAppearance.Image!=null)
			this.HeaderAppearance.Image.src=this.HeaderAppearance.ExpandedAppearance.ImageUrl;
		var agt=navigator.userAgent.toLowerCase();
		var isWin98=(agt.indexOf("win98")!=-1) || (agt.indexOf("windows 98")!=-1);
		if(this.Control.GroupExpandEffect==0 && !isWin98 ){
			if(this.ExpandEffect==null)this.ExpandEffect=new iglbar_expandEffect(this);
			this.ExpandEffect.Expand(true);
		}
	}
	else{
		if(this.Control.GroupExpandEffect==0 && !isWin98){
			if(this.ExpandEffect==null)this.ExpandEffect=new iglbar_expandEffect(this);
			this.ExpandEffect.Expand(false);
		}else ig_csom.getElementById(this.Id+"_items").style.display="none";
		var ei=this.HeaderAppearance.ExpansionIndicator;
		ei.src=this.HeaderAppearance.CollapsedAppearance.ExpansionIndicatorImageUrl;
		if(ei.getAttribute("igAltC")!=null){
		    ei.setAttribute("igAltX",ei.getAttribute("alt"));
		    ei.setAttribute("alt",ei.getAttribute("igAltC"));
		    ei.removeAttribute("igAltC");
		}
		ig_csom.getElementById(this.Id+"_header").className=this.HeaderAppearance.CollapsedAppearance.ClassName;
		if(this.HeaderAppearance.LeftCornerImage!=null)
			this.HeaderAppearance.LeftCornerImage.src=this.HeaderAppearance.CollapsedAppearance.LeftCornerImageUrl;
		if(this.HeaderAppearance.RightCornerImage!=null)
			this.HeaderAppearance.RightCornerImage.src=this.HeaderAppearance.CollapsedAppearance.RightCornerImageUrl;
		if(this.HeaderAppearance.Image!=null)
			this.HeaderAppearance.Image.src=this.HeaderAppearance.CollapsedAppearance.ImageUrl;
	}
	this.Control.update(this, null, "Expanded", expand);
	if(byMouse)iglbar_groupHeaderMouseOver(window.event,this.HeaderAppearance.Element,this);
}
function iglbar_Slide(expand){
	clearInterval(this.ShrinkProcess);	
	this.Group.originalItemAreaStyleHeight = (this.Group.originalItemAreaStyleHeight==null ? this.ItemsArea.style.height : this.Group.originalItemAreaStyleHeight) ;
	this.Group.originalItemAreaParentStyleHeight = (this.Group.originalItemAreaParentStyleHeight==null ? this.ItemsArea.offsetParent.style.height : this.Group.originalItemAreaParentStyleHeight);		
	if(this.Group.itemsHeight==null || (!expand && this.Group.itemsHeight!=this.ItemsArea.offsetHeight))this.Group.itemsHeight=this.ItemsArea.offsetHeight;
	this.AlphaConstant=100/parseInt(this.Group.itemsHeight);
	
	myid=this.Group.Id;

	if(expand){
		this.ItemsArea.offsetParent.style.filter="alpha(opacity=1)";
		this.ItemsArea.style.height=1+ "px";
		this.ItemsArea.offsetParent.style.height=1+ "px";
 		this.Opacity=1;
		this.ShrinkProcess=setInterval("iglbar_SlideDown('"+myid+"')",10);
	}
	else
	{
		


		this._originalPos = this.ItemsArea.offsetParent.style.position; 
		this.ItemsArea.offsetParent.style.position = "relative";
		
		if(ig_csom.IsIE)	this.ItemsArea.style.overflowY="hidden";
		else this.ItemsArea.style.overflow="hidden";
			
		this.ItemsArea.offsetParent.style.filter="alpha(opacity=100)";
		this.ItemsArea.style.height=this.Group.itemsHeight + "px";
		this.ItemsArea.offsetParent.style.height=this.Group.itemsHeight + "px";
 		this.Opacity=100;
		this.ShrinkProcess=setInterval("iglbar_SlideUp('"+myid+"')",10);
	}
}
function iglbar_SlideDown(groupId){
	
	var group =iglbar_getGroupById(groupId)
	if(group == null)
		return;
	var expandEffect = group.ExpandEffect;
	if(expandEffect == null)
		return;
		
	var curHeight=parseInt(expandEffect.ItemsArea.style.height);

	if((parseInt(expandEffect.Group.itemsHeight)-curHeight)<11){
		
		
		if(expandEffect._originalPos != null)
		{
			expandEffect.ItemsArea.offsetParent.style.position = expandEffect._originalPos;
			expandEffect._originalPos = null;
		}
		
		


		if(expandEffect.ItemsArea.offsetParent.filters)
              expandEffect.ItemsArea.offsetParent.style.removeAttribute("filter");
		
		clearInterval(expandEffect.ShrinkProcess);
		expandEffect.ItemsArea.style.height=expandEffect.Group.itemsHeight;//(6*(originalHeight/curHeight));
		expandEffect.ItemsArea.offsetParent.style.height=expandEffect.ItemsArea.style.height;
		if(expandEffect.ItemsArea.offsetParent.filters && expandEffect.ItemsArea.offsetParent.filters.length > 0)expandEffect.ItemsArea.offsetParent.filters[0].opacity=100;
		shrinkProcess=null;
		
		expandEffect.ItemsArea.style.height = expandEffect.Group.originalItemAreaStyleHeight;
		expandEffect.ItemsArea.offsetParent.style.height = expandEffect.Group.originalItemAreaParentStyleHeight;
		
		expandEffect.Group.originalItemAreaStyleHeight = null; 
		expandEffect.Group.originalItemAreaParentStyleHeight  = null;
		
		




		if(ig_csom.IsIE)	expandEffect.ItemsArea.style.overflowY="visible";
		else  expandEffect.ItemsArea.style.overflow="";
					
		return;
	}
	expandEffect.ItemsArea.style.height=(parseInt(expandEffect.ItemsArea.style.height)+10)+ "px";//(6*(originalHeight/curHeight));
	expandEffect.ItemsArea.offsetParent.style.height=expandEffect.ItemsArea.style.height ;
	expandEffect.Opacity=expandEffect.Opacity+(10*expandEffect.AlphaConstant);
	if(expandEffect.ItemsArea.filters)expandEffect.ItemsArea.offsetParent.filters[0].opacity=expandEffect.Opacity;//itemsArea.offsetParent.filters[0].opacity-alphaConst;
}
function iglbar_SlideUp(groupId){
	var group =iglbar_getGroupById(groupId)
	if(group == null)
		return;
	var expandEffect = group.ExpandEffect;
	if(expandEffect == null)
		return;
	var curHeight=parseInt(expandEffect.ItemsArea.style.height);
	if(curHeight<11){
		clearInterval(expandEffect.ShrinkProcess);
		expandEffect.ItemsArea.style.height=1 + "px";//(6*(originalHeight/curHeight));
		expandEffect.ItemsArea.offsetParent.style.height=1 + "px";
		if(expandEffect.ItemsArea.filters)expandEffect.ItemsArea.offsetParent.filters[0].opacity=0;
		shrinkProcess=null;
		ig_csom.getElementById(expandEffect.Group.Id+"_items").style.display="none";
		return;
	}
	

	expandEffect.ItemsArea.offsetParent.style.verticalAlign =  "top";
	expandEffect.ItemsArea.style.height=(parseInt(expandEffect.ItemsArea.style.height)-10) + "px";//(6*(originalHeight/curHeight));
	expandEffect.ItemsArea.offsetParent.style.height=expandEffect.ItemsArea.style.height;
	
	expandEffect.Opacity=expandEffect.Opacity-(10*expandEffect.AlphaConstant);
	if(expandEffect.ItemsArea.filters)expandEffect.ItemsArea.offsetParent.filters[0].opacity=expandEffect.Opacity;//itemsArea.offsetParent.filters[0].opacity-alphaConst;
}

function iglbar_expandEffect(group){
	this.ShrinkProcess=0;
	this.ItemsArea=ig_csom.getElementById(group.Id+"_items").firstChild;
	while(this.ItemsArea!=null&&this.ItemsArea.tagName!="TD"){this.ItemsArea=this.ItemsArea.nextSibling;}
	if(this.ItemsArea==null||this.ItemsArea.tagName!="TD")return;
	this.ItemsArea=this.ItemsArea.firstChild;
	while(this.ItemsArea!=null&&this.ItemsArea.tagName!="DIV"){this.ItemsArea=this.ItemsArea.nextSibling;}
	
	this.Group=group;
	this.Opacity=100;
	this.Expand=iglbar_Slide;
}
function iglbar_selectGroup(select){




  
	if(select){
		this.Control.update(null, null, "SelectedGroup", this.i);
		if(this.Control!=null&&this.Control.Events.AfterGroupSelected[1]) this.Control.doPost(this.Id+":GroupSelected");
		if(this.Control.SelectedGroup!=null)this.Control.SelectedGroup.setSelected(false);
		this.selected=true;
		this.Control.SelectedGroup=this;
		this.Element[0].className=this.ButtonSelectedStyleClassName;
		this.Element[1].className=this.ButtonStyleClassName; //The button may not receive the mouseout event.  In this case, we need to change the style back to the Default style manually.
		iglbar_adjust(this.Control);
	}else{
		this.selected=false;
		if(this.Control.SelectedGroup==this)this.Control.SelectedGroup=null;
		this.Element[0].className=this.ButtonStyleClassName;
	}
}
function iglbar_selectItem(select){
    var lbar=this.Group.Control;
	if(select){
		lbar.update(null, null, "SelectedIndex", "" + this.Group.i + "i" + this.i);
		var supressNavigate=!lbar._autoNavigateSelectedItem && lbar.isInitializing;
		if(lbar!=null&&lbar.Events.AfterItemSelected[1] && !supressNavigate){
			lbar.doPost(this.Id + ":ItemSelected");
		}
		if(lbar.SelectedItem!=null)lbar.SelectedItem.setSelected(false);
		lbar.SelectedItem=this;
		this.selected=true;
		if(!supressNavigate)
		{
			

			if(ig_shared.IsIE7)
				setTimeout("iglbar_navigate(\"" + this.TargetUrl +"\",'" + this.TargetFrame+"')", 100);
			else
				iglbar_navigate(this.TargetUrl,this.TargetFrame);
		}
		if(this.Group.ItemSelectionStyle==1&&this.getImage()!=null)
			this.getImage().className=this.SelectedStyleClassName;
		else this.Element.className=this.SelectedStyleClassName;
		if(this.SelectedImageUrl!=null&&this.SelectedImageUrl.length>0)
			this.getImage().src=this.SelectedImageUrl;
	}else{
	    if(!lbar.isInitializing && lbar.SelectedItem) 
	        lbar.SelectedItem._removeLabels(this.Element);
		lbar.SelectedItem=null;
		this.selected=false;
		if(this.Group.ItemSelectionStyle==1&&this.getImage()!=null)
			this.getImage().className=this.DefaultStyleClassName;
		else this.Element.className=this.DefaultStyleClassName;
		if(this.ImageUrl!=null&&this.ImageUrl.length>0)
			this.getImage().src=this.ImageUrl;
	}
}
function iglbar_pickupGroup(evt, src, group)
{
	if(group.Control.AllowGroupMoving != 1 || !group.getEnabled())return;
	iglbar_SourceGroup=group.Element;
	var oEvent=iglbar_fireEvent(group.Control,group.Control.Events.BeforeGroupMove[0],group,evt);
	if(oEvent!=null&&oEvent.cancel) return;
	iglbar_CurrentX =(evt.pageX?evt.pageX:(evt.clientX + document.body.scrollLeft));
	iglbar_CurrentY =(evt.pageY?evt.pageY:(evt.clientY + document.body.scrollTop));
	if(ig_csom.IsIE){
		iglbar_SourceGroup.onmouseup=iglbar_dropGroup;
		iglbar_SourceGroup.setCapture();
	}
	ig_cancelEvent(evt);
	iglbar_AllowMove=1;
}
function iglbar_MoveGroup(evt)
{
	if(evt == null) if((evt = window.event) == null) return;
	if(iglbar_AllowMove < 1) return;
	if (evt.button == 0 && ig_csom.IsIE) { iglbar_dropGroup(evt); return; }
	NewX = evt.pageX?(evt.pageX):(document.body.scrollLeft+evt.clientX);
	NewY = evt.pageY?(evt.pageY):(document.body.scrollTop+evt.clientY);
	DistanceX = (NewX - iglbar_CurrentX);
	DistanceY = (NewY - iglbar_CurrentY);
	if(DistanceX>3||DistanceY>3||DistanceY<-3||DistanceX<-3)
		iglbar_startDrag(evt);
	if(iglbar_AllowMove==1)return;
	var oGroup=iglbar_getGroupById(iglbar_SourceGroup.id);
	if(oGroup.Control.Events.GroupDrag[0]!=null&&oGroup.Control.Events.GroupDrag[0].length>0){
		var oEvent=new ig_EventObject();
		oEvent.event=evt;
		ig_fireEvent(oGroup.Control,oGroup.Control.Events.GroupDrag[0],oGroup,iglbar_CloneGroup,evt);
		if(oEvent!=null&&oEvent.cancel){
			iglbar_dropGroup(evt);
			return;
		}
	}
	ig_cancelEvent(evt);
	iglbar_CurrentX = NewX;
	iglbar_CurrentY = NewY;
	// K.D. March 10, 2011 Bug #67390 "GroupSwapping" is not working: Implementing it with the new ToPixel function
	iglbar_CloneGroup.style.left = ig_csom.ToPixel(parseInt(iglbar_CloneGroup.style.left) + DistanceX); //+"px";
	iglbar_CloneGroup.style.top = ig_csom.ToPixel(parseInt(iglbar_CloneGroup.style.top) + DistanceY);//+"px";
}
function iglbar_startDrag(evt){
	if(iglbar_AllowMove==2)return;
	iglbar_AllowMove=2;
	var groupPosition=iglbar_getPosition(iglbar_SourceGroup);
	var width=iglbar_SourceGroup.offsetWidth;
	iglbar_CloneGroup = iglbar_SourceGroup.cloneNode(true);
	// K.D. March 10, 2011 Bug #67390 "GroupSwapping" is not working: Implementing it with the new ToPixel function
	iglbar_CloneGroup.style.position = "absolute";
	iglbar_CloneGroup.style.left = ig_csom.ToPixel(groupPosition.x);
	iglbar_CloneGroup.style.top = ig_csom.ToPixel(groupPosition.y);
	iglbar_CloneGroup.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=50)";
	// K.D. March 10, 2011 Bug #67390 "GroupSwapping" is not working: fixing opacity for Firefox 4 as well
    if (ig_csom.IsFireFox)
	    iglbar_CloneGroup.style.opacity = '0.5';
	iglbar_CloneGroup.style.width = ig_csom.ToPixel(width);
	iglbar_CloneGroup.style.zIndex = 1000;
	document.body.appendChild(iglbar_CloneGroup);
	document.body.style.cursor="move";
}

function iglbar_getPosition(el){
	for (var lx=0,ly=0;el!=null;lx+=(el.offsetLeft-el.scrollLeft),ly+=(el.offsetTop-el.scrollTop),el=el.offsetParent);
	return {x:lx+(window.pageXOffset?window.pageXOffset:(document.body.scrollLeft?document.body.scrollLeft:0)),y:(ly+(window.pageYOffset?window.pageYOffset:(document.body.scrollTop?document.body.scrollTop:0)))}
}
function iglbar_dropGroup(evt)
{
	if(iglbar_AllowMove < 1) return;
	if(evt == null) evt = window.event;
	var group=iglbar_getGroupById(iglbar_SourceGroup.id);
	if(ig_csom.IsIE){
		iglbar_SourceGroup.onmouseup="";
		iglbar_SourceGroup.releaseCapture();
	}
	if(iglbar_AllowMove==1) group = null;
	iglbar_AllowMove=0;
	if(group == null) return;
	var listbar=iglbar_getListbarById(iglbar_SourceGroup.id);
	var i=0;
	var insertGroup=iglbar_SourceGroup;
	var oldY=0;
	while(listbar.Groups[i]!=null){
		var newY=parseInt(iglbar_getPosition(listbar.Groups[i].Element).y);
		if(newY<parseInt(iglbar_CloneGroup.style.top)&&newY>oldY&&insertGroup.tagName=="TABLE"){
			insertGroup=listbar.Groups[i].Element;
			oldY=newY;
		}
		i++;
	}
	document.body.removeChild(iglbar_CloneGroup);
	if(iglbar_SourceGroup!=insertGroup||(iglbar_SourceGroup==insertGroup&&oldY==0))
	{
		if(oldY==0)
		{
			insertGroup.offsetParent.insertBefore(iglbar_SourceGroup,insertGroup.offsetParent.firstChild);
		}else if(insertGroup.nextSibling)
		{
			insertAtElement=insertGroup.nextSibling;
			while(insertAtElement!=null&&insertAtElement.tagName!="TABLE")insertAtElement=insertAtElement.nextSibling;
			if(insertAtElement!=null&&insertAtElement.tagName=="TABLE")insertGroup.offsetParent.insertBefore(iglbar_SourceGroup,insertAtElement);
			else insertGroup.offsetParent.appendChild(iglbar_SourceGroup);
		}else insertGroup.offsetParent.appendChild(iglbar_SourceGroup);
		var order = "";
		i = -1;
		//vs: needs more work
		while(++i < listbar.groupCount) order += listbar.Groups[i].getVisibleIndex() + "i";
		listbar.update(null, null, "GroupOrder", order);
	}
	var image=group.HeaderAppearance.Image;
	if(image!=null)
	{
		image.style.visibility="hidden";
		image.style.visibility="visible";
	}
	if(!(ig_csom.IsIE)){
		iglbar_fixNetscapeImages(listbar.Groups);
	}
	document.body.style.cursor="default";
	var oEvent=(iglbar_fireEvent(group.Control,group.Control.Events.AfterGroupMove[0],group,evt));
}
function iglbar_fixNetscapeImages(Groups){
	var i=0;
	if(Groups==null)return;
	while(Groups[i]!=null){
		if(Groups[i].HeaderAppearance.Image!=null){
			Groups[i].HeaderAppearance.Image.style.visibility="hidden";		
			Groups[i].HeaderAppearance.Image.style.visibility="visible";
		}
		i++;
	}
}
function iglbar_getVisibleIndex(){
	var currentGroup=this.Element.offsetParent.firstChild;
	var i=0;
	while(currentGroup!=null&&currentGroup!=this.Element){if(currentGroup.tagName=="TABLE")i++;currentGroup=currentGroup.nextSibling}
	return i;
}
function iglbar_fireEvent(listbar,name,target,browserEvent){
	var oEvent;
	if(name==null||name.length<=0)return null;
	if(iglbar_fireEvent.arguments.length>4)
		oEvent=iglbar_fireEvent.arguments[4];
	else oEvent=new ig_EventObject();
	oEvent.event=browserEvent;
	ig_fireEvent(listbar,name,target,oEvent);
	return oEvent;
}
function iglbar_k(k, ii)
{
	var x = 0, i = -1, l = (k == null) ? 0 : k.length;
	while(++i < l)
	{
		var d = k.charCodeAt(i) - 48;
		if(d < 0 || d > 9) return k;
		x = x * 10 + d;
	}
	return (x < ii) ? null : k;
}
