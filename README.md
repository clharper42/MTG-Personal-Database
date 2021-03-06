# MTG-Personal-Database
This program is a way to create and interact with your personal Magic The Gathering card collection.

First you scan cards using the TCGPlayer app as a way to speed up the process of entering cards into the database. You take the csv file that the app gives you and pass that in as an argument into the program.

Once the program has processed the cards it uses the information gathered to make a request to the [Scryfall API](https://scryfall.com/docs/api) to pull more information about the cards that the TCGPlayer file does not provide.

With the infromation gathered from Scryfall the program provides features such as searching and filtering (features are being worked on).

Once the program has gathered information from Scryfall the program updates a csv file with the cards' information for which it will pull from and update when the program is ran again.

Set - Collector Number - Printing(Normal/Foil) is the unique identifier for a card 

Some cards may have issues being entered due to TCGPlayer and Scryfall having different ways of identifying cards. Fixing when I notice a discrepancy

[Example of features as of first commit](https://streamable.com/60due7)

[Example of type line features](https://streamable.com/asxu4h)

[Example of CMC features](https://streamable.com/zphuo9)

[Example of price feature](https://streamable.com/3lgkhm)

[Example of keyword features](https://streamable.com/is4wcl)

[Example of remove card feature](https://streamable.com/cip868)

[Example of list features](https://streamable.com/018cpa)
