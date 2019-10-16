using System;
using System.Threading;
using CustomTypes;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShellRunspaceCustomType
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Person in main thread
            Console.WriteLine("\nCreating Person in therad {0}", Thread.CurrentThread.ManagedThreadId);
            Person me = new Person("Benjamin", "Elser", 32);
            Console.WriteLine(me);
            Console.WriteLine("------------------------------\n");

            // Setup Runspace to load custom types
            InitialSessionState iss = InitialSessionState.CreateDefault();
            SessionStateTypeEntry sste = new SessionStateTypeEntry(@"./CustomTypes.cs");
            iss.Types.Add(sste);
            
            using (PowerShell ps = PowerShell.Create(iss))
            {
                // create type in runspace from custom type Person invoking ToString() method
                var results = ps.AddScript("using namespace CustomTypes;$t = [Person]::new('Benjamin', 'Elser', 32); $t.ToString(); [System.Threading.Thread]::CurrentThread.ManagedThreadId").Invoke();
                Console.WriteLine("Creating Person in Runspace and getting TheadID");
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("------------------------------\n");
                ps.Commands.Clear();
                
                // Explicitly Cast return object to type Class
                Console.WriteLine("Creating Person in runspace...");
                var psobjects = ps.AddScript("using namespace CustomTypes; $t = [Person]::new('Adam', 'Sandler', 32); $t; [System.Threading.Thread]::CurrentThread.ManagedThreadId").Invoke();
                Person adamSandler = (Person)psobjects[0].ImmediateBaseObject;
                int rsthread = (int)psobjects[1].ImmediateBaseObject;
                System.Console.WriteLine("Person created in Runspace {0} --------- Runspace Thread {1}", adamSandler, rsthread);
                Console.WriteLine("------------------------------\n");
                Console.WriteLine("Explicitly casted Person object from Runspace first name: {0} in thread {1}\n", adamSandler.FirstName, Thread.CurrentThread.ManagedThreadId);

                // Creating the second custom type Pizza
                Console.WriteLine("------------------------------\n");
                ps.Commands.Clear();
                var pizzaRs = ps.AddScript("using namespace CustomTypes; [system.Collections.ArrayList]$t=@('cheee','mushrooms'); $p = [Pizza]::new([PizzaSize]::L, $t); $p; [System.Threading.Thread]::CurrentThread.ManagedThreadId").Invoke();
                Pizza mushroomPizza = (Pizza)pizzaRs[0].ImmediateBaseObject;
                int pizzaRSThrea = (int)pizzaRs[1].ImmediateBaseObject;
                Console.WriteLine("Explicitly casted Pizza object from Runspace: {0} in Runspace thread {1}\n", mushroomPizza, pizzaRSThrea);

            }

        }
            
    }
}
