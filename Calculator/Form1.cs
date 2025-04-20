using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using buttonsForCalc;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonForCalc1_Click(object sender, EventArgs e)
        {

        }

        private void buttonForCalc3_Click(object sender, EventArgs e)
        {

        }

        private void buttonForCalc9_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Double result = 0;
        string operation = string.Empty;
        string fstNum, sndNum;
        bool enterValue = false;

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            if (result != 0) BtnEquals.PerformClick();
            else result = Double.Parse(TextDisplay1.Text);

            ButtonForCalc button = (ButtonForCalc)sender;
            operation = button.Text;
            enterValue = true;
            if(TextDisplay1.Text != "0") {
                TextDisplay2.Text = fstNum = $"{result} {operation}";
                TextDisplay1.Text = string.Empty;
            }
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            sndNum = TextDisplay1.Text;
            TextDisplay2.Text = $"{TextDisplay2.Text} {TextDisplay1.Text}=";
            if (TextDisplay1.Text != string.Empty)
            {
                if (TextDisplay1.Text == "0") TextDisplay2.Text = string.Empty;
                switch (operation)
                {
                    case "+":
                        TextDisplay1.Text =(result+Double.Parse(TextDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "-":
                        TextDisplay1.Text = (result - Double.Parse(TextDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "×":
                        TextDisplay1.Text = (result * Double.Parse(TextDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "÷":
                        TextDisplay1.Text = (result / Double.Parse(TextDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    default: TextDisplay2.Text = $"{TextDisplay1.Text} = ";
                        break;
                }


                result = Double.Parse(TextDisplay1.Text);
                operation = string.Empty;
            }
               
        }

        private void ButtonHistory_Click(object sender, EventArgs e)
        {
            PanelHistory.Height = (PanelHistory.Height == 5) ? PanelHistory.Height = 398 : 5;
        }

        private void ButtonClearHistory_Click(object sender, EventArgs e)
        {
            RtBoxDisplayHistory.Clear();
            if (RtBoxDisplayHistory.Text == string.Empty)
                RtBoxDisplayHistory.Text = "There is no history yet.";

        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            if (TextDisplay1.Text.Length > 0)
                TextDisplay1.Text = TextDisplay1.Text.Remove(TextDisplay1.Text.Length - 1, 1);
            if (TextDisplay1.Text == string.Empty) TextDisplay1.Text = "0";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            TextDisplay1.Text = "0";
            TextDisplay2.Text = string.Empty;
            result = 0;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            TextDisplay1.Text = "0";
        }

        private void ButtonOperations_Click(object sender, EventArgs e)
        {
            ButtonForCalc button = (ButtonForCalc)sender;
            operation = button.Text;
            switch (operation) {
                case "√x":
                    TextDisplay2.Text = $"√({TextDisplay1.Text})";
                    TextDisplay1.Text = Convert.ToString(Math.Sqrt(Double.Parse(TextDisplay1.Text)));
                    break;
                case "х²":
                    TextDisplay2.Text = $"({TextDisplay1.Text})²";
                    TextDisplay1.Text = Convert.ToString(Convert.ToDouble(TextDisplay1.Text)*(Convert.ToDouble(TextDisplay1.Text)));
                    break;
                case "¹/x":
                    TextDisplay2.Text = $"¹/({TextDisplay1.Text})";
                    TextDisplay1.Text = Convert.ToString(1.0 / Convert.ToDouble(TextDisplay1.Text));
                    break;
                case "%":
                    TextDisplay2.Text = $"({TextDisplay1.Text})%";
                    TextDisplay1.Text = Convert.ToString(Convert.ToDouble(TextDisplay1.Text) / Convert.ToDouble(100));
                    break;
                case "±":
                    TextDisplay1.Text = Convert.ToString(-1 * Convert.ToDouble(TextDisplay1.Text));
                    break;
            }
            RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonHide_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ButtonFullScreen_Click(object sender, EventArgs e)
        {
            //if(WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
            //else WindowState = FormWindowState.Normal;
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            if (TextDisplay1.Text == "0" || enterValue) TextDisplay1.Text = string.Empty;
            enterValue = false;
            ButtonForCalc button = (ButtonForCalc)sender;
            if (button.Text == ".")
            {
                if (!TextDisplay1.Text.Contains("."))
                    TextDisplay1.Text = TextDisplay1.Text + button.Text;
            }
            else TextDisplay1.Text = TextDisplay1.Text + button.Text;
        }
    }
}
