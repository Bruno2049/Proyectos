using System.Collections.Generic;
using PruebaMVC5.Models;

namespace PruebaMVC5.Clases
{
    public class GestionDepartamentos
    {
        public List<Departamento> ListaDepartamentos
        {
            get
            {
                return _listaDepartamentos;
            }
        }

        private List<Departamento> _listaDepartamentos;

        public GestionDepartamentos()
        {
            LlenaLista();
        }

        private void LlenaLista()
        {
            _listaDepartamentos = new List<Departamento>
            {
                new Departamento {IdDepartamento = 1, NombreDepartamento = "Tecnologias de la informacion"},
                new Departamento {IdDepartamento = 2, NombreDepartamento = "Recursos Humanos"},
                new Departamento {IdDepartamento = 3, NombreDepartamento = "Contabilidad"}
            };
        }
    }
}