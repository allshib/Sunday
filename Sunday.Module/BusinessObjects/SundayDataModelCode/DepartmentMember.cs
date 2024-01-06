using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraRichEdit.API.Native;
using Sunday.Module.BusinessObjects.Security;
namespace Sunday.Module.BusinessObjects.SundayDataModel
{

    public partial class DepartmentMember
    {
        public DepartmentMember(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);


            switch(propertyName)
            {
                case nameof(DepartmentRole) when Employee != null:

                    if (oldValue != null && Employee.Roles.Any(x => x.Oid == ((CustomPermissionPolicyRole)oldValue).Oid))
                        Employee.Roles.Remove(((CustomPermissionPolicyRole)oldValue));
                        
                    if (newValue != null && !Employee.Roles.Any(x => x.Oid == ((CustomPermissionPolicyRole)newValue).Oid))
                        Employee.Roles.Add(((CustomPermissionPolicyRole)newValue));
                    break;
            }
        }


        protected override void OnDeleting()
        {
            base.OnDeleting();


            if (Employee == null || DepartmentRole == null) return;


            if (Employee.Roles.Any(x => x.Oid == DepartmentRole.Oid))
                Employee.Roles.Remove(DepartmentRole);
        }
    }

}
