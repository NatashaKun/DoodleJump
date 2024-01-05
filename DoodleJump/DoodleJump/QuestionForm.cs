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
    public partial class QuestionForm : Form
    {
        public bool correct; // верный ли ответ
        static Random r = new Random();
        public int index = r.Next(0, Form1.questions.Count);
        public QuestionForm()
        {
            InitializeComponent();
            // присваиваем элементам текст выбранного вопроса
            label1.Text = Form1.questions[index].text;
            button1.Text = Form1.questions[index].answers[0].text;
            button2.Text = Form1.questions[index].answers[1].text;
            button3.Text = Form1.questions[index].answers[2].text;
            button4.Text = Form1.questions[index].answers[3].text;
        }
        private void QuestionForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            correct = Form1.questions[index].answers[0].correct; // получаем true или false
            this.Close(); // закрываем форму
        }

        private void button2_Click(object sender, EventArgs e)
        {
            correct = Form1.questions[index].answers[1].correct;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            correct = Form1.questions[index].answers[2].correct;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            correct = Form1.questions[index].answers[3].correct;
            this.Close();

        }
    }
}
