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
namespace Sunday.Module.BusinessObjects.SundayDataModel {

    public partial class PrimaryGeographicalUnit : DevExpress.Persistent.BaseImpl.BaseObject {
        string fName;
        public string Name {
            get { return fName; }
            set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
        }
        [Association(@"LocalityReferencesPrimaryGeographicalUnit")]
        public XPCollection<Locality> Localities { get { return GetCollection<Locality>(nameof(Localities)); } }
    }

}
