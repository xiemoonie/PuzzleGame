using Godot;
using System;
using System.Diagnostics;

public class SetPlate : Area2D
{
    bool platePlaced = false;
    bool candlePlaced = false;
    bool flamePlaced = false;
    bool potPlaced = false;
    bool meltingPotEmpty = false;
    bool meltingPotMetal = false;
    private Sprite plate;
    private Sprite candle;
    private Sprite flame;
    private Sprite potCompleted;
    private Sprite potMelted;
    private Sprite potEmpty;
    private Sprite spoonMelted;
    private float counter = 1.5f;
    InventoryManager inventory;
    [Export] string pathResourcePlate = "";
    [Export] string pathGuiResourcePlate = "";
    [Export] string pathResourceCandle = "";
    [Export] string pathGuiResourceCandle = "";
    [Export] string pathResourceFlame = "";
    [Export] string pathGuiResourceFlame = "";
    [Export] string pathResourcePot = "";
    [Export] string pathGuiResourcePot = "";
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");

    public override void _Ready()
    {
        plate = GetParent().GetNodeOrNull<Sprite>("Plate");
        candle = GetParent().GetNodeOrNull<Sprite>("Candle");
        flame = GetParent().GetNodeOrNull<Sprite>("Flame");
        potCompleted = GetParent().GetNodeOrNull<Sprite>("MeltingPotCompleted");
        potMelted = GetParent().GetNodeOrNull<Sprite>("MeltingPotMelted");
        potEmpty = GetParent().GetNodeOrNull<Sprite>("MeltingPotEmpty");
        spoonMelted = GetParent().GetNodeOrNull<Sprite>("SpoonMelted");

    }

    public override void _PhysicsProcess(float delta)
    {
        if (meltingPotMetal && counter <= 10 && !(counter < 0)) {
            counter += counter * delta;
            GD.Print("the counter for pot is: " +counter);
        }else if (counter > 10){
            GD.Print("the last sprite should appear bigger than 10");
            potMelted.Visible = true;
            potCompleted.Visible = false;
            meltingPotEmpty = true;
            counter = -1;
        }
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
            item.Visible = true;
            inventory.eraseItem();
            WorldDictionary.setStateObject(item.Name, 2);
            GameSaver.SaveGameScene();
            flamePlaced = true;
        }
    }
    public void placePot(Sprite item, string texture)
    {
        if (texture == pathResourcePot || texture == pathGuiResourcePot)
        {
            GD.Print("pot placed 3");
            item.Visible = true;
            inventory.eraseItem();
            WorldDictionary.setStateObject(item.Name, 2);
            GameSaver.SaveGameScene();
            potPlaced = true;
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
                    placeFlame(flame, s.Texture.ResourcePath);

                }
                else if (potPlaced == false && s != null && s.Texture != null && s.Texture.ResourcePath != null)
                {
                    placePot(potCompleted, s.Texture.ResourcePath);
                    meltingPotMetal= true;
                }
                else if (potPlaced == true && meltingPotMetal && meltingPotEmpty)
                {
                    GD.Print("grabbed item for melting");
                    InventoryItem item = itemScene.Instance<InventoryItem>();
                    potMelted.Visible = false;
                    potEmpty.Visible = true;
                    inventory.pickedItem(item, spoonMelted);
                    meltingPotEmpty = false;

                }

            }
        }
    }
}
