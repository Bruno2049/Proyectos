using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_SITIOS
    {
        //Identificador unico del registro de sitios
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SITIO_ID { get; set; }
        //Nombre del Sitio
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_NOMBRE { get; set; }
        
        //Nombre del responsable del sitio
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_RESPONSABLE { get; set; }
        //Telefonos de los email  del contacto
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_EMAIL { get; set; }
        //Telefonos de los responsable o del contacto
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_TELEFONOS { get; set; }
        //Campo de direccion
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_DIRECCION_1 { get; set; }
        //Campo de direccion
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_DIRECCION_2 { get; set; }
        //Codigo Postal
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_CP { get; set; }
        //Ciudad
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_CIUDAD { get; set; }
        //Estado
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_ESTADO { get; set; }
        //Pais
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_PAIS { get; set; }

        //Refrencias sobre como llegar al sitio
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_REFERENCIAS { get; set; }
        //Comentarios adicionales sobre el sitio
        [Campo_String(false, false, 256, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_COMENTARIOS { get; set; }

        //Sentencia SQL que indicara a los empleados que automáticamente se asignaran a este campo
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SITIO_CONSULTA { get; set; }
        //Indica desde que hora se podra sincronizar el almacen de vectores
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.Horas)]
        public DateTime SITIO_HDESDE_SVEC { get; set; }
        //Indica Hasta que hora se podra sincronizar el almacen de vectores si esta hora es menor que la anterior significa que termina al dia siguiete
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.Horas)]
        public DateTime SITIO_HHASTA_SVEC { get; set; }

        //Indica cuantos segundos esperará entre cada sincronización
        [Campo_Int(false, false, -1, -1, 1200, Campo_IntAttribute.Tipo.TextBox)]
        public int SITIO_SEGUNDOS_SYNC { get; set; }
       

        //Indica si el sitio se encuentra activo para ser seleccionado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool SITIO_BORRADO { get; set; }

    }
}
