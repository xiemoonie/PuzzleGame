using Godot;
using System;
using Object= Godot.Object;

public class Sextant : Area2D
{
    Node parent;
    Sprite background;
    Sprite sextantView;
    Sprite sun;
    Sprite sunCompleted;
    Sprite ruler;
    TextureRect itemToDisplay;
    Label degrees;
    float degreeValue;
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
       GD.Print("hey I am being called");
       degreeValue = 100;
       click = parent.GetNode<AudioStreamPlayer2D>("click");
        itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        /*
        if (itemToDisplay.Texture != null && itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/CompletedSextant.PNG")
        {
            sextantView.Visible = true;
            ruler.Visible = true;
            sun.Visible = true;
        }
        else
        {
            sextantView.Visible = false;
            ruler.Visible = false;
            sun.Visible = false;
        }
        */
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
                //GD.Print("RightBackGround X:" + position.x);
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
           //     GD.Print("Left BackGround X:" + position.x);
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
          //  GD.Print("Up BackGround Y:" + position.y);
        }
       
    }
    public void MoveDown()
    {
        Vector2 position = background.Position;
        if (position.y >= -300)
        {
            position.y -= 50;
            background.Position = position;
          //  GD.Print("Down BackGround Y:" + position.y);
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
                int invY = (int)Mathf.Lerp(0,180, 1-axisY);
                sun.Position = position;
                degreeValue  = invY; //+
              //  GD.Print("Up SUN Y:" + position.y);
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
                float axisY = Mathf.InverseLerp(-600, 800, position.y);// -700, 800, position.y
                position.y += 50;
                int invY = (int)Mathf.Lerp(0,180, 1-axisY);
                sun.Position = position;
                degreeValue  = invY;
             //   GD.Print("Down SUN Y:" + position.y);
                degrees.Text = degreeValue.ToString();
            }
          
        }
    }

    public bool InPlace()
    {
        Vector2 positionSun = sun.Position;
        Vector2 positionBackground = background.Position;

        if (positionBackground.x == 2475 && positionBackground.y == 0  && positionSun.x == 960  && positionSun.y == 300)
        {
            click.Play();
            sun.Visible = false;
            sunCompleted.Visible = true;
            var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
            s.eraseItem();
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
                    if (itemToDisplay.Texture.ResourcePath == "res://Images/Locations/MoonieDrawing/CompletedSextant.PNG")
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
