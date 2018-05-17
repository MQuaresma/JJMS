using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System;
using System.Linq;

namespace mvcJJMS.Controllers{
    public class UtilizadorController : Controller{

        private readonly JJMSContext _context;

        public UtilizadorController(JJMSContext context){
            _context = context;
        }
    }
}