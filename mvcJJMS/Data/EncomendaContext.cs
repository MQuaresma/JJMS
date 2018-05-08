using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
	public class EncomendaContext : DbContext {
		public EncomendaContext() : base("jjmsdb"){

		}
	}
}