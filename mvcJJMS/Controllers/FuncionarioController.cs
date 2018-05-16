using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;

namespace mvcJJMS.Controllers{
    public class FuncionarioController : Controller{

        private readonly JJMSContext _context;

        public FuncionarioController(JJMSContext context){
            _context = context;
        }
    }
}