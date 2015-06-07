using System;
using SimpleMetrics.Model.Database;

namespace SimpleMetrics
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Opening connection");
                using (var context = new MetricsContext())
                {
                    if (context.Database.Exists())
                    {
                        Console.WriteLine("Database exists!");
                    }
                    Console.WriteLine("Listing current applications");
                    foreach (Application application in context.Applications)
                    {
                        Console.WriteLine(application.Name);
                    }
                    Console.WriteLine("Trying to add test application");
                    context.Applications.Add(new Application {Name = "TestApplication"});
                    Console.WriteLine("Saving");
                    context.SaveChanges();

                    foreach (Application application in context.Applications)
                    {
                        Console.WriteLine(application.Name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}