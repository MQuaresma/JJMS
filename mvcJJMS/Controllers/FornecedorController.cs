using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Controllers{
    public class FornecedorController : Controller{

        private JJMSContext _context;

        public FornecedorController(JJMSContext context){
            _context = context;
        }

        public int registerFornecedor(Fornecedor nFornecedor){
            _context.Fornecedores.Add(nFornecedor);
            return 0;
        }
    }
}