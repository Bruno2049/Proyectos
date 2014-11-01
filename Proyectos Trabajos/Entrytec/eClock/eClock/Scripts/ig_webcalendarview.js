/*
* ig_webcalendarview.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


//vs 06/08/2009
function ig_getWebCalendarViewById(id)
{
	return ig_getWebControlById(id);
} 
function ig_WebCalendarView(prop)
{
	this._initProp(prop);
}
function ig_CreateCalendar(prop)
{
if(!ig_WebControl.prototype.isPrototypeOf(ig_WebCalendarView.prototype))
{
	ig_WebCalendarView.prototype=new ig_WebControl();
	ig_WebCalendarView.prototype.constructor=ig_WebCalendarView;
	ig_WebCalendarView.prototype.base=ig_WebControl.prototype;
	ig_WebCalendarView.prototype._initProp = function(prop)
	{
		if (!prop || typeof document == 'unknown') return;
		this._is1st = true;
		this._initControlProps(prop);
		var id = this._clientID;
		this.init(id);
		this._infoID = prop[0][2];
		this._info = ig_getWebControlById(this._infoID);
		this._titleFormat = prop[0][3];
		this._css = new Array();
		var elem = document.getElementById(id);
		if (!elem) return;
		

		elem.control = this;

		this._setActiveDay = function(info, evt, day, id, smartCallback)
		{
			var me = this._event._object;
			if (me && day && me._clientID != id) me.select(day.getFullYear(), day.getMonth() + 1, day.getDate(), -3);
		}
		this._initAtr = function()
		{
			var i, elem;
			//0=500-prevMonth,1=502-nextMonth,2=504-MonthDrop,3=506-YearDrop
			//4=508-Footer,5=510-Title,6=512-Calendar,7=514-Dow
			this._elems = new Array(8);
			for (i = 0; i < 8; i++)
			{
				if ((elem = document.getElementById(id + "_" + (500 + i * 2))) != null)
				{
					this._elems[i] = elem;
					elem.setAttribute(ig_shared.attrID, id);
					elem.setAttribute("id_", 500 + i * 2);
					if (i == 2 || i == 3) ig_shared.addEventListener(elem, 'change', ig_handleEvent);
				}
			}
		}
		this._navigate = function(info, evnt, years, months, days, day, id, smartCallback)
		{
			var me = this._event._object;
			if (me._info.getEnableSmartCallbacks())
			{
				if (me._info.getActiveDay().getMonth() == day.getMonth() && me._info.getActiveDay().getFullYear() == day.getFullYear())
					me.select(day.getFullYear(), day.getMonth() + 1, day.getDate(), -3);
				else
				{
					if (smartCallback != null)
					{
						var registeredControls = new Array();
						registeredControls = smartCallback._registeredControls;
						smartCallback._registeredControls = new Array();
						smartCallback._registeredControls[0] = registeredControls[0];
						smartCallback._registeredControls[1] = registeredControls[1];
						var clientContext = { operation: "Navigate", requestType: "html", Days: days, Months: months, Years: years };
						if (me._clientID == id)
							clientContext.CausedNavigate = true;
						me._registerSmartCB(smartCallback, "Navigate", "html", null, clientContext);
						clientContext = { operation: "Navigate", requestType: "json" };
						if (me._clientID == id)
							clientContext.CausedNavigate = true;
						me._registerSmartCB(smartCallback, "Navigate", "json", null, clientContext);
						me._registerSmartCB(smartCallback, "Navigate", "styles");
						for (var i = 2; i < registeredControls.length; i++)
							smartCallback._registeredControls.push(registeredControls[i]);
					}

					evnt.needPostBack = true;
				}
			}
		}
		this._appointmentModified = function(info, evnt, activity, id, smartCallback, cancelChangeControlOrder)
		{
			var me = this._event._object;

			if (smartCallback != null)
			{
				if (!cancelChangeControlOrder)
				{
					var registeredControls = new Array();
					registeredControls = smartCallback._registeredControls;
					smartCallback._registeredControls = new Array();
					smartCallback._registeredControls[0] = registeredControls[0];
					smartCallback._registeredControls[1] = registeredControls[1];
				}

				me._registerSmartCB(smartCallback, "ActivityUpdate", "html");
				me._registerSmartCB(smartCallback, "ActivityUpdate", "json");
				me._registerSmartCB(smartCallback, "ActivityUpdate", "styles");

				if (!cancelChangeControlOrder)
				{
					for (var i = 2; i < registeredControls.length; i++)
						smartCallback._registeredControls.push(registeredControls[i]);
				}

			}

		}
		this._info.addEventListener("InternalSetActiveDay", this._setActiveDay, this, false);
		this._info.addEventListener("InternalNavigate", this._navigate, this, false);
		this._info.addEventListener("InternalActivityUpdate", this._appointmentModified, this, false);
		if(this.getSection508Compliant() || this.getEnableKeyboardNavigation())
			ig_shared.addEventListener(elem, "keydown", ig_handleEvent);
		ig_shared.addEventListener(elem, "select", ig_cancelEvent);
		ig_shared.addEventListener(elem, "selectstart", ig_cancelEvent);
		ig_shared.addEventListener(elem, "mousedown", ig_handleEvent);
		ig_shared.addEventListener(elem, "mouseup", ig_handleEvent);
		ig_shared.addEventListener(elem, "click", ig_handleEvent);
		ig_shared.addEventListener(elem, "dblclick", ig_handleEvent);
		this._element = elem;
		elem.setAttribute(ig_shared.attrID, id);
		var o, i = -1, sep = ",";
		var dates = prop[0][5];
		prop = prop[0][4].split(sep);
		this._enabled = !ig_csom.isEmpty(prop[0]);
		this._dow = this.intI(prop, 1);
		this._fixVis = !ig_csom.isEmpty(prop[2]);
		for (i = 3; i < 10; i++) if (!ig_csom.isEmpty(o = prop[i])) this._css[i - 3] = o;
		this.Days = new Array(42);
		prop = dates.split(sep);
		this._date = [this.intI(prop, 0), this.intI(prop, 1), this.intI(prop, 2), -1];
		var year = this.intI(prop, 3), month = this.intI(prop, 4);
		this._min = this._nd(this.intI(prop, 5), this.intI(prop, 6), this.intI(prop, 7));
		this._max = this._nd(this.intI(prop, 8), this.intI(prop, 9), this.intI(prop, 10));
		this._now = [this.intI(prop, 11), this.intI(prop, 12), this.intI(prop, 13)];
		this._yearFix = this.intI(prop, 14); //for not gregorian
		this._day = new Object();
		this._initAtr();
		this.repaint(year, month);
		this._is1st = false;
		this.fireEvent("initialize");
	}
	ig_WebCalendarView.prototype.valI=function(o,i){o=(o==null||o.length<=i)?null:o[i];return (o==null)?"":o;}
	ig_WebCalendarView.prototype.intI=function(o,i){return ig_csom.isEmpty(o=this.valI(o,i))?-1:parseInt(o);}
	ig_WebCalendarView.prototype._nd=function(y,m,d)
	{
		d=new Date(y,--m,d);
		if(y<100&&d.setFullYear!=null)d.setFullYear(y);
		return d;
	}
	ig_WebCalendarView.prototype._registerSmartCB=function(smartCallback, operation,type, sc, cc)
	{
		if(sc == null)
			sc = {operation:operation, requestType:type};
		if(cc == null)
			cc = {operation:operation, requestType:type};
			
		smartCallback.registerControl(cc, sc, null, this._uniqueID, this)
	}
	ig_WebCalendarView.prototype._df=function(d,i)
	{
		if(i==0)return d.getFullYear();
		if(i==1)return d.getMonth()+1;
		return (i==2)?d.getDate():d.getDay();
	}
	ig_WebCalendarView.prototype._render=function(o,css,sel)
	{
		this._day.year=o.year;
		this._day.month=o.month;
		this._day.day=o.day;
		this._day.index=o.index;
		this._day.dow=(this._dow+(o.index%7))%7;
		this._day.element=o.element;
		this._day.text=""+o.day;
		this._day.activity=o.activity;
		o=this._day;
		o.css=css;
		o.selected=sel;
		return this.fireEvent("renderday",null,o);
	}
	ig_WebCalendarView.prototype.isSelected=function(year,month,day,i)
	{
		if(this._date[0]==year && this._date[1]==month && this._date[2]==day)
		{if(i>=0)this._date[3]=i;return true;}
		return false;
	}
	ig_WebCalendarView.prototype.isSel=function(i){return this._date[3]==i;}
	//already checked for old sel
	ig_WebCalendarView.prototype.select=function(year,month,day,i,e,post)
	{
		if(this._info)if(!this._info.setActiveDay(this._nd(year,month,day),post,this._clientID))return;
		//unselect
		var o,text=null,sel=this._date[3];
		if(sel>=0)
		{
			o=this.Days[sel];
			sel=o.css;
			if(this._render(o,sel,false))o=null;
			else{text=this._day.text;sel=this._day.css;}
			if(o!=null)
			{
				o.element.className=sel;
				if(text!=null)ig_shared.setText(o.element,text);
			}
		}
		this._date[0]=year;
		this._date[1]=month;
		this._date[2]=day;
		this._date[3]=-1;
		if(i<-1)//flag to calculate i
		{
			if(year!=this.Days[15].year||month!=this.Days[15].month)if(this.repaint(year,month,false,e)!=1)
				return;
			for(i=41;i>=0;i--)if(year==this.Days[i].year&&month==this.Days[i].month&&day==this.Days[i].day)
				break;
		}
		if(i>-2)if((this._date[3]=i)>=0)
		{
			o=this.Days[i];
			sel=o.css+" "+this._css[5];
			text=null;
			if(this._render(o,sel,true))o=null;
			else{text=this._day.text;sel=this._day.css;}
			if(o!=null)
			{
				o.element.className=sel;
				if(text!=null)ig_shared.setText(o.element,text);
			}
		}
	}
	ig_WebCalendarView.prototype.minMax=function(y,m,d)
	{
		m=this._nd(y,m,d);
		d=m.getTime();
		if(d>this._max.getTime())return this._max;
		if(d<this._min.getTime())return this._min;
		return null;
	}
	ig_WebCalendarView.prototype.repaint=function(year,month,check,e)
	{
		var id=(year==null);
		if(id||year<1)year=this.Days[15].year;
		if(month==null)month=this.Days[15].month;
		if(check==null)check=false;
		if(month<1){month=12;year--;}
		if(month>12){month-=12;year++;}
		var yy,i,o,d=this.minMax(year,month,1);
		if(d!=null){year=this._df(d,0);month=this._df(d,1);}
		if(e!=null)
		{
			if(this.fireEvent("navigate",e,(d==null)?this._nd(year,month,1):d))
			{
				if((o=this._elems[2])!=null)o.selectedIndex=this.Days[15].month-1;
				if((o=this._elems[3])!=null)o.selectedIndex=this.Days[15].year-this.year0;
				return 1;
			}
		}
		else
		{
			if((o=this._elems[2])!=null)o.selectedIndex=month-1;
			if((o=this._elems[3])!=null)if(this.year0==null)if((d=o.options)!=null)
				if((d=d[0])!=null)try{this.year0=parseInt(ig_shared.getText(d))-this._yearFix;}catch(ex){}
			if(this.year0!=null)
			{
				i=o.options.length;
				var y=year-(i>>1);
				d=this._df(this._min,0);
				if(y<d)y=d;else if(y+i>(d=this._df(this._max,0)))y=d-i+1;
				if(this.year0!=y)
				{
					while(i-->0)
					{
						yy=y+i+this._yearFix;d=(yy>999)?"":((yy>99)?"0":"00");
						ig_shared.setText(o.options[i],d+yy);
					}
					o.selectedIndex=-1;
				}
				o.selectedIndex=year-(this.year0=y);
			}
			if((o=this.Days[15])!=null)
			{
				if(o.year==year&&o.month==month){if(check)return 0;}
				else check=true;
			}
			else check=false;
			var numDays=(month==2)?28:30;
			d=this._nd(year,month,numDays+1);
			if(this._df(d,1)==month)numDays++;
			d=this._nd(year,month,1);
			i=this._df(d,3)-this._dow;
			var day1=(i<0)?i+7:i;
			if(!this._is1st)
			{
				if(this._elems[5]!=null&&(id||this.Days[15].month!=month||this.Days[15].year!=year))
				{
					d=(((yy=year+this._yearFix)<1000)?((yy<100)?"00":"0"):"")+yy;
					o=this._titleFormat.replace("##",d).replace("#",d.substring(2)).replace("%%",this._objects[month-1]).replace("%",""+month);
					ig_shared.setText(this._elems[5],o);
				}
			}
			id=this.fireEvent("renderday","check");
			d=this._nd(year,month,0);
			var day0=this._df(d,2)-day1+1;
			this._date[3]=-1;
			var iC=0,iCL=this._collections?this._collections.length:0,sun=(7-this._dow)%7;
			for(i=0;i<42;i++)
			{
				if(this._is1st)
				{
					var elem=null;
					if((elem=document.getElementById(this._id+"_d"+i))==null)continue;
					elem.setAttribute(ig_shared.attrID,this._id);
					elem.setAttribute("id_",i);
					o=this.Days[i]=new Object();
					o.element=elem;o.calendar=this;o.index=i;
				}
				else o=this.Days[i];
				o.year=year;o.month=month;o.activity=false;
				o.css=this._css[0];
				if(i%7==sun||i%7==(sun+6)%7)o.css+=" "+this._css[1];
				if(i<day1)
				{
					o.day=day0+i;
					if(--o.month<1){o.month=12;o.year--;}
					o.css+=" "+this._css[2];
				}
				else if(i<day1+numDays)o.day=i-day1+1;
				else
				{
					o.day=i+1-(day1+numDays);
					if(++o.month>12){o.month=1;o.year++;}
					o.css+=" "+this._css[2];
				}
				if(o.day==this._now[2]&&o.month==this._now[1]&&o.year==this._now[0])
					o.css+=" "+this._css[3];
				var text=o.day,sel=this.isSelected(o.year,o.month,o.day,i);
				while(iC<iCL)
				{
					var c=this._collections[iC];
					if(!c||c[0]<o.year||(c[0]==o.year&&c[1]<o.month)||(c[0]==o.year&&c[1]==o.month&&c[2]<o.day)){iC++;continue;}
					if(c[0]==o.year&&c[1]==o.month&&c[2]==o.day)
					{o.css+=" "+this._css[4];o.activity=true;iC++;}
					break;
				}
				d=o.css;
				if(sel)d+=" "+this._css[5];
				if(id)
				{
					if(this._render(o,d,sel))continue;
					o=this._day;d=this._day.css;text=this._day.text;
				}
				else if(this._is1st&&!o.activity)continue;
				o.element.className=d;
				ig_shared.setText(o.element,text);
			}
			if(!check||e==null||!this._info)return 0;
		}
		d=this._info.getActiveDay();
		this._info.navigate(year-d.getFullYear(),month-1-d.getMonth(),0,this._id);
		return 0;
	}
	ig_WebCalendarView.prototype._onHandleEvent=function(src,e)
	{		
		if(!this._enabled)return;
		var type=e.type;
		var dbl=type=="dblclick";
		if(this.fireEvent(type, e, src))return;
		if(type.indexOf('mouse')==0){if(!ig_shared.IsSafari||!src||src.nodeName!='SELECT'){if(src.nodeName!='SELECT')ig_cancelEvent(e);return;}}
		if(!dbl&&!(type=='click'||type=='change'))return;
		var o=this.Days[15];
		var y=o.year,m=o.month;
		src = ig_findElemWithAttr(src, ig_shared.attrID);
		if(!src)return;
		var id = src.getAttribute("id_");
		if(id!=0)id=id?parseInt(id):-1;
		//drop
		if(id==504||id==506)
		{
			if(type!='change')return;
			if(id==504){if((o=this._elems[2].selectedIndex+1)==m)return;m=o;}
			else
			{
				if((o=this.year0)==null)return;
				if((o+=this._elems[3].selectedIndex)==y)return;y=o;
			}
			this.repaint(y,m,true,e);
			return;
		}
		//cal
		if(id<0)return;
		//prev/
		if(id>=500&&id<=502)this.repaint(y,m+id-501,true,e);
		//-3-request to scroll vis month
		var d,i=-3,toggle=e.ctrlKey;
		//today
		if(id==508)
		{
			y=this._now[0];m=this._now[1];d=this._now[2];toggle=false;
			if(!dbl&&this.isSelected(y,m,d))return;
			dbl=true;
		}
		else
		{
			if(id>=42)return;
			//days
			o=this.Days[id];
			y=o.year;m=o.month;d=o.day;
			if(this.isSel(o.index)){if(!dbl)return;}else toggle=false;
			if(!this._fixVis||this.Days[15].month==m)i=o.index;
		}
		if(this.minMax(y,m,d)!=null)return;
		this.select(y,m,d,i,e,dbl);//was+toggle??
	}
	
	ig_WebCalendarView.prototype.getNavigationAnimation = function()
	{
		return this._props[6];
	}
	
	ig_WebCalendarView.prototype.getSection508Compliant = function()
	{
		return this._props[7];
	}
	
	ig_WebCalendarView.prototype.getEnableKeyboardNavigation = function()
	{
		return this._props[8];
	}
	
	ig_WebCalendarView.prototype._afterSlide = function()
    {
		this._oldElem.parentNode.removeChild(this._oldElem);
		this.getContainer().scrollLeft = 0; 
		this.cv._loadJSON(this.cv._json);
		this._newElem.style.width = "100%"; 
		this._viewDiv.style.width = this._elemOrigWidth; 
		this._viewDiv.childNodes[0].style.width = "100%";
    }
	
	ig_WebCalendarView.prototype.callbackRender = function(response, context)
    {
		this.getNavigationAnimation();
		var animate = (context.operation == "Navigate" && context.CausedNavigate && this.getNavigationAnimation() > 0); 
		if(context.requestType == "html")
		{
			var html = response.replace(/\^/g, "\"");
			var elem = this.getElement();
			if(animate)
				var cloneElem = elem.cloneNode(true);
			this._setInnerHTML(html);
			
			if(animate)
			{						
				cloneElem.id += "_clone";
				if(this.viewport == null)
				{	
					this.viewport = new ig_viewport();
					this.viewport.createViewport(elem, ViewportOrientationEnum.Horizontal);
					this.viewport.transferPositionToDiv(elem,cloneElem);
					this.viewport.animate.cv = this;
					this.viewport.animate.increment = 50;
					this.viewport.animate.onEnd = this._afterSlide;
					this.viewport.animate._elemOrigWidth = elem.style.width; 
					if(elem.style.width == "")
						this.viewport.animate._elemOrigWidth = elem.offsetWidth + "px";
					
				}
				this.viewport.div.style.width = this.viewport.div.offsetWidth + "px";
				var direction =  AnimationDirectionEnum.Left;
				if(context.Days > 0 || ((context.Years * 12) + context.Months) > 0)
					direction = AnimationDirectionEnum.Right;
					
				this.viewport.animate._newElem = elem;
				elem.style.width = this.viewport.div.style.width;
				cloneElem.style.width = elem.style.width;
				this.viewport.animate._oldElem = cloneElem;
				this.viewport.scroll(cloneElem, elem, direction, this.getNavigationAnimation());
				this.viewport.animate._viewDiv = this.viewport.div; 
			}	
			
			if(this.viewport != null && cloneElem != null && (elem.offsetHeight > this.viewport.div.offsetHeight || cloneElem.offsetHeight > this.viewport.div.offsetHeight))
			{
				this.viewport.div.style.height = elem.style.height;
			}
	    }
		else if(context.requestType == "json")
		{
			var json = eval(response.replace(/\^/g, "\""));
			this._decodeProps(json);
			
			if(!animate)
				this._loadJSON(json);			
			else
				this._json = json; 
		}
    }
    ig_WebCalendarView.prototype._loadJSON = function(json)
    {
		if(typeof document=='unknown')return;
		this._is1st=true;
		var dates = json[0][5];
		var prop=dates.split(",");
		this._date=[this.intI(prop,0),this.intI(prop,1),this.intI(prop,2),-1];
		var year=this.intI(prop,3),month=this.intI(prop,4);
		this._min=this._nd(this.intI(prop,5),this.intI(prop,6),this.intI(prop,7));
		this._max=this._nd(this.intI(prop,8),this.intI(prop,9),this.intI(prop,10));
		this._now=[this.intI(prop,11),this.intI(prop,12),this.intI(prop,13)];
		this._yearFix=this.intI(prop,14);//for not gregorian
		this._day=new Object();
		this._collections = json[1];
		this._initAtr();
		var activeDay = this._info.getActiveDay();
		this.repaint(activeDay.getFullYear(), activeDay.getMonth() + 1);
		this._initActiveDay(this._info.getActiveDay());
		this._is1st=false;
    }
    ig_WebCalendarView.prototype._initActiveDay = function(activeDay)
    {
		var year = activeDay.getFullYear();
		var month = activeDay.getMonth() + 1;
		var day = activeDay.getDate();
		for(var i = 0; i < 42; i++)
		{
			if(this.Days[i].year == year && this.Days[i].month == month && this.Days[i].day == day)
			{
				this._date[0]=activeDay.getFullYear();
				this._date[1]=activeDay.getMonth() + 1;
				this._date[2]=activeDay.getDate();
				this._date[3]=i;
				break;
			}
		}
	}
    ig_WebCalendarView.prototype._setInnerHTML = function(html)
    {
		var tempElement = document.createElement("DIV");
		tempElement.innerHTML = html;
		var mainElem = this.getElement();
		var table = tempElement.childNodes[0];
		if(table.tagName == "TABLE")
		{	
			var tbody = table.childNodes[0];
			for(var i = 1; i < table.childNodes.length && tbody.tagName != "TBODY"; i++)
				tbody = table.childNodes[i];
				
			var mainTbody = mainElem.childNodes[0];
			for(var i = 1; i < mainElem.childNodes.length && mainTbody.tagName != "TBODY"; i++)
				mainTbody = mainElem.childNodes[i];
				
			if(mainTbody.tagName == "TBODY")
			{
				while(mainTbody.rows.length > 0)
					mainTbody.removeChild(mainTbody.rows[0]);
				while(tbody.rows.length > 0)
					mainTbody.appendChild(tbody.rows[0]);
			}
			
		}
	}
	
	ig_WebCalendarView.prototype._dayKeyDown = function(index, key)
	{
		var elem = null; 
		var first = 0, second; 
		if(key == 37)		// Left
		{		
			first = -1; 
			second = 42			
		}
		else if(key == 38)	// Up
		{
			first = -7;
			second = 42;
		}
		else if(key == 39) // Right
		{
			first = 1;
			second = -42;
		}
		else if(key == 40) // Down
		{
			first = 7; 
			second = -42;
		}
		else if(key == 13 || key == 32) // Enter/space
		{
			var date = new Date();
			var d = this.Days[index]; 
			date.setFullYear(d.year, d.month-1, d.day); 
			this._now[0] = d.year;
			this._now[1] = d.month;
			this._now[2] = d.day; 
			this._now[3] = d.day+1; 
			this._info.setActiveDay(date); 
		}
		if(first != 0)
		{
			index += first;
			if(index < 0)
				index += second; 
			else if(index > this.Days.length -1)
				index+= second; 
			elem = this.Days[index].element;
		}
		return elem; 
	
	}
	
	ig_WebCalendarView.prototype._onKeydown = function(src, evt)
	{
			var elem = null;
			var attr = src.getAttribute("id_");
			if(attr != null)
			{
				attr = parseInt(attr);
				if(attr == Number.NaN)
					attr = null;
			}
			var o = this.Days[15];
			var y = o.year, m = o.month;
			if(attr == null)
			{
				if(evt.keyCode != 9)
					elem = this.Days[this._now[2] + 1].element;
			}
			else if(attr < 500)
			{
				if(src != this._element && attr < 500)
					src.tabIndex = -1; 
				elem = this._dayKeyDown(attr, evt.keyCode);
			}
			else if(attr == 500 || attr == 502)
			{
				if(evt.keyCode == 32)
				{
					ig_cancelEvent(evt);
					this.repaint(y, m + attr - 501, true, evt);
				}
				else if(!evt.shiftKey && evt.keyCode != 9)
					elem = this.Days[this._now[2] + 1].element; 
			}
			else if((attr==504||attr==506) && evt.keyCode == 13)
			{
				if(attr==504){if((o=this._elems[2].selectedIndex+1)==m)return;m=o;}
				else
				{
					if((o=this.year0)==null)return;
					if((o+=this._elems[3].selectedIndex)==y)return;y=o;
				}
				this.repaint(y,m,true,evt);
			}
			if(!elem)
				return;
			elem.tabIndex = this._element.tabIndex; 
			elem.focus();  
			ig_cancelEvent(evt); 
	}
}
if(ig_csom._skipNew)return null;
return new ig_WebCalendarView(prop);
}
