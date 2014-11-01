/* ----------------------------------------------------------------------
 * File Name: K_CREDITO_SUSTITUCIONBLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   K_CREDITO_SUSTITUCION business logic lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
   public class K_CREDITO_SUSTITUCIONBLL
    {
       private static readonly K_CREDITO_SUSTITUCIONBLL _classinstance = new K_CREDITO_SUSTITUCIONBLL();

       public static K_CREDITO_SUSTITUCIONBLL ClassInstance { get { return _classinstance; } }

       /// <summary>
       /// Add K_CREDITO_SUSTITUCION
       /// </summary>
       /// <param name="instance"></param>
       /// <returns></returns>
       public int Insert_K_CREDITO_SUSTITUCION(List<K_CREDITO_SUSTITUCIONModel> listAdd)
       {
           int iResult = 0;
           try
           {
               using (TransactionScope scope = new TransactionScope())
               {
                   for (int i = 0; i < listAdd.Count; i++)
                   {
                       K_CREDITO_SUSTITUCIONModel instance = listAdd[i];
                       iResult += K_CREDITO_SUSTITUCIONDAL.ClassInstance.Insert_K_CREDITO_SUSTITUCION(instance);
                   }
                   scope.Complete();
               }
           }
           catch (LsDAException ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
           return iResult;
       }
       /// <summary>
       /// Update K_CREDITO_SUSTITUCION
       /// </summary>
       /// <param name="instance"></param>
       /// <returns></returns>
       public int Update_K_CREDITO_SUSTITUCION(List<K_CREDITO_SUSTITUCIONModel> listUpdate)
       {
           int iResult = 0;
           try
           {
               using (TransactionScope scope = new TransactionScope())
               {
                   for (int i = 0; i < listUpdate.Count; i++)
                   {
                       K_CREDITO_SUSTITUCIONModel instance = listUpdate[i];
                       iResult += K_CREDITO_SUSTITUCIONDAL.ClassInstance.Update_K_CREDITO_SUSTITUCION(instance);
                   }
                   scope.Complete();
               }
           }
           catch (LsDAException ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
           return iResult;
       }
       /// <summary>
       /// update status to INHABILITADO
       /// </summary>
       /// <param name="listUpdate"></param>
       /// <returns></returns>
       public int UpdateK_CREDITO_SUSTITUCIONStatus(List<string[]> listUpdate)
       {
           int iResult = 0;
           try
           {
               using (TransactionScope scope = new TransactionScope())
               {
                   for (int i = 0; i < listUpdate.Count; i++)
                   {
                       string[] update = listUpdate[i];
                       iResult += K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpdateK_CREDITO_SUSTITUCIONEstatusToInhabilitado(update[0], Convert.ToInt32(update[1]), DateTime.Now);
                   }
                   scope.Complete();
               }
           }
           catch (LsDAException ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
           return iResult;
       }
       //edit by coco 2012-01-06
       /// <summary>
       /// Update old product property information
       /// </summary>
       /// <param name="instance"></param>
       /// <param name="DisposalID"></param>
       /// <param name="UserType"></param>
       /// <returns></returns>
       public int UpdateCreditSustutionByModel(K_CREDITO_SUSTITUCIONModel instance,K_PRODUCTO_CHARACTERSEntity instanceProduct, ScheduledNotificationEntity scheduledNotificationEntity)
       {
           try
           {
               using (TransactionScope scope = new TransactionScope())
               {
                   int result = 0;

                   result += K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpdateCreditSustutionByModel(instance);
                   result += K_PRODUCTO_CHARACTERSDal.ClassInstance.Insert_K_PRODUCTO_CHARACTERS(instanceProduct); 
                 
                   //Added by Jerry 2012/04/12
                   result += ScheduledNotificationDal.ClassInstance.InsertScheduledNotification(scheduledNotificationEntity);

                   if (result > 2)
                   {
                       scope.Complete();
                   }
                   return result;
               }
           }
           catch (Exception ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
       }
       //edit coco
       //add by coco 2012-04-12
       public Boolean IsAllowUploadReceiptionOldEquipment(string disposalID, string UserType, string SustitucionID)
       {
           bool Reslut = true;
           try
           {
               DataTable dtTemp = K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsAllowUploadReceiptionOldEquipment(disposalID, UserType);
               if (dtTemp != null && dtTemp.Rows.Count >0)
               {
                 dtTemp.DefaultView.RowFilter ="Id_Credito_Sustitucion=" +SustitucionID;
                 if (dtTemp.DefaultView.ToTable().Rows.Count  == 0)
                   {
                       Reslut = false;
                   }
               }
           }
           catch (Exception ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
           return Reslut;
       }
       public Boolean IsAllowUploadUnableOldEquipment(string disposalID, string UserType, string SustitucionID)
       {
           bool Reslut = true;
           try
           {
               DataTable dtTemp = K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsAllowUploadUnableOldEquipment(disposalID, UserType);
               if (dtTemp != null && dtTemp.Rows.Count>0)
               {
                   dtTemp.DefaultView.RowFilter = "Id_Credito_Sustitucion=" + SustitucionID;
                   if (dtTemp.DefaultView.ToTable().Rows.Count == 0)
                   {
                       Reslut = false;
                   }
               }
           }
           catch (Exception ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
           return Reslut;
       }
       //end add
       //added by tina 2012-07-12
       public Boolean IsAllowReceiptOldEquipment(int disposalID, string userType, string sustitucionID)
       {
           bool Reslut = true;
           try
           {
               DataTable dtTemp = K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsAllowReceiptOldEquipment(disposalID, userType);
               if (dtTemp != null && dtTemp.Rows.Count > 0)
               {
                   dtTemp.DefaultView.RowFilter = "Id_Credito_Sustitucion=" + sustitucionID;
                   if (dtTemp.DefaultView.ToTable().Rows.Count == 0)
                   {
                       Reslut = false;
                   }
               }
           }
           catch (Exception ex)
           {
               throw new LsBLException(this, ex.Message, ex, false);
           }
           return Reslut;
       }
       //end
    }
}
