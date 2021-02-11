using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace MTGRares {
        static class ProgFunctions {

            public static List<Card> Allcards {get; set;}
            public static List<string> Colorids {get; private set;}
            public static List<List<Card>> SepedCardsByColorId {get; private set;}
            public static List<List<String>> Types {get; private set;}
            public static List<List<String>> Subtypes {get; private set;}
            public static List<List<String>> Cmcs {get; private set;}
            public static List<List<List<Card>>> SepedCardsByCmcs {get; private set;}
            public static List<List<List<Card>>> SepedCardsByType {get; private set;}
            public static List<List<List<Card>>> SepedCardsBySubtype {get; private set;}
            
            private static void CardSearch(string cardname, bool isexact) {
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

            private static void CardSearchByIndex(int index)
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

            private static void SepByColorId(bool showcards){
                if(Colorids is null)
                {
                    Colorids = new List<string>();
                    SepedCardsByColorId = new List<List<Card>>();

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
                    // Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                    for(int i = 0; i < Colorids.Count; i++)
                    {
                        Console.WriteLine("- " + Colorids[i]);
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        foreach(Card card in SepedCardsByColorId[i])
                        {
                            Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                        }
                        Console.WriteLine("--------");
                        Console.WriteLine(" ");
                    }
                    Console.WriteLine(" ");
                    Console.WriteLine("Enter Any Key To Exit:");
                    Console.ReadLine(); 
                }

            }

            private static void SepByTypeLine()
            {
                Types = new List<List<String>>(); // color - type
                Subtypes = new List<List<String>>();

                SepedCardsByType = new List<List<List<Card>>>(); // color - type - card
                SepedCardsBySubtype = new List<List<List<Card>>>();

                if(Colorids is null)
                {
                    SepByColorId(false);
                }

                foreach(List<Card> color in SepedCardsByColorId)
                {
                    Types.Add(new List<string>());
                    Subtypes.Add(new List<string>());
                    SepedCardsByType.Add(new List<List<Card>>());
                    SepedCardsBySubtype.Add(new List<List<Card>>());

                    foreach(Card card in color)
                    {
                        if(card.Card_faces != null)
                        {
                            foreach(Cardface cardface in card.Card_faces)
                            {
                                if(cardface.Type_line.Contains('—'))
                                {
                                    if(!Types[Types.Count - 1].Contains(cardface.Type_line.Substring(0,cardface.Type_line.IndexOf('—') - 1)))
                                    {
                                        Types[Types.Count - 1].Add(cardface.Type_line.Substring(0,cardface.Type_line.IndexOf('—') - 1));
                                        SepedCardsByType[SepedCardsByType.Count - 1].Add(new List<Card>());
                                        SepedCardsByType[SepedCardsByType.Count - 1][SepedCardsByType[SepedCardsByType.Count - 1].Count-1].Add(card);
                                    }
                                    else
                                    {
                                        SepedCardsByType[SepedCardsByType.Count - 1][Types[Types.Count - 1].IndexOf(cardface.Type_line.Substring(0,cardface.Type_line.IndexOf('—') - 1))].Add(card);
                                    }

                                    if(!Subtypes[Subtypes.Count - 1].Contains(cardface.Type_line.Substring(cardface.Type_line.IndexOf('—') + 2, cardface.Type_line.Length - (cardface.Type_line.IndexOf('—') + 2))))
                                    {
                                        Subtypes[Subtypes.Count - 1].Add(cardface.Type_line.Substring(cardface.Type_line.IndexOf('—') + 2, cardface.Type_line.Length - (cardface.Type_line.IndexOf('—') + 2)));
                                        SepedCardsBySubtype[SepedCardsBySubtype.Count - 1].Add(new List<Card>());
                                        SepedCardsBySubtype[SepedCardsBySubtype.Count - 1][SepedCardsBySubtype[SepedCardsBySubtype.Count - 1].Count-1].Add(card);                                        
                                    }
                                    else
                                    {
                                        SepedCardsBySubtype[SepedCardsBySubtype.Count - 1][Subtypes[Subtypes.Count - 1].IndexOf(cardface.Type_line.Substring(cardface.Type_line.IndexOf('—') + 2, cardface.Type_line.Length - (cardface.Type_line.IndexOf('—') + 2)))].Add(card);
                                    }
                                }
                                else
                                {
                                    if(!Types[Types.Count - 1].Contains(cardface.Type_line))
                                    {
                                        Types[Types.Count - 1].Add(cardface.Type_line);
                                        SepedCardsByType[SepedCardsByType.Count - 1].Add(new List<Card>());
                                        SepedCardsByType[SepedCardsByType.Count - 1][SepedCardsByType[SepedCardsByType.Count - 1].Count-1].Add(card);
                                    }
                                    else
                                    {
                                        SepedCardsByType[SepedCardsByType.Count - 1][Types[Types.Count - 1].IndexOf(cardface.Type_line)].Add(card);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(card.Type_line.Contains('—'))
                            {
                                if(!Types[Types.Count - 1].Contains(card.Type_line.Substring(0,card.Type_line.IndexOf('—') - 1)))
                                {
                                    Types[Types.Count - 1].Add(card.Type_line.Substring(0,card.Type_line.IndexOf('—') - 1));
                                    SepedCardsByType[SepedCardsByType.Count - 1].Add(new List<Card>());
                                    SepedCardsByType[SepedCardsByType.Count - 1][SepedCardsByType[SepedCardsByType.Count - 1].Count-1].Add(card);
                                }
                                else
                                {
                                    SepedCardsByType[SepedCardsByType.Count - 1][Types[Types.Count - 1].IndexOf(card.Type_line.Substring(0,card.Type_line.IndexOf('—') - 1))].Add(card);
                                }
                                if(!Subtypes[Subtypes.Count - 1].Contains(card.Type_line.Substring(card.Type_line.IndexOf('—') + 2, card.Type_line.Length - (card.Type_line.IndexOf('—') + 2))))
                                {
                                    Subtypes[Subtypes.Count - 1].Add(card.Type_line.Substring(card.Type_line.IndexOf('—') + 2, card.Type_line.Length - (card.Type_line.IndexOf('—') + 2)));
                                    SepedCardsBySubtype[SepedCardsBySubtype.Count - 1].Add(new List<Card>());
                                    SepedCardsBySubtype[SepedCardsBySubtype.Count - 1][SepedCardsBySubtype[SepedCardsBySubtype.Count - 1].Count-1].Add(card);                                        
                                }
                                else
                                {
                                    SepedCardsBySubtype[SepedCardsBySubtype.Count - 1][Subtypes[Subtypes.Count - 1].IndexOf(card.Type_line.Substring(card.Type_line.IndexOf('—') + 2, card.Type_line.Length - (card.Type_line.IndexOf('—') + 2)))].Add(card);
                                }
                            }
                            else
                            {
                                if(!Types[Types.Count - 1].Contains(card.Type_line))
                                {
                                    Types[Types.Count - 1].Add(card.Type_line);
                                    SepedCardsByType[SepedCardsByType.Count - 1].Add(new List<Card>());
                                    SepedCardsByType[SepedCardsByType.Count - 1][SepedCardsByType[SepedCardsByType.Count - 1].Count-1].Add(card);
                                }
                                else
                                {
                                    SepedCardsByType[SepedCardsByType.Count - 1][Types[Types.Count - 1].IndexOf(card.Type_line)].Add(card);
                                }
                            }
                        }
                    }
                }
            }

            private static void SepByCMC()
            {
                Cmcs = new List<List<string>>(); // color - cmc
                SepedCardsByCmcs = new List<List<List<Card>>>(); // color - cmc - card

                if(Colorids is null)
                {
                    SepByColorId(false);
                }

                foreach(List<Card> color in SepedCardsByColorId)
                {
                    Cmcs.Add(new List<string>());
                    SepedCardsByCmcs.Add(new List<List<Card>>());

                    foreach(Card card in color)
                    {
                        if(!Cmcs[Cmcs.Count-1].Contains(card.Cmc.Split('.')[0]))
                        {
                            Cmcs[Cmcs.Count-1].Add(card.Cmc.Split('.')[0]);
                            SepedCardsByCmcs[SepedCardsByCmcs.Count - 1].Add(new List<Card>());
                            SepedCardsByCmcs[SepedCardsByCmcs.Count - 1][Cmcs[Cmcs.Count-1].Count-1].Add(card);
                        }
                        else
                        {
                            SepedCardsByCmcs[SepedCardsByCmcs.Count - 1][Cmcs[Cmcs.Count-1].IndexOf(card.Cmc.Split('.')[0])].Add(card);
                        }
                    }
                }

                foreach(List<List<Card>> color in SepedCardsByCmcs)
                {
                    color.Sort((x,y) => {return x[0].Cmc.Split('.')[0].CompareTo(y[0].Cmc.Split('.')[0]);});
                }

                foreach(List<string> cmc in Cmcs)
                {
                    cmc.Sort((x,y) => {return x.CompareTo(y);});
                }


            }

            public static void DisplayByCMC() //refactor displays to be one function
            {
                if(Cmcs is null)
                {
                    SepByCMC();
                }

                Console.Clear();
                for(int i = 0; i < SepedCardsByCmcs.Count; i++)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("--" + " " + Colorids[i]);

                    for(int j = 0; j < SepedCardsByCmcs[i].Count; j++)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("-" + " " + Cmcs[i][j]);
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        foreach(Card card in SepedCardsByCmcs[i][j])
                        {
                          Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                        }
                    }
                }
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();                
            }

            private static void DisplayByType()
            {
                if(Types is null)
                {
                    SepByTypeLine();
                }

                Console.Clear();
                for(int i = 0; i < SepedCardsByType.Count; i++)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("--" + " " + Colorids[i]);

                    for(int j = 0; j < SepedCardsByType[i].Count; j++)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("-" + " " + Types[i][j]);
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        foreach(Card card in SepedCardsByType[i][j])
                        {
                          Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));
                        }
                    }
                }
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine();                 
            }

            private static void DisplayBySubtype()
            {
                if(Subtypes is null)
                {
                    SepByTypeLine();
                }

                Console.Clear();
                for(int i = 0; i < SepedCardsBySubtype.Count; i++)
                {
                    if(SepedCardsBySubtype[i].Count != 0)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine("--" + " " + Colorids[i]);

                        for(int j = 0; j < SepedCardsBySubtype[i].Count; j++)
                        {
                            Console.WriteLine(" ");
                            Console.WriteLine("-" + " " + Subtypes[i][j]);
                            Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                            foreach(Card card in SepedCardsBySubtype[i][j])
                            {
                                Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));                            
                            }
                        }
                    }
                }
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine(); 
            }

            private static void CardsByColor(string colors, bool containsonly)
            {
                //'C' for colorless
                bool notinset = false;
                bool nocards = true;
                if(Colorids is null){
                    SepByColorId(false);
                }

                Console.Clear();

                for(int i = 0; i < Colorids.Count; i++)
                 {
                    if(containsonly){
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
                        }
                        else
                        {
                            notinset = true;
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
                    }

                    if(!notinset)
                    {
                        nocards = false;
                        Console.WriteLine("- " + Colorids[i]);
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        foreach(Card card in SepedCardsByColorId[i])
                        {
                            Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));                            
                        }
                        Console.WriteLine("--------");
                        Console.WriteLine(" ");                                
                    }
                    notinset = false;             
                }

                if(nocards)
                {
                    Console.WriteLine("No Cards In Color(s)");
                }

                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine(); 
                
            }

            private static void CardsByText(string expr) //change to allow color selection like other cardsby
            {
                bool match = false;
                bool printed = false;
                if(Colorids is null){
                    SepByColorId(false);
                }

                Console.Clear();
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
                                        printed = true;
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
                                        printed = true;
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

                if(!printed)
                {
                    Console.WriteLine("No Cards In Search");
                }


                Console.WriteLine(" ");
                Console.WriteLine("Enter Any Key To Exit:");
                Console.ReadLine(); 
            }
            private static void CardsByFilter(List<List<List<Card>>> sepedcardsbyfilter, List<List<String>> filter, string expr, string colors, bool usecolors, bool colorsexact, bool exprexact)
            {
                Console.Clear();

                bool printed = false;
                for(int i = 0; i < sepedcardsbyfilter.Count; i++) //color
                {
                    bool correctcolor = true;
                    bool correctcmc = true;
                    bool consolecolor = true;
                    if(usecolors)
                    {
                        if(colorsexact)
                        {
                            if(colors.Length == Colorids[i].Length)
                            {
                                foreach(char color in colors)
                                {
                                    if(!Colorids[i].Contains(char.ToUpper(color)))
                                    {
                                        correctcolor = false;
                                        break;
                                    }
                                }                               
                            }
                            else
                            {
                                correctcolor = false;
                            }
                        }
                        else
                        {
                            correctcolor = false;
                            foreach(char color in colors)
                            {
                                if(Colorids[i].Contains(char.ToUpper(color)))
                                {
                                    correctcolor = true;
                                    break;
                                }
                            }
                        }                            
                    }

                    if(correctcolor)
                    {
                        consolecolor = true;

                        for(int j = 0; j < sepedcardsbyfilter[i].Count; j++) //cmc
                        {
                            correctcmc = false;
                            if(exprexact)
                            {
                                if(filter[i][j].ToLower().Equals(expr.ToLower()))
                                {
                                    correctcmc = true;
                                }
                            }
                            else
                            {
                                if(Regex.IsMatch(filter[i][j].ToLower(),expr.ToLower()))
                                {
                                    correctcmc = true;
                                }
                            }

                            if(correctcmc)
                            {
                                if(consolecolor)
                                {
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("--" + " " + Colorids[i]);
                                    consolecolor = false;
                                    printed = true;
                                }

                                Console.WriteLine(" ");
                                Console.WriteLine("-" + " " + filter[i][j]);
                                Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");

                                foreach(Card card in sepedcardsbyfilter[i][j])
                                {
                                    Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + Allcards.IndexOf(card));                          
                                }                                
                            }
                        }
                    }
                }

                if(!printed)
                {
                    Console.WriteLine("No Cards In Search");
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
                            if(Convert.ToInt32(selection) == 1)
                            {
                                DisplayByType();
                            }
                            else if(Convert.ToInt32(selection) == 2)
                            {
                                DisplayBySubtype();
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

                                if(SepedCardsByType is null)
                                {
                                    SepByTypeLine();
                                }

                                if(Convert.ToInt32(selection) == 3)
                                {
                                    CardsByFilter(SepedCardsByType,Types,expr,colors,usecolors,colorsexact,exactsearch);
                                }
                                else
                                {
                                    CardsByFilter(SepedCardsBySubtype,Subtypes,expr,colors,usecolors,colorsexact,exactsearch);
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
                            if(Convert.ToInt32(selection) == 1)
                            {
                                DisplayByCMC();
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

                                if(SepedCardsByCmcs is null)
                                {
                                    SepByCMC();
                                }

                                CardsByFilter(SepedCardsByCmcs,Cmcs,expr,colors,usecolors,colorsexact,true);
                                //CardsByCMC(expr,colors,usecolors,colorsexact,true);                                
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