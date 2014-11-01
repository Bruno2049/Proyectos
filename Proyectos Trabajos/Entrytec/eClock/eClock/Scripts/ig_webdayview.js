/*
* ig_webdayview.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


//vs 091908
function ig_getWebDayViewById(id)
{
	return ig_getWebControlById(id);
} 
function ig_WebDayView(props)
{
	this._initProp(props);
}
function ig_CreateWebDayView(props)
{
	if(!ig_WebControl.prototype.isPrototypeOf(ig_WebDayView.prototype))
	{
		ig_WebDayView.prototype = new ig_WebControl();
		ig_WebDayView.prototype.constructor = ig_WebDayView;
		ig_WebDayView.prototype.base = ig_WebControl.prototype;
		ig_WebDayView.prototype._initDays = function(fields)
		{
			fields = fields.split("-");
			this._days = new Array();
			var i = 0;
			while(i + 2 < fields.length)
			{
				var day = new Object();
				day.time = (new Date(fields[i], fields[i + 1], fields[i + 2])).getTime();
				var next = (new Date(fields[i], fields[i + 1], parseInt(fields[i + 2]) + 1)).getTime() - day.time;
				if(next == 90000000)
					day._shift = 3600000;
				if(next == 82800000)
					day._shift = -3600000;
				day.first = day.last = -1;
				this._days[i / 3] = day;
				i += 3;
			}
		}
		ig_WebDayView.prototype._initProp = function(props)
		{
			if (!props || typeof document == 'unknown')
				return;
			this._isInitializing = true;
			this._initControlProps(props);
			this.init(this._clientID);
			this._element = document.getElementById(this._clientID);						
			var elem = this._element;
			if (!elem)
				return;

			

			elem.control = this;
			
			this._fixed = true;
			this._childSlots = this._props[7];
			this._dayStart = this._props[12];
			this._dayDuration = this._props[13];
			this._initDays(this._props[3]);
			this._dayHeaderElements = new Array();
			this._apps = new Array();
			ig_shared.addEventListener(elem, "keydown", ig_handleEvent);
			ig_shared.addEventListener(elem, "mousemove", ig_handleEvent);
			ig_shared.addEventListener(elem, "mouseover", ig_handleEvent);
			ig_shared.addEventListener(elem, "mouseout", ig_handleEvent);
			ig_shared.addEventListener(elem, "mousedown", ig_handleEvent);
			ig_shared.addEventListener(elem, "mouseup", ig_handleEvent);
			ig_shared.addEventListener(elem, "dblclick", ig_handleEvent);
			ig_shared.addEventListener(elem, "click", ig_handleEvent);
			ig_shared.addEventListener(elem, "select", ig_cancelEvent);
			ig_shared.addEventListener(elem, "selectstart", ig_cancelEvent);
			this._initElem(this._element);
			var es = elem.style;
			this._fixOnResize = ((ig_shared.isEmpty(es.width) || es.width.indexOf("%") > 0) ? 2 : 0) + ((es.height.indexOf("%") > 0) ? 1 : 0);
			this._onResize(1);
			this._info = ig_getWebControlById(this._props[2]);            
			this._setActiveDay = function(info, evnt, day, id, smartCallback)
			{
				var me = this._event._object;
				if (!me || !day)
					return;
				day = (new Date(day.getFullYear(), day.getMonth(), day.getDate())).getTime();
				var currentActiveDay = info.getActiveDay();
				day2 = (new Date(currentActiveDay.getFullYear(), currentActiveDay.getMonth(), currentActiveDay.getDate())).getTime();
				var day0 = me._days[0].time;
				if ((day < day0 || day > day0 + (me._days.length - 1) * 3600000 * 24) || (day2 < day0 || day2 > day0 + (me._days.length - 1) * 3600000 * 24))
				{
					evnt.needPostBack = true;
					if (smartCallback != null)
					{
						var serverContext = { operation: "ActiveDaySync", requestType: "html" };
						var clientContext = { operation: "ActiveDaySync", requestType: "html" };
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

						serverContext = { operation: "ActiveDaySync", requestType: "json" };
						clientContext = { operation: "ActiveDaySync", requestType: "json", scrollPosition: me._divScroll.scrollTop };
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

						serverContext = { operation: "ActiveDaySync", requestType: "styles" };
						clientContext = { operation: "ActiveDaySync", requestType: "styles" };
						smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);
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
			
			this._navigate = function(info, evnt, years, months, days, newActiveDay, id, smartCallback)
			{
				var me = this._event._object;

				if (smartCallback != null)
				{
					var serverContext = { operation: "Navigate", requestType: "html" };
					var clientContext = { operation: "Navigate", requestType: "html", Days: days, Months: months, Years: years };
					if (me._clientID == id)
						clientContext.CausedNavigate = true;
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

					serverContext = { operation: "Navigate", requestType: "json" };
					clientContext = { operation: "Navigate", requestType: "json", scrollPosition: me._divScroll.scrollTop };
					if (me._clientID == id)
						clientContext.CausedNavigate = true;
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

					serverContext = { operation: "Navigate", requestType: "styles" };
					clientContext = { operation: "Navigate", requestType: "styles" };
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
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me); ;

					serverContext = { operation: "ActivityUpdate", requestType: "json" };
					clientContext = { operation: "ActivityUpdate", requestType: "json", scrollPosition: me._divScroll.scrollTop };
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);

					serverContext = { operation: "ActivityUpdate", requestType: "styles" };
					clientContext = { operation: "ActivityUpdate", requestType: "styles" };
					smartCallback.registerControl(clientContext, serverContext, null, me._uniqueID, me);
				}
			}
			this._info.addEventListener("InternalActivityUpdate", this._appointmentModified, this, false);
			this._info.addEventListener("InternalSetActiveDay", this._setActiveDay, this, false);
			this._info.addEventListener("InternalNavigate", this._navigate, this, false);
			
			this._info.addEventListener("InternalSetActiveResource", this._setActiveResource, this, true);
			ig_shared.addEventListener(this._divScroll, "scroll", ig_handleEvent);
			try { window.setTimeout("ig_handleEvent({type:'_load'})", 0); } catch (ex) { }
			
			


			this._isInitializing = false;
			this.fireEvent("initialize");
		}
		ig_WebDayView.prototype._onLoad = function(){this._on_load();}
		ig_WebDayView.prototype._on_load = function()
		{
			var x = this._lbl0 ? this._lbl0.offsetWidth : 0, h = this._tblRows ? this._tblRows.offsetHeight : 0;
			if(!this._info || (!this._onTimer && h > 0 && x > 0 && x == this._days[0].left))
				return false;
			if(!h || h == 0)
			{
				if(!this._onTimer)try
				{
					this._onTimer = function(){return this._on_load();}
					ig_handleTimer(this);
				}catch(ex){}
				return false;
			}
			if(this._onTimer)
				delete this._onTimer;
			this._initElem(this._element);
			this._fixLbls();
			this._onResize(2);
			if(!this._noScroll)
			{
				if((x = this.getScrollPosition()) < 0)
					x = (this._row0 ? this._row0.offsetHeight : 16) * this._props[9];
				this._divScroll.scrollTop = x;
			}
			return true;
		}
		ig_WebDayView.prototype._fixLbls = function()
		{
			var rows = this._tblRows.rows;
			var e0 = rows[0].cells[0], len = rows.length;
			var e1 = rows[len - 1].cells[0], height = Math.floor(this._tblRows.offsetHeight / len);
			if(e0 && e1 && e0.offsetHeight + 1 < e1.offsetHeight) for(var i = 0; i < len; i++)
			{
				rows[i].cells[0].style.height = height + 'px';
				if(i == 0 && e1)
				{
					var height0 = rows[i].cells[0].offsetHeight;
					if(height0 > height)
					{
						height += height - height0;
						i--;
					}
					e1 = null;
				}
			}
		}
		ig_WebDayView.prototype.getFirstDay = function()
		{
			return new Date(this._days[0].time);
		}
		ig_WebDayView.prototype.getWebScheduleInfo = function()
		{
			return this._info;
		}
		ig_WebDayView.prototype.getActivityHeightMinimum = function()
		{
			return this._props[4];
		}
		ig_WebDayView.prototype.getActivityWidthMinimum = function()
		{
			return this._props[5];
		}
		ig_WebDayView.prototype.getTimeSlotInterval = function()
		{
			return this._props[6];
		}
		ig_WebDayView.prototype.getScrollPosition = function()
		{
			return this._props[8];
		}
		ig_WebDayView.prototype.setScrollPosition = function(value, evt)
		{
			this._props[8] = value;
			this.updateControlState("ScrollPosition", value);
			if(!evt)
				this._divScroll.scrollTop = value;
		} 
		ig_WebDayView.prototype.getSection508Compliant = function()
		{
			return this._props[10];
		}
		ig_WebDayView.prototype.getEnableKeyboardNavigation = function()
		{
			return this._props[11];
		} 
		ig_WebDayView.prototype.getEnableActivityMoving = function()
		{
			return this._props[14];
		}
		ig_WebDayView.prototype.getEnableActivityResizing = function()
		{
			return this._props[15];
		}
		ig_WebDayView.prototype.getEnableAutoActivityDialog = function()
		{
			return this._props[16];
		}
		ig_WebDayView.prototype.getEnabled = function()
		{
			return this._props[18];
		}
		ig_WebDayView.prototype.getActivitySelectedStyle = function()
		{
			return this._props[26];
		}
		ig_WebDayView.prototype.getActivityHoverStyle = function()
		{
			return this._props[19];
		}
		ig_WebDayView.prototype.getAllDayEventAreaSelectedStyle = function()
		{
			return this._props[20];
		}
		ig_WebDayView.prototype.getAllDayEventSelectedStyle = function()
		{
			return this._props[21];
		}
		ig_WebDayView.prototype.getActivityEdgeBusyStyle = function()
		{
			return this._props[22];
		}
		ig_WebDayView.prototype.getActivityEdgeTentativeStyle = function()
		{
			return this._props[23];
		}
		ig_WebDayView.prototype.getActivityEdgeOutofOfficeStyle = function()
		{
			return this._props[24];
		}
		ig_WebDayView.prototype.getActivityEdgeFreeStyle = function()
		{
			return this._props[25];
		}
		ig_WebDayView.prototype.getCaptionHeaderVisible = function()
		{
			return this._props[27];
		}
		ig_WebDayView.prototype.getNavigationAnimation = function()
		{
			return this._props[28];
		}
		
		ig_WebDayView.prototype.getFadedAppointmentStyle = function()
		{
			return this._props[29];
		}
		ig_WebDayView.prototype._onResize = function(type)
		{
			if(type != 1 && type != 2 && this._fixOnResize == 0)
				return;
			var elem = this._element, tbl = this._tblRows, div = this._divScroll, bot = this._bottom;
			var ts = tbl.style;
			var height = 0, total = elem.clientHeight, height0 = elem.style.height;
			if(this._top1)
				height += this._top1.offsetHeight;
			if(this._top2)
				height += this._top2.offsetHeight;
			if(this._mrc)
			    height += this._mrc.offsetHeight;
			if(height0.indexOf("px") > 0 && type == 1)
			{
				height = parseInt(height0) - height;
				if(height < 1)
					height = 1;
				height = "" + height + "px";
				div.style.height = height;
				if(bot)
					bot.style.height = height;
			}
			else if(height0.indexOf("%") > 0 && height > 0 && bot && !ig_shared.IsIEWin)
			{
				if(!total || total < 2)
					total = elem.offsetHeight;
				if((total -= height) > 0)
					bot.style.height = total + "px";
			}
			if(this._fixScroll0 && bot)
			{
				var show = (bot.offsetHeight >= tbl.offsetHeight) ? "none" : "";
				if(this._fixScroll0.style.display != show)
				{
					this._fixScroll0.style.display = show;
					if(this._fixScroll1)
						this._fixScroll1.style.display = show;
				}
			}
			width = ts.width.indexOf("%") < 0;
			if(this._fixOnResize > 1 && width)
			{
				ts.width = "100%";
				width = false;
			}
			if(!width)
			{
				width = div.offsetWidth - this._top2.offsetWidth;
				if(width > 5)
					if((width = tbl.offsetWidth - width) > 10)
						ts.width = width + "px";
				if(width < 10)
				{
					var tw = parseInt(ts.width.substring(0, ts.width.length - 1));
					var cw = div.clientWidth, sw = div.scrollWidth;
					if(cw && sw && cw != sw)
					{
						ts.width = (sw = cw / sw * 100) + '%';
						if((tw = tbl.offsetWidth) > cw)
							ts.width = (cw / tw * sw) + '%';
					}
				}
			}
			if(this._fixOnResize > 1 || type == 2)
				this._fixAppBounds();
		}
		ig_WebDayView.prototype._app = function(i, elem)
		{
			var obj = this._apps[i];
			if(obj == null)
				this._apps[i] = obj = new Object();
			if(elem == null)
				return obj;
			obj.elem = elem;
			var day = elem.getAttribute("day");
			if(day)
				day = parseInt(day);
			else
				day = 0;
			if(this._days[day] == null)
				day = 0;
			obj.key = elem.getAttribute("key");
			obj.occur = elem.getAttribute("ig_occur");
			obj.nextOccur = elem.getAttribute("ig_nextOccur");
			obj.prevOccur = elem.getAttribute("ig_prevOccur");
			if(obj.occur != null)
			{
				obj.occur = eval(obj.occur);
				obj.time = obj.occur.getTime();
			}
			
			var app = this._info ? this._info.getActivities().getItemFromKey(obj.key) : null;
			if(obj.occur == null && app != null )
				obj.time = app.getStartDateTime().getTime();
			if(app == null)
				return obj;
			obj.index = obj.indexInArray = i;
			obj.day = day;
			this._days[day].last = i;
			if(this._days[day].first < 0)
				this._days[day].first = i;
			i = app.getShowTimeAs();
			if(i == null)
				i = 3;
			else if(i.length != null)
				i = parseInt(i);
			obj.timeAs = i;
			i = app.getDuration();
			if(i == null)
				i = 30;
			else if(i.length != null)
				i = parseInt(i);
			obj.length = i * 60000;
			return obj;
		}
		ig_WebDayView.prototype._initElem = function(elem)
		{
			try
			{
				if (!elem || !elem.getAttribute)
					return;
			} catch (ex)
			{
				return;
			}
			var i = -1;
			var attr = elem.getAttribute("uie");
			if (attr == "MAIN")
				this._dayHeaderElements = new Array();
			if (ig_shared.notEmpty(attr))
			{
				if (attr.length > 4)
				{
					try
					{
						i = parseInt(attr.substring(4));
						attr = attr.substring(0, 4);
					} catch (ex) { }
				}
				switch (attr)
				{
					case "FULL":
						this._fullHeight = true;
						break;
					case "MRC":
					    
					    this._mrc = elem;
					    if (!this._info)
							return;
					    break;
					case "TOP1":
						this._top1 = elem;
						if (!this._info)
							return;
						break;
					case "TOP2":
						this._top2 = elem;
						if (!this._info)
							return;
						break;
					case "BOTT":
						this._bottom = elem;
						break;
					case "SCRL":
						this._divScroll = elem;
						elem.id = this._clientID + "_divScroll";
						if (i == 1 && !this._bottom)
							this._bottom = elem;
						break;
					case "ROWS":
						if (i < 0)
						{
							this._tblRows = elem;
							if (!this._info)
								return;
						}
						else
							this._days[i].tblRows = elem;
						break;
					case "CAPT":
						this._caption = elem;
						return;
					case "_ADA":
						i = elem.getAttribute("day");
						this._days[i ? parseInt(i) : 0].ada = elem;
						return;
					case "FIX0":
						this._fixScroll0 = elem;
						return;
					case "FIX1":
						this._fixScroll1 = elem;
						return;
					case "LABL":
						if (i == 0)
							this._lbl0 = elem;
						return;
					case "SLOT":
						if (i == 0)
							this._row0 = elem;
						return;
					case "ATBL":
						if (!this._info)
							return;
						this._app(i, elem);
						break;
					case "A_TD":
						this._app(i).elemTd = elem;
						break;
					case "ADIV":
						this._app(i).elemDiv = elem;
						break;
					case "ALEF":
						this._app(i).elemLeft = elem;
						return;
					case "ATOP":
						this._app(i).elemTop = elem;
						return;
					case "ABOT":
						this._app(i).elemBot = elem;
						return;
					case "ATXT":
						this._app(i).elemTxt = elem;
						return;
					case "AIMA":
						this._app(i).elemAlert = elem;
						return;
					case "AIMC":
						this._app(i).elemCancel = elem;
						return;
					case "AIMR":
						this._app(i).elemReoccur = elem;
						return;
					case "DayH":
						this._dayHeaderElements.push(elem);
						return;
				}
			}
			var nodes = elem.childNodes;
			if (nodes != null)
			{
				for (var i = 0; i < nodes.length; i++)
				{
					this._initElem(nodes[i]);
				}
			}			
		}		
		
		ig_WebDayView.prototype._fixShift = function(day, time, mouse)
		{
			var s = day._shift;
			if(s && ((s > 0 && time > s) || (s < 0 && time >= -2 * s)))
				return time + (mouse ? s : - s);
			return time;
		}
		ig_WebDayView.prototype._fixAppBounds = function(day)
		{
			var height = this._tblRows.offsetHeight;
			if(!this._info || height == 0)
				return;
			var minH = this._minH;
			if(day == null)
			{
				var minH = this.getActivityHeightMinimum();
				if(minH < 1 && this._row0)
					minH = this._row0.offsetHeight;
				if(minH < 8)
					minH = 8;
				this._minH = minH;
				for(day = 0; day < this._days.length; day++)
					this._fixAppBounds(day);
				return;
			}
			var oDay = this._days[day];
			var x = this._lbl0 ? this._lbl0.offsetWidth : 0;
			var width;
			if(this._days[day].tblRows != null)
				width = this._days[day].tblRows.offsetWidth;
			while(day-- > 0)
				x += this._days[day].width;
			if(width < 1)
				width = 100;
			oDay.width = width;
			oDay.left = x;
			oDay.right = x + width;
			var j = -1, i2 = oDay.last + 1, i = oDay.first;
			var len = i2 - i;
			if(len < 1 || i < 0)
				return;
			var app, w, bottom, buf, id, jj, add = 0;
			var apps = new Array(len);
			while(++j < len)
				apps[j] = this._apps[i + j];
			this._unit = this._dayDuration / height;
			i = -1;
			while(++i < len)
			{
				app = apps[i];
				app.x = app.width = -1;
				app.y = Math.floor(this._fixShift(oDay, app.time - oDay.time - this._dayStart) / this._unit);
				app.height = Math.floor(app.length / this._unit);
				if(app.height < minH)
					app.height = minH;
			}
			for(i = 0; i < len; i++)
			{
				app = apps[i2 = i];
				bottom = app.y + app.height;
				while(++i2 < len)
				{
					app = apps[i2];
					if(app.y >= bottom)
						break;
					buf = app.y + app.height;
					if(buf < bottom)
						bottom = buf;
				}
				j = i2;
				while(--j >= i)
				{
					id = i2 - 1;
					jj = i2;
					bottom =- 1;
					while(--jj >= i)
					{
						app = apps[jj];
						if(app.x < 0)
						{
							buf = app.y + app.height;
							if(buf > bottom)
							{
								id = jj;
								bottom = buf;
							}
						}
					}
					app = apps[id];
					app.x = j - i;
					if(j == i2 - 1)
						app.width = 0;
				}
				id = i;
				j = i2;
				app = apps[i];
				jj = app.x;
				bottom = app.y;
				while(--j > i)
				{
					app = apps[j];
					buf = app.x;
					if(buf > jj)
					{
						bottom = app.y;
						id = j;
					}
				}
				w = width;
				buf = Math.floor(w * (i2 - i) / 6);
				if(buf > w)
					buf = w;
				j = i;
				while(--j >= 0)
				{
					app = apps[j];
					if(app.y + app.height > bottom && app.x <= w)
					{
						if(app.x > buf)
							w = app.x;
						else
						{
							if(apps[id].y + apps[id].height <= app.y + app.height && app.x + app.width >= w && app.x <= w - w / (i2 - i))
								app.width -= 3;
							else
							{
								if(add == 0)
									add = 2;
							}
						}
					}
				}
				if(add == 2)
				{
					w -= 3;
					add = 1;
				}
				else
					add = 0;
				j = i2;
				buf = Math.floor(w / (i2 - i));
				while(--j >= i)
				{
					app = apps[j];
					app.x *= buf;
					app.width = (app.width == 0) ? (w - app.x) : buf;
				}
				i = i2 - 1;
			}
			buf = "px";
			for(i = 0; i < len; i++)
			{
				app = apps[i];
				app.width -= 2;
				if(app.width < this.getActivityWidthMinimum())
					app.width = this.getActivityWidthMinimum();
				else
				{
					if(app.x + app.width + 2 >= width)
						app.width--;
				}
			}
			for(i = 0; i < len; i++)
			{
				app = apps[i];
				if(app.x + app.width + 2 >= width)
					app.x = width - app.width - 2;
				app.elem.style.left = (x + app.x) + "px";
				app.elem.style.width = app.width + "px";
				this._setAppHeight(app, (app.height < minH) ? minH : app.height);
			}
		}
		ig_WebDayView.prototype._setAppHeight = function(app, val)
		{
			var y = app.y, h = this._tblRows.offsetHeight, min = this._minH;
			app.noDrag = 0;
			if(y < 1)
			{
				if(y < 0)
				{
					app.noDrag = 2;
					app.elemLeft.style.cursor = app.elemTop.style.cursor = "default";
				}
				val += y - 1;
				y = 1;
				if(val > 0 && val < min) val = min;
			}
			if(val + y > h)
			{
				app.noDrag = 1;
				app.elemBot.style.cursor = "default";
				if((val = h - y) < min)if(val > 0)
				{
					val = min;
					y = h - val;
				}
			}
			if(val <= 0){app.elem.style.display="none";return;}
			app.elem.style.top = (y - 1) + "px";
			app.elem.style.height = val-- + "px";
			if(ig_shared.notEmpty(app.elem.style.display))
				app.elem.style.display = "";
			if(this._fixAppDiv || app.elem.offsetHeight > val || app.elemTd.offsetHeight > val)
			{
				this._fixAppDiv = true;
				var old = val, txt = app.elemTxt;
				if((h = app.elemTop.offsetHeight) > 9) h = 4;
				val -= h + h;
				txt = txt ? txt.offsetHeight : -9;
				if(val < txt && val < 20)
				{
					if((h = Math.floor((old - txt) / 2)) < 2)
						h = 2;
					app.elemTop.style.height = app.elemBot.style.height = h + "px";
					val = old - h - h;
				}
				app.elemTd.style.height = app.elemDiv.style.height = ((val < 2) ? 2 : val) + "px";
			}
		}
		ig_WebDayView.prototype._getAppFromIndex = function(index)
		{
			for(var j = 0; j < this._apps.length; j++)
			{
				if(this._apps[j].index == index)
					return this._apps[j];
			}
			return null;
		}
		ig_WebDayView.prototype._fixPos = function(app)
		{
			var i0 = app.indexInArray;
			var i = i0, iF = this._days[app.day].first, iL = this._days[app.day].last;
			var t = app.time;
			var apps = this._apps;
			while(i-- > iF)
			{
				if(apps[i].time > t)
				{
					app = apps[i];
					apps[i] = apps[i + 1];
					apps[i + 1] = app;
					apps[i].indexInArray = i;
					app.indexInArray = i + 1;
				}
				else break;
			}
			if(++i != i0)
				return i;
			while(++i <= iL)
			{
				if(apps[i].time < t)
				{
					app = apps[i];
					apps[i] = apps[i - 1];
					apps[i - 1] = app;
					apps[i].indexInArray = i;
					app.indexInArray = i - 1;
				}
				else break;
			}
			return --i;
		}
		ig_WebDayView.prototype._fire = function(evt)
		{
			return false;
		}
		ig_WebDayView.prototype._selectAD = function(elem)
		{
			var old = this._selectedAD;
			if(old === elem)
				return;
			if(old != null)
			{
				old.className = this._selAD_class;
				if(ig_shared.notEmpty(this._adHeight))
					this._adHeight = old.style.height = "";
			}
			if((this._selectedAD = elem) == null)
				return;
			this._selAD_class = elem.className;
			var i = ig_shared.isEmpty(elem.style.height) ? 0 : 9, h = elem.offsetHeight;
			elem.className += " " + ((elem.getAttribute("key") == null) ? this.getAllDayEventAreaSelectedStyle() : this.getAllDayEventSelectedStyle());
			
			elem.className = elem.className.replace(this.getFadedAppointmentStyle(),"");
			while(h < elem.offsetHeight && ++i < 8)
				this._adHeight = elem.style.height = (h - i) + "px";
		}
		ig_WebDayView.prototype._select = function(app)
		{
			var old = this._selectedApp;
			if(app === old)
				return;
			if(old != null)
			{
				old.elem.className = old.elemClass;
				old.elemTop.className = old.elemBot.className = old.edgeClass;
			}
			if((this._selectedApp = app) == null)
				return;
			this._selectAD();
			this._hover();
			app.elemClass = app.elem.className;
			app.edgeClass = app.elemTop.className;
			var edge = this.getActivityEdgeBusyStyle();
			switch(app.timeAs)
			{
				case 0:
					edge = this.getActivityEdgeFreeStyle();
					break;
				case 1:
					edge = this.getActivityEdgeTentativeStyle();
					break;
				case 2:
					edge = this.getActivityEdgeOutofOfficeStyle();
					break;
			}
			app.elem.className += " " + this.getActivitySelectedStyle();
			
			app.elem.className = app.elem.className.replace(this.getFadedAppointmentStyle(),"");
			app.elemTop.className += " " + edge;
			app.elemBot.className += " " + edge;
		}
		ig_WebDayView.prototype._hover = function(app)
		{
			var old = this._hoverApp;
			if(app === old)
				return;
			if(old != null)
				old.elem.className = old.elemClass;
			if(app === this._selectedApp)
				app = null;
			if(app != null)
			{
				app.elemClass = app.elem.className;
				app.elem.className += " " + this.getActivityHoverStyle();
			}
			this._hoverApp = app;
		}
		ig_WebDayView.prototype._onHandleEvent = function(src, evt) {
		    var j = -1, i = -1, drag = 0;
		    var type = evt.type;
		    var y = evt.clientY;
		    if (type == "scroll") {
		        this.setScrollPosition(this._divScroll.scrollTop, evt);
		        return;
		    }
		    if (!this.getEnabled())
		        return;
		    src = ig_findElemWithAttr(src, "uie");
		    if (!src || !this._info)
		        return;
		    var attr = src.getAttribute("uie");
		    var day = src.getAttribute("day");
		    day = day ? parseInt(day) : 0;
		    var modAttr = attr;
		    if (attr.length > 4) {
		        try {
		            i = parseInt(attr.substring(4));
		            modAttr = attr.substring(0, 4);
		        } catch (ex) { }
		    }
		    var app = (i < 0 || modAttr.substring(0, 1) != "A") ? null : this._getAppFromIndex(i);
		    var appt = null;
		    if (app == null && modAttr.substring(0, 2) == "_A") {
		        appt = this.getWebScheduleInfo().getActivities().getItemFromKey(src.getAttribute("key"));
		    }
		    if (type == "click") {
		        var appKey = (app != null) ? app.key : null;
		        (appKey == null && appt != null) ? appKey = appt.getDataKey() : appKey = null;
		        var appResKey = null;
		        (appKey != null) ? appResKey = this._info.getActivities().getItemFromKey(appKey).getResourceKey() : "";
		        if (appKey != null
		                && appResKey.length > 0
                        && appResKey != this._info.getActiveResourceDataKey()) {
		            this._info.setActiveResource(appResKey);
		            ig_cancelEvent(evt);
		            return;
		        }
		        if (this.fireEvent(type, evt, src))
		            return;
		        if (attr.indexOf("BUT") == 0) {
		            if (!ig_shared.IsIE) ig_cancelEvent(evt);
		            i = this._days.length;
		            if (attr == "BUT1") i = -i;
		            if (!this.fireEvent((i > 0) ? "navigatenext" : "navigateprevious", evt, i))
		                this._info.navigate(0, 0, i, this._clientID);
		        }
		        
		        if (attr.indexOf("RBUT") == 0) {
		            if (!ig_shared.IsIE) ig_cancelEvent(evt);
		            var resKey = attr.substring(4);
		            
		            //if(!this.fireEvent((i > 0) ? "navigatenext" : "navigateprevious", evt, i))
		            this._info.setActiveResource(resKey);
		        }
		        return;
		    }
		    attr = modAttr;
		    if (type == "dblclick") {
		        var appResKey = null;
		        (app != null) ? appResKey = this._info.getActivities().getItemFromKey(app.key).getResourceKey() : "";
		        if (app != null
		                && appResKey.length > 0
                        && appResKey != this._info.getActiveResourceDataKey()) {
		            this._info.setActiveResource(appResKey);
		            ig_cancelEvent(evt);
		            return;
		        }
		        if (this.fireEvent(type, evt, src))
		            return;
		        if (!this.getEnableAutoActivityDialog())
		            return;
		        if (attr == "SLOT" || attr == "LABL" || attr == "_ADA") {
		            var time = 0;
		            if (i >= 0)
		                time += i * this.getTimeSlotInterval() * 60000;
		            if (attr == "LABL")
		                time *= this._childSlots;
		            time += this._days[day].time + this._dayStart;
		            time = this._fixShift(this._days[day], time, true);
		            app = this._info.getActivities().createActivity();
		            app.setStartDateTime(new Date(time));
		            if (i >= 0)
		                app.setDuration(this.getTimeSlotInterval());
		            app.setAllDayEvent(i < 0);
		            this._info.showAddAppointmentDialog(app, this._clientID);
		            return;
		        }

		        var occur = null;
		        var nextOccur = null;
		        var prevOccur = null;
		        if (app != null) {
		            occur = app.occur;
		            nextOccur = app.nextOccur;
		            prevOccur = app.prevOccur;
		            app = app.key;
		        }
		        else if (attr == "_ADE")
		            app = src.getAttribute("key");
		        if (ig_shared.notEmpty(app)) {
		            if (occur != null)
		                this._info._showAppointmentDialog(app, this._clientID, occur, prevOccur, nextOccur)
		            else
		                this._info._showAppointmentDialog(app, this._clientID, null, prevOccur, nextOccur)
		        }
		        return;
		    }
		    var minH = this._minH;
		    if (type == "mousedown") {
		        var appResKey = null;
		        (app != null) ? appResKey = this._info.getActivities().getItemFromKey(app.key).getResourceKey() : "";
		        if (app != null
		                && appResKey.length > 0
                        && appResKey != this._info.getActiveResourceDataKey()) {
                    
                    this._select(app);
		            this._info.setActiveResource(appResKey);
		            ig_cancelEvent(evt);
		            return;
		        }
		        if (this.fireEvent(type, evt, src))
		            return;
		        //ig_cancelEvent(evt);
		        this._select(app);
		        this._selectAD((attr == "_ADE" || attr == "_ADA") ? src : null);
		        if (app == null)
		            return;
		        switch (attr) {
		            case "ALEF":
		                if (this.getEnableActivityMoving() && app.noDrag != 2)
		                    drag = 1;
		                break;
		            case "ATOP":
		                if (this.getEnableActivityResizing() && this.getEnableActivityMoving() && app.noDrag != 2)
		                    drag = 3;
		                break;
		            case "ABOT":
		                if (this.getEnableActivityResizing() && app.noDrag != 1)
		                    drag = 2;
		                break;
		        }
		        if (drag > 0) {
		            this._drag = app;
		            this._dragY = y;
		            this._dragType = drag;
		            this._dragFire = true;
		            app.length0 = app.length;
		            app.time0 = app.time;
		        }
		        return;
		    }
		    var out = type == "mouseout";
		    if (type == "mouseover" || out) {
		        if (app != null && out) {
		            var to = evt.toElement;
		            if (!to)
		                to = evt.relatedTarget;
		            while (to != null) {
		                if (to === app.elem)
		                    return;
		                to = to.parentNode;
		            }
		        }
		        this._hover(app);
		        return;
		    }
		    var up = type == "mouseup";
		    if (up)
		        this.fireEvent(type, evt, src);
		    if (this._drag != null && (up || type == "mousemove")) {
		        app = this._drag;
		        var fixY = (this._dragType & 1) == 1;
		        if (fixY && app.y + y - this._dragY < 0)
		            y = this._dragY - app.y;
		        if (this._dragFire || up) {
		            this._dragFire = false;
		            if (this._fire(this._dragType))
		                return;
		        }
		        var height = y - this._dragY;
		        if (this._dragType > 2)
		            height = -height;
		        if (up) {
		            var h0 = this.getTimeSlotInterval() * 60000;
		            if (this._dragType > 1) {
		                height += app.height;
		                if (height < minH) {
		                    height = minH;
		                    if (this._dragType > 2)
		                        y = this._dragY - minH + app.height;
		                }
		                app.length = Math.ceil(height * this._unit);
		                if (this._fixed)
		                    app.length = app.length0 + h0 * Math.ceil((app.length - app.length0 - h0 / 2) / h0);
		            }
		            if (fixY) {
		                app.time += Math.floor((y - this._dragY) * this._unit);
		                if (this._fixed)
		                    app.time = app.time0 + h0 * Math.ceil((app.time - app.time0 - h0 / 2 + 10000) / h0);
		            }
		            var ap = this._info.getActivities().getItemFromKey(app.key);
		            if (app.time != app.time0 || app.length != app.length0) {
		                if (ap.getRecurrence() != null) {
		                    ap.setOriginalStartDate(app.occur);
		                    ap.setIsVariance(true);
		                }
		                if (!this._info.updateActivity({ StartDateTime: new Date(app.time), Duration: Math.floor(app.length / 60000) }, ap, this._clientID)) {
		                    app.time = app.time0;
		                    app.length = app.length0;
		                }
		                else if (fixY)
		                    this._fixPos(this._drag);
		            }
		            this._drag = null;
		            this._fixAppBounds();
		        }
		        else {
		            if (this._dragType > 1) {
		                height += app.height;
		                if (height < minH) {
		                    height = minH;
		                    if (this._dragType > 2)
		                        y = this._dragY - minH + app.height;
		                }
		                this._setAppHeight(app, height);
		            }
		            if (fixY)
		                app.elem.style.top = (app.y + y - this._dragY) + "px";
		        }
		        return;
		    }
		}
	}
	ig_WebDayView.prototype.getSelectedActivity = function()
	{
		var app = this._selectedApp;
		if(app)
			app = app.key;
		if(!app) if((app = this._selectedAD) != null)
			app = app.getAttribute("key");
		return (app && this._info) ? this._info.getActivities().getItemFromKey(app) : null;
	}
	
	ig_WebDayView.prototype.callbackRender = function(response, context)
    {
		var elem = this.getElement();
		
		if(context.operation == "SetActiveResource")
		    this._selectedApp = null;
		var animate = (context.operation == "Navigate" && context.CausedNavigate && this.getNavigationAnimation() > 0);
		if(context.requestType == "html")
		{
			if(animate)
				var cloneElem = elem.cloneNode(true);
			
			var html = response.replace(/\^/g, "\"");
			
			if(elem.tagName == "TABLE")
				this._setInnerHTML(html);
			else
			{
				this._bottom = null;
				elem.innerHTML = html;
			}
			
			if(animate)
			{						
				cloneElem.id += "_clone";
				if(this.viewport == null)
				{	
					this.viewport = new ig_viewport();
					this.viewport.createViewport(elem, ViewportOrientationEnum.Horizontal);
					this.viewport.table.style.height = "100%";
					this._oldOnResize = this._onResize;
					this._onResize = this._viewportDivResize;
					this.viewport.transferPositionToDiv(elem,cloneElem);
					this.viewport.animate.dayView = this;
					this.viewport.animate.increment = 50;
					this.viewport.animate.onEnd = this._afterSlide;
					this.viewport.animate._elemOrigWidth = elem.style.width; 
				}
				this.viewport.div.style.width = this.viewport.div.offsetWidth + "px";
				var direction = AnimationDirectionEnum.Left;
				if(context.Days > 0 || context.Months > 0 || context.Years > 0)
					direction = AnimationDirectionEnum.Right;
					
				elem.style.width = this.viewport.div.style.width;
				cloneElem.style.width = elem.style.width;
					
				this.viewport.animate._newElem = elem;
				this.viewport.animate._oldElem = cloneElem;
				this.viewport.scroll(cloneElem, elem, direction, this.getNavigationAnimation());
				this.viewport.animate._viewDiv = this.viewport.div; 
			}						
		}
		else if(context.requestType == "json")
		{	
			var json = eval(response.replace(/\^/g, "\""));
			if(!animate)
				this._loadJSON(json, context.scrollPosition);			
			else
			{
				this._json = json; 
				this._sp = context.scrollPosition;
			}
		}
    }
    ig_WebDayView.prototype._viewportDivResize = function(type)
    {
		var returnval = this._oldOnResize(type);
		this.viewport.div.style.height = this._element.style.height; 
		return returnval;
    }
    ig_WebDayView.prototype._loadJSON = function(json, scrollPosition)
    {
		var elem = this.getElement();
		this._initDays(json[1]);
		this._info = null;
		this._initElem(elem);
		this._onResize(1);
		this._info = ig_getWebControlById(this._props[2]);
		this._noScroll = true;
		this._on_load();
		this._noScroll = false;
		this._divScroll.scrollTop = scrollPosition;
    }
    ig_WebDayView.prototype._afterSlide = function()
    {
		this.dayView._loadJSON(this.dayView._json, this.dayView._sp);
		this.dayView._json == null;
		this.dayView._sp = null;
		this._oldElem.parentNode.removeChild(this._oldElem);
		this.getContainer().scrollLeft = 0; 
		this._newElem.style.width = "100%"; 
		this._viewDiv.style.width = this._elemOrigWidth; 
    }
    
    ig_WebDayView.prototype._setInnerHTML = function(html)
    {
		var tempElement = document.createElement("DIV");
		tempElement.innerHTML = html;
		var mainElem = this.getElement();
		var table = tempElement.childNodes[0];
		if(table.tagName == "TABLE")
		{	
			var tbody = table.childNodes[0];
			while(tbody.tagName != "TBODY")
			{
				tbody = tbody.nextSibling;	
			}
			if(tbody.tagName == "TBODY")
			{
				var mainTbody = mainElem.childNodes[0];
				while(mainTbody.tagName != "TBODY")
				{
					mainTbody = mainTbody.nextSibling;	
				}
				if(mainTbody.tagName == "TBODY")
				{
					while(mainTbody.rows.length > 0)
						mainTbody.removeChild(mainTbody.rows[0]);
					while(tbody.rows.length > 0)
						mainTbody.appendChild(tbody.rows[0]);
				}
			}
			
		}
	}
	
	ig_WebDayView.prototype._getUieFromElem = function(elem)
	{
		return attr = elem.getAttribute ? elem.getAttribute("uie") : null;
	}
	ig_WebDayView.prototype._getDayHeaderIndex = function(src)
	{
		for(var i = 0; i < this._dayHeaderElements.length; i++)
			if(this._dayHeaderElements[i] == src)
				return i; 
		return -1;
	}
	ig_WebDayView.prototype._dayHeaderKeyDown = function(src, key)
	{
		var elem = null; 
		var index = this._getDayHeaderIndex(src); 
		if(key == 37)      // left
		{
			index--;
			if(index < 0)
				elem = this._dayHeaderElements[this._dayHeaderElements.length -1]; 
			else
				elem = this._dayHeaderElements[index];  
		}		
		else if(key == 39) // right
		{
			index++;
			if(index == this._dayHeaderElements.length )
				elem = this._dayHeaderElements[0]; 
			else
				elem = this._dayHeaderElements[index];  
		}
		else if(key == 40) // down
		{
			var allDayArea = this._days[index].ada; 	
			if (allDayArea)
			{
				var day = allDayArea.getAttribute("day"); 
				if(day == null)
					day = 0; 
				
				if(this._days[index].allDayAppts == null)
				{
					this._days[index].allDayAppts = new Array(); 
					var ada = allDayArea.childNodes[0].childNodes[0]; 
					
					if (allDayArea.childNodes[0] && allDayArea.childNodes[0].getAttribute("uie") && allDayArea.childNodes[0].getAttribute("uie").indexOf("_ADE") > -1)
					{
						ada = allDayArea;
						for(var i = 0; i < ada.childNodes.length; i++)
						{
							var uie = this._getUieFromElem(ada.childNodes[i]); 
							if(uie != null && uie.indexOf("_ADE") > -1)
							{
								ada.childNodes[i].day = index;
								this._days[index].allDayAppts.push(ada.childNodes[i]); 
							}
						}
						
					} else
					{
						for(var i = 0; i < ada.childNodes.length; i++)
						{
							if(ada.childNodes[i].tagName == "TR")
							{
								var cells = ada.childNodes[i].childNodes
								for(var j = 0; j < cells.length; j++)
								{
									var uie = this._getUieFromElem(cells[j]); 
									if(uie != null && uie.indexOf("_ADE") > -1)
									{
										cells[j].day = index;
										this._days[index].allDayAppts.push(cells[j]); 
									}
								}
							}
						}
					}
				}
			
				if(this._days[index].allDayAppts.length > 0)
					return this._days[index].allDayAppts[0];
				for(var i = 0; i < this._apps.length; i++)
					if(this._apps[i].day == day)
						return this._apps[i].elem; 
			}
			
		}
		else if(key == 13) // Enter
		{
			if(this.getEnableAutoActivityDialog())
			{
				var scheduleInfo = this.getWebScheduleInfo(); 
				var dateTime = new Date(this._days[index].time)
				var appointment = scheduleInfo.getActivities().createActivity();
				appointment.setStartDateTime(dateTime);
				scheduleInfo.showAddAppointmentDialog(appointment, this._clientID);
				elem = this._element;
			}			
		}
		
		return elem; 
	}
	
	ig_WebDayView.prototype._getAppointmentsForDay = function(index)
	{
		var appts = new Array(); 
		appts = appts.concat(this._days[index].allDayAppts); 
		for(var i = 0; i< this._apps.length; i++)
			if(this._apps[i].day == index)
				appts.push(this._apps[i].elem); 
		return appts; 
	}
	
	ig_WebDayView.prototype._getIndexOfAppt = function(appt, appts)
	{
		for(var i = 0; i < appts.length; i++)
			if(appts[i] == appt)						
				return i; 
		return -1; 
	}
	
	ig_WebDayView.prototype._apptKeyDown = function(src, key)
	{
		var index = src.getAttribute("day"); 
		if(index == null)
			index = 0; 
		var appts = this._getAppointmentsForDay(index); 		
		var apptIndex = this._getIndexOfAppt(src, appts); 
		var elem = null; 
		if(key == 37)      // Left
		{
			index--;
			if(index == -1)
				elem =this._dayHeaderElements[this._dayHeaderElements.length-1]; 	
			else
				elem =this._dayHeaderElements[index]; 	
		}
		else if(key == 38) // Up
		{
			apptIndex--; 
			if(apptIndex == -1)
				elem = appts[appts.length -1]; 
			else
				elem = appts[apptIndex]; 
		
		}
		else if(key == 39) // Right
		{
			index++;
			if(index == this._dayHeaderElements.length)  
				elem =this._dayHeaderElements[0]; 	
			else
				elem =this._dayHeaderElements[index]; 
		}
		else if(key == 40) // Down
		{	
			apptIndex++; 
			if(apptIndex == appts.length)
				elem = appts[0]; 
			else
				elem = appts[apptIndex]; 
		}
		else if(key == 13) // Enter
		{
			if(this.getEnableAutoActivityDialog())
			{
				var scheduleInfo = this.getWebScheduleInfo(); 
				var prevOccur = src.getAttribute("ig_prevOccur"); 
				var nextOccur = src.getAttribute("ig_nextOccur"); 
				var occur = src.getAttribute("ig_occur");
				var key = src.getAttribute("key");
				scheduleInfo._showAppointmentDialog(key, this._clientID, occur, prevOccur, nextOccur) 
				elem = this._element;
			}
		}
		else if(key == 46) // Delete
		{
			var wsi = this.getWebScheduleInfo(); 
			var appt = wsi.getActivities().getItemFromKey(src.getAttribute("key")); 
			wsi.deleteActivity(appt); 
		}
		else if(key == 27)
			elem = this._dayHeaderElements[index]; 
		return elem; 
	}
	
	ig_WebDayView.prototype._onKeydown = function(src, evt)
	{
		if(this.getSection508Compliant() || this.getEnableKeyboardNavigation())
		{
			if(src != this._element && src.tagName != "BUTTON")
				src.tabIndex = -1; 	
				
			var elem = null;
			var uie = src.getAttribute("uie");
			
			if(uie == "DayHeader")
				elem = this._dayHeaderKeyDown(src, evt.keyCode); 
			else if(uie.indexOf("_ADE") > -1 || uie.indexOf("ATBL") > -1)
				elem  = this._apptKeyDown(src, evt.keyCode); 
			else
			{
				if(src.tagName == "BUTTON")
				{
					if(evt.keyCode == 32 || evt.shiftKey)
						return null; 
				}
				if(evt.keyCode != 9)				
					elem = this._dayHeaderElements[0]; 
			}

			if(elem != null)
			{
				if(elem.parentElement != null)
				{		
					elem.tabIndex = this._element.tabIndex; 
					elem.focus();  
				}
				ig_cancelEvent(evt); 
			}
		}
	}
	return new ig_WebDayView(props);
}
