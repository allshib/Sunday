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
using Microsoft.Extensions.DependencyInjection;
using Shib.Common.Interfaces.Address;
using Shib.XAF.Address.BusinessObjects.NonPersistent;
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
        SimpleAction detailedSearchAction;
        private readonly IAddressSearcher addressSearcher;
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
            searchAction.Execute += SearchAction_Execute;

            detailedSearchAction = new SimpleAction(this, "DetailedSearchAction", "SearchAddressCategory")
            {
                //Specify the Action's button caption.
                Caption = "Детальный поиск",
                TargetObjectType = TargetObjectType,
            };
            detailedSearchAction.Execute += SearchAction_Execute1;

        }

        [ActivatorUtilitiesConstructor]
        public SearchAddressController(IAddressSearcher addressSearcher) : this()
        {
            this.addressSearcher = addressSearcher;
        }


        private void SearchAction_Execute1(object sender, SimpleActionExecuteEventArgs e)
        {
            AddressBase address = View.CurrentObject as AddressBase;

            var os = Application.CreateObjectSpace(typeof(AddressPicker));

            var addressPicker = new AddressPicker(address.FastAddressString, addressSearcher.Name);
            DetailView detailView = Application.CreateDetailView(os, addressPicker);
            detailView.CurrentObject = addressPicker;
            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;

            Application.ShowViewStrategy.ShowViewInPopupWindow(detailView,
            () => {
                var addressEntity = addressPicker.Adresses.SingleOrDefault(x => x.Selected);

                if (addressEntity == null)
                    throw new UserFriendlyException("Адрес не найден");

                address.FillAddressFromEntity(addressEntity);
                Application.ShowViewStrategy.ShowMessage("Done.");
            },
            () => Application.ShowViewStrategy.ShowMessage("Cancelled."),
            null, null);

            

        }

        protected override void OnActivated() {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            AddressBase address = View.CurrentObject as AddressBase;
            address.ApiName = addressSearcher.Name;
        }

        private void SearchAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            AddressBase address = View.CurrentObject as AddressBase;

            if (String.IsNullOrEmpty(address.FastAddressString)) 
                throw new UserFriendlyException("Адрес не задан");


            var searchedAddress = addressSearcher.GetAddress(address.FastAddressString);

            if (searchedAddress == null)
                throw new UserFriendlyException("Адрес не найден");

            address.FillAddressFromEntity(searchedAddress);
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
