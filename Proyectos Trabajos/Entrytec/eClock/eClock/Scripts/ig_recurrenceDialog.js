
/*
    Infragistics RecurrenceDialog Script
    Version 7.1.20071.40 
    Copyright (c) 2005 - 2006 Infragistics, Inc. All Rights Reserved.
*/


function ig_CreateRecurrenceDialog(dialogWindow)
{
	ig_RecurrenceDialog.prototype.constructor = ig_RecurrenceDialog;
	
	ig_RecurrenceDialog.prototype.init=function(dialogWindow)
	{
		if(dialogWindow.dialogArguments) // Modal Dialog (IE)
		{
			this._window = dialogWindow; 
			this._appointment = dialogWindow.dialogArguments._appointment;
			this._appointmentDialogDocument = dialogWindow.dialogArguments;
		}
		else if(dialogWindow.opener != null)// Modeless Dialog (Firefox)
		{	
			this._window = dialogWindow; 
			this._appointmentDialogDocument = dialogWindow.opener.document;
			this._appointment = dialogWindow.opener.document._appointment; 
		}
	}
	ig_RecurrenceDialog.prototype.getAppointment=function()
	{
		return this._appointment;
	}
	ig_RecurrenceDialog.prototype.okClose=function(recurrence)
	{
		this._appointment.setRecurrence(recurrence); 
		this._window.close();
	}
	ig_RecurrenceDialog.prototype.removeClose=function()
	{
		this._appointment.setRecurrence(null); 
		this._window.close();
	}
	ig_RecurrenceDialog.prototype.cancelClose=function(recurrence)
	{
		this._window.close();
	}
	
	return new ig_RecurrenceDialog(dialogWindow);
}

function ig_RecurrenceDialog(dialogWindow)
{
	if(arguments.length > 0)
	{
		this.init(dialogWindow);
	}
}

