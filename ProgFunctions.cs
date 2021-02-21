using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    static class ProgFunctions {
        public static List<Card> Allcards {get; set;}
        private static void RemoveCard (int index)
        {
            //keep track of when card is removed so sep functions can run again, check if amount of card is one or more
            //create array of the list<string> and clear them and add them to the null checks
            //cardprice seprate style from others
            //enter id, search for id options
            Console.Clear();
            if(Allcards[index].Amount > 1)
            {
                Allcards[index].Amount = Allcards[index].Amount - 1;
                Console.WriteLine(Allcards[index].Special_name + " Amount Is Now " + Allcards[index].Amount);
            }
            else
            {
                Console.WriteLine(Allcards[index].Special_name + " Removed From Database");
                Console.WriteLine("IDs Above " + index + " Are Now One Less Then Before");
                Allcards.RemoveAt(index);
                //do this in a better way?
                SeprateFunctions.Reset();
                PriceFunctions.Reset(); 
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();           
        }
        public static void ChooseFeature(int choice)
        {
            string selection;
            if(choice == 1)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Search By Card Name or Card Database ID:");
                    Console.WriteLine("1 - Card Name");
                    Console.WriteLine("2 - Card Database ID");
                    Console.WriteLine("3 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-3]$")) 
                    {
                        if(Convert.ToInt32(selection) == 1)
                        {
                            string decs;
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Exact search? (Y/N)");
                                decs = Console.ReadLine().ToLower();
                                Console.Clear();
                                if("y".Equals(decs))
                                {
                                    Console.WriteLine("Enter Card Name:");
                                    CardSearchFunctions.CardSearch(Console.ReadLine(),true);
                                    break;
                                }
                                else if("n".Equals(decs))
                                {
                                    Console.WriteLine("Enter Card Name:");
                                    CardSearchFunctions.CardSearch(Console.ReadLine(),false);
                                    break;
                                }
                            }
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter ID:");
                            CardSearchFunctions.CardSearchByIndex(Convert.ToInt32(Console.ReadLine()));
                        }
                        else if(Convert.ToInt32(selection) == 3)
                        {
                            break;
                        }                              
                    }                 
                }
            }
            else if(choice == 2)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Enter Text(Cards Filterd By Color Idenity):");
                    selection = Console.ReadLine();
                    if(selection.Length > 0)
                    {
                        break;
                    }
                }
                DisplayFunctions.CardsByText(selection);   
            }
            else if(choice == 3)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Seprate All Cards By Color Idenity or Search For Cards By Color Idenity:");
                    Console.WriteLine("1 - Sperate Cards");
                    Console.WriteLine("2 - Search By Color");
                    Console.WriteLine("3 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-3]$"))
                    {
                        if(Convert.ToInt32(selection) == 1)
                        {
                            SeprateFunctions.SepByColorId(true);
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter Colors (W - White, U - Blue, B - Black, R - Red, G - Green, C - Colorless):");
                                selection = Console.ReadLine();
                                if(Regex.IsMatch(selection, @"^[WUBRGCwubrgc]+$"))
                                {
                                    string decs;
                                    while(true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Are Only These Colors? (Y/N):");
                                        decs = Console.ReadLine();
                                        if("y".Equals(decs.ToLower()))
                                        {
                                            DisplayFunctions.CardsByColor(selection,true);
                                            break;
                                        }
                                        else if("n".Equals(decs.ToLower()))
                                        {
                                            DisplayFunctions.CardsByColor(selection,false);
                                            break;
                                        }                                            
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }                     
                }
            }                
            else if(choice == 4)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Seprate Cards By Type/Subtype or Search By Type/Subtype:");
                    Console.WriteLine("1 - Seprate By Type");
                    Console.WriteLine("2 - Seprate By Subtype");
                    Console.WriteLine("3 - Search By Type");
                    Console.WriteLine("4 - Searh By Subtype");
                    Console.WriteLine("5 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-5]$"))
                    {
                        if(SeprateFunctions.Types is null) //SepedCardsByType
                        {
                            SeprateFunctions.SepByTypeLine();
                        }
                        if(Convert.ToInt32(selection) == 1)
                        {
                            FilterFunctions.DisplayByFilter(SeprateFunctions.SepedCardsByType,SeprateFunctions.Types);
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            FilterFunctions.DisplayByFilter(SeprateFunctions.SepedCardsBySubtype,SeprateFunctions.Subtypes);
                        }
                        else if(Convert.ToInt32(selection) == 3 || Convert.ToInt32(selection) == 4)
                        {
                            bool exactsearch;
                            bool usecolors;
                            bool colorsexact = false;
                            string expr;
                            string colors = "";
                            string geninput = "";
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Exact Search? (Y/N):");
                                geninput = Console.ReadLine();
                                if("y".Equals(geninput.ToLower()))
                                {
                                    exactsearch = true;
                                    break;
                                }
                                else if("n".Equals(geninput.ToLower()))
                                {
                                    exactsearch = false;
                                    break;
                                }
                            }
                            Console.Clear();
                            if(Convert.ToInt32(selection) == 3)
                            {
                                Console.WriteLine("Enter Type:");
                            }
                            else
                            {
                                Console.WriteLine("Enter Subtype:");
                            }
                            expr = Console.ReadLine();
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Use Color Idenity To Filter Serach? (Y/N):");
                                geninput = Console.ReadLine();
                                if("y".Equals(geninput.ToLower()))
                                {
                                    usecolors = true;
                                    break;
                                }
                                else if("n".Equals(geninput.ToLower()))
                                {
                                    usecolors = false;
                                    break;
                                }                                                                       
                            }
                            if(usecolors)
                            {
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter Colors (W - White, U - Blue, B - Black, R - Red, G - Green, C - Colorless):");
                                    colors = Console.ReadLine();
                                    if(Regex.IsMatch(colors, @"^[WUBRGCwubrgc]+$"))
                                    {
                                        break;
                                    }                                        
                                }   
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Are Only These Colors? (Y/N):");
                                    geninput = Console.ReadLine();
                                    if("y".Equals(geninput.ToLower()))
                                    {
                                        colorsexact = true;
                                        break;
                                    }
                                    else if("n".Equals(geninput.ToLower()))
                                    {
                                        colorsexact = false;
                                        break;
                                    }                                        
                                }
                            }
                            if(Convert.ToInt32(selection) == 3)
                            {
                                FilterFunctions.CardsByFilter(SeprateFunctions.SepedCardsByType,SeprateFunctions.Types,expr,colors,usecolors,colorsexact,exactsearch);
                            }
                            else
                            {
                                FilterFunctions.CardsByFilter(SeprateFunctions.SepedCardsBySubtype,SeprateFunctions.Subtypes,expr,colors,usecolors,colorsexact,exactsearch);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else if(choice == 5)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Seprate Cards By CMC or Search By CMC:");
                    Console.WriteLine("1 - Seprate By CMC");
                    Console.WriteLine("2 - Search By CMC");
                    Console.WriteLine("3 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-3]$"))
                    {
                        if(SeprateFunctions.Cmcs is null) //SepedCardsByCmcs
                        {
                            SeprateFunctions.SepByCMC();
                        }
                        if(Convert.ToInt32(selection) == 1)
                        {
                            FilterFunctions.DisplayByFilter(SeprateFunctions.SepedCardsByCmcs,SeprateFunctions.Cmcs);
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            bool usecolors;
                            bool colorsexact = false;
                            string expr;
                            string colors = "";
                            string geninput = "";
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter CMC:");
                                expr = Console.ReadLine();
                                if(Regex.IsMatch(expr, @"^[0-9]+$"))
                                {
                                    break;
                                }  
                            }
                            
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Use Color Idenity To Filter Serach? (Y/N):");
                                geninput = Console.ReadLine();
                                if("y".Equals(geninput.ToLower()))
                                {
                                    usecolors = true;
                                    break;
                                }
                                else if("n".Equals(geninput.ToLower()))
                                {
                                    usecolors = false;
                                    break;
                                }                                                                       
                            }
                            if(usecolors)
                            {
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter Colors (W - White, U - Blue, B - Black, R - Red, G - Green, C - Colorless):");
                                    colors = Console.ReadLine();
                                    if(Regex.IsMatch(colors, @"^[WUBRGCwubrgc]+$"))
                                    {
                                        break;
                                    }                                        
                                }   
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Are Only These Colors? (Y/N):");
                                    geninput = Console.ReadLine();
                                    if("y".Equals(geninput.ToLower()))
                                    {
                                        colorsexact = true;
                                        break;
                                    }
                                    else if("n".Equals(geninput.ToLower()))
                                    {
                                        colorsexact = false;
                                        break;
                                    }                                        
                                }
                            }
                            FilterFunctions.CardsByFilter(SeprateFunctions.SepedCardsByCmcs,SeprateFunctions.Cmcs,expr,colors,usecolors,colorsexact,true);                              
                        }
                        else
                        {
                            break;
                        }
                    }                    
                }
            }
            else if(choice == 6)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Seprate Cards By Keyword or Search By Keyword:");
                    Console.WriteLine("1 - Seprate By Keyword");
                    Console.WriteLine("2 - Search By Keyword");
                    Console.WriteLine("3 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-3]$"))
                    {
                        if(SeprateFunctions.Keywords is null) //SepedCardsByKeywords
                        {
                            SeprateFunctions.SepByKeyword();
                        }
                        if(Convert.ToInt32(selection) == 1)
                        {
                            FilterFunctions.DisplayByFilter(SeprateFunctions.SepedCardsByKeywords,SeprateFunctions.Keywords);
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            bool exactsearch;
                            bool usecolors;
                            bool colorsexact = false;
                            string expr;
                            string colors = "";
                            string geninput = "";
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Exact Search? (Y/N):");
                                geninput = Console.ReadLine();
                                if("y".Equals(geninput.ToLower()))
                                {
                                    exactsearch = true;
                                    break;
                                }
                                else if("n".Equals(geninput.ToLower()))
                                {
                                    exactsearch = false;
                                    break;
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("Enter Keyword: ");
                            expr = Console.ReadLine();
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Use Color Idenity To Filter Serach? (Y/N):");
                                geninput = Console.ReadLine();
                                if("y".Equals(geninput.ToLower()))
                                {
                                    usecolors = true;
                                    break;
                                }
                                else if("n".Equals(geninput.ToLower()))
                                {
                                    usecolors = false;
                                    break;
                                }                                                                       
                            }
                            if(usecolors)
                            {
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter Colors (W - White, U - Blue, B - Black, R - Red, G - Green, C - Colorless):");
                                    colors = Console.ReadLine();
                                    if(Regex.IsMatch(colors, @"^[WUBRGCwubrgc]+$"))
                                    {
                                        break;
                                    }                                        
                                }   
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Are Only These Colors? (Y/N):");
                                    geninput = Console.ReadLine();
                                    if("y".Equals(geninput.ToLower()))
                                    {
                                        colorsexact = true;
                                        break;
                                    }
                                    else if("n".Equals(geninput.ToLower()))
                                    {
                                        colorsexact = false;
                                        break;
                                    }                                        
                                }
                            }
                            FilterFunctions.CardsByFilter(SeprateFunctions.SepedCardsByKeywords,SeprateFunctions.Keywords,expr,colors,usecolors,colorsexact,exactsearch);                                                              
                        }
                        else
                        {
                            break;
                        }
                    }                                               
                }
            }
            else if(choice == 7)
            {
                PriceFunctions.CardsByPrice();
            }
            else if(choice == 8)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Enter Card Datbase ID or Search For Card To Get ID:");
                    Console.WriteLine("1 - Enter ID");
                    Console.WriteLine("2 - Search");
                    Console.WriteLine("3 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-3]$"))
                    {
                        if(Convert.ToInt32(selection) == 1)
                        {
                            string index;
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine(" - Enter B To Back Out - ");
                                Console.WriteLine("Enter ID: ");  
                                index = Console.ReadLine();
                                if(index.ToLower().Equals("b"))
                                {
                                    break;
                                }
                                else if(Regex.IsMatch(index, @"^[0-9]+$") && Convert.ToInt32(index) >= 0 && Convert.ToInt32(index) < Allcards.Count)
                                {
                                    RemoveCard(Convert.ToInt32(index));
                                    break;
                                }
                            }
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            string decs;
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine("Exact search? (Y/N)");
                                decs = Console.ReadLine().ToLower();
                                Console.Clear();
                                if("y".Equals(decs))
                                {
                                    Console.WriteLine("Enter Card Name:");
                                    CardSearchFunctions.CardSearch(Console.ReadLine(),true);
                                    break;
                                }
                                else if("n".Equals(decs))
                                {
                                    Console.WriteLine("Enter Card Name:");
                                    CardSearchFunctions.CardSearch(Console.ReadLine(),false);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

}