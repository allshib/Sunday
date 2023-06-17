using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Sunday.Module.BusinessObjects.SundayDataModel;
using Sunday.Module.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunday.Module.Controllers {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SearchAddressController : ViewController {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        SimpleAction searchAction;
        public SearchAddressController() {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(AddressBase);
            TargetViewType = ViewType.DetailView;
            searchAction = new SimpleAction(this, "SearchAddressAction", "SearchAddressCategory") {
                //Specify the Action's button caption.
                Caption = "Search Action",
                TargetObjectType = TargetObjectType,
            };
            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            searchAction.Execute += SearchAction_Execute;
        }
        protected override void OnActivated() {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            
        }

        private void SearchAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            AddressBase address = View.CurrentObject as AddressBase;
            if (String.IsNullOrEmpty(address.FastAddressString)) throw new UserFriendlyException("Адрес не задан");


            address.FillAddressByQuery(address.FastAddressString);
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated() {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
