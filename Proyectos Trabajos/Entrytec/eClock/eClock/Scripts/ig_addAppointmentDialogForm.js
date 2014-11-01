/*
    Infragistics AddAppointmentDialog Script
    Version 7.1.20071.40 
    Copyright (c) 2005 - 2006 Infragistics, Inc. All Rights Reserved.
*/

var dialogInterface = null;
if(this.opener != null)
{
	dialogInterface = ig_CreateAddAppointmentDialog(this);
}
			
function AppointmentAddLoad()
{
	if(dialogInterface != null)
	{
		SetupUI();
		InitializeValues();
	}
}

/* GLOBALS */
var startDateChooser = null;
var endDateChooser = null;
var startTimeDropDown = null;
var endTimeDropDown = null;
var startDateId = null;
var endDateId = null;
var calDifference = null;

function SetupUI()
{
	var ids = [':_ctl0:_ctl0:','$xctl0$ctl00$','xxctl0xctl00x','$_ctl0$ctl00$','__ctl0_ctl00_'];
	var i = -1;
	while(!startDateChooser && ++i < ids.length)
	{
		startDateId = 'UltraWebTab1' + ids[i] + 'wdcStartTime';
		startDateChooser = igdrp_getComboById(startDateId);
	}
	if(!startDateChooser)
	{
		alert("Error: can not find fields on appointment form");
		return;
	}
	endDateId = 'UltraWebTab1' + ids[i] + 'wdcEndTime';
	endDateChooser = igdrp_getComboById(endDateId);
	/* Stops the WebDateChooser's width from expanding to 100% in Firefox 1.5 */
	if(navigator.userAgent.toLowerCase().indexOf("mozilla")>=0)
	{
		if(navigator.userAgent.toLowerCase().indexOf("firefox")>=0)
		{
			if(navigator.userAgent.indexOf("1.5") >= 0)
			{
				var parentstartDateElem = document.getElementById(startDateChooser.Element.id+"_input").parentNode;
				parentstartDateElem.style.width="0px";
				parentstartDateElem = parentstartDateElem.parentNode.parentNode.parentNode;
				parentstartDateElem.style.width="0px";
				var parentEndDateElem = document.getElementById(endDateChooser.Element.id+"_input").parentNode;
				parentEndDateElem.style.width="0px";
				parentEndDateElem = parentEndDateElem.parentNode.parentNode.parentNode;
				parentEndDateElem.style.width="0px";
			}
		}
	}
	try
	{
		startTimeDropDown = oUltraWebTab1__ctl0__ctl0_ddStartTime;
		endTimeDropDown = oUltraWebTab1__ctl0__ctl0_ddEndTime;
	}
	catch(e)
	{
		startTimeDropDown = oUltraWebTab1__ctl0_ctl00_ddStartTime;
		endTimeDropDown = oUltraWebTab1__ctl0_ctl00_ddEndTime;
	}
	
	startTimeDropDown._input.onblur = startTime_onBlur;
	startTimeDropDown._input.onfocus = startTime_onFocus;
	startTimeDropDown._elem.dropDownEvent = "startTime_DropDown";
	startTimeDropDown._elem.itemSelect = "startTime_itemSelect";

	endTimeDropDown._input.onblur = endTime_onBlur;
	endTimeDropDown._elem.dropDownEvent = "endTime_DropDown";
	endTimeDropDown._elem.itemSelect = "endTime_itemSelect";
	
	/* Section 508 Compliant Labels */
	document.getElementById("startdateLabel508").htmlFor = startDateChooser.inputBox.id;
	document.getElementById("startTimeLabel508").htmlFor = startTimeDropDown._input.id;
	document.getElementById("endDateLabel508").htmlFor = endDateChooser.inputBox.id;
	document.getElementById("endTimeLabel508").htmlFor = endTimeDropDown._input.id;
	

	/*Removes the spacing between each toolbar item, which closes the gaps between the borders*/
	var toolbar = document.getElementById("UltraWebToolbar2");
	if(toolbar != null)
	{
		for(var i = 0 ; i < toolbar.childNodes.length; i++)
		{
			if(toolbar.childNodes[i].tagName == "TABLE")
			{
				toolbar.childNodes[i].cellSpacing = 0;
				break;
			}
		}
	}
		
	/* Removes the border around the Tab's content area */
	var apptTabContentArea = document.getElementById("UltraWebTab1_cp");
	if(apptTabContentArea != null)
		apptTabContentArea.style.border = "none";
		
	/* Stops the Tab's Width from Resizing */
	var appTabHeader = document.getElementById("UltraWebTab1td0");
	if(appTabHeader != null)
	{
		var lastTd = null;
		while(appTabHeader.nextSibling != null)
		{
			appTabHeader = appTabHeader.nextSibling;
			if(appTabHeader.tagName == "TD")
				lastTd = appTabHeader;
		}
		if(lastTd != null)
			lastTd.style.width = "100%";
	}

}

