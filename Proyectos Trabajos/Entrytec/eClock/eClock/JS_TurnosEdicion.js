var genAsist;//
var chkGenera;
var hdnGenAsist;
var tipoAsist;//
var tblTipoAsist;
var optAsistHora;
var optAsistJor;
var hdnTipoAsist;
var tblBloque;
var tblTolerancia;//
var tol;
var tblToleranciaCom; //
var tolComida;
var tblTiempoEx;//
var mismoHorario;//
var tblMismoHorario;
var chkAsistDias;
var hdnMismoHorario;
var restAcceso;//
var tblRestAcceso;
var chkRestAcceso;
var hdnRestAcceso;
var comida;//
var tblComida;
var optSinComida;
var optComidaHorario;
var optComidaTiempo;
var optComidaAvanzado;
var hdnComida;
var pnlHorarioDias;
var tblHorarioDias;//
var EntMin;//
var Ent;//
var EntMax;//
var ComFija;//
var TieCom; //
var SalMin;//
var Sal; //
var SalMax; //
var Jornada;
var inicio;
var ctrlTxt;
var displayBlock;
var displayNone;
var displayInline;
var disabledTrue;
var disabledFalse;
var dias;
var cols;
var tblDiasGral;
var hdnTurno;
var edicion;
// -
function InicializarVariables()
{  
//debugger;
    genAsist;//
    chkGenera = document.getElementById('chkGenera');
    hdnGenAsist = document.getElementById('hdnGenAsist');
    tipoAsist;//
    tblTipoAsist = document.getElementById('tblTipoAsist');
    optAsistHora = document.getElementById('optAsistHora');
    optAsistJor = document.getElementById('optAsistJor');
    hdnTipoAsist = document.getElementById('hdnTipoAsist');
    tblBloque = document.getElementById('tblBloque'); //
    tblTolerancia = document.getElementById('tblTolerancia');//
    tol = parseInt(document.getElementById('WebPanel3_txtTol').value);
    tblToleranciaCom = document.getElementById('tblToleranciaCom'); //
    tolComida = parseInt(document.getElementById('WebPanel3_txtTolComida').value);

    tblTiempoEx = document.getElementById('tblTiempoEx');//
    mismoHorario;//
    tblMismoHorario = document.getElementById('tblMismoHorario');
    chkAsistDias = document.getElementById('chkAsistDias');
    hdnMismoHorario = document.getElementById('hdnMismoHorario');
    restAcceso;//
    tblRestAcceso = document.getElementById('tblRestAcceso');
    chkRestAcceso = document.getElementById('chkRestAcceso');
    hdnRestAcceso = document.getElementById('hdnRestAcceso');
    comida;//
    tblComida = document.getElementById('tblComida');
    optSinComida = document.getElementById('optSinComida');
    optComidaHorario = document.getElementById('optComidaHorario');
    optComidaTiempo = document.getElementById('optComidaTiempo');
    optComidaAvanzado = document.getElementById('optComidaAvanzado');
    hdnComida = document.getElementById('hdnComida');
    pnlHorarioDias = document.getElementById('WebPanel1');
    tblHorarioDias = document.getElementById('tblHorarioDias');
    EntMin = document.getElementById('EntMin');//
    Ent = document.getElementById('Ent');//
    EntMax = document.getElementById('EntMax');//
    ComFija = document.getElementById('ComFija');//
    TieCom = document.getElementById('TieCom'); //
    SalMin = document.getElementById('SalMin'); //
    Sal = document.getElementById('Sal'); //
    SalMax = document.getElementById('SalMax'); //
    Jornada = document.getElementById('Jornada'); //
    inicio = "document.getElementById('";
    ctrlTxt = "WebPanel1_";
    displayBlock = "').style.display = 'block';";
    displayNone = "').style.display = 'none';";
    displayInline = "').style.display = 'inline';";
    disabledTrue = "').disabled = true;";
    disabledFalse = "').disabled = false;";
    dias = new Array("Lun","Mar","Mie","Jue","Vie","Sab","Dom");
    cols = new Array("A", "AA", "B", "C", "D", "E", "F", "G", "SalMin", "H", "SalMax", "Jornada", "I");
    tblDiasGral = document.getElementById('tblDiasGral');//
    hdnTurno = document.getElementById('hdnTurno');
}

