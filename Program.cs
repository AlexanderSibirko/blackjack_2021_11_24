////////////////////////////////////////////////////////////////////////////////////////////////
// Измененный метод по другому алгоритму
// Входящие аргументы: numCards- количество карт в колоде, numDecks - количество колод;
// Метод возвращает массив deck содержащий, в зависимости от типа колоды, перемешанные значения карт 
// для "русской колоды" состоящей из 36 карт значения от 6 до 14, с 6 до 10 по номиналу, 11 - туз, 12, 13, 14 соответственно валет, дама, король
// для стандартной или "французской" колоды значения карт от 2 до 14, с 2 до 10 по номиналу, 11 - туз, 12, 13, 14 соответственно валет, дама, король

int[] Mixing(int numCards, int numDecks)
{
    int j, temp, fromValueCard; int count = 0; int[] deck = new int[numCards * numDecks];

    if (numCards == 52) fromValueCard = 2; else fromValueCard = 6;

    for (int k = 0; k < 4 * numDecks; k++) // создаёт колоду
    { for (int n = fromValueCard; n < 15; n++) { deck[count] = n; count += 1; } }

    for (int i = 0; i < deck.Length; i++) // перемешивает колоду
    { temp = deck[i]; j = new Random().Next(i, deck.Length); deck[i] = deck[j]; deck[j] = temp; }

    return deck;
}
////////////////////////////////////////////////////////////////////////////////////////////////

int RequestNumber(string words) // ввод чисел с проверкой
{
    while (true)
    {
        Console.Write(words);
        if (int.TryParse(Console.ReadLine(), out int num)) return num;
        else Console.WriteLine("Что-то вы не то ввели, давайте-ка снова.");
    }
}

(string[] playersNames, int numDecks, int[] balance) Greetings()
{
    Console.Write("Введите имена игроков через запятую: ");
    string[] playersNames = Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries); //Прошлый код давал ошибку вводя строку "Маша, Паша, Саша", получали массив {"Маша","","Паша","","Cаша"}. Теперь можно вводить как "Маша Паша Саша" так и "Маша, Паша,Саша" и т.п.
    int[] balance = new int[playersNames.Length];
    int numDecks = RequestNumber("Укажите начальный баланс игорков: ");
    for (int i = 0; i < balance.Length; i++)
    {
        balance[i] = numDecks;
    }
    numDecks = RequestNumber("Сколько колод возьмём: ");
    return (playersNames, numDecks, balance);
}

int[] MakeBets(string[] playersNames, int[] balance) //опрос всех игроков о их ставке, количество игроков и их балансов должны быть массивы одинакового размера
{
    int playersCount = playersNames.Length;
    int[] betsArray = new int[playersCount];
    for (int i = 0; i < playersCount; i++)
    {
        betsArray[i] = AskForBet(playersNames[i], balance[i]);
    }
    return betsArray;
}

int AskForBet(string playerName, int playerBalance) //метод опроса отдельного игрока, переспрашивает пока ставка не будет больше 0 и меньше баланса.
{
    while (true)
    {
        int betAmount = RequestNumber($"{playerName} у вас {playerBalance} фишек, делайте вашу ставку: ");
        if (betAmount <= playerBalance && betAmount > 0) return betAmount;
        else Console.WriteLine($"Ставка не может быть меньше 1 или больше количества ваших фишек.");
    }
}

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
    (string[] playersNames, int numDecks, int[] balance) = Greetings(); //передаём результат кортежа в переменные
    int[] bets = MakeBets(playersNames, balance); //заполняем массив принятых ставок
    int[] deck = Mixing(52, numDecks);
    (int[,] playersDecks, int[] croupierDeck) = SetUp(playersNames, deck);
}

RunGame(); //запускаем игру



// ---------Первичный код Рустема-----------------
// int[] Mixing() // Перетасовка колоды карт, метод возвращает массив с 52-мя значениями карт,
// {              // размещенных на случайных позициях 
//     int[] deck = new int[52];
//     for (int i = 0; i < deck.Length; i++)
//     {
//         deck[i] = new Random().Next(1, 14);
//         if (CheckNum(deck[i], deck)) i -= 1;
//     }

//     bool CheckNum(int arg, int[] deck)
//     {
//         int count = 0;
//         for (int i = 0; i < deck.Length; i++) if (arg == deck[i]) count += 1;
//         return count > 4;
//     }
//     return deck;
// }


// (int[], int[]) setUp(int[] deck)            // набор карт игроком и крупье
// {                                           // метод возвращает два массива со значениями карт
//     int[] player = new int[deck.Length];    // массив игрока
//     int[] croupier = new int[deck.Length];  // массив крупье
//     int flag = 0;                           // переменная flag хранит позицию в массиве колоды
//                                             // на которой закончил брать карты игрок
//     player[0] = deck[deck.Length - 1];      // на первую (нулевую) позицию массива игрока записываем значение карты, которая находится в колоде на последней позиции
//     deck[deck.Length - 1] = 0;              // последнюю позицию массива колоды обнуляем

