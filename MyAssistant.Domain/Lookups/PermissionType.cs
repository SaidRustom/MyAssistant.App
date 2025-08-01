﻿using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    [Table("PermissionType")]
    public class PermissionType : LookupBase<PermissionType>
    {
        public static readonly int Read = 1;
        public static readonly int ReadWrite = 2;
        public static readonly int ReadWriteDelete = 3;

        public PermissionType() { }
    }

    public class PermissionTypeList : LookupBaseList<PermissionType>
    {
        public PermissionTypeList CachedList
        {
            get { return _permissionTypeList; }
            set { _permissionTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _permissionTypeList.Clear();
        }

        private static PermissionTypeList _permissionTypeList { get; set; } = new();
    }
}
