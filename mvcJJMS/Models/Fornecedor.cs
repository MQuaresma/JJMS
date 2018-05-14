using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Fornecedor {
		private int FornecedorID{get;set;}
		private string nome{get;set;}
		private string morada{get;set;}

		private ICollection<Encomenda> encomendas{get;set;}
	}
}