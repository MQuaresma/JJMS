using System.Collections.Generic;

namespace mvcJJMS.Models{
	/// <summary>
	/// Represents a single user whether a Client or an Employee
	/// </summary>
	public abstract class Utilizador {
		public int UtilizadorID{get;set;}
		public string Email{get;set;}
		public byte[] Password{get;set;}
		public string Nome{get;set;}
	}
}