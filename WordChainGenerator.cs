using Fastenshtein;
using Kaos.Combinatorics;
using QuikGraph;

namespace szolanc
{
    internal class WordChainGenerator
    {
        public WordChainGenerator(List<string> words)
        {
            if (words.Count == 0)
                throw new ArgumentException("invalid argument in constructor: empty wordlist provided");
            this.words = words;

            // készítünk egy (irányítatlan) gráfot a szavakból; a szavakat reprezentálják a csúcsok...
            graph = new();
            for (int i = 0; i < words.Count; i++)
            {
                graph.AddVertex(i);
            }
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < words.Count; j++)
                {
                    // ...két csúcs között pedig akkor lesz él, ha a két szó
                    // állhat egymás mellett a szóláncban.

                    // A Math.Min és Max hívások azért kellenek, mert csak úgy lehet létrehozni egy
                    // UndirectedEdge példányt két csúcs között, ha a kisebb csúcs sorszámát írjuk előre.
                    if (i != j && IsTransformable(words[i], words[j]))
                        graph.AddEdge(new UndirectedEdge<int>(Math.Min(i, j), Math.Max(i, j)));
                }
            }
        }

        private readonly List<string> words;
        private readonly UndirectedGraph<int, UndirectedEdge<int>> graph;
        /// <summary>
        /// Megadja, a paraméterként kapott két szó átalakítható-e egymásba (azaz állhatnak-e egymás mellett a szóláncban).
        ///
        /// Két szó akkor alakítható át egymásba (azaz akkor állhatnak egymás mellett), 
        /// ha a Levenshtein-távolságuk maximum 1 (azaz max 1 karakterben különböznek a feladat szabályai szerint
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private bool IsTransformable(string from, string to) => Levenshtein.Distance(from, to) <= 1;

        /// <summary>
        /// Visszaad egy lehetséges szóláncot a szavakból, amiben az összes szót felhasználja (amennyiben lehetséges)
        /// </summary>
        /// <returns>Egy szabályos szólánc a szavkaból. Ha nem lehet ilyet alkotni, üres string</returns>
        public string GetPossibleWordChain()
        {
            // Addig generáljuk (nem ismétlődő módon) permutációit a gráf csúcsainak (amik a szavakat reprezentálják),
            // amíg nem találunk olyat, ami szabályos szóláncot alkotna.
            // Ha nincs ilyen, üres stringgel térünk vissza
            foreach (Permutation? row in new Permutation(graph.VertexCount).GetRows())
            {
                List<int> sequence = Permutation.Permute(row, graph.Vertices.ToList());
                // Ha helyes szekvenciát találtunk, elkészítjük a szóláncot és visszaadjuk azt
                if (IsConnectedSequence(sequence))
                    return GetWordsInOrder(sequence);
            }
            return "";
        }
        /// <summary>
        /// Visszaadja a szavakat egy bizonyos sorrendben
        /// </summary>
        /// <param name="sequence">A sorrend</param>
        /// <returns>A szavak a megadott sorrendben, space-ekkel összefűzve</returns>
        private string GetWordsInOrder(List<int> sequence)
        {
            if (sequence.Count != words.Count)
                throw new ArgumentException("invalid argument: sequence length doesn't match the number of words");
            if (sequence.Max() > words.Count - 1 || sequence.Min() != 0)
                throw new ArgumentException("invalid argument: sequence contains illegal characters");

            List<string> wordsResult = new();
            for (int i = 0; i < sequence.Count; i++)
            {
                wordsResult.Add(words[sequence[i]]);
            }
            return string.Join(" ", wordsResult);
        }

        /// <summary>
        /// Megadja, hogy a paraméterként kapott szekvencia (sorrend) szabályos szóláncot alkotna-e
        /// </summary>
        /// <param name="sequence">A sorrend</param>
        /// <returns>Szabályos szóláncot alkotna-e a sorrend</returns>
        private bool IsConnectedSequence(List<int> sequence)
        {
            if (sequence.Count != words.Count)
                throw new ArgumentException("invalid argument: sequence length doesn't match the number of words");

            // edge case: az összes egyelemű szekvencia helyes (azaz minden szó önmagában helyes szóláncnak számít)
            if (sequence.Count == 1) return true;
            for (int i = 1; i < sequence.Count; i++)
            {
                // ha két egymást követő szó között a gráfban nincs él, akkor ez nem lenne szabályos szólánc
                if (!graph.ContainsEdge(sequence[i], sequence[i - 1]))
                    return false;
            }
            return true;
        }
    }
}