using Godot;
using System;

public class Settings : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Pressed()
    {
        base._Pressed();
        GD.Print("setting...");
 
    }
}
