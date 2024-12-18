using lab4;

namespace lab3;

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
            textBoxInput1.Text = textBoxInput1.Text.ToUpper();
            textBoxInput2.Text = textBoxInput2.Text.ToUpper();

            int inNS = int.Parse(comboBoxInNS.Text);
            string input1 = textBoxInput1.Text;
            string input2 = textBoxInput2.Text;
            string? operation = comboBoxOperation.SelectedItem?.ToString();


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

            int decimal1 = Operations.ToDecimal(input1, inNS);
            int decimal2 = Operations.ToDecimal(input2, inNS);
            string binary1 = Operations.ToNumeralSystem(decimal1, 2);
            string binary2 = Operations.ToNumeralSystem(decimal2, 2);

            string result = "";
            int resultDecimal;
            string resultBinary = "";

            switch (operation)
            {
                case "+":
                    resultBinary = Operations.AddBinary(binary1, binary2);
                    resultDecimal = decimal1 + decimal2;
                    break;
                case "-":
                    resultBinary = Operations.SubtractBinary(binary1, binary2);
                    resultDecimal = decimal1 - decimal2;
                    break;
                case "*":
                    resultBinary = Operations.MultiplyBinary(binary1, binary2);
                    resultDecimal = decimal1 * decimal2;
                    break;
                case "/":
                    if (decimal2 == 0)
                    {
                        MessageBox.Show("Деление на ноль!");
                        return;
                    }
                    resultBinary = Operations.DivideBinary(binary1, binary2);
                    resultDecimal = decimal1 / decimal2;
                    break;
                default:
                    MessageBox.Show("Неверная операция");
                    return;
            }

            textBoxOutput.Text = Operations.ToNumeralSystem(resultDecimal, inNS);
            textBoxOutputDC.Text = Operations.ToDirectCode(resultBinary);
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
}