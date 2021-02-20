using Project.Controller;
using Project.DAO;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.View
{
    public partial class Main : Form
    {

        private readonly FilmController filmController;
        // Debugoláshoz látható legyen-e az ID, és Prioritás
        private const bool isVisible = false;

        public Main()
        {
            InitializeComponent();
            this.toolStripMenuItem1.Click += AddFilmMenuItem_Click;
            this.toolStripMenuItem2.Click += ListFilmMenuItem_Click;

            filmController = new FilmController(new FilmDBDao());
        }

        // Adatbázis kilistázása, ID, és priorítás nem látható, de elérhető
        private void ListFilmMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<Film> films = filmController.GetFilms();
            filmGridView.DataSource = null;
            filmGridView.DataSource = films;
            filmGridView.Columns["ID"].Visible = isVisible;
            filmGridView.Columns["Priority"].Visible = isVisible;
            filmGridView.Visible = true;
        }

        // Film hozzáadás ablak
        private void AddFilmMenuItem_Click(object sender, EventArgs e)
        {
            using var addFilmForm = new AddMovie(filmController);
            addFilmForm.ShowDialog();
        }

        // Film módosítás ablak
        private void ItemClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!(filmGridView.CurrentRow.DataBoundItem is Film film))
                return;

            using var addFilmForm = new AddMovie(filmController, film);
            addFilmForm.ShowDialog();
        }
    }
}
