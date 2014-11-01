/*
* ig_spellcheckerdialog.js
* Version 11.1.20111.2158
* Copyright(c) 2001-2012 Infragistics, Inc. All Rights Reserved.
*/


function ig_CreateWebSpellCheckerDialog(props)
{  
	if(!ig_WebControl.prototype.isPrototypeOf(ig_WebSpellCheckerDialog.prototype))
	{
		ig_WebSpellCheckerDialog.prototype = new ig_WebControl();
		ig_WebSpellCheckerDialog.prototype.constructor = ig_WebSpellCheckerDialog;
		ig_WebSpellCheckerDialog.prototype.base=ig_WebControl.prototype;

		ig_WebSpellCheckerDialog.prototype.init = function (props)
		{
			this._isInitializing = true;

			
			

			if (props && props.length > 0 && props[0].length > 11)
			{
				props[0][11] = props[0][11].replace(/\%2520/g, "@igspace@");
				
				
				
				
				//props[0][11] = props[0][11].replace(/\%252B/g, "%2B");
				props[0][11] = props[0][11].replace(/\%25/g, "%252525");
			}

			this._initControlProps(props);
			

			if (props && props.length > 0 && props[0].length > 11)
				props[0][11] = props[0][11].replace(/\@igspace\@/g, "%20");
			this.base.init.apply(this, [this.getClientID()]);
			this._isInitializing = false;
			this._spellCheckFinished = false;
			this._duplicateWord = false;
			this._currentWordIndex = 0;
			this._ignoreWordIndexes = new Object();
			this._dialog = this;
			this._webSpellChecker = null;
			this._openerWindow = null;
			this._oldText = "";
		}	
		
		ig_WebSpellCheckerDialog.prototype._onLoad = function(src, evnt)
		{
			if(window.dialogArguments) 
			{
				this._webSpellChecker = window.dialogArguments[0];
				

				this._openerWindow = window.dialogArguments[2].window;
				if(!this._openerWindow)
					this._openerWindow = window.dialogArguments[2].parentWindow;    
				if(window.dialogArguments[1] != "Loaded")
				{
					var formChildren = document.forms[0].childNodes; 
					for(var i = formChildren.length - 1; i >= 0 ; i--)
					{
						if(formChildren[i].tagName != "SCRIPT" && formChildren[i].tagName != "STYLE" && formChildren[i].tagName !="LINK")
							document.forms[0].removeChild(formChildren[i]); 
					}
					var iframe = document.createElement("IFRAME");
					iframe.style.height = "100%";
					iframe.style.width ="100%";
					var html = window.dialogArguments[1]; 
					window.dialogArguments[1] = "Loaded";
					iframe.src='javascript:new String("<html></html>")';
					document.forms[0].appendChild(iframe);
					iframe.contentWindow.document.open();
					iframe.contentWindow.document.write(html);
					iframe.contentWindow.document.close();
					iframe.contentWindow.document.forms[0].submit();
					return;
				}
			}
			else if(window.opener != null)
			{
				



				this._webSpellChecker = window.opener.ig_getWebControlById(this._dialog.getWebSpellCheckerId());
				this._openerWindow = window.opener;
			}
	
			if(this._webSpellChecker != null)
			{
				
				this._oldText = this._fixPlus(this.getText());
				var element = document.getElementById(this.getChangeAllButtonId());
				if(element != null)
				{
					element._dialog = this;
					ig_shared.addEventListener(element, "click", this.changeAll, true);
				}
					
				element = document.getElementById(this.getChangeButtonId());
				if(element != null)
				{
					element._dialog = this;
					ig_shared.addEventListener(element, "click", this.change, true);
				}
				
				element = document.getElementById(this.getFinishButtonId());
				if(element != null)
				{
					element._dialog = this;
					element.onclick = this.finish;
				}
				element = document.getElementById(this.getIgnoreAllButtonId());
				if(element != null)
				if(element != null)
				{
					element._dialog = this;
					ig_shared.addEventListener(element, "click", this.ignoreAll, true);
				}
					
				element = document.getElementById(this.getIgnoreButtonId());
				if(element != null)
				{
					element._dialog = this;
					ig_shared.addEventListener(element, "click", this.ignoreCurrent, true);
				}
					
				element = document.getElementById(this.getAddButtonId());
				if(element != null)
				{
					element._dialog = this;
					element.onclick = this.addCurrent;
				}
				
				element = document.getElementById(this.getSuggestionBoxId());
				if(element != null)
				{
					element._dialog = this;
					ig_shared.addEventListener(element, "dblclick", this.change, true);
					element.onchange = this.changeSuggestions;
				}
				
				element = document.getElementById(this.getChangeToBoxId());
				if(element != null)
				{
					ig_shared.addEventListener(element, "keypress", this.keyPress, true);
				}
				
				var label = document.createElement("LABEL");
				this._changeToLabelId = "changeToLabel508";
				label.id = this._changeToLabelId;
				label.htmlFor = this.getChangeToBoxId(); 
				label.style.display = "none";
				document.body.appendChild(label); 
				
				label = document.createElement("LABEL");
				label.id = "suggestionLabel508";
				label.htmlFor = this.getSuggestionBoxId(); 
				label.style.display = "none";
				document.body.appendChild(label); 
				
				this.refresh();
			}
		}
		
		ig_WebSpellCheckerDialog.prototype.keyPress = function(evnt)
		{
			if(evnt.keyCode == 13)
				return false;
		}
		
		ig_WebSpellCheckerDialog.prototype.change = function(evt)
		{
			var elem = _ig_ResolveElemFromEvnt(evt);			
			var dialog = elem._dialog
			
			if(dialog._currentWordIndex < dialog.getBadWords().length)
			{
				dialog.changeWord(dialog._currentWordIndex);
				dialog.nextWord();
			}	
			ig_cancelEvent(evt);
		}
		
		ig_WebSpellCheckerDialog.prototype.changeAll = function(evt)
		{
			var elem = _ig_ResolveElemFromEvnt(evt);			
			var dialog = elem._dialog
			
			var badWords = dialog.getBadWords();
			if(dialog._currentWordIndex < badWords.length)
			{ 
				var currentWord = badWords.getItem(dialog._currentWordIndex).getText(); 
				dialog.changeWord(dialog._currentWordIndex); 
				for (var i = dialog._currentWordIndex + 1; i < badWords.length; i++) 
				{ 
					if (!dialog._ignoreWordIndexes[i] && badWords.getItem(i).getText() == currentWord) 
					{ 
						dialog.changeWord(i); 
						dialog._ignoreWordIndexes[i] = true; 
					} 
				} 
				dialog.nextWord(); 
			}
			ig_cancelEvent(evt);
		}
		ig_WebSpellCheckerDialog.prototype.ignoreCurrent = function(evt)
		{	
			var elem = _ig_ResolveElemFromEvnt(evt);
			var dialog = elem._dialog; 
			if(dialog._currentWordIndex<dialog.getBadWords().length)
			{
				dialog.nextWord();
			}
			ig_cancelEvent(evt);
		}
		ig_WebSpellCheckerDialog.prototype.ignoreAll = function(evt)
		{
			

			var dialog = null; 
			if(evt)
			{
				var elem = _ig_ResolveElemFromEvnt(evt);
				dialog = elem._dialog;
			}
			else
				dialog = this;
				
			var badWords = dialog.getBadWords();
			if(dialog._currentWordIndex < badWords.length)
			{ 
				var currentWord = badWords.getItem(dialog._currentWordIndex).getText();
				for (var i = dialog._currentWordIndex + 1; i < badWords.length; i++) 
				{  
					if (!dialog._ignoreWordIndexes[i] && badWords.getItem(i).getText() == currentWord) 
					{  
						dialog._ignoreWordIndexes[i] = true;  
					} 
				} 
				
				dialog.nextWord(); 
			}
			ig_cancelEvent(evt);
		}
		ig_WebSpellCheckerDialog.prototype.changeSuggestions = function()
		{
			var suggestions = document.getElementById(this._dialog.getSuggestionBoxId());
			var changeToBox = document.getElementById(this._dialog.getChangeToBoxId());
			var suggestion=suggestions.options[suggestions.selectedIndex].text;
			if(suggestion!=this._dialog.getNoSuggestionsText())
			{
				changeToBox.value=suggestion;
			}
		}
		ig_WebSpellCheckerDialog.prototype.addCurrent = function()
		{
			var addButton = this;
			var badWords = this._dialog.getBadWords();
			var currentWord = badWords.getItem(this._dialog._currentWordIndex).getText();
			if(addButton)
				addButton.disabled=true;
			var addFrame = document.createElement("IFRAME");
			addFrame.style.height = "0px";
			addFrame.style.width ="0px";
			addFrame.style.visibility ="hidden";
			document.body.appendChild(addFrame);
			addFrame.contentWindow.document.open();
			var hiddenInputs = "<input type='hidden' name='UserDictionaryFile' value='"+this._dialog.getUserDictionaryFile()+"'>";
			hiddenInputs += "<input type='hidden' name='Add' value='"+currentWord+"'>";
			addFrame.contentWindow.document.write("<html><head></head><body><form method='post' action='" + this._dialog._openerWindow.location.href + "'>"+ hiddenInputs+"</form></body></html>");
			addFrame.contentWindow.document.close();
			addFrame.contentWindow.document.forms[0].submit();
			addButton.disabled=false;
			this._dialog.ignoreAll();
		}
		ig_WebSpellCheckerDialog.prototype.finish = function()
		{
			if (this._dialog._webSpellChecker != null)
			{
				var textComponentId = this._dialog.getTextComponentId();
				
				var txt = this._dialog._fixPlus(this._dialog.getText());
				if (textComponentId != null && textComponentId.length > 0)
					this._dialog._webSpellChecker.setText(textComponentId, txt);
				else if (this._dialog._webSpellChecker._returnFunc != null)
					this._dialog._webSpellChecker._returnFunc(txt);

				this._dialog._webSpellChecker._onSpellCheckComplete(this._dialog._oldText, txt);

				

				if (!this._dialog)
					return;

				var stateItem = this._dialog._webSpellChecker.addStateItem("Finished", "SpellCheck");
				this._dialog._webSpellChecker.updateStateItem(stateItem, "OldText", this._dialog._oldText);
				this._dialog._webSpellChecker.updateStateItem(stateItem, "CorrectedText", txt);

				if (this._dialog._webSpellChecker.getAutoPostBackSpellCheckComplete())
				{
					


					if (!ig_shared.IsIE)
						this._dialog._webSpellChecker._fireFoxFireFinishedServerEvent();
					else
						this._dialog._webSpellChecker._fireSpellCheckComplete();
				}
			}
			window.close();
		}
		
		ig_WebSpellCheckerDialog.prototype.moveWordOffsets = function(delta, start)
		{
			var badWords = this.getBadWords();
			for(i=start;i<badWords.length;i++)
			{
				var badWord = badWords.getItem(i);
				badWord.setStartPosition(badWord.getStartPosition()+delta);
				badWord.setEndPosition(badWord.getEndPosition()+delta);
			}
		}
		ig_WebSpellCheckerDialog.prototype.changeWord = function (index)
		{
			var newText = "";
			var changeToBox = document.getElementById(this.getChangeToBoxId());
			var newWord = changeToBox.value;
			var badWords = this.getBadWords();
			var badWord = badWords.getItem(index);
			if (this._duplicateWord && changeToBox.value == '')
			{
				badWord.setStartPosition((badWord.getStartPosition() - 1));
			}
			if (this._dialog._webSpellChecker != null)
			{
				var override = this._dialog._webSpellChecker._onWordCorrected(badWord.getText(), newWord);
				if (override != "")
					newWord = override;
			}
			


			var text = this._fixPlus(this.getText());
			newText += text.substring(0, badWord.getStartPosition());
			newText += newWord;
			newText += text.substring(badWord.getEndPosition(), text.length);
			this.moveWordOffsets(newWord.length - text.substring(badWord.getStartPosition(), badWord.getEndPosition()).length, index + 1);
			this.setText(newText);
		}
		ig_WebSpellCheckerDialog.prototype._fixScrollPosition = function()
		 {
			var documentTextPanel = document.getElementById(this.getDocumentTextPanelId());
			clearTimeout(this._scrollInterval);
			
			for(var i =0; i < documentTextPanel.childNodes.length; i++)
			{
				




				var spans = documentTextPanel.getElementsByTagName("SPAN");
				for(var j = 0; j < spans.length; j++)
				{
					if(spans[j].className == this.getBadWordsStyleClassName())
					{						
						var node = spans[j];
						var scroll = node.offsetTop;

						
						
						while(node.offsetParent != documentTextPanel)
						{
							node = node.offsetParent;
							scroll += node.offsetTop;
		                }

		                documentTextPanel.scrollTop = scroll;
 						break;
 					
					}
				}				
			}
		 }

		 ig_WebSpellCheckerDialog.prototype.refresh = function ()
		 {

		 	var documentTextPanel = document.getElementById(this.getDocumentTextPanelId());
		 	var suggestions = document.getElementById(this.getSuggestionBoxId());
		 	var changeToBox = document.getElementById(this.getChangeToBoxId());
		 	var badWords = this.getBadWords();
		 	var badWord = badWords.getItem(this._currentWordIndex);
		 	var changeToLabel = document.getElementById(this._changeToLabelId);
		 	if (documentTextPanel != null)
		 	{
		 		if (this._currentWordIndex < this.getBadWords().length)
		 		{
		 			documentTextPanel.innerHTML = "";

		 			



		 			var text = this._fixPlus(this.getText());
		 			var beginning = this.textToHtml(text.substring(0, badWord.getStartPosition()));
		 			var spanHtml = "<span id='highlight' class='" + this.getBadWordsStyleClassName() + "' style='height:1px;'>";
		 			spanHtml += text.substring(badWord.getStartPosition(), badWord.getEndPosition());
		 			spanHtml += "</span>";
		 			var end = this.textToHtml(text.substring(badWord.getEndPosition(), text.length));

		 			
		 			documentTextPanel.innerHTML = beginning + spanHtml + end + "<br/><br/>";
		 			suggestions.options.length = 0;

		 			


		 			this._scrollInterval = setInterval(ig_createCallback(this._fixScrollPosition, this, null), 100);


		 			var n = badWord.getSuggestions().count;
		 			if (n == 0)
		 			{
		 				changeToBox.value = badWord.getText();
		 				suggestions.options[0] = new Option(this.getNoSuggestionsText());
		 			}
		 			else if (badWord.getSuggestions().getItem(0) == "Remove duplicate word")
		 			{
		 				suggestions.options[0] = new Option(badWords[currentWordIndex].suggestions[0]);
		 				suggestions.selectedIndex = 0;
		 				changeToBox.value = '';
		 				this._duplicateWord = true;
		 			}
		 			else
		 			{
		 				changeToBox.value = badWord.getSuggestions().getItem(0);
		 				for (var i = 0; i < n; i++)
		 				{
		 					suggestions.options[i] = new Option(badWord.getSuggestions().getItem(i));
		 				}

		 				suggestions.selectedIndex = 0;
		 				this._duplicateWord = false;
		 			}
		 			changeToLabel.innerHTML = "Change From " + badWord.getText() + " To " + changeToBox.value;
		 			changeToBox.select();
		 		}
		 		else
		 		{

		 			
		 			documentTextPanel.innerHTML = this._fixPlus(this.textToHtml(this.getText()));

		 			changeToBox.value = "";
		 			suggestions.options.length = 0;
		 			suggestions.options[0] = new Option(this.getNoSuggestionsText());

		 			

		 			if (ig_shared.IsIEStandards && badWords.length < 1)
		 				setTimeout(ig_createDelegate(this, this._displayFinishedText, [badWords.length]), 0);
		 			else
		 				this._displayFinishedText(badWords.length);
		 		}
		 	}
		 }
		 
		ig_WebSpellCheckerDialog.prototype._displayFinishedText = function(badWordsCount)
		{
			if (badWordsCount > 0)
			{
				if (this._webSpellChecker.getShowFinishedMessage())
					alert(this.getFinishedText());
			}
			else
			{
				if (this._webSpellChecker.getShowNoErrorsMessage())
					alert(this.getNoErrorsText());
			}
			this._spellCheckFinished = true;
			this.finish();
		}
		ig_WebSpellCheckerDialog.prototype.textToHtml = function(t) 
		{
			


			
			var xml = this._webSpellChecker;
			if (xml && (xml._htmlEditor || !xml.getAllowXML()))
				xml = null;
			if (xml)
			{ 
				var ltexp = new RegExp("<"); 
				
				while(ltexp.test(t))
					t = t.replace(ltexp, "&lt;");
			
				var gtexp = new RegExp(">");
				
				while(gtexp.test(t))
					t = t.replace(gtexp, "&gt;"); 
			} 
			else {} 
			 
			var newlineexp = new RegExp("\n");
			 
			while(newlineexp.test(t))
				t = t.replace(newlineexp, "<br>");
			
			return t;
		
		}

		ig_WebSpellCheckerDialog.prototype.nextWord = function ()
		{			
			var badWords = this.getBadWords();
			while (this._currentWordIndex++ < badWords.length && this._ignoreWordIndexes[this._currentWordIndex]);
			var changeButton = document.getElementById(this.getChangeButtonId());
			var changeAllButton = document.getElementById(this.getChangeAllButtonId());
			var ignoreButton = document.getElementById(this.getIgnoreButtonId());
			var ignoreAllButton = document.getElementById(this.getIgnoreAllButtonId());

			if (this._currentWordIndex >= badWords.length)
			{
				if (changeButton != null)
					changeButton.disabled = true;

				if (changeAllButton != null)
					changeAllButton.disabled = true;

				if (ignoreButton != null)
					ignoreButton.disabled = true;

				if (ignoreAllButton != null)
					ignoreAllButton.disabled = true;
			}

			this.refresh();
		}
		
		ig_WebSpellCheckerDialog.prototype.getDocumentTextPanelId = function() 
		{
			return this._props[2];
		}
		ig_WebSpellCheckerDialog.prototype.getChangeAllButtonId = function() 
		{
			return this._props[3];
		}
		ig_WebSpellCheckerDialog.prototype.getChangeButtonId = function() 
		{
			return this._props[4];
		}
		ig_WebSpellCheckerDialog.prototype.getChangeToBoxId = function() 
		{
			return this._props[5];
		}
		ig_WebSpellCheckerDialog.prototype.getFinishButtonId = function() 
		{
			return this._props[6];
		}
		ig_WebSpellCheckerDialog.prototype.getIgnoreAllButtonId = function() 
		{
			return this._props[7];
		}
		ig_WebSpellCheckerDialog.prototype.getIgnoreButtonId = function() 
		{
			return this._props[8];
		}
		ig_WebSpellCheckerDialog.prototype.getSuggestionBoxId = function() 
		{
			return this._props[9];
		}
		ig_WebSpellCheckerDialog.prototype.getAddButtonId = function() 
		{
			return this._props[10];
		}
		
		ig_WebSpellCheckerDialog.prototype._fixPlus = function(txt)
		{
			return txt.replace(/\%2B/g, '+');
		}
		ig_WebSpellCheckerDialog.prototype.getText = function() 
		{
			return this._props[11];
		}
		ig_WebSpellCheckerDialog.prototype.setText = function(val) 
		{
			this._props[11] = val;
		}
		ig_WebSpellCheckerDialog.prototype.getNoErrorsText = function() 
		{
			return this._props[12];
		}
		ig_WebSpellCheckerDialog.prototype.getFinishedText = function() 
		{
			return this._props[13];
		}
		ig_WebSpellCheckerDialog.prototype.getNoSuggestionsText = function() 
		{
			return this._props[14];
		}
		ig_WebSpellCheckerDialog.prototype.getUserDictionaryFile = function() 
		{
			return this._props[15];
		}
		ig_WebSpellCheckerDialog.prototype.getWebSpellCheckerId = function() 
		{
			return this._props[16];
		}
		ig_WebSpellCheckerDialog.prototype.getBadWordsStyleClassName = function() 
		{
			return this._props[17];
		}
		ig_WebSpellCheckerDialog.prototype.getTextComponentId = function() 
		{
			return this._props[18];
		}
		
		ig_WebSpellCheckerDialog.prototype.getBadWords = function() 
		{
			if(this._collections[0] == null)
					return null;
			if(this._badWords == null)
				this._badWords = new ig_BadWordsCollection(this._collections[0]);
			return this._badWords;
			
			
			return this._props[10];
		}
		
		ig_WebSpellCheckerDialog.prototype._onUnload = function(src, evnt)
		{
			this._webSpellChecker = null;
			


			this._dialog = null;
			this._openerWindow = null;
		}
	}
	 return new ig_WebSpellCheckerDialog(props);
}

