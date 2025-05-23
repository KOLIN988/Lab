using System; // 引入系統命名空間，提供基本功能，如輸出入、亂數等
using System.Collections.Generic; // 引入泛型集合，如 List、HashSet



namespace lab2
{
    internal class Program
    {
        // 方法：產生一組隨機的接觸記錄（使用 Fisher–Yates 洗牌演算法）
        static int[] GenerateRandomContacts(int N)
        {
            int[] contacts = new int[N]; // 宣告一個陣列，長度為 N，用來儲存每位公民接觸的對象 ID

            // 初始化 contacts 陣列為 0, 1, 2, ..., N-1
            for (int i = 0; i < N; i++)
                contacts[i] = i;

            Random rand = new Random(); // 建立一個隨機數生成器

            // Fisher–Yates 洗牌：從陣列尾部向前隨機交換元素，保證完全隨機分布
            for (int i = N - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1); // 隨機產生一個 0 ~ i 的整數
                int temp = contacts[i]; // 交換位置 i 和 j 的值
                contacts[i] = contacts[j];
                contacts[j] = temp;
            }

            return contacts; // 傳回隨機打亂後的接觸紀錄陣列
        }


        // 方法：追蹤傳染鏈，從最初感染者（預設是 0）開始
        static List<int> TrackInfectionChain(int[] contacts)
        {
            List<int> chain = new List<int>(); // 用來記錄傳染路徑的 ID 清單
            HashSet<int> visited = new HashSet<int>(); // 用來避免重複拜訪（防止進入循環）

            int current = 0; // 初始感染者為 ID 0
            while (!visited.Contains(current)) // 若此人尚未被拜訪過，繼續追蹤
            {
                chain.Add(current);       // 將目前感染者加入傳染鏈
                visited.Add(current);     // 標記此人已經拜訪過
                current = contacts[current]; // 取得被此人接觸到的對象，繼續追蹤
            }

            return chain; // 傳回整條傳染鏈
        }

                static void Main(string[] args)
        {
            int N = 16; // 公民總數（可以自行修改）

            // 產生接觸記錄（每位公民只接觸一人）
            int[] contacts = GenerateRandomContacts(N);

            // 顯示每位公民的接觸對象
            Console.WriteLine("接觸記錄：");
            for (int i = 0; i < N; i++)
                Console.WriteLine($"公民 {i} 接觸了 公民 {contacts[i]}");

            // 呼叫追蹤方法，找出從 0 號開始的傳染鏈
            List<int> chain = TrackInfectionChain(contacts);

            // 顯示傳染鏈的順序
            Console.WriteLine("\n傳染鏈：");
            foreach (int id in chain)
                Console.Write(id + " ");
            Console.WriteLine(); // 換行
        }
    }
}
