using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class CharacterChoice : Form
    {
        public int numPl;
        public CharacterChoice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numPl = 1; // номер персонажа
            this.Close(); // закрытие формы
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numPl = 2; 
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numPl = 3;
            this.Close();
        }

        private void CharacterChoice_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Icon1; 
            pictureBox2.Image = Properties.Resources.Icon2; 
            pictureBox3.Image = Properties.Resources.Icon3; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RatingForm ratingForm = new RatingForm(); // Создаем экземпляр второй формы
            ratingForm.Show();
        }
    }
}
