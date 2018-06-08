using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using mvcJJMS.Data;
using mvcJJMS.Models;

namespace mvcJJMS.Controllers{
    public class FuncionarioController : Controller{
        private readonly JJMSContext _context;

		public FuncionarioController(JJMSContext context){
			_context=context;
		}
        
        public int DelegarFuncionario( int idEncomenda,  string destino) {
			throw new System.Exception("Not implemented");
		}

        public void EnviarEmail( int idFunc,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public int GetZona( string morada) {
			throw new System.Exception("Not implemented");
		}
    }
}
