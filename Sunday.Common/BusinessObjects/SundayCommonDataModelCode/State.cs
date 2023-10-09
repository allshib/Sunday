using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Sunday.Common.Interfaces;

namespace Sunday.Common.SundayCommonDataModel
{

    public partial class State : IState
    {
        public State(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
