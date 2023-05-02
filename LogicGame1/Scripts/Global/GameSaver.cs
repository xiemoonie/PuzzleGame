using Godot;
using System;
using System.Collections.Generic;


public class GameSaver
{



    public void someFun(Node parentInventory,Node parentScene)
    {
        var saveNodes = parentScene.GetTree().GetNodesInGroup("Persist");
        var saveItemInventory = parentInventory.GetTree().GetNodesInGroup("PersistInventory");
        foreach (Node saveNode in saveNodes)
        {
            SaveGameScene(parentScene);
        }
        foreach (Node saveNode in saveItemInventory)
        {
            SaveInventory(parentInventory);
        }

    }

    public void SaveInventory(Node parentInventory)
    {
        var saveGame = new File();
        saveGame.Open("user://savegameInventory.save", File.ModeFlags.Write);
        GD.Print("saving...");
        var saveNodes = parentInventory.GetTree().GetNodesInGroup("PersistInventory");
        foreach (Node saveNode in saveNodes)
        {
            // Check the node is an instanced scene so it can be instanced again during load.
            if (saveNode.Filename.Empty())
            {
                GD.Print(String.Format("persistent node '{0}' is not an instanced scene, skipped", saveNode.Name));
                continue;
            }

            // Check the node has a save function.
            if (!saveNode.HasMethod("Save"))
            {
                GD.Print(String.Format("persistent node '{0}' is missing a Save() function, skipped", saveNode.Name));
                continue;
            }

            // Call the node's save function.
            var nodeData = saveNode.Call("Save");

            // Store the save dictionary as a new line in the save file.
            saveGame.StoreLine(JSON.Print(nodeData));
            GD.Print("saving 1...", nodeData.ToString());
        }

        saveGame.Close();

    
}

        public void SaveGameScene(Node parent)
    {
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);
        GD.Print("saving...");
        var saveNodes = parent.GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
        {
            // Check the node is an instanced scene so it can be instanced again during load.
            if (saveNode.Filename.Empty())
            {
                GD.Print(String.Format("persistent node '{0}' is not an instanced scene, skipped", saveNode.Name));
                continue;
            }

            // Check the node has a save function.
            if (!saveNode.HasMethod("Save"))
            {
                GD.Print(String.Format("persistent node '{0}' is missing a Save() function, skipped", saveNode.Name));
                continue;
            }

            // Call the node's save function.
            var nodeData = saveNode.Call("Save");

            // Store the save dictionary as a new line in the save file.
            saveGame.StoreLine(JSON.Print(nodeData));
            GD.Print("saving 1...", nodeData.ToString());
        }

        saveGame.Close();
      
        SceneController myScript = parent.GetNode<SceneController>("/root/Main/SceneController");
        myScript.goToMenu();

    }

    // Note: This can be called from anywhere inside the tree. This function is
    // path independent.

    public void LoadGame(HFlowContainer parentOne, Control parentTwo)
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegame.save"))
        {
            return; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT");
        }


        // Load the file line by line and process that dictionary to restore the object
        // it represents.
        saveGame.Open("user://savegame.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);


            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (Node)newObjectScene.Instance();
            var newCoin = nodeData["Coin"];
            if (newCoin == null)
            {


                GD.Print("name object on file", newObject.Name);

                var coin = newObject.GetNode<Sprite>("Coin/Coin");
                coin.QueueFree();
                parentTwo.AddChild(newObject);
            }
            else
            {
                parentTwo.AddChild(newObject);
            }

            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Coin" ||  key == "PosX" || key == "PosY" || key == "Screwdriver")
                    continue;
                newObject.Set(key, entry.Value);
            }
        }

        saveGame.Close();
        LoadInventory(parentOne);

    }


    public void LoadInventory(HFlowContainer parentOne)
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegameInventory.save"))
        {
            return; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE INVENTORY FOUND!! name of parent:", parentOne.Name);
            
        }

        saveGame.Open("user://savegameInventory.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
          //  var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
          //  var newObject = (Node)newObjectScene.Instance();


            var newItem = (PackedScene)ResourceLoader.Load(nodeData["InventoryItem"].ToString());
            var newItemObject = (Node)newItem.Instance();

            GD.Print("Type array:", nodeData["TextureFile"].GetType().Name);

            var listTextures = nodeData["TextureFile"] as Godot.Collections.Array;
            if (listTextures.Count != 0)
            {

                GD.Print("name object on file", listTextures[0].ToString());
                var item = newItemObject.GetNode<TextureRect>("Content/Texture");
                item.Texture = ResourceLoader.Load<Texture>(listTextures[0] as String);

                
                parentOne.AddChild(newItemObject);
            }
            else
            {
                parentOne.AddChild(newItemObject);
            }
           
            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "TextureFile" || key == "InventoryItem")
                    continue;
                newItemObject.Set(key, entry.Value);
            }
        }

        saveGame.Close();

    }
}