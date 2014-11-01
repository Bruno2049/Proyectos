/*
    Infragistics ComboBox Script
    Version 7.1.20071.40 
    Copyright (c) 2005 - 2006 Infragistics, Inc. All Rights Reserved.
*/	
var ig_currentComboElem = null;
var tempId = null;
var tempSelectedElement = null;
function ig_CreateComboBox(id)
{
	ig_ComboBox.prototype.constructor = ig_ComboBox;
	
	ig_ComboBox.prototype.init=function(elemId)
	{
		this._id=elemId;
		ig_all[elemId]=this;
		this._elem = document.getElementById(elemId);
		this._input = document.getElementById(elemId + "_inputbox");
		this._button = document.getElementById(elemId + "_button");
		this._button._combo = this;
		ig_addEventListener(this._button, 'click', this._openComboClick, true);
		ig_addEventListener(document, 'mousedown', this.closeCombo, true);
		ig_addEventListener(document, 'mouseup', this.closeCombo, true);		
		this._lastSelected = null;
		this._mouseDown = false;
		this._combo = this; 
	}
	ig_ComboBox.prototype.initIframe=function()
	{
		this._iframe = document.createElement('iframe');
		this._iframe.height = '0px';
		this._iframe.width = '0px';
		this._iframe.src = 'javascript:new String("<html></html>")';
		this._iframe.style.visibility = 'hidden';
		this._iframe.style.position = 'absolute';
		this._iframe.id = this._id +'_comboFrame';
		this._iframe.style.zIndex = '10000';
		document.body.appendChild(this._iframe);
		this._iframe.contentWindow.document.onselectstart = function(){return false;}
	}
	
	ig_ComboBox.prototype._onLoad = function(src, evnt)
    {
		this.initIframe();
    }
	
	ig_ComboBox.prototype.closeCombo=function(evnt)
	{
		if(ig_currentComboElem != null)
		{
			var elem = ig_currentComboElem;
			elem._mouseDown = false;
			if(elem._iframe.style.visibility == 'visible')
			{
				elem._iframe.style.visibility = 'hidden';
				var div = elem._iframe.contentWindow.document.getElementById(elem._id+'_parentDiv');
				elem._iframe.contentWindow.document.body.removeChild(div);
			}
		}
	}
	
	ig_ComboBox.prototype._openComboClick = function(evnt)
	{
		var src;
		if(evnt.target)
			src = evnt.target;
		else
			src = evnt.srcElement;
			
		src._combo.openCombo();
	}
	
	ig_ComboBox.prototype.openCombo=function()
	{ 
		ig_currentComboElem = this; 
		var link = this._iframe.contentWindow.document.createElement('link');
		var fileName = document.location.href.split('/');
		var path = document.location.href.replace(fileName[fileName.length-1],'');
		link.href = path + './Styles/AppointmentDialog.css';
		link.type = 'text/css';
		link.rel = 'stylesheet';
		var head = this._iframe.contentWindow.document.childNodes[0];
		while(head.tagName != 'HTML') head = head.nextSibling;
			head = head.childNodes[0];
		while(head.tagName != 'HEAD') head = head.nextSibling;
			head.appendChild(link);
		if(this._iframe.style.visibility == 'hidden')
		{
			var pdiv = this._getParentDiv();
			var returnVal =	eval(document.getElementById(this._id).dropDownEvent + "()");
			if(returnVal)
				return;
			var tempControl = this._input;
			var top = this._input.offsetHeight;
			while(tempControl != null)
			{
				top += tempControl.offsetTop;
				if(tempControl.tagName == 'BODY')
					top -= tempControl.scrollTop;
				tempControl = tempControl.offsetParent;
			}
			this._iframe.style.top = top;
			tempControl = this._elem;
            var left = 0;;
			if(this._input.clientLeft)
				left = this._input.clientLeft;
			while(tempControl.tagName != 'BODY')
			{
				left += tempControl.offsetLeft;
				tempControl = tempControl.offsetParent;
			}
			this._iframe.style.left = left;
			this._iframe.height = "200px";
			this._iframe.width = "100px";
			this._iframe.style.visibility = 'visible';
		}
	}
	
	ig_ComboBox.prototype._getParentDiv=function()
	{
		var pdiv = this._iframe.contentWindow.document.getElementById(this._id+'_parentDiv');
		if(pdiv == null)
			pdiv = this._iframe.contentWindow.document.createElement('div');
		pdiv.id = this._id+'_parentDiv';
		pdiv.style.position = 'absolute'
		pdiv.style.top = 0;
		pdiv.style.left = 0;
		pdiv.className = 'Fonts';
		this._iframe.contentWindow.document.body.style.backgroundColor = 'white';
		this._iframe.contentWindow.document.body.appendChild(pdiv);
		ig_addEventListener(pdiv, 'mousedown', this.selectStart, true);
		ig_addEventListener(pdiv, 'mouseup', this.select, true);
		ig_addEventListener(pdiv, 'mouseover', this.hover, true);
		pdiv._combo = this;
		return pdiv;
	}
	
	
	ig_ComboBox.prototype.populateCombo=function(array)
	{
		var pdiv = this._getParentDiv();
		
		for(var i =0 ; i < array.length; i++)	
		{
			var cdiv = this._iframe.contentWindow.document.createElement('div');
			cdiv.innerHTML = array[i];
			cdiv.className = 'Fonts';
			cdiv.style.cursor = 'pointer';
			cdiv.style.width = '78px';
			pdiv.appendChild(cdiv);	
		}
	}
	
	ig_ComboBox.prototype.getValue = function()
	{
		return this._input.value;
	}
	
	ig_ComboBox.prototype.setValue = function(val, updatePrev)
	{
		if(updatePrev || updatePrev == null)
		{
			if(this._input.value == '')
				this._input.prevValue = val;
			else
				this._input.prevValue = this._input.value;
		}
		this._input.value = val;
	}
	
	
	ig_ComboBox.prototype.scrollIntoView = function(val, id)
	{
		if(this._id != null)
			 id = this._id;
		var parent = this._iframe.contentWindow.document.getElementById(id+'_parentDiv');
		var parentElem = this._iframe.contentWindow.document.body;
		var childElem = val;
		var parentTop = parentElem.offsetTop;
		var childTop = childElem.offsetTop;
		if(parentElem.scrollTop == 0)
		{
			tempId = id;
			tempSelectedElement = val;
			setTimeout('ig_currentComboElem.scrollIntoView(tempSelectedElement, tempId)', 30);
		}
		parentElem.scrollTop = childTop;
	}
	
	ig_ComboBox.prototype.selectStart = function(evnt)
	{
		var src;
		if(evnt.target)
			src = evnt.target;
		else
			src = evnt.srcElement;
		
		if(src.tagName == 'DIV')
		{
			var combo = src.parentNode._combo;
			combo._mouseDown = false;
			var id = src.parentNode.id.replace('_parentDiv', '');
			if(combo._lastSelected != null)
			{
				combo._lastSelected.style.background = 'white';
				combo._lastSelected.style.color = 'black';
			}
			combo._lastSelected = src;
			src.style.background = 'navy';
			src.style.color = 'white';
			combo.setValue(src.innerHTML);
			eval(combo._elem.itemSelect + "( " + this._elem + ",  '\"+  src.innerHTML + \"' )" );
			combo._mouseDown = true;
		}
	}
	
	ig_ComboBox.prototype.select = function(evnt)
	{
		
		var src;
		if(evnt.target)
			src = evnt.target;
		else
			src = evnt.srcElement;
		if(src.tagName == 'DIV')
		{
			var combo = src.parentNode._combo;
			combo._mouseDown = false;
			var id = src.parentNode.id.replace('_parentDiv', '');
			combo.closeCombo();
			combo._input.onblur();
		}
		else
			combo.closeCombo();
	}
	
	ig_ComboBox.prototype.hover = function(evnt)
	{
		var src;
		if(evnt.target)
			src = evnt.target;
		else 
			src = evnt.srcElement;
		
		var combo = src._combo;
		if(src.tagName == 'DIV')
		{
			var combo = src.parentNode._combo;
			var id = src.parentNode.id.replace('_parentDiv', '');
			if(combo._mouseDown == true)
			{
				if(combo._lastSelected != null)
				{
					combo._lastSelected.style.background = 'white';
					combo._lastSelected.style.color = 'black';
				}
				combo._lastSelected = src;
				src.style.background = 'navy';
				src.style.color = 'white';
				combo.setValue(src.innerHTML);
				eval(combo._elem.itemSelect + "()" );
			}
		}
	}
	
	
	
	return new ig_ComboBox(id);
}

