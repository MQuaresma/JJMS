using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
	public class FuncionarioContext : DbContext  {
		public FuncionarioContext() : base("jjmsdb"){

		}
	}
}