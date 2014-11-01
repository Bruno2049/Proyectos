/*
* ig_webtree.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


 





if(typeof(igtree_IE) != "boolean")
	var igtree_IE = (document.all) ? true : false;
if(typeof(igtree_treeState) != "object")
	var igtree_treeState=[];
if(typeof(igtree_indexState) != "object")
	var igtree_indexState=[];
if(typeof(igtree_nodeState) != "object")
	var igtree_nodeState=[];



if (typeof(igtree_displayLoadPrompt) != "object")
    var igtree_displayLoadPrompt = true;
    
// public - Obtains the Tree object using its id
function igtree_getTreeById(id) 
{
    
    
    var tree = igtree_treeState[id];
    if(!tree && id)
    {
        id = id.replace(/_/g,"");
        id = id.replace(/-/g,"");
        id = id.replace(/:/g,"");
        id = id.replace(/$/g,"");
        id = id.replace(/\./g,"");
        id = id.replace(/{/g,"");
        id = id.replace(/}/g,"");
        tree = igtree_treeState[id];
    }
	return tree;
}

// public - Obtains a Node object using its id
function igtree_getNodeById(id) 
{
	var nodeElement = igtree_getElementById(id);
	if(nodeElement == null)
		return null;
	var oNode = nodeElement.oNode;
    
    
	for(var i in oNode) 
	    return oNode;
	
	oNode = new igtree_initNode(nodeElement);
	igtree_nodeState[id] = oNode; 
	nodeElement.oNode = oNode;
	return oNode;
}

// public - returns a Tree object based on a node Id 
function igtree_getTreeByNodeId(nodeId)
{
    if(!nodeId)
        return null;
	var treeName = nodeId;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	var tree = igtree_treeState[treeName];
	return tree;
}

// public - returns the tree object from a Node element
function igtree_getTreeByNode(node) {
	return igtree_getTreeByNodeId(node.id);
}

function igtree_setSelectedNode(tn, nodeId)
{
	var oTree=igtree_treeState[tn];
	if(!oTree)
		return null;

	oTree.Element.hideFocus = true;
		
	var node=null;
	var oNode = igtree_getNodeById(nodeId);
	if(nodeId != null)
		node = igtree_getElementById(nodeId);
		
	if(node) {
		if(node.disabled)
			return;
	}
	var igtree_currentNode=igtree_selectedNode(tn);

	if(node==igtree_currentNode)
		return igtree_currentNode;

	var oldNodeId = null;
	if(igtree_currentNode!=null)
		oldNodeId=igtree_currentNode.id;

	if(oTree.TreeLoaded) {
		if(igtree_fireEvent(tn,oTree.Events.BeforeNodeSelectionChange,"(\""+tn+"\",\""+oldNodeId+"\",\""+nodeId+"\")"))
			


			return null;
	}
	var className=igtree_getResolvedHiliteClass(tn,node);

	if(igtree_editControl != null && igtree_editControl.style.display!="none")
		if(igtree_endedit(true))
			return igtree_currentNode;

	if(igtree_currentNode!=null)
	{
		var nodeSpan;
		var styleE;
		if(oTree.FullNodeSelect == true)
			styleE = igtree_currentNode;
		else
			styleE = igtree_getNodeSpan(igtree_currentNode)
	    
	    if(styleE)
		{
		    styleE.tabIndex = -1;
		    nodeSpan = igtree_getNodeSpan(igtree_currentNode)
		    var image=nodeSpan.previousSibling.previousSibling;
		    if(image!=null && image.tagName=="IMG") {
			    var unselectedImage = igtree_currentNode.getAttribute("igUnselImage");
    			
			    
			    var alt = image.getAttribute("alt");
			    if(alt!=null)
			    {
				    var xpandAlt = image.getAttribute("igAltU");
				    if(xpandAlt!=null)
				    {
					    image.setAttribute("igAltS",alt);
					    image.setAttribute("alt",xpandAlt);
					    image.removeAttribute("igAltU");
				    }
			    }
			    if(unselectedImage!=null && unselectedImage.length>0) {
				    image.src=unselectedImage;
			    }
			    else if(image.src == "" && ig_csom.notEmpty(oTree.DefaultImage))
				    image.src=oTree.DefaultImage;
		    }
		    styleE.className = styleE.getAttribute("igtInitClass");
		   
		   
		    if (oldNodeId!=null )
		    {
		        var oldNodeObject = igtree_getNodeById(oldNodeId);
		        if (oldNodeObject && !oldNodeObject.getEnabled() && styleE.className!=oTree.DisabledClass)
		        {
		            styleE.className += " " + oTree.DisabledClass;
		        }
		    }
		}
	}
	if(node)
	{
		var nodeSpan;
		var styleE;
		if(oTree.FullNodeSelect == true)
			styleE = node;
		else
			styleE = igtree_getNodeSpan(node)
		nodeSpan = igtree_getNodeSpan(node)
		if(nodeSpan == null)
			return;
		if(oNode.hoverSet) 
			oNode.unHover();
		var nodeClassName=styleE.className;
		if(nodeClassName!=className)
		{
			var initClass=styleE.HovClass;
			if(initClass!=null)
				styleE.setAttribute("igtInitClass", initClass);
			else
				styleE.setAttribute("igtInitClass", nodeClassName);
				
			if(oTree.MergeStyles)
				styleE.className += " " + className;
			else
				styleE.className=className;
			
			
			var image=nodeSpan.previousSibling.previousSibling;
			if(image!=null && image.tagName=="IMG") 
			{
				var igimg = image.getAttribute("igimg");
				if(igimg!=null && igimg.length>0) 
				{
					var selectedImage = node.getAttribute("igSelImage");
					if(selectedImage==null || selectedImage.length==0)
						selectedImage=oTree.DefaultSelectedImage;
					if(ig_csom.notEmpty(selectedImage)) 
					{
						if(oTree.TreeLoaded)
							node.setAttribute("igUnselImage", image.src);
						
							
						var alt = image.getAttribute("alt");
						if(alt!=null)
						{
							var xpandAlt = image.getAttribute("igAltS");
							if(xpandAlt!=null)
							{
								image.setAttribute("igAltU",alt);
								image.setAttribute("alt",xpandAlt);
								image.removeAttribute("igAltS");
							}
						}
						image.src=selectedImage;
					}
				}
			}
		}
		oTree.selectedNodeElement = node;
		
		var oNode = igtree_getNodeById(nodeId);
		var parent = oNode.getParent();
		while(parent != null) {
			bExp = parent.getExpanded();
			if(!bExp)
				parent.setExpanded(true);
			parent = parent.getParent();
		}
		if(igtree_IE) {
			if(nodeSpan.offsetWidth != 0 && nodeSpan.offsetHeight != 0) {
				nodeSpan.tabIndex = 1000;
				try {
					//if(oTree.TreeLoaded == true) {
						for(var i = 0; i <oNode.Element.childNodes.length; i++)
						{
							if(oNode.Element.childNodes[i].tagName=="SPAN") {
							    oNode.Element.childNodes[i].hideFocus = true;
							    



								//oNode.Element.childNodes[i].focus();
							}
						}
					//}
				}
				catch (e)
				{
				}
			}
		}
		// clientViewState
		oTree.update("SelectedNode", nodeId);
	}
	else {
		oTree.update("SelectedNode", "NONE");
		oTree.selectedNodeElement = null;
	}
	
	igtree_currentNode = node;

	if(!oTree.TreeLoaded)
		return igtree_currentNode;
	igtree_fireEvent(tn,oTree.Events.AfterNodeSelectionChange,"(\""+tn+"\",\""+nodeId+"\")");
	if(oTree.NeedPostBack && igtree_clickCounter==0) {
		oTree.NeedPostBack=false;
		
		oTree._requestedPostBack = true;
		__doPostBack(oTree.UniqueId,"");
		
		return null;
	}
	return igtree_currentNode;
}

// public - Browser independent way to retrieve the source element of an event
function igtree_getSrcElement(evnt)
{
	if(igtree_IE)
		return evnt.srcElement;
	else
		return evnt.target;
}

// public - Browser independent way to retrieve an element by its id
function igtree_getElementById(id)
{
	return document.getElementById(id);
}

// public - Retrieves the UniqueId on the server for the tree.
function igtree_getUniqueId(treeId)
{
	return igtree_treeState[treeId].UniqueId;
}

// public - Begins editing of a node in the tree.
function igtree_beginedit(tn,nodeId)
{
	var e = igtree_getElementById(nodeId);
	var disabled = e.disabled;
	if(disabled)
		return;
	disabled = e.getAttribute("nodeDisabled");
	if(disabled == "1")
		return;
	igtree_endedit(true);
	var src=igtree_getNodeSpan(igtree_getElementById(nodeId));
	var height = src.offsetHeight;
	var ts = igtree_getTreeByNodeId(src.parentNode.id)
	var te = ts.treeElement;
	
	


	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.BeforeBeginNodeEdit,"(\""+tn+"\",\""+nodeId+"\")"))
		return;
			
	igtree_editControl=igtree_getEditControl(src);
	if(igtree_editControl) {
	    
		if(igtree_IE)
			igtree_editControl.value=src.innerText;
		else
			igtree_editControl.value=src.innerHTML;

        igtree_editControl.style.position = "";
        
        

	    var currentStyle = ig_csom.getRuntimeStyle(src);
	    if(currentStyle != null)
	    {
	        igtree_editControl.style.fontFamily = currentStyle.fontFamily;
	        igtree_editControl.style.fontSize = currentStyle.fontSize;
	    }
	    
        var eEditParent = igtree_editControl.parentNode;
        if(eEditParent != null)
            eEditParent.removeChild(igtree_editControl);
            
		var eParent = src.parentNode;
		ts.eEditTextSpan = src.cloneNode(true);		
		src.parentNode.removeChild(src);
		eParent.appendChild(igtree_editControl);

        var width = igtree_editControl.value.length + 2;
        igtree_editControl.setAttribute("size", width);	
		igtree_editControl.style.height = height + "px";
		
		


		
		igtree_editControl.setAttribute("currentNode", nodeId);
		igtree_editControl.setAttribute("oldInnerText", src.innerText);
		igtree_editControl.style.display="";
		
		igtree_editControl.focus();
		igtree_editControl.select();
		igtree_fireEvent(tn,igtree_treeState[tn].Events.AfterBeginNodeEdit,"(\""+tn+"\",\""+nodeId+"\")");
	}
}

// public - Ends editing of the current node
function igtree_endedit(accept) {
	if(!igtree_editControl || igtree_editControl.style.display=="none")
		return;
	if(igtree_editControl.endEdit)
		return;
	igtree_editControl.endEdit = true;
	
	var nodeId = igtree_editControl.getAttribute("currentNode");
	var eNode = igtree_getElementById(nodeId);
	var node=igtree_getNodeById(nodeId);
	var ts = igtree_treeState[node.getTreeId()];
    var eParent = igtree_editControl.parentNode;
    eParent.removeChild(igtree_editControl);
    eParent.appendChild(ts.eEditTextSpan);
	var span = igtree_getNodeSpan(eNode);
    
	
	if(igtree_fireEvent(node.getTreeId(),ts.Events.BeforeEndNodeEdit,"(\""+node.getTreeId()+"\",\""+node.getElement().id+"\",\""+igtree_editControl.value.replace(/\"/g,"\\\"")+"\")"))
	{
		igtree_editControl.endEdit = false;
		ts.Element.appendChild(igtree_editControl);
		

		igtree_hideEditControl()
		return true;
	}
	if(span && accept) {
		var nodeId = node.Id;
		var treeName = nodeId;
		var strArray = treeName.split("_");
		treeName = strArray[0];
		
		var newText = igtree_editControl.value;
		if(igtree_fireEvent(treeName,ts.Events.BeforeNodeUpdate,"(\""+treeName+"\",\""+nodeId+"\",\""+newText.replace(/\"/g,"\\\"")+"\")")) {
			igtree_editControl.endEdit = false;
			ts.Element.appendChild(igtree_editControl);
			

			igtree_hideEditControl()
			return;
		}
		if(newText == "")
			newText = " ";
		node.setText(newText);
		igtree_fireEvent(treeName,ts.Events.AfterNodeUpdate,"(\""+treeName+"\",\""+nodeId+"\")");
		



	}
	else if(span)
		span.innerText=igtree_editControl.getAttribute("oldInnerText");

	ts.Element.appendChild(igtree_editControl);
	

	igtree_hideEditControl()
	igtree_editControl.endEdit = false;
	igtree_editControl=null;
	if(igtree_fireEvent(node.getTreeId(),igtree_treeState[node.getTreeId()].Events.AfterEndNodeEdit,"(\""+node.getTreeId()+"\",\""+node.getElement().id+"\")")) {
		return;
	}
		
	if(ts.NeedPostBack)	{
		ts.NeedPostBack=false;
		__doPostBack(ts.UniqueId,node.element.id+":Edit");
		return;
	}

	node.Element.focus();
}




// public - Marks a tree for postback to the server.  At the completion of the current event, 
// the page will be posted.
function igtree_needPostBack(tn)
{
	igtree_treeState[tn].NeedPostBack=true;
}

// public - Cancels a pending postback
function igtree_cancelPostBack(tn)
{
	igtree_treeState[tn].CancelPostBack=true;
}

// private - Initializes the tree object on the client
function igtree_initTree(treeId) 
{

    
	var tree=igtree_getTreeById(treeId);
	
	
	
	//if(tree)
	//	igtree_pageUnload();
	if(tree && typeof document != 'unknown')
	{
		if(tree.stateItems)
			igtree_removeState(tree.stateItems);
		delete igtree_treeState[treeId];
	}
	var treeElement = igtree_getElementById("T_"+treeId);
	// Create the tree object and assign it to the tree variable on the html page
	var tree = new igtree_tree(treeId, treeElement,eval("igtree_"+treeId+"_Tree"));
	treeElement.igtree = tree;
	igtree_fireEvent(treeId,tree.Events.InitializeTree,"(\""+treeId+"\");");
	tree.TreeLoaded=true;
	return tree;
}
function igtree_initLevel() {

}

// private - Ensure that any tree's that were interacted with on the client have their states reset based on
// post data field.
function igtree_Load(e)
{   
    var o; 
	for(o in igtree_treeState)
	{	    
		if(igtree_treeState[o])
		    

		    if(igtree_treeState[o]._load != null)
		        igtree_treeState[o]._load();			
	}
	return;	
}

// private - constructor for the tree object
function igtree_tree(treeId, _treeElement,treeProps)
{

	igtree_treeState[treeId]=this;
	igtree_indexState[igtree_indexState.length] = this;
	this.treeElement = _treeElement;
	this.treeId = treeId;
	this.Id = treeId;
	
	this._requestedPostBack = false;
	
	this.isInitializing;
	
	ig_shared.addEventListener(window, "load", igtree_Load );
	
	this.update = function(propName, propValue) {
	    var elemState = ig_csom.getElementById(this.UniqueId);
		if(this.suspendUpdates == true)
			return;
		ig_ClientState.setPropertyValue(this.treeState,propName,propValue);
		
	    if(this.postField!=null)
	    {
	          
			this.postField.value = ig_ClientState.getText(this.stateItems);		
	    }
	}

	this.Element = _treeElement;
	this.UniqueId=treeProps[0];
	this.HiliteClass=treeProps[1];
	this.HoverClass=treeProps[2];
	this.ExpandImage=treeProps[3];
	this.CollapseImage=treeProps[4];
	this.Selectable=treeProps[5];
	this.Editable=treeProps[6];
	this.ImageDirectory=treeProps[7];
	this.ClassicTree=treeProps[8];
	this.SingleBranchExpand=treeProps[9];
	this.LoadOnDemand=treeProps[10];
	this.RenderAnchors=treeProps[11];
	this.DefaultSelectedImage=treeProps[12];
	this.DefaultImage=treeProps[13];
	this.DisabledClass=treeProps[14];

	this.getSelectedNode=igtreem_getSelectedNode;
	this.setSelectedNode=igtreem_setSelectedNode;
	this.getNodeById=igtree_getNodeById;
	this.getNodes=igtree_getTreeNodes;
	this.clearNodes=igtree_clearNodes;
	this.getClientUniqueId=igtree_getClientUniqueId;
	this.insertRoot=function(beforeNode, text, className) {
		return this._insert(null, beforeNode, text, className);
	}
	this.addRoot=function(text, className) {
		return this._insert(null, -1, text, className);
	}
	this._insert=igtree_insertChild;
	this.endEdit=function(acceptChanges) {
		igtree_endedit(acceptChanges);
	}

	var uniqueId = this.getClientUniqueId();
	this.Events=new igtree_events(eval("igtree_"+uniqueId+"_Events"));
	this.Levels=eval("igtree_"+uniqueId+"_Levels");
	this.Levels.getItem = function(index) {
		for(i=0;i<this.length;i++) {
			if(this[i][0] == index) {
				var level = new igtree_initLevel();
				level.Index = index;
				level.LevelCheckBoxes = this[i][1]
				level.LevelClass = this[i][2]
				level.LevelHiliteClass = this[i][3]
				level.LevelHoverClass = this[i][4]
				level.LevelImage = this[i][5]
				level.LevelIslandClass = this[i][6]
				return level;
			}
		}
		return null;
	}

	this.scrollnodeId=treeProps[16];
	this.scrolltop=treeProps[17];
	this.Enabled=treeProps[18]
	this.TargetUrl=treeProps[19]; 
	this.TargetFrame=treeProps[20]; 
	this.AllowDrag=treeProps[21];
	this.AllowDrop=treeProps[22];
	this.Indentation=treeProps[23];
	this.CheckBoxes=treeProps[24];
	this.RootNodeClass=treeProps[25]; 
	this.ParentNodeClass=treeProps[26]; 
	this.LeafNodeClass=treeProps[27]; 
	this.RootNodeImageUrl=treeProps[28]; 
	this.ParentNodeImageUrl=treeProps[29]; 
	this.LeafNodeImageUrl=treeProps[30];
    this.Expandable=treeProps[31];
	 
// V20061	
    if(treeProps.length > 31 ) {
	    this._version = treeProps[32];
	    var images = treeProps[33];
	    if(images.length > 0) {
			


	        this._ominus = encodeURI(treeProps[33][0]);
	        this._fminus = encodeURI(treeProps[33][1]);
	        this._lminus = encodeURI(treeProps[33][2]);
	        this._mminus = encodeURI(treeProps[33][3]);
	        this._oplus = encodeURI(treeProps[33][4]);
	        this._fplus = encodeURI(treeProps[33][5]);
	        this._lplus = encodeURI(treeProps[33][6]);
	        this._mplus = encodeURI(treeProps[33][7]);
	        this._s = encodeURI(treeProps[33][8]);
	        this._f = encodeURI(treeProps[33][9]);
	        this._l = encodeURI(treeProps[33][10]);
	        this._t = encodeURI(treeProps[33][11]);
	        this._i = encodeURI(treeProps[33][12]);
	        this._w = encodeURI(treeProps[33][13]);
	    }
    	
	    this.NodeMargins = treeProps[34];
	    this.NodePaddings = treeProps[35];
	    this.NodeClass = treeProps[36];
	}
    if(treeProps.length > 38 ) {
	    this.LoadOnDemandPrompt = treeProps[37];
	    this.ExpandImagesVisible = treeProps[38];
	    this.ExpandOnClick = treeProps[39];
	    this.ExpandAnimation = treeProps[40];
	}
	else {
	    this.LoadOnDemandPrompt = "";
	    this.ExpandImagesVisible = true;
	    this.ExpandOnClick = false;
	    this.ExpandAnimation = 0;
	}
// End V20061 


    
    if (this.Element.style.height=="")
        this.Element.style.overflow='hidden';


// V20062 
    if(treeProps.length > 42 ) {
		this.FullNodeSelect = treeProps[41];
		this.MergeStyles = treeProps[42];
	}
// End V20062

	this.TreeLoaded=false;
	var nodeId=treeProps[15];
	if(nodeId && nodeId.length>0) {
		igtree_setSelectedNode(uniqueId, nodeId);
	}
  	
	this.NeedPostBack=false;
	this.CancelPostBack=false;
	
	if(this.AllowDrag) {
		ig_csom.addEventListener(this.Element, "mousedown", igtree_preselect, true);
	}
	// clientViewState
	this.postField	= ig_csom.getElementById(igtree_getUniqueId(treeId));	
	var agt=navigator.userAgent.toLowerCase();
	//if(!(ig_csom.IsIE5 &&	(agt.indexOf("msie 5.0")!=-1))){
		this.stateItems	= ig_ClientState.createRootNode();
		this.treeState	= ig_ClientState.addNode(this.stateItems,"WebTree");
		this.nodeState	= ig_ClientState.addNode(this.treeState,"Nodes");
	//}
	

	 	
// V20061
	this.doLoadOnDemand = function(tree, node, currentNodeText) {
	    var index = node.Id.indexOf("_");
	    var nodeId = node.Id.substring(index);
	    
	    // Construct the parent display chain for the treelines
	    var nodeDisplayChain = "";
	    if(tree.ClassicTree){
	        var parent = node;
	        while(parent != null) {
	            var next;
	            next = parent.getNextSibling();
                nodeDisplayChain += (next != null) ? "1" : "0";
	            parent = parent.getParent();
	        }       
	    }
	     var clientContext = {operation:"LoadOnDemand", clientId:tree.Id, requestType:"html", nodeId:node.Id, currentLoadingNodeText:currentNodeText};
	    var serverContext = {serverId:tree.UniqueId, nodeId:nodeId, dataPath:node.getDataPath(), level:node.getLevel(), displayChain:nodeDisplayChain};
	    
	    var smartCallback = new ig_SmartCallback(clientContext, serverContext, null, tree.UniqueId, this)
	    smartCallback.execute();
	    
	}
	
	this.callbackRender = function (payload, context ){
	    

	    igtree_displayLoadPrompt = false;
	    var tree = igtree_getTreeById(context.clientId);
	    var parentNode = igtree_getNodeById(context.nodeId);
	    var tmp = payload;
	    var error = false;
	    var response;
        var subnodesId = "M_" + parentNode.Element.id;
        var subnodes = document.getElementById(subnodesId);
	    
	    if(tmp != null && tmp.substring(0, 9) == "Exception")
	        error = true;
	    else  { 
            tmp = tmp.replace(/\^\^/g, "\"");
	        response = tmp; //eval(tmp);
        }
        
 
		if(response == null || response.length == 0 || response.substring(0,6) == "NoData") 
		    error = true;
		    
		if(error == true) {
		    parentNode._loadingNodes = true;
		    if(tmp.substring(0, 9) == "Exception" && tmp.length > 10) 
		        parentNode.addChild(tmp.substring(10));
		    else
			    parentNode.addChild("No Data Returned from Server");
            if(context.currentLoadingNodeText != null)
                
                parentNode.setHtml(context.currentLoadingNodeText);
		    parentNode._loadingNodes = null;
       	    
			if(tree.ExpandAnimation == AnimationRateEnum.Static || tree.Element.offsetHeight == 0 || !tree.TreeLoaded) {
				subnodes.style.display = "";
			}
			else {
   				var animate = new ig_RevealAnimation();
				animate.setElement(subnodes);
				animate.setRate(tree.ExpandAnimation);
				animate.setDirection(AnimationDirectionEnum.Down);
				animate.finishPos = subnodes.scrollHeight;
				animate.startPos = 0;
       			subnodes.style.height = 1;
       			subnodes.style.display = "";
				animate.finishPos = subnodes.scrollHeight;
    			animate.play();
    		}	
	        return;
	    }
	        
	    var html = response; //[0]; 
        html = html.replace(/\^/g, "\"");

	    if(subnodes != null) {
            subnodes.innerHTML = html;
        }
    	if(tree.SingleBranchExpand) 
	        igtree_showSingleBranch(tree, parentNode);

        if(context.currentLoadingNodeText != null)
            
            parentNode.setHtml(context.currentLoadingNodeText);
         
       	parentNode._loadingNodes = null;
       	
		if(tree.ExpandAnimation == AnimationRateEnum.Static || tree.Element.offsetHeight == 0 || !tree.TreeLoaded) {
			subnodes.style.display = "";
		}
		else {
  			var animate = new ig_RevealAnimation();
			animate.setElement(subnodes);
			animate.setRate(tree.ExpandAnimation);
			animate.setDirection(AnimationDirectionEnum.Down);
			animate.finishPos = subnodes.scrollHeight;
			animate.startPos = 0;
       		subnodes.style.height = 1;
       		subnodes.style.display = "";
			animate.finishPos = subnodes.scrollHeight;
    		animate.play();	
    	}
    	return;	
        
	}
// End V20061	
   

 
	this._load=function()
	{
	    var tree = this;
	    var elemState = ig_csom.getElementById(this.UniqueId);
	    if(elemState != null)
		{
		    var v=unescape(elemState.value);
		    if(v != null && v.length > 0)
			{
			    var nodesStrIndex = v.indexOf("Nodes", 0);  
			    // code for IE
                if (window.ActiveXObject)
                {
                    var doc=new ActiveXObject("Microsoft.XMLDOM");
                    doc.async="false";
                    doc.loadXML(v);
                }
                // code for Mozilla, Firefox, Opera, etc.
                else
                {
                    var parser=new DOMParser();
                    var doc=parser.parseFromString(v,"text/xml");
                }

                var x=doc.documentElement;
                
                if(x!=null)
                {
                    if(x.childNodes[0].tagName == "Nodes")
                    {
                        tree.isInitializing = true;
                        for(var k = 0; k<x.attributes.length; k++)
                        {
                          var n = igtree_getNodeById(x.attributes[k].value);
                          switch(x.attributes[k].name)
                          {
                                    
                            case "SelectedNode":
                                if(n.getSelected() != Boolean(x.attributes[k].value))
	                                n.setSelected(Boolean(x.attributes[k].value));
	                            break;
	                                
                          }  
                        }
                        var nodesDoc = x.childNodes[0].childNodes;
                        for(var i = 0; i< nodesDoc.length; i++)
                        {
                            var n = igtree_getNodeById(nodesDoc[i].tagName);
                            
                            if(n != null)
                            {
                                for(var j = 0; j<nodesDoc[i].attributes.length; j++)
                                {
                                
                                    switch(nodesDoc[i].attributes[j].name)
                                    {
                                        case "Expanded":
                                            if(n.getExpanded() != Boolean(nodesDoc[i].attributes[j].value))
                                                n.setExpanded(Boolean(nodesDoc[i].attributes[j].value));
                                            break;    
                                        case "Checked":
                                            n.setChecked(Boolean(nodesDoc[i].attributes[j].value));
                                            break;
                                        case "Text":
	                                        n.setText(unescape(nodesDoc[i].attributes[j].value));
	                                        break;
                                    }
                                }
                            } 
                        }
                        tree.isInitializing = false;
                        this._reloadTree = false;
                    }                    
                }
			}
			
			


			    
			else if(tree.LoadOnDemand == 1 || tree.LoadOnDemand == 2)
		    {
		        var clientState=eval("igtree_"+uniqueId+"_cs");
	            for(i=0;i<clientState.length;i++) {
		            var node = clientState[i];
		            if(!node)
			            break;
		            var id = uniqueId + node[0];
		            var action = node[1];
		            switch(action) {
			            case 0 :
				            this.update("SelectedNode", id);
				            break;
			            case 1 :
				            igtree_updateNodeToggle(this, id, true);
				            break;
			            case 2 :
				            igtree_updateNodeToggle(this, id, false);
				            break;
			            case 3 :
				            igtree_updateNodeCheck(this, id, true)
				            break;
			            case 4 :
				            igtree_updateNodeCheck(this, id, false)
				            break;
		            }
	            }
	        }
		}
	}
}

function igtree_loadcomplete() {
	for(i=0; i<igtree_indexState.length; i++) {
		var tree = igtree_indexState[i];
		var eNode = ig_csom.getElementById(tree.scrollnodeId);
		if(eNode) {
			igtree_scrollToView(tree.Element,eNode)
		}
	}
}
// private - initializes the client-side events for the Tree object
function igtree_events(events)
{
	this.AfterBeginNodeEdit=events[0];
	this.AfterEndNodeEdit=events[1];
	this.AfterNodeSelectionChange=events[2];
	this.AfterNodeUpdate=events[3];
	this.BeforeBeginNodeEdit=events[4];
	this.BeforeEndNodeEdit=events[5];
	this.BeforeNodeSelectionChange=events[6];
	this.BeforeNodeUpdate=events[7];
	this.NodeChecked=events[8];
	this.EditKeyDown=events[9];
	this.EditKeyUp=events[10];
	this.InitializeTree=events[11];
	this.KeyDown=events[12];
	this.KeyUp=events[13];
	this.NodeClick=events[14];
	this.NodeCollapse=events[15];
	this.NodeExpand=events[16];
	this.DemandLoad=events[17];
	this.Drag=events[18];
	this.DragEnd=events[19];
	this.DragEnter=events[20];
	this.DragLeave=events[21];
	this.DragOver=events[22];
	this.DragStart=events[23];
	this.Drop=events[24];
}

// private
function igtree_getNodeSpan(node)
{
	if(!node)
		return null;
    
    var span = null;
	if(node.childNodes.length!=0)
	    span=node.childNodes[node.childNodes.length-1];

	while(span && span.tagName!="SPAN")
		span=span.previousSibling;
	return span;
}

function igtree_getSrcNodeElement(evnt,tn)
{
	var src=igtree_getSrcElement(evnt);
	var parent = src.parentNode;
	while(parent != null) {
		if(parent.id != null && parent.id.length > 0)
			return src;
		if(src.tagName=="IMG" && src.getAttribute("imgType")=="exp")
			return src;
		if(src.tagName=="IMG" && src.getAttribute("igimg")=="1")
			return src;
		if(src.tagName=="INPUT" || src.tagName=="SPAN")
			return src;
		if(src.tagName=="DIV")
			return src;
			
		src = parent;
		parent = parent.parentNode;
	}
	return null;
	
}
function igtree_getNodeElement(src)
{
	var parent = src;
	while(parent) {
		if(ig_csom.notEmpty(parent.id))
			return parent;
		parent = parent.parentNode;
	}
	return null;
}

function igtree_pageUnload(){
	if(ig_csom.IsIE55Plus) {
	    	
		
		for(var treeId in igtree_treeState)
		{
			try
			{
				if(typeof(document)!=='unknown')
				{					
					igtree_removeState(igtree_getTreeById(treeId).stateItems);
				}
			}
			catch(e)	
			{		
			}			
		}
		
		ig_delete(igtree_treeState);
		ig_delete(igtree_indexState);
		ig_delete(igtree_nodeState);
	}
}


function igtree_removeState(stateNode)
{
    while(stateNode.childNodes.length > 0)
        igtree_removeState(stateNode.childNodes[0]);
   
    if(stateNode.parentNode)
    {
		if(typeof(stateNode.parentNode.removeChild) != "undefined") 
			stateNode.parentNode.removeChild(stateNode); // IE
		else 
			stateNode.parentNode.removeNode(stateNode); // Firefox
    }
    
    // removeNode isn't enough to make the node deallocate in IE
    if(typeof(stateNode.outerHTML) != "undefined")
        stateNode.outerHTML = "";
}  


if(typeof(ig_csom)!="undefined" && ig_csom.IsIE)
	ig_csom.addEventListener(window, "unload", igtree_pageUnload, true);

// private - toggles the expansion state of a node.
function igtree_toggle(tn, nodeId)
{
	var node=igtree_getNodeById(nodeId);
	var subnodes = igtree_getElementById("M_"+nodeId);
	var ts=igtree_treeState[tn];

	
	if (ig_csom.IsIE8)
	{
		if (typeof(subnodes._expanded) != "undefined")
		{
			node.setExpanded(!subnodes._expanded);
			return;
		}
	}
	
	if(!node.getEnabled()) 
		return;
		
	// V20061
	if(!subnodes && (ts.LoadOnDemand == 1 || ts.LoadOnDemand == 2)) {
	// End V20061
		node.setExpanded(true);
		return;
	}
	if(subnodes != null) {
	    if(subnodes.style.display == "none") {
		    node.setExpanded(true);
	    }
	    else
		    node.setExpanded(false);
	}
	return;		
}

// private - Implements the Collapse() method for the Node object
function igtree_collapseNode(node) {
	var tn=node.getTreeId();
	var ts=igtree_treeState[tn];
	var s;
	s = igtree_getElementById("M_"+node.element.id);
	if(!s)
		return;
	var oNode = ts.getSelectedNode();
	if(oNode != null && ts.Events.AfterNodeSelectionChange[1] == 0) {
		var parent = oNode.getParent();
		while(parent != null) {
			if(parent.element.id == node.element.id)
				node.setSelected(true);
			parent = parent.getParent();
		}
	}
	if(igtree_fireEvent(tn,ts.Events.NodeCollapse,"(\""+tn+"\",\""+node.element.id+"\")"))
		return;
	if(ts.NeedPostBack && !ts.isInitializing)	{
		if(ts._FreezeServerEvents==null)
		{
			ts.NeedPostBack=false;
			__doPostBack(ts.UniqueId,node.element.id+":Collapse");
			return;
		}
	}
	
	if(ts.ExpandAnimation == AnimationRateEnum.Static || ts.Element.offsetHeight == 0 || !ts.TreeLoaded) {
		s.style.display = "none";
	}
	else {
		
		if(!ig_csom.IsStandardsMode)
		{	
			var currentSubnodeWidth = s.style.width;
			s.style.width = s.parentNode.offsetWidth + "px";
		}
   		var animate = new ig_RevealAnimation();
		animate.setElement(s);
		animate.setRate(ts.ExpandAnimation);
		animate.setDirection(AnimationDirectionEnum.Up);
		animate.finishPos = 1;
		animate.startPos = s.scrollHeight;
		animate.play();	
    }	
   	
	igtree_updateNodeToggle(ts, s.id, false);

	var button = igtree_getNodeExpandCollapseImage(ts, node);
	if(!button)
		return;
	
	if(ts.ClassicTree){
	     

		 if(button.src.toLowerCase().indexOf(ts._ominus.toLowerCase().replace("../", "/")) != -1)
			button.src = button.src.toLowerCase().replace(ts._ominus.toLowerCase().replace("../", "/"), ts._oplus.replace("../", "/"));
		 else if(button.src.toLowerCase().indexOf(ts._fminus.toLowerCase().replace("../", "/")) != -1)	
			button.src = button.src.toLowerCase().replace(ts._fminus.toLowerCase().replace("../", "/"), ts._fplus.replace("../", "/"));
		 else if(button.src.toLowerCase().indexOf(ts._lminus.toLowerCase().replace("../", "/")) != -1)
			button.src = button.src.toLowerCase().replace(ts._lminus.toLowerCase().replace("../", "/"), ts._lplus.replace("../", "/"));
		 else if(button.src.toLowerCase().indexOf(ts._mminus.toLowerCase().replace("../", "/")) != -1)
		    button.src = button.src.toLowerCase().replace(ts._mminus.toLowerCase().replace("../", "/"), ts._mplus.replace("../", "/"));
	}
	else{
		image = ts.ExpandImage;
		if(image == "")
			image = "ig_treeplus.gif";
		button.src = image;
	}
	var alt = button.getAttribute("alt");
	if(alt != null){
	    var clpseAlt = button.getAttribute("igAltC");
	    if(clpseAlt != null){
	        button.setAttribute("igAltX",alt);
	        button.setAttribute("alt",clpseAlt);
	        button.removeAttribute("igAltC");
	    }
	}
}

// Private - returns the expand image for a node
function igtree_getNodeExpandCollapseImage(tree, node) {
	var index = 1;
	var button
	if(tree.ClassicTree) {
		index = 0;
		button=node.element.childNodes[0].childNodes[index];
	}
	else
		button=node.element.childNodes[index];
	if(button.tagName!="IMG" || button.getAttribute("imgType")!="exp"){
		while (button!=null && (button.tagName!="IMG" || button.getAttribute("imgType")!="exp")) {
			button=button.nextSibling;
		}
		if(button==null)
			return;
	}
	return button; 
}

// Private - Implements the Expand() method for the Node object
function igtree_expandNode(node) {
	var tn=node.getTreeId();
	var ts=igtree_treeState[tn];
	
	var button = igtree_getNodeExpandCollapseImage(ts, node);
   	var subnodes;
// V20061	
    subnodes = igtree_getElementById("M_" + node.element.id);
    if(!subnodes)
		return;
	if(ts.LoadOnDemand >= 1 && !node.getPopulated())
	{
	    if(ts.LoadOnDemand == 1 || ts.LoadOnDemand == 2) {
		    igtree_updateNodeToggle(ts, "M_"+node.element.id, true);
		    if(igtree_fireEvent(tn,ts.Events.DemandLoad,"(\""+tn+"\",\""+node.element.id+"\")"))
			    return;
		    if(ts.NeedPostBack && !ts.isInitializing)	{
			    __doPostBack(ts.UniqueId,node.element.id+":DemandLoad");
			    return;
		    }
		    return;
		}

	    var animation = constUseAnimation;
	    if(igtree_fireEvent(tn,ts.Events.NodeExpand,"(\"" + tn + "\",\"" + node.element.id + "\")"))
		    return;
	    if(igtree_fireEvent(tn,ts.Events.DemandLoad,"(\"" + tn + "\",\"" + node.element.id + "\")"))
		    return;
		ts.NeedPostBack = false;
	    igtree_updateNodeToggle(ts, subnodes.id, true);

        var currentLoadingNodeText;
        if(ts.LoadOnDemandPrompt.length > 0) {
    	    currentLoadingNodeText = node.__getText(true);
			var e = ig_getNodeTextElement(node);
			try{
			    






			    e.innerHTML = "";			
			    igtree_displayLoadPrompt = true;			                   
			    setTimeout(ig_createCallback(function(){			     
			        try{ if(igtree_displayLoadPrompt){e.innerHTML = this.text;}}catch(ex){}}, 
			        {e:e, text:ts.LoadOnDemandPrompt}), 0);
    								
			}catch(ex){}
    	}
    	igtree_showCollapseImage(ts, button);
    	
        ts.doLoadOnDemand(ts, node, currentLoadingNodeText);
        node.setPopulated(true);
  
	    return;
	}
    
	if(igtree_fireEvent(tn,ts.Events.NodeExpand,"(\""+tn+"\",\""+node.element.id+"\")"))
		return;

	if(ts.NeedPostBack && !ts.isInitializing){
		ts.NeedPostBack=false;
		__doPostBack(ts.UniqueId,node.element.id+":Expand");
		return;
	}


	igtree_showCollapseImage(ts, button);
	
	if(ts.SingleBranchExpand) 
	    igtree_showSingleBranch(ts, node);
	  
	if(ts.ExpandAnimation == AnimationRateEnum.Static || ts.Element.offsetHeight == 0 || !ts.TreeLoaded) {
		subnodes.style.display = "";
	}
	else {
		
		if(!ig_csom.IsStandardsMode)
		{		
			var currentSubnodeWidth = subnodes.style.width;
			subnodes.style.width = subnodes.parentNode.offsetWidth;
		}				
   		var animate = new ig_RevealAnimation();
		animate.setElement(subnodes);
		animate.setRate(ts.TreeLoaded ? ts.ExpandAnimation : AnimationEnum.None);
		animate.setDirection(AnimationDirectionEnum.Down);
		animate.startPos = 0;
		subnodes.style.height = 1;
		subnodes.style.display = "";
		animate.finishPos = subnodes.scrollHeight;
		animate.play();
    }		
	
	if(button != null) {
		var alt = button.getAttribute("alt");
		if(alt!=null){
			var xpandAlt = button.getAttribute("igAltX");
			if(xpandAlt!=null){
				button.setAttribute("igAltC",alt);
				button.setAttribute("alt",xpandAlt);
				button.removeAttribute("igAltX");
			}
		}
	}

	igtree_updateNodeToggle(ts, subnodes.id, true);
}

// V20061

function igtree_showSingleBranch(tree, node) {
	tree._FreezeServerEvents = true;
	var prev = node.getPrevSibling();
	while(prev != null) {
		prev.setExpanded(false);
		prev = prev.getPrevSibling();
	}
	var next = node.getNextSibling();
	while(next != null) {
		next.setExpanded(false);
		next = next.getNextSibling();
	}
	tree._FreezeServerEvents = null;
}

function igtree_showCollapseImage(tree, button) {
    if(button == null)
        return;
	if(tree.ClassicTree) {
	    

		if(button.src.toLowerCase().indexOf(tree._oplus.toLowerCase().replace("../", "/")) != -1)
			button.src = button.src.toLowerCase().replace(tree._oplus.toLowerCase().replace("../", "/"), tree._ominus.replace("../", "/"));
		else if(button.src.toLowerCase().indexOf(tree._fplus.toLowerCase().replace("../", "/")) != -1)
			button.src = button.src.toLowerCase().replace(tree._fplus.toLowerCase().replace("../", "/"), tree._fminus.replace("../", "/"));
		else if(button.src.toLowerCase().indexOf(tree._lplus.toLowerCase().replace("../", "/")) != -1)
			button.src = button.src.toLowerCase().replace(tree._lplus.toLowerCase().replace("../", "/"), tree._lminus.replace("../", "/"));
		else if(button.src.toLowerCase().indexOf(tree._mplus.toLowerCase().replace("../", "/")) != -1)
			button.src = button.src.toLowerCase().replace(tree._mplus.toLowerCase().replace("../", "/"), tree._mminus.replace("../", "/"));
	}
	else {
		image = tree.CollapseImage;
		if(image == "")
			image = "ig_treeminus.gif";
		button.src = image;
	}
}

// End V20061

// private - Handles checkbox clicking within the tree.
function igtree_checkboxClick(tn, nodeId, src)
{
	var ts=igtree_treeState[tn];
	var node=igtree_getNodeById(nodeId);
	if(src.checked) {
		if(igtree_fireEvent(tn,ts.Events.NodeChecked,"(\""+tn+"\",\""+nodeId+"\", true)")) {
			src.checked = false;
			return;
		}
		if(ts.NeedPostBack && !ts.isInitializing)	{
			__doPostBack(ts.UniqueId,nodeId+":Checked");
			return;
		}
		igtree_updateNodeCheck(ts, nodeId, true);
	}
	else {
		if(igtree_fireEvent(tn,ts.Events.NodeChecked,"(\""+tn+"\",\""+nodeId+"\", false)")) {
			src.checked = true;
			return;
		}
		if(ts.NeedPostBack && !ts.isInitializing)	{
			__doPostBack(ts.UniqueId,nodeId+":Unchecked");
			return;
		}
		igtree_updateNodeCheck(ts, nodeId, false);
	}
}

// private - Retrieves the resolved hover class for an item in the tree.
function igtree_getResolvedHoverClass(tree,node)
{
	if(node.getAttribute("HoverClass"))
		return node.getAttribute("HoverClass");
	return tree.HoverClass;
}

// private - Handles the mouse over event for the tree.
function igtree_mouseover(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
		
	var tree=igtree_treeState[tn];
	var eNode = igtree_getNodeElement(src);
	var node=igtree_getNodeById(eNode.id);
	if(node == null || !node.getEnabled())
		return;
	if(eNode.id == "T_" + tn)
		return;
	if(eNode.id.indexOf("M_") == 0)
		return;
	if(eNode == igtree_selectedNode(tn))
		return;

	var className=igtree_getResolvedHoverClass(tree,eNode);

	if(tree.FullNodeSelect == true) {
		node.hover();
	}
	else {
		if(src.tagName != "SPAN")
			return;

	    if(igtree_IE && tree.Element.clientWidth > 0 && eNode.offsetWidth > tree.Element.clientWidth) 
		    if(ig_csom.isEmpty(eNode.title)) {
			    src.title=src.innerText;
			    eNode.igtitle = true;
		    }

		if(className=="" || src.className == className)
			return;
		if(igtree_IE && tree.Element.clientWidth>0 && eNode.offsetWidth>tree.Element.clientWidth) 
			if(ig_csom.isEmpty(eNode.title)) {
				src.title=src.innerText;
				eNode.igtitle = true;
			}
		var igtxt = src.getAttribute("igtxt");	
		if(igtxt!=null && igtxt.length>0) {
			src.HovClass = src.className;
			src.hoverSet = true;
			if(tree.MergeStyles)
				src.className += " " + className;
			else
				src.className = className;
		}
	}
}

// private - Handles the mouse out event for the tree
function igtree_mouseout(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	var eNode = igtree_getNodeElement(src);
	if(eNode.id == "T_" + tn)
		return;
	if(eNode.id.indexOf("M_") == 0)
		return;

	if(eNode.igtitle) {
		src.title="";
		eNode.igtitle = null;
	}
	
	var tree=igtree_treeState[tn];
	var node=igtree_getNodeById(eNode.id);
	if(!node.getEnabled())
		return;
	if(eNode == igtree_selectedNode(tn))
		return;
		
	if(tree.FullNodeSelect == true) {
		node.unHover();
	}
	else {
		if(src.tagName!="SPAN")
			return;
	
		var igtxt = src.getAttribute("igtxt");	
		if(igtxt==null || igtxt.length==0) {
			return;
		}
		if(src.style != null) {
			if(src.hoverSet) {
				prevClass =	src.HovClass;
				if(prevClass == null)
					prevClass = "";
				src.className = prevClass;
				src.hoverSet = null;
			}
		}
	}
}

// private - Handles the right click event for the tree.
function igtree_contextmenu(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var ts=igtree_treeState[tn];
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	if (src.tagName!="SPAN") return;

	var eNode = igtree_getNodeElement(src);
	ts.event = evnt;

	if(igtree_fireEvent(tn,ts.Events.NodeClick,"(\""+tn+"\",\""+eNode.id+"\", 2)")) {
		ig_cancelEvent(evnt);
		return false;
	}
	ts.event = null;
}

// private - Retieves the resolved HiliteClass for a node in the tree.
function igtree_getResolvedHiliteClass(tn,src)
{
	if(!src)
		return "";
	if(src.getAttribute("HiliteClass"))
		return src.getAttribute("HiliteClass");
	if(igtree_treeState[tn].HiliteClass!="")
		return igtree_treeState[tn].HiliteClass;
	return tn+"HiliteClass";
}

function igtree_navigate(tree, node) {
	if(!node.getEnabled())
		return;
	if(!node.WebTree || !node.WebTree.Enabled)
		return;

	if(node.getTargetUrl()==null)
	{
		if(igtree_fireEvent(tree.Id,tree.Events.NodeClick,"(\""+tree.Id+"\",\""+node.Id+"\",1)"))
			return;
		if(tree.NeedPostBack)
		{
			igtree_postNodeClick(tree.Id,node.Id);
			return;
		}
	}
	if(ig_csom.notEmpty(node.getTargetUrl()) && !tree.RenderAnchors) //&& 
	{
	    




	    var igUrl = node.getTargetUrl();
	    var url = unescape(igUrl);
        var index = url.indexOf("javascript:") ;
        if(index > 0)
            url = eval(igUrl);
        if(url == null)
            url = igUrl;
		ig_csom.navigateUrl(url,node.getTargetFrame());
	}
}

// private - Handles the click event for nodes
function igtree_nodeclick(evnt,tn)
{
	if(!igtree_treeState[tn])
		return;
	var tree=igtree_treeState[tn];
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	

	if(!tree.Enabled)
		return;
	var eNode = igtree_getNodeElement(src);

	
	if (ig_csom.IsIE8)
    {
    	var subnodes = igtree_getElementById("M_" + eNode.id);
    	
    	if (subnodes != null)
    		if (subnodes.scrollHeight == 0) //B.C. 8/19/2011 BUG #76001 When an element's content does not generate a vertical scrollbar, then its scrollHeight property is equal to its height.
    			subnodes.style.display = "none";
    }
	
	
	if(eNode.id.substring(0, 2) == "M_" && eNode.id.lastIndexOf("_") == 1)
		return;
	var node=igtree_getNodeById(eNode.id);
	if(node != null) {
		if(node.getEnabled() == false)
			return;
	}
	var igtxt = src.getAttribute("igtxt")!=null && src.getAttribute("igtxt").length>0;	
	var igimg = src.getAttribute("igimg")!=null && src.getAttribute("igimg").length>0;	
	var igchk = src.getAttribute("igchk")!=null && src.getAttribute("igchk").length>0;	
	var igtype = src.getAttribute("imgType") != null && src.getAttribute("imgType").length>0;	

	igtree_lastActiveTree=tn;
    
    var bToggled = false;
    if((tree.FullNodeSelect && !igtype) || igtxt || igimg)
	{
	    if(src == tree.Element)
	        return;
		if(tree.Selectable) {
		    
			if(igtree_setSelectedNode(tn,eNode.id) == null)
			{
			    
			    if(tree.ExpandOnClick && !bToggled && node.hasNodes())
	                igtree_toggle(tn,eNode.id);
			    return;
			}
			igtree_navigate(tree, node)			
		}
		if(igchk)
		    igtree_checkboxClick(tn, eNode.id, src);
	}
	else
	if(src.tagName=="IMG" && node.hasNodes())
	{
		bToggled = true;
		igtree_toggle(tn,eNode.id);
	}
	else if(igchk) {
		igtree_checkboxClick(tn, eNode.id, src);
	}
	
	// V20061	
    if(tree.ExpandOnClick && !bToggled && node.hasNodes())
	    igtree_toggle(tn,eNode.id);
	// V20061 end
	
	return false;
}

var igtree_treeName;
var igtree_nodeId;
var igtree_postCanceled = false;

var igtree_clickCounter = 0;

// private - Handles posting node click events to the server
function igtree_postNodeClick(treeName, nodeId)
{
	igtree_treeName = treeName;
	igtree_nodeId = nodeId;
	var src;
	src = igtree_getElementById(nodeId);
	igtree_clickCounter++;
	if(igtree_clickCounter == 1)
		setTimeout('igtree_onTimerPostNodeClick()', 300);
}

// private - Posts node click events on time expiration
function igtree_onTimerPostNodeClick() 
{
	var tree = igtree_getTreeById(igtree_treeName);	
	tree.update("SelectedNode", igtree_nodeId);
	if(igtree_postCanceled == false) {
		tree.NeedPostBack=false;
		


		if(!tree._requestedPostBack)
		    __doPostBack(tree.UniqueId, igtree_nodeId+":Clicked");
	}
	igtree_postCanceled = false;
	igtree_clickCounter = 0;
}

// private - Handles the scrolling of the tree
function igtree_onscroll(src)
{
	var treeName = src;
	var ts = igtree_getTreeById(src);	
	var treeControl = igtree_getElementById("T_" + treeName);
	ts.update("ScrollTopPos", treeControl.scrollTop);
	return true;
}
	
// private - Updates the PostBackData field
function igtree_updatePostField(treeName, nodeId, oldNodeId)
{
	var formControl = igtree_getElementById(igtree_getUniqueId(treeName));
	if(!formControl)
		return;
	
	formControl.value = treeState;
}

// private - Handles the double click event for a node
function igtree_dblclick(evnt,tn)
{
	var src=igtree_getSrcNodeElement(evnt,tn);
	if(!src)
		return;
	var tree=igtree_treeState[tn];
	var eNode = igtree_getNodeElement(src);
	var igtxt = src.getAttribute("igtxt")!=null && src.getAttribute("igtxt").length>0;	
	var igimg = src.getAttribute("igimg")!=null && src.getAttribute("igimg").length>0;	
	if(igtxt && tree.Editable)
	{
		igtree_postCanceled = true;
		igtree_beginedit(tn,eNode.id);
		return;
	}
	if(igimg || igtxt) {
		if(!tree.Enabled)
			return;

		var node=igtree_getNodeById(eNode.id);
		if(node.getFirstChild() != null || tree.LoadOnDemand>=1) {
			igtree_toggle(tn,eNode.id);
		}
	}
}

var igtree_editControl = null;
// private - Retrieves the in-place edit control for an editable tree
function igtree_getEditControl(src)
{
	var strArray;
	if(!src)
		return null;
	if(igtree_IE)
		strArray = src.parentElement.id.split("_");
	else
		strArray = src.parentNode.id.split("_");
	var treeName = strArray[0];
	return igtree_getElementById(treeName+"_tb");
}

//private - Hides the in-place edit control.
function igtree_hideEditControl()
{
    
    igtree_editControl.value = "";
	igtree_editControl.removeAttribute("currentNode");
	igtree_editControl.removeAttribute("oldInnerText");
	igtree_editControl.style.display = "none";
}

// private - Handles mouse clicks within the edit control
function igtree_editClickHandler()
{
	event.cancelBubble = true;
}

// private - Handles selection events for the tree
var igtree_bDragSelect = false;
function igtree_selectStart(event)
{
    if (!event) var event = window.event;
    var srcElement = event.srcElement ? event.srcElement : event.target; 
	if(srcElement.tagName == "INPUT")
		return;
	if(igtree_bDragSelect)
		return;
	event.cancelBubble = true; 
	event.returnValue = false; 
	return false;	
}

// private - Updates internal buffer for node expansion/collapse data
function igtree_updateNodeToggle(ts, nodeId, bExpanded)
{
	nodeId = nodeId.replace("M_", "");
	var node = igtree_getNodeById(nodeId);
	node.update("Expanded", bExpanded);
	
}
   
// private - Updates internal buffer for node checked status
function igtree_updateNodeCheck(ts, nodeId, bChecked){
	var node = igtree_getNodeById(nodeId);
	node.update("Checked", bChecked);

}


// private - Handles key down events
function igtree_keydown(evnt, treeID, squelchEvent)
{	
	var ts=igtree_treeState[treeID];
	var tree=ts.treeElement;
	

	if(igtree_editControl && ig_csom.IsFireFox)
		return;
	

	if(evnt.keyCode == 9 && evnt.shiftKey == false)
	{
		var elem = evnt.srcElement; 
		if(elem == null)
			elem = evnt.target; 
		if(elem.tagName == "INPUT" && elem.type == "checkbox")
		{
			var node = elem.nextSibling; 
			node.tabIndex = tree.tabIndex; 
			node.title = node.innerHTML;
			node.focus(); 
			ig_cancelEvent(evnt);
			
			node.tabIndex = 0;
			return;
		}
	}
	
	var processed=false;
	var igtree_currentNode = igtree_selectedNode(treeID);
	
	if(igtree_currentNode)
		tree.hideFocus = true;
	if(squelchEvent == null)	
	    if(igtree_fireEvent(treeID,ts.Events.KeyDown,"(\""+treeID+"\","+evnt.keyCode+")"))
		    return;
	if(evnt.keyCode == 13){  // Enter
		if(igtree_currentNode != null)	{
			processed=true;
			igtree_navigate(ts, ts.getSelectedNode());			
		}
	}
	if(evnt.keyCode == 113){  // F2
		if(igtree_currentNode != null)	{
			processed=true;
			if(ts.Editable)
				igtree_beginedit(treeID,igtree_currentNode.id);
		}
	}
	if(evnt.keyCode == 107 || evnt.keyCode == 109 || evnt.keyCode == 37 || evnt.keyCode == 39)
	{ // plus/minus key
		if(igtree_currentNode == null)
			return;
		var ns=igtree_getElementById("M_"+igtree_currentNode.id);
		if(!ns && ts.LoadOnDemand >= 1) {
			if(evnt.keyCode==107 || evnt.keyCode==39)
				igtree_toggle(treeID,igtree_currentNode.id);
			processed=true;
		}
		else
		if(ns)	{
			
			var toggle = ((evnt.keyCode == 107 || evnt.keyCode == 39) && ((ns.style.display == "none") || (ns._expanded == false)) || (evnt.keyCode == 109 || evnt.keyCode == 37) && ns.style.display == "");
			if(toggle)
				igtree_toggle(treeID,igtree_currentNode.id);
			processed=true;
		}
	}
	if(evnt.keyCode == 35){ // end key
		if(igtree_currentNode)	{
			var last = igtree_lastNode(treeID);
			if(last){
				last=igtree_setSelectedNode(treeID,last.id);
				//igtree_scrollToView(tree,last);
				processed=true;
			}
		}
	}
	if(evnt.keyCode == 36){ // home key
		if(igtree_currentNode){
			var first = igtree_firstNode(treeID);
			if(first){
				first=igtree_setSelectedNode(treeID,first.id);
				//igtree_scrollToView(tree,first);
				processed=true;
			}
		}
	}
	if(evnt.keyCode == 38)	{ // up arrow
		if(igtree_currentNode)	{
			var sibling = igtree_prevVisibleNode(treeID,igtree_currentNode);
			if(sibling)	{
				while(sibling && sibling.getAttribute("nodeDisabled"))
					sibling = igtree_prevVisibleNode(treeID,sibling);
				if(!sibling)
					return;	
				sibling=igtree_setSelectedNode(treeID,sibling.id);
				//igtree_scrollToView(tree,sibling);
				processed = true;

				
                if (ig_csom.IsIE)
				    document.documentElement.scrollTop = igtree_fnGetTopPos(sibling) - igtree_fnGetTopPos(tree);
			}
		}
	}
	if(evnt.keyCode == 40)
	{ // down arrow
		if(igtree_currentNode){
			var sibling = igtree_nextVisibleNode(treeID,igtree_currentNode);
			if(sibling)	{
				while(sibling && sibling.getAttribute("nodeDisabled"))
					sibling = igtree_nextVisibleNode(treeID,sibling);
				if(!sibling)
					return;	
				sibling=igtree_setSelectedNode(treeID,sibling.id);
				//igtree_scrollToView(tree,sibling);
				processed = true;

                
				if (ig_csom.IsIE)
				    document.documentElement.scrollTop = igtree_fnGetTopPos(sibling) - igtree_fnGetTopPos(tree);
			}
		}
		else
		{
			tree.hideFocus = true;
			var sibling = ts.getNodes()[0].Element;
			if(sibling)	{
				while(sibling && sibling.getAttribute("nodeDisabled"))
					sibling = igtree_nextVisibleNode(treeID,sibling);
				if(!sibling)
					return;	
				sibling=igtree_setSelectedNode(treeID,sibling.id);
				//igtree_scrollToView(tree,sibling);
				processed=true;				
			}
		}
	}
	if(processed)	{
		evnt.cancelBubble=true;
		evnt.returnValue=false;
		return false;
	}
}

// private - Handles key up events
function igtree_keyup(evnt, tn)
{
	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.KeyUp,"(\""+tn+"\","+evnt.keyCode+")"))
		return;
}

function getFirstChildDivofParent(parent)
{
	for(var i = 0; i < parent.childNodes.length; i++)
	{
		if(parent.childNodes[i].tagName == "DIV")
			return parent.childNodes[i];
	}
}

// private
function igtree_nextSibling(tn,node,all) {
	var ts=igtree_treeState[tn];
	var sibling;
	if(!node)	{
		var tree=ts.treeElement;
		var first=getFirstChildDivofParent(getFirstChildDivofParent(tree.childNodes[0]));
		while(first && first.tagName!="DIV")
			first=first.nextSibling;
		if(!first)
			return null;
		sibling=first;
		while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
			sibling=sibling.nextSibling;
		return sibling;
	}
	else
		sibling = node.nextSibling;
		
	while (sibling && (sibling.tagName != "DIV" || (sibling.style.display == "none" || sibling._expanded == false) || all && sibling.id.substr(0, 2) == "M_"))
		sibling=sibling.nextSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.nextSibling;
		while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
			sibling=sibling.nextSibling;
		parentNode=parentNode.parentNode;
	}
	if(sibling && sibling.id.substr(0,2)=="M_")
		sibling=sibling.childNodes[0];
	while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
		sibling=sibling.nextSibling;
	return sibling;
}

// private
function igtree_nextSiblingAll(tn,node,all) {
	var ts=igtree_treeState[tn];
	var sibling;
	if(!node)	{
		var tree=ts.treeElement;
		var first=tree.childNodes[0].childNodes[0].childNodes[0];
		while(first && first.tagName!="DIV")
			first=first.nextSibling;
		if(!first)
			return null;
		sibling=first;
		while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
			sibling=sibling.nextSibling;
		return sibling;
	}
	else
		sibling = node.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || all && sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.nextSibling;
		while(sibling && (sibling.tagName!="DIV" ))
			sibling=sibling.nextSibling;
		parentNode=parentNode.parentNode;
	}
	if(sibling && sibling.id.substr(0,2)=="M_")
		sibling=sibling.childNodes[0];
	while(sibling && (sibling.tagName!="DIV" ))
		sibling=sibling.nextSibling;
	return sibling;
}

function igtree_prevSibling(tn,node,all)
{
	var ts=igtree_treeState[tn];
	var sibling;
	if(!node)	{
		var tree=ts.treeElement;
		var last=tree.childNodes[0].childNodes[tree.childNodes[0].childNodes.length-1];
		while(last && last.tagName!="DIV")
			last=last.previousSibling;
		if(!last)
			return null;
		sibling=last;
		while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
			sibling=sibling.previousSibling;
		return sibling;
	}
	else
		sibling = node.previousSibling;
	
	while (sibling && (sibling.tagName != "DIV" || (sibling.style.display == "none" || sibling._expanded == false) || all && sibling.id.substr(0, 2) == "M_"))
		sibling=sibling.previousSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.previousSibling;
		while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
			sibling=sibling.previousSibling;
		parentNode=parentNode.parentNode;
	}
	while(sibling && sibling.id.substr(0,2)=="M_") {
		sibling=sibling.childNodes[sibling.childNodes.length-1];
		while(sibling && (sibling.tagName!="DIV" || sibling.style.display=="none"))
			sibling=sibling.previousSibling;
	}
	return sibling;
}

function igtree_fnGetLeftPos2(e) 
{
    var x = e.offsetLeft;
    tmpE = e.offsetParent;
    while (tmpE != null)
    {
		if(ig_csom.IsIE && tmpE.style.position == "absolute")
			break;
        x += tmpE.offsetLeft;
        if(tmpE.tagName=="DIV" && tmpE.style.borderLeftWidth)
	        x += parseInt(tmpE.style.borderLeftWidth);
        if(igtree_IE && tmpE.tagName!="BODY") {
			x-=tmpE.scrollLeft;
	    }
		if(!ig_csom.IsIE && tmpE.style.position == "absolute")
			break;
        tmpE = tmpE.offsetParent;
    }
    return x;
}
function igtree_fnGetLeftPos(e) 
{
    var x = 0;
    var parent = e;
    while (parent != null) {
		
        x += parent.offsetLeft;
        parent = parent.offsetParent;
    }
    return x;
}

// Returns top position of some element
function igtree_fnGetTopPos2(e) 
{
    var y = e.offsetTop;
    tmpE = e.offsetParent;
    while (tmpE != null) {
		if(ig_csom.IsIE && tmpE.style.position == "absolute")
			break;
        y += tmpE.offsetTop;
        if(tmpE.tagName=="DIV" && tmpE.style.borderTopWidth)
	        y += parseInt(tmpE.style.borderTopWidth);
        if(igtree_IE && tmpE.tagName!="BODY")
			y-=tmpE.scrollTop;
		if(!ig_csom.IsIE && tmpE.style.position == "absolute")
			break;
        tmpE = tmpE.offsetParent;
    }
    return y;
 }
 function igtree_fnGetTopPos(e) 
{

    var y = 0;
    var parent = e;
    while(parent != null) {
		
		y += parent.offsetTop;
		y -= parent.scrollTop;
        parent = parent.offsetParent;
	}
    return y;
}

function igtree_scrollToView(parent,child)
{
	parent.scrollTop = 0;
	if(parent.scrollWidth<=parent.offsetWidth && parent.scrollHeight<=parent.offsetHeight)
		return;
	var childLeft=igtree_fnGetLeftPos(child);
	var parentLeft=igtree_fnGetLeftPos(parent);
	var childTop=igtree_fnGetTopPos(child);
	var parentTop=igtree_fnGetTopPos(parent);

	parent.scrollTop = childTop - parentTop;
	parent.scrollLeft = childLeft - parentLeft;
}

function igtree_editKeyDown(evnt,tn)
{
	if(!igtree_editControl)
		return;
	var src=igtree_getElementById(igtree_editControl.getAttribute("currentNode"));
	var node=igtree_getNodeById(src.id);
	//src=igtree_getNodeSpan(src);
	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.EditKeyDown,"(\""+tn+"\",\""+node.getElement().id+"\","+evnt.keyCode+")")){
		evnt.cancelBubble = true;
		evnt.returnValue = false;
		return;
	}
	if(igtree_IE) {
		evnt.cancelBubble=true;
		if(evnt.keyCode==13)
		{
			event.returnValue=false;
			igtree_endedit(true);
			return false;
		}
		else if(evnt.keyCode==27)
			igtree_endedit(false);
	}
	else {
		if(evnt.keyCode==13){
			evnt.stopPropagation();
			igtree_endedit(true);
			return false;
		}
		else if(evnt.keyCode==27)
			igtree_endedit(false);
	}
}

function igtree_editKeyUp(evnt,tn)
{
	if(!igtree_editControl)
		return;
	var src=igtree_getElementById(igtree_editControl.getAttribute("currentNode"));
	var node=igtree_getNodeById(src.id);
	//src=igtree_getNodeSpan(src);
	if(igtree_fireEvent(tn,igtree_treeState[tn].Events.EditKeyUp,"(\""+tn+"\",\""+node.getElement().id+"\","+evnt.keyCode+")"))
		return;
	if(igtree_IE)
		evnt.cancelBubble=true;
}

// private - Initializes a Node object with properties and method references
function igtree_initNode(node)
{
	this.element=node;
	this.Element=node;
	this.Id = node.id;
	this.WebTree = igtree_getTreeByNodeId(this.Element.id);
	this.getTreeId=igtree_getTreeId;
	this.getElement=igtree_getElement;
	this.getText=igtree_getText;
	this.__getText=igtree_getText;
	this.setText=igtree_setText;
	this.setHtml=igtree_setHtml;
	this.getClass=igtree_getClass;
	this.setClass=igtree_setClass;
	this.getTag=igtree_getTag;
	this.setTag=igtree_setTag;
	this.getDataKey=igtree_getDataKey;
	this.getHiliteClass=igtree_getHiliteClass;
	this.setHiliteClass=igtree_setHiliteClass;
	this.getHoverClass=igtree_getHoverClass;
	this.setHoverClass=igtree_setHoverClass;
	this.getEnabled=igtree_getEnabled;
	this.setEnabled=igtree_setEnabled;
	this.getTargetFrame=igtree_getTargetFrame;
	this.setTargetFrame=igtree_setTargetFrame;
	this.getTargetUrl=igtree_getTargetUrl;
	this.setTargetUrl=igtree_setTargetUrl;
	this.hasChildren=igtree_hasChildren;
	
	this.getExpanded=igtree_getExpanded;
	this.setExpanded=igtree_setExpanded;
	this.getSelected=igtree_getSelected;
	this.setSelected=igtree_setSelected;
	this.getChecked=igtree_getChecked;
	this.setChecked=igtree_setChecked;
	this.hasCheckbox=igtree_hasCheckbox;
	
	this.getNextSibling=igtree_getNodeNextSibling;
	this.getPrevSibling=igtree_getNodePrevSibling;
	this.getFirstChild=igtree_getNodeFirstChild;
	this.getParent=igtree_getNodeParent;
	this.getChildNodes=igtree_getChildNodes;
	this.getLevel = function () {
		var nodeName=this.element.id.split("_")
		if(nodeName.length>1)
		{
			return nodeName.length - 2;			
		}
	}
	this.getIndex = function () {
		var index=0;
		var nodeName=this.element.id.split("_")
		if(nodeName.length>1)
		{
			index = parseInt(nodeName[nodeName.length-1]);
			return index-1;
		}
	}
	this.update = function (propName, propValue) {
		if(this.WebTree.suspendUpdates == true)
			return;
		if(propName == 'Remove') {
			var nodestate = ig_ClientState.addNode(this.WebTree.nodeState, this.Id);
			ig_ClientState.setPropertyValue(nodestate,propName,propValue);
		}
		else 
		{
			if(this.nodeState == null)
				this.nodeState = ig_ClientState.addNode(this.WebTree.nodeState, this.Id);
			if(propName == "Text")
				propValue = propValue.replace(/\+/g, "%2B");
			ig_ClientState.setPropertyValue(this.nodeState,propName,propValue);
		}
		if(this.WebTree.postField!=null)
			this.WebTree.postField.value = ig_ClientState.getText(this.WebTree.stateItems);	
	}
	this.insertChild=function(beforeIndex, text, className) {
		return this.WebTree._insert(this, beforeIndex, text, className);
	}
	this.addChild=function(text, className) {
		return this.WebTree._insert(this, -1, text, className);
	}
	
	this.getPopulated = function () {
	    subnodes = igtree_getElementById("M_" + this.element.id);
	    if(subnodes == null)
	        return false;
	    var a = subnodes.getAttribute("igPop");
	    if(a != null && a.length > 0)
	        if(a == "true")
		        return true;
		    else
		        return false;
	    else
		    return true;
	
	}
	this.setPopulated = function (bPopulated) {
	    subnodes = igtree_getElementById("M_" + this.element.id);
	    if(subnodes == null)
	        return;
	    subnodes.setAttribute("igPop", (bPopulated) ? "true" : "false" );
	}
	
	this.removeChild=function(index) {
	// V20061  
	// was this.getNodes() which is not implemented;
		var node = this.getChildNodes()[index];
		if(node) {
			node.remove();
		    node.WebTree = null;
		    ig_dispose(node);
		}
    // End V20061		
	}
	
	// V20061
	this.hasNodes = function() {
	   var subnodesId = "M_" + this.Element.id;
	   if(ig_csom.getElementById(subnodesId) != null)
	      return true;
	   else
	      return false;
	}
	
	// End V20061
	this.edit=function() {
		igtree_beginedit(this.WebTree.Id,this.Id)
	}
	
	this.scrollIntoView=function() {
		igtree_scrollToView(this.WebTree.Element,this.Element)
	}

	this.hover = function() {
		var className=igtree_getResolvedHoverClass(this.WebTree, this.Element);
		if(this.WebTree.FullNodeSelect == true) {
			if(this.WebTree.hoverNode && this.WebTree.hoverNode != this)
				this.WebTree.hoverNode.unHover();
				
			this.WebTree.hoverNode = this;
			
			if(className=="" || this.Element.className == className)
				return;
			if(this.hoverSet)
				return;
			//if(ig_isMouseOverSourceAChild(evnt,eNode))
			//	return;
			this.Element.HovClass = this.Element.className;
			this.hoverSet = true;
			if(this.WebTree.MergeStyles)
				this.Element.className += " " + className;
			else
				this.Element.className = className;
		}
		else {
		
			if(igtree_IE && tree.Element.clientWidth>0 && eNode.offsetWidth>tree.Element.clientWidth) 
				if(ig_csom.isEmpty(this.Element.title)) {
					src.title=src.innerText;
					this.Element.igtitle = true;
				}
			var igtxt = src.getAttribute("igtxt");	
			if(igtxt!=null && igtxt.length>0) {
				src.HovClass = src.className;
				src.hoverSet = true;
				src.className = className;
			}
		}	
	}

	this.unHover=function() {
		if(this.WebTree.FullNodeSelect == true) {
			if(this.hoverSet) {
				//if(ig_isMouseOutSourceAChild(evnt, eNode))
				//	return;
				prevClass =	this.Element.HovClass;
				if(prevClass == null)
					prevClass = "";
				this.Element.className = prevClass;
				this.hoverSet = null;
				this.WebTree.hoverNode = null;
			}
		}
		else {
			var igtxt = src.getAttribute("igtxt");	
			if(igtxt==null || igtxt.length==0) {
				return;
			}
			if(src.style != null) {
				if(src.hoverSet) {
					prevClass =	src.HovClass;
					if(prevClass == null)
						prevClass = "";
					src.className = prevClass;
					src.hoverSet = null;
				}
			}
		}
	}
		
	this.remove=function() {
		var nodeArrayDeletionQueue=new Array();
		var index = this.getIndex();
		var nodeId = this.Element.id;
		var removeTop = false;
		var nextTop = null;
		var prevTop = null;
		var tree = this.WebTree;
		if(tree._removeProcess == null) {
			removeTop = true;
			tree._removeProcess = true;
			nextTop = this.getNextSibling();
			prevTop = this.getPrevSibling();
			this.update("Remove", "1");
		}
			
		var node = this.getFirstChild();
		while(node) {
			var next = node.getNextSibling();	
			node.remove();
			node.WebTree=null;
			nodeArrayDeletionQueue.push(node);
			node = next;
		}
		ig_dispose(nodeArrayDeletionQueue);
		var next = this.Element.nextSibling;
		
		var nParent = this.Element.parentNode;
		if (nParent) nParent.removeChild(this.Element);

		if(next && next.id.indexOf("M_")==0)
		{
			nParent = next.parentNode;
			if (nParent) nParent.removeChild(next);
		}
		var parentNode = this.getParent();
		if(removeTop == true) {
			tree._removeProcess = null;
			if(tree.ClassicTree) {
				if(nextTop)
					igtree_updateNodeLines(tree, nextTop, null, false);
				if(prevTop)
					igtree_updateNodeLines(tree, prevTop, null, false);
			}
			if(!nextTop && !prevTop) {
				if(parentNode) {
					next = parentNode.Element.nextSibling;
					if(next && next.id.indexOf("M_")==0)
					{
						nParent = next.parentNode;
						if (nParent) nParent.removeChild(next);
					}
					if(tree.ClassicTree) 
						igtree_updateNodeLines(tree, parentNode, null, false);
				}
			}
			
		}
		var nodeElements;
		if(parentNode == null) {
			nodeElements = igtree_getChildNodeElements(tree, null);
		}
		else {
			nodeElements = igtree_getChildNodeElements(tree, parentNode.Element);
			if(nodeElements.length == 0) {
				if(!tree.ClassicTree && tree.Expandable) {
					if(parentNode.Element.childNodes[1].getAttribute("imgType") == "exp") {
						parentNode.Element.removeChild(parentNode.Element.childNodes[1]);
						parentNode.Element.removeChild(parentNode.Element.childNodes[1]);
						if(!document.all) {
						    

						    if(parentNode.Element.childNodes[0].tagName != "SPAN")
						        parentNode.Element.childNodes[0].style.cssText = "width:0px;margin-left:0px;";
						}
						else {
						    parentNode.Element.childNodes[0].style.height = "0px";
						    parentNode.Element.childNodes[0].style.width = "16px";
						}
					}
				}
			}
		}
		var parentId = this.Element.id;
		var ptr = parentId.lastIndexOf("_");
		parentId = parentId.substring(0, ptr);
		
		igtree_setChildIds(tree, nodeElements, index, parentId);
		var selNode = tree.getSelectedNode();
		if(selNode && (nodeId == selNode.Id))
			tree.setSelectedNode(null);
	}
	this.isChildOf = function (parent) {
		if(parent == null || typeof(parent) != "object")
			return false;
		if(this.Id.search(parent.Id) == 0) 
		   return true; 
		return false;
	}
	// V20061
	if(this.WebTree && this.WebTree._version && (this.WebTree._version >= 20061)) {
	    this.getDataPath = function() {
	        return this.element.getAttribute("igPath");
	    }
	}
	// End V20061
}

function igtree_getElement() {
	return this.element;
}
function igtree_getTreeId() {
	var treeName = this.element.id;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	return treeName;
}



function igtree_getText(innerHTML) {
    

        
    var e = ig_getNodeTextElement(this);
    if (e==null && igtree_editControl && !igtree_editControl.endEdit)
    {
        if(igtree_editControl.value)
            return igtree_editControl.value;
        else
            return "";           
    }
    
    else{

        
        
        
        e = e.parentNode;
	    if(e==null)return "";
	    var i,v=null,ii=(e.childNodes==null)?0:e.childNodes.length;
    	
	    for(i=-1;i<ii;i++)
	    {
		    var ei=(i<0)?e:e.childNodes[i];
		    if(ei.nodeName=="#text")v=(v==null)?ei.nodeValue:v+" "+ei.nodeValue;
		}

	    if(v!=null)return v;
	   // if ((v = e.text) != null) return v;

	    
	    if(ig_csom.IsIE)
	        if(innerHTML)
	            try{return ei.innerHTML;}catch(ex){}
	        else
	            try{return ei.innerText;}catch(ex){}
	    else
	        
	        try{return ei.textContent;}catch(ex){}
    	
	    return "";
    }
}

function igtree_setText(text, innerHTML) {
	var e = ig_getNodeTextElement(this);
	this.update("Text", text);
	if(e.innerText != null)
		try{return e.innerText = text;}catch(ex){}
	else
		try{return e.innerHTML = text;}catch(ex){}
}
function igtree_setHtml(html) {
	var e = ig_getNodeTextElement(this);
	this.update("Text", html);
	try{return e.innerHTML = html;}catch(ex){}
}
function igtree_getClass() {
	var e = ig_getNodeTextElement(this);
	return e.className;
}
function igtree_setClass(className) {
	var e = ig_getNodeTextElement(this);
	var oldClass = e.className;
	if(this.WebTree.getSelectedNode() == this)
		this.element.setAttribute("igtInitClass",className);
	else
		e.className=className;
}
function igtree_getTag() {
	var a = this.element.getAttribute("igTag");
	if(a!=null && a.length>0)
		return a;
	else
		return null;
}
function igtree_setTag(text) {
	this.element.setAttribute("igTag", text);
	this.update("Tag", text);
}
function igtree_getDataKey() {
	var a = this.element.getAttribute("igDataKey");
	if(a!=null && a.length>0)
		return a;
	else
		return null;
}
function igtree_getHiliteClass() {
	return this.element.getAttribute("HiliteClass")
}
function igtree_setHiliteClass(hiliteClass) {
	this.element.setAttribute("HiliteClass", hiliteClass)
}
function igtree_getHoverClass() {
	return this.element.getAttribute("HoverClass")
}
function igtree_setHoverClass(hoverClass) {
	this.element.setAttribute("HoverClass", hoverClass)
}
function igtree_getEnabled() {
	return(this.element.getAttribute("nodeDisabled")?false:true);
}
function igtree_setEnabled(enabled) {
	var i;
	var nodeSpan = null;
	var checkbox = null;
	for(i=0; i < this.element.childNodes.length; i++) {
		var attrib = this.element.childNodes[i].getAttribute("igTxt");
		var chk = this.element.childNodes[i].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			checkbox = this.element.childNodes[i];
		if(attrib=="1")
			nodeSpan = this.element.childNodes[i];
	}
	if(nodeSpan != null) {
		if(enabled == true) {
			this.element.removeAttribute("nodeDisabled");
			this.element.className = "";
			//var oldClass = nodeSpan.getAttribute("oldEnabledClass");
			if(oldClass != null && oldClass.length > 0) {
				nodeSpan.className="  ";
				nodeSpan.removeAttribute("oldEnabledClass");
			}
			


 			else if(oldClass == null)
			{
			    var ts = igtree_getTreeById(this.getTreeId());
			    var nodeClass = null;
			    
			    var levelIndex = this.getLevel();
			    
		    	if(levelIndex == 0 && ig_csom.notEmpty(ts.RootNodeClass))
	                nodeClass = ts.RootNodeClass;
	            
	            if(ig_csom.notEmpty(ts.LeafNodeClass))
		            nodeClass = nodeClass + " " + ts.LeafNodeClass;
		            
	            if(ig_csom.notEmpty(ts.NodeClass))
		            nodeClass = nodeClass + " " + ts.NodeClass;
		            
		        nodeSpan.className = "  ";
			    
				nodeSpan.removeAttribute("oldEnabledClass");
				
				this.element.className = nodeClass;
			}
			if(checkbox!=null)
				checkbox.disabled = false;
		}
		else {
			var ts = igtree_getTreeById(this.getTreeId());
			var disabledClass = ts.DisabledClass;
			this.element.setAttribute("nodeDisabled", "1");
			var oldClass = nodeSpan.className;
			nodeSpan.className=disabledClass;
			nodeSpan.setAttribute("oldEnabledClass", oldClass);
			this.element.removeAttribute("igtInitClass");
			if(checkbox!=null)
				checkbox.disabled = true;
		}
		this.update("Enabled", enabled);
	}
}
function igtree_getTargetFrame() {
	var frame = this.element.getAttribute("igFrame");
	if(ig_csom.notEmpty(frame))
		return frame;
	else
	if(ig_csom.notEmpty(this.WebTree.TargetFrame)) {
		return this.WebTree.TargetFrame;
	}
	else
		return null;
}
function igtree_setTargetFrame(frame) {
	this.element.setAttribute("igFrame", frame)
	this.update("TargetFrame", frame);
}
function igtree_getTargetUrl() {
	var url = this.element.getAttribute("igUrl");
	if(ig_csom.notEmpty(url))
		return url;
	else
	if(ig_csom.notEmpty(this.WebTree.TargetUrl)) {
		return this.WebTree.TargetUrl;
	}
	else
		return null;
}
function igtree_setTargetUrl(url) {
	this.element.setAttribute("igUrl", url)
	this.update("TargetUrl", url);
}
function igtree_setChecked(bChecked, bFireCSOMEvent) {
	var node = this.element;
	var ts = igtree_getTreeByNodeId(node.id);	
	var index=1;
	var count=node.childNodes.length;
	for(index=1;index<count;index++) {
		var chk = node.childNodes[index].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			break;
	}
	if(index >= count)
		return;
	eCheck = node.childNodes[index];
	if(bChecked==false) {
		eCheck.checked=false;
	}
	else {
		eCheck.checked=true;
	}
	if(bFireCSOMEvent!=false) {
		var tn = this.getTreeId();
		var nodeId = this.Id;
		if(igtree_fireEvent(tn,ts.Events.NodeChecked,"(\""+tn+"\",\""+nodeId+"\", eCheck.checked)")) {
			eCheck.checked = oldValue;
			return;
		}
	}	
	igtree_updateNodeCheck(ts, this.element.id, bChecked);
}

function igtree_getChecked() {
	var node = this.element;
	var index=1;
	var count=node.childNodes.length;
	for(index=1;index<count;index++) {
		var chk = node.childNodes[index].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			break;
	}
	return this.element.childNodes[index].checked;
}
function igtree_hasCheckbox() {
	var index;
	for(index=1;index<this.element.childNodes.length;index++) {
		var chk = this.element.childNodes[index].getAttribute("igChk");
		if(chk!=null && chk.length>0)
			return true;
	}
	return false;
}

function igtree_hasChildren() {
	var childNodesParentElement = ig_csom.getElementById("M_"+this.element.id);
	if( (childNodesParentElement) && (childNodesParentElement.tagName == "DIV") )
		return true;
	else
		return false;
}

function igtree_hasChildrenElements(nodeElement) {
	if(nodeElement.nextSibling && nodeElement.nextSibling.id == "M_"+nodeElement.id)
		return true;
	else
		return false;
}

function igtree_getExpanded() {
	var expEl=igtree_getElementById("M_"+this.element.id);
	if(expEl != null)
		return(expEl.style.display!="none");
	else
		return false;
}
function igtree_setExpanded(expand) {
	if(expand == true)
		igtree_expandNode(this);
	else
		igtree_collapseNode(this);
}
function igtree_getSelected() {
	var treeName = this.element.id;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	return (this.Element==igtree_selectedNode(treeName));
}
function igtree_setSelected(bSelect) {
	var treeName = this.element.id;
	var strArray = treeName.split("_");
	treeName = strArray[0];
	if(bSelect) {
		igtree_setSelectedNode(treeName, this.element.id)
	}
	else {
		if(this.Element==igtree_selectedNode(treeName))
			igtree_setSelectedNode(treeName, null)
	}
}

// private - Initializes an event
function igtree_fireEvent(tn,eventObj,eventString)
{
	var ts=igtree_treeState[tn];
	var result=false;
	if(eventObj[0]!="")
		result=eval(eventObj[0]+eventString);
	if(ts.TreeLoaded && result!=true && eventObj[1]==1 && !ts.CancelPostBack)
		igtree_needPostBack(tn);
	ts.CancelPostBack=false;
	return result;
}

// private
var igtree_lastActiveTree="";
if(!igtree_IE)
	if(window.addEventListener)
		window.addEventListener('keydown',igtree_windowKeyDown,false);

// private
function igtree_windowKeyDown(evnt)
{
	
	if(igtree_lastActiveTree!="" && evnt.keyCode!=13)
		if(igtree_keydown(evnt,igtree_lastActiveTree, true)==true)
		{
			evnt.stopPropagation();
			evnt.preventDefault();
		}
}

// private
function igtree_initEvent(se)
{
	this.target=se;
}

// public - Returns the selected Node for the Tree 
function igtree_selectedNode(tn) 
{
	return igtree_treeState[tn].selectedNodeElement;
}

// private - Implements the setSelectedNode method for the tree
function igtreem_setSelectedNode(node)
{
	var uniqueId = this.getClientUniqueId();
	var id=null;
	if(node!=null)
		id=node.Id;
	igtree_setSelectedNode(uniqueId, id);
}

// private - Implements the getSelectedNode method for the tree
function igtreem_getSelectedNode()
{
	var uniqueId = this.getClientUniqueId();
	var node=igtree_selectedNode(uniqueId);
	if(node)
		return igtree_getNodeById(node.id);
	return null;
}

// private
function igtree_getClientUniqueId() {
    var u = this.UniqueId.replace(/:/gi, "");
 
    while(u.indexOf("$") != -1)
        u = u.replace("$",""); // CLR 2.0
	
	u = u.replace(/_/gi, "");
    u = u.replace(/\-/gi,"");
    


    u = u.replace(/\./gi, "");
	
	if(u.indexOf("{") != -1) {
		u = "IG" + u;
		u = u.replace(/\{/gi, "");
		u = u.replace(/\}/gi, "");
	}
	return u;
}

// private

// Inserts a node to the tree
function igtree_insertChild(parentNode, index, text, className) {
	// obtain the Nodes collection for the operation
	var nodes;
	var nodeElements;
	var parentId;
	var insert = false;
	if(index != -1)
		insert = true;
	
	if(parentNode == null) {
		nodes = this.getNodes(false);
		if(this.nodes)
			this.nodes = null;
		parentId = this.Id;
	}
	else {
		nodes = parentNode.getChildNodes();
		if(parentNode.nodes)
			parentNode.nodes = null;
		parentId = parentNode.Id;
		if(nodes == null || nodes.length == 0 && 
		        document.getElementById("M_" + parentId) == null) { // create childNodes div
			var mdiv = window.document.createElement("DIV");
			mdiv.id = "M_"+parentId;
			var next = parentNode.Element.nextSibling;
			if(next != null)
				parentNode.Element.parentNode.insertBefore(mdiv, next);
			else		
				parentNode.Element.parentNode.appendChild(mdiv);		
			
			var span = parentNode.Element.firstChild;
			if(ig_csom.IsIE) {
			    span.style.width = '0px';
			    span.style.padding.left = '0px';
			}
			else
			    span.style.cssText = "width:0px; padding-left:0px;" + " margin-left:" + span.style.marginLeft + ";";
			if(!this.ClassicTree && this.Expandable) {
				var ig = window.document.createElement("IMG");
				
				ig.setAttribute("imgType", "exp");
				ig.src = this.CollapseImage;
				parentNode.Element.insertBefore(ig, parentNode.Element.childNodes[1]);
				ig = window.document.createElement("SPAN");
				ig.style.width="5px";
				ig.innerHTML="&nbsp;";
				parentNode.Element.insertBefore(ig, parentNode.Element.childNodes[2]);
			}
			igtree_updateNodeToggle(this, parentId, true);
			if(ig_csom.notEmpty(this.ParentNodeClass))
				if(parentNode.getParent() != null || ig_csom.isEmpty(this.RootNodeClass))
					parentNode.setClass(this.ParentNodeClass);
		}
	}
	if(index == null || index == -1 || index > nodes.length)
		index = nodes.length;
		
	// create the node DOM structure
	var div = window.document.createElement("DIV");

	var mrgn = 0;
	var parentLevel = null;
	var levelIndex;
	if(parentNode) {
		levelIndex = parentNode.getLevel();
		parentLevel = this.Levels.getItem(levelIndex);
		levelIndex++;
		mrgn = levelIndex * this.Indentation;
	}
	else {
		levelIndex = 0;
	}
	var level = this.Levels.getItem(levelIndex);
		
	var html = "";
	
	if(this.NodeMargins != null && !this.ClassicTree) {
	    if(this.NodeMargins[0].length > 0)
	        div.style.marginTop = this.NodeMargins[0];
	    if(this.NodeMargins[1].length > 0)
	        div.stylemarginLeft = this.NodeMargins[1];
	    if(this.NodeMargins[2].length > 0)
	        div.style.marginBottom = this.NodeMargins[2];
	    if(this.NodeMargins[3].length > 0)
	        div.style.marginRight = this.NodeMargins[3];
	    
	    if(this.NodePaddings[0].length > 0)
	        div.style.paddingTop = this.NodePaddings[0];
	    if(this.NodePaddings[1].length > 0)
	        div.style.paddingLeft = this.NodePaddings[1];
	    if(this.NodePaddings[2].length > 0)
	        div.style.paddingBottom = this.NodePaddings[2];
	    if(this.NodePaddings[3].length > 0)
	        div.style.paddingRight = this.NodePaddings[3];
	}
	
	if(this.ClassicTree) {
		marginHtml = "<span igl='1'></span>";
	}
	else {
		var marginHtml;
		
		//A.T. 18 March 2010 - Fix for bug #29351 - The indentation does not work in IE-8 when adding Nodes with javascript
		if(ig_csom.IsIE)
		    marginHtml = "<img style='width:16px;height:0;margin-left:%MARGIN%px;'></img>"; 
		else
		    marginHtml = "<span style='padding-left:16px;margin-left:%MARGIN%px;'></span>"; 
		marginHtml = marginHtml.replace("%MARGIN%", mrgn);
	}
	html += marginHtml;
	
	var bCheck = false;
	var checkboxHtml = "<input class='igt_align' type='checkbox' igchk='1'>";
	if(level && level.LevelCheckBoxes==2)
		bCheck = true;
	else
	if(level && level.LevelCheckBoxes==1)
		bCheck = false;
	else
		bCheck = this.CheckBoxes;
	if(bCheck)
		html += checkboxHtml;
	
	//A.T. 6th Feb. 2010 - Fix for bug #26504 - Node size changes and connector lines break with XHTML DocType 1.0 Transitional in IE8.
	var img = "<img igimg='1' style='margin-right:4px;vertical-align:middle;' src='%IMAGE%'>";
	var imgurl = "";
	if(level && ig_csom.notEmpty(level.LevelImage))
		imgurl = level.LevelImage;
	else
	if(parentNode == null && ig_csom.notEmpty(this.RootNodeImageUrl)) { // Root Node
		imgurl = this.RootNodeImageUrl;
	}
	else
	if(ig_csom.notEmpty(this.LeafNodeImageUrl)) {
		imgurl = this.LeafNodeImageUrl;
	}
	else
	if(ig_csom.notEmpty(this.DefaultImage)) {
		imgurl = this.DefaultImage;
	}
	if(imgurl.length > 0) {
	// V20061
	    var startChar = imgurl.substr(0, 1);
	    if(startChar != "." && startChar != "/" && startChar != "\\" && imgurl.substr(0,4) != "http")
	    {
	        imgurl = this.ImageDirectory + imgurl;
	    }
	// End V20061
		img = img.replace("%IMAGE%", imgurl);
		html += img;
	}
	
	var cls = null;
	if(className != null)
		cls = className;
	else
	if(level && level.ClassName != null)
		cls = level.ClassName;
	else
	if(levelIndex == 0 && ig_csom.notEmpty(this.RootNodeClass))
		cls = this.RootNodeClass;
	else
	if(ig_csom.notEmpty(this.LeafNodeClass))
		cls = this.LeafNodeClass;
	else
	if(ig_csom.notEmpty(this.NodeClass))
		cls = this.NodeClass;
		
	var txt = "<span %CLASS%igtxt='1'>%TEXT%</span>";
	if(ig_csom.notEmpty(cls))
		txt = txt.replace("%CLASS%", "class='"+cls+"' ");
	else
		txt = txt.replace("%CLASS%", "");
	
	txt = txt.replace("%TEXT%", text);
	html += txt;
	
	div.innerHTML = html;
	var parentElem = ig_csom.getElementById("M_"+parentId);
	if(parentElem == null) return;
	
	




	
	if(insert && nodes.length <= index)  // convert to an Add operation
		insert = false;
		
	// construct the nodeId
	div.id = parentId + '_' + (index+1);
	var nodeId = div.id;
	// insert the node into the tree DOM
	if(insert) { // Perform insert
		if(parentNode == null) {
			nodeElements = igtree_getChildNodeElements(this, null);
		}
		else {
			nodeElements = igtree_getChildNodeElements(this, parentNode.Element);
		}
		var beforeElem = nodeElements[index];


		div.id = this.Id + "_ig_temp";
		parentElem.insertBefore(div, beforeElem);		
		if(parentNode == null) {
			nodeElements = igtree_getChildNodeElements(this, null);
		}
		else {
			nodeElements = igtree_getChildNodeElements(this, parentNode.Element);
		}
		div.id = nodeId;
		
		var parId = div.id;
		var ptr = parId.lastIndexOf("_");
		parId = parId.substring(0, ptr);
		igtree_setChildIds(this, nodeElements, index+1, parId);
	}
	else  
		parentElem.appendChild(div);	
			
	var node = igtree_getNodeById(nodeId);
	if(this.ClassicTree) {
		if(parentNode && !parentNode._loadingNodes)
			igtree_updateNodeLines(this, parentNode, null, true);
		igtree_updateNodeLines(this, node, levelIndex, true);
	}
	if(node.WebTree == null)
		node.WebTree = this;
		
	if(!parentNode || !parentNode._loadingNodes) {
	    if(insert) 
		    node.update("Add", index.toString());
	    else
		    node.update("Add", "-1");
	    if(text)
		    node.update("Text", text);
	    if(className)
		    node.update("CssClass", className);
    }
	return node;
}

function igtree_setChildIds(tree, nodeElements, index, parentId) {
	var len = nodeElements.length;
	if(index==null || index>=len)
		index=0;	
	var i;
	for(i=index;i<len;i++) {
		var nodeElement = nodeElements[i];
		var id = parentId;
		x = i+1;
		id += "_" + x.toString();
		var childNodeElements;
		var bSetChildren = igtree_hasChildrenElements(nodeElement);
		if(bSetChildren)
			childNodeElements = igtree_getChildNodeElements(tree, nodeElement);
			
		nodeElement.id = id;
		if(nodeElement.oNode != null)
			nodeElement.oNode.Id = id;
		if(bSetChildren) {
			igtree_setChildIds(tree, childNodeElements, 0, id);
		}
		var nextSibling = nodeElement.nextSibling;
		if(nextSibling != null && nextSibling.id.substr(0,2)=="M_") {
			nextSibling.id = "M_" + id;
		}
	}
}

function igtree_getChildNodeElements(tree, parentNodeElement) {
	elements=new Array();
	var nodeCount=0;
	var childNodeElement=igtree_getFirstChildNodeElement(tree, parentNodeElement);
	while(childNodeElement)	{
		elements[nodeCount++]=childNodeElement;
		childNodeElement=igtree_getNextChildNodeElement(tree, childNodeElement);
	}
	return elements;
}

function igtree_getFirstChildNodeElement(tree,parentNodeElement) {
	var sibling;
	if(!parentNodeElement)	{
		sibling=tree.treeElement.childNodes[0].childNodes[0].childNodes[0];
		return sibling;
	}
	sibling = parentNodeElement.nextSibling;
	if(!sibling)
		return null;
	if(sibling.id.substr(0,2)!="M_")
		return null;
	var firstChild = sibling.firstChild;	
	return firstChild;
}

function igtree_getNextChildNodeElement(tree,nodeElement) {
	if(!nodeElement)
		return;
	var sibling = nodeElement.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	return sibling;
}

function igtree_updateNodeLines(tree, node, levelIndex, append) {
	var parent = node.getParent();
	if(levelIndex == null)
		levelIndex = node.getLevel();
	if(parent)
		igtree_updateNodeDescendantLines(tree, parent)
	igtree_updateLines(tree, node, levelIndex);
	igtree_updateNodeDescendantLines(tree, node)
	var prev = node.getPrevSibling();
	if(prev) {
		igtree_updateLines(tree, prev, levelIndex);
		igtree_updateNodeDescendantLines(tree, prev)
	}
	var next = node.getNextSibling();
	if(next) {
		igtree_updateLines(tree, next, levelIndex);
		igtree_updateNodeDescendantLines(tree, next)
	}
}

function igtree_updateNodeDescendantLines(tree, parentNode) {
	var node = parentNode.getFirstChild();
	while(node) {
		igtree_updateLines(tree, node, node.getLevel());
		if(node.getFirstChild())
			igtree_updateNodeDescendantLines(tree, node) 
		node = node.getNextSibling();	
	}
}

// private
function igtree_updateLines(tree, node, levelIndex) {
	var eLines = igtree_getLinesElement(node);
    var html = igtree_WriteLines(tree, node, levelIndex);
	eLines.innerHTML = html;
}

// private
function igtree_getLinesElement(node) {
	var e = node.Element;
	var eLine = e.firstChild;
	if(eLine.attributes["igl"].nodeValue == '1')
		return eLine;
}

// private
function igtree_getShowExpand(node) {
	    subnodes = igtree_getElementById("M_" + node.Element.id);
	    if(subnodes == null)
	        return true;
	    return node.getPopulated();
}

// private
function igtree_WriteLines(tree, node, level) {
	// write the level line images
	var s;
	if((node.getFirstChild() && tree.Expandable && tree.ExpandImagesVisible) || (tree.LoadOnDemand >= 1 && !igtree_getShowExpand(node))) {
		s = igtree_WriteLineLevelImage(tree, node, level);
		if(node.getExpanded())
			s += igtree_WriteCollapseImage(tree, node) 
		else
			s += igtree_WriteExpandImage(tree, node) 
	}
	else {
		s = igtree_WriteLineLevelImage(tree, node, level);
		s += igtree_WriteJoinerImage(tree, node);
	}
	return s;
}

// private
function igtree_WriteLineLevelImage(tree, node, level) {
	var list = new Array();

	var parent = node.getParent();
	var s = "";
	var i = 0;
	while(parent != null) {
		list[i++] = parent;
		parent = parent.getParent();
	}
	for(j = list.length-1; j >= 0; j--) {
		parent = list[j];
		//A.T. 6th Feb. 2010 - Fix for bug #26504 - Node size changes and connector lines break with XHTML DocType 1.0 Transitional in IE8.
		s += "<img style='vertical-align:middle;' ";
		s += "src='";
		if(parent.getNextSibling() != null && !parent.getNextSibling().Hidden)
			s += tree._i;
		else
			s += tree._w;
		s += "'>";
	}
	return s;
}

function igtree_WriteCollapseImage(tree, node) {
	var s = "<img style='vertical-align:middle;' src='";
	if(node.getParent() == null) {
		if((node.getPrevSibling() == null) &&
			(node.getNextSibling() == null)) {
			s += tree._ominus;
		}
		else
			if(node.getPrevSibling() == null) {
			s += tree._fminus;
		}
		else
			if(node.getNextSibling() == null) 
			s += tree._lminus;
		else
			s += tree._mminus;
	}
	else
	if(node.getPrevSibling() == null) {
		if(node.getNextSibling() == null) 
			s += tree._lminus;
		else
			s += tree._mminus;
	}
	else
	if(node.getNextSibling() == null) 
		s += tree._lminus;
	else
		s += tree._mminus;
	s += "' imgType='exp'>";
	return s;
}

function igtree_WriteExpandImage(tree, node) {
	var s = "<img style='vertical-align:middle;' src='";
	if(node.getParent() == null) {
		if((node.getPrevSibling() == null || node.getPrevSibling().Hidden) &&
			(node.getNextSibling() == null || node.getNextSibling().Hidden) ) {
			s += tree._oplus;
		}
		else
			if(node.getPrevSibling() == null || node.getPrevSibling().Hidden) {
			if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
				s += tree._lplus;
			else
				s += tree._fplus;
		}
		else
			if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
			s += tree._lplus;
		else
			s += tree._mplus;
	}
	else
		if(node.getPrevSibling() == null || node.getPrevSibling().Hidden) {
		if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
			s += tree._lplus;
		else
			s += tree._mplus;
	}
	else
		if(node.getNextSibling() == null || node.getNextSibling().Hidden) 
		s += tree._lplus;
	else
		s += tree._mplus;
	s += "' imgType='exp'>";
	return s;
}

function igtree_WriteJoinerImage(tree, node) {
	var s = "<img style=\"vertical-align:middle;\" src='";
	// The image to render can be either for the first node, a middle node, or the last node of a collection
	
	if (null==node.getParent()&&null==node.getPrevSibling()&&null==node.getNextSibling()&&null==node.getChildNodes())
		s += tree._s;
	else
	if(node.getParent() == null && node.getPrevSibling() == null && node.getNextSibling() == null)
		s += tree._w;
	else
	if(node.getParent() == null && node.getPrevSibling() == null)
		s += tree._f;
	else
		if(node.getNextSibling() == null)
		s += tree._l;
	else
		if(node.getPrevSibling() == null)
		s += tree._t;
	else
		s += tree._t;
	s += "'>";
	return s;
}

// private
function igtree_resolveImage(tree, image) {
    if(tree.treeElement.clr2 != null)
        return "Infragistics.WebUI.UltraWebNavigator.IMG." + image;
    else
	    return tree.ImageDirectory + image;
}

// private
function igtree_getTreeNodes(all){
	if(all == undefined)
		all = false;
	var nodes=new Array();
	var nodeCount=0;
	var uniqueId = this.getClientUniqueId();	
	var node=igtree_nextSibling(uniqueId,null, !all);
	while(node)	{
		nodes[nodeCount++]=igtree_getNodeById(node.id);
		node=igtree_nextSibling(uniqueId,node, !all);
	}
	return nodes;
}

function igtree_nextNode(tn,node,all) {
	var ts=igtree_treeState[tn];
	var sibling;
	sibling = node.nextSibling;
	while(sibling && (sibling.tagName!="DIV" || all && sibling.id.substr(0,2)=="M_"))
		sibling=sibling.nextSibling;
	if(!sibling && all)
		return null;
	var parentNode = node.parentNode;
	while(!sibling && parentNode && parentNode.id.substr(0,2)=="M_"){
		sibling=parentNode.nextSibling;
		while(sibling && (sibling.tagName!="DIV" ))
			sibling=sibling.nextSibling;
		parentNode=parentNode.parentNode;
	}
	if(sibling && sibling.id.substr(0,2)=="M_")
		sibling=sibling.childNodes[0];
	while(sibling && (sibling.tagName!="DIV" ))
		sibling=sibling.nextSibling;
	return sibling;
}

// private - Implements getNextSibling for the Node object
function igtree_getNodeNextSibling(){
	var node=igtree_nextSibling(this.getTreeId(),this.element,true);
	if(node)
		node=igtree_getNodeById(node.id);
	return node;
}

// private - Implements GetPrevSibling for the Node object
function igtree_getNodePrevSibling(){
	var node=igtree_prevSibling(this.getTreeId(),this.element,true);
	if(node)
		node=igtree_getNodeById(node.id);
	return node;
}

// private - Implements FirstChild for the Node object
function igtree_getNodeFirstChild(){
	var node=null;
	if(this.hasChildren()) {
		var expEl = null;
				
		var childNodesParentElement = ig_csom.getElementById("M_"+this.element.id);
		if( (childNodesParentElement) && (childNodesParentElement.tagName == "DIV") )
			expEl = childNodesParentElement;
				
		if(expEl) 
		{
			var child = getFirstChildDivofParent(expEl);
			if(child) {
				var id = child.id;
				node=igtree_getNodeById(id);
			}
		}
	}
	return node;
}

// private - Implements the Parent property for the Node object
function igtree_getNodeParent(){
	var node=null;
	var nodeName=this.element.id.split("_")
	if(nodeName.length>2)
	{
		var parentName=this.element.id.substr(0,this.element.id.length-nodeName[nodeName.length-1].length-1);
		node=igtree_getNodeById(parentName);
	}
	return node;
}

// private - Implements Nodes collection property for the Node object
function igtree_getChildNodes(){
	var nodes=new Array();
	var nodeCount=0;
	var node=this.getFirstChild();
	while(node)	{
		nodes[nodeCount++]=node;
		node=node.getNextSibling();
	}
	return nodes;
}

// private
function igtree_firstNode(tn){
	return igtree_nextSibling(tn,null);
}

// private
function igtree_lastNode(tn) {
	return igtree_prevSibling(tn,null);
}

// private
function igtree_nextNode(tn,node){
	return igtree_nextSibling(tn,node,true);
}

// private
function igtree_prevNode(tn,node) {
	return igtree_prevSibling(tn,node,true);
}

// private
function igtree_nextVisibleNode(tn,node) {
	return igtree_nextSibling(tn,node);
}

// private
function igtree_prevVisibleNode(tn,node) {
	return igtree_prevSibling(tn,node);
}
//private
function igtree_clearNodes() {
	var nodes = this.getNodes();
	var len = nodes.length;
	var i;
	for(i=0;i<len;i++) {
		var node=nodes[i];
		node.remove();
		node.WebTree=null;
		ig_dispose(node);
	}
	ig_dispose(nodes);
	return this.nodes = null;
}
// private
var ig_currDropNode;
function igtree_dragstart(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	var node = tree.getNodeById(eNode.id);
	if(!tree.AllowDrag || !node.getEnabled()) {
		ig_cancelEvent(evnt);
		return false;
	}
	ig_dataTransfer = new ig_DataTransferObject(evnt.dataTransfer, treeId, node);
	ig_csom.dataTransfer = ig_dataTransfer;
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragStart,node,ig_dataTransfer,evnt);
}

var ig_treeSelectedTextRange = null;

function igtree_preselect(event) {

    if (!event) var event = window.event; 

	if(typeof(igmenu_mouseDown) == "function")
		igmenu_mouseDown(event);

    var e;

   // e = event.srcElement;
    e = event.srcElement ? event.srcElement : event.target;
    var igtxt = e.getAttribute("igtxt");	
	if(!igtxt) return;
	var eNode = e.parentNode;
	var node = igtree_getNodeById(eNode.id);
	if(!node) return;
	if(!node.WebTree.AllowDrag)
		return;
	if(!node.getEnabled())
		return;
	igtree_bDragSelect = true;

	// B.C. 23 November, 2011. Bug #85701. In the diffrent browsers the selection APIs are different.
	if(ig_csom.IsIE) 
	{
		r = document.body.createTextRange();
		r.moveToElementText(e);
		r.select();
	}
	else
	{
		window.getSelection().removeAllRanges();
		r = document.createRange();
		r.setStartBefore(e);
		r.setEndAfter(e);
		window.getSelection().addRange(r);
	}
	
	// [KV 10/29/2004, 3:40 PM] save the selection, so that we unselect it later.
	ig_treeSelectedTextRange = r;
  
	event.cancelBubble = true;
	igtree_bDragSelect = false;
}

function igtree_drag(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	var node = tree.getNodeById(eNode.id);
	var oEvent = igtree_fireEvent1(tree,tree.Events.Drag,node,ig_dataTransfer,evnt);
}
function igtree_dragend(evnt, treeId) 
{
	igtree_clearCurrDropNode();
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
		
	if(ig_currDropNode && ig_currDropNode != src) {
		var e = ig_getNodeTextElement(ig_currDropNode);
		if(e && e.igdrop != null) {
			e.className = e.igdrop;
		}
		else
		if(e)
			e.className = "";
	}
	if (ig_treeSelectedTextRange) 
	{
		ig_treeSelectedTextRange.execCommand("Unselect");
		delete ig_treeSelectedTextRange;
		ig_treeSelectedTextRange = null;
	}
	
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	var node = tree.getNodeById(eNode.id);
	evnt.returnValue = false;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragEnd,node,ig_dataTransfer,evnt);
}

function igtree_clearCurrDropNode() {
	if(ig_currDropNode) {
		var e = ig_getNodeTextElement(ig_currDropNode);
		if(e && e.igdrop != null) 
			e.className = e.igdrop;
	}
}

function igtree_dragenter(evnt, treeId) 
{
	if(!ig_dataTransfer)
		ig_dataTransfer = new ig_DataTransferObject(evnt.dataTransfer, null, null);

	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	if(!tree.AllowDrop) {
		return;
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	if(eNode.id.indexOf(tree.Id) == -1)
		return;
	var node = tree.getNodeById(eNode.id);
	if(ig_currDropNode != node) {
		igtree_clearCurrDropNode();
		ig_currDropNode = node;
		var selClass = tree.HiliteClass;
		e = ig_getNodeTextElement(node);
		if(e) {
			e.igdrop = e.className;
			e.className = selClass;
		}
	}
	evnt.returnValue = false;

    // K.D. April 1st, 2011 Drag and Drop would not work in IE9 fixed by accessing the event through the window element
    if(ig_csom.IsIE9Plus)
        window.event.returnValue = false;

	var oEvent = igtree_fireEvent1(tree,tree.Events.DragEnter,node,ig_dataTransfer,evnt);
	// B.C. Novenber 23th, 2011, Bug #85701, Calling the preventDefault method during both a dragenter and dragover event will indicate that a drop is allowed at that location.
	if (evnt.preventDefault) {
		evnt.preventDefault();
	}
	return false;
}
function igtree_dragover(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	if(eNode.id.indexOf(tree.Id) == -1)
		return;
	if(!tree.AllowDrop) {
		return;
	}
	var node = tree.getNodeById(eNode.id);
	if(ig_dataTransfer.sourceObject == node) {
		evnt.dataTransfer.effectAllowed = "none";
		return;
	}
	if(!node.getEnabled()) {
		evnt.dataTransfer.effectAllowed = "none";
		return false;
	}
	evnt.returnValue = false;

    // K.D. April 1st, 2011 Drag and Drop would not work in IE9 fixed by accessing the event through the window element
    if(ig_csom.IsIE9Plus)
        window.event.returnValue = false;
	
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragOver,node,ig_dataTransfer,evnt);
	// B.C. Novenber 23th, 2011, Bug #85701, Calling the preventDefault method during both a dragenter and dragover event will indicate that a drop is allowed at that location.
	if (evnt.preventDefault) {
		evnt.preventDefault();
	}
	return false;
}
function igtree_dragleave(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	var eNode = igtree_getNodeElement(src);
	if(!eNode)
		return;
	if(eNode.id.indexOf(tree.Id) == -1)
		return;
	var node = tree.getNodeById(eNode.id);
	if(ig_currDropNode != node) {
		var e = ig_getNodeTextElement(node);
		if(e && e.igdrop != null) {
			e.className = e.igdrop;
		}
		else
		if(e)
			e.className = "";
	}
	evnt.returnValue = false;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.DragLeave,node,ig_dataTransfer,evnt);
}
function igtree_drop(evnt, treeId) 
{
	var tree = igtree_getTreeById(treeId);
	var src=igtree_getSrcNodeElement(evnt,treeId);
	if(!src)
		return;
	if(src.tagName == "TBODY") {
		src = ig_csom.getElementById(treeId);
	}
	if(!tree.AllowDrop) {
		return;
	}
	var node;
	if(src.id == "T_" + treeId)
		node = null;
	else {
		var eNode = igtree_getNodeElement(src);
		if(!eNode)	return;
		node = tree.getNodeById(eNode.id);
	}
	if(ig_dataTransfer.sourceObject == node)
		return;
	evnt.returnValue = true;  
	var oEvent = igtree_fireEvent1(tree,tree.Events.Drop,node,ig_dataTransfer,evnt);
	if(tree.NeedPostBack && node) {
		tree.NeedPostBack=false;
		__doPostBack(tree.UniqueId,node.Id+":Dropped:"+ig_dataTransfer.sourceName+":"+ escape(ig_dataTransfer.dataTransfer.getData("Text")) );
	}
	ig_currDropNode = null;
	if(node) {
		var e = ig_getNodeTextElement(node);
		if(e) {			
			if(e.className == tree.HiliteClass) {
				e.className =  e.igdrop;
			}
			else if(e.igdrop == null)
				e.className = "";
		}
	}
	if(ig_dataTransfer) {
		ig_dataTransfer = null;
	}
	// B.C. 23 November, 2011. Bug #85701. Because the tree items in most casses are links this will prevent them from opening on drop.
	if(evnt.preventDefault) {
		evnt.preventDefault();
	}
	return false;
}
function igtree_fireEvent1(oTree,eventObj,oNode,dataTransfer,evnt) {
	var oEvent = new ig_EventObject();
	oEvent.event = evnt;
	ig_fireEvent(oTree,eventObj[0],oNode,dataTransfer,oEvent);
	if(oTree.TreeLoaded && eventObj[1]==1 && !oTree.CancelPostBack)
		oTree.NeedPostBack = true;
	oTree.CancelPostBack=false;
	return oEvent;
}
var ig_dataTransfer;
function ig_DataTransferObject(dataTransfer, sourceName, sourceObject) {
	this.dataTransfer = dataTransfer;
	this.sourceName = sourceName;
	this.sourceObject = sourceObject;
}

function ig_getNodeTextElement(node) {
	
	


 
	if(node.element)
	{
	    var i;
	    for(i=0; i < node.element.childNodes.length; i++) {
		    var attrib = node.element.childNodes[i].getAttribute("igTxt");
		    if (attrib=="1")
		        return node.element.childNodes[i];
	    }
	}
	return null;
}



if (window.addEventListener) {
	window.addEventListener("load",igtree_loadcomplete,false);
}
else if (window.attachEvent) {
	window.attachEvent("onload",igtree_loadcomplete);
}


var constTimerInterval = 30;
var constAccelDecelTimerInterval = 10;
var constUseAnimation = true;

var AnimationEnum = new function() {
   this.None = 0;
   this.Accelerate  = 1;
   this.Decelerate  = 2; 
   this.AccelDecel = 3;
}
