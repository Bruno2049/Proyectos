/*
    Infragistics RecurrenceDialog Script
    Version 7.1.20071.40 
    Copyright (c) 2005 - 2006 Infragistics, Inc. All Rights Reserved.
*/		

var owdcStartDate = null;
var owdcEndDate = null;
var owdcEndRecurrence = null;
var startTimeDropDown = null;
var endTimeDropDown = null;
var startDateId = "wdcStartTime";
var endDateId = "wdcEndTime";
var endRecurrenceDateId = "wdcEndRecurrence";
var calDifference = null;
var recurrence = null;
var appointment = null;
var newRecurrence = false;
var invalidString = "The recurrence pattern is not valid"; 
var recDialogInterface = null;
var originalRecurrence = null; 
var originalRecurrence2 = null; 

if(this.dialogArguments || this.opener != null)
	recDialogInterface = ig_CreateRecurrenceDialog(this);

function recurrenceLoad()
{
	if(recDialogInterface != null)
	{
		appointment = recDialogInterface.getAppointment();
		recurrence = appointment.getRecurrence();
		if(recurrence == null)
		{
			recurrence = appointment.createRecurrence()
			newRecurrence = true;
		}
		else
		{
			originalRecurrence = recurrence.clone(); 
			originalRecurrence2 = recurrence.clone(); 
			originalRecurrence.setEndDate(originalRecurrence.getEndDate()); 
			originalRecurrence2.setMaxOccurrences(originalRecurrence2.getMaxOccurrences()); 
		}
		InitializeDates();
		InitializeRecurrencePattern();
		InitializeRangeOfRecurrence();
	}
}

function InitializeDates()
{
	/* Fixes a Firefox UI issue with the WebDateChooser */ 
	if(navigator.userAgent.toLowerCase().indexOf("mozilla")>=0)
	{
		if(navigator.userAgent.toLowerCase().indexOf("firefox")>=0)
		{
			var startInput = document.getElementById(startDateId + "_input"); 
			var endInput = document.getElementById(endDateId + "_input"); 
			var recurInput = document.getElementById(endRecurrenceDateId + "_input"); 
			startInput.style.border = "";
			endInput.style.border = "";
			recurInput.style.border = "";
			startInput = document.getElementById(startDateId); 
			endInput = document.getElementById(endDateId); 
			recurInput = document.getElementById(endRecurrenceDateId); 
			startInput.style.border = "";
			endInput.style.border = "";
			recurInput.style.border = "";
			
		}
	}	
	
	owdcStartDate = igdrp_getComboById(startDateId);
	owdcEndDate = igdrp_getComboById(endDateId);	
	owdcEndRecurrence = igdrp_getComboById(endRecurrenceDateId);
	startTimeDropDown = oddApptStartTime;
	endTimeDropDown = oddApptEndTime;

	startTimeDropDown._input.onblur = startTime_onBlur;
	startTimeDropDown._input.onfocus = startTime_onFocus;
	startTimeDropDown._elem.dropDownEvent = "startTime_DropDown";
	startTimeDropDown._elem.itemSelect = "startTime_itemSelect";

	endTimeDropDown._input.onblur = endTime_onBlur;
	endTimeDropDown._elem.dropDownEvent = "endTime_DropDown";
	endTimeDropDown._elem.itemSelect = "endTime_itemSelect";
	
	var startDate = new Date();
	startDate.setTime(appointment.getStartDateTime().getTime());
	if(newRecurrence)
		owdcStartDate.setValue(startDate);
	else 
	{
		var originalDate = new Date();
		originalDate.setTime(appointment.getOriginalStartDate().getTime());
		owdcStartDate.setValue(originalDate);
	}
		
	startTimeDropDown.setValue(convertDateTimeToString(startDate));
	
	var endDate = new Date();
	endDate.setTime(owdcStartDate.getValue().getTime());
	endDate.setMinutes(endDate.getMinutes() +  parseInt(appointment.getDuration()));
	owdcEndDate.setValue(endDate);
	endTimeDropDown.setValue(convertDateTimeToString(endDate));
	
	var date = new Date(startDate.getTime());
	date.setDate(date.getDate() + 70);
	owdcEndRecurrence.setValue(date)
	
	document.getElementById("wdcEndTime").style.height = "18px";
	document.getElementById("wdcEndTime_input").style.height = "18px";
	document.getElementById("wdcStartTime").style.height = "18px";
	document.getElementById("wdcStartTime_input").style.height = "18px";
	document.getElementById("wdcEndRecurrence").style.height = "18px";
	document.getElementById("wdcEndRecurrence_input").style.height = "18px";
	
	calDifference =  owdcStartDate.getValue().getTime() - owdcEndDate.getValue().getTime();
}

