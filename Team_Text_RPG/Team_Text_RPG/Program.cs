using System;
using System.Collections.Generic;
using Text_Rpg;
internal class Program //커밋 해보기

{

    internal static Character player;
    internal static Inventory myinventory;
    internal static Equipment equipmentitem;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사1", 1, 10, 5, 100, 1500);

        // 인벤토리 정보 세팅
        myinventory = new Inventory();
        equipmentitem = new Equipment(0, 0, 0);

        // 아이템 정보 세팅
    }

    internal static void DisplayGameIntro()
    {
        Dungeon dungeon = new Dungeon();

        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 던전");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
            case 3:
                dungeon.ChoiceDungeon();
                break;
        }
    }
    static void Shop()
    {
        Console.Clear();
        Console.WriteLine("상점에 오신 것을 환영합니다 !");
        Console.WriteLine("아래에 물건을 구매하실 수 있습니다.");
        Console.WriteLine("현재 소지액: " + player.Gold);

    }
    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

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

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine($"- 1   ");
        Console.WriteLine($"- 2   ");
        Console.WriteLine();
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

        static void DisplayEquipItem()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine($"-1 ");
            Console.WriteLine($"-2 ");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;

                case 1:
                    DisplayEquipItem();
                    break;

                case 2:
                    DisplayEquipItem();
                    break;
            }
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

            Console.WriteLine("잘못된 입력입니다.");
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
}

public class Character : FightUnit
{
    //public string Name { get; }
    public string Job { get; }
    //public int Level { get; }
    //public int Atk { get; }
    //public int Def { get; }
    //public int Hp { get; }
    //public int Gold { get; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}
