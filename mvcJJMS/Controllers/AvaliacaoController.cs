using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;

namespace mvcJJMS.Controllers{
    public class AvaliacaoController : Controller{

        private readonly JJMSContext _context;

        public AvaliacaoController(JJMSContext context){
            _context = context;
        }
    }
}