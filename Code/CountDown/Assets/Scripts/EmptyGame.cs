using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class EmptyGame : IGame 
{
    private string m_AvailableLetters = "areallylongword";

    private int m_NumberOfLetters = 0;

    private List<string> m_ScoreRecord = new List<string>();
    private List<string> m_Words = new List<string>();

    private WordParser m_Parser = new WordParser();


    public EmptyGame(TextAsset a_WordList, int a_NumberOfLetters)
    {
        m_Words = m_Parser.GetWords(a_WordList);
        SetNumberOfLetters(a_NumberOfLetters);
    }

    /// <summary>
    /// Set number of letters
    /// </summary>
    /// <param name="a_NumberOfLetters">Number of letters</param>
    public void SetNumberOfLetters(int a_NumberOfLetters)
    {
        m_NumberOfLetters = a_NumberOfLetters;

        if (m_NumberOfLetters > 0)
        {
            //Given more time this would probably not be hard coded here. Maybe in the inspector or in a config file?
            string letters = "aaaabbbbccddeeeeffggghhiiiijjjkkllllmmnnoooppppqqrrrssssttttuuuuvvwwxxyyzz";

            System.Random rnd = new System.Random();
            m_AvailableLetters = new string(letters.ToCharArray().OrderBy(x => rnd.Next()).Take(m_NumberOfLetters).ToArray());
        }
    }

    public void SubmitWord(string a_Word)
	{
        if (m_Parser.IsValidWord(a_Word, ref m_AvailableLetters, m_Words))
        {
            m_ScoreRecord.Add(a_Word);
            m_ScoreRecord = m_ScoreRecord.OrderByDescending(r => r.Length).ToList();

            if (m_ScoreRecord.Count > 10)
            {
                m_ScoreRecord.RemoveRange(10, m_ScoreRecord.Count - 10);
            }
        }
    }

	public string GetWordEntryAtPosition(int a_Position)
	{
        if (a_Position >= m_ScoreRecord.Count)
        {
            return "";
        }
        else
        {
            return m_ScoreRecord[a_Position];
        }
    }

	public int GetScoreAtPosition(int a_Position)
	{
        if (a_Position >= m_ScoreRecord.Count)
        {
            return 0;
        }
        else
        {
            return m_ScoreRecord[a_Position].Length;
        }
    }

    public string GetAvailableLetters()
    {
        return m_AvailableLetters;
    }

    public void Reset()
    {
        // Reset scoreboard and change letters
        m_ScoreRecord.Clear();
        SetNumberOfLetters(m_NumberOfLetters);
    }
}
