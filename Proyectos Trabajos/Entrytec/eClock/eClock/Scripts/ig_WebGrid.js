/*
* ig_WebGrid.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


function igtbl_getCollapseImage(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getCollapseImage();
}

function igtbl_getExpandImage(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getExpandImage();
}

function igtbl_getCellClickAction(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getCellClickAction();
}

function igtbl_getSelectTypeCell(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	var res = g.SelectTypeCell;
	if (g.Bands[bandNo].SelectTypeCell != 0)
		res = g.Bands[bandNo].SelectTypeCell;
	return res;
}

function igtbl_getSelectTypeColumn(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	var res = g.SelectTypeColumn;
	if (g.Bands[bandNo].SelectTypeColumn != 0)
		res = g.Bands[bandNo].SelectTypeColumn;
	return res;
}

function igtbl_getSelectTypeRow(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	var res = g.SelectTypeRow;
	if (g.Bands[bandNo].SelectTypeRow != 0)
		res = g.Bands[bandNo].SelectTypeRow;
	return res;
}

function igtbl_getHeaderClickAction(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	var res = g.HeaderClickAction;
	var band = g.Bands[bandNo];
	var column = band.Columns[columnNo];
	if (column.HeaderClickAction != 0)
		res = column.HeaderClickAction;
	else if (band.HeaderClickAction != 0)
		res = band.HeaderClickAction;
	if (res > 1)
	{
		if (band.AllowSort != 0)
		{
			if (band.AllowSort == 2)
				res = 0;
		}
		else if (g.AllowSort == 0 || g.AllowSort == 2)
			res = 0;
	}
	return res;
}

function igtbl_getAllowUpdate(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	if (typeof (columnNo) != "undefined")
		return g.Bands[bandNo].Columns[columnNo].getAllowUpdate();
	var res = g.AllowUpdate;
	if (g.Bands[bandNo].AllowUpdate != 0)
		res = g.Bands[bandNo].AllowUpdate;
	return res;
}

function igtbl_getAllowColSizing(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	var res = g.AllowColSizing;
	if (g.Bands[bandNo].AllowColSizing != 0)
		res = g.Bands[bandNo].AllowColSizing;
	if (g.Bands[bandNo].Columns[columnNo].AllowColResizing != 0)
		res = g.Bands[bandNo].Columns[columnNo].AllowColResizing;
	return res;
}

function igtbl_getRowSizing(gn, bandNo, row)
{
	var g = igtbl_getGridById(gn);
	var res = g.RowSizing;
	if (g.Bands[bandNo].RowSizing != 0)
		res = g.Bands[bandNo].RowSizing;
	if (row.getAttribute("sizing"))
		res = parseInt(row.getAttribute("sizing"), 10);
	return res;
}

function igtbl_getRowSelectors(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getRowSelectors();
}

function igtbl_getNullText(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	if (g.Bands[bandNo].Columns[columnNo].NullText != "")
		return g.Bands[bandNo].Columns[columnNo].NullText;
	if (g.Bands[bandNo].NullText != "")
		return g.Bands[bandNo].NullText;
	return g.NullText;
}

function igtbl_getEditCellClass(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	if (g.Bands[bandNo].EditCellClass != "")
		return g.Bands[bandNo].EditCellClass;
	return g.EditCellClass;
}

function igtbl_getFooterClass(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getFooterClass();
}

function igtbl_getGroupByRowClass(gn, bandNo)
{
	return g.Bands[bandNo].getGroupByRowClass();
}

function igtbl_getHeadClass(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].Columns[columnNo].getHeadClass();
}

function igtbl_getRowLabelClass(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getRowLabelClass();
}

function igtbl_getSelGroupByRowClass(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getSelGroupByRowClass();
}

function igtbl_getSelHeadClass(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	if (g.Bands[bandNo].Columns[columnNo].SelHeadClass != "")
		return g.Bands[bandNo].Columns[columnNo].SelHeadClass;
	if (g.Bands[bandNo].SelHeadClass != "")
		return g.Bands[bandNo].SelHeadClass;
	return g.SelHeadClass;
}

function igtbl_getSelCellClass(gn, bandNo, columnNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].Columns[columnNo].getSelClass();
}

function igtbl_getExpAreaClass(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	return g.Bands[bandNo].getExpAreaClass();
}

function igtbl_getCurrentRowImage(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	var res = g.CurrentRowImage;
	var band = g.Bands[bandNo];
	if (band.CurrentRowImage != "")
		res = band.CurrentRowImage;
	var au = igtbl_getAllowUpdate(gn, band.Index);
	if (band.RowTemplate != "" && (au == 1 || au == 3))
	{
		res = g.CurrentEditRowImage;
		if (band.CurrentEditRowImage != "")
			res = band.CurrentEditRowImage;
	}
	return res;
}

function igtbl_getCurrentRowAltText(gn, bandNo)
{
	var g = igtbl_getGridById(gn);
	var band = g.Bands[bandNo];
	var au = igtbl_getAllowUpdate(gn, bandNo);
	var alt = g._currentRowAltText;
	if (band.RowTemplate != "" && (au == 1 || au == 3))
		alt = g._currentEditRowAltText;
	return alt;
}

function igtbl_getBandFAC(gn, elem)
{
	var gs = igtbl_getGridById(gn);
	var bandNo = null;
	
	if (elem.tagName == "TD" || elem.tagName == "TH")
	{
		if (elem.id != "")
		{ return igtbl_getBandById(elem.id).firstActiveCell; }
		else { elem = elem.parentNode; }
	}
	if (elem.tagName == "TR")
		bandNo = elem.parentNode.parentNode.getAttribute("bandNo");
	if (elem.tagName == "TABLE")
		bandNo = elem.getAttribute("bandNo");
	if (bandNo)
		return gs.Bands[bandNo].firstActiveCell;
	return null;
}
function igtbl_enumColumnCells(gn, column)
{	
	var cellIndex = null; 
	
	
	var colSpan = column.getAttribute("colspan");
	if (colSpan !== null)
		colSpan = igtbl_parseInt(colSpan);
	else
		colSpan = 0;
	
	var colOffs = column.getAttribute("colOffs");
	if (colOffs !== null)
		cellIndex = igtbl_parseInt(colOffs);
	else
	{
		var i = 0;
		while (i < column.parentNode.childNodes.length && cellIndex === null)
		{
			if (column.parentNode.childNodes[i] == column)
				cellIndex = i;
			i++;
		}
	}
	var nonFixed = false;
	i = 0;
	var pn = column.parentNode;
	while (i < 5 && pn && !(pn.tagName == "DIV" && pn.id == gn + "_drs"))
	{
		pn = pn.parentNode;
		nonFixed = pn && pn.tagName == "DIV" && pn.id == gn + "_drs";
		i++;
	}
	var ar = new Array();
	var colIdA = column.id.split("_");
	var fac = igtbl_getBandFAC(gn, column);
	var thead = column.parentNode;
	
	while (thead && (thead.tagName != "THEAD" || (thead.parentNode && thead.parentNode.parentNode && thead.parentNode.parentNode.id == gn + "_drs")))
		thead = thead.parentNode;
	if (thead)
		for (var i = 1; i < thead.parentNode.rows.length; i++)
	{
		var row = thead.parentNode.rows[i];
		if (!row.getAttribute("hiddenRow") && row.parentNode.tagName != "TFOOT")
		{
			var visElem = null;
			if (!nonFixed)
				visElem = row.cells[cellIndex];
			else
				for (var j = fac; j < row.cells.length && !visElem; j++)
			{
				var cell = row.cells[j];
				if (cell.firstChild && cell.firstChild.id == gn + "_drs")
				{

					row = cell.firstChild.firstChild.rows[0];
					visElem = row.cells[cellIndex];
				}
			}

			
			
			if (colSpan > 1 && visElem && visElem.id == "")
				var cell = igtbl_getCellByElement(visElem);
			if (visElem && visElem.id || !visElem)
			{
				var visCol = null;
				if (visElem)
					visCol = igtbl_getColumnById(visElem.id);
				if (visElem && (!visCol || visCol.Id != column.id) || !visElem)
				{
					
					visElem = row.cells[0];
					visCol = null;
					if (visElem)
						visCol = igtbl_getColumnById(visElem.id);
					while (visElem && (!visCol || visCol.Id != column.id))
					{
						visElem = visElem.nextSibling;
						if (visElem)
							visCol = igtbl_getColumnById(visElem.id);
					}
				}
			}

			if (visElem)
			{
				ar[ar.length] = visElem;
			}
		}
	}
	return ar;
}


function igtbl_getElemVis(cols, index)
{
	var i = 0, j = -1;
	while (cols && cols[i] && j != index)
	{
		
		if (!ig_csom.IsIE7Compat || cols[i].style.display != "none")
			j++;
		i++;
	}
	return cols[i - 1];
}


function igtbl_hideColHeader(tBody, col, hide, fixedHeaders)
{	
	var realIndex = -1;
	
	var trueRealIndex = -1;
	var tr = tBody.childNodes[0];
	for (var i = 0; i < tr.cells.length; i++)
	{
		var c = tr.cells[i];
		if (c.colSpan > 1 && c.firstChild.tagName == "DIV" && c.firstChild.id.substr(c.firstChild.id.length - 4) == "_drs")
		{
			tr = c.firstChild.firstChild.childNodes[1].rows[0];
			i = 0;
			c = tr.cells[i];
			
			trueRealIndex = -1;
		}
		if (c.style.display == "")
		{
			realIndex++;
			trueRealIndex++;
		}
		if (col.Id && c.id == col.Id || col.fId && c.id == col.fId)
		{
			var h = (hide ? "none" : "");
			if (c.style.display == h)
				return;
			c.style.display = h;
			
			
			
			var headerColGroup = null;
			var stationaryMarginsUsed = false; 
			if (fixedHeaders)
			{
				
				if (col.getFixed())
				{
					
					headerColGroup = tBody.previousSibling.childNodes;
				}
				
				else if (tr && tr.parentNode && tr.parentNode.previousSibling && tr.parentNode.previousSibling.tagName == "COLGROUP")
				{
					headerColGroup = tr.parentNode.previousSibling.childNodes;
					stationaryMarginsUsed = true;
				}
				else if(tBody.nextSibling && tBody.nextSibling.nextSibling)
				{
					
					
					var childNodes = tBody.nextSibling.nextSibling.childNodes[0].childNodes;
					var j = 0;
					while (j < childNodes.length)
					{
						var col = childNodes[j];
						j++;
						if (col.colSpan > 1 && col.firstChild.tagName == "DIV" && col.firstChild.id.substr(col.firstChild.id.length - 4) == "_drs")
						{
							headerColGroup = col.childNodes[0].childNodes[0].childNodes[0].childNodes;
							break;
						}
					}
				}
			}
			else if (tBody.nextSibling && tBody.nextSibling.nextSibling)
			{
				headerColGroup = tBody.nextSibling.nextSibling.childNodes[0].childNodes;
			}

						
			if (headerColGroup && !stationaryMarginsUsed)
			{
				headerColGroup[i].style.display = h;
			}
			
			
			var chn = tBody.previousSibling;
			while (chn.tagName != "COLGROUP")
				chn = chn.previousSibling;
			chn = chn.childNodes;
			
			if (hide)
			{
				
				if (!ig_csom.IsIE9)
				{
					var ch = chn[realIndex];
					col.Width = ch.width;
					ch.parentNode.appendChild(ch);
					ch.width = "1px";
					ch.style.display = "none";
				}
				
				
				if (headerColGroup)
				{
								
					if (stationaryMarginsUsed)
					{
						var sch = headerColGroup[trueRealIndex];
						col.Width = sch.width;
						sch.parentNode.appendChild(sch);
						sch.width = "1px";
						sch.style.display = "none";												
					}
					else
						headerColGroup[headerColGroup.length - 1].width = col.Width;
				}
			}
			else
			{
				
				
				var ch = chn[chn.length - 1];
				ch.style.cssText = col.Style;
				ch.width = col.Width + "px";
				if (chn[realIndex + 1] && !ig_csom.IsIE9)
					ch.parentNode.insertBefore(ch, chn[realIndex + 1])
				if (ch.style.display == "none")
					ch.style.display = "";				

				if (headerColGroup)
				{
								
					if (stationaryMarginsUsed)
					{
						var sch = headerColGroup[headerColGroup.length - 1];
						sch.style.cssText = col.Style;
						sch.width = col.Width + "px";
						if (headerColGroup[trueRealIndex + 1])
							sch.parentNode.insertBefore(sch, headerColGroup[trueRealIndex + 1])
						if (sch.style.display == "none")
							sch.style.display = "";
					}
					else
						headerColGroup[i].width = col.Width;
				}
				
					
				
				
			}
			break;
		}
	}
	
	return trueRealIndex;
}

function igtbl_hideColumn(rows, col, hide)
{	
	
	if (col && col.Hidden == hide)
		return;
	
	var g = col.Band.Grid;
	var ao = g.Activation;
	var realIndex = -1; 
	igtbl_lineupHeaders(col.Id, col.Band);
	if (col.Band.Index == rows.Band.Index)
	{
		if (col.Band.Index == 0)
		{			
			if (g.StatHeader)
			{
				var el = g.StatHeader.getElementByColumn(col);
				
				realIndex = igtbl_hideColHeader(g.StatHeader.Element, col, hide, g.UseFixedHeaders);
			}
			if (g.StatFooter)
			{
				var el = g.StatFooter.getElementByColumn(col);
				
				realIndex = igtbl_hideColHeader(g.StatFooter.Element, col, hide, g.UseFixedHeaders);
			}

			if (g.StatHeader || g.StatFooter)
			{
				
				var filterRow = rows.FilterRow;
				if (filterRow)
				{
					var filterCell = filterRow.getCellByColumn(col);
					if(g.UseFixedHeaders)
					{
						var fCols = filterCell.Element.parentNode.parentNode.previousSibling;
						if (hide)
						{
							
							var fCol = fCols.childNodes[realIndex];
							fCol.width = "1px";
							fCol.style.display = "none";
							fCol.parentNode.appendChild(fCol);
							filterCell.Element.style.display = "none";
						}
						else
						{
							filterCell.Element.style.display = "";
							var fCol = fCols.childNodes[fCols.childNodes.length - 1];
							fCol.width = col.Width + "px";
							fCol.style.display = "";
							if (filterCell.Element.cellIndex < fCols.childNodes.length - 1)
								fCols.insertBefore(fCol, fCols.childNodes[filterCell.Element.cellIndex]);
						}
					}
					else
						filterCell.Element.style.display = (hide ? "none" : "");
				}
			}
		}

		var tBody = rows.Element.previousSibling;
		if (tBody)
		{
			
			
			realIndex = igtbl_hideColHeader(tBody, col, hide, g.UseFixedHeaders);
		}
		
		
		if (g.UseFixedHeaders && !g.StatFooter && rows.Element.nextSibling &&
			rows.Element.nextSibling.tagName == "TFOOT")
		{			
			igtbl_hideColHeader(rows.Element.nextSibling, col, hide, g.UseFixedHeaders);
		}		
	}
	for (var i = 0; i < rows.length; i++)
	{
		var row = rows.getRow(i);
		if (col.Band.Index == rows.Band.Index && !row.GroupByRow)
		{
			var cell = row.getCellByColumn(col);
			if (hide)
			{
				
				
				if (cell.Element == null) { }
				else
				{
					var cellElm = cell.Element;
					cellElm.style.display = "none";					

					
					if (g.UseFixedHeaders && !col.getFixed() && cellElm.parentNode && cellElm.parentNode.parentNode &&
						cellElm.parentNode.parentNode.previousSibling && cellElm.parentNode.parentNode.previousSibling.tagName == "COLGROUP")
					{
						var headerColGroup = cellElm.parentNode.parentNode.previousSibling.childNodes;
						var sch = headerColGroup[realIndex];
						col.Width = sch.width;
						sch.parentNode.appendChild(sch);
						sch.width = "1px";
						sch.style.display = "none";
					}
				}
				if (col.Band.Grid.getActiveRow() == row)
				{
					if (igtbl_hasClassName(cell.Element, ao._cssClassL))
					{
						igtbl_removeClassName(cell.Element, ao._cssClassL);
						for (var j = col.Index + 1; j < col.Band.Columns.length; j++)
							if (col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
						{
							igtbl_setClassName(row.getCellByColumn(col.Band.Columns[j]).Element, ao._cssClassL);
							break;
						}
					}
					if (igtbl_hasClassName(cell.Element, ao._cssClassR))
					{
						igtbl_removeClassName(cell.Element, ao._cssClassR);
						for (var j = col.Index - 1; j >= 0; j--)
							if (col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
						{
							igtbl_setClassName(row.getCellByColumn(col.Band.Columns[j]).Element, ao._cssClassR);
							break;
						}
					}
				}
			}
			else
			{
				
				
				if (cell.Element == null) { }
				else
				{					
					var cellElm = cell.Element;					
					
					
					if (g.UseFixedHeaders && !col.getFixed() && cellElm.parentNode && cellElm.parentNode.parentNode &&
						cellElm.parentNode.parentNode.previousSibling && cellElm.parentNode.parentNode.previousSibling.tagName == "COLGROUP")
					{
						var headerColGroup = cellElm.parentNode.parentNode.previousSibling.childNodes;
						var sch = headerColGroup[headerColGroup.length - 1];
						sch.style.cssText = col.Style;
						sch.width = col.getWidth() + "px";
						if (headerColGroup[realIndex + 1])
							sch.parentNode.insertBefore(sch, headerColGroup[realIndex + 1])
						if (sch.style.display == "none")
							sch.style.display = "";								
					}
					cellElm.style.display = "";
				}
				if (col.Band.Grid.getActiveRow() == row)
				{
					var j = 0;
					for (j = 0; j < col.Band.Columns.length; j++)
						if (col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
						break;
					if (j > col.Index)
					{
						igtbl_removeClassName(row.getCellByColumn(col.Band.Columns[j]).Element, ao._cssClassL);
						igtbl_setClassName(cell.Element, ao._cssClassL);
					}
					for (j = col.Band.Columns.length - 1; j >= 0; j--)
						if (col.Band.Columns[j].getVisible() && col.Band.Columns[j].hasCells())
						break;
					if (j < col.Index)
					{
						igtbl_removeClassName(row.getCellByColumn(col.Band.Columns[j]).Element, ao._cssClassR);
						igtbl_setClassName(cell.Element, ao._cssClassR);
					}
				}
			}
		}
		else if (col.Band.Index >= rows.Band.Index && row.Expandable)
		{
			if ((row.GroupByRow || col.Band.Index > rows.Band.Index) && row.Rows)
				igtbl_hideColumn(row.Rows, col, hide);
		}
	}
	
	//if (ig_shared.IsFireFox || ig_shared.IsIE7)/*AK 6/11/2007 BR23503: Columns does not get visible again from client side after hiding them in FireFox*/
	
	var oldHiddenValue = col.Hidden;
	col.Hidden = hide;
	igtbl_lineupHeaders(col.Id, col.Band);
	col.Hidden = oldHiddenValue;

	
	if (g.UseFixedHeaders && col.Band.Index == 0)
	{						
		var colWidthAdjustment = (hide) ? col.getWidth() * -1 : col.getWidth();
		var scrw = g._scrElem.firstChild.offsetWidth + colWidthAdjustment;
		if (scrw >= 0)
		{			
			g._scrElem.firstChild.style.width = scrw + "px";
		}
		g.alignStatMargins();
		g.alignDivs(0, true);
	}
}

function igtbl_isColEqual(col1, col2)
{
	if (col1 == null && col2 == null)
		return true;
	if (col1 == null || col2 == null)
		return false;
	if (col1.Band.Index == col2.Band.Index && col1.Key == col1.Key && col1.Index == col2.Index)
		return true;
	return false;
}


function igtbl_assignColumnElements(ce, band)
{
	if (ce)
	{
		if (typeof (ce.getAttribute) != "undefined" && ce.getAttribute("columnNo"))
		{
			var colNo = igtbl_parseInt(ce.getAttribute("columnNo"));
			band.Columns[colNo].Element = ce;
		}
		if (ce.childNodes)
			for (var i = 0; i < ce.childNodes.length; i++)
			igtbl_assignColumnElements(ce.childNodes[i], band);
	}
}


function _igtbl_sortNumber(a, b)
{
	return a[0] - b[0];
}




function _igtbl_createXmlElement(doc, tagName, ns)
{
	return igtbl_xml.createXmlElement(doc, tagName, ns);
}

function _igtbl_createXmlTextNode(doc, ns)
{
	return igtbl_xml.createXmlTextNode(doc, ns);
}

function igtbl_rowGetValue(colId)
{
	
}

if (ig_csom.IsIE6)
{
	var igtbl_mouseDownX;
	var igtbl_mouseDownY;
}

function igtbl_headerClickDown(evnt, gn)
{	
	if (!evnt && event)
		evnt = event;
	if (!gn && igtbl_lastActiveGrid)
		gn = igtbl_lastActiveGrid;
	if (!gn || !evnt)
		return false;
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return;
	gs.event = evnt;
	igtbl_lastActiveGrid = gn;
	var te = gs.Element;
	
	te.setAttribute("mouseDown", evnt.button);
	
	var se = igtbl_srcElement(evnt);
	if (se && se.tagName == "IMG" && (se.getAttribute("imgType") == "group" || se.getAttribute("imgType") == "fixed"))
		return;
	while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn) && (se.tagName != "DIV" || !se.getAttribute("groupInfo")))
		se = se.parentNode;
	if (!se)
		return;
	if (se.tagName == "TH" && se.parentNode.parentNode.tagName != "TFOOT")
	{
		var colObj = igtbl_getColumnById(se.id);
		if (!colObj) return;		
		else
		{
			
			if (ig_csom.IsIE && gs._isComboGrid)
			{
				var comboId = igtbl_getElementById(gn).getAttribute("igComboId");
				if (comboId)
				{
					var oCombo = igcmbo_getComboById(comboId);
					oCombo.Element.setAttribute("noOnBlur", true);
					window.setTimeout("igcmbo_cancelNoOnBlur('" + oCombo.Id + "')", 100);
				}
			}
		}
		if (igtbl_fireEvent(gn, gs.Events.MouseDown, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")") == true)
			return true;
		if (igtbl_button(gn, evnt) != 0)
			return;

		if (igtbl_inEditMode(gn))
			gs.endEdit();
		if (igtbl_inEditMode(gn))
			return true;

		var bandNo = colObj.Band.Index;
		var band = colObj.Band;
		
		if (igtbl_getOffsetX_header(evnt, se, bandNo) > igtbl_clientWidth(se) - 4 && igtbl_getAllowColSizing(gn, bandNo, colObj.Index) == 2)
		{
			
			te.setAttribute("elementMode", "resize");
			te.setAttribute("resizeColumn", se.id);
			igtbl_lineupHeaders(se.id, band);
			var div, divr;
			if (!document.body.igtbl_resizeDiv)
			{
				div = document.createElement("DIV");
				div.style.zIndex = gs._getZ(10000, 1);
				div.style.position = "absolute";
				div.style.left = "0px"; 
				div.style.top = "0px";
				div.style.width = "0px";
				div.style.height = "0px";
				document.body.insertBefore(div, document.body.firstChild);
				igtbl_addEventListener(div, "mouseup", igtbl_resizeDivMouseUp, false);
				igtbl_addEventListener(div, "mousemove", igtbl_resizeDivMouseMove, false);
				igtbl_addEventListener(div, "selectstart", igtbl_resizeDivSelectStart, false);
				document.body.igtbl_resizeDiv = div;
				divr = document.createElement("DIV");
				div.appendChild(divr);
				divr.style.position = "absolute";
				if (igtbl_isXHTML || ig_csom.IsNetscape6)
				{
					divr.style.borderLeftWidth = "1px";
					divr.style.borderLeftColor = "black";
					divr.style.borderLeftStyle = "solid";
					divr.style.width = "1px";
				}
				else
				{
					divr.style.borderWidth = "1px";
					divr.style.borderColor = "black";
					divr.style.borderStyle = "solid";
					divr.style.width = "2px";
				}
			}
			else
			{
				div = document.body.igtbl_resizeDiv;
				divr = div.firstChild;
			}
			div.setAttribute("gn", gn);
			div.style.display = "";
			div.style.cursor = "w-resize";
			var divw = document.body.clientWidth, divh = document.body.clientHeight
			div.style.width = divw + "px";
			div.style.height = divh + "px";
			div.style.backgroundColor = "transparent";
			divr.style.top = igtbl_getAbsolutePos2("Top", te.parentNode, false) + "px"; 
			divr.style.left = evnt.clientX
			+ igtbl_getBodyScrollLeft()
			+ "px";
			divr.style.height = te.parentNode.offsetHeight + "px";
			div.column = colObj;
			div.srcElement = se;
			div.initX = evnt.clientX;
			return true;
		}
		se.setAttribute("justClicked", true);
		
		if (ig_csom.IsIE6)
		{
			igtbl_mouseDownX = evnt.x;
			igtbl_mouseDownY = evnt.y;
		}
		if (igtbl_getHeaderClickAction(gn, bandNo, colObj.Index) == 1 && (gs.SelectedColumns[se.id] != true || gs.ViewType != 2 || igtbl_getSelectTypeColumn(gn, bandNo) == 3))
		{
			if (igtbl_getSelectTypeColumn(gn, bandNo) < 2)
				return true;
			te.setAttribute("elementMode", "select");
			te.setAttribute("selectMethod", "column");
			if (!(igtbl_getSelectTypeColumn(gn, bandNo) == 3 && evnt.ctrlKey))
				igtbl_clearSelectionAll(gn);
			if (te.getAttribute("shiftSelect") && evnt.shiftKey)
			{
				te.setAttribute("lastSelectedColumn", "");
				igtbl_selectColumnRegion(gn, se);
				te.removeAttribute("shiftSelect");
			}
			else
			{
				te.setAttribute("startColumn", se.id);
				if (gs.SelectedColumns[se.id] && evnt.ctrlKey)
					igtbl_selectColumn(gn, se.id, false);
				else
					igtbl_selectColumn(gn, se.id);
				te.removeAttribute("shiftSelect");
				if (!evnt.ctrlKey)
					te.setAttribute("shiftSelect", true);
			}
		}
		
		ig_cancelEvent(evnt);
		return true;
	}
	else if (se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		if (igtbl_button(gn, evnt) != 0)
			return;
		if (igtbl_fireEvent(gn, gs.Events.MouseDown, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")") == true)
			return;
		var groupInfo = se.getAttribute("groupInfo").split(":");
		if (groupInfo[0] != "band")
			igtbl_changeStyle(gn, se, igtbl_getSelHeadClass(gn, groupInfo[1], groupInfo[2]));
		se.setAttribute("justClicked", true);
		return true;
	}
}

function igtbl_resizeDivMouseUp(evnt)
{
	if (!evnt) evnt = event;
	if (!evnt) return;
	var se = document.body.igtbl_resizeDiv;
	
	if (!se) return;
	var gn = se.getAttribute("gn");
	var g = igtbl_getGridById(gn);
	
	if (g && g.Element.getAttribute("mouseDown"))
		g.Element.removeAttribute("mouseDown");
	se.style.display = "none";
	if (se.initX != evnt.clientX)
	{
		var col = se.column;
		
		if (!col || !col.Width)
			return;
		var oldWidth = -1;
		if (col.Width.length && col.Width.charAt(col.Width.length - 1) == "%")
		{
			oldWidth = se.srcElement.offsetWidth;
		}
		else if (col.Element && col.Element.colSpan > 1)
		{
			
			var colTags = col._getColTags();
			if (colTags.length == 4)
			{
				oldWidth = igtbl_getAbsBounds(col.Element).w - colTags[1].width;
			}
		}
		if (oldWidth == -1)
		{
			oldWidth = parseInt(col.Width, 10);
		}
		var newWidth = oldWidth + evnt.clientX - se.initX;
		if (newWidth <= 0)
			newWidth = 1;
		if (oldWidth != newWidth)
			col.setWidth(newWidth);
	}
}

function igtbl_resizeDivMouseMove(evnt)
{
	if (!evnt)
		evnt = event;
	if (!evnt)
		return;
	var se = document.body.igtbl_resizeDiv;
	if (!se)
		return;
	var gn = se.getAttribute("gn");
	var g = igtbl_getGridById(gn);
	var te = null;
	if (g) te = g.Element;
	
	
	if (igtbl_button(null, evnt) > 0 || !te || !te.getAttribute("mouseDown"))
	{		
		
		if (ig_csom.IsFireFox30 && se.style.display == "none")
			return;
		return igtbl_resizeDivMouseUp(evnt);
	}
	se.style.cursor = "w-resize";
	if (!se.firstChild)
		se = se.parentNode;
	if (se.initX != evnt.clientX)
	{		
		var col = se.column;
		if (parseInt(col.Width, 10) + evnt.clientX - se.initX > 0)
			se.firstChild.style.left = evnt.clientX + igtbl_getBodyScrollLeft() + "px";
	}
}

function igtbl_resizeDivSelectStart(evnt)
{
	if (!evnt) evnt = event;
	if (!evnt) return;
	return igtbl_cancelEvent(evnt);
}

