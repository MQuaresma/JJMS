namespace mvcJJMS.Models{
	public class Utilizador {
		public int UtilizadorID{get;set;}
		public string Email{get;set;}
		public string Password{get;set;}
		public string Nome{get;set;}
		private int FuncionarioID{get;set;}
		private int ClienteID{get;set;}
	}
}