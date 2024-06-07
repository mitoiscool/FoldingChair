// See https://aka.ms/new-console-template for more information

Console.WriteLine(Add(10, 12));
Console.WriteLine(Sub(10, 12));
Console.WriteLine(Mul(10, 12));
Console.WriteLine(Div(10, 12));

static int Add(int i1, int i2)
{
    return i1 + i2;
}
        
static int Sub(int i1, int i2)
{
    return i1 - i2;
}
        
static int Mul(int i1, int i2)
{
    return i1 * i2;
}
        
static int Div(int i1, int i2)
{
    return i1 / i2;
}
