//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class SIS_AADM_MENUS
    {
        public SIS_AADM_MENUS()
        {
            this.SIS_AADM_MENUS1 = new HashSet<SIS_AADM_MENUS>();
        }
    
        public int ID_MENU_PADRE { get; set; }
        public Nullable<int> ID_MENU_HIJO { get; set; }
        public string MENU { get; set; }
    
        public virtual ICollection<SIS_AADM_MENUS> SIS_AADM_MENUS1 { get; set; }
        public virtual SIS_AADM_MENUS SIS_AADM_MENUS2 { get; set; }
    }
}
