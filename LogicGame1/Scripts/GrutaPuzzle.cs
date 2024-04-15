using Godot;
using System;

public class GrutaPuzzle : Area2D
{
    InventoryManager inventory;
    Sprite puzzle;
    bool cleaned = false;
    [Export] string pathResourceCloth = "";
    [Export] string pathGuiResourceCloth = "";
    public override void _Ready()
    {
        puzzle = GetParent().GetNodeOrNull<Sprite>("PuzzleGruta");
        inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
    }
    public void placePuzzle (Sprite item, string texture)
    {
        if(inventory.onlyOneSelected())
        {
            if (texture == pathResourceCloth || texture == pathGuiResourceCloth)
            {
                GD.Print("Should be visible" + item.Name);
                item.Visible = true;
                inventory.eraseItem();
                WorldDictionary.setStateObject(item.Name, 2);
                WorldDictionary.setStateObject("Cloth", 3);
                GameSaver.SaveGameScene();
                cleaned = true;
                QueueFree();

            }
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
                if (cleaned == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                {
                    placePuzzle(puzzle, s.Texture.ResourcePath);
                    GameSaver.SaveGameInvenotry();
                }

            }

            }
    }
}
