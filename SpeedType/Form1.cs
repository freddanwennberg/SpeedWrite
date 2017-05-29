using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Documents;


namespace SpeedType
{


    public partial class Form1 : Form
    {
        class Word
        {
            public int startindex;
            public int endIndex;
            public string word;
        }

        rm.Trie.Trie wordList;

        List<Word> inputWordList = new List<Word>();
        BKTree dic;

        int caretPosition;

        public Form1()
        {
            InitializeComponent();

            string inputText3 = "  \nJ\na\ng är \t\n\n\n\n\n \n\nfi\n\ns\n\nk \n slut   fridap\n\n\n\nluttgurkan";
            richTextBox1.Text = inputText3;

            wordList = new rm.Trie.Trie();

            dic = new BKTree();


            createWords(inputText3);
            PrintWords();
            CreateTrie();

            List<string> prefix = dic.Search("Hell", 1);

        }

        private void CreateTrie()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
             new System.IO.StreamReader("c:\\20k.txt");
            while ((line = file.ReadLine()) != null)
            {
                wordList.AddWord(line + ";" + counter.ToString());
                dic.Add(line, counter);
                counter++;
            }

        }


        private void createWords(string input)
        {
            if (inputWordList.Count > 0) inputWordList.Clear();

            string charList = "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ'";
            Word word = null;
            int i = 0;

            bool isWord = false;

            foreach (char c in input)
            {
                if (charList.Contains(c.ToString()))
                {
                    if (isWord == false) // nytt ord 
                    {
                        word = new Word();
                        word.startindex = i;
                    }

                    isWord = true;
                    word.word += c.ToString();
                }
                else
                {
                    if (isWord == true) // slut på ord 
                    {
                        word.endIndex = i;
                        inputWordList.Add(word);
                        isWord = false;
                    }
                }

                i++;
            }

            // close the word if the last charactare in the input text is a char 
            if (isWord == true && i == input.Length)
            {
                word.endIndex = input.Length;
                inputWordList.Add(word);
            }

        }

        private Word GetWordFromIndex(int i)
        {
            Word error = new Word();
            error.word = "error";

            foreach (Word word in inputWordList)
            {
                if (i >= word.startindex && i <= word.endIndex)
                {
                    return word;
                }
            }

            return error;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            createWords(richTextBox1.Text); 
            PrintWords();
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            caretPosition = richTextBox1.SelectionStart;
            PrintWordAtCaret();
            //SuggestWordByPrefixAtCaret(); 
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            caretPosition = richTextBox1.SelectionStart;
            PrintWordAtCaret();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string word = "Insert text";

            int insertWordAt = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Insert(insertWordAt, word);

            richTextBox1.SelectionStart = insertWordAt + word.Length;
            richTextBox1.Focus();

            foreach (string pword in wordList.GetWords("T"))
            {
                DebugTextBox.Text += pword + "\n";
            }

            createWords(richTextBox1.Text);
        }

        private void PrintWordAtCaret()
        {
            Word word = GetWordFromIndex(caretPosition);
            DebugTextBox.Text = "";
            DebugTextBox.Text += caretPosition + " > " + word.word + "\n";

            if (word.word.Length > 1)
            {
                Dictionary<string, int> prefixWordsByOrder = new Dictionary<string, int>();

                foreach (string pword in wordList.GetWords(word.word))
                {
                    string[] res = pword.Split(';');
                    prefixWordsByOrder.Add(res[0], Convert.ToInt32(res[1]));
                }

                var output = prefixWordsByOrder.OrderBy(e => e.Value).Select(e => new { frequency = e.Value, word = e.Key }).ToList();

                foreach (var entry in output)
                {
                    DebugTextBox.Text += entry.word + ": " + entry.frequency + "\n";
                }
            }

        }

        void SuggestWordByPrefixAtCaret()
        {
            Word word = GetWordFromIndex(caretPosition);
            DebugTextBox.Text = "";
            DebugTextBox.Text += caretPosition + " > " + word.word + "\n";

            if (word.word.Length > 3)
            {
                Dictionary<string, int> prefixWordsByOrder = new Dictionary<string, int>();

                foreach (string pword in dic.Search(word.word, 1))
                {
                    string[] res = pword.Split(';');
                    prefixWordsByOrder.Add(res[0], Convert.ToInt32(res[1]));
                }

                var output = prefixWordsByOrder.OrderBy(e => e.Value).Select(e => new { frequency = e.Value, word = e.Key }).ToList();

                foreach (var entry in output)
                {
                    DebugTextBox.Text += entry.word + ": " + entry.frequency + "\n";
                }
            }

        }

        private void PrintWords()
        {
            DebugWordListTextBox.ResetText();
            foreach (Word word in inputWordList)
            {
                DebugWordListTextBox.Text += word.startindex.ToString() + ":" + word.endIndex.ToString() + ": " + word.word + "\n";
            }
        }


    }
}