function igtbl_headerClickUp(evnt, gn)
{
	if (!evnt && event)
		evnt = event;
	if (!gn && igtbl_lastActiveGrid)
		gn = igtbl_lastActiveGrid;
	if (!gn || !evnt)
		return false;
	var gs = igtbl_getGridById(gn);
	
	
	if (!gs || !gs.Events || !gs.isDisabled || gs.isDisabled())
		return;
	gs.event = evnt;
	if (igtbl_button(gn, evnt) == 2)
		return;

	if (igtbl_inEditMode(gn))
		gs.endEdit();
	if (igtbl_inEditMode(gn))
		return true;

	var te = gs.Element;
	
	if (te.getAttribute("mouseDown"))
		te.removeAttribute("mouseDown");
	else
		return;
	var se = igtbl_srcElement(evnt);
	
	if (igtbl_isTemplatedElement(se))
		return;
	if (se && se.tagName == "IMG")
	{
		var imgType = se.getAttribute("imgType");
		if (imgType == "group" || imgType == "fixed" || imgType == "filter")
			return;
	}
	while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn) && (se.tagName != "DIV" || !se.getAttribute("groupInfo")))
		se = se.parentNode;
	if (!se)
		return;
	
	var seTemp = se;
	while (seTemp != null)
	{
		if (seTemp.tagName == "TFOOT")
		{
			return;
		}
		seTemp = seTemp.parentNode
	}
	seTemp = null;
	if (se.tagName == "TH")
	{
		var column = igtbl_getColumnById(se.id);
		if (!column) return;
		var bandNo = column.Band.Index;
		var columnNo = column.Index;
		var mode = te.getAttribute("elementMode");
		
		var headerClickNeedPost = false;
		if (mode != "resize")
		{
			var oldNP = gs.NeedPostBack;
			igtbl_fireEvent(gn, gs.Events.ColumnHeaderClick, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")");
			if (gs.NeedPostBack && gs.NeedPostBack != oldNP)
				headerClickNeedPost = true;
		}
		if (igtbl_fireEvent(gn, gs.Events.MouseUp, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")") == true)
			return true;
		
		var headerClickAction = igtbl_getHeaderClickAction(gn, bandNo, columnNo);
		if (headerClickAction != 1)
		
			igtbl_changeStyle(gn, se, null);
		te.removeAttribute("elementMode");
		te.removeAttribute("resizeColumn");
		te.removeAttribute("selectMethod");
		if (!te.getAttribute("shiftSelect"))
			te.removeAttribute("startColumn");
		
		
		if (mode != "resize" && (headerClickAction == 2 || headerClickAction == 3) && column.SortIndicator != 3)
		{
			if (gs.Bands[bandNo].ClientSortEnabled)
			{
				gs._displayPI();
				gs.startHourGlass();
				gs.sortingColumn = se;
				gs.oldColCursor = se.style.cursor;
				
				window.setTimeout("igtbl_gridSortColumn('" + gn + "','" + se.id + "'," + evnt.shiftKey + ")", 1);
			}
			else
				gs.sortColumn(se.id, evnt.shiftKey);
			if (gs.NeedPostBack && !headerClickNeedPost)
				igtbl_doPostBack(gn, evnt.shiftKey ? "shiftKey:true" : "");
		}
		else
		{
			if (mode == "resize")
				igtbl_resizeDivMouseUp(evnt);
			if ((mode == "resize" || mode == "select") && gs.NeedPostBack)
			{
				
				igtbl_doPostBack(gn, 'HeaderClick:' + se.id);
				
			}
			te.removeAttribute("elementMode");
		}
	}
	else if (se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		igtbl_fireEvent(gn, gs.Events.ColumnHeaderClick, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")");
		if (igtbl_fireEvent(gn, gs.Events.MouseUp, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")") == true)
			return;
		var groupInfo = se.getAttribute("groupInfo").split(":");
		if (groupInfo[0] != "band")
		{
			igtbl_changeStyle(gn, se, null);
			var bandNo = igtbl_bandNoFromColId(se.id);
			var columnNo = igtbl_colNoFromColId(se.id);
			var column = gs.Bands[bandNo].Columns[columnNo];
			
			var headerClickAction = igtbl_getHeaderClickAction(gn, bandNo, columnNo);
			if ((headerClickAction == 2 || headerClickAction == 3) && column.SortIndicator != 3)
			
			{
				if (gs.Bands[bandNo].ClientSortEnabled)
				{
					gs._displayPI();
					gs.startHourGlass();
					gs.sortingColumn = se;
					gs.oldColCursor = se.style.cursor;
					
					window.setTimeout("igtbl_gridSortColumn('" + gn + "','" + se.id + "',true)", 1);
				}
				else
					gs.sortColumn(se.id, evnt.shiftKey);
				if (gs.NeedPostBack)
					igtbl_doPostBack(gn, evnt.shiftKey ? "shiftKey:true" : "");
			}
		}
	}
	if (gs.NeedPostBack)
		igtbl_doPostBack(gn, 'HeaderClick:' + se.id);
	return true;
}

function igtbl_headerContextMenu(evnt, gn)
{
	if (!evnt && event)
		evnt = event;
	if (!gn && igtbl_lastActiveGrid)
		gn = igtbl_lastActiveGrid;
	if (!gn || !evnt)
		return false;
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return;
	gs.event = evnt;
	
	if (igtbl_button(gn, evnt) == 2 && !ig_csom.IsFireFox)
		return;
	var te = gs.Element;
	te.removeAttribute("mouseDown");
	var se = igtbl_srcElement(evnt);
	while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn) && (se.tagName != "DIV" || !se.getAttribute("groupInfo")))
		se = se.parentNode;
	if (!se)
		return;
	if (se.tagName == "TH" || se.tagName == "DIV")
	{
		var column = igtbl_getColumnById(se.id);
		if (se.tagName == "TH" && !column) return;
		igtbl_fireEvent(gn, gs.Events.ColumnHeaderClick, "(\"" + gn + "\",\"" + se.id + "\",2)");
		if (igtbl_fireEvent(gn, gs.Events.MouseUp, "(\"" + gn + "\",\"" + se.id + "\",2)") == true)
			return igtbl_cancelEvent(evnt);
	}
}

function igtbl_headerMouseOut(evnt, gn)
{
	if (!evnt && event)
		evnt = event;
	if (!gn && igtbl_lastActiveGrid)
		gn = igtbl_lastActiveGrid;
	if (!gn || !evnt)
		return false;
	var gs = igtbl_getGridById(gn);
	var se = igtbl_srcElement(evnt);
	if (!gs || !se || gs.isDisabled())
		return;
	gs.event = evnt;
	if (se.tagName == "NOBR" && se.title)
	{
		se.title = "";
		
		if (se.removeAttribute)
		{
			se.removeAttribute("title");
		}
	}
	while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn) && (se.tagName != "DIV" || !se.getAttribute("groupInfo")))
		se = se.parentNode;
	if (!se)
		return;
	if (se.tagName == "TH")
	{
		var column = igtbl_getColumnById(se.id);
		if (!column) return;
		var sep = se.parentNode;
		if (gs.Element.getAttribute("elementMode") == "select")
			return true;
		if (!igtbl_isMouseOut(se, evnt)) return true;
		if (igtbl_fireEvent(gn, gs.Events.MouseOut, "(\"" + gn + "\",\"" + se.id + "\",1)") == true)
			return true;
		if (igtbl_getHeaderClickAction(gn, column.Band.Index, column.Index) != 1)
			igtbl_changeStyle(gn, se, null);
		return true;
	}
	else if (se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		if (!igtbl_isMouseOut(se, evnt)) return true;
		if (igtbl_fireEvent(gn, gs.Events.MouseOut, "(\"" + gn + "\",\"" + se.id + "\",1)") == true)
			return true;
		var groupInfo = se.getAttribute("groupInfo").split(":");
		if (groupInfo[0] != "band")
			igtbl_changeStyle(gn, se, null);
		return true;
	}
}

function igtbl_isMouseOut(se, evnt)
{
	var te = evnt.toElement;
	if (te == null)
		te = evnt.relatedTarget;
	while (te != null)
	{
		if (te == se)
			return false;
		try
		{
			te = te.parentNode;
		}
		catch (exc)
		{
			break;
		}
	}
	se._hasMouse = false;
	return true;
}

function igtbl_headerMouseOver(evnt, gn)
{
	if (!evnt && event)
		evnt = event;
	if (!evnt)
		return false;
	var se = igtbl_srcElement(evnt);
	if (!se)
		return;
	var column;
	if (se.tagName == "NOBR")
	{
		column = igtbl_getColumnById(se.parentNode.id);
		if (column)
		{
			var nobr = se;
			var showTitle = nobr.offsetWidth > se.parentNode.offsetWidth || nobr.offsetHeight > se.parentNode.offsetHeight;
			var titleMode = column.getTitleModeResolved();
			showTitle |= titleMode == igtbl_CellTitleMode.Always;
			showTitle &= titleMode != igtbl_CellTitleMode.Never;
			if (showTitle)
			{
				nobr.title = column.HeaderText;
			}
		}
	}
	else
		column = igtbl_getColumnById(se.id);
	if (!column) return;
	var gs = column.Band.Grid;
	if (!gn)
		gn = gs.Id;
	gs.event = evnt;

	
	
	if (!igtbl_lastActiveGrid || igtbl_lastActiveGrid.length < 0 || !igtbl_getGridById(igtbl_lastActiveGrid) || !igtbl_inEditMode(igtbl_lastActiveGrid))
		igtbl_lastActiveGrid = gn;
	while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn) && (se.tagName != "DIV" || !se.getAttribute("groupInfo")))
		se = se.parentNode;
	if (!se)
		return;
	if (se.tagName != "DIV")
	{
		while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn))
			se = se.parentNode;
		if (!se)
			return;
	}
	if (se._hasMouse) return;
	if (se.tagName == "TH")
	{
		var column = igtbl_getColumnById(se.id);
		if (!column) return;
		se._hasMouse = true;
		igtbl_fireEvent(gn, gs.Events.MouseOver, "(\"" + gn + "\",\"" + se.id + "\",1)");
	}
	else if (se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		se._hasMouse = true;
		igtbl_fireEvent(gn, gs.Events.MouseOver, "(\"" + gn + "\",\"" + se.id + "\",1)");
	}
}

function igtbl_headerMouseMove(evnt, gn)
{
	if (!evnt && event)
		evnt = event;
	if (!gn && igtbl_lastActiveGrid)
		gn = igtbl_lastActiveGrid;
	if (!gn || !evnt)
		return false;
	var gs = igtbl_getGridById(gn);
	var se = igtbl_srcElement(evnt);
	if (!gs || !se || gs.isDisabled())
		return false;
	gs.event = evnt;
	while (se && (se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn) && (se.tagName != "DIV" || !se.getAttribute("groupInfo")))
		se = se.parentNode;
	if (!se)
		return;
	if (se.tagName == "TH")
	{		
		var column = igtbl_getColumnById(se.id);
		if (!column) return;
		var bandNo = column.Band.Index;
		var columnNo = column.Index;
		
		
		if ((!ig_shared.IsIE9Plus && igtbl_button(gn, evnt) == 0) || gs.Element.getAttribute("mouseDown"))
		{
			
			if (ig_shared.IsIE6 && igtbl_mouseDownX == evnt.x && igtbl_mouseDownY == evnt.y)
				return;
			var mode = gs.Element.getAttribute("elementMode");
			if (mode != null && mode == "resize")
				igtbl_resizeDivMouseMove(evnt);
			else if (mode == "select" && igtbl_getHeaderClickAction(gn, bandNo, columnNo) == 1 && !evnt.ctrlKey)
				igtbl_selectColumnRegion(gn, se);
			else
			{
				var cursorName = se.getAttribute("oldCursor");
				if (cursorName != null)
				{
					se.style.cursor = cursorName;
					se.removeAttribute("oldCursor");
				}
				if (igtbl_getHeaderClickAction(gn, bandNo, columnNo) != 1 || gs.SelectedColumns[se.id] || igtbl_getSelectTypeColumn(gn, bandNo) < 2)
					if (column.AllowGroupBy == 1 && gs.ViewType == 2 && gs.GroupByBox.Element || column.Band.AllowColumnMoving > 1)
				{
					if (se.getAttribute("justClicked"))
					{
						if (typeof (igtbl_headerDragStart) != "undefined")
							igtbl_headerDragStart(gn, se, evnt);
					}
					else
						igtbl_changeStyle(gn, se, null);
				}
			}
			if (se.getAttribute("justClicked"))
				se.removeAttribute("justClicked");
			if ((column.TemplatedColumn & 1) && se != igtbl_srcElement(evnt))
				return;
			igtbl_cancelEvent(evnt);
			return true;
		}
		else
		{
			var c, te = gs.Element;
			te.removeAttribute("elementMode");
			te.removeAttribute("resizeColumn");
			te.removeAttribute("selectMethod");
			if (!te.getAttribute("shiftSelect"))
				te.removeAttribute("startColumn");
				
			if (igtbl_getOffsetX_header(evnt, se) > igtbl_clientWidth(se) - 4 && igtbl_getAllowColSizing(gn, bandNo, columnNo) == 2)
			{
				if (se.getAttribute("oldCursor") == null)
					se.setAttribute("oldCursor", se.style.cursor);
				se.style.cursor = "w-resize";
				if ((c = se.firstChild) != null) if ((c = c.firstChild) != null) if ((c = c.style) != null) c.cursor = "w-resize";
			}
			else
			{
				var cursorName = se.getAttribute("oldCursor");
				if (cursorName != null)
				{
					se.style.cursor = cursorName;
					se.removeAttribute("oldCursor");
					if ((c = se.firstChild) != null) if ((c = c.firstChild) != null) if ((c = c.style) != null) c.cursor = cursorName;
				}
			}
		}
		if (se.getAttribute("justClicked"))
			se.removeAttribute("justClicked");
		if ((column.TemplatedColumn & 1) && se != igtbl_srcElement(evnt))
			return;
	}
	else if (se.tagName == "DIV" && se.getAttribute("groupInfo"))
	{
		var groupInfo = se.getAttribute("groupInfo").split(":");
		if (groupInfo[0] != "band")
		{
			
			if (igtbl_button(gn, evnt) == 0 || gs.Element.getAttribute("mouseDown"))
			{
				var cursorName = se.getAttribute("oldCursor");
				if (cursorName != null)
				{
					se.style.cursor = cursorName;
					se.removeAttribute("oldCursor");
				}
				igtbl_changeStyle(gn, se, null);
				if (gs.ViewType == 2 && se.getAttribute("justClicked") && typeof (igtbl_headerDragStart) != "undefined")
					igtbl_headerDragStart(gn, se, evnt);
			}
		}
		if (se.getAttribute("justClicked"))
			se.removeAttribute("justClicked");
		return true;
	}
	return false;
}

function igtbl_tableMouseMove(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	var se = igtbl_srcElement(evnt);
	if (!gs || !se || gs.isDisabled())
		return false;
	gs.event = evnt;
	var te = gs.Element;
	
	var lastRowSelectorId = (gs.Rows && gs.Rows.legnth) ? gn + "_|_" + gs.Rows.length - 1 : "";
	if (igtbl_button(gn, evnt) == 0 && te.getAttribute("elementMode") == "resize")
	{
		
		if (((se.id == gn + "_div" && lastRowSelectorId != "" && lastRowSelectorId != gs._mouseID) || (se.id == gn + "_hdiv" && lastRowSelectorId != "" && lastRowSelectorId != gs._mouseID) || se.tagName == "TABLE" && se.parentNode.parentNode.getAttribute("hiddenRow")))
		{			
			igtbl_resizeDivMouseMove(evnt);
			
			return igtbl_cancelEvent(evnt);
		}
		
		else if (se.tagName == "TR" && se.getAttribute("hiddenRow") || se.id == gn + "_drs")
		{			
			igtbl_resizeDivMouseMove(evnt);
			return igtbl_cancelEvent(evnt);
		}
	}
	else if (te.parentNode && typeof (te.parentNode.oldCursor) == "string")
	{
		te.parentNode.style.cursor = te.parentNode.oldCursor;
		if (gs.StatHeader)
			gs.StatHeader.Element.parentNode.parentNode.style.cursor = te.parentNode.oldCursor;
		te.parentNode.oldCursor = null;
	}
	if (se == te || se == gs.DivElement || se.tagName == "TH")
		igtbl_colButtonMouseOut(evnt, gn);
}


function igtbl_resizeRowMouseMove(e)
{
	var evnt = igtbl_event.getEvent(e);
	var se = igtbl_srcElement(evnt);
	var gn = igtbl_lastActiveGrid;
	if (!gn) return;
	var gs = igtbl_getGridById(gn);
	var te = gs.Element;
	if (te.getAttribute("resizeRow"))
	{
		if (typeof (te.parentNode.oldCursor) != "string")
		{
			te.parentNode.oldCursor = te.parentNode.style.cursor;
			te.parentNode.style.cursor = "n-resize";
		}
		var rowId = te.getAttribute("resizeRow");
		var row = igtbl_getElementById(rowId);
		if (!row || row.getAttribute("hiddenRow"))
			return;

		
		var scrollTop = (ig_csom.IsIE6 || ig_csom.IsIE7) && igtbl_isXHTML ? gs.DivElement.scrollTop : 0;
		var r1h = row.offsetHeight + (evnt.clientY - ((igtbl_getTopPos(row) - scrollTop) + row.offsetHeight));
		
		igtbl_resizeRow(gn, rowId, r1h);
		return igtbl_cancelEvent(evnt);
	}
}

function igtbl_resizeRowMouseUp(e)
{	
	// need to unhook the event when the mouse is released
	if (!igtbl_lastActiveGrid) return;
	var gs = igtbl_getGridById(igtbl_lastActiveGrid);
	
	
	ig_csom.removeEventListener(document, "mousemove", igtbl_resizeRowMouseMove);
	ig_csom.removeEventListener(document, "mouseup", igtbl_resizeRowMouseUp);	
}


function igtbl_clearResizeDiv(gs, evnt, noForce)
{
	gs.Element.removeAttribute("elementMode");
	gs.Element.removeAttribute("resizeColumn");
	
	var resizeDiv = document.body.igtbl_resizeDiv;
	if (resizeDiv)
	{
		resizeDiv.style.display = "none";
		if (!noForce)
			igtbl_resizeDivMouseUp(evnt);
	}
	gs.Element.removeAttribute("mouseDown");
}

function igtbl_tableMouseUp(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return false;
	
	if (ig_csom.IsFireFox && gs.Element.getAttribute("elementMode") == "resize")
	{
		igtbl_resizeDivMouseUp(evnt)
		return true;
	}
	gs.event = evnt;
	var se = igtbl_srcElement(evnt);
	if (!se) return;
	if (se == gs._editorCurrent) return;
	if (gs.Element.getAttribute("elementMode") == "resize")
	{
		if (se.id == gn + "_div")
		{
			
			igtbl_clearResizeDiv(gs, evnt);
			
			
			
		}
		else if (se.tagName == "TR" && se.getAttribute("hiddenRow") || se.id == gn + "_drs")
			igtbl_resizeDivMouseUp(evnt);
	}
	var ar = gs.getActiveRow();
	if (ar && !igtbl_isAChildOfB(se, ar.Element))
	{
		gs.endEdit();
		if (ar.IsAddNewRow)
			ar.commit();
		else
			if (ar._dataChanged && ar._dataChanged > 1)
			ar.processUpdateRow();
	}
	if((!ig_shared.IsFireFox || !gs._isComboGrid) && (ar || gs.getActiveCell()))
		igtbl_activate(gn);
}

function igtbl_cellClickDown(evnt, gn)
{	
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return;
	gs.event = evnt;
	igtbl_lastActiveGrid = gn;
	gs._mouseDown = 1;
	gs.Element.setAttribute("mouseDown", "1");
	var se = igtbl_srcElement(evnt);
	
	if (!se || se.tagName == "IMG" && se.getAttribute("imgType") == "expand")
		return;
	
	if ('' + se.contentEditable == 'true')
		return;
	
	igtbl_filterMouseUpDocument();
	if (!se || se == gs._editorCurrent) return; 
	
	if ((se.id == gn + "_vl") || (se.parentNode && se.parentNode.id == gn + "_vl")) { if (gs._focusElem) ig_cancelEvent(evnt); return; }
	if (se.id == gn + "_tb" || se.id == gn + "_ta")
		return;
	var sel = igtbl_getElementById(gn + "_vl");
	if (sel && sel.style.display == "" && sel.getAttribute("noOnBlur"))
		return igtbl_cancelEvent(evnt);
	
	var parentCell = igtbl_getParentCell(se);
	
	
	if ((!ig_csom.IsNetscape6 && !ig_csom.IsIE9Plus) || (!ig_csom.IsIE9Plus && !((se.tagName == "INPUT" && se.type == "text" || se.tagName == "TEXTAREA") && parentCell && (parentCell.Column.TemplatedColumn & 2))))
		ig_cancelEvent(evnt);
	var se = igtbl_dom.find.parentByTag(se, ["TD", "TH"]);
	if (!se)
		return;

	
	var row;
	var cell = igtbl_getCellByElement(se);
	var id = gs._mouseID = se.id;
	if (cell)
	{
		row = cell.Row;
		id = cell.Element.id;
	}
	else row = igtbl_getRowById(id);
	if (!row && !cell) return;
	var fac = row.Band.firstActiveCell;
	if (igtbl_fireEvent(gn, gs.Events.MouseDown, "(\"" + gn + "\",\"" + id + "\"," + igtbl_button(gn, evnt) + ")") == true)
	{
		igtbl_cancelEvent(evnt);
		return true;
	}
	var band = row.Band;
	var bandNo = band.Index;
	
	if (igtbl_hideEdit(gn) && !((band.getSelectTypeCell() == 2 || band.getSelectTypeCell() == 3) && band.getCellClickAction() == 1 && cell && !cell.getSelected())) return;
	if (igtbl_button(gn, evnt) == 0 && !cell && igtbl_getOffsetY(evnt, se) > igtbl_clientHeight(se) - 4 && igtbl_getRowSizing(gn, bandNo, row.Element) == 2 && !se.getAttribute("groupRow"))
	{
		gs.Element.setAttribute("elementMode", "resize");
		gs.Element.setAttribute("resizeRow", row.Element.id);
		row.Element.style.height = row.Element.offsetHeight;

		
		
		ig_csom.addEventListener(document, "mousemove", igtbl_resizeRowMouseMove);
		ig_csom.addEventListener(document, "mouseup", igtbl_resizeRowMouseUp);			
	}
	else
	{
		var te = gs.Element;
		var workTableId;
		if (
		   (row.IsAddNewRow
		
			|| row.IsFilterRow
		    ) && row.Band.Index == 0)
			workTableId = gs.Element.id;
		else
			if (se.getAttribute("groupRow"))
			workTableId = se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
		else
			workTableId = row.Element.parentNode.parentNode.id;
		if (igtbl_button(gn, evnt) != 0)
			return;
		if (workTableId == "")
			return;
		
		te.removeAttribute("lastSelectedCell");
		var prevSelRow = gs.SelectedRows[igtbl_getWorkRow(row.Element, gn).id];
		if (prevSelRow && igtbl_getLength(gs.SelectedRows) > 1)
			prevSelRow = false;
		var selPresent = (igtbl_getLength(gs.SelectedCells) > 0 ? 1 : 0) | (igtbl_getLength(gs.SelectedRows) > 0 ? 2 : 0) | (igtbl_getLength(gs.SelectedCols) > 0 ? 4 : 0);
		if (se.getAttribute("groupRow") || !cell || igtbl_getCellClickAction(gn, bandNo) == 2)
		{
			if (!(igtbl_getSelectTypeRow(gn, bandNo) == 3 && evnt.ctrlKey) && !(row.getSelected() && igtbl_getLength(gs.SelectedRows) == 1))
				igtbl_clearSelectionAll(gn, (row && row.Element ? row.Element.id : "")); 
		}
		else
		{
			if (!(igtbl_getSelectTypeCell(gn, bandNo) == 3 && evnt.ctrlKey) && !(cell.getSelected() && igtbl_getLength(gs.SelectedCells) == 1))
				igtbl_clearSelectionAll(gn, (row && row.Element ? row.Element.id : "")); 
		}
		gs.Element.setAttribute("elementMode", "select");
		if (se.getAttribute("groupRow"))
		{
			te.setAttribute("selectTable", se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id);
			te.setAttribute("selectMethod", "row");
		}
		else
		{
			te.setAttribute("selectTable", workTableId);
			if (!cell || igtbl_getCellClickAction(gn, bandNo) == 2)
				te.setAttribute("selectMethod", "row");
			else
				te.setAttribute("selectMethod", "cell");
		}
		if (te.getAttribute("shiftSelect") && evnt.shiftKey)
			igtbl_selectRegion(gn, se);
		else
		{
			if (!cell || igtbl_getCellClickAction(gn, bandNo) == 2 || se.getAttribute("groupRow"))
			{
				var seRow = igtbl_getRowById(row.Element.id);
				if (gs.SelectedRows[row.Element.id] && evnt.ctrlKey)
				{
					igtbl_selectRow(gn, seRow, false);
					gs.setActiveRow(seRow);
				}
				else
				{
					var showEdit = true;
					if (!gs._exitEditCancel)
					{
						if (gs.Activation.AllowActivation)
						{
							var ar = gs.oActiveRow;
							if (ar != seRow)
							{
								gs.setActiveRow(seRow);
								showEdit = false;
							}
							else
								showEdit = true;
						}
						if (igtbl_getSelectTypeRow(gn, bandNo) > 1)
							igtbl_selectRow(gn, seRow, true, !prevSelRow);
						if (showEdit && !se.getAttribute("groupRow") && row)
							row.editRow();
					}
				}
			}
			else
			{
				if (cell.getSelected() && evnt.ctrlKey)
				{
					cell.select(false);
					cell.activate();
				}
				else
				{
					if (band.getSelectTypeCell() > 1 && band.getCellClickAction() >= 1 && !gs._exitEditCancel)
						cell.select();
					else if (selPresent)
					{
						var gsNPB = gs.NeedPostBack;
						igtbl_fireEvent(gn, gs.Events.AfterSelectChange, "(\"" + gn + "\",\"" + id + "\");");
						if (!gsNPB && !(gs.Events.AfterSelectChange[1] & selPresent))
							gs.NeedPostBack = false;
					}
					cell.activate();
				}
			}
			if (se.getAttribute("groupRow"))
				te.setAttribute("startPointRow", se.parentNode.parentNode.parentNode.parentNode.parentNode.id);
			else
				te.setAttribute("startPointRow", row.Element.id);
			te.setAttribute("startPointCell", id);
			te.removeAttribute("shiftSelect");
			if (!evnt.ctrlKey)
				te.setAttribute("shiftSelect", true);
		}
	}
	if (typeof (igtbl_currentEditTempl) != "undefined" && igtbl_currentEditTempl != null)
		igtbl_gRowEditMouseDown(evnt);
	if (typeof (igcmbo_currentDropped) != "undefined" && igcmbo_currentDropped != null)
		igcmbo_mouseDown(evnt);
}

