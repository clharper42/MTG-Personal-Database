﻿using System;
using System.Text;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MTGRares
{
    class Program
    {
        private const string URL = "https://api.scryfall.com/cards/collection";
        static void Main(string[] args)
        {
            List<ExcelCard> carddb = new List<ExcelCard>(); // REF TO DB
            List<ExcelCard> tcgplayercards = new List<ExcelCard>();
            var csvTable = new DataTable();  
            using (var csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(System.IO.File.OpenRead(@args[0])), true))  
            {  
                csvTable.Load(csvReader);  
            }  

            foreach(DataRow row in csvTable.Rows)
            {
               
                carddb.Add(new ExcelCard{ Name = row.Field<string>(0), Special_name = row.Field<string>(1), Set = row.Field<string>(2), Amount = Convert.ToInt32(row.Field<string>(3)), Printing = row.Field<string>(4), Collector_number = row.Field<string>(5)});
            }


            csvTable = new DataTable();
            using (var csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(System.IO.File.OpenRead(@args[1])), true))  
            {  
                csvTable.Load(csvReader);  
            }  

            foreach(DataRow row in csvTable.Rows)
            {
                
                tcgplayercards.Add(new ExcelCard{ Name = row.Field<string>(2), Special_name = row.Field<string>(1), Set = row.Field<string>(5), Amount = Convert.ToInt32(row.Field<string>(0)), Printing = row.Field<string>(6), Collector_number = row.Field<string>(4)  });
                if(tcgplayercards[tcgplayercards.Count-1].Set.ToLower().Equals("10e") && tcgplayercards[tcgplayercards.Count-1].Printing.Equals("Foil"))
                {
                    tcgplayercards[tcgplayercards.Count-1].Collector_number = tcgplayercards[tcgplayercards.Count-1].Collector_number + "★";
                }              
            }

            if(tcgplayercards.Count != 0)
            {

                foreach(ExcelCard excard in tcgplayercards)
                {
                    ExcelCard match = carddb.Find(c => c.Set.Equals(excard.Set.ToLower()) && c.Collector_number.Equals(excard.Collector_number) && c.Printing.Equals(excard.Printing));
                    if(match != null)
                    {
                        carddb[carddb.IndexOf(match)].Amount = carddb[carddb.IndexOf(match)].Amount + 1;
                    }
                    else
                    {
                        carddb.Add(excard);
                    }
                }
            }


            ICollection<CardSearch> CardsToSearch = new List<CardSearch>();
            string jsonstring;
            StringContent httpContent;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            Cards returncards;
            List<Card> allcards = new List<Card>(); // HAS ALL CARDS AND INFROMATION
            for(int i = 0; i < carddb.Count; i++)
            {
                CardsToSearch.Add(new CardSearch{ Set = carddb[i].Set, Collector_number = carddb[i].Collector_number});
                if(CardsToSearch.Count == 75 || i == carddb.Count - 1)
                {
                    jsonstring = JsonConvert.SerializeObject(new Identifier{ IDs = CardsToSearch});
                    httpContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                    response = client.PostAsync(URL, httpContent).Result;
                    returncards = response.Content.ReadAsAsync<Cards>().Result;
                    allcards.AddRange(returncards.Data);
                    CardsToSearch.Clear();
                }
            }

            allcards.Sort((x, y) => {
                if(x.Name.CompareTo(y.Name) != 0)
                {
                    return  x.Name.CompareTo(y.Name);
                }
                else if(x.Set.CompareTo(y.Set) != 0)
                {
                    return x.Set.CompareTo(y.Set);
                }
                else
                {
                    return x.Collector_number.CompareTo(y.Collector_number);
                }
            });
            carddb.Sort((x, y) => {
                if(x.Name.CompareTo(y.Name) != 0)
                {
                    return  x.Name.CompareTo(y.Name);
                }
                else if(x.Set.CompareTo(y.Set) != 0)
                {
                    return x.Set.CompareTo(y.Set);
                }
                else
                {
                    return x.Collector_number.CompareTo(y.Collector_number);
                }
            });

            for(int i = 0; i < allcards.Count; i++)
            {
                allcards[i].Amount = carddb[i].Amount;
                allcards[i].Special_name = carddb[i].Special_name;
                // allcards[i].Card_num = carddb[i].Card_num;
                if("Foil".Equals(carddb[i].Printing))
                {
                    allcards[i].Printing = "Foil";
                }
                else
                {
                    allcards[i].Printing = "Normal";
                }
            }
 
            ProgFunctions.Allcards = allcards;
            //Work on interactions. Make list(s), only through ID or search also? ? CMC and typeline dispaly together? Search by card color other than idenity. Search by id displays more information about card. Check on 'the list' cards
            //make lists, has name and description. add to. display. print to file. read in file
            //remove function checks lists (Ask if it is okay to remove card if in list)
            //loading in list checks to see if each card has been removed
            //adding to a list checks num of copies????
            Console.WriteLine("Database Loaded");
            Console.WriteLine("-----");
            string selection;
            while(true)
            {
                Console.WriteLine("Select Feature:");
                Console.WriteLine("1 - Search For Card(s)");
                Console.WriteLine("2 - Search By Card Text");
                Console.WriteLine("3 - Filter By Color");
                Console.WriteLine("4 - Filter By Type Line");
                Console.WriteLine("5 - Filter By CMC");
                Console.WriteLine("6 - Filter By Keyword");
                Console.WriteLine("7 - Display Prices");
                Console.WriteLine("8 - Remove Card");
                Console.WriteLine("9 - Exit");
                selection = Console.ReadLine();
                if(Regex.IsMatch(selection,@"^[1-9]$"))
                {
                    
                    if(Convert.ToInt32(selection) == 9)
                    {
                        break;
                    }
                    else
                    {
                        ProgFunctions.ChooseFeature(Convert.ToInt32(selection));
                    }
                }
                Console.Clear();

            }

            System.IO.File.WriteAllText(@args[0],string.Empty);
            System.IO.File.WriteAllText(@args[1],string.Empty);
            using (var writer = new StreamWriter(args[0]))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(allcards);
            }
        }
    }
}