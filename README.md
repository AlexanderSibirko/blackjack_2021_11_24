## Игра "Blackjack"

Описание правил игры можно почитать на вот [тут](https://ru.wikipedia.org/wiki/%D0%91%D0%BB%D1%8D%D0%BA%D0%B4%D0%B6%D0%B5%D0%BA)

### Идея решения

В игре участвуют несколько игроков, для простоты можно взять двух игроков.
Каждый игрок по очереди отвечат на вопрос - "нужна ли Вам еще одна карта?". Если ответ утвердительный, то игроку выдается следующая карта из колоды. Если игрок ответил отрицательно, то у этого игрока больше не спрашиваем. Когда все игроки отказались от новых карт, то начинается подсчет очков. В результате определяется победитель.

Для начала надо определиться как мы будем работать с картами. Напомню, в задаче нельзя пользоваться ничем кроме массивов.
Также важно понять как производить итоговый подсчет очков у каждого игрока, то есть надо как-то узнать сколько очков дают за карты, которые находятся на руках у каждого из игроков.

Сама игра проходит так:

1. Создаем колоду карт.
2. Для игры нам необходима перемешанная колода, поэтому надо в случайном порядке перетасовать карты (или сделать какой-то механизм, чтобы менять порядок карт)
3. Фаза набора карт игроками. Каждый игрок берет по одной карте, до тех пока не скажет что ему больше карты не нужны. Карты выдаются по одной из перемешанной колоды.
4. Фаза подсчета очков. Нам необходимо узнать сколько очков у каждого игрока.
5. Определение победителя в этом раунде.
6. Можно продолжить играть и перейти к следующему раунду (шаг 3).

Попробуйте подумать над решением задачи. В соседней папке есть подсказки, но попробуйте день-два подумать над реализацией и только потом заглядывайте в подсказки. 

Обратите внимание - в подсказках только один из возможных вариантов решений, возможно, вы сможете придумать более интересный и красивый вариант)

Удачи с решением. Через несколько дней выложу еще подсказки, но надеюсь, что они не понадобятся)


Подробное описание правил:

Простещий вариант для начала.
1) Все игроки делают ставки (т.е. игрокам нужно раздать банк и позволить часть своего банка ставить как ставку)
2) После всем игрокам сдаётся по 2 карты (которые видят все игроки). Затем Крупье сдаёт себе 2 карты (1 из которых видна, другая не видна игрокам)
3) Далее по очереди все игроки опрашиваются, на добор карт или хватит (если сумма очков карт = 21 то не спрашиваем, если сумма выше 21, то ставка сгорает в пользу казино)
4) После того как опрошен последний игрок, крупье открывает вторую карту и добирает по принципу меньше 17 очков добирать, если больше то стоп.
5) Происходит сравнивание карт игроков и их очков с очками крупье
5а) При подсчёте очков все карты с картинками = 10 очкам, карты 2-10 = своей цифре, Туз = 1 или 11 (на выбор). 
5б) Если первые две карты составляют Туз + 10 (в сумме 21 с двух карт) комбинация называется Блэк-джек и это влияет на выигрыш по ставкам !

6) Если очков меньше ставка сгорает в пользу казино (т.е. был банк 500, ставку сделали 100. Банк 400. Проиграли =( так и остаёмся при 400)
6а) Если очков у игрока и крупье равное кол-во, то игрок возвращает свою ставку (т.е. был банк 500, ставку сделали 100. Банк 400, выиграли 400+100=500))
6б) Если у игрока больше очков чем у крупье, то выигрыш 1 к 1, т.е. при ставке 100, игрок получает ставку обратно + 100 от "казино" (т.е. был банк 500, ставку сделали 100. Банк 400, выиграли 400+100+100=600)
6в) Если игрок выиграл через БлэкДжек (а у крупье небыло ответного Блэкджека), то игрок выигрвает 3 к 2, или другими словами 150% ставки (т.е. был банк 500, ставку сделали 100. Банк 400, выиграли 400+100+150=650)


1. Определяется кол-во игроков (Представляются по именам?) - массив игроков
2. Все игроки получают некий банк (?) - массив банков игроков

Игра
3. Игроки делают ставки
4. Все получают по 2 карты (у крупье 1 карта открытая, вторая закрытая)
5. Все игроки по очереди принимают решения о доборе картэ
    5а - добор карт прекращется елси перебор или 21 (при переборе ставка сразу сгорает)
6. Крупье, вскрывает вторую карту и добирает карты по алгоритму.
7. Определяются победители и проигравшие (происходит зачисления банков)
Раунд завершён



Условия окончания игры для игрока: 
1) банк игрока равен 0?

// 2) игрок принял решение выйти?

------------------------
(string[] playersNames, int deckLength, int[] balance) Greetings()
{
    Запрашивает 
        1 размер колоды 
        2 имена игроков через запятую, пересобирает строку в массив игроков, 
          длина массива будет количеством игроков
        3 размер счёта на старте
}
------------------------------
(int[] bets) Betting(string[] playersNames, int[] balance)
{
    Запрашивает размеры ставок поочерёдно у игроков
    проверяет хватает ли баланса, отдаёт изменённый баланс
}
-----------------------------
(int[] deck) Deck(int deckLength)
{
    Инициализирует случайную колоду
}
-------------------------------