using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using Sunday.Module.BusinessObjects.Enums;

namespace Sunday.Module.BusinessObjects.Security;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo
{
    public ApplicationUser(Session session) : base(session) { }


    private UserType _Type;
    public UserType Type
    {
        get { return _Type; }
        set { SetPropertyValue(nameof(Type), ref _Type, value); }
    }

    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo
    {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
    {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
}
