using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhobd
{
    class Socio
    {

        private String _socioID;
        private String _nome;
        private String _dataInsc;
        private String _dataNasc; 
        private String _claque;

        public String SocioID
        {
            get { return _socioID; }
            set { _socioID = value; }
        }

        public String Nome
        {
            get { return _nome; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("O nome da claque não pode estar vazio.");
                    return;
                }
                _nome = value;
            }
        }

        public String DataInsc
        {
            get { return _dataInsc; }
            set { _dataInsc = value; }
        }

        public String DataNasc
        {
            get { return _dataNasc; }
            set { _dataNasc = value; }
        }
        public String Claque
        {
            get { return _claque; }
            set { _claque = value; }
        }

        public Socio() : base()
        {
        }

        public Socio(String nome) : base()
        {
            this.Nome = nome;
        }

        public Socio(String nome, String dataInsc, String dataNasc, String claque) : base()
        {
            this.Nome = nome;
            this.DataInsc = dataInsc;
            this.DataNasc = dataNasc;
            this.Claque = claque;
        }

    }
}
