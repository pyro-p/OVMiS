namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int inNS = int.Parse(comboBoxInNS.Text);
                string input1 = textBoxInput1.Text;
                string input2 = textBoxInput2.Text;
                string operation = comboBoxOperation.SelectedItem.ToString();


                if (inNS < 2 || inNS > 16)
                {
                    MessageBox.Show("Неверно указана система счисления");
                    return;
                }

                if (!CheckValidNum(input1, inNS) || !CheckValidNum(input2, inNS))
                {
                    MessageBox.Show("Некорректное число");
                    return;
                }

                int decimalInput1 = ConvertToDecimal(input1, inNS);
                int decimalInput2 = ConvertToDecimal(input2, inNS);

                int bitLength = GetRequiredBitLength(Math.Max(Math.Abs(decimalInput1), Math.Abs(decimalInput2)), inNS); // Определяем необходимую разрядность

                string binary1 = ConvertFromDecimal(decimalInput1, 2).PadLeft(bitLength, (decimalInput1 < 0) ? '1' : '0');  // Дополняем до нужной разрядности
                string binary2 = ConvertFromDecimal(decimalInput2, 2).PadLeft(bitLength, (decimalInput2 < 0) ? '1' : '0');

                string resultBinary = "";

                switch (operation)
                {
                    case "+":
                        resultBinary = AddBinary(binary1, binary2);
                        break;
                    case "-":
                        resultBinary = SubtractBinary(binary1, binary2);
                        break;
                    case "*":
                        resultBinary = MultiplyBinary(binary1, binary2);
                        break;
                    case "/":
                        if (decimalInput2 == 0)
                        {
                            MessageBox.Show("Деление на ноль!");
                            return;
                        }
                        resultBinary = DivideBinary(binary1, binary2);
                        break;
                    default:
                        MessageBox.Show("Неверная операция");
                        return;
                }


                int resultDecimal = ConvertToDecimal(resultBinary, 2);
                string resultInNS = ConvertFromDecimal(resultDecimal, inNS);

                textBoxOutput.Text = resultInNS;
                textBoxOutputDC.Text = FormatBinary(ToDirectCode(resultDecimal));
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный ввод");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Переполнение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string AddBinary(string bin1, string bin2)
        {
            int maxLength = Math.Max(bin1.Length, bin2.Length);
            bin1 = bin1.PadLeft(maxLength, '0');
            bin2 = bin2.PadLeft(maxLength, '0');

            char carry = '0';
            string result = "";

            for (int i = maxLength - 1; i >= 0; i--)
            {
                int sum = (bin1[i] - '0') + (bin2[i] - '0') + (carry - '0');
                switch (sum)
                {
                    case 0:
                        result = '0' + result;
                        carry = '0';
                        break;
                    case 1:
                        result = '1' + result;
                        carry = '0';
                        break;
                    case 2:
                        result = '0' + result;
                        carry = '1';
                        break;
                    case 3:
                        result = '1' + result;
                        carry = '1';
                        break;
                }
            }

            if (carry == '1')
            {
                result = '1' + result;
            }

            return result;
        }

        private string SubtractBinary(string bin1, string bin2)
        {
            int maxLength = Math.Max(bin1.Length, bin2.Length);
            bin1 = bin1.PadLeft(maxLength, '0');
            bin2 = bin2.PadLeft(maxLength, '0');

            string bin2Complement = TwosComplement(bin2);

            string result = AddBinary(bin1, bin2Complement);

            if (result.Length > maxLength)
            {
                result = result.Substring(1);
            }

            return result;
        }

        private string TwosComplement(string binary)
        {
            string inverted = "";
            foreach (char bit in binary)
            {
                inverted += (bit == '0') ? '1' : '0';
            }

            return AddBinary(inverted, "1");
        }

        private string MultiplyBinary(string bin1, string bin2)
        {
            bool negativeResult = (bin1[0] == '1') ^ (bin2[0] == '1');

            if (bin1.StartsWith("1")) bin1 = TwosComplement(bin1);
            if (bin2.StartsWith("1")) bin2 = TwosComplement(bin2);

            bin1 = bin1.TrimStart('0');
            bin2 = bin2.TrimStart('0');


            if (string.IsNullOrEmpty(bin1) || string.IsNullOrEmpty(bin2))
            {
                return "0";
            }

            string result = "0";

            for (int i = bin2.Length - 1; i >= 0; i--)
            {
                if (bin2[i] == '1')
                {
                    string partialProduct = bin1;

                    for (int j = 0; j < bin2.Length - 1 - i; j++)
                    {
                        partialProduct += '0';
                    }

                    result = AddBinary(result, partialProduct);

                }
            }

            if (negativeResult && result != "0") result = TwosComplement(result);
            return result;
        }

        private string DivideBinary(string bin1, string bin2)
        {
            if (string.IsNullOrEmpty(bin2.TrimStart('0')))
            {
                throw new DivideByZeroException();
            }


            bool negativeResult = (bin1[0] == '1') ^ (bin2[0] == '1');

            if (bin1.StartsWith("1")) bin1 = TwosComplement(bin1);

            if (bin2.StartsWith("1")) bin2 = TwosComplement(bin2);



            bin1 = bin1.TrimStart('0');
            bin2 = bin2.TrimStart('0');



            if (string.IsNullOrEmpty(bin1)) return "0";

            string result = "";
            string currentDividend = "";


            for (int i = 0; i < bin1.Length; i++)
            {

                currentDividend += bin1[i];
                if (IsGreaterOrEqual(currentDividend, bin2))
                {


                    currentDividend = SubtractBinary(currentDividend, bin2);

                    result += "1";
                }
                else
                {
                    result += "0";
                }
                currentDividend = currentDividend.TrimStart('0');
            }

            if (negativeResult && !string.IsNullOrEmpty(result.TrimStart('0'))) result = TwosComplement(result);

            return result;
        }

        private int GetRequiredBitLength(int decimalValue, int baseNS)
        {
            int bitLength = 0;
            int maxValue = 1;
            while (maxValue < decimalValue)
            {
                maxValue *= 2;
                bitLength++;
            }

            if (baseNS > 2 && bitLength > 0)
            {
                bitLength += 1;
            }
            else if (baseNS == 2 && decimalValue < 0)
            {
                bitLength += 1;
            }

            return Math.Max(bitLength, 4);
        }

        private bool IsGreaterOrEqual(string bin1, string bin2)
        {
            int n1 = bin1.Length;
            int n2 = bin2.Length;

            if (n1 > n2) return true;
            if (n1 < n2) return false;

            for (int i = 0; i < n1; i++)
            {
                if (bin1[i] > bin2[i]) return true;
                if (bin1[i] < bin2[i]) return false;
            }

            return true;
        }

        private static bool CheckValidNum(string num, int inNS)
        {
            foreach (char c in num)
            {
                if (c == '-') continue;
                int digit;

                if (char.IsDigit(c))
                {
                    digit = c - '0';
                }
                else
                {
                    digit = char.ToUpper(c) - 'A' + 10;
                }

                if (digit < 0 || digit >= inNS)
                {
                    return false;
                }
            }
            return true;
        }



        private static int ConvertToDecimal(string num, int inNS)
        {
            int decimalValue = 0;
            int sign = 1;

            if (num.StartsWith("-"))
            {
                sign = -1;
                num = num[1..];
            }


            for (int i = 0; i < num.Length; i++)
            {
                int digit;
                char c = num[num.Length - 1 - i];

                if (char.IsDigit(c))
                {
                    digit = c - '0';
                }
                else
                {
                    digit = char.ToUpper(c) - 'A' + 10;
                }

                decimalValue += digit * (int)Math.Pow(inNS, i);
            }

            return decimalValue * sign;
        }

        private static string ConvertFromDecimal(int num, int outNS)
        {
            string convertedNum = "";
            int abs = Math.Abs(num);

            while (abs > 0)
            {
                int remainder = abs % outNS;
                char digit = (remainder < 10) ? (char)(remainder + '0') : (char)(remainder - 10 + 'A');
                convertedNum = digit + convertedNum;
                abs /= outNS;
            }

            if (num < 0)
            {
                convertedNum = "-" + convertedNum;
            }
            else if (string.IsNullOrEmpty(convertedNum))
            {
                return "0";
            }

            return convertedNum;
        }


        private static string ToDirectCode(int decimalValue)
        {
            int bitSize = 4;
            if (Math.Abs(decimalValue) > 15) bitSize = 8;
            if (Math.Abs(decimalValue) > 255) bitSize = 12;
            if (Math.Abs(decimalValue) > 4095) bitSize = 16;
            if (Math.Abs(decimalValue) > 65535) bitSize = 32;

            if (decimalValue < 0)
            {
                string binary = Convert.ToString(Math.Abs(decimalValue), 2).PadLeft(bitSize, '0');
                char[] bits = binary.ToCharArray();
                bits[0] = '1';
                return new string(bits);
            }
            else
            {
                string binary = Convert.ToString(decimalValue, 2).PadLeft(bitSize, '0');
                return binary;
            }
        }

        private static string FormatBinary(string num)
        {
            string format = "";

            for (int i = 0; i < num.Length; i++)
            {
                format += num[i];
                if (i != num.Length - 1 && (i + 1) % 4 == 0)
                {
                    format += " ";
                }
            }

            return format;
        }
    }
}