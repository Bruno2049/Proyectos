var imagenes = new Array();

function CargarImagenes()
{
/*    imagenes[0] = new Image;
    imagenes[0].src = "Imagenes.Main/Default.jpg";
    imagenes[1] = new Image;
    imagenes[1].src = "Imagenes.Main/E_Nuevo.jpg";
    imagenes[2] = new Image;
    imagenes[2].src = "Imagenes.Main/E_Listado.jpg";
    imagenes[3] = new Image;
    imagenes[3].src = "Imagenes.Main/E_Imprimir.jpg";
    imagenes[4] = new Image;
    imagenes[4].src = "Imagenes.Main/E_Excel.jpg";
    imagenes[5] = new Image;
    imagenes[5].src = "Imagenes.Main/T_CapturaExpress.jpg";
    imagenes[6] = new Image;
    imagenes[6].src = "Imagenes.Main/T_NuevoTurno.jpg";
    imagenes[7] = new Image;
    imagenes[7].src = "Imagenes.Main/T_Asignacion.jpg";
    imagenes[8] = new Image;
    imagenes[8].src = "Imagenes.Main/T_AsignacionExpress.jpg";
    imagenes[9] = new Image;
    imagenes[9].src = "Imagenes.Main/T_AsignacionAvanzada.jpg";
    imagenes[10] = new Image;
    imagenes[10].src = "Imagenes.Main/A_FaltasRetardos.jpg";
    imagenes[11] = new Image;
    imagenes[11].src = "Imagenes.Main/A_AsistenciaAnual.jpg";
    imagenes[12] = new Image;
    imagenes[12].src = "Imagenes.Main/A_Justificaciones.jpg";
    imagenes[13] = new Image;
    imagenes[13].src = "Imagenes.Main/A_AsistenciaSemanal.jpg";
    imagenes[14] = new Image;
    imagenes[14].src = "Imagenes.Main/A_HorasExtra.jpg";
    imagenes[15] = new Image;
    imagenes[15].src = "Imagenes.Main/R_AsistenciaDetallada.jpg";
    imagenes[16] = new Image;
    imagenes[16].src = "Imagenes.Main/R_Asistencia.jpg";
    imagenes[17] = new Image;
    imagenes[17].src = "Imagenes.Main/R_AsistenciaMensual.jpg";
    imagenes[18] = new Image;
    imagenes[18].src = "Imagenes.Main/R_Grafica.jpg";
    imagenes[19] = new Image;
    imagenes[19].src = "Imagenes.Main/R_Accesos.jpg";
    imagenes[20] = new Image;
    imagenes[20].src = "Imagenes.Main/R_HorasExtra.jpg";*/
    document.getElementById('FooterTable').style.display = 'none';
/*    document.getElementById('vistaPrevia').src = imagenes[0].src;*/
}