function ig_ComboBox(id)
{
	if(arguments.length > 0)
	{
		this.init(id);
	}
}

ig_addEventListener=function(elem,evtName,fn,flag)
{ 
	try{if(elem.attachEvent){elem.attachEvent("on"+evtName,fn); return;}}catch(ex){}
	try{if(elem.addEventListener){elem.addEventListener(evtName,fn,flag); return;}}catch(ex){}
	eval("var old=elem.on"+evtName);
	var sF=fn.toString();
	var i=sF.indexOf("(")+1;
	try
	{
	if((typeof old =="function") && i>10)
	{
		old=old.toString();
		
		var args=old.substring(old.indexOf("(")+1,old.indexOf(")"));
		while(args.indexOf(" ")>0) args=args.replace(" ","");
		if(args.length>0) args=args.split(",");
		
		old=old.substring(old.indexOf("{")+1,old.lastIndexOf("}"));
		
		sF=sF.substring(9,i);
		if(old.indexOf(sF)>=0)return;
		var s="fn=new Function(";
		for(i=0;i<args.length;i++)
		{
			if(i>0)sF+=",";
			s+="\""+args[i]+"\",";
			sF+=args[i];
		}
		sF+=");"+old;
		eval(s+"sF)");
	}
	eval("elem.on"+evtName+"=fn");
	}catch(ex){}
}

