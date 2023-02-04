namespace szolanc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 0    1   2   3   4   5   6  7
            //coat hat hot dog cat hog cot oat
            //dog hog hot hat oat coat cat cot
            // 3   5   2   1   7   0    4   6
            Console.WriteLine("Add meg a szavakat szóközzel elválasztva:");
            var words = Console.ReadLine().Split(' ').ToList();

            var matrix = new TransformationMatrix(words);
            string result = matrix.GetPossibleWordChain();
            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Hiba: nincs megoldás");
            }
            else
            {
                Console.WriteLine(result);
            }
            Console.ReadKey();
        }
    }
}