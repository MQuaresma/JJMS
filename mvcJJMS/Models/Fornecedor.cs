using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Fornecedor {
		public int FornecedorID{get;set;}
		public string nome{get;set;}
		public string morada{get;set;}

		public ICollection<Encomenda> Encomendas{get;set;}
	
		public void setNome(string nome){
			this.nome=nome;
		}
		public void setMorada(string morada){
			this.morada=morada;
		}

		public string getNome(){
			return this.nome;
		}
	}
}