function ig_WebSpellCheckerDialog(props)
{
   if(arguments.length != 0)
	   this.init(props);
}




function ig_BadWordsCollection(props)
{
	this._props = props;
	this.length = props.length;
	this.getItem = function(index)
	{
		if(index < 0 || index > this.length)
			return null;
			
		if(this[index] == null)
			this[index] = ig_CreateBadWord(this._props[index])
			
		return(this[index]);
	}
}   

function ig_CreateBadWord(props)
{
	if(!ig_BadWord.prototype.isPrototypeOf(this.prototype))
	{
		ig_BadWord.prototype.getText = function()
		{
			return this._props[0];
		}
		
		ig_BadWord.prototype.gete = function()
		{
			return this._props[1];
		}
		
		ig_BadWord.prototype.getStartPosition = function()
		{
			return parseInt(this._props[2]);
		}
		ig_BadWord.prototype.setStartPosition = function(val)
		{
			this._props[2] = val;
		}
		ig_BadWord.prototype.getEndPosition = function()
		{
			return parseInt(this._props[3]);
		}
		ig_BadWord.prototype.setEndPosition = function(val)
		{
			this._props[3] = val;
		}
		
		ig_BadWord.prototype.getSuggestions = function()
		{
			if(this._suggestions == null)
				this._suggestions = new ig_SuggestionsCollection(this._props[4]);
			return this._suggestions; 
		}
	
	}
	 return new ig_BadWord(props);
}

function _ig_ResolveElemFromEvnt(evnt)
{
	if(evnt.target) 
		return evnt.target;
	else if(evnt.srcElement)
		return evnt.srcElement;
}

function ig_BadWord(props)
{
	this._props = props;
}
   
function ig_SuggestionsCollection(props)
{
	this._props = props;
	this.count =  this._props.length;
	
	this.getItem = function(index)
	{
		if(index < 0 || index > this.count)
			return null;
			
		if(this[index] == null)
			this[index] = this._props[index]
			
		return(this[index]);
	}
}
