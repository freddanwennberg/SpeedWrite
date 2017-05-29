using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AlgoLib; 


namespace SpeedWrite
{
    public partial class Form1 : Form
    {
        private Trie<int> wordList;
        private BKTree bktree;

        int cursorPosition;

        struct WordInTextBox
        {
            public string word;
            public int start; 
            public int end; 
        };

        List<WordInTextBox> words;
        int nextWordInList;
        bool wordSelected;
        bool changedWord; 
                             

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            wordList = new Trie<int>();
            bktree = new BKTree();
            words = new List<WordInTextBox>(); 
            
            string line;
            int rank = 0;
            string word;
            int lineNum = 0;
            nextWordInList = 0;
            wordSelected = false;
            changedWord = true; 

                        
            System.IO.StreamReader file = new System.IO.StreamReader("..\\..\\wordlist.txt");
            
            while ((line = file.ReadLine()) != null)
            {
                lineNum++; 

                try
                {
                    string[] data = line.Split(null);
                  
                    rank = Convert.ToInt32(data[0]);
                    //word = data[1] + " " + data[2] + " " + data[3];
                    word = data[1]; 

                    wordList.Add(word, rank);
                    bktree.Add(word); 
                 
                }
                catch (Exception ee)
                {
                    Debug.WriteLine("At line: {0}", lineNum);
                    Debug.WriteLine("{0} Exception caught.", ee);
                }

            }
            
        }

        
        private void inputWord_TextChanged(object sender, EventArgs e)
        {
            resultWordList.Text = null; 

            if (inputWord.Text.Length > 2)
            {
                var resWords = wordList.GetByPrefix(inputWord.Text).OrderBy(key => key.Value).ToList();

                resultWordList.Text = null; 

                foreach (var element in resWords)
                {
                    //Debug.Write(element.Key + "\t" + element.Value + "\n");
                    resultWordList.Text += element.Key + ":\t" + element.Value + "\n"; 
                }             
            }
        }

        

        private void InputDistance_TextChanged(object sender, EventArgs e)
        {
            resultApprox.Text = null;

            if (inputDistance.Text.Length > 2)
            {
                var resWords = bktree.Search(inputDistance.Text, 1); 

                resultWordList.Text = null;

                foreach (var element in resWords)
                {
                    //Debug.Write(element.Key + "\t" + element.Value + "\n");
                    //resultWordList.Text += element.Key + ":\t" + element.Value + "\n";
                    resultApprox.Text += element + "\n"; 
                }
            }
        }

        private void selectWord()
        {
            int cursorPosition = richTextBox1.SelectionStart; // Get Carrect pos 
            int nextSpace = richTextBox1.Text.IndexOf(' ', cursorPosition);
            int selectionStart = 0;

            string trimmedString = string.Empty;
                        
            if (nextSpace != -1)
            {
                trimmedString = richTextBox1.Text.Substring(0, nextSpace);
            }
            else
            {
                trimmedString = richTextBox1.Text;
            }

            if (trimmedString.LastIndexOf(' ') != -1)
            {
                selectionStart = 1 + trimmedString.LastIndexOf(' ');
                trimmedString = trimmedString.Substring(1 + trimmedString.LastIndexOf(' '));
            }
           
            richTextBox1.SelectionStart = selectionStart;
            richTextBox1.SelectionLength = trimmedString.Length;
        }

        private bool IsLetter(char c)
        {
            const string validCharacters = "abcdefghijklmnopqrstuvwxyzåäö"; 
            char cc = Char.ToLower(c);

            for (int i = 0; i < validCharacters.Length; i++)
            {
                if (cc == validCharacters[i]) return true;   
            }

            return false; 
        }

