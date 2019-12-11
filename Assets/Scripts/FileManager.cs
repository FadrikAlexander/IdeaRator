using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public List<string> GameStyle;
    public List<string> GameGenre;
    public List<string> GameMechanic;
    public List<string> GameTheme;
    public List<string> Words;

    //Text Files
    public List<TextAsset> Files;


    void Awake()
    {
        //Load all The Files
        GameStyle = LoadFile(GameStyle,0);
        GameGenre = LoadFile(GameGenre,1);
        GameMechanic = LoadFile(GameMechanic,2);
        GameTheme = LoadFile(GameTheme,3);
        Words = LoadFile(Words,4);
    }

    //The Load Function
    List<string> LoadFile(List<string> Wordlist,int index)
    {
        //Initialize the list
        Wordlist = new List<string>();

        //get file string
        string fileString = Files[index].text;

        //split the strings and add them to the list
        foreach(string s in fileString.Split('\n'))
                Wordlist.Add(s);
        
        //return list
        return Wordlist;
    }
}
