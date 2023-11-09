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

        [Browsable(false)]
        public string StateName { get => Name; set => Name = value; }
        [Browsable(false)]
        public int StateCode { get => Code; set => Code = value; }

        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