function igtbl_cellClickUp(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return;
	
	gs.event = evnt;
	gs._mouseDown = 0;
	if (igtbl_button(gn, evnt) == 2)
		return;
	
	
	if (gs.Element.getAttribute("mouseDown"))
		gs.Element.removeAttribute("mouseDown");
	else
		return;
	var se = igtbl_srcElement(evnt);
	if (!se || se == gs._editorCurrent || (se.tagName && se.tagName.length > 4))
	{
		
		if (se && se != gs._editorCurrent)
		{
			while (se && (!(se.tagName == "TD"
				|| se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH"
				) || se.id.length < gn.length || se.id.substr(0, gn.length) != gn))
				se = se.parentNode;
			if (se)
			{
				if (igtbl_fireEvent(gn, gs.Events.MouseUp, "(\"" + gn + "\",\"" + se.id + "\"," + igtbl_button(gn, evnt) + ")") == true)
				{
					igtbl_cancelEvent(evnt);
					return true;
				}
			}
		}
		return;
	}
	if (!gs._editorCurrent && gs._focusElem && !gs._focus0)
		igtbl_activate(gn);
	if (se.id == gn + "_vl" || se.id == gn + "_tb" || se.id == gn + "_ta")
		return;
	var sel = igtbl_getElementById(gn + "_vl");
	if (sel && sel.style.display == "" && sel.getAttribute("noOnBlur"))
		return igtbl_cancelEvent(evnt);
	if (se.tagName == "IMG" && se.getAttribute("imgType") == "expand")
	{
		igtbl_toggleRow(evnt);
		return;
	}
	while (se && (!(se.tagName == "TD"
				|| se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH"
				) || se.id.length < gn.length || se.id.substr(0, gn.length) != gn))
		se = se.parentNode;
	if (!se)
		return;
	
	if (se.tagName != "TD"
	    && !(se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH")
	)
		return;
	if (se.id == "")
		return;
	var row;
	var id = se.id;
	var cell = igtbl_getCellById(id);
	if (cell)
	{
		row = cell.Row;
		id = cell.Element.id;
	}
	else row = igtbl_getRowById(id);
	if (!row && !cell) return;
	var te = gs.Element;
	var mode = gs.Element.getAttribute("elementMode");
	gs.Element.removeAttribute("elementMode");
	te.removeAttribute("selectTable");
	te.removeAttribute("selectMethod");
	te.removeAttribute("resizeRow");
	
	var resizeDiv = document.body.igtbl_resizeDiv;
	if (resizeDiv) resizeDiv.style.display = "none";
	if (!te.getAttribute("shiftSelect"))
	{
		te.removeAttribute("startPointRow");
		te.removeAttribute("startPointCell");
	}
	var bandNo = row.Band.Index;
	var fac = row.Band.firstActiveCell;
	if (cell && igtbl_getCellClickAction(gn, bandNo) == 1 && gs._mouseID == id)
	{
		
		if (igtbl_getAllowUpdate(gn, bandNo, cell.Column.Index) == 3 && cell.getEditable() != "no")
			row.editRow(true);
		else
			cell.beginEdit();
	}
	var oldNPB = gs.NeedPostBack;
	if (!se.getAttribute("groupRow") && mode != "resize")
	{
		if (!cell)
			igtbl_fireEvent(gn, gs.Events.RowSelectorClick, "(\"" + gn + "\",\"" + row.Element.id + "\"," + igtbl_button(gn, evnt) + ")");
		else
			igtbl_fireEvent(gn, gs.Events.CellClick, "(\"" + gn + "\",\"" + id + "\"," + igtbl_button(gn, evnt) + ")");
	}
	gs._noCellChange = false;
	if (igtbl_fireEvent(gn, gs.Events.MouseUp, "(\"" + gn + "\",\"" + id + "\"," + igtbl_button(gn, evnt) + ")") == true)
	{
		igtbl_cancelEvent(evnt);
		return true;
	}
	
	if ((mode == "resize" || mode == "select") && oldNPB)
	{
		se = igtbl_srcElement(evnt);
		if (!(se && se.tagName == "INPUT" && se.type == "checkbox"))
			igtbl_doPostBack(gn);
		return;
	}
	if (gs.NeedPostBack && (!cell || igtbl_getCellClickAction(gn, bandNo) == 2))
		igtbl_doPostBack(gn, 'RowClick:' + row.Element.id + (row.Element.getAttribute("level") ? "\x05" + row.Element.getAttribute("level") : ""));
	else if (gs.NeedPostBack)
		igtbl_doPostBack(gn, 'CellClick:' + id + (cell.Element.getAttribute("level") ? "\x05" + cell.Element.getAttribute("level") : ""));
	
	var ctd = false;
	for (var gId in igtbl_gridState)
		if (gId != gn)
	{
		igtbl_globalMouseUp(evnt, gId);
		ctd = true;
	}
	if (ctd && !igtbl_inEditMode(gn))
		window.setTimeout("igtbl_activate('" + gn + "');", 0);
	return igtbl_cancelEvent(evnt);
}

function igtbl_cellContextMenu(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return;
	gs.event = evnt;
	var te = gs.Element;
	te.removeAttribute("mouseDown");
	te.removeAttribute("elementMode");
	te.removeAttribute("resizeColumn");
	te.removeAttribute("selectMethod");
	if (!te.getAttribute("shiftSelect"))
		te.removeAttribute("startColumn");
	var se = igtbl_srcElement(evnt);
	if (!se || se.id == gn + "_vl" || se.id == gn + "_tb" || se.id == gn + "_ta")
		return;
	while (se && !(se.tagName == "TD"
				|| se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH"
				))
		se = se.parentNode;
	
	if (!se || (se.tagName != "TD" && se.tagName != "TH"))
		return;
	var row;
	var cell = igtbl_getCellByElement(se);
	var id = se.id;
	if (cell)
	{
		row = cell.Row;
		id = cell.Element.id;
	}
	else row = igtbl_getRowById(id);
	if (!row && !cell) return;
	if (!se.getAttribute("groupRow"))
	{
		if (!cell)
			igtbl_fireEvent(gn, gs.Events.RowSelectorClick, "(\"" + gn + "\",\"" + row.Element.id + "\",2)");
		else
			igtbl_fireEvent(gn, gs.Events.CellClick, "(\"" + gn + "\",\"" + id + "\",2)");
	}
	if (igtbl_fireEvent(gn, gs.Events.MouseUp, "(\"" + gn + "\",\"" + id + "\",2)") == true)
		return igtbl_cancelEvent(evnt);
	if (gs.NeedPostBack && (!cell || igtbl_getCellClickAction(gn, row.Band.Index) == 2))
		igtbl_doPostBack(gn, 'RowClick:' + row.Element.id + (row.Element.getAttribute("level") ? "\x05" + row.Element.getAttribute("level") : ""));
	else if (gs.NeedPostBack)
		igtbl_doPostBack(gn, 'CellClick:' + id + (cell.Element.getAttribute("level") ? "\x05" + cell.Element.getAttribute("level") : ""));
}

function igtbl_cellMouseOver(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	var se = igtbl_srcElement(evnt);
	if (!gs || !se || gs.isDisabled())
		return;
	gs.event = evnt;
	
	
	try { if (se.nodeName == "TD" || (se.nodeName == "DIV" && '' + se.contentEditable != 'true')) se.unselectable = "on"; } catch (ex) { ; }
	if (se.tagName == "NOBR")
	{
		var cell = igtbl_getCellByElement(se.parentNode);
		if (cell)
		{
			var nobr = cell.Element.childNodes[0];

			if (cell.Element.title)
			{
				nobr.title = cell.Element.title;
			}
			else
			{
				var showTitle = nobr.offsetWidth > cell.Element.offsetWidth
                    || nobr.offsetHeight > cell.Element.offsetHeight
                    || (cell.Element.style.textOverflow == "ellipsis" && nobr.offsetWidth + 6 > cell.Element.offsetWidth)
				
			    	|| (cell.Element.currentStyle && cell.Element.currentStyle.textOverflow == "ellipsis" && (nobr.offsetWidth + 6 > cell.Element.offsetWidth || cell.Element.scrollWidth > cell.Element.clientWidth));

				var titleMode = cell.getTitleModeResolved();
				showTitle |= titleMode == igtbl_CellTitleMode.Always;
				showTitle &= titleMode != igtbl_CellTitleMode.Never;
				if (showTitle)
				{
					
					if (gs.Section508Compliant && titleMode != igtbl_CellTitleMode.OnOverflow)
					{
						var row = cell.Row;
						if (row)
						{
							var fmtStr = (row.ParentRow) ? gs._childRowToolTipFormatStr : gs._rowToolTipFormatStr;
							
							fmtStr = fmtStr.replace("{0}", (1 + row.getIndex()).toString());
							
							
							fmtStr = fmtStr.replace("{1}", (cell.Column.HeaderText));
							
							
							if (igtbl_string.trim(cell.MaskedValue))
							{
								nobr.title = fmtStr.replace("{2}", cell.MaskedValue);
							}
							else
							{
								nobr.title = fmtStr.replace("{2}", cell.getValue(true));
							}
						}
					}
					else
						if (igtbl_string.trim(cell.MaskedValue))
					{
						nobr.title = cell.MaskedValue;
					}
					else
					{
						nobr.title = cell.getValue(true);
					}
				}
			}
		}
		se = se.parentNode;
	}
	while (se && !(se.tagName == "TD"
				|| se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH"
				))
		se = se.parentNode;
	if (!se || se.tagName != "TD"
		&& se.tagName != "TH"
	)
		return;
	var row;
	var cell = igtbl_getCellByElement(se);
	var id = se.id;
	if (cell)
	{
		if (!cell.Element) return;
		row = cell.Row;
		id = cell.Element.id;
	}
	else row = igtbl_getRowById(se.id);
	if (!row && !cell) return;
	if (se._hasMouse) return;
	se._hasMouse = true;
	var te = gs.Element;
	if (evnt.shiftKey && row.Band.getSelectTypeRow() == 3 && !te.getAttribute("shiftSelect"))
		te.setAttribute("shiftSelect", true);
	if (igtbl_fireEvent(gn, gs.Events.MouseOver, "(\"" + gn + "\",\"" + id + "\",0)") == true)
		return;
}

function igtbl_cellMouseMove(evnt, gn)
{
	var se = igtbl_srcElement(evnt);
	var gs = igtbl_getGridById(gn);
	if (!gs || !se || gs.isDisabled())
		return;
	gs.event = evnt;
	var te = gs.Element;
	if (se.id == gn + "_vl" || se.id == gn + "_tb" || se.id == gn + "_ta")
		return;
	if (te.getAttribute("resizeRow") && (se.tagName == "TH" && se.parentNode.parentNode.tagName == "TFOOT" || se.tagName == "TD" && se.parentNode.getAttribute("hiddenRow")))
		return igtbl_tableMouseMove(evnt, gn);
	while (se && !(se.tagName == "TD"
				|| se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH"
				))
		se = se.parentNode;
	if (!se || se.tagName != "TD"
		&& se.tagName != "TH"
	)
		return;
	var row;
	var cell = igtbl_getCellByElement(se);
	var id = se.id;
	if (cell)
	{
		row = cell.Row;
		if (!cell || !cell.Element) return;
		id = cell.Element.id;
	}
	else row = igtbl_getRowById(se.id);
	if (!row && !cell)
		return;
	
	if (cell && cell.Row.IsFilterRow) return;
	var bandNo = row.Band.Index;
	var fac = row.Band.firstActiveCell;
	
	if((!ig_shared.IsIE9Plus && igtbl_button(gn, evnt) == 0) || gs.Element.getAttribute("mouseDown"))
	{
		var mode = te.getAttribute("elementMode");
		
		if (!cell)
		{
			var cursorName = se.getAttribute("oldCursor");
			if (cursorName != null)
			{
				se.style.cursor = cursorName;
				se.removeAttribute("oldCursor");
			}
		}
		if (mode && mode == "select" && !evnt.ctrlKey)
		{			
			var lsc = te.getAttribute("lastSelectedCell");
			if (!lsc || lsc != se.id)
				igtbl_selectRegion(gn, se);
			te.setAttribute("lastSelectedCell", id);
		}
	}
	else if (igtbl_getOffsetY(evnt, se) > igtbl_clientHeight(se) - 4 && !cell && igtbl_getRowSizing(gn, bandNo, row.Element) == 2)
	{
		var cursorName = se.getAttribute("oldCursor");
		if (cursorName == null)
			se.setAttribute("oldCursor", se.style.cursor);
		se.style.cursor = "n-resize";
		igtbl_colButtonMouseOut(null, gn);
	}
	else
	{
		te.removeAttribute("elementMode");
		te.removeAttribute("resizeRow");
		var cursorName = se.getAttribute("oldCursor");
		if (cursorName != null)
		{
			se.style.cursor = cursorName;
			se.removeAttribute("oldCursor");
		}
		if (!cell)
			igtbl_colButtonMouseOut(null, gn);
		else
		{
			var column = (cell ? cell.Column : null);
			
			if (cell && cell.hasButtonEditor(igtbl_cellButtonDisplay.OnMouseEnter))
			{
				if (gs._editorButton && gs._editorButton.style.display != "")
					if (gs._mouseWait++ > 5)
					gs._mouseWait = 0;
				if (gs._mouseIn != id)
					igtbl_showColButton(gn, cell.Element);
			}
			else
				igtbl_colButtonMouseOut(null, gn);
		}
	}
	gs._mouseIn = id;
	return false;
}

// Event handler for mouse out from cell
function igtbl_cellMouseOut(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	var se = igtbl_srcElement(evnt);
	if (!gs || !se || gs.isDisabled())
		return;
	gs.event = evnt;
	if (se.tagName == "NOBR")
	{
		var cell = igtbl_getCellByElement(se.parentNode);
		if (cell)
			cell.Element.childNodes[0].title = "";
		se = se.parentNode;
	}
	while (se && !(se.tagName == "TD"
				|| se.parentNode && se.parentNode.tagName == "TR" && se.tagName == "TH"
				))
		se = se.parentNode;
	if (!se || se.tagName != "TD"
		&& se.tagName != "TH"
	)
		return;
	var row;
	var cell = igtbl_getCellByElement(se);
	var id = se.id;
	if (cell)
	{
		if (!cell.Element) return;
		row = cell.Row;
		id = cell.Element.id;
		var btn = igtbl_getElementById(gn + "_bt")
		if (btn && btn.style.display == "" && btn.getAttribute("srcElement") == id && evnt.toElement && evnt.toElement.id != id && evnt.toElement.id != gn + "_bt")
			igtbl_colButtonMouseOut(null, gn);
	}
	else row = igtbl_getRowById(id);
	if (!row && !cell) return;
	if (igtbl_isMouseOut(se, evnt))
		igtbl_fireEvent(gn, gs.Events.MouseOut, "(\"" + gn + "\",\"" + id + "\",0)");
}

function igtbl_cellDblClick(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || gs.isDisabled())
		return;
	gs.event = evnt;
	var se = igtbl_srcElement(evnt);
	if (!se || se.id == gn + "_vl" || se.id == gn + "_tb" || se.id == gn + "_ta")
		return;
	while (se && (se.tagName != "TD" && se.tagName != "TH" || se.id.length < gn.length || se.id.substr(0, gn.length) != gn))
		se = se.parentNode;
	if (!se)
		return;
	if (se.tagName != "TD" && se.tagName != "TH")
		return;
	var row;
	var id = se.id;
	var cell = igtbl_getCellById(id);
	if (cell)
	{
		row = cell.Row;
		id = cell.Element.id;
	}
	else row = igtbl_getRowById(se.id);
	var column = igtbl_getColumnById(se.id);
	if (!row && !cell && !column) return;
	if (se.tagName == "TD"
		|| se.tagName == "TH" && row 
	)
	{
		if (se.getAttribute("groupRow"))
		{
			igtbl_toggleRow(gn, row.Element.id);
			return;
		}
		
		if (igtbl_fireEvent(gn, gs.Events.DblClick, "(\"" + gn + "\",\"" + id + "\")") == true)
			return;
		if (row && !cell)
		{
			if (gs.NeedPostBack)
			
				igtbl_doPostBack(gn, 'RowDblClick:' + row.Element.id + (row.getLevel(true) ? "\x05" + row.getLevel(true) : ""));
			return;
		}
		var bandNo = row.Band.Index;
		if (gs.NeedPostBack)
		{
			if (igtbl_getCellClickAction(gn, bandNo) == 2)
			
				igtbl_doPostBack(gn, 'RowDblClick:' + row.Element.id + (row.getLevel(true) ? "\x05" + row.getLevel(true) : ""));
			else
				igtbl_doPostBack(gn, 'CellDblClick:' + id + (cell.getLevel(true) ? "\x05" + cell.getLevel(true) : ""));
			return;
		}
		if (igtbl_getCellClickAction(gn, bandNo) == 0)
			return;

		var cancelEdit = gs._exitEditCancel;
		var activeRow = gs.getActiveRow();
		if (activeRow && activeRow != row)
		{
			cancelEdit |= gs.fireEvent(gs.Events.BeforeRowDeactivate, [gs.Id, activeRow.Element.id]) == true;
			cancelEdit |= gs.fireEvent(gs.Events.BeforeRowActivate, [gs.Id, row.Element.id]) == true;
		}
		if (!cancelEdit)
		{
			
			if (cell.Column.getAllowUpdate() == 3 && cell.getEditable() != "no" && !cell.Row.IsFilterRow)
				row.editRow(true);
			else
				cell.beginEdit();
		}
	}
	else
	{
		
		if (igtbl_fireEvent(gn, gs.Events.DblClick, "(\"" + gn + "\",\"" + se.id + "\")") == true)
			return;
		if (gs.NeedPostBack)
			igtbl_doPostBack(gn, 'HeaderDblClick:' + se.id);
	}
}

var igtbl_dontHandleChkBoxChange = false;
function igtbl_chkBoxChange(evnt, gn)
{
	if (igtbl_dontHandleChkBoxChange ||
	 (ig_csom.IsIE && evnt.propertyName != "checked") ||
	 (!ig_csom.IsIE && evnt.type != "change")
	 )
		return false;
	var se = igtbl_srcElement(evnt);
	if (!se) return false;
	var c = se.parentNode;
	while (c && !(c.tagName == "TD" && c.id != ""))
		c = c.parentNode;
	if (!c) return;
	var s = se;
	var cell = igtbl_getCellById(c.id);
	if (!cell) return;
	if (!evnt && event) evnt = event; 
	if (!gn) gn = cell.Band.Grid.Id; 
	var column = cell.Column;
	var gs = igtbl_getGridById(gn);
	gs.event = evnt;
	var oldValue = !s.checked;
	if (gs._exitEditCancel || !cell.isEditable() || igtbl_fireEvent(gn, gs.Events.BeforeCellUpdate, "(\"" + gn + "\",\"" + c.id + "\",\"" + s.checked + "\")"))
	{
		igtbl_dontHandleChkBoxChange = true;
		s.checked = oldValue;
		igtbl_dontHandleChkBoxChange = false;
		return true;
	}
	cell.Row._dataChanged |= 2;
	if (typeof (cell._oldValue) == "undefined")
		cell._oldValue = oldValue;
	igtbl_saveChangedCell(gs, cell, s.checked.toString());
	cell.Value = cell.Column.getValueFromString(s.checked);
	if (!c.getAttribute("oldValue"))
		c.setAttribute("oldValue", s.checked);
		
	if (gs.Section508Compliant)
		s.alt = (s.checked ? "checked" : "unchecked");
	
	if (c.getAttribute(igtbl_sUnmaskedValue))
		c.setAttribute(igtbl_sUnmaskedValue, s.checked.toString());
	c.setAttribute("chkBoxState", s.checked.toString());
	var cca = igtbl_getCellClickAction(gn, column.Band.Index);
	if (cca == 1 || cca == 3)
		igtbl_setActiveCell(gn, c);
	else if (cca == 2)
		igtbl_setActiveRow(gn, c.parentNode);

	if (cell.Node)
	{
		cell.setNodeValue(!s.checked ? "False" : "True");
		var cdata = cell.Node.firstChild;
		if (s.checked)
			cdata.text = cdata.text.replace("type='checkbox'", "type='checkbox' checked"); 
		else
			cdata.text = cdata.text.replace(" checked", "");
		gs.invokeXmlHttpRequest(gs.eReqType.UpdateCell, cell, s.checked);
	}
	else if (ig_csom.IsNetscape6)
		gs.invokeXmlHttpRequest(gs.eReqType.UpdateCell, cell, s.checked);
	igtbl_fireEvent(gn, gs.Events.AfterCellUpdate, "(\"" + gn + "\",\"" + c.id + "\",\"" + s.checked + "\")");
	if (gs.LoadOnDemand == 3)
		gs.NeedPostBack = false;
	if (gs.NeedPostBack)
		igtbl_doPostBack(gn);
	return false;
}

function igtbl_colButtonClick(evnt, gn, b, se)
{
	if (!b) b = igtbl_getElementById(gn + "_bt");
	if (b && se == null)
		se = igtbl_getElementById(b.getAttribute("srcElement"));
	if (se == null || !se.id)
	{
		if (!se)
			se = igtbl_srcElement(evnt).parentNode;
		else
			se = se.parentNode;
		if (se && se.tagName == "NOBR")
			do
		{
			se = se.parentNode;
		} while (se && se.tagName != "TD");
	}
	var gs = igtbl_getGridById(gn);
	if (gs == null || gs._exitEditCancel || se == null || se.id == "" || gs.isDisabled())
		return;
	gs.event = evnt;
	var cell = igtbl_getCellById(se.id);
	if (!cell) return;
	var sel = cell != gs.oActiveCell;
	try
	{
		if (sel && igtbl_isChild(gn, cell.Element)) cell.activate();
	} catch (e) { }
	igtbl_fireEvent(gn, gs.Events.ClickCellButton, "(\"" + gn + "\",\"" + se.id + "\")");
	if (gs.NeedPostBack)
	{
		
		var cellInfo = cell.Row._generateUpdateRowSemaphore(false);
		if (igtbl_doPostBack(gn, 'CellButtonClick:' + se.id + "\x05" + cell.getLevel(true) + (gs.LoadOnDemand == 3 ? "\x02" + cellInfo + "\x02" + cell.Row.Band.Index + "\x02" + cell.Row.DataKey : "")))
			return;
	}
}

function igtbl_colButtonMouseOut(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	if (gs == null || gs.isDisabled()) return;
	var b = igtbl_getElementById(gn + "_bt");
	if (!b || b.getAttribute("noOnBlur"))
		return;
	if (evnt && evnt.toElement && evnt.toElement.id == b.getAttribute("srcElement"))
		return;
	if (b.style.display == "")
	{
		b.setAttribute("noOnBlur", true);
		b.style.display = "none";
		b.removeAttribute("srcElement");
		if (!gs.Activation.AllowActivation)
			return;
		if (gs.oActiveCell)
		{
			
			if (gs.oActiveCell.hasButtonEditor(igtbl_cellButtonDisplay.OnMouseEnter))
				igtbl_showColButton(gn, gs.oActiveCell.Element);
		}
		window.setTimeout("igtbl_clearNoOnBlurBtn('" + gn + "')", 100);
		gs._mouseIn = null;
	}
}

function igtbl_colButtonEvent(evnt, gn)
{
}


function igtbl_dropDownChange(evnt, gn)
{
	var sel = null;
	if (!gn)
	{
		sel = igtbl_srcElement(evnt);
		gn = sel.id.substring(0, sel.id.length - 3);
	}
	else
	{
		sel = igtbl_getElementById(gn + "_vl");
	}
	igtbl_getGridById(gn).event = evnt;
	if (sel && sel.style.display == "")
		igtbl_fireEvent(gn, igtbl_getGridById(gn).Events.ValueListSelChange, "(\"" + gn + "\",\"" + gn + "_vl\",\"" + sel.getAttribute("currentCell") + "\");");
}

function igtbl_fixedClick(evnt)
{
	var se = igtbl_srcElement(evnt);
	if (!se) return;
	var pn = se.parentNode;
	while (pn && pn.tagName != "TH") pn = pn.parentNode;
	if (!pn || !pn.id) return;
	var column = igtbl_getColumnById(pn.id);
	if (column.Band.Grid.UseFixedHeaders)
	{
		if (column.getFixed())
			igtbl_doPostBack(column.Band.Grid.Id, "Unfix:" + column.Band.Index + ":" + column.Index);
		else
			igtbl_doPostBack(column.Band.Grid.Id, "Fix:" + column.Band.Index + ":" + column.Index);
		return igtbl_cancelEvent(evnt);
	}
}

function igtbl_mouseWheel(evnt, gn)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || !gs._scrElem || gs.isDisabled()) return;
	if (evnt.wheelDelta)
	{
		igtbl_hideEdit(gn);
		gs._scrElem.scrollTop -= evnt.wheelDelta / 3;
	}
}

if (typeof (ig_csom) != "undefined")
	ig_csom.addEventListener(document.documentElement, "mousewheel", igtbl_globalMouseWheel);

function igtbl_globalMouseWheel(evnt)
{
	
	var scrElem = evnt.srcElement ? evnt.srcElement : evnt.target;
	var hideEdit = true;
	for (var gn in igtbl_gridState)
	{
		if (scrElem == null || scrElem.id != gn + "_vl")
		{
			
			
			if (typeof (igcmbo_getComboById) == "function")
			{
				var g = igtbl_getGridById(gn);
				
				if (igtbl_getElementById(g.Id) != null)
				{
					
					var comboId = igtbl_getElementById(g.Id).getAttribute("igComboId"); 
					var src = igcmbo_srcElement(evnt);
					if (comboId)
					{
						if (igtbl_isAChildOfB(src, g.Element.parentNode))
						{
							hideEdit = false;
						}
					}
					else
					{
						
						if (g._editorCurrent && igcmbo_displaying && igcmbo_displaying.Element.id == g._editorCurrent.id)
						{
							
							if (igtbl_isAChildOfB(src, igcmbo_displaying.getGrid().Element.parentNode))
							{
								hideEdit = false;
							}
						}
					}
				}
			}
			
			if (hideEdit)
			{
				igtbl_hideEdit(gn);

				
				var grid = igtbl_getGridById(gn);
				for (var b = 0; b < grid.Bands.length; b++)
				{
					for (var c = 0; c < grid.Bands[b].Columns.length; c++)
						for (var v = 0; v < grid.Bands[b].Columns[c].Validators.length; v++)
					{
						if (document.getElementById(grid.Bands[b].Columns[c].Validators[v])
			                    && !document.getElementById(grid.Bands[b].Columns[c].Validators[v]).isvalid)
						{
							return false;
						}
					}
				}
			}
			hideEdit = true;
		}
	}
}

function igtbl_onScrollFixed(evnt, gn)
{
	var g = igtbl_getGridById(gn)
	if (!g || !g._scrElem) return;
	var s = g.Element.parentNode.scrollTop;
	igtbl_scrollTop(g.Element.parentNode, 0);
	igtbl_scrollTop(g.Element.parentNode.parentNode, 0);
	igtbl_scrollTop(g._scrElem, s);
}

function igtbl_onResizeFixed(evnt, gn)
{
	var g = igtbl_getGridById(gn)
	if (!g || !g._scrElem) return;
	if (g.Element.getAttribute("noOnResize"))
	{
		if (g._scrElem.getAttribute("oldW"))
			g._scrElem.style.width = g._scrElem.getAttribute("oldW");
		if (g._scrElem.getAttribute("oldH"))
			g._scrElem.style.height = g._scrElem.getAttribute("oldH");
		return igtbl_cancelEvent(evnt);
	}
	if (!g._scrElem.style.width || g._scrElem.style.width.charAt(g._scrElem.style.width.length - 1) != "%")
		g._scrElem.setAttribute("oldW", g._scrElem.offsetWidth);
	if (!g._scrElem.style.height || g._scrElem.style.height.charAt(g._scrElem.style.height.length - 1) != "%")
		g._scrElem.setAttribute("oldH", g._scrElem.offsetHeight);
}



function igtbl_onStationaryMarginScroll(evnt, gn, marginId)
{
	var gs = igtbl_getGridById(gn);
	var marginElement = document.getElementById(marginId);
	if (marginElement && marginElement.scrollLeft != 0 && !gs.UseFixedHeaders)
	{
		
		var scrollLeft = marginElement.scrollLeft + -marginElement.childNodes[0].offsetLeft;
		marginElement.scrollLeft = 0;
		gs.DivElement.scrollLeft = scrollLeft;
	}
	else if (marginElement && marginElement.scrollLeft != 0 && gs.UseFixedHeaders)
	{
		
		var offsetDiv = marginElement.childNodes[0].childNodes[1].childNodes[0].childNodes[0].childNodes[0];
		if (offsetDiv.id != gn + "_drs")
		{
			offsetDiv = marginElement.childNodes[0].childNodes[1].childNodes[0].childNodes[2].childNodes[0];
		}
		var scrollLeft = marginElement.scrollLeft + -offsetDiv.childNodes[0].offsetLeft;
		marginElement.scrollLeft = 0;
		document.getElementById(gn + "_divscr").scrollLeft = scrollLeft;
	}
}


var igtbl_oldOnBodyResize;
function igtbl_onBodyResize()
{
	var result;
	if (igtbl_oldOnBodyResize)
	{
		result = igtbl_oldOnBodyResize();
	}
	if (!document.body.getAttribute("noOnBodyResize"))
		for (var gridId in igtbl_gridState)
	{
		var grid = igtbl_getGridById(gridId);
		
		if (!grid || !grid.MainGrid || !grid.MainGrid.parentNode)
			continue;
		
		
		
		if (igtbl_inEditMode(gridId))
		{
			
			var edit = grid._editorCurrent;
			edit = edit ? edit.Object : null;
			
			if (edit && edit._dropTime && (new Date()).getTime() < edit._dropTime + 200)
				break;
			igtbl_hideEdit(gridId);
		}
		
		grid.alignStatMargins();
	}
	return result;
}




igtbl_oldSelectTab = null;
function igtbl_tabChanges(tab, index)
{
	var selectedTab = tab.getSelectedTab();
	if (selectedTab && selectedTab.index != index)
	{
		for (var gId in igtbl_gridState)
		{
			var g = igtbl_getGridById(gId);
			if (igtbl_isAChildOfB(g.MainGrid, selectedTab.elemDiv))
			{
				var pn = g.MainGrid.parentNode;
				if (!pn.id || pn.id.length <= 10 || pn.id.substr(pn.id.length - 10, 10) != "_container")
					g.hide();

				
				igtbl_showColButton(gId, "hide");
			}
		}
	}
	igtbl_oldSelectTab(tab, index, arguments[2]);
	selectedTab = tab.getSelectedTab();
	if (selectedTab)
	{
		for (var gId in igtbl_gridState)
		{
			var g = igtbl_getGridById(gId);
			if (igtbl_isAChildOfB(g.MainGrid, selectedTab.elemDiv))
			{
				g.show();
			}
		}
	}
}

function igtbl_onPagerClick(gn, evnt)
{
	var g = igtbl_getGridById(gn);
	if (!g || !evnt) return;
	if (!g.isLoaded())
		return ig_cancelEvent(evnt);
}


function igtbl_showFilterOptions(evnt)
{
	var columnId = null;
	if(typeof evnt=="string")
		columnId=evnt;
	else
	{
		
		var x = igtbl_button(null, evnt);
		
		var parentEl = evnt.srcElement;
		
		if (!parentEl) parentEl = evnt.target;
	
		do
		{
			if (parentEl.getAttribute("GroupByHeaderFloatingDiv"))
				return;
			parentEl = parentEl.parentNode;

			if (!columnId && parentEl.tagName == "TH")
				columnId = parentEl.id;
			
		} while (parentEl && parentEl.tagName != "BODY")
	}

	
	var col = igtbl_getColumnById(columnId);

	if (!col || evnt && typeof evnt == "object" && !((ig_csom.IsNetscape6 || ig_csom.IsIE9Plus) && evnt.button == 0 || ig_csom.IsIE && evnt.button == 1)) return;
	
	
	if (col.Band.Grid.Element.getAttribute("elementMode") == "resize") return;
	
	
	var gs = col.Band.Grid;
	var ar = gs.getActiveRow();
	if (ar)
	{
		gs.endEdit();
		if (ar.IsAddNewRow)
			ar.commit();
		else
			if (ar._dataChanged && ar._dataChanged > 1)
			ar.processUpdateRow();
	}

	col.showFilterDropDown();
	return ig_cancelEvent(evnt);
}
function igtbl_filterOptionMouseUp(evnt)
{
	
	if (!evnt || (ig_csom.IsIE && !evnt.srcElement) || (ig_csom.IsFireFox && !evnt.target)) return;
	var src = ig_csom.IsIE ? evnt.srcElement : evnt.target;
	
	while (src && !src.getAttribute("fo"))
		src = src.parentNode;
	if (!src) return;
	
	var value = src.getAttribute("value");
	var filterObject = src.parentNode.parentNode._filterObject;
	var band = filterObject.Column.Band;
	var grid = band.Grid;
	
	
	if (filterObject._setFilter(value)) return;
	filterObject.applyFilter();
	
	grid.alignStatMargins();
	grid.alignDivs();
	grid.fireEvent(grid.Events.AfterRowFilterApplied, [grid.Id, filterObject.Column]);
}


function igtbl_GridCornerClick()
{
	if (ig_csom.IsIE)
	{
		var elem = event.srcElement;
		var gridName = elem.getAttribute("gridName");
		var g = igtbl_getGridById(gridName);
		if (g)
		{
			igtbl_fireEvent(g.Id, g.Events.GridCornerImageClick, '("' + g.Id + '");');
		}
	}
}

var igtbl_oldGlobalMouseMove;

function igtbl_globalMouseMove(evnt)
{
	if (!evnt)
		evnt = event;

	if (igtbl_oldGlobalMouseMove)
	{
		igtbl_oldGlobalMouseMove(evnt);
	}

	if (typeof (igtbl_gridState) != "undefined" && igtbl_gridState)
	{
		for (var gId in igtbl_gridState)
		{
			var gs = igtbl_gridState[gId];
			
			if (gs && gs.Element)
			{
				var srcElem = evnt.srcElement ? evnt.srcElement : evnt.target;
				if (srcElem && !ig_isAChildOfB(srcElem, gs.Element))
				{
					if (gs.Element.getAttribute("elementMode") == "resize")
					{
						 
						if (igtbl_button(gId, evnt) == -1 || (ig_shared.IsIE9Plus && gs.Element.getAttribute("mouseDown") == null))
						{
							igtbl_clearResizeDiv(gs, evnt, true);
						}
					}

					if (srcElem.id != gId+"_bt")
						igtbl_colButtonMouseOut(evnt, gId);
				}
			}
		}
	}
}

