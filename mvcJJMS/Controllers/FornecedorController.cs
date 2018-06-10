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

		/// <summary>
		/// obtain the id of Fornecedor with nomeForn
		/// </summary>
		/// <param name="nomeForn"></param>
		/// <returns>returns id of Fornecedor or -1 if not exists</returns>
        public int IdForn( string nomeForn) {
			bool found = false;
			int id = -1;
			string nome;
			foreach (Fornecedor f in _context.Fornecedores){
				if (!found){
					nome = f.nome;
					if (nomeForn.Equals(nome)){
						found = true;
						id = f.FornecedorID;
					}
				}
			}
			return id;
		}

		/// <summary>
		/// obtains adress of Fornecedor with the id idForn
		/// </summary>
		/// <param name="idForn"></param>
		/// <returns>returns adress of Fornecedor</returns>
        public string GetMoradaForn(int idForn) {
			Fornecedor forn = _context.Fornecedores.Find(idForn);
			return forn.morada;
		}
    }
}
