using System.Data.Entity;

namespace mvcJJMS.Models{
	public class Utilizador {
		private int UtilizadorID{get;set;}
		private String Email{get;set;}
		private String Password{get;set;}
		private String Nome{get;set;}
		private int FuncionarioID{get;set;};
		private int ClienteID{get;set;};

		public Funcionario? Funcionario{get;set;} 	//Nullable value
		public Cliente? Cliente{get;set;} 			//Nullable value
	}
}