using Godot;
using System;
using Object = Godot.Object;

public class Soldering : Area2D
{
    TextureRect itemToDisplay;
    Sprite standClassic;
    Sprite sextandStand;
    Sprite sextandClomplited;
    Sprite sextand;
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    public override void _Ready()
    {
        base._Ready();
        standClassic = this.GetParent().GetNode<Sprite>("Stand");
        sextandStand = this.GetParent().GetNode<Sprite>("SectantUnfinished");
        sextandClomplited = this.GetParent().GetNode<Sprite>("FinishedSextant");
        sextand = this.GetParent().GetNode<Sprite>("CompletedSextant");

    }

    void placeSextant()
    {
        itemToDisplay.Texture = null;
        var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        s.eraseItem();
        standClassic.Visible = false;
        sextandStand.Visible = true;

    }

    void placeVision()
    {
        itemToDisplay.Texture = null;
        var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        s.eraseItem();
        sextandStand.Visible = false;
        sextandClomplited.Visible = true;


    }

    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {

                itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
                if (sextandClomplited.Visible)
                {
                    InventoryItem item = itemScene.Instance<InventoryItem>(); ;
                    var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
                    //sextand.Visible = true;
                    standClassic.Visible = true;
                    s.pickedItem(item, sextand);
                    sextandClomplited.Visible = false;
                    sextandStand.Visible = false;
                }
                if (itemToDisplay.Texture != null && itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/sextant.PNG" && sextandStand!=null && !sextandStand.Visible)
                {
                    placeSextant();
                }
                if (sextandStand != null && sextandStand.Visible && !sextandClomplited.Visible && itemToDisplay.Texture != null && itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/vision.PNG")
                {
                    placeVision();
                }
               
              
            }
        }

    }
}
