using System;
using System.Windows.Forms;

namespace KółkoIKrzyżyk
{
    public class MainForm : Form
    {
        private Button[,] guzik; // Przyciski na planszy
        private int gracz; // Numer aktualnego gracza
        private Label info_etykieta; // Etykieta informacji o aktualnym graczu
        private Label win_etykieta; // Etykieta informacji o zwycięzcy

        public MainForm()
        {
            // Konfiguracja okna głównego
            this.Text = "Gra w Kółko i Krzyżyk";
            this.Size = new System.Drawing.Size(300, 400);
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            guzik = new Button[3, 3]; // Inicjalizacja przycisków na planszy
            gracz = 1; // Początkowo gra rozpoczyna się od gracza 1 (Kółko)

            info_etykieta = new Label();
            info_etykieta.Text = "Gracz 1 (Kółko) zaczyna";
            info_etykieta.Location = new System.Drawing.Point(10, 240);
            info_etykieta.Size = new System.Drawing.Size(230, 30);
            info_etykieta.Font = new System.Drawing.Font("Verdana", 12);
            this.Controls.Add(info_etykieta);

            win_etykieta = new Label();
            win_etykieta.Text = "Wynik gry: Brak zwycięzcy";
            win_etykieta.Location = new System.Drawing.Point(10, 270);
            win_etykieta.Size = new System.Drawing.Size(600, 30);
            win_etykieta.Font = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            this.Controls.Add(win_etykieta);

            // Inicjalizacja przycisków na planszy
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    guzik[i, j] = new Button();
                    guzik[i, j].Size = new System.Drawing.Size(80, 80);
                    guzik[i, j].Location = new System.Drawing.Point(i * 80, j * 80);
                    guzik[i, j].Font = new System.Drawing.Font("Arial", 32, System.Drawing.FontStyle.Bold);
                    guzik[i, j].Click += new EventHandler(plansza);
                    guzik[i, j].Tag = 0; // 0 oznacza puste pole
                    guzik[i, j].FlatStyle = FlatStyle.Flat;
                    guzik[i, j].BackColor = System.Drawing.Color.FromArgb(220, 220, 220);
                    this.Controls.Add(guzik[i, j]);
                }
            }
        }

        private void plansza(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if ((int)button.Tag == 0) // Jeśli pole jest puste
            {
                if (gracz == 1)
                {
                    button.Text = "O"; // Gracz 1 (Kółko)
                    button.Tag = 1;
                    gracz = 2;
                    info_etykieta.Text = "Gracz 2 (Krzyżyk)";
                }
                else
                {
                    button.Text = "X"; // Gracz 2 (Krzyżyk)
                    button.Tag = 2;
                    gracz = 1;
                    info_etykieta.Text = "Gracz 1 (Kółko)";
                }

                button.Enabled = false; // Dezaktywacja przycisku po ruchu

                if (wygrany() == true) // Sprawdzenie, czy jest zwycięzca
                {
                    string zwyciezca = "Brak zwycięzcy";
                    if (gracz == 1)
                        zwyciezca = "Gracz 2 (Krzyżyk)";
                    else if (gracz == 2)
                        zwyciezca = "Gracz 1 (Kółko)";

                    win_etykieta.Text = "Wynik gry: " + zwyciezca + " wygrywa!";
                    zielony(); // Podświetlenie zwycięskiego wzorca
                    wylacz_guzik(); // Dezaktywacja wszystkich przycisków
                }
                else if (remis()) // Sprawdzenie remisu
                {
                    win_etykieta.Text = "Wynik gry: Remis!";
                }
            }
        }



        private bool wygrany()
        {
            // Sprawdzamy, czy któryś z graczy wygrał w poziomych i pionowych liniach oraz na przekątnych

            // Sprawdzanie poziomych linii
            for (int i = 0; i < 3; i++)
            {
                // Jeśli pole jest zajęte (Tag != 0) i wszystkie pola w linii są takie same, zwracamy true, co oznacza zwycięstwo
                if ((int)guzik[i, 0].Tag != 0 && (int)guzik[i, 0].Tag == (int)guzik[i, 1].Tag && (int)guzik[i, 1].Tag == (int)guzik[i, 2].Tag)
                    return true;

                // Jeśli pole jest zajęte (Tag != 0) i wszystkie pola w kolumnie są takie same, zwracamy true, co oznacza zwycięstwo
                if ((int)guzik[0, i].Tag != 0 && (int)guzik[0, i].Tag == (int)guzik[1, i].Tag && (int)guzik[1, i].Tag == (int)guzik[2, i].Tag)
                    return true;
            }

            // Sprawdzanie przekątnych
            // Jeśli pole jest zajęte (Tag != 0) i wszystkie pola na głównej przekątnej są takie same, zwracamy true, co oznacza zwycięstwo
            if ((int)guzik[0, 0].Tag != 0 && (int)guzik[0, 0].Tag == (int)guzik[1, 1].Tag && (int)guzik[1, 1].Tag == (int)guzik[2, 2].Tag)
                return true;

            // Jeśli pole jest zajęte (Tag != 0) i wszystkie pola na drugiej przekątnej są takie same, zwracamy true, co oznacza zwycięstwo
            if ((int)guzik[0, 2].Tag != 0 && (int)guzik[0, 2].Tag == (int)guzik[1, 1].Tag && (int)guzik[1, 1].Tag == (int)guzik[2, 0].Tag)
                return true;

            // Jeśli nie znaleziono zwycięzcy, zwracamy false
            return false;
        }




        private void zielony()
        {
            // Funkcja odpowiedzialna za podświetlenie zwycięskiego wzorca na zielono

            for (int i = 0; i < 3; i++)
            {
                // Sprawdzanie poziomych linii
                if ((int)guzik[i, 0].Tag != 0 && (int)guzik[i, 0].Tag == (int)guzik[i, 1].Tag && (int)guzik[i, 1].Tag == (int)guzik[i, 2].Tag)
                {
                    guzik[i, 0].BackColor = guzik[i, 1].BackColor = guzik[i, 2].BackColor = System.Drawing.Color.LightGreen;
                    return;
                }

                // Sprawdzanie pionowych linii
                if ((int)guzik[0, i].Tag != 0 && (int)guzik[0, i].Tag == (int)guzik[1, i].Tag && (int)guzik[1, i].Tag == (int)guzik[2, i].Tag)
                {
                    guzik[0, i].BackColor = guzik[1, i].BackColor = guzik[2, i].BackColor = System.Drawing.Color.LightGreen;
                    return;
                }
            }

            // Sprawdzanie przekątnych linii

            // Pierwsza przekątna (górna lewa do dolnej prawej)
            if ((int)guzik[0, 0].Tag != 0 && (int)guzik[0, 0].Tag == (int)guzik[1, 1].Tag && (int)guzik[1, 1].Tag == (int)guzik[2, 2].Tag)
            {
                guzik[0, 0].BackColor = guzik[1, 1].BackColor = guzik[2, 2].BackColor = System.Drawing.Color.LightGreen;
                return;
            }

            // Druga przekątna (górna prawa do dolnej lewej)
            if ((int)guzik[0, 2].Tag != 0 && (int)guzik[0, 2].Tag == (int)guzik[1, 1].Tag && (int)guzik[1, 1].Tag == (int)guzik[2, 0].Tag)
            {
                guzik[0, 2].BackColor = guzik[1, 1].BackColor = guzik[2, 0].BackColor = System.Drawing.Color.LightGreen;
            }
        }



        private void wylacz_guzik()
        {
            foreach (Button btn in guzik)
            {
                btn.Enabled = false;
            }
        }

        private bool remis()
        {
            foreach (Button btn in guzik)
            {
                if ((int)btn.Tag == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}
