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

    
    Console.WriteLine("Welcome to the Math Game!");
    Console.WriteLine("To Play the game, please press type '1' || To see your hi-scores, please type '2'.");
    int playerOption = CheckAnswer();
    if (playerOption == 1) 
    {
        LoadMathGame();
    }
    if (playerOption == 2)
    {
        LoadHiScores();
        //Console.WriteLine("Hi-scores are not yet implemented. Please press enter to return to the main menu.");
        //Console.ReadLine();
        //GameMenu();
    }
    
}

void LoadMathGame()
{
    currentQuestion = 0;
    questionMaxCount = 5;
    correctAnswers = 0; // reset the correct answer count.
    Console.WriteLine("You will be asked a series of math questions.");
    Console.WriteLine("Try to answer them correctly!");
    Console.WriteLine("There are 4 difficulties, please type the corresponding number for each difficulty:");
    Console.WriteLine("1 - Easy (Numbers 1-25) || 2 - Medium (Numbers 25-50) || 3 - Hard (Numbers 50-75) || 4 - Einstein (Numbers 75-100)");
    difficulty = CheckAnswer();
    if (difficulty < 1 || difficulty > 4)
    {
        Console.WriteLine("Invalid difficulty selected. Defaulting to difficulty 1.");
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

        Console.WriteLine($"\nGame #{key}:");

        foreach (string question in value)
        {
            Console.WriteLine($" - {question}");
        }
        Console.WriteLine("");
    }
    Console.WriteLine("Please press '1' to go back to the Menu.");
    int playerResponse = 0;
    while (playerResponse != 1)
    {
        playerResponse = CheckAnswer();
        if (playerResponse == 1)
        {
            GameMenu();
        }
        Console.WriteLine("Incorrect input, please try again!");
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
    
    Console.WriteLine($"Question {currentQuestion} || Difficulty {difficulty}:");
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
            
            Console.WriteLine($"What is {num1} + {num2}?");
            answer = CheckAnswer();
            question = $"What is {num1} + {num2}?\n Answer: {answer}";
            expectedAnswer = num1 + num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 2:
            Console.WriteLine($"What is {num1} - {num2}?");
            answer = CheckAnswer();
            question = $"What is {num1} - {num2}?\n Answer: {answer}";
            expectedAnswer = num1 - num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 3:
            Console.WriteLine($"What is {num1} * {num2}?");
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
            Console.WriteLine($"What is {num1} / {num2}");
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
        Console.WriteLine($"Correct! Well done. Please press enter to continue...");
        correctAnswers++;
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Wrong! Better luck next time. Please press enter to continue...");
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
    Console.WriteLine($"Congratulations! You've answered {correctAnswers} out of {questionMaxCount} questions!");
    Console.WriteLine($"To try again, type '1' || to quit to the menu type '2'.");
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

GameMenu();
