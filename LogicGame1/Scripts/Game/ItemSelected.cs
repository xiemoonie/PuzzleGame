using Godot;
using System;
using System.Threading;
using System.Timers;
using Object = Godot.Object;


class ItemSelected : TextureRect
{
    bool click;
    Vector2 positionMouse;
    Vector2 mousePosition;
    float counter;
    InventoryItem some;
    public override void _Ready()
    {
        counter = 0;
    }
    public override void _Process(float delta)
    {
        if (click)
        {
            counter += delta;
            GD.Print("the counter "+counter);
        }
        else
        {
            counter = 0;
        }
        if (positionMouse != Vector2.Zero && counter >= 1)
        {
            some.SetGlobalPosition(positionMouse);
        }
    }
    public override void _GuiInput(InputEvent _event)
    {
        base._GuiInput(_event);
        if (_event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
                some = GetOwner<InventoryItem>();
                s.selectedItem(some);
                click = true;
            }
            else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                click = false;
                positionMouse = Vector2.Zero;
            }
          
        }

        if (_event is InputEventMouseMotion mouseMoveEvent)
        {
            if (click && counter >= 1)
            {
                var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
                some = GetOwner<InventoryItem>();
                var groups = some.GetGroups();
                foreach (string g in groups)
                {
                    if (g == "Combinable")
                    {
                        Color c = new Color(0, 0, 0, (float)0.5);
                        some.Modulate = c;
                        positionMouse = mouseMoveEvent.GlobalPosition;
                    }
                }
               
            }
            
        }
       
     }
}


