using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PruebaMVC5.Clases;

namespace PruebaMVC5.Controllers
{
    public class PrincipalController : Controller
    {

        public ActionResult ListaDepartamentos()
        {
            var gestionEmp = new GestionDepartamentos();
            var listaDepartamento = gestionEmp.ListaDepartamentos;
            return View(listaDepartamento);
        }

        public ActionResult ListaEmpleados(int idDepartamento)
        {
            var gestionEmp = new DatosEmpleados();
            var lista = gestionEmp.ListaEmpleados.Where(dep => dep.IdDepartamento == idDepartamento).ToList();
            return View(lista);
        }

        public ActionResult DetallesEmpleados(int idEmpleado)
        {
            var gestionEmp = new DatosEmpleados();
            var empleado =  gestionEmp.ListaEmpleados.FirstOrDefault(emp => emp.IdEmpleado == idEmpleado);
            return View(empleado);
        }

        public ActionResult ListaPaises()
        {
            var listaVb = new List<string>
            {
                "Mexico",
                "India",
                "Us",
                "Canada"
            };

            var listaVd = new List<string>
            {
                "Chana",
                "UK",
                "Colombia",
                "Japon"
            };

            ViewBag.Lista = listaVb;
            ViewData["ListaVD"] = listaVd;

            return View();
        }
	}
}