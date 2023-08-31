using System;
using System.Collections.Generic;

internal class Program //커밋
{
    private static Character player;

    static void Main(string[] args)
    {
        Setting();
        Intro();
    }

    static void Setting()
    {
        player = new Character("Jamnini", "도적", 279, 2763, 37, 420, 1000);
        player.AddItem("롱 소드");
        player.AddItem("야만의 몽둥이");
    }

    static void Intro()
    {
        Console.Clear();
        Console.WriteLine("커닝시티에 오신 것을 환영합니다");
        Console.WriteLine("파티퀘스트 '첫번째 동행' 에 들어가기 전 활동을 하실 수 있습니다.");
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점가기");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                Info();
                break;
            case 2:
                Inventory();
                break;
            case 3:
                Shop();
                break;
        }
    }

    static void Info()
    {
        Console.Clear();
        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 능력치를 표시합니다.");
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} ");
        Console.WriteLine("0. 나가기");

        CheckValidInput(0, 0);
        Intro();
    }

    static void Inventory()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");

        Console.WriteLine("보유한 아이템 목록:");
        foreach (string item in player.Inventory)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n0. 나가기");

        CheckValidInput(0, 0);
        Intro();
    }

    static void Shop()
    {
        Console.Clear();
        Console.WriteLine("상점에 오신 것을 환영합니다");
        Console.WriteLine("아래에 물품을 구매 또는 판매 할 수 있습니다");
        Console.WriteLine("현재 소지액: " + player.Gold);
        Console.WriteLine("1. 도란검. 100메소");
        Console.WriteLine("2. 도란방패. 150메소");
        Console.WriteLine("3. 도란반지. 200메소");
        Console.WriteLine("4. 삼위일체. 333메소");
        Console.WriteLine("5. 판매하기");
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 5);
        switch (input)
        {
            case 0:
                Intro();
                break;
            case 1:
                BuyItem("도란검", 100);
                break;
            case 2:
                BuyItem("도란방패", 150);
                break;
            case 3:
                BuyItem("도란링", 200);
                break;
            case 4:
                BuyItem("삼위일체", 333);
                break;
            case 5:
                SellItem();
                break;
        }
    }

    static void BuyItem(string itemName, int itemPrice)
    {
        if (player.Gold >= itemPrice)
        {
            player.ModifyGold(-itemPrice);
            Console.WriteLine($"{itemName}을(를) 구매했습니다.");
            Console.WriteLine("남은 소지액: " + player.Gold);
            player.AddItem(itemName);

            Console.WriteLine("아무 키나 누르면 메인 화면으로 돌아갑니다.");
            Console.ReadKey();
            Intro();
        }
        else
        {
            Console.WriteLine("소지금이 부족합니다.");
            Console.WriteLine("아무 키나 누르면 메인 화면으로 돌아갑니다.");
            Console.ReadKey();
            Intro();
        }
    }

    static void SellItem()
    {
        Console.Clear();
        Console.WriteLine("판매할 아이템을 선택하세요:");

        for (int i = 0; i < player.Inventory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Inventory[i]}");
        }

        Console.WriteLine("0. 취소");

        int input = CheckValidInput(0, player.Inventory.Count);
        if (input == 0)
        {
            Shop();
        }
        else
        {
            SellConfirmed(player.Inventory[input - 1]);
        }
    }

    static void SellConfirmed(string itemName)
    {
        int sellPrice = player.GetItemPrice(itemName) / 3;
        Console.Clear();
        Console.WriteLine($"{itemName}을(를) 판매합니다.");
        Console.WriteLine($"판매 가격: {sellPrice}");
        Console.WriteLine("1. 판매하기");
        Console.WriteLine("2. 취소");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                player.ModifyGold(sellPrice);
                player.RemoveItem(itemName);
                Console.WriteLine($"{itemName}을(를) 판매했습니다. 소지금: {player.Gold}");
                Console.WriteLine("아무 키나 누르면 메인 화면으로 돌아갑니다.");
                Console.ReadKey();
                Shop(); // 판매하기 완료 후에는 상점으로 돌아가도록 변경
                break;
            case 2:
                Shop();
                break;
        }
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();
            bool parseSuccess = int.TryParse(input, out int ret);
            if (parseSuccess && ret >= min && ret <= max)
                return ret;
            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; private set; }

    public List<string> Inventory { get; private set; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        Inventory = new List<string>();
    }

    private Dictionary<string, int> itemPrices = new Dictionary<string, int>
    {
        { "도란검", 100 },
        { "도란방패", 150 },
        { "도란링", 200 },
        { "삼위일체", 333 } //추가할시 밑에 이런식으로 할 것
    };

    public int GetItemPrice(string itemName)
    {
        return itemPrices.ContainsKey(itemName) ? itemPrices[itemName] : 0;
    }

    public void ModifyGold(int amount)
    {
        Gold += amount;
    }

    public void AddItem(string itemName)
    {
        Inventory.Add(itemName);
    }

    public void RemoveItem(string itemName)
    {
        Inventory.Remove(itemName);
    }
}