function _igtbl_processServerPassedColumnFilters(serverFilterInfo,g)
{
	
	var itrCount = serverFilterInfo.length;
	for (var itr = 0; itr < itrCount ; itr++)
	{
		var filterInfo = serverFilterInfo[itr];
		
		if(!filterInfo)break;
		
		
		if (filterInfo[0])
		{
			var row = igtbl_getRowById(filterInfo[0]);
			if(row)
			{	var parentTableId="";
				var workingBand;
				if (row.Rows && row.Rows.Element)
				{
					var parentTable = row.Rows.Element;
					do
					{
						parentTable = parentTable.parentNode;
					}while (parentTable && !(parentTable.tagName=="TABLE" && parentTable.id.length>0))
					if (!parentTable)continue;
					parentTableId=parentTable.id;
					workingBand=row.Rows.Band;
				}
				else
				{
					parentTableId=filterInfo[0].replace("_r","_t");
					
					workingBand=g.Bands[row.Band.Index+1];
				}
				
				filterPanel = workingBand._filterPanels[parentTableId];
				if(filterPanel)
					filterPanel = filterPanel[filterInfo[1]];
				else
				{
					workingBand._filterPanels[parentTableId] = new Object();
				}
				if (!filterPanel)
				{
					filterPanel = workingBand._filterPanels[parentTableId][filterInfo[1]] = new igtbl_FilterDropDown(igtbl_getColumnById(filterInfo[1]));
					if (row)
						filterPanel.RowIsland = row.Rows;
				}
				filterPanel.setFilter(filterInfo[2],filterInfo[3],true);
			}
		}
		else
		{
			var band = g.Bands[0];
			var filteredColumn = igtbl_getColumnById(filterInfo[1]);
			if (!band._filterPanels[filteredColumn.Id])
			{
				band._filterPanels[filteredColumn.Id] = new igtbl_FilterDropDown(filteredColumn);
			}
			filterPanel = band._filterPanels[filteredColumn.Id];
			filterPanel.RowIsland = filteredColumn.Band.Grid.Rows;
			filterPanel.setFilter(filterInfo[2],filterInfo[3],true);
		}
	}
}
function _igtbl_setFilterIndicators(colFilters,rows)
{

    if (colFilters && colFilters.length>0)
	{
	    
	    _igtbl_setFilterIndicators(undefined,rows);
	    
	    var itrCount = colFilters.length;
	    for (var itr = 0; itr < itrCount ; itr++)
		{
		    var column = igtbl_getColumnById(colFilters[itr][1]);
		    _igtbl_setColumnFilterIndicator(column, true, rows);
        }
    }
    else if(rows)
	{
		
		var band = rows.Band;
		for(var itr = 0; itr < band.Columns.length ; itr++)
		{
			_igtbl_setColumnFilterIndicator(band.Columns[itr], false, rows);
		}
	} 
}

function _igtbl_setColumnFilterIndicator(column, isFiltered, rows)
{
    var filterImageSrc = column.Band.Grid.FilterDefaultImage;
    if(isFiltered)
        filterImageSrc = column.Band.Grid.FilterAppliedImage;
    
    var band = column.Band;    
    if (band.RowFilterMode==1 || band.Index==0 )
	{
		for(var itr = 0; itr < band.Columns.length ; itr++)
		{
			var headerTags = column._getHeadTags();
			if(headerTags)
			{
				for(var cnt = 0; cnt<headerTags.length;cnt++)				    
				    _igtbl_changeFilterImage(column, headerTags[cnt], filterImageSrc);
			}	
		}
	}
	else
	{
		for(var itr = 0; itr < band.Columns.length ; itr++)
		{
			var myDirectColumnHeader = igtbl_getChildElementById(rows.Element.parentNode,column.Id);
			if(myDirectColumnHeader)
		        _igtbl_changeFilterImage(column, myDirectColumnHeader, filterImageSrc);
    	}
	}
}

function _igtbl_changeFilterImage(column, header, newImageSrc)
{
    var filterImg = column._findFilterImage(header);
	if(filterImg)
	{
		filterImg.src = newImageSrc;
        
		var alt = filterImg.getAttribute("alt");
		if(alt != null)
		{
			var clpsAlt = filterImg.getAttribute("igAltF1");
			if(clpsAlt != null){
				filterImg.setAttribute("igAltF0",alt);						
				filterImg.setAttribute("alt",clpsAlt);
				filterImg.removeAttribute("igAltF1");
			}
		}
	}		
}

function _igtbl_containsColumnFilter(column_Id, colFilters) 
{
    if(!colFilters || colFilters.length == 0)
        return false;
    
    for(var x=0; x<colFilters.length; x++) 
    {
        if(column_Id == colFilters[x][1])
            return true;
    }
    
    return false;
}

function igtbl_filterGridScroll()
{
	for(var gridId in igtbl_gridState)
	{
		var g = igtbl_getGridById(gridId);
		if (g._currentFilterDropped)
			g._currentFilterDropped.show(false);
	}
}



function igtbl_filterTypeKeyDown(evnt)
{
    var src = evnt.srcElement?evnt.srcElement:evnt.target; 
    while(src && src.tagName!="TD")
    {
        src=src.parentNode;
    }
    var cell=src.Object;
    if(!cell)cell=igtbl_getCellById(src.id);
    switch(evnt.keyCode)
    {
        case(27): 
        {
            var col=cell.Column;
            if (col.FilterIconsList!=null)
            {
            col.FilterIconsList.show();
            }
            
            igtbl_cancelEvent(evnt);
        }
        case(9): 
        {
            
        }
    }
}


function igtbl_filterTypeSelect(evnt)
{
    var src = evnt.srcElement?evnt.srcElement:evnt.target;
    while(src && src.tagName!="TD")
    {
        src=src.parentNode;
    }
    var cell=src.Object;
    if(!cell)cell=igtbl_getCellById(src.id);
    
    var col=cell.Column;
    if (col.FilterIconsList==null)
    {
        col.FilterIconsList=new igtbl_FilterIconsList(col);
    }
    col.FilterIconsList.show(cell);
    igtbl_cancelEvent(evnt);
}
function igtbl_filterIconsMouseUp(evnt)
{
	
	if (!evnt ||  (ig_csom.IsIE && !evnt.srcElement) || (ig_csom.IsFireFox && !evnt.target) ) return;
	var src = ig_csom.IsIE ? evnt.srcElement : evnt.target ;
	
	while(src&&!src.getAttribute("filterListOption"))
		src=src.parentNode;
	if (!src)return;
	var value=src.getAttribute("operator");
	var filterIconSource=src;
	while(filterIconSource&&!filterIconSource.getAttribute("filterIconList"))
	    filterIconSource=filterIconSource.parentNode;
	filterIconSource=filterIconSource.object;
	var filterCell=filterIconSource._currentCell;
	
	filterCell._setFilterTypeImage(value);
	
	
    var columnFilter = filterCell.Column._getFilterPanel(filterCell.Row.Element);
                        
    if(columnFilter.IsActive())
    {
		
		// If the operand is changed while the compare value is cleared, the filter operand should be all
		// rather than whatever was choosen.
		var compareValue = columnFilter.getEvaluationValue();
        if (compareValue==null || compareValue=="") value=igtbl_filterComparisionOperator.All;
		
        
        columnFilter.setFilter(value, compareValue);
            
        columnFilter.applyFilter();
        
        
        var gs = filterCell.Column.Band.Grid;        
        gs.fireEvent(gs.Events.AfterRowFilterApplied, [gs.Id, filterCell.Column]);

		gs.alignDivs();
    }
    else
    {   
        columnFilter.setOperator(value);
    }
}

function igtbl_clearNoOnBlurBtn(gn)
{
	var b = igtbl_getElementById(gn + "_bt");
	b.removeAttribute("noOnBlur");
}

function igtbl_clearNoOnBlurElem(id)
{
	var e = igtbl_getElementById(id);
	
	if (e) e.removeAttribute("noOnBlur");
}


function igtbl_cancelNoOnBlurTB(gn, id)
{
	if (id)
	{
		var src = igtbl_getElementById(id);
		if (src)
		{
			src.removeAttribute("noOnBlur");
			return;
		}
	}
	var textBox = igtbl_getElementById(gn + "_tb");
	if (textBox && textBox.style.display == "")
		textBox.removeAttribute("noOnBlur");
	var sel = igtbl_getElementById(gn + "_vl");
	if (sel && sel.style.display == "")
		sel.removeAttribute("noOnBlur");
}

function igtbl_cancelNoOnBlurDD(gn)
{
	if (arguments.length == 0)
		gn = igtbl_lastActiveGrid;
	var gs = igtbl_getGridById(gn);
	
	if (gs && (gs.editorControl || gs._editorCustom))
	{
		if (gs.editorControl)
			gs.editorControl.Element.removeAttribute("noOnBlur");
		else
			gs._editorCustom.Element.removeAttribute("noOnBlur");
	}
}

function igtbl_blur(gn)
{
	window.setTimeout("igtbl_blurTimeout('" + gn + "')", 100);
}

function igtbl_blurTimeout(gn)
{
	var g = igtbl_getGridById(gn);
	if (!g) return;
	var ar = g.getActiveRow();
	var activeElement = null;
	try
	{
		activeElement = document.activeElement;
	} catch (e) { ; }
	if (ar && !igtbl_inEditMode(gn) && activeElement && !igtbl_isAChildOfB(activeElement, g.DivElement))
	{
		if (ar.IsAddNewRow)
			ar.commit();
		else
			if (ar.processUpdateRow)
			ar.processUpdateRow();
	}
	if (g._focusElem)
		if (g._lastKey == 9 || g._lastKey == 13 || g._lastKey == 27)
		igtbl_activate(gn);
}

function igtbl_getGridById(gridId)
{
	if (typeof (igtbl_gridState) == "undefined")
		return null;
	var grid = igtbl_gridState[gridId];
	if (!grid)
		for (var gId in igtbl_gridState)
		if (igtbl_gridState[gId].UniqueID == gridId || igtbl_gridState[gId].ClientID == gridId)
	{
		grid = igtbl_gridState[gId];
		break;
	}
	return grid;
}

function igtbl_getBandById(tagId)
{
	if (!tagId)
		return null;
	var parts = tagId.split("_");
	var gridId = parts[0];
	var el = igtbl_getElementById(tagId);
	var bandIndex = igtbl_getBandNo(el);
	var objTypeId = parts[1];

	if (objTypeId == "c" && el && el.tagName == "TH")
	{
		bandIndex = parts[2];
	}
	if (!igtbl_getGridById(gridId))
		return null;
	var grid = igtbl_getGridById(gridId);
	return grid.Bands[bandIndex];
}

function igtbl_getColumnById(tagId)
{
	if (!tagId)
		return null;
	var parts = tagId.split("_");
	var bandIndex = parts.length - 2;
	var gridId = parts[0];
	var objTypeId = parts[1];
	var el = igtbl_getElementById(tagId);

	if (objTypeId == "anc" && el && el.tagName == "TD")
	{
		bandIndex = igtbl_getBandById(tagId).Index;
	}
	else
		if (objTypeId == "flc" && el.tagName == "TD") 
	{
		bandIndex = igtbl_getBandById(tagId).Index;
	}
	else
		if (objTypeId == "rc" && el && el.tagName == "TD")
	{
		bandIndex = igtbl_getBandById(tagId).Index;
	}
	else if (objTypeId == "cf")
	{
		if (el && el.tagName != "TH")
			return null;
		bandIndex = parts[2];
	}
	else if (objTypeId == "cg")
	{
		if (el && el.tagName !=
			"TH"
		)
			return null;
		bandIndex = parts[2];
	}
	else if (objTypeId == "c")
	{
		if (el && el.tagName != "TH")
			return;
		bandIndex = parts[2];
	}
	else
		return null;

	if (!igtbl_getGridById(gridId))
		return null;
	var grid = igtbl_getGridById(gridId);
	var band = grid.Bands[bandIndex];
	var colIndex = parts[parts.length - 1];
	return band.Columns[colIndex];
}

