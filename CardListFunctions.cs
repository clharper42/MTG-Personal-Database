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
                Console.WriteLine("Enter List Name:");
                name = Console.ReadLine();
                if(name.Length > 0)
                {
                    break;
                }
            }

            while(true)
            {
                Console.Clear();
                Console.WriteLine("Enter List Description:");
                desc = Console.ReadLine();
                if(desc.Length > 0)
                {
                    break;
                }
            }

           AllCardLists.Add(new CardList(name,desc));
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
            CardList thelist = GetCardList();
            thelist.Add();                      
        }
        public static void DisplayCardList() {

            CardList thelist = GetCardList();
            thelist.Display();
        }

        public static void PrintFileCardList() {

            CardList thelist = GetCardList();
            thelist.PrintToFile();
        }

        public static void RemoveFromCardList() {
            CardList thelist = GetCardList();
            thelist.Remove();
        }

        public static void RemoveCardList() {
            CardList thelist = GetCardList();
            Console.WriteLine(thelist.Name + " Was Removed");
            AllCardLists.Remove(thelist);
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();
        }

        public static void PrintCardListToFile() {
            CardList thelist = GetCardList();
            thelist.PrintToFile();
            //add feature
        }        
    }
}