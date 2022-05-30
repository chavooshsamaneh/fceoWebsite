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

var FarsiType =
{
	farsiKey : [
   0x0020, 0x0021, 0x061B, 0x066B, 0x00A4, 0x066A, 0x060C, 0x06AF,
   0x0029, 0x0028, 0x002A, 0x002B, 0x0648, 0x002D, 0x002E, 0x002F,
   0x0030, 0x0031, 0x0032, 0x0033, 0x0034, 0x0035, 0x0036, 0x0037,
   0x0038, 0x0039, 0x003A, 0x0643, 0x003E, 0x003D, 0x003C, 0x061F,
   0x066C, 0x0624, 0x200C, 0x0698, 0x0649, 0x064D, 0x0625, 0x0623,
   0x0622, 0x0651, 0x0629, 0x00BB, 0x00AB, 0x0621, 0x004E, 0x005D,
   0x005B, 0x0652, 0x064B, 0x0626, 0x064F, 0x064E, 0x0056, 0x064C,
   0x0058, 0x0650, 0x0643, 0x062C, 0x067E, 0x0686, 0x00D7, 0x0640,
   0x200D, 0x0634, 0x0630, 0x0632, 0x064A, 0x062B, 0x0628, 0x0644,
   0x0627, 0x0647, 0x062A, 0x0646, 0x0645, 0x067E, 0x062F, 0x062E,
   0x062D, 0x0636, 0x0642, 0x0633, 0x0641, 0x0639, 0x0631, 0x0635,
   0x0637, 0x063A, 0x0638, 0x007D, 0x007C, 0x007B, 0x007E ],
	Type : true,
	counter : 0
}

FarsiType.enable_disable = function(Dis) {
	var invis, obj;

	if (!Dis.checked)  {
		FarsiType.Type = true;
		invis = 'visible';
	}
	else {
		FarsiType.Type = false;
		invis = 'hidden';
	}

	for (var i=1; i<= FarsiType.counter; i++) {
		obj = document.getElementById('FarsiType_button_' + i);
		obj.style.visibility = invis;
	}
}

FarsiType.init = function() {
	var Inputs = document.getElementsByTagName('INPUT');	
	    for (var i=0; i<Inputs.length; i++) {
		    if (Inputs[i].type.toLowerCase() == 'text' && Inputs[i].lang.toLowerCase() == 'fa') {
			    FarsiType.counter++;
			    new FarsiType.KeyObject(Inputs[i], FarsiType.counter);
		    }
	
	}

	var Areas = document.getElementsByTagName('TEXTAREA');	
	
	for (var i=0; i<Areas.length; i++) {	
		if (Areas[i].lang.toLowerCase() == 'fa') {
			FarsiType.counter++;
			new FarsiType.KeyObject(Areas[i], FarsiType.counter);
		}
	}

	var Dis = document.getElementById('disableFarsiType')
	if (Dis != null) {
		FarsiType.enable_disable (Dis);
		Dis.onclick = new Function( "FarsiType.enable_disable (this);" )
	}
		FarsiType.SetLabels();
}

