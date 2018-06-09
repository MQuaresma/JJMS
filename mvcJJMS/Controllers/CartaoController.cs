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

        public bool Luhn_check( int numCartCredito) {
			string cc_number = numCartCredito.ToString();
			int checksum = 0;
			int j = 1;
			int i = cc_number.Length-1;
			int calc;

			while(i>=0){
				calc = Convert.ToInt32(cc_number.Substring(i,1))*j;
				
				if(calc>9){
					checksum++;
					calc-=10;
				}
				checksum+=calc;
				j = (j==1) ? 2 : 1;
				i--;
			}

			if(checksum % 10 != 0) return false;
			return true;
		}

        public bool CartaoValido( int numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			if(Luhn_check(numCartCredito)==false) return false;
			
			string cvvstring = cvv.ToString();
			if(cvvstring.Length!=3 || cvvstring.Length!=4) return false;

			DateTime data = DateTime.Today;
			int anoatual = data.Year;
			int mesatual = data.Month;

			if(ano<anoatual) return false;
			if(ano==anoatual && mes<mesatual) return false;
			return true;
		}
    }
}