function ItemHover(menuId, itemId, bHover){
    if(bHover)
    {
        var itemName = itemId.substr(27,itemId.length-27);
        var vistaPrevia = "document.getElementById('vistaPrevia').src = ";
        if (itemName == "Empleados_1") eval(vistaPrevia += "imagenes[2].src;");
        if (itemName == "Empleados_1_1") eval(vistaPrevia += "imagenes[1].src;");
        if (itemName == "Empleados_1_2") eval(vistaPrevia += "imagenes[2].src;");
        if (itemName == "Empleados_1_3") eval(vistaPrevia += "imagenes[3].src;");
        if (itemName == "Empleados_1_4") eval(vistaPrevia += "imagenes[4].src;");
        if (itemName == "Turnos_1") eval(vistaPrevia += "imagenes[8].src;");
        if (itemName == "Turnos_1_1") eval(vistaPrevia += "imagenes[5].src;");
        if (itemName == "Turnos_1_2") eval(vistaPrevia += "imagenes[6].src;");
        if (itemName == "Turnos_1_3") eval(vistaPrevia += "imagenes[7].src;");
        if (itemName == "Turnos_1_4") eval(vistaPrevia += "imagenes[8].src;");
        if (itemName == "Turnos_1_5") eval(vistaPrevia += "imagenes[9].src;");
        if (itemName == "Asistencia_1") eval(vistaPrevia += "imagenes[13].src;");
        if (itemName == "Asistencia_1_1") eval(vistaPrevia += "imagenes[10].src;");
        if (itemName == "Asistencia_1_2") eval(vistaPrevia += "imagenes[11].src;");
        if (itemName == "Asistencia_1_3") eval(vistaPrevia += "imagenes[12].src;");
        if (itemName == "Asistencia_1_4") eval(vistaPrevia += "imagenes[13].src;");
        if (itemName == "Asistencia_1_5") eval(vistaPrevia += "imagenes[14].src;");
        if (itemName == "Reportes_1") eval(vistaPrevia += "imagenes[15].src;");
        if (itemName == "Reportes_1_1") eval(vistaPrevia += "imagenes[15].src;");
        if (itemName == "Reportes_1_2") eval(vistaPrevia += "imagenes[16].src;");
        if (itemName == "Reportes_1_3") eval(vistaPrevia += "imagenes[17].src;");
        if (itemName == "Reportes_1_4") eval(vistaPrevia += "imagenes[18].src;");
        if (itemName == "Reportes_1_5") eval(vistaPrevia += "imagenes[19].src;");
        if (itemName == "Reportes_1_6") eval(vistaPrevia += "imagenes[20].src;");
    }
    else
        document.getElementById('vistaPrevia').src = imagenes[0].src;
}


function SubMenuDisplay(menuId, itemId, bDisplay){
    var strGetMenu = "document.getElementById('ctl0ContentPlaceHolder1Menu";
    var strGetLabel = "document.getElementById('_ctl0_ContentPlaceHolder1_Label";
    var strHide = "_MainM').style.visibility = 'hidden'";
    var strShow = "_MainM').style.visibility = 'visible'";
    var strHideLbl = "').style.visibility = 'hidden'";
    var strShowLbl = "').style.visibility = 'visible'";
    if(bDisplay)
    {
        var menuName = itemId.substr(27,itemId.length-30);
	    switch (menuName)
	    {
	        case "Empleados":
	            eval(strGetMenu + "Turnos" + strHide);
	            eval(strGetMenu + "Asistencia" + strHide);
	            eval(strGetMenu + "Reportes" + strHide);
	            eval(strGetLabel + "2" + strHideLbl);
	            eval(strGetLabel + "3" + strHideLbl);
	            eval(strGetLabel + "4" + strHideLbl);
	        break;
	        case "Turnos":
	            eval(strGetMenu + "Empleados" + strHide);
	            eval(strGetMenu + "Asistencia" + strHide);
	            eval(strGetMenu + "Reportes" + strHide);
	            eval(strGetLabel + "1" + strHideLbl);
	            eval(strGetLabel + "3" + strHideLbl);
	            eval(strGetLabel + "4" + strHideLbl);
	        break;
	        case "Asistencia":
	            eval(strGetMenu + "Empleados" + strHide);
	            eval(strGetMenu + "Turnos" + strHide);
	            eval(strGetMenu + "Reportes" + strHide);
	            eval(strGetLabel + "1" + strHideLbl);
	            eval(strGetLabel + "2" + strHideLbl);
	            eval(strGetLabel + "4" + strHideLbl);
	        break;
	        case "Reportes":
	            eval(strGetMenu + "Empleados" + strHide);
	            eval(strGetMenu + "Turnos" + strHide);
	            eval(strGetMenu + "Asistencia" + strHide);
	            eval(strGetLabel + "1" + strHideLbl);
	            eval(strGetLabel + "2" + strHideLbl);
	            eval(strGetLabel + "3" + strHideLbl);
	        break;
	    }
	}
	else
	{
	    eval(strGetMenu + "Empleados" + strShow);
	    eval(strGetMenu + "Turnos" + strShow);
	    eval(strGetMenu + "Asistencia" + strShow);
	    eval(strGetMenu + "Reportes" + strShow);
	    eval(strGetLabel + "1" + strShowLbl);
	    eval(strGetLabel + "2" + strShowLbl);
	    eval(strGetLabel + "3" + strShowLbl);
	    eval(strGetLabel + "4" + strShowLbl);
	}
}