using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Globalization;

namespace eClockSync
{
    class CeVT_AS215 : CeTerminalSync
    {
        
        /// <summary>
        /// Obtiene el numero de serie en formato FacilityCode/Card Serial Number
        /// </summary>
        /// <param name="ValorVT">Valor en el software veritrax ej 002400000000018D</param>
        /// <returns></returns>
        public string ObtenNSProximidad(string ValorVT)
        {
            string FC = ValorVT.Substring(0, 4);
            string CSN = ValorVT.Substring(4);
            string NS = "";
            return NS = Hex2Int(FC).ToString() + "/" + Hex2Int(CSN).ToString();
        }
        protected void AgregaChecada(short Usuario, DateTime FechaHora, TipoAccesos TAcceso)
        {
            DS_VT_AS215_BDTableAdapters.tblCardTableAdapter TACard = new eClockSync.DS_VT_AS215_BDTableAdapters.tblCardTableAdapter();
            DS_VT_AS215_BD.tblCardDataTable TablaCard = TACard.GetDataByiUser(Usuario);
            if (TablaCard.Rows.Count > 0)
            {
                string NS = ObtenNSProximidad(TablaCard[0].tCodeStr);
                //string NS = Convert.ToInt32(TablaCard[0].iSiteCode) + "/" + Convert.ToInt32(TablaCard[0].tCard);
                AgregaChecada(NS, FechaHora, TAcceso);
            }

        }
        public override Errores PoleoChecadas()
        {
            short UltimoIDTemp = 0;
            short UltimoID = 0;
            DateTime UltimoIDGuar = new DateTime(2002, 09, 24);
            CultureInfo culture = new CultureInfo("es-MX");
            try
            {
                string[] Valores = IUChecadas.Split(new char[] { '|' });
                if (Valores.Length >= 1)
                    UltimoID = Convert.ToInt16(Valores[0]);
                if (Valores.Length >= 2)
                    UltimoIDTemp = Convert.ToInt16(Valores[1]);


                UltimoIDGuar = Convert.ToDateTime(Valores[2], culture);
            }
            catch
            {

            }
            if (UltimoID == 5000)
                UltimoID = 0;
            if (UltimoIDGuar > DateTime.Now)
            {
                UltimoIDGuar = DateTime.Today.AddDays(-5);
                UltimoID = 0;
            }

            try
            {
                DS_VT_AS215_BDTableAdapters.tblEventsBuffTableAdapter TABuf = new eClockSync.DS_VT_AS215_BDTableAdapters.tblEventsBuffTableAdapter();
                short No = Convert.ToInt16(TABuf.NoRegistros());
                if(No != UltimoIDTemp)
                {
                    UltimoIDTemp = No;
                    DS_VT_AS215_BD.tblEventsBuffDataTable TablaBuf = TABuf.GetData();
                    foreach (DS_VT_AS215_BD.tblEventsBuffRow Fila in TablaBuf)
                    {
                        try
                        {
                            DateTime FechaHora = Fila.dEvent_Date + Fila.dEvent_Time.TimeOfDay;
                            if (Fila.IsiUserNumNull() || Fila.iUserNum == 0)
                            {
                                if (!Fila.IstCardCodeNull() && Fila.tCardCode.Length > 0)
                                {
                                    string NS = ObtenNSProximidad(Fila.tCardCode);
                                    AgregaChecada(NS, FechaHora, TipoAccesos.Correcto);
                                }
                            }
                            else
                            {
                                if (Fila.iUserNum > 0)
                                    AgregaChecada(Fila.iUserNum, FechaHora, TipoAccesos.Correcto);
                            }
                        }
                        catch (Exception ex)
                        {
                            CeLog2.AgregaError(ex);
                        }

                    }
                }
                
            }
            catch
            {

            }
            try
            {                
                DS_VT_AS215_BDTableAdapters.tblEventsTableAdapter TA = new eClockSync.DS_VT_AS215_BDTableAdapters.tblEventsTableAdapter();
                DS_VT_AS215_BD.tblEventsDataTable Tabla = TA.GetDataByiEventNum(UltimoID,UltimoIDGuar,DateTime.Today.AddDays(1));
                foreach(DS_VT_AS215_BD.tblEventsRow Fila in Tabla)
                {
                    try
                    {
                        DateTime FechaHora = Fila.dEvent_Date + Fila.dEvent_Time.TimeOfDay;
                        if (Fila.IsiUserNumNull() || Fila.iUserNum == 0)
                        {
                            if (!Fila.IstCardCodeNull() && Fila.tCardCode.Length > 0)
                            {
                                string NS = ObtenNSProximidad(Fila.tCardCode);

                                AgregaChecada(NS, FechaHora, TipoAccesos.Correcto);
                            }
                        }
                        else
                        {
                            if (Fila.iUserNum > 0)
                                AgregaChecada(Fila.iUserNum,FechaHora, TipoAccesos.Correcto);
                        }
                        UltimoIDGuar = Fila.dtSave;
                        UltimoID = Fila.iEventNum;
                    }
                    catch(Exception ex)
                    {
                        CeLog2.AgregaError(ex);
                    }

                }

                IUChecadasNuevo = UltimoID.ToString() + "|" + UltimoIDTemp.ToString() + "|" + UltimoIDGuar.ToString(culture);
                return Errores.Correcto;
            }
            catch
            {

            }
            return Errores.Error_Conexion;

        }
    }
}
