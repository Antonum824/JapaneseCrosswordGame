using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapaneseCrosswordGame
{
    public enum CellState
    {
        Empty,      // пусто
        Pending,    // серый
        Filled,     // чёрный
        Cross       // крестик
    }

    public class Puzzle
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public bool[,] Solution { get; set; }
        public CellState[,] Current { get; set; }
        public int Lives { get; set; }

        public Puzzle(string name, int size, bool[,] solution)
        {
            Name = name;
            Size = size;
            Solution = solution;
            Current = new CellState[size, size];
            Lives = 3;
        }

        public void Reset()
        {
            Current = new CellState[Size, Size];
            Lives = 3;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (Solution[i, j] != (Current[i, j] == CellState.Filled))
                        return false;
            return true;
        }
    }
}