function InitializeRecurrencePattern()
{	
	//Recurrence Pattern
	var period = parseInt(recurrence.getPeriod());
	var periodMultiple = recurrence.getPeriodMultiple()
	var periodElements = document.getElementsByName("radioList");
		
	// Setup Default Values
	setDefaultDailyValues();
	setDefaultWeeklyValues();
	setDefaultMonthlyValues();
	setDefaultYearlyValues();
		
	if(ig_shared.IsIE)
		periodElements[period + 1].checked = true;
	else
	{		
		periodElements[period].checked = true;
		firefoxRadioChanged(periodElements[period]);
	}
	
	if(!newRecurrence)
	{		
		if(period == 0) // Daily
		{
			var daily = document.getElementsByName("dailyRadioList");
			if(ig_shared.IsIE)
			{
				if(recurrence.getDayOfWeekMask() == 62)
					daily[2].checked = true;
				else
					daily[1].checked = true;
			}
			else
			{
				if(recurrence.getDayOfWeekMask() == 62)
					daily[1].checked = true;
				else
					daily[0].checked = true;
			
			}
				
			document.getElementById("inputEveryXDays").value = periodMultiple;
		}
		else if (period == 1) // Weekly
		{
			var dayofweeks = document.getElementsByName("dayOfWeekCb");
			var mask = parseInt(recurrence.getDayOfWeekMask());
			while(mask != 0)
			{
				for(var i = dayofweeks.length-1 ; i >= 0 ; i--)
				{
					if((mask - dayofweeks[i].getAttribute("val")) >=0)
					{
						mask = mask - dayofweeks[i].getAttribute("val");
						dayofweeks[i].checked = true;
					}
				}
			}
			document.getElementById("inputRecursOn").value = periodMultiple;
		}
		else if(period == 2) // Monthly
		{
			var doM = parseInt(recurrence.getDayOfMonth());
			if(doM > 0)
			{
				document.getElementById("input1DayOf").value = doM;
				document.getElementById("input2DayOf").value = periodMultiple;
				if(ig_shared.IsIE)
					document.getElementsByName("monthlyRadioList")[1].checked = true;
				else
					document.getElementsByName("monthlyRadioList")[0].checked = true;
			}
			else
			{
				if(ig_shared.IsIE)
					document.getElementsByName("monthlyRadioList")[2].checked = true;
				else
					document.getElementsByName("monthlyRadioList")[1].checked = true;
				var domOccurrence = document.getElementById("select1XOf");
				doM *= (-1);
				doM -= 1; 
				domOccurrence.options[doM].selected = true;
				
				var dayofweeks = document.getElementById("select2XOf");
				var mask = parseInt(recurrence.getDayOfWeekMask());
				for(var i = 0; i < dayofweeks.options.length; i++)
				{
					if( dayofweeks.options[i].getAttribute("val") == mask)
					{
						dayofweeks.options[i].selected = true;
						break;
					}
				}
				
				document.getElementById("inputXOf").value = periodMultiple
			}
		}
		else // Yearly
		{
			var moY = recurrence.getMonthOfYear();
			var doM = parseInt(recurrence.getDayOfMonth());
			var index = 0;
			var monthList = document.getElementById("select1DateOf");
			var monthList2 = document.getElementById("select3XOfX");
			for(var i = 0; i < monthList.options.length; i++)
			{
				if(monthList.options[i].value == moY)
				{
					index = i;
					break;
				}
			}
			if(doM > 0)
			{
				document.getElementById("inputDateOf").value = doM;
				monthList.options[index].selected = true;
				if(ig_shared.IsIE)
					document.getElementsByName("yearlyRadioList")[1].checked = true;
				else
					document.getElementsByName("yearlyRadioList")[0].checked = true;
			}
			else
			{
				var dowMask = recurrence.getDayOfWeekMask();
				var dayList = document.getElementById("select2XOfX");
				for(var i = 0; i < dayList.options.length; i++)
				{
					if(dayList.options[i].getAttribute("val") == dowMask)
					{
						dayList.options[i].selected = true;
						break;
					}
				}
				monthList2.options[index].selected = true;
				doM *= (-1);
				doM -= 1; 
				var domOccurrence = document.getElementById("select1XOfX");
				domOccurrence.options[doM].selected = true;
				if(ig_shared.IsIE)
					document.getElementsByName("yearlyRadioList")[2].checked = true;
				else
					document.getElementsByName("yearlyRadioList")[1].checked = true;
				
			}
		}
	}		
}

