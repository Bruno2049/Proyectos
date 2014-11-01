function Enfoca(sControl)
{
	document.getElementById(sControl).focus();
}

function NoVacio(sControl, sMensaje)
{
	var oControl = document.getElementById(sControl);
	if (oControl.value == '')
	{
		alert(sMensaje);
		if (oControl.type != "hidden")
			oControl.focus();
		return false;
	}
	return true;
}

function ValidaCaracteres(sControl, sMensaje)
{
	var sCadena = document.getElementById(sControl).value;
	var patt1 = new RegExp("[a-zA-Z0123456789]");
	var res = patt1.test(sCadena);
	
	if (!res)
	{
		alert(sMensaje);
		Enfoca(sControl);
	}
	return res;
}

function ValidaEmail(sControl, sMensaje)
{
	var sCadena = document.getElementById(sControl).value;
	var res = false;
	if (sCadena.indexOf('@') > 0)
		if (sCadena.indexOf('.') > 0)
			res = true;
	
	if (!res)
	{
		alert(sMensaje);
		Enfoca(sControl);
	}
	return res;
}

function trim(cadena)
{
	for (var i = 0; i < cadena.length; )
	{
		if (cadena.charAt(i) == " ")
			cadena=cadena.substring(i + 1, cadena.length);
		else
			break;
	}

	for (i = cadena.length-1; i >= 0; i = cadena.length-1)
	{
		if (cadena.charAt(i) == " ")
			cadena = cadena.substring(0, i);
		else
			break;
	}
	
	return cadena;
}

function SoloNumeros(e)
{
	var charCode = (e.which) ? e.which : event.keyCode;
	if (charCode >= 65 && charCode <= 90)		// a = 65, z = 90
		return false;
	if (charCode >= 191)
		return false;
    return false;
}



//PERMITE NUMEROS ENTEROS Y DECIMALES NO DEJA INTRODUCIR CARACTERES COMO ESPACIO +,-,*,/;
function SoloNumeros2(e)
{
    
	var charCode = (e.which) ? e.which : event.keyCode;

	if (charCode <= 13 || (charCode >= 48 && charCode <= 57) || charCode == 46){
	    return true;
	}
	else{
	    return false;
	}
	
	
}
function SoloNumeros3(e)
{
	var charCode = (e.which) ? e.which : event.keyCode;

	if (charCode <= 13 || (charCode >= 48 && charCode <= 57)){
	    return true;
	}
	else{
	    return false;
	}
}



function SoloMayus(oControl, e)
{
	var charCode = (e.which) ? e.which : event.keyCode;
	if (!oControl.readOnly)
	{
		if (charCode >= 65 && charCode <= 90)		// a = 65, z = 90
		{
		    if (oControl.value.length < oControl.maxLength)
			    oControl.value += String.fromCharCode(charCode);
			return false;
		}
	}
    return false;
}

function ValidaFecha(oControl)
{
	var patt = new RegExp("[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]");
	if (!patt.test(oControl.value))
	{
		oControl.value = "dd/mm/yyyy";
		oControl.select();
	}
}

function ValidaHora(oControl)
{
	var patt = new RegExp("[0-9][0-9]:[0-9][0-9]:[0-9][0-9]");
	if (!patt.test(oControl.value))
	{
		oControl.value = "HH:MM:SS";
		oControl.select();
	}
}