function InitializeValues()
{
	var appointment = dialogInterface.getAppointment();
	if(appointment.getIsOccurrence())
	{
		var recurrence = appointment.getRecurrence();
		if(recurrence != null)
		{
			var eReminder = document.getElementById("cbReminder");
			if(eReminder)
				eReminder.disabled = true;

			var type = recurrence.getEditType();
			
			if(type == 0)
				type = recurrence.tempEditType;
			if(type == 1)
			{
				document.getElementById("recurrenceTable").style.display = "";
				document.getElementById("durationTable").style.display = "none";
				document.getElementById("recurrenceDescriptionText").innerHTML = "Recurrence: " + recurrence.getRecurrenceDescription();
			}
			else if(type == 2)
				document.getElementById("statusBar").style.display = "";
		}
	}
	else if(appointment.getIsVariance())
	{
		document.getElementById("statusBar").style.display = "";
		var eReminder = document.getElementById("cbReminder");
		if(eReminder)
			eReminder.disabled = true;
	}
	
	if(!dialogInterface.getSupportsRecurrence() || ( (recurrence != null && type == 2) || appointment.getIsVariance())) /* HIDES THE RECURRENCE BUTTON IF RECURRENCES AREN'T SUPPORTED*/
	{		
		var recurrenceButton = document.getElementById("UltraWebToolbar2_Item_3");
		var node = recurrenceButton;
		while(node.tagName != "TABLE")
			node = node.parentNode;
		for(var i = 0; i < node.childNodes.length; i++)
		{
			if(node.childNodes[i].tagName == "COLGROUP")
			{
				if(!ig_shared.IsIE)
				{
					recurrenceButton.style.display = "none";
					recurrenceButton.style.visibility = "hidden";
				}
				else
				{
					node.childNodes[i].childNodes[6].style.display = "none";
					node.childNodes[i].childNodes[6].style.visibility = "hidden";
				}
				break;								   
			}				
		}
		recurrenceButton.tabIndex = -1;
	}	 
		
		
	var tdf = dialogInterface.getTimeDisplayFormat();
	if(tdf == 0)
		MilitaryTime = false;
	else if(tdf == 1)
		MilitaryTime = true;
		
	var startDate = new Date();
	startDate.setTime(appointment.getStartDateTime().getTime());
	startDateChooser.setValue(startDate);
	startTimeDropDown.setValue(convertDateTimeToString(startDate));
	
	var endDate = new Date();
	endDate.setTime(startDate.getTime());
	endDate.setMinutes(endDate.getMinutes() +  parseInt(appointment.getDuration()));
	endDateChooser.setValue(endDate);
	endTimeDropDown.setValue(convertDateTimeToString(endDate));
	
	var subject = document.getElementById("tbSubject");
	subject.value = appointment.getSubject();
	subject.focus();
	
	var loc = document.getElementById("tbLocation");
		loc.value = appointment.getLocation();
	
	var description = document.getElementById("txtMsgBody");
		description.value = appointment.getDescription();
	
	var alldayEvent = document.getElementById("cbAllDayEvent");
	alldayEvent.checked = appointment.getAllDayEvent();
	cbAllDayEvent_Clicked();
		
	if(!dialogInterface.getAllowAllDayEvents())
	{
		alldayEvent.checked = false;
		alldayEvent.style.display = "none"; 
		document.getElementById("AllDayEventLabel").style.display = "none";
		cbAllDayEvent_Clicked();
	}
		
	var enableReminder = document.getElementById("cbReminder");
	enableReminder.checked = appointment.getEnableReminder();
	cbReminder_Clicked();

	var displayInterval = document.getElementById("ddReminder");	
	var interval = convertTicksToString(appointment.getReminderInterval());
	var index = -1;
	for(var i = 0; i < displayInterval.options.length && index == -1; i++){
		if(interval == displayInterval.options[i].innerHTML)
			index = displayInterval.options[i].index;
	}
	if(index != -1)
		displayInterval.selectedIndex = index;
	else
	{
		var option = document.createElement("OPTION");
		displayInterval.appendChild(option);
		option.innerHTML = interval;
		option.value = "Test";
		displayInterval.value = "Test";
		window.setTimeout("document.getElementById('ddReminder').selectedIndex = document.getElementById('ddReminder').options.length -1;",1);
	}
	
	var showtimeas = document.getElementById("ddShowTimeAs");
	showtimeas.selectedIndex = appointment.getShowTimeAs();;
		
	var importance = igtbar_getToolbarById("UltraWebToolbar2");
	var importanceVal = appointment.getImportance();
	if(importance.Items[4].Type == "1")
		importance = importance.Items[4];
	else
		importance = importance.Items[5];
	if(importanceVal == "0")
		importance.Items[1].setSelected(true);
	else if(importanceVal == "2")
		importance.Items[0].setSelected(true);
	
	var dataKey = appointment.getDataKey();
	if(dataKey == null)
	{	
		var deleteButton = document.getElementById("UltraWebToolbar2_Item_2");
		var node = deleteButton;
		while(node.tagName != "TABLE")
			node = node.parentNode;
		for(var i = 0; i < node.childNodes.length; i++)
		{
			if(node.childNodes[i].tagName == "COLGROUP")
			{
				if(!ig_shared.IsIE)
				{
					deleteButton.style.display = "none";
					deleteButton.style.visibility = "hidden";
				}
				else
				{
					node.childNodes[i].childNodes[4].style.display = "none";
					node.childNodes[i].childNodes[4].style.visibility = "hidden";
				}				
				break;								   
			}				
		}
		deleteButton.tabIndex = -1; 
	}
		
	calDifference =  startDateChooser.getValue().getTime() - endDateChooser.getValue().getTime();
}


