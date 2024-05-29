using System.Drawing;
using FoldingChair.Engine.Util;
using Pastel;

namespace FoldingChair.CLI;

public class ConsoleLogger : ILogger
{ // use pastel/datetime
    
    string GetBase(string notifier)
    {
        return $"[{DateTime.Now:HH:mm:ss}] [{notifier}] ".Pastel(ConsoleColor.White);
    }
    
    public void Success(string msg)
    {
        Console.WriteLine(
            GetBase(
                "SUCCESS".Pastel(Color.LimeGreen)
                )
            + msg
            );
    }

    public void Warn(string msg)
    {
        Console.WriteLine(
            GetBase(
                "WARN".Pastel(Color.Yellow)
            )
            + msg
        );
    }

    public void Error(string msg)
    {
        Console.WriteLine(
            GetBase(
                "ERROR".Pastel(Color.Red)
            )
            + msg
        );
    }

    public void Fatal(string msg)
    {
        Console.WriteLine(
            GetBase(
                "FATAL".Pastel(Color.DarkRed)
            )
            + msg
        );
    }

    public void Verbose(string msg)
    {
        Console.WriteLine(
            GetBase(
                "VERBOSE".Pastel(Color.Aqua)
            )
            + msg
        );
    }

    public void Info(string msg)
    {
        Console.WriteLine(
            GetBase(
                "INFO".Pastel(Color.DodgerBlue)
            )
            + msg
        );
    }
}