FarsiType.KeyObject = function(z,x) {
if(isFarsi)
{
	z.insertAdjacentHTML("beforeBegin", "<div align=left><input type='button' id=FarsiType_button_"+x+" style='border: none; background-image:none;background-color:red; font-size:10; color:white; font-family:tahoma; padding: 1px; margin: 1px; width: auto; height: auto;' value='FA' /></div>");
	z.bottelm = document.getElementById ('FarsiType_button_' + x);
  
	//z.farsi = true;
	z.farsi = isFarsi;
	z.style.textAlign = "right";
	z.style.direction = "rtl";
	z.bottelm.title = 'تغییر زبان به انگلیسی';
}
else
{
    z.insertAdjacentHTML("beforeBegin", "<div align=left><input type='button' id=FarsiType_button_"+x+" style='border: none; background-image:none;background-color:red; font-size:10; color:white; font-family:tahoma; padding: 1px; margin: 1px; width: auto; height: auto;' value='EN' /></div>");
	z.bottelm = document.getElementById ('FarsiType_button_' + x);
  
	//z.farsi = true;
	z.farsi = isFarsi;
	z.style.textAlign = "left";
	z.style.direction = "ltr";
	z.bottelm.title = 'تغییر زبان به فارسی';
}

	setSelectionRange = function(input, selectionStart, selectionEnd) {	
		input.focus()
		input.setSelectionRange(selectionStart, selectionEnd)
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
			            if(SMSCount == 5)
			                SMSCount = 4;		             
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
			            if(SMSCount == 5)
			                SMSCount = 4;		                		             
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
          if(SMSCount>=4)
          {         
            return ;
          }
          else
          {
		        //if(z.farsi)
		         if(isFarsi)
		        {			        
		          var SMSBody =null;
                   SMSBody=document.getElementById(txtAreaName);
		       
                        var t =((SMSBody.value.length)/70);
                     
			            SMSCount= (parseInt(t.toString()))+1; 	
			            if(SMSCount == 5)
			                SMSCount = 4;		             
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
			            if(SMSCount == 5)
			                SMSCount = 4;		                		             
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
	}

	ChangeLang = function() {
	var SMSBody=null;
	    SMSBody=document.getElementById(txtAreaName);
	  
		//if (z.farsi) 
		 if(isFarsi)
		{
			z.style.textAlign = "left";
			z.style.direction = "ltr";
			//z.farsi = false;
			isFarsi=false;
			z.farsi = isFarsi;
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
		else 
		{
			z.style.textAlign = "right";
			z.style.direction = "rtl";
			//z.farsi = true;
			isFarsi=true;
			z.farsi = isFarsi;
			z.bottelm.value = "FA";
			z.bottelm.title = 'تغییر زبان به انگلیسی';
			MaxLenght = 70;
			 lblSMSLenght.SetText(MaxLenght-SMSBody.value.length)
			//lblSMSLenght.SetText(MaxLenght.toString())
		}
		z.focus();
	}

	Convert = function(e) {
	
            if (e == null) e = window.event;
		      var key = e.keyCode ? e.keyCode : e.charCode;
            if(key==27)
		    {	
		    
		     var SMSBody=null;// =document.forms[0].namedItem('ctl00$ContentPlaceHolder1$RoundPanelSMSText$txtbSMSBody');	           
		         SMSBody=document.getElementById(txtAreaName);
		    // lblSMSLenght.SetText(70);	
		        SMSCount=1;
		        lblSMSCount.SetText(SMSCount);	
		        lblSMSLenght.SetText(((SMSCount*70) -SMSBody.value.length).toString());  		        
		    }
		if (FarsiType.Type) {


			if (e == null) e = window.event;
			 
		 if(SMSCount>4)
          {                  
            	return false;
          }
		
			eElement = (e.srcElement) ? e.srcElement : e.originalTarget;            
			var key = e.keyCode ? e.keyCode : e.charCode;
			if (navigator.userAgent.toLowerCase().indexOf('opera')>-1) key = e.which;

			if ( (e.charCode != null) && (e.charCode != key) )	return;
			if (e.ctrlKey || e.altKey || e.metaKey || key == 13 || key == 27 || key == 8) return;

			if (key>128){
				alert("لطفا زبان ویندوز را در حالت انگلیسی قرار دهید");
				return false;
			}

			if (z.farsi && key > 31 && key < 128) {

				if ( (key >= 65 && key <= 90) && !e.shiftKey ) {
					alert("Caps Lock is On. To prevent entering farsi incorrectly, you should press Caps Lock to turn it off.");
					return false;
				}
				else if ( (key >= 97 && key <= 122 ) && e.shiftKey ) {
					alert("Caps Lock is On. To prevent entering farsi incorrectly, you should press Caps Lock to turn it off.");
					return false;
				}

				if (key == 32 && e.shiftKey)
					key = 8204;
				else
					key = FarsiType.farsiKey[key-32];

				try {
					e.keyCode = key		
						            					
				}
				catch(error) {
					try {
						e.initKeyEvent("keypress", true, true, document.defaultView, false, false, true, false, 0, key, eElement);
					}
					catch(error) {
						try {
							var nScrollTop = eElement.scrollTop;
							var nScrollLeft = eElement.scrollLeft;
							var nScrollWidth = eElement.scrollWidth;

							replaceString = String.fromCharCode(key);

							var selectionStart = eElement.selectionStart;
							var selectionEnd = eElement.selectionEnd;
							eElement.value = eElement.value.substring(0, selectionStart) + replaceString + eElement.value.substring(selectionEnd);
							setSelectionRange(eElement, selectionStart + replaceString.length, selectionStart + replaceString.length);

							var nW = eElement.scrollWidth - nScrollWidth;
							if (eElement.scrollTop == 0) { eElement.scrollTop = nScrollTop }

							e.preventDefault()
						}
						catch(error) {
							alert('امکان تایپ فارسی مقدور نمی باشد')
							FarsiType.Type = false;

							var Dis = document.getElementById('disableFarsiType')
							if (Dis != null) {
								Dis.disabled = true;
								Dis.checked = true;
							}

							for (var i=1; i<= FarsiType.counter; i++) {
								document.getElementById('FarsiType_button_' + i).style.visibility = 'hidden';
							}
							return false;
						}
					}
				}
			}
		}		
		return true;
		
	}

	z.bottelm.onmouseup = ChangeLang;
	z.onkeyup = isChangeLang;
	z.onkeypress = Convert;
}

function PreInit()
	{
	
	//FarsiType.init();
	  if (window.attachEvent) {
	    FarsiType.init();
	//window.attachEvent('onload', FarsiType.init)
}
else if (window.addEventListener) {
 FarsiType.init();
//	window.addEventListener('load', FarsiType.init, false)
	}
	}
	
/*
if (window.attachEvent) {
	window.attachEvent('onload', FarsiType.init);
}
else if (window.addEventListener) {
	window.addEventListener('load', FarsiType.init, false);
}*/

