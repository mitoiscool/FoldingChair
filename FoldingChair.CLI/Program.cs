// Note: Use sharpprompt as a cool way to navigate through classes/methods to select things to virt

using FoldingChair.CLI;

var log = new ConsoleLogger();

log.Success("Operation completed successfully.");
log.Error("Operation failed.");
log.Fatal("There has been a fatal error.");
log.Warn("There was an issue completing this operation.");
log.Info("This is notable information.");
log.Verbose("This is verbose.");
