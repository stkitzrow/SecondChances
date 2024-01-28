using PX.Data;
using PX.Data.BQL;

namespace PX.Objects.SecondChances {

    public class UpcyclingDestination {
        public class ListAttribute : PXStringListAttribute {
            public ListAttribute()
                : base(
                      PXStringListAttribute.Pair(None, "No Destination"),
                      PXStringListAttribute.Pair(CompanyLocation, "Branch"), 
                      PXStringListAttribute.Pair(Customer, "Customer"), 
                      PXStringListAttribute.Pair(Vendor, "Vendor"), 
                      PXStringListAttribute.Pair(Site, "Warehouse")) {
            }
        }

        public class company : BqlType<IBqlString, string>.Constant<company> {
            public company()
                : base("L") {
            }
        }

        public class customer : BqlType<IBqlString, string>.Constant<customer> {
            public customer()
                : base("C") {
            }
        }

        public class vendor : BqlType<IBqlString, string>.Constant<vendor> {
            public vendor()
                : base("V") {
            }
        }

        public class site : BqlType<IBqlString, string>.Constant<site> {
            public site()
                : base("S") {
            }
        }

        public class projectSite : BqlType<IBqlString, string>.Constant<projectSite> {
            public projectSite()
                : base("P") {
            }
        }

        public const string None = "-";
        public const string CompanyLocation = "L";
        public const string Customer = "C";
        public const string Vendor = "V";
        public const string Site = "S";
    }
}
