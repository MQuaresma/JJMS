using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;

namespace mvcJJMS.Controllers{
    public class EncomendaController : Controller{

        private readonly JJMSContext _context;

        public EncomendaController(JJMSContext context){
            _context = context;
        }
    }
}