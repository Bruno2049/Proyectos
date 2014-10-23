using System.Collections.Generic;

namespace Universidad.Entidades.ControlUsuario
{
    public class MenuSistemaE
    {
        public int IdMenuPadre { get; set; }
        public int IdMenuHijo { get; set; }
        public string Modulo { get; set; }

        public List<MenuSistemaE> Hijos;
    }
}
