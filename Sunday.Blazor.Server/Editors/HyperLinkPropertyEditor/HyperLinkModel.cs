using DevExpress.ExpressApp.Blazor.Components.Models;

namespace Sunday.Blazor.Server.Editors.HyperLinkPropertyEditor
{
    public class HyperLinkModel : ComponentModelBase
    {
        public string Value
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public string DisplayValue
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

    }
}
