using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using EjemploTreeView.Modelos;

namespace EjemploTreeView.Clases
{
    public class GestionEmpleados
    {
        public List<Empleados> ListaEmpleados {
            get { return _listaEmpleados;  }
        }
        private List<Empleados> _listaEmpleados;
        
        public GestionEmpleados()
        {
            CreaLista();
        }

        private void CreaLista()
        {
            _listaEmpleados = new List<Empleados>
            {
                new Empleados {IdEmpleados = 1, NombreEmpleado = "Javier Lopes", IdJeje = 0},
                new Empleados {IdEmpleados = 2, NombreEmpleado = "Marco Perez", IdJeje = 0},
                new Empleados {IdEmpleados = 3, NombreEmpleado = "Sara Gutierrez", IdJeje = 0},
                new Empleados {IdEmpleados = 4, NombreEmpleado = "Angelica Suarez", IdJeje = 2},
                new Empleados {IdEmpleados = 5, NombreEmpleado = "Maria Juarez", IdJeje = 2},
                new Empleados {IdEmpleados = 6, NombreEmpleado = "Martha Rosas", IdJeje = 5},
                new Empleados {IdEmpleados = 7, NombreEmpleado = "Antonio Dias", IdJeje = 6},
                new Empleados {IdEmpleados = 8, NombreEmpleado = "Martin Lopes", IdJeje = 1},
                new Empleados {IdEmpleados = 9, NombreEmpleado = "Perla Zarate", IdJeje = 3},
                new Empleados {IdEmpleados = 10, NombreEmpleado = "Sarai Gonzales", IdJeje = 9}
            };
        }

        public DataTable Nodo()
        {
            var lista = ListToDataTable(_listaEmpleados);
            return lista;
        }

        public List<Empleados> NodoHijos(int idJefe)
        {
            var listaJefes = _listaEmpleados.Where(r => r.IdJeje == idJefe).ToList();
            return listaJefes;
        }

        private static DataTable ListToDataTable<T>(IEnumerable<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
