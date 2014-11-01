/*
    Infragistics MCListbox Script
    Version 7.1.20071.40 
    Copyright (c) 2005 - 2006 Infragistics, Inc. All Rights Reserved.
*/	
function ig_CreateMCListbox(id)
{
	ig_MCListbox.prototype.constructor = ig_MCListbox;
	
	ig_MCListbox.prototype.init=function(elemId)
	{
		this.element = document.getElementById(elemId);
		this.Id = elemId;
		this.selectedItem = null;
		this.rows = null;
		ig_addEventListener(document, 'keydown', this.onkeyDown, true);
	}

	ig_MCListbox.prototype.rowSelected = function(newRowId)
	{
		if(newRowId != selectedItem)
		{
			var newrow = document.getElementById(newRowId);
			var prev = document.getElementById(selectedItem);
			selectedItem = newRowId;
			newrow.className ="MCLSelected";
			prev.className='MCLRowStyle';
			if(newrow.parentNode.parentNode.clickEvent)
				eval(newrow.parentNode.parentNode.clickEvent + '('+ newRowId + ')');
		}
	}

	ig_MCListbox.prototype.rowDoubleClicked = function(newRowId)
	{
		var newrow = document.getElementById(newRowId);
		if(newrow.parentNode.parentNode.dblclickEvent)
			eval(newrow.parentNode.parentNode.dblclickEvent + '('+ newRowId + ')');
	}

	ig_MCListbox.prototype.onkeyDown = function(evnt)
	{
		if(controlID != null)
		{
			var tbody = document.getElementById(controlID+'_m').getElementsByTagName('tbody')[0];
			var rows = tbody.getElementsByTagName('tr');
			if(selectedItem != null)
			{
				var input = document.getElementById('mc_Input');
				if(input == null)
				{
					input = document.createElement('input');
					input.id = 'mc_Input';
					input.className = 'HiddenInput';
				}
				var row = document.getElementById(selectedItem);
				var curIndex = row.rowIndex;
				if(evnt.keyCode == 38) // UP
				{ 
					if(curIndex > 0)
					{
						selectedItem = rows[curIndex-1].id;
						rows[curIndex-1].className = 'MCLSelected';
						rows[curIndex].className = 'MCLRowStyle';
						rows[curIndex-1].childNodes[2].appendChild(input);
						input.focus();
						rows[curIndex-1].childNodes[0].focus();
						if(rows[curIndex-1].parentNode.parentNode.keyDownEvent)
							eval(rows[curIndex-1].parentNode.parentNode.keyDownEvent + '('+ rows[curIndex-1].id + ',' + evnt.keyCode+ ')');
					}
				}									
				else if(evnt.keyCode == 40)	// Down
				{ 
					if(curIndex < rows.length-2)
					{
						selectedItem = rows[curIndex+1].id;
						rows[curIndex+1].className = 'MCLSelected';
						rows[curIndex].className = 'MCLRowStyle';
						rows[curIndex+1].childNodes[2].appendChild(input);
						input.focus();
						rows[curIndex+1].childNodes[0].focus();
						if(rows[curIndex+1].parentNode.parentNode.keyDownEvent)
							eval(rows[curIndex+1].parentNode.parentNode.keyDownEvent + '('+ rows[curIndex+1].id + ',' + evnt.keyCode+ ')');
					}				
				} 
				else 
					eval(row.parentNode.parentNode.keyDownEvent + '('+ selectedItem + ',' + evnt.keyCode+ ')');
			}
		}
	}

	ig_MCListbox.prototype.getRowCount = function(controlID)
	{
		var mc_control = document.getElementById(controlID + '_m');
		var tbody = mc_control.getElementsByTagName('tbody')[0];
		var rows = tbody.getElementsByTagName('tr');
		return rows.length - 1;
	}

	ig_MCListbox.prototype.removeRow = function(controlID, id)
	{ 
		if(id != null)	
		{
			var row = document.getElementById(id);
			var prevRowIndex = row.rowIndex; 
			var input = document.getElementById('mc_Input');
			var mc_control = document.getElementById(controlID + '_m');
			if(input != null)
				mc_control.appendChild(input);
			row.parentNode.removeChild(row);
			if(selectedItem == id)	{
				var tbody = mc_control.getElementsByTagName('tbody')[0];
				var rows = tbody.getElementsByTagName('tr');
				var index = prevRowIndex-1;
				if(index == -1 & rows.length > 1)
					index = 0;
				if(index != -1)	{
					selectedItem = rows[index].id;
					rows[index].className='MCLSelected';
					if(input != null){
						rows[index].childNodes[2].appendChild(input);
						input.focus();
					}
				}
				else
					selectedItem = null;
			}
		}
	}
	
	ig_MCListbox.prototype.selectStart = function()
	{ 
		window.event.returnValue = false;
	}

	ig_MCListbox.prototype.addRow = function(controlID, value1, value2, value3, dataKey)
	{
		var tbody = document.getElementById(controlID + '_m').getElementsByTagName('tbody')[0];
		var rows = tbody.getElementsByTagName('tr');
		var remove = rows[rows.length -1];
		remove.parentNode.removeChild(remove);
		var newRow = document.createElement('tr');
		newRow.setAttribute('dataKey', dataKey);
		var cell1 = document.createElement('td');
		cell1.innerHTML = value1
		cell1.style.width = '0%';
		var cell2 = document.createElement('td');
		cell2.innerHTML = '&nbsp;&nbsp;' + value2;
		cell2.style.width = '50%';
		var cell3 = document.createElement('td');
		cell3.innerHTML = '&nbsp; &nbsp;' + value3;
		cell3.style.width = '50%';
		cell3.noWrap = 'true';
		newRow.appendChild(cell1);
		newRow.appendChild(cell2);
		newRow.appendChild(cell3);
		var index = 0;
		if(rows.length > 0)
			index = parseInt(rows[rows.length-1].id.replace('MCROW_','')) + 1;
		newRow.id= 'MCROW_' + index;
		newRow.className = 'MCLRowStyle';
		tbody.appendChild(newRow);
		newRow.onclick = function(){rowSelected(newRow.id)};
		newRow.ondblclick = function(){rowDoubleClicked(newRow.id)};
		newRow.onselectstart = function(){selectStart()}
		tbody.appendChild(document.createElement('tr'));
		if(index == 0){
			selectedItem = newRow.id;
			newRow.className = 'MCLSelected';
		}
		return newRow;
	 }				
	 
	return new ig_MCListbox(id);
}

function ig_MCListbox(id)
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
			s+="""+args[i]+"",";
			sF+=args[i];
		}
		sF+=");"+old;
		eval(s+"sF)");
	}
	eval("elem.on"+evtName+"=fn");
	}catch(ex){}
}
	
