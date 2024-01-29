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
namespace Shib.XAF.Address.BusinessObjects.AddressModuleDataModel
{

    public partial class AddressBase : DevExpress.Persistent.BaseImpl.BaseObject
    {
        Country fCountry;
        public Country Country
        {
            get { return fCountry; }
            set { SetPropertyValue<Country>(nameof(Country), ref fCountry, value); }
        }
        Region fRegion;
        public Region Region
        {
            get { return fRegion; }
            set { SetPropertyValue<Region>(nameof(Region), ref fRegion, value); }
        }
        Locality fLocality;
        public Locality Locality
        {
            get { return fLocality; }
            set { SetPropertyValue<Locality>(nameof(Locality), ref fLocality, value); }
        }
        Street fStreet;
        public Street Street
        {
            get { return fStreet; }
            set { SetPropertyValue<Street>(nameof(Street), ref fStreet, value); }
        }
        int fPostalCode;
        public int PostalCode
        {
            get { return fPostalCode; }
            set { SetPropertyValue<int>(nameof(PostalCode), ref fPostalCode, value); }
        }
        double fLat;
        public double Lat
        {
            get { return fLat; }
            set { SetPropertyValue<double>(nameof(Lat), ref fLat, value); }
        }
        double fLon;
        public double Lon
        {
            get { return fLon; }
            set { SetPropertyValue<double>(nameof(Lon), ref fLon, value); }
        }
        string fFormattedAddress;
        public string FormattedAddress
        {
            get { return fFormattedAddress; }
            set { SetPropertyValue<string>(nameof(FormattedAddress), ref fFormattedAddress, value); }
        }
        [PersistentAlias("Iif([Country] <> null, [Country.Name], '') + ', ' + Iif([Region] <> null, [Region.Name], '') + ', ' + Iif([Locality] <> null, [Locality.Name], '') + ', ' + Iif([Street] <> null, [Street.Name] + ', ' + [Street.HouseNumber], '')")]
        public string FullAddress
        {
            get { return (string)(EvaluateAlias(nameof(FullAddress))); }
        }
    }

}
