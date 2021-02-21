using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    static class FilterFunctions {
        public static void DisplayByFilter(List<List<List<Card>>> sepedcardsbyfilter, List<List<String>> filter)
        {
            Console.Clear();
            for(int i = 0; i < sepedcardsbyfilter.Count; i++)
            {
                if(sepedcardsbyfilter[i].Count != 0)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("--" + " " + SeprateFunctions.Colorids[i]);
                    for(int j = 0; j < sepedcardsbyfilter[i].Count; j++)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("-" + " " + filter[i][j]);
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        foreach(Card card in sepedcardsbyfilter[i][j])
                        {
                            Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));                            
                        }
                    }
                }
            }
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();                 
                        
        }
        public static void CardsByFilter(List<List<List<Card>>> sepedcardsbyfilter, List<List<String>> filter, string expr, string colors, bool usecolors, bool colorsexact, bool exprexact)
        {
            Console.Clear();
            bool printed = false;
            for(int i = 0; i < sepedcardsbyfilter.Count; i++) //color
            {
                bool correctcolor = true;
                bool correctcmc = true;
                bool consolecolor = true;
                if(usecolors)
                {
                    if(colorsexact)
                    {
                        if(colors.Length == SeprateFunctions.Colorids[i].Length)
                        {
                            foreach(char color in colors)
                            {
                                if(!SeprateFunctions.Colorids[i].Contains(char.ToUpper(color)))
                                {
                                    correctcolor = false;
                                    break;
                                }
                            }                               
                        }
                        else
                        {
                            correctcolor = false;
                        }
                    }
                    else
                    {
                        correctcolor = false;
                        foreach(char color in colors)
                        {
                            if(SeprateFunctions.Colorids[i].Contains(char.ToUpper(color)))
                            {
                                correctcolor = true;
                                break;
                            }
                        }
                    }                            
                }
                if(correctcolor)
                {
                    consolecolor = true;
                    for(int j = 0; j < sepedcardsbyfilter[i].Count; j++) //filter
                    {
                        correctcmc = false;
                        if(exprexact)
                        {
                            if(filter[i][j].ToLower().Equals(expr.ToLower()))
                            {
                                correctcmc = true;
                            }
                        }
                        else
                        {
                            if(Regex.IsMatch(filter[i][j].ToLower(),expr.ToLower()))
                            {
                                correctcmc = true;
                            }
                        }
                        if(correctcmc)
                        {
                            if(consolecolor)
                            {
                                Console.WriteLine(" ");
                                Console.WriteLine(" ");
                                Console.WriteLine("--" + " " + SeprateFunctions.Colorids[i]);
                                consolecolor = false;
                                printed = true;
                            }
                            Console.WriteLine(" ");
                            Console.WriteLine("-" + " " + filter[i][j]);
                            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                            foreach(Card card in sepedcardsbyfilter[i][j])
                            {
                                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));                          
                            }                                
                        }
                    }
                }
            }
            if(!printed)
            {
                Console.WriteLine("No Cards In Search");
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine(); 
        }
    }
}