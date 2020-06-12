using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhobd
{
    class Jogador
    {
        private String _jogadorID;
        private String _pessoaID;  //acrescentei aqui
        private String _pos;
        private String _nome;
        private String _dataterm;
        private String _equipa;
        private String _liga;
        private String _datanasc;

        public String JogadorID
        {
            get { return _jogadorID; }
            set { _jogadorID = value; }
        }

        public String PessoaID
        {
            get { return _pessoaID; }
            set { _pessoaID = value; }
        }

        public String Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public String Nome
        {
            get { return _nome; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("O nome do jogador não pode estar vazio.");
                }
                _nome = value;
            }
        }

        public String Dataterm
        {
            get { return _dataterm; }
            set { _dataterm = value; }
        }

        public String Equipa
        {
            get { return _equipa; }
            set { _equipa = value; }
        }

        public String Liga
        {
            get { return _liga; }
            set { _liga = value; }
        }

        public String Datanasc
        {
            get { return _datanasc; }
            set { _datanasc = value; }
        }

        public Jogador() : base()
        {
        }

        public Jogador(String nome) : base()
        {
            this.Nome = nome;
        }

        public Jogador(String nome, String pos, String dataterm, String equipa, String liga, String datanasc) : base()
        {
            this.Nome = nome;
            this.Pos = pos;
            this.Dataterm = dataterm;
            this.Equipa = equipa;
            this.Liga = liga;
            this.Datanasc = datanasc;
        }

    }

}
