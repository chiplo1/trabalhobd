using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace trabalhobd
{

	[Serializable()]
	public class Estadio
	{
		private String _id_estadio;
		private String _nome;
		private String _data_inauguracao;
		private String _arquiteto;
		private String _lotacao;
		private String _localizacao;

		public String Id_estadio
		{
			get { return _id_estadio; }
			set
			{

				if (value == null | String.IsNullOrEmpty(value))
				{
					throw new Exception("id_estadio field can’t be empty");
					return;
				}
				_id_estadio = value;
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
					return;
				}
				_nome = value;
			}
		}

		public String Data_inauguracao
		{
			get { return _data_inauguracao; }
			set { _data_inauguracao = value; }
		}

		public String Arquiteto
		{
			get { return _arquiteto; }
			set { _arquiteto = value; }
		}

		public String Lotacao
		{
			get { return _lotacao; }
			set { _lotacao = value; }
		}


		public String Localizacao
		{
			get { return _localizacao; }
			set { _localizacao = value; }
		}


		public override String ToString()
		{
			return _id_estadio + "   " + _nome + "   " + _data_inauguracao + "   " + _arquiteto + "   " + _lotacao + "   " + _localizacao;
		}

		public Estadio() : base()
		{
		}

		public Estadio(String id_estadios, String nome, String data_inauguracao, String arquiteto, String lotacao,  String localizacao) : base()
		{
			this.Id_estadio = id_estadios;
			this.Nome = nome;
			this.Data_inauguracao = data_inauguracao;
			this.Arquiteto = arquiteto;
			this.Lotacao = lotacao;
			this.Localizacao = localizacao;
		}

	}
}
