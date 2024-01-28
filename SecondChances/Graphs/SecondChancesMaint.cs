using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;
using System.Collections;

namespace PX.Objects.SecondChances {
    public class SecondChancesMaint : PXGraph<SecondChancesMaint, SecondChances> {

        [PXViewName("SecondChances")]
        public PXSelect<SecondChances> Document;

        public PXSelect<SecondChances, Where<SecondChances.objectID, Equal<Current<SecondChances.objectID>>>> CurrentDocument;

        public CRAttributeList<SecondChances> Answers;

        public SecondChancesMaint() {
            Action.MenuAutoOpen = true;
        }

        protected virtual void _(Events.RowSelected<SecondChances> e) {
            var doc = e.Row;
            if (doc == null) {
                return;
            }
            var cache = e.Cache;
            PXUIFieldAttribute.SetEnabled<SecondChances.shipToBAccountID>(cache, doc, doc.ShipDestType != POShippingDestination.CompanyLocation && IsShipToBAccountRequired(doc));
            PXUIFieldAttribute.SetEnabled<SecondChances.shipToLocationID>(cache, doc, IsShipToBAccountRequired(doc));
            PXUIFieldAttribute.SetEnabled<SecondChances.siteID>(cache, doc, (doc.ShipDestType == POShippingDestination.Site));
            PXUIFieldAttribute.SetRequired<SecondChances.siteID>(cache, doc.ShipDestType == POShippingDestination.Site);
            PXUIFieldAttribute.SetRequired<SecondChances.shipToBAccountID>(cache, IsShipToBAccountRequired(doc));
            PXUIFieldAttribute.SetRequired<SecondChances.shipToLocationID>(cache, IsShipToBAccountRequired(doc));
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.shipDestType> e) {
            var row = e.Row;
            if (row == null) return;
            var cache = e.Cache;
            if (row.ShipDestType == POShippingDestination.Site) {
                cache.SetDefaultExt<POOrder.siteID>(e.Row);
                cache.SetValueExt<POOrder.shipToBAccountID>(e.Row, null);
                cache.SetValueExt<POOrder.shipToLocationID>(e.Row, null);
            } else if (row.ShipDestType == POShippingDestination.ProjectSite) {
                cache.SetValueExt<POOrder.siteID>(e.Row, null);
                cache.SetValueExt<POOrder.shipToBAccountID>(e.Row, null);
                cache.SetValueExt<POOrder.shipToLocationID>(e.Row, null);
            } else {
                cache.SetValueExt<POOrder.siteID>(e.Row, null);
                cache.SetDefaultExt<POOrder.shipToBAccountID>(e.Row);
                cache.SetDefaultExt<POOrder.shipToLocationID>(e.Row);
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.siteID> e) {
            var row = e.Row;
            var cache = e.Cache;

            if (row != null && row.ShipDestType == POShippingDestination.Site) {
                try {
                    POShipAddressAttribute.DefaultRecord<POOrder.shipAddressID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<POOrder.siteID>(e.Row, cache.GetValueExt<POOrder.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingAddressMayNotBeEmpty, PXErrorLevel.Error));
                    cache.SetValueExt<POOrder.shipAddressID>(e.Row, null);
                }
                try {
                    POShipContactAttribute.DefaultRecord<POOrder.shipContactID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<POOrder.siteID>(e.Row, cache.GetValueExt<POOrder.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingContactMayNotBeEmpty, PXErrorLevel.Error));
                    cache.SetValueExt<POOrder.shipContactID>(e.Row, null);
                }
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.shipToBAccountID> e) {
            var row = e.Row;
            var cache = e.Cache;
            if (row != null) {
                cache.SetDefaultExt<POOrder.shipToLocationID>(e.Row);
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.shipToLocationID> e) {
            var row = e.Row;
            var cache = e.Cache;
            if (row != null) {
                try {
                    POShipAddressAttribute.DefaultRecord<POOrder.shipAddressID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<POOrder.siteID>(e.Row, cache.GetValueExt<POOrder.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingAddressMayNotBeEmpty, PXErrorLevel.Error));
                }
                try {
                    POShipContactAttribute.DefaultRecord<POOrder.shipContactID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<POOrder.siteID>(e.Row, cache.GetValueExt<POOrder.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingContactMayNotBeEmpty, PXErrorLevel.Error));
                }
            }
        }

        protected virtual void _(Events.FieldVerifying<SecondChances, SecondChances.shipToLocationID> e) {
            var row = e.Row;
            var cache = e.Cache;
            if (row != null && !IsShipToBAccountRequired(row)) {
                e.Cancel = true;
                e.NewValue = null;
            }
        }

        protected virtual void _(Events.FieldVerifying<SecondChances, SecondChances.shipToBAccountID> e) {
            var row = e.Row;
            if (row == null) return;
            if (!IsShipToBAccountRequired(row)) {
                e.Cancel = true;
                e.NewValue = null;
            }
        }

        public PXAction<SecondChances> Action;
        [PXUIField(DisplayName = "Actions", MapEnableRights = PXCacheRights.Select)]
        [PXButton(MenuAutoOpen = true)]
        protected virtual IEnumerable action(PXAdapter adapter) {
            return adapter.Get();
        }

        public PXAction<SecondChances> sendToListing;
        [PXUIField(DisplayName = "Send To Listing Service", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable SendToListing(PXAdapter adapter) {
            Save.Press();
            var doc = Document.Current;
            if (doc != null) {
                DoSend(doc);
            }
            return adapter.Get();
        }

        private void DoSend(SecondChances doc) {
        }

        public virtual bool IsShipToBAccountRequired(SecondChances doc) {
            return doc.ShipDestType.IsNotIn(POShippingDestination.Site, POShippingDestination.ProjectSite);
        }
    }
}