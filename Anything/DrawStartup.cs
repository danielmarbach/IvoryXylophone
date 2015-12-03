using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ColoredConsole;

namespace Anything
{
    public class DrawStartup
    {
        public static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
        {
            if (x > Console.WindowWidth) return;
            if (y > Console.WindowHeight) return;

            var trimLeft = x < 0 ? -x : 0;
            int index = y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            var linesToPrint =
                from line in lines
                let currentIndex = index++
                where currentIndex > 0 && currentIndex < Console.WindowHeight
                select new
                {
                    Text =
                        new String(
                            line.Skip(trimLeft)
                                .Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft))
                                .ToArray()),
                    X = x,
                    Y = y++
                };

            Console.Clear();
            foreach (var line in linesToPrint)
            {
                Console.SetCursorPosition(line.X, line.Y);
                ColorConsole.Write(line.Text.Green());
            }
        }

        public static void IvoryXylophone()
        {
            Console.CursorVisible = false;
            Thread.Sleep(500);

            var arr = new[]
            {
                @"     _   _______ ______   __            ____   ___   _ ____  ",
                @"    | \ | \  ___)  _ \ \ / /           (___ \ / _ \ / |  __) ",
                @"    |  \| |\ \  | |_) ) v / ___  _  __   __) ) | | |- | |__  ",
                @"    |     | > > |  _ ( > < / _ \| |/ /  / __/| | | || |___ \ ",
                @"    | |\  |/ /__| |_) ) ^ ( (_) ) / /  | |___| |_| || |___) )",
                @"    |_| \_/_____)____/_/ \_\___/|__/   |_____)\___/ |_(____/ ",
            };

            DrawText(arr);
        }

        private static void DrawText(string[] arr)
        {
            var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (var y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
            {
                ConsoleDraw(arr, x, y);
                if (arr.Length > 1)
                    Thread.Sleep(100);
            }
        }

        public static void Message(string[] morse)
        {
            Console.CursorVisible = false;

            DrawText(morse);
        }
    }
}
