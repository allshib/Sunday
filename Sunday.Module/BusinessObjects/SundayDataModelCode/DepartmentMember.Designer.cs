﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Sunday.Module.BusinessObjects.SundayDataModel
{

    public partial class DepartmentMember : DevExpress.Persistent.BaseImpl.BaseObject
    {
        Department fDepartment;
        [Association(@"DepartmentMemberReferencesDepartment")]
        public Department Department
        {
            get { return fDepartment; }
            set { SetPropertyValue<Department>(nameof(Department), ref fDepartment, value); }
        }
        Sunday.Module.BusinessObjects.Security.Employee fEmployee;
        [DevExpress.Xpo.Association(@"DepartmentMemberReferencesEmployee")]
        public Sunday.Module.BusinessObjects.Security.Employee Employee
        {
            get { return fEmployee; }
            set { SetPropertyValue<Sunday.Module.BusinessObjects.Security.Employee>(nameof(Employee), ref fEmployee, value); }
        }
        Sunday.Module.BusinessObjects.Security.CustomPermissionPolicyRole fDepartmentRole;
        public Sunday.Module.BusinessObjects.Security.CustomPermissionPolicyRole DepartmentRole
        {
            get { return fDepartmentRole; }
            set { SetPropertyValue<Sunday.Module.BusinessObjects.Security.CustomPermissionPolicyRole>(nameof(DepartmentRole), ref fDepartmentRole, value); }
        }
    }

}