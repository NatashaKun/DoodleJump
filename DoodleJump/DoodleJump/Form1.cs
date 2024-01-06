using DoodleJump.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoodleJump
{
    public partial class Form1 : Form
    {
        public int num, counter; // номер персонажа, счетчик верных ответов
        public static List<Question> questions = new List<Question>(); // для считывания базы вопросов
        List<string> lines = new List<string>(); // для считывания базы вопросов
        Player player;
        Timer timer1;
        public Form1()
        {
            // считывание вопросов
            using (StreamReader sr = new StreamReader("../../questions.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            for (int i = 0; i < lines.Count; i += 9)
            {

                Question question = new Question(lines[i], lines[i + 1], lines[i + 2], lines[i + 3], lines[i + 4], lines[i + 5], lines[i + 6], lines[i + 7], lines[i + 8]);
                questions.Add(question);
            }

            InitializeComponent();

            Init();
            timer1 = new Timer();
            timer1.Interval = 15;
            timer1.Tick += new EventHandler(Update); // вызывает update через интервал
            timer1.Start();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.BackgroundImage = Properties.Resources.Background;
            this.Height = 600;
            this.Width = 330;
            this.Paint += new PaintEventHandler(OnRepaint);
        }

        public void Init() // начало новой игры
        {
            // форма выбора персонажа
            CharacterChoice characterChoiceForm = new CharacterChoice();
            characterChoiceForm.ShowDialog();
            num = characterChoiceForm.numPl; // номер персонажа
            player = new Player(num);

            Controller.platforms.Clear(); // обнуляем список
            Controller.AddPlatform(new PointF(100, 400));// добавляем стартовую платформу
            Controller.startPlatformposY = 400;
            Controller.score = 0;
            counter = 0;
            Controller.GenerateStartPlat(); // генерируем еще 10
            Controller.bullets.Clear();
            Controller.enemies.Clear();
            Controller.bonuses.Clear();
            Controller.spPlatforms.Clear();

        }

        private void OnKeyboardUp(object sender, KeyEventArgs e) // обнуление dx при отпускании клавиши 
        {
            player.physics.dx = 0;
            
            switch (e.KeyCode.ToString())
            {
                case "Space":// выстрел только когда отпускаем пробел
                    if (!player.physics.usedBonus || player.physics.shield)
                    {
                        if (player.physics.shield)// если есть щит
                            player.physics.imgType = "bubble";
                        else
                            player.physics.imgType = "default";
                        // создаем пулю
                        Controller.CreateBullet(new PointF(player.physics.transform.pos.X + player.physics.transform.size.Width / 2 - 10, player.physics.transform.pos.Y));
                    }
                    break;
            }
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e) // перемещение по х
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    player.physics.dx = 10;
                    break;
                case "Left":
                    player.physics.dx = -10;
                    break;
                case "Space":// замена картинки по нажатию
                    if (!player.physics.usedBonus || player.physics.shield)
                    {
                        if (player.physics.shield) // если есть щит
                            player.physics.imgType = "bubbleShoot";
                        else
                            player.physics.imgType = "shoot"; 
                    }
                    break;
            }
        }

        private void Update(object sender, EventArgs e) // обновление
        {
            this.Text = "NSTU Jump" ;
            label1.Text = "score - " + Controller.score;
            // врезался в монстра
            if (player.physics.CollidePlayerWithMonsters())   
            {
                timer1.Stop();

                if (counter == 0) // если еще не отвечал на вопрос
                {
                    QuestionForm questionForm = new QuestionForm(); // форма с вопросом
                    questionForm.ShowDialog();
                    if (questionForm.correct) // правильный ответ
                    {
                        counter++; // счетчик выпадания вопроса
                        questionForm.Close();
                        if (player.physics.transform.pos.Y >= Controller.platforms[0].transform.pos.Y + 200)
                            player.physics.AddForce(23); // прыжок обратно если упал
                        timer1.Start();
                    }
                    else // неправильный ответ
                    {
                        Init(); // проиграл
                        timer1.Start();
                    }
                }
                else
                {
                    Init(); // проиграл
                    timer1.Start();
                }
            }
            // если упал
            if ((player.physics.transform.pos.Y >= Controller.platforms[0].transform.pos.Y + 200))
            {
                timer1.Stop();
                Init(); // проиграл
                timer1.Start();
            }

            player.physics.CollidePlayerWithBonuses(); // проверка на сбор бонусов
            // проверка картинки
            switch (player.physics.imgType)
            {
                case "default":
                    player.image = player.img;
                    break;
                case "bubble":
                    player.image = player.bubbleImg;
                    break;
                case "rocket":
                    player.image = player.rocketImg;
                    break;
                case "shoot":
                    player.image = player.shootImg;
                    break;
                case "bubbleShoot":
                    player.image = player.bubbleShootImg;
                    break;
            }
            
            if (Controller.bullets.Count > 0) // наоичие пуль
            {
                for (int i = 0; i < Controller.bullets.Count; i++)
                {// удаление пуль если они далеко от персонажа
                    if (Math.Abs(Controller.bullets[i].transform.pos.Y - player.physics.transform.pos.Y) > 500)
                    {
                        Controller.RemoveBullet(i);
                        continue;
                    }
                    Controller.bullets[i].Move(); // движение пуль вперед
                }
            }
            // удаление монстров если в них попала пуля
            if (Controller.enemies.Count > 0) // есть монстр
            {
                for (int i = 0; i < Controller.enemies.Count; i++)
                {
                    if (Controller.enemies[i].StandartCollide()) // попадание пули в монстра
                    {
                        Controller.RemoveEnemy(i);
                        break;
                    }
                }
            }

            player.physics.CalculatePhysics(); // применение физики
            FollowPlayer(); // обновление позиций графики
            Invalidate(); // перерисовка
        }

        public void FollowPlayer() // передвижение игрока и платформ
        {
            int offset = 400 - (int)player.physics.transform.pos.Y; // число на которое будем перемещать
            player.physics.transform.pos.Y += offset;

            for(int i = 0; i < Controller.platforms.Count; i++)
            {
                var platform = Controller.platforms[i];
                platform.transform.pos.Y += offset;
            }

            for (int i = 0; i < Controller.bullets.Count; i++)
            {
                var bullet = Controller.bullets[i];
                bullet.transform.pos.Y += offset;
            }

            for (int i = 0; i < Controller.enemies.Count; i++)
            {
                var enemy = Controller.enemies[i];
                enemy.transform.pos.Y += offset;
            }

            for (int i = 0; i < Controller.bonuses.Count; i++)
            {
                var bonus = Controller.bonuses[i];
                bonus.transform.pos.Y += offset;
            }

            for (int i = 0; i < Controller.spPlatforms.Count; i++)
            {
                var spPlatform = Controller.spPlatforms[i];
                spPlatform.transform.pos.Y += offset;
            }
        }

        private void OnRepaint(object sender, PaintEventArgs e) // обновление графики
        {
            Graphics g = e.Graphics;
            if (Controller.platforms.Count > 0)
            {
                for (int i = 0; i < Controller.platforms.Count; i++)
                    Controller.platforms[i].Draw(g);
            }
            if (Controller.bullets.Count > 0)
            {
                for (int i = 0; i < Controller.bullets.Count; i++)
                    Controller.bullets[i].Draw(g);
            }
            if (Controller.enemies.Count > 0)
            {
                for (int i = 0; i < Controller.enemies.Count; i++)
                    Controller.enemies[i].Draw(g);
            }
            if (Controller.bonuses.Count > 0)
            {
                for (int i = 0; i < Controller.bonuses.Count; i++)
                    Controller.bonuses[i].Draw(g);
            }
            if (Controller.spPlatforms.Count > 0)
            {
                for (int i = 0; i < Controller.spPlatforms.Count; i++)
                    Controller.spPlatforms[i].Draw(g);
            }

            player.Draw(g);
        }
    }
}
