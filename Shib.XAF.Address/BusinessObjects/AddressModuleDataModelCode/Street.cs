using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Shib.XAF.Address.BusinessObjects.AddressModuleDataModel
{

    [System.ComponentModel.DisplayName(nameof(Name))]
    public partial class Street
    {
        public Street(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
