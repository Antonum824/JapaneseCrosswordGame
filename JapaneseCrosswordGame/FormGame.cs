using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace JapaneseCrosswordGame
{
    public partial class FormGame : Form
    {
        private Puzzle currentPuzzle;
        private Button[,] cells;
        private int hintsRemaining;

        public FormGame(string levelFilePath)
        {
            InitializeComponent();

            LoadLevelFromFile(levelFilePath);
            DrawGrid();
            UpdateLivesDisplay();
            UpdateHintsDisplay();
        }

        private void LoadLevelFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            int size = int.Parse(lines[0].Trim());
            string name = lines[1].Trim();

            bool[,] solution = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                string[] parts = lines[2 + i].Trim().Split(' ');
                for (int j = 0; j < size; j++)
                {
                    solution[i, j] = (parts[j] == "1");
                }
            }

            currentPuzzle = new Puzzle(name, size, solution);
            if (currentPuzzle.Size == 5)
            {
                hintsRemaining = 3;
            }
            else if (currentPuzzle.Size == 10)
            {
                hintsRemaining = 5;
            }
            else
            {
                hintsRemaining = 7;
            }
            lblLevelTitle.Text = currentPuzzle.Name;
        }

        private void DrawGrid()
        {
            int size = currentPuzzle.Size;

            tableLayoutGrid.Controls.Clear();
            tableLayoutGrid.RowStyles.Clear();
            tableLayoutGrid.ColumnStyles.Clear();

            int cellSize;
            int hintSize;
            int extraWidth;   // дополнительная ширина
            int extraHeight;  // дополнительная высота

            if (size == 5)
            {
                cellSize = 38;
                hintSize = 70;
                extraWidth = 6;
                extraHeight = 6;
            }
            else if (size == 10)
            {
                cellSize = 35;
                hintSize = 70;
                extraWidth = 12;
                extraHeight = 12;
            }
            else 
            {
                cellSize = 35;
                hintSize = 100;
                extraWidth = 17;
                extraHeight = 17;
            }

            tableLayoutGrid.RowCount = size + 1;
            tableLayoutGrid.ColumnCount = size + 1;

            // Строки
            tableLayoutGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, hintSize));
            for (int i = 0; i < size; i++)
            {
                tableLayoutGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, cellSize));
            }

            // Колонки
            tableLayoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, hintSize));
            for (int j = 0; j < size; j++)
            {
                tableLayoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellSize));
            }

            // Размер панели с учётом дополнительных пикселей
            tableLayoutGrid.Width = hintSize + size * cellSize + extraWidth;
            tableLayoutGrid.Height = hintSize + size * cellSize + extraHeight;

            // Центрируем
            tableLayoutGrid.Location = new Point(
                (this.ClientSize.Width - tableLayoutGrid.Width) / 2,
                80
            );

            cells = new Button[size, size];

            for (int i = 0; i <= size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    Button btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.Margin = new Padding(0);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.BackColor = Color.White;

                    if (i == 0 && j == 0)
                    {
                        btn.Enabled = false;
                        btn.BackColor = Color.LightGray;
                        tableLayoutGrid.Controls.Add(btn, j, i);
                    }
                    else if (i == 0)
                    {
                        List<int> hints = GetColumnHints(currentPuzzle.Solution, j - 1);
                        btn.Text = string.Join("\n", hints.ConvertAll(x => x.ToString()));
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                        btn.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Bold);
                        btn.Enabled = false;
                        btn.BackColor = Color.LightGray;
                        tableLayoutGrid.Controls.Add(btn, j, i);
                    }
                    else if (j == 0)
                    {
                        List<int> hints = GetRowHints(currentPuzzle.Solution, i - 1);
                        btn.Text = string.Join(" ", hints);
                        btn.TextAlign = ContentAlignment.MiddleRight;
                        btn.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Bold);
                        btn.Enabled = false;
                        btn.BackColor = Color.LightGray;
                        tableLayoutGrid.Controls.Add(btn, j, i);
                    }
                    else
                    {
                        btn.Tag = (i - 1, j - 1);
                        btn.MouseDown += Cell_MouseDown;
                        btn.BackColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 1;

                        int blockRow = (i - 1) / 5;
                        int blockCol = (j - 1) / 5;

                        if ((blockRow + blockCol) % 2 == 1)
                        {
                            btn.FlatAppearance.BorderColor = Color.LightGray;
                        }
                        else
                        {
                            btn.FlatAppearance.BorderColor = Color.Black;
                        }

                        tableLayoutGrid.Controls.Add(btn, j, i);
                        cells[i - 1, j - 1] = btn;
                    }
                }
            }
        }

        private List<int> GetRowHints(bool[,] solution, int row)
        {
            List<int> hints = new List<int>();
            int count = 0;
            for (int j = 0; j < solution.GetLength(1); j++)
            {
                if (solution[row, j]) count++;
                else if (count > 0) { hints.Add(count); count = 0; }
            }
            if (count > 0) hints.Add(count);
            if (hints.Count == 0) hints.Add(0);
            return hints;
        }

        private List<int> GetColumnHints(bool[,] solution, int col)
        {
            List<int> hints = new List<int>();
            int count = 0;
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                if (solution[i, col]) count++;
                else if (count > 0) { hints.Add(count); count = 0; }
            }
            if (count > 0) hints.Add(count);
            if (hints.Count == 0) hints.Add(0);
            return hints;
        }

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            var (i, j) = ((int, int))btn.Tag;

            if (e.Button == MouseButtons.Left)
            {
                switch (currentPuzzle.Current[i, j])
                {
                    case CellState.Empty:
                        // Пусто - серый 
                        currentPuzzle.Current[i, j] = CellState.Pending;
                        btn.BackColor = Color.FromArgb(170, 170, 170);
                        btn.Text = "";
                        break;

                    case CellState.Pending:
                        // Серый - чёрный
                        currentPuzzle.Current[i, j] = CellState.Filled;
                        btn.BackColor = Color.Black;
                        btn.Text = "";

                        // Проверяем, ошибка ли это
                        if (!currentPuzzle.Solution[i, j])
                        {
                            currentPuzzle.Lives--;
                            UpdateLivesDisplay();
                            btn.BackColor = Color.LightCoral;

                            if (currentPuzzle.Lives <= 0)
                            {
                                DialogResult result = MessageBox.Show("Жизни закончились!\n\nНачать уровень заново?",
                                    "Игра окончена", MessageBoxButtons.YesNoCancel);

                                if (result == DialogResult.Yes)
                                {
                                    // Да → начать уровень заново
                                    currentPuzzle.Reset();
                                    DrawGrid();
                                    UpdateLivesDisplay();
                                    UpdateHintsDisplay();
                                    tableLayoutGrid.Enabled = true;
                                }
                                else if (result == DialogResult.No)
                                {
                                    // Нет → вернуться в меню
                                    this.Close();
                                }
                                // Cancel → остаться на уровне (ничего не делаем, поле заблокировано)
                                else
                                {
                                    tableLayoutGrid.Enabled = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Ошибка! Осталось жизней: {currentPuzzle.Lives}");

                                // Возвращаем в серый через 1 секунду
                                var timer = new Timer();
                                timer.Interval = 1000;
                                timer.Tick += (s, ev) =>
                                {
                                    if (currentPuzzle.Current[i, j] != CellState.Cross)
                                    {
                                        currentPuzzle.Current[i, j] = CellState.Pending;
                                        btn.BackColor = Color.LightGray;
                                    }
                                    timer.Stop();
                                };
                                timer.Start();
                            }
                        }
                        else if (CheckWin())
                        {
                            DialogResult result = MessageBox.Show("ПОБЕДА! \n\nХотите вернуться в меню?", "Поздравляем!", MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                            {
                                this.Close();  // возврат в меню
                            }
                            else
                            {
                                tableLayoutGrid.Enabled = false;  // остаться на уровне, но заблокировать поле
                            }
                        }
                        break;

                    case CellState.Cross:
                        // Крестик → серый
                        currentPuzzle.Current[i, j] = CellState.Pending;
                        btn.BackColor = Color.LightGray;
                        btn.Text = "";
                        break;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (currentPuzzle.Current[i, j] == CellState.Filled)
                {
                    return;
                }
                // Правая кнопка: ставим/убираем крестик
                if (currentPuzzle.Current[i, j] == CellState.Cross)
                {
                    currentPuzzle.Current[i, j] = CellState.Empty;
                    btn.BackColor = Color.White;
                    btn.Text = "";
                }
                else
                {
                    currentPuzzle.Current[i, j] = CellState.Cross;
                    btn.BackColor = Color.White;
                    btn.Text = "✗";
                    btn.ForeColor = Color.Black;
                }
            }
        }

        private void UpdateHintsDisplay()
        {
            lblHintsRemaining.Text = $"Подсказок осталось: {hintsRemaining}";
        }

        private bool CheckWin()
        {
            for (int i = 0; i < currentPuzzle.Size; i++)
                for (int j = 0; j < currentPuzzle.Size; j++)
                    if (currentPuzzle.Solution[i, j] != (currentPuzzle.Current[i, j] == CellState.Filled))
                        return false;
            return true;
        }

        private void UpdateLivesDisplay()
        {
            string hearts = "";
            for (int i = 0; i < currentPuzzle.Lives; i++) hearts += "♥";
            for (int i = 0; i < 3 - currentPuzzle.Lives; i++) hearts += "";
            lblLives.Text = hearts;
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGiveUp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите сдаться? Будет показан правильный ответ.",
        "Сдаться", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Показываем правильное решение
                for (int i = 0; i < currentPuzzle.Size; i++)
                {
                    for (int j = 0; j < currentPuzzle.Size; j++)
                    {
                        if (currentPuzzle.Solution[i, j])
                        {
                            currentPuzzle.Current[i, j] = CellState.Filled;
                            cells[i, j].BackColor = Color.Black;
                        }
                        else
                        {
                            currentPuzzle.Current[i, j] = CellState.Empty;
                            cells[i, j].BackColor = Color.White;
                            cells[i, j].Text = "";
                        }
                    }
                }

                MessageBox.Show("Правильное решение показано. Возврат в меню.");

                // Возвращаемся в меню
                this.Close();
            }
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            if (hintsRemaining <= 0)
            {
                MessageBox.Show("Подсказки закончились!", "Предупреждение");
                return;
            }
            // Ищем все незакрашенные клетки, которые должны быть чёрными
            List<(int, int)> unfilledCorrect = new List<(int, int)>();

            for (int i = 0; i < currentPuzzle.Size; i++)
            {
                for (int j = 0; j < currentPuzzle.Size; j++)
                {
                    if (currentPuzzle.Solution[i, j] && currentPuzzle.Current[i, j] != CellState.Filled)
                    {
                        unfilledCorrect.Add((i, j));
                    }
                }
            }

            if (unfilledCorrect.Count == 0)
            {
                MessageBox.Show("Все правильные клетки уже закрашены!", "Подсказка");
                return;
            }

            // Выбираем случайную клетку
            Random rand = new Random();
            var (iHint, jHint) = unfilledCorrect[rand.Next(unfilledCorrect.Count)];

            hintsRemaining--;
            UpdateHintsDisplay();

            // Подсвечиваем её зелёным на секунду
            cells[iHint, jHint].BackColor = Color.LightGreen;
            // Возвращаем нормальный цвет через 1 секунду
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) => {
                if (currentPuzzle.Current[iHint, jHint] == CellState.Filled)
                    cells[iHint, jHint].BackColor = Color.Black;
                else if (currentPuzzle.Current[iHint, jHint] == CellState.Pending)
                    cells[iHint, jHint].BackColor = Color.LightGray;
                else
                    cells[iHint, jHint].BackColor = Color.White;
                timer.Stop();
            };
            timer.Start();
        }

        private void lblHintsRemaining_Click(object sender, EventArgs e)
        {

        }
    }
}
