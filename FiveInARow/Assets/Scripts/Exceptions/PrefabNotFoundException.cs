using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    [Serializable]
    public class PrefabNotFoundException:Exception
    {
        public PrefabNotFoundException(string msg) : base(msg) { }
    }
}
