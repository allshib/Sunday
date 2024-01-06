using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using Sunday.Module.BusinessObjects.Enums;
using Sunday.Module.BusinessObjects.SundayDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Module.BusinessObjects.Security
{


    [MapInheritance(MapInheritanceType.ParentTable)]
    public class CustomPermissionPolicyRole : PermissionPolicyRole
    {
        public CustomPermissionPolicyRole(Session session) : base(session)
        {
        }


        private Department _Department;

        [Association(@"CustomPermissionPolicyRoleReferencesDepartment")]
        public Department Department
        {
            get { return _Department; }
            set { SetPropertyValue(nameof(Department), ref _Department, value); }
        }
    }
}
