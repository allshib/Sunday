using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Sunday.Blazor.Server.Editors.HyperLinkPropertyEditor
{
    public class HyperLinkAdapter : ComponentAdapterBase
    {
        public const string UrlEmailMask = @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})";

        public HyperLinkAdapter(HyperLinkModel componentModel)
        {
            this.componentModel = componentModel ?? throw new ArgumentNullException(nameof(componentModel));
        }
        public HyperLinkModel componentModel { init; get; }

        public override IComponentModel ComponentModel => componentModel;

        public override void SetAllowEdit(bool allowEdit)
        {

        }
        public override object GetValue()
        {
            return ((HyperLinkModel)ComponentModel).Value;
        }
        private static bool IsValidUrl(string url)
        {
            return Regex.IsMatch(url, UrlEmailMask);
        }
        public override void SetValue(object value)
        {
            if (value == null) return;
            ((HyperLinkModel)ComponentModel).DisplayValue = value?.ToString();
            string url = value?.ToString();
            string result = "";
            if (url.Contains("@") && IsValidUrl(url))
            {
                result = string.Format("mailto:{0}", url);
            }
            else if (!url.Contains("://"))
            {
                result = string.Format("https://{0}", url);
            }
            ((HyperLinkModel)ComponentModel).Value = result;
        }
        protected override RenderFragment CreateComponent()
        {
            return ComponentModelObserver.Create(ComponentModel, HyperLinkRenderer.Create(((HyperLinkModel)ComponentModel)));
        }
        public override void SetAllowNull(bool allowNull) { /* ...*/ }
        public override void SetDisplayFormat(string displayFormat) { /* ...*/ }
        public override void SetEditMask(string editMask) { /* ...*/ }
        public override void SetEditMaskType(EditMaskType editMaskType) { /* ...*/ }
        public override void SetErrorIcon(ImageInfo errorIcon) { /* ...*/ }
        public override void SetErrorMessage(string errorMessage) { /* ...*/ }
        public override void SetIsPassword(bool isPassword) { /* ...*/ }
        public override void SetMaxLength(int maxLength) { /* ...*/ }
        public override void SetNullText(string nullText) { /* ...*/ }
    }
}