try{ig_addEventListener(window, "load", ig_combohandleEvent);}catch(ex){}

function ig_combohandleEvent(evt)
{
	if(evt == null) if((evt = window.event) == null) return;
	var obj, attr = ig_shared.attrID, src = evt.srcElement, type = evt.type;
	if(ig_shared.isEmpty(type)) return;
	var fn = "obj._on" + type.substring(0, 1).toUpperCase() + type.substring(1);
	if(src == null)
		src = evt.target;
	if(type == "load" || type == "unload" || type == "resize" || !src)
	{
		for(obj in ig_all)
		{
			if((obj = ig_all[obj]) == null)
				continue;
			eval("if(" + fn + "!=null){" + fn + "(src,evt); obj=null;}");
			if(obj != null && obj._onHandleEvent != null)
            obj._onHandleEvent(src, evt);
		}
		if(type == "unload")
			ig_dispose(ig_all);
		return;
	}
	var elem = ig_findElemWithAttr(src, attr);
	if(elem == null)
		elem = ig_findElemWithAttr(this, attr);
	if(elem != null && (obj = ig_getWebControlById(elem.getAttribute(attr))) != null)
	{
		eval("if(" + fn + "!=null){" + fn + "(src,evt); obj=null;}");
		if(obj != null && obj._onHandleEvent != null)
			obj._onHandleEvent(src, evt);
	}
}



/* WebCombo functionality shared between the AppointmentAdd dialog and the Recurrence Dialog */

var MilitaryTime = false;
var AM = "AM";
var PM = "PM";
var COLON = ":";
if(MilitaryTime)
{
	AM = "";
	PM = "";
}

function startTime_DropDown()
{
	startTime_onFocus();
	var times = new Array();
	
	if(!MilitaryTime)
	{
		times.push("12"+COLON+"00 " + AM);
		times.push("12"+COLON+"30 " + AM);
		for(var i = 1; i < 12; i++)
		{
			times.push(i.toString() + COLON + "00 " + AM);
			times.push(i.toString() + COLON + "30 " + AM);
		}
		times.push("12"+COLON+"00 " + PM);
		times.push("12"+COLON+"30 " + PM);
		for(var i = 1; i < 12; i++)
		{
			times.push(i.toString() + COLON + "00 " + PM);
			times.push(i.toString() + COLON + "30 " + PM);
		}
	}
	else
	{
		for(var i = 0; i < 24; i++)
		{
			times.push(i.toString() + COLON + "00");
			times.push(i.toString() + COLON + "30");
		}
	}
	startTimeDropDown.populateCombo(times); 
	var value = startTimeDropDown.getValue();
	var selectedElement = comboBox_selectValue(startTimeDropDown, value);
	startTimeDropDown.scrollIntoView(selectedElement);
}

