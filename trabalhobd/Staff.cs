using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace trabalhobd
{

	[Serializable()]
	public class Staff
	{
		private String _id_staff;
		private String _nome;
		private String _tipo;
		private String _data_termino;
		private String _data_nascimento;

		public String Id_staff
		{
			get { return _id_staff; }
			set {
					
					if (value == null | String.IsNullOrEmpty(value))
					{
						throw new Exception("ID staff field can’t be empty");
						return;
					}
					_id_staff = value;
			}
		}

		public String Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		public String Tipo
		{
			get { return _tipo; }
			set { _tipo = value; }
		}

		public String Data_termino
		{
			get { return _data_termino; }
			set { _data_termino = value; }
		}

		public String Data_nascimento
		{
			get { return _data_nascimento; }
			set { _data_nascimento = value; }
		}

		public override String ToString()
		{
			return Id_staff + "   " + Nome + "   " + Tipo + "   " + Data_termino;
		}

		public Staff() : base()
		{
		}

		public Staff(String id_staff, String nome, String tipo, String data_termino, String data_nascimento) : base()
		{
			this.Id_staff = id_staff;
			this.Nome = nome;
			this.Tipo = tipo;
			this.Data_termino = data_termino;
			this.Data_nascimento = data_nascimento;
		}

	}
}
