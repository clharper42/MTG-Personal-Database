using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    
    static class CardListFunctions {
        public static List<CardList> AllCardLists {get; private set;} = new List<CardList>();
        public static void CreateCardList() {

            string name;
            string desc;

            while(true)
            {
                Console.Clear();
                Console.WriteLine("50 Character Limit");
                Console.WriteLine("Enter List Name:");
                name = Console.ReadLine();
                if(name.Length > 0 && name.Length < 50 && Regex.IsMatch(name + ".txt", @"^[a-zA-Z0-9](?:[a-zA-Z0-9 ._-]*[a-zA-Z0-9])?\.[a-zA-Z0-9_-]+$"))
                {
                    break;
                }
            }

            while(true)
            {
                Console.Clear();
                Console.WriteLine("50 Character Limit");
                Console.WriteLine("Enter List Description:");
                desc = Console.ReadLine();
                if(desc.Length > 0 && name.Length < 50)
                {
                    break;
                }
            }

           AllCardLists.Add(new CardList(name,desc));
           Console.Clear();
           Console.WriteLine("List Created");
           Console.WriteLine(" ");
           Console.WriteLine("Enter Any Key To Exit:");
           Console.ReadLine();
        }
        public static CardList GetCardList() {
            Console.Clear();
            string index;
            Console.WriteLine("Select List:");
            for(int i = 0; i < AllCardLists.Count; i++)
            {
                Console.WriteLine((i + 1) + " - " + AllCardLists[i].Name);
            }

            while(true)
            {
                index = Console.ReadLine();
                if(Regex.IsMatch(index, @"^[0-9]+$"))
                {
                    if(Convert.ToInt32(index) > 0 && Convert.ToInt32(index) <= AllCardLists.Count)
                    {
                        break;
                    }
                }
            }
    
            return AllCardLists[Convert.ToInt32(index) - 1];
        }
        public static void AddToCardList() {
            if(AllCardLists.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Lists Available");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();
                return;
            }
            CardList thelist = GetCardList();
            thelist.Add();                      
        }
        public static void DisplayCardList() {
            if(AllCardLists.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Lists Available");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();
                return;
            }
            CardList thelist = GetCardList();
            thelist.Display();
        }

        public static void RemoveFromCardList() {
            if(AllCardLists.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Lists Available");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();
                return;
            }            
            CardList thelist = GetCardList();
            thelist.Remove();
        }

        public static void RemoveCardList() {
            if(AllCardLists.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Lists Available");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();
                return;
            }            
            CardList thelist = GetCardList();
            Console.Clear();
            Console.WriteLine(thelist.Name + " Was Removed");
            AllCardLists.Remove(thelist);
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();
        }

        public static void PrintCardListToFile() {
            if(AllCardLists.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Lists Available");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();
                return;
            }            
            CardList thelist = GetCardList();
            thelist.PrintToFile();
        }

        public static void LoadListFromFile() {
            Console.Clear();
            bool canload = false;
            string filename;
            string filepath;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" - Enter B To Back Out - ");
                Console.WriteLine("Enter File Name:");
                filename = Console.ReadLine();
                if(filename.ToLower().Equals("b"))
                {
                    return;
                }
                filename = filename + ".txt";
                filepath = System.IO.Directory.GetCurrentDirectory() + "\\" + filename;
                if(System.IO.File.Exists(filepath))
                {
                    break;
                }
            }
            Console.Clear();

            string[] lines = System.IO.File.ReadAllLines(filepath);

            CardList loadedlist = new CardList(lines[0],lines[1]);
            for(int i = 2; i < lines.Length; i++)
            {
                string[] cardelements = lines[i].Split("_");
                string cardname = cardelements[0];
                string cardset = cardelements[1];
                string cardcolnum = cardelements[2];
                string cardprint = cardelements[3];
                string cardnum = cardelements[4];
                int min = 0;
                int max = ProgFunctions.Allcards.Count - 1;
                int index = 0;
                bool found = false;
                Card card = null;
                while(min <= max)
                {
                    int mid = (min + max) / 2;
                    if(cardname.Equals(ProgFunctions.Allcards[mid].Name)) {
                        index = mid;
                        if(cardset.Equals(ProgFunctions.Allcards[mid].Set) && cardcolnum.Equals(ProgFunctions.Allcards[mid].Collector_number) && cardprint.Equals(ProgFunctions.Allcards[mid].Printing))
                        {
                            card = ProgFunctions.Allcards[mid];
                            found = true;
                            break;
                        }
                        else
                        {
                            for(int j = index + 1; j < ProgFunctions.Allcards.Count; j++)
                            {
                                if(cardname.Equals(ProgFunctions.Allcards[j].Name))
                                {
                                    if(cardset.Equals(ProgFunctions.Allcards[j].Set) && cardcolnum.Equals(ProgFunctions.Allcards[j].Collector_number) && cardprint.Equals(ProgFunctions.Allcards[j].Printing))
                                    {
                                        card = ProgFunctions.Allcards[j];
                                        found = true;
                                        break;
                                    }                                     
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if(found)
                            {
                                break;
                            }
                            for(int j = index - 1; j >= 0; j--)
                            {
                                if(cardname.Equals(ProgFunctions.Allcards[j].Name))
                                {
                                    if(cardset.Equals(ProgFunctions.Allcards[j].Set) && cardcolnum.Equals(ProgFunctions.Allcards[j].Collector_number) && cardprint.Equals(ProgFunctions.Allcards[j].Printing))
                                    {
                                        card = ProgFunctions.Allcards[j];
                                        found = true;
                                        break;
                                    }                                      
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    else if(cardname.CompareTo(ProgFunctions.Allcards[mid].Name) < 0)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }

                if(found)
                {
                    if(Convert.ToInt32(cardnum) > card.Amount)
                    {
                        //card in load is greater amount then card in db
                        string selection;
                        while(true)
                        {
                            Console.Clear();
                            Console.WriteLine(card.Special_name + " Has More In The List" + "(" + cardnum + ")" + " Then In The Database(" + card.Amount + ")");
                            Console.WriteLine("1 - Skip Loading Card");
                            Console.WriteLine("2 - Cancel List Load");                            
                            selection = Console.ReadLine();
                            if(Regex.IsMatch(selection,@"^[1-2]$"))
                            {
                                if("1".Equals(selection))
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    canload = true;
                                    Console.Clear();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //card fine
                        Console.WriteLine(card.Special_name + " Loaded");
                        loadedlist.AddFromLoad(card,Convert.ToInt32(cardnum));
                    }
                }
                else
                {
                    //card not in db
                    string selection;
                    while(true)
                    {
                        Console.Clear();
                        Console.WriteLine(card.Special_name + " Is Not In The Database");
                        Console.WriteLine("1 - Skip Loading Card");
                        Console.WriteLine("2 - Cancel List Load");                            
                        selection = Console.ReadLine();
                        if(Regex.IsMatch(selection,@"^[1-2]$"))
                        {
                            if("1".Equals(selection))
                            {
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                canload = true;
                                Console.Clear();
                                break;
                            }
                        }
                    }
                }

                if(canload)
                {
                    return;
                }
            }

            AllCardLists.Add(loadedlist);
            Console.WriteLine(" ");
            Console.WriteLine("List Loaded");
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine(); 


        }        
    }
}