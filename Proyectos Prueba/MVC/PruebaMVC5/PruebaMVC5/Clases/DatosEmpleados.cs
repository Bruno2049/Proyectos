using System;
using System.Collections.Generic;
using PruebaMVC5.Models;

namespace PruebaMVC5.Clases
{
    public class DatosEmpleados
    {
        public List<Empleados> ListaEmpleados
        {
            get
            {
                return _listaEmpleados;
            }

            set
            {
                _listaEmpleados = value;
            }
        }

        private List<Empleados> _listaEmpleados;

        public DatosEmpleados()
        {
            LlenaListaEmpleados();
        }

        void LlenaListaEmpleados()
        {
            _listaEmpleados = new List<Empleados>
            {
                new Empleados
                {
                    IdEmpleado = 1,
                    Nombre = "Javier Lopes",
                    Ciudad = "Monterrey",
                    FechaNacimiento = Convert.ToDateTime("1988/05/03"),
                    Genero = "H",
                    IdDepartamento = 1
                },

                new Empleados
                {
                    IdEmpleado = 2,
                    Nombre = "Maira Morales",
                    Ciudad = "Chilpancingo",
                    FechaNacimiento = Convert.ToDateTime("1978/05/03"),
                    Genero = "M",
                    IdDepartamento = 2
                },

                new Empleados
                {
                    IdEmpleado = 3,
                    Nombre = "Perla Juares",
                    Ciudad = "Morelia",
                    FechaNacimiento = Convert.ToDateTime("1979/07/10"),
                    Genero = "M",
                    IdDepartamento = 3
                },

                new Empleados
                {
                    IdEmpleado = 4,
                    Nombre = "Juan Diaz",
                    Ciudad = "Sinaloa",
                    FechaNacimiento = Convert.ToDateTime("1967/12/03"),
                    Genero = "M",
                    IdDepartamento = 1
                },

                new Empleados
                {
                    IdEmpleado = 5,
                    Nombre = "Pedro Ortiz",
                    Ciudad = "Ecatepec",
                    FechaNacimiento = Convert.ToDateTime("1992/12/03"),
                    Genero = "M",
                    IdDepartamento = 2
                }
            };
        }
    }
}