using Godot;
using System;

public abstract class LocationHolder : Node {

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
       
        GC.Collect();
    }


    //to each Garden class make inherit LocationHolder make it 
    public abstract Godot.Collections.Dictionary<string, object> Save();
    
    


}
