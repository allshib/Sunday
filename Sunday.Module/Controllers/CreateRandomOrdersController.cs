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
using Sunday.Module.BusinessObjects.Security;
using Sunday.Module.BusinessObjects.SundayDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunday.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CreateRandomOrdersController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CreateRandomOrdersController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(CustomerOrder);


            SimpleAction createRandomOrdersAction = new SimpleAction(this, "CreateRandomOrdersAction", PredefinedCategory.View)
            {
                //Specify the Action's button caption.
                Caption = "Создать случайные объекты"
            };
            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            createRandomOrdersAction.Execute += CreateRandomOrdersAction_Execute;


            SimpleAction removeAllAction = new SimpleAction(this, "RemoveAllAction", PredefinedCategory.View)
            {
                //Specify the Action's button caption.
                Caption = "Удалить всю коллекцию"
            };
            removeAllAction.Execute += RemoveAllAction_Execute;

        }

        private void RemoveAllAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var orders = ObjectSpace.GetObjects<CustomerOrder>();
            //while (orders.Count > 0)
            //{
                ObjectSpace.Delete(orders);
            //}
            ObjectSpace.CommitChanges();
        }

        private void CreateRandomOrdersAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var types = ObjectSpace.GetObjects<WorkType>();
            var customers = ObjectSpace.GetObjects<Customer>();
            var rnd = new Random();

            for (int i = 0; i < 250000; i++)
            {
                var order = ObjectSpace.CreateObject<CustomerOrder>();
                var type = types[rnd.Next(types.Count)];
                order.Type = type;
                order.Customer = customers[rnd.Next(customers.Count)];
                order.Name = type.Name;
            }

            ObjectSpace.CommitChanges();
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
