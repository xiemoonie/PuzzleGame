using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public static class WorldDictionary
{

    static string currentScene;

    static Dictionary<string, string> objectsResources = new Dictionary<string, string>()
      {
      {"Vision", "res://Images/Locations/MoonieDrawing/vision.PNG"},
      {"Stand","res://Images/Locations/MoonieDrawing/stand.PNG"},
      {"Sextant","res://Images/Locations/MoonieDrawing/sextant.PNG"},
      {"SextantVision","res://Images/Locations/MoonieDrawing/sextantVision.PNG"},
      {"SextantFinished","res://Images/Locations/MoonieDrawing/sextantFinished.PNG" },
      {"SextantUnfinished","res://Images/Locations/MoonieDrawing/sextantUnfinished.PNG" },
      {"SextantView","res://Images/Locations/MoonieDrawing/SextantView.png"},
      {"Candle","res://Images/Locations/MoonieDrawing/Candle.PNG"},
      {"Fungi", "res://Images/Gui/FungiKey.PNG"},
      {"Sun", "res://Images/Locations/MoonieDrawing/sun.PNG"},
      {"Ruler", "res://Images/Locations/MoonieDrawing/Ruler.PNG"},
      {"Screwdriver", "res://Images/Gui/ScrewDriver.PNG"},
      {"Plate", "res://Images/Locations/MoonieDrawing/Plate.PNG"},
      {"SecretCompartiment", "res://Images/Locations/MoonieDrawing/SecretCompartiment.PNG"},
      {"Knob", "res://Images/Locations/MoonieDrawing/CoinPNG.png"},
      {"SextantGardenBackground", "res://Images/Locations/MoonieDrawing/gardenSun.png"}
};

    static Dictionary<string, int> objectState = new Dictionary<string, int>()
      {
      {"Coin",0},
      {"Vision",0},
      {"Stand",0},
      {"Sextant",0},
      {"SextantVision",0},
      {"SextantFinished",0},
      {"SextantView",0},
      {"SextantGardenBackground",0},
      {"Candle",0},
      {"Fungi", 0},
      {"Sun", 0},
      {"Ruler", 0},
      {"Screwdriver", 0},
      {"Plate",0},
      {"SecretCompartiment", 0},
      {"Knob", 0},
};
    static IDictionary<string, string> locations = new Dictionary<string, string>(){
    {"GardenOne", "res://Scenes/Locations/GardenLocation/GardenOne.tscn"},
    {"GardenTwo", "res://Scenes/Locations/GardenLocation/GardenTwo.tscn"},
    {"GardenThree", "res://Scenes/Locations/GardenLocation/GardenThree.tscn"},
    {"GardenHouseOne", "res://Scenes/Locations/GardenLocation/GardenHouseOne.tscn"},
    {"GardenHouseTwo", "res://Scenes/Locations/GardenLocation/GardenHouseTwo.tscn"},
    {"GardenHouseThree", "res://Scenes/Locations/GardenLocation/GardenHouseThree.tscn"},
    {"GardenHouseWorkingTable", "res://Scenes/Locations/GardenLocation/GardenHouseWorkingTable.tscn"},
    {"GardenSextant", "res://Scenes/Locations/GardenLocation/GardenSextant.tscn"},
    {"GardenGruta", "res://Scenes/Locations/GardenLocation/GardenGruta.tscn"},
    {"GardenDiary", "res://Scenes/Locations/GardenLocation/GardenDiary.tscn"},
};
    static int getStat(string objectName)
    {
        return 0;
    }
    static public string getSceneBack()
    {
        switch (currentScene)
        {
            case "GardenGruta": setCurrentScene("GardenOne"); return locations["GardenOne"];
            case "GardenSextant": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            case "GardenHouseOne": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            case "GardenHouseTwo": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            case "GardenHouseThree": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            case "GardenHouseWorkingTable": setCurrentScene("GardenHouseOne"); return locations["GardenHouseOne"];
            case "GardenDiary": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            default: throw new Exception($"Cannot go Back, Unknown scene \"{currentScene}\"");
        }
    }

    static public string getSceneLeft()
    {
        switch (currentScene)
        {
            case "GardenOne": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            case "GardenTwo": setCurrentScene("GardenThree"); return locations["GardenThree"];
            case "GardenThree": setCurrentScene("GardenOne"); return locations["GardenOne"];
            case "GardenHouseOne": setCurrentScene("GardenHouseTwo"); return locations["GardenHouseTwo"];
            case "GardenHouseTwo": setCurrentScene("GardenHouseThree"); return locations["GardenHouseThree"];
            case "GardenHouseThree": setCurrentScene("GardenHouseOne"); return locations["GardenHouseOne"];
            default: throw new Exception($"Cannot go Left, Unknown scene \"{currentScene}\"");
        }
    }
    static public string getSceneRight()
    {
        switch (currentScene)
        {
            case "GardenOne": setCurrentScene("GardenThree"); return locations["GardenThree"];
            case "GardenTwo": setCurrentScene("GardenOne"); return locations["GardenOne"];
            case "GardenThree": setCurrentScene("GardenTwo"); return locations["GardenTwo"];
            case "GardenHouseOne": setCurrentScene("GardenHouseThree"); return locations["GardenHouseThree"];
            case "GardenHouseTwo": setCurrentScene("GardenHouseOne"); return locations["GardenHouseOne"];
            case "GardenHouseThree": setCurrentScene("GardenHouseTwo"); return locations["GardenHouseTwo"];
            default: throw new Exception($"Cannot go Right, Unknown scene \"{currentScene}\"") ;
        }
    }
    static public string getSceneZoom(string zoomedObject)
    {
        switch (currentScene)
        {
            case "GardenOne":
                switch (zoomedObject)
                {
                    case "Gruta": setCurrentScene("GardenGruta"); return locations["GardenGruta"];
                    default: throw new Exception($"Cannot zoom in scene \"{currentScene}\", Unknown zoomedObj \"{zoomedObject}\"");
                }
            case "GardenHouseOne":
                switch (zoomedObject)
                {
                    case "WorkingTable": setCurrentScene("GardenHouseWorkingTable"); return locations["GardenHouseWorkingTable"];
                    default: throw new Exception($"Cannot zoom in scene \"{currentScene}\", Unknown zoomedObj \"{zoomedObject}\"");
                }
            case "GardenTwo":
                switch (zoomedObject)
                {
                    case "House": setCurrentScene("GardenHouseOne"); return locations["GardenHouseOne"];
                    case "Diary": setCurrentScene("GardenDiary"); return locations["GardenDiary"];
                    case "Pillar": setCurrentScene("GardenSextant"); return locations["GardenSextant"];
                    default: throw new Exception($"Cannot zoom in scene \"{currentScene}\", Unknown zoomedObj \"{zoomedObject}\"");
                }

            default: throw new Exception($"Cannot zoom in UNKNOWN scene \"{currentScene}\", zoomedObj was \"{zoomedObject}\""); ;
        }
    }
    static public string getMainScene()
    {
        return locations["GardenOne"];
    }
    static public void setCurrentScene(string NameScene)
    {
        currentScene = NameScene;
    }
    static public string getCurrentScene()
    {
        return currentScene;
    }
    static public int getStateObject(string objectName)
    {
        return objectState[objectName];
    }
    static public List<string> getInventoryObject()
    {

        List<string> objectsPath = new List<string>();
        foreach (var i in objectState)
        {
            if (i.Value == 1)
            {
                objectsPath.Add(objectsResources[i.Key]);
            }
        }
        return objectsPath;
    }
    static public void setStateObject(string objectName, int state)
    {
        objectState[objectName] = state;
    }
    public static Dictionary<string, int> GetObjectState()
    {
        return objectState;
    }
    static public List<string> getKeys()
    {
        List<string> keyList = new List<string>(objectState.Keys);
        return keyList;
    }
    public static Dictionary<string, int> returnDictionary()
    {
        return objectState;
    }
    public static int checkObjectStatuScene(string NameObject)
    {
        Dictionary<string, int> objectsState = objectState;
        foreach (KeyValuePair<string, int> o in objectsState)
        {
            if (NameObject == o.Key)
            {
                return o.Value;
            }
        }
        return 0;
    }

}
