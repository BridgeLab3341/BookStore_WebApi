using System;
using System.Collections.Generic;

namespace RepoLayer.Context.Models
{
    public partial class CartTable
    {
        public long CartId { get; set; }
        public long? RegisterId { get; set; }
        public long? ProductId { get; set; }

        public virtual ProductTable Product { get; set; }
        public virtual RegistrationTable Register { get; set; }
    }
}
