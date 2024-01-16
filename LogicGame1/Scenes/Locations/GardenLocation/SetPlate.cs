using Godot;
using System;

public class SetPlate : Area2D
{
    bool platePlaced = false;
    bool candlePlaced = false;
    private Sprite plate;
    private Sprite candle;
    [Export] string pathResourcePlate = "";
    [Export] string pathGuiResourcePlate = "";
    [Export] string pathResourceCandle = "";
    [Export] string pathGuiResourceCandle = "";
    public override void _Ready()
    {
        plate = GetParent().GetNodeOrNull<Sprite>("Plate");
        candle = GetParent().GetNodeOrNull<Sprite>("Candle");
    }

    public void placePlate(Sprite item, string texture)
    {
        if (texture == pathResourcePlate || texture == pathGuiResourcePlate)
            {
                    item.Visible = true;
                    var inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
                    inventory.eraseItem();
                    WorldDictionary.setStateObject(item.Name, 2);
                    GameSaver.SaveGameScene();
                    platePlaced = true;
            }
     }
    public void placeCandle(Sprite item, string texture)
    {
        
            if (texture == pathResourceCandle || texture == pathGuiResourceCandle)
            {
                item.Visible = true;
                var inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
                inventory.eraseItem();
                WorldDictionary.setStateObject(item.Name, 2);
                GameSaver.SaveGameScene();
                candlePlaced = true;
            }
     }
    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                var s = GetNodeOrNull<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
                if (platePlaced == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                {
                    placePlate(plate, s.Texture.ResourcePath);
                }
                else
                {
                    if (candlePlaced == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                    {
                        placeCandle(candle, s.Texture.ResourcePath);
                        
                    }
                }
            }
        }
    }
}