int questionMaxCount = 5;
int currentQuestion = 0;
int correctAnswers = 0;
int difficulty = 0;

void GameMenu()
{
    Console.WriteLine("Welcome to the Math Game!");
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
}
void AskQuestion(int level)
{
    currentQuestion++;
    Console.WriteLine($"Question {currentQuestion} || Difficulty {difficulty}:");
    Random rand = new Random();

    int num1 = rand.Next(1, 25 * level);
    int num2 = rand.Next(1, 25 * level);
    int clampedNum = Math.Clamp(num2 / (5 * level), 1, (5 * level));
    int answer = 0;
    int expectedAnswer = 0;
    int newNum = 0;

    int questionType = rand.Next(1, 5); // 1:Addition, 2:Subtraction, 3:Multiplication, 4:Division
    switch (questionType)
    {
        case 1:
            Console.WriteLine($"What is {num1} + {num2}?");
            answer = CheckAnswer();
            expectedAnswer = num1 + num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 2:
            Console.WriteLine($"What is {num1} - {num2}?");
            answer = CheckAnswer();
            expectedAnswer = num1 - num2;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 3:
            Console.WriteLine($"What is {num1} * {clampedNum}?");
            answer = CheckAnswer();
            expectedAnswer = num1 * clampedNum;
            GiveFeedback(answer, expectedAnswer);
            break;
        case 4:
            if(num1 % clampedNum != 0)
            {
                CheckDividendIsFullyDivisible(num1, out newNum);
                clampedNum = newNum;
            }
            Console.WriteLine($"What is {num1} / {clampedNum}");
            answer = CheckAnswer();
            expectedAnswer = num1 / clampedNum;
            GiveFeedback(answer, expectedAnswer);
            break;
    }
}
static int CheckAnswer()
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
        Console.WriteLine("Correct! Well done. Please press enter to continue...");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Wrong! Better luck next time. Please press enter to continue...");
        Console.ReadLine();
    }
}

void CheckDividendIsFullyDivisible(int firstNum, out int newNumber)
{
    for (int d = 1; d <= 10; d++)
    {
        if (firstNum % d == 0)
        {
            newNumber = d;
            return;
        }
    }

    newNumber = 1;
}
void PlayGame()
{
    GameMenu();
    while (currentQuestion < questionMaxCount)
    {
        AskQuestion(difficulty);
    }
}

PlayGame();
