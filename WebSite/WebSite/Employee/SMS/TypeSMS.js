var MaxLenght =0,SMSCount=1,SMSLength=0,HasFariChar =false;
var  txtAreaName=null;
var isFarsi=true;
if(typeof HTMLElement!="undefined" && ! HTMLElement.prototype.insertAdjacentElement) {
	HTMLElement.prototype.insertAdjacentElement = function (where,parsedNode) {
		switch (where) {
			case 'beforeBegin':
				this.parentNode.insertBefore(parsedNode,this)
				break;
			case 'afterBegin':
				this.insertBefore(parsedNode,this.firstChild);
				break;
			case 'beforeEnd':
				this.appendChild(parsedNode);
				break;
			case 'afterEnd':
				if (this.nextSibling)
					this.parentNode.insertBefore(parsedNode,this.nextSibling);
				else
					this.parentNode.appendChild(parsedNode);
				break;
		}
	}

	HTMLElement.prototype.insertAdjacentHTML = function (where,htmlStr) {
		var r = this.ownerDocument.createRange();
		r.setStartBefore(this);
		var parsedHTML = r.createContextualFragment(htmlStr);
		this.insertAdjacentElement(where,parsedHTML)
	}

	HTMLElement.prototype.insertAdjacentText = function (where,txtStr) {
		var parsedText = document.createTextNode(txtStr)
		this.insertAdjacentElement(where,parsedText)
	}
}

var FarsiType = {
	// Farsi keyboard map based on Iran Popular Keyboard Layout
	farsiKey: [
		32,	33,	34,	35,	36,	37,	1548,	1711,
		41,	40,	215,	43,	1608,	45,	46,	47,
		48,	49,	50,	51,	52,	53,	54,	55,
		56,	57,	58,	1705,	44,	61,	46,	1567,
		64,	1616,	1584,	125,	1609,	1615,	1609,	1604,
		1570,	247,	1600,	1548,	47,	8217,	1583,	215,
		1563,	1614,	1569,	1613,	1601,	8216,	123,	1611,
		1618,	1573,	126,	1580,	1688,	1670,	94,	95,
		1662,	1588,	1584,	1586,	1740,	1579,	1576,	1604,
		1575,	1607,	1578,	1606,	1605,	1574,	1583,	1582,
		1581,	1590,	1602,	1587,	1601,	1593,	1585,	1589,
		1591,	1594,	1592,	60,	124,	62,	1617
	],
	Type: true,
	counter: 0,
	ShowChangeLangButton: 1,	// 0: Hidden / 1: Visible
	KeyBoardError: 1,			// 0: Disable FarsiType / 1: Show Error
	ChangeDir: 0,			// 0: No Action / 1: Do Rtl-Ltr / 2: Rtl-Ltr button
	UnSupportedAction: 0		//0: Disable FarsiType / 1: Low Support
}

FarsiType.enable_disable = function(Dis) {
	var invis, obj;
	
	if (!Dis.checked)  {
		FarsiType.Type = true;
		disable = false;
		color = 'darkblue';
	} else {
		FarsiType.Type = false;
		disable = true;
		color = '#ECE9D8';
	}

	if (FarsiType.ShowChangeLangButton == 1) { 
		for (var i=1; i<= FarsiType.counter; i++) {
			obj = document.getElementById('FarsiType_button_' + i);
			obj.disabled = disable;
			obj.style.backgroundColor = color;
		}
	}
}

FarsiType.Disable = function() {
	FarsiType.Type = false;
	var Dis = document.getElementById('disableFarsiType')
	if (Dis != null) {
		Dis.checked = true;
	}

	if (FarsiType.ShowChangeLangButton == 1) { 
		for (var i=1; i<= FarsiType.counter; i++) {
			obj = document.getElementById('FarsiType_button_' + i);
			obj.disabled = true;
			obj.style.backgroundColor = '#ECE9D8';
		}
	}
}

