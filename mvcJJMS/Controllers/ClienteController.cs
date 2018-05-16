using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;

namespace mvcJJMS.Controllers{
    public class ClienteController : Controller{

        private readonly JJMSContext _context;

        public ClienteController(JJMSContext context){
            _context = context;
        }
    }
}