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
        private Button reset; // Przycisk do wyłączenia aplikacji
        private bool gameOver;

        public MainForm()
        {
            // Konfiguracja okna głównego
            this.Text = "Gra w Kółko i Krzyżyk"; // Ustawienie tytułu okna
            this.Size = new System.Drawing.Size(300, 400); // Ustawienie rozmiaru okna
            this.BackColor = System.Drawing.Color.White; // Ustawienie koloru tła okna
            guzik = new Button[3, 3]; // Inicjalizacja tablicy przycisków planszy
            gracz = 1; // Początkowo gra rozpoczyna się od gracza 1 (Kółko)


            // Inicjalizacja przycisków na planszy

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    guzik[i, j] = new Button(); // Tworzenie nowego przycisku
                    guzik[i, j].Size = new System.Drawing.Size(80, 80); // Ustawienie rozmiaru przycisku
                    guzik[i, j].Location = new System.Drawing.Point(i * 80, j * 80); // Ustawienie pozycji przycisku
                    guzik[i, j].Font = new System.Drawing.Font("Arial", 32, System.Drawing.FontStyle.Bold); // Ustawienie czcionki przycisku
                    guzik[i, j].Click += new EventHandler(plansza); // Dodanie obsługi zdarzenia kliknięcia na przycisku
                    guzik[i, j].Tag = 0; // Ustawienie Tag przycisku na 0, oznaczający puste pole
                    guzik[i, j].FlatStyle = FlatStyle.Flat; // Ustawienie płaskiego stylu przycisku
                    guzik[i, j].BackColor = System.Drawing.Color.FromArgb(220, 220, 220); // Ustawienie koloru tła przycisku
                    this.Controls.Add(guzik[i, j]); // Dodanie przycisku do kontrolek na oknie
                }
            }

            // Konfiguracja etykiety informującej o aktualnym graczu
            info_etykieta = new Label();
            info_etykieta.Text = "Gracz 1 (Kółko) zaczyna";
            info_etykieta.Location = new System.Drawing.Point(10, 250);
            info_etykieta.Size = new System.Drawing.Size(230, 30);
            info_etykieta.Font = new System.Drawing.Font("Verdana", 12);
            this.Controls.Add(info_etykieta); // Dodanie etykiety do kontrolek na oknie

            // Konfiguracja etykiety informującej o stanie gry
            win_etykieta = new Label();
            win_etykieta.Text = "Wynik gry: ----";
            win_etykieta.Location = new System.Drawing.Point(10, 280);
            win_etykieta.Size = new System.Drawing.Size(600, 30);
            win_etykieta.Font = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            this.Controls.Add(win_etykieta); // Dodanie etykiety do kontrolek na oknie


            reset = new Button();
            reset.Text = "Nowa Gra";
            reset.Size = new System.Drawing.Size(150, 40);
            reset.Location = new System.Drawing.Point(10, 320);
            reset.Click += new EventHandler(reset_gry_funkcja);
            reset.FlatStyle = FlatStyle.Flat;
            reset.Visible = false;
            reset.Font = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            this.Controls.Add(reset);


        }

        private void plansza(object sender, EventArgs e)
        {
            Button pole = (Button)sender; // Pobranie przycisku, który został kliknięty
           
            if ((int)pole.Tag == 0) // Jeśli pole jest puste
            {
                if (gracz == 1)
                {
                    pole.Text = "O"; // Ustawienie "O" dla Gracza 1 (Kółko)
                    pole.Tag = 1; // Ustawienie Tag przycisku na 1, oznaczając, że pole jest teraz zajęte przez Gracza 1
                    gracz = 2; // Zmiana aktualnego gracza na Gracza 2 (Krzyżyk)
                    info_etykieta.Text = "Gracz 2 (Krzyżyk)"; // Aktualizacja etykiety informującej o aktualnym graczu
                }
                else
                {
                    pole.Text = "X"; // Ustawienie "X" dla Gracza 2 (Krzyżyk)
                    pole.Tag = 2; // Ustawienie Tag przycisku na 2, oznaczając, że pole jest teraz zajęte przez Gracza 2
                    gracz = 1; // Zmiana aktualnego gracza na Gracza 1 (Kółko)
                    info_etykieta.Text = "Gracz 1 (Kółko)"; // Aktualizacja etykiety informującej o aktualnym graczu
                }

                pole.Enabled = false; // Dezaktywacja przycisku po ruchu, aby uniknąć kolejnych zmian

                if (wygrany() == true) // Sprawdzenie, czy jest zwycięzca
                {
                    string zwyciezca = "Brak zwycięzcy"; // Początkowo brak zwycięzcy
                    if (gracz == 1)
                        zwyciezca = "Gracz 2 (Krzyżyk)";
                    else if (gracz == 2)
                        zwyciezca = "Gracz 1 (Kółko)";

                    win_etykieta.Text = "Wynik gry: " + zwyciezca + " wygrywa!"; // Wyświetlenie informacji o zwycięzcy
                    zielony(); // Wywołanie funkcji podświetlającej zwycięski wzorzec na zielono
                    wylacz_guzik(); // Wywołanie funkcji dezaktywującej wszystkie przyciski, aby zakończyć grę
                    alert_wygranego(zwyciezca);
                }
                else if (remis()) // Sprawdzenie remisu
                {
                    win_etykieta.Text = "Wynik gry: Remis!"; // Wyświetlenie informacji o remisie
                }
            }
        }


        private void reset_gry_funkcja(object sender, EventArgs e)
        {
            resetuj_gre(); // Wyłączanie aplikacji po kliknięciu przycisku "Wyłącz aplikację"
        }

        private void resetuj_gre()
        {
            // Przywracanie początkowych ustawień gry
            gracz = 1;
            info_etykieta.Text = "Gracz 1 (Kółko) zaczyna";
            win_etykieta.Text = "Wynik gry: Brak zwycięzcy";
            reset.Visible = false; // Ukryj przycisk wyłączenia aplikacji
            gameOver = false;

            // Resetowanie przycisków planszy
            foreach (Button btn in guzik)
            {
                btn.Enabled = true;
                btn.Text = "";
                btn.Tag = 0;
                btn.BackColor = System.Drawing.Color.FromArgb(220, 220, 220);
            }
        }


        private void alert_wygranego(string winner)
        {
            if (gameOver == false)
            {
                win_etykieta.Text = "Wynik gry: " + winner + " wygrywa!";
                MessageBox.Show("Gratulacje, " + winner + " wygrał!", "Koniec gry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset.Visible = true; // Po zakończeniu gry pokazujemy przycisk wyłączenia aplikacji
                gameOver = true;
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
