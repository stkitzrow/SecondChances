using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.SO;

namespace PX.Objects.SecondChances {

    [Serializable]
    [PXCacheName("SecondChances")]
    public class SecondChances : IBqlTable {

        #region ObjectID
        public abstract class objectID : BqlInt.Field<objectID> { }
        [PXDBIdentity(IsKey = true)]
        [PXUIField(DisplayName = "Chance ID")]
        public virtual int? ObjectID { get; set; }
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
        //[PXDefault(typeof(
        //    Search2<INItemClass.itemClassID, InnerJoin<INSetup,
        //        On<stkItem.FromCurrent.IsEqual<False>.And<INSetup.dfltNonStkItemClassID.IsEqual<INItemClass.itemClassID>>.
        //        Or<stkItem.FromCurrent.IsEqual<True>.And<INSetup.dfltStkItemClassID.IsEqual<INItemClass.itemClassID>>>>>>))]
        //[PXUIRequired(typeof(INItemClass.stkItem))]
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
        [PXDefault]
        [Customer(
            typeof(Search<BAccountR.bAccountID, Where<True, Equal<True>>>), // TODO: remove fake Where after AC-101187
            Visibility = PXUIVisibility.SelectorVisible,
            DescriptionField = typeof(Customer.acctName),
            Filterable = true)]
        //[CustomerOrOrganizationInNoUpdateDocRestrictor]
        //[PXRestrictor(typeof(Where<Optional<SOOrder.isTransferOrder>, Equal<True>,
        //        Or<Customer.status, IsNull,
        //        Or<Customer.status, Equal<CustomerStatus.active>,
        //        Or<Customer.status, Equal<CustomerStatus.oneTime>>>>>),
        //    AR.Messages.CustomerIsInStatus,
        //    typeof(Customer.status))]
        //[PXForeignReference(typeof(Field<SOOrder.customerID>.IsRelatedTo<BAccount.bAccountID>))]
        public virtual int? CustomerID { get; set; }
        #endregion

        #region CustomerLocationID
        public abstract class customerLocationID : BqlInt.Field<customerLocationID> { }
        [LocationActive(typeof(Where<Location.bAccountID, Equal<Current<customerID>>,
            And<MatchWithBranch<Location.cBranchID>>>), DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible)]
        //[PXDefault(typeof(Coalesce<Search2<BAccountR.defLocationID,
        //    InnerJoin<CRLocation, On<CRLocation.bAccountID, Equal<BAccountR.bAccountID>, And<CRLocation.locationID, Equal<BAccountR.defLocationID>>>>,
        //    Where<BAccountR.bAccountID, Equal<Current<SOOrder.customerID>>,
        //        And<CRLocation.isActive, Equal<True>,
        //        And<MatchWithBranch<CRLocation.cBranchID>>>>>,
        //    Search<CRLocation.locationID,
        //    Where<CRLocation.bAccountID, Equal<Current<SOOrder.customerID>>,
        //    And<CRLocation.isActive, Equal<True>, And<MatchWithBranch<CRLocation.cBranchID>>>>>>))]
        //[PXForeignReference(
        //    typeof(CompositeKey<
        //        Field<SOOrder.customerID>.IsRelatedTo<Location.bAccountID>,
        //        Field<SOOrder.customerLocationID>.IsRelatedTo<Location.locationID>
        //    >))]
        public virtual int? CustomerLocationID { get; set; }
        #endregion

        #region SiteID
        public abstract class siteID : BqlInt.Field<siteID> { }
        [SOSiteAvail(DocumentBranchType = typeof(SOOrder.branchID))]
        [PXParent(typeof(Select<SOOrderSite, Where<SOOrderSite.orderType, Equal<Current<SOLine.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOOrderSite.siteID, Equal<Current2<SOLine.siteID>>>>>>), LeaveChildren = true, ParentCreate = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        //[PXUIRequired(typeof(IIf<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, True, False>))]
        //[InterBranchRestrictor(typeof(Where2<SameOrganizationBranch<INSite.branchID, Current<SOOrder.branchID>>,
        //    Or<Current<SOOrder.behavior>, Equal<SOBehavior.qT>>>))]
        //[PXUnboundFormula(typeof(IIf<Where<openLine.IsEqual<True>>, int1, int0>), typeof(SumCalc<SOOrderSite.openLineCntr>), SkipZeroUpdates = false, ValidateAggregateCalculation = true)]
        public virtual int? SiteID { get; set; }
        #endregion

        #region LocationID
        public abstract class locationID : BqlInt.Field<locationID> { }
        [Location(typeof(siteID))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? LocationID { get; set; }
        #endregion

        #region ContactID
        public abstract class contactID : BqlInt.Field<contactID> { }
        [ContactRaw()]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        //[PXUIEnabled(typeof(Where<SOOrder.customerID, IsNotNull>))]
        public virtual int? ContactID { get; set; }
        #endregion

        #region WorkgroupID
        public abstract class workgroupID : BqlInt.Field<workgroupID> { }
        [PXDBInt]
        //[PXDefault(typeof(Customer.workgroupID), PersistingCheck = PXPersistingCheck.Nothing)]
        [PX.TM.PXCompanyTreeSelector]
        [PXUIField(DisplayName = "Workgroup")]
        public virtual int? WorkgroupID { get; set; }
        #endregion

        #region OwnerID
        public abstract class ownerID : BqlInt.Field<ownerID> { }
        //[PXDefault(typeof(Coalesce<
        //    Search<CREmployee.defContactID, Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>>,
        //    Search<BAccount.ownerID, Where<BAccount.bAccountID, Equal<Current<SOOrder.customerID>>>>>),
        //    PersistingCheck = PXPersistingCheck.Nothing)]
        [PX.TM.Owner(typeof(workgroupID))]
        public virtual int? OwnerID { get; set; }
        #endregion

        #region InventoryID
        public abstract class inventoryID : BqlInt.Field<inventoryID> {
            //public class InventoryBaseUnitRule :
            //    InventoryItem.baseUnit.PreventEditIfExists<
            //        Select<SOLine,
            //        Where<inventoryID, Equal<Current<InventoryItem.inventoryID>>,
            //            And<lineType, In3<SOLineType.inventory, SOLineType.nonInventory>,
            //            And<completed, NotEqual<True>>>>>> { }
        }
        [CrossItem(Filterable = true)]
        //[PXDefault()]
        //[PXForeignReference(typeof(FK.InventoryItem))]
        //[ConvertedInventoryItem(typeof(isStockItem))]
        public virtual int? InventoryID { get; set; }
        #endregion

    }
}