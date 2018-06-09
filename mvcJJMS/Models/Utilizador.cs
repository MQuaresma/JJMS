namespace mvcJJMS.Models{
	public abstract class Utilizador {
		public int UtilizadorID{get;set;}
		public string Email{get;set;}
		public byte[] Password{get;set;}
		public string Nome{get;set;}
	}
}