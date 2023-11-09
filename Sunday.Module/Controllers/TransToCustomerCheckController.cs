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
using Sunday.Common.NonPersistent.Types;
using Sunday.Common.SundayCommonDataModel;
using Sunday.Module.BusinessObjects.SundayDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunday.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TransToCustomerCheckController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public TransToCustomerCheckController()
        {
            InitializeComponent();
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(Customer);

            SimpleAction TransToCustomer = new SimpleAction(this, "TransToCustomer", PredefinedCategory.View)
            {
                Caption = "Проверка перевозчика",
            };

            TransToCustomer.Execute += TransToCustomer_Execute;
        }

        private void TransToCustomer_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var customer = View.CurrentObject as Customer;

            customer.CheckFinished += Customer_CheckFinished;


            var newState = ObjectSpace.FindObject<State>(CriteriaOperator.Parse("StateCode = ?", 2));


            customer.GoToState(newState);
            
        }

        private void Customer_CheckFinished(StateCheckResult stateCheckResult)
        {
            Console.WriteLine("ТЕст");
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