function OKClicked(oToolbar, oButton, oEvent) 
{	
	if(oButton.Key != "High" && oButton.Key != "Low" )
	{
		if(oButton.Index == 0 || oButton.Index == 2)	// Save and Close and Delete
		{
			var alldayEvent = document.getElementById("cbAllDayEvent");
			if(alldayEvent.checked)
			{
				var time = new Date();
				time.setHours(0,0,0,0);
				startTimeDropDown.setValue(convertDateTimeToString(time));
				time.setHours(23,59,0,0);
				endTimeDropDown.setValue(convertDateTimeToString(time));
			}
			var startDateValue = startDateChooser.getValue();
			var startTime = convertStringToDateTime(startTimeDropDown.getValue());
			
			var endDateValue = endDateChooser.getValue();
			var endTime = convertStringToDateTime(endTimeDropDown.getValue());
			
			var startDateTime = new Date();
			startDateTime.setTime(startDateValue.getTime());
			startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
			
			
			var endDateTime = new Date();
			endDateTime.setTime(endDateValue.getTime());
			endDateTime.setHours(endTime.getHours(), endTime.getMinutes());
			
			var duration = (endDateTime.getTime() - startDateTime.getTime())/60000;
			var intduration = parseInt(duration.toString());
			if((duration - intduration) > 0)
				duration += 1;
			
			var subject = document.getElementById("tbSubject");
			var loc = document.getElementById("tbLocation");
			var description = document.getElementById("txtMsgBody");			
			var enableReminder = document.getElementById("cbReminder");
			
			var displayInterval = document.getElementById("ddReminder");	
			var interval = convertStringToTicks(displayInterval.options[displayInterval.selectedIndex].innerHTML.split(" "));
			
			var showtimeas = document.getElementById("ddShowTimeAs");									
			
			var importance = igtbar_getToolbarById("UltraWebToolbar2");
			var highItem = importance.Items[5].Items[0];
			var lowItem = importance.Items[5].Items[1];
			var importanceValue = "1";
			if(highItem.getSelected())
				importanceValue = "2";
			else if(lowItem.getSelected())
				importanceValue = "0";
		var activityEditProps = {StartDate:		 {	Element: startDateChooser.Element, 
													Object : startDateChooser,
													Value  : startDateValue},
									StartTime:		 {	Element: startTimeDropDown._elem,
													Value  : startTime},
									EndDate:		 {	Element: endDateChooser.Element, 
													Object : endDateChooser,
													Value  : endDateValue},
									EndTime:		 {	Element: endTimeDropDown._elem,
													Value  : endTime},
									AllDayEvent:	 {	Element: alldayEvent, 
													Value  : alldayEvent.checked},
									Subject:		 {	Element: subject, 
													Value  : subject.value},
									Location:		 {	Element: loc, 
													Value  : loc.value}, 
									Description:	 {	Element: description, 
													Value  : description.value},
									EnableReminder: {	Element: enableReminder, 
													Value  : enableReminder.checked},
									DisplayInterval:{	Element: displayInterval, 
													Value  : interval}, 
									ShowTimeAs:	 {	Element: showtimeas, 
													Value  : showtimeas.options[showtimeas.selectedIndex].innerHTML},
									Importance:	 {	ToolBar: importance, 
													HighItem: highItem,
													LowItem: lowItem, 
													Value  : importanceValue},
									Duration:		 {  Value  : duration},
									document:		 document,
									window:		 window
								}	
			
			dialogInterface.setActivityEditProps(activityEditProps);
			var appointment = dialogInterface.getAppointment();
			var variance = false; 
			if(appointment.recurrence != null)
			{
				var type = parseInt(appointment.recurrence.getEditType());
				if(type == 0)
					type = appointment.recurrence.tempEditType; 
				if(type == 2)
					variance = true; 
			}
			else
				variance = appointment.getIsVariance(); 
			
			if(variance)
			{
				var wsi = dialogInterface.getWebScheduleInfo(); 
				var rootActivity = wsi.getActivities().getItemFromKey(appointment.getRootActivityKey()); 
				var recurrence = rootActivity.getRecurrence(); 
				var recurrenceEndDate = recurrence.getEndDate();
				if(recurrenceEndDate != null)
					recurrenceEndDate.setDate(recurrenceEndDate.getDate() + 1); 
				else
				{
					/* SJZ 9/18/06 BR15903 - If the recurrence doesn't have an end date then assume max date */
					recurrenceEndDate = new Date();
					recurrenceEndDate.setFullYear(9999,11,31);
				}
					
				var nextOccur = (appointment._nextOccur == null || appointment._nextOccur.length == 0) ? recurrenceEndDate.getTime() : appointment._nextOccur.getTime();
				var rootEndDate = rootActivity.getStartDateTime();
				rootEndDate.setHours(0,0,0,0); 
				var prevOccur = (appointment._prevOccur == null || appointment._prevOccur.length == 0) ? rootEndDate.getTime() : appointment._prevOccur.getTime(); 
				
				if(startDateTime.getTime() <= prevOccur)
				{					
					if(appointment._prevOccur == null || appointment._prevOccur.length == 0)
						alert("Cannot reschedule an occurrence if it occurs before the start date of the recurrence."); 
					else
						alert("Cannot reschedule an occurrence if it skips over an earlier occurrence of the same appointment."); 
					return; 
				}
				if(endDateTime.getTime() >= nextOccur)
				{
					if(appointment._nextOccur == null || appointment._nextOccur.length == 0)
						alert("Cannot reschedule an occurrence if it occurs later than the end date of the recurrence."); 
					else
						alert("Cannot reschedule an occurrence if it skips over a later occurrence of the same appointment."); 
					return; 
				}
				
			}
			
			var dataKey = appointment.getDataKey();
			if(oButton.Index == 0 && dataKey != null)
				dialogInterface.setOperation("Update");
			else if(oButton.Index == 0 && dataKey == null)
				dialogInterface.setOperation("Add");
			else if(oButton.Index == 2)	
				dialogInterface.setOperation("Delete");
				
			var type =  null;
			var recurrence = appointment.getRecurrence();
			
			if(recurrence != null)
				type = parseInt(appointment.getRecurrence().getEditType());
			
			if(type == 0)
				type = recurrence.tempEditType;
			
			if(type == 1 && !dialogInterface.getRecurrenceDialogDisplayed())
			{
				var date = new Date();
				date.setTime(appointment.getOriginalStartDate().getTime());
				appointment.setStartDateTime(date);
			}
			
			if(recurrence == null || type == 2)
			{	
				appointment.setAllDayEvent(alldayEvent.checked);
				appointment.setStartDateTime(startDateTime);	
				appointment.setDuration(duration);			
			}
			appointment.setSubject(subject.value);
			appointment.setLocation(loc.value);
			appointment.setDescription(description.value);
			appointment.setEnableReminder(enableReminder.checked);
			appointment.setReminderInterval(interval);
			appointment.setShowTimeAs(showtimeas.selectedIndex);
			appointment.setImportance(importanceValue);
							
			dialogInterface.saveAndClose();
		}
		else if(oButton.Index == 1) // Print
		{
			var ActiveResourceName = dialogInterface.getActiveResourceName();

			var frame = document.getElementById("printFrame");
			frame.style.position = 'absolute';
			frame.style.zIndex = -1;
			frame.style.height = 1;
			frame.style.width = 1;
			frame.style.visibility = "visible";

			var frameDocument = frame.contentWindow.document;
			
			frameDocument.getElementById("ResourceLabel").innerHTML = ActiveResourceName;
			frameDocument.getElementById("SubjectLabel").innerHTML =  document.getElementById("SubjectLabel").innerHTML;
			frameDocument.getElementById("tbSubject").innerHTML = document.getElementById("tbSubject").value;
			frameDocument.getElementById("LocationLabel").innerHTML =  document.getElementById("LocationLabel").innerHTML;
			frameDocument.getElementById("tbLocation").innerHTML = document.getElementById("tbLocation").value;
			frameDocument.getElementById("StartTimeLabel").innerHTML = document.getElementById("StartTimeLabel").innerHTML;
			frameDocument.getElementById("EndTimeLabel").innerHTML = document.getElementById("EndTimeLabel").innerHTML;
			frameDocument.getElementById("cbAllDayEvent").checked = document.getElementById("cbAllDayEvent").checked;
			if(frameDocument.getElementById("cbAllDayEvent").checked)
			{
				frameDocument.getElementById("ddStartTime").style.display = "none";
				frameDocument.getElementById("ddEndTime").style.display = "none";
			}
			else
			{
				frameDocument.getElementById("ddStartTime").innerHTML = startTimeDropDown.getValue();
				frameDocument.getElementById("ddEndTime").innerHTML = endTimeDropDown.getValue();
			}
			frameDocument.getElementById("AllDayEventLabel").innerHTML = document.getElementById("AllDayEventLabel").innerHTML;
			frameDocument.getElementById("cbReminder").checked = document.getElementById("cbReminder").checked;
			frameDocument.getElementById("cbReminderLabel").innerHTML = document.getElementById("ReminderLabel").innerHTML;
			var ddShowTimeAs = document.getElementById("ddShowTimeAs");
			frameDocument.getElementById("ddShowTimeAs").innerHTML = ddShowTimeAs.options[ddShowTimeAs.selectedIndex].innerHTML;
			var ddReminder = document.getElementById("ddReminder");
			frameDocument.getElementById("ddReminder").innerHTML = ddReminder.options[ddReminder.selectedIndex].innerHTML;
			frameDocument.getElementById("txtMsgBody").value = document.getElementById("txtMsgBody").value;
			frameDocument.getElementById("wdcStartTime").innerHTML = startDateChooser.getText();
			frameDocument.getElementById("wdcEndTime").innerHTML = endDateChooser.getText();
		
			frame.contentWindow.document.parentWindow.focus();
			frame.contentWindow.document.parentWindow.print();
			
			frame.style.visibility = "hidden";
		}
				
	}
	if(oButton.Index == 3) // Recurrence
	{
		var app = dialogInterface.getAppointment();
		var startDateValue = startDateChooser.getValue();
		var startTime = convertStringToDateTime(startTimeDropDown.getValue());
		
		var endDateValue = endDateChooser.getValue();
		var endTime = convertStringToDateTime(endTimeDropDown.getValue());
		
		var startDateTime = new Date();
		startDateTime.setTime(startDateValue.getTime());
		startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
		app.setStartDateTime(startDateTime);
		
		var endDateTime = new Date();
		endDateTime.setTime(endDateValue.getTime());
		endDateTime.setHours(endTime.getHours(), endTime.getMinutes());
		var duration = (endDateTime.getTime() - startDateTime.getTime())/60000;
		app.setDuration(duration);
		
		dialogInterface.recurrenceDialogClosed = updateRecurrence;
		dialogInterface.showRecurrenceDialog();
	}
}
document.recurrenceApplied = false;