        private int SelectWord()
        {
            // monkeys are flat
            // xxxxxxxxxxxxxcxx

            int leftBound = 0;   
            int rightBound = 0;


            if (cursorPosition <= richTextBox1.Text.Length)
            {
                
                leftBound = cursorPosition;
                while (leftBound > 0 && IsLetter(Convert.ToChar(richTextBox1.Text[leftBound]))) leftBound--;

                rightBound = cursorPosition;
                while (rightBound > 0 && rightBound < richTextBox1.Text.Length && IsLetter(Convert.ToChar(richTextBox1.Text[rightBound]))) rightBound++;

                richTextBox1.SelectionStart = leftBound;
                richTextBox1.SelectionLength = rightBound;

                Debug.WriteLine("start select: {0}\n", leftBound);
                Debug.WriteLine("lenght: {0}\n", rightBound); 
                 

            }

            return 1; 

        }
      

        /*
                            
          Tar in en sträng med text och indexerar orden basrat på cursor position. 
          
        */
                
        private void ParseWords(string text)
        {
            int cursor = 0;
            bool firstCharInWord = true; 
            
            WordInTextBox word = new WordInTextBox();
            words.Clear(); 
            
            foreach (char c in text)
            {
                if ( (char.IsLetter(c)) && firstCharInWord == true)
                {
                    word.word += c; 
                    word.start = cursor;
                    firstCharInWord = false;
                 
                }
                else if (char.IsLetter(c))
                {
                    word.word += c;
                 
                }
                // Add words based on space or new line as delimiter 
                if (c == ' ' || c == '\n')
                {
                    firstCharInWord = true;
                    if (word.word != null)
                    {
                        word.end = cursor;
                        words.Add(word);
                        word.word = null;
                        word.end = word.start = 0;
                    }
                }

                cursor++;                        
            }

            word.end = cursor; 
            words.Add(word);  // add the word that is currently being enterd 

        }

        private string GetWordFromSelection(int cursor)
        {
            string result = null; 
            foreach (WordInTextBox word in words)
            {
                if (cursor >= word.start && cursor <= word.end)
                {
                    result = word.word; 
                    break;
                }
            }
            return result; 

        }

        private string GetSuffixWord(string prefix, int i)
        {
            var resWords = wordList.GetByPrefix(prefix).OrderBy(key => key.Value).ToList();
            string suffix = null; 
            if (resWords.Count > 0)
            {
                suffix = resWords[i].Key.ToString().Remove(0, prefix.Length);
            }

            return suffix; 
        }

        /*
           App
          
          
         */ 



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
                ParseWords(richTextBox1.Text); // ïndexer om alla ord vid en förändring av texten 

                cursorPosition = richTextBox1.SelectionStart;  

                string currentTyped = GetWordFromSelection(cursorPosition); // 

                debugTextBox.Text = currentTyped; 
                                        
                    if (currentTyped != null && currentTyped.Length > 2)
                    {
                        string suffix = null;  

                        while (!wordSelected)
                        {
                            if (changedWord)
                            {
                                if (nextWordInList > 0) // how to handle full tab loop that will get back to 0 
                                {
                                    suffix = GetSuffixWord(currentTyped, nextWordInList - 1);
                                    richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - suffix.Length, suffix.Length);         
                                }

                                suffix = GetSuffixWord(currentTyped, nextWordInList);
                                richTextBox1.Text += suffix;
                                changedWord = false; 
                            }

                            //richTextBox1.SelectionStart = cursorPosition;

                            Application.DoEvents();                                             
 
                        }

                        wordSelected = !wordSelected;

                        if (suffix != null)
                        {
                            currentTyped += suffix;
                            richTextBox1.SelectionStart = cursorPosition + suffix.Length + 3;
                        }
                    }

    
                ParseWords(richTextBox1.Text); 
            

            
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            cursorPosition = richTextBox1.SelectionStart;
            debugTextBox.Text = GetWordFromSelection(cursorPosition); 
           
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
            cursorPosition = richTextBox1.SelectionStart;
            debugTextBox.Text = GetWordFromSelection(cursorPosition); 
           
                      
            if (e.KeyCode == Keys.F1)
            {                
                nextWordInList++;
                changedWord = !changedWord; 
            }

            if (e.KeyCode == Keys.Escape)
            {
                wordSelected = !wordSelected;
            }
        
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ParseWords(richTextBox1.Text);
        
        }

        private void debugTextBox_Changed(object sender, EventArgs e)
        {
          
        }



    }
}
