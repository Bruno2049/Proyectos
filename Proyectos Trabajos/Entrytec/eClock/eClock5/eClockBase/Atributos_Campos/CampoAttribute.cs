using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class CampoAttribute : Attribute
    {

        private object m_ValorPredeterminado = null;
        public object ValorPredeterminado
        {
            get { return m_ValorPredeterminado; }
            set
            {
                m_ValorPredeterminado = value;
            }
        }

        private bool m_SoloLectura = false;
        public bool SoloLectura
        {
            get
            {
                return m_SoloLectura;
            }
            set
            {
                m_SoloLectura = value;
            }
        }

        private bool m_Requerido = false;
        /// <summary>
        /// Indica si el campo es obligatorio
        /// </summary>
        public bool Requerido
        {

            get
            {

                return m_Requerido;
            }
            set
            {
                m_Requerido = value;
            }
        }

        public string Etiqueta
        {
            get { return this.GetType().ToString(); }
        }

        public string Ayuda
        {
            get { return "Ayuda" + this.GetType().ToString(); }
        }



        public virtual bool ToBool(object Valor)
        {
            try
            {
                ///Si es diferente de nulo intentara convertir y en caso de no poder usará el valor predeterminado
                if (Valor != null)
                    return Convert.ToBoolean(Valor);
            }
            catch { }
            return CeC.Convierte2Bool(ValorPredeterminado);
        }

        /// <summary>
        /// Convierte el valor del campo actual a texto
        /// </summary>
        /// <returns>regresa el texto convertido o "" si es nulo el valor </returns>
        public virtual string ToString(object Valor)
        {
            try
            {
                ///Si es diferente de nulo intentara convertir y en caso de no poder usará el valor predeterminado
                if (Valor != null)
                    return Convert.ToString(Valor);
            }
            catch { }
            return CeC.Convierte2String(ValorPredeterminado, "");
        }

        public virtual DateTime? ToDateTime(object Valor)
        {
            try
            {
                ///Si es diferente de nulo intentara convertir y en caso de no poder usará el valor predeterminado
                if (Valor != null)
                    Convert.ToDateTime(Valor);
                return CeC.Convierte2DateTime(Valor);
            }
            catch { }

            if (ValorPredeterminado == null)
                return null;
            return CeC.Convierte2DateTime(ValorPredeterminado);
        }

    }
}
