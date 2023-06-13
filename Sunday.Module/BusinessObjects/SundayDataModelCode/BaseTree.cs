using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Persistent.Base.General;

namespace Sunday.Module.BusinessObjects.SundayDataModel {

    public partial class BaseTree : ITreeNode {
        public BaseTree(Session session) : base(session) { }

        public IBindingList Children => BaseTreeCollection;

        ITreeNode ITreeNode.Parent => this.Parent;

        string ITreeNode.Name => this.Name;

        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
