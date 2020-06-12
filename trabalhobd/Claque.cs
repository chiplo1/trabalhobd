using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhobd
{
    class Claque
    {
        private String _claqueID;
        private String _nome;
        private String _loc;
        private String _bancada;

        public String ClaqueID
        {
            get { return _claqueID; }
            set { _claqueID = value; }
        }

        public String Nome
        {
            get { return _nome; }
            set {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("O nome da claque não pode estar vazio.");
                }
                _nome = value; 
            }
        }

        public String Loc
        {
            get { return _loc; }
            set { _loc = value; }
        }

        public String Bancada
        {
            get { return _bancada; }
            set { _bancada = value; }
        }

        public Claque() : base()
        {
        }

        public Claque(String nome) : base()
        {
            this.Nome = nome;
        }

        public Claque(String nome, String loc, String bancada) : base()
        {
            this.Nome = nome;
            this.Loc = loc;
            this.Bancada = bancada;
        }

    }

}
