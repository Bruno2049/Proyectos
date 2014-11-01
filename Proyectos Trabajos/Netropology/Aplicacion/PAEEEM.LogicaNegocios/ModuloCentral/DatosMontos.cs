using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Utilizables;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class DatosMontos
    {
        public decimal? montoTotalPrograma()
        {
            var monto = new Metas().montoTotalPrograma();
            return monto;
        }

        public decimal montoMinimoPrograma()
        {
            var monto = new Metas().montoMinimoPrograma();
            return monto;
        }

        public decimal? montoTotalIncentivo()
        {
            var monto = new Metas().montoTotalIncentivo();
            return monto;
        }

        public decimal montoMinimoIncentivo()
        {
            var monto = new Metas().montoMinimoIncentivo();
            return monto;
        }

        public decimal? montoDisponibleIncentivo()
        {
            var monto = new Metas().montoDisponibleIncentivo();
            return monto;
        }

        public decimal? montoDisponiblePrograma()
        {
            var monto = new Metas().montoDisponiblePrograma();
            return monto;
        }

        public bool ProveedorTecAdquisicion(int ID)
        {
            var p = new Metas().ProveedorTecAdquisicion(ID);
            return p;
        }

        public string TipoMovimiento(int ID)
        {
            var m = new Metas().TipoMovimiento(ID);
            return m;
        }

        public List<CComboBox> ObtieneTecnologiasAquisicion(int CveTarifa, int idProveedor)
        {
            var cboTecnologias = new List<CComboBox>();
            var tecnologias = new List<CAT_TECNOLOGIA>();

            tecnologias = new Metas().ObtieneTecnologiasAquisicion(CveTarifa, idProveedor);

            if (tecnologias.Count > 0)
            {
                cboTecnologias.Add(new CComboBox(-1, "Seleccione"));

                foreach (var tecnologia in tecnologias)
                {
                    cboTecnologias.Add(new CComboBox(tecnologia.Cve_Tecnologia, tecnologia.Dx_Nombre_General));
                }

            }

            return cboTecnologias;
        }

        public List<TR_PARAMETROS_GLOBALES> CorreoUsuario()
        {
            var us = new Metas().CorreoUsuario();
            return us;
        }
    }
}
