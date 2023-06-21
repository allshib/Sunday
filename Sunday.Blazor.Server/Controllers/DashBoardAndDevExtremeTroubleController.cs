using DevExpress.DashboardBlazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Blazor.Components;
using Microsoft.AspNetCore.Components;

namespace Sunday.Blazor.Server.Controllers {

    /// <summary>
    /// Контроллер используется для устранения ошибок при совместном использовании DevExtreme модуля совместно с модулем Dashboard
    /// https://docs.devexpress.com/eXpressAppFramework/404346
    /// </summary>
    public class DashBoardAndDevExtremeTroubleController : ViewController<DetailView> {
        protected override void OnActivated() {
            base.OnActivated();
            View.CustomizeViewItemControl<BlazorDashboardViewerViewItem>(this, CustomizeDashboardViewerViewItem);
        }
        void CustomizeDashboardViewerViewItem(BlazorDashboardViewerViewItem dashboardViewItem) {
            if (dashboardViewItem.Control is DxDashboardViewerAdapter adapter) {
                adapter.JSCustomizationModel.OnScriptsLoading =
                    EventCallback.Factory.Create<ScriptsLoadingEventArgs>(this, OnScriptsLoading);
            }
        }
        private void OnScriptsLoading(ScriptsLoadingEventArgs args) {
            args.Scripts.Remove(ScriptIdentifiers.JQuery);
            args.Scripts.Remove(ScriptIdentifiers.Knockout);
            args.Scripts.Remove(ScriptIdentifiers.DevExtreme);
        }
    }
}