//     Console.WriteLine($"Ваша первая карта: {WhatIsCard(player[0])}");
//     if ((!Overload(player)) && player[0] == 1) player[0] = 111;

//     for (int i = 1; i < deck.Length - 1; i++)
//     {
//         Console.WriteLine("Ещё? (Да - 'Y', Нет - любая клавиша): ");
//         if (WaitUser())
//         {
//             player[i] = deck[deck.Length - 1 - i];
//             deck[deck.Length - 1 - i] = 0;
//             Console.WriteLine($"Ваша {i + 1}-я карта: {WhatIsCard(player[i])}");

//             if ((!Overload(player)) && player[i] == 1) player[i] = 111;
//             if (Overload(player))
//             {
//                 Console.WriteLine("У Вас перебор!");
//                 flag = i + 1;
//                 i = deck.Length - 1;
//             }

//         }
//         else
//         {
//             flag = i;
//             i = deck.Length - 1;
//         }
//     }

//     croupier[0] = deck[deck.Length - flag - 1];   // на первую позицию массива крупье (за минусом нулевых позиций, которые забрал игрок)
//                                                   // записываем значение карты, которая находится в колоде на последней позиции
//     deck[deck.Length - flag - 1] = 0;             // последнюю позицию массива колоды ( за минусом нулевых) обнуляем

//     Thread.Sleep(1500);                         // задержка                         
//     Console.Clear();

//     Console.WriteLine($"Первый ход крупье. Выпала карта: {WhatIsCard(croupier[0])}");
//     if (croupier[0] == 1) croupier[0] = 111;    //если выпал туз, записываем в массив 111

//     for (int j = 1; j < deck.Length - 1; j++)
//     {
//         if (OverloadCroupier(croupier) < 17)
//         {
//             Thread.Sleep(1500);
//             Console.WriteLine($"{j + 1}-й xод крупье.");
//             croupier[j] = deck[deck.Length - (flag + 1) - j];
//             deck[deck.Length - (flag + 1) - j] = 0;
//             Console.WriteLine($"Выпала карта: {WhatIsCard(croupier[j])}");

//             if (croupier[j] == 1) croupier[j] = 111;
//             if (OverloadCroupier(croupier) > 21)
//             {
//                 Console.WriteLine("У крупье перебор!");
//                 j = deck.Length - 1;
//             }

//         }
//         else
//         {
//             j = deck.Length - 1;
//         }
//         Thread.Sleep(1500);
//     }

//     bool Overload(int[] collection)      // проверка "перебора" у игрока
//     {
//         int sum = 0;
//         for (int i = 0; i < collection.Length; i++)
//         {
//             if (collection[i] < 11) sum += collection[i];
//             else if (collection[i] == 111) sum += collection[i] - 100;
//             else sum += 10;
//         }
//         return sum > 21;
//     }

//     int OverloadCroupier(int[] collection)      // проверка "перебора" у крупье
//     {
//         int sum = 0;
//         for (int i = 0; i < collection.Length; i++)
//         {
//             if (collection[i] < 11) sum += collection[i];
//             else if (collection[i] == 111) sum += collection[i] - 100;
//             else sum += 10;
//         }
//         return sum;
//     }

//     bool WaitUser()                             //метод (процедура) ожидание ответа пользователя
//     {
//         return Console.ReadLine().ToLower() == "y";     //если нажата "y", то возвращает значение true
//     }

//     string WhatIsCard(int arg)
//     {
//         string ValueCard = string.Empty;
//         if (arg == 1) ValueCard = "Туз";
//         if (arg == 2) ValueCard = "Двойка";
//         if (arg == 3) ValueCard = "Тройка";
//         if (arg == 4) ValueCard = "Четверка";
//         if (arg == 5) ValueCard = "Пятерка";
//         if (arg == 6) ValueCard = "Шестерка";
//         if (arg == 7) ValueCard = "Семерка";
//         if (arg == 8) ValueCard = "Восьмерка";
//         if (arg == 9) ValueCard = "Девятка";
//         if (arg == 10) ValueCard = "Десятка";
//         if (arg == 11) ValueCard = "Валет";
//         if (arg == 12) ValueCard = "Дама";
//         if (arg == 13) ValueCard = "Король";
//         return ValueCard;
//     }

//     return (player, croupier);
// }

// Console.Clear();
// var score = setUp(Mixing());
// int playerScore = 0;
// int croupierScore = 0;

// for (int i = 0; i < score.Item1.Length; i++)
// {
//     if (score.Item1[i] < 11) playerScore += score.Item1[i];
//     else if (score.Item1[i] == 111) playerScore += score.Item1[i] - 100;
//     else playerScore += 10;
//     if (score.Item2[i] < 11) croupierScore += score.Item2[i];
//     else if (score.Item2[i] == 111) croupierScore += score.Item2[i] - 100;
//     else croupierScore += 10;
// }
// Console.Clear();
// Console.WriteLine($"Счет игрока: {playerScore}, счет крупье: {croupierScore} ");


