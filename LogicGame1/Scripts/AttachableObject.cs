using Godot;
using System;

public class AttachableObject : Area2D
{
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    private PackedScene game = (PackedScene)GD.Load("res://Scenes/Game.tscn");
    GameWrapper gameWrapper;
    [Export] String pathResource = "";
    [Export] String pathGuiResource = "";

    [Export] public float xShape;
    InventoryManager inventory;

    public bool unlockedSlide = false;
    public override void _Ready()
    {
        gameWrapper = game.Instance<GameWrapper>();
        inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
    }

    public void checkForKey()
    {
        var s = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        if (s != null)
        {
            GD.Print("Resource path" + s.Texture.ResourcePath);
            if (inventory.onlyOneSelected())
            {
                if (s.Texture.ResourcePath == pathResource || s.Texture.ResourcePath == pathGuiResource)
                {
                    Sprite knob = GetNode<Sprite>("Knob");
                    if (knob != null)
                    {
                        knob.Visible = true;
                        inventory.eraseItem();
                        unlockedSlide = true;
                        WorldDictionary.setStateObject("Knob", 2);
                        GameSaver.SaveGameScene();
                    }
                }
            }
           
        }
     
    }
    
    public void ReshapeArea2D()
    { 
        Vector2 posKnob = this.GlobalPosition;
        Node parent = this.GetParent();
        Sprite woodenPlank = parent.GetNode<Sprite>("WoodenPlank");
        Vector2 posWoodenPlank = woodenPlank.GlobalPosition;

        if (posKnob.x >= 420)
        {
            posKnob.x -= 200;
            posKnob.y = 235;
            posWoodenPlank.x -= 75;
            posWoodenPlank.y = 700;
            woodenPlank.Position = posWoodenPlank;
            this.Position = posKnob;
        }
        else
        {
            posKnob.x = 230;
            posKnob.y = 235;
            posWoodenPlank.x = 500;
            posWoodenPlank.y = 700;
            this.GlobalPosition = posKnob;
            woodenPlank.Position = posWoodenPlank;
            ShowSecretCompartiment();
        }
    }

    public void ShowSecretCompartiment()
    {
        Node parent = this.GetParent();
        Sprite secretCompartiment = parent.GetNode<Sprite>("SecretCompartiment");
        Sprite woodenPlank = parent.GetNode<Sprite>("WoodenPlank");
        secretCompartiment.Visible = true;
        WorldDictionary.setStateObject(secretCompartiment.Name, 3);
        WorldDictionary.setStateObject(Name, 3);
        WorldDictionary.setStateObject("Fungi", 3);
        GameSaver.SaveGameScene();
    }
    
  
    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                if (unlockedSlide == false)
                {
                    GD.Print("check fot key");
                    checkForKey();
                }
                else
                {
                    GD.Print("reshape area");
                    ReshapeArea2D();
                }
            }

        }

     }
    }