
using System;
namespace ViewModel.InterfaceBase
{
    public enum TipoShowMessage
    {
        Normal,
        Eliminar,
        Guardar,
        Cancelar,
        Preguntar,
        Guardado,
        Confirmar
    }

    public interface IDialogService
    {
        void ShowMessage(string message, string titulo, TipoShowMessage tipoShowMessage=TipoShowMessage.Normal);
        void ShowMessage(string message, Exception error);
        
        bool AskConfirmation(string message, string titulo);
        bool CerrarChild(string message, string titulo);
    }
}
