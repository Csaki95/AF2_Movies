using Project.Controller;
using Project.Model;
using System;
using System.Windows.Forms;

namespace Project.View
{
    public partial class AddMovie : Form
    {
        private readonly FilmController controller;
        private readonly int FilmID;
        private readonly bool isModification;
        // Módosításnál a többi mező engedélyezett-e
        private const bool editFieldEnabled = false;

        // Film létrehozás konstruktor
        public AddMovie(FilmController filmController)
        {
            InitializeComponent();

            this.controller = filmController;
        }

        // Konstruktor film módosításához
        // a feladatleírásban az szerepel, hogy csak a prioritás legyen módosítható, így a többi elem látható, de nem engedélyezett
        public AddMovie(FilmController filmController, Film film) : this(filmController)
        {
            this.Text = "Modify Movie";
            isModification = true;
            FilmID = film.ID;

            titleTextBox.Text = film.Title;
            categoryTextBox.Text = film.Category;
            yearUpDown.Value = film.Year;
            lengthUpDown.Value = film.Length;
            priorityUpDown.Value = film.Priority;

            titleTextBox.Enabled = editFieldEnabled;
            categoryTextBox.Enabled = editFieldEnabled;
            yearUpDown.Enabled = editFieldEnabled;
            lengthUpDown.Enabled = editFieldEnabled;

            addButton.Text = "Modify";

            this.controller = filmController;
        }

        // Oké gomb lenyomása
        // Mivel nem lehet negatív számot megadni a mezőben az nincs ellenőrizve
        private void AddFilm_Click(object sender, EventArgs e)
        {
            // Bevitt adatok mentése
            string title = titleTextBox.Text;
            string category = categoryTextBox.Text;

            int year = (int)yearUpDown.Value;
            int length = (int)lengthUpDown.Value;
            int priority = (int)priorityUpDown.Value;

            if (title == string.Empty || category == string.Empty)
            {
                MessageBox.Show("Kötelező megadni a film címét, és kategóriáját", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Film film = new Film
            {
                Title = title,
                Category = category,
                Year = year,
                Length = length,
                Priority = priority
            };

            bool result;
            if (isModification)
            {
                // Ha a művelet módosítás, és sikeres akkor kilép az ablakból
                result = controller.ModifyFilm(FilmID, priority);
                if(result)
                    DialogResult = DialogResult.Cancel;
            }
            else
            {
                result = controller.AddFilm(film);
            }

            if (!result)
            {
                if (controller.IsInDatabase(title))
                {
                    MessageBox.Show("Nem lehet két azonos című film!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Probléma merült fel a művelet elvégzése közben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Az adatbázis módosítása megtörtént.", "Sikeres Művelet!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
