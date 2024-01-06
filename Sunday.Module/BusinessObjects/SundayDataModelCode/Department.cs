using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Sunday.Module.BusinessObjects.Security;
namespace Sunday.Module.BusinessObjects.SundayDataModel
{

    public partial class Department
    {
        public Department(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }





        [Association(@"CustomPermissionPolicyRoleReferencesDepartment")]
        public XPCollection<CustomPermissionPolicyRole> DepartmentRoles { get { return GetCollection<CustomPermissionPolicyRole>(nameof(DepartmentRoles)); } }
    }

}
