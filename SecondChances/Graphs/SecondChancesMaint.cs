using PX.Data;
using PX.Objects.CR;

namespace PX.Objects.SecondChances {
    public class SecondChancesMaint : PXGraph<SecondChancesMaint, SecondChances> {

        [PXViewName("SecondChances")]
        public PXSelect<SecondChances> Document;

        public PXSelect<SecondChances, Where<SecondChances.objectID, Equal<Current<SecondChances.objectID>>>> CurrentDocument;

        public CRAttributeList<SecondChances> Answers;
        
        public SecondChancesMaint() {
        }

    }
}