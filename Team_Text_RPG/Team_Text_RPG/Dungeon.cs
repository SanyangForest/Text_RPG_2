using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Text_Rpg
{
    internal class Dungeon
    {



        Character player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        internal void ChoiceDungeon()
        {


            Console.Clear();
            Console.WriteLine("1. 마을");
            Console.WriteLine("2. 배틀");
            Console.WriteLine("진입하시겠습니까?");

            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    Program.DisplayGameIntro();
                    break;
                case 2:
                    Battle();
                    break;
            }


        }

        public void Battle()
        {
            Monster[] monsters = {
            new Monster("대포미니언", 150, 150, 20,2),
            new Monster("미니언", 100, 100, 10,1),
            new Monster("공허충", 3, 200, 15,3)
        };

            Random random = new Random();
            int randomIndex = random.Next(monsters.Length);
            Monster randomMonster = monsters[randomIndex];

            Console.WriteLine($"{randomMonster.Name}가 등장했다!");
            do
            {

                Console.Clear();
                randomMonster.StatusRender(randomMonster.Name);





                Console.WriteLine("1. 때린다");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 도망간다");
                int input = Program.CheckValidInput(1, 3);
                switch (input)
                {
                    case 1:
                        player.BattleLogic(randomMonster);
                        break;
                    case 2:
                        // 스킬
                        break;
                    case 3:
                        Program.DisplayGameIntro();
                        break;
                }

                Console.ReadLine();
            } while (true);
        }

        enum STARTSELECT
        {
            SLELCTTOWN,
            SLELCBETTLE,
            NONSELECT

        }





    }

    public class FightUnit
    {
        public string Name;
        public int Level { get; set; }

        public int Def { get; set; }
        protected int MaxHp { get; set; }
        public int Hp { get; set; }


        public int Atk { get; set; }

        public int Gold { get; set; }

        public void StatusRender(string _name)
        {
            Name = _name;
            Console.Write(Name);
            Console.WriteLine("의능력치-----------------------------------------------");
            Console.Write("공격력:");
            Console.WriteLine(Atk);
            //50/100
            Console.Write("체력:");
            Console.Write(Hp);
            Console.Write("/");
            Console.WriteLine(MaxHp);
            Console.WriteLine("-----------------------------------------------");
        }
        public void BattleLogic(Monster monster)
        {


            Console.WriteLine($"Lv.{Level} {Name} 의 공격!");
            Console.WriteLine($" {Name} 을(를) 맞췄습니다.  [데미지: {Atk}]");

            Console.WriteLine($"Lv.{Level} {Name}");
            Console.WriteLine($"HP{Hp}-> {Hp -= Atk}");
            Console.WriteLine($"HP {Hp}");


        }


    }



    public class Monster : FightUnit
    {

        public Monster(string Name, int level, int atk, int hp, int gold)
        {


            Level = level;
            Atk = atk;

            Hp = hp;
            Gold = gold;
        }
    }
}
