using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhobd
{
    class Pessoa
    { 
        private String _id_pessoa;
        private String _nif;
        private String _fname;
        private String _lname;
        private String _data_nasc;

        public String Id_pessoa
        {
            get { return _id_pessoa; }
            set {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("O id não pode estar vazio.");
                }
                _id_pessoa = value;
        }
        }

        public String Nif
        {
            get { return _nif; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("O nome da claque não pode estar vazio.");
                }
                _nif = value;
            }
        }

        public String Fname
        {
            get { return _fname; }
            set { _fname = value; }
        }

        public String Lname
        {
            get { return _lname; }
            set { _lname = value; }
        }
        public String Data_nasc
        {
            get { return _data_nasc; }
            set { _data_nasc = value; }
        }

        public Pessoa() : base()
        {
        }

        public Pessoa(String id_pessoa, String nif, String fname, String lname, String data_nasc) : base()
        {
            this.Id_pessoa = id_pessoa;
            this.Nif = nif;
            this.Fname = fname;
            this.Lname = lname;
        }

    }
}
