using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ViewModelFacturas.ServicioFacturas;

namespace ViewModelFacturas
{
    public class ListarViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private ServicioFacturas.CabeceraServiceClient serviceClient;

        private ServicioFacturas.CabeceraFacturaDTO _cabeceraFactura;
        public ServicioFacturas.CabeceraFacturaDTO CabeceraFacturaDto
        {
            get { return _cabeceraFactura; }
            set
            {
                _cabeceraFactura = value;
                if (CabeceraFacturaDto!=null)
                {
                    ListaDetalles = CabeceraFacturaDto.DetalleFactura;
                }
                RaisePropertyChanged("CabeceraFacturaDto");

            }
        }

        private ObservableCollection<ServicioFacturas.CabeceraFacturaDTO> _listacabeceraFactura;
        public  ObservableCollection<ServicioFacturas.CabeceraFacturaDTO> ListaCabeceraFacturaDto
        {
            get { return _listacabeceraFactura; }
            set
            {
                _listacabeceraFactura = value;

                RaisePropertyChanged("ListaCabeceraFacturaDto");

            }
        }

        private ObservableCollection<DetalleFacturaDTO> _listaDetalles;
        public ObservableCollection<DetalleFacturaDTO> ListaDetalles
        {
            get { return _listaDetalles; }
            set
            {
                _listaDetalles = value;

                RaisePropertyChanged("ListaDetalles");

            }
        }

        public ListarViewModel()
        {
            serviceClient = new CabeceraServiceClient();
            serviceClient.ListaCabeceraFacturasServiceCompleted += serviceClient_ListaCabeceraFacturasServiceCompleted;

            ListaCabeceraFacturaDto = new ObservableCollection<CabeceraFacturaDTO>();
            CabeceraFacturaDto = new CabeceraFacturaDTO();


            serviceClient.ListaCabeceraFacturasServiceAsync();
        }

        void serviceClient_ListaCabeceraFacturasServiceCompleted(object sender, ListaCabeceraFacturasServiceCompletedEventArgs e)
        {
            if (e.Error!=null)
            {
                //hay error
            }
            else
            {
                foreach (var itemCabece in e.Result)
                {
                    ListaCabeceraFacturaDto.Add(itemCabece);
                }
            }
          
        }

    }
}
