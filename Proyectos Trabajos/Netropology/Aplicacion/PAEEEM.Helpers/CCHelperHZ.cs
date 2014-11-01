using System;
using System.Collections.Generic;

namespace PAEEEM.Helpers
{
    public class CCHelper
    {
        #region Sample values
        static public Dictionary<string, int[]> tempMops = new Dictionary<string, int[]>
        {
            { "1", new int[] { 23, 4717 } },
            { "10", new int[] { 0, 790 } },
            { "19", new int[] { 33, 6662 } },
            { "25", new int[] { 70, 9174 } },
            { "26", new int[] { 51, 4579 } },
            { "29", new int[] { 98, 4710 } },
            { "30", new int[] { 4, 3767 } },
            { "31", new int[] { 96, 2445 } },
            { "35", new int[] { -1, 7639 } },
            { "40", new int[] { 1, 8586 } },
            { "42", new int[] { 55, 1236 } },
            { "52", new int[] { 78, 3789 } },
            { "59", new int[] { 59, 5676 } },
            { "60", new int[] { 69, 9964 } },
            { "61", new int[] { 2, 6299 } },
            { "75", new int[] { 3, 5042 } },
            { "80", new int[] { 14, 7239 } },
            { "82", new int[] { 22, 3971 } },
            { "96", new int[] { 99, 5390 } },
            { "97", new int[] { 5, 8775 } },
            { "PAEEEMDW06A00071", new int[] { 4, 8775 } },
            { "PAEEEMDW06A00072", new int[] { 4, 665 } },
            {"PAEEEMDM22A00073", new int[]{4, 201}}
        };
        #endregion

        #region Properties
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }

        public string Calle { get; set; }
        public string Numero { get; set; }
        public string NumeroInterior { get; set; }
        public string ColoniaPoblacion { get; set; }
        public string CP { get; set; }
        public string DelegacionMunicipio { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }

        public float ImporteContrato { get; set; }
        public string NumeroFirma { get; set; }
        public string folio { get; set; }
        #endregion

        public int ConsultaCirculo()
        {
            int mop;

            try
            {
                mop = tempMops[NumeroFirma][0];
                folio = tempMops[NumeroFirma][1].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta a Circulo de crédito. " + ex.Message.ToUpper(), ex);
            }

            return mop;
        }
    }
}