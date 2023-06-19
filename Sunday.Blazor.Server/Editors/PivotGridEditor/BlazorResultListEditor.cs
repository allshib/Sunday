
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using Sunday.Module.BusinessObjects.SundayDataModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sunday.Blazor.Server.Editors.PivotGridEditor {

    [ListEditor(typeof(ExampleData))]
    public class BlazorResultListEditor : ListEditor {
        class XafPivotGridAdapter<T> : IComponentContentHolder {
            XafPivotGridModel<T> model;
            public XafPivotGridAdapter(XafPivotGridModel<T> model) {
                this.model = model;
            }
            public RenderFragment ComponentContent => XafPivotGrid<T>.Create(model);
        }

        public BlazorResultListEditor(IModelListView info) : base(info) { }

        private XafPivotGridModel<ExampleData> pivotGridModel;

        protected override object CreateControlsCore() {
            pivotGridModel = new XafPivotGridModel<ExampleData>();
            pivotGridModel.Fields = new XafPivotGridFieldModel[] {
                new XafPivotGridFieldModel() { Name = nameof(ExampleData.Sum), Area = PivotGridFieldArea.Data },
                new XafPivotGridFieldModel() { Name = nameof(ExampleData.Name), Area = PivotGridFieldArea.Row },
                new XafPivotGridFieldModel() { Name = nameof(ExampleData.Type), Area = PivotGridFieldArea.Column }
            };
            return new XafPivotGridAdapter<ExampleData>(pivotGridModel);
        }

        protected override void AssignDataSourceToControl(Object dataSource) {
            if (pivotGridModel != null && dataSource is QueryableCollection queryableCollection) {
                pivotGridModel.Data = (IEnumerable<ExampleData>)queryableCollection.Queryable;
            }
        }

        protected override void OnDataSourceChanged() {
            base.OnDataSourceChanged();
            if (pivotGridModel != null) {
                pivotGridModel.Update();
            }
        }

        public override void Refresh() { }

        public override object FocusedObject { get; set; }

        public override IList GetSelectedObjects() {
            return new object[0];
        }

        public override SelectionType SelectionType {
            get { return SelectionType.None; }
        }
    }
}