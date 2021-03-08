using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    static class ProgFunctions {
        public static List<Card> Allcards {get; set;}
        private static void RemoveCard ()
        {
            string indexstr;
            int index;
            while(true)
            {
                Console.Clear();
                Console.WriteLine(" - Enter B To Back Out - ");
                Console.WriteLine("Enter ID: ");  
                indexstr = Console.ReadLine();
                if(indexstr.ToLower().Equals("b"))
                {
                    return;
                }
                else if(Regex.IsMatch(indexstr, @"^[0-9]+$") && Convert.ToInt32(indexstr) >= 0 && Convert.ToInt32(indexstr) < Allcards.Count)
                {
                    index = Convert.ToInt32(indexstr);
                    break;
                }
            }

            Console.Clear();
            bool inlist = false;
            string selection = "";
            List<CardList> AffectedLists = new List<CardList>();
            if(CardListFunctions.AllCardLists.Count > 0)
            {
                foreach(CardList cl in CardListFunctions.AllCardLists)
                {
                    if(cl.TheList.Contains(Allcards[index]) && cl.NumOfCard[cl.TheList.IndexOf(Allcards[index])] == Allcards[index].Amount)
                    {
                        AffectedLists.Add(cl);
                        Console.WriteLine("- " + cl.Name);
                        inlist = true;
                    }
                }
            }
            if(inlist)
            {
                while(true)
                {
                    Console.WriteLine("Card Is In The Above List(s) And Will Be Affected. Do You Still Wish To Remove? (Y/N): ");
                    selection = Console.ReadLine();
                    if(selection.ToLower().Equals("y"))
                    {
                        Console.Clear();
                        break;
                    }
                    else if(selection.ToLower().Equals("n"))
                    {
                        return;
                    }
                    Console.Clear();
                }

                foreach(CardList cl in AffectedLists)
                {
                    cl.RemoveByCard(Allcards[index]);
                }

                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Proceed:");
                Console.ReadLine();  
            }

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
                            Console.Clear();
                            Console.WriteLine("Enter Card Name:");
                            CardSearchFunctions.CardSearch(Console.ReadLine());
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
                DisplayFunctions.CardsByText();   
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
                            DisplayFunctions.CardsByColor();
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
                        if(SeprateFunctions.Types.Count == 0) //SepedCardsByType
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
                        if(SeprateFunctions.Cmcs.Count == 0) //SepedCardsByCmcs
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
                        if(SeprateFunctions.Keywords.Count == 0) //SepedCardsByKeywords
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
                    Console.WriteLine("Select List Option:");
                    Console.WriteLine("1 - Create List");
                    Console.WriteLine("2 - Dispaly List");
                    Console.WriteLine("3 - Add To List");
                    Console.WriteLine("4 - Remove From List");
                    Console.WriteLine("5 - Remove List");
                    Console.WriteLine("6 - Print List To File");
                    Console.WriteLine("7 - Load List From File");
                    Console.WriteLine("8 - Exit");
                    selection = Console.ReadLine();
                    if(Regex.IsMatch(selection,@"^[1-8]$"))
                    {
                        if(Convert.ToInt32(selection) == 1)
                        {
                            CardListFunctions.CreateCardList();
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            CardListFunctions.DisplayCardList();
                        }
                        else if(Convert.ToInt32(selection) == 3)
                        {
                            CardListFunctions.AddToCardList();
                        }
                        else if(Convert.ToInt32(selection) == 4)
                        {
                            CardListFunctions.RemoveFromCardList();
                        }
                        else if(Convert.ToInt32(selection) == 5)
                        {
                            CardListFunctions.RemoveCardList();
                        }
                        else if(Convert.ToInt32(selection) == 6)
                        {
                            CardListFunctions.PrintCardListToFile();
                        }
                        else if(Convert.ToInt32(selection) == 7)
                        {
                            CardListFunctions.LoadListFromFile();
                        }
                        else
                        {
                            break;
                        }
                    }

                }
            }
            else if(choice == 9)
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
                            RemoveCard();
                        }
                        else if(Convert.ToInt32(selection) == 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Card Name:");
                            CardSearchFunctions.CardSearch(Console.ReadLine());
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