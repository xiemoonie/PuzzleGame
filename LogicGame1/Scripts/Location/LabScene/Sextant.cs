using Godot;
using System;
using Object = Godot.Object;

public class Sextant : Area2D
{
    Node parent;
    Sprite background;
    Sprite sextantView;
    Sprite sun;
    Sprite sunCompleted;
    Sprite ruler;
    public TextureRect itemToDisplay;
    public Label degrees;
    public float degreeValue;
    AudioStreamPlayer2D click;
    public bool locked = false;
    public override void _Ready()
    {
        parent = this.GetParent();
        background = parent.GetNode<Sprite>("SextantGardenBackground");
        ruler = parent.GetNode<Sprite>("Ruler");
        sextantView = parent.GetNode<Sprite>("SextantView");
        sun = parent.GetNode<Sprite>("Sun");
        sunCompleted = parent.GetNode<Sprite>("SunCompleted");
        degrees = ruler.GetNode<Label>("RichTextLabel");
        degreeValue = 100;
        click = parent.GetNode<AudioStreamPlayer2D>("click");
        itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
    }

    public void MoveRight()
    {
        if (background != null)
        {
            Vector2 position = background.Position;

            if (position.x >= -600)
            {
                position.x -= 50;
                background.Position = position;
            }
        }
    }
    public void MoveLeft()
    {
        if (background != null)
        {
            Vector2 position = background.Position;

            if (position.x <= 2500)
            {
                position.x += 50;
                background.Position = position;
            }
        }
    }
    public void MoveUp()
    {
        Vector2 position = background.Position;
        if (position.y <= 1000)
        {
            position.y += 50;
            background.Position = position;
        }

    }
    public void MoveDown()
    {
        Vector2 position = background.Position;
        if (position.y >= -300)
        {
            position.y -= 50;
            background.Position = position;
        }
    }
    public void MoveUpRuler()
    {

        if (sun != null)
        {
            Vector2 position = sun.Position;

            if (position.y >= -600)
            {
                float axisY = Mathf.InverseLerp(-600, 800, position.y);
                position.y -= 50;
                int invY = (int)Mathf.Lerp(0, 180, 1 - axisY);
                sun.Position = position;
                degreeValue = invY;
                degrees.Text = degreeValue.ToString();
            }
        }
    }
    public void MoveDownRuler()
    {

        if (sun != null)
        {
            Vector2 position = sun.Position;

            if (position.y <= 800)
            {
                float axisY = Mathf.InverseLerp(-600, 800, position.y);
                position.y += 50;
                int invY = (int)Mathf.Lerp(0, 180, 1 - axisY);
                sun.Position = position;
                degreeValue = invY;
                degrees.Text = degreeValue.ToString();
            }

        }
    }

    public bool InPlace()
    {
        Vector2 positionSun = sun.Position;
        Vector2 positionBackground = background.Position;

        if (positionBackground.x == 2475 && positionBackground.y == 0 && positionSun.x == 960 && positionSun.y == 300)
        {
            click.Play();
            sun.Visible = false;
            sunCompleted.Visible = true;
            var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
            s.eraseItem();
            WorldDictionary.setStateObject("SextantGardenBackground", 3);
            WorldDictionary.setStateObject("SextantVision", 3);
            GameSaver.SaveGameScene();
            return true;

        }
        else
        {
            return false;
        }
    }

    public void lockedScene()
    {
        Vector2 positionSun = sun.Position;
        Vector2 positionBackground = background.Position;

        if (positionBackground.x == 2475 && positionBackground.y == 0 && positionSun.x == 960 && positionSun.y == 300)
        {
            click.Play();
        }
    }
    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
                if (itemToDisplay.Texture != null && !InPlace())
                {
                    GD.Print("item to display" + itemToDisplay.Texture.ResourcePath);
                    if (itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/sextantVision.PNG")
                    {
                        ruler.Visible = true;
                        sextantView.Visible = true;
                        sun.Visible = true;
                        if (this.Name == "Right")
                        {
                            GD.Print("Right");
                            MoveRight();
                        }
                        if (this.Name == "Left")
                        {
                            GD.Print("Left");
                            MoveLeft();
                        }
                        if (this.Name == "Up")
                        {
                            GD.Print("Up");
                            MoveUp();
                        }
                        if (this.Name == "Down")
                        {
                            GD.Print("Down");
                            MoveDown();
                        }
                        if (this.Name == "DownRuler")
                        {
                            MoveUpRuler();
                        }
                        if (this.Name == "UpRuler")
                        {
                            MoveDownRuler();
                        }
                    }
                    else
                    {
                        sextantView.Visible = false;
                        ruler.Visible = false;
                        sun.Visible = false;
                    }
                }
                else
                {
                    lockedScene();
                }

            }
        }
    }
}
