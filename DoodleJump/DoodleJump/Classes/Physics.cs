using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Physics
    {
        public Transform transform; // позиция и размер
        float gravity; // для прыжка
        float a; // ускорение

        public string imgType; // тип картинки
        int sc = 0; // для щита

        public float dx; // изменение по х

        

        public bool usedBonus, fly = false, shield = false; // для бонусов

        public Physics(PointF pos, Size size)
        {
            transform = new Transform(pos, size);
            gravity = 0; // если гравитация положительная персонаж летит вниз если отрицательная то вверх
            a = 0.9f; // каждый раз гравитация изменяется на ускорение так реализовано передвижение по вертикали
            dx = 0;
        }

        public void CalculatePhysics()
        {
            if (dx != 0)
                transform.pos.X += dx; // передвижение по х
            if (transform.pos.Y < 700) // прыжок
            {
                transform.pos.Y += gravity;
                gravity += a;

                if (gravity > -25 && fly)// генерация платформ после ракеты и пружины
                {
                    Controller.startPlatformposY = 200;
                    Controller.GenerateStartPlat();
                    Controller.startPlatformposY = -125;
                    fly = false;
                    
                }

                if (gravity > -5 && usedBonus && !shield)// отключение бонуса после ракеты и пружины
                {
                    usedBonus = false;
                    imgType = "default";
                }

                if(shield && Controller.score - sc > 200)// отключение щита через 10 платформ
                {
                    shield = false;
                    usedBonus = false;
                    imgType = "default";
                    sc = 0;
                }

                Collide(); // взаимодействие персонажа и платформы
            }
        }


        public bool CollidePlayerWithMonsters() //взаимодействие персонажа и монстра
        {
            for (int i = 0; i < Controller.enemies.Count; i++)
            {
                var enemy = Controller.enemies[i];
                PointF delta = new PointF();
                delta.X = (transform.pos.X + transform.size.Width / 2) - (enemy.transform.pos.X + enemy.transform.size.Width / 2);
                delta.Y = (transform.pos.Y + transform.size.Height / 2) - (enemy.transform.pos.Y + enemy.transform.size.Height / 2);
                // если разность середин по х меньше чем сумма половин по х
                if (Math.Abs(delta.X) + 25 <= transform.size.Width / 2 + enemy.transform.size.Width / 2)
                {
                    // аналогично
                    if (Math.Abs(delta.Y) + 25 <= transform.size.Height / 2 + enemy.transform.size.Height / 2)
                    {
                        Controller.RemoveEnemy(i); // удаляем монстра
                        if (shield)// если есть щит - выживаем и удаляем щит
                        {
                            
                            shield = false;
                            usedBonus = false;
                            imgType = "default";
                            sc = 0;
                            return false;
                        }
                        if (!usedBonus) // проиграли
                            return true;
                    }
                }
            }// не коснулись
            return false;
        }


        public bool CollidePlayerWithBonuses() // взаимодействие персонажа и бонусов
        {
            for (int i = 0; i < Controller.bonuses.Count; i++)
            {
                var bonus = Controller.bonuses[i];
                PointF delta = new PointF();
                delta.X = (transform.pos.X + transform.size.Width / 2) - (bonus.transform.pos.X + bonus.transform.size.Width / 2);
                delta.Y = (transform.pos.Y + transform.size.Height / 2) - (bonus.transform.pos.Y + bonus.transform.size.Height / 2);
                // если разность середин по х меньше чем сумма половин по х
                if (Math.Abs(delta.X) + 25 <= transform.size.Width / 2 + bonus.transform.size.Width / 2)
                {
                    // аналогично
                    if (Math.Abs(delta.Y) + 10 <= transform.size.Height / 2 + bonus.transform.size.Height / 2)
                    {
                        //if (!usedBonus && !shield)
                        if(!usedBonus)// если нет бонуса то можем взять
                        {
                            Controller.RemoveBonus(i);
                            switch (bonus.bonusType)
                            {
                                case 1:
                                    shield = true;
                                    usedBonus = true;
                                    imgType = "bubble";
                                    sc = Controller.score;
                                    break;
                                case 2:
                                    imgType = "rocket";
                                    usedBonus = true;
                                    fly = true;
                                    AddForce(60);
                                    Controller.score += 200;
                                    break;
                                case 3:
                                    usedBonus = true;
                                    fly = true;
                                    AddForce(30);
                                    Controller.score += 100;
                                    break;
                            }
                        }

                        return true;
                    }
                }
            }
            return false;
        }


        public void Collide() // касание платформы
        {
            for(int i = 0; i < Controller.platforms.Count; i++)
            {
                var platform = Controller.platforms[i];
                // если середина персонажа по х находится в пределах платформы
                if(transform.pos.X + transform.size.Width/2 + 5 >= platform.transform.pos.X && transform.pos.X + transform.size.Width / 2 - 5 <= platform.transform.pos.X + platform.transform.size.Width)
                {
                    // если по у находится в пределах платформы
                    if(transform.pos.Y + transform.size.Height + 5 >= platform.transform.pos.Y && transform.pos.Y + transform.size.Height + 5 <= platform.transform.pos.Y + platform.transform.size.Height)
                    {
                        if(gravity > 0) // если персонаж сверху
                        {
                            AddForce(10.5f);
                            if (!platform.isTouched) // если раньше не касался 
                            {
                                Controller.score += 20;
                                Controller.GenerateRandomPlatform(); // добавляем новую платформу
                                platform.isTouched = true;
                            }
                        }
                    }
                }
            }
            // взаимодействие со специальными платформами
            for (int i = 0; i < Controller.spPlatforms.Count; i++)
            {
                var spPlatform = Controller.spPlatforms[i];
                // если середина персонажа по х находится в пределах платформы
                if (transform.pos.X + transform.size.Width / 2 + 5 >= spPlatform.transform.pos.X && transform.pos.X + transform.size.Width / 2 - 5 <= spPlatform.transform.pos.X + spPlatform.transform.size.Width)
                {
                    // если по у находится в пределах платформы
                    if (transform.pos.Y + transform.size.Height + 5 >= spPlatform.transform.pos.Y && transform.pos.Y + transform.size.Height + 5 <= spPlatform.transform.pos.Y + spPlatform.transform.size.Height)
                    {
                        if (gravity > 0) // если персонаж сверху
                        {
                            if (spPlatform.plType == 1) // если сломанная
                            {
                                AddForce(10.5f);
                                Controller.spPlatforms.RemoveAt(i); // платформа пропадает
                                Controller.score += 20;
                                Controller.GenerateRandomPlatform();

                            }
                            else// если мнимая
                            {
                                Controller.spPlatforms.RemoveAt(i); // платформа пропадает
                            }
                        }
                    }
                }
            }
        }

        public void AddForce(float value)// прыжки
        {
            gravity = -value;
        }
    }
}
