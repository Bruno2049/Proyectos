/*
* ig_WebGrid_dom.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


// General object. Where it all starts.
function igtbl_Object(type)
{
	if (arguments.length > 0)
		this.init(type);
}
igtbl_Object.prototype.init = function(type)
{
	this.Type = type;
}

// Web object. The one with an HTML element attached.
igtbl_WebObject.prototype = new igtbl_Object();
igtbl_WebObject.prototype.constructor = igtbl_WebObject;
igtbl_WebObject.base = igtbl_Object.prototype;
function igtbl_WebObject(type, element, node)
{
	if (arguments.length > 0)
		this.init(type, element, node);
}
igtbl_WebObject.prototype.init = function(type, element, node, viewState)
{
	igtbl_WebObject.base.init.apply(this, [type]);
	if (element)
	{
		this.Id = element.id;
		this.Element = element;
	}
	if (node)
		this.Node = node;
	if (viewState)
		this.ViewState = viewState;
}
igtbl_WebObject.prototype.get = function(name)
{
	if (this.Node)
		return this.Node.getAttribute(name);
	if (this.Element)
		return this.Element.getAttribute(name);
	return null;
}
igtbl_WebObject.prototype.set = function(name, value)
{
	if (this.Node)
		this.Node.setAttribute(name, value);
	else if (this.Element)
		this.Element.setAttribute(name, value);
	if (this.ViewState)
		ig_ClientState.setPropertyValue(this.ViewState, name, value);
}

// Band object
igtbl_Band.prototype = new igtbl_WebObject();
igtbl_Band.prototype.constructor = igtbl_Band;
igtbl_Band.base = igtbl_WebObject.prototype;
function igtbl_Band(grid, node, index
	, bandsInitArray, colsInitArray
)
{
	if (arguments.length > 0)
		this.init(grid, node, index
			, bandsInitArray, colsInitArray
		);
}
var igtbl_ptsBand = [
"init",
function(grid, node, index
	, bandsInitArray, colsInitArray
)
{
	igtbl_Band.base.init.apply(this, ["band", null, node]);

	this.Grid = grid;
	this.Index = index;
	var defaultProps = new Array("Key", "AllowAddNew", "AllowColSizing", "AllowDelete", "AllowSort", "ItemClass", "AltClass", "AllowUpdate",
		"CellClickAction", "ColHeadersVisible", "ColFootersVisible", "CollapseImage", "CurrentRowImage",
		"CurrentEditRowImage", "DefaultRowHeight", "EditCellClass", "Expandable", "ExpandImage",
		"FooterClass", "GroupByRowClass", "GroupCount", "HeaderClass", "HeaderClickAction", "Visible",
		"IsGrouped", "ExpAreaClass", "NonSelHeaderClass", "RowLabelClass", "SelGroupByRowClass", "SelHeadClass",
		"SelCellClass", "RowSizing", "SelectTypeCell", "SelectTypeColumn", "SelectTypeRow", "RowSelectors",
		"NullText", "RowTemplate", "ExpandEffects", "AllowColumnMoving", "ClientSortEnabled", "Indentation",
		"RowLabelWidth", "DataKeyField", "HeaderHTML", "FooterHTML", "FixedHeaderIndicator", "AllowRowNumbering",
		"IndentationType"
		, "HasHeaderLayout", "HasFooterLayout", "GroupByColumnsHidden", "AddNewRowVisible", "AddNewRowView",
		"AddNewRowStyle"
		, "_optSelectRow"
	
		, "ShowAllCondition", "ShowEmptyCondition", "ShowNonEmptyCondition",
		"Filter_AllString", "Filter_EmptyString", "Filter_NonEmptyString", "ServerPassedFilters", "ApplyOnAdd", "FilterDropDownRowCount", "RowFilterMode", "FilterDropDownStyle", "FilterHighlightRowStyle"
        , "CellTitleMode", "HeaderTitleMode"
        , "FilterUIType", "AllowRowFiltering", "FilterRowView", "FilterEvaluationTrigger", "FilterRowStyle", "FilterOperandDropDownStyle", "FilterOperandItemStyle", "FilterOperandItemHoverStyle", "FilterOperandButtonStyle"
        , "FilterOperandStrings"
		, "SortingAlgorithm"
		);
	this.VisibleColumnsCount = 0;
	this.Columns = new Array();
	var bandArray;
	bandArray = bandsInitArray[index];
	var bandCount = 0;
	if (bandArray)
	{
		bandCount = bandsInitArray.length;
		for (var i = 0; i < bandArray.length; i++)
			this[defaultProps[i]] = bandArray[i];
		if (this.RowTemplate != "")
			this.ExpandEffects = new igtbl_expandEffects(this.ExpandEffects);
		if (this.HeaderHTML != "")
			this.HeaderHTML = unescape(this.HeaderHTML);
		if (this.FooterHTML != "")
			this.FooterHTML = unescape(this.FooterHTML);
	}
	else
		bandCount = this.Node.parentNode.selectNodes("Band").length;
	var colsArray = colsInitArray[index];
	if (!node)
	{
		for (var i = 0; i < colsArray.length; i++)
		{
			this.Columns[i] = new igtbl_Column(null, this, i
				, -1, colsArray[i]
			);
			if (!this.Columns[i].Hidden)
				this.VisibleColumnsCount++;
			if (this.Columns[i].getSelClass() != this.getSelClass())
				this._selClassDiffer = true;
		}
	}
	else
	{
		this.Columns.Node = this.Node.selectSingleNode("Columns");
		var columNodes = this.Columns.Node.selectNodes("Column");
		var nodeIndex = 0;
		for (var i = 0; i < columNodes.length; i++)
		{
			this.Columns[i] = new igtbl_Column(columNodes[i], this, i, nodeIndex
				, colsArray[i]
			);
			if (!this.Columns[i].Hidden && this.Columns[i].hasCells())
				this.VisibleColumnsCount++;
			if (!colsArray[i][33])
				nodeIndex++;
			if (this.Columns[i].getSelClass() != this.getSelClass())
				this._selClassDiffer = true;
		}
	}
	igtbl_dispose(defaultProps);

	if (node)
	{
		this.ColumnsOrder = "";
		for (var i = 0; i < this.Columns.length; i++)
			this.ColumnsOrder += this.Columns[i].Key + (i < this.Columns.length - 1 ? ";" : "");
	}
	this._filterPanels = new Object();

	if (this.ServerPassedFilters[0])
	{
		for (var itr = 0; itr < this.ServerPassedFilters.length; itr += 2)
		{
			var filterPanel = this._filterPanels[this.ServerPassedFilters[itr]];
			var filterSettingsOpCode = this.ServerPassedFilters[itr + 1][0];
			var filterSettingsValue = this.ServerPassedFilters[itr + 1][1];
			var colIndex = this.ServerPassedFilters[itr].split("_");
			colIndex = colIndex[colIndex.length - 1];
			if (!filterPanel)
			{
				var filteredColumn = this.Columns[colIndex];
				filterPanel = this._filterPanels[this.ServerPassedFilters[itr]] = new igtbl_FilterDropDown(filteredColumn);
			}
			filterPanel.setFilter(filterSettingsOpCode, filterSettingsValue, true);
		}
	}
	
	if (this.AllowAddNew == 1)
	{
		if (this.Index == 0)
			this.curTable = grid.Element;

		if (grid.AddNewBoxVisible)
		{
			var addNew = igtbl_getElementById(grid.Id + "_addBox");
			if (grid.AddNewBoxView == 0)
				this.addNewElem = addNew.childNodes[0].rows[0].cells[1].childNodes[0].rows[this.Index].cells[this.Index];
			else
				this.addNewElem = addNew.childNodes[0].rows[0].cells[1].childNodes[0].rows[0].cells[this.Index * 2];
		}
	}
	this.SortedColumns = new Array();

	var rs = this.getRowSelectors();
	if (bandCount == 1)
	{
		if (rs == 2)
			this.firstActiveCell = 0;
		else
			this.firstActiveCell = 1;
	}
	else
	{
		if (rs == 2)
			this.firstActiveCell = 1;
		else
			this.firstActiveCell = 2;
	}
	this._sqlWhere = "";
	this.SortImplementation = null;
},
"_alignColumns",
function()
{
	
	if (this.HasHeaderLayout)
	{
		var drsEls = igtbl_getDocumentElement(this.Grid.Id + "_drs");
		if (drsEls)
		{
			var master = drsEls[0].firstChild.firstChild;
			if (master.tagName != "COLGROUP")
			{
				return;
			}
			for (var d = 1; d < drsEls.length; d++)
			{
				var colGroup = drsEls[d].firstChild.firstChild;
				if (colGroup.tagName != "COLGROUP")
				{
					continue;
				}
				for (var c = 0; c < colGroup.childNodes.length && c < master.childNodes.length; c++)
				{
					colGroup.childNodes[c].width = master.childNodes[c].width;
				}
			}
		}
	}
},
"getSelectTypeRow",
function()
{
	var res = this.Grid.SelectTypeRow;
	if (this.SelectTypeRow != 0)
		res = this.SelectTypeRow;
	return res;
},
"getSelectTypeCell",
function()
{
	var res = this.Grid.SelectTypeCell;
	if (this.SelectTypeCell != 0)
		res = this.SelectTypeCell;
	return res;
},
"getSelectTypeColumn",
function()
{
	var res = this.Grid.SelectTypeColumn;
	if (this.SelectTypeColumn != 0)
		res = this.SelectTypeColumn;
	return res;
},
"getColumnFromKey",
function(key)
{
	var column = null;
	for (var i = 0; i < this.Columns.length; i++)
		if (this.Columns[i].Key == key)
	{
		column = this.Columns[i];
		break;
	}
	
	if (!column)
	{
		for (var i = 0; i < this.Columns.length; i++)
		{
			var colKey = this.Columns[i].Key;
			if (colKey != null && key != null && colKey.toLowerCase() == key.toLowerCase())
			{
				column = this.Columns[i];
				break;
			}
		}
	} return column;
},
"getExpandImage",
function()
{
	var ei = this.Grid.ExpandImage;
	if (this.ExpandImage != "")
		ei = this.ExpandImage;
	return ei;
},
"getCollapseImage",
function()
{
	var ci = this.Grid.CollapseImage;
	if (this.CollapseImage != "")
		ci = this.CollapseImage;
	return ci;
},
"getRowStyleClassName",
function()
{
	if (this.ItemClass != "")
		return this.ItemClass;
	return this.Grid.ItemClass;
},
"getRowAltClassName",
function()
{
	if (this.AltClass != "")
		return this.AltClass;
	return this.Grid.AltClass;
},
"getExpandable",
function()
{
	if (this.Expandable != 0)
		return this.Expandable;
	else return this.Grid.Expandable;
},
"getCellClickAction",
function()
{
	var res = this.Grid.CellClickAction;
	if (this.CellClickAction != 0)
		res = this.CellClickAction;
	return res;
},
"getExpAreaClass",
function()
{
	if (this.ExpAreaClass != "")
		return this.ExpAreaClass;
	return this.Grid.ExpAreaClass;
},
"getRowLabelClass",
function()
{
	if (this.RowLabelClass != "")
		return this.RowLabelClass;
	return this.Grid.RowLabelClass;
},
"getItemClass",
function()
{
	if (this.ItemClass != "")
		return this.ItemClass;
	return this.Grid.ItemClass;
},
"getAltClass",
function()
{
	if (this.AltClass != "")
		return this.AltClass;
	else if (this.Grid.AltClass != "")
		return this.Grid.AltClass;
	else if (this.ItemClass != "")
		return this.ItemClass;
	return this.Grid.ItemClass;
},
"getSelClass",
function()
{
	if (this.SelCellClass != "")
		return this.SelCellClass;
	return this.Grid.SelCellClass;
},
"getFooterClass",
function()
{
	if (this.FooterClass != "")
		return this.FooterClass;
	return this.Grid.FooterClass;
},
"getGroupByRowClass",
function()
{
	if (this.GroupByRowClass != "")
		return this.GroupByRowClass;
	return this.Grid.GroupByRowClass;
},
"addNew",
function()
{
	if (typeof (igtbl_addNew) == "undefined")
		return null;
	if (this.AddNewRowVisible == 1)
	{
		igtbl_activateAddNewRow(this.Grid, this.Index, igtbl_getClickRow(this.Grid, this.Index));
		return;
	}
	return igtbl_addNew(this.Grid.Id, this.Index);
},
"getHeadClass",
function()
{
	if (this.HeaderClass != "")
		return this.HeaderClass;
	return this.Grid.HeaderClass;
},
"getRowSelectors",
function()
{
	var res = this.Grid.RowSelectors;
	if (this.RowSelectors != 0)
		res = this.RowSelectors;
	return res;
},
"removeColumn",
function(index)
{
	if (!this.Node) return;
	var column = this.Columns[index];
	if (!column)
		return;
	var elem = column._getHeadTags(true);
	var fElem = column._getFootTags(true);
	var cols = column._getColTags(true);
	for (var i = 0; elem && i < elem.length; i++)
	{
		if (elem[i])
		{
			elem[i].parentNode.removeChild(elem[i]);
			elem[i].id = "";
		}
	}
	for (var i = 0; fElem && i < fElem.length; i++)
	{
		if (fElem[i])
		{
			fElem[i].parentNode.removeChild(fElem[i]);
			fElem[i].id = "";
		}
	}
	for (var i = 0; cols && i < cols.length; i++)
		if (cols[i])
		cols[i].parentNode.removeChild(cols[i]);
	column.colElem = elem;
	column.colFElem = fElem;
	if (column.Node)
		column.Node.parentNode.removeChild(column.Node);
	if (this.Columns.splice)
		this.Columns.splice(index, 1);
	else
		this.Columns = this.Columns.slice(0, index).concat(this.Columns.slice(index + 1));
	column.Id = "";
	column.fId = "";
	this.reIdColumns();
	return column;
},
"insertColumn",
function(column, index)
{
	if (!this.Node || !column || !column.Node || index < 0 || index > this.Columns.length)
		return;
	var column1 = this.Columns[index];
	var hAr;
	var hAr1;
	var fAr;
	var fAr1;
	var insIndex;
	if (column1)
	{
		this.Columns.Node.insertBefore(column.Node, this.Columns[index].Node);
		if (this.Columns.splice)
			this.Columns.splice(index, 0, column);
		else
			this.Columns = this.Columns.slice(0, index).concat(column, this.Columns.slice(index));
		insIndex = index;
		while (column1 && !column1.hasCells())
			column1 = this.Columns[++index];
	}
	else
	{
		this.Columns.Node.appendChild(column.Node);
		this.Columns[this.Columns.length] = column;
		insIndex = this.Columns.length - 1;
	}
	if (column1)
	{
		hAr = column.colElem;
		fAr = column.colFElem;
		if (column.getFixed() === column1.getFixed())
		{
			hAr1 = column1._getHeadTags(true);
			for (var i = 0; hAr && i < hAr.length; i++)
			{
				var tr = hAr1[i].parentNode;
				tr.insertBefore(hAr[i], hAr1[i])
			}
			if (fAr)
			{
				fAr1 = column1._getFootTags(true);
				for (var i = 0; i < fAr.length; i++)
				{
					var tr = fAr1[i].parentNode;
					tr.insertBefore(fAr[i], fAr1[i])
				}
			}
		}
		else
		{
			column1 = this.Columns[index - 1];
			hAr1 = this.Columns[index - 1]._getHeadTags(true);
			for (var i = 0; hAr && i < hAr.length; i++)
			{
				var tr = hAr1[i].parentNode;
				tr.insertBefore(hAr[i], hAr1[i].nextSibling)
			}
			if (fAr)
			{
				fAr1 = this.Columns[index - 1]._getFootTags(true);
				for (var i = 0; i < fAr.length; i++)
				{
					var tr = fAr1[i].parentNode;
					tr.insertBefore(fAr[i], fAr1[i].nextSibling)
				}
			}
		}
		if (column.getVisible() && column1.getVisible())
		{
			if (column.getFixed() === column1.getFixed())
				column1._insertCols(true, column.Width);
			else
				this.Columns[index - 1]._insertCols(false, column.Width);
		}
		else if (column.getVisible())
		{
			column2 = column1;
			if (!column1.hasCells()) 
			{
				while (column2 && !column2.hasCells())
					column2 = this.Columns[column2.Index + 1];
			}
			if (column2 && column2.getVisible())
				column2._insertCols(true, column.Width);
			else 
			{
				column2 = column1;
				while (column2 && !column2.getVisible())
					column2 = this.Columns[column2.Index - 1];
				if (!column2) return;
				column2._insertCols(false, column.Width);
			}
		}
	}
	else
	{
		column1 = this.Columns[insIndex - 1];
		while (column1 && !column1.hasCells())
			column1 = this.Columns[--insIndex];
		if (!column1) return;
		hAr = column.colElem;
		fAr = column.colFElem;
		hAr1 = column1._getHeadTags(true);
		for (var i = 0; hAr && i < hAr.length; i++)
		{
			var tr = hAr1[i].parentNode;
			tr.appendChild(hAr[i])
		}
		if (fAr)
		{
			fAr1 = column1._getFootTags(true);
			for (var i = 0; i < fAr.length; i++)
			{
				var tr = fAr1[i].parentNode;
				tr.appendChild(fAr[i])
			}
		}
		if (column.getVisible() && column1.getVisible())
			column1._insertCols(false, column.Width);
		else if (column.getVisible())
		{
			while (column1 && !column1.getVisible())
				column1 = this.Columns[column1.Index - 1];
			if (!column1) return;
			column1._insertCols(false, column.Width);
		}
	}
	this.reIdColumns();
	igtbl_dispose(hAr);
	igtbl_dispose(fAr);
	igtbl_dispose(hAr1);
	igtbl_dispose(fAr1);
	return column;
},
"reIdColumns",
function()
{
	if (!this.Node) return;
	for (var i = 0; i < this.Columns.length; i++)
		if (!this.Columns[i]._reIded)
		this.Columns[i]._reId(i);
	for (var i = this.Columns.length - 1; i >= 0; i--)
		delete this.Columns[i]._reIded;
},
"getSelGroupByRowClass",
function()
{
	if (this.SelGroupByRowClass != "")
		return this.SelGroupByRowClass;
	return this.Grid.SelGroupByRowClass;
},
"getBorderCollapse",
function()
{
	if (this.get("BorderCollapse") == "Separate")
		return "";
	if (this.Grid.get("BorderCollapseDefault") == "Separate")
		return "";
	if (this.curTable)
		return this.curTable.style.borderCollapse;
	return this.Grid.Element.style.borderCollapse;
},
"getIndentation",
function()
{
	
	var result = this.Indentation;
	if (result == 22)
		result = this.Grid.Indentation;
	return result;
}
, "getSortingAlgorithm",
function()
{
	if (this.SortingAlgorithm == 0)
		return this.Grid.SortingAlgorithm;
	return this.SortingAlgorithm;
}
, "getSortImplementation",
function()
{
	if (this.SortImplementation == null)
		return this.Grid.SortImplementation;
	return this.SortImplementation;
}
];
for (var i = 0; i < igtbl_ptsBand.length; i += 2)
	igtbl_Band.prototype[igtbl_ptsBand[i]] = igtbl_ptsBand[i + 1];

function igtbl_expandEffects(values)
{
	this.Delay = values[0];
	this.Duration = values[1];
	this.Opacity = values[2];
	this.ShadowColor = values[3];
	this.ShadowWidth = values[4];
	this.EffectType = values[5];
}
// Cell object
igtbl_Cell.prototype = new igtbl_WebObject();
igtbl_Cell.prototype.constructor = igtbl_Cell;
igtbl_Cell.base = igtbl_WebObject.prototype;
function igtbl_Cell(element, node, row, index)
{
	if (arguments.length > 0)
		this.init(element, node, row, index);
}
var igtbl_ptsCell = [
"init",
function (element, node, row, index)
{
	igtbl_Cell.base.init.apply(this, ["cell", element, node]);

	var gs = row.OwnerCollection.Band.Grid;
	this.Row = row;
	this.Band = row.Band;
	if (typeof (index) != "number")
		try { index = parseInt(index.toString(), 10); } catch (e) { }
	this.Column = this.Band.Columns[index];
	this.Index = index;
	var cell = this.Element;
	if (cell)
	{
		cell.Object = this;
		this.NextSibling = cell.nextSibling;
		if (cell.cellIndex == this.Band.firstaActiveCell)
			this.PrevSibling = null;
		else
			this.PrevSibling = cell.previousSibling;
		if (this.Column.MaskDisplay)
			this.MaskedValue = igtbl_getInnerText(cell);
	}
	
	
	if (this.Column.ColumnType == 3 && element)
	{
		var childNodes = null;
		if (element.childNodes.length > 0)
			childNodes = element.childNodes[0].childNodes;
		for (var chkBoxCount = 0; childNodes && chkBoxCount < childNodes.length; chkBoxCount++)
		{
			var chkBox = childNodes[chkBoxCount];
			if (chkBox.tagName == "INPUT" && chkBox.type == "checkbox")
			{
				chkBox.unselectable = "on";
				break;
			}
		}
	}
	this._Changes = new Object();
},
"getElement",
function ()
{
	if (this._scrElem)
		return this._scrElem;
	return this.Element;
},
"getNodeValue",
function ()
{
	if (!this.Node)
		return;
	
	var v = igtbl_getNodeValue(this.Node);
	if (typeof (this.Value) == "undefined" && this.Column.ColumnType == 9)
	{
		var subWithTrailingA = v.substring(v.indexOf('>') + 1);
		v = subWithTrailingA.substring(0, subWithTrailingA.indexOf('<'));
		subWithTrailingA = null;
	}
	else if (this.Column.ColumnType == 9 && typeof (this.Value) != "undefined")
	{
		return this.Value;
	}
	return v;
},
"setNodeValue",
function (value, displayValue)
{
	if (!this.Node)
		return;
	
	igtbl_setNodeValue(this.Node, value, displayValue, this.Element);
},
"getValue",
function (textValue, force)
{
	if (typeof (this.Value) != "undefined" && !textValue && !force)
		return this.Value;
	var value;
	if (this.Node)
		value = this.getNodeValue();
	if (this.Element)
	{
		if (!this.Node)
			value = this.Element.getAttribute(igtbl_sigCellText);
		if (typeof (value) != "string" || textValue        
		   || (this.Column.ColumnType == 9 && this.Node)) 
		{
			value = this.Element.getAttribute(igtbl_sUnmaskedValue);
			if (value) value = unescape(value);
			if (typeof (value) == "undefined" || value == null)
			{
				var elem = this.Element;
				if (elem.childNodes.length > 0 && elem.childNodes[0].tagName == "NOBR")
					elem = elem.childNodes[0];
				if (this.Column.ColumnType == 9 && elem.childNodes.length > 0 && elem.childNodes[0].tagName == "A")
					elem = elem.childNodes[0];
				
				if (this.Row.IsFilterRow)
				{
					var tempE = null;
					var chldNodes = elem.childNodes;
					
					for (var itr = 0; itr < chldNodes.length; itr++)
					{
						if (chldNodes[itr].tagName == "SPAN")
						{
							tempE = chldNodes[itr];
							break;
						}
					}
					elem = tempE;
				}
				value = igtbl_getInnerText(elem);
				if (value == " ") value = "";
			}
			else if (textValue)
			{
				if (this.MaskedValue)
					value = this.MaskedValue;
				else
					value = value.toString();
			}
			var oCombo = null;
			this.Column.ensureWebCombo();
			if (this.Column.WebComboId)
				oCombo = igcmbo_getComboById(this.Column.WebComboId);
			if (oCombo)
			{
				if (!textValue && !this.Element.getAttribute(igtbl_sUnmaskedValue))
				{
					var oCombo = igcmbo_getComboById(this.Column.WebComboId);
					if (oCombo && oCombo.DataTextField)
					{
						var re = new RegExp("^" + igtbl_getRegExpSafe(value) + "$", "gi");
						var column = oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataTextField);
						if (column)
						{
							var cell = column.find(re);
							if (cell && oCombo.DataValueField)
								value = cell.Row.getCellByColumn(oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataValueField)).getValue(true);
						}
						delete re;
					}
				}
			}
			else if (this.Column.ColumnType == 3 && this.Element.childNodes.length > 0)
			{
				if (!this.Element.getAttribute(igtbl_sUnmaskedValue))
				{
					var chBox = this.Element.childNodes[0];
					while (chBox && chBox.tagName != "INPUT")
						chBox = chBox.childNodes[0];
					value = false;
					if (chBox)
						value = chBox.checked;
					if (textValue)
						value = value.toString();
				}
			}
			else if (this.Column.ColumnType == 5 && this.Column.ValueList.length > 0)
			{
				
				if (!textValue)
				{
					
					var tempValue = this.Element.getAttribute(igtbl_sigDataValue);
					if (tempValue != null)
					{
						value = tempValue;
						
						
					}
					else
						for (var i = 0; i < this.Column.ValueList.length; i++)
						
							if (this.Column.ValueList[i] && this.Column.ValueList[i][1] == value)
							{
								value = this.Column.ValueList[i][0];
								break;
							}
				}
			}
			else if (this.Column.ColumnType == 7 && this.Element.childNodes.length > 0)
			{
				var button = this.Element.childNodes[0];
				while (button && button.tagName != "INPUT")
					button = button.childNodes[0];
				if (button)
					value = button.value;
			}
			if (typeof (value) == "string" && this.Column.AllowNull && value == this.Column.getNullText())
			{
				if (textValue)
					value = this.Column.getNullText();
				else
					value = null;
			}
		}
	}
	
	else
	{
		if (this.Column.MergeCells)
		{
			
			var upRow = this.Row.getPrevRow();
			if (upRow)
				value = upRow.getCellByColumn(this.Column).getValue();
		}
	}
	if (typeof (value) != "undefined")
	{
		if (!textValue)
		{
			
			
			value = this.Column.getValueFromString(value);
		}
	}
	else if (textValue)
		value = "";
	if (!textValue)
		this.Value = value;
	return value;
},
"setValue",
function (value, fireEvents)
{
	if (typeof (fireEvents) == "undefined")
		fireEvents = true;
	var gn = this.Row.gridId;
	var gs = igtbl_getGridById(gn);
	if (this.Column.DataType != 8 && typeof (value) == "string")
		value = igtbl_string.trim(value);
	if (!gs.insideBeforeUpdate)
	{
		gs.insideBeforeUpdate = true;
		var ev = value;
		if ((ev == null || ev == this.Column.getNullText() || typeof (ev) == 'undefined') && typeof (ev) != 'number' && typeof (ev) != 'boolean')
		{
			ev = this.Column.getNullText();
			value = null;
		}
		else
		{
			ev = ev.toString().replace(/\r\n/g, "\\r\\n");
			
			ev = ev.toString().replace(/\\/g, "\\\\");
			ev = ev.replace(/\"/g, "\\\"");
		}
		var res = fireEvents && this.Element && igtbl_fireEvent(gn, gs.Events.BeforeCellUpdate, "(\"" + gn + "\",\"" + this.Element.id + "\",\"" + ev + "\")");
		gs.insideBeforeUpdate = false;
		if (res == true)
			return;
	}
	var v = value;
	var oldValue = this.getValue();
	if (typeof (value) != "undefined" && value != null)
	{
		if ((oldValue && oldValue.getMonth || this.Column.DataType == 7) && typeof (value) == "string")
		{
			if (this.Column.MaskDisplay.substr(0, 1).toLowerCase() == "h")
				value = "01/01/01 " + value;
			else
			{
				var year = "";
				for (var i = value.length - 1; i >= 0; i--)
				{
					var y = parseInt(value.charAt(i), 10);
					if (isNaN(y))
						break;
					else
						year = y.toString() + year;
				}
				if (year && year.length < 3)
					value = value.substr(0, i + 1) + (parseInt(year, 10) + gs.DefaultCentury).toString();
			}
			value = new Date(value);
		}
		if (value.getMonth)
		{
			if (isNaN(value)) value = oldValue;
			v = value;
			if (value)
				value = igtbl_dateToString(value);
		}
	}
	var displayValue = null;
	if (this.Element)
	{
		if (this.Element.getAttribute(igtbl_sigCellText) != null)
		{
			
			this.Element.setAttribute(igtbl_sigCellText, typeof (value) == "undefined" || value == null ? "" : value.toString());
			if (this.Node)
				this.Node.setAttribute(igtbl_sigCellText, value == null ? "" : value);
		}
		else
		{
			var rendVal = null;
			
			var colEditor = this.Column.getEditorControl();
			if (colEditor && colEditor.getRenderedValue && (rendVal = colEditor.getRenderedValue(v)) != null)
			{
				v = rendVal;
				if (value != null)
				{
					
					if (igtbl_number.isNumberType(this.Column.DataType))
					{
						
						colEditor.setValue(v, 2);
						value = colEditor.getValue();
					}
					if (value != null)
					{
						
						if (typeof (value.getMonth) != "undefined")
							value = igtbl_dateToString(value);
						this.Element.setAttribute(igtbl_sUnmaskedValue, value.toString());
					}
					else
						this.Element.removeAttribute(igtbl_sUnmaskedValue);
				}
				else
					this.Element.removeAttribute(igtbl_sUnmaskedValue);
				this.MaskedValue = v;
			}
			else
			{
				if (this.Column.AllowNull && (typeof (v) == "undefined" || v == null || typeof (v) == "string" && (v == "" || v == this.Column.getNullText())))
				{
					v = this.Column.getNullText();
					value = "";
				}
				else
					v = typeof (value) != "undefined" && value != null ? value.toString() : "";
				if (this.Column.MaskDisplay !== "")
				{
					if (this.Column.AllowNull && v == this.Column.getNullText())
					{
						this.Element.setAttribute(igtbl_sUnmaskedValue, null);
						this.MaskedValue = (v === "" ? " " : v);
					}
					else
					{
						v = igtbl_Mask(gn, v, this.Column.DataType, this.Column.MaskDisplay);
						if (v === "")
						{
							var umv = this.Element.getAttribute(igtbl_sUnmaskedValue);
							if (ig_csom.notEmpty(umv))
								v = igtbl_Mask(gn, umv, this.Column.DataType, this.Column.MaskDisplay);
							else
							{
								v = this.Column.getNullText();
								value = "";
							}
						}
						else
						{
							if (this.Column.MaskDisplay == "MM/dd/yyyy" || this.Column.MaskDisplay == "MM/dd/yy" || this.Column.MaskDisplay == "hh:mm" || this.Column.MaskDisplay == "HH:mm" || this.Column.MaskDisplay == "hh:mm tt")
								value = v;
							this.Element.setAttribute(igtbl_sUnmaskedValue, value);
							this.MaskedValue = v;
						}
					}
				}
				else if (ig_csom.notEmpty(this.Element.getAttribute(igtbl_sUnmaskedValue)))
				
					this.Element.setAttribute(igtbl_sUnmaskedValue, igtbl_string.toString(value));
				if (!(this.Column.AllowNull && v == this.Column.getNullText()))
				{
					if (this.Column.MaskDisplay === "")
					{
						if (typeof (value) != "undefined" && value != null && this.Column.DataType != 7)
						{
							v = this.Column.getValueFromString(value);
							if (v != null)
							{
								v = v.toString();
								value = v;
							}
						}
						if (this.Column.FieldLength > 0)
						{
							v = v.substr(0, this.Column.FieldLength);
							value = v;
						}
						
						if (this.Column.Case == 1)
						{
							v = v.toLowerCase();
							value = v;
						}
						else if (this.Column.Case == 2)
						{
							v = v.toUpperCase();
							value = v;
						}
					}
				}
			}
			var setInner = true;
			this.Column.ensureWebCombo();
			if (this.Column.WebComboId && typeof (igcmbo_getComboById) != "undefined")
			{
				var oCombo = igcmbo_getComboById(this.Column.WebComboId);
				if (oCombo && oCombo.DataValueField)
				{
					var re = new RegExp("^" + igtbl_getRegExpSafe(value) + "$", "gi");
					var column = oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataValueField);
					if (column)
					{
						var cell = column.find(re);
						if (cell && oCombo.Prompt && cell.Row.getIndex() == 0)
							cell = column.findNext();
					}
					if (cell && oCombo.DataTextField)
					{
						v = cell.Row.getCellByColumn(oCombo.grid.Bands[0].getColumnFromKey(oCombo.DataTextField)).getValue(true);
						displayValue = v;
					}
					this.Element.setAttribute(igtbl_sigDataValue, value);
					this.Element.setAttribute(igtbl_sUnmaskedValue, value.toString());
					delete re;
				}
			}
			else if (this.Column.ColumnType == 3 && this.Element.childNodes.length > 0)
			{
				igtbl_dontHandleChkBoxChange = true;
				var chBox = this.Element.childNodes[0];
				while (chBox && chBox.tagName != "INPUT")
					chBox = chBox.childNodes[0];
				if (chBox)
				{
					if (!value || value.toString().toLowerCase() == "false" || v == "0")
						chBox.checked = false;
					else
						chBox.checked = true;
					
					if (gs.Section508Compliant)
						chBox.alt = (chBox.checked ? "checked" : "unchecked");
					this.Element.setAttribute("chkBoxState", v);
					
					this.Element.setAttribute(igtbl_sUnmaskedValue, v);
				}
				igtbl_dontHandleChkBoxChange = false;
				setInner = false;
				if (this.Node)
				{
					this.setNodeValue(chBox.checked ? "False" : "True");
					var cdata = this.Node.firstChild;
					if (chBox.checked)
						cdata.text = cdata.text.replace("type='checkbox'", "type='checkbox' checked");
					else
						cdata.text = cdata.text.replace(" checked", "");
				}
			}
			else if (this.Column.ColumnType == 5 && this.Column.ValueList.length > 0)
			{
				
				var v2 = value;
				if (this.Column.DataType == 11) 
				{
					v2 = this._getBoolFromStringIfPossible(value);
				}
				for (var i = 0; i < this.Column.ValueList.length; i++)
				{
					var valueListValue;
					
					if (this.Column.ValueList[i])
						valueListValue = this.Column.ValueList[i][0];
					if (this.Column.DataType == 11) 
					{
						valueListValue = this._getBoolFromStringIfPossible(valueListValue);
					}
					if (valueListValue === v2)
					
					{
						v = this.Column.ValueList[i][1];
						displayValue = v;
						this.Element.setAttribute(igtbl_sigDataValue, value);
						if (this.Node)
							this.Node.setAttribute(igtbl_sigDataValue, value);
						break;
					}
				}
				if (i == this.Column.ValueList.length)
					this.Element.removeAttribute(igtbl_sigDataValue);
			}
			else if (this.Column.ColumnType == 7 && this.Element.childNodes.length > 0)
			{
				var button = this.Element.childNodes[0];
				while (button && button.tagName != "INPUT")
					button = button.childNodes[0];
				if (button)
				{
					button.value = v;
					setInner = false;
				}
				else
				{
					button = igtbl_getElementById(gn + "_bt");
					if (button)
						button.value = v;
				}
			}
			if (setInner)
			{
				
				var vs = (this.Column.DataType == 8 || this.Column.DataType == 10) ? v : igtbl_string.trim(v);
				var e = this.Element;
				var paddedValue = false;
				if (vs === "")
				{
					
					paddedValue = true;
					vs = " ";
					e.setAttribute(igtbl_sUnmaskedValue, v);
				}
				
				else if (vs != v && !this.MaskedValue)
					e.setAttribute(igtbl_sUnmaskedValue, v);
				else if (e.getAttribute(igtbl_sUnmaskedValue, "") === "")
					e.removeAttribute(igtbl_sUnmaskedValue);
				e = this.getElement();
				el = e;
				if (el.firstChild && el.firstChild.tagName == "NOBR")
					el = el.firstChild;
				if ((this.Column.ColumnType == 9 || this.getTargetURL()) && el.firstChild && el.firstChild.tagName == "A")
					el = el.firstChild;
				
				if (this.Row.IsFilterRow)
				{
					var tempE = null;
					var chldNodes = el.childNodes;
					
					for (var itr = 0; itr < chldNodes.length; itr++)
					{
						if (chldNodes[itr].tagName == "SPAN")
						{
							tempE = chldNodes[itr];
							break;
						}
					}
					el = tempE;
				}
				if ((this.Column.ColumnType == 9 || this.getTargetURL()) && el.tagName == "A")
				{
					if ((value != " " && vs == " ") || vs === "")
					{
						igtbl_setInnerText(el, "");
						if (el.parentNode.innerHTML.indexOf(" ") > 0
							&& el.parentNode.innerHTML.lastIndexOf(" ") < (el.parentNode.innerHTML.length - 1)
							&& el.parentNode.innerHTML.indexOf("&nbsp;") > 0
							&& el.parentNode.innerHTML.lastIndexOf("&nbsp;") < (el.parentNode.innerHTML.length - 1 - 5)
							)
							el.parentNode.innerHTML += "&nbsp;";
					}
					else
					{
						igtbl_setInnerText(el, vs);
					}
				}
				else
					igtbl_setInnerText(el, vs, this.Column.Wrap);
				
				if ((e.getAttribute(igtbl_sigDataValue) || this.Column.HTMLEncodeContent) && this.Column.ColumnType != 5 && (!this.Column.WebComboId || typeof (igcmbo_getComboById) == "undefined"))
				{
					
					if (paddedValue && vs === " " && value === "")
						e.setAttribute(igtbl_sigDataValue, "");
					else
						e.setAttribute(igtbl_sigDataValue, vs);
				}
				
				if (el.tagName == "A" && this.Column.ColumnType == 9 && !this.getTargetURL())
					el.href = (v.indexOf('@') >= 0 ? "mailto:" : "") + v;
				if (this.Node)
				{
					this.Node.firstChild.text = (e.getAttribute(igtbl_sUnmaskedValue, "") === "") ? "&nbsp;" : vs;
				}
			}
		}
	}
	if (this.Node)
	{
		
		
		var nodeValue = (value == null || value === '') ? this.Column.getNullText() : value;
		if (this.Column.ColumnType == 7)
			this.setNodeValue("<input type=\"button\" style=\"width:100%;height:100%;\" value=\"" + nodeValue + "\" onclick=\"igtbl_colButtonClick(event,'" + this.Band.Grid.Id + "',null, igtbl_srcElement(event));\" class=\"\" tabindex=\"-1\" />");
		else
			this.setNodeValue(nodeValue, displayValue);
	}
	var newValue = this.getValue(false, true);
	if (!((typeof (newValue) == "undefined" || newValue == null) && (typeof (oldValue) == "undefined" || oldValue == null) || newValue != null && oldValue != null && newValue.valueOf() == oldValue.valueOf()))
	{
		this.Row._dataChanged |= 2;
		if (typeof (this._oldValue) == "undefined")
		{
			if (oldValue && oldValue.getMonth)
				oldValue = igtbl_dateToString(oldValue);
			this._oldValue = oldValue;
		}
		
		
		if (!this.Row.IsAddNewRow)
			igtbl_saveChangedCell(gs, this, value);
		


		
		if (this.Row.IsFilterRow)
		{
			
			var columnFilter = this.Column._getFilterPanel(this.Row.Element);
			var filterOp = parseInt(this._getFilterTypeImage().getAttribute("operator"));

			
			
			if (this.Column.DataType == 7 && value != null && value !== "")
			{   
				var re = new RegExp("^\\s+");
				value = value.replace(re, "");
			}

			
			if (value == columnFilter.getEvaluationValue())
				return;

			
			if (value == null || value === "") filterOp = igtbl_filterComparisionOperator.All;

			
			columnFilter.setFilter(filterOp, value);

			
			if (this.Row.Band.FilterEvaluationTrigger == 1)
			{
				
				if (this.Row.Band.Index > 0)
				{
					columnFilter.RowIsland = this.Row.OwnerCollection;
				}
				
				if (!gs.fireEvent(gs.Events.BeforeRowFilterApplied, [gs.Id, this.Column]))
				{
					columnFilter.applyFilter();
					gs.fireEvent(gs.Events.AfterRowFilterApplied, [gs.Id, this.Column]);
					gs.alignDivs();
				}
			}
		}
		else
		{
			
			gs.invokeXmlHttpRequest(gs.eReqType.UpdateCell, this, value ? value.toString() : value);
			if (fireEvents && this.Element)
			{
				igtbl_fireEvent(gn, gs.Events.AfterCellUpdate, "(\"" + gn + "\",\"" + this.Element.id + "\")");
				if (gs.LoadOnDemand == 3 || this.Row.IsAddNewRow)
					gs.NeedPostBack = false;
			}
		}
	}
},
"_getBoolFromStringIfPossible",
function (str)
{
	
	
	if (str == null || str.length == 0 || typeof (str) != "string") return str;
	if (str.toLowerCase() == "true") return true;
	if (str.toLowerCase() == "false") return false;
	return str;
}
,
"getRow",
function ()
{
	return this.Row;
},
"getNextTabCell",
function (shift, addRow
, filterRow
)
{
	var g = this.Row.Band.Grid;
	var cell = null;
	switch (g.TabDirection)
	{
		case 0:
		case 1:
			if (shift && g.TabDirection == 0 || !shift && g.TabDirection == 1)
			{
				
				cell = this;
				do
				{	
					cell = cell.getPrevCell();
					if (!cell) break;
				} while (!cell.Element);
				if (!cell)
				{
					var row = this.Row.getNextTabRow(true, false, addRow
						, filterRow
					);
					if (row && !row.GroupByRow)
					{
						cell = row.getCell(row.cells.length - 1);
						do
						{
							if (!cell.Column.getVisible() || !cell.Element)
								cell = cell.getPrevCell();
							if (!cell) break;
						} while (!cell.Element);
					}
				}
			}
			else
			{
				
				cell = this;
				do
				{
					cell = cell.getNextCell();
					if (!cell) break;
				} while (!cell.Element);
				if (!cell)
				{
					var row = this.Row.getNextTabRow(false, false, addRow);
					if (row && !row.GroupByRow)
					{
						cell = row.getCell(0);
						do
						{
							if (!cell.Column.getVisible() || !cell.Element)
								cell = cell.getNextCell();
							if (!cell) break;
						} while (!cell.Element);
					}
				}
			}
			break;
		case 2:
		case 3:
			if (shift && g.TabDirection == 2 || !shift && g.TabDirection == 3)
			{
				var row = this.Row.getPrevRow();
				if (row && row.getExpanded())
				{
					row = this.Row.getNextTabRow(true, false, addRow);
					cell = row.getCell(row.cells.length - 1);
					do
					{
						if (!cell.Column.getVisible() || !cell.Element)
							cell = cell.getPrevCell();
						if (!cell) break;
					} while (!cell.Element);
				}
				else if (row)
					cell = row.getCell(this.Index);
				else
				{
					if (this.Index == 0)
					{
						row = this.Row.getNextTabRow(true, false, addRow);
						if (row && !row.GroupByRow)
						{
							cell = row.getCell(row.cells.length - 1);
							do
							{
								if (!cell.Column.getVisible() || !cell.Element)
									cell = cell.getPrevCell();
								if (!cell) break;
							} while (!cell.Element);
						}
					}
					else
					{
						cell = this.Row.OwnerCollection.getRow(this.Row.OwnerCollection.length - 1).getCell(this.Index - 1);
						do
						{
							if (!cell.Column.getVisible() || !cell.Element)
								cell = cell.getPrevCell();
							if (!cell) break;
						} while (!cell.Element);
					}
				}
			}
			else
			{
				if (this.Row.getExpanded())
				{
					cell = this.Row.Rows.getRow(0).getCell(0);
					do
					{
						if (!cell.Column.getVisible() || !cell.Element)
							cell = cell.getNextCell();
						if (!cell) break;
					} while (!cell.Element);
				}
				else
				{
					var row = this.Row.getNextRow();
					if (row)
						cell = row.getCell(this.Index);
					else if (this.Index < this.Row.cells.length - 1)
					{
						cell = this.Row.OwnerCollection.getRow(0).getCell(this.Index + 1);
						do
						{
							if (!cell.Column.getVisible() || !cell.Element)
								cell = cell.getNextCell();
							if (!cell) break;
						} while (!cell.Element);
					}
					else
					{
						row = this.Row.getNextTabRow(false, false, addRow
						, filterRow
						);
						if (row && !row.GroupByRow)
						{
							cell = row.getCell(0);
							do
							{
								if (!cell.Column.getVisible() || !cell.Element)
									cell = cell.getNextCell();
								if (!cell) break;
							} while (!cell.Element);
						}
					}
				}
			}
			break;
	}
	return cell;
},
"beginEdit",
function (keyCode)
{
	if (this.isEditable())
	{
		igtbl_editCell((typeof (event) != "undefined" ? event : null), this.Row.gridId, this.Element, keyCode);
		var ec = this.Band.Grid._editorCurrent;
		if (ec)
		{
			ec.setAttribute("noOnBlur", true);
			if (igtbl_isVisible(ec))
				window.setTimeout("igtbl_cancelNoOnBlurTB('" + this.Band.Grid.Id + "','" + ec.id + "')", 100);
			else
				ec.removeAttribute("noOnBlur");
		}
	}
},
"endEdit",

function (force)
{
	var ec = this.Column.getEditorControl();
	if (!ec)
		ec = this.Column.Band.Grid._editorCurrent;
	
	
	if (ec && ec.Element)
		ec = ec.Element;
	if (force)
		if (ec && ec.removeAttribute)
			ec.removeAttribute("noOnBlur");
	if (ec && ec.getAttribute && ec.getAttribute("noOnBlur"))
		return;
	igtbl_hideEdit(this.Row.gridId);
},
"getSelected",
function ()
{
	if (this._Changes["SelectedCells"])
		return true;
	return false;
},
"setSelected",
function (select)
{
	var stc = this.Band.getSelectTypeCell();
	if (stc > 1)
	{
		if (stc == 2)
			this.Band.Grid.clearSelectionAll();
		igtbl_selectCell(this.Row.gridId, this, select);
	}
},
"getNextCell",
function (includeHiddenColumns)
{
	var nc = this.Index + 1;
	while (
		includeHiddenColumns != true &&
		nc < this.Row.cells.length && !this.Row.getCell(nc).Column.getVisible())
	{
		nc++;
	}
	if (nc < this.Row.cells.length)
		return this.Row.getCell(nc);
	return null;
},
"getPrevCell",
function (includeHiddenColumns)
{
	var pc = this.Index - 1;
	while (
		includeHiddenColumns != true &&
		pc >= 0 && !this.Row.getCell(pc).Column.getVisible())
	{
		pc--;
	}
	if (pc >= 0)
		return this.Row.getCell(pc);
	return null;
},
"activate",
function ()
{
	this.Row.Band.Grid.setActiveCell(this);
},
"scrollToView",
function ()
{
	var g = this.Row.Band.Grid;
	
	
	var adj = 0;
	
	if (!ig_csom.IsIE7Compat && ((this.Row.IsFilterRow || this.Row.IsAddNewRow) && this.Row.isFixed()))
	{
		var rowTblElem = this.Row.Element.parentNode;
		while (rowTblElem && rowTblElem.tagName != "TABLE")
			rowTblElem = rowTblElem.parentNode;
		adj = -(rowTblElem.offsetLeft);
	}

	if (g.UseFixedHeaders)
	{
		var c = this.Column;
		
		

		var w = 0, i = 0, c1 = null;
		while (i < c.Index)
		{
			c1 = c.Band.Columns[i++];
			if (c1.getVisible())
			{
				if (!c1.getFixed())
					w += c1.getWidth();
				
			}
		}
		if (!c.getFixed() && w + c.getWidth() < g._scrElem.scrollLeft)
		{
			igtbl_scrollLeft(g._scrElem, w)
		}
		
		
		igtbl_scrollToView(g.Id, this.Element, c.getWidth(), w, (
		
			this.Row.IsFilterRow ||
			this.Row.IsAddNewRow) && this.Row.isFixed() ? 1 : 0, adj);
		return;
	}

	igtbl_scrollToView(g.Id, this.Element
		, null, null, (
	
			this.Row.IsFilterRow ||
		this.Row.IsAddNewRow) && this.Row.isFixed() ? 1 : 0, adj
	);
},
"isEditable",
function ()
{
	var attr = "";
	if (this.Node)
		attr = this.Node.getAttribute(igtbl_litPrefix + "allowedit");
	else if (this.Element)
		attr = this.Element.getAttribute("allowedit");
	if (attr == "yes")
		return true;
	if (attr == "no")
		return false;
	if (this.Row.IsFilterRow)
	{
		
		return this.Column.AllowRowFiltering != 1;
	}
	return igtbl_getAllowUpdate(this.Row.gridId, this.Column.Band.Index, this.Column.Index) == 1;
},


"getEditable",
function ()
{
	if (this.Node)
		return this.Node.getAttribute(igtbl_litPrefix + "allowedit");
	else if (this.Element)
		return this.Element.getAttribute("allowedit");
},
"setEditable",
function (bEdit)
{
	if (bEdit == null || typeof (bEdit) == "undefined")
		bEdit = false;
	var attr = bEdit ? "yes" : "no";
	if (this.Node)
		this.Node.setAttribute(igtbl_litPrefix + "allowedit", attr)
	if (this.Element)
		this.Element.setAttribute("allowedit", attr);

	
	if (this.Column.ColumnType == 3)
	{
		var checkboxes = this.Element.getElementsByTagName("input");
		if (checkboxes.length == 1)
		{
			checkboxes[0].disabled = !bEdit;
		}
	}
},


"hasButtonEditor",
function (cellButtonDisplay)
{
	return this.Column.ColumnType == 7
		&& !this.Row.GroupByRow
	
		&& !this.Row.IsFilterRow
		&& (typeof (cellButtonDisplay) == "undefined" || this.Column.CellButtonDisplay == cellButtonDisplay);
},
"renderActive",
function (render)
{
	var g = this.Row.Band.Grid;
	if (!g.Activation.AllowActivation || !this.Element)
		return;
	var e = this.getElement();
	if (typeof (render) == "undefined") render = true;
	var ao = g.Activation;
	if (render)
	{
		igtbl_setClassName(e, ao._cssClass);
		igtbl_setClassName(e, ao._cssClassL);
		igtbl_setClassName(e, ao._cssClassR);
	}
	else
	{
		igtbl_removeClassName(e, ao._cssClassR);
		igtbl_removeClassName(e, ao._cssClassL);
		igtbl_removeClassName(e, ao._cssClass);
	}
},
"getLevel",
function (s)
{
	var l = this.Row.getLevel();
	l[l.length] = this.Column.Index;
	if (s)
	{
		s = l.join("_");
		igtbl_dispose(l);
		delete l;
		return s;
	}
	return l;
},
"selectCell",
function (selFlag)
{
	var e = this.getElement();
	if (!e)
		return;
	var className = null;
	if (selFlag != false)
		className = this.Column.getSelClass();
	igtbl_changeStyle(this.Row.gridId, e, className);
},
"select",
function (selFlag, fireEvent)
{
	var gs = this.Column.Band.Grid;
	var gn = gs.Id;
	var cellID = this.Element.id;
	if (gs._exitEditCancel || gs._noCellChange)
		return;
	if (this.Band.getSelectTypeCell() < 2)
		return;
	if (igtbl_fireEvent(gn, gs.Events.BeforeSelectChange, "(\"" + gn + "\",\"" + cellID + "\")") == true)
		return;
	if (selFlag != false)
	{
		this.selectCell();
		
		gs._recordChange("SelectedCells", this, gs.GridIsLoaded);
		if (!gs.SelectedCellsRows[this.Element.parentNode.id])
			gs.SelectedCellsRows[this.Element.parentNode.id] = new Object();
		gs.SelectedCellsRows[this.Element.parentNode.id][cellID] = true;
	}
	else
	{
		
		if (gs.SelectedCells[cellID] || gs._containsChange("SelectedCells", this))
		{
			gs._removeChange("SelectedCells", this);
			var scr = gs.SelectedCellsRows[this.Element.parentNode.id];
			if (scr && scr[cellID])
				delete scr[cellID];
		}
		if (igtbl_getLength(gs.SelectedCellsRows[this.Element.parentNode.id]) == 0)
			delete gs.SelectedCellsRows[this.Element.parentNode.id];
		if (!this.Column.Selected && !this.Row.getSelected())
			this.selectCell(false);
	}
	if (fireEvent != false)
	{
		var gsNPB = gs.NeedPostBack;
		igtbl_fireEvent(gn, gs.Events.AfterSelectChange, "(\"" + gn + "\",\"" + cellID + "\");");
		if (!gsNPB && !(gs.Events.AfterSelectChange[1] & 1))
			gs.NeedPostBack = false;
		if (gs.NeedPostBack)
			igtbl_moveBackPostField(gn, "SelectedCells");
	}
},
"getOldValue",
function ()
{
	return this._oldValue;
},
"getTargetURL",
function ()
{
	var url = null;
	if (this.Node && (url = this.Node.getAttribute("targetURL")))
		return url;
	if (this.Element && (url = this.Element.getAttribute("targetURL")))
		return url;
	if (this.Column.ColumnType == 9)
		return this.getValue();
	return url;
},
"getTitleModeResolved",
function ()
{
	var result;
	if (this.Element)
	{
		result = this.Element.getAttribute("iTM");
	}
	if (!result && this.Band)
	{
		result = this.Band.CellTitleMode;
	}
	if (!result && this.Band && this.Band.Grid)
	{
		result = this.Band.Grid.CellTitleMode;
	}
	return result;
},
"setTargetURL",
function (url)
{
	if (this.Node && this.Node.getAttribute("targetURL"))
		this.Node.setAttribute("targetURL", url);
	if (this.Element && this.Element.getAttribute("targetURL"))
		this.Element.setAttribute("targetURL", url);
	var urls = igtbl_splitUrl(url);
	var el = this.Element;
	if (el)
	{
		if (el.firstChild && el.firstChild.tagName == "NOBR")
			el = el.firstChild;
		if (el.firstChild && el.firstChild.tagName == "A")
			el = el.firstChild;
	}
	if (this.Column.ColumnType == 9)
		this.setValue(urls[0]);
	if (el && el.tagName == "A")
	{
		if (this.Column.ColumnType != 9)
			el.href = urls[0];
		if (urls[1])
			el.target = urls[1];
		else
			el.target = "_self";
	}
	igtbl_dispose(urls);
}
, "_getFilterTypeImage",
function ()
{
	if (!this.Row.IsFilterRow) return null;
	return this._getFilterTypeImageRecus(this.Element.childNodes);
},
"_getFilterTypeImageRecus",
function (elements)
{
	if (elements != null)
	{
		for (var itr = 0; itr < elements.length; itr++)
		{
			if (elements[itr].tagName == "IMG")
				return elements[itr];
			else
				return this._getFilterTypeImageRecus(elements[itr].childNodes);
		}
	}
	return null;
},
"_setFilterTypeImage",
function (filterType)
{
	
	var g = this.Row.Band.Grid;
	for (var itr = 0; itr < g.FilterButtonImages.length; itr++)
	{
		var fbi = g.FilterButtonImages[itr];
		if (filterType == fbi[0])
		{
			var img = this._getFilterTypeImage();
			if (img)
			{
				img.src = fbi[1];
				img.alt = img.title = fbi[2];
				img.setAttribute("operator", filterType);

				
				if (ig_csom.IsFireFox && img.parentNode && img.parentNode.tagName == "BUTTON")
					img.parentNode.title = fbi[2];
			}
		}
	}
}
];
for (var i = 0; i < igtbl_ptsCell.length; i += 2)
	igtbl_Cell.prototype[igtbl_ptsCell[i]] = igtbl_ptsCell[i + 1];

// Column object
igtbl_Column.prototype = new igtbl_WebObject();
igtbl_Column.prototype.constructor = igtbl_Column;
igtbl_Column.base = igtbl_WebObject.prototype;
function igtbl_Column(node, band, index, nodeIndex, colInitArray)
{
	if (arguments.length > 0)
		this.init(node, band, index, nodeIndex
			, colInitArray
		);
}
var igtbl_ptsColumn = [
"init",
function(node, band, index, nodeIndex
	, colInitArray
)
{
	igtbl_Column.base.init.apply(this, ["column", null, node]);

	this.Band = band;
	this.Index = index;
	this.Id = (band.Grid.Id
		+ "_"
		+ "c_" + band.Index.toString() + "_" + index.toString());
	if (band.ColFootersVisible == 1)
		this.fId = (band.Grid.Id
			+ "_"
			+ "f_" + band.Index.toString() + "_" + index.toString());
	var defaultProps = new Array("Key", "HeaderText", "DataType", "CellMultiline", "Hidden", "AllowGroupBy", "AllowColResizing", "AllowUpdate",
		"Case", "FieldLength", "CellButtonDisplay", "HeaderClickAction", "IsGroupBy", "MaskDisplay", "Selected",
		"SortIndicator", "NullText", "ButtonClass", "SelCellClass", "SelHeadClass", "ColumnType", "ValueListPrompt",
		"ValueList", "ValueListClass", "EditorControlID", "DefaultValue", "TemplatedColumn", "Validators",
		"CssClass", "Style", "Width", "AllowNull", "Wrap", "ServerOnly", "HeaderClass", "ButtonStyle", "Fixed", "FooterClass",
		"FixedHeaderIndicator", "FooterText", "HeaderStyle", "FooterStyle", "HeaderWrap"
		, "HeaderImageUrl", "HeaderImageAltText", "HeaderImageHeight", "HeaderImageWidth"
		, "MergeCells"
		, "DefaultFilterList", "RowFilterMode", "AllowRowFiltering", "GatherFilterData"
		, "FilterIcon"
        , "HeaderTitleMode"
	
        , "FilterOperatorDefaultValue"
	
        , "FilterComparisonType"
		, "SortingAlgorithm"
        , "SortCaseSensitive"
        , "WidthResolved"
		, "HTMLEncodeContent" 
								);
	var columnArray;
	columnArray = colInitArray;
	if (columnArray)
	{
		for (var i = 0; i < columnArray.length; i++)
			this[defaultProps[i]] = columnArray[i];
		if (this.Key && this.Key.length > 0)
			this.Key = unescape(this.Key);
		if (this.HeaderText && this.HeaderText.length > 0)
			this.HeaderText = unescape(this.HeaderText);
		if (this.HeaderImageUrl && this.HeaderImageUrl.length > 0)
			this.HeaderImageUrl = unescape(this.HeaderImageUrl);
		if (this.HeaderImageAltText && this.HeaderImageAltText.length > 0)
			this.HeaderImageAltText = unescape(this.HeaderImageAltText);
		
		
		this._AltCssClass = this.Band.getRowAltClassName() + (this.CssClass ? " " : "") + this.CssClass;
		this.CssClass = this.Band.getRowStyleClassName() + (this.CssClass ? " " : "") + this.CssClass;
	}
	this.ensureWebCombo();
	if (node)
	{
		this.Node.setAttribute("index", index + 1);
		this.Node.setAttribute("cellIndex", nodeIndex + 1);
	}
	igtbl_dispose(defaultProps);
	if (this.EditorControlID)
		this.editorControl = igtbl_getElementById(this.EditorControlID);

	if (this.Validators && this.Validators.length > 0 && typeof (Page_Validators) != "undefined")
	{
		for (var i = 0; i < this.Validators.length; i++)
		{
			var val = igtbl_getElementById(this.Validators[i]);
			if (val)
				val.enabled = false;
		}
	}
	this._Changes = new Object();
	this.SortImplementation = null;
},

"getEditorControl",
function()
{
	if (!this.editorControl)
		return null;
	if (this.editorControl.Object)
		this.editorControl = this.editorControl.Object;
	return this.editorControl;
},
"getAllowUpdate",
function()
{
	var g = this.Band.Grid;
	var res = g.AllowUpdate;
	if (this.Band.AllowUpdate != 0)
		res = this.Band.AllowUpdate;
	if (this.AllowUpdate != 0)
		res = this.AllowUpdate;
	if (this.TemplatedColumn & 2)
		res = 2;
	return res;
},
"setAllowUpdate",
function(value)
{
	this.AllowUpdate = value;
	switch (this.DataType)
	{
		case 11:  
			igtbl_AdjustCheckboxDisabledState(this, this.Band.Index, this.Band.Grid.Rows, this.getAllowUpdate());
			break;
	}
},
"getHidden",
function()
{
	return this.Hidden;
},
"setHidden",
function(h)
{
	if (this.Node)
	{
		if (h === false)
			this.Node.removeAttribute("hidden");
		else
			this.Node.setAttribute("hidden", true);
	}
	igtbl_hideColumn(this.Band.Grid.Rows, this, h);
	this.Hidden = h;
	if (this.Band.Index == 0)
		this.Band.Grid.alignStatMargins();
	var ac = this.Band.Grid.getActiveCell();
	if (ac && ac.Column == this && h)
		this.Band.Grid.setActiveCell(null);
	else
		this.Band.Grid.alignGrid();
},
"getVisible",
function()
{
	return !this.getHidden() && this.hasCells();
},
"hasCells",
function()
{
	return !this.ServerOnly && (!this.IsGroupBy || this.Band.GroupByColumnsHidden == 2);
},
"getNullText",
function()
{
	return igtbl_getNullText(this.Band.Grid.Id, this.Band.Index, this.Index);
},
"find",
function(re, back)
{
	var g = this.Band.Grid;
	if (re)
		g.regExp = re;
	if (!g.regExp || !this.hasCells())
		return null;
	g.lastSearchedCell = null;
	if (back == true || back == false)
		g.backwardSearch = back;
	var row = null;
	if (!g.backwardSearch)
	{
		row = g.Rows.getRow(0);
		if (row && row.getHidden())
			row = row.getNextRow();
		while (row && (row.Band != this.Band || row.getCellByColumn(this).getValue(true).search(g.regExp) == -1))
			row = row.getNextTabRow(false, true);
	}
	else
	{
		var rows = g.Rows;
		while (rows)
		{
			row = rows.getRow(rows.length - 1);
			if (row && row.getHidden())
				row = row.getPrevRow();
			if (row && row.Expandable)
				rows = row.Rows;
			else
			{
				if (!row)
					row = rows.ParentRow;
				rows = null;
			}
		}
		while (row && (row.Band != this.Band || row.getCellByColumn(this).getValue(true).search(g.regExp) == -1))
			row = row.getNextTabRow(true, true);
	}
	g.lastSearchedCell = (row ? row.getCellByColumn(this) : null);
	return g.lastSearchedCell;
},
"hideValidators",
function()
{
	
	if (!this.Validators)
		return;
	for (var v = 0; v < this.Validators.length; v++)
	{
		var validator = document.getElementById(this.Validators[v]);
		validator.isvalid = true;
		ValidatorUpdateDisplay(validator);
	}
},
"findNext",
function(re, back)
{
	var g = this.Band.Grid;
	if (!g.lastSearchedCell || g.lastSearchedCell.Column != this)
		return this.find(re, back);
	if (re)
		g.regExp = re;
	if (!g.regExp)
		return null;
	if (back == true || back == false)
		g.backwardSearch = back;
	var row = g.lastSearchedCell.Row.getNextTabRow(g.backwardSearch, true);
	while (row && (row.Band != this.Band || row.getCellByColumn(this).getValue(true).search(g.regExp) == -1))
		row = row.getNextTabRow(g.backwardSearch, true);
	g.lastSearchedCell = (row ? row.getCellByColumn(this) : null);
	return g.lastSearchedCell;
},
"getFooterText",
function()
{
	var fId = this.Band.Grid.Id
		+ "_"
		+ "f_" + this.Band.Index + "_" + this.Index;
	var foot = igtbl_getElementById(fId);
	if (foot)
		return igtbl_getInnerText(foot);
	return "";
},
"setFooterText",
function(value, useMask)
{
	var fId = this.Band.Grid.Id
		+ "_"
		+ "f_" + this.Band.Index + "_" + this.Index;
	var foot = igtbl_getDocumentElement(fId);
	if (foot)
	{
		if (useMask && this.MaskDisplay)
			value = igtbl_Mask(this.Band.Grid.Id, value.toString(), this.DataType, this.MaskDisplay);
		else if (useMask && this.getEditorControl() && this.editorControl.getRenderedValue)
			value = this.getEditorControl().getRenderedValue(value);
		if (igtbl_string.trim(value) == "")
			value = "&nbsp;";
		if (!foot.length)
			foot = [foot];
		var fElem = foot[0];
		if (fElem.childNodes.length > 0 && fElem.childNodes[0].tagName == "NOBR")
			value = "<nobr>" + value + "</nobr>";
		for (var i = 0; i < foot.length; i++)
		{
			fElem = foot[i];
			fElem.innerHTML = value;
		}
	}
},
"getSelClass",
function()
{
	if (this.SelCellClass != "")
		return this.SelCellClass;
	return this.Band.getSelClass();
},
"getHeadClass",
function()
{
	if (this.HeaderClass != "")
		return this.HeaderClass;
	return this.Band.getHeadClass();
},
"getFooterClass",
function()
{
	if (this.FooterClass != "")
		return this.FooterClass;
	return this.Band.getFooterClass();
},
"compareRows",
function(row1, row2)
{
	if (igtbl_columnCompareRows)
		return igtbl_columnCompareRows.apply(this, [row1, row2]);
	return 0;
},
"compareCells",
function(cell1, cell2)
{
	if (igtbl_columnCompareCells)
		return igtbl_columnCompareCells.apply(this, [cell1, cell2]);
	return 0;
},
"move",
function(toIndex)
{
	if (!this.Node) return;

	var band = this.Band;
	var bandNo = band.Index;
	var gs = band.Grid;
	if (bandNo == 0 && !band.IsGrouped)
	{
		var arIndex = -1, acColumn = null, acrIndex = -1;
		if (gs.oActiveRow && gs.oActiveRow.OwnerCollection == gs.Rows)
			arIndex = gs.oActiveRow.getIndex();
		if (gs.oActiveCell && gs.oActiveCell.Row.OwnerCollection == gs.Rows)
		{
			acColumn = gs.oActiveCell.Column;
			acrIndex = gs.oActiveCell.Row.getIndex();
		}
		gs.setActiveRow(null);
		gs.setActiveCell(null);
		
		if ((gs.StatHeader || gs.StatFooter) && gs.Rows.FilterRow)
		{
			var cells = gs.Rows.FilterRow.getCellElements();
			var cell = cells[this.Index];
			var insertCell = cells[toIndex];
			cell.parentNode.removeChild(cell);
			var parent = insertCell.parentNode;
			if (toIndex > this.Index)
			{
				var shouldAppend = (insertCell.nextSibling == null);
				if (shouldAppend) parent.appendChild(cell);
				else parent.insertBefore(cell, insertCell.nextSibling);
			}
			else
				parent.insertBefore(cell, insertCell);
			var cellObj = gs.Rows.FilterRow.cells[this.Index];
			gs.Rows.FilterRow.cells[this.Index] = gs.Rows.FilterRow.cells[toIndex];
			gs.Rows.FilterRow.cells[toIndex] = cellObj;
			cell = parent.cells[band.firstActiveCell];
			for (var i = 0; i < band.Columns.length && cell; i++)
				if (band.Columns[i].hasCells())
			{
				var splitId = cell.id.split("_");
				splitId[splitId.length - 1] = i.toString();
				cell.id = splitId.join("_");
				cellObj = gs.Rows.FilterRow.cells[i];
				if (cellObj)
				{
					cellObj.index = i;
					cellObj.Id = cell.id;
				}
				cell = cell.nextSibling;
			}
		}
		this._move(toIndex);
		gs.Rows.repaint();
		if (arIndex != -1)
			gs.Rows.getRow(arIndex).activate();
		
		if (acColumn && acrIndex >= 0)
			gs.Rows.getRow(acrIndex).getCellByColumn(acColumn).activate();
	}
	else
	{
		var elem = igtbl_getDocumentElement(this.Id);
		var rAr = new Array();
		if (typeof (elem) != "undefined")
		{
			if (!elem.length)
				elem = [elem];
			for (var i = 0; i < elem.length; i++)
			{
				var pe = elem[i].parentNode.parentNode.parentNode.parentNode;
				if (pe.tagName == "DIV" && pe.id.substr(pe.id.length - 4) == "_drs")
					pe = pe.parentNode.parentNode.parentNode.parentNode.parentNode;
				var ps = pe.parentNode.previousSibling;
				if (ps)
					rAr[i] = igtbl_getRowById(ps.id);
			}
		}
		var arIndex = -1, acColumn = null, acrIndex = -1, aRows = null;
		if (gs.oActiveRow)
		{
			arIndex = gs.oActiveRow.getIndex();
			aRows = gs.oActiveRow.OwnerCollection;
			if (aRows.Band.Index >= bandNo)
				gs.setActiveRow(null);
		}
		if (gs.oActiveCell)
		{
			acColumn = gs.oActiveCell.Column;
			acrIndex = gs.oActiveCell.Row.getIndex();
			aRows = gs.oActiveCell.Row.OwnerCollection;
			if (aRows.Band.Index >= bandNo)
				gs.setActiveCell(null);
		}
		this._move(toIndex);
		for (var i = 0; i < rAr.length; i++)
		{
			if (rAr[i])
			{
				rAr[i].Rows.repaint();
				if (aRows == rAr[i].Rows)
				{
					if (arIndex != -1)
						aRows.getRow(arIndex).activate();
					if (acColumn)
						aRows.getRow(acrIndex).getCellByColumn(acColumn).activate();
					aRows = null;
				}
				rAr[i] = null;
			}
		}
		igtbl_dispose(rAr);
		delete rAr;
	}
},
"_move",
function(toIndex)
{
	
	oldIndex = this.Index;
	this.Band.Grid._recordChange("ColumnMove", this, toIndex);
	var b = this.Band, oldSortedColumn = null;
	if (b.SortedColumns && b.SortedColumns.length > 0)
	{
		oldSortedColumn = new Array();
		for (var i = 0; i < b.SortedColumns.length; i++)
			for (var j = 0; j < b.Columns.length; j++)
			if (b.Columns[j].Id == b.SortedColumns[i])
		{
			oldSortedColumn[i] = b.Columns[j];
			break;
		}
	}
	var fromId = this.Id;
	var toColumn = this.Band.Columns[toIndex];
	var toId = toColumn.Id;
	this.Band.insertColumn(this.Band.removeColumn(this.Index), toIndex);
	if (typeof (this.Band._filterPanels) != "undefined" && this.Band._filterPanels)
	{
		var fromList = this.Band._filterPanels[fromId];
		var toList = this.Band._filterPanels[toId];
		if (typeof (fromList) != "undefined" || typeof (toList) != "undefined")
		{
			if (typeof (toList) != "undefined")
			{
				this.Band._filterPanels[fromId] = toList;
				toList.column = toColumn;
				toList.Id = toColumn.Id + "_Filter";
			}
			if (typeof (fromList) != "undefined")
			{
				this.Band._filterPanels[toId] = fromList;
				fromList.column = this;
				fromList.Id = this.Id + "_Filter";
			}
		}
	}
	if (oldSortedColumn)
		for (var i = 0; i < oldSortedColumn.length; i++)
	{
		b.SortedColumns[i] = oldSortedColumn[i].Id;
		oldSortedColumn[i] = null;
	}
	igtbl_dispose(oldSortedColumn);
	igtbl_swapCells(this.Band.Grid.Rows, this.Band.Index, oldIndex, toIndex);
},
"_filterOnBand",
function(bandIndex, recordSet)
{
	var band = this.Band;
	if (!recordSet || band.Index > bandIndex) return;
	if (bandIndex == recordSet.Band.Index)
	{
		this._filterOnRowIsland(recordSet);
	}
	else
	{
		var recordsetLength = recordSet.length;
		for (var itr = 0; itr < recordsetLength; itr++)
		{
			this._filterOnBand(bandIndex, recordSet.getRow(itr).Rows);
		}
	}
},
"_filterOnRowIsland",
function(rowCollection)
{
	
	var siblingRows = null;
	if (rowCollection)
	{
		siblingRows = rowCollection;
	}
	else
	{
		
		
		if ((this.Band.Index == 0 && this.Band.GroupCount == 0)
		)
		{
			siblingRows = this.Band.Grid.Rows;
		}
		else
		{
			
			
			var colE = this.Band.Grid.event.srcElement;
			if (!colE)
				this.Band.Grid.event.target;
			
			var parentTable = colE;
			do
			{
				parentTable = parentTable.parentNode;
			} while (parentTable && !(parentTable.tagName == "DIV" && parentTable.id.length > 0))

			if (!parentTable) return;

			var parentRow = igtbl_getRowById(parentTable.id.slice(0, parentTable.id.length - 7));

			if (parentRow)
				siblingRows = parentRow.Rows;
			else
				siblingRows = this.Band.Grid.Rows;
		}
	}
	var srCount = siblingRows.length;
	var cellIndex = this.Index;
	var oFilterConditions = null;
	if (this.Band.Index == 0 && this.Band.GroupCount == 0)
	{
		oFilterConditions = this.Band._filterPanels;
	}
	
	else if ((this.Band.Columns[0].RowFilterMode == 1 && this.Band.GroupCount == 0)
	)
	{
		
		oFilterConditions = this.Band._filterPanels;
	}
	else 
	{
		
		oFilterConditions = this.Band._filterPanels[siblingRows.Element.parentNode.id];
	}
	
	var myFilterCondition = oFilterConditions[this.Id];
	if (myFilterCondition && myFilterCondition.IsActive())
	{
		var myDirectColumnHeader = igtbl_getChildElementById(siblingRows.Element.parentNode, myFilterCondition.Column.Id);
		
		if (this.Band.Index == 0 && this.Band.Grid.StatHeader)
		{
			myDirectColumnHeader = this.Band.Grid.StatHeader.getElementByColumn(this)
		}
		else
		{
			myDirectColumnHeader = igtbl_getChildElementById(siblingRows.Element.parentNode, myFilterCondition.Column.Id);
		}
		if (myDirectColumnHeader)
		{
			
			var filterImg = this._findFilterImage(myDirectColumnHeader);
			if (filterImg)
			{
				
				var alt = filterImg.getAttribute("alt");
				if (myFilterCondition.getOperator() == igtbl_filterComparisionOperator.All)
				{
					filterImg.src = this.Band.Grid.FilterDefaultImage;
					if (alt != null)
					{
						var clpsAlt = filterImg.getAttribute("igAltF0");
						if (clpsAlt != null)
						{
							filterImg.setAttribute("igAltF1", alt);
							filterImg.setAttribute("alt", clpsAlt);
							filterImg.removeAttribute("igAltF0");
						}
					}
				}
				else
				{
					filterImg.src = this.Band.Grid.FilterAppliedImage;
					if (alt != null)
					{
						var clpsAlt = filterImg.getAttribute("igAltF1");
						if (clpsAlt != null)
						{
							filterImg.setAttribute("igAltF0", alt);
							filterImg.setAttribute("alt", clpsAlt);
							filterImg.removeAttribute("igAltF1");
						}
					}
				}
			}
		}
	}
	for (var srCounter = 0; srCounter < srCount; srCounter++)
	{
		
		this._evaluateFilters(siblingRows.getRow(srCounter), oFilterConditions, this.Band);
	}
},
"_findFilterImage",
function(elem)
{
	if (elem.tagName == "IMG" && elem.getAttribute("imgType") == "filter")
		return elem;
	for (var itr = 0; itr < elem.childNodes.length; itr++)
	{
		var e = this._findFilterImage(elem.childNodes[itr]);
		if (e) return e;
	}
	return null;
},
"_evaluateFilters",
function(oRow, oFilterCollection, oBand)
{
	
	if (oRow.GroupByRow)
	{
		var srCount = oRow.Rows.length;
		for (var srCounter = 0; srCounter < srCount; srCounter++)
		{
			
			this._evaluateFilters(oRow.Rows.getRow(srCounter), oFilterCollection, oBand);
		}
		return;
	}

	
	
	var showRow = true;
	for (var filter in oFilterCollection)
	{
		filter = oFilterCollection[filter];
		if (filter.IsActive())
		{
			var filterCol = filter.Column;
			
			var evalValue = filter.getEvaluationValue();
			var cellValue = oRow.getCell(filterCol.Index).getValue();
			
			switch (filterCol.DataType)
			{
				case 7:
					{
						if (evalValue)
							evalValue = new Date(evalValue).valueOf();
						if (cellValue)
							cellValue = new Date(cellValue).valueOf();
						break;
					}
				case 11:
					{
						if (evalValue)
							evalValue = igtbl_string.stringToBool(evalValue);
						if (cellValue)
							cellValue = igtbl_string.stringToBool(cellValue);
					}
			}
			if (!this._evaluateExpression(filter.getOperator(), cellValue, evalValue
                    , filterCol.FilterComparisonType, filterCol.DataType
				 ))
			{
				showRow = false;
				break;
			}
		}
	}
	// throw row filtering event passing in row and the value of the hidden field, if cancelled then dont change value of hidden		
	if (oRow.getHidden() != !showRow)
		oRow.setHidden(!showRow);
},
"_evaluateExpression",
function(operator, operand1, operand2
    , caseSensitive, columnDataType
)
{
	
	operator = parseInt(operator);
	switch (operator)
	{
		case (igtbl_filterComparisionOperator.NotEmpty):
			{
				return operand1 && (typeof (operand1) == "string" ? operand1.length > 0 : true);
				break;
			}
		case (igtbl_filterComparisionOperator.Empty):
			{
				if (operand1 === null) return true; 
				if (typeof (operand1) == "string")
					return operand1.length == 0;
				return false;
				break;
			}
		case (igtbl_filterComparisionOperator.All):
			{
				return true;
				break;
			}
		case (igtbl_filterComparisionOperator.Equals):
			{
				
				if (caseSensitive == igtbl_filterComparisonType.CaseInsensitive && columnDataType == 8 && operand1 && operand2)
				{
					
					
					operand2 = igtbl_regExp.escape(operand2);
					var re = new RegExp("^" + operand2 + "$", "i");
					return operand1.match(re) != null;
				}
				else
					return operand1 == operand2;
				break;
			}
		case (igtbl_filterComparisionOperator.NotEquals):
			{
				
				if (caseSensitive == igtbl_filterComparisonType.CaseInsensitive && columnDataType == 8 && operand1 && operand2)
				{
					
					
					operand2 = igtbl_regExp.escape(operand2);
					var re = new RegExp("^" + operand2 + "$", "i");
					return operand1.match(re) == null;
				}
				else
					return operand1 != operand2;
				break;
			}
		case (igtbl_filterComparisionOperator.Like):
			{
				if (columnDataType == 8)
				{
					
					
					operand2 = igtbl_regExp.escape(operand2);
					var re = new RegExp("^" + operand2, caseSensitive == 1 ? "i" : "");
					return operand1 && operand1.match(re);
					
				}
				return false;
				break;
			}
		case (igtbl_filterComparisionOperator.NotLike):
			{
				if (columnDataType == 8)
				{
					
					var likeMatches = this._evaluateExpression(igtbl_filterComparisionOperator.Like, operand1, operand2, caseSensitive, columnDataType) || this._evaluateExpression(igtbl_filterComparisionOperator.Equals, operand1, operand2, caseSensitive, columnDataType);
					return !likeMatches;
					//return ! this._evaluateExpression(igtbl_filterComparisionOperator.Like,operand1,operand2, caseSensitive, columnDataType) || this._evaluateExpression(igtbl_filterComparisionOperator.Equals,operand1,operand2, caseSensitive, columnDataType);
				}
				return false;
				break;
			}
		case (igtbl_filterComparisionOperator.GreaterThan):
			{
				if (columnDataType == 8)
				{
					if (operand1 == null) return false;
					if (operand1 && operand2 == null) return true;
					if (caseSensitive == igtbl_filterComparisonType.CaseInsensitive)
					{
						return operand1.toLowerCase() > operand2.toLowerCase();
					}
					else
					{
						return operand1 > operand2;
					}
				}
				else
				{
					return operand1 > operand2;
				}
				break;
			}
		case (igtbl_filterComparisionOperator.GreaterThanOrEqualTo):
			{
				return this._evaluateExpression(igtbl_filterComparisionOperator.GreaterThan, operand1, operand2, caseSensitive, columnDataType) || this._evaluateExpression(igtbl_filterComparisionOperator.Equals, operand1, operand2, caseSensitive, columnDataType);
				break;
			}
		case (igtbl_filterComparisionOperator.LessThanOrEqualTo):
			{
				return (!this._evaluateExpression(igtbl_filterComparisionOperator.GreaterThan, operand1, operand2, caseSensitive, columnDataType)) || this._evaluateExpression(igtbl_filterComparisionOperator.Equals, operand1, operand2, caseSensitive, columnDataType);
				break;
			}
		case (igtbl_filterComparisionOperator.LessThan):
			{
				return (!this._evaluateExpression(igtbl_filterComparisionOperator.GreaterThanOrEqualTo, operand1, operand2, caseSensitive, columnDataType));
				break;
			}
		case (igtbl_filterComparisionOperator.StartsWith):
			{
				if (columnDataType == 8)
				{
					if (operand1 == null) return false;
					if (operand1 && operand2 == null) return true;
					
					
					
					operand2 = igtbl_regExp.escape(operand2);
					var re = new RegExp("^" + operand2, caseSensitive == 1 ? "i" : "");
					return operand1 && operand1.match(re);
				}
				break;
			}
		case (igtbl_filterComparisionOperator.DoesNotStartWith):
			{
				return (!this._evaluateExpression(igtbl_filterComparisionOperator.StartsWith, operand1, operand2, caseSensitive, columnDataType));
				break;
			}
		case (igtbl_filterComparisionOperator.EndsWith):
			{
				if (columnDataType == 8)
				{
					if (operand1 == null) return false;
					if (operand1 && operand2 == null) return true;
					
					
					
					operand2 = igtbl_regExp.escape(operand2);
					var re = new RegExp(operand2 + "$", caseSensitive == 1 ? "i" : "");
					return operand1 && operand1.match(re);
				}
				break;
			}
		case (igtbl_filterComparisionOperator.DoesNotEndWith):
			{
				return (!this._evaluateExpression(igtbl_filterComparisionOperator.EndsWith, operand1, operand2, caseSensitive, columnDataType));
				break;
			}
		case (igtbl_filterComparisionOperator.Contains):
			{
				if (columnDataType == 8)
				{
					if (operand1 == null) return false;
					if (operand1 && operand2 == null) return true;
					var re = new RegExp(igtbl_regExp.escape(operand2), caseSensitive == 1 ? "i" : "");
					return operand1 && operand1.match(re);
				}
				break;
			}
		case (igtbl_filterComparisionOperator.DoesNotContain):
			{
				return (!this._evaluateExpression(igtbl_filterComparisionOperator.Contains, operand1, operand2, caseSensitive, columnDataType));
				break;
			}
	}
},
"_fillFilterList",
function(vc, sr)
{
	
	
	var srCount = sr.length;
	var cellIndex = this.Index;
	var oCell = null;
	var oRow = null;
	
	for (var srCounter = 0; srCounter < srCount; srCounter++)
	{
		oRow = sr.getRow(srCounter);
		var cellValue;
		var cellText;
		if (!oRow.GroupByRow)
		{
			oCell = oRow.getCell(cellIndex);

			
			
			cellValue = oCell.getValue();
			cellText = oCell.getValue(true);
			
			var valueDate = false;
			if (cellValue != null && typeof (cellValue.getFullYear) == "function")
			{
				cellValue = igtbl_dateToString(cellValue);
				valueDate = true;
			}
			
			if (cellText)
			{
				if ((typeof (cellValue) == "string" && cellValue.length == 0) || (typeof (cellText) == "string" && cellText.length == 0))
					continue;
				
				if (ig_shared.IsFireFox)
				{
					cellText = cellText.replace(/\r\n/g, " ");
					cellText = cellText.replace(/\n/g, " ");
					
					if (typeof (cellValue) == "string" && !valueDate)
						cellValue = cellText;
				}
				vc.push([cellValue, cellText]);
			}
		}
		else
		{
			this._fillFilterList(vc, oRow.Rows);
		}
	}
},
"_getSiblingRowIsland",
function()
{
	var siblingRows = null;
	
	
	if (this.Band.Index == 0 && this.Band.GroupCount == 0)
	{
		siblingRows = this.Band.Grid.Rows;
	}
	else
	{
		
		
		var colE = null;
		try
		{
			
			colE = this.Band.Grid.event.srcElement;
			if (!colE) colE = this.Band.Grid.event.target;
		}
		catch (e)
		{
			
			colE = this._filterSrcElement;
			delete this._filterSrcElement;
		}
		if (!colE) return null;
		
		var parentTable = colE;
		do
		{
			parentTable = parentTable.parentNode;
		} while (parentTable && !(parentTable.tagName == "TABLE" && parentTable.id.length > 0))
		if (!parentTable) return;
		var parentRow = igtbl_getRowById(parentTable.id);
		
		if (!parentRow)
			siblingRows = this.Band.Grid.Rows;
		else
			siblingRows = parentRow.Rows;
	}
	return siblingRows;
},
"_getFilterValuesFromSiblings",
function(rowCollection)
{
	
	var siblingRows = null;
	if (rowCollection)
	{
		siblingRows = rowCollection;
	}
	else
	{
		siblingRows = this._getSiblingRowIsland();
	}
	
	var workingList = new Array();

	if (!siblingRows || siblingRows.length == 0)
	{ }
	else
	{
		this._fillFilterList(workingList, siblingRows);
		
		if (this.DataType == 2 || this.DataType == 3 || this.DataType == 16 || this.DataType == 17 || this.DataType == 18 || this.DataType == 19 || this.DataType == 20 || this.DataType == 21)
			workingList.sort(_igtbl_sortNumber);
		else
			workingList.sort();
	}
	return workingList;
},

"_getFilterPanel",
function(sourceElement)
{
	
	
	var filterPanel = null;
	var band = this.Band;
	var g = band.Grid;
	
	if (this.RowFilterMode == 2) 
	{
		if (band.Index == 0 && band.GroupCount == 0)
		{
			if (!band._filterPanels[this.Id])
			{
				band._filterPanels[this.Id] = new igtbl_FilterDropDown(this);
			}
			filterPanel = band._filterPanels[this.Id];
			filterPanel.RowIsland = g.Rows;
		}
		else
		{
			
			var colE = sourceElement;

			if (!colE)
				colE = g.event.srcElement;

			
			if (!colE)
				colE = g.event.target;
			
			var parentTable = colE;
			
			
			if (band.Index == 0 && band.IsGrouped && (g.StationaryMargins == 1 || g.StationaryMargins == 3) && g.get("StationaryMarginsOutlookGroupBy") == "True")
			{
				parentTable = g.Rows.Element;
			}
			do
			{
				parentTable = parentTable.parentNode;
			} while (parentTable && !(parentTable.tagName == "TABLE" && parentTable.id.length > 0))
			if (!parentTable) return;

			
			filterPanel = band._filterPanels[parentTable.id];
			if (filterPanel)
				filterPanel = filterPanel[this.Id];
			else
			{
				band._filterPanels[parentTable.id] = new Object();
			}
			if (!filterPanel)
			{
				filterPanel = band._filterPanels[parentTable.id][this.Id] = new igtbl_FilterDropDown(this);
			}
			if (filterPanel.RowIsland == null || filterPanel.RowIsland.Type != "rows")
			{
				var row = igtbl_getRowById(parentTable.id);
				if (row)
					filterPanel.RowIsland = row.Rows;
				else
				{
					
					
					if (band.Index == 0 && band.IsGrouped && (g.StationaryMargins == 1 || g.StationaryMargins == 3) && g.get("StationaryMarginsOutlookGroupBy") == "True")
					{
						filterPanel.RowIsland = g.Rows;
					}
				}
			}
		}
	}
	else if (this.RowFilterMode == 1)
	{
		
		filterPanel = band._filterPanels[this.Id];
		if (!filterPanel)
		{
			filterPanel = band._filterPanels[this.Id] = new igtbl_FilterDropDown(this);
		}
		
		if (band.Index == 0 && band.IsGrouped)
		{
			filterPanel.RowIsland = g.Rows;
		}
	}
	else
	{
		return null;
	}
	return filterPanel;
},
"_getFilterValuesFromBand",
function()
{
	var resultSet;
	
	if (this.Band.Index == 0 && !this.Band.IsGrouped)
	{
		
		resultSet = this._getFilterValuesFromSiblings();
	}
	else
	{
		
		resultSet = this._fillFilterListFromBand(this.Band.Index, this.Band.Grid.Rows);
	}
	
	if (this.DataType == 2 || this.DataType == 3 || this.DataType == 16 || this.DataType == 17 || this.DataType == 18 || this.DataType == 19 || this.DataType == 20 || this.DataType == 21)
		resultSet.sort(_igtbl_sortNumber);
	else
		resultSet.sort();

	return resultSet;
},
"_fillFilterListFromBand",
function(bandIndex, recordSet)
{
	var resultSet = new Array();

	if (!recordSet || bandIndex < recordSet.Band.Index)
		return resultSet;

	if (bandIndex == recordSet.Band.Index)
	{
		resultSet = this._getFilterValuesFromSiblings(recordSet);
	}
	else
	{
		var recordsetLength = recordSet.length;
		for (var itr = 0; itr < recordsetLength; itr++)
		{
			var tempSet = this._fillFilterListFromBand(bandIndex, recordSet.getRow(itr).Rows);
			if (tempSet && tempSet.length > 0)
				resultSet = resultSet.concat(tempSet);
		}
	}
	return resultSet;
},
"showFilterDropDown",
function(drop)
{
	var autoDropCheck = (typeof (drop) === 'undefined')
	if (autoDropCheck)
		drop = true;

	
	if (this.AllowRowFiltering < 2) return; 

	
	var filterPanel = this._getFilterPanel();

	if (filterPanel == null) return;

	if (!drop)
	{
		if (filterPanel.IsDropped)
		{
			filterPanel.show(false);
		}
		return;
	}
	else
	{
		
		if (autoDropCheck)
			drop = !filterPanel.IsDropped;
		filterPanel.show(drop);
	}
},

"getFilterIcon",
function()
{
	return this.FilterIcon;
},

"setFilterIcon",
function(show)
{
	


	
	if (show != this.getFilterIcon())
	{
		this.FilterIcon = show;

		var headerTags = this._getHeadTags();
		if (headerTags)
		{
			
			for (var x = 0; x < headerTags.length; x++)
			{
				var filterIcon = this._findFilterImage(headerTags[x]);
				
				if (filterIcon)
				{
					if (show)
						filterIcon.style.display = "";
					else
						filterIcon.style.display = "none";
				}
			}

			
			this.Band.Grid._removeChange("ColumnFilterIconChanged", this);
			this.Band.Grid._recordChange("ColumnFilterIconChanged", this, show);
		}
	}
},
"getLevel",
function(s)
{
	var l = new Array();
	l[0] = this.Band.Index;
	l[1] = this.Index;
	if (s)
	{
		s = l.join("_");
		igtbl_dispose(l);
		delete l;
		return s;
	}
	return l;
},
"getFixed",
function()
{
	if (this.Band.Grid.UseFixedHeaders)
		return this.Fixed;
},
"setFixed",
function(fixed)
{
	this.Fixed = fixed;
},
"getWidth",
function()
{
	if (typeof (this.Width) != "string")
		return this.Width;
	var e = igtbl_getElementById(this.Id);
	if (!e || !e.offsetWidth || typeof (this.Width) == "string" && this.Width.substr(this.Width.length - 2, 2) == "px")
		this.Width = igtbl_parseInt(this.Width);
	if (typeof (this.Width) == "string")
	{
		this.Width = e.offsetWidth;
	}
	return this.Width;
},
"setWidth",
function (width)
{	
	var gs = this.Band.Grid, gn = gs.Id;
	var colObj = igtbl_getElementById(this.Id);
	
	if (!colObj) return;
	
	var scrLeft = gs._scrElem ? gs._scrElem.scrollLeft : 0;
	var fac = this.Band.firstActiveCell;
	var c1w = width;
	var corr = 0;
	var isBackCompat = document.compatMode && document.compatMode == "BackCompat"; 
	
	var widthDifference = this.getWidth() - width;
	if (c1w > 0 && !igtbl_fireEvent(gn, gs.Events.BeforeColumnSizeChange, "(\"" + gn + "\",\"" + colObj.id + "\"," + c1w + ")"))
	{
		var fixed = (gs.UseFixedHeaders && !this.getFixed());
		var colEl = this.Element;
		var isIECompat = gs.IsXHTML && ig_csom.IsIE && ig_csom.IsIE7Compat;
		
		if (colEl && colEl.offsetWidth && gs.UseFixedHeaders && isIECompat)
		{
			corr = colEl.offsetWidth - colEl.clientWidth;
			corr += igtbl_parseInt(colEl.currentStyle.paddingLeft);
			corr += igtbl_parseInt(colEl.currentStyle.paddingRight);
		}

		
		if ((gs.UseFixedHeaders || gs.XmlLoadOnDemandType == 2) && this.Band.Index == 0)
		{
			var origWidth = gs._scrElem.firstChild.offsetWidth;
			var scrw = origWidth + c1w - this.getWidth() - (fixed ? corr : 0);
			if (scrw >= 0 && this.Band.Index == 0 && scrw != origWidth)
			{
				gs._scrElem.firstChild.style.width = scrw + corr + "px";
				//this._xhtmlCorrected = true;
			}
		}

		
		var fixedColIndex = null;
		var columns = igtbl_getDocumentElement(this.Id);
		if (!columns.length)
			columns = [columns];
		if (fixed)
		{
			for (var i = 0; i < columns.length; i++)
			{
				var cells = igtbl_enumColumnCells(gn, columns[i]);
				for (var j = 0; j < cells.length; j++)
				{
					var cg = cells[j].parentNode.parentNode.previousSibling;
					if (cg)
					{
						var colIndex = cells[j].cellIndex;
						
						if (ig_csom.IsFireFox || (ig_csom.IsIEStandards && !isBackCompat))
						{
							var curIndex = this.Index;
							while (colIndex > 0 && --curIndex >= 0)
							
							
								if (!this.Band.Columns[curIndex].getVisible() && !this.Band.Columns[curIndex].getFixed()
								 && !this.Band.Columns[curIndex].IsGroupBy && !this.Band.Columns[curIndex].ServerOnly)
								colIndex--;
							
							fixedColIndex = colIndex;
						}
						var c = cg.childNodes[colIndex];
						if (c)
						{
							
							c.style.width = c1w + "px"; 
							c.width = c1w + "px";  
						}
					}
					
					cells[j].style.width = c1w + "px";
				}
			}
			var colFoots = igtbl_getDocumentElement(this.fId);
			if (colFoots)
			{
				if (!colFoots.length)
					colFoots = [colFoots];
				for (var i = 0; i < colFoots.length; i++)
				{
					var cg = colFoots[i].parentNode.parentNode.previousSibling;
					if (cg && cg.tagName == "COLGROUP")
					{
						var c = cg.childNodes[colFoots[i].cellIndex];
						
						
						if ((ig_csom.IsFireFox || (ig_csom.IsIEStandards && !isBackCompat)) && fixedColIndex != null && fixedColIndex > -1 && fixedColIndex < cg.childNodes.length)
							c = cg.childNodes[fixedColIndex];
						if (c)
						{
							
							c.style.width = c1w + "px";
							c.width = c1w + "px"; 
						}
					}
					var nfth = colFoots[i].parentNode;
					while (nfth && nfth.tagName != "TH")
						nfth = nfth.parentNode;
					if (nfth && this.Band.Index == 0 && this.Band.Index == 0 && gs.StatFooter)
					{
						cg = nfth.parentNode.parentNode.previousSibling;
						if (this.Band.AddNewRowView == 2 && gs.Rows.AddNewRow)
						{
							cg = cg.previousSibling;
							var addRow = gs.Rows.AddNewRow;
							var c = addRow.getCell(this.Index).Element.parentNode.parentNode.previousSibling.childNodes[colFoots[i].cellIndex];
							if (c)
							{
								
								c.style.width = c1w + "px";
							}
						}
						if (cg)
						{
							var nfthCellIndex = nfth.cellIndex;
							
							
							if (ig_csom.IsFireFox || (ig_csom.IsIEStandards && !isBackCompat))
							{
								var nfthElements = nfth.parentNode.childNodes;
								for (var cellIndexItr = 0; cellIndexItr < nfth.cellIndex; cellIndexItr++)
									if (nfthElements[0].style.display == "none")
									nfthCellIndex--;

							}
							 
							var c = cg.childNodes[(ig_csom.IsIE9Plus ? nfthCellIndex : (nfthCellIndex + colFoots[i].cellIndex))];
							if (c)
							{
								
								var newWidth = (!ig_csom.IsIE9Plus ? c1w : parseInt(c.width) - widthDifference);
								c.style.width = newWidth + "px";
								c.width = newWidth + "px"; 
							}
						}
					}
					
					colFoots[i].style.width = c1w + "px";
				}
			}
		}
		for (var i = 0; i < columns.length; i++)
		{
			var cg = columns[i].parentNode.parentNode.previousSibling;
			var colIndex = columns[i].cellIndex;
			
			
			
			if (columns[i].style.display == "none" || ig_csom.IsFireFox || (ig_csom.IsIEStandards && !isBackCompat))
			{
				var itr = 0;
				var parentCollection = columns[i].parentNode.childNodes;
				for (; itr < parentCollection.length; itr++)
				{
					if (parentCollection[itr] == columns[i]) break;
				}
				colIndex = itr;
			}

			
			
			
			
			if (ig_csom.IsFireFox || (ig_csom.IsIEStandards && !isBackCompat))
			{
				var pn = columns[i].parentNode;
				for (var j = 0; j <= columns[i].cellIndex; j++)
				{
					
					var colObject = igtbl_getColumnById(pn.childNodes[j].id);
					if (pn.childNodes[j].style.display == "none" && colObject && !colObject.getVisible())
						colIndex--;
				}
				if (colIndex < 0)
					continue;
			}


			if (this.Band.HasHeaderLayout && cg)
			{
				var colOffs = parseInt(columns[i].getAttribute("coloffs"), 10);

				if (this.getFixed() !== false)
					colOffs += this.Band.firstActiveCell;
				;
				var c = cg.childNodes[colOffs];
				
				var widthChange = c1w - c.width;
				
				c.style.width = c1w + "px";

				
				c.offsetParent.style.width = (c.offsetParent.clientWidth + widthChange) + "px";

				
				if (ig_csom.IsFireFox)
				{
					c.width = c1w;					
				}
				if (fixed)
				{
					var nfth = columns[i].parentNode;
					while (nfth && nfth.tagName != "TH")
						nfth = nfth.parentNode;
					if (nfth)
					{
						cg = nfth.parentNode.parentNode.previousSibling;
						if (cg)
						{
							 
							var c = cg.childNodes[(ig_csom.IsIE9Plus ? nfth.cellIndex : (nfth.cellIndex + colIndex))];
							
							
							var widthChange = c1w - c.width;
							
							var newWidth = (!ig_csom.IsIE9Plus ? c1w : parseInt(c.width) - widthDifference);
							c.style.width = newWidth + "px";
							c.width = newWidth + "px";  
							
							if (ig_csom.IsFireFox)
							{
								c.width = c1w;
								c.offsetParent.style.width = (c.offsetParent.clientWidth + widthChange) + "px";
							}
						}
						
					}
				}
			}
			else
			{
				var c;
				if (cg)
					c = cg.childNodes[colIndex];
				else
					c = columns[i];
				
				
				c.style.width = c1w + "px";
				c.width = c1w; 
				columns[i].style.width = c1w + "px";
				if (fixed)
				{
					var nfth = columns[i].parentNode;
					while (nfth && nfth.tagName != "TH")
						nfth = nfth.parentNode;
					if (nfth)
					{
						cg = nfth.parentNode.parentNode.previousSibling;
						if (cg)
						{
							var nfthCellIndex = nfth.cellIndex;
							
							
							if (ig_csom.IsFireFox || ig_csom.IsIEStandards)
							{
								var nfthElements = nfth.parentNode.childNodes;
								for (var cellIndexItr = 0; cellIndexItr < nfth.cellIndex; cellIndexItr++)
									if (nfthElements[0].style.display == "none")
									nfthCellIndex--;

							}
							 
							var c = cg.childNodes[(ig_csom.IsIE9Plus ? nfthCellIndex : (nfthCellIndex + colIndex))];
							
							var newWidth = (!ig_csom.IsIE9Plus ? c1w : parseInt(c.width) - widthDifference);
							c.style.width = newWidth + corr + "px";
							c.width = newWidth + corr + "px";  
						}
						
						if (this.Band.Index == 0 && this.Band.AddNewRowView == 1 && !this.Band.IsGrouped && gs.StatHeader)
						{
							cg = cg.previousSibling;
							var addRow = gs.Rows.AddNewRow;
							var c = addRow.getCell(this.Index).Element.parentNode.parentNode.previousSibling.childNodes[columns[i].cellIndex];
							
							c.style.width = c1w + "px";
						}
					}
				}
				else
				{
					var table = columns[i];
					while (table && table.tagName != "TABLE")
						table = table.parentNode;
					if (table && table.style.width.length > 0)
					{
						var oldWidth = table.style.width;
						if (oldWidth.length > 2 && oldWidth.substr(oldWidth.length - 2, 2) == "px")
						{
							var tbw = igtbl_parseInt(oldWidth) + c1w - this.getWidth(); 
							if (tbw > 0)
								table.style.width = tbw.toString() + "px";
						}
					}
				}

				
				if (gs.get("StationaryMarginsOutlookGroupBy") == "True" && this.Band.Index == 0 && this.Band.IsGrouped && i == 0)
				{
					table = gs.getDivElement().firstChild;
					
					


					
					for (var gri = 0; gri < gs.Rows.rows.length; gri++)
					{
						var grId = gn + "_gr_" + gri.toString();
						var grTbl, grWidth;
						
						if (document.getElementById(grId) && document.getElementById(grId).childNodes[0]
							    && document.getElementById(grId).childNodes[0].childNodes[0])
						{
							grTbl = document.getElementById(grId).childNodes[0].childNodes[0];
							
							
							
							
							var currentgrTblWidth = (grTbl.width && grTbl.width.indexOf("%") != -1 || grTbl.width == "") ? null : igtbl_parseInt(grTbl.width);
							if (currentgrTblWidth == null)
								break;
							grWidth = currentgrTblWidth + c1w - this.getWidth();
							grTbl.width = grWidth.toString() + "px";
						}

						for (var j = 1; j < gs.GroupCount; j++)
						{
							grId = grId + "_0";
							
							if (document.getElementById(grId) && document.getElementById(grId).childNodes[0]
							    && document.getElementById(grId).childNodes[0].childNodes[0])
							{
								grTbl = document.getElementById(grId).childNodes[0].childNodes[0];

								
								currentgrTblWidth = (grTbl.width && grTbl.width.indexOf("%") != -1) || grTbl.width == "" ? null : igtbl_parseInt(grTbl.width);
								if (currentgrTblWidth == null)
									break;
								grWidth = currentgrTblWidth + c1w - this.getWidth();
								grTbl.width = grWidth.toString() + "px";
							}
						}
					}
					
					if (table.style.width && table.style.width.indexOf("px") != -1)
					{
						var tblWidth = igtbl_parseInt(table.style.width);
						if (tblWidth)
							table.style.width = (tblWidth - widthDifference) + "px";
						if (gs.StatHeader)
						{
							if (gs.StatHeader.Element && gs.StatHeader.Element.parentNode && gs.StatHeader.Element.parentNode.tagName == "TABLE")
							{
								var statHeaderTbl = gs.StatHeader.Element.parentNode;
								
								
								
								var statHeaderTblWidth = 0;
								var statThs = gs.StatHeader.Element.previousSibling.childNodes;
								for (var idx = 0; idx < statThs.length; idx++)
								{
									var w = parseInt(statThs[idx].width);
									if (!isNaN(w))
										statHeaderTblWidth += w;
								}
								if (statHeaderTblWidth > 0)
								{
									statHeaderTbl.style.width = statHeaderTblWidth + "px";
								}
								
							}
						}

						if (gs.StatFooter)
						{
							if (gs.StatFooter.Element && gs.StatFooter.Element.parentNode && gs.StatFooter.Element.parentNode.tagName == "TABLE")
							{
								var statFooterTbl = gs.StatFooter.Element.parentNode;
								
								
								
								var statFooterTblWidth = 0;
								var statThs = gs.StatFooter.Element.previousSibling.childNodes;
								for (var idx = 0; idx < statThs.length; idx++)
								{
									var w = parseInt(statThs[idx].width);
									if (!isNaN(w))
										statFooterTblWidth += w;
								}
								if (statFooterTblWidth > 0)
								{
									statFooterTbl.style.width = statFooterTblWidth + "px";
								}
								
							}
						}
					}

				}
			}
		}

		this.Width = c1w;
		if (this.Node)
		{
			this.Node.setAttribute(igtbl_litPrefix + "width", c1w);
			
			this.Node.setAttribute(igtbl_litPrefix + "widthResolved", c1w);
		}
		if (this.Band.Index == 0)
		{
			if (gs.StatHeader)
				gs.StatHeader.ScrollTo(gs.Element.parentNode.scrollLeft);
			if (gs.StatFooter)
			{
				if (!fixed)
					gs.StatFooter.Resize(this.Index, c1w);
				gs.StatFooter.ScrollTo(gs.Element.parentNode.scrollLeft);
			}
		}
		gs.alignStatMargins();
		gs.alignDivs(0, true);
		this.Band._alignColumns();
		
		gs._alignFilterRow(gs.Rows);
		gs._removeChange("ResizedColumns", this);
		gs._recordChange("ResizedColumns", this, c1w);
		igtbl_fireEvent(gn, gs.Events.AfterColumnSizeChange, "(\"" + gn + "\",\"" + colObj.id + "\"," + c1w + ")");
		if (gs.NeedPostBack)
			igtbl_doPostBack(gn);
		
		var de = gs.getDivElement();
		if (!gs.MainGrid.style.height && de.clientHeight != de.scrollHeight && !de.getAttribute("scdAdded"))
		{
			var scDiv = document.createElement("DIV");
			scDiv.id = gs.Element.id + "_scd";
			scDiv.innerHTML = "&nbsp;";
			
			var divsHeight = de.scrollHeight - de.clientHeight;
			if (divsHeight < 0) divsHeight = -divsHeight;
			scDiv.style.height = divsHeight + 1;
			de.appendChild(scDiv);
			de.style.overflowY = "hidden";
			de.setAttribute("scdAdded", "true");
		}
		
		scrLeftNew = gs._scrElem ? gs._scrElem.scrollLeft : 0;
		if (scrLeft != scrLeftNew)
			gs._recordChange("ScrollLeft", gs, scrLeftNew);

		if (gs.UseFixedHeaders)
			igtbl_lineupHeaders(this.Id, this.Band);
		return true;
	}
	return false;
},
"ensureWebCombo",
function()
{
	if (typeof (igcmbo_getComboById) != "undefined" && igcmbo_getComboById(this.EditorControlID) && !this.WebComboId)
		this.WebComboId = this.EditorControlID;
},
"getRealIndex",
function(row)
{
	if (!this.hasCells())
		return -1;
	var ri = -1;
	var colspan = 1;
	var cell = null;
	if (row)
		cell = row.Element.cells[row.Band.firstActiveCell];
	var i = 0;
	while (i < this.Index + 1 && !this.Band.Columns[i].hasCells())
		i++;
	if (i > this.Index)
		return ri;
	ri = 0;
	for (; i < this.Index; i++)
	{
		if (!this.Band.Columns[i].hasCells())
			continue;
		if (row)
		{
			if (colspan > 1)
			{
				colspan--;
				continue;
			}
			var cellSplit;
			if (cell)
			{
				cellSplit = cell.id.split("_");
				if (parseInt(cellSplit[cellSplit.length - 1], 10) > i)
					ri--;
				else
				{
					cell = cell.nextSibling;
					if (cell)
						colspan = cell.colSpan;
				}
			}
		}
		ri++;
	}
	return ri;
},
"getFixedHeaderIndicator",
function()
{
	if (this.FixedHeaderIndicator != 0)
		return this.FixedHeaderIndicator;
	if (this.Band.FixedHeaderIndicator != 0)
		return this.Band.FixedHeaderIndicator;
	return this.Band.Grid.FixedHeaderIndicator;
},
"getValueFromString",
function(value)
{
	if (value == null || typeof (value) == "undefined")
		return null;
	value = value.toString();
	if (this.AllowNull && value == this.getNullText())
		return null;
	return igtbl_valueFromString(value, this.DataType);
}
, "_getHeadTags",
function(withAddRow)
{
	var elem = null;
	if (this.Id)
		elem = igtbl_getDocumentElement(this.Id);
	elem = igtbl_getArray(elem);
	if (withAddRow)
	{
		var addRow = this.Band.Grid.Rows.AddNewRow;
		var addNewPresent = (addRow && addRow.isFixedTop());
		if (!addNewPresent)
			return elem;

		var ri = this.Band.firstActiveCell;
		var columns = this.Band.Columns;
		for (var i = 0; i < this.Index; i++)
			if (columns[i].hasCells())
			ri++;
		if (this.getFixed() === false)
		{
			var fnfRi = this.Band.firstActiveCell;
			for (var i = 0; i < columns.length && columns[i].getFixed(); i++)
				if (columns[i].hasCells())
				fnfRi++;
			ri = ri - fnfRi;
			var tbl = addRow.Element.cells[fnfRi].firstChild.firstChild;
			elem[elem.length] = tbl.rows[0].cells[ri];
		}
		else
			elem[elem.length] = addRow.Element.cells[ri];
	}
	return elem;
},
"_getFootTags",
function(withAddRow)
{
	var elem = null;
	if (this.fId)
		elem = igtbl_getDocumentElement(this.fId);
	elem = igtbl_getArray(elem);
	if (withAddRow)
	{
		var addRow = this.Band.Grid.Rows.AddNewRow;
		var addNewPresent = (addRow && addRow.isFixedBottom());
		if (!addNewPresent)
			return elem;

		var ri = this.Band.firstActiveCell;
		var columns = this.Band.Columns;
		for (var i = 0; i < this.Index; i++)
			if (columns[i].hasCells())
			ri++;
		if (this.getFixed() === false)
		{
			var fnfRi = this.Band.firstActiveCell;
			for (var i = 0; i < columns.length && columns[i].getFixed(); i++)
				if (columns[i].hasCells())
				fnfRi++;
			ri = ri - fnfRi;
			var tbl = addRow.Element.cells[fnfRi].firstChild.firstChild;
			elem[elem.length] = tbl.rows[0].cells[ri];
		}
		else
			elem[elem.length] = addRow.Element.cells[ri];
	}
	return elem;
},
"_getColTags",
function(withAddRow)
{
	if (!this.hasCells())
		return null;
	var band = this.Band;
	var fac = band.firstActiveCell;
	var g = band.Grid;
	var columns = band.Columns;
	var res = new Array();
	var gColOffs = fac;
	if (!this.getHidden())
	{
		for (var i = 0; i < this.Index; i++)
			if (columns[i].getVisible())
			gColOffs++;
	}
	else
	{
		for (var i = 0; i < columns.length; i++)
			if (columns[i].hasCells())
			gColOffs++;
		for (var i = columns.length - 1; i >= this.Index; i--)
			if (columns[i].getHidden())
			gColOffs--;
	}
	var fnfColumn = null; 
	var lColOffs = 0;
	var fnfRi = fac;
	if (this.getFixed() === false)
	{
		fnfColumn = this;
		while (fnfColumn.Index > 0 && !this.Band.Columns[fnfColumn.Index - 1].getFixed())
			fnfColumn = this.Band.Columns[fnfColumn.Index - 1];
		for (var i = 0; i < fnfColumn.Index; i++)
		{
			if (columns[i].getVisible())
				lColOffs++;
			if (columns[i].hasCells())
				fnfRi++;
		}
		lColOffs = gColOffs - lColOffs - fac;
	}
	else
		lColOffs = gColOffs;
	var addRow = g.Rows.AddNewRow;
	
	var addNewHead = (addRow && !addRow.isFixedTop());
	var addNewFoot = (addRow && !addRow.isFixedBottom());
	var hAr = this._getHeadTags();
	if (hAr)
	{
		var cg;
		for (var i = 0; i < hAr.length; i++)
		{
			if (this.getFixed() === false)
			{
				var nfth = hAr[i].parentNode;
				while (nfth && nfth.tagName != "TH")
					nfth = nfth.parentNode;
				if (nfth)
				{
					cg = nfth.parentNode.parentNode.previousSibling;
					if (cg)
						res[res.length] = cg.childNodes[gColOffs];
				}
			}
			cg = hAr[i].parentNode.parentNode.previousSibling;
			if (cg)
				res[res.length] = cg.childNodes[lColOffs];
		}
	}
	var fAr = this._getFootTags();
	if (fAr)
	{
		var cg;
		for (var i = 0; i < fAr.length; i++)
		{
			if (this.getFixed() === false)
			{
				var nfth = fAr[i].parentNode;
				while (nfth && nfth.tagName != "TH")
					nfth = nfth.parentNode;
				if (nfth)
				{
					cg = nfth.parentNode.parentNode.previousSibling;
					if (addNewFoot)
						cg = cg.previousSibling;
					if (cg)
						res[res.length] = cg.childNodes[gColOffs];
				}
			}
			if (this.Band.Index == 0 && this.Band.Grid.StatFooter)
			{
				cg = fAr[i].parentNode.parentNode.previousSibling;
				if (this.getFixed() !== false && addNewFoot)
					cg = cg.previousSibling;
				if (cg)
					res[res.length] = cg.childNodes[lColOffs];
			}
		}
	}
	if (withAddRow && (addNewHead || addNewFoot) && this.getFixed() === false)
	{
		cg = addRow.Element.cells[fnfRi].firstChild.firstChild.firstChild;
		res[res.length] = cg.childNodes[lColOffs];
	}
	if (res.length > 0)
		return res;
	return null;
},
"_insertCols",
function(front, width)
{
	
	if (ig_csom.IsIE && igtbl_parseInt(width) < 1)
		width = "";

	var cols = this._getColTags(true);
	for (var i = 0; cols && i < cols.length; i++)
	{
		if (cols[i])
		{
			var col = document.createElement("COL");
			col.width = width;
			var cg = cols[i].parentNode;
			if (front)
				cg.insertBefore(col, cols[i]);
			else
			{
				if (cols[i].nextSibling)
					cg.insertBefore(col, cols[i].nextSibling);
				else
					cg.appendChild(col);
			}
		}
	}
},
"_reId",
function(i)
{
	if (i == this.Index) return;
	this._rec = true;
	for (var j = 0; j < this.Band.Columns.length; j++)
	{
		var col = this.Band.Columns[j];
		if (!col._rec && col.Index == i)
		{
			col._rec = true;
			this.Band.Columns[j]._reId(j);
			delete col._rec;
		}
	}
	delete this._rec;
	var elem = null;
	var fElem = null;
	column = this;
	if (column.hasCells())
	{
		if (this.Id)
			elem = this._getHeadTags(true);
		else
			elem = this.colElem;
		if (this.fId)
			fElem = this._getFootTags(true);
		else
			fElem = this.colFElem;
	}
	column.Id = this.Band.Grid.Id
		+ "_"
		+ "c_" + this.Band.Index.toString() + "_" + i.toString();
	column.Index = i;
	if (this.Band.ColFootersVisible == 1)
		column.fId = this.Band.Grid.Id
			+ "_"
			+ "f_" + this.Band.Index.toString() + "_" + i.toString();
	if (elem)
		for (var j = 0; j < elem.length; j++)
	{
		c = elem[j];
		if (c && c.tagName == "TH")
		{
			c.id = column.Id;
			c.setAttribute("columnNo", i.toString());
		}
		else if (c)
		{
			var r = c.parentNode;
			while (r && (r.tagName != "TR" || !r.getAttribute("level")))
				r = r.parentNode;
			if (r)
			{
				cid = r.id.split("_");
				cid[0] = cid[0].substr(0, cid[0].length - 1) + "c";
				cid[cid.length] = i.toString();
				c.id = cid.join("_")
			}
		}
	}
	if (fElem)
		for (var j = 0; j < fElem.length; j++)
	{
		c = fElem[j];
		if (c && c.tagName == "TH")
			c.id = column.fId;
		else if (c)
		{
			var r = c.parentNode;
			while (r && (r.tagName != "TR" || !r.getAttribute("level")))
				r = r.parentNode;
			if (r)
			{
				cid = r.id.split("_");
				cid[0] = cid[0].substr(0, cid[0].length - 1) + "c";
				cid[cid.length] = i.toString();
				c.id = cid.join("_")
			}
		}
	}
	igtbl_dispose(elem);
	igtbl_dispose(fElem);
	this._reIded = true;
}
, "setHeaderText",
function(value)
{
	
	var headerElements;
	if (this.Element)
	{
		headerElements = new Array(this.Element);
		var grid = this.Band.Grid;
		
		if (grid.StatHeader && grid.Rows.length == 0)
		{
			var header1 = igtbl_getChildElementById(grid.Element, this.Id);
			if (header1)
				headerElements[headerElements.length] = header1;
		}
	}
	else
	{
		
		headerElements = igtbl_getDocumentElement(this.Id);
		if (!headerElements)
		{
			return;
		}
		if (!headerElements.length) 
		{
			headerElements = new Array(headerElements);
		}
	}
	for (hE = 0; hE < headerElements.length; hE++)
	{
		var el = headerElements[hE];
		for (n = 0; n < el.childNodes.length; n++)
		{
			if (el.childNodes[n].nodeType == 1)
			{
				el = el.childNodes[n];
			}
		}
		if (el.tagName == "IMG")
		{
			if (el.imgType)
			{
				
				el.parentElement.innerHTML = value + el.outerHTML;
			}
			else
			{
				
				var caption;
				if (el.nextSibling && el.nextSibling.tagName == "NOBR")
				{
					caption = el.nextSibling;
				}
				else
				{
					caption = document.createElement("NOBR");
					if (el.nextSibling)
						el.parentElement.insertBefore(caption, el.nextSibling);
					else
						el.parentElement.appendChild(caption);
				}

				if (caption)
				{
					caption.innerHTML = value;
				}
			}
		}
		else
		{
			el.innerHTML = value;
		}
	}
}
, "getTitleModeResolved",
function()
{
	var result = this.HeaderTitleMode;
	if (!result && this.Band)
	{
		result = this.Band.HeaderTitleMode;
	}
	if (!result && this.Band && this.Band.Grid)
	{
		result = this.Band.Grid.HeaderTitleMode;
	}
	return result;
}
, "getSortingAlgorithm",
function()
{
	if (this.SortingAlgorithm == 0)
		return this.Band.getSortingAlgorithm();
	return this.SortingAlgorithm;
}
, "getSortImplementation",
function()
{
	if (this.SortImplementation == null)
		return this.Band.getSortImplementation();
	return this.SortImplementation;
}
];
for (var i = 0; i < igtbl_ptsColumn.length; i += 2)
	igtbl_Column.prototype[igtbl_ptsColumn[i]] = igtbl_ptsColumn[i + 1];


var igtbl_reqType = new Object();
igtbl_reqType.None = 0;
igtbl_reqType.ChildRows = 1;
igtbl_reqType.MoreRows = 2;
igtbl_reqType.Sort = 3;
igtbl_reqType.UpdateCell = 4;
igtbl_reqType.AddNewRow = 5;
igtbl_reqType.DeleteRow = 6;
igtbl_reqType.UpdateRow = 7;
igtbl_reqType.Custom = 8;
igtbl_reqType.Page = 9;
igtbl_reqType.Scroll = 10;
igtbl_reqType.FilterDropDownFill = 11;
igtbl_reqType.Filter = 12;
igtbl_reqType.Refresh = 13;
var igtbl_readyState = new Object();
igtbl_readyState.Ready = 0;
igtbl_readyState.Loading = 1;

var igtbl_error = new Object();
igtbl_error.Ok = 0;
igtbl_error.LoadFailed = 1;
igtbl_error.Timeout = 2;
var igtbl_featureRowView = {
	"Top": 1,
	"Bottom": 2
};
var igtbl_featureRowView = {
	"Top": 1,
	"Bottom": 2
};
var igtbl_filterComparisonType = {
	"CaseInsensitive": 1,
	"CaseSensitive": 2
};
var igtbl_RowFilterMode = {
	"AllRowsInBand": 1,
	"SiblingRowsOnly": 2
};

var igtbl_filterComparisionOperator = {
	"All": 0,
	"Empty": 1,
	"NotEmpty": 2,
	"Equals": 3
	, "NotEquals": 4,
	"Like": 5,
	"NotLike": 6,
	"LessThan": 7,
	"LessThanOrEqualTo": 8,
	"GreaterThan": 9,
	"GreaterThanOrEqualTo": 10
	, "StartsWith": 11,
	"DoesNotStartWith": 12,
	"EndsWith": 13,
	"DoesNotEndWith": 14,
	"Contains": 15,
	"DoesNotContain": 16
};


var igtbl_dataType = {
	"Int16": 2,
	"Int32": 3,
	"Single": 4,
	"Double": 5,
	"DateTime": 7,
	"String": 8,
	"Boolean": 11,
	"Object": 12,
	"Decimal": 14,
	"Byte": 16,
	"SByte": 17,
	"UInt16": 18,
	"UInt32": 19,
	"Int64": 20,
	"UInt64": 21,
	
	"Char": 22,
    // K.D. December 1, 2010 Bug #59342 The Guid should have its enum so it wouldn't default at the string enum
    "Guid": 23
};

var igtbl_CellTitleMode = new Object();
igtbl_CellTitleMode.NotSet = 0;
igtbl_CellTitleMode.Always = 1;
igtbl_CellTitleMode.OnOverflow = 2;
igtbl_CellTitleMode.Never = 3;
var igtbl_ClipboardError = {
	"Failure": -1,
	"Ok": 0,
	"NotSupported": 1,
	"NoActiveObject": 2,
	"NothingToPaste": 3,
	"NothingToCopy": 4
};

var igtbl_ClipboardOperation = {
	"Copy": 0,
	"Cut": 1,
	"Paste": 2
};
var igtbl_cellButtonDisplay = {
	"OnMouseEnter": 0,
	"Always": 1
};

// Client events object
igtbl_Events.prototype = new igtbl_WebObject();
igtbl_Events.prototype.constructor = igtbl_Events;
igtbl_Events.base = igtbl_WebObject.prototype;
function igtbl_Events(grid, eventsInitArray)
{
	if (arguments.length > 0)
		this.init(grid, eventsInitArray);
}
var igtbl_ptsEvents = [
"init",
function(grid, eventsInitArray)
{
	igtbl_Events.base.init.apply(this, ["events", null, null]);

	this._defaultProps = new Array("AfterCellUpdate", "AfterColumnMove", "AfterColumnSizeChange", "AfterEnterEditMode", "AfterExitEditMode",
								"AfterRowActivate", "AfterRowCollapsed", "AfterRowDeleted", "AfterRowTemplateClose", "AfterRowTemplateOpen",
								"AfterRowExpanded", "AfterRowInsert", "AfterRowSizeChange", "AfterSelectChange", "AfterSortColumn",
								"BeforeCellChange", "BeforeCellUpdate", "BeforeColumnMove", "BeforeColumnSizeChange", "BeforeEnterEditMode",
								"BeforeExitEditMode", "BeforeRowActivate", "BeforeRowCollapsed", "BeforeRowDeleted", "BeforeRowTemplateClose",
								"BeforeRowTemplateOpen", "BeforeRowExpanded", "BeforeRowInsert", "BeforeRowSizeChange", "BeforeSelectChange",
								"BeforeSortColumn", "ClickCellButton", "CellChange", "CellClick", "ColumnDrag", "ColumnHeaderClick", "DblClick",
								"EditKeyDown", "EditKeyUp", "InitializeLayout", "InitializeRow", "KeyDown", "KeyUp", "MouseDown", "MouseOver",
								"MouseOut", "MouseUp", "RowSelectorClick", "TemplateUpdateCells", "TemplateUpdateControls", "ValueListSelChange",
	
								"BeforeRowUpdate", "AfterRowUpdate",
								"BeforeXmlHttpRequest", "AfterXmlHttpResponseProcessed",
								"XmlHTTPResponse"
								, "XmlVirtualScroll"
								, "BeforeFilterDroppedDown", "BeforeFilterPopulated", "BeforeFilterClosed"
								, "AfterFilterDroppedDown", "AfterFilterPopulated", "AfterFilterClosed",
								"BeforeRowFilterApplied", "AfterRowFilterApplied"
								, "BeforeRowDeactivate"
								, "BeforeClipboardOperation", "AfterClipboardOperation", "ClipboardError"
								, "GridCornerImageClick"
								);
	var eventsArray;
	eventsArray = eventsInitArray;
	if (eventsArray)
		for (var i = 0; i < eventsArray.length; i++)
		this[this._defaultProps[i]] = eventsArray[i];
},
"unload",
function()
{
	for (var i = 0; i < this._defaultProps.length; i++)
		this[this._defaultProps[i]] = null;
	igtbl_dispose(this._defaultProps);
}
];
for (var i = 0; i < igtbl_ptsEvents.length; i += 2)
	igtbl_Events.prototype[igtbl_ptsEvents[i]] = igtbl_ptsEvents[i + 1];


function igtbl_fireEvent(gn, eventObj, eventString)
{
	var gs = igtbl_getGridById(gn);
	if (!gs || !gs.isLoaded()) return;
	var result = false;
	if (eventObj[0] != "")
	{
		
		try
		{
			if (typeof (eval(eventObj[0])) != "function")
				throw "Event handler does not exist.";
		}
		catch (ex)
		{
			alert("There is a problem with the event handler method: '" + eventObj[0] + "'. Please check the method name's spelling.")
			return false;
		}
		
		result = eval(eventObj[0] + eventString);
	}
	if (gs.GridIsLoaded && result != true && eventObj[1] >= 1 && !gs.CancelPostBack)
		igtbl_needPostBack(gn);
	gs.CancelPostBack = false;
	return result;
}

// obsolete
// use igcsom.cancelEvent instead 
function igtbl_cancelEvent(evnt)
{
	ig_cancelEvent(evnt);
	return false;
}

igtbl_FilterDropDown.prototype = new igtbl_WebObject();
igtbl_FilterDropDown.prototype.constructor = igtbl_FilterDropDown;
igtbl_FilterDropDown.base = igtbl_WebObject.prototype;
function igtbl_FilterDropDown(column)
{
	if (column != null)
	{
		var divElem, grid = column.Band.Grid;
		divElem = document.createElement("DIV");
		divElem.style.zIndex = grid._getZ(10000, 1);
		divElem.style.position = "absolute";
		divElem.setAttribute("filter", 1);
		divElem.setAttribute("bandNo", column.Band.Index);
		
		divElem.className = column.Band.FilterDropDownStyle;
		if (divElem.className.length == 0)
			divElem.className = grid.FilterDropDownStyle;
		divElem.id = column.Id + "_Filter";
		var mainGrid = grid.MainGrid;
		
		document.body.insertBefore(divElem, document.body.firstChild);
		
		this.init(divElem, column);
		divElem.style.display = "none";
	}
}
var igtbl_ptsFilterDropDown =
[
"init",
function(element, column)
{
	
	igtbl_FilterDropDown.base.init.apply(this, ["filterDropDown", element, null]);
	this.Column = column;
	this.RowIsland = null;
	this.Element.object = this;
	this._evaluationValue = null;
	this._operator = igtbl_filterComparisionOperator.Equals;
	this._activeFilter = false; 
},
"getHighlightStyle",
function()
{
	
	var b = this.Column.Band;
	if (b.FilterHighlightRowStyle && b.FilterHighlightRowStyle.length > 0)
		return b.FilterHighlightRowStyle;
	return b.Grid.FilterHighlightRowStyle;
},
"IsActive",
function()
{
	
	return this._activeFilter;
},
"setFilter",
function(operand, value, serverSet)
{
	this._operator = operand;
	this._evaluationValue = value;
	this._activeFilter = true;
	var rowIsland = this.RowIsland;
	var parentRowId;
	if (rowIsland && rowIsland.ParentRow)
	{
		parentRowId = rowIsland.ParentRow.getLevel(true) + "\x01" + rowIsland.ParentRow.DataKey;
	}
	else
	{
		parentRowId = "\x01";
	}
	var col = this.Column;
	var g = col.Band.Grid;

	
	g._removeFilterChange(col, parentRowId);
	
	g._recordChange("FilterColumn", col, operand + "\x01" + igtbl_escape(value) + "\x01" + parentRowId + "\x01" + (serverSet ? "server" : "client"));
},
"setOperator",
function(op)
{
	
	this._operator = op;
},
"getOperator",
function()
{
	
	return this._operator;
},
"setEvaluationValue",
function(op)
{
	this._evaluationValue = op;
},
"getEvaluationValue",
function()
{
	return this._evaluationValue;
},
"getWorkingFilterList",
function() { return this._currentWorkingList; },
"setWorkingFilterList",
function(oList) { this._currentWorkingList = oList; },
"_setFilter",
function(value)
{
	var band = this.Column.Band;
	switch (value)
	{
		case (band.Filter_AllString):
			{
				this.setFilter(igtbl_filterComparisionOperator.All, value);
				break;
			}
		case (band.Filter_EmptyString):
			{
				this.setFilter(igtbl_filterComparisionOperator.Empty, value);
				break;
			}
		case (band.Filter_NonEmptyString):
			{
				this.setFilter(igtbl_filterComparisionOperator.NotEmpty, value);
				break;
			}
		default:
			{
				
				this.setFilter(igtbl_filterComparisionOperator.Equals, value);
				break;
			}
	}
	var grid = band.Grid;
	return grid.fireEvent(grid.Events.BeforeRowFilterApplied, [grid.Id, this.Column]);
},
"applyFilter",
function()
{
	
	var col = this.Column;
	var g = col.Band.Grid;
	if (g.LoadOnDemand == 3 && !col.Band.IsGrouped)
	{
		g.invokeXmlHttpRequest(g.eReqType.Filter, col, (col.RowFilterMode == 1 && col.Band.FilterUIType != 1 ? null : this.RowIsland));
		return;
	}
	
	if (this.Column.AllowRowFiltering == 3)
	{
		igtbl_doPostBack(g.Id);
		return;
	}
	if (col.RowFilterMode == 1)
	{
		col._filterOnBand(col.Band.Index, col.Band.Grid.Rows);
	}
	else 
	{
		col._filterOnRowIsland(this.RowIsland);
	}
},
"_showFillingList",
function(col, workingList)
{
	
	if (typeof (workingList) == "undefined")
		workingList = this._currentWorkingList;
	
	var resultList;
	if (col.RowFilterMode == 1)
	{
		resultList = col._getFilterValuesFromBand();
	}
	else 
	{
		resultList = col._getFilterValuesFromSiblings();
	}
	
	resultList = this._cleanList(resultList);
	workingList = workingList.concat(resultList);
	col.Band.Grid._hidePI();
	
	this._afterFilterFilled(col.Band.Grid, col, workingList);
	return workingList;
},
"_afterFilterFilled",
function(grid, col, workingList)
{
	this._currentWorkingList = workingList;

	
	grid.fireEvent(grid.Events.AfterFilterPopulated, [grid.Id, this, workingList]);

	var filterTableElem = this._buildFilterTable(workingList, this);
	
	this._lastWorkingList = workingList;
	this._filterTable = filterTableElem;
	this._showFilter(filterTableElem);

	col.Band.Grid._currentFilterShowing = filterTableElem;
	
	grid.fireEvent(grid.Events.AfterFilterDroppedDown, [grid.Id, this]);

	this.IsDropped = true;
	this.Element.style.display = "";
	this.Column.Band.Grid._currentFilterDropped = this;
},
"show",
function(show)
{
	var col = this.Column;
	var grid = col.Band.Grid;
	if (show)
	{
		if (this.IsDropped) return;
		for (var gridId in igtbl_gridState)
		{
			var g = igtbl_getGridById(gridId);
			if (g._currentFilterDropped)
				g._currentFilterDropped.show(false);
		}
		
		if (grid.fireEvent(grid.Events.BeforeFilterDroppedDown, [grid.Id, this]) == true)
		{
			return true;
		}
		
		
		var workingList = new Array();

		for (var iList = 0; iList < col.DefaultFilterList.length; iList++)
		{
			workingList.push(col.DefaultFilterList[iList]);
		}

		this._currentWorkingList = workingList;

		
		if (this.Column.GatherFilterData == 2 || grid.fireEvent(grid.Events.BeforeFilterPopulated, [grid.Id, this, this.Column, this._currentWorkingList, this._lastWorkingList]) == true)
		{
			
			workingList = this._currentWorkingList;
			
			this._afterFilterFilled(grid, col, workingList);
		}
		else
		{
			
			
			
			if (grid.LoadOnDemand == 3  )
			{
				grid.invokeXmlHttpRequest(grid.eReqType.FilterDropDownFill, col, (col.RowFilterMode == 1 ? null : col._getSiblingRowIsland()));
				return;
			}
			if (grid.EnableProgressIndicator)
			{
				
				grid._displayPI();
				
				
				if ((col.Band.Index > 0 && this.RowIsland) || (col.Band.IsGrouped && col.Band.RowFilterMode == 2))
				{
					
					col._filterSrcElement = grid.event.srcElement;

					if (!col._filterSrcElement)
						col._filterSrcElement = grid.event.target;

					setTimeout("igtbl_getGridById('" + grid.Id + "').Bands[" + col.Band.Index + "]._filterPanels['" + this.RowIsland.Element.parentNode.id + "']['" + col.Id + "']._showFillingList(igtbl_getColumnById('" + col.Id + "'));");
				}
				else
					setTimeout("igtbl_getGridById('" + grid.Id + "').Bands[" + col.Band.Index + "]._filterPanels['" + col.Id + "']._showFillingList(igtbl_getColumnById('" + col.Id + "'));");
			}
			else
				this._showFillingList(col);
		}
	}
	else
	{
		if (grid.fireEvent(grid.Events.BeforeFilterClosed, [grid.Id, this]) == true)
		{ return true; }
		this.IsDropped = false;
		if (col.Band.transPanel) col.Band.transPanel.hide();
		this.Element.style.display = "none";
		grid._currentFilterDropped = null;
		grid.fireEvent(grid.Events.AfterFilterClosed, [grid.Id, this]);
	}
},
"_cleanList",
function(workingList)
{
	if (workingList == null || workingList.length < 2) return workingList;
	var currentValue = workingList[workingList.length - 1];
	for (var itr = workingList.length - 2; itr > -1; itr--)
	{
		if (currentValue[1] === workingList[itr][1])
		{
			workingList.splice(itr, 1);
		}
		else
		{
			currentValue = workingList[itr];
		}
	}
	return workingList;
},
"_buildFilterTable",
function(workingList, filterObject)
{
	var divElem;
	if (workingList === this._lastWorkingList && this._filterTable)
	{
		divElem = this._filterTable;
	}
	else
	{
		divElem = this.Element;
		
		divElem.style.overflow = "auto";
		if (divElem.childNodes.length > 0)
		{
			for (var itr = divElem.childNodes.length - 1; itr >= 0; itr--)
				divElem.removeChild(divElem.childNodes[itr]);
		}
		var elem = document.createElement("TABLE");
		elem.cellSpacing = 0;
		elem.className = divElem.className;
		elem.style.borderStyle = "none"; 
		elem.style.borderWidth = "0px";
		ig_csom.addEventListener(elem, "mouseup", igtbl_filterMouseUp);
		ig_csom.addEventListener(elem, "mouseover", igtbl_filterMouseOver);
		ig_csom.addEventListener(elem, "mouseout", igtbl_filterMouseOut);
		ig_csom.addEventListener(elem, "selectstart", ig_cancelEvent);
		ig_csom.addEventListener(document, "mouseup", igtbl_filterMouseUpDocument);

		var gridDiv = this.Column.Band.Grid.getDivElement()
		ig_csom.addEventListener(gridDiv, "scroll", igtbl_filterGridScroll); 
		
		elem._filterObject = filterObject;
		var colGroup = document.createElement("COLGROUP");
		var tbody = document.createElement("TBODY");
		elem.appendChild(colGroup);
		elem.appendChild(tbody);
		divElem.appendChild(elem);
		var column = document.createElement("COL");
		column.style.width = "100%";
		colGroup.appendChild(column);
		for (var itr = 0; itr < workingList.length; itr++)
		{
			var row = document.createElement("TR");
			ig_csom.addEventListener(row, "mouseup", igtbl_filterOptionMouseUp);
			
			row.setAttribute("fo", 1);
			row.style.height = this.Column.Band.DefaultRowHeight;
			
			row.setAttribute("value", workingList[itr][0]);
			var cell = document.createElement("TD");
			var cellText = document.createTextNode(workingList[itr][1]);
			cell.appendChild(cellText);
			row.appendChild(cell);
			tbody.appendChild(row);
		}
	}
	return divElem;
},
"_showFilter",
function(filterDivElem)
{
	var band = this.Column.Band;
	var gridObj = band.Grid;
	
	var tPan = band.transPanel;
	if (tPan == null && ig_csom.IsIEWin)
	{
		band.transPanel = tPan = ig_csom.createTransparentPanel();
		if (tPan)
		{
			filterDivElem.parentNode.insertBefore(tPan.Element, filterDivElem);
			tPan.Element.style.zIndex = igtbl_parseInt(filterDivElem.style.zIndex) - 1;
		}
	}
	
	var fc = this.Column.Element;
	if (!fc || !fc.offsetHeight)
	{
		// A.T. Fix for bug #25473 - JScript error is thrown while applying filter.
		if (ig_csom.IsIEStandards && this.Column.Band.Index == 0 && this.Column.Element)
		{
			fc = igtbl_getDocumentElement(this.Column.Element.id)[1];
		}
		else
			try
		{
			fc = gridObj.event.srcElement;
			if (!fc)
			{
				fc = gridObj.event.target;
			}
		}
		catch (excep)
		{
		}
		if (!fc)
		{
			
			var colHeaderIndex = 0;
			var headerTags = this.Column._getHeadTags();
			

			
			if (this.RowIsland && this.RowIsland.Element && !(band.Index == 0 && gridObj.StatHeader))
			{
				var parentTable = this.RowIsland.Element;
				do
				{
					parentTable = parentTable.parentNode;
				} while (parentTable && !(parentTable.tagName == "TABLE" && parentTable.id.length > 0))
				if (parentTable)
				{
					for (var itr = 0; itr < headerTags.length; itr++)
					{
						var parTable = headerTags[itr];
						do
						{
							parTable = parTable.parentNode;
						}
						while (parTable && !(parTable.tagName == "TABLE" && parTable.id.length > 0))
						if (parTable.id == parentTable.id)
						{
							colHeaderIndex = itr;
						}
					}
				}
			}
			fc = headerTags[colHeaderIndex];
		}
		while (fc.tagName != "TH")
		{
			fc = fc.parentNode;
		}
	}
	
	filterDivElem.style.display = "";
	
	if (band.FilterDropDownRowCount > 0)
	{
		var rows = filterDivElem.childNodes[0].childNodes[1].childNodes;
		if (rows.length > 0)
		{
			var calcValue = (rows.length < band.FilterDropDownRowCount) ? rows.length : band.FilterDropDownRowCount;
			var calcHeight = (rows[0].clientHeight * calcValue);
			var scrollBarHeight = filterDivElem.offsetHeight - filterDivElem.clientHeight;
			
			filterDivElem.style.height = (calcHeight + scrollBarHeight) + "px";
		}
	}
	
	
	if (!filterDivElem.getAttribute("adjusted") && filterDivElem.offsetWidth - filterDivElem.clientWidth > 10 && filterDivElem.offsetWidth - filterDivElem.clientWidth < 30)
	{
		filterDivElem.style.width = (filterDivElem.offsetWidth + (filterDivElem.offsetWidth - filterDivElem.clientWidth)).toString() + "px";
		filterDivElem.setAttribute("adjusted", "true");
	}
	ig_csom.absPosition(fc, filterDivElem, ig_Location.BelowLeft, tPan);

	
	
	if (gridObj.StatHeader && gridObj.Rows.length < 1 && this.Column.Element != null && this.Column.Element.parentElement != null)
	{
		var offs = filterDivElem.style.posTop + this.Column.Element.parentElement.clientHeight;
		filterDivElem.style.top = offs + "px";
		band.transPanel.Element.style.top = (offs - 1) + "px";
	}

	this._filterPanel = filterDivElem;
}
]

for (var i = 0; i < igtbl_ptsFilterDropDown.length; i += 2)
	igtbl_FilterDropDown.prototype[igtbl_ptsFilterDropDown[i]] = igtbl_ptsFilterDropDown[i + 1];



igtbl_FilterIconsList.prototype = new igtbl_WebObject();
igtbl_FilterIconsList.prototype.constructor = igtbl_FilterIconsList;
igtbl_FilterIconsList.base = igtbl_WebObject.prototype;
function igtbl_FilterIconsList(column)
{
	if (column != null)
	{
		var divElem, grid = column.Band.Grid;
		divElem = document.createElement("DIV");
		divElem.style.zIndex = grid._getZ(10000, 1);
		divElem.style.position = "absolute";
		divElem.setAttribute("filterIconList", 1);
		divElem.setAttribute("bandNo", column.Band.Index);
		divElem.id = column.Id + "_FilterIconList";
		var mainGrid = grid.MainGrid;
		document.body.insertBefore(divElem, document.body.firstChild);
		this.init(divElem, column);
		divElem.style.display = "none";
	}
}
var igtbl_ptsFilterIconDropDown =
[
"init",
function(element, column)
{
	
	igtbl_FilterDropDown.base.init.apply(this, ["filterIconDropDown", element, null]);
	this.Column = column;
	this.Element.object = this;
	
	var gridDiv = this.Column.Band.Grid.getDivElement()
	ig_csom.addEventListener(gridDiv, "scroll", igtbl_filterGridScroll);
	var divElem = element;
	var g = column.Band.Grid;
	var elem = document.createElement("TABLE");
	elem.cellSpacing = 0;
	elem.className = g.FilterOperandDropDownStyle + " " + column.Band.FilterOperandDropDownStyle;
	ig_csom.addEventListener(elem, "selectstart", ig_cancelEvent);
	ig_csom.addEventListener(elem, "mouseup", igtbl_filterIconsMouseUp);
	ig_csom.addEventListener(elem, "mouseover", igtbl_filterMouseOver);
	ig_csom.addEventListener(elem, "mouseout", igtbl_filterMouseOut);
	ig_csom.addEventListener(document, "mouseup", igtbl_filterMouseUpDocument);
	var colGroup = document.createElement("COLGROUP");
	var tbody = document.createElement("TBODY");
	elem.appendChild(colGroup);
	elem.appendChild(tbody);
	divElem.appendChild(elem);
	var column = document.createElement("COL");
	column.style.width = "100%";
	colGroup.appendChild(column);

	for (var itr = 0; itr < g.FilterButtonImages.length; itr++)
	{
		var filterImageObj = g.FilterButtonImages[itr];
		var row = document.createElement("TR");
		var cell = document.createElement("TD");
		var div = document.createElement("DIV");
		cell.appendChild(div);
		var img = document.createElement("IMG");
		img.src = filterImageObj[1];
		img.title = filterImageObj[2];
		img.style.verticalAlign = "middle";
		img.setAttribute("operator", filterImageObj[0]);
		div.appendChild(img);
		
		cell.className = g.FilterOperandItemStyle + " " + this.Column.Band.FilterOperandItemStyle;
		row.appendChild(cell);
		var cellText = document.createTextNode(this.Column.Band.FilterOperandStrings[itr]);
		div.appendChild(cellText);
		tbody.appendChild(row);
		row.setAttribute("filterListOption", "true");
		
		row.setAttribute("operator", filterImageObj[0]);
	}
},
"show",

function(cell, force)
{
	
	if (force && this.IsDropped) return;
	
	var col = this.Column;
	var band = col.Band;
	var grid = band.Grid;
	
	if (cell)
	{
		if (grid.fireEvent(grid.Events.BeforeFilterDroppedDown, [grid.Id, this]) == true)
		{
			return true;
		}
		for (var gridId in igtbl_gridState)
		{
			var g = igtbl_getGridById(gridId);
			if (g._currentFilterDropped)
				g._currentFilterDropped.show(false);
		}
		this._showListOption(igtbl_filterComparisionOperator.Like, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.NotLike, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.Contains, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.DoesNotContain, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.StartsWith, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.DoesNotStartWith, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.EndsWith, col.DataType == igtbl_dataType.String);
		this._showListOption(igtbl_filterComparisionOperator.DoesNotEndWith, col.DataType == igtbl_dataType.String);
        // K.D. December 1, 2010 Bug #59342 The Guid should display only filters appropriate for the Guid datatype
        if(col.DataType == igtbl_dataType.Guid)
        {
            this._showListOption(igtbl_filterComparisionOperator.LessThan, false);
            this._showListOption(igtbl_filterComparisionOperator.LessThanOrEqualTo, false);
            this._showListOption(igtbl_filterComparisionOperator.GreaterThan, false);
            this._showListOption(igtbl_filterComparisionOperator.GreaterThanOrEqualTo, false);
        }
		this._showDropDown(this.Element);
		this.IsDropped = true;
		
		grid.fireEvent(grid.Events.AfterFilterDroppedDown, [grid.Id, this]);
		
		this._currentCell = cell;
		grid._currentFilterDropped = this;
	}
	else
	{
		this.Element.style.display = "none";
		this.IsDropped = false;
		if (band.transPanel)
			band.transPanel.hide();
		grid._currentFilterDropped = null;
		this._currentCell = null;
	}
},
"_showDropDown",
function(filterDivElem)
{
	var band = this.Column.Band;
	var gridObj = band.Grid;
	
	var tPan = band.transPanel;
	if (tPan == null && ig_csom.IsIEWin)
	{
		band.transPanel = tPan = ig_csom.createTransparentPanel();
		if (tPan)
		{
			filterDivElem.parentNode.insertBefore(tPan.Element, filterDivElem);
			tPan.Element.style.zIndex = igtbl_parseInt(filterDivElem.style.zIndex) - 1;
		}
	}
	
	var fc;
	try
	{
		fc = gridObj.event.srcElement;
		if (!fc)
		{
			fc = gridObj.event.target;
		}
	}
	catch (excep)
	{
	}
	
	filterDivElem.style.display = "";

	if (!filterDivElem.getAttribute("adjusted") && filterDivElem.offsetWidth - filterDivElem.clientWidth > 10 && filterDivElem.offsetWidth - filterDivElem.clientWidth < 30)
	{
		filterDivElem.style.width = (filterDivElem.offsetWidth + (filterDivElem.offsetWidth - filterDivElem.clientWidth)).toString() + "px";
		filterDivElem.setAttribute("adjusted", "true");
	}

	ig_csom.absPosition(fc, filterDivElem, ig_Location.BelowLeft, tPan);
	
	filterDivElem.CurrentDropSource = this;
}
,
"getHighlightStyle",
function()
{
	
	var b = this.Column.Band;
	
	return b.Grid.FilterOperandItemHoverStyle + " " + b.FilterOperandItemHoverStyle;
},
"_showListOption",
function(option, show)
{
	
	
	var e = this.Element.childNodes[0].childNodes[1].childNodes; 
	for (var itr = 0; itr < e.length; itr++) 
	{
		var node = e[itr];
		if (node.getAttribute("operator") == option)
		{
			node.style.display = (show ? "" : "none");
		}
	}
}
]
for (var i = 0; i < igtbl_ptsFilterIconDropDown.length; i += 2)
	igtbl_FilterIconsList.prototype[igtbl_ptsFilterIconDropDown[i]] = igtbl_ptsFilterIconDropDown[i + 1];

var igtbl_waitDiv = null;
var igtbl_wndOldCursor = null;
var igtbl_oldMouseDown = null;
var igtbl_currentEditTempl = null;
var igtbl_focusedElement = null;

// Grid object
igtbl_Grid.prototype = new igtbl_WebObject();
igtbl_Grid.prototype.constructor = igtbl_Grid;
igtbl_Grid.base = igtbl_WebObject.prototype;
function igtbl_Grid(element, node
	, gridInitArray, bandsInitArray, colsInitArray, eventsInitArray, xmlInitProps
)
{
	if (arguments.length > 0)
		this.init(element, node
		, gridInitArray, bandsInitArray, colsInitArray, eventsInitArray, xmlInitProps
		);
}


var igtbl_ptsGrid = [
"init",
function (element, node
	, gridInitArray, bandsInitArray, colsInitArray, eventsInitArray, xmlInitProps
)
{
	igtbl_Grid.base.init.apply(this, ["grid", element, node]);
	this.IsXHTML = igtbl_isXHTML;
	if (node)
	{
		this.XmlNS = "";
		this.Xml = node;

		this.Node = this.Xml.selectSingleNode("UltraWebGrid/UltraGridLayout");
	}
	this.ViewState = ig_ClientState.addNode(ig_ClientState.createRootNode(), "UltraWebGrid");
	this.ViewState = ig_ClientState.addNode(this.ViewState, "DisplayLayout");
	this.StateChanges = ig_ClientState.addNode(this.ViewState, "StateChanges");

	this.Id = this.Id.substr(2);

	// Initialize properties

	this._Changes = new Array();

	this.SelectedRows = new Object();
	this.SelectedColumns = new Object();
	this.SelectedCells = new Object();
	this.SelectedCellsRows = new Object();
	this.ExpandedRows = new Object();
	this.CollapsedRows = new Object();
	this.ResizedColumns = new Object();
	this.ResizedRows = new Object();
	this.ChangedRows = new Object();
	this.ChangedCells = new Object();
	this.AddedRows = new Object();
	this.DeletedRows = new Object();
	//** OBSOLETE ***
	this.ActiveCell = "";
	this.ActiveRow = "";
	this.grid = this;
	this.activeRect = null;
	this.SuspendUpdates = false;
	//** END OBSOLETE ***

	this.timeout = 120000;
	this._lastSelectedRow = "";
	this.ScrollPos = 0;
	this.currentTriImg = null;
	this.newImg = null;

	this.NeedPostBack = false;
	this.CancelPostBack = false;
	this.GridIsLoaded = false;

	this._exitEditCancel = false;
	this._noCellChange = false;
	this._insideSetActive = false;
	this.MainGrid = igtbl_getElementById(this.Id + "_main");
	this.DivElement = igtbl_getElementById(this.Id + "_div");
	var defaultProps = new Array("AddNewBoxVisible", "AddNewBoxView", "AllowAddNew", "AllowColSizing", "AllowDelete", "AllowSort",
					"ItemClass", "AltClass", "AllowUpdate", "CellClickAction", "EditCellClass", "Expandable", "FooterClass",
					"GroupByRowClass", "GroupCount", "HeaderClass", "HeaderClickAction", "Indentation", "NullText",
					"ExpAreaClass", "RowLabelClass", "SelGroupByRowClass", "SelHeadClass", "SelCellClass", "RowSizing",
					"SelectTypeCell", "SelectTypeColumn", "SelectTypeRow", "ShowBandLabels", "ViewType", "AllowPaging",
					"PageCount", "CurrentPageIndex", "PageSize", "CollapseImage", "ExpandImage", "CurrentRowImage",
					"CurrentEditRowImage", "NewRowImage", "BlankImage", "SortAscImg", "SortDscImg", "Activation",
					"cultureInfo", "RowSelectors", "UniqueID", "StationaryMargins", "LoadOnDemand", "RowLabelBlankImage",
					"EIRM", "TabDirection", "ClientID", "DefaultCentury", "UseFixedHeaders", "FixedHeaderIndicator",
					"FixedHeaderOnImage", "FixedHeaderOffImage",
	
					"StopperStyle",
					"FixedColumnScrollType", "TableLayout", "AllowRowNumbering",
					"ClientSideRenumbering"
					, "XmlLoadOnDemandType", "Section508Compliant", "_rowToolTipFormatStr", "_childRowToolTipFormatStr"
					, "FilterDropDownStyle", "FilterHighlightRowStyle", "FilterDefaultImage", "FilterAppliedImage", "ImageDirectory", "_progressIndicatorImage", "EnableProgressIndicator"
					, "CellTitleMode", "HeaderTitleMode", "SortAscAltText", "SortDescAltText", "ExpandAltText", "CollapseAltText", "_currentRowAltText", "_currentEditRowAltText", "_fixedHeaderOnAltText", "_fixedHeaderOffAltText", "_newRowAltText"
					, "GridCornerImage", "UrlExecutionPath"
					, "ActivationObjectClassTB", "ActivationObjectClassL", "ActivationObjectClassR"
					, "FilterButtonImages", "FilterRowStyle", "FilterOperandDropDownStyle", "FilterOperandItemStyle", "FilterOperandItemHoverStyle", "FilterOperandButtonStyle"
					, "SortingAlgorithm"
					);
	this.Bands = new Array();
	var props;
	props = gridInitArray;
	if (props)
	{
		for (var i = 0; i < defaultProps.length; i++)
			this[defaultProps[i]] = props[i];
		this.Activation = new igtbl_initActivation(this.Activation);
		this.Activation._cssClass = this.ActivationObjectClassTB;
		this.Activation._cssClassL = this.ActivationObjectClassL;
		this.Activation._cssClassR = this.ActivationObjectClassR;
		this.cultureInfo = this.cultureInfo.split("|");
	}
	if (this.UseFixedHeaders
	|| this.XmlLoadOnDemandType != 0
	&& this.XmlLoadOnDemandType != 4
	)
	{
		this._scrElem = this.Element.parentNode.previousSibling;
		this._tdContainer = this._scrElem.parentNode.parentNode;
	}
	else
		this._tdContainer = this.Element.parentNode.parentNode;
	var xmlProps = xmlInitProps;
	this._AddnlProps = xmlProps;
	this.RowsServerLength = xmlProps[0];
	this.RowsRange = xmlProps[1];
	this.RowsRetrieved = xmlProps[2];
	
	if (this.XmlLoadOnDemandType == 2 && this.RowsRetrieved > this.RowsRange)
		this._recordChange("RowToStart", this, this.RowsRetrieved - this.RowsRange);
	if (!node)
	{
		var bandsArray = bandsInitArray;
		var bandCount = bandsArray.length;
		for (var i = 0; i < bandCount; i++)
			this.Bands[i] = new igtbl_Band(this, null, i
				, bandsInitArray, colsInitArray
			);
	}
	else
	{
		this.Bands.Node = this.Xml.selectSingleNode("UltraWebGrid/Bands");
		var bandNodes = this.Bands.Node.selectNodes("Band");
		for (var i = 0; i < bandNodes.length; i++)
			this.Bands[i] = new igtbl_Band(this, bandNodes[i], i
				, bandsInitArray, colsInitArray
			);
	}
	igtbl_dispose(defaultProps);

	igtbl_gridState[this.Id] = this;

	this.Events = new igtbl_Events(this
		, eventsInitArray
	);
	this.Rows = new igtbl_Rows((this.Node ? this.Xml.selectSingleNode("UltraWebGrid/Rs") : null), this.Bands[0], null);
	
	this.Rows._getRowToStart = function ()
	{
		if (this.Grid.XmlLoadOnDemandType == 2)
		{
			
			var topRowNo = Math.round(this.Grid.getDivElement().scrollTop / this.Grid.getDefaultRowHeight());
			return topRowNo;
		}
		return 0;
	};

	
	
	if (this.Bands && !this.Bands[0].IsGrouped && this.StationaryMargins != 1 && this.StationaryMargins != 3)
	{
		igtbl_assignColumnElements(this.Rows.Element.previousSibling, this.Bands[0]);
	}
	this.regExp = null;
	this.backwardSearch = false;
	this.lastSearchedCell = null;
	this.lastSortedColumn = "";
	if (this.AllowRowNumbering == 2) this.CurrentRowNumber = 0;
	this.GroupByBox = new igtbl_initGroupByBox(this);
	this.eReqType = igtbl_reqType;
	this.eReadyState = igtbl_readyState;
	this.eError = igtbl_error;
	
	
	
	this.eFilterComparisionOperator = igtbl_filterComparisionOperator;
	this.eRowFilterMode = igtbl_RowFilterMode;
	this.eFeatureRowView = igtbl_featureRowView;
	this.eFilterComparisonType = igtbl_filterComparisonType;
	this.eClipboardOperation = igtbl_ClipboardOperation;
	this.eClipboardError = igtbl_ClipboardError;
	if (this.Node || !ig_csom.IsIE && this.LoadOnDemand == 3)
	{
		this.ReqType = this.eReqType.None;
		this.ReadyState = this.eReadyState.Ready;
		this.Error = this.eError.Ok;

		this._innerObj = document.createElement("div");

		
		

		this.QueryString = "";
		this.XslProcessor = new igtbl_XSLTProcessor(this._AddnlProps[11]);
		if (ig_csom.IsIE)
			this.XmlResp = ig_createActiveXFromProgIDs(["MSXML2.DOMDocument", "Microsoft.XMLDOM"]);
		else
			this.DOMParser = new DOMParser();
		if (node)
			this.Rows.render();
	}
	
	if (xmlProps[14])
	{
		_igtbl_processServerPassedColumnFilters(xmlProps[14], this)
	}
	this._progressIndicator = new ig_progressIndicator(this.ImageDirectory + this._progressIndicatorImage, this.MainGrid);
	this._progressIndicator.setLocation(ig_Location.MiddleCenter);
	if (this.Bands[0].ColHeadersVisible != 2 && (this.StationaryMargins == 1 || this.StationaryMargins == 3)
		&& igtbl_getElementById(this.Id + "_hdiv")
	)
		this.StatHeader = new igtbl_initStatHeader(this);
	if (this.Bands[0].ColFootersVisible == 1 && (this.StationaryMargins == 2 || this.StationaryMargins == 3)
		&& igtbl_getElementById(this.Id + "_fdiv")
	)
		this.StatFooter = new igtbl_initStatFooter(this);
	this._calculateStationaryHeader();
	this.VirtualScrollDelay = 500;
	if (this.XmlLoadOnDemandType == 3)
		window.setTimeout("_igtbl_getMoreRows('" + this.Id + "');", 100);

	
	if (typeof (igtbl_oldGlobalMouseMove) == "undefined")
	{
		igtbl_oldGlobalMouseMove = igtbl_addEventListener(document.body, "mousemove", igtbl_globalMouseMove);
	}

	var thisForm = igtbl_getThisForm(this.Element);
	if (thisForm)
	{
		this._thisForm = thisForm;
		
		if (thisForm.igtblGrid && thisForm.igtblGrid.Id != this.Id)
			this.oldIgtblGrid = thisForm.igtblGrid;
		else
		{
			if (thisForm.addEventListener
			
				&& !igtbl_isAtlas
			)
			{
				thisForm.addEventListener('submit', igtbl_submit, false);
			}
			else if (typeof (thisForm.igtbl_oldOnSubmit) == "undefined" || thisForm.igtbl_oldOnSubmit == null)
			{
				thisForm.igtbl_oldOnSubmit = thisForm.onsubmit;
				thisForm.onsubmit = igtbl_submit;
			}
			
			if (typeof (MSOLayout_RemoveWebPart) == "undefined" && (typeof (thisForm.igtbl_oldSubmit) == "undefined" || thisForm.igtbl_oldSubmit == null))
			{
				thisForm.igtbl_oldSubmit = thisForm.submit;
				
				
				thisForm.submit = igtbl_formSubmit;
			}
			if (typeof (window._igtbl_doPostBackOld) == "undefined" || window._igtbl_doPostBackOld == null)
			{
				window._igtbl_doPostBackOld = window.__doPostBack;
				window.__doPostBack = igtbl_submit;
			}
			window._igtbl_thisForm = thisForm;
		}
		thisForm.igtblGrid = this;
	}
	this.SortImplementation = null;

	
	

	
	this._initFF();
},

"_initFF",
function ()
{
	if (!ig_csom.IsFireFox || this._initedFF)
		return;
	var height = 0;
	// gbr
	// _pager
	// _mtb
	// _hdiv
	var mr = document.getElementById(this.Id + "_mr");
	var mc = document.getElementById(this.Id + "_mc");
	var gbr = document.getElementById(this.Id + "_gbr");
	var hdiv = document.getElementById(this.Id + "_hdiv");
	if (gbr) height += gbr.offsetHeight;
	if (hdiv) height += hdiv.offsetHeight;
	
	if (height == 0)
		return;
	var gridMain = document.getElementById(this.Id + "_mtb");
	if (gridMain)
	{
		for (i = 0; i < gridMain.childNodes.length; i++)
		{
			var e = gridMain.childNodes[i];
			if (e.childNodes[0] && e.childNodes[0].id == this.Id + "_pager")
				height += e.childNodes[0].offsetHeight;
		}
	}
	var gridHeighInPX = this.MainGrid.style.height.indexOf("%") == -1;
	
	if (mr && mc && gridHeighInPX)
	{
		mr.style.height = (mr.offsetHeight - height) + 'px';
		mc.style.height = (mc.offsetHeight - height) + 'px';
	}
	this._initedFF = true;
},

"_getZ",
function (z, more)
{
	var elem = this.MainGrid;
	while (elem)
	{
		if (elem.nodeName == 'BODY' || elem.nodeName == 'FORM')
			break;
		var zi = ig_shared.getStyleValue(null, 'zIndex', elem);
		if (zi && zi.substring)
			zi = (zi.length > 3 && zi.charCodeAt(0) < 58) ? parseInt(zi) : 0;
		if (zi && zi >= z)
			z = zi + 1 + (more ? 1 : 0);
		elem = elem.parentNode;
	}
	return z;
},
"sortColumn",
function (colId, shiftKey)
{
	var bandNo = igtbl_bandNoFromColId(colId);
	var band = this.Bands[bandNo];
	var colNo = igtbl_colNoFromColId(colId);
	if (band.Columns[colNo].SortIndicator == 3)
		return;
	var headClk = igtbl_getHeaderClickAction(this.Id, bandNo, colNo);
	if (headClk == 2 || headClk == 3)
	{
		var gs = igtbl_getGridById(this.Id);
		if (!band.ClientSortEnabled)
			gs.NeedPostBack = true;
		var eventCanceled = igtbl_fireEvent(this.Id, this.Events.BeforeSortColumn, "(\"" + this.Id + "\",\"" + colId + "\")");
		if (eventCanceled && band.ClientSortEnabled)
			return;
		if (!eventCanceled)
			this.addSortColumn(colId, (headClk == 2 || !shiftKey));
		else
			gs.NeedPostBack = false;
		if (!eventCanceled && band.ClientSortEnabled)
		{
			var el = igtbl_getDocumentElement(colId);
			
			if (!el.length && el.tagName == "TH" && el.getAttribute("groupInfo"))
				igtbl_sortGroupedRows(this.Rows, bandNo, colId);
			else
			{
				if (!el.length)
				{
					el = new Array();
					el[0] = igtbl_getElementById(colId);
				}
				for (var i = 0; i < el.length; i++)
				{
					var rows = el[i].parentNode;
					
					while (rows && (rows.tagName != "TABLE" || (rows.tagName == "TABLE" && rows.id == ""))) rows = rows.parentNode;

					if (rows && rows.tBodies[0]) rows = rows.tBodies[0];
					if (!rows || !rows.Object) continue;
					rows.Object.sort();
				}
			}
			gs._recalcRowNumbers();
			igtbl_hideEdit(this.Id);
			igtbl_fireEvent(this.Id, this.Events.AfterSortColumn, "(\"" + this.Id + "\",\"" + colId + "\")");
		}
	}
},
"addSortColumn",
function (colId, clear)
{
	var colAr = colId.split(";");
	if (colAr.length > 1)
	{
		for (var i = 0; i < colAr.length; i++)
			if (colAr[i] !== "")
			{
				var band = this.Bands[igtbl_bandNoFromColId(colAr[i])];
				band.SortedColumns[band.SortedColumns.length] = colAr[i];
				
				var colObj = igtbl_getColumnById(colAr[i]);
				var colNo = igtbl_colNoFromColId(colAr[i]);
				var bandNo = band.Index;
				if (colObj.IsGroupBy)
				{
					var postString = "group:" + bandNo + ":" + colNo + ":false:band:" + bandNo;
					this._recordChange("ColumnGroup", band.Columns[colNo], postString);
					colObj._Changes["ColumnGroup"].setFireEvent(false); 
				}
				else
					this._recordChange("SortedColumns", band.Columns[colNo], "false" + ":" + band.Columns[colNo].SortIndicator);
			}
	}
	else
	{
		var band = this.Bands[igtbl_bandNoFromColId(colId)];
		var colNo = igtbl_colNoFromColId(colId);
		if (band.Columns[colNo].SortIndicator == 3)
			return;
		if (clear)
		{
			var scLen = band.SortedColumns.length;
			for (var i = scLen - 1; i >= 0; i--)
			{
				var cn = igtbl_colNoFromColId(band.SortedColumns[i]);
				if (cn != colNo && band.Columns[cn].SortIndicator != 3 && !band.Columns[cn].IsGroupBy)
				{
					band.Columns[cn].SortIndicator = 0;
					if (band.ClientSortEnabled)
					{
						var colEl = igtbl_getDocumentElement(band.SortedColumns[i]);
						
						if (colEl)
						{
							if (!colEl.length)
								colEl = [colEl];
							for (var j = 0; j < colEl.length; j++)
							{
								var img = null;
								var el = colEl[j];
								
								for (var x = 0; x < el.childNodes.length; x++)
								{
									if (el.childNodes[x].tagName == "NOBR")
									{
										el = el.childNodes[x];
										break;
									}
								}

								
								
								
								if (el.childNodes.length)
								{
									
									img = _igtbl_findSortImage(el.childNodes);
									
								}
								if (img)
									el.removeChild(img);
							}
						}
					}
				}
				if (band.Columns[cn].IsGroupBy)
					break;
				band.SortedColumns = band.SortedColumns.slice(0, -1);
				
				if (this.LoadOnDemand == 3 && this._containsChange("SortedColumns", band.Columns[cn]))
					this._removeChange("SortedColumns", band.Columns[cn]);
				
			}
		}
		if (band.Columns[colNo].SortIndicator == 1)
			band.Columns[colNo].SortIndicator = 2;
		else
			band.Columns[colNo].SortIndicator = 1;
		
		if (this.LoadOnDemand == 3 && this._containsChange("SortedColumns", band.Columns[colNo]))
		{
			this._removeChange("SortedColumns", band.Columns[colNo]);
		}
		this._recordChange("SortedColumns", band.Columns[colNo], clear.toString() + ":" + band.Columns[colNo].SortIndicator);
		band.Grid.lastSortedColumn = colId;
		if (band.ClientSortEnabled)
		{
			var colEl = igtbl_getDocumentElement(colId);
			
			if (colEl)
			{
				if (!colEl.length)
					colEl = [colEl];
				for (var i = 0; i < colEl.length; i++)
				{
					var img = null;
					var el = colEl[i];
					if (el.firstChild && el.firstChild.tagName == "NOBR")
						el = el.firstChild;
					
					
					if (el.childNodes.length)
					{


						
						img = _igtbl_findSortImage(el.childNodes);
						
					}
					
					if (img === null)
					{
						img = document.createElement("img");
						img.border = "0";
						img.height = "12";
						img.width = "12";
						img.setAttribute("imgType", "sort");
						if (!el.getAttribute("charApnd"))
						{
							el.innerHTML += "&nbsp;";
							el.setAttribute("charApnd", 1);
						}
						img.alt = this.SortDescAltText;
						img.setAttribute("igAltS", this.SortAscAltText);
						el.appendChild(img);
					}
					
					var alt = img.getAttribute("alt");
					if (band.Columns[colNo].SortIndicator == 1)
					{
						img.src = this.SortAscImg;
						if (alt != null)
						{
							var clpsAlt = img.getAttribute("igAltS");
							var clpsAlt2 = img.getAttribute("igAltUp")
							if (clpsAlt != null)
							{
								img.setAttribute("igAltD", alt);
								img.setAttribute("alt", clpsAlt);
								img.removeAttribute("igAltS");
							}
							else if (clpsAlt2 != null)
							{
								img.setAttribute("igAltDn", alt);
								img.setAttribute("alt", clpsAlt2);
								img.removeAttribute("igAltUp");
							}

						}
					}
					else
					{
						img.src = this.SortDscImg;
						if (alt != null)
						{
							var clpsAlt = img.getAttribute("igAltD");
							var clpsAlt2 = img.getAttribute("igAltDn")
							if (clpsAlt != null)
							{
								img.setAttribute("igAltS", alt);
								img.setAttribute("alt", clpsAlt);
								img.removeAttribute("igAltD");
							}
							else if (clpsAlt2 != null)
							{
								img.setAttribute("igAltUp", alt);
								img.setAttribute("alt", clpsAlt2);
								img.removeAttribute("igAltDn");
							}
						}
					}
				}
			}
		}
		if (!band.Columns[colNo].IsGroupBy)
		{
			for (var i = 0; i < band.SortedColumns.length; i++)
				if (band.SortedColumns[i] == colId)
					break;
			if (i == band.SortedColumns.length)
			{
				band.Columns[colNo].ensureWebCombo();
				band.SortedColumns[band.SortedColumns.length] = colId;
			}
		}
	}
},
"getActiveCell",
function ()
{
	return this.oActiveCell;
},
"setActiveCell",
function (cell, force)
{
	if (!this.Activation.AllowActivation || this._insideSetActive)
		return;
	if (!cell || !cell.Element || cell.Element.tagName != "TD")
		cell = null;
	if (!force && (cell && this.oActiveCell == cell || this._exitEditCancel))
	{
		this._noCellChange = true;
		return;
	}
	if (!cell)
	{
		this.ActiveCell = "";
		this.ActiveRow = "";
		var row = this.oActiveRow;
		cell = this.oActiveCell;
		if (cell)
			row = cell.Row;
		if (row)
			row.setSelectedRowImg(true);
		if (cell)
			cell.renderActive(false);
		if (this.oActiveRow)
			this.oActiveRow.renderActive(false);
		this.oActiveCell = null;
		this.oActiveRow = null;
		if (cell)
			this._removeChange("ActiveCell", cell);
		if (row)
			this._removeChange("ActiveRow", row);
		if (this.AddNewBoxVisible)
			this.updateAddNewBox();
		return;
	}
	var change = true;
	var oldACell = this.oActiveCell;
	var oldARow = this.oActiveRow;
	if (!oldARow && oldACell)
		oldARow = oldACell.Row;
	this.endEdit();

	if (this._exitEditCancel || this.fireEvent(this.Events.BeforeCellChange, [this.Id, cell.Element.id]) == true)
		change = false;
	if (change && cell.Row != oldARow)
	{
		if (oldARow)
		{
			change &= this.fireEvent(this.Events.BeforeRowDeactivate, [this.Id, oldARow.Element.id]) != true;
			if (oldARow.IsAddNewRow)
				oldARow.commit();
			else
				oldARow.processUpdateRow();
		}
		if (this._exitEditCancel || this.fireEvent(this.Events.BeforeRowActivate, [this.Id, cell.Row.Element.id]) == true)
			change = false;
	}
	if (!change)
	{
		this._noCellChange = true;
		return;
	}
	this._noCellChange = false;
	if (this.oActiveCell)
		this.oActiveCell.renderActive(false);
	if (this.oActiveRow)
		this.oActiveRow.renderActive(false);
	this.oActiveCell = cell;
	this.ActiveCell = cell.Element.id;
	if (this.oActiveRow)
		this._removeChange("ActiveRow", this.oActiveRow);
	this.oActiveRow = null;
	this.ActiveRow = "";
	this.oActiveCell.renderActive();
	if (this.oActiveCell.Row != oldARow)
		this.setNewRowImg(null);
	this.oActiveCell.Row.setSelectedRowImg();
	this.colButtonMouseOut();
	if (this.AddNewBoxVisible)
		this.updateAddNewBox();
	
	
	igtbl_activate(this.Id);
	this._removeChange("ActiveCell", oldACell);
	this._recordChange("ActiveCell", this.oActiveCell);

	this.fireEvent(this.Events.CellChange, [this.Id, this.oActiveCell.Element.id]);
	
	if (this.oActiveCell && this.oActiveCell.Row != oldARow)
		this.fireEvent(this.Events.AfterRowActivate, [this.Id, this.oActiveCell.Row.Element.id]);

	
	if (cell.Row.IsFilterRow)
		this.NeedPostBack = false;
},
"getActiveRow",
function ()
{
	if (this.oActiveRow != null)
		return this.oActiveRow;
	if (this.oActiveCell != null)
		return this.oActiveCell.Row;
	return null;
},
"setActiveRow",
function (row, force, fireEvents)
{
	if (!this.Activation.AllowActivation || this._insideSetActive)
		return;
	if (typeof (fireEvents) == "undefined")
		fireEvents = true;
	if (!row || !row.Element || row.Element.tagName != "TR")
		row = null;
	if (!force && (row && this.oActiveRow == row || this._exitEditCancel))
	{
		this._noCellChange = true;
		return;
	}
	if (!row)
	{
		this.ActiveCell = "";
		this.ActiveRow = "";
		row = this.oActiveRow;
		var cell = this.oActiveCell;
		if (cell)
			row = cell.Row;
		if (row)
			row.setSelectedRowImg(true);
		if (cell)
			cell.renderActive(false);
		if (this.oActiveRow)
			this.oActiveRow.renderActive(false);
		this.oActiveCell = null;
		this.oActiveRow = null;

		if (cell)
			this._removeChange("ActiveCell", cell);
		this._removeChange("ActiveRow", row);

		
		if (this._fromServerActiveRow)
			this._recordChange("ActiveRow", this, -1);

		if (this.AddNewBoxVisible)
			this.updateAddNewBox();
		return;
	}
	var change = true;
	var oldACell = this.oActiveCell;
	var oldARow = this.oActiveRow;
	if (!oldARow && oldACell)
		oldARow = oldACell.Row;
	this.endEdit();

	if (fireEvents && row != oldARow && oldARow)
	{
		change &= this.fireEvent(this.Events.BeforeRowDeactivate, [this.Id, oldARow.Element.id]) != true;
		if (oldARow.IsAddNewRow)
			oldARow.commit();
		else
			oldARow.processUpdateRow();
	}

	if (this._exitEditCancel || fireEvents && this.fireEvent(this.Events.BeforeRowActivate, [this.Id, row.Element.id]) == true)
		change = false;
	var cellChanged = this.oActiveCell != null;
	if (change && cellChanged)
		change = !this.fireEvent(this.Events.BeforeCellChange, [this.Id, this.oActiveCell.Element.id]);
	if (!change)
	{
		this._noCellChange = true;
		return;
	}
	this._noCellChange = false;
	if (this.oActiveCell)
		this.oActiveCell.renderActive(false);
	if (this.oActiveRow)
		this.oActiveRow.renderActive(false);
	this.oActiveRow = row;
	this.ActiveRow = row.Element.id;
	if (cellChanged)
		this._removeChange("ActiveCell", this.oActiveCell);
	this.oActiveCell = null;
	this.ActiveCell = "";
	this.oActiveRow.renderActive();
	this.oActiveRow.setSelectedRowImg();
	this.colButtonMouseOut();
	if (this.AddNewBoxVisible)
		this.updateAddNewBox();
	
	
	
	var mouseDownStatus = this._mouseDown;
	igtbl_activate(this.Id);
	this._mouseDown = mouseDownStatus;
	igtbl_activate(this.Id);

	this._removeChange("ActiveRow", oldARow);
	this._recordChange("ActiveRow", this.oActiveRow);
	if (fireEvents)
	{
		if (cellChanged)
			this.fireEvent(this.Events.CellChange, [this.Id, ""]);
		var oldNPB = this.NeedPostBack;
		this.fireEvent(this.Events.AfterRowActivate, [this.Id, row.Element.id]);
		if (!oldNPB && this.NeedPostBack && oldARow == row)
			this.NeedPostBack = false;

		
		if (row.IsFilterRow)
			this.NeedPostBack = false;
	}


},
"deleteSelectedRows",
function ()
{
	igtbl_deleteSelRows(this.Id);
	igtbl_activate(this.Id);
	this._recalcRowNumbers();
},
"unloadGrid",
function ()
{
	if (this.Id)
		igtbl_unloadGrid(this.Id);
},
"dispose",
function ()
{
	igtbl_unloadGrid(this.Id, true);
},
"beginEditTemplate",
function ()
{
	var row = this.getActiveRow();
	if (row)
		row.editRow();
},
"endEditTemplate",
function (saveChanges)
{
	var row = this.getActiveRow();
	if (row)
		row.endEditRow(saveChanges);
},
"find",
function (re, back
, searchHiddenColumns
)
{
	var g = this;
	if (re)
		g.regExp = re;
	if (!g.regExp)
		return null;
	g.lastSearchedCell = null;
	if (back == true || back == false)
		g.backwardSearch = back;
	var row = null;
	if (!g.backwardSearch)
	{
		row = g.Rows.getRow(0);
		if (row && row.getHidden())
			row = row.getNextRow();
		while (row &&
			row.find(re, back, searchHiddenColumns)
			== null)
		{
			row = row.getNextTabRow(false, true);
		}
	}
	else
	{
		var rows = g.Rows;
		while (rows)
		{
			row = rows.getRow(rows.length - 1);
			if (row && row.getHidden())
				row = row.getPrevRow();
			if (row && row.Expandable)
				rows = row.Rows;
			else
			{
				if (!row)
					row = rows.ParentRow;
				rows = null;
			}
		}
		while (row &&
			row.find(re, back, searchHiddenColumns)
			== null)
			row = row.getNextTabRow(true, true);
	}
	return g.lastSearchedCell;
},
"findNext",
function (re, back
, searchHiddenColumns
)
{
	var g = this;
	if (!g.lastSearchedCell)
	{
		return this.find(re, back, searchHiddenColumns);
	}
	if (re)
		g.regExp = re;
	if (!g.regExp)
		return null;
	if (back == true || back == false)
		g.backwardSearch = back;
	var row = g.lastSearchedCell.Row;
	while (row &&
	row.findNext(re, back, searchHiddenColumns) == null
	)
		row = row.getNextTabRow(g.backwardSearch, true);
	return g.lastSearchedCell;
},

"alignStatMarginsScrollBar",
function (elem)
{
	var divElem = this.getDivElement();
	if (divElem.clientHeight < divElem.scrollHeight)
	{
		var scrollbarWidth = divElem.offsetWidth - divElem.clientWidth;

		if (elem.Element.parentNode.style.width == "100%")
		{
			
			
			if (igtbl_isXHTML && ig_shared.IsIE7Compat)
			{
				
				if (this.UseFixedHeaders)
					elem.Element.parentNode.parentNode.parentNode.style.paddingRight = (scrollbarWidth > 0) ? scrollbarWidth + "px" : "";
				else
					elem.Element.parentNode.parentNode.style.paddingRight = (scrollbarWidth > 0) ? scrollbarWidth + "px" : "";
			}
			else
			{
				
				elem.Element.parentNode.parentNode.style.paddingRight = (scrollbarWidth > 0) ? scrollbarWidth + "px" : "";
				
				if (elem.Element.parentNode.parentNode.style.width == "100%" && !ig_shared.IsIE7Compat)
					elem.Element.parentNode.parentNode.style.width = "";
			}
		}
	}
	else
	{
		if (elem.Element.parentNode.style.width == "100%")
		{
			
			
			
			
			elem.Element.parentNode.parentNode.style.paddingRight = "";
		}
	}
},
"alignStatMargins",
function ()
{
	
	if (this.MainGrid.offsetHeight == 0) return;

	if (this.UseFixedHeaders)
	{
		var hDiv = igtbl_getElementById(this.Id + "_hdiv");
		if (this.Bands.length == 1 && this.StatHeader && hDiv && hDiv.firstChild && hDiv.firstChild.tHead && hDiv.firstChild.tHead.rows && hDiv.firstChild.tHead.rows.length > 0)
		{
			
			var lastHead = hDiv.firstChild.tHead.rows[hDiv.firstChild.tHead.rows.length - 1];
			if (lastHead.lastChild && lastHead.lastChild.firstChild && lastHead.lastChild.firstChild.id == this.Id + "_drs")
			{
				var hDivScr = lastHead.lastChild.firstChild;
				var divEl = this.getDivElement();
				hDivScr.firstChild.style.left = -divEl.scrollLeft + "px";
			}
			
			if (ig_csom.IsIE)
			{
				this.alignStatMarginsScrollBar(this.StatHeader);
				if (this.StatFooter)
					this.alignStatMarginsScrollBar(this.StatFooter);
			}
		}
		else
		{
			if (this.StatHeader)
				this.StatHeader.ScrollTo(this.getDivElement().scrollLeft);
			if (this.StatFooter)
				this.StatFooter.ScrollTo(this.getDivElement().scrollLeft);
		}
	}
	else
	{
		
		var scrollLeft = this.getDivElement().scrollLeft;
		if (this.StatHeader)
		{
			
			
			var divContent = this.DivElement.firstChild;
			var percentageWidth = this.Element.style.width.indexOf("%") != -1; 
			if (!percentageWidth)
			{
				if (!this.DivElement.getAttribute("scrollDivWidth"))
				{
					var comWidth = this.DivElement.firstChild.offsetWidth;
					
					
					
					
					
					this.DivElement.firstChild.style.width = comWidth + "px"; 
				}
			}
			else
			{
				

				var testTbl = this.Element;
				
				var ignoreVisibility = false;
				if (this.Bands && this.Bands[0].IsGrouped && this.Rows)
				{
					
					if (this.StatHeader)
						testTbl = this.StatHeader.Element.parentNode;
					else
					{
						var row = this.Rows.getRow(0);
						
						if (row.GroupByRow && row.Rows)
						{
							var childRows = row.getChildRows();
							if (childRows && childRows.length && childRows.length > 0 && childRows[0] &&
								childRows[0].parentNode && childRows[0].parentNode.parentNode)
							{
								testTbl = childRows[0].parentNode.parentNode;
								ignoreVisibility = true;
							}
						}
					}
				}
				
				
				
				
				

				if (!igtbl_dom.table.hasPercentageColumns(testTbl, this.Bands[0].firstActiveCell, ignoreVisibility))
				{
					var comWidth = this.Element.offsetWidth;
					
					var indentation = 0;
					if (this.get("StationaryMarginsOutlookGroupBy") == "True")
						indentation += _igtbl_headerRowIndentation(this, this.Bands[0].SortedColumns);
					this.DivElement.firstChild.style.width = comWidth + indentation + "px"; 

					
					this.DivElement.firstChild.width = "";

					
					this.StatHeader.Element.parentNode.style.width = comWidth + "px";
					if (this.StatFooter)
						this.StatFooter.Element.parentNode.style.width = comWidth + "px";
				}
				else
					this.alignStatMarginsScrollBar(this.StatHeader);
			}
			this.StatHeader.ScrollTo(scrollLeft);
		}
		if (this.StatFooter)
		{
			this.StatFooter.ScrollTo(scrollLeft);
			
			if (!this.StatHeader || ig_csom.IsIE)
			{
				var percentageWidth = this.Element.style.width.indexOf("%") != -1;
				if (percentageWidth && igtbl_dom.table.hasPercentageColumns(this.Element, this.Bands[0].firstActiveCell))
					this.alignStatMarginsScrollBar(this.StatFooter);
			}
		}
		
		var mtb = igtbl_getElementById(this.Id + "_mtb");
		if (mtb && mtb.rows && mtb.rows.length == 2 && mtb.parentNode && !ig_shared.IsNetscape6)
		{
			var r1H = igtbl_getAbsBounds(mtb.rows[1]).h;
			
			var r2H = 0;
			var re = new RegExp("_gbr$", "");
			if (this.StatHeader || !mtb.rows[0].id.match(re))
			{
				r2H = igtbl_getAbsBounds(mtb.rows[0]).h;
			}
			igtbl_getElementById(this.Id + "_mc").style.height = (r1H - r2H) + "px";
		}
	}
},

"selectCellRegion",
function (startCell, endCell)
{
	var sCol = startCell.Column, eCol = endCell.Column;
	if (sCol.Index > eCol.Index)
	{
		var c = sCol;
		sCol = eCol;
		eCol = c;
	}
	var sRow = startCell.Row, sRowIndex = sRow.getIndex(), eRow = endCell.Row, eRowIndex = eRow.getIndex();
	if (sRowIndex > eRowIndex)
	{
		var c = sRow;
		sRow = eRow;
		eRow = c;
		var i = sRowIndex;
		sRowIndex = eRowIndex;
		eRowIndex = i;
	}
	var pc = sRow.OwnerCollection;
	var band = sCol.Band;
	
	
	var selArray = new Array();
	
	if (sRowIndex > -1)
		for (var i = sRowIndex; i <= eRowIndex; i++)
		{
			var row = pc.getRow(i);
			if (!row.getHidden())
				for (var j = sCol.Index; j <= eCol.Index; j++)
				{
					var col = band.Columns[j];
					if (col.getVisible())
					{
						var cell = row.getCellByColumn(col);
						if (cell && cell.Element)
							selArray[selArray.length] = cell.Element.id;
					}
				}
		}
	if (selArray.length > 0)
		igtbl_gSelectArray(this.Id, 0, selArray);
	delete selArray;
},
"selectRowRegion",
function (startRow, endRow)
{
	var sRowIndex = startRow.getIndex(), eRowIndex = endRow.getIndex();
	if (sRowIndex > eRowIndex)
	{
		var r = startRow;
		startRow = endRow;
		endRow = r;
		var i = sRowIndex;
		sRowIndex = eRowIndex;
		eRowIndex = i;
	}
	
	
	if ((startRow.isFixedTop && startRow.isFixedTop()) ||
		(startRow.isFixedBottom && startRow.isFixedBottom()) ||
		(endRow.isFixedTop && endRow.isFixedTop()) ||
		(endRow.isFixedBottom && endRow.isFixedBottom())
		) return;
	var pc = startRow.OwnerCollection;
	var selArray = new Array();
	for (var i = sRowIndex; i <= eRowIndex; i++)
	{
		var row = pc.getRow(i);
		if (row && !row.getHidden())
			selArray[selArray.length] = row.Element.id;
	}
	if (selArray.length > 0)
		igtbl_gSelectArray(this.Id, 1, selArray);
	delete selArray;
},
"selectColRegion",
function (startCol, endCol)
{
	if (startCol.Index > endCol.Index)
	{
		var c = startCol;
		startCol = endCol;
		endCol = c;
	}
	var band = startCol.Band;
	var selArray = new Array();
	for (var i = startCol.Index; i <= endCol.Index; i++)
	{
		var col = band.Columns[i];
		if (col.getVisible())
			selArray[selArray.length] = col.Id;
	}
	if (selArray.length > 0)
		igtbl_gSelectArray(this.Id, 2, selArray);
	delete selArray;
},
"startHourGlass",
function ()
{
	if (!igtbl_waitDiv)
	{
		igtbl_waitDiv = document.createElement("div");
		document.body.insertBefore(igtbl_waitDiv, document.body.firstChild);
		igtbl_waitDiv.style.zIndex = this._getZ(10000, 1);
		igtbl_waitDiv.style.position = "absolute";
		igtbl_waitDiv.style.left = 0;
		igtbl_waitDiv.style.top = 0;
		igtbl_waitDiv.style.backgroundColor = "transparent";
	}
	igtbl_waitDiv.style.display = "";
	igtbl_waitDiv.style.width = document.body.clientWidth;
	igtbl_waitDiv.style.height = document.body.clientHeight;
	igtbl_waitDiv.style.cursor = "wait";
	
	if (igtbl_wndOldCursor === null)
		igtbl_wndOldCursor = document.body.style.cursor;

	document.body.style.cursor = "wait";
},
"stopHourGlass",
function ()
{
	if (igtbl_waitDiv)
	{
		igtbl_waitDiv.style.cursor = "";
		igtbl_waitDiv.style.display = "none";
		document.body.style.cursor = igtbl_wndOldCursor;
		igtbl_wndOldCursor = null;
	}
},
"clearSelectionAll",
function ()
{
	igtbl_clearSelectionAll(this.Id);
},
//*** OBSOLETE ***
"alignGrid",
function () { },
"suspendUpdates",
function (suspend)
{
	if (suspend == false)
	{
		this.SuspendUpdates = false;
	}
	else
		this.SuspendUpdates = true;
},
//*** END OBSOLETE ***
"beginEdit",
function ()
{
	if (this.oActiveCell)
		this.oActiveCell.beginEdit();
},
"endEdit",

function (force)
{
	var ec = this._editorCurrent;
	if (!ec && this.oActiveCell)
	{
		ec = this.oActiveCell.Column.getEditorControl();
		if (ec && ec.Element)
			ec = ec.Element;
	}
	if (force)
		if (ec && ec.removeAttribute)
			ec.removeAttribute("noOnBlur");
	if (ec && ec.getAttribute && ec.getAttribute("noOnBlur"))
		return;
	igtbl_hideEdit(this.Id);
},
"fireEvent",
function (eventObj, args)
{
	if (!this.isLoaded()) return;
	var result = false;
	if (eventObj[0] !== "")
	{
		
		try
		{
			if (typeof (eval(eventObj[0])) != "function")
				throw "Event handler does not exist.";
		}
		catch (ex)
		{
			alert("There is a problem with the event handler method: '" + eventObj[0] + "'. Please check the method name's spelling.")
			return false;
		}
		
		result = eval(eventObj[0]).apply(this, args);
	}
	if (this.GridIsLoaded && result != true && eventObj[1] > 0 && !this.CancelPostBack)
		this.NeedPostBack = true;
	this.CancelPostBack = false;
	return result;
},
"setNewRowImg",
function (row)
{
	var gs = this;
	if (row)
		row.setSelectedRowImg(true);
	if (gs.newImg != null)
	{
		gs._lastSelectedRow = null;
		var imgObj;
		imgObj = document.createElement("img");
		imgObj.src = gs.BlankImage;
		imgObj.border = "0";
		imgObj.setAttribute("imgType", "blank");
		gs.newImg.parentNode.appendChild(imgObj);
		gs.newImg.parentNode.removeChild(gs.newImg);
		var oRow = igtbl_getRowById(imgObj.parentNode.parentNode.id);
		if (oRow)
			gs._recalcRowNumbers(oRow);
		gs.newImg = null;
	}
	if (!row || row.Band.getRowSelectors() == 2 || row.Band.AllowRowNumbering > 1)
		return;
	var imgObj;
	imgObj = document.createElement("img");
	imgObj.src = gs.NewRowImage;
	imgObj.border = "0";
	imgObj.setAttribute("imgType", "newRow");
	if (gs.Section508Compliant)
		imgObj.setAttribute("alt", gs._newRowAltText);
	var cell = row.Element.cells[row.Band.firstActiveCell - 1];
	cell.innerHTML = "";
	cell.appendChild(imgObj);
	gs.newImg = imgObj;
},
"colButtonMouseOut",
function ()
{
	igtbl_colButtonMouseOut(null, this.Id);
},
"sort",
function ()
{
	if (igtbl_sortGrid)
	{
		igtbl_sortGrid.apply(this);
		this._recordChange("Sort", this)
		this._recalcRowNumbers();
	}
},
"updateAddNewBox",
function ()
{
	igtbl_updateAddNewBox(this.Id);
},
"update",
function ()
{
	if (typeof (igtbl_hideEdit) != "undefined")
	{
		if (this._editorCurrent)
			this._editorCurrent.removeAttribute("noOnBlur");
		igtbl_hideEdit(this.Id);
	}
	
	var p = igtbl_getElementById(this.ClientID);
	if (!p) return;
	if (this.Element.parentNode)
	{
		if (this.Element.parentNode.scrollLeft)
			ig_ClientState.setPropertyValue(this.ViewState, "ScrollLeft", this.Element.parentNode.scrollLeft.toString());
		if (this.Element.parentNode.scrollTop)
			ig_ClientState.setPropertyValue(this.ViewState, "ScrollTop", this.Element.parentNode.scrollTop.toString());
	}
	p.value = ig_ClientState.getText(this.ViewState.parentNode);
},
"goToPage",
function (page)
{
	
	page = igtbl_parseInt(page);
	if (!this.isLoaded() || !this.AllowPaging || this.CurrentPageIndex == page || page < 1 || page > this.PageCount)
		return;
	if (!this.Node && !ig_csom.IsNetscape6 || this.LoadOnDemand != 3)
	{
		this._recordChange("PageChanged", this, page);
		igtbl_doPostBack(this.Id);
	}
	else
	{
		
		
		this.invokeXmlHttpRequest(this.eReqType.Page, this, page);
	}
},
"getRowByLevel",
function (level)
{
	if (typeof (level) == "string")
		level = level.split("_");
	var rows = this.Rows;
	
	var adj = 0;
	if (typeof (rows._getRowToStart) != "undefined")
		adj = rows._getRowToStart();
	for (var i = 0; i < level.length - 1; i++)
	{
		rows = rows.getRow(level[i] - adj).Rows;
		if (typeof (rows._getRowToStart) != "undefined")
			adj = rows._getRowToStart();
	}
	return rows.getRow(level[level.length - 1] - adj);
},
"xmlHttpRequest",
function (type, waitResponse)
{
	if (this.fireEvent(this.Events.BeforeXmlHttpRequest, [this.Id, type]) == true)
		return;
	var smartCallback = null;
	
	
	var serverContext = { QueryString: igtbl_escape(this.QueryString), requestType: "xml" };
	var clientContext = { requestType: "json" };

	
	

	smartCallback = new ig_SmartCallback(clientContext, serverContext, null, this.UniqueID, this, waitResponse)
	smartCallback.callbackFinished = igtbl_onReadyStateChange;
	smartCallback.Type = type;
	smartCallback.QueryString = this.QueryString;
	smartCallback.RowToQuery = this.RowToQuery;
	smartCallback.ReqType = type;
	this._displayPI();
	smartCallback.execute();
	
	
},
"_containsChange",
function (type, obj)
{	
	return obj && (obj._Changes[type] != null);
},
"_recordChange",
function (type, obj, value, inId)
{
	var stateChange = new igtbl_StateChange(type, this, obj, value);
	if (typeof (this[type]) != "undefined")
	{
		var id = obj ? (obj.Element ? obj.Element.id : obj.Id) : inId;
		if (typeof (value) != "undefined" && value != null)
			this[type][id] = value;
		else
			this[type][id] = inId ? stateChange : true;
	}
	return stateChange;
},
"_removeChange",
function (type, obj, lastOnly)
{
	var ch;
	if (obj && (ch = obj._Changes[type]))
	{
		if (ch.length)
		{
			if (lastOnly)
				ch[ch.length - 1].remove(lastOnly);
			else
			{
				for (var i = ch.length - 1; i >= 1; i--)
					ch[i].remove();
				obj._Changes[type].remove();
			}
		}
		else
			ch.remove(lastOnly);
		if (typeof (this[type]) != "undefined")
		{
			var id = obj.Element ? obj.Element.id : obj.Id;
			delete this[type][id];
		}
	}
},

"_removeFilterChange",
function (obj, parentRowId)
{
	if (obj.RowFilterMode == 2)
	{
		var parentRowIds = parentRowId.split("\x01");
		var ch;
		if (obj && (ch = obj._Changes["FilterColumn"]))
		{
			if (ch.length)
			{
				for (var i = ch.length - 1; i >= 1; i--)
				{
					
					var values = (ch[i].Node.getAttribute && ch[i].Node.getAttribute("Value")) ? ch[i].Node.getAttribute("Value").split("%01") : ch[i].Node.props;
					if (values[2] == parentRowIds[0] && values[3] == parentRowIds[1])
						ch[i].remove();
				}
			}
			else
			{
				
				var values = (ch.Node.getAttribute && ch.Node.getAttribute("Value")) ? ch.Node.getAttribute("Value").split("%01") : ch.Node.props;
				if (values[2] == parentRowIds[0] && values[3] == parentRowIds[1])
					ch.remove();
			}
		}
	}
	else
		this._removeChange("FilterColumn", obj);
},
"alignDivs",
function (scrollLeft, force)
{
	
	if (this.MainGrid.offsetHeight == 0) return;

	if (!this.UseFixedHeaders
	
		&& (this.XmlLoadOnDemandType == 0
		|| this.XmlLoadOnDemandType == 4
		)
	)
	{
		
		
		return;
	}
	var mainGrid = this.MainGrid;
	if (!mainGrid) return;
	var divs = this._scrElem;
	var divf = this.Element.parentNode;
	var isInit = false;
	this.Element.setAttribute("noOnResize", true);
	
	if (ig_csom.IsFireFox)
	{
		if (divs.style.height != divs.parentNode.style.height)
			divs.style.height = divs.parentNode.style.height;
	}
	
	if ((!divs.firstChild.style.width || divs.firstChild.style.width == "0px") && this.Element.offsetWidth)
	{
		
		var expandAreaWidth = (this.Bands.length > 1) ? this.Element.firstChild.firstChild.offsetWidth : 0;
		var tw = this.Element.offsetWidth;
		
		if (ig_shared.IsIE && this.GroupCount > 0 && this.StatHeader && this.StatHeader.Element.offsetWidth > tw)
		{
			tw = this.StatHeader.Element.offsetWidth;
			if (this.UseFixedHeaders)
			{
				var th = this.StatHeader.Element.firstChild.firstChild;
				while (th && (!th.firstChild || typeof (th.firstChild.tagName) == "undefined" || th.firstChild.tagName != "DIV" || th.firstChild.id != this.Id + "_drs"))
					th = th.nextSibling;
				if (th)
					tw = th.offsetLeft + th.firstChild.scrollWidth;
			}
			else
				tw += expandAreaWidth + (this.GroupCount == 1 ? this.Bands[0].getIndentation() : 0);
		}
		else
			tw += expandAreaWidth + (this.GroupCount == 1 ? this.Bands[0].getIndentation() : 0);
		divs.firstChild.style.width = tw.toString() + "px";
		if (!mainGrid.style.height)
			divs.style.overflowY = "hidden";
		isInit = true;
	}
	

	var calculatedScrollWidth = divs.getAttribute("scrollDivWidth");
	if (calculatedScrollWidth)
	{
		if (calculatedScrollWidth > divs.firstChild.offsetWidth)
		{
			divs.firstChild.style.width = calculatedScrollWidth + "px";
		}
	}

	if (!mainGrid.style.width)
		divs.style.width = mainGrid.clientWidth.toString() + "px";

	
	
	if (this.XmlLoadOnDemandType == 0 || this.XmlLoadOnDemandType == 4)
		divs.firstChild.style.height = this.Element.offsetHeight.toString() + "px";
	else
		this._setScrollDivHeight();

	if (!mainGrid.style.height)
	{
		divs.style.height = this.Element.offsetHeight.toString() + "px";
		if (divs.scrollHeight != divs.clientHeight)
		{
			
			var divsHeight = this.Element.offsetHeight + divs.scrollHeight - divs.clientHeight;
			if (divsHeight < 0) divsHeight = -divsHeight;
			divs.style.height = divsHeight.toString() + "px";
		}
		divs.parentNode.style.height = divs.offsetHeight.toString() + "px";
	}
	if (isInit)
	{
		if (!divs.style.width || divs.style.width.charAt(divs.style.width.length - 1) != "%")
			divs.setAttribute("oldW", divs.offsetWidth);
		if (!divs.style.height || divs.style.height.charAt(divs.style.height.length - 1) != "%")
			divs.setAttribute("oldH", divs.offsetHeight);
	}
	var relOffs = false;
	if (ig_csom.IsIE)
	{
		while (mainGrid && mainGrid.tagName != (igtbl_isXHTML ? "HTML" : "BODY") && !relOffs)
		{
			relOffs = mainGrid.style.position != "" && mainGrid.style.position != "static";
			if (!relOffs) mainGrid = mainGrid.parentNode;
		}
	}

	divf.style.left = (parseInt(divf.style.left, 10) + igtbl_getAbsolutePos2("Left", divs) - igtbl_getAbsolutePos2("Left", divf)).toString() + "px";
	divf.style.top = (parseInt(divf.style.top, 10) + igtbl_getAbsolutePos2("Top", divs) - igtbl_getAbsolutePos2("Top", divf)).toString() + "px";
	
	
	
	
	{
		divf.style.width = igtbl_clientWidth(divs).toString() + "px";
		divf.style.height = igtbl_clientHeight(divs).toString() + "px";
	}

	
	if (this.XmlLoadOnDemandType == 0 || this.XmlLoadOnDemandType == 4)
		divs.firstChild.style.height = this.Element.offsetHeight.toString() + "px";
	else
		this._setScrollDivHeight();

	
	if (divs && ig_shared.IsIE8 && divs.style.overflow == "auto")
	{
		divs.style.overflow = "hidden";
		divs.style.overflow = "auto";
	}

	if (divf.firstChild.style.left == "")
		divf.firstChild.style.left = "0px";
	if (divf.firstChild.style.top == "")
		divf.firstChild.style.top = "0px";
	if (!scrollLeft)
		scrollLeft = divs.scrollLeft;
	else
	{
		
		
		igtbl_scrollLeft(divs, scrollLeft);
		
	}
	var doHoriz = false;
	if (!this._oldScrollLeftAlign)
		this._oldScrollLeftAlign = 0;
	if (this._oldScrollLeftAlign != scrollLeft)
	{
		this._oldScrollLeftAlign = scrollLeft;
		doHoriz = true;
	}
	if (parseInt(divf.firstChild.style.top, 10) != -divs.scrollTop)
	{
		if (this.XmlLoadOnDemandType != 2)
		{
			if (ig_shared.IsIEStandards)
			{
				setTimeout("igtbl_getGridById(\"" + this.Id + "\").Element.parentNode.firstChild.style.top = \"-" + divs.scrollTop + "px\";");
			}
			else
				divf.firstChild.style.top = (-divs.scrollTop).toString() + "px";
		}
		if (this.StatHeader || this.StateFooter)
			doHoriz = true;
	}
	if (doHoriz || force)
	{
		if (this.UseFixedHeaders)
		{
			
			var rowDivs = igtbl_getDocumentElement(this.Id + "_drs");
			if (rowDivs)
			{
				
				if (!rowDivs.length)
					rowDivs = [rowDivs];
				
				for (var i = 0; i < rowDivs.length; i++)
					rowDivs[i].firstChild.style.left = (-scrollLeft).toString() + "px";
			}
		}
		else 
		{
			if (this.XmlLoadOnDemandType != 2)
				divf.firstChild.style.top = (-divs.scrollTop).toString() + "px";
			
			
			divf.firstChild.style.left = (-divs.scrollLeft).toString() + "px";

		}
	}
	if (isInit)
	{
		divf.style.left = (parseInt(divf.style.left, 10) + igtbl_getAbsolutePos2("Left", divs) - igtbl_getAbsolutePos2("Left", divf)).toString() + "px";
		divf.style.top = (parseInt(divf.style.top, 10) + igtbl_getAbsolutePos2("Top", divs) - igtbl_getAbsolutePos2("Top", divf)).toString() + "px";
		divf.style.width = igtbl_clientWidth(divs).toString() + "px";
		divf.style.height = igtbl_clientHeight(divs).toString() + "px";
	}
	
	
	this.Element.removeAttribute("noOnResize");
},


"_alignFilterRow",
function (rowsObj)
{
	var filterRow = rowsObj.FilterRow;
	if (filterRow)
	{
		var elem = filterRow.getCellElements();
		for (var i = 0; elem != null && elem.length && i < elem.length; i++)
		{

			
			var spanElm = null;
			for (var j = 0; j < elem[i].childNodes.length; j++)
			{
				if (elem[i].childNodes[j].tagName == "SPAN")
				{
					spanElm = elem[i].childNodes[j];
				}
			}
			if (spanElm != null)
			{

				
				spanElm.style.width = "0px";
				
				
				var tdWidth = elem[i].clientWidth - (ig_csom.IsFireFox30 ? 1 : 0);
				
				if (tdWidth - spanElm.offsetLeft > 0)
					spanElm.style.width = (tdWidth - spanElm.offsetLeft) + "px";
			}
		}
	}
	

},

"_setScrollDivHeight",
function ()
{
	var divs = this._scrElem;
	
	
	var estRowsHeight = this.RowsServerLength * (this.getDefaultRowHeight()); 
	if (!this.StatHeader && this.Bands[0].ColHeadersVisible == 1)
		estRowsHeight += this.getDefaultRowHeight();
	if (!this.StatFooter && this.Bands[0].ColFootersVisible == 1)
		estRowsHeight += this.getDefaultRowHeight();
	
	
	var height = (this.Rows.Element.parentNode.offsetHeight > estRowsHeight) ? this.Rows.Element.parentNode.offsetHeight : estRowsHeight;

	
	
	if (ig_csom.IsIE)
	{
		var mil = 1000000;
		var cDiv = 1;
		while (height > mil)
		{
			var div;
			if (divs.childNodes.length == cDiv)
			{
				div = document.createElement("DIV");
				divs.appendChild(div);
			}
			else
				div = divs.childNodes[cDiv];

			div.style.height = mil + "px";
			height -= mil;
			cDiv++;
		}
		while (divs.childNodes.length > cDiv)
			divs.removeChild(divs.lastChild);
	}
	divs.firstChild.style.height = height + "px";
},
"_recalcRowNumbers",
function (row)
{
	if (this.ClientSideRenumbering != 1) return;
	if (row && row.Band.AllowRowNumbering < 2 || !row && this.AllowRowNumbering < 2) return; 

	for (var i = 0; i < this.Bands.length; i++)
		this.Bands[i]._currentRowNumber = 0;

	if (!row)
		igtbl_RecalculateRowNumbers(this.Rows, 1, this.Bands[0], this.Rows.Node);
	else
		switch (row.Band.AllowRowNumbering)
	{
		case (2):
		case (4):
			igtbl_RecalculateRowNumbers(this.Rows, 1, this.Bands[0], this.Rows.Node);
			break;
		case (3):
			var rc = row.ParentRow ? row.ParentRow.Rows : this.Rows;
			igtbl_RecalculateRowNumbers(rc, 1, rc.Band, rc.Node);
			break;
	}
},
"_calculateStationaryHeader",
function ()
{
	
	var band = this.Bands[0];
	if (!band.IsGrouped && this.StatHeader && (this.StationaryMargins == 1 || this.StationaryMargins == 3))
	{
		var tr = this.StatHeader.Element.parentNode.parentNode.parentNode.parentNode;
		var oldTRDisplay = tr.style.display;
		var th = this.Element.childNodes[1];
		var i = 0;
		var drs = null;
		var row = th.firstChild;
		while (i < row.cells.length && (!row.cells[i].firstChild || row.cells[i].firstChild.id != this.Id + "_drs")) i++;
		if (i < row.cells.length)
		{
			var td = row.cells[i];
			drs = td.firstChild;
		}
		
		if (this.Rows && (this.Rows.length > 0
		 || (this.Rows.AddNewRow && !this.Rows.AddNewRow.isFixedBottom()
		
		 || this.Rows.FilterRow && !this.Rows.FilterRow.isFixedBottom()
		)))
		{
			
			var gridHeighInPX = this.MainGrid.style.height.indexOf("%") == -1;
			
			
			if (this.Rows.length == 1 && gridHeighInPX && th.offsetHeight > 1)
			{
				var mrElem = document.getElementById(this.Id + "_mr");
				var mcElem = document.getElementById(this.Id + "_mc");
				if (mrElem && mcElem && igtbl_parseInt(mrElem.style.height) && igtbl_parseInt(mcElem.style.height))
				{
					if (mrElem.style.height.substr(mrElem.style.height.length - 2) == "px" && mcElem.style.height.substr(mcElem.style.height.length - 2) == "px")
					{
						mrElem.style.height = (igtbl_parseInt(mrElem.style.height) - igtbl_parseInt(th.clientHeight)).toString() + "px";
						mcElem.style.height = (igtbl_parseInt(mcElem.style.height) - igtbl_parseInt(th.clientHeight)).toString() + "px";
					}
					else
					{
						mrElem.style.height = (mrElem.offsetHeight - igtbl_parseInt(th.offsetHeight)).toString() + "px";
						mcElem.style.height = (mcElem.offsetHeight - igtbl_parseInt(th.offsetHeight)).toString() + "px";
					}

				}
			}
			tr.style.display = "";
			
			var hdiv = tr.childNodes[0].childNodes[0];
			if (hdiv.style.height == "0pt") hdiv.style.height = "";
			
			
			
			
			
			if (!this._fixHeightOnce && igtbl_isXHTML && this.MainGrid && this.MainGrid.style.height)
			{
				
				this._fixHeightOnce = true;
				var height = this.MainGrid.style.height;
				if (height.substr(height.length - 2) == "px")
					this.MainGrid.style.height = (igtbl_parseInt(height) - th.offsetHeight).toString() + "px";
			}
			
			

			
			
			if (this.Rows.length == 0 && !(this.Rows.AddNewRow && !this.Rows.AddNewRow.isFixedBottom()
			|| this.Rows.FilterRow && !this.Rows.FilterRow.isFixedBottom()))
			{
				th.style.display = "";
				th.style.visibility = "hidden";
			}
			else
				th.style.display = "none";
			
			
			if (drs)
				drs.style.display = "none";
		}
		else
		{
			tr.style.display = "none";
			
			
			
			th.style.display = "";
			th.style.visibility = "";
			
			

			
			
			
			
			if (!this._fixHeightOnce && igtbl_isXHTML && this.MainGrid && this.MainGrid.style.height)
			{
				
				this._fixHeightOnce = true;
				var height = this.MainGrid.style.height;
				
				if (height.substr(height.length - 2) == "px" && !(this.Rows.length < 1 && ig_csom.IsFireFox))
					this.MainGrid.style.height = (igtbl_parseInt(height) + th.offsetHeight).toString() + "px";
			}
			
			var mrElem = document.getElementById(this.Id + "_mr");
			var mcElem = document.getElementById(this.Id + "_mc");
			if (ig_csom.IsFireFox && mrElem && mcElem && igtbl_parseInt(mrElem.style.height) && igtbl_parseInt(mcElem.style.height))
			{
				if (mrElem.style.height.substr(mrElem.style.height.length - 2) == "px" && mcElem.style.height.substr(mcElem.style.height.length - 2) == "px")
				{
					mrElem.style.height = (igtbl_parseInt(mrElem.style.height) + igtbl_parseInt(th.clientHeight)).toString() + "px";
					mcElem.style.height = (igtbl_parseInt(mcElem.style.height) + igtbl_parseInt(th.clientHeight)).toString() + "px";
				}
			}
			if (drs)
				drs.style.display = "";
		}
		
		if (oldTRDisplay != tr.style.display)
		{
			for (var i = 0; i < band.Columns.length; i++)
			{
				var cols = igtbl_getDocumentElement(band.Columns[i].Id);
				if (cols && cols.length == 2)
				{
					if (oldTRDisplay == "")
					{
						cols[1].innerHTML = cols[0].innerHTML;
						cols[0].innerHTML = "&nbsp;";
					}
					else
					{
						cols[0].innerHTML = cols[1].innerHTML;
						cols[1].innerHTML = "&nbsp;";
					}
				}
			}
		}
	}
},

"_getCurrentFiltersString",
function (col, band, parentRowId)
{
	if (!band)
	{
		if (col) band = col.Band;
		else band = this.Bands[0];
	}
	var currentFilters;
	if (((col && col.RowFilterMode == igtbl_RowFilterMode.AllRowsInBand) || band.Index == 0) && !band.IsGrouped)
	{
		currentFilters = band._filterPanels;
	}
	else
	{
		
		if (parentRowId && parentRowId.length > 0)
		{
			var tempId = "";
			if (band.IsGrouped)
			{
				tempId = parentRowId.replace("_gr", "_t");
			}
			else
			{
				tempId = parentRowId.replace("_r", "_t")
			}
			currentFilters = band._filterPanels[tempId];
		}
	}
	var currentFilterString = "";
	if (currentFilters)
	{
		var seperator = "\x05";
		for (var cf in currentFilters)
		{
			
			if (currentFilters[cf] && currentFilters[cf].getOperator && currentFilters[cf].IsActive())
			{
				
				var newfilter = false;
				if (col) newfilter = (cf == col.Id);
				var foundColumn = igtbl_getColumnById(cf);
				currentFilterString += foundColumn.getLevel(true) + seperator + currentFilters[cf].getOperator() + seperator + currentFilters[cf].getEvaluationValue() + seperator + newfilter + "\x03";
			}
		}
	}
	return currentFilterString;
},

"invokeXmlHttpRequest",
function (type, object, data, waitResponse)
{
	var g = this;
	if (!g.Node && !ig_csom.IsNetscape6 || g.LoadOnDemand != 3)
		return;
	
	switch (type)
	{
		case g.eReqType.FilterDropDownFill:
			{
				var rows = data;
				var col = object;
				
				var parentRowDataKey = "";
				var parentRowId = "";
				var oSqlWhere = null;
				if (rows)
				{
					if (rows.ParentRow && (rows.Band.Index > 0 || rows.Band.IsGrouped))
					{
						parentRowId = rows.ParentRow.Id;
						parentRowDataKey = rows.ParentRow.DataKey;
					}
					
					if (rows.ParentRow)
						oSqlWhere = rows.ParentRow._generateBandsSqlWhere(rows.ParentRow.Band);
				}
				var sqlWhere = "";
				var newLevel = "";
				if (oSqlWhere)
				{
					sqlWhere = oSqlWhere.sqlWhere ? oSqlWhere.sqlWhere : "";
					newLevel = oSqlWhere.newLevel ? oSqlWhere.newLevel : "";
				}
				g.QueryString = "FilterDropFill\x01" + col.getLevel(true) + "\x01" + parentRowDataKey + "\x01" + parentRowId + "\x01" + sqlWhere + "\x01" + newLevel;
				g.xmlHttpRequest(type);
				break;
			}
		case g.eReqType.Filter:
			{
				var rows = data;
				var col = object;
				var parentRowDataKey = "";
				var parentRowId = "";
				if (rows)
				{
					if (rows.Band.Index > 0 || rows.Band.IsGrouped)
					{
						parentRowId = rows.ParentRow.Id;
						parentRowDataKey = rows.ParentRow.DataKey;
					}
				}
				
				var currentFilterString = this._getCurrentFiltersString(col, col.Band, parentRowId);
				
				g.QueryString = "Filter\x01" + col.getLevel(true) + "\x01" + parentRowDataKey + "\x01" + parentRowId + "\x01" + currentFilterString + "\x01" + g._buildSortOrder(g);
				igtbl_scrollTop(g.getDivElement(), 0);
				g.xmlHttpRequest(type);

				break;
			}
		case g.eReqType.UpdateCell:
			{
				var cell = object;
				var row = cell.Row;
				if (g.LoadOnDemand == 3 && (typeof (g.Events.AfterRowUpdate) == "undefined" || g.Events.AfterRowUpdate[1] == 0 && (g.Events.XmlHTTPResponse[1] == 1 || g.Events.AfterCellUpdate[1] == 1)))
				{
					var cellInfo = row._generateUpdateRowSemaphore();
					g.QueryString = "UpdateCell\x01" + cell.Band.Index + "\x02" + cell.Column.Index + "\x02" + cell.Row.getIndex(true) + "\x02" + cell.Row.DataKey + "\x02" + data + "\x02" + cell.getLevel(true) + "\x02" + cell.getOldValue() + "\x02" + (cellInfo.length > 0 ? "CellValues\x06" + cellInfo : "");
					g.xmlHttpRequest(type);
				}
				break;
			}
		case g.eReqType.AddNewRow:
			{
				var rows = object;
				if ((typeof (g.Events.AfterRowUpdate) == "undefined" || g.Events.AfterRowUpdate[1] == 0 && g.Events.XmlHTTPResponse[1] == 1))
				{
					g.QueryString = "AddNewRow\x01" + rows.Band.Index + "\x02" + (rows.ParentRow ? rows.ParentRow.getIndex(true) + "\x02" + rows.ParentRow.DataKey : "\x02");
					g.xmlHttpRequest(type);
				}
				break;
			}
		case g.eReqType.Sort:
			{
				var rows = object;
				rows.sortXml();
				break;
			}
		case g.eReqType.ChildRows:
			{
				var row = object;
				row.requestChildRows();
				break;
			}
		case g.eReqType.DeleteRow:
			{
				if (g.LoadOnDemand == 3 && (!g.Events.XmlHTTPResponse || g.Events.XmlHTTPResponse[1] || g.Events.AfterRowDeleted[1]))
				{
					var row = object;
					var cellInfo = row._generateUpdateRowSemaphore(true);
					
					var dataKey = row._generateHierarchicalDataKey();
					
					g.QueryString = "DeleteRow\x01" + row.Band.Index + "\x02" + row.getIndex(true) + "\x02" + dataKey + "\x02" + row.getLevel(true) + "\x02" + row.DataKey + "\x02" + g.RowsRetrieved + "\x04" + (cellInfo.length > 0 ? "CellValues\x06" + cellInfo + "\x04" : "") + "Page" + "\x03" + (g.AllowPaging === true ? g.CurrentPageIndex : -1);
					
					g.QueryString += "\x04" + g._buildSortOrder();

					g.RowToQuery = row;
					g.xmlHttpRequest(type, waitResponse);
				}
				break;
			}
		case g.eReqType.UpdateRow:
			{
				var row = object;
				var cellInfo = "";
				if (row._dataChanged & 1)
				{
					g.QueryString = "AddNewRow\x06" + (row.ParentRow ? row.ParentRow.getLevel(true) + "\x02" + row.ParentRow.DataKey : "\x02") + (g.QueryString.length > 0 ? "\x04" : "") + g.QueryString;
					
					this.setNewRowImg(null);
				}
				else
					cellInfo = row._generateUpdateRowSemaphore();
				
				var dataKey = row._generateHierarchicalDataKey();
				
				g.QueryString = "UpdateRow\x01" + row._dataChanged + "\x02" + row.Band.Index + "\x02" + row.getLevel(true) + "\x02" + dataKey + "\x02" + g.RowsRetrieved + "\x02" + g.CurrentPageIndex + "\x04" + (cellInfo.length > 0 ? "CellValues\x06" + cellInfo + "\x04" : "") + g.QueryString;
				g.RowToQuery = row;
				g.xmlHttpRequest(type);
				break;
			}
		case g.eReqType.MoreRows:
			{
				
				if (this.AllowPaging || this.GroupCount > 0 || g.requestingMoreRows) return;
				g.requestingMoreRows = true;
				var de = g.getDivElement();
				de.setAttribute("oldST", de.scrollTop.toString());
				if (g.RowsServerLength > g.Rows.length)
				{
					g.QueryString = "NeedMoreRows\x01" + g.RowsRetrieved + "\x02" + g.Rows.length.toString();
					var sortOrder = "";
					sortOrder = g._buildSortOrder();
					
					g.QueryString += "\x02" + g.Bands[0].ColumnsOrder;
					g.QueryString += "\x02" + sortOrder;
					g.QueryString += "\x02" + this.Bands[0]._sqlWhere;

					var currentFilters = "";
					var bandFilters = g.Bands[0]._filterPanels;
					if (bandFilters)
					{
						for (var colId in bandFilters)
						{
							var filter = bandFilters[colId];
							
							if (filter.IsActive())
							{
								var col = igtbl_getColumnById(colId);
								currentFilters += col.getLevel(true) + "\x05" + filter.getOperator() + "\x05" + filter.getEvaluationValue() + "\x03";
								
								
							}
						}
					}
					g.QueryString += "\x02" + currentFilters;
					de.setAttribute("noOnScroll", "true");
					g.xmlHttpRequest(g.eReqType.MoreRows);
				}
				break;
			}
		case g.eReqType.Custom:
			{
				g.QueryString = "Custom\x01" + data;
				g.xmlHttpRequest(g.eReqType.Custom);
				break;
			}
		case g.eReqType.Page:
			{
				g.QueryString = "Page\x01" + data + "\x01" + g.CurrentPageIndex + "\x01" + g._buildSortOrder(g)
				+ "\x01" + g._getCurrentFiltersString();
				;
				
				g._pageToGo = data;
				g.xmlHttpRequest(g.eReqType.Page);
				break;
			}
		case g.eReqType.Scroll:
			{
				if (this.AllowPaging) return;
				var de = g.getDivElement();
				de.setAttribute("oldST", de.scrollTop.toString());

				
				var topRowNo = Math.round(de.scrollTop / g.getDefaultRowHeight());
				
				if (g.XmlLoadOnDemandType == 2)
				{
					g._recordChange("RowToStart", g, topRowNo);
				}
				g.QueryString = "NeedMoreRows\x01" + topRowNo.toString() + "\x02" + topRowNo.toString();
				var sortOrder = "";
				sortOrder = g._buildSortOrder();
				g.QueryString += "\x02" + g.Bands[0].ColumnsOrder;
				g.QueryString += "\x02" + sortOrder;
				g.QueryString += "\x02" + this.Bands[0]._sqlWhere;
				
				g.QueryString += "\x02" + g._getCurrentFiltersString();
				g.xmlHttpRequest(g.eReqType.Scroll);
				break;
			}
		case g.eReqType.Refresh:
			{
				var rows = g.Rows;
				if (object && object.Type)
				{
					if (object.Type == "rows")
						rows = object;
					else if (object.Type == "row")
						rows = object.Rows;
				}
				if (rows)
					rows.refresh();
				break;
			}
	}
},
"getDefaultRowHeight",
function ()
{
	var rh = igtbl_parseInt(this.Bands[0].DefaultRowHeight);
	if (!rh)
		rh = 22;
	if (igtbl_isXHTML)
	{
		rh += 2;
		
		var row = this.Rows.Element.rows[0];
		while (row && row.offsetHeight == 0)
			row = row.nextSibling;
		if (row)
			rh = row.offsetHeight;
	}
	return rh;
},
"_buildSortOrder",
function ()
{
	var sortOrder = "";
	for (var i = 0; i < this.Bands[0].SortedColumns.length; i++)
	{
		var col = igtbl_getColumnById(this.Bands[0].SortedColumns[i]);
		sortOrder += col.Key + (col.SortIndicator == 2 ? " DESC" : "") + (i < this.Bands[0].SortedColumns.length - 1 ? "," : "");
	}
	return sortOrder;
},
"_ensureValidParent",
function (obj)
{
	e = obj.Element;
	var pe = e ? e.parentNode : null;
	if (pe && pe.tagName != "FORM" && pe.tagName != (igtbl_isXHTML ? "HTML" : "BODY"))
		try
		{
			ig_csom._skipNew = true;
			npe = igtbl_getElementById(this.Id);
			if (npe)
				npe = npe.form;
			if (obj._relocate)
				obj._relocate(npe, window.document.body);
			else
			{
				pe.removeChild(e);
				if (npe)
					try
					{
						npe.appendChild(e);
					}
					catch (ex)
					{
						npe = null;
					}
				if (!npe)
					document.body.insertBefore(e, document.body.firstChild);
				e.style.zIndex = 9999;
			}
			ig_csom._skipNew = false;
		}
		catch (ex) { }
},
"getDivElement",
function ()
{
	var de = this.DivElement;
	if (this._scrElem)
		de = this._scrElem;
	return de;
},
"isDisabled",
function ()
{
	var result = false;
	if (this._thisForm && igtbl_isDisabled(this._thisForm) || igtbl_isDisabled(this.MainGrid))
		result = true;
	return result;
},
"isLoaded", 
function ()
{
	if (!this.GridIsLoaded)
		return false;
	return !this.isDisabled();
},

"resize",
function (width, height)
{
	
	width = (width < 0) ? 0 : width
	height = (height < 0) ? 0 : height

	
	if (!ig_csom.IsIE7Compat || ((ig_csom.IsIE6 || ig_csom.IsIE7) && igtbl_isXHTML))
	{
		// need to set the width first so we can see whether anything (groupby box) is going to 
		// get taller or shorter because of changes to text wrapping
		
		var marginWidth = igtbl_dom.dimensions.bordersWidth(this.MainGrid);
		this.MainGrid.style.width = width + "px";
		document.getElementById(this.Id + "_mc").style.width = (width - marginWidth) >= 0 ? width - marginWidth : 0 + "px";

		
		this.MainGrid.style.height = 0 + "px";
		
		this._fixHeightOnce = false;

		// measure how much space all the fixed elements are going to need 
		
		var marginHeight = igtbl_dom.dimensions.bordersHeight(this.MainGrid);
		for (var x = 0; x < this.MainGrid.rows.length; x++)
		{
			if (this.MainGrid.rows[x].id != this.Id + "_mr")
			{
				marginHeight += this.MainGrid.rows[x].offsetHeight;
			}
		}

		
		if (height < marginHeight)
		{
			height = marginHeight;
		}

		// set the new widths and heights of the outer table and rows area 
		this.MainGrid.style.height = height + "px";
		document.getElementById(this.Id + "_mr").style.height = (height - marginHeight) + "px";
		document.getElementById(this.Id + "_mc").style.height = (height - marginHeight) + "px";
	} else
	{
		this.MainGrid.style.width = width + "px";
		this.MainGrid.style.height = height + "px";
	}

	
	
	if (ig_csom.IsFireFox)
	{
		
		var mainTBody = document.getElementById(this.Id + "_mtb");
		if (mainTBody)
			mainTBody.style.height = height + "px";
		this.alignStatMargins();
		this.alignDivs(0, true);
	}
},

"hide",
function ()
{
	this.getDivElement().style.display = "none";
	this.MainGrid.style.display = "none";
},

"show",
function ()
{
	this.MainGrid.style.display = "";
	this.getDivElement().style.display = "none";
	this.getDivElement().style.display = "";
	this._initFF();
	if (this.alignDivs)
		this.alignDivs();
	if (this.alignStatMargins)
		this.alignStatMargins();
	if (this.StatHeader) 
	{
		_igtbl_headerOrFooterHeight(this.StatHeader.Element)
	}
	
	igtbl_browserWorkarounds.ieTabScrollBarAdjustment(this.Bands[0]);
}
, "onCBSubmit", 
function ()
{
	this.update();
},
"getProgressIndicator",
function ()
{
	return this._progressIndicator;
},
"_displayPI",
function ()
{
	if (!this.EnableProgressIndicator)
		return;
	if (!this._piIndex)
		this._piIndex = 0;
	this._piIndex++;
	document.body.setAttribute("noOnBodyResize", "true"); 
	window.setTimeout("document.body.removeAttribute(\"noOnBodyResize\")", 100);
	
	this._progressIndicator.display();
},
"_hidePI",
function ()
{
	if (!this.EnableProgressIndicator)
		return;
	if (this._piIndex)
		this._piIndex--;
	if (this._piIndex)
		return;
	document.body.setAttribute("noOnBodyResize", "true"); 
	window.setTimeout("document.body.removeAttribute(\"noOnBodyResize\")", 100);
	this._progressIndicator.hide();
}
, "_generateSelArray",
function ()
{
	var activeRow = null;
	var activeCell = this.getActiveCell();
	if (!activeCell)
	{
		activeRow = this.getActiveRow();
		if (activeRow)
			activeCell = activeRow.getCell(0);
	}
	if (!activeCell)
		return null;
	var rowColl = activeCell.Row.OwnerCollection;
	var clipArray = new Array();
	var assignCell = function (cell, rowColl, clipObject)
	{
		if (!cell) return clipObject;
		var colIndex = cell.Column.Index;
		var rowIndex = cell.Row.getIndex();
		if (rowColl != cell.Row.OwnerCollection)
			return clipObject;
		var clipArray = clipObject.clipArray;
		var l = clipObject.leftIndex, t = clipObject.topIndex, r = clipObject.rightIndex, b = clipObject.bottomIndex;
		if (clipArray.length == 0)
		{
			clipArray[0] = new Array();
			clipArray[0][0] = cell;
			l = r = colIndex;
			t = b = rowIndex;
		}
		else
		{
			if (t > rowIndex)
			{
				for (var i = 0; i < t - rowIndex; i++)
				{
					var insElem = new Array();
					for (var j = 0; j <= r - l; j++)
						insElem.push(null);
					clipArray.unshift(insElem);
				}
				t = rowIndex;
			}
			if (b < rowIndex)
			{
				for (var i = 0; i < rowIndex - b; i++)
				{
					var insElem = new Array();
					for (var j = 0; j <= r - l; j++)
						insElem.push(null);
					clipArray.push(insElem);
				}
				b = rowIndex;
			}
			if (l > colIndex)
			{
				for (var i = 0; i < clipArray.length; i++)
					for (var j = 0; j < l - colIndex; j++)
						clipArray[i].unshift(null);
				l = colIndex;
			}
			if (r < colIndex)
			{
				for (var i = 0; i < clipArray.length; i++)
					for (var j = 0; j < colIndex - r; j++)
						clipArray[i].push(null);
				r = colIndex;
			}
			clipArray[rowIndex - t][colIndex - l] = cell;
		}
		clipObject.leftIndex = l;
		clipObject.topIndex = t;
		clipObject.rightIndex = r;
		clipObject.bottomIndex = b;
		return clipObject;
	};
	var clipObject = { "clipArray": clipArray, "leftIndex": -1, "rightIndex": -1, "topIndex": -1, "bottomIndex": -1 };
	for (var cellID in this.SelectedCells)
	{
		var cell = igtbl_getCellById(cellID);
		clipObject = assignCell(cell, rowColl, clipObject);
	}
	for (var rowID in this.SelectedRows)
	{
		var row = igtbl_getRowById(rowID);
		if (row.OwnerCollection != rowColl)
			continue;
		for (var i = 0; i < row.cells.length; i++)
			clipObject = assignCell(row.getCell(i), rowColl, clipObject);
	}
	for (var colID in this.SelectedColumns)
	{
		var col = igtbl_getColumnById(colID);
		if (col.Band.Index != rowColl.Band.Index)
			continue;
		var cols = igtbl_getDocumentElement(colID);
		if (!cols.length) cols = [cols];
		for (var i = 0; i < cols.length; i++)
		{
			var cells = igtbl_enumColumnCells(this.Id, cols[i]);
			if (cells && cells.length)
			{
				for (var j = 0; j < cells.length; j++)
					clipObject = assignCell(igtbl_getCellByElement(cells[j]), rowColl, clipObject);
			}
		}
	}
	if (clipArray.length == 0 && (activeRow || activeCell))
	{
		clipArray[0] = new Array();
		if (activeCell)
			clipArray[0][0] = activeCell;
		else
			for (var i = 0; i < activeRow.cells.length; i++)
				clipArray[0][i] = activeRow.getCell(i);
	}
	return clipArray;
},
"_getSelectedCells",
function ()
{
	var selArray = new Array();
	for (var cellID in this.SelectedCells)
	{
		var cell = igtbl_getCellById(cellID);
		selArray.push(cell);
	}
	for (var rowID in this.SelectedRows)
	{
		var row = igtbl_getRowById(rowID);
		for (var i = 0; i < row.cells.length; i++)
			selArray.push(row.getCell(i));
	}
	for (var colID in this.SelectedColumns)
	{
		var col = igtbl_getColumnById(colID);
		var cols = igtbl_getDocumentElement(colID);
		if (!cols.length) cols = [cols];
		for (var i = 0; i < cols.length; i++)
		{
			var cells = igtbl_enumColumnCells(this.Id, cols[i]);
			if (cells && cells.length)
			{
				for (var j = 0; j < cells.length; j++)
					selArray.push(igtbl_getCellByElement(cells[j]));
			}
		}
	}
	if (selArray.length == 0)
	{
		var activeCell = this.getActiveCell();
		var activeRow = this.getActiveRow();
		if (activeCell)
			selArray.push(activeCell);
		else if (activeRow)
			for (var i = 0; i < activeRow.cells.length; i++)
				selArray.push(activeRow.getCell(i));
	}
	return selArray;
},
"copy",
function (copytext, cutting)
{
	if (typeof (cutting) == "undefined") cutting = false;
	var clipArray = true;
	var options = { "copyFormatted": false, "ignoreHiddenColumns": false };
	if (!copytext)
	{
		if (this.fireEvent(this.Events.BeforeClipboardOperation, [this.Id, (!cutting ? this.eClipboardOperation.Copy : this.eClipboardOperation.Cut), options]) === true)
			return false;
		copytext = "";
		clipArray = this._generateSelArray();
		for (var i = 0; clipArray && i < clipArray.length; i++)
		{
			var cLine = clipArray[i];
			for (var j = 0; j < cLine.length; j++)
			{
				var cell = cLine[j];
				if (cell == null || !options.ignoreHiddenColumns || !cell.Column.Hidden)
				{
					if (cell != null)
					{
						var v = cLine[j].getValue(options.copyFormatted);
						
						if (v !== null && v !== undefined)
							copytext += v.toString();
					}
					if (j < cLine.length - 1)
						copytext += '\t';
				}
			}
			copytext += "\r\n";
		}
	}
	if (!copytext)
	{
		
		var oError = new Object();
		oError.OperationType = cutting ? this.eClipboardOperation.Cut : this.eClipboardOperation.Copy;
		oError.Data = null;
		oError.Options = options;
		this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.NothingToCopy, oError]);
		return false;
	}
	var copyFailed = false;
	var excMessage;
	try
	{
		if (!igtbl_setClipboardData(copytext))
		{
			
			var oError = new Object();
			oError.OperationType = cutting ? this.eClipboardOperation.Cut : this.eClipboardOperation.Copy;
			oError.Data = copytext;
			oError.Options = options;
			this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.NotSupported, "", oError]);
			return false;
		}
	}
	catch (exc)
	{
		copyFailed = true;
		excMessage = exc;
	}
	if (copyFailed)
	{

		
		var oError = new Object();
		oError.Options = options;
		oError.OperationType = cutting ? this.eClipboardOperation.Cut : this.eClipboardOperation.Copy;
		oError.Data = copytext;
		this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.Failure, excMessage, oError]);
		return false;
		
	}
	if (!cutting)
		this.fireEvent(this.Events.AfterClipboardOperation, [this.Id, this.eClipboardOperation.Copy, clipArray]);
	return clipArray;
},
"cut",
function ()
{
	var clipArray = this.copy(null, true);
	if (clipArray && clipArray !== true && clipArray.length)
	{
		for (var i = 0; i < clipArray.length; i++)
		{
			var cLine = clipArray[i];
			for (var j = 0; j < cLine.length; j++)
			{
				var cell = cLine[j];
				if (cell != null && cell.isEditable())
				{
					if (cell.Column.AllowNull)
						cell.setValue(null);
					else if (typeof (cell.Column.DefaultValue) != "undefined" && cell.Column.DefaultValue !== null)
						cell.setValue(cell.Column.DefaultValue);
				}
			}
		}
		this.fireEvent(this.Events.AfterClipboardOperation, [this.Id, this.eClipboardOperation.Cut, clipArray]);
		return clipArray;
	}
	return false;
},
"paste",
function ()
{
	var options = { "strictPaste": false, "selectPastedCells": true, "ignoreHiddenColumns": false, "ignoreServerOnlyCells": true };
	if (this.fireEvent(this.Events.BeforeClipboardOperation, [this.Id, this.eClipboardOperation.Paste, options]) === true)
		return false;
	var pasteFailed = false;
	var pasteText = null;
	try
	{
		pasteText = igtbl_getClipboardData();
		if (pasteText == undefined)
		{
			
			var oError = new Object();
			oError.Options = options;
			oError.OperationType = this.eClipboardOperation.Paste;
			oError.Data = null;
			this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.NotSupported, "", oError]);
			return false;
		}
	}
	catch (exc)
	{
		
		var oError = new Object();
		oError.Options = options;
		oError.OperationType = this.eClipboardOperation.Paste;
		oError.Data = null;
		this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.Failure, exc, oError]);
		return false;
	}
	if (!pasteText)
	{
		
		var oError = new Object();
		oError.Options = options;
		oError.OperationType = this.eClipboardOperation.Paste;
		oError.Data = null;
		this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.NothingToPaste, "", oError]);
		return false;
	}
	var clipArray = this.processPastedText(pasteText, options);
	this.fireEvent(this.Events.AfterClipboardOperation, [this.Id, this.eClipboardOperation.Paste, clipArray]);
	return clipArray;
},

"_preparePastedCells",
function (pasteText, newLineDelimiter)
{
	var clipArray = pasteText.split(newLineDelimiter);
	var multiline = false;
	for (var i = 0; i < clipArray.length; i++)
	{
		var cellStr = clipArray[i];
		if (cellStr.indexOf("\"") == 0)
		{
			pasteText = pasteText.replace(cellStr + newLineDelimiter, cellStr + "<br/>");
			multiline = true;
		}
	}
	if (multiline)
		clipArray = pasteText.split(newLineDelimiter);
	return clipArray;
},
"processPastedText",
function (pasteText, options, newLineDelimiter)
{
	
	if (!options) options = { "strictPaste": false, "selectPastedCells": true, "ignoreHiddenColumns": false, "ignoreServerOnlyCells": true };
	if (!newLineDelimiter) newLineDelimiter = "\n"; 
	
	var clipArray = this._preparePastedCells(pasteText, newLineDelimiter);
	for (var i = clipArray.length - 1; i >= 0; i--)
		if (clipArray[i])
		{
			var lineStr = clipArray[i];
			if (lineStr.substr(lineStr.length - 1, 1) == "\r")
				lineStr = lineStr.substr(0, lineStr.length - 1);
			var clipLine = clipArray[i] = lineStr.split("\t");
			
			for (var j = 0; j < clipLine.length; j++)
			{
				cellStr = clipLine[j];
				if (cellStr.substr(0, 1) == "\"" && cellStr.substr(cellStr.length - 1, 1) == "\"")
					clipLine[j] = cellStr.substr(1, cellStr.length - 2).replace(/\<br\/\>/g, "\n\r").replace(/\"\"/g, "\"");
			}
		}
	if (clipArray.length > 1 && !clipArray[clipArray.length - 1])
		clipArray.pop();
	var cell; 
	if (clipArray.length == 1 && clipArray[0].length == 1)
	{
		var v = clipArray[0][0];
		var clipArray = this._getSelectedCells();
		for (var i = 0; i < clipArray.length; i++)
		{
			cell = clipArray[i];
			if (!cell) continue;
			if (v)
			{
				if (cell.isEditable())
					cell.setValue(v);
			}
			else
			{
				if (cell.isEditable())
				{
					if (cell.Column.AllowNull)
						cell.setValue(null);
					else if (typeof (cell.Column.DefaultValue) != "undefined" && cell.Column.DefaultValue !== null)
						cell.setValue(cell.Column.DefaultValue);
				}
			}
		}
	}
	else
	{
		var activeRow = null;
		var activeCell = this.getActiveCell();
		if (!activeCell)
		{
			activeRow = this.getActiveRow();
			if (activeRow)
				activeCell = activeRow.getCell(0);
		}
		if (!activeCell)
		{
			
			var oError = new Object();
			oError.OperationType = this.eClipboardOperation.Paste;
			oError.Data = pasteText;
			this.fireEvent(this.Events.ClipboardError, [this.Id, this.eClipboardError.NoActiveObject, "", oError]);
			return false;
		}
		if (clipArray.length && (clipArray.length > 1 || clipArray[0].length > 1))
		{
			var curSelArray = this._generateSelArray();
			if (curSelArray && curSelArray.length && (curSelArray.length > 1 || curSelArray[0].length > 1)
					&& curSelArray.length == clipArray.length && curSelArray[0].length == clipArray[0].length)
				activeCell = curSelArray[0][0];
		}
		var sct = activeCell.Band.getSelectTypeCell();
		if (options.selectPastedCells && sct == 3)
			this.clearSelectionAll();
		var row = activeCell.Row;
		var cellIndex = activeCell.Index;
		for (var i = 0; row && i < clipArray.length; i++)
		{
			var cell = row.getCell(cellIndex);
			if (clipArray[i])
				for (var j = 0; cell && j < clipArray[i].length; j++)
				{
					if (!cell.Element)
					{
						cell = cell.getNextCell();
						if (options.ignoreServerOnlyCells)
							j--;
						continue;
					}
					if (clipArray[i][j])
					{
						if (cell.isEditable() && (!options.ignoreHiddenColumns || !cell.Column.getHidden()))
						{
							cell.setValue(clipArray[i][j]);
							if (options.selectPastedCells && sct == 3)
								igtbl_selectCell(this.Id, cell, true);
						}
						clipArray[i][j] = cell;
					}
					else
					{
						clipArray[i][j] = null;
						if (options.strictPaste && cell.isEditable() && (!options.ignoreHiddenColumns || !cell.Column.getHidden()))
						{
							if (cell.Column.AllowNull)
								cell.setValue(null);
							else if (typeof (cell.Column.DefaultValue) != "undefined" && cell.Column.DefaultValue !== null)
								cell.setValue(cell.Column.DefaultValue);
							if (options.selectPastedCells && sct == 3)
								igtbl_selectCell(this.Id, cell, true);
						}
					}
					cell = cell.getNextCell();
				}
			row = row.getNextRow();
		}
	}
	return clipArray;
},
"correctScrollWidthForFixedHeaders",
function ()
{
	if (!this.UseFixedHeaders)
		return;

	var expandAreaWidth = (this.Bands.length > 1) ? this.Element.firstChild.firstChild.offsetWidth : 0;
	var largestWidth = (this.Element.offsetWidth + expandAreaWidth + (this.GroupCount == 1 ? this.Bands[0].getIndentation() : 0));

	for (var i = 0; i < this.Rows.length; i++)
	{
		var row = this.Rows.getRow(i);
		if (row && !row.getHidden() && row.Band.getExpandable() && row.getExpanded() && row.ChildRowsCount > 0)
			largestWidth = this._correctScrollWidth(largestWidth, row);
	}
	var divs = this._scrElem;
	divs.removeAttribute("scrollDivWidth");
	divs.firstChild.style.width = largestWidth + "px";
	this.alignDivs();
},
"_correctScrollWidth",
function (sWidth, row)
{
	var childRow = row.getChildRow(0);
	var crElem = (childRow ? childRow.Element : null);
	if (crElem.parentNode && crElem.parentNode.parentNode)
	{
		var width = crElem.parentNode.parentNode.offsetWidth;
		for (var iCells = 0; iCells < crElem.childNodes.length - 1; iCells++)
		{   
			var crElem = crElem.childNodes[iCells];
			width += crElem.offsetWidth;
		}
		if (row.Band.Index > 0)
		{   
			for (var iBandsIndex = row.Band.Index; iBandsIndex >= 0; iBandsIndex--)
			{
				width += this.Bands[iBandsIndex].getIndentation();
			}
		}

		if (this.GroupCount > 0)
			width += this.Bands[0].getIndentation() * this.GroupCount;
		if (width > sWidth)
			sWidth = width;
	}
	for (var i = 0; i < row.ChildRowsCount; i++)
	{
		var childRow = row.getChildRow(i);
		if (childRow && !childRow.getHidden() && childRow.Band.getExpandable() && childRow.getExpanded()
			&& childRow.ChildRowsCount > 0)
		{
			sWidth = this._correctScrollWidth(sWidth, childRow);
		}
	}
	return sWidth;
}
];
for (var i = 0; i < igtbl_ptsGrid.length; i += 2)
	igtbl_Grid.prototype[igtbl_ptsGrid[i]] = igtbl_ptsGrid[i + 1];


function _igtbl_findSortImage(nodes)
{
	if (nodes == null) return null; 
	for (var imgNodeIndex = 0; imgNodeIndex < nodes.length; imgNodeIndex++)
	{
		var currentNode = nodes[imgNodeIndex];
		if (currentNode.tagName == "IMG" && currentNode.getAttribute("imgType") == "sort")
			return currentNode;
		else
		{   
			var lowerNode = _igtbl_findSortImage(currentNode.childNodes);
			
			if (lowerNode) return lowerNode;
		}
	}
	return null;
}

// Row object 
igtbl_Row.prototype = new igtbl_WebObject();
igtbl_Row.prototype.constructor = igtbl_Row;
igtbl_Row.base = igtbl_WebObject.prototype;
function igtbl_Row(element, node, rows, index)
{
	if (arguments.length > 0)
		this.init(element, node, rows, index);
}
var igtbl_ptsRow = [
"init",
function (element, node, rows, index)
{
	igtbl_Row.base.init.apply(this, ["row", element, node]);

	var gs = rows.Band.Grid;
	var gn = gs.Id;
	this.gridId = gs.Id;
	var row = this.Element;
	row.Object = this;
	this.OwnerCollection = rows;
	if (this.OwnerCollection)
		this.ParentRow = this.OwnerCollection.ParentRow;
	else
		this.ParentRow = null;
	this.Band = this.OwnerCollection.Band;
	this.GroupByRow = false;
	this.GroupColId = null;
	if (row.getAttribute("groupRow"))
	{
		this.GroupByRow = true;
		this.GroupColId = row.getAttribute("groupRow");
		var sTd = row.childNodes[0].childNodes[0].tBodies[0].childNodes[0].childNodes[0];
		this.MaskedValue = sTd.getAttribute("cellValue");
		this.Value = this.MaskedValue;
		if (sTd.getAttribute(igtbl_sUnmaskedValue))
			this.Value = sTd.getAttribute(igtbl_sUnmaskedValue);
		this.Value = igtbl_getColumnById(this.GroupColId).getValueFromString(this.Value);
	}
	var fr = igtbl_getFirstRow(row);
	this.Expandable = ((fr.nextSibling && fr.nextSibling.getAttribute("hiddenRow") || this.Element.getAttribute("showExpand")));
	this.ChildRowsCount = 0;
	this.VisChildRowsCount = 0;
	if (this.Expandable)
	{
		if (fr.nextSibling && fr.nextSibling.getAttribute("hiddenRow"))
		{
			this.HiddenElement = fr.nextSibling;
			if (this.getExpanded() && !gs.ExpandedRows[this.Element.id])
				gs.ExpandedRows[this.Element.id] = this;
			this.ChildRowsCount = igtbl_rowsCount(igtbl_getChildRows(gn, row));
			this.VisChildRowsCount = igtbl_visRowsCount(igtbl_getChildRows(gn, row));
			var rowsNode = (this.Node ? this.Node.selectSingleNode("Rs") : null);
			this.Rows = new igtbl_Rows(rowsNode, gs.Bands[rows.Band.Index + (this.GroupByRow ? 0 : 1)], this);
			
			var rowIslandFilters = null;
			if (rowsNode)
				rowIslandFilters = eval(rowsNode.getAttribute("columnFilters"));
			if (rowIslandFilters)
				_igtbl_processServerPassedColumnFilters(rowIslandFilters, gs);

			//* OBSOLETE*
			this.FirstChildRow = this.Rows.getRow(0);
			//***********
		}
	}
	this.FirstRow = fr;

	if (this.Node)
	{
		if (!this.Expandable)
			this.Expandable = this.Node.selectSingleNode("Rs") != null || this.Node.getAttribute("showExpand") == "true";

	}
	if (!this.GroupByRow)
	{
		this.cells = new Array(this.Band.Columns.length);
		if (gs.UseFixedHeaders)
		{
			for (var i = 0; i < this.Element.cells.length; i++)
			{
				if (this.Element.cells[i].childNodes.length > 0 && this.Element.cells[i].firstChild.tagName == "DIV" && this.Element.cells[i].firstChild.id.substr(this.Element.cells[i].firstChild.id.length - 4) == "_drs")
				{
					this.nfElement = this.Element.cells[i].firstChild.firstChild.childNodes[1].rows[0];
					this.nfElement.Object = this;
					break;
				}
			}
		}
		if (!this.IsAddNewRow && !this.IsFilterRow)
		{
			var tr = this.Element;
			var cellId = this.Id.split("_");
			var lastIndex = cellId.length;
			cellId[1] = "rc";
			var j = 0;
			
			if (this.Band.Grid.Bands.length > 1 || (gs.LoadOnDemand && this.Expandable)) j++;
			if (this.Band.getRowSelectors() < 2) j++;
			var cols = this.Band.Columns;
			var nonFixed = false, colSpan = 1;
			for (var i = 0; i < cols.length; i++)
			{
				if (colSpan > 1)
				{
					colSpan--;
					continue;
				}
				if (cols[i].getFixed() === false && !nonFixed)
				{
					tr = this.nfElement;
					j = 0;
					nonFixed = true;
				}
				if (cols[i].hasCells())
				{
					
					if (tr && tr.cells[j] && !tr.cells[j].id)
					{
						cellId[lastIndex] = cols[i].Index.toString();
						tr.cells[j].id = cellId.join("_");
						colSpan = tr.cells[j].colSpan;
					}
					j++;
					
				}
			}
		}
	}

	if (this.Node)
	{
		
		this.DataKey = "";
		if (this.get(igtbl_litPrefix + "DataKey"))
			this.DataKey = unescape(this.get(igtbl_litPrefix + "DataKey"));
	}
	else
	{
		if (this.Element.getAttribute(igtbl_litPrefix + "DataKey"))
		
			this.DataKey = unescape(this.Element.getAttribute(igtbl_litPrefix + "DataKey"));
	}
	this.Expanded = this.getExpanded();
	this._Changes = new Object();
	this._dataChanged = 0;
	if (gs.ExpandedRows[this.Id])
	{
		var stateChange = gs.ExpandedRows[this.Id];
		stateChange.Object = this;
		gs.ExpandedRows[this.Id] = this;
		if (this.DataKey)
		{
			var value = this.DataKey;
			if (value == "" && typeof (value) == "string") value = "\x01";
			ig_ClientState.setPropertyValue(stateChange.Node, "Value", value);
		}
		this._Changes[stateChange.Type] = stateChange;
	}
},
"getDataKey",
function ()
{
	
	
	if (typeof (this.DataKey) == "undefined" || this.DataKey === null) return null;
	var dKey = this.DataKey.split('\x07');
	return dKey;
},
"getIndex",
function (
virtual
)
{
	if (this.Node)
	{
		var index = igtbl_parseInt(this.Node.getAttribute("i"));
		var g = this.Band.Grid;
		if (this.Band.Index == 0 && !virtual && g.XmlLoadOnDemandType == 2)
		{
			var firstRow = g.Rows.getRow(0);
			var topRow;
			if (firstRow)
				topRow = igtbl_parseInt(firstRow.Node.getAttribute("i"));
			else
			{
				var de = g.getDivElement();
				
				var topRow = Math.round(de.scrollTop / g.getDefaultRowHeight());
			}
			index -= topRow;
		}
		return index;
	}
	else if (this.OwnerCollection)
		return this.OwnerCollection.indexOf(this);
	return -1;
},
"toggleRow",
function ()
{
	this.setExpanded(!this.getExpanded());
},
"getExpanded",
function (expand)
{
	return (this.Expandable && this.HiddenElement && this.HiddenElement.style.display == "");
},
"setExpanded",
function (expand)
{
	if (this.Band.getExpandable() != 1 || !this.Expandable)
		return;
	if (expand != false)
		expand = true;
	var gn = this.gridId;
	if (expand == this.getExpanded())
	{
		if (expand && !this._Changes["ExpandedRows"])
			igtbl_stateExpandRow(gn, this, expand);
		return;
	}
	var gs = igtbl_getGridById(gn);
	if (gs.isDisabled()) return;

	
	if (igtbl_inEditMode(gn))
	{
		var elem = gs._editorCurrent;
		if (elem && elem.getAttribute("noOnBlur"))
			elem.removeAttribute("noOnBlur");
		igtbl_hideEdit(gn);
	}
	if (gs._scrElem && gs.IsXHTML && this.GroupByRow && expand && !this.HiddenElement)
		gs._scrElem.scrollLeft = 0;
	var rcrRes = true;
	if (gs.LoadOnDemand == 3 && !this.HiddenElement)
		rcrRes = this.requestChildRows();
	if (rcrRes)
		this._setExpandedComplete(expand);

	
	if (this.Node && ig_csom.IsIE7Compat) 
	{
		var row = this;
		var rowElement = this.Element;
		
		while (rowElement = rowElement.nextSibling)
		{
			
			if (!igtbl_string.isNullOrEmpty(rowElement, "hiddenRow") || !igtbl_string.isNullOrEmpty(rowElement, "groupRow"))
			{
				
				rowElement.style.position = "";
				rowElement.style.position = "relative";
			}
		}
	}

	
	if (gs._editorButton && gs.oActiveCell && gs.oActiveCell.Row.ParentRow && gs.oActiveCell.hasButtonEditor(igtbl_cellButtonDisplay.OnMouseEnter))
	{
		if (expand == false && gs._editorButton.style.display != "none" && gs.oActiveCell.Row.ParentRow == this)
			igtbl_showColButton(gn, "hide");
		else if (gs.oActiveCell.Row.ParentRow != this && gs._editorButton.style.display != "none" || gs.oActiveCell.Row.ParentRow == this && gs._editorButton.style.display == "none")
			igtbl_showColButton(gn, gs.oActiveCell.Element);
	}
},
"_setExpandedComplete",
function (expand)
{
	var gn = this.gridId;
	var gs = igtbl_getGridById(gn);
	if (this.Node)
	{
		var rsn = this.Node.selectSingleNode("Rs");

		
		if (!this.Rows)
		{
			if (this.GroupByRow || gs.Bands.length > this.Band.Index + 1)
				this.Rows = new igtbl_Rows(rsn, gs.Bands[this.Band.Index + (this.GroupByRow ? 0 : 1)], this);
			
			var rowIslandFilters = null;
			if (rsn)
				rowIslandFilters = eval(rsn.getAttribute("columnFilters"));
			if (rowIslandFilters)
				_igtbl_processServerPassedColumnFilters(rowIslandFilters, gs);
		}
		if (!this.HiddenElement && this.Rows)
		{
			this.prerenderChildRows();
			if (rsn)
				this.Rows.render();
		}
		
		
		if (gs.LoadOnDemand != 3 && this.Rows && typeof (this.Rows.Band.SortedColumns) != "undefined" && igtbl_getLength(this.Rows.Band.SortedColumns) > 0)
		{
			
			if (!(this.GroupByRow && igtbl_getColumnById(this.GroupColId).Id == this.Band.SortedColumns[this.Band.SortedColumns.length - 1]))
				this.Rows.sort();
		}
	}
	else if (!this.Rows)
	{
		if (this.GroupByRow || gs.Bands.length > this.Band.Index + 1)
			this.Rows = new igtbl_Rows(null, gs.Bands[this.Band.Index + (this.GroupByRow ? 0 : 1)], this);
		
		if ((gs.LoadOnDemand == 0 || gs.LoadOnDemand == 3) && this.Rows)
			this.prerenderChildRows();
	}
	var srcRow = this.getFirstRow().id;
	var sr = igtbl_getElementById(srcRow);
	var hr = this.HiddenElement;
	var cancel = false;
	if (expand != false)
	{
		
		if (igtbl_fireEvent(gn, gs.Events.BeforeRowExpanded, "(\"" + gn + "\",\"" + srcRow + "\");") == true)
			cancel = true;
		if (!cancel)
		{
			if (ig_csom.IsFireFox && this.GroupByRow)
			{
				var cr = this;
				while (cr && cr.GroupByRow)
				{
					if (!cr._origHeight)
						cr._origHeight = cr.FirstRow.offsetHeight; 
					cr = cr.ParentRow;
				}
			}
			if (!gs.NeedPostBack || gs.LoadOnDemand != 0 && this.Rows && (this.Rows.length > 0
				|| this.Rows.AddNewRow
			
				|| this.Rows.hasRowFilters()
				))
			{
				gs.NeedPostBack = false;
				if (hr)
				{
					hr.style.display = "";
					hr.style.visibility = "";

					
					if (ig_csom.IsIE6)
					{
						var selectElements = hr.getElementsByTagName("select");
						for (var x = 0; x < selectElements.length; x++)
						{
							if (selectElements[x].getAttribute("beforeExpandDisplay") != null)
							{
								selectElements[x].style.display = selectElements[x].getAttribute("beforeExpandDisplay");
								selectElements[x].setAttribute("beforeExpandDisplay", null);
							}
						}
					}
				}
				var eImg = sr.childNodes[0].childNodes[0];
				eImg.src = this.Band.getCollapseImage();
				var alt = eImg.getAttribute("alt");
				if (alt != null)
				{
					var clpsAlt = eImg.getAttribute("igAltC");
					if (clpsAlt != null)
					{
						eImg.setAttribute("igAltX", alt);
						eImg.setAttribute("alt", clpsAlt);
						eImg.removeAttribute("igAltC");
					}
				}
			}
			igtbl_stateExpandRow(gn, this, true);
			if (!gs.NeedPostBack)
				igtbl_fireEvent(gn, gs.Events.AfterRowExpanded, "(\"" + gn + "\",\"" + srcRow + "\");");
			if (gs.AddNewBoxVisible)
				gs.updateAddNewBox();
			
			if (this.Rows)
				gs._alignFilterRow(this.Rows);
		}
	}
	else
	{
		if (igtbl_fireEvent(gn, gs.Events.BeforeRowCollapsed, "(\"" + gn + "\",\"" + srcRow + "\")") == true)
			cancel = true;
		if (!cancel)
		{
			if (!gs.NeedPostBack)
			{
				if (hr)
				{
					hr.style.display = "none";
					hr.style.visibility = "hidden";

					
					if (ig_csom.IsIE6)
					{
						var selectElements = hr.getElementsByTagName("select");
						for (var x = 0; x < selectElements.length; x++)
						{
							if (selectElements[x].style.display != "none")
							{
								selectElements[x].setAttribute("beforeExpandDisplay", selectElements[x].style.display);
								selectElements[x].style.display = "none";
							}
							else
								selectElements[x].setAttribute("beforeExpandDisplay", null);
						}
					}
				}
				var eImg = sr.childNodes[0].childNodes[0];
				eImg.src = this.Band.getExpandImage();
				var alt = eImg.getAttribute("alt");
				if (alt != null)
				{
					var xpAlt = eImg.getAttribute("igAltX");
					if (xpAlt != null)
					{
						eImg.setAttribute("igAltC", alt);
						eImg.setAttribute("alt", xpAlt);
						eImg.removeAttribute("igAltX");
					}
				}
			}
			igtbl_stateExpandRow(gn, this, false);
			
			
			var cr = this;
			while (cr)
			{
				if (cr.GroupByRow && cr._origHeight)
				{
					cr.Element.firstChild.firstChild.style.height = cr._origHeight + "px";

				}
				cr = cr.ParentRow;
			}
			
			if (!gs.NeedPostBack)
				igtbl_fireEvent(gn, gs.Events.AfterRowCollapsed, "(\"" + gn + "\",\"" + srcRow + "\");");
		}
	}
	if (!cancel)
	{
		if (gs.NeedPostBack)
		{
			if (expand != false)
				igtbl_moveBackPostField(gn, "ExpandedRows");
			else
				igtbl_moveBackPostField(gn, "CollapsedRows");
		}
	}
	if (gs.XmlLoadOnDemandType != 2)
	{
		
		if (gs.UseFixedHeaders || gs.XmlLoadOnDemandType != 0)
		{
			if (this.GroupByRow)
				hr = this.Rows.getRow(0).Element;

			
			
			var tableWidth = this.Element.offsetWidth;
			if (hr && hr.lastChild.firstChild && hr.lastChild.firstChild.id == gs.Id + "_drs")
				tableWidth = hr.lastChild.firstChild.firstChild.offsetWidth;
			else if (expand && gs.UseFixedHeaders && hr && hr.lastChild && hr.lastChild.tagName == "TD"
				&& hr.lastChild.firstChild && hr.lastChild.firstChild.tagName == "TABLE")
				tableWidth = hr.lastChild.firstChild.offsetWidth;
			for (var iCells = 0; hr && iCells < hr.childNodes.length - 1; iCells++)
			{   
				var hc = hr.childNodes[iCells];
				tableWidth += hc.offsetWidth;
			}
			if (this.Band.Index > 0)
			{   
				for (var iBandsIndex = this.Band.Index; iBandsIndex >= 0; iBandsIndex--)
				{
					tableWidth += gs.Bands[iBandsIndex].getIndentation();
				}
			}
			
			if (gs.GroupCount > 0)
				tableWidth += gs.Bands[0].getIndentation() * gs.GroupCount;

			
			var divs = gs._scrElem;
			if (this.OwnerCollection.Grid.Element.offsetHeight > 0 &&
				divs && divs.firstChild.offsetWidth < tableWidth)
			{   
				divs.setAttribute("scrollDivWidth", tableWidth);
			}
		}
		gs.alignDivs();
	}
	if (!gs.UseFixedHeaders && (gs.StatHeader || gs.StatFooter))
		gs.alignStatMargins();
	if (gs.NeedPostBack)
		igtbl_doPostBack(gn);
},
"getFirstRow",
function ()
{
	return igtbl_getFirstRow(this.Element);
},
"requestChildRows",
function ()
{
	if (this.Rows)
		if (this.Node)
		{
			if (this.Rows.Node)
				return true;
		}
		else
			return true;
	var g = this.Band.Grid;
	if (this.Node && this.Node.selectSingleNode("Rs"))
		return true;
	g.QueryString = "LODXml\x01" + this._buildChildRowsQuery();
	g.RowToQuery = this;
	g.xmlHttpRequest(g.eReqType.ChildRows
		, !g.GridIsLoaded
	);
	return false;
},
"_buildChildRowsQuery",
function ()
{
	var g = this.Band.Grid;
	var sqlWhere = "";
	var sortOrder = "";
	var newLevel = "";
	for (var i = 0; i <= this.Band.Index; i++)
	{
		var cr = this;
		while (cr && cr.Band != g.Bands[i])
			cr = cr.ParentRow;
		if (g.Bands[i].DataKeyField && cr.get(igtbl_litPrefix + "DataKey"))
		{
			
			sqlWhere += cr._generateSqlWhere(g.Bands[i].DataKeyField, unescape(cr.get(igtbl_litPrefix + "DataKey")));
			if (newLevel != null)
				newLevel += (i > 0 ? "_" : "") + cr.getIndex().toString(); 
		}
		else
			newLevel = null;
		sqlWhere += (i == this.Band.Index ? "" : ";");
	}
	var queryString = (newLevel == null ? this.getLevel(true) : newLevel);
	for (var i = 0; i < g.Bands.length; i++)
	{
		var so = "";
		for (var j = 0; j < g.Bands[i].SortedColumns.length; j++)
		{
			var col = igtbl_getColumnById(g.Bands[i].SortedColumns[j]);
			so += col.Key + (col.SortIndicator == 2 ? " DESC" : "") + (j < g.Bands[i].SortedColumns.length - 1 ? "," : "");
		}
		sortOrder += so + (i == g.Bands.length - 1 ? "" : ";");
	}
	var band = g.Bands[this.Band.Index + 1], sCols;
	if (band)
	{
		sCols = band.Index;
		for (var i = 0; i < band.SortedColumns.length; i++)
		{
			var col = igtbl_getColumnById(band.SortedColumns[i]);
			sCols += "|" + col.Index;
			sCols += ":" + col.IsGroupBy.toString();
			sCols += ":" + col.SortIndicator;
		}
	}
	
	var bandFilter = "";
	if (this.Band.RowFilterMode == 1)
	{
		bandFilter = g._getCurrentFiltersString(band.Columns[0], band);
	}
	queryString += "\x02" + sqlWhere;
	queryString += "\x02" + sortOrder;
	
	queryString += "\x02";
	if (band && band.ColumnsOrder)
		queryString += band.ColumnsOrder;
	queryString += "\x02" + sCols;
	queryString += "\x02" + bandFilter;
	
	var filterString = "";
	for (var x = 0; x <= this.Band.Index; x++)
	{
		var b = this.Band.Grid.Bands[x];
		filterString += g._getCurrentFiltersString(b.Columns[0], b, this.Id);
	}

	queryString += "\x02" + filterString;

	this.Band.Grid.NeedPostBack = false;
	return queryString;
},
"_generateBandsSqlWhere",
function (band)
{
	var oSqlWhere = new Object();
	var g = band.Grid;
	oSqlWhere.sqlWhere = "";
	oSqlWhere.newLevel = "";
	for (var i = 0; i <= band.Index; i++)
	{
		var cr = this;
		while (cr && cr.Band != g.Bands[i])
			cr = cr.ParentRow;
		if (g.Bands[i].DataKeyField && cr.get(igtbl_litPrefix + "DataKey"))
		{
			oSqlWhere.sqlWhere += cr._generateSqlWhere(g.Bands[i].DataKeyField, unescape(cr.get(igtbl_litPrefix + "DataKey")));
			if (oSqlWhere.newLevel != null)
				oSqlWhere.newLevel += (i > 0 ? "_" : "") + cr.getIndex().toString();
		}
		else
		{
			
			if (this.Band.IsGrouped)
			{
				if (oSqlWhere.newLevel != null)
					oSqlWhere.newLevel += (i > 0 ? "_" : "") + cr.getIndex().toString();
			}
			else
			{
				oSqlWhere.newLevel = null;
			}
		}
		oSqlWhere.sqlWhere += (i == this.Band.Index ? "" : ";");
	}
	return oSqlWhere;
},
"prerenderChildRows",
function ()
{	
	if (!this.HiddenElement)
	{
		var g = this.Band.Grid;
		
		var band = this.Rows.Band;
		if (!band.Visible) return;

		var hidRow = document.createElement("tr");
		this.HiddenElement = hidRow;
		if (!this.GroupByRow)
		{
			if (this.Element.nextSibling)
				this.Element.parentNode.insertBefore(this.HiddenElement, this.Element.nextSibling);
			else
				this.Element.parentNode.appendChild(this.HiddenElement);
		}
		else
			this.getFirstRow().parentNode.appendChild(this.HiddenElement);
		var rn = this.Element.id.split("_");
		rn[0] = this.gridId + "rh";
		hidRow.id = rn.join("_");
		hidRow.setAttribute("hiddenRow", true);
		hidRow.setAttribute("groupRow", this.GroupColId);
		hidRow.style.position = "relative";
		var majCell;
		var img;
		var tBody;
		var childGroupRows = (this.Rows.Node && this.Rows.SelectedNodes[0] && this.Rows.SelectedNodes[0].nodeName == "Group");

		if (this.GroupByRow)
		{
			var majCell = document.createElement("td");
			hidRow.appendChild(majCell);
			majCell.style.paddingLeft = this.Band.getIndentation() + "px"; 
		}
		else
		{
			if (band.IndentationType != 2)
			{
				var ec = document.createElement(
					"th"
					);
				hidRow.appendChild(ec);
				if (!band._optSelectRow)
					ec.className = this.Band.getExpAreaClass();
				ec.style.borderWidth = 0;
				ec.style.textAlign = "center";
				ec.style.padding = 0;
				ec.style.cursor = "default";
				ec.style.height = "auto"; 
				ec.innerHTML = "&nbsp;";
				if (this.Band.getRowSelectors() == 1)
				{
					var rsc = document.createElement(
						"th"
					);
					hidRow.appendChild(rsc);
					rsc.className = this.Band.getRowLabelClass();
					rsc.style.height = "auto"; 
					img = document.createElement("img");
					img.src = g.BlankImage;
					img.border = 0;
					img.style.visibility = "hidden";
					rsc.appendChild(img);
				}
			}
			majCell = document.createElement("td");
			hidRow.appendChild(majCell);
			majCell.style.overflow = (ig_shared.IsIEStandards ? "visible" : "auto");
			majCell.style.width = "100%";
			majCell.style.border = 0;
			majCell.colSpan = this.Band.VisibleColumnsCount + 1 + (this.Band.getRowSelectors() == 1 ? 1 : 0);
			if (g.UseFixedHeaders && band._optSelectRow)
				majCell.className = g.StopperStyle;
		}

		if (!childGroupRows && (band.HeaderHTML || band.FooterHTML))
		{
			var str = "<table>";
			if (band.HeaderHTML)
				str += band.HeaderHTML;
			str += "<tbody></tbody>";
			if (band.FooterHTML)
				str += band.FooterHTML;
			str += "</table>";
			majCell.innerHTML = str;
			table = majCell.firstChild;
			tBody = table.tBodies[0];
		}
		else
			table = document.createElement("table");
		
		rn[0] = this.gridId;
		rn[1] = "t";
		table.id = rn.join("_");
		
		
		
		table.border = 0;
		table.cellPadding = g.Element.cellPadding;
		table.cellSpacing = g.Element.cellSpacing;
		table.setAttribute("bandNo", band.Index);
		table.style.position = "relative";
		table.style.borderCollapse = this.Band.getBorderCollapse();

		
		
		//		if (ig_csom.IsIE7)
		//		{
		//		   table.width="100%";
		//		}
		if (band._wdth == "100%")
		{
			table.width = "100%";
		}
		
		if (majCell.style.width == "100%" && g.MainGrid.style.width.substr(g.MainGrid.style.width.length - 1) == "%")
			table.width = "100%";

		
		
		table.style.tableLayout = "fixed";
		if (this.Rows && this.Rows.Node && this.Rows.Node.selectSingleNode("Group"))
			table.style.tableLayout = "auto";
		if (g.TableLayout != 1)
			table.style.tableLayout = "auto";

		if (childGroupRows)
		{
			majCell.appendChild(table);
			table.width = "100%";
			var tHead = document.createElement("thead");
			var tr = document.createElement("tr");
			var th = document.createElement("th");
			th.innerHTML = "&nbsp;";
			tr.appendChild(th);
			tHead.appendChild(tr);
			tHead.style.display = "none";
			table.appendChild(tHead);
			tBody = document.createElement("tbody");
			table.appendChild(tBody);
		}
		else
		{
			if (!band.HeaderHTML)
			{
				majCell.appendChild(table);
				var colGr = document.createElement("colgroup");
				var col;
				var tableWidth = 0;
				if (g.Bands.length > 1)
				{
					col = document.createElement("col");
					if (band.getIndentation() > 0)
						col.width = band.getIndentation();
					else
						col.style.display = "none";
					colGr.appendChild(col);
					if (col.width)
						tableWidth += parseInt(col.width, 10);
				}

				if (band.getRowSelectors() == 1)
				{
					col = document.createElement("col");
					col.width = (band.RowLabelWidth ? band.RowLabelWidth : "22px");
					colGr.appendChild(col);
					if (col.width)
						tableWidth += parseInt(col.width, 10);
				}
				var tablePercWidth = "";
				var fixedColWidth = 0;
				for (var i = 0; i < band.Columns.length; i++)
				{
					var co = band.Columns[i];
					if (co.getVisible())
					{
						col = document.createElement("col");
						if (g.UseFixedHeaders && !co.getFixed() && co.Node && co.Node.getAttribute(igtbl_litPrefix + "widthResolved"))
							try
							{
								col.width = co.Node.getAttribute(igtbl_litPrefix + "widthResolved");
								
								if (col.width.length > 1 && col.width.substr(col.width.length - 1) == "%")
									tablePercWidth = "100%";
							} catch (e) { ; }
						else
							try
							{
								
								
								var colWidth = (co.Node ? co.Node.getAttribute(igtbl_litPrefix + "widthResolved") : null);
								if (colWidth && typeof (colWidth) == "string" && colWidth.substr(colWidth.length - 1) == "%")
									col.width = colWidth;
								else
									col.width = co.getWidth();
							} catch (e) { ; }

						
						if ((!ig_shared.IsIE9Plus || !g.UseFixedHeaders) || (ig_shared.IsIE9Plus && co.getFixed()))
							colGr.appendChild(col);
						else if (ig_shared.IsIE9Plus && !co.getFixed())
							fixedColWidth += parseInt(col.width);
					}
				}
				
				if (fixedColWidth > 0)
				{
					col = document.createElement("col");
					col.width = fixedColWidth;
					colGr.appendChild(col);
				}
				for (var i = 0; i < band.Columns.length; i++)
					if (band.Columns[i].getHidden())
					{
						col = document.createElement("col");
						col.width = "1px";
						col.style.display = "none";
						colGr.appendChild(col);
					}
				if (table.childNodes.length > 0)
					table.insertBefore(colGr, table.childNodes[0]);
				else
					table.appendChild(colGr);
				var tHead = document.createElement("thead");
				if (this.Band.Index == 0 && this.Band.Grid.StatHeader && this.GroupByRow && g.get("StationaryMarginsOutlookGroupBy") == "True")
					tHead.style.display = "none";
				if (table.childNodes.length > 1)
					table.insertBefore(tHead, table.childNodes[1]);
				else
					table.appendChild(tHead);
				igtbl_addEventListener(tHead, "mousedown", igtbl_headerClickDown);
				igtbl_addEventListener(tHead, "mouseup", igtbl_headerClickUp);
				igtbl_addEventListener(tHead, "mouseout", igtbl_headerMouseOut);
				igtbl_addEventListener(tHead, "mousemove", igtbl_headerMouseMove);
				igtbl_addEventListener(tHead, "mouseover", igtbl_headerMouseOver);
				igtbl_addEventListener(tHead, "contextmenu", igtbl_headerContextMenu);
				if (band._optSelectRow)
				{
					tHead.className = band.getItemClass();
					tHead.className += " ";
					tHead.className += band.getHeadClass();
				}
				var tr = document.createElement("tr");
				tHead.appendChild(tr);
				var th;

				if (g.Bands.length > 1)
				{
					th = document.createElement("th");
					if (!band._optSelectRow)
						th.className = band.NonSelHeaderClass;
					th.height = band.DefaultRowHeight;
					img = document.createElement("img");
					img.src = g.BlankImage;
					img.border = 0;
					th.appendChild(img);
					tr.appendChild(th);
				}

				if (band.getRowSelectors() == 1)
				{
					th = document.createElement("th");
					if (!band._optSelectRow)
						th.className = band.NonSelHeaderClass;
					th.height = band.DefaultRowHeight;
					img = document.createElement("img");

					
					img.src = g.GridCornerImage ? g.GridCornerImage : g.BlankImage;
					igtbl_addEventListener(img, "click", igtbl_GridCornerClick);
					img.setAttribute("gridName", g.UniqueID);
					img.border = 0;
					th.appendChild(img);
					tr.appendChild(th);
				}
				var nfrow = null;
				var setHeight = false;
				for (var i = 0; i < band.Columns.length; i++)
				{
					var column = band.Columns[i];
					if (column.hasCells())
					{
						th = document.createElement("th");
						th.id = this.gridId + "_c" + "_" + band.Index + "_" + i.toString();
						th.setAttribute("columnNo", i);
						if (column.getHidden())
							th.style.display = "none";
						var headerNode = null;
						if (column.Node)
						{
							headerNode = column.Node.selectSingleNode("Header");
							var titleAttrib;
							if (headerNode && (titleAttrib = headerNode.getAttribute(igtbl_litPrefix + "title")))
								th.setAttribute("title", unescape(titleAttrib))
						}
						
						
						var colHeadImg = "";
						var colHeadImgUrl;
						var colHeadImgAltText;
						var colHeadImgHeight;
						var colHeadImgWidth;
						if (headerNode)
						{
							colHeadImgUrl = headerNode.getAttribute("ImageUrl");
							colHeadImgAltText = headerNode.getAttribute("ImageAltText");
							colHeadImgHeight = headerNode.getAttribute("ImageHeight");
							colHeadImgWidth = headerNode.getAttribute("ImageWidth");
						}
						else
						{
							colHeadImgUrl = column.HeaderImageUrl;
							colHeadImgAltText = column.HeaderImageAltText;
							colHeadImgHeight = column.HeaderImageHeight;
							colHeadImgWidth = column.HeaderImageWidth;
						}
						if (colHeadImgUrl || colHeadImgAltText)
						{
							colHeadImg = "<img";
							if (colHeadImgUrl)
								colHeadImg += " src=" + unescape(colHeadImgUrl);
							if (colHeadImgAltText)
								colHeadImg += " alt=" + unescape(colHeadImgAltText);
							if (colHeadImgHeight)
								colHeadImg += " Height=" + colHeadImgHeight;
							if (colHeadImgWidth)
								colHeadImg += " Width=" + colHeadImgWidth;
							colHeadImg += ">";
						}
						
						var filterImage = "";
						if (column.AllowRowFiltering >= 2
								&& column.Band.FilterUIType == 2 
						) 
						{
							var useAppliedImage = false;
							
							if (column.RowFilterMode == 1 || column.Band.Index == 0)
							{
								var filterPanel = g.Bands[column.Band.Index]._filterPanels[column.Id];
								useAppliedImage = (filterPanel && filterPanel.getOperator() > 0);
							}
							else
							{
								var innerTableId = this.Id.replace("_r_", "_t_");
								var filterPanel = band._filterPanels[innerTableId];
								useAppliedImage = filterPanel && filterPanel[column.Id] && filterPanel[column.Id].getOperator() > 0;
							}
							filterImage = "<img src='";
							filterImage += (useAppliedImage ? g.FilterAppliedImage : g.FilterDefaultImage);
							filterImage += "' border='0px' imgType='filter'";
							filterImage += " onmousedown='javascript:ig_cancelEvent(event);'";
							filterImage += " onmouseup='javascript:igtbl_showFilterOptions(\"" + column.Id + "\",event);'";
							if (!column.getFilterIcon())
							{
								filterImage += "style=\"display:none\"";
							}
							filterImage += " imgType=\"filter\">";
						}
						var ht = "";
						if (colHeadImg.length > 0)
							ht += colHeadImg;
						
						var headerText = column.HeaderText;
						if (!column.HeaderWrap)
							ht += "<nobr>" + (headerText ? headerText : "&nbsp;");
						else
							ht += column.HeaderText;
						ht += filterImage;
						var sortIndImg = "";
						switch (column.SortIndicator)
						{
							case 1:
								
								sortIndImg = "&nbsp;<img src='" + g.SortAscImg + "' alt='" + g.SortAscAltText + "' border='0' height='12' width='12' imgType='sort'>";
								break;
							case 2:
								
								sortIndImg = "&nbsp;<img src='" + g.SortDscImg + "' alt='" + g.SortDescAltText + "' border='0' height='12' width='12' imgType='sort'>";
								break;
						}
						ht += sortIndImg;
						if (g.UseFixedHeaders && column.getFixedHeaderIndicator() == 2)
						{
							if (column.Fixed)
								ht += "&nbsp;<img src='" + g.FixedHeaderOnImage + "' alt='" + g._fixedHeaderOnAltText + "' border='0' width='12' height='12' imgType='fixed' onclick='igtbl_fixedClick(event)'>";
							else
								ht += "&nbsp;<img src='" + g.FixedHeaderOffImage + "' alt='" + g._fixedHeaderOffAltText + "' border='0' width='12' height='12' imgType='fixed' onclick='igtbl_fixedClick(event)'>";
						}
						
						if (!column.HeaderWrap)
							ht += "</nobr>";
						if (g.UseFixedHeaders && !column.Fixed && !nfrow)
						{
							var nftd = document.createElement("th");
							nftd.colSpan = band.Columns.length - column.Index;
							if (!g.IsXHTML)
								nftd.width = "100%";
							else
							{
								nftd.style.verticalAlign = "top";
								setHeight = true;
							}
							nftd.style.textAlign = "left";
							if (band._optSelectRow)
								nftd.className = g.StopperStyle;
							tr.appendChild(nftd);
							var nfd = document.createElement("div");
							nftd.appendChild(nfd);
							nfd.id = g.Id + "_drs";
							nfd.style.overflow = "hidden";
							if (!g.IsXHTML)
								nfd.style.width = "100%";
							nfd.style.height = "100%";
							if (g.IsXHTML)
								nfd.style.position = "relative";
							var nftable = document.createElement("table");
							nfd.appendChild(nftable);
							
							
							if (!ig_csom.IsIE7Compat)
								nftable.width = "1";
							nftable.border = "0";
							nftable.cellPadding = g.Element.cellPadding;
							nftable.cellSpacing = g.Element.cellSpacing;
							nftable.style.position = "relative";
							nftable.style.tableLayout = "fixed";
							nftable.style.height = "100%"; 
							var nfcgs = document.createElement("colgroup");
							nftable.appendChild(nfcgs);
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getVisible())
								{
									var nfcg = document.createElement("col");
									nfcg.width = band.Columns[j].Width;
									nfcgs.appendChild(nfcg);
								}
							}
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getHidden())
								{
									var nfcg = document.createElement("col");
									nfcg.width = "1px";
									nfcg.style.display = "none";
									nfcgs.appendChild(nfcg);
								}
							}
							if (g._scrElem.scrollLeft)
								nftable.style.left = (-g._scrElem.scrollLeft).toString() + "px";
							var nftb = document.createElement("tbody");
							nftable.appendChild(nftb);
							nfrow = document.createElement("tr");
							nftb.appendChild(nfrow);
						}
						{
							if (!band._optSelectRow)
								th.className = column.getHeadClass();
							else if (this.HeaderClass)
								th.className = this.HeaderClass;
							if (column.HeaderStyle)
								th.style.cssText = column.HeaderStyle;
							th.innerHTML = ht;
						}
						if (nfrow)
						{
							nfrow.appendChild(th);
							if (setHeight)
							{
								var nftd = nfrow.parentNode.parentNode.parentNode.parentNode;
								
								nftd.style.height = nftd.parentNode.offsetHeight + "px";
								setHeight = false;
							}
						}
						else
							tr.appendChild(th);

						
						if (column.getVisible())
							tableWidth += column.getWidth();
					}
				}
				
				if (!ig_csom.IsIE7Compat)
				{
					table.style.width = tableWidth + "px"; //"100%";
				}

				
				if (nftable && nfd && nftable.offsetWidth > nfd.offsetWidth)
					nfd.style.width = nftable.offsetWidth + 'px';

				if (band.ColHeadersVisible != 1)
					tHead.style.display = "none";
				if (tablePercWidth)
					table.style.width = tablePercWidth;
				if (table.tBodies.length == 0)
				{
					tBody = document.createElement("tbody");
					table.appendChild(tBody);
					if (band._optSelectRow)
					{
						tBody.className = band.getItemClass();
						tBody.className += " ";
						tBody.className += band.getHeadClass();
					}
				}
			}
			if (this.Rows.Band.FilterUIType == 1 && this.Rows.Band.FilterRowView == 1)
			{
				
				var tr = document.createElement("tr");
				tBody.appendChild(tr);
				tr.id = this.gridId + "_flr_" + this.getLevel(true);
				tr.setAttribute("filterRow", "true");
				if (band._optSelectRow) tr.className = band.getItemClass();
				var td;
				
				if (g.Bands.length > 1)
				{
					td = document.createElement(
						"th"
					);
					tr.appendChild(td);
					td.className = igtbl_getExpAreaClass(this.gridId, band.Index);
					td.style.height = band.DefaultRowHeight; 
					img = document.createElement("img");
					td.appendChild(img);
					img.src = g.BlankImage;
					img.border = 0;
				}
				
				if (band.getRowSelectors() == 1)
				{
					td = document.createElement(
						"th"
					);
					tr.appendChild(td);
					td.className = igtbl_getRowLabelClass(this.gridId, band.Index);
					td.id = this.gridId + "_fll_" + this.getLevel(true);
					td.style.height = band.DefaultRowHeight; 
					img = document.createElement("img");
					td.appendChild(img);
					img.src = g.BlankImage;
					img.border = 0;
				}
				var nfrow = null;
				setHeight = false;

				var filterTypeImage = null;
				var filterButtonImgString = "";
				for (var i = 0; i < band.Columns.length; i++)
				{
					var column = band.Columns[i];
					if (column.hasCells())
					{
						
						if (filterTypeImage == null || filterTypeImage[0] != column.FilterOperatorDefaultValue)
						{
							filterTypeImage = null;
							var filImgs = g.FilterButtonImages;
							for (var itr = 0; itr < filImgs.length; itr++)
							{
								if (column.FilterOperatorDefaultValue == filImgs[itr][0])
								{
									filterTypeImage = filImgs[itr];
									break;
								}
							}

							
							filterButtonImgString = "<button onclick=\"igtbl_filterTypeSelect(event);\" class=\"" + band.FilterOperandButtonStyle + " " + band.Grid.FilterOperandButtonStyle + "\" style=\"height:100%;padding:0px;\"><img src=\"" + filterTypeImage[1] + "\" alt=\"" + filterTypeImage[2] + "\" operator=" + filterTypeImage[0] + " /></button><span></span>";
						}
						
						var filterValue = "";
						if (band.RowFilterMode == 2 && band._filterPanels && band._filterPanels[table.id] && band._filterPanels[table.id][column.Id])
							filterValue = band._filterPanels[table.id][column.Id].getEvaluationValue();
						else if (band._filterPanels && band._filterPanels[column.Id])
							filterValue = band._filterPanels[column.Id].getEvaluationValue();

						if (filterValue !== "")
						{
							filterButtonImgString = filterButtonImgString.replace("</span>", filterValue + "</span>");
						}

						td = document.createElement("td");
						td.id = this.gridId + "_flc_" + this.getLevel(true) + "_" + i.toString();
						var ct = filterButtonImgString;

						td.className = band.FilterRowStyle + " " + band.Grid.FilterRowStyle;
						if (column.getHidden())
							td.style.display = "none";
						
						if (g.UseFixedHeaders && !column.Fixed && !nfrow)
						{
							var nftd = document.createElement("td");
							nftd.colSpan = band.Columns.length - column.Index;
							if (band._optSelectRow)
							{
								nftd.className = g.StopperStyle;
								if (g.IsXHTML)
									setHeight = true;
							}
							else
							{
								if (!g.IsXHTML)
									nftd.width = "100%";
								else
								{
									nftd.style.verticalAlign = "top";
									setHeight = true;
								}
							}
							tr.appendChild(nftd);
							var nfd = document.createElement("div");
							nftd.appendChild(nfd);
							nfd.id = g.Id + "_drs";
							nfd.style.overflow = "hidden";
							if (!g.IsXHTML)
								nfd.style.width = "100%";
							nfd.style.height = "100%";
							if (g.IsXHTML)
								nfd.style.position = "relative";
							var nftable = document.createElement("table");
							nfd.appendChild(nftable);
							
							if (!ig_csom.IsIE7Compat)
								nftable.width = "1";
							nftable.border = "0";
							nftable.cellPadding = g.Element.cellPadding;
							nftable.cellSpacing = g.Element.cellSpacing;
							nftable.style.position = "relative";
							nftable.style.tableLayout = "fixed";
							nftable.style.height = "100%"; 
							var nfcgs = document.createElement("colgroup");
							nftable.appendChild(nfcgs);
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getVisible())
								{
									var nfcg = document.createElement("col");
									nfcg.width = band.Columns[j].Width;
									nfcgs.appendChild(nfcg);
								}
							}
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getHidden())
								{
									var nfcg = document.createElement("col");
									nfcg.width = "1px";
									nfcg.style.display = "none";
									nfcgs.appendChild(nfcg);
								}
							}
							if (g._scrElem.scrollLeft)
								nftable.style.left = (-g._scrElem.scrollLeft).toString() + "px";
							var nftb = document.createElement("tbody");
							nftable.appendChild(nftb);
							nfrow = document.createElement("tr");
							nfrow.id = this.gridId + "_flfr_" + this.getLevel(true);
							nftb.appendChild(nfrow);
						}

						if (column.CssClass && !band._optSelectRow)
							td.className += (td.className.length > 0 ? " " : "") + column.CssClass;
						td.innerHTML = ct;
						
						if (nfrow)
						{
							nfrow.appendChild(td);
							if (setHeight)
							{
								var nftd = nfrow.parentNode.parentNode.parentNode.parentNode;
								nftd.style.height = nftd.parentNode.offsetHeight + "px";
								setHeight = false;
							}
						}
						else
							tr.appendChild(td);
					}
				}
				this.Rows.FilterRow = new igtbl_FilterRow(tr, this.Rows);
			}
			if (!this.GroupByRow && this.Rows.Band.AddNewRowVisible == 1 && this.Rows.Band.AllowAddNew == 1)
			{
				var tr = document.createElement("tr");
				tBody.appendChild(tr);
				tr.id = this.gridId +
					"_anr_"
					+ this.getLevel(true);
				tr.setAttribute("addNewRow", "true");
				if (band._optSelectRow)
					tr.className = band.getItemClass();
				var td;

				if (g.Bands.length > 1)
				{
					td = document.createElement(
						"th"
					);
					tr.appendChild(td);
					td.className = igtbl_getExpAreaClass(this.gridId, band.Index);
					td.style.height = band.DefaultRowHeight; 
					img = document.createElement("img");
					td.appendChild(img);
					img.src = g.BlankImage;
					img.border = 0;
				}
				if (band.getRowSelectors() == 1)
				{
					td = document.createElement(
						"th"
					);
					tr.appendChild(td);
					td.className = igtbl_getRowLabelClass(this.gridId, band.Index);
					td.id = this.gridId +
						"_anl_"
						+ this.getLevel(true);
					td.style.height = band.DefaultRowHeight; 
					img = document.createElement("img");
					td.appendChild(img);
					img.src = g.BlankImage;
					img.border = 0;
				}
				var nfrow = null;
				setHeight = false;
				for (var i = 0; i < band.Columns.length; i++)
				{
					var column = band.Columns[i];
					if (column.hasCells())
					{
						td = document.createElement("td");
						td.id = this.gridId +
							"_anc_"
							+ this.getLevel(true) + "_" + i.toString();
						var ct = column.DefaultValue;
						if (band.AddNewRowStyle)
							td.style.cssText = band.AddNewRowStyle;
						if (column.getHidden())
							td.style.display = "none";
						if (!column.Wrap)
						{
							
							switch (column.ColumnType)
							{
								case 3:
									if (!ct || ct.toString().toLowerCase() == "false" || ct == "0")
										ct = false;
									else
										ct = true;

									ct = "<nobr><input type='checkbox' tabIndex='-1' " + (ct ? 'CHECKED' : '') + " on" + (ig_csom.IsIE ? "property" : "") + "change='igtbl_chkBoxChange(event,\"" + g.Id + "\");' " +
									
									(g.Section508Compliant ? "alt='" + (ct ? "checked" : "unchecked") + "'" : "")
									  + ">";

									break;
								default:
									ct = "<nobr>" + (ct ? ct : "&nbsp;") + "</nobr>";
									break;
							}
						}
						if (g.UseFixedHeaders && !column.Fixed && !nfrow)
						{
							var nftd = document.createElement("td");
							nftd.colSpan = band.Columns.length - column.Index;
							if (band._optSelectRow)
							{
								nftd.className = g.StopperStyle;
								if (g.IsXHTML)
									setHeight = true;
							}
							else
							{
								if (!g.IsXHTML)
									nftd.width = "100%";
								else
								{
									nftd.style.verticalAlign = "top";
									setHeight = true;
								}
							}
							tr.appendChild(nftd);
							var nfd = document.createElement("div");
							nftd.appendChild(nfd);
							nfd.id = g.Id + "_drs";
							nfd.style.overflow = "hidden";
							if (!g.IsXHTML)
								nfd.style.width = "100%";
							nfd.style.height = "100%";
							if (g.IsXHTML)
								nfd.style.position = "relative";
							var nftable = document.createElement("table");
							nfd.appendChild(nftable);
							
							if (!ig_csom.IsIE7Compat)
								nftable.width = "1";
							nftable.border = "0";
							nftable.cellPadding = g.Element.cellPadding;
							nftable.cellSpacing = g.Element.cellSpacing;
							nftable.style.position = "relative";
							nftable.style.tableLayout = "fixed";
							nftable.style.height = "100%"; 
							var nfcgs = document.createElement("colgroup");
							nftable.appendChild(nfcgs);
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getVisible())
								{
									var nfcg = document.createElement("col");
									nfcg.width = band.Columns[j].Width;
									nfcgs.appendChild(nfcg);
								}
							}
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getHidden())
								{
									var nfcg = document.createElement("col");
									nfcg.width = "1px";
									nfcg.style.display = "none";
									nfcgs.appendChild(nfcg);
								}
							}
							if (g._scrElem.scrollLeft)
								nftable.style.left = (-g._scrElem.scrollLeft).toString() + "px";
							var nftb = document.createElement("tbody");
							nftable.appendChild(nftb);
							nfrow = document.createElement("tr");
							
							nfrow.id = this.gridId + "_anfr_" + this.getLevel(true);
							nftb.appendChild(nfrow);
						}
						
						if (column.CssClass
							&& !band._optSelectRow
						)
							td.className = (td.className.length > 0 ? " " : "") + column.CssClass;
						td.innerHTML = ct;
						if (nfrow)
						{
							nfrow.appendChild(td);
							if (setHeight)
							{
								var nftd = nfrow.parentNode.parentNode.parentNode.parentNode;
								
								nftd.style.height = nftd.parentNode.offsetHeight + "px";
								setHeight = false;

								
								nfrow.style.height = nftd.style.height;
							}
						}
						else
							tr.appendChild(td);
					}
				}
				this.Rows.AddNewRow = new igtbl_AddNewRow(tr, this.Rows);
				igtbl_setNewRowImg(this.gridId, tr);
				g.newImg = null;
			}
			var footersNode = null;
			if (this.Rows.Node)
				footersNode = this.Rows.Node.selectSingleNode("Footers");
			if (band.ColFootersVisible == 1 && !band.FooterHTML)
			{
				var tFoot = document.createElement("tfoot");
				table.appendChild(tFoot);
				if (this.Band.Index == 0 && this.Band.Grid.StatFooter && this.GroupByRow && g.get("StationaryMarginsOutlookGroupBy") == "True")
					tFoot.style.display = "none";
				if (band._optSelectRow)
				{
					tFoot.className = band.getItemClass();
					tFoot.className += " ";
					tFoot.className += band.getHeadClass();
					tFoot.className += " ";
					tFoot.className += band.getFooterClass();
				}
				var tr = document.createElement("tr");
				tFoot.appendChild(tr);
				var th;

				if (g.Bands.length > 1)
				{
					th = document.createElement("th");
					tr.appendChild(th);
					if (!band._optSelectRow)
						th.className = band.getExpAreaClass();
					th.height = band.DefaultRowHeight;
					img = document.createElement("img");
					th.appendChild(img);
					img.src = band.Grid.BlankImage;
					img.border = 0;
					img.style.visibility = "hidden";
				}

				if (band.getRowSelectors() == 1)
				{
					th = document.createElement("th");
					tr.appendChild(th);
					if (!band._optSelectRow)
						th.className = band.getRowLabelClass();
					th.height = band.DefaultRowHeight;
					img = document.createElement("img");
					th.appendChild(img);
					img.src = band.Grid.BlankImage;
					img.border = 0;
					img.style.visibility = "hidden";
				}
				var footers = null;
				if (footersNode)
					footers = footersNode.selectNodes("Footer");
				var nfrow = null;
				setHeight = false;
				for (var i = 0; i < band.Columns.length; i++)
				{
					var column = band.Columns[i];
					if (column.hasCells())
					{
						th = document.createElement("th");
						th.id = this.gridId +
						"_" +
						"f" + "_" + band.Index + "_" + i.toString();
						if (column.getHidden())
							th.style.display = "none";
						var ht = "&nbsp;";
						if (footers && i < footers.length && footers[i].getAttribute("caption"))
							ht = unescape(footers[i].getAttribute("caption"));
						else if (column.Node)
						{
							var fn = column.Node.selectSingleNode("Footer");
							if (fn && fn.getAttribute("caption"))
								ht = unescape(fn.getAttribute("caption"));
						}
						if (g.UseFixedHeaders && !column.Fixed && !nfrow)
						{
							var nftd = document.createElement("th");
							nftd.colSpan = band.Columns.length - column.Index;
							nftd.style.textAlign = "left";
							if (band._optSelectRow)
								nftd.className = g.StopperStyle;
							if (!g.IsXHTML)
								nftd.width = "100%";
							else
							{
								nftd.style.verticalAlign = "top";
								setHeight = true;
							}
							tr.appendChild(nftd);
							var nfd = document.createElement("div");
							nftd.appendChild(nfd);
							nfd.id = g.Id + "_drs";
							nfd.style.overflow = "hidden";
							if (!g.IsXHTML)
								nfd.style.width = "100%";
							nfd.style.height = "100%";
							if (g.IsXHTML)
								nfd.style.position = "relative";
							var nftable = document.createElement("table");
							nfd.appendChild(nftable);
							
							if (!ig_csom.IsIE7Compat)
								nftable.width = "1";
							nftable.border = "0";
							nftable.cellPadding = g.Element.cellPadding;
							nftable.cellSpacing = g.Element.cellSpacing;
							nftable.style.position = "relative";
							nftable.style.tableLayout = "fixed";
							nftable.style.height = "100%"; 

							var nfcgs = document.createElement("colgroup");
							nftable.appendChild(nfcgs);
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getVisible())
								{
									var nfcg = document.createElement("col");
									nfcg.width = band.Columns[j].Width;
									nfcgs.appendChild(nfcg);
								}
							}
							for (var j = column.Index; j < band.Columns.length; j++)
							{
								if (band.Columns[j].getHidden())
								{
									var nfcg = document.createElement("col");
									nfcg.width = "1px";
									nfcg.style.display = "none";
									nfcgs.appendChild(nfcg);
								}
							}
							if (g._scrElem.scrollLeft)
								nftable.style.left = (-g._scrElem.scrollLeft).toString() + "px";
							var nftb = document.createElement("tbody");
							nftable.appendChild(nftb);
							nfrow = document.createElement("tr");
							nftb.appendChild(nfrow);
						}
						{
							if (!band._optSelectRow)
								th.className = column.getFooterClass();
							else if (column.FooterClass)
								th.className = column.FooterClass;
							if (column.FooterStyle)
								th.style.cssText = column.FooterStyle;
							th.innerHTML = ht;
						}
						if (nfrow)
						{
							nfrow.appendChild(th);
							if (setHeight)
							{
								var nftd = nfrow.parentNode.parentNode.parentNode.parentNode;
								nftd.style.height = nftd.parentNode.offsetHeight + "px"; 
								setHeight = false;
							}
						}
						else
							tr.appendChild(th);
					}
				}
			}
		}

		this.Rows.Element = tBody;
		tBody.Object = this.Rows;
	}
},
"getLevel",
function (s, paged)
{
	var l = new Array();
	l[0] = this.getIndex(true);
	var thisRow = this;
	var pr = this.ParentRow;
	while (pr)
	{
		l[l.length] = pr.getIndex(true);
		thisRow = pr;
		pr = pr.ParentRow;
	}
	
	if (paged && thisRow.Band.Grid.AllowPaging)
		l[l.length - 1] += (thisRow.Band.Grid.CurrentPageIndex - 1) * thisRow.Band.Grid.PageSize;

	
	if (l.length > 1)
		l = l.reverse();
	if (s)
	{
		s = l.join("_");
		igtbl_dispose(l);
		delete l;
		return s;
	}
	return l;
},
"getCell",
function (index)
{
	if (index < 0 || !this.cells || index >= this.cells.length)
		return null;
	if (!this.cells[index])
	{
		var cell = null;
		var col = this.Band.Columns[index];
		if (col.hasCells())
		{
			if (this.Band.Grid.UseFixedHeaders && !col.getFixed())
			{
				var i = 0, ci = this.Band.firstActiveCell, colspan = 1;
				var cells = this.Element.cells;
				while (i <= index)
				{
					if (!this.Band.Columns[i].getFixed() && (i == 0 || this.Band.Columns[i - 1].getFixed()))
					{
						
						
						var tempCells = cells[cells.length - 1].firstChild.firstChild.rows
						if (tempCells && tempCells.length > 0)
						{
							cells = tempCells[0].cells;
							ci = 0;
							colspan = 1;
						}
					}
					if (this.Band.Columns[i].hasCells())
					{
						if (i == index && colspan == 1)
							cell = cells[ci];
						if (colspan == 1)
						{
							if (cells[ci])
								colspan = cells[ci].colSpan;
							ci++;
						}
						else
							colspan--;
					}
					i++;
				}
			}
			else
			{
				var ri = col.getRealIndex(this);
				if (ri >= 0)
				{
					cell = this.Element.cells[this.Band.firstActiveCell + ri];
					if (cell)
					{
						var column = igtbl_getColumnById(cell.id);
						if (!column || !igtbl_isColEqual(column, col))
							cell = null;
					}
				}
			}
		}
		var node = null;
		if (this.Node)
		
		{
			var cni = -1, colNo = 0; 
			while (colNo < col.Node.parentNode.childNodes.length)
			{
				if (!col.Node.parentNode.childNodes[colNo].getAttribute("serverOnly"))
					cni++;
				if (colNo == col.Node.getAttribute("columnNo"))
					break;
				colNo++;
			}
			if (cni >= 0 && cni < col.Node.parentNode.childNodes.length)
				node = this.Node.selectSingleNode("Cs").childNodes[cni];
		}
		this.cells[index] = new igtbl_Cell(cell, node, this, index);
	}
	return this.cells[index];
},
"getCellByColumn",
function (col)
{
	return this.getCell(col.Index);
},
"getCellFromKey",
function (key)
{
	var cell = null;
	var col = this.Band.getColumnFromKey(key);
	if (col)
		cell = this.getCellByColumn(col);
	return cell;
},
"getChildRow",
function (index)
{
	if (!this.Expandable)
		return null;
	
	if (!this.FirstChildRow && this.Rows)
		this.FirstChildRow = this.Rows.getRow(0);
	if (index < 0 || index >= this.ChildRowsCount || !this.FirstChildRow)
		return null;
	var i = 0;
	var r = this.FirstChildRow.Element;
	while (i < index && r)
	{
		r = igtbl_getNextSibRow(this.gridId, r);
		i++;
	}
	if (!r)
		return null;
	return igtbl_getRowById(r.id);
},
"compare",
function (row)
{
	if (this.OwnerCollection != row.OwnerCollection)
		return 0;
	if (this.GroupByRow)
		return igtbl_getColumnById(this.GroupColId).compareRows(this, row);
	else
	{
		var sc = this.OwnerCollection.Band.SortedColumns;
		for (var i = 0; i < sc.length; i++)
		{
			var col = igtbl_getColumnById(sc[i]);
			if (col.hasCells())
			{
				var cell1 = this.getCellByColumn(col);
				var cell2 = row.getCellByColumn(col);
				var res = col.compareCells(cell1, cell2);
				if (res != 0)
				{
					return res;
				}
			}
		}
	}
	return 0;
},
"remove",
function (fireEvents)
{
	return this.OwnerCollection.remove(this.OwnerCollection.indexOf(this), fireEvents);
},
"getNextTabRow",
function (shift, ignoreCollapse, addRow
, filterRow
)
{
	var row = null;
	if (shift)
	{
		row = this.getPrevRow(addRow
	, filterRow
		);
		if (row)
		{
			while (row.Rows && (row.getExpanded() || ignoreCollapse && row.Expandable))
			{
				if (addRow && row.Rows.AddNewRow && (row.Band.AddNewRowView == 2 || this.Rows.length == 0 && this.Band.AddNewRowView == 1))
					row = row.Rows.AddNewRow;
				else
					row = row.Rows.getRow(row.Rows.length - 1);
			}
		}
		else if (this.ParentRow)
			row = this.ParentRow;
	}
	else
	{
		if (this.Rows && (this.getExpanded() || ignoreCollapse && this.Expandable))
		{
			if (addRow && this.Rows.AddNewRow && (this.Band.AddNewRowView == 1 || this.Rows.length == 0 && this.Band.AddNewRowView == 2))
				row = this.Rows.AddNewRow;
			else
				row = this.Rows.getRow(0);
		}
		else
		{
			row = this.getNextRow(addRow
			, filterRow
			);
			if (!row && this.ParentRow)
			{
				var pr = this.ParentRow;
				while (!row && pr)
				{
					row = pr.getNextRow(addRow);
					pr = pr.ParentRow;
				}
			}
		}
	}
	return row;
},
"getSelected",
function ()
{
	if (this._Changes["SelectedRows"])
		return true;
	return false;
},
"setSelected",
function (select)
{
	var str = this.Band.getSelectTypeRow();
	if (str > 1)
	{
		if (str == 2)
			this.Band.Grid.clearSelectionAll();
		igtbl_selectRow(this.gridId, this, select);
	}
},
"getNextRow",
function (addRow
	, filterRow
)
{
	var nr;
	if (this.IsAddNewRow)
	{
		if (this.Band.AddNewRowView == 1)
		{
			if (this.Band.Index == 0 && this.Band.Grid.StatHeader || this._dataChanged)
				return null;
			nr = 0;
		}
		else
			if (this.Band.Index == 0 && this.Band.Grid.StatFooter)
				return null;
	}
	else
		if (this.IsFilterRow)
		{
			if (this.Band.FilterRowView == igtbl_featureRowView.Top)
			{
				if (this.Band.Index == 0 && this.Band.Grid.StatHeader )
					return null;
				nr = 0;
			}
			else
				if (this.Band.Index == 0 && this.Band.Grid.StatFooter)
					return null;
		}
		else
			nr = this.getIndex() + 1;
	var nextRow = this.OwnerCollection.getRow(nr);
	while (nr < this.OwnerCollection.length && nextRow && nextRow.getHidden())
	{
		nr++;
		nextRow = this.OwnerCollection.getRow(nr);
	}
	if (nr < this.OwnerCollection.length && nextRow)
		return nextRow;
	if (addRow && this.Band.AddNewRowVisible == 1 && this.Band.AddNewRowView == 2 && nr == this.OwnerCollection.length)
		return this.OwnerCollection.AddNewRow;
	if (filterRow && this.Band.FilterUIType == 1 && this.Band.FilterRowView == igtbl_featureRowView.Bottom && nr == this.OwnerCollection.length)
		return this.OwnerCollection.FilterRow;
	return null;
},
"getPrevRow",
function (addRow
	, filterRow
)
{
	var pr;
	if (this.IsAddNewRow)
	{
		if (this.Band.AddNewRowView == 2)
		{
			if (this.Band.Index == 0 && this.Band.Grid.StatFooter || this._dataChanged)
				return null;
			pr = this.OwnerCollection.length - 1;
		}
		else
			if (this.Band.Index == 0 && this.Band.Grid.StatHeader)
				return null;
	}
	else
		if (this.IsFilterRow)
		{
			if (this.Band.FilterRowView == igtbl_featureRowView.Bottom)
			{
				if (this.Band.Index == 0 && this.Band.Grid.StatFooter )
					return null;
				pr = this.OwnerCollection.length - 1;
			}
			else
				if (this.Band.Index == 0 && this.Band.Grid.StatHeader)
					return null;
		}
		else
			pr = this.getIndex() - 1;
	
	var foundRow = null;
	while (pr >= 0)
	{
		foundRow = this.OwnerCollection.getRow(pr);
		if (!foundRow || !foundRow.getHidden())
			break;
		pr--;
	}
	if (pr >= 0)
		return foundRow;
	if (addRow && this.Band.AddNewRowVisible == 1 && this.Band.AddNewRowView == 1 && pr == -1)
		return this.OwnerCollection.AddNewRow;
	if (filterRow && this.Band.FilterUIType == 1 && this.Band.FilterRowView == igtbl_featureRowView.Top && pr == -1)
		return this.OwnerCollection.FilterRow;
	return null;
},
"activate",
function (fireEvents)
{
	this.Band.Grid.setActiveRow(this, false, fireEvents);
},
"isActive",
function ()
{
	return this.Band.Grid.getActiveRow() == this;
},
"scrollToView",
function ()
{
	igtbl_scrollToView(this.gridId, this.Element);
},
"deleteRow",
function (skipRowRecalc)
{
	var gs = igtbl_getGridById(this.gridId);
	var del = false;
	var rowId = this.Element.id;
	if (this.Band.AllowDelete == 1 || this.Band.AllowDelete == 0 && gs.AllowDelete == 1)
	{
		var rows = this.OwnerCollection;
		if (igtbl_inEditMode(this.gridId))
		{
			igtbl_hideEdit(this.gridId);
			if (igtbl_inEditMode(this.gridId))
				return false;
		}
		if (igtbl_fireEvent(this.gridId, gs.Events.BeforeRowDeleted, "(\"" + this.gridId + "\",\"" + rowId + "\")") == true)
			return false;
		var btn = igtbl_getElementById(this.gridId + "_bt");

		del = true;
		var prevAdded = typeof (gs.AddedRows[rowId]) != "undefined";
		if (!prevAdded)
			gs.invokeXmlHttpRequest(gs.eReqType.DeleteRow, this, null, true); 
		if (gs.XmlResponseObject && gs.XmlResponseObject.Cancel) return;
		if (btn && btn.style.display == "")
			btn.style.display = "none";
		igtbl_scrollLeft(gs.Element.parentNode, 0);
		this.OwnerCollection.setLastRowId();
		if (this.getExpanded())
			this.toggleRow();
		
		if (this.Band.SortedColumns.length == 0)
		{
			igtbl_clearRowChanges(gs, this);
			for (var rid in gs.AddedRows)
				if (rid == rowId || rid.substr(0, rowId.length + 1) == rowId + "_")
					igtbl_clearRowChanges(gs, igtbl_getRowById(rid));
		}
		if (!rows.deletedRows)
			rows.deletedRows = new Array();
		var ar = this.Band.Grid.getActiveRow();
		var needPB = false;
		this.Element.setAttribute("deleted", true);
		if (typeof (this.Node) == "undefined")
		{
			var overlappingColSpan = -1;
			for (var i = 0; i < this.Band.Columns.length; i++)
			{
				var cell = this.getCellByColumn(this.Band.Columns[i]);
				if (!cell && this.Band.Columns[i].hasCells())
				{
					var row = this;
					while (row.getPrevRow() && !cell)
					{
						row = row.getPrevRow();
						cell = row.getCellByColumn(this.Band.Columns[i]);
					}
					if (row == this || !cell || cell.Column.hasCells() && cell.Element != null && cell.Element.rowSpan == 1)
					{
						needPB = true;
						break;
					}
				}
				else if (cell && cell.Column.hasCells() && (!cell.Element || cell.Element.rowSpan > 1))
				{
					if (overlappingColSpan > 1)
						overlappingColSpan--;
					if (cell.Element && cell.Element.rowSpan > 1)
					{
						needPB = true;
						break;
					}
				}
				if (cell && cell.Element)
				{
					if (cell.Element.rowSpan > 1)
						cell.Element.rowSpan--;
					if (cell.Element.colSpan > 1)
						overlappingColSpan = cell.Element.colSpan;
				}
			}
		}
		if (!needPB)
		{
			rows.deletedRows[rows.deletedRows.length] = this.remove(false);
			
			if (gs.LoadOnDemand == 3 && (!gs.Events.XmlHTTPResponse || gs.Events.XmlHTTPResponse[1] || gs.Events.AfterRowDeleted[1]))
				gs._removeChange("DeletedRows", this);
			var pr = this.ParentRow;
			if (pr)
			{
				pr.VisChildRowsCount--;
				pr.ChildRowsCount--;
			}
			while (pr)
			{
				if (pr.Expandable && pr.Rows.length == 0)
				{
					if (pr.Rows.Band.AddNewRowVisible != 1)
						pr.setExpanded(false);
					if (pr.GroupByRow)
					{
						gs._removeChange("CollapsedRows", pr);
						gs.DeletedRows[pr.Element.id] = true;
						pr.Element.setAttribute("deleted", true);
						rows.deletedRows[rows.deletedRows.length] = pr.remove(false);
						gs._removeChange("DeletedRows", pr);
						delete gs.SelectedRows[pr.Element.id];
					}
					else
					{
						if (pr.Rows.Band.AddNewRowVisible != 1)
							pr.Element.childNodes[0].childNodes[0].style.display = "none";
					}
					pr.Expandable = false; 
				}
				pr = pr.ParentRow;
			}
			if (this.Node && !gs.isDeletingSelected)
				rows.reIndex(this.getIndex(true));
			if (ar == this)
				this.Band.Grid.setActiveRow(null);
			else
			{
				var ac = this.Band.Grid.getActiveCell();
				if (ac && ac.Row == this)
					this.Band.Grid.setActiveCell(null);
			}
		}
		else
		{
			gs._recordChange("DeletedRows", this);
			igtbl_needPostBack(this.gridId);
		}

		
		if (prevAdded && this.Band.SortedColumns.length <= 0)
			this._Changes["DeletedRows"].setFireEvent(false);
		gs._calculateStationaryHeader();
		
		if (ig_csom.IsIE)
			gs.alignDivs();
		
		if (!skipRowRecalc) gs._recalcRowNumbers();
		
		
		if (ig_csom.IsFireFox && gs.MainGrid.style.height.indexOf("px") == -1)
		{
			gs._initedFF = false;
			gs._initFF();
		}

		igtbl_fireEvent(this.gridId, gs.Events.AfterRowDeleted, "(\"" + this.gridId + "\",\"" + rowId + "\");");
		if (gs.LoadOnDemand == 3)
			gs.NeedPostBack = false;
	}
	return del;
},
"getLeft",
function (offsetElement)
{
	return igtbl_getLeftPos(igtbl_getElemVis(this.Element.cells, igtbl_getBandFAC(this.gridId, this.Element)), true, offsetElement);
},
"getTop",
function (offsetElement)
{
	var t = igtbl_getTopPos(this.Element, true, offsetElement);
	return t;
},
"editRow",
function (force)
{
	
	var au = igtbl_getAllowUpdate(this.gridId, this.Band.Index);
	if (igtbl_currentEditTempl != null || !force && au != 1 && au != 3 || this.IsAddNewRow || this.IsFilterRow)
		return;
	var editTempl = igtbl_getElementById(this.Band.RowTemplate);
	if (!editTempl)
		return;
	
	var tPan = this.Band.transPanel;
	if (tPan == null && ig_csom.IsIEWin)
	{
		this.Band.transPanel = tPan = ig_csom.createTransparentPanel();
		if (tPan)
		{
			editTempl.parentNode.insertBefore(tPan.Element, editTempl);
			tPan.Element.style.zIndex = igtbl_parseInt(editTempl.style.zIndex) - 1;
		}
	}
	var gridObj = igtbl_getGridById(this.gridId);
	
	gridObj.Element.setAttribute("noOnResize", true);
	window.setTimeout("igtbl_clearNoOnResize('" + this.gridId + "')", 100);
	if (igtbl_fireEvent(this.gridId, gridObj.Events.BeforeRowTemplateOpen, "(\"" + this.gridId + "\",\"" + this.Element.id + "\",\"" + this.Band.RowTemplate + "\")"))
		return;
	try
	{
		if (editTempl.style.filter != null && this.Band.ExpandEffects)
		{
			var ee = this.Band.ExpandEffects;
			if (ee.EffectType != 'NotSet')
			{
				editTempl.style.filter = "progid:DXImageTransform.Microsoft." + ee.EffectType + "(duration=" + ee.Duration / 1000 + ");"
				if (ee.ShadowWidth > 0)
					editTempl.style.filter += " progid:DXImageTransform.Microsoft.Shadow(Direction=135, Strength=" + ee.ShadowWidth + ",color=" + ee.ShadowColor + ");"
				if (ee.Opacity < 100)
					editTempl.style.filter += " progid:DXImageTransform.Microsoft.Alpha(Opacity=" + ee.Opacity + ");"
				if (editTempl.filters[0] != null)
					editTempl.filters[0].apply();
				if (editTempl.filters[0] != null)
					editTempl.filters[0].play();
			}
			else
			{
				if (ee.ShadowWidth > 0)
					editTempl.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.Shadow(Direction=135, Strength=" + ee.ShadowWidth + ",ee.Color=" + ee.ShadowColor + ");"
				if (ee.Opacity < 100)
					editTempl.runtimeStyle.filter += " progid:DXImageTransform.Microsoft.Alpha(Opacity=" + ee.Opacity + ");"
			}
		}
	}
	catch (ex) { }
	editTempl.style.display = "";
	
	editTempl.style.visibility = "hidden";
	if (!editTempl.style.width)
		editTempl.style.width = editTempl.offsetWidth;
	if (!editTempl.style.height)
		editTempl.style.height = editTempl.offsetHeight;
	editTempl.setAttribute("noHide", true);
	var fc = igtbl_getElemVis(this.Element.cells, igtbl_getBandFAC(this.gridId, this.Element));
	editTempl.style.left = igtbl_getRelativePos(this.gridId, fc, "Left");
	var tw = igtbl_clientWidth(editTempl);
	var bw = gridObj.IsXHTML ? document.documentElement.clientWidth : document.body.clientWidth; 
	var gdw = gridObj.Element.parentNode.scrollLeft;
	
	if (gridObj.IsXHTML)
	{
		var leftVal = gridObj.MainGrid.offsetLeft + fc.offsetLeft - gridObj.DivElement.scrollLeft;
		if (leftVal < 0) leftVal = gridObj.MainGrid.offsetLeft;
		editTempl.style.left = leftVal + "px";
	}
	else
		editTempl.style.left = editTempl.offsetLeft + gdw;
	if (editTempl.offsetLeft + tw - igtbl_getBodyScrollLeft() > bw)
		if (bw - tw + igtbl_getBodyScrollLeft() - gdw > 0)
			editTempl.style.left = bw - tw + igtbl_getBodyScrollLeft() - gdw;
		else
			editTempl.style.left = 0;
	var th = igtbl_clientHeight(editTempl);
	var bh = gridObj.IsXHTML ? document.documentElement.clientHeight : document.body.clientHeight; 
	
	if (gridObj.IsXHTML)
	{
		var elemAbsBounds = igtbl_getAbsBounds(this.Element, gridObj, true);
		
		var topVal = elemAbsBounds.y;
		
		topVal += elemAbsBounds.h;
		if (!ig_shared.IsIEStandards)
		{
			var marginTop = parseInt(ig_shared.getRuntimeStyle(document.body).marginTop, 10);
			if (!isNaN(marginTop))
				topVal -= marginTop;
		}
		editTempl.style.top = topVal + "px";
	}
	else
		editTempl.style.top = igtbl_getRelativePos(this.gridId, fc, "Top") + this.Element.offsetHeight + "px";
	if (editTempl.offsetTop + th - igtbl_getBodyScrollTop() > bh)
		if (bh - th + igtbl_getBodyScrollTop() > 0)
			editTempl.style.top = bh - th + igtbl_getBodyScrollTop() + "px";
		else
			editTempl.style.top = "0px";
	
	if (tPan)
	{
		
		
		
		tPan.setPosition(editTempl.style.top, editTempl.style.left, editTempl.style.width, editTempl.style.height);
		tPan.show();
		
		var z = gridObj._getZ(10000, 1);
		editTempl.style.zIndex = z;
		tPan.Element.style.zIndex = z;
	}
	editTempl.setAttribute("editRow", this.Element.id);
	igtbl_fillEditTemplate(this, editTempl.childNodes);
	
	editTempl.style.visibility = "visible";
	if (igtbl_focusedElement && igtbl_isVisible(igtbl_focusedElement))
	{
		igtbl_focusedElement.focus();
		if (igtbl_focusedElement.select)
			igtbl_focusedElement.select();
		igtbl_focusedElement = null;
	}
	igtbl_currentEditTempl = this.Band.RowTemplate;
	igtbl_oldMouseDown = igtbl_addEventListener(document, "mousedown", igtbl_gRowEditMouseDown, false);
	igtbl_justAssigned = true;
	window.setTimeout(igtbl_resetJustAssigned, 100);
	editTempl.removeAttribute("noHide");
	igtbl_fireEvent(this.gridId, gridObj.Events.AfterRowTemplateOpen, "(\"" + this.gridId + "\",\"" + this.Element.id + "\")");
},
"endEditRow",
function (saveChanges)
{
	if (arguments.length == 0 || typeof (saveChanges) == "undefined")
		saveChanges = false;
	var gs = igtbl_getGridById(this.gridId);
	var editTempl = igtbl_getElementById(this.Band.RowTemplate);
	if (!editTempl || editTempl.style.display != "")
		return;
	if (editTempl.getAttribute("noHide"))
		return;
	if (igtbl_fireEvent(this.gridId, gs.Events.BeforeRowTemplateClose, "(\"" + this.gridId + "\",\"" + this.Element.id + "\"," + saveChanges.toString() + ")"))
		return;
	editTempl.style.display = "none";
	
	if (this.Band.transPanel)
		this.Band.transPanel.hide();
	igtbl_currentEditTempl = null;
	igtbl_removeEventListener(document, "mousedown", igtbl_gRowEditMouseDown, igtbl_oldMouseDown, false);
	igtbl_oldMouseDown = null;
	if (saveChanges)
		igtbl_unloadEditTemplate(this, editTempl.childNodes);
	igtbl_fireEvent(this.gridId, gs.Events.AfterRowTemplateClose, "(\"" + this.gridId + "\",\"" + this.Element.id + "\"," + saveChanges.toString() + ")");
	if (gs.NeedPostBack)
		igtbl_doPostBack(gs.Id);

	
	var rowTemplate = igtbl_srcElement(igtbl_getGridById(this.gridId).event)
	while (rowTemplate != null && rowTemplate.id != this.Band.RowTemplate)
		rowTemplate = rowTemplate.parentNode;
	if (rowTemplate)
		igtbl_activate(this.gridId);

},
"getHidden",
function ()
{
	return (this.Element.style.display == "none");
},
"setHidden",
function (h)
{
	this.Element.style.display = (h ? "none" : "");
	igtbl_browserWorkarounds.ieBorderCollapseArtifacts(this, h);

	if (this.getExpanded())
		this.setExpanded(false);
	var g = this.Band.Grid;
	if (g.UseFixedHeaders)
	{
		var drs = null;
		var row = this.Element;
		var i = 0;
		while (i < row.cells.length && (!row.cells[i].firstChild || row.cells[i].firstChild.id != g.Id + "_drs")) i++;
		if (i < row.cells.length)
		{
			var td = row.cells[i];
			drs = td.firstChild;
		}
		if (drs)
			drs.style.display = (h ? "none" : "");
	}
	if (this.ParentRow)
		this.ParentRow.VisChildRowsCount += (h ? -1 : 1);
	var ac = this.Band.Grid.getActiveCell();
	if (ac && ac.Row == this && h)
		this.Band.Grid.setActiveCell(null);
	else
	{
		var ar = this.Band.Grid.getActiveRow();
		if (ar && ar == this && h)
			this.Band.Grid.setActiveRow(null);
		else
			this.Band.Grid.alignGrid();
	}
	
	for (var i = 0; i < this.Band.Columns.length; i++)
	{
		if (this.Band.Columns[i].ColumnType == 7) 
		{
			var cellElement = this.getCell(i).getElement();
			cellElement.style.display = (h ? "none" : "");
		}
	}
	g._recordChange("HiddenRows", this, h.toString());
},
"find",
function (re, back
, searchHiddenColumns
)
{
	var g = this.Band.Grid;
	if (re)
		g.regExp = re;
	if (!g.regExp)
		return null;
	g.lastSearchedCell = null;
	if (back == true || back == false)
		g.backwardSearch = back;
	var cell = null;
	if (!g.backwardSearch)
	{
		cell = this.getCell(0);
		if (cell && !cell.Column.getVisible()
			&& searchHiddenColumns != true
		)
		{
			cell = cell.getNextCell();
		}
		while (cell && cell.getValue(true).search(g.regExp) == -1)
		{
			cell =
				cell.getNextCell(searchHiddenColumns);
		}
	}
	else
	{
		cell = this.getCell(this.cells.length - 1);
		if (cell && !cell.Column.getVisible()
			&& searchHiddenColumns != true
		)
		{
			cell = cell.getPrevCell();
		}
		while (cell && cell.getValue(true).search(g.regExp) == -1)
		{
			cell =
				cell.getPrevCell(searchHiddenColumns);
		}
	}
	if (cell)
		g.lastSearchedCell = cell;
	return g.lastSearchedCell;
},
"findNext",
function (re, back
, searchHiddenColumns
)
{
	var g = this.Band.Grid;
	if (!g.lastSearchedCell || g.lastSearchedCell.Row != this)
	{
		return this.find(re, back, searchHiddenColumns);
	}
	if (re)
		g.regExp = re;
	if (!g.regExp)
		return null;
	if (back == true || back == false)
		g.backwardSearch = back;
	var cell = null;
	if (!g.backwardSearch)
	{
		cell = g.lastSearchedCell.getNextCell(searchHiddenColumns);
		while (cell && cell.getValue(true).search(g.regExp) == -1)
		{
			cell = cell.getNextCell(searchHiddenColumns);
		}
	}
	else
	{
		cell = g.lastSearchedCell.getPrevCell(searchHiddenColumns);
		while (cell && cell.getValue(true).search(g.regExp) == -1)
		{
			cell = cell.getPrevCell(searchHiddenColumns);
		}
	}
	if (cell)
		g.lastSearchedCell = cell;
	else
		g.lastSearchedCell = null;
	return g.lastSearchedCell;
},
"setSelectedRowImg",
function (hide)
{
	var gs = this.Band.Grid;
	if (this.Band.AllowRowNumbering >= 2 || this.IsAddNewRow)
		return;
	var row = this.Element;
	if (gs.currentTriImg != null)
	{
		gs._lastSelectedRow = null;
		var imgObj;
		imgObj = document.createElement("img");
		imgObj.setAttribute("imgType", "blank");
		imgObj.border = "0";
		if (gs.RowLabelBlankImage)
			imgObj.src = gs.RowLabelBlankImage;
		else
		{
			imgObj.src = gs.BlankImage;
			imgObj.style.visibility = "hidden";
		}
		gs.currentTriImg.parentNode.appendChild(imgObj);
		gs.currentTriImg.parentNode.removeChild(gs.currentTriImg);
		gs.currentTriImg = null;
	}
	if (!hide && row && !row.getAttribute("deleted") && !row.getAttribute("groupRow") && this.Band.getRowSelectors() != 2)
	{
		var rl = row.cells[this.Band.firstActiveCell - 1];
		if (rl.childNodes.length == 0 || !(rl.childNodes[0].tagName == "IMG" && rl.childNodes[0].getAttribute("imgType") == "newRow"))
		{
			var imgObj;
			var bIndex = this.Band.Index;
			imgObj = document.createElement("img");
			imgObj.src = igtbl_getCurrentRowImage(this.gridId, bIndex);
			imgObj.border = "0";
			imgObj.setAttribute("imgType", "tri");
			if (gs.Section508Compliant)
			{
				var altT = igtbl_getCurrentRowAltText(this.gridId, bIndex);
				if (altT) imgObj.setAttribute("alt", altT);
			}
			var cell = row.cells[this.Band.firstActiveCell - 1];
			cell.innerHTML = "";
			cell.appendChild(imgObj);
			gs.currentTriImg = imgObj;
		}
		gs._lastSelectedRow = row.id;
	}
},
"renderActive",
function (render)
{
	var g = this.Band.Grid;
	var ao = g.Activation;
	if (!ao.AllowActivation)
		return;
	if (typeof (render) == "undefined") render = true;
	if (this.GroupByRow)
	{
		var fr = this.getFirstRow();
		fr = fr.firstChild;
		if (render)
		{
			igtbl_setClassName(fr, ao._cssClass);
			igtbl_setClassName(fr, ao._cssClassL);
			igtbl_setClassName(fr, ao._cssClassR);
		}
		else
		{
			igtbl_removeClassName(fr, ao._cssClassR);
			igtbl_removeClassName(fr, ao._cssClassL);
			igtbl_removeClassName(fr, ao._cssClass);
		}
	}
	else
	{
		{
			var i = 0;
			var els = this.getCellElements();
			if (!els || els.length == 0) return;
			var cell = els[i];
			
			
			while (cell && this.Band.Columns[i].getHidden() && i < this.cells.length)
				cell = els[++i];
			if (i < els.length)
			{
				if (render)
					igtbl_setClassName(cell, ao._cssClassL);
				else
					igtbl_removeClassName(cell, ao._cssClassL);
			}
			for (i = 0; i < els.length; i++)
			{
				cell = els[i];
				if (render)
					igtbl_setClassName(cell, ao._cssClass);
				else
					igtbl_removeClassName(cell, ao._cssClass);
			}
			i = els.length - 1;
			cell = els[i];
			
			
			while (cell && this.Band.Columns[i].getHidden() && i >= 0)
				cell = els[--i];
			if (i >= 0)
			{
				if (render)
					igtbl_setClassName(cell, ao._cssClassR);
				else
					igtbl_removeClassName(cell, ao._cssClassR);
			}
			igtbl_dispose(els);
		}
	}
},
"select",
function (selFlag, fireEvent)
{
	var gs = this.Band.Grid;
	if (this.Band.getSelectTypeRow() < 2 || this.getSelected() == selFlag)
		return false;
	if (gs._exitEditCancel || gs._noCellChange)
		return false;
	if (fireEvent != false)
		if (igtbl_fireEvent(gs.Id, gs.Events.BeforeSelectChange, "(\"" + gs.Id + "\",\"" + this.Element.id + "\")") == true)
			return false;
	if (!this.GroupByRow)
	{
		var style = null;
		if (selFlag != false)
			style = this.Band.getSelClass();
		if (this.Band._optSelectRow)
		{
			if (style)
			{
				var aoStyle = "";
				if (gs.oActiveRow == this)
				{
					var styles = this.Element.className.split(" ");
					aoStyle = " " + styles[styles.length - 1];
					styles = styles.slice(0, styles.length - 1);
					this.Element.className = styles.join(" ");
					if (this.nfElement)
						this.nfElement.className = this.Element.className;
				}
				this.Element.className += " " + style + aoStyle;
				if (this.nfElement)
					this.nfElement.className += " " + style + aoStyle;
			}
			else
			{

				
				var styles = this.Element.className;
				style = this.Band.getSelClass();
				if (style && styles.indexOf(style) > -1)
					this.Element.className = styles.replace(style, "");

				if (this.nfElement)
					this.nfElement.className = this.Element.className;
			}
		}
		else if (!this.Band._selClassDiffer)
		{
			var els = this.getCellElements();
			for (var i = 0; i < els.length; i++)
				igtbl_changeStyle(gs.Id, els[i], style);
		}
		if (this.Band._selClassDiffer)
			for (var i = 0; i < this.cells.length; i++)
				this.getCell(i).selectCell(selFlag);
	}
	else if (selFlag != false)
		igtbl_changeStyle(gs.Id, this.FirstRow.cells[0], this.Band.getSelGroupByRowClass());
	else
		igtbl_changeStyle(gs.Id, this.FirstRow.cells[0], null);
	if (selFlag != false)
		gs._recordChange("SelectedRows", this, gs.GridIsLoaded.toString());
	
	else if (gs.SelectedRows[this.Element.id] || gs._containsChange("SelectedRows", this))
		gs._removeChange("SelectedRows", this);
	if (fireEvent != false)
	{
		var gsNPB = gs.NeedPostBack;
		igtbl_fireEvent(gs.Id, gs.Events.AfterSelectChange, "(\"" + gs.Id + "\",\"" + this.Element.id + "\");");
		if (!gsNPB && !(gs.Events.AfterSelectChange[1] & 2))
			gs.NeedPostBack = false;
		if (gs.NeedPostBack)
			igtbl_moveBackPostField(gs.Id, "SelectedRows");
	}
	return true;
},
"processUpdateRow",
function ()
{
	return this._processUpdateRow();
},
"_processUpdateRow",
function ()
{
	var result = false;
	var g = this.Band.Grid;
	if (!this._dataChanged || typeof (g.Events.BeforeRowUpdate) == "undefined")
		return result;
	for (var i = 0; (this._dataChanged & 2) && i < this.cells.length; i++)
		if (typeof (this.getCell(i)._oldValue) != "undefined")
			break;
	if (i < this.cells.length)
	{
		g.QueryString = "";
		result = g.fireEvent(g.Events.BeforeRowUpdate, [g.Id, this.Element.id]);
		if ((this._dataChanged & 2))
			for (; i < this.cells.length; i++)
			{
				var cell = this.getCell(i);
				if (typeof (cell._oldValue) != "undefined")
				{
					if (result)
						cell.setValue(cell._oldValue, false);
					else if (g.LoadOnDemand == 3)
						g.QueryString += (g.QueryString && g.QueryString.length > 0 ? "\x04" : "") + "UpdateCell\x06" + cell.Column.Key + "\x02" + igtbl_escape(cell.getValue() == null ? cell.Column.getNullText() : igtbl_dateToString(cell.getValue())); 
				}
			}
		if (!result)
		{
			if (g.LoadOnDemand == 3 && (g.Events.AfterRowUpdate[1] || g.Events.XmlHTTPResponse[1]))
				g.invokeXmlHttpRequest(g.eReqType.UpdateRow, this);
			else
			{
				g.fireEvent(g.Events.AfterRowUpdate, [g.Id, this.Element.id]);
				if (g.NeedPostBack)
					igtbl_doPostBack(g.Id);
			}
		}
		this._dataChanged = 0;
	}
	return result;
},
"_getRowNumber",
function ()
{
	var index = null;
	var band = this.Band;
	var oLbl;
	if (this.Id && this.Id.indexOf(this.gridId + "_r_") == 0)
	{
		var oLbl = igtbl_getElementById(this.Id.replace("_r_", "_l_"));
	}
	if (!oLbl) 
	{
		
		var oLbl = igtbl_getElementById(this.gridId + "_l_" + this.getLevel(true));
	}
	if (band.getRowSelectors() < 2 && band.AllowRowNumbering > 1 && oLbl)
	{
		index = igtbl_getInnerText(oLbl);
	}
	return index;
},
"_setRowNumber",
function (value)
{
	var band = this.Band;
	var oRS = band.firstActiveCell - 1;
	var oLbl = -1;
	if (this.Element)
		oLbl = this.Element.childNodes[oRS];
	if (band.getRowSelectors() < 2 && band.AllowRowNumbering > 1)
	{
		if (this.Node) this.Node.setAttribute(igtbl_litPrefix + "rowNumber", value);
		if (oLbl) oLbl.innerHTML = value;
		return value;
	}
	else
		return -1;
},

"_generateHierarchicalDataKey",
function ()
{
	var currentRow = this;
	var dataKey = "";
	while (currentRow)
	{
		if (currentRow.DataKey)
			dataKey = currentRow.DataKey + dataKey;

		if (currentRow.ParentRow)
			dataKey = '\x08' + dataKey;

		currentRow = currentRow.ParentRow;
	}
	return dataKey;
},
"_generateUpdateRowSemaphore",
function (clear)
{
	var cellInfo = "";
	for (var j = 0; j < this.cells.length; j++)
	{
		var cell = this.getCell(j);
		if (cell)
		{
			if (typeof (cell.getOldValue()) != "undefined")
			{
				var oldValue = cell.getOldValue();
				oldValue = igtbl_dateToString(oldValue); 
				cellInfo += (cellInfo.length > 0 ? "\x03" : "") + igtbl_escape(cell.Column.Key + "\x05" + cell.Column.Index + "\x05" + oldValue);
				if (clear)
					delete cell._oldValue;
			}
			else
				cellInfo += (cellInfo.length > 0 ? "\x03" : "") + igtbl_escape(cell.Column.Key + "\x05" + cell.Column.Index + "\x05" + (cell.getValue() == null ? cell.Column.getNullText() : igtbl_dateToString(cell.getValue()))); 
		}
	}
	return cellInfo;
},
"_generateSqlWhere",
function (dataKeyField, value)
{
	if (!dataKeyField) return;
	var sqlWhere = "";
	var dkfArray = dataKeyField.split(",");
	var valArray = value.split('\x07');
	for (var i = 0; i < dkfArray.length; i++)
	{
		var dk = igtbl_string.trim(dkfArray[i]); 
		if (i > 0)
			sqlWhere += " AND ";
		if (this.Band.getColumnFromKey(dk).DataType == 8)
			sqlWhere += dk + "='" + valArray[i] + "'";
		else
			sqlWhere += dk + "=" + valArray[i];
	}
	if (this.Band._sqlWhere)
	{
		if (sqlWhere)
			sqlWhere += " AND ";
		sqlWhere += this.Band._sqlWhere;
	}
	return sqlWhere;
},
"getChildRows",
function ()
{
	var rows = null;
	row = this.Element;
	if (row.getAttribute("groupRow"))
		rows = row.childNodes[0].childNodes[0].childNodes[0].rows[1].childNodes[0].childNodes[0].tBodies[0].rows;
	else
	{
		if (row.nextSibling && row.nextSibling.getAttribute("hiddenRow"))
		{
			if (this.Band.IndentationType == 2)
				rows = row.nextSibling.firstChild.firstChild.tBodies[0].rows;
			else
				rows = row.nextSibling.childNodes[this.Band.firstActiveCell].firstChild.tBodies[0].rows;
		}
		else
			rows = null;
	}
	return rows;
}
,
"getCellElements",
function (flCells)
{
	var re = this.Element, nfr = false;
	
	if (!re || !re.cells.length || this.GroupByRow) return;
	var result = new Array();
	var start = 0;
	if (this.Band.Grid.Bands.length > 1) start++;
	if (this.Band.getRowSelectors() < 2) start++;
	for (var i = start; i < re.cells.length; i++)
	{
		if (this.Band.Grid.UseFixedHeaders && !nfr)
		{
			if (re.cells[i].childNodes.length > 0 && re.cells[i].firstChild.tagName == "DIV" && re.cells[i].firstChild.id.substr(re.cells[i].firstChild.id.length - 4) == "_drs")
			{
				re = re.cells[i].firstChild.firstChild.childNodes[1].rows[0];
				i = 0;
				nfr = true;
			}
		}
		if (flCells)
		{
			if (re.cells[i].offsetHeight > 0)
			{
				result[result.length] = re.cells[i];
				break;
			}
		}
		else if (i < re.cells.length)
			result[result.length] = re.cells[i];
	}
	if (flCells)
	{
		if (this.Band.Grid.UseFixedHeaders && !nfr)
		{
			re = re.cells[re.cells.length - 1].firstChild.firstChild.childNodes[1].rows[0];
			i = 0;
		}
		for (var j = re.cells.length - 1; j >= i; j--)
			if (re.cells[j].offsetHeight > 0)
			{
				result[result.length] = re.cells[j];
				break;
			}
	}
	return result;
},
"getRowSelectorElement",
function ()
{
	if (!this.GroupByRow && this.Band.getRowSelectors() != 2)
		return this.Element.cells[this.Band.firstActiveCell - 1];
	return null;
},
"getExpansionElement",
function ()
{
	if (this.GroupByRow) return null;
	if (this.Band.getRowSelectors() != 2)
	{
		if (this.Band.firstActiveCell > 1)
			return this.Element.cells[0];
	}
	else if (this.Band.firstActiveCell > 0)
		return this.Element.cells[0];
	return null;
},
"_evaluateFilters",
function ()
{
	
	if (this.Band.ApplyOnAdd == 0) return;

	
	var oFilterConditions;

	if (this.Band.Index == 0 && this.Band.GroupCount == 0)
	{
		oFilterConditions = this.Band._filterPanels;
	}
	else if ((this.Band.Columns[0].RowFilterMode == 1 && this.Band.GroupCount == 0) ||
			 (this.Band.Index == 0 && this.Band.GroupCount > 0 && this.Band.Grid.StatHeader))
	{
		
		oFilterConditions = this.Band._filterPanels;
	}
	else 
	{
		
		var siblingRows = this.OwnerCollection;
		oFilterConditions = this.Band._filterPanels[siblingRows.Element.parentNode.id];
	}
	if (oFilterConditions)
	{
		this.getCell(0).Column._evaluateFilters(this, oFilterConditions, this.Band);
	}
}
, "dispose",
function ()
{
	igtbl_cleanRow(this);
	igtbl_dispose(this);
}
];
for (var i = 0; i < igtbl_ptsRow.length; i += 2)
	igtbl_Row.prototype[igtbl_ptsRow[i]] = igtbl_ptsRow[i + 1];
igtbl_Row.prototype["getRowNumber"] = igtbl_Row.prototype["_getRowNumber"];

// Add new row object. Inherited from the row object.
igtbl_AddNewRow.prototype = new igtbl_Row();
igtbl_AddNewRow.prototype.constructor = igtbl_AddNewRow;
igtbl_AddNewRow.base = igtbl_Row.prototype;
function igtbl_AddNewRow(element, rows)
{
	if (arguments.length > 0)
		this.init(element, rows);
}
var igtbl_ptsAddNewRow = [
"init",
function(element, rows)
{
	this.IsAddNewRow = true;
	igtbl_AddNewRow.base.init.apply(this, [element, null, rows, -1]);
	this.Type = "addNewRow";
},
"commit",
function()
{
	if (this._dataChanged)
	{
		this._dataChanged = 0;
		var ac = this.Band.Grid.oActiveCell, ar = this.Band.Grid.oActiveRow;
		var newRow = igtbl_rowsAddNew(this.gridId, this.ParentRow, this);
		if (newRow)
		{
			for (var i = 0; i < this.Band.Columns.length; i++)
			{
				var cellObj = this.getCell(i);
				cellObj.setValue(cellObj.Column.getValueFromString(cellObj.Column.DefaultValue));
			}
			this._dataChanged = 0;
			if (ac && ac.Row.IsAddNewRow)
			{
				var acSel = ac.getSelected();
				if (acSel)
					ac.setSelected(false);
				var nac = newRow.getCell(ac.Column.Index);
				nac.activate();
				if (acSel)
					nac.setSelected();
			}
			else if (ar.IsAddNewRow)
			{
				var arSel = ar.getSelected();
				if (arSel)
					ar.setSelected(false);
				newRow.activate();
				if (arSel)
					newRow.setSelected();
			}
			newRow.processUpdateRow();
		}
		return newRow;
	}
	return null;
},
"isFixed",
function()
{
	return this.isFixedTop() || this.isFixedBottom();
},
"isFixedTop",
function()
{
	return this.Band.Index == 0 && this.Band.Grid.StatHeader != null && this.Band.AddNewRowView == 1;
},
"isFixedBottom",
function()
{
	return this.Band.Index == 0 && this.Band.Grid.StatFooter != null && this.Band.AddNewRowView == 2;
}
];
for (var i = 0; i < igtbl_ptsAddNewRow.length; i += 2)
	igtbl_AddNewRow.prototype[igtbl_ptsAddNewRow[i]] = igtbl_ptsAddNewRow[i + 1];


igtbl_FilterRow.prototype = new igtbl_Row();
igtbl_FilterRow.prototype.constructor = igtbl_FilterRow;
igtbl_FilterRow.base = igtbl_Row.prototype;
function igtbl_FilterRow(element, rows)
{
	if (arguments.length > 0)
		this.init(element, rows);
}
var igtbl_ptsFilterRow = [
"init",
function(element, rows)
{
	this.IsFilterRow = true;
	igtbl_FilterRow.base.init.apply(this, [element, null, rows, -1]);
	this.Type = "filterRow";
	this._fetchDelay = 1000;
},
"isFixed",
function()
{
	return this.isFixedTop() || this.isFixedBottom();
},
"isFixedTop",
function()
{
	return this.Band.Index == 0 && this.Band.Grid.StatHeader != null && this.Band.FilterRowView == 1;
},
"isFixedBottom",
function()
{
	return this.Band.Index == 0 && this.Band.Grid.StatFooter != null && this.Band.FilterRowView == 2;
},
"getFetchDelay",
function()
{
	return this._fetchDelay;
},
"setFetchDelay",
function(delay)
{
	this._fetchDelay = delay;
}
];
for (var i = 0; i < igtbl_ptsFilterRow.length; i += 2)
	igtbl_FilterRow.prototype[igtbl_ptsFilterRow[i]] = igtbl_ptsFilterRow[i + 1];

// Rows collection object 
igtbl_Rows.prototype = new igtbl_WebObject();
igtbl_Rows.prototype.constructor = igtbl_Rows;
igtbl_Rows.base = igtbl_WebObject.prototype;
function igtbl_Rows(node, band, parentRow)
{
	if (arguments.length > 0)
	{
		var element = null;
		if (band.Index == 0 && !parentRow)
			element = band.Grid.Element.tBodies[0];
		else if (parentRow && parentRow.Element)
		{
			if (parentRow.GroupByRow)
			{
				var tb = parentRow.Element.childNodes[0].childNodes[0].tBodies[0];
				if (tb.childNodes.length > 1)
					this.Element = tb.childNodes[1].childNodes[0].childNodes[0].tBodies[0];
			}
			else if (parentRow.Element.nextSibling && parentRow.Element.nextSibling.getAttribute("hiddenRow"))
				this.Element = parentRow.Element.nextSibling.childNodes[parentRow.Band.IndentationType == 2 ? 0 : parentRow.Band.firstActiveCell].childNodes[0].tBodies[0];
		}
		this.init(element, node, band, parentRow);
	}
}
var igtbl_ptsRows = [
"init",
function(element, node, band, parentRow)
{
	igtbl_Rows.base.init.apply(this, ["rows", element, node]);

	this.Grid = band.Grid;
	this.Band = band;
	this.ParentRow = parentRow;
	this.rows = new Array();
	this.length = 0;
	if (node)
	{
		this.SelectedNodes = node.selectNodes("R");
		if (!this.SelectedNodes.length)
		{
			this.SelectedNodes = node.selectNodes("Group");
			if (this.SelectedNodes.length)
				this.GroupColId = this.SelectedNodes[0].getAttribute(igtbl_litPrefix + "groupRow");
		}
		this.length = this.SelectedNodes.length;
	}
	else
	{
		if (parentRow)
			this.length = parentRow.ChildRowsCount;
		else
		{
			this.length = this.Element.childNodes.length;
			for (var i = 0; i < this.Element.childNodes.length; i++)
			{
				var r = this.Element.childNodes[i];
				if (r.getAttribute("hiddenRow")
				|| r.getAttribute("addNewRow")
                || r.getAttribute("filterRow")
				)
					this.length--;
			}
		}
	}
	if (this.Element)
		this.Element.Object = this;
	this.lastRowId = "";
	if (!this.ParentRow || !this.ParentRow.GroupByRow)
	{
		
		var anr = igtbl_getElementById(this.Grid.Id + "_anr" + (this.ParentRow ? "_" + this.ParentRow.getLevel(true, true) : ""));
		if (anr)
			this.AddNewRow = new igtbl_AddNewRow(anr, this);
	}
	
	var filterRow = igtbl_getElementById(this.Grid.Id + "_flr" + (this.ParentRow ? "_" + this.ParentRow.getLevel(true) : ""));
	if (filterRow)
	{
		this.FilterRow = new igtbl_FilterRow(filterRow, this);
	}
},
"reapplyRowStyles",
function()
{
	
	var alternateStyle = this.Band.getRowAltClassName();
	var rowStyle = this.Band.getRowStyleClassName();
	
	var useAlternateRowStyle = (alternateStyle != "") && (alternateStyle != rowStyle);
	
	if (!useAlternateRowStyle) return;
	
	var altRow = false;
	for (var i = 0; i < this.length; i++)
	{
		var curRow = this.getRow(i);
		
		if (curRow.getHidden())
			continue;
		var className = "";
		
		if (useAlternateRowStyle)
			className = altRow ? alternateStyle : rowStyle;
		if (className && !curRow.GroupByRow)
		{
			var rowE = curRow.Element;
			
			if (curRow.Band._optSelectRow)
			{
				
				if (altRow)
					igtbl_dom.css.replaceClass(rowE, rowStyle, alternateStyle);
				else
					igtbl_dom.css.removeClass(rowE, alternateStyle);
			}
			else
			{
				
				
				var j = curRow.Band.firstActiveCell;
				var colNo = 0;
				var rowElem = curRow.Element;
				var nonFixed = false;
				while (j < rowElem.cells.length)
				{
					var col = curRow.Band.Columns[colNo];
					if (col.getFixed() === false && !nonFixed)
					{
						j = 0;
						rowElem = curRow.nfElement;
						nonFixed = true;
					}
					var e = rowElem.cells[j];
					if (e)
					{
						
						if (useAlternateRowStyle)
						{
							var colCssClass = (!altRow) ? col.CssClass : col._AltCssClass;
							colCssClass = colCssClass && className != colCssClass ? " " + colCssClass : "";
							if (e.className != className + colCssClass)
								e.className = className + colCssClass;
						}
						else
							e.className = className + (col.CssClass ? " " + col.CssClass : "");
					}
					j++;
					colNo++;
				}
			}
		}
		if (useAlternateRowStyle)
			altRow = !altRow;
	}
},
"getRow",
function(rowNo, rowElement)
{
	if (typeof (rowNo) != "number")
	{
		rowNo = parseInt(rowNo);
		if (isNaN(rowNo))
			return null;
	}
	if (rowNo < 0 || !this.Element || !this.Element.childNodes)
		return null;
	if (rowNo >= this.length)
	{
		if (this.length > this.rows.length)
			this.rows[this.length - 1] = null;
		return null;
	}
	if (rowNo >= this.rows.length)
		this.rows[this.length - 1] = null;
	if (!this.rows[rowNo])
	{
		var row = rowElement;
		if (!row)
		{
			var cr = 0;
			if (this.Grid.Bands.length == 1 && !this.Grid.Bands[0].IsGrouped)
			{
				var adj = 0;
				if (!igtbl_getElementById(this.Grid.Id + "_hdiv") && this.Grid.Bands[0].AddNewRowVisible == 1 && this.Grid.Bands[0].AddNewRowView == 1)
					adj++;
				
				if (this.Grid.Bands[0].AllowRowFiltering >= 2 && this.Grid.Bands[0].FilterUIType == 1)
				{
					
					if (!igtbl_getElementById(this.Grid.Id + "_hdiv"))
						adj++;
					else
					{
						var filterRow = this.FilterRow;
						if (filterRow && filterRow.Id)
						{
							var filterRowElm = document.getElementById(filterRow.Id);
							if (filterRowElm && filterRowElm.parentNode && filterRowElm.parentNode.parentNode &&
								filterRowElm.parentNode.parentNode.parentNode &&
								filterRowElm.parentNode.parentNode.parentNode.id != (this.Grid.Id + "_hdiv"))
								adj++;
						}
					}
				}
				row = this.Element.childNodes[rowNo + adj];
			}
			else
				for (var i = 0; i < this.Element.childNodes.length; i++)
			{
				var r = this.Element.childNodes[i];
				if (!r.getAttribute("hiddenRow")
						&& !r.getAttribute("addNewRow")
                        && !r.getAttribute("filterRow")
					)
				{
					if (rowNo == cr)
					{
						row = this.Element.childNodes[i];
						break;
					}
					cr++;
				}
			}
		}
		if (!row)
			return null;
		this.rows[rowNo] = new igtbl_Row(row, (this.Node ? this.SelectedNodes[rowNo] : null), this, rowNo);
	}
	return this.rows[rowNo];
},

"getRowById",
function(rowId)
{
	for (var i = 0; i < this.length; i++)
	{
		var row = this.getRow(i);
		if (row.Element.id == rowId)
			return row;
	}
	return null;
},
"getColumn",
function(colNo)
{
	var thead = this.Element.previousSibling;
	if (!thead || thead.tagName != "THEAD")
		return;
	var j = -1;
	var metFixed = false;
	for (var i = 0; i < this.Band.Columns.length; i++)
	{
		var column = this.Band.Columns[i];
		if (column.hasCells())
			j++;
		if (column.getFixed() === false && !metFixed)
		{
			metFixed = true;
			j = 0;
		}
		if (i == colNo)
			break;
	}
	if (j < 0 || j >= this.Band.Columns.length)
		return null;
	if (this.Band.Columns[i].getFixed() === false)
	{
		thead = thead.firstChild.cells[thead.firstChild.cells.length - 1];
		return thead.firstChild.firstChild.rows[0].cells[j];
	}
	return thead.firstChild.cells[j + this.Band.firstActiveCell];
},
"indexOf",
function(row)
{
	if (row.IsAddNewRow)
		return -1;
	if (row.IsFilterRow)
		return -1;
	if (row.Node)
	{
		
		var index = parseInt(row.Node.getAttribute("i"), 10);
		if (typeof (this._getRowToStart) != "undefined")
			index -= this._getRowToStart();
		return index;
	}
	if (this.Grid.Bands.length == 1 && !this.Grid.Bands[0].IsGrouped)
	{
		var index = row.Element.sectionRowIndex;
		if (this.Band.AddNewRowVisible == 1 && this.Band.AddNewRowView == 1 && !this.Grid.StatHeader)
			index--;
		if (this.Band.FilterUIType == 1 && this.Band.FilterRowView == igtbl_featureRowView.Top && !this.Grid.StatHeader)
			index--;
		return index;
	}
	var level = -1;
	var rId = row.Element.id, rows = this.Element.rows;
	for (var i = 0; i < rows.length; i++)
	{
		var r = rows[i];
		if (!r.getAttribute("hiddenRow")
			&& !r.getAttribute("addNewRow")
            && !r.getAttribute("filterRow")
		)
			level++;
		else
			continue;
		if (r.id == rId)
			return level;
	}
	return -1;
},
"insert",
function(row, rowNo)
{
	var g = this.Grid;
	if (!row || row.OwnerCollection && row.OwnerCollection != this)
	{
		if (g.getActiveRow() == row)
			g.setActiveRow(null);
		return false;
	}
	if (!g._isSorting)
	{
		if (g.fireEvent(g.Events.BeforeRowInsert, [g.Id, (this.ParentRow ? this.ParentRow.Element.id : ""), row.Element.id, rowNo]) == true)
		{
			if (g.getActiveRow() == row)
				g.setActiveRow(null);
			return false;
		}
	}
	var row1 = this.getRow(rowNo);
	if (row1)
	{
		if (this.rows.splice)
			this.rows.splice(rowNo, 0, row);
		else
			this.rows = this.rows.slice(0, rowNo).concat(row, this.rows.slice(rowNo));
		this.Element.insertBefore(row.Element, row1.Element);
		if (row.Expandable && row.HiddenElement && !row.GroupByRow)
			this.Element.insertBefore(row.HiddenElement, row1.Element);
		if (this.Node)
		{
			
			var curNode = row.Node;
			var curIndex = igtbl_parseInt(row1.Node.getAttribute("i"));
			this.Node.insertBefore(row.Node, row1.Node);
			while (curNode && curNode.nodeName ==
			"R"
			)
			{
				curNode.setAttribute("i", curIndex++);
				curNode = curNode.nextSibling;
			}
			
			this.SelectedNodes = this.Node.selectNodes("R");
			if (!this.SelectedNodes.length)
				this.SelectedNodes = this.Node.selectNodes("Group");
		}
	}
	else
	{
		this.rows[this.rows.length] = row;
		this.Element.appendChild(row.Element);
		if (row.Expandable && row.HiddenElement && !row.GroupByRow)
			this.Element.appendChild(row.HiddenElement);
		if (this.Node)
		{
			this.Node.appendChild(row.Node);
			row.Node.setAttribute("i", this.rows.length - 1);
		}
	}
	this.length++;
	if (typeof (row._removedFrom) != "undefined")
	{
		g._removeChange("DeletedRows", row);
		g._recordChange("MoveRow", row, row._removedFrom + ":" + row.getLevel(true));

		if (row._Changes.MoveRow.length)
			row._Changes.MoveRow[row._Changes.MoveRow.length - 1].Node.setAttribute("Level", row._removedFrom);
		else
			row._Changes.MoveRow.Node.setAttribute("Level", row._removedFrom);

		delete row._removedFrom;
	}
	if (!g._isSorting)
	{
		var oldNPB = g.NeedPostBack;
		g.fireEvent(g.Events.AfterRowInsert, [g.Id, row.Element.id, rowNo]);
		if (!oldNPB && g.NeedPostBack && !g.Events.AfterRowInsert[1] & 2)
			g.NeedPostBack = false;
		if (g.NeedPostBack)
			igtbl_doPostBack(g.Id, "");
	}
	return true;
},
"remove",
function(rowNo, fireEvents)
{
	var row = this.getRow(rowNo);
	if (!row)
		return;
	if (typeof (fireEvents) == "undefined") fireEvents = true;
	if (!this.Grid._isSorting)
	{
		this.setLastRowId();
		if (fireEvents && this.Grid.fireEvent(this.Grid.Events.BeforeRowDeleted, [this.Grid.Id, row.Element.id]) == true)
			return null;
		this.Grid._recordChange("DeletedRows", row);
		row._removedFrom = row.getLevel(true);
	}
	this.Element.removeChild(row.Element);
	if (row.Expandable && row.HiddenElement && !row.GroupByRow)
		this.Element.removeChild(row.HiddenElement);
	if (row.Node)
	{
		
		var curNode = row.Node.nextSibling;
		row.Node.parentNode.removeChild(row.Node);
		while (curNode && curNode.nodeName ==
			"R"
		)
		{
			curNode.setAttribute("i", igtbl_parseInt(curNode.getAttribute("i")) - 1);
			curNode = curNode.nextSibling;
		}
		var rows = row.OwnerCollection;
		rows.SelectedNodes = rows.Node.selectNodes("R");
		if (!rows.SelectedNodes.length)
			rows.SelectedNodes = rows.Node.selectNodes("Group");
	}
	if (this.rows.splice)
		this.rows.splice(rowNo, 1);
	else
		this.rows = this.rows.slice(0, rowNo).concat(this.rows.slice(rowNo + 1));
	this.length--;
	if (fireEvents && !this.Grid._isSorting)
		this.Grid.fireEvent(this.Grid.Events.AfterRowDeleted, [this.Grid.Id, row.Element.id]);
	
	if (ig_csom.IsFireFox)
	{
		if (this.Grid.getActiveRow() === row || this.Grid.getActiveRow() == null)
		{
			this.Grid.setActiveRow(null);
			var tmp = this.Grid.Rows.getRow(0); 
			if (tmp)
			{
				this.Grid.setActiveRow(tmp);
				this.Grid.setActiveRow(null);
			}
		}
		else
		{
			var tmp = this.Grid.getActiveRow();
			this.Grid.setActiveRow(null);
			this.Grid.setActiveRow(tmp);
		}
	}
	return row;
},
"sort",
function(sortedCols)
{
	var issortch = false;
	if (!this.Grid._isSorting)
		this.Grid._isSorting = issortch = true;
	if (typeof (igtbl_clctnSort) != "undefined")
		igtbl_clctnSort.apply(this, [sortedCols]);
	if (issortch)
		delete this.Grid._isSorting;
},
"getFooterText",
function(columnKey)
{
	var tFoot;
	if (this.Band.Index == 0 && this.Grid.StatFooter)
		tFoot = this.Grid.StatFooter.Element;
	else
		tFoot = this.Element.nextSibling;
	var col = this.Band.getColumnFromKey(columnKey);
	if (tFoot && tFoot.tagName == "TFOOT" && col)
	{
		var fId = this.Grid.Id
			+ "_"
			+ "f_" + this.Band.Index + "_" + col.Index;
		for (var i = 0; i < tFoot.rows[0].childNodes.length; i++)
			if (tFoot.rows[0].childNodes[i].id == fId)
			return igtbl_getInnerText(tFoot.rows[0].childNodes[i]);
	}
	return "";
},
"setFooterText",
function(columnKey, value, useMask)
{
	var tFoot;
	if (this.Band.Index == 0 && this.Grid.StatFooter)
		tFoot = this.Grid.StatFooter.Element;
	else
		tFoot = this.Element.nextSibling;
	var col = this.Band.getColumnFromKey(columnKey);
	if (tFoot && tFoot.tagName == "TFOOT" && col)
	{
		var fId = this.Grid.Id
			+ "_"
			+ "f_" + this.Band.Index + "_" + col.Index;
		if (useMask && col.MaskDisplay)
			value = igtbl_Mask(this.Grid.Id, value.toString(), col.DataType, col.MaskDisplay);
		var foot = igtbl_getChildElementById(tFoot, fId);
		if (foot)
		{
			if (igtbl_string.trim(value) == "")
				value = "&nbsp;";
			if (foot.childNodes.length > 0 && foot.childNodes[0].tagName == "NOBR")
				value = "<nobr>" + value + "</nobr>";
			foot.innerHTML = value;
		}
	}
},
"render",
function()
{
	var strTransform = this.applyXslToNode(this.Node);
	if (strTransform)
	{
		var anId = (this.AddNewRow ? this.AddNewRow.Id : null);
		this.Element.parentNode.parentNode.appendChild(this.Grid._innerObj); // SR: _innerObj needs to be connected for script to resolve.
		this.Grid._innerObj.innerHTML = "<table style=\"table-layout:fixed;\">" + strTransform + "</table>";
		this.Element.parentNode.parentNode.removeChild(this.Grid._innerObj); // SR: disconnect temporarily
		var tbl = this.Element.parentNode;
		igtbl_replaceChild(tbl, this.Grid._innerObj.firstChild.firstChild, this.Element);
		igtbl_fixDOEXml();
		var _b = this.Band;
		var headerDiv = igtbl_getElementById(this.Grid.Id + "_hdiv");
		var footerDiv = igtbl_getElementById(this.Grid.Id + "_fdiv");
		if (this.AddNewRow)
		{
			if (_b.Index > 0 || _b.AddNewRowView == 1 && !headerDiv || _b.AddNewRowView == 2 && !footerDiv)
			{
				var anr = this.AddNewRow.Element;
				anr.parentNode.removeChild(anr);
				if (_b.AddNewRowView == 1 && tbl.tBodies[0].rows.length > 0)
					tbl.tBodies[0].insertBefore(anr, tbl.tBodies[0].rows[0]);
				else
					tbl.tBodies[0].appendChild(anr);
			}
			this.AddNewRow.Element = igtbl_getElementById(anId);
			this.AddNewRow.Element.Object = this.AddNewRow;
		}
		this.Element = tbl.tBodies[0];
		this.Element.Object = this;
		this._setupFilterRow();
		for (var i = 0; i < this.Band.Columns.length; i++)
		{
			var column = this.Band.Columns[i];
			if (column.Selected && column.hasCells())
			{
				var col = this.getColumn(i);
				if (col)
					igtbl_selColRI(this.Grid.Id, col, this.Band.Index, i);
			}
		}
		if (this.ParentRow)
		{
			this.ParentRow.ChildRowsCount = this.length;
			this.ParentRow.VisChildRowsCount = this.length;
		}
	}
},
"applyXslToNode",
function(node
)
{
	if (!node) return "";
	if (typeof (rowToStart) == "undefined")
		rowToStart = 0;
	var xslProc = this.Grid.XslProcessor;
	xslProc.input = node;
	var hasGrouped = false;
	
	if (this.SelectedNodes && this.SelectedNodes.length && this.SelectedNodes[0].nodeName == "Group")
		hasGrouped = true;
	var prL = "";
	if (this.ParentRow)
	{
		prL = this.ParentRow.Element.id.split("_");
		prL = prL.slice(1);
		prL = prL.slice(1);
		prL = prL.join("_") + "_";
	}
	if (hasGrouped)
	{
		
		if (!this.Band._wdth)
		{
			var pdng = 0; 
			if (this.Grid.get("StationaryMarginsOutlookGroupBy") != "True")
				pdng = 5;
			var wdth = 0;
			if (this.Grid.Bands.length > 0)
				wdth += 22;
			if (this.Band.getRowSelectors() == 1)
				wdth += 22;
			for (var i = 0; i < this.Band.Columns.length; i++)
				if (this.Band.Columns[i].getVisible())
			{
				var colWidth = this.Band.Columns[i].Width;
				
				if ((colWidth || colWidth === "") && typeof (colWidth) == "string" && (colWidth.length <= 2 || colWidth.substr(colWidth.length - 2) != "px"))
				{
					wdth = 0;
					break;
				}
				wdth += this.Band.Columns[i].getWidth() + pdng;
			}
			if (wdth > 0)
			{
				var j = this.Band.getIndentation();
				for (var i = this.Band.SortedColumns.length - 1; i >= 0; i--)
				{
					var col = igtbl_getColumnById(this.Band.SortedColumns[i]);
					if (!col.IsGroupBy)
						continue;
					if (this.GroupColId == this.Band.SortedColumns[i])
						break;
					j += this.Band.getIndentation();
				}
				wdth += j;
				this.Band._wdth = wdth;
			}
			else
				this.Band._wdth = "100%";
		}
		node.setAttribute("grpWidth", this.Band._wdth);
	}
	node.setAttribute("parentRowLevel", prL)
	if (this.Grid.UseFixedHeaders && this.Grid._scrElem.scrollLeft)
		this.Grid.Node.setAttribute("fixedScrollLeft", "left:" + (-this.Grid._scrElem.scrollLeft).toString() + "px;");
	else
		this.Grid.Node.removeAttribute("fixedScrollLeft");
	xslProc.transform();
	return xslProc.output;
},

"_setupFilterRow",
function()
{	
	if (!this.FilterRow) return;
	var _b = this.Band;
	var headerDiv = igtbl_getElementById(this.Grid.Id + "_hdiv");
	var footerDiv = igtbl_getElementById(this.Grid.Id + "_fdiv");
	var tbl = this.Element.parentNode;
	var flr = this.FilterRow.Element;

	
	if (_b.Index > 0 ||
		_b.FilterRowView == igtbl_featureRowView.Top && (!headerDiv || this.length == 0) ||
		_b.FilterRowView == 2 && !footerDiv)
	{
		flr.parentNode.removeChild(flr);
		if (_b.FilterRowView == igtbl_featureRowView.Top && tbl.tBodies[0].rows.length > 0)
			tbl.tBodies[0].insertBefore(flr, tbl.tBodies[0].rows[0]);
		else
		{
			
			if (headerDiv)
			{
				
				var oldHeaderHeight = headerDiv.offsetHeight;
				headerDiv.style.height = "";
				
				if (ig_csom.IsFireFox || ig_shared.IsIEStandards)
				{
					var mr = document.getElementById(this.Grid.Id + "_mr");
					if (mr && mr.style.height.indexOf("px") != -1)
					{
						var headerHeight = headerDiv.offsetHeight;
						mr.style.height = (mr.offsetHeight + (oldHeaderHeight - headerHeight)) + "px";
					}
				}
			}
			tbl.tBodies[0].appendChild(flr);
		}
	}
	
	else if (!this.Band.IsGrouped && this.length > 0 && headerDiv && _b.FilterRowView == igtbl_featureRowView.Top
			&& !igtbl_dom.isParent(this.FilterRow.Element, headerDiv))
	{	
		flr.parentNode.removeChild(flr);

		
		if (headerDiv)
			headerDiv.style.height = "";

		headerDiv.firstChild.tBodies[0].appendChild(flr);
	}
	
	if (ig_shared.IsIEStandards && headerDiv && !this.FilterRow.isFixedBottom() && headerDiv.style.height.indexOf("px") > -1 )
	{
		
		var oldHeaderHeight = headerDiv.offsetHeight;
		headerDiv.style.height = "";		
		
		var mr = document.getElementById(this.Grid.Id + "_mr");
		if (mr && mr.style.height.indexOf("px") != -1)
		{
			var headerHeight = headerDiv.offsetHeight;
			mr.style.height = (mr.offsetHeight + (oldHeaderHeight - headerHeight)) + "px";
		}
		headerDiv.style.height = headerDiv.offsetHeight + "px";		
	}

	
	var filterRowElement = igtbl_getElementById(this.FilterRow.Id);
	if (filterRowElement)
	{
		this.FilterRow.Element = filterRowElement;
		this.FilterRow.Element.Object = this.FilterRow;
	}
},
"getHeaderRow",
function()
{
	if (this.Band.Index == 0 && this.Grid.StatHeader && this.length > 0)
	{
		return this.Grid.StatHeader.Element;
	}
	//return this.Element.t;
	return null;
},
"addNew",
function()
{
	var g = this.Grid;
	if (this.AddNewRow)
		return igtbl_activateAddNewRow(this.Grid, this.Band.Index, this.ParentRow);
	return igtbl_rowsAddNew(g.Id, this.ParentRow);
},
"dispose",
function(self)
{
	for (var i = 0; i < this.rows.length; i++)
	{
		if (this.rows[i])
		{
			if (this.rows[i].Rows)
				this.rows[i].Rows.dispose(true);
			igtbl_cleanRow(this.rows[i]);
		}
	}
	igtbl_dispose(this.rows);
	delete this.rows;
	if (self)
	{
		this.Grid = null;
		this.Band = null;
		this.ParentRow = null;
		this.deletedRows = null;
		this.Element.Object = null;
		if (this.AddNewRow)
			igtbl_cleanRow(this.AddNewRow);
		if (this.FilterRow)
			igtbl_cleanRow(this.FilterRow);
		igtbl_dispose(this);
	}
	else
		this.rows = new Array();
},
"reIndex",
function(sRow)
{
	for (var i = sRow; i < this.length; i++)
		this.getRow(i).Node.setAttribute("i", i.toString());
},
"repaint",
function()
{
	var strTransform = this.applyXslToNode(this.Node);
	if (strTransform)
	{
		var anId = (this.AddNewRow ? this.AddNewRow.Id : null);
		this.Grid._innerObj.innerHTML = "<table>" + strTransform + "</table>";
		var tbl = this.Element.parentNode;
		var newEl = this.Grid._innerObj.firstChild.firstChild;
		for (var i = this.rows.length - 1; i >= 0; i--)
			if (this.rows[i])
		{
			if (this.rows[i].HiddenElement)
			{
				if (i == newEl.rows.length - 1)
					newEl.appendChild(this.rows[i].HiddenElement);
				else
					newEl.insertBefore(this.rows[i].HiddenElement, newEl.rows[i + 1]);
				var img = newEl.rows[i].firstChild;
				if (this.rows[i].getExpanded() && img)
				{
					img = newEl.rows[i].firstChild.firstChild;
					if (img && img.tagName == "IMG")
					{
						img.src = this.Band.getCollapseImage();
						
						var alt = img.getAttribute("alt");
						if (alt != null)
						{
							var clpsAlt = img.getAttribute("igAltC");
							if (clpsAlt != null)
							{
								img.setAttribute("igAltX", alt);
								img.setAttribute("alt", clpsAlt);
								img.removeAttribute("igAltC");
							}
						}
					}
				}
			}
			var row = this.rows[i];

			
			var reSelectRow = false;
			if (row.getSelected())
			{
				reSelectRow = true;
				row.select(false, false);
			}

			row.Element = newEl.rows[i];
			row.Element.Object = row;
			var metFixed = false;
			var ri = 0;
			for (var j = 0; row.cells && j < row.cells.length; j++)
			{
				var cell = row.cells[j];
				var column = this.Band.Columns[j];
				if (column.getFixed() === false && !metFixed)
				{
					metFixed = true;
					ri = 0;
				}
				if (cell)
				{
					cell.Column = column;
					cell.Index = j; 
					if (cell.Column.hasCells())
					{
						
						var reSelectCell = false;
						if (cell.getSelected())
						{
							reSelectCell = true;
							cell.select(false, false);
						}

						if (cell.Column.getFixed() === false)
						{
							rowEl = row.Element.cells[row.Element.cells.length - 1];
							cell.Element = rowEl.firstChild.firstChild.rows[0].cells[ri];
						}
						else
							cell.Element = row.Element.cells[cell.Column.getRealIndex() + this.Band.firstActiveCell];

						
						var nodePosition = parseInt(cell.Column.Node.getAttribute("cellIndex")) - 1;
						if (nodePosition < row.Node.selectSingleNode("Cs").childNodes.length)
							cell.Node = row.Node.selectSingleNode("Cs").childNodes[nodePosition];

						cell.Element.Object = cell;
						cell.Id = cell.Element.id;

						
						if (reSelectCell)
							cell.select(true, false);
						ri++;
					}
					else
						cell.Element = null;
				}
				else if (column.hasCells())
					ri++;
			}

			
			if (reSelectRow)
				row.select(true, false);
		}
		var anr;
		if (this.AddNewRow)
		{
			if (this.Band.AddNewRowView == 1 && (this.Band.Index > 0 || !igtbl_getElementById(this.Grid.Id + "_hdiv")))
				anr = this.AddNewRow.Element;
		}
		if (anr)
		{
			while (anr.nextSibling)
				anr.parentNode.removeChild(anr.nextSibling)
			while (newEl.rows.length)
				anr.parentNode.appendChild(newEl.rows[0]);
		}
		else
			igtbl_replaceChild(tbl, newEl, this.Element);

		igtbl_fixDOEXml();
		this.Element = tbl.tBodies[0];
		this.Element.Object = this;
		if (this.AddNewRow)
		{
			if (this.Band.AddNewRowView == 2 && (this.Band.Index > 0 || !igtbl_getElementById(this.Grid.Id + "_fdiv")))
			{
				anr = this.AddNewRow.Element;
				tbl.tBodies[0].appendChild(anr);
			}
			this.AddNewRow.Element = igtbl_getElementById(anId);
			this.AddNewRow.Element.Object = this.AddNewRow;
		}
	}
},
"_buildSortXmlQueryString",
function(op)
{
	

	var g = this.Grid;
	var row = this.ParentRow;
	g.QueryString = op + "\x01";
	if (row)
		g.QueryString += row.getLevel(true);
	var sqlWhere = "";
	var sortOrder = "";
	for (var i = 0; i <= this.Band.Index; i++)
	{
		var cr = row;
		var sqlW = "";
		while (cr && cr.Band != g.Bands[i])
			cr = cr.ParentRow;
		if (g.Bands[i].DataKeyField && cr && cr.get(igtbl_litPrefix + "DataKey"))
		
		
			sqlW += cr._generateSqlWhere(g.Bands[i].DataKeyField, unescape(cr.get(igtbl_litPrefix + "DataKey")));
		else if (g.Bands[i]._sqlWhere)
		{
			if (sqlW)
				sqlW += " AND ";
			sqlW += g.Bands[i]._sqlWhere;
		}
		sqlWhere = sqlW + (i == this.Band.Index ? "" : ";");
	}
	for (var i = 0; i < g.Bands.length; i++)
	{
		var so = "";
		for (var j = 0; j < g.Bands[i].SortedColumns.length; j++)
		{
			var col = igtbl_getColumnById(g.Bands[i].SortedColumns[j]);
			so += col.Key + (col.SortIndicator == 2 ? " DESC" : "") + (j < g.Bands[i].SortedColumns.length - 1 ? "," : "");
		}
		sortOrder += so + (i == g.Bands.length - 1 ? "" : ";");
	}

	
	var band = this.Band, sCols;
	if (band)
	{
		sCols = band.Index;
		for (var i = 0; i < band.SortedColumns.length; i++)
		{
			var col = igtbl_getColumnById(band.SortedColumns[i]);
			sCols += "|" + col.Index;
			sCols += ":" + col.IsGroupBy.toString();
			sCols += ":" + col.SortIndicator;
		}
	}

	g.QueryString += "\x02" + sqlWhere;
	g.QueryString += "\x02" + sortOrder;
	if (this.Band.ColumnsOrder)
		g.QueryString += "\x02" + this.Band.ColumnsOrder;

	g.QueryString += "\x02" + sCols;

	var currentFilters = "";
	if (this.hasRowFilters())
	{
		var bandFilters = this.CurrentFilterScope();
		if (bandFilters)
		{
			for (var colId in bandFilters)
			{
				var filter = bandFilters[colId];
				
				if (filter.IsActive())
				{
					var col = igtbl_getColumnById(colId);

					currentFilters += col.getLevel(true) + "\x05" + filter.getOperator() + "\x05" + filter.getEvaluationValue() + "\x03";
				}
			}
		}
	}
	g.QueryString += "\x02" + currentFilters;
},
"sortXml",
function(sortedCols)
{
	if (this.Band.SortedColumns.length == 0)
		return;
	var g = this.Grid;
	this._buildSortXmlQueryString("Sort");
	g.RowToQuery = this.ParentRow;
	g.xmlHttpRequest(g.eReqType.Sort);
},
"getLastRowId",
function()
{
	if (!this.lastRowId)
		this.setLastRowId();
	return this.lastRowId;
},
"setLastRowId",
function(lrId)
{
	if (arguments.length == 0 && !this.lastRowId)
	{
		if (this.length > 0)
			this.lastRowId = this.getRow(this.length - 1).Element.id;
	}
	else if (lrId)
		this.lastRowId = lrId;
}
, "CurrentFilterScope",
function()
{   
	if (this.Band.RowFilterMode == 1 || (this.Band.Index == 0 && !this.Band.IsGrouped))
	{
		return this.Band._filterPanels;
	}
	if (this.Band.RowFilterMode == 2)
	{
		var filterPanels = this.Band._filterPanels;
		var myTable = this.Element;
		while (myTable != null && myTable.tagName != "TABLE")
		{
			myTable = myTable.parentNode;
		}
		if (!filterPanels || !myTable) return null;
		for (var fp in filterPanels)
		{
			if (myTable.id == fp)
				return filterPanels[fp];
		}
	}
	return null;
}
, "hasRowFilters",
function()
{
	
	switch (this.Band.RowFilterMode)
	{
		case "1":
		case 1: 
			{
				
				var filterPanels = this.Band._filterPanels;
				
				if (!filterPanels || igtbl_getLength(filterPanels) == 0) return false;
				return true;
				break;
			}
		case "2":
		case 2: 
			{
				
				var filterPanels = this.Band._filterPanels;
				if (filterPanels && this.Band.Index == 0 && !this.Band.IsGrouped && igtbl_getLength(filterPanels) > 0) return true;
				var myTable = this.Element;
				while (myTable != null && myTable.tagName != "TABLE")
				{
					myTable = myTable.parentNode;
				}
				if (!filterPanels || !myTable) return false;
				for (var fp in filterPanels)
				{
					if (myTable.id == fp)
						return true;
				}
				break;
			}
		default:
			return false;
	}
	return false;
}
, "refresh",
function(data)
{
	var g = this.Grid;
	g.setActiveCell(null);
	g.setActiveRow(null);
	g.clearSelectionAll();
	this._buildSortXmlQueryString("Refresh");
	if (data)
		g.QueryString += "\x02" + data;
	g.RowToQuery = this.ParentRow;
	g.xmlHttpRequest(g.eReqType.Refresh);
}
, "getFilterRow",
function()
{
	
	
	if (this.Band.RowFilterMode == 1) return null;

	
	if (this.Band.FilterUIType != 1) return null;

	
	return this.FilterRow;
}
];
for (var i = 0; i < igtbl_ptsRows.length; i += 2)
	igtbl_Rows.prototype[igtbl_ptsRows[i]] = igtbl_ptsRows[i + 1];

// State change object
igtbl_StateChange.prototype = new igtbl_WebObject();
igtbl_StateChange.prototype.constructor = igtbl_StateChange;
igtbl_StateChange.base = igtbl_WebObject.prototype;
function igtbl_StateChange(type, grid, obj, value)
{
	if (arguments.length > 0)
		this.init(type, grid, obj, value);
}
igtbl_StateChange.prototype.init = function(type, grid, obj, value)
{
	igtbl_StateChange.base.init.apply(this, [type]);
	this.Node = ig_ClientState.addNode(grid.StateChanges, "StateChange");

	this.Grid = grid;
	this.Object = obj;
	ig_ClientState.setPropertyValue(this.Node, "Type", this.Type);
	if (typeof (value) != "undefined" && value != null)
	{
		if (value == "" && typeof (value) == "string") value = "\x01";
		ig_ClientState.setPropertyValue(this.Node, "Value", value);
	}
	if (obj)
	{
		if (obj.getLevel)
			ig_ClientState.setPropertyValue(this.Node, "Level", obj.getLevel(true));
		var dataKey = null;
		if (obj.Type == "row" || obj.Type == "cell" || obj.Type == "rows")
		{
			var row = obj;
			if (obj.Type == "cell")
				row = obj.Row;
			else if (obj.Type == "rows")
				row = obj.ParentRow;
			if (row)
			{
				dataKey = (row.DataKey ? row.DataKey : "");
				while (row.ParentRow)
				{
					row = row.ParentRow;
					
					dataKey = (row.DataKey ? row.DataKey : "") + "\x08" + dataKey;
				}
			}
		}
		if (dataKey)
			ig_ClientState.setPropertyValue(this.Node, "DataKey", dataKey);
		if (this.Object._Changes[this.Type])
		{
			var ch = this.Object._Changes[this.Type];
			if (!ch.length)
				ch = new Array(ch);
			this.Object._Changes[this.Type] = ch.concat(this);
		}
		else
			this.Object._Changes[this.Type] = this;
	}
}
igtbl_StateChange.prototype.remove = function(lastOnly)
{
	if (lastOnly && this.Grid.StateChanges.lastChild != this.Node)
		return;
	ig_ClientState.removeNode(this.Grid.StateChanges, this.Node);
	var ch = this.Object._Changes[this.Type];
	if (ch.length)
	{
		for (var i = 0; i < ch.length; i++)
			if (ch[i] == this)
		{
			ch = this.Object._Changes[this.Type] = ch.slice(0, i).concat(ch.slice(i + 1));
			break;
		}
		if (ch.length == 1)
		{
			this.Object._Changes[this.Type] = ch[0];
			ch[0] = null;
			igtbl_dispose(ch);
		}
	}
	else
		delete this.Object._Changes[this.Type];
	this.Grid = null;
	this.Object = null;
	this.Node = null;
	igtbl_dispose(this);
}
igtbl_StateChange.prototype.setFireEvent = function(value)
{
	ig_ClientState.setPropertyValue(this.Node, "FireEvent", value.toString());
}

function igtbl_XSLTProcessor(xsltURL)
{
	if (!xsltURL)
		return null;
	if (ig_csom.IsIE)
	{
		var xslt = ig_createActiveXFromProgIDs(["MSXML2.FreeThreadedDOMDocument", "Microsoft.FreeThreadedXMLDOM"]);
		xslt.async = false;
		var http = new ActiveXObject("Msxml2.XMLHTTP.3.0");
		http.open("GET", xsltURL, false);
		http.send();
		xslt.loadXML(http.responseText);
		var xslTemplate = new ActiveXObject("MSXML2.XSLTemplate");
		xslTemplate.stylesheet = xslt;
		this.Processor = xslTemplate.createProcessor();

		
	}
	else
	{
		var xmlResp = new DOMParser();
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.open("GET", xsltURL, false);
		xmlHttp.send(null);
		this.Processor = new XSLTProcessor();
		this.Processor.importStylesheet(xmlResp.parseFromString(xmlHttp.responseText, "text/xml"));
	}
}
igtbl_XSLTProcessor.prototype.addParameter = function(name, value)
{
	if (!this.Processor) return null;
	if (ig_csom.IsIE)
		return this.Processor.addParameter(name, value);
	else
		return this.Processor.setParameter(null, name, value);
};
igtbl_XSLTProcessor.prototype.transform = function()
{
	if (!this.input)
		return false;
	if (ig_csom.IsIE)
	{
		this.Processor.input = this.input;
		this.Processor.transform();
		this.output = this.Processor.output;
	}
	else
		return this.outputDocument = this.Processor.transformToDocument(this.input);
	return true;
};

if (document.implementation && document.implementation.createDocument && igtbl_XSLTProcessor.prototype.__defineGetter__)
{
	igtbl_XSLTProcessor.prototype.__defineGetter__("output", function _igtbl_XSLTProcOutput()
	{
		if (ig_csom.IsIE)
			return this.Processor.output;
		else
		{
			if (!this.outputDocument || !this.outputDocument.firstChild)
				return null;
			var output = this.outputDocument.firstChild.innerHTML; 
			if (!output)
				output = "<tbody></tbody>";
			return output;
		}
	});

	XMLDocument.prototype.selectNodes =
	Element.prototype.selectNodes = function(sExpr)
	{
		try
		{
			var xpe = new XPathEvaluator();
			var nsResolver = xpe.createNSResolver(this.ownerDocument == null ? this.documentElement : this.ownerDocument.documentElement);
			var result = xpe.evaluate(sExpr, this, nsResolver, 0, null);
			var found = [];
			var res;
			while (res = result.iterateNext())
				found.push(res);
			return found;
		}
		catch (exc) { ; }
		return null;
	};
	XMLDocument.prototype.selectSingleNode =
	Element.prototype.selectSingleNode = function(sExpr)
	{
		try
		{
			var xpe = new XPathEvaluator();
			var nsResolver = xpe.createNSResolver(this.ownerDocument == null ? this.documentElement : this.ownerDocument.documentElement);
			var result = xpe.evaluate(sExpr, this, nsResolver, 0, null);
			var res = result.iterateNext();
			return res;
		}
		catch (exc) { ; }
		return null;
	};
	Element.prototype.__defineGetter__("text", function() { return this.textContent; });
	Element.prototype.__defineSetter__("text", function(sText) { this.textContent = sText; });
	CDATASection.prototype.__defineGetter__("text", function() { return this.textContent; });
	CDATASection.prototype.__defineSetter__("text", function(sText) { this.textContent = sText; });
}

function igtbl_getNodeValue(node)
{
	
	var value = node.getAttribute("uV");
	if (value !== null)
	{
		
		if( value.indexOf && value.indexOf('&#') >= 0)
		{
			var elem = ig_shared._gridValueFilter;
			if( !elem)
				elem = ig_shared._gridValueFilter = document.createElement('SPAN');
			elem.innerHTML = value;
			value = elem.innerHTML;
		}
		return unescape(value);
	}
	value = node.getAttribute("iDV");
	if (value !== null)
		return unescape(value);
	value = node.getAttribute("iCT");
	if (value !== null)
		return unescape(value);
	value = node.firstChild.text; 
	if (value == "&nbsp;")
		value = "";
	value = value.replace(/<br\/>/g, "\r\n");
	return value;
}

function igtbl_setNodeValue(node, value, displayValue, cellElement)
{
	var valueSet = false;
	
	if (node.getAttribute("uV") != null || (cellElement && (cellElement.getAttribute("uV") != null && cellElement.getAttribute("uV") != "")))
	{
		node.setAttribute("uV", igtbl_escape(value));
		valueSet = true;
	}
	
	if (node.getAttribute("iDV") != null || (cellElement && (cellElement.getAttribute("iDV") != null && cellElement.getAttribute("iDV") != "")))
	{
		node.setAttribute("iDV", igtbl_escape(value));
		valueSet = true;
	}
	
	if (node.getAttribute("iCT") != null || (cellElement && (cellElement.getAttribute("iCT") != null && cellElement.getAttribute("iCT") != "")))
	{
		node.setAttribute("iCT", igtbl_escape(value));
		valueSet = true;
	}
	if (displayValue)
		node.firstChild.text = displayValue;
	else if (!valueSet)
	
	
		node.firstChild.text = (value === "" ? "&nbsp;" : value.toString()); 
}