function igtbl_getRowById(tagId)
{
	if (!tagId)
		return null;
	var parts = tagId.split("_");
	
	var rowTypeId = parts[1];
	var gridId = parts[0];
	var row = null;
	var isGrouped = false;
	
	var gridIdStore = gridId;
	if (rowTypeId == "anfr")
	{
		row = igtbl_getWorkRow(igtbl_getElementById(tagId).parentNode.parentNode.parentNode.parentNode.parentNode);
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
		}
	}

	if (row == null && rowTypeId == "grc")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;
		if (!row || !row.getAttribute("groupRow"))
			row = null;
		isGrouped = true;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "sgr")
	{
		row = igtbl_getWorkRow(igtbl_getElementById(tagId));
		if (!row || !row.getAttribute("groupRow"))
			row = null;
		isGrouped = true;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}

	if (row == null && rowTypeId == "nfr")
	{
		row = igtbl_getWorkRow(igtbl_getElementById(tagId).parentNode.parentNode.parentNode.parentNode.parentNode);
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "anr")
	{
		row = igtbl_getElementById(tagId);
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "anl")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "anc")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;
		
		if (row && row.id.substr(0, gridId.length + 5) == gridId + "_anfr")
			do
		{
			row = row.parentNode
		} while (row && row.tagName != "TR");
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "gr")
	{
		row = igtbl_getElementById(tagId);
		if (!row || !row.getAttribute("groupRow"))
			row = null;
		isGrouped = true;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "rh")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.previousSibling;
		if (!row || !row.getAttribute("hiddenRow"))
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "rc")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;

		
		if (row && row.id.substr(0, gridId.length + 1) == gridId.substr(0, gridId.length - 2) + "_nfr")
			do
		{
			row = row.parentNode
		} while (row && row.tagName != "TR");
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "r")
	{
		row = igtbl_getElementById(tagId);
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "l")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;
		if (!row || row.tagName != "TR")
			row = null;
		
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null && rowTypeId == "t")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode.parentNode.previousSibling;
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}

	}
	
	if (row == null && rowTypeId == "flc")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;

		if (row && row.id.substr(0, gridId.length + 5) == gridId + "_flfr")
			do
		{
			row = row.parentNode
		} while (row && row.tagName != "TR");
		if (!row || row.tagName != "TR")
			row = null;

		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	
	if (row == null && rowTypeId == "flfr")
	{
		row = igtbl_getWorkRow(igtbl_getElementById(tagId).parentNode.parentNode.parentNode.parentNode.parentNode);
		if (!row || row.tagName != "TR")
			row = null;

		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	
	if (row == null && rowTypeId == "fll")
	{
		row = igtbl_getElementById(tagId);
		if (typeof (row) != "undefined" && row)
			row = row.parentNode;
		if (!row || row.tagName != "TR")
			row = null;

		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	
	if (row == null && rowTypeId == "flr")
	{
		row = igtbl_getElementById(tagId);
		if (!row || row.tagName != "TR")
			row = null;
		if (row && !_validateGrid(gridId))
		{
			row = null
			gridId = gridIdStore;
			isGrouped = false;
		}
	}
	if (row == null)
		return null;
	var gs = igtbl_getGridById(gridId);
	if (!gs)
		return null;
	
	if (typeof (row.Object) != "undefined" && row.Object.Band)
		return row.Object;
	else
	{
		parts = new Array();
		while (true)
		{
			row = igtbl_getWorkRow(row, gridId);
			var level = -1;

			var bandZero = gs.Bands[0];
			if (gs.Bands.length == 1 && !bandZero.IsGrouped)
			{
				level = row.sectionRowIndex;
				if (!gs.StatHeader && (bandZero.AddNewRowVisible == 1 && bandZero.AddNewRowView == 1
				
					|| bandZero.FilterRowView == 1 && bandZero.FilterUIType == 1
				  ))
					level--;
			}
			else
				for (var i = 0; i < row.parentNode.childNodes.length; i++)
			{
				var r = row.parentNode.childNodes[i];
				if (!r.getAttribute("hiddenRow")
						&& !r.getAttribute("addNewRow")
                        && !r.getAttribute("filterRow")
					)
					level++;
				if (r == row)
					break;
			}
			parts[parts.length] = level;
			if (row.parentNode.parentNode.id == gs.Element.id)
				break;
			row = row.parentNode.parentNode.parentNode.parentNode.previousSibling;
		}
		
		if (parts.length > 1)
			parts = parts.reverse();
		var rows = gs.Rows;
		for (var i = 0; i < parts.length; i++)
		{
			row = rows.getRow(parseInt(parts[i], 10), row.Element ? null : row);
			if (row && row.Expandable && i < parts.length - 1)
				rows = row.Rows;
			else if (i < parts.length - 1)
			{
				row = null;
				break;
			}
		}
		if (!row)
			return null;
		delete parts;
		row.Element.Object = row;
		return row;
	}
}

function igtbl_getCellById(tagId)
{
	if (!tagId)
		return null;
	var parts = tagId.split("_");
	var gridId = parts[0];
	var cellTypeId = parts[1];

	if (cellTypeId != "rc")
	{
		if (cellTypeId != "anc")
		
			if (cellTypeId != "flc")

			return null;
	}
	var gs = igtbl_getGridById(gridId);
	if (!gs)
		return null;
	
	var row = igtbl_getRowById(igtbl_getRowIdFromCellId(tagId));
	if (!row)
		return null;
	var column = row.Band.Columns[parseInt(parts[parts.length - 1], 10)];
	return row.getCellByColumn(column);
}
function igtbl_getRowIdFromCellId(id)
{
	if (id == null || id.length == 0) return;
	var rowIdAr = id.split("_");
	switch (rowIdAr[1])
	{
		case ("rc"):
			rowIdAr[1] = "r";
			break;
		case ("anc"):
			rowIdAr[1] = "anr";
			break;
		 
		case ("flc"):
			rowIdAr[1] = "flr";
			break;
	}

	return rowIdAr.slice(0, rowIdAr.length - 1).join("_");
}
function igtbl_getCellByElement(td)
{
	td = igtbl_dom.find.parentByTag(td, "TD");
	if (!td) return null;
	if (td.id)
		return igtbl_getCellById(td.id);
	var tr = td.parentNode;
	var row = null;
	while (!row && tr)
	{
		if (tr.tagName == "TR" && tr.id)
			row = igtbl_getRowById(tr.id);
		tr = tr.parentNode;
	}
	if (row)
	{
		if (td.id)
			return igtbl_getCellById(td.id);
		while (td.parentNode && (td.parentNode != row.Element && td.parentNode != row.nfElement))
			td = td.parentNode;
		if (td.parentNode && td.tagName == "TD" && td.id)
			return igtbl_getCellById(td.id);
	}
	return null;
}

function igtbl_getColumnNo(gn, cell)
{
	if (cell)
	{
		var column = igtbl_getColumnById(cell.id);
		if (column)
			return column.Index;
		else
			return -1;
	}
}

function igtbl_getBandNo(cell)
{
	if (!cell)
		return -1;
	var tbl = cell;
	while (tbl && !tbl.getAttribute("bandNo"))
		tbl = tbl.parentNode;
	if (tbl)
		return parseInt(tbl.getAttribute("bandNo"));
	return -1;
}

function igtbl_getFirstRow(row)
{
	if (row.getAttribute("groupRow"))
		return row.childNodes[0].childNodes[0].childNodes[0].rows[0];
	else
		return row;
}

function igtbl_getWorkRow(row, gridId)
{
	if (!row) return;
	if (row.getAttribute("groupRow"))
	{
		var id = row.id.split("_");
		if (!gridId)
		{
			if (id[1] == "sgr")
				return row.parentNode.parentNode.parentNode.parentNode;
			else
				return row;
		}
		else
		{
			var rowId = id[1];
			if (rowId == "sgr")
				return row.parentNode.parentNode.parentNode.parentNode;
			else
				return row;
		}

	}
	else
		return row;
}

function igtbl_getColumnByCellId(cellID)
{
	var cell = igtbl_getCellById(cellID);
	if (!cell)
		return null;
	if (cell.Band.Grid.UseFixedHeaders && !cell.Column.getFixed())
	{
		var tbl;
		if (cell.Band.Index == 0 && !cell.Band.IsGrouped && cell.Band.ColHeadersVisible == 1 && (cell.Band.Grid.StationaryMargins == 1 || cell.Band.Grid.StationaryMargins == 3))
			tbl = cell.Band.Grid.StatHeader.Element.parentNode;
		else
		{
			tbl = cell.Element;
			while (tbl != null && (tbl.tagName != "TABLE" || !tbl.id))
				tbl = tbl.parentNode;
		}
		if (tbl)
		{
			thCells = tbl.childNodes[1].rows[0].cells[cell.Element.parentNode.parentNode.parentNode.parentNode.parentNode.cellIndex].firstChild.firstChild.rows[0].cells;
			var i = 0;
			while (i < thCells.length && thCells[i].cellIndex != cell.Element.cellIndex)
				i++;
			if (i < thCells.length)
				return thCells[i];
		}
		return null;
	}
	if (cell.Band.Index == 0 && !cell.Band.IsGrouped && cell.Band.ColHeadersVisible == 1 && (cell.Band.Grid.StationaryMargins == 1 || cell.Band.Grid.StationaryMargins == 3))
		return igtbl_getElemVis(cell.Band.Grid.StatHeader.Element.rows[0].cells, cell.Element.cellIndex);
	if (cell.Element.parentNode.parentNode.parentNode.childNodes[1].tagName == "THEAD")
		return igtbl_getElemVis(cell.Element.parentNode.parentNode.parentNode.childNodes[1].rows[0].cells, cell.Element.cellIndex);
	return null;
}

function igtbl_bandNoFromColId(colId)
{
	var s = colId.split("_");
	if (s.length < 3)
		return null;
	return parseInt(s[s.length - 2]);
}

function igtbl_colNoFromColId(colId)
{
	var s = colId.split("_");
	if (s.length < 3)
		return null;
	return parseInt(s[s.length - 1]);
}

function igtbl_colNoFromId(id)
{
	if (!id)
		return null;
	var s = id.split("_");
	if (s.length == 0)
		return null;
	return parseInt(s[s.length - 1]);
}

function igtbl_isCell(itemName)
{
	var parts = itemName.split("_");
	
	return (parts[1] == "rc" || parts[1] == "anc");
}

function igtbl_isColumnHeader(itemName)
{
	var parts = itemName.split("_");
	
	return parts[1] == "c";
}


function igtbl_isColumnFooter(itemName)
{
	var parts = itemName.split("_");
	return parts[1] == "f";
}

function igtbl_isRowLabel(itemName)
{
	var parts = itemName.split("_");
	
	return parts[1] == "l";
}


function igtbl_isTemplatedElement(element)
{
	if (element.imgType) return false;

	// is the element inside a column?
	var column, columnElement = element;
	while (columnElement && !column)
	{
		column = igtbl_getColumnById(columnElement.id);
		if (!column)
			columnElement = columnElement.parentNode;
	}
	if (!column || column.TemplatedColumn == 0 || !columnElement.id || columnElement == element) return false;

	if (igtbl_isColumnHeader(columnElement.id))
	{
		return (column.TemplatedColumn & 1) == 1;
	}
	if (igtbl_isCell(columnElement.id))
	{
		return (column.TemplatedColumn & 2) == 2;
	}
	if (igtbl_isColumnFooter(columnElement.id))
	{
		return column.TemplatedColumn & 4;
	}
	return false;
}

function igtbl_isChild(gn, e)
{
	if (!e) return false;
	var ge = igtbl_getElementById(gn + "_main");
	var p = e.parentNode;
	while (p && p != ge)
		p = p.parentNode;
	return p != null;
}


function igtbl_getParentCell(element)
{
	if (element && element.parentNode)
	{
		var parentElement = element.parentNode;
		while (parentElement != null)
		{
			if (parentElement.id)
			{
				var parentCell = igtbl_getCellById(parentElement.id);
				if (parentCell)
					return parentCell
			}
			parentElement = parentElement.parentNode;
		}
	}
	return null;
}

function igtbl_getCurCell(se)
{
	var c = null;
	while (se && !c)
		if (se.tagName == "TD")
		c = se;
	else
		se = se.parentNode;
	return c;
}

function igtbl_shGetElemByCol(col)
{
	if (!col.hasCells())
		return null;
	var j = 0;
	var cols = col.Band.Columns;
	for (var i = 0; i < col.Index; i++)
	{
		if (cols[i].hasCells())
			j++;
	}
	
	var headerElem = null;
	if (col.Band.Grid.UseFixedHeaders)
	{
		var childNodes = this.Element.childNodes[0].childNodes;
		childNodes = childNodes[childNodes.length - 1].childNodes[0].childNodes[0].childNodes[1].childNodes[0].childNodes;
		
		for (var nodesLen = 0; nodesLen < childNodes.length; nodesLen++)
		{
			if (childNodes[nodesLen].id == col.Id)
			{
				headerElem = childNodes[nodesLen];
				break;
			}
		}
	}
	if (!headerElem)
		headerElem = this.Element.childNodes[0].childNodes[j + col.Band.firstActiveCell];
	return headerElem;
}

function igtbl_sfGetElemByCol(col)
{
	if (!col.hasCells())
		return null;
	var j = 0;
	for (var i = 0; i < col.Index; i++)
	{
		if (col.Band.Columns[i].hasCells())
			j++;
	}
	return this.Element.childNodes[0].childNodes[j + col.Band.firstActiveCell];
}

function igtbl_getCellsByColumn(columnId)
{
	var c = igtbl_getDocumentElement(columnId);
	if (!c) return;
	if (!c.length) c = [c];
	var cells = [];
	var colSplit = columnId.split("_");
	var colIndex = parseInt(colSplit[colSplit.length - 1], 10);
	for (var k = 0; k < c.length; k++)
	{
		var tbody = c[k].parentNode;
		while (tbody && tbody.tagName != "THEAD" && tbody.tagName != "TABLE")
			tbody = tbody.parentNode;
		if (!tbody || tbody.tagName == "TABLE")
			continue;
		tbody = tbody.nextSibling;
		if (!tbody)
			continue;
		for (var i = 0; i < tbody.rows.length; i++)
		{
			if (tbody.rows[i].getAttribute("hiddenRow"))
				continue;
			var cell = tbody.rows[i].cells[c[k].cellIndex];
			while (cell)
			{
				var cellSplit = cell.id.split("_");
				var cellIndex = parseInt(cellSplit[cellSplit.length - 1], 10);
				if (cellIndex == colIndex)
					break;
				cell = cell.nextSibling;
			}
			if (cell)
				cells[cells.length] = cell;
		}
	}
	return cells;
}
function igtbl_gridSortColumn(gn, colId, shiftKey)
{
	var gs = igtbl_getGridById(gn);
	gs._isSorting = true;
	gs.sortColumn(colId, shiftKey);
	if (gs.sortingColumn && gs.oldColCursor)
		gs.sortingColumn.style.cursor = gs.oldColCursor;
	gs.stopHourGlass();
	gs._hidePI();
	delete gs._isSorting;
	if (gs.NeedPostBack)
		igtbl_doPostBack(gn, "shiftKey:" + shiftKey.toString());
}



function igtbl_resizeColumn(gn, colId, width)
{
	var gs = igtbl_getGridById(gn);
	if (!gs)
		return false;
	var col = igtbl_getColumnById(colId);
	if (!col)
		return false;
	return col.setWidth(width);
}

function igtbl_setActiveCell(gn, cell, force)
{
	var g = igtbl_getGridById(gn);
	if (g)
		g.setActiveCell(cell ? igtbl_getCellById(cell.id) : null, force);
	return;
}

function igtbl_setActiveRow(gn, row, force)
{
	var g = igtbl_getGridById(gn);
	if (g)
		g.setActiveRow(row ? igtbl_getRowById(row.id) : null, force);
	return;
}

function igtbl_pageGrid(evnt, gn, pageNo)
{
	var g = igtbl_getGridById(gn);
	if (!g || !g.goToPage) return;
	g.goToPage(pageNo);
	
	igtbl_cancelEvent(evnt);
}

function igtbl_inEditMode(gn)
{
	var g = igtbl_getGridById(gn);
	if (g && g._cb) return g._editorCurrent != null;
	if (g.editorControl && g.editorControl.getVisible())
		return true;
	var sel = igtbl_getElementById(gn + "_vl");
	if (sel && sel.style.display == "")
		return true;
	var tb = igtbl_getElementById(gn + "_tb");
	if (tb && tb.style.display == "")
		return true;
	var ta = igtbl_getElementById(gn + "_ta");
	if (ta && ta.style.display == "")
		return true;
	return false;
}

function igtbl_saveChangedCell(gs, cell, value)
{
	if (typeof (gs.ChangedRows[cell.Row.Element.id]) == "undefined")
		gs.ChangedRows[cell.Row.Element.id] = new Object();
	if (cell.Element)
		gs.ChangedRows[cell.Row.Element.id][cell.Element.id] = true;
	gs._recordChange("ChangedCells", cell, value);
}

function igtbl_endCustomEdit()
{
	if (arguments.length < 3)
		return;
	var oEditor = arguments[0];
	var oEvent = arguments[arguments.length - 2];
	var oThis = arguments[arguments.length - 1];
	var key = (oEvent && oEvent.event) ? oEvent.event.keyCode : 0;
	if (oThis) oThis._lastKey = key;
	if (oEvent && typeof (oEvent.event) != "undefined" && key != 9 && key != 13 && key != 27 && key != 0)
		return;
	var se = null;
	if (oEditor.Element)
		se = oEditor.Element;
	if (se != null)
	{
		if (se.getAttribute("noOnBlur"))
			return igtbl_cancelEvent(oEvent.event);
		if (se.getAttribute("editorControl"))
		{
			if (!oEditor.getVisible())
				return;
			var cell = igtbl_getElementById(se.getAttribute("currentCell"));
			if (!cell)
				return;
			var gs = oThis;
			var cellObj = igtbl_getCellById(cell.id);
			if (key == 27)
				oEditor.setValue(cellObj.getValue(), false);
			if (typeof (oEditor.getValue()) != "undefined")
				cellObj.setValue(oEditor.getValue());
			if (igtbl_fireEvent(gs.Id, gs.Events.BeforeExitEditMode, "(\"" + gs.Id + "\",\"" + cell.id + "\")") == true)
			{
				if (!gs._exitEditCancel && !gs._insideSetActive)
				{
					gs._insideSetActive = true;
					igtbl_setActiveCell(gs.Id, cell);
					gs._insideSetActive = false;
				}
				gs._exitEditCancel = true;
				return true;
			}
			oEditor.setVisible(false);
			oEditor.removeEventListener("blur", igtbl_endCustomEdit);
			oEditor.removeEventListener("keydown", igtbl_endCustomEdit);
			gs._exitEditCancel = false;
			se.removeAttribute("currentCell");
			se.removeAttribute("oldInnerText");
			gs.editorControl = null;
			se.removeAttribute("editorControl");
			igtbl_fireEvent(gs.Id, gs.Events.AfterExitEditMode, "(\"" + gs.Id + "\",\"" + cell.id + "\");");
			if (key == 9 || key == 13)
			{
				var res = null;
				if (typeof (igtbl_ActivateNextCell) != "undefined")
				{
					if (oEvent.event.shiftKey && key == 9)
						res = igtbl_ActivatePrevCell(gs.Id);
					else
						res = igtbl_ActivateNextCell(gs.Id);
				}
				if (res && igtbl_getCellClickAction(gs.Id, cellObj.Column.Band.Index) == 1)
					igtbl_EnterEditMode(gs.Id);
				if (!res)
				{
					igtbl_blur(gs.Id);
					return;
				}
				igtbl_activate(gs.Id);
				oEvent.cancel = true;
			}
			else
				gs.alignGrid();
			igtbl_blur(gs.Id);
			if (gs.NeedPostBack)
				igtbl_doPostBack(gs.Id);
		}
	}
}

// ig_WebGrid.js
// Infragistics UltraWebGrid Script 
// Copyright (c) 2001-2007 Infragistics, Inc. All Rights Reserved.
var igtbl_lastActiveGrid = "";
var igtbl_isXHTML = document.compatMode == "CSS1Compat";
var testVariable = null;
var igtbl_sUnmaskedValue = "uV";
var igtbl_sigCellText = "iCT";
var igtbl_sigDataValue = "iDV";
var igtbl_isAtlas = false;
var igtbl_litPrefix = "";
function igtbl_initGrid(gridId
	, gridInitArray, bandsInitArray, colsInitArray, eventsInitArray, xmlInitProps, isInsideUpdatePanel, isInsideWARP
	, firefoxXml
)
{
	var grid = null;
	var gridElement = igtbl_getElementById("G_" + gridId);
	igtbl_isAtlas = typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined";
	
	var rm = null;
	if (igtbl_isAtlas && typeof Sys.WebForms == 'object' && typeof Sys.WebForms.PageRequestManager == 'function') try
	{
		rm = Sys.WebForms.PageRequestManager.getInstance();
	} catch (e) { }
	
	if (rm && !rm._ig_grid_onsubmit)
	{
		rm._ig_grid_onsubmit = rm._onsubmit;
		if (!rm._ig_grid_onsubmit)
			rm._ig_grid_onsubmit = 2;
		rm._onsubmit = function()
		{
			
			
			
			if (typeof igtbl_gridState == 'object') for (var id in igtbl_gridState) try
			{
				var o = igtbl_gridState[id];
				if (o && o.update)
					o.update();
			} catch (id) { }
			
			if (typeof this._ig_grid_onsubmit == 'function') try
			{
				if (this._ig_grid_onsubmit() === false)
					return false;
			} catch (i) { }
			
			return true;
		}
	}
	if (isInsideUpdatePanel) 
	{
		
		var metCur = false;
		for (var gi in igtbl_gridState)
		{
			var g = igtbl_getGridById(gi);
			if (g && metCur)
				g.GridIsLoaded = false;
			if (gridId == gi)
				metCur = true;
		}
	}
	grid = igtbl_getGridById(gridId);
	if (grid)
	{
		
		if (isInsideUpdatePanel && gridElement == grid.Element)
		{
			grid.GridIsLoaded = true; 
			return;
		}
		
		
		
		
		
		
		igtbl_unloadGrid(gridId, true);

		
		igtbl_clearGridsPost(grid);
	}
	var xml;
	if (firefoxXml)
	{
		var xmlDoc = igtbl_xml.createDocumentFromString(firefoxXml);
		if (xmlDoc) xml = xmlDoc.firstChild;
	}
	grid = new igtbl_Grid(gridElement, xml
		, gridInitArray, bandsInitArray, colsInitArray, eventsInitArray, xmlInitProps
		);
	
	if (typeof (igtab_selectTab) != "undefined" && igtab_selectTab && igtab_selectTab != igtbl_tabChanges)
	{
		igtbl_oldSelectTab = igtab_selectTab;
		igtab_selectTab = igtbl_tabChanges;
	}
	var scrollLeft = grid._AddnlProps[9];
	var scrollTop = grid._AddnlProps[10];
	if (grid.LoadOnDemand != 3 || grid.XmlLoadOnDemandType == 2
		|| grid.XmlLoadOnDemandType == 4
	 )
	{
		if (scrollLeft > 0)
			grid._recordChange("ScrollLeft", grid, scrollLeft);
		if (scrollTop > 0)
			grid._recordChange("ScrollTop", grid, scrollTop);
	}
	
	var sortedColsIds = grid._AddnlProps[8];
	if (sortedColsIds)
		grid.addSortColumn(sortedColsIds);
	
	if (grid.Rows.hasRowFilters())
		grid.Rows.reapplyRowStyles();
	var expRowsIds = grid._AddnlProps[3];
	for (var i = 0; i < expRowsIds.length; i++)
	{
		var id;
		if (!xml)
		{
			var splitId = expRowsIds[i].split(";");
			igtbl_stateExpandRow(grid.Id, null, true, splitId[0], splitId[1]);
			id = splitId[0];
		}
		else
		{
			id = expRowsIds[i];
			id = id.split(";")[0];
			igtbl_toggleRow(grid.Id, id, true);
		}
		
		var row = igtbl_getRowById(id);
		if (row && row.Rows && row.Rows.hasRowFilters())
			row.Rows.reapplyRowStyles();
	}
	var selRowsIds = grid._AddnlProps[4];
	for (i = 0; i < selRowsIds.length; i++)
		igtbl_selectRow(grid.Id, selRowsIds[i]);
	var selCellsIds = grid._AddnlProps[5];
	for (i = 0; i < selCellsIds.length; i++)
		igtbl_selectCell(grid.Id, selCellsIds[i]);
	var selColsIds = grid._AddnlProps[13];
	for (i = 0; selColsIds && i < selColsIds.length; i++)
		igtbl_selectColumn(grid.Id, selColsIds[i]);
	var activeCellId = grid._AddnlProps[6];
	var activeRowId = grid._AddnlProps[7];
	var de = grid.getDivElement();
	var mainGrid = grid.MainGrid;

	var percentageHeight = mainGrid.style.height.indexOf("%") != -1;
	
	if (typeof (igtbl_oldOnBodyResize) == "undefined")
	{
		igtbl_oldOnBodyResize = igtbl_addEventListener(window, "resize", igtbl_onBodyResize, false);
	}
	
	if (!grid.UseFixedHeaders)
	{
		
		if (scrollLeft)
			igtbl_scrollLeft(de, scrollLeft);
		grid.alignStatMargins();
	}
	if (!mainGrid.style.height && de.clientHeight != de.scrollHeight)
	{
		var scDiv = document.createElement("DIV");
		scDiv.id = grid.Element.id + "_scd";
		scDiv.innerHTML = "&nbsp;";
		scDiv.style.height = de.scrollHeight - de.clientHeight + 1;
		de.appendChild(scDiv);
		de.style.overflowY = "hidden";
		de.setAttribute("scdAdded", "true");
	}
	grid.alignDivs(scrollLeft);
	if (grid.UseFixedHeaders
	|| grid.XmlLoadOnDemandType != 0
	&& grid.XmlLoadOnDemandType != 4
	)
	{
		if (grid.StatHeader)
			grid.StatHeader.ScrollTo(scrollLeft); 
		if (grid.StatFooter)
			grid.StatFooter.ScrollTo(scrollLeft); 
		grid.alignStatMargins();
        grid.correctScrollWidthForFixedHeaders(); 
	}
	
	grid._alignFilterRow(grid.Rows);
	if (grid.XmlLoadOnDemandType == 2)
		de.setAttribute("noOnScroll", true);
	if (scrollTop)
		igtbl_scrollTop(de, scrollTop);
	if (scrollTop || scrollLeft)
	{
		var st = de.scrollTop.toString();
		de.setAttribute("noOnScroll", "true");
		de.setAttribute("oldSL", de.scrollLeft.toString());
		de.setAttribute("oldST", st);
		grid.cancelNoOnScrollTimeout = window.setTimeout("igtbl_cancelNoOnScroll('" + grid.Id + "')", 100);
	}
	if (grid.XmlLoadOnDemandType == 2)
		de.removeAttribute("noOnScroll");
	if (activeCellId)
	{
		grid.setActiveCell(igtbl_getCellById(activeCellId));
		var cell = grid.oActiveCell;
		if (cell)
		{
			cell.scrollToView();
			if (cell.Band.getSelectTypeCell() == 3)
				grid.Element.setAttribute("startPointCell", cell.Element.id);
		}
	}
	else if (activeRowId)
	{
		grid.setActiveRow(igtbl_getRowById(activeRowId));
		var row = grid.oActiveRow;
		if (row)
		{
			row.scrollToView();
			if (row.Band.getSelectTypeRow() == 3)
				grid.Element.setAttribute("startPointRow", row.Element.id);
		}
	}
	grid._cb = typeof igtbl_editEvt == "function";
	

	
	grid._fromServerActiveRow = grid.oActiveRow;

	
	ig_csom.addCBEventListener("igtbl_getGridById('" + gridId + "')");

	var rowsRetrieved = grid._AddnlProps[15];
	if (rowsRetrieved >= 0)
	{
		grid.RowsRetrieved = rowsRetrieved;
		grid._recordChange("RowsRetrieved", grid, rowsRetrieved);
	}

	if (grid.UseFixedHeaders && grid.Bands[0] && grid.Bands[0].HasHeaderLayout)
	{		
		
		var tHead;
		if (grid.StatHeader)
		{
			tHead = grid.StatHeader.Element;
		}
		else
		{
			tHead = grid.Element.tHead;
		}
		var tBodyBounds = igtbl_getAbsBounds(grid.Element.tBodies[0]);
		var tHeadBounds = igtbl_getAbsBounds(tHead);
		
		
		
		var nfhHeight = tHead.offsetHeight;
		
		
		
		tHead.rows[0].lastChild.style.height = (nfhHeight > 0) ? nfhHeight + "px" : "auto";
		var c = 0, someFixed = false;
		for (c = 0; c < grid.Bands[0].Columns.length; c++)
		{
			someFixed |= grid.Bands[0].Columns[c].getFixed();
			if (someFixed)
			{
				break;
			}
		}

		if (igtbl_isXHTML && ig_csom.IsIE7Compat && !someFixed) 
		{
			
			var divContent = grid.getDivElement().firstChild;
			var percentageWidth = divContent.style.width.indexOf("%") != -1;
			var drs = igtbl_getElementById(gridId + "_drs");
			if (!percentageWidth && drs)
			{
				divContent.style.width = drs.scrollWidth + "px";
			}
		}		
	}

	grid.GridIsLoaded = true;
	igtbl_fireEvent(grid.Id, grid.Events.InitializeLayout, '("' + grid.Id + '");');

	
	igtbl_browserWorkarounds.addActiveElementTracking();
	try
	{
		
		if (!document.activeElement && (activeCellId || activeRowId))
			igtbl_activate(gridId);
	}
	catch (e) { ; }
	
	if (typeof igtbl_currentEditTempl == 'string')
		igtbl_currentEditTempl = null;
	
	
	igtbl_getElementById(grid.ClientID).control = grid;
	return grid;
}

function igtbl_initActivation(aa)
{
	this.AllowActivation = aa[0];
	this.BorderColor = aa[1];
	this.BorderStyle = aa[2];
	this.BorderWidth = aa[3];
	this.BorderDetails = new Object();
	var bd = this.BorderDetails;
	bd.ColorLeft = aa[4][0];
	bd.ColorTop = aa[4][1];
	bd.ColorRight = aa[4][2];
	bd.ColorBottom = aa[4][3];
	bd.StyleLeft = aa[4][4];
	bd.StyleTop = aa[4][5];
	bd.StyleRight = aa[4][6];
	bd.StyleBottom = aa[4][7];
	bd.WidthLeft = aa[4][8];
	bd.WidthTop = aa[4][9];
	bd.WidthRight = aa[4][10];
	bd.WidthBottom = aa[4][11];
	this.getValue = function(where, what)
	{
		var res = "";
		if (where)
			res = this.BorderDetails[what + where];
		if (res == "" || res == "NotSet")
			res = this["Border" + what];
		return res;
	}
	this.hasBorderDetails = function()
	{
		var bd = this.BorderDetails;
		if (bd.ColorLeft || bd.ColorTop || bd.ColorRight || bd.ColorBottom ||
			bd.StyleLeft || bd.StyleTop || bd.StyleRight || bd.StyleBottom ||
			bd.WidthLeft || bd.WidthTop || bd.WidthRight || bd.WidthBottom)
			return true;
		return false;
	}
}

function igtbl_initGroupByBox(grid)
{
	this.Element = igtbl_getElementById(grid.Id + "_groupBox");
	this.pimgUp = igtbl_getElementById(grid.Id + "_pimgUp");
	if (this.pimgUp)
		this.pimgUp.style.zIndex = grid._getZ(100000, 1);
	this.pimgDn = igtbl_getElementById(grid.Id + "_pimgDn");
	if (this.pimgDn)
		this.pimgDn.style.zIndex = grid._getZ(100000, 1);
	this.postString = "";
	this.moveString = "";
	if (this.Element)
	{
		this.groups = new Array();
		var gt = this.Element.childNodes[0];
		if (gt.tagName == "TABLE")
			for (var i = 0; i < gt.rows.length; i++)
			this.groups[i] = new igtbl_initGroupMember(gt.rows[i].cells[i]);
	}
}

function igtbl_initGroupMember(e)
{
	var d = e;
	if (!d.getAttribute("groupInfo"))
		return null;
	this.Element = d;
	this.groupInfo = d.getAttribute("groupInfo").split(":");
	this.groupInfo[1] = parseInt(this.groupInfo[1], 10);
	if (this.groupInfo[0] == "col")
		this.groupInfo[2] = parseInt(this.groupInfo[2], 10);
}

function igtbl_lineupHeaders(colId, band, secondCall) {
	var gs = band.Grid;
	var te = gs.Element;
	var cg = new Array();	
	var cells = new Array();
	var stat = false;
	
	if (band.Index == 0 && !band.IsGrouped && gs.StationaryMargins > 0)
	{		
		cg[0] = te.childNodes[0];
		
		
		if (ig_shared.IsIEStandards)
			cells[0] = te.childNodes[1].lastChild;
		if (gs.StatHeader)
		{
			cg[1] = gs.StatHeader.Element.previousSibling;
			
			if (ig_shared.IsIEStandards)
			{
				
				cells[1] = gs.StatHeader.Element.lastChild;
				
				if (te.childNodes[1].style.display == "none")
					cells[0] = cells[1];		
			}
		}
		if (gs.StatFooter)
		{
			if ((gs.Rows.AddNewRow && band.AddNewRowView == 2)
				|| (gs.Rows.FilterRow && gs.Rows.FilterRow.isFixedBottom()))
			{
				cg[cg.length] = gs.StatFooter.Element.previousSibling.previousSibling;
				
				
				if (ig_shared.IsIEStandards)
					cells[cells.length] = gs.StatFooter.Element.previousSibling.lastChild;
			}
			else
			{
				cg[cg.length] = gs.StatFooter.Element.previousSibling;
				
				
				if (ig_shared.IsIEStandards)
					cells[cells.length] = gs.StatFooter.Element.lastChild;
			}
		}
		stat = true;
	}
	else
	{
		var e = igtbl_getDocumentElement(colId);
		if (e && e.length)
		{
			for (var i = 0; i < e.length; i++)
			{
				cg[i] = e[i].parentNode.parentNode.previousSibling;
				
				if (ig_shared.IsIEStandards)
					cells[i] = e[i].parentNode;
			}
		}
		else if (e && e.parentNode.parentNode.previousSibling)
		{
			cg[0] = e.parentNode.parentNode.previousSibling;
			
			if (ig_shared.IsIEStandards)
				cells[0] = e.parentNode;
		}
	}
	if (cg.length > 0)
	{		
		for (var j = 0; j < cg.length; j++)
		{
			var hasPercW = false;
			for (var i = 0; cg[j] && i < cg[j].childNodes.length && !hasPercW; i++)
			{
				var w = cg[j].childNodes[i].width.toString();
				if (!w || w.substr(w.length - 1) == "%")
					hasPercW = true;
			}

			if (hasPercW)
			{
				var e = igtbl_getDocumentElement(colId);
				if (e && e.length)
					e = e[0];
				var hiddenColCount = 0;
				for (var i = 0; i < cg[j].childNodes.length; i++)
				{
					var colEl = cg[j].childNodes[i];
					colEl.oldWidth = colEl.offsetWidth;

					
					
					if (ig_shared.IsIEStandards && colEl.style.display != "none" && cells[j].childNodes.length > i)
					{
						var column = igtbl_getColumnById(cells[j].childNodes[i + hiddenColCount].id);
						while (column && column.Hidden && i + hiddenColCount < cells[j].childNodes.length)
						{
							hiddenColCount++;
							column = igtbl_getColumnById(cells[j].childNodes[i + hiddenColCount].id);
						}
						var cellIndex = i + hiddenColCount;
						colEl.oldWidth = cells[j].childNodes[cellIndex].offsetWidth;
					}
					if (igtbl_isXHTML && ig_shared.IsIE7Compat && e)
					{
						colEl.oldWidth -= igtbl_parseInt(e.currentStyle.paddingLeft);
						colEl.oldWidth -= igtbl_parseInt(e.currentStyle.paddingRight);
						colEl.oldWidth -= igtbl_parseInt(e.currentStyle.borderLeftWidth);
						colEl.oldWidth -= igtbl_parseInt(e.currentStyle.borderRightWidth);
					}

				}
			}			
			if (j > 0 && stat || gs.TableLayout)
			{				
				var pn = cg[j].parentNode;
				
				
				
					if (!pn.oldWidth)
						pn.oldWidth = pn.style.width;
					if (pn.oldWidth)
					{
						var e = igtbl_getDocumentElement(colId);
						if (e && e.length)
							e = e[0];
						var colGWidth = 0;
						for (var cs = 0; cs < cg[j].childNodes.length; cs++)
						{
							var colEl = cg[j].childNodes[cs];
							if (!colEl.style.display)
							{
								var wdt = 0;
								if (colEl.oldWidth)
									colGWidth += (wdt = igtbl_parseInt(colEl.oldWidth));
								if (wdt==0)
									colGWidth += igtbl_parseInt(colEl.width);
								if (igtbl_isXHTML && ig_shared.IsIE7Compat && e)
								{
									colGWidth -= igtbl_parseInt(e.currentStyle.paddingLeft);
									colGWidth -= igtbl_parseInt(e.currentStyle.paddingRight);
									colGWidth -= igtbl_parseInt(e.currentStyle.borderLeftWidth);
									colGWidth -= igtbl_parseInt(e.currentStyle.borderRightWidth);
								}
							}
						}
						if (colGWidth)
						{
							if (band.Index == 0 && band.IsGrouped && gs.get("StationaryMarginsOutlookGroupBy") == "True" && gs.StationaryMargins > 0)
							{
								var groupedCols = band.SortedColumns.length;
								var gc = groupedCols - 1;
								while(gc>=0)
								{
									if (band.SortedColumns[gc].IsGroupBy)
										groupedCols--;
									gc--;
								}
								
							}
							pn.style.width = colGWidth + "px";

							
							if (ig_csom.IsIE7Compat)
							{								
								pn.style.paddingRight = "";
							}
						}
						else
							pn.style.width = "";
					}
				
				}				
			var hiddenColCount = 0;	
			for (var i = 0; i < cg[j].childNodes.length; i++)
			{				
				if (cg[j].childNodes[i].oldWidth)
				{
					if (cg[j].nextSibling)
					{
						var co = igtbl_getElemVis(cg[j].nextSibling.firstChild.childNodes, i + hiddenColCount);
						var column = igtbl_getColumnById(co.id);
						
						if (ig_shared.IsIEStandards && column && column.Hidden)
						{
							while (column && column.Hidden && (i + hiddenColCount < cg[j].childNodes.length))
							{
								hiddenColCount++;
								co = igtbl_getElemVis(cg[j].nextSibling.firstChild.childNodes, i + hiddenColCount);
								column = igtbl_getColumnById(co.id);
							}
						}
						if (column)
						{
							co.style.width = "";
							if (cg[j].childNodes[i].oldWidth > 0)
								co.width = cg[j].childNodes[i].oldWidth;
							
							
							column.setWidth(igtbl_parseInt(co.width));
							if (column.Node) column.Node.setAttribute(igtbl_litPrefix + "width", co.width);
						}
					}
					cg[j].childNodes[i].style.width = "";
					if (cg[j].childNodes[i].oldWidth > 0)
						cg[j].childNodes[i].width = cg[j].childNodes[i].oldWidth;
					cg[j].childNodes[i].oldWidth = null;
				}
			}			
		}
	}	
	igtbl_dispose(cg);
	delete cg;

	
	if (!secondCall && ig_shared.IsIE && !ig_shared.IsIEStandards && stat && gs.TableLayout && document.getElementById(band.Grid.Id + "_hdiv").firstChild.style.width != document.getElementById("G_" + band.Grid.Id).style.width)			
		igtbl_lineupHeaders(colId, band, true);

	
	var scrollDiv = document.getElementById(band.Grid.Id + "_div");
	if (scrollDiv && ig_shared.IsIE8 && scrollDiv.style.overflow == "auto")
	{
		scrollDiv.style.overflow = "hidden";
		scrollDiv.style.overflow = "auto";
	}
}



function igtbl_scrollToView(gn, child, childWidth, nfWidth, scrollDirection, childLeftAdj)
{
	
	if (!childLeftAdj)
		childLeftAdj = 0;
	if (!child)
		return;
	var gs = igtbl_getGridById(gn);
	var parent = gs.Element.parentNode;
	
	var drsParent = null; 
	var scrParent = parent;
	var addParentScrollLeft = false;	
	if (gs.UseFixedHeaders
		|| gs.XmlLoadOnDemandType != 0
	&& gs.XmlLoadOnDemandType != 4
	)
	{
		scrParent = gs._scrElem;
		
		
		if (child.tagName == "TD" || child.tagName == "TH")
		{
			var prnt = child;
			var i = 0;
			while (i < 6 && prnt && (prnt.tagName != "DIV" || !prnt.id || prnt.id.substr(prnt.id.length - 4, 4) != "_drs"))
			{
				i++;
				prnt = prnt.parentNode;
			}
			if (i < 6 && prnt)
				drsParent = prnt;
		}
		
		addParentScrollLeft = ((scrollDirection == 1 && ig_csom.IsFireFox) || ig_csom.IsIEStandards) ? true : false;		
	}
	if (scrParent.scrollWidth <= scrParent.offsetWidth && scrParent.scrollHeight <= scrParent.offsetHeight)
		return;
	
	var childLeft = igtbl_getLeftPos(child) + (ig_csom.IsIEStandards ? 0 : childLeftAdj);
	var parentLeft = igtbl_getLeftPos(drsParent ? drsParent : parent);

	var childTop = igtbl_getTopPos(child);
	var parentTop = igtbl_getTopPos(parent);

	

	
	

	var childRight = childLeft + child.offsetWidth;
	var childBottom = childTop + child.offsetHeight;
	
	
	
	var parentRight = scrParent ? (igtbl_getLeftPos(scrParent) + scrParent.clientWidth + (addParentScrollLeft ? scrParent.scrollLeft * (ig_csom.IsFireFox ? 1 : -1) : 0)) : (parentLeft + parent.clientWidth);
	
	
	if (ig_csom.IsIE7Compat && igtbl_isXHTML && scrParent && gs.UseFixedHeaders)
		parentRight -= scrParent.scrollLeft;
	var parentBottom = parentTop + parent.clientHeight;
	var hsw = parent.offsetHeight - parent.clientHeight; 
	var vsw = parent.offsetWidth - parent.clientWidth; 
	if (!scrollDirection || scrollDirection == 2)
	{
		if (childBottom > parentBottom)
		{
			
			if (childTop - (parentTop - childTop) > parentTop && childBottom - childTop < parentBottom - parentTop)
				igtbl_scrollTop(scrParent, scrParent.scrollTop + childBottom - parentBottom + hsw - 1);
			else
				igtbl_scrollTop(scrParent, scrParent.scrollTop + childTop - parentTop - 1);
		}
		if (childTop < parentTop)
			igtbl_scrollTop(scrParent, scrParent.scrollTop - (parentTop - childTop) - 1);
	}
	
	if (!scrollDirection || scrollDirection == 1)
	{
		
		if (typeof (nfWidth) != "undefined" && nfWidth !== null && (childLeft == childRight || childRight - childLeft < childWidth))
		{
			igtbl_scrollLeft(scrParent, nfWidth);
			return;
		}
		if (childRight > parentRight)
		{
			
			if (childLeft - (childRight - parentRight) > parentLeft && childRight - childLeft < parentRight - parentLeft)
				igtbl_scrollLeft(scrParent, scrParent.scrollLeft + childRight - parentRight + vsw);
			else
				igtbl_scrollLeft(scrParent, scrParent.scrollLeft + childLeft - parentLeft);
		}
		if (childLeft < parentLeft)
			igtbl_scrollLeft(scrParent, scrParent.scrollLeft - parentLeft + childLeft - vsw - 1);
		
		
		else if (gs.UseFixedHeaders && childLeft == parentLeft)
			igtbl_scrollLeft(scrParent, 0);
	}
}


function _validateGrid(gridId)
{
	return (igtbl_getGridById(gridId) != null);
}

function igtbl_needPostBack(gn)
{
	igtbl_getGridById(gn).NeedPostBack = true;
}

function igtbl_cancelPostBack(gn)
{
	igtbl_getGridById(gn).CancelPostBack = true;
}

function igtbl_moveBackPostField(gn, param)
{
	var gs = igtbl_getGridById(gn);
	gs.moveBackPostField = param;
}

function igtbl_updatePostField(gn, param)
{
}

function igtbl_doPostBack(gn, args)
{
	var gs = igtbl_getGridById(gn);

	if (gs.isLoaded() && !gs.CancelPostBack)
	{
		gs.GridIsLoaded = false;
		if (!args)
			args = "";
		window.setTimeout("var g=igtbl_getGridById('" + gn + "');if(g){g.GridIsLoaded=true;g.NeedPostBack=false;}", 1000); 

		
		if (gs._isComboGrid)
		{
			var comboId = igtbl_getElementById(gn).getAttribute("igComboId");
			if (comboId)
			{
				var oCombo = igcmbo_getComboById(comboId);
				if (oCombo)
					oCombo._returnContainer();
			}
		}
			
					
		if (ig_shared.IsSafari)
			__doPostBack(gs.UniqueID, args);
		else
		
			window.setTimeout("window.__doPostBack('" + gs.UniqueID + "','" + args + "');");
		return true;
	}
	return false;
}

function igtbl_unloadGrid(gn, self)
{
	if (typeof (self) == "undefined")
		self = false;
	var grid = igtbl_gridState[gn];
	
	if (!grid || !grid.Events) return;

	grid.Events.unload();
	grid.editorControl = null;
	
	grid.eReqType = null;
	grid.eReadyState = null;
	grid.eError = null;
	
	grid.eFeatureRowView = null;
	grid.eFilterRowType = null;
	grid.eRowFilterMode = null;
	grid.eClipboardError = null;
	grid.eClipboardOperation = null;
	grid.eFilterComparisonType = null;
	grid.eFilterComparisionOperator = null;

	
	if (grid.dragDropDiv)
	{
		var dragDropDiv = grid.dragDropDiv;
		dragDropDiv.parentNode.removeChild(dragDropDiv);
		grid.dragDropDiv = null;

		grid.GroupByBox.pimgUp.style.display = "none";
		grid.GroupByBox.pimgDn.style.display = "none";
	}

	
	for (var b = 0; b < grid.Bands.length; b++)
	{
		for (var c = 0; c < grid.Bands[b].Columns.length; c++)
			grid.Bands[b].Columns[c].hideValidators();
	}

	for (var i = 0; i < grid.Bands.length; i++)
		for (var j = 0; j < grid.Bands[i].Columns.length; j++)
	{
		
		var editor = grid.Bands[i].Columns[j].editorControl;
		if (editor)
		{
			var elem = editor.Element, old = editor._old_parent;
			if (elem && old && (old != elem.parentNode))
			{
				if (elem.parentNode != null)
					elem.parentNode.removeChild(elem);
				old.appendChild(elem);
				editor._old_parent = null;
			}
			
			if (editor._getContainer)
			{
				var container = editor._getContainer();
				if (container && old && container.parentNode && container.parentNode != old)
				{
					if (container.parentNode != null)
						container.parentNode.removeChild(container);
					old.appendChild(container);
				}
			}
			grid.Bands[i].Columns[j].editorControl = null;
		}
	}
	
	if (grid._editorArea)
	{
		if (grid._editorArea.parentNode)
			grid._editorArea.parentNode.removeChild(grid._editorArea);
		grid._editorArea = null;
	}
	if (grid._editorInput)
	{
		if (grid._editorInput.parentNode)
			grid._editorInput.parentNode.removeChild(grid._editorInput);
		grid._editorInput = null;
	}
	if (grid._editorList)
	{
		if (grid._editorList.parentNode)
			grid._editorList.parentNode.removeChild(grid._editorList);
		grid._editorList = null;
	}
	var f = grid._thisForm;
	if (!f)
		f = igtbl_getThisForm(grid.Element);
	if (f && self && !(typeof (f.igtblGrid) == "undefined" || f.igtblGrid == null))
	{
		var g = f.igtblGrid, tg = null;
		while (g && g != grid)
		{
			tg = g;
			g = g.oldIgtblGrid;
		}
		if (tg == null)
			f.igtblGrid = grid.oldIgtblGrid;
		else
			tg.oldIgtblGrid = grid.oldIgtblGrid;
		grid.oldIgtblGrid = null;
	}
	if (f && (!self || (typeof (f.igtblGrid) == "undefined" || f.igtblGrid == null)))
	{
		var old = grid.oldIgtblGrid;
		if (old)
		{
			
			grid.oldIgtblGrid = null;
			igtbl_unloadGrid(old.Id);
		}
		try
		{
			f.igtblGrid = null;
			if (grid._thisForm && grid._thisForm.removeEventListener)
				grid._thisForm.removeEventListener('submit', igtbl_submit, false);
			
			
			
			if (f.onsubmit == igtbl_submit)
			{
				f.onsubmit = f.igtbl_oldOnSubmit;
				f.igtbl_oldOnSubmit = null;
			}
			
			
			
			if (typeof (f.igtbl_oldSubmit) != "undefined" && f.igtbl_oldSubmit != null && f.submit == igtbl_formSubmit)
			{
				f.submit = f.igtbl_oldSubmit;
				f.igtbl_oldSubmit = null;
			}
			
			
			if (typeof (window._igtbl_doPostBackOld) != "undefined" && window._igtbl_doPostBackOld != null && window.__doPostBack == igtbl_submit)
			{
				window.__doPostBack = window._igtbl_doPostBackOld;
				window._igtbl_doPostBackOld = null;
			}
			
			window._igtbl_thisForm = null;
		}
		catch (ex)
		{
		}
	}

	grid.disposing = true;
	
	var state = igtbl_dom.find.rootNode(grid.StateChanges);
	var node = grid.Node;
	grid.event = null;
	igtbl_dispose(grid);
	igtbl_removeState(state);
	if (node) igtbl_xml.disposeDocument(node);
	delete node;
	delete igtbl_gridState[gn];
	delete state;
}


function igtbl_removeState(stateNode)
{
	
	if (!stateNode || !stateNode.childNodes)
		return;
		
	while (stateNode.childNodes.length > 0)
		igtbl_removeState(stateNode.childNodes[0]);

	if (stateNode.parentNode)
	{
		if (typeof (stateNode.parentNode.removeChild) != "undefined")
			stateNode.parentNode.removeChild(stateNode); // IE
		else
			stateNode.parentNode.removeNode(stateNode); // Firefox
	}

	// removeNode isn't enough to make the node deallocate in IE
	if (typeof (stateNode.outerHTML) != "undefined")
		stateNode.outerHTML = "";
}

function igtbl_dispose(obj)
{
	if (ig_csom.IsNetscape || ig_csom.IsNetscape6)
		return;
	for (var item in obj)
	{
		if (typeof (obj[item]) != "undefined" && obj[item] != null && !obj[item].tagName && !obj[item].disposing && typeof (obj[item]) != "string" && obj.hasOwnProperty(item) )
		{
			try
			{

				obj[item].disposing = true;
				igtbl_dispose(obj[item]);
			} catch (exc1) { ; }
		}
		try
		{
			delete obj[item];
		} catch (exc2)
		{
			return;
		}
	}
}


var igtbl_oldOnUnload;
var igtbl_bInsideOldOnUnload = false;
function igtbl_unload()
{
	
	igtbl_browserWorkarounds.removeActiveElementTracking();

	if (igtbl_oldOnUnload && !igtbl_bInsideOldOnUnload)
	{
		igtbl_bInsideOldOnUnload = true;
		igtbl_oldOnUnload();
		igtbl_bInsideOldOnUnload = false;
	}
	for (var gridId in igtbl_gridState)
	{
		try
		{
			if (typeof (document) !== 'unknown')
			{
				var p = igtbl_getElementById(gridId);
				p.value = ig_ClientState.getText(igtbl_gridState[gridId].ViewState);
			}
		}
		catch (e)
		{
		}
		if (igtbl_gridState[gridId].unloadGrid)
			igtbl_gridState[gridId].unloadGrid();
		else
			delete igtbl_gridState[gridId];
	}
}


if (typeof igtbl_gridState != "object")
	var igtbl_gridState = new Object();

var igtbl_bInsideigtbl_oldOnSubmit = false;
var igtbl_bInsideigtbl_oldDoPostBack = false;
function igtbl_submit()
{
	var retVal = true;
	if (arguments.length == 0 || ((ig_csom.IsNetscape || ig_csom.IsNetscape6 || ig_csom.IsIE9Plus) && arguments.length == 1))
	{
		var form = this;
		if (form.tagName != "FORM")
			form = window._igtbl_thisForm;
		if (form)
		{
			if (form.igtbl_oldOnSubmit && !igtbl_bInsideigtbl_oldOnSubmit)
			{
				igtbl_bInsideigtbl_oldOnSubmit = true;
				if (arguments.length == 0)
					retVal = form.igtbl_oldOnSubmit();
				else
					retVal = form.igtbl_oldOnSubmit(arguments[0]);
				igtbl_bInsideigtbl_oldOnSubmit = false;
				
				if (retVal === false)
				{
					
					if (typeof (igtbl_gridState) != "undefined" && igtbl_gridState != null)
						for (var gId in igtbl_gridState)
					{
						var g = igtbl_getGridById(gId);
						if (g) g.GridIsLoaded = true;
					}
					return retVal;
				}
			}
			igtbl_updateGridsPost(form.igtblGrid);
			
			if ((window.__smartNav
			
			
			) && form.igtblGrid)
				igtbl_unloadGrid(form.igtblGrid.Id);
		}
	}
	else if (typeof (window._igtbl_doPostBackOld) != "undefined" && !igtbl_bInsideigtbl_oldDoPostBack && window._igtbl_thisForm)
	{
		igtbl_updateGridsPost(window._igtbl_thisForm.igtblGrid);
		

		if (typeof (ValidatorOnSubmit) == "function")
			retVal = ValidatorOnSubmit();

		if (retVal)
		{
			
			igtbl_bInsideigtbl_oldDoPostBack = true;
			retVal = window._igtbl_doPostBackOld(arguments[0], arguments[1]);
			
			igtbl_bInsideigtbl_oldDoPostBack = false;
			
		}
	}
	
	
	return retVal;
}

function igtbl_formSubmit()
{
	igtbl_updateGridsPost(this.igtblGrid);
	var val;
	
	try
	{
		
		val = this.igtbl_oldSubmit();
	}
	catch (e) { };
	return val;
}

function igtbl_updateGridsPost(grid)
{
	if (!grid) return;
	igtbl_updateGridsPost(grid.oldIgtblGrid);
	grid.update();
}

function igtbl_clearGridsPost(grid)
{
	
	if (!grid || !grid.ViewState || !grid.ViewState.parentNode) return;
	
	if (typeof (grid.ViewState.parentNode.removeChild) != "undefined")
		grid.ViewState.parentNode.removeChild(grid.ViewState);
	else
		grid.ViewState.parentNode.removeNode(grid.ViewState);
}

if (window.addEventListener)
	window.addEventListener('unload', igtbl_unload, false);
else if (window.onunload != igtbl_unload)
{
	igtbl_oldOnUnload = window.onunload;
	window.onunload = igtbl_unload;
}
function igtbl_toggleRow()
{
	var srcRow, expand;
	if (arguments.length == 1)
	{
		var evnt = arguments[0];
		var se = igtbl_srcElement(evnt);
		if (!se || se.tagName != "IMG")
			return;
		srcRow = se.parentNode.parentNode.id;
	}
	else
	{
		srcRow = arguments[1];
		expand = arguments[2];
	}
	var sr = igtbl_getRowById(srcRow);
	if (!sr) return;
	igtbl_lastActiveGrid = sr.gridId;
	if (typeof (expand) == "undefined")
		expand = !sr.getExpanded();
	if (expand != false)
		sr.setExpanded(true);
	else
		sr.setExpanded(false);
}

function igtbl_resizeRow(gn, rowId, height)
{	
	var gs = igtbl_getGridById(gn);
	if (!gs)
		return;
	var row = igtbl_getRowById(rowId);
	if (!row)
		return;
	if (height > 0)
	{
		var cancel = false;
		if (igtbl_fireEvent(gn, gs.Events.BeforeRowSizeChange, "(\"" + gn + "\",\"" + row.Element.id + "\"," + height + ")") == true)
			cancel = true;
		if (!cancel)
		{
			var origOffsetHeight = row.Element.offsetHeight;
			if (!row._origHeight)
				row._origHeight = row.Element.offsetHeight;
			
			row.Element.style.height = height + "px";
			gs._removeChange("ResizedRows", row);
			gs._recordChange("ResizedRows", row, height);
			
			var rowLabel = row.getRowSelectorElement();
			if (rowLabel)
				rowLabel.style.height = height + "px";
			var expansionArea = row.getExpansionElement();
			if (expansionArea)
				expansionArea.style.height = height + "px";

			if (gs.UseFixedHeaders)
			{				
				var i = 0;
				var rowElCells = row.Element.cells;
				while (i < rowElCells.length && (!rowElCells[i].firstChild || rowElCells[i].firstChild.id != gn + "_drs")) i++;
				if (i < rowElCells.length)
				{					
					var td = rowElCells[i];
					var noneFixedRow = td.firstChild.firstChild.rows[0];
					var noneFixedRowHeight = noneFixedRow.style.height;					
					td.style.height = height + "px";
					noneFixedRow.style.height = height;
										
					if (gs.IsXHTML && height > row._origHeight)
					{
						
						
						if (rowLabel)
						{							
							var calcHeight = (height + rowLabel.offsetHeight - rowLabel.clientHeight);
							td.style.height = calcHeight + "px";							
							
							noneFixedRow.style.height = calcHeight + "px";
						}						
					}

					
					if (ig_csom.IsIE6)
					{						
						if (td.offsetHeight > noneFixedRow.offsetHeight)
						{
							noneFixedRow.style.height = noneFixedRowHeight;
							td.style.height = td.offsetHeight + "px";							
						}
						if (td.clientHeight < noneFixedRow.offsetHeight)
						{
							noneFixedRow.style.height = (igtbl_parseInt(noneFixedRow.style.height) - (noneFixedRow.offsetHeight - td.clientHeight)) + "px";						
						}
						
					}
					else if (ig_csom.IsIE7Compat)
					{						
						if (td.offsetHeight > noneFixedRow.offsetHeight)
							noneFixedRow.style.height = noneFixedRowHeight;						
					}
					
					
				}
			}
			
			if (gs.StatHeader && (row.IsFilterRow || row.IsAddNewRow) && row.Band.Index == 0)
			{
				var headerDiv = gs.StatHeader.Element.parentNode.parentNode;
				headerDiv.height = "";
				headerDiv.style.height = "";
			}
			gs.alignGrid();
			
			gs.alignStatMargins();
			gs.alignDivs();
			igtbl_fireEvent(gn, gs.Events.AfterRowSizeChange, "(\"" + gn + "\",\"" + row.Element.id + "\"," + height + ")");
		}
	}
}

function igtbl_setSelectedRowImg(gn, row, hide)
{
	var gs = igtbl_getGridById(gn);
	if (!gs) return;
	if (row)
		igtbl_getRowById(row.id).setSelectedRowImg(hide);
}

function igtbl_setNewRowImg(gn, row)
{
	var gs = igtbl_getGridById(gn);
	if (!gs) return;
	gs.setNewRowImg(row ? igtbl_getRowById(row.id) : null);
}

// Expands/collapses row in internal row structure
function igtbl_stateExpandRow(gn, row, expandFlag, id, level)
{
	var gs = igtbl_getGridById(gn);
	if (!gs)
		return;
	if (expandFlag)
	{
		var dk = (row ? row.DataKey : null)
		var stateChange = gs._recordChange("ExpandedRows", row, dk, id);
		if (!row)
			ig_ClientState.setPropertyValue(stateChange.Node, "Level", level);
		else if (gs.CollapsedRows[row.Element.id])
			gs._removeChange("CollapsedRows", row);
	}
	else
	{
		if (!row) return;
		gs._recordChange("CollapsedRows", row, row.DataKey);
		gs._removeChange("ExpandedRows", row);
	}
}

function igtbl_getChildRows(gn, row)
{
	var rows = null;
	if (!row || !row.id)
		return rows;
	var rObj = igtbl_getRowById(row.id);
	if (!rObj)
		return rows;
	return rObj.getChildRows();
}

function igtbl_rowsCount(rows)
{
	var i = 0, j = 0;
	
	if (!rows) return i;
	while (j < rows.length)
	{
		var r = rows[j];
		if (!r.getAttribute("hiddenRow")
			&& !r.getAttribute("addNewRow")
            && !r.getAttribute("filterRow")
		)
			i++;
		j++;
	}
	return i;
}

function igtbl_visRowsCount(rows)
{
	var i = 0, j = 0;
	
	if (!rows) return i;
	while (j < rows.length)
	{
		var r = rows[j];
		if (!r.getAttribute("hiddenRow") && r.style.display == ""
			&& !r.getAttribute("addNewRow")
            && !r.getAttribute("FilterRow")
		)
			i++;
		j++;
	}
	return i;
}

function igtbl_sortGroupedRows(rows, bandNo, colId)
{
	
	if (rows.length <= 0)
		return;
	if (rows.Band.Index == bandNo && rows.getRow(0).Element.getAttribute("groupRow") == colId)
	{
		rows.sort();
		return;
	}
	for (var i = 0; i < rows.length; i++)
	{
		var row = rows.getRow(i);
		if (row.Rows && row.Rows.length > 0)
			igtbl_sortGroupedRows(row.Rows, bandNo, colId);
	}
}

function _igtbl_getMoreRows(gn)
{
	var g = igtbl_getGridById(gn);
	if (g)
	{
		
		if (g.ReadyState == 0)
			g.invokeXmlHttpRequest(g.eReqType.MoreRows);
	}
}

function igtbl_deleteSelRows(gn)
{
	var gs = igtbl_getGridById(gn);
	var ar = gs.getActiveRow();
	
	if (ar && ar.IsAddNewRow) return;
	var del = false;
	if (igtbl_inEditMode(gn))
	{
		igtbl_hideEdit(gn);
		if (igtbl_inEditMode(gn))
			return;
	}
	if (gs.Node)
	{
		var arOffs = ar ? ar.getIndex() : 0;
		gs.isDeletingSelected = true;
		var arr = igtbl_sortRowIdsByClctn(gs.SelectedRows);
		for (var i = 0; i < arr.length; i++)
		{
			var row = gs.getRowByLevel(arr[i]);
			if (row.deleteRow())
			{
				if (i == arr.length - 1 || arr[i].length != arr[i + 1].length || arr[i].length > 1 && arr[i][arr[i].length - 2] != arr[i + 1][arr[i + 1].length - 2])
				{
					var rows = row.OwnerCollection;
					rows.SelectedNodes = rows.Node.selectNodes("R");
					if (!rows.SelectedNodes.length)
						rows.SelectedNodes = rows.Node.selectNodes("Group");
					rows.reIndex(row.getIndex(true));
					rows.repaint();
				}
			}
		}
		if (!arr.length && ar)
		{
			var rows = ar.OwnerCollection;
			if (ar.deleteRow())
			{
				rows.SelectedNodes = rows.Node.selectNodes("R");
				if (!rows.SelectedNodes.length)
					rows.SelectedNodes = rows.Node.selectNodes("Group");
				while (rows.length == 0 && rows.ParentRow && rows.ParentRow.GroupByRow)
					rows = rows.ParentRow.OwnerCollection;
				rows.reIndex(arOffs);
				rows.repaint();
			}
		}
		if (ar && !gs.getActiveRow())
		{
			var rows = ar.OwnerCollection;
			if (arOffs < rows.length)
				rows.getRow(arOffs).activate();
			else if (rows.length > 0)
				rows.getRow(rows.length - 1).activate();
			else if (rows.ParentRow)
				rows.ParentRow.activate();
			ar = gs.getActiveRow();
			if (ar && ar.Band.getSelectTypeRow() == 2)
				ar.setSelected();
		}
		gs.isDeletingSelected = false;
		ig_dispose(arr);
		delete arr;
	}
	else
	{
		var r = null;
		if (ar && !gs.getActiveCell())
		{
			r = ar.getNextRow();
			while (r && r.getSelected())
				r = r.getNextRow();
			if (!r)
			{
				r = ar.getPrevRow();
				while (r && r.getSelected())
					r = r.getPrevRow();
			}
			if (!r)
				r = ar.ParentRow;
		}
		for (var rowId in gs.SelectedRows)
		{
			if (gs.SelectedRows[rowId])
			{
				var row = igtbl_getRowById(rowId);
				if (row && row.deleteRow(true))
					del = true;
			}
		}
		ar = gs.getActiveRow();
		if (!del && ar && !gs.SelectedRows[ar.Element.id])
		{
			del = ar.deleteRow(true);
			if (del) ar = null;
		}
		if (del)
		{
			if (r && igtbl_getElementById(r.Element.id))
			{
				if (r.Band.getSelectTypeRow() == 2)
					r.setSelected();
				r.activate();
				ar = r;
			}
			else
				ar = null;
		}
		if (!ar)
			gs.setActiveRow(null);
	}
	gs.alignStatMargins();
	if (gs.NeedPostBack)
		igtbl_doPostBack(gn);
}

function igtbl_deleteRow(gn, rowId)
{
	var row = igtbl_getRowById(rowId);
	if (!row)
		return false;
	return row.deleteRow();
}

function igtbl_clearRowChanges(gs, row)
{
	if (!row) return;
	if (gs.SelectedRows[row.Element.id])
		gs._removeChange("SelectedRows", row);
	if (gs.SelectedCellsRows[row.Element.id])
	{
		for (var cell in gs.SelectedCellsRows[row.Element.id])
		{
			gs._removeChange("SelectedCells", igtbl_getCellById(cell));
			delete gs.SelectedCellsRows[row.Element.id][cell];
		}
		delete gs.SelectedCellsRows[row.Element.id];
	}
	if (gs.ChangedRows[row.Element.id])
	{
		for (var cell in gs.ChangedRows[row.Element.id])
		{
			gs._removeChange("ChangedCells", igtbl_getCellById(cell));
			delete gs.ChangedRows[row.Element.id][cell];
		}
		delete gs.ChangedRows[row.Element.id];
	}
	if (gs.ResizedRows[row.Element.id])
		gs._removeChange("ResizedRows", row);
	if (gs.ExpandedRows[row.Element.id])
		gs._removeChange("ExpandedRows", row);
	if (gs.CollapsedRows[row.Element.id])
		gs._removeChange("CollapsedRows", row);
	if (typeof (gs.AddedRows[row.Element.id]) != "undefined")
		row._Changes["AddedRows"].setFireEvent(false);
}

function igtbl_cleanRow(row)
{
	if (row.cells)
		for (var j = 0; j < row.cells.length; j++)
	{
		var cell = row.cells[j];
		if (cell)
		{
			cell.Column = null;
			cell.Band = null;
			cell.Row = null;
			for (var change in cell._Changes)
			{
				var ch = cell._Changes[change];
				try
				{
					if (ch.length)
						ch = ch[0];
					if (ch.Grid)
						ch.Grid._removeChange(change, cell);
				} catch (e) { ; }
			}
			if (cell.Element)
				cell.Element.Object = null;
		}
	}
	if (row._Changes)
		for (var change in row._Changes)
	{
		var ch = row._Changes[change];
		try
		{
			if (ch.length)
				ch = ch[0];
			if (ch.Grid)
				ch.Grid._removeChange(change, row);
		} catch (e) { ; }
	}
	row.OwnerCollection = null;
	row.Band = null;
	row.ParentRow = null;
	row.Element.Object = null;
}

var igtbl_justAssigned = false;
function igtbl_resetJustAssigned()
{
	igtbl_justAssigned = false;
}

function igtbl_clearNoOnResize(gn)
{
	var g = igtbl_getGridById(gn);
	g.Element.removeAttribute("noOnResize");
}
function igtbl_fillEditTemplate(row, childNodes)
{
	for (var i = childNodes.length - 1; i >= 0; i--)
	{
		var el = childNodes[i];
		if (typeof (el.getAttribute) == "undefined")
			continue;
		var colKey = el.getAttribute("columnKey");
		var column = row.Band.getColumnFromKey(colKey);
		if (column)
		{
			var cell = row.getCellByColumn(column);
			if (!cell)
			{
				if (!el.isDisabled)
				{
					el.setAttribute("disabledBefore", true);
					el.disabled = true;
				}
				el.value = "";
				continue;
			}
			else if (el.isDisabled && el.getAttribute("disabledBefore"))
			{
				el.disabled = false;
				el.removeAttribute("disabledBefore");
			}
			var cellValue = cell.getValue();
			var cellText = "";
			var nullText = "";
			if (cellValue == null)
			{
				nullText = cell.Column.getNullText();
				cellText = nullText;
			}
			else
				cellText = cellValue.toString();
			var ect = cellText.replace(/\r\n/g, "\\r\\n");
			ect = ect.replace(/\"/g, "\\\"");
			var s = "(\"" + row.gridId + "\",\"" + el.id + "\",\"" + (cell.Element ? cell.Element.id : "") + "\",\"" + ect + "\")";
			if (!igtbl_fireEvent(row.gridId, igtbl_getGridById(row.gridId).Events.TemplateUpdateControls, s))
			{
				if (el.tagName == "SELECT")
				{
					for (var j = 0; j < el.childNodes.length; j++)
						if (el.childNodes[j].tagName == "OPTION")
						if (el.childNodes[j].value == cellText)
					{
						el.childNodes[j].selected = true;
						break;
					}
				}
				else if (el.tagName == "INPUT" && el.type == "checkbox")
				{
					if (!cellValue || cellText.toLowerCase() == "false")
						el.checked = false;
					else
						el.checked = true;
				}
				else if (el.tagName == "DIV" || el.tagName == "SPAN")
				{
					for (var j = 0; j < el.childNodes.length; j++)
					{
						if (el.childNodes[j].tagName == "INPUT" && el.childNodes[j].type == "radio")
							if (el.childNodes[j].value == cellText)
						{
							el.childNodes[j].checked = true;
							break;
						}
					}
				}
				else
					el.value = cellText;
				if (!el.isDisabled)
					igtbl_focusedElement = el;
			}
		}
		else if (el.childNodes && el.childNodes.length > 0)
			igtbl_fillEditTemplate(row, el.childNodes);
	}
}

function igtbl_unloadEditTemplate(row, childNodes)
{
	for (var i = 0; i < childNodes.length; i++)
	{
		var el = childNodes[i];
		
		if (typeof (el.getAttribute) == "unknown" || !el.getAttribute)
			continue;
		var colKey = el.getAttribute("columnKey");
		var column = row.Band.getColumnFromKey(colKey);
		if (column)
		{
			var cell = row.getCellByColumn(column);
			if (cell && !igtbl_fireEvent(row.gridId, igtbl_getGridById(row.gridId).Events.TemplateUpdateCells, "(\"" + row.gridId + "\",\"" + el.id + "\",\"" + (cell.Element ? cell.Element.id : "") + "\")"))
			{
				if (cell.isEditable() || cell.Column.getAllowUpdate() == 3)
				{
					if (el.tagName == "SELECT")
						cell.setValue(el.options[el.selectedIndex].value);
					else if (el.tagName == "INPUT" && el.type == "checkbox")
						cell.setValue(el.checked);
					else if (el.tagName == "DIV" || el.tagName == "SPAN")
					{
						for (var j = 0; j < el.childNodes.length; j++)
						{
							if (el.childNodes[j].tagName == "INPUT" && el.childNodes[j].type == "radio")
								if (el.childNodes[j].checked)
							{
								cell.setValue(el.childNodes[j].value);
								break;
							}
						}
					}
					else if (typeof (el.value) != "undefined")
						cell.setValue(el.value);
				}
			}
		}
		else if (el.childNodes && el.childNodes.length > 0)
			igtbl_unloadEditTemplate(row, el.childNodes);
	}
}

function igtbl_gRowEditMouseDown(evnt)
{
	if (igtbl_justAssigned)
	{
		igtbl_justAssigned = false;
		return;
	}
	if (!evnt)
		evnt = event;
	var src = igtbl_srcElement(evnt);
	var editTempl = igtbl_getElementById(igtbl_currentEditTempl);
	if (editTempl && src && !igtbl_contains(editTempl, src))
	{
		var rId = editTempl.getAttribute("editRow");
		if (rId)
		{
			var row = igtbl_getRowById(rId);
			row.Band.Grid.event = evnt;
			row.endEditRow();
		}
	}
}

function igtbl_gRowEditButtonClick(evnt, saveChanges)
{
	if (!evnt)
		evnt = event;
	var src = igtbl_srcElement(evnt);
	var editTempl = igtbl_getElementById(igtbl_currentEditTempl);
	if (editTempl)
	{
		if (typeof (saveChanges) == "undefined")
			saveChanges = (src.id.substring(src.id.length - 13) == "igtbl_reOkBtn") || src.value.toUpperCase() == "OK";
		var rId = editTempl.getAttribute("editRow");
		if (rId)
		{
			var row = igtbl_getRowById(rId);
			row.Band.Grid.event = evnt;
			row.endEditRow(saveChanges);
		}
	}
}

function igtbl_RecalculateRowNumbers(rc, startingIndex, band, xmlNode)
{	
	if (rc == null && band == null) return startingIndex;

	var oRow;
	var iRowLbl = -1;
	var oFAC;
	var returnedIndex = -1;
	var workingIndex;
	var oBand = band ? band : rc.Band;

	switch (oBand.AllowRowNumbering)
	{
		case (2): 
			workingIndex = startingIndex;
			break;
		case (3): 
			workingIndex = 1;
			break;
		case (4): 
			
			
			var indexOffSet = (oBand.Grid.AllowPaging && oBand.Index == 0) ? ((oBand.Grid.CurrentPageIndex - 1) * oBand.Grid.PageSize) : oBand._currentRowNumber;
			workingIndex = indexOffSet + 1;			
			break;
	}	

	if (null != rc)
	{
		for (var i = 0; i < rc.length; i++)
		{
			iRowLbl = -1;
			oRow = rc.getRow(i);

			if (oRow.Band.AllowRowNumbering >= 2)
				iRowLbl = oRow._setRowNumber(workingIndex);

			if (iRowLbl > -1)
			{
				var childRows = oRow.Rows;
				var childBand = childRows ? childRows.Band : oRow.Band.Grid.Bands[oRow.Band.Index + 1];
				var childXmlNode = childRows ? childRows.Node : (oRow.Node ? oRow.Node.selectSingleNode("Rs") : null);
				returnedIndex = igtbl_RecalculateRowNumbers(childRows, workingIndex + 1, childBand, childXmlNode);
			}

			switch (rc.Band.AllowRowNumbering)
			{
				case (2): 
					workingIndex = returnedIndex;
					break;
				case (3): 
					workingIndex = ++workingIndex;
					break;
				case (4): 
					oRow.Band._currentRowNumber = workingIndex;
					workingIndex = ++workingIndex;
					break;
			}
		}
	}
	else if (band != null && xmlNode != null)
	{
		var oXmlRows = xmlNode.selectNodes("R");
		for (var i = 0; i < oXmlRows.length; i++)
		{
			iRowLbl = -1;
			oRow = oXmlRows[i];

			if (band.AllowRowNumbering >= 2)
				oRow.setAttribute(igtbl_litPrefix + "rowNumber", workingIndex);

			var childRows = null;
			var childBand = band.Grid.Bands[band.Index + 1];
			var childXmlNode = oRow.selectSingleNode("Rs");

			returnedIndex = igtbl_RecalculateRowNumbers(childRows, workingIndex + 1, childBand, childXmlNode);

			switch (band.AllowRowNumbering)
			{
				case (2): 
					workingIndex = returnedIndex;
					break;
				case (3): 
					workingIndex = ++workingIndex;
					break;
				case (4): 
					band._currentRowNumber = workingIndex;
					workingIndex = ++workingIndex;
					break;
			}
		}
	}
	return workingIndex;
}

function igtbl_swapCells(rows, bandNo, index, toIndex)
{
	if (!rows || rows.Band.Index > bandNo)
		return;
	for (var i = 0; i < rows.rows.length; i++)
	{
		var row = rows.rows[i];
		if (row)
		{
			if (!row.GroupByRow && row.Band.Index == bandNo && row.cells)
			{
				var cell = row.cells[index];
				row.cells[index] = row.cells[toIndex];
				row.cells[toIndex] = cell;
			}
			igtbl_swapCells(row.Rows, bandNo, index, toIndex);
		}
	}
}

function igtbl_AdjustCheckboxDisabledState(column, bandIndex, rows, value)
{
	if (!rows) return;
	if (rows.Band.Index == bandIndex)
		for (var i = 0; i < rows.length; i++)
	{
		var oC = rows.getRow(i).getCellByColumn(column);
		oC = igtbl_getCheckboxFromElement(oC.Element);
		if (oC) oC.disabled = !(1 == value);
	}
	else if (rows.Band.Index < bandIndex) 
		for (var i = 0; i < rows.length; i++) igtbl_AdjustCheckboxDisabledState(column, bandIndex, rows.getRow(i).Rows, value);
}


function igtbl_cancelNoOnScroll(gn)
{
	var g = igtbl_getGridById(gn);
	if (!g) return;
	var de = g.getDivElement();
	de.removeAttribute("noOnScroll");
	de.removeAttribute("oldST");
	de.removeAttribute("oldSL");
	g.cancelNoOnScrollTimeout = 0;
}

function igtbl_scrollLeft(e, left)
{
	e.scrollLeft = left;
	if ((ig_csom.IsNetscape || ig_csom.IsNetscape6) && e.onscroll && !e._insideFFOnScroll)
	{
		e._insideFFOnScroll = true;
		e.onscroll();
		e._insideFFOnScroll = false;
	}
}

function igtbl_scrollTop(e, top)
{
	if (e.scrollTop == top)
		return;
	
	if (ig_shared.IsIE8)	
		e.scrollTop = (top > 0 ? top - 1 : top + 1);
	e.scrollTop = top;
	if ((ig_csom.IsNetscape || ig_csom.IsNetscape6) && e.onscroll && !e._insideFFOnScroll)
	{
		e._insideFFOnScroll = true;
		e.onscroll();
		e._insideFFOnScroll = false;
	}
}

function igtbl_getBodyScrollLeft()
{
	var isXHTML = document.compatMode == "CSS1Compat";
	if (isXHTML)
		return document.body.parentNode.scrollLeft;
	else
		return document.body.scrollLeft;
}

function igtbl_getBodyScrollTop()
{
	if (igtbl_isXHTML)
		return document.body.parentNode.scrollTop;
	else
		return document.body.scrollTop;
}
function igtbl_selectStart(evnt, gn)
{	
	var se = igtbl_srcElement(evnt);
	if (se)
	{
		var over = false, cell = null, column = null, cellByElem = null;
		
		while (se && !over)
		{
			
			
			if (se && (se.tagName == "TABLE" && se.id == "G_" + gn ||
					  se.tagName == "TH" && (column = igtbl_getColumnById(se.id)) != null ||
					  se.tagName == "TD" && ((cell = igtbl_getCellById(se.id)) != null || 
					  (cellByElem = igtbl_getCellByElement(se)) != null)))
			{
				if (cellByElem != null && cell == null)
					cell = cellByElem;
				over = true;
			}
			se = se.parentNode;
		}
		
		if (cell)
		{
			if (!(cell.Column.TemplatedColumn & 2))			
				igtbl_cancelEvent(evnt);			
		}
		
		else if (column)
		{
			if ((!(column.TemplatedColumn & 1) && se.parentNode.parentNode.tagName == "THEAD") ||
				(!(column.TemplatedColumn & 4) && se.parentNode.parentNode.tagName == "TFOOT")
			  )			
				igtbl_cancelEvent(evnt);			
		}
		else		
			igtbl_cancelEvent(evnt);		
	}
}

function igtbl_selectColumnRegion(gn, se)
{
	var gs = igtbl_getGridById(gn);
	if (!gs)
		return;
	var te = gs.Element;
	var lastSelectedColumn = te.getAttribute("lastSelectedColumn");
	var selMethod = te.getAttribute("selectMethod");
	if (selMethod == "column" && se.id != lastSelectedColumn)
	{
		var startColumn = igtbl_getColumnById(te.getAttribute("startColumn"));
		if (startColumn == null)
			startColumn = igtbl_getColumnById(se.id);
		var endColumn = igtbl_getColumnById(se.id);
		if (endColumn.Band.getSelectTypeColumn() == 3)
			gs.selectColRegion(startColumn, endColumn);
		else
		{
			igtbl_clearSelectionAll(gn);
			igtbl_selectColumn(gn, se.id);
		}
		gs.Element.setAttribute("lastSelectedColumn", se.id);
	}
}

function igtbl_selectRegion(gn, se)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || !se)
		return;
	var rowContainer;
	var cell = igtbl_getCellById(se.id), row = null;
	if (!cell)
		row = igtbl_getRowById(se.id);
	else
		row = cell.Row;
	if (!row)
		return;
	if (se.getAttribute("groupRow"))
		rowContainer = se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
	else
		rowContainer = row.Element.parentNode;
	var te = gs.Element;
	var selTableId = te.getAttribute("selectTable");
	var workTableId;
	if (row.IsAddNewRow && row.Band.Index == 0)
		workTableId = gs.Element.id;
	else
		if (se.getAttribute("groupRow"))
		workTableId = se.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
	else
		workTableId = row.Element.parentNode.parentNode.id;
	if (workTableId == "")
		return;
	var bandNo = igtbl_getElementById(workTableId).getAttribute("bandNo");
	if (selTableId == workTableId)
	{
		var selMethod = te.getAttribute("selectMethod");
		if (selMethod == "row" && (!cell || igtbl_getCellClickAction(gn, bandNo) == 2 && cell || se.getAttribute("groupRow")))
		{
			var selRow = igtbl_getRowById(te.getAttribute("startPointRow"));
			var rowId;
			if (se.getAttribute("groupRow"))
				rowId = se.parentNode.parentNode.parentNode.parentNode.parentNode.id;
			else
				rowId = row.Element.id;
			var curRow = igtbl_getRowById(rowId);
			if (selRow && igtbl_getSelectTypeRow(gn, bandNo) == 3 && igtbl_getCellClickAction(gn, bandNo) > 0)
			{
				igtbl_setActiveRow(gn, curRow.getFirstRow()); 
				gs.selectRowRegion(selRow, curRow);
			}
			else
			{
				igtbl_setActiveRow(gn, igtbl_getFirstRow(igtbl_getElementById(rowId))); 
				if (!(curRow.getSelected() && igtbl_getLength(gs.SelectedRows) == 1))
				{
					igtbl_clearSelectionAll(gn);
					if (se.getAttribute("groupRow"))
						rowId = igtbl_getWorkRow(row.Element, gn).id;
					if (igtbl_getSelectTypeRow(gn, bandNo) > 1 && igtbl_getCellClickAction(gn, bandNo) > 0)
						igtbl_selectRow(gn, curRow);
				}
			}
		}
		else if (selMethod == "cell" && cell)
		{
			var selCell = igtbl_getCellById(te.getAttribute("startPointCell"));
			var curCell = igtbl_getCellById(se.id);
			if (igtbl_getSelectTypeCell(gn, bandNo) == 3 && igtbl_getCellClickAction(gn, bandNo) > 0 && selCell)
			{
				gs.selectCellRegion(selCell, curCell);
				curCell.activate();
			}
			else
			{
				if (!(curCell.getSelected() && igtbl_getLength(gs.SelectedRows) == 1))
				{
					igtbl_clearSelectionAll(gn);
					if (igtbl_getSelectTypeCell(gn, bandNo) > 1 && igtbl_getCellClickAction(gn, bandNo) > 0)
						igtbl_selectCell(gn, curCell);
				}
				igtbl_setActiveCell(gn, se);
			}
		}
	}
}

