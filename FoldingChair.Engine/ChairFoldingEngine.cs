using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using AsmResolver.DotNet;

namespace FoldingChair.Engine;

public class ChairFoldingEngine
{
    public ChairFoldingEngine(Context ctx)
    {
        _context = ctx;
        _methodVirtualizer = new MethodVirtualizer(ctx);
    }
    
    private readonly Context _context;
    private readonly MethodVirtualizer _methodVirtualizer;

    public void MarkMethod(MethodDefinition method)
    {
        var sw = Stopwatch.StartNew();
        
        _methodVirtualizer.Virtualize(method);
        
        sw.Stop();

        _context.Logger.Success($"Virtualized method {method.FullName} in {sw.ElapsedMilliseconds} ms");
    }

    public void Fold() // finish everything, silly folding chair convention
    {
        
    }
    
}