using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace trabalhobd
{

	[Serializable()]
	public class Centro_de_treino
	{
		private String _id_centro_treinos;
		private String _nome;
		private String _data_inauguracao;
		private String _localizacao;

		public String Id_centro_treinos
		{
			get { return _id_centro_treinos; }
			set
			{

				if (value == null | String.IsNullOrEmpty(value))
				{
					throw new Exception("id_centro_treinos field can’t be empty");
				}
				_id_centro_treinos = value;
			}
		}

		public String Nome
		{
			get { return _nome; }
			set
			{
				if (value == null | String.IsNullOrEmpty(value))
				{
					throw new Exception("Nome field can’t be empty");
				}
				_nome = value;
			}
		}

		public String Localizacao
		{
			get { return _localizacao; }
			set { _localizacao = value; }
		}

		public String Data_inauguracao
		{
			get { return _data_inauguracao; }
			set { _data_inauguracao = value; }
		}

		public override String ToString()
		{
			return _id_centro_treinos + "   " + _nome + "   " + _data_inauguracao + "   " + _localizacao;
		}

		public Centro_de_treino() : base()
		{
		}

		public Centro_de_treino(String id_centro_treinos, String nome, String data_inauguracao, String localizacao) : base()
		{
			this.Id_centro_treinos = id_centro_treinos;
			this.Nome = nome;
			this.Data_inauguracao = data_inauguracao;
			this.Localizacao = localizacao;
		}

	}
}
