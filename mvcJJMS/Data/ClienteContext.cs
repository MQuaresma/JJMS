using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
	public class ClienteContext : DbContext  {
		public ClienteContext() : base("jjmsdb"){

		}
	}
}