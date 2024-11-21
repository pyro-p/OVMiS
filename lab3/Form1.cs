namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ComboBoxInNS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxOutNS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            int inNs = Convert.ToInt32(comboBoxInNS.SelectedText);
            int outNS = Convert.ToInt32(comboBoxOutNS.SelectedText);

            int input = Convert.ToInt32(textBoxInput.Text);
            ConvertToDecimal(input, inNs);
        }

        private int ConvertToDecimal(int number, int numeralSystem)
        {
            string numString = number.ToString();
            int[] numArray = new int[numString.Length];

            for (int i = 0; i < numString.Length; i++)
            {
                numArray[i] = (int)Char.GetNumericValue(number.ToString()[i]);
            }

            int output = Convert.ToInt32(numArray);
            return output;
        }


    }
}
