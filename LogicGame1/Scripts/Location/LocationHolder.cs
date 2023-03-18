using Godot;
using System;

public class LocationHolder : Node {

    [Export] public string leftLocationPath = "";
    [Export] public string rightLocationPath = "";
    [Export] public string backLocationPath = "";

    private Sprite ss;
    
    public override void _Ready() {
        base._Ready();

        GC.Collect();
    }
   
}
