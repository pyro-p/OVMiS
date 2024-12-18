namespace lab4;

class Operations
{
    public static string AddBinary(string input1, string input2)
    {
        string num1;
        string num2;
        bool isNeg1 = false;
        bool isNeg2 = false;
        int negCount = 0;
        int negLen = 0;

        if (input1.StartsWith('-'))
        {
            isNeg1 = true;
            input1 = input1[1..];
            negCount++;
        }

        if (input2.StartsWith('-'))
        {
            isNeg2 = true;
            input2 = input2[1..];
            negCount++;
        }

        int maxLen = Math.Max(input1.Length, input2.Length);

        num1 = input1.PadLeft(maxLen, '0');
        num2 = input2.PadLeft(maxLen, '0');

        // To Two's complement
        if (isNeg1)
        {
            bool check = false;

            if (maxLen < 4 && !check)
            {
                check = true;
                negLen = 4;
                input1 = input1.PadLeft(negLen, '0');
            }

            if (4 <= maxLen && maxLen < 8 && !check)
            {
                check = true;
                negLen = 8;
                input1 = input1.PadLeft(negLen, '0');
            }

            if (8 <= maxLen && maxLen < 16 && !check)
            {
                check = true;
                negLen = 16;
                input1 = input1.PadLeft(negLen, '0');
            }

            if (16 <= maxLen && maxLen < 32 && !check)
            {
                negLen = 32;
                input1 = input1.PadLeft(negLen, '0');
            }

            input1 = ToTwosComplement(input1);
        }

        // To Two's complement
        if (isNeg2)
        {
            bool check = false;

            if (maxLen < 4 && !check)
            {
                check = true;
                negLen = 4;
                input2 = input2.PadLeft(negLen, '0');
            }

            if (4 <= maxLen && maxLen < 8 && !check)
            {
                check = true;
                negLen = 8;
                input2 = input2.PadLeft(negLen, '0');
            }

            if (8 <= maxLen && maxLen < 16 && !check)
            {
                check = true;
                negLen = 16;
                input2 = input2.PadLeft(negLen, '0');
            }

            if (16 <= maxLen && maxLen < 32 && !check)
            {
                negLen = 32;
                input2 = input2.PadLeft(negLen, '0');
            }

            input2 = ToTwosComplement(input2);
        }

        maxLen = Math.Max(input1.Length, input2.Length);
        input1 = input1.PadLeft(maxLen, '0');
        input2 = input2.PadLeft(maxLen, '0');

        string result = "";
        int carry = 0;

        for (int i = maxLen - 1; i >= 0; i--)
        {
            int bitA = input1[i] - '0';
            int bitB = input2[i] - '0';
            int sum = bitA + bitB + carry;

            result = sum % 2 + result;
            carry = sum / 2;
        }

        if (carry > 0) result = carry + result;

        if (negCount != 0)
        {
            if (result.Length > negLen)
            {
                if (negCount == 2)
                {
                    result = ToTwosComplement(result.Substring(1));
                    result = '-' + result;
                }

                if (negCount == 1 && CompareBinary(num1, num2) > 0 && isNeg1)
                {
                    result = ToTwosComplement(result.Substring(1));
                    result = '-' + result;
                }

                if (negCount == 1 && CompareBinary(num1, num2) > 0 && isNeg2)
                {
                    result = result.Substring(1);
                }

                if (negCount == 1 && CompareBinary(num1, num2) < 0 && isNeg1)
                {
                    result = result.Substring(1);
                }

                if (negCount == 1 && CompareBinary(num1, num2) < 0 && isNeg2)
                {
                    result = ToTwosComplement(result.Substring(1));
                    result = '-' + result;
                }

                if (negCount == 1 && CompareBinary(num1, num2) == 0)
                {
                    result = "0";
                    return result;
                }
            }

            if (result.Length == negLen && result[0] == '1')
            {
                result = ToTwosComplement(result);
                result = '-' + result;
            }
        }

        return result.TrimStart('0');
    }

    public static string SubtractBinary(string num1, string num2)
    {
        if (num2 == "0") return num1;

        num2 = ToTwosComplement(num2);

        return AddBinary(num1, num2);
    }

    public static string MultiplyBinary(string input1, string input2)
    {
        if (input1 == "0" || input2 == "0") return "0";

        bool isNeg = input1.StartsWith('-') && !input2.StartsWith('-') || !input1.StartsWith('-') && input2.StartsWith('-');

        if (input1.StartsWith('-')) input1 = input1.Substring(1);
        if (input2.StartsWith('-')) input2 = input2.Substring(1);

        string result = "0";

        for (int i = input2.Length - 1; i >= 0; i--)
        {
            if (input2[i] == '1')
            {
                string carry = input1 + new string('0', input2.Length - 1 - i);
                result = AddBinary(result, carry);
            }
        }

        if (isNeg) result = '-' + result;

        if (result.TrimStart('0') == "") return "0";
        else return result.TrimStart('0');
    }

