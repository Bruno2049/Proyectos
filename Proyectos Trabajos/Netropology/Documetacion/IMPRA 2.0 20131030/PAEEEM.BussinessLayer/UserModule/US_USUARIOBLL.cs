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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Web;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class US_USUARIOBll
    {
        private static readonly US_USUARIOBll _classinstance = new US_USUARIOBll();

        public static US_USUARIOBll ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_USUARIO(US_USUARIOModel instance)
        {
            return US_USUARIODal.ClassInstance.Insert_US_USUARIO(instance);
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public int Delete_US_USUARIO(String pkid)
        {
            return US_USUARIODal.ClassInstance.Delete_US_USUARIO(pkid);
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_US_USUARIO(US_USUARIOModel instance)
        {
            return US_USUARIODal.ClassInstance.Update_US_USUARIO(instance);
        }
        /// <summary>
        /// Get all Record
        /// </summary>
        public List<US_USUARIOModel> Get_AllUS_USUARIO()
        {
            return US_USUARIODal.ClassInstance.Get_AllUS_USUARIO();
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public US_USUARIOModel Get_US_USUARIOByPKID(String pkid)
        {
            return US_USUARIODal.ClassInstance.Get_US_USUARIOByPKID(pkid);
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public int AuthenticationUser(US_USUARIOModel userEntity)
        {
            return US_USUARIODal.ClassInstance.AuthenticationUser(userEntity);
        }
        /// <summary>
        /// Validate user name
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ValidationUserName(string username, out string password, out string address)
        {
            return US_USUARIODal.ClassInstance.ValidationUserName(username, out password, out address);
        }
        /// <summary>
        /// Validate user email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsUserNameExist(string user)
        {
            return US_USUARIODal.ClassInstance.IsUserNameExist(user);
        }
        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="uS_USUARIOModel"></param>
        /// <returns></returns>
        public int UpdatePassword(US_USUARIOModel uS_USUARIOModel)
        {
            return US_USUARIODal.ClassInstance.UpdatePassword(uS_USUARIOModel);
        }
        /// <summary>
        /// get user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public US_USUARIOModel Get_UserByUserName(string userName)
        {
            return US_USUARIODal.ClassInstance.Get_UserByUserName(userName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public List<US_USUARIOModel> Validation_User(string strUserName)
        {
            throw new NotImplementedException();
        }
    }
}

