using System;
using System.Windows.Forms;

namespace StopWatch_Application
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        private int hours = 0, minutes = 0, seconds = 0;
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            UpdateDisplay();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;

            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }

            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            richTextBox1.Text = hours.ToString("00");
            richTextBox2.Text = minutes.ToString("00");
            richTextBox3.Text = seconds.ToString("00");
        }

        // Start
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                timer.Start();
                isRunning = true;
            }
        }

        // Stop
        private void button2_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                timer.Stop();
                isRunning = false;
            }
        }

        // Save
        private void button3_Click(object sender, EventArgs e)
        {
            string time = $"{hours:00}:{minutes:00}:{seconds:00}";
            MessageBox.Show("Saved Time: " + time, "Stopwatch");
        }

        // Reset
        private void button4_Click(object sender, EventArgs e)
        {
            timer.Stop();
            isRunning = false;
            hours = 0;
            minutes = 0;
            seconds = 0;
            UpdateDisplay();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void richTextBox2_TextChanged(object sender, EventArgs e) { }
        private void richTextBox3_TextChanged(object sender, EventArgs e) { }
    }
}