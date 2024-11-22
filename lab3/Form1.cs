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
                int outNS = int.Parse(comboBoxOutNS.Text);
                string input = textBoxInput.Text;

                if (inNS < 2 || inNS > 16 || outNS < 2 || outNS > 16)
                {
                    MessageBox.Show("Неверно указана система счисления");
                    return;
                }

                if (!CheckValidNum(input, inNS))
                {
                    MessageBox.Show("Некорректное число");
                    return;
                }

                int decimalInput = ConvertToDecimal(input, inNS);
                string convertedInput = ConvertFromDecimal(decimalInput, outNS);

                if (outNS == 2)
                {
                    textBoxOutput.Text = convertedInput;
                    textBoxOutputDC.Text = FormatBinary(ToDirectCode(decimalInput));
                }
                else
                {
                    textBoxOutput.Text = convertedInput;
                }


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
