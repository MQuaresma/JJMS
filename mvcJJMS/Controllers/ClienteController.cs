using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System;
using System.Linq;

namespace mvcJJMS.Controllers{
    public class ClienteController : Controller{

        private readonly JJMSContext _context;

        public ClienteController(JJMSContext context){
            _context = context;
        }
    }
}