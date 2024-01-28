using Newtonsoft.Json;
using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Linq;

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
            var row = e.Row;
            if (row == null) return;
            var cache = e.Cache;
            PXUIFieldAttribute.SetEnabled<SecondChances.itemClassID>(cache, row, true);
            PXUIFieldAttribute.SetEnabled<SecondChances.inventoryID>(cache, row, true);
            PXUIFieldAttribute.SetEnabled<SecondChances.shipDestType>(cache, row, false);
            PXUIFieldAttribute.SetEnabled<SecondChances.shipToBAccountID>(cache, row, false);
            PXUIFieldAttribute.SetEnabled<SecondChances.shipToLocationID>(cache, row, false);
            PXUIFieldAttribute.SetEnabled<SecondChances.shipToSiteID>(cache, row, false);
            PXUIFieldAttribute.SetEnabled<SecondChances.branchID>(cache, row, false);
            PXUIFieldAttribute.SetEnabled<SecondChances.listing>(cache, row, false);
            PXUIFieldAttribute.SetRequired<SecondChances.itemClassID>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.inventoryID>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.shipDestType>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.shipToBAccountID>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.shipToLocationID>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.shipToSiteID>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.branchID>(cache, false);
            PXUIFieldAttribute.SetRequired<SecondChances.listing>(cache, false);
            switch (row.Status) {
                case DocumentStatus._New:
                    break;
                case DocumentStatus.Under_Eval:
                    break;
                case DocumentStatus.Recycle:
                case DocumentStatus.Donate:
                    PXUIFieldAttribute.SetEnabled<SecondChances.shipDestType>(cache, row, true);
                    PXUIFieldAttribute.SetRequired<SecondChances.shipDestType>(cache, true);
                    break;
                case DocumentStatus.Reuse:
                    //PXUIFieldAttribute.SetEnabled<SecondChances.inventoryID>(cache, row, true);
                    //PXUIFieldAttribute.SetEnabled<SecondChances.itemClassID>(cache, row, true);
                    //PXUIFieldAttribute.SetRequired<SecondChances.listing>(cache, false);
                    //PXUIFieldAttribute.SetRequired<SecondChances.listing>(cache, false);
                    break;
                case DocumentStatus.Resell:
                    PXUIFieldAttribute.SetEnabled<SecondChances.listing>(cache, row, true);
                    PXUIFieldAttribute.SetRequired<SecondChances.listing>(cache, true);
                    break;
            }
            switch (row.ShipDestType) {
                case UpcyclingDestination.Site:
                    PXUIFieldAttribute.SetEnabled<SecondChances.siteID>(cache, row, true);
                    PXUIFieldAttribute.SetRequired<SecondChances.siteID>(cache, true);
                    break;
                case UpcyclingDestination.Vendor:
                case UpcyclingDestination.Customer:
                    PXUIFieldAttribute.SetEnabled<SecondChances.shipToBAccountID>(cache, row, true);
                    PXUIFieldAttribute.SetEnabled<SecondChances.shipToLocationID>(cache, row, true);
                    PXUIFieldAttribute.SetRequired<SecondChances.shipToBAccountID>(cache, true);
                    PXUIFieldAttribute.SetRequired<SecondChances.shipToLocationID>(cache, true);
                    break;
                case UpcyclingDestination.CompanyLocation:
                    PXUIFieldAttribute.SetEnabled<SecondChances.branchID>(cache, row, true);
                    PXUIFieldAttribute.SetRequired<SecondChances.branchID>(cache, true);
                    break;
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.shipDestType> e) {
            var row = e.Row;
            if (row == null) return;
            var cache = e.Cache;
            if (row.ShipDestType == UpcyclingDestination.Site) {
                //cache.SetDefaultExt<SecondChances.siteID>(e.Row);
                cache.SetValueExt<SecondChances.shipToBAccountID>(e.Row, null);
                cache.SetValueExt<SecondChances.shipToLocationID>(e.Row, null);
            //} else if (row.ShipDestType == UpcyclingDestination.ProjectSite) {
            //    cache.SetValueExt<SecondChances.siteID>(e.Row, null);
            //    cache.SetValueExt<SecondChances.shipToBAccountID>(e.Row, null);
            //    cache.SetValueExt<SecondChances.shipToLocationID>(e.Row, null);
            } else {
                cache.SetValueExt<SecondChances.siteID>(e.Row, null);
                //cache.SetDefaultExt<SecondChances.shipToBAccountID>(e.Row);
                //cache.SetDefaultExt<SecondChances.shipToLocationID>(e.Row);
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.status> e) {
            var row = e.Row;
            if (row == null) return;
            var cache = e.Cache;
            switch (row.Status) {
                case DocumentStatus._New:
                    break;
                case DocumentStatus.Under_Eval:
                    break;
                case DocumentStatus.Recycle:
                case DocumentStatus.Donate:
                    break;
                case DocumentStatus.Reuse:
                case DocumentStatus.Resell:
                    cache.SetDefaultExt<SecondChances.shipDestType>(e.Row);
                    cache.SetValueExt<SecondChances.shipToBAccountID>(e.Row, null);
                    cache.SetValueExt<SecondChances.shipToLocationID>(e.Row, null);
                    cache.SetValueExt<SecondChances.siteID>(e.Row, null);
                    cache.SetValueExt<SecondChances.branchID>(e.Row, null);
                    break;
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.siteID> e) {
            var row = e.Row;
            var cache = e.Cache;

            if (row != null && row.ShipDestType == UpcyclingDestination.Site) {
                try {
                    POShipAddressAttribute.DefaultRecord<SecondChances.shipAddressID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<SecondChances.siteID>(e.Row, cache.GetValueExt<SecondChances.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingAddressMayNotBeEmpty, PXErrorLevel.Error));
                    cache.SetValueExt<SecondChances.shipAddressID>(e.Row, null);
                }
                try {
                    POShipContactAttribute.DefaultRecord<SecondChances.shipContactID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<SecondChances.siteID>(e.Row, cache.GetValueExt<SecondChances.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingContactMayNotBeEmpty, PXErrorLevel.Error));
                    cache.SetValueExt<SecondChances.shipContactID>(e.Row, null);
                }
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.shipToBAccountID> e) {
            var row = e.Row;
            var cache = e.Cache;
            if (row != null) {
                cache.SetDefaultExt<SecondChances.shipToLocationID>(e.Row);
            }
        }

        protected virtual void _(Events.FieldUpdated<SecondChances, SecondChances.shipToLocationID> e) {
            var row = e.Row;
            var cache = e.Cache;
            if (row != null) {
                try {
                    POShipAddressAttribute.DefaultRecord<SecondChances.shipAddressID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<SecondChances.siteID>(e.Row, cache.GetValueExt<SecondChances.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingAddressMayNotBeEmpty, PXErrorLevel.Error));
                }
                try {
                    POShipContactAttribute.DefaultRecord<SecondChances.shipContactID>(cache, e.Row);
                } catch (SharedRecordMissingException) {
                    cache.RaiseExceptionHandling<SecondChances.siteID>(e.Row, cache.GetValueExt<SecondChances.siteID>(e.Row),
                        new PXSetPropertyException(PO.Messages.ShippingContactMayNotBeEmpty, PXErrorLevel.Error));
                }
            }
        }

        //protected virtual void _(Events.FieldVerifying<SecondChances, SecondChances.shipToLocationID> e) {
        //    var row = e.Row;
        //    var cache = e.Cache;
        //    if (row != null && !IsShipToBAccountRequired(row)) {
        //        e.Cancel = true;
        //        e.NewValue = null;
        //    }
        //}

        //protected virtual void _(Events.FieldVerifying<SecondChances, SecondChances.shipToBAccountID> e) {
        //    var row = e.Row;
        //    if (row == null) return;
        //    if (!IsShipToBAccountRequired(row)) {
        //        e.Cancel = true;
        //        e.NewValue = null;
        //    }
        //}

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

        public PXAction<SecondChances> receiveInventory;
        [PXUIField(DisplayName = "Receive Inventory", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ReceiveInventory(PXAdapter adapter) {
            return adapter.Get();
        }

        private void DoSend(SecondChances doc) {
            var client = new SecondChancesRestClient();
            var imgBytes = GetImageBytes(doc);
            var itemClass = INItemClass.PK.Find(this, doc.ItemClassID);
            var response = client.PostProductListing(doc.Descr, doc.Body, itemClass?.Descr, imgBytes);
            var product = JsonConvert.DeserializeObject<ShopifyObj>(response.Content).product;
            if (product != null) {
                var id = product.id;
                var url = SecondChancesRestClient.BASE_URL + SecondChancesRestClient.ROUTE + '/' + product.handle;
                doc.ListingID = id.ToString();
                doc.ListingURL = url;
                Document.Current = doc;
                Document.Cache.Update(doc);
                Actions.PressSave();
            }
        }

        private byte[] GetImageBytes(SecondChances doc) {
            var cache = Caches[typeof(SecondChances)];
            var fm = PXGraph.CreateInstance<PX.SM.UploadFileMaintenance>();
            var files = PXNoteAttribute.GetFileNotes(cache, cache.Current);
            var bytes = fm.GetFile(files.FirstOrDefault())?.BinData ?? new byte[0];
            return bytes;
        }

        //public virtual bool IsShipToBAccountRequired(SecondChances doc) {
        //    return doc.ShipDestType.IsNotIn(UpcyclingDestination.Site, UpcyclingDestination.ProjectSite);
        //}


        public class ShopifyObj {
            public Product product { get; set; }
        }

        public class Product {
            public long id { get; set; }
            public string title { get; set; }
            public string body_html { get; set; }
            public string vendor { get; set; }
            public string product_type { get; set; }
            public DateTime created_at { get; set; }
            public string handle { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime published_at { get; set; }
            public object template_suffix { get; set; }
            public string published_scope { get; set; }
            public string tags { get; set; }
            public string status { get; set; }
            public string admin_graphql_api_id { get; set; }
            public Variant[] variants { get; set; }
            public Option[] options { get; set; }
            public Image1[] images { get; set; }
            public Image image { get; set; }
        }

        public class Image {
            public long id { get; set; }
            public object alt { get; set; }
            public int position { get; set; }
            public long product_id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string admin_graphql_api_id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string src { get; set; }
            public object[] variant_ids { get; set; }
        }

        public class Variant {
            public long id { get; set; }
            public long product_id { get; set; }
            public string title { get; set; }
            public string price { get; set; }
            public string sku { get; set; }
            public int position { get; set; }
            public string inventory_policy { get; set; }
            public object compare_at_price { get; set; }
            public string fulfillment_service { get; set; }
            public object inventory_management { get; set; }
            public string option1 { get; set; }
            public object option2 { get; set; }
            public object option3 { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public bool taxable { get; set; }
            public object barcode { get; set; }
            public int grams { get; set; }
            public object image_id { get; set; }
            public float weight { get; set; }
            public string weight_unit { get; set; }
            public long inventory_item_id { get; set; }
            public int inventory_quantity { get; set; }
            public int old_inventory_quantity { get; set; }
            public bool requires_shipping { get; set; }
            public string admin_graphql_api_id { get; set; }
        }

        public class Option {
            public long id { get; set; }
            public long product_id { get; set; }
            public string name { get; set; }
            public int position { get; set; }
            public string[] values { get; set; }
        }

        public class Image1 {
            public long id { get; set; }
            public object alt { get; set; }
            public int position { get; set; }
            public long product_id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string admin_graphql_api_id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string src { get; set; }
            public object[] variant_ids { get; set; }
        }

    }
}