function setDefaultDailyValues()
{
	var daily = document.getElementsByName("dailyRadioList");
	if(!ig_shared.IsIE)
		daily[0].checked = true;
	else
		daily[1].checked = true;
	document.getElementById("inputEveryXDays").value = recurrence.getPeriodMultiple();
}

function setDefaultWeeklyValues()
{
	var dayofweeks = document.getElementsByName("dayOfWeekCb");
	var d = new Date(appointment.getStartDateTime().getTime());
	var mask = Math.pow(2, d.getDay());
	for(var i = dayofweeks.length-1 ; i >= 0 ; i--)
	{
		if(mask == dayofweeks[i].getAttribute("val"))
		{
			dayofweeks[i].checked = true;
			break;
		}
	}
	document.getElementById("inputRecursOn").value = recurrence.getPeriodMultiple();
}
function setDefaultMonthlyValues()
{
	var d = new Date(appointment.getStartDateTime().getTime());
	var day = d.getDay();
	var monthRadioList = document.getElementsByName("monthlyRadioList");
	monthRadioList[monthRadioList.length -1].checked = true;
	document.getElementById("input1DayOf").value = d.getDate();
	document.getElementById("input2DayOf").value = recurrence.getPeriodMultiple();
	var dayDropDown = document.getElementById("select2XOf");
	for(var i = 0; i < dayDropDown.options.length; i++)
	{
		if(dayDropDown.options[i].value == day)
		{
			dayDropDown.options[i].selected = true;
			break;
		}
	}
	document.getElementById("inputXOf").value = recurrence.getPeriodMultiple();
	
	var originalMonth = d.getMonth();
	var count = -1; 
	while(originalMonth == d.getMonth())
	{
		count++;
		d.setDate(d.getDate() - 7);
	}
	var dowOccurrence = document.getElementById("select1XOf");
	dowOccurrence.options[count].selected = true;
	
}
function setDefaultYearlyValues()
{
	var d = new Date(appointment.getStartDateTime().getTime());
	var month = d.getMonth();
	var day = d.getDay();
	var yearRadioList = document.getElementsByName("yearlyRadioList");
	if(ig_shared.IsIE)
		yearRadioList[1].checked = true;
	else
		yearRadioList[0].checked = true;
	var monthList = document.getElementById("select1DateOf");
	var monthList2 = document.getElementById("select3XOfX");
	for(var i = 0; i < monthList.options.length; i++)
	{
		if(monthList.options[i].value == month+1)
		{
			monthList.options[i].selected = true;
			monthList2.options[i].selected = true;
			break;
		}
	}
	var dayList = document.getElementById("select2XOfX");
	for(var i = 0; i < dayList.options.length; i++)
	{
		if(dayList.options[i].value == day)
		{
			dayList.options[i].selected = true;
			break;
		}
	}
	document.getElementById("inputDateOf").value = d.getDate();
}
function InitializeRangeOfRecurrence()
{
	var rangeElements = document.getElementsByName("rangeRadioList");
	var rangeType = parseInt(recurrence.getMaxOccurrences());
	if(rangeType > 0 || recurrence.getEndDate() != null  && !newRecurrence)
		rangeType = 2; 
	if(rangeType > 0 && recurrence.getEndDate() == null)
		rangeType = 1; 
	
	if(ig_shared.IsIE)
		rangeElements[rangeType + 1].checked = true;
	else
		rangeElements[rangeType].checked = true;
	if(rangeType == 0)
		document.getElementById("inputEndAfter").value = "10";
	else
	{
		var maxOccur = recurrence.getMaxOccurrences();
		if(maxOccur == 0)
			maxOccur = 10; 
		document.getElementById("inputEndAfter").value = maxOccur;
		owdcEndRecurrence.setValue(recurrence.getEndDate());
	}
}

