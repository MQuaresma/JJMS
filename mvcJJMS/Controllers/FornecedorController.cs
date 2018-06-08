using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using mvcJJMS.Data;
using mvcJJMS.Models;

namespace mvcJJMS.Controllers{
    public class FornecedorController : Controller{
        private readonly JJMSContext _context;

		public FornecedorController(JJMSContext context){
			_context=context;
		}

        public int IdForn( string nomeForn) {
			throw new System.Exception("Not implemented");
		}

        public string GetMoradaForn(int idForn) {
			Fornecedor forn = _context.Fornecedores.Find(idForn);
			return forn.morada;
		}
    }
}
