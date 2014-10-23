using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4._Herencia
{
    static class Program
    {
        static void Main()
        {
            WorkItem item = new WorkItem(
                            "Fix Bugs",
                            "Fix all bugs in my source code branch",
                            new TimeSpan(3, 4, 0, 0));

            ChangeRequest change = new ChangeRequest("Change design of base class",
                                                     "Add members to base class",
                                                     new TimeSpan(4, 0, 0),
                                                     1);

            Console.WriteLine(item.ToString());

            // ChangeRequest inherits WorkItem's override of ToString
            Console.WriteLine(change.ToString());

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