function radioChanged(elem)
{
	if(elem.checked)
		document.getElementById(elem.id+"_div").style.display= "";
	else
		document.getElementById(elem.id+"_div").style.display= "none";
	
}

var previouslyCheckedElem = null;
function firefoxRadioChanged(elem)
{
	if(previouslyCheckedElem == null)
		previouslyCheckedElem = document.getElementById("radWeekly"); 
	document.getElementById(previouslyCheckedElem.id+"_div").style.display= "none";
	document.getElementById(elem.id+"_div").style.display= "";
	previouslyCheckedElem = elem;	
}

function elem_focus(elem)
{
	var id = elem.id;
	id = id.replace("input1", "rad");
	id = id.replace("input2", "rad");
	id = id.replace("input", "rad");
	id = id.replace("select1", "rad");
	id = id.replace("select2", "rad");
	id = id.replace("select3", "rad");
	document.getElementById(id).checked = true;
}

function forward_focus(id1,id2,id3,id4)
{
	var elem = document.getElementById(id1);
	if(elem && elem.checked){elem.focus();return;}
	elem = document.getElementById(id2);
	if(elem && elem.checked){elem.focus();return;}
	elem = document.getElementById(id3);
	if(elem && elem.checked){elem.focus();return;}
	elem = document.getElementById(id4);
	if(elem && elem.checked){elem.focus();return;}
}

function backward_focus(id,evnt)
{
	if(!evnt)evnt=window.event;
	if(evnt.keyCode==9&&evnt.shiftKey){
		var elem=document.getElementById(id);
		if(elem)elem.focus();
	}
	return true;
}

function getPeriodValue()
{
	var periodElements = document.getElementsByName("radioList");
	var period; 
	var x = 1; 
	if(!ig_shared.IsIE)
		x = 0; 
	for(var i = x; i < periodElements.length; i++)
	{
		if(periodElements[i].checked)
		{
			period = i-x;
			break;
		}
	}
	return period; 
}

function getDayOfWeekMask(period)
{
	var dayOfWeekMask; 
	switch(period)
	{
		case 0:
		{
			var daily = document.getElementsByName("dailyRadioList");
			if(ig_shared.IsIE)
				dailyOcurChecked = daily[1].checked; 
			else
				dailyOcurChecked = daily[0].checked; 
				
			if( dailyOcurChecked)
				dayOfWeekMask = 0; 
			else
				dayOfWeekMask = 62; 
			break;
		}
		case 1:
		{
			var dayofweeks = document.getElementsByName("dayOfWeekCb");
			var mask = 0
			for(var i = dayofweeks.length-1 ; i >= 0 ; i--)
			{
				if(dayofweeks[i].checked)
					mask += parseInt(dayofweeks[i].getAttribute("val"));
			}
			dayOfWeekMask = mask; 
			break;
		}
		case 2:
		{
			var dayofweeks = document.getElementById("select2XOf");
			dayOfWeekMask = dayofweeks.options[dayofweeks.selectedIndex].getAttribute("val");
			break;
		}
		case 3:
		{
			var dayList = document.getElementById("select2XOfX");
			dayOfWeekMask = dayList.options[dayList.selectedIndex].getAttribute("val");
			break;
		}
	}
	return dayOfWeekMask; 

}

function getRangeLimitType()
{
	var rangeElements = document.getElementsByName("rangeRadioList");
	var rangeLimit = 1; 
	var x = 1; 
	if(!ig_shared.IsIE)
		x = 0; 
	for(var i = x; i < rangeElements.length; i++)
	{
		if(rangeElements[i].checked == true)
		{
			rangeLimit = i; 
			if(!ig_shared.IsIE)
				rangeLimit += 1; 
			break;
		}
	}
	return rangeLimit; 
}

