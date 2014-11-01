/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Helpers
{
    public static class GlobalVar
    {
        //USER STATUS
        public const string STATUS_USER_PENDING = "P";
        public const string STATUS_USER_ACTIVE = "A";
        public const string STATUS_USER_INACTIVE = "I";
        public const string STATUS_USER_CANCEL = "C";
        //User types
        public const string CENTRAL_OFFICE = "C_O";
        public const string REGIONAL_OFFICE = "R_O";
        public const string SUPPLIER = "S";
        public const string SUPPLIER_BRANCH = "S_B";
        public const string DISPOSAL_CENTER = "D_C";
        public const string DISPOSAL_CENTER_BRANCH = "D_C_B";
        public const string MANUFACTURER = "M";
        public const string ZONE_OFFICE = "Z_O";
        //Job Status
        public const string WAITING_FOR_PROCESS = "WaitProcess";
        public const string PROCESSED = "Processed";
        public const string CANCELED = "Canceled";    
   
        //Provider or Branch
        public const string SUPPLIER_M = "Matriz";
        public const string SUPPLIER_B = "Sucursal";
        //Disposal Center or Branch
        public const string DisposalCenter = "Disposal Center";
        public const string DisposalBranch = "Branch";
        //supplier status
        public const string ACTIVE_SUPPLIER = "ACTIVO";
        public const string INACTIVE_SUPPLIER = "INACTIVO";
        public const string PENDING_SUPPLIER = "PENDIENTE";
        public const string CANCELED_SUPPLIER = "CANCELADO";
        //DISPOSAL CENTER STATUS
        public const string ACTIVE_DISPOSAL_CENTER = "ACTIVO";
        public const string INACTIVE_DISPOSAL_CENTER = "INACTIVO";
        public const string PENDING_DISPOSAL_CENTER = "PENDIENTE";
        public const string CANCELED_DISPOSAL_CENTER = "CANCELADO";
        //product status
        public const string ACTIVATE = "active";
        public const string DEACTIVATE = "inactive";
        public const string CANCEL = "cancel";
        //Added by Jerry 2012-04-12
        public const string REPLACED_DAY_OF_NUMBER = "#Number0fDay#";
        public const string REPLACED_OLD_EQUIPMENT_NUMBER = "#OldEquipmentNumber#";
        public const string REPLACED_CREDIT_NUMBER = "#CreditNumber#";
        public const string NOTIFICATION_EMAIL_SUBJECT = "AVISO EQUIPO BAJA EFICIENCIA PARA INHABILITACIóN";
        public const string NOTIFICTION_EMAIL_BODY = @"SE NOTIFICA QUE CUENTA CON #Number0fDay# DíAS NATURALES A PARTIR DE ESTE MOMENTO, PARA REALIZAR EL REGISTRO DE INHABILITACIóN DEL EQUIPO DE BAJA EFICIENCIA CON EL FOLIO NUM.  #OldEquipmentNumber# INCLUIDO EN EL CRéDITO NUM. #CreditNumber#.
                                                    SI NO REALIZA LA INHABILITACION DEL EQUIPO DE BAJA EFICIENCIA EN EL TIEMPO MáXIMO ESTABLECIDO, NO PODRá CONTINUAR CON LOS SIGUIENTES PROCESOS.";

    }
}
