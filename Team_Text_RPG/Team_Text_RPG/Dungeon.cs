using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rpg
{
    internal class Dungeon
    {     
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
            new Monster("대포미니언", 2, 10, 200,200),
            new Monster("미니언", 1, 5, 100,100),
            new Monster("공허충", 3, 10, 105,300)
        };

            Random random = new Random();
            int randomIndex = random.Next(monsters.Length);
            Monster randomMonster = monsters[randomIndex];

            Console.WriteLine($"{randomMonster.Name}가 등장했다!");
            while (randomMonster.IsDeath() == false && Program.player.IsDeath() == false)
            {
                Console.Clear();

                randomMonster.StatusRender(randomMonster.Name);


                if (Program.player.IsDeath() == false)
                {
                    randomMonster.BattleLogic(Program.player);
                }





                Console.WriteLine("1. 때린다");
                Console.WriteLine("2. 아이템사용");
                Console.WriteLine("3. 도망간다");
                if (Program.player.IsDeath() == true)
                {


                    bool isPlayerDead = Program.player.IsDeath();
                    isPlayerDead = false;

                    Console.Clear();
                    Death();




                }
                if (randomMonster.IsDeath() == true)
                {

                    bool isMonsterDead = randomMonster.IsDeath();
                    isMonsterDead = false;
                    Console.Clear();
                    reward();



                }


                int input = Program.CheckValidInput(1, 3);
                switch (input)
                {
                    case 1:
                        Program.player.BattleLogic(randomMonster);
                        break;
                    case 2:
                        // 스킬
                        break;
                    case 3:
                        Program.DisplayGameIntro();
                        break;
                }

                Console.ReadLine();

            }

            Console.WriteLine("싸움이끝났습니다");
            Console.ReadLine();
            Program.DisplayGameIntro();
        }


        public void Death()
        {
            Console.WriteLine("사  망");


            Console.WriteLine("1. 다시");
            Console.WriteLine("2. 마을로귀환");
            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    ChoiceDungeon();

                    break;
                case 2:
                    Program.DisplayGameIntro();
                    break;
            }
        }

        public void reward()
        {
            Console.WriteLine("보상에관련된 로직");



            Console.WriteLine("1. 다시");
            Console.WriteLine("2. 마을로귀환");
            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    ChoiceDungeon();

                    break;
                case 2:
                    Program.DisplayGameIntro();
                    break;
            }

        }






    }

    public class FightUnit
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public int Def { get; set; }
        protected int MaxHp { get; set; }
        public int Hp { get; set; }


        public int Atk { get; set; }

        public int Gold { get; set; }

        public bool IsDeath()
        {
            bool boolDeath = Hp <= 0;

            return boolDeath;
        }

        public void StatusRender(string _name)
        {
            Name = _name;
            Console.Write(Name);
            Console.WriteLine("의능력치-----------------------------------------------");
            Console.Write("공격력:");
            Console.WriteLine(Atk);
            Console.Write("체력:");
            Console.WriteLine(Hp);
            Console.WriteLine("-----------------------------------------------");
        }



        public void BattleLogic(FightUnit OtherUnit)
        {
            Console.WriteLine($"Lv.{Level} {Name} 의 공격!");
            Console.WriteLine($" {OtherUnit.Name} 을(를) 맞췄습니다.  [데미지: {Atk}]");
            Console.WriteLine($"Lv.{OtherUnit.Level} {OtherUnit.Name}");
            Console.WriteLine($"HP{OtherUnit.Hp}-> {OtherUnit.Hp -= Atk}");
            Console.WriteLine($"HP {OtherUnit.Hp}");
        }


    }



    public class Monster : FightUnit
    {

        public Monster(string name, int level, int atk, int hp, int gold)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Hp = hp;
            Gold = gold;
        }
    }

}
