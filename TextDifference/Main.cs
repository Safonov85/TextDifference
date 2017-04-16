using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDifference
{

    public partial class MainForm : Form
    {
        public enum DiffSectionType
        {
            Copy,
            Insert,
            Delete
        }

        public struct DiffSection
        {
            private readonly DiffSectionType _Type;
            private readonly int _Length;

            public DiffSection(DiffSectionType type, int length)
            {
                _Type = type;
                _Length = length;
            }

            public DiffSectionType Type
            {
                get
                {
                    return _Type;
                }
            }

            public int Length
            {
                get
                {
                    return _Length;
                }
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", Type, Length);
            }
        }

        string firstText, secondText;
        int? selectFirst, selectLast;
        int potensNum = 2 * 2 * 2;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text1RichTextBox.SelectionBackColor = Color.Yellow;
            Text1RichTextBox.AutoWordSelection = false;
            Text1RichTextBox.Text = null;
            Text2RichTextBox.Text = null;
        }

        private void LoadText1Button_Click(object sender, EventArgs e)
        {
            SelectFileFromDialog("1");
            //GetMissingWord();
            //HighlightText();
        }

        private void LoadText2Button_Click(object sender, EventArgs e)
        {
            SelectFileFromDialog("2");
            //GetMissingWord();
            //var result = FindLongestCommonSubstring(Text1RichTextBox.Text, Text2RichTextBox.Text);

            var result = Diff(
    Text1RichTextBox.Text.ToCharArray(), 0, Text1RichTextBox.Text.Length,
    Text2RichTextBox.Text.ToCharArray(), 0, Text2RichTextBox.Text.Length,
    EqualityComparer<char>.Default);

            //Text1RichTextBox.Text += Environment.NewLine + result.
            //Text1RichTextBox.Text += Environment.NewLine + result.Success;
            //HighlightText();
            //MatchString(Text1RichTextBox.Text, Text2RichTextBox.Text);
            //Text1RichTextBox.Text += Environment.NewLine + LCSBack(Text1RichTextBox.Text, Text2RichTextBox.Text);
        }

        void SelectFileFromDialog(string richTextBox)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\Users\\MinDator\\Desktop\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {

                        using (TextReader reader = new StreamReader(myStream, Encoding.GetEncoding(1252), true)) // Using special encoding to get Swedish åäö letters
                        {
                            String text = reader.ReadToEnd();
                            if(richTextBox == "1")
                            {
                                Text1RichTextBox.Text = text;
                                firstText = Text1RichTextBox.Text;
                            }
                            else
                            {
                                Text2RichTextBox.Text = text;
                                secondText = Text2RichTextBox.Text;
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private IEnumerable<DiffSection> Diff<T>(
    IList<T> firstCollection, int firstStart, int firstEnd,
    IList<T> secondCollection, int secondStart, int secondEnd,
    IEqualityComparer<T> equalityComparer)
        {
            var lcs = FindLongestCommonSubstring(
                firstCollection, firstStart, firstEnd,
                secondCollection, secondStart, secondEnd,
                equalityComparer);

            if (lcs.Success)
            {
                // deal with the section before
                var sectionsBefore = Diff(
                    firstCollection, firstStart, lcs.PositionInFirstCollection,
                    secondCollection, secondStart, lcs.PositionInSecondCollection,
                    equalityComparer);
                foreach (var section in sectionsBefore)
                    yield return section;

                // output the copy operation
                yield return new DiffSection(
                    DiffSectionType.Copy,
                    lcs.Length);

                // deal with the section after
                var sectionsAfter = Diff(
                    firstCollection, lcs.PositionInFirstCollection + lcs.Length, firstEnd,
                    secondCollection, lcs.PositionInSecondCollection + lcs.Length, secondEnd,
                    equalityComparer);
                foreach (var section in sectionsAfter)
                    yield return section;

                yield break;
            }

            // if we get here, no LCS

            if (firstStart < firstEnd)
            {
                // we got content from first collection --> deleted
                yield return new DiffSection(
                    DiffSectionType.Delete,
                    firstEnd - firstStart);
            }
            if (secondStart < secondEnd)
            {
                // we got content from second collection --> inserted
                yield return new DiffSection(
                    DiffSectionType.Insert,
                    secondEnd - secondStart);
            }
        }

        void CompareStrings(string text1, string text2)
        {
            int result = 0;

            result = string.Compare(text1, text2);


        }

        public LongestCommonSubstringResult FindLongestCommonSubstring(
    string firstText,
    string secondText)
        {
            return FindLongestCommonSubstring(
                firstText.ToCharArray(),
                secondText.ToCharArray());
        }

        public LongestCommonSubstringResult FindLongestCommonSubstring<T>(
            IList<T> firstCollection,
            IList<T> secondCollection)
        {
            return FindLongestCommonSubstring(
                firstCollection,
                secondCollection,
                EqualityComparer<T>.Default);
        }

        public LongestCommonSubstringResult FindLongestCommonSubstring<T>(
    IList<T> firstCollection,
    IList<T> secondCollection,
    IEqualityComparer<T> equalityComparer)
        {
            // default result, if we can't find anything
            var result = new LongestCommonSubstringResult();

            for (int index1 = 0; index1 < firstCollection.Count; index1++)
            {
                for (int index2 = 0; index2 < secondCollection.Count; index2++)
                {
                    if (equalityComparer.Equals(
                        firstCollection[index1],
                        secondCollection[index2]))
                    {
                        int length = CountEqual(
                            firstCollection, index1,
                            secondCollection, index2,
                            equalityComparer);

                        // Is longer than what we already have --> record new LCS
                        if (length > result.Length)
                        {
                            result = new LongestCommonSubstringResult(
                                index1,
                                index2,
                                length);
                        }
                    }
                }
            }

            return result;
        }

        public int CountEqual<T>(
            IList<T> firstCollection, int firstPosition,
            IList<T> secondCollection, int secondPosition,
            IEqualityComparer<T> equalityComparer)
        {
            int length = 0;
            while (firstPosition < firstCollection.Count
                && secondPosition < secondCollection.Count)
            {
                if (!equalityComparer.Equals(
                    firstCollection[firstPosition],
                    secondCollection[secondPosition]))
                {
                    break;
                }

                firstPosition++;
                secondPosition++;
                length++;
            }
            return length;
        }

        public LongestCommonSubstringResult FindLongestCommonSubstring<T>(
    IList<T> firstCollection, int firstStart, int firstEnd,
    IList<T> secondCollection, int secondStart, int secondEnd,
    IEqualityComparer<T> equalityComparer)
        {
            // default result, if we can't find anything
            var result = new LongestCommonSubstringResult();

            for (int index1 = firstStart; index1 < firstEnd; index1++)
            {
                for (int index2 = secondStart; index2 < secondEnd; index2++)
                {
                    if (equalityComparer.Equals(
                        firstCollection[index1],
                        secondCollection[index2]))
                    {
                        int length = CountEqual(
                            firstCollection, index1, firstEnd,
                            secondCollection, index2, secondEnd,
                            equalityComparer);

                        // Is longer than what we already have --> record new LCS
                        if (length > result.Length)
                        {
                            result = new LongestCommonSubstringResult(
                                index1,
                                index2,
                                length);
                        }
                    }
                }
            }

            return result;
        }

        public int CountEqual<T>(
            IList<T> firstCollection, int firstPosition, int firstEnd,
            IList<T> secondCollection, int secondPosition, int secondEnd,
            IEqualityComparer<T> equalityComparer)
        {
            int length = 0;
            while (firstPosition < firstEnd
                && secondPosition < secondEnd)
            {
                if (!equalityComparer.Equals(
                    firstCollection[firstPosition],
                    secondCollection[secondPosition]))
                {
                    break;
                }

                firstPosition++;
                secondPosition++;
                length++;
            }
            return length;
        }

        void GetMissingWord()
        {
            if (string.IsNullOrEmpty(firstText))
            {
                return;
            }
            if (string.IsNullOrEmpty(secondText))
            {
                return;
            }
            List<string> listFirstText = new List<string>();
            List<string> listSecondText = new List<string>();
            List<int> listRemovedWords = new List<int>();

            // Add words to First Text list
            string[] wordsText1 = firstText.Split(' ');
            foreach (string word in wordsText1)
            {
                listFirstText.Add(word);
            }

            // Add words to Second Text list
            string[] wordsText2 = secondText.Split(' ');
            foreach (string word in wordsText2)
            {
                listSecondText.Add(word);
            }

            int num = 0;

            while(num < 1)
            {
                num++;
                int index = 0;
                foreach(string word in listFirstText)
                {
                    if(word == listSecondText[index])
                    {
                        index++;
                        continue;
                    }
                    //else
                    //{
                    //    listRemovedWords.Add(index);
                    //    listSecondText.RemoveAt(index);
                    //    num = 0;
                    //    break;
                    //}
                }
            }
            foreach (string wurd in listFirstText)
            {
                Text1RichTextBox.Text += Environment.NewLine + wurd;
            }
        }



        public string LCSBack(string text1, string text2)
        {
            string aSub = text1.Substring(0, (text1.Length - 1 < 0) ? 0 : text1.Length - 1);
            string bSub = text2.Substring(0, (text2.Length - 1 < 0) ? 0 : text2.Length - 1);

            if (text1.Length == 0 || text2.Length == 0)
            {
                return "";
            }
            else if (text1[text1.Length - 1] == text2[text2.Length - 1])
            {
                return LCSBack(aSub, bSub) + text1[text1.Length - 1];
            }
            else
            {
                string x = LCSBack(text1, bSub);
                string y = LCSBack(aSub, text2);
                return (x.Length > y.Length) ? x : y;
            }
        }

        void MatchString(string text1, string text2)
        {
            char[] textChar1 = text1.ToCharArray();
            List<char> listFirstText = textChar1.OfType<char>().ToList();


            char[] textChar2 = text2.ToCharArray();
            List<char> listSecondText = textChar2.OfType<char>().ToList();

            

            int count = 0;
            foreach(char letter in listFirstText)
            {
                if(letter != listSecondText[count])
                {
                    //Text2RichTextBox.Select(count, count);

                    //Text2RichTextBox.SelectionBackColor = Color.Yellow;

                    if(selectFirst == null)
                    {
                        selectFirst = count;
                    }
                    else
                    {
                        selectLast = count;
                    }
                    listSecondText.RemoveAt(count);
                }
                count++;
            }

            Text1RichTextBox.Select((int)selectFirst, (int)selectLast);

            Text1RichTextBox.SelectionBackColor = Color.Yellow;

            //int count = 0;
            //foreach(char array in textChar1)
            //{
            //    if(array != textChar2[count])
            //    {
            //        //Text1RichTextBox.Text += Environment.NewLine + array.ToString();

            //        Text2RichTextBox.Select(count, count);

            //        Text2RichTextBox.SelectionBackColor = Color.Yellow;


            //    }

            //    count++;
            //}
        }

        int CountSpaceChars(string value)
        {
            int result = 0;
            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    result++;
                }
            }
            return result;
        }

        void HighlightText()
        {
            //if (String.IsNullOrEmpty(Text1RichTextBox.Text))
            //{
            //    return;
            //}
            //int start = 0;
            //int end = Text1RichTextBox.Text.LastIndexOf("hoppade");

            Text1RichTextBox.SelectAll();
            Text1RichTextBox.SelectionBackColor = Color.Transparent;


            //Text1RichTextBox.Find("hoppade", 0, Text1RichTextBox.TextLength, RichTextBoxFinds.MatchCase);

            //Text1RichTextBox.Select(5, 10);
            
            //Text1RichTextBox.SelectionBackColor = Color.Yellow;

            Text1RichTextBox.Select(1, 27 - 20);

            Text1RichTextBox.SelectionBackColor = Color.Yellow;

            //start = Text1RichTextBox.Text.IndexOf("hoppade", start) + 1;

        }

        
    }
}
