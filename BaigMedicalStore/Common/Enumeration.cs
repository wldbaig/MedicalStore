using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.Common
{
    public class Enumeration
    {

        #region Enums
          
        public enum EntityStatus
        {
            InActive = 0,
            Active,
            Delete
        }

        public enum ItemStatusInOrder
        {
            New = 1,
            Repeat 
        } 
        public enum ItemOrderStatus
        {
            New = 1,
            Repeat,
            MoveFromOtherOrder
        }

  
        #region APPLICATION MESSAGES


        #endregion

        #endregion

        #region Struct
         
        public struct UserRole
        {
            public const string SuperAdmin = "superadmin";
            public const string Admin = "admin"; 
        }
          
         
        public struct ImagePath
        {
            public const string NoImage = "/Content/images/no-picture.png";
        }
          

        public struct MessageType
        {
            public const string Error = "Error";
            public const string Info = "Info";
            public const string Warning = "Warning";
            public const string Success = "Success";
        }
          
        #endregion

    }
}