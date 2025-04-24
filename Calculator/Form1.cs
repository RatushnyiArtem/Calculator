using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using buttonsForCalc;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private Button ButtonMenu;
        private Label TypeLabel;

        private void InitializeCustomUI()
        {
            this.Text = "Calculator";
            this.Size = new Size(350, 650);
            this.BackColor = Color.FromArgb(32, 32, 32);
            this.StartPosition = FormStartPosition.CenterScreen;

            PanelTitle = new Panel { Size = new Size(350, 50), Dock = DockStyle.Top, BackColor = Color.FromArgb(32, 32, 32) };
            ButtonExit = new Button { Image = Image.FromFile("../../Images/close.png"), Size = new Size(50, 50), Dock = DockStyle.Right, BackColor = Color.FromArgb(32, 32, 32), FlatStyle = FlatStyle.Flat, Tag = "ButtonExit" };
            ButtonExit.FlatAppearance.BorderSize = 0;
            ButtonExit.Click += ButtonExit_Click;
            ButtonFullScreen = new Button { Image = Image.FromFile("../../Images/square.png"), Size = new Size(50, 50), Dock = DockStyle.Right, BackColor = Color.FromArgb(32, 32, 32), FlatStyle = FlatStyle.Flat };
            ButtonFullScreen.FlatAppearance.BorderSize = 0;
            ButtonFullScreen.Click += ButtonFullScreen_Click;
            ButtonHide = new Button { Image = Image.FromFile("../../Images/minus-sign.png"), Size = new Size(50, 50), Dock = DockStyle.Right, BackColor = Color.FromArgb(32, 32, 32), FlatStyle = FlatStyle.Flat };
            ButtonHide.FlatAppearance.BorderSize = 0;
            ButtonHide.Click += ButtonHide_Click;
            PanelTitle.Controls.AddRange(new Control[] { ButtonHide, ButtonFullScreen, ButtonExit });

            PanelHistory = new Panel { Size = new Size(350, 0), Dock = DockStyle.Bottom, BackColor = Color.FromArgb(32, 32, 32), AutoScroll = true };
            RtBoxDisplayHistory = new RichTextBox { Dock = DockStyle.Fill, BackColor = Color.FromArgb(32, 32, 32), ForeColor = Color.White, ReadOnly = true, BorderStyle = BorderStyle.None, Tag = "RtBoxDisplayHistory" };
            ButtonClearHistory = new Button { Size = new Size(350,60),Image = Image.FromFile("../../Images/bin.png"), Dock = DockStyle.Bottom,FlatStyle = FlatStyle.Flat };
            ButtonClearHistory.FlatAppearance.BorderSize = 0;
            ButtonClearHistory.Click += ButtonClearHistory_Click;
            PanelHistory.Controls.AddRange(new Control[] { RtBoxDisplayHistory, ButtonClearHistory });

            panel1 = new Panel { Size = new Size(350, 61), Dock = DockStyle.Top, BackColor = Color.FromArgb(32, 32, 32) };
            ButtonMenu = new Button { Image = Image.FromFile("../../Images/menu.png"), Size = new Size(50, 50), Dock = DockStyle.Left, BackColor = Color.FromArgb(32, 32, 32), FlatStyle = FlatStyle.Flat };
            ButtonMenu.FlatAppearance.BorderSize = 0;
            Panel panelMode = new Panel
            {
                Size = new Size(220, this.ClientSize.Height),
                BackColor = Color.FromArgb(50, 50, 50),
                Location = new Point(0, 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                Visible = false
            };
            Label lblTitle = new Label
            {
                Text = "Calculator",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            Button btnStandard = new Button
            {
                Text = "    Standard",
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Height = 40,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            btnStandard.FlatAppearance.BorderSize = 0;
            btnStandard.MouseEnter += (s, e) => btnStandard.BackColor = Color.FromArgb(30, 30, 30);
            btnStandard.MouseLeave += (s, e) => btnStandard.BackColor = Color.Transparent;
            btnStandard.Click += (s, e) => {
                TypeLabel.Text = "Standard";
                panelMode.Visible = false;
            };

            Button btnScientific = new Button
            {
                Text = "    Scientific",
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Height = 40,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            btnScientific.FlatAppearance.BorderSize = 0;
            btnScientific.MouseEnter += (s, e) => btnScientific.BackColor = Color.FromArgb(30, 30, 30);
            btnScientific.MouseLeave += (s, e) => btnScientific.BackColor = Color.Transparent;
            btnScientific.Click += (s, e) => {
                TypeLabel.Text = "Scientific";
                panelMode.Visible = false;
            };

            panelMode.Controls.Add(btnScientific);
            panelMode.Controls.Add(btnStandard);
            panelMode.Controls.Add(lblTitle);
            this.Controls.Add(panelMode);
            ButtonMenu.BringToFront();
            ButtonMenu.Click += (s, e) => panelMode.Visible = !panelMode.Visible;
            TypeLabel = new Label { Font = new Font("Gadugi", 15, FontStyle.Bold), ForeColor = Color.White, Dock = DockStyle.Left, BackColor = Color.FromArgb(32, 32, 32), Text = "Standard",TextAlign = ContentAlignment.MiddleCenter };
            ButtonHistory = new Button { Image = Image.FromFile("../../Images/history.png"), Size = new Size(50, 50), Dock = DockStyle.Right, BackColor = Color.FromArgb(32, 32, 32), FlatStyle = FlatStyle.Flat, Tag = "ButtonHistory" };
            ButtonHistory.FlatAppearance.BorderSize = 0;
            ButtonHistory.Click += ButtonHistory_Click;
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                BackColor = Color.FromArgb(32, 32, 32),
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));  // ButtonMenu
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // TypeLabel
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            layout.Controls.Add(ButtonMenu, 0, 0);
            layout.Controls.Add(TypeLabel, 1, 0);
            layout.Controls.Add(ButtonHistory, 2, 0);
            panel1.Controls.Add(layout);
            TextDisplay2 = new TextBox { Dock = DockStyle.Top, BackColor = Color.FromArgb(32, 32, 32), ForeColor = Color.Silver, ReadOnly = true, BorderStyle = BorderStyle.None, Font = new Font("Gadugi", 14), TextAlign = HorizontalAlignment.Right };
            TextDisplay1 = new TextBox { Dock = DockStyle.Top, BackColor = Color.FromArgb(32, 32, 32), ForeColor = Color.White, ReadOnly = true, BorderStyle = BorderStyle.None, Font = new Font("Gadugi", 30, FontStyle.Bold), TextAlign = HorizontalAlignment.Right,Text = "0" };
            var memoryPanel = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 6,
                Dock = DockStyle.Top,
                Height = 50,
                AutoSize = false,
                BackColor = Color.Transparent
            };

            memoryPanel.ColumnStyles.Clear();
            for (int i = 0; i < 6; i++)
                memoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));

            string[] memoryButtons = { "MC", "MR", "M+", "M-", "MS", "M~" };
            foreach (var text in memoryButtons)
            {
                var btn = new Button
                {
                    Text = text,
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(32, 32, 32),
                    ForeColor = Color.White,
                    Margin = new Padding(2)
                };
                btn.FlatAppearance.BorderSize = 0;
                memoryPanel.Controls.Add(btn);
            }
            var buttonPanel = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 4,
                Dock = DockStyle.Fill,
                AutoSize = false,
                BackColor = Color.Transparent
            };

            buttonPanel.RowStyles.Clear();
            buttonPanel.ColumnStyles.Clear();
            for (int i = 0; i < 6; i++)
                buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));
            for (int j = 0; j < 4; j++)
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 4));

            string[,] buttonLabels = new string[6, 4]
            {
    { "%", "CE", "C", "⌫" },
    { "¹∕ₓ", "x²", "√x", "÷" },
    { "7", "8", "9", "×" },
    { "4", "5", "6", "−" },
    { "1", "2", "3", "+" },
    { "±", "0", ".", "=" }
            };

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var btn = new ButtonForCalc
                    {
                        Text = buttonLabels[row, col],
                        Dock = DockStyle.Fill,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(60, 60, 60),
                        ForeColor = Color.White,
                        Font = new Font("Gadugi", 14, FontStyle.Regular),
                        Margin = new Padding(2)
                    };
                    btn.Click += GenericCalcButton_Click;
                    btn.FlatAppearance.BorderSize = 0;
                    buttonPanel.Controls.Add(btn, col, row);
                }
            }
            for (int i = 0; i < 24; i++) {
                if (i < 8) buttonPanel.Controls[i].BackColor = Color.FromArgb(50, 50, 50);
                else if ((i + 1) % 4 == 0) buttonPanel.Controls[i].BackColor = Color.FromArgb(50, 50, 50);
            }
            for(int i = 7; i < 24; i++)
            {
                if ((i+1) % 4 != 0) buttonPanel.Controls[i].Font = new Font("Gadugi", 15, FontStyle.Bold);

            }
            buttonPanel.Controls[23].BackColor = Color.Blue;
            

            this.Controls.AddRange(new Control[] { PanelHistory, buttonPanel,memoryPanel, TextDisplay1, TextDisplay2, panel1, PanelTitle });

            Panel scientificContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

           
            Panel dropdownTrigonometry = new Panel
            {
                Size = new Size(320, 90),
                BackColor = Color.FromArgb(40, 40, 40),
                Visible = false
            };
            dropdownTrigonometry.AutoScroll = true; 
            dropdownTrigonometry.BorderStyle = BorderStyle.FixedSingle; 

            string[] trigFuncs = { "sin", "cos", "tan", "cot", "sec", "csc", "hyp" };
            int x = 10, y = 10;
            foreach (string func in trigFuncs)
            {
                var btn = new ButtonForCalc
                {
                    Text = func,
                    Size = new Size(90, 35),
                    Location = new Point(x, y),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(60, 60, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += GenericCalcButton_Click;
                dropdownTrigonometry.Controls.Add(btn);
                x += 95; if (x > 270) { x = 10; y += 40; }
            }
            this.Controls.Add(dropdownTrigonometry);

            
            Panel dropdownFunction = new Panel
            {
                Size = new Size(320, 90),
                BackColor = Color.FromArgb(40, 40, 40),
                Visible = false
            };

            string[] funcFuncs = { "|x|", "⌊x⌋", "⌈x⌉", "rand", "→dms", "→deg" };
            x = 10; y = 10;
            foreach (string func in funcFuncs)
            {
                var btn = new ButtonForCalc
                {
                    Text = func,
                    Size = new Size(90, 35),
                    Location = new Point(x, y),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(60, 60, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += GenericCalcButton_Click;
                dropdownFunction.Controls.Add(btn);
                x += 95; if (x > 270) { x = 10; y += 40; }
            }
            this.Controls.Add(dropdownFunction);

           
            FlowLayoutPanel dropdownHeader = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 45,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 5, 0, 5),
                BackColor = Color.Transparent
            };

            Button btnTrigonometry = new Button
            {
                Text = "Trigonometry ▼",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 30, 30),
                Margin = new Padding(2)
            };
            btnTrigonometry.FlatAppearance.BorderSize = 0;

            Button btnFunction = new Button
            {
                Text = "Function ▼",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 30, 30),
                Margin = new Padding(2)
            };
            btnFunction.FlatAppearance.BorderSize = 0;

            btnTrigonometry.Click += (s, e) =>
            {
                var locationOnScreen = btnTrigonometry.PointToScreen(Point.Empty);
                var locationOnForm = this.PointToClient(locationOnScreen);

                dropdownTrigonometry.Location = new Point(locationOnForm.X, locationOnForm.Y + btnTrigonometry.Height);

                dropdownTrigonometry.Visible = !dropdownTrigonometry.Visible;
                dropdownTrigonometry.BringToFront();

                dropdownFunction.Visible = false;
            };

            btnFunction.Click += (s, e) =>
            {
                var locationOnScreen = btnFunction.PointToScreen(Point.Empty);
                var locationOnForm = this.PointToClient(locationOnScreen);
                dropdownFunction.Location = new Point(locationOnForm.X - 100, locationOnForm.Y + btnFunction.Height);
                dropdownFunction.Visible = !dropdownFunction.Visible;
                dropdownFunction.BringToFront();
                dropdownTrigonometry.Visible = false;
            };

            dropdownHeader.Controls.Add(btnTrigonometry);
            dropdownHeader.Controls.Add(btnFunction);

            // --- Scientific Grid Panel ---
            TableLayoutPanel sciPanel = new TableLayoutPanel
            {
                RowCount = 7,
                ColumnCount = 5,
                AutoSize = true,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Margin = new Padding(10)
            };
            sciPanel.RowStyles.Clear();
            sciPanel.ColumnStyles.Clear();
            for (int i = 0; i < 7; i++) sciPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            for (int j = 0; j < 5; j++) sciPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));

            string[,] sciLabels = new string[7, 5]
            {
    { "2ⁿᵈ", "π", "e", "C", "⌫" },
    { "x²", "¹∕ₓ", "|x|", "exp", "mod" },
    { "√x", "(", ")", "n!", "÷" },
    { "xʸ", "7", "8", "9", "×" },
    { "10ˣ", "4", "5", "6", "−" },
    { "log", "1", "2", "3", "+" },
    { "ln", "0", ".", "±", "=" }
            };
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var btn = new ButtonForCalc
                    {
                        Text = sciLabels[row, col],
                        Dock = DockStyle.Fill,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(45, 45, 45),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 10),
                        Margin = new Padding(2)
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += GenericCalcButton_Click;
                    sciPanel.Controls.Add(btn, col, row);
                }
            }

            scientificContainer.Controls.Add(sciPanel);
            scientificContainer.Controls.Add(dropdownHeader);

            btnScientific.Click += (s, e) => {
                this.Controls.Remove(buttonPanel);
                this.Controls.Add(scientificContainer);
                scientificContainer.BringToFront();
            };


            btnStandard.Click += (s, e) => {
                this.Controls.Remove(scientificContainer);
                this.Controls.Add(buttonPanel);
                buttonPanel.BringToFront();
            };


        }
        public Form1()
        {
            InitializeComponent();
            InitializeCustomUI();
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

            ButtonForCalc button = (ButtonForCalc)sender;
            operation = button.Text;
            if (double.TryParse(TextDisplay1.Text, out double parsedValue))
            {
                result = parsedValue;
                fstNum = TextDisplay1.Text;
            }
            enterValue = true;
            TextDisplay2.Text = fstNum = $"{result} {operation}";
            TextDisplay1.Text = string.Empty;
            
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            sndNum = TextDisplay1.Text;
            TextDisplay2.Text = $"{TextDisplay2.Text} {TextDisplay1.Text}=";
            if (TextDisplay1.Text != string.Empty)
            {
                if (TextDisplay1.Text == "0") TextDisplay2.Text = string.Empty;
                if (RtBoxDisplayHistory.Text.Contains("There is no history yet"))
                {
                    RtBoxDisplayHistory.Clear();
                }

                switch (operation)
                {
                    case "+":
                        TextDisplay1.Text =(result+double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "-":
                        TextDisplay1.Text = (result - double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "×":
                        TextDisplay1.Text = (result * double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "÷":
                        TextDisplay1.Text = (result / double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} = {TextDisplay1.Text}\n");
                        break;
                    case "xʸ":
                        double exponent = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                        TextDisplay1.Text = Math.Pow(result, exponent).ToString();
                        RtBoxDisplayHistory.AppendText($"{result} ^ {exponent} = {TextDisplay1.Text}\n");
                        break;
                    case "mod":
                        double second = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                        TextDisplay1.Text = (result % second).ToString();
                        RtBoxDisplayHistory.AppendText($"{result} mod {second} = {TextDisplay1.Text}\n");
                        break;
                    default: TextDisplay2.Text = $"{TextDisplay1.Text} = ";
                        break;
                }


                result = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                operation = string.Empty;
            }
               
        }
        private void ButtonHistory_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            string value = btn.Tag.ToString();
            if (value == "ButtonHistory")
            {
                if (PanelHistory.Height == 0)
                {
                    PanelHistory.Height = 408;
                    PanelHistory.BringToFront();
                }
                else
                {
                    PanelHistory.Height = 0;
                }


            }
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
            if (RtBoxDisplayHistory.Text.Contains("There is no history yet"))
            {
                RtBoxDisplayHistory.Clear();
            }
            switch (operation) {
                case "√x":
                    TextDisplay2.Text = $"√({TextDisplay1.Text})";
                    TextDisplay1.Text = Convert.ToString(Math.Sqrt(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)));
                    break;
                case "x²":
                    TextDisplay2.Text = $"({TextDisplay1.Text})²";
                    TextDisplay1.Text = Convert.ToString(Convert.ToDouble(TextDisplay1.Text)*(Convert.ToDouble(TextDisplay1.Text)));
                    break;
                case "¹∕ₓ":
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
                case "π":
                    TextDisplay1.Text = Math.PI.ToString("G17");
                    break;
                case "e":
                    TextDisplay1.Text = Math.E.ToString("G17");
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
            if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;
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
        private void GenericCalcButton_Click(object sender, EventArgs e)
        {
            var btn = sender as ButtonForCalc;
            string value = btn.Text;
            

            if ("0123456789.".Contains(value))
            {
                BtnNum_Click(sender, e);
            }
            else if ("+-×÷".Contains(value))
            {
                BtnMathOperation_Click(sender, e);
            }
            else if (value == "=")
            {
                BtnEqual_Click(sender, e); 
            }
            else if (value == "C")
            {
                BtnC_Click(sender, e);
            }
            else if (value == "CE")
            {
                BtnCE_Click(sender, e); 
            }
            else if (value == "⌫")
            {
                BtnBackSpace_Click(sender, e); 
            }
            else if (value == "%" || value == "±" || value == "x²" || value == "¹/x" ||  value == "√x" || value == "²√x" || value == "π" || value == "e")
            {
                ButtonOperations_Click(sender, e);
            }
            else if (value == "xʸ")
            {
                result = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                operation = "xʸ";
                TextDisplay2.Text = $"{TextDisplay1.Text} ^"; 
                enterValue = true; 
            }
            else if (value == "10ˣ")
            {
                TextDisplay2.Text = "10^" + TextDisplay1.Text;
                TextDisplay1.Text = Math.Pow(10, double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "n!")
            {
                double number = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                double factorial = 1;
                for (int i = 1; i <= number; i++) factorial *= i;
                TextDisplay2.Text = $"{number}!";
                TextDisplay1.Text = factorial.ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "log")
            {
                TextDisplay2.Text = $"log({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Log10(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "ln")
            {
                TextDisplay2.Text = $"ln({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Log(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "|x|")
            {
                TextDisplay2.Text = $"|{TextDisplay1.Text}|";
                TextDisplay1.Text = Math.Abs(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "(" || value == ")")
            {
                TextDisplay1.Text += value;
            }
            else if (value == "mod")
            {
                result = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);       
                operation = "mod";
                TextDisplay2.Text = $"{TextDisplay1.Text} mod";     
                enterValue = true;
            }
            else if (value == "sin")
            {
                TextDisplay2.Text = $"sin({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Sin(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "cos")
            {
                TextDisplay2.Text = $"cos({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Cos(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "tan")
            {
                TextDisplay2.Text = $"tan({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Tan(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "cot")
            {
                TextDisplay2.Text = $"cot({TextDisplay1.Text})";
                TextDisplay1.Text = (1.0 / Math.Tan(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture))).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "sec")
            {
                TextDisplay2.Text = $"sec({TextDisplay1.Text})";
                TextDisplay1.Text = (1.0 / Math.Cos(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture))).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "csc")
            {
                TextDisplay2.Text = $"csc({TextDisplay1.Text})";
                TextDisplay1.Text = (1.0 / Math.Sin(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture))).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "hyp")
            {
                TextDisplay2.Text = $"√({TextDisplay1.Text}² + {TextDisplay1.Text}²)";
                double x = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                TextDisplay1.Text = Math.Sqrt(x * x + x * x).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }

            else if (value == "⌊x⌋")
            {
                TextDisplay2.Text = $"floor({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Floor(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "⌈x⌉")
            {
                TextDisplay2.Text = $"ceil({TextDisplay1.Text})";
                TextDisplay1.Text = Math.Ceiling(double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture)).ToString();
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "rand")
            {
                Random rnd = new Random();
                TextDisplay1.Text = rnd.NextDouble().ToString("F5");
                TextDisplay2.Text = "rand()";
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "→dms")
            {
                double degrees = double.Parse(TextDisplay1.Text, CultureInfo.InvariantCulture);
                int d = (int)degrees;
                double mFull = (degrees - d) * 60;
                int m = (int)mFull;
                double s = (mFull - m) * 60;
                TextDisplay2.Text = $"{degrees}° = {d}° {m}' {s:0.##}";
                TextDisplay1.Text = $"{d}.{m:D2}{(int)s:D2}";
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
            else if (value == "→deg")
            {
                string[] parts = TextDisplay1.Text.Split('.');
                int d = int.Parse(parts[0]);
                int m = int.Parse(parts[1].Substring(0, 2));
                int s = int.Parse(parts[1].Substring(2));
                double degrees = d + m / 60.0 + s / 3600.0;
                TextDisplay1.Text = degrees.ToString("F5");
                TextDisplay2.Text = $"deg({d}°{m}'{s})";
                RtBoxDisplayHistory.AppendText($"{TextDisplay2.Text} = {TextDisplay1.Text} \n");
            }
        }
    }
}
