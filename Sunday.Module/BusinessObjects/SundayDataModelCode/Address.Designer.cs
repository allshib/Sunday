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

    public partial class AddressBase : DevExpress.Persistent.BaseImpl.BaseObject
    {
        SundayCountry fCountry;
        public SundayCountry Country
        {
            get { return fCountry; }
            set { SetPropertyValue<SundayCountry>(nameof(Country), ref fCountry, value); }
        }
        SundayRegion fRegion;
        public SundayRegion Region
        {
            get { return fRegion; }
            set { SetPropertyValue<SundayRegion>(nameof(Region), ref fRegion, value); }
        }
        SundayStreet fStreet;
        public SundayStreet Street
        {
            get { return fStreet; }
            set { SetPropertyValue<SundayStreet>(nameof(Street), ref fStreet, value); }
        }
        Locality fLocality;
        public Locality Locality
        {
            get { return fLocality; }
            set { SetPropertyValue<Locality>(nameof(Locality), ref fLocality, value); }
        }
        int fZIP;
        public int ZIP
        {
            get { return fZIP; }
            set { SetPropertyValue<int>(nameof(ZIP), ref fZIP, value); }
        }
        [PersistentAlias("Iif([Country] <> null, [Country.Name], '') + ', ' + Iif([Region] <> null, [Region.Name], '') + ', ' + Iif([Locality] <> null, [Locality.Name], '') + ', ' + Iif([Street] <> null, [Street.Name] + ', ' + [Street.HouseNumber], '')")]
        public string Name
        {
            get { return (string)(EvaluateAlias(nameof(Name))); }
        }
        string fFastAddressString;
        public string FastAddressString
        {
            get { return fFastAddressString; }
            set { SetPropertyValue<string>(nameof(FastAddressString), ref fFastAddressString, value); }
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
    }

}
