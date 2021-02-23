using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
    class CardList {
        public string Name {get; private set;}
        public string Description {get; private set;}
        public List<Card> TheList {get; private set;}
        public List<int> NumOfCard {get; private set;}
        public CardList(string name, string description) {
            Name = name;
            Description = description;
            TheList = new List<Card>();
            NumOfCard = new List<int>();
        }

        public void Add()
        {
            string selection;
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
                            else if(Regex.IsMatch(index, @"^[0-9]+$") && Convert.ToInt32(index) >= 0 && Convert.ToInt32(index) < ProgFunctions.Allcards.Count)
                            {
                                Card card = ProgFunctions.Allcards[Convert.ToInt32(index)];
                                string numof;
                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter Amout Of " + card.Special_name + " To Add:");
                                    numof = Console.ReadLine();
                                    if(Regex.IsMatch(numof, @"^[0-9]+$") && Convert.ToInt32(numof) > 0 && Convert.ToInt32(numof) <= card.Amount)
                                    {
                                        NumOfCard.Add(Convert.ToInt32(numof));
                                        break;
                                    }
                                }
                                TheList.Add(card);                                
                                break;
                            }
                        }
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

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
            foreach(Card card in TheList)
            {
                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + NumOfCard[TheList.IndexOf(card)] + " " + ProgFunctions.Allcards.IndexOf(card));
            }

            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();              
        }

        private void DisplayWithID()
        {
            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - LIST ID");
            foreach(Card card in TheList)
            {
                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + NumOfCard[TheList.IndexOf(card)] + " " + TheList.IndexOf(card));
            }         
        }

        public void PrintToFile()
        {
            //add feature
            //https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file
            string selection;
            //print nonloadable, pint loadable
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Select Print Option:");
                Console.WriteLine("1 - Print Normal File");
                Console.WriteLine("2 - Print Loadable File");
                selection = Console.ReadLine();
                if("1".Equals(selection) || "2".Equals(selection))
                {
                    break;
                }
            }

            if("1".Equals(selection))
            {

            }
        }

        public void RemoveByCard(Card card)
        {
            Console.Clear();
            if(TheList.Contains(card))
            {
                if(NumOfCard[TheList.IndexOf(card)] > 1)
                {
                    NumOfCard[TheList.IndexOf(card)] = NumOfCard[TheList.IndexOf(card)] - 1;
                    Console.WriteLine("Amount In List " + Name + " is now " + NumOfCard[TheList.IndexOf(card)]);
                }
                else
                {
                    NumOfCard.RemoveAt(TheList.IndexOf(card));
                    TheList.Remove(card);
                    Console.WriteLine("Card Removed From List " + Name);
                }                 
            }
            else
            {
                Console.WriteLine("Card Not In List");
            }
           
        }

        public void Remove()
        {
            string index;
            while(true)
            {
                Console.Clear();
                DisplayWithID();
                Console.WriteLine(" - Enter B To Back Out - ");
                Console.WriteLine("Enter ID: ");
                index = Console.ReadLine();
                if(index.ToLower().Equals("b"))
                {
                    break;
                }
                else if(Regex.IsMatch(index, @"^[0-9]+$") && Convert.ToInt32(index) >= 0 && Convert.ToInt32(index) < TheList.Count)
                {
                    RemoveByCard(TheList[Convert.ToInt32(index)]);
                    break;
                }                  
            }
            
            Console.WriteLine(" ");
            Console.WriteLine("Enter Any Key To Exit:");
            Console.ReadLine();                       
        }

    }
}