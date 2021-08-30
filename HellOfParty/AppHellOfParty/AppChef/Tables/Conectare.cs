using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChef.Tables
{
    class Conectare
    {
        private int _codP;
        private int _codU;

        public Conectare(int codU, int codP)
        {
            _codU = codU;
            _codP = codP;
        }

        public int codU
        {
            get
            {
                return _codU;
            }
        }

        public int codP
        {
            get
            {
                return _codP;
            }
        }
    }
}
