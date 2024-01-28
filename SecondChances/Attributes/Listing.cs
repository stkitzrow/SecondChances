using PX.Data;

namespace PX.Objects.SecondChances {

    public class Listing {
        public class ListAttribute : PXStringListAttribute {
            public ListAttribute()
                : base(
                      PXStringListAttribute.Pair(None, "None"),
                      PXStringListAttribute.Pair(EBay, "eBay"),
                      PXStringListAttribute.Pair(Shopify, "Shopify"),
                      PXStringListAttribute.Pair(FreeCycle, "Freecycle: Front Door"),
                      PXStringListAttribute.Pair(NextDoor, "Nextdoor"),
                      PXStringListAttribute.Pair(CraigList, "Craiglist"),
                      PXStringListAttribute.Pair(Facebook, "Facebook"),
                      PXStringListAttribute.Pair(TooGood, "Too Good To Go"),
                      PXStringListAttribute.Pair(UpcycleThat, "Upcycle That: Home"),
                      PXStringListAttribute.Pair(OfferUp, "OfferUp")) {
            }
        }

        public const string None = "-";
        public const string EBay = "E";
        public const string Shopify = "S";
        public const string FreeCycle = "F";
        public const string NextDoor = "N";
        public const string CraigList = "C";
        public const string Facebook = "B";
        public const string TooGood = "T";
        public const string UpcycleThat = "U";
        public const string OfferUp = "O";
    }
}
