using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
        static class ProgFunctions {

            public static List<Card> Allcards {get; set;}
            public static List<string> Colorids {get; private set;}
            public static List<List<Card>> SepedCardsByColorId {get; private set;}

            public static List<string> Types {get; private set;}
            public static List<string> Subtypes {get; private set;}
            public static List<List<Card>> SepedCardsByType {get; private set;}
            public static List<List<Card>> SepedCardsBySubtype {get; private set;}
            public static void CardSearch(string cardname, bool isexact) {
                Console.Clear();
                bool found = false;
                if(isexact)
                {
                    int min = 0;
                    int max = Allcards.Count - 1;
                    int index = 0;
                    while(min <= max)
                    {
                        int mid = (min + max) / 2;
                        if(cardname.ToLower().Equals(Allcards[mid].Name.ToLower())){
                            found = true;
                            index = mid;
                            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                            Console.WriteLine(Allcards[index].Special_name + " " + Allcards[index].Set + " " + Allcards[index].Printing + " " + Allcards[index].Amount + " " + index);
                            for(int i = index + 1; i < Allcards.Count; i++)
                            {
                                if(cardname.ToLower().Equals(Allcards[i].Name.ToLower()))
                                {
                                    Console.WriteLine(Allcards[index].Special_name + " " + Allcards[index].Set + " " + Allcards[index].Printing + " " + Allcards[index].Amount + " " + index);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for(int i = index - 1; i >= 0; i--)
                            {
                                if(cardname.ToLower().Equals(Allcards[i].Name.ToLower()))
                                {
                                    Console.WriteLine(Allcards[index].Special_name + " " + Allcards[index].Set + " " + Allcards[index].Printing + " " + Allcards[index].Amount + " " + index);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else if(cardname.ToLower().CompareTo(Allcards[mid].Name.ToLower()) < 0){
                            max = mid - 1;
                        }
                        else{
                            min = mid + 1;
                        }
                    }
                }
                else
                {
                    foreach(Card card in Allcards)
                    {
                        if(Regex.IsMatch(card.Name.ToLower(),cardname.ToLower()))
                        {
                            if(found == false)
                            {
                                Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                            }
                            found = true;
                            Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
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
                if(index >= 0 && index < Allcards.Count)
                {
                    Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                    Console.WriteLine(Allcards[index].Special_name + " " + Allcards[index].Set + " " + Allcards[index].Printing + " " + Allcards[index].Amount + " " + index);
                }
                else
                {
                    Console.WriteLine("Invalid ID");
                }
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine(); 
            }

            public static void SepByColorId(bool showcards){
                Colorids = new List<string>();
                SepedCardsByColorId = new List<List<Card>>();
                if(Colorids.Count == 0)
                {
                    Colorids.Add("C"); // C for colorless
                    SepedCardsByColorId.Add( new List<Card>());
                    foreach(Card card in Allcards)
                    {
                        if(card.Color_identity.Count == 0)
                        {
                            SepedCardsByColorId[0].Add(card);
                        }
                        else
                        {
                            string idstr = new string(card.Color_identity.ToArray());
                            if(!Colorids.Contains(idstr))
                            {
                                Colorids.Add(idstr);
                                SepedCardsByColorId.Add(new List<Card>());
                                SepedCardsByColorId[SepedCardsByColorId.Count -1].Add(card);
                            }
                            else
                            {
                                SepedCardsByColorId[Colorids.IndexOf(idstr)].Add(card);
                            }
                        }

                    }

                }

                if(showcards)
                {
                    Console.Clear();
                    Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                    for(int i = 0; i < Colorids.Count; i++)
                    {
                        Console.WriteLine(Colorids[i]);
                        foreach(Card card in SepedCardsByColorId[i])
                        {
                            Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                        }
                        Console.WriteLine("--------");
                    }
                    Console.WriteLine(" ");
                    Console.WriteLine("Enter Any Key To Exit:");
                    Console.ReadLine(); 
                }

            }

            public static void SepByTypeLine()
            {
                Types = new List<string>();
                Subtypes = new List<string>();

                SepedCardsByType = new List<List<Card>>();
                SepedCardsBySubtype = new List<List<Card>>();

                foreach(Card card in Allcards)
                {
                    if(card.Card_faces != null)
                    {
                        foreach(Cardface cardface in card.Card_faces)
                        {
                            if(cardface.Type_line.Contains('—'))
                            {
                                if(!Types.Contains(cardface.Type_line.Substring(0,cardface.Type_line.IndexOf('—') - 1)))
                                {
                                    Types.Add(cardface.Type_line.Substring(0,cardface.Type_line.IndexOf('—') - 1));
                                    SepedCardsByType.Add(new List<Card>());
                                    SepedCardsByType[SepedCardsByType.Count - 1].Add(card);
                                }
                                else
                                {
                                    SepedCardsByType[Types.IndexOf(cardface.Type_line.Substring(0,cardface.Type_line.IndexOf('—') - 1))].Add(card);
                                }

                                if(!Subtypes.Contains(cardface.Type_line.Substring(cardface.Type_line.IndexOf('—') + 2, cardface.Type_line.Length - (cardface.Type_line.IndexOf('—') + 2))))
                                {
                                    Subtypes.Add(cardface.Type_line.Substring(cardface.Type_line.IndexOf('—') + 2, cardface.Type_line.Length - (cardface.Type_line.IndexOf('—') + 2)));
                                    SepedCardsBySubtype.Add(new List<Card>());
                                    SepedCardsBySubtype[SepedCardsBySubtype.Count - 1].Add(card);
                                }
                                else
                                {
                                    SepedCardsBySubtype[Subtypes.IndexOf(cardface.Type_line.Substring(cardface.Type_line.IndexOf('—') + 2, cardface.Type_line.Length - (cardface.Type_line.IndexOf('—') + 2)))].Add(card);
                                }                                
                            }
                            else
                            {
                                if(!Types.Contains(cardface.Type_line))
                                {
                                    Types.Add(cardface.Type_line);
                                    SepedCardsByType.Add(new List<Card>());
                                    SepedCardsByType[SepedCardsByType.Count - 1].Add(card);
                                }
                                else
                                {
                                    SepedCardsByType[Types.IndexOf(cardface.Type_line)].Add(card);
                                }                                 
                            }
                        }
                    }
                    else
                    {
                            if(card.Type_line.Contains('—'))
                            {
                                if(!Types.Contains(card.Type_line.Substring(0,card.Type_line.IndexOf('—') - 1)))
                                {
                                    Types.Add(card.Type_line.Substring(0,card.Type_line.IndexOf('—') - 1));
                                    SepedCardsByType.Add(new List<Card>());
                                    SepedCardsByType[SepedCardsByType.Count - 1].Add(card);
                                }
                                else
                                {
                                    SepedCardsByType[Types.IndexOf(card.Type_line.Substring(0,card.Type_line.IndexOf('—') - 1))].Add(card);
                                }

                                if(!Subtypes.Contains(card.Type_line.Substring(card.Type_line.IndexOf('—') + 2, card.Type_line.Length - (card.Type_line.IndexOf('—') + 2))))
                                {
                                    Subtypes.Add(card.Type_line.Substring(card.Type_line.IndexOf('—') + 2, card.Type_line.Length - (card.Type_line.IndexOf('—') + 2)));
                                    SepedCardsBySubtype.Add(new List<Card>());
                                    SepedCardsBySubtype[SepedCardsBySubtype.Count - 1].Add(card);
                                }
                                else
                                {
                                    SepedCardsBySubtype[Subtypes.IndexOf(card.Type_line.Substring(card.Type_line.IndexOf('—') + 2, card.Type_line.Length - (card.Type_line.IndexOf('—') + 2)))].Add(card);
                                }                                
                            }
                            else
                            {
                                if(!Types.Contains(card.Type_line))
                                {
                                    Types.Add(card.Type_line);
                                    SepedCardsByType.Add(new List<Card>());
                                    SepedCardsByType[SepedCardsByType.Count - 1].Add(card);
                                }
                                else
                                {
                                    SepedCardsByType[Types.IndexOf(card.Type_line)].Add(card);
                                }                                 
                            }                        
                    }
                }
            }

            public static void CardsByColor(string colors, bool containsonly)
            {
                //'C' for colorless
                List<Card> cardsincolor = new List<Card>();
                bool notinset = false;
                if(Colorids is null){
                    SepByColorId(false);
                }

                for(int i = 0; i < Colorids.Count; i++)
                 {
                    if(containsonly){
                        //fix this
                        if(Colorids[i].Length == colors.Length)
                        {
                             foreach(char let in colors)
                            {
                                if(!Colorids[i].Contains(Char.ToUpper(let)))
                                {
                                    notinset = true;
                                    break;
                                }
                            }
                            if(!notinset)
                            {
                                cardsincolor.AddRange(SepedCardsByColorId[i]);
                            }
                            notinset = false;
                        }
                    }
                    else
                    {
                        notinset = true;
                        foreach(char let in colors)
                        {
                            if(Colorids[i].Contains(Char.ToUpper(let)))
                            {
                                notinset = false;
                                break;
                            }
                        }
                        if(!notinset)
                        {
                            cardsincolor.AddRange(SepedCardsByColorId[i]);
                        }
                    }
                }

                Console.Clear();
                if(cardsincolor.Count > 0)
                {
                    Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                    foreach(Card card in cardsincolor)
                    {
                        Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                    }
                }
                else
                {
                    Console.WriteLine("No Cards In Color(s)");
                }

                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine(); 
                
            }

            public static void CardsByText(string expr)
            {
                bool match = false;
                if(Colorids is null){
                    SepByColorId(false);
                }

                for(int i = 0; i < SepedCardsByColorId.Count; i++)
                {
                    foreach(Card card in SepedCardsByColorId[i])
                    {
                        if(card.Card_faces is null)
                        {

                            if(card.Oracle_text != null)
                            {
                                if(Regex.IsMatch(card.Oracle_text.ToLower(),expr.ToLower()))
                                {
                                    if(!match)
                                    {
                                         Console.WriteLine(" ");
                                        Console.WriteLine(" - " + Colorids[i]);
                                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                                        match = true;
                                    }
                                    Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                                    Console.WriteLine(card.Oracle_text);
                                    Console.WriteLine("-----");
                                }
                            }
                        }
                        else
                        {
                            foreach(Cardface cardface in card.Card_faces)
                            {
                                if(cardface.Oracle_text != null)
                            {
                                if(Regex.IsMatch(cardface.Oracle_text.ToLower(),expr.ToLower()))
                                {
                                    if(!match)
                                    {
                                        Console.WriteLine(" - " + Colorids[i]);
                                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                                        match = true;
                                    }                                    
                                    Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                                    Console.WriteLine(cardface.Oracle_text);
                                    Console.WriteLine("-----");
                                }
                            }
                            }
                        }                        
                    }
                    match = false;
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
                        Console.WriteLine("Search By Card Name Or Card Database ID:");
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
                                        CardSearch(Console.ReadLine(),true);
                                        break;
                                    }
                                    else if("n".Equals(decs))
                                    {
                                        Console.WriteLine("Enter Card Name:");
                                        CardSearch(Console.ReadLine(),false);
                                        break;
                                    }
                                }
                            }
                            else if(Convert.ToInt32(selection) == 2)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter ID:");
                                CardSearchByIndex(Convert.ToInt32(Console.ReadLine()));

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
                        Console.WriteLine("Seprate All Cards By Color Idenity or Search For Cards By Color Idenity:");
                        Console.WriteLine("1 - Sperate Cards");
                        Console.WriteLine("2 - Search By Color");
                        Console.WriteLine("3 - Exit");
                        selection = Console.ReadLine();
                        if(Regex.IsMatch(selection,@"^[1-3]$"))
                        {
                            if(Convert.ToInt32(selection) == 1)
                            {
                                SepByColorId(true);
                            }
                            else if(Convert.ToInt32(selection) == 2)
                            {

                                while(true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter colors (W - white, U - Blue, B - Black, R - Red, G - green, C - Colorless):");
                                    selection = Console.ReadLine();
                                    if(Regex.IsMatch(selection, @"^[WUBRGCwubrgc]+$"))
                                    {
                                        string decs;
                                        while(true)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Are only these colors? (Y/N):");
                                            decs = Console.ReadLine();
                                            if("y".Equals(decs.ToLower()))
                                            {
                                                CardsByColor(selection,true);
                                                break;
                                            }
                                            else if("n".Equals(decs.ToLower()))
                                            {
                                                CardsByColor(selection,false);
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
                else if(choice == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Enter Text(Cards Filterd By Color Idenity):");
                    CardsByText(Console.ReadLine());   
                }
            }

        }

    }