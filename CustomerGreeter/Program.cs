namespace CustomerGreeter
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileEntries = new TextParser().ReadEntriesInFile();
            var customers = new Translator().TranslateEntriesToCustomers(fileEntries);
            var customersWithinDistance = new OfficeManager().GetCustomersWithinDistance(customers, 100);
            new TextWriter().WriteToFile(customersWithinDistance);

        }
    }
}
