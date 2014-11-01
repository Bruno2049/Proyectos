using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_REPORTES
    {
        //Identificador de Reporte
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REPORTE_ID { get; set; }

        //Servidor donde se encuentra el reporte
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int REPORTE_SRV_ID { get; set; }

        //Titulo de Reporte
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_TITULO { get; set; }

        //Descripcion del Reporte
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string REPORTE_DESCRIP { get; set; }

        //Idioma del reporte
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_IDIOMA { get; set; }

        //Nombre de la clase de reporte
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_CLASE { get; set; }

        //Modelo de datos a mostrar
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_MODELO { get; set; }

        //Imagen previa del reporte
        [Campo_BinarioAttribute(false, false, Campo_BinarioAttribute.Tipo.Imagen)]
        public byte[] REPORTE_IMAGEN { get; set; }

        //Precio del reporte
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REPORTE_PRECIO { get; set; }

        //Tamaño de la hoja 
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_TAMANO { get; set; }

        //Campos a capturar para enviar como parametros
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.CamposTextoDiseno)]
        public string REPORTE_PARAM { get; set; }

        //Campos a capturar para enviar como parametros
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.CamposTextoDiseno)]
        public string REPORTE_CONFIG { get; set; }

        //Formatos_Rep_IDs Separados por comas que estarán disponibles para exportar el reporte
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_FORMATOS { get; set; }

        //Indica si el reporte se ha borrado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool REPORTE_BORRADO { get; set; }
    }
}