function endTime_DropDown()
{
	var times = new Array();
	var startvalue = startTimeDropDown.getValue();
	var startIndex = startvalue.indexOf(COLON,0);
	var zeromin = parseInt(startvalue.substring(startIndex+1, startIndex +3));
	var zerohour = parseInt(startvalue.substring(0, startIndex));
	var thirtymin = zeromin + 30;
	var thirtyhour = zerohour;
	var indexTT = startvalue.indexOf("AM");
	var startTT = " " + AM;
	var endTT = " " + PM;
	if(indexTT == -1)
	{
		startTT = " " + PM;
		endTT = " " + AM;
	}
	var startTT2 = startTT;
	var endTT2 = endTT;
	
	if(thirtymin >= 60)
	{
		thirtymin -= 60;
		thirtyhour++;
	}
	if(!MilitaryTime)
	{
		if(thirtyhour > 12)
			thirtyhour -= 12;
	
		if(thirtyhour == 12 && thirtymin <30)
		{
			var temptt = startTT2;
			startTT2 = endTT2;
			endTT2 = temptt;
		}
	}
	else
	{
		if(zerohour == 23 && zeromin >= 30)
			thirtyhour = 0;
	}
		
	if(thirtymin < 10)
		thirtymin = "0" + thirtymin;
	if(zeromin < 10)
		zeromin = "0" + zeromin;
	times.push(startvalue);
	times.push(thirtyhour + COLON + thirtymin + startTT2);
	for(var i = 0; i < 23; i++)
	{
		zerohour++;
		thirtyhour++;
		if(!MilitaryTime)
		{
			if(zerohour > 12)
				zerohour -= 12;
			if(zerohour == 12 )
			{
				var temptt = startTT;
				startTT = endTT;
				endTT = temptt;
			}
			if(thirtyhour > 12)
				thirtyhour -=12;
			if(thirtyhour == 12)
			{
				var temptt = startTT2;
				startTT2 = endTT2;
				endTT2 = temptt;
			}
		}
		else
		{
			if(zerohour == 24)
				zerohour = 0;
			if(thirtyhour == 24)
				thirtyhour = 0;
		}
	
		times.push(zerohour + COLON + zeromin + startTT);
		times.push(thirtyhour + COLON + thirtymin + startTT2);
	}
	
	endTimeDropDown.populateCombo(times);
	var value = endTimeDropDown.getValue();
	var selectedElement = comboBox_selectValue(endTimeDropDown, value);
	endTimeDropDown.scrollIntoView(selectedElement);
}

function startTime_itemSelect()
{	
	startTime_onBlur();
}

function endTime_itemSelect( )
{
	var value = endTimeDropDown.getValue();
	var calEnd = igdrp_getComboById(endDateId)
	var enddate = calEnd.getValue();
	var calStart = igdrp_getComboById(startDateId)
	var startDate = calStart.getValue();
	var time = convertStringToDateTime(value);
	var startTime = convertStringToDateTime(startTimeDropDown.getValue());
	startDate.setHours(startTime.getHours(), startTime.getMinutes());
	if(startDate.getDate() == enddate.getDate())
	{
		if(startTime.getHours() > time.getHours() || (startTime.getHours() == time.getHours() && startTime.getMinutes() > time.getMinutes()))
			enddate.setFullYear(enddate.getFullYear(),enddate.getMonth(), enddate.getDate() + 1);
	}
	else if(startDate.getDate()+1 == enddate.getDate())
	{
		if(startTime.getHours() <= time.getHours())
			enddate.setFullYear(enddate.getFullYear(),enddate.getMonth(), enddate.getDate() - 1);			
	}
	enddate.setHours(time.getHours(), time.getMinutes());
	calEnd.setValue(enddate);
}

