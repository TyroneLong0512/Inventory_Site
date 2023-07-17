using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Structs
{
    public struct PropertyStruct
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public bool ShouldRender { get; set; }
    }
}
