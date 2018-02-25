using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WordParser
{
    public List<string> GetWords(TextAsset a_WordList)
    {
        //Load word data from the file.
        return LoadWordData(a_WordList.text);
    }
    
    /// <summary>
    /// Checks to see if the entered letters are valid. Mark them as 'used'
    /// </summary>
    /// <param name="a_Word">Word to check validity</param>
    /// <param name="a_AvailableLetters">List of valid letters</param>
    public bool IsValidWord(string a_Word, ref string a_AvailableLetters, List<string> a_WordList)
    {
        char[] enteredLetters = a_Word.ToArray();
        char[] availableLetters = a_AvailableLetters.ToArray();

        for (int i = 0; i < enteredLetters.Length; i++)
        {
            if (!availableLetters.Contains(enteredLetters[i]))
            {
                // One of letters entered is not valid
                return false;
            }
            else
            {
                for(int j = 0; j < availableLetters.Length; j++)
                {
                    if (availableLetters[j] == enteredLetters[i])
                    {
                        // The valid letter is marked 'used'
                        availableLetters[j] = ' ';
                        break;
                    }
                }
            }
        }

        if (!IsInWordList(a_Word, a_WordList))
        {
            return false;
        }

        a_AvailableLetters = new string(availableLetters);
        return true;
    }

    /// <summary>
    /// Loads word data from string
    /// </summary>
    /// <param name="a_WordList">List of words</param>
    List<string> LoadWordData(string a_WordList)
    {
        return a_WordList.Split('\n').ToList();
    }

    /// <summary>
    /// Check to see if a word is available in a list of words
    /// </summary>
    /// <param name="a_Word">Word to check validity of</param>
    /// <param name="a_WordList">Word List</param>
    public bool IsInWordList(string a_Word, List<string> a_WordList)
    {
        List<string> shortList = GetShortListOfWords(a_WordList, a_Word.Length, a_Word.Substring(0, 1), a_Word.Substring(a_Word.Length - 1, 1));
        return IsWordExist(shortList, a_Word);
    }

    List<string> GetShortListOfWords(List<string> a_WordList, int a_Length, string a_StartCharacter, string a_EndCharacter)
    {
        return a_WordList.Where(o => o.Length == a_Length && o.StartsWith(a_StartCharacter) && o.EndsWith(a_EndCharacter)).ToList();
    }

    bool IsWordExist(List<string> a_WordList, string a_Word)
    {
        return a_WordList.Contains(a_Word);
    }

}