function InicializarNuevo()
{
//debugger;
    edicion = false;//eval(inicio + "_ctl0_ContentPlaceHolder1_lblError" + displayNone);
    GenerarAsistencia();
    MostrarDias(edicion);
    Comida();
    RestringirAcceso();
    TpoAsistencia();/**/
}

function InicializarEdicion(GA, TA, MH, RA, C)
{
    //debugger;
    edicion = true;
    if (GA == 1)
        chkGenera.checked = true;
    else
        chkGenera.checked = false;
    if (TA == 0)
        optAsistHora.checked = 'checked';
    else
        optAsistJor.checked = 'checked';
    if (MH == 1)
        chkAsistDias.checked = true;
    else
        chkAsistDias.checked = false;
    if (RA == 1)
        chkRestAcceso.checked = true;
    else
        chkRestAcceso.checked = false;
    switch (C)
    {
        case 0:
            optSinComida.checked = 'checked';
            break;
        case 1:
            optComidaHorario.checked = 'checked';
            break;
        case 2:
            optComidaTiempo.checked = 'checked';
            break;
        case 3:
            optComidaAvanzado.checked = 'checked';
            break;
    }
    for (var i = 0; i < 7; i++)
    {
       AplicarTurnoDiaHdn(dias[i]);   
    }
   ActualizaTurnoFEnd();
}
function ActualizaTurnoFEnd()
{
    GenerarAsistencia();
    RestringirAcceso();
    TpoAsistencia();
    
    Comida();
    
    MostrarDias(edicion);
}
function GenerarAsistencia()
{
//debugger;
    if (chkGenera.checked)
    {
        hdnGenAsist.value = 1;
        return;
        optAsistHora.disabled = false;
        optAsistJor.disabled = false;
        chkAsistDias.disabled = false;
        chkRestAcceso.disabled = false;
        optSinComida.disabled = false;
        optComidaHorario.disabled = false;
        optComidaTiempo.disabled = false;
        optComidaAvanzado.disabled = false;
        tblBloque.style.display = 'block';
        tblTolerancia.style.display = 'block';
        MostrarToleranciaCom();
        tblTipoAsist.style.display = 'block';
        tblTiempoEx.style.display = 'block';
        tblMismoHorario.style.display = 'block';
        tblRestAcceso.style.display = 'block';
        tblComida.style.display = 'block';
        pnlHorarioDias.style.display = 'block';
        tblHorarioDias.style.display = 'block';
        tblDiasGral.style.display = 'block';
        
        if (!optAsistHora.checked)
            tblRestAcceso.style.display = 'none';
            
    }
    else
    {
        hdnGenAsist.value = 0;
        return;
        optAsistHora.disabled = true;
        optAsistJor.disabled = true;
        chkAsistDias.disabled = true;
        chkRestAcceso.disabled = true;
        optSinComida.disabled = true;
        optComidaHorario.disabled = true;
        optComidaTiempo.disabled = true;
        optComidaAvanzado.disabled = true;
        tblBloque.style.display = 'none';
        tblTolerancia.style.display = 'none';
        MostrarToleranciaCom();
        tblTipoAsist.style.display = 'none';
        tblTiempoEx.style.display = 'none';
        tblMismoHorario.style.display = 'none';
        tblRestAcceso.style.display = 'none';
        
        tblComida.style.display = 'none';
        pnlHorarioDias.style.display = 'none';
        tblHorarioDias.style.display = 'none';
        tblDiasGral.style.display = 'none';
        
    }
}

