int questionMaxCount = 5;
int currentQuestion = 0;
int correctAnswers = 0;

void AskQuestion()
{
    currentQuestion++;
    Console.WriteLine($"Question {currentQuestion}:");
    Random rand = new Random();

    int num1 = rand.Next(1, 100);
    int num2 = rand.Next(1, 100);
    int clampedNum = Math.Clamp(num2 / 10, 1, 10);
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
    while (currentQuestion < questionMaxCount)
    {
        AskQuestion();
    }
}

PlayGame();
