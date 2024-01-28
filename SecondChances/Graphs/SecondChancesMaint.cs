using Newtonsoft.Json;
using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
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

        public PXAction<SecondChances> receiveInventory;
        [PXUIField(DisplayName = "Receive Inventory", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ReceiveInventory(PXAdapter adapter) {
            return adapter.Get();
        }

        private void DoSend(SecondChances doc) {
            var client = new SecondChancesRestClient();
            var imgBytes = GetImageBytes(doc);
            var response = client.PostProductListing(doc.Descr, imgBytes).Result;
            var product = JsonConvert.DeserializeObject<ShopifyObj>(response.Content).product;
            if (product != null) {
                var id = product.id;
                var url = SecondChancesRestClient.BASE_URL + SecondChancesRestClient.ROUTE + product.handle;
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
            var bytes = fm.GetFileWithNoData(files.FirstOrDefault())?.BinData ?? new byte[0];
            return bytes;
        }

        public virtual bool IsShipToBAccountRequired(SecondChances doc) {
            return doc.ShipDestType.IsNotIn(POShippingDestination.Site, POShippingDestination.ProjectSite);
        }


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