function getPeriodMultiple(period)
{
	var periodMultiple;
	switch(period)
	{
		case 0:
		{
			periodMultiple = parseInt(document.getElementById("inputEveryXDays").value);
			break;
		}
		case 1:
		{
			periodMultiple = parseInt(document.getElementById("inputRecursOn").value); 
			break;
		}
		case 2:
		{
			var monthly = document.getElementsByName("monthlyRadioList");
			var first = 0; 
			if(ig_shared.IsIE)
				first = 1; 
			if(monthly[first].checked)
				periodMultiple = parseInt(document.getElementById("input2DayOf").value);
			else
				periodMultiple = parseInt(document.getElementById("inputXOf").value);
			break;
		}
		case 3:
		{
			break;
		}
	}
	return periodMultiple; 
}

function okClicked()
{	
	var startDateValue = owdcStartDate.getValue();
	var startTime = convertStringToDateTime(startTimeDropDown.getValue());
	
	var endDateValue = owdcEndDate.getValue();
	var endTime = convertStringToDateTime(endTimeDropDown.getValue());
	
	var startDateTime = new Date();
	startDateTime.setTime(startDateValue.getTime());
	startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
	appointment.setStartDateTime(startDateTime);
	
	var endDateTime = new Date();
	endDateTime.setTime(endDateValue.getTime());
	endDateTime.setHours(endTime.getHours(), endTime.getMinutes());
	var duration = (endDateTime.getTime() - startDateTime.getTime())/60000;
	appointment.setDuration(duration);

	var period = getPeriodValue(); 
	recurrence.setPeriod(period);
	
	if(period == 0 || period == 1)		// Daily || Weekly
	{
			recurrence.setDayOfWeekMask(getDayOfWeekMask(period));
			recurrence.setPeriodMultiple(getPeriodMultiple(period));
	}
	else if(period == 2)// Monthly
	{
		var monthly = document.getElementsByName("monthlyRadioList");
		if(!ig_shared.IsIE)
			firstChecked = monthly[0].checked; 
		else
			firstChecked = monthly[1].checked; 
		if(firstChecked)
		{
			recurrence.setDayOfMonth(parseInt(document.getElementById("input1DayOf").value));
			recurrence.setPeriodMultiple(getPeriodMultiple(period));
			recurrence.setDayOfWeekMask(0);
		}
		else
		{
			var domOccurrence = document.getElementById("select1XOf");
			recurrence.setDayOfMonth(-domOccurrence.options[domOccurrence.selectedIndex].value);
			
			recurrence.setDayOfWeekMask(getDayOfWeekMask(period));
			
			recurrence.setPeriodMultiple(getPeriodMultiple(period));			
		}
	}
	else if(period == 3)// Yearly
	{
		var yearly = document.getElementsByName("yearlyRadioList");
		if(!ig_shared.IsIE)
			firstChecked = yearly[0].checked; 
		else
			firstChecked = yearly[1].checked; 
		if(firstChecked)
		{
			var dayOfMonth = document.getElementById("inputDateOf").value; 
			var monthList = document.getElementById("select1DateOf");
			var month = monthList.options[monthList.selectedIndex].value; 
			if(!validateDayOfMonth(month-1,dayOfMonth))
				return; 
			
			recurrence.setDayOfMonth(dayOfMonth);
			recurrence.setMonthOfYear(month);
			recurrence.setDayOfWeekMask(0);
		}
		else
		{
			var monthList = document.getElementById("select3XOfX");
			recurrence.setMonthOfYear(monthList.options[monthList.selectedIndex].value);
			
			recurrence.setDayOfWeekMask(getDayOfWeekMask(period));
			
			var domOccurrence = document.getElementById("select1XOfX");
			recurrence.setDayOfMonth(-domOccurrence.options[domOccurrence.selectedIndex].value)
		}
	}
	
	var rangeLimit = getRangeLimitType();
	if(rangeLimit == 1)// No End Date
	{
		recurrence.setEndDate(null);
		recurrence.setMaxOccurrences(0);
	}
	else if(rangeLimit == 2) // Max Occurences
	{
		var occurrences = getMaxOccurrences(); 
		if(occurrences == null)
			return;
		
		recurrence.setMaxOccurrences(occurrences);
	}
	else if(rangeLimit == 3) // EndDate
	{
		recurrence.setEndDate(new Date(owdcEndRecurrence.getValue().getTime()));
		var temp = recurrence.getEndDate(); /* SJZ 5/24/06 BR12907 - If using the dateChooser, without touching it, an error gets thrown when the EndDate is accessed after the dialog is closed */
	}
		
	if(!newRecurrence)
	{
		var compare1 = originalRecurrence.compare(recurrence); 
		var compare2 = originalRecurrence2.compare(recurrence); 
		if(!compare1 && !compare2)
		{
			var result = confirm("Any exceptions associated with this recurring appointment will be lost. Is this OK?"); 
			if(result)
			{
				recurrence._updated = true; 
				recDialogInterface.okClose(recurrence);
			}
			else
			{
				if(compare1)
					recurrence = originalRecurrence.clone(); 
				else
					recurrence = originalRecurrence2.clone(); 
				
				return;
			}
		}
	}
	
	recDialogInterface.okClose(recurrence);
}

