using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb3_DesignPatterns_Architecture
{
    // Definierar ett interface för varma drycker
    public interface IWarmDrink
    {
        void Consume(); // Metod för att konsumera drycken
    }

    // Implementerar en specifik varm dryck, i detta fall vatten
    internal class Water : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Hot water is served."); // Utskrift vid konsumtion av vatten
        }
    }

    // Implementerar en specifik varm dryck, i detta fall kaffe
    internal class Coffee : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Coffee is served."); // Utskrift vid konsumtion av kaffe
        }
    }

    // Implementerar en specifik varm dryck, i detta fall cappuccino
    internal class Cappuccino : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Cappuccino is served."); // Utskrift vid konsumtion av cappuccino
        }
    }

    // Implementerar en specifik varm dryck, i detta fall choklad
    internal class HotChocolate : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Hot chocolate is served."); // Utskrift vid konsumtion av choklad
        }
    }

    // Definierar ett interface för fabriker som kan skapa varma drycker
    public interface IWarmDrinkFactory
    {
        IWarmDrink Prepare(int total); // Metod för att förbereda drycken med en specifik mängd
    }

    // Implementerar en specifik fabrik som förbereder varmt vatten
    internal class HotWaterFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pouring {total} ml hot water in your cup"); // Utskrift av mängden vatten som hälls upp
            return new Water(); // Returnerar en ny instans av Water
        }
    }

    // Implementerar en specifik fabrik som förbereder kaffe
    internal class CoffeeFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pouring {total} ml hot coffee in your cup"); // Utskrift av mängden kaffe som hälls upp
            return new Coffee(); // Returnerar en ny instans av Coffee
        }
    }

    // Implementerar en specifik fabrik som förbereder cappuccino
    internal class CappuccinoFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pouring {total} ml hot cappuccino in your cup"); // Utskrift av mängden cappuccino som hälls upp
            return new Cappuccino(); // Returnerar en ny instans av Cappuccino
        }
    }

    // Implementerar en specifik fabrik som förbereder choklad
    internal class HotChocolateFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pouring {total} ml hot chocolate in your cup"); // Utskrift av mängden choklad som hälls upp
            return new HotChocolate(); // Returnerar en ny instans av HotChocolate
        }
    }

    // Maskin som hanterar skapandet av varma drycker
    public class WarmDrinkMachine
    {
        private readonly List<Tuple<string, IWarmDrinkFactory>> namedFactories; // Lista över fabriker med deras namn

        public WarmDrinkMachine()
        {
            namedFactories = new List<Tuple<string, IWarmDrinkFactory>>(); // Initierar listan över fabriker

            // Registrerar fabriker explicit
            RegisterFactory<HotWaterFactory>("Hot water"); // Registrerar fabriken för varmt vatten
            RegisterFactory<CoffeeFactory>("Coffee"); // Registrerar fabriken för kaffe
            RegisterFactory<CappuccinoFactory>("Cappuccino"); // Registrerar fabriken för cappuccino
            RegisterFactory<HotChocolateFactory>("Hot chocolate"); // Registrerar fabriken för varm choklad
        }

        // Metod för att registrera en fabrik
        private void RegisterFactory<T>(string drinkName) where T : IWarmDrinkFactory, new()
        {
            namedFactories.Add(Tuple.Create(drinkName, (IWarmDrinkFactory)Activator.CreateInstance(typeof(T)))); // Lägger till fabriken i listan
        }

        // Metod för att skapa en varm dryck
        public IWarmDrink MakeDrink()
        {
            Console.WriteLine("Welcome to Mini's Coffee Shop!");
            Console.WriteLine();
            Console.WriteLine("This is what we serve today:");
            for (var index = 0; index < namedFactories.Count; index++)
            {
                var tuple = namedFactories[index];
                Console.WriteLine($"{index}: {tuple.Item1}"); // Skriver ut tillgängliga drycker
            }
            Console.WriteLine();
            Console.WriteLine("Select your drink by entering the corresponding number to continue:");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int i) && i >= 0 && i < namedFactories.Count) // Läser och validerar användarens val
                {
                    Console.WriteLine();
                    Console.WriteLine("These are our cup sizes:");
                    Console.WriteLine("1: Short: 236 ml");
                    Console.WriteLine("2: Tall: 354 ml");
                    Console.WriteLine("3: Grande: 473 ml");
                    Console.WriteLine("4: Venti: 591 ml");
                    Console.WriteLine();
                    Console.WriteLine("Select size by entering the corresponding number: ");

                    if (int.TryParse(Console.ReadLine(), out int size) && (size == 1 || size == 2 || size == 3 || size == 4))
                    {
                        Console.WriteLine();
                        int total = size == 1 ? 300 : 450; // Läser och validerar mängden baserat på användarens val
                        return namedFactories[i].Item2.Prepare(total); // Förbereder och returnerar drycken
                    }
                }
                Console.WriteLine("Something went wrong with your input, please try again."); // Meddelande vid felaktig inmatning
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new WarmDrinkMachine(); // Skapar en instans av WarmDrinkMachine
            IWarmDrink drink = machine.MakeDrink(); // Skapar en dryck
            Console.WriteLine();
            drink.Consume(); // Konsumerar drycken
        }
    }
}


