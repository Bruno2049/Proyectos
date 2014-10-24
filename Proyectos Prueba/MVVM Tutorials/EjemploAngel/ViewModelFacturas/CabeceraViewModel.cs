using System;
using System.Collections.Generic;
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
using ViewModel.InterfaceBase;
using ViewModelFacturas.ServicioFacturas;

namespace ViewModelFacturas
{
    public class CabeceraViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        ViewModelFacturas.ServicioFacturas.CabeceraServiceClient serviceClient;

      
        // PROPIEDADES

        private ServicioFacturas.CabeceraFacturaDTO _cabeceraFactura;
        public ServicioFacturas.CabeceraFacturaDTO CabeceraFacturaDto
        {
            get { return _cabeceraFactura; }
            set
            {   
                _cabeceraFactura = value;

            RaisePropertyChanged("CabeceraFacturaDto");
            
            }
        }

        private ServicioFacturas.DetalleFacturaDTO _itemDetalles;
        public ServicioFacturas.DetalleFacturaDTO ItemDetalles
        {
            get { return _itemDetalles; }
            set
            {
               
                _itemDetalles = value;

                RaisePropertyChanged("ItemDetalles");

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


        public CabeceraViewModel()
        {
            serviceClient = new CabeceraServiceClient();
            ListaDetalles = new ObservableCollection<DetalleFacturaDTO>();
            CabeceraFacturaDto = new CabeceraFacturaDTO();


            Insertar = new CommandBase(P => true, P => InsertarCabeceraConDetalles()) { IsEnabled = true };
            NuevoDetalle = new CommandBase(P => true, P => NuevoDetalleMetodo()) { IsEnabled = true };
            InsertarDetalle = new CommandBase(P => true, P => InsertarDetalleMetodo()) { IsEnabled = true };

            serviceClient.InsertarCabeceraServiceCompleted += serviceClient_InsertarCabeceraServiceCompleted;
        }

        private void InsertarCabeceraConDetalles()
        {
            CabeceraFacturaDto.DetalleFactura = ListaDetalles;
            serviceClient.InsertarCabeceraServiceAsync(CabeceraFacturaDto);
        }

        private void InsertarDetalleMetodo()
        {
            ListaDetalles.Add(ItemDetalles);
        }

        private void NuevoDetalleMetodo()
        {
            ItemDetalles = new DetalleFacturaDTO();
        }

        void serviceClient_InsertarCabeceraServiceCompleted(object sender, InsertarCabeceraServiceCompletedEventArgs e)
        {
            if (e.Result==true)
            {
                MessageBox.Show("Insertado Correctamente ");
            }
        }




        public ICommand Insertar { get; set; }
        public ICommand NuevoDetalle { get; set; }
        public ICommand InsertarDetalle { get; set; }



    }
}
