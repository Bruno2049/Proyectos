using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockSync5
{
    class CeC_Vectores
    {
        public List<string> Huellas = new List<string>();
        public List<string> Palmas = new List<string>();
        public List<string> Rostros = new List<string>();
        public List<string> IRIS = new List<string>();
        public List<string> HuellasOpciones = new List<string>();
        public List<string> PalmasOpciones = new List<string>();
        public List<string> RostrosOpciones = new List<string>();
        public List<string> IRISOpciones = new List<string>();
        public bool Carga(string Datos, ref List<string> Destino, string Elemento)
        {
            int Pos = 0;
            int PosFinal = 0;
            string ElementoInicial = "<" + Elemento + ">";
            string ElementoFinal = "</" + Elemento + ">";
            while (true)
            {
                Pos = Datos.IndexOf(ElementoInicial, PosFinal);
                if (Pos < 0)
                    break;
                PosFinal = Datos.IndexOf(ElementoFinal, Pos);
                if (PosFinal > Pos)
                {
                    Destino.Add(Datos.Substring(Pos + ElementoInicial.Length, PosFinal - Pos - ElementoInicial.Length));
                }
            }
            return true;
        }
        public bool Carga(string Datos)
        {
            Carga(Datos, ref Huellas, "HUELLA");
            Carga(Datos, ref Palmas, "PALMA");
            Carga(Datos, ref Rostros, "ROSTRO");
            Carga(Datos, ref IRIS, "IRIS");
            Carga(Datos, ref HuellasOpciones, "HUELLA_OPC");
            Carga(Datos, ref PalmasOpciones, "PALMA_OPC");
            Carga(Datos, ref RostrosOpciones, "ROSTRO_OPC");
            Carga(Datos, ref IRISOpciones, "IRIS_OPC");
            return true;
        }

        public static CeC_Vectores Nuevo(byte[] Datos)
        {
            return Nuevo(CeC.ObtenString(Datos));
        }
        public static CeC_Vectores Nuevo(string Datos)
        {
            CeC_Vectores Vec = new CeC_Vectores();
            Vec.Carga(Datos);
            return Vec;
        }
        public string ObtenCadena(List<string> Origen, string Elemento)
        {
            string R = "";

            if (Origen != null)
                foreach (string Cadena in Origen)
                {
                    R += "<" + Elemento + ">" + Cadena + "</" + Elemento + ">";
                }
            return R;
        }
        public override string ToString()
        {
            string R = "";
            R += ObtenCadena(Huellas, "HUELLA");
            R += ObtenCadena(Palmas, "PALMA");
            R += ObtenCadena(Rostros, "ROSTRO");
            R += ObtenCadena(IRIS, "IRIS");
            R += ObtenCadena(HuellasOpciones, "HUELLA_OPC");
            R += ObtenCadena(PalmasOpciones, "PALMA_OPC");
            R += ObtenCadena(RostrosOpciones, "ROSTRO_OPC");
            R += ObtenCadena(IRISOpciones, "IRIS_OPC");
            return R;
        }
        public byte[] ObtenBytes()
        {
            return CeC.ObtenArregloBytes(this.ToString());
        }

    }
}
