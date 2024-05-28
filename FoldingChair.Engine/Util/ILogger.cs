namespace FoldingChair.Engine.Util;

public interface ILogger
{
    public void Success(string msg);
    public void Warn(string msg);
    public void Error(string msg);
    public void Fatal(string msg);
}