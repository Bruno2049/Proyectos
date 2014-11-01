using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace eClockMobile.Models
{
    public class RegistroEmpleadoModel
    {
        [Required]
        [Display(Name = "No. de Suscripción")]
        public string Suscripcion { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "No. de Empleado")]
        public int PersonaLinkID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Dirección de correo electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirmación de correo electrónico")]
        [Compare("Email", ErrorMessage = "El correo electrónico y su confirmación no coinciden.")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}