function getMaxOccurrences()
{
	var occurrences = parseInt(document.getElementById("inputEndAfter").value);
	if(occurrences < 0 || occurrences.toString().toLowerCase().indexOf("nan") > -1)
	{
		alert(invalidString);
		return null;
	}
	return  occurrences; 
}

function validateDayOfMonth(month, dayOfMonth)
{
	var date = new Date();
	if(month == 1 && dayOfMonth == 29)
		return true;  
	date.setMonth(month); 
	date.setDate(dayOfMonth); 
	if(date.getMonth() != month)
	{
		alert(invalidString); 
		return false; 
	}
	else
		return true; 
}

function cancelClicked()
{
	recDialogInterface.cancelClose();
}

function removeRecurrenceClicked()
{
	recDialogInterface.removeClose();
}

function wdcEndRecTime1_ValueChanged(oDropDown, newValue, oEvent)
{
	var cal = igdrp_getComboById(startDateId);
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

function wdcEndRecTime2_ValueChanged(oDropDown, newValue, oEvent)
{
	var cal = igdrp_getComboById(startDateId);
	var endCal = igdrp_getComboById(endDateId); 
	var calDiff = endCal.getValue().setHours(0,0,0) - cal.getValue().setHours(0,0,0); 
	if(cal != null && endCal != null)
	{
		var date = new Date();
		if(newValue.getTime() < cal.getValue().getTime())
		{
			date.setTime(newValue.getTime());
			var period = getPeriodValue(); 
			var maxOccurrences = getMaxOccurrences(); 
			if(maxOccurrences == null)
			{
				date.setDate(date.getDate() - 1); 
				cal.setValue(date);
				endCal.setValue(date); 
			} 
			switch(period)
			{
				case 0:
					date.setDate(date.getDate() - maxOccurrences); 
				break;
				case 1:
					date.setDate(date.getDate() - (7* maxOccurrences)); 
				break;
				case 2:
					date.setMonth(date.getMonth() - maxOccurrences); 
				break;
				
				case 3:
					date.setFullYear(date.getFullYear() - maxOccurrences); 
				break; 
			}
			cal.setValue(date);
			date = new Date(date.setHours(0,0,0) + calDiff); 
			date.setHours(date.getHours() + 1); 
			endCal.setValue(date); 
		}	
	}
}

function wdcStartRecTime_ValueChanged(oDropDown, newValue, oEvent)
{
	var cal1 = igdrp_getComboById(endDateId);
	var cal2 = igdrp_getComboById(endRecurrenceDateId);
	
	if(cal1 != null)
	{
		var date = new Date();
		date.setTime(newValue.getTime() - calDifference);
		cal1.setValue(date);
		calDifference = newValue.getTime() - cal1.getValue().getTime();
	}		
	if(cal2 != null)
	{
		
		if(newValue.getTime() >= cal2.getValue().getTime())
		{
			var date = new Date(); 
			date.setTime(newValue.getTime()); 
			var period = getPeriodValue(); 
			var maxOccurrences = getMaxOccurrences(); 
			if(maxOccurrences == null)
			{
				date.setDate(date.getDate() + 1); 
				cal2.setValue(date);
			}  
			switch(period)
			{
				case 0:
					date.setDate(date.getDate() + maxOccurrences); 
				break;
				case 1:
					date.setDate(date.getDate() + (7* maxOccurrences)); 
				break;
				case 2:
					date.setMonth(date.getMonth() + maxOccurrences); 
				break;
				
				case 3:
					date.setFullYear(date.getFullYear() + maxOccurrences); 
				break; 
			}
			cal2.setValue(date);
		}
	}
}