function igtbl_clearSelectionAll(gn, rowId)
{
	var gs = igtbl_getGridById(gn);
	if (!rowId)
		rowId = "";
	if (igtbl_fireEvent(gn, gs.Events.BeforeSelectChange, "(\"" + gn + "\",\"" + rowId + "\")") == true)
		return;
	var row, column, cell;
	gs._noCellChange = false;
	for (var row in gs.SelectedRows)
		igtbl_selectRow(gn, row, false, false);
	for (var column in gs.SelectedColumns)
		igtbl_selectColumn(gn, column, false, false);
	for (var row in gs.SelectedCellsRows)
	{
		for (var cell in gs.SelectedCellsRows[row])
			delete gs.SelectedCellsRows[row][cell];
		delete gs.SelectedCellsRows[row];
	}
	for (var cell in gs.SelectedCells)
		igtbl_selectCell(gn, cell, false, false);
}

function igtbl_selectCell(gn, cellID, selFlag, fireEvent)
{
	var cell = cellID;
	if (typeof (cell) == "string")
		cell = igtbl_getCellById(cellID);
	
	
	if (!cell)
		return;
	cell.select(selFlag, fireEvent);
}

function igtbl_selectRow(gn, rowID, selFlag, fireEvent)
{
	var rowObj = rowID;
	if (typeof (rowObj) == "string")
		rowObj = igtbl_getRowById(rowID);
	else
		rowID = rowObj.Element.id;
	if (!rowObj)
		return false;
	return rowObj.select(selFlag, fireEvent);
}

