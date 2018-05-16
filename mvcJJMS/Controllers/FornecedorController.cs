using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;

namespace mvcJJMS.Controllers{
    public class FornecedorController : Controller{

        private readonly JJMSContext _context;

        public FornecedorController(JJMSContext context){
            _context = context;
        }
    }
}