function MostrarDias(edicion)
{
    //debugger;
    var ContY;
    var ContX;
   // if(edicion == undefined)
     //   edicion = false;
    if (chkAsistDias.checked)
    {
        for (i=2; i<8; i++)
        {
            for (j=0; j<13; j++)
            {
                eval(inicio + cols[j] + i + displayNone);
            }
            if (!edicion)
                eval(inicio + "hdn" + dias[i-1] + "').value = 0;");
        }
        hdnMismoHorario.value = 1;
        eval(inicio + "chkAsistLun').checked = 'checked'");
        AplicarTurnoDia("chkAsistLun");
        eval(inicio + "chkAsistLun" + disabledTrue);
        eval(inicio + "WebPanel1_lblLun" + displayNone);
        eval(inicio + "WebPanel1_lblGral" + displayInline);
        tblDiasGral.style.display = 'block';
        
    }
    else
    {
     
        for (ContX=2; ContX<8; ContX++)
        {
          
          for (ContY=0; ContY<13; ContY++)
            {
               
               eval(inicio + cols[ContY] + ContX + displayBlock);  
            }
          if (!edicion)
                eval(inicio + "hdn" + dias[ContX-1] + "').value = 1;");
        }
        hdnMismoHorario.value = 0;        
        eval(inicio + "chkAsistLun" + disabledFalse);
        eval(inicio + "WebPanel1_lblLun" + displayInline);
        eval(inicio + "WebPanel1_lblGral" + displayNone);
        tblDiasGral.style.display = 'none';
      
    }
}

function Comida()
{
    //debugger;
    
    if(optSinComida.checked)
    {
        ComFija.style.display = 'none';
        TieCom.style.display = 'none';
        hdnComida.value = 0;
    }
    if(optComidaHorario.checked)
    {
        ComFija.style.display = 'block';
        TieCom.style.display = 'none';
        hdnComida.value = 1;
    }
    if(optComidaTiempo.checked)
    {
        ComFija.style.display = 'none';
        TieCom.style.display = 'block';
        hdnComida.value = 2;
    }
    if (optComidaAvanzado.checked)
    {
        ComFija.style.display = 'block';
        TieCom.style.display = 'block';
        hdnComida.value = 3;
    }
    
}

function RestringirAcceso()
{
    //debugger;

    if (chkRestAcceso.checked)
    {
        EntMin.style.display = 'block';
        EntMax.style.display = 'block';
        SalMin.style.display = 'block';
        SalMax.style.display = 'block';        
        hdnRestAcceso.value = 1;
    }
    else
    {
        EntMin.style.display = 'none';
        EntMax.style.display = 'none';
        SalMin.style.display = 'none';
        SalMax.style.display = 'none';        
        hdnRestAcceso.value = 0;
    }
    Comida();
}
function MostrarComida() {

}
function MostrarToleranciaCom()
{

    //return;
    if (chkGenera.checked && !optSinComida.checked) {
        tblToleranciaCom.style.display = 'block';
    }
    else
        tblToleranciaCom.style.display = 'none';
}
function TpoAsistencia()
{
//debugger;
    if (optAsistHora.checked)
    {
//        eval(inicio + ctrlTxt.substring(0,ctrlTxt.length-2) + "3_txtJornadaTrab" + disabledTrue);
        tblRestAcceso.style.display = 'block';
        tblBloque.style.display = 'none';
        tblTolerancia.style.display = 'block';
        MostrarToleranciaCom();
        EntMin.style.display = 'none';
        Ent.style.display = 'block';
        EntMax.style.display = 'none';
        ComFija.style.display = 'none';
        SalMin.style.display = 'none';
        Sal.style.display = 'block';
        SalMin.style.display = 'none';
        Jornada.style.display = 'none';
        optComidaHorario.disabled = false;
        hdnTipoAsist.value = 0;
        RestringirAcceso();
    }
    else
    {
//        eval(inicio + ctrlTxt.substring(0, ctrlTxt.length - 2) + "3_txtJornadaTrab" + disabledFalse);
        
        tblRestAcceso.style.display = 'none';
        tblBloque.style.display = 'block';
        tblTolerancia.style.display = 'block';
        MostrarToleranciaCom();
        EntMin.style.display = 'block';
        Ent.style.display = 'block';
        EntMax.style.display = 'block';
        ComFija.style.display = 'none';
        SalMin.style.display = 'none';
        Sal.style.display = 'none';
        SalMax.style.display = 'none';
        Jornada.style.display = 'block';
        optComidaHorario.disabled = true;
        if(optComidaHorario.checked)
            optSinComida.checked = true;
        hdnTipoAsist.value = 1;
        
    }
}

