using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CliTools.JumpList
{
    public class Program
    {
        private const string ItemSeparator = ">";
        private const string RecordSeparator = ",";
        private static readonly Dictionary<string, string> Lookup = new Dictionary<string, string>();

        private static void Main(string[] args)
        {
            // args = new string[]{"-l"};
            // args = new string[]{"-a", "project"};
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments passed.");
                return;
            }
            Execute(args);
        }

        private static void Execute(string[] args)
        {
            LoadJumpList();

            var command = args[0];
            switch (command.ToLower())
            {
                case "--add":
                case "-a":
                    Add(args);                    
                    return;
                case "--rm":
                case "-r":
                    Remove(args);
                    return;
                case "--list":
                case "-l":
                    Display();                    
                    return;
                default:
                    Jump(args[0]);
                    return;
            }
        }

        private static void Add(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Must provide at least one argument for add flag.");
                return;
            }
            var key = args[1];
            var value = Directory.GetCurrentDirectory();
            Lookup[key] = value;
            SaveJumpList();
        }

        private static void Remove(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Must provide at least one argument for rm flag.");
                return;
            }

            if (!Lookup.ContainsKey(args[1])) return;

            Lookup.Remove(args[1]);
            SaveJumpList();
        }

        private static void Display()
        {
            foreach (var kvp in Lookup)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }

        private static void LoadJumpList()
        {
            var path = GetDbPath();
            if(!File.Exists(path))
            {
                return;
            }
            var data = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            var rows = data.Split(RecordSeparator);
            foreach (var row in rows.Where(a => !string.IsNullOrWhiteSpace(a)))
            {
                var record = row.Split(ItemSeparator);
                if (record.Length != 2)
                {
                    throw new Exception($"Invalid record data: {record}");
                }
                var key = record[0];
                var value = record[1];
                Lookup[key] = value;
            }
        }

        private static void Jump(string key)
        {            
            if (Lookup.TryGetValue(key, out var path))
            {
                Console.WriteLine(path);
            }
        }

        private static void SaveJumpList()
        {
            var data = Lookup
                .Aggregate(string.Empty, (current, kvp) => current + $"{kvp.Key}{ItemSeparator}{kvp.Value}{RecordSeparator}");
            var dbPath = GetDbPath();
            File.WriteAllText(dbPath, data);
        }

        private static string GetDbPath()
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ".jumpdb");
        }
    }
}
