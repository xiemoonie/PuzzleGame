using Godot;
using System;
using Object = Godot.Object;

public class Soldering : Area2D
{
    TextureRect itemToDisplay;
    Sprite stand;
    Sprite sextantUnfinished;
    Sprite sextandClomplited;
    Sprite sextand;
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    public override void _Ready()
    {
        base._Ready();
        stand = this.GetParent().GetNodeOrNull<Sprite>("Stand");
        sextantUnfinished = this.GetParent().GetNodeOrNull<Sprite>("SextantUnfinished");
        sextandClomplited = this.GetParent().GetNodeOrNull<Sprite>("SextantFinished");
        sextand = this.GetParent().GetNodeOrNull<Sprite>("SextantCompleted");

    }

    void placeSextant()
    {
        itemToDisplay.Texture = null;
        var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        s.eraseItem();
        stand.Visible = false;
        sextantUnfinished.Visible = true;
        WorldDictionary.setStateObject("Sextant", 3);
        WorldDictionary.setStateObject("Stand", 4);
        WorldDictionary.setStateObject("SextantUnfinished", 2);


        GameSaver.SaveGameScene();
    }

    void placeVision()
    {
        itemToDisplay.Texture = null;
        var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        s.eraseItem();
        sextantUnfinished.Visible = false;
        sextandClomplited.Visible = true;
        WorldDictionary.setStateObject("Sextant", 3);
        WorldDictionary.setStateObject("Vision", 3);
        WorldDictionary.setStateObject("SextantUnfinished", 3);
        WorldDictionary.setStateObject("SextantFinished",2);
        GameSaver.SaveGameScene();
    }

    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
                itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
                if (sextandClomplited != null && sextandClomplited.Visible)
                {
                    InventoryItem item = itemScene.Instance<InventoryItem>();
                    stand.Visible = true;
                    sextantUnfinished.Visible = false;
                    sextandClomplited.Visible = false;
                    s.pickedItem(item, sextand);
                    WorldDictionary.setStateObject("Stand", 2);
                    WorldDictionary.setStateObject("SextantVision", 1);
                    WorldDictionary.setStateObject("SextantFinished", 3);
                    GameSaver.SaveGameScene();
                }
                if (itemToDisplay.Texture != null && itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/sextant.PNG" && sextantUnfinished!=null && !sextantUnfinished.Visible)
                {
                    placeSextant();
                }
                if (sextantUnfinished != null && sextantUnfinished.Visible && sextandClomplited != null && !sextandClomplited.Visible && itemToDisplay.Texture != null && itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/vision.PNG")
                {
                    placeVision();
                }
               
              
            }
        }

    }
}
