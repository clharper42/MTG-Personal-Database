using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    
    static class CardSearchFunctions {
        public static void CardSearch(string cardname) {
            Console.Clear();

            bool isexact;
            string decs;
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Exact search? (Y/N)");
                decs = Console.ReadLine().ToLower();
                Console.Clear();
                if("y".Equals(decs))
                {
                    isexact = true;
                    break;
                }
                else if("n".Equals(decs))
                {
                    isexact = false;
                    break;
                }
            }

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
                Console.WriteLine("Card Name: " + ProgFunctions.Allcards[index].Special_name);
                if(ProgFunctions.Allcards[index].Card_faces is null)
                {
                    Console.WriteLine("Mana Cost: " + ProgFunctions.Allcards[index].Mana_cost);
                    Console.WriteLine("Type Line: " + ProgFunctions.Allcards[index].Type_line);
                    Console.WriteLine("Oracle Text: " + ProgFunctions.Allcards[index].Oracle_text);
                    if(!(ProgFunctions.Allcards[index].Power is null))
                    {
                        Console.WriteLine("Power: " + ProgFunctions.Allcards[index].Power);
                        Console.WriteLine("Toughness: " + ProgFunctions.Allcards[index].Toughness);
                    }
                    if(!(ProgFunctions.Allcards[index].Loyalty is null))
                    {
                        Console.WriteLine("Loyalty: " + ProgFunctions.Allcards[index].Loyalty);
                    }
                }
                else
                {
                    foreach(Cardface cardface in ProgFunctions.Allcards[index].Card_faces)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("Card Face Name: " + cardface.Name);
                        Console.WriteLine("Mana Cost: " + cardface.Mana_cost);
                        Console.WriteLine("Type Line: " + cardface.Type_line);
                        Console.WriteLine("Oracle Text: " + cardface.Oracle_text);
                        if(!(cardface.Power is null))
                        {
                            Console.WriteLine("Power: " + cardface.Power);
                            Console.WriteLine("Toughness: " + cardface.Toughness);
                        }
                        if(!(cardface.Loyalty is null))
                        {
                            Console.WriteLine("Loyalty: " + cardface.Loyalty);
                        }                   
                    }
                }
                Console.WriteLine(" ");
                Console.WriteLine("Scryfall Link: " + ProgFunctions.Allcards[index].Scryfall_uri);               
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