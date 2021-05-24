/*
 * We can remove ceremony when implementing the entry point of our application my removing the following:
 *
 * 1. Namespace
 * 2. Program class
 * 3. Main method
 *
 * More info:
 *
 * 1. Only one top-level file - An application must have only one entry point, so a project can have only one file with top-level statements.
 * 2. No other entry points - You can write a Main method explicitly, but it can't function as an entry point.
 * 3. Using directives - If you include using directives, they must come first in the file.
 * 4. Global namespace - Top-level statements are implicitly in the global namespace.
 * 5. args - Top-level statements can reference the args variable to access any command-line arguments that were entered.
 * 6. await - You can call an async method by using await.
 *
 * For more info, see https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements
 */

using System.Threading.Tasks;

System.Console.WriteLine(args[0]);
var helloMessage = await Task.FromResult("Hello World");
System.Console.WriteLine(helloMessage);