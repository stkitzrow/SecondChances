using System;
using PX.Data;
using PX.Data.BQL;

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

    }
}