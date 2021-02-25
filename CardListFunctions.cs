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
    }
}