using Projekt3;
using System.ComponentModel.Design;
using System.Drawing;

/*
 * Zbadać doświadczalnie średnią wysokość drzew BST (z duplikatami) budowanych ze słów języka polskiego.  
Doświadczenia przeprowadzić dla drzew o różnej ilości elementów (100 do 10 000 z krokiem 100) times losowych słów ze zbiorów: 

- {ALA, OLA, KOS, PIES}; 
- słów o długości do 4 liter; 
- wszystkich słów. 

Sprawdzić średnią ilość operacji

porównania dla tych

przypadków przy: 

- wstawianiu,  
- wyszukiwaniu, 
- usuwaniu elementów z drzewa.  

Porównać odpowiednie przypadki times podać ograniczenia funkcyjne dla poszczególnych z nich. 
*/

public enum WordsType
{
    StandardWords,
    LessEqualFourWords,
    AllWords
}

class Program
{
    static string krzyzacy = "krzyzacySlowa.txt";

    static string standardWords = "standardWords.txt";
    static string lessEqual4Words = "mniejRowne4Slowa.txt";
    static string allWords = "wszystkieSlowa.txt";

    static int thingsCount = 5;

    static void Main()
    {
        Run(WordsType.StandardWords);
        Run(WordsType.LessEqualFourWords);
        Run(WordsType.AllWords);
    }

    static void Run(WordsType type)
    {
        Random rand = new Random();
        string[] words = GetWords(type);

        int[] averageDepths = new int[100];
        int[] averageInsertMoves = new int[100];
        int[] averageSearchMoves = new int[100];
        int[] averagetDeleteMoves = new int[100];

        //for every size
        for (int size = 100; size <= 10 * 1000; size += 100)
        {
            int sizeIndex = size / 100 - 1;

            int totalDepth = 0;
            int totalInsert = 0;
            int totalSearch = 0;
            int totalDelete = 0;
            //x times for better rounding
            for (int times = 0; times < thingsCount; times++)
            {
                Tree tree = new Tree();

                //filling the tree
                for (int i = 0; i < size; i++)
                {
                    tree.Insert(words[GetRandomIndex(words.Length)], false);
                }
                //get depth of each tree
                totalDepth += tree.depth;
                //x times again, for better rounded data
                for (int j = 0; j < thingsCount * 2; j++)
                {
                    tree.Insert(words[GetRandomIndex(words.Length)], true);
                    tree.Search(words[GetRandomIndex(words.Length)], true);
                    tree.Delete(words[GetRandomIndex(words.Length)], true);
                }
                //save sum of things of doing it x times per size
                totalInsert += Tree.InsertMoveCount;
                totalSearch += Tree.SearchMoveCount;
                totalDelete += Tree.DeleteMoveCount;

            }

            Console.WriteLine($"SIZE = {size} Index---({sizeIndex})");
            averageDepths[sizeIndex] = totalDepth / (thingsCount);
            averageInsertMoves[sizeIndex] = totalInsert / (thingsCount * 2);
            averageSearchMoves[sizeIndex] = totalSearch / (thingsCount * 2);
            averagetDeleteMoves[sizeIndex] = totalDelete / (thingsCount * 2);
            //Console.WriteLine($"average depth={totalDepth / thingsCount}");
            //Console.WriteLine($"average InsertMoveCount={totalInsert / thingsCount}");
            //Console.WriteLine($"average SearchMoveCount={totalSearch / thingsCount}");
            //Console.WriteLine($"average DeleteMoveCount={totalDelete / thingsCount}");

            totalDepth = 0;
            Tree.ResetData();
        }

        Console.WriteLine();
        Console.WriteLine($"average depth:");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"{averageDepths[i]} ");
        }

        Console.WriteLine($"average Insert Moves:");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"{averageInsertMoves[i]} ");
        }

        Console.WriteLine($"average Search Moves:");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"{averageSearchMoves[i]} ");
        }

        Console.WriteLine($"averaget Delete Moves:");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"{averagetDeleteMoves[i]} ");
        }
    }

    static int GetRandomIndex(int size)
    {
        Random rand = new Random();
        return rand.Next() % size;
    }
    static string[] GetWords(WordsType type)
    {
        string[] words = new string[File.ReadAllText(krzyzacy).Length];
        switch (type)
        {
            case WordsType.StandardWords:
                words = File.ReadAllLines(standardWords);
                break;
            case WordsType.LessEqualFourWords:
                words = File.ReadAllLines(lessEqual4Words);
                break;
            case WordsType.AllWords:
                words = File.ReadAllLines(allWords);
                break;
            default:
                break;
        }
        return words;
    }
}