using Godot;
using System;

public class ClickOnGruta: Area2D
{
    Vector2 PositionMouse;
    Sprite PressedRock;
    private PackedScene pressedRock;
    private int nodesGraph = 6;
    bool created = false;
    float counter = 0;
    public int pressedCounter;
    int[,] puzzlePieceArray =   { {0,1,0,0,0,0},
                                {1,0,1,0,1,0 },
                                {0,1,0,0,1,0 },
                                {0,0,0,1,0,0 },
                                {0,1,1,0,0,1 },
                                {0,0,0,0,1,0 } };
    public override void _Ready()
    {
        pressedRock = ResourceLoader.Load<PackedScene>("res://Objects/Locations/Gruta/PuzzlePiece.tscn");
       
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (!created)
        {
            if (counter < 0.5)
            {
                counter += delta;
            }
            else
            {
                created = true;
                createPressedButtons();
            }
        }
        
        
    }

    public void createPressedButtons()
    {

        Vector2 InitialPositionPressed = new Vector2(580, 130);
        for (int i = 0; i < nodesGraph; i++)
        {
            for (int j = 0; j < nodesGraph; j++)
            {
                Node rr = pressedRock.Instance();
                var PuzzlePieceRR = rr.GetNode<PuzzlePiece>("PuzzlePiece");
                PuzzlePieceRR.setIDPuzzlePiece(i, j);
                GetParent().AddChild(rr);
                Area2D sss = rr.GetNode<Area2D>("PuzzlePiece");
                sss.GlobalPosition = InitialPositionPressed + new Vector2(i*130 , 100*j);
                if (puzzlePieceArray[i, j] == 1)
                {
                    PuzzlePieceRR.setKeyPuzzlePiece(1);
                }
                else
                {
                    PuzzlePieceRR.setKeyPuzzlePiece(0);
                }
            }

        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (pressedCounter == 11)
        {
            pressedCounter = 0;
            var parentPuzzle = GetParent().GetParent();
            var s = parentPuzzle.GetChildren(); 
            foreach (Node n in s)
            {
                if (n.GetType().ToString() == "Godot.Node2D")
                {
                    n.QueueFree();
                }
            }
            parentPuzzle = GetParent();
            parentPuzzle.QueueFree();
            WorldDictionary.setStateObject("PuzzleGruta", 3);
            GameSaver.SaveGameScene();
            GrutaScene gruta = GetNode<GrutaScene>("/root/Main/Screen/GameWrapper/SceneContainer/GardenGruta");
            gruta.OpenGruta();
        }
    }

    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                PositionMouse = mouseEvent.GlobalPosition; 
            }

        }
    }
}
