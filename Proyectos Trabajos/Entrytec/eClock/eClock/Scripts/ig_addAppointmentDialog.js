/*
    Infragistics AddAppointmentDialog Script
    Version 7.1.20071.40 
    Copyright (c) 2005 - 2006 Infragistics, Inc. All Rights Reserved.
*/	

function ig_CreateAddAppointmentDialog(dialogWindow)
{
	ig_AddAppointmentDialog.prototype.constructor = ig_AddAppointmentDialog;
	
	ig_AddAppointmentDialog.prototype.init=function(dialogWindow)
	{
		var opener = dialogWindow.opener;
		this._openerWindow = opener;
		this._values = opener.document.fieldValues;
		this.dialog = opener.document.__webDialog;
		this._activityEditProps = null;
		this._scheduleInfo = this.dialog._scheduleInfo;
		this._appointment = this._values.getValue("Appointment");
		this._recurrence = this._appointment.getRecurrence();
		if(this._recurrence != null)
		{
			var type = this._recurrence.getEditType();
			if(type == 0)
				type = this._recurrence.tempEditType;
			if(type == 2)
				this._appointment.setOriginalStartDate(new Date(this._appointment.getStartDateTime().getTime()));		
		}
		this._operation = null;
		dialogWindow.onunload = this.close;
		dialogWindow._appointmentDialog = this;
		this._recurrenceDialogDisplayed = false; 
		this._recurrenceDialogUrl = "Recurrence.aspx";
		this._recurrenceDialogHeight = "355px";
		this._recurrenceDialogWidth = "605px";
		if(!ig_shared.IsIE)
			this._recurrenceDialogWidth = "620px";	
	}
	ig_AddAppointmentDialog.prototype.getAppointment = function()
	{
		return this._appointment;
	}
	ig_AddAppointmentDialog.prototype.getActiveResourceName = function()
	{
		return this._values.getValue("ActiveResourceName");
	}
	ig_AddAppointmentDialog.prototype.getID = function()
	{
		return this._values.getValue("ID");
	}
	ig_AddAppointmentDialog.prototype.getTimeDisplayFormat = function()
	{
		return this._values.getValue("TimeDisplayFormat");
	}
	ig_AddAppointmentDialog.prototype.getAllowAllDayEvents = function()
	{
		return this._values.getValue("AllowAllDayEvents");
	}
	ig_AddAppointmentDialog.prototype.getSupportsRecurrence = function()
	{
		return this._values.getValue("SupportsRecurrence");
	}
	ig_AddAppointmentDialog.prototype.getWebScheduleInfo = function()
	{
		return this._values.getValue("WebScheduleInfo");
	}
	ig_AddAppointmentDialog.prototype.setOperation = function(val)
	{
		this._operation = val;
	}
	ig_AddAppointmentDialog.prototype.saveAndClose = function()
	{
		if(this._activityEditProps != null)
		{
			if(!this._scheduleInfo._onActivityDialogEdit(this._activityEditProps))		
			{
				if(this._operation != null)
					this._values["Operation"] = this._operation;	
				else if(this._appointment != null && this._appointment.getDataKey() != null)
					this._values["Operation"] = "Update";	
				else
					this._values["Operation"] = "Add";	
				
				if(this._appointment.recurrence != null)
				{
					var type = parseInt(this._appointment.recurrence.getEditType());
					if(type == 0)
						type = this._appointment.recurrence.tempEditType; 
					if(type == 2)
						this._appointment.setIsVariance(true);
				}
				
				this._values["Appointment"] = this._appointment;
				this.dialog._dialogClosed(true);
				this.dialog.closeDialog();
			}
		}
		else
		{
			this.dialog._dialogClosed(true);
			this.dialog.closeDialog();
		}
	}
	
	ig_AddAppointmentDialog.prototype.setActivityEditProps = function(activityEditProps)
	{
		this._activityEditProps = activityEditProps;
	}
	ig_AddAppointmentDialog.prototype.close = function()
	{
		this._appointmentDialog.dialog.closeDialog();
	}
	ig_AddAppointmentDialog.prototype.getRecurrenceDialogDisplayed = function()
	{
		return this._recurrenceDialogDisplayed;	
	}
	ig_AddAppointmentDialog.prototype.setRecurrenceDialogInformation = function(url, height, width)
	{
		this._recurrenceDialogUrl = url;
		this._recurrenceDialogHeight = height; 
		this._recurrenceDialogWidth = width; 
	}
	ig_AddAppointmentDialog.prototype.showRecurrenceDialog = function()
	{
		document._appointment = this._appointment; 
		var recurrenceDialog = null;
		this._recurrenceDialogDisplayed = true;
		document._dialog = this; 
		if(ig_shared.IsIE) // IE is Modal
		{
			recurrenceDialog = showModalDialog(this._recurrenceDialogUrl, document, "dialogHeight:"+ this._recurrenceDialogHeight +"; dialogWidth: " + this._recurrenceDialogWidth + " edge: Sunken; center: Yes; help: No; scroll:No;  resizable: Yes; status: No;");
			this._recurrenceDialogClosing();
		}
		else// Firefox is Modeless
		{
			recurrenceDialog = window.open(this._recurrenceDialogUrl, null, 'modal=yes,resizable=yes,scrollbars=auto,dependent=yes,toolbar=no,status=no,location=no,menubar=no,height='+this._recurrenceDialogHeight + ', width=' + this._recurrenceDialogWidth);
			recurrenceDialog.onunload = this._recurrenceDialogClosing;
		}
	}
	
	ig_AddAppointmentDialog.prototype._recurrenceDialogClosing = function(evnt)
	{
		/* unload fires twice, the first time it fires the url is "about:blank" ignore that one. */
		if(evnt != null && evnt.target.URL == "about:blank") 
			return; 
		var dialog = document._dialog; 
		var recurrence = dialog._appointment.getRecurrence();
		var date = new Date();
		date.setTime(dialog._appointment.getStartDateTime().getTime());
		dialog._appointment.setStartDateTime(date);
		if(recurrence != null && recurrence.getEndDate() != null)
		{
			var date2= new Date();
			date2.setTime(recurrence.getEndDate().getTime());
			recurrence.setEndDate(date2);
		}
		
		if(dialog.recurrenceDialogClosed)
		{
			dialog.recurrenceDialogClosed(dialog._appointment, recurrence);
		}
		
	}	
	
	return new ig_AddAppointmentDialog(dialogWindow);
}

function ig_AddAppointmentDialog(dialogWindow)
{
	if(arguments.length > 0)
	{
		this.init(dialogWindow);
	}
}
