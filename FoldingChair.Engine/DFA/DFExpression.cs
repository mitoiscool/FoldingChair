using AsmResolver.PE.DotNet.Cil;

namespace FoldingChair.Engine.DFA;

public class DFExpression
{
    public DFExpression(CilCode code, object? operand, DFExpression[] args)
    {
        OpCode = code;
        Operand = operand;
        Arguments = args;
    }

    public DFExpression(CilCode code, DFExpression[] args)
    {
        OpCode = code;
        Arguments = args;
    }
    
    public CilCode OpCode;
    public object? Operand;

    public DFExpression[] Arguments;
}