using Coding.Challange.IsankaThalagala.Utility;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Coding.Challange.IsankaThalagala
{
    public partial class MobileEmulator : Form
    {

        // To track the number of same key press
        private StringBuilder currentInput = new StringBuilder();
        private string lastKeyPressed = "";
        private DateTime lastKeyPressedTime = DateTime.Now;
        public MobileEmulator()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            PictureBox clickedButton = (PictureBox)sender;
            var buttonName = clickedButton.Name.Split('_')[1];
            switch (buttonName)
            {
                case "Ok":
                    txtOutput.Text = TextDecorder.OldPhonePad(txtInput.Text);
                    break;
                case "Center":
                    txtOutput.Text = TextDecorder.OldPhonePad(txtInput.Text);
                    break;
                case "Cancel":
                    currentInput.Clear();
                    lastKeyPressed = "";
                    txtInput.Text = string.Empty;
                    txtOutput.Text = string.Empty;
                    break;
                case "Star":
                    break;
                case "Hash":
                    txtOutput.Text = TextDecorder.OldPhonePad(txtInput.Text);
                    break;
                default:
                    if (Regex.IsMatch(buttonName, @"^\d+$")) //  string contains only digits.                  
                        ProcessKeyPress(buttonName);
                    else
                        Console.WriteLine("Unknown key event.");
                    break;
            }
        }

        private void ProcessKeyPress(string key)
        {
            var delay = DateTime.Now - lastKeyPressedTime;
            if (lastKeyPressed == key && delay.TotalMilliseconds <= 1000)
            {
                // User have pressed same key.
                currentInput.Append(key);
            }
            else
            {
                currentInput.Append("   ");
                currentInput.Append(key);
            }

            lastKeyPressed = key;
            txtInput.Text = currentInput.ToString();
            lastKeyPressedTime = DateTime.Now;

            // Auto decode when user idle for more than 1 second
            if (lastKeyPressed == key && delay.TotalMilliseconds > 1000)
                txtOutput.Text = TextDecorder.OldPhonePad(currentInput.ToString());
        }

        public void bpBtn_Ok_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
