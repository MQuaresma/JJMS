using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using mvcJJMS.Data;
using mvcJJMS.Models;

namespace mvcJJMS.Controllers{

    public class CartaoController : Controller{
 		private readonly JJMSContext _context;

		public CartaoController(JJMSContext context){
			_context=context;
		}

        public bool Luhn_check( int num_cart_cred) {
			throw new System.Exception("Not implemented");
		}

        public bool CartaoValido( int numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			throw new System.Exception("Not implemented");
		}
    }
}
