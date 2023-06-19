using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Sunday.Blazor.Server.Editors.PivotGridEditor {
    public class XafPivotGridModel<T> {
        public IEnumerable<T> Data { get; set; }
        public IEnumerable<XafPivotGridFieldModel> Fields { get; set; }
        public Action Update { get; set; }
    }
    public class XafPivotGridFieldModel {
        public string Name { get; set; }
        public PivotGridFieldArea Area { get; set; }
    }
    public class XafPivotGridAdapter<T> : IComponentContentHolder {
        XafPivotGridModel<T> model;
        public XafPivotGridAdapter(XafPivotGridModel<T> model) {
            this.model = model;
        }
        public RenderFragment ComponentContent => XafPivotGrid<T>.Create(model);
    }
}
