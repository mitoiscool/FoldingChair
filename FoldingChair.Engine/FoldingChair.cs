using AsmResolver.DotNet;

namespace FoldingChair.Engine;

public class FoldingChair
{
    public FoldingChair(Context ctx)
    {
        Context = ctx;
    }
    
    public Context Context;


    public void MarkMethod(MethodDefinition method)
    {
        
    }

    public void MarkFinished()
    {
        
    }
    
}