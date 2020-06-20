using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify.Data
{
    public class ICar : GenericData
    {

        public ICar(IDataReader data)
        {
            Load(data);
        }

        public ICar()
        {

        }
    }
}
