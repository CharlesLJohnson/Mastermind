using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MasterMind
{
    class Program
    {

        static void Main(string[] args)
        {
            string userEntry = null, mastermindAnswer = null;
            char[] mastermindWork = null, userEntryWork;
            string[] incrementedAttempt = { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th and final" };
            string currentPrompt = null, hintPluses = null, hintMinuses = null, hintMessage;

            Boolean validCharacters = false, validEntry = true;

            Random randomInteger = new Random();

            for (int i = 0; i < 4; i++)
                mastermindAnswer = mastermindAnswer + randomInteger.Next(1, 6).ToString();

            Console.WriteLine("Mastermind!");
            Console.WriteLine();

            Console.WriteLine("Enter 4 digits between 1 and 6 to work out the answer");
            Console.WriteLine();
            Console.WriteLine("The returned \"+\" characters will represent digits that are correct and in the right position");
            Console.WriteLine("The returned \"-\" characters will represent digits that are correct but in the wrong position");
            Console.WriteLine("Nothing is shown for digits that are not part of the answer");
            Console.WriteLine("The \"+\" characters are listed first follwed by the \"-\" characters");
            Console.WriteLine("The returned \"+-\" characters do not correspond to the entered digits of the answer");
            Console.WriteLine();
            Console.WriteLine("You have 10 attempts to find the answer");
            Console.WriteLine();

            for (int numberedTry=0; numberedTry < 10; numberedTry++)
            {
                currentPrompt = $"{ incrementedAttempt[numberedTry] } attempt:  Please enter four digits between 1 and 6 or \"xxxx\" to quit";
                Console.WriteLine(currentPrompt);
                userEntry = Console.ReadLine();
                userEntry = Regex.Replace(userEntry, @"\s+", "");

                while (userEntry.ToUpper() != "XXXX")
                {
                    validEntry = true;
                    if (userEntry.Length > 4)
                    {
                        Console.WriteLine("More than 4 characters were entered");
                        validEntry = false;
                    }
                    else if (userEntry.Length < 4)
                    {
                        Console.WriteLine("Fewer than 4 characters were entered");
                        validEntry = false;
                    }
                    else
                    { 
                        validCharacters = System.Text.RegularExpressions.Regex.IsMatch(userEntry, "^[1-6]*$");
                        if (!validCharacters)
                        {
                            Console.Write("Only four digits between 1 and 6 may be entered.  ");
                            validEntry = false;
                        }
                    }

                    if (!validEntry)
                    {
                        Console.WriteLine();
                        Console.Write($"Still the {incrementedAttempt[numberedTry]} attempt.  ");
                        Console.WriteLine($"Please enter four digits between 1 and 6 or \"xxxx\" to quit");
                        Console.WriteLine();
                        userEntry = Console.ReadLine();
                    }
                    else
                        break;
                }

                if (userEntry.ToUpper() == "XXXX")
                {
                    Console.WriteLine();
                    Console.WriteLine($"The answer is { mastermindAnswer }");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to terminate");
                    Console.ReadKey();
                    return;
                }

                if (userEntry == mastermindAnswer)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Congratulations that's the answer!!!!");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to terminate");
                    Console.ReadKey();
                    return;
                }

                hintPluses = null;
                mastermindWork = mastermindAnswer.ToCharArray(0,4);
                userEntryWork = userEntry.ToCharArray(0,4);
                for (int i = 0; i < 4; i++)
                { 
                    if (userEntry[i].ToString() == mastermindWork[i].ToString())
                    { 
                        hintPluses = hintPluses + "+";
                        userEntryWork[i] = ' ';
                        mastermindWork[i] = ' ';
                    }
                }
                
                hintMinuses = null;
                for (int i = 0; i < 4; i++)
                {
                    if (userEntryWork[i] == ' ')
                        continue;

                    if (mastermindWork.Contains(userEntryWork[i]))
                    {
                        hintMinuses = hintMinuses + "-";
                        mastermindWork[Array.IndexOf(mastermindWork, userEntryWork[i])] = ' ';
                    }

                }

                if (hintPluses == null && hintMinuses == null)
                    hintMessage = "Hint:  None of the entered characters are in the answer";
                else
                    hintMessage = "Hint:  " + hintPluses + hintMinuses;

                Console.WriteLine($"{hintMessage}");
                Console.WriteLine();

            }

            Console.WriteLine();
            Console.Write($"The Mastermind answer was { mastermindAnswer}");
            Console.WriteLine();
            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();
        }
    }
}
