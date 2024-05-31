// Note: Use sharpprompt as a cool way to navigate through classes/methods to select things to virt

using System.Diagnostics;
using System.Drawing;
using System.Text;
using AsmResolver.DotNet;
using FoldingChair.CLI;
using Pastel;
using Sharprompt;

args = new string[1];
args[0] = "test.dll";
// idea: STORE OPERANDS IN A SPECIAL ENCRYPTED BINARY DATA REGION, ALLOC ON RUN AND GET A PTR TO IT THEN DECRYPT DATA ON RT

ConsoleLogger log = new ConsoleLogger();

// validate args, assume first is always module path

if(args.Length == 0)
    log.Fatal("Insufficient args provided. Please refer to the documentation.");

ModuleDefinition module = new ModuleDefinition(""); // probably a bad way of initializing this, however my ide hates when I make this null.
Dictionary<string, MethodDefinition> methodNameCache = new Dictionary<string, MethodDefinition>();

try
{
    module = ModuleDefinition.FromFile(args[0]);
}
catch (FileNotFoundException)
{
    log.Fatal($"Could not locate module at path: {args[0]}.");
}
catch (Exception exc)
{
    log.Fatal(exc.Message);
}

log.Info($"Successfully loaded module: {module.Name.ToString().Pastel(Color.LawnGreen)}");

List<MethodDefinition> selectedMethods = new List<MethodDefinition>();

bool shouldTypeSearch = true; // force them to use method selector as of now
Dictionary<string, string[]> typeMethodSelectionCache = new Dictionary<string, string[]>();

while (shouldTypeSearch)
{
    Console.Clear();
    
    log.Info("Selecting methods to virtualize for module: " + module.Name.ToString().Pastel(Color.LawnGreen));
    log.Info(
            typeMethodSelectionCache.SelectMany(x => x.Value).Count()
        .ToString().Pastel(Color.LawnGreen) + " methods selected.");
    log.Warn("Please note that incompatible members are automatically hidden.");
    
    var selectedTypeString = Prompt.Select("Select a type:", module.GetAllTypes()
        .Where(ShouldShowType)
        .Select(x => x.FullName.ToString()) // select all names of types in module
        .OrderBy(c => c, StringComparer.Ordinal), 10 // sort alphabetically
    );

    TypeDefinition selectedType = LocateType(selectedTypeString);

    if (selectedType.Methods.Count == 0)
    {
        log.Error($"Selected type: {selectedType.FullName} contains no methods.");
    }
    else
    {
        // handle method selection
        
        // ensure cached record exists
        typeMethodSelectionCache.TryAdd(selectedTypeString, Array.Empty<string>());
        
        

        var selectedMethodStrings = Prompt.MultiSelect("Select methods within this class to virtualize:",
            selectedType.Methods
                .Where(ShouldShowMethod) // Only include virtualize-able methods
                .OrderBy(c => c.Name.ToString(), StringComparer.Ordinal) // Sort alphabetically
                .Select(GenerateMethodName), // Generate pretty method names to be displayed
            8, 0, int.MaxValue, typeMethodSelectionCache[selectedTypeString]
            
        );

        typeMethodSelectionCache[selectedTypeString] = selectedMethodStrings.ToArray(); // INCLUDE PARAMS FOR OVERLOADS
    }

    // break if need-be
    shouldTypeSearch = Prompt.Confirm("Would you like to continue selecting members?");
}

// resolve methods and add to list only once done selecting

// resolve methods
// note an error will occur if duplicates in these lists occur, potential fix would be to include type-info as ctx

selectedMethods.AddRange(
    typeMethodSelectionCache.SelectMany(x => x.Value).Select(GetMethodFromName)
    );

log.Success($"Resolved {selectedMethods.Count} target methods for virtualization.");



TypeDefinition LocateType(string name)
{
    try
    {
        return module.GetAllTypes().Single(x => x.FullName == name);
    }
    catch
    {
        log.Fatal($"Could not find selected type {name} in module.");
        return null;
    }
}



string GenerateMethodName(MethodDefinition def)
{
    StringBuilder sb = new StringBuilder();

    if (def.IsPublic)
        sb.Append("pub ".Pastel(Color.LawnGreen));

    if (def.IsPrivate)
        sb.Append("priv ".Pastel(Color.Red));

    if (def.IsStatic)
        sb.Append("static ".Pastel(Color.YellowGreen));

    if (!def.Signature.ReturnsValue) // sig could be null, however should be automatically hidden so we would not get to this point
        sb.Append("void ".Pastel(Color.DimGray));

    sb.Append($"{def.Name} ".Pastel(Color.White));

    sb.Append('(');

    sb.Append(
        string.Join(", ", def.Parameters.Select(p => 
        $"{p.ParameterType.Name}".Pastel(p.Index % 2 == 0 ? Color.DarkGray : Color.Gray))));
    
    sb.Append(')');

    var name = sb.ToString();
    methodNameCache.TryAdd(name, def);
    return name;
}

MethodDefinition GetMethodFromName(string name)
{
    if (!methodNameCache.TryGetValue(name, out var def))
        log.Fatal($"Could not resolve cached method {name}");

    Debug.Assert(def != null); // should never be null
    return def;
}

bool ShouldShowMethod(MethodDefinition def)
{
    return def.HasMethodBody && def.Signature != null; // will add more clauses once I think of them. Potential unsupported opcode check??
}

bool ShouldShowType(TypeDefinition type)
{
    return type.Methods.Count > 0;
}