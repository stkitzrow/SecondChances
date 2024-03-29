using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.PO;
using PX.Objects.SO;

namespace PX.Objects.SecondChances {

    [Serializable]
    [PXCacheName("SecondChances")]
    public class SecondChances : IBqlTable {

        #region ObjectID
        public abstract class objectID : BqlInt.Field<objectID> { }
        [PXDBIdentity]
        [PXUIField(DisplayName = "Tracking ID")]
        [PXSelector(typeof(Search<objectID>),
            DescriptionField = typeof(descr),
            SubstituteKey = typeof(objectCD),
            DirtyRead = true)]
        public virtual int? ObjectID { get; set; }
        #endregion

        #region ObjectCD
        public abstract class objectCD : BqlString.Field<objectCD> { }
        [PXDBString(30, IsUnicode = true, IsKey = true)]
        [PXUIField(DisplayName = "Tracking ID")]
        public virtual string ObjectCD { get; set; }
        #endregion

        #region Descr
        public abstract class descr : BqlString.Field<descr> { }
        [DBMatrixLocalizableDescription(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        [PX.Data.EP.PXFieldDescription]
        public virtual string Descr { get; set; }
        #endregion

        #region ItemClassID
        public abstract class itemClassID : BqlInt.Field<itemClassID> { }
        [PXDBInt]
        [PXUIField(DisplayName = "Item Class", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDimensionSelector(INItemClass.Dimension, typeof(Search<INItemClass.itemClassID>), typeof(INItemClass.itemClassCD), DescriptionField = typeof(INItemClass.descr), CacheGlobal = true)]
        public virtual int? ItemClassID { get; set; }
        #endregion

        #region ImageUrl
        public abstract class imageUrl : BqlString.Field<imageUrl> { }
        [PXDBString(255)]
        [PXUIField(DisplayName = "Image")]
        public virtual string ImageUrl { get; set; }
        #endregion

        #region Attributes
        public abstract class attributes : BqlAttributes.Field<attributes> { }
        [CRAttributesField(typeof(itemClassID))]
        public virtual string[] Attributes { get; set; }
        public virtual int? ClassID => ItemClassID;
        #endregion

        #region Body
        public abstract class body : BqlString.Field<body> { }
        [PXDBLocalizableString(IsUnicode = true)]
        [PXUIField(DisplayName = "Content")]
        public virtual string Body { get; set; }
        #endregion

        #region CustomerID
        public abstract class customerID : BqlInt.Field<customerID> { }
        [Customer(
            typeof(Search<BAccountR.bAccountID, Where<True, Equal<True>>>), // TODO: remove fake Where after AC-101187
            Visibility = PXUIVisibility.SelectorVisible,
            DescriptionField = typeof(Customer.acctName),
            Filterable = true)]
        public virtual int? CustomerID { get; set; }
        #endregion

        #region CustomerLocationID
        public abstract class customerLocationID : BqlInt.Field<customerLocationID> { }
        [LocationActive(typeof(Where<Location.bAccountID, Equal<Current<customerID>>,
            And<MatchWithBranch<Location.cBranchID>>>), DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible)]
        [PXDefault(typeof(Coalesce<Search2<BAccountR.defLocationID,
            InnerJoin<CRLocation, On<CRLocation.bAccountID, Equal<BAccountR.bAccountID>, And<CRLocation.locationID, Equal<BAccountR.defLocationID>>>>,
            Where<BAccountR.bAccountID, Equal<Current<customerID>>,
                And<CRLocation.isActive, Equal<True>,
                And<MatchWithBranch<CRLocation.cBranchID>>>>>,
            Search<CRLocation.locationID,
            Where<CRLocation.bAccountID, Equal<Current<customerID>>,
            And<CRLocation.isActive, Equal<True>, And<MatchWithBranch<CRLocation.cBranchID>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? CustomerLocationID { get; set; }
        #endregion

        #region WorkgroupID
        public abstract class workgroupID : BqlInt.Field<workgroupID> { }
        [PXDBInt]
        [TM.PXCompanyTreeSelector]
        [PXUIField(DisplayName = "Workgroup")]
        public virtual int? WorkgroupID { get; set; }
        #endregion

        #region OwnerID
        public abstract class ownerID : BqlInt.Field<ownerID> { }
        //[PXDefault(typeof(Coalesce<
        //    Search<CREmployee.defContactID, Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>>,
        //    Search<BAccount.ownerID, Where<BAccount.bAccountID, Equal<Current<customerID>>>>>),
        //    PersistingCheck = PXPersistingCheck.Nothing)]
        [TM.Owner(typeof(workgroupID))]
        public virtual int? OwnerID { get; set; }
        #endregion

        #region SiteID
        public abstract class siteID : BqlInt.Field<siteID> { }
        [SOSiteAvail]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? SiteID { get; set; }
        #endregion

        #region LocationID
        public abstract class locationID : BqlInt.Field<locationID> { }
        [Location(typeof(siteID))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? LocationID { get; set; }
        #endregion

        #region ShipDestType
        public abstract class shipDestType : BqlString.Field<shipDestType> { }
        [PXDBString(1, IsFixed = true)]
        [UpcyclingDestination.List]
        [PXDefault(UpcyclingDestination.None)]
        [PXUIField(DisplayName = "Destination Type")]
        public virtual string ShipDestType { get; set; }
        #endregion

        #region ShipToSiteID
        public abstract class shipToSiteID : BqlInt.Field<shipToSiteID> { }
        [Site(DescriptionField = typeof(INSite.descr), ErrorHandling = PXErrorHandling.Always)]
        public virtual int? ShipToSiteID { get; set; }
        #endregion

        #region BranchID
        public abstract class branchID : BqlInt.Field<branchID> { }
        [GL.Branch(null, null, addDefaultAttribute: false, useDefaulting: false, IsDetail = false)]
        //[PXFormula(typeof(Switch<Case<Where<POOrder.vendorLocationID, IsNotNull,
        //            And<Selector<POOrder.vendorLocationID, Location.vBranchID>, IsNotNull>>,
        //        Selector<POOrder.vendorLocationID, Location.vBranchID>,
        //        Case<Where<Current2<POOrder.branchID>, IsNotNull>,
        //            Current2<POOrder.branchID>>>,
        //    Current<AccessInfo.branchID>>))]
        public virtual int? BranchID { get; set; }
        #endregion

        #region ShipToBAccountID
        public abstract class shipToBAccountID : BqlInt.Field<shipToBAccountID> { }
        [PXDBInt]
        [PXSelector(typeof(
            Search2<BAccount2.bAccountID,
            LeftJoin<Vendor, On<
                Vendor.bAccountID, Equal<BAccount2.bAccountID>,
                And<Match<Vendor, Current<AccessInfo.userName>>>>,
            LeftJoin<AR.Customer, On<
                AR.Customer.bAccountID, Equal<BAccount2.bAccountID>,
                And<Match<AR.Customer, Current<AccessInfo.userName>>>>,
            LeftJoin<GL.Branch, On<
                GL.Branch.bAccountID, Equal<BAccount2.bAccountID>,
                And<Match<GL.Branch, Current<AccessInfo.userName>>>>>>>,
            Where<
                Vendor.bAccountID, IsNotNull, And<Optional<shipDestType>, Equal<UpcyclingDestination.vendor>,
                    And2<Where<BAccount2.type, In3<BAccountType.vendorType, BAccountType.combinedType>>,
            Or<Where<GL.Branch.bAccountID, IsNotNull, And<Optional<shipDestType>, Equal<UpcyclingDestination.company>,
                Or<Where<AR.Customer.bAccountID, IsNotNull, And<Optional<shipDestType>, Equal<UpcyclingDestination.customer>>>>>>>>>>>),
                typeof(BAccount.acctCD), typeof(BAccount.acctName), typeof(BAccount.type), typeof(BAccount.acctReferenceNbr), typeof(BAccount.parentBAccountID),
            SubstituteKey = typeof(BAccount.acctCD), DescriptionField = typeof(BAccount.acctName), CacheGlobal = true)]
        [PXUIField(DisplayName = "Ship To")]
        public virtual int? ShipToBAccountID { get; set; }
        #endregion

        #region ShipToLocationID
        public abstract class shipToLocationID : PX.Data.BQL.BqlInt.Field<shipToLocationID> { }
        [LocationActive(typeof(Where<Location.bAccountID, Equal<Current<shipToBAccountID>>>), DescriptionField = typeof(Location.descr))]
        //[PXDefault(null,
        //    typeof(Search<BAccount2.defLocationID,
        //            Where<BAccount2.bAccountID, Equal<Optional<shipToBAccountID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Shipping Location")]
        public virtual int? ShipToLocationID { get; set; }
        #endregion

        #region ShipAddressID
        public abstract class shipAddressID : PX.Data.BQL.BqlInt.Field<shipAddressID> { }
        [PXDBInt]
        [POShipAddress(typeof(Select2<Address,
                    InnerJoin<CRLocation, On<Address.bAccountID, Equal<CRLocation.bAccountID>,
                        And<Address.addressID, Equal<CRLocation.defAddressID>,
                        And<Current<shipDestType>, NotEqual<UpcyclingDestination.site>,
                        And<CRLocation.bAccountID, Equal<Current<shipToBAccountID>>,
                        And<CRLocation.locationID, Equal<Current<shipToLocationID>>>>>>>,
                    LeftJoin<POShipAddress, On<POShipAddress.bAccountID, Equal<Address.bAccountID>,
                        And<POShipAddress.bAccountAddressID, Equal<Address.addressID>,
                        And<POShipAddress.revisionID, Equal<Address.revisionID>,
                        And<POShipAddress.isDefaultAddress, Equal<boolTrue>>>>>>>,
                    Where<True, Equal<True>>>))]
        [PXUIField()]
        public virtual int? ShipAddressID { get; set; }
        #endregion

        #region ShipContactID
        public abstract class shipContactID : BqlInt.Field<shipContactID> { }
        [PXDBInt]
        [POShipContact(typeof(Select2<Contact,
                    InnerJoin<CRLocation, On<Contact.bAccountID, Equal<CRLocation.bAccountID>,
                        And<Contact.contactID, Equal<CRLocation.defContactID>,
                        And<Current<shipDestType>, NotEqual<UpcyclingDestination.site>,
                        And<CRLocation.bAccountID, Equal<Current<shipToBAccountID>>,
                        And<CRLocation.locationID, Equal<Current<shipToLocationID>>>>>>>,
                    LeftJoin<POShipContact, On<POShipContact.bAccountID, Equal<Contact.bAccountID>,
                        And<POShipContact.bAccountContactID, Equal<Contact.contactID>,
                        And<POShipContact.revisionID, Equal<Contact.revisionID>,
                        And<POShipContact.isDefaultContact, Equal<boolTrue>>>>>>>,
                    Where<True, Equal<True>>>))]
        [PXUIField()]
        public virtual int? ShipContactID { get; set; }
        #endregion

        #region InventoryID
        public abstract class inventoryID : BqlInt.Field<inventoryID> { }
        [CrossItem(Filterable = true)]
        public virtual int? InventoryID { get; set; }
        #endregion

        #region Status
        public abstract class status : BqlString.Field<status> { }
        [PXUIField(DisplayName = "Status")]
        [DocumentStatus.List]
        [PXDefault(DocumentStatus._New)]
        [PXDBString(1, IsFixed = true)]
        public virtual string Status { get; set; }
        #endregion

        #region Listing
        public abstract class listing : BqlString.Field<listing> { }
        [PXUIField(DisplayName = "Listing")]
        [Listing.List]
        [PXDBString(1, IsFixed = true)]
        public virtual string Listing { get; set; }
        #endregion

        #region ListingID
        public abstract class listingID : BqlString.Field<listingID> { }
        [PXDBString(128, IsUnicode = true)]
        [PXUIField(DisplayName = "Listing ID", IsReadOnly = true)]
        public virtual string ListingID { get; set; }
        #endregion

        #region ListingURL
        public abstract class listingURL : BqlString.Field<listingURL> { }
        [PXDBString(128, IsUnicode = true)]
        [PXUIField(DisplayName = "Listing URL", IsReadOnly = true)]
        public virtual string ListingURL { get; set; }
        #endregion

        #region NoteID 
        public abstract class noteID : BqlGuid.Field<noteID> { }
        [PXNote]
        public virtual Guid? NoteID { get; set; }
        #endregion

        #region CreatedByID 
        public abstract class createdByID : BqlGuid.Field<createdByID> { }
        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }
        #endregion

        #region CreatedByScreenID 
        public abstract class createdByScreenID : BqlString.Field<createdByScreenID> { }
        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "Screen ID", Enabled = false)]
        public virtual string CreatedByScreenID { get; set; }
        #endregion

        #region CreatedDateTime 
        public abstract class createdDateTime : BqlDateTime.Field<createdDateTime> { }
        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false)]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion

        #region LastModifiedByID 
        public abstract class lastModifiedByID : BqlGuid.Field<lastModifiedByID> { }
        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion

        #region LastModifiedByScreenID 
        public abstract class lastModifiedByScreenID : BqlString.Field<lastModifiedByScreenID> { }
        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion

        #region LastModifiedDateTime 
        public abstract class lastModifiedDateTime : BqlDateTime.Field<lastModifiedDateTime> { }
        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false)]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion

        #region tstamp 
        public abstract class Tstamp : BqlByteArray.Field<Tstamp> { }
        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}