using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using File = Godot.File;

public static class GameSaver
{
   

    public static void SaveGameScene()
    {
        var saveSceneGame = new File();
        saveSceneGame.Open("user://savegameScene.save", File.ModeFlags.Write);

        Dictionary<string, int> dicToSave = WorldDictionary.returnDictionary();
        saveSceneGame.StoreLine(JSON.Print(dicToSave));

        saveSceneGame.Close();
    }

    public static void SaveGameInvenotry()
    {
        var saveSceneGame = new File();
        saveSceneGame.Open("user://savegameInventory.save", File.ModeFlags.Write);

        Dictionary<string, int> dicToSave = WorldDictionary.returnDictionary();
        saveSceneGame.StoreLine(JSON.Print(dicToSave));

        saveSceneGame.Close();
    }

}

