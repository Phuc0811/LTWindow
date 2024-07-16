using System;

class TroChoiDoanSo
{
    static void Main()
    {
        Random random = new Random();
        int soBiMat = random.Next(100, 1000);
        string soBiMatString = soBiMat.ToString();
        int soLanThu = 7;

        Console.WriteLine("Chào mừng đến với trò chơi đoán số!");
        Console.WriteLine("Máy tính đã chọn một số từ 100 đến 999. Bạn có 7 lần đoán để tìm ra số đó.");

        for (int i = 1; i <= soLanThu; i++)
        {
            Console.Write($"Lần đoán thứ {i}: Nhập vào số bạn đoán (3 chữ số): ");
            string doanCuaNguoiChoi = Console.ReadLine();

            if (doanCuaNguoiChoi.Length != 3 || !int.TryParse(doanCuaNguoiChoi, out int soDoan) || soDoan < 100 || soDoan > 999)
            {
                Console.WriteLine("Nhập không hợp lệ. Vui lòng nhập một số có 3 chữ số.");
                i--; 
                continue;
            }

            if (doanCuaNguoiChoi == soBiMatString)
            {
                Console.WriteLine("Chúc mừng! Bạn đã đoán đúng số.");
                return;
            }

            char[] phanHoi = new char[3];
            for (int j = 0; j < 3; j++)
            {
                if (doanCuaNguoiChoi[j] == soBiMatString[j])
                {
                    phanHoi[j] = '+';
                }
                else if (soBiMatString.Contains(doanCuaNguoiChoi[j]))
                {
                    phanHoi[j] = '?';
                }
                else
                {
                    phanHoi[j] = ' ';
                }
            }

            Console.WriteLine($"Phản hồi: {new string(phanHoi)}");
        }

        Console.WriteLine($"Rất tiếc, bạn đã dùng hết các lần đoán. Số đúng là {soBiMat}.");
    }
}
