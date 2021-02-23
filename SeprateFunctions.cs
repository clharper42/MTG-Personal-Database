using System;
using System.Collections.Generic;

namespace MTGRares {
    static class SeprateFunctions {
        public static List<string> Colorids {get; private set;} = new List<string>();
        public static List<List<Card>> SepedCardsByColorId {get; private set;} = new List<List<Card>>();
        public static List<List<String>> Types {get; private set;} = new List<List<string>>();  // color - type
        public static List<List<String>> Subtypes {get; private set;} = new List<List<string>>();
        public static List<List<String>> Cmcs {get; private set;} = new List<List<string>>(); // color - cmc
        public static List<List<String>> Keywords {get; private set;} = new List<List<string>>(); // color - keyword
        public static List<List<List<Card>>> SepedCardsByType {get; private set;} = new List<List<List<Card>>>(); // color - type - card
        public static List<List<List<Card>>> SepedCardsBySubtype {get; private set;} = new List<List<List<Card>>>();
        public static List<List<List<Card>>> SepedCardsByCmcs {get; private set;} = new List<List<List<Card>>>(); // color - cmc - card
        public static List<List<List<Card>>> SepedCardsByKeywords{get; private set;} = new List<List<List<Card>>>(); // color - keyword - card
        public static void SepByColorId(bool showcards){
                if(Colorids.Count == 0)
                {

                    Colorids.Add("C"); // C for colorless
                    SepedCardsByColorId.Add( new List<Card>());
                    foreach(Card card in ProgFunctions.Allcards)
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
                    for(int i = 0; i < Colorids.Count; i++)
                    {
                        Console.WriteLine("- " + Colorids[i]);
                        Console.WriteLine("NAME - SET - PRINTING - AMOUNT - ID");
                        foreach(Card card in SepedCardsByColorId[i])
                        {
                            Console.WriteLine(card.Special_name + " " + card.Set + " " + card.Printing + " " + card.Amount + " " + ProgFunctions.Allcards.IndexOf(card));
                        }
                        Console.WriteLine("--------");
                        Console.WriteLine(" ");
                    }
                    Console.WriteLine(" ");
                    Console.WriteLine("Enter Any Key To Exit:");
                    Console.ReadLine(); 
                }

        }

        public static void SepByTypeLine()
        {
            if(Colorids.Count == 0)
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

        public static void SepByCMC()
        {
            if(Colorids.Count == 0)
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
        public static void SepByKeyword()
        {
            if(Colorids.Count == 0)
            {
                SepByColorId(false);
            }
            foreach(List<Card> color in SepedCardsByColorId)
            {
                Keywords.Add(new List<string>());
                SepedCardsByKeywords.Add(new List<List<Card>>());
                foreach(Card card in color)
                {
                    foreach(String keyword in card.Keywords)
                    {
                        if(!Keywords[Keywords.Count - 1].Contains(keyword))
                        {
                            Keywords[Keywords.Count - 1].Add(keyword);
                            SepedCardsByKeywords[SepedCardsByKeywords.Count - 1].Add(new List<Card>());
                            SepedCardsByKeywords[SepedCardsByKeywords.Count - 1][Keywords[Keywords.Count - 1].Count-1].Add(card);
                        }
                        else
                        {
                            SepedCardsByKeywords[SepedCardsByKeywords.Count - 1][Keywords[Keywords.Count - 1].IndexOf(keyword)].Add(card);
                        }
                    }
                }
            }
           
        }
        public static void Reset()
        {
            Colorids.Clear();
            Types.Clear();
            Subtypes.Clear();
            Cmcs.Clear();
            Keywords.Clear(); 
        }        
    }
}