using Godot;
using System;

public class Menu : Node
{
   
    public override void _Ready()
    {
        GD.Print("hello menu");

        
    }

  
   public void Buttons()
    {
        TextureButton playButton = GetNode<TextureButton>("Play");
        TextureButton loadButton = GetNode<TextureButton>("LoadGame");
        TextureButton settingButton = GetNode<TextureButton>("Settings");
        TextureButton quitButton = GetNode<TextureButton>("Quit");


    }


}





