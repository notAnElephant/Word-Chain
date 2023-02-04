using Fastenshtein;
using Kaos.Combinatorics;
using QuikGraph;

namespace szolanc
{
    internal class TransformationMatrix
    {

        public TransformationMatrix(List<string> words)
        {
            this.words = words;
            graph = new();

            for (int i = 0; i < words.Count; i++)
            {
                graph.AddVertex(i);
            }
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < words.Count; j++)
                {
                    if (i != j && IsTransformable(words[i], words[j]))
                        graph.AddEdge(new UndirectedEdge<int>(Math.Min(i, j), Math.Max(i, j)));
                }
            }
#if DEBUG
            VisualizeGraph();
#endif
        }

        private List<string> words;
        private UndirectedGraph<int, UndirectedEdge<int>> graph;
        private bool IsTransformable(string from, string to)
        {
            return Levenshtein.Distance(from, to) <= 1;
        }

        private void VisualizeGraph()
        {
            Console.WriteLine();
            Console.Write("\t");
            for (int i = 0; i < words.Count; i++)
            {
                Console.Write($"{words[i]}\t");
            }
            Console.WriteLine();
            for (int i = 0; i < words.Count; i++)
            {
                Console.Write($"{words[i]}\t");
                for (int j = 0; j < words.Count; j++)
                {
                    Console.Write($"{graph.ContainsEdge(i, j)}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public string GetPossibleWordChain()
        {
            foreach (Permutation? row in new Permutation(graph.VertexCount).GetRows())
            {
                List<int> sequence = Permutation.Permute(row, graph.Vertices.ToList());
                if (IsConnectedSequence(sequence))
                {
                    sequence.ForEach(Console.Write);
                    Console.WriteLine();
                    return GetWordsInOrder(sequence);
                }
            }
            return "";
        }

        private string GetWordsInOrder(List<int> sequence)
        {
            List<string> wordsResult = new();
            for (int i = 0; i < sequence.Count; i++)
            {
                wordsResult.Add(words[sequence[i]]);
            }
            return string.Join(" ", wordsResult);
        }

        private bool IsConnectedSequence(List<int> sequence)
        {
            if (sequence.Count == 1) return true;
            for (int i = 1; i < sequence.Count; i++)
            {
                if (!graph.ContainsEdge(new UndirectedEdge<int>(Math.Min(sequence[i], sequence[i - 1]),
                    Math.Max(sequence[i], sequence[i - 1]))))
                    return false;
            }
            return true;
        }
    }
}