﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Sunday.Module.BusinessObjects.SundayDataModel
{

    public partial class Customer : DevExpress.Persistent.BaseImpl.BaseObject
    {
        AddressBase fAddress;
        public AddressBase Address
        {
            get { return fAddress; }
            set { SetPropertyValue<AddressBase>(nameof(Address), ref fAddress, value); }
        }
        int fINN;
        public int INN
        {
            get { return fINN; }
            set { SetPropertyValue<int>(nameof(INN), ref fINN, value); }
        }
        string fEmail;
        public string Email
        {
            get { return fEmail; }
            set { SetPropertyValue<string>(nameof(Email), ref fEmail, value); }
        }
        Sunday.Common.SundayCommonDataModel.State fStatus;
        public Sunday.Common.SundayCommonDataModel.State Status
        {
            get { return fStatus; }
            set { SetPropertyValue<Sunday.Common.SundayCommonDataModel.State>(nameof(Status), ref fStatus, value); }
        }
        [Association(@"CustomerOrderReferencesCustomer")]
        public XPCollection<CustomerOrder> CustomerOrders { get { return GetCollection<CustomerOrder>(nameof(CustomerOrders)); } }
        [Association(@"CustomerPhoneReferencesCustomer"), Aggregated]
        public XPCollection<CustomerPhone> CustomerPhones { get { return GetCollection<CustomerPhone>(nameof(CustomerPhones)); } }
    }

}