function igtbl_selColRI(gn, column, bandNo, colNo, nonFixed)
{
	var cellElems = igtbl_enumColumnCells(gn, column);
	for (var i = 0; i < cellElems.length; i++)
	{
		var visElem = cellElems[i];
		igtbl_changeStyle(gn, visElem, igtbl_getSelCellClass(gn, bandNo, colNo));
	}
	igtbl_changeStyle(gn, column, igtbl_getSelHeadClass(gn, bandNo, colNo));
	igtbl_dispose(cellElems);
}

function igtbl_selectColumn(gn, columnID, selFlag, fireEvent)
{
	var column = igtbl_getElementById(columnID);
	var colObj = igtbl_getColumnById(columnID);
	
	if (!colObj) return;
	var bandNo = colObj.Band.Index;
	if (igtbl_getSelectTypeColumn(gn, bandNo) < 2)
		return;
	var colNo = colObj.Index;
	var gs = igtbl_getGridById(gn);
	if (gs._exitEditCancel || gs._noCellChange)
		return;
	if (fireEvent != false)
		if (igtbl_fireEvent(gn, gs.Events.BeforeSelectChange, "(\"" + gn + "\",\"" + columnID + "\")") == true)
		return;
	var nonFixed = gs.UseFixedHeaders && !colObj.getFixed();
	var aRow = null;
	var aCell = gs.getActiveCell();
	if (aCell && aCell.Column != colObj)
		aCell = null;
	else if (!aCell)
		aRow = gs.getActiveRow();
	if (selFlag != false)
	{
		var cols = igtbl_getDocumentElement(columnID);
		if (cols)
		{
			if (cols.length)
				for (var j = 0; j < cols.length; j++)
				igtbl_selColRI(gn, cols[j], bandNo, colNo, nonFixed);
			else
				igtbl_selColRI(gn, column, bandNo, colNo, nonFixed);
			gs._recordChange("SelectedColumns", colObj, gs.GridIsLoaded.toString());
			colObj.Selected = true;
			gs.Element.setAttribute("lastSelectedColumn", columnID);
		}
	}
	else
	{
		var cols = igtbl_getDocumentElement(columnID);
		if (!cols.length)
			cols = [cols];
		for (var j = 0; j < cols.length; j++)
		{
			var colsj = cols[j];
			igtbl_changeStyle(gn, colsj, null);
			var cellElems = igtbl_enumColumnCells(gn, cols[j]);
			for (var i = 0; i < cellElems.length; i++)
			{
				var cell = cellElems[i];
				var row = cell.parentNode;
				if (!row.getAttribute("hiddenRow") && !gs.SelectedRows[row.id] && !gs.SelectedCells[cell.id])
					igtbl_changeStyle(gn, cell, null);
			}
			igtbl_dispose(cellElems);
		}
		gs._removeChange("SelectedColumns", colObj);
		colObj.Selected = false;
	}
	if (aRow)
		aRow.renderActive();
	if (aCell)
		aCell.renderActive();
	if (fireEvent != false)
	{
		var gsNPB = gs.NeedPostBack;
		igtbl_fireEvent(gn, gs.Events.AfterSelectChange, "(\"" + gn + "\",\"" + columnID + "\");");
		if (!gsNPB && !(gs.Events.AfterSelectChange[1] & 4))
			gs.NeedPostBack = false;
		if (gs.NeedPostBack)
			igtbl_moveBackPostField(gn, "SelectedColumns");
	}
}

function igtbl_gSelectArray(gn, elem, array)
{
	var gs = igtbl_getGridById(gn);
	gs._noCellChange = false;
	if (elem == 0)
	{
		var oldSelCells = gs.SelectedCells;
		gs.SelectedCells = new Object();
		
		for (var i = 0; i < array.length; i++)
			if (oldSelCells[array[i]])
			gs.SelectedCells[array[i]] = true;
		var fireOnUnsel = true;
		for (var i = 0; i < array.length; i++)
			if (!oldSelCells[array[i]])
		{
			igtbl_selectCell(gn, array[i]);
			fireOnUnsel = false;
		}
		for (var cell in oldSelCells)
			if (!gs.SelectedCells[cell])
			igtbl_selectCell(gn, cell, false, fireOnUnsel);
		for (var cell in oldSelCells)
			delete oldSelCells[cell];
	}
	else if (elem == 1)
	{
		var oldSelRows = gs.SelectedRows;
		gs.SelectedRows = new Object();
		
		for (var i = 0; i < array.length; i++)
			if (oldSelRows[array[i]])
			gs.SelectedRows[array[i]] = true;
		var fireOnUnsel = true;
		for (var i = 0; i < array.length; i++)
			if (!oldSelRows[array[i]])
		{
			igtbl_selectRow(gn, array[i]);
			fireOnUnsel = false;
		}
		for (var row in oldSelRows)
			if (!gs.SelectedRows[row])
			igtbl_selectRow(gn, row, false, fireOnUnsel);
		for (var row in oldSelRows)
			delete oldSelRows[row];
	}
	else
	{
		var oldSelCols = gs.SelectedColumns;
		gs.SelectedColumns = new Object();
		
		for (var i = 0; i < array.length; i++)
			if (oldSelCols[array[i]])
			gs.SelectedColumns[array[i]] = true;
		var fireOnUnsel = true;
		for (var i = 0; i < array.length; i++)
			if (!oldSelCols[array[i]])
		{
			igtbl_selectColumn(gn, array[i]);
			fireOnUnsel = false;
		}
		for (var col in oldSelCols)
			if (!gs.SelectedColumns[col])
			igtbl_selectColumn(gn, col, false, fireOnUnsel);
		for (var col in oldSelCols)
			delete oldSelCols[col];
	}
}
function igtbl_initStatHeader(gs)
{	
	this.Type = "statHeader";	
	this.gridId = gs.Id;
	this.Element = gs._tdContainer.parentNode.previousSibling.childNodes[0].childNodes[0].childNodes[0].childNodes[1];
	this.ScrollTo = igtbl_scrollStatHeader;
	this.getElementByColumn = igtbl_shGetElemByCol;
	
	if (!gs.Bands[0].HasHeaderLayout)
		_igtbl_headerOrFooterHeight(this.Element);  
	var outlGB = false;
	if (gs.Rows && gs.Rows.length > 0 && (row = gs.Rows.getRow(0)).GroupByRow)
		outlGB = true;
	if (!gs.UseFixedHeaders)
	{
		var row;
		if (outlGB)
		{
			while (row.GroupByRow && row.Rows && row.Rows.length > 0)
				row = row.Rows.getRow(0);
			if (row.GroupByRow)
			{
				for (var i = 0; i < this.Element.childNodes[0].childNodes.length; i++)
				{
					var col = this.Element.childNodes[0].childNodes[i];
					if (col.getAttribute("columnNo"))
					{
						var colNo = parseInt(col.getAttribute("columnNo"));
						gs.Bands[0].Columns[colNo].Element = col;
					}
				}
				return;
			}
		}
		for (var i = 0; i < this.Element.childNodes[0].childNodes.length; i++)
		{
			var col = this.Element.childNodes[0].childNodes[i];
			if (col.getAttribute("columnNo"))
			{
				var colNo = parseInt(col.getAttribute("columnNo"));
				gs.Bands[0].Columns[colNo].Element = col;
			}
		}
	}
	else
	{
		var childNodes = this.Element.childNodes[0].childNodes;
		var i = 0;
		while (i < childNodes.length)
		{
			var col = childNodes[i];
			i++;
			if (col.getAttribute("columnNo"))
			{
				var colNo = parseInt(col.getAttribute("columnNo"));
				gs.Bands[0].Columns[colNo].Element = col;
			}
			else if (col.colSpan > 1 && col.firstChild.tagName == "DIV" && col.firstChild.id.substr(col.firstChild.id.length - 4) == "_drs")
			{
				childNodes = col.firstChild.firstChild.childNodes[1].rows[0].childNodes;
				i = 0;
			}
		}
	}
		
	var comWidth = gs.Element.offsetWidth == 0 ? gs.Element.style.width : gs.Element.offsetWidth;
	var hasPercWidth = gs.Element.style.width.indexOf("%") > 0; 
	if (typeof (comWidth) == "number" || (typeof (comWidth) == "string" && comWidth.indexOf("%") == -1))
	{
		
		comWidth = (typeof (comWidth) == "string") ? igtbl_parseInt(comWidth) : comWidth;
		if ((gs.AllowUpdate == 1 || gs.Bands[0].AllowUpdate == 1) && !hasPercWidth)
			comWidth--;
		if (outlGB)
		{
			comWidth -= _igtbl_headerRowIndentation(gs, gs._AddnlProps[8].split(";"));
		}
		
		
		
		
		var testTbl = gs.Element;
		
		var ignoreVisibility = false;		
		if (gs.Bands && gs.Bands[0].IsGrouped && gs.Rows)
		{	
					
			testTbl = this.Element.parentNode;
		}
				
		if (hasPercWidth && igtbl_dom.table.hasPercentageColumns(testTbl, gs.Bands[0].firstActiveCell, ignoreVisibility))
			this.Element.parentNode.style.width = "100%"
		else if (comWidth > 0)
		{			
			comWidth = comWidth + "px";
			this.Element.parentNode.style.width = comWidth; 
			if (!hasPercWidth && gs.Element.style.width != comWidth)			
				gs.Element.style.width = comWidth;			
		}
	}
	else if (comWidth > 0)
	{		
		this.Element.parentNode.style.width = comWidth + "px";
	}
}

function igtbl_scrollStatHeader(scrollLeft)
{

	var gs = igtbl_getGridById(this.gridId);

	var parentNodeStyle = this.Element.parentNode.style;
	
	if (!gs.UseFixedHeaders)
		parentNodeStyle.left = -scrollLeft + "px";
	
	
	var hasPercWidth = gs.Element.style.width.indexOf("%") > 0; 
	var comWidth = gs.getDivElement().firstChild.offsetWidth;
	var hdrTblWidth = comWidth;
	var hdrRowInd = 0;
	if (gs.Rows && gs.Rows.length > 0 && (row = gs.Rows.getRow(0)).GroupByRow)
	{
		hdrRowInd = _igtbl_headerRowIndentation(gs, gs.Bands[0].SortedColumns);
		comWidth -= hdrRowInd;
		
		
		hdrTblWidth -= (row.Node ? hdrRowInd : 2 * hdrRowInd);
	}

	
	
	
	if (parentNodeStyle.width && !(hasPercWidth && parentNodeStyle.width.indexOf("%") != -1) && comWidth > 0 && gs.Element.offsetWidth > (comWidth + hdrRowInd))
	{
		
		if (hasPercWidth && parentNodeStyle.width == "")
			parentNodeStyle.width = gs.Element.style.width;
		else
		{

			
			comWidth = comWidth + "px";
			hdrTblWidth = hdrTblWidth + "px";
			
			
			parentNodeStyle.width = hdrTblWidth;
			
			
			
			
		}

	}
}



function igtbl_initStatFooter(gs)
{
	this.Type = "statFooter";
	this.ScrollTo = igtbl_scrollStatFooter;
	this.Resize = igtbl_resizeStatFooter;
	this.getElementByColumn = igtbl_sfGetElemByCol;

	this.gridId = gs.Id;
	var tbl = gs._tdContainer.parentNode.nextSibling.firstChild.firstChild.firstChild;
	this.Element = tbl.rows[tbl.rows.length - 1].parentNode;
	_igtbl_headerOrFooterHeight(this.Element); 
	var comWidth = gs.Element.offsetWidth;
	var hasPercWidth = gs.Element.style.width.indexOf("%") > 0; 
	if ((gs.AllowUpdate == 1 || gs.Bands[0].AllowUpdate == 1) && !hasPercWidth)
		comWidth--;
	if (gs.Rows && gs.Rows.length > 0 && (row = gs.Rows.getRow(0)).GroupByRow)
	{
		comWidth -= _igtbl_headerRowIndentation(gs, gs._AddnlProps[8].split(";"));
	}
	if (comWidth > 0)
	{
		comWidth = comWidth + "px"; 
		this.Element.parentNode.style.width = comWidth;
	}
}

function igtbl_scrollStatFooter(scrollLeft)
{
	var gs = igtbl_getGridById(this.gridId);
	if (!gs.UseFixedHeaders)
		this.Element.parentNode.style.left = -scrollLeft + "px";

	var comWidth = gs.Element.offsetWidth;

	
	if (gs.Rows && gs.Rows.length > 0 && (row = gs.Rows.getRow(0)).GroupByRow)
	{
		comWidth -= _igtbl_headerRowIndentation(gs, gs.Bands[0].SortedColumns);
	}
	
	if (this.Element.parentNode.style.width && comWidth > 0)
	{
		comWidth = comWidth + "px"; 
		this.Element.parentNode.style.width = comWidth;
	}
}

function igtbl_resizeStatFooter(index, width)
{
	var c1w = width;
	var gs = igtbl_getGridById(this.gridId);
	
	var column = gs.Bands[0].Columns[index];
	
	
	var el = igtbl_getDocumentElement(column.fId);
	if (el && el.length && el.length > 0)
	{
		el = el[el.length - 1];
	}

	var spannedFooter = false;
	if (!el)
	{
		el = igtbl_getElemVis(gs.StatFooter.Element.childNodes[0].childNodes, index);
		spannedFooter = true;
	}
	if (el)
	{
		
		var cg = el.parentNode.parentNode.previousSibling;
		var anCell = null;
		if (gs.Rows.AddNewRow && gs.Bands[0].AddNewRowView == 2)
		{
			cg = cg.previousSibling;
			anCell = gs.Rows.AddNewRow.getCellByColumn(column);
		}
		
		while (cg && cg.tagName != 'COLGROUP')
		{
			cg = cg.previousSibling;
		}
		var c;
		if (cg)
		{
			c = cg.childNodes[anCell ? anCell.getElement().cellIndex : el.cellIndex];
		}
		else
			c = el;
		
		
		c.style.width = c1w + "px";
		el.style.width = c1w + "px";
		if (gs.UseFixedHeaders && column && !column.getFixed())
		{
			var d = c.style.display;
			c.style.display = "none";
			c.style.display = d;
		}
	}
	
}

function _igtbl_headerOrFooterHeight(el)
{
	
	
	
	if (el.parentNode.offsetHeight == 0)
		return;

	if (el.parentNode.offsetHeight == 0)
	{
		var chn = el.firstChild.firstChild;
		while (chn && !chn.height)
		{
			chn = chn.nextSibling;
		}
		if (chn && chn.height)
		{
			
			var chnH;
			if (chn.currentStyle)
			{
				chnH = parseInt(chn.currentStyle.height) + parseInt(chn.currentStyle.borderBottomWidth) + parseInt(chn.currentStyle.borderTopWidth)
				if (isNaN(chnH))
				{
					chnH = chn.height;
				}
				else
				{
					chnH += "px";
				}
			}
			else
			{
				chnH = chn.height;
			}
			el.parentNode.parentNode.style.height = chnH;
		}
		else
		{
			el.parentNode.parentNode.style.height = "20px";
		}
	}
	else
	{
		el.parentNode.parentNode.style.height = el.parentNode.offsetHeight + "px";
	}
}

function _igtbl_headerRowIndentation(gs, sc)
{
	var indentation0 = gs.Bands[0].getIndentation();
	var result = 0;
	for (var i = 0; i < sc.length; i++)
	{
		var col = igtbl_getColumnById(sc[i]);
		if (!col || col.Band.Index > 0 || !col.IsGroupBy)
		{
			break;
		}
		result += indentation0;
	}
	return result;
}
igtbl_browserWorkarounds = {

	
	

	
	ieBorderCollapseArtifacts: function(row, h)
	{
		
		var table = row.Element.parentNode.parentNode;
		if (!ig_csom.IsIE7Compat ||
			table.currentStyle.borderCollapse != "collapse" ||
			table.currentStyle.tableLayout == "fixed")
		{
			return;
		}

		
		for (var x = row.cells.length - 1; x >= 0; x--)
		{
			var cell = row.getCell(x);
			var col = cell.Column;
			cell.Element.style.display = (col.Hidden || h ? "none" : "");
		}
	},

	ieTabScrollBarAdjustment: function(firstBand)
	{
		if (!ig_csom.IsIE7Compat) return;
		
		if (firstBand)
		{
			var firstColumn = firstBand.Columns[0];
			
			while (firstColumn && firstColumn.getHidden())
				firstColumn = firstBand.Columns[firstColumn.Index + 1]
			if (firstColumn) firstColumn.setWidth(firstColumn.getWidth());
		}
	},

	
	


	
	

	addActiveElementTracking: function()
	{
		if (typeof (document.activeElement) == "undefined" && !this.isTrackingActiveElement)
		{
			ig_csom.addEventListener(document, "focus", this.trackActiveElement);
			this.isTrackingActiveElement = true;
		}
	},
	removeActiveElementTracking: function()
	{
		if (this.isTrackingActiveElement)
		{
			ig_csom.removeEventListener(document, "focus", this.trackActiveElement);
			igtbl_browserWorkarounds.activeElement = null;
		}
	},
	trackActiveElement: function(e)
	{
		var evnt = igtbl_event.getEvent(e);
		igtbl_browserWorkarounds.activeElement = igtbl_srcElement(evnt);
	}
};

function igtbl_fixDOEXml()
{
	if (ig_csom.IsNetscape6)
	{
		var doeElems = document.getElementsByName("_igdoe");
		for (var i = doeElems.length - 1; i >= 0; i--)
		{
			var doe = doeElems[i];
			doe.innerHTML = doe.textContent;
			doe.removeAttribute("name");
		}
	}
}
igtbl_string =
{
	
	stringToBool: function(value)
	{
		if (value === "true" || value === true)
			return true;
		return false;
	},

	
	trim: function(s)
	{
		if (!s)
			return s;
		s = s.toString();
		var result = s;
		for (var i = 0; i < s.length; i++)
			if (s.charAt(i) != ' ')
			break;
		result = s.substr(i, s.length - i);
		for (var i = result.length - 1; i >= 0; i--)
			if (result.charAt(i) != ' ')
			break;
		result = result.substr(0, i + 1);
		return result;
	},

	
	isNullOrEmpty: function(object, property)
	{
		if (typeof (object[property]) == "undefined") return true;
		if (object[property] === "") return true;
		if (object[property] === null) return true;
		return false;
	},

	toString: function(object)
	{
		if (typeof (object) != undefined && object != null && typeof (object.toString) == "function")
			return object.toString();
		return "";
	}
};

igtbl_number =
{
	
	fromString: function(number)
	{
		
		if (number)
		{
			var outValue = parseInt(number, 10);
			if (!isNaN(outValue))
				return outValue;
		}
		return 0;
	},

	sortNumber: function(a, b)
	{
		return a[0] - b[0];
	},

	isNumberType: function(dataType)
	{
		switch (dataType)
		{
			case igtbl_dataType.Int16:
			case igtbl_dataType.Int32:
			case igtbl_dataType.Byte:
			case igtbl_dataType.SByte:
			case igtbl_dataType.UInt16:
			case igtbl_dataType.UInt32:
			case igtbl_dataType.Int64:
			case igtbl_dataType.UInt64:
			case igtbl_dataType.Single:
			case igtbl_dataType.Double:
			case igtbl_dataType.Decimal:
				return true;
		}
		return false;
	}
};

function igtbl_valueFromString(value, dataType)
{
	if (typeof (value) == "undefined" || value == null)
		return value;
	switch (dataType)
	{
		case igtbl_dataType.Int16:
		case igtbl_dataType.Int32:
		case igtbl_dataType.Byte:
		case igtbl_dataType.SByte:
		case igtbl_dataType.UInt16:
		case igtbl_dataType.UInt32:
		case igtbl_dataType.Int64:
		case igtbl_dataType.UInt64:
			if (typeof (value) == "number")
				return value;
			if (typeof (value) == "boolean")
				return (value ? 1 : 0);
			if (value.toString().toLowerCase() == "true")
				return 1;
			value = parseInt(value.toString(), 10);
			if (value.toString() == "NaN")
				value = 0;
			break;
		case igtbl_dataType.Single:
		case igtbl_dataType.Double:
		case igtbl_dataType.Decimal:
			if (typeof (value) == "float")
				return value;
			value = parseFloat(value.toString());
			if (value.toString() == "NaN")
				value = 0.0;
			break;
		case igtbl_dataType.Boolean:
			if (!value || value.toString() == "0" || value.toString().toLowerCase() == "false")
				value = false;
			else
				value = true;
			break;
		case igtbl_dataType.DateTime:
			
			var d;
			if (typeof (value) == "string")
			{
				var dtV = value.split(".");
				var ms = 0, lastPart = dtV.length > 1 ? dtV[1].substr(dtV[1].length - 3).toUpperCase() : "";
				if (dtV.length > 1 && (lastPart == " AM" || lastPart == " PM"))
				{
					ms = igtbl_parseInt(dtV[1]);
					dtV[0] += lastPart;
				}
				else
					dtV[0] = value;
				d = new Date(dtV[0]);
				if (!isNaN(d))
					d.setMilliseconds(ms);
			}
			else
				d = new Date(value);
			if (d.toString() != "NaN" && d.toString() != "Invalid Date")
				value = d;
			else
				value = igtbl_string.trim(value.toString());
			delete d;
			break;
		case igtbl_dataType.String:
			break;
		default:
			value = igtbl_string.trim(value.toString());
	}
	return value;
}

function igtbl_dateToString(date)
{
	if (date == null)
		return "";
	if (typeof (date.getFullYear) != "function")
		return date.toString();
	var month = date.getMonth();
	var day = date.getDate();
	var year = date.getFullYear();
	var hour = date.getHours();
	var min = date.getMinutes();
	var sec = date.getSeconds();
	var ms = date.getMilliseconds();

	
	return (month + 1).toString() + "/" + day.toString() + "/" +
		(year.toString().length > 4 ? year.toString().substr(0, 4) : year) + " " +
		(hour == 0 ? "12" : (hour % 12).toString()) + ":" + (min < 10 ? "0" : "") +
		min + ":" + (sec < 10 ? "0" : "") + sec +
		igtbl_dateMsToString(date) + " " + (hour < 12 ? "AM" : "PM");
}

function igtbl_dateMsToString(date)
{
	var ms = date.getMilliseconds();
	if (ms == 0)
		return "";
	if (ms < 10)
		return ".00" + ms.toString();
	if (ms < 100)
		return ".0" + ms.toString();
	return "." + ms.toString();
}

function igtbl_parseInt(inValue)
{
	var outValue = parseInt(inValue, 10);
	if (isNaN(outValue))
		outValue = 0;
	return outValue;
}

function igtbl_trim(s)
{
	if (!s)
		return s;
	s = s.toString();
	var result = s;
	for (var i = 0; i < s.length; i++)
		if (s.charAt(i) != ' ')
		break;
	result = s.substr(i, s.length - i);
	for (var i = result.length - 1; i >= 0; i--)
		if (result.charAt(i) != ' ')
		break;
	result = result.substr(0, i + 1);
	return result;
}


igtbl_debug =
{
	
	writeLine : function(message)
	{
	},
	
	writeStackTrace : function(startingPoint)
	{
	} 
	
}
function igtbl_contains(e1, e2)
{
	if (e1.contains)
		return e1.contains(e2);
	var contains = false;
	var p = e2;
	while (p && p != e1)
		p = p.parentNode;
	return p == e1;
}

function igtbl_getStyleSheet(name)
{
	var nameAr = name.split(".");
	if (nameAr.length > 2)
		return null;
	else if (nameAr.length == 2)
	{
		if (ig_csom.IsIE)
			nameAr[0] = nameAr[0].toUpperCase();
		else
			nameAr[0] = nameAr[0].toLowerCase();
		name = nameAr.join(".");
	}
	else
		name = "." + name;
	for (var i = 0; i < document.styleSheets.length; i++)
	{
		
		var ssrules = null;
		try
		{
			if (ig_csom.IsIE)
				ssrules = document.styleSheets[i].rules;
			else
				ssrules = document.styleSheets[i].cssRules;
		} catch (e) { ; }
		if (ssrules)
			for (var j = 0; j < ssrules.length; j++)
			if (ssrules[j].selectorText == name)
			return ssrules[j].style;
	}
	return null;
}

