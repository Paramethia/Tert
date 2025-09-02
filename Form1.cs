using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Tert
{
    public partial class Tert : Form
    {
        public Tert()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Show textbox
            textBox1.Visible = true;
            label3.Visible = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Hide  first textbox
            textBox1.Visible = false;
            label3.Visible = false;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            bool reverse = radioButton1.Checked;
            bool binary = radioButton2.Checked;
            bool decode = radioButton3.Checked;

            if (text != "" && reverse == true )
            {
                uint wordsQuantity = 0U;
                // Binary reversal
                if (Regex.IsMatch(text, "^[01 ]+$"))
                {
                    string[] binaryWords = text.Split(' ');
                    if (textBox1.Text.ToLower() == "full")
                    {
                        string fullReversedB = "";
                        foreach (string word in binaryWords)
                        {
                            wordsQuantity++;
                            List<string> bytes = new List<string>();
                            for (int i = 0; i < word.Length; i += 8)
                            {
                                if (i + 8 <= word.Length)
                                {
                                    string chunk = word.Substring(i, 8);
                                    bytes.Add(chunk);
                                }
                            }
                            fullReversedB += string.Join("", bytes.AsEnumerable().Reverse()) + " ";
                        }
                        fullReversedB = fullReversedB.Remove(fullReversedB.Length - 1, 1);
                        int utfBytes = fullReversedB.Replace(" ", "").Length / 8;
                        MessageBox.Show("Click ok or close this to copy to clipboard \n\n" + fullReversedB + $"\n Words reversed: {wordsQuantity}" + $"\nBytes present {utfBytes}", "---Full Binary Reversal---");
                        Clipboard.SetText(fullReversedB);
                    }
                    else if (textBox1.Text.ToLower() == "each" || textBox1.Text == "")
                    {

                        Array.Reverse(binaryWords);
                        wordsQuantity = (uint)binaryWords.Length;
                        string eachReversedB = string.Join(" ", binaryWords);
                        int utfBytes = eachReversedB.Replace(" ", "").Length / 8;

                        MessageBox.Show("Click ok or close this to copy to clipboard \n\n" + eachReversedB + $"\nWords present: {wordsQuantity}" + $"\nBytes present {utfBytes}", "---Each word Binary Reversal---");
                        Clipboard.SetText(eachReversedB);
                    }
                } else
                {
                    // Normal text reversal
                    if (textBox1.Text.ToLower() == "full")
                    {
                        string totalReversal = new string(text.Reverse().ToArray());
                        MessageBox.Show("Click ok or close this to copy to clipboard \n\n" + totalReversal, "---Each word Text Reverse---");
                        Clipboard.SetText(totalReversal);
                    }
                    else if (textBox1.Text.ToLower() == "each" || textBox1.Text == "")
                    {
                        string[] words = text.Split(' ');
                        for (int i = 0; i < words.Length; i++)
                        {
                            wordsQuantity++;
                            char[] charArray = words[i].ToCharArray();
                            Array.Reverse(charArray);
                            words[i] = new string(charArray);
                        }
                        string wordReversal = string.Join(" ", words);
                        MessageBox.Show("Click ok or close this to copy to clipboard \n\n" + wordReversal + $"\nWords reversed: {wordsQuantity}", "---Full Text Reversal---");
                        Clipboard.SetText(wordReversal);
                    }
                }
            }
            else if (text != "" && binary == true)
            {
                if (Regex.IsMatch(text, "^[01 ]+$"))
                {
                    MessageBox.Show("That's already binary 🤦‍", "Error");
                    return;
                }
                // Convert the text to binary
                string binaryText = "";
                uint wordsQuantity = 0U;
                string[] words = text.Split(' ');
                foreach (string word in words)
                {
                    wordsQuantity++;

                    // Get the UTF-8 bytes for this word
                    byte[] utf8Bytes = Encoding.UTF8.GetBytes(word);

                    // Convert each byte to binary (8 bits)
                    string binoredWord = string.Join("", utf8Bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

                    binaryText += binoredWord + " ";
                }
                int utfBytesCount = text.Replace(" ", "").Length;
                binaryText = binaryText.Remove(binaryText.Length - 1, 1);
                MessageBox.Show("Click ok or close this to copy to clipboard \n\n" + binaryText + $"\nWords present: {wordsQuantity}" + $"\nBytes present {utfBytesCount}", "---Text to Binary---");
                Clipboard.SetText(binaryText);
            }
            else if (text != "" && decode == true)
            {
                if (!Regex.IsMatch(text, "^[01 ]+$"))
                {
                    MessageBox.Show("Your text does not seem to be binary", "Error");
                    return;
                }
                uint wordsQuantity = 0U;
                try
                {
                    string[] binaryWords = text.Split(' ');
                    string decodedText = "";

                    foreach (string word in binaryWords)
                    {
                        List<byte> bytes = new List<byte>();
                        wordsQuantity++;

                        for (int i = 0; i < word.Length; i += 8)
                        {
                            if (i + 8 <= word.Length)
                            {
                                string chunk = word.Substring(i, 8);
                                byte value = Convert.ToByte(chunk, 2);
                                bytes.Add(value);
                            }
                        }

                        decodedText += Encoding.UTF8.GetString(bytes.ToArray()) + " ";
                    }

                    MessageBox.Show("Click ok or close this to copy to clipboard \n\n" + decodedText + $"\nWords Present: {wordsQuantity}", "---Binary to Text---");
                    Clipboard.SetText(decodedText);
                }
                catch
                {
                    MessageBox.Show("Invalid binary input. Please ensure the input is a valid UTF-8 binary string.", "Error");
                }
            } else
            {
                MessageBox.Show("Nothing was converted because the checkbox is not checked or the input is empty.", "Error");
            }
            
        }
    }
}