function Validar()
{
 //debugger;
    var valido = true;
    var errores = true;
    var horasDia = new Array();
    hdnTurno.value = "";
    if(chkAsistDias.checked)
    {
        horasDia[0] = new Array();
        horasDia[0][0] = eval(inicio + "chkAsist" + dias[0] + "').checked");
        horasDia[0][1] = eval(inicio + ctrlTxt +"EntMin" + dias[0] + "').value");
        horasDia[0][2] = eval(inicio + ctrlTxt +"Ent" + dias[0] + "').value");
        horasDia[0][3] = eval(inicio + ctrlTxt +"EntMax" + dias[0] + "').value");
        horasDia[0][4] = eval(inicio + ctrlTxt +"SalCom" + dias[0] + "').value");
        horasDia[0][5] = eval(inicio + ctrlTxt +"RegCom" + dias[0] + "').value");
        horasDia[0][6] = eval(inicio + ctrlTxt +"Sal" + dias[0] + "').value");
        horasDia[0][7] = eval(inicio + ctrlTxt +"TieCom" + dias[0] + "').value");
        horasDia[0][8] = eval(inicio + "chkSigDia" + dias[0] + "').checked");
    }
    else
        for (i=0; i<7; i++)
        {
            horasDia[i] = new Array();
            horasDia[i][0] = eval(inicio + "chkAsist" + dias[i] + "').checked");
            horasDia[i][1] = eval(inicio + ctrlTxt +"EntMin" + dias[i] + "').value");
            horasDia[i][2] = eval(inicio + ctrlTxt +"Ent" + dias[i] + "').value");
            horasDia[i][3] = eval(inicio + ctrlTxt +"EntMax" + dias[i] + "').value");
            horasDia[i][4] = eval(inicio + ctrlTxt +"SalCom" + dias[i] + "').value");
            horasDia[i][5] = eval(inicio + ctrlTxt +"RegCom" + dias[i] + "').value");
            horasDia[i][6] = eval(inicio + ctrlTxt +"Sal" + dias[i] + "').value");
            horasDia[i][7] = eval(inicio + ctrlTxt +"TieCom" + dias[i] + "').value");
            horasDia[i][8] = eval(inicio + "chkSigDia" + dias[i] + "').checked");
            alert(horasDia[i]);
        }
    if(chkAsistDias.checked)
    {
        if(ValidarHoras(horasDia[0]))
            hdnTurno.value = horasDia;
            else{
                eval(inicio + "lblError" + displayBlock);
                }
    }
    else
        for(i=0; i<7; i++)
        {
            if(ValidarHoras(horasDia[i]))
            {
                hdnTurno.value += horasDia[i];
                eval(inicio + "lblError" + displayBlock);
            }
            else
            {
                eval(inicio + "lblError" + displayBlock);
                break;
            }
        }
}

