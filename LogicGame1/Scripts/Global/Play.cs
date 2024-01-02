using Godot;
using System;
using System.IO;

public class Play : TextureButton
{
    private PackedScene GameTemplate;


    private Control ScreenContent;
    public override void _Ready()
    {


    }


    public override void _Pressed()
    {
        base._Pressed();
        GD.Print("play game...");


        ScreenContent = GetNode<Control>("/root/Main/Screen");
        ScreenContent.removeAllChildren();

        GameTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn");

        var gameWrapperGame = GameTemplate.Instance<GameWrapper>();
        gameWrapperGame.LocationToLoad = "res://Scenes/Locations/GardenLocation/GardenOne.tscn";
        ScreenContent.AddChild(gameWrapperGame);
        EraseFiles();

    }

    public void EraseFiles()
    {
        string[] filePaths = System.IO.Directory.GetFiles("C:\\Users\\carod\\AppData\\Roaming\\Godot\\app_userdata\\LogicGame1");

        foreach (string filePath in filePaths)
        {
            var name = new FileInfo(filePath).Name;
            name = name.ToLower();
            if (name != "logs")
            {
                System.IO.File.Delete(filePath);
            }
        }
    }

}

