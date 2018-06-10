using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Fornecedor {
		public int FornecedorID{get;set;}
		public string nome{get;set;}
		public string morada{get;set;}

		public ICollection<Encomenda> Encomendas{get;set;}

		/// <summary>
		/// set the name of Fornecedor(provider)
		/// </summary>
		/// <param name="nome"></param>	
		public void setNome(string nome){
			this.nome=nome;
		}

		/// <summary>
		/// set the adress of Fornecedor(provider)
		/// </summary>
		/// <param name="morada"></param>
		public void setMorada(string morada){
			this.morada=morada;
		}

		/// <summary>
		/// obtain the name of Fornecedor(provider)
		/// </summary>
		/// <returns>return the name</returns>
		public string getNome(){
			return this.nome;
		}
	}
}