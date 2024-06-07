using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Metadata.Tables.Rows;

namespace FoldingChair.Engine;

public class MethodVirtualizer
{
    public MethodVirtualizer(Context ctx)
    {
        _ctx = ctx;
        _pipeline = new StepExecuter[]
        {
            TransformIL,
            BuildCFG,
            BuildDFA,
            BuildVMIR,
            EncodeVMIL,
            Deinitialize
        };

    }

    private readonly Context _ctx;
    private MethodDefinition? _targetMethod;
    private bool _continueVirtualizing = true;

    private readonly StepExecuter[] _pipeline;

    public void Virtualize(MethodDefinition method)
    {
        // populate pipeline
        _targetMethod = method;

        foreach (var pipelineStep in _pipeline)
        {
            pipelineStep();
            if(!_continueVirtualizing)
                break; // break if error virtualizing
        }
    }


    void TransformIL()
    {
        
            
            
    }

    void BuildCFG()
    {
        
    }

    void BuildDFA()
    {
        
    }

    void BuildVMIR()
    {
        
    }

    void EncodeVMIL()
    {
        
    }

    void Deinitialize()
    {
        
    }
    
    
}

public delegate void StepExecuter();