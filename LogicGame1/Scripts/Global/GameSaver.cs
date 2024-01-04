using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class GameSaver
{
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
            GD.Print("saving Inventory right here...", nodeData.ToString());
        }
        saveGame.Close();
    }
    public void LoadInventory(HFlowContainer parentInventory)
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegameInventory.save"))
        {
            return; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE INVENTORY FOUND!! name of parent:", parentInventory.Name);

        }
        saveGame.Open("user://savegameInventory.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
            var newItem = (PackedScene)ResourceLoader.Load(nodeData["InventoryItem"].ToString());


            GD.Print("Type array:", nodeData["TextureFile"].GetType().Name);


            var listTextures = nodeData["TextureFile"] as Godot.Collections.Array;
            for (int i = 0; i < listTextures.Count; i++)
            {
                var newItemObject = (Node)newItem.Instance();
                GD.Print("name object on file", listTextures[i].ToString());
                var item = newItemObject.GetNode<TextureRect>("Content/Texture");
                item.Texture = ResourceLoader.Load<Texture>(listTextures[i] as String);
                parentInventory.AddChild(newItemObject);


                foreach (KeyValuePair<string, object> entry in nodeData)
                {
                    string key = entry.Key.ToString();
                    if (key == "Filename" || key == "TextureFile" || key == "InventoryItem")
                        continue;
                    newItemObject.Set(key, entry.Value);
                }
            }
        }
        saveGame.Close();
    }

    public void SaveGameScene(Node parent)
    {
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);
        GD.Print("saving Game...");
        var saveNodes = parent.GetTree().GetNodesInGroup("PersistGarden");
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
            GD.Print("saving GameScene...", nodeData.ToString());
        }

        saveGame.Close();

    }

    public void SaveParticularScene(Node parentInventory, Node parentScene, string nameSceneFolder)
    {
        var saveNodes = parentScene.GetTree().GetNodesInGroup("Garden");
        GD.Print("Nodes: " + saveNodes.Count);
        //var saveItemInventory = parentInventory.GetTree().GetNodesInGroup("GardenInventory");
        var saveSceneGame = new File();
        saveSceneGame.Open("user://save" + nameSceneFolder + ".save", File.ModeFlags.Write);
        GD.Print("saving Game...");
        foreach (Node saved in saveNodes)
        {
            // Check the node is an instanced scene so it can be instanced again during load.
            if (saved.Filename.Empty())
            {
                GD.Print(String.Format("persistent node '{0}' is not an instanced scene, skipped", saved.Name));
                continue;
            }

            // Check the node has a save function.
            if (!saved.HasMethod("Save"))
            {
                GD.Print(String.Format("persistent node '{0}' is missing a Save() function, skipped", saved.Name));
                continue;
            }

            // Call the node's save function.
            var nodeData = saved.Call("Save");

            saveSceneGame.StoreLine(JSON.Print(nodeData));
            GD.Print("saving GameScene...", nodeData.ToString());
        }

        //SaveInventory(parentInventory);
        saveSceneGame.Close();
    }

    public bool LoadGardenOneScene(HFlowContainer parentInventory, Control parentTwo)
    {

        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGardenOne.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGardenOne.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            var newFungi = nodeData["Fungi"];
            var newCoin = nodeData["Coin"];
            newObject.leftLocationPath = nodeData["LeftPath"].ToString();
            newObject.rightLocationPath = nodeData["RightPath"].ToString();
            if (newCoin == null)
            {
                var coin = newObject.GetNode<Sprite>("Coin");
                coin.QueueFree();
            }
            if (newFungi == null)
            {

                var fungi = newObject.GetNode<Sprite>("Fungi");
                fungi.QueueFree();
            }

            parentTwo.AddChild(newObject);


            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" ||
                    key == "Parent" ||
                    key == "Coin" ||
                    key == "Fungi" ||
                    key == "PosXcoin" ||
                    key == "PosYcoin" ||
                    key == "PosXfungi" ||
                    key == "PosYfungi" ||
                    key == "LeftPath" ||
                    key == "RightPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;

        }

        saveGame.Close();
        return found;
    }

    public bool LoadGardenTwoScene(HFlowContainer parentOne, Control parentTwo)
    {
        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGardenTwo.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGardenTwo.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            var newCoin = nodeData["Coin"];
            newObject.leftLocationPath = nodeData["LeftPath"].ToString();
            newObject.rightLocationPath = nodeData["RightPath"].ToString();

            if (newCoin == null)
            {
                var coin = newObject.GetNode<Sprite>("Coin");
                coin.QueueFree();
            }
            parentTwo.AddChild(newObject);
            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Coin" || key == "PosXcoin" || key == "PosYcoin" || key == "LeftPath" || key == "RightPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;
        }
        saveGame.Close();
        return found;
    }
    public bool LoadGardenThreeScene(HFlowContainer parentOne, Control parentTwo)
    {
        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGardenThree.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGardenThree.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            var newCoin = nodeData["Vision"];
            newObject.leftLocationPath = nodeData["LeftPath"].ToString();
            newObject.rightLocationPath = nodeData["RightPath"].ToString();

            if (newCoin == null)
            {
                var coin = newObject.GetNode<Sprite>("Vision");
                coin.QueueFree();
            }
            parentTwo.AddChild(newObject);
            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Vision" || key == "PosXvision" || key == "PosYvision" || key == "LeftPath" || key == "RightPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;
        }

        saveGame.Close();
        return found;
    }

    public bool LoadGreenHouseOneScene(HFlowContainer parentOne, Control parentTwo)
    {

        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGreenHouseOne.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGreenHouseOne.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            newObject.backLocationPath = nodeData["BackPath"].ToString();
            newObject.leftLocationPath = nodeData["LeftPath"].ToString();
            newObject.rightLocationPath = nodeData["RightPath"].ToString();
            bool newPlate = (bool)nodeData["VisibilityPlate"];
            if (!newPlate)
            {
                GD.Print("Plate should not be there");
                var plate = newObject.GetNode<Sprite>("Plate");
                plate.QueueFree();
            }

            parentTwo.AddChild(newObject);


            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "VisibilityPlate" || key == "PosX" || key == "PosY" || key == "LeftPath" || key == "RightPath" || key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;

        }

        saveGame.Close();
        return found;
    }

    public bool LoadGreenHouseTwoScene(HFlowContainer parentOne, Control parentTwo)
    {

        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGreenHouseTwo.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGreenHouseTwo.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
           
            newObject.backLocationPath = nodeData["BackPath"].ToString();
            newObject.rightLocationPath = nodeData["RightPath"].ToString();
            newObject.leftLocationPath = nodeData["LeftPath"].ToString();
            float sackPosX = (float)nodeData["PosXSack"];
            float sackPosY = (float)nodeData["PosYSack"];


            var screwdriver = nodeData["Screwdriver"];
            var sack = newObject.GetNode<Sprite>("Sack");
            Vector2 positionSack = new Vector2(sackPosX, sackPosY);
            sack.Position = positionSack;
            if (screwdriver == null)
            {
                var screwDriver = newObject.GetNode<Sprite>("Screwdriver");
                screwDriver.QueueFree();
            } 
            parentTwo.AddChild(newObject);


            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Screwdriver" || key == "Sack" || key == "PosXSack" || key == "PosYSack" || key == "LeftPath" || key == "RightPath" || key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;

        }

        saveGame.Close();
        return found;
    }

    public bool LoadGreenHouseThreeScene(HFlowContainer parentOne, Control parentTwo)
    {

        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGreenHouseThree.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGreenHouseThree.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            newObject.backLocationPath = nodeData["BackPath"].ToString();
            newObject.rightLocationPath = nodeData["RightPath"].ToString();
            newObject.leftLocationPath = nodeData["LeftPath"].ToString();

            var secretCompartiment = newObject.GetNode<Sprite>("SecretCompartiment");
            var woodenPlank = newObject.GetNode<Sprite>("WoodenPlank");
            var areaKnob = newObject.GetNode<Area2D>("Area2D");
            
            var knob = areaKnob.GetNode<Sprite>("Knob");
            bool sextant = (bool)nodeData["Sextant"];
            
            bool visibilityKnob = (bool)nodeData["KnobVisibility"];
            bool secretCompartimentVisibility = (bool)nodeData["SecretCompartimentVisibility"];
            float woodenPlankPosX = (float)nodeData["WoodenPlankPosX"];
            float woodenPlankPosY = (float)nodeData["WoodenPlankPosY"];
            var locked = (bool)nodeData["Locked"];
            float areaKnobPosX = (float)nodeData["AreaKnobPosX"];
            float areaKnobPosY = (float)nodeData["AreaKnobPosY"];
            Vector2 positionWoodenPlank = new Vector2(woodenPlankPosX, woodenPlankPosY);
            Vector2 positionArea = new Vector2(areaKnobPosX, areaKnobPosY);
            if (visibilityKnob)
            {
                knob.Visible = true;
                var s = (AttachableObject)areaKnob;
                s.unlockedSlide = true;
            }
            else
            {
                knob.Visible = visibilityKnob;
            }
            if (secretCompartimentVisibility)
            {
                secretCompartiment.Visible = secretCompartimentVisibility;
                if (!sextant)
                {
                    secretCompartiment.removeAllChildren();
                }
                
            }
            else
            {
                secretCompartiment.Visible = secretCompartimentVisibility;
            }
            
            woodenPlank.Position = positionWoodenPlank;
            areaKnob.Position = positionArea;

            parentTwo.AddChild(newObject);


            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Locked" ||  key == "KnobVisibility" || key == "SecretCompartimentVisibility" || key == "WoodenPlankPosY" || key == "WoodenPlankPosX" || key == "AreaKnobPosX" || key == "AreaKnobPosY" || key == "LeftPath" || key == "RightPath" || key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;
        }
        saveGame.Close();
        return found;
    }

    public bool LoadGardenSextantScene(HFlowContainer parentOne, Control parentTwo)
    {
        GD.Print("LOAD SCENE CANNOT BELIEVE IT!!!!");
        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGardenSextant.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGardenSextant.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            
            var background = newObject.GetNode<Sprite>("SextantGardenBackground");
            var sunComplited = newObject.GetNode<Sprite>("SunCompleted");
            var sextantView = newObject.GetNode<Sprite>("SextantView");
            var angleRuler = newObject.GetNode<Label>("Ruler/RichTextLabel");
            var ruler = newObject.GetNode<Sprite>("Ruler");

            newObject.backLocationPath = nodeData["BackPath"].ToString();
            Vector2 position;
            Vector2 positionSun;
            positionSun.x = (float)nodeData["PositionSunCompletedX"];
            positionSun.y = (float)nodeData["PositionSunCompletedY"];

            bool sunCompletedVisibility = (bool)nodeData["SunCompletedVisibility"];

            string angles = (string)nodeData["Degree"];
            
            if (sunCompletedVisibility)
            {
                sextantView.Visible = (bool)nodeData["SextantVisibility"];
                sunComplited.Visible = true;
                sunComplited.Position = positionSun;
                position.x = (float)nodeData["PositionBackgroundX"];
                position.y = (float)nodeData["PositionBackgroundY"];
                background.Position = position;
                ruler.Visible = true;
                angleRuler.Text = angles.ToString();
            }
           
            parentTwo.AddChild(newObject);

            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || 
                    key == "Parent" ||
                    key == "PositionBackgroundX" ||
                    key == "PositionBackgroundY" ||
                    key == "SunVisibility" ||
                    key == "PositionSunCompletedX" ||
                    key == "PositionSunCompletedY" ||
                    key == "SunCompletedVisibility" ||
                    key == "BackPath" ||
                    key == "Degree" ||
                    key == "RulerBlocked" ||
                    key == "SextantVisibility"
                   )
                    continue;
                newObject.Set(key, entry.Value);
            }
            found = true;
        }

        saveGame.Close();
        return found;
    }
    public bool LoadDiaryScene(HFlowContainer parentOne, Control parentTwo)
    {
        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveDiary.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveDiary.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            newObject.backLocationPath = nodeData["BackPath"].ToString();
            parentTwo.AddChild(newObject);

            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY" || key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;
        }

        saveGame.Close();
        return found;
    }

    public bool LoadWorkingTableScene(HFlowContainer parentOne, Control parentTwo)
    {
        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveWorkingTable.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveWorkingTable.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            bool standClassicVisibility = (bool)nodeData["StandClassisVisibility"];
            bool sextantUnfinishedVisibility = (bool)nodeData["SextantUnfinished"];
            bool sextandFinishedVisibility = (bool)nodeData["finishedSextantVisibility"];
            bool sextandVisibility = (bool)nodeData["SextandVisibility"];

            Sprite standClassic = newObject.GetNode<Sprite>("Stand"); ;
            Sprite sextandStand = newObject.GetNode<Sprite>("SectantUnfinished");
            Sprite sextandClomplited = newObject.GetNode<Sprite>("FinishedSextant"); ;
            Sprite sextand = newObject.GetNode<Sprite>("CompletedSextant");

            if (standClassicVisibility)
            {
                standClassic.Visible = standClassicVisibility;
            }
            else
            {
                standClassic.Visible = false;
            }
            if (sextantUnfinishedVisibility)
            {
                sextandStand.Visible = sextantUnfinishedVisibility;
            }
            else
            {
                sextandStand.Visible = false;
            }
            if (sextandFinishedVisibility)
            {
                sextandClomplited.Visible = sextandFinishedVisibility;
            }
            else
            {
                sextandClomplited.Visible = false;
            }
            if (sextandVisibility)
            {
                sextand.Visible = sextandVisibility; ;
            }
            else
            {
                sextand.Visible = false;
            }
            parentTwo.AddChild(newObject);

            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
              
                if (key == "Filename" || 
                    key == "Parent" || 
                    key == "StandClassisVisibility" ||
                    key == "finishedSextantVisibility" ||
                    key == "SextantUnfinished" ||
                    key == "SextandVisibility" ||
                    key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;

        }

        saveGame.Close();
        return found;
    }

    public bool LoadGrutaScene(HFlowContainer parentOne, Control parentTwo)
    {
        bool found = false;
        var saveGame = new File();
        if (!saveGame.FileExists("user://saveGrutaScene.save"))
        {
            return found; // Error! We don't have a save to load.
        }
        else
        {
            GD.Print("FILE FOUNT LOAD");
        }

        saveGame.Open("user://saveGrutaScene.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();
            var newCandle = nodeData["Candle"];
            newObject.backLocationPath = nodeData["BackPath"].ToString();
            if (newCandle == null)
            {
                GD.Print("Candle should be there");                
                var candleSprite = newObject.GetNode<Sprite>("Candle");
                candleSprite.QueueFree();
            }
            parentTwo.AddChild(newObject);
            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Candle" || key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;
        }

        saveGame.Close();
        return found;
    }



}

