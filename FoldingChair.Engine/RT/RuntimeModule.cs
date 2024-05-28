using AsmResolver.DotNet;

namespace FoldingChair.Engine.RT;

public class RuntimeModule
{
    public RuntimeModule(ModuleDefinition mod)
    {
        Module = mod;
    }

    public ModuleDefinition Module;
    
    
    
}