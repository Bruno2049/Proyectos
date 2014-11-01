/*
* ig_webweekview.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


 





// WebWeekView prototype and constructor
function ig_CreateWebWeekView(props)
{
	ig_DayOrientedView.prototype = new ig_WebControl(); 
	ig_DayOrientedView.prototype.constructor = ig_DayOrientedView;
	ig_DayOrientedView.prototype.base = ig_WebControl.prototype; 
	
    if(!ig_DayOrientedView.prototype.isPrototypeOf(ig_WebWeekView.prototype))
    {
        ig_WebWeekView.prototype = new ig_DayOrientedView(props);
        ig_WebWeekView.prototype.constructor = ig_WebWeekView;
        ig_WebWeekView.prototype.base=ig_DayOrientedView.prototype;
        
		ig_WebWeekView.prototype.init = function(props)
		{
			this.base.init.apply(this,[this._clientID]);			
			this._days = new Array();
			this._selectedAppt = new Object();
			this._initElem(this._element);
			

			this._element.control = this;
			this._initActiveDay();            
			this._setActiveDay = function(info, evnt, activeDay, id, smartCallback)
			{
				var me = this._event._object;
				if(!me || !activeDay || me._clientID == id)
					return;
				var day = activeDay.getFullYear() + "," + activeDay.getMonth() + "," + activeDay.getDate();
				var currentActiveDay = info.getActiveDay();
				var currentDay = currentActiveDay.getFullYear() + "," + currentActiveDay.getMonth() + "," + currentActiveDay.getDate(); 
				var pendingActiveDay = info._pendingActiveDay;
				if(pendingActiveDay != null)
					pendingActiveDay = pendingActiveDay.getFullYear() + "," + pendingActiveDay.getMonth() + "," + pendingActiveDay.getDate(); 
				else
					 pendingActiveDay = currentDay;
				if(me._days[day] && me._days[currentDay] && me._days[pendingActiveDay] )
					me._selectDay(day);
				else
				{
					evnt.needPostBack = true;
					if(smartCallback != null)
					{
						var serverContext = {operation:"ActiveDaySync", requestType:"html"};
						var clientContext = {operation:"ActiveDaySync", requestType:"html"};
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
						
						serverContext = {operation:"ActiveDaySync", requestType:"styles"};
						clientContext = {operation:"ActiveDaySync", requestType:"styles"};
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
					}
				}
			}
			this._navigate = function(info, evnt, years, months, days, newActiveDay, id, smartCallback)
			{
				var me = this._event._object;
				if(!me || !newActiveDay)
					return;
				var day = newActiveDay.getFullYear() + "," + newActiveDay.getMonth() + "," + newActiveDay.getDate();
				if(me._days[day])
					me._selectDay(day, true);
				else
				{
					if(smartCallback != null)
					{
						var serverContext = {operation:"Navigate", requestType:"html"};
						var clientContext = {operation:"Navigate", Days:days, Months:months, Years:years};
						if(me._clientID == id)
							clientContext.CausedNavigate = true;
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
						
						serverContext = {operation:"Navigate", requestType:"styles"};
						clientContext = {operation:"Navigate", requestType:"styles"};
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
					}
				}
			}
			
            
			this._setActiveResource = function(info, evnt, resKey, smartCallback)
			{
				var me = this._event._object;

				if (smartCallback != null)
				{
					var serverContext = { operation: "SetActiveResource", requestType: "html" };
					var clientContext = { operation: "SetActiveResource", requestType: "html", ResKey: resKey };
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

					serverContext = { operation: "SetActiveResource", requestType: "json" };
					clientContext = { operation: "SetActiveResource", requestType: "json", scrollPosition: me._divScroll.scrollTop };
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

					serverContext = { operation: "SetActiveResource", requestType: "styles" };
					clientContext = { operation: "SetActiveResource", requestType: "styles" };
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);
				}
			}
			
			this._appointmentModified = function(info, evnt, activity, id, smartCallback)
			{
				var me = this._event._object;
				
				if(smartCallback != null)
				{
					var serverContext = {operation:"ActivityUpdate", requestType:"html"};
					var clientContext = {operation:"ActivityUpdate"};
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
					
					serverContext = {operation:"ActivityUpdate", requestType:"styles"};
					clientContext = {operation:"ActivityUpdate", requestType:"styles"};
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
				}
			}
			this.getWebScheduleInfo().addEventListener("InternalSetActiveDay", this._setActiveDay, this, false);
			this.getWebScheduleInfo().addEventListener("InternalNavigate", this._navigate, this, false);
			this.getWebScheduleInfo().addEventListener("InternalActivityUpdate", this._appointmentModified, this, false);
			
			this.getWebScheduleInfo().addEventListener("InternalSetActiveResource", this._setActiveResource, this, true);
			
			


			this._isInitializing=false;
			this.fireEvent("initialize");
		}
		
		ig_WebWeekView.prototype._initActiveDay = function()
		{
			var i, day = this._activeDay;
			if(day && (day = this._days[day]) != null)
			{
				var classes = this.getActiveDayHeaderStyle().split(" ");
				var oldClass = day.headElem.className;
				for(i = 0; i < classes.length; i++)
					oldClass = oldClass.replace(classes[i], "");
				day.headOldClass = oldClass;
				try{
				if(day.headElem.style.backgroundImage!=null && day.headElem.style.backgroundImage!="")
				{
				    day.oldBimg = day.headElem.style.backgroundImage;
				    day.headElem.style.backgroundImage = "";
				}
				else day.oldBimg = null;
				}catch(x){;}
				classes = this.getActiveDayStyle().split(" ");
				oldClass = day.dayElem.parentNode.className;
				for(i = 0; i < classes.length; i++)
					oldClass = oldClass.replace(classes[i], "");
				day.dayOldClass = oldClass;				
			}
		}
		ig_WebWeekView.prototype._initElem = function(e)
		{	
			if(e == this._element)
				this._dayElements = new Array(); 
			var attr = e.getAttribute ? e.getAttribute("uie") : null;
			var i = (attr == "Day");
			if(i || attr == "DayHeader")
			{
				if(attr == "DayHeader")
					this._dayElements.push(e); 
				if(ig_shared.isEmpty(attr = e.getAttribute("date")))
					return;
				var d = this._days[attr];
				if(!d) d = this._days[attr] = new Object();
				if(i) d.dayElem = e;
				else d.headElem = e;
				if(e.id == this._clientID + "_ActiveDay")
					this._activeDay = attr;
			}
			var nodes = e.childNodes;
			if(nodes) for(i = 0; i < nodes.length; i++)
				this._initElem(nodes[i]);
		}
		
		ig_WebWeekView.prototype._getDayFromDayHeader = function(header)
		{
			var elem = header.parentNode; 
			while(elem.tagName != "TR")
				elem = elem.parentNode; 
			
			elem = elem.nextSibling; 
			while(elem.tagName != "TR")
				elem = elem.nextSibling; 
				
			elem = elem.childNodes[0]; 
			while(elem.tagName != "TD")
				elem = elem.nextSibling; 
			
			for(var i = 0; i < elem.childNodes.length; i++)
			{
				if(this._getUieFromElem(elem.childNodes[i]) == "Day")
					return elem.childNodes[i]; 
			}
			return null; 
		}
				
		ig_WebWeekView.prototype._getDayHeaderFromDay = function(day)
		{
			var parent = day.parentNode; 
			while(parent.tagName != "TR")
				parent = parent.parentNode; 
				
			parent = parent.previousSibling; 
			while(parent.tagName != "TR")
				parent = parent.previousSibling; 
			
			for(var i =0; i < parent.childNodes.length; i++)
			{
				if(this._getUieFromElem(parent.childNodes[i]) == "DayHeader")
					return parent.childNodes[i]; 
			}
			return null; 
		}
		
		ig_WebWeekView.prototype._getApptsFromDay = function(elem)
		{
			var appts = new Array(); 
			for(var i =0; i < elem.childNodes.length; i++)
			{	
				var attr = this._getUieFromElem(elem.childNodes[i]);
				if(attr == "Appt")
					appts.push(elem.childNodes[i]); 
			}
			return appts; 
		
		}
		ig_WebWeekView.prototype._getNextElem = function(index, dir)
		{
			var elem = null; 
			if(dir == 37)      // Left
			{	
				index--; 
				if(index < 0)
					index = this._dayElements.length -1; 
				elem = this._dayElements[index]; 
			}
			else if(dir == 38) // Up
			{
				var date = this._getDateFromString(this._dayElements[index].getAttribute("date")); 
				date.setHours(0,0,0,0);
				date.setDate(date.getDate() - 1); 
				elem = this._getDayHeaderFromDate(date); 
				if(elem == null)
				{	
					date.setDate(date.getDate() + 7); 
					elem = this._getDayHeaderFromDate(date); 
				}
			}
			else if(dir == 39) // Right
			{
				index++; 
				if(index == this._dayElements.length)
					index = 0; 
				elem = this._dayElements[index]; 
			}
			else if(dir == 40) // Down
			{
				var date = this._getDateFromString(this._dayElements[index].getAttribute("date")); 
				date.setHours(0,0,0,0);
				date.setDate(date.getDate() + 1); 
				elem = this._getDayHeaderFromDate(date); 
				if(elem == null)
				{	
					date.setDate(date.getDate() - 7); 
					elem = this._getDayHeaderFromDate(date); 
				}
			}
			return elem;
		}	
		
		ig_WebWeekView.prototype._getDayFromAppt = function(elem)
		{
			var parent = elem.parentNode; 
			var attr ="";
			do
			{
				if(this._getUieFromElem(parent) == "Day" )
					break;
				parent = parent.parentNode; 
			}while(parent != null); 
			return parent; 
		}
						
		ig_WebWeekView.prototype._processKeyDown = function(src, key, uie)
		{
			var elem = null;
			if(uie == "DayHeader")
				elem = this._dayHeaderKeyDown(src, key);
			else if(uie == "Appt")
				elem = this._apptKeyDown(src, key); 
			else	
				elem = this._getElemFromNonUIE(src,key); 			
			return elem; 
		}	
		
		ig_WebWeekView.prototype._onClick = function(src, evt)
		{
			if(this.fireEvent(evt.type, evt, src))
				return;
			var info = this.getWebScheduleInfo();
			src = ig_findElemWithAttr(src, "uie");
			var attr = (info && src) ? src.getAttribute("uie") : null;
			var i = (attr == "Next") ? 7 : ((attr == "Prev") ? -7 : 0);
			if(i != 0)
			{
				if(!this.fireEvent((i > 0) ? "navigatenext" : "navigateprevious", evt, i))
					info.navigate(0, 0, i, this._clientID);
				if(!ig_shared.IsIE)ig_cancelEvent(evt);
			}
			
			if(attr != null && attr.indexOf("RBUT") == 0)
			{
				if(!ig_shared.IsIE)ig_cancelEvent(evt);
				var resKey = attr.substring(4);
				
				//if(!this.fireEvent((i > 0) ? "navigatenext" : "navigateprevious", evt, i))
					this.getWebScheduleInfo().setActiveResource(resKey);
			}						
		}		
		ig_WebWeekView.prototype._onMouseup = function(src, evt)
		{
			this.fireEvent(evt.type, evt, src);
		}	
		
		ig_WebWeekView.prototype._onMousedown = function(src, evt)
		{
		    

		    if(this.getEnableKeyboardNavigation() || this.getSection508Compliant() )
			    this._element.focus(); 
			var scheduleInfo = this.getWebScheduleInfo();
			if(src.tagName == "IMG")
				src = src.parentNode;
			if(this.fireEvent(evt.type, evt, src))
				return;
			var uie = src.getAttribute("uie");
			if(uie == "Appt")
			{
				var day = null, e = src;
				while(e)
				{
					if(e.getAttribute && ig_shared.notEmpty(day = e.getAttribute("date")))
						break;
					e = e.parentNode;
				}
				if(!e || !day || !this._selectDay(day))
					return;
	            this._selectAppt(src); 
				if(this._selectedAppt != null)
				{
                    var appResKey = null;
		            (this._selectedAppt != null) ? appResKey = scheduleInfo.getActivities().getItemFromKey(this._selectedAppt.key).getResourceKey() : "";
		            if (this._selectedAppt != null
		                && appResKey.length > 0
                        && appResKey != scheduleInfo.getActiveResourceDataKey()) 
                    {
		                scheduleInfo.setActiveResource(appResKey);
		                ig_cancelEvent(evt);
		                return;
		            }				    
				}
			}
			this._selectDay(src.getAttribute("date"));
		}
		ig_WebWeekView.prototype._selectDay = function(dayID, setDay)
		{		
			var day = this._days[dayID], oldDay = this._activeDay;
			if(!day)
				return false;
			if(this._activeDay == dayID)
				return true;
			if(oldDay) oldDay = this._days[oldDay];
			var scheduleInfo = this.getWebScheduleInfo();
			var date = dayID.split(",");
			if(!setDay)
				setDay = scheduleInfo.setActiveDay(new Date(date[0], date[1], date[2]), false, this._clientID);
			if(!setDay)
				return false;
			if(oldDay != null)
			{
				oldDay.dayElem.parentNode.className = oldDay.dayOldClass;
				oldDay.headElem.className = oldDay.headOldClass;
				try{
                if(oldDay.oldBimg!=null)
				{
                    oldDay.headElem.style.backgroundImage = oldDay.oldBimg;
                    oldDay.oldBimg = null;
				}
				}catch(x0){;}
				this._selectAppt(null); 
			}
			this._activeDay = dayID;
			day.dayOldClass = day.dayElem.parentNode.className;
			day.headOldClass = day.headElem.className;
			day.headElem.className +=  " " + this.getActiveDayHeaderStyle();
			day.dayElem.parentNode.className += " " + this.getActiveDayStyle();
			try{
            if(day.headElem.style.backgroundImage!=null && day.headElem.style.backgroundImage!="")
            {
                day.oldBimg = day.headElem.style.backgroundImage;
                day.headElem.style.backgroundImage = "";
            }
		    else day.oldBimg = null;
			}catch(x1){;}
			if(this._selectedAppt.elem != null)
				this._selectedAppt.elem.className = this._selectedAppt.oldClass;
			return true;
		}
		ig_WebWeekView.prototype._onDblclick = function(src, evt)
		{
			if(!src || !src.getAttribute || this.fireEvent(evt.type, evt, src))
				return;
			var scheduleInfo = this.getWebScheduleInfo();
			if(this.getEnableAutoActivityDialog())
			{
				if(src.tagName == "IMG")
					src = src.parentNode;
				var uie = src.getAttribute("uie");
				if(uie == "Appt")
					this._showUpdateAppointmentDialog(src); 
				var date = (uie == "DayHeader" || uie == "Day") ? src.getAttribute("date") : null;
				if(!date)
					return;
				var dateTime = this._getDateFromString(date); 
				this._addNewAppointment(dateTime); 				
			}
		}		
		ig_WebWeekView.prototype.getSelectedActivity = function()
		{
			if(this._selectedAppt == null || this._selectedAppt.elem == null)
				return null;
			var activities = this.getWebScheduleInfo().getActivities();
			return activities.getItemFromKey(this._selectedAppt.key);
		}
        ig_WebWeekView.prototype.getCaptionHeaderVisible = function() {
           return this._props[2];
        }  
         
        ig_WebWeekView.prototype.getEnableAutoActivityDialog = function() {
           return this._props[3];
        }  

        ig_WebWeekView.prototype.getWebScheduleInfoID = function() {
           return this._props[4];
        }  
		ig_WebWeekView.prototype.getWebScheduleInfo = function()
		{
			return ig_getWebControlById(this._props[4]);
		}
        ig_WebWeekView.prototype.getWeekViewFrameStyle = function() {
           return this._props[5];
        }  
        ig_WebWeekView.prototype.getCaptionHeaderStyle = function() {
           return this._props[6];
        }
        ig_WebWeekView.prototype.getWeekViewDayAreaStyle = function() {
           return this._props[7];
        }
        ig_WebWeekView.prototype.getNavigationAnimation = function() {
           return this._props[8];
        }
        ig_WebWeekView.prototype.getSection508Compliant = function() {
           return this._props[9];
        }
        ig_WebWeekView.prototype.getEnableKeyboardNavigation = function() {
           return this._props[10];
        }
        ig_WebWeekView.prototype.getActiveDayHeaderStyle = function() {
           return this._props[11];
        }
        ig_WebWeekView.prototype.getActiveDayStyle = function() {
           return this._props[12];
        } 
        ig_WebWeekView.prototype.getAllDayEventStyle = function() {
           return this._props[13];
        } 
        ig_WebWeekView.prototype.getAppointmentStyle = function() {
           return this._props[14];
        } 
        ig_WebWeekView.prototype.getSelectedAppointmentStyle = function() {
           return this._props[15];           
        }     
        ig_WebWeekView.prototype.getDayHeaderStyle = function() {
           return this._props[16];
        }
        ig_WebWeekView.prototype.getDayStyle = function() {
           return this._props[17];           
        }
        ig_WebWeekView.prototype.getTodayHeaderStyle = function() {
           return this._props[18];
        }
        ig_WebWeekView.prototype.getTodayStyle = function() {
           return this._props[19];           
        }
        ig_WebWeekView.prototype.callbackRender = function(html, context)
        {
			var animate = (context.operation == "Navigate" && context.CausedNavigate && this.getNavigationAnimation() > 0);
			html = html.replace(/\^/g, "\"");
	        var elem = this.getElement();
	        if(animate)
				var cloneElem = elem.cloneNode(true);
	        elem.innerHTML = html;
	        ig_dispose(this._days);
	        this._days = new Array();
	        this._initElem(elem);
	        
	        this._initActiveDay();
	        	
			if(animate)
			{						
				cloneElem.id += "_clone";
				
				if(this.viewport == null)
				{	
					this.viewport = new ig_viewport();
					this.viewport.createViewport(elem, ViewportOrientationEnum.Horizontal);
					this.viewport.transferPositionToDiv(elem,cloneElem);
					this.viewport.animate.increment = 50;
					this.viewport.animate.onEnd = this._afterSlide;
					this.viewport.animate._elemOrigWidth = elem.style.width; 
				}
				this.viewport.div.style.width = this.viewport.div.offsetWidth + "px";
				var direction = AnimationDirectionEnum.Left;
				if(context.Days > 0 || ((context.Years * 12) + context.Months) > 0)
					direction = AnimationDirectionEnum.Right;
				
				this.viewport.animate._newElem = elem;
				elem.style.width = this.viewport.div.style.width;
				this.viewport.animate._oldElem = cloneElem;
				cloneElem.style.width = elem.style.width;
					
				this.viewport.scroll(cloneElem, elem, direction, this.getNavigationAnimation());
				this.viewport.animate._viewDiv = this.viewport.div; 
			}
        }
        
        ig_WebWeekView.prototype._afterSlide = function()
        {
			this._oldElem.parentNode.removeChild(this._oldElem);
			this.getContainer().scrollLeft = 0; 
			this._newElem.style.width = "100%"; 
			this._viewDiv.style.width = this._elemOrigWidth; 
        }
	}
	return new ig_WebWeekView(props);
}

function ig_WebWeekView(props)
{
	if(arguments.length != 0)
		this.init(props);
}

// public: get object from ClientID or UniqueID
function ig_getWebWeekViewById(id)
{
	return ig_getWebControlById(id);
}
