using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public static class Controller // манипуляция объектами
    {
        public static List<Platform> platforms = new List<Platform>();
        public static List<SpPlatform> spPlatforms = new List<SpPlatform>();
        public static List<Bullet> bullets = new List<Bullet>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Bonus> bonuses= new List<Bonus>();
        public static int startPlatformposY = 400; // для спавна платформ
        public static int score = 0; // очки
        public static Transform transform; // позиция и размер


        public static void CreateBullet(PointF pos) // создание пули
        {
            var bullet = new Bullet(pos);
            bullets.Add(bullet);
        }

        public static void CreateBonus(PointF pos)
        {
            Random r = new Random();
            var bonusType = r.Next(1, 4);// тип бонуса

            switch (bonusType)
            {
                case 1: // щит
                    var bonus = new Bonus(new PointF(pos.X + 10, pos.Y - 30), bonusType);
                    bonuses.Add(bonus);
                    break;
                case 2: // ракета
                    bonus = new Bonus(new PointF(pos.X, pos.Y - 50), bonusType);
                    bonuses.Add(bonus);
                    break;
                case 3: // пружина
                    bonus = new Bonus(new PointF(pos.X + 20, pos.Y - 10), bonusType);
                    bonuses.Add(bonus);
                    break;
            }
        }

        public static void CreateEnemy(PointF pos) // создание монстра
        {
            // для лучшей отрисовки (чтобы монстр не был под платформой) 
            pos.X -= 10;
            pos.Y -= 50;
            Random r = new Random();
            var enemyType = r.Next(1, 5); // тип монстра

            switch (enemyType) 
            {
                case 1:
                    var enemy = new Enemy(pos, enemyType);
                    enemies.Add(enemy);
                    break;
                case 2:
                    enemy = new Enemy(pos, enemyType);
                    enemies.Add(enemy);
                    break;
                case 3:
                    enemy = new Enemy(pos, enemyType);
                    enemies.Add(enemy);
                    break;
                case 4:
                    enemy = new Enemy(pos, enemyType);
                    enemies.Add(enemy);
                    break;
            }
        }


        public static void AddPlatform(PointF pos) // добавление платформы
        {
            Platform platform = new Platform(pos);
            platforms.Add(platform);
        }

        public static void GenerateStartPlat() // генерируем стартовые 10 платформ
        {
            Random r = new Random();
            for(int i = 0; i < 10; i++)
            {
                int x = r.Next(20, 240);
                int y = r.Next(55, 60);
                startPlatformposY -= y;// каждая новая платформа всё выше и выше
                PointF pos = new PointF(x, startPlatformposY);
                AddPlatform(pos);
            }
        }
              
        
        public static void GenerateRandomPlatform() // генерация последующих платформ
        {
            Clear(); // удаляем лишнее 
            Random r = new Random();
            int x = r.Next(20, 240);
            int posX = 0; // для спавна доп платформ

            PointF pos = new PointF(x, startPlatformposY + 100);
            AddPlatform(pos);

            var c = r.Next(1, 4);

            switch (c) // рандомный выбор для спавна монстров и бонусов
            {
                case 1:// монстр
                    c = r.Next(1, 5);
                    if (c == 1)
                    {
                        CreateEnemy(pos);
                    }
                    break;
                case 2:// бонус
                    c = r.Next(1, 10);
                    if (c == 1)
                    {
                        CreateBonus(pos);
                    }
                    break;
                case 3:// доп платформа
                    c = r.Next(1, 10);
                    // для того чтобы избежать наложения доп платформы на обычную
                    switch (pos.X > 130)
                    {
                        case false:
                            posX = r.Next(195, 240);
                            break;
                        case true:
                            posX = r.Next(20, 65);
                            break;
                    }
                    if (c == 1)
                    {
                        var spPlatform = new SpPlatform(new PointF(posX, startPlatformposY + 100), c);
                        spPlatforms.Add(spPlatform);
                    }
                    if(c == 2)
                    {
                        var spPlatform = new SpPlatform(new PointF(posX, startPlatformposY + 100), c);
                        spPlatforms.Add(spPlatform);
                    }
                    break;
            }
        }

        public static void RemoveEnemy(int i) // удаление монстра
        {
            enemies.RemoveAt(i);
        }

        public static void RemoveBonus(int i) // удаление бонуса
        {
            bonuses.RemoveAt(i);
        }

        public static void RemoveBullet(int i) // удаление пули
        {
            bullets.RemoveAt(i);
        }

        public static void Clear() // удаление лишнего
        {
            platforms.RemoveAll(platform => platform.transform.pos.Y >= 700);
            spPlatforms.RemoveAll(spPlatform => spPlatform.transform.pos.Y >= 700);
            bullets.RemoveAll(bullet => bullet.transform.pos.Y >= 700);
            enemies.RemoveAll(enemy => enemy.transform.pos.Y >= 700);
            bonuses.RemoveAll(bonus => bonus.transform.pos.Y >= 700);
        }
    }
}
