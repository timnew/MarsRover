using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windy.IO
{
    public static class ConsoleExntesion
    {
        public static void SetConsoleColor(ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public static void SetConsoleColor(Tuple<ConsoleColor, ConsoleColor> tuple)
        {
            Console.ForegroundColor = tuple.Item1;
            Console.BackgroundColor = tuple.Item2;
        }

        private static Lazy<Stack<Tuple<ConsoleColor, ConsoleColor>>> colorStack = new Lazy<Stack<Tuple<ConsoleColor, ConsoleColor>>>();
        public static Tuple<ConsoleColor, ConsoleColor> PushConsoleColor()
        {
            var result = Tuple.Create(Console.ForegroundColor, Console.BackgroundColor);
            colorStack.Value.Push(result);
            return result;
        }

        public static Tuple<ConsoleColor, ConsoleColor> PopConsoleColor(bool apply = true)
        {
            if (!colorStack.IsValueCreated || colorStack.Value.Count == 0)
                return null;

            var result = colorStack.Value.Pop();

            if (apply)
            {
                SetConsoleColor(result);
            }

            return result;
        }

        public static void Display(this string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            PushConsoleColor();
            SetConsoleColor(foreground, background);
            Console.WriteLine(message);
            PopConsoleColor();
        }

        public static Answers Ask(this string message, ConsoleColor color, Answers options)
        {
            StringBuilder SB = new StringBuilder();
            SB.AppendLine(message);

            if (options.TestBit(Answers.Retry))
                SB.Append("(R)etry ");

            if (options.TestBit(Answers.Ignore))
                SB.Append("(I)gnore ");

            if (options.TestBit(Answers.Delete))
                SB.Append("(D)elete ");

            if (options.TestBit(Answers.Abort))
                SB.Append("(A)bort ");

            if (options.TestBit(Answers.Quit))
                SB.Append("(Q)uit ");


            message = SB.ToString();

            Answers result = Answers.None;

            Action InvalidInput = () =>
            {
                "Invalid Input\r\n".Display(ConsoleColor.Yellow);
            };
            while (result == Answers.None)
            {
                message.Display(color);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.R:
                        if (options.TestBit(Answers.Retry))
                        {
                            result = Answers.Retry;
                        }
                        else
                        {
                            InvalidInput();
                        }
                        break;
                    case ConsoleKey.I:
                        if (options.TestBit(Answers.Ignore))
                        {
                            result = Answers.Ignore;
                        }
                        else
                        {
                            InvalidInput();
                        }
                        break;
                    case ConsoleKey.D:
                        if (options.TestBit(Answers.Delete))
                        {
                            result = Answers.Delete;
                        }
                        else
                        {
                            InvalidInput();
                        }
                        break;
                    case ConsoleKey.A:
                        if (options.TestBit(Answers.Abort))
                        {
                            result = Answers.Abort;
                        }
                        else
                        {
                            InvalidInput();
                        }
                        break;
                    case ConsoleKey.Q:
                        if (options.TestBit(Answers.Quit))
                        {
                            result = Answers.Quit;
                        }
                        else
                        {
                            InvalidInput();
                        }
                        break;
                    default:
                        InvalidInput();
                        break;
                }
            }

            return result;
        }
    }
}
