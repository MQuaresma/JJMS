using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Fornecedor {
		public int FornecedorID{get;set;}
		private string nome{get;set;}
		private string morada{get;set;}

		private ICollection<Encomenda> encomendas{get;set;}
	
		public void setNome(string nome){
			this.nome=nome;
		}
		public void setMorada(string morada){
			this.morada=morada;
		}
	}
}