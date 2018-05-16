using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;

namespace mvcJJMS.Controllers{
    public class CartaoCreditoController : Controller{

        private readonly JJMSContext _context;

        public CartaoCreditoController(JJMSContext context){
            _context = context;
        }
    }
}