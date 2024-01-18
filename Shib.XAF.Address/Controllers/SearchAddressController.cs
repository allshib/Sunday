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
using DevExpress.Xpo.DB.Helpers;
using Microsoft.Extensions.DependencyInjection;

using Shib.Common.Interfaces.Address;
using Shib.XAF.Address.BusinessObjects.NonPersistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shib.XAF.Address.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SearchAddressController : ViewController
    {
        private readonly IAddressSearcher addressSearcher;
        public SearchAddressController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(AddressPicker);
            TargetViewType = ViewType.DetailView;


            SimpleAction searchAddress = new SimpleAction(this, "SearchAddress", PredefinedCategory.View)
            {
                Caption = "Найти адрес",
            };

            searchAddress.Execute += SearchAddress_Execute;
        }

        [ActivatorUtilitiesConstructor]
        public SearchAddressController(IAddressSearcher addressSearcher) : this()
        {
            this.addressSearcher = addressSearcher;
        }

        private void SearchAddress_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var picker = View.CurrentObject as AddressPicker;

            var nominatimAddresses = addressSearcher.GetAddressList(picker.SearchString);



            foreach(var address in nominatimAddresses)
            {
                picker.Adresses.Add((AddressEntity)address);
            }

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
