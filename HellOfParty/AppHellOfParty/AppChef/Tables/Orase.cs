using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChef.Tables
{
    class Orase
    {
        private int _codO;
        private String _numeOras;
        public Orase(int cod, String n)
        {
            _codO = cod;
            _numeOras = n;
        }

        public int codO
        {
            get
            {
                return _codO;
            }
        }

        public String numeOras
        {
            get
            {
                return _numeOras;
            }
        }
    }
}
