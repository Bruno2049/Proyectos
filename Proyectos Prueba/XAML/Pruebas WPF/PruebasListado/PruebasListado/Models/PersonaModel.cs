using System.Windows.Navigation;
using PruebasListado.Clases;
using PruebasListado.Modelos;

namespace PruebasListado.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;

    public class PersonaModel : INotifyPropertyChanged
    {
        private List<Personas> _ListaPersonas = new ListarPersonas().Personas;

        public List<Personas> ListaPersonas
        {
            get { return _ListaPersonas; }
        }
        
        public PersonaModel()
        {
           
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