FarsiType.init = function() {

	var Inputs = document.getElementsByTagName('INPUT');
	for (var i=0; i<Inputs.length; i++) {
		if (Inputs[i].type.toLowerCase() == 'text' && (Inputs[i].lang.toLowerCase() == 'fa' || Inputs[i].lang.toLowerCase() == 'fa-ir')) {
			FarsiType.counter++;
			new FarsiType.KeyObject(Inputs[i], FarsiType.counter);
		}
	}

	var Areas = document.getElementsByTagName('TEXTAREA');
	for (var i=0; i<Areas.length; i++) {
		if (Areas[i].lang.toLowerCase() == 'fa' || Areas[i].lang.toLowerCase() == 'fa-ir') {
			FarsiType.counter++;
			new FarsiType.KeyObject(Areas[i], FarsiType.counter);
		}
	}
	
	var Dis = document.getElementById('disableFarsiType')
	if (Dis != null) {
		FarsiType.enable_disable (Dis);
		Dis.onclick = new Function( "FarsiType.enable_disable (this);" )
	}
}

FarsiType.KeyObject = function(z,x) {

	GenerateStr = "";
	if (FarsiType.ShowChangeLangButton == 1) {
		GenerateStr = GenerateStr + "<div align=left><input type='button' id=FarsiType_button_"+x+" style='border: none; background-color:red; font-size:11; color:white; font-family:tahoma; padding: 1px; margin: 1px; width: auto; height: auto;' value='FA' /></div>&nbsp;";
	}
	if (FarsiType.ChangeDir == 2) {
		GenerateStr = GenerateStr  + "<div align=left><input type='button' id=FarsiType_ChangeDir_"+x+" style='border: none; background-color:red; font-size:11; color:white; font-family:tahoma; padding: 1px; margin: 1px; width: auto; height: auto;' value='RTL' /></div>"
	}
	//z.insertAdjacentHTML("afterEnd", GenerateStr);
	z.insertAdjacentHTML("beforeBegin", GenerateStr);

	if (FarsiType.ShowChangeLangButton == 1) { 
		z.bottelm = document.getElementById ('FarsiType_button_' + x);
		z.bottelm.title = 'تغییر زبان به انگلیسی';
	}
	if (FarsiType.ChangeDir == 2) {
		z.Direlm = document.getElementById ('FarsiType_ChangeDir_' + x); 
	}

	isFarsi= true;
	z.farsi = isFarsi;
	z.dir = "rtl";
	z.align = "right";

	z.style.textAlign = z.align;
	z.style.direction = z.dir;

	setSelectionRange = function(input, selectionStart, selectionEnd) {
		input.focus()
		input.setSelectionRange(selectionStart, selectionEnd)
	}

	ChangeDirection = function() {
		if (z.dir == "rtl") {
			z.dir = "ltr";
			z.align = "left";
			z.Direlm.value = "LTR";
			z.Direlm.title = "تغییر جهت نوشتن: راست به چپ"
		} else {
			z.dir = "rtl";
			z.align = "right";
			z.Direlm.value = "RTL";
			z.Direlm.title = "تغییر جهت نوشتن: چپ به راست"
		}
		z.style.textAlign = z.align;
		z.style.direction = z.dir;
		z.focus();
	}

	
FarsiType.SetLabels=function()
{
// if(z.farsi)
  if(isFarsi)
		        {			        
		          var SMSBody =null;
                   SMSBody=document.getElementById(txtAreaName);
		       
                        var t =((SMSBody.value.length)/70);
                     
			            SMSCount= (parseInt(t.toString()))+1; 	
//			            if(SMSCount == 5)
//			                SMSCount = 4;		             
			            lblSMSCount.SetText(SMSCount);  
			   
			   //  lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length+1).toString());	     
                    lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length).toString());	
		        }
		        else
		        {		   
		         var SMSBody=null;
		             SMSBody=document.getElementById(txtAreaName);
                    for (var i=0; i<SMSBody.value.length; i++)
                    {
                       if(SMSBody.value.toString().charCodeAt(i)>128)
                       {
                            HasFariChar = true;                           
                            break;
                        }
                    }
                    if(HasFariChar == true)
                    {
                         var t =((SMSBody.value.length)/70);
                       
			            SMSCount= (parseInt(t.toString()))+1; 				           
//			            if(SMSCount == 5)
//			                SMSCount = 4;		                		             
			            lblSMSCount.SetText(SMSCount);        
                        //lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length+1).toString());	                       
                          lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length).toString());
                        HasFariChar = false; 
                    }
                    else
                    {
		                lblSMSCount.SetText(SMSCount);	
                            var t =((SMSBody.value.length)/160);
			                SMSCount= (parseInt(t.toString()))+1; 			               
			                lblSMSCount.SetText(SMSCount);      
			               // lblSMSLenght.SetText(((SMSCount*160) -SMSBody.value.length+1).toString());	
                        lblSMSLenght.SetText(((SMSCount*160) -SMSBody.value.length).toString());	
                    }
                 }
}
	isChangeLang = function(e) {
	
		if (e == null) e = window.event;
		    var key = e.keyCode ? e.keyCode : e.charCode;		  
//          if(SMSCount>=4)
//          {         
//            return ;
//          }
//          else
//          {
		        //if(z.farsi)
		         if(isFarsi)
		        {			        
		          var SMSBody =null;
                   SMSBody=document.getElementById(txtAreaName);
		       
                        var t =((SMSBody.value.length)/70);
                     
			            SMSCount= (parseInt(t.toString()))+1; 	
//			            if(SMSCount == 5)
//			                SMSCount = 4;		             
			            lblSMSCount.SetText(SMSCount);  
			   
			   //  lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length+1).toString());	     
                    lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length).toString());	
		        }
		        else
		        {		   
		         var SMSBody=null;
		             SMSBody=document.getElementById(txtAreaName);
                    for (var i=0; i<SMSBody.value.length; i++)
                    {
                       if(SMSBody.value.toString().charCodeAt(i)>128)
                       {
                            HasFariChar = true;                           
                            break;
                        }
                    }
                    if(HasFariChar == true)
                    {
                         var t =((SMSBody.value.length)/70);
                       
			            SMSCount= (parseInt(t.toString()))+1; 				           
//			            if(SMSCount == 5)
//			                SMSCount = 4;		                		             
			            lblSMSCount.SetText(SMSCount);        
                        //lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length+1).toString());	                       
                          lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length).toString());
                        HasFariChar = false; 
                    }
                    else
                    {
		                lblSMSCount.SetText(SMSCount);	
                            var t =((SMSBody.value.length)/160);
			                SMSCount= (parseInt(t.toString()))+1; 			               
			                lblSMSCount.SetText(SMSCount);      
			               // lblSMSLenght.SetText(((SMSCount*160) -SMSBody.value.length+1).toString());	
                        lblSMSLenght.SetText(((SMSCount*160) -SMSBody.value.length).toString());	
                    }
                 }
            //}      
	}
	
	ChangeLang = function(e, ze) {
	var SMSBody=null;
	    SMSBody=document.getElementById(txtAreaName);
		if(ze)
			z = ze;

		if (FarsiType.Type) {
			if (isFarsi) {
			isFarsi=false;
				z.farsi = isFarsi;
				if (FarsiType.ShowChangeLangButton == 1) { 
					z.bottelm.value = "EN";
					z.bottelm.title = 'تغییر زبان به فارسی';
					
					for (var i=0; i<SMSBody.value.length; i++)
            {
               if(SMSBody.value.toString().charCodeAt(i)>128)
               {
                    HasFariChar = true;                           
                    break;
                }
            }
            if( HasFariChar == true)
            {            
			    MaxLenght = 70;
			  
			    lblSMSLenght.SetText(MaxLenght-SMSBody.value.length);//MaxLenght.toString())
			    HasFariChar = false;  
            }
            else
            {
			    MaxLenght = 160;
			    lblSMSLenght.SetText(MaxLenght-SMSBody.value.length);//MaxLenght.toString()
			}
				}
				if (FarsiType.ChangeDir == 1) {
					z.style.textAlign = "left";
					z.style.direction = "ltr";
				}
			} else {
			isFarsi=true;
				z.farsi = isFarsi;
				if (FarsiType.ShowChangeLangButton == 1) { 
					z.bottelm.value = "FA";
					z.bottelm.title = 'تغییر زبان به انگلیسی';
					
			MaxLenght = 70;
			 lblSMSLenght.SetText(MaxLenght-SMSBody.value.length);
				}
				if (FarsiType.ChangeDir == 1) {
					z.style.textAlign = "right";
					z.style.direction = "rtl";
				}
			}
			z.focus();
		}
		
		var evt = e || window.event;
		if (evt.preventDefault)
			evt.preventDefault();
		else
		    evt.returnValue = false;
		return false;
	}

	Convert = function(e) {

		if (e == null)
			e = window.event;

		var key = e.which || e.charCode || e.keyCode;
		var eElement = e.target || e.originalTarget || e.srcElement;

		if (e.ctrlKey && key == 32) {
			ChangeLang(e, z);
		}
        else if(key==27)
		    {	
		    
		     var SMSBody=null;// =document.forms[0].namedItem('ctl00$ContentPlaceHolder1$RoundPanelSMSText$txtbSMSBody');	           
		         SMSBody=document.getElementById(txtAreaName);
		    // lblSMSLenght.SetText(70);	
		        SMSCount=1;
		        lblSMSCount.SetText(SMSCount);	
		        lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length).toString());  		        
		    }

		if (FarsiType.Type) {
			if (
				(e.charCode != null && e.charCode != key) ||
				(e.which != null && e.which != key) ||
				(e.ctrlKey || e.altKey || e.metaKey) ||
				(key == 13 || key == 27 || key == 8)
			) return true;

			//check windows lang
			if (key > 128) {
				if (FarsiType.KeyBoardError == 0) {
					FarsiType.Disable();
				} else {
				alert("لطفا زبان ویندوز را در حالت انگلیسی قرار دهید");
					return false;
				}
			}

			// If Farsi
			if (FarsiType.Type && isFarsi) {

				//check CpasLock
				if ((key >= 65 && key <= 90&& !e.shiftKey) || (key >= 97 && key <= 122 ) && e.shiftKey) {
					alert("Caps Lock is On. To prevent entering farsi incorrectly, you should press Caps Lock to turn it off.");
					return false;
				}

				// Shift-space -> ZWNJ
				if (key == 32 && e.shiftKey)
					key = 8204;
				else
					key = FarsiType.farsiKey[key-32];

				key = typeof key == 'string' ? key : String.fromCharCode(key);

				// to farsi
				try {
				
					var docSelection = document.selection;
					var selectionStart = eElement.selectionStart;
					var selectionEnd = eElement.selectionEnd;

					if (typeof selectionStart == 'number') { 
						//FOR W3C STANDARD BROWSERS
						var nScrollTop = eElement.scrollTop;
						var nScrollLeft = eElement.scrollLeft;
						var nScrollWidth = eElement.scrollWidth;
	
						eElement.value = eElement.value.substring(0, selectionStart) + key + eElement.value.substring(selectionEnd);
						setSelectionRange(eElement, selectionStart + key.length, selectionStart + key.length);
		
						var nW = eElement.scrollWidth - nScrollWidth;
						if (eElement.scrollTop == 0) { eElement.scrollTop = nScrollTop }
					} else if (docSelection) {
						var nRange = docSelection.createRange();
						nRange.text = key;
						nRange.setEndPoint('StartToEnd', nRange);
						nRange.select();
					}
	
				} catch(error) {
					try {
						// IE
						e.keyCode = key
					} catch(error) {
						try {
							// OLD GECKO
							e.initKeyEvent("keypress", true, true, document.defaultView, false, false, true, false, 0, key, eElement);
						} catch(error) {
							//OTHERWISE
							if (FarsiType.UnSupportedAction == 0) {
							alert('امکان تایپ فارسی مقدور نمی باشد');
								FarsiType.Disable();
								var Dis = document.getElementById('disableFarsiType')
								if (Dis != null) {
									Dis.disabled = true;
								}
								return false;
							} else {
								eElement.value += key;					
							}
						}
					}
				}
				
				var evt = e || window.event;
				if (evt.preventDefault)
					evt.preventDefault();
				else
				    evt.returnValue = false;
			}
		}
		return true;
	}

	if (FarsiType.ShowChangeLangButton == 1) { z.bottelm.onmouseup = ChangeLang; }
	if (FarsiType.ChangeDir == 2) { z.Direlm.onmouseup = ChangeDirection; }
	z.onkeypress = Convert;
	z.onkeyup = isChangeLang;
}


function PreInit()
	{
	  if (window.attachEvent) {
	    FarsiType.init();
}
else if (window.addEventListener) {
 FarsiType.init();
	}
	}
	
	/*
	if (window.attachEvent) {
	window.attachEvent('onload', FarsiType.init)
} else if (window.addEventListener) {
	window.addEventListener('load', FarsiType.init, false)
}
*/