function comboBox_selectValue(combo, value)
{
	var iframe = combo._iframe;
	var pdiv = combo._getParentDiv();
	var valueTime = convertStringToDateTime(value);
	for(var i =0; i < pdiv.childNodes.length; i++)
	{
		var itemTime = convertStringToDateTime(pdiv.childNodes[i].innerHTML);
		var	diff = valueTime.getTime() - itemTime.getTime();
		diff = (diff/1000)/60;
		if(diff < 0)
			diff *= -1;
		if(diff < 30 && diff >= 0 && valueTime.getHours() == itemTime.getHours())
		{
			if(combo._lastSelected != null)
			{
				combo._lastSelected.style.background = "white";
				combo._lastSelected.style.color = "black";
			}
			combo._lastSelected = pdiv.childNodes[i];
			combo._lastSelected.style.background = "navy";
			combo._lastSelected.style.color = "white";
		}
	}
	
	return combo._lastSelected;
}


function startTime_onBlur()
{
	var returnVal = Time_onBlur(startTimeDropDown);
	if(returnVal)
		return
	var startValue = startTimeDropDown.getValue();
	
	var startTime = convertStringToDateTime(startValue);
	var calStart = igdrp_getComboById(startDateId)
	var startDateTime = calStart.getValue();
	startDateTime.setHours(startTime.getHours(),startTime.getMinutes());
	
	var calEnd = igdrp_getComboById(endDateId)
	var endDateTime = calEnd.getValue();
	endDateTime.setTime(startDateTime.getTime() + difference);
	calEnd.setValue(endDateTime);			
	
	var endTT = "";	
	var endhours = endDateTime.getHours();
	if(!MilitaryTime)
	{
		var endTT = " " +  AM;
		if(endhours > 11)
			endTT = " " +  PM;
		if(endhours >12)
			endhours -=12;
		if(endhours == 0)
			endhours = 12;
	}	
	var endMinutes = endDateTime.getMinutes();
	if(endMinutes < 10)
		endMinutes = "0" + endMinutes;
	
	endTimeDropDown.setValue(endhours+ COLON + endMinutes + endTT);									
	
}	
var difference = null;
function startTime_onFocus()
{
	var startValue = startTimeDropDown.getValue();
	var endValue = endTimeDropDown.getValue();
	
	var startTime = convertStringToDateTime(startValue);
	var endTime = convertStringToDateTime(endValue);
	
	var calStart = igdrp_getComboById(startDateId)
	var startDateTime = calStart.getValue();
	startDateTime.setHours(startTime.getHours(),startTime.getMinutes());
	var calEnd = igdrp_getComboById(endDateId)
	var endDateTime = calEnd.getValue();
	endDateTime.setHours(endTime.getHours(),endTime.getMinutes());
	difference = endDateTime.getTime() - startDateTime.getTime();
}
	
function endTime_onBlur()
{
	var returnVal = Time_onBlur(endTimeDropDown);
	if(!returnVal)
		endTime_itemSelect(endTimeDropDown, endTimeDropDown.getValue());
}

