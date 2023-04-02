using Godot;
using System;

public class SaveButton : TextureButton
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
        GD.Print("click saving...");
        GameSaver myScript = new GameSaver();
        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        inventory.getSprites();
        Node myGameWrapper = GetNode<Node>("/root/Main");
        myScript.someFun(inventory, myGameWrapper);
    }


}
