using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Shib.XAF.Address.BusinessObjects.AddressModuleDataModel
{

    public partial class Region
    {
        public Region(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
