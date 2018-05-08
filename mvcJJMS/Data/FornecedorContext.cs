using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
	public class FornecedorContext : DbContext {
		public FornecedorContext() : base("jjmsdb"){

		}
	}
}