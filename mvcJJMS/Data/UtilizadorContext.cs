using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
	public class UtilizadorContext : DbContext {
		public UtilizadorContext() : base("jjmsdb"){

		}
	}
}