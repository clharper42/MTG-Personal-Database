using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    
    static class CardSearchFunctions {
        public static void CardSearch(string cardname, bool isexact) {
            Console.Clear();
            bool found = false;
            if(isexact)
            {
                int min = 0;
                int max = ProgFunctions.Allcards.Count - 1;
                int index = 0;
                while(min <= max)
                {
                    int mid = (min + max) / 2;
                    if(cardname.ToLower().Equals(ProgFunctions.Allcards[mid].Name.ToLower())){
                        found = true;
                        index = mid;
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        Console.WriteLine(ProgFunctions.Allcards[index].Special_name + " " + ProgFunctions.Allcards[index].Set + " " + ProgFunctions.Allcards[index].Printing + " " + ProgFunctions.Allcards[index].Amount + " " + index);
                        for(int i = index + 1; i < ProgFunctions.Allcards.Count; i++)
                        {
                            if(cardname.ToLower().Equals(ProgFunctions.Allcards[i].Name.ToLower()))
                            {
                                Console.WriteLine(ProgFunctions.Allcards[index].Special_name + " " + ProgFunctions.Allcards[index].Set + " " + ProgFunctions.Allcards[index].Printing + " " + ProgFunctions.Allcards[index].Amount + " " + index);
                            }
                            else
                            {
                                break;
                            }
                        }
                        for(int i = index - 1; i >= 0; i--)
                        {
                            if(cardname.ToLower().Equals(ProgFunctions.Allcards[i].Name.ToLower()))
                            {
                                Console.WriteLine(ProgFunctions.Allcards[index].Special_name + " " + ProgFunctions.Allcards[index].Set + " " + ProgFunctions.Allcards[index].Printing + " " + ProgFunctions.Allcards[index].Amount + " " + index);
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                    else if(cardname.ToLower().CompareTo(ProgFunctions.Allcards[mid].Name.ToLower()) < 0){
                        max = mid - 1;
                    }
                    else{
                        min = mid + 1;
                    }
                }
            }
            else
            {
                foreach(Card card in ProgFunctions.Allcards)
                {
                    if(Regex.IsMatch(card.Name.ToLower(),cardname.ToLower()))
                    {
                        if(found == false)
                        {
                            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        }
                        found = true;
                        Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));
                    }
                }
            }
            if(!found)
            {
                Console.WriteLine("No Card(s) Found");
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();    
        }
        public static void CardSearchByIndex(int index)
        {
            Console.Clear();
            if(index >= 0 && index < ProgFunctions.Allcards.Count)
            {
                Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                Console.WriteLine(ProgFunctions.Allcards[index].Special_name + " " + ProgFunctions.Allcards[index].Set + " " + ProgFunctions.Allcards[index].Printing + " " + ProgFunctions.Allcards[index].Amount + " " + index);
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine(); 
        }        
    }
}