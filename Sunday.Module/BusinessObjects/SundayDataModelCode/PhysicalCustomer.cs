using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Sunday.Module.BusinessObjects.SundayDataModel
{

    public partial class PhysicalCustomer
    {
        public PhysicalCustomer(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }



        [Browsable(false)]
        [PersistentAlias("[ShortName] + '. Заказов ' + ToStr([CustomerOrders][].Count())")]
        private string DefaultPropertyString => EvaluateAlias(nameof(DefaultPropertyString)).ToString();


        public override string DefaultProperty => DefaultPropertyString;
    }

}