function ValidarHoras(horasDia)
{
//debugger;
    var Errores = true;
    var asist = horasDia[0];
    var entMin = horasDia[1].split(":");
    var entMinInt = entMin[0]*60 + parseInt(entMin[1]);
    var ent = horasDia[2].split(":");
    var entInt = ent[0]*60 + parseInt(ent[1]);
    var entMax = horasDia[3].split(":");
    var entMaxInt = entMax[0]*60 + parseInt(entMax[1]);
    var salCom = horasDia[4].split(":");
    var salComInt = salCom[0]*60 + parseInt(salCom[1]);
    var regCom = horasDia[5].split(":");
    var regComInt = regCom[0]*60 + parseInt(regCom[1]);
    var sal = horasDia[6].split(":");
    var salInt = sal[0]*60 + parseInt(sal[1]);
    var tieCom = horasDia[7];
    var sigDia = horasDia[8];
    if(!asist)
        return Errores;
    else
    {
        if (optAsistHora.checked)
        {
            if (!chkRestAcceso.checked)
            {
                if (optSinComida.checked)
                    if ((salInt - (entInt + tol)) < 0)
                        if (!sigDia)
                            Errores =  false;
                if (optComidaHorario.checked || optComidaAvanzado.checked)
                {
                    if ((salComInt - (entInt + tol)) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((regComInt - salComInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((salInt - regComInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                }
                if (optComidaTiempo.checked || optComidaAvanzado.checked)
                    if ((salInt - (entInt + tol + tieCom)) < 0)
                        if (!sigDia)
                            Errores =  false;
            }
            else
            {
                if (optSinComida.checked)
                {
                    if ((entInt - entMinInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((entMaxInt - (entInt + tol)) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((salInt - entMaxInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                }
                if (optComidaHorario.checked || optComidaAvanzado.checked)
                {
                    if ((entInt - entMinInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((entMaxInt - (entInt + tol)) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((salComInt - entMaxInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((regComInt - salComInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((salInt - regComInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                }
                if (optComidaTiempo.checked || optComidaAvanzado.checked)
                {
                    if ((entInt - entMinInt) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((entMaxInt - (entInt + tol)) < 0)
                        if (!sigDia)
                            Errores =  false;
                    if ((salInt - (entMaxInt + tieCom)) < 0)
                        if (!sigDia)
                            Errores =  false;
                }
            }
        }
        else
        {
            if (optSinComida.checked)
                if ((entMaxInt - entMinInt) < 0)
                    if (!sigDia)
                        Errores =  false;
            else
                if ((entMaxInt - (entMinInt + tieCom)) < 0)
                    if (!sigDia)
                        Errores =  false;
        }
    }  
    if (!Errores){
        alert("Error: "+ Errores);
        return Errores;
        }
    else{
        alert("Error: "+ Errores);
        return Errores;}
}

function AplicarTurnoDia(ID)
{
   // debugger;
    var horasDias = new Array();

    var dia = ID.substring(8)
    var  sdd= eval(inicio + ID + "').checked");
    if(sdd)
    {
        var tienehorasDia = new Array();
        eval(inicio + ctrlTxt + "EntMin" + dia + disabledFalse);
           tienehorasDia[1] = eval(inicio + ctrlTxt +"EntMin" + dia + "').value");
        eval(inicio + ctrlTxt + "Ent" + dia + disabledFalse);
           tienehorasDia[2] = eval(inicio + ctrlTxt +"Ent" + dia + "').value");
        eval(inicio + ctrlTxt + "EntMax" + dia + disabledFalse);
            tienehorasDia[3] = eval(inicio + ctrlTxt +"EntMax" + dia + "').value");
        eval(inicio + ctrlTxt + "SalCom" + dia + disabledFalse);
            tienehorasDia[4] = eval(inicio + ctrlTxt +"SalCom" + dia + "').value");
        eval(inicio + ctrlTxt + "RegCom" + dia + disabledFalse);
            tienehorasDia[5] = eval(inicio + ctrlTxt +"RegCom" + dia + "').value");
        eval(inicio + ctrlTxt + "TieCom" + dia + disabledFalse);
            tienehorasDia[6] = eval(inicio + ctrlTxt +"TieCom" + dia + "').value");
        eval(inicio + ctrlTxt + "Sal" + dia + disabledFalse);
            tienehorasDia[7] = eval(inicio + ctrlTxt +"Sal" + dia + "').value");
        eval(inicio + "WebPanel1_chkSigDia" + dia + "1" + disabledFalse);
        
         eval(inicio + "hdn" + dia + "').value = 1;");
         var mdia 
         var cargar = false;
      for (mdia=1; mdia<8; mdia++)
        {
            if(tienehorasDia[mdia]== "")
                cargar = true;
            else
            {
             cargar = false;
             break;
            }
        
        }
        if(cargar)
            cargarturno(dia);
       
        
  
    }
    else
    {
        eval(inicio + ctrlTxt + "EntMin" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "Ent" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "EntMax" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "SalCom" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "RegCom" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "TieCom" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "Sal" + dia + disabledTrue);
        eval(inicio + "WebPanel1_chkSigDia" + dia + "1" + disabledTrue);
        eval(inicio + "hdn" + dia + "').value = 0;");
      
    }
}
function cargarturno(dia)
{
//debugger;
    var horasDia = new Array();
    var xdia
    for (xdia=0; xdia<8; xdia++)
        {
            horasDia[xdia] = new Array();
            if (eval(inicio + "chkAsist" + dias[xdia] + "').checked"))
            {
                inicioinfra="igedit_getById('";
                           //igedit_getById("WebDateTimeEdit1").getValue()  
                           
               horasDia[xdia][1] = eval(inicio + ctrlTxt +"EntMin" + dias[xdia] + "').value");
                  if(horasDia[xdia][1]!= "")
                    eval(inicioinfra + ctrlTxt + "EntMin" + dia + "').setValue('"+ horasDia[xdia][1]+"');");
                horasDia[xdia][2] = eval(inicio + ctrlTxt +"Ent" + dias[xdia]  + "').value");
                  if(horasDia[xdia][2]!= "")
                    eval(inicioinfra + ctrlTxt + "Ent" + dia + "').setValue('"+ horasDia[xdia][2]+"');");
                horasDia[xdia][3] = eval(inicio + ctrlTxt +"EntMax" + dias[xdia]  + "').value");
                  if(horasDia[xdia][3]!= "")
                    eval(inicioinfra + ctrlTxt + "EntMax" + dia + "').setValue('"+ horasDia[xdia][3]+"');");
                horasDia[xdia][4] = eval(inicio + ctrlTxt +"SalCom" + dias[xdia] + "').value");
                  if(horasDia[xdia][4]!= "")
                    eval(inicioinfra + ctrlTxt + "SalCom" + dia + "').setValue('"+ horasDia[xdia][4]+"');");
                horasDia[xdia][5] = eval(inicio + ctrlTxt +"RegCom" + dias[xdia]  + "').value");
                  if(horasDia[xdia][5]!= "")
                    eval(inicioinfra + ctrlTxt + "RegCom" + dia + "').setValue('"+ horasDia[xdia][5]+"');");
                horasDia[xdia][6] = eval(inicio + ctrlTxt +"TieCom" + dias[xdia] + "').value");
                  if(horasDia[xdia][6]!= "")
                    eval(inicioinfra + ctrlTxt + "TieCom" + dia + "').setValue('"+ horasDia[xdia][6]+"');");
                horasDia[xdia][7] = eval(inicio + ctrlTxt +"Sal" + dias[xdia]  + "').value");
                  if(horasDia[xdia][7]!= "")
                    eval(inicioinfra + ctrlTxt + "Sal" + dia + "').setValue('"+ horasDia[xdia][7]+"');");
                             
                horasDia[xdia][8] = eval(inicio  + "WebPanel1_" + "chkSigDia" + dias[xdia] + "1').checked");
               
                eval(inicio + "WebPanel1_chkSigDia" + dia + "1" + "').checked ="+ horasDia[xdia][8]+";");
       
                 
                return;
            }
        }
        
        

}
function AplicarTurnoDiaHdn(dia)
{
//debugger;
    var aplica = eval(inicio + "hdn" + dia + "')");
    if(aplica.value == "1")
    {
        eval(inicio + ctrlTxt + "EntMin" + dia + disabledFalse);
        eval(inicio + ctrlTxt + "Ent" + dia + disabledFalse);
        eval(inicio + ctrlTxt + "EntMax" + dia + disabledFalse);
        eval(inicio + ctrlTxt + "SalCom" + dia + disabledFalse);
        eval(inicio + ctrlTxt + "RegCom" + dia + disabledFalse);
        eval(inicio + ctrlTxt + "TieCom" + dia + disabledFalse);
        eval(inicio + ctrlTxt + "Sal" + dia + disabledFalse);
        eval(inicio + "WebPanel1_chkSigDia" + dia + "1" + disabledFalse);
        eval(inicio + "chkAsist" + dia + "').checked = true");
        eval(inicio + "hdn" + dia + "').value = 1;");

    }
    else
    {
        eval(inicio + ctrlTxt + "EntMin" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "Ent" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "EntMax" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "SalCom" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "RegCom" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "TieCom" + dia + disabledTrue);
        eval(inicio + ctrlTxt + "Sal" + dia + disabledTrue);
        eval(inicio + "WebPanel1_chkSigDia" + dia + "1" + disabledTrue);
        eval(inicio + "chkAsist" + dia + "').checked = false");
        eval(inicio + "hdn" + dia + "').value = 0;");
    }
}