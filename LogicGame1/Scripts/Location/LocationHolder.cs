using Godot;
using System;

public class LocationHolder : Node {

    [Export] public string leftLocationPath = "";
    [Export] public string rightLocationPath = "";
    [Export] public string backLocationPath = "";
    [Export] public string sceneName = "";

    private Sprite ss;
    private Vector2 position;

    private Sprite coin;
    private Area2D element2;

    public override void _Ready() {
        base._Ready();
        
       // element1 = GetNode<Area2D>("cube/Sprite/Area2D");
        coin = GetNode<Sprite>("Coin/Coin");
        position = coin.GetPosition();
        GC.Collect();
    }

    public Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
    {
        { "Filename", this.Filename},
        { "Parent", GetParent().GetParent()},
        { "Coin", coin},
        { "PosX", position.x + 20.0f}, 
        { "PosY", position.y - 20.0f}

    };
    }



}
