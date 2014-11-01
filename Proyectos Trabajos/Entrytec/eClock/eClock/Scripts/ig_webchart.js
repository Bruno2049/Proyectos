/*
* ig_webchart.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/










// Global array to relate Elements with Objects.
var ID2OBJECT = new Array();


// IGBrowser class implements the basic browser independent functionality
function IGBrowser() 
{
	// Public Properties
	this.ScriptVersion		= "<SUCCESSFUL_COMPONENT_VERSION>"; 
	this.AgentName			= navigator.userAgent.toLowerCase();
	this.IsFirefox          = this.AgentName.indexOf("firefox") != -1;
	this.MajorVersionNumber = parseInt(navigator.appVersion);
	this.IsDom				= (document.getElementById) ? true : false;
	this.IsSafari           = this.AgentName.indexOf("safari") != -1;
	//in most cases we want to treat safari like firefox, so introducing this flag.
	this.IsFirefoxOrSafari  = this.IsFirefox || this.IsSafari;
	this.IsNetscape			= (document.layers ? true : false);
	this.IsNetscape4Plus	= (this.IsNetscape && this.MajorVersionNumber >=4) ? true : false;
	this.IsNetscape6		= !this.IsSafari && (this.IsDom && navigator.appName == "Netscape");
	this.IsOpera			= this.AgentName.indexOf('opera') != -1;
	this.IsOpera9Plus       = this.IsOpera && this.MajorVersionNumber >= 9;
	this.IsMac				= (this.AgentName.indexOf("mac")!=-1);
	this.IsIE				= (document.all ? true : false);
	this.IsIE4				= (document.all && !this.IsDom) ? true : false;
	this.IsIE4Plus			= (this.IsIE && this.MajorVersionNumber >= 4) ? true : false;
	this.IsIE5				= (document.all && this.IsDom) ? true : false;
	this.IsIE8Plus          = this.AgentName.indexOf("msie") != -1 && navigator.appVersion && navigator.appVersion.indexOf("MSIE") != -1 && parseInt(navigator.appVersion.split("MSIE")[1]) >= 8;
    this.IsIE9              = this.AgentName.indexOf("msie 9") != -1;
	this.IsWin              = ((this.AgentName.indexOf("win") != -1) || (this.AgentName.indexOf("16bit") != -1));
	this.IsXHTML            = document.compatMode=="CSS1Compat";
	this.ID					= "IGB";	

	//Emulate 'apply' if it doesn't exist.
	if ((typeof Function != 'undefined')&&(typeof Function.prototype != 'undefined')&&(typeof Function.apply != 'function'))
	{
	    Function.prototype.apply = function(obj, args)
	    {
			var result, fn = 'ig_apply';
			while(typeof obj[fn] != 'undefined') fn += fn;
			obj[fn] = this;
			var length=(((ig_csom.isArray(args))&&(typeof args == 'object'))?args.length:0);
			switch(length)
			{
				case 0:
					result = obj[fn]();
					break;
				default:
					for(var item=0, params=''; item<args.length;item++)
					{
						if(item!=0) params += ',';
						params += 'args[' + item +']';
					}
					result = eval('obj.'+fn+'('+params+');');
					break;
			}
			this.Dispose(obj[fn]);
			return result;
		}
	}
	this.Dispose = function(obj)
	{
		if (this.IsIE && this.IsWin)	
			for(var item in obj)
			{
				if(typeof(obj[item])!="undefined" && obj[item]!=null && !obj[item].tagName && !obj[item].disposing && typeof(obj[item])!="string")
				{
					try 
					{
						obj[item].disposing=true;
						ig_dispose(obj[item]);
					} 
					catch(e1) {;}
			}
			try{delete obj[item];}catch(e2){;}
		}
	}

	this.GetObject = function(id, doc) 
	{
		var i,x;  
		if (!doc) doc=document; 
		if (!(x=doc[id])&&doc.all) x=doc.all[id]; 
		if (!x && document.getElementById) x=document.getElementById(id); 
		return x;
	}

	this.GetHeight = function(obj) 
	{ 
		if (this.IsNetscape) 
		{ 
			return (obj.height) ? obj.height : obj.clip.height; 
		} 
		else if (this.IsOpera && !this.IsOpera9Plus)
		{
		    return obj.style.pixelHeight;
		}
		else
		{
		    return obj.offsetHeight;
		}
    }

	this.SetHeight = function(obj,h) 
	{ 
		if ((this.IsFirefoxOrSafari || IGB.IsIE9) && document.documentElement) h += "px";
		if (this.IsNetscape) 
		{
			if (obj.clip) obj.clip.bottom=h; 
		} else if (this.IsOpera) obj.style.pixelHeight=h;
		else obj.style.height=h; 
	}

	this.GetWidth = function(obj) 
	{ 
		if (this.IsNetscape) 
		{ 
			return (obj.width) ? obj.width : obj.clip.width; 
			
		}
		else if (this.IsOpera && !this.IsOpera9Plus)
		{
		    return obj.style.pixelWidth;
		} 
		return obj.offsetWidth; 
	}

	this.SetWidth = function(obj,w) 
	{
	    if ((this.IsFirefoxOrSafari || IGB.IsIE9) && document.documentElement) w += "px";
		if (this.IsNetscape) 
		{
			if (obj.clip) obj.clip.right=w;
		}else if (this.IsOpera)obj.style.pixelWidth=w;
		else obj.style.width=w; 
	}

	this.GetX = function(obj) 
	{ 
		var x=(this.IsNetscape)? obj.left:(this.IsOpera)? obj.style.pixelLeft:obj.offsetLeft; 
		return x;
	}

	this.SetX = function(obj, x) 
	{
	    if ((this.IsFirefoxOrSafari || IGB.IsIE9) && document.documentElement) x += "px";
		(this.IsNetscape)? obj.left=x:(this.IsOpera)? obj.style.pixelLeft=x:obj.style.left=x; 
	}

	this.GetY = function(obj) 
	{  
		var y=(this.IsNetscape)? obj.top:(this.IsOpera)? obj.style.pixelTop:obj.offsetTop; 
		return y;
	}

	this.SetY = function(obj, y) 
	{
	    if ((this.IsFirefoxOrSafari || IGB.IsIE9) && document.documentElement) y += "px";
		(this.IsIE||this.IsDom)? obj.style.top=y:(this.IsNetscape)? obj.top=y:obj.style.pixelTop=y; 
	}

	this.GetPageX = function(obj) 
	{ 
        if (document && document.getBoxObjectFor)
        {
            return document.getBoxObjectFor(obj).x;
        }
        else if (this.IsFirefox) 
        {
			return IGB.getBoxObjectFor(obj).x;
        }
		else if (this.IsNetscape) 
		{ 
			var x = (obj.pageX) ? obj.pageX : obj.x; 
			return x; 
		} 
		else if (this.IsOpera && !this.IsOpera9Plus)  
		{  
			var x = 0; 
			while(eval(obj)) 
			{ 
				x += obj.style.pixelLeft; 
				obj = obj.offsetParent; 
			} 
			return x; 
		} 
		else 
		{ 
			var x=0; 
			var relativePos = -1;
			while(eval(obj)) 
			{ 
				if (this.IsNetscape6)
				{
					if (obj.style.position=="relative")
					{
						relativePos = x;
					}
					else if (obj.style.position=="absolute")
					{
						relativePos = -1;
					}
				}
				x += obj.offsetLeft; 
				obj = obj.offsetParent; 
			} 
			if (this.IsNetscape6&&relativePos!=-1)
			{
				return relativePos;
			}
			return x; 
		}
    }
    
	this.GetPageY = function(obj) 
	{ 
        if (document && document.getBoxObjectFor)
        {
            return document.getBoxObjectFor(obj).y;
        }
        else if (this.IsFirefox) 
        {
			return IGB.getBoxObjectFor(obj).y;
        }
		else if (this.IsNetscape) 
		{ 
			var y = (obj.pageY) ? obj.pageY : obj.y; 
			return y; 
		} 
		else if (this.IsOpera && !this.IsOpera9Plus) 
		{  
			var y = 0; 
			while(eval(obj)) 
			{ 
				y += obj.style.pixelTop; 
				obj = obj.offsetParent; 
				
			} 
			return y; 
		}
		else 
		{ 
			var y=0; 
			while(eval(obj)) 
			{ 
				if (this.IsNetscape6)
				{
					if (obj.style.position=="relative")
					{
						relativePos = y;
					}
					else if (obj.style.position=="absolute")
					{
						relativePos = -1;
					}
				}
				y += obj.offsetTop; 
				obj = obj.offsetParent; 
			} 
			if (this.IsNetscape6 && relativePos != -1)
			{
				return relativePos;
			}
			return y; 
		}
    }

    // [GT 03/05/2010:Bug28948] getBoxObjectFor is removed from Firefox 3.6+
    
    this.getBoxObjectFor = function(elem) {
        if (document && document.getBoxObjectFor) {
            return document.getBoxObjectFor(elem);
        }

        var ok = 0, r = new Object(), body = window.document.body, e = elem;
        var z = e.offsetWidth, pe = e;
        if (z == null || z < 2)
            z = 70;
        r.w = z;
        z = e.offsetHeight;
        if (z == null || z < 2)
            z = 18;
        r.h = z;
        r.x = 1;
        r.y = 1;
        while (e != null) {
            if (ok < 1 || e == body) {
                z = e.offsetLeft;
                if (z) {
                    r.x += z;

                    if (this.IsIE && !this.IsXHTML) {
                        var borderWidthX = parseInt(e.currentStyle.borderLeftWidth);
                        if (!isNaN(borderWidthX) && e.tagName != "TD")
                            r.x += borderWidthX;
                    }
                }
                z = e.offsetTop;
                if (z) {
                    r.y += z;

                    if (this.IsIE && !this.IsXHTML) {
                        var borderWidthY = parseInt(e.currentStyle.borderTopWidth);
                        if (!isNaN(borderWidthY) && e.tagName != "TD")
                            r.y += borderWidthY;
                    }
                }
            }
            if (e.nodeName == "HTML")
                body = e;
            if (e == body)
                break;
            z = e.scrollLeft;
            if (z == null || z == 0)
                z = pe.scrollLeft;
            if (z != null && z > 0)
                r.x -= z;
            z = e.scrollTop;
            if (z == null || z == 0)
                z = pe.scrollTop;
            if (z != null && z > 0)
                r.y -= z;
            pe = e.parentNode;
            e = e.offsetParent;
            if (pe.tagName == "TR")
                pe = e;
            if (e == body && pe.tagName == "DIV") {
                e = pe;
                ok++;
            }
        }
        try {

            if (!document.elementFromPoint || window.frameElement || body.currentStyle && (body.currentStyle.borderWidth == "0px" || body.currentStyle.borderStyle == "none"))
                return r;
        }
        catch (e) {
            return r;
        }
        var i = 1, x = r.x, y = r.y, x0, y0;
        if (this.IsXHTML)
            x0 = document.body.parentNode.scrollLeft;
        else
            x0 = document.body.scrollLeft;
        if (this.IsXHTML)
            y0 = document.body.parentNode.scrollTop;
        else
            y0 = document.body.scrollTop;
        while (++i < 16) {
            z = (i > 2) ? ((i & 2) - 1) * (i & 14) / 2 * 5 : 2;
            e = document.elementFromPoint(x + z - x0, y + z - y0);
            if (!e || e == elem || e.parentNode == elem || e.parentNode.parentNode == elem)
                break;
        }
        if (i > 15 || !e)
            return r;
        x += z;
        y += z;
        i = 0;
        z = 0;
        while (++i < 22) {
            if (z == 0)
                x--;
            else
                y--;
            e = document.elementFromPoint(x - x0, y - y0);
            if (!e || i > 20)
                return r;
            if (e != elem && e.parentNode != elem && e.parentNode.parentNode != elem)
                if (z > 0)
                break;
            else {
                i = z = 1;
                x++;
            }
        }
        r.x = x - 1;
        r.y = y;
        return r;
    };

	this.SetPos = function(obj,x,y) 
	{ 
		this.SetX(obj,parseInt(x));
		this.SetY(obj,parseInt(y)); 
	}
	
	this.SetPosRelative = function(obj,x,y) 
	{ 
		this.SetX(obj,parseInt(this.GetPageX(obj))+parseInt(x));
		this.SetY(obj,parseInt(this.GetPageY(obj))+parseInt(y)); 
	}

	this.SetZValue = function(obj,z) 
	{ 
		if (this.IsNetscape)obj.zIndex=z;
		else obj.style.zIndex=z; 
	}
	

	this.ShowObject = function(obj,disp) 
	{ 
		(this.IsNetscape)? '':(!disp)? obj.style.display="inline":obj.style.display=disp;
		(this.IsNetscape)? obj.visibility='show':obj.style.visibility='visible';  
	}

	this.HideObject = function(obj,disp) 
	{ 
		(this.IsNetscape)? '':(arguments.length!=2)? obj.style.display="none":obj.style.display=disp;
		(this.IsNetscape)? obj.visibility='hide':obj.style.visibility='hidden';  
	}

	this.SetStyle = function(obj,s,v) 
	{ 
		if (this.IsIE5||this.IsDom) eval("obj.style."+s+" = '" + v +"'"); 
	}

	this.GetStyle = function(obj,s) 
	{ 
		if (this.IsIE5||this.IsDom) return eval("obj.style."+s); 
	}

	this.AddEventListener = function (o,e,f,c)
	{ 
		if(o.addEventListener)o.addEventListener(e,f,c);
		else if(o.attachEvent)o.attachEvent("on"+e,f);else eval("o.on"+e+"="+f)
	}
	
	this.AddEventListener = function(obj,eventName,callbackFunction,flag)
	{ 
		
		if (obj.addEventListener) 
		{
			obj.addEventListener(eventName,callbackFunction,flag);
		}
		else if (obj.attachEvent) 
		{
			obj.attachEvent("on"+eventName,callbackFunction);
		}
		else 
		{
			eval("obj.on"+eventName+"="+callbackFunction);
		}
	}
	
	this.WriteHTML = function(obj,html) 
	{
		
		if (this.IsNetscape)
		{
			var doc=obj.document;
			doc.write(html);
			doc.close();
			return false;
		}
		if (obj.innerHTML) 
		{
		    obj.innerHTML = html; 
        }
	}

    this.SetXClientOverflowSafe = function (obj, x) 
    {  
	    var objWidth = IGB.GetWidth(obj);
	    var objRight = objWidth + x;
	    var clientWidth = IGB.GetClientWidth();
	    if ((clientWidth - objRight) > (x - objWidth - 5))
	        this.SetX(obj, x);
	    else
	        this.SetXScrollContainerSafe(obj, x - objWidth - 5);
	}

	this.SetXOverflowSafe=function(obj, x, locx, container)
	{
		var objR = IGB.GetWidth(obj) + locx;
		var containerW = IGB.GetWidth(container);
		if (objR <= containerW)
			this.SetXScrollContainerSafe(obj, x);
		else
			this.SetXScrollContainerSafe(obj, x - (objR - containerW));
	}

    this.SetYClientOverflowSafe = function (obj, y) 
    {
	    var objH = IGB.GetHeight(obj);
	    var objT = objH + y + 23; // plus 23, as 20 offset was already added to y
	    var clientH = IGB.GetClientHeight();
	    if ((clientH - objT) > (y - objH - 25))
	        this.SetYScrollContainerSafe(obj, y);
	    else
	        this.SetYScrollContainerSafe(obj, y - objH - 25);
	}

	this.SetYOverflowSafe=function(obj, y, locy, container)
	{
		var objT = IGB.GetHeight(obj) + locy;
		var containerH = IGB.GetHeight(container);
		if (objT <= containerH)
			this.SetYScrollContainerSafe(obj, y);
		else
			this.SetYScrollContainerSafe(obj, y - (objT - containerH));
	}

    this.GetClientWidth = function () 
    {
	    var theWidth = 0;
	    if (window.innerWidth) 
        {
	        theWidth = window.innerWidth;
	    }
	    else if (document.documentElement && document.documentElement.clientWidth) 
        {
	        theWidth = document.documentElement.clientWidth;
	    }
	    else if (document.body) 
        {
	        theWidth = document.body.clientWidth;
	    }

	    return theWidth;
	}

	this.GetClientHeight = function () 
    {
        var theHeight = 0;
        if (window.innerHeight) //Netscape and Opera
        {
            theHeight = window.innerHeight;
        }
        else if (document.documentElement && document.documentElement.clientHeight) 
        {
            theHeight = document.documentElement.clientHeight;
        }
        else if (document.body) 
        {
            theHeight = document.body.clientHeight; 
        }

        return theHeight;
    }
	
	this.SetXScrollContainerSafe = function(obj, x)
	{
		var hSC = this.GetHScrolledContainer(obj);
		if (hSC != null)
			this.SetXScrollContainerAdjusted(obj, x, hSC);
		else
			this.SetX(obj, x);
    }

	this.SetYScrollContainerSafe = function(obj, y)
	{
		var vSC = this.GetVScrolledContainer(obj);
		if (vSC != null)
			this.SetYScrollContainerAdjusted(obj, y, vSC);
		else
			this.SetY(obj, y);
    }

	this.GetVScrolledContainer = function(obj)
	{
		if (obj.scrollTop > 0 && obj.tagName != 'BODY')
			return obj;
		else if (obj.offsetParent != null)
			return this.GetVScrolledContainer(obj.offsetParent);
		else
			return null;
	}
	this.GetHScrolledContainer = function(obj)
	{
		if (obj.scrollLeft > 0 && obj.tagName != 'BODY')
			return obj;
		else if (obj.offsetParent != null)
			return this.GetHScrolledContainer(obj.offsetParent);
		else
			return null;
	}
	this.SetXScrollContainerAdjusted = function(obj, x, container)
	{
		this.SetX(obj, x + container.scrollLeft);
	}
	this.SetYScrollContainerAdjusted = function(obj, y, container)
	{
		this.SetY(obj, y + container.scrollTop);
	}

	this.InsertHTML = function(obj,html,where) 
	{
		
		if (this.IsOpera) return;
		if (obj.insertAdjacentHTML) 
		{ 
			obj.insertAdjacentHTML(where,html);
			return;
		}
		if (this.IsNetscape) 
		{
			this.WriteHTML(obj,html);
			return;
		}
		
		// Mozilla
		var ref = obj.ownerDocument.createRange();
		ref.setStartBefore(obj);
		
		var fragment = ref.createContextualFragment(html);
		
		this.DOMInsertObj(obj,where,fragment);	
	}

	this.DOMInsertObj  = function(obj, where, node) 
	{
		switch (where)
		{
			case 'beforeBegin':
				obj.parentNode.insertBefore(node,obj)
				break;
			case 'afterBegin':
				obj.insertBefore(node,obj.firstChild);
				break;
			case 'beforeEnd':
				obj.appendChild(node);
				break;
			case 'afterEnd':
				if (obj.nextSibling)
				{
					obj.parentNode.insertBefore(node,obj.nextSibling);
				} 
				else 
				{
					obj.parentNode.appendChild(node)
				}
				break;
		}
	}

	ID2OBJECT["IGB"] = this;

	this.Listener	  = new Array();
	this.AddListener  = function(type, function_ref) 
	{
		this.Listener[type] = function_ref;
	}

	this.CurrentX = 0;
	this.CurrentY = 0;

	this.GlobalHandleMouseMove = function(evt)
	{
		if (this.IsNetscape4Plus)
		{
			this.CurrentX=evt.pageX;
			this.CurrentY=evt.pageY;
		}
		else if (this.IsNetscape6)
		{
			this.CurrentX=evt.clientX;
			this.CurrentY=evt.clientY;
		}
		else if (this.IsIE5)
		{
			this.CurrentX=event.clientX;
			this.CurrentY=event.clientY;
		}

		IGProcessEventsObjects("onmousemove", this);

		return false;
	}

	// Gets the function name from the function reference.
	this.FunctionName = function(f)
	{

		if (f==null)
		{
			return "anonymous";
		}
		var s=f.toString().match(/function\s*(\w*)/)[1];
		if((s==null)|| (s.length==0)) return "anonymous";
		return s;
	}

	// DecodeArguments, spilt and url-decode it.
	// string is split at "&" and url decoded, all the items are put in an array.
	this.DecodeArguments=function(inputString) 
	{
		var splitArray = inputString.split('&');
		for (i = 0; i < splitArray.length; i++)
		{
		    if (decodeURI)
		    {
		        splitArray[i] = decodeURI(splitArray[i]);
		    }

		    splitArray[i] = unescape(splitArray[i].replace(/\+/g, " "));		    
		}
		return splitArray;
	}
}

// Util objects
function IGRectangle(x, y, width, height)
{
	this.X=x;
	this.Y=y;
	this.Width=width;
	this.Height=height;

	this.Inside = function(x,y)
	{
		return (x >=this.X && y >= this.Y && x <=(this.X+this.Width) && y <=(this.Y+this.Height));
	}
}

function IGPoint(x, y) 
{
	this.X = x;
	this.Y = y;
} 

var IGB = new IGBrowser();

function IGProcessEventsObjects(type, sender_object)
{
	if (eval("sender_object.Listener")) 
	{
		var function_ref = sender_object.Listener[type];
		if (function_ref != null) 
		{
			function_ref(type, null, sender_object);
		}
	}
}

function IGProcessEvents(type, sender_element)
{
	var sender_object = ID2OBJECT[sender_element.id];
	IGBubbleEvent(type, sender_element, sender_object);

	if (eval("sender_object.Listener")) 
	{
		var function_ref = sender_object.Listener[type];
		if (function_ref != null) 
		{
			function_ref(type, sender_element, sender_object);
		}
	}
}
function IGBubbleEvent(type, sender_element, sender_object)
{
	if (eval("sender_object."+type)) 
	{
		if (eval("sender_object."+type+"(sender_element, sender_object)"))
		{
			var parent_ref = sender_object.Parent;
			if (parent_ref != null) 
			{
				IGBubbleEvent(type, sender_element, parent_ref);
			}
		}
	}
}

// Repeating infrastructure related. This used to implement repeating functionality in controls such as scrollbar and fader.
// Please note that this repeating logic can only handle one repeating at a time. Call to Cancel repeating will cancel any
// other repeating in progress. This logic can be extended to handle more than one repeating but since chart uses are limited
// not implemented. -KV
var RepeatingDelegate=null;
var DelegateParameter=null;
var DelegateeObject=null;
var TimerId= null;

// Linear decay repeating
//    ^
//    |   unit: msec
// 70 +
//    | \
//    |   \
//  5 +     \____
//    +-----+---->
//    0     552 

function GetDelay(nextTimeOut)
{
	if (nextTimeOut == -1)
	{
		nextTimeOut = 70;
	}
	else
	{
		nextTimeOut-=5;
		if (nextTimeOut<5) nextTimeOut = 5;
	}
	return nextTimeOut;
}

// Repeating handler, called upon each timeout
function RepeatingHandler(nextTimeOut) 
{
	nextTimeOut = GetDelay(nextTimeOut);

	TimerId=setTimeout("RepeatingHandler("+nextTimeOut+")", nextTimeOut);

	if (RepeatingDelegate!=null) 
	{
		RepeatingDelegate.apply(DelegateeObject, DelegateParameter);
	}
}

// setup start and stop repeating
function Repeating(trueToStartfalseToEnd, delegate, parameters, ThisObject) 
{
	if (trueToStartfalseToEnd == true) 
	{
		RepeatingDelegate = delegate;
		DelegateParameter = parameters;
		DelegateeObject   = ThisObject;
		RepeatingHandler(-1); 
	}
	else 
	{
		if (TimerId)
		{
			clearTimeout(TimerId);
			TimerId = null;
		}

		RepeatingDelegate = null;
		DelegateParameter = null;
	}
}

// Fader class is used to create fading effect on given element. 
// This animates the Opacity Style in given interval.
// Very Generic Object can be used on pretty much any element ref.
function Fader()
{
	ID2OBJECT[this.ID]= this; // save id's ref.
	this.Parent		  = null; // for parent child relationship
	this.Listener	  = new Array();
	this.AddListener  = function(type, function_ref) 
	{
		// nothing supported yet
	}

	this.FaderOpacity = 0; 

	this.FaderStep=function(id_ref, min, max, delta)
	{
		if( (this.FaderOpacity<=max) && (this.FaderOpacity>=min) )
		{
			this.FaderOpacity += delta;
			if (IGB.IsIE4 || IGB.IsIE5) id_ref.style.filter="alpha(opacity="+this.FaderOpacity+")";
			if (IGB.IsNetscape6) id_ref.style.MozOpacity=this.FaderOpacity/100;
		} 
		else
		{
			Repeating(false);
		}
	}

	// Starts the fader
	// animates the opacity of an element from [min..max] with delta.
	// make sure delta is not equal to zero other wise it will never stop repeating
	this.Start=function(id_ref, min, max, delta)
	{
		this.FaderOpacity = min;
		Repeating(true, this.FaderStep, [id_ref, min, max, delta], this);
	}

	this.End=function()
	{
		Repeating(false);
	}
}


/// Bounce event to right object.
function Bounce(evt, id, func_name, paramArray) 
{
	var this_ref = ID2OBJECT[id];  
	var fn = func_name;
	if (this_ref)
	{
		if (fn)
		{
			fn = func_name;
		}
		else
		{
			fn = "on"+evt.type;
		}

		eval("this_ref."+fn+"(evt, id, paramArray)");
	}
}


// Infragistics Web Chart window viewer Script 
 
function IGWindowViewer(id, imageId, vuid, srcBounds, destBounds) 
{
	this.SourceBounds = srcBounds;
	this.DestBounds	= destBounds;
	this.ImageId	= imageId;
	this.VUId		= vuid;

	ID2OBJECT[this.ID]= this; // save id's ref.
	this.Parent		  = null; // for parent child relationship
	this.HTML		  = "";   // debugging purposes
	this.Listener	  = new Array();
	this.AddListener  = function(type, function_ref) 
	{
		// does nothing
	}

	this.MoveBy=function(x,y)
	{
		this.SourceBounds.X += x;
		this.SourceBounds.Y += y;
		this.Render();
	}

	this.Render=function()
	{
		var imgref = IGB.GetObject(this.ImageId);
		var vuwref = IGB.GetObject(this.VUId);
		
		// Viewing transform (scaling=1)
		//  1) Translate (-x, -y); img
		//  3) Translate (x1, y1); vu

		IGB.SetX(imgref, - this.SourceBounds.X);
		IGB.SetY(imgref, - this.SourceBounds.Y);

		var w = this.SourceBounds.Width;
		var h = this.SourceBounds.Height;

		if ((IGB.IsFirefoxOrSafari || IGB.IsIE9) && document.documentElement)
		{
			w += "px";
			h += "px";
		}
		
		IGB.SetStyle(vuwref, "width", w);	
		IGB.SetStyle(vuwref, "height", h);
		IGB.SetStyle(vuwref, "overflow", "hidden");	

		IGB.SetX(vuwref, this.DestBounds.X);
		IGB.SetY(vuwref, this.DestBounds.Y);
	}
}

// Infragistics Web Chart cross hair Script 

function IGCrossHair(id, toggleOnClick)
{
	this.ID					= id;
	this.ToggleOnClick		= toggleOnClick;
	this.Visible			= false;

	// must set properties
	this.SpanImageObject	= null;
	this.HairHorizontal		= null;
	this.HairVertical		= null;

	ID2OBJECT[this.ID]= this; // save id's ref.
	this.Parent		  = null; // for parent child relationship
	this.HTML		  = "";   // debugging purposes
	this.Listener	  = new Array();
	this.AddListener  = function(type, function_ref) 
	{
		// no events supported yet.
	}

	this.Render = function(b) 
	{
	    // [DN 8/28/2007:BR25943] reposition xhair in firefox if offsetParent is an element other than the chart (typically it will be the body if it's not the chart)
	    var xOffset = 0, yOffset = 0, chartElement = document.getElementById(this.Parent.ID), xHairOffsetParent = this.HairHorizontal.offsetParent;
	    if (IGB.IsFirefoxOrSafari && chartElement && xHairOffsetParent && xHairOffsetParent.tagName.toLowerCase() == "body" && xHairOffsetParent != chartElement)// && xHairOffsetParent != chartElement.offsetParent) 
	    {
	        // [GT 03/05/2010:Bug28948] getBoxObjectFor is removed from Firefox 3.6+	                   
            var boxObj = IGB.getBoxObjectFor(chartElement);
            if (boxObj) 
            {
                xOffset = boxObj.x;
                yOffset = boxObj.y;
            }	        
	    }
	    IGB.SetX(this.HairHorizontal, b.X + xOffset);
	    IGB.SetY(this.HairVertical, b.Y + yOffset);
	    IGB.SetWidth(this.HairHorizontal, b.Width);
	    IGB.SetHeight(this.HairHorizontal, 1);
	    IGB.SetHeight(this.HairVertical, b.Height);
	    IGB.SetWidth(this.HairVertical, 1);
	}

	this.Update = function(x, y, baseImageRef)
	{
		if (this.Visible)
		{
			IGB.ShowObject(this.HairHorizontal);
			IGB.ShowObject(this.HairVertical);

			var chart = IGB.GetObject(this.Parent.ID);
			var newZIndex = this.Parent.getMaxZIndexOfParents(chart) + 5;
			IGB.SetZValue(this.HairVertical, newZIndex);
			IGB.SetZValue(this.HairHorizontal, newZIndex);
			window.status = newZIndex;
		}
		else 
		{
			IGB.HideObject(this.HairHorizontal);
			IGB.HideObject(this.HairVertical);
		}
	    
		if (x && y)
		{
		    IGB.SetYScrollContainerSafe(this.HairVertical, IGB.GetPageY(baseImageRef) + 6);
		    IGB.SetXScrollContainerSafe(this.HairVertical, x);

		    IGB.SetXScrollContainerSafe(this.HairHorizontal, IGB.GetPageX(baseImageRef) + 6);
		    IGB.SetYScrollContainerSafe(this.HairHorizontal, y);
		}
	}
}

// Infragistics Web Chart Scrollbar Script 
 
function IGScrollBar(id, width, height, scrollerLength, url, orientation, uniqueId) 
{
	this.Orientation = orientation==null?'horizontal':orientation;
	this.ID			 = id;
	this.UniqueID    = uniqueId;
	this.ImageURL	 = url;
	this.Width		 = width;
	this.Height		 = height;
	this.ScrollerLen = scrollerLength;
	
	// [KV 12/3/2004, 10:37 AM] BR01044 Scrollbar images do not appear when 
	// chart is on a user control. Since every scrollbar has unique id, and 
	// it uses images from that id. Following provides an alternative way to
	// to use the images.
	this.UseImageFromId = this.ID
	this.Location	 = new IGPoint(0,0);

	this.Minimum	 = 0;
	this.Maximum	 = 100;
	this.Value		 = 0;
	this.SmallChange  = 5;
	this.LargeChange  = 15;

	ID2OBJECT[this.UniqueID] = this; // save id's ref.
	this.Parent		  = null; // for parent child relationship
	this.HTML		  = "";   // debugging purposes
	this.Listener	  = new Array();
	this.Where        = null;
	
	this.AddListener  = function(type, function_ref) 
	{
		this.Listener[type] = function_ref; // save listener into array.
	}

    this.SetLocation = function()
    {
        var where_ref;        
        if (this.Where)
        {
            where_ref = IGB.GetObject(this.Where); 
        }
        if (!where_ref)
        {
            return;
        }

        // [IK 13/05/10] this logic has been replaced by modifying the HTML template for the WebChart by insering a div element --> SR_CONTROL_WITH_SCROLL_V20062
        // [DN 8/28/2007:BR25945] reposition scrollbar in firefox if offsetParent is an element other than the chart (typically it will be the body if it's not the chart)
//        var xOffset = 0, yOffset = 0, chartElement = document.getElementById(this.Parent.ID), tableElement = document.getElementById(this.Parent.ID + "_table");
//        if (IGB.IsFirefoxOrSafari && chartElement && tableElement && where_ref.offsetParent != chartElement && where_ref.offsetParent != chartElement.offsetParent)
//        {
//            if (tableElement.offsetParent == where_ref.offsetParent)
//            {
//            	// [DN 3/4/2008:BR31051] the table is offset from the same position as the scrollbar.  only account for the chart element's position in the offset...
//                // [GT 03/05/2010:Bug28948] getBoxObjectFor is removed from Firefox 3.6+
//                var boxObj = IGB.getBoxObjectFor(chartElement);
//                xOffset = boxObj.x;
//                yOffset = boxObj.y;
//            }
//            else
//            {
//                var offsetParent = tableElement.offsetParent;
//                while (offsetParent && offsetParent != where_ref.offsetParent)
//                {
//                    xOffset += offsetParent.offsetLeft;
//                    yOffset += offsetParent.offsetTop;
//                    offsetParent = offsetParent.offsetParent;
//                }
//            }
//        }
//        
//		if (this.Location)
//		{
//			IGB.SetX(where_ref, this.Location.X + xOffset);
//			IGB.SetY(where_ref, this.Location.Y + yOffset);
//		}

		if (this.Location)
		{
			IGB.SetX(where_ref, this.Location.X);
			IGB.SetY(where_ref, this.Location.Y);
		}
	}
        
	this.Render = function(where) 
	{
	    this.Where = where;
		var where_ref = IGB.GetObject(this.Where); 
		if (where_ref == null) return;

		this.SetLocation();

		if ((this.Orientation != null)&&(this.Orientation=='vertical'))
		{
			var scrl = this.Height - 2 * this.Width - this.ScrollerLen;

			this.HTML ="<table OnMouseWheel=ScrollbarMouseWheel('"+this.UniqueID+"') id='"+this.ID+"' width="+this.Width+" height="+this.Height+" border=0 cellpadding=0 cellspacing=0 style='table-layout:fixed'>";
			this.HTML += "<tr>";
			this.HTML += "<td valign=top>";
			this.HTML += "<img						width="+this.Width+"px height="+this.Width+"px			src='"+this.ImageURL+"/"+this.UseImageFromId+"_top_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_top_over.jpg';\"		OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_top_.jpg';\"		OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_top_down.jpg'; ScrollItV('"+this.UniqueID+"', -"+this.SmallChange+"); Repeating(true, ScrollItV, ['"+this.UniqueID+"', -"+this.SmallChange+"]); \"	OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_top_.jpg';\" ><br>";
			this.HTML += "<img id='"+this.UniqueID+"_1'	width="+this.Width+"px height=1px						src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_.jpg';\"	OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_down.jpg'; ScrollItV('"+this.UniqueID+"', -"+this.LargeChange+"); Repeating(true, ScrollItV, ['"+this.UniqueID+"', -"+this.LargeChange+"]);\"	OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_.jpg';\" ><br>";
			this.HTML += "<img						width="+this.Width+"px height="+this.ScrollerLen+"px id='" + this.UniqueID + "_engagable' src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_v_.jpg'	OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_v_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_v_.jpg';\"	OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_v_down.jpg'; EngageObject('"+this.UniqueID+"');\"																						OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_v_.jpg';\" ><br>";
			this.HTML += "<img id='"+this.UniqueID+"_3'	width="+this.Width+"px height="+scrl+"px				src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_.jpg';\"	OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_down.jpg'; ScrollItV('"+this.UniqueID+"', "+this.LargeChange+"); Repeating(true, ScrollItV, ['"+this.UniqueID+"', "+this.LargeChange+"]); \"	OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_v_.jpg';\" ><br>";
			this.HTML += "<img						width="+this.Width+"px height="+this.Width+"px			src='"+this.ImageURL+"/"+this.UseImageFromId+"_bottom_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_bottom_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_bottom_.jpg';\"	OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_bottom_down.jpg'; ScrollItV('"+this.UniqueID+"', "+this.SmallChange+"); Repeating(true, ScrollItV, ['"+this.UniqueID+"', "+this.SmallChange+"]); \"	OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_bottom_.jpg';\" >";
			this.HTML += "</td></tr>";
			this.HTML += "</table>";	
		}
		else 
		{
			var scrl = this.Width - 2 * this.Height - this.ScrollerLen;

			this.HTML ="<table OnMouseWheel=ScrollbarMouseWheel('"+this.UniqueID+"') id='"+this.UniqueID+"' width="+this.Width+" height="+this.Height+" border=0 cellpadding=0 cellspacing=0 style='table-layout:fixed'>";
			this.HTML += "<tr>";
			this.HTML += "<td  width="+this.Height+"px		height="+this.Height+"px>";
			this.HTML += "<img width="+this.Height+"px		height="+this.Height+"px src='"+this.ImageURL+"/"+this.UseImageFromId+"_left_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_left_over.jpg';\"		OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_left_.jpg';\"		OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_left_.jpg';\"		OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_left_down.jpg'; ScrollItH('"+this.UniqueID+"', -"+this.SmallChange+");		Repeating(true, ScrollItH, ['"+this.UniqueID+"', -"+this.SmallChange+"]); \" ></td>";
			this.HTML += "<td  width=1px					height="+this.Height+"px id='"+this.UniqueID+"_1'>"; 
			this.HTML += "<img width=1px					height="+this.Height+"px id='"+this.UniqueID+"_2'		src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_.jpg';\"	OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_.jpg';\"		OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_down.jpg'; ScrollItH('"+this.UniqueID+"', -"+this.LargeChange+");	Repeating(true, ScrollItH, ['"+this.UniqueID+"', -"+this.LargeChange+"]);\" ></td>";
			this.HTML += "<td  width="+this.ScrollerLen+"px height="+this.Height+"px>";
			this.HTML += "<img width="+this.ScrollerLen+"px height="+this.Height+"px id='"+this.UniqueID+"_engagable' src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_h_.jpg'	OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_h_over.jpg';\" OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_h_.jpg';\"  OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_h_.jpg';\"	OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_scroll_h_down.jpg'; EngageObject('"+this.UniqueID+"');\"   ></td>";
			this.HTML += "<td  width="+scrl+"px				height="+this.Height+"px id='"+this.UniqueID+"_3'>";
			this.HTML += "<img width="+scrl+"px				height="+this.Height+"px id='"+this.UniqueID+"_4'		src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_.jpg';\"	OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_.jpg';\"		OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_back_h_down.jpg'; ScrollItH('"+this.UniqueID+"', "+this.LargeChange+");		Repeating(true, ScrollItH, ['"+this.UniqueID+"', "+this.LargeChange+"]); \" ></td>";
			this.HTML += "<td  width="+this.Height+"px		height="+this.Height+"px>";
			this.HTML += "<img width="+this.Height+"px		height="+this.Height+"px						src='"+this.ImageURL+"/"+this.UseImageFromId+"_right_.jpg'		OnMouseOver=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_right_over.jpg';\"	OnMouseOut=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_right_.jpg';\"		OnMouseUp=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_right_.jpg';\"		OnMouseDown=\"this.src='"+this.ImageURL+"/"+this.UseImageFromId+"_right_down.jpg'; ScrollItH('"+this.UniqueID+"', "+this.SmallChange+");		Repeating(true, ScrollItH, ['"+this.UniqueID+"', "+this.SmallChange+"]); \" ></td>";
			this.HTML += "</tr>";
			this.HTML += "</table>";

		}
		if (where_ref.style != null)
		{
			where_ref.style.height = this.Height;
			where_ref.style.width = this.Width;
		}
		IGB.InsertHTML(where_ref, this.HTML, "afterBegin");		
		this.SetValue(this.Value);
		
        var igShared = this.GetIGShared();
		if (this.Orientation == "vertical" && IGB.GetHeight(IGB.GetObject(this.UniqueID + "_3")) == 0 && igShared && igShared.addTabListener)
		{ 
		    
		    if (igShared == top.ig_shared && !top.ID2OBJECT)
		    {
		        top.ID2OBJECT = ID2OBJECT;
		    }
		    this._tabListener = "var obj = ID2OBJECT['" + this.ID + "']; if (obj && obj.InitSize) obj.InitSize();";
	        igShared.addTabListener(this._tabListener);
		}
	}
	this.GetIGShared = function()
	{
	    
	    if (typeof(ig_shared) != "undefined" && ig_shared)
	    {
	        return ig_shared;
	    }
	    else if (top.ig_shared)
	    {
	        return top.ig_shared;
	    }
	    return null;
	}
    this.InitSize = function(id)
    {
        if (this.Orientation != null && this.Orientation=='vertical')
		{
			var scrl = this.Height - 2 * this.Width - this.ScrollerLen;
			var img3 = IGB.GetObject(this.UniqueID + "_3");
			IGB.SetHeight(img3, scrl);
			var igShared = this.GetIGShared();
			if (IGB.GetHeight(img3) > 0 && igShared && igShared.removeTabListener)
			{
			    
			    igShared.removeTabListener(this._tabListener);
			    this._tabListener = null;
			}
		}
    }
	this.SetValue = function(val)
	{
		this.Value = val;
		var id = this.UniqueID;

		var id1 = IGB.GetObject(id+"_1");
		var id2 = IGB.GetObject(id+"_2");
		var id3 = IGB.GetObject(id+"_3");
		var id4 = IGB.GetObject(id+"_4");

		var cwid1 = 0;
		var cwid3 = 0;

		if ((this.Orientation != null)&&(this.Orientation=='vertical'))
		{
			cwid1 = IGB.GetHeight(id1);
			cwid3 = IGB.GetHeight(id3);
		}
		else 
		{
			cwid1 = IGB.GetWidth(id1);
			cwid3 = IGB.GetWidth(id3);
		}

		var totwid = cwid1 + cwid3 -1;

		if (this.Maximum != this.Minimum)
		{
			nuwid1 = (val - this.Minimum) * (totwid-1) / (this.Maximum - this.Minimum) + 1 ;
		}
		else 
		{
			nuwid1 = totwid
		}

		if (nuwid1 > totwid) nuwid1 = totwid;
		if (nuwid1 < 1) nuwid1 = 1;

		var nuwid3 = totwid - nuwid1;
		if (nuwid3 > totwid) nuwid3 = totwid;
		if (nuwid3 < 1) nuwid3 = 1;


		if ((this.Orientation != null)&&(this.Orientation=='vertical'))
		{
			IGB.SetHeight(id1, nuwid1);
			IGB.SetHeight(id3, nuwid3);
		}
		else 
		{
			IGB.SetWidth(id1, nuwid1);
			IGB.SetWidth(id2, nuwid1);

			IGB.SetWidth(id3, nuwid3);
			IGB.SetWidth(id4, nuwid3);
		}
	}
}

function ScrollItH(id, scroll) 
{
	var id1 = IGB.GetObject(id+"_1");
	var id2 = IGB.GetObject(id+"_2");
	var id3 = IGB.GetObject(id+"_3");
	var id4 = IGB.GetObject(id+"_4");

	var cwid1 = IGB.GetWidth(id1);
	var cwid3 = IGB.GetWidth(id3);

	var totwid = cwid1 + cwid3 -1;

	var nuwid1 = cwid1 + scroll;
	if (nuwid1 > totwid) nuwid1 = totwid;
	if (nuwid1 < 1) nuwid1 = 1;

	var nuwid3 = cwid3 - scroll;
	if (nuwid3 > totwid) nuwid3 = totwid;
	if (nuwid3 < 1) nuwid3 = 1;

	// repeating sanity
	if (nuwid1==1 || nuwid3 == 1)
	{
		Repeating(false); // clear timer
	}

	IGB.SetWidth(id1, nuwid1);
	IGB.SetWidth(id2, nuwid1);

	IGB.SetWidth(id3, nuwid3);
	IGB.SetWidth(id4, nuwid3);

	// update the value
	var obj = ID2OBJECT[id];
	obj.Value = obj.Minimum + ((nuwid1-1)/(totwid-1))*(obj.Maximum - obj.Minimum);
	
	// Fire events.
	var listener = obj.Listener["scroll"];
	if (listener)
	{
		listener.apply(this, ["scroll", IGB.GetObject(id), obj]);
	}
}

function ScrollItV(id, scroll) 
{
	var id1 = IGB.GetObject(id+"_1");
	var id3 = IGB.GetObject(id+"_3");

	var cwid1 = IGB.GetHeight(id1);
	var cwid3 = IGB.GetHeight(id3);

	var totwid = cwid1 + cwid3 -1;

	var nuwid1 = cwid1 + scroll;
	if (nuwid1 > totwid) nuwid1 = totwid;
	if (nuwid1 < 1) nuwid1 = 1;

	var nuwid3 = cwid3 - scroll;
	if (nuwid3 > totwid) nuwid3 = totwid;
	if (nuwid3 < 1) nuwid3 = 1;

	// repeating sanity
	if (nuwid1==1 || nuwid3 == 1)
	{
		Repeating(false); // clear timer
	}

	IGB.SetHeight(id1, nuwid1);
	IGB.SetHeight(id3, nuwid3);

	// update the value
	var obj = ID2OBJECT[id];
	obj.Value = obj.Minimum + ((nuwid1-1)/(totwid-1))*(obj.Maximum - obj.Minimum);

	// Fire events.
	var listener = obj.Listener["scroll"];
	if (listener)
	{
		listener.apply(this, ["scroll", IGB.GetObject(id), obj]);
	}
}

var EngagedObject=null;
var OldMouseDown;
var OldMouseMove;
var OldMouseUp;
var MouseDownX, MouseDownY;

function EngageObject(which) 
{
	EngagedObject = ID2OBJECT[which]; 
}
function ReleaseObject() 
{
	EngagedObject = null;
}

function NewMouseDown(evt) 
{
	if (OldMouseDown) OldMouseDown(evt);
	if (IGB.IsNetscape6) 
	{
		MouseDownX = evt.pageX;
		MouseDownY = evt.pageY;

		if (evt && evt.target && evt.target.id && (evt.target.id.indexOf('engagable')>-1))
		{
			return false;	
		}
	} else {
		MouseDownX = window.event.clientX;
		MouseDownY = window.event.clientY;
	}
	return true;
}
function NewMouseMove(evt) 
{
	if (OldMouseMove) OldMouseMove(evt);
	if( EngagedObject!=null) 
	{
		var scroll = 0;
		var id = EngagedObject.UniqueID?EngagedObject.UniqueID:EngagedObject.ID;
		if ((EngagedObject.Orientation != null)&&(EngagedObject.Orientation=='vertical')) 
		{
			if (IGB.IsNetscape6) 
			{
                scroll = (evt.pageY - MouseDownY);
				MouseDownY = evt.pageY;
            } 
			else 
			{
                scroll = (window.event.clientY - MouseDownY);
				MouseDownY = window.event.clientY;
            }
			ScrollItV(id, scroll);
		}
		else 
		{
			if (IGB.IsNetscape6) 
			{
                scroll = (evt.pageX - MouseDownX);
				MouseDownX = evt.pageX;
            } 
			else 
			{
                scroll = (window.event.clientX - MouseDownX);
				MouseDownX = window.event.clientX;
            }
			ScrollItH(id, scroll);
		}
		return false;
	}
	return true;
}
function NewMouseUp(evt) 
{
	Repeating(false); // clear timer
	if (OldMouseUp) OldMouseUp(evt);
	ReleaseObject();
	return true;
}

function ScrollbarMouseWheel(id)
{
	var obj = ID2OBJECT[id];
	var scroll = 0;
	if		(event.wheelDelta >=  120) { scroll = -10;  }
    else if (event.wheelDelta <= -120) { scroll = +10;  }
	if ((obj.Orientation != null)&&(obj.Orientation=='vertical'))
	{
		ScrollItV(id, scroll);
	}
	else
	{
		ScrollItH(id, scroll);
	}	
	return false;
}

function InitilizeScrollbar() 
{
	if( OldMouseDown==null && IGB.FunctionName(document.OnMouseDown)!="NewMouseDown" && IGB.FunctionName(document.onmousedown)!="NewMouseDown")
	{
		OldMouseDown=document.onmousedown;
	}
	if( OldMouseUp==null && IGB.FunctionName(document.OnMouseUp)!="NewMouseUp" && IGB.FunctionName(document.onmouseup) != "NewMouseUp")
	{
		OldMouseUp=document.onmouseup;
	}
	if( OldMouseMove==null && IGB.FunctionName(document.OnMouseMove)!="NewMouseMove" && IGB.FunctionName(document.onmousemove) != "NewMouseMove")
	{
		OldMouseMove=document.onmousemove;
	}

	document.onmousedown = NewMouseDown;
	document.onmouseup   = NewMouseUp;
	document.onmousemove = NewMouseMove;

	if (IGB.IsNetscape6) 
	{
        document.captureEvents(Event.MOUSEDOWN | Event.MOUSEMOVE | Event.MOUSEUP)
    }
}
 
// UltraChart CSOM object.
// param id: chart id
// param imageUrl: Url to pickup scrollbar images from.
function IGUltraChart(id, imageUrl, uniqueId)
{
	// public properties assined with constructor
	this.ID			= id;
	this.ImageUrl	= imageUrl;
	this.UniqueID   = uniqueId;
	
	// must be assigned public properties
	this.EnableTooltipFading = false;
	this.EventData	= null;
	this.TooltipData= null;
	this.RowCount = 0;
	this.ColumnCount = 0;
	this.TooltipDisplay = 0; // Never
	this.EnableCrossHair = false;
	this.EnableServerEvent = false;
	this.Section508Compliant = false;
	
	ID2OBJECT[this.ID]= this; // save id's ref.
	this.Parent		  = null; // for parent child relationship
	this.HTML		  = "";   // debugging purposes
	this.Listener	  = new Array();
	this.DEBUG		  = false;
	this.AddListener  = function(type, function_ref) 
	{
		this.Listener[type] = function_ref; // save listener into array.
		// In all these listeners "this" refers to calling IGUltraChart object
		// UltraChart fires following CSOM Events.
		// mousemove(object []arg)
		// mouseout(object []arg)
		// mouseclick(object []arg)
		// showtooltip(string text, object tooltip_ref)
		// hidetooltip(string text, object tooltip_ref)
		// crosshairmove(int x, int y)
		// hidecrosshair(int x, int y)
		// showcrosshair(int x, int y)
		// scroll(IGScrollBar sb)
		
	}

	// private variables
	this.payloadHandler = null;
	this.SB1 = null;
	this.SB2 = null;
	this.igWindowVuer = null;
	this.iGCrossHair = null;
	this.XhairBounds = null;
	this.TooltipFader = null;
	this.TooltipVisible = false;
    this.componentsVals = null;
	this.renderVals = null;

	// Called by UltraWebChart when it runs at Server.
	this.CreateComponents = function (vals) 
    {
	    this.componentsVals = vals;	
	    // hide the tooltip if the chart is in UpdatePanel and the tooltip is not cleared.
	    var tooltip_ref_body = IGB.GetObject(this.ID+"_IGTooltipBody");
	    if (tooltip_ref_body)
	    {
            tooltip_ref_body.style.visibility = 'hidden';
	    }
	
		// Create instance
		
		var indexOfColon = this.UniqueID.lastIndexOf(":");
		var idPrefix;
		if (indexOfColon != -1)
		{
			idPrefix = this.UniqueID.substring(indexOfColon + 1, this.UniqueID.length);
		}
		else
		{
			idPrefix = this.UniqueID;
		}
        if (idPrefix.indexOf("$") != -1)
        {
            var tokens = idPrefix.split("$");
            if (tokens.length > 0)
            {
                idPrefix = tokens[tokens.length - 1];
            }
        }

		this.SB1				= new IGScrollBar(idPrefix + "_SB1", vals[2], vals[3],  30, this.ImageUrl, "", this.UniqueID + "_SB1");
		this.SB2				= new IGScrollBar(idPrefix + "_SB2", vals[9], vals[10], 30, this.ImageUrl, "vertical", this.UniqueID + "_SB2");
		this.igWindowVuer		= new IGWindowViewer(this.ID + "_igWindowVuer", this.ID + "_ScrollImage", this.ID + "_igWindowVuer");
		this.iGCrossHair		= new IGCrossHair(this.ID + "_iGCrossHair");
		this.TooltipFader		= new Fader();

		// Set parents for object containment hierarchy.	
		this.SB1.Parent = this;
		this.SB2.Parent = this;
		this.igWindowVuer.Parent = this;
		this.iGCrossHair.Parent = this;

		var baseImage = IGB.GetObject(this.ID + "_BaseImage");
		var scrollImage = IGB.GetObject(this.ID + "_ScrollImage");
		var table = IGB.GetObject(this.ID + "_table");
		
		// cross hair settings
		this.iGCrossHair.SpanImageObject	= baseImage;
		this.iGCrossHair.HairHorizontal		= IGB.GetObject(this.ID+"_HairHorizontal");
		this.iGCrossHair.HairVertical		= IGB.GetObject(this.ID+"_HairVertical");
		this.iGCrossHair.Visible			= this.EnableCrossHair;

		if (IGB.IsNetscape6 && table.offsetParent && table.offsetParent.tagName != "BODY")
		{
			var oL = table.offsetLeft;
			var oT = table.offsetTop;
			if (vals[1])
			{
				vals[1].X += oL;
				vals[1].Y += oT;
			}
			var offsetXHairBounds = table.offsetParent.style.position != "absolute" || (this.iGCrossHair.HairHorizontal.offsetParent && this.iGCrossHair.HairHorizontal.offsetParent.tagName != "BODY");
			if (offsetXHairBounds)
			{
			    if (vals[0])
			    {
				    vals[0].X += oL;
				    vals[0].Y += oT;
				}
			}
			
		}
		// [DN 6/13/2006:BR10355] surgical fix.  scrolling has been enabled on the immediate parent so let's try to respect that by turning off relative positioning.
        if (document.documentElement && IGB.IsIE && table && table.parentElement && table.parentElement.parentElement && table.parentElement.parentElement.style && !scrollImage)
	    {
	        var overflow = table.parentElement.parentElement.style.overflow;
	        if (overflow && overflow != "" && overflow.toLowerCase() != "visible" && baseImage && baseImage.parentElement && baseImage.parentElement.style)
	        {
	            table.style.position = "";
	            baseImage.parentElement.style.position = "";
	        }
	    }
		// View settings
		this.igWindowVuer.SourceBounds		= vals[4]; 
		this.igWindowVuer.DestBounds		= vals[5]; 
		this.XhairBounds					= vals[0]; 
		
		
		// Scrollbar settings
		this.SB1.Location = vals[1];
		this.SB1.AddListener("scroll", this.SB1_Scroll);
		this.SB1.Minimum = vals[6];
		this.SB1.Maximum = vals[7];
		this.SB1.Value	 = vals[6];

		this.SB2.Location = vals[8];
		this.SB2.AddListener("scroll", this.SB2_Scroll);
		this.SB2.Minimum = vals[11];
		this.SB2.Maximum = vals[12];
		this.SB2.Value	 = vals[11]
	}

	// Called by UltraWebChart when it runs at Client.
    this.Render = function (vals) 
    {
        this.renderVals = vals;
		if (IGB.IsNetscape6 && vals[5])
		{
			var table = IGB.GetObject(this.ID + "_table");	  
			if (this.iGCrossHair.HairHorizontal.offsetParent && this.iGCrossHair.HairHorizontal.offsetParent.tagName != "BODY")
			{
			    var oL = table.offsetLeft;
			    var oT = table.offsetTop;
			    vals[5].X += oL;
			    vals[5].Y += oT;
			}
		}
		if (vals[0]) this.SB1.Render(this.UniqueID+"_SB1_Location");
		if (vals[1]) this.SB2.Render(this.UniqueID+"_SB2_Location");
		if (vals[2]) this.igWindowVuer.Render();
		if (vals[3])this.iGCrossHair.Render(vals[5]);
		
		// Show image after rendering is done
		if (vals[4]) IGB.ShowObject(IGB.GetObject(this.ID+"_ScrollImage"));
	}

	// This handles the Horizontal Scroll functionlity of chart.
	this.SB1_Scroll=function(evt, sender_element, sender_object) 
	{
		sender_object.Parent.igWindowVuer.SourceBounds.X = parseInt(sender_object.Value);
		sender_object.Parent.igWindowVuer.Render();
		var function_ref = sender_object.Parent.Listener[evt];
		if (function_ref != null) 
		{
			function_ref.apply(sender_object.Parent, [sender_object]);
		}
	}

	// This handles the Vertical Scroll functionlity of chart.
	this.SB2_Scroll=function(evt, sender_element, sender_object) 
	{
		sender_object.Parent.igWindowVuer.SourceBounds.Y = parseInt(sender_object.Value);
		sender_object.Parent.igWindowVuer.Render();
		var function_ref = sender_object.Parent.Listener[evt];
		if (function_ref != null) 
		{
			function_ref.apply(sender_object.Parent, [sender_object]);
		}
	}

	// This handles the Horizontal Scrolling on mouse wheel functionlity of chart.
	this.onmousewheel=function(evt, id)
	{
		// works only horizontally.
		if		(evt.wheelDelta >=  120) { this.igWindowVuer.MoveBy(-10,0);  }
		else if (evt.wheelDelta <= -120) { this.igWindowVuer.MoveBy(+10,0);  }

		this.SB1.SetValue(this.igWindowVuer.SourceBounds.X);
	}

	// This handles the On Mouse Move functionlity of chart.
	this.onmousemove = function(evt, id) {
	    var x, y;
	    var crossHair = this.iGCrossHair;
	    var oldState = crossHair.Visible;
	    var baseImageRef = IGB.GetObject(this.ID + "_BaseImage");
	    var tooltip_ref_body = IGB.GetObject(this.ID + "_IGTooltipBody");

	    if (!tooltip_ref_body) {
	        if (!(tooltip_ref_body = this.cloneTooltip())) {
	            return;
	        }
	    }

	    if (IGB.IsNetscape6) {
	        x = evt.pageX;
	        y = evt.pageY;
	    }
	    else {
	        x = window.event.clientX;
	        y = window.event.clientY;
	        if ((IGB.IsIE8Plus || IGB.IsOpera9Plus) && document.documentElement) {
	            x += document.documentElement.scrollLeft;
	            y += document.documentElement.scrollTop;
	        }
	        else {
	            x += document.body.scrollLeft;
	            y += document.body.scrollTop;
	        }
	        if (IGB.IsMac && IGB.IsIE) {
	            x -= 10;
	            y -= 15;
	        }
	    }

	    var locx = x;
	    var locy = y;

	    // calculate offset of the tooltip that is in the body HTML element
	    var offsetParent = tooltip_ref_body.offsetParent;
	    var positionFromBody = offsetParent.tagName == "BODY" || offsetParent.tagName == "HTML";
	    if (!positionFromBody) {
	        x -= IGB.GetPageX(baseImageRef);
	        y -= IGB.GetPageY(baseImageRef);
	        if (IGB.IsFirefoxOrSafari && tooltip_ref_body.offsetParent != baseImageRef.offsetParent) {
	            var p = baseImageRef.offsetParent, i = 0, xOffset = 0, yOffset = 0;
	            while (p != tooltip_ref_body.offsetParent) {
	                xOffset += p.offsetLeft;
	                yOffset += p.offsetTop;
	                p = p.offsetParent;
	                if (i++ > 50) {
	                    xOffset = 0;
	                    yOffset = 0;
	                }
	            }
	            x += xOffset;
	            y += yOffset;
	        }
	    }

	    // calculate offset of the original tooltip
	    var tooltip_ref = IGB.GetObject(this.ID + "_IGTooltip");

	    offsetParent = tooltip_ref.offsetParent;
	    positionFromBody = offsetParent.tagName == "BODY" || offsetParent.tagName == "HTML";
	    if (!positionFromBody) {
	        locx -= IGB.GetPageX(baseImageRef);
	        locy -= IGB.GetPageY(baseImageRef);
	        if (IGB.IsFirefoxOrSafari && tooltip_ref.offsetParent != baseImageRef.offsetParent) {
	            var p = baseImageRef.offsetParent, i = 0, xOffset = 0, yOffset = 0;
	            while (p != tooltip_ref.offsetParent) {
	                xOffset += p.offsetLeft;
	                yOffset += p.offsetTop;
	                p = p.offsetParent;
	                if (i++ > 50) {
	                    xOffset = 0;
	                    yOffset = 0;
	                }
	            }
	            locx += xOffset;
	            locy += yOffset;
	        }
	    }

	    if (this.EnableCrossHair) {
	        crossHair = this.getClonedCrossHairs();
	        // [DN 8/28/2007:BR25943] reposition xhair in firefox if offsetParent is an element other than the chart (typically it will be the body if it's not the chart)
	        var xOffset = 0, yOffset = 0;
	        if (IGB.IsFirefoxOrSafari && crossHair.HairHorizontal.offsetParent != baseImageRef.offsetParent && positionFromBody) {
	            // [GT 03/05/2010:Bug28948] getBoxObjectFor is removed from Firefox 3.6+
	            var boxObj = IGB.getBoxObjectFor(baseImageRef);
	            if (boxObj) {
	                xOffset -= boxObj.x;
	                yOffset -= boxObj.y;
	            }

	        }
	        var hSC = IGB.GetHScrolledContainer(crossHair.HairHorizontal);
	        var vSC = IGB.GetVScrolledContainer(crossHair.HairHorizontal);
	        xOffset += hSC ? hSC.scrollLeft : 0;
	        yOffset += vSC ? vSC.scrollTop : 0;
	        crossHair.Visible = this.XhairBounds.Inside(locx + xOffset, locy + yOffset);
	        crossHair.Update(x - 3, y - 3, baseImageRef);
	        if (crossHair.Visible) {
	            var function_ref = this.Listener["crosshairmove"];
	            if (function_ref != null) {
	                function_ref.apply(this, [x - 3, y - 3]);
	            }
	        }
	    }

	    if (oldState != crossHair.Visible) {
	        if (oldState) {
	            var function_ref = this.Listener["hidecrosshair"];
	            if (function_ref != null) {
	                function_ref.apply(this, [x - 3, y - 3]);
	            }
	        }
	        else {
	            var function_ref = this.Listener["showcrosshair"];
	            if (function_ref != null) {
	                function_ref.apply(this, [x - 3, y - 3]);
	            }
	        }
	    }

	    x += 15;
	    y += 20;

	    locx += 18;
	    locy += 23;

	    var ttof = this.TooltipOverflow ? this.TooltipOverflow : tooltip_ref_body.getAttribute("igTtOf");
	    switch (ttof) {
	        case "None":
	            IGB.SetXScrollContainerSafe(tooltip_ref_body, x);
	            IGB.SetYScrollContainerSafe(tooltip_ref_body, y);
	            break;
	        case "ClientArea":
	            IGB.SetXClientOverflowSafe(tooltip_ref_body, x);
	            IGB.SetYClientOverflowSafe(tooltip_ref_body, y);
	            break;
	        case "ChartArea":
	            locx = x - IGB.GetPageX(baseImageRef);
	            locy = y - IGB.GetPageY(baseImageRef);
	            IGB.SetXOverflowSafe(tooltip_ref_body, x, locx, baseImageRef);
	            IGB.SetYOverflowSafe(tooltip_ref_body, y, locy, baseImageRef);
	            break;
	    }
	    if (this.TooltipVisible)
	    {
	        
	        IGB.ShowObject(tooltip_ref_body);
	    }
	}


	this.cloneHorizontalCrossHair = function() {
	    var horizontal_ref = IGB.GetObject(this.ID + "_HairHorizontal");
	    var horizontal_ref_body = horizontal_ref.cloneNode(true);
	    horizontal_ref_body.id = horizontal_ref_body.id.replace("_HairHorizontal", "_HairHorizontalBody");
	    document.body.appendChild(horizontal_ref_body);
	    return horizontal_ref_body;
	}

	this.cloneVerticalCrossHair = function() {
	    var vertical_ref = IGB.GetObject(this.ID + "_HairVertical");
	    var vertical_ref_body = vertical_ref.cloneNode(true);
	    vertical_ref_body.id = vertical_ref_body.id.replace("_HairVertical", "_HairVerticalBody");
	    document.body.appendChild(vertical_ref_body);
	    return vertical_ref_body;
	}

	this.getClonedCrossHairs = function () 
    {
	    var crossHair_ref = this.iGCrossHair;
	    // [PK 11/3/2011:BR58592] the DOM objects are recreated (ajax request), we need to recreate the javasrcipt objects too
        // [DN 4/11/2011:BR70761] changed this to check for parentNode instead of offsetParent, because in several browsers, offsetParent will return null if display is none.
	    if (!this.ElementHasParentNodeSafe(crossHair_ref.HairHorizontal) &&
            this.componentsVals != null && this.renderVals != null) 
        {
	        this.CreateComponents(this.componentsVals);
	        this.Render(this.renderVals);
	        crossHair_ref = this.iGCrossHair;
	    }

	    if (document.readyState == "interactive") 
        {
	        return crossHair_ref; // [DN 6/13/2008:BR33878] avoid MSIE bug KB927917
	    }
	    
	    if (!crossHair_ref.HairVertical.id.match(/_HairVerticalBody$/)) {
	        crossHair_ref.HairVertical = this.cloneVerticalCrossHair();
	    }

	    if (!crossHair_ref.HairHorizontal.id.match(/_HairHorizontalBody$/)) {
	        crossHair_ref.HairHorizontal = this.cloneHorizontalCrossHair();
	    }

	    return crossHair_ref;
	}

	this.ElementHasParentNodeSafe = function (elem) 
    {
	    try 
        {
	        return elem.parentNode ? true : false;
	    }
	    catch (e) 
        {
	        return false;
	    }
	}
	
	this.cloneTooltip=function()
	{
	    var tooltip_ref = IGB.GetObject(this.ID+"_IGTooltip");
        var tooltip_ref_body = tooltip_ref.cloneNode(true);
        tooltip_ref_body.id = tooltip_ref_body.id.replace("_IGTooltip", "_IGTooltipBody");
        
        if (document.readyState == "interactive")
        {
            return; // [DN 6/13/2008:BR33878] avoid MSIE bug KB927917
        }
        document.body.appendChild(tooltip_ref_body);
        
        return tooltip_ref_body;
	}
	
	this.getMaxZIndexOfParents = function(obj)
	{
	    var zIndex = obj.style.zIndex;
	    var parent = obj.parentNode;
	    while (parent.nodeName != "HTML")
	    {
	        if (parent.style.zIndex > zIndex)
	        {
	            zIndex = parent.style.zIndex;
	        }

	        parent = parent.parentNode;
	    }

	    return zIndex;
	}
	
	// This handles the Tooltips functionlity of chart.
	this.ShowTooltip=function(evt, id, args)
	{
	    var tooltip_ref_body = IGB.GetObject(this.ID+"_IGTooltipBody");
        
        if (!tooltip_ref_body)
	    {
	        if (!(tooltip_ref_body = this.cloneTooltip()))
	        {
	            return;
	        }
	    }		
		
		var text = "";
		var data_id = args[4]+"_"+args[1]+"_"+args[2];
		
		if (this.DEBUG)
		{
			window.status = data_id;
		}

		if (this.TooltipData!=null)
		{
			text = this.TooltipData[data_id];
		}
				
		IGB.WriteHTML(tooltip_ref_body, "<nobr>"+text+"</nobr>");		
		if (this.TooltipDisplay == 2)
		{
		    
		    
		    IGB.ShowObject(tooltip_ref_body);
        }
		this.TooltipVisible = true;

		if (this.EnableTooltipFading)
		{
			this.TooltipFader.End();
			this.TooltipFader.Start(tooltip_ref_body, 0, 100, 20);
		}
		var function_ref = this.Listener["showtooltip"];
		if (function_ref != null) 
		{
			function_ref.apply(this, [text, tooltip_ref_body]);
        }

        var chart = IGB.GetObject(this.ID);
        var newZIndex = this.getMaxZIndexOfParents(chart) + 5;
        IGB.SetZValue(tooltip_ref_body, newZIndex);
	}

	// This handles the Tooltips functionlity of chart.
	this.HideTooltip=function(evt, id, args)
	{
		// Don't do this until the tooltip was really visible
		if (!this.TooltipVisible) return;
		
		var tooltip_ref_body = IGB.GetObject(this.ID+"_IGTooltipBody");
		
		var text = "";
		var data_id = args[4]+"_"+args[1]+"_"+args[2];

		if (this.TooltipData!=null)
		{
			text = this.TooltipData[data_id];
		}

		this.TooltipFader.End();
		tooltip_ref_body.style.visibility = 'hidden';
		
		this.TooltipVisible = false;
		var function_ref = this.Listener["hidetooltip"];
		if (function_ref != null) 
		{
			function_ref.apply(this, [text, tooltip_ref_body]);
		}
	}

	// This handles the functionality of the chart related to all other events. This acts as main dispatcher of client events and Glue between Version 1/3 javascript code.
	this.onallevent=function(evt, id, args)
	{

		// args = [this_ref, row, column, event_name, layer_id]
		
		var function_ref = this.Listener[evt.type];
		if (function_ref != null) 
		{
			var v = IGB.DecodeArguments( this.EventData[args[4]+"_"+args[1]+"_"+args[2]] );
			function_ref.apply(this, [this, v[0], v[1], v[2], v[3], v[4], evt.type, args[4] ] );
		}
		
		if (evt.type == "mouseover" && this.TooltipDisplay == 1)
		{
			this.ShowTooltip(evt, id, args);
		}
		if (evt.type == "mousemove" && (IGB.IsNetscape6 || IGB.IsMac || IGB.IsOpera || IGB.IsSafari || IGB.IsIE9))
		{
			this.onmousemove(evt);
		}
		else if (evt.type == "click" && this.TooltipDisplay == 2)
		{
			this.ShowTooltip(evt, id, args);
		}
		else if (evt.type == "mouseout" )
		{
			this.HideTooltip(evt, id, args);
		}

		if (((evt.type=="click")||(evt.type=='dblclick')) && (this.EnableServerEvent))
		{
			var data = this.EventData[args[4]+"_"+args[1]+"_"+args[2]];
			data+='&'+evt.type;
			if (this.DEBUG) window.status = "RawData="+data;
			__doPostBack(this.UniqueID, data);
		}
	}
}

IGB.AddEventListener(window, "resize", igchart_onWindowResize);

function igchart_onWindowResize() 
{
    var o;
    for (o in ID2OBJECT)
    {
        if (ID2OBJECT[o] && ID2OBJECT[o].SetLocation)
        {
            ID2OBJECT[o].SetLocation();
        }
    }
}
