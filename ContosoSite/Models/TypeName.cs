//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContosoSite.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class TypeName
    {
        public TypeName()
        {
            this.DTs = new HashSet<DT>();
        }
    
        public int TypeNameID { get; set; }
        public string typename1 { get; set; }
        public Nullable<int> parentid { get; set; }
        [JsonIgnore]
        public virtual ICollection<DT> DTs { get; set; }
    }
}
