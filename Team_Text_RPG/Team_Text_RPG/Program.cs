
namespace Team_Text_RPG
{
    internal class Program 
    {
        public static Character player;
        internal static Inventory myinventory;
        internal static Equipment equipmentitem;
        private static string userName;
        public static Item sword;
        public static Item chainmail;
        public static Item bow;
        public static Item clotharmor;
        public static Item dagger;
        public static Item leatherarmor;

        public enum Jobs
        {
            warrior = 1,
            archer,
            thief
        }
        static void Main(string[] args)
        {
            GameStartScene();
            DisplayGameIntro();
        }
        static void GameStartScene() // 게임 시작 화면에서 유저입력 - 이름 설정, 직업 선택
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ! ~ Dungeon Of Sparta ~ ! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write(" 이름을 입력해주세요 : ");
            userName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(" 직업을 선택해주세요 ");
            Console.WriteLine();
            Console.WriteLine(" 1: 전사 2: 궁수 3: 도적 ");
            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    GameDataSetting(Jobs.warrior);
                    break;
                case 2:
                    GameDataSetting(Jobs.archer);
                    break;
                case 3:
                    GameDataSetting(Jobs.thief);
                    break;
            }
        }
        static void GameDataSetting(Jobs jobs)
        {
            // 초기 아이템 추가 부탁드립니다 - kim to jang
            switch (jobs)
            {
                case Jobs.warrior:
                    player = new Character(userName, "전사", 1, 10, 10, 150, 2000, 0.0f);
                    Console.WriteLine($" 환영합니다 !!! {userName} 님, 전사를 선택하셨습니다.");
                    // ex - 검, 사슬 갑옷
                    break;
                case Jobs.archer:
                    player = new Character(userName, "궁수", 1, 15, 5, 100, 2000, 0.0f);
                    Console.WriteLine($" 환영합니다 !!! {userName} 님, 궁수를 선택하셨습니다.");
                    // ex - 활, 천 갑옷
                    break;
                case Jobs.thief:
                    player = new Character(userName, "도적", 1, 12, 8, 130, 2000, 0.0f);
                    Console.WriteLine($" 환영합니다 !!! {userName} 님, 도적를 선택하셨습니다.");
                    // ex - 단검, 가죽 갑옷
                    break;
            }
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.Write(" 캐릭터 생성 중 ");
            Loading();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" 캐릭터 생성이 완료되었습니다, 마을로 진입합니다. ");
            Thread.Sleep(2000);


            // 인벤토리 정보 세팅
            myinventory = new Inventory();
            equipmentitem = new Equipment(0, 0, 0);

            //아이템 정보 세팅
            sword = new Item("철검", "기초적인 철검", 1, 0, 0, false);
            chainmail = new Item("사슬 갑옷", "기초적인 사슬 갑옷", 0, 0, 0, false);
            bow = new Item("나무 활", "기초적인 나무 활", 1, 0, 0, false);
            clotharmor = new Item("천 갑옷", "기초적인 천 갑옷", 0, 1, 0, false);
            dagger = new Item("단검", "기초적인 단검", 1, 0, 0, false);
            leatherarmor = new Item("가죽 갑옷", "기초적인 가죽 갑옷", 0, 1, 0, false);
        }

        static void Loading() // 로딩 함수 추가
        {
            int i = 0;
            while (i < 3)
            {
                Thread.Sleep(500);
                Console.Write(" ▷");
                i++;
            }
        }
        internal static void DisplayGameIntro()
        {
            Dungeon dungeon = new Dungeon();

            Console.Clear();

            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ! ~ Dungen of Cheating City ~ ! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" 커닝시티에 오신 여러분 환영합니다! ");
            Console.WriteLine(" 마을에서 할 수 있는 활동입니다. ");
            Console.WriteLine();
            Console.WriteLine(" 1. 상태 보기 ");
            Console.WriteLine(" 2. 가방 보기 ");
            Console.WriteLine(" 3. 상점 보기 ");
            Console.WriteLine(" 4. 던전 가기 ");
            Console.WriteLine();
            Console.WriteLine(" 원하시는 행동을 입력해주세요! "); ;

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    Console.WriteLine(" 상태 창 이동 중 ");
                    Loading();
                    DisplayMyInfo();
                    break;

                case 2:
                    Console.WriteLine(" 가방 이동 중 ");
                    Loading();
                    DisplayInventory();
                    break;
                case 3:
                    Console.WriteLine(" 상점 이동 중 ");
                    Loading();
                    Shop();
                    break;
                case 4:
                    Console.WriteLine(" 던전 이동 중 ");
                    Loading();
                    dungeon.ChoiceDungeon();
                    break;
            }
        }
        static void DisplayMyInfo()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ~ 상태 ~ ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" 캐릭터의 정보를 표시합니다. ");
            Console.WriteLine();
            Console.WriteLine($" 이름 : {player.Name} ");
            Console.WriteLine($" 직업 : {player.Job} ");
            Console.WriteLine($" 레벨 : {player.Level} ");
            Console.Write($" 공격력 : {player.Atk} (+{equipmentitem.AddAtk})");
            Console.WriteLine();
            Console.Write($" 방어력 : {player.Def} (+{equipmentitem.AddDef})");
            Console.WriteLine();
            Console.WriteLine($" 돈 : {player.Gold}G ");
            Console.WriteLine();
            Console.WriteLine(" 0. 나가기 ");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }
        static void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ~ 가방 ~ ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" 보유 중인 아이템을 관리할 수 있습니다. ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" [아이템 목록] ");
            Console.ResetColor();

            myinventory.ItemDisplay();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    DisplayEquipItem();
                    break;
            }   
        

        

        }
        static void Shop()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ~ 상점 ~ ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" 아이템을 구매/판매할 수 있습니다. ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($" [보유 골드] \t{player.Gold} G ");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" [아이템 목록] ");
            Console.ResetColor();
            Console.WriteLine();
            // 아이템 목록 출력 test.. 
            Console.WriteLine("1. 도란검. 100메소");
            Console.WriteLine("2. 도란방패. 150메소");
            Console.WriteLine("3. 도란반지. 200메소");
            Console.WriteLine("4. 삼위일체. 333메소");
            Console.WriteLine("5. 판매하기");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine(" 1. 구매하기 ");
            Console.WriteLine(" 2. 판매하기 ");
            Console.WriteLine();
            Console.WriteLine(" 0. 나가기 ");
            Console.WriteLine();
            Console.WriteLine(" 원하시는 행동을 입력해주세요! ");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    // 구매하기
                    break;
                case 2:
                    // 판매하기
                    break;

            }

        }
        static void DisplayEquipItem()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            
            myinventory.ItemDisplay();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, myinventory.InventoryItem.Count);
            if (input == 0)
            {
                DisplayInventory();
            }
            else
            {
                if (myinventory.InventoryItem[input - 1].IsEquip == false)
                {
                    equipmentitem.EquipingItem(myinventory.InventoryItem[input - 1]);
                }
                else
                {
                    equipmentitem.UnEquipingItem(myinventory.InventoryItem[input - 1]);
                }
                DisplayEquipItem();
            }
        }
        internal static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine(" 잘못된 입력입니다! 다시 입력해주세요. ");
            }
        }
        public class Inventory
        {
            public List<Item> InventoryItem = new List<Item>();
            public void AddItemInventory(Item item)
            {
                InventoryItem.Add(item);
            }
            public void RemoveItemInventory(Item item)
            {
                InventoryItem.Remove(item);
            }
            public void ItemDisplay()
            {
                for (int i = 0; myinventory.InventoryItem.Count != i; i++)
                {
                    string ATK = "";
                    string DEF = "";
                    string HP = "";

                    if (myinventory.InventoryItem[i].Atk > 0)
                    {
                        ATK = $"| 공격력 +{myinventory.InventoryItem[i].Atk}";
                    }
                    else
                    {
                        ATK = "";
                    }

                    if (myinventory.InventoryItem[i].Def > 0)
                    {
                        DEF = $"| 방어력 +{myinventory.InventoryItem[i].Def}";
                    }
                    else
                    {
                        DEF = "";
                    }

                    if (myinventory.InventoryItem[i].Hp > 0)
                    {
                        HP = $"| 체력 +{myinventory.InventoryItem[i].Hp}";
                    }
                    else
                    {
                        HP = "";
                    }

                    Console.WriteLine($"-{i + 1} {(myinventory.InventoryItem[i].IsEquip ? "[E]" : "")}{myinventory.InventoryItem[i].Name} {ATK} {DEF} {HP} | {myinventory.InventoryItem[i].Info}");
                }
            }
        }

        public class Equipment
        {
            public List<Item> EquipItems = new List<Item>();
            public void EquipingItem(Item item)
            {
                EquipItems.Add(item);
                equipmentitem.AddEquipItem();
                item.IsEquip = !item.IsEquip;
            }
            public void UnEquipingItem(Item item)
            {
                EquipItems.Remove(item);
                equipmentitem.AddEquipItem();
                item.IsEquip = !item.IsEquip;
            }
            public void AddEquipItem()
            {
                AddAtk = AddDef = AddHp = 0;
                for (int i = 0; EquipItems.Count != i; i++)
                {
                    AddAtk += EquipItems[i].Atk;
                    AddDef += EquipItems[i].Def;
                    AddHp += EquipItems[i].Hp;
                }
            }
            public int AddAtk { get; set; }
            public int AddDef { get; set; }
            public int AddHp { get; set; }

            public Equipment(int addatk, int adddef, int addhp)
            {
                AddAtk = addatk;
                AddDef = adddef;
                AddHp = addhp;
            }
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Info { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public bool IsEquip { get; set; }

        public Item(string name, string info, int atk, int def, int hp, bool isEquip)
        {
            Name = name;
            Info = info;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquip = isEquip;
        }
    }

    public class Character : FightUnit
    {
       
        public string Job { get; }
        public float Exp { get; set; }
        public float MaxExp { get; set; }

        public void Heal()
        {
            MaxHp = Level * 100;
            Hp = MaxHp;
        }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold, float exp)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            Exp = exp;

            CriticalChance = 50; // 치명타 확률: 50%
            EvadeChance = 10; // 회피 확률: 10%
        }
    }    
}

