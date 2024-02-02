using Godot;
using System;

public class SetPlate : Area2D
{
    bool platePlaced = false;
    bool candlePlaced = false;
    bool flamePlaced = false;
    private Sprite plate;
    private Sprite candle;
    private Sprite flame;
    InventoryManager inventory;
    [Export] string pathResourcePlate = "";
    [Export] string pathGuiResourcePlate = "";
    [Export] string pathResourceCandle = "";
    [Export] string pathGuiResourceCandle = "";
    [Export] string pathResourceFlame = "";
    [Export] string pathGuiResourceFlame = "";
    public override void _Ready()
    {
        plate = GetParent().GetNodeOrNull<Sprite>("Plate");
        candle = GetParent().GetNodeOrNull<Sprite>("Candle");
        flame = GetParent().GetNodeOrNull<Sprite>("Flame");
    }
    public void placePlate(Sprite item, string texture)
    {
        if (texture == pathResourcePlate || texture == pathGuiResourcePlate)
        {
            item.Visible = true;
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
            inventory.eraseItem();
            WorldDictionary.setStateObject(item.Name, 2);
            GameSaver.SaveGameScene();
            candlePlaced = true;
        }
    }
    public void placeFlame(Sprite item, string texture)
    {
        if (texture == pathResourceFlame || texture == pathGuiResourceFlame)
        {
            GD.Print("flame placed 2");
            item.Visible = true;
            inventory.eraseItem();
            WorldDictionary.setStateObject(item.Name, 2);
            GameSaver.SaveGameScene();
            flamePlaced = true;
        }
    }
    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                var s = GetNodeOrNull<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
                if (platePlaced == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                {
                    placePlate(plate, s.Texture.ResourcePath);
                }
                else if (candlePlaced == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                {
                    placeCandle(candle, s.Texture.ResourcePath);

                }
                else if (flamePlaced == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                {
                    GD.Print("flame placed 1");
                    placeFlame(flame, s.Texture.ResourcePath);

                }
            }
        }
    }
}
