int questionMaxCount = 5;
int currentQuestion = 0;
int correctAnswers = 0;
int difficulty = 0;
int gameHistoryKeyCounter = 0;
List<string> answersLog;
Dictionary<int, List<string>> gameHistory;
gameHistory = new Dictionary<int, List<string>>();
void GameMenu()
{
    answersLog = new List<string>();
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    RainbowText("~~~                              Welcome to the Math Game!                                          ~~~");
    Console.WriteLine();
    Console.WriteLine("~~~    To Play the game, please press type '1' || To see your hi-scores, please type '2'.           ~~~");
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    int playerOption = CheckAnswer();
    if (playerOption == 1) 
    {
        LoadMathGame();
    }
    if (playerOption == 2)
    {
        LoadHiScores();
    }
}

void LoadMathGame()
{
    currentQuestion = 0;
    questionMaxCount = 5;
    correctAnswers = 0; // reset the correct answer count.
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    Console.WriteLine("~~~       You will be asked a series of math questions.                                             ~~~");
    Console.WriteLine("~~~       Try to answer them correctly!                                                             ~~~");
    Console.WriteLine("~~~       There are 4 difficulties, please type the corresponding number for each difficulty:       ~~~");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("~~~       1 - Easy  || 2 - Medium || 3 - Hard  || 4 - Einstein                                      ~~~");
    Console.ResetColor();
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    difficulty = CheckAnswer();
    if (difficulty < 1 || difficulty > 4)
    {
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        Console.WriteLine("~~~              Invalid difficulty selected. Defaulting to difficulty 1.                           ~~~");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        difficulty = 1;
    }
    GameLoop();
}

void LoadHiScores()
{
    foreach (var entry in gameHistory)
    {
        int key = entry.Key;
        List<string> value = entry.Value;
        RainbowText("#######################################################################################################");
        RainbowText("-------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"\nGame #{key}:");
        RainbowText("-------------------------------------------------------------------------------------------------------");
        foreach (string question in value)
        {
            Console.WriteLine($" - {question}");
        }
        RainbowText("#######################################################################################################");
        Console.WriteLine("");
    }
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    Console.WriteLine("~~~                          Please press '1' to go back to the Menu.                               ~~~");
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    int playerResponse = 0;
    while (playerResponse != 1)
    {
        playerResponse = CheckAnswer();
        if (playerResponse == 1)
        {
            GameMenu();
        }
        Console.WriteLine("---------------------------------------------------------------------------------------------------");
        Console.WriteLine("~~~                       Incorrect input, please try again!                                    ~~~");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");
    }
}
void AskQuestion(int level)
{
    currentQuestion++;
    if(currentQuestion == 1) // set the amount of questions to scale with difficulty only once at the start of the game.
    {
        gameHistoryKeyCounter++;
        questionMaxCount *= level;
    }
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    Console.WriteLine($"                                   Question {currentQuestion} || Difficulty {difficulty}:                           ");
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    Random rand = new Random();

    int num1 = rand.Next(1, 25 * level);
    int num2 = rand.Next(1, 25 * level);
    int answer = 0;
    int expectedAnswer = 0;
    int newNum = 0;
    string question = "";

    int questionType = rand.Next(1, 5); // 1:Addition, 2:Subtraction, 3:Multiplication, 4:Division
    switch (questionType)
    {
        case 1:
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"                                       What is {num1} + {num2}?                                   ");
            Console.ResetColor();
            answer = CheckAnswer();

            question = $"What is {num1} + {num2}?\n Answer: {answer}";
            expectedAnswer = num1 + num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"                                       What is {num1} - {num2}?                                   ");
            Console.ResetColor();
            answer = CheckAnswer();
            question = $"What is {num1} - {num2}?\n Answer: {answer}";
            expectedAnswer = num1 - num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"                                       What is {num1} * {num2}?                                   ");
            Console.ResetColor();
            answer = CheckAnswer();
            question = $"What is {num1} * {num2}?\n Answer: {answer}";
            expectedAnswer = num1 * num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 4:
            if(num1 % num2 != 0)
            {
                CheckDividendIsFullyDivisible(num1, out newNum);
                num2 = newNum;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"                                       What is {num1} / {num2}                                    ");
            Console.ResetColor();
            answer = CheckAnswer();
            question = $"What is {num1} / {num2}?\n Answer: {answer}";
            expectedAnswer = num1 / num2;
            GiveFeedback(answer, expectedAnswer);
            break;
    }
    answersLog.Add(question);
}
static int CheckAnswer() // checks the input is a valid value and then attempts to parse it.
{
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int value))
    {
        return value = 0;
    }

    return value;
}
void GiveFeedback(int playerAnswer, int questionAnswer)
{
    if (playerAnswer == questionAnswer)
    {
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"~~~                   Correct! Well done. Please press enter to continue...                        ~~~");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        correctAnswers++;
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        Console.WriteLine("~~~                  Wrong! Better luck next time. Please press enter to continue...                ~~~");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        Console.ReadLine();
    }
}

void CheckDividendIsFullyDivisible(int firstNum, out int newNumber) // checks to see if the dividend is fully divisible by any number based on difficulty
{
    for (int d = (5 * difficulty); d >= 2; d--)
    {
        if (firstNum % d == 0)
        {
            newNumber = d;
            return;
        }
    }

    newNumber = 1;
}
void GameLoop()
{
    while (currentQuestion < questionMaxCount)
    {
        AskQuestion(difficulty);
    }
    gameHistory.Add(gameHistoryKeyCounter, new List<string>(answersLog));
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    Console.WriteLine($"~~~     Congratulations! You've answered {correctAnswers} out of {questionMaxCount} questions!     ~~~");
    Console.WriteLine();
    Console.WriteLine($"~~~           To try again, type '1' || to quit to the menu type '2'.                              ~~~");
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    int playerResponse = CheckAnswer();
    if (playerResponse == 1)
    {
        Console.Clear();
        LoadMathGame();
    }
    else if (playerResponse == 2)
    {
        Console.Clear();
        GameMenu();
    }
}

void RainbowText(string text)
{
    ConsoleColor[] colors = {
        ConsoleColor.Red,
        ConsoleColor.Yellow,
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Blue,
        ConsoleColor.Magenta
    };
    Random rand = new Random();
    foreach (char c in text)
    {
        Console.ForegroundColor = colors[rand.Next(colors.Length)];
        Console.Write(c);
    }
    Console.ResetColor();
    Console.WriteLine();
}
GameMenu();
