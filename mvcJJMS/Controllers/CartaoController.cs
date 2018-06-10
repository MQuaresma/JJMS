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

		/// <summary>
		/// checks if a number of credit card respects the luhn algoritm
		/// </summary>
		/// <param name="numCartCredito"></param>
		/// <returns>true if is valid, false if not</returns>
        public bool Luhn_check( long numCartCredito) {
			string cc_number = numCartCredito.ToString();
			long checksum = 0;
			int j = 1;
			int i = cc_number.Length-1;
			long calc;

			while(i>=0){
				calc = Convert.ToInt64(cc_number.Substring(i,1))*j;
				
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

		/// <summary>
		/// Verify is CartaoCredito is valid, aplies luhn algoritm, checks validate and if cvv have 3 or 4 of length
		/// </summary>
		/// <param name="numCartCredito"></param>
		/// <param name="mes"></param>
		/// <param name="ano"></param>
		/// <param name="cvv"></param>
		/// <param name="pais"></param>
		/// <returns>true if CartaoCredito is valid, false if not</returns>
        public bool CartaoValido( long numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			if(Luhn_check(numCartCredito)==false) return false;
			
			string cvvstring = cvv.ToString();
			if(cvvstring.Length!=3 && cvvstring.Length!=4) return false;

			DateTime data = DateTime.Today;
			int anoatual = data.Year;
			int mesatual = data.Month;

			if(ano<anoatual) return false;
			if(ano==anoatual && mes<mesatual) return false;
			return true;
		}
    }
}