function updateRecurrence(appointment, recurrence)
{
	var recurrenceTable = document.getElementById("recurrenceTable");
	var durationTable = document.getElementById("durationTable");
	if(recurrence != null)
	{
		durationTable.style.display = "none";
		document.getElementById("recurrenceDescriptionText").innerHTML = "Recurrence: This is a recurring appointment."; 
		recurrenceTable.style.display = "";
	}	
	else
	{
		durationTable.style.display = "";
		recurrenceTable.style.display = "none";
	}
}


function convertTicksToString(ticks){
	var seconds = ticks / 10000000;
	var minutes = seconds / 60;
	var hours = minutes / 60; 
	var days = hours / 24;
	var weeks = days / 7;
	if(days%7 != 0)
		weeks = 0; 
	var returnString; 
	
	if(weeks == 1)
		returnString = "1 week";
	else if(weeks > 1)
		returnString = weeks + " weeks";
	else if(days == 1)
		returnString = "1 day";
	else if (days > 1)
		returnString = days + " days";		
	else if(hours == 1)
		returnString = "1 hour";
	else if (hours > 1)
		returnString = hours + " hours";
	else if(minutes == 1)
		returnString = "1 minute";
	else if (minutes > 1 || minutes == 0)
		returnString = minutes + " minutes";
	else
		returnString = "1 minute or less";
		
	if(returnString == "12 hours")
		returnString = "0.5 days";
					
	return returnString;
}
function convertStringToTicks(string){
	var interval = string[0];
	var units = string[1];
	var ticks = 0; 
	if(units.indexOf("week",0) != -1)
		ticks = interval * 7 * 24 * 60 * 60 * 10000000;
	else if(units.indexOf("day",0) != -1)
		ticks = interval * 24 * 60 * 60 * 10000000;
	else if(units.indexOf("hour",0) != -1)
		ticks = interval * 60 * 60 * 10000000;
	else if(units.indexOf("minute",0) != -1)
		ticks = interval * 60 * 10000000;
	return ticks;	
}

function cbAllDayEvent_Clicked()
{
	var cb = document.getElementById("cbAllDayEvent");
	var td1 = document.getElementById("startTime");
	var td2 = document.getElementById("endTime");

	if(cb.checked)
	{
		startTimeDropDown._elem.style.visibility = "hidden"; 
		startTimeDropDown._elem.style.display = "none";
		endTimeDropDown._elem.style.visibility = "hidden"; 
		endTimeDropDown._elem.style.display = "none";
		td1.style.display = "none";
		td1.style.visibility = "hidden";
		td2.style.display = "none";
		td2.style.visibility = "hidden";
	}
	else
	{
		startTimeDropDown._elem.style.visibility = ""; 
		startTimeDropDown._elem.style.display = "";
		endTimeDropDown._elem.style.visibility = ""; 
		endTimeDropDown._elem.style.display = "";
		td1.style.display = "";
		td1.style.visibility = "";
		td2.style.display = "";
		td2.style.visibility = "";
	}
}



function cbReminder_Clicked()
{
	var reminder = document.getElementById("cbReminder");
	var ddreminder = document.getElementById("ddReminder");
	ddreminder.disabled = !reminder.checked;
}


