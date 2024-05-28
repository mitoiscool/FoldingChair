namespace FoldingChair.Engine.Util;

//  created by mito on 5/27/24

public class UniqueRNG
{
    private readonly Random _rnd = new();
    private readonly HashSet<int> _prevIntegers = new();
    private readonly HashSet<string> _prevStrings = new();
    
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.<>?"; // '.' hehe funny dnspy
    private const int DefaultStringLength = 8;

    public int RandomInt(int max = int.MaxValue)
    {
        return _rnd.Next(max);
    }
    
    public int UniqueRandomInt(int max = int.MaxValue)
    { // having a max on this kind of algo seems a bit iffy
        int i = _rnd.Next(max);

        while (_prevIntegers.Contains(i))
            i = _rnd.Next(max); // ensure no collisions

        _prevIntegers.Add(i);
        return i;
    }

    public string UniqueRandomString(int length = DefaultStringLength)
    {
        string s = RandomString(length);
        while (_prevStrings.Contains(s)) // same as in int, ensure no duplicates. Hopefully this does not get SUPER slow overtime.
            s = RandomString(length);

        _prevStrings.Add(s);
        return s;
    }

    public string RandomString(int length = DefaultStringLength)
    {
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = Chars[random.Next(Chars.Length)];
        }

        return new String(stringChars);
    }
    
}