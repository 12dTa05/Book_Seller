using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class CommonService : ICommonService
    {
        public string GenerateRandomCode(int number)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#!";

            Random random = new Random();

            var blindCharacters = Enumerable.Range(0, number).Select(x => characters[random.Next(0, characters.Length)]);

            return new string(blindCharacters.ToArray());
        }
    }
}