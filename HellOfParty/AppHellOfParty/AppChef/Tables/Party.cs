using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChef.Tables
{
    class Party
    {
        private int _codP;
        private String _nume;
        private String _club;
        private int _locuri;
        private int _codO;

        public Party(int codP, String num, String club, int loc, int codO)
        {
            _codP = codP;
            _nume = num;
            _club = club;
            _locuri = loc;
            _codO = codO;
        }

        public int codP
        {
            get
            {
                return _codP;
            }
        }

        public String nume
        {
            get
            {
                return _nume;
            }
        }

        public String club
        {
            get
            {
                return _club;
            }
        }
        public int locuri
        {
            get
            {
                return _locuri;
            }
        }

        public int codO
        {
            get
            {
                return _codO;
            }
        }
    }
}
