/*
* ig_webGauge.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


 




function ig_getWebGaugeById(id)
{
	return ig_getWebControlById(id);
}
function ig_WebGauge(prop)
{
	this._initProp(prop);
}
function ig_CreateWebGauge(prop)
{
	if(!ig_WebControl.prototype.isPrototypeOf(ig_WebGauge.prototype))
	{
		ig_WebGauge.prototype = new ig_WebControl();
		ig_WebGauge.prototype.constructor = ig_WebGauge;
		ig_WebGauge.prototype.base = ig_WebControl.prototype;
		ig_WebGauge.prototype._initElement = function(elem)
		{
		    if (!(this._element = elem))
		    {
		        return;
		    }
		    elem.setAttribute(ig_shared.attrID, this._id);
			this._evts = ['click','dblclick','mousedown','mouseup','mousemove','mouseover','mouseout'];
			for(var i = 0; i < this._evts.length; i++)
			{
				ig_shared.addEventListener(elem, this._evts[i], ig_handleEvent);
			}
			ig_shared.addEventListener(elem, 'selectstart', ig_cancelEvent);
			ig_shared.addEventListener(elem, 'dragstart', ig_cancelEvent);
			this._asyncID = 0;
			this._asyncOk = true;
			this._mouse = '';
			this.fireEvent('initialize');
		}
		ig_WebGauge.prototype._initProp = function(prop)
		{
		    if(!prop)
		    {
				return;
			}
			this._initControlProps(prop);
			var id = this._clientID;
			this.init(id);
			this._initElement(ig_csom.getElementById(id));
		}
		
		ig_WebGauge.prototype._updateMouse = function(clear)
		{
			if((this._mouse || clear) && this._mouse != '')
				this.updateControlState('ValueFromClient', this._mouse);
			this._mouse = '';
		}
		
		ig_WebGauge.prototype._onHandleEvent = function(src,e)
		{
			if (!this._element)
			{
			    this._initElement(ig_csom.getElementById(this._clientID));
			}
			var elem = this._element;
			if(!e || src != elem)return;
			var i = this._evts.length, type = e.type;
			if(type == 'mousedown')
			{
				ig_cancelEvent(e);
				this.mouseDown = true;
			}
			else if(type != 'mousemove')
				this.mouseDown = false;
			while(i-- > 0) if(this._evts[i] == type)
				break;
			if(i < 0)return;
			if(!this._bdr)
				this._bdr = {t:this._intVal(elem, 'borderTopWidth'),r:this._intVal(elem, 'borderRightWidth'),l:this._intVal(elem, 'borderLeftWidth'),b:this._intVal(elem, 'borderBottomWidth')};
			var x = e.offsetX, y = e.offsetY;
			if(x == null)
			{
				
				x = e.layerX - this._bdr.l;
				y = e.layerY - this._bdr.t;
			}
			i = this.mouseDown ? 8 : 0;
			if(e.shiftKey) i |= 1;
			if(e.ctrlKey) i |= 2;
			if(e.altKey) i |= 4;
			var w = elem.offsetWidth - this._bdr.l - this._bdr.r, h = elem.offsetHeight - this._bdr.t - this._bdr.b;
			
			this._mouse = x + ':' + y + ':' + w + ':' + h + ':' + i + ':' + type;
			
			this.fireEvent(e.type, e, x, y);
			
			if(this._postAsync == 1)
				this.refresh(1);
			
			else if(this._postRequest == 1)
			{
				this._updateMouse(true);
				this.fireServerEvent('');
			}
			else if(type == 'dblclick')
				this._updateMouse();
			this._mouse = '';
		}
		
		ig_WebGauge.prototype._intVal = function(elem, p)
		{
			var val = null, s = elem.currentStyle, s0 = elem.style, win = document.defaultView;
			if(!s)
			{
				if(!win)
					win = window;
				if(win.getComputedStyle)
					s = win.getComputedStyle(elem, '');
			}
			if(!s)
				s = s0;
			try{val = s[p];}catch(i)
			{
				s = s0;
				try{eval('val=s.'+p)}catch(i){}
			}
			if(!val && s.getPropertyValue)
				val = s.getPropertyValue(p);
			p = val ? val.charCodeAt(0) : 0;
			return (p == 45 || (p > 47 && p < 58)) ? parseInt(val) : 0;
		}
		
		ig_WebGauge.prototype.refresh = function(wait)
		{
			
			if(wait == 1 && (!this._asyncOk || this._bufImage) && (this._asyncTime && this._asyncTime + 5000 < (new Date()).getTime()))
			{
				
				this._mouseOld = this._mouse;
				return;
			}
			
			if(this._mouseOld)
				this._mouse = this._mouseOld;
			this._mouseOld = null;
			
			this._asyncTime = (new Date()).getTime();
			
			this._updateMouse(true);
			if(++this._asyncID > 999999999)this._asyncID = 0;
			var clientContext = {id: this._asyncID};
			var serverContext = {operation: 'Refresh'};
			var smartCallback = new ig_SmartCallback(clientContext, serverContext, null, this._uniqueID, this);
			smartCallback.execute();
		}
		
		ig_WebGauge.prototype._deleteMe = function(){this._imgAct(true);}
		
		ig_WebGauge.prototype._imgEvt = function()
		{
			for(var id in ig_all)
				if((id = ig_all[id]) != null) if(id._imgAct)
					id._imgAct();
		}
		
		ig_WebGauge.prototype._imgAct = function(src)
		{
			var im = this._bufImage, id = this._clientID + '-' + this._asyncID;
			if(src)
			{
				if(im)
					im.onreadystatechange = im.onload = this._bufImage = null;
				if(src.length)
				{
					im = this._bufImage = new Image();
					this._imgSrc = im.src = src;
					
					im.id = id;
					window.setTimeout("try{ig_all['" + this._id + "']._imgAct(false);}catch(i){}", 1000);
					im.onreadystatechange = im.onload = this._imgEvt;
				}
				return;
			}
			
			if(!im || !(src === false || im.complete || im.readyState == 'complete') || im.id != id)
				return;
			
			this._element.src = this._imgSrc;
			im.onreadystatechange = im.onload = this._bufImage = null;
			if(this._mouseOld)
				this.refresh(1);
		}
		ig_WebGauge.prototype.callbackRender = function(response, context)
		{
			var props = eval(response.replace(/\^/g, "\""));
			this._decodeProps(props);
			this._asyncOk = context.id <= this._asyncID;
			
			if(context.id < this._asyncID)
			{
				return;
			}
			if(typeof(Image) != 'undefined')
			{
				this._imgAct(props[0]);
			}
			else
			{
				this._element.src = props[0];
			}
			if(props[1])
			{
				this.updateControlState('AsyncValues', props[1]);
			}
			if (props[2] && props[2].substr && props[2].substr(0, 2) == "aR")
			{
			    this.setRefreshInterval(parseInt(props[2].substr(3, props[2].length - 3)));
			}
			
			if(this._mouseOld)
				this.refresh(1);
		}
		ig_WebGauge.prototype.setRefreshInterval = function(refreshInterval)
		{
		    if (!this.controlState || this.controlState["RefreshInterval"] != refreshInterval)
		    {                
		        this.updateControlState("RefreshInterval", refreshInterval);
		        if (this.interval)
		        {
		            window.clearInterval(this.interval);
		        }
		        if (refreshInterval > 0)
		        {
		            this.interval = window.setInterval("try{o" + this._clientID + ".refresh();}catch(i){}", refreshInterval * 1000);
		        }
		    }
		}
	}
	return new ig_WebGauge(prop);   
}
