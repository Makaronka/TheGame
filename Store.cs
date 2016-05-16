using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    abstract class Store
    {
        public abstract Container Inventory { get; }
    }
}
