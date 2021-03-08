using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
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

        public void Add() //NEEDS TO CHECK IF CARD IS ALREADY IN LIST
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
                                int numofalready = 0;
                                while(true)
                                {
                                    Console.Clear();
                                    if(TheList.Contains(card))
                                    {
                                        Console.WriteLine(NumOfCard[TheList.IndexOf(card)] +"x " + card.Special_name + " Already In list");
                                        numofalready = NumOfCard[TheList.IndexOf(card)];
                                        Console.WriteLine(" ");
                                    }
                                    Console.WriteLine("Total Of " + card.Amount + " " + card.Special_name + " In Collection");
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" - Enter B To Back Out - ");
                                    Console.WriteLine("Enter Amout Of " + card.Special_name + " To Add:");
                                    numof = Console.ReadLine();
                                    if(Regex.IsMatch(numof, @"^[0-9]+$") && Convert.ToInt32(numof) > 0 && Convert.ToInt32(numof) <= card.Amount && Convert.ToInt32(numof) + numofalready <= card.Amount)
                                    {
                                        NumOfCard.Add(Convert.ToInt32(numof));
                                        TheList.Add(card);
                                        break;
                                    }
                                    else if("b".Equals(numof.ToLower()))
                                    {
                                        break;
                                    }
                                }

                                if("b".Equals(numof.ToLower()))
                                {
                                    break;
                                }

                                Console.Clear();
                                Console.WriteLine("Card Added");
                                Console.WriteLine(" ");
                                Console.WriteLine("Enter Any Key To Continue:");
                                Console.ReadLine();                                 
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

        public void AddFromLoad(Card card, int amount) {
            TheList.Add(card);
            NumOfCard.Add(amount);
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("NAME - SET - PRINTING - AMOUNT IN LIST - CARD DB ID");
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
            Console.WriteLine("NAME - SET - PRINTING - AMOUNT IN LIST - LIST ID");
            foreach(Card card in TheList)
            {
                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + NumOfCard[TheList.IndexOf(card)] + " " + TheList.IndexOf(card));
            }
            Console.WriteLine(" ");         
        }

        public void PrintToFile()
        {
            string selection;
            //print nonloadable, pint loadable
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Select Print Option:");
                Console.WriteLine("1 - Print Deck Builder File (Archidekt/Tappedout)");
                Console.WriteLine("2 - Print TCGPlayer File");
                Console.WriteLine("3 - Print StarCity/CardKingdom File");
                Console.WriteLine("4 - Print Loadable File");
                Console.WriteLine("5 - Exit");
                selection = Console.ReadLine();
                if(Regex.IsMatch(selection,@"^[1-5]$"))
                {
                    string docpath = Environment.CurrentDirectory + "\\Files";

                    if("1".Equals(selection))
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docpath, Name + "DeckBuild.txt"),false))
                        {
                            outputFile.WriteLine("Name: " + Name);
                            outputFile.WriteLine("");
                            outputFile.WriteLine("Description: " + Description);
                            outputFile.WriteLine("");
                            outputFile.WriteLine("");
                            for(int i = 0; i < TheList.Count; i++)
                            {
                                outputFile.WriteLine(NumOfCard[i] + "x " + TheList[i].Name + " (" + TheList[i].Set +")");
                            }
                        }
                    }
                    else if("2".Equals(selection))
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docpath, Name + "TCGPlayer.txt"),false))
                        {
                            outputFile.WriteLine("Name: " + Name);
                            outputFile.WriteLine("");
                            outputFile.WriteLine("Description: " + Description);
                            outputFile.WriteLine("");
                            outputFile.WriteLine("");
                            for(int i = 0; i < TheList.Count; i++)
                            {
                                outputFile.WriteLine(NumOfCard[i] + " " + TheList[i].Name + " [" + TheList[i].Set +"]");
                            }
                        }
                    }
                    else if("3".Equals(selection))
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docpath, Name + "StarKingdom.txt"),false))
                        {
                            outputFile.WriteLine("Name: " + Name);
                            outputFile.WriteLine("");
                            outputFile.WriteLine("Description: " + Description);
                            outputFile.WriteLine("");
                            outputFile.WriteLine("");
                            for(int i = 0; i < TheList.Count; i++)
                            {
                                outputFile.WriteLine(NumOfCard[i] + " x " + TheList[i].Name);
                            }
                        }                
                    }          
                    else if("4".Equals(selection))
                    {
                        //check if star in set number works on load
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docpath, Name + "Load.txt"),false))
                        {
                            outputFile.WriteLine(Name);
                            outputFile.WriteLine(Description);
                            for(int i = 0; i < TheList.Count; i++)
                            {
                                outputFile.WriteLine(TheList[i].Name + "_" + TheList[i].Set + "_" + TheList[i].Collector_number  + "_" + TheList[i].Printing  + "_" + NumOfCard[i]);
                            }
                        }                
                    }
                    else
                    {
                        Console.Clear();
                        return;
                    }
        
                    Console.Clear();
                    Console.WriteLine("List Pinted");
                    Console.WriteLine(" ");
                    Console.WriteLine("Enter Any Key To Exit:");
                    Console.ReadLine();
                }
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
                    Console.WriteLine("Amount In List " + Name + " Is Now " + NumOfCard[TheList.IndexOf(card)]);
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
                    Console.Clear();
                    Console.WriteLine("Card Removed");
                    Console.WriteLine(" ");
                    Console.WriteLine("Enter Any Key To Continue:");
                    Console.ReadLine(); 
                }                  
            }                      
        }

    }
}