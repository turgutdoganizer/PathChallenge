﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathProjectChallenge.Data.Mapping
{
    public partial class NopEntityFieldDescriptor
    {
        public string Name { get; set; }
        public bool IsIdentity { get; set; }
        public bool? IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsUnique { get; set; }
        public int? Precision { get; set; }
        public int? Size { get; set; }
        public Type Type { get; set; }
    }
}
