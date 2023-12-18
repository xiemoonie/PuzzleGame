using Godot;
using System;
using System.Collections.Generic;
public class GameSaver
{
    public void someFun(Node parentInventory, Node parentScene)
    {
        SaveGameScene(parentScene);
        SaveInventory(parentInventory);
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
        SceneController myScript = parentInventory.GetNode<SceneController>("/root/Main/SceneController");
        GD.Print("Going to MENUUUU");

        myScript.goToMenu();
        saveGame.Close();
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
                parentOne.AddChild(newItemObject);


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
        var saveItemInventory = parentInventory.GetTree().GetNodesInGroup("GardenInventory");
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
        saveSceneGame.Close();


        foreach (Node saveNode in saveItemInventory)
        {
            // SaveInventory(parentInventory);
        }
    }

    public bool LoadGardenOneScene(HFlowContainer parentOne, Control parentTwo)
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

                var coin = newObject.GetNode<Sprite>("Coin/Coin");
                coin.QueueFree();
            }
            if (newFungi == null)
            {

                var fungi = newObject.GetNode<Sprite>("Fungi/Fungi");
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

                var coin = newObject.GetNode<CanvasLayer>("Vision");
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
            parentTwo.AddChild(newObject);


            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "Coin" || key == "PosX" || key == "PosY" || key == "LeftPath" || key == "RightPath" || key == "BackPath")
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



            parentTwo.AddChild(newObject);


            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY" || key == "LeftPath" || key == "RightPath")
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
            

            bool visibilityKnob = (bool)nodeData["KnobVisibility"];
            bool secretCompartimentVisibility = (bool)nodeData["SecretCompartimentVisibility"];
            float woodenPlankPosX = (float)nodeData["WoodenPlankPosX"];
            float woodenPlankPosY = (float)nodeData["WoodenPlankPosY"];
            float areaKnobPosX = (float)nodeData["AreaKnobPosX"];
            float areaKnobPosY = (float)nodeData["AreaKnobPosY"];
            Vector2 positionWoodenPlank = new Vector2(woodenPlankPosX, woodenPlankPosY);
            Vector2 positionArea = new Vector2(areaKnobPosX, areaKnobPosY);
            if (visibilityKnob)
            {
                knob.Visible = visibilityKnob;
            }
            else
            {
                knob.Visible = visibilityKnob;
            }
            if (secretCompartimentVisibility)
            {
                secretCompartiment.Visible = secretCompartimentVisibility;
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
                if (key == "Filename" || key == "Parent" || key == "KnobVisibility" || key == "SecretCompartimentVisibility" || key == "WoodenPlankPosY" || key == "WoodenPlankPosX" || key == "AreaKnobPosX" || key == "AreaKnobPosY" || key == "LeftPath" || key == "RightPath" || key == "BackPath")
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
            GD.Print(" INDISE OF THE MADNESS!!!!");

            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (LocationHolder)newObjectScene.Instance();

            var background = newObject.GetNode<Sprite>("SextantGardenBackground");
            var sunComplited = newObject.GetNode<Sprite>("SunCompleted");
            var sun = newObject.GetNode<Sprite>("Sun");
            var sextantView = newObject.GetNode<Sprite>("SextantView");
            var ruler = newObject.GetNode<Sprite>("Ruler");
            var angleRuler = newObject.GetNode<Label>("Ruler/RichTextLabel");

            newObject.backLocationPath = nodeData["BackPath"].ToString();
            Vector2 position;


            position.x = (float)nodeData["PositionBackgroundX"];
            position.y = (float)nodeData["PositionBackgroundY"];
            background.Position = position;
            bool sunVisibility = (bool)nodeData["SunVisibility"];
            bool sunCompletedVisibility = (bool)nodeData["SunCompletedVisibility"];
            bool sextantVisibility = (bool)nodeData["SextantViewVisibility"];
            string angles = nodeData["AnglesRuler"].ToString();
            if (sunVisibility)
            {
                position.x = (float)nodeData["PositionSunX"];
                position.y = (float)nodeData["PositionSunY"];
                sunComplited.Visible = false;
                sun.Visible = true;
                sun.Position = position;
            }
            if (sunCompletedVisibility)
            {
                sun.Visible = false;
                sunComplited.Visible = true;
                position.x = (float)nodeData["PositionSunCompletedX"];
                position.y = (float)nodeData["PositionSunCompletedY"];
            }
            if (sextantVisibility)
            {
                sextantView.Visible = true;
                ruler.Visible = true;

            }
            else
            {
                sextantView.Visible = false;
            }
            angleRuler.Text = angles;


            parentTwo.AddChild(newObject);

            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "PositionBackgroundX" || key == "PositionBackgroundY" || key == "PositionSunX" || key == "PositionSunY" || key == "SunVisibility" || key == "PositionSunCompletedX" || key == "PositionSunCompletedY" || key == "SunCompletedVisibility" || key == "SextantViewVisibility" || key == "AnglesRuler" || key == "BackPath")
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
            bool sextantStandVisibility = (bool)nodeData["SextantStandVisibility"];
            bool sextandClomplitedVisibility = (bool)nodeData["SextandClomplitedVisibility"];
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
            if (sextantStandVisibility)
            {
                sextandStand.Visible = sextantStandVisibility;
            }
            else
            {
                sextandStand.Visible = false;
            }
            if (sextandClomplitedVisibility)
            {
                sextandClomplited.Visible = sextandClomplitedVisibility;
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
                if (key == "Filename" || key == "Parent" || key == "StandClassisVisibility" || key == "SextantStandVisibility" || key == "SextandClomplitedVisibility" || key == "SextandVisibility" || key == "BackPath")
                    continue;
                newObject.Set(key, entry.Value);
            }

            found = true;

        }

        saveGame.Close();
        return found;
    }



}

