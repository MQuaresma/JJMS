using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
	public class CartaoCreditoContext : DbContext {
		public CartaoCreditoContext() : base("jjmsdb"){

		}
	}
}