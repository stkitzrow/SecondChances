using PX.Data;

namespace PX.Objects.SecondChances {
    public class DocumentStatus {
        public class ListAttribute : PXStringListAttribute {

            public ListAttribute() : base(("N", "New"), ("E", "Under Evaluation"),
                ("C", "Recycle"), ("S", "Resell"), ("U", "Reuse"), ("D", "Donate")) { }
        }
    }
}
