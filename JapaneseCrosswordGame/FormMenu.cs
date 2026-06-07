using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;

namespace JapaneseCrosswordGame
{
    public partial class FormMenu : Form
    {
        private List<string> levelFiles = new List<string>();
        private List<string> levelNames = new List<string>();
        private List<SoundPlayer> players = new List<SoundPlayer>();
        private int currentTrack = 0;
        private Timer musicTimer;

        public FormMenu()
        {
            InitializeComponent();
            cmbSize.Items.Add("Лёгкая (5×5)");
            cmbSize.Items.Add("Средняя (10×10)");
            cmbSize.Items.Add("Сложная (15×15)");
            cmbSize.SelectedIndex = 0;
            InitializeMusic();
            PlayNextTrack();
            this.FormClosing += FormMenu_FormClosing;

            // Загружаем уровни для начального размера
            UpdateLevelList();
        }

        private void InitializeMusic()
        {
            players.Add(new SoundPlayer("melody1.wav"));
            players.Add(new SoundPlayer("melody2.wav"));
            players.Add(new SoundPlayer("melody3.wav"));
            players.Add(new SoundPlayer("melody4.wav"));
        }

        private void PlayNextTrack()
        {
            if (players.Count == 0) return;

            // Останавливаем текущий трек
            if (currentTrack > 0)
                players[currentTrack - 1]?.Stop();

            // Запускаем следующий
            players[currentTrack].Play();
            musicTimer = new Timer();
            musicTimer.Interval = 120000;  // 2 минуты
            musicTimer.Tick += (s, ev) => {
                currentTrack = (currentTrack + 1) % players.Count;
                PlayNextTrack();
                musicTimer.Stop();
            };
            musicTimer.Start();
        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            musicTimer?.Stop();
            foreach (var player in players)
            {
                player?.Stop();
                player?.Dispose();
            }
        }

        private void UpdateLevelList()
        {
            // Определяем выбранный размер
            int selectedSize = 5;
            string selected = cmbSize.SelectedItem.ToString();
            if (selected.Contains("10×10")) selectedSize = 10;
            else if (selected.Contains("15×15")) selectedSize = 15;

            // Очищаем старые списки
            levelFiles.Clear();
            levelNames.Clear();
            cmbLevel.Items.Clear();

            // Папка с уровнями (создаётся в папке с программой)
            string levelsDir = Path.Combine(Application.StartupPath, "Levels");

            // Если папки нет — создаём
            if (!Directory.Exists(levelsDir))
            {
                Directory.CreateDirectory(levelsDir);
                MessageBox.Show("Папка 'Levels' создана. Добавьте туда файлы уровней.");
                return;
            }

            // Ищем все .txt файлы
            string[] files = Directory.GetFiles(levelsDir, "*.txt");

            foreach (string file in files)
            {
                try
                {
                    string[] lines = File.ReadAllLines(file);
                    if (lines.Length >= 2)
                    {
                        int fileSize = int.Parse(lines[0].Trim());
                        if (fileSize == selectedSize)
                        {
                            string levelName = lines[1].Trim();
                            levelFiles.Add(file);
                            levelNames.Add(levelName);
                            cmbLevel.Items.Add($"{levelName} ({fileSize}×{fileSize})");
                        }
                    }
                }
                catch
                {
                    // Если файл повреждён — пропускаем
                }
            }

            if (cmbLevel.Items.Count == 0)
            {
                cmbLevel.Items.Add("Нет уровней");
                cmbLevel.Enabled = false;
            }
            else
            {
                cmbLevel.SelectedIndex = 0;
                cmbLevel.Enabled = true;
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLevelList();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (levelFiles.Count == 0 || cmbLevel.SelectedIndex < 0 || cmbLevel.SelectedIndex >= levelFiles.Count)
            {
                MessageBox.Show("Нет доступных уровней. Добавьте файлы в папку Levels.");
                return;
            }
            string selectedFile = levelFiles[cmbLevel.SelectedIndex];
            FormGame game = new FormGame(selectedFile);
            game.ShowDialog();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
