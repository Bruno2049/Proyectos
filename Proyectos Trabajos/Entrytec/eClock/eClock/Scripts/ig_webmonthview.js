/*
* ig_webmonthview.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


 




// WebMonthView prototype and constructor

function ig_CreateWebMonthView(props)
{
	ig_DayOrientedView.prototype = new ig_WebControl(); 
	ig_DayOrientedView.prototype.constructor = ig_DayOrientedView;
	ig_DayOrientedView.prototype.base = ig_WebControl.prototype; 
	
    if(!ig_DayOrientedView.prototype.isPrototypeOf(ig_WebMonthView.prototype))
    {
        ig_WebMonthView.prototype = new ig_DayOrientedView(props);
        ig_WebMonthView.prototype.constructor = ig_WebMonthView;
        ig_WebMonthView.prototype.base=ig_DayOrientedView.prototype;

        ig_WebMonthView.prototype.init = function(props)
        {
        	this._isInitializing = true;
        	this.base.init.apply(this, [this._clientID]);
        	

        	
        	this._element.control = this;
        	var currentMonth = this.getCurrentMonth();
        	this.updateControlState("CurrentMonth", currentMonth.getFullYear() + ":" + (currentMonth.getMonth() + 1) + ":" + currentMonth.getDate());
        	this._setActiveDay = function(info, evnt, activeDay, id, smartCallback)
        	{
        		var me = this._event._object;
        		if (!me || !activeDay)
        			return;

        		if (me._clientID != id)
        		{
        			var dayHeader = me._daysOfMonth[activeDay.getFullYear() + "," + activeDay.getMonth() + "," + activeDay.getDate()];
        			if (dayHeader != null)
        			{
        				var day = me._getDayFromHeader(dayHeader);
        				me._selectDay(dayHeader, day);
        			}
        			else
        			{
        				var fvd = me.getFirstVisibleDay();
        				var lvd = new Date();
        				lvd.setTime(fvd.getTime());
        				lvd.setDate(fvd.getDate() + 41);
        				var currentMonth = new Date();
        				currentMonth.setTime(me.getCurrentMonth().getTime());
        				var lastCurrentMonth = currentMonth.getMonth();
        				currentMonth.setDate(1);

        				if (activeDay.getTime() < fvd.getTime())
        				{
        					currentMonth.setMonth(currentMonth.getMonth() - (currentMonth.getMonth() - activeDay.getMonth()));
        					currentMonth.setFullYear(activeDay.getFullYear());
        				}
        				else if (activeDay.getTime() > lvd.getTime())
        				{
        					currentMonth.setMonth(currentMonth.getMonth() + (activeDay.getMonth() - currentMonth.getMonth()));
        					currentMonth.setFullYear(activeDay.getFullYear());
        				}

        				if (currentMonth.getMonth() != lastCurrentMonth)
        					me.getWebScheduleInfo().setPreviousMonth(me.getCurrentMonth());

        				me.setCurrentMonth(currentMonth);

        				evnt.needPostBack = true;
        				if (smartCallback != null)
        				{
        					var value = me.getCurrentMonth();
        					var date = value.getFullYear() + ":" + (value.getMonth() + 1) + ":" + value.getDate();
        					var serverContext = { operation: "ActiveDaySync", requestType: "html", CurrentMonth: date };
        					var clientContext = { operation: "ActiveDaySync", requestType: "html" };
        					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        					serverContext = { operation: "ActiveDaySync", requestType: "json" };
        					clientContext = { operation: "ActiveDaySync", requestType: "json" };
        					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        					serverContext = { operation: "ActiveDaySync", requestType: "styles" };
        					clientContext = { operation: "ActiveDaySync", requestType: "styles" };
        					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
        				}
        			}
        		}
        	}

        	this._navigate = function(info, evnt, years, months, days, newActiveDay, id, smartCallback)
        	{
        		var me = this._event._object;
        		if (me._clientID != id)
        		{
        			var dayHeader = me._daysOfMonth[newActiveDay.getFullYear() + "," + newActiveDay.getMonth() + "," + newActiveDay.getDate()];
        			if (dayHeader != null)
        			{
        				var day = me._getDayFromHeader(dayHeader);
        				me._selectDay(dayHeader, day, true);
        			}
        			else
        			{
        				var info = me.getWebScheduleInfo();
        				var prevActiveDay = info.getActiveDay();
        				var newActiveDay = new Date();
        				newActiveDay.setTime(prevActiveDay.getTime());
        				if (days == 0)
        					newActiveDay.setDate(1);

        				newActiveDay.setFullYear(newActiveDay.getFullYear() + years, newActiveDay.getMonth() + months, newActiveDay.getDate() + days);
        				if (days == 0)
        				{
        					var month = newActiveDay.getMonth();
        					var date = prevActiveDay.getDate();
        					newActiveDay.setDate(date);
        					while (newActiveDay.getMonth() != month)
        					{
        						newActiveDay.setMonth(month);
        						newActiveDay.setDate(date);
        						date--;
        					}
        				}

        				var fvd = me.getFirstVisibleDay();
        				var lvd = new Date();
        				lvd.setTime(fvd.getTime());
        				lvd.setDate(fvd.getDate() + 41);
        				var currentMonth = new Date();
        				currentMonth.setTime(me.getCurrentMonth().getTime());
        				var lastCurrentMonth = currentMonth.getMonth();
        				currentMonth.setDate(1);

        				if (newActiveDay.getTime() < fvd.getTime())
        				{
        					currentMonth.setMonth(currentMonth.getMonth() - (currentMonth.getMonth() - newActiveDay.getMonth()));
        					currentMonth.setFullYear(newActiveDay.getFullYear());
        				}
        				else if (newActiveDay.getTime() > lvd.getTime())
        				{
        					currentMonth.setMonth(currentMonth.getMonth() + (newActiveDay.getMonth() - currentMonth.getMonth()));
        					currentMonth.setFullYear(newActiveDay.getFullYear());
        				}
        				var prevMonthSet = false;
        				if (currentMonth.getMonth() != lastCurrentMonth)
        				{
        					info.setPreviousMonth(me.getCurrentMonth());
        					prevMonthSet = true;
        				}

        				me.setCurrentMonth(currentMonth);

        				if (smartCallback != null)
        				{
        					if (prevMonthSet)
        					{
        						var previousMonth = info.getPreviousMonth();
        						var prevMonthDate = previousMonth.getFullYear() + ":" + (previousMonth.getMonth() + 1) + ":" + previousMonth.getDate();
        						smartCallback._registeredControls[0].serverContext.PreviousMonth = prevMonthDate;
        					}

        					var value = me.getCurrentMonth();
        					var date = value.getFullYear() + ":" + (value.getMonth() + 1) + ":" + value.getDate();
        					var serverContext = { operation: "Navigate", requestType: "html", CurrentMonth: date };
        					var clientContext = { operation: "Navigate", requestType: "html", Days: days, Months: months, Years: years };
        					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        					serverContext = { operation: "Navigate", requestType: "json" };
        					clientContext = { operation: "Navigate", requestType: "json" };
        					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        					serverContext = { operation: "Navigate", requestType: "styles" };
        					clientContext = { operation: "Navigate", requestType: "styles" };
        					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
        				}
        			}
        		}
        		else
        		{
        			if (smartCallback != null)
        			{
        				var value = me.getCurrentMonth();
        				var date = value.getFullYear() + ":" + (value.getMonth() + 1) + ":" + value.getDate();
        				var serverContext = { operation: "Navigate", requestType: "html", CurrentMonth: date };
        				var clientContext = { operation: "Navigate", requestType: "html", Days: days, Months: months, Years: years, CausedNavigate: true };
        				smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        				serverContext = { operation: "Navigate", requestType: "json" };
        				clientContext = { operation: "Navigate", requestType: "json" };
        				smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        				serverContext = { operation: "Navigate", requestType: "styles" };
        				clientContext = { operation: "Navigate", requestType: "styles" };
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

        		if (smartCallback != null)
        		{
        			var serverContext = { operation: "ActivityUpdate", requestType: "html" };
        			var clientContext = { operation: "ActivityUpdate", requestType: "html" };
        			smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        			serverContext = { operation: "ActivityUpdate", requestType: "json" };
        			clientContext = { operation: "ActivityUpdate", requestType: "json" };
        			smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)

        			serverContext = { operation: "ActivityUpdate", requestType: "styles" };
        			clientContext = { operation: "ActivityUpdate", requestType: "styles" };
        			smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me)
        		}
        	}
        	this.getWebScheduleInfo().addEventListener("InternalActivityUpdate", this._appointmentModified, this, false);
        	this.getWebScheduleInfo().addEventListener("InternalSetActiveDay", this._setActiveDay, this, false);
        	this.getWebScheduleInfo().addEventListener("InternalNavigate", this._navigate, this, false);
			
			this.getWebScheduleInfo().addEventListener("InternalSetActiveResource", this._setActiveResource, this, true);
        	
        	this._initActiveDay();

        	if (this.getWebScheduleInfo().getActiveDayClientSynchronization() == 0 || this.getWebScheduleInfo().getEnableSmartCallbacks())
        		this._loadDaysOfMonth();
			
			


        	this._isInitializing = false;
        	this.fireEvent("initialize");
        }
		ig_WebMonthView.prototype._getDayFromHeader = function(header)
		{
			if(this.getDocType() == "2")
			{
			    var allDayEventBannerIndex = 0;
			    
			    var index = header.cellIndex;
				if(this.getWeekNumbersVisible() && header.getAttribute("uie") == "DAYHEADER")
							index--;
			    
			    var tds = header.parentNode.nextSibling.childNodes;
				for (i = 0; i <= tds.length; i++)
				//for(i=0; i <= index; i++)
				{
				    if (tds[i] && tds[i].colSpan && tds[i].colSpan >1)
				        allDayEventBannerIndex += tds[i].colSpan-1;
				}
				
			//	index -= allDayEventBannerIndex;
				
				var cells = header.parentNode.nextSibling.cells;
				
				if (index >= cells.length)
					index = cells.length - 1;
				
				return cells[index];
			}
			else
			{
				return header.parentNode.parentNode.parentNode;
			}
		}
		
		ig_WebMonthView.prototype._getDayFromDayHeader = function(header)
		{
			if(this.getDocType() == "2")
				return this._getDayFromHeader(header); 
			else
			{
				var day = header.parentNode.nextSibling; 
				while(day.tagName != "TR")
					day = day.nextSibling; 
				
				day = day.childNodes[0];
				while(day.tagName != "TD")
					day = day.nextSibling; 
				for(var i = 0; i< day.childNodes.length; i++)
				{
					if(this._getUieFromElem(day.childNodes[i]) == "DAY")
						return day.childNodes[i]; 
				}
				return null; 
			}
		}
		
		ig_WebMonthView.prototype._getApptsFromDay = function(day)
		{
			var appts = null; 
			if(this.getDocType() == "2")
			{
				for(var i =0; i < day.childNodes.length; i++)
				{
					if(this._getUieFromElem(day.childNodes[i]) == "APPTAREA")
					{
						for(var j = 0; j < day.childNodes[i].childNodes.length; j++)
						{
							if(this._getUieFromElem(day.childNodes[i].childNodes[j]) == "Appt")
							{		
								if(appts == null)
									appts = new Array(); 
								appts.push(day.childNodes[i].childNodes[j]); 
							}
						
						}
					}
				}
			}
			else
			{
				for(var i = 0; i < day.childNodes.length; i++)
				{
					if(this._getUieFromElem(day.childNodes[i]) == "Appt")
					{		
						if(appts == null)
							appts = new Array(); 
						appts.push(day.childNodes[i]); 
					}
				}
			}
			
			// A.T. check for multi-day event banners
			if (day.parentNode && day.parentNode.nodeName == 'TR' && day.parentNode.nextSibling && !day.parentNode.nextSibling.getAttribute("uie"))
			{
				// get other appointments
				var otherAppts = this._getStandardApptsFromDay(day.parentNode.nextSibling.cells[day.cellIndex]);
				if (otherAppts)
				{
					for(var i=0;i< otherAppts.length; i++)
					{
						appts.push(otherAppts[i]);
					}
				}
			} else if (day.parentNode && day.parentNode.nodeName == 'TR' && day.parentNode.previousSibling && !day.parentNode.previousSibling.getAttribute("uie"))
			{
				// get other appointments from PREVIOUS TR 
				var otherAppts = this._getStandardApptsFromDay(day.parentNode.previousSibling.cells[day.cellIndex]);
				if (otherAppts)
				{
					for(var i=0;i< otherAppts.length; i++)
					{
						appts.push(otherAppts[i]);
					}
				}
			}
			
			return appts; 
		}
		
		ig_WebMonthView.prototype._getStandardApptsFromDay = function(day)
		{
			var appts = null; 
			if(this.getDocType() == "2")
			{
				for(var i =0; i < day.childNodes.length; i++)
				{
					if(this._getUieFromElem(day.childNodes[i]) == "APPTAREA")
					{
						for(var j = 0; j < day.childNodes[i].childNodes.length; j++)
						{
							if(this._getUieFromElem(day.childNodes[i].childNodes[j]) == "Appt")
							{		
								if(appts == null)
									appts = new Array(); 
								appts.push(day.childNodes[i].childNodes[j]); 
							}
						
						}
					}
				}
			}
			else
			{
				for(var i = 0; i < day.childNodes.length; i++)
				{
					if(this._getUieFromElem(day.childNodes[i]) == "Appt")
					{		
						if(appts == null)
							appts = new Array(); 
						appts.push(day.childNodes[i]); 
					}
				}
			}
			
			return appts;
			
		}
		
		ig_WebMonthView.prototype._getDayFromAppt = function(elem)
		{
			var parent = elem.parentNode; 
			var attr ="";
			do
			{
				if(this._getUieFromElem(parent) == "DAY" )
					break;
				parent = parent.parentNode; 
			}while(parent != null); 
			
			return parent; 
		}
		
		ig_WebMonthView.prototype._getDayHeaderFromDay = function(day)
		{
			if(this.getDocType() == "2")
				return this._getHeaderFromDay(day); 
			else
			{
				var header = day.parentNode.parentNode.previousSibling; 
				while(header.tagName != "TR")
					header = header.previousSibling; 
				
				for(var i = 0;i < header.childNodes.length; i++)
				{
					if(this._getUieFromElem(header.childNodes[i]) == "DAYHEADER")
						return header.childNodes[i]; 
				}
				return null; 	
			}
		}	
		
		ig_WebMonthView.prototype._getHeaderFromDay = function(day)
		{
			if(this.getDocType() == "2")
			{
			    var index = day.cellIndex;
			    var allDayEventBannerIndex = 0;

				if(this.getWeekNumbersVisible() && day.getAttribute("uie") == "DAY")
				    index++;


				var tds = day.parentNode.childNodes;
				for (i = 0; i <= tds.length; i++)
				{
				    if (tds[i] && tds[i].colSpan && tds[i].colSpan >1)
				        allDayEventBannerIndex += tds[i].colSpan-1;
				}
				
				
				// A.T. 31.07.2009 Fix for bug 20142
				var headerRow = day.parentNode.previousSibling;
				
				while (headerRow!=null && headerRow.getAttribute("uie")!= "DAYHEADERS")
				{
				    headerRow = headerRow.previousSibling;
				}

                index += allDayEventBannerIndex - (day.colSpan-1);
				return headerRow.cells[index];
			}
			else
			{
				return day.parentNode.parentNode.parentNode;
			}
		}
		ig_WebMonthView.prototype._initActiveDay = function() 
		{
			this._selectedAppt = new Object();
			this._selectedHeader = new Object();
			this._selectedDay = new Object();
			var activeDay = document.getElementById(this._clientID + "_ActiveDay");
			if(activeDay != null)
			{
				this._selectedHeader.elem = activeDay;
				var classes = this.getActiveDayHeaderStyle().split(" ");
				var oldClass = activeDay.className;
				for(var i = 0; i < classes.length; i++)
					oldClass = oldClass.replace(classes[i], "");
				this._selectedHeader.oldClass = oldClass;
				if(this._selectedHeader.elem.style.backgroundImage!=null && this._selectedHeader.elem.style.backgroundImage!="")
				{
				    this._selectedHeader.oldBimg = this._selectedHeader.elem.style.backgroundImage;
				    this._selectedHeader.elem.style.backgroundImage = "";
				}
				else this._selectedHeader.oldBimg = null;
									
				this._selectedDay.elem = this._getDayFromHeader(activeDay)				
				classes = this.getActiveDayStyle().split(" ");
				// A.T. Fix for bug #30737 - typo - undefned in ig_webmonthview js file
				if(typeof(this._selectedDay.elem) != "undefined" && this._selectedDay.elem != null)
				{
				    oldClass = this._selectedDay.elem.className;
				    for(var i = 0; i < classes.length; i++)
    					oldClass = oldClass.replace(classes[i], "");
				    this._selectedDay.oldClass =  oldClass;
				}
			} 
		}
		ig_WebMonthView.prototype._loadDaysOfMonth = function() 
		{
			this._dayElements = new Array(); 
			this._daysOfMonth = new Object();
			if(this.getDocType() == "1")
			{				
				var week = document.getElementById(this._clientID + "_FirstWeek");
				for(var i = 0; i < 6; i++)
				{
					var weekHeader = this._getFirstChild(week, "TD");
					weekHeader = this._getFirstChild(weekHeader, "TABLE");
					weekHeader = this._getFirstChild(weekHeader, "TBODY");
					weekHeader = this._getFirstChild(weekHeader, "TR");
					
					weekHeader = this._getFirstChild(weekHeader, "TD");
					if(this.getWeekNumbersVisible())
						weekHeader = weekHeader.nextSibling;
					var counter = 7; 
					for(var j = 0; j < counter; j++)
					{
						var day = this._getFirstChild(weekHeader, "TABLE");
						day = this._getFirstChild(day, "TBODY");
						day = this._getFirstChild(day, "TR");
						day = this._getFirstChild(day, "TD");
						var date = day.getAttribute("date");
						if(date == null)
						{
							counter = 6;
							for(var k = 0; k < 2; k++)
							{
								var weekend = this._getFirstChild(day, "TABLE");		
								weekend = this._getFirstChild(weekend, "TBODY");
								weekend = this._getFirstChild(weekend, "TR");
								weekend = this._getFirstChild(weekend, "TD");
								weekend = this._getFirstChild(weekend, "TABLE");
								weekend = this._getFirstChild(weekend, "TBODY");
								weekend = this._getFirstChild(weekend, "TR");
								weekend = this._getFirstChild(weekend, "TD");
								date = weekend.getAttribute("date");
								this._daysOfMonth[date] =  weekend;
								this._dayElements.push(weekend); 
								if(k == 0)
								{
									day = day.parentNode.nextSibling;
									day = this._getFirstChild(day, "TD");
								}
							}
						}
						else
						{
							this._daysOfMonth[date] = day;
							this._dayElements.push(day); 
						}
						
						weekHeader = weekHeader.nextSibling;
					}
					if(i < 5)
					{
						week = week.nextSibling;
						while(week.tagName != "TR")
							week = week.nextSibling;
					}
				}
				
			}
			else
			{
				var week = document.getElementById(this._clientID + "_FirstWeek");
				while(week.attributes != null && week.getAttribute("uie") == "DAYHEADERS")
				{
					for(var i = 0; i < week.cells.length; i++)
					{
						if(week.cells[i].getAttribute("uie") == "DAYHEADER" || week.cells[i].getAttribute("uie") == "COMPDAYHEADER" )
						{
							var date = week.cells[i].getAttribute("date");
							this._daysOfMonth[date] =  week.cells[i];
							this._dayElements.push(week.cells[i]); 
						}
					}
					do
					{
						week = week.nextSibling;
					}
					while((week.attributes == null || week.getAttribute("uie") != "DAYHEADERS") && week.nextSibling != null);
						
				}
			}
		}
				
		ig_WebMonthView.prototype._getNextElem = function(index, dir)
		{
			var elem = null; 
			var date = this._getDateFromString(this._dayElements[index].getAttribute("date")); 
			date.setHours(0,0,0,0);
			var firstAttempt = 0; 
			var lastAttempt = 0; 
			if(dir == 37)      // Left
			{	
				firstAttempt = -1; 
				lastAttempt = 42; 				
			}
			else if(dir == 38) // Up
			{
				if(date.getDay() == 0)
					firstAttempt = -1; 
				else if(date.getDay() ==  6)
					firstAttempt = -6;
				else
					firstAttempt = -7; 
				lastAttempt = 42; 				
			}
			else if(dir == 39) // Right
			{
				firstAttempt = 1; 
				lastAttempt = -42; 				
			}
			else if(dir == 40) // Down
			{
				if(date.getDay() == 0)
					firstAttempt = 6; 
				else if(date.getDay() ==  6)
					firstAttempt = 1;
				else
					firstAttempt = 7; 
				lastAttempt = -42;				
			}
			date.setDate(date.getDate() + firstAttempt); 
			elem = this._getDayHeaderFromDate(date); 
			if(elem == null)
			{	
				date.setDate(date.getDate() + lastAttempt); 
				elem = this._getDayHeaderFromDate(date); 
			}
			return elem;
		}
		
		ig_WebMonthView.prototype._processKeyDown = function(src, key, uie)
		{
			var elem = null; 
			if(uie == "DAYHEADER" || uie == "COMPDAYHEADER")
				elem = this._dayHeaderKeyDown(src, key); 
			else	
				elem = this._getElemFromNonUIE(src,key); 
			return elem;
		}
		
		ig_WebMonthView.prototype._onMouseup = function(src, evt) 
		{
			this.fireEvent(evt.type, evt, src);
		}

		ig_WebMonthView.prototype._onClick = function(src, evt) 
		{
			if(this.fireEvent(evt.type, evt, src))
				return;
			var info = this.getWebScheduleInfo();
			src = ig_findElemWithAttr(src, "uie");
			var attr = (info && src) ? src.getAttribute("uie") : null;
			var incrementYear = false;
            
			if(attr != null && attr.indexOf("RBUT") == 0)
			{
				if(!ig_shared.IsIE)ig_cancelEvent(evt);
				var resKey = attr.substring(4);
				
				//if(!this.fireEvent((i > 0) ? "navigatenext" : "navigateprevious", evt, i))
					this.getWebScheduleInfo().setActiveResource(resKey);
			}			
			if(attr == "NEXT" || attr == "PREV")
			{
				if(!this.fireEvent((attr == "NEXT") ? "navigatenext" : "navigateprevious", evt, (attr=="NEXT") ? 1: -1))	
				{
					var fvd = new Date();
					var activeDay = info.getActiveDay();
					fvd = this.getFirstVisibleDay();
					var fvdDayofWeek = fvd.getDay();
					var activeDayofWeek = activeDay.getDay();
					var newMonth;
								
					if(attr == "NEXT")
						newMonth = fvd.getMonth() + 2;
					else if(attr == "PREV")
						newMonth = fvd.getMonth();
						
					if(newMonth > 11)
					{
						newMonth = newMonth - 12;
						incrementYear = true;
					}
						
					//Calculate the First Visible Day for the Next Month
					var newFvd = new Date();
					newFvd.setTime(fvd.getTime());
					newFvd.setDate(1);
					newFvd.setMonth(newMonth);
					if(incrementYear)
						newFvd.setFullYear(newFvd.getFullYear() + 1);
					newFvd.setDate(newFvd.getDate()-10);
					while(newFvd.getDay() != fvdDayofWeek)
						newFvd.setDate(newFvd.getDate() + 1);
					
					// Find the Amount of days difference between last Month's FVD and ActiveDay
					var daysToAdd = (activeDay.getTime() - fvd.getTime())/1000/60/60/24;
					
					// Calculate the newActiveDay
					var newActiveDay = new Date();
					newActiveDay.setTime(newFvd);
					newActiveDay.setDate(newActiveDay.getDate() + daysToAdd);
					
					var daysToMove = (newActiveDay.getTime() - activeDay.getTime())/1000/60/60/24;
					daysToMove = parseInt(daysToMove.toString());
					
					// Test the newActiveDay and Make sure that its 
					var testDate = new Date();
					testDate.setTime(activeDay.getTime());
					testDate.setDate(testDate.getDate() + daysToMove);
					
					if(testDate.getDay() > activeDayofWeek)
					{
						if(this.getCompressedDayStyle() == 0 && activeDayofWeek == 0)
							daysToMove++;
						else
							daysToMove--;
					}
					else if(testDate.getDay() < activeDayofWeek || (activeDayofWeek == 0 && testDate.getDay() != activeDayofWeek))
						daysToMove++;
					
					var currentMonth = new Date();
					currentMonth.setTime(this.getCurrentMonth().getTime());
					currentMonth.setDate(1);
					var lastCurrentMonth = currentMonth.getMonth();
					var currentYear = currentMonth.getFullYear();
					currentMonth.setMonth(newMonth);
					if(currentYear < currentMonth.getFullYear())
						currentMonth.setFullYear(currentMonth.getFullYear() - 1);
					else if(currentYear > currentMonth.getFullYear())
						currentMonth.setFullYear(currentMonth.getFullYear() + 1);
						
					if(newMonth == 11 && lastCurrentMonth == 0)
						currentMonth.setFullYear(currentMonth.getFullYear() - 1);
					else if(newMonth == 0 && lastCurrentMonth == 11 )
						currentMonth.setFullYear(currentMonth.getFullYear() + 1);
					
					if(currentMonth.getMonth() != lastCurrentMonth)
						info.setPreviousMonth(this.getCurrentMonth());
					
					this.setCurrentMonth(currentMonth);
					info.navigate(0,0,daysToMove, this._clientID);
					if(!ig_shared.IsIE)ig_cancelEvent(evt);
				}
			}
		}

		ig_WebMonthView.prototype._onMousedown = function(src, evt) 
		{
			if(this.fireEvent(evt.type, evt, src))
				return;
			var scheduleInfo = this.getWebScheduleInfo();
			if(!scheduleInfo) return;
			if(src.tagName == "IMG")
				src = src.parentNode;
			var uie = src.getAttribute("uie");	
			if(uie == "Appt")
			{	
				var srcheader, srcday;
				if(this.getDocType() == "1")
				{
					srcheader = src.parentNode.parentNode.parentNode.previousSibling;
					srcheader = this._getFirstChild(srcheader);
					if(srcheader.getAttribute("uie") != "DAYHEADER" )
						return;
					srcday = srcheader.parentNode.parentNode.parentNode;
				}
				else
				{
					srcday = src.parentNode;
					while(srcday.getAttribute("uie") != "DAY" && srcday.getAttribute("uie") != "COMPDAY" )
						srcday = srcday.parentNode;
					srcheader = this._getHeaderFromDay(srcday);
				}
				
				this._selectDay(srcheader, srcday);		
				
				if(srcheader == this._selectedHeader.elem)
				{
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
			}	
						
			if(uie == "DAYHEADER" || uie == "COMPDAYHEADER")
			{
				var srcday =  this._getDayFromHeader(src); 
				this._selectDay(src, srcday);
			}
			if(uie == "DAY" || uie == "COMPDAY")
			{
				if(this.getDocType() == "1")
				{
					var srcDay = src.parentNode.parentNode.parentNode.parentNode;
					var srcHeader = this._getFirstChild(srcDay).childNodes[0];
					srcHeader = this._getFirstChild(srcHeader);
					if(srcHeader.getAttribute("uie") != "DAYHEADER")
						return;
					this._selectDay(srcHeader, srcDay);	
				}
				else
				{
					var srcHeader = this._getHeaderFromDay(src);
					this._selectDay(srcHeader, src);
				}
				
			}
			if(uie == "APPTAREA")
			{
				var srcDay = src.parentNode;
				while(srcDay.getAttribute("uie") != "DAY" && srcDay.getAttribute("uie") != "COMPDAY" )
					srcDay = srcDay.parentNode;
				var srcHeader = this._getHeaderFromDay(srcDay);
				this._selectDay(srcHeader, srcDay);
			}
		}
		ig_WebMonthView.prototype._selectDay = function(srcHeader, srcDay, setDay)
		{
		    if (srcDay==null)
		        return;
		    
			var scheduleInfo = this.getWebScheduleInfo();
			if(!scheduleInfo) return;
			if(this._selectedHeader.elem != srcHeader)
			{
				var date = srcHeader.getAttribute("date");
				if(ig_shared.isEmpty(date)) return;
				date = date.split(",");
				var dateTime = new Date();
				dateTime.setFullYear(date[0], date[1], date[2]);
				dateTime.setHours(0,0,0);
				if(!setDay)
					setDay = scheduleInfo.setActiveDay(dateTime, false, this._clientID);	
			
				if(setDay)
				{
					if(this._selectedHeader.elem != null)
					{
						this._selectedHeader.elem.className = this._selectedHeader.oldClass;
						if(this._selectedHeader.oldBimg!=null)
						{
                            this._selectedHeader.elem.style.backgroundImage = this._selectedHeader.oldBimg;
                            this._selectedHeader.oldBimg = null;
						}
						if(this._selectedHeader.elem != srcHeader && this._selectedAppt.elem != null)
						{
							this._selectAppt(null)								
						}
					}
					this._selectedHeader.elem = srcHeader;
					this._selectedHeader.oldClass = srcHeader.className;
					if(srcHeader.style.backgroundImage!=null && srcHeader.style.backgroundImage!="")
					{
					    this._selectedHeader.oldBimg = srcHeader.style.backgroundImage;
					    srcHeader.style.backgroundImage = "";
					}
					else this._selectedHeader.oldBimg = null;
					srcHeader.className +=  " " + this.getActiveDayHeaderStyle();	
					
					if(this._selectedDay.elem != null)
						this._selectedDay.elem.className = this._selectedDay.oldClass;
					this._selectedDay.elem = srcDay;
					this._selectedDay.oldClass = srcDay.className;
					srcDay.className += " " + this.getActiveDayStyle();		
				}
			}
			if(this._selectedAppt.elem != null)
			{
				this._selectedAppt.elem.className = this._selectedAppt.oldClass;
			}
		}
		ig_WebMonthView.prototype._onDblclick = function(src, evt)
		{
			if(this.fireEvent(evt.type, evt, src))
				return;
			var scheduleInfo = this.getWebScheduleInfo();
			if(!scheduleInfo) return;
			if(this.getEnableAutoActivityDialog())
			{
				if(src.tagName == "IMG")
					src = src.parentNode;
				var uie = src.getAttribute("uie");	
				if(uie == "APPTAREA")
				{
					src = src.parentNode;
					uie = src.getAttribute("uie");
				}
				if(uie == "Appt")
					this._showUpdateAppointmentDialog(src)
				else if(uie == "DAYHEADER" || uie == "COMPDAYHEADER")
				{
					this._openApptDialog(src);
				}
				else if(uie == "DAY" || uie=="COMPDAY")
				{
					var srcHeader = null;
					if(this.getDocType() == "1")
						 srcHeader = this._getFirstChild(src.parentNode.parentNode.previousSibling);
					else
						 srcHeader = this._getHeaderFromDay(src);
					this._openApptDialog(srcHeader);	
				}
			}
		}
		ig_WebMonthView.prototype._openApptDialog = function(srcHeader)
		{
			var scheduleInfo = this.getWebScheduleInfo();
			var date = srcHeader.getAttribute("date");
			if(ig_shared.isEmpty(date)) return;
			date = date.split(",");
			var dateTime = new Date();
			dateTime.setFullYear(date[0], date[1], date[2]);
			this._addNewAppointment(dateTime); 
		}
        ig_WebMonthView.prototype.getSelectedActivity = function()
		{
			if(this._selectedAppt == null || this._selectedAppt.elem == null)
				return null;
			else
			{
				var activities = this.getWebScheduleInfo().getActivities();
				return activities.getItemFromKey(this._selectedAppt.key);
			}
		}
        ig_WebMonthView.prototype.getCaptionHeaderVisible = function() {
           return this._props[5];
        }  
        ig_WebMonthView.prototype.getEnableAutoActivityDialog = function() {
           return this._props[6];
        } 
        ig_WebMonthView.prototype.getWebScheduleInfoID = function() {
           return this._props[7];
        }
		ig_WebMonthView.prototype.getWebScheduleInfo = function()
		{
			return ig_getWebControlById(this._props[7]);
		}
		ig_WebMonthView.prototype.getFirstVisibleDay = function() {
           return this._props[8];
        }
        ig_WebMonthView.prototype.getCurrentMonth = function() {
           return this._props[9];
        }
		ig_WebMonthView.prototype.setCurrentMonth = function(value)
		{
			this._props[9] = value; 
			var date =  value.getFullYear() + ":" + (value.getMonth() + 1) + ":" + value.getDate();
			this.updateControlState("CurrentMonth", date);
		}
		ig_WebMonthView.prototype.getWeekNumbersVisible = function() {
           return this._props[10];
        }   
        ig_WebMonthView.prototype.getMonthDayOfWeekHeaderStyle = function() {
           return this._props[11];
        }   
        ig_WebMonthView.prototype.getCaptionHeaderStyle = function() {
           return this._props[12];
        }   
        ig_WebMonthView.prototype.getMonthStyle = function() {
           return this._props[13];
        }   
        ig_WebMonthView.prototype.getOtherMonthDayHeaderStyle = function() {
           return this._props[14];
        }    
        ig_WebMonthView.prototype.getOtherMonthDayStyle = function() {
           return this._props[15];
        }
        ig_WebMonthView.prototype.getCompressedDayStyle = function() {
           return this._props[16];
        }
        ig_WebMonthView.prototype.getOtherCompressedDayStyle = function() {
           return this._props[17];
        }
        ig_WebMonthView.prototype.getDocType = function() {
           return this._props[18];
        }
        ig_WebMonthView.prototype.getNavigationAnimation = function() {
           return this._props[19];
        }
        ig_WebMonthView.prototype.getSection508Compliant = function() {
           return this._props[20];
        }
        ig_WebMonthView.prototype.getEnableKeyboardNavigation = function() {
           return this._props[21];
        }
        ig_WebMonthView.prototype.getActiveDayHeaderStyle = function() {
           return this._props[22];
        }
        ig_WebMonthView.prototype.getActiveDayStyle = function() {
           return this._props[23];
        } 
        ig_WebMonthView.prototype.getAllDayEventStyle = function() {
           return this._props[24];
        } 
        ig_WebMonthView.prototype.getAppointmentStyle = function() {
           return this._props[25];
        } 
        ig_WebMonthView.prototype.getSelectedAppointmentStyle = function() {
           return this._props[26];           
        }     
        ig_WebMonthView.prototype.getDayHeaderStyle = function() {
           return this._props[27];           
        }
        ig_WebMonthView.prototype.getDayStyle = function() {
           return this._props[28];           
        }
        ig_WebMonthView.prototype.getTodayHeaderStyle = function() {
           return this._props[29];
        }
        ig_WebMonthView.prototype.getTodayStyle = function() {
           return this._props[30];
        }
		ig_WebMonthView.prototype._getFirstChild = function(parent, name)
		{
			for(var i=0; i < parent.childNodes.length; i++)
			{
				var elem = parent.childNodes[i];
				if(name)
				{
					if(elem.tagName == name)
						return elem;
				}
				else if(elem.tagName == "TD" || elem.tagName == "TBODY")
					return elem;
			}
			return null;
		}
		ig_WebMonthView.prototype.callbackRender = function(response, context)
        {
			var animate = (context.operation == "Navigate" && context.CausedNavigate && this.getNavigationAnimation() > 0);
			if(context.requestType == "html")
			{
				var html = response.replace(/\^/g, "\"");
				var elem = this.getElement();
				if(animate)
					var cloneElem = elem.cloneNode(true);
				elem.innerHTML = html;
				this._loadDaysOfMonth();
				this._initActiveDay();
				
				if(animate)
				{						
					cloneElem.id += "_clone";
					elem.style.padding = "0px";
					cloneElem.style.padding = "0px";
			
					if(this.viewport == null)
					{	
						this.viewport = new ig_viewport();
						this.viewport.createViewport(elem, ViewportOrientationEnum.Horizontal);
						this.viewport.transferPositionToDiv(elem, cloneElem);
						this.viewport.animate.increment = 50;
						this.viewport.animate.onEnd = this._afterSlide;
						this.viewport.animate._elemOrigWidth = elem.style.width; 
					}
					this.viewport.div.style.width = this.viewport.div.offsetWidth + "px";
					var direction = AnimationDirectionEnum.Left;
					if(context.Days > 0 || context.Months > 0 || context.Years > 0)
						direction = AnimationDirectionEnum.Right;
					this.viewport.animate._newElem = elem;
					elem.style.width = this.viewport.div.style.width;
					this.viewport.animate._oldElem = cloneElem;
					cloneElem.style.width = elem.style.width;
					this.viewport.scroll(cloneElem, elem, direction, this.getNavigationAnimation());
					this.viewport.animate._viewDiv = this.viewport.div; 
				}
			}
			else if(context.requestType == "json")
			{
				var json = eval(response.replace(/\^/g, "\""));
				this._props = json;
			}
        }
        
        ig_WebMonthView.prototype._afterSlide = function()
        {
			this._oldElem.parentNode.removeChild(this._oldElem);
			this.getContainer().scrollLeft = 0; 
			this._newElem.style.width = "100%"; 
			this._viewDiv.style.width = this._elemOrigWidth; 
        }
        
	}
	return new ig_WebMonthView(props);
}

function ig_WebMonthView(props)
{
	if(arguments.length != 0)
		this.init(props);
}

// public: get object from ClientID or UniqueID
function ig_getWebMonthViewById(id)
{
	return ig_getWebControlById(id);
}
