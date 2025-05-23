using System;

namespace lab4
{
    // 1. 定義 Strategy 介面（所有猜數字的行為都要實作這個介面）
    public interface Strategy
    {
        // Next 方法定義：給定目前猜測的區間，返回一個猜的數字
        int Next(int low, int high);
    }

    // 2. Player 抽象類別：每個玩家都要有名字並實作猜數字的行為
    public abstract class Player : Strategy
    {
        public string Name { get; set; }

        // 建構子：接收玩家名稱
        public Player(string name)
        {
            Name = name;
        }

        // 抽象方法：子類別必須實作猜數字的邏輯
        public abstract int Next(int low, int high);
    }

    // 3. HumanPlayer：實作從使用者輸入猜數字的邏輯
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override int Next(int low, int high)
        {
            Console.Write($"請輸入一個數字（{low} 到 {high}）：");
            int guess;
            // 驗證使用者輸入正確且在範圍內
            while (!int.TryParse(Console.ReadLine(), out guess) || guess < low || guess > high)
            {
                Console.Write("輸入錯誤，請重新輸入：");
            }
            return guess;
        }
    }

    // 4. NaiveAI：亂數猜（不記憶、不學習）
    public class NaiveAI : Player
    {
        private Random rand = new Random();

        public NaiveAI(string name) : base(name) { }

        public override int Next(int low, int high)
        {
            return rand.Next(low, high + 1);
        }
    }

    // 5. BinarySearchAI：使用二分搜尋策略猜數字
    public class BinarySearchAI : Player
    {
        public BinarySearchAI(string name) : base(name) { }

        public override int Next(int low, int high)
        {
            return (low + high) / 2; // 每次猜中間值
        }
    }

    // 6. SuperAI：更進階 AI（目前範例與 BinarySearchAI 相同，可自行改進）
    public class SuperAI : Player
    {
        public SuperAI(string name) : base(name) { }

        public override int Next(int low, int high)
        {
            return (low + high) / 2;
        }
    }

    // 7. Game 類別：管理遊戲流程與邏輯
    public class Game
    {
        private int secretNumber; // 隨機產生的秘密數字
        private int low = 1;      // 最低猜測值
        private int high = 100;   // 最高猜測值
        private Player player;    // 玩家物件
        private bool win = false; // 記錄是否猜中

        // 建構子：接收一個 Player，並隨機產生 secretNumber
        public Game(Player player)
        {
            this.player = player;
            secretNumber = new Random().Next(low, high + 1);
        }

        // Run 方法：遊戲主流程
        public void Run()
        {
            Console.WriteLine($"🎮 玩家 {player.Name} 開始猜數字遊戲！");
            while (!win)
            {
                int guess = player.Next(low, high); // 讓玩家猜一個數字
                Console.WriteLine($"{player.Name} 猜了：{guess}");

                // 判斷猜測結果
                if (guess == secretNumber)
                {
                    Console.WriteLine("🎉 猜對了！遊戲結束。");
                    win = true;
                }
                else if (guess < secretNumber)
                {
                    Console.WriteLine("太小了！");
                    low = guess + 1; // 調整最小範圍
                }
                else
                {
                    Console.WriteLine("太大了！");
                    high = guess - 1; // 調整最大範圍
                }
            }
        }
    }

    // 8. 主程式進入點：讓使用者選擇玩家類型並開始遊戲
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請選擇玩家類型：");
            Console.WriteLine("1. HumanPlayer（真人輸入）");
            Console.WriteLine("2. NaiveAI（亂猜 AI）");
            Console.WriteLine("3. BinarySearchAI（二分搜尋 AI）");
            Console.WriteLine("4. SuperAI（進階 AI）");

            Player player = null;
            string choice;

            while (player == null)
            {
                Console.Write("請輸入選項 (1-4)：");
                choice = Console.ReadLine();

                Console.Write("請輸入玩家名稱：");
                string name = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        player = new HumanPlayer(name);
                        break;
                    case "2":
                        player = new NaiveAI(name);
                        break;
                    case "3":
                        player = new BinarySearchAI(name);
                        break;
                    case "4":
                        player = new SuperAI(name);
                        break;
                    default:
                        Console.WriteLine("❌ 選項錯誤，請重新輸入 1 到 4。");
                        break;
                }
            }

            // 建立並執行遊戲
            Game game = new Game(player);
            game.Run();

            Console.WriteLine("🎮 遊戲結束，感謝遊玩！");
        }
    }
}
