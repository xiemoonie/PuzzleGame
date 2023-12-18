using Godot;
using System;

namespace LogicGame1.Scripts.Location
{
    public class DiaryLocationHolder : LocationHolder
    {
        Vector2 position;

        Sprite background;
        Sprite first;
        Sprite back;

        Sprite middle;
        Sprite page;
        Sprite one;
        Sprite two;
        Sprite three;

        Sprite lastSprite;

        int pageNum = 0;

        bool open = false;
        string backPath;

        public override void _Ready()
        {
            base._Ready();

            
            first = GetNode<Sprite>("First");
            back = GetNode<Sprite>("Last");

            middle = GetNode<Sprite>("Middle");
            one = GetNode<Sprite>("One");
            two = GetNode<Sprite>("Middle/Two");
            three = GetNode<Sprite>("Middle/Three");
            page = GetNode<Sprite>("Middle/Page");
            backPath = base.backLocationPath;
        }

        public void goRight()
        {
            if (open == false)
            {
                open = true;
                first.Visible = false;
                one.Visible = true;
                
            }
            else
            {
                pageNum++;
                GD.Print("derecha ", pageNum);
                switch (pageNum)
                {
                    case 1: one.Visible = true; break;
                    case 2: middle.Visible= true; page.Visible = true; one.Visible = false; two.Visible = true; break;
                    case 3: two.Visible = false; three.Visible = true; break;
                    case 4: page.Visible = false; three.Visible = false; break;
                    case 5: middle.Visible = false; pageNum = 0; first.Visible = true; open = false; break;

                }
            }
        }

        public void goLeft()
        {
            if (open != false && pageNum > 0)
            {
                pageNum--;
                GD.Print("izquierda", pageNum);
                switch (pageNum)
                {
                 
                    case 0: one.Visible = false; first.Visible = true; open = false; break;
                    case 1: two.Visible = false; one.Visible = true; middle.Visible = false; break;
                    case 2: three.Visible = false; two.Visible = true; break;
                    case 3: two.Visible = false; three.Visible = true; page.Visible = true; break;

                        
                }

            }
        }

        public override Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "GardenThing", first},
                { "PosX", position.x + 20.0f},
                { "PosY", position.y - 20.0f},
                { "BackPath", backPath}

            };
        }


    }
}