function igtbl_getCurrentStyleProperty(e, propName, forceCalc)
{
	if (e && e.tagName && ig_csom.IsIE && !forceCalc)
		return e.currentStyle[propName];
	else
	{
		if (e && e.tagName && e.style[propName])
			return e.style[propName];
		var className = e;
		if (e && e.tagName)
			className = e.className;
		if (className)
		{
			var clsNames = className.split(" ");
			clsNames = clsNames.reverse();
			for (var i = 0; i < clsNames.length; i++)
			{
				var style = igtbl_getStyleSheet(clsNames[i]);
				if (style && style[propName])
					return style[propName];
			}
		}
	}
	return "";
}

function igtbl_getArray(elem)
{
	if (!elem) return null;
	var a = new Array();
	if (!elem.length)
		a[0] = elem;
	else
		for (var i = 0; i < elem.length; i++)
		a[i] = elem[i];
	return a;
}

function igtbl_getCheckboxFromElement(oCellE)
{
	var oChk = null;
	for (var i = 0; i < oCellE.childNodes.length; i++)
	{
		if (oCellE.childNodes[i].tagName == "INPUT" && oCellE.childNodes[i].type == "checkbox")
			oChk = oCellE.childNodes[i];
		else
			oChk = igtbl_getCheckboxFromElement(oCellE.childNodes[i])
		if (oChk) break;
	}
	return oChk;
}

igtbl_dom =
{
	
	isParent: function(child, parent)
	{
		if (child == null || parent == null) return false;

		var possibleParent = child.parentNode;
		while (possibleParent != null)
		{
			if (possibleParent == parent)
				return true;

			possibleParent = possibleParent.parentNode;
		}
		return false;

	},

	
	isTag: function(element, type)
	{
		if (!element)
			return false;

		if (!igtbl_array.isList(type))
			type = [type];

		for (var x = 0; x < type.length; x++)
		{
			if (element.tagName === type[x])
				return true;
			else if (typeof (type[x].toUpperCase) != "undefined" && element.tagName &&
				element.tagName.toUpperCase() === type[x].toUpperCase())
				return true;
		}

		return false;
	},

	hasVisibleStyle: function(elem)
	{
		if (igtbl_dom.css.getComputedStyle(elem, "display") != "none" &&
			igtbl_dom.css.getComputedStyle(elem, "visibility") != "hidden")
		{
			return true;
		}

		return false;
	},

	isVisible: function(elem)
	{
		while (elem && elem.tagName != (igtbl_isXHTML ? "HTML" : "BODY"))
		{
			
			if (elem.style && elem.style.display == "none" ||
				elem.tagName != "FORM" && elem.tagName != "BODY" && !elem.offsetHeight)
				return false;
			elem = elem.parentNode;
		}
		return true;
	},

	find:
	{
		// use igcsom.getElementById wherever is possible 
		elementById: function(tagId)
		{
			
			if (!document) return;

			var obj = ig_csom.getElementById(tagId);
			if (obj && obj.length && typeof (obj.tagName) == "undefined")
			{
				var i = 0;
				while (i < obj.length && (obj[i].id != tagId || !igtbl_isVisible(obj[i]))) i++;
				if (i < obj.length) obj = obj[i];
				else obj = obj[0];
			}
			return obj;
		},

		
		parentByTag: function(element, parentType)
		{
			var parent = element;
			while (parent && !igtbl_dom.isTag(parent, parentType))
				parent = parent.parentNode;
			return parent;
		},

		parentForm: function(elem)
		{
			if (!elem) return null;
			var thisForm = igtbl_dom.find.parentByTag(elem, "FORM");
			
			if (!thisForm && document.forms && document.forms.length == 1)
				thisForm = document.forms[0];
			return thisForm;
		},

		
		childByTag: function(element, childType)
		{
			if (element)
			{
				for (var x = 0; x < element.childNodes.length; x++)
				{
					var child = element.childNodes[x];
					if (igtbl_dom.isTag(child, childType))
						return child;
					var foundChild = igtbl_dom.find.childByTag(child, childType);
					if (foundChild)
						return foundChild;
				}
			}
			return null;
		},

		childrenByPath: function(element, path)
		{
			var pathElements = path.split("/");
			var matches = [];
			if (pathElements.length > 0)
			{
				var elementToFind = pathElements[0];
				for (var x = 0; x < element.childNodes.length; x++)
				{
					var childNode = element.childNodes[x];
					if (igtbl_dom.isTag(childNode, elementToFind))
					{
						if (elementToFind == path)
							matches.push(childNode);
						else
							return igtbl_dom.find.childrenByPath(childNode, path.substring(elementToFind.length + 1));
					}
				}
			}
			return matches;
		},

		childById: function(parent, id)
		{
			if (!id || !parent.childNodes || !parent.childNodes.length)
				return null;
			for (var i = 0; i < parent.childNodes.length; i++)
				try
			{
				if (parent.childNodes[i].id && parent.childNodes[i].id == id)
					return parent.childNodes[i];
				var che = igtbl_dom.find.childById(parent.childNodes[i], id);
				if (che)
					return che;
			} catch (ex) { ; }
			return null;
		},

		childrenById: function(parent, id)
		{
			if (!id || !parent.childNodes || !parent.childNodes.length)
				return null;
			var array = [];
			for (var i = 0; i < parent.childNodes.length; i++)
			{
				try
				{
					if (parent.childNodes[i].id && parent.childNodes[i].id == id)
						array[array.length] = parent.childNodes[i];
					var ches = igtbl_dom.find.childrenById(parent.childNodes[i], id);
					if (ches)
						array = array.concat(ches);
				}
				catch (ex) { ; }
			}
			if (array.length)
				return array;
			return null;
		},

		rootNode: function(element)
		{
			if (!element) return;
			while (element.parentNode)
				element = element.parentNode;
			return element;
		}
	},

	css:
	{
		getComputedStyle: function(element, property)
		{
			if (typeof (window.getComputedStyle) != "undefined")
				return window.getComputedStyle(element, "")[property];

			if (typeof (element.currentStyle) != "undefined")
				return element.currentStyle[property];

			return element.style[property];
		},

		removeClass: function(element, className)
		{
			element.className = element.className.replace(className, "");
		},

		replaceClass: function(element, oldClassName, newClassName)
		{
			igtbl_dom.css.removeClass(element, oldClassName);
			if (element.className.indexOf(newClassName) == -1)
				element.className += " " + newClassName;
		}
	},

	dimensions:
	{
		
		bordersWidth: function(element, includePadding)
		{
			var width = 0;
			if (element.offsetWidth && element.clientHeight)
				width += element.offsetWidth - element.clientWidth;
			if (includePadding && (!ig_csom.IsIE7Compat || igtbl_isXHTML))
			{
				width += igtbl_number.fromString(igtbl_dom.css.getComputedStyle(element, "paddingLeft"));
				width += igtbl_number.fromString(igtbl_dom.css.getComputedStyle(element, "paddingRight"));
			}
			return width;
		},

		
		bordersHeight: function(element, includePadding)
		{
			var height = 0;
			if (element.offsetHeight && element.clientHeight)
				height += element.offsetHeight - element.clientHeight;
			if (includePadding && (!ig_csom.IsIE7Compat || igtbl_isXHTML))
			{
				height += igtbl_number.fromString(igtbl_dom.css.getComputedStyle(element, "paddingTop"));
				height += igtbl_number.fromString(igtbl_dom.css.getComputedStyle(element, "paddingBottom"));
			}
			return height;
		},

		hasPercentageWidth: function(elem)
		{
			var width = igtbl_dom.css.getComputedStyle(elem, "width");
			if (width && width.indexOf("%") > 0)
				return true;

			if (elem.width && elem.width.indexOf("%") > 0)
				return true;
		},

		hasEmptyWidth: function(elem)
		{
			var width = igtbl_dom.css.getComputedStyle(elem, "width");
			if (width == "auto")
				return true;

			return false;
		}
	},

	table:
	{
		
		allPercentageColumns: function(elem, startIndex)
		{
			if (!igtbl_dom.isTag(elem, "TABLE")) return false;

			if (startIndex == undefined) startIndex = 0;
			var cols = igtbl_dom.find.childrenByPath(elem, "colgroup/col");
			for (var x = startIndex; x < cols.length; x++)
			{
				var col = cols[x];
				if (igtbl_dom.hasVisibleStyle(col))
				{
					
					if (igtbl_dom.dimensions.hasEmptyWidth(col))
						return true;
					if (!igtbl_dom.dimensions.hasPercentageWidth(col))
						return false;
				}
			}

			return true;
		},
		
		hasPercentageColumns: function(elem, startIndex, ignoreVisibility)
		{
			if (!igtbl_dom.isTag(elem, "TABLE")) return false;

			if (startIndex == undefined) startIndex = 0;
			var cols = igtbl_dom.find.childrenByPath(elem, "colgroup/col");
			for (var x = startIndex; x < cols.length; x++)
			{
				var col = cols[x];
				if (igtbl_dom.hasVisibleStyle(col) || ignoreVisibility)
				{
					if (igtbl_dom.dimensions.hasEmptyWidth(col))
						return true;
					if (igtbl_dom.dimensions.hasPercentageWidth(col))
						return true;
				}
			}

			return false;
		}
	}
};

igtbl_stylesheet =
{
	addRule: function(rule)
	{
		if (typeof (document.styleSheets) != "undefined" && document.styleSheets.length > 0)
		{
			document.styleSheets[0].insertRule(rule, 0);
		}
	}
};

igtbl_event =
{
	getEvent: function(evnt)
	{
		if (typeof (evnt) != "undefined")
			return evnt;
		return event;
	},

	addEventListener: function(obj, eventName, fRef, dispatch)
	{
		if (typeof (dispatch) == "undefined")
			dispatch = true;
		if (obj.addEventListener)
			return obj.addEventListener(eventName, fRef, dispatch);
		else
		{
			var oldHandler = obj["on" + eventName];
			eval("obj.on" + eventName + "=fRef;");
			return oldHandler;
		}
	},

	removeEventListener: function(obj, eventName, fRef, oldfRef, dispatch)
	{
		if (typeof (dispatch) == "undefined")
			dispatch = true;
		if (obj.removeEventListener)
			return obj.removeEventListener(eventName, fRef, dispatch);
		else
			eval("obj.on" + eventName + "=oldfRef;");
	}
};

igtbl_browser =
{

}


function igtbl_getClipboardData()
{

	if (window.clipboardData)
	{

		return window.clipboardData.getData("Text");

	}

	else if (ig_shared.IsFireFox || ig_shared.IsNetscape)
	{

		netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect'); 

		var clip = Components.classes["@mozilla.org/widget/clipboard;1"].createInstance(Components.interfaces.nsIClipboard);

		var trans = Components.classes["@mozilla.org/widget/transferable;1"].createInstance(Components.interfaces.nsITransferable);

		trans.addDataFlavor("text/unicode");

		clip.getData(trans, clip.kGlobalClipboard);

		var str = new Object();

		var len = new Object();

		trans.getTransferData("text/unicode", str, len);

		if (str)

			return str.value.QueryInterface(Components.interfaces.nsISupportsString).toString();

	}

}


function igtbl_setClipboardData(copytext)
{

	if (window.clipboardData)
	{

		window.clipboardData.setData("Text", copytext);

	}

	else if (ig_shared.IsFireFox || ig_shared.IsNetscape)
	{

		netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect'); 

		var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard); 

		var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable); 

		trans.addDataFlavor('text/unicode'); 

		var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);

		str.data = copytext;

		trans.setTransferData("text/unicode", str, copytext.length * 2);

		var clipid = Components.interfaces.nsIClipboard;

		clip.setData(trans, null, clipid.kGlobalClipboard);

	}

	else

		return false;

	return true;

}


function igtbl_isAChildOfB(a, b)
{

	if (a == null || b == null) return false;

	while (a != null)
	{

		if (a == b) return true;

		a = a.parentNode;

	}

	return false;

}


function igtbl_getThisForm(elem)
{

	if (!elem)

		return null;

	var thisForm = elem.parentNode;

	while (thisForm && thisForm.tagName != "FORM")

		thisForm = thisForm.parentNode;

	

	if (!thisForm && document.forms && document.forms.length == 1)

		thisForm = document.forms[0];

	return thisForm;

}


function igtbl_addEventListener(obj, eventName, fRef, dispatch)
{

	if (typeof (dispatch) == "undefined")

		dispatch = true;

	if (obj.addEventListener)

		return obj.addEventListener(eventName, fRef, dispatch);

	else
	{

		var oldHandler = obj["on" + eventName];

		eval("obj.on" + eventName + "=fRef;");

		return oldHandler;

	}

}

function igtbl_removeEventListener(obj, eventName, fRef, oldfRef, dispatch)
{

	if (typeof (dispatch) == "undefined")

		dispatch = true;

	if (obj.removeEventListener)

		return obj.removeEventListener(eventName, fRef, dispatch);

	else

		eval("obj.on" + eventName + "=oldfRef;");

}


function igtbl_isVisible(elem)
{

	while (elem && elem.tagName != (igtbl_isXHTML ? "HTML" : "BODY"))
	{

		if (elem.style && elem.style.display == "none" ||
			elem.tagName != "FORM" && elem.tagName != "BODY" && !elem.offsetHeight)

			return false;

		elem = elem.parentNode;

	}

	return true;

}


// use igcsom.getElementById wherever is possible 

function igtbl_getElementById(tagId)
{


	if (!document) return;


	var obj = ig_csom.getElementById(tagId);

	if (obj && obj.length && typeof (obj.tagName) == "undefined")
	{

		var i = 0;

		while (i < obj.length && (obj[i].id != tagId || !igtbl_isVisible(obj[i]))) i++;

		if (i < obj.length) obj = obj[i];

		else obj = obj[0];

	}

	return obj;

}


function igtbl_getChildElementsById(parent, id)
{

	if (!id || !parent.childNodes || !parent.childNodes.length)

		return null;

	var array = [];

	for (var i = 0; i < parent.childNodes.length; i++)

		try
	{

		if (parent.childNodes[i].id && parent.childNodes[i].id == id)

			array[array.length] = parent.childNodes[i];

		var ches = igtbl_getChildElementsById(parent.childNodes[i], id);

		if (ches)

			array = array.concat(ches);

	}

	catch (ex) { ; }

	if (array.length)

		return array;

	return null;

}


function igtbl_getChildElementById(parent, id)
{

	if (!id || !parent.childNodes || !parent.childNodes.length)

		return null;

	for (var i = 0; i < parent.childNodes.length; i++)

		try
	{

		if (parent.childNodes[i].id && parent.childNodes[i].id == id)

			return parent.childNodes[i];

		var che = igtbl_getChildElementById(parent.childNodes[i], id);

		if (che)

			return che;

	} catch (ex) { ; }

	return null;

}
igtbl_array =
{
	
	contains: function(array, element)
	{
		for (var x in array)
		{
			if (array[x] === element)
				return true;
		}
		return false;
	},

	
	exclude: function(array, exclude)
	{
		var newArray = [];
		for (var x in array)
		{
			if (!igtbl_array.contains(exclude, array[x]))
				newArray.push(array[x]);
		}
		return newArray;
	},

	isList: function(value)
	{
		return value != null &&
			typeof (value) == "object" &&
			typeof (value.length) == "number" &&
			(value.length == 0 || typeof (value[0]) != "undefined");
	},

	hasElements: function(array)
	{
		if (!array)
			return false;
		for (element in array)
			if (array[element] != null)
			return true;
		return false;
	},

	getLength: function(obj)
	{
		var count = 0;
		for (var item in obj)
			count++;
		return count;
	}
};

function igtbl_arrayHasElements(array)
{
	if (!array)
		return false;
	for (element in array)
		if (array[element] != null)
		return true;
	return false;
}

function igtbl_getLength(obj)
{
	var count = 0;
	for (var item in obj)
		count++;
	return count;
}
igtbl_nav =
{
	splitUrl: function(url)
	{
		var targetFrame = null;
		if (url.substr(0, 1) == "@")
		{
			targetFrame = "_blank";
			url = url.substr(1);
			var cb = -1;
			if (url.substr(0, 1) == "[" && (cb = url.indexOf("]")) > 1)
			{
				targetFrame = url.substr(1, cb - 1);
				url = url.substr(cb + 1);
			}
		}
		return [url, targetFrame];
	},

	navigateUrl: function(url)
	{
		var urls = igtbl_splitUrl(url);
		ig_csom.navigateUrl(urls[0], urls[1]);
		igtbl_dispose(urls);
	}
}

function igtbl_escape(text)
{
	text = escape(text);
	return text.replace(/\+/g, "%2b");
}


function igtbl_splitUrl(url)
{
	var targetFrame = null;
	if (url.substr(0, 1) == "@")
	{
		targetFrame = "_blank";
		url = url.substr(1);
		var cb = -1;
		if (url.substr(0, 1) == "[" && (cb = url.indexOf("]")) > 1)
		{
			targetFrame = url.substr(1, cb - 1);
			url = url.substr(cb + 1);
		}
	}
	return [url, targetFrame];
}

function igtbl_navigateUrl(url)
{
	var urls = igtbl_splitUrl(url);
	ig_csom.navigateUrl(urls[0], urls[1]);
	igtbl_dispose(urls);
}
function igtbl_getLeftPos(e, cc, oe)
{
	return igtbl_getAbsolutePos("Left", e, cc, oe);
}


function igtbl_getLeftPos_header(e, cc, oe)
{
	return igtbl_getAbsolutePos_header("Left", e, cc, oe);
}

function igtbl_getTopPos(e, cc, oe)
{
	return igtbl_getAbsolutePos("Top", e, cc, oe);
}
function igtbl_getAbsolutePos(where, e, cc, oe)
{
	
	if (ig_csom.IsIE && igtbl_isXHTML && e.getBoundingClientRect)
	{
        
        return igtbl_getAbsBoundsInternal(where,e);
	}
	return igtbl_getAbsolutePos2(where, e, cc, oe);
}

function igtbl_getAbsolutePos_header(where, e, cc, oe)
{
	if (ig_csom.IsIE7Compat && igtbl_isXHTML && e.getBoundingClientRect)
	{
        igtbl_getAbsBoundsInternal(where,e);
	}
	return igtbl_getAbsolutePos2(where, e, cc, oe);
}

function igtbl_getAbsBoundsInternal(where,e)
{
    switch (where)
	{
		case "Left":
			return igtbl_getAbsBounds(e).x;
		case "Top":
			return igtbl_getAbsBounds(e).y;
	}
}

function igtbl_getAbsolutePos2(where, e, cc, oe)
{

	var offs = "offset" + where, cl = "client" + where, bw = "border" + where + "Width", sl = "scroll" + where;
	var crd = e[offs];
	if (e[cl] && cc != false)
		crd += e[cl];
	if (typeof (oe) == "undefined")
		oe = null;
	var tmpE = e.offsetParent, cSb = true;
	while (tmpE != null && tmpE != oe)
	{
		crd += tmpE[offs];
		if ((tmpE.tagName == "DIV" || tmpE.tagName == "TD") && tmpE.style[bw])
		{
			var bwv = parseInt(tmpE.style[bw], 10);
			if (!isNaN(bwv))
				crd += bwv;
		}
		if (cSb && typeof (tmpE[sl]) != "undefined")
		{
			var op = tmpE.offsetParent, t = tmpE;
			while (t && t != op && t.tagName != (igtbl_isXHTML ? "HTML" : "BODY"))
			{
				if (t[sl])
					crd -= t[sl];
				t = t.parentNode;
			}
			
		}
		if (tmpE[cl] && cc != false)
			crd += tmpE[cl];
		tmpE = tmpE.offsetParent;


	}
	if (tmpE && tmpE[cl] && cc != false)
		crd += tmpE[cl];
	return crd;
}

function igtbl_getAbsBounds(elem, g, forAbsPos)
{
	
	if (elem.offsetWidth == 0)
		return {x:0,y:0,w:0,h:0};
	var r = new Object();
	if (typeof(elem.getBoundingClientRect) != "undefined")
	{
		var rect = elem.getBoundingClientRect();
		r.x = rect.left;
		r.y = rect.top;
		r.w = rect.right - rect.left;
		r.h = rect.bottom - rect.top;
	}
	else if (ig_csom.IsFireFox && typeof(document.getBoxObjectFor) != "undefined")
	{
		var rect = document.getBoxObjectFor(elem);
		r.x = rect.x;
		r.y = rect.y;
		r.w = rect.width;
		r.h = rect.height;
	}
	else
	{
		return igtbl_getAbsBounds2(elem, g);
	}
	var tmpE = elem;
	var passedMain = false;
	while (tmpE && tmpE != document.documentElement)
	{
		passedMain |= g != null && tmpE == g.MainGrid;
		if (forAbsPos)
		{
			
		}
		else
		{			
			if (tmpE.scrollLeft)
			{
				r.x += tmpE.scrollLeft;
			}		
			if (tmpE.scrollTop)
			{
				r.y += tmpE.scrollTop;
			}
			
			
			if (!igtbl_isXHTML)
			{
				var left = parseInt(tmpE.style.left);
				if (!isNaN(left))
				{
					if (left < 0)
						r.x -= left;
				}
			}
		}
		if (tmpE == elem)
		{
			
			if (typeof (tmpE.currentStyle) != "undefined")
			{
				if (!igtbl_isXHTML || !ig_csom.IsIE7Compat)
				{
					var cs = tmpE.currentStyle;
					var bw = 0;
					var b = cs.borderLeftWidth.toLowerCase();
					if (b == "thin")
						bw++;
					else if (b == "medium")
						bw += 3;
					else if (b == "thick")
						bw += 5;
					else
						bw += igtbl_parseInt(b);
					
					
					b = cs.borderRightWidth.toLowerCase();
					if (b == "thin")
						bw++;
					else if (b == "medium")
						bw += 3;
					else if (b == "thick")
						bw += 5;
					else
						bw += igtbl_parseInt(b);
					r.w -= bw;

					bw = 0;
					b = cs.borderTopWidth.toLowerCase();
					if (b == "thin")
						bw++;
					else if (b == "medium")
						bw += 3;
					else if (b == "thick")
						bw += 5;
					else
						bw += igtbl_parseInt(b);
					
					
					b = cs.borderBottomWidth.toLowerCase();
					if (b == "thin")
						bw++;
					else if (b == "medium")
						bw += 3;
					else if (b == "thick")
						bw += 5;
					else
						bw += igtbl_parseInt(b);
					r.h -= bw;
				}
			}
			else
			{
				if (tmpE.offsetWidth && tmpE.clientWidth)
				{
					var xDiff = tmpE.offsetWidth - tmpE.clientWidth;
					r.x -= xDiff / 2;
					r.w -= xDiff;
				}
				if (tmpE.offsetHeight && tmpE.clientHeight)
				{
					var yDiff = tmpE.offsetHeight - tmpE.clientHeight;
					r.y -= yDiff / 2;
					r.h -= yDiff;
				}
			}
		}
		tmpE = tmpE.offsetParent;
	}
	
	if (!igtbl_isXHTML && ig_csom.IsFireFox && forAbsPos && passedMain && g != null) 
	{
		var divElement = g.getDivElement();
		r.x -= divElement.scrollLeft;
		r.y -= divElement.scrollTop;
	}
	
	
	if ((tmpE = document.documentElement) && typeof (elem.getBoundingClientRect) != "undefined")
	{
		
		
		try
		{
			var frameEl = tmpE.document.parentWindow.frameElement;
			
			if (frameEl && (frameEl.tagName == "IFRAME" || frameEl.tagName == "FRAME"))
			{
				var fb = frameEl.getAttribute("frameBorder");
				
				if ((fb && (fb === "0" || fb.toLowerCase() === "no")) || (fb === "" && frameEl.tagName == "FRAME"))
				{
					r.x += 2;
					r.y += 2;
				}
			}
		}
		catch (exc) { ; }
		if (tmpE.scrollLeft)
		{
			r.x += tmpE.scrollLeft;
		}
		if (tmpE.scrollTop)
		{
			r.y += tmpE.scrollTop;
		}
		if (!igtbl_isXHTML && (tmpE = document.body))
		{
			if (tmpE.scrollLeft)
			{
				r.x += tmpE.scrollLeft;
			}
			if (tmpE.scrollTop)
			{
				r.y += tmpE.scrollTop;
			}
		}
	}
	return r;
}
function igtbl_getAbsBounds2(elem, g)
{
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
	while (e != null)
	{
		if (ok < 1 || e == body)
		{
			z = e.offsetLeft;
			if (z)
			{
				r.x += z;
				
				if (ig_csom.IsIE7Compat && !igtbl_isXHTML)
				{
					var borderWidthX = parseInt(e.currentStyle.borderLeftWidth);
					if (!isNaN(borderWidthX) && e.tagName != "TD")
						r.x += borderWidthX;
				}
			}
			z = e.offsetTop;
			if (z)
			{
				r.y += z;
				
				if (ig_csom.IsIE7Compat && !igtbl_isXHTML)
				{
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
		if (e == body && pe.tagName == "DIV")
		{
			e = pe;
			ok++;
		}
	}
	try
	{
		
		if (!document.elementFromPoint || window.frameElement || body.currentStyle && (body.currentStyle.borderWidth == "0px" || body.currentStyle.borderStyle == "none"))
			return r;
	}
	catch (e)
	{
		return r;
	}
	var i = 1, x = r.x, y = r.y, x0 = igtbl_getBodyScrollLeft(), y0 = igtbl_getBodyScrollTop();
	while (++i < 16)
	{
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
	while (++i < 22)
	{
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
		else
		{
			i = z = 1;
			x++;
		}
	}
	r.x = x - 1;
	r.y = y;
	return r;
}

function igtbl_getRelativePos(gn, e, where, ignoreTableBorder)
{
	var g = igtbl_getGridById(gn);
	var mainGrid = igtbl_getElementById(gn + "_main");
	var passedMainGrid = false;
	var offs = "offset" + where, bw = "border" + where + "Width";
	var ovfl = "overflow", ovflC = ovfl + (where == "Left" ? "X" : "Y");
	var crd = e[offs];
	var parent = e.offsetParent;

	if (!parent)
	{
		if (e.tagName == "TD" || e.tagName == "TH")
		{
			parent = e.parentNode;
			while (parent && parent.tagName != "TABLE")
			{
				parent = parent.parentNode;
			}
		}
	}

	while ((parent != null && parent.tagName != (igtbl_isXHTML ? "HTML" : "BODY") && (
	
		!passedMainGrid || 
		parent.style.position != "relative" || (parent.style.position == "relative" && parent.id == "G_" + gn))))
	{
		passedMainGrid = passedMainGrid || igtbl_isAChildOfB(mainGrid.parentNode, parent);
		if (passedMainGrid && (parent.style.position == "absolute" || parent.style[ovflC] && parent.style[ovflC] != "visible" || parent.style[ovfl] && parent.style[ovfl] != "visible"))
			break;
		crd += parent[offs];
		if (ig_csom.IsIE7Compat &&
			(parent.tagName == "DIV" || parent.tagName == "TD" || parent.tagName == "FIELDSET"
			) &&
			parent.style[bw])
		{
			var bwv = parseInt(parent.style[bw], 10);
			if (!isNaN(bwv))
				crd += bwv;
		}
		if (parent == mainGrid)
			passedMainGrid = true;
		parent = parent.offsetParent;
	}

	

	var deductScroll = true;
	if (where == "Top" && g.StatHeader)
	{
		while (e)
		{
			if (e == g.StatHeader.Element)
			{
				deductScroll = false;
				break;
			}
			e = e.parentNode;
		}
	}
	if (deductScroll)

		crd -= g.Element.offsetParent["scroll" + where]
	return crd;
}

igtbl_regExp =
{
	
	escape: function(text, exclusions)
	{
		
		if (typeof (text) == "undefined" || text == null) return "";

		var characters = ["\\", "^", "$", "*", "+", "?", "!", "-", "=", ":", ",", ".", "|", "(", ")", "{", "}", "[", "]"];
		var includedCharacters = characters;
		if (exclusions)
			includedCharacters = igtbl_array.exclude(characters, exclusions);

		for (var x in includedCharacters)
			includedCharacters[x] = "\\" + includedCharacters[x];

		return text.replace(new RegExp("(" + includedCharacters.join("|") + ")", "g"), '\\$1');
	}
};

function igtbl_getRegExpSafe(val)
{
	
	if (typeof (val) == "undefined" || val == null)
		return "";
	
	var res = val.toString();
	res = res.replace(/\\/g, "\\\\");
	
	res = res.replace(/\*/g, "\\*");
	res = res.replace(/\$/g, "\\$");
	res = res.replace(/\+/g, "\\+");
	res = res.replace(/\?/g, "\\?");
	res = res.replace(/\,/g, "\\,");
	res = res.replace(/\./g, "\\.");
	res = res.replace(/\:/g, "\\:");
	res = res.replace(/\=/g, "\\=");
	res = res.replace(/\-/g, "\\-");
	res = res.replace(/\!/g, "\\!");
	res = res.replace(/\|/g, "\\|");
	res = res.replace(/\(/g, "\\(");
	res = res.replace(/\)/g, "\\)");
	res = res.replace(/\[/g, "\\[");
	res = res.replace(/\]/g, "\\]");
	res = res.replace(/\{/g, "\\{");
	res = res.replace(/\}/g, "\\}");
	return res;
}
igtbl_xml =
{
	
	createXmlElement: function(doc, tagName, ns)
	{
		if (typeof (doc.createNode) != "undefined") // IE
			return doc.createNode(1, tagName, ns);
		else if (doc.createElement) // Firefox
			return doc.createElement(tagName);
	},

	
	createXmlTextNode: function(doc, ns)
	{
		
		if (typeof (doc.createNode) != "undefined") // IE
			return doc.createNode(4, "", ns);
		else if (doc.createCDATASection)// Firefox 
			return doc.createCDATASection("");
	},

	createDocumentFromString: function(xml)
	{
		if (!ig_csom.IsIE)
		{
			var objDOMParser = new DOMParser();
			return objDOMParser.parseFromString(xml, "text/xml");
		}
		else
		{
			var doc = new ActiveXObject("Microsoft.XMLDOM")
			doc.async = "false";
			doc.loadXML(xml);
			return doc;
		}
	},

	disposeDocument: function(node)
	{
		if (node.parentNode)
			node = igtbl_dom.find.rootNode(node);
		igtbl_xml.disposeNode(node);
	},

	disposeNode: function(node)
	{
		while (node.childNodes.length > 0)
			igtbl_xml.disposeNode(node.childNodes[0]);

		if (node.parentNode)
		{
			if (typeof (node.parentNode.removeChild) != "undefined")
				node.parentNode.removeChild(node);
		}
	}
};
