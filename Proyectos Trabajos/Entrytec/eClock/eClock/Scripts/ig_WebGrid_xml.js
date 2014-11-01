/*
* ig_WebGrid_xml.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/







// ig_WebGrid_xml.js
// Infragistics UltraWebGrid Script 
// Copyright (c) 2001-2007 Infragistics, Inc. All Rights Reserved.
function igtbl_onReadyStateChange(gridName)
{
	var gn;
	var g=this._control;
	






	if (!g && gridName)
	{
	    g=igtbl_getGridById(gridName);
	}
	gn=g.Id;
	var xmlHttp=this._xmlHttpRequest;
	if (!xmlHttp)
	{
        




	    xmlHttp=this.__xmlHttpRequest;
	}
	if(xmlHttp.readyState==4)
	{
		g.responseText=xmlHttp.responseText;
		g.ReqType=this.ReqType;
		var r=this.RowToQuery;
		g.RowToQuery=r;
		if (g.XmlResponseObject)
		{
			var a = g.XmlResponseObject;
			g.XmlResponseObject = null;
			a.Grid = null;
			igtbl_dispose(a);
		}
		var xmlRespObj = new Object();
		g.XmlResponseObject=xmlRespObj;
		xmlRespObj.ResponseStatus=g.eError.Ok;
		xmlRespObj.ReqType=g.ReqType;
		xmlRespObj.Tag=null;
		xmlRespObj.XmlResp=null;
		xmlRespObj.Cancel=false;
		xmlRespObj.Grid = g;
		if(g.responseText=="")
			xmlRespObj.ResponseStatus=g.eError.LoadFailed;
		else
		{
			var start=g.responseText.indexOf("<xml");
			var end=g.responseText.indexOf("</xml>")+6;
			if(!ig_csom.IsIE)
				g.XmlResp=g.DOMParser.parseFromString(g.responseText.substr(start,end-start),"text/xml");
			else
				g.XmlResp.loadXML(g.responseText.substr(start,end-start));
			var node=g.XmlResp.selectSingleNode("xml/UltraWebGrid/XmlHTTPResponse");
			if(node)
			{
				xmlRespObj.Node = node;
				xmlRespObj.StatusMessage = unescape(node.selectSingleNode("StatusMessage").text);
				xmlRespObj.Tag = unescape(node.selectSingleNode("Tag").text);
				xmlRespObj.XmlResp=g.XmlResp;
				if(node.getAttribute("ResponseStatus")!=0)
					xmlRespObj.ResponseStatus=g.eError.LoadFailed;
				xmlRespObj.Cancel=(node.selectSingleNode("Cancel").text=="true");	
				var srlNode=node.selectSingleNode("ServerRowsLength");
				
				if(srlNode)
				{
					xmlRespObj.ServerRowsLength=igtbl_parseInt(srlNode.text);
					
					
						
					
					
					


					if(g.ReqType!=g.eReqType.ChildRows && 
						!(g.ReqType==g.eReqType.UpdateRow && g.LoadOnDemand == 3 && g.XmlLoadOnDemandType == 2))
						g.RowsServerLength=xmlRespObj.ServerRowsLength;
				}
				

				xmlRespObj.applyCssClasses = function()
				{
					var cssNode=this.Node.selectSingleNode("Css");
					if(cssNode)
					{
						var ssIndex=document.styleSheets.length-1;
						var styleSheet=null;
						if(ssIndex>=0)
							styleSheet=document.styleSheets[ssIndex];
						while(styleSheet && styleSheet.href)
						{
							if(--ssIndex>=0)
								styleSheet=document.styleSheets[ssIndex];
							else
								styleSheet=null;
						}
						
						


















						if(styleSheet)
							this.Grid._asyncCssClasses=ig_shared.getCBManager()._setCss(this.Grid._asyncCssClasses, styleSheet.cssText+cssNode.text, null, true);
					}
				}
			}
			else
			{
				xmlRespObj.StatusMessage=g.responseText;
				xmlRespObj.ResponseStatus=g.eError.LoadFailed;
				var de=g.getDivElement();
				de.removeAttribute("oldST");
				de.removeAttribute("noOnScroll");
				g.Error=g.eError.LoadFailed;
				g.ReqType = g.eReqType.None;
			}
		}
		if(g.fireEvent(g.Events.XmlHTTPResponse,[g.Id,r&&r.Element?r.Element.id:"",g.XmlResponseObject]) || xmlRespObj.ResponseStatus==g.eError.LoadFailed)
		{
			if(g.Events.XmlHTTPResponse[1]==1)
				g.NeedPostBack=false;
			g.ReadyState=g.eReadyState.Ready;
			g.Error=g.eError.LoadFailed;
			if(g.ReqType==g.eReqType.UpdateRow)
				g.RowToQuery._generateUpdateRowSemaphore(true);
			if(g._servingXmlHttp)
				igtbl_dispose(g._servingXmlHttp);
			g._hidePI();
			this.RowToQuery=null;
			return;
		}
		if(g.Events.XmlHTTPResponse[1]==1)
			g.NeedPostBack=false;
		switch(g.ReqType)
		{
			case g.eReqType.ChildRows:
				igtbl_requestChildRowsComplete(gn);
				break;
			case g.eReqType.MoreRows:
				







				g.requestingMoreRows = false;
				igtbl_requestMoreRowsComplete(gn);
				igtbl_updateFooters(gn);
				break;
			case g.eReqType.Refresh:
			case g.eReqType.Sort:
				igtbl_requestSortComplete(gn);
				igtbl_updateFooters(gn);
                




                g._calculateStationaryHeader();
				





				g._removeChange("PageChanged",g);
				break;
			case g.eReqType.UpdateRow:
				igtbl_requestUpdateRowComplete(gn);
				break;
			case g.eReqType.Page:
				igtbl_requestPageComplete(gn);
				igtbl_updateFooters(gn);
				break;
			case g.eReqType.Scroll:
				igtbl_requestScrollComplete(gn);
				break;
			case g.eReqType.FilterDropDownFill:				
				igtbl_requestFilterDropDownFillComplete(gn);
				break;
			case g.eReqType.Filter:
				igtbl_requestFilterComplete(gn);
				igtbl_updateFooters(gn);
				
				g._calculateStationaryHeader();
								
				



				var de=g.getDivElement();	
				if(de.getAttribute("oldSL"))
				{
					igtbl_scrollLeft(de,parseInt(de.getAttribute("oldSL")));
					de.removeAttribute("oldSL");
				}
				




				g._removeChange("PageChanged",g);
				break;
            case g.eReqType.UpdateCell:
                igtbl_requestUpdateCellComplete(gn);
                break;
			default:
				igtbl_requestComplete(gn
					,xmlHttp
				);
				break;
		}
		g.ReadyState=g.eReadyState.Ready;
		g.fireEvent(g.Events.AfterXmlHttpResponseProcessed,[g.Id]);
		if(g.ReqType!=g.eReqType.None)
		{
			g.RowToQuery=null;
			this.RowToQuery=null;
		}
		g.ReqType=g.eReqType.None;
		g.Error=g.eError.Ok;
		if(g._servingXmlHttp)
			igtbl_dispose(g._servingXmlHttp);
		g._hidePI();
		this.RowToQuery=null;
	}
	else if(this._timedOut)
	{
		g.Error=g.eError.LoadFailed;
		g.ReqType = g.eReqType.None;
		g._hidePI();
		var xmlRespObj = new Object();
		g.XmlResponseObject=xmlRespObj;
		xmlRespObj.ResponseStatus=g.eError.Timeout;
		xmlRespObj.StatusMessage = this._timedOut;
		xmlRespObj.ReqType=g.ReqType;
		xmlRespObj.Tag=null;
		xmlRespObj.XmlResp=null;
		xmlRespObj.Cancel=true;
		xmlRespObj.Grid = g;

		g.fireEvent(g.Events.XmlHTTPResponse,[gn, "",g.XmlResponseObject]);
		if(g._servingXmlHttp)
			igtbl_dispose(g._servingXmlHttp);
	}
}





function igtbl_requestUpdateCellComplete(gn)
{
    var gs=igtbl_getGridById(gn);
	for(var cell in gs.ChangedCells)
	{
	    gs._removeChange("ChangedCells",igtbl_getCellById(cell));
	}
}

function igtbl_requestChildRowsComplete(gn)
{
	var g=igtbl_getGridById(gn);
	var r=g.RowToQuery;
	{
		var rowsNode=g.XmlResp.selectSingleNode("form");
		if(!rowsNode)
			rowsNode=g.XmlResp;
		
		var selStr="xml/UltraWebGrid/Rs";
		for(var i=0;i<=r.Band.Index;i++)
		{
			var cband=g.Bands[i];
			if(cband.SortedColumns && cband.SortedColumns.length>0)
				for(var j=0;j<cband.SortedColumns.length;j++)
				{
					var col=igtbl_getColumnById(cband.SortedColumns[j]);
					if(col.IsGroupBy)
						selStr+="/Group/Rs"
					else
						break;
				}
			selStr+="/R/Rs"
		}
		rowsNode=rowsNode.selectSingleNode(selStr);
		




	
		if(rowsNode!=null)
		{
			r.Node.appendChild(rowsNode);			
			if(!r.Rows)
				r.Rows=new igtbl_Rows(r.Node.selectSingleNode("Rs"), r.Band.Grid.Bands[r.Band.Index+1], r);
			else
			{
				




				r.Rows.Node=rowsNode;
				r.Rows.SelectedNodes=rowsNode.selectNodes("R");
				if(r.Rows.SelectedNodes.length==0)
					r.Rows.SelectedNodes=rowsNode.selectNodes("Group");
				r.Rows.length=r.Rows.SelectedNodes.length;
			}
			




			var rowIslandFilters=null;
			if (rowsNode)rowIslandFilters=eval(rowsNode.getAttribute("columnFilters"));
			if (rowIslandFilters)
			{	
				_igtbl_processServerPassedColumnFilters(rowIslandFilters,g);
			}
			r.prerenderChildRows();
			r.Rows.render();
		}
	}
	r._setExpandedComplete(true);
}

function igtbl_onScrollXml(evnt,gn)
{
	var g=igtbl_getGridById(gn);
	g.event=evnt;
	var de=g.getDivElement();
	if(g.noMoreRows)
		return;
	
	

	if(de && de.scrollHeight==de.scrollTop+de.clientHeight && g.RowsRange>0
		&& (g.XmlLoadOnDemandType==0
		|| g.XmlLoadOnDemandType==4
		)
		|| (g.XmlLoadOnDemandType==1 && 
		 ((ig_csom.IsFireFox30 ? (de.parentNode.childNodes[1].scrollHeight - igtbl_parseInt(de.parentNode.childNodes[1].firstChild.style.top)) : de.parentNode.childNodes[1].scrollHeight) <=de.scrollTop+de.clientHeight + 50))		 
	)
	{
		if(g.RowsServerLength>g.Rows.length)
		{
			




			if(g.ReadyState==0)
			g.invokeXmlHttpRequest(g.eReqType.MoreRows);
			return igtbl_cancelEvent(evnt);
		}
	}
	if(g.XmlLoadOnDemandType==2)
	{
		if(g._vScrTimer)
			window.clearTimeout(g._vScrTimer);
		
		

		if(!g.fireEvent(g.Events.XmlVirtualScroll,[g.Id,Math.round(de.scrollTop/g.getDefaultRowHeight())]))
			g._vScrTimer=window.setTimeout("igbtl_vScrollGrid('"+gn+"')",g.VirtualScrollDelay);
	}
}

function igbtl_vScrollGrid(gn)
{
	var g=igtbl_getGridById(gn);
	delete g._vScrTimer;
	g.invokeXmlHttpRequest(g.eReqType.Scroll);
}

function igtbl_requestMoreRowsComplete(gn)
{
	var g=igtbl_getGridById(gn);
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;
		node=node.selectSingleNode("xml/UltraWebGrid/Rs");
		if(!node)
		{
			igtbl_cancelNoOnScroll(gn);
			return;
		}
		g.Node.appendChild(node);
		var strTransform=g.Rows.applyXslToNode(node,g.Rows.SelectedNodes.length);
		if(strTransform)
		{
			g._innerObj.innerHTML="<table>"+strTransform+"</table>";
    		g.Node.removeChild(node);
			var nodes=node.selectNodes("R");
			if(nodes.length==0)
				nodes=node.selectNodes("Group");
			g.Rows.length+=nodes.length;
			g.RowsRetrieved+=nodes.length;
			






			var beforeWidth = g.Rows.Element.parentNode.offsetWidth;
			for(var i=0;i<nodes.length;i++)
			{
				g.Rows.Node.appendChild(nodes[i]);
				g.Rows.Element.appendChild(g._innerObj.firstChild.rows[0]);
			}
			igtbl_fixDOEXml();
			g.Rows.SelectedNodes=g.Rows.Node.selectNodes("R");
			if(g.Rows.SelectedNodes.length==0)
				g.Rows.SelectedNodes=g.Rows.Node.selectNodes("Group");
			if(g._scrElem && beforeWidth < g.Rows.Element.parentNode.offsetWidth)
			{
				






				g._scrElem.firstChild.style.width="";
			}
			g.alignDivs(0,true);
		}
	}
	
	g.Rows.setLastRowId(g.Rows.length?g.Rows.getRow(g.Rows.length-1).Id:null);
	
	

	var de=g.getDivElement();
	

	if ((g.XmlLoadOnDemandType==3 && g.Rows.length<g.RowsServerLength) || 
		(g.XmlLoadOnDemandType==1 && 
		(ig_csom.IsFireFox30 ? (de.parentNode.childNodes[1].scrollHeight - igtbl_parseInt(de.parentNode.childNodes[1].firstChild.style.top)) : de.parentNode.childNodes[1].scrollHeight)<=de.scrollTop+de.clientHeight + 50))
		window.setTimeout("_igtbl_getMoreRows('"+g.Id+"');",100);
	if(g.XmlLoadOnDemandType==4)
	{
		g._removeChange("RowsRetrieved",g);
		g._recordChange("RowsRetrieved",g,g.RowsRetrieved);
	}

	g.cancelNoOnScrollTimeout=window.setTimeout("igtbl_cancelNoOnScroll('"+g.Id+"')",100);
}

function igtbl_isArLess(a1,a2)
{
	if(a1.length<a2.length)
		return true;
	if(a1.length>a2.length)
		return false;
	for(var i=0;i<a1.length;i++)
	{
		if(a1[i]<a2[i])
			return true;
		if(a1[i]>a2[i])
			return false;
	}
	return false;
}

function igtbl_sortRowIdsByClctn(rc)
{
	var ar=new Array(),i=0;
	for(var rowId in rc)
	{
		var row=igtbl_getRowById(rowId);
		if(row)
			ar[i++]=row.getLevel();
		else
			ar[i++]=rowId.split('_').slice(1);
	}
	for(var i=0;i<ar.length;i++)
		for(var j=0;j<ar[i].length;j++)
			ar[i][j]=parseInt(ar[i][j],10);
	var sorted=false;
	while(!sorted)
	{
		sorted=true;
		for(var i=0;i<ar.length-1;i++)
			if(igtbl_isArLess(ar[i],ar[i+1]))
			{
				var a=ar[i];
				ar[i]=ar[i+1];
				ar[i+1]=a;
				sorted=false;
			}
	}
	return ar;
}







function _igtbl_PagerRedraw(g)
{
	var node=g.XmlResp.selectSingleNode("form");
	if(!node)
		node=g.XmlResp;
    node=node.selectSingleNode("xml/UltraWebGrid/Pager");
	



    var pager = igtbl_getDocumentElement(g.UniqueID+"_pager");
    if(pager)
    {
	    if(!pager.length)
	    {
		    var oldElem = pager;
		    pager = new Array(1);
		    pager[0] = oldElem;
	    }
	    for (var i=0;i<pager.length;i++)
		    pager[i].innerHTML = unescape(node.getAttribute("Labels"));
		    
	    
	    g.CurrentPageIndex=igtbl_parseInt(node.getAttribute("CurrentPageIndex"));
	    


	    
	    g.PageCount=igtbl_parseInt(node.getAttribute("PageCount"));
	 }
}

function igtbl_requestPageComplete(gn)
{
	var g=igtbl_getGridById(gn);
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;		
		if (node)
		{
			g.clearSelectionAll();
			g.setActiveCell(null);
			g.setActiveRow(null);
			igtbl_requestSortComplete(gn);
			
			






			g._removeChange("PageChanged",g);
			g._recordChange("PageChanged",g,g._pageToGo);
		}
	}
}






function igtbl_refillXmlGrid(gn)
{
	var g=igtbl_getGridById(gn);
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;
		if (node)
		{
			g.clearSelectionAll();
			g.setActiveCell(null);
			g.setActiveRow(null);
			igtbl_requestSortComplete(gn);
		}
	}
	g._calculateStationaryHeader();
}

function igtbl_requestScrollComplete(gn)
{
	var g=igtbl_getGridById(gn);	
	igtbl_refillXmlGrid(gn);
	var de=g.getDivElement();
	de.removeAttribute("oldST");
	de.removeAttribute("noOnScroll");
}









function _igtbl_replaceRowIsland(g,node,rows)
{
	igtbl_replaceChild(rows.Node.parentNode,node,rows.Node);
	rows.Node=node;
	rows.SelectedNodes=node.selectNodes("R");
	// AK 3/9/2006 BR10794: Cannot click on Expansion indicator when XML Paging and GroupingBy a column initially on server side. 
	if(rows.SelectedNodes.length==0)
		rows.SelectedNodes=node.selectNodes("Group");
	var arIndex=-1,acColumn=null,acrIndex=-1,aRows=null;
	if(g.oActiveRow && g.oActiveRow.OwnerCollection==rows)
		arIndex=g.oActiveRow.getIndex();
	if(g.oActiveRow && g.oActiveRow.Band.Index>=rows.Band.Index)
		g.setActiveRow(null);
	if(g.oActiveCell && g.oActiveCell.Row.OwnerCollection==rows)
	{
		acColumn=g.oActiveCell.Column;
		acrIndex=g.oActiveCell.Row.getIndex();
	}
	if(g.oActiveCell && g.oActiveCell.Band.Index>=rows.Band.Index)
		g.setActiveCell(null);
	rows.dispose();
	rows.length=rows.SelectedNodes.length;
	rows.render();
	if(arIndex!=-1)
	{
		





		var r = rows.getRow(arIndex);
		if(r)r.activate();
	}	
	if(acColumn)
	{
		if(acrIndex==-1)
		{
			if(rows.AddNewRow)
				rows.AddNewRow.getCellByColumn(acColumn).activate();
		}
		else if(acrIndex<rows.length)
			rows.getRow(acrIndex).getCellByColumn(acColumn).activate();
	}
	g.RowsRetrieved=rows.length;
	if(rows.Band.Index==0&& g.ReqType!=g.eReqType.Scroll)
	{
		if(g._scrElem)
		{
			igtbl_scrollTop(g._scrElem,0);
			g.alignDivs();
		}
		else
			igtbl_scrollTop(g.DivElement,0);
	}
}

function igtbl_requestSortComplete(gn)
{
	var g=igtbl_getGridById(gn);	
	{	
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;







		


        if((!g.RowToQuery || !g.RowToQuery.Rows || !g.Bands)|| (g.RowToQuery && g.RowToQuery.Rows && g.Bands && g.RowToQuery.Rows.Band == g.Bands[0]))
			_igtbl_PagerRedraw(g);
		node=node.selectSingleNode("xml/UltraWebGrid/Rs");
		if(!node) return;
		var rows=g.Rows;
		if(g.RowToQuery && g.RowToQuery.Rows)
		{
			rows=g.RowToQuery.Rows;
			for(var i=0;i<rows.Band.Index;i++)
				node=node.selectSingleNode("R/Rs")
		}
		if(!node) return;
		_igtbl_replaceRowIsland(g,node,rows);
		




		g.alignDivs(0,true);
	}
	if(g.XmlLoadOnDemandType==4)
		g._removeChange("RowsRetrieved",g);
}

function igtbl_requestUpdateRowComplete(gn)
{
	var g=igtbl_getGridById(gn);
	var r=g.RowToQuery;
	






	
	if(!r || !r.cells || !r.Band)
		return;
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)node=g.XmlResp;
		node=node.selectSingleNode("xml/UltraWebGrid/XmlHTTPResponse");
		if (node)
		{
			var cellsNode=node.selectSingleNode("R/Cs");
			if(cellsNode)
			{
				




			    var rowNode = node.selectSingleNode("R");
				r.DataKey = rowNode.getAttribute("DataKey");
								
				



				

				if (r.DataKey != null && r.DataKey != "undefined")
					r.set(igtbl_litPrefix+"DataKey", r.DataKey); 
				
				for(var i=0;i<cellsNode.childNodes.length;i++)
				{
					var cell=r.getCellFromKey(unescape(cellsNode.childNodes[i].getAttribute(igtbl_litPrefix+"key")));
					
					
					if(cell && cell.Column && (cell.Column.TemplatedColumn&2)==0)
					{
						var value=igtbl_getNodeValue(cellsNode.childNodes[i]);
						var oldValue=cell.getNodeValue();
						if(typeof(cell._oldValue)!="undefined")
						{
							delete cell._oldValue;
							g._removeChange("ChangedCells",cell);
						}
						if(value!=oldValue)
						{
							cell.setValue(cell.Column.getValueFromString(value),false);
							g._removeChange("ChangedCells",cell);
							




				            delete cell._oldValue;
						}
					}
				}
			}
		}
	}
	g.fireEvent(g.Events.AfterRowUpdate,[g.Id,r.Element.id]);
	if(g.Events.AfterRowUpdate[1]==1)
		g.NeedPostBack=false;
}
function igtbl_requestFilterDropDownFillComplete(gn)
{
	var g = igtbl_getGridById(gn);
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)node=g.XmlResp;
		node=node.selectSingleNode("xml/UltraWebGrid/FilterCollection");
		if (node)
		{
			var colId = node.getAttribute("ColumnId");
			var parentRowId = node.getAttribute("ParentRowId");
			var column = igtbl_getColumnById(colId);
			var workingList = new Array();
			var childNodeCount = node.childNodes.length;			
            if (ig_csom.IsIE)
            {
                var currentChildNode;
			    for(var i = 0; i < childNodeCount ; i++)
			    {			
			        currentChildNode=node.childNodes[i];
				    workingList.push([unescape(currentChildNode.getAttribute("cond")),unescape(currentChildNode.getAttribute("text"))]);
			    }
			}
			else
			{
			    var currentChildNode;
			    for(var i = 0; i < childNodeCount ; i++)
			    {
			        currentChildNode=node.childNodes[i];
			        if (currentChildNode.nodeName!="#text")
			        {
			            workingList.push([unescape(currentChildNode.getAttribute("cond")),unescape(currentChildNode.getAttribute("text"))]);
			        }
			    }
			}
			
			var filterPanel=null;
			if ((column.Band.Index==0 || column.RowFilterMode==1) && !column.Band.IsGrouped)
			{
				filterPanel=g.Bands[column.Band.Index]._filterPanels[colId];
			}
			else 
			{
				var row = igtbl_getRowById(parentRowId);
				if(row.GroupByRow)
				{
					parentRowId=parentRowId.replace( "_gr","_t");
				}
				else
				{
					parentRowId=parentRowId.replace( "_r","_t");
				}
				filterPanel=g.Bands[column.Band.Index]._filterPanels[parentRowId][colId];
			}
			filterPanel._afterFilterFilled(g,column,workingList);
		}
	}
}
function igtbl_requestComplete(gn
	,xmlHttp
)
{
	var g=igtbl_getGridById(gn);
	g.ReqType=g.eReqType.None;
	if(xmlHttp.readyState==4)
		g.ReadyState=g.eReadyState.Ready;
}
function igtbl_requestFilterComplete(gn)
{
	var g=igtbl_getGridById(gn);
	{
		var node=g.XmlResp.selectSingleNode("form");
		if(!node)
			node=g.XmlResp;
        _igtbl_PagerRedraw(g);
        var filterRowIslandInfo=node.selectSingleNode("xml/UltraWebGrid/FilterRowIsland");
        node=node.selectSingleNode("xml/UltraWebGrid/Rs");
		if(!node) return;
		





		var rows=null;
		if (filterRowIslandInfo)
		{
			var row=igtbl_getRowById(filterRowIslandInfo.getAttribute("ParentRowId"));
			var bandIndex=row.Band.Index;
			



			if(!row.GroupByRow)
			{
				while (node && (node.getAttribute("bandNo")-1<=bandIndex))
				{
					node=node.selectSingleNode("R/Rs");
				}
			}
			






			else
			{
				while (node && (node.getAttribute("bandNo")-1<=bandIndex))
				{
					node=node.selectSingleNode("Group/Rs");
				}
			}
				
			if(!node)return;
			rows = row.Rows;
		}
		else
		{
			rows=g.Rows;
		}
		



		_igtbl_replaceRowIsland(g,node,rows);
		var colFilters = node.getAttribute("columnFilters");
		if (colFilters)
		{
			colFilters = eval(colFilters);
			if (colFilters)
			{	
				_igtbl_processServerPassedColumnFilters(colFilters,g);
				_igtbl_setFilterIndicators(colFilters,rows);
			}
		}
	}
	if(g.XmlLoadOnDemandType==4)
	{
		g._removeChange("RowsRetrieved",g);
		g.alignDivs();
		g.RowsRetrieved = g.RowsRange;
	}
	g.alignStatMargins();
}
function igtbl_updateFooters(gn)
{
	var g=igtbl_getGridById(gn);
	var rows=g.Rows;
	if(g.RowToQuery && g.RowToQuery.Rows) rows=g.RowToQuery.Rows;
	{
		var node=rows.Node.lastChild;
		if(!node || node.nodeName!="Footers") return;
		var band=rows.Band;
		for(var i=0;i<band.Columns.length;i++)
		{
			var footerNode=node.childNodes[i];
			if(footerNode)
			{
				var caption=footerNode.getAttribute("caption");
				if(caption)	
				{
					



					rows.setFooterText(band.Columns[i].Key,unescape(caption));
				}
			}
			else
				break;
		}
	}
}













