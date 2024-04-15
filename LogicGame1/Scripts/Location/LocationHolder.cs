using Godot;
using System;

public abstract class LocationHolder : Node {

    [Export] public bool RightPath;
    [Export] public bool LeftPath;
    [Export] public bool BackPath;

    private Sprite ss;
    private Vector2 position;

    private Sprite coin;
    private Area2D element2;

    public override void _Ready() {
        base._Ready();
        GC.Collect();
        GameSaver.SaveGameScene();
        GD.Print("......................Saved in the inventory.................");
        GameSaver.SaveGameInvenotry();
    }
}
