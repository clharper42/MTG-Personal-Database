using System;
using System.Collections.Generic;
namespace MTGRares {
    static class PriceFunctions{
        public static List<Card> Allcardsprice {get; private set;}
        private static void SortByPrice()
        {
            Allcardsprice = new List<Card>(ProgFunctions.Allcards);
            Allcardsprice.Sort((x,y) => {
                double pricex;
                double pricey;
                if(x.Printing.Equals("Foil"))
                {
                    pricex = Convert.ToDouble(x.Prices["usd_foil"]);
                }
                else
                {
                    pricex = Convert.ToDouble(x.Prices["usd"]);
                }
                if(y.Printing.Equals("Foil"))
                {
                    pricey = Convert.ToDouble(y.Prices["usd_foil"]);
                }
                else
                {
                    pricey = Convert.ToDouble(y.Prices["usd"]);
                }
                return pricex.CompareTo(pricey);
            });
            // name amount x price - total, when printing
        }
        public static void CardsByPrice()
        {
            if(Allcardsprice is null)
            {
                SortByPrice();
            }
            Console.Clear();
            double total = 0;
            double calc = 0;
            Console.WriteLine("NAME - SET - PRINTING - ID - AMOUNT - PRICE - TOTAL");
            foreach(Card card in Allcardsprice)
            {
                if(card.Printing.Equals("Foil"))
                {
                    calc = (Convert.ToDouble(card.Prices["usd_foil"]) * card.Amount);
                    total = Math.Round(total + calc, 2);
                    Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + + ProgFunctions.Allcards.IndexOf(card) + " | " + card.Amount + "x" + card.Prices["usd_foil"] + " = " + calc); 
                }
                else
                {
                    calc = (Convert.ToDouble(card.Prices["usd"]) * card.Amount);
                    total = Math.Round(total + calc, 2);
                    Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + + ProgFunctions.Allcards.IndexOf(card) + " | " + card.Amount + "x" + card.Prices["usd"] + " = " + calc); 
                }
            }
            Console.WriteLine("-----");
            Console.WriteLine("Total: " + total);
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine(); 
        }
        public static void Reset()
        {
            Allcardsprice = null;
        }        
    }
}