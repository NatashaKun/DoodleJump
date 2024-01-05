using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Question
    {
        public string text; // текст вопросов
        public List<Answer> answers = new List<Answer>(); // список ответов

        public Question(string text, string ans1, string cor1, string ans2, string cor2, string ans3, string cor3, string ans4, string cor4)
        {
            this.text = text;
            Answer answer1 = new Answer(ans1, cor1);
            answers.Add(answer1);
            Answer answer2 = new Answer(ans2, cor2);
            answers.Add(answer2);
            Answer answer3 = new Answer(ans3, cor3);
            answers.Add(answer3);
            Answer answer4 = new Answer(ans4, cor4);
            answers.Add(answer4);
        }
    }
}
