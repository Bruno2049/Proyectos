/*
* ig_weblistbarNS4.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/








function iglbar_groupButtonClickedDL(lbar,group,postback){
	if(postback)__doPostBack(lbar,group+":GroupSelected");
}

function iglbar_itemClickedDL(lbar,item,postback){
	if(postback)__doPostBack(lbar,item+":ItemSelected");
}


function iglbar_expandGroupDL(lbar,group,expand){
	if(expand){
		__doPostBack(lbar,group+":GroupExpanded");		
	}
	else{
		__doPostBack(lbar,group+":GroupCollapsed");		
	}

}


function iglbar_killEvent(evt){
	evt = (evt) ? evt : ((window.event) ? window.event : "");
	return false;
}
