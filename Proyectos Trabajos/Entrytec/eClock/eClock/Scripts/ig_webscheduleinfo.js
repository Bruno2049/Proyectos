/*
* ig_webscheduleinfo.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


 




        
// WebScheduleInfo prototype and constructor

function ig_CreateWebScheduleInfo(props)
{
    if(!ig_WebControl.prototype.isPrototypeOf(ig_WebScheduleInfo.prototype))
    {
        ig_WebScheduleInfo.prototype = new ig_WebControl();
        ig_WebScheduleInfo.prototype.constructor = ig_WebScheduleInfo;
        ig_WebScheduleInfo.prototype.base=ig_WebControl.prototype;
        
        ig_WebScheduleInfo.prototype.init = function(props)
        {
            this._isInitializing = true;
            this._initControlProps(props);
            this.base.init.apply(this,[this.getClientID()]);
            this._isInitializing = false;
			this._ig_WebScheduleClientState = new ig_xmlNodeStatic();
			this.clientState = this._ig_WebScheduleClientState.createRootNode();	
			this.rootNode = this._ig_WebScheduleClientState.addNode(this.clientState, "XMLRootNode");
		
		    
			if(this.getEnableSmartCallbacks() && this.getEnableReminders())
			{
				this._fetchReminderInterval = 120000; 
				this._fetchRemindertimerId = setInterval(ig_createCallback(this.tickHandler, this, null), this._fetchReminderInterval);
			}
			this._addSnoozeStateItems()
        }
       
		ig_WebScheduleInfo.prototype.tickHandler = function() 
		{			
			this.fetchReminders(); 		    
		}
        
        ig_WebScheduleInfo.prototype.updateControlState = function(propName, propValue) {
			if(this.controlState == null)
				this.controlState = this._ig_WebScheduleClientState.addNode(this.rootNode, "ControlState");
				
			this._ig_WebScheduleClientState.setPropertyValue(this.controlState, propName, propValue);
			if(this.postField != null)
				this.postField.value = this._ig_WebScheduleClientState.getText(this.clientState);	
		}

		ig_WebScheduleInfo.prototype.addStateItem  = function(name, value) {
			if(this.stateItems == null)
				this.stateItems = this._ig_WebScheduleClientState.addNode(this.rootNode, "StateItems");
			var stateItem = this._ig_WebScheduleClientState.addNode(this.stateItems, "StateItem");
			this.updateStateItem(stateItem, name, value);
			return stateItem;
		}

		ig_WebScheduleInfo.prototype.updateStateItem = function(stateItem, propName, propValue) {
			this._ig_WebScheduleClientState.setPropertyValue(stateItem, propName, propValue);
			if(this.postField != null)
				this.postField.value = this._ig_WebScheduleClientState.getText(this.clientState);	
		}
		
		ig_WebScheduleInfo.prototype.getProgressIndicatorDefaultImage = function() {
           return this._props[2];
        } 
       
        ig_WebScheduleInfo.prototype.getAllowAllDayEvents = function() {
           return this._props[3];
        }  
        ig_WebScheduleInfo.prototype.getActiveDay = function() {
           return this._props[4];
        }
        this._pendingActiveDay = null;  
        this._updatingActiveDay = false;
        ig_WebScheduleInfo.prototype.setActiveDay = function(value, post, id) 
        {
			var oldDate = new Date();
			oldDate.setTime(this._props[4].getTime());
			value.setHours(oldDate.getHours(), oldDate.getMinutes(), oldDate.getSeconds());
			if(this.getEnableSmartCallbacks())
			{
				if(this._pendingActiveDay == null)
					this._pendingActiveDay = value;
				else if(this._pendingActiveDay.getTime() != value.getTime())
					this._pendingActiveDay = value;
				else if(value.getTime() == this._pendingActiveDay.getTime())
					return true;
			}
			if(!this._updatingActiveDay && !this._onActiveDayChanging(oldDate, value))
			{	
				var smartCallback = null;
				var enableSmartCallbacks = this.getEnableSmartCallbacks();
				if(this.getActiveDayClientSynchronization() ==  0 || enableSmartCallbacks)
				{	
					if(enableSmartCallbacks)
					{
						var serverContext = {operation:"ActiveDaySync", requestType:"json"};
						var clientContext = {operation:"ActiveDaySync", requestType:"json"};
						smartCallback = this._createSmartCallback(clientContext, serverContext, true, true);
						serverContext.ActiveDay = this._createServerDateString(value);
						
						serverContext = {operation:"ActiveDaySync", requestType:"styles"};
						clientContext = {operation:"ActiveDaySync", requestType:"styles"};
						smartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this)
					}
					this._updatingActiveDay = true;
					this.fireEvent("InternalSetActiveDay", null, value, id, smartCallback);					
				}	
				this._ActiveDayChangedValue = value;
				if(this._postRequest == 1 || post)
				{
					if(smartCallback != null)
					{
						if(smartCallback._registeredControls.length > 1)
							smartCallback.execute();
						this._updatingActiveDay = false;
					}
					else
						setTimeout("__doPostBack('"+this._uniqueID+"','')", 500);
				}
				else
					this._updatingActiveDay = false;	
				this._setActiveDayControlState(value); 
				this._onActiveDayChanged(this._props[4], value, false);	
				this._props[4] = value;    
				return true;
			}
			else if(this._updatingActiveDay)
				return true;
				
			return false;
        }
        ig_WebScheduleInfo.prototype._setActiveDayControlState = function(date)
        {
			this.updateControlState("ActiveDay", this._createServerDateString(date));
        }
        ig_WebScheduleInfo.prototype.getWorkDayStartTime = function() {
           return this._props[6];
        }  
        ig_WebScheduleInfo.prototype.getWorkDayEndTime = function() {
           return this._props[7];
        }  
        ig_WebScheduleInfo.prototype.getActiveResourceName = function() {
           return this._props[9];
        }  
        ig_WebScheduleInfo.prototype.getActiveResourceDataKey = function() {
           return this._props[10];
        }  

        ig_WebScheduleInfo.prototype.getAppointmentDialogID = function() {
           return this._props[11];
        }  
        ig_WebScheduleInfo.prototype.getReminderDialogID = function() {
           return this._props[12];
        }
        ig_WebScheduleInfo.prototype.getDefaultReminderInterval = function() {
           return this._props[13];
        }  
        ig_WebScheduleInfo.prototype.getDefaultSnoozeInterval = function() {
           return this._props[14];
        }
        ig_WebScheduleInfo.prototype.getTimeDisplayFormat = function() {
           return this._props[15];
        }      
        ig_WebScheduleInfo.prototype.getAllowAllDayEvents = function() {
           return this._props[16];
        }
        ig_WebScheduleInfo.prototype.getActiveDayClientSynchronization = function() {
           return this._props[17];
        }
        ig_WebScheduleInfo.prototype.getPreviousMonth = function() {
           return this._props[18];
        }
        ig_WebScheduleInfo.prototype.setPreviousMonth = function(value) {
			
			var date =  this._createServerDateString(value);
			if(!this.getEnableSmartCallbacks())
				this.updateControlState("PreviousMonth", date);
			this._props[18] = value;
        }
        ig_WebScheduleInfo.prototype.getLastActiveDate = function() {
           return this._props[19];
        }
        ig_WebScheduleInfo.prototype.getEnableSmartCallbacks = function() {
           return this._props[20];
        }
        ig_WebScheduleInfo.prototype.setEnableSmartCallbacks = function(value) {
            this._props[20]= value;
        }
        ig_WebScheduleInfo.prototype.getSupportsRecurrence = function() {
           return this._props[21];
        }
        ig_WebScheduleInfo.prototype.getVersion = function() {
           return this._props[22];
        }
        ig_WebScheduleInfo.prototype.getEnableProgressIndicator = function() {
           return this._props[23];
        }
        ig_WebScheduleInfo.prototype.getSnoozePersistenceType = function() {
           return this._props[24];
        }
        
        ig_WebScheduleInfo.prototype.getEnableReminders = function() {
           return this._props[25];
        }
        
        ig_WebScheduleInfo.prototype.getEnableMultiResourceView = function() {
           return this._props[26];
        }
        
        ig_WebScheduleInfo.prototype._onActiveDayChanging = function(oldDate, newDate)
        {
        	return this.fireEvent("ActiveDayChanging", null, oldDate, newDate);
        }
        this._activeDayStateItem  = null;
        ig_WebScheduleInfo.prototype._onActiveDayChanged = function(oldDate, newDate, post)
        {
			if(!this.getEnableSmartCallbacks())
			{
				if(this._activeDayStateItem== null)
					this._activeDayStateItem = this.addStateItem("ActiveDay", "Changed");	
				this.updateStateItem(this._activeDayStateItem, "OldDate", oldDate.getFullYear() + "-" + (oldDate.getMonth() + 1) + "-" + oldDate.getDate() + "-" + oldDate.getHours() + "-" + oldDate.getMinutes() + "-" + oldDate.getSeconds());
				this.updateStateItem(this._activeDayStateItem, "NewDate", newDate.getFullYear() + "-" + (newDate.getMonth() + 1) + "-" + newDate.getDate() + "-" + newDate.getHours() + "-" + newDate.getMinutes() + "-" + newDate.getSeconds());
			}
    		this.fireEvent("ActiveDayChanged", null, newDate);
    		if(post || this._postRequest == 1)
    			this.fireServerEvent("");
        }
        
        ig_WebScheduleInfo.prototype._onActivityAdding = function(activity, id)
        {
			return this.fireEvent("ActivityAdding", null, activity, id);
        }
        
        ig_WebScheduleInfo.prototype._onActivityDialogEdit = function(activityEditProps)
        {
        	return this.fireEvent("ActivityDialogEdit", null, activityEditProps);
        }
        
        ig_WebScheduleInfo.prototype._onActivityUpdating = function(activityUpdate, activity, id)
        {
        	return this.fireEvent("ActivityUpdating", null, activityUpdate, activity,  id);
        }
        
        ig_WebScheduleInfo.prototype._onActivityDeleting = function(activity, id)
        {
        	return this.fireEvent("ActivityDeleting", null, activity,  id);
        }
        ig_WebScheduleInfo.prototype._onActivityDialogOpening = function(dlg, activity)
        {
        	return this.fireEvent("ActivityDialogOpening", null, dlg, activity);
        }
        ig_WebScheduleInfo.prototype._onReminderDialogOpening = function(src, evnt)
        {
        	return this.fireEvent("ReminderDialogOpening", evnt);
        }
        
		ig_WebScheduleInfo.prototype.getActivities = function ()
		{
			if(this._collections[0] == null)
				return null;
			if(this._activities == null)
				this._activities = new ig_ActivityCollection(this._collections[0], this);
			return this._activities;
		} 
        
		ig_WebScheduleInfo.prototype.getReminders = function ()
		{
			if(this._collections[1] == null)
				return null;
			if(this._activityReminders == null)
				this._activityReminders = new ig_ReminderCollection(this._collections[1]);
			return this._activityReminders;
		} 
		
		ig_WebScheduleInfo.prototype.getSnoozeList = function()
		{
			return this._collections[2];
		}
		ig_WebScheduleInfo.prototype.getProgressIndicator = function()
		{
			if(this._progressIndicator == null)
				this._progressIndicator = new ig_progressIndicator(this.getProgressIndicatorDefaultImage());
			
			return this._progressIndicator; 
		}
		ig_WebScheduleInfo.prototype.setProgressIndicator = function(value)
		{
			this._progressIndicator = value; 
		}
		ig_WebScheduleInfo.prototype._createActivityServerContext = function (operation, requestType, activity, id)
		{
			var startDate = null, startTime = null;
			var recurrence = activity.getRecurrence();
			if(activity.getStartDateTime() != null)
				startDate =  this._createServerDateString(activity.getStartDateTime());
			var serverContext = {operation:operation, requestType:requestType,ID:id};
			
			this._setUpActivityServerInformation(this._addItemToServerContext, serverContext, activity);
			return serverContext;		
		}
		ig_WebScheduleInfo.prototype.updateActivityStateItem = function (stateItem, id, activity)
		{
			this.updateStateItem(stateItem, "ID", id);
			this._setUpActivityServerInformation(this._addItemToStateItem, stateItem, activity);
			
		}
		ig_WebScheduleInfo.prototype._addItemToServerContext = function(serverContext, id, value, wsi)
		{
			serverContext[id] = value; 
		}
		
		ig_WebScheduleInfo.prototype._addItemToStateItem = function(stateItem, id, value, wsi)
		{
			wsi.updateStateItem(stateItem, id, value);
		}
		
		ig_WebScheduleInfo.prototype._setUpActivityServerInformation = function(func, obj, activity)
		{
			var recurrence = activity.getRecurrence();
			if(activity.getStartDateTime() != null)
				func(obj, "date_appt_StartDateTime", this._createServerDateString(activity.getStartDateTime()), this);
			func(obj, "tsmin_appt_Duration",				activity.getDuration(), this);			
			func(obj, "str_appt_DataKey",					activity.getDataKey(), this);
			func(obj, "bool_appt_AllDayEvent",			activity.getAllDayEvent(), this);
			func(obj, "bool_appt_EnableReminder",			activity.getEnableReminder(), this);	
			func(obj, "str_appt_Subject",					activity.getSubject().replace(/\+/g,"%2B"), this);
			func(obj, "str_appt_Location",				activity.getLocation().replace(/\+/g,"%2B"), this);
			func(obj, "str_appt_Description",				activity.getDescription().replace(/\+/g,"%2B"), this);
			func(obj, "enum_appt_ShowTimeAs",				activity.getShowTimeAs(), this);
			func(obj, "enum_appt_Importance",				activity.getImportance(), this);
			func(obj, "tsticks_appt_ReminderInterval",	activity.getReminderInterval(), this);		
			func(obj, "str_appt_ResourceKey",				this.getActiveResourceDataKey(), this);
			func(obj, "ResourceName",				this.getActiveResourceName(), this);
			func(obj, "hex_appt_Timestamp",				activity.getTimestamp(), this);
			if(recurrence != null)
			{
				if(recurrence._updated != null && recurrence._updated == true)
					func(obj, "RecurrenceUpdated",			true, this);	
				func(obj, "enum_recur_EditType",			recurrence.getEditType(), this);
				func(obj, "int_recur_DayOfMonth",			recurrence.getDayOfMonth(), this);
				func(obj, "enum_recur_DayOfWeekMask",		recurrence.getDayOfWeekMask(), this);
				if(recurrence.getEndDate() != null)
					func(obj, "date_recur_EndDateUtc",	this._createServerDateString(recurrence.getEndDate()), this);
				func(obj, "int_recur_MonthOfYear",		recurrence.getMonthOfYear(), this);
				func(obj, "enum_recur_Period",			recurrence.getPeriod(), this);
				func(obj, "int_recur_PeriodMultiple",		recurrence.getPeriodMultiple(), this);
				if(recurrence.getMaxOccurrences() > 0)
					func(obj, "int_recur_MaxOccurrences",	recurrence.getMaxOccurrences(), this);
				if(activity.getOriginalStartDate() != null)
					func(obj, "OriginalStartDate",			this._createServerDateString(activity.getOriginalStartDate()), this);
				if(recurrence.getLastReminderDateTimeUtc() != null && recurrence.getLastReminderDateTimeUtc().length > 0)
					func(obj, "date_recur_LastReminderDateTimeUtc", this._createServerDateString(recurrence.getLastReminderDateTimeUtc()), this); 
			}
			if(activity.getRecurrenceKey() != null && activity.getRecurrenceKey().length > 0)
				func(obj, "str_appt_RecurrenceKey",		activity.getRecurrenceKey(), this);
			if(activity.getVarianceKey() != null && activity.getVarianceKey().length >0)
					func(obj, "guid_appt_VarianceKey",	activity.getVarianceKey(), this);
			if(activity.getIsVariance())
			{
				func(obj, "IsVariance",					true, this);
				var rootActivity = this.getActivities().getItemFromKey(activity.getRootActivityKey());
				var rootRecurrence = rootActivity.getRecurrence();
				func(obj, "str_appt_RecurrenceKey",		rootRecurrence.getKey(), this);
			}
		}

		ig_WebScheduleInfo.prototype.addActivity = function (activity, id)
		{
			if(!this._onActivityAdding(activity, id))
			{
				if(this.getEnableSmartCallbacks())
				{
					var smartCallback = null;
					var serverContext = this._createActivityServerContext("ActivityAdd", "json", activity, id);
					var clientContext = {operation:"ActivityUpdate", requestType:"json"};
					smartCallback = this._createSmartCallback(clientContext, serverContext, false, true)
					
					serverContext = {operation:"ActivityAdd", requestType:"styles"};
					clientContext = {operation:"ActivityAdd", requestType:"styles"};
					smartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this)
					
					this.fireEvent("InternalActivityUpdate", null, activity, id, smartCallback);	
					if(ig_shared.IsIE)
						smartCallback.execute();
					else
						this._firefoxExecute(smartCallback);
				}
				else
				{
					var stateItem = this.addStateItem("Appointment", "Add");
					this.updateActivityStateItem(stateItem, id, activity)
					this.fireServerEvent("Add", "Appointment");	
				}
			}
		}
		
		ig_WebScheduleInfo.prototype.updateActivity = function (activityUpdate, activity, id)
		{
			if(!this._onActivityUpdating(activityUpdate, activity, id))
			{
				if(activityUpdate.StartDateTime != null)
					activity.setStartDateTime(activityUpdate.StartDateTime);
				if(activityUpdate.Duration != null)
					activity.setDuration(activityUpdate.Duration);
				if(activityUpdate.Subject != null)
					activity.setSubject(activityUpdate.Subject);
				if(activityUpdate.Location != null)
					activity.setLocation(activityUpdate.Location);
				if(activityUpdate.Description != null)
					activity.setDescription(activityUpdate.Description);
				if(activityUpdate.AllDayEvent != null)
					activity.setAllDayEvent(activityUpdate.AllDayEvent);
				if(activityUpdate.EnableReminder != null)
					activity.setEnableReminder(activityUpdate.EnableReminder);
				if(activityUpdate.ShowTimeAs != null)
					activity.setShowTimeAs(activityUpdate.ShowTimeAs);
				if(activityUpdate.Importance != null)
					activity.setImportance(activityUpdate.Importance);
				if(activityUpdate.ReminderInterval != null)
					activity.setReminderInterval(activityUpdate.ReminderInterval);
				if(typeof(activityUpdate.Recurrence) == "object")
					activity.setRecurrence(activityUpdate.Recurrence);
				if(activityUpdate.IsVariance != null)
					activity.setIsVariance(activityUpdate.IsVariance);
				if(activityUpdate.OriginalStartDate != null)
					activity.setOriginalStartDate(activityUpdate.OriginalStartDate);
			
				if(this.getEnableSmartCallbacks())
				{
					var smartCallback = null;
					var serverContext = this._createActivityServerContext("ActivityUpdate", "json", activity, id);
					var clientContext = {operation:"ActivityUpdate", requestType:"json"};
					smartCallback = this._createSmartCallback(clientContext, serverContext, false, true)					
					
					serverContext = {operation:"ActivityUpdate", requestType:"styles"};
					clientContext = {operation:"ActivityUpdate", requestType:"styles"};
					smartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this)
					
					this.fireEvent("InternalActivityUpdate", null, activity, id, smartCallback);	
					if(ig_shared.IsIE)
						smartCallback.execute();
					else
						this._firefoxExecute(smartCallback);
					return true;
				}
				else
				{
					var stateItem = this.addStateItem("Appointment", "Update");
					this.updateActivityStateItem(stateItem, id, activity)
					this.fireServerEvent("Update","Appointment");
					return true;
				}
			}
			return false; 
		}
		
		ig_WebScheduleInfo.prototype._delayCallbackHandler = function()
		{
			clearTimeout(this._sCBTimerId);
			this._currentCallback.execute();
			this._currentCallback == null;
		}
		
		ig_WebScheduleInfo.prototype._firefoxExecute = function(smartCallback)
		{
			this._currentCallback = smartCallback; 
			this._sCBTimerId = setInterval(ig_createCallback(this._delayCallbackHandler, this, null), 500);
		}
		
		ig_WebScheduleInfo.prototype.deleteActivity = function (activity, id)
		{
			if(!this._onActivityDeleting(activity, id))
			{
				if(activity.getIsVariance())
				{
					var rootActivity = this.getActivities().getItemFromKey(activity.getRootActivityKey()); 
					var recurrenceKey = rootActivity.getRecurrence().getKey(); 
					var varianceDataKey = activity.getDataKey(); 
					var rootDataKey = rootActivity.getDataKey(); 
				}
				
				if(this.getEnableSmartCallbacks())
				{
					var date =  this._createServerDateString(this.getActiveDay());
					var smartCallback = null;
					var serverContext = this._createActivityServerContext("ActivityDelete", "json", activity, id);
					if(activity.getIsVariance())
					{
						serverContext.RecurrenceKey = recurrenceKey; 										
						serverContext.VarianceDataKey = varianceDataKey; 
						serverContext.RootDataKey = rootDataKey; 
					}	
					var clientContext = {operation:"ActivityDelete", requestType:"json"};
					smartCallback = this._createSmartCallback(clientContext, serverContext, false, true)
					
					serverContext = {operation:"ActivityDelete", requestType:"styles"};
					clientContext = {operation:"ActivityDelete", requestType:"styles"};
					smartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this)
					
					this.fireEvent("InternalActivityUpdate", null, activity, id, smartCallback);	
					
					if(ig_shared.IsIE)
						smartCallback.execute();
					else
						this._firefoxExecute(smartCallback);
					
				}
				else
				{
					var stateItem = this.addStateItem("Appointment", "Delete");	
					this.updateActivityStateItem(stateItem, id, activity)
					if(activity.getIsVariance())
					{
						this.updateStateItem(stateItem, "RecurrenceKey", recurrenceKey);
						this.updateStateItem(stateItem, "VarianceDataKey", varianceDataKey);
						this.updateStateItem(stateItem, "RootDataKey", rootDataKey);
					}
					this.fireServerEvent("Delete", "Appointment");
				}
			}
		}
		
		ig_WebScheduleInfo.prototype._modifyRecurrenceDialogClosing = function()
		{		    
			var oDlg = document._currentDialog;
			document._currentDialog = null;
			var fieldValues = oDlg.getFieldValues();
			if(document._appt.getRecurrence().tempEditType == 1 || document._varAppt == null)
				fieldValues.addValues("Appointment", document._appt);
			else
			{
				var appt = document._varAppt; 
				document._varAppt = null; 
				fieldValues.addValues("Appointment",appt);
			}
			if(document._cancelDlg)
			{	
				document._cancelDlg = null;									
				return true; 
			}
			else
				oDlg.showDialog(700, 800);	
			return true;
		}
		
		ig_WebScheduleInfo.prototype.showAddAppointmentDialogForOccurrence = function (dataKey, startDateTime, id, prevOccur, nextOccur) 
		{
			var activity = this.getActivities().getItemFromKey(dataKey); 
			activity.setStartDateTime(startDateTime);
			activity._prevOccur = prevOccur; 
			activity._nextOccur = nextOccur; 
			this.showAddAppointmentDialog(activity, id);
		}
		
		ig_WebScheduleInfo.prototype._showAppointmentDialog = function (dataKey, id, occur, prevOccur, nextOccur) 
		{
			if(prevOccur != null)
				prevOccur = eval(prevOccur); 
			if(nextOccur != null)
				nextOccur = eval(nextOccur); 
			if(occur != null)
			{
				occur = eval(occur); 
				var activity = this.getActivities().getItemFromKey(dataKey);
				activity.setStartDateTime(occur);
				activity._prevOccur = prevOccur; 
				activity._nextOccur = nextOccur; 
				this.showAddAppointmentDialog(activity, id);
			}
			else
				this._internalShowUpdateAppointmentDialog(dataKey, id, prevOccur, nextOccur); 
			
		}		
		ig_WebScheduleInfo.prototype.showAddAppointmentDialog = function (appointment, id) 
		{
			var oDlg = ig_getWebDialogActivatorById(this.getAppointmentDialogID());
			if(oDlg != null) 
			{
				if(!this._updatingActiveDay && !this._onActivityDialogOpening(oDlg, appointment))
				{
					var fieldValues = oDlg.getFieldValues();
					oDlg.setCallbackFunction("dialogClosed");
					oDlg._scheduleInfo = this;
					if(appointment.getIsVariance())
					{
						var clonedAppointment = this.getActivities().getItemFromKey(appointment.getRootActivityKey()); 
						clonedAppointment = clonedAppointment.clone(); 
						document._varAppt = appointment.clone(); 
						document._varAppt._prevOccur = appointment._prevOccur; 
						document._varAppt._nextOccur = appointment._nextOccur; 
					}
					else
					{
						var clonedAppointment = appointment.clone();
						clonedAppointment._prevOccur = appointment._prevOccur; 
						clonedAppointment._nextOccur = appointment._nextOccur; 
					}
					
					fieldValues.addValues("ActiveResourceName", this.getActiveResourceName());
					fieldValues.addValue("TimeDisplayFormat", this.getTimeDisplayFormat());
					fieldValues.addValue("AllowAllDayEvents", this.getAllowAllDayEvents());
					fieldValues.addValues("ID", id);
					fieldValues.addValue("SupportsRecurrence", this.getSupportsRecurrence());
					fieldValues.addValue("WebScheduleInfo", this); 
					
					if((clonedAppointment.getRecurrence() != null && clonedAppointment.getRecurrence().getEditType() == 0))
					{
						var url = oDlg.getContentUrl(); 
						var end = url.lastIndexOf('/')+1;
						url=url.substring(0,end);
						document._appt = clonedAppointment;
						document._currentDialog = oDlg;
						if(ig_shared.IsIE)
						{
							var modRecurrenceDialog = showModalDialog(url +"ModifyRecurrenceDialog.aspx", document, "dialogHeight:'200px'; dialogWidth: 300px; edge: Sunken; center: Yes; help: No; scroll:No;  resizable: Yes; status: No;");
							return this._modifyRecurrenceDialogClosing();
						}
						else// Firefox is Modeless
						{
							modRecurrenceDialog = window.open(url +"ModifyRecurrenceDialog.aspx", null, 'modal=yes,resizable=yes,scrollbars=auto,dependent=yes,toolbar=no,status=no,location=no,menubar=no,height=170px, width=300px');
							//modRecurrenceDialog.onunload = this._modifyRecurrenceDialogClosing;
							return true;
						}
					}
					else
					{
						if(appointment.getIsVariance() && clonedAppointment.getRecurrence().getEditType() == 2)
							clonedAppointment = appointment; 
						fieldValues.addValues("Appointment", clonedAppointment);
						oDlg.showDialog(700, 800);	
						return true;
					}
					return false; 	
				}
				else if(this._updatingActiveDay)
				{
					var startDateTime = appointment.getStartDateTime();
					var dataKey = (appointment.getDataKey() != null)? appointment.getDataKey(): -1;
					var dateTime =  this._createServerDateString(startDateTime);
					var isOccur = appointment.getIsOccurrence(); 
					var prevOccur = this._createServerDateString(appointment._prevOccur); 
					var nextOccur = this._createServerDateString(appointment._nextOccur); 
					this.updateControlState("ShowApptDialog", dataKey + "," + id + "," + dateTime  +"," + isOccur + "," + prevOccur + "," + nextOccur );
				}
			}
			else
			{
				_showAppointmentsQueued = new Object();
				_showAppointmentsQueued.scheduleInfo = this;
				_showAppointmentsQueued.appointment = appointment;
				_showAppointmentsQueued.id = id;
				return false;
			}
		}
		
		ig_WebScheduleInfo.prototype.showUpdateAppointmentDialog = function (datakey, id) 
		{
			this._internalShowUpdateAppointmentDialog(datakey, id, null, null); 		
		}
		ig_WebScheduleInfo.prototype._internalShowUpdateAppointmentDialog = function (datakey, id, prevOccur, nextOccur) 
		{
			var appointment = this.getReminders().getReminderFromKey(datakey);
			if(appointment == null)
				appointment = this.getActivities().getItemFromKey(datakey);
			if(appointment == null)
				return false; 
			appointment._prevOccur = prevOccur; 
			appointment._nextOccur = nextOccur; 						
			return this.showAddAppointmentDialog(appointment, id);
		}
		
		ig_WebScheduleInfo.prototype.createNewAppointment = function(dateTime)
		{
			var appointment = this.getActivities().createActivity();
			appointment.setStartDateTime(dateTime);
			return appointment;
		}
		ig_WebScheduleInfo.prototype.showReminders = function () 
		{
			var oDlg = ig_getWebDialogActivatorById(this.getReminderDialogID());
			if(oDlg != null) 
			{			    
				if(!this._onReminderDialogOpening())
				{				    
					var fieldValues = oDlg.getFieldValues();
					var reminders = this.getReminders();
					oDlg.setCallbackFunction("reminderDialogClosed");
					oDlg._scheduleInfo = this;
					fieldValues.addValues(
						"ActiveResourceName", this.getActiveResourceName(),
						"Reminders", reminders);
					if(reminders.length > 0)
					{
						var result = oDlg.showDialog(400, 600);
						return true;
					}
					return false; 
				}
			}
			else
			{
				_showReminderQueued = this;
				return false;
			}
		}
		var _defaultActivityDuration = 30;
		ig_WebScheduleInfo.prototype.getDefaultActivityDuration = function () 
		{
			return _defaultActivityDuration;
		}
		ig_WebScheduleInfo.prototype.setDefaultActivityDuration = function (val) 
		{
			_defaultActivityDuration = val;
		}
		
		
		ig_WebScheduleInfo.prototype.setActiveResource = function(resKey)
		{
		    if(resKey == this.getActiveResourceDataKey())
		        
		        return;
		    if(this.fireEvent("ActiveResourceChanging", null, resKey))
		        return;
		    var smartCallback = null;
			if(this.getEnableSmartCallbacks())
			{
				var serverContext = {operation:"SetActiveResource", requestType:"json", ResKey:resKey};
				var clientContext = {operation:"SetActiveResource", requestType:"json"};
				smartCallback = this._createSmartCallback(clientContext, serverContext, true, true)
				
				serverContext = {operation:"SetActiveResource", requestType:"styles"};
				clientContext = {operation:"SetActiveResource", requestType:"styles"};
				smartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this)
				
				this.updateControlState("ActiveResource", resKey);
				
				this.updateControlState("ResKey", resKey);
			}
			else
				this._updatingActiveResource = true;
			this.fireEvent("InternalSetActiveResource", null, resKey, smartCallback);
			
			if(smartCallback != null)
				smartCallback.execute();
			else
			{
				var stateItem = this.addStateItem("ActiveResource", "Set");	
				this.updateStateItem(stateItem, "ResKey", resKey);
				this.fireServerEvent("SetActiveResource", "ActiveResource");
			}
		}
		
		ig_WebScheduleInfo.prototype.navigate = function (years, months, days, id)
		{
			var smartCallback = null;
			var activeDay = this.getActiveDay();
			var newActiveDay = new Date();
			newActiveDay.setTime(activeDay.getTime());
			newActiveDay.setFullYear(newActiveDay.getFullYear() + years, newActiveDay.getMonth() + months, newActiveDay.getDate() + days);
			if(months == -1)
			{
				while(activeDay.getMonth() == newActiveDay.getMonth())
					newActiveDay.setDate(newActiveDay.getDate() - 1);
			}
			else if(months == 1)
			{
				while(activeDay.getMonth() == newActiveDay.getMonth() - 2)
					newActiveDay.setDate(newActiveDay.getDate() - 1);
			}
			
			if(this._onActiveDayChanging(activeDay, newActiveDay))
				return;
			if(this.getEnableSmartCallbacks())
			{
				var serverContext = {operation:"Navigate", requestType:"json", Years:years, Months:months, Days:days};
				var clientContext = {operation:"Navigate", requestType:"json"};
				smartCallback = this._createSmartCallback(clientContext, serverContext, true, true)
				 



				serverContext.ActiveDay = null; 
				
				serverContext = {operation:"Navigate", requestType:"styles"};
				clientContext = {operation:"Navigate", requestType:"styles"};
				smartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this)
				
				this._pendingActiveDay = newActiveDay;
				this.updateControlState("ActiveDay", this._createServerDateString(activeDay));
			}
			else
				this._updatingActiveDay = true;
			this.fireEvent("InternalNavigate", null, years, months, days, newActiveDay, id, smartCallback);

			//A.T. 22/11/2009 Fix for bug #25021 ActiveDayChanged client side event does not fire
			this.fireEvent("ActiveDayChanged", null, newActiveDay);
			
			if(smartCallback != null)
				smartCallback.execute();
			else
			{
				var stateItem = this.addStateItem("ActiveDay", "Navigate");	
				this.updateStateItem(stateItem, "Years", years);
				this.updateStateItem(stateItem, "Months", months);
				this.updateStateItem(stateItem, "Days", days);
				this.updateStateItem(stateItem, "ID", id);
				this.fireServerEvent("Navigate", "ActiveDay");
			}
		}
		ig_WebScheduleInfo.prototype._onUnload = function()
		{
			var dialog = ig_getWebDialogActivatorById(this.getReminderDialogID());
			if(dialog && dialog.isOpen())
			{
				dialog._dialogClosed(true);
				dialog.closeDialog();
			}
			dialog = ig_getWebDialogActivatorById(this.getAppointmentDialogID());
			if(dialog && dialog.isOpen())
			{
				dialog.closeDialog();
			}
		}
		ig_WebScheduleInfo.prototype._onLoad = function()
		{
			var old = this.getActiveDay();
			if(!old || !this.postField) return;
			var str = unescape(unescape(this.postField.value));
			var i = str.indexOf("ActiveDay=\"");
			if(i < 1) return;
			str = str.substring(i + 11);
			i = str.indexOf("\"");
			if(i < 8) return;
			str = str.substring(0, i).split(":");
			if(old.getFullYear() != parseInt(str[0]) || old.getMonth() + 1 != parseInt(str[1]) || old.getDate() != parseInt(str[2]))
				window.setTimeout("try{__doPostBack('"+this._uniqueID+"','');}catch(e){}", 0);
		}
		
		ig_WebScheduleInfo.prototype._createServerDateString = function(date)
		{
			if(date != null)
				return date.getFullYear() + ":" + (date.getMonth() + 1) + ":" + date.getDate() + ":" + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
			else
				return "";
		}
		
		ig_WebScheduleInfo.prototype.setFetchReminderInterval = function(interval)
		{	
		    this._fetchReminderInterval = interval; 
			if(interval >0)
			{				
				clearTimeout(this._fetchRemindertimerId);
				this._fetchRemindertimerId = setInterval(ig_createCallback(this.tickHandler, this, null), this._fetchReminderInterval);
			}
			else
				clearTimeout(this._fetchRemindertimerId);
		}
		
		ig_WebScheduleInfo.prototype.fetchReminders = function()
		{	
			var date =  this._createServerDateString(this.getActiveDay());
			var serverContext = {operation:"Refresh", requestType:"json"};
			var clientContext = {operation:"Refresh", requestType:"json"};
			var smartCallback = this._createSmartCallback(clientContext, serverContext, false, true);
			smartCallback.execute();
		}
		
		ig_WebScheduleInfo.prototype._reminderDismissedCallback = function(dataKey, status)
		{
			var serverContext = {operation:"ReminderDismissed", requestType:"json",DataKey:dataKey, Status:status};
			var clientContext = {operation:"ReminderDismissed", requestType:"json"};
			if(this._dismissSnoozeSmartCallback == null || this._dismissSnoozeSmartCallback.registerControl == null || this._dismissSnoozeSmartCallback._registeredControls == null)
				this._dismissSnoozeSmartCallback = this._createSmartCallback(clientContext, serverContext, false, false);
			else
				this._dismissSnoozeSmartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this);
		}
		
		ig_WebScheduleInfo.prototype._reminderSnoozeCallback = function(dataKey, snoozeInterval, snoozeTimestamp)
		{
			var serverContext = {operation:"SnoozeUpdate", requestType:"json", DataKey:dataKey, SnoozeInterval:snoozeInterval, SnoozeTimeStamp:snoozeTimestamp};
			var clientContext = {operation:"SnoozeUpdate", requestType:"json"};
			if(this._dismissSnoozeSmartCallback == null || this._dismissSnoozeSmartCallback.registerControl == null || this._dismissSnoozeSmartCallback._registeredControls == null)
				this._dismissSnoozeSmartCallback = this._createSmartCallback(clientContext, serverContext, false, false);
			else
				this._dismissSnoozeSmartCallback.registerControl(clientContext, serverContext, null, this._uniqueID, this);
		}
		
		ig_WebScheduleInfo.prototype._createSmartCallback = function(clientContext, serverContext, beforeCallback, callbackComplete)
		{
			var smartCallback = new ig_SmartCallback(clientContext, serverContext, null, this._uniqueID, this);
			serverContext.ActiveDay = this._createServerDateString(this.getActiveDay()); 
			serverContext.LastActiveDate = this._createServerDateString(this.getLastActiveDate());
            serverContext.ActiveResource = this.getActiveResourceDataKey();
            			
			if(beforeCallback)
				smartCallback.beforeCallback = this.beforeCallback;
			if(callbackComplete)
				smartCallback.callbackFinished = this.callbackComplete;
			
			if(this.getEnableProgressIndicator())
				smartCallback.setProgressIndicator(this.getProgressIndicator());
			
			return smartCallback; 
		}
		
		ig_WebScheduleInfo.prototype.callbackRender = function(response, context)
		{
			if(context.operation == "ReminderDismissed" || context.operation == "SnoozeUpdate")
				return;
			if(context.requestType == "json")
			{
				var originalActiveResourceDataKey = this.getActiveResourceDataKey();
				var json = eval(response.replace(/\^/g, "\""));
				this._decodeProps(json);
				this._props = json[0];
				var orginalRemindersCount = this._collections[1].length;
				this._collections = json[1];
				this._activities = null;
				this._activityReminders = null;
				if(json[2][0])
					this._remindersNeeded = true;
				
				
				this.postField.value = "";
				this.stateItems = null;
				this.controlState = null;
				this.clientState = this._ig_WebScheduleClientState.createRootNode();	
				this.rootNode = this._ig_WebScheduleClientState.addNode(this.clientState, "XMLRootNode");
				
				this.updateControlState('ActiveResource',this.getActiveResourceDataKey());
				if(originalActiveResourceDataKey != this.getActiveResourceDataKey())
    				this.fireEvent("ActiveResourceChanged", null, this.getActiveResourceDataKey());
				
				
				this._addSnoozeStateItems();
				this.updateControlState("ReminderDisplayTimeStamp", this._createServerDateString(new Date()));
				this._setActiveDayControlState(this.getActiveDay());				
			}
			clearTimeout(this._fetchRemindertimerId);
			
			if(this.getEnableReminders() && this._fetchReminderInterval > 0)
			    this._fetchRemindertimerId = setInterval(ig_createCallback(this.tickHandler, this, null), this._fetchReminderInterval);
		}
		ig_WebScheduleInfo.prototype._addSnoozeStateItems = function()
		{
			var snoozeList = this.getSnoozeList();
			if(this.getSnoozePersistenceType() == 1 && snoozeList != null && snoozeList.length > 0)
			{	
				for(var i =0; i < snoozeList.length; i++)
				{
					var stateItem = this.addStateItem('Snooze', 'Create');
					this.updateStateItem(stateItem, 'ActivityKey', snoozeList[i][0]);
					this.updateStateItem(stateItem, 'SnoozeInterval', snoozeList[i][1]);
					this.updateStateItem(stateItem, 'SnoozeTimeStamp', snoozeList[i][2]);
				}
			}
		}
		ig_WebScheduleInfo.prototype.callbackComplete = function()
		{
			var info = this._control;
			info._inCallback = false;
			if(info._queuedCallbacks != null && info._queuedCallbacks.length > 0)
			{	
				info._queuedCallbacks.reverse();
				info._queuedCallbacks.pop().execute();
				if(info._queuedCallbacks != null && info._queuedCallbacks.length > 1)
					info._queuedCallbacks.reverse();
				else
					this._pendingActiveDay = null;
			}
			else
			{
				info.fireEvent("InternalSetActiveDay", null, info.getActiveDay(), "", null);
				
				info.fireEvent("InternalSetActiveResource", null, info.getActiveResourceDataKey(), "", null);
			}
			if(info._remindersNeeded)
			{
				info._remindersNeeded = false;
				info.showReminders();
			}
		}
		ig_WebScheduleInfo.prototype.beforeCallback = function()
		{
			var info = this._control;
			if(info._inCallback)
			{
				if(info._queuedCallbacks == null)
					info._queuedCallbacks = new Array();
				
				
				if(this._registeredControls[0].serverContext.operation == "ActiveDaySync")
				{
					for(var i = 0; i < info._queuedCallbacks.length; i++)
					{
						if(info._queuedCallbacks[i]._registeredControls[0].serverContext.operation == "ActiveDaySync")
							info._queuedCallbacks.splice(i, 1);
					}
				}
				info._queuedCallbacks.push(this);
				return false;
			}
			
			info._inCallback = true;
			return true;
		}
		
		ig_WebScheduleInfo.prototype._fireFoxReminderDialogClosed = function()
		{
			clearTimeout(this._fireFoxDialogDelayedTimerId);
			this._fireFoxDialogDelayedTimerId = null;
			this.fireServerEvent("Process", "Reminder");
		}

		ig_WebScheduleInfo.prototype._dialogClosed = function(oDlg)
		{
		    var reminderDialog = ig_getWebDialogActivatorById(this.getReminderDialogID());
		    if (reminderDialog != null && reminderDialog.isOpen())
		        reminderDialog.closeDialog()
		    
		    if (oDlg == null && !ig_shared.IsIE)
		    {
		        clearTimeout(this._fireFoxDialogDelayedTimerId);
		        this._fireFoxDialogDelayedTimerId = null;
		        oDlg = this._internalFirefoxDialog;
		    }
		    var scheduleInfo = this;
		    var fieldValues = oDlg.getFieldValues();
		    var operation = fieldValues["Operation"];
		    var id = fieldValues["ID"];
            //this.setActiveResource(this.getActiveResourceDataKey());
		    var appointment = fieldValues["Appointment"];

		    if (operation == "Update")
		    {
		        var originalAppointment = appointment.getDataKey();
		        if (originalAppointment != null)
		            originalAppointment = scheduleInfo.getActivities().getItemFromKey(originalAppointment);
		        if (appointment.compare(originalAppointment))
		            return;
		        var appointmentDynamicObject = { StartDateTime: appointment.getStartDateTime(),
		            Duration: appointment.getDuration(),
		            Subject: appointment.getSubject(),
		            Location: appointment.getLocation(),
		            Description: appointment.getDescription(),
		            AllDayEvent: appointment.getAllDayEvent(),
		            EnableReminder: appointment.getEnableReminder(),
		            ShowTimeAs: appointment.getShowTimeAs(),
		            Importance: appointment.getImportance(),
		            ReminderInterval: appointment.getReminderInterval(),
		            Recurrence: appointment.getRecurrence(),
		            IsVariance: appointment.getIsVariance(),
		            OriginalStartDate: appointment.getOriginalStartDate()
		        };
		        scheduleInfo.updateActivity(appointmentDynamicObject, originalAppointment, id);
		    }
		    else
		    {
		        if (operation == "Delete")
		            scheduleInfo.deleteActivity(appointment, id);
		        else if (operation == "Add")
		            scheduleInfo.addActivity(appointment, id);
		    }
		}
		
		





	}
	return new ig_WebScheduleInfo(props);
}
        
function ig_WebScheduleInfo(props)
{
   if(arguments.length != 0)
       this.init(props);
}

// public: get object from ClientID or UniqueID
function ig_getWebScheduleInfoById(id)
{
	return ig_getWebControlById(id);
}  

function dialogClosed(oDlg, result) 
{
	if(result == true) 
	{
		if(oDlg._scheduleInfo == null)
			throw new Exception("Invalid method call - dialogClosed");
		var scheduleInfo = oDlg._scheduleInfo; 
		

		if(!ig_shared.IsIE)
		{
			scheduleInfo._internalFirefoxDialog = oDlg;
			scheduleInfo._fireFoxDialogDelayedTimerId = setInterval(ig_createCallback(scheduleInfo._dialogClosed, scheduleInfo, null), 500);
		}
		else
			scheduleInfo._dialogClosed(oDlg);
	}
}      

function reminderDialogClosed(oDlg, result) {
	if(result == true) {
		if(oDlg._scheduleInfo == null)
			throw new Exception("Invalid method call - reminderDialogClosed");
		var scheduleInfo = oDlg._scheduleInfo;	
		var fieldValues = oDlg.getFieldValues();
		var postBack = fieldValues.getValue("Dismissed");
		if(postBack == true)
		{
			if(scheduleInfo.getEnableSmartCallbacks())
			{
				if(scheduleInfo._dismissSnoozeSmartCallback != null)
				{
					var date =  scheduleInfo._createServerDateString(scheduleInfo.getActiveDay());
					var serverContext = {operation:"Refresh", requestType:"json", ActiveDay:date};
					var clientContext = {operation:"Refresh", requestType:"json"};
					scheduleInfo._dismissSnoozeSmartCallback.registerControl(clientContext, serverContext, null, scheduleInfo._uniqueID, scheduleInfo);
					scheduleInfo.fireEvent("InternalActivityUpdate", null, null, "", scheduleInfo._dismissSnoozeSmartCallback, true);	
					

					if(ig_shared.IsIE)
						scheduleInfo._dismissSnoozeSmartCallback.execute();
					else
						scheduleInfo._firefoxExecute(scheduleInfo._dismissSnoozeSmartCallback);
				}
			}
			else
			{
				if(!ig_shared.IsIE)
					scheduleInfo._fireFoxDialogDelayedTimerId = setInterval(ig_createCallback(scheduleInfo._fireFoxReminderDialogClosed, scheduleInfo, null), 500);
				else
					scheduleInfo.fireServerEvent("Process", "Reminder");
			}
		}
	}
}     

function ig_CreateActivity(props)
{
	if(!ig_Activity.prototype.isPrototypeOf(this.prototype))
    {
		ig_Activity.prototype.getStartDateTime = function() {
			return(this._props[0]);
		}
		ig_Activity.prototype.setStartDateTime = function(value) {
			this._props[0] = value;
		}
		ig_Activity.prototype.getDuration = function() {
			return(this._props[1]);
		}
		ig_Activity.prototype.setDuration = function(value) {
			this._props[1] = value;
		}
		ig_Activity.prototype.getDataKey = function() {
			return(this._props[2]);
		}
		ig_Activity.prototype.getAllDayEvent = function() {
			return(this._props[3]);
		}
		ig_Activity.prototype.setAllDayEvent = function(value) {
			this._props[3] = value;
		}
		ig_Activity.prototype.getStatus = function() {
			return(this._props[4]);
		}
		ig_Activity.prototype.getEnableReminder = function() {
			return(this._props[5]);
		}
		ig_Activity.prototype.setEnableReminder = function(value) {
			this._props[5] = value;
		}
		ig_Activity.prototype.getReminderInterval = function() {
			return(this._props[6]);
		}
		ig_Activity.prototype.setReminderInterval = function(value) {
			this._props[6] = value;
		}
		ig_Activity.prototype.getImportance = function() {
			return(this._props[7]);
		}
		ig_Activity.prototype.setImportance = function(value) {
			this._props[7] = value;
		}		
		ig_Activity.prototype.getShowTimeAs = function() {
			return(this._props[8]);
		}
		ig_Activity.prototype.setShowTimeAs = function(value) {
			this._props[8] = value;
		}		
		ig_Activity.prototype.getSnoozeTimeStamp = function() {
			return(this._props[9]);
		}
		ig_Activity.prototype.setSnoozeTimeStamp = function(value) {
			this._props[9] = value;
		}		
		ig_Activity.prototype.getSnoozeInterval = function() {
			return(this._props[10]);
		}
		ig_Activity.prototype.setSnoozeInterval = function(value) {
			this._props[10] = value;
		}		
		ig_Activity.prototype.getSubject = function() {
			return(this._props[11]);
		}
		ig_Activity.prototype.setSubject = function(value) {
			this._props[11] = value;
		}
		ig_Activity.prototype.getLocation = function() {
			return(this._props[12]);
		}
		ig_Activity.prototype.setLocation = function(value) {
			this._props[12] = value;
		}
		ig_Activity.prototype.getDescription = function() {
			return(this._props[13]);
		}
		ig_Activity.prototype.setDescription = function(value) {
			this._props[13] = value;
		}
		ig_Activity.prototype.getTimestamp = function() {
			return(this._props[14]);
		}
		ig_Activity.prototype.setTimestamp = function(value) {
			this._props[14] = value;
		}
		ig_Activity.prototype.getIsOccurrence = function() {
			return this._props[15];
		}
		ig_Activity.prototype.getRecurrence = function() 
		{
			if(this.recurrence == null && (this._props[16] != null && this._props[16].length > 0))
				this.recurrence = new ig_Recurrence(this._props[16]);
			return this.recurrence;
		}
		ig_Activity.prototype.setRecurrence = function(val) 
		{
			this.recurrence = val;
			if(val == null)
			{
				this._props[16] = null;
				this.setStartDateTime(this.getOriginalStartDate()); 
			}
		}
		ig_Activity.prototype.getRecurrenceKey = function() 
		{
			return this._props[17];
		}
		ig_Activity.prototype.getIsVariance = function() 
		{		
			return this._props[18];
		} 
		ig_Activity.prototype.setIsVariance = function(val) 
		{		
			this._props[18] = val;
		} 
		ig_Activity.prototype.getVarianceKey = function() 
		{		
			return this._props[19];
		}
		ig_Activity.prototype.getRootActivityKey = function() 
		{
			return this._props[20];
		}
		ig_Activity.prototype.getOriginalStartDate	 =	function()
		{
			var val = this._props[21];
			if(val == "")
				val = this.getStartDateTime();
			return val;
		}
		ig_Activity.prototype.setOriginalStartDate	 =	function(val)
		{
			this._props[21] = val;
		}
		ig_Activity.prototype.getExtendedDataKey	 =	function()
		{
			return this._props[22]; 
		}
		ig_Activity.prototype.getResourceKey         = function()
		{
		    return this._props[23];
		}
		ig_Activity.prototype.setResourceKey         = function(val)
		{
		    this._props[23] = val;
		}
		ig_Activity.prototype.getResourceName         = function()
		{
		    return this._props[24];
		}
		ig_Activity.prototype.setResourceName         = function(val)
		{
		    this._props[24] = val;
		}
		
		ig_Activity.prototype.clone = function()
		{
			var props = new Array();
			props = props.concat(this._props);
			var activity = new ig_CreateActivity(props); 
			if(this.getRecurrence() != null)
				activity.setRecurrence(this.getRecurrence().clone()); 
			return activity;
		}
		ig_Activity.prototype.compare = function(activity)
		{
			for(var i = 0; i < this._props.length; i++)
			{
				if(typeof(this._props[i]) == "object")
				{
					if(this._props[i] != null)
					{
						try
						{
							if(this._props[i].getTime() != activity._props[i].getTime())
								return false;
						}catch(e){};
					}
				}
				else
				{
					if(this._props[i] != activity._props[i])
						return false;
				}
			}
			var localRecurrence = this.getRecurrence();
			var awayRecurrence = activity.getRecurrence();
			if(localRecurrence != null)
			{
				if(awayRecurrence == null)
					return false;
				
				return localRecurrence.compare(awayRecurrence);
			}
			else 
			{
				if(awayRecurrence == null)
					return true;
				else 
					return false;
			}
			return true; 
		}
		ig_Activity.prototype.createRecurrence = function()
		{
			if(this.recurrence == null && (this._props[16] == null || this._props[16].length == 0))
			{
				var props = [
					0, 													// EditType
					0,													// DayOfMonth
					0,													// DayOfWeekMask
					new Date(),											// EndDate
					0,													// MaxOccurrences
					0,													// MonthOfYear
					1,													// Period
					1,													// PeriodMultiple
					"",													// Description
					""													// Key
					];
					
				return new ig_Recurrence(props);
			}
		}
		
	}
    return new ig_Activity(props);
}   
   
function ig_Activity(props)
{
	this._props = props;
}        
   
function ig_ActivityCollection(props, scheduleInfo) {
	this._props = props;
	this.length = props.length;
	this._scheduleInfo = scheduleInfo;
	
	this.getItem = function(index) {
		if(index < 0 || index > this._props.length)
			throw new Exception("Index Out of Bounds for ActivityCollection");
			
		if(this[index] == null)
			this[index] = ig_CreateActivity(this._props[index])
			
		return(this[index]);
	}
	
	this.getItemFromKey = function(dataKey)
	{
		for(var i = 0; i < this.length; i++)
		{
			if(this.getItem(i).getDataKey() == dataKey)
				return this[i];
		}		
		return null;	
	}
	this.createActivity = function()
	{
		var props = [
					new Date(), 										// StartDateTime
					this._scheduleInfo.getDefaultActivityDuration(),	// Duration
					null,												// DataKey
					false,												// AllDayEvent
					0,													// Status
					true,												// EnableReminder
					this._scheduleInfo.getDefaultReminderInterval(),	// ReminderInterval
					1,													// Importance
					3,													// ShowTimeAs
					0,													// SnoozeTimeStamp
					this._scheduleInfo.getDefaultSnoozeInterval(),		// SnoozeInterval
					"",													// Subject
					"",													// Location
					"",													// Description
					"0",												// Timestamp
					false,												// IsOccurrence
					null,												// Recurrence Object
					"",													// Recurrence Key
					false,												// IsVariance
					"",													// VarianceKey
					"",													// RootActivityKey
					"",													// OriginalStartDate
					null,												// ExtendedDataKey
					this._scheduleInfo.getActiveResourceDataKey(),      // ResourceKey
					this._scheduleInfo.getActiveResourceName()          // ResourceName
					];
		return new ig_CreateActivity(props);
	}
}   

function ig_Recurrence(props)
{
	this._props = props;
	ig_Recurrence.prototype.getEditType					=	function(){return this._props[0];}
	ig_Recurrence.prototype.setEditType					=	function(val){this._props[0] = val;}
	ig_Recurrence.prototype.getDayOfMonth				=	function(){return this._props[1];}
	ig_Recurrence.prototype.setDayOfMonth				=	function(val){this._props[1] = val;}
	ig_Recurrence.prototype.getDayOfWeekMask			=	function(){return this._props[2];}
	ig_Recurrence.prototype.setDayOfWeekMask			=	function(val){this._props[2] = val;}
	ig_Recurrence.prototype.getEndDate					=	function()
	{
		if(this._props[3] == "")
			return null;
		else
			return this._props[3];
	}
	ig_Recurrence.prototype.setEndDate					=	function(val)
	{
		this._props[3] = val;
		this._props[4] = 0; 
	}
	ig_Recurrence.prototype.getMaxOccurrences			=	function(){return this._props[4];}
	ig_Recurrence.prototype.setMaxOccurrences			=	function(val)
	{
		this._props[4] = val;
		this._props[3] = null;
	}
	ig_Recurrence.prototype.getMonthOfYear				=	function(){return this._props[5];}
	ig_Recurrence.prototype.setMonthOfYear				=	function(val){this._props[5] = val;}
	ig_Recurrence.prototype.getPeriod					=	function(){return this._props[6];}
	ig_Recurrence.prototype.setPeriod					=	function(val){this._props[6] = val;}
	ig_Recurrence.prototype.getPeriodMultiple			=	function(){return this._props[7];}
	ig_Recurrence.prototype.setPeriodMultiple			=	function(val){this._props[7] = val;}
	ig_Recurrence.prototype.getRecurrenceDescription	=	function(){return this._props[8];}
	ig_Recurrence.prototype.getKey						=	function(){return this._props[9];}
	ig_Recurrence.prototype._setKey						=	function(val){this._props[9] = val;}
	ig_Recurrence.prototype.getLastReminderDateTimeUtc	=	function(){return this._props[10];}
	ig_Recurrence.prototype.clone = function()
	{
		var props = new Array();
		props = props.concat(this._props);
		return new ig_Recurrence(props);
	}
	ig_Recurrence.prototype.compare						=  function(recurrence)
	{
		for(var i = 0; i < this._props.length; i++)
		{
			if(typeof(this._props[i]) == "object")
			{
				try
				{
				    
				    if(typeof(this._props[i] != typeof(recurrence._props[i])))
				        return false;
				    else if(this._props[i].getTime() != recurrence._props[i].getTime())
						return false;
				}catch(e){};
			}
			else
			{
				if(this._props[i] != recurrence._props[i])
					return false;
			}
		}
		return true; 
	}
	
}

function ig_CreateReminder(props)
{
	if(!ig_Activity.prototype.isPrototypeOf(ig_Reminder.prototype))
	{
		ig_Reminder.prototype = new ig_CreateActivity(props);
		ig_Reminder.prototype.constructor = ig_Reminder;
		ig_Reminder.prototype.base = ig_Activity.prototype;
	}
    
	return new ig_Reminder(props);
}

function ig_Reminder(props)
{
	this._props = props;		
}

function ig_ReminderCollection(props)
{
	this._props = props;
	this.length = props.length;
	
	this.getItem = function(index) {
		if(index < 0 || index > this._props.length)
			throw new Exception("Index Out of Bounds for ReminderCollection");
			
		if(this[index] == null)
			this[index] = ig_CreateReminder(this._props[index])
			
		return(this[index]);
	}
	
	this.getReminderFromKey = function(dataKey)
	{		
		for(var i = 0; i < this.length; i++)
		{
			if(this.getItem(i).getDataKey() == dataKey)
				return this[i];
		}		
		return null;	
	}
}



function ig_DayOrientedView(props)
{
	if(!props || typeof document=='unknown')
		return;
	this._initControlProps(props);
	this._element = document.getElementById(this._clientID+"_main");	
	if(this._element == null)
		this._element = document.getElementById(this._clientID);
	//this._element.tabIndex = 0; 
	this._dayElements = new Array(); 
	ig_shared.addEventListener(this._element, "mousedown", ig_handleEvent);
	ig_shared.addEventListener(this._element, "mouseup", ig_handleEvent);
	ig_shared.addEventListener(this._element, "click", ig_handleEvent);
	ig_shared.addEventListener(this._element, "dblclick", ig_handleEvent);
	ig_shared.addEventListener(this._element, "select", ig_cancelEvent);
	ig_shared.addEventListener(this._element, "selectstart", ig_cancelEvent);
	ig_shared.addEventListener(this._element,"keydown", ig_handleEvent); 
	
	ig_DayOrientedView.prototype._getDayHeaderIndex = function(elem)
		{
			for(var i = 0; i < this._dayElements.length; i++)
			{
				if(elem == this._dayElements[i])
					return i; 
			}
			return -1; 
		}
	
		
	ig_DayOrientedView.prototype._getNextElem = function(index, dir){}
	ig_DayOrientedView.prototype._processKeyDown = function(src, key, uie){}
	ig_DayOrientedView.prototype._getDayFromDayHeader = function(header){}
	ig_DayOrientedView.prototype._getApptsFromDay = function(elem){}
		
	ig_DayOrientedView.prototype._onKeydown = function(src, evt)
	{
		if(this.getSection508Compliant() || this.getEnableKeyboardNavigation())
		{		    
			if(src != this._element && src.tagName != "BUTTON")
				src.tabIndex = -1; 	
				
			var elem = null;
			var uie = src.getAttribute("uie");
			elem = this._processKeyDown(src, evt.keyCode, uie); 		
				
			if(elem != null)
			{
				
				elem.setAttribute("tabindex", 0);
				elem.tabIndex = this._element.tabIndex;
				elem.focus();  
				ig_cancelEvent(evt); 
			}
		}
	}
	
	ig_DayOrientedView.prototype._dayHeaderKeyDown = function(elem, key)
	{
		if(this._selectedAppt != null && this._selectedAppt.elem != null)
			this._selectAppt(null); 
		var index = this._getDayHeaderIndex(elem); 
		if(key >= 37 && key<=40)
			elem = this._getNextElem(index, key);	
		else if(key == 13 || key == 32)
		{		
			var date = this._getDateFromString(elem.getAttribute("date"));
			var tempdate = new Date(date.getTime());
			tempdate.setHours(0,0,0,0);
			var activeDay = this.getWebScheduleInfo().getActiveDay();
			activeDay.setHours(0,0,0,0);
			if(tempdate.getTime() == activeDay.getTime())
			{
				var appts = this._getAppointmentsFromDayHeader(elem); 
				if(appts == null || appts.length == 0 || key == 32)
					this._addNewAppointment(date); 
				else
				{	
					elem = appts[0]; 
					this._selectAppt(elem); 
				}
			}
			else
				this.getWebScheduleInfo().setActiveDay(date); 
		}
		else
			elem = null; 
		return elem; 
	}
	
	ig_DayOrientedView.prototype._apptKeyDown = function(elem, key)
	{
		if(key == 37)	   // Left
		{
			this._selectAppt(null)
			elem = this._getDayHeaderFromAppt(elem); 
			elem = this._dayHeaderKeyDown(elem, key); 
		}
		else if(key == 38) // Up
		{
			var day = this._getDayFromAppt(elem); 
			var appts = this._getApptsFromDay(day); 
			var i;
			for(i = 0; i < appts.length; i++)
			{
				if(appts[i] == elem)
					break;	
			}
			if(i == 0)
				elem = appts[appts.length-1]; 
			else 
				elem = appts[i-1]; 
				
			this._selectAppt(elem); 
			
		}
		else if(key == 39) // Right
		{
			this._selectAppt(null)
			elem = this._getDayHeaderFromAppt(elem); 
			elem = this._dayHeaderKeyDown(elem, key);
		}
		else if(key == 40) // Down
		{			     
			var day = this._getDayFromAppt(elem); 
			var appts = this._getApptsFromDay(day); 
			var i;
			for(i = 0; i < appts.length; i++)
			{
				if(appts[i] == elem)
					break;	
			}
			if(i == appts.length -1)
				elem = appts[0]; 
			else 
				elem = appts[i+1]; 
				
			this._selectAppt(elem); 
		}
		else if(key == 13) // Enter
			this._showUpdateAppointmentDialog(elem); 
		else if(key == 27) // Escape
		{
			this._selectAppt(null)
			elem = this._getDayHeaderFromAppt(elem); 
		}
		else if(key == 46) // Delete
		{
			var wsi = this.getWebScheduleInfo(); 
			var appt = wsi.getActivities().getItemFromKey(elem.getAttribute("ig_key")); 
			elem = this._getDayHeaderFromAppt(elem); 
			this._selectAppt(null); 
			wsi.deleteActivity(appt); 
		}
		else 
			elem = null; 
		return elem; 
	}
		
	ig_DayOrientedView.prototype._getDayHeaderFromAppt = function(appt)
	{	
		var elem = this._getDayFromAppt(appt); 
		elem = this._getDayHeaderFromDay(elem); 
		return elem; 
	}	
	
	ig_DayOrientedView.prototype._getElemFromNonUIE = function(src, key)
	{	    
		if(src.tagName == "BUTTON")
		{
			if(key == 32)
				return null; 
		}
		if(this._selectedAppt == null || this._selectedAppt.elem == null)
		{
			var date = this.getWebScheduleInfo().getActiveDay(); 
			var elem = this._getDayHeaderFromDate(date);
			if(elem == null)
			{
				date = this._getDateFromString(this._dayElements[0].getAttribute("date")); 
				date.setHours(0,0,0,0);
				elem = this._dayHeaderKeyDown(this._getDayHeaderFromDate(date), key); 
			}
			else
			{
				var origElem = elem; 
				elem = this._dayHeaderKeyDown(elem, key); 
				if(elem != null)
					return origElem;
			}
		}
		else
			elem = this._apptKeyDown(this._selectedAppt.elem, key); 
		return elem;
	}
	
	ig_DayOrientedView.prototype._getAppointmentsFromDayHeader = function(header)
	{
		var day = this._getDayFromDayHeader(header) 
		var appts = this._getApptsFromDay(day); 
		return appts; 
	}
	
	ig_DayOrientedView.prototype._getFirstApptElemFromStartDate = function(date)
	{
		var wsi = this.getWebScheduleInfo(); 
		var dayHeader = this._getDayHeaderFromDate(date); 
		var appts = this._getAppointmentsFromDayHeader(dayHeader); 
		return appts; 
	}
		
	ig_DayOrientedView.prototype._getDayHeaderFromDate = function(date)
	{
		for(var i = 0; i < this._dayElements.length; i++)
		{
			var d =  this._getDateFromString(this._dayElements[i].getAttribute("date")); 
			d.setHours(0,0,0,0);
			if(d.getTime() == date.getTime())
				return this._dayElements[i]; 
		}
		return null; 
	}
	
	ig_DayOrientedView.prototype._getDateFromString = function(str)
	{
		var date = str.split(",");
		var dateTime = new Date();
		dateTime.setFullYear(date[0], date[1], date[2],0,0,0);
		return dateTime; 
	}
	
	ig_DayOrientedView.prototype._showUpdateAppointmentDialog = function(src)
	{
		if(this.getEnableAutoActivityDialog())
		{
			var scheduleInfo = this.getWebScheduleInfo(); 
			var prevOccur = src.getAttribute("ig_prevOccur"); 
			var nextOccur = src.getAttribute("ig_nextOccur"); 
			var occur = src.getAttribute("ig_occur");
			var key = src.getAttribute("ig_key");
			scheduleInfo._showAppointmentDialog(key, this._clientID, occur, prevOccur, nextOccur) 
		}
	}
	
	ig_DayOrientedView.prototype._addNewAppointment = function(dateTime)
	{
		if(this.getEnableAutoActivityDialog())
		{
			var scheduleInfo = this.getWebScheduleInfo(); 
			//var dateTime = new Date();
			var minutes = dateTime.getMinutes();
			if(minutes < 30)
				dateTime.setMinutes(30);
			else if(minutes > 30)
				dateTime.setHours(dateTime.getHours() + 1,0);
			var appointment = scheduleInfo.getActivities().createActivity();
			appointment.setStartDateTime(dateTime);
			scheduleInfo.showAddAppointmentDialog(appointment, this._clientID);
		}
	}
	
	ig_DayOrientedView.prototype._getUieFromElem = function(elem)
	{
		return attr = elem.getAttribute ? elem.getAttribute("uie") : null;
	}
	
	ig_DayOrientedView.prototype._selectAppt = function(src)
	{
		if(src != null)
		{
			if(this._selectedAppt.elem != null)
				this._selectedAppt.elem.className = this._selectedAppt.oldClass;
			this._selectedAppt.elem = src;
			this._selectedAppt.key = src.getAttribute("ig_key");
			this._selectedAppt.oldClass = src.className;
			src.className = src.className.replace(this.getAllDayEventStyle(), "");
			src.className +=  " " +  this.getSelectedAppointmentStyle();
		}
		else
		{
			if(this._selectedAppt.elem != null)
			{
				this._selectedAppt.elem.className = this._selectedAppt.oldClass;
				this._selectedAppt.elem = null;
			}
		}
	}
	
}


function ig_getElementsByAttribute(baseElement, tagName, attributeName, attributeValue)
{
        var arrElements = (tagName == "*" && document.all)? document.all : baseElement.getElementsByTagName(tagName);
        var arrReturnElements = new Array();
        var attributeValue = (typeof attributeValue != "undefined")? new RegExp("(^|\\s)" + attributeValue + "(\\s|$)") : null;
        var oCurrent;
        var oAttribute;
        for(var i=0; i<arrElements.length; i++)
        {
            oCurrent = arrElements[i];
            oAttribute = oCurrent.getAttribute(attributeName);
            if(typeof oAttribute == "string" && oAttribute.length > 0)
            {
                if(typeof attributeValue == "undefined" || 
                        (attributeValue && attributeValue.test(oAttribute)))
                {
                    arrReturnElements.push(oCurrent);
                }
            }
        }
        return arrReturnElements;
}
