using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class CatalogosPlanilla
    {
        private static readonly CatalogosPlanilla _classInstance = new CatalogosPlanilla();
        public static CatalogosPlanilla ClassInstance { get { return _classInstance; } }

        #region Metodos de Consulta
        public Comp_Plantilla ObtenPlantillaSeleccionada(int? idPlantilla)
        {
            var resultado = AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenPlantillaFiltro(idPlantilla);

            return resultado;
        }

        public List<Campo_Base_Plantilla> ObtenBasesPlantilla(int? idPlantilla)
        {
            var resultado =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenCamposBasePlantilla(idPlantilla);

            return resultado;
        }

        public List<Campo_Customizable_Plantilla> ObtenCustomizablesPlantilla(int? idPlantilla)
        {
            var rownum = 1;

            var resultado = 
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenCamposCustomPlantilla(idPlantilla);

            foreach (var campoCustomizablePlantilla in resultado)
            {
                campoCustomizablePlantilla.Rownum = rownum;
                rownum++;
            }

            return resultado;
        }

        public List<Campo_Customizable> ObtenListaCustomizables()
        {
            var resultado =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenCamposCustomizables();

            //var rownum = 1;

            //foreach (var campoCustomizable in resultado)
            //{
            //    campoCustomizable.Rownum = rownum;
            //    rownum++;
            //}
            
            return resultado;
        }

        public Campo_Customizable ObtenCustomizableFiltro(int idCampo)
        {
            var resultado =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenCampoCustomizableFiltro(idCampo);

            return resultado;
        }

        public List<Valor_Campo_Catalogo> ObtenValoresCatalogo(int idCampo)
        {
            var resultado = 
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenValoresCatalogo(idCampo);

            return resultado;
        }

        public List<CampoCustomizableProducto> ObtenCustomizableProductos(int idPlantilla, int idProducto)
        {
            var resultado =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenCamposCustomProductos(idPlantilla, idProducto);

            return resultado;
        }

        public List<CampoCustomizableProducto> ObtenCustomizableProductosNuevo(int idPlantilla)
        {
            var resultado =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ObtenCamposCustomProductosNuevo(idPlantilla: idPlantilla);

            return resultado;
        }

        public bool ExistenCustomProducto(int idPlantilla, int idProducto)
        {
            return AccesoDatos.Catalogos.Plantilla.ClassInstance.ExisteCamposProducto(idPlantilla, idProducto);
        }

        #endregion

        #region Metodos de Insercion

        public static CAT_PLANTILLA InsertaPlantilla(Comp_Plantilla plantilla)
        {
            var p = new CAT_PLANTILLA
            {
                Dx_Descripcion = plantilla.DxDescripcion,
                Estatus = 1,
                Fecha_Adicion = DateTime.Now,
                Adicionado_por = plantilla.AdicionadoPor
            };

            var resultado = AccesoDatos.Catalogos.Plantilla.ClassInstance.InsertaPlantilla(p);

            return resultado;
        }

        public CAT_CAMPOS_CUSTOM InsertaCampoCustomizable(Campo_Customizable campo)
        { 
            var c = new CAT_CAMPOS_CUSTOM
            {
                Dx_Descripcion_Campo = campo.DxDescripcionCampo,
                Dx_Nombre_Campo = campo.DxNombreCampo,
                Dx_Tipo = campo.DxTipo,
                Dx_Tooltip = campo.ToolTip,
                Estatus = campo.Estatus,
                Fecha_Adicion = campo.FechaAdicion,
                Adicionado_por = campo.AdicionadoPor,
                CveObligatorio = campo.CveObligatorio
            };


            var resultado = AccesoDatos.Catalogos.Plantilla.ClassInstance.InsertaCampoCustomizable(c);

            return resultado;               
        }

        public bool InsertaValoresCatalogo(List<Valor_Campo_Catalogo> lstValores)
        {
            try
            {
                int[] consec = {0};

                foreach (var v in lstValores.Select(valor => new CAT_VALORES_CAMPOS_CAT
                {
                    Cve_Valor = consec[0],
                    Cve_Campo = valor.CveCampo,
                    Dx_Valor = valor.DxValor,
                    Estatus = valor.Estatus,
                    Fecha_adicion = valor.FechaAdicion,
                    Adicionado_por = valor.AdicionadoPor
                }))
                {
                    AccesoDatos.Catalogos.Plantilla.ClassInstance.InsertaValorCatalogo(v);
                    consec[0]++;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public CAT_CAMPOS_PLANTILLA InsertaCampoPlantilla(Campo_Customizable_Plantilla campoPlantilla)
        {
            var c = new CAT_CAMPOS_PLANTILLA
            {
                Cve_Plantilla = campoPlantilla.CvePlantilla,
                Cve_Campo = campoPlantilla.CveCampo,
                Estatus = campoPlantilla.Estatus,
                Fecha_Adicion = campoPlantilla.FechaAdicion
            };
            //c.Adicionado_por = campoPlantilla.AdicionadoPor;

            var resultado =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.InsertaCampoPlanilla(c);

            return resultado;
        }

        public List<CampoCustomizableProducto> InsertaCustomizableProductos(
            List<CampoCustomizableProducto> lstCustomizableProductos, int idProducto)
        {

            foreach (var campoCustomizableProducto in lstCustomizableProductos)
            {
                var campo = new CAT_CAMPOS_PLANTILLA_VALOR
                {
                    Cve_Campo = campoCustomizableProducto.CveCampo,
                    Cve_Plantilla = campoCustomizableProducto.CvePlantilla,
                    Cve_Producto = idProducto,
                    Cve_Valor_Cat =
                        campoCustomizableProducto.DxTipo == "Cat" ? int.Parse(campoCustomizableProducto.Valor) : 0,
                    Dx_Valor_Text = campoCustomizableProducto.DxTipo == "Text" ? campoCustomizableProducto.Valor : "",
                    Estatus = 1,
                    Fecha_Adicion = DateTime.Now
                };

                var resultado = AccesoDatos.Catalogos.Plantilla.ClassInstance.InsertaValorCampoPlantilla(campo);

                if (resultado != null)
                {
                    campoCustomizableProducto.Estatus = resultado.Estatus;
                    campoCustomizableProducto.FechaAdicion = resultado.Fecha_Adicion;
                }
            }

            return lstCustomizableProductos;
        }

        public static List<Campo_Base_Plantilla> InserCampoBasePlantillas(
            List<Campo_Base_Plantilla> lsCampoBasePlantillas, int idPlantilla)
        {
            foreach (var campoBasePlantilla in lsCampoBasePlantillas)
            {
                var campoBase = new CAT_CAMPOS_BASE_PLANTILLA
                {
                    Cve_Plantilla = idPlantilla,
                    Cve_Campo_Base = campoBasePlantilla.Cve_Campo_Base,
                    Dx_Campo_Base = campoBasePlantilla.Dx_Campo_Base,
                    Dx_Nombre_Control = campoBasePlantilla.Dx_Nombre_Control,
                    Cve_Agregar_Reporte = 0,
                    Estatus = campoBasePlantilla.Estatus,
                    Dx_Adicional1 = campoBasePlantilla.Dx_Adicional1,
                    Dx_Adicional2 = campoBasePlantilla.Dx_Adicional2
                };

                var resultado = AccesoDatos.Catalogos.Plantilla.InsertCamposBasePlantilla(campoBase);

                if (resultado != null)
                    campoBasePlantilla.Cve_Plantilla = resultado.Cve_Plantilla;
            }

            return lsCampoBasePlantillas;
        }

        #endregion

        #region Metodos de Actualizacion

        public static void ActualizaBasePlantilla(Campo_Base_Plantilla campoBase)
        {
            var c = new CAT_CAMPOS_BASE_PLANTILLA
            {
                Cve_Plantilla = campoBase.Cve_Plantilla,
                Cve_Campo_Base = campoBase.Cve_Campo_Base,
                Dx_Campo_Base = campoBase.Dx_Campo_Base,
                Dx_Nombre_Control = campoBase.Dx_Nombre_Control,
                Dx_Tabla_BD = campoBase.Dx_Tabla_BD,
                Dx_Campo_BD = campoBase.Dx_Campo_BD,
                Cve_Agregar_Reporte = campoBase.Cve_Agregar_Reporte,
                Estatus = campoBase.Estatus,
                Cve_Adicional1 = campoBase.Cve_Adicional1,
                Cve_Adicional2 = campoBase.Cve_Adicional2,
                Dx_Adicional1 = campoBase.Dx_Adicional1,
                Dx_Adicional2 = campoBase.Dx_Adicional2,
                Cve_Adicional3 = campoBase.Cve_Adicional3
            };

            var actualiza = 
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ActualizaBasePlantilla(c);

            //return actualiza;
        }

        public static bool ActualizaCampoCustomizable(Campo_Customizable campo)
        {
            if (campo.CveCampo != null)
            {
                var c = new CAT_CAMPOS_CUSTOM
                {
                    Cve_Campo = (int)campo.CveCampo,
                    Dx_Nombre_Campo = campo.DxNombreCampo,
                    Dx_Descripcion_Campo = campo.DxDescripcionCampo,
                    Dx_Tipo = campo.DxTipo,
                    Dx_Tooltip = campo.ToolTip,
                    Estatus = campo.Estatus,
                    Fecha_Adicion = campo.FechaAdicion,
                    Adicionado_por = campo.AdicionadoPor,
                    CveObligatorio = campo.CveObligatorio
                };

                var actualiza =
                    AccesoDatos.Catalogos.Plantilla.ClassInstance.ActualizaCampoCustomizable(c);

                return actualiza;
            }
            return false;
        }

        public bool ActualizaCampoPlantilla(Campo_Customizable_Plantilla campo)
        {
            var c = new CAT_CAMPOS_PLANTILLA
            {
                Cve_Plantilla = campo.CvePlantilla,
                Cve_Campo = campo.CveCampo,
                Cve_Agregar_Reporte = campo.CveAgregarReporte,
                Estatus = campo.Estatus,
                Fecha_Adicion = campo.FechaAdicion,
                Adicionado_por = campo.AdicionadoPor
            };

            var actualiza = 
                AccesoDatos.Catalogos.Plantilla.ClassInstance.ActaulizaCampoPlantilla(c);

            return actualiza;
        }

        public List<CampoCustomizableProducto> ActualizaCustomizableProductos(
            List<CampoCustomizableProducto> lstCustomizableProductos)
        {

            foreach (var campoCustomizableProducto in lstCustomizableProductos)
            {
                var campo = new CAT_CAMPOS_PLANTILLA_VALOR
                {
                    Cve_Campo = campoCustomizableProducto.CveCampo,
                    Cve_Plantilla = campoCustomizableProducto.CvePlantilla,
                    Cve_Producto = campoCustomizableProducto.CveProducto,
                    Cve_Valor_Cat =
                        campoCustomizableProducto.DxTipo == "Cat" ? int.Parse(campoCustomizableProducto.Valor) : 0,
                    Dx_Valor_Text = campoCustomizableProducto.DxTipo == "Text" ? campoCustomizableProducto.Valor : "",
                    Estatus = 1,
                    Fecha_Adicion = campoCustomizableProducto.FechaAdicion
                };

                AccesoDatos.Catalogos.Plantilla.ClassInstance.ActualizaCamposCustomProducto(campo);
            }

            return lstCustomizableProductos;
        }

        #endregion

        #region Metodos de Eliminacion

        public bool EliminaCampoPlantilla(Campo_Customizable_Plantilla campo)
        {
            var c = new CAT_CAMPOS_PLANTILLA
            {
                Cve_Plantilla = campo.CvePlantilla,
                Cve_Campo = campo.CveCampo,
                Estatus = campo.Estatus,
                Fecha_Adicion = campo.FechaAdicion
            };

            var camposValor = new CAT_CAMPOS_PLANTILLA_VALOR();
            camposValor.Cve_Plantilla = c.Cve_Plantilla;
            camposValor.Cve_Campo = c.Cve_Campo;

            var lstCamposPlantillaValor =
                AccesoDatos.Catalogos.Plantilla.ObtenListaCamposPlantillaValor(campo.CvePlantilla, campo.CveCampo);

            if (lstCamposPlantillaValor != null)
            {
                foreach (var catCamposPlantillaValor in lstCamposPlantillaValor)
                {
                    var eliminaValor =
                        AccesoDatos.Catalogos.Plantilla.EliminaValoresCamposPlantilla(catCamposPlantillaValor);
                }
            }
            
            var elimina =
                AccesoDatos.Catalogos.Plantilla.ClassInstance.EliminaCampoPlantilla(c);

            return elimina;
        }

        #endregion
    }
}
