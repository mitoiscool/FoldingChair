using AsmResolver.DotNet;
using FoldingChair.Engine.RT;
using FoldingChair.Engine.Util;

namespace FoldingChair.Engine;

public class Context
{
    public Context(ModuleDefinition mod, Settings settings, ModuleDefinition runtimeModule)
    {
        Module = mod;
        Settings = settings;
        RuntimeModule = new RuntimeModule(runtimeModule);
    }

    public RuntimeModule RuntimeModule;
    public ModuleDefinition Module;
    public Settings Settings;

    private UniqueRNG _rng = new UniqueRNG();
}