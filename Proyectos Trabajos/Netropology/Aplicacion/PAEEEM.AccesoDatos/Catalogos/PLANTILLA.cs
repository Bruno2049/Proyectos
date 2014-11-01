using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Plantilla
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
        private static readonly Plantilla _classInstance = new Plantilla();
        public static Plantilla ClassInstance { get { return _classInstance; } }

        #region Metodos de Consulta

        public Comp_Plantilla ObtenPlantillaFiltro(int? idPlantilla)
        {
            var resultado = (from plantillas in _contexto.CAT_PLANTILLA
                             select new Comp_Plantilla
                             {
                                 CvePlantilla = plantillas.Cve_Plantilla,
                                 DxDescripcion = plantillas.Dx_Descripcion,
                                 Estatus = plantillas.Estatus,
                                 FechaAdicion = plantillas.Fecha_Adicion,
                                 AdicionadoPor = plantillas.Adicionado_por,
                                 DxPanel = plantillas.Dx_Panel_Plantilla
                             }).ToList().FirstOrDefault(me => me.CvePlantilla == idPlantilla);

            return resultado;
        }

        public List<Campo_Base_Plantilla> ObtenCamposBasePlantilla(int? idPlantilla)
        {
            var resultado = (from camposBase in _contexto.CAT_CAMPOS_BASE_PLANTILLA
                             where camposBase.Cve_Plantilla == idPlantilla
                             select new Campo_Base_Plantilla
                             {
                                 Cve_Plantilla = camposBase.Cve_Plantilla,
                                 Cve_Campo_Base = camposBase.Cve_Campo_Base,
                                 Dx_Campo_Base = camposBase.Dx_Campo_Base,
                                 Dx_Nombre_Control = camposBase.Dx_Nombre_Control,
                                 Dx_Tabla_BD = camposBase.Dx_Tabla_BD,
                                 Dx_Campo_BD = camposBase.Dx_Campo_BD,
                                 Cve_Agregar_Reporte = camposBase.Cve_Agregar_Reporte,
                                 Estatus = camposBase.Estatus,
                                 Cve_Adicional1 = camposBase.Cve_Adicional1,
                                 Cve_Adicional2 = camposBase.Cve_Adicional2,
                                 Dx_Adicional1 = camposBase.Dx_Adicional1,
                                 Dx_Adicional2 = camposBase.Dx_Adicional2,
                                 Cve_Adicional3 = camposBase.Cve_Adicional3
                             }).ToList();

            return resultado;
        }

        public List<Campo_Customizable_Plantilla> ObtenCamposCustomPlantilla(int? idPlantilla)
        {
            var resultado = (from camposPlantilla in _contexto.CAT_CAMPOS_PLANTILLA
                             join customizables in _contexto.CAT_CAMPOS_CUSTOM
                                on camposPlantilla.Cve_Campo
                                equals customizables.Cve_Campo
                             where camposPlantilla.Cve_Plantilla == idPlantilla
                             select new Campo_Customizable_Plantilla
                             {
                                 CvePlantilla = camposPlantilla.Cve_Plantilla,
                                 CveCampo = camposPlantilla.Cve_Campo,
                                 DxNombreCampo = customizables.Dx_Nombre_Campo,
                                 DxDescripcionCampo = customizables.Dx_Descripcion_Campo,
                                 DxTipo = customizables.Dx_Tipo,
                                 Estatus = camposPlantilla.Estatus,
                                 FechaAdicion = camposPlantilla.Fecha_Adicion,
                                 AdicionadoPor = camposPlantilla.Adicionado_por,
                                 CveAgregarReporte = camposPlantilla.Cve_Agregar_Reporte ?? 0
                             }).ToList();

            return resultado;
 
        }

        public List<CampoCustomizableProducto> ObtenCamposCustomProductos(int idPlantilla, int idProducto)
        {
            var lstCustomizablesProducto = (from camposPlantilla in _contexto.CAT_CAMPOS_PLANTILLA
                             join customizables in _contexto.CAT_CAMPOS_CUSTOM
                                on camposPlantilla.Cve_Campo
                                equals customizables.Cve_Campo
                             where camposPlantilla.Cve_Plantilla == idPlantilla
                             select new CampoCustomizableProducto
                             {
                                 CvePlantilla = camposPlantilla.Cve_Plantilla,
                                 CveCampo = camposPlantilla.Cve_Campo,
                                 CveProducto = idProducto,
                                 DxNombreCampo = customizables.Dx_Nombre_Campo,
                                 DxDescripcionCampo = customizables.Dx_Descripcion_Campo,
                                 DxTipo = customizables.Dx_Tipo,
                                 Estatus = camposPlantilla.Estatus,
                                 FechaAdicion = camposPlantilla.Fecha_Adicion,
                                 AdicionadoPor = camposPlantilla.Adicionado_por,
                                 CveAgregarReporte = camposPlantilla.Cve_Agregar_Reporte ?? 0,
                                 CveObligatorio = customizables.CveObligatorio,
                                 ToolTip = customizables.Dx_Tooltip
                             }).ToList();

            foreach (var campoCustomizableProducto in lstCustomizablesProducto)
            {
                if (campoCustomizableProducto.DxTipo == "Cat")
                {
                    campoCustomizableProducto.LisValorCatalogos =
                       ObtenValoresCatalogo(campoCustomizableProducto.CveCampo);
                }

                var valorCampo = BuscaValorCamposPlantillaValor(
                    campoCustomizableProducto.CvePlantilla, campoCustomizableProducto.CveProducto,
                    campoCustomizableProducto.CveCampo);

                if (valorCampo != null)
                {
                    campoCustomizableProducto.Valor = campoCustomizableProducto.DxTipo == "Cat"
                        ? valorCampo.Cve_Valor_Cat.ToString()
                        : valorCampo.Dx_Valor_Text;
                }                   
            }
            
            return lstCustomizablesProducto;

        }

        public List<CampoCustomizableProducto> ObtenCamposCustomProductosNuevo(int idPlantilla)
        {
            var lstCustomizablesProducto = (from camposPlantilla in _contexto.CAT_CAMPOS_PLANTILLA
                                            join customizables in _contexto.CAT_CAMPOS_CUSTOM
                                               on camposPlantilla.Cve_Campo
                                               equals customizables.Cve_Campo
                                            where camposPlantilla.Cve_Plantilla == idPlantilla
                                            select new CampoCustomizableProducto
                                            {
                                                CvePlantilla = camposPlantilla.Cve_Plantilla,
                                                CveCampo = camposPlantilla.Cve_Campo,
                                                DxNombreCampo = customizables.Dx_Nombre_Campo,
                                                DxDescripcionCampo = customizables.Dx_Descripcion_Campo,
                                                DxTipo = customizables.Dx_Tipo,
                                                Estatus = camposPlantilla.Estatus,
                                                FechaAdicion = camposPlantilla.Fecha_Adicion,
                                                AdicionadoPor = camposPlantilla.Adicionado_por,
                                                CveAgregarReporte = camposPlantilla.Cve_Agregar_Reporte ?? 0,
                                                CveObligatorio = customizables.CveObligatorio,
                                                ToolTip = customizables.Dx_Tooltip
                                            }).ToList();

            foreach (var campoCustomizableProducto in lstCustomizablesProducto)
            {
                if (campoCustomizableProducto.DxTipo == "Cat")
                {
                    campoCustomizableProducto.LisValorCatalogos =
                       ObtenValoresCatalogo(campoCustomizableProducto.CveCampo);
                }
            }

            return lstCustomizablesProducto;

        }

        public List<Campo_Customizable> ObtenCamposCustomizables()
        {
            var resultado = (from customizables in _contexto.CAT_CAMPOS_CUSTOM
                             select new Campo_Customizable
                             {
                                 CveCampo = customizables.Cve_Campo,
                                 DxNombreCampo = customizables.Dx_Nombre_Campo,
                                 DxDescripcionCampo = customizables.Dx_Descripcion_Campo,
                                 DxTipo = customizables.Dx_Tipo,
                                 ToolTip = customizables.Dx_Tooltip,
                                 Estatus = customizables.Estatus,
                                 FechaAdicion = customizables.Fecha_Adicion,
                                 AdicionadoPor = customizables.Adicionado_por,
                                 CveObligatorio = 0
                             }).ToList();

            return resultado;
        }

        public Campo_Customizable ObtenCampoCustomizableFiltro(int idCampo)
        {
            var resultado = (from customizables in _contexto.CAT_CAMPOS_CUSTOM
                             select new Campo_Customizable
                             {
                                 CveCampo = customizables.Cve_Campo,
                                 DxNombreCampo = customizables.Dx_Nombre_Campo,
                                 DxDescripcionCampo = customizables.Dx_Descripcion_Campo,
                                 DxTipo = customizables.Dx_Tipo,
                                 ToolTip = customizables.Dx_Tooltip,
                                 Estatus = customizables.Estatus,
                                 FechaAdicion = customizables.Fecha_Adicion,
                                 AdicionadoPor = customizables.Adicionado_por,
                                 CveObligatorio = 0
                             }).ToList().FirstOrDefault(me => me.CveCampo == idCampo);

            return resultado;
        }

        public List<Valor_Campo_Catalogo> ObtenValoresCatalogo(int idCampo)
        {
            var resultado = (from valores in _contexto.CAT_VALORES_CAMPOS_CAT
                             where valores.Cve_Campo == idCampo
                             select new Valor_Campo_Catalogo
                             {
                                 CveCampo = valores.Cve_Campo,
                                 CveValor = valores.Cve_Valor,
                                 DxValor = valores.Dx_Valor,
                                 Estatus = valores.Estatus,
                                 FechaAdicion = valores.Fecha_adicion,
                                 AdicionadoPor = valores.Adicionado_por
                             }).ToList();

            return resultado;
        }

        public CAT_CAMPOS_PLANTILLA_VALOR BuscaValorCamposPlantillaValor(int idPlantilla, int idProducto, int idCampo)
        {
            CAT_CAMPOS_PLANTILLA_VALOR resultado;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA_VALOR>())
            {
                resultado =
                    r.Extraer(
                        me =>
                            me.Cve_Plantilla == idPlantilla && me.Cve_Producto == idProducto && me.Cve_Campo == idCampo);
            }

            return resultado;
        }

        public static List<CAT_CAMPOS_PLANTILLA_VALOR> ObtenListaCamposPlantillaValor(int idPlantilla, int idCampo)
        {
            List<CAT_CAMPOS_PLANTILLA_VALOR> lstCamposPlantillaValor = null;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA_VALOR>())
            {
                lstCamposPlantillaValor = r.Filtro(me => me.Cve_Plantilla == idPlantilla && me.Cve_Campo == idCampo);
            }

            return lstCamposPlantillaValor;
        }

        public bool ExisteCamposProducto(int idPlantilla, int idProducto)
        {
            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA_VALOR>())
            {
                var resultado = r.Filtro(me => me.Cve_Plantilla == idPlantilla && me.Cve_Producto == idProducto);

                if (resultado.Count > 0)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Metodos de Insercion

        public CAT_PLANTILLA InsertaPlantilla(CAT_PLANTILLA plantilla)
        {
            CAT_PLANTILLA resultado;

            using (var r = new Repositorio<CAT_PLANTILLA>())
            {
                int idPlantilla = r.Filtro(me => me.Estatus == 1).Max(me => me.Cve_Plantilla);
                plantilla.Cve_Plantilla = idPlantilla + 1;

                resultado = r.Agregar(plantilla);
            }

            return resultado;
        }

        public static CAT_CAMPOS_BASE_PLANTILLA InsertCamposBasePlantilla(CAT_CAMPOS_BASE_PLANTILLA campo)
        {
            using (var r = new Repositorio<CAT_CAMPOS_BASE_PLANTILLA>())
            {
                return r.Agregar(campo);
            }
        }

        public CAT_CAMPOS_CUSTOM InsertaCampoCustomizable(CAT_CAMPOS_CUSTOM customizable)
        {
            CAT_CAMPOS_CUSTOM resultado = null;

            using (var r = new Repositorio<CAT_CAMPOS_CUSTOM>())
            {
                resultado = r.Agregar(customizable);
            }

            return resultado;
        }

        public CAT_VALORES_CAMPOS_CAT InsertaValorCatalogo(CAT_VALORES_CAMPOS_CAT valor)
        {
            using (var r = new Repositorio<CAT_VALORES_CAMPOS_CAT>())
            {
               return r.Agregar(valor);
            }
        }

        public CAT_CAMPOS_PLANTILLA InsertaCampoPlanilla(CAT_CAMPOS_PLANTILLA campo)
        {
            CAT_CAMPOS_PLANTILLA resultado;
            CAT_CAMPOS_PLANTILLA existe;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA>())
            {
                existe = r.Extraer(me => me.Cve_Plantilla == campo.Cve_Plantilla && me.Cve_Campo == campo.Cve_Campo); 

                if(existe == null)
                    resultado = r.Agregar(campo);
                else
                    throw new Exception("El Campo Seleccionado ya esta asignado a la plantilla");
            }

            return resultado;
        }

        public CAT_CAMPOS_PLANTILLA_VALOR InsertaValorCampoPlantilla(CAT_CAMPOS_PLANTILLA_VALOR valorCampo)
        {
            CAT_CAMPOS_PLANTILLA_VALOR resultado;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA_VALOR>())
            {
                resultado = r.Agregar(valorCampo);
            }

            return resultado;
        }

        #endregion

        #region Metodos de Actualizacion

        public bool ActualizaBasePlantilla(CAT_CAMPOS_BASE_PLANTILLA camposBase)
        {
            bool actualiza;

            using(var r = new Repositorio<CAT_CAMPOS_BASE_PLANTILLA>())
            {
                actualiza = r.Actualizar(camposBase);
            }

            return actualiza;
        }

        public bool ActualizaCampoCustomizable(CAT_CAMPOS_CUSTOM campo)
        {
            bool actualiza;

            using (var r = new Repositorio<CAT_CAMPOS_CUSTOM>())
            {
                actualiza = r.Actualizar(campo);
            }

            return actualiza;
        }

        public bool ActaulizaCampoPlantilla(CAT_CAMPOS_PLANTILLA campo)
        {
            bool actualiza;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA>())
            {
                actualiza = r.Actualizar(campo);
            }

            return actualiza;
        }

        public bool ActualizaCamposCustomProducto(CAT_CAMPOS_PLANTILLA_VALOR campo)
        {
            bool actualiza;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA_VALOR>())
            {
                var existe =
                    r.Extraer(
                        me =>
                            me.Cve_Plantilla == campo.Cve_Plantilla && me.Cve_Producto == campo.Cve_Producto &&
                            me.Cve_Campo == campo.Cve_Campo);

                if(existe != null)
                    actualiza = r.Actualizar(campo);
                else
                {
                    var inserta = r.Agregar(campo);

                    actualiza = inserta != null;
                }
            }

            return actualiza;
        }

        #endregion

        #region Metodos de Eliminación

        public bool EliminaCampoPlantilla(CAT_CAMPOS_PLANTILLA campoPlantilla)
        {
            bool elimina;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA>())
            {
                elimina = r.Eliminar(campoPlantilla);
            }

            return elimina;
        }

        public static bool EliminaValoresCamposPlantilla(CAT_CAMPOS_PLANTILLA_VALOR campoValor)
        {
            bool elimina;

            using (var r = new Repositorio<CAT_CAMPOS_PLANTILLA_VALOR>())
            {
                elimina = r.Eliminar(campoValor);
            }

            return elimina;
        }

        #endregion
    }
}
