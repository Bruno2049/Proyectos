using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_Vacaciones
    {
        public decimal SaldoVacaciones { get; set; }
        public DateTime SiguienteCorte { get; set; }
        public decimal SiguienteIncremento { get; set; }
        public decimal Credito { get; set; }
        /// <summary>
        /// Indica que se perderán las vacaciones al realizarse el corte y solo se tendrá el nuevo saldo
        /// </summary>
        public bool PierdeVacaciones { get; set; }
    }
}
