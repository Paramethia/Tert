using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        string[,,] binaryLetters = new string[2, 26, 2]
        {
            {
                { "A", "01000001" }, { "B", "01000010" }, { "C", "01000011" }, { "D", "01000100" }, { "E", "01000101" }, { "F", "01000110" },
                { "G", "01000111" }, { "H", "01001000" }, { "I", "01001001" }, { "J", "01001010" }, { "K", "01001011" }, { "L", "01001100" },
                { "M", "01001101" }, { "N", "01001110" }, { "O", "01001111" }, { "P", "01010000" }, { "Q", "01010001" }, { "R", "01010010" },
                { "S", "01010011" }, { "T", "01010100" }, { "U", "01010101" }, { "V", "01010110" }, { "W", "01010111" }, { "X", "01011000" },
                { "Y", "01011001" }, { "Z", "01011010" }
            },
            {
                { "a", "01100001" }, { "b", "01100010" }, { "c", "01100011" }, { "d", "01100100" }, { "e", "01100101" }, { "f", "01100110" },
                { "g", "01100111" }, { "h", "01101000" }, { "i", "01101001" }, { "j", "01101010" }, { "k", "01101011" }, { "l", "01101100" },
                { "m", "01101101" }, { "n", "01101110" }, { "o", "01101111" }, { "p", "01110000" }, { "q", "01110001" }, { "r", "01110010" },
                { "s", "01110011" }, { "t", "01110100" }, { "u", "01110101" }, { "v", "01110110" }, { "w", "01110111" }, { "x", "01111000" },
                { "y", "01111001" }, { "z", "01111010" }
            }
        };

        private void button1_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            bool reverse = radioButton1.Checked;
            bool binary = radioButton2.Checked;
            bool decode = radioButton3.Checked;

            if (text != "" && reverse == true)
            {
                // Reverse the text
                uint wordsQuantity = 0U;
                if (textBox1.Text.ToLower() == "full")
                {
                    string totalReversal = new string(text.Reverse().ToArray());
                    MessageBox.Show("Click ok or close this to copy to clipboard \n \n" + totalReversal, "---Conversion---");
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
                    MessageBox.Show("Click ok or close this to copy to clipboard \n \n" + wordReversal + $"\n Amount of words present: {wordsQuantity}", "---Conversion---");
                    Clipboard.SetText(wordReversal);
                }
            }
            else if (text != "" && binary == true)
            {
                // Convert the text to binary
                string[] words = text.Split(' ');
                string word;

                List<string> characters = new List<string>();
                string charac;
                List<string> binoredChars = new List<string>();
                string letterCode;
                string binoredWord = "";
                string binaryText = "";

                uint wordsQuantity = 0U;
                for (int i = 0; i < words.Length; i++)
                {
                    wordsQuantity++;
                    word = words[i];
                    for (int index = 0; index < word.Length; index++)
                    {
                        charac = Convert.ToString(word[index]);
                        characters.Add(charac);
                        for (int casing = 0; casing < 2; casing++)
                        {
                            for (int letter = 0; letter < 26; letter++)
                            {
                                string binaryLetter = binaryLetters[casing, letter, 0];
                                if (binaryLetter == charac)
                                {
                                    letterCode = binaryLetters[casing, letter, 1];
                                    binoredChars.Add(letterCode);

                                    if (binoredChars.Count >= word.Length)
                                    {
                                        foreach (string bytee in binoredChars) { binoredWord += bytee; }
                                        binaryText += binoredWord + " ";
                                        binoredWord = "";
                                        binoredChars.Clear();
                                    }
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Click ok or close this to copy to clipboard \n \n" + binaryText + $"\n Amount of words present: {wordsQuantity}", "---Conversion---");
                Clipboard.SetText(binaryText);
            }
            else if (text != "" && decode == true && text.Contains("0") && text.Contains("1"))
            {
                try
                {
                    // Decode the binary to text
                    string[] binaryWords = text.Split(' ');
                    string decodedText = "";

                    foreach (string word in binaryWords)
                    {
                        for (int i = 0; i < word.Length; i += 8)
                        {
                            if (i + 8 <= word.Length)
                            {
                                string chunk = word.Substring(i, 8);
                                try
                                {
                                    int ascii = Convert.ToInt32(chunk, 2);
                                    decodedText += (char)ascii;
                                }
                                catch
                                {
                                    decodedText += "?";
                                }
                            }
                        }
                        decodedText += " "; // add a space between words
                    }
                    MessageBox.Show("Click ok or close this to copy to clipboard \n \n" + decodedText, "---Conversion---");
                    Clipboard.SetText(decodedText);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid binary input. Please ensure the input is a valid binary string.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Nothing was converted because the checkbox is not checked or the input is empty.", "Error");
            }
            
        }
    }
}
