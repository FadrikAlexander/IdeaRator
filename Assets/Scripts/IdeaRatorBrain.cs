using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdeaRatorBrain : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI IdeaText;
    [SerializeField]
    TextMeshProUGUI IdeaNumText;
    FileManager fileManager;

    string GameStyle;
    string GameGenre1;
    string GameGenre2;
    string GameMechanic1;
    string GameMechanic2;
    string Words;
    string GameTheme;

    string savedIdeas;
    int savedIdeasNum;
    bool newIdea = false;

    void Start()
    {
        fileManager = FindObjectOfType<FileManager>();
        savedIdeas = "";
        savedIdeasNum = 0;
        IdeaNumText.text = "Ideas Saved: " + savedIdeasNum;

        GenerateIdea();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GenerateIdea();
    }

    //Generate a new Idea and write it to the screen
    public void GenerateIdea()
    {
        GameStyle = getWord(fileManager.GameStyle);
        GameGenre1 = getWord(fileManager.GameGenre);
        GameGenre2 = getWord(fileManager.GameGenre);
        GameMechanic1 = getWord(fileManager.GameMechanic);
        GameMechanic2 = getWord(fileManager.GameMechanic);
        GameTheme = getWord(fileManager.GameTheme);
        Words = "<b>" + getWord(fileManager.Words) + "</b>, <b>" + getWord(fileManager.Words) + "</b>, <b>" + getWord(fileManager.Words) + "</b>";
        writeIdea();
    }

    //change a part of an idea and re write it
    public void changeIdea(string ID)
    {
        switch (ID)
        {
            case "GameStyle":
                GameStyle = getWord(fileManager.GameStyle);
                break;
            case "GameGenre1":
                GameGenre1 = getWord(fileManager.GameGenre);
                break;
            case "GameGenre2":
                GameGenre2 = getWord(fileManager.GameGenre);
                break;
            case "GameMechanic1":
                GameMechanic1 = getWord(fileManager.GameMechanic);
                break;
            case "GameMechanic2":
                GameMechanic2 = getWord(fileManager.GameMechanic);
                break;
            case "GameTheme":
                GameTheme = getWord(fileManager.GameTheme);
                break;
            case "Words":
                Words = "<b>" + getWord(fileManager.Words) + "</b>, <b>" + getWord(fileManager.Words) + "</b>, <b>" + getWord(fileManager.Words) + "</b>";
                break;
        }
        writeIdea();
    }

    //create the idea string and print it on the screen
    void writeIdea()
    {
        //Style
        string gameIdea = "<size=130%><link=\"GameStyle\"><b>" + GameStyle + "</b></link></size> Game\n";

        //Genre
        gameIdea += "with <size=130%><link=\"GameGenre1\"><b>" + GameGenre1 + "</b></link></size> and <size=130%><link=\"GameGenre2\"><b>" + GameGenre2 + "</b></link></size> as main Genres\n";

        //Mechanics
        gameIdea += "Containing <size=130%><link=\"GameMechanic1\"><b>" + GameMechanic1 + "</b></link></size> and <size=130%><link=\"GameMechanic2\"><b>" + GameMechanic2 + "</b></link></size> as Mechanics\n";

        //Theme
        gameIdea += "all in <size=130%><link=\"GameTheme\"><b>" + GameTheme + "</b></link></size> theme\n\n";

        //Words
        gameIdea += "Random Words: <size=130%><link=\"Words\">" + Words + "</link></size>";

        IdeaText.text = gameIdea;
        newIdea = true;
    }

    //get a random word from a list
    string getWord(List<string> list)
    {
        return list[Random.Range(0, list.Count)].Replace("\r", "").Replace("\n", "");
    }

    //create the copy version of the idea without the Rich Text and add it to the clipboard
    public void CopyIdea()
    {
        if (newIdea)
        {
            //Style
            string gameIdea = GameStyle + " Game\n";
            //Genre
            gameIdea += "with " + GameGenre1 + " and " + GameGenre2 + " as main Genres\n";
            //Mechanics
            gameIdea += "Containing " + GameMechanic1 + " and " + GameMechanic2 + " as Mechanics\n";
            //Theme
            gameIdea += "all in " + GameTheme + " theme\n";
            //Words
            gameIdea += "Random Words: " + Words;

            savedIdeasNum++;
            savedIdeas += "Idea " +savedIdeasNum + ":  \n" + gameIdea + "\n\n\n";
            
            IdeaNumText.text = "Ideas Saved: " + savedIdeasNum;
            newIdea = false;

            TextEditor copyIdea = new TextEditor();
            copyIdea.text = savedIdeas;
            copyIdea.SelectAll();
            copyIdea.Copy();
        }
    }
}