    public static string DivideBinary(string input1, string input2)
    {
        if (input2 == "0") throw new DivideByZeroException("Деление на ноль!");
        bool isNeg = input1.StartsWith('-') && !input2.StartsWith('-') || !input1.StartsWith('-') && input2.StartsWith('-');

        if (input1.StartsWith('-')) input1 = input1.Substring(1);
        if (input2.StartsWith('-')) input2 = input2.Substring(1);

        string result = "0";
        string div = "0";

        for (int i = 0; i < input1.Length; i++)
        {
            div = AddBinary(div + input1[i].ToString(), "0");

            if (CompareBinary(div, input2) >= 0)
            {
                div = SubtractBinary(div, input2);
                result += "1";
            }
            else
            {
                result += "0";
            }
        }

        result = result.TrimStart('0');

        if (isNeg && result != "") result = '-' + result;

        if (result == "") return "0";
        else return result;
    }

    public static string ToTwosComplement(string binary)
    {
        char[] inverted = new char[binary.Length];

        for (int i = 0; i < binary.Length; i++)
        {
            inverted[i] = binary[i] == '0' ? '1' : '0';
        }

        string invertedBinary = new string(inverted);
        return AddBinary(invertedBinary, "1");
    }

    public static int CompareBinary(string input1, string input2)
    {
        if (input1.Length != input2.Length)
        {
            return input1.Length.CompareTo(input2.Length);
        }

        return input1.CompareTo(input2);
    }

    public static int ToDecimal(string num, int numSys)
    {
        bool isNeg = num.StartsWith('-');
        num = isNeg ? num[1..] : num;
        int result = 0;

        foreach (char c in num)
        {
            int digit = c >= '0' && c <= '9' ? c - '0' : c - 'A' + 10;
            if (digit < 0 || digit >= numSys)
            {
                throw new ArgumentException($"Недопустимое число.");
            }
            result = result * numSys + digit;
        }

        if (isNeg) return -result;
        else return result;
    }

    public static string ToNumeralSystem(int n, int b)
    {
        if (n == 0) return "0";
        if (b == 10) return n.ToString();

        const string digits = "0123456789ABCDEF";
        bool isNeg = n < 0;
        n = Math.Abs(n);

        string result = "";
        while (n > 0)
        {
            result = digits[n % b] + result;
            n /= b;
        }

        if (isNeg) return "-" + result;
        else return result;
    }

    public static string ToDirectCode(string binary)
    {
        binary = binary.TrimStart('0');

        if (string.IsNullOrEmpty(binary)) return "0000";

        bool isNeg = binary.StartsWith('-');

        if (isNeg) binary = binary[1..];

        int binaryLen = binary.Length;
        int bitsCount = binaryLen <= 4 ? 4 : binaryLen <= 8 ? 8 : binaryLen <= 16 ? 16 : binaryLen <= 32 ? 32 : 0;

        if (bitsCount == 0)
        {
            MessageBox.Show("Слишком длинное число");
            return "Слишком длинное число";
        }

        int leadingZerosCount = bitsCount - binaryLen - (isNeg ? 1 : 0);

        if (leadingZerosCount < 0) leadingZerosCount = 0;

        string ToDirectCode = (isNeg ? "1" : "0") + new string('0', leadingZerosCount) + binary;

        if (ToDirectCode.Length > bitsCount)
        {
            ToDirectCode = ToDirectCode.Substring(ToDirectCode.Length - bitsCount); // Обрезаем
        }
        else if (ToDirectCode.Length < bitsCount)
        {
            ToDirectCode = ToDirectCode.PadLeft(bitsCount, '0'); // Заполняем нулями слева
        }

        return SplitString(ToDirectCode, 4); // Разделяем на группы
    }

    private static string SplitString(string str, int chunkSize)
    {
        if (chunkSize <= 0) throw new ArgumentOutOfRangeException(nameof(chunkSize), "Размер чанка меньше нуля.");

        var result = new List<string>();
        for (int i = 0; i < str.Length; i += chunkSize)
        {
            if (i + chunkSize > str.Length)
            {
                result.Add(str.Substring(i));
            }
            else
            {
                result.Add(str.Substring(i, chunkSize));
            }
        }

        return string.Join(' ', result);
    }
}
