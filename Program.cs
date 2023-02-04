namespace szolanc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Add meg a szavakat szóközzel elválasztva:");
            var words = Console.ReadLine().Split(' ').ToList();
            if (words is null || string.IsNullOrEmpty(words.First()))
            {
                Console.WriteLine("Hiba: üres bemenet");
                return;
            }
            var generator = new WordChainGenerator(words);
            string result = generator.GetPossibleWordChain();
            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Hiba: nincs megoldás");
            }
            else
            {
                Console.WriteLine($"Egy érvényes szólánc: {result}");
            }
            Console.WriteLine("\nNyomj meg egy gombot a kilépéshez");
            Console.ReadKey();
        }
    }
}