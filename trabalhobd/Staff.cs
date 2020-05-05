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
		private String _id_pessoa;
		private String _tipo;
		private String _data_termino;

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

		public String Id_pessoa
		{
			get { return _id_pessoa; }
			set
			{
				if (value == null | String.IsNullOrEmpty(value))
				{
					throw new Exception("Pessoa ID field can’t be empty");
					return;
				}
				_id_pessoa = value;
			}
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

		public override String ToString()
		{
			return Id_staff + "   " + Id_pessoa + "   " + Tipo + "   " + Data_termino;
		}

		public Staff() : base()
		{
		}

		public Staff(String id_staff, String id_pessoa, String tipo, String data_termino) : base()
		{
			this.Id_staff = id_staff;
			this.Id_pessoa = id_pessoa;
			this.Tipo = tipo;
			this.Data_termino = data_termino;
		}

	}
}
