using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    static class DisplayFunctions{
        public static void CardsByColor()
        {
            string colors;
            bool containsonly;
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Enter Colors (W - White, U - Blue, B - Black, R - Red, G - Green, C - Colorless):");
                colors = Console.ReadLine();
                if(Regex.IsMatch(colors, @"^[WUBRGCwubrgc]+$"))
                {
                    string decs;
                    while(true)
                    {
                        Console.Clear();
                        Console.WriteLine("Are Only These Colors? (Y/N):");
                        decs = Console.ReadLine();
                        if("y".Equals(decs.ToLower()))
                        {
                            containsonly = true;
                            break;
                        }
                        else if("n".Equals(decs.ToLower()))
                        {
                            containsonly = false;
                            break;
                        }                                            
                    }
                    break;
                }
            }

            //'C' for colorless
            bool notinset = false;
            bool nocards = true;
            if(SeprateFunctions.Colorids.Count == 0){
                SeprateFunctions.SepByColorId(false);
            }
            Console.Clear();
            for(int i = 0; i < SeprateFunctions.Colorids.Count; i++)
             {
                if(containsonly){
                    if(SeprateFunctions.Colorids[i].Length == colors.Length)
                    {
                        foreach(char let in colors)
                        {
                            if(!SeprateFunctions.Colorids[i].Contains(Char.ToUpper(let)))
                            {
                                notinset = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        notinset = true;
                    }
                }
                else
                {
                    notinset = true;
                    foreach(char let in colors)
                    {
                        if(SeprateFunctions.Colorids[i].Contains(Char.ToUpper(let)))
                        {
                            notinset = false;
                            break;
                        }
                    }
                }
                if(!notinset)
                {
                    nocards = false;
                    Console.WriteLine("- " + SeprateFunctions.Colorids[i]);
                    Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                    foreach(Card card in SeprateFunctions.SepedCardsByColorId[i])
                    {
                        Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));                            
                    }
                    Console.WriteLine("--------");
                    Console.WriteLine(" ");                                
                }
                notinset = false;             
            }
            if(nocards)
            {
                Console.WriteLine("No Cards In Color(s)");
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine(); 
            
        }
        public static void CardsByText() //change to allow color selection like other cardsby
        {
            string expr;
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Enter Text(Cards Filterd By Color Idenity):");
                expr = Console.ReadLine();
                if(expr.Length > 0)
                {
                    break;
                }
            }

            bool match = false;
            bool printed = false;
            if(SeprateFunctions.Colorids.Count == 0){
                SeprateFunctions.SepByColorId(false);
            }
            Console.Clear();
            for(int i = 0; i < SeprateFunctions.SepedCardsByColorId.Count; i++)
            {
                foreach(Card card in SeprateFunctions.SepedCardsByColorId[i])
                {
                    if(card.Card_faces is null)
                    {
                        if(card.Oracle_text != null)
                        {
                            if(Regex.IsMatch(card.Oracle_text.ToLower(),expr.ToLower()))
                            {
                                if(!match)
                                {
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" - " + SeprateFunctions.Colorids[i]);
                                    Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                                    match = true;
                                    printed = true;
                                }
                                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));
                                Console.WriteLine("Oracle Text: " + card.Oracle_text);
                                Console.WriteLine("-----");
                            }
                        }
                    }
                    else
                    {
                        foreach(Cardface cardface in card.Card_faces)
                        {
                            if(cardface.Oracle_text != null)
                        {
                            if(Regex.IsMatch(cardface.Oracle_text.ToLower(),expr.ToLower()))
                            {
                                if(!match)
                                {
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" - " + SeprateFunctions.Colorids[i]);
                                    Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                                    match = true;
                                    printed = true;
                                }                                    
                                Console.WriteLine(cardface.Name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));
                                Console.WriteLine("Oracle Text: " + cardface.Oracle_text);
                                Console.WriteLine("-----");
                            }
                        }
                        }
                    }                        
                }
                match = false;
            }
            if(!printed)
            {
                Console.WriteLine("No Cards In Search");
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine(); 
        }

        public static void DispalyAllCards() {
            Console.Clear();
            if( ProgFunctions.Allcards.Count == 0)
            {
                Console.WriteLine("No Cards In Database");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();
            }
            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
            foreach(Card card in ProgFunctions.Allcards)
            {
                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();
        }    
    }
}