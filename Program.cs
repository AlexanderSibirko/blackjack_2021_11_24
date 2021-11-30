(int[,], int[]) SetUp(string[] playersNames, int[] deck) // создает двумерный массив, в котором каждая строка - массив карт игроков, столбец - значение карты
{
    int playersCount = playersNames.Length, deckCount = deck.Length - 1;
    int[,] playersDecks = new int[playersCount, 11]; // массивы колод игроков и крупье, максимальный размер - 11, 
    int[] croupierDeck = new int[11]; // исходя их худшей вариации минимальных карт: 1+1+1+1+2+2+2+2+3+3+3=21
                                      
    for (int i = 0; i < playersCount; i++) // первые две карты игроков
    {
        for (int j = 0; j < 2; j++)
        {
            if (deck[deckCount] == 0) { j--; deckCount--; } // проверка - если при втором раунде используем ту же колоду
            else { playersDecks[i, j] = deck[deckCount]; deck[deckCount] = 0; deckCount--; }
        }
    }

    for (int i = 0; i < 2; i++) // первые две карты крупье
    {
        if (deck[deckCount] == 0) { i--; deckCount--; }
        else { croupierDeck[i] = deck[deckCount]; deck[deckCount] = 0; deckCount--; }
    }
    return (playersDecks, croupierDeck);
}

//Код игры
void RunGame()
{
    (int[,] playersDecks, int[] croupierDeck) = SetUp(playersNames, deck);
}
