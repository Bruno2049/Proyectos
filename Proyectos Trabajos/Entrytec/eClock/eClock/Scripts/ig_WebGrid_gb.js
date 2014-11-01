/*
* ig_WebGrid_gb.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/







// ig_WebGrid_gb.js
// Infragistics UltraWebGrid Script 
// Copyright (c) 2001-2007 Infragistics, Inc. All Rights Reserved.
var igtbl_documentMouseMove=null;
var igtbl_documentMouseUp=null;

function igtbl_dragDropMouseMove(evnt)
{
	if(!evnt)
		evnt=event;
	var gs=igtbl_getGridById(igtbl_lastActiveGrid);
	






	if(!gs && igtbl_documentMouseMove || (igtbl_button(igtbl_lastActiveGrid,evnt)!=0 && !gs.Element.getAttribute("mouseDown")))
	{
		igtbl_headerDragDrop();
		return;
	}
	if(!gs)
		return;
	gs.event=evnt;
	




	if(gs._colMovingTimerID) 
	{
		window.clearTimeout(gs._colMovingTimerID);
		gs._colMovingTimerID = null;
	}
	if(gs.dragDropDiv && gs.dragDropDiv.style.display=="")
	{		
		var col=gs.dragDropDiv.srcElement;
		var bandNo=parseInt(igtbl_bandNoFromColId(col.id),10);
		var band=gs.Bands[bandNo];
		var colNo=parseInt(igtbl_colNoFromColId(col.id),10);
		var x=evnt.clientX+igtbl_getBodyScrollLeft();
		var y=evnt.clientY+igtbl_getBodyScrollTop();				
		




		gs.dragDropDiv.style.left=(x-gs.dragDropDiv.offsetWidth/2)+"px";
		gs.dragDropDiv.style.top=(y-gs.dragDropDiv.offsetHeight/2)+"px";
		var gb=gs.GroupByBox;
		var gbx;
		var gby;

		
		
    	var pNode = gs.MainGrid.parentNode;
        var totalScrollTop = 0;
        var totalScrollLeft = 0
        while(pNode && pNode.tagName != "BODY")
        {
            totalScrollTop += pNode.scrollTop;
            totalScrollLeft += pNode.scrollLeft;
            pNode = pNode.parentNode;
        }
                
        




		if(gb.Element)
		{
			gbx=igtbl_getLeftPos(gb.Element,false);
			
			gbx-=totalScrollLeft;
			gby=igtbl_getTopPos(gb.Element,false);
			
			gby-=totalScrollTop;
		}										
		if(gb.Element && x>=gbx && x<gbx+gb.Element.offsetWidth && y>=gby && y<gby+gb.Element.offsetHeight && band.Columns[colNo].AllowGroupBy==1)
		{			
			if(gb.groups.length==0)
			{
				gb.pimgUp.style.display="";
				




				gb.pimgUp.style.left=(gbx-gb.pimgUp.offsetWidth/2)+"px";
				gb.pimgUp.style.top=(gby+gb.Element.offsetHeight)+"px";
				gb.pimgDn.style.display="";
				gb.pimgDn.style.left=(gbx-gb.pimgDn.offsetWidth/2)+"px";
				gb.pimgDn.style.top=(gby-gb.pimgDn.offsetHeight)+"px";
				gb.postString="group:"+bandNo+":"+colNo+":true:band:"+bandNo;												
			}
			else
			{
				var el=null;
				var frontPark=false;
				var grNo=0;
				for(var i=0;i<gb.groups.length;i++)
				{
					var ge=gb.groups[i].Element;
					var gex=igtbl_getLeftPos(ge,false);
					
					gex-=totalScrollLeft;
					var gey=igtbl_getTopPos(ge,false);
					
					gey-=totalScrollTop;
					var eBandNo=gb.groups[i].groupInfo[1];
					if(eBandNo<bandNo)
					{
						el=gb.groups[i];
						grNo=i;
						frontPark=false;
					}
					else if(eBandNo==bandNo)
					{
						if(!(el && x<gex))
						{
							el=gb.groups[i];
							grNo=i;
							if(el.groupInfo[0]=='band' || x<gex+ge.offsetWidth/2)
								frontPark=true;
							else
								frontPark=false;
							if(x>=gex && x<gex+ge.offsetWidth)
								break;
						}
					}
					else if(!el)
					{
						el=gb.groups[i];
						grNo=i;
						frontPark=true;
					}
				}
				if(el && (((el.groupInfo[0]=="col" && !(el.groupInfo[1]==bandNo && el.groupInfo[2]==colNo) || el.groupInfo[0]=="band") && (frontPark && (grNo==0 || gb.groups[grNo-1].groupInfo[0]=="band" || !(gb.groups[grNo-1].groupInfo[1]==bandNo && gb.groups[grNo-1].groupInfo[2]==colNo)) || !frontPark && (grNo>=gb.groups.length-1 || gb.groups[grNo+1].groupInfo[0]=="band" || !(gb.groups[grNo+1].groupInfo[1]==bandNo && gb.groups[grNo+1].groupInfo[2]==colNo))))))
				{
					var gex=igtbl_getLeftPos(el.Element,false);
					

					gex-=totalScrollLeft;
					var gey=igtbl_getTopPos(el.Element,false);
					

					gey-=totalScrollTop;
					gb.pimgUp.style.display="";
					




					gb.pimgUp.style.left=(gex-gb.pimgUp.offsetWidth/2+(frontPark?0:el.Element.offsetWidth))+"px";
					gb.pimgUp.style.top=(gey+el.Element.offsetHeight)+"px";
					gb.pimgDn.style.display="";
					gb.pimgDn.style.left=(gex-gb.pimgDn.offsetWidth/2+(frontPark?0:el.Element.offsetWidth))+"px";
					gb.pimgDn.style.top=(gey-gb.pimgDn.offsetHeight)+"px";
					gb.postString="group:"+bandNo+":"+colNo+":"+frontPark+":"+el.groupInfo[0]+":"+el.groupInfo[1]+(el.groupInfo[0]=="col"?":"+el.groupInfo[2]:"");
				}
				else
				{
					gb.postString="";
					gb.moveString="";
					gb.pimgUp.style.display="none";
					gb.pimgDn.style.display="none";
				}
			}
		}
		else
		{			
			var defaultInit=true;
			if(band.AllowColumnMoving>1 && !band.HasHeaderLayout && !band.HasFooterLayout)
			{
				var gdiv;
				if(bandNo==0)
				{
					if((gs.StationaryMargins==1 || gs.StationaryMargins==3) && gs.StatHeader)
						gdiv=gs.StatHeader.Element.parentNode.parentNode;
					else
						gdiv=gs.Element.parentNode;
				}
				else
					gdiv=col.parentNode;
				var gx=igtbl_getLeftPos(gdiv);
				var gy=igtbl_getTopPos(gdiv);
				




				

				
				if(ig_csom.IsIE && igtbl_isXHTML) 
				{
					









					gx=gx-gdiv.scrollLeft;
					gy=gy-gs.DivElement.scrollTop;
				}
				
				var colEl=igtbl_overHeader(gs.Rows,x,y,gx,gy,gdiv.offsetWidth,gdiv.offsetHeight, totalScrollTop, totalScrollLeft);
				if(colEl)
				{
					
					var tBandNo=parseInt(igtbl_bandNoFromColId(colEl.id),10);
					var tColNo=parseInt(igtbl_colNoFromColId(colEl.id),10);
					if(tBandNo==bandNo && tColNo!=colNo)
					{
						
						var cx=igtbl_getLeftPos(colEl,false);
						var cy=igtbl_getTopPos(colEl,false);						

						
														
						
						if(ig_csom.IsIE && igtbl_isXHTML) 
						{
							










							cx=cx-gdiv.scrollLeft;
							cy=cy-gs.DivElement.scrollTop;							
						}												
						
						var ow=colEl.offsetWidth;
						if(cx+ow>gx+gdiv.offsetWidth) 
						{
							ow=gx+gdiv.offsetWidth-cx;
						}
						




						else if(cx<gx) 
						{
							ow = ow-(gx-cx);
							cx = gx;
						}
						
						
						var frontPark=false;
						

						if(x<(cx - totalScrollLeft)+ow/2)
							frontPark=true;

						var beforeColId=colEl.id;
						var col=gs.Bands[tBandNo].Columns[tColNo];
						var mCol=band.Columns[colNo];
						var beforeCol=gs.Bands[tBandNo].Columns[tColNo+1];
						if(beforeCol==mCol && mCol.IsGroupBy)

							beforeCol=gs.Bands[tBandNo].Columns[tColNo+2];
						if(!frontPark && beforeCol)
							beforeColId=beforeCol.Id;
						else if(!frontPark)
							beforeColId=null;
						
						





						if(gs.UseFixedHeaders && frontPark && !col.getFixed())
						{
							
							var overlapColEl=igtbl_overHeader(gs.Rows,cx,y,gx,gy,gdiv.offsetWidth,gdiv.offsetHeight, totalScrollTop, totalScrollLeft);
							if(overlapColEl && colEl!=overlapColEl && igtbl_getColumnById(overlapColEl.id).getFixed()) 
							{					
								newX=igtbl_getLeftPos(overlapColEl,false)+overlapColEl.offsetWidth;
								ow=cx-newX;
								cx=newX;
								col.colMovingFixedEdge=true;
							}
						}
						
						var allowMove=false;
						if(!gs.UseFixedHeaders || (frontPark && (mCol.Fixed && (col.Fixed || tColNo>0 && tColNo-1!=colNo && gs.Bands[tBandNo].Columns[tColNo-1].Fixed) || !mCol.Fixed && !col.Fixed) || !frontPark && (mCol.Fixed && col.Fixed || !mCol.Fixed && (!beforeCol || mCol!=beforeCol && !beforeCol.Fixed))))
							allowMove=true;

						if(allowMove && (frontPark && (!colEl.previousSibling || !colEl.previousSibling.id || parseInt(igtbl_colNoFromColId(colEl.previousSibling.id),10)!=colNo) ||
								!frontPark && (!colEl.nextSibling || !colEl.nextSibling.id || parseInt(igtbl_colNoFromColId(colEl.nextSibling.id),10)!=colNo)))
						{							
							

							if(igtbl_fireEvent(gs.Id,gs.Events.ColumnDrag,"(\""+gs.Id+"\",\""+mCol.Id+"\","+(beforeColId?"\""+beforeColId+"\"":null)+")")!=true)
							{								
	    						gb.pimgUp.style.display="";
								




	                            								
								gb.pimgUp.style.left=(cx- totalScrollLeft -gb.pimgUp.offsetWidth/2+(frontPark?0:ow))+"px";
								gb.pimgUp.style.top=(cy-totalScrollTop+colEl.offsetHeight)+"px";
								gb.pimgDn.style.display="";
								gb.pimgDn.style.left=(cx- totalScrollLeft -gb.pimgDn.offsetWidth/2+(frontPark?0:ow))+"px";
								gb.pimgDn.style.top=(cy-totalScrollTop-gb.pimgDn.offsetHeight)+"px";
								





								
								if(gs.dragDropDiv.srcElement && gs.dragDropDiv.srcElement.getAttribute("groupInfo"))
									gb.postString="ungroup:"+bandNo+":"+colNo;
								else
									gb.postString="";
								gb.moveString="move:"+bandNo+":"+colNo+":"+frontPark+":"+tBandNo+":"+tColNo;
								defaultInit=false;
								
								




								if(cx-gx<=5 || (col.colMovingFixedEdge && frontPark))
								{
									gs._colMovingTimerID = window.setTimeout("_igtbl_columnMovingScroll('"+gs.Id+"',"+tBandNo+","+ col.Index+", 'left')", 500);
									col.colMovingFixedEdge=undefined;
								}
								else if((gx+gdiv.offsetWidth)-(cx+ow)<=5) 
									gs._colMovingTimerID = window.setTimeout("_igtbl_columnMovingScroll('"+gs.Id+"',"+tBandNo+","+ col.Index+", 'right')", 500);
								else if(gs.UseFixedHeaders && col.getFixed() && !frontPark && !mCol.getFixed()) 
								{
									
									var firstColEl=igtbl_overHeader(gs.Rows,cx+ow+4,y,gx,gy,gdiv.offsetWidth,gdiv.offsetHeight, totalScrollTop, totalScrollLeft);
									if(firstColEl)
									{
										var firstCol=igtbl_getColumnById(firstColEl.id);
										if(firstCol && !firstCol.getFixed()) 
											gs._colMovingTimerID = window.setTimeout("_igtbl_columnMovingScroll('"+gs.Id+"',"+tBandNo+","+ firstCol.Index+", 'left')", 700);
									}
								}
							}
						}
					}
				}
			}
			if(defaultInit)
			{
				if(col && col.getAttribute && col.getAttribute("groupInfo"))
					gb.postString="ungroup:"+bandNo+":"+colNo;
				else
					gb.postString="";
				gb.moveString="";
				gb.pimgUp.style.display="none";
				gb.pimgDn.style.display="none";
			}
		}
	}
	igtbl_cancelEvent(evnt);
	return true;
}

function _igtbl_columnMovingScroll(gn, bandIndex, columnIndex, direction) 
{
	




	var g=igtbl_getGridById(gn);
	var col=g.Bands[bandIndex].Columns[columnIndex];
	
	window.clearTimeout(g._colMovingTimerID);
	g._colMovingTimerID=null;
	
	if(!col.getFixed()) 
	{
		var scrollToCol=null;
		if(direction=='left')
			scrollToCol=_igtbl_previousVisibleColumn(col)
		else 
			scrollToCol=_igtbl_nextVisibleColumn(col);
		
		if(scrollToCol != null) 
		{
			igtbl_scrollToView(gn, scrollToCol.Element, scrollToCol.offsetWidth);
			g._colMovingTimerID=setTimeout("_igtbl_columnMovingScroll('"+g.Id+"',"+bandIndex+","+scrollToCol.Index+",'"+direction+"')", 500);
		}
	}
}

function _igtbl_previousVisibleColumn(col) 
{
	if(col.Index > 0)
	{
		var prevCol = col.Band.Columns[col.Index-1];
		while(prevCol != null && prevCol.getHidden()) 
		{
			if(prevCol.Index > 0)
				prevCol = col.Band.Columns[prevCol.Index-1];
			else
				prevCol = null;
		}
		return prevCol;
	}
	return null;
}

function _igtbl_nextVisibleColumn(col) 
{
	if(col.Index < col.Band.Columns.length-1)
	{
		var nextCol = col.Band.Columns[col.Index+1];
		while(nextCol != null && nextCol.getHidden()) 
		{
			if(nextCol.Index < col.Band.Columns.length-1)
				nextCol = col.Band.Columns[nextCol.Index+1];
			else
				nextCol = null;
		}
		return nextCol;
	}
	return null;
}


function igtbl_overHeader(rows,x,y,gx,gy,gw,gh, totalScrollTop, totalScrollLeft)
{
	

	if (!totalScrollTop)
		totalScrollTop = 0;
	if (!totalScrollLeft)
		totalScrollLeft =0;

	var g=rows.Grid;
	var useExp=0;

	
    y += totalScrollTop;
	               
	while(rows)
	{
		
		




		var firstRow = rows.length>0 ? rows.getRow(0) : null;
		
		



	
		if(!firstRow 		
			|| (firstRow && !firstRow.GroupByRow)
					
			|| (rows.Band.Index==0 && rows.Grid.StatHeader)
		)
		{
			var colsCount;
			if(firstRow && !firstRow.GroupByRow)
				colsCount=firstRow.cells.length;
			else
				colsCount=rows.Band.Columns.length;
			for(var i=0;i<colsCount;i++)
			{
				if(!rows.Band.Columns[i].getVisible())
					continue;
				var colEl;
				if(firstRow && !firstRow.GroupByRow)
				{
					var cell=firstRow.getCell(i);
					colEl=igtbl_getColumnByCellId(cell.Element.id);
				}
				else
					colEl=rows.Band.Columns[i].Element;
				if(colEl)
				{
					var cy=igtbl_getTopPos(colEl);
					if(y>gy+gh)
						return false;
					var cx=igtbl_getLeftPos(colEl);
					







	
									
					if(ig_csom.IsIE && igtbl_isXHTML && !g.StatHeader) 
					{
						cx=cx-g.DivElement.scrollLeft;
						cy=cy-g.DivElement.scrollTop;
					}
					
					var cx1=cx+colEl.offsetWidth;
					var cy1=cy+colEl.offsetHeight;
					if(cx<gx) cx=gx;
					if(cy<gy) cy=gy;
					if(cx1>gx+gw) cx1=gx+gw;
					if(cy1>gy+gh) cy1=gy+gh;
					if(!(y>cy && y<cy1))
						break;
					
					if(x>=(cx - totalScrollLeft) && x<(cx1-totalScrollLeft))
					{					    
						return colEl;
					}
				}
			}
		}
		rows=null;
		var i=0;
		for(var rowId in g.ExpandedRows)
		{
			if(i==useExp)
			{
				var row=igtbl_getRowById(rowId);
				rows=row.Rows;
				useExp++;
				break;
			}
			i++;
		}
	}
}

function igtbl_headerDragStart(gn,se,evnt)
{
	var gs=igtbl_getGridById(gn);
	if(!gs) return;
	var column=igtbl_getColumnById(se.id);
	if(!column) return;
	if(!column.IsGroupBy)
	{
		var j=0;
		for(var i=0;i<column.Band.Columns.length;i++)
		{
			var col=column.Band.Columns[i];
			if(col.hasCells() && col.getVisible())
				j++;
		}
		if(j<=1)
			return;
	}
	if(igtbl_fireEvent(gs.Id,gs.Events.BeforeColumnMove,"(\""+gs.Id+"\",\""+se.id+"\")")==true)
		return;
	if(!gs.dragDropDiv)
	{
		gs.dragDropDiv=document.createElement("DIV");
		gs.dragDropDiv.style.display="none";
		document.body.insertBefore(gs.dragDropDiv,document.body.firstChild);
		var gb=gs.GroupByBox;
		if(gb && gb.pimgUp.parentNode!=document.body)
		{
			gb.pimgUp.parentNode.removeChild(gb.pimgUp);
			document.body.insertBefore(gb.pimgUp,document.body.firstChild);
			gb.pimgDn.parentNode.removeChild(gb.pimgDn);
			document.body.insertBefore(gb.pimgDn,document.body.firstChild);
		}
		
		gs.dragDropDiv.setAttribute("GroupByHeaderFloatingDiv",1);
	}
	gs.dragDropDiv.style.position="absolute";
	gs.dragDropDiv.style.display="";
	if(ig_csom.IsNetscape6)
	{
		gs.dragDropDiv.style.MozOpacity="0.6";
		gs.dragDropDiv.style.cursor="-moz-grabbing";
	}
	else if(ig_csom.IsIE)
		gs.dragDropDiv.style.filter+="progid:DXImageTransform.Microsoft.Alpha(Opacity=60);";
	




	
	gs.dragDropDiv.style.left=(evnt.clientX+igtbl_getBodyScrollLeft()-se.offsetWidth/2)+"px";
	gs.dragDropDiv.style.top=(evnt.clientY+igtbl_getBodyScrollTop()-se.offsetHeight/2)+"px";
	gs.dragDropDiv.style.width=se.offsetWidth+"px";
	gs.dragDropDiv.style.height=se.offsetHeight+"px";
	gs.dragDropDiv.style.zIndex=gs._getZ(100000, 1);
	gs.dragDropDiv.innerHTML="<table style=\"width:100%;height:100%\"><thead><tr><th></th></tr></thead></table>";
	var th=gs.dragDropDiv.firstChild.firstChild.firstChild.firstChild;
	th.innerHTML=se.innerHTML;
	srcTh=se;
	while(th.tagName!="TABLE")
	{
		th.className=srcTh.className;
		th.style.cssText=srcTh.style.cssText;
		th=th.parentNode;
		srcTh=srcTh.parentNode;
	}
	gs.dragDropDiv.srcElement=se;
	igtbl_documentMouseMove=igtbl_addEventListener(document,"mousemove",igtbl_dragDropMouseMove);
	igtbl_documentMouseUp=igtbl_addEventListener(document,"mouseup",igtbl_headerDragDrop);
}

function igtbl_headerDragDrop()
{
	var gs=igtbl_getGridById(igtbl_lastActiveGrid);
	if(!gs || !gs.dragDropDiv)
		return;
	



	if(gs._colMovingTimerID) 
	{
		window.clearTimeout(gs._colMovingTimerID);
		gs._colMovingTimerID = null;
	}
	gs.dragDropDiv.style.display="none";
	igtbl_removeEventListener(document,"mousemove",igtbl_dragDropMouseMove,igtbl_documentMouseMove);
	igtbl_removeEventListener(document,"mouseup",igtbl_headerDragDrop,igtbl_documentMouseUp);
	igtbl_documentMouseUp=null;
	igtbl_documentMouseMove=null;
	gs.GroupByBox.pimgUp.style.display="none";
	gs.GroupByBox.pimgDn.style.display="none";
	var col=gs.dragDropDiv.srcElement;
	





	_igtbl_processUpdates(gs, null);
	




	
	var bandNo=parseInt(igtbl_bandNoFromColId(col.id),10);
	var band=gs.Bands[bandNo];		
	var xmlClientSideMoving = (gs.Node && band.AllowColumnMoving==3);
	
	if(gs.GroupByBox.moveString!="" && !gs.GroupByBox.postString && !xmlClientSideMoving)
		igtbl_fireEvent(gs.Id,gs.Events.AfterColumnMove,"(\""+gs.Id+"\",\""+col.id+"\")");
	if(gs.Node && band.AllowColumnMoving==3 && gs.GroupByBox.moveString!="" && gs.GroupByBox.postString=="")
	{
		var moveAr=gs.GroupByBox.moveString.split(":");
		var fromIndex=parseInt(moveAr[2],10),toIndex=parseInt(moveAr[5],10)+(moveAr[3]=="true"?0:1);
		if(fromIndex<toIndex)
			toIndex--;
		
		band.Columns[fromIndex].move(toIndex);
		





		if(gs.GroupByBox.moveString!="" && xmlClientSideMoving)
			igtbl_fireEvent(gs.Id,gs.Events.AfterColumnMove,"(\""+gs.Id+"\",\""+col.id+"\")");
	}
	else
	{
		if(gs.GroupByBox.postString!="" || gs.GroupByBox.moveString!="")
		{
			var c=igtbl_getColumnById(col.id);
			if(gs.GroupByBox.postString)
				gs._recordChange("ColumnGroup",c,gs.GroupByBox.postString);
			





			if(gs.GroupByBox.moveString)
				gs._recordChange("ColumnMove",c,gs.GroupByBox.moveString);
			igtbl_doPostBack(igtbl_lastActiveGrid,"");
		}
	}
	gs.GroupByBox.postString="";
	gs.GroupByBox.moveString="";
	
	gs.Element.removeAttribute("mouseDown");
}













