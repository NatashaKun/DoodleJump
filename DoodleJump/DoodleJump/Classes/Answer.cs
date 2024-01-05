using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Answer
    {
        public string text; // текст ответа
        public bool correct; // верный ли он

        public Answer(string text, string correct)
        {
            this.text = text;
            this.correct = bool.Parse(correct);
        }
    }
}
