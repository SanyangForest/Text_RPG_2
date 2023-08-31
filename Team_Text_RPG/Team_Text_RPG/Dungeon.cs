using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rpg
{
    
    internal class Dungeon
    {
         
        private static List<Monster> RandomMonsterList = new List<Monster>();
        public static int FiledMonsterCount = 0;
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
        public void Init()
        {
            RandomMonsterList.Clear();
            FiledMonsterCount = 0;
        }

        public void Battle()
        {
            if(FiledMonsterCount>0)
            {
                Init();
            }

          


            Monster[] monsters = { new Monster("대포미니언", 2, 10, 200, 200),   new Monster("미니언", 1, 5, 100, 100),   new Monster("공허충", 3, 10, 105, 300) };

            Random random = new Random();

            int numberOfMonsters = random.Next(1, 3);

            Console.WriteLine($"{numberOfMonsters}마리의 몬스터가 등장했다!");


            int RandomIndex = random.Next(monsters.Length);
            Monster randomMonster = monsters[RandomIndex];

            for (int i = 0; i < numberOfMonsters; i++)
            {
                RandomIndex = random.Next(monsters.Length);

                randomMonster = new Monster(monsters[RandomIndex].Name, monsters[RandomIndex].Level, monsters[RandomIndex].Atk, monsters[RandomIndex].Hp, monsters[RandomIndex].Gold);//중복되는 몬스터는 서로다른 객체 상태로 존재하기위해

                RandomMonsterList.Add( randomMonster);
                randomMonster.StatusRender(randomMonster.Name);

            }
           
            Console.ReadLine();
            Console.Clear();

        
            // 리스트를 순회하면서  상태 체크  << 비추 
            // 팔드위에 몬스트 수를   체크하면서 0 이되면 << 이부분 

            while (FiledMonsterCount <= 0 || Program.player.IsDeath() == false)//RandomMonsterList 의 요소가 전부 True일때  
            {
                int MonsterCount = 1;
                Console.WriteLine("=================================================================================================");
                foreach (Monster ArrRandomMonster in RandomMonsterList)
                {
                    string DethStat = ArrRandomMonster.DethMonster ? "사망" : " ";
                    Console.WriteLine($"{MonsterCount}   {ArrRandomMonster.Name}  Atk:{ArrRandomMonster.Atk},Hp{ArrRandomMonster.Hp} {DethStat}");
                    MonsterCount++;              

                    FiledMonsterCount++;
                }
                Console.WriteLine("=================================================================================================");


                if (Program.player.IsDeath() == false)
                {
                    for (int i = 0; i < RandomMonsterList.Count; i++)
                    {
                        if (RandomMonsterList[i].IsDeath()==false)
                        {
                            RandomMonsterList[i].BattleLogic(Program.player);
                            if (Program.player.IsDeath() == true)
                            {                                                             
                                Death();
                            }
                        }
                        else
                        {
                            FiledMonsterCount--;
                        }
                       
                    }
                }
                Console.WriteLine("공격할 몬스터를 선택해주세요");



                int InputMonster = Program.CheckValidInput(1, RandomMonsterList.Count)-1;




                Console.WriteLine("1. 때린다");
                     Console.WriteLine("2. 회복");
                     Console.WriteLine("3. 도망간다");

              
                   




                    int input = Program.CheckValidInput(1, 3);
                    switch (input)
                    {
                        case 1:
                            Program.player.BattleLogic(RandomMonsterList[InputMonster]);
                            break;
                        case 2:
                        Program.player.Heal();
                            break;
                        case 3:                           
                            Program.player.Heal();
                            Program.DisplayGameIntro();
                            break;
                    }

                    Console.ReadLine();
                if (Program.player.IsDeath() == true)
                {
                    Console.Clear();
                    Death();
                }
                if (randomMonster.IsDeath() == true)
                {
                    Console.Clear();
                    reward();
                }

                Console.Clear();

                }
 
               
        
        }
       
            public void Death()
        {
            Console.WriteLine("사  망");
           
            Program.player.Heal();

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
            Console.WriteLine($"보상 100 G");
            Console.WriteLine("\n\n");
            Console.WriteLine($"현재Gold {Program.player.Gold+=100}");
            Console.WriteLine("\n\n");
           
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
        public int MaxHp { get; set; }
        public int Hp { get; set; }


        public int Atk { get; set; }

        public int Gold { get; set; }
        public int CriticalChance { get; set; }//치명타 확률 변수 선언
        public int EvadeChance { get; set; }//회피 확률 변수 선언

        public bool IsDeath()
        {
            bool boolDeath = Hp <= 0;
            
            return boolDeath;
        }

        public void StatusRender(string _name)
        {
            Name = _name;
            Console.Write(Name);
            Console.WriteLine($"Atk:{Atk},Hp{Hp}");
            Console.WriteLine($"{Name}가 등장했다!");
            Console.WriteLine("-----------------------------------------------");
        }



        public void BattleLogic(FightUnit OtherUnit)
        {
            Console.WriteLine($"Lv.{Level} {Name} 의 공격!");

            // 치명타 확률 계산
            bool isCritical = new Random().Next(100) < CriticalChance;
            int damage = isCritical ? Atk * 2 : Atk;

            // 회피 확률 계산
            bool isEvaded = new Random().Next(100) < OtherUnit.EvadeChance;

            // 계산식 변경
            if (!isEvaded)
            {
                if (isCritical)
                {
                    Console.WriteLine("치명타!");
                }
                Console.WriteLine($" {OtherUnit.Name} 을(를) 맞췄습니다.  [데미지: {damage}]");
                Console.WriteLine($"Lv.{OtherUnit.Level} {OtherUnit.Name}");
                Console.WriteLine($"HP{OtherUnit.Hp}-> {OtherUnit.Hp -= damage}");
                Console.WriteLine($"HP {OtherUnit.Hp}");
            }
            else
            {
                Console.WriteLine($"{OtherUnit.Name} 의 공격을 회피했습니다!");
            }

        }




    }



    public class Monster : FightUnit
    {

       public bool DethMonster { get; set; }

        public Monster(string name, int level, int atk, int hp, int gold)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Hp = hp;
            Gold = gold;
            CriticalChance = 5; // 치명타 확률: 5%
            EvadeChance = 10; // 회피 확률: 10%
        }
    }

}