function Time_onBlur(elem)
{
	var value = elem.getValue();
	var tt = null;
	var firstTime;
	var zeroFound;
	while(value.indexOf("0",0 ) == 0){
		value = value.substring(1, value.length );
		zeroFound = true;
	}
	if(MilitaryTime && zeroFound)
		value = "0" + value;
				
	if(value == "" || parseInt(value.substring(0,1)).toString() == "NaN" )
	{
		alert("You Must Specify a Valid Time!");
		elem.setValue(elem._input.prevValue, false);
		Time_onBlur(elem);
		return true;
	}
	else
		firstTime = value.substring(0,1);
	
	if(parseInt(value.substring(1,2)).toString() != "NaN" )
		firstTime += value.substring(1,2);
		
	if(parseInt(firstTime) >= 24)
		firstTime = firstTime.substring(0,1);
	else
	{
		if(!MilitaryTime)
		{
			var timeTest = parseInt(firstTime);
			if(timeTest > 12)
			{
				firstTime = (timeTest - 12).toString();
				tt = " " + PM;
			}
		}
	}
	firstTime += COLON;
	
	var colon = value.indexOf(COLON, 0);
	if(colon == firstTime.length-1)
	{
		var firstNumber = parseInt(value.substring(colon+1,colon+2));
		var secondNumber = parseInt(value.substring(colon+2,colon+3));
		if(firstNumber.toString() != "NaN" )
		{
			if(firstNumber > 5)
				firstTime += "0" + firstNumber.toString();
			else if(secondNumber.toString() != "NaN" )
				firstTime += firstNumber.toString() + secondNumber.toString();
			else
				firstTime += "0" + firstNumber.toString();
		}	
		else
			firstTime += "00";		
	}	
	else
		firstTime += "00";		
	if(tt == null)
	{
		var p = value.indexOf(PM.substring(0,1).toLowerCase(), 0);
		var P = value.indexOf(PM.substring(0,1).toUpperCase(), 0);
		var a = value.indexOf(AM.substring(0,1).toLowerCase(), 0);
		var A = value.indexOf(AM.substring(0,1).toUpperCase(), 0);
		
		if(p != -1 || P != -1 || a != -1 || A != -1)
		{
			if(a == -1)
				a = 999;
			if(A == -1)
				A = 999;				
			if(((p < a || p < A) && p != -1) ||(( P < a || P < A) && P != -1))
				tt = " " + PM;
			else
				tt = " " + AM;			
		}
		else
			tt = " " + AM;
	}		
			
	if(!MilitaryTime)
		firstTime += tt;
	elem.setValue(firstTime);
}

function convertStringToDateTime(value)
{
	var time = new Date();
	var startIndex = value.indexOf(COLON, 0);
	var minutes = parseInt(value.substring(startIndex+1, startIndex+3), 10);
	var hours = parseInt(value.substring(0,startIndex), 10);
	if(!MilitaryTime)
	{
		var ttindex = value.indexOf("AM");
		if(ttindex == -1 && hours !=12)
			hours -=12;
		else if(ttindex != -1 && hours == 12)
			hours = 0;
	}				
	time.setHours(hours,minutes,0,0);
	return time;		
}

function convertDateTimeToString(dateTime)
{
	var hours = dateTime.getHours();
	var minutes = dateTime.getMinutes();
	var tt = "";
	
	if(!MilitaryTime)
	{
		tt = " "+ AM;
		if(hours > 12)
		{
			hours = hours - 12;
			tt = " " + PM;
		}
		else if(hours == 0)
			hours = 12;
		else if(hours == 12)
			tt = " " + PM;
	}
	if(minutes < 10)
			minutes = "0" + minutes;	
	return hours + COLON + minutes + tt;	
}

function DropDown_Cal1()
{
	var cal = igdrp_getComboById(startDateId)
	if(!cal.isDropDownVisible())
		cal.setDropDownVisible(true);
}

function DropDown_Cal2()
{
	var cal = igdrp_getComboById(endDateId)
	if(!cal.isDropDownVisible())
		cal.setDropDownVisible(true);
}

function DropDown_Cal3()
{
	var cal = igdrp_getComboById(endRecurrenceDateId)
	if(!cal.isDropDownVisible())
		cal.setDropDownVisible(true);
}

function wdcEndTime_ValueChanged(oDropDown, newValue, oEvent)
{
	var cal = igdrp_getComboById(startDateId)
	if(cal != null)
	{
		var date = new Date();
		if(newValue.getTime() < cal.getValue().getTime())
		{
			date.setTime(newValue.getTime());
			cal.setValue(date);
		}	
		calDifference = cal.getValue().getTime() - newValue.getTime();
		
	}
}

function wdcStartTime_ValueChanged(oDropDown, newValue, oEvent)
{
	var cal = igdrp_getComboById(endDateId)
	if(cal != null)
	{
		var date = new Date();
		date.setTime(newValue.getTime() - calDifference);
		cal.setValue(date);
		calDifference = newValue.getTime() - cal.getValue().getTime();
	}			
}
