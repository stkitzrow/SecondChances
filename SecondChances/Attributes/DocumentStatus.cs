using PX.Data;

namespace PX.Objects.SecondChances {

    public class DocumentStatus {
        public class ListAttribute : PXStringListAttribute {
            public ListAttribute()
                : base(
                      PXStringListAttribute.Pair(_New, "New"),
                      PXStringListAttribute.Pair(Under_Eval, "Under Evaluation"), 
                      PXStringListAttribute.Pair(Recycle, "Recycle"), 
                      PXStringListAttribute.Pair(Resell, "Resell"), 
                      PXStringListAttribute.Pair(Reuse, "Reuse"), 
                      PXStringListAttribute.Pair(Donate, "Donate")) {
            }
        }

        public const string _New = "-";
        public const string Under_Eval = "E";
        public const string Recycle = "C";
        public const string Resell = "S";
        public const string Reuse = "U";
        public const string Donate = "D";
    }
}
