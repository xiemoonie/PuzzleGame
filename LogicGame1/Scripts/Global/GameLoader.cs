using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public static class GameLoader
{
    public static bool LoadScene()
    {
        var saveGame = new Godot.File();
        if (!saveGame.FileExists("user://savegameScene.save"))
        {
            return false;
        }
        else
        {
            GD.Print("FILE INVENTORY FOUND!!");

        }
        saveGame.Open("user://savegameScene.save", Godot.File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            List<string> keys = WorldDictionary.getKeys();
            foreach (var key in keys)
            {
                string state = nodeData[key].ToString();
                GD.Print("state:", state, key);
                WorldDictionary.setStateObject(key, state.ToInt());

            }       
        }
        saveGame.Close();
        